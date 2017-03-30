using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using RS = SinoPac.WebExpress.Common.Properties;
using AclRS = SinoPac.WebExpress.Work.Properties;

public partial class AclExpress_Admin_AclRule : SecurePage
{
    #region 頁面共用

    //資料庫來源
    private string _DBName = AclExpress._AclDBName;
    //主檔鍵值欄位
    private string[] _Main_KeyList = "RuleID".Split(',');
    //明細檔鍵值欄位
    private string[] _Detail_KeyList = "RuleID,ChkGrpNo,ChkSeqNo".Split(',');

    //是否為主檔 Add / Copy 模式
    private bool _IsADD
    {
        get
        {
            if (ViewState["_IsADD"] == null) { ViewState["_IsADD"] = false; }
            return (bool)(ViewState["_IsADD"]);
        }
        set
        {
            ViewState["_IsADD"] = value;
        }
    }

    //主檔 RuleID 值
    private string _RuleID
    {
        get
        {
            if (ViewState["_RuleID"] == null) { ViewState["_RuleID"] = ""; }
            return (string)(ViewState["_RuleID"]);
        }
        set
        {
            ViewState["_RuleID"] = value;
        }
    }

    //明細 ChkGrpNo 值
    private int _ChkGrpNo
    {
        get
        {
            if (ViewState["_ChkGrpNo"] == null) { ViewState["_ChkGrpNo"] = 0; }
            return (int)(ViewState["_ChkGrpNo"]);
        }
        set
        {
            ViewState["_ChkGrpNo"] = value;
        }
    }

    //明細 ChkSeqNo 值
    private int _ChkSeqNo
    {
        get
        {
            if (ViewState["_ChkSeqNo"] == null) { ViewState["_ChkSeqNo"] = 0; }
            return (int)(ViewState["_ChkSeqNo"]);
        }
        set
        {
            ViewState["_ChkSeqNo"] = value;
        }
    }


    private Dictionary<string, string> _Dic_ChkOrgUserObjectProperty
    {
        get
        {
            if (ViewState["_Dic_ChkOrgUserObjectProperty"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_ChkOrgUserObjectProperty"];
            }
            else
            {
                ViewState["_Dic_ChkOrgUserObjectProperty"] = AclExpress.getChkOrgUserObjectPropertyList();
                return (Dictionary<string, string>)ViewState["_Dic_ChkOrgUserObjectProperty"];
            }
        }
    }

    private Dictionary<string, string> _Dic_ChkOrgUserExp
    {
        get
        {
            if (ViewState["_Dic_ChkOrgUserExp"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_ChkOrgUserExp"];
            }
            else
            {
                ViewState["_Dic_ChkOrgUserExp"] = AclExpress.getChkOrgUserExpList();
                return (Dictionary<string, string>)ViewState["_Dic_ChkOrgUserExp"];
            }
        }
    }


    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次進入
            if (!AclExpress.IsAclAdminUser())
            {
                divError.Visible = true;
                DivMainList.Visible = false;
                DivMainSingle.Visible = false;
                DivDetailArea.Visible = false;
                labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, AclRS.Resources.Msg_AclAdminDeny);
                return;
            }

            //強制顯示主檔清單
            _IsADD = false;
            _RuleID = "";
            _ChkGrpNo = 0;
            _ChkSeqNo = 0;

            Refresh(true);
        }


        //彈出視窗 事件訂閱
        ucModalPopup1.onClose += ucModalPopup1_onClose;

        //主檔清單 事件訂閱
        ucGridMain.RowCommand += ucGridMain_RowCommand;

        //明細檔清單 訂閱 RowCommand 事件
        ucGridDetail.RowCommand += ucGridDetail_RowCommand;

    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        Refresh();
    }

    /// <summary>
    /// 重新整理全頁
    /// <para>自動處理 fmMain / ucGridMain / ucGridDetail 之間的連動關係</para>
    /// </summary>
    /// <param name="InitGridMain">初始主檔清單</param>
    /// <param name="InitGridDetail">初始明細清單</param>
    protected void Refresh(bool InitGridMain = false, bool InitGridDetail = false)
    {

        #region 設定主檔清單
        //「主檔清單」屬性
        ucGridMain.ucDBName = _DBName;
        ucGridMain.ucDataQrySQL = "Select * From AclRule ";
        ucGridMain.ucDataKeyList = _Main_KeyList;
        ucGridMain.ucDefSortExpression = _Main_KeyList[0];
        ucGridMain.ucDefSortDirection = "Asc";
        Dictionary<string, string> oDicMain = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicMain.Clear();
        oDicMain.Add("RuleID", "規則代號");
        oDicMain.Add("RuleName", "規則名稱");
        oDicMain.Add("IsEnabled", "啟用@Y");
        oDicMain.Add("UpdUser", "更新人員");
        oDicMain.Add("UpdDateTime", "更新時間@T");
        ucGridMain.ucDataDisplayDefinition = oDicMain;

        //設定其他屬性
        ucGridMain.ucAddEnabled = true;
        ucGridMain.ucCopyEnabled = true;
        ucGridMain.ucDeleteEnabled = true;
        ucGridMain.ucEditEnabled = true;
        //Data Dump 2016.11.08
        ucGridMain.ucDownloadEnabled = true;
        ucGridMain.ucDownloadIcon = Util.Icon_DataDump;
        ucGridMain.ucDownloadToolTip = RS.Resources.GridView_DataDump;
        #endregion

        #region 設定明細檔清單
        //「明細檔清單1」屬性
        ucGridDetail.ucDBName = _DBName;
        ucGridDetail.ucDataQrySQL = string.Format("Select * From AclRuleExp Where RuleID = '{0}' ", (string.IsNullOrEmpty(_RuleID)) ? "---" : _RuleID);
        ucGridDetail.ucDataKeyList = _Detail_KeyList;
        ucGridDetail.ucDefSortExpression = "ChkGrpNo, ChkSeqNo";
        ucGridDetail.ucDefSortDirection = "";
        Dictionary<string, string> oDicDetail = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicDetail.Clear();
        oDicDetail.Add("ChkGrpNo", "群");
        oDicDetail.Add("ChkSeqNo", "序");
        oDicDetail.Add("IsEnabled", "啟用@Y");
        oDicDetail.Add("ChkOrgUserObjectProperty", "檢核物件");
        oDicDetail.Add("ChkOrgUserSW", "運算@Y");
        oDicDetail.Add("ChkOrgUserExp", "條件@L30");
        oDicDetail.Add("ChkOrgUserPropertyValue", "比對");
        oDicDetail.Add("ChkCodeMapSW", "對照@Y");
        oDicDetail.Add("ChkCodeMapFldName", "對照欄位");
        oDicDetail.Add("UpdUser", "更新人員");
        oDicDetail.Add("UpdDateTime", "更新時間@T");
        ucGridDetail.ucDataDisplayDefinition = oDicDetail;

        ucGridDetail.ucAddEnabled = true;
        ucGridDetail.ucCopyEnabled = true;
        ucGridDetail.ucSelectEnabled = false;
        ucGridDetail.ucEditEnabled = true;
        ucGridDetail.ucDeleteEnabled = true;

        ucGridDetail.ucMultilingualToolTip = "對照來源維護";
        ucGridDetail.ucMultilingualIcon = Util.Icon_TreeNode;
        ucGridDetail.ucMultilingualEnabledDataColName = "ChkCodeMapSW";
        ucGridDetail.ucMultilingualEnabled = true;

        #endregion

        if (_IsADD)
        {
            //主檔 Add/Copy模式
            DivMainList.Visible = false;
            DivMainSingle.Visible = true;
            DivDetailArea.Visible = false;
            fmMainRefresh();
        }
        else
        {
            //非主檔 Add/Copy 模式
            if (string.IsNullOrEmpty(_RuleID))
            {
                //無鍵值，顯示主檔清單
                DivMainList.Visible = true;
                DivMainSingle.Visible = false;
                DivDetailArea.Visible = false;
                ucGridMain.Refresh(InitGridMain);
            }
            else
            {
                //有鍵值，顯示指定主檔及其明細
                DivMainList.Visible = false;
                DivMainSingle.Visible = true;
                DivDetailArea.Visible = true;
                fmMainRefresh();
                ucGridDetail.Refresh(InitGridDetail);
            }
        }
    }

    #region 主檔 相關

    void ucGridMain_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                _IsADD = true;
                _RuleID = "";
                Refresh(true, true);
                break;
            case "cmdCopy":
                _IsADD = true;
                _RuleID = e.DataKeys[0];
                Refresh(true, true);
                break;
            case "cmdDelete":
                _IsADD = false;
                _RuleID = "";
                _ChkGrpNo = 0;
                _ChkSeqNo = 0;

                try
                {
                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    dicKey.Clear();
                    dicKey.Add("RuleID", e.DataKeys[0]);
                    AclExpress.IsAclTableLog("AclRule", dicKey, LogHelper.AppTableLogType.Delete);
                    AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Delete);

                    sb.Reset();
                    sb.AppendStatement("Delete AclRuleExp  Where RuleID = ").AppendParameter("RuleID", e.DataKeys[0]);
                    sb.AppendStatement("Delete AclRule     Where RuleID = ").AppendParameter("RuleID", e.DataKeys[0]);
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdEdit":
            case "cmdSelect":
                _IsADD = false;
                _RuleID = e.DataKeys[0];
                _ChkGrpNo = 0;
                _ChkSeqNo = 0;

                Refresh(false, true);
                break;
            case "cmdDownload":
                //Data Dump 2016.11.08
                string strHeader = string.Format("[{0}] 資料轉儲 SQL", e.DataKeys[0]);
                string strDataFilter = string.Format(" RuleID = '{0}' ", e.DataKeys[0]);
                string strDumpSQL = string.Empty;

                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("/* {0}     on [{1}] */ \n", strHeader, DateTime.Now);
                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("Use {0} \nGo\n", AclExpress._AclDBName);
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* AclRule    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(AclExpress._AclDBName, "AclRule", strDataFilter);
                strDumpSQL += "\n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* AclRuleExp    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(AclExpress._AclDBName, "AclRuleExp", strDataFilter);

                txtDumpSQL.ucTextData = strDumpSQL;
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 850;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucPopupHeader = strHeader;
                ucModalPopup1.ucPanelID = pnlDumpSQL.ID;
                ucModalPopup1.Show();

                break;
            default:
                Util.MsgBox(string.Format("cmd=[{0}],key=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
                break;
        }
    }

    protected void fmMainRefresh()
    {
        DbHelper db = new DbHelper(_DBName);
        DataTable dt = null;

        if (!string.IsNullOrEmpty(_RuleID))
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement("Select * From AclRule Where RuleID = ").AppendParameter("RuleID", _RuleID);
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        }

        if (_IsADD)
        {
            // Add,Copy
            fmMain.ChangeMode(FormViewMode.Insert);
            fmMain.DataSource = dt;
            fmMain.DataBind();
        }
        else
        {
            //Edit
            fmMain.ChangeMode(FormViewMode.Edit);
            fmMain.DataSource = dt;
            fmMain.DataBind();
        }

        Util.setJS_AlertDirtyData(fmMain);
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        Util_ucTextBox oText;
        CheckBox oChk;
        DataRow dr = null;
        string strObjID = "";

        if (fmMain.DataSource != null)
        {
            dr = ((DataTable)fmMain.DataSource).Rows[0];
        }

        // Add,Copy
        if (fmMain.CurrentMode == FormViewMode.Insert)
        {
            Util.setJS_AlertPageNotValid("btnMainInsert");

            strObjID = "RuleID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim() + "Copy";

            strObjID = "RuleName";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

        }

        // Edit
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            Util.setJS_AlertPageNotValid("btnMainUpdate");

            strObjID = "RuleID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "RuleName";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            Button oBtn = (Button)fmMain.FindControl("btnMainDelete");
            if (oBtn != null)
            {
                Util.ConfirmBox(oBtn, RS.Resources.Msg_Confirm_Delete);
            }
        }
    }

    protected void btnMainInsert_Click(object sender, EventArgs e)
    {
        //取得表單輸入資料
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Insert AclRule ");
        sb.Append("( ");
        sb.Append("  RuleID,RuleName,IsEnabled,Remark,UpdUser,UpdDateTime");
        sb.Append(" ) Values (");
        sb.Append("   ").AppendParameter("RuleID", oDic["RuleID"]);
        sb.Append("  ,").AppendParameter("RuleName", oDic["RuleName"]);
        sb.Append("  ,").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append("  ,").AppendParameter("Remark", oDic["Remark"]);
        sb.Append("  ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append("  ,").AppendDbDateTime();
        sb.Append("  )");

        if (_IsADD && _RuleID != oDic["RuleID"])
        {
            //Copy Detail
            sb.AppendStatement(Util.getDataCopySQL(AclExpress._AclDBName, _Main_KeyList, _RuleID.Split(','), oDic["RuleID"].Split(','), "AclRuleExp".Split(',')));
        }

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                _IsADD = false;
                _RuleID = oDic["RuleID"];

                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("RuleID", _RuleID);
                AclExpress.IsAclTableLog("AclRule", dicKey, LogHelper.AppTableLogType.Create);
                AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Create);

                Util.NotifyMsg(string.Format("[{0}]新增成功，可進行後續編修。", oDic["RuleID"]), Util.NotifyKind.Success);
                Refresh(true, true);
            }
            else
            {
                Util.NotifyMsg("新增失敗", Util.NotifyKind.Error);
            }
        }
        catch
        {
            Util.NotifyMsg("新增錯誤，請檢查資料是否重複", Util.NotifyKind.Error);
        }
    }

    protected void btnMainUpdate_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();

        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);
        sb.Reset();
        sb.AppendStatement("Update AclRule Set ");
        sb.Append("  RuleName = ").AppendParameter("RuleName", oDic["RuleName"]);
        sb.Append(", IsEnabled  = ").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append(", Remark   = ").AppendParameter("Remark", oDic["Remark"]);
        sb.Append(", UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append("  Where RuleID = ").AppendParameter("RuleID", _RuleID);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("RuleID", _RuleID);
                AclExpress.IsAclTableLog("AclRule", dicKey, LogHelper.AppTableLogType.Update);

                Util.NotifyMsg("更新成功", Util.NotifyKind.Success);
                _IsADD = false;
                _RuleID = "";
                Refresh(false, true);
            }
        }
        catch (Exception ex)
        {
            string strErrMsg = "更新主檔失敗";
            //將錯誤記錄到 Log 模組
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            Util.NotifyMsg(strErrMsg, Util.NotifyKind.Error);
            //錯誤發生時，因為頁面需保留目前主檔表單輸入的內容，故只更新明細清單清單
            ucGridDetail.Refresh();
        }
    }

    protected void btnMainDelete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        try
        {
            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Clear();
            dicKey.Add("RuleID", _RuleID);
            AclExpress.IsAclTableLog("AclRule", dicKey, LogHelper.AppTableLogType.Delete);
            AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Delete);

            sb.Reset();
            sb.AppendStatement("Delete AclRuleExp   Where RuleID = ").AppendParameter("RuleID", _RuleID);
            sb.AppendStatement("Delete AclRule      Where RuleID = ").AppendParameter("RuleID", _RuleID);
            db.ExecuteNonQuery(sb.BuildCommand());
            Util.NotifyMsg("刪除成功", Util.NotifyKind.Success);
            _RuleID = "";
            _ChkGrpNo = 0;
            _ChkSeqNo = 0;
            Refresh(true, true);
        }
        catch
        {
            throw;
        }
    }

    protected void btnMainCancel_Click(object sender, EventArgs e)
    {
        _IsADD = false;
        _RuleID = "";
        Refresh();
    }
    #endregion


    #region 明細檔 相關

    void ucGridDetail_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        //承接明細檔　GridView　按鈕事件
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        string[] oDataKeys = e.DataKeys;
        if (oDataKeys != null)
        {
            _RuleID = oDataKeys[0];
            _ChkGrpNo = int.Parse(oDataKeys[1]);
            _ChkSeqNo = int.Parse(oDataKeys[2]);
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 640;
        ucModalPopup1.ucPopupHeight = 320;
        ucModalPopup1.ucPopupHeader = "條件設定";
        ucModalPopup1.ucPanelID = pnlDetailForm.ID;

        //處理按鈕命令
        switch (e.CommandName)
        {
            case "cmdAdd":
                fmDetail.ChangeMode(FormViewMode.Insert);
                fmDetail.DataSource = null;
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);
                ucModalPopup1.Show();
                break;
            case "cmdCopy":
                sb.Reset();
                sb.AppendStatement("Select * From AclRuleExp Where 0=0 ");
                sb.Append(" And RuleID = ").AppendParameter("RuleID", _RuleID);
                sb.Append(" And ChkGrpNo = ").AppendParameter("ChkGrpNo", _ChkGrpNo);
                sb.Append(" And ChkSeqNo = ").AppendParameter("ChkSeqNo", _ChkSeqNo);

                fmDetail.ChangeMode(FormViewMode.Insert);
                fmDetail.DataSource = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                sb.Reset();
                sb.AppendStatement("Select * From AclRuleExp Where 0=0 ");
                sb.Append(" And RuleID = ").AppendParameter("RuleID", _RuleID);
                sb.Append(" And ChkGrpNo = ").AppendParameter("ChkGrpNo", _ChkGrpNo);
                sb.Append(" And ChkSeqNo = ").AppendParameter("ChkSeqNo", _ChkSeqNo);

                fmDetail.ChangeMode(FormViewMode.Edit);
                fmDetail.DataSource = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                try
                {
                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    dicKey.Clear();
                    dicKey.Add("RuleID", _RuleID);
                    dicKey.Add("ChkGrpNo", _ChkGrpNo.ToString());
                    dicKey.Add("ChkSeqNo", _ChkSeqNo.ToString());
                    AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Delete);

                    sb.Reset();
                    sb.AppendStatement("Delete From AclRuleExp Where 0=0 ");
                    sb.Append(" And RuleID = ").AppendParameter("RuleID", _RuleID);
                    sb.Append(" And ChkGrpNo = ").AppendParameter("ChkGrpNo", _ChkGrpNo);
                    sb.Append(" And ChkSeqNo = ").AppendParameter("ChkSeqNo", _ChkSeqNo);
                    db.ExecuteNonQuery(sb.BuildCommand());
                    _ChkGrpNo = 0;
                    _ChkSeqNo = 0;
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdMultilingual":
                sb.Reset();
                sb.AppendStatement("Select ChkCodeMapFldName From AclRuleExp Where 0=0 ");
                sb.Append(" And RuleID = ").AppendParameter("RuleID", _RuleID);
                sb.Append(" And ChkGrpNo = ").AppendParameter("ChkGrpNo", _ChkGrpNo);
                sb.Append(" And ChkSeqNo = ").AppendParameter("ChkSeqNo", _ChkSeqNo);
                string strFldName = db.ExecuteDataSet(sb.BuildCommand()).Tables[0].Rows[0][0].ToString();

                if (string.IsNullOrEmpty(strFldName))
                {
                    Util.MsgBox("「對照表欄位」尚未設定！");
                }
                else
                {
                    ucModalPopup1.Reset();
                    ucModalPopup1.ucPopupWidth = 820;
                    ucModalPopup1.ucPopupHeight = 650;
                    ucModalPopup1.ucPopupHeader = "對照表資料維護";
                    ucModalPopup1.ucFrameURL = string.Format("{0}?DBName={1}&LogDBName={2}&TabName={3}&FldName={4}", Util._CodeMapAdminUrl, AclExpress._AclDBName, AclExpress._AclLogDBName, _RuleID, strFldName);
                    ucModalPopup1.Show();
                }
                break;
            default:
                //未定義的命令
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, e.CommandName));
                break;
        }
        fmMainRefresh();
    }

    protected void fmDetail_DataBound(object sender, EventArgs e)
    {
        //初始
        Util_ucTextBox oText;
        Util_ucCommSingleSelect oDdl;
        CheckBox oChk;
        string strObjID;
        DataRow dr = null;

        if (fmDetail.DataSource != null)
        {
            dr = ((DataTable)fmDetail.DataSource).Rows[0];
        }

        // Add,Copy
        if (fmDetail.CurrentMode == FormViewMode.Insert)
        {
            Util.setJS_AlertPageNotValid("btnDetailInsert");

            strObjID = "ChkGrpNo";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            oText.ucTextData = "1";
            if (dr != null) oText.ucTextData = (int.Parse("0" + dr[strObjID].ToString()) + 1).ToString();
            oText.Refresh();

            strObjID = "ChkSeqNo";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            oText.ucTextData = "1";
            if (dr != null) oText.ucTextData = (int.Parse("0" + dr[strObjID].ToString()) + 1).ToString();
            oText.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkOrgUserObjectProperty";
            oDdl = (Util_ucCommSingleSelect)fmDetail.FindControl(strObjID);
            oDdl.ucSourceDictionary = _Dic_ChkOrgUserObjectProperty;
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "ChkOrgUserSW";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkOrgUserExp";
            oDdl = (Util_ucCommSingleSelect)fmDetail.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_ChkOrgUserExp);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "ChkOrgUserPropertyValue";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ChkCodeMapSW";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkCodeMapFldName";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();
        }

        // Edit
        if (fmDetail.CurrentMode == FormViewMode.Edit)
        {
            Util.setJS_AlertPageNotValid("btnDetailUpdate");

            strObjID = "ChkGrpNo";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString();
            oText.Refresh();

            strObjID = "ChkSeqNo";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString();
            oText.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkOrgUserObjectProperty";
            oDdl = (Util_ucCommSingleSelect)fmDetail.FindControl(strObjID);
            oDdl.ucSourceDictionary = _Dic_ChkOrgUserObjectProperty;
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "ChkOrgUserSW";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkOrgUserExp";
            oDdl = (Util_ucCommSingleSelect)fmDetail.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_ChkOrgUserExp);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.Refresh();

            strObjID = "ChkOrgUserPropertyValue";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ChkCodeMapSW";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "ChkCodeMapFldName";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            //鍵值欄位需為唯讀
            for (int i = 0; i < _Detail_KeyList.Count(); i++)
            {
                oText = (Util_ucTextBox)fmDetail.FindControl(_Detail_KeyList[i]);
                if (oText != null)
                {
                    oText.ucIsReadOnly = true;
                    oText.Refresh();
                }
            }
        }
    }

    /// <summary>
    /// 確認新增明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailInsert_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmDetail);

        if (!string.IsNullOrEmpty(_RuleID))
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            //新增明細
            sb.AppendStatement("Insert AclRuleExp ");
            sb.Append("(RuleID,ChkGrpNo,ChkSeqNo,IsEnabled,ChkOrgUserObjectProperty,ChkOrgUserSW,ChkOrgUserExp,ChkOrgUserPropertyValue,ChkCodeMapSW,ChkCodeMapFldName,Remark,UpdUser,UpdDateTime) ");
            sb.Append(" Values (").AppendParameter("RuleID", _RuleID);
            sb.Append("        ,").AppendParameter("ChkGrpNo", oDic["ChkGrpNo"]);
            sb.Append("        ,").AppendParameter("ChkSeqNo", oDic["ChkSeqNo"]);
            sb.Append("        ,").AppendParameter("IsEnabled", oDic["IsEnabled"]);
            sb.Append("        ,").AppendParameter("ChkOrgUserObjectProperty", oDic["ChkOrgUserObjectProperty"]);

            sb.Append("        ,").AppendParameter("ChkOrgUserSW", oDic["ChkOrgUserSW"]);
            sb.Append("        ,").AppendParameter("ChkOrgUserExp", oDic["ChkOrgUserExp"]);
            sb.Append("        ,").AppendParameter("ChkOrgUserPropertyValue", oDic["ChkOrgUserPropertyValue"]);
            sb.Append("        ,").AppendParameter("ChkCodeMapSW", oDic["ChkCodeMapSW"]);
            sb.Append("        ,").AppendParameter("ChkCodeMapFldName", oDic["ChkCodeMapFldName"]);

            sb.Append("        ,").AppendParameter("Remark", oDic["Remark"]);
            sb.Append("        ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
            sb.Append("        ,").AppendDbDateTime();
            sb.Append(")");

            try
            {
                db.ExecuteNonQuery(sb.BuildCommand());
                _ChkGrpNo = int.Parse(oDic["ChkGrpNo"]);
                _ChkSeqNo = int.Parse(oDic["ChkSeqNo"]);

                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("RuleID", _RuleID);
                dicKey.Add("ChkGrpNo", _ChkGrpNo.ToString());
                dicKey.Add("ChkSeqNo", _ChkSeqNo.ToString());
                AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Create);

                Util.NotifyMsg("條件新增成功", Util.NotifyKind.Success);
            }
            catch
            {
                Util.NotifyMsg("條件新增失敗", Util.NotifyKind.Error);
            }

            Refresh(false, true);
        }
    }

    /// <summary>
    /// 取消新增明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailInsertCancel_Click(object sender, EventArgs e)
    {
        Refresh();
    }

    /// <summary>
    /// 確認更新明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmDetail);

        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        //更新明細
        sb.AppendStatement("Update AclRuleExp Set ");
        sb.Append("  IsEnabled   = ").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append(", ChkOrgUserObjectProperty   = ").AppendParameter("ChkOrgUserObjectProperty", oDic["ChkOrgUserObjectProperty"]);
        sb.Append(", ChkOrgUserSW               = ").AppendParameter("ChkOrgUserSW", oDic["ChkOrgUserSW"]);
        sb.Append(", ChkOrgUserExp              = ").AppendParameter("ChkOrgUserExp", oDic["ChkOrgUserExp"]);
        sb.Append(", ChkOrgUserPropertyValue    = ").AppendParameter("ChkOrgUserPropertyValue", oDic["ChkOrgUserPropertyValue"]);
        sb.Append(", ChkCodeMapSW               = ").AppendParameter("ChkCodeMapSW", oDic["ChkCodeMapSW"]);
        sb.Append(", ChkCodeMapFldName          = ").AppendParameter("ChkCodeMapFldName", oDic["ChkCodeMapFldName"]);
        sb.Append(", Remark      = ").AppendParameter("Remark", oDic["Remark"]);
        sb.Append(", UpdUser     = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append(" Where 0=0 ");
        sb.Append(" And RuleID   =").AppendParameter("RuleID", _RuleID);
        sb.Append(" And ChkGrpNo =").AppendParameter("ChkGrpNo", _ChkGrpNo);
        sb.Append(" And ChkSeqNo =").AppendParameter("ChkSeqNo", _ChkSeqNo);

        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());

            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Clear();
            dicKey.Add("RuleID", _RuleID);
            dicKey.Add("ChkGrpNo", _ChkGrpNo.ToString());
            dicKey.Add("ChkSeqNo", _ChkSeqNo.ToString());
            AclExpress.IsAclTableLog("AclRuleExp", dicKey, LogHelper.AppTableLogType.Update);

            Util.NotifyMsg("條件更新成功", Util.NotifyKind.Success);
        }
        catch
        {
            Util.NotifyMsg("條件更新失敗", Util.NotifyKind.Error);
        }

        Refresh();
    }

    /// <summary>
    /// 取消更新明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailUpdateCancel_Click(object sender, EventArgs e)
    {
        Refresh();
    }
    #endregion

}