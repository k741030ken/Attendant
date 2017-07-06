Imports Microsoft.VisualBasic

Partial Public Class POSqlCommand

    ''' <summary>
    ''' SelectSameKeySql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectSameKeySql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" CompID, DeptID, OrganID ")
        sb.Append(" FROM PunchSpecialUnitDefine ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        sb.Append(" AND DeptID=@DeptID ")
        sb.Append(" AND OrganID=@OrganID ")
        sb.Append(" ; ")
    End Sub
    ''' <summary>
    ''' SelectPunchSpecialUnitDefineSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectPunchSpecialUnitDefineSql(ByVal dataBean As PunchSpecialUnitDefineBean, ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" PO.CompID, PO.CompName, PO.DeptID, PO.DeptName, PO.OrganID, PO.OrganName, PO.SpecialFlag, PO.LastChgComp, PO.LastChgID, PO.LastChgDate, ")
        sb.Append(" PL.NameN AS LastChgName, CP.CompName AS LastChgCompName ")
        sb.Append(" FROM PunchSpecialUnitDefine PO ")
        sb.Append(" LEFT JOIN " & _eHRMSDB & ".dbo.Personal AS PL")
        sb.Append(" ON PO.LastChgComp = PL.CompID AND PO.LastChgID = PL.EmpID ")
        sb.Append(" LEFT JOIN " & _eHRMSDB & ".dbo.Company AS CP")
        sb.Append(" ON PO.LastChgComp = CP.CompID ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND PO.CompID=@CompID ")
        If Not String.IsNullOrEmpty(dataBean.DeptID) Then
            sb.Append(" AND PO.DeptID=@DeptID ")
        End If
        If Not String.IsNullOrEmpty(dataBean.OrganID) Then
            sb.Append(" AND PO.OrganID=@OrganID ")
        End If
        sb.Append(" ; ")
    End Sub

    ''' <summary>
    ''' InsertPunchSpecialUnitDefineSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub InsertPunchSpecialUnitDefineSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" INSERT INTO PunchSpecialUnitDefine ( ")
        sb.Append(" CompID, CompName, DeptID, DeptName, OrganID, OrganName, SpecialFlag, LastChgComp, LastChgID, LastChgDate ")
        sb.Append(" ) VALUES ( ")
        sb.Append(" @CompID, @CompName, @DeptID, @DeptName, @OrganID, @OrganName, @SpecialFlag, @LastChgComp, @LastChgID, getDate() ")
        sb.Append(" ); ")
    End Sub
    ''' <summary>
    ''' UpdatePunchSpecialUnitDefineSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub UpdatePunchSpecialUnitDefineSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" UPDATE PunchSpecialUnitDefine SET ")
        sb.Append(" DeptID=@DeptID , ")
        sb.Append(" DeptName=@DeptName , ")
        sb.Append(" OrganID=@OrganID , ")
        sb.Append(" OrganName=@OrganName , ")
        sb.Append(" SpecialFlag=@SpecialFlag , ")
        sb.Append(" LastChgComp=@LastChgComp , ")
        sb.Append(" LastChgID=@LastChgID , ")
        sb.Append(" LastChgDate=getDate() ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID_Old ")
        sb.Append(" AND DeptID=@DeptID_Old ")
        sb.Append(" AND OrganID=@OrganID_Old ")
        sb.Append(" ; ")
    End Sub
    ''' <summary>
    ''' DeletePunchSpecialUnitDefineSql
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub DeletePunchSpecialUnitDefineSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" DELETE ")
        sb.Append(" FROM PunchSpecialUnitDefine ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        sb.Append(" AND DeptID=@DeptID ")
        sb.Append(" AND OrganID=@OrganID ")
        sb.Append(" ; ")
    End Sub
End Class
