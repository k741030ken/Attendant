using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [人員選取]控制項
/// </summary>
public partial class Util_ucUserPicker : BaseUserControl
{
    #region 相關屬性
    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (PageViewState["_PopupHeader"] == null)
            {
                PageViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header;
            }
            return (string)(PageViewState["_PopupHeader"]);
        }
        set
        {
            PageViewState["_PopupHeader"] = value;
        }
    }

    /// <summary>
    /// 是否 Require(預設 false)
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (PageViewState["_IsRequire"] == null)
            {
                PageViewState["_IsRequire"] = false;
                txtUserInfoList.CausesValidation = false;
                RequiredFieldValidator1.Enabled = false;
            }
            return (bool)(PageViewState["_IsRequire"]);
        }
        set
        {
            PageViewState["_IsRequire"] = value;
            txtUserInfoList.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
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
                PageViewState["_RequireErrMsg"] = "*";
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
    /// 唯讀時的 CSSClass (預設 Util_clsReadOnly)
    /// </summary>
    public string ucReadOnlyCSS
    {
        get
        {
            if (PageViewState["_ReadOnlyCSS"] == null)
            {
                PageViewState["_ReadOnlyCSS"] = "Util_clsReadOnly";
            }
            return (string)(PageViewState["_ReadOnlyCSS"]);
        }
        set
        {
            PageViewState["_ReadOnlyCSS"] = value;
        }
    }

    /// <summary>
    /// 按鈕是否啟用(預設 true)
    /// </summary>
    public bool ucBtnEnabled
    {
        //2017.03.21 新增
        get
        {
            return btnLaunch.Enabled;
        }
        set
        {
            btnLaunch.Enabled = value;
        }
    }

    /// <summary>
    /// 按鈕的提示訊息
    /// </summary>
    public string ucBtnTooltip
    {
        get
        {
            if (PageViewState["_BtnTooltip"] == null)
            {
                PageViewState["_BtnTooltip"] = RS.Resources.UserPicker_btnLaunch_ToolTip;
            }
            return (string)(PageViewState["_BtnTooltip"]);
        }
        set
        {
            PageViewState["_BtnTooltip"] = value;
        }
    }

    /// <summary>
    /// 文字框寬度(預設 100)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (PageViewState["_Width"] == null)
            {
                PageViewState["_Width"] = 100;
            }
            return (int)(PageViewState["_Width"]);
        }
        set
        {
            PageViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 是否允許多選人員(預設 N)
    /// </summary>
    public string ucIsMultiSelectYN
    {
        get
        {
            if (PageViewState["_IsMultiSelectYN"] == null)
            {
                PageViewState["_IsMultiSelectYN"] = "N";
            }
            return (string)(PageViewState["_IsMultiSelectYN"]);
        }
        set
        {
            PageViewState["_IsMultiSelectYN"] = value.ToUpper();
        }
    }

    /// <summary>
    /// 是否允許選擇常用人員(預設 Y)
    /// </summary>
    public string ucIsSelectCommUserYN
    {
        //2016.08.16 新增
        get
        {
            if (PageViewState["_IsSelectCommUserYN"] == null)
            {
                PageViewState["_IsSelectCommUserYN"] = "Y";
            }
            return (string)(PageViewState["_IsSelectCommUserYN"]);
        }
        set
        {
            PageViewState["_IsSelectCommUserYN"] = value.ToUpper();
        }
    }


    /// <summary>
    /// 自訂 CommCascade 的 ServiceMethod
    /// </summary>
    public string ucCustCommCascadeServiceMethod
    {
        //2016.11.01 新增
        get
        {
            if (PageViewState["_CustCommCascadeServiceMethod"] == null)
            {
                PageViewState["_CustCommCascadeServiceMethod"] = "";
            }
            return (string)(PageViewState["_CustCommCascadeServiceMethod"]);
        }
        set
        {
            PageViewState["_CustCommCascadeServiceMethod"] = value;
        }
    }

    /// <summary>
    /// 限制可選擇的 CompID 清單
    /// </summary>
    public string ucValidCompIDList
    {
        //2016.08.16 新增
        get
        {
            if (PageViewState["_ValidCompIDList"] == null)
            {
                PageViewState["_ValidCompIDList"] = "";
            }
            return (string)(PageViewState["_ValidCompIDList"]);
        }
        set
        {
            PageViewState["_ValidCompIDList"] = value;
        }
    }

    /// <summary>
    /// 限制可選擇的 DeptID 清單
    /// </summary>
    public string ucValidDeptIDList
    {
        //2016.08.16 新增
        get
        {
            if (PageViewState["_ValidDeptIDList"] == null)
            {
                PageViewState["_ValidDeptIDList"] = "";
            }
            return (string)(PageViewState["_ValidDeptIDList"]);
        }
        set
        {
            PageViewState["_ValidDeptIDList"] = value;
        }
    }

    /// <summary>
    /// 限制可選擇的 UserID 清單
    /// </summary>
    public string ucValidUserIDList
    {
        //2016.08.16 新增
        get
        {
            if (PageViewState["_ValidUserIDList"] == null)
            {
                PageViewState["_ValidUserIDList"] = "";
            }
            return (string)(PageViewState["_ValidUserIDList"]);
        }
        set
        {
            PageViewState["_ValidUserIDList"] = value;
        }
    }

    /// <summary>
    /// 預設 CompID
    /// </summary>
    public string ucDefCompID
    {
        get
        {
            if (PageViewState["_DefCompID"] == null)
            {
                PageViewState["_DefCompID"] = "";
            }
            return (string)(PageViewState["_DefCompID"]);
        }
        set
        {
            PageViewState["_DefCompID"] = value;
        }
    }

    /// <summary>
    /// 預設 DeptID
    /// </summary>
    public string ucDefDeptID
    {
        get
        {
            if (PageViewState["_DefDeptID"] == null)
            {
                PageViewState["_DefDeptID"] = "";
            }
            return (string)(PageViewState["_DefDeptID"]);
        }
        set
        {
            PageViewState["_DefDeptID"] = value;
        }
    }

    /// <summary>
    /// 預設 UserIDList
    /// </summary>
    public string ucDefUserIDList
    {
        get
        {
            if (PageViewState["_DefUserIDList"] == null)
            {
                PageViewState["_DefUserIDList"] = "";
            }
            return (string)(PageViewState["_DefUserIDList"]);
        }
        set
        {
            PageViewState["_DefUserIDList"] = value;
        }
    }


    /// <summary>
    /// 已選取 UserIDList
    /// </summary>
    public string ucSelectedUserIDList
    {
        get
        {
            if (hidClearData.Value.ToUpper() == "Y")
            {
                txtUserIDList.Text = "";
                txtUserInfoList.Text = "";
            }
            return (string)(txtUserIDList.Text);
        }
    }

    /// <summary>
    /// 已選取 UserInfoList
    /// </summary>
    public string ucSelectedUserInfoList
    {
        get
        {
            return (string)(txtUserInfoList.Text);
        }
    }


    /// <summary>
    /// 是否使用切換可見功能(預設 false)
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
        get { return int.Parse(labCaption.Width.ToString()); }
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
    #endregion

    #region 自訂事件
    /// <summary>
    /// 開啟[人員清單]的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);
    /// <summary>
    /// 關閉[人員清單]的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);
    public event Launch onLaunch;
    public event Close onClose;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
        if (IsPostBack)
        {
            if (!string.IsNullOrEmpty(ucCommCascadeSelect1.ucSelectedUserIDList))
            {
                txtUserIDList.Text = ucCommCascadeSelect1.ucSelectedUserIDList;
                txtUserInfoList.Text = ucCommCascadeSelect1.ucSelectedUserInfoList;
                ucDefUserIDList = txtUserIDList.Text;
            }
        }

        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
    }

    /// <summary>
    /// 清除目前設定
    /// </summary>
    public void Clear()
    {
        //2016.06.28 新增
        ucDefUserIDList = "";
        txtUserIDList.Text = "";
        txtUserInfoList.Text = "";
        ((Util_ucCommCascadeSelect)ucCommCascadeSelect1).ucSelectedUserIDList = "";
        ((Util_ucCommCascadeSelect)ucCommCascadeSelect1).ucSelectedUserInfoList = "";
    }

    /// <summary>
    /// 重新整理/初始元件
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

        txtUserIDList.Style["display"] = "none";
        string strJS = "this.value='';";
        strJS += "var oFlag = document.getElementById('" + hidClearData.ClientID + "');if (oFlag != null){oFlag.value='Y';}";
        txtUserInfoList.Attributes.Add("ondblclick", strJS);
        hidClearData.Value = "N";
        txtUserInfoList.ToolTip = SinoPac.WebExpress.Common.Properties.Resources.UserPicker_UserInfoList_ToolTip;
        txtUserInfoList.Width = ucWidth;


        if (!string.IsNullOrEmpty(ucBtnTooltip))
        {
            btnLaunch.ToolTip = ucBtnTooltip;
        }

        if (!string.IsNullOrEmpty(ucReadOnlyCSS))
        {
            txtUserInfoList.CssClass = ucReadOnlyCSS;
        }
        else
        {
            txtUserInfoList.CssClass = "";
        }
        //處理預設人員
        string[] arDefUserList = Util.getFixList(ucDefUserIDList.Split(','));
        if (arDefUserList != null && arDefUserList.Count() > 0)
        {
            Dictionary<string, string> dicDefUserList = new Dictionary<string, string>();
            string tmpName = "";
            for (int i = 0; i < arDefUserList.Count(); i++)
            {
                tmpName = UserInfo.findUserName(arDefUserList[i]);
                if (!string.IsNullOrEmpty(tmpName))
                {
                    dicDefUserList.Add(arDefUserList[i], tmpName);
                }
            }

            if (dicDefUserList.Count > 0)
            {
                txtUserIDList.Text = Util.getStringJoin(Util.getArray(dicDefUserList));
                txtUserInfoList.Text = Util.getStringJoin(Util.getArray(Util.getDataTable(dicDefUserList), 2));
                ucDefUserIDList = txtUserIDList.Text;
            }
        }
    }


    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {

        if (!string.IsNullOrEmpty(ucCommCascadeSelect1.ucSelectedUserIDList))
        {
            hidClearData.Value = "N";
        }

        if (onClose != null)
        {
            onClose(this, e);
        }
    }

    protected void btnLaunch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtUserIDList.Text))
        {
            if (ucIsMultiSelectYN == "N" && txtUserIDList.Text.Split(',').Count() > 1)
            {
                Util.MsgBox(SinoPac.WebExpress.Common.Properties.Resources.JS_Alert_OnlyChooseOne); //只能單選，但預設人員清單卻超過一人
                return;
            }
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucPopupHeight = 320;
        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;
        ucCommCascadeSelect1.ucIsSelectUserYN = "Y";
        ucCommCascadeSelect1.ucIsMultiSelectYN = ucIsMultiSelectYN;
        ucCommCascadeSelect1.ucIsSelectCommUserYN = ucIsSelectCommUserYN;
        ucCommCascadeSelect1.ucCustCommCascadeServiceMethod = ucCustCommCascadeServiceMethod; //2016.11.01

        ucCommCascadeSelect1.ucDefCompID = ucDefCompID;
        ucCommCascadeSelect1.ucDefDeptID = ucDefDeptID;
        ucCommCascadeSelect1.ucDefUserIDList = ucDefUserIDList;
        ucCommCascadeSelect1.ucSelectedUserIDListToParentObjClientID = txtUserIDList.ClientID;
        ucCommCascadeSelect1.ucSelectedUserInfoListToParentObjClientID = txtUserInfoList.ClientID;
        //2016.08.16 新增
        ucCommCascadeSelect1.ucValidCompIDList = ucValidCompIDList;
        ucCommCascadeSelect1.ucValidDeptIDList = ucValidDeptIDList;
        ucCommCascadeSelect1.ucValidUserIDList = ucValidUserIDList;
        ucCommCascadeSelect1.Refresh();

        ucModalPopup1.ucPanelID = ModalPanel.ID;
        ucModalPopup1.Show();

        if (onLaunch != null)
        {
            onLaunch(this, e);
        }
    }
}