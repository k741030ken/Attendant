using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [常用組織人員選單] 控制項
/// </summary>
public partial class Util_ucCommCascadeSelect : BaseUserControl
{
    private string _CommUserList = "CommUserList"; //[常用人員]選單鍵值

    /// <summary>
    /// 是否自動更新常用人員清單(預設 true)
    /// </summary>
    public bool ucIsAutoUpdateCommUserList
    {
        get
        {
            if (ViewState["_IsAutoUpdateCommUserList"] == null)
            {
                ViewState["_IsAutoUpdateCommUserList"] = true;
            }
            return (bool)(ViewState["_IsAutoUpdateCommUserList"]);
        }
        set
        {
            ViewState["_IsAutoUpdateCommUserList"] = value;
        }
    }

    /// <summary>
    /// 是否允許選擇人員(預設 Y)
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
    /// 預設 UserID
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
    /// 選定的 CompID
    /// </summary>
    public string ucSelectedCompID { get { return ucCascadingDropDown1.ucSelectedValue01; } }

    /// <summary>
    /// 選定的 DeptID
    /// </summary>
    public string ucSelectedDeptID { get { return ucCascadingDropDown1.ucSelectedValue02; } }

    /// <summary>
    /// 選定的 UserIDList
    /// </summary>
    public string ucSelectedUserIDList { get { return idUserIDList.Text; } set { idUserIDList.Text = value; } }

    /// <summary>
    /// 選定的 CompInfo
    /// </summary>
    public string ucSelectedCompInfo { get { return ucCascadingDropDown1.ucSelectedText01; } }

    /// <summary>
    /// 選定的 DeptInfo
    /// </summary>
    public string ucSelectedDeptInfo { get { return ucCascadingDropDown1.ucSelectedText02; } }

    /// <summary>
    /// 選定的 UserInfoList
    /// </summary>
    public string ucSelectedUserInfoList { get { return idUserInfoList.Text; } set { idUserInfoList.Text = value; } }

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
    /// ModalPopup 物件的父階 [Close] 按鈕ID
    /// </summary>
    public string ucParentModalPopupCloseClientID
    {
        get
        {
            if (ViewState["_ParentModalPopupCloseClientID"] == null)
            {
                ViewState["_ParentModalPopupCloseClientID"] = "";
            }
            return (string)(ViewState["_ParentModalPopupCloseClientID"]);
        }
        set
        {
            ViewState["_ParentModalPopupCloseClientID"] = value;
        }
    }


    /// <summary>
    /// 候選/結果區 寬度(預設240)
    /// </summary>
    public int ucBoxListWidth
    {
        get
        {
            if (ViewState["_BoxListWidth"] == null)
            {
                ViewState["_BoxListWidth"] = 240;
            }
            return (int)(ViewState["_BoxListWidth"]);
        }
        set
        {
            ViewState["_BoxListWidth"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區 高度(預設155)
    /// </summary>
    public int ucBoxListHeight
    {
        get
        {
            if (ViewState["_BoxListHeight"] == null)
            {
                ViewState["_BoxListHeight"] = 155;
            }
            return (int)(ViewState["_BoxListHeight"]);
        }
        set
        {
            ViewState["_BoxListHeight"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區 CSS Style
    /// </summary>
    public string ucBoxListStyle
    {
        get
        {
            if (ViewState["_BoxListStyle"] == null)
            {
                ViewState["_BoxListStyle"] = "font-size:10pt;white-space:nowrap;border: 1px solid silver; overflow:auto;";
            }
            return (string)(ViewState["_BoxListStyle"]);
        }
        set
        {
            ViewState["_BoxListStyle"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setJS(Util._JSjQueryUrl, this.ClientID + "_jQuery");
        if (!IsPostBack)
        {
            Refresh();
        }
        else
        {
            //自動更新常用人員清單
            if (ucIsSelectUserYN == "Y" && ucIsAutoUpdateCommUserList)
            {
                if (Session["UserID"] != null && !string.IsNullOrEmpty(idUserIDList.Text))
                {
                    string strCurrCommUserList = UserInfo.getUserProperty(Session["UserID"].ToString(), "User", _CommUserList)["PropJSON"].Trim();
                    if (Util.getCompareList(idUserIDList.Text.Split(','), strCurrCommUserList.Split(','), Util.ListCompareMode.Diff).Count() > 0)
                    {
                        //有差異時才需更新
                        string strNewCommUserList = Util.getStringJoin(Util.getCompareList(idUserIDList.Text.Split(','), strCurrCommUserList.Split(','), Util.ListCompareMode.Merge));
                        UserInfo.setUserProperty(Session["UserID"].ToString(), "User", _CommUserList, null, null, null, strNewCommUserList);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        StringBuilder strJS = new StringBuilder();

        //設定Ajax關聯式下拉選單
        ucCascadingDropDown1.SetDefault();
        if (!string.IsNullOrEmpty(ucCustCommCascadeServiceMethod)) 
        {
            ucCascadingDropDown1.ucServiceMethod = ucCustCommCascadeServiceMethod; //2016.11.01 調整
        }
        ucCascadingDropDown1.ucDropDownListEnabled03 = false;
        ucCascadingDropDown1.ucIsVerticalLayout = true;
        ucCascadingDropDown1.ucDefaultSelectedValue01 = ucDefCompID;
        ucCascadingDropDown1.ucDefaultSelectedValue02 = ucDefDeptID;
        ucCascadingDropDown1.ucValidKeyList01 = ucValidCompIDList; //2016.08.16 新增
        ucCascadingDropDown1.ucValidKeyList02 = ucValidDeptIDList; //2016.08.16 新增

        //若能選擇人員
        if (ucIsSelectUserYN == "Y")
        {
            ucCascadingDropDown1.ucDropDownListEnabled03 = true;
            ucCascadingDropDown1.ucCategory03 = "UserID";
            ucCascadingDropDown1.ucPromptText03 = SinoPac.WebExpress.Common.Properties.Resources.CommCascadeSelect_SelectUser;
            ucCascadingDropDown1.ucDefaultSelectedValue03 = (!string.IsNullOrEmpty(ucDefUserIDList)) ? ucDefUserIDList.Split(',')[0] : "";
            ucCascadingDropDown1.ucValidKeyList03 = ucValidUserIDList; //2016.08.16 新增
        }

        ucCascadingDropDown1.Refresh();

        //設定常用人員
        ddlCommUser.BorderWidth = 1;
        ddlCommUser.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");

        ddlCommUser.Items.Clear();
        ddlCommUser.DataValueField = "Key";
        ddlCommUser.DataTextField = "Value";

        if (Session["UserID"] != null)
        {
            string strCommUserList = UserInfo.getUserProperty(Session["UserID"].ToString(), "User", _CommUserList)["PropJSON"].Trim();
            if (!string.IsNullOrEmpty(strCommUserList))
            {
                ddlCommUser.DataSource = Util.getDictionary(UserInfo.findUserName(strCommUserList.Split(',')));
                ddlCommUser.DataBind();
            }
        }
        ddlCommUser.Items.Insert(0, new ListItem(SinoPac.WebExpress.Common.Properties.Resources.CommCascadeSelect_SelectCommUser, ""));
        ddlCommUser.SelectedIndex = 0;


        //設定按鈕
        btnAddCommUser.Text = SinoPac.WebExpress.Common.Properties.Resources.CommCascadeSelect_AddResult;
        btnAddResult.Text = SinoPac.WebExpress.Common.Properties.Resources.CommCascadeSelect_AddResult;
        btnAddCommUser.Attributes.Add("style", "width: " + ucBoxListWidth + "px; margin-top:5px;");
        btnAddResult.Attributes.Add("style", "width: " + ucBoxListWidth + "px; margin-top:5px;");
        divResultBoxList.Style.Value = string.Format("{0};width:{1}px;height:{2}px;", ucBoxListStyle, ucBoxListWidth + 60, ucBoxListHeight + 5);

        //初始傳回欄位
        idCompID.Text = "";
        idDeptID.Text = "";
        idUserIDList.Text = "";
        idCompInfo.Text = "";
        idDeptInfo.Text = "";
        idUserInfoList.Text = "";

        idCompID.Style.Add("display", "none");
        idDeptID.Style.Add("display", "none");
        idUserIDList.Style.Add("display", "none");
        idCompInfo.Style.Add("display", "none");
        idDeptInfo.Style.Add("display", "none");
        idUserInfoList.Style.Add("display", "none");

        //處理預設人員清單
        defResultItem.Text = "";
        if (ucIsSelectUserYN == "Y" && !string.IsNullOrEmpty(ucDefUserIDList))
        {
            //需排除系統管理員
            string[] defUserList = Util.getCompareList(ucDefUserIDList.Split(','), Util.getAppSetting("app://AdminUserID/").Split(','), Util.ListCompareMode.Subset);
            DataTable dt = Util.getDataTable(UserInfo.findUserName(defUserList));

            if (dt != null && dt.Rows.Count > 0)
            {
                //按照ucDefUserIDList原始順序顯示項目，並設為傳回值
                DataRow[] drs;
                for (int i = 0; i < ucDefUserIDList.Split(',').Count(); i++)
                {
                    drs = dt.Select(string.Format("Key = '{0}'", ucDefUserIDList.Split(',')[i]));
                    if (drs.Count() > 0)
                    {
                        defResultItem.Text += string.Format(Util._ReorderItemHtmlFormat, drs[0][0], drs[0][2]);
                    }
                }
            }

            Util.setJSContent("$(function() {" + this.ClientID + "_calData(); \n }); \n", this.ClientID + "_Selected_Init");
        }

        //btnAddCommUser
        strJS.Clear();
        if (ucIsMultiSelectYN.ToUpper() == "N")
        {
            //只能單選
            strJS.Append("if ($('#" + ResultItemContent.ClientID + " li').length > 0){ alert(JS_Alert_OnlyChooseOne);}else{");
        }

        strJS.Append("var chkValue = $('#" + ddlCommUser.ClientID + " :selected').val();"
                    + "if (chkValue.length > 0){"
                    + "  if ( $('#" + ResultItemContent.ClientID + " li:contains(\"' + chkValue + '\")').length < 1 ){"
                    + "     $('#" + ResultItemContent.ClientID + "').append('" + string.Format(Util._ReorderItemHtmlFormat, "' + $('#" + ddlCommUser.ClientID + " :selected').val() + '", "' + $('#" + ddlCommUser.ClientID + " :selected').text() + '") + "');"
                    + "     }else{alert(JS_Alert_RepeatChoose);}"
                    + "  }"
                    );

        if (ucIsMultiSelectYN.ToUpper() == "N")
        {
            strJS.Append("}");
        }

        strJS.Append(this.ClientID + "_calData();return false;");
        btnAddCommUser.OnClientClick = strJS.ToString();

        //btnAddResult
        strJS.Clear();
        if (ucIsMultiSelectYN.ToUpper() == "N")
        {
            //只能單選
            strJS.Append("if ($('#" + ResultItemContent.ClientID + " li').length > 0){ alert(JS_Alert_OnlyChooseOne);}else{");
        }

        strJS.Append("var chkValue = $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').val();"
                    + "if (chkValue.length > 0){"
                    + "  if ( $('#" + ResultItemContent.ClientID + " li:contains(\"' + chkValue + '\")').length < 1 ){"
                    + "     $('#" + ResultItemContent.ClientID + "').append('" + string.Format(Util._ReorderItemHtmlFormat, "' + $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').val() + '", "' + $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').text() + '") + "');"
                    + "     }else{alert(JS_Alert_RepeatChoose);}"
                    + "  }"
                    );

        if (ucIsMultiSelectYN.ToUpper() == "N")
        {
            strJS.Append("}");
        }

        strJS.Append(this.ClientID + "_calData();return false;");
        btnAddResult.OnClientClick = strJS.ToString();

        //處理版面顯示
        if (ucIsSelectUserYN == "Y")
        {
            btnAddResult.Style["display"] = "";
            Area_CommUserList.Style["display"] = "";
            Area_ResultList.Style["display"] = "";

            if (ucIsSelectCommUserYN.ToUpper() != "Y")
            {
                //2016.08.16 新增
                Area_CommUserList.Style["display"] = "none";
                btnAddResult.Style["margin-top"] = "57px";
            }
        }
        else
        {
            btnAddResult.Style["display"] = "none";
            Area_CommUserList.Style["display"] = "none";
            Area_ResultList.Style["display"] = "none";
        }
    }

}