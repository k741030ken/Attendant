using System;
using System.Collections.Generic;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [AppDataLog]查詢公用程式
/// <para>**適用 [AppDataLog] 資料表**</para>
/// </summary>
public partial class Util_AppDataLogQry : SecurePage
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
            return (string)(PageViewState["_DBName"]);
        }
        set
        {
            PageViewState["_DBName"] = value;
        }
    }

    protected string _LogTable
    {
        get
        {
            if (PageViewState["_LogTable"] == null)
            {
                PageViewState["_LogTable"] = Util.getRequestQueryStringKey("LogTable");
            }
            return (string)(PageViewState["_LogTable"]);
        }
        set
        {
            PageViewState["_LogTable"] = value;
        }
    }

    protected string _LogKey
    {
        get
        {
            if (PageViewState["_LogKey"] == null)
            {
                PageViewState["_LogKey"] = Util.getRequestQueryStringKey("LogKey");
            }
            return (string)(PageViewState["_LogKey"]);
        }
        set
        {
            PageViewState["_LogKey"] = value;
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

    protected Dictionary<string, string> _Dic_AppDataLogType
    {
        get
        {
            if (PageViewState["_AppDataLogType"] != null)
            {
                return (Dictionary<string, string>)PageViewState["_AppDataLogType"];
            }
            else
            {
                Dictionary<int, string> tmpDic = Util.getDictionary(typeof(LogHelper.AppDataLogType), false);
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                foreach (var pair in tmpDic)
                {
                    oDic.Add(pair.Value, pair.Value);
                }

                PageViewState["_AppDataLogType"] = oDic;
                return (Dictionary<string, string>)PageViewState["_AppDataLogType"];
            }
        }
    }

    //查詢基礎 SQL
    private string _QryBaseSQL = "Select DataLogNo,DataLogTable,DataLogKey,DataLogType,DataLogField,DataLogFieldCaption,DataLogFieldValue,DataLogUser + ' - ' + DataLogUserName as DataLogUser,DataLogDateTime From AppDataLog ";

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
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ParaNotFoundList, "DBName"));
            return;
        }

        //限定特定資料表
        if (!string.IsNullOrEmpty(_LogTable))
        {
            DataLogTable.ucTextData = _LogTable;
            DataLogTable.ucIsReadOnly = true;
        }

        //限定特定鍵值
        if (!string.IsNullOrEmpty(_LogKey))
        {
            DataLogKey.ucTextData = _LogKey;
            DataLogKey.ucIsReadOnly = true;
        }

        if (_AllowPurgeYN.ToUpper() == "Y")
        {
            btnPurgeTable.Visible = true;
        }
        else
        {
            btnPurgeTable.Visible = false;
        }

        if (!IsPostBack)
        {
            btnPurgeTable.ToolTip = "清除[AppDataLog]所有資料";
            btnPurgeTable.OnClientClick = "return confirm('確定清除[" + _DBName + "]資料庫內，[AppDataLog]資料表的所有資料？');";

            DataLogType.ucSourceDictionary = _Dic_AppDataLogType;
            DataLogType.Refresh();

            //若同時指定 _DBName,_LogTable,_LogKey，則會隱藏查詢條件區，直接顯示結果
            if (!string.IsNullOrEmpty(_DBName) && !string.IsNullOrEmpty(_LogTable) && !string.IsNullOrEmpty(_LogKey))
            {
                DivQryCondition.Visible = false;
                ShowQryResult();
            }
        }

    }

    protected void btnPurgeTable_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        db.ExecuteNonQuery("TRUNCATE TABLE AppDataLog ");
        _QryResultSQL = "";
        DivQryResult.Visible = false;
        Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        _QryResultSQL = "";
        DivQryResult.Visible = false;
    }

    protected void btnQry_Click(object sender, EventArgs e)
    {
        ShowQryResult();
    }

    /// <summary>
    /// 顯示查詢結果
    /// </summary>
    protected void ShowQryResult()
    {
        Dictionary<string, string> oDicResult = Util.getControlEditResult(DivQryCondition);

        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement(_QryBaseSQL).Append(" Where 0=0 ");

        //組合查詢條件
        if (!string.IsNullOrEmpty(oDicResult["DataLogTable"]))
            sb.Append(" And DataLogTable = ").AppendParameter("DataLogTable", oDicResult["DataLogTable"]);

        if (!string.IsNullOrEmpty(oDicResult["DataLogKey"]))
            sb.Append(" And DataLogKey = ").AppendParameter("DataLogKey", oDicResult["DataLogKey"]);

        if (!string.IsNullOrEmpty(oDicResult["DataLogType"]))
            sb.Append(" And DataLogType = ").AppendParameter("DataLogType", oDicResult["DataLogType"]);

        if (!string.IsNullOrEmpty(oDicResult["DataLogField"]))
            sb.Append(" And DataLogField = ").AppendParameter("DataLogField", oDicResult["DataLogField"]);

        if (!string.IsNullOrEmpty(oDicResult["DataLogUser"]))
            sb.Append(" And DataLogUser = ").AppendParameter("DataLogUser", oDicResult["DataLogUser"]);

        if (!string.IsNullOrEmpty(oDicResult["DataLogDate1"]))
        {
            if (string.IsNullOrEmpty(oDicResult["DataLogDate2"]))
            {
                sb.Append(" And convert(varchar(10),[DataLogDateTime],111) = ").AppendParameter("DataLogDateTime1", oDicResult["DataLogDate1"]);
            }
            else
            {
                if (string.Compare(oDicResult["DataLogDate1"], oDicResult["DataLogDate2"]) > 0)
                {
                    //若起始日期　大於　終止日期，則自動交換
                    string tmpDate = oDicResult["DataLogDate1"];
                    oDicResult["DataLogDate1"] = oDicResult["DataLogDate2"];
                    oDicResult["DataLogDate2"] = tmpDate;

                    DataLogDate1.ucSelectedDate = oDicResult["DataLogDate1"];
                    DataLogDate2.ucSelectedDate = oDicResult["DataLogDate2"];
                }
                sb.Append(" And ( ");
                sb.Append(" convert(varchar(10),[DataLogDateTime],111) Between ").AppendParameter("DataLogDateTime1", oDicResult["DataLogDate1"]);
                sb.Append(" And ").AppendParameter("DataLogDateTime2", oDicResult["DataLogDate2"]);
                sb.Append(" ) ");
            }
        }

        //將查詢條件的SQL物件，還原為純 SQL 字串，以便存到 _QryResultSQL 變數
        _QryResultSQL = Util.getPureSQL(sb);
        //Response.Write(_QryResultSQL);

        DivQryResult.Visible = true;
        UserInfo oUser = UserInfo.getUserInfo();
        //基礎設定
        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        dicDisplay.Add("DataLogNo", "Log@R");
        if (string.IsNullOrEmpty(_LogTable))
        {
            dicDisplay.Add("DataLogTable", "資料表");
        }
        //if (string.IsNullOrEmpty(_LogKey))
        //{
            dicDisplay.Add("DataLogKey", "鍵值");
        //}
        dicDisplay.Add("DataLogType", "類型");
        dicDisplay.Add("DataLogField", "欄位名稱");
        dicDisplay.Add("DataLogFieldCaption", "欄位抬頭");
        dicDisplay.Add("DataLogFieldValue", "欄位內容");
        dicDisplay.Add("DataLogUser", "記錄人員");
        dicDisplay.Add("DataLogDateTime", "記錄時間@S");

        ucGridView1.ucDBName = _DBName;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.ucDataKeyList = "DataLogNo".Split(',');
        ucGridView1.ucSortExpression = "DataLogNo";
        ucGridView1.ucSortDirection = "Desc";

        ucGridView1.ucDataDisplayDefinition = dicDisplay;
        ucGridView1.ucExportAllField = true;
        ucGridView1.ucExportOpenXmlEnabled = true;
        ucGridView1.Refresh(true);

    }
}