using System;
using System.Web;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [檔案上傳按鈕] 控制項
/// </summary>
public partial class Util_ucUploadButton : BaseUserControl
{
    //private string _defBtnClass = "Util_clsBtn";
    //private string _defBtnClientJS = "Util_IsChkDirty = false;";
    //private int _defBtnWidth = 80;

    //protected string _ParentObjID = "";   //預設要傳回 ID 的網頁父階物件
    //protected string _ParentObjInfo = ""; //預設要傳回 Info 的網頁父階物件

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
                PageViewState["_BtnCaption"] = RS.Resources.Upload_btnLaunch;
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
            if (PageViewState["_BtnStyle"] == null)
            {
                PageViewState["_BtnStyle"] = "Util_clsBtn";
            }
            return (string)(PageViewState["_BtnStyle"]);
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
    /// 按鈕高度(預設 0，即自動計算)
    /// </summary>
    public int ucBtnHeight
    {
        get
        {
            if (PageViewState["_BtnHeight"] == null)
            {
                PageViewState["_BtnHeight"] = 0;
            }
            return (int)(PageViewState["_BtnHeight"]);
        }
        set
        {
            PageViewState["_BtnHeight"] = value;
        }
    }

    /// <summary>
    /// 按鈕 Client 端 JS
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

    #region 其他屬性
    private string _GUID
    {
        get
        {
            if (PageViewState["_GUID"] == null)
            {
                PageViewState["_GUID"] = Guid.NewGuid().ToString().Replace("-", "");
            }
            return (string)(PageViewState["_GUID"]);
        }
    }

    /// <summary>
    /// 上傳檔案最大KB(預設 1024)
    /// </summary>
    public int ucUploadFileMaxKB
    {
        get
        {
            if (PageViewState["_UploadFileMaxKB"] == null)
            {
                PageViewState["_UploadFileMaxKB"] = 1024;
            }
            return (int)(PageViewState["_UploadFileMaxKB"]);
        }
        set
        {
            PageViewState["_UploadFileMaxKB"] = value;
        }
    }

    /// <summary>
    /// 上傳可使用的副檔名清單
    /// </summary>
    public string ucUploadFileExtList
    {
        get
        {
            if (PageViewState["_UploadFileExtList"] == null)
            {
                PageViewState["_UploadFileExtList"] = "";
            }
            return (string)(PageViewState["_UploadFileExtList"]);  //ex: PDF,ZIP,DOC
        }
        set
        {
            PageViewState["_UploadFileExtList"] = value;
        }
    }

    /// <summary>
    /// 上傳完畢時的自訂訊息
    /// </summary>
    public string ucUploadedCustMsg
    {
        //2017.01.19 新增
        get
        {
            if (PageViewState["_UploadedCustMsg"] == null)
            {
                PageViewState["_UploadedCustMsg"] = ""; //不自訂，使用預設值
            }
            return (string)(PageViewState["_UploadedCustMsg"]);
        }
        set
        {
            PageViewState["_UploadedCustMsg"] = value;
        }
    }


    /// <summary>
    /// 已上傳的檔名
    /// </summary>
    public string ucUploadedFileName
    {
        get
        {
            if (PageViewState["_UploadedFileName"] == null)
            {
                PageViewState["_UploadedFileName"] = "";
            }

            return (string)(PageViewState["_UploadedFileName"]);
        }
    }

    /// <summary>
    /// 已上傳的檔案內容
    /// </summary>
    public byte[] ucUploadedFileBody
    {
        get
        {
            if (PageViewState["_UploadedFileBody"] == null)
            {
                PageViewState["_UploadedFileBody"] = new byte[] { };
            }

            return (byte[])(PageViewState["_UploadedFileBody"]);
        }
    }
    #endregion

    #region 自訂事件
    /// <summary>
    /// [檔案上傳按鈕] 控制項 Launch 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);
    /// <summary>
    /// [檔案上傳按鈕] 控制項 Launch 事件
    /// </summary>
    public event Launch onLaunch;
    /// <summary>
    /// [檔案上傳按鈕] 控制項 Close 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);
    /// <summary>
    /// [檔案上傳按鈕] 控制項 Close 事件
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
    /// 視窗高度(預設 200)
    /// </summary>
    public int ucPopupHeight
    {
        get
        {
            if (PageViewState["_PopupHeight"] == null)
            {
                PageViewState["_PopupHeight"] = 200;
            }
            return (int)(PageViewState["_PopupHeight"]);
        }
        set
        {
            PageViewState["_PopupHeight"] = value;
        }
    }

    /// <summary>
    /// 是否開新視窗模式(預設 false)
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

    protected void Page_Load(object sender, EventArgs e)
    {
        PageViewState["_UploadedFileName"] = null;
        PageViewState["_UploadedFileBody"] = null;
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        Refresh();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        ucModalPopup1.Hide();
        btnLaunch.OnClientClick = ucBtnClientJS; //2016.09.22
        btnLaunch.Text = ucBtnCaption;
        btnLaunch.CssClass = ucBtnCssClass;
        if (ucBtnWidth > 0)
            btnLaunch.Width = ucBtnWidth;
        else
            btnLaunch.Style.Add("width", "auto");

        if (ucBtnHeight > 0)
        {
            btnLaunch.Height = ucBtnHeight;
        }

        if (ucIsPopNewWindow)
        {
            //開新視窗模式
            string strJS = "javascript:UploadPopWin=window.open('" + getUploadURL() + "','Attach01'";
            strJS += ",'width=" + ucPopupWidth + ",height=" + (ucPopupHeight - 50) + ",top=' + Util_getPopTop(" + ucPopupHeight + ") + ',left=' + Util_getPopLeft(" + ucPopupWidth + ") + '";
            strJS += ",status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');";
            strJS += "UploadTimer=setInterval(function(){if(UploadPopWin.closed){";
            strJS += "clearInterval(UploadTimer);";
            strJS += "oBtn = document.getElementById('" + ucModalPopup1.ClientID + "_btnClose');if (oBtn!=null){oBtn.click();}";
            strJS += "}},1000);UploadPopWin.focus();return false;";
            btnLaunch.OnClientClick = strJS;
        }
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        //處理上傳結果
        if (Session["FileName_" + _GUID] != null)
        {
            //更新按鈕屬性，並清除相關Session
            PageViewState["_UploadedFileName"] = (string)Session["FileName_" + _GUID];
            PageViewState["_UploadedFileBody"] = (byte[])Session["FileBody_" + _GUID];
            Session["FileName_" + _GUID] = null;
            Session["FileBody_" + _GUID] = null;
        }

        //事件訂閱
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

        PageViewState["_UploadedFileName"] = null;
        PageViewState["_UploadedFileBody"] = null;

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucPopupWidth = ucPopupWidth;
        ucModalPopup1.ucPopupHeight = ucPopupHeight;

        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;

        ucModalPopup1.ucFrameURL = getUploadURL();
        ucModalPopup1.Show();
    }

    protected string getUploadURL()
    {
        string strURL = string.Format("{0}?", Util.getFixURL("~/Util/Upload.aspx"));
        strURL += string.Format("GUID={0}&UploadFileMaxKB={1}&UploadFileExtList={2}&Rnd={3}", _GUID, ucUploadFileMaxKB, ucUploadFileExtList, new Random().Next(10000, 99999));
        //2017.01.19 新增
        if (!string.IsNullOrEmpty(ucUploadedCustMsg))
        {
            strURL += string.Format("&IsDoneMsg={0}", HttpUtility.UrlEncode(ucUploadedCustMsg));
        }
        return strURL;
    }
}