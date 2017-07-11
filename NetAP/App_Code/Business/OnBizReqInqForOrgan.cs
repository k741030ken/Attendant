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
/// OnBizReqInqForOrgan 的摘要描述
/// </summary>
public class OnBizReqInqForOrgan //與交易同名
{
    private static string _attendantDBName = "AattendantDB";
    private static string _attendantFlowID = "AattendantDB";
    private static string _eHRMSDB_ITRD = "eHRMSDB";

    /// <summary>
    /// SelectBothComp
    /// 公出單紀錄查詢
    /// 取得CompID下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectBothComp(OnBizPublicOutModel model, out List<DropDownListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {

                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectBothCompSql(ref sb);
                try
                {
                    datas = conn.Query<DropDownListModel>(sb.ToString(), model).ToList();
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
    /// SelectOrgan
    /// 公出單紀錄查詢
    /// 取得行政組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectOrgan(OnBizPublicOutModel model, out List<OrganListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<OrganListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectOrganSql(ref sb);
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
    /// SelectFlowOrgan
    /// 公出單紀錄查詢
    /// 取得功能組織下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectFlowOrgan(OnBizPublicOutModel model, out List<FlowOrganListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<FlowOrganListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectFlowOrganSql(ref sb);
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
    /// SelectVisitFormOrgan
    /// 公出單紀錄查詢
    /// 取得Grid資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectVisitFormOrgan(OnBizPublicOutModel model, string fieldName, out List<OnBizPublicOutBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<OnBizPublicOutBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    OrganID = model.OBOrganID,
                    FlowOrganID = model.OBFlowOrganID,
                    VisitBeginDate = model.OBVisitBeginDate,
                    VisitEndDate = model.OBVisitEndDate
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectVisitFormOrganSql(dataBean, fieldName, ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), dataBean).ToList();
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