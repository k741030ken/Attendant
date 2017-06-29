using System;
using System.Web;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [處理過程按鈕] 控制項
/// </summary>
public partial class Util_ucProcessButton : BaseUserControl
{

    #region 自訂屬性

    protected string _defPopDisplayClientJS
    {
        get
        {
            string strJS = "Util_IsChkDirty = false;";
            strJS += ucLightBox.ucShowClientJS;

            return strJS;
        }
    }

    protected string _defPopCloseClientJS
    {
        get
        {
            string strJS = "dom.Ready(function(){ ";
            strJS += ucLightBox.ucHideClientJS;
            strJS += "});";

            return strJS;
        }
    }

    /// <summary>
    /// 按鈕是否啟用(預設 true)
    /// </summary>
    public bool ucBtnEnabled
    {
        //2017.03.21 新增
        get
        {
            return btnStart.Enabled;
        }
        set
        {
            btnStart.Enabled = value;
        }
    }

    /// <summary>
    /// 按鈕抬頭
    /// </summary>
    public string ucBtnCaption
    {
        get
        {
            if (PageViewState["_BtnCaption"] == null)
            {
                PageViewState["_BtnCaption"] = RS.Resources.Msg_ProcessStart;
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_BtnCaption"]));
        }
        set
        {
            PageViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕提示訊息
    /// </summary>
    public string ucBtnToolTip
    {
        //2017.05.19 新增
        get
        {
            if (PageViewState["_BtnToolTip"] == null)
            {
                PageViewState["_BtnToolTip"] = string.Empty;
            }
            return PageViewState["_BtnToolTip"].ToString();
        }
        set
        {
            PageViewState["_BtnToolTip"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式(預設 Util_clsBtnGray)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (PageViewState["_BtnStyle"] == null)
            {
                PageViewState["_BtnStyle"] = "Util_clsBtnGray";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_BtnStyle"]));
        }
        set
        {
            PageViewState["_BtnStyle"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (PageViewState["_BtnWidth"] == null)
            {
                PageViewState["_BtnWidth"] = 80;
            }
            return (int)(PageViewState["_BtnWidth"]);
        }
        set
        {
            PageViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 提示訊息
    /// </summary>
    public string ucConfirmMsg
    {
        get
        {
            if (PageViewState["_ConfirmMsg"] == null)
            {
                PageViewState["_ConfirmMsg"] = RS.Resources.Msg_Confirm;
            }
            return HttpUtility.HtmlEncode(PageViewState["_ConfirmMsg"].ToString());
        }
        set
        {
            PageViewState["_ConfirmMsg"] = value;
        }
    }

    /// <summary>
    /// 燈箱訊息
    /// </summary>
    public string ucProcessLightboxMsg
    {
        get
        {
            if (PageViewState["_ProcessLightboxMsg"] == null)
            {
                PageViewState["_ProcessLightboxMsg"] = RS.Resources.Msg_ExportDataPreparing;
            }
            ucLightBox.ucLightBoxMsg = HttpUtility.HtmlEncode(PageViewState["_ProcessLightboxMsg"].ToString());
            return HttpUtility.HtmlEncode(PageViewState["_ProcessLightboxMsg"].ToString());
        }
        set
        {
            PageViewState["_ProcessLightboxMsg"] = value;
            ucLightBox.ucLightBoxMsg = value;
        }
    }


    /// <summary>
    /// 是否允許中斷(預設 false)
    /// </summary>
    public bool ucProcessBreakEnabled
    {
        get
        {
            if (PageViewState["_ProcessBreakEnabled"] == null)
            {
                PageViewState["_ProcessBreakEnabled"] = false;
            }
            return (bool)PageViewState["_ProcessBreakEnabled"];
        }
        set
        {
            PageViewState["_ProcessBreakEnabled"] = value;
        }
    }
    #endregion

    #region 自訂事件
    /// <summary>
    /// 控制項 Start 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Start(object sender, EventArgs e);

    /// <summary>
    /// 控制項 Start 事件
    /// </summary>
    public event Start onStart;

    /// <summary>
    /// 控制項 Break 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Break(object sender, EventArgs e);

    /// <summary>
    /// 控制項 Break 事件
    /// </summary>
    public event Break onBreak;

    protected void btnStart_Click(object sender, EventArgs e)
    {
        if (onStart != null)
        {
            onStart(this, e);
        }
    }

    protected void ucLightBox_onBreak(object sender, EventArgs e)
    {
        if (onBreak != null)
        {
            onBreak(this, e);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh();
        }
        ucLightBox.onBreak += ucLightBox_onBreak;
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        //顯示屬性
        btnStart.Text = ucBtnCaption;
        btnStart.CssClass = ucBtnCssClass;
        btnStart.Width = ucBtnWidth;
        ucLightBox.ucLightBoxBreakEnabled = ucProcessBreakEnabled;

        if (!string.IsNullOrEmpty(ucBtnToolTip)) //2017.05.19
            btnStart.ToolTip = ucBtnToolTip;

        if (string.IsNullOrEmpty(ucConfirmMsg))
            btnStart.OnClientClick = _defPopDisplayClientJS;
        else
            btnStart.OnClientClick = "if (confirm('" + ucConfirmMsg + "')) { " + _defPopDisplayClientJS + " } else{ return false; }";
    }

    /// <summary>
    /// 處理完畢
    /// </summary>
    /// <param name="strNotifyHtmlMsg">通知訊息</param>
    /// <param name="eNotifyKind">通知類別</param>
    public void Complete(string strNotifyHtmlMsg = "", Util.NotifyKind eNotifyKind = Util.NotifyKind.Normal)
    {
        Util.setJSContent(_defPopCloseClientJS, this.ClientID + "_Complete");
        if (!string.IsNullOrEmpty(strNotifyHtmlMsg))
        {
            Util.NotifyMsg(strNotifyHtmlMsg, eNotifyKind);
        }
    }

}