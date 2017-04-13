<%@ WebHandler Language="C#" Class="OverTimeMultiDataApprovedHandler" %>

using System;
using System.Data;
using System.Text;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

using System.ComponentModel;

public class OverTimeMultiDataApprovedHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 跨域呼叫ashx測試
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// 


    public class DataInfo
    {
        [JsonProperty(PropertyName = "OTStartDate")]
        public string OTStartDate { get; set; }

        [JsonProperty(PropertyName = "OTEndDate")]
        public string OTEndDate { get; set; }

        [JsonProperty(PropertyName = "OTEmpID")]
        public string OTEmpID { get; set; }

        [JsonProperty(PropertyName = "OTStartTime")]
        public string OTStartTime { get; set; }

        [JsonProperty(PropertyName = "OTEndTime")]
        public string OTEndTime { get; set; }

        [JsonProperty(PropertyName = "OTCompID")]
        public string OTCompID { get; set; }

        [JsonProperty(PropertyName = "OTTxnID")]
        public string OTTxnID { get; set; }

        [JsonProperty(PropertyName = "OTSeq")]
        public string OTSeq { get; set; }

        [JsonProperty(PropertyName = "OTRegisterID")]
        public string OTRegisterID { get; set; }

        [JsonProperty(PropertyName = "OTRegisterComp")]
        public string OTRegisterComp { get; set; }
    }



    [JsonObject("DataGrid")]
    public class DataListObject
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<DataInfo> DataList { get; set; }
    }

    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    private Aattendant at = new Aattendant();

    public void ProcessRequest(HttpContext context)
    {
        SinoPac.WebExpress.DAO.DbHelper db = new SinoPac.WebExpress.DAO.DbHelper(_overtimeDBName);
        SinoPac.WebExpress.DAO.CommandHelper sb = db.CreateCommandHelper();
        System.Data.Common.DbConnection cn = db.OpenConnection();
        System.Data.Common.DbTransaction tx = cn.BeginTransaction();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        string strName = "";
        //string strOTTxnID = "";
        //LoadCheckData();
        Dictionary<string, string> oAssTo = new Dictionary<string, string>();
        //DataTable DT = (DataTable)ViewState["dt"];
        string dataGridString = "";//context.Request.QueryString["DataGrid"];

        string Platform = context.Request.QueryString["Platform"];
        string SystemID = context.Request.QueryString["SystemID"];
        string TxnName = context.Request.QueryString["TxnName"];
        string UserID = context.Request.QueryString["UserID"];
        string UserComp = context.Request.QueryString["UserComp"];
        string CacheID = context.Request.QueryString["CacheID"];

        if (!UserInfo.Init(UserID, true))
        {
            throw new Exception(Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Login Fail!"));
        }

        string strQrySQL = string.Format("select CacheData from CacheData where  Platform = '{0}' and SystemID='{1}' and TxnName='{2}' and UserID='{3}' and CacheID='{4}'", Platform, SystemID, TxnName, UserID, CacheID);

        try
        {
            dataGridString = db.ExecuteScalar(CommandType.Text, strQrySQL).ToString();
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            SetResponse(context, new ReturnJsonModel()
            {
                IsSussecc = false,
                Message = "送簽失敗(7)"
            });
            return;
        }


        var dataListObject = JsonConvert.DeserializeObject<DataListObject>(dataGridString);

        try
        {
            //if (gvMain.Visible && gvMain.Rows.Count > 0)
            if (dataListObject != null)
            {
                var dataList = dataListObject.DataList;

                if (dataList.Count <= 0)
                {
                    //Util.MsgBox("尚未勾選資料");
                    SetResponse(context, new ReturnJsonModel()
                    {
                        IsSussecc = false,
                        Message = "尚未勾選資料"
                    });
                    ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                    return;
                }
                //if (DT.Rows.Count > 0)
                //{

                if (dataList != null && dataList.Count > 0)
                {
                    int count = -1;
                    foreach (var data in dataList)
                    {
                        count++;
                        //逐筆送簽
                        oAssTo.Clear();
                        using (DataTable dt = AattendantForHandler.getOrganHRBoss(data.OTRegisterComp, data.OTRegisterID, data.OTCompID, data.OTEmpID))
                        {
                            oAssTo.Add(dt.Rows[0]["Boss"].ToString(), dt.Rows[0]["oAssName"].ToString());
                        }
                        strName = AattendantForHandler.QueryColumn("NameN", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", " AND EmpID='" + data.OTEmpID + "' ");
                        string strStartTime = data.OTStartTime.ToString().Substring(0, 2) + "：" + data.OTStartTime.ToString().Substring(2, 2);
                        string strEndTime = data.OTEndTime.ToString().Substring(0, 2) + "：" + data.OTEndTime.ToString().Substring(2, 2);
                        string FlowKeyValue = "B," + data.OTCompID + "," + data.OTEmpID + "," + data.OTStartDate + "," + data.OTEndDate + "," + data.OTSeq;
                        string FlowShowValue = data.OTEmpID + "," + strName + "," + data.OTStartDate + "," + strStartTime + "," + data.OTEndDate + "," + strEndTime;
                        if (FlowExpress.IsFlowInsVerify(flow.FlowID, FlowKeyValue.Split(','), FlowShowValue.Split(','), "btnAfterLast", oAssTo, ""))
                        {

                            string flowCaseID = FlowExpress.getFlowCaseID(flow.FlowID, FlowKeyValue);
                            //更新AssignToName(部門+員工姓名)
                            if (!string.IsNullOrEmpty(flowCaseID))
                            {
                                //CommandHelper sb1 = db.CreateCommandHelper();
                                string eNameN = "";
                                string eRankID = "";
                                string eTitleID = "";
                                string eOrganID = "";
                                string eUpOrganID = "";
                                string eBoss = "";
                                string eBossCompID = "";
                                string eDeptID = "";
                                string ePositionID = "";
                                string eWorkTypeID = "";
                                string eFlowOrganID = "";
                                string eFlowDeptID = "";
                                string eFlowUpOrganID = "";
                                string eFlowBoss = "";
                                string eFlowBossCompID = "";
                                string eBusinessType = "";
                                string eEmpFlowRemarkID = "";
                                //查詢加班人資料
                                EmpInfo.QueryEmpData(data.OTCompID, data.OTEmpID, DateTime.Now.ToString("yyyy/MM/dd"),
                                   out eNameN, out eRankID, out eTitleID, out eOrganID, out eUpOrganID, out eBoss, out eBossCompID, out eDeptID, out ePositionID, out eWorkTypeID,
                                   out eFlowOrganID, out eFlowDeptID, out eFlowUpOrganID, out eFlowBoss, out eFlowBossCompID, out eBusinessType, out eEmpFlowRemarkID);
                                string NameN = "";
                                string RankID = "";
                                string TitleID = "";
                                string OrganID = "";
                                string UpOrganID = "";
                                string Boss = "";
                                string BossCompID = "";
                                string DeptID = "";
                                string PositionID = "";
                                string WorkTypeID = "";
                                string FlowOrganID = "";
                                string FlowDeptID = "";
                                string FlowUpOrganID = "";
                                string FlowBoss = "";
                                string FlowBossCompID = "";
                                string BusinessType = "";
                                string EmpFlowRemarkID = "";
                                string bBoss = "";
                                string bBossCompID = "";
                                string bOrganID = "";
                                //查詢申請人資料
                                EmpInfo.QueryEmpData(data.OTRegisterComp, data.OTRegisterID, DateTime.Now.ToString("yyyy/MM/dd"),
                                    out NameN, out RankID, out TitleID, out OrganID, out UpOrganID, out Boss, out BossCompID, out DeptID, out PositionID, out WorkTypeID,
                                    out FlowOrganID, out FlowDeptID, out FlowUpOrganID, out FlowBoss, out FlowBossCompID, out BusinessType, out EmpFlowRemarkID);
                                //查詢申請人主管資料
                                EmpInfo.QueryOrganData(data.OTRegisterComp, OrganID, DateTime.Now.ToString("yyyy/MM/dd"), out bOrganID, out bBoss, out bBossCompID);

                                /*20170323: 當HR審核主管與加班人是同一人時，要以加班人主管為審核主管*/
                                if (bBossCompID.Trim().Equals(data.OTCompID.Trim()) && bBoss.Trim().Equals(data.OTEmpID.Trim()))
                                {
                                    //查詢加班人主管資料
                                    EmpInfo.QueryOrganData(data.OTCompID, eOrganID, DateTime.Now.ToString("yyyy/MM/dd"), out bOrganID, out bBoss, out bBossCompID);
                                }
                                
                                //加進HROverTimeLog
                                FlowUtility.InsertHROverTimeLogCommand(flowCaseID, "1", flowCaseID + ".00001",
                                   "D", data.OTEmpID, eOrganID, "", data.OTRegisterID,
                                   "", "", "", "4", UserComp, bBoss, bOrganID, "", "0", false, ref sb, 1, "", count);

                                //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
                                //回寫FlowCaseID和OTstatus
                                if (data.OTStartDate == data.OTEndDate)
                                {
                                    sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "', OTStatus='2' ");
                                    sb.Append(" WHERE OTCompID='" + data.OTCompID + "'");
                                    sb.Append(" AND OTEmpID='" + data.OTEmpID + "'");
                                    sb.Append(" AND OTStartDate='" + data.OTStartDate + "'");
                                    sb.Append(" AND OTEndDate='" + data.OTEndDate + "'");
                                    sb.Append(" AND OTStartTime='" + data.OTStartTime + "'");
                                    sb.Append(" AND OTEndTime='" + data.OTEndTime + "'");
                                    sb.Append(" AND OTTxnID='" + data.OTTxnID + "'");
                                    //sb.Append(" AND OTSeq='" + data.OTSeq + "'");
                                }
                                else
                                {
                                    string crossDayArray = data.OTStartDate + "," + data.OTEndDate;
                                    //string strOTTxnID = at.QueryColumn("OTTxnID", "OverTimeDeclaration", " AND OTEmpID='" + data.OTEmpID + "' AND OTStartDate='" + data.OTStartDate + "' AND OTStartTime='" + data.OTStartTime + "' AND OTEndTime='2359' AND OTSeq='" + data.OTSeq + "'");
                                    for (int j = 0; j < crossDayArray.Split(',').Length; j++)
                                    {
                                        if (crossDayArray.Split(',')[j] == data.OTStartDate)
                                        {
                                            sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "', OTStatus='2' ");
                                            sb.Append(" WHERE OTCompID='" + data.OTCompID + "'");
                                            sb.Append(" AND OTEmpID='" + data.OTEmpID + "'");
                                            sb.Append(" AND OTStartDate='" + data.OTStartDate + "'");
                                            sb.Append(" AND OTEndDate='" + data.OTStartDate + "'");
                                            sb.Append(" AND OTStartTime='" + data.OTStartTime + "'");
                                            sb.Append(" AND OTEndTime='2359'");
                                            sb.Append(" AND OTTxnID='" + data.OTTxnID + "'");
                                            //sb.Append(" AND OTSeq='" + data.OTSeq + "'");
                                        }
                                        else
                                        {

                                            sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "', OTStatus='2' ");
                                            sb.Append(" WHERE OTCompID='" + data.OTCompID + "'");
                                            sb.Append(" AND OTEmpID='" + data.OTEmpID + "'");
                                            sb.Append(" AND OTStartDate='" + data.OTEndDate + "'");
                                            sb.Append(" AND OTEndDate='" + data.OTEndDate + "'");
                                            sb.Append(" AND OTStartTime='0000'");
                                            sb.Append(" AND OTEndTime='" + data.OTEndTime + "'");
                                            sb.Append(" AND OTTxnID='" + data.OTTxnID + "'");
                                            //sb.Append(" AND OTTxnID='" + strOTTxnID + "'");
                                        }
                                    }
                                }

                            }

                            //    if (data.OTStartDate.ToString() != data.OTEndDate.ToString())//當跨日的時候須回寫FlowCaseID到迄日
                            //    {
                            //        CommandHelper sb1 = db.CreateCommandHelper();
                            //        sb1.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID=A.FlowCaseID");
                            //        sb1.Append(" FROM (SELECT FlowCaseID FROM OverTimeDeclaration");
                            //        sb1.Append(" WHERE OTEmpID='" + data.OTEmpID + "'");
                            //        sb1.Append(" AND OTStartDate='" + data.OTStartDate + "'");
                            //        sb1.Append(" AND OTEndDate='" + data.OTStartDate + "'");
                            //        sb1.Append(" AND OTStartTime='" + data.OTStartTime + "'");
                            //        sb1.Append(" AND OTEndTime='2359'");
                            //        sb1.Append(" ) A ");
                            //        sb1.Append(" WHERE OTEmpID='" + data.OTEmpID + "'");
                            //        sb1.Append(" AND OTStartDate='" + data.OTEndDate + "'");
                            //        sb1.Append(" AND OTEndDate='" + data.OTEndDate + "'");
                            //        sb1.Append(" AND OTStartTime='0000'");
                            //        sb1.Append(" AND OTEndTime='" + data.OTEndTime + "'");
                            //        try
                            //        {
                            //            db.ExecuteNonQuery(sb1.BuildCommand(), tx);
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                            //            tx.Rollback();//資料更新失敗
                            //            //Util.MsgBox("送簽失敗");

                            //            SetResponse(context, new ReturnJsonModel()
                            //            {
                            //                IsSussecc = false,
                            //                Message = "送簽失敗(2)"
                            //            });
                            //            ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                            //            return;
                            //        }
                            //    }
                        }
                        else
                        {
                            SetResponse(context, new ReturnJsonModel()
                            {
                                IsSussecc = false,
                                Message = "送簽失敗(3)"
                            });
                            ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                            return;
                        }

                        //if (data.OTStartDate.ToString() == data.OTEndDate.ToString())
                        //{
                        //    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStatus='2',");
                        //    sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                        //    sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "'");
                        //    sb.Append(" WHERE OTEmpID='" + data.OTEmpID + "'");
                        //    sb.Append(" AND OTStartDate='" + data.OTStartDate + "'");
                        //    sb.Append(" AND OTEndDate='" + data.OTEndDate + "'");
                        //    sb.Append(" AND OTStartTime='" + data.OTStartTime + "'");
                        //    sb.Append(" AND OTEndTime='" + data.OTEndTime + "'");
                        //}
                        //else
                        //{
                        //    string crossDayArray = data.OTStartDate + "," + data.OTEndDate;
                        //    for (int j = 0; j < crossDayArray.Split(',').Length; j++)
                        //    {
                        //        if (crossDayArray.Split(',')[j] == data.OTStartDate.ToString())
                        //        {
                        //            sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStatus='2',");
                        //            sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                        //            sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "'");
                        //            sb.Append(" WHERE OTEmpID='" + data.OTEmpID + "'");
                        //            sb.Append(" AND OTStartDate='" + data.OTStartDate + "'");
                        //            sb.Append(" AND OTEndDate='" + data.OTStartDate + "'");
                        //            sb.Append(" AND OTStartTime='" + data.OTStartTime + "'");
                        //            sb.Append(" AND OTEndTime='2359'");
                        //        }
                        //        else
                        //        {
                        //            sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStatus='2',");
                        //            sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                        //            sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "'");
                        //            sb.Append(" WHERE OTEmpID='" + data.OTEmpID + "'");
                        //            sb.Append(" AND OTStartDate='" + data.OTEndDate + "'");
                        //            sb.Append(" AND OTEndDate='" + data.OTEndDate + "'");
                        //            sb.Append(" AND OTStartTime='0000'");
                        //            sb.Append(" AND OTEndTime='" + data.OTEndTime + "'");
                        //        }
                        //    }
                        //}
                    }

                    try
                    {
                        db.ExecuteNonQuery(sb.BuildCommand(), tx);
                        tx.Commit();

                        // Util.MsgBox("送簽成功");//提案送審成功

                        SetResponse(context, new ReturnJsonModel()
                        {
                            IsSussecc = true,
                            Message = "送簽成功"
                        });
                        ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                        //tx.Rollback();//資料更新失敗
                        Util.MsgBox("送簽失敗(6)"); //提案送審失敗
                        return;
                    }
                    
                    
                    //try
                    //{
                    //    db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    //    tx.Commit();
                    //    // Util.MsgBox("送簽成功");//提案送審成功

                    //    SetResponse(context, new ReturnJsonModel()
                    //    {
                    //        IsSussecc = true,
                    //        Message = "送簽成功"
                    //    });
                    //    ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                    //    tx.Rollback();//資料更新失敗

                    //    SetResponse(context, new ReturnJsonModel()
                    //    {
                    //        IsSussecc = false,
                    //        Message = "送簽失敗(4)"
                    //    });
                    //    ClearData(Platform, SystemID, TxnName, UserID, CacheID);
                    //    return;
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            SetResponse(context, new ReturnJsonModel()
            {
                IsSussecc = false,
                Message = "送簽失敗(5)"
            });
            ClearData(Platform, SystemID, TxnName, UserID, CacheID);
            return;
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
                cn.Dispose();
            }

            if (tx != null)
            {
                tx.Dispose();
            }
            ClearData(Platform, SystemID, TxnName, UserID, CacheID);
        }
    }

    /// <summary>
    /// 清除Table資料
    /// </summary>
    private void ClearData(string Platform, string SystemID, string TxnName, string UserID, string CacheID)
    {
        SinoPac.WebExpress.DAO.DbHelper db = new SinoPac.WebExpress.DAO.DbHelper(_overtimeDBName);
        SinoPac.WebExpress.DAO.CommandHelper sb = db.CreateCommandHelper();
        System.Data.Common.DbConnection cn = db.OpenConnection();
        System.Data.Common.DbTransaction tx = cn.BeginTransaction();

        string strQrySQL = string.Format("delete from CacheData where  Platform = '{0}' and SystemID='{1}' and TxnName='{2}' and UserID='{3}' and CacheID='{4}'", Platform, SystemID, TxnName, UserID, CacheID);
        db.ExecuteNonQuery(CommandType.Text, strQrySQL);
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class RequestJsonModel
    {
        public string test01 { get; set; }
        public string test02 { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class ReturnJsonModel
    {
        public bool IsSussecc { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// 設定Response
    /// </summary>
    /// <param name="context">HttpResponse</param>
    /// <param name="jsonData">ReturnJsonModel</param>
    private static void SetResponse(HttpContext context, ReturnJsonModel jsonData)
    {
        string jsonCallback = context.Request.QueryString["jsoncallback"];

        if (string.IsNullOrEmpty(jsonCallback))
        {
            jsonCallback = context.Request.QueryString["callback"];
        }

        context.Response.ContentType = "application/json";
        context.Response.Charset = "utf-8";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        context.Response.Write(string.Format("{0}({1});", jsonCallback, serializer.Serialize(jsonData)));
    }

    /// <summary>
    /// 將Request.InputStream轉成城可讀字串
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <returns>string</returns>
    private static string GetFromInputStream(HttpContext context)
    {
        var reader = new System.IO.StreamReader(context.Request.InputStream);
        var result = reader.ReadToEnd();

        return result;
    }

    /// <summary>
    /// ashx預設參數
    /// </summary>
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

