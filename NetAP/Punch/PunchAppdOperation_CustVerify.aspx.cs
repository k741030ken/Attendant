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

public partial class Punch_PunchAppdOperation_CustVerify : SecurePage
{
    #region"全域變數"
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    public static string _HRDB = Util.getAppSetting("app://HRDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    #endregion"全域變數"

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
            DoSubmit();
        }
    }

    private void DoSubmit()
    {
        if (Session["FlowVerifyInfo"] != null)
        {
            Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
            Dictionary<string, string> dic = (Dictionary<string, string>)Session["PunchAppdOperation_GridView"];
            Dictionary<string, string> toUserData = (Dictionary<string, string>)Session["PunchAppdOperation_toUserData"];
            Util.setJSContent(oVerifyInfo["FlowVerifyJS"]);
            Session["FlowVerifyInfo"] = null;
            Punch_Confirm_Remedy_Bean model = new Punch_Confirm_Remedy_Bean()
            {
                //跟共用條件
                CompID = dic["CompID"],
                EmpID = dic["EmpID"],
                EmpName = dic["EmpName"],
                LastChgComp = UserInfo.getUserInfo().CompID.Trim(),
                LastChgID = UserInfo.getUserInfo().UserID.Trim(),
                LastChgDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),

                //預存所需資料
                PunchTime = dic["PunchTime"],
                ConfirmPunchFlag = dic["ConfirmPunchFlag"],
                DutyTime = dic["DutyTime"],
                RestBeginTime = dic["RestBeginTime"],
                RestEndTime = dic["RestEndTime"],

                //Remedy所需資料
                PunchDate = dic["PunchDate"],
                PunchRemedySeq = dic["PunchRemedySeq"],
                FlowCaseID = dic["FlowCaseID"],
                ValidDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                ValidCompID = UserInfo.getUserInfo().CompID.Trim(),
                ValidID = UserInfo.getUserInfo().UserID.Trim(),
                ValidName = UserInfo.getUserInfo().UserName.Trim(),

                //Confirm所需資料
                DutyDate = dic["DutyDate"],
                PunchConfirmSeq = dic["PunchConfirmSeq"],
                RemedyReasonID = dic["RemedyReasonID"],
                RemedyReasonCN = dic["RemedyReasonCN"],
                RemedyPunchTime = dic["RemedyPunchTime"],
                Remedy_MAFT10_FLAG = dic["Remedy_MAFT10_FLAG"],
                Remedy_AbnormalFlag = dic["Remedy_AbnormalFlag"],
                Remedy_AbnormalReasonID = dic["Remedy_AbnormalReasonID"],
                Remedy_AbnormalReasonCN = dic["Remedy_AbnormalReasonCN"],
                Remedy_AbnormalDesc = dic["Remedy_AbnormalDesc"]
            };

            switch (oVerifyInfo["FlowStepBtnID"].ToString())
            {
                //PORemedyStatus。0:未處理，1:未送簽，2:送簽中，3:核准，4:駁回
                //ConfirmStatus。0:正常，1:異常，2:送簽中，3:異常不控管
                case "btnClose":
                    model.PORemedyStatus = "3";
                    string strConfirmStatus = "", strAbnormalType = "";
                    if (!PunchUpdate.PunchAppdOperation_EXEC_PunchCheckData(model, out strConfirmStatus, out strAbnormalType))
                    {
                        Util.MsgBox("打卡異常檢核失敗!!");
                        return;
                    }
                    model.ConfirmStatus = strConfirmStatus; //0 or 1記得用John的TSQL
                    model.AbnormalType = strAbnormalType;
                    break;
                case "btnApprove":
                case "btnReApprove":
                    model.PORemedyStatus = "2";
                    model.ConfirmStatus = "2";
                    break;
                case "btnReject":
                    model.PORemedyStatus = "4";
                    model.ConfirmStatus = "1";
                    break;
            }
            Dictionary<string, string> oAssDic = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);
            DataTable LastHROtherFlowLog = PunchUpdate.HROtherFlowLog(model.FlowCaseID, true);
            long seccessCount = 0;
            string msg="";
            bool result = false;
            result=PunchUpdate.PunchAppdOperation_SaveData(model, oAssDic, oVerifyInfo["FlowStepBtnID"].ToString(), oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), out seccessCount, out msg, LastHROtherFlowLog.Rows[0]["FlowCode"].ToString(), LastHROtherFlowLog.Rows[0]["FlowSN"].ToString(), LastHROtherFlowLog.Rows[0]["FlowSeq"].ToString(), LastHROtherFlowLog.Rows[0]["FlowLogBatNo"].ToString(), LastHROtherFlowLog.Rows[0]["FlowLogID"].ToString(), toUserData);

            if (result)
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                txtErrMsg.Text = msg;
                txtErrMsg.Visible = true;
            }
        }
    }
}
