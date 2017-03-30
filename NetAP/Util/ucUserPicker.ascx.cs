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
    //private string _PopupHeader = SinoPac.WebExpress.Common.Properties.Resources.ModalPopup_Header;
    //private string _defBtnTooltip = SinoPac.WebExpress.Common.Properties.Resources.UserPicker_btnLaunch_ToolTip;
    //private string _ReadOnlyClass = "Util_clsReadOnly";
    //private bool _IsRequire = false;
    //private int _defWidth = 100;

    #region 相關屬性
    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (ViewState["_PopupHeader"] == null)
            {
                ViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header;
            }
            return (string)(ViewState["_PopupHeader"]);
        }
        set
        {
            ViewState["_PopupHeader"] = value;
        }
    }

    /// <summary>
    /// 是否 Require(預設 false)
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (ViewState["_IsRequire"] == null)
            {
                ViewState["_IsRequire"] = false;
                txtUserInfoList.CausesValidation = false;
                RequiredFieldValidator1.Enabled = false;
            }
            return (bool)(ViewState["_IsRequire"]);
        }
        set
        {
            ViewState["_IsRequire"] = value;
            txtUserInfoList.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// 唯讀時的 CSSClass (預設 Util_clsReadOnly)
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
    /// 按鈕的提示訊息
    /// </summary>
    public string ucBtnTooltip
    {
        get
        {
            if (ViewState["_BtnTooltip"] == null)
            {
                ViewState["_BtnTooltip"] = RS.Resources.UserPicker_btnLaunch_ToolTip;
            }
            return (string)(ViewState["_BtnTooltip"]);
        }
        set
        {
            ViewState["_BtnTooltip"] = value;
        }
    }

    /// <summary>
    /// 文字框寬度(預設 100)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null)
            {
                ViewState["_Width"] = 100;
            }
            return (int)(ViewState["_Width"]);
        }
        set
        {
            ViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 是否允許多選人員(預設 N)
    /// </summary>
    public string ucIsMultiSelectYN
    {
        get
        {
            if (ViewState["_IsMultiSelectYN"] == null)
            {
                ViewState["_IsMultiSelectYN"] = "N";
            }
            return (string)(ViewState["_IsMultiSelectYN"]);
        }
        set
        {
            ViewState["_IsMultiSelectYN"] = value.ToUpper();
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
            if (ViewState["_IsSelectCommUserYN"] == null)
            {
                ViewState["_IsSelectCommUserYN"] = "Y";
            }
            return (string)(ViewState["_IsSelectCommUserYN"]);
        }
        set
        {
            ViewState["_IsSelectCommUserYN"] = value.ToUpper();
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
            if (ViewState["_CustCommCascadeServiceMethod"] == null)
            {
                ViewState["_CustCommCascadeServiceMethod"] = "";
            }
            return (string)(ViewState["_CustCommCascadeServiceMethod"]);
        }
        set
        {
            ViewState["_CustCommCascadeServiceMethod"] = value;
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
            if (ViewState["_ValidCompIDList"] == null)
            {
                ViewState["_ValidCompIDList"] = "";
            }
            return (string)(ViewState["_ValidCompIDList"]);
        }
        set
        {
            ViewState["_ValidCompIDList"] = value;
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
            if (ViewState["_ValidDeptIDList"] == null)
            {
                ViewState["_ValidDeptIDList"] = "";
            }
            return (string)(ViewState["_ValidDeptIDList"]);
        }
        set
        {
            ViewState["_ValidDeptIDList"] = value;
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
            if (ViewState["_ValidUserIDList"] == null)
            {
                ViewState["_ValidUserIDList"] = "";
            }
            return (string)(ViewState["_ValidUserIDList"]);
        }
        set
        {
            ViewState["_ValidUserIDList"] = value;
        }
    }

    /// <summary>
    /// 預設 CompID
    /// </summary>
    public string ucDefCompID
    {
        get
        {
            if (ViewState["_DefCompID"] == null)
            {
                ViewState["_DefCompID"] = "";
            }
            return (string)(ViewState["_DefCompID"]);
        }
        set
        {
            ViewState["_DefCompID"] = value;
        }
    }

    /// <summary>
    /// 預設 DeptID
    /// </summary>
    public string ucDefDeptID
    {
        get
        {
            if (ViewState["_DefDeptID"] == null)
            {
                ViewState["_DefDeptID"] = "";
            }
            return (string)(ViewState["_DefDeptID"]);
        }
        set
        {
            ViewState["_DefDeptID"] = value;
        }
    }

    /// <summary>
    /// 預設 UserIDList
    /// </summary>
    public string ucDefUserIDList
    {
        get
        {
            if (ViewState["_DefUserIDList"] == null)
            {
                ViewState["_DefUserIDList"] = "";
            }
            return (string)(ViewState["_DefUserIDList"]);
        }
        set
        {
            ViewState["_DefUserIDList"] = value;
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