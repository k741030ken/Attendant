'****************************************************
'功能說明：OV5000的查詢Funct
'建立人員：Jason
'建立日期：2017/01/19
'修改日期：2017/01/24
'****************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class OV5

#Region "下拉選單"
    Enum DisplayType
        OnlyName    '只顯示名字
        OnlyID      '顯示ID
        Full        '顯示ID + 名字
    End Enum

    Public Shared Sub FillDDL(ByVal objDDL As DropDownList, ByVal strTabName As String, ByVal strValue As String, ByVal strText As String, Optional ByVal Type As DisplayType = DisplayType.Full, Optional ByVal CondStr As String = "", Optional ByVal Distinct As String = "", Optional ByVal JoinStr As String = "")
        Dim objOV As New OV5()
        Try
            Using dt As DataTable = objOV.GetDDLInfo(strTabName, strValue, strText, CondStr, Distinct, JoinStr)
                With objDDL
                    .Items.Clear()
                    .DataSource = dt
                    Select Case Type
                        Case DisplayType.OnlyID
                            .DataTextField = "Code"
                        Case DisplayType.OnlyName
                            .DataTextField = "CodeCName"
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
        strSQL.AppendLine(" " & strValue & " AS Code, " & strText & " AS CodeCName, " & strValue & " + '-' + " & strText & " AS FullName " & " FROM " & strTabName)
        If JoinStr <> "" Then strSQL.AppendLine(" " + JoinStr + " ")
        strSQL.AppendLine(" Where 1=1 ")
        If CondStr <> "" Then strSQL.AppendLine(CondStr)
        strSQL.AppendLine("Order By " & strValue & " ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function
#End Region

#Region "OV5000 代碼類別下拉選單"
    Public Shared Sub FillFldName_OV5000(ByVal objDDL As DropDownList, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objOV As New OV5
        Try
            Using dt As DataTable = objOV.GetFldName_OV5000()
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

    Public Function GetFldName_OV5000() As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT RTrim(FldName) + '\' + RTrim(Code) AS FldName, CodeCName, RTrim(FldName)+'\'+RTrim(Code) + '-' + CodeCName AS FullName ")
        strSQL.AppendLine(" FROM AT_CodeMap WHERE TabName = 'HRCodeMap_OpenMaintain' ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function
#End Region

#Region "OV5000 代碼下拉選單"
    Public Shared Sub FillCode_OV5000(ByVal objDDL As DropDownList, ByVal FldName As String, ByVal Code As String, Optional ByVal Type As Bsp.Enums.DisplayType = Bsp.Enums.DisplayType.Full)
        Dim objOV As New OV5
        Try
            Using dt As DataTable = objOV.GetCode_OV5000(FldName, Code)
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

    Public Function GetCode_OV5000(ByVal FldName As String, ByVal Code As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine(" SELECT RTrim(Code) AS Code, CodeCName, Code + '-' + CodeCName AS FullName FROM AT_CodeMap ")
        strSQL.AppendLine(" WHERE TabName='" + FldName + "' AND FldName ='" + Code + "' ORDER BY TabName, FldName, SortFld ")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "AattendantDB").Tables(0)
    End Function
#End Region

    ''' <summary>
    ''' 設定排序順序(0~99)
    ''' </summary>
    ''' <param name="objDDL"></param>
    ''' <remarks></remarks>
    Public Sub GetSortFld(ByVal objDDL As DropDownList)
        '排序順序,預設值為0,數值0~99　(僅支援.NET 3.5以上)
        objDDL.DataSource = Enumerable.Range(0, 100)
        objDDL.DataBind()
    End Sub

    ''' <summary>
    ''' 使用下拉表單做查詢條件查詢
    ''' </summary>
    ''' <param name="beManageCodeSet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryByDDL(ByVal beManageCodeSet As beManageCodeSet.Row) As DataTable
        Dim bsManageCodeSet As New beManageCodeSet.Service()
        Dim strSQL As New StringBuilder()

        Try
            strSQL.AppendLine(" AND ")
            If beManageCodeSet.TabName.Value <> "" Then strSQL.AppendLine(" TabName = '" + beManageCodeSet.TabName.Value + "'")
            If beManageCodeSet.FldName.Value <> "" Then strSQL.AppendLine(" AND FldName = '" + beManageCodeSet.FldName.Value + "'")
            If beManageCodeSet.Code.Value <> "" Then strSQL.AppendLine(" AND Code = '" + beManageCodeSet.Code.Value + "'")
            strSQL.AppendLine(" ORDER BY TabName, FldName, SortFld ")
            Return bsManageCodeSet.QuerybyWhere(strSQL.ToString()).Tables(0)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 用CompID、員工編號查詢員工姓名
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="EmpID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmpName(ByVal CompID As String, ByVal EmpID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine("Select NameN From Personal")
        strSQL.AppendLine("Where CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And EmpID = " & Bsp.Utility.Quote(EmpID))

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "eHRMSDB").Tables(0)
    End Function
End Class
