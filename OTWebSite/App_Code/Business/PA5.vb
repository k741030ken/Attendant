'****************************************************
'功能說明：其他參數設定
'建立人員：MickySung
'建立日期：2015.05.25
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA5
#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID
        Full        '顯示ID + 名字
    End Enum

    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal CondStr As String = "", Optional ByVal Distinct As String = "", Optional ByVal JoinStr As String = "")
        Dim objPA As New PA5()
        Try
            Using dt As DataTable = objPA.GetDDLInfo(strTabName, strValue, strText, CondStr, Distinct, JoinStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeName"
                        Case Else
                            .DataTextField = "FullName"
                    End Select
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function GetDDLInfo(ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal CondStr As String = "", Optional ByVal Distinct As String = "", Optional ByVal JoinStr As String = "") As DataTable
        Dim strSQL As New StringBuilder
        strSQL.AppendLine(" Select ")
        If Distinct = "Y" Then strSQL.AppendLine(" Distinct ")
        strSQL.AppendLine(" " & strValue & " AS Code, " & strText & " AS CodeName, " & strValue & " + '-' + " & strText & " AS FullName " & " FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(" " + JoinStr + " ")
        strSQL.AppendLine(" Where 1=1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order By " & strValue & " ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA5100 其他代碼設定"

#Region "PA5100 其他代碼設定-查詢"
    Public Function HRCodeMapSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" H.TabName, H.FldName, H.CodeCName AS CodeName2, H.Code, H.CodeCName, H.SortFld, H.NotShowFlag ")
        strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" ,LastChgDate = Case When Convert(Char(10), H.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, H.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM HRCodeMap H ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON H.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON H.LastChgComp = Pers.CompID AND H.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" WHERE H.TabName IN (SELECT FldName FROM HRCodeMap WHERE TabName='HRCodeMap_OpenMaintain') ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "TabName"
                        strSQL.AppendLine(" AND H.TabName = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FldName"
                        strSQL.AppendLine(" AND H.FldName = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "Code"
                        strSQL.AppendLine(" AND H.Code = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA5101 其他代碼設定-新增"
    Public Function AddHRCodeMapSetting(ByVal beHRCodeMap As beHRCodeMap.Row) As Boolean
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsHRCodeMap.Insert(beHRCodeMap, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

#Region "PA5102 其他代碼設定-修改"
    Public Function UpdateHRCodeMapSetting(ByVal beHRCodeMap As beHRCodeMap.Row, ByVal saveTabName As String, ByVal saveFldName As String, ByVal saveCode As String) As Boolean
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" UPDATE HRCodeMap SET TabName='" + beHRCodeMap.TabName.Value.ToString + "',FldName='" + beHRCodeMap.FldName.Value.ToString + "' ")
        strSQL.AppendLine(" ,Code='" + beHRCodeMap.Code.Value.ToString + "',CodeCName=N'" + beHRCodeMap.CodeCName.Value.ToString + "' ")
        strSQL.AppendLine(" ,SortFld='" + beHRCodeMap.SortFld.Value.ToString + "',NotShowFlag='" + beHRCodeMap.NotShowFlag.Value.ToString + "' ")
        strSQL.AppendLine(" ,LastChgComp='" + beHRCodeMap.LastChgComp.Value.ToString + "',LastChgID='" + beHRCodeMap.LastChgID.Value.ToString + "',LastChgDate= GETDATE() ")
        strSQL.AppendLine(" WHERE TabName='" + saveTabName + "' AND FldName='" + saveFldName + "' AND Code='" + saveCode + "' ")

        'strSQL.AppendLine(" UPDATE HRCodeMap SET Code='" + beHRCodeMap.Code.Value.ToString + "' ")
        'strSQL.AppendLine(" WHERE TabName='" + saveTabName + "' AND FldName='" + saveFldName + "' AND Code='" + saveCode + "' ")

        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")

        Return True
    End Function
#End Region

#Region "PA5103 其他代碼設定-刪除"
    Public Function DeleteHRCodeMapSetting(ByVal beHRCodeMap As beHRCodeMap.Row) As Boolean
        Dim bsHRCodeMap As New beHRCodeMap.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsHRCodeMap.DeleteRowByPrimaryKey(beHRCodeMap, tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "P5100 代碼類別下拉選單"
    Public Shared Sub FillFldName_PA5100(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA5
        Try
            Using dt As DataTable = objPA.GetFldName_PA5100()
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "FldName"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetFldName_PA5100() As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" select RTrim(FldName) + '\' + RTrim(Code) as FldName, CodeCName, RTrim(FldName)+'\'+RTrim(Code) + '-' + CodeCName AS FullName ")
        strSQL.AppendLine(" from HRCodeMap where TabName='HRCodeMap_OpenMaintain' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "P5100 代碼下拉選單"
    Public Shared Sub FillCode_PA5100(ByVal objDDL As DropDownList, ByVal FldName As String, ByVal Code As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA5
        Try
            Using dt As DataTable = objPA.GetCode_PA5100(FldName, Code)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "Code"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetCode_PA5100(ByVal FldName As String, ByVal Code As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" select RTrim(Code) as Code, CodeCName, Code + '-' + CodeCName AS FullName from HRCodeMap ")
        strSQL.AppendLine(" where TabName='" + FldName + "' and FldName='" + Code + "' order by SortFld ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#End Region

#Region "PA5200 功能負責人維護"

#Region "PA5200 功能負責人維護-查詢"
    Public Function MaintainSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" a.CompID,c.CompName,a.FunctionID,isnull(f.CodeCName,'') as FunctionName ")
        strSQL.AppendLine(" , a.Role,case a.Role when '1' then '主管' when '2' then '經辦' else '' end as RoleName ")
        strSQL.AppendLine(" ,a.EmpComp,d.CompName as EmpCompName,a.EmpID,b.NameN as Name,isnull(g.EMail,'') as EMail,a.Telephone,a.Fax ")
        strSQL.AppendLine(" ,Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" ,LastChgDate = Case When Convert(Char(10), a.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, a.LastChgDate, 120) End ")
        strSQL.AppendLine(" from Maintain as a  ")
        strSQL.AppendLine(" join Personal as b on a.EmpComp=b.CompID and a.EmpID=b.EmpID ")
        strSQL.AppendLine(" join Company  as c on a.CompID=c.CompID ")
        strSQL.AppendLine(" join Company  as d on a.EmpComp=d.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON a.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON a.LastChgComp = Pers.CompID AND a.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" left outer join Communication as g on b.IDNo = g.IDNo ")
        strSQL.AppendLine(" left outer join HRCodeMap as f on f.TabName='Maintain' and f.FldName='FunctionID' and f.Code = a.FunctionID and f.NotShowFlag='0' ")
        strSQL.AppendLine(" WHERE 1=1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND a.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "FunctionID"
                        strSQL.AppendLine(" AND a.FunctionID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpID"
                        strSQL.AppendLine(" AND a.EmpID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "EmpName"
                        strSQL.AppendLine(" AND b.NameN like N'%" & ht(strKey).ToString() & "%'")
                End Select
            End If
        Next

        strSQL.AppendLine(" order by a.FunctionID,a.Role ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA5201 功能負責人維護-新增"
    Public Function AddMaintainSetting(ByVal beMaintain As beMaintain.Row) As Boolean
        Dim bsMaintain As New beMaintain.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsMaintain.Insert(beMaintain, tran) = 0 Then Return False
                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

#Region "PA5202 功能負責人維護-修改"
    Public Function UpdateMaintainSetting(ByVal beMaintain As beMaintain.Row) As Boolean
        Dim bsMaintain As New beMaintain.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                strSQL.AppendLine("UPDATE Maintain")
                strSQL.AppendLine("SET FunctionID = @FunctionID")
                strSQL.AppendLine(", Role = @Role")
                strSQL.AppendLine(", EmpComp = @EmpComp")
                strSQL.AppendLine(", EmpID = @EmpID")
                strSQL.AppendLine(", Telephone = @Telephone")
                strSQL.AppendLine(", Fax = @Fax")
                strSQL.AppendLine(", LastChgComp = @LastChgComp")
                strSQL.AppendLine(", LastChgID = @LastChgID")
                strSQL.AppendLine(", LastChgDate = @LastChgDate")
                strSQL.AppendLine("WHERE CompID = @KeyCompID")
                strSQL.AppendLine("AND FunctionID = @KeyFunctionID")
                strSQL.AppendLine("AND Role = @KeyRole")
                strSQL.AppendLine("AND EmpComp = @KeyEmpComp")
                strSQL.AppendLine("AND EmpID = @KeyEmpID")

                Dim DbParam() As DbParameter = { _
                    Bsp.DB.getDbParameter("@FunctionID", beMaintain.FunctionID.Value), _
                    Bsp.DB.getDbParameter("@Role", beMaintain.Role.Value), _
                    Bsp.DB.getDbParameter("@EmpComp", beMaintain.EmpComp.Value), _
                    Bsp.DB.getDbParameter("@EmpID", beMaintain.EmpID.Value), _
                    Bsp.DB.getDbParameter("@Telephone", beMaintain.Telephone.Value), _
                    Bsp.DB.getDbParameter("@Fax", beMaintain.Fax.Value), _
                    Bsp.DB.getDbParameter("@LastChgComp", beMaintain.LastChgComp.Value), _
                    Bsp.DB.getDbParameter("@LastChgID", beMaintain.LastChgID.Value), _
                    Bsp.DB.getDbParameter("@LastChgDate", Now), _
                    Bsp.DB.getDbParameter("@KeyCompID", beMaintain.CompID.Value), _
                    Bsp.DB.getDbParameter("@KeyFunctionID", beMaintain.FunctionID.OldValue), _
                    Bsp.DB.getDbParameter("@KeyRole", beMaintain.Role.OldValue), _
                    Bsp.DB.getDbParameter("@KeyEmpComp", beMaintain.EmpComp.OldValue), _
                    Bsp.DB.getDbParameter("@KeyEmpID", beMaintain.EmpID.OldValue)}

                If Bsp.DB.ExecuteNonQuery(CommandType.Text, strSQL.ToString(), DbParam, tran, "eHRMSDB") = 0 Then Return False

                tran.Commit()
            Catch ex As Exception
                tran.Rollback()
                Throw
            Finally
                If tran IsNot Nothing Then tran.Dispose()
            End Try
        End Using
        Return True
    End Function
#End Region

#Region "PA5203 功能負責人維護-刪除"
    Public Function DeleteHRCodeMapSetting(ByVal beMaintain As beMaintain.Row) As Boolean
        Dim bsMaintain As New beMaintain.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsMaintain.DeleteRowByPrimaryKey(beMaintain, tran)

                tran.Commit()
                inTrans = False
            Catch ex As Exception
                If inTrans Then tran.Rollback()
                Throw
            Finally
                tran.Dispose()
                If cn.State = ConnectionState.Open Then cn.Close()
            End Try
        End Using

        Return True
    End Function
#End Region

#Region "PA5200 功能代碼查詢名稱"
    Public Function FunctionNameQuery(ByVal FunctionID As String) As String
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT CodeCName FROM HRCodeMap WHERE TabName = 'Maintain' AND FldName='FunctionID' AND Code='" + FunctionID + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0).Rows(0).Item(0)
    End Function
#End Region

#End Region

End Class
