using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Text;

/// <summary>
/// TemplateSql 的摘要描述
/// </summary>
public partial class SqlCommand
{
    /// <summary>
    /// SelectPunchConfirmSql
    /// 打卡查詢--個人
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchConfirmSql(PunchConfirmBean model,string searchType,ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" AbnormalType,PunchDate AS PunchSDate,CONVERT(char(5),PunchTime) AS PunchTime,ConfirmPunchFlag,Source,Remedy_AbnormalReasonCN,Remedy_AbnormalDesc ");
        sb.Append(" FROM PunchConfirm ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        if (!String.IsNullOrEmpty(model.PunchSDate))
        {
            sb.Append(" AND PunchDate >= @PunchSDate ");
        }
        if (!String.IsNullOrEmpty(model.PunchEDate))
        {
            sb.Append(" AND PunchDate <= @PunchEDate ");
        }
        if (!String.IsNullOrEmpty(searchType))
        {
            switch (searchType)
            {
                case "1": { sb.Append(" AND AbnormalType = '4' "); break; }
                case "2": { sb.Append(" AND PunchFlag = '0' "); break; }
                case "3": { sb.Append(" AND AbnormalType IN ('5','6') "); break; }
                case "4": { sb.Append(" AND Source = 'C' "); break; }
            }
        }
        if (!String.IsNullOrEmpty(model.ConfirmPunchFlag))
        {
            sb.Append(" AND ConfirmPunchFlag = @ConfirmPunchFlag ");
        }
        if (!String.IsNullOrEmpty(model.ConfirmStatus))
        {
            sb.Append(" AND ConfirmStatus = @ConfirmStatus ");
        }
        if (!String.IsNullOrEmpty(model.Remedy_AbnormalFlag)) 
        {
            sb.Append(" AND Remedy_AbnormalFlag = @Remedy_AbnormalFlag ");
        }
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectPunchConfirmForAllSql
    /// 打卡查詢--單位
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchConfirmForAllSql(PunchConfirmBean model,string orgType, string searchType, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" AbnormalType,PunchDate AS PunchSDate,CONVERT(char(5),PunchTime) AS PunchTime,ConfirmPunchFlag,Source,DeptName,OrganName,EmpID,EmpName AS EmpNameN,Remedy_AbnormalReasonCN,RotateFlag ");
        sb.Append(" FROM PunchConfirm ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        switch (orgType)
        {
            case "Organ":
                {
                    sb.AppendFormat(" AND OrganID IN ('{0}') ",model.OrganID);
                    break;
                }
            case "FlowOrgan":
                {
                    sb.AppendFormat(" AND FlowOrganID IN ('{0}') ",model.FlowOrganID);
                    break;
                }
        }
        if (!String.IsNullOrEmpty(model.PunchSDate))
        {
            sb.Append(" AND PunchDate >= @PunchSDate ");
        }
        if (!String.IsNullOrEmpty(model.PunchEDate))
        {
            sb.Append(" AND PunchDate <= @PunchEDate ");
        }
        if (!String.IsNullOrEmpty(model.PunchSTime.Replace(":","")))
        {
            sb.Append(" AND PunchTime >= @PunchSTime ");
        }
        if (!String.IsNullOrEmpty(model.PunchETime.Replace(":", "")))
        {
            sb.Append(" AND PunchTime <= @PunchETime ");
        }
        if (!String.IsNullOrEmpty(searchType))
        {
            switch (searchType)
            {
                case "1": { sb.Append(" AND AbnormalType = '4' "); break; }
                case "2": { sb.Append(" AND PunchFlag = '0' "); break; }
                case "3": { sb.Append(" AND AbnormalType IN ('5','6') "); break; }
                case "4": { sb.Append(" AND Source = 'C' "); break; }
            }
        }
        if (!String.IsNullOrEmpty(model.ConfirmPunchFlag))
        {
            sb.Append(" AND ConfirmPunchFlag = @ConfirmPunchFlag ");
        }
        if (!String.IsNullOrEmpty(model.ConfirmStatus))
        {
            sb.Append(" AND ConfirmStatus = @ConfirmStatus ");
        }
        if (!String.IsNullOrEmpty(model.Remedy_AbnormalFlag))
        {
            sb.Append(" AND Remedy_AbnormalFlag = @Remedy_AbnormalFlag ");
        }
        if (!String.IsNullOrEmpty(model.EmpID))
        {
            sb.Append(" AND EmpID=@EmpID ");
        }
        if (!String.IsNullOrEmpty(model.EmpNameN))
        {
            sb.AppendFormat(" AND EmpName LIKE '%{0}%' " , model.EmpNameN);
        }
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectOrganForAllSql
    /// 打卡查詢--單位
    /// 行政組織-所有人
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectOrganForAllSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine("SELECT O.OrgType, O.OrgType + '-' + O2.OrganName AS OrgTypeName");
        sb.AppendLine(", O.DeptID, O.DeptID + '-' + O3.OrganName AS DeptName");
        sb.AppendLine(", O.OrganID, O.OrganID + '-' + O.OrganName AS OrganName");
        sb.AppendLine("FROM Organization O");
        sb.AppendLine("LEFT JOIN Organization O2 ON O.CompID = O2.CompID AND O.OrgType = O2.OrganID");
        sb.AppendLine("LEFT JOIN Organization O3 ON O.CompID = O3.CompID AND O.DeptID = O3.OrganID");
        sb.AppendLine("WHERE O.CompID = @CompID");
    }

    /// <summary>
    /// SelectFlowOrganForAllSql
    /// 打卡查詢--單位
    /// 功能組織-所有人
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectFlowOrganForAllSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine("SELECT BusinessType, RoleCode, UpOrganID, DeptID, OrganID");
        sb.AppendLine(", OrganLevel = CASE RoleCode WHEN '10' THEN OrganID WHEN '0' THEN UpOrganID ELSE OrganID END");
        sb.AppendLine(", OrganName = CASE WHEN RoleCode = '0' THEN '└─' + OrganID + '-' + OrganName ELSE OrganID + '-' + OrganName END");
        sb.AppendLine("FROM OrganizationFlow O");
        sb.AppendLine("WHERE CompID = @CompID");
        sb.AppendLine("ORDER BY BusinessType, OrganLevel, RoleCode DESC, OrganID");
    }
}