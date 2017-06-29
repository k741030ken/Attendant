using System;
using System.Web;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [常用組織人員選單按鈕] 控制項
/// </summary>
public partial class Util_ucCommCascadeSelectButton : BaseUserControl
{
    string _BtnClientJS = "Util_IsChkDirty = false;";

    #region 按鈕相關屬性
    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (PageViewState["_PopupHeader"] == null)
            {
                PageViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header; ;
            }
            return (string)(PageViewState["_PopupHeader"]);
        }
        set
        {
            PageViewState["_PopupHeader"] = value;
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
    /// 按鈕抬頭
    /// </summary>
    public string ucBtnCaption
    {
        get
        {
            if (PageViewState["_BtnCaption"] == null)
            {
                PageViewState["_BtnCaption"] = RS.Resources.CommCascadeSelect_btnLaunch; ;
            }
            return (string)(PageViewState["_BtnCaption"]);
        }
        set
        {
            PageViewState["_BtnCaption"] = value;
        }
    }

    /// <summary>
    /// 按鈕提示訊息
    /// </summary>
    public string ucBtnToolTip
    {
        //2017.05.19 新增
        get
        {
            if (PageViewState["_BtnToolTip"] == null)
            {
                PageViewState["_BtnToolTip"] = string.Empty;
            }
            return PageViewState["_BtnToolTip"].ToString();
        }
        set
        {
            PageViewState["_BtnToolTip"] = value;
        }
    }

    /// <summary>
    /// 按鈕樣式
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (PageViewState["_BtnStyle"] == null)
            {
                PageViewState["_BtnStyle"] = "Util_clsBtn";
            }
            return (string)(PageViewState["_BtnStyle"]);
        }
        set
        {
            PageViewState["_BtnStyle"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucBtnWidth
    {
        get
        {
            if (PageViewState["_BtnWidth"] == null)
            {
                PageViewState["_BtnWidth"] = 80;
            }
            return (int)(PageViewState["_BtnWidth"]);
        }
        set
        {
            PageViewState["_BtnWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕 Client 端 JS
    /// </summary>
    public string ucBtnClientJS
    {
        //配合Fortify 2017.04.21
        get
        {
            return _BtnClientJS;
        }
        set
        {
           _BtnClientJS += value;
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
            if (PageViewState["_IsSelectUserYN"] == null)
            {
                PageViewState["_IsSelectUserYN"] = "Y";
            }
            return (string)(PageViewState["_IsSelectUserYN"]);
        }
        set
        {
            PageViewState["_IsSelectUserYN"] = value.ToUpper();
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
        //2016.11.01 新增
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
    /// <para>自訂覆寫 CommCascade 的服務方法</para>
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
    /// 選定的 CompID 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedCompIDToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentCompObjID"] == null)
            {
                PageViewState["_ParentCompObjID"] = "";
            }
            return (string)(PageViewState["_ParentCompObjID"]);
        }
        set
        {
            PageViewState["_ParentCompObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 DeptID 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedDeptIDToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentDeptObjID"] == null)
            {
                PageViewState["_ParentDeptObjID"] = "";
            }
            return (string)(PageViewState["_ParentDeptObjID"]);
        }
        set
        {
            PageViewState["_ParentDeptObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 UserIDList 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedUserIDListToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentUserListObjID"] == null)
            {
                PageViewState["_ParentUserListObjID"] = "";
            }
            return (string)(PageViewState["_ParentUserListObjID"]);
        }
        set
        {
            PageViewState["_ParentUserListObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 CompInfo 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedCompInfoToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentCompInfoObjID"] == null)
            {
                PageViewState["_ParentCompInfoObjID"] = "";
            }
            return (string)(PageViewState["_ParentCompInfoObjID"]);
        }
        set
        {
            PageViewState["_ParentCompInfoObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 DeptInfo 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedDeptInfoToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentDeptInfoObjID"] == null)
            {
                PageViewState["_ParentDeptInfoObjID"] = "";
            }
            return (string)(PageViewState["_ParentDeptInfoObjID"]);
        }
        set
        {
            PageViewState["_ParentDeptInfoObjID"] = value;
        }
    }

    /// <summary>
    /// 選定的 UserInfoList 輸出到父階物件 ID
    /// </summary>
    public string ucSelectedUserInfoListToParentObjClientID
    {
        get
        {
            if (PageViewState["_ParentUserInfoListObjID"] == null)
            {
                PageViewState["_ParentUserInfoListObjID"] = "";
            }
            return (string)(PageViewState["_ParentUserInfoListObjID"]);
        }
        set
        {
            PageViewState["_ParentUserInfoListObjID"] = value;
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

        if (!string.IsNullOrEmpty(ucBtnToolTip)) //2017.05.19
            btnLaunch.ToolTip = ucBtnToolTip;

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