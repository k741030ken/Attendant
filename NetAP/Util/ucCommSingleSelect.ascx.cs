using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [常用單選下拉選單]控制項
/// </summary>
public partial class Util_ucCommSingleSelect : BaseUserControl
{
    /// <summary>
    /// 資料來源
    /// </summary>
    public Dictionary<string, string> ucSourceDictionary
    {
        get
        {
            if (ViewState["_dtSource"] == null)
                ViewState["_dtSource"] = new Dictionary<string, string>();
            return (Dictionary<string, string>)ViewState["_dtSource"];
        }
        set
        {
            ViewState["_dtSource"] = value;
        }
    }

    /// <summary>
    /// 選擇結果值
    /// </summary>
    public string ucSelectedID
    {
        get { return idSelectedID.Text; }
        set { idSelectedID.Text = value; }
    }

    /// <summary>
    /// 選擇結果文字
    /// </summary>
    public string ucSelectedInfo
    {
        get
        {
            if (!string.IsNullOrEmpty(idSelectedID.Text))
            {
                return ((Dictionary<string, string>)ViewState["_dtSource"])[idSelectedID.Text];
            }
            else
            {
                return ucDropDownSourceListEmptyLabel;
            }
        }
    }

    /// <summary>
    /// 選擇結果
    /// </summary>
    public Dictionary<string, string> ucSelectedDictionary
    {
        get
        {
            Dictionary<string, string> oDic = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(idSelectedID.Text))
                oDic.Add(ucSelectedID, ucSelectedInfo);

            return oDic;
        }
    }

    /// <summary>
    /// 選擇結果 輸出到網頁父階指定物件 ID
    /// </summary>
    public string ucSelectedIDToParentObjClientID
    {
        get
        {
            if (ViewState["_SelectedIDToParentObjClientID"] == null)
            {
                ViewState["_SelectedIDToParentObjClientID"] = "";
            }
            return (string)(ViewState["_SelectedIDToParentObjClientID"]);
        }
        set
        {
            ViewState["_SelectedIDToParentObjClientID"] = value;
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
                ViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommSingleSelect_WaterMarkText; //"請輸入搜尋文字";
            }
            return (string)(ViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            ViewState["_SearchBoxWaterMarkText"] = value;
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
    /// 下拉選單空白項目顯示內容
    /// </summary>
    public string ucDropDownSourceListEmptyLabel
    {
        get
        {
            if (ViewState["_DropDownSourceListEmptyLabel"] == null)
            {
                ViewState["_DropDownSourceListEmptyLabel"] = RS.Resources.Msg_DDL_EmptyItem; //"--請選擇--";;
            }
            return (string)(ViewState["_DropDownSourceListEmptyLabel"]);
        }
        set
        {
            ViewState["_DropDownSourceListEmptyLabel"] = value;
        }
    }

    /// <summary>
    /// 下拉選單寬度(預設 200)
    /// </summary>
    public int ucDropDownSourceListWidth
    {
        get
        {
            if (ViewState["_DropDownSourceListWidth"] == null)
            {
                ViewState["_DropDownSourceListWidth"] = 200;
            }
            return (int)(ViewState["_DropDownSourceListWidth"]);
        }
        set
        {
            ViewState["_DropDownSourceListWidth"] = value;
        }
    }

    ///// <summary>
    ///// 控制項寬度
    ///// </summary>
    //[Description("控制項寬度"), Category("自訂屬性"), Browsable(true)]
    //public int ucWidth
    //{
    //    get { return _Width; }
    //    set { _Width = value; }
    //}

    ///// <summary>
    ///// 字體大小
    ///// </summary>
    //[Description("字體大小"), Category("自訂屬性"), Browsable(true)]
    //public int ucFontSize
    //{
    //    get { return _FontSize; }
    //    set { _FontSize = value; }
    //}

    ///// <summary>
    ///// 字體名稱
    ///// </summary>
    //[Description("字體名稱)"), Category("自訂屬性"), Browsable(true)]
    //public string ucFontName
    //{
    //    get { return _FontName; }
    //    set { _FontName = value; }
    //}

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

    ///// <summary>
    ///// 一般狀態時的 CSSClass 名稱(預設 空白)
    ///// </summary>
    //public string ucCssClass
    //{
    //    get
    //    {
    //        if (ViewState["_CssClass"] == null)
    //        {
    //            ViewState["_CssClass"] = "";
    //        }
    //        return (string)(ViewState["_CssClass"]);
    //    }
    //    set
    //    {
    //        ViewState["_CssClass"] = value;
    //    }
    //}

    /// <summary>
    /// ReadOnly 時的 CSSClass 名稱
    /// </summary>
    public string ucReadOnlyCSS
    {
        get
        {
            if (ViewState["_ReadOnlyCSS"] == null)
            {
                ViewState["_ReadOnlyCSS"] = "Util_clsReadOnly";
            }
            return (string)(ViewState["_ReadOnlyCSS"]);
        }
        set
        {
            ViewState["_ReadOnlyCSS"] = value;
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
    /// 是否需 Require
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
            ddlSourceList.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
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
            return Convert.ToInt16(labCaption.Width.Value);
        }
        set
        {
            labCaption.Width = Unit.Pixel(value);
        }
    }

    /// <summary>
    /// 當PostBack後，控制項是否自動重新整理(預設 true)
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
    /// 重新整理
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

        labCaption.Width = ucCaptionWidth;
        labCaption.Text = ucCaption;
        labCaption.Visible = (string.IsNullOrEmpty(labCaption.Text)) ? false : true;

        if (ucIsSearchEnabled)
        {
            txtSearch.Style["display"] = "";
            //從Util.js複製過來，產生對應的專用JS，解決同一頁多個相同控制項問題 2016.03.29
            string strFilterJS = @"

                // === [{Ctrl}] 專用JS [Begin]===
                var {Ctrl}ddl_already_init = false;
                var {Ctrl}ddl_NameSearch_value = '';
                var {Ctrl}ddl_ct_name = [];
                var {Ctrl}ddl_ct_val = [];

                function {Ctrl}SearchDropDownListItem(SearchTextID, DropDownListID) {
                    var myvalue = document.getElementById(SearchTextID).value;
                    if ({Ctrl}ddl_NameSearch_value == myvalue)
                        return false;
                    else
                        {Ctrl}ddl_NameSearch_value = myvalue;

                    var RegExp_NameSearch = new RegExp(myvalue, 'i');
                    var Name_DropDownList = document.getElementById(DropDownListID);
                    if (Name_DropDownList == null) {
                        return false;
                    }

                    {Ctrl}Dropdown_arrayInit(Name_DropDownList); //初始化陣列...
                    {Ctrl}RemoveAllOptions(Name_DropDownList); //清除選單...

                    //因Server端會固定在[0]產生一個空項目，該項目需固定保留
                    {Ctrl}AddOption(Name_DropDownList, {Ctrl}ddl_ct_name[0], {Ctrl}ddl_ct_val[0]);
                    //其餘項目則需內容符合才能建成選單項目
                    for (i = 1; i < {Ctrl}ddl_ct_name.length; i++) {
                        if (RegExp_NameSearch.test({Ctrl}ddl_ct_name[i])) //只列出符合條件的...
                        {
                            {Ctrl}AddOption(Name_DropDownList, {Ctrl}ddl_ct_name[i], {Ctrl}ddl_ct_val[i]);
                        }
                    }

                    Name_DropDownList.options[0].selected = true;
                }


                function {Ctrl}Dropdown_arrayInit(DDL_obj) {
                    if (!{Ctrl}ddl_already_init) {
                        for (var i = 0; i < DDL_obj.options.length; i++) {
                            {Ctrl}ddl_ct_name[i] = DDL_obj.options[i].text;
                            {Ctrl}ddl_ct_val[i] = DDL_obj.options[i].value;
                        }

                        {Ctrl}ddl_already_init = true;
                    }
                }

                //清除所有清單選項
                function {Ctrl}RemoveAllOptions(objList) {
                    if (objList != null) {
                        objList.options.length = 0;
                    }
                }

                //增加清單選項
                function {Ctrl}AddOption(objList, text, value) {
                    if (objList != null) {
                        var optn = document.createElement('OPTION');
                        optn.text = text;
                        optn.value = value;
                        objList.options.add(optn);
                    }
                } 
                // === [{Ctrl}] 專用JS [End]===

            ";
            strFilterJS = strFilterJS.Replace("{Ctrl}", this.ClientID + "_");
            Util.setJSContent(strFilterJS, this.ClientID + "_FilterJS_Init");
        }
        else
        {
            txtSearch.Style["display"] = "none";
        }

        txtSearch.Width = ucSearchBoxWidth;
        ddlSourceList.Width = ucDropDownSourceListWidth;
        ddlSourceList.Items.Clear();
        ddlSourceList.DataSource = ucSourceDictionary;
        ddlSourceList.DataValueField = "Key";
        ddlSourceList.DataTextField = "Value";
        ddlSourceList.CausesValidation = ucIsRequire;
        ddlSourceList.Enabled = true;
        if (Util.getIEVersion() > 8)
        {
            ddlSourceList.Style["padding-bottom"] = "1px"; //IE CSS fix 2017.02.09
        }
        ddlSourceList.SelectedValue = null;     //2016.05.30 加入重複使用時的狀態修正
        if (ucSourceDictionary != null && ucSourceDictionary.Count > 0)
        {
            //2016.06.06 加入判斷
            ddlSourceList.DataBind();
        }
        ddlSourceList.Items.Insert(0, new ListItem(ucDropDownSourceListEmptyLabel, ""));  //固定保留一個空白項目，方便查詢跟非必要輸入得情境

        if (string.IsNullOrEmpty(ucSelectedID))
        {
            ddlSourceList.SelectedIndex = 0;
            idSelectedID.Text = ddlSourceList.SelectedValue;
        }
        else
        {
            ddlSourceList.SelectedValue = ucSelectedID;
        }

        if (ucIsReadOnly)
        {
            //IE無法設定 [Enabled = false] 時的CSS，改用別的顯示邏輯 2015.06.23
            if (Util.getIEVersion() > 0 || HttpContext.Current.Request.UserAgent.Contains("Trident"))
            {
                //for IE
                var tmpItem = new ListItem(ucSelectedInfo, ucSelectedID);
                ddlSourceList.Items.Clear();
                ddlSourceList.Items.Add(tmpItem);
            }
            else
            {
                //non IE
                ddlSourceList.Enabled = false;
            }

            if (!string.IsNullOrEmpty(ucReadOnlyCSS))
            {
                ddlSourceList.CssClass = ucReadOnlyCSS;
            }
        }

        txtSearch.Text = ucSearchBoxWaterMarkText;
        txtSearch.CssClass = "Util_WaterMarkedTextBox";
        txtSearch.Attributes.Add("OnFocus", string.Format("Util_WaterMark_Focus('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
        txtSearch.Attributes.Add("OnBlur", string.Format("Util_WaterMark_Blur('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
        txtSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");  //預防按了 Enter 送出 PostBack

        //根據是否有指定Client端父階物件設定動作模式
        if (string.IsNullOrEmpty(ucSelectedIDToParentObjClientID))
        {
            //若未指定Client端父階物件
            txtSearch.Attributes.Add("onkeyup", string.Format(this.ClientID + "_SearchDropDownListItem('{0}','{1}');document.getElementById('{2}').value = document.getElementById('{1}').value;", txtSearch.ClientID, ddlSourceList.ClientID, idSelectedID.ClientID));
            //輸出到本頁物件，並以 [ucSelectedID]屬性傳回
            ddlSourceList.Attributes.Add("onchange", string.Format("document.getElementById('{1}').value = document.getElementById('{0}').value;", ddlSourceList.ClientID, idSelectedID.ClientID));
        }
        else
        {
            //若有指定Client端父階物件
            txtSearch.Attributes.Add("onkeyup", string.Format(this.ClientID + "_SearchDropDownListItem('{0}','{1}');window.parent.document.getElementById('{2}').value = document.getElementById('{1}').value;", txtSearch.ClientID, ddlSourceList.ClientID, ucSelectedIDToParentObjClientID));
            //輸出到Client端物件，[ucSelectedID]屬性則不處理
            ddlSourceList.Attributes.Add("onchange", string.Format("window.parent.document.getElementById('{1}').value = document.getElementById('{0}').value;", ddlSourceList.ClientID, ucSelectedIDToParentObjClientID));
        }
    }
}