using System;
using System.Collections.Generic;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [常用多重項目選單按鈕] 控制項
/// </summary>
public partial class Util_ucCommMultiSelectButton : BaseUserControl
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
                PageViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header; ;
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
                PageViewState["_BtnCaption"] = RS.Resources.CommMultiSelect_btnLaunch; ;
            }
            return (string)(PageViewState["_BtnCaption"]);
        }
        set
        {
            PageViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式
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

    #region 其他屬性
    /// <summary>
    /// 指定候選資料來源
    /// </summary>
    public Dictionary<string, string> ucSourceDictionary { get; set; }

    /// <summary>
    /// 「全選」前的確認訊息
    /// </summary>
    public string ucSelectAllConfirmMsg { get; set; }

    /// <summary>
    /// 選擇結果ID清單輸出到網頁父階指定物件ID
    /// </summary>
    public string ucSelectedIDListToParentObjClientID
    {
        get
        {
            if (PageViewState["_SelectedIDListToParentObjClientID"] == null)
            {
                PageViewState["_SelectedIDListToParentObjClientID"] = "";
            }
            return (string)(PageViewState["_SelectedIDListToParentObjClientID"]);
        }
        set
        {
            PageViewState["_SelectedIDListToParentObjClientID"] = value;
        }
    }

    /// <summary>
    /// 選擇結果Info清單輸出到網頁父階指定物件ID
    /// </summary>
    public string ucSelectedInfoListToParentObjClientID
    {
        get
        {
            if (PageViewState["_SelectedInfoListToParentObjClientID"] == null)
            {
                PageViewState["_SelectedInfoListToParentObjClientID"] = "";
            }
            return (string)(PageViewState["_SelectedInfoListToParentObjClientID"]);
        }
        set
        {
            PageViewState["_SelectedInfoListToParentObjClientID"] = value;
        }
    }


    /// <summary>
    /// 選擇結果ID清單
    /// </summary>
    public string ucSelectedIDList
    {
        //2016.10.13 新增
        get { return ucCommMultiSelect1.ucSelectedIDList; }
        set { ucCommMultiSelect1.ucSelectedIDList = value; }
    }

    /// <summary>
    /// 選擇結果Info清單
    /// </summary>
    public string ucSelectedInfoList
    {
        //2016.10.13 新增
        get { return ucCommMultiSelect1.ucSelectedInfoList; }
        set { ucCommMultiSelect1.ucSelectedInfoList = value; }
    }

    /// <summary>
    /// 選擇結果
    /// </summary>
    public Dictionary<string, string> ucSelectedDictionary
    {
        //2016.10.13 新增
        get
        {
            return ucCommMultiSelect1.ucSelectedDictionary;
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
        ucModalPopup1.ucPopupHeight = 330;

        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;

        ucCommMultiSelect1.ucSourceDictionary = ucSourceDictionary;
        ucCommMultiSelect1.ucSelectedIDListToParentObjClientID = ucSelectedIDListToParentObjClientID;
        ucCommMultiSelect1.ucSelectedInfoListToParentObjClientID = ucSelectedInfoListToParentObjClientID;
        ucCommMultiSelect1.ucSelectAllConfirmMsg = ucSelectAllConfirmMsg;
        ucCommMultiSelect1.Refresh();
        ucModalPopup1.ucPanelID = ModalPanel.ID;

        ucModalPopup1.Show();
    }
}