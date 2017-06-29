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
public class OnBizRegInquire //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// 公出查詢
    /// 取得Grid資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectVisitForm(OnBizPublicOutModel model, out List<OnBizPublicOutBean> datas, out string msg)
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
                    OBUseType = model.OBUseType,
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    VisitBeginDate = model.OBVisitBeginDate,
                    VisitEndDate = model.OBVisitEndDate,
                    OBFormStatus = model.OBFormStatus

                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectVisitFormsql(dataBean, ref sb);
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