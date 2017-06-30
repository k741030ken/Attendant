'****************************************************
'功能說明：單位分行註記檔
'建立人員：John
'建立日期：2017/06/07
'****************************************************
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common

Public Class AT2

    Private Property eHRMSDB_ITRD As String
        Get
            Dim result As String = ConfigurationManager.AppSettings("eHRMSDB")
            If String.IsNullOrEmpty(result) Then
                result = "eHRMSDB_ITRD"
            End If
            Return result
        End Get
        Set(ByVal value As String)

        End Set
    End Property

#Region "AT2000 單位分行註記檔"
#Region "AT2000 單位分行註記檔-查詢"
    Public Function OrgBranchMarkGridViewQuery(ParamArray Params() As String) As DataTable
        Dim ht As Hashtable = Bsp.Utility.getHashTableFromParam(Params)
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" O.CompID, C.CompName, O.DeptID, O2.OrganName AS DeptName, O.OrganID, O1.OrganName AS OrganName ")
        strSQL.AppendLine(" , CASE WHEN O.BranchFlag = '1' THEN '是' ELSE '否' END AS BranchFlag ")
        strSQL.AppendLine(" , O.LastChgComp + '-' + C1.CompName AS LastChgComp, O.LastChgID + '-' + P.NameN AS LastChgID ")
        strSQL.AppendLine(" , CASE WHEN CONVERT(Char(10), O.LastChgDate, 111) = '1900/01/01' THEN '' ElSE Convert(Varchar, O.LastChgDate, 120) END AS LastChgDate ")
        strSQL.AppendLine(" FROM OrgBranchMark O ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Company C ON C.CompID = O.CompID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Organization O1 ON O.CompID = O1.CompID and O.DeptID = O1.DeptID and O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Organization O2 on O.CompID = O2.CompID and O.DeptID = O2.DeptID and O.DeptID = O2.OrganID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Company C1 ON O.LastChgComp = C1.CompID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Personal P ON O.LastChgComp = P.CompID AND O.LastChgID = P.EmpID ")
        strSQL.AppendLine(" WHERE 1 = 1 ")

        For Each strKey As String In ht.Keys
            If Bsp.Utility.IsStringNull(ht(strKey)) <> "" Then
                Select Case strKey
                    Case "CompID"
                        strSQL.AppendLine("AND O.CompID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "DeptID"
                        strSQL.AppendLine("AND O.DeptID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                    Case "OrganID"
                        strSQL.AppendLine("AND O.OrganID = " & Bsp.Utility.Quote(ht(strKey).ToString()))
                End Select
            End If
        Next

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function

    Public Function GetOrgBranchMarkData(ByVal strCompID As String, ByVal strDeptID As String, ByVal strOrganID As String) As DataTable
        Dim strSQL As New StringBuilder()

        strSQL.AppendLine(" SELECT ")
        strSQL.AppendLine(" O.CompID, C.CompName, O.DeptID, O2.OrganName AS DeptName, O.OrganID, O1.OrganName AS OrganName, O.BranchFlag ")
        strSQL.AppendLine(" , O.LastChgComp, O.LastChgID ")
        strSQL.AppendLine(" , LastChgDate = CASE WHEN CONVERT(Char(10), O.LastChgDate, 111) = '1900/01/01' THEN '' ElSE Convert(Varchar, O.LastChgDate, 120) END ")
        strSQL.AppendLine(" FROM OrgBranchMark O ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Company C ON C.CompID = O.CompID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Organization O1 ON O.CompID = O1.CompID and O.DeptID = O1.DeptID and O.OrganID = O1.OrganID ")
        strSQL.AppendLine(" LEFT JOIN " + eHRMSDB_ITRD + ".[dbo].Organization O2 on O.CompID = O2.CompID and O.DeptID = O2.DeptID and O.DeptID = O2.OrganID ")
        strSQL.AppendLine(" WHERE O.CompID = '" + strCompID.ToString + "' ")
        strSQL.AppendLine("AND O.DeptID = '" + strDeptID.ToString + "' ")
        strSQL.AppendLine("AND O.OrganID = '" + strOrganID.ToString + "' ")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString(), "AattendantDB").Tables(0)
    End Function
#End Region

#Region "PA1902 工作地點設定-修改"
    Public Function UpdateOrgBranchMarkSetting(ByVal beOrgBranchMark As beOrgBranchMark.Row) As Boolean
        Dim bsOrgBranchMark As New beOrgBranchMark.Service()
        Dim strSQL As New StringBuilder()

        Using cn As DbConnection = Bsp.DB.getConnection("AattendantDB")
            cn.Open()
            Dim tran As DbTransaction = cn.BeginTransaction

            Try
                If bsOrgBranchMark.Update(beOrgBranchMark, tran) = 0 Then Return False
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
#End Region
End Class
