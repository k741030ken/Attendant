using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程全頁審核
/// </summary>
public partial class FlowExpress_FlowPageVerify : SecurePage
{
    /// <summary>
    /// 指派清單是否同時顯示鍵值的鍵值最大長度
    /// </summary>
    public int _ChkMaxKeyLen
    {
        //2016.07.12 新增
        get
        {
            if (ViewState["_ChkMaxKeyLen"] == null)
            {
                ViewState["_ChkMaxKeyLen"] = int.Parse(Util.getRequestQueryStringKey("ChkMaxKeyLen", "0"));
            }
            return (int)(ViewState["_ChkMaxKeyLen"]);
        }
    }

    /// <summary>
    /// 是否使用 CheckBoxList 顯示複選指派對象(預設 false)
    /// </summary>
    public bool _IsCheckBoxListEnabled
    {
        //2016.09.20 新增
        get
        {
            if (ViewState["_IsCheckBoxListEnabled"] == null)
            {
                ViewState["_IsCheckBoxListEnabled"] = (Util.getRequestQueryStringKey("IsShowCheckBoxList", "N", true) == "Y") ? true : false;
            }
            return (bool)(ViewState["_IsCheckBoxListEnabled"]);
        }
        set
        {
            ViewState["_IsCheckBoxListEnabled"] = value;
        }
    }

    /// <summary>
    /// 回上一頁的(待辦清單)Url
    /// </summary>
    public string _FlowTodoUrl
    {
        //2017.02.23 新增 2017.03.02 改從 QueryString 處理
        get
        {
            if (ViewState["_FlowTodoUrl"] == null)
            {
                ViewState["_FlowTodoUrl"] = Util.getRequestQueryStringKey("FlowTodoUrl", Request.UrlReferrer.AbsoluteUri); //回上一頁 URL
            }
            return (string)(ViewState["_FlowTodoUrl"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();
        if (Util.getRequestQueryStringKey("ProxyType", "", true) == "SEMI")
        {
            //助理無審核權
            labNotAllowVerify.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_AssistantNotAllowVerify);
            labNotAllowVerify.Visible = true;
            return;
        }

        if (!IsPostBack)
        {
            //設定 TabContainer 樣式
            labCustForm.Text = WorkRS.Resources.FlowVerifyTab_CustForm;
            labFlowVerify.Text = WorkRS.Resources.FlowVerifyTab_FlowVerify;
            btnFlowAttach.ucBtnCaption = WorkRS.Resources.FlowVerifyTab_FlowAttach;
            labFlowStepInfo.Text = string.Format(" [{0}-{1}] ", oFlow.FlowCurrStepID, oFlow.FlowCurrStepName);
            btnSaveTempOpinion.Text = WorkRS.Resources.FlowVerifyTab_SaveFlowTempOpinion;

            //檢查 LogStatus
            if (oFlow.FlowCurrLogIsClose)
            {
                labNotAllowVerify.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowLogClosed);
                labNotAllowVerify.Visible = true;
                return;
            }
            else
            {
                ucLightBox.ucLightBoxMsg = string.Format(RS.Resources.Msg_Waiting1, WorkRS.Resources.FlowVerifyTab_btnBack);
                btnBack.Text = WorkRS.Resources.FlowVerifyTab_btnBack;
                btnBack.OnClientClick = ucLightBox.ucShowClientJS + string.Format("location.href='{0}';return false;", _FlowTodoUrl); //回上一頁 2017.02.23 調整

                //強制切到[tabCustForm]
                TabMainContainer.ActiveTabIndex = 0;
                int intTabAreaHeight = oFlow.FlowVerifyPopupHeight;
                int intTabAreaWidth = oFlow.FlowVerifyPopupWidth;

                if (oFlow.FlowVerifyPopupWidth > 0)
                {
                    TabMainContainer.Width = Unit.Pixel(oFlow.FlowVerifyPopupWidth);
                }

                //載入 CustFormUrl 並設定顯示範圍
                string strFlowVerifyCustFormURL = string.Format("{0}?FlowID={1}&FlowLogID={2}", Util.getFixURL(oFlow.FlowVerifyCustFormURL), oFlow.FlowID, oFlow.FlowLogID);
                for (int i = 0; i < oFlow.FlowKeyFieldList.Count(); i++)
                {
                    if (oFlow.FlowKeyFieldList[i].ToUpper() != "AUTONO")
                    {
                        strFlowVerifyCustFormURL += string.Format("&{0}={1}", oFlow.FlowKeyFieldList[i], oFlow.FlowKeyValueList[i]);
                    }
                }
                CustFormFrame.Attributes["src"] = strFlowVerifyCustFormURL;
                CustFormFrame.Attributes["width"] = (intTabAreaWidth - 50).ToString();
                CustFormFrame.Attributes["height"] = (intTabAreaHeight - 5).ToString();

                //流程附件
                btnFlowAttach.Visible = false;
                if (oFlow.FlowCurrStepAttachMaxQty > 0)
                {
                    btnFlowAttach.Visible = true;
                    //btnFlowAttach.ucBtnCssClass = "Util_clsBtnGray";
                    btnFlowAttach.ucAttachDB = oFlow.FlowAttachDB;
                    btnFlowAttach.ucAttachID = string.Format(FlowExpress._FlowAttachIDFormat, oFlow.FlowID, oFlow.FlowLogID);
                    btnFlowAttach.ucAttachFileMaxQty = oFlow.FlowCurrStepAttachMaxQty;
                    btnFlowAttach.ucAttachFileMaxKB = oFlow.FlowCurrStepAttachMaxKB;
                    btnFlowAttach.ucAttachFileTotKB = oFlow.FlowCurrStepAttachTotKB;
                    //btnFlowAttach.Refresh();
                }

                //流程記錄(強制 [不顯示案件表頭][不自動展開])
                string strFlowLogDisplayURL = string.Format("{0}?FlowID={1}&FlowCaseID={2}&FlowLogID={3}&IsShowCaseHeader=N&IsAutoRefresh=N", FlowExpress._FlowLogDisplayURL, oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                FlowLogFrame.Attributes["src"] = strFlowLogDisplayURL;
                FlowLogFrame.Attributes["width"] = (intTabAreaWidth - 20).ToString();
                Util.setJS_SetFrameHeight(FlowLogFrame.ID);

                //上一關意見
                labPrevStepOpinion.Text = WorkRS.Resources.FlowVerifyTab_PrevStepOpinion;
                gvFlowPrevStepLog.ucDataQryTable = FlowExpress.getSecondLastFlowLogData(oFlow.FlowID, oFlow.FlowCaseID);
                gvFlowPrevStepLog.ucDataKeyList = "FlowID,FlowLogID".Split(',');
                Dictionary<string, string> dicDisp = new Dictionary<string, string>();
                dicDisp.Add("FlowStepName", WorkRS.Resources.FlowLogMsg_StepName + "@C"); //關卡
                dicDisp.Add("AssignToName", WorkRS.Resources.FlowLogMsg_AssignTo + "@C"); //處理者
                dicDisp.Add("FlowStepBtnCaption", WorkRS.Resources.FlowLogMsg_VerifyBtn + "@C"); //動作
                dicDisp.Add("LogUpdDateTime", WorkRS.Resources.FlowLogMsg_UpdDateTime + "@T"); //時間
                dicDisp.Add("FlowStepOpinion", WorkRS.Resources.FlowLogMsg_VerifyOpinion + "@C"); //意見
                gvFlowPrevStepLog.ucDataDisplayDefinition = dicDisp;
                gvFlowPrevStepLog.ucSortEnabled = false;
                gvFlowPrevStepLog.ucSeqNoEnabled = false;
                gvFlowPrevStepLog.ucDisplayOnly = true;
                gvFlowPrevStepLog.Refresh(true);

                //常用片語
                ucCommPhrase.ucTargetClientID = txtFlowOpinion.ClientID + "_txtData";
                ucCommPhrase.Refresh();

                //流程片語 2017.03.03
                ucFlowPhrase.ucIsVisibleWhenNoData = false;
                ucFlowPhrase.ucSearchBoxWaterMarkText = WorkRS.Resources.FlowPhrase_WaterMarkText;
                ucFlowPhrase.ucBtnToolTip = WorkRS.Resources.FlowPhrase_btnAppend;
                ucFlowPhrase.ucDBName = FlowExpress._FlowSysDB;
                ucFlowPhrase.ucPKID = oFlow.FlowID;
                ucFlowPhrase.ucPKKind = "FlowCustProperty";
                ucFlowPhrase.ucPropID = "Phrase";
                ucFlowPhrase.ucTargetClientID = txtFlowOpinion.ClientID + "_txtData";
                ucFlowPhrase.Refresh();

                //流程意見
                labFlowOpinion.Text = WorkRS.Resources.FlowVerifyTab_FlowOpinion;
                txtFlowOpinion.ucDispEnteredWordsObjClientID = dispFlowOpinion.ClientID;
                txtFlowOpinion.ucWidth = Convert.ToInt16(intTabAreaWidth / 2) - 70;
                txtFlowOpinion.ucRows = (ucFlowPhrase.Visible) ? 12 : 13; //根據是否有 [流程片語] 自動調整
                txtFlowOpinion.ucMaxLength = oFlow.FlowOpinionMaxLength;
                txtFlowOpinion.ucTextData = FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepOpinion"].ToString();
                txtFlowOpinion.Refresh();

                //流程按鈕
                DataTable dtBtn = Util.getDataTable(FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepBtnInfoJSON"].ToString());
                string strBtnSeqNo = "";
                for (int i = 0; i < dtBtn.Rows.Count; i++)
                {
                    strBtnSeqNo = (i + 1).ToString().PadLeft(2, '0');
                    Button oBtn = (Button)Util.FindControlEx(TabMainContainer, "btnVerify" + strBtnSeqNo);
                    if (oBtn != null)
                    {
                        oBtn.Text = dtBtn.Rows[i]["FlowStepBtnCaption"].ToString();
                        oBtn.CommandName = dtBtn.Rows[i]["FlowStepBtnID"].ToString();
                        oBtn.CommandArgument = (dtBtn.Rows[i]["FlowStepBtnIsNeedOpinion"].ToString().ToUpper() == "Y") ? "Y" : "N"; //是否需輸入意見
                        oBtn.Visible = true;
                    }
                }
            }
        }  //!IsPostBack

        //事件訂閱
        ucModalPopup1.onClose += ucModalPopup1_onClose;
        ucCommUserAdminButton.onLaunch += AdminButton_onLaunchClose;
        ucCommUserAdminButton.onClose += AdminButton_onLaunchClose;
        ucCommPhraseAdminButton.onLaunch += AdminButton_onLaunchClose;
        ucCommPhraseAdminButton.onClose += AdminButton_onLaunchClose;
    }

    void AdminButton_onLaunchClose(object sender, EventArgs e)
    {
        ucCommPhrase.Refresh();
        gvFlowPrevStepLog.Refresh();
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();
        if (oFlow.FlowCurrLogIsClose)
        {
            Response.Redirect(_FlowTodoUrl);
        }
        else
        {
            gvFlowPrevStepLog.Refresh();
        }
    }

    protected void btnSaveTempOpinion_Click(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();
        DbHelper dblog = new DbHelper(oFlow.FlowLogDB);
        string strUpdSQL = string.Format("Update {0}FlowOpenLog Set FlowStepOpinion = N'{2}' Where FlowLogID = '{1}'", oFlow.FlowID, oFlow.FlowLogID, txtFlowOpinion.ucTextData);
        if (dblog.ExecuteNonQuery(CommandType.Text, strUpdSQL) > 0)
        {
            Util.setJSContent("alert('" + string.Format(RS.Resources.Msg_Succeed1, WorkRS.Resources.FlowVerifyTab_SaveFlowTempOpinion) + "');");
        }
        else
        {
            Util.setJSContent("alert('" + string.Format(RS.Resources.Msg_Error1, WorkRS.Resources.FlowVerifyTab_SaveFlowTempOpinion) + "');");
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        Button oBtn = (Button)sender;
        //檢查流程意見是否為必需輸入
        if (string.IsNullOrEmpty(txtFlowOpinion.ucTextData))
        {
            if (oBtn.CommandArgument.ToUpper() == "Y")
            {
                //必需輸入
                Util.setJSContent("alert('" + WorkRS.Resources.FlowVerifyMsg_OpinionRequired + "');");
                return;
            }
            else
            {
                //非必需輸入，填入預設意見
                txtFlowOpinion.ucTextData = WorkRS.Resources.FlowDefOpinion;
            }
        }


        Dictionary<string, string> dicPara = Util.getRequestQueryString();
        if (dicPara.IsNullOrEmpty())
        {
            //缺少流程參數
            Util.setJSContent("alert('" + WorkRS.Resources.FlowFullLogMsg_FlowParaError + "');");
            return;
        }
        else
        {
            ucModalPopup1.Reset();
            ucModalPopup1.ucFrameURL = string.Format("{0}?FlowID={1}&FlowLogID={2}&FlowStepBtnID={3}&FlowOpinion={4}", FlowExpress._FlowPageVerifyFrameURL, dicPara["FlowID"], dicPara["FlowLogID"], oBtn.CommandName, HttpUtility.UrlEncode(txtFlowOpinion.ucTextData)); //FlowExpress._FlowPageVerifyFrameURL + "?Dummy=" + Util.getRandomCode();
            ucModalPopup1.ucPopupWidth = 680;
            ucModalPopup1.ucPopupHeight = 570;
            ucModalPopup1.Show();
        }


        //FlowExpress oFlow = new FlowExpress();
        //DataTable dtBtn = Util.getDataTable(FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepBtnInfoJSON"].ToString());
        //if (dtBtn != null && dtBtn.Select(string.Format("FlowStepBtnID = '{0}' ", oBtn.CommandName)).Count() > 0)
        //{
        //    DataRow drBtn = dtBtn.Select(string.Format("FlowStepBtnID = '{0}' ", oBtn.CommandName))[0];
        //    Dictionary<string, string> dicBtnAssignToList = new Dictionary<string, string>();
        //    Dictionary<string, string> dicBtnContext = new Dictionary<string, string>();

        //    dicBtnContext.Clear();
        //    dicBtnContext.Add("FlowID", oFlow.FlowID);
        //    dicBtnContext.Add("FlowLogID", oFlow.FlowLogID);
        //    dicBtnContext.Add("VerifyStepInfo", labFlowStepInfo.Text);

        //    dicBtnContext.Add("VerifyOpinion", txtFlowOpinion.ucTextData);
        //    dicBtnContext.Add("FlowStepBtnID", drBtn["FlowStepBtnID"].ToString());
        //    dicBtnContext.Add("FlowStepBtnIsAddMultiSubFlow", drBtn["FlowStepBtnIsAddMultiSubFlow"].ToString());
        //    dicBtnContext.Add("FlowStepBtnAddSubFlowID", drBtn["FlowStepBtnAddSubFlowID"].ToString());
        //    dicBtnContext.Add("FlowStepBtnAddSubFlowStepBtnID", drBtn["FlowStepBtnAddSubFlowStepBtnID"].ToString());
        //    if (!string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowID"].ToString()) && !string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString()))
        //    {
        //        //若會新增子流程
        //        if (oFlow.FlowCurrLogStepID != drBtn["FlowStepBtnNextStepID"].ToString())
        //        {
        //            //若主流程下一關不為同關卡代號，才傳遞主流程的原始指派清單備用 2015.08.26 優化
        //            dicBtnContext.Add("FlowStepBtnAssignToList", drBtn["FlowStepBtnAssignToList"].ToString());
        //        }
        //        else
        //        {
        //            dicBtnContext.Add("FlowStepBtnAssignToList", "");
        //        }
        //    }
        //    Session["PageVerifyData"] = dicBtnContext; //參數傳遞

        //    ucModalPopup1.Reset();
        //    ucModalPopup1.ucFrameURL = string.Format("{0}?FlowID={1}&FlowLogID={2}&FlowStepBtnID={3}&FlowOpinion={4}", FlowExpress._FlowPageVerifyFrameURL, oFlow.FlowID, oFlow.FlowLogID, oBtn.CommandName, HttpUtility.UrlEncode(txtFlowOpinion.ucTextData)); //FlowExpress._FlowPageVerifyFrameURL + "?Dummy=" + Util.getRandomCode();
        //    ucModalPopup1.ucPopupWidth = 680;
        //    ucModalPopup1.ucPopupHeight = 550;
        //    ucModalPopup1.Show();
        //}
    }

}