using System;
using System.Collections.Generic;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [AppLog]查詢公用程式
/// <para>**適用 [AppLog] 資料表**</para>
/// </summary>
public partial class Util_AppLogQry : SecurePage
{

    #region 環境變數
    protected string _DBName
    {
        get
        {
            if (ViewState["_DBName"] == null)
            {
                ViewState["_DBName"] = Util.getRequestQueryStringKey("DBName");
            }
            return (string)(ViewState["_DBName"]);
        }
        set
        {
            ViewState["_DBName"] = value;
        }
    }

    protected string _AllowPurgeYN
    {
        get
        {
            if (ViewState["_AllowPurgeYN"] == null)
            {
                ViewState["_AllowPurgeYN"] = Util.getRequestQueryStringKey("AllowPurgeYN","N",true);
            }
            return (string)(ViewState["_AllowPurgeYN"]);
        }
        set
        {
            ViewState["_AllowPurgeYN"] = value.ToUpper();
        }
    }

    protected Dictionary<string, string> _Dic_AppLogType
    {
        get
        {
            if (ViewState["_AppLogType"] != null)
            {
                return (Dictionary<string, string>)ViewState["_AppLogType"];
            }
            else
            {
                Dictionary<int, string> tmpDic = Util.getDictionary(typeof(LogHelper.AppLogType), false);
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                foreach (var pair in tmpDic)
                {
                    oDic.Add(pair.Value, pair.Value);
                }

                ViewState["_AppLogType"] = oDic;
                return (Dictionary<string, string>)ViewState["_AppLogType"];
            }
        }
    }


    //查詢基礎 SQL
    private string _QryBaseSQL = @"Select LogNo,LogApp,LogType,LogFrom,LogUser,LogDateTime
                        ,'參數' as 'LogParaTitle'  , ISNULL(NULLIF(LogPara,''),'N/A') as 'LogPara'
                        ,'說明' as 'LogDescTitle'  , ISNULL(NULLIF(LogDesc,''),'N/A') as 'LogDesc'
                        ,'SQL'  as 'LogSQLTitle'   , ISNULL(NULLIF(LogSQL,''),'N/A') as 'LogSQL'
                        ,'備註' as 'LogRemarkTitle', ISNULL(NULLIF(LogRemark,''),'N/A') as 'LogRemark'
                        From AppLog ";

    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (ViewState["_QryResultSQL"] == null) { ViewState["_QryResultSQL"] = ""; }
            return (string)(ViewState["_QryResultSQL"]);
        }
        set
        {
            ViewState["_QryResultSQL"] = value;
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
            btnPurgeTable.ToolTip = "清除[AppLog]所有資料";
            btnPurgeTable.OnClientClick = "return confirm('確定清除[" + _DBName + "]資料庫內，[AppLog]資料表的所有資料？');";

            LogType.ucSourceDictionary = _Dic_AppLogType;
            LogType.Refresh();
        }
    }

    protected void btnPurgeTable_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        db.ExecuteNonQuery("TRUNCATE TABLE AppLog ");
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
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement(_QryBaseSQL).Append(" Where 0=0 ");

        Dictionary<string, string> oDicResult = Util.getControlEditResult(DivQryCondition);

        //組合查詢條件
        if (!string.IsNullOrEmpty(oDicResult["LogApp"]))
            sb.Append(" And LogApp = ").AppendParameter("LogApp", oDicResult["LogApp"]);

        if (!string.IsNullOrEmpty(oDicResult["LogType"]))
            sb.Append(" And LogType = ").AppendParameter("LogType", oDicResult["LogType"]);

        if (!string.IsNullOrEmpty(oDicResult["LogUser"]))
            sb.Append(" And LogUser = ").AppendParameter("LogUser", oDicResult["LogUser"]);

        if (!string.IsNullOrEmpty(oDicResult["LogDate1"]))
        {
            if (string.IsNullOrEmpty(oDicResult["LogDate2"]))
            {
                sb.Append(" And convert(varchar(10),[LogDateTime],111) = ").AppendParameter("LogDateTime1", oDicResult["LogDate1"]);
            }
            else
            {
                if (string.Compare(oDicResult["LogDate1"], oDicResult["LogDate2"]) > 0)
                {
                    //若起始日期　大於　終止日期，則自動交換
                    string tmpDate = oDicResult["LogDate1"];
                    oDicResult["LogDate1"] = oDicResult["LogDate2"];
                    oDicResult["LogDate2"] = tmpDate;

                    LogDate1.ucSelectedDate = oDicResult["LogDate1"];
                    LogDate2.ucSelectedDate = oDicResult["LogDate2"];
                }
                sb.Append(" And ( ");
                sb.Append(" convert(varchar(10),[LogDateTime],111) Between ").AppendParameter("LogDateTime1", oDicResult["LogDate1"]);
                sb.Append(" And ").AppendParameter("LogDateTime2", oDicResult["LogDate2"]);
                sb.Append(" ) ");
            }
        }

        //將查詢條件的SQL物件，還原為純 SQL 字串，以便存到 _QryResultSQL 變數
        _QryResultSQL = Util.getPureSQL(sb);
        //Response.Write(_QryResultSQL);
        //顯示查結果
        ShowQryResult();
    }

    /// <summary>
    /// 顯示查詢結果
    /// </summary>
    protected void ShowQryResult()
    {
        DivQryResult.Visible = true;
        UserInfo oUser = UserInfo.getUserInfo();
        //基礎設定
        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        dicDisplay.Add("LogNo", "LogNo@R");
        dicDisplay.Add("LogApp", "LogApp");
        dicDisplay.Add("LogType", "LogType");
        dicDisplay.Add("LogFrom", "LogFrom");
        dicDisplay.Add("LogUser", "LogUser");
        dicDisplay.Add("LogDateTime", "LogTime");

        Dictionary<string, string> dicTip = new Dictionary<string, string>();
        dicTip.Add("LogType", "LogDescTitle,LogDesc");
        dicTip.Add("LogFrom", "LogParaTitle,LogPara");
        dicTip.Add("LogUser", "LogSQLTitle,LogSQL");
        dicTip.Add("LogDateTime", "LogRemarkTitle,LogRemark");

        ucGridView1.ucDBName = _DBName;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.ucDataKeyList = "LogNo".Split(',');
        ucGridView1.ucSortExpression = "LogNo";
        ucGridView1.ucSortDirection = "Desc";

        ucGridView1.ucHoverTooltipTemplete = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Dialog);
        ucGridView1.ucDataDisplayDefinition = dicDisplay;
        ucGridView1.ucDataDisplayToolTipDefinition = dicTip;
        ucGridView1.ucExportAllField = true;
        ucGridView1.ucExportOpenXmlEnabled = true;
        ucGridView1.Refresh(true);
         
    }

}