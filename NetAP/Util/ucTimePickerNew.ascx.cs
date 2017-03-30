using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
///  [時間選取] 控制項
/// </summary>
public partial class Util_ucTimePickerNew : System.Web.UI.UserControl
{
    //private bool _IsRequire = false;
    //private string _ErrorMessage = "*";
    private string _ReadOnlyClass = "Util_clsReadOnly";

    private string[] _HHList = "請選擇,00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23".Split(',');
    private string[] _MMList = "請選擇,00,01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59".Split(',');
    private string[] _SSList = "00,30".Split(',');

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
    /// 是否要選[秒] (預設false)
    /// </summary>
    public bool ucIsSSEnabled
    {
        get
        {
            if (ViewState["_IsSSEnabled"] == null)
            {
                ViewState["_IsSSEnabled"] = false;
            }
            return (bool)(ViewState["_IsSSEnabled"]);
        }
        set
        {
            ViewState["_IsSSEnabled"] = value;
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
    /// 控制項顯示抬頭寬度(預設 80px)
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
    /// 預設選取HH
    /// </summary>
    public string ucDefaultSelectedHH
    {
        get
        {
            return (string)ddlHH.SelectedValue;
        }
        set
        {
            ddlHH.SelectedValue = value;
        }
    }

    /// <summary>
    /// 預設選取MM
    /// </summary>
    public string ucDefaultSelectedMM
    {
        get
        {
            return (string)ddlMM.SelectedValue;
        }
        set
        {
            ddlMM.SelectedValue = value;
        }
    }

    /// <summary>
    /// 預設選取SS
    /// </summary>
    public string ucDefaultSelectedSS
    {
        get
        {
            return (string)ddlSS.SelectedValue;
        }
        set
        {
            ddlSS.SelectedValue = value;
        }
    }


    /// <summary>
    /// 選取結果HH
    /// </summary>
    public string ucSelectedTime
    {
        get
        {
            string sResult = string.Format("{0}:{1}", ddlHH.SelectedValue, ddlMM.SelectedValue);
            if (ucIsSSEnabled)
            {
                sResult += string.Format(":{0}", ddlSS.SelectedValue);
            }
            return (string)sResult;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }

    public void Refresh()
    {
        //chkVisibility 相關設定
        string tmpValue;

        tmpValue = ddlHH.SelectedValue;
        ddlHH.DataSource = _HHList;
        ddlHH.DataBind();
        ddlHH.SelectedValue = tmpValue;
        if (string.IsNullOrEmpty(tmpValue))
        {
            ddlHH.SelectedValue = ucDefaultSelectedHH;
        }

        tmpValue = ddlMM.SelectedValue;
        ddlMM.DataSource = _MMList;
        ddlMM.DataBind();
        ddlMM.SelectedValue = tmpValue;
        if (string.IsNullOrEmpty(tmpValue))
        {
            ddlMM.SelectedValue = ucDefaultSelectedMM;
        }


        litSS.Visible = ucIsSSEnabled;
        ddlSS.Visible = ucIsSSEnabled;
        if (ddlSS.Visible)
        {
            tmpValue = ddlSS.SelectedValue;
            ddlSS.DataSource = _SSList;
            ddlSS.DataBind();
            ddlSS.SelectedValue = tmpValue;
            if (string.IsNullOrEmpty(tmpValue))
            {
                ddlSS.SelectedValue = ucDefaultSelectedSS;
            }
        }

        string strChkJS = "";
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


        if (ucIsReadOnly)
        {
            ddlHH.Enabled = false;
            ddlMM.Enabled = false;
            ddlSS.Enabled = false;

            if (!string.IsNullOrEmpty(ucReadOnlyCSS))
            {
                ddlHH.CssClass = ucReadOnlyCSS;
                ddlMM.CssClass = ucReadOnlyCSS;
                ddlSS.CssClass = ucReadOnlyCSS;
            }
        }

    }
}