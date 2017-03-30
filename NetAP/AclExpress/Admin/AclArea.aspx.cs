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

public partial class AclExpress_Admin_AclArea : SecurePage
{
    #region 頁面共用

    //資料庫來源
    private string _DBName = AclExpress._AclDBName;
    //主檔鍵值欄位
    private string[] _Main_KeyList = "AreaID".Split(',');
    //明細檔鍵值欄位
    private string[] _Detail_KeyList = "AreaID,GrantID".Split(',');

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

    //主檔 AreaID 值
    private string _AreaID
    {
        get
        {
            if (ViewState["_AreaID"] == null) { ViewState["_AreaID"] = ""; }
            return (string)(ViewState["_AreaID"]);
        }
        set
        {
            ViewState["_AreaID"] = value;
        }
    }

    //明細 GrantID 值
    private string _GrantID
    {
        get
        {
            if (ViewState["_GrantID"] == null) { ViewState["_GrantID"] = ""; }
            return (string)(ViewState["_GrantID"]);
        }
        set
        {
            ViewState["_GrantID"] = value;
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
            _AreaID = "";
            _GrantID = "";

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
        ucGridMain.ucDataQrySQL = "Select * From AclArea ";
        ucGridMain.ucDataKeyList = _Main_KeyList;
        ucGridMain.ucDefSortExpression = _Main_KeyList[0];
        ucGridMain.ucDefSortDirection = "Asc";
        Dictionary<string, string> oDicMain = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicMain.Clear();
        oDicMain.Add("AreaID", "區域代號");
        oDicMain.Add("AreaName", "區域名稱");
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
        ucGridDetail.ucDataQrySQL = string.Format("Select * From AclAreaGrantList Where AreaID = '{0}' ", (string.IsNullOrEmpty(_AreaID)) ? "---" : _AreaID);
        ucGridDetail.ucDataKeyList = _Detail_KeyList;
        ucGridDetail.ucDefSortExpression = _Detail_KeyList[1];
        ucGridDetail.ucDefSortDirection = "Asc";
        Dictionary<string, string> oDicDetail = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicDetail.Clear();
        oDicDetail.Add("GrantID", "項目代號");
        oDicDetail.Add("IsEnabled", "啟用@Y");
        oDicDetail.Add("GrantName", "項目名稱");
        ucGridDetail.ucDataDisplayDefinition = oDicDetail;

        ucGridDetail.ucAddEnabled = true;
        ucGridDetail.ucCopyEnabled = true;
        ucGridDetail.ucSelectEnabled = false;
        ucGridDetail.ucEditEnabled = true;
        ucGridDetail.ucDeleteEnabled = true;

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
            if (string.IsNullOrEmpty(_AreaID))
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
                _AreaID = "";
                Refresh(true, true);
                break;
            case "cmdCopy":
                _IsADD = true;
                _AreaID = e.DataKeys[0];
                Refresh(true, true);
                break;
            case "cmdDelete":
                try
                {
                    _IsADD = false;
                    _AreaID = "";
                    _GrantID = "";

                    Dictionary<string, string> dicKey = new Dictionary<string, string>();
                    dicKey.Clear();
                    dicKey.Add("AreaID", e.DataKeys[0]);
                    AclExpress.IsAclTableLog("AclArea", dicKey, LogHelper.AppTableLogType.Delete);
                    AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Delete);

                    sb.Reset();
                    sb.AppendStatement("Delete AclAreaGrantList Where AreaID = ").AppendParameter("AreaID", e.DataKeys[0]);
                    sb.AppendStatement("Delete AclArea          Where AreaID = ").AppendParameter("AreaID", e.DataKeys[0]);
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
                _AreaID = e.DataKeys[0];
                _GrantID = "";
                Refresh(false, true);
                break;
            case "cmdDownload":
                //Data Dump 2016.11.08
                string strHeader = string.Format("[{0}] 資料轉儲 SQL", e.DataKeys[0]);
                string strDataFilter = string.Format(" AreaID = '{0}' ", e.DataKeys[0]);
                string strDumpSQL = string.Empty;

                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("/* {0}     on [{1}] */ \n", strHeader, DateTime.Now);
                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("Use {0} \nGo\n", AclExpress._AclDBName);
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* AclArea    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(AclExpress._AclDBName, "AclArea", strDataFilter);
                strDumpSQL += "\n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* AclAreaGrantList    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(AclExpress._AclDBName, "AclAreaGrantList", strDataFilter);

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

        if (!string.IsNullOrEmpty(_AreaID))
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement("Select * From AclArea Where AreaID = ").AppendParameter("AreaID", _AreaID);
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

            strObjID = "AreaID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim() + "Copy";

            strObjID = "AreaName";
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

            strObjID = "AreaID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "AreaName";
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
        sb.AppendStatement("Insert AclArea ");
        sb.Append("(AreaID,AreaName,IsEnabled,Remark,UpdUser,UpdDateTime)");
        sb.Append(" Values (").AppendParameter("AreaID", oDic["AreaID"]);
        sb.Append("        ,").AppendParameter("AreaName", oDic["AreaName"]);
        sb.Append("        ,").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append("        ,").AppendParameter("Remark", oDic["Remark"]);
        sb.Append("        ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append("        ,").AppendDbDateTime();
        sb.Append("        )");
        
        if (_IsADD && _AreaID != oDic["AreaID"]) 
        { 
            //Copy Detail
            sb.AppendStatement(Util.getDataCopySQL(AclExpress._AclDBName,_Main_KeyList, _AreaID.Split(','),oDic["AreaID"].Split(','),"AclAreaGrantList".Split(',')));
        }

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                _IsADD = false;
                _AreaID = oDic["AreaID"];

                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("AreaID", _AreaID);
                AclExpress.IsAclTableLog("AclArea", dicKey, LogHelper.AppTableLogType.Create);
                AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Create);

                Util.NotifyMsg(string.Format("[{0}]新增成功，可進行後續編修。", oDic["AreaID"]), Util.NotifyKind.Success);
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
        sb.AppendStatement("Update AclArea Set ");
        sb.Append("  AreaName = ").AppendParameter("AreaName", oDic["AreaName"]);
        sb.Append(", IsEnabled  = ").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append(", Remark   = ").AppendParameter("Remark", oDic["Remark"]);
        sb.Append(", UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append("  Where AreaID = ").AppendParameter("AreaID", _AreaID);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("AreaID", _AreaID);
                AclExpress.IsAclTableLog("AclArea", dicKey, LogHelper.AppTableLogType.Update);
                
                _IsADD = false;
                //_AreaID = "";

                Util.NotifyMsg("更新成功", Util.NotifyKind.Success);
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
            dicKey.Add("AreaID", _AreaID);
            AclExpress.IsAclTableLog("AclArea", dicKey, LogHelper.AppTableLogType.Delete);
            AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Delete);

            sb.Reset();
            sb.AppendStatement("Delete AclAreaGrantList Where AreaID = ").AppendParameter("AreaID", _AreaID);
            sb.AppendStatement("Delete AclArea       Where AreaID = ").AppendParameter("AreaID", _AreaID);
            db.ExecuteNonQuery(sb.BuildCommand());
            Util.NotifyMsg("刪除成功", Util.NotifyKind.Success);
            _AreaID = "";
            _GrantID = "";
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
        _AreaID = "";
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
            _AreaID = oDataKeys[0];
            _GrantID = oDataKeys[1];
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 540;
        ucModalPopup1.ucPopupHeight = 250;
        ucModalPopup1.ucPopupHeader = "項目設定";
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
                sb.AppendStatement("Select * From AclAreaGrantList Where 0=0 ");
                sb.Append(" And AreaID = ").AppendParameter("AreaID", _AreaID);
                sb.Append(" And GrantID = ").AppendParameter("GrantID", _GrantID);

                fmDetail.ChangeMode(FormViewMode.Insert);
                fmDetail.DataSource = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                sb.Reset();
                sb.AppendStatement("Select * From AclAreaGrantList Where 0=0 ");
                sb.Append(" And AreaID = ").AppendParameter("AreaIS", _AreaID);
                sb.Append(" And GrantID = ").AppendParameter("GrantID", _GrantID);

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
                    dicKey.Add("AreaID", _AreaID);
                    dicKey.Add("GrantID", _GrantID);
                    AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Delete);

                    sb.Reset();
                    sb.AppendStatement("Delete From AclAreaGrantList Where 0=0 ");
                    sb.Append(" And AreaID = ").AppendParameter("AreaIS", _AreaID);
                    sb.Append(" And GrantID = ").AppendParameter("GrantID", _GrantID);
                    db.ExecuteNonQuery(sb.BuildCommand());
                    _GrantID = "";
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
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

            strObjID = "GrantID";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim() + "-Copy";

            strObjID = "GrantName";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();
        }

        // Edit
        if (fmDetail.CurrentMode == FormViewMode.Edit)
        {
            Util.setJS_AlertPageNotValid("btnDetailUpdate");

            strObjID = "GrantID";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "GrantName";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmDetail.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().ToUpper() == "Y") ? true : false;

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

        if (!string.IsNullOrEmpty(_AreaID))
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            //新增明細
            sb.AppendStatement("Insert AclAreaGrantList ");
            sb.Append("(AreaID,GrantID,GrantName,IsEnabled,Remark,UpdUser,UpdDateTime) ");
            sb.Append(" Values (").AppendParameter("AreaID", _AreaID);
            sb.Append("        ,").AppendParameter("GrantID", oDic["GrantID"]);
            sb.Append("        ,").AppendParameter("GrantName", oDic["GrantName"]);
            sb.Append("        ,").AppendParameter("IsEnabled", oDic["IsEnabled"]);
            sb.Append("        ,").AppendParameter("Remark", oDic["Remark"]);
            sb.Append("        ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
            sb.Append("        ,").AppendDbDateTime();
            sb.Append(")");

            try
            {
                db.ExecuteNonQuery(sb.BuildCommand());
                _GrantID = oDic["GrantID"];

                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                dicKey.Clear();
                dicKey.Add("AreaID", _AreaID);
                dicKey.Add("GrantID", _GrantID);
                AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Create);

                Util.NotifyMsg("項目新增成功", Util.NotifyKind.Success);
                Refresh(false, true);
            }
            catch
            {
                Util.NotifyMsg("項目新增失敗", Util.NotifyKind.Error);
            }

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
        sb.AppendStatement("Update AclAreaGrantList Set ");
        sb.Append("  GrantName   = ").AppendParameter("GrantName", oDic["GrantName"]);
        sb.Append(", IsEnabled   = ").AppendParameter("IsEnabled", oDic["IsEnabled"]);
        sb.Append(", Remark      = ").AppendParameter("Remark", oDic["Remark"]);
        sb.Append(", UpdUser     = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append(" Where 0=0 ");
        sb.Append(" And AreaID  =").AppendParameter("AreaID", _AreaID);
        sb.Append(" And GrantID =").AppendParameter("POSeq", _GrantID);

        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            Util.NotifyMsg("項目更新成功", Util.NotifyKind.Success);

            Dictionary<string, string> dicKey = new Dictionary<string, string>();
            dicKey.Clear();
            dicKey.Add("AreaID", _AreaID);
            dicKey.Add("GrantID", _GrantID);
            AclExpress.IsAclTableLog("AclAreaGrantList", dicKey, LogHelper.AppTableLogType.Update);

            Refresh();
        }
        catch
        {
            Util.NotifyMsg("項目更新失敗", Util.NotifyKind.Error);
        }
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