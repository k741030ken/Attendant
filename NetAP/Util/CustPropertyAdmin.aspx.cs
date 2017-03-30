using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [CustProperty]維護公用程式 
/// </summary>
public partial class Util_CustPropertyAdmin : SecurePage
{
    #region 共用屬性
    //資料表來源資料庫
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
    //資料表名稱
    private string _TableName = "CustProperty";
    //鍵值欄位清單
    private string[] _PKList = "PKID,PKKind,PropID".Split(',');
    //預設排序欄位
    private string _DefSortExpression = "PKID";
    //預設排序方向
    private string _DefSortDirection = "Asc";
    //查詢條件基底 SQL
    private string _QryBaseSQL = @"Select *, Case When len(PropJSON) > 0 Then 'Y' Else 'N' end as 'IsPropJSON' From {0} ";
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
    //IsPropJSON
    private Dictionary<string, string> _dicIsPropJSON
    {
        get
        {
            if (ViewState["_dicIsPropJSON"] == null)
            {
                Dictionary<string, string> tDic = new Dictionary<string, string>();
                tDic.Add("Y", "有內容");
                tDic.Add("N", "無資料");
                ViewState["_dicIsPropJSON"] = tDic;
            }
            return (Dictionary<string, string>)(ViewState["_dicIsPropJSON"]);
        }
        set
        {
            ViewState["_dicIsPropJSON"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次載入
            qryIsPropJSON.ucIsSearchEnabled = false;
            qryIsPropJSON.ucSourceDictionary = Util.getDictionary(_dicIsPropJSON);
            qryIsPropJSON.Refresh();
            //初始 ucGridView
            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = _QryResultSQL;
            ucGridView1.ucDataKeyList = _PKList;
            ucGridView1.ucDefSortExpression = _DefSortExpression;
            ucGridView1.ucDefSortDirection = _DefSortDirection;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("PKID", "PKID");
            dicDisplay.Add("PKKind", "PKKind");
            dicDisplay.Add("PropID", "PropID");
            dicDisplay.Add("Prop1", "Prop1");
            dicDisplay.Add("Prop2", "Prop2");
            dicDisplay.Add("Prop3", "Prop3");
            dicDisplay.Add("IsPropJSON", "PropJSON@Y");
            dicDisplay.Add("UpdDateTime", "UpdTime@T");
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
        //CheckBox oChk;
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

            strObjID = "PKID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PKKind";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PropID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop1";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop2";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop3";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PropJSON";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
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

            strObjID = "PKID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PKKind";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PropID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop1";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop2";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Prop3";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "PropJSON";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "Remark";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            //顯示更新人員/時間
            strObjID = "UpdUser";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucTextData = string.Format("{0} - {1}", dr[strObjID].ToString(), UserInfo.findUser(dr[strObjID].ToString(), false).UserName);
            strObjID = "UpdDateTime";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucTextData = string.Format("{0:yyyy\\/MM\\/dd HH:mm}", dr[strObjID]);

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
        _QryResultSQL += Util.getDataQueryConditionSQL("PKID", ((qryIsLikePKID.Checked) ? "%" : "="), qryPKID);
        _QryResultSQL += Util.getDataQueryConditionSQL("PKKind", ((qryIsLikePKKind.Checked) ? "%" : "="), qryPKKind);
        _QryResultSQL += Util.getDataQueryConditionSQL("PropID", ((qryIsLikePropID.Checked) ? "%" : "="), qryPropID);

        if (!string.IsNullOrEmpty(qryIsPropJSON.ucSelectedID))
        {
            switch (qryIsPropJSON.ucSelectedID)
            {
                case "Y":
                    _QryResultSQL += " and len(PropJSON) > 0 ";
                    break;
                case "N":
                    _QryResultSQL += " and len(PropJSON) = 0 ";
                    break;
                default:
                    break;
            }
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
        sb.Append("( PKID,PKKind,PropID,Prop1,Prop2,Prop3,PropJSON,Remark");
        sb.Append(" ,UpdUser,UpdDateTime");
        sb.Append(") Values (");
        sb.Append("   ").AppendParameter("PKID", oEditResult["PKID"]);
        sb.Append("  ,").AppendParameter("PKKind", oEditResult["PKKind"]);
        sb.Append("  ,").AppendParameter("PropID", oEditResult["PropID"]);
        sb.Append("  ,").AppendParameter("Prop1", oEditResult["Prop1"]);
        sb.Append("  ,").AppendParameter("Prop2", oEditResult["Prop2"]);
        sb.Append("  ,").AppendParameter("Prop3", oEditResult["Prop3"]);
        sb.Append("  ,").AppendParameter("PropJSON", oEditResult["PropJSON"]);
        sb.Append("  ,").AppendParameter("Remark", oEditResult["Remark"]);
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
                sb.Append(" , ").Append(pair.Key).Append(" = ").AppendParameter(pair.Key, pair.Value);
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