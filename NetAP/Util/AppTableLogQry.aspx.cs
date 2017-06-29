using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [_LOG]查詢公用程式
/// <para>**可用參數：DBName , TableName, AllowPurgeYN **</para>
/// </summary>
public partial class Util_AppTableLogQry : SecurePage
{

    #region 環境變數
    protected string _DBName
    {
        get
        {
            if (PageViewState["_DBName"] == null)
            {
                PageViewState["_DBName"] = Util.getRequestQueryStringKey("DBName");
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_DBName"]));
        }
        set
        {
            PageViewState["_DBName"] = value;
        }
    }

    protected string _TableName
    {
        get
        {
            if (PageViewState["_TableName"] == null)
            {
                PageViewState["_TableName"] = Util.getRequestQueryStringKey("TableName");
            }
            return HttpUtility.HtmlEncode((PageViewState["_TableName"]).ToString());
        }
        set
        {
            PageViewState["_TableName"] = value;
        }
    }

    protected string _AllowPurgeYN
    {
        get
        {
            if (PageViewState["_AllowPurgeYN"] == null)
            {
                PageViewState["_AllowPurgeYN"] = Util.getRequestQueryStringKey("AllowPurgeYN", "N", true);
            }
            return (string)(PageViewState["_AllowPurgeYN"]);
        }
        set
        {
            PageViewState["_AllowPurgeYN"] = value.ToUpper();
        }
    }

    protected Dictionary<string, string> _Dic_AppTableLogType
    {
        get
        {
            if (PageViewState["_AppTableLogType"] != null)
            {
                return (Dictionary<string, string>)PageViewState["_AppTableLogType"];
            }
            else
            {
                Dictionary<int, string> tmpDic = Util.getDictionary(typeof(LogHelper.AppTableLogType), false);
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                foreach (var pair in tmpDic)
                {
                    oDic.Add(pair.Value, pair.Value);
                }

                PageViewState["_AppTableLogType"] = oDic;
                return (Dictionary<string, string>)PageViewState["_AppTableLogType"];
            }
        }
    }


    //查詢基礎 SQL
    private string _QryBaseSQL = @"Select * From {0} Where 0=0 ";

    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (PageViewState["_QryResultSQL"] == null) { PageViewState["_QryResultSQL"] = ""; }
            return (string)(PageViewState["_QryResultSQL"]);
        }
        set
        {
            PageViewState["_QryResultSQL"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(_DBName))
        {
            DivQryCondition.Visible = false;
            DivQryResult.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ParaNotFound1, "DBName"));
            return;
        }

        if (string.IsNullOrEmpty(_TableName))
        {
            ShowSelectTable();
            return;
        }

        if (!IsPostBack)
        {
            InitQryCondition();
        }

        LogUser.onClose += LogUser_onClose;
    }

    void LogUser_onClose(object sender, EventArgs e)
    {
        if (DivQryResult.Visible) ucGridView1.Refresh();
    }

    protected void btnTableName_Click(object sender, EventArgs e)
    {
        DivSelectTable.Visible = false;
        DivQryCondition.Visible = true;
        DivQryResult.Visible = false;

        _TableName = ddlTableName.ucSelectedID;
        labQryHeader.Text = string.Format("[{0}]查詢條件", _TableName);
        InitQryCondition();
    }

    /// <summary>
    /// 初始查詢條件
    /// </summary>
    protected void InitQryCondition()
    {
        labQryHeader.Text = string.Format("[{0}] 查詢條件", _TableName);
        labQryResultHeader.Text = string.Format("[{0}] 查詢結果", _TableName);
        btnPurgeTable.ToolTip = string.Format("清除[{0}]所有資料", _TableName);
        btnPurgeTable.OnClientClick = string.Format("return confirm('確定清除[{0}]資料庫內，[{1}]資料表的所有資料？');", _DBName, _TableName);

        DataTable dtSchema = Util.getDataSchemaInfo(_DBName).Select(string.Format(" TABLE_NAME = '{0}' and TABLE_TYPE = 'BASE TABLE' ", _TableName)).CopyToDataTable();
        Dictionary<string, string> dicTable = Util.getDictionary(dtSchema, "COLUMN_NAME", "IS_PKEY");
        PageViewState["_TableFieldList"] = Util.getArray(dicTable);
        PageViewState["_TablePKFieldList"] = dicTable.AsEnumerable().Where(r => r.Value == "Y").Select(k => k.Key).ToArray();

        Util_ucTextBox oTxt;
        CheckBox oChk;

        for (int i = 0; i < 10; i++)
        {
            oTxt = (Util_ucTextBox)Util.FindControlEx(this.Page, string.Format("PKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            oChk = (CheckBox)Util.FindControlEx(this.Page, string.Format("IsLikePKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            if (oTxt != null)
            {
                oTxt.ucCaption = "";
                oTxt.ucTextData = "";
                oTxt.Visible = false;
                oChk.Visible = false;
                oChk.Checked = false;
            }
        }

        for (int i = 0; i < ((string[])PageViewState["_TablePKFieldList"]).Count(); i++)
        {
            oTxt = (Util_ucTextBox)Util.FindControlEx(this.Page, string.Format("PKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            oChk = (CheckBox)Util.FindControlEx(this.Page, string.Format("IsLikePKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            if (oTxt != null && ((string[])PageViewState["_TablePKFieldList"])[i] != "TableLogNo")
            {
                oTxt.ucCaptionWidth = 100;
                oTxt.ucWidth = 150;
                oTxt.ucCaption = ((string[])PageViewState["_TablePKFieldList"])[i];
                oTxt.ucTextData = "";
                oTxt.Visible = true;
                oChk.Visible = true;
            }
        }

        LogType.ucSourceDictionary = _Dic_AppTableLogType;
        LogType.Refresh();

        if (_AllowPurgeYN.ToUpper() == "Y")
            btnPurgeTable.Visible = true;
        else
            btnPurgeTable.Visible = false;

        if (!Util.getRequestQueryString().IsNullOrEmpty("TableName"))
            btnClearTableName.Visible = false;
        else
            btnClearTableName.Visible = true;
    }

    protected void btnPurgeTable_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        db.ExecuteNonQuery(string.Format("TRUNCATE TABLE {0} ", _TableName));
        _QryResultSQL = "";
        DivQryResult.Visible = false;
        Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        _QryResultSQL = "";
        DivQryResult.Visible = false;
        Util.setControlClear(DivQryCondition, true);
    }

    protected void btnClearTableName_Click(object sender, EventArgs e)
    {
        _TableName = "";
        ShowSelectTable();
    }

    protected void btnQry_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        Dictionary<string, string> oDicResult = Util.getControlEditResult(DivQryCondition);
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName);


        //組合查詢條件
        Util_ucTextBox oTxt;
        CheckBox oChk;
        for (int i = 0; i < ((string[])PageViewState["_TablePKFieldList"]).Count(); i++)
        {
            oTxt = (Util_ucTextBox)Util.FindControlEx(this.Page, string.Format("PKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            oChk = (CheckBox)Util.FindControlEx(this.Page, string.Format("IsLikePKey{0}", (i + 1).ToString().PadLeft(2, '0')));
            if (oTxt != null)
            {
                _QryResultSQL += Util.getDataQueryConditionSQL(oTxt.ucCaption, oChk.Checked ? "Like" : "=", oTxt.ucTextData);
            }
        }

        _QryResultSQL += Util.getDataQueryConditionSQL("TableLogType", "=", oDicResult["LogType"]);
        _QryResultSQL += Util.getDataQueryConditionSQL("TableLogUser", "=", oDicResult["LogUser"]);
        _QryResultSQL += Util.getDataQueryConditionSQL("TableLogDateTime", "BETWEEN", oDicResult["LogDate1"], oDicResult["LogDate2"]);

        //顯示查結果
        ShowQryResult();
    }

    protected void ShowSelectTable()
    {
        DivSelectTable.Visible = true;
        DivQryCondition.Visible = false;
        DivQryResult.Visible = false;
        DataTable dtSchema = Util.getDataSchemaInfo(_DBName).Select(" TABLE_NAME Like '%_LOG' and TABLE_TYPE = 'BASE TABLE' ").CopyToDataTable();
        ddlTableName.ucIsRequire = true;
        ddlTableName.ucSourceDictionary = Util.getDictionary(dtSchema.DefaultView.ToTable(true, "TABLE_NAME,TABLE_DESCRIPTION".Split(',')));
        ddlTableName.Refresh();
    }


    /// <summary>
    /// 顯示查詢結果
    /// </summary>
    protected void ShowQryResult()
    {
        DivQryResult.Visible = true;
        //基礎設定
        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        for (int i = 0; i < ((string[])PageViewState["_TablePKFieldList"]).Count(); i++)
        {
            dicDisplay.Add(((string[])PageViewState["_TablePKFieldList"])[i], ((string[])PageViewState["_TablePKFieldList"])[i]);
        }
        dicDisplay.Add("TableLogType", "LogType");
        dicDisplay.Add("TableLogUser", "LogUser");
        dicDisplay.Add("TableLogDateTime", "LogTime");

        ucGridView1.ucDBName = _DBName;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.ucDataKeyList = (string[])PageViewState["_TablePKFieldList"];
        ucGridView1.ucSortExpression = "";
        ucGridView1.ucSortDirection = "";

        ucGridView1.ucDataDisplayDefinition = dicDisplay;
        ucGridView1.ucExportAllField = true;
        ucGridView1.ucExportOpenXmlEnabled = true;
        ucGridView1.Refresh(true);

    }

}