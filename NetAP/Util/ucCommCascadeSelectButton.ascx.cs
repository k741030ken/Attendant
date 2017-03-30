using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [常用組織人員選單按鈕] 控制項
/// </summary>
public partial class Util_ucCommCascadeSelectButton : BaseUserControl
{
    #region 按鈕相關屬性
    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (ViewState["_PopupHeader"] == null)
            {
                ViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header; ;
            }
            return (string)(ViewState["_PopupHeader"]);
        }
        set
        {
            ViewState["_PopupHeader"] = value;
        }
    }

    /// <summary>
    /// 按鈕抬頭
    /// </summary>
    public string ucBtnCaption
    {
        get
        {
            if (ViewState["_BtnCaption"] == null)
            {
                ViewState["_BtnCaption"] = RS.Resources.CommCascadeSelect_btnLaunch; ;
            }
            return (string)(ViewState["_BtnCaption"]);
        }
        set
        {
            ViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (ViewState["_BtnStyle"] == null)
            {
                ViewState["_BtnStyle"] = "Util_clsBtn";
            }
            return (string)(ViewState["_BtnStyle"]);
        }
        set
        {
            ViewState["_BtnStyle"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (ViewState["_BtnWidth"] == null)
            {
                ViewState["_BtnWidth"] = 80;
            }
            return (int)(ViewState["_BtnWidth"]);
        }
        set
        {
            ViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕 Client 端 JS
    /// </summary>
    public string ucBtnClientJS
    {
        //2016.09.22 新增
        get
        {
            if (ViewState["_BtnClientJS"] == null)
            {
                ViewState["_BtnClientJS"] = "Util_IsChkDirty = false;";
            }
            return (string)(ViewState["_BtnClientJS"]);
        }
        set
        {
            ViewState["_BtnClientJS"] = "Util_IsChkDirty = false;" + value;
        }
    }
    #endregion

    #region 其他屬性
    /// <summary>
    /// 是否允許選人員(預設 Y)
    /// </summary>
    public string ucIsSelectUserYN
    {
        get
        {
            if (ViewState["_IsSelectUserYN"] == null)
            {
                ViewState["_IsSelectUserYN"] = "Y";
            }
            return (string)(ViewState["_IsSelectUserYN"]);
        }
        set
        {
            ViewState["_IsSelectUserYN"] = value.ToUpper();
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
        //2016.11.01 新增
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
    /// <para>自訂覆寫 CommCascade 的服務方法</para>
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
    /// 選定的 CompID 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedCompIDToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentCompObjID"] == null)
            {
                ViewState["_ParentCompObjID"] = "";
            }
            return (string)(ViewState["_ParentCompObjID"]);
        }
        set
        {
            ViewState["_ParentCompObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 DeptID 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedDeptIDToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentDeptObjID"] == null)
            {
                ViewState["_ParentDeptObjID"] = "";
            }
            return (string)(ViewState["_ParentDeptObjID"]);
        }
        set
        {
            ViewState["_ParentDeptObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 UserIDList 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedUserIDListToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentUserListObjID"] == null)
            {
                ViewState["_ParentUserListObjID"] = "";
            }
            return (string)(ViewState["_ParentUserListObjID"]);
        }
        set
        {
            ViewState["_ParentUserListObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 CompInfo 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedCompInfoToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentCompInfoObjID"] == null)
            {
                ViewState["_ParentCompInfoObjID"] = "";
            }
            return (string)(ViewState["_ParentCompInfoObjID"]);
        }
        set
        {
            ViewState["_ParentCompInfoObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 DeptInfo 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedDeptInfoToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentDeptInfoObjID"] == null)
            {
                ViewState["_ParentDeptInfoObjID"] = "";
            }
            return (string)(ViewState["_ParentDeptInfoObjID"]);
        }
        set
        {
            ViewState["_ParentDeptInfoObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 UserInfoList 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedUserInfoListToParentObjClientID
    {
        get
        {
            if (ViewState["_ParentUserInfoListObjID"] == null)
            {
                ViewState["_ParentUserInfoListObjID"] = "";
            }
            return (string)(ViewState["_ParentUserInfoListObjID"]);
        }
        set
        {
            ViewState["_ParentUserInfoListObjID"] = value;
        }
    }


    /// <summary>
    /// 選定的 CompID
    /// </summary>
    public string ucSelectedCompID
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedCompID;
        }
    }

    /// <summary>
    /// 選定的 CompInfo
    /// </summary>
    public string ucSelectedCompInfo
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedCompInfo;
        }
    }

    /// <summary>
    /// 選定的 DeptID
    /// </summary>
    public string ucSelectedDeptID
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedDeptID;
        }
    }

    /// <summary>
    /// 選定的 DeptInfo
    /// </summary>
    public string ucSelectedDeptInfo
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedDeptInfo;
        }
    }

    /// <summary>
    /// 選定的 UserIDList
    /// </summary>
    public string ucSelectedUserIDList
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedUserIDList;
        }
    }

    /// <summary>
    /// 選定的 ucSelectedUserInfoList
    /// </summary>
    public string ucSelectedUserInfoList
    {
        //2016.10.13 新增
        get
        {
            return ucCommCascadeSelect1.ucSelectedUserInfoList;
        }
    }
    #endregion

    #region 自訂事件
    /// <summary>
    /// [常用組織人員選單按鈕] 控制項 Launch 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);

    /// <summary>
    /// [常用組織人員選單按鈕] 控制項 Launch 事件
    /// </summary>
    public event Launch onLaunch;

    /// <summary>
    /// [常用組織人員選單按鈕] 控制項 Close 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);

    /// <summary>
    /// [常用組織人員選單按鈕] 控制項 Close 事件
    /// </summary>
    public event Close onClose;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        Refresh();
    }

    void ucCommCascadeSelect1_onDone(object sender, EventArgs e)
    {
        if (onClose != null)
        {
            onClose(this, e);
        }
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        if (onClose != null)
        {
            onClose(this, e);
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        btnLaunch.OnClientClick = ucBtnClientJS; //2016.09.22
        btnLaunch.Text = ucBtnCaption;
        btnLaunch.CssClass = ucBtnCssClass;
        btnLaunch.Style.Remove("width");
        if (ucBtnWidth > 0)
            btnLaunch.Width = ucBtnWidth;
        else
            btnLaunch.Style.Add("width", "auto");
    }

    protected void btnLaunch_Click(object sender, EventArgs e)
    {
        if (onLaunch != null)
        {
            onLaunch(this, e);
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucPopupHeight = 330;

        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;

        ucCommCascadeSelect1.ucIsSelectUserYN = ucIsSelectUserYN;
        ucCommCascadeSelect1.ucIsMultiSelectYN = ucIsMultiSelectYN;
        ucCommCascadeSelect1.ucIsSelectCommUserYN = ucIsSelectCommUserYN;   //2016.11.01
        ucCommCascadeSelect1.ucCustCommCascadeServiceMethod = ucCustCommCascadeServiceMethod; //2016.11.01

        ucCommCascadeSelect1.ucDefCompID = ucDefCompID;
        ucCommCascadeSelect1.ucDefDeptID = ucDefDeptID;
        ucCommCascadeSelect1.ucDefUserIDList = ucDefUserIDList;
        ucCommCascadeSelect1.ucSelectedCompIDToParentObjClientID = ucSelectedCompIDToParentObjClientID;
        ucCommCascadeSelect1.ucSelectedCompInfoToParentObjClientID = ucSelectedCompInfoToParentObjClientID;
        ucCommCascadeSelect1.ucSelectedDeptIDToParentObjClientID = ucSelectedDeptIDToParentObjClientID;
        ucCommCascadeSelect1.ucSelectedDeptInfoToParentObjClientID = ucSelectedDeptInfoToParentObjClientID;
        ucCommCascadeSelect1.ucSelectedUserIDListToParentObjClientID = ucSelectedUserIDListToParentObjClientID;
        ucCommCascadeSelect1.ucSelectedUserInfoListToParentObjClientID = ucSelectedUserInfoListToParentObjClientID;
        ucCommCascadeSelect1.Refresh();

        if (ucIsSelectUserYN != "Y")
        {
            ucModalPopup1.ucPopupWidth = 300;
        }
        ucModalPopup1.ucPanelID = ModalPanel.ID;
        ucModalPopup1.Show();
    }
}