using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於顯示在view上的資料暫存(資料多以畫面顯示user的格式為主)
/// </summary>
public class OnBizPublicOutModel //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 查詢條件
    /// </summary>
    public String OBUseType { get; set; }

    /// <summary>
    /// 登入人員公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 登入人員公司ID
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
    public String OBWriteDate { get; set; }

    /// <summary>
    /// 登入時間
    /// </summary>
    public String OBWriteTime { get; set; }

    /// <summary>
    /// 登入人ID
    /// </summary>
    public String OBWriterID { get; set; }

    /// <summary>
    /// 登入日期
    /// </summary>
    public String oldOBWriteDate { get; set; }

    /// <summary>
    /// 登入人姓名
    /// </summary>
    public String OBWriterName { get; set; }

    /// <summary>
    /// 登入人
    /// </summary>
    public String OBWriterID_Name { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String OBFormSeq { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String oldOBFormSeq { get; set; }

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
    public String OBVisitFormNo { get; set; }

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
    public String OBDeptID { get; set; }

    /// <summary>
    /// 公出部門名稱
    /// </summary>
    public String OBDeptName { get; set; }

    /// <summary>
    /// 公出單位ID
    /// </summary>
    public String OBOrganID { get; set; }

    /// <summary>
    /// 公出單位名稱
    /// </summary>
    public String OBOrganName { get; set; }

    /// <summary>
    /// 公出人員工作性質ID
    /// </summary>
    public String OBWorkTypeID { get; set; }

    /// <summary>
    /// 公出人員工作性質代號
    /// </summary>
    public String OBWorkType { get; set; }

    /// <summary>
    /// 公出單位ID(功能)
    /// </summary>
    public String OBFlowOrganID { get; set; }

    /// <summary>
    /// 公出單位名稱(功能)
    /// </summary>
    public String OBFlowOrganName { get; set; }

    /// <summary>
    /// 公出人員職稱ID
    /// </summary>
    public String OBTitleID { get; set; }

    /// <summary>
    /// 公出人員職稱
    /// </summary>
    public String OBTitleName { get; set; }

    /// <summary>
    /// 公出人員職位ID
    /// </summary>
    public String OBPositionID { get; set; }

    /// <summary>
    /// 公出人員職位
    /// </summary>
    public String OBPosition { get; set; }

    /// <summary>
    /// 公出人員連絡電話一
    /// </summary>
    public String OBTel_1 { get; set; }

    /// <summary>
    /// 公出人員連絡電話二
    /// </summary>
    public String OBTel_2 { get; set; }

    /// <summary>
    /// 公出日期(起)
    /// </summary>
    public String OBVisitBeginDate { get; set; }

    /// <summary>
    /// 公出開始時間
    /// </summary>
    public String OBBeginTime { get; set; }

    /// <summary>
    /// 公出開始時間(HH)
    /// </summary>
    public String OBBeginTimeH { get; set; }

    /// <summary>
    /// 公出開始時間(MM)
    /// </summary>
    public String OBBeginTimeM { get; set; }

    /// <summary>
    /// 公出日期(迄)
    /// </summary>
    public String OBVisitEndDate { get; set; }

    /// <summary>
    /// 公出結束時間
    /// </summary>
    public String OBEndTime { get; set; }

    /// <summary>
    /// 公出結束時間(HH)
    /// </summary>
    public String OBEndTimeH { get; set; }

    /// <summary>
    /// 公出結束時間(MM)
    /// </summary>
    public String OBEndTimeM { get; set; }

    /// <summary>
    /// 職務代理人員ID
    /// </summary>
    public String OBDeputyID { get; set; }

    /// <summary>
    /// 職務代理人員姓名
    /// </summary>
    public String OBDeputyName { get; set; }

    /// <summary>
    /// 職務代理人員EMail
    /// </summary>
    public String DeputyID_EMail { get; set; }

    /// <summary>
    /// 前往地點
    /// </summary>
    public String OBLocationType { get; set; }

    /// <summary>
    /// 內部地點ID
    /// </summary>
    public String OBInterLocationID { get; set; }

    /// <summary>
    /// 內部地點
    /// </summary>
    public String OBInterLocationName { get; set; }

    /// <summary>
    /// 外部地點
    /// </summary>
    public String OBExterLocationName { get; set; }

    /// <summary>
    /// 連絡人姓名
    /// </summary>
    public String OBVisiterName { get; set; }

    /// <summary>
    /// 連絡人電話
    /// </summary>
    public String OBVisiterTel { get; set; }

    /// <summary>
    /// 洽辦事由ID
    /// </summary>
    public String OBVisitReasonID { get; set; }

    /// <summary>
    /// 洽辦事由
    /// </summary>
    public String OBVisitReasonCN { get; set; }

    /// <summary>
    /// 其他說明
    /// </summary>
    public String OBVisitReasonDesc { get; set; }

    /// <summary>
    /// 最後異動公司ID
    /// </summary>
    public String OBLastChgComp { get; set; }

    /// <summary>
    /// 最後異動公司名稱
    /// </summary>
    public String OBLastChgCompName { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String OBLastChgCompID_Name { get; set; }

    /// <summary>
    /// 最後異動人員ID
    /// </summary>
    public String OBLastChgID { get; set; }

    /// <summary>
    /// 最後異動人員姓名
    /// </summary>
    public String OBLastChgName { get; set; }

    /// <summary>
    /// 最後異動人員
    /// </summary>
    public String OBLastChgID_Name { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String OBLastChgDate { get; set; }

    /// <summary>
    /// 畫面名稱
    /// </summary>
    public String PageName { get; set; }

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<PersonGridData> PersonGridDataList { get; set; }

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<gridDataModel> SelectGridDataList { get; set; }
}

public class PersonGridData //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 員工編號
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 員編姓名
    /// </summary>
    public String Emp_NameN { get; set; }

    /// <summary>
    /// 內部地點ID
    /// </summary>
    public String OBInterLocationID { get; set; }

    /// <summary>
    /// 內部地點
    /// </summary>
    public String OBInterLocationName { get; set; }
}

public class gridDataModel
{
    /// <summary>
    /// 人員公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 登入日期
    /// </summary>
    public String OBWriteDate { get; set; }

    /// <summary>
    /// OBFormSeq
    /// </summary>
    public String OBFormSeq { get; set; }

    /// <summary>
    /// OBFormStatus(X)
    /// </summary>
    public String OBFormStatus { get; set; }

    /// <summary>
    /// OBFormStatus(X)
    /// </summary>
    public String OBFormStatusName { get; set; }

    /// <summary>
    /// FlowCaseID(X)
    /// </summary>
    public String FlowCaseID { get; set; }

    /// <summary>
    /// FlowLogID(X)
    /// </summary>
    public String FlowLogID { get; set; }

    /// <summary>
    /// 簽核人員ID
    /// </summary>
    public String ValidID { get; set; }

    /// <summary>
    /// 簽核人員姓名
    /// </summary>
    public String ValidName { get; set; }

    /// <summary>
    /// 簽核人員
    /// </summary>
    public String ValidID_Name { get; set; }

    /// <summary>
    /// 公出人員ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 公出人員姓名
    /// </summary>
    public String EmpNameN { get; set; }

    /// <summary>
    /// 公出人員
    /// </summary>
    public String EmpID_NameN { get; set; }

    /// <summary>
    /// 職務代理人員ID
    /// </summary>
    public String DeputyID { get; set; }

    /// <summary>
    /// 職務代理人員姓名
    /// </summary>
    public String DeputyName { get; set; }

    /// <summary>
    /// 職務代理人員
    /// </summary>
    public String DeputyID_Name { get; set; }

    /// <summary>
    /// 公出日期
    /// </summary>
    public String VisitBeginDate { get; set; }

    /// <summary>
    /// 公出開始時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 公出日期
    /// </summary>
    public String VisitEndDate { get; set; }

    /// <summary>
    /// 公出結束時間
    /// </summary>
    public String EndTime { get; set; }

    /// <summary>
    /// 洽辦事由ID
    /// </summary>
    public String VisitReasonID { get; set; }

    /// <summary>
    /// 洽辦事由
    /// </summary>
    public String VisitReasonCN { get; set; }

}

public class DropDownListModel
{
    /// <summary>
    /// 下拉選單ID
    /// </summary>
    public String DataValue { get; set; }

    /// <summary>
    /// 下拉選單Value
    /// </summary>
    public String DataText { get; set; }
}

public class OrganListModel
{
    /// <summary>
    /// 處代碼
    /// </summary>
    public String OrgType { get; set; }

    /// <summary>
    /// 處名稱
    /// </summary>
    public String OrgTypeName { get; set; }

    /// <summary>
    /// 部代碼
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 部名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 科組課代碼
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 科組課名稱
    /// </summary>
    public String OrganName { get; set; }
}

public class FlowOrganListModel
{
    /// <summary>
    /// 業務類別
    /// </summary>
    public String BusinessType { get; set; }

    /// <summary>
    /// RoleCode
    /// </summary>
    public String RoleCode { get; set; }

    /// <summary>
    /// 上階部門
    /// </summary>
    public String UpOrganID { get; set; }

    /// <summary>
    /// 部代碼
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 單位代碼
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 單位名稱
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 單位階層
    /// </summary>
    public String OrganLevel { get; set; }
}

