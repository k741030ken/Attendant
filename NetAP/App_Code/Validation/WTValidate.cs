using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// WTValidate 的摘要描述
/// </summary>
public partial class ValidationUtility
{
    /// <summary>
    /// 查詢值班人數
    /// </summary>
    /// <param name="model"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static int SelectDutyCnt(EmpGuardWorkTimeModel model, out string msg)
    {
        var result = 0;

        msg = "";
        var viewData = new EmpGuardWorkTimeBean()
        {
            DutyDate = model.DutyDate,
            DutyCompID = model.CompID,
            DutyDeptID = model.DeptID,
            DutyOrganID = model.OrganID,
            BranchFlag = model.BranchFlag,
            WTCompID = model.CompID,
            WTID = model.WTID
        };

        WorkTime.SelectDutyCnt(viewData, out result, out msg);
        return result;
    }


    /// <summary>
    /// 查詢有無請假
    /// </summary>
    /// <param name="EmpID"></param>
    /// <param name="VacDate"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public static int GetVacInfo(string EmpID, string VacDate, out string msg)
    {
        var result = 0;
        msg = "";

        var viewData = new WorkTimeViewModel()
        {
            EmpID = EmpID,
            RenderDate = VacDate
        };

        WorkTime.GetVacInfo(viewData, out result, out msg);
        return result;
    }

}