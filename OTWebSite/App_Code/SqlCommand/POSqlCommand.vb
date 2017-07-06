Imports Microsoft.VisualBasic

Partial Public Class POSqlCommand
    Private Shared Property _eHRMSDB As String
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
    ''' <summary>
    ''' SelectOrganSql
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="isReset"></param>
    ''' <remarks></remarks>
    Public Shared Sub SelectOrganSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.AppendLine("SELECT O.OrgType, O.OrgType + '-' + O2.OrganName AS OrgTypeName")
        sb.AppendLine(", O.DeptID, O.DeptID + '-' + O3.OrganName AS DeptName")
        sb.AppendLine(", O.OrganID, O.OrganID + '-' + O.OrganName AS OrganName")
        sb.AppendLine("FROM Organization O")
        sb.AppendLine("LEFT JOIN Organization O2 ON O.CompID = O2.CompID AND O.OrgType = O2.OrganID")
        sb.AppendLine("LEFT JOIN Organization O3 ON O.CompID = O3.CompID AND O.DeptID = O3.OrganID")
        sb.AppendLine("WHERE O.CompID = @CompID")
        sb.Append(" ; ")
    End Sub

    ''' <summary>
    ''' SelectFlowOrganSql
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="isReset"></param>
    ''' <remarks></remarks>
    Public Shared Sub SelectFlowOrganSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.AppendLine("SELECT BusinessType, RoleCode, UpOrganID, DeptID, OrganID")
        sb.AppendLine(", OrganLevel = CASE RoleCode WHEN '10' THEN OrganID WHEN '0' THEN UpOrganID ELSE OrganID END")
        sb.AppendLine(", OrganName = CASE WHEN RoleCode = '0' THEN '└─' + OrganID + '-' + OrganName ELSE OrganID + '-' + OrganName END")
        sb.AppendLine("FROM OrganizationFlow O")
        sb.AppendLine("WHERE CompID = @CompID")
        sb.AppendLine("ORDER BY BusinessType, OrganLevel, RoleCode DESC, OrganID")
        sb.Append(" ; ")
    End Sub
End Class
