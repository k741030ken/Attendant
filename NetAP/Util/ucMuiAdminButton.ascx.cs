using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
/// <summary>
/// [多語系維護按鈕] 控制項
/// </summary>
public partial class Util_ucMuiAdminButton : BaseUserControl
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
                ViewState["_BtnCaption"] = RS.Resources.MuiAdmin_btnLaunch; ;
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
    /// 資料庫名稱
    /// </summary>
    public string ucDBName
    {
        get
        {
            if (ViewState["_DBName"] == null)
            {
                ViewState["_DBName"] = "";
            }
            return (string)(ViewState["_DBName"]);
        }
        set
        {
            ViewState["_DBName"] = value;
        }
    }

    /// <summary>
    /// 資料表名稱
    /// </summary>
    public string ucTableName
    {
        get
        {
            if (ViewState["_TableName"] == null)
            {
                ViewState["_TableName"] = "";
            }
            return (string)(ViewState["_TableName"]);
        }
        set
        {
            ViewState["_TableName"] = value;
        }
    }

    /// <summary>
    /// 鍵值欄位清單(用逗號分隔)
    /// </summary>
    public string[] ucPKFieldList
    {
        get
        {
            if (ViewState["_PKFieldList"] == null)
            {
                ViewState["_PKFieldList"] = "".Split(',');
            }
            return (string[])(ViewState["_PKFieldList"]);
        }
        set
        {
            ViewState["_PKFieldList"] = value;
        }
    }

    /// <summary>
    /// 鍵值內容清單(用逗號分隔)
    /// </summary>
    public string[] ucPKValueList
    {
        get
        {
            if (ViewState["_PKValueList"] == null)
            {
                ViewState["_PKValueList"] = "".Split(',');
            }
            return (string[])(ViewState["_PKValueList"]);
        }
        set
        {
            ViewState["_PKValueList"] = value;
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

        string strURL = string.Format("{0}?DBName={1}&TableName={2}&PKFieldList={3}&PKValueList={4}",Util._MuiAdminUrl, ucDBName, ucTableName, Util.getStringJoin(ucPKFieldList), Util.getStringJoin(ucPKValueList));
        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucFrameURL = strURL;
        ucModalPopup1.ucPopupWidth = 650;
        ucModalPopup1.ucPopupHeight = 350;
        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;
        ucModalPopup1.Show();
    }
}