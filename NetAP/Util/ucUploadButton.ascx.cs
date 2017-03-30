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
            if (ViewState["_PopupHeader"] == null)
            {
                ViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header;
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
                ViewState["_BtnCaption"] = RS.Resources.Upload_btnLaunch;
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
    /// 按鈕高度(預設 0，即自動計算)
    /// </summary>
    public int ucBtnHeight
    {
        get
        {
            if (ViewState["_BtnHeight"] == null)
            {
                ViewState["_BtnHeight"] = 0;
            }
            return (int)(ViewState["_BtnHeight"]);
        }
        set
        {
            ViewState["_BtnHeight"] = value;
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
    private string _GUID
    {
        get
        {
            if (ViewState["_GUID"] == null)
            {
                ViewState["_GUID"] = Guid.NewGuid().ToString().Replace("-", "");
            }
            return (string)(ViewState["_GUID"]);
        }
    }

    /// <summary>
    /// 上傳檔案最大KB(預設 1024)
    /// </summary>
    public int ucUploadFileMaxKB
    {
        get
        {
            if (ViewState["_UploadFileMaxKB"] == null)
            {
                ViewState["_UploadFileMaxKB"] = 1024;
            }
            return (int)(ViewState["_UploadFileMaxKB"]);
        }
        set
        {
            ViewState["_UploadFileMaxKB"] = value;
        }
    }

    /// <summary>
    /// 上傳可使用的副檔名清單
    /// </summary>
    public string ucUploadFileExtList
    {
        get
        {
            if (ViewState["_UploadFileExtList"] == null)
            {
                ViewState["_UploadFileExtList"] = "";
            }
            return (string)(ViewState["_UploadFileExtList"]);  //ex: PDF,ZIP,DOC
        }
        set
        {
            ViewState["_UploadFileExtList"] = value;
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
            if (ViewState["_UploadedCustMsg"] == null)
            {
                ViewState["_UploadedCustMsg"] = ""; //不自訂，使用預設值
            }
            return (string)(ViewState["_UploadedCustMsg"]);
        }
        set
        {
            ViewState["_UploadedCustMsg"] = value;
        }
    }


    /// <summary>
    /// 已上傳的檔名
    /// </summary>
    public string ucUploadedFileName
    {
        get
        {
            if (ViewState["_UploadedFileName"] == null)
            {
                ViewState["_UploadedFileName"] = "";
            }

            return (string)(ViewState["_UploadedFileName"]);
        }
    }

    /// <summary>
    /// 已上傳的檔案內容
    /// </summary>
    public byte[] ucUploadedFileBody
    {
        get
        {
            if (ViewState["_UploadedFileBody"] == null)
            {
                ViewState["_UploadedFileBody"] = new byte[] { };
            }

            return (byte[])(ViewState["_UploadedFileBody"]);
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
    /// 視窗高度(預設 200)
    /// </summary>
    public int ucPopupHeight
    {
        get
        {
            if (ViewState["_PopupHeight"] == null)
            {
                ViewState["_PopupHeight"] = 200;
            }
            return (int)(ViewState["_PopupHeight"]);
        }
        set
        {
            ViewState["_PopupHeight"] = value;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["_UploadedFileName"] = null;
        ViewState["_UploadedFileBody"] = null;
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
            ViewState["_UploadedFileName"] = (string)Session["FileName_" + _GUID];
            ViewState["_UploadedFileBody"] = (byte[])Session["FileBody_" + _GUID];
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

        ViewState["_UploadedFileName"] = null;
        ViewState["_UploadedFileBody"] = null;

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