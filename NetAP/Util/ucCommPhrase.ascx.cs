using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [常用片語]控制項
/// <para>** 資料來源為 CustProperety 資料表，故調整部份參數後，亦可適用其他類似應用 **</para>
/// </summary>
public partial class Util_ucCommPhrase : BaseUserControl
{
    /// <summary>
    /// 當沒資料時，控制項是否可見(預設 true)
    /// </summary>
    public bool ucIsVisibleWhenNoData
    {
        get
        {
            if (PageViewState["_IsVisibleWhenNoData"] == null)
            {
                PageViewState["_IsVisibleWhenNoData"] = true;
            }
            return (bool)(PageViewState["_IsVisibleWhenNoData"]);
        }
        set
        {
            PageViewState["_IsVisibleWhenNoData"] = value;
        }
    }


    /// <summary>
    /// 資料來源 DBName (預設 空白)
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
    /// 資料來源 PKKind (預設 空白)
    /// </summary>
    public string ucPKID
    {
        get
        {
            if (PageViewState["_PKID"] == null)
            {
                PageViewState["_PKID"] = "";
            }
            return (string)(PageViewState["_PKID"]);
        }
        set
        {
            PageViewState["_PKID"] = value;
        }
    }

    /// <summary>
    /// 資料來源 PKKind (預設 [User])
    /// </summary>
    public string ucPKKind
    {
        get
        {
            if (PageViewState["_PKKind"] == null)
            {
                PageViewState["_PKKind"] = "User";
            }
            return (string)(PageViewState["_PKKind"]);
        }
        set
        {
            PageViewState["_PKKind"] = value;
        }
    }

    /// <summary>
    /// 資料來源 PropID (預設 [CommPhrase])
    /// </summary>
    public string ucPropID
    {
        get
        {
            if (PageViewState["_PropID"] == null)
            {
                PageViewState["_PropID"] = "CommPhrase";
            }
            return (string)(PageViewState["_PropID"]);
        }
        set
        {
            PageViewState["_PropID"] = value;
        }
    }


    /// <summary>
    /// 要加工的目標是否為[父階]物件(預設 false)
    /// </summary>
    public bool ucIsParentTarget
    {
        get
        {
            if (PageViewState["_IsParentTarget"] == null)
            {
                PageViewState["_IsParentTarget"] = false;
            }
            return (bool)(PageViewState["_IsParentTarget"]);
        }
        set
        {
            PageViewState["_IsParentTarget"] = value;
        }
    }

    /// <summary>
    /// 要添加內容的目標物件 ClientID
    /// <para>**若為空白則無法挑選片語**</para>
    /// </summary>
    public string ucTargetClientID
    {
        get
        {
            if (PageViewState["_TargetClientID"] == null)
            {
                PageViewState["_TargetClientID"] = "";
            }
            return (string)(PageViewState["_TargetClientID"]);
        }
        set
        {
            PageViewState["_TargetClientID"] = value;
        }
    }

    /// <summary>
    /// 選單寬度(預設 180)
    /// </summary>
    public int ucDropDownSourceListWidth
    {
        get
        {
            if (PageViewState["_DropDownSourceListWidth"] == null)
            {
                PageViewState["_DropDownSourceListWidth"] = 180;
            }
            return (int)(PageViewState["_DropDownSourceListWidth"]);
        }
        set
        {
            PageViewState["_DropDownSourceListWidth"] = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭(空白時自動隱藏)
    /// </summary>
    public string ucCaption
    {
        get
        {
            if (PageViewState["_Caption"] == null)
            {
                PageViewState["_Caption"] = "";
            }
            return (string)(PageViewState["_Caption"]);
        }
        set
        {
            PageViewState["_Caption"] = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭寬度(預設 80)
    /// </summary>
    public int ucCaptionWidth
    {
        get
        {
            if (PageViewState["_CaptionWidth"] == null)
            {
                PageViewState["_CaptionWidth"] = 80;
            }
            return (int)(PageViewState["_CaptionWidth"]);
        }
        set
        {
            PageViewState["_CaptionWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 25)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (PageViewState["_BtnWidth"] == null)
            {
                PageViewState["_BtnWidth"] = 25;
            }
            return (int)(PageViewState["_BtnWidth"]);
        }
        set
        {
            PageViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕高度(預設 19)
    /// </summary>
    public int ucBtnHeight
    {
        get
        {
            if (PageViewState["_BtnHeight"] == null)
            {
                PageViewState["_BtnHeight"] = 19;
            }
            return (int)(PageViewState["_BtnHeight"]);
        }
        set
        {
            PageViewState["_BtnHeight"] = value;
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
                PageViewState["_BtnCaption"] = "▼";
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
        get
        {
            if (PageViewState["_BtnToolTip"] == null)
            {
                PageViewState["_BtnToolTip"] = RS.Resources.CommPhrase_btnAppend; //添加片語
            }
            return (string)(PageViewState["_BtnToolTip"]);
        }
        set
        {
            PageViewState["_BtnToolTip"] = value;
        }
    }

    /// <summary>
    /// 按鈕 CssClass (預設 Util_clsBtnGray + Util_Pointer)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (PageViewState["_BtnCssClass"] == null)
            {
                PageViewState["_BtnCssClass"] = "Util_clsBtnGray Util_Pointer";
            }
            return (string)(PageViewState["_BtnCssClass"]);
        }
        set
        {
            PageViewState["_BtnCssClass"] = value;
        }
    }

    /// <summary>
    /// 是否提供搜尋功能(預設true)
    /// <para>若提供搜尋，則會強制產生一個空白項目</para>
    /// </summary>
    public bool ucIsSearchEnabled
    {
        get
        {
            if (PageViewState["_IsSearchEnabled"] == null)
            {
                PageViewState["_IsSearchEnabled"] = true;
            }
            return (bool)(PageViewState["_IsSearchEnabled"]);
        }
        set
        {
            PageViewState["_IsSearchEnabled"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框寬度(預設 100)
    /// </summary>
    public int ucSearchBoxWidth
    {
        get
        {
            if (PageViewState["_SearchBoxWidth"] == null)
            {
                PageViewState["_SearchBoxWidth"] = 100;
            }
            return (int)(PageViewState["_SearchBoxWidth"]);
        }
        set
        {
            PageViewState["_SearchBoxWidth"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框浮水印內容
    /// </summary>
    public string ucSearchBoxWaterMarkText
    {
        get
        {
            if (PageViewState["_SearchBoxWaterMarkText"] == null)
            {
                PageViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommPhrase_WaterMarkText; //搜尋片語
            }
            return (string)(PageViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            PageViewState["_SearchBoxWaterMarkText"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh();
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        if (Session["UserID"] == null)
        {
            divPick.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, RS.Resources.Msg_SessionNotFound);
            return;
        }

        if (string.IsNullOrEmpty(ucTargetClientID))
        {
            divPick.Visible = false;
            return;
        }

        //btnAppend
        btnAppend.CssClass = ucBtnCssClass;
        btnAppend.Text = ucBtnCaption;
        btnAppend.Width = ucBtnWidth;
        btnAppend.Height = ucBtnHeight;
        btnAppend.ToolTip = ucBtnToolTip;
        btnAppend.OnClientClick = string.Format("Util_DropDownListItemToTextBox('{0}','{1}','{2}','N');return false;", ddlPhrase.ClientID + "_ddlSourceList", ucTargetClientID, (ucIsParentTarget) ? "Y" : "N");

        //ddlPhrase
        ddlPhrase.ucCaption = ucCaption;
        ddlPhrase.ucCaptionWidth = ucCaptionWidth;
        ddlPhrase.ucDropDownSourceListWidth = ucDropDownSourceListWidth;
        ddlPhrase.ucIsSearchEnabled = ucIsSearchEnabled;
        ddlPhrase.ucSearchBoxWidth = ucSearchBoxWidth;
        ddlPhrase.ucSearchBoxWaterMarkText = ucSearchBoxWaterMarkText;

        Dictionary<string, string> dicPhrase = new Dictionary<string, string>();
        if (ucPKKind == "User" && ucPropID == "CommPhrase")
            dicPhrase = Util.getDictionary(UserInfo.getUserProperty(UserInfo.getUserInfo().UserID, ucPKKind, ucPropID)["PropJSON"]);
        else
            dicPhrase = Util.getDictionary(Util.getCustProperty(ucDBName, ucPKID, ucPKKind, ucPropID)["PropJSON"]);

        ddlPhrase.ucSourceDictionary = dicPhrase;
        ddlPhrase.ucSelectedID = "";
        ddlPhrase.Refresh();

        //判斷顯示與否
        if (!ucIsVisibleWhenNoData && dicPhrase.IsNullOrEmpty())
            this.Visible = false;
        else
            this.Visible = true;
    }
}