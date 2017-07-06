Imports Microsoft.VisualBasic

Partial Public Class POSqlCommand

    ''' <summary>
    ''' SelectPunchConfirmSql
    ''' 打卡異常控管維護、打卡查詢--經管單位
    ''' </summary>
    ''' <param name="sb">StringBuilder</param>
    ''' <param name="isReset">Boolean</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectPunchConfirmSql(ByVal dataBean As PunchConfirmBean, ByVal searchOrgType As String, ByVal searchType As String, ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.Append(" SELECT ")
        sb.Append(" CompID, EmpID, EmpName, CONVERT(char(10),PunchDate,120) AS PunchSDate, CONVERT(char(5),PunchTime) AS PunchSTime,  CONVERT(char(10),DutyDate) AS DutyDate, PunchConfirmSeq, DeptID, DeptName, OrganID, OrganName,FlowOrganID, FlowOrganName, ")
        sb.Append(" ConfirmStatus, AbnormalType, ConfirmPunchFlag, AbnormalFlag, AbnormalReasonCN, Remedy_AbnormalFlag, Remedy_AbnormalReasonCN, ")
        sb.Append(" LastChgComp, LastChgID,LastChgDate ")
        sb.Append(" FROM PunchConfirm ")
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
        If Not String.IsNullOrEmpty(searchType) Then
            Select Case searchType
                Case "1"
                    sb.Append(" AND AbnormalType = '4' ")
                Case "2"
                    sb.Append(" AND PunchFlag = '0' ")
                Case "3"
                    sb.Append(" AND AbnormalType IN ('5','6') ")
                Case "4"
                    sb.Append(" AND Source = 'C' ")
            End Select
        End If
        If Not String.IsNullOrEmpty(dataBean.ConfirmPunchFlag) Then
            sb.Append(" AND ConfirmPunchFlag = @ConfirmPunchFlag ")
        End If
        If Not String.IsNullOrEmpty(dataBean.ConfirmStatus) Then
            sb.Append(" AND ConfirmStatus = @ConfirmStatus ")
        End If
        If Not String.IsNullOrEmpty(dataBean.Remedy_AbnormalFlag) Then
            sb.Append(" AND Remedy_AbnormalFlag = @Remedy_AbnormalFlag ")
        End If
        If Not String.IsNullOrEmpty(dataBean.EmpID) Then
            sb.Append(" AND EmpID = @EmpID ")
        End If
        If Not String.IsNullOrEmpty(dataBean.EmpName) Then
            sb.AppendFormat(" AND EmpName LIKE '%{0}%'", dataBean.EmpName)
        End If
        sb.Append(" ; ")
    End Sub

    ''' <summary>
    ''' UpdatePunchConfirmSql
    ''' 打卡異常控管維護
    ''' </summary>
    ''' <param name="sb"></param>
    ''' <param name="isReset"></param>
    ''' <remarks></remarks>
    Public Shared Sub UpdatePunchConfirmSql(ByRef sb As StringBuilder, Optional isReset As Boolean = False)
        If isReset Then
            sb = New StringBuilder()
        End If
        sb.AppendLine(" UPDATE PunchConfirm ")
        sb.AppendLine(" SET ConfirmStatus = '3'")
        sb.AppendLine(" WHERE 0=0 ")
        sb.AppendLine(" AND CompID = @CompID ")
        sb.AppendLine(" AND EmpID = @EmpID ")
        sb.AppendLine(" AND DutyDate = @DutyDate ")
        sb.AppendLine(" AND PunchConfirmSeq = @PunchConfirmSeq ")
        sb.Append(" ; ")
    End Sub

End Class
