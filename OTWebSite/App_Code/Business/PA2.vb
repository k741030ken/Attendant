'****************************************************
'功能說明：組織參數設定
'建立人員：MickySung
'建立日期：2015/04/09
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class PA2
#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID
        Full        '顯示ID + 名字
    End Enum
#End Region

#Region "PA2100 公司班別設定"
#Region "PA2100 公司班別設定-查詢"
    Public Function WorkTimeSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" W.CompID, C.CompName, W.WTID ")
        strSQL.AppendLine(" , Case When W.BeginTime <> '' Then  LEFT(W.BeginTime,'2') + ':' + RIGHT(W.BeginTime,'2') ELSE '' END AS BeginTime ")
        strSQL.AppendLine(" , Case When W.EndTime <> '' Then  LEFT(W.EndTime,'2') + ':' + RIGHT(W.EndTime,'2') ELSE '' END AS EndTime ")
        strSQL.AppendLine(" , Case When W.RestBeginTime <> '' Then  LEFT(W.RestBeginTime,'2') + ':' + RIGHT(W.RestBeginTime,'2') ELSE '' END AS RestBeginTime ")
        strSQL.AppendLine(" , Case When W.RestEndTime <> '' Then  LEFT(W.RestEndTime,'2') + ':' + RIGHT(W.RestEndTime,'2') ELSE '' END AS RestEndTime ")
        strSQL.AppendLine(" , W.AcrossFlag, W.InValidFlag ")
        strSQL.AppendLine(" , W.WTIDTypeFlag + '-' + ISNULL(M1.CodeCName, '') AS WTIDTypeFlag") '2016/03/30 增加欄位
        strSQL.AppendLine(" , W.Remark + '-' + ISNULL(M2.CodeCName, '') AS Remark") '2016/03/30 增加欄位
        strSQL.AppendLine(" , Comp.CompName AS LastChgComp, Pers.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), W.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, W.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM WorkTime W ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = W.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON W.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON W.LastChgComp = Pers.CompID AND W.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap M1 ON W.WTIDTypeFlag = M1.Code AND M1.TabName = 'WorkTime' AND M1.FldName = 'WTIDTypeFlag' ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap M2 ON W.Remark = M2.Code AND M2.TabName = 'WorkTime' AND M2.FldName = 'Remark' ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine(" AND W.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTID"
                        strSQL.AppendLine(" AND W.WTID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BeginTimeStart"   '2015/07/30 modify
                        If ht("BeginTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.BeginTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.BeginTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("BeginTimeEnd").ToString()))
                        End If
                    Case "BeginTimeEnd"   '2015/07/30 modify
                        If ht("BeginTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.BeginTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EndTimeStart"   '2015/07/30 modify
                        If ht("EndTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.EndTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.EndTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("EndTimeEnd").ToString()))
                        End If
                    Case "EndTimeEnd"   '2015/07/30 modify
                        If ht("EndTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.EndTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "RestBeginTimeStart"   '2015/07/30 modify
                        If ht("RestBeginTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.RestBeginTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.RestBeginTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("RestBeginTimeEnd").ToString()))
                        End If
                    Case "RestBeginTimeEnd"   '2015/07/30 modify
                        If ht("RestBeginTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.RestBeginTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "RestEndTimeStart"   '2015/07/30 modify
                        If ht("RestEndTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.RestEndTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.RestEndTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("RestEndTimeEnd").ToString()))
                        End If
                    Case "RestEndTimeEnd"   '2015/07/30 modify
                        If ht("RestEndTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.RestEndTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "AcrossFlag"
                        strSQL.AppendLine(" AND W.AcrossFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "InValidFlag"
                        strSQL.AppendLine(" AND W.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTIDTypeFlag"
                        strSQL.AppendLine(" AND W.WTIDTypeFlag = " & Bsp.Utility.Quote(ht(strKey).ToString())) '2016/03/30 增加欄位
                    Case "Remark"
                        strSQL.AppendLine(" AND W.Remark = " & Bsp.Utility.Quote(ht(strKey).ToString())) '2016/03/30 增加欄位
                End Select
            End If
        Next

        strSQL.AppendLine(" ORDER BY CompID, WTID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2101 公司班別設定-新增"
    Public Function AddWorkTimeSetting(ByVal beWorkTime As beWorkTime.Row) As Boolean
        Dim bsWorkTime As New beWorkTime.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkTime.Insert(beWorkTime, tran) = 0 Then Return False
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

#Region "PA2102 公司班別設定-修改"
    Public Function UpdateWorkTimeSetting(ByVal beWorkTime As beWorkTime.Row) As Boolean
        Dim bsWorkTime As New beWorkTime.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsWorkTime.Update(beWorkTime, tran) = 0 Then Return False
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

#Region "PA2103 公司班別設定-刪除"
    Public Function DeleteWorkTimeSetting(ByVal beWorkTime As beWorkTime.Row) As Boolean
        Dim bsWorkTime As New beWorkTime.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsWorkTime.DeleteRowByPrimaryKey(beWorkTime, tran)

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

#Region "PA2100 公司班別設定-下傳"
    Public Function QueryWorkTimeByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And W.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTID"
                        strSQL.AppendLine("And W.WTID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "BeginTimeStart"   '2015/07/30 modify
                        If ht("BeginTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.BeginTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.BeginTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("BeginTimeEnd").ToString()))
                        End If
                    Case "BeginTimeEnd"   '2015/07/30 modify
                        If ht("BeginTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.BeginTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "EndTimeStart"   '2015/07/30 modify
                        If ht("EndTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.EndTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.EndTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("EndTimeEnd").ToString()))
                        End If
                    Case "EndTimeEnd"   '2015/07/30 modify
                        If ht("EndTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.EndTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "RestBeginTimeStart"   '2015/07/30 modify
                        If ht("RestBeginTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.RestBeginTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.RestBeginTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("RestBeginTimeEnd").ToString()))
                        End If
                    Case "RestBeginTimeEnd"   '2015/07/30 modify
                        If ht("RestBeginTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.RestBeginTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "RestEndTimeStart"   '2015/07/30 modify
                        If ht("RestEndTimeEnd") = "" Then
                            strSQL.AppendLine(" AND W.RestEndTime >= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        Else
                            strSQL.AppendLine(" AND W.RestEndTime BETWEEN " & Bsp.Utility.Quote(ht(strKey).ToString()) & " AND " & Bsp.Utility.Quote(ht("RestEndTimeEnd").ToString()))
                        End If
                    Case "RestEndTimeEnd"   '2015/07/30 modify
                        If ht("RestEndTimeStart") = "" Then
                            strSQL.AppendLine(" AND W.RestEndTime <= " & Bsp.Utility.Quote(ht(strKey).ToString()))
                        End If
                    Case "AcrossFlag"
                        strSQL.AppendLine("And W.AcrossFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "InValidFlag"
                        strSQL.AppendLine("And W.InValidFlag = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTIDTypeFlag"
                        strSQL.AppendLine(" AND W.WTIDTypeFlag = " & Bsp.Utility.Quote(ht(strKey).ToString())) '2016/03/30 增加欄位
                    Case "Remark"
                        strSQL.AppendLine(" AND W.Remark = " & Bsp.Utility.Quote(ht(strKey).ToString())) '2016/03/30 增加欄位
                End Select
            End If
        Next

        strFieldNames.AppendLine("W.CompID as '公司代碼',")
        strFieldNames.AppendLine("isnull(C.CompName,'') as '公司名稱',")
        strFieldNames.AppendLine("W.WTID as '班別代碼',")
        'strFieldNames.AppendLine("W.BeginTime as '上班時間',")
        'strFieldNames.AppendLine("W.EndTime as '下班時間',")
        'strFieldNames.AppendLine("W.RestBeginTime as '休息開始時間',")
        'strFieldNames.AppendLine("W.RestEndTime as '休息結束時間',")

        strFieldNames.AppendLine(" CASE W.BeginTime WHEN '' THEN '' ELSE Left(W.BeginTime, 2) + ':' + Right(W.BeginTime, 2) END AS '上班時間',")
        strFieldNames.AppendLine(" CASE W.EndTime WHEN '' THEN '' ELSE Left(W.EndTime, 2) + ':' + Right(W.EndTime, 2) END AS '下班時間',")
        strFieldNames.AppendLine(" CASE W.RestBeginTime WHEN '' THEN '' ELSE Left(W.RestBeginTime, 2) + ':' + Right(W.RestBeginTime, 2) END AS '休息開始時間',")
        strFieldNames.AppendLine(" CASE W.RestEndTime WHEN '' THEN '' ELSE Left(W.RestEndTime, 2) + ':' + Right(W.RestEndTime, 2) END AS '休息結束時間',")
        strFieldNames.AppendLine("W.AcrossFlag as '跨日註記',")
        strFieldNames.AppendLine("W.InValidFlag as '無效註記',")
        strFieldNames.AppendLine("W.WTIDTypeFlag + '-' + ISNULL(M1.CodeCName, '') as '班別類型',") '2016/03/30 增加欄位
        strFieldNames.AppendLine("W.Remark + '-' + ISNULL(M2.CodeCName, '') as '班別說明',") '2016/03/30 增加欄位
        strFieldNames.AppendLine("W.LastChgComp as '最後異動公司',")
        strFieldNames.AppendLine("W.LastChgID as '最後異動人員',")
        strFieldNames.AppendLine("convert(char(19),W.LastChgDate,20) as '最後異動時間'")

        Return GetWorkTimeWaitInfoByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetWorkTimeWaitInfoByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine(" Select " & FieldNames)
        strSQL.AppendLine(" From WorkTime W")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = W.CompID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON W.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON W.LastChgComp = Pers.CompID AND W.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap M1 ON W.WTIDTypeFlag = M1.Code AND M1.TabName = 'WorkTime' AND M1.FldName = 'WTIDTypeFlag' ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap M2 ON W.Remark = M2.Code AND M2.TabName = 'WorkTime' AND M2.FldName = 'Remark' ")
        strSQL.AppendLine(" Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order by W.CompID, W.WTID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2100 班別代碼下拉選單"
    Public Shared Sub FillWTID_PA2100(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA2
        Try
            Using dt As DataTable = objPA.GetWTID_PA2100(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "WTID"
                    .DataValueField = "WTID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetWTID_PA2100(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT DISTINCT WTID FROM WorkTime WHERE CompID='" + CompID + "' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region
#End Region

#Region "PA2200 單位班別設定"
#Region "PA2200 班別代碼下拉選單"
    Public Shared Sub FillWTID_PA2200(ByVal objDDL As DropDownList, ByVal CompID As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objPA As New PA2
        Try
            Using dt As DataTable = objPA.GetWTID_PA2200(CompID)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    .DataTextField = "FullName"
                    .DataValueField = "WTID"
                    .DataBind()
                End With
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetWTID_PA2200(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT WTID ")
        strSQL.AppendLine(" , WTID + '-' + LEFT(BeginTime, 2) + ':' + RIGHT(BeginTime, 2) + '~' + LEFT(EndTime, 2) + ':' + RIGHT(EndTime, 2) + '　' + Remark + '-' + ISNULL(CodeCName, '') AS FullName ")
        strSQL.AppendLine(" FROM WorkTime ")
        strSQL.AppendLine(" LEFT JOIN HRCodeMap ON Remark = Code AND TabName = 'WorkTime' AND FldName = 'Remark' ")
        strSQL.AppendLine(" WHERE CompID = " + Bsp.Utility.Quote(CompID) + " AND WTIDTypeFlag = '1' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2201 單位代碼下拉選單"
    Public Shared Sub FillOrganID_PA2201(ByVal objDDL As DropDownList, ByVal CompID As String, ByVal DeptID As String, ByVal strType As String, Optional ByVal Type As DisplayType = DisplayType.Full)
        Dim objPA As New PA2()
        Try
            Using dt As DataTable = objPA.GetOrganID_PA2201(CompID, DeptID, strType)
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

    Public Function GetOrganID_PA2201(ByVal CompID As String, ByVal DeptID As String, ByVal strType As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" OrganID AS Code, OrganName AS CodeName, OrganID + '-' + OrganName AS FullName FROM Organization ")

        If strType = "1" Then
            strSQL.AppendLine(" WHERE OrganID = DeptID AND CompID='" + CompID + "' ")
        Else
            strSQL.AppendLine(" WHERE CompID='" + CompID + "' AND DeptID='" + DeptID + "' ") '2015/07/06 midify
        End If
        '2015/05/28 規格變更 排除無效與虛擬單位
        strSQL.AppendLine(" AND InValidFlag = '0' and VirtualFlag = '0' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2200 單位班別設定-查詢"
    Public Function OrgWorkTimeSettingQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" O.CompID, C.CompName, O.DeptID, O2.OrganName AS DeptName, O.OrganID, O1.OrganName AS OrganName, O.WTID ")
        strSQL.AppendLine(" , C1.CompName AS LastChgComp, P.NameN AS LastChgID ")
        strSQL.AppendLine(" , LastChgDate = Case When Convert(Char(10), O.LastChgDate, 111) = '1900/01/01' Then '' ElSE Convert(Varchar, O.LastChgDate, 120) End ")
        strSQL.AppendLine(" FROM OrgWorkTime O ")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = O.CompID ")
        strSQL.AppendLine(" LEFT JOIN Organization O1 ON O.CompID = O1.CompID and O.DeptID = O1.DeptID and O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Organization O2 on O.CompID = O2.CompID and O.DeptID = O2.DeptID and O.DeptID = O2.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Company C1 ON O.LastChgComp = C1.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal P ON O.LastChgComp = P.CompID AND O.LastChgID = P.EmpID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTID"
                        strSQL.AppendLine("AND O.WTID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("AND O.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("AND O.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strSQL.AppendLine(" ORDER BY O.CompID, O.DeptID, O.OrganID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2201 單位班別設定-新增"
    Public Function AddOrgWorkTimeSetting(ByVal beOrgWorkTime() As beOrgWorkTime.Row) As Boolean
        Dim bsOrgWorkTime As New beOrgWorkTime.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrgWorkTime.Insert(beOrgWorkTime, tran) = 0 Then Return False
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

#Region "PA2202 單位班別設定-修改"
    Public Function UpdateOrgWorkTimeSetting(ByVal beOrgWorkTime As beOrgWorkTime.Row, ByVal saveWTID As String, ByVal saveDeptID As String, ByVal saveOrganID As String) As Boolean
        Dim bsOrgWorkTime As New beOrgWorkTime.Service()
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" UPDATE OrgWorkTime SET WTID='" + beOrgWorkTime.WTID.Value.ToString + "',DeptID='" + beOrgWorkTime.DeptID.Value.ToString + "',OrganID='" + beOrgWorkTime.OrganID.Value.ToString + "' ")
        strSQL.AppendLine(" ,LastChgComp='" + beOrgWorkTime.LastChgComp.Value.ToString + "',LastChgID='" + beOrgWorkTime.LastChgID.Value.ToString + "',LastChgDate= GetDate() ")
        strSQL.AppendLine(" WHERE CompID='" + beOrgWorkTime.CompID.Value.ToString + "' AND WTID='" + saveWTID + "' AND DeptID='" + saveDeptID + "' AND OrganID='" + saveOrganID + "' ")
        Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB")

        Return True
    End Function
#End Region

#Region "PA2203 單位班別設定-刪除"
    Public Function DeleteOrgWorkTimeSetting(ByVal beOrgWorkTime As beOrgWorkTime.Row) As Boolean
        Dim bsOrgWorkTime As New beOrgWorkTime.Service()

        Using cn As DbConnection = Bsp.DB.getConnection("eHRMSDB")
            cn.Open()
            Dim tran As Data.Common.DbTransaction = cn.BeginTransaction
            Dim inTrans As Boolean = True

            Try
                bsOrgWorkTime.DeleteRowByPrimaryKey(beOrgWorkTime, tran)

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

#Region "PA2200 單位班別設定-下傳"
    Public Function QueryOrgWorkTimeByDownload(ByVal ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()
        Dim strFieldNames As New StringBuilder()
        Dim aryStr() As String

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("And O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "WTID"
                        strSQL.AppendLine("And O.WTID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("And O.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("And O.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        strFieldNames.AppendLine("O.CompID as '公司代碼',")
        strFieldNames.AppendLine("isnull(C.CompName,'') as '公司名稱',")
        strFieldNames.AppendLine("O.DeptID as '部門代碼',")
        strFieldNames.AppendLine("isnull(O2.OrganName,'') as '部門名稱',")
        strFieldNames.AppendLine("O.OrganID as '科組課代碼',")
        strFieldNames.AppendLine("isnull(O1.OrganName,'') as '科組課名稱',")
        strFieldNames.AppendLine("O.WTID as '班別代碼',")
        strFieldNames.AppendLine("O.LastChgComp as '最後異動公司',")
        strFieldNames.AppendLine("O.LastChgID as '最後異動人員',")
        strFieldNames.AppendLine("convert(char(19),O.LastChgDate,20) as '最後異動時間'")

        Return GetOrgWorkTimeByDownload(strFieldNames.ToString(), strSQL.ToString())
    End Function
    Public Function GetOrgWorkTimeByDownload(Optional ByVal FieldNames As String = "*", Optional ByVal CondStr As String = "") As DataTable
        Dim strSQL As New StringBuilder()

        If FieldNames = "" Then FieldNames = "*"

        strSQL.AppendLine(" Select " & FieldNames)
        strSQL.AppendLine(" From OrgWorkTime O")
        strSQL.AppendLine(" LEFT JOIN Company C ON C.CompID = O.CompID ")
        strSQL.AppendLine(" LEFT JOIN Organization O1 ON O.CompID = O1.CompID and O.DeptID = O1.DeptID and O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Organization O2 on O.CompID = O2.CompID and O.DeptID = O2.DeptID and O.DeptID = O2.OrganID ")
        strSQL.AppendLine(" LEFT JOIN Company Comp ON O.LastChgComp = Comp.CompID ")
        strSQL.AppendLine(" LEFT JOIN Personal Pers ON O.LastChgComp = Pers.CompID AND O.LastChgID = Pers.EmpID ")
        strSQL.AppendLine(" Where 1 = 1")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine(" Order by O.CompID, O.DeptID, O.OrganID ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "PA2200 自行訂義條件Table之資料數"
    Public Function IsDataExists(ByVal strTable As String, ByVal strWhere As String) As Boolean
        Dim strSQL As String
        strSQL = "Select Count(*) Cnt From " & strTable
        strSQL += " Where 1 = 1 " & strWhere
        Return IIf(Bsp.DB.ExecuteScalar(strSQL, "eHRMSDB") > 0, True, False)
    End Function
#End Region
#End Region

End Class
