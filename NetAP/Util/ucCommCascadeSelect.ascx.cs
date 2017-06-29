using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

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
            if (PageViewState["_IsAutoUpdateCommUserList"] == null)
            {
                PageViewState["_IsAutoUpdateCommUserList"] = true;
            }
            return (bool)(PageViewState["_IsAutoUpdateCommUserList"]);
        }
        set
        {
            PageViewState["_IsAutoUpdateCommUserList"] = value;
        }
    }

    /// <summary>
    /// 是否允許選擇人員(預設 Y)
    /// </summary>
    public string ucIsSelectUserYN
    {
        get
        {
            if (PageViewState["_IsSelectUserYN"] == null)
            {
                PageViewState["_IsSelectUserYN"] = "Y";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_IsSelectUserYN"]));
        }
        set
        {
            PageViewState["_IsSelectUserYN"] = value.ToUpper();
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_IsSelectCommUserYN"]));
        }
        set
        {
            PageViewState["_IsSelectCommUserYN"] = value.ToUpper();
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_IsMultiSelectYN"]));
        }
        set
        {
            PageViewState["_IsMultiSelectYN"] = value.ToUpper();
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_CustCommCascadeServiceMethod"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ValidCompIDList"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ValidDeptIDList"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ValidUserIDList"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_DefCompID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_DefDeptID"]));
        }
        set
        {
            PageViewState["_DefDeptID"] = value;
        }
    }

    /// <summary>
    /// 預設 UserID
    /// </summary>
    public string ucDefUserIDList
    {
        get
        {
            if (PageViewState["_DefUserIDList"] == null)
            {
                PageViewState["_DefUserIDList"] = "";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_DefUserIDList"]));
        }
        set
        {
            PageViewState["_DefUserIDList"] = value;
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
            if (PageViewState["_ParentCompObjID"] == null)
            {
                PageViewState["_ParentCompObjID"] = "";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentCompObjID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentDeptObjID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentUserListObjID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentCompInfoObjID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentDeptInfoObjID"]));
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
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentUserInfoListObjID"]));
        }
        set
        {
            PageViewState["_ParentUserInfoListObjID"] = value;
        }
    }

    /// <summary>
    /// ModalPopup 物件的父階 [Close] 按鈕ID
    /// </summary>
    public string ucParentModalPopupCloseClientID
    {
        get
        {
            if (PageViewState["_ParentModalPopupCloseClientID"] == null)
            {
                PageViewState["_ParentModalPopupCloseClientID"] = "";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_ParentModalPopupCloseClientID"]));
        }
        set
        {
            PageViewState["_ParentModalPopupCloseClientID"] = value;
        }
    }


    /// <summary>
    /// 候選/結果區 寬度(預設240)
    /// </summary>
    public int ucBoxListWidth
    {
        get
        {
            if (PageViewState["_BoxListWidth"] == null)
            {
                PageViewState["_BoxListWidth"] = 240;
            }
            return (int)(PageViewState["_BoxListWidth"]);
        }
        set
        {
            PageViewState["_BoxListWidth"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區 高度(預設155)
    /// </summary>
    public int ucBoxListHeight
    {
        get
        {
            if (PageViewState["_BoxListHeight"] == null)
            {
                PageViewState["_BoxListHeight"] = 155;
            }
            return (int)(PageViewState["_BoxListHeight"]);
        }
        set
        {
            PageViewState["_BoxListHeight"] = value;
        }
    }

    /// <summary>
    /// 候選/結果區 CSS Style
    /// </summary>
    public string ucBoxListStyle
    {
        get
        {
            if (PageViewState["_BoxListStyle"] == null)
            {
                PageViewState["_BoxListStyle"] = "font-size:10pt;white-space:nowrap;border: 1px solid silver; overflow:auto;";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_BoxListStyle"]));
        }
        set
        {
            PageViewState["_BoxListStyle"] = value;
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