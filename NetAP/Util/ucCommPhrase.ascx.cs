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
            if (ViewState["_IsVisibleWhenNoData"] == null)
            {
                ViewState["_IsVisibleWhenNoData"] = true;
            }
            return (bool)(ViewState["_IsVisibleWhenNoData"]);
        }
        set
        {
            ViewState["_IsVisibleWhenNoData"] = value;
        }
    }


    /// <summary>
    /// 資料來源 DBName (預設 空白)
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
    /// 資料來源 PKKind (預設 空白)
    /// </summary>
    public string ucPKID
    {
        get
        {
            if (ViewState["_PKID"] == null)
            {
                ViewState["_PKID"] = "";
            }
            return (string)(ViewState["_PKID"]);
        }
        set
        {
            ViewState["_PKID"] = value;
        }
    }

    /// <summary>
    /// 資料來源 PKKind (預設 [User])
    /// </summary>
    public string ucPKKind
    {
        get
        {
            if (ViewState["_PKKind"] == null)
            {
                ViewState["_PKKind"] = "User";
            }
            return (string)(ViewState["_PKKind"]);
        }
        set
        {
            ViewState["_PKKind"] = value;
        }
    }

    /// <summary>
    /// 資料來源 PropID (預設 [CommPhrase])
    /// </summary>
    public string ucPropID
    {
        get
        {
            if (ViewState["_PropID"] == null)
            {
                ViewState["_PropID"] = "CommPhrase";
            }
            return (string)(ViewState["_PropID"]);
        }
        set
        {
            ViewState["_PropID"] = value;
        }
    }


    /// <summary>
    /// 要加工的目標是否為[父階]物件(預設 false)
    /// </summary>
    public bool ucIsParentTarget
    {
        get
        {
            if (ViewState["_IsParentTarget"] == null)
            {
                ViewState["_IsParentTarget"] = false;
            }
            return (bool)(ViewState["_IsParentTarget"]);
        }
        set
        {
            ViewState["_IsParentTarget"] = value;
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
            if (ViewState["_TargetClientID"] == null)
            {
                ViewState["_TargetClientID"] = "";
            }
            return (string)(ViewState["_TargetClientID"]);
        }
        set
        {
            ViewState["_TargetClientID"] = value;
        }
    }

    /// <summary>
    /// 選單寬度(預設 180)
    /// </summary>
    public int ucDropDownSourceListWidth
    {
        get
        {
            if (ViewState["_DropDownSourceListWidth"] == null)
            {
                ViewState["_DropDownSourceListWidth"] = 180;
            }
            return (int)(ViewState["_DropDownSourceListWidth"]);
        }
        set
        {
            ViewState["_DropDownSourceListWidth"] = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭(空白時自動隱藏)
    /// </summary>
    public string ucCaption
    {
        get
        {
            if (ViewState["_Caption"] == null)
            {
                ViewState["_Caption"] = "";
            }
            return (string)(ViewState["_Caption"]);
        }
        set
        {
            ViewState["_Caption"] = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭寬度(預設 80)
    /// </summary>
    public int ucCaptionWidth
    {
        get
        {
            if (ViewState["_CaptionWidth"] == null)
            {
                ViewState["_CaptionWidth"] = 80;
            }
            return (int)(ViewState["_CaptionWidth"]);
        }
        set
        {
            ViewState["_CaptionWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 25)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (ViewState["_BtnWidth"] == null)
            {
                ViewState["_BtnWidth"] = 25;
            }
            return (int)(ViewState["_BtnWidth"]);
        }
        set
        {
            ViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕高度(預設 19)
    /// </summary>
    public int ucBtnHeight
    {
        get
        {
            if (ViewState["_BtnHeight"] == null)
            {
                ViewState["_BtnHeight"] = 19;
            }
            return (int)(ViewState["_BtnHeight"]);
        }
        set
        {
            ViewState["_BtnHeight"] = value;
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
                ViewState["_BtnCaption"] = "▼";
            }
            return (string)(ViewState["_BtnCaption"]);
        }
        set
        {
            ViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕提示訊息
    /// </summary>
    public string ucBtnToolTip
    {
        get
        {
            if (ViewState["_BtnToolTip"] == null)
            {
                ViewState["_BtnToolTip"] = RS.Resources.CommPhrase_btnAppend; //添加片語
            }
            return (string)(ViewState["_BtnToolTip"]);
        }
        set
        {
            ViewState["_BtnToolTip"] = value;
        }
    }

    /// <summary>
    /// 按鈕 CssClass (預設 Util_clsBtnGray + Util_Pointer)
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (ViewState["_BtnCssClass"] == null)
            {
                ViewState["_BtnCssClass"] = "Util_clsBtnGray Util_Pointer";
            }
            return (string)(ViewState["_BtnCssClass"]);
        }
        set
        {
            ViewState["_BtnCssClass"] = value;
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
            if (ViewState["_IsSearchEnabled"] == null)
            {
                ViewState["_IsSearchEnabled"] = true;
            }
            return (bool)(ViewState["_IsSearchEnabled"]);
        }
        set
        {
            ViewState["_IsSearchEnabled"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框寬度(預設 100)
    /// </summary>
    public int ucSearchBoxWidth
    {
        get
        {
            if (ViewState["_SearchBoxWidth"] == null)
            {
                ViewState["_SearchBoxWidth"] = 100;
            }
            return (int)(ViewState["_SearchBoxWidth"]);
        }
        set
        {
            ViewState["_SearchBoxWidth"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框浮水印內容
    /// </summary>
    public string ucSearchBoxWaterMarkText
    {
        get
        {
            if (ViewState["_SearchBoxWaterMarkText"] == null)
            {
                ViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommPhrase_WaterMarkText; //搜尋片語
            }
            return (string)(ViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            ViewState["_SearchBoxWaterMarkText"] = value;
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