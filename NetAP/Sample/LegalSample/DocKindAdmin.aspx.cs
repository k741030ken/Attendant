using System;
using System.Collections.Generic;
using System.Data;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;

public partial class LegalSample_DocKindAdmin : SecurePage
{
    #region 屬性
    /// <summary>
    /// IsEnabled 清單項目
    /// </summary>
    public Dictionary<string, string> _dicIsEnabled
    {
        get
        {
            if (ViewState["_dicIsEnabled"] == null)
            {
                Dictionary<string, string> oIsEnabled = new Dictionary<string, string>();
                oIsEnabled.Clear();
                oIsEnabled.Add("Y", "Y");
                oIsEnabled.Add("N", "N");
                ViewState["_dicIsEnabled"] = oIsEnabled;
            }
            return (Dictionary<string, string>)(ViewState["_dicIsEnabled"]);
        }
        set
        {
            ViewState["_dicIsEnabled"] = value;
        }
    }

    /// <summary>
    /// GridView 顯示欄位定義
    /// </summary>
    public Dictionary<string, string> _dicDisplay
    {
        get
        {
            if (ViewState["_dicDisplay"] == null)
            {
                Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
                dicDisplay.Clear();
                dicDisplay.Add("KindNo", (string)GetLocalResourceObject("_KindNo") + "@50");
                dicDisplay.Add("KindName", (string)GetLocalResourceObject("_KindName") + "@L200");
                dicDisplay.Add("IsEnabled", (string)GetLocalResourceObject("_IsEnabled") + "@C40");
                dicDisplay.Add("Remark", (string)GetLocalResourceObject("_Remark") + "@L100");
                dicDisplay.Add("UpdUserName", (string)GetLocalResourceObject("_UpdUserName"));
                dicDisplay.Add("UpdDateTime", (string)GetLocalResourceObject("_UpdDateTime") + "@T");
                ViewState["_dicDisplay"] = dicDisplay;
            }
            return (Dictionary<string, string>)(ViewState["_dicDisplay"]);
        }
        set
        {
            ViewState["_dicDisplay"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        TabKind1.HeaderText = (string)GetLocalResourceObject("Msg_TabKind1");
        TabKind2.HeaderText = (string)GetLocalResourceObject("Msg_TabKind2");
        TabKind3.HeaderText = (string)GetLocalResourceObject("Msg_TabKind3");

        ucCascadingKind1.ucServiceMethod = LegalSample._LegalDocKindServiceMethod;
        ucCascadingKind1.ucCategory01 = "Kind1";
        ucCascadingKind1.ucIsReadOnly01 = true;
        ucCascadingKind1.ucDropDownListEnabled01 = true;
        ucCascadingKind1.ucDropDownListEnabled02 = false;
        ucCascadingKind1.ucDropDownListEnabled03 = false;
        ucCascadingKind1.ucDropDownListEnabled04 = false;
        ucCascadingKind1.ucDropDownListEnabled05 = false;

        ucCascadingKind2.ucServiceMethod = LegalSample._LegalDocKindServiceMethod;
        ucCascadingKind2.ucCategory01 = "Kind1";
        ucCascadingKind2.ucCategory02 = "Kind2";
        ucCascadingKind2.ucIsReadOnly01 = true;
        ucCascadingKind2.ucIsReadOnly02 = true;
        ucCascadingKind2.ucDropDownListEnabled01 = true;
        ucCascadingKind2.ucDropDownListEnabled02 = true;
        ucCascadingKind2.ucDropDownListEnabled03 = false;
        ucCascadingKind2.ucDropDownListEnabled04 = false;
        ucCascadingKind2.ucDropDownListEnabled05 = false;

        ucKind1.RowCommand += new Util_ucGridView.GridViewRowClick(ucKind1_RowCommand);
        ucKind2.RowCommand += new Util_ucGridView.GridViewRowClick(ucKind2_RowCommand);
        ucKind3.RowCommand += new Util_ucGridView.GridViewRowClick(ucKind3_RowCommand);

        ucKind1.Visible = false;
        ucKind2.Visible = false;
        ucKind3.Visible = false;
        if (!IsPostBack)
        {
            ddlIsEnabled.DataSource = _dicIsEnabled;
            ddlIsEnabled.DataValueField = "key";
            ddlIsEnabled.DataTextField = "value";
            ddlIsEnabled.DataBind();

            ucKind1.ucSortEnabled = false;
            ucKind1.ucPageSizeList = new int[] { 500 };
            ucKind1.ucPageSize = 500;

            ucKind2.ucSortEnabled = false;
            ucKind2.ucPageSizeList = new int[] { 500 };
            ucKind2.ucPageSize = 500;

            ucKind3.ucSortEnabled = false;
            ucKind3.ucPageSizeList = new int[] { 500 };
            ucKind3.ucPageSize = 500;


            TabContainer1.ActiveTabIndex = 0;
            RefreshKind1(true);
        }

    }

    #region ucGridView_RowCommand
    void ucKind1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        string strKindNo = (e.DataKeys != null) ? e.DataKeys[0] : "";
        Dictionary<string, string> dicContext = new Dictionary<string, string>();
        switch (e.CommandName)
        {
            case "cmdAdd":
                ucKind1.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Add");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind1");
                dicContext.Add("ParentKindNo", "Root");
                LaunchPopup(dicContext);
                break;
            case "cmdEdit":
                ucKind1.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Edit");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind1");
                dicContext.Add("ParentKindNo", "Root");
                LaunchPopup(dicContext);
                break;
            case "cmdSelect":
                TabContainer1.ActiveTabIndex = 1;
                ucCascadingKind1.ucDefaultSelectedValue01 = strKindNo;
                RefreshKind2(true, strKindNo);
                break;
            case "cmdDelete":
                DeleteDocKind(strKindNo);
                ucKind1.Visible = true;
                break;
            default:
                Util.MsgBox(RS.Resources.Msg_Undefined1, e.CommandName);
                break;
        }
    }

    void ucKind2_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        string strKindNo = (e.DataKeys != null) ? e.DataKeys[0] : "";
        Dictionary<string, string> dicContext = new Dictionary<string, string>();
        switch (e.CommandName)
        {
            case "cmdAdd":
                ucKind2.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Add");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind2");
                dicContext.Add("ParentKindNo", ucCascadingKind1.ucSelectedValue01);
                LaunchPopup(dicContext);
                break;
            case "cmdEdit":
                ucKind1.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Edit");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind2");
                dicContext.Add("ParentKindNo", ucCascadingKind1.ucSelectedValue01);
                LaunchPopup(dicContext);
                break;
            case "cmdSelect":
                TabContainer1.ActiveTabIndex = 2;
                ucCascadingKind2.ucDefaultSelectedValue01 = ucCascadingKind1.ucSelectedValue01;
                ucCascadingKind2.ucDefaultSelectedValue02 = strKindNo;
                RefreshKind3(true, strKindNo);
                break;
            case "cmdDelete":
                DeleteDocKind(strKindNo);
                ucKind2.Visible = true;
                break;
            default:
                Util.MsgBox(RS.Resources.Msg_Undefined1, e.CommandName);
                break;
        }
    }

    void ucKind3_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        string strKindNo = (e.DataKeys != null) ? e.DataKeys[0] : "";
        Dictionary<string, string> dicContext = new Dictionary<string, string>();
        switch (e.CommandName)
        {
            case "cmdAdd":
                ucKind3.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Add");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind3");
                dicContext.Add("ParentKindNo", ucCascadingKind2.ucSelectedValue02);
                LaunchPopup(dicContext);
                break;
            case "cmdEdit":
                ucKind1.Visible = true;
                dicContext.Clear();
                dicContext.Add("Mode", "Edit");
                dicContext.Add("KindNo", strKindNo);
                dicContext.Add("SourceView", "ucKind3");
                dicContext.Add("ParentKindNo", ucCascadingKind2.ucSelectedValue02);
                LaunchPopup(dicContext);
                break;
            case "cmdDelete":
                DeleteDocKind(strKindNo);
                ucKind3.Visible = true;
                break;
            default:
                Util.MsgBox(RS.Resources.Msg_Undefined1, e.CommandName);
                break;
        }
    }

    #endregion

    protected void btnKind1_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 1;
        RefreshKind2(true, ucCascadingKind1.ucSelectedValue01);
    }

    protected void btnKind2_Click(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 2;
        RefreshKind3(true, ucCascadingKind2.ucSelectedValue02);
    }

    protected void RefreshKind1(bool IsInit = false, string strParentKindNo = "Root")
    {
        if (IsInit)
        {
            ucKind1.Visible = true;

            ucKind1.ucDBName = LegalSample._LegalSysDBName;
            ucKind1.ucDataKeyList = "KindNo".Split(',');
            ucKind1.ucDataQrySQL = string.Format("Select * From LegalDocKind Where ParentKindNo = '{0}' ", strParentKindNo);

            ucKind1.ucDataDisplayDefinition = _dicDisplay;

            ucKind1.ucAddEnabled = true;
            ucKind1.ucEditEnabled = true;
            ucKind1.ucDeleteEnabled = true;
            ucKind1.ucDeleteConfirm = (string)GetLocalResourceObject("Msg_Delete_Confirm"); //確定刪除本類別及其相關的全部子類別？
            ucKind1.ucSelectEnabled = true;
            ucKind1.ucSelectIcon = Util.Icon_TreeNode;
            ucKind1.ucSelectToolTip = (string)GetLocalResourceObject("Msg_SelectTooltip");//往下展開
        }
        ucKind1.Refresh(IsInit);
    }

    protected void RefreshKind2(bool IsInit = false, string strParentKindNo = "")
    {
        if (IsInit)
        {
            ucKind2.Visible = true;
            ucKind2.ucDBName = LegalSample._LegalSysDBName;
            ucKind2.ucDataKeyList = "KindNo".Split(',');
            ucKind2.ucDataQrySQL = string.Format("Select * From LegalDocKind Where ParentKindNo = '{0}' ", strParentKindNo);
            ucKind2.ucDataDisplayDefinition = _dicDisplay;

            ucKind2.ucAddEnabled = true;
            ucKind2.ucEditEnabled = true;
            ucKind2.ucDeleteEnabled = true;
            ucKind2.ucDeleteConfirm = (string)GetLocalResourceObject("Msg_Delete_Confirm"); //確定刪除本類別及其相關的全部子類別？
            ucKind2.ucSelectEnabled = true;
            ucKind2.ucSelectIcon = Util.Icon_TreeNode;
            ucKind2.ucSelectToolTip = (string)GetLocalResourceObject("Msg_SelectTooltip");//往下展開
        }
        ucKind2.Refresh(IsInit);
    }

    protected void RefreshKind3(bool IsInit = false, string strParentKindNo = "")
    {
        if (IsInit)
        {
            ucKind3.Visible = true;
            ucKind3.ucDBName = LegalSample._LegalSysDBName;
            ucKind3.ucDataKeyList = "KindNo".Split(',');
            ucKind3.ucDataQrySQL = string.Format("Select * From LegalDocKind Where ParentKindNo = '{0}' ", strParentKindNo);
            ucKind3.ucDataDisplayDefinition = _dicDisplay;


            ucKind3.ucAddEnabled = true;
            ucKind3.ucEditEnabled = true;
            ucKind3.ucDeleteEnabled = true;
        }
        ucKind3.Refresh(IsInit);
    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        ucKind1.Visible = false;
        ucKind2.Visible = false;
        ucKind3.Visible = false;

        switch (TabContainer1.ActiveTabIndex)
        {
            case 0:
                if (!ucKind1.Visible)
                {
                    RefreshKind1(true);
                }
                break;
            case 1:
                if (!ucKind2.Visible)
                {
                    RefreshKind2(true, ucCascadingKind1.ucSelectedValue01);
                }
                break;
            case 2:
                if (!ucKind3.Visible)
                {
                    RefreshKind3(true, ucCascadingKind2.ucSelectedValue02);
                }
                break;
        }
    }

    protected void DeleteDocKind(string strKindNo)
    {
        if (string.IsNullOrEmpty(strKindNo))
        {
            Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.ParaDataError));
            return;
        }
        else
        {
            try
            {
                //相關連的子類別會由 LegalDocKind 的 Trigger 自動遞迴刪除
                DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
                CommandHelper sb = db.CreateCommandHelper();
                sb.Reset();
                sb.AppendStatement("Delete LegalDocKind Where KindNo = ").AppendParameter("KindNo", strKindNo);
                db.ExecuteNonQuery(sb.BuildCommand());
                Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
            }
            catch (Exception ex)
            {
                Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.Error, ex.ToString()));
            }
        }
    }

    /// <summary>
    /// 資料作業視窗
    /// </summary>
    /// <param name="oContext"></param>
    protected void LaunchPopup(Dictionary<string, string> oContext)
    {
        switch (oContext["Mode"].ToUpper())
        {
            case "ADD":
                ucModalPopup1.ucPopupHeader = (string)GetLocalResourceObject("Msg_PopupHeader_Add"); //新增作業
                ucKindNo.ucTextData = "";
                ucKindNo.ucIsReadOnly = false;
                ucKindNo.Refresh();
                ucKindName.ucTextData = "";
                ucKindName.Refresh();
                ddlIsEnabled.SelectedValue = "Y";
                ucRemark.ucTextData = "";
                ucRemark.Refresh();
                break;
            case "EDIT":
                ucModalPopup1.ucPopupHeader = (string)GetLocalResourceObject("Msg_PopupHeader_Edit"); //編輯作業
                DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
                CommandHelper sb = db.CreateCommandHelper();
                sb.Reset();
                sb.AppendStatement("Select * From LegalDocKind Where KindNo = ").AppendParameter("KindNo",oContext["KindNo"]);
                DataRow dr = db.ExecuteDataSet(sb.BuildCommand()).Tables[0].Rows[0];
                ucKindNo.ucTextData = dr["KindNo"].ToString();
                ucKindNo.ucIsReadOnly = true;
                ucKindNo.Refresh();
                ucKindName.ucTextData = dr["KindName"].ToString();
                ucKindName.Refresh();
                ddlIsEnabled.SelectedValue = dr["IsEnabled"].ToString();
                ucRemark.ucTextData = dr["Remark"].ToString();
                ucRemark.Refresh();
                break;
            default:
                break;
        }
        ucModalPopup1.ucContextData = Util.getJSON(oContext);
        ucModalPopup1.ucPanelID = pnlEdit.ID;
        ucModalPopup1.Show();
    }


    /// <summary>
    /// 按下彈出視窗[確定]按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();

        UserInfo oUser = UserInfo.getUserInfo();
        Dictionary<string, string> dicContext = Util.getDictionary(ucModalPopup1.ucContextData);
        Util_ucGridView oView = (Util_ucGridView)Util.FindControlEx(this, dicContext["SourceView"]);

        switch (dicContext["Mode"].ToUpper())
        {
            case "ADD":
                sb.Reset();
                sb.AppendStatement("Insert LegalDocKind ");
                sb.Append("(KindNo,KindName,IsEnabled,ParentKindNo,Remark,UpdUser,UpdUserName,UpdDateTime)");
                sb.Append(" Values (").AppendParameter("KindNo", ucKindNo.ucTextData);
                sb.Append("        ,").AppendParameter("KindName", ucKindName.ucTextData);
                sb.Append("        ,").AppendParameter("IsEnabled", ddlIsEnabled.SelectedValue);
                sb.Append("        ,").AppendParameter("ParentKindNo", dicContext["ParentKindNo"]);
                sb.Append("        ,").AppendParameter("Remark", ucRemark.ucTextData);
                sb.Append("        ,").AppendParameter("UpdUser", oUser.UserID);
                sb.Append("        ,").AppendParameter("UpdUserName", oUser.UserName);
                sb.Append("        ,").AppendDbDateTime();
                sb.Append(")");
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
                    oView.Visible = true;
                    oView.Refresh(true);
                }
                catch
                {
                    oView.Visible = true;
                    oView.Refresh();
                    Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
                }
                break;
            case "EDIT":
                sb.Reset();
                sb.AppendStatement(" Update LegalDocKind Set KindName = ").AppendParameter("KindName", ucKindName.ucTextData);
                sb.Append(" ,IsEnabled = ").AppendParameter("IsEnabled", ddlIsEnabled.SelectedValue);
                sb.Append(" ,Remark = ").AppendParameter("Remark", ucRemark.ucTextData);
                sb.Append(" ,UpdUser = ").AppendParameter("UpdUser", oUser.UserID);
                sb.Append(" ,UpdUserName = ").AppendParameter("UpdUserName", oUser.UserName);
                sb.Append(" ,UpdDateTime = ").AppendDbDateTime();
                sb.Append(" Where KindNo = ").AppendParameter("KindNo", ucKindNo.ucTextData);
                sb.Append("   and ParentKindNo = ").AppendParameter("ParentKindNo", dicContext["ParentKindNo"]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                    oView.Visible = true;
                    oView.Refresh(true);
                }
                catch
                {
                    oView.Visible = true;
                    oView.Refresh();
                    Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
                }
                break;
            default:
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, dicContext["Mode"]));
                break;
        }
    }


    /// <summary>
    /// 按下彈出視窗[取消]按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dicContext = Util.getDictionary(ucModalPopup1.ucContextData);
        Util_ucGridView oView = (Util_ucGridView)Util.FindControlEx(this, dicContext["SourceView"]);
        oView.Visible = true;
        oView.Refresh();
    }
}