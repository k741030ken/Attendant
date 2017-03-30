using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Sample_BullMsgAdmin : SecurePage
{
    #region 共用屬性
    //資料表來源資料庫
    private string _DBName = "NetSample";
    //資料表名稱
    private string _TableName = "BullMsg";
    //鍵值欄位清單
    private string[] _PKList = "MsgID".Split(',');
    //預設排序欄位
    private string _DefSortExpression = "MsgID";
    //預設排序方向
    private string _DefSortDirection = "Desc";
    //查詢條件基底 SQL
    private string _QryBaseSQL = @"Select *  From {0} ";

    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (ViewState["_QryResultSQL"] == null) { ViewState["_QryResultSQL"] = string.Format(_QryBaseSQL, _TableName); }
            return (string)(ViewState["_QryResultSQL"]);
        }
        set
        {
            ViewState["_QryResultSQL"] = value;
        }
    }


    private Dictionary<string, string> _Dic_MsgKind
    {
        get
        {
            if (ViewState["_Dic_MsgKind"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_MsgKind"];
            }
            else
            {
                ViewState["_Dic_MsgKind"] = Util.getCodeMap(_DBName, "BullMsg", "MsgKind");
                return (Dictionary<string, string>)ViewState["_Dic_MsgKind"];
            }
        }
    }


    //欲鎖定的MsgKind 2015.05.15
    private string _fix_MsgKind
    {
        get
        {
            if (ViewState["_fix_MsgKind"] == null) { ViewState["_fix_MsgKind"] = ""; }
            return (string)(ViewState["_fix_MsgKind"]);
        }
        set
        {
            ViewState["_fix_MsgKind"] = value;
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
        //欲鎖定的MsgKind 2015.05.15
        _fix_MsgKind = Util.getRequestQueryStringKey("MsgKind");
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName) + ((!string.IsNullOrEmpty(_fix_MsgKind)) ? string.Format(" Where MsgKind = '{0}' ", _fix_MsgKind) : "");

        if (!IsPostBack)
        {
            qryMsgKind.ucSourceDictionary = Util.getDictionary(_Dic_MsgKind);
            if (!string.IsNullOrEmpty(_fix_MsgKind))
            {
                qryMsgKind.ucSelectedID = _fix_MsgKind;
                qryMsgKind.ucIsReadOnly = true;
            }
            qryMsgKind.Refresh();

            //首次載入才初始 ucGridView
            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = _QryResultSQL;
            ucGridView1.ucDataKeyList = _PKList;
            ucGridView1.ucDefSortExpression = _DefSortExpression;
            ucGridView1.ucDefSortDirection = _DefSortDirection;
            ucGridView1.ucPagerPosition = PagerPosition.TopAndBottom;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("MsgID", "訊息ID");
            dicDisplay.Add("MsgKind", "類別@C80," + Util.getJSON(_Dic_MsgKind));
            dicDisplay.Add("IsEnabled", "啟用@Y");
            dicDisplay.Add("MsgBody", "訊息內容@L300");
            dicDisplay.Add("StartDateTime", "起始時間@T");
            dicDisplay.Add("EndDateTime", "截止時間@T");
            ucGridView1.ucDataDisplayDefinition = dicDisplay;

            Dictionary<string, string> dicEdit = new Dictionary<string, string>();
            dicEdit.Clear();
            dicEdit.Add("MsgKind", "DropdownList@" + Util.getJSON(Util.getDictionary(_Dic_MsgKind)));
            dicEdit.Add("IsEnabled", "CheckBox");
            dicEdit.Add("MsgBody", "TextBox@{'ucIsRequire':'True','ucRows':'3'}");
            dicEdit.Add("StartDateTime", "Calendar@{'ucIsRequire':'True'}");
            dicEdit.Add("EndDateTime", "Calendar@{'ucIsRequire':'True'}");
            ucGridView1.ucDataEditDefinition = dicEdit;

            ucGridView1.ucSelectEnabled = false;
            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDeleteEnabled = true;
            ucGridView1.Refresh(true);
        }

        //資料清單事件訂閱
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        ucGridView1.GridViewCommand += ucGridView1_GridViewCommand;
    }

    void ucGridView1_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = e.DataTable;

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.AppendStatement("Update ").Append(_TableName).Append(" set UpdDateTime   = ").AppendDbDateTime();
                sb.Append(" , UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);

                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    sb.Append(" ," + dt.Columns[j].ColumnName + " = ").AppendParameter(dt.Columns[j].ColumnName + i, dt.Rows[i][j].ToString());
                }
                sb.Append(" Where 0=0 ");
                sb.Append(Util.getDataQueryKeySQL(_PKList, dt.Rows[i][0].ToString().Split(',')));
            }

            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
            }
            catch
            {
                tx.Rollback();
                Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
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
                divMainGridview.Visible = false;
                fmMain.ChangeMode(FormViewMode.Insert);
                fmMain.DataSource = null;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdCopy":
                //資料複製
                sb.Reset();
                sb.AppendStatement(string.Format("Select * From {0} Where 0 = 0 ", _TableName));
                sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                divMainGridview.Visible = false;
                fmMain.ChangeMode(FormViewMode.Insert);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdEdit":
                //資料編輯
                divMainGridview.Visible = false;
                sb.Reset();
                sb.AppendStatement(string.Format("Select * From {0} Where 0 = 0 ", _TableName));
                sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                fmMain.ChangeMode(FormViewMode.Edit);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                divMainFormView.Visible = true;
                break;
            case "cmdDelete":
                //資料刪除
                sb.Reset();
                sb.AppendStatement(string.Format("Delete From {0} Where 0 = 0 ", _TableName));
                sb.Append(Util.getDataQueryKeySQL(_PKList, oDataKeys));

                if (db.ExecuteNonQuery(sb.BuildCommand()) > 0)
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success); //刪除成功
                }
                else
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error); //刪除失敗
                }
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
        Util_ucCommSingleSelect oDdl;
        Util_ucDatePicker oDate;
        Util_ucTimePicker oTime;
        CheckBox oChk;

        string strObjID;
        DataRow dr = null;

        //「新增」「資料複製」模式
        if (fmMain.CurrentMode == FormViewMode.Insert)
        {
            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnInsert");

            //是否為[資料複製]
            if (fmMain.DataSource != null)
            {
                dr = ((DataTable)fmMain.DataSource).Rows[0];
            }

            //自動產生 MsgID
            strObjID = "MsgID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucTextData = Util.getAppKey(_DBName, "BullMsg").Item1[0];
            oText.ucIsReadOnly = true;
            oText.Refresh();

            strObjID = "MsgKind";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_MsgKind);
            if (!string.IsNullOrEmpty(_fix_MsgKind))
            {
                oDdl.ucSelectedID = _fix_MsgKind;
                oDdl.ucIsReadOnly = true;
            }
            oDdl.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "MsgBody";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "MsgUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "StartDate";
            oDate = (Util_ucDatePicker)fmMain.FindControl(strObjID);
            if (dr != null) oDate.ucDefaultSelectedDate = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim());

            strObjID = "StartTime";
            oTime = (Util_ucTimePicker)fmMain.FindControl(strObjID);
            if (dr != null) oTime.ucDefaultSelectedHH = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("HH");
            if (dr != null) oTime.ucDefaultSelectedMM = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("mm");
            if (dr != null) oTime.ucDefaultSelectedSS = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("ss");

            strObjID = "EndDate";
            oDate = (Util_ucDatePicker)fmMain.FindControl(strObjID);
            if (dr != null) oDate.ucDefaultSelectedDate = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim());

            strObjID = "EndTime";
            oTime = (Util_ucTimePicker)fmMain.FindControl(strObjID);
            if (dr != null) oTime.ucDefaultSelectedHH = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("HH");
            if (dr != null) oTime.ucDefaultSelectedMM = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("mm");
            if (dr != null) oTime.ucDefaultSelectedSS = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("ss");

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

        }

        //「編輯」模式
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnUpdate");

            //取出目前表單資料來源備用
            dr = ((DataTable)fmMain.DataSource).Rows[0];

            strObjID = "MsgID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "MsgKind";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_MsgKind);
            if (!string.IsNullOrEmpty(_fix_MsgKind))
            {
                oDdl.ucSelectedID = _fix_MsgKind;
                oDdl.ucIsReadOnly = true;
            }
            oDdl.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "MsgBody";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "MsgUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "StartDate";
            oDate = (Util_ucDatePicker)fmMain.FindControl(strObjID);
            if (dr != null) oDate.ucDefaultSelectedDate = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim());

            strObjID = "StartTime";
            oTime = (Util_ucTimePicker)fmMain.FindControl(strObjID);
            if (dr != null) oTime.ucDefaultSelectedHH = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("HH");
            if (dr != null) oTime.ucDefaultSelectedMM = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("mm");
            if (dr != null) oTime.ucDefaultSelectedSS = Convert.ToDateTime(dr["StartDateTime"].ToString().Trim()).ToString("ss");

            strObjID = "EndDate";
            oDate = (Util_ucDatePicker)fmMain.FindControl(strObjID);
            if (dr != null) oDate.ucDefaultSelectedDate = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim());

            strObjID = "EndTime";
            oTime = (Util_ucTimePicker)fmMain.FindControl(strObjID);
            if (dr != null) oTime.ucDefaultSelectedHH = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("HH");
            if (dr != null) oTime.ucDefaultSelectedMM = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("mm");
            if (dr != null) oTime.ucDefaultSelectedSS = Convert.ToDateTime(dr["EndDateTime"].ToString().Trim()).ToString("ss");

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            //顯示更新人員/時間
            strObjID = "UpdUser";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucTextData = string.Format("{0} - {1}", dr[strObjID].ToString(), UserInfo.findUser(dr[strObjID].ToString(), false).UserName);
            strObjID = "UpdDateTime";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucTextData = string.Format("{0:yyyy\\/MM\\/dd hh:mm}", dr[strObjID]);

            //鍵值欄位需為唯讀
            for (int i = 0; i < _PKList.Length; i++)
            {
                oText = (Util_ucTextBox)fmMain.FindControl(_PKList[i]);
                if (oText != null)
                {
                    oText.ucIsReadOnly = true;
                    oText.Refresh();
                }
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
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName) + " Where 0 = 0 ";

        _QryResultSQL += Util.getDataQueryConditionSQL("MsgKind", "=", qryMsgKind);

        _QryResultSQL += Util.getDataQueryConditionSQL("StartDateTime", "Between", qryStartDate1, qryStartDate2, "{0} 00:00:00", "{0} 23:59:59");

        _QryResultSQL += Util.getDataQueryConditionSQL("EndDateTime", "Between", qryEndDate1, qryEndDate2, "{0} 00:00:00", "{0} 23:59:59");

        if (qryIsLikeMsgBody.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("MsgBody", "Like", qryMsgBody);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("MsgBody", "=", qryMsgBody);
        }

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
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName) + ((!string.IsNullOrEmpty(_fix_MsgKind)) ? string.Format(" Where MsgKind = '{0}' ", _fix_MsgKind) : "");
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
        sb.AppendStatement("Insert ").Append(_TableName);
        sb.Append("( ");
        sb.Append("  MsgID,MsgKind,IsEnabled,MsgBody,MsgUrl,Remark");
        sb.Append(" ,StartDateTime,EndDateTime");
        sb.Append(" ,UpdUser,UpdDateTime");
        sb.Append(" ) Values (");
        sb.Append("   ").AppendParameter("MsgID", oEditResult["MsgID"]);
        sb.Append("  ,").AppendParameter("MsgKind", oEditResult["MsgKind"]);
        sb.Append("  ,").AppendParameter("IsEnabled", oEditResult["IsEnabled"]);
        sb.Append("  ,").AppendParameter("MsgBody", oEditResult["MsgBody"]);
        sb.Append("  ,").AppendParameter("MsgUrl", oEditResult["MsgUrl"]);
        sb.Append("  ,").AppendParameter("Remark", oEditResult["Remark"]);
        if (oEditResult["StartDate"].CompareTo(oEditResult["EndDate"]) > 0)
        {
            sb.Append("  ,").AppendParameter("StartDateTime", oEditResult["EndDate"] + " " + oEditResult["EndTime"]);
            sb.Append("  ,").AppendParameter("EndDateTime", oEditResult["StartDate"] + " " + oEditResult["StartTime"]);
        }
        else
        {
            sb.Append("  ,").AppendParameter("StartDateTime", oEditResult["StartDate"] + " " + oEditResult["StartTime"]);
            sb.Append("  ,").AppendParameter("EndDateTime", oEditResult["EndDate"] + " " + oEditResult["EndTime"]);
        }

        sb.Append("  ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append("  ,").AppendDbDateTime();
        sb.Append("  )");

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success); //新增成功
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

        //去除不必要欄位
        oEditResult.Remove("UpdUser");
        oEditResult.Remove("UpdDateTime");

        //組合SQL
        sb.Reset();
        sb.AppendStatement("Update ").Append(_TableName).Append(" set UpdDateTime   = ").AppendDbDateTime();
        sb.Append(" , UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);

        //處理非鍵值欄位
        foreach (var pair in oEditResult)
        {
            if (!_PKList.Contains(pair.Key))
            {
                switch (pair.Key)
                {
                    case "StartDate":
                        if (oEditResult["StartDate"].CompareTo(oEditResult["EndDate"]) > 0)
                        {
                            sb.Append(" , StartDateTime = ").AppendParameter("StartDateTime", oEditResult["EndDate"] + " " + oEditResult["EndTime"]);
                            sb.Append(" , EndDateTime = ").AppendParameter("EndDateTime", oEditResult["StartDate"] + " " + oEditResult["StartTime"]);
                        }
                        else
                        {
                            sb.Append(" , StartDateTime = ").AppendParameter("StartDateTime", oEditResult["StartDate"] + " " + oEditResult["StartTime"]);
                            sb.Append(" , EndDateTime = ").AppendParameter("EndDateTime", oEditResult["EndDate"] + " " + oEditResult["EndTime"]);
                        }
                        break;
                    case "EndDate":
                    case "StartTime":
                    case "EndTime":
                        break;
                    default:
                        sb.Append(" , ").Append(pair.Key).Append(" = ").AppendParameter(pair.Key, pair.Value);
                        break;
                }
            }
        }

        //處理鍵值欄位
        sb.Append(" Where 0 = 0 ");
        sb.Append(Util.getDataQueryKeySQL(_PKList, oEditResult));

        //執行SQL
        if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
        {
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success); //更新成功
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error); //更新失敗
        }

        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        ucGridView1.Refresh();
    }

}