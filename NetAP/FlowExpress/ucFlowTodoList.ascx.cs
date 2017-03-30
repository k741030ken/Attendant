using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Work.Properties;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程待辦清單控制項
/// </summary>
public partial class FlowExpress_ucFlowTodoList : BaseUserControl
{
    #region 屬性
    /// <summary>
    /// 指定欲處理流程清單(ex: [FlowID1,FlowID2])
    /// </summary>
    public string[] ucFlowIDList
    {
        get
        {
            if (ViewState["_FlowIDList"] == null)
            {
                ViewState["_FlowIDList"] = "".Split(',');
            }
            return (string[])(ViewState["_FlowIDList"]);
        }
        set
        {
            ViewState["_FlowIDList"] = value;
        }
    }

    /// <summary>
    /// 是否允許使用者自行[重建]審核資料(預設 true)
    /// </summary>
    public bool ucIsEnabledUserReBuild
    {
        get
        {
            if (ViewState["_IsEnabledUserReBuild"] == null)
            {
                ViewState["_IsEnabledUserReBuild"] = true;  //2015.06.18 調整
            }
            return (bool)(ViewState["_IsEnabledUserReBuild"]);
        }
        set
        {
            ViewState["_IsEnabledUserReBuild"] = value;
        }
    }


    /// <summary>
    /// 是否顯示自身(Self)的待辦清單(預設 true)
    /// </summary>
    public bool ucIsEnabledSelfTodoList
    {
        get
        {
            if (ViewState["_IsEnabledSelfTodoList"] == null)
            {
                ViewState["_IsEnabledSelfTodoList"] = true;
            }
            return (bool)(ViewState["_IsEnabledSelfTodoList"]);
        }
        set
        {
            ViewState["_IsEnabledSelfTodoList"] = value;
        }
    }

    /// <summary>
    /// 是否顯示代理(Proxy)的待辦清單(預設 true)
    /// </summary>
    public bool ucIsEnabledProxyTodoList
    {
        get
        {
            if (ViewState["_IsEnabledProxyTodoList"] == null)
            {
                ViewState["_IsEnabledProxyTodoList"] = true;
            }
            return (bool)(ViewState["_IsEnabledProxyTodoList"]);
        }
        set
        {
            ViewState["_IsEnabledProxyTodoList"] = value;
        }
    }


    /// <summary>
    /// 特定代理清單過濾條件(預設 空白)
    /// </summary>
    public string ucProxyFullFilter
    {
        get
        {
            if (ViewState["_ProxyFullFilter"] == null)
            {
                ViewState["_ProxyFullFilter"] = "";
            }
            return (string)(ViewState["_ProxyFullFilter"]);
        }
        set
        {
            ViewState["_ProxyFullFilter"] = value;
        }
    }

    /// <summary>
    /// 特定助理清單過濾條件(預設 空白)
    /// </summary>
    public string ucProxySemiFilter
    {
        get
        {
            if (ViewState["_ProxySemiFilter"] == null)
            {
                ViewState["_ProxySemiFilter"] = "";
            }
            return (string)(ViewState["_ProxySemiFilter"]);
        }
        set
        {
            ViewState["_ProxySemiFilter"] = value;
        }
    }

    /// <summary>
    /// GridLines 樣式(預設 Both)
    /// </summary>
    public GridLines ucGridLines
    {
        get
        {
            if (ViewState["_GridLines"] == null)
            {
                ViewState["_GridLines"] = GridLines.Both;
            }
            return (GridLines)ViewState["_GridLines"];
        }
        set
        {
            ViewState["_GridLines"] = value;
        }
    }

    /// <summary>
    /// 群組抬頭格式(預設 《{0}》)
    /// </summary>
    public string ucGroupHeaderFormat
    {
        get
        {
            if (ViewState["_GroupHeaderFormat"] == null)
            {
                ViewState["_GroupHeaderFormat"] = "《{0}》";  //2015.06.18 調整
            }
            return (string)(ViewState["_GroupHeaderFormat"]);
        }
        set
        {
            ViewState["_GroupHeaderFormat"] = value;
        }
    }

    /// <summary>
    /// 複選指派對象時是否使用 ChkBoxList(預設 false)
    /// </summary>
    public bool ucIsUsingCheckBoxListWhenMultiSelect
    {
        // 2016.09.21 新增
        get
        {
            if (ViewState["_IsUsingCheckBoxListWhenMultiSelect"] == null)
            {
                ViewState["_IsUsingCheckBoxListWhenMultiSelect"] = false;
            }
            return (bool)(ViewState["_IsUsingCheckBoxListWhenMultiSelect"]);
        }
        set
        {
            ViewState["_IsUsingCheckBoxListWhenMultiSelect"] = value;
        }
    }

    /// <summary>
    /// 是否為彈出式審核(預設 false)
    /// </summary>
    public bool ucIsPopupVerifyEnabled
    {
        get
        {
            if (ViewState["_IsPopupVerifyEnabled"] == null)
            {
                ViewState["_IsPopupVerifyEnabled"] = false; //2017.02.07 預設值改為 false
            }
            return (bool)(ViewState["_IsPopupVerifyEnabled"]);
        }
        set
        {
            ViewState["_IsPopupVerifyEnabled"] = value;
        }
    }

    /// <summary>
    /// 審核彈出視窗是否顯示[Complete]按鈕(預設 false)
    /// </summary>
    public bool ucPopupBtnCompleteEnabled
    {
        get
        {
            if (ViewState["_PopupBtnCompleteEnabled"] == null)
            {
                ViewState["_PopupBtnCompleteEnabled"] = false;
            }
            return (bool)(ViewState["_PopupBtnCompleteEnabled"]);
        }
        set
        {
            ViewState["_PopupBtnCompleteEnabled"] = value;
        }
    }

    /// <summary>
    /// 審核彈出視窗是否顯示[Close]按鈕(預設 true)
    /// </summary>
    public bool ucPopupBtnCloseEnabled
    {
        get
        {
            if (ViewState["_PopupBtnCloseEnabled"] == null)
            {
                ViewState["_PopupBtnCloseEnabled"] = true;
            }
            return (bool)(ViewState["_PopupBtnCloseEnabled"]);
        }
        set
        {
            ViewState["_PopupBtnCloseEnabled"] = value;
        }
    }

    /// <summary>
    /// 是否利用[ucUpdUserTodoQtyUrlFormat]更新待辦數字(預設 false)
    /// </summary>
    /// <remarks>2015.12.25 新增</remarks>
    public bool ucUpdUserTodoQtyEnabled
    {
        get
        {
            if (ViewState["_UpdUserTodoQtyEnabled"] == null)
            {
                ViewState["_UpdUserTodoQtyEnabled"] = false;
            }
            return (bool)(ViewState["_UpdUserTodoQtyEnabled"]);
        }
        set
        {
            ViewState["_UpdUserTodoQtyEnabled"] = value;
        }
    }

    /// <summary>
    /// 依據網址格式更新遠端待辦數字(參數依序為 UserID 、 TodoQty)
    /// <para>格式範例：[http&#58;//x.x.x.x/UpdUserTodoQty.ashx&#63;UserID=&#123;0&#125;&#38;ToDoQty=&#123;1&#125;&#38;TodoNotiID=TodoG01]</para>
    /// </summary>
    public string ucUpdUserTodoQtyUrlFormat
    {
        //2015.12.25 新增
        get
        {
            if (ViewState["_UpdUserTodoQtyUrlFormat"] == null)
            {

                ViewState["_UpdUserTodoQtyUrlFormat"] = "";
            }
            return (string)(ViewState["_UpdUserTodoQtyUrlFormat"]);
        }
        set
        {
            ViewState["_UpdUserTodoQtyUrlFormat"] = value;
        }
    }


    /// <summary>
    /// 允許指派清單內容可同時顯示鍵值時的鍵值最大長度(預設 0，代表不檢查)
    /// </summary>
    public int ucChkMaxKeyLen
    {
        //2016.07.12 新增
        get
        {
            if (ViewState["_ChkMaxKeyLen"] == null)
            {
                ViewState["_ChkMaxKeyLen"] = 0;
            }
            return (int)(ViewState["_ChkMaxKeyLen"]);
        }
        set
        {
            ViewState["_ChkMaxKeyLen"] = value;
        }
    }

    /// <summary>
    /// 自訂批次審核URL
    /// <para>**由於批次作業時可能會面臨每個案件的自訂審核URL皆不同，故需額外自訂專用URL**</para>
    /// </summary>
    public string ucCustBatchVerifyUrl
    {
        //2017.03.01 新增
        get
        {
            if (ViewState["_CustBatchVerifyUrl"] == null)
            {

                ViewState["_CustBatchVerifyUrl"] = "";
            }
            return (string)(ViewState["_CustBatchVerifyUrl"]);
        }
        set
        {
            ViewState["_CustBatchVerifyUrl"] = value;
        }
    }

    /// <summary>
    /// 審核子視窗的[Close]事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void btnCloseClick(object sender, EventArgs e);
    /// <summary>
    /// 審核子視窗的[Complete]事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void btnCompleteClick(object sender, EventArgs e);
    public event btnCloseClick onClose;
    public event btnCompleteClick onComplete;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        labSelfTodoList.Text = WorkRS.Resources.FlowTodoList_SelfTodoList;
        labProxyTodoList.Text = WorkRS.Resources.FlowTodoList_ProxyTodoList;

        btnSelfTodoList.Visible = false;
        btnProxyTodoList.Visible = false;

        //若允許使用者自行[重建]審核資料，則產生相關按鈕
        if (ucIsEnabledUserReBuild)
        {
            btnSelfTodoList.Visible = true;
            btnSelfTodoList.ImageUrl = Util.Icon_Refresh;
            btnSelfTodoList.ToolTip = RS.Resources.Msg_Rebuild;
            Util.ConfirmBox(btnSelfTodoList, RS.Resources.Msg_Confirm_Refresh);

            btnProxyTodoList.Visible = true;
            btnProxyTodoList.ImageUrl = Util.Icon_Refresh;
            btnProxyTodoList.ToolTip = RS.Resources.Msg_Rebuild;
            Util.ConfirmBox(btnProxyTodoList, RS.Resources.Msg_Confirm_Refresh);
        }

        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCloselEnabled = ucPopupBtnCloseEnabled;
        ucModalPopup1.ucBtnCompleteEnabled = ucPopupBtnCompleteEnabled;

        //事件訂閱
        gvSelfTodoList.RowCommand += new Util_ucGridView.GridViewRowClick(gvSelfTodoList_RowCommand);
        gvSelfTodoList.GridViewCommand += new Util_ucGridView.GridViewClick(gvSelfTodoList_GridViewCommand);
        gvProxyTodoList.RowCommand += new Util_ucGridView.GridViewRowClick(gvProxyTodoList_RowCommand);
        gvProxyTodoList.GridViewCommand += new Util_ucGridView.GridViewClick(gvProxyTodoList_GridViewCommand);

        ucModalPopup1.ucBtnCompleteHeader = WorkRS.Resources.FlowVerifyTab_btnClose;
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        ucModalPopup1.onComplete += new Util_ucModalPopup.btnCompleteClick(ucModalPopup1_onComplete);
    }

    //待辦清單重新整理
    protected void btnSelfTodoList_Click(object sender, ImageClickEventArgs e)
    {
        gvSelfTodoList.ucDataQryTable = FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.Assign, UserInfo.getUserInfo(), ucFlowIDList, null, true, ucProxyFullFilter, ucProxySemiFilter);
        this.Refresh(true);
    }

    //代理清單重新整理
    protected void btnProxyTodoList_Click(object sender, ImageClickEventArgs e)
    {
        gvProxyTodoList.ucDataQryTable = FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.Proxy, UserInfo.getUserInfo(), ucFlowIDList, null, true, ucProxyFullFilter, ucProxySemiFilter);
        this.Refresh(true);
    }


    /// <summary>
    /// gvSelfTodoList 呼叫批次審核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gvSelfTodoList_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        DataTable dtVerify = getBatchVerifyData(e.DataTable, gvSelfTodoList.ucDataQryTable);
        if (dtVerify != null && dtVerify.Rows.Count > 0)
        {
            if (e.DataTable.Rows.Count > dtVerify.Rows.Count)
            {
                //若[批次審核]筆數少於原始傳遞筆數 2016.08.17
                Util.NotifyMsg(string.Format(WorkRS.Resources.FlowVerifyMsg_BatchVerifyCaseAutoIgnore1, e.DataTable.Rows.Count - dtVerify.Rows.Count));
            }

            StartBatchVerify(dtVerify);
        }
        else
        {
            //2016.08.17 改用 NotifyMsg 方式
            Util.NotifyMsg(WorkRS.Resources.FlowVerifyMsg_BatchVerifyCaseNotFound, Util.NotifyKind.Error);
        }
    }

    /// <summary>
    /// gvSelfTodoList 呼叫單筆審核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gvSelfTodoList_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        StartSingleVerify(e.DataKeys[0], e.DataKeys[1], e.DataKeys[2]);
    }

    /// <summary>
    /// gvProxyTodoList 呼叫批次審核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gvProxyTodoList_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        DataTable dtVerify = getBatchVerifyData(e.DataTable, gvProxyTodoList.ucDataQryTable);
        if (dtVerify != null && dtVerify.Rows.Count > 0)
        {
            if (e.DataTable.Rows.Count > dtVerify.Rows.Count)
            {
                //若[批次審核]筆數少於原始傳遞筆數 2016.08.17
                Util.NotifyMsg(string.Format(WorkRS.Resources.FlowVerifyMsg_BatchVerifyCaseAutoIgnore1, e.DataTable.Rows.Count - dtVerify.Rows.Count));
            }

            StartBatchVerify(dtVerify);
        }
        else
        {
            //2016.08.17 改用 NotifyMsg 方式
            Util.NotifyMsg(WorkRS.Resources.FlowVerifyMsg_BatchVerifyCaseNotFound, Util.NotifyKind.Error);
        }
    }

    /// <summary>
    /// gvProxyTodoList 呼叫單筆審核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void gvProxyTodoList_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        StartSingleVerify(e.DataKeys[0], e.DataKeys[1], e.DataKeys[2]);
    }

    /// <summary>
    /// 重新整理 gvSelfTodoList 及 gvProxyTodoList
    /// </summary>
    /// <param name="IsInit"></param>
    public void Refresh(bool IsInit = false)
    {
        FlowExpress oFlow = new FlowExpress(ucFlowIDList[0]);
        if (IsInit)
        {
            //初始共用物件
            Dictionary<string, string> oPropDic = new Dictionary<string, string>();
            oPropDic.Clear();
            oPropDic.Add("ucIsRequire", "True");
            oPropDic.Add("ucWidth", "70");

            Dictionary<string, string> oAssJSONDic = new Dictionary<string, string>();
            oAssJSONDic.Clear();
            oAssJSONDic.Add("JSON", "FlowBatchVerifyStepBtnAssignToList"); //指派對象來源欄位

            Dictionary<string, string> oEditDic = new Dictionary<string, string>();
            oEditDic.Add("FlowBatchVerifyStepBtnAssignToInfo", "DropDownFirst@" + Util.getJSON(oAssJSONDic));  //改用 DropDownFirst 2016.08.05
            oEditDic.Add("FlowStepOpinion", "TextBox@" + Util.getJSON(oPropDic));

            // == gvSelfTodoList ==
            gvSelfTodoList.ucGridLines = ucGridLines;
            gvSelfTodoList.ucDataKeyList = "FlowID,FlowLogID,ProxyType".Split(',');
            Dictionary<string, string> oSelfDispDic = new Dictionary<string, string>();

            //審核進度
            if (!string.IsNullOrEmpty(ucGroupHeaderFormat))
            {
                gvSelfTodoList.ucGroupHeaderFormat = ucGroupHeaderFormat;
            }
            gvSelfTodoList.ucDataGroupKey = "FlowStepInfo";
            //子流程圖示
            oSelfDispDic.Add("FlowSubCaseIcon", "<img src='" + Util.Icon_TreeNode + "' style='padding-top:3px;float:left;border-style:none;' />@L");
            //FlowShowFieldList
            for (int i = 0; i < oFlow.FlowShowFieldList.Count(); i++)
            {
                oSelfDispDic.Add(oFlow.FlowShowFieldList[i], oFlow.FlowShowCaptionList[i]);
            }

            //批次審核
            if (oFlow.FlowBatchEnabled)
            {
                //審核動作
                oSelfDispDic.Add("FlowStepBtnInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnInfo);
                //下一關卡
                //oSelfDispDic.Add("FlowStepBtnNextStepInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnNextStepInfo);
                //派送對象
                oSelfDispDic.Add("FlowBatchVerifyStepBtnAssignToInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnAssignToInfo);
                //審核意見
                oSelfDispDic.Add("FlowStepOpinion", WorkRS.Resources.FlowTodoList_FlowStepOpinion + "@L100");

                //批次審核編輯欄位定義
                gvSelfTodoList.ucDataEditDefinition = oEditDic;
                gvSelfTodoList.ucCheckEnabled = true;
                gvSelfTodoList.ucCheckEnabledDataColName = "FlowStepBatchEnabled";  //動態判斷可批次審核的資料列
            }
            gvSelfTodoList.ucDataDisplayDefinition = oSelfDispDic;

            //流程資訊
            gvSelfTodoList.ucHoverTooltipTemplete = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Dialog);
            Dictionary<string, string> oSelfTipDic = new Dictionary<string, string>();
            oSelfTipDic.Add("FlowSubCaseIcon", "FlowID,FlowTipInfo");
            gvSelfTodoList.ucDataDisplayToolTipDefinition = oSelfTipDic;

            //設定資料來源
            gvSelfTodoList.ucDataQryTable = FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.Assign, UserInfo.getUserInfo(), ucFlowIDList, null, false, ucProxyFullFilter, ucProxySemiFilter);
            gvSelfTodoList.ucDefSortExpression = "FlowStepInfo";

            //設定功能按鈕
            gvSelfTodoList.ucSelectEnabled = true;
            gvSelfTodoList.ucSelectIcon = Util.Icon_Approve;
            gvSelfTodoList.ucSelectToolTip = WorkRS.Resources.FlowVerifyMsg_SingleVerify;

            gvSelfTodoList.ucUpdateAllCaption = WorkRS.Resources.FlowVerifyMsg_BatchVerify;
            gvSelfTodoList.ucUpdateAllConfirm = WorkRS.Resources.FlowVerifyMsg_BatchVerifyConfirm;
            gvSelfTodoList.ucDeleteAllEnabled = false;

            // == gvProxyTodoList ==
            gvProxyTodoList.ucGridLines = ucGridLines;
            gvProxyTodoList.ucDataKeyList = "FlowID,FlowLogID,ProxyType".Split(',');
            Dictionary<string, string> oProxyDispDic = new Dictionary<string, string>();
            //審核進度
            if (!string.IsNullOrEmpty(ucGroupHeaderFormat))
            {
                gvProxyTodoList.ucGroupHeaderFormat = ucGroupHeaderFormat;
            }
            gvProxyTodoList.ucDataGroupKey = "FlowStepInfo";
            //子流程圖示
            oProxyDispDic.Add("FlowSubCaseIcon", "<img src='" + Util.Icon_TreeNode + "' style='padding-top:3px;float:left;border-style:none;' />@L");
            //原處理者
            oProxyDispDic.Add("AssignToName", WorkRS.Resources.FlowTodoList_SelfAssignToName);
            //FlowShowFieldList
            for (int i = 0; i < oFlow.FlowShowFieldList.Count(); i++)
            {
                oProxyDispDic.Add(oFlow.FlowShowFieldList[i], oFlow.FlowShowCaptionList[i]);
            }

            //批次審核
            if (oFlow.FlowBatchEnabled)
            {
                //審核動作
                oProxyDispDic.Add("FlowStepBtnInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnInfo);
                //下一關卡
                //oProxyDispDic.Add("FlowStepBtnNextStepInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnNextStepInfo);
                //派送對象
                oProxyDispDic.Add("FlowBatchVerifyStepBtnAssignToInfo", WorkRS.Resources.FlowTodoList_FlowStepBtnAssignToInfo);

                //審核意見
                oProxyDispDic.Add("FlowStepOpinion", WorkRS.Resources.FlowTodoList_FlowStepOpinion + "@L100");

                //批次審核編輯欄位定義
                gvProxyTodoList.ucDataEditDefinition = oEditDic;
                gvProxyTodoList.ucCheckEnabled = true;
                gvProxyTodoList.ucCheckEnabledDataColName = "FlowStepBatchEnabled";  //動態判斷可批次審核的資料列
            }
            gvProxyTodoList.ucDataDisplayDefinition = oProxyDispDic;


            //流程資訊
            gvProxyTodoList.ucHoverTooltipTemplete = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Dialog);
            Dictionary<string, string> oProxyTipDic = new Dictionary<string, string>();
            oProxyTipDic.Add("FlowSubCaseIcon", "FlowID,FlowTipInfo");
            gvProxyTodoList.ucDataDisplayToolTipDefinition = oProxyTipDic;

            //設定資料來源
            gvProxyTodoList.ucDataQryTable = FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.Proxy, UserInfo.getUserInfo(), ucFlowIDList, null, false, ucProxyFullFilter, ucProxySemiFilter);
            gvProxyTodoList.ucDefSortExpression = "FlowStepInfo";
            //設定功能按鈕
            gvProxyTodoList.ucSelectEnabled = true;
            gvProxyTodoList.ucSelectIcon = Util.Icon_Approve;
            gvProxyTodoList.ucSelectToolTip = WorkRS.Resources.FlowVerifyMsg_SingleVerify;

            gvProxyTodoList.ucUpdateAllCaption = WorkRS.Resources.FlowVerifyMsg_BatchVerify;
            gvProxyTodoList.ucUpdateAllConfirm = WorkRS.Resources.FlowVerifyMsg_BatchVerifyConfirm;
            gvProxyTodoList.ucDeleteAllEnabled = false;
        }

        //顯示自身(Self)待辦清單
        DivSelfTodoList.Visible = ucIsEnabledSelfTodoList;
        gvSelfTodoList.Refresh(IsInit);

        //顯示代理(Proxy)待辦清單
        DivProxyTodoList.Visible = ucIsEnabledProxyTodoList;
        gvProxyTodoList.Refresh(IsInit);

        //2015.12.25 新增，主動向遠端更新待辦數字
        if (ucUpdUserTodoQtyEnabled)
        {
            if (!string.IsNullOrEmpty(ucUpdUserTodoQtyUrlFormat))
            {
                int intTodoQty = 0;

                if (ucIsEnabledSelfTodoList)
                {
                    intTodoQty += gvSelfTodoList.ucDataQryTable.Rows.Count;
                }

                if (ucIsEnabledProxyTodoList)
                {
                    intTodoQty += gvProxyTodoList.ucDataQryTable.Rows.Count;
                }

                //利用 iframe 發動更新，方便 Client 端進行除錯
                string strTodoUrl = string.Format(ucUpdUserTodoQtyUrlFormat, UserInfo.getUserInfo().UserID, intTodoQty);
                LiteralControl oFrame = new LiteralControl(string.Format("<iframe height='1px' width='1px' frameborder='0' style='display:none;' src='{0}'></iframe>", strTodoUrl));
                this.Page.Form.Controls.Add(oFrame);
            }
        }


    }

    /// <summary>
    /// 進行單筆審核
    /// </summary>
    /// <param name="strFlowID"></param>
    /// <param name="strFlowLogID"></param>
    protected void StartSingleVerify(string strFlowID, string strFlowLogID, string strProxyType)
    {
        FlowExpress oFlow = new FlowExpress(strFlowID, strFlowLogID);
        string strVerifyUrlPara = string.Format("?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}", oFlow.FlowID, oFlow.FlowLogID, strProxyType, (ucPopupBtnCompleteEnabled == true) ? "Y" : "N", (ucIsUsingCheckBoxListWhenMultiSelect == true) ? "Y" : "N", ucChkMaxKeyLen);
        //判斷操作模式是 彈出式(PopUp)審核 還是 轉址式(Redirect)審核  2015.05.22 / 2017.02.02 分拆成兩隻 url
        if (!ucIsPopupVerifyEnabled)
        {
            //轉址式 2017.03.02 調整傳遞參數
            Response.Redirect(FlowExpress._FlowPageVerifyURL + strVerifyUrlPara + "&FlowTodoUrl=" + HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
        }
        else
        {
            //彈出式
            ucModalPopup1.ucFrameURL = FlowExpress._FlowPopVerifyURL + strVerifyUrlPara;

            if (oFlow.FlowVerifyPopupHeight > 0)
                ucModalPopup1.ucPopupHeight = oFlow.FlowVerifyPopupHeight;

            if (oFlow.FlowVerifyPopupWidth > 0)
                ucModalPopup1.ucPopupWidth = oFlow.FlowVerifyPopupWidth;

            ucModalPopup1.Show();
        }
    }

    /// <summary>
    /// 進行批次審核
    /// </summary>
    /// <param name="dtVerify"></param>
    protected void StartBatchVerify(DataTable dtVerify)
    {
        Session["BatchVerifyData"] = dtVerify;

        ucModalPopup1.ucPopupHeight = 450;
        ucModalPopup1.ucPopupWidth = 650;
        ucModalPopup1.ucFrameURL = string.IsNullOrEmpty(ucCustBatchVerifyUrl)?FlowExpress._FlowBatchVerifyURL: ucCustBatchVerifyUrl; //2017.03.01 新增 ucCustBatchVerifyUrl
        ucModalPopup1.Show();
    }

    /// <summary>
    /// 取出適合進行流程審核的資料表
    /// </summary>
    /// <param name="dtKey"></param>
    /// <param name="dtSource"></param>
    /// <returns></returns>
    protected DataTable getBatchVerifyData(DataTable dtKey, DataTable dtSource)
    {
        //從 GridView 取得的 dtKey (含 Key / FlowBatchVerifyStepBtnAssignToInfo / FlowStepOpinion 欄位)，與原始GridView Source比對

        //移除[任意指派對象]類型的案件 2016.08.17
        DataRow[] keyRows = dtKey.Select("FlowBatchVerifyStepBtnAssignToInfo <> '*'");
        if (keyRows.Count() > 0)
        {
            dtKey = keyRows.CopyToDataTable();
        }
        else
        {
            return null;
        }

        //取出適合進行流程審核的資料表
        string strFilter = "";
        for (int i = 0; i < dtKey.Rows.Count; i++)
        {
            if (strFilter.Length > 0)
            {
                strFilter += " Or ";
            }
            string[] strKeyList = dtKey.Rows[i]["key"].ToString().Split(',');
            strFilter += string.Format(" ( FlowID = '{0}' And FlowLogID = '{1}' ) ", strKeyList[0], strKeyList[1]);
        }

        DataRow[] drList = dtSource.Select(strFilter);
        DataTable dtVerify = new DataTable();
        dtVerify.Columns.Add("FlowID");
        dtVerify.Columns.Add("FlowLogID");
        dtVerify.Columns.Add("ProxyType");
        dtVerify.Columns.Add("FlowStepBtnID");

        dtVerify.Columns.Add("FlowStepBtnIsAddMultiSubFlow");
        dtVerify.Columns.Add("FlowStepBtnAddSubFlowID");
        dtVerify.Columns.Add("FlowStepBtnAddSubFlowStepBtnID");
        dtVerify.Columns.Add("FlowStepOpinion");

        dtVerify.Columns.Add("FlowStepBtnNextStepID");   //主流程下一關卡，當會產生子流程時，主流程是否需審核，需檢查此關卡代號是否與目前停留代號相通 2015.08.26 優化
        dtVerify.Columns.Add("FlowStepBtnAssignToList"); //主流程指派對象
        dtVerify.Columns.Add("FlowStepBtnAddSubFlowStepBtnAssignToList"); //子流程指派對象
        dtVerify.Columns.Add("FlowBatchVerifyStepBtnAssignToList"); //批次審核指派對象

        for (int i = 0; i < drList.Count(); i++)
        {
            string strAssignID = dtKey.Select(string.Format("Key = '{0},{1},{2}' ", drList[i]["FlowID"], drList[i]["FlowLogID"], drList[i]["ProxyType"]))[0]["FlowBatchVerifyStepBtnAssignToInfo"].ToString();
            string strOpinion = dtKey.Select(string.Format("Key = '{0},{1},{2}' ", drList[i]["FlowID"], drList[i]["FlowLogID"], drList[i]["ProxyType"]))[0]["FlowStepOpinion"].ToString();


            DataRow drVerify = dtVerify.NewRow();
            drVerify["FlowID"] = drList[i]["FlowID"];
            drVerify["FlowLogID"] = drList[i]["FlowLogID"];
            drVerify["ProxyType"] = drList[i]["ProxyType"];
            drVerify["FlowStepBtnID"] = drList[i]["FlowStepBtnID"];

            drVerify["FlowStepBtnIsAddMultiSubFlow"] = drList[i]["FlowStepBtnIsAddMultiSubFlow"];
            drVerify["FlowStepBtnAddSubFlowID"] = drList[i]["FlowStepBtnAddSubFlowID"];
            drVerify["FlowStepBtnAddSubFlowStepBtnID"] = drList[i]["FlowStepBtnAddSubFlowStepBtnID"];
            drVerify["FlowStepOpinion"] = strOpinion;

            drVerify["FlowStepBtnNextStepID"] = drList[i]["FlowStepBtnNextStepID"];
            drVerify["FlowStepBtnAssignToList"] = drList[i]["FlowStepBtnAssignToList"];
            drVerify["FlowStepBtnAddSubFlowStepBtnAssignToList"] = drList[i]["FlowStepBtnAddSubFlowStepBtnAssignToList"];
            drVerify["FlowBatchVerifyStepBtnAssignToList"] = Util.getJSON(Util.getDictionary(Util.getDictionary(drList[i]["FlowBatchVerifyStepBtnAssignToList"].ToString()), strAssignID.Split(',')));
            dtVerify.Rows.Add(drVerify);
        }
        return dtVerify;
    }


    /// <summary>
    /// 彈出視窗 Complete
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ucModalPopup1_onComplete(object sender, Util_ucModalPopup.btnCompleteEventArgs e)
    {
        if (onComplete != null)
        {
            onComplete(this, e);
        }
        this.Refresh(true);
    }

    /// <summary>
    /// 彈出視窗 Close
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        if (onClose != null)
        {
            onClose(this, e);
        }
        this.Refresh(true);
    }

    /// <summary>
    /// 彈出視窗 Close (批審後專用，由iFrame呼叫）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Refresh(true);
    }
}