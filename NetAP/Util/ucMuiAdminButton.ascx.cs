using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
/// <summary>
/// [多語系維護按鈕] 控制項
/// </summary>
public partial class Util_ucMuiAdminButton : BaseUserControl
{
    string _BtnClientJS = "Util_IsChkDirty = false;";

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
                PageViewState["_BtnCaption"] = RS.Resources.MuiAdmin_btnLaunch; ;
            }
            return (string)(PageViewState["_BtnCaption"]);
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
    /// 按鈕 Client 端 JS
    /// </summary>
    public string ucBtnClientJS
    {
        //配合Fortify 2017.04.21
        get
        {
            return _BtnClientJS;
        }
        set
        {
           _BtnClientJS += value;
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
            if (PageViewState["_DBName"] == null)
            {
                PageViewState["_DBName"] = "";
            }
            return (string)(PageViewState["_DBName"]);
        }
        set
        {
            PageViewState["_DBName"] = value;
        }
    }

    /// <summary>
    /// 資料表名稱
    /// </summary>
    public string ucTableName
    {
        get
        {
            if (PageViewState["_TableName"] == null)
            {
                PageViewState["_TableName"] = "";
            }
            return (string)(PageViewState["_TableName"]);
        }
        set
        {
            PageViewState["_TableName"] = value;
        }
    }

    /// <summary>
    /// 鍵值欄位清單(用逗號分隔)
    /// </summary>
    public string[] ucPKFieldList
    {
        get
        {
            if (PageViewState["_PKFieldList"] == null)
            {
                PageViewState["_PKFieldList"] = "".Split(',');
            }
            return (string[])(PageViewState["_PKFieldList"]);
        }
        set
        {
            PageViewState["_PKFieldList"] = value;
        }
    }

    /// <summary>
    /// 鍵值內容清單(用逗號分隔)
    /// </summary>
    public string[] ucPKValueList
    {
        get
        {
            if (PageViewState["_PKValueList"] == null)
            {
                PageViewState["_PKValueList"] = "".Split(',');
            }
            return (string[])(PageViewState["_PKValueList"]);
        }
        set
        {
            PageViewState["_PKValueList"] = value;
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

        if (!string.IsNullOrEmpty(ucBtnToolTip)) //2017.05.19
            btnLaunch.ToolTip = ucBtnToolTip;

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