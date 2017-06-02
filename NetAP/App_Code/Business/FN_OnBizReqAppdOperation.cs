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
/// FN_OnBizReqAppdOperation 的摘要描述
/// </summary>
public class FN_OnBizReqAppdOperation
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    public static bool GetVisitFormGridViewData(OnBizReqAppdOperationModel model, out List<CheckVisitGridDataBean> ReturnDatas, out string msg)
    {
        bool result = false;
        msg = "";
        ReturnDatas = new List<CheckVisitGridDataBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                CheckVisitGridDataBean dataBean = new CheckVisitGridDataBean()
                {
                    CompID = model.CompID,
                    ValidID = model.ValidID
                };
                StringBuilder sb = new StringBuilder();
                OnBizReqAppdOperationSql.GetOnBizReqAppdOperationData(dataBean, ref sb);
                try
                {
                    ReturnDatas = conn.Query<CheckVisitGridDataBean>(sb.ToString(), dataBean).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                if (ReturnDatas == null || ReturnDatas.Count == 0)
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

    public static bool GetVisitFormDetailData(CheckVisitPKModel model, out OnBizReqAppdOperationBean ReturnDatas, out string msg)
    {
        bool result = false;
        msg = "";
        ReturnDatas = new OnBizReqAppdOperationBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                OnBizReqAppdOperationBean dataBean = new OnBizReqAppdOperationBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    WriteDate = model.WriteDate,
                    FormSeq = model.FormSeq
                };
                StringBuilder sb = new StringBuilder();
                OnBizReqAppdOperationSql.GetVisitFormDetailData(dataBean, ref sb);
                try
                {
                    ReturnDatas = conn.Query<OnBizReqAppdOperationBean>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (ReturnDatas == null)
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
    /// 將DB資料轉成作格式化
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<CheckVisitGridDataModel> GridDataFormat(List<CheckVisitGridDataBean> dbDataList)
    {
        var result = new List<CheckVisitGridDataModel>();
        foreach (var item in dbDataList)
        {
            var data = new CheckVisitGridDataModel();
            data.CompID = item.CompID;
            data.EmpID = item.EmpID;
            data.WriteDate = item.WriteDate;
            data.FormSeq = item.FormSeq;
            data.EmpID_NameN = item.EmpID + "-" + item.EmpNameN;
            data.DeputyID_Name = item.DeputyID_Name;
            data.VisitBeginDate = item.VisitBeginDate;
            data.BeginTime = item.BeginTime;
            data.VisitEndDate = item.VisitEndDate;
            data.EndTime = item.EndTime;
            data.FlowCaseID = item.FlowCaseID;
            data.FlowLogID = item.FlowLogID;
            data.VisitReasonCN = item.VisitReasonCN;
            result.Add(data);
        }
        return result;
    }


    public static OnBizReqAppdOperationModel DetailDataFormat(OnBizReqAppdOperationBean Detaildatas)
    {
        var result = new OnBizReqAppdOperationModel();

        result.CompID_Name = Detaildatas.CompID_Name;
        result.EmpID_NameN = Detaildatas.EmpID_NameN;
        result.WriteDate = Detaildatas.WriteDate;
        result.WriterID_Name = Detaildatas.WriterID_Name;
        result.VisitFormNo = Detaildatas.VisitFormNo;
        result.DeptName = Detaildatas.DeptName;
        result.TitleName = Detaildatas.TitleName;
        result.Position = Detaildatas.Position;
        result.Tel_1 = Detaildatas.Tel_1;
        result.Tel_2 = Detaildatas.Tel_2;
        result.VisitDate = Detaildatas.VisitDate;
        result.VisitTime = Detaildatas.VisitTime;
        result.DeputyID_Name = Detaildatas.DeputyID_Name;
        result.LocationType = Detaildatas.LocationType;
        result.InterLocationName = Detaildatas.InterLocationName;
        result.ExterLocationName = Detaildatas.ExterLocationName;
        result.VisiterName = Detaildatas.VisiterName;
        result.VisiterTel = Detaildatas.VisiterTel;
        result.VisitReason = Detaildatas.VisitReason;
        result.VisitReasonDesc = Detaildatas.VisitReasonDesc;
        result.LastChgComp_Name = Detaildatas.LastChgComp_Name;
        result.LastChgID_Nanme = Detaildatas.LastChgID_Nanme;
        result.LastChgDate = Detaildatas.LastChgDate;
        result.FlowCaseID = Detaildatas.FlowCaseID;
        return result;
    }
}