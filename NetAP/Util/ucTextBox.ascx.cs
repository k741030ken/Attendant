using System;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [文數字輸入] 控制項
/// <para>提供前置抬頭、顯示切換、唯讀、輸入檢核(支援RegExp)、多行編輯、字數提示及限制。。。等擴充功能</para>
/// </summary>
public partial class Util_ucTextBox : BaseUserControl
{
    private bool _IsRequire = false;
    private bool _IsRegExp = false;
    private bool _IsClear = false;
    private string _ReadOnlyClass = "Util_clsReadOnly";
    private string _WaterMarkClass = "Util_WaterMarkedTextBox";
    private bool _IsWidthByPixel = true;
    private string _RequireErrMsg = "*";
    private string _RegExpErrMsg = "*";
    private int _Height = 20;
    private int _Width = 80;
    private int _MaxLength = 0;  //最大字數，若為 0 則不限制

    /// <summary>
    /// 輸入框文字內容轉移種類
    /// <para>**非密碼輸入時才有作用**</para>
    /// </summary>
    public Util.TextShiftKind ucTextShiftKind
    {
        //2017.01.23 新增
        get
        {
            if (PageViewState["_TextShiftKind"] == null)
            {
                PageViewState["_TextShiftKind"] = Util.TextShiftKind.None;
            }
            return (Util.TextShiftKind)(PageViewState["_TextShiftKind"]);
        }
        set
        {
            PageViewState["_TextShiftKind"] = value;
        }
    }

    /// <summary>
    /// 是否為密碼輸入(預設false)
    /// </summary>
    public bool ucIsPassword
    {
        get
        {
            if (PageViewState["_IsPassword"] == null)
            {
                PageViewState["_IsPassword"] = false;
            }
            return (bool)(PageViewState["_IsPassword"]);
        }
        set
        {
            PageViewState["_IsPassword"] = value;
            if (value == true)
            {
                txtData.TextMode = TextBoxMode.Password;
            }
        }
    }

    /// <summary>
    /// 是否可輸入中文(預設true)
    /// <remarks>**只適用於 IE **</remarks>
    /// </summary>
    public bool ucIsActiveIME
    {
        get
        {
            if (PageViewState["_IsActiveIME"] == null)
            {
                PageViewState["_IsActiveIME"] = true;
            }
            return (bool)(PageViewState["_IsActiveIME"]);
        }
        set
        {
            PageViewState["_IsActiveIME"] = value;
        }
    }

    /// <summary>
    /// 是否 ReadOnly
    /// </summary>
    public bool ucIsReadOnly
    {
        get
        {
            if (PageViewState["_IsReadOnly"] == null)
            {
                PageViewState["_IsReadOnly"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly"]);
        }
        set
        {
            PageViewState["_IsReadOnly"] = value;
        }
    }

    /// <summary>
    /// 一般狀態時的 CSSClass 名稱(預設：無)
    /// </summary>
    public string ucCssClass
    {
        get
        {
            if (PageViewState["_CssClass"] == null)
            {
                PageViewState["_CssClass"] = "";
            }
            return (string)(PageViewState["_CssClass"]);
        }
        set
        {
            PageViewState["_CssClass"] = value;
            txtData.CssClass = value;
        }
    }

    /// <summary>
    /// ReadOnly 時的 CSSClass 名稱(預設：[Util_clsReadOnly])
    /// </summary>
    public string ucReadOnlyCSS
    {
        get
        {
            if (PageViewState["_ReadOnlyCSS"] == null)
            {
                PageViewState["_ReadOnlyCSS"] = _ReadOnlyClass;
            }
            return (string)(PageViewState["_ReadOnlyCSS"]);
        }
        set
        {
            PageViewState["_ReadOnlyCSS"] = value;
        }
    }

    /// <summary>
    /// Watermark 時的 CSSClass 名稱(預設：[Util_WaterMarkedTextBox])
    /// </summary>
    public string ucWaterMarkCSS
    {
        get
        {
            if (PageViewState["_WaterMarkCSS"] == null)
            {
                PageViewState["_WaterMarkCSS"] = _WaterMarkClass;
            }
            return (string)(PageViewState["_WaterMarkCSS"]);
        }
        set
        {
            PageViewState["_WaterMarkCSS"] = value;
        }
    }

    /// <summary>
    /// 是否將輸入內容自動轉英文大寫
    /// <para>**非密碼輸入時才有作用**</para>
    /// </summary>
    public bool ucIsAutoToUpperCase
    {
        get
        {
            if (PageViewState["_IsAutoToUpperCase"] == null)
            {
                PageViewState["_IsAutoToUpperCase"] = false;
            }
            return (bool)(PageViewState["_IsAutoToUpperCase"]);
        }
        set
        {
            PageViewState["_IsAutoToUpperCase"] = value;
        }
    }

    /// <summary>
    /// 是否使用切換可見功能
    /// </summary>
    public bool ucIsToggleVisibility
    {
        get
        {
            if (PageViewState["_IsToggleVisibility"] == null)
            {
                PageViewState["_IsToggleVisibility"] = false;
            }
            return (bool)(PageViewState["_IsToggleVisibility"]);
        }
        set
        {
            PageViewState["_IsToggleVisibility"] = value;
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
        get
        {
            return (int)(((Unit)(labCaption.Width)).Value);
        }
        set
        {
            labCaption.Width = Unit.Pixel(value);
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
            labCaption.Style["text-align"] = ((HorizontalAlign)PageViewState["_CaptionHorizontalAlign"]).ToString().ToLower();
        }
    }

    /// <summary>
    /// 浮水印內容
    /// </summary>
    public string ucWaterMark
    {
        get
        {
            if (PageViewState["_WaterMark"] == null)
            {
                PageViewState["_WaterMark"] = "";
            }
            return (string)(PageViewState["_WaterMark"]);
        }
        set
        {
            PageViewState["_WaterMark"] = value;
        }
    }

    /// <summary>
    /// 輸入框文字內容
    /// </summary>
    public string ucTextData
    {
        get
        {
            if (!string.IsNullOrEmpty(ucWaterMark))
            {
                return txtData.Text.Replace(ucWaterMark, "");
            }
            else
            {
                return txtData.Text.Trim();
            }
        }
        set
        {
            txtData.Text = value;
        }
    }

    /// <summary>
    /// 輸入框Width計量單位是否為[像素]
    /// </summary>
    public bool ucIsWidthByPixel
    {
        get
        {
            if (PageViewState["_IsWidthByPixel"] == null)
            {
                PageViewState["_IsWidthByPixel"] = _IsWidthByPixel;
            }
            return (bool)(PageViewState["_IsWidthByPixel"]);
        }
        set
        {
            PageViewState["_IsWidthByPixel"] = value;
        }
    }

    /// <summary>
    /// 輸入框Width
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (PageViewState["_Width"] == null)
            {
                PageViewState["_Width"] = _Width;
                if (ucIsWidthByPixel)
                {
                    txtData.Width = Unit.Pixel(_Width);
                }
                else
                {
                    txtData.Width = Unit.Percentage(_Width);
                }
            }
            txtTemp.Width = txtData.Width;
            return (int)(PageViewState["_Width"]);
        }
        set
        {
            PageViewState["_Width"] = value;
            if (ucIsWidthByPixel)
            {
                txtData.Width = Unit.Pixel(value);
            }
            else
            {
                txtData.Width = Unit.Percentage(value);
            }
            txtTemp.Width = txtData.Width;
        }
    }

    /// <summary>
    /// 輸入框Height
    /// </summary>
    public int ucHeight
    {
        get
        {
            if (PageViewState["_Height"] == null)
            {
                PageViewState["_Height"] = _Height;
                if (ucIsWidthByPixel)
                {
                    txtData.Height = Unit.Pixel(_Height);
                }
                else
                {
                    txtData.Height = Unit.Percentage(_Height);
                }
            }
            txtTemp.Height = txtData.Height;
            return (int)(PageViewState["_Height"]);
        }
        set
        {
            PageViewState["_Height"] = value;
            if (ucIsWidthByPixel)
            {
                txtData.Height = Unit.Pixel(value);
            }
            else
            {
                txtData.Height = Unit.Percentage(value);
            }
            txtTemp.Height = txtData.Height;
        }
    }

    /// <summary>
    /// 輸入框 Rows
    /// </summary>
    public int ucRows
    {
        get
        {
            return int.Parse(txtData.Rows.ToString());
        }
        set
        {
            txtData.Rows = value;
            txtData.TextMode = (ucRows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
            //修正 IE 多行模式顯示 Bug 2016.09.06 / 2016.09.14
            if (ucRows > 1 && Util.getIEVersion() > 0)
                txtData.Style["white-space"] = "pre-wrap";
        }
    }

    /// <summary>
    /// 是否顯示目前輸入/最大字數
    /// </summary>
    public bool ucIsDispEnteredWords
    {
        get
        {
            if (PageViewState["_IsDispEnteredWords"] == null) PageViewState["_IsDispEnteredWords"] = false;
            return (bool)PageViewState["_IsDispEnteredWords"];
        }
        set
        {
            PageViewState["_IsDispEnteredWords"] = value;
        }
    }

    /// <summary>
    /// 設定用來顯示[目前輸入/最大字數]的物件(預設顯示於輸入框的後方)
    /// </summary>
    public string ucDispEnteredWordsObjClientID
    {
        get
        {
            if (PageViewState["_DispEnteredWordsObjClientID"] == null) PageViewState["_DispEnteredWordsObjClientID"] = "";
            return (string)PageViewState["_DispEnteredWordsObjClientID"];
        }

        set
        {
            PageViewState["_DispEnteredWordsObjClientID"] = value;
        }
    }


    /// <summary>
    /// 輸入框大字數限制
    /// <para>此參數可配合ucIsDispEnteredWords / ucDispEnteredWordsObjClientID 運作</para>
    /// <para></para>
    /// <para></para>
    /// </summary>
    public int ucMaxLength
    {
        get
        {
            if (PageViewState["_MaxLength"] == null) PageViewState["_MaxLength"] = _MaxLength;
            return (int)PageViewState["_MaxLength"];
        }
        set
        {
            PageViewState["_MaxLength"] = value;
        }
    }

    /// <summary>
    /// 當達到最大字數時，自動將焦點移至指定物件
    /// <para>ucMaxLength 需 ≧ 0 才有作用</para>
    /// </summary>
    public string ucMaxLengthNextFocusObjClientID
    {
        //2016.07.04 新增
        get
        {
            if (PageViewState["_MaxLengthNextFocusClientID"] == null)
            {
                PageViewState["_MaxLengthNextFocusClientID"] = "";
            }
            return (string)(PageViewState["_MaxLengthNextFocusClientID"]);
        }
        set
        {
            PageViewState["_MaxLengthNextFocusClientID"] = value;
        }
    }

    /// <summary>
    /// 驗證元件的正則運算式
    /// </summary>
    public string ucRegExp
    {
        get
        {
            return RegularExpressionValidator1.ValidationExpression;
        }
        set
        {
            RegularExpressionValidator1.ValidationExpression = value;
            if (!string.IsNullOrEmpty(value))
                ucIsRegExp = true;
            else
                ucIsRegExp = false;
        }
    }

    /// <summary>
    /// 是否需 RegExp 
    /// </summary>
    public bool ucIsRegExp
    {
        get
        {
            if (PageViewState["_IsRegExp"] == null)
            {
                PageViewState["_IsRegExp"] = _IsRegExp;
            }
            return (bool)(PageViewState["_IsRegExp"]);
        }
        set
        {
            PageViewState["_IsRegExp"] = value;
            RegularExpressionValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// 是否需 Require
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (PageViewState["_IsRequire"] == null)
            {
                PageViewState["_IsRequire"] = _IsRequire;
            }
            return (bool)(PageViewState["_IsRequire"]);
        }
        set
        {
            PageViewState["_IsRequire"] = value;
            txtData.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// 滑鼠[雙擊]時，是否清除現有內容 
    /// </summary>
    public bool ucIsClear
    {
        get
        {
            if (PageViewState["_IsClear"] == null)
            {
                PageViewState["_IsClear"] = _IsClear;
            }
            return (bool)(PageViewState["_IsClear"]);
        }
        set
        {
            PageViewState["_IsClear"] = value;
        }
    }

    /// <summary>
    /// 是否設為焦點(預設false)
    /// </summary>
    public bool ucIsFocus
    {
        get
        {
            if (PageViewState["_IsFocus"] == null)
            {
                PageViewState["_IsFocus"] = false;
            }
            return (bool)(PageViewState["_IsFocus"]);
        }
        set
        {
            PageViewState["_IsFocus"] = value;
            if (value)
            {
                txtData.Focus();
            }
        }
    }

    /// <summary>
    /// 當浮水印輸入框被設為焦點時是否清除浮水印文字(預設 true)
    /// </summary>
    public bool ucIsClearWaterMarkWhenFocus
    {
        get
        {
            if (PageViewState["_IsClearWaterMarkWhenFocus"] == null)
            {
                PageViewState["_IsClearWaterMarkWhenFocus"] = true;
            }
            return (bool)(PageViewState["_IsClearWaterMarkWhenFocus"]);
        }
        set
        {
            PageViewState["_IsClearWaterMarkWhenFocus"] = value;
        }
    }



    /// <summary>
    /// 自訂Require錯誤訊息
    /// </summary>
    public string ucRequireErrorMessage
    {
        get
        {
            if (PageViewState["_RequireErrMsg"] == null)
            {
                PageViewState["_RequireErrMsg"] = _RequireErrMsg;
            }
            return (string)(PageViewState["_RequireErrMsg"]);
        }
        set
        {
            PageViewState["_RequireErrMsg"] = value;
            RequiredFieldValidator1.ErrorMessage = value;
        }
    }

    /// <summary>
    /// 自訂RegExp錯誤訊息
    /// </summary>
    public string ucRegExpErrMsg
    {
        get
        {
            if (PageViewState["_RegExpErrMsg"] == null)
            {
                PageViewState["_RegExpErrMsg"] = _RegExpErrMsg;
            }
            return (string)(PageViewState["_RegExpErrMsg"]);
        }
        set
        {
            PageViewState["_RegExpErrMsg"] = value;
            RegularExpressionValidator1.ErrorMessage = value;
        }
    }

    /// <summary>
    /// 提示訊息
    /// </summary>
    public string ucToolTip
    {
        get
        {
            if (PageViewState["_ToolTip"] == null)
            {
                PageViewState["_ToolTip"] = "";
            }
            return (string)(PageViewState["_ToolTip"]);
        }
        set
        {
            PageViewState["_ToolTip"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ucIsPassword && Util.getIEVersion() >= 10)
        {
            Util.setJSContent("document.msCapsLockWarningOff = true;", "DisableCapsLock_Init");
        }
        Refresh();
    }

    public void Refresh()
    {
        labCapsLock.Text = RS.Resources.JS_Alert_CapsLock;  //[大寫鎖定]啟用中
        labCapsLock.Attributes.Add("style", "padding: 2px 10px 2px 10px; color:#000; background: #FFE8A6; font-weight: bold; border: 1px solid darkgray; white-space: nowrap; border-radius: 3px;");

        if (ucIsFocus)
        {
            txtData.Focus();
        }

        if (ucWidth > 0)
        {
            if (ucIsWidthByPixel)
            {
                txtData.Width = Unit.Pixel(ucWidth);
            }
            else
            {
                txtData.Width = Unit.Percentage(ucWidth);
            }
        }

        //chkVisibility 相關設定
        string strChkJS = "";
        if (ucIsRequire)
        {
            strChkJS += "var oValid = document.getElementById('" + RequiredFieldValidator1.ClientID + "');";
            strChkJS += "ValidatorEnable(oValid,this.checked);";
        }
        if (ucIsRegExp)
        {
            strChkJS += "var oValid = document.getElementById('" + RegularExpressionValidator1.ClientID + "');";
            strChkJS += "ValidatorEnable(oValid,this.checked);";
        }
        strChkJS += string.Format("Util_ChkBoxToggleVisibility('{0}', '{1}');", chkVisibility.ClientID, divDataArea.ClientID);
        chkVisibility.Attributes.Add("onclick", strChkJS);
        if (chkVisibility.Checked)
        {
            divDataArea.Style["visibility"] = "";
        }
        else
        {
            divDataArea.Style["visibility"] = "hidden";
        }

        if (ucIsClear)
        {
            txtData.Attributes["ondblclick"] = "if (confirm('" + SinoPac.WebExpress.Common.Properties.Resources.TextBox_ConfirmMsg + "')){this.value='';}";
        }

        string strKeyUpJS = "";
        string strBlurJS = "";

        //因為此控制項可能出現在Popup視窗，若使用 HoverTooltip會造成位置計算錯誤
        if (!string.IsNullOrEmpty(ucToolTip))
        {
            strKeyUpJS += "this.title = '" + ucToolTip + " ';"; //預留空白，以防後面要顯示輸入字數
        }
        else
        {
            strKeyUpJS += "this.title='';";
        }

        //調整 ucMaxLength / ucIsDispEnteredWords 判斷順序 2016.10.06
        if (ucMaxLength > 0)
        {
            //因為本控制項有可能是執行期才動態載入，故無法使用 Util.setJS_ChkMaxLength() 作檢核
            strKeyUpJS += "if (this.value.length >= " + ucMaxLength + " && this.value != '" + ucWaterMark + "'){ \n";
            strKeyUpJS += "  this.value = this.value.substring(0, " + ucMaxLength + "); \n";

            //移動焦點到指定物件 2016.07.04
            if (!string.IsNullOrEmpty(ucMaxLengthNextFocusObjClientID))
            {
                strKeyUpJS += "  oNext=document.getElementById('" + ucMaxLengthNextFocusObjClientID + "');if(oNext != null){oNext.focus();} \n";
            }

            strKeyUpJS += "};\n";
        }

        if (ucIsDispEnteredWords)
        {
            if (string.IsNullOrEmpty(ucDispEnteredWordsObjClientID))
            {
                //若未指定，則用內建 Label 顯示 2014.11.06
                labDataQty.Visible = true;
                strKeyUpJS += "var oTxt = document.getElementById('" + labDataQty.ClientID + "');if (oTxt != null){oTxt.innerText = String.format(JS_Msg_DispInputMaxWords, this.value.length, " + ucMaxLength + ");}";
            }
            else
            {
                //有指定顯示用物件
                strKeyUpJS += "var oTxt = document.getElementById('" + ucDispEnteredWordsObjClientID + "');if (oTxt != null){oTxt.innerText = String.format(JS_Msg_DispInputMaxWords, this.value.length, " + ucMaxLength + ");}";
            }
        }

        if (ucIsReadOnly)
        {
            //唯讀
            txtData.ReadOnly = true;
            txtData.CssClass = ucReadOnlyCSS;
        }
        else
        {
            //可編輯
            txtData.ReadOnly = false;
            txtData.CssClass = ucCssClass;

            if (!ucIsPassword)
            {
                //非密碼輸入

                //自動大寫
                if (ucIsAutoToUpperCase)
                {
                    Util.setJSContent("dom.Ready(function(){ document.getElementById('" + this.txtData.ClientID + "').value = document.getElementById('" + this.txtData.ClientID + "').value.toUpperCase(); });", this.ClientID + "_AutoToUpperCase_Init");
                    strBlurJS += "this.value = this.value.toUpperCase();";
                }

                //轉移種類 2017.01.23
                switch (ucTextShiftKind)
                {
                    case Util.TextShiftKind.Full:
                        //全形
                        Util.setJSContent("dom.Ready(function(){ Util_ToFull('" + this.txtData.ClientID + "'); });", this.ClientID + "_Shift_Init");
                        strBlurJS += "Util_ToFull('" + this.txtData.ClientID + "');";
                        break;
                    case Util.TextShiftKind.Half:
                        //半形
                        Util.setJSContent("dom.Ready(function(){ Util_ToHalf('" + this.txtData.ClientID + "');});", this.ClientID + "_Shift_Init");
                        strBlurJS += "Util_ToHalf('" + this.txtData.ClientID + "');";
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(strKeyUpJS))
            {
                //2014.10.25 從 onkeyup 改用 onpropertychange / oninput 事件，
                //2014.10.29 部份 IE 用 onpropertychange 會當掉，改回onkeyup
                txtData.Attributes.Add("onkeyup", strKeyUpJS);
            }
        }

        if (!string.IsNullOrEmpty(ucWaterMark))
        {
            //有浮水印
            if (ucIsPassword)
            {
                //密碼輸入
                txtData.Style["display"] = "none";
                txtData.CssClass = ucWaterMarkCSS;
                txtData.TextMode = TextBoxMode.Password;
                txtData.Attributes.Add("onblur", "document.getElementById('" + divPopPanel.ClientID + "').style.display='none';if (this.value==''){this.style.display='none';var oTxt=document.getElementById('" + txtTemp.ClientID + "');oTxt.style.display='';}");

                txtTemp.Style["display"] = "none";
                txtTemp.Visible = true;
                txtTemp.CssClass = ucWaterMarkCSS;
                txtTemp.Text = ucWaterMark;
                txtTemp.Style["display"] = "";
                txtTemp.Attributes.Add("onfocus", "this.style.display='none';var oTxt=document.getElementById('" + txtData.ClientID + "');oTxt.style.display='';oTxt.focus();");

                txtData.Attributes.Add("onkeypress", txtData.ClientID + "_isCapsLock(event)");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("function " + txtData.ClientID + "_isCapsLock(e){ "
                        + "  kc = e.keyCode ? e.keyCode : e.which; "
                        + "  sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);"
                        + "  if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk)) {"
                        + "      document.getElementById('" + divPopPanel.ClientID + "').style.display = ''; "
                        + "  } else { "
                        + "      document.getElementById('" + divPopPanel.ClientID + "').style.display = 'none'; "
                        + "  }"
                        + "}");
                Util.setJSContent(sb.ToString(), txtData.ClientID + "_isCapsLock");
            }
            else
            {
                //非密碼輸入
                if (!ucIsActiveIME)
                {
                    //關閉中文輸入法
                    txtData.Style["ime-mode"] = "disabled"; //for ie & firefox
                    //txtData.Attributes.Add("onkeyup", "this.value=this.value.replace(/[\u4e00-\u9fa5]/g,'');");  //for chrome
                }

                txtData.CssClass = ucWaterMarkCSS;
                if (string.IsNullOrEmpty(txtData.Text)) txtData.Text = ucWaterMark;
                if (ucIsClearWaterMarkWhenFocus)
                {
                    //onfocus 時自動清除浮水印
                    txtData.Attributes.Add("onfocus", "if (this.value==''){this.value = '" + ucWaterMark + "';}if (this.value=='" + ucWaterMark + "'){this.value = '';}");
                    txtData.Attributes.Add("onblur", "if (this.value==''){this.value = '" + ucWaterMark + "';}" + strBlurJS);
                }
                else
                {
                    //onfocus 時不清除浮水印 2015.06.16
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("if (this.value=='') {"
                        //若為空值，將游標移至最前面
                            + "  this.value = '" + ucWaterMark + "';"
                            + "  if(this.createTextRange) {"
                            + "     var range = elem.createTextRange();"
                            + "     range.move('character', 0);"
                            + "     range.select();"
                            + "   } else { "
                            + "     if(this.selectionStart) {"
                            + "       this.setSelectionRange(0, 0); "
                            + "     }"
                            + "   }"
                            + "}"
                        );
                    txtData.Attributes.Add("onfocus", sb.ToString());
                    txtData.Attributes.Add("onkeydown", "this.value = this.value.replace('" + ucWaterMark + "','');");
                    txtData.Attributes.Add("onblur", "if (this.value==''){this.value = '" + ucWaterMark + "';}" + strBlurJS);
                }
            }
        }
        else
        {
            //無浮水印
            if (ucIsPassword)
            {
                //密碼輸入
                //[大寫鎖定]處理 2015.07.24 
                txtData.TextMode = TextBoxMode.Password;
                txtData.Attributes.Add("onblur", "document.getElementById('" + divPopPanel.ClientID + "').style.display='none';");

                txtData.Attributes.Add("onkeypress", txtData.ClientID + "_isCapsLock(event)");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("function " + txtData.ClientID + "_isCapsLock(e){ "
                        + "  kc = e.keyCode ? e.keyCode : e.which; "
                        + "  sk = e.shiftKey ? e.shiftKey : ((kc == 16) ? true : false);"
                        + "  if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk)) {"
                        + "      document.getElementById('" + divPopPanel.ClientID + "').style.display = ''; "
                        + "  } else { "
                        + "      document.getElementById('" + divPopPanel.ClientID + "').style.display = 'none'; "
                        + "  }"
                        + "}");
                Util.setJSContent(sb.ToString(), txtData.ClientID + "_isCapsLock");
            }
            else
            {
                //非密碼輸入
                if (!ucIsActiveIME)
                {
                    //關閉中文輸入法
                    txtData.Style["ime-mode"] = "disabled"; //for ie & firefox
                    //txtData.Attributes.Add("onkeyup", "this.value=this.value.replace(/[\u4e00-\u9fa5]/g,'');");  //for chrome
                }

                if (!string.IsNullOrEmpty(strBlurJS))
                {
                    txtData.Attributes.Add("onblur", strBlurJS);
                }
            }
        }

    }

}