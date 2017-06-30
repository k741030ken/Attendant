using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AT_CodeMap_Bean
{
    public String TabName { get; set; }
    public String FldName { get; set; }
    public String Code { get; set; }
    public String CodeCName { get; set; }
    public String SortFld { get; set; }
    public String NotShowFlag { get; set; }
    public String LastChgComp { get; set; }
    public String LastChgID { get; set; }
    public String LastChgDate { get; set; }
    public List<AT_CodeMap_Model> SelectDDLDataList { get; set; }
}

public class HROtherFlowLog_Bean
{
public String FlowCaseID { get; set; }
public String FlowLogBatNo { get; set; }
public String FlowLogID { get; set; }
public String Seq { get; set; }
public String Mode { get; set; }
public String ApplyID { get; set; }
public String EmpID { get; set; }
public String EmpOrganID { get; set; }
public String EmpFlowOrganID { get; set; }
public String SignOrganID { get; set; }
public String SignFlowOrganID { get; set; }
public String FlowCode { get; set; }
public String FlowSN { get; set; }
public String FlowSeq { get; set; }
public String SignLine { get; set; }
public String SignIDComp { get; set; }
public String SignID { get; set; }
public String SignTime { get; set; }
public String FlowStatus { get; set; }
public String ReAssignFlag { get; set; }
public String ReAssignComp { get; set; }
public String ReAssignEmpID { get; set; }
public String Remark { get; set; }
public String LastChgComp { get; set; }
public String LastChgID { get; set; }
public String LastChgDate { get; set; }
}

public class Punch_Confirm_Remedy_Bean //請取有意義的名稱+Bean結尾
{
    public String CompID { get; set; }
    public String EmpID { get; set; }
    public String EmpName { get; set; }
    public String DutyDate { get; set; }
    public String DutyTime { get; set; }
    public String PunchDate { get; set; }
    public String PunchTime { get; set; }
    public String PunchConfirmSeq { get; set; }
    public String DeptID { get; set; }
    public String DeptName { get; set; }
    public String OrganID { get; set; }
    public String OrganName { get; set; }
    public String FlowOrganID { get; set; }
    public String FlowOrganName { get; set; }
    public String Sex { get; set; }
    public String PunchFlag { get; set; }
    public String WorkTypeID { get; set; }
    public String WorkType { get; set; }
    public String MAFT10_FLAG { get; set; }
    public String ConfirmStatus { get; set; }
    public String AbnormalType { get; set; }
    public String ConfirmPunchFlag { get; set; }
    public String PunchSeq { get; set; }
    public String PunchRemedySeq { get; set; }
    public String RemedyReasonID { get; set; }
    public String RemedyReasonCN { get; set; }
    public String RemedyPunchTime { get; set; }
    public String AbnormalFlag { get; set; }
    public String AbnormalReasonID { get; set; }
    public String AbnormalReasonCN { get; set; }
    public String AbnormalDesc { get; set; }
    public String Remedy_AbnormalFlag { get; set; }
    public String Remedy_AbnormalReasonID { get; set; }
    public String Remedy_AbnormalReasonCN { get; set; }
    public String Remedy_AbnormalDesc { get; set; }
    public String Source { get; set; }
    public String APPContent { get; set; }
    public String LastChgComp { get; set; }
    public String LastChgID { get; set; }
    public String LastChgDate { get; set; }

    //Confirm與Remedy沒有公用的欄位
    public String RemedyPunchFlag { get; set; }
    public String BatchFlag { get; set; }
    public String PORemedyStatus { get; set; }
    public String RejectReason { get; set; }
    public String RejectReasonCN { get; set; }
    public String ValidDateTime { get; set; }
    public String ValidCompID { get; set; }
    public String ValidID { get; set; }
    public String ValidName { get; set; }
    public String Remedy_MAFT10_FLAG { get; set; }

    //gridview查詢條件使用
    public String StartPunchDate { get; set; }// 打卡日期-查詢起始
    public String EndPunchDate { get; set; }// 打卡日期-查詢結束

    //gridview顯示中文用
    public String ConfirmStatusGCN { get; set; }
    public String ConfirmPunchFlagGCN { get; set; }
    public String AbnormalReasonGCN { get; set; }
    public String SourceGCN { get; set; }
    public String SexGCN { get; set; }

    //給預存PunchCheckData計算用資料
    public String SpecialFlag { get; set; }
    public String RestBeginTime { get; set; }
    public String RestEndTime { get; set; }
    public String OtherPunchTime { get; set; }

    //永豐流程FlowCaseID
    public String FlowCaseID { get; set; }

    /// <summary>
    /// Grid值
    /// </summary>
    public List<Punch_Confirm_Remedy_Model> SelectGridDataList { get; set; }
}

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class PunchRemedyLogBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 永豐流程FlowCaseID
    /// </summary>
    public String FlowCaseID { get; set; }

    /// <summary>
    /// 打卡人公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 部門ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 部門名稱
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 單位ID
    /// </summary>
    public String PunchDate { get; set; }

    /// <summary>
    /// 單位名稱
    /// </summary>
    public String PunchTime { get; set; }

    /// <summary>
    /// 特殊單位
    /// </summary>
    public String PunchRemedySeq { get; set; }

    /// <summary>
    /// 打卡人ID
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 打卡人姓名
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 打卡類型。0:出退勤打卡，1:午休打卡。
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 工作性質代碼
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 工作性質
    /// </summary>
    public String PunchConfirmSeq { get; set; }

    /// <summary>
    /// 女性十點後打卡註記
    /// </summary>
    public String RemedyPunchFlag { get; set; }

    /// <summary>
    /// 異常類型
    /// </summary>
    public String DutyDate { get; set; }

    /// <summary>
    /// 異常原因代碼
    /// </summary>
    public String DutyTime { get; set; }

    /// <summary>
    /// 異常原因中文
    /// </summary>
    public String MAFT10_FLAG { get; set; }

    /// <summary>
    /// 其他異常原因說明
    /// </summary>
    public String AbnormalFlag { get; set; }

    /// <summary>
    /// 批次處理註記，0:未處理，1:已處理。
    /// </summary>
    public String AbnormalReasonID { get; set; }

    /// <summary>
    /// 來源別。A:APP，B:永豐雲
    /// </summary>
    public String AbnormalReasonCN { get; set; }

    /// <summary>
    /// 以Json字串格式，不包含換行格式。
    /// </summary>
    public String AbnormalDesc { get; set; }

    /// <summary>
    /// APP緯度
    /// </summary>
    public String BatchFlag { get; set; }

    /// <summary>
    /// APP經度
    /// </summary>
    public String PORemedyStatus { get; set; }

    /// <summary>
    /// APP定位類型。GPS:GPS定位，AGPS:輔助GPS定位(可能室內)
    /// </summary>
    public String RejectReason { get; set; }

    /// <summary>
    /// APP系統別。A:Android，I:IOS
    /// </summary>
    public String RejectReasonCN { get; set; }

    /// <summary>
    ///APP設備別，字串長度255
    /// </summary>
    public String ValidDateTime { get; set; }

    ///// <summary>
    ///// 加班人性別。1:男，2:女。
    ///// </summary>
    //public String ValidTime { get; set; }

    /// <summary>
    /// 打卡日期
    /// </summary>
    public String ValidCompID { get; set; }

    /// <summary>
    /// 打卡時間
    /// </summary>
    public String ValidID { get; set; }

    /// <summary>
    /// 打卡時間計算用
    /// </summary>
    public String ValidName { get; set; }

    /// <summary>
    /// 班表值勤開始時間
    /// </summary>
    public String RemedyReasonID { get; set; }

    /// <summary>
    /// 班表值勤結束時間
    /// </summary>
    public String RemedyReasonCN { get; set; }

    /// <summary>
    /// 班表午休開始時間
    /// </summary>
    public String RemedyPunchTime { get; set; }

    /// <summary>
    /// 班表午休結束時間
    /// </summary>
    public String Remedy_MAFT10_FLAG { get; set; }

    /// <summary>
    /// 異常註記
    /// </summary>
    public String Remedy_AbnormalFlag { get; set; }

    /// <summary>
    /// 處理訊息
    /// </summary>
    public String Remedy_AbnormalReasonID { get; set; }

    /// <summary>
    /// 提醒訊息
    /// </summary>
    public String Remedy_AbnormalReasonCN { get; set; }

    /// <summary>
    /// 提醒訊息(提醒加班)
    /// </summary>
    public String Remedy_AbnormalDesc { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動日期
    /// </summary>
    public String LastChgDate { get; set; }

    /// <summary>
    /// 打卡日期-查詢起始
    /// </summary>
    public String StartPunchDate { get; set; }

    /// <summary>
    /// 打卡日期-查詢結束
    /// </summary>
    public String EndPunchDate { get; set; }

    /// <summary>
    /// 功能組織(給HROtherFlowLog用的)
    /// </summary>
    public String FlowOrganID { get; set; }
}