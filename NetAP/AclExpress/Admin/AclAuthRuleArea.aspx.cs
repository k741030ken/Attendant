using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

/// <summary>
/// [AppKeyMap]維護公用程式 
/// </summary>
public partial class AclExpress_Admin_AclAuthRuleArea : SecurePage
{
    #region 共用屬性

    //資料庫來源
    private string _DBName = AclExpress._AclDBName;
    //鍵值欄位清單
    private string[] _PKList = "RuleID,AreaID,GrantID".Split(',');
    //查詢條件基底 SQL
    private string _QryBaseSQL = @"Select *  From viewAclAuthRuleAreaGrantList Where 0=0 ";
    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (ViewState["_QryResultSQL"] == null) { ViewState["_QryResultSQL"] = _QryBaseSQL; }
            return (string)(ViewState["_QryResultSQL"]);
        }
        set
        {
            ViewState["_QryResultSQL"] = value;
        }
    }

    #endregion

    /// <summary>
    /// 頁面載入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        qryAreaGrant.ucServiceMethod = AclExpress._AclAreaGrantServiceMethod;
        qryAreaGrant.ucCategory01 = "AreaID";
        qryAreaGrant.ucCategory02 = "GrantID";
        qryAreaGrant.ucDropDownListEnabled01 = true;
        qryAreaGrant.ucDropDownListEnabled02 = true;
        qryAreaGrant.ucDropDownListEnabled03 = false;
        qryAreaGrant.ucDropDownListEnabled04 = false;
        qryAreaGrant.ucDropDownListEnabled05 = false;
        qryAreaGrant.Refresh();

        if (!IsPostBack)
        {
            //首次載入
            qryRuleID.ucSourceDictionary = Util.getDictionary(AclExpress.getAclRuleData(), 0, 1, true);
            qryRuleID.Refresh();

            qryAuthType.ucSourceDictionary = Util.getDictionary(AclExpress.getAclAuthTypeList());
            qryAuthType.Refresh();

            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = _QryResultSQL;
            ucGridView1.ucDataKeyList = _PKList;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("AreaID", "區域代號");
            dicDisplay.Add("AreaName", "區域名稱");
            dicDisplay.Add("GrantID", "項目代號");
            dicDisplay.Add("GrantName", "項目名稱");
            dicDisplay.Add("AuthType", "授權類型");
            dicDisplay.Add("AllowActList", "限定動作");
            dicDisplay.Add("UpdUser", "更新人員");
            dicDisplay.Add("UpdDateTime", "更新時間@T");

            ucGridView1.ucDataGroupKey = "RuleID";
            ucGridView1.ucDataDisplayDefinition = dicDisplay;
            ucGridView1.ucSelectEnabled = false;
            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDeleteEnabled = true;
            //Data Dump 2016.11.08
            ucGridView1.ucDataDumpEnabled = true;

            ucGridView1.Refresh(true);
        }

        //事件訂閱
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        ucModalPopup1.onClose += ucModalPopup1_onClose;
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        ucGridView1.Refresh();
    }

    /// <summary>
    /// 資料清單執行命令
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        DataTable dt = null;
        CommandHelper sb = db.CreateCommandHelper();
        string[] oDataKeys = e.DataKeys;
        switch (e.CommandName)
        {
            case "cmdAdd":
                //新增模式
                DivQryArea.Visible = false;
                divMainGridview.Visible = false;
                fmMain.ChangeMode(FormViewMode.Insert);
                fmMain.DataSource = null;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdCopy":
                //資料複製
                sb.Reset();
                sb.AppendStatement("Select * From AclAuthRuleAreaGrantList Where 0 = 0 ");
                sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                DivQryArea.Visible = false;
                divMainGridview.Visible = false;
                fmMain.ChangeMode(FormViewMode.Insert);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdEdit":
                //資料編輯
                DivQryArea.Visible = false;
                divMainGridview.Visible = false;
                sb.Reset();
                sb.AppendStatement("Select * From AclAuthRuleAreaGrantList Where 0 = 0 ");
                sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                fmMain.ChangeMode(FormViewMode.Edit);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdDelete":
                //資料刪除
                try
                {
                    Dictionary<string, string> dicKey = Util.getDictionary(_PKList, oDataKeys);
                    AclExpress.IsAclTableLog("AclAuthRuleAreaGrantList", dicKey, LogHelper.AppTableLogType.Delete);

                    sb.Reset();
                    sb.AppendStatement("Delete From AclAuthRuleAreaGrantList Where 0 = 0 ");
                    sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success); //刪除成功
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdDataDump":
                //Data Dump 2016.11.08
                string strHeader = string.Format("[{0}] 資料轉儲 SQL", "AclAuthRuleAreaGrantList");
                string strDumpSQL = string.Empty;

                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("/* {0}     on [{1}] */ \n", strHeader, DateTime.Now);
                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("Use {0} \nGo\n", AclExpress._AclDBName);
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* AclAuthRuleAreaGrantList    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(AclExpress._AclDBName, "AclAuthRuleAreaGrantList");

                txtDumpSQL.ucTextData = strDumpSQL;
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 850;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucPopupHeader = strHeader;
                ucModalPopup1.ucPanelID = pnlDumpSQL.ID;
                ucModalPopup1.Show();

                break;
            default:
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, e.CommandName)); //無此命令
                break;
        }
    }

    /// <summary>
    /// fmMain 資料繫結事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        //初始物件
        Util_ucTextBox oText;
        Util_ucCascadingDropDown oCascad;
        Util_ucCommSingleSelect oDdl;
        Util_ucCheckBoxList oChkList;

        string strObjID;
        DataRow dr = null;
        if (fmMain.DataSource != null)
        {
            dr = ((DataTable)fmMain.DataSource).Rows[0];
        }

        //「新增」「資料複製」模式
        if (fmMain.CurrentMode == FormViewMode.Insert)
        {
            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnInsert");

            strObjID = "RuleID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(AclExpress.getAclRuleData(), 0, 1, true);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "AreaGrant";
            oCascad = (Util_ucCascadingDropDown)fmMain.FindControl(strObjID);
            oCascad.ucServiceMethod = AclExpress._AclAreaGrantServiceMethod;
            oCascad.ucCategory01 = "AreaID";
            oCascad.ucCategory02 = "GrantID";
            oCascad.ucDropDownListEnabled01 = true;
            oCascad.ucDropDownListEnabled02 = true;
            oCascad.ucDropDownListEnabled03 = false;
            oCascad.ucDropDownListEnabled04 = false;
            oCascad.ucDropDownListEnabled05 = false;
            if (dr != null) oCascad.ucDefaultSelectedValue01 = dr["AreaID"].ToString().Trim();
            if (dr != null) oCascad.ucDefaultSelectedValue02 = dr["GrantID"].ToString().Trim();
            oCascad.Refresh();

            strObjID = "AuthType";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(AclExpress.getAclAuthTypeList());
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "AllowActList";
            oChkList = (Util_ucCheckBoxList)fmMain.FindControl(strObjID);
            oChkList.ucSourceDictionary = Util.getDictionary(AclExpress.getAclAuthActList());
            if (dr != null) oChkList.ucSelectedIDList = dr[strObjID].ToString().Trim();
            oChkList.Refresh();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();
        }

        //「編輯」模式
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnUpdate");

            strObjID = "RuleID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(AclExpress.getAclRuleData(), 0, 1, true);
            oDdl.ucIsReadOnly = true; //鍵值欄位需為唯讀
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "AreaGrant";
            oCascad = (Util_ucCascadingDropDown)fmMain.FindControl(strObjID);
            oCascad.ucServiceMethod = AclExpress._AclAreaGrantServiceMethod;
            oCascad.ucCategory01 = "AreaID";
            oCascad.ucCategory02 = "GrantID";
            oCascad.ucDropDownListEnabled01 = true;
            oCascad.ucDropDownListEnabled02 = true;
            oCascad.ucDropDownListEnabled03 = false;
            oCascad.ucDropDownListEnabled04 = false;
            oCascad.ucDropDownListEnabled05 = false;
            oCascad.ucIsReadOnly01 = true; //鍵值欄位需為唯讀
            oCascad.ucIsReadOnly02 = true; //鍵值欄位需為唯讀
            if (dr != null) oCascad.ucDefaultSelectedValue01 = dr["AreaID"].ToString().Trim();
            if (dr != null) oCascad.ucDefaultSelectedValue02 = dr["GrantID"].ToString().Trim();
            oCascad.Refresh();

            strObjID = "AuthType";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(AclExpress.getAclAuthTypeList());
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "AllowActList";
            oChkList = (Util_ucCheckBoxList)fmMain.FindControl(strObjID);
            oChkList.ucSourceDictionary = Util.getDictionary(AclExpress.getAclAuthActList());
            if (dr != null) oChkList.ucSelectedIDList = dr[strObjID].ToString().Trim();
            oChkList.Refresh();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            //刪除確認訊息
            Button oBtn = (Button)fmMain.FindControl("btnDelete");
            if (oBtn != null)
            {
                Util.ConfirmBox(oBtn, RS.Resources.Msg_Confirm_Delete);
            }
        }
    }

    /// <summary>
    /// 「查詢」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQry_Click(object sender, EventArgs e)
    {
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        _QryResultSQL = _QryBaseSQL;

        _QryResultSQL += Util.getDataQueryConditionSQL("RuleID", "=", qryRuleID.ucSelectedID);
        _QryResultSQL += Util.getDataQueryConditionSQL("AreaID", "=", qryAreaGrant.ucSelectedValue01);
        _QryResultSQL += Util.getDataQueryConditionSQL("GrantID", "=", qryAreaGrant.ucSelectedValue02);

        _QryResultSQL += Util.getDataQueryConditionSQL("AuthType", "=", qryAuthType.ucSelectedID);

        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.Refresh(true);
    }

    /// <summary>
    /// 「查詢清除」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQryClear_Click(object sender, EventArgs e)
    {
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        Util.setControlClear(DivQryArea, true);
        _QryResultSQL = _QryBaseSQL;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.Refresh(true);
    }

    /// <summary>
    /// 「新增取消」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsertCancel_Click(object sender, EventArgs e)
    {
        DivQryArea.Visible = true;
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        ucGridView1.Refresh();
    }

    /// <summary>
    /// 「更新取消」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateCancel_Click(object sender, EventArgs e)
    {
        DivQryArea.Visible = true;
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        ucGridView1.Refresh();
    }

    /// <summary>
    /// 「新增」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsert_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //取得 FormView 編輯控制項的編輯結果
        Dictionary<string, string> oEditResult = Util.getControlEditResult(fmMain);

        sb.Reset();
        sb.AppendStatement("Insert AclAuthRuleAreaGrantList ");
        sb.Append("( ");
        sb.Append("  RuleID,AreaID,GrantID,AuthType,AllowActList,Remark,UpdUser,UpdDateTime");
        sb.Append(" ) Values (");
        sb.Append("   ").AppendParameter("RuleID", oEditResult["RuleID"]);
        sb.Append("  ,").AppendParameter("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
        sb.Append("  ,").AppendParameter("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);
        sb.Append("  ,").AppendParameter("AuthType", oEditResult["AuthType"]);
        sb.Append("  ,").AppendParameter("AllowActList", oEditResult["AllowActList"]);
        sb.Append("  ,").AppendParameter("Remark", oEditResult["Remark"]);
        sb.Append("  ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append("  ,").AppendDbDateTime();
        sb.Append("  )");

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("RuleID", oEditResult["RuleID"]);
                dicKey.Add("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
                dicKey.Add("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);
                AclExpress.IsAclTableLog("AclAuthRuleAreaGrantList", dicKey, LogHelper.AppTableLogType.Create);

                Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success); //新增成功
                DivQryArea.Visible=true;
                divMainFormView.Visible = false;
                divMainGridview.Visible = true;
                ucGridView1.Refresh(true);
            }
            else
            {
                Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error); //新增失敗
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }

    }

    /// <summary>
    /// 「更新」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //取得編輯結果
        Dictionary<string, string> oEditResult = Util.getControlEditResult(fmMain);

        //組合SQL
        //  RuleID,AreaID,GrantID      ,AuthType,AllowActList,Remark,UpdUser,UpdDateTime
        sb.Reset();
        sb.AppendStatement("Update AclAuthRuleAreaGrantList set ");
        sb.Append("  AuthType      = ").AppendParameter("AuthType", oEditResult["AuthType"]);
        sb.Append(", AllowActList  = ").AppendParameter("AllowActList", oEditResult["AllowActList"]);
        sb.Append(", Remark        = ").AppendParameter("Remark", oEditResult["Remark"]);
        sb.Append(", UpdUser       = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime   = ").AppendDbDateTime();
        sb.Append(" Where 0 = 0 ");
        sb.Append("  and RuleID    = ").AppendParameter("RuleID", oEditResult["RuleID"]);
        sb.Append("  and AreaID    = ").AppendParameter("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
        sb.Append("  and GrantID   = ").AppendParameter("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);

        //執行SQL
        if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
        {
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Clear();
            dicKey.Add("RuleID", oEditResult["RuleID"]);
            dicKey.Add("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
            dicKey.Add("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);
            AclExpress.IsAclTableLog("AclAuthRuleAreaGrantList", dicKey, LogHelper.AppTableLogType.Update);

            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success); //更新成功
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error); //更新失敗
        }

        DivQryArea.Visible = true;
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        ucGridView1.Refresh();
    }

    /// <summary>
    /// 「刪除」按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //取得編輯結果
        Dictionary<string, string> oEditResult = Util.getControlEditResult(fmMain);

        try
        {
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Clear();
            dicKey.Add("RuleID", oEditResult["RuleID"]);
            dicKey.Add("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
            dicKey.Add("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);
            AclExpress.IsAclTableLog("AclAuthRuleAreaGrantList", dicKey, LogHelper.AppTableLogType.Delete);

            sb.Reset();
            sb.AppendStatement("Delete AclAuthRuleAreaGrantList ");
            sb.Append(" Where 0 = 0 ");
            sb.Append("  and RuleID    = ").AppendParameter("RuleID", oEditResult["RuleID"]);
            sb.Append("  and AreaID    = ").AppendParameter("AreaID", (oEditResult["AreaGrant"]).Split(',')[0]);
            sb.Append("  and GrantID   = ").AppendParameter("GrantID", (oEditResult["AreaGrant"]).Split(',')[1]);
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success); //刪除成功
                DivQryArea.Visible = true;
                divMainFormView.Visible = false;
                divMainGridview.Visible = true;
                ucGridView1.Refresh(true);
            }
            else
            {
                Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error); //刪除失敗
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }

    }
}