using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

/// <summary>
/// TestLogic 的摘要描述
/// </summary>
public class WorkTime //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    #region "下拉選單"
    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadCompID(WorkTimeViewModel model, out List<DropDownListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DropDownListMobel>(WorkTimeSql.LoadCompID(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadComp(WorkTimeViewModel model, out List<DropDownListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DropDownListMobel>(WorkTimeSql.LoadComp(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadBothComp(WorkTimeViewModel model, out List<DropDownListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DropDownListMobel>(WorkTimeSql.LoadBothComp(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }


    /// <summary>
    /// 取得行政組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadOrgan(WorkTimeViewModel model, out List<OrganListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<OrganListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<OrganListMobel>(WorkTimeSql.LoadOrgan(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得功能組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadFlowOrgan(WorkTimeViewModel model, out List<FlowOrganListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<FlowOrganListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<FlowOrganListMobel>(WorkTimeSql.LoadFlowOrgan(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得班別下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadWTID(EmpWorkTimeModel model, out List<DropDownListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DropDownListMobel>(WorkTimeSql.LoadWTID(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得值勤班別下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadGuardWTID(WorkTimeViewModel model, out List<WorkTimeDDLMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<WorkTimeDDLMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<WorkTimeDDLMobel>(WorkTimeSql.LoadGuardWTID(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得個人工作地點
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadPerWorkSite(EmpGuardWorkTimeModel model, out List<WorkSiteMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<WorkSiteMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<WorkSiteMobel>(WorkTimeSql.LoadPerWorkSite(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得值勤人員列表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadDutyEmpID(WorkTimeViewModel model, out List<EmpListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<EmpListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<EmpListMobel>(WorkTimeSql.LoadDutyEmpID(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得值勤單位下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadDutyOrgan(EmpWorkTimeModel model, out List<DropDownListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DropDownListMobel>(WorkTimeSql.LoadDutyOrgan(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }
    #endregion

    #region "個人班表EmpWorkTime"
    /// <summary>
    /// 查詢EmpWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadEmpWorkTimeGridData(EmpWorkTimeModel model, out List<QueryListBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<QueryListBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<QueryListBean>(WorkTimeSql.LoadEmpWorkTimeGridData(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 查詢EmpWorkTime資料(新增頁)
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadEmpWorkTimeGridData_Add(EmpWorkTimeModel model, out List<QueryListBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<QueryListBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<QueryListBean>(WorkTimeSql.LoadEmpWorkTimeGridData_Add(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 查詢EmpWorkTimeLog資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadEmpWorkTimeLogGridData(EmpWorkTimeModel model, out List<QueryListBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<QueryListBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<QueryListBean>(WorkTimeSql.LoadEmpWorkTimeLogGridData(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }


    /// <summary>
    /// 新增EmpWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool AddEmpWorkTime(List<EmpWorkTimeBean> dataBean, out string msg)
    {
        bool result = false;
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in dataBean)
                        {
                            var count = conn.Query<EmpWorkTimeBean>(WorkTimeSql.SelectEmpWorkTime(), item, trans).Count(); //執行查詢，結果回傳至TestBean物件
                            if (count == 0)
                            {
                                conn.Execute(WorkTimeSql.AddEmpWorkTime(), item, trans); //執行新增，並做Transaction機制
                            }
                            else
                            {
                                conn.Execute(WorkTimeSql.UpdateEmpWorkTime(), item, trans); //執行修改，並做Transaction機制
                            }
                        }
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        result = false;
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 刪除EmpWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool DeleteEmpWorkTime(List<EmpWorkTimeBean> dataBean, out string msg)
    {
        bool result = false;
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(WorkTimeSql.DeleteEmpWorkTime(), dataBean, trans); //執行修改，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        result = false;
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }
    #endregion

    #region "行事曆"
    /// <summary>
    /// 取得單位工作地點
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadOrgWorkSite(EmpGuardWorkTimeModel model, out List<WorkSiteMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<WorkSiteMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<WorkSiteMobel>(WorkTimeSql.LoadOrgWorkSite(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得Calendar假日資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadHoliday(WorkTimeViewModel model, out List<CalendarListMobel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<CalendarListMobel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    if (model.WorkSite != "台灣")
                    {
                        datas = conn.Query<CalendarListMobel>(WorkTimeSql.LoadOverSeaHoliday(), model).ToList();
                    }
                    else
                    {
                        datas = conn.Query<CalendarListMobel>(WorkTimeSql.LoadHoliday(), model).ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }


    /// <summary>
    /// 取得假單檔
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetVacInfo(WorkTimeViewModel model, out int cnt, out string msg)
    {
        bool result = false;
        msg = "";
        cnt = 0;
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    cnt = Convert.ToInt32(conn.ExecuteScalar(WorkTimeSql.GetVacInfo(), model));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得值勤日期行事曆
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadGuardCalendar(EmpGuardWorkTimeModel model, out List<DutyListBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DutyListBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DutyListBean>(WorkTimeSql.LoadGuardCalendar(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }
    #endregion

    #region "值勤表EmpGuardWorkTime"
    /// <summary>
    /// 查詢EmpGuardWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadEmpGuardWorkTimeGridData(EmpGuardWorkTimeModel model, out List<DutyQueryListBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DutyQueryListBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<DutyQueryListBean>(WorkTimeSql.LoadEmpGuardWorkTimeGridData(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 查詢EmpGuardWorkTime資料(修改頁)
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetEmpGuardWorkTime(EmpGuardWorkTimeModel model, out List<EmpGuardWorkTimeBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<EmpGuardWorkTimeBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    datas = conn.Query<EmpGuardWorkTimeBean>(WorkTimeSql.GetEmpGuardWorkTime(), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 新增EmpGuardWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool AddEmpGuardWorkTime(EmpGuardWorkTimeBean dataBean, out string msg)
    {
        bool result = false;
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var count = conn.Query<EmpGuardWorkTimeBean>(WorkTimeSql.SelectEmpGuardWorkTime(), dataBean, trans).Count(); //執行查詢，結果回傳至TestBean物件
                        if (count == 0)
                        {
                            conn.Execute(WorkTimeSql.AddEmpGuardWorkTime(), dataBean, trans); //執行修改，並做Transaction機制
                        }
                        else
                        {
                            throw new Exception("資料已存在，請勿重複新增!!");
                        }
                        
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        result = false;
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 修改EmpGuardWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool UpdateEmpGuardWorkTime(EmpGuardWorkTimeBean dataBean, out string msg)
    {
        bool result = false;
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(WorkTimeSql.UpdateEmpGuardWorkTime(), dataBean, trans); //執行修改，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        result = false;
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 刪除EmpGuardWorkTime資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool DeleteEmpGuardWorkTime(List<EmpGuardWorkTimeBean> dataBean, out string msg)
    {
        bool result = false;
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(WorkTimeSql.DeleteEmpGuardWorkTime(), dataBean, trans); //執行修改，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        result = false;
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 查詢值班人數
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectDutyCnt(EmpGuardWorkTimeBean dataBean, out int cnt, out string msg)
    {
        bool result = false;
        msg = "";
        cnt = 0;
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();

                try
                {
                    cnt = Convert.ToInt32(conn.ExecuteScalar(WorkTimeSql.SelectDutyCnt(), dataBean));
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }
    #endregion

    /// <summary>
    /// 取得分行註記
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="strField">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LoadOrgBranchMark(WorkTimeViewModel model, out string strField, out string msg)
    {
        bool result = false;
        msg = "";
        strField = "";

        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                try
                {
                    var objField = conn.ExecuteScalar(WorkTimeSql.LoadOrgBranchMark(), model);

                    if (objField != null)
                    {
                        strField = objField.ToString().Trim();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }
}