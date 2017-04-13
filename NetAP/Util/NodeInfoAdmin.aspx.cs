using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;


/// <summary>
/// [NodeInfo]維護公用程式
/// <para>**可用參數：DBName **</para>
/// </summary>
public partial class Util_NodeInfoAdmin : SecurePage
{

    #region 共用屬性
    //資料表來源資料庫
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
    //資料表名稱
    private string _TableName = "NodeInfo";
    //鍵值欄位清單
    private string[] _PKFieldList = "NodeID".Split(',');
    //預設排序欄位
    private string _DefSortExpression = "NodeID";
    //預設排序方向
    private string _DefSortDirection = "Asc";
    //查詢條件基底 SQL
    private string _QryBaseSQL = @"Select * 
                                    , 'DefaultEnabledNodeID = [ ' + DefaultEnabledNodeID + ' ]' as 'DefaultEnabledNodeInfo' 
                                    , Case When len(TargetUrl) > 0 Then 'Y' Else 'N' end as 'IsTargetUrl' 
                                    , Case When len(TargetPara) > 0 Then 'Y' Else 'N' end as 'IsTargetPara' 
                                    From {0} ";
    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (PageViewState["_QryResultSQL"] == null) { PageViewState["_QryResultSQL"] = string.Format(_QryBaseSQL, _TableName); }
            return (string)(PageViewState["_QryResultSQL"]);
        }
        set
        {
            PageViewState["_QryResultSQL"] = value;
        }
    }

    protected Dictionary<string, string> _Dic_ParentNodeID
    {
        get
        {
            if (PageViewState["_Dic_ParentNodeID"] != null)
            {
                return (Dictionary<string, string>)PageViewState["_Dic_ParentNodeID"];
            }
            else
            {
                DbHelper db = new DbHelper(_DBName);
                PageViewState["_Dic_ParentNodeID"] = Util.getDictionary(db.ExecuteDataSet(string.Format("Select NodeID,NodeName From {0} Where TargetUrl = '' ", _TableName)).Tables[0]);
                return (Dictionary<string, string>)PageViewState["_Dic_ParentNodeID"];
            }
        }
    }

    protected Dictionary<string, string> _Dic_DefaultEnabledNodeID
    {
        get
        {
            if (PageViewState["_Dic_DefaultEnabledNodeID"] != null)
            {
                return (Dictionary<string, string>)PageViewState["_Dic_DefaultEnabledNodeID"];
            }
            else
            {
                DbHelper db = new DbHelper(_DBName);
                PageViewState["_Dic_DefaultEnabledNodeID"] = Util.getDictionary(db.ExecuteDataSet(string.Format("Select NodeID,NodeName From {0} Where TargetUrl <> '' ", _TableName)).Tables[0]);
                return (Dictionary<string, string>)PageViewState["_Dic_DefaultEnabledNodeID"];
            }
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
            //首次載入才初始
            qryParentNodeID.ucSourceDictionary = Util.getDictionary(_Dic_ParentNodeID);
            qryParentNodeID.Refresh();
            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = _QryResultSQL;
            ucGridView1.ucDataKeyList = _PKFieldList;
            ucGridView1.ucDefSortExpression = _DefSortExpression;
            ucGridView1.ucDefSortDirection = _DefSortDirection;

            Dictionary<string, string> dicTip = new Dictionary<string, string>();
            dicTip.Add("ParentNodeID", "DefaultEnabledNodeInfo");
            dicTip.Add("IsTargetUrl", "TargetUrl");
            dicTip.Add("IsTargetPara", "TargetPara");
            ucGridView1.ucDataDisplayToolTipDefinition = dicTip;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("NodeID", "NodeID");
            dicDisplay.Add("NodeName", "Name@L120");
            dicDisplay.Add("ParentNodeID", "Parent");
            dicDisplay.Add("IsEnabled", "Enable@Y");
            dicDisplay.Add("OrderSeq", "Seq");
            dicDisplay.Add("IsTargetUrl", "Url@Y");
            dicDisplay.Add("IsTargetPara", "Para@Y");
            dicDisplay.Add("ChkGrantID", "ChkGrantID");
            dicDisplay.Add("UpdDateTime", "UpdTime@T");
            ucGridView1.ucDataDisplayDefinition = dicDisplay;
            ucGridView1.ucSelectEnabled = false;
            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDeleteEnabled = true;
            ucGridView1.ucMultilingualEnabled = true;
            ucGridView1.ucExportAllField = true;
            ucGridView1.ucExportOpenXmlName = "NodeInfo.xlsx";
            ucGridView1.ucExportOpenXmlEnabled = true;
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
                sb.Append(Util.getDataQueryKeySQL(_PKFieldList, oDataKeys));
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
                sb.Append(Util.getDataQueryKeySQL(_PKFieldList, oDataKeys));
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
                sb.Append(Util.getDataQueryKeySQL(_PKFieldList, oDataKeys));

                if (db.ExecuteNonQuery(sb.BuildCommand()) > 0)
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success); //刪除成功
                    PageViewState["_Dic_DefaultEnabledNodeID"] = null;
                    PageViewState["_Dic_ParentNodeID"] = null;
                }
                else
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error); //刪除失敗
                }
                break;
            case "cmdMultilingual":
                string strURL = string.Format("{0}?DBName={1}&TableName={2}&PKFieldList={3}&PKValueList={4}", Util._MuiAdminUrl, _DBName, _TableName, Util.getStringJoin(_PKFieldList), Util.getStringJoin(e.DataKeys));
                ucModalPopup1.Reset();
                ucModalPopup1.ucFrameURL = strURL;
                ucModalPopup1.ucPopupWidth = 650;
                ucModalPopup1.ucPopupHeight = 350;
                ucModalPopup1.ucBtnCloselEnabled = true;
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
        Util_ucCommSingleSelect oDdl;
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

            strObjID = "NodeID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "NodeName";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ParentNodeID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_ParentNodeID);
            oDdl.Refresh();

            strObjID = "DefaultEnabledNodeID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_DefaultEnabledNodeID);
            oDdl.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "OrderSeq";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "TargetUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "TargetPara";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ChkGrantID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ImageUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ToolTip";
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

            strObjID = "NodeID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "NodeName";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ParentNodeID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_ParentNodeID);
            if (dr != null)
            {
                oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
                var tmpDic = Util.getDictionary(_Dic_ParentNodeID);
                tmpDic.Remove(dr["NodeID"].ToString().Trim());
                oDdl.ucSourceDictionary = tmpDic;
            }
            oDdl.Refresh();

            strObjID = "DefaultEnabledNodeID";
            oDdl = (Util_ucCommSingleSelect)fmMain.FindControl(strObjID);
            if (dr != null) oDdl.ucSelectedID = dr[strObjID].ToString().Trim();
            oDdl.ucSourceDictionary = Util.getDictionary(_Dic_DefaultEnabledNodeID);
            oDdl.Refresh();

            strObjID = "IsEnabled";
            oChk = (CheckBox)fmMain.FindControl(strObjID);
            if (dr != null) oChk.Checked = (dr[strObjID].ToString().Trim().ToUpper() == "Y") ? true : false;

            strObjID = "OrderSeq";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            oText.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "TargetUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "TargetPara";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ChkGrantID";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ImageUrl";
            oText = (Util_ucTextBox)fmMain.FindControl(strObjID);
            if (dr != null) oText.ucTextData = dr[strObjID].ToString().Trim();

            strObjID = "ToolTip";
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
            for (int i = 0; i < _PKFieldList.Length; i++)
            {
                oText = (Util_ucTextBox)fmMain.FindControl(_PKFieldList[i]);
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

        if (qryIsLikeNodeID.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("NodeID", "Like", qryNodeID);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("NodeID", "=", qryNodeID);
        }

        if (qryIsLikeNodeName.Checked)
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("NodeName", "Like", qryNodeName);
        }
        else
        {
            _QryResultSQL += Util.getDataQueryConditionSQL("NodeName", "=", qryNodeName);
        }

        _QryResultSQL += Util.getDataQueryConditionSQL("ParentNodeID", "=", qryParentNodeID.ucSelectedID);

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
        sb.Append("  NodeID,NodeName,ParentNodeID,DefaultEnabledNodeID,IsEnabled,OrderSeq");
        sb.Append(" ,TargetUrl,TargetPara,ChkGrantID,ImageUrl,ToolTip,Remark");
        sb.Append(" ,UpdUser,UpdDateTime");
        sb.Append(" ) Values (");
        sb.Append("   ").AppendParameter("NodeID", oEditResult["NodeID"]);
        sb.Append("  ,").AppendParameter("NodeName", oEditResult["NodeName"]);
        sb.Append("  ,").AppendParameter("ParentNodeID", oEditResult["ParentNodeID"]);
        sb.Append("  ,").AppendParameter("DefaultEnabledNodeID", oEditResult["DefaultEnabledNodeID"]);
        sb.Append("  ,").AppendParameter("IsEnabled", oEditResult["IsEnabled"]);
        sb.Append("  ,").AppendParameter("OrderSeq", oEditResult["OrderSeq"]);
        sb.Append("  ,").AppendParameter("TargetUrl", oEditResult["TargetUrl"]);
        sb.Append("  ,").AppendParameter("TargetPara", oEditResult["TargetPara"]);
        sb.Append("  ,").AppendParameter("ChkGrantID", oEditResult["ChkGrantID"]);
        sb.Append("  ,").AppendParameter("ImageUrl", oEditResult["ImageUrl"]);
        sb.Append("  ,").AppendParameter("ToolTip", oEditResult["ToolTip"]);
        sb.Append("  ,").AppendParameter("Remark", oEditResult["Remark"]);
        sb.Append("  ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append("  ,").AppendDbDateTime();
        sb.Append("  )");

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success); //新增成功
                PageViewState["_Dic_DefaultEnabledNodeID"] = null;
                PageViewState["_Dic_ParentNodeID"] = null;
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
            if (!_PKFieldList.Contains(pair.Key))
            {
                switch (pair.Key)
                {
                    default:
                        sb.Append(" , ").Append(pair.Key).Append(" = ").AppendParameter(pair.Key, pair.Value);
                        break;
                }
            }
        }

        //處理鍵值欄位
        sb.Append(" Where 0 = 0 ");
        sb.Append(Util.getDataQueryKeySQL(_PKFieldList, oEditResult));

        //執行SQL
        if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
        {
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success); //更新成功
            PageViewState["_Dic_DefaultEnabledNodeID"] = null;
            PageViewState["_Dic_ParentNodeID"] = null;
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