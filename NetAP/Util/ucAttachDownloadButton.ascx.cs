﻿using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [附件下載按鈕] 控制項
/// </summary>
public partial class Util_ucAttachDownloadButton : BaseUserControl
{
    #region 按鈕相關屬性

    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (ViewState["_PopupHeader"] == null)
            {
                ViewState["_PopupHeader"] = RS.Resources.Attach_btnDownloadLaunch; ;
            }
            return (string)(ViewState["_PopupHeader"]);
        }
        set
        {
            ViewState["_PopupHeader"] = value;
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
                ViewState["_BtnCaption"] = RS.Resources.Attach_btnDownloadLaunch; ;
            }
            return (string)(ViewState["_BtnCaption"]);
        }
        set
        {
            ViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式(預設 Util_clsBtn)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (ViewState["_BtnStyle"] == null)
            {
                ViewState["_BtnStyle"] = "Util_clsBtn";
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
    /// 按鈕 ClientJS
    /// </summary>
    public string ucBtnClientJS
    {
        //2016.09.22 新增
        get
        {
            if (ViewState["_BtnClientJS"] == null)
            {
                ViewState["_BtnClientJS"] = "Util_IsChkDirty = false;"; 
            }
            return (string)(ViewState["_BtnClientJS"]);
        }
        set
        {
            ViewState["_BtnClientJS"] = "Util_IsChkDirty = false;" + value;
        }
    }
    #endregion

    #region 其他屬性
    /// <summary>
    /// 附檔資料庫
    /// </summary>
    public string ucAttachDB
    {
        get
        {
            if (ViewState["_AttachDB"] == null)
            {
                ViewState["_AttachDB"] = "";
            }
            return (string)(ViewState["_AttachDB"]);
        }
        set
        {
            ViewState["_AttachDB"] = value;
        }
    }

    /// <summary>
    /// 附檔鍵值
    /// </summary>
    public string ucAttachID
    {
        get
        {
            if (ViewState["_AttachID"] == null)
            {
                ViewState["_AttachID"] = "";
            }
            return (string)(ViewState["_AttachID"]);
        }
        set
        {
            ViewState["_AttachID"] = value;
        }
    }
    #endregion

    #region 自訂事件

    /// <summary>
    /// [附件下載按鈕] 控制項 Launch 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);

    /// <summary>
    /// [附件下載按鈕] 控制項 Launch 事件
    /// </summary>
    public event Launch onLaunch;

    /// <summary>
    /// [附件下載按鈕] 控制項 Close 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);

    /// <summary>
    /// [附件下載按鈕] 控制項 Close 事件
    /// </summary>
    public event Close onClose;
    #endregion

    /// <summary>
    /// 視窗寬度(預設 650)
    /// </summary>
    public int ucPopupWidth
    {
        get
        {
            if (ViewState["_PopupWidth"] == null)
            {
                ViewState["_PopupWidth"] = 650;
            }
            return (int)(ViewState["_PopupWidth"]);
        }
        set
        {
            ViewState["_PopupWidth"] = value;
        }
    }

    /// <summary>
    /// 視窗高度(預設 380)
    /// </summary>
    public int ucPopupHeight
    {
        get
        {
            if (ViewState["_PopupHeight"] == null)
            {
                ViewState["_PopupHeight"] = 380;
            }
            return (int)(ViewState["_PopupHeight"]);
        }
        set
        {
            ViewState["_PopupHeight"] = value;
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
            if (ViewState["_IsPopNewWindow"] == null)
            {
                ViewState["_IsPopNewWindow"] = false;
            }
            return (bool)(ViewState["_IsPopNewWindow"]);
        }
        set
        {
            ViewState["_IsPopNewWindow"] = value;
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
            if (ViewState["_IsPopNewWindowCloseEvent"] == null)
            {
                ViewState["_IsPopNewWindowCloseEvent"] = false;
            }
            return (bool)(ViewState["_IsPopNewWindowCloseEvent"]);
        }
        set
        {
            ViewState["_IsPopNewWindowCloseEvent"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
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
            string strJS = "javascript:AttachPopWin=window.open('" + getAttachDownloadURL() + "','Attach01'";
            strJS += ",'width=" + ucPopupWidth + ",height=" + (ucPopupHeight - 50) + ",top=' + Util_getPopTop(" + ucPopupHeight + ") + ',left=' + Util_getPopLeft(" + ucPopupWidth + ") + '";
            strJS += ",status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');";

            if (ucIsPopNewWindowCloseEvent)
            {
                //模擬 onClose 事件
                //模擬 onClose 事件
                strJS += "AttachTimer=setInterval(function(){if(AttachPopWin.closed){";
                strJS += "clearInterval(AttachTimer);";
                strJS += "oBtn = document.getElementById('" + ucModalPopup1.ClientID + "_btnClose');if (oBtn!=null){oBtn.click();}";
                strJS += "}},1000);";
            }

            strJS += "AttachPopWin.focus();return false;";
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
        ucModalPopup1.ucFrameURL = getAttachDownloadURL();
        ucModalPopup1.ucPopupWidth = ucPopupWidth;
        ucModalPopup1.ucPopupHeight = ucPopupHeight;
        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;
        ucModalPopup1.Show();
    }

    protected string getAttachDownloadURL()
    {
        string strURL = string.Format("{0}?AttachDB={1}&AttachID={2}", Util._AttachDownloadUrl, ucAttachDB, ucAttachID);
        return strURL;
    }
}