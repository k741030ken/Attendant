using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

/// <summary>
/// 單選[項目清單]控制項
/// <para></para>
/// <para>不必寫Code，只需定義即可作到：</para>
/// <para>    -附掛在ucGridView或一般FormView內，設定完即可使用</para>
/// <para>    -可自訂是否為非必要輸入</para>
/// <para>    -可自訂顯示文字欄位的長度/寬度</para>
/// <para>    -可自訂項目彈出區塊的長度/寬度</para>
/// </summary>
public partial class Util_ucRadioList : BaseUserControl
{
    private string _ReadOnlyClass = "Util_clsReadOnly";
    private string _WaterMarkClass = "Util_WaterMarkedTextBox";
    protected string _ToolTip = SinoPac.WebExpress.Common.Properties.Resources.CheckBoxList_ToolTip;
    protected string _SearchWaterMarkText = SinoPac.WebExpress.Common.Properties.Resources.CommMultiSelect_WaterMarkText; // "搜尋內容";

    #region 屬性
    /// <summary>
    /// 顯示框Width計量單位是否為[像素](預設值=true)
    /// </summary>
    public bool ucIsWidthByPixel
    {
        get
        {
            if (ViewState["_IsWidthByPixel"] == null) ViewState["_IsWidthByPixel"] = true;
            return (bool)ViewState["_IsWidthByPixel"];
        }
        set
        {
            ViewState["_IsWidthByPixel"] = value;
        }
    }

    /// <summary>
    /// 顯示框寬度(預設值=100)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null) ViewState["_Width"] = 100;
            return (int)ViewState["_Width"];
        }

        set
        {
            ViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 核取清單寬度(預設值=200)
    /// </summary>
    public int ucRadioListWidth
    {
        get
        {
            if (ViewState["_RadioListWidth"] == null) ViewState["_RadioListWidth"] = 200;
            return (int)ViewState["_RadioListWidth"];
        }

        set
        {
            ViewState["_RadioListWidth"] = value;
        }
    }

    /// <summary>
    /// 控制項總高度(預設值=175)
    /// </summary>
    public int ucRadioListHeight
    {
        get
        {
            if (ViewState["_RadioListHeight"] == null) ViewState["_RadioListHeight"] = 175;
            return (int)ViewState["_RadioListHeight"];
        }

        set
        {
            ViewState["_RadioListHeight"] = value;
        }
    }

    /// <summary>
    /// 指定候選資料來源
    /// </summary>
    public Dictionary<string, string> ucSourceDictionary
    {
        get
        {
            if (ViewState["_SourceDictionary"] == null) ViewState["_SourceDictionary"] = null;
            return (Dictionary<string, string>)ViewState["_SourceDictionary"];
        }
        set
        {
            ViewState["_SourceDictionary"] = value;

            //設定候選項目
            RadioList1.DataSource = value;
            RadioList1.DataValueField = "Key";
            RadioList1.DataTextField = "Value";
            RadioList1.DataBind();
            for (int i = 0; i < RadioList1.Items.Count; i++)
            {
                RadioList1.Items[i].Attributes.Add("style", "white-space:nowrap;font-size:10pt;font-weight:normal;");
            }
        }
    }

    /// <summary>
    /// 已選取的ID清單
    /// </summary>
    public string ucSelectedID
    {
        get
        {
            return txtID.Text;
        }
        set
        {
            txtID.Text = value;
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                //檢查內容是否合理 2015.08.26
                txtID.Text = Util.getStringJoin(Util.getCompareList(Util.getArray(ucSourceDictionary), txtID.Text.Trim().Split(','), Util.ListCompareMode.Same));
            }

            //根據內容處理相關欄位
            if (string.IsNullOrEmpty(txtID.Text))
            {
                txtInfo.Text = "";
            }
            else
            {
                txtInfo.Text = Util.getStringJoin(Util.getArray(Util.getDictionary(ucSourceDictionary, txtID.Text.Trim().Split(',')), 1));
            }
        }
    }

    /// <summary>
    /// 是否需 Require(預設 false)
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (ViewState["_IsRequire"] == null)
            {
                ViewState["_IsRequire"] = false;
            }
            return (bool)(ViewState["_IsRequire"]);
        }
        set
        {
            ViewState["_IsRequire"] = value;
            txtID.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// 提示訊息
    /// </summary>
    public string ucToolTip
    {
        get
        {
            if (ViewState["_ToolTip"] == null)
            {
                ViewState["_ToolTip"] = _ToolTip;
            }
            return (string)(ViewState["_ToolTip"]);
        }
        set
        {
            ViewState["_ToolTip"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框標籤內容
    /// </summary>
    public string ucSearchBoxWaterMarkText
    {
        get { return _SearchWaterMarkText; }
        set { _SearchWaterMarkText = value; }
    }

    /// <summary>
    /// 搜尋文字框是否顯示(預設true)
    /// </summary>
    public bool ucIsSearchBoxEnabled
    {
        get { if (ViewState["_IsSearchBoxEnabled"] == null) { ViewState["_IsSearchBoxEnabled"] = true; } return (bool)ViewState["_IsSearchBoxEnabled"]; }
        set { ViewState["_IsSearchBoxEnabled"] = value; }
    }

    /// <summary>
    /// 離開按鈕區是否顯示(預設true)
    /// </summary>
    public bool ucIsExitBoxEnabled
    {
        get { if (ViewState["_IsExitBoxEnabled"] == null) { ViewState["_IsExitBoxEnabled"] = true; } return (bool)ViewState["_IsExitBoxEnabled"]; }
        set { ViewState["_IsExitBoxEnabled"] = value; }
    }

    /// <summary>
    /// Watermark 時的 CSSClass 名稱(預設：[Util_WaterMarkedTextBox])
    /// </summary>
    public string ucWaterMarkCSS
    {
        get
        {
            if (ViewState["_WaterMarkCSS"] == null)
            {
                ViewState["_WaterMarkCSS"] = _WaterMarkClass;
            }
            return (string)(ViewState["_WaterMarkCSS"]);
        }
        set
        {
            ViewState["_WaterMarkCSS"] = value;
        }
    }

    /// <summary>
    /// 是否 ReadOnly
    /// </summary>
    public bool ucIsReadOnly
    {
        get
        {
            if (ViewState["_IsReadOnly"] == null)
            {
                ViewState["_IsReadOnly"] = false;
            }
            return (bool)(ViewState["_IsReadOnly"]);
        }
        set
        {
            ViewState["_IsReadOnly"] = value;
        }
    }

    /// <summary>
    /// ReadOnly 時的 CSSClass 名稱(預設：[Util_clsReadOnly])
    /// </summary>
    public string ucReadOnlyCSS
    {
        get
        {
            if (ViewState["_ReadOnlyCSS"] == null)
            {
                ViewState["_ReadOnlyCSS"] = _ReadOnlyClass;
            }
            return (string)(ViewState["_ReadOnlyCSS"]);
        }
        set
        {
            ViewState["_ReadOnlyCSS"] = value;
        }
    }

    /// <summary>
    /// 一般狀態時的 CSSClass 名稱(預設：無)
    /// </summary>
    public string ucCssClass
    {
        get
        {
            if (ViewState["_CssClass"] == null)
            {
                ViewState["_CssClass"] = "";
            }
            return (string)(ViewState["_CssClass"]);
        }
        set
        {
            ViewState["_CssClass"] = value;
        }
    }

    /// <summary>
    /// 是否使用切換可見功能
    /// </summary>
    public bool ucIsToggleVisibility
    {
        get
        {
            if (ViewState["_IsToggleVisibility"] == null)
            {
                ViewState["_IsToggleVisibility"] = false;
            }
            return (bool)(ViewState["_IsToggleVisibility"]);
        }
        set
        {
            ViewState["_IsToggleVisibility"] = value;
            if (value == true)
            {
                chkVisibility.Visible = true;
                divDataArea.Style["visibility"] = "";
            }
            else
            {
                chkVisibility.Visible = false;
                divDataArea.Style["visibility"] = "hidden";
            }
        }
    }

    /// <summary>
    /// 控制項可見狀態
    /// </summary>
    public bool ucIsVisibility
    {
        get
        {
            return chkVisibility.Checked;
        }
        set
        {
            chkVisibility.Checked = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭(空白時自動隱藏)
    /// </summary>
    public string ucCaption
    {
        get
        {
            return labCaption.Text.Trim();
        }

        set
        {
            value = value.Trim();
            labCaption.Text = value;
            if (string.IsNullOrEmpty(value))
            {
                labCaption.Visible = false;
            }
            else
            {
                labCaption.Visible = true;
            }
        }
    }

    /// <summary>
    /// 控制項顯示抬頭寬度(預設 80)
    /// </summary>
    public int ucCaptionWidth
    {
        get { return int.Parse(labCaption.Width.ToString()); }
        set
        {
            labCaption.Width = Unit.Pixel(value);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }

    /// <summary>
    /// 重新整理控制項
    /// </summary>
    public void Refresh()
    {
        //chkVisibility 相關設定
        string strRequireJS = "";
        if (ucIsRequire)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator1.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        strRequireJS += string.Format("Util_ChkBoxToggleVisibility('{0}', '{1}');", chkVisibility.ClientID, divDataArea.ClientID);
        chkVisibility.Attributes.Add("onclick", strRequireJS);
        if (chkVisibility.Checked)
        {
            divDataArea.Style["visibility"] = "";
        }
        else
        {
            divDataArea.Style["visibility"] = "hidden";
        }

        if (!string.IsNullOrEmpty(ucToolTip))
        {
            txtInfo.Attributes.Add("title", ucToolTip);
        }

        if (ucIsReadOnly)
        {
            txtInfo.CssClass = ucReadOnlyCSS;
            txtInfo.Attributes.Remove("onclick");
        }
        else
        {
            txtInfo.CssClass = ucCssClass;
            //因為物件可能由ucGridView動態載入，故初始[選取]項目的動作，改從JS發動
            string strClickJS = "";
            strClickJS += "Util_ToggleDisplay('" + divPopPanel.ClientID + "');";
            strClickJS += string.Format("Util_SetChkBoxListSelectedItemFromTextBox('{0}','{1}','{2}','{3}');", txtID.ClientID, RadioList1.ClientID, RadioList1.Items.Count, txtInfo.ClientID);
            //[點選]切換彈出視窗的顯示/隱藏
            txtInfo.Attributes.Add("onclick", strClickJS);
        }

        txtID.Style.Add("display", "none");
        if (!string.IsNullOrEmpty(txtID.Text))
        {
            //只取有在候選清單內的項目
            txtInfo.Text = Util.getStringJoin(Util.getArray(Util.getDictionary(ucSourceDictionary, txtID.Text.Trim().Split(',')), 1));
        }

        //設定 txtInfoList 物件

        if (ucIsWidthByPixel)
            txtInfo.Width = Unit.Pixel(ucWidth);
        else
            txtInfo.Width = Unit.Percentage(ucWidth);

        txtInfo.Rows = 1;
        txtInfo.TextMode = TextBoxMode.SingleLine;

        //設定 CSS
        pnlBoxList.Style.Value = string.Format("background-color: #FFF;BORDER: 1px solid #707070;WIDTH: {0}px; HEIGHT: {1}px;", ucRadioListWidth, ucRadioListHeight);
        divBoxList.Style.Value = string.Format("BORDER:0px none #707070; overflow:auto; WIDTH: {0}px; HEIGHT: {1}px;", ucRadioListWidth, ucRadioListHeight - (ucIsSearchBoxEnabled ? 32 : 1) - (ucIsExitBoxEnabled ? 28 : 1));

        //勾選單一項目JS
        string strJS = "";
        strJS += string.Format("Util_ChkBoxListSelectedItem('{0}','{1}','{2}');", RadioList1.ClientID, RadioList1.Items.Count, null);
        strJS += string.Format("Util_GetChkBoxListSelectedItemToTextBox('{0}','{1}','{2}','{3}','{4}');", RadioList1.ClientID, RadioList1.Items.Count, txtID.ClientID, txtInfo.ClientID, "N");
        strJS += "Util_ToggleDisplay('" + divPopPanel.ClientID + "');";
        RadioList1.Attributes.Add("onclick", strJS);

        //處理搜尋功能
        if (ucIsSearchBoxEnabled)
        {
            divSearch.Style["display"] = "inline-block";
            txtSearch.CssClass = ucWaterMarkCSS;
            txtSearch.Text = _SearchWaterMarkText;
            txtSearch.Attributes.CssStyle.Add("width", (ucRadioListWidth - 10) + "px");
            txtSearch.Attributes.CssStyle.Add("height", "20px");
            txtSearch.Attributes.CssStyle.Add("margin-left", "1px");
            txtSearch.Attributes.Add("OnFocus", string.Format("Util_WaterMark_Focus('{0}', '{1}');", txtSearch.ClientID, _SearchWaterMarkText.Replace("'", "\'")));
            txtSearch.Attributes.Add("OnBlur", string.Format("Util_WaterMark_Blur('{0}', '{1}');", txtSearch.ClientID, _SearchWaterMarkText.Replace("'", "\'")));
            txtSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");  //預防按了 Enter 送出 PostBack
            txtSearch.Attributes.Add("onkeyup", string.Format("Util_ChkBoxListItemSearch('{0}','{1}','{2}');", RadioList1.ClientID, RadioList1.Items.Count, txtSearch.ClientID));
        }
        else
        {
            divSearch.Style["display"] = "none";
        }

        //離開按鈕
        if (ucIsExitBoxEnabled)
        {
            //全清JS
            strJS = "";
            strJS += string.Format("Util_ChkBoxListClearAllItem('{0}','{1}','{2}');", RadioList1.ClientID, RadioList1.Items.Count, null);
            strJS += string.Format("document.getElementById('{0}').value='';document.getElementById('{1}').value='';", txtID.ClientID, txtInfo.ClientID);
            strJS += "Util_ToggleDisplay('" + divPopPanel.ClientID + "');";
            strJS += "return false;";
            btnClear.Text = SinoPac.WebExpress.Common.Properties.Resources.Msg_Clear;
            btnClear.OnClientClick = strJS;

            btnExit.Text = SinoPac.WebExpress.Common.Properties.Resources.Msg_Exit;
            btnExit.OnClientClick = "Util_ToggleDisplay('" + divPopPanel.ClientID + "');return false;";
        }
        else
        {
            divExit.Style["display"] = "none";
        }
    }
}