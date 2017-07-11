'****************************************************
'功能說明：PO1200與OB1200查詢Funct
'建立人員：Jason
'建立日期：2017.07.11
'修改日期：
'****************************************************

Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data

Public Class SPUsrUtility

#Region "部門選單"

    ''' <summary>
    ''' 從資料庫撈取資料組成行政組織的tree
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetOrgMenu(ByVal CompID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.AppendLine("Select OrgType, DeptID, OrganID")
        strSQL.AppendLine(", OrganID + '-' + OrganName As OrganName")
        strSQL.AppendLine(", SortNode = Case When DeptID <> OrganID Then '3' When OrgType = DeptID Then '1' When OrganID = DeptID Then '2' End")
        strSQL.AppendLine(", IsNull(W.Color, '#FFFFFF') Color")
        strSQL.AppendLine(", O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID")
        strSQL.AppendLine("From Organization O")
        strSQL.AppendLine("Left Join OrganColor_Web W On O.CompID = W.CompID and O.SortOrder = W.SortOrder")
        strSQL.AppendLine("Where O.CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.AppendLine("And VirtualFlag = '0'")
        strSQL.AppendLine("And InValidFlag = '0'")
        strSQL.AppendLine("Order By O.InValidFlag, O.SortOrder, Right(O.GroupType, 1), O.GroupID, O.OrgType, O.OrganID")

        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function
#End Region

#Region "部門選單"

    ''' <summary>
    ''' 從資料庫撈取資料組成功能組織的tree
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="BusinessType"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetOrgFlowMenu(ByVal CompID As String, ByVal BusinessType As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.Append("Select BusinessType, RoleCode, UpOrganID, OrganID")
        strSQL.Append(" ,'[' + OrganID + ']　' + OrganName AS OrganName ")
        strSQL.Append(" From OrganizationFlow With(NoLock)")
        strSQL.Append(" Where CompID =" & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And BusinessType = " & Bsp.Utility.Quote(BusinessType))
        strSQL.Append(" And VirtualFlag = '0'")
        strSQL.Append(" And InValidFlag = '0'")
        strSQL.Append(" AND RoleCode <> '' ")
        strSQL.Append(" AND RoleCode IS NOT NULL ")
        strSQL.Append(" Order By RoleCode Desc, OrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

    ''' <summary>
    ''' 從資料庫撈取資料組成功能組織的tree(全業務別)
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="BusinessType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllOrgFlowMenu(ByVal CompID As String, ByVal BusinessType As ArrayList) As DataTable
        Dim strSQL As New StringBuilder()
        Dim strBSType As String = ""

        If (BusinessType.Count <> 0) Then
            For i As Integer = 0 To BusinessType.Count - 1
                BusinessType(i) = Bsp.Utility.Quote(BusinessType(i))
                If i = 0 Then
                    strBSType = BusinessType(i).ToString()
                Else
                    strBSType &= "," & BusinessType(i).ToString()
                End If
            Next
        End If

        strSQL.Append(" SELECT BusinessType, RoleCode, UpOrganID, OrganID ")
        strSQL.Append(" ,'[' + OrganID + ']　' + OrganName AS OrganName ")
        strSQL.Append(" FROM OrganizationFlow WITH(NOLOCK)")
        strSQL.Append(" WHERE CompID = " & Bsp.Utility.Quote(CompID))
        strSQL.Append(" AND VirtualFlag = '0' ")
        strSQL.Append(" AND InValidFlag = '0' ")
        strSQL.Append(" AND RoleCode <> '' ")
        strSQL.Append(" AND RoleCode IS NOT NULL ")
        strSQL.Append(" AND BusinessType in (" + strBSType + ")")

        strSQL.Append(" Order By BusinessType, RoleCode Desc, UpOrganID, OrganID")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

#End Region

    ''' <summary>
    ''' 查詢指定CompID的OrganID是屬於哪種BusinessType
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="OrganID"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetBusinessType(ByVal CompID As String, ByVal OrganID As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.Append("Select BusinessType")
        strSQL.Append(" From OrganizationFlow With(NoLock)")
        strSQL.Append(" Where CompID =" & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And OrganID = " & Bsp.Utility.Quote(OrganID))
        strSQL.Append(" And VirtualFlag = '0'")
        strSQL.Append(" And InValidFlag = '0'")
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
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

    ''' <summary>
    ''' 用CompID、員工姓名查詢員工編號
    ''' </summary>
    ''' <param name="CompID"></param>
    ''' <param name="EmpName"></param>
    ''' <returns>System.Data.DataSet</returns>
    ''' <remarks></remarks>
    Public Function GetEmpID(ByVal CompID As String, ByVal EmpName As String) As DataTable
        Dim strSQL As New StringBuilder()
        strSQL.Append("Select EmpID")
        strSQL.Append(" From Personal With(NoLock)")
        strSQL.Append(" Where CompID =" & Bsp.Utility.Quote(CompID))
        strSQL.Append(" And NameN = " & Bsp.Utility.Quote(EmpName))
        Return Bsp.DB.ExecuteDataSet(CommandType.Text, strSQL.ToString, "eHRMSDB").Tables(0)
    End Function

End Class
