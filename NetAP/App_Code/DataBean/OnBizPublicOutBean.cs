using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class OnBizPublicOutBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 查詢條件
    /// </summary>
    public String OBUseType { get; set; }

    /// <summary>
    /// 人員公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 人員公司ID
    /// </summary>
    public String oldCompID { get; set; }

    /// <summary>
    /// 公司名稱
    /// </summary>
    public String CompName { get; set; }

    /// <summary>
    /// 公司別
    /// </summary>
    public String selectCompID { get; set; }

    /// <summary>
    /// FlowType
    /// </summary>
    public String FlowType { get; set; }

    /// <summary>
    /// 人員公司
    /// </summary>
    public String CompID_Name { get; set; }

    /// <summary>
    /// 公出人員ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 公出人員ID
    /// </summary>
    public String oldEmpID { get; set; }

    /// <summary>
    /// 公出人員姓名
    /// </summary>
    public String EmpNameN { get; set; }

    /// <summary>
    /// 公出人員
    /// </summary>
    public String EmpID_Name { get; set; }

    /// <summary>
    /// 公出人員EMail
    /// </summary>
    public String EmpID_EMail { get; set; }

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
    /// 登入日期
    /// </summary>
    public String oldWriteDate { get; set; }

    /// <summary>
    /// 登入人姓名
    /// </summary>
    public String WriterName { get; set; }

    /// <summary>
    /// 登入人
    /// </summary>
    public String WriterID_Name { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String FormSeq { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String oldFormSeq { get; set; }

    /// <summary>
    /// FlowCaseID(X)
    /// </summary>
    public String FlowCaseID { get; set; }

    /// <summary>
    /// FlowLogID(X)
    /// </summary>
    public String FlowLogID { get; set; }

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
    /// OBFormStatus(X)
    /// </summary>
    public String OBFormStatusName { get; set; }

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
    /// ValidIDEMail
    /// </summary>
    public String ValidID_EMail { get; set; }

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
    /// 公出單位ID
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 公出單位名稱
    /// </summary>
    public String OrganName { get; set; }

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
    /// 公出日期
    /// </summary>
    public String VisitBeginDate { get; set; }

    /// <summary>
    /// 公出開始時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 公出開始時間(HH)
    /// </summary>
    public String BeginTimeH { get; set; }

    /// <summary>
    /// 公出開始時間(MM)
    /// </summary>
    public String BeginTimeM { get; set; }

    /// <summary>
    /// 公出日期
    /// </summary>
    public String VisitEndDate { get; set; }

    /// <summary>
    /// 公出結束時間
    /// </summary>
    public String EndTime { get; set; }

    /// <summary>
    /// 公出結束時間(HH)
    /// </summary>
    public String EndTimeH { get; set; }

    /// <summary>
    /// 公出結束時間(MM)
    /// </summary>
    public String EndTimeM { get; set; }

    /// <summary>
    /// 職務代理人員ID
    /// </summary>
    public String DeputyID { get; set; }

    /// <summary>
    /// 職務代理人員姓名
    /// </summary>
    public String DeputyName { get; set; }

    /// <summary>
    /// 職務代理人員EMail
    /// </summary>
    public String DeputyID_EMail { get; set; }

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
    /// 洽辦事由
    /// </summary>
    public String VisitReasonCN { get; set; }

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
    /// 最後異動公司
    /// </summary>
    public String LastChgCompID_Name { get; set; }

    /// <summary>
    /// 最後異動人員ID
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動人員姓名
    /// </summary>
    public String LastChgName { get; set; }

    /// <summary>
    /// 最後異動人員
    /// </summary>
    public String LastChgID_Name { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }

}

public class HRFlowTypeDefineBean
{
    /// <summary>
    /// SystemID
    /// </summary>
    public String SystemID { get; set; }

    /// <summary>
    /// FlowCode
    /// </summary>
    public String FlowCode { get; set; }

    /// <summary>
    /// FlowType
    /// </summary>
    public String FlowType { get; set; }

    /// <summary>
    /// FlowTypeFlag
    /// </summary>
    public String FlowTypeFlag { get; set; }

    /// <summary>
    /// FlowSN
    /// </summary>
    public String FlowSN { get; set; }

}




