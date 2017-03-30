﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

/// <summary>
/// 單、複選[項目清單]控制項
/// <para></para>
/// <para>不必寫Code，只需定義即可作到：</para>
/// <para>    -附掛在ucGridView或一般FormView內，設定完即可使用</para>
/// <para>    -可設定能挑選的項目數量範圍，若最小數量為 0 ，代表為非必要輸入</para>
/// <para>    -可自訂選擇數量不合理時的錯誤訊息</para>
/// <para>    -可自訂顯示文字欄位的長度/寬度</para>
/// <para>    -可自訂項目彈出區塊的長度/寬度</para>
/// </summary>
public partial class Util_ucCheckBoxList : BaseUserControl
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
    public int ucChkBoxListWidth
    {
        get
        {
            if (ViewState["_ChkBoxListWidth"] == null) ViewState["_ChkBoxListWidth"] = 200;
            return (int)ViewState["_ChkBoxListWidth"];
        }

        set
        {
            ViewState["_ChkBoxListWidth"] = value;
        }
    }

    /// <summary>
    /// 核取清單總高度(預設值=175)
    /// </summary>
    public int ucChkBoxListHeight
    {
        get
        {
            if (ViewState["_ChkBoxListHeight"] == null) ViewState["_ChkBoxListHeight"] = 175;
            return (int)ViewState["_ChkBoxListHeight"];
        }

        set
        {
            ViewState["_ChkBoxListHeight"] = value;
        }
    }

    /// <summary>
    /// 核取清單 X軸 偏移植(預設 0) 
    /// </summary>
    public int ucChkBoxListOffsetX
    {
        //2016.08.19 新增
        get
        {
            if (ViewState["_ucChkBoxListOffsetX"] == null)
            {
                ViewState["_ucChkBoxListOffsetX"] = 0;
            }
            return (int)(ViewState["_ucChkBoxListOffsetX"]);
        }
        set
        {
            ViewState["_ucChkBoxListOffsetX"] = value;
        }
    }

    /// <summary>
    /// 核取清單 Y軸 偏移植(預設 0) 
    /// </summary>
    public int ucChkBoxListOffsetY
    {
        //2016.08.19 新增
        get
        {
            if (ViewState["_ucChkBoxListOffsetY"] == null)
            {
                ViewState["_ucChkBoxListOffsetY"] = 0;
            }
            return (int)(ViewState["_ucChkBoxListOffsetY"]);
        }
        set
        {
            ViewState["_ucChkBoxListOffsetY"] = value;
        }
    }

    /// <summary>
    /// 顯示框列數(預設值=1)
    /// </summary>
    public int ucRows
    {
        get
        {
            if (ViewState["_Rows"] == null) ViewState["_Rows"] = 1;
            return (int)ViewState["_Rows"];
        }
        set
        {
            ViewState["_Rows"] = value;
        }
    }

    /// <summary>
    /// 顯示框為多列數時，是否逐行顯示項目(預設true)
    /// </summary>
    public bool ucIsLineByLineWhenMultiRow
    {
        get
        {
            if (ViewState["_IsLineByLineWhenMultiRow"] == null) ViewState["_IsLineByLineWhenMultiRow"] = true;
            return (bool)ViewState["_IsLineByLineWhenMultiRow"];
        }
        set
        {
            ViewState["_IsLineByLineWhenMultiRow"] = value;
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
            ChkBoxList1.DataSource = value;
            ChkBoxList1.DataValueField = "Key";
            ChkBoxList1.DataTextField = "Value";
            ChkBoxList1.DataBind();
            for (int i = 0; i < ChkBoxList1.Items.Count; i++)
            {
                ChkBoxList1.Items[i].Attributes.Add("style", "white-space:nowrap;font-size:10pt;font-weight:normal;");
            }
        }
    }

    /// <summary>
    /// 已選取的ID清單
    /// </summary>
    public string ucSelectedIDList
    {
        get
        {
            return txtIDList.Text;
        }
        set
        {
            txtIDList.Text = value;
            if (!string.IsNullOrEmpty(txtIDList.Text))
            {
                //檢查內容是否合理 2015.08.26
                txtIDList.Text = Util.getStringJoin(Util.getCompareList(Util.getArray(ucSourceDictionary), txtIDList.Text.Trim().Split(','), Util.ListCompareMode.Same));
            }

            //根據內容處理相關欄位
            if (string.IsNullOrEmpty(txtIDList.Text))
            {
                txtIDQty.Text = "0";
                txtInfoList.Text = "";
            }
            else
            {
                txtInfoList.Text = Util.getStringJoin(Util.getArray(Util.getDictionary(ucSourceDictionary, txtIDList.Text.Trim().Split(',')), 1));
                txtIDQty.Text = txtInfoList.Text.Split(',').Count().ToString();
            }
        }
    }

    /// <summary>
    /// 選擇結果(Dictionary格式)
    /// </summary>
    public Dictionary<string, string> ucSelectedDictionary
    {
        //2016.08.19 新增
        get
        {
            return Util.getDictionary(ucSourceDictionary, txtIDList.Text.Trim().Split(','));
        }
    }

    /// <summary>
    /// 自訂Range最小選擇數量(預設值=0)
    /// </summary>
    public int ucRangeMinQty
    {
        get
        {
            if (ViewState["_RangeMinQty"] == null) { ViewState["_RangeMinQty"] = 0; } return (int)ViewState["_RangeMinQty"];
        }
        set
        {
            ViewState["_RangeMinQty"] = value;
        }
    }

    /// <summary>
    /// 自訂Range最大選擇數量(預設值=99)
    /// </summary>
    public int ucRangeMaxQty
    {
        get
        {
            if (ViewState["_RangeMaxQty"] == null) { ViewState["_RangeMaxQty"] = 99; } return (int)ViewState["_RangeMaxQty"];
        }
        set
        {
            ViewState["_RangeMaxQty"] = value;
        }
    }

    /// <summary>
    /// 自訂Range錯誤訊息(預設值=*)
    /// </summary>
    public string ucRangeErrorMessage
    {
        get
        {
            if (ViewState["_RangeErrMsg"] == null) ViewState["_RangeErrMsg"] = "*";
            return (string)ViewState["_RangeErrMsg"];
        }
        set
        {
            ViewState["_RangeErrMsg"] = value;
            txtIDQty_RangeValidator.ErrorMessage = value;
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
    /// 選擇按鈕區是否顯示(預設true)
    /// </summary>
    public bool ucIsSelectBoxEnabled
    {
        get { if (ViewState["_IsSelectBoxEnabled"] == null) { ViewState["_IsSelectBoxEnabled"] = true; } return (bool)ViewState["_IsSelectBoxEnabled"]; }
        set { ViewState["_IsSelectBoxEnabled"] = value; }
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
    /// 無選擇資料時，是否自動彈出候選清單(預設 false)
    /// </summary>
    public bool ucIsAutoPopWhenNoSelection //2016.08.31
    {
        get
        {
            if (ViewState["_IsAutoPopWhenNoSelection"] == null)
            {
                ViewState["_IsAutoPopWhenNoSelection"] = false;
            }
            return (bool)(ViewState["_IsAutoPopWhenNoSelection"]);
        }
        set
        {
            ViewState["_IsAutoPopWhenNoSelection"] = value;
        }
    }

    /// <summary>
    /// 當PostBack後，控制項是否自動Refresh(預設true)
    /// </summary>
    public bool ucIsAutoRefresh //2016.06.06
    {
        get
        {
            if (ViewState["_IsAutoRefresh"] == null)
            {
                ViewState["_IsAutoRefresh"] = true;
            }
            return (bool)(ViewState["_IsAutoRefresh"]);
        }
        set
        {
            ViewState["_IsAutoRefresh"] = value;
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
        if (!IsPostBack)
        {
            Refresh();
        }
        else
        {
            if (ucIsAutoRefresh) //2016.06.06
            {
                Refresh();
            }
        }
    }

    /// <summary>
    /// 重新整理控制項
    /// </summary>
    public void Refresh()
    {
        //chkVisibility 相關設定
        string strRequireJS = "";
        if (ucRangeMinQty > 0)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + txtIDQty_RangeValidator.ClientID + "');";
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

        txtIDQty_RangeValidator.MinimumValue = ucRangeMinQty.ToString();
        txtIDQty_RangeValidator.MaximumValue = ucRangeMaxQty.ToString();

        if (!string.IsNullOrEmpty(ucToolTip))
        {
            txtInfoList.Attributes.Add("title", ucToolTip);
        }

        if (ucIsReadOnly)
        {
            txtInfoList.CssClass = ucReadOnlyCSS;
            txtInfoList.Attributes.Remove("onclick");
            txtInfoList.Style.Remove("cursor");
        }
        else
        {
            txtInfoList.Style["cursor"] = "pointer";
            //2016.08.19 新增
            if (ucChkBoxListOffsetX != 0)
            {
                divPopPanel.Style["left"] = string.Format("{0}px", ucChkBoxListOffsetX);
            }

            //2016.08.19 新增
            if (ucChkBoxListOffsetY != 0)
            {
                divPopPanel.Style["top"] = string.Format("{0}px", ucChkBoxListOffsetY);
            }

            txtInfoList.CssClass = ucCssClass;
            //因為物件可能由ucGridView動態載入，故初始[選取]項目的動作，改從JS發動
            string strClickJS = "";
            strClickJS += "Util_ToggleDisplay('" + divPopPanel.ClientID + "');";
            strClickJS += string.Format("Util_SetChkBoxListSelectedItemFromTextBox('{0}','{1}','{2}','{3}');", txtIDList.ClientID, ChkBoxList1.ClientID, ChkBoxList1.Items.Count, txtInfoList.ClientID);
            //是否多列又需逐行顯示
            if (ucIsLineByLineWhenMultiRow && ucRows > 1)
            {
                strClickJS += "oID = document.getElementById('" + txtInfoList.ClientID + "');if (oID.value.length > 0){oID.value = oID.value.replace(/,/g, '\\n');}";
            }
            //[點選]切換彈出視窗的顯示/隱藏
            txtInfoList.Attributes.Add("onclick", strClickJS);
        }

        txtIDList.Style.Add("display", "none");
        txtIDQty.Style.Add("display", "none");
        if (string.IsNullOrEmpty(txtIDQty.Text))
        {
            txtIDQty.Text = "0";
        }

        if (!string.IsNullOrEmpty(txtIDList.Text))
        {
            //只取有在候選清單內的項目
            txtInfoList.Text = Util.getStringJoin(Util.getArray(Util.getDictionary(ucSourceDictionary, txtIDList.Text.Trim().Split(',')), 1));
            txtIDQty.Text = string.IsNullOrEmpty(txtInfoList.Text) ? "0" : txtInfoList.Text.Split(',').Count().ToString();
        }

        //設定 txtInfoList 物件
        if (ucIsWidthByPixel)
            txtInfoList.Width = Unit.Pixel(ucWidth);
        else
            txtInfoList.Width = Unit.Percentage(ucWidth);

        txtInfoList.Rows = ucRows;
        txtInfoList.TextMode = (txtInfoList.Rows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;

        //是否多列又需逐行顯示
        if (ucIsLineByLineWhenMultiRow && ucRows > 1)
        {
            if (!string.IsNullOrEmpty(txtInfoList.Text))
            {
                txtInfoList.Text = txtInfoList.Text.Replace(",", "\n");
            }
        }

        //處理自動彈出選單 2016.08.31 新增
        if (ucIsAutoPopWhenNoSelection && int.Parse(txtIDQty.Text) <= 0)
        {
            divPopPanel.Style["display"] = "";
        }

        //設定 CSS
        pnlBoxList.Style.Value = string.Format("background-color: #FFF;BORDER: 1px solid #707070;WIDTH: {0}px; HEIGHT: {1}px;", ucChkBoxListWidth, ucChkBoxListHeight);
        divBoxList.Style.Value = string.Format("BORDER:0px none #707070; overflow:auto; WIDTH: {0}px; HEIGHT: {1}px;", ucChkBoxListWidth, ucChkBoxListHeight - (ucIsSearchBoxEnabled ? 32 : 1) - (ucIsSelectBoxEnabled ? 28 : 1) - (ucIsExitBoxEnabled ? 28 : 1));

        //勾選單一項目JS
        string strJS = "";
        strJS += string.Format("Util_ChkBoxListSelectedItem('{0}','{1}','{2}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, null);
        strJS += string.Format("Util_GetChkBoxListSelectedItemToTextBox('{0}','{1}','{2}','{3}','{4}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, txtIDList.ClientID, txtInfoList.ClientID, "N");
        strJS += string.Format("Util_GetTextBoxStringListQtyToTextBox('{0}','{1}','{2}');", txtIDList.ClientID, txtIDQty.ClientID, ",");

        //是否多列又需逐行顯示
        if (ucIsLineByLineWhenMultiRow && ucRows > 1)
        {
            strJS += "oID = document.getElementById('" + txtInfoList.ClientID + "');if (oID.value.length > 0){oID.value = oID.value.replace(/,/g, '\\n');}";
        }
        ChkBoxList1.Attributes.Add("onclick", strJS);

        //處理搜尋功能
        if (ucIsSearchBoxEnabled)
        {
            divSearch.Style["display"] = "inline-block";
            txtSearch.CssClass = ucWaterMarkCSS;
            txtSearch.Text = _SearchWaterMarkText;
            txtSearch.Attributes.CssStyle.Add("width", (ucChkBoxListWidth - 10) + "px");
            txtSearch.Attributes.CssStyle.Add("height", "20px");
            txtSearch.Attributes.CssStyle.Add("margin-left", "1px");
            txtSearch.Attributes.Add("OnFocus", string.Format("Util_WaterMark_Focus('{0}', '{1}');", txtSearch.ClientID, _SearchWaterMarkText.Replace("'", "\'")));
            txtSearch.Attributes.Add("OnBlur", string.Format("Util_WaterMark_Blur('{0}', '{1}');", txtSearch.ClientID, _SearchWaterMarkText.Replace("'", "\'")));
            txtSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");  //預防按了 Enter 送出 PostBack
            txtSearch.Attributes.Add("onkeyup", string.Format("Util_ChkBoxListItemSearch('{0}','{1}','{2}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, txtSearch.ClientID));
        }
        else
        {
            divSearch.Style["display"] = "none";
        }

        //選擇按鈕
        if (ucIsSelectBoxEnabled)
        {
            btnAll.Text = SinoPac.WebExpress.Common.Properties.Resources.CommMultiSelect_SelectAll;
            btnClear.Text = SinoPac.WebExpress.Common.Properties.Resources.CommMultiSelect_CancelAll;

            //全選JS
            strJS = "";
            strJS += string.Format("Util_ChkBoxListSelectAllItem('{0}','{1}','{2}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, null);
            strJS += string.Format("Util_GetChkBoxListSelectedItemToTextBox('{0}','{1}','{2}','{3}','{4}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, txtIDList.ClientID, txtInfoList.ClientID, "N");
            strJS += string.Format("Util_GetTextBoxStringListQtyToTextBox('{0}','{1}','{2}');", txtIDList.ClientID, txtIDQty.ClientID, ",");

            //是否多列又需逐行顯示
            if (ucIsLineByLineWhenMultiRow && ucRows > 1)
            {
                strJS += "oID = document.getElementById('" + txtInfoList.ClientID + "');if (oID.value.length > 0){oID.value = oID.value.replace(/,/g, '\\n');}";
            }
            strJS += "return false;";
            btnAll.OnClientClick = strJS;

            //全清JS
            strJS = "";
            strJS += string.Format("Util_ChkBoxListClearAllItem('{0}','{1}','{2}');", ChkBoxList1.ClientID, ChkBoxList1.Items.Count, null);
            strJS += string.Format("document.getElementById('{0}').value='';document.getElementById('{1}').value='';", txtIDList.ClientID, txtInfoList.ClientID);
            strJS += string.Format("Util_GetTextBoxStringListQtyToTextBox('{0}','{1}','{2}');", txtIDList.ClientID, txtIDQty.ClientID, ",");
            strJS += "return false;";
            btnClear.OnClientClick = strJS;
        }
        else
        {
            divSelect.Style["display"] = "none";
        }

        //離開按鈕
        if (ucIsExitBoxEnabled)
        {
            btnExit.Text = SinoPac.WebExpress.Common.Properties.Resources.Msg_Exit;
            btnExit.OnClientClick = "Util_ToggleDisplay('" + divPopPanel.ClientID + "');return false;";
        }
        else
        {
            divExit.Style["display"] = "none";
        }
    }
}