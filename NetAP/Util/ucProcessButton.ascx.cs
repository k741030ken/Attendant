using System;
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
    /// 按鈕抬頭
    /// </summary>
    public string ucBtnCaption
    {
        get
        {
            if (ViewState["_BtnCaption"] == null)
            {
                ViewState["_BtnCaption"] = RS.Resources.Msg_ProcessStart;
            }
            return (string)(ViewState["_BtnCaption"]);
        }
        set
        {
            ViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式(預設 Util_clsBtnGray)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (ViewState["_BtnStyle"] == null)
            {
                ViewState["_BtnStyle"] = "Util_clsBtnGray";
            }
            return (string)(ViewState["_BtnStyle"]);
        }
        set
        {
            ViewState["_BtnStyle"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (ViewState["_BtnWidth"] == null)
            {
                ViewState["_BtnWidth"] = 80;
            }
            return (int)(ViewState["_BtnWidth"]);
        }
        set
        {
            ViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 提示訊息
    /// </summary>
    public string ucConfirmMsg
    {
        get
        {
            if (ViewState["_ConfirmMsg"] == null)
            {
                ViewState["_ConfirmMsg"] = RS.Resources.Msg_Confirm;
            }
            return ViewState["_ConfirmMsg"].ToString();
        }
        set
        {
            ViewState["_ConfirmMsg"] = value;
        }
    }

    /// <summary>
    /// 燈箱訊息
    /// </summary>
    public string ucProcessLightboxMsg
    {
        get
        {
            if (ViewState["_ProcessLightboxMsg"] == null)
            {
                ViewState["_ProcessLightboxMsg"] = RS.Resources.Msg_ExportDataPreparing;
            }
            ucLightBox.ucLightBoxMsg = ViewState["_ProcessLightboxMsg"].ToString();
            return ViewState["_ProcessLightboxMsg"].ToString();
        }
        set
        {
            ViewState["_ProcessLightboxMsg"] = value;
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
            if (ViewState["_ProcessBreakEnabled"] == null)
            {
                ViewState["_ProcessBreakEnabled"] = false;
            }
            return (bool)ViewState["_ProcessBreakEnabled"];
        }
        set
        {
            ViewState["_ProcessBreakEnabled"] = value;
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