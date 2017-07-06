Imports Microsoft.VisualBasic

Partial Public Class POSqlCommand

    ''' <summary>
    ''' SelectPunchLogSql
    ''' 打卡查詢--經管單位
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectPunchLogSql(ByVal dataBean As PunchConfirmBean, ByVal searchOrgType As String, ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" CompID, EmpID, EmpName, CONVERT(char(10),PunchDate,120) AS PunchSDate, CONVERT(char(5),PunchTime) AS PunchSTime, PunchSeq, DeptID, DeptName, OrganID, OrganName,FlowOrganID, FlowOrganName, ")
        sb.Append(" AbnormalFlag AS Remedy_AbnormalFlag, AbnormalReasonCN AS Remedy_AbnormalReasonCN, ")
        sb.Append(" LastChgComp, LastChgID, LastChgDate ")
        sb.Append(" FROM PunchLog ")
        sb.Append(" WHERE 0 = 0 ")
        sb.Append(" AND CompID=@CompID ")
        Select Case searchOrgType
            Case "Organ"
                sb.Append(" AND OrganID IN ('" & dataBean.OrganID & "') ")
            Case "FlowOrgan"
                sb.Append(" AND FlowOrganID IN ('" & dataBean.FlowOrganID & "') ")
        End Select
        If Not String.IsNullOrEmpty(dataBean.PunchSDate) Then
            sb.Append(" AND PunchDate >= @PunchSDate ")
        End If
        If Not String.IsNullOrEmpty(dataBean.PunchEDate) Then
            sb.Append(" AND PunchDate >= @PunchEDate ")
        End If
        If Not String.IsNullOrEmpty(dataBean.PunchSTime.Replace(":", "")) Then
            sb.Append(" AND PunchTime >= @PunchSTime ")
        End If
        If Not String.IsNullOrEmpty(dataBean.PunchETime.Replace(":", "")) Then
            sb.Append(" AND PunchTime <= @PunchETime ")
        End If
        If Not String.IsNullOrEmpty(dataBean.Remedy_AbnormalFlag) Then
            sb.Append(" AND AbnormalFlag = @Remedy_AbnormalFlag ")
        End If
        If Not String.IsNullOrEmpty(dataBean.EmpID) Then
            sb.Append(" AND EmpID = @EmpID ")
        End If
        If Not String.IsNullOrEmpty(dataBean.EmpName) Then
            sb.AppendFormat(" AND EmpName LIKE '%{0}%'", dataBean.EmpName)
        End If
        sb.Append(" ; ")
    End Sub
End Class
