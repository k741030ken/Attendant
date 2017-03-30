using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;


/// <summary>
/// [日期選取] 控制項
/// </summary>
[ValidationProperty("ucSelectedDate")]
public partial class Util_ucDatePicker : BaseUserControl
{
    private String _DateFormat = "yyyy/MM/dd";
    private int _SelectYearRange = 3;
    private DateTime _StartDate = new DateTime(DateTime.Today.Year - 3, 1, 1); //起始日預設為往前 _SelectYearRange 年的 01月01日
    private DateTime _EndDate = new DateTime(DateTime.Today.Year + 3, 12, 31); //終止日預設為往後 _SelectYearRange 年的 12月31日
    private bool _IsRequire = false;
    private string _ToolTip = SinoPac.WebExpress.Common.Properties.Resources.DatePicker_ToolTip;
    private string _ErrorMessage = "*";
    private string _ReadOnlyClass = "Util_clsReadOnly";

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
    /// ReadOnly 時的 CSSClass 名稱
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
    /// 選取的日期(唯讀)
    /// </summary>
    public string ucSelectedDate { get { return txtDate.Text.Trim(); } set { txtDate.Text = value; } }

    /// <summary>
    /// 輸入框寬度
    /// </summary>
    public int ucWidth { get { return int.Parse(txtDate.Width.ToString()); } set { txtDate.Width = Unit.Pixel(value); } }

    /// <summary>
    /// 日期格式
    /// </summary>
    public string ucDateFormat
    {
        get
        {
            if (ViewState["_DateFormat"] == null)
            {
                ViewState["_DateFormat"] = _DateFormat;
            }
            return (string)(ViewState["_DateFormat"]);
        }
        set
        {
            ViewState["_DateFormat"] = value;
        }
    }

    /// <summary>
    /// 可選取日期的年份範圍
    /// <para>若有設定 <paramref name="ucDefaultSelectedDate"/> 則以該日期 往前/後 推展 <paramref name="ucSelectYearRange"/> 年的 01月01日當作 <paramref name="ucStartDate"/> ，12月31日則當作 <paramref name="ucEndDate"/></para>
    /// <para>若未設定 <paramref name="ucDefaultSelectedDate"/> 則以當天日期 往前/後 推展 <paramref name="ucSelectYearRange"/> 年的 01月01日當作 <paramref name="ucStartDate"/> ，12月31日則當作 <paramref name="ucEndDate"/></para>
    /// </summary>
    public int ucSelectYearRange
    {
        get
        {
            if (ViewState["_SelectYearRange"] == null)
            {
                ViewState["_SelectYearRange"] = _SelectYearRange;
            }
            return (int)(ViewState["_SelectYearRange"]);
        }
        set
        {
            ViewState["_SelectYearRange"] = value;
            //2016.10.26 強化判斷邏輯
            if (CalendarExtender1.SelectedDate != null && CalendarExtender1.SelectedDate.Value != null)
            {
                //若有預設選取日期，則以該日期進行計算
                ucStartDate = new DateTime(CalendarExtender1.SelectedDate.Value.Year - ucSelectYearRange, 1, 1);
                ucEndDate = new DateTime(CalendarExtender1.SelectedDate.Value.Year + ucSelectYearRange, 12, 31);
            }
            else
            {
                //若無預設選取日期，以當天日期進行計算
                ucStartDate = new DateTime(DateTime.Today.Year - ucSelectYearRange, 1, 1);
                ucEndDate = new DateTime(DateTime.Today.Year + ucSelectYearRange, 12, 31);
            }
        }
    }

    /// <summary>
    /// 預設選取日期
    /// </summary>
    public DateTime ucDefaultSelectedDate
    {
        get
        {
            return (DateTime)CalendarExtender1.SelectedDate;
        }
        set
        {
            CalendarExtender1.SelectedDate = value;
            ucStartDate = new DateTime(value.Year - ucSelectYearRange, 1, 1);
            ucEndDate = new DateTime(value.Year + ucSelectYearRange, 12, 31);
        }
    }

    /// <summary>
    /// 可選取的起始日期
    /// </summary>
    public DateTime ucStartDate
    {
        get
        {
            if (ViewState["_StartDate"] == null)
            {
                ViewState["_StartDate"] = _StartDate;
            }
            return (DateTime)(ViewState["_StartDate"]);
        }
        set
        {
            ViewState["_StartDate"] = value;
            CalendarExtender1.StartDate = value;
        }
    }

    /// <summary>
    /// 可選取的終止日期
    /// </summary>
    public DateTime ucEndDate
    {
        get
        {
            if (ViewState["_EndDate"] == null)
            {
                ViewState["_EndDate"] = _EndDate;
            }
            return (DateTime)(ViewState["_EndDate"]);
        }
        set
        {
            ViewState["_EndDate"] = value;
            CalendarExtender1.EndDate = value;
        }
    }

    /// <summary>
    /// 是否 Require
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
            txtDate.CausesValidation = value;
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
    /// 自訂錯誤訊息
    /// </summary>
    public string ucErrorMessage
    {
        get
        {
            if (ViewState["_ErrorMessage"] == null)
            {
                ViewState["_ErrorMessage"] = _ErrorMessage;
            }
            return (string)(ViewState["_ErrorMessage"]);
        }
        set
        {
            ViewState["_ErrorMessage"] = value;
            RequiredFieldValidator1.ErrorMessage = value;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }

    public void Refresh()
    {
        //chkVisibility 相關設定
        string strChkJS = "";
        if (ucIsRequire)
        {
            strChkJS += "var oValid = document.getElementById('" + RequiredFieldValidator1.ClientID + "');";
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


        txtDate.Attributes.Add("ReadOnly", "true");
        CalendarExtender1.Format = ucDateFormat;
        CalendarExtender1.StartDate = ucStartDate;
        CalendarExtender1.EndDate = ucEndDate;
        //預設為 Days 模式
        CalendarExtender1.DefaultView = AjaxControlToolkit.CalendarDefaultView.Days;

        //非 Days 模式需靠自訂 JS 進行控制 2017.01.06
        if (!CalendarExtender1.Format.ToUpper().Contains("DD"))
        {
            CalendarExtender1.BehaviorID = this.ClientID;
            CalendarExtender1.OnClientShown = "Show_" + CalendarExtender1.BehaviorID;
            CalendarExtender1.OnClientHidden = "Hide_" + CalendarExtender1.BehaviorID;

            // Years/Months 模式JS
            string strCalendarJS = @"
                    function Show_{0}() {
                       var cal = $find('{0}');
                       cal._switchMode('{1}', true);
                       if (cal._yearsBody) {
                          for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                             var row = cal._yearsBody.rows[i];
                             for (var j = 0; j < row.cells.length; j++) { Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, 'click', call_{0}); }
                          }
                       }
                       if (cal._monthsBody) {
                          for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                             var row = cal._monthsBody.rows[i];
                             for (var j = 0; j < row.cells.length; j++) { Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, 'click', call_{0}); }
                          }
                       }
                    }

                    function Hide_{0}() {
                      var cal = $find('{0}');
                      if (cal._yearsBody) {
                         for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                            var row = cal._yearsBody.rows[i];
                            for (var j = 0; j < row.cells.length; j++) { Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, 'click', call_{0}); }
                         }
                      }
                      if (cal._monthsBody) {
                         for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                             var row = cal._monthsBody.rows[i];
                             for (var j = 0; j < row.cells.length; j++) { Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, 'click', call_{0}); }
                         }
                      }
                    }

                    function call_{0}(eventElement) {
                       var target = eventElement.target;
                       var cal = $find('{0}');
                       switch (target.mode) {
                           case 'year':
                             cal.set_selectedDate(target.date);
                             cal._blur.post(true);
                             cal.raiseDateSelectionChanged(); 
		                     break;
                           case 'month':
                             cal._visibleDate = target.date;
                             cal.set_selectedDate(target.date);
                             //cal._switchMonth(target.date);
                             cal._blur.post(true);
                             cal.raiseDateSelectionChanged();
                             break;	   
                      }
                    }
                    ";

            if (CalendarExtender1.Format.ToUpper().Contains("MM"))
            {
                //Month 模式
                strCalendarJS = strCalendarJS.Replace("{0}", CalendarExtender1.BehaviorID);
                strCalendarJS = strCalendarJS.Replace("{1}", "months");
            }
            else
            {
                //Year 模式
                strCalendarJS = strCalendarJS.Replace("{0}", CalendarExtender1.BehaviorID);
                strCalendarJS = strCalendarJS.Replace("{1}", "years");
            }

            Util.setJSContent(strCalendarJS, this.ClientID + "_Init");
        }


        if (ucIsReadOnly)
        {
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                if (CalendarExtender1.SelectedDate != null)
                {
                    txtDate.Text = string.Format("{0:" + ucDateFormat + "}", CalendarExtender1.SelectedDate);
                }
            }
            CalendarExtender1.Enabled = false;
            if (!string.IsNullOrEmpty(ucReadOnlyCSS))
            {
                txtDate.CssClass = ucReadOnlyCSS;
            }
        }
        else
        {
            txtDate.ToolTip = ucToolTip; //控制項可能出現在Popup視窗，此處若使用 HoverTooltip 會造成位置計算錯誤
            txtDate.Attributes.Add("ondblclick", "this.value='';");
        }
    }
}