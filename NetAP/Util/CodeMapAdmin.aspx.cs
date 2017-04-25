using System;
using System.Collections.Generic;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [CodeMap]維護公用程式
/// <para>**可用參數：DBName , LogDBName, TableName , TabName , FldName **</para>
/// <para>**若 LogDBName 有值，則支援 [_LOG] 方式儲存資料異動Log</para>
/// </summary>
public partial class Util_CodeMapAdmin : SecurePage
{

    #region 屬性定義
    //所在資料庫
    protected string _DBName
    {
        get
        {
            if (PageViewState["_DBName"] == null)
            {
                PageViewState["_DBName"] = Util.getRequestQueryStringKey("DBName");
            }
            return (string)(PageViewState["_DBName"]);
        }
        set
        {
            PageViewState["_DBName"] = value;
        }
    }

    //Log所在資料庫 2016.06.28 
    protected string _LogDBName
    {
        get
        {
            if (PageViewState["_LogDBName"] == null)
            {
                PageViewState["_LogDBName"] = Util.getRequestQueryStringKey("LogDBName");
            }
            return (string)(PageViewState["_LogDBName"]);
        }
        set
        {
            PageViewState["_LogDBName"] = value;
        }
    }

    //指定對照表的資料表名稱來源
    protected string _TableName
    {
        get
        {
            if (PageViewState["_TableName"] == null)
            {
                PageViewState["_TableName"] = Util.getRequestQueryStringKey("TableName");
            }
            return (string)(PageViewState["_TableName"]);
        }
        set
        {
            PageViewState["_TableName"] = value;
        }
    }

    //指定 TabName 
    protected string _TabName
    {
        get
        {
            if (PageViewState["_TabName"] == null)
            {
                PageViewState["_TabName"] = Util.getRequestQueryStringKey("TabName");
            }
            return (string)(PageViewState["_TabName"]);
        }
        set
        {
            PageViewState["_TabName"] = value;
        }
    }

    //指定 FldName
    protected string _FldName
    {
        get
        {
            if (PageViewState["_FldName"] == null)
            {
                PageViewState["_FldName"] = Util.getRequestQueryStringKey("FldName");
            }
            return (string)(PageViewState["_FldName"]);
        }
        set
        {
            PageViewState["_FldName"] = value;
        }
    }

    //查詢條件基底 SQL
    private string _QryBaseSQL = @"Select *  From {0} ";

    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (PageViewState["_QryResultSQL"] == null)
            {
                PageViewState["_QryResultSQL"] = string.Format(_QryBaseSQL, _TableName)
                    + ((!string.IsNullOrEmpty(_TabName)) ? string.Format(" Where TabName = '{0}' ", _TabName) : "")
                    + ((!string.IsNullOrEmpty(_FldName)) ? string.Format(" And   FldName = '{0}' ", _FldName) : "");
            }
            return (string)(PageViewState["_QryResultSQL"]);
        }
        set
        {
            PageViewState["_QryResultSQL"] = value;
        }
    }

    protected Dictionary<string, string> _Dic_TabList
    {
        get
        {
            if (PageViewState["_Dic_TabList"] != null)
            {
                return (Dictionary<string, string>)PageViewState["_Dic_TabList"];
            }
            else
            {
                DbHelper db = new DbHelper(_DBName);
                PageViewState["_Dic_TabList"] = Util.getDictionary(db.ExecuteDataSet(string.Format("Select distinct TabName,TabName as 'Caption' From {0} ", _TableName)).Tables[0]);
                return (Dictionary<string, string>)PageViewState["_Dic_TabList"];
            }
        }
    }
    #endregion

    //可支援的對照表名稱清單
    protected string[] _CodeMapNameList = "PU_CodeMap,EP_CodeMap,CodeMap".Split(',');
    //基礎查詢SQL
    protected string _MainQrySQL = "Select TabName,FldName,Value,Description,Remark,UpdUser,UpdTime From {0} Where 0=0 ";
    //鍵值欄位清單
    protected string _PKFieldList = "TabName,FldName,Value";

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setRequestValidatorBypassIDList("*"); //2016.11.08
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(_TableName))
            {
                //利用 _CodeMapList 自動判定可用的對照資料表名稱
                DbHelper db = new DbHelper(_DBName);
                DataTable dt = db.ExecuteDataSet("Select Top 1 Name from sysobjects Where xtype = 'U' and name in ('" + Util.getStringJoin(_CodeMapNameList, "','") + "')").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    _TableName = dt.Rows[0][0].ToString();
                }
            }

            if (string.IsNullOrEmpty(_TableName))
            {
                labErrMsg.Visible = true;
                labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound);
                ucGridView1.Visible = false;
            }
            else
            {
                //初始
                qryTabName.ucCaption = RS.Resources.CodeMap_TabName;
                qryFldName.ucCaption = RS.Resources.CodeMap_FldName;
                qryValue.ucCaption = RS.Resources.CodeMap_Value;
                qryDescription.ucCaption = RS.Resources.CodeMap_Description;
                qryRemark.ucCaption = RS.Resources.CodeMap_Remark;

                qryTabName.ucSourceDictionary = _Dic_TabList;
                qryTabName.ucSelectedID = "";
                ucGridView1.ucDBName = _DBName;
                ucGridView1.ucDataQrySQL = _QryResultSQL;
                ucGridView1.ucDataKeyList = _PKFieldList.Split(',');
                Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
                dicDisplay.Add("FldName", RS.Resources.CodeMap_FldName);
                dicDisplay.Add("Value", RS.Resources.CodeMap_Value);
                dicDisplay.Add("Description", RS.Resources.CodeMap_Description);
                dicDisplay.Add("Remark", RS.Resources.CodeMap_Remark);
                dicDisplay.Add("UpdUser", RS.Resources.CodeMap_UpdUser);
                dicDisplay.Add("UpdTime", RS.Resources.CodeMap_UpdTime + "@T");
                ucGridView1.ucDataDisplayDefinition = dicDisplay;

                if (string.IsNullOrEmpty(_TabName))
                {
                    ucGridView1.ucDataGroupKey = "TabName";
                }

                ucGridView1.ucAddEnabled = true;
                ucGridView1.ucCopyEnabled = true;
                ucGridView1.ucEditEnabled = true;
                ucGridView1.ucDeleteEnabled = true;
                ucGridView1.ucMultilingualEnabled = true;
                ucGridView1.ucDataDumpEnabled = true;  //2016.11.08
                ucGridView1.ucDataDumpToolTip += "，會參照 「對照資料表」 自動篩選資料";

                ucGridView1.Refresh(true);
            }
        }

        if (!string.IsNullOrEmpty(_TabName))
        {
            if (_Dic_TabList.ContainsKey(_TabName))  //2016.06.22 新增檢核
            {
                qryTabName.ucSelectedID = _TabName;
            }
            qryTabName.ucIsReadOnly = true;
        }
        qryTabName.Refresh();

        if (!string.IsNullOrEmpty(_FldName))
        {
            qryFldName.ucTextData = _FldName;
            qryFldName.ucIsReadOnly = true;
        }

        //事件訂閱
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        ucGridView1.Refresh();
    }

    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();

        string strDataKeys = (e.DataKeys != null) ? Util.getStringJoin(e.DataKeys) : "";
        Dictionary<string, string> dicContext = new Dictionary<string, string>();
        switch (e.CommandName)
        {
            case "cmdAdd":
                dicContext.Clear();
                dicContext.Add("Mode", "Add");
                dicContext.Add("DataKeys", strDataKeys);
                LaunchPopup(dicContext);
                break;
            case "cmdEdit":
                dicContext.Clear();
                dicContext.Add("Mode", "Edit");
                dicContext.Add("DataKeys", strDataKeys);
                LaunchPopup(dicContext);
                break;
            case "cmdCopy":
                dicContext.Clear();
                dicContext.Add("Mode", "Copy");
                dicContext.Add("DataKeys", strDataKeys);
                LaunchPopup(dicContext);
                break;
            case "cmdDelete":
                if (string.IsNullOrEmpty(strDataKeys))
                {
                    Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.ParaDataError));
                    return;
                }
                else
                {
                    try
                    {
                        //2016.06.28 新增
                        if (!string.IsNullOrEmpty(_LogDBName))
                        {
                            Dictionary<string, string> dicKey = new Dictionary<string, string>();
                            dicKey.Clear();
                            dicKey.Add("TabName", strDataKeys.Split(',')[0]);
                            dicKey.Add("FldName", strDataKeys.Split(',')[1]);
                            dicKey.Add("Value", strDataKeys.Split(',')[2]);
                            LogHelper.WriteAppTableLog(_DBName, _LogDBName, _TableName, dicKey, LogHelper.AppTableLogType.Delete);
                        }

                        sb.Reset();
                        sb.AppendStatement(string.Format("Delete {0} ", _TableName));
                        sb.Append(" Where TabName = ").AppendParameter("TabName", strDataKeys.Split(',')[0]);
                        sb.Append(" And   FldName = ").AppendParameter("FldName", strDataKeys.Split(',')[1]);
                        sb.Append(" And   Value   = ").AppendParameter("Value", strDataKeys.Split(',')[2]);
                        db.ExecuteNonQuery(sb.BuildCommand());
                        Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);

                        PageViewState["_Dic_TabList"] = null;
                        qryTabName.ucSourceDictionary = _Dic_TabList;
                        qryTabName.ucSelectedID = "";
                        if (!string.IsNullOrEmpty(_TabName))
                        {
                            if (_Dic_TabList.ContainsKey(_TabName))
                            {
                                qryTabName.ucSelectedID = _TabName;
                            }
                            qryTabName.ucIsReadOnly = true;
                        }

                        qryTabName.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.Error, ex.ToString()));
                    }
                }
                break;
            case "cmdMultilingual":
                string strURL = string.Format("{0}?DBName={1}&TableName={2}&PKFieldList={3}&PKValueList={4}", Util._MuiAdminUrl, _DBName, _TableName, _PKFieldList, Util.getStringJoin(e.DataKeys));
                ucModalPopup1.Reset();
                ucModalPopup1.ucFrameURL = strURL;
                ucModalPopup1.ucPopupWidth = 650;
                ucModalPopup1.ucPopupHeight = 350;
                ucModalPopup1.ucBtnCloselEnabled = true;
                ucModalPopup1.Show();
                break;
            case "cmdDataDump":
                //Data Dump 2016.11.08
                string tmpKey = (!string.IsNullOrEmpty(_TabName)) ? _TabName : qryTabName.ucSelectedID;
                string strHeader = string.Format("[CodeMap] {0} 資料轉儲 SQL", (!string.IsNullOrEmpty(tmpKey)) ? "[" + tmpKey + "]" : "");
                string strDataFilter = (!string.IsNullOrEmpty(tmpKey)) ? string.Format(" TabName = '{0}' ", tmpKey) : "";
                string strDumpSQL = string.Empty;

                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("/* {0}     on [{1}] */ \n", strHeader, DateTime.Now);
                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("Use {0} \nGo\n", (string.IsNullOrEmpty(_DBName)) ? Util.getAppSetting("app://CfgDefConnDB/") : _DBName);
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* CodeMap    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL((string.IsNullOrEmpty(_DBName)) ? Util.getAppSetting("app://CfgDefConnDB/") : _DBName, _TableName, strDataFilter);

                txtDumpSQL.ucTextData = strDumpSQL;
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 850;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucPopupHeader = strHeader;
                ucModalPopup1.ucPanelID = pnlDumpSQL.ID;
                ucModalPopup1.Show();

                break;

            default:
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, e.CommandName));
                break;
        }


    }

    protected void LaunchPopup(Dictionary<string, string> oContext)
    {
        TabName.ucCaption = RS.Resources.CodeMap_TabName;
        FldName.ucCaption = RS.Resources.CodeMap_FldName;
        Value.ucCaption = RS.Resources.CodeMap_Value;
        Description.ucCaption = RS.Resources.CodeMap_Description;
        Remark.ucCaption = RS.Resources.CodeMap_Remark;

        TabName.ucIsRequire = true;
        FldName.ucIsRequire = true;
        Value.ucIsRequire = true;
        Description.ucIsRequire = true;

        TabName.ucIsReadOnly = false;
        FldName.ucIsReadOnly = false;
        Value.ucIsReadOnly = false;
        TabName.ucTextData = "";
        FldName.ucTextData = "";
        Value.ucTextData = "";
        Description.ucTextData = "";
        Remark.ucTextData = "";

        if (!string.IsNullOrEmpty(oContext["DataKeys"]))
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(string.Format(_MainQrySQL, _TableName));
            sb.Append(" And TabName = ").AppendParameter("TabName", oContext["DataKeys"].Split(',')[0]);
            sb.Append(" And FldName = ").AppendParameter("FldName", oContext["DataKeys"].Split(',')[1]);
            sb.Append(" And Value   = ").AppendParameter("Value", oContext["DataKeys"].Split(',')[2]);
            DataRow dr = db.ExecuteDataSet(sb.BuildCommand()).Tables[0].Rows[0];

            TabName.ucTextData = dr["TabName"].ToString();
            FldName.ucTextData = dr["FldName"].ToString();
            Value.ucTextData = dr["Value"].ToString();
            Description.ucTextData = dr["Description"].ToString();
            Remark.ucTextData = dr["Remark"].ToString();

            TabName.ucIsReadOnly = (oContext["Mode"] != "Copy") ? true : false;
            FldName.ucIsReadOnly = (oContext["Mode"] != "Copy") ? true : false;
            Value.ucIsReadOnly = (oContext["Mode"] != "Copy") ? true : false;

            if (!string.IsNullOrEmpty(_TabName))
            {
                TabName.ucTextData = _TabName;
                TabName.ucIsReadOnly = true;
            }

            if (!string.IsNullOrEmpty(_FldName))
            {
                FldName.ucTextData = _FldName;
                FldName.ucIsReadOnly = true;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(_TabName))
            {
                TabName.ucTextData = _TabName;
                TabName.ucIsReadOnly = true;
            }

            if (!string.IsNullOrEmpty(_FldName))
            {
                FldName.ucTextData = _FldName;
                FldName.ucIsReadOnly = true;
            }

        }

        TabName.Refresh();
        FldName.Refresh();
        Value.Refresh();

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 580;
        ucModalPopup1.ucPopupHeight = 320;
        ucModalPopup1.ucContextData = Util.getJSON(oContext);
        ucModalPopup1.ucPanelID = pnlEdit.ID;
        ucModalPopup1.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();

        Dictionary<string, string> oContext = Util.getDictionary(ucModalPopup1.ucContextData);
        Dictionary<string, string> oData = Util.getControlEditResult(pnlEdit);
        switch (oContext["Mode"].ToUpper())
        {
            case "ADD":
            case "COPY":
                sb.Reset();
                sb.AppendStatement(string.Format("Insert {0} ", _TableName));
                sb.Append("(TabName,FldName,Value,Description,Remark,UpdUser,UpdTime)");
                sb.Append(" Values (").AppendParameter("TabName", oData["TabName"]);
                sb.Append("        ,").AppendParameter("FldName", oData["FldName"]);
                sb.Append("        ,").AppendParameter("Value", oData["Value"]);
                sb.Append("        ,").AppendParameter("Description", oData["Description"]);
                sb.Append("        ,").AppendParameter("Remark", oData["Remark"]);
                sb.Append("        ,").AppendParameter("UpdUser", oUser.UserID);
                sb.Append("        ,").AppendDbDateTime();
                sb.Append(")");
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());

                    //2016.06.28 新增
                    if (!string.IsNullOrEmpty(_LogDBName))
                    {
                        Dictionary<string, string> dicKey = new Dictionary<string, string>();
                        dicKey.Clear();
                        dicKey.Add("TabName", oData["TabName"]);
                        dicKey.Add("FldName", oData["FldName"]);
                        dicKey.Add("Value", oData["Value"]);
                        LogHelper.WriteAppTableLog(_DBName, _LogDBName, _TableName, dicKey, LogHelper.AppTableLogType.Create);
                    }

                    Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
                }
                break;
            case "EDIT":
                sb.Reset();
                sb.AppendStatement(string.Format(" Update {0} ", _TableName));
                sb.Append(" Set Description = ").AppendParameter("Description", oData["Description"]);
                sb.Append("    ,Remark = ").AppendParameter("Remark", oData["Remark"]);
                sb.Append("    ,UpdUser = ").AppendParameter("UpdUser", oUser.UserID);
                sb.Append("    ,UpdTime = ").AppendDbDateTime();
                sb.Append(" Where TabName = ").AppendParameter("TabName", oData["TabName"]);
                sb.Append("   and FldName = ").AppendParameter("FldName", oData["FldName"]);
                sb.Append("   and Value   = ").AppendParameter("Value", oData["Value"]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());

                    //2016.06.28 新增
                    if (!string.IsNullOrEmpty(_LogDBName))
                    {
                        Dictionary<string, string> dicKey = new Dictionary<string, string>();
                        dicKey.Clear();
                        dicKey.Add("TabName", oData["TabName"]);
                        dicKey.Add("FldName", oData["FldName"]);
                        dicKey.Add("Value", oData["Value"]);
                        LogHelper.WriteAppTableLog(_DBName, _LogDBName, _TableName, dicKey, LogHelper.AppTableLogType.Update);
                    }

                    Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteSysLog(ex);
                    Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
                }
                break;
            default:
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, oContext["Mode"]));
                break;
        }
        PageViewState["_Dic_TabList"] = null;
        qryTabName.ucSourceDictionary = _Dic_TabList;
        qryTabName.ucSelectedID = "";
        if (!string.IsNullOrEmpty(_TabName))
        {
            if (_Dic_TabList.ContainsKey(_TabName))
            {
                qryTabName.ucSelectedID = _TabName;
            }
            qryTabName.ucIsReadOnly = true;
        }
        qryTabName.Refresh();
        ucGridView1.Refresh(true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ucGridView1.Refresh();
    }

    protected void btnQry_Click(object sender, EventArgs e)
    {
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName) + " Where 0 = 0 ";

        _QryResultSQL += Util.getDataQueryConditionSQL("TabName", "=", qryTabName);

        if (qryIsLikeFldName.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("FldName", "Like", qryFldName);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("FldName", "=", qryFldName);
        }

        if (qryIsLikeValue.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Value", "Like", qryValue);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Value", "=", qryValue);
        }

        if (qryIsLikeDescription.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Description", "Like", qryDescription);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Description", "=", qryDescription);
        }

        if (qryIsLikeRemark.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Remark", "Like", qryRemark);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("Remark", "=", qryRemark);
        }

        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.Refresh(true);
    }

    protected void btnQryClear_Click(object sender, EventArgs e)
    {
        Util.setControlClear(DivQryArea, true);

        qryTabName.ucSelectedID = "";
        if (!string.IsNullOrEmpty(_TabName))
        {
            if (_Dic_TabList.ContainsKey(_TabName))
            {
                qryTabName.ucSelectedID = _TabName;
            }
            qryTabName.ucIsReadOnly = true;
        }
        qryTabName.Refresh();

        if (!string.IsNullOrEmpty(_FldName))
        {
            qryFldName.ucTextData = _FldName;
            qryFldName.ucIsReadOnly = true;
        }

        _QryResultSQL = null;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.Refresh(true);
    }
}