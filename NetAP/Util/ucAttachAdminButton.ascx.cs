using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
/// <summary>
/// [附件管理按鈕] 控制項
/// </summary>
public partial class Util_ucAttachAdminButton : BaseUserControl
{
    #region 按鈕相關屬性

    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (PageViewState["_PopupHeader"] == null)
            {
                PageViewState["_PopupHeader"] = RS.Resources.Attach_btnAdminLaunch; ;
            }
            return (string)(PageViewState["_PopupHeader"]);
        }
        set
        {
            PageViewState["_PopupHeader"] = value;
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
            return btnLaunch.Enabled;
        }
        set
        {
            btnLaunch.Enabled = value;
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
                PageViewState["_BtnCaption"] = RS.Resources.Attach_btnAdminLaunch;
            }
            return (string)(PageViewState["_BtnCaption"]);
        }
        set
        {
            PageViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式(預設 Util_clsBtn)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (PageViewState["_BtnCssClass"] == null)
            {
                PageViewState["_BtnCssClass"] = "Util_clsBtn";
            }
            return (string)(PageViewState["_BtnCssClass"]);
        }
        set
        {
            PageViewState["_BtnCssClass"] = value;
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
    /// 按鈕 ClientJS
    /// </summary>
    public string ucBtnClientJS
    {
        //2016.09.22 新增
        get
        {
            if (PageViewState["_BtnClientJS"] == null)
            {
                PageViewState["_BtnClientJS"] = "Util_IsChkDirty = false;";
            }
            return (string)(PageViewState["_BtnClientJS"]);
        }
        set
        {
            PageViewState["_BtnClientJS"] = "Util_IsChkDirty = false;" + value;
        }
    }
    #endregion

    #region 自訂事件

    /// <summary>
    /// Launch 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);

    /// <summary>
    /// Launch 事件
    /// </summary>
    public event Launch onLaunch;

    /// <summary>
    /// Close 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);

    /// <summary>
    /// Close 事件
    /// </summary>
    public event Close onClose;
    #endregion

    /// <summary>
    /// 附檔資料庫
    /// </summary>
    public string ucAttachDB
    {
        get
        {
            if (PageViewState["_AttachDB"] == null)
            {
                PageViewState["_AttachDB"] = "";
            }
            return (string)(PageViewState["_AttachDB"]);
        }
        set
        {
            PageViewState["_AttachDB"] = value;
        }
    }

    /// <summary>
    /// 附檔鍵值
    /// </summary>
    public string ucAttachID
    {
        get
        {
            if (PageViewState["_AttachID"] == null)
            {
                PageViewState["_AttachID"] = "";
            }
            return (string)(PageViewState["_AttachID"]);
        }
        set
        {
            PageViewState["_AttachID"] = value;
        }
    }

    /// <summary>
    /// 可使用的副檔名清單
    /// </summary>
    public string ucAttachFileExtList
    {
        get
        {
            if (PageViewState["_AttachFileExtList"] == null)
            {
                PageViewState["_AttachFileExtList"] = "";
            }
            return (string)(PageViewState["_AttachFileExtList"]);
        }
        set
        {
            PageViewState["_AttachFileExtList"] = value;
        }
    }

    /// <summary>
    /// 可否匿名存取(預設 N)
    /// </summary>
    public string ucAnonymousYN
    {
        get
        {
            if (PageViewState["_AnonymousYN"] == null)
            {
                PageViewState["AnonymousYN"] = "N";
            }
            return (string)(PageViewState["AnonymousYN"]);
        }
        set
        {
            PageViewState["AnonymousYN"] = value.ToUpper();
        }
    }

    /// <summary>
    /// 最大上傳檔案數量(預設 1)
    /// </summary>
    public int ucAttachFileMaxQty
    {
        get
        {
            if (PageViewState["_AttachFileMaxQty"] == null)
            {
                PageViewState["_AttachFileMaxQty"] = 1;
            }
            return (int)(PageViewState["_AttachFileMaxQty"]);
        }
        set
        {
            PageViewState["_AttachFileMaxQty"] = value;
        }
    }

    /// <summary>
    /// 最大單檔大小限制KB(預設 1024)
    /// </summary>
    public int ucAttachFileMaxKB
    {
        get
        {
            if (PageViewState["_AttachFileMaxKB"] == null)
            {
                PageViewState["_AttachFileMaxKB"] = 1024;
            }
            return (int)(PageViewState["_AttachFileMaxKB"]);
        }
        set
        {
            PageViewState["_AttachFileMaxKB"] = value;
        }
    }

    /// <summary>
    /// 檔案總體配額限制KB(預設 0，即自動計算)
    /// </summary>
    public int ucAttachFileTotKB
    {
        get
        {
            if (PageViewState["_AttachFileTotKB"] == null)
            {
                PageViewState["_AttachFileTotKB"] = 0;
            }
            return (int)(PageViewState["_AttachFileTotKB"]);
        }
        set
        {
            PageViewState["_AttachFileTotKB"] = value;
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
    /// 視窗高度(預設 480)
    /// </summary>
    public int ucPopupHeight
    {
        get
        {
            if (PageViewState["_PopupHeight"] == null)
            {
                PageViewState["_PopupHeight"] = 480;
            }
            return (int)(PageViewState["_PopupHeight"]);
        }
        set
        {
            PageViewState["_PopupHeight"] = value;
        }
    }

    /// <summary>
    /// 是否為彈出新視窗模式(預設 false)
    /// </summary>
    public bool ucIsPopNewWindow
    {
        //2015.06.24 新增
        get
        {
            if (PageViewState["_IsPopNewWindow"] == null)
            {
                PageViewState["_IsPopNewWindow"] = false;
            }
            return (bool)(PageViewState["_IsPopNewWindow"]);
        }
        set
        {
            PageViewState["_IsPopNewWindow"] = value;
        }
    }

    /// <summary>
    /// 關閉新視窗時，是否引發 Close 事件(預設 false)
    /// </summary>
    public bool ucIsPopNewWindowCloseEvent
    {
        //2015.06.24 新增
        get
        {
            if (PageViewState["_IsPopNewWindowCloseEvent"] == null)
            {
                PageViewState["_IsPopNewWindowCloseEvent"] = false;
            }
            return (bool)(PageViewState["_IsPopNewWindowCloseEvent"]);
        }
        set
        {
            PageViewState["_IsPopNewWindowCloseEvent"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucModalPopup1.onClose += ucModalPopup1_onClose;
        Refresh();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        btnLaunch.OnClientClick = ucBtnClientJS; //2016.09.22
        btnLaunch.Text = ucBtnCaption;
        btnLaunch.CssClass = ucBtnCssClass;
        if (ucBtnWidth > 0)
            btnLaunch.Width = ucBtnWidth;
        else
            btnLaunch.Style.Add("width", "auto");

        if (ucIsPopNewWindow)
        {
            //開新視窗模式
            string strJS = "javascript:PopWin=window.open('" + getAttachAdminURL() + "',''";
            strJS += ",'width=" + ucPopupWidth + ",height=" + (ucPopupHeight - 50) + ",top=' + Util_getPopTop(" + ucPopupHeight + ") + ',left=' + Util_getPopLeft(" + ucPopupWidth + ") + '";
            strJS += ",status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');";

            if (ucIsPopNewWindowCloseEvent)
            {
                //模擬 onClose 事件
                strJS += "PopWinTimer=setInterval(function(){if(PopWin.closed){";
                strJS += "clearInterval(PopWinTimer);";
                strJS += "oBtn = document.getElementById('" + ucModalPopup1.ClientID + "_btnClose');if (oBtn!=null){oBtn.click();}";
                strJS += "}},1000);";
            }

            strJS += "PopWin.focus();return false;";
            btnLaunch.OnClientClick = strJS;
        }
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        if (onClose != null)
        {
            onClose(this, e);
        }
    }

    protected void btnLaunch_Click(object sender, EventArgs e)
    {
        if (onLaunch != null)
        {
            onLaunch(this, e);
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucFrameURL = getAttachAdminURL();
        ucModalPopup1.ucPopupWidth = ucPopupWidth;
        ucModalPopup1.ucPopupHeight = ucPopupHeight;
        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;
        ucModalPopup1.Show();
    }

    protected string getAttachAdminURL()
    {
        return string.Format("{0}?AttachDB={1}&AttachID={2}&AttachFileMaxQty={3}&AttachFileMaxKB={4}&AttachFileTotKB={5}&AnonymousYN={6}&AttachFileExtList={7}", Util._AttachAdminUrl, ucAttachDB, ucAttachID, ucAttachFileMaxQty, ucAttachFileMaxKB, ucAttachFileTotKB, ucAnonymousYN, ucAttachFileExtList);
    }
}