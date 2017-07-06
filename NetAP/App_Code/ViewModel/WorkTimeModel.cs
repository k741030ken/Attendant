using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於顯示在view上的資料暫存(資料多以畫面顯示user的格式為主)
/// </summary>
/// 
public class WorkTimeViewModel
{
    /// <summary>
    /// 登入者公司
    /// </summary>
    public String UserComp { get; set; }

    /// <summary>
    /// 登入者員編
    /// </summary>
    public String UserID { get; set; }

    /// <summary>
    /// 公司別
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 科組課
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// FlowType
    /// </summary>
    public String FlowType { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    public String RenderDate { get; set; }

    /// <summary>
    /// 工作地點
    /// </summary>
    public String WorkSite { get; set; }
}
public class EmpWorkTimeModel
{
    /// <summary>
    /// 公司別
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 部門
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 科組課
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 科組課
    /// </summary>
    public String FlowOrganID { get; set; }

    /// <summary>
    /// 班別
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// FlowType
    /// </summary>
    public bool AllSearch { get; set; }
}

public class EmpGuardWorkTimeModel
{
    /// <summary>
    /// 公司別
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 部門
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 科組課
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 單位類型
    /// </summary>
    public String BranchFlag { get; set; }

    /// <summary>
    /// 值勤日期
    /// </summary>
    public String DutyDate { get; set; }

    /// <summary>
    /// 值勤班別
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 值勤月份
    /// </summary>
    public String DutyDateMonth { get; set; }

    /// <summary>
    /// 值勤年份
    /// </summary>
    public String DutyDateYear { get; set; }
}

public class OrganListMobel
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

public class FlowOrganListMobel
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

public class EmpListMobel
{
    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String EmpName { get; set; }
}

public class CalendarListMobel
{
    /// <summary>
    /// 日期
    /// </summary>
    public DateTime SysDate { get; set; }
}

public class WorkSiteMobel
{
    /// <summary>
    /// 工作地點
    /// </summary>
    public String WorkSiteID { get; set; }

    /// <summary>
    /// 工作地點
    /// </summary>
    public String WorkSiteName { get; set; }
}

public class WorkTimeDDLMobel
{
    /// <summary>
    /// 班別代碼
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 班別
    /// </summary>
    public String WorkTime { get; set; }

    /// <summary>
    /// 班別名稱
    /// </summary>
    public String Remark { get; set; }

    /// <summary>
    /// 上下班時間
    /// </summary>
    public String GuardTime { get; set; }

    /// <summary>
    /// 上班時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 下班時間
    /// </summary>
    public String EndTime { get; set; }
}

public class DropDownListMobel
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