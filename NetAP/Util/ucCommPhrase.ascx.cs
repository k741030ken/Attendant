using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_TargetClientID"]));
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
    /// 控制項顯示抬頭水平對齊方式(預設 Right)
    /// </summary>
    public HorizontalAlign ucCaptionHorizontalAlign
    {
        //2017.06.03 新增
        get
        {
            if (PageViewState["_CaptionHorizontalAlign"] == null)
            {
                PageViewState["_CaptionHorizontalAlign"] = HorizontalAlign.Right;
            }
            return (HorizontalAlign)(PageViewState["_CaptionHorizontalAlign"]);
        }
        set
        {
            PageViewState["_CaptionHorizontalAlign"] = value;
            if (value == HorizontalAlign.NotSet)
            {
                PageViewState["_CaptionHorizontalAlign"] = HorizontalAlign.Right;
            }
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
    /// 搜尋文字框寬度(預設 80)
    /// </summary>
    public int ucSearchBoxWidth
    {
        get
        {
            if (PageViewState["_SearchBoxWidth"] == null)
            {
                PageViewState["_SearchBoxWidth"] = 80;
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
        btnAppend.Text = RS.Resources.CommPhrase_btnAppend;
        btnAppend.OnClientClick = string.Format("Util_DropDownListItemToTextBox('{0}','{1}','{2}','N');return false;", ddlPhrase.ClientID + "_ddlSourceList", ucTargetClientID, (ucIsParentTarget) ? "Y" : "N");

        //btnReplace 2017.06.09
        btnReplace.CssClass = ucBtnCssClass;
        btnReplace.Text = RS.Resources.CommPhrase_btnReplace;
        btnReplace.OnClientClick = string.Format("Util_DropDownListItemToTextBox('{0}','{1}','{2}','Y');return false;", ddlPhrase.ClientID + "_ddlSourceList", ucTargetClientID, (ucIsParentTarget) ? "Y" : "N");

        //ddlPhrase
        ddlPhrase.ucCaption = ucCaption;
        ddlPhrase.ucCaptionWidth = ucCaptionWidth;
        ddlPhrase.ucCaptionHorizontalAlign = ucCaptionHorizontalAlign;
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