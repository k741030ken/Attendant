using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於顯示在view上的資料暫存(資料多以畫面顯示user的格式為主)
/// </summary>
public class OnBizReqAddesModel //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 登入人員公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 公出人員ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 公出人員姓名
    /// </summary>
    public String EmpNameN { get; set; }

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
    /// 登入人姓名
    /// </summary>
    public String OBWriterName { get; set; }

    /// <summary>
    /// 編號(CompID+EmpID+WriteDate+FormSeq最大值)
    /// </summary>
    public String OBFormSeq { get; set; }

    /// <summary>
    /// 公出號碼單
    /// </summary>
    public String OBVisitFormNo { get; set; }

    /// <summary>
    /// 公出人員公司名稱
    /// </summary>
    public String OBCompName { get; set; }

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
    /// 公出日期
    /// </summary>
    public String OBVisitBeginDate { get; set; }

    /// <summary>
    /// 公出開始時間
    /// </summary>
    public String OBBeginTime { get; set; }

    /// <summary>
    /// 公出日期
    /// </summary>
    public String OBVisitEndDate { get; set; }

    /// <summary>
    /// 公出結束時間
    /// </summary>
    public String OBEndTime { get; set; }

    /// <summary>
    /// 職務代理人員ID
    /// </summary>
    public String OBDeputyID { get; set; }

    /// <summary>
    /// 職務代理人員姓名
    /// </summary>
    public String OBDeputyName { get; set; }

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
    /// 外部地點ID
    /// </summary>
    public String OBExterLocationID { get; set; }

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
    public String OBLastChgCompID { get; set; }

    /// <summary>
    /// 最後異動公司名稱
    /// </summary>
    public String OBLastChgCompName { get; set; }

    /// <summary>
    /// 最後異動人員ID
    /// </summary>
    public String OBLastChgID { get; set; }

    /// <summary>
    /// 最後異動人員姓名
    /// </summary>
    public String OBLastChgNanme { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String OBLastChgDate { get; set; }
}

public class ddlPersonModel
{
    /// <summary>
    /// 公司
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 公司名稱
    /// </summary>
    public String CompName { get; set; }

    /// <summary>
    /// ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 部門
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 部門名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 單位(功能)
    /// </summary>
    public String FlowOrganID { get; set; }

    /// <summary>
    /// 單位名稱(功能)
    /// </summary>
    public String FlowOrganName { get; set; }

    /// <summary>
    /// 單位
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 單位名稱
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 職稱ID
    /// </summary>
    public String TitleID { get; set; }

    /// <summary>
    /// 職稱
    /// </summary>
    public String TitleName { get; set; }

    /// <summary>
    /// 職位ID
    /// </summary>
    public String PositionID { get; set; }

    /// <summary>
    /// 職位
    /// </summary>
    public String Position { get; set; }

    /// <summary>
    /// 工作性質ID
    /// </summary>
    public String WorkTypeID { get; set; }

    /// <summary>
    /// 工作性質
    /// </summary>
    public String WorkType { get; set; }

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<PersonGridData> PersonGridDataList { get; set; }public class PersonGridData //請取有意義的名稱+Model結尾
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
}

}

