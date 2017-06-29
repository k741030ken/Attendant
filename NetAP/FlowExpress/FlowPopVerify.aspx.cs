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
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using RS = SinoPac.WebExpress.Common.Properties;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程單筆審核
/// </summary>
public partial class FlowExpress_FlowPopVerify : SecurePage
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
                if (ViewState["_ChkMaxKeyLen"] == null)
                {
                    ViewState["_ChkMaxKeyLen"] = int.Parse(Util.getRequestQueryStringKey("ChkMaxKeyLen", "0"));
                }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();

        //設定 TabContainer 樣式
        labCustForm.Text = WorkRS.Resources.FlowVerifyTab_CustForm;
        labFlowVerify.Text = WorkRS.Resources.FlowVerifyTab_FlowVerify;
        labFlowAttach.Text = WorkRS.Resources.FlowVerifyTab_FlowAttach;
        labFlowFullLog.Text = WorkRS.Resources.FlowVerifyTab_FlowFullLog;

        btnSaveTempOpinion.Text = WorkRS.Resources.FlowVerifyTab_SaveFlowTempOpinion;
        labFlowVerifyButton.Text = string.Format(" [{0}-{1}] ", oFlow.FlowCurrStepID, oFlow.FlowCurrStepName);

        //設定「開始審核」按鈕相關機制
        ucLightBox.ucLightBoxMsg = WorkRS.Resources.FlowVerifyMsg_FlowVerifyProcessing;
        string strJS = "var isStartVerify = true;";

        //檢查是否必需輸入意見 2014.11.20 調整判斷方式
        //若需輸入意見，但為空白，則出現警告訊息
        //若不一定需輸入意見，但為空白，則自動填上預設意見
        strJS += "var oOpiConfirm = document.getElementById('" + opiStartVerify.ClientID + "');";
        strJS += "var oOpinion = document.getElementById('TabMainContainer_tabFlowVerify_txtFlowOpinion_txtData');"; //ucTextBox的內部物件
        strJS += "if (oOpiConfirm.value == 'Y') {";
        strJS += "     if (oOpinion.value.trim().length == 0) {alert('" + WorkRS.Resources.FlowVerifyMsg_OpinionRequired + "'); isStartVerify = false; return false;}";
        strJS += "} else {";
        strJS += "     if (oOpinion.value.trim().length == 0) { oOpinion.value='" + oFlow.FlowDefOpinion + "';}";
        strJS += "} ";

        //是否需出現確認訊息
        strJS += "var oMsgConfirm = document.getElementById('" + msgStartVerify.ClientID + "');";
        strJS += "if (oMsgConfirm.value.length > 0) {";
        strJS += "   if (!confirm(oMsgConfirm.value)) {isStartVerify = false; return false;}";
        strJS += "}";

        strJS += "if (isStartVerify) {";
        strJS += "   this.style.display = 'none';";
        strJS += ucLightBox.ucShowClientJS;
        strJS += "   var oClose = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
        strJS += "   if (oClose != null){oClose.style.display='none';}";
        strJS += "   var oComplete = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
        strJS += "   if (oComplete != null){oComplete.style.display='none';}";
        strJS += "}";

        btnStartVerify.OnClientClick = strJS;
        btnStartVerify.Text = WorkRS.Resources.FlowVerifyTab_btnStartVerify;

        if (Util.getRequestQueryStringKey("ProxyType", "", true) == "SEMI")
        {
            //助理無審核權
            btnStartVerify.Visible = false;
            labNotVerify.Text = WorkRS.Resources.FlowVerifyMsg_AssistantNotAllowVerify;
            labNotVerify.Visible = true;
        }

        if (!IsPostBack)
        {
            if (oFlow.FlowCurrLogIsClose)
            {
                // FlowCurrLogIsClose = true
                DivVerifyBtnArea.Visible = false;
                DivVerifyMsgArea.Visible = true;
                labFlowVerifyMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowLogClosed);
            }
            else
            {
                //強制切到[tabCustForm]
                TabMainContainer.ActiveTabIndex = 0;
                //設定 TabMainContainer 顯示範圍(依據BtnComplete是否顯示自動計算)
                int intTabAreaHeight = oFlow.FlowVerifyPopupHeight - 140;
                if (Util.getRequestQueryStringKey("IsShowBtnComplete", "Y", true) == "Y")
                {
                    intTabAreaHeight = oFlow.FlowVerifyPopupHeight - 140;
                }
                else
                {
                    intTabAreaHeight = oFlow.FlowVerifyPopupHeight - 110;
                }

                int intTabAreaWidth = oFlow.FlowVerifyPopupWidth - 40;
                if (oFlow.FlowVerifyPopupWidth > 0)
                {
                    TabMainContainer.Width = Unit.Pixel(intTabAreaWidth);
                    divVerifyArea.Style["width"] = string.Format("{0}px", intTabAreaWidth.ToString());
                }
                if (oFlow.FlowVerifyPopupHeight > 0)
                {
                    TabMainContainer.Height = Unit.Pixel(intTabAreaHeight);
                }
                //載入 CustFormUrl 並設定顯示範圍
                string strFlowVerifyCustFormURL = string.Format("{0}?FlowID={1}&FlowLogID={2}", Util.getFixURL(oFlow.FlowVerifyCustFormURL), oFlow.FlowID, oFlow.FlowLogID);
                for (int i = 0; i < oFlow.FlowKeyFieldList.Count(); i++)
                {
                    if (oFlow.FlowKeyFieldList[i].ToUpper() != "_AUTONO")
                    {
                        strFlowVerifyCustFormURL += string.Format("&{0}={1}", oFlow.FlowKeyFieldList[i], oFlow.FlowKeyValueList[i]);
                    }
                }
                CustFormFrame.Attributes["src"] = strFlowVerifyCustFormURL;
                CustFormFrame.Attributes["width"] = (intTabAreaWidth - 10).ToString();
                CustFormFrame.Attributes["height"] = (intTabAreaHeight - 5).ToString();

                //流程附檔
                tabFlowAttach.Visible = false;
                if (oFlow.FlowCurrStepAttachMaxQty > 0)
                {
                    tabFlowAttach.Visible = true;
                    string strFlowAttachID = string.Format(FlowExpress._FlowAttachIDFormat, oFlow.FlowID, oFlow.FlowLogID);
                    string strFlowAttachURL = Util._AttachAdminUrl;
                    strFlowAttachURL += string.Format("?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}", oFlow.FlowAttachDB, strFlowAttachID, oFlow.FlowCurrStepAttachMaxQty, oFlow.FlowCurrStepAttachMaxKB, oFlow.FlowCurrStepAttachTotKB, Util.getStringJoin(oFlow.FlowCurrStepAttachExtList));
                    FlowAttachFrame.Attributes["src"] = strFlowAttachURL;
                    FlowAttachFrame.Attributes["width"] = (intTabAreaWidth - 20).ToString();
                    FlowAttachFrame.Attributes["height"] = (intTabAreaHeight - 10).ToString();
                }

                //流程記錄
                //新增 FlowLogID 2016.10.14
                string strFlowLogDisplayURL = string.Format("{0}?FlowID={1}&FlowCaseID={2}&FlowLogID={3}", FlowExpress._FlowLogDisplayURL, oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                FlowLogFrame.Attributes["src"] = strFlowLogDisplayURL;
                FlowLogFrame.Attributes["width"] = (intTabAreaWidth - 20).ToString();
                FlowLogFrame.Attributes["height"] = (intTabAreaHeight - 10).ToString();
                Util.setJS_SetFrameHeight(FlowLogFrame.ID);

                //流程意見
                labFlowOpinion.Text = WorkRS.Resources.FlowVerifyTab_FlowOpinion;
                txtFlowOpinion.ucRows = 3;
                txtFlowOpinion.ucDispEnteredWordsObjClientID = dispFlowOpinion.ClientID;
                txtFlowOpinion.ucWidth = intTabAreaWidth - 50;
                txtFlowOpinion.ucMaxLength = oFlow.FlowOpinionMaxLength;
                txtFlowOpinion.ucTextData = FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepOpinion"].ToString();
                txtFlowOpinion.Refresh();

                //按鈕清單
                DataTable dtBtn = Util.getDataTable(FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepBtnInfoJSON"].ToString());
                Dictionary<string, string> dicBtnAssignToList = new Dictionary<string, string>();

                //用 dtBtn 初始按鈕及指派對象
                TabVerifyContainer.ActiveTabIndex = 0;
                Dictionary<string, string> dicBtnContext = new Dictionary<string, string>();
                string strBtnSeqNo = "";
                for (int i = 0; i < dtBtn.Rows.Count; i++)
                {
                    strBtnSeqNo = (i + 1).ToString().PadLeft(2, '0');
                    AjaxControlToolkit.TabPanel TabVerify = (AjaxControlToolkit.TabPanel)Util.FindControlEx(TabVerifyContainer, "TabVerify" + strBtnSeqNo);

                    if (TabVerify != null)
                    {
                        Label TabVerifyHeader = (Label)Util.FindControlEx(TabVerify, "TabVerifyHeader" + strBtnSeqNo);
                        if (TabVerifyHeader != null)
                        {
                            TabVerifyHeader.Text = dtBtn.Rows[i]["FlowStepBtnCaption"].ToString();
                        }

                        TabVerify.Visible = true;
                        //設定審核前的確認訊息(由JS作檢查)
                        ((HiddenField)Util.FindControlEx(this, "msgVerify" + strBtnSeqNo, true)).Value = dtBtn.Rows[i]["FlowStepBtnConfirmMsg"].ToString().Trim();
                        //設定檢核意見是否必需輸入(由JS作檢查)
                        ((HiddenField)Util.FindControlEx(this, "opiVerify" + strBtnSeqNo, true)).Value = (dtBtn.Rows[i]["FlowStepBtnIsNeedOpinion"].ToString().ToUpper() == "Y") ? "Y" : "N";

                        dicBtnContext.Clear();
                        dicBtnContext.Add("FlowStepBtnID", dtBtn.Rows[i]["FlowStepBtnID"].ToString());
                        dicBtnContext.Add("FlowStepBtnIsAddMultiSubFlow", dtBtn.Rows[i]["FlowStepBtnIsAddMultiSubFlow"].ToString());
                        dicBtnContext.Add("FlowStepBtnAddSubFlowID", dtBtn.Rows[i]["FlowStepBtnAddSubFlowID"].ToString());
                        dicBtnContext.Add("FlowStepBtnAddSubFlowStepBtnID", dtBtn.Rows[i]["FlowStepBtnAddSubFlowStepBtnID"].ToString());
                        if (!string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnAddSubFlowID"].ToString()) && !string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString()))
                        {
                            //若會新增子流程
                            if (oFlow.FlowCurrLogStepID != dtBtn.Rows[i]["FlowStepBtnNextStepID"].ToString())
                            {
                                //若主流程下一關不為同關卡代號，才傳遞主流程的原始指派清單備用 2015.08.26 優化
                                dicBtnContext.Add("FlowStepBtnAssignToList", dtBtn.Rows[i]["FlowStepBtnAssignToList"].ToString());
                            }
                            else
                            {
                                dicBtnContext.Add("FlowStepBtnAssignToList", "");
                            }
                        }
                        TabVerify.DynamicContextKey = Util.getJSON(dicBtnContext); //儲存必要的傳遞參數，方便執行審核時使用

                        Label labVerify = (Label)Util.FindControlEx(TabVerify, "labVerify" + strBtnSeqNo);
                        labVerify.CssClass = "Util_txtDone";

                        if (string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnAddSubFlowID"].ToString()) && string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString()))
                        {
                            //一般指派處理
                            dicBtnAssignToList = Util.getDictionary(dtBtn.Rows[i]["FlowStepBtnAssignToList"].ToString());
                            //按鈕提示訊息
                            if (!string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnDesc"].ToString()))
                            {
                                labVerify.CssClass = "Util_txtErr";
                                labVerify.Text = "※ " + dtBtn.Rows[i]["FlowStepBtnDesc"].ToString();
                            }
                            else
                            {
                                labVerify.Text = string.Format(WorkRS.Resources.FlowVerifyTab_labVerifyToolTip1, dtBtn.Rows[i]["FlowStepBtnNextStepID"].ToString() + "-" + dtBtn.Rows[i]["FlowStepBtnNextStepName"].ToString());
                            }
                        }
                        else
                        {
                            //會新增子流程的指派處理
                            dicBtnAssignToList = Util.getDictionary(dtBtn.Rows[i]["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString());
                            //按鈕提示訊息
                            if (!string.IsNullOrEmpty(dtBtn.Rows[i]["FlowStepBtnDesc"].ToString()))
                            {
                                labVerify.CssClass = "Util_txtErr";
                                labVerify.Text = "※ " + dtBtn.Rows[i]["FlowStepBtnDesc"].ToString();
                            }
                            else
                            {
                                FlowExpress oSubFlow = new FlowExpress(dtBtn.Rows[i]["FlowStepBtnAddSubFlowID"].ToString(), null, false, false);
                                labVerify.Text = string.Format(WorkRS.Resources.FlowVerifyTab_labVerifyAddSubToolTip1, dtBtn.Rows[i]["FlowStepBtnNextStepID"].ToString() + "-" + dtBtn.Rows[i]["FlowStepBtnNextStepName"].ToString(), oSubFlow.FlowName);
                            }
                        }

                        if (dicBtnAssignToList.Count > 0)
                        {
                            if (dicBtnAssignToList.Count == 1)
                            {
                                //當指派清單只有一筆資料
                                if (dicBtnAssignToList.First().Key == "*")
                                {
                                    //若為任意指派
                                    Util_ucUserPicker anyVerify = (Util_ucUserPicker)Util.FindControlEx(TabVerify, "anyVerify" + strBtnSeqNo);
                                    UserInfo oUser = UserInfo.getUserInfo();
                                    if (anyVerify != null)
                                    {
                                        switch (dicBtnAssignToList.First().Value.ToUpper())
                                        {
                                            case "COMP":
                                                //同公司對象
                                                if (oUser != null)
                                                {
                                                    anyVerify.ucIsSelectCommUserYN = "N";
                                                    anyVerify.ucValidCompIDList = oUser.CompID;
                                                }
                                                break;
                                            case "PART":
                                                //同公司(含兼職)對象
                                                if (oUser != null)
                                                {
                                                    anyVerify.ucIsSelectCommUserYN = "N";
                                                    string[] oPartList = Util.getArray(oUser.PartInfoTable, "CompID");
                                                    if (oPartList != null)
                                                    {
                                                        anyVerify.ucValidCompIDList = Util.getStringJoin(Util.getCompareList(oUser.CompID.Split(','), oPartList, Util.ListCompareMode.Merge));
                                                    }
                                                    else
                                                    {
                                                        anyVerify.ucValidCompIDList = oUser.CompID;
                                                    }
                                                }
                                                break;
                                            default:
                                                // [* / ANY] 任意指派 2014.10.20 新增
                                                break;
                                        }

                                        if (dtBtn.Rows[i]["FlowStepBtnIsMultiSelect"].ToString().ToUpper() != "N")
                                        {
                                            //若不為[N]單選
                                            anyVerify.ucIsMultiSelectYN = "Y";
                                        }

                                        if (oUser != null)
                                        {
                                            //預設為自己部門 2016.06.06
                                            anyVerify.ucDefCompID = oUser.CompID;
                                            anyVerify.ucDefDeptID = oUser.DeptID;
                                        }
                                        anyVerify.ucWidth = 350;
                                        anyVerify.Visible = true;
                                        anyVerify.Refresh();
                                    }
                                }
                                else
                                {
                                    //若為單一對象
                                    ListBox allVerify = (ListBox)Util.FindControlEx(TabVerify, "allVerify" + strBtnSeqNo);
                                    if (allVerify != null)
                                    {
                                        //檢查可顯示鍵值的鍵值最大長度
                                        if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                            allVerify.DataSource = dicBtnAssignToList;
                                        else
                                            allVerify.DataSource = Util.getDictionary(dicBtnAssignToList);

                                        allVerify.DataTextField = "value";
                                        allVerify.DataValueField = "key";
                                        allVerify.CssClass = "Util_clsDropDownListReadOnly"; //2017.02.02
                                        allVerify.DataBind();
                                        allVerify.Width = 400;
                                        allVerify.Height = 22;
                                        allVerify.Rows = 2; //避免出現捲軸
                                        allVerify.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                //若指派清單有多筆資料
                                switch (dtBtn.Rows[i]["FlowStepBtnIsMultiSelect"].ToString().ToUpper())
                                {
                                    case "A":
                                        //全選 2014.11.05
                                        ListBox allVerify = (ListBox)Util.FindControlEx(TabVerify, "allVerify" + strBtnSeqNo);
                                        Label labAllVerify = (Label)Util.FindControlEx(TabVerify, "labAllVerify" + strBtnSeqNo);
                                        if (allVerify != null && labAllVerify != null)
                                        {
                                            //檢查可顯示鍵值的鍵值最大長度
                                            if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                                allVerify.DataSource = dicBtnAssignToList;
                                            else
                                                allVerify.DataSource = Util.getDictionary(dicBtnAssignToList);

                                            allVerify.DataTextField = "value";
                                            allVerify.DataValueField = "key";
                                            allVerify.CssClass = "Util_clsDropDownListReadOnly"; //2017.02.02
                                            allVerify.DataBind();
                                            allVerify.Width = 400;
                                            allVerify.Rows = 10;

                                            labAllVerify.ForeColor = System.Drawing.Color.DarkRed;
                                            labAllVerify.Text = WorkRS.Resources.FlowVerifyTab_labAllAssigned;

                                            allVerify.Visible = true;
                                            labAllVerify.Visible = true;
                                        }
                                        break;
                                    case "Y":
                                        //複選
                                        //2016.09.20 改成支援 ucCommMultiSelect 或是 ucCheckBoxList 實作
                                        if (_IsCheckBoxListEnabled)
                                        {
                                            //使用 ucCheckBoxList
                                            Util_ucCheckBoxList chkVerify = (Util_ucCheckBoxList)Util.FindControlEx(TabVerify, "chkVerify" + strBtnSeqNo);
                                            if (chkVerify != null)
                                            {
                                                if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                                    chkVerify.ucSourceDictionary = dicBtnAssignToList;
                                                else
                                                    chkVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                                chkVerify.ucWidth = 400;
                                                chkVerify.ucRows = 10;
                                                chkVerify.ucChkBoxListWidth = 410;
                                                chkVerify.ucChkBoxListOffsetX = -8;
                                                chkVerify.ucChkBoxListHeight = 170;
                                                chkVerify.ucChkBoxListOffsetY = (Util.getIEVersion() > 0) ? -145 : -15;
                                                chkVerify.ucRangeMaxQty = dicBtnAssignToList.Count;
                                                chkVerify.ucIsAutoPopWhenNoSelection = true;  //若無選擇結果，就自動彈出候選清單 2016.09.01

                                                chkVerify.Refresh();
                                                chkVerify.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            //使用 ucCommMultiSelect
                                            Util_ucCommMultiSelect muiVerify = (Util_ucCommMultiSelect)Util.FindControlEx(TabVerify, "muiVerify" + strBtnSeqNo);
                                            if (muiVerify != null)
                                            {
                                                if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                                    muiVerify.ucSourceDictionary = dicBtnAssignToList;
                                                else
                                                    muiVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                                muiVerify.ucMultiSelectOffsetY = -15;
                                                muiVerify.ucBoxListHeight = 100;
                                                muiVerify.ucBoxListWidth = 200;
                                                muiVerify.Refresh();
                                                muiVerify.Visible = true;
                                            }
                                        }
                                        break;
                                    case "N":
                                        //單選
                                        Util_ucCommSingleSelect oneVerify = (Util_ucCommSingleSelect)Util.FindControlEx(TabVerify, "oneVerify" + strBtnSeqNo);
                                        if (oneVerify != null)
                                        {
                                            if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                                oneVerify.ucSourceDictionary = dicBtnAssignToList;
                                            else
                                                oneVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                            oneVerify.Refresh();
                                            oneVerify.ucDropDownSourceListWidth = 250;
                                            oneVerify.Visible = true;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                //設定「開始審核」初始狀態
                opiStartVerify.Value = opiVerify01.Value;
                msgStartVerify.Value = msgVerify01.Value;
            }
        }  //!IsPostBack
    }

    protected void btnSaveTempOpinion_Click(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();
        DbHelper dblog = new DbHelper(oFlow.FlowLogDB);
        string strUpdSQL = string.Format("Update {0}FlowOpenLog Set FlowStepOpinion = N'{2}' Where FlowLogID = '{1}'", oFlow.FlowID, oFlow.FlowLogID, txtFlowOpinion.ucTextData);
        if (dblog.ExecuteNonQuery(CommandType.Text, strUpdSQL) > 0)
        {
            Util.NotifyMsg(RS.Resources.Msg_Succeed, Util.NotifyKind.Success);
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Msg_Error, Util.NotifyKind.Error);
        }
    }

    protected void btnStartVerify_Click(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress();
        labFlowVerifyMsg.Text = "";
        string strShowPopBtnJS = "";

        //取得流程意見，若為空白則補上預設值
        if (string.IsNullOrEmpty(txtFlowOpinion.ucTextData)) { txtFlowOpinion.ucTextData = oFlow.FlowDefOpinion; }
        //取得指派對象，需將 value 中包含的項目Key移除
        AjaxControlToolkit.TabPanel TabVerify = TabVerifyContainer.ActiveTab;
        string strBtnSeqNo = TabVerify.ID.Right(2);
        Dictionary<string, string> oAssDic = new Dictionary<string, string>();

        //單選指派
        Util_ucCommSingleSelect oneVerify = (Util_ucCommSingleSelect)Util.FindControlEx(TabVerify, "oneVerify" + strBtnSeqNo);
        if (oneVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(oneVerify.ucSelectedDictionary, false));
        }
        //複選指派
        //可能是 ucCommMultiSelect 或是 ucCheckBoxList 2016.09.20
        Util_ucCommMultiSelect muiVerify = (Util_ucCommMultiSelect)Util.FindControlEx(TabVerify, "muiVerify" + strBtnSeqNo);
        if (muiVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(muiVerify.ucSelectedDictionary, false));
        }

        Util_ucCheckBoxList chkVerify = (Util_ucCheckBoxList)Util.FindControlEx(TabVerify, "chkVerify" + strBtnSeqNo);
        if (chkVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(chkVerify.ucSelectedDictionary, false));
        }


        //2014.10.20 新增
        //任意指派
        Util_ucUserPicker anyVerify = (Util_ucUserPicker)Util.FindControlEx(TabVerify, "anyVerify" + strBtnSeqNo);
        if (anyVerify.Visible && !string.IsNullOrEmpty(anyVerify.ucSelectedUserIDList))
        {
            for (int i = 0; i < anyVerify.ucSelectedUserIDList.Split(',').Count(); i++)
            {
                oAssDic.Add(anyVerify.ucSelectedUserIDList.Split(',')[i], UserInfo.findUserName(anyVerify.ucSelectedUserIDList.Split(',')[i]));
            }
        }

        //2014.11.05 新增
        //全部指派
        ListBox allVerify = (ListBox)Util.FindControlEx(TabVerify, "allVerify" + strBtnSeqNo);
        if (allVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(Util.getDictionary(allVerify.GetAllItems()), false));
        }

        //只顯示 [tabFlowVerify] 頁籤 2017.02.07
        tabCustForm.Visible = false;
        tabFlowAttach.Visible = false;
        tabFlowFullLog.Visible = false;
        DivVerifyBtnArea.Visible = false;
        DivVerifyMsgArea.Visible = true;

        strShowPopBtnJS += "var oClose = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
        strShowPopBtnJS += "if (oClose != null){oClose.style.display='';}";
        strShowPopBtnJS += "var oComplete = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
        strShowPopBtnJS += "if (oComplete != null){oComplete.style.display='';}";
        Util.setJSContent(strShowPopBtnJS);

        if (oAssDic.Count > 0)
        {
            //指派對象有值，可進行審核
            labFlowVerifyMsg.Text = "";
            Dictionary<string, string> dicBtnContext = Util.getDictionary(TabVerify.DynamicContextKey);  //取出btn參數
            string strFlowStepBtnID = dicBtnContext["FlowStepBtnID"].ToString().Trim();
            string strFlowStepBtnIsAddMultiSubFlow = dicBtnContext["FlowStepBtnIsAddMultiSubFlow"].ToString().Trim().ToUpper();
            string strFlowStepBtnAddSubFlowID = dicBtnContext["FlowStepBtnAddSubFlowID"].ToString().Trim();
            string strFlowStepBtnAddSubFlowStepBtnID = dicBtnContext["FlowStepBtnAddSubFlowStepBtnID"].ToString().Trim();
            string strFlowStepOpinion = txtFlowOpinion.ucTextData;

            bool IsNeedAddSubFlow = false;
            bool IsAddSubFlowSucceed = false;

            //檢查是否有按鈕停止條件 2017.05.25 新增
            string[] oStopReasonList;
            string strStopResonMsg;
            if (FlowExpress.IsFlowStepButtonStop(oFlow, strFlowStepBtnID, out oStopReasonList))
            {
                strStopResonMsg = string.Format(WorkRS.Resources.FlowVerifyMsg_StopVerify1, oFlow.FlowCaseHtmlInfo);
                if (!oStopReasonList.IsNullOrEmpty())
                {
                    string strStopReasonTip = string.Format(" {0} \n", WorkRS.Resources.FlowVerifyMsg_StopReasonTipTitle);
                    for (int i = 0; i < oStopReasonList.Length; i++)
                    {
                        strStopReasonTip += string.Format(" ● {0}\n", oStopReasonList[i]);
                    }
                    strStopResonMsg = string.Format("<span title=\"{0}\">{1}</span>", strStopReasonTip, strStopResonMsg);
                }
                labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, strStopResonMsg);
                return;
            }

            //檢查是否有自訂審核URL
            if (!string.IsNullOrEmpty(oFlow.FlowCurrStepCustVerifyURL))
            {
                if (System.IO.File.Exists(Server.MapPath(oFlow.FlowCurrStepCustVerifyURL)))
                {
                    //==若有自訂審核URL==
                    //設定Session傳遞參數[FlowVerifyInfo]
                    dicBtnContext.Add("FlowStepAssignToList", Util.getJSON(oAssDic));
                    dicBtnContext.Add("FlowStepOpinion", strFlowStepOpinion);
                    dicBtnContext.Add("FlowVerifyJS", strShowPopBtnJS);
                    Session["FlowVerifyInfo"] = dicBtnContext;
                    //因為 Ajax Page 無法使用Server.Transfer，故 CustVerifyURL 改用 Response.Redirect()執行
                    //設定 CustVerifyURL
                    string strVerifyURL = string.Format("{0}?FlowID={1}&FlowLogID={2}", oFlow.FlowCurrStepCustVerifyURL, oFlow.FlowID, oFlow.FlowLogID);
                    for (int i = 0; i < oFlow.FlowKeyFieldList.Count(); i++)
                    {
                        if (oFlow.FlowKeyFieldList[i].ToUpper() != "_AUTONO")
                        {
                            strVerifyURL += string.Format("&{0}={1}", oFlow.FlowKeyFieldList[i], oFlow.FlowKeyValueList[i]);
                        }
                    }
                    Response.Redirect(strVerifyURL);
                }
                else
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_NotExist1, oFlow.FlowCurrStepCustVerifyURL));
                }
                return;
            }

            //==無自訂審核URL==
            //處理[新增子流程]
            if (!string.IsNullOrEmpty(strFlowStepBtnAddSubFlowID) && !string.IsNullOrEmpty(strFlowStepBtnAddSubFlowStepBtnID))
            {
                //檢查是否自動新增子流程
                FlowExpress oSubFlow = new FlowExpress(strFlowStepBtnAddSubFlowID, null, false, false);
                //該子流程需符合以下條件才能自動新增：
                //01.子流程與父流程名稱不同
                //  子流程的 KeyFieldList 需只比主流程多出 _AutoNo 欄位　
                //  子流程的 KeyFieldsList 需與主流程的 KeyShowFieldList 相同
                //02.子流程與父流程名稱相同(遞迴)
                //  KeyFieldList 最後一欄需為 _AutoNo 欄位　
                if (oFlow.FlowID != strFlowStepBtnAddSubFlowID)
                {
                    //01.子流程與父流程名稱不同
                    string[] diffList = Util.getCompareList(oFlow.FlowKeyFieldList, oSubFlow.FlowKeyFieldList, Util.ListCompareMode.Diff);
                    if (diffList.Count() == 1 && diffList[0].ToUpper() == "_AUTONO")
                    {
                        if (Util.getCompareList(oFlow.FlowShowFieldList, oSubFlow.FlowShowFieldList, Util.ListCompareMode.Diff).Count() == 0)
                        {
                            IsNeedAddSubFlow = true;
                            if (strFlowStepBtnIsAddMultiSubFlow == "Y")
                            {
                                Dictionary<string, string> oTmpAss = new Dictionary<string, string>();
                                //每個指派對象都產生獨立子流程(只要其中一個子流程新增失敗，就自動中斷)
                                IsAddSubFlowSucceed = true;
                                foreach (var pair in oAssDic)
                                {
                                    oTmpAss.Clear();
                                    oTmpAss.Add(pair.Key, pair.Value);
                                    if (IsAddSubFlowSucceed)
                                    {
                                        if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
                                        {
                                            IsAddSubFlowSucceed = true;
                                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                        }
                                        else
                                        {
                                            IsAddSubFlowSucceed = false;
                                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //所有指派對象只產生一個子流程
                                if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
                                {
                                    IsAddSubFlowSucceed = true;
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                }
                                else
                                {
                                    IsAddSubFlowSucceed = false;
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //02.子流程與父流程名稱相同(遞迴)
                    if (oFlow.FlowKeyFieldList[oFlow.FlowKeyFieldList.Count() - 1].ToUpper() == "_AUTONO")
                    {
                        IsNeedAddSubFlow = true;
                        //組合子流程的 KeyValueList
                        string[] subKeyValueList = new string[oFlow.FlowKeyFieldList.Count() - 1];
                        for (int i = 0; i < subKeyValueList.Count(); i++)
                        {
                            subKeyValueList[i] = oFlow.FlowKeyValueList[i];
                        }

                        if (strFlowStepBtnIsAddMultiSubFlow == "Y")
                        {
                            Dictionary<string, string> oTmpAss = new Dictionary<string, string>();
                            //每個指派對象都產生獨立子流程(只要其中一個子流程新增失敗，就自動中斷)
                            IsAddSubFlowSucceed = true;
                            foreach (var pair in oAssDic)
                            {
                                oTmpAss.Clear();
                                oTmpAss.Add(pair.Key, pair.Value);
                                if (IsAddSubFlowSucceed)
                                {
                                    if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
                                    {
                                        IsAddSubFlowSucceed = true;
                                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                    }
                                    else
                                    {
                                        IsAddSubFlowSucceed = false;
                                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //所有指派對象只產生一個子流程
                            if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
                            {
                                IsAddSubFlowSucceed = true;
                                labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                            }
                            else
                            {
                                IsAddSubFlowSucceed = false;
                                labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                            }
                        }
                    }
                }
            }

            //處理[主流程審核]
            if (IsNeedAddSubFlow)
            {
                //需先[新增子流程]
                if (IsAddSubFlowSucceed)
                {
                    //若[新增子流程]成功
                    //當[FlowStepBtnAssignToList]有值，才執行主流程的[一般審核]  2015.08.26 優化
                    oAssDic.Clear();
                    oAssDic = Util.getDictionary(dicBtnContext["FlowStepBtnAssignToList"].ToString());
                    if (oAssDic != null && oAssDic.Count > 0)
                    {
                        if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinion))
                        {
                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                        }
                        else
                        {
                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                            //2014.11.27 顯示審核錯誤原因
                            DataTable dtError = FlowExpressTraceLog.getFlowExpressErrorLogData(oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                            if (dtError != null && dtError.Rows.Count > 0)
                            {
                                labFlowVerifyMsg.Text += "<ul>";
                                for (int i = 0; i < dtError.Rows.Count; i++)
                                {
                                    labFlowVerifyMsg.Text += string.Format("<li style='color:gray;'>{0}</li>", dtError.Rows[i]["LogDesc"].ToString().Trim());
                                }
                                labFlowVerifyMsg.Text += "</ul>";
                            }
                        }
                    }
                }
            }
            else
            {
                //一般審核
                if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinion))
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                }
                else
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                    //2014.11.27 顯示審核錯誤原因
                    DataTable dtError = FlowExpressTraceLog.getFlowExpressErrorLogData(oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                    if (dtError != null && dtError.Rows.Count > 0)
                    {
                        labFlowVerifyMsg.Text += "<ul>";
                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            labFlowVerifyMsg.Text += string.Format("<li style='color:gray;'>{0}</li>", dtError.Rows[i]["LogDesc"].ToString().Trim());
                        }
                        labFlowVerifyMsg.Text += "</ul>";
                    }
                }
            }
        }
        else
        {
            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_AssignToNotFound);
        }
    }

}