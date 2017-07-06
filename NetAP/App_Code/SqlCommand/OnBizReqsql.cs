using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Text;

/// <summary>
/// 公出流程的摘要描述
/// </summary>
public partial class SqlCommand
{
    //--------------------------------------------------------------------------------------------------------------------------For : 公出查詢

    /// <summary>
    /// SelectVisitFormsql
    /// 公出查詢
    /// 查詢Grid資料
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectVisitFormsql(OnBizPublicOutBean model, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" V.CompID,REPLACE(V.WriteDate,'-','/') AS WriteDate,V.FormSeq,V.OBFormStatus,V.ValidID,V.ValidName,V.EmpID,V.EmpNameN,V.DeputyID,V.DeputyName,");
        sb.Append(" REPLACE(V.VisitBeginDate,'-','/') AS VisitBeginDate,CONVERT(char(5),V.BeginTime) AS BeginTime, ");
        sb.Append(" REPLACE(V.VisitEndDate,'-','/') AS VisitEndDate,CONVERT(char(5),V.EndTime) AS EndTime, ");
        sb.Append(" V.VisitReasonCN, AT.CodeCName AS OBFormStatusName , ");
        sb.Append(" V.FlowCaseID");
        sb.Append(" FROM VisitForm V ");
        sb.Append(" LEFT JOIN AT_CodeMap AT ON AT.TabName = 'On_Business' AND AT.FldName = 'OBFormStatus' AND AT.Code = V.OBFormStatus ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND V.CompID=@CompID ");
        if (!String.IsNullOrEmpty(model.OBUseType))
        {
            switch (model.OBUseType)
            {
                case "1": { sb.Append(" AND V.WriterID=@EmpID "); break; }
                case "2": { sb.Append(" AND V.EmpID=@EmpID "); break; }
            }
        }
        else 
        {
            sb.Append(" AND ( V.EmpID=@EmpID or V.WriterID = @EmpID) ");
        }
        

        if (!String.IsNullOrEmpty(model.VisitBeginDate))
        {
            sb.Append(" AND VisitBeginDate >= @VisitBeginDate ");
        }
        if (!String.IsNullOrEmpty(model.VisitEndDate))
        {
            sb.Append(" AND VisitEndDate <= @VisitEndDate ");
        }
        if (!String.IsNullOrEmpty(model.OBFormStatus))
        {
            sb.Append(" AND OBFormStatus=@OBFormStatus ");
        }
        sb.Append(" ; ");
    }

    /// <summary>
    /// DeleteVisitFormSql
    /// 公出查詢
    /// 暫存_刪除
    /// 把狀態更新為5
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void DeleteVisitFormSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" UPDATE VisitForm SET ");
        sb.Append(" OBFormStatus = '5' ,");
        sb.Append(" LastChgComp = @LastChgComp ,");
        sb.Append(" LastChgID = @LastChgID ,");
        sb.Append(" LastChgDate = getDate() ");
        sb.Append(" WHERE CompID = @CompID ");
        sb.Append(" AND EmpID = @EmpID ");
        sb.Append(" AND WriteDate = @WriteDate ");
        sb.Append(" AND FormSeq = @FormSeq ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// CancelVisitFormSql
    /// 公出查詢
    /// 暫存_取消
    /// 把狀態更新為6
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void CancelVisitFormSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE VisitForm SET ");
        sb.Append(" OBFormStatus = '6' ,");
        sb.Append(" LastChgComp = ").AppendParameter("@LastChgComp", model.LastChgComp).Append(" ,");
        sb.Append(" LastChgID = ").AppendParameter("@LastChgID", model.LastChgID).Append(" ,");
        sb.Append(" LastChgDate = getDate() ");
        sb.Append(" WHERE CompID = ").AppendParameter("@CompID", model.CompID);
        sb.Append(" AND EmpID = ").AppendParameter("@EmpID", model.EmpID);
        sb.Append(" AND WriteDate = ").AppendParameter("@WriteDate", model.WriteDate);
        sb.Append(" AND FormSeq = ").AppendParameter("@FormSeq", model.FormSeq);
        sb.Append(" ; ");
    }

    /// <summary>
    /// UpdateHROtherFlowLogSql
    /// 暫存_取消
    /// 把狀態更新為4
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateHROtherFlowLogSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE HROtherFlowLog SET ");
        sb.Append(" FlowStatus = '4' ,");
        sb.Append(" LastChgComp = ").AppendParameter("@LastChgComp", model.LastChgComp).Append(" ,");
        sb.Append(" LastChgID = ").AppendParameter("@LastChgID", model.LastChgID).Append(" ,");
        sb.Append(" LastChgDate = getDate()");
        sb.Append(" WHERE FlowCaseID = ").AppendParameter("@FlowCaseID", model.FlowCaseID);
        sb.Append(" AND Seq = (SELECT TOP 1 Seq FROM HROtherFlowLog WHERE FlowCaseID = ").AppendParameter("@FlowCaseID", model.FlowCaseID).Append(" ORDER BY Seq DESC) ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// LogoffVisitFormSql
    /// 公出查詢
    /// 核准_註銷
    /// 把狀態更新為9
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void LogoffVisitFormSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE VisitForm SET ");
        sb.Append(" OBFormStatus = '9' ,");
        sb.Append(" LastChgComp = ").AppendParameter("@LastChgComp", model.LastChgComp).Append(" ,");
        sb.Append(" LastChgID = ").AppendParameter("@LastChgID", model.LastChgID).Append(" ,");
        sb.Append(" LastChgDate = getDate() ");
        sb.Append(" WHERE CompID = ").AppendParameter("@CompID", model.CompID);
        sb.Append(" AND EmpID = ").AppendParameter("@EmpID", model.EmpID);
        sb.Append(" AND WriteDate = ").AppendParameter("@WriteDate", model.WriteDate);
        sb.Append(" AND FormSeq = ").AppendParameter("@FormSeq", model.FormSeq);
        sb.Append(" ; ");
    }

    /// <summary>
    /// UpdateOnBizReqAppd_ITRDFlowFullLogSql
    /// 公出查詢
    /// 核准_註銷
    /// 更新OnBizReqAppd_ITRDFlowFullLog狀態
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateOnBizReqAppd_ITRDFlowFullLogSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE OnBizReqAppd_ITRDFlowFullLog SET ");
        sb.Append(" FlowStepBtnID = ").AppendParameter("@FlowStepBtnID", "btnCancel").Append(",");
        sb.Append(" FlowStepBtnCaption = ").AppendParameter("@FlowStepBtnCaption", "取消").Append(",");
        sb.Append(" LogCrDateTime = getDate() ,");
        sb.Append(" LogUpdDateTime = getDate() ");
        sb.Append(" WHERE FlowCaseID = ").AppendParameter("@FlowCaseID", model.FlowCaseID);
        sb.Append(" AND FlowLogBatNo = (SELECT TOP 1 FlowLogBatNo FROM OnBizReqAppd_ITRDFlowFullLog WHERE FlowCaseID = ").AppendParameter("@FlowCaseID", model.FlowCaseID).Append(" ORDER BY FlowLogBatNo DESC) ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// UpdateOnBizReqAppd_ITRDFlowCaseSql
    /// 公出查詢
    /// 核准_註銷
    /// 更新OnBizReqAppd_ITRDFlowCase狀態
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateOnBizReqAppd_ITRDFlowCaseSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE OnBizReqAppd_ITRDFlowCase SET ");
        sb.Append(" FlowCurrStepID = 'Z03' ");
        sb.Append(" WHERE FlowCaseID = ").AppendParameter("@FlowCaseID", model.FlowCaseID);
        sb.Append(" ; ");
    }

    /// <summary>
    /// InsertOnBizReqAppd_ITRDFlowFullLogSql
    /// 公出查詢
    /// 核准_註銷
    /// 新增一筆資料到OnBizReqAppd_ITRDFlowFullLog
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void InsertOnBizReqAppd_ITRDFlowFullLogSql(OnBizPublicOutBean model, string flowLogNo, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" INSERT INTO OnBizReqAppd_ITRDFlowFullLog ( ");
        sb.Append(" FlowCaseID , FlowLogBatNo , FlowLogID , FlowStepID , FlowStepName , FlowStepBtnID , FlowStepBtnCaption , FlowStepOpinion , FlowLogIsClose , ");
        sb.Append(" IsProxy , AttachID , FromDept , FromDeptName , FromUser , FromUserName , AssignTo , AssignToName , ");
        sb.Append(" ToDept , ToDeptName , ToUser , ToUserName , LogCrDateTime , LogUpdDateTime )");
        sb.Append("  VALUES ( ");
        sb.AppendParameter("@FlowCaseID", model.FlowCaseID).Append(",");
        sb.AppendParameter("@FlowLogBatNo", flowLogNo).Append(",");
        sb.AppendParameter("@FlowLogID", model.FlowCaseID + "." + flowLogNo.PadLeft(5, '0')).Append(",");
        sb.AppendParameter("@FlowStepID", "Z03").Append(",");
        sb.AppendParameter("@FlowStepName", "取消").Append(",");
        sb.AppendParameter("@FlowStepBtnID", "").Append(",");
        sb.AppendParameter("@FlowStepBtnCaption", "").Append(",");
        sb.AppendParameter("@FlowStepOpinion", "").Append(",");
        sb.AppendParameter("@FlowLogIsClose", "Y").Append(",");
        sb.AppendParameter("@IsProxy", "").Append(",");
        sb.AppendParameter("@AttachID", "").Append(",");
        sb.AppendParameter("@FromDept", model.DeptID).Append(",");
        sb.AppendParameter("@FromDeptName", model.DeptName).Append(",");
        sb.AppendParameter("@FromUser", model.WriterID).Append(",");
        sb.AppendParameter("@FromUserName", model.WriterName).Append(",");
        sb.AppendParameter("@AssignTo", model.WriterID).Append(",");
        sb.AppendParameter("@AssignToName", model.WriterName).Append(",");
        sb.AppendParameter("@ToDept", model.DeptID).Append(",");
        sb.AppendParameter("@ToDeptName", model.DeptName).Append(",");
        sb.AppendParameter("@ToUser", model.WriterID).Append(",");
        sb.AppendParameter("@ToUserName", model.WriterName).Append(",");
        sb.Append("getDate() ,");
        sb.Append("getDate()").Append(")");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectOnBizReqAppd_ITRDFlowFullLogSql
    /// 公出查詢
    /// 核准_註銷
    /// 取得FlowLogBatNo
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectOnBizReqAppd_ITRDFlowFullLogSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT TOP 1 FlowLogBatNo ");
        sb.Append(" FROM OnBizReqAppd_ITRDFlowFullLog ");
        sb.Append(" WHERE FlowCaseID = @FlowCaseID ");
        sb.Append(" ORDER BY FlowLogBatNo DESC ");
        sb.Append(" ; ");
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出申請

    /// <summary>
    /// SelectWorkSiteSql
    /// 公出申請
    /// 查詢工作地點
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectWorkSiteDTSql(ref StringBuilder sb,string compID, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" RTRIM(WorkSiteID) AS InterLocationID, RTRIM(WorkSiteID) +' '+ Remark AS InterLocationName");
        sb.Append(" FROM " + _eHRMSDB_ITRD + " .dbo.WorkSite ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID= '"+ compID+"'" );
        sb.Append(" ORDER BY WorkSiteID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectWorkSiteSql
    /// 公出申請
    /// 查詢工作地點
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectWorkSiteSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" RTRIM(WorkSiteID) AS InterLocationID,Remark AS InterLocationName");
        sb.Append(" FROM WorkSite ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" ORDER BY WorkSiteID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectVisitFormMaxSeqSql
    /// 公出申請
    /// 查詢VisitForm最大的Seq
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectVisitFormMaxSeqSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" TOP 1 FormSeq ");
        sb.Append(" FROM VisitForm ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND WriterID=@WriterID ");
        sb.Append(" AND WriteDate=@WriteDate ");
        sb.Append(" ORDER BY FormSeq DESC");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectSameKeySql
    /// 公出申請
    /// Same Key 查詢
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectSameKeySql(OnBizPublicOutBean databean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" CompID, EmpID, WriteDate, FormSeq, LastChgComp, LastChgID, LastChgDate ");
        sb.Append(" FROM VisitForm ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" AND WriteDate=@WriteDate ");
        sb.Append(" AND FormSeq=@FormSeq ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// InsertVisitFormSql
    /// 公出申請
    /// 申請 > 暫存 
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void InsertVisitFormSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" INSERT INTO VisitForm ( ");
        sb.Append(" CompID, EmpID, EmpNameN, WriteDate, WriteTime, WriterID, WriterName, FormSeq, FlowCaseID, TransactionSeq, OBFormStatus, ValidDate, ");
        sb.Append(" ValidID, ValidName,RejectReasonID, RejectReasonCN, DeptID, DeptName, OrganID, OrganName, WorkTypeID, WorkType, FlowOrganID, FlowOrganName, TitleID, ");
        sb.Append(" TitleName, PositionID, Position, Tel_1, Tel_2, VisitBeginDate, BeginTime, VisitEndDate, EndTime, DeputyID, DeputyName, LocationType, InterLocationID, ");
        sb.Append(" InterLocationName, ExterLocationName, VisiterName, VisiterTel, VisitReasonID, VisitReasonCN, VisitReasonDesc, ");
        sb.Append(" LastChgComp, LastChgID, LastChgDate ");
        sb.Append(" ) VALUES ( ");
        sb.Append(" @CompID, @EmpID, @EmpNameN, @WriteDate, @WriteTime, @WriterID, @WriterName, @FormSeq, @FlowCaseID, @TransactionSeq, @OBFormStatus, @ValidDate, ");
        sb.Append(" @ValidID, @ValidName, @RejectReasonID, @RejectReasonCN, @DeptID, @DeptName, @OrganID, @OrganName, @WorkTypeID, @WorkType, @FlowOrganID, @FlowOrganName, @TitleID, ");
        sb.Append(" @TitleName, @PositionID, @Position, @Tel_1, @Tel_2, @VisitBeginDate, @BeginTime, @VisitEndDate, @EndTime, @DeputyID, @DeputyName, @LocationType, @InterLocationID, ");
        sb.Append(" @InterLocationName, @ExterLocationName, @VisiterName, @VisiterTel, @VisitReasonID, @VisitReasonCN, @VisitReasonDesc, ");
        sb.Append(" @LastChgComp, @LastChgID, getDate() ");
        sb.Append(" ); ");
    }

    /// <summary>
    /// InesrtVisitFormSendSql
    /// 公出申請
    /// 申請 > 送簽
    /// </summary>s
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void InesrtVisitFormSendSql(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" INSERT INTO VisitForm ( ");
        sb.Append(" CompID, EmpID, EmpNameN, WriteDate, WriteTime, WriterID, WriterName, FormSeq, FlowCaseID, TransactionSeq, OBFormStatus, ValidDate, ");//13
        sb.Append(" ValidID, ValidName, RejectReasonID, RejectReasonCN, DeptID, DeptName, OrganID, OrganName, WorkTypeID, WorkType, FlowOrganID, FlowOrganName, TitleID, ");//13
        sb.Append(" TitleName, PositionID, Position, Tel_1, Tel_2, VisitBeginDate, BeginTime, VisitEndDate, EndTime, DeputyID, DeputyName, LocationType, InterLocationID, ");//13
        sb.Append(" InterLocationName, ExterLocationName, VisiterName, VisiterTel, VisitReasonID, VisitReasonCN, VisitReasonDesc, ");//7
        sb.Append(" LastChgComp, LastChgID, LastChgDate ");//3
        sb.Append(" ) VALUES ( ");
        sb.AppendParameter("CompID", model.CompID).Append(",").AppendParameter("EmpID", model.EmpID).Append(",");
        sb.AppendParameter("EmpNameN", model.EmpNameN).Append(",").AppendParameter("WriteDate", model.WriteDate).Append(",");
        sb.AppendParameter("WriteTime", model.WriteTime).Append(",").AppendParameter("WriterID", model.WriterID).Append(",");
        sb.AppendParameter("WriterName", model.WriterName).Append(",").AppendParameter("FormSeq", model.FormSeq).Append(",");
        sb.AppendParameter("FlowCaseID", model.FlowCaseID).Append(",").AppendParameter("TransactionSeq", model.TransactionSeq).Append(",");
        sb.AppendParameter("OBFormStatus", model.OBFormStatus).Append(",");
        sb.AppendParameter("ValidDate", model.ValidDate).Append(",").AppendParameter("ValidID", model.ValidID).Append(",");
        sb.AppendParameter("ValidName", model.ValidName).Append(",").AppendParameter("RejectReasonID", model.RejectReasonID).Append(",");
        sb.AppendParameter("RejectReasonCN", model.RejectReasonCN).Append(",").AppendParameter("DeptID", model.DeptID).Append(",");
        sb.AppendParameter("DeptName", model.DeptName).Append(",").AppendParameter("OrganID", model.OrganID).Append(",");
        sb.AppendParameter("OrganName", model.OrganName).Append(",").AppendParameter("WorkTypeID", model.WorkTypeID).Append(",");
        sb.AppendParameter("WorkType", model.WorkType).Append(",").AppendParameter("FlowOrganID", model.FlowOrganID).Append(",");
        sb.AppendParameter("FlowOrganName", model.FlowOrganName).Append(",").AppendParameter("TitleID", model.TitleID).Append(",").AppendParameter("TitleName", model.TitleName).Append(",");
        sb.AppendParameter("PositionID", model.PositionID).Append(",").AppendParameter("Position", model.Position).Append(",");
        sb.AppendParameter("Tel_1", model.Tel_1).Append(",").AppendParameter("Tel_2", model.Tel_2).Append(",");
        sb.AppendParameter("VisitBeginDate", model.VisitBeginDate).Append(",").AppendParameter("BeginTime", model.BeginTime).Append(",");
        sb.AppendParameter("VisitEndDate", model.VisitEndDate).Append(",").AppendParameter("EndTime", model.EndTime).Append(",");
        sb.AppendParameter("DeputyID", model.DeputyID).Append(",").AppendParameter("DeputyName", model.DeputyName).Append(",");
        sb.AppendParameter("LocationType", model.LocationType).Append(",").AppendParameter("InterLocationID", model.InterLocationID).Append(",");
        sb.AppendParameter("InterLocationName", model.InterLocationName).Append(",").AppendParameter("ExterLocationName", model.ExterLocationName).Append(",");
        sb.AppendParameter("VisiterName", model.VisiterName).Append(",").AppendParameter("VisiterTel", model.VisiterTel).Append(",");
        sb.AppendParameter("VisitReasonID", model.VisitReasonID).Append(",").AppendParameter("VisitReasonCN", model.VisitReasonCN).Append(",");
        sb.AppendParameter("VisitReasonDesc", model.VisitReasonDesc).Append(",").AppendParameter("LastChgComp", model.LastChgComp).Append(",");
        sb.AppendParameter("LastChgID", model.LastChgID).Append(",").Append("getDate()");
        sb.Append(" ); ");
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出申請、公出修改

    /// <summary>
    /// SelectSupervisorSql
    /// 公出申請、公出修改
    /// 取得主管人員
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectSupervisorSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine(" SELECT NameN AS EmpNameN ");
        sb.AppendLine(" FROM Personal ");
        sb.AppendLine(" WHERE 0 = 0 ");
        sb.AppendLine(" AND CompID = @CompID ");
        sb.AppendLine(" AND EmpID = @EmpID ");
        sb.AppendLine(" ;");
    }

    /// <summary>
    /// SelectPersonSql
    /// 公出申請、公出修改、打卡查詢--單位
    /// 查詢人員資料
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPersonSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" P.CompID,C.CompName,P.EmpID,P.NameN AS EmpNameN,P.DeptID,Dpt.OrganName AS DeptName,P.OrganID,O.OrganName,WW.WorkTypeID,WW.Remark AS WorkType, ");
        sb.Append(" PP.PositionID,PP.Remark AS Position,OrgF.OrganID AS FlowOrganID,OrgF.OrganName AS FlowOrganName,P.TitleID,T.TitleName ");
        sb.Append(" FROM Personal P ");
        sb.Append(" LEFT JOIN Company C ON P.CompID = C.CompID ");
        sb.Append(" LEFT JOIN Organization O ON P.CompID = O.CompID AND P.OrganID = O.OrganID ");
        sb.Append(" LEFT JOIN Title T on P.CompID=T.CompID and P.TitleID=T.TitleID and P.RankID=T.RankID ");
        sb.Append(" LEFT JOIN EmpPosition EP on P.CompID=EP.CompID and P.EmpID=EP.EmpID and EP.PrincipalFlag='1' ");
        sb.Append(" LEFT JOIN Position PP on EP.CompID=PP.CompID and EP.PositionID=PP.PositionID ");
        sb.Append(" LEFT JOIN EmpFlow EF on P.CompID=EF.CompID and P.EmpID=EF.EmpID ");
        sb.Append(" LEFT JOIN OrganizationFlow OrgF on EF.OrganID=OrgF.OrganID ");
        sb.Append(" LEFT JOIN Organization Dpt on P.CompID=Dpt.CompID and P.DeptID=Dpt.OrganID ");
        sb.Append(" LEFT JOIN EmpWorkType EW on P.CompID=EW.CompID and P.EmpID=EW.EmpID and EW.PrincipalFlag='1' ");
        sb.Append(" LEFT JOIN WorkType WW on EW.CompID=WW.CompID and EW.WorkTypeID=WW.WorkTypeID ");
        sb.Append(" WHERE P.WorkStatus='1' ");
        sb.Append(" AND P.CompID = @CompID ");
        sb.Append(" AND P.EmpID = @EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectAT_CodeMapSql
    /// 公出申請、公出修改
    /// 洽辦事由下拉選單(要做調整)
    /// </summary>
    /// <param name="Tabname">On_Business</param>
    /// <param name="FldName">VisitReasonCN</param>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectAT_CodeMapSql(string Tabname,string FldName,ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine(" SELECT Code AS DataValue , CodeCName AS DataText ");
        sb.AppendLine(" FROM AT_CodeMap ");
        sb.AppendFormat(" WHERE TabName = '{0}' ",Tabname);
        sb.AppendFormat(" AND FldName = '{0}' ", FldName);
        sb.AppendLine(" ORDER BY Code ");
        sb.AppendLine(" ;");
    }

    /// <summary>
    /// SelectEmpFlowSql
    /// 公出申請、公出修改
    /// 洽辦事由下拉選單(要做調整)
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectEmpFlowValidSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine(" SELECT Boss AS DataValue,P.NameN as DataText from EmpFlow EF ");
        sb.AppendLine(" LEFT JOIN OrganizationFlow O ON EF.OrganID=O.OrganID ");
        sb.AppendLine(" LEFT JOIN Personal P ON P.EmpID = O.Boss ");
        sb.AppendLine(" WHERE 0=0 ");
        sb.AppendLine(" AND EF.EmpID=@EmpID ");
        sb.AppendLine(" AND EF.CompID=@CompID ");
        sb.AppendLine(" ;");
    }

    /// <summary>
    /// SelectPersonMailSql
    /// 公出申請、公出修改
    /// 查詢員工E-Mail
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectPersonMailSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" ISNULL(C1.EMail,'') AS EmpID_EMail, ");
        sb.Append(" FROM Personal P ");
        sb.Append(" LEFT JOIN Communication C1 ON P.IDNo=C1.IDNo ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND P.CompID = @CompID ");
        sb.Append(" AND P.EmpID = @EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectHRMailSql
    /// 公出申請、公出修改
    /// 查詢HR E-Mail
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectHRMailSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT C.EMail AS EmpID_EMail FROM HRDB.dbo.SC_UserGroup SC ");
        sb.Append(" LEFT JOIN Personal P ON P.EmpID=SC.UserID AND P.CompID=SC.CompID ");
        sb.Append(" LEFT JOIN Communication C ON P.IDNo=C.IDNo ");
        sb.Append(" LEFT JOIN Communication C1 ON P.IDNo=C1.IDNo ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND SC.CompID= @CompID ");
        sb.Append(" AND SC.GroupID='ADM-OT' ");
        sb.Append(" ; ");
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出修改

    /// <summary>
    /// UpdateVisitFormSql
    /// 公出修改
    /// 修改 > 暫存
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateVisitFormSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" UPDATE VisitForm SET ");
        sb.Append(" EmpID =@EmpID ,EmpNameN = @EmpNameN ,WriteDate = @WriteDate ,WriteTime = @WriteTime ,WriterID = @WriterID ,WriterName = @WriterName , ");
        sb.Append(" DeptID = @DeptID ,DeptName = @DeptName ,OrganID = @OrganID ,OrganName = @OrganName ,WorkTypeID = @WorkTypeID ,WorkType = @WorkType , ");
        sb.Append(" FlowOrganID = @FlowOrganID ,FlowOrganName = @FlowOrganName ,TitleID = @TitleID ,TitleName = @TitleName ,PositionID = @PositionID ,Position = @Position , ");
        sb.Append(" Tel_1 = @Tel_1 ,Tel_2 = @Tel_2 ,VisitBeginDate = @VisitBeginDate ,BeginTime = @BeginTime ,VisitEndDate = @VisitEndDate ,EndTime = @EndTime , ");
        sb.Append(" DeputyID = @DeputyID ,DeputyName = @DeputyName , ");
        sb.Append(" LocationType = @LocationType ,InterLocationID = @InterLocationID ,InterLocationName = @InterLocationName ,ExterLocationName = @ExterLocationName , ");
        sb.Append(" VisiterName = @VisiterName ,VisiterTel = @VisiterTel ,VisitReasonID = @VisitReasonID ,VisitReasonCN = @VisitReasonCN ,VisitReasonDesc = @VisitReasonDesc , ");
        sb.Append(" LastChgComp = @LastChgComp ,LastChgID = @LastChgID ,LastChgDate = getDate() ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID = @oldCompID ");
        sb.Append(" AND EmpID = @oldEmpID ");
        sb.Append(" AND WriteDate = @oldWriteDate ");
        sb.Append(" AND FormSeq = @oldFormSeq ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// UpdateVisitFormSend
    /// 公出修改
    /// 修改 > 送簽
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateVisitFormSend(OnBizPublicOutBean model, ref CommandHelper sb, bool isReset = false)
    {
        if (isReset)
        {
            sb.Reset();
        }
        sb.Append(" UPDATE VisitForm SET ");
        sb.Append("EmpID = ").AppendParameter("EmpID", model.EmpID).Append(",").Append("EmpNameN = ").AppendParameter("EmpNameN", model.EmpNameN).Append(",");
        sb.Append("WriteDate = ").AppendParameter("WriteDate", model.WriteDate).Append(",").Append("WriteTime = ").AppendParameter("WriteTime", model.WriteTime).Append(",");
        sb.Append("WriterID = ").AppendParameter("WriterID", model.WriterID).Append(",").Append("WriterName = ").AppendParameter("WriterName", model.WriterName).Append(",");
        sb.Append("FlowCaseID = ").AppendParameter("FlowCaseID", model.FlowCaseID).Append(",").Append("OBFormStatus = ").AppendParameter("OBFormStatus", model.OBFormStatus).Append(",");
        sb.Append("ValidID = ").AppendParameter("ValidID", model.ValidID).Append(",").Append("ValidName = ").AppendParameter("ValidName", model.ValidName).Append(",");
        sb.Append("DeptID = ").AppendParameter("DeptID", model.DeptID).Append(",").Append("DeptName = ").AppendParameter("DeptName", model.DeptName).Append(",");
        sb.Append("OrganID = ").AppendParameter("OrganID", model.OrganID).Append(",").Append("OrganName = ").AppendParameter("OrganName", model.OrganName).Append(",");
        sb.Append("WorkTypeID = ").AppendParameter("WorkTypeID", model.WorkTypeID).Append(",").Append("WorkType = ").AppendParameter("WorkType", model.WorkType).Append(",");
        sb.Append("FlowOrganID = ").AppendParameter("FlowOrganID", model.FlowOrganID).Append(",").Append("FlowOrganName = ").AppendParameter("FlowOrganName", model.FlowOrganName).Append(",");
        sb.Append("TitleID = ").AppendParameter("TitleID", model.TitleID).Append(",").Append("TitleName = ").AppendParameter("TitleName", model.TitleName).Append(",");
        sb.Append("PositionID = ").AppendParameter("PositionID", model.PositionID).Append(",").Append("Position = ").AppendParameter("Position", model.Position).Append(",");
        sb.Append("Tel_1 = ").AppendParameter("Tel_1", model.Tel_1).Append(",").Append("Tel_2 = ").AppendParameter("Tel_2", model.Tel_2).Append(",");
        sb.Append("VisitBeginDate = ").AppendParameter("VisitBeginDate", model.VisitBeginDate).Append(",").Append("BeginTime = ").AppendParameter("BeginTime", model.BeginTime).Append(",");
        sb.Append("VisitEndDate = ").AppendParameter("VisitEndDate", model.VisitEndDate).Append(",").Append("EndTime = ").AppendParameter("EndTime", model.EndTime).Append(",");
        sb.Append("DeputyID = ").AppendParameter("DeputyID", model.DeputyID).Append(",").Append("DeputyName = ").AppendParameter("DeputyName", model.DeputyName).Append(",");
        sb.Append("LocationType = ").AppendParameter("LocationType", model.LocationType).Append(",").Append("InterLocationID = ").AppendParameter("InterLocationID", model.InterLocationID).Append(",");
        sb.Append("InterLocationName = ").AppendParameter("InterLocationName", model.InterLocationName).Append(",").Append("ExterLocationName = ").AppendParameter("ExterLocationName", model.ExterLocationName).Append(",");
        sb.Append("VisiterName = ").AppendParameter("VisiterName", model.VisiterName).Append(",").Append("VisiterTel = ").AppendParameter("VisiterTel", model.VisiterTel).Append(",");
        sb.Append("VisitReasonID = ").AppendParameter("VisitReasonID", model.VisitReasonID).Append(",").Append("VisitReasonCN = ").AppendParameter("VisitReasonCN", model.VisitReasonCN).Append(",");
        sb.Append("VisitReasonDesc = ").AppendParameter("VisitReasonDesc", model.VisitReasonDesc).Append(",").Append("LastChgComp = ").AppendParameter("LastChgComp", model.LastChgComp).Append(",");
        sb.Append("LastChgID = ").AppendParameter("LastChgID", model.LastChgID).Append(",").Append("LastChgDate = getDate() ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID = ").AppendParameter("OldCompID", model.oldCompID);
        sb.Append(" AND EmpID = ").AppendParameter("OldEmpID", model.oldEmpID);
        sb.Append(" AND WriteDate = ").AppendParameter("OldWriteDate", model.oldWriteDate);
        sb.Append(" AND FormSeq = ").AppendParameter("OldFormSeq", model.oldFormSeq);
        //sb.Append(" ; ");
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出修改、公出明細

    /// <summary>
    /// SelectOnlyVisitFormSql
    /// 公出修改、公出明細
    /// 查出公出單明細資料
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectOnlyVisitFormSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" V.CompID,C.CompName,V.EmpID,V.EmpNameN,REPLACE(V.WriteDate,'-','/') AS WriteDate ,V.WriterID,V.WriterName,V.FormSeq, ");
        sb.Append(" V.VisitFormNo,V.OrganID,V.OrganName,V.TitleID,V.TitleName,V.PositionID,V.Position,V.Tel_1,V.Tel_2, ");
        sb.Append(" REPLACE(V.VisitBeginDate,'-','/') AS VisitBeginDate,CONVERT(char(5),V.BeginTime) AS BeginTime, ");
        sb.Append(" REPLACE(V.VisitEndDate,'-','/') AS VisitEndDate,CONVERT(char(5),V.EndTime) AS EndTime, ");
        sb.Append(" V.DeputyID,V.DeputyName, ");
        sb.Append(" V.LocationType,V.InterLocationID,V.InterLocationName,V.ExterLocationName,V.VisiterName,V.VisiterTel,V.VisitReasonID,V.VisitReasonCN,V.VisitReasonDesc, ");
        sb.Append(" V.LastChgComp,CL.CompName AS LastChgCompName,V.LastChgID,P.NameN AS LastChgName,V.LastChgDate ");
        sb.Append(" FROM VisitForm V ");
        sb.Append(" LEFT JOIN eHRMSDB_ITRD.dbo.Company C ON V.CompID = C.CompID ");
        sb.Append(" LEFT JOIN eHRMSDB_ITRD.dbo.Company CL ON V.LastChgComp = CL.CompID ");
        sb.Append(" LEFT JOIN eHRMSDB_ITRD.dbo.Personal P ON V.LastChgComp = P.CompID AND V.LastChgID = P.EmpID ");
        sb.Append(" WHERE 0 = 0");
        sb.Append(" AND V.CompID = @CompID ");
        sb.Append(" AND V.EmpID = @EmpID ");
        sb.Append(" AND V.WriteDate = @WriteDate ");
        sb.Append(" AND V.FormSeq = @FormSeq ");
        sb.Append(" ; ");
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出單紀錄查詢

    /// <summary>
    /// SelectVisitFormOrganSql
    /// 公出單紀錄查詢
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectVisitFormOrganSql(OnBizPublicOutBean model,string searchStr ,ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" V.CompID,REPLACE(V.WriteDate,'-','/') AS WriteDate,V.FormSeq,V.OBFormStatus,V.ValidID,V.ValidName,V.EmpID,V.EmpNameN,V.DeputyID,V.DeputyName,");
        sb.Append(" REPLACE(V.VisitBeginDate,'-','/') AS VisitBeginDate,CONVERT(char(5),V.BeginTime) AS BeginTime, ");
        sb.Append(" REPLACE(V.VisitEndDate,'-','/') AS VisitEndDate,CONVERT(char(5),V.EndTime) AS EndTime, ");
        sb.Append(" V.VisitReasonCN, AT.CodeCName AS OBFormStatusName , ");
        sb.Append(" V.FlowCaseID,LG.FlowLogID ");
        sb.Append(" FROM VisitForm V ");
        sb.Append(" LEFT JOIN OnBizReqAppd_ITRDFlowOpenLog LG ON V.FlowCaseID = LG.FlowCaseID ");
        sb.Append(" LEFT JOIN AT_CodeMap AT ON AT.TabName = 'On_Business' AND AT.FldName = 'OBFormStatus' AND AT.Code = V.OBFormStatus ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
            
        switch (searchStr)
        {
            case "Organ":
                {
                    sb.Append(" AND V.OrganID IN ('" +model.OrganID + "') ");
                    break;
                }
            case "FlowOrgan":
                {
                    sb.Append(" AND V.FlowOrganID IN ('" + model.FlowOrganID + "') ");
                    break;
                }
        }
        if (!String.IsNullOrEmpty(model.EmpID))
        {
            sb.Append(" AND V.EmpID=@EmpID ");
        }
        if (!String.IsNullOrEmpty(model.VisitBeginDate))
        {
            sb.Append(" AND V.VisitBeginDate >= @VisitBeginDate ");
        }
        if (!String.IsNullOrEmpty(model.VisitEndDate))
        {
            sb.Append(" AND V.VisitEndDate <= @VisitEndDate ");
        }
        if (!String.IsNullOrEmpty(model.OBFormStatus))
        {
            sb.Append(" AND V.OBFormStatus=@OBFormStatus ");
        }
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectBothCompSql
    /// 公出單紀錄查詢
    /// 主管_可能隸屬於兩家公司
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectBothCompSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.AppendLine("SELECT C.CompID AS DataValue, C.CompID + '-' + C.CompName AS DataText");
        sb.AppendLine("FROM Organization O");
        sb.AppendLine("JOIN Company C ON O.CompID = C.CompID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.BossCompID = @selectCompID");
        sb.AppendLine("AND O.Boss = @EmpID");
        sb.AppendLine("UNION");
        sb.AppendLine("SELECT C.CompID AS DataValue, C.CompID + '-' + C.CompName AS DataText");
        sb.AppendLine("FROM OrganizationFlow O");
        sb.AppendLine("JOIN Company C ON O.CompID = C.CompID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.BossCompID = @selectCompID");
        sb.AppendLine("AND O.Boss = @EmpID");
        sb.AppendLine("AND BusinessType IN (SELECT Code FROM HRCodeMap WHERE TabName = 'Business' AND FldName = 'BusinessType' AND NotShowFlag = '0')");
    }

    /// <summary>
    /// SelectOrganSql
    /// 公出單紀錄查詢
    /// 主管_轄下單位(行政)
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectOrganSql(ref StringBuilder sb, bool isReset = false)
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
        sb.AppendLine("WHERE O.CompID = @selectCompID");
        sb.AppendLine("AND O.OrganID IN (");
        sb.AppendLine("SELECT OrganID FROM funGetAllowOrgan(@selectCompID, @CompID, @EmpID, '')");
        sb.AppendLine(")");
    }

    /// <summary>
    /// SelectFlowOrganSql
    /// 公出單紀錄查詢
    /// 主管_轄下單位(功能)
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param> 
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectFlowOrganSql(ref StringBuilder sb, bool isReset = false)
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
        sb.AppendLine("AND OrganID IN (");
        sb.AppendLine("SELECT OrganID FROM funGetAllowOrgan(@selectCompID, @CompID, @EmpID, @FlowType)");
        sb.AppendLine(")");
        sb.AppendLine("ORDER BY BusinessType, OrganLevel, RoleCode DESC, OrganID");
    }

}