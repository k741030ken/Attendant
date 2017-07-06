using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

/// <summary>
/// PunchUpdate 的摘要描述
/// </summary>
public partial class SqlCommand
{


    public static void Select_AT_CodeMap(AT_CodeMap_Bean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();
        sb.AppendLine(" select TabName , FldName , Code , CodeCName , SortFld , NotShowFlag , LastChgComp , LastChgID , LastChgDate ");
        sb.AppendLine(" from AT_CodeMap ");
        sb.AppendLine(" where 1=1 ");
        if (!string.IsNullOrEmpty(Bean.TabName))
            sb.AppendLine(" and TabName=").AppendFormat("@TabName", Bean.TabName);
        if (!string.IsNullOrEmpty(Bean.FldName))
            sb.AppendLine(" and FldName=").AppendFormat("@FldName", Bean.FldName);
    }

    public static void Select_HROtherFlowLog(HROtherFlowLog_Bean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();
        sb.AppendLine(" SELECT Top 1 ");
        sb.AppendLine(" FlowCaseID,FlowLogBatNo,FlowLogID,Seq,Mode,ApplyID,EmpID,EmpOrganID,EmpFlowOrganID, ");
        sb.AppendLine(" SignOrganID,SignFlowOrganID,FlowCode,FlowSN,FlowSeq,SignLine,SignIDComp,SignID,SignTime,FlowStatus, ");
        sb.AppendLine(" ReAssignFlag,ReAssignComp,ReAssignEmpID,Remark,LastChgComp,LastChgID,LastChgDate ");
        sb.AppendLine(" from HROtherFlowLog ");
        sb.AppendLine(" where 1=1 ");
        if (!string.IsNullOrEmpty(Bean.FlowCaseID))
            sb.AppendLine(" and FlowCaseID=").AppendFormat("@HROtherFlowLog_FlowCaseID", Bean.FlowCaseID);
        if (!string.IsNullOrEmpty(Bean.ApplyID))
            sb.AppendLine(" and ApplyID=").AppendFormat("@HROtherFlowLog_ApplyID", Bean.ApplyID);
        if (!string.IsNullOrEmpty(Bean.EmpID))
            sb.AppendLine(" and EmpID=").AppendFormat("@HROtherFlowLog_EmpID", Bean.EmpID);
        if (!string.IsNullOrEmpty(Bean.SignID))
            sb.AppendLine(" and SignID=").AppendFormat("@HROtherFlowLog_SignID", Bean.SignID);
        sb.AppendLine(" order by FlowCaseID desc ,FlowLogID desc ");
    }

    public static void Select_Punch_Confirm_Remedy_OpenLog(Punch_Confirm_Remedy_Bean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();

        sb.AppendLine(" SELECT ");
        //PunchConfirm
        sb.AppendLine(" PC.CompID ,PC.EmpID ,PC.EmpName ,CONVERT(VARCHAR, PC.DutyDate,111) as DutyDate ,PC.DutyTime ,CONVERT(VARCHAR, PC.PunchDate,111) as PunchDate ,CONVERT(CHAR(5),PC.PunchTime) as PunchTime ,PC.PunchConfirmSeq ,PC.DeptID ,PC.DeptName ,PC.OrganID ,PC.OrganName ,PC.FlowOrganID ,PC.FlowOrganName ,");

        sb.AppendLine(" PC.PunchFlag ,PC.WorkTypeID ,PC.WorkType ,PC.MAFT10_FLAG ,");
        sb.AppendLine(" PC.AbnormalType ,");
        sb.AppendLine(" PC.AbnormalReasonCN ,");
        sb.AppendLine(" PC.PunchSeq ,PC.PunchRemedySeq ,PR.RemedyReasonID ,PR.RemedyReasonCN ,CONVERT(CHAR(5),PR.RemedyPunchTime) as RemedyPunchTime,PC.AbnormalFlag ,PC.AbnormalReasonID ,");
        sb.AppendLine(" PC.AbnormalDesc ,PR.Remedy_AbnormalFlag ,PR.Remedy_AbnormalReasonID ,PR.Remedy_AbnormalReasonCN ,PR.Remedy_AbnormalDesc ,");
        sb.AppendLine(" PC.APPContent ,PC.LastChgComp ,PC.LastChgID ,PC.LastChgDate ,");

        //性別-gridview用
        sb.AppendLine(" PC.Sex , case PC.Sex  when '1' then '男' when '2' then '女' else '不明' end as SexGCN ,");
        //狀態-gridview用
        sb.AppendLine(" PC.ConfirmStatus ,case PC.ConfirmStatus when'0' then '正常' when'1'then ('異常'+AbnormalType) when '2' then '送簽中' when '3' then '異常不控管' else '資料錯誤' end as ConfirmStatusGCN ,");
        //類型-gridview用
        sb.AppendLine(" PC.ConfirmPunchFlag ,case PC.ConfirmPunchFlag when '1' then '上班' when '2' then '下班' when '3' then '午休開始' when '4' then '午休結束'  end as ConfirmPunchFlagGCN,");
        //原因-gridview用
        sb.AppendLine(" case PC.AbnormalFlag when '0' then '無異常' when '1' then '公務' when '2' then '非公務-'+PC.AbnormalReasonCN else '資料錯誤' end as AbnormalReasonGCN ,");
        //來源-gridview用
        sb.AppendLine(" PC.Source ,case PC.Source when 'A' then 'APP' when 'B' then '永豐雲' else '資料錯誤' end as SourceGCN ,");
        //特殊人員註記,午休開始時間,午休結束時間-gridview用
        sb.AppendLine(" isnull(SpecialFlag,'0') as SpecialFlag , ISNULL(WT.RestBeginTime,'') as RestBeginTime , ISNULL(WT.RestEndTime,'') as RestEndTime ,");

        //PunchRemedyLog 因為是Join過來的，所以可能會有Null，要加上isnull
        sb.AppendLine(" isnull(PR.FlowCaseID,'') as FlowCaseID, isnull(PR.RemedyPunchFlag,'') as RemedyPunchFlag, isnull(PR.BatchFlag ,'') as BatchFlag, isnull(PR.PORemedyStatus ,'') as PORemedyStatus , isnull(PR.RejectReason ,'') as RejectReason , isnull(PR.RejectReasonCN ,'') as RejectReasonCN , isnull(PR.ValidDateTime,'') as ValidDateTime , isnull(PR.ValidCompID ,'') as ValidCompID , isnull(PR.ValidID ,'') as ValidID , isnull(PR.ValidName  ,'') as ValidName , isnull(PR.Remedy_MAFT10_FLAG,'') as Remedy_MAFT10_FLAG ");

        sb.AppendLine(" FROM PunchConfirm PC ");
        sb.AppendLine(" left join PunchRemedyLog PR ");
        sb.AppendLine(" on PC.CompID=PR.CompID and PC.EmpID=PR.EmpID and PC.DutyDate=PR.DutyDate and PC.PunchConfirmSeq=PR.PunchConfirmSeq and PC.PunchRemedySeq=PR.PunchRemedySeq ");
        sb.AppendLine(" left join dbo.PunchUpdate_ITRDFlowOpenLog OL ");
        sb.AppendLine(" on OL.FlowCaseID=PR.FlowCaseID ");
        sb.AppendLine(" left join dbo.PunchSpecialUnitDefine PS  ");
        sb.AppendLine(" on PS.CompID=PC.CompID and PS.OrganID=PC.OrganID and PS.DeptID=PC.DeptID and PS.SpecialFlag='1' ");
        sb.AppendLine(" left join dbo.EmpWorkTime EWT  ");
        sb.AppendLine(" on EWT.CompID=PC.CompID and EWT.EmpID=PC.EmpID ");
        sb.AppendLine(" left join eHRMSDB_ITRD.dbo.WorkTime WT  ");
        sb.AppendLine(" on EWT.WTCompID=WT.CompID and EWT.WTID=WT.WTID ");
        sb.AppendLine(" Where PC.ConfirmStatus in ('2') ");
        sb.AppendLine(" and OL.AssignTo=").AppendFormat("@ValidID", Bean.ValidID);
    }

    public static void Select_Punch_Confirm_Remedy(Punch_Confirm_Remedy_Bean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();

        sb.AppendLine(" SELECT ");
        //PunchConfirm
        sb.AppendLine(" PC.CompID ,PC.EmpID ,PC.EmpName ,CONVERT(VARCHAR, PC.DutyDate,111) as DutyDate ,PC.DutyTime ,CONVERT(VARCHAR, PC.PunchDate,111) as PunchDate ,CONVERT(CHAR(5),PC.PunchTime) as PunchTime ,PC.PunchConfirmSeq ,PC.DeptID ,PC.DeptName ,PC.OrganID ,PC.OrganName ,PC.FlowOrganID ,PC.FlowOrganName ,PC.PunchFlag ,PC.WorkTypeID ,PC.WorkType ,PC.MAFT10_FLAG ,");
        //sb.AppendLine(" PC.CompID ,PC.EmpID ,PC.EmpName ,CONVERT(VARCHAR, PC.DutyDate,111) as DutyDate ,PC.DutyTime ,CONVERT(VARCHAR, PC.PunchDate,111) as PunchDate ,CONVERT(CHAR(5),PC.PunchTime) as PunchTime ,PC.PunchConfirmSeq ,PC.DeptID ,PC.DeptName ,PC.OrganID ,PC.OrganName ,PC.FlowOrganID ,PC.FlowOrganName ,PC.Sex ,PC.PunchFlag ,PC.WorkTypeID ,PC.WorkType ,PC.MAFT10_FLAG ,");
        sb.AppendLine(" PC.AbnormalType ,");
        sb.AppendLine(" PC.AbnormalReasonCN ,");
        sb.AppendLine(" PC.PunchSeq ,PC.PunchRemedySeq ,PC.RemedyReasonID ,PC.RemedyReasonCN ,PC.RemedyPunchTime ,PC.AbnormalFlag ,PC.AbnormalReasonID ,");
        sb.AppendLine(" PC.AbnormalDesc ,PC.Remedy_AbnormalFlag ,PC.Remedy_AbnormalReasonID ,PC.Remedy_AbnormalReasonCN ,PC.Remedy_AbnormalDesc ,");
        sb.AppendLine(" PC.APPContent ,PC.LastChgComp ,PC.LastChgID ,PC.LastChgDate ,");

        //狀態-gridview用
        sb.AppendLine(" PC.Sex , case PC.Sex  when '1' then '男' when '2' then '女' else '不明' end as SexGCN ,");
        //狀態-gridview用
        sb.AppendLine(" PC.ConfirmStatus ,case PC.ConfirmStatus when'0' then '正常' when'1'then ('異常'+AbnormalType) when '2' then '送簽中' when '3' then '異常不控管' else '資料錯誤' end as ConfirmStatusGCN ,");
        //類型-gridview用
        sb.AppendLine(" PC.ConfirmPunchFlag ,case PC.ConfirmPunchFlag when '1' then '上班' when '2' then '下班' when '3' then '午休開始' when '4' then '午休結束'  end as ConfirmPunchFlagGCN,");
        //原因-gridview用
        sb.AppendLine(" case PC.AbnormalFlag when '0' then '無異常' when '1' then '公務' when '2' then '非公務-'+PC.AbnormalReasonCN else '資料錯誤' end as AbnormalReasonGCN ,");
        //來源-gridview用
        sb.AppendLine(" PC.Source ,case PC.Source when 'A' then 'APP' when 'B' then '永豐雲' else '資料錯誤' end as SourceGCN ,");

        //PunchRemedyLog 因為是Join過來的，所以可能會有Null，要加上isnull
        sb.AppendLine(" isnull(FlowCaseID,'') as FlowCaseID, isnull(PR.RemedyPunchFlag,'') as RemedyPunchFlag, isnull(PR.BatchFlag ,'') as BatchFlag, isnull(PR.PORemedyStatus ,'') as PORemedyStatus , isnull(PR.RejectReason ,'') as RejectReason , isnull(PR.RejectReasonCN ,'') as RejectReasonCN , isnull(PR.ValidDateTime,'') as ValidDateTime , isnull(PR.ValidCompID ,'') as ValidCompID , isnull(PR.ValidID ,'') as ValidID , isnull(PR.ValidName  ,'') as ValidName , isnull(PR.Remedy_MAFT10_FLAG,'') as Remedy_MAFT10_FLAG ");
        //sb.AppendLine(" isnull(PR.RemedyPunchFlag,'') , isnull(PR.BatchFlag ,'') , isnull(PR.PORemedyStatus ,'') , isnull(PR.RejectReason ,'') , isnull(PR.RejectReasonCN ,'') , isnull(PR.ValidDateTime,'') ,  isnull(CONVERT(CHAR(5),PR.ValidTime),'') as ValidTime , isnull(PR.ValidCompID ,'') , isnull(PR.ValidID ,'') , isnull(PR.ValidName  ,'') , isnull(PR.Remedy_MAFT10_FLAG,'') ");

        sb.AppendLine(" FROM PunchConfirm PC ");
        sb.AppendLine(" left join PunchRemedyLog PR ");
        sb.AppendLine(" on PC.CompID=PR.CompID and PC.EmpID=PR.EmpID and PC.DutyDate=PR.DutyDate and PC.PunchConfirmSeq=PR.PunchConfirmSeq and PC.PunchRemedySeq=PR.PunchRemedySeq ");
        sb.AppendLine(" Where PC.ConfirmStatus in ('1','2') ");
        if (!string.IsNullOrEmpty(Bean.EmpID))
            sb.AppendLine(" and PC.EmpID=").AppendFormat("@EmpID", Bean.EmpID);
        if (!string.IsNullOrEmpty(Bean.ConfirmPunchFlag))
            sb.AppendLine(" and PC.ConfirmPunchFlag=").AppendFormat("@ConfirmPunchFlag", Bean.ConfirmPunchFlag);
        if (!string.IsNullOrEmpty(Bean.ConfirmStatus))
            sb.AppendLine(" and PC.ConfirmStatus=").AppendFormat("@ConfirmStatus", Bean.ConfirmStatus);
        if (!string.IsNullOrEmpty(Bean.AbnormalFlag))
            sb.AppendLine(" and PC.AbnormalFlag=").AppendFormat("@AbnormalFlag", Bean.AbnormalFlag);

        if (!string.IsNullOrEmpty(Bean.StartPunchDate))
            sb.AppendLine(" and PC.PunchDate>=").AppendFormat("@StartPunchDate", Bean.StartPunchDate);
        if (!string.IsNullOrEmpty(Bean.EndPunchDate))
            sb.AppendLine(" and PC.PunchDate<=").AppendFormat("@EndPunchDate", Bean.EndPunchDate);
        //PR
        if (!string.IsNullOrEmpty(Bean.ValidCompID))
            sb.AppendLine(" and PR.ValidCompID=").AppendFormat("@ValidCompID", Bean.ValidCompID);
        if (!string.IsNullOrEmpty(Bean.ValidID))
            sb.AppendLine(" and PR.ValidID=").AppendFormat("@ValidID", Bean.ValidID);
    }


    public static void SelectPunchConfirm(PunchRemedyLogBean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();
        sb.AppendLine("SELECT CompID , EmpID , EmpName , DutyDate , DutyTime , PunchDate , CONVERT(CHAR(5),PunchTime) as PunchTime , PunchConfirmSeq , DeptID , DeptName , OrganID , OrganName , FlowOrganID , FlowOrganName , Sex , PunchFlag , WorkTypeID , WorkType , MAFT10_FLAG , ConfirmStatus , AbnormalType , ConfirmPunchFlag , PunchSeq , PunchRemedySeq , RemedyReasonID , RemedyReasonCN , RemedyPunchTime , AbnormalFlag , AbnormalReasonID , AbnormalReasonCN , AbnormalDesc , Remedy_AbnormalFlag , Remedy_AbnormalReasonID , Remedy_AbnormalReasonCN , Remedy_AbnormalDesc , Source , APPContent , LastChgComp , LastChgID , LastChgDate   FROM PunchConfirm");
        sb.AppendLine(" Where 1=1 ");
        if (!string.IsNullOrEmpty(Bean.EmpID))
            sb.AppendLine(" and EmpID=").AppendFormat("@EmpID", Bean.EmpID);
        if (!string.IsNullOrEmpty(Bean.RemedyPunchFlag))
            sb.AppendLine(" and RemedyPunchFlag=").AppendFormat("@RemedyPunchFlag", Bean.RemedyPunchFlag);
        if (!string.IsNullOrEmpty(Bean.PORemedyStatus))
            sb.AppendLine(" and PORemedyStatus=").AppendFormat("@PORemedyStatus", Bean.PORemedyStatus);
        if (!string.IsNullOrEmpty(Bean.RemedyReasonID))
            sb.AppendLine(" and RemedyReasonID=").AppendFormat("@RemedyReasonID", Bean.RemedyReasonID);

        if (!string.IsNullOrEmpty(Bean.StartPunchDate))
            sb.AppendLine(" and PunchDate>=").AppendFormat("@StartPunchDate", Bean.StartPunchDate);
        if (!string.IsNullOrEmpty(Bean.EndPunchDate))
            sb.AppendLine(" and PunchDate<=").AppendFormat("@EndPunchDate", Bean.EndPunchDate);
    }

    public static void SelectPunchRemedyLog(PunchRemedyLogBean Bean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset) sb.Clear();
        sb.AppendLine("SELECT CompID , EmpID , EmpName , PunchDate , CONVERT(CHAR(5),PunchTime) as PunchTime , PunchRemedySeq , DeptID , DeptName , OrganID , OrganName , PunchConfirmSeq , RemedyPunchFlag , DutyDate , DutyTime , MAFT10_FLAG , AbnormalFlag , AbnormalReasonID , AbnormalReasonCN , AbnormalDesc , BatchFlag , PORemedyStatus , RejectReason , RejectReasonCN , ValidDateTime  , ValidCompID , ValidID , ValidName , RemedyReasonID , RemedyReasonCN , RemedyPunchTime , Remedy_MAFT10_FLAG , Remedy_AbnormalFlag , Remedy_AbnormalReasonID , Remedy_AbnormalReasonCN , Remedy_AbnormalDesc , LastChgComp , LastChgID , LastChgDate  FROM PunchRemedyLog");
        //sb.AppendLine("SELECT CompID , EmpID , EmpName , PunchDate , CONVERT(CHAR(5),PunchTime) as PunchTime , PunchRemedySeq , DeptID , DeptName , OrganID , OrganName , PunchConfirmSeq , RemedyPunchFlag , DutyDate , DutyTime , MAFT10_FLAG , AbnormalFlag , AbnormalReasonID , AbnormalReasonCN , AbnormalDesc , BatchFlag , PORemedyStatus , RejectReason , RejectReasonCN , ValidDateTime , CONVERT(CHAR(5),ValidTime) as ValidTime , ValidCompID , ValidID , ValidName , RemedyReasonID , RemedyReasonCN , RemedyPunchTime , Remedy_MAFT10_FLAG , Remedy_AbnormalFlag , Remedy_AbnormalReasonID , Remedy_AbnormalReasonCN , Remedy_AbnormalDesc , LastChgComp , LastChgID , LastChgDate  FROM PunchRemedyLog");
        sb.AppendLine(" Where 1=1 ");
        if (!string.IsNullOrEmpty(Bean.EmpID))
            sb.AppendLine(" and EmpID=").AppendFormat("@EmpID", Bean.EmpID);
        if (!string.IsNullOrEmpty(Bean.RemedyPunchFlag))
            sb.AppendLine(" and RemedyPunchFlag=").AppendFormat("@RemedyPunchFlag", Bean.RemedyPunchFlag);
        if (!string.IsNullOrEmpty(Bean.PORemedyStatus))
            sb.AppendLine(" and PORemedyStatus=").AppendFormat("@PORemedyStatus", Bean.PORemedyStatus);
        if (!string.IsNullOrEmpty(Bean.RemedyReasonID))
            sb.AppendLine(" and RemedyReasonID=").AppendFormat("@RemedyReasonID", Bean.RemedyReasonID);

        if (!string.IsNullOrEmpty(Bean.StartPunchDate))
            sb.AppendLine(" and PunchDate>=").AppendFormat("@StartPunchDate", Bean.StartPunchDate);
        if (!string.IsNullOrEmpty(Bean.EndPunchDate))
            sb.AppendLine(" and PunchDate<=").AppendFormat("@EndPunchDate", Bean.EndPunchDate);
    }

    /// <summary>
    /// InesrtPunchRemedyLogSend
    /// 申請 > 送簽
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void PunchUpdateModify_InsertPunchRemedyLog(Punch_Confirm_Remedy_Bean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" INSERT INTO PunchRemedyLog ( ");
        sb.Append(" CompID , EmpID , EmpName , PunchDate , PunchTime , PunchRemedySeq , FlowCaseID , DeptID , DeptName , OrganID , OrganName , PunchConfirmSeq , RemedyPunchFlag , DutyDate , DutyTime , MAFT10_FLAG , AbnormalFlag , AbnormalReasonID , AbnormalReasonCN , AbnormalDesc , BatchFlag , PORemedyStatus , RejectReason , RejectReasonCN , ValidDateTime , ValidCompID , ValidID , ValidName , RemedyReasonID , RemedyReasonCN , RemedyPunchTime , Remedy_MAFT10_FLAG , Remedy_AbnormalFlag , Remedy_AbnormalReasonID , Remedy_AbnormalReasonCN , Remedy_AbnormalDesc , LastChgComp , LastChgID , LastChgDate");//3
        //sb.Append(" CompID , EmpID , EmpName , PunchDate , PunchTime , PunchRemedySeq , FlowCaseID , DeptID , DeptName , OrganID , OrganName , PunchConfirmSeq , RemedyPunchFlag , DutyDate , DutyTime , MAFT10_FLAG , AbnormalFlag , AbnormalReasonID , AbnormalReasonCN , AbnormalDesc , BatchFlag , PORemedyStatus , RejectReason , RejectReasonCN , ValidDateTime , ValidTime , ValidCompID , ValidID , ValidName , RemedyReasonID , RemedyReasonCN , RemedyPunchTime , Remedy_MAFT10_FLAG , Remedy_AbnormalFlag , Remedy_AbnormalReasonID , Remedy_AbnormalReasonCN , Remedy_AbnormalDesc , LastChgComp , LastChgID , LastChgDate");//3
        sb.Append(" ) VALUES ( ");
        sb.Append("'" + model.CompID + "'").Append(",");
        sb.Append("'" + model.EmpID + "'").Append(",");
        sb.Append("'" + model.EmpName + "'").Append(",");
        sb.Append("'" + model.PunchDate + "'").Append(",");
        sb.Append("'" + model.PunchTime + "'").Append(",");
        sb.Append("'" + model.PunchRemedySeq + "'").Append(",");
        sb.Append("'" + model.FlowCaseID + "'").Append(",");
        sb.Append("'" + model.DeptID + "'").Append(",");
        sb.Append("'" + model.DeptName + "'").Append(",");
        sb.Append("'" + model.OrganID + "'").Append(",");
        sb.Append("'" + model.OrganName + "'").Append(",");
        sb.Append("'" + model.PunchConfirmSeq + "'").Append(",");
        sb.Append("'" + model.RemedyPunchFlag + "'").Append(","); //SET RemedyPunchFlag=實際是ConfirmPunchFlag的值
        sb.Append("'" + model.DutyDate + "'").Append(",");
        sb.Append("'" + model.DutyTime + "'").Append(",");
        sb.Append("'" + model.MAFT10_FLAG + "'").Append(",");
        sb.Append("'" + model.AbnormalFlag + "'").Append(",");
        sb.Append("'" + model.AbnormalReasonID + "'").Append(",");
        sb.Append("'" + model.AbnormalReasonCN + "'").Append(",");
        sb.Append("'" + model.AbnormalDesc + "'").Append(",");
        sb.Append("'" + model.BatchFlag + "'").Append(",");
        sb.Append("'" + model.PORemedyStatus + "'").Append(",");
        sb.Append("'" + model.RejectReason + "'").Append(",");
        sb.Append("'" + model.RejectReasonCN + "'").Append(",");
        sb.Append("'" + model.ValidDateTime + "'").Append(",");
        //sb.Append("'"+ model.ValidTime+"'").Append(",");
        sb.Append("'" + model.ValidCompID + "'").Append(",");
        sb.Append("'" + model.ValidID + "'").Append(",");
        sb.Append("'" + model.ValidName + "'").Append(",");
        sb.Append("'" + model.RemedyReasonID + "'").Append(",");
        sb.Append("'" + model.RemedyReasonCN + "'").Append(",");
        sb.Append("'" + model.RemedyPunchTime + "'").Append(",");
        sb.Append("'" + model.Remedy_MAFT10_FLAG + "'").Append(",");
        sb.Append("'" + model.Remedy_AbnormalFlag + "'").Append(",");
        sb.Append("'" + model.Remedy_AbnormalReasonID + "'").Append(",");
        sb.Append("'" + model.Remedy_AbnormalReasonCN + "'").Append(",");
        sb.Append("'" + model.Remedy_AbnormalDesc + "'").Append(",");
        sb.Append("'" + model.LastChgComp + "'").Append(",");
        sb.Append("'" + model.LastChgID + "'").Append(",");
        sb.Append("'" + model.LastChgDate + "'");
        sb.Append(" ); ");
        sb.AppendStatement(" update PunchConfirm SET ");
        sb.Append(" ConfirmStatus='2'").Append(",");
        sb.Append(" PunchRemedySeq=").AppendParameter("PunchRemedySeq", model.PunchRemedySeq).Append(",");
        sb.Append(" LastChgComp=").AppendParameter("LastChgComp", model.LastChgComp).Append(",");
        sb.Append(" LastChgID=").AppendParameter("LastChgID", model.LastChgID).Append(",");
        sb.Append(" LastChgDate=").AppendParameter("LastChgDate", model.LastChgDate);
        sb.Append(" where ");
        sb.Append(" CompID=").AppendParameter("CompID", model.CompID);
        sb.Append(" and EmpID=").AppendParameter("EmpID", model.EmpID);
        sb.Append(" and DutyDate=").AppendParameter("DutyDate", model.DutyDate);
        sb.Append(" and PunchConfirmSeq=").AppendParameter("PunchConfirmSeq", model.PunchConfirmSeq);
    }

    /// <summary>
    /// SelectPersonValid
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPersonValid(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" EmpID AS ValidID ,NameN AS ValidName ");
        sb.Append(" From " + _eHRMSDB_ITRD + ".dbo.Personal ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" ; ");
    }

    #region"PunchAppdOperation"
    public static void PunchAppdOperation_UpdatePunchRemedyLog(Punch_Confirm_Remedy_Bean model, ref CommandHelper sb, bool isReset = false)
    {
        string UpdatePunchRemedyLog_ = "UpdatePunchRemedyLog_";
        if (isReset)
        {
            sb.Reset();
        }
        sb.AppendStatement(" Update PunchRemedyLog set ");

        //下一關審核主管
        sb.Append(" ValidDateTime=").AppendParameter(UpdatePunchRemedyLog_ + "ValidDateTime", model.ValidDateTime).Append(",");
        sb.Append(" ValidCompID=").AppendParameter(UpdatePunchRemedyLog_ + "ValidCompID", model.ValidCompID).Append(",");
        sb.Append(" ValidID=").AppendParameter(UpdatePunchRemedyLog_ + "ValidID", model.ValidID).Append(",");
        sb.Append(" ValidName=").AppendParameter(UpdatePunchRemedyLog_ + "ValidName", model.ValidName).Append(",");
        sb.Append(" PORemedyStatus=").AppendParameter(UpdatePunchRemedyLog_ + "PORemedyStatus", model.PORemedyStatus).Append(",");

        //LastChange
        sb.Append(" LastChgComp=").AppendParameter(UpdatePunchRemedyLog_ + "LastChgComp", model.LastChgComp).Append(",");
        sb.Append(" LastChgID=").AppendParameter(UpdatePunchRemedyLog_ + "LastChgID", model.LastChgID).Append(",");
        sb.Append(" LastChgDate=").AppendParameter(UpdatePunchRemedyLog_ + "LastChgDate", model.LastChgDate);
        //Where
        sb.Append(" where ");
        sb.Append(" CompID=").AppendParameter(UpdatePunchRemedyLog_ + "CompID", model.CompID);
        sb.Append(" and EmpID=").AppendParameter(UpdatePunchRemedyLog_ + "EmpID", model.EmpID);
        sb.Append(" and PunchDate=").AppendParameter(UpdatePunchRemedyLog_ + "PunchDate", model.PunchDate);
        sb.Append(" and PunchRemedySeq=").AppendParameter(UpdatePunchRemedyLog_ + "PunchRemedySeq", model.PunchRemedySeq);
    }

    public static void PunchAppdOperation_UpdatePunchConfirm(Punch_Confirm_Remedy_Bean model, ref CommandHelper sb, bool isReset = false)
    {
        string UpdatePunchConfirm_ = "UpdatePunchConfirm_";
        if (isReset)
        {
            sb.Reset();
        }
        if (model.ConfirmStatus == "3" || model.ConfirmStatus == "4")
        {
            sb.AppendStatement(" update PunchConfirm SET ");
            //狀態
            sb.Append(" ConfirmStatus=").AppendParameter(UpdatePunchConfirm_ + "ConfirmStatus", model.ConfirmStatus).Append(",");
            //Remedy(無修改前欄位，此為原生欄位)
            if (model.ConfirmStatus == "3")
            {
                sb.Append(" RemedyReasonID=").AppendParameter(UpdatePunchConfirm_ + "RemedyReasonID", model.RemedyReasonID).Append(",");
                sb.Append(" RemedyReasonCN=").AppendParameter(UpdatePunchConfirm_ + "RemedyReasonCN", model.RemedyReasonCN).Append(",");
                //Remedy(有修改前的相對應欄位)
                sb.Append(" RemedyPunchTime=").AppendParameter(UpdatePunchConfirm_ + "RemedyPunchTime", model.RemedyPunchTime).Append(",");
                sb.Append(" Remedy_MAFT10_FLAG=").AppendParameter(UpdatePunchConfirm_ + "Remedy_MAFT10_FLAG", model.Remedy_MAFT10_FLAG).Append(",");
                sb.Append(" Remedy_AbnormalFlag=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalFlag", model.Remedy_AbnormalFlag).Append(",");
                sb.Append(" Remedy_AbnormalReasonID=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalReasonID", model.Remedy_AbnormalReasonID).Append(",");
                sb.Append(" Remedy_AbnormalReasonCN=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalReasonCN", model.Remedy_AbnormalReasonCN).Append(",");
                sb.Append(" Remedy_AbnormalDesc=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalDesc", model.Remedy_AbnormalDesc).Append(",");
            }
            //LastChange
            sb.Append(" LastChgComp=").AppendParameter(UpdatePunchConfirm_ + "LastChgComp", model.LastChgComp).Append(",");
            sb.Append(" LastChgID=").AppendParameter(UpdatePunchConfirm_ + "LastChgID", model.LastChgID).Append(",");
            sb.Append(" LastChgDate=").AppendParameter(UpdatePunchConfirm_ + "LastChgDate", model.LastChgDate);
            //Where
            sb.Append(" where ");
            sb.Append(" CompID=").AppendParameter(UpdatePunchConfirm_ + "CompID", model.CompID);
            sb.Append(" and EmpID=").AppendParameter(UpdatePunchConfirm_ + "EmpID", model.EmpID);
            sb.Append(" and DutyDate=").AppendParameter(UpdatePunchConfirm_ + "DutyDate", model.DutyDate);
            sb.Append(" and PunchConfirmSeq=").AppendParameter(UpdatePunchConfirm_ + "PunchConfirmSeq", model.PunchConfirmSeq);
        }
    }

    public static void PunchAppdOperation_UpdatePunchConfirm_New(Punch_Confirm_Remedy_Bean model, ref CommandHelper sb, bool isReset = false)
    {
        string UpdatePunchConfirm_ = "UpdatePunchConfirm_";
        if (isReset)
        {
            sb.Reset();
        }
        if (model.ConfirmStatus == "3" || model.ConfirmStatus == "4")
        {
            sb.AppendStatement(" update PunchConfirm SET ");
            //狀態
            sb.Append(" ConfirmStatus=").AppendParameter(UpdatePunchConfirm_ + "ConfirmStatus", model.ConfirmStatus).Append(",");
            //Remedy(無修改前欄位，此為原生欄位)
            if (model.ConfirmStatus == "3")
            {
                sb.Append(" RemedyReasonID=").AppendParameter(UpdatePunchConfirm_ + "RemedyReasonID", model.RemedyReasonID).Append(",");
                sb.Append(" RemedyReasonCN=").AppendParameter(UpdatePunchConfirm_ + "RemedyReasonCN", model.RemedyReasonCN).Append(",");
                //Remedy(有修改前的相對應欄位)
                sb.Append(" RemedyPunchTime=").AppendParameter(UpdatePunchConfirm_ + "RemedyPunchTime", model.RemedyPunchTime).Append(",");
                sb.Append(" Remedy_MAFT10_FLAG=").AppendParameter(UpdatePunchConfirm_ + "Remedy_MAFT10_FLAG", model.Remedy_MAFT10_FLAG).Append(",");
                sb.Append(" Remedy_AbnormalFlag=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalFlag", model.Remedy_AbnormalFlag).Append(",");
                sb.Append(" Remedy_AbnormalReasonID=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalReasonID", model.Remedy_AbnormalReasonID).Append(",");
                sb.Append(" Remedy_AbnormalReasonCN=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalReasonCN", model.Remedy_AbnormalReasonCN).Append(",");
                sb.Append(" Remedy_AbnormalDesc=").AppendParameter(UpdatePunchConfirm_ + "Remedy_AbnormalDesc", model.Remedy_AbnormalDesc).Append(",");
            }
            //LastChange
            sb.Append(" LastChgComp=").AppendParameter(UpdatePunchConfirm_ + "LastChgComp", model.LastChgComp).Append(",");
            sb.Append(" LastChgID=").AppendParameter(UpdatePunchConfirm_ + "LastChgID", model.LastChgID).Append(",");
            sb.Append(" LastChgDate=").AppendParameter(UpdatePunchConfirm_ + "LastChgDate", model.LastChgDate);
            //Where
            sb.Append(" where ");
            sb.Append(" CompID=").AppendParameter(UpdatePunchConfirm_ + "CompID", model.CompID);
            sb.Append(" and EmpID=").AppendParameter(UpdatePunchConfirm_ + "EmpID", model.EmpID);
            sb.Append(" and DutyDate=").AppendParameter(UpdatePunchConfirm_ + "DutyDate", model.DutyDate);
            sb.Append(" and PunchConfirmSeq=").AppendParameter(UpdatePunchConfirm_ + "PunchConfirmSeq", model.PunchConfirmSeq);
        }
    }

    public static void PunchAppdOperation_UpdateHROtherFlowLog(string FlowCaseID, string FlowStatus, ref CommandHelper sb, bool isReset = false)
    {
        string HROtherFlowLog_ = "HROtherFlowLog_";
        if (isReset)
        {
            sb.Reset();
        }
        sb.AppendStatement(" Update HROtherFlowLog set ");
        sb.Append(" FlowStatus=").AppendParameter(HROtherFlowLog_ + "FlowStatus", FlowStatus);
        sb.Append(" where FlowCaseID=").AppendParameter(HROtherFlowLog_ + "0_FlowCaseID", FlowCaseID);
        sb.Append(" and Seq=  (select Top 1 Seq from HROverTimeLog  where FlowCaseID=").AppendParameter(HROtherFlowLog_ + "1_FlowCaseID", FlowCaseID);
        sb.Append(" order by Seq desc) ; ");
    }

    
    //public static void UpdateHROrverTimeLog(string FlowCaseID, string FlowStatus, ref  CommandHelper sb)
    //{
    //    sb.Append("  UPDATE HROverTimeLog set FlowStatus='" + FlowStatus + "'  where FlowCaseID='" + FlowCaseID + "'  and Seq=  (select Top 1 Seq from HROverTimeLog  where FlowCaseID='" + FlowCaseID + "'  order by Seq desc) ; ");
    //}
    #endregion"PunchAppdOperation"
}

