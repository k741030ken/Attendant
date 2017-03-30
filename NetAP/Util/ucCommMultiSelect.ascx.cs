using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
///  [常用多重項目選單]控制項
/// </summary>
public partial class Util_ucCommMultiSelect : System.Web.UI.UserControl
{

    /// <summary>
    /// 指定候選資料來源
    /// </summary>
    public Dictionary<string, string> ucSourceDictionary
    {
        get
        {
            if (ViewState["_SourceDictionary"] == null)
            {
                return null;
            }
            return (Dictionary<string, string>)(ViewState["_SourceDictionary"]);
        }
        set
        {
            ViewState["_SourceDictionary"] = value;
        }
    }



    /// <summary>
    /// [全選]前的提示訊息(若為空白則直接執行)
    /// </summary>
    public string ucSelectAllConfirmMsg
    {
        get
        {
            if (ViewState["_SelectAllConfirmMsg"] == null)
            {
                ViewState["_SelectAllConfirmMsg"] = "";
            }
            return (string)(ViewState["_SelectAllConfirmMsg"]);
        }
        set
        {
            ViewState["_SelectAllConfirmMsg"] = value;
        }
    }


    /// <summary>
    /// 選擇結果ID清單
    /// </summary>
    public string ucSelectedIDList
    {
        get { return idSelectedIDList.Text; }
        set { idSelectedIDList.Text = value; }
    }

    /// <summary>
    /// 選擇結果Info清單
    /// </summary>
    public string ucSelectedInfoList
    {
        get { return idSelectedInfoList.Text; }
        set { idSelectedInfoList.Text = value; }
    }

    /// <summary>
    /// 選擇結果
    /// </summary>
    public Dictionary<string, string> ucSelectedDictionary
    {
        get
        {
            Dictionary<string, string> oDic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(ucSelectedIDList))
            {
                for (int i = 0; i < ucSelectedIDList.Split(',').Count(); i++)
                {
                    oDic.Add(ucSelectedIDList.Split(',')[i], ucSelectedInfoList.Split(',')[i]);
                }
            }
            return oDic;
        }
    }

    /// <summary>
    /// 選擇結果ID清單輸出到網頁父階指定物件ID
    /// </summary>
    public string ucSelectedIDListToParentObjClientID
    {
        get
        {
            if (ViewState["_SelectedIDListToParentObjClientID"] == null)
            {
                ViewState["_SelectedIDListToParentObjClientID"] = "";
            }
            return (string)(ViewState["_SelectedIDListToParentObjClientID"]);
        }
        set
        {
            ViewState["_SelectedIDListToParentObjClientID"] = value;
        }
    }


    /// <summary>
    /// 選擇結果Info清單輸出到網頁父階指定物件ID
    /// </summary>
    public string ucSelectedInfoListToParentObjClientID
    {
        get
        {
            if (ViewState["_SelectedInfoListToParentObjClientID"] == null)
            {
                ViewState["_SelectedInfoListToParentObjClientID"] = "";
            }
            return (string)(ViewState["_SelectedInfoListToParentObjClientID"]);
        }
        set
        {
            ViewState["_SelectedInfoListToParentObjClientID"] = value;
        }
    }


    /// <summary>
    /// 候選/結果區寬度(預設 240)
    /// </summary>
    public int ucBoxListWidth
    {
        get
        {
            if (ViewState["_BoxListWidth"] == null)
            {
                ViewState["_BoxListWidth"] = 240;
            }
            return (int)(ViewState["_BoxListWidth"]);
        }
        set
        {
            ViewState["_BoxListWidth"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區高度(預設 130)
    /// </summary>
    public int ucBoxListHeight
    {
        get
        {
            if (ViewState["_BoxListHeight"] == null)
            {
                ViewState["_BoxListHeight"] = 130;
            }
            return (int)(ViewState["_BoxListHeight"]);
        }
        set
        {
            ViewState["_BoxListHeight"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區 CSS Style
    /// </summary>
    public string ucBoxListStyle
    {
        get
        {
            if (ViewState["_BoxListStyle"] == null)
            {
                ViewState["_BoxListStyle"] = "font-size:10pt;white-space:nowrap;border: 1px solid silver; overflow:auto;";
            }
            return (string)(ViewState["_BoxListStyle"]);
        }
        set
        {
            ViewState["_BoxListStyle"] = value;
        }
    }


    /// <summary>
    /// 候選區已選項目 CssClass
    /// </summary>
    public string ucChkBoxListSelectedItemCssClass
    {
        get
        {
            if (ViewState["_ChkBoxListSelectedItemCssClass"] == null)
            {
                ViewState["_ChkBoxListSelectedItemCssClass"] = "Util_ChkBoxListSelectedItem";
            }
            return (string)(ViewState["_ChkBoxListSelectedItemCssClass"]);
        }
        set
        {
            ViewState["_ChkBoxListSelectedItemCssClass"] = value;
        }
    }


    /// <summary>
    /// 結果區項目Style
    /// </summary>
    public string ucListItemStyle
    {
        get
        {
            if (ViewState["_ListItemStyle"] == null)
            {
                ViewState["_ListItemStyle"] = "clear:both;list-style:none;margin:2px;text-align:left;";
            }
            return (string)(ViewState["_ListItemStyle"]);
        }
        set
        {
            ViewState["_ListItemStyle"] = value;
        }
    }


    /// <summary>
    /// 搜尋文字框CssClass
    /// </summary>
    public string ucSearchBoxCssClass
    {
        get
        {
            if (ViewState["_SearchBoxCssClass"] == null)
            {
                ViewState["_SearchBoxCssClass"] = "Util_WaterMarkedTextBox";
            }
            return (string)(ViewState["_SearchBoxCssClass"]);
        }
        set
        {
            ViewState["_SearchBoxCssClass"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框標籤內容
    /// </summary>
    public string ucSearchBoxWaterMarkText
    {
        get
        {
            if (ViewState["_SearchBoxWaterMarkText"] == null)
            {
                ViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommMultiSelect_WaterMarkText; //搜尋內容
            }
            return (string)(ViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            ViewState["_SearchBoxWaterMarkText"] = value;
        }
    }

    /// <summary>
    /// 搜尋文字框是否顯示(預設 true)
    /// </summary>
    public bool ucIsSearchBoxEnabled
    {
        get
        {
            if (ViewState["IsSearchBoxEnabled"] == null)
            {
                ViewState["IsSearchBoxEnabled"] = true;
            }
            return (bool)ViewState["IsSearchBoxEnabled"];
        }
        set
        {
            ViewState["IsSearchBoxEnabled"] = value;
        }
    }

    /// <summary>
    /// 是否使用切換可見功能(預設 false)
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

    /// <summary>
    /// 是否需輸入 (預設 false)
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (ViewState["_IsRequire"] == null)
            {
                ViewState["_IsRequire"] = false; ;
            }
            return (bool)(ViewState["_IsRequire"]);
        }
        set
        {
            ViewState["_IsRequire"] = value;
            idSelectedIDList.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// 複選清單 X軸 偏移植(預設 0) 
    /// </summary>
    public int ucMultiSelectOffsetX
    {
        //2016.09.20 新增
        get
        {
            if (ViewState["_MultiSelectOffsetX"] == null)
            {
                ViewState["_MultiSelectOffsetX"] = 0;
            }
            return (int)(ViewState["_MultiSelectOffsetX"]);
        }
        set
        {
            ViewState["_MultiSelectOffsetX"] = value;
        }
    }

    /// <summary>
    /// 複選清單 Y軸 偏移植(預設 0) 
    /// </summary>
    public int ucMultiSelectOffsetY
    {
        //2016.09.20 新增
        get
        {
            if (ViewState["_MultiSelectOffsetY"] == null)
            {
                ViewState["_MultiSelectOffsetY"] = 0;
            }
            return (int)(ViewState["_MultiSelectOffsetY"]);
        }
        set
        {
            ViewState["_MultiSelectOffsetY"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setJS(Util._JSjQueryUrl, this.ClientID + "_jQuery");
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
        defResultItem.Text = "";
        idSelectedIDList.Style["display"] = "none";
        idSelectedInfoList.Style["display"] = "none";

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

        divSourceBoxList.Style.Value = string.Format("{0};width:{1}px;height:{2}px;", ucBoxListStyle, ucBoxListWidth, (ucIsSearchBoxEnabled) ? ucBoxListHeight - 28 : ucBoxListHeight);
        divResultBoxList.Style.Value = string.Format("{0};width:{1}px;height:{2}px;", ucBoxListStyle, ucBoxListWidth + 60, ucBoxListHeight);

        //2016.09.20 新增 
        if (ucMultiSelectOffsetX != 0)
        {
            MultiSelectArea.Style["left"] = string.Format("{0}px", ucMultiSelectOffsetX);
        }

        //2016.09.20 新增
        if (ucMultiSelectOffsetY != 0)
        {
            MultiSelectArea.Style["top"] = string.Format("{0}px", ucMultiSelectOffsetY);
        }

        //若無設定 Offset ，則需移除以下屬性以便相容舊版 2016.10.05
        if (ucMultiSelectOffsetX == 0 && ucMultiSelectOffsetY == 0)
        {
            divDataArea.Style.Remove("position");
            MultiSelectArea.Style.Remove("position");
        }

        if (ucSourceDictionary != null)
        {
            idSourceBoxList.DataSource = ucSourceDictionary;
            idSourceBoxList.DataValueField = "Key";
            idSourceBoxList.DataTextField = "Value";
            idSourceBoxList.DataBind();

            StringBuilder strJS = new StringBuilder();
            strJS.Clear();
            strJS.Append("\n\n$(function() {"

                    //候選區checkbox JS
                    + "  $('#" + idSourceBoxList.ClientID + " :checkbox').change(function () { \n"
                    + "      if ($(this).is(':checked')) { \n"
                    + "          $(this).closest('tr').attr('class', '" + ucChkBoxListSelectedItemCssClass + "'); \n"
                    + "          $('#" + ResultItemContent.ClientID + "').append('" + string.Format(Util._ReorderItemHtmlFormat, "' + $(this).val() + '", "' + $(this).next('label').text() + '") + "'); \n"
                    + "      } \n"
                    + "      else { \n"
                    + "         $(this).closest('tr').attr('class', ''); \n"
                //JS [data-value] 設值時加入雙引號防止當為特殊符號時會造成誤判 2016.08.17
                    + "         $('#" + ResultItemContent.ClientID + " li[data-value=\"' + $(this).val() + '\"]').closest('li').remove(); \n"
                    + "      } \n"
                    + "      " + this.ClientID + "_calData();\n"
                    + "  }); \n"

                    + "});");

            Util.setJSContent(strJS.ToString(), this.ClientID + "_Init");

            //全部選擇
            strJS.Clear();
            strJS.Append("$('#" + ResultItemContent.ClientID + "').empty();"
                    + "$('#" + idSourceBoxList.ClientID + " :checkbox').attr('checked', true).closest('tr').attr('class', '" + ucChkBoxListSelectedItemCssClass + "');"

                    + "$('#" + idSourceBoxList.ClientID + " :checkbox').each(function () {"
                    + "      $('#" + ResultItemContent.ClientID + "').append('" + string.Format(Util._ReorderItemHtmlFormat, "' + $(this).val() + '", "' + $(this).next('label').text() + '") + "');"
                    + "});"

                    + this.ClientID + "_calData();"
                    + "return false;"
                );
            btnSelectAll.OnClientClick = strJS.ToString();

            if (!string.IsNullOrEmpty(ucSelectAllConfirmMsg))
            {
                strJS.Clear();
                strJS.Append("if (confirm('" + ucSelectAllConfirmMsg + "')){"
                        + btnSelectAll.OnClientClick
                        + " } else { return false; }"
                    );
                btnSelectAll.OnClientClick = strJS.ToString();
            }
            btnSelectAll.Text = string.Format("►► {0}", RS.Resources.CommMultiSelect_SelectAll);

            //全部取消
            strJS.Clear();
            strJS.Append("$('#" + idSourceBoxList.ClientID + " :checkbox').attr('checked', false).closest('tr').attr('class', '');"
                    + "$('#" + ResultItemContent.ClientID + "').empty();"
                    + this.ClientID + "_calData();"
                    + "return false;"
                );
            btnCancelAll.OnClientClick = strJS.ToString();
            btnCancelAll.Text = string.Format("◄◄ {0}", RS.Resources.CommMultiSelect_CancelAll);

            //預設已選取項目
            if (ucSelectedIDList.Length > 0)
            {
                string[] arList = ucSelectedIDList.Split(',');
                strJS.Clear();

                for (int i = 0; i < arList.Count(); i++)
                {
                    //該項目必須有在候選清單內才合理
                    if (ucSourceDictionary.ContainsKey(arList[i]))
                    {
                        //JS [value] 設值時加入雙引號防止當為特殊符號時會造成誤判 2016.08.17
                        defResultItem.Text += string.Format(Util._ReorderItemHtmlFormat, arList[i], ucSourceDictionary[arList[i]]);
                        strJS.AppendFormat("$('#{0} :checkbox[value=\"{1}\"]').attr('checked',true); \n", idSourceBoxList.ClientID, arList[i]);
                        strJS.AppendFormat("$('#{0} :checkbox[value=\"{1}\"]').closest('tr').attr('class', 'Util_ChkBoxListSelectedItem'); \n", idSourceBoxList.ClientID, arList[i]);
                    }
                }

                if (strJS.Length > 0)
                {
                    Util.setJSContent("$(function() {" + strJS.ToString() + this.ClientID + "_calData(); \n }); \n", this.ClientID + "_Selected_Init");
                }
            }

            //處理搜尋功能
            if (ucIsSearchBoxEnabled)
            {
                txtSearch.Visible = true;
                txtSearch.CssClass = ucSearchBoxCssClass;
                txtSearch.Text = ucSearchBoxWaterMarkText;
                txtSearch.Attributes.CssStyle.Add("width", (ucBoxListWidth - 4) + "px");
                txtSearch.Attributes.CssStyle.Add("margin", "0 0 2px 0");

                txtSearch.Attributes.Add("OnFocus", string.Format("Util_WaterMark_Focus('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
                txtSearch.Attributes.Add("OnBlur", string.Format("Util_WaterMark_Blur('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
                txtSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");  //預防按了 Enter 送出 PostBack
                txtSearch.Attributes.Add("onkeyup", string.Format("Util_ChkBoxListItemSearch('{0}','{1}','{2}');", idSourceBoxList.ClientID, idSourceBoxList.Items.Count, txtSearch.ClientID));
            }
        }
    }
}