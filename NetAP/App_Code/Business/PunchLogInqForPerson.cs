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
/// OnBizRegInquire 的摘要描述
/// </summary>
public class PunchLogInqForPerson //與交易同名
{
    private static string _attendantDBName = "AattendantDB";
    private static string _attendantFlowID = "AattendantDB";
    private static string _eHRMSDB_ITRD = "eHRMSDB";

    /// <summary>
    /// GridDataFormat
    /// 公出查詢
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
            data.Remedy_AbnormalReasonCN = item.Remedy_AbnormalReasonCN;
            data.Remedy_AbnormalDesc = item.Remedy_AbnormalDesc;
            result.Add(data);
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
    public static bool SelectPunchConfirm(PunchConfirmModel model,string searchType, out List<PunchConfirmBean> datas, out string msg)
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
                    PunchSDate = model.PunchSDate,
                    PunchEDate = model.PunchEDate,
                    ConfirmPunchFlag = model.ConfirmPunchFlag,
                    ConfirmStatus = model.ConfirmStatus,
                    Remedy_AbnormalFlag = model.Remedy_AbnormalFlag
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchConfirmSql(dataBean,searchType, ref sb);
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