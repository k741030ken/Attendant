using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [AppKeyMap]維護公用程式 
/// </summary>
public partial class Util_AppKeyMapAdmin : SecurePage
{
    #region 共用屬性
    //資料表名稱
    private string _TableName = "AppKeyMap";
    //鍵值欄位清單
    private string[] _PKList = "KeyID".Split(',');
    //預設排序欄位
    private string _DefSortExpression = "KeyID";
    //預設排序方向
    private string _DefSortDirection = "Asc";
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

    private string _DBName
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

    #endregion

    /// <summary>
    /// 頁面載入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次載入才初始 ucGridView
            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = _QryResultSQL;
            ucGridView1.ucDataKeyList = _PKList;
            ucGridView1.ucDefSortExpression = _DefSortExpression;
            ucGridView1.ucDefSortDirection = _DefSortDirection;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("KeyID", "編號鍵值");
            dicDisplay.Add("IsLock", "鎖定中@Y");
            dicDisplay.Add("KeyBaseDate", "編號基礎");
            dicDisplay.Add("KeyFormat", "編號格式");
            dicDisplay.Add("SeqNoLen", "長度@N0");
            dicDisplay.Add("LastSeqNo", "最後號碼@N0");

            ucGridView1.ucDataDisplayDefinition = dicDisplay;
            ucGridView1.ucSelectEnabled = false;
            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDeleteEnabled = true;
            ucGridView1.Refresh(true);
        }

        //資料清單事件訂閱
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
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
        //Util_ucCommSingleSelect oDdl;
        //Util_ucDatePicker oDate;
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

            strObjID = "KeyID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsLock";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "KeyBaseDate";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "KeyFormat";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "SeqNoLen";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "LastSeqNo";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

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

            strObjID = "KeyID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "IsLock";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "KeyBaseDate";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "KeyFormat";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "SeqNoLen";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "LastSeqNo";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();
            
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

        if (qryIsLikeMsgBody.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("KeyID", "Like", qryKeyID);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("KeyID", "=", qryKeyID);
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
        _QryResultSQL = string.Format(_QryBaseSQL, _TableName);
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
        sb.Append("  KeyID,KeyBaseDate,IsLock,SeqNoLen,LastSeqNo,KeyFormat,Remark");
        sb.Append(" ) Values (");
        sb.Append("   ").AppendParameter("KeyID", oEditResult["KeyID"]);
        sb.Append("  ,").AppendParameter("KeyBaseDate", oEditResult["KeyBaseDate"]);
        sb.Append("  ,").AppendParameter("IsLock", oEditResult["IsLock"]);
        sb.Append("  ,").AppendParameter("SeqNoLen", oEditResult["SeqNoLen"]);
        sb.Append("  ,").AppendParameter("LastSeqNo", oEditResult["LastSeqNo"]);
        sb.Append("  ,").AppendParameter("KeyFormat", oEditResult["KeyFormat"]);
        sb.Append("  ,").AppendParameter("Remark", oEditResult["Remark"]);
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
        sb.AppendStatement("Update ").Append(_TableName).Append(" set ");

        //處理非鍵值欄位
        bool isComma = false;
        foreach (var pair in oEditResult)
        {
            if (!_PKList.Contains(pair.Key))
            {
                switch (pair.Key)
                {
                    default:
                        if (isComma)
                        {
                            sb.Append(" , ");
                        }
                        else 
                        {
                            isComma = true;
                        }
                        sb.Append(pair.Key).Append(" = ").AppendParameter(pair.Key, pair.Value);
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