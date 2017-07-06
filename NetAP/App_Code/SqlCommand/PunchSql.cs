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
    /// SelectDutySql
    /// 值勤班表
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectDutySql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" WTBeginTime AS BeginTime ,WTEndTime AS EndTime ");
        sb.Append(" FROM EmpGuardWorkTime ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" AND DutyDate = @PunchDate ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectEmpWorkSql
    /// 個人班表
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectEmpWorkSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" WT.BeginTime,WT.EndTime,WT.RestBeginTime,WT.RestEndTime ");
        sb.Append(" FROM EmpWorkTimeLog EL ");
        sb.Append(" LEFT JOIN eHRMSDB_ITRD.dbo.WorkTime WT ON EL.WTCompID = WT.CompID AND EL.WTID = WT.WTID ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND EL.CompID=@CompID ");
        sb.Append(" AND EL.EmpID=@EmpID ");
        sb.Append(" AND EL.StartDate <= getdate() ");
        sb.Append(" AND EL.LastChgDate = (SELECT TOP 1 LastChgDate FROM EmpWorkTimeLog WHERE EmpID = @EmpID ORDER BY LastChgDate DESC) ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectPersonalOtherSql
    /// 公司班表
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPersonalOtherSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" WT.BeginTime,WT.EndTime,WT.RestBeginTime,WT.RestEndTime ");
        sb.Append(" FROM PersonalOther PO ");
        sb.Append(" LEFT JOIN WorkTime WT ON WT.CompID = PO.CompID AND WT.WTID = PO.WTID ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND PO.CompID=@CompID ");
        sb.Append(" AND PO.EmpID=@EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectPunchParaSql
    /// 打卡參數規定異常時間、提醒訊息
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchParaSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" Para,MsgPara ");
        sb.Append(" FROM PunchPara ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectPunchLogMaxSeqSql
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchLogMaxSeqSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" TOP 1 PunchSeq ");
        sb.Append(" FROM PunchLog ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" AND PunchDate=@PunchDate ");
        sb.Append(" ORDER BY PunchSeq DESC");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectOnBizReqAddesSql
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchLogSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" CompID, EmpID, PunchDate, PunchSeq, LastChgComp, LastChgID, LastChgDate ");
        sb.Append(" FROM PunchLog ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" AND PunchDate=@PunchDate ");
        sb.Append(" AND PunchSeq=@PunchSeq ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// InsertPunchLogSql
    /// 新增資料至打卡紀錄檔
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void InsertPunchLogSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" INSERT INTO PunchLog ");
        sb.Append(" (CompID,EmpID,EmpName,PunchDate,PunchTime,PunchSeq,DeptID,DeptName,OrganID,OrganName,FlowOrganID,FlowOrganName,Sex, ");
        sb.Append(" PunchFlag,WorkTypeID,WorkType,MAFT10_FLAG,AbnormalFlag,AbnormalReasonID,AbnormalReasonCN,AbnormalDesc, ");
        sb.Append(" BatchFlag,Source,PunchUserIP,RotateFlag,APPContent,Lat,Lon,GPSType,OS,DeviceID,LastChgComp,LastChgID,LastChgDate)  ");
        sb.Append(" VALUES ");
        sb.Append(" (@CompID,@EmpID,@EmpName,@PunchDate,@PunchTime,@PunchSeq,@DeptID,@DeptName,@OrganID,@OrganName,@FlowOrganID,@FlowOrganName,@Sex, ");
        sb.Append(" @PunchFlag,@WorkTypeID,@WorkType,@MAFT10_FLAG,@AbnormalFlag,@AbnormalReasonID,@AbnormalReasonCN,@AbnormalDesc, ");
        sb.Append(" @BatchFlag,@Source,@PunchUserIP,@RotateFlag,@APPContent,@Lat,@Lon,@GPSType,@OS,@DeviceID,@LastChgComp,@LastChgID,getDate()) ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// 特殊單位
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPunchSpecialUnitDefine(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" SpecialFlag ");
        sb.Append(" FROM PunchSpecialUnitDefine ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND DeptID=@DeptID ");
        sb.Append(" AND OrganID=@OrganID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectEmpOrganSql
    /// 取得個人FlowOrganID
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectEmpFlowOrganSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine(" SELECT ");
        sb.AppendLine(" EF.OrganID AS FlowOrganID, OG.OrganName AS FlowOrganName ");
        sb.AppendLine(" FROM EmpFlow EF ");
        sb.AppendLine(" LEFT JOIN OrganizationFlow OG ON EF.OrganID=OG.OrganID ");
        sb.AppendLine(" WHERE 0=0 ");
        sb.AppendLine(" AND EF.CompID=@CompID ");
        sb.AppendLine(" AND EF.EmpID=@EmpID ");
        sb.AppendLine(" ;");
    }
}