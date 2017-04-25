using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

/// <summary>
/// 全選/全取消[ucParentControlID]下所有可見的自訂編輯控制項(Util_ucXXX)核取方塊
/// </summary>
public partial class Util_ucToggleControlVisibility : BaseUserControl
{

    /// <summary>
    /// 父階控制項ID
    /// </summary>
    public string ucParentControlID
    {
        get
        {
            if (PageViewState["_ParentControlID"] == null)
            {
                PageViewState["_ParentControlID"] = "";
            }
            return (string)(PageViewState["_ParentControlID"]);
        }
        set
        {
            PageViewState["_ParentControlID"] = value;
        }
    }

    /// <summary>
    /// 核取方塊提示訊息
    /// </summary>
    public string ucToggleVisibilityToolTip
    {
        get
        {
            if (PageViewState["_ToggleVisibilityToolTip"] == null)
            {
                PageViewState["_ToggleVisibilityToolTip"] = SinoPac.WebExpress.Common.Properties.Resources.ToggleControlVisibility_ToolTip;
            }
            return (string)(PageViewState["_ToggleVisibilityToolTip"]);
        }
        set
        {
            PageViewState["_ToggleVisibilityToolTip"] = value;
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
            return int.Parse(labCaption.Width.ToString());
        }
        set
        {
            labCaption.Width = Unit.Pixel(value);
        }
    }

    protected void Page_Load(object sender, EventArgs e) { }

    public void Refresh()
    {
        if (!string.IsNullOrEmpty(ucToggleVisibilityToolTip))
        {
            chkToggleVisibility.ToolTip = ucToggleVisibilityToolTip;
        }

        if (!string.IsNullOrEmpty(ucParentControlID))
        {
            Page oPage = (Page)HttpContext.Current.CurrentHandler;
            Control oParentCtrl = Util.FindControlEx(oPage, ucParentControlID);
            string strChkJS = "";
            if (oParentCtrl != null)
            {
                if (oParentCtrl.GetType().Name == "FormView")
                {
                    #region Is [FormView]
                    foreach (FormViewRow oRow in oParentCtrl.Controls[0].Controls)
                    {
                        foreach (TableCell oCell in oRow.Controls)
                        {
                            foreach (Control oCtrl in oCell.Controls)
                            {
                                strChkJS += getControlChkJS(oCtrl);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Not [FormView]
                    foreach (Control oCtrl in oParentCtrl.Controls)
                    {
                        strChkJS += getControlChkJS(oCtrl);
                    }
                    #endregion
                }

            }

            if (!string.IsNullOrEmpty(strChkJS))
            {
                chkToggleVisibility.Attributes.Add("onclick", strChkJS);
            }
        }
    }

    protected string getControlChkJS(Control oCtrl)
    {
        string strTypeName = "";
        string strChkJS = "";
        if (oCtrl.GetType().Namespace.Contains("System"))
            strTypeName = oCtrl.GetType().Name;
        else
            strTypeName = oCtrl.GetType().BaseType.Name;

        switch (strTypeName)
        {
            case "Util_ucDatePicker":
            case "Util_ucTextBox":
            case "Util_ucUserPicker":
            case "Util_ucCheckBoxList":
            case "Util_ucCommSingleSelect":
            case "Util_ucCommMultiSelect":
            case "Util_ucCascadingDropDown":
                if ((bool)oCtrl.GetType().GetProperty("ucIsToggleVisibility").GetValue(oCtrl, null))
                {
                    CheckBox oCtrlChk = (CheckBox)oCtrl.FindControl("chkVisibility");
                    strChkJS += "Util_ChkBoxToggleChecked('" + chkToggleVisibility.ClientID + "','" + oCtrlChk.ClientID + "');";
                }
                break;
            default:
                break;
        }	
        return strChkJS;
    }
}