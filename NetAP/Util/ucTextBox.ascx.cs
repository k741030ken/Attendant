using System;
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
            if (ViewState["_TextShiftKind"] == null)
            {
                ViewState["_TextShiftKind"] = Util.TextShiftKind.None;
            }
            return (Util.TextShiftKind)(ViewState["_TextShiftKind"]);
        }
        set
        {
            ViewState["_TextShiftKind"] = value;
        }
    }

    /// <summary>
    /// 是否為密碼輸入(預設false)
    /// </summary>
    public bool ucIsPassword
    {
        get
        {
            if (ViewState["_IsPassword"] == null)
            {
                ViewState["_IsPassword"] = false;
            }
            return (bool)(ViewState["_IsPassword"]);
        }
        set
        {
            ViewState["_IsPassword"] = value;
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
            if (ViewState["_IsActiveIME"] == null)
            {
                ViewState["_IsActiveIME"] = true;
            }
            return (bool)(ViewState["_IsActiveIME"]);
        }
        set
        {
            ViewState["_IsActiveIME"] = value;
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
    /// 是否將輸入內容自動轉英文大寫
    /// <para>**非密碼輸入時才有作用**</para>
    /// </summary>
    public bool ucIsAutoToUpperCase
    {
        get
        {
            if (ViewState["_IsAutoToUpperCase"] == null)
            {
                ViewState["_IsAutoToUpperCase"] = false;
            }
            return (bool)(ViewState["_IsAutoToUpperCase"]);
        }
        set
        {
            ViewState["_IsAutoToUpperCase"] = value;
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
    /// 浮水印內容
    /// </summary>
    public string ucWaterMark
    {
        get
        {
            if (ViewState["_WaterMark"] == null)
            {
                ViewState["_WaterMark"] = "";
            }
            return (string)(ViewState["_WaterMark"]);
        }
        set
        {
            ViewState["_WaterMark"] = value;
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
            if (ViewState["_IsWidthByPixel"] == null)
            {
                ViewState["_IsWidthByPixel"] = _IsWidthByPixel;
            }
            return (bool)(ViewState["_IsWidthByPixel"]);
        }
        set
        {
            ViewState["_IsWidthByPixel"] = value;
        }
    }

    /// <summary>
    /// 輸入框Width
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null)
            {
                ViewState["_Width"] = _Width;
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
            return (int)(ViewState["_Width"]);
        }
        set
        {
            ViewState["_Width"] = value;
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
            if (ViewState["_Height"] == null)
            {
                ViewState["_Height"] = _Height;
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
            return (int)(ViewState["_Height"]);
        }
        set
        {
            ViewState["_Height"] = value;
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
            txtData.TextMode = (value > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
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
            if (ViewState["_IsDispEnteredWords"] == null) ViewState["_IsDispEnteredWords"] = false;
            return (bool)ViewState["_IsDispEnteredWords"];
        }
        set
        {
            ViewState["_IsDispEnteredWords"] = value;
        }
    }

    /// <summary>
    /// 設定用來顯示[目前輸入/最大字數]的物件(預設顯示於輸入框的後方)
    /// </summary>
    public string ucDispEnteredWordsObjClientID
    {
        get
        {
            if (ViewState["_DispEnteredWordsObjClientID"] == null) ViewState["_DispEnteredWordsObjClientID"] = "";
            return (string)ViewState["_DispEnteredWordsObjClientID"];
        }

        set
        {
            ViewState["_DispEnteredWordsObjClientID"] = value;
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
            if (ViewState["_MaxLength"] == null) ViewState["_MaxLength"] = _MaxLength;
            return (int)ViewState["_MaxLength"];
        }
        set
        {
            ViewState["_MaxLength"] = value;
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
            if (ViewState["_MaxLengthNextFocusClientID"] == null)
            {
                ViewState["_MaxLengthNextFocusClientID"] = "";
            }
            return (string)(ViewState["_MaxLengthNextFocusClientID"]);
        }
        set
        {
            ViewState["_MaxLengthNextFocusClientID"] = value;
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
            if (ViewState["_IsRegExp"] == null)
            {
                ViewState["_IsRegExp"] = _IsRegExp;
            }
            return (bool)(ViewState["_IsRegExp"]);
        }
        set
        {
            ViewState["_IsRegExp"] = value;
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
            if (ViewState["_IsRequire"] == null)
            {
                ViewState["_IsRequire"] = _IsRequire;
            }
            return (bool)(ViewState["_IsRequire"]);
        }
        set
        {
            ViewState["_IsRequire"] = value;
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
            if (ViewState["_IsClear"] == null)
            {
                ViewState["_IsClear"] = _IsClear;
            }
            return (bool)(ViewState["_IsClear"]);
        }
        set
        {
            ViewState["_IsClear"] = value;
        }
    }

    /// <summary>
    /// 是否設為焦點(預設false)
    /// </summary>
    public bool ucIsFocus
    {
        get
        {
            if (ViewState["_IsFocus"] == null)
            {
                ViewState["_IsFocus"] = false;
            }
            return (bool)(ViewState["_IsFocus"]);
        }
        set
        {
            ViewState["_IsFocus"] = value;
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
            if (ViewState["_IsClearWaterMarkWhenFocus"] == null)
            {
                ViewState["_IsClearWaterMarkWhenFocus"] = true;
            }
            return (bool)(ViewState["_IsClearWaterMarkWhenFocus"]);
        }
        set
        {
            ViewState["_IsClearWaterMarkWhenFocus"] = value;
        }
    }



    /// <summary>
    /// 自訂Require錯誤訊息
    /// </summary>
    public string ucRequireErrorMessage
    {
        get
        {
            if (ViewState["_RequireErrMsg"] == null)
            {
                ViewState["_RequireErrMsg"] = _RequireErrMsg;
            }
            return (string)(ViewState["_RequireErrMsg"]);
        }
        set
        {
            ViewState["_RequireErrMsg"] = value;
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
            if (ViewState["_RegExpErrMsg"] == null)
            {
                ViewState["_RegExpErrMsg"] = _RegExpErrMsg;
            }
            return (string)(ViewState["_RegExpErrMsg"]);
        }
        set
        {
            ViewState["_RegExpErrMsg"] = value;
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
            if (ViewState["_ToolTip"] == null)
            {
                ViewState["_ToolTip"] = "";
            }
            return (string)(ViewState["_ToolTip"]);
        }
        set
        {
            ViewState["_ToolTip"] = value;
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