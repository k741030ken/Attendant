Imports Microsoft.VisualBasic

Partial Public Class POSqlCommand

    ''' <summary>
    ''' SeleteTemplateSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub SeleteTemplateSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" CompID, EmpID, LastChgComp, LastChgID, LastChgDate ")
        sb.Append(" FROM EmpFlowSN ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        sb.Append(" AND EmpID=@EmpID ")
        sb.Append(" ; ")
    End Sub
    ''' <summary>
    ''' InsertTemplateSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub InsertTemplateSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" INSERT INTO EmpFlowSN ( ")
        sb.Append(" CompID, EmpID, LastChgComp, LastChgID, LastChgDate ")
        sb.Append(" ) VALUES ( ")
        sb.Append(" @CompID, @EmpID, @LastChgComp, @LastChgID, @LastChgDate ")
        sb.Append(" ); ")
    End Sub
    ''' <summary>
    ''' UpdateTemplateSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdateTemplateSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" UPDATE EmpFlowSN SET ")
        sb.Append(" LastChgComp=@LastChgComp ")
        sb.Append(" , LastChgID=@LastChgID ")
        sb.Append(" , LastChgDate=@LastChgDate ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        sb.Append(" AND EmpID=@EmpID ")
        sb.Append(" ; ")
    End Sub
    ''' <summary>
    ''' DeleteTemplateSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub DeleteTemplateSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" DELETE ")
        sb.Append(" FROM EmpFlowSN ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        sb.Append(" AND EmpID=@EmpID ")
        sb.Append(" ; ")
    End Sub
    ''' <summary>
    ''' GetEmpFlowSNEmpAndSexSql
    ''' </summary>
    ''' <param name="dataBean">TemplateBean</param>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub GetEmpFlowSNEmpAndSexSql(ByVal dataBean As TemplateBean, ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" EFSN.CompID, EFSN.EmpID ")
        sb.Append(" ,P.NameN, P.Sex ")
        sb.Append(" FROM EmpFlowSN AS EFSN ")
        sb.Append(" LEFT JOIN " & _eHRMSDB & ".dbo.Personal AS P")
        sb.Append(" ON EFSN.CompID = P.CompID AND EFSN.EmpID = P.EmpID ")
        sb.Append(" WHERE 0 = 0 ")
        If Not String.IsNullOrEmpty(dataBean.CompID) Then
            sb.Append(" AND EFSN.CompID=@CompID ")
        End If
        If Not String.IsNullOrEmpty(dataBean.EmpID) Then
            sb.Append(" AND EFSN.EmpID=@EmpID ")
        End If
        If Not String.IsNullOrEmpty(dataBean.NameN) Then
            sb.Append(" AND P.NameN=@NameN ")
        End If
        If Not String.IsNullOrEmpty(dataBean.Sex) Then
            sb.Append(" AND P.Sex =@Sex ")
        End If
        sb.Append(" ; ")
    End Sub
End Class
