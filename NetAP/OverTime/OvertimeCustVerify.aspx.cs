using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Data;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Work.Properties;
using System.Data.Common;

public partial class Overtime_OvertimeCustVerify : SecurePage
{
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    public static string _HRDB = Util.getAppSetting("app://HRDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");

    #region"網頁傳值"

    public string _CurrFlowID
    {
        get
        {
            if (ViewState["_CurrFlowID"] == null)
            {
                ViewState["_CurrFlowID"] = (Request["FlowID"] != null) ? Request["FlowID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowID"]);
        }
        set
        {
            ViewState["_CurrFlowID"] = value;
        }
    }
    public string _CurrFlowLogID
    {
        get
        {
            if (ViewState["_CurrFlowLogID"] == null)
            {
                ViewState["_CurrFlowLogID"] = (Request["FlowLogID"] != null) ? Request["FlowLogID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowLogID"]);
        }
        set
        {
            ViewState["_CurrFlowLogID"] = value;
        }
    }

    public string _ReturnUrl
    {
        get
        {
            if (ViewState["_ReturnUrl"] == null)
            {
                ViewState["_ReturnUrl"] = "";
            }
            return (string)(ViewState["_ReturnUrl"]);
        }
        set
        {
            ViewState["_ReturnUrl"] = value;
        }
    }
    #endregion"網頁傳值"

    #region Judy的
    //Judy在Aattendant撈取StartDate會有問題改成EndDate 去掉CompID
    public DataTable QueryData(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Append("SELECT " + strColumn + " FROM " + strTable);
        sb.Append(" WHERE 1=1 ");
        sb.Append(strWhere);
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt;
    }
    public int QuerySeq(string strTable, string strEmpID, string date)//Seq
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        int OTSeq = 0;
        DataTable dt = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, " AND OTEmpID='" + strEmpID + "' AND OTEndDate='" + date + "'");
        if (dt.Rows.Count == 0)
        {
            OTSeq = 1;
        }
        else
        {
            OTSeq = Convert.ToInt32(dt.Rows[0]["MAXOTSeq"].ToString() == "" ? "0" : dt.Rows[0]["MAXOTSeq"].ToString()) + 1;
        }
        return OTSeq;
    }
    #endregion Judy的

    /// <summary>
    /// 1.當共用檔使用有問題時，須額外撈取現在是否最後一關時使用。
    /// 2.只有最基本的流程走法不會用到，其他情況幾乎看這。
    /// </summary>
    /// <param name="OTCompID">加班人公司</param>
    /// <param name="flowCaseID">FlowCaseID</param>
    /// <param name="otModel">A/D</param>
    /// <returns>現在是否為最後一關</returns>
    private bool isLastFlowNow(string OTCompID, string flowCaseID, string otModel)
    {
        DataRow retrunRow;
        string message = "";
        try
        {
            if (!FlowUtility.QueryHRFlowEngineDatas_Now(OTCompID, flowCaseID, otModel, out retrunRow, out message))
                return false;
            else if (retrunRow.Table.Rows.Count > 0)
            {
                string FlowEndFlag = retrunRow["FlowEndFlag"].ToString();
                return FlowEndFlag == "1" ? true : false;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            return true;
        }
    }

    /// <summary>
    ///遇到什麼奇怪狀況沒辦法知道現在關卡實際簽核人(非代理人)時，使用
    /// </summary>
    private string getLastAssignTo(string FlowCaseID)
    {
        return CustVerify.HROverTimeLog(FlowCaseID, false).Rows[0]["SignID"].ToString();
    }

    /// <summary>
    /// 從Para取得的dt中，撈取該公司Valid與Emp的RankID
    /// </summary>
    /// <param name="dt">完整的Para資料表</param>
    /// <param name="CompID">欲找尋RankMapping的公司</param>
    /// <param name="RankType">ValidRankID與EmpRankID欄位名稱擇一</param>
    /// <returns>欄位內容</returns>
    private string RankPara(DataTable dt, string CompID, string RankType)
    {
        string RankID = "0";
        Aattendant a = new Aattendant();
        try
        {
            if (dt.Select("CompID='" + CompID + "'").Count() > 0)
            {
                RankID = a.Json2DataTable(dt.Select("CompID='" + CompID + "'").CopyToDataTable().Rows[0]["Para"].ToString()).Rows[0][RankType].ToString();
                return RankID;
            }
        }catch(Exception ex)
        {
            return RankID;
        }
        return RankID;
    }

    /// <summary>
    /// 原本不允許送審重複主管，現在又改成可以，所以把這方法註解，改成不檢核直接查詢
    /// </summary>
    /// <param name="SignID">下一關主管ID</param>
    /// <param name="SignIDComp">下一關主管公司</param>
    /// <param name="oAssDic">回傳Dictionary格式給永豐流程使用</param>
//    private void oAssDicCheck(string SignID, string SignIDComp, ref  Dictionary<string, string> oAssDic)
//{
//    if (SignID.Trim().Equals(UserInfo.getUserInfo().UserID.Trim()))
//        {
//            oAssDic.Clear();
//        }
//        else
//        {
//            oAssDic = CustVerify.getEmpID_Name_Dictionary(SignID, SignIDComp);
//        }
//}

    protected void Page_Load(object sender, EventArgs e)
    {
        //代辦畫面傳來的多筆審核資料
        DataTable dtOverTimeAdvance = (DataTable)Session["dtOverTimeAdvance"];
        DataTable dtOverTimeDeclaration = (DataTable)Session["dtOverTimeDeclaration"];

        //永豐流程相關資料
        FlowExpress oFlow = new FlowExpress(Aattendant._AattendantFlowID, Request["FlowLogID"], true);

        //錯誤訊息儲存
        string ErrMsg = "";
        CustVerify CV=new CustVerify();
        Dictionary<string, string> toUserData = (Dictionary<string, string>)Session["toUserData"];

        //EmpInfo.QueryOrganData || EmpInfo.QueryFlowOrganData 使用
        //string SignOrganID = "", SignID = "", SignIDComp = "";

        if (!IsPostBack)
        {
            if (Session["FlowVerifyInfo"] != null)
            {
                #region"變數"
                //TrtCatchIsFlowVerify()使用
                DbHelper db = new DbHelper(Aattendant._AattendantDBName);
                CommandHelper sb = db.CreateCommandHelper();
                DbConnection cn = db.OpenConnection();
                DbTransaction tx = cn.BeginTransaction();

                //撈取該筆加班單相關資料
                DataTable dtOverTime;

                //預設下關待辦人(會變，沒變代表沒找到相關資料)
                Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
                Util.setJSContent(oVerifyInfo["FlowVerifyJS"]);
                Session["FlowVerifyInfo"] = null;
                Dictionary<string, string> oAssDic = Util.getDictionary(oVerifyInfo["FlowStepAssignToList"]);
                
                //共用檔
                Aattendant _Aattendant = new Aattendant();

                //Para撈取參數設定
                DataTable dtOverTimePara_All = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];

                string AD="";
                #endregion"變數"
                switch (oVerifyInfo["FlowStepBtnID"].ToString())
                {
                    case "btnClose":
                    case "btnReApprove":
                    case "FlowReassign":
                    case "btnApprove":  //審核
                        //單筆審核(使用oFlow.FlowCaseID判斷)
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null)
                        {
                            #region"單筆審核"
                            //給IsFlowVerify的下一關簽核主管
                            //oAssDicCheck(toUserData["SignID"], toUserData["SignIDComp"], ref oAssDic); //原本要檢核主管是否重複，後來說不要了
                            oAssDic = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);
                            switch (oFlow.FlowCurrStepID)
                            {
                                    //事先
                                case "A10":
                                case "A20":
                                   AD="A";
                                    break;
                                    //事後
                                case "A30":
                                case "A40":
                                    AD="D";
                                    break;
                            }
                            dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(oFlow.FlowCaseID, AD);
                            if (oVerifyInfo["FlowStepBtnID"].ToString() == "btnClose") //結案
                            {
                                sb.Reset();
                                CustVerify.UpdateOverTime(AD, "3", oFlow.FlowCaseID, ref sb);
                                CustVerify.UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                CustVerify.CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                if(AD=="A")CustVerify.insertFromAdvanceToData(oFlow.FlowCaseID, ref sb);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], oVerifyInfo["FlowStepBtnID"], oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0], true))
                                {
                                    CV.MailLogContent(oFlow.FlowCaseID, "3", AD, "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                            }
                            else //沒結案
                            {
                                sb.Reset();
                                DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(oFlow.FlowCaseID, false);
                                CustVerify.UpdateOverTime(AD, "2", oFlow.FlowCaseID, ref sb);
                                CustVerify.UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                FlowUtility.InsertHROverTimeLogCommand(
                                    oFlow.FlowCaseID,
                                    CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                    CustVerify.FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                    AD,
                                    dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                    dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                    dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                    UserInfo.getUserInfo().UserID,
                                    dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                    dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                    CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                    toUserData["SignLine"],
                                    toUserData["SignIDComp"],
                                    toUserData["SignID"],
                                    toUserData["SignOrganID"],
                                    toUserData["SignFlowOrganID"],
                                    "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                    );

                                if (TryCatchIsFlowVerify(Request["FlowLogID"], oVerifyInfo["FlowStepBtnID"], oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0],true))
                                {
                                    CV.MailLogContent(oFlow.FlowCaseID, "2", AD, "", "", false);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                            }
                            #endregion"單筆審核"
                        }
                        /*================================================*/
                        //多筆審核有四個迴圈，事先兩個是後兩個
                        //UpdateAD、HROverTimeLog、MailLog在第一迴圈
                        //其他在第二迴圈
                        //多筆審核按鈕(非印章)
                        else
                        {
                            #region"事先多筆"
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                AD = "A";
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    if (dtOverTimeAdvance.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, CustVerify.getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);
                                        string btnName = dtOverTimeAdvance.Rows[i]["btnName"].ToString();
                                        //oAssDicCheck(dtOverTimeAdvance.Rows[i]["SignID"].ToString(), dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString(), ref oAssDic);
                                        oAssDic = CustVerify.getEmpID_Name_Dictionary(dtOverTimeAdvance.Rows[i]["SignID"].ToString(), dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString());
                                        if (btnName == "btnClose") //結案
                                        {

                                            sb.Reset();
                                            CustVerify.UpdateOverTime(AD, "3", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            CustVerify.UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            CustVerify.CloseHROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                            CustVerify.insertFromAdvanceToData(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                            {
                                                CV.MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "3", AD, "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                        else //沒結案
                                        {
                                            sb.Reset();
                                            DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), false);
                                            CustVerify.UpdateOverTime(AD, "2", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            CustVerify.UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            FlowUtility.InsertHROverTimeLogCommand(
                                                dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(),
                                                CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                                CustVerify.FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                                AD,
                                                dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                                UserInfo.getUserInfo().UserID,
                                                dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                                dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                                CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                                //toUserData start
                                                dtOverTimeAdvance.Rows[i]["SignLine"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignID"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignOrganID"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignFlowOrganID"].ToString(),
                                                //toUserData end
                                                "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                                );

                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                            {
                                                CV.MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", AD, "", "", false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion"事先多筆"
                            /*------------------*/
                            #region"事後多筆"
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                                AD = "D";
                                for (int i = 0; i < dtOverTimeDeclaration.Rows.Count; i++)
                                {
                                    if (dtOverTimeDeclaration.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, CustVerify.getFlowLogID(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "D"), true);
                                        string btnName = dtOverTimeDeclaration.Rows[i]["btnName"].ToString();
                                        //oAssDicCheck(dtOverTimeDeclaration.Rows[i]["SignID"].ToString(), dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString(), ref oAssDic);
                                        oAssDic = CustVerify.getEmpID_Name_Dictionary(dtOverTimeDeclaration.Rows[i]["SignID"].ToString(), dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString());
                                        if (btnName == "btnClose") //結案
                                        {
                                            sb.Reset();
                                            CustVerify.UpdateOverTime(AD, "3", dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            CustVerify.UpdateHROrverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            CustVerify.CloseHROverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                            
                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                            {
                                                CV.MailLogContent(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "3", AD, "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                        else //沒結案
                                        {
                                            sb.Reset();
                                            DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), false);
                                            CustVerify.UpdateOverTime(AD, "2", dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            CustVerify.UpdateHROrverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            FlowUtility.InsertHROverTimeLogCommand(
                                                dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(),
                                                CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                                CustVerify.FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                                AD,
                                                dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                                UserInfo.getUserInfo().UserID,
                                                dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                                dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                                CustVerify.addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                                //toUserData start
                                                dtOverTimeDeclaration.Rows[i]["SignLine"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignID"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignOrganID"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignFlowOrganID"].ToString(),
                                                //toUserData end
                                                "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                                );

                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                            {
                                                CV.MailLogContent(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", AD, "", "", false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion"事後多筆"
                        }
                        break;

                    /*=================================================*/
                    case "btnReject":  //駁回
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null) //true 印章 false 多筆審核按鈕
                        {
                            #region"單筆駁回"
                            switch (oFlow.FlowCurrStepID)
                            {
                                //事先
                                case "A10":
                                case "A20":
                                    AD = "A";
                                    break;
                                //事後
                                case "A30":
                                case "A40":
                                    AD = "D";
                                    break;
                            }
                                dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(oFlow.FlowCaseID, AD);
                                sb.Reset();
                                CustVerify.UpdateOverTime(AD, "4", oFlow.FlowCaseID, ref sb);
                                CustVerify.UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                CustVerify.CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                if (AD == "D") CustVerify.AfterReject_CheckAndInsert(oFlow.FlowCaseID, ref sb);
                                //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0],true))
                                {
                                    CV.MailLogContent(oFlow.FlowCaseID, "4", AD, "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                            #endregion"單筆駁回"
                        }

                        else //多筆審核按鈕
                        {
                            #region"多筆駁回"
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                AD = "A";
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    if (dtOverTimeAdvance.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, CustVerify.getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);
                                        sb.Reset();
                                        CustVerify.UpdateOverTime(AD, "4", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                        CustVerify.UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "3", ref sb);
                                        CustVerify.CloseHROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                        //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                        if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                        {
                                            CV.MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "4", AD, "", "", true);
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                        }
                                        else
                                        {
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                        }
                                    }
                                }
                            }
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                               AD = "D";
                               for (int i = 0; i < dtOverTimeDeclaration.Rows.Count; i++)
                               {
                                   if (dtOverTimeDeclaration.Rows[i]["OTSeqNo"].ToString() == "1")
                                   {
                                       oFlow = new FlowExpress(Aattendant._AattendantFlowID, CustVerify.getFlowLogID(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "D"), true);
                                       sb.Reset();
                                       CustVerify.UpdateOverTime(AD, "4", oFlow.FlowCaseID, ref sb);
                                       CustVerify.UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                       CustVerify.CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                       CustVerify.AfterReject_CheckAndInsert(oFlow.FlowCaseID, ref sb);
                                       //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                       if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                       {
                                           CV.MailLogContent(oFlow.FlowCaseID, "4", AD, "", "", true);
                                           labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                       }
                                       else
                                       {
                                           labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                       }
                                   }
                               }
                            }
                            #endregion"多筆駁回"
                        }
                        break;
                    /*==============================*/
                }
                if (txtErrMsg.Text != "")
                {
                    txtErrMsg.Text = "審核失敗清單：" + txtErrMsg.Text;
                    txtErrMsg.Visible = true;
                }
               
            }
            else
            {
                //參數錯誤
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError);
            }
        }
    }

    /// <summary>
    /// 核准動作：IsFlowVerify()、其他Table新刪修的sb、try-catch與失敗清單
    /// </summary>
    /// <param name="FlowLogID">IsFlowVerify用</param>
    /// <param name="btn">IsFlowVerify用</param>
    /// <param name="oAssDic">IsFlowVerify用</param>
    /// <param name="FlowStepOpinion">IsFlowVerify用</param>
    /// <param name="sb">全部Table的新刪修SQL</param>
    /// <param name="ErrMsg">回報錯誤(目前沒用)</param>
    /// <param name="drOverTime">失敗清單，找尋單筆相關資料用</param>
    /// <param name="ADType">失敗清單，找尋單筆相關資料用</param>
    /// <returns></returns>
    private bool TryCatchIsFlowVerify(string FlowLogID, string btn, Dictionary<string, string> oAssDic, string FlowStepOpinion, CommandHelper sb, out string ErrMsg, DataRow drOverTime = null, bool isOnly = true)
    {
        ErrMsg = "";
        /************************************************/
        /*測試用test                                                                  */
        //sb.Append("test我是來製作錯誤的!!");                    
        /*if (btn == "btnClose") btn = "btnApprove";                */
        //return true;
        /************************************************/

        //查無主管的檢核往前移了，正常應該是不會有空的oAssDic，以防萬一留著
        if(oAssDic.IsNullOrEmpty())
        {
            MsgOut(drOverTime, "查無下一關審核主管。", isOnly);
            return false;
        }
        else if (oAssDic.Count == 0)
        {
            MsgOut(drOverTime, "查無下一關審核主管。", isOnly);
            return false;
        }
      
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            if (FlowExpress.IsFlowVerify(Request["FlowID"], FlowLogID, btn, oAssDic, FlowStepOpinion))
            {
                tx.Commit();
                return true;
            }
            else
            {
                if (drOverTime != null)
                {
                    MsgOut(drOverTime, "IsFlowVerify執行出錯。", isOnly);
                }
                tx.Rollback();
                return false;
            }
        }
        catch (Exception ex)
        {
            ErrMsg = ex.ToString();
            if (drOverTime != null)
            {
                  MsgOut(drOverTime, "加班單相關Table新增、修改出錯。", isOnly);
            }
            tx.Rollback();
            return false;
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
        return true;
    }
    private void MsgOut(DataRow drOverTime, string Msg, bool isOnly)
    {
        if (isOnly)
        {
            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                    "-------------------------------------" + Environment.NewLine +
                    "公司：" + drOverTime["OTCompID"].ToString() + Environment.NewLine +
                    "加班人：" + drOverTime["OTEmpID"].ToString() + Environment.NewLine +
                    "起迄日期：" + drOverTime["OTDate"].ToString() + Environment.NewLine +
                    "開始時間：" + drOverTime["OTStartTime"].ToString() + Environment.NewLine +
                    "結束時間：" + drOverTime["OTEndTime"].ToString() + Environment.NewLine +
                    "錯誤原因：" + Msg;
        }
        else
        {
            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                               "-------------------------------------" + Environment.NewLine +
                               "公司：" + drOverTime["CompID"].ToString() + Environment.NewLine +
                               "加班人：" + drOverTime["EmpID"].ToString() + Environment.NewLine +
                               "起迄日期：" + drOverTime["OTStartDate"].ToString() +
                               "～" + drOverTime["OTEndDate"].ToString() + Environment.NewLine +
                               "開始時間：" + drOverTime["OTStartTime"].ToString() + Environment.NewLine +
                               "結束時間：" + drOverTime["OTEndTime"].ToString() + Environment.NewLine +
                               "錯誤原因：" + Msg;
        }
    }
    
}