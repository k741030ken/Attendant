using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// OnBizReqAppdOperationBean 的摘要描述
/// </summary>
public class OnBizReqAppdOperationBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 人員公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 公出人員公司名稱
    /// </summary>
    public String CompName { get; set; }

    /// <summary>
    /// 公出人員公司ID + 名稱
    /// </summary>
    public String CompID_Name { get; set; }

    /// <summary>
    /// 公出人員ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 公出人員姓名
    /// </summary>
    public String EmpNameN { get; set; }

    /// <summary>
    /// 公出人員ID + 姓名
    /// </summary>
    public String EmpID_NameN { get; set; }

    /// <summary>
    /// 登入日期
    /// </summary>
    public String WriteDate { get; set; }

    /// <summary>
    /// 登入時間
    /// </summary>
    public String WriteTime { get; set; }

    /// <summary>
    /// 登入人ID
    /// </summary>
    public String WriterID { get; set; }

    /// <summary>
    /// 登入人姓名
    /// </summary>
    public String WriterName { get; set; }

    /// <summary>
    /// 登入人ID + 姓名
    /// </summary>
    public String WriterID_Name { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String FormSeq { get; set; }

    /// <summary>
    /// FlowCaseID(X)
    /// </summary>
    public String FlowCaseID { get; set; }

    /// <summary>
    /// TransactionSeq(X)
    /// </summary>
    public String TransactionSeq { get; set; }

    /// <summary>
    /// 公出號碼單
    /// </summary>
    public String VisitFormNo { get; set; }

    /// <summary>
    /// OBFormStatus(X)
    /// </summary>
    public String OBFormStatus { get; set; }

    /// <summary>
    /// ValidDate(X)
    /// </summary>
    public String ValidDate { get; set; }

    /// <summary>
    /// ValidID(X)
    /// </summary>
    public String ValidID { get; set; }

    /// <summary>
    /// ValidName(X)
    /// </summary>
    public String ValidName { get; set; }

    /// <summary>
    /// ValidID(X)
    /// </summary>
    public String ValidID_Name { get; set; }

    /// <summary>
    /// RejectReasonID(X)
    /// </summary>
    public String RejectReasonID { get; set; }

    /// <summary>
    /// RejectReasonCN(X)
    /// </summary>
    public String RejectReasonCN { get; set; }

    /// <summary>
    /// 公出部門ID
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 公出部門名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 公出部門ID + 名稱
    /// </summary>
    public String DeptID_Name { get; set; }

    /// <summary>
    /// 公出單位ID
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 公出單位名稱
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 公出單位ID + 名稱
    /// </summary>
    public String OrganID_Name { get; set; }

    /// <summary>
    /// 公出人員工作性質ID
    /// </summary>
    public String WorkTypeID { get; set; }

    /// <summary>
    /// 公出人員工作性質代號
    /// </summary>
    public String WorkType { get; set; }

    /// <summary>
    /// 公出單位ID(功能)
    /// </summary>
    public String FlowOrganID { get; set; }

    /// <summary>
    /// 公出單位名稱(功能)
    /// </summary>
    public String FlowOrganName { get; set; }

    /// <summary>
    /// 公出人員職稱ID
    /// </summary>
    public String TitleID { get; set; }

    /// <summary>
    /// 公出人員職稱
    /// </summary>
    public String TitleName { get; set; }

    /// <summary>
    /// 公出人員職位ID
    /// </summary>
    public String PositionID { get; set; }

    /// <summary>
    /// 公出人員職位
    /// </summary>
    public String Position { get; set; }

    /// <summary>
    /// 公出人員連絡電話一
    /// </summary>
    public String Tel_1 { get; set; }

    /// <summary>
    /// 公出人員連絡電話二
    /// </summary>
    public String Tel_2 { get; set; }

    /// <summary>
    /// 公出日期(起)
    /// </summary>
    public String VisitBeginDate { get; set; }

    /// <summary>
    /// 公出開始時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 公出日期(迄)
    /// </summary>
    public String VisitEndDate { get; set; }

    /// <summary>
    /// 公出結束時間
    /// </summary>
    public String EndTime { get; set; }

    /// <summary>
    /// 公出日期
    /// </summary>
    public String VisitDate { get; set; }

    /// <summary>
    /// 公出時間
    /// </summary>
    public String VisitTime { get; set; }

    /// <summary>
    /// 職務代理人員ID
    /// </summary>
    public String DeputyID { get; set; }

    /// <summary>
    /// 職務代理人員姓名
    /// </summary>
    public String DeputyName { get; set; }

    /// <summary>
    /// 職務代理人員ID + 姓名
    /// </summary>
    public String DeputyID_Name { get; set; }

    /// <summary>
    /// 前往地點
    /// </summary>
    public String LocationType { get; set; }

    /// <summary>
    /// 內部地點ID
    /// </summary>
    public String InterLocationID { get; set; }

    /// <summary>
    /// 內部地點
    /// </summary>
    public String InterLocationName { get; set; }

    /// <summary>
    /// 外部地點ID
    /// </summary>
    public String ExterLocationID { get; set; }

    /// <summary>
    /// 外部地點
    /// </summary>
    public String ExterLocationName { get; set; }

    /// <summary>
    /// 連絡人姓名
    /// </summary>
    public String VisiterName { get; set; }

    /// <summary>
    /// 連絡人電話
    /// </summary>
    public String VisiterTel { get; set; }

    /// <summary>
    /// 洽辦事由ID
    /// </summary>
    public String VisitReasonID { get; set; }

    /// <summary>
    /// 洽辦事由text
    /// </summary>
    public String VisitReasonCN { get; set; }

    /// <summary>
    /// 洽辦事由
    /// </summary>
    public String VisitReason { get; set; }

    /// <summary>
    /// 其他說明
    /// </summary>
    public String VisitReasonDesc { get; set; }

    /// <summary>
    /// 最後異動公司ID
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動公司名稱
    /// </summary>
    public String LastChgCompName { get; set; }

    /// <summary>
    /// 最後異動公司ID + 名稱
    /// </summary>
    public String LastChgComp_Name { get; set; }

    /// <summary>
    /// 最後異動人員ID
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動人員姓名
    /// </summary>
    public String LastChgNanme { get; set; }

    /// <summary>
    /// 最後異動人員ID + 姓名
    /// </summary>
    public String LastChgID_Nanme { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<CheckVisitGridDataBean> CheckVisitGridDatas { get; set; }
}

public class CheckVisitGridDataBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 公出人員公司
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 公出人員編號
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 公出人員
    /// </summary>
    public String EmpNameN { get; set; }

    /// <summary>
    /// 登錄日期
    /// </summary>
    public String WriteDate { get; set; }

    /// <summary>
    /// 代理人員
    /// </summary>
    public String DeputyID_Name { get; set; }

    /// <summary>
    /// 開始日期
    /// </summary>
    public String VisitBeginDate { get; set; }

    /// <summary>
    /// 開始時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 結束日期
    /// </summary>
    public String VisitEndDate { get; set; }

    /// <summary>
    /// 結束時間
    /// </summary>
    public String EndTime { get; set; }

    /// <summary>
    /// 洽辦事由
    /// </summary>
    public String VisitReasonCN { get; set; }

    /// <summary>
    /// 打卡紀錄確認序號
    /// </summary>
    public String FormSeq { get; set; }

    /// <summary>
    /// ValidID(X)
    /// </summary>
    public String ValidID { get; set; }

    /// <summary>
    /// FlowCaseID(X)
    /// </summary>
    public String FlowCaseID { get; set; }

    /// <summary>
    /// FlowLogID(X)
    /// </summary>
    public String FlowLogID { get; set; }

}