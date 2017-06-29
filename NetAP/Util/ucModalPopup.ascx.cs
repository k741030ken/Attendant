using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [彈出式視窗] 控制項
/// <para>可自訂視窗大小，內建三顆按鈕[Close/Complete/Cancel]並提供相應事件</para>
/// <para>視窗內可視需求嵌入 Content / Frame / Panel　三擇一</para>
/// </summary>
public partial class Util_ucModalPopup : BaseUserControl
{
    string _HtmlContent = "";

    /// <summary>
    /// 自訂ContextData，方便資料傳遞
    /// </summary>
    public string ucContextData
    {
        get
        {
            if (PageViewState["_ContextData"] == null)
            {
                PageViewState["_ContextData"] = "";
            }
            return (string)(PageViewState["_ContextData"]);
        }
        set
        {
            PageViewState["_ContextData"] = value;
        }
    }

    /// <summary>
    /// 是否觸動 Validation(預設 false)
    /// </summary>
    public bool ucCausesValidation
    {
        get
        {
            if (PageViewState["_CausesValidation"] == null)
            {
                PageViewState["_CausesValidation"] = false;
                btnClose.CausesValidation = false;
                btnComplete.CausesValidation = false;
                btnCancel.CausesValidation = false;
            }
            return (bool)(PageViewState["_CausesValidation"]);
        }
        set
        {
            PageViewState["_CausesValidation"] = value;
            btnClose.CausesValidation = value;
            btnComplete.CausesValidation = value;
            btnCancel.CausesValidation = value;
        }
    }

    /// <summary>
    /// 是否使用 Close 按鈕
    /// </summary>
    public bool ucBtnCloselEnabled { get { return btnClose.Visible; } set { btnClose.Visible = value; } }

    /// <summary>
    /// 是否使用 Complete 按鈕
    /// </summary>
    public bool ucBtnCompleteEnabled { get { return btnComplete.Visible; } set { btnComplete.Visible = value; } }

    /// <summary>
    /// 是否使用 Cancel 按鈕
    /// </summary>
    public bool ucBtnCancelEnabled { get { return btnCancel.Visible; } set { btnCancel.Visible = value; } }

    /// <summary>
    /// Complete 按鈕抬頭
    /// </summary>
    public string ucBtnCompleteHeader
    {
        get
        {
            if (PageViewState["_BtnCompleteHeader"] == null)
            {
                PageViewState["_BtnCompleteHeader"] = RS.Resources.ModalPopup_btnComplete;
            }
            return (string)(PageViewState["_BtnCompleteHeader"]);
        }
        set
        {
            PageViewState["_BtnCompleteHeader"] = value;
        }
    }

    /// <summary>
    /// Cancel 按鈕抬頭
    /// </summary>
    public string ucBtnCancelHeader
    {
        get
        {
            if (PageViewState["_BtnCancelHeader"] == null)
            {
                PageViewState["_BtnCancelHeader"] = RS.Resources.ModalPopup_btnCancel;
            }
            return (string)(PageViewState["_BtnCancelHeader"]);
        }
        set
        {
            PageViewState["_BtnCancelHeader"] = value;
        }
    }

    /// <summary>
    /// Complete 按鈕寬度(預設 120)
    /// </summary>
    public int ucBtnCompleteWidth
    {
        get
        {
            if (PageViewState["_BtnCompleteWidth"] == null)
            {
                PageViewState["_BtnCompleteWidth"] = 120;
            }
            return (int)(PageViewState["_BtnCompleteWidth"]);
        }
        set
        {
            PageViewState["_BtnCompleteWidth"] = value;
        }
    }


    /// <summary>
    /// Cancel 按鈕寬度(預設 120)
    /// </summary>
    public int ucBtnCancelWidth
    {
        get
        {
            if (PageViewState["_BtnCancelWidth"] == null)
            {
                PageViewState["_BtnCancelWidth"] = 120;
            }
            return (int)(PageViewState["_BtnCancelWidth"]);
        }
        set
        {
            PageViewState["_BtnCancelWidth"] = value;
        }
    }

    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (PageViewState["_PopupHeader"] == null)
            {
                PageViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header;
            }
            return (string)(PageViewState["_PopupHeader"]);
        }
        set
        {
            PageViewState["_PopupHeader"] = value;
        }
    }

    /// <summary>
    /// 視窗寬度(預設 650)
    /// </summary>
    public int ucPopupWidth
    {
        get
        {
            if (PageViewState["_PopupWidth"] == null)
            {
                PageViewState["_PopupWidth"] = 650;
            }
            return (int)(PageViewState["_PopupWidth"]);
        }
        set
        {
            PageViewState["_PopupWidth"] = value;
        }
    }

    /// <summary>
    /// 視窗高度(預設 500)
    /// </summary>
    public int ucPopupHeight
    {
        get
        {
            if (PageViewState["_PopupHeight"] == null)
            {
                PageViewState["_PopupHeight"] = 500;
            }
            return (int)(PageViewState["_PopupHeight"]);
        }
        set
        {
            PageViewState["_PopupHeight"] = value;
        }
    }

    /// <summary>
    /// Frame 頁框網址
    /// </summary>
    public string ucFrameURL
    {
        get
        {
            if (PageViewState["_FrameURL"] == null)
            {
                PageViewState["_FrameURL"] = "";
            }
            return (string)(PageViewState["_FrameURL"]);
        }
        set
        {
            PageViewState["_FrameURL"] = value;
        }
    }

    /// <summary>
    /// 計時重新整理頁框內容(預設 300 毫秒)
    /// </summary>
    public int ucFrameRefreshTimeout
    {
        get
        {
            if (PageViewState["_FrameRefreshTimeout"] == null)
            {
                PageViewState["_FrameRefreshTimeout"] = 300;
            }
            return (int)(PageViewState["_FrameRefreshTimeout"]);
        }
        set
        {
            PageViewState["_FrameRefreshTimeout"] = value;
        }
    }

    /// <summary>
    /// 欲顯示的 Html 內容
    /// </summary>
    public string ucHtmlContent
    {
		//配合Fortify 2017.04.21
        get
        {
            return _HtmlContent;
        }
        set
        {
            _HtmlContent = value;
        }
    }

    /// <summary>
    /// 欲顯示的 Panel ID
    /// </summary>
    public string ucPanelID
    {
        get
        {
            if (PageViewState["_PanelID"] == null)
            {
                PageViewState["_PanelID"] = "";
            }
            return (string)(PageViewState["_PanelID"]);
        }
        set
        {
            PageViewState["_PanelID"] = value;
        }
    }

    /// <summary>
    /// 關閉[彈出式視窗]JS
    /// <para>**提供開發人員自行撰寫 Client 處理時引用**</para>
    /// </summary>
    public string ucHideClientJS
    {
        //2017.02.03
        get
        {
            return string.Format("document.getElementById('{0}').style.display='none';document.getElementById('{1}').style.display='none';", ModalPanel.ClientID, ModalPopupExtender1.ClientID + "_backgroundElement");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int IEVer = Util.getIEVersion();
        if (IEVer < 0 || IEVer > 8)
        {
            ModalPanel.Style["display"] = "none"; //此處不能用 Hide() 2016.03.31
        }

        if (!IsPostBack)
        {
            //嘗試從Client取目前視窗大小，並存到 ClientWidth / ClientHeight 隱藏物件傳回 Server
            string strJSFun = "function(){";
            strJSFun += " var oChk01 = document.getElementById('" + ClientWidth.ClientID + "');";
            strJSFun += " var oChk02 = document.getElementById('" + ClientHeight.ClientID + "');";
            strJSFun += " if (oChk01 != null && oChk02 != null) {";
            strJSFun += "    oChk01.value=Util_getWidth();oChk02.value=Util_getHeight();";
            strJSFun += " }}";
            string strJS = string.Format("setTimeout(\"{0}\",{1});", strJSFun, 300);

            Util.setJSContent(strJS, this.ClientID + "_getClientWinSize");
        }
    }


    /// <summary>
    /// [彈出式視窗] Close 事件定義
    /// </summary>
    public class btnCloseEventArgs : EventArgs
    {
        string _Header = string.Empty;
        /// <summary>
        /// Close 按鈕抬頭
        /// </summary>
        public string Header
        {
            set { _Header = value; }
            get { return _Header; }
        }
    }

    /// <summary>
    /// [彈出式視窗] Close 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void btnCloseClick(object sender, btnCloseEventArgs e);
    /// <summary>
    ///  [彈出式視窗] Close 事件發布
    /// </summary>
    public event btnCloseClick onClose;

    /// <summary>
    /// [彈出式視窗] Complete 事件定義
    /// </summary>
    public class btnCompleteEventArgs : EventArgs
    {
        string _Header = "";
        /// <summary>
        /// Complete 按鈕抬頭
        /// </summary>
        public string Header
        {
            set { _Header = value; }
            get { return _Header; }
        }
    }

    /// <summary>
    /// [彈出式視窗] Complete 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void btnCompleteClick(object sender, btnCompleteEventArgs e);
    /// <summary>
    /// [彈出式視窗] Complete 事件發布
    /// </summary>
    public event btnCompleteClick onComplete;

    /// <summary>
    /// [彈出式視窗] Cancel 事件定義
    /// </summary>
    public class btnCancelEventArgs : EventArgs
    {
        string _Header = "";
        /// <summary>
        /// Cancel 按鈕抬頭
        /// </summary>
        public string Header
        {
            set { _Header = value; }
            get { return _Header; }
        }
    }

    /// <summary>
    /// [彈出式視窗] Cancel 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void btnCancelClick(object sender, btnCancelEventArgs e);

    /// <summary>
    /// [彈出式視窗] Cancel 事件發布
    /// </summary>
    public event btnCancelClick onCancel;

    /// <summary>
    /// [彈出式視窗] 重置全部參數
    /// </summary>
    public void Reset()
    {
        //參數回覆初始值
        this.PageViewState.Clear();
        btnClose.Visible = true;
        btnComplete.Visible = false;
        btnCancel.Visible = false;
    }

    //事件處理
    /// <summary>
    /// [彈出式視窗] 顯示彈出視窗
    /// </summary>
    public void Show()
    {
        ModalPanel.Style["display"] = "";
        ModalFrame.Visible = false;
        ModalHtmlMsg.Visible = false;

        int intModalHeight = (btnComplete.Visible || btnCancel.Visible) ? (ucPopupHeight - 80) : (ucPopupHeight - 50);

        //fix iPad iframe Scrolling issue 2017.05.04
        ModalParent.Style["height"] = string.Format("{0}px", intModalHeight + 5 );  //預留5px Buffer 2017.05.09

        if (string.IsNullOrEmpty(ucFrameURL) && string.IsNullOrEmpty(ucHtmlContent) && string.IsNullOrEmpty(ucPanelID))
        {
            Util.MsgBox(string.Format(RS.Resources.ModalPopup_AttributeError, "[ucFrameURL] or [ucHtmlContent] or [ucPanelID]"));
        }
        else
        {
            //子網頁
            if (!string.IsNullOrEmpty(ucFrameURL))
            {
                //設定 ModalFrame 屬性 2017.02.07 嘗試改用 JS 進行設定，但IE9(含)以上仍會 Page_Load Twice，改回原樣
                ModalFrame.Attributes["src"] = ucFrameURL;
                ModalFrame.Attributes["width"] = (ucPopupWidth - 20).ToString();
                ModalFrame.Attributes["height"] = intModalHeight.ToString();
                ModalFrame.Visible = true;
            }

            //顯示Html內容
            if (!string.IsNullOrEmpty(ucHtmlContent))
            {
                ModalHtmlMsg.Text = ucHtmlContent;
                ModalHtmlMsg.Width = Unit.Pixel(ucPopupWidth - 20);
                ModalHtmlMsg.Height = Unit.Pixel(intModalHeight);
                ModalHtmlMsg.Visible = true;
            }

            //嵌入控制項
            if (!string.IsNullOrEmpty(ucPanelID))
            {
                Control oPanel = this.Parent.FindControl(ucPanelID); //Panel物件應與ModalPopup同一層
                if (oPanel != null)
                {
                    if (oPanel.Parent.ID != ModalControl.ID)
                    {
                        oPanel.Visible = true;
                        //從Client端進行處理
                        string strJS = "dom.Ready(function(){var oModalControl = document.getElementById('" + ModalControl.ClientID + "');var oCtl = document.getElementById('" + oPanel.ClientID + "');if (oModalControl != null && oCtl != null) oModalControl.insertBefore(oCtl, oModalControl.firstChild); else oCtl.style.display='none'; });";
                        Util.setJSContent(strJS, this.ClientID + "_InitModalControl");
                    }

                    ModalControl.Width = Unit.Pixel(ucPopupWidth - 20);
                    ModalControl.Height = Unit.Pixel(intModalHeight);
                    ModalControl.Visible = true;
                }
            }

            labPopupHeader.Text = HttpUtility.HtmlEncode(ucPopupHeader);

            ModalPanel.Width = ucPopupWidth;
            ModalPanel.Height = ucPopupHeight;

            btnComplete.Width = ucBtnCompleteWidth;
            btnComplete.Text = ucBtnCompleteHeader;

            btnCancel.Width = ucBtnCancelWidth;
            btnCancel.Text = ucBtnCancelHeader;

            //根據 Client 傳回的視窗大小計算彈出視窗左上角位置
            int intWidth = int.Parse(ClientWidth.Value);
            int intHeight = int.Parse(ClientHeight.Value);
            if (intWidth > 0 && intHeight > 0)
            {
                ModalPopupExtender1.X = Convert.ToInt32((intWidth - ucPopupWidth) / 2);
                ModalPopupExtender1.Y = Convert.ToInt32((intHeight - ucPopupWidth) / 2);
                ModalPopupExtender1.Show();
            }
            else
            {
                ModalPopupExtender1.X = -1;
                ModalPopupExtender1.Y = -1;
                ModalPopupExtender1.Show();
            }
        }

    }

    /// <summary>
    /// [彈出式視窗] 關閉彈出視窗
    /// </summary>
    public void Hide()
    {
        ModalPanel.Style["display"] = "none";
        ModalFrame.Visible = false;    //2017.02.07 加入
        ModalHtmlMsg.Visible = false;  //2017.02.07 加入

        ModalPopupExtender1.Hide();
    }

    /// <summary>
    /// 重新整理頁框內容
    /// </summary>
    public void RefreshFrame()
    {
        if (!string.IsNullOrEmpty(ModalFrame.Attributes["src"].ToString()))
        {
            string strJS = string.Format("setTimeout(\"window.document.getElementById('{0}').src = window.document.getElementById('{0}').src;\",{1});", ModalFrame.ClientID, ucFrameRefreshTimeout);
            Util.setJSContent(strJS, this.ClientID + "_RefreshFrame");
        }
    }

    /// <summary>
    /// 按下 Close 按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Hide();
        btnCloseEventArgs eArgs = new btnCloseEventArgs();
        eArgs.Header = "Close";
        if (onClose != null)
        {
            onClose(this, eArgs);
        }
    }

    /// <summary>
    /// 按下 Complete 按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnComplete_Click(object sender, EventArgs e)
    {
        this.Hide();
        btnCompleteEventArgs eArgs = new btnCompleteEventArgs();
        eArgs.Header = btnComplete.Text;
        if (onComplete != null)
        {
            onComplete(this, eArgs);
        }
    }

    /// <summary>
    /// 按下 Cancel 按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Hide();
        btnCancelEventArgs eArgs = new btnCancelEventArgs();
        eArgs.Header = btnCancel.Text;
        if (onCancel != null)
        {
            onCancel(this, eArgs);
        }
    }
}