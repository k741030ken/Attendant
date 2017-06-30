using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class EmpWorkTimeBean
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
    /// 班別公司別
    /// </summary>
    public String WTCompID { get; set; }

    /// <summary>
    /// 班別
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 生效時間
    /// </summary>
    public String StartDate { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者員編
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }

}

public class QueryListBean
{
    /// <summary>
    /// 公司別
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 公司名稱
    /// </summary>
    public String CompName { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String NameN { get; set; }

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

    /// <summary>
    /// 班別代碼
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 班別時間
    /// </summary>
    public String WorkTime { get; set; }

    /// <summary>
    /// 異動原因
    /// </summary>
    public String Action { get; set; }

    /// <summary>
    /// 輪班註記
    /// </summary>
    public String RotateFlag { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者員編
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }
}

public class CalendarBean
{
    /// <summary>
    /// 日期
    /// </summary>
    public String SysDate { get; set; }

    /// <summary>
    /// 1-假日, 0-非假日
    /// </summary>
    public String HolidayOrNot { get; set; }
}

public class DutyListBean
{
    /// <summary>
    /// 值勤日期
    /// </summary>
    public DateTime DutyDate { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 值勤時間
    /// </summary>
    public String DutyTime { get; set; }

    /// <summary>
    /// 班別
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 最後異動者員編
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public DateTime LastChgDate { get; set; }
}

public class EmpGuardWorkTimeBean
{
    /// <summary>
    /// 值勤日期
    /// </summary>
    public String DutyDate { get; set; }

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
    public String NameN { get; set; }

    /// <summary>
    /// 值勤公司別
    /// </summary>
    public String DutyCompID { get; set; }

    /// <summary>
    /// 值勤部門
    /// </summary>
    public String DutyDeptID { get; set; }

    /// <summary>
    /// 值勤科組課
    /// </summary>
    public String DutyOrganID { get; set; }

    /// <summary>
    /// 班別公司別
    /// </summary>
    public String WTCompID { get; set; }

    /// <summary>
    /// 班別
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 值勤開始時間
    /// </summary>
    public String WTBeginTime { get; set; }

    /// <summary>
    /// 值勤結束時間
    /// </summary>
    public String WTEndTime { get; set; }

    /// <summary>
    /// 分行註記
    /// </summary>
    public String BranchFlag { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者員編
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }
}

public class DutyQueryListBean
{
    /// <summary>
    /// 值勤日期
    /// </summary>
    public String DutyDate { get; set; }

    /// <summary>
    /// 值勤公司別
    /// </summary>
    public String DutyCompID { get; set; }

    /// <summary>
    /// 值勤公司
    /// </summary>
    public String DutyCompName { get; set; }

    /// <summary>
    /// 值勤單位別
    /// </summary>
    public String DutyOrganID { get; set; }

    /// <summary>
    /// 值勤單位
    /// </summary>
    public String DutyOrganName { get; set; }

    /// <summary>
    /// 員工公司別
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 員工公司
    /// </summary>
    public String CompName { get; set; }

    /// <summary>
    /// 員編
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 員工處代碼
    /// </summary>
    public String OrgType { get; set; }

    /// <summary>
    /// 員工處名稱
    /// </summary>
    public String OrgTypeName { get; set; }

    /// <summary>
    /// 員工部代碼
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 員工部名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 員工科組課代碼
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 員工科組課名稱
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 班別代碼
    /// </summary>
    public String WTID { get; set; }

    /// <summary>
    /// 值勤時間
    /// </summary>
    public String DutyTime { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者員編
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動時間
    /// </summary>
    public String LastChgDate { get; set; }
}