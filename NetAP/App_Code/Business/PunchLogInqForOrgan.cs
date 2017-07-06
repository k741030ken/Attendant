using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// PunchLogInqForOrgan 的摘要描述
/// </summary>
public class PunchLogInqForOrgan //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;


    /// <summary>
    /// GridDataFormat
    /// 打卡查詢--單位
    /// 將Grid資料作格式化
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<PunchConfirmDataModel> GridDataFormat(List<PunchConfirmBean> dbDataList)
    {
        var result = new List<PunchConfirmDataModel>();
        foreach (var item in dbDataList)
        {
            var data = new PunchConfirmDataModel();
            data.AbnormalType_Show = "異常" + item.AbnormalType;
            data.PunchDate = item.PunchSDate;
            data.PunchTime = item.PunchTime;
            switch (item.ConfirmPunchFlag)
            {
                case "1": { data.ConfirmPunchFlag_Show = "出勤打卡"; break; }
                case "2": { data.ConfirmPunchFlag_Show = "退勤打卡"; break; }
                case "3": { data.ConfirmPunchFlag_Show = "午休開始"; break; }
                case "4": { data.ConfirmPunchFlag_Show = "午休結束"; break; }
            }
            switch (item.Source)
            {
                case "A": { data.Source_Show = "APP"; break; }
                case "B": { data.Source_Show = "永豐雲"; break; }
                case "C": { data.Source_Show = "送簽中"; break; }
            }
            data.OrganName_Show = item.DeptName + "/" + item.OrganName;
            data.EmpID = item.EmpID;
            data.EmpName = item.EmpNameN;
            data.Remedy_AbnormalReasonCN = item.Remedy_AbnormalReasonCN;
            switch (item.RotateFlag) 
            {
                case "0": { data.RotateFlag = "否"; break; }
                case "1": { data.RotateFlag = "是"; break; }
            }
            result.Add(data);
        }
        return result;
    }

    /// <summary>
    /// SelectOrganForAll
    /// 打卡查詢--單位
    /// 取得行政組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectOrganForAll(PunchConfirmModel model, out List<OrganListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<OrganListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectOrganForAllSql(ref sb);
                try
                {
                    datas = conn.Query<OrganListModel>(sb.ToString(), model).ToList();
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
    /// SelectFlowOrganForAll
    /// 打卡查詢--單位
    /// 取得功能組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectFlowOrganForAll(PunchConfirmModel model, out List<FlowOrganListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<FlowOrganListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectFlowOrganForAllSql(ref sb);
                try
                {
                    datas = conn.Query<FlowOrganListModel>(sb.ToString(), model).ToList();
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
    /// SelectPerson
    /// 打卡查詢--單位
    /// 取得公司人員資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectPerson(PunchConfirmModel model, out PunchConfirmBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchConfirmBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                PunchConfirmBean dataBean = new PunchConfirmBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPersonSql(ref sb);
                try
                {
                    datas = conn.Query<PunchConfirmBean>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null)
                {
                    throw new Exception("查無資料!");
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
    /// 打卡查詢-個人
    /// 取得Grid資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectPunchConfirmForAll(PunchConfirmModel model,string orgType, string searchType, out List<PunchConfirmBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<PunchConfirmBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchConfirmBean dataBean = new PunchConfirmBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    EmpNameN = model.EmpName,
                    OrganID = model.OrganID,
                    FlowOrganID = model.FlowOrganID,
                    PunchSDate = model.PunchSDate,
                    PunchEDate = model.PunchEDate,
                    PunchSTime = model.PunchSTime,
                    PunchETime = model.PunchETime,
                    ConfirmPunchFlag = model.ConfirmPunchFlag,
                    ConfirmStatus = model.ConfirmStatus,
                    Remedy_AbnormalFlag = model.Remedy_AbnormalFlag
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchConfirmForAllSql(dataBean,orgType, searchType, ref sb);
                try
                {
                    datas = conn.Query<PunchConfirmBean>(sb.ToString(), dataBean).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null || datas.Count == 0)
                {
                    throw new Exception("查無資料!");
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