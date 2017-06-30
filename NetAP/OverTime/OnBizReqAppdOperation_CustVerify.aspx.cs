/*
 * 建立日期：2017/06/13 
 * 建立人員：Leo
 * 程式說明：
 * 1.為配合單筆流程審核規範，支援John增加公出單筆審核，有問題找John。 (凝視遠方)
 * 2.目前只有一關，若要調整成多關須再配合調整。
 */
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
using Newtonsoft.Json;

public partial class Overtime_OvertimeCustVerify : SecurePage
{
    private static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private static string _HRDB = Util.getAppSetting("app://HRDB_OverTime/");
    private string _attendantDBName = Util.getAppSetting("app://AattendantDB_OverTime/");

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DoRelease();
        }
    }

    #region"審核"
    private void DoRelease()
    {
        
        if (Session["FlowVerifyInfo"] != null)
        {
            DbHelper db = new DbHelper(Aattendant._AattendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                //永豐流程相關資料
                FlowExpress oFlow = new FlowExpress("OnBizReqAppd_ITRD", Request["FlowLogID"], true);

                //預設下關審核人，因為只有一關所以是結案人，即為本關審核人
                Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
                Session["FlowVerifyInfo"] = null;
                Util.setJSContent(oVerifyInfo["FlowVerifyJS"]);
                Dictionary<string, string> oAssDic = Util.getDictionary(oVerifyInfo["FlowStepAssignToList"]);

                //審核資料
                string strFlowCustDB = oFlow.FlowID;
                string strReason = oFlow.FlowDefOpinion;
                string [] FlowList=oFlow.FlowKeyValueList;
                string rowCompID = FlowList[0];
                string rowEmpID = FlowList[1];
                string rowWriteDate = FlowList[2];
                string rowFormSeq = FlowList[3];
                string rowFlowCaseID = oFlow.getFlowCaseID(oFlow.FlowLogID);
                string rowFlowLogID = oFlow.FlowLogID;
                string btnAct = "";
                string strOBFormStatus = "";
                string HRLogStatus="";
                
                if (oVerifyInfo["FlowStepBtnID"].ToString() == "btnClose") //核准
                {
                    btnAct = "btnClose";
                    strOBFormStatus = "3";
                    HRLogStatus = "2";
                }
                else if (oVerifyInfo["FlowStepBtnID"].ToString() == "btnReject") //駁回
                {
                    btnAct = "btnReject";
                    strOBFormStatus = "4";
                    HRLogStatus = "3";
                }
                else //有問題
                {
                    ShowError("永豐流程按鈕錯誤");
                    return;
                }

                //審核SQL字串
                OnBizReqAppdOperationSql.UpdateVisitForm(rowCompID, rowEmpID, rowWriteDate, rowFormSeq, strOBFormStatus, strReason, ref sb);
                OnBizReqAppdOperationSql.UpdateHROverTimeLog(rowFlowCaseID, HRLogStatus, ref sb);
                FlowUtility.ChangeFlowFlag(rowFlowCaseID, "OB01", "0001", UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID, "0", ref sb);

                /*******************************/
                //sb.Append("我是來製造錯誤的~☆");  //測試用
                //labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed);
                //return;
                /*******************************/

                //先執行SQL，if(永豐流程成功)則Commit，else則RollBack。
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                if (FlowExpress.IsFlowVerify(strFlowCustDB, rowFlowLogID, btnAct, oAssDic, strReason))
                {
                    tx.Commit();
                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed);
                }
                else
                {
                    ShowError("永豐流程執行失敗");
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                ShowError("SQL語法出現錯誤，\n" + ex.Message);
            }
        }
        else
        {
            //參數錯誤
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError);
        }
    }
    #endregion"審核"

    private void ShowError(string str)
    {
        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError);
        txtErrMsg.Text = str;
        txtErrMsg.Visible = true;
    }
}