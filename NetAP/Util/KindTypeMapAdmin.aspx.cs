using System;
using System.Collections.Generic;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [KindTypeMap]維護公用程式
/// <para>**可用參數：DBName , KindID , TypeID **</para>
/// </summary>
public partial class Util_KindTypeMapAdmin : SecurePage
{
    #region 屬性定義
    //所在資料庫
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

    //指定 KindID
    protected string _KindID
    {
        get
        {
            if (ViewState["_KindID"] == null)
            {
                ViewState["_KindID"] = Util.getRequestQueryStringKey("KindID");
            }
            return (string)(ViewState["_KindID"]);
        }
        set
        {
            ViewState["_KindID"] = value;
        }
    }

    //指定 TypeID
    protected string _TypeID
    {
        get
        {
            if (ViewState["_TypeID"] == null)
            {
                ViewState["_TypeID"] = Util.getRequestQueryStringKey("TypeID");
            }
            return (string)(ViewState["_TypeID"]);
        }
        set
        {
            ViewState["_TypeID"] = value;
        }
    } 
    #endregion

    protected string _TableName = "KindTypeMap";
    //基礎查詢SQL
    protected string _BaseQrySQL = "Select KindID,TypeID,ItemID,IsEnabled,ItemName,ItemRemark,UpdUser,UpdDateTime From {0} Where 0=0 ";
    //鍵值欄位清單
    protected string _PKFieldList = "KindID,TypeID,ItemID";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            //初始
            if (string.IsNullOrEmpty(_DBName))
            {
                labErrMsg.Visible = true;
                ucGridView1.Visible = false;
                labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_ParaError1,"DBName"));
                return;
            }

            string strQrySQL = string.Format(_BaseQrySQL,_TableName);
            if (!string.IsNullOrEmpty(_KindID))
            {
                strQrySQL += string.Format(" And KindID = '{0}'", _KindID);
            }
            if (!string.IsNullOrEmpty(_TypeID))
            {
                strQrySQL += string.Format(" And TypeID = '{0}'", _TypeID);
            }

            ucGridView1.ucDBName = _DBName;
            ucGridView1.ucDataQrySQL = strQrySQL;
            ucGridView1.ucDataKeyList = _PKFieldList.Split(',');
            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            // KindID,TypeID,ItemID,IsEnabled,ItemName,ItemRemark,UpdUser,UpdDateTime
            dicDisplay.Add("KindID", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_KindID);
            dicDisplay.Add("TypeID", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_TypeID);
            dicDisplay.Add("ItemID", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemID);
            dicDisplay.Add("IsEnabled", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_IsEnabled);
            dicDisplay.Add("ItemName", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemName);
            dicDisplay.Add("ItemRemark", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemRemark + "@L100");
            dicDisplay.Add("UpdUser", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_UpdUser);
            dicDisplay.Add("UpdDateTime", SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_UpdDateTime + "@T");
            ucGridView1.ucDataDisplayDefinition = dicDisplay;
            ucGridView1.ucDataGroupKey = "KindID";

            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucDeleteEnabled = true;
            ucGridView1.ucMultilingualEnabled = true;
            ucGridView1.Refresh(true);
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
                        sb.Reset();
                        sb.AppendStatement(string.Format("Delete {0} ",_TableName));
                        sb.Append(" Where KindID = ").AppendParameter("KindID", strDataKeys.Split(',')[0]);
                        sb.Append(" And   TypeID = ").AppendParameter("TypeID", strDataKeys.Split(',')[1]);
                        sb.Append(" And   ItemID   = ").AppendParameter("ItemID", strDataKeys.Split(',')[2]);
                        db.ExecuteNonQuery(sb.BuildCommand());
                        Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
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
            default:
                Util.MsgBox(string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_Undefined1, e.CommandName));
                break;
        }


    }

    protected void LaunchPopup(Dictionary<string, string> oContext)
    {

        KindID.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_KindID;
        TypeID.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_TypeID;
        ItemID.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemID;
        IsEnabled.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_IsEnabled;
        ItemName.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemName;
        ItemProp1.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemProp1;
        ItemProp2.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemProp2;
        ItemProp3.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemProp3;
        ItemJSON.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemJSON;
        ItemRemark.ucCaption = SinoPac.WebExpress.Common.Properties.Resources.KindTypeMap_ItemRemark;


        KindID.ucIsRequire = true;
        TypeID.ucIsRequire = true;
        ItemID.ucIsRequire = true;
        IsEnabled.ucIsRequire = true;
        ItemName.ucIsRequire = true;

        KindID.ucIsReadOnly = false;
        TypeID.ucIsReadOnly = false;
        ItemID.ucIsReadOnly = false;
        KindID.ucTextData = "";
        TypeID.ucTextData = "";
        ItemID.ucTextData = "";
        IsEnabled.ucSelectedID = "Y";
        ItemName.ucTextData = "";
        ItemProp1.ucTextData = "";
        ItemProp2.ucTextData = "";
        ItemProp3.ucTextData = "";
        ItemJSON.ucTextData = "";
        ItemRemark.ucTextData = "";

        Dictionary<string, string> oDicIsEnabled = new Dictionary<string, string>();
        oDicIsEnabled.Add("Y", "Y");
        oDicIsEnabled.Add("N", "N");
        IsEnabled.ucSourceDictionary = oDicIsEnabled;
        IsEnabled.ucIsSearchEnabled = false;
        IsEnabled.ucDropDownSourceListWidth = 40;
        IsEnabled.Refresh();

        if (!string.IsNullOrEmpty(oContext["DataKeys"]))
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(string.Format("Select * From {0} Where 0=0 ",_TableName));
            sb.Append(" And KindID = ").AppendParameter("KindID", oContext["DataKeys"].Split(',')[0]);
            sb.Append(" And TypeID = ").AppendParameter("TypeID", oContext["DataKeys"].Split(',')[1]);
            sb.Append(" And ItemID   = ").AppendParameter("ItemID", oContext["DataKeys"].Split(',')[2]);
            DataRow dr = db.ExecuteDataSet(sb.BuildCommand()).Tables[0].Rows[0];

            KindID.ucIsReadOnly = true;
            TypeID.ucIsReadOnly = true;
            if (oContext["Mode"] != "Copy")
            {
                ItemID.ucIsReadOnly = true;
            }
            KindID.ucTextData = dr["KindID"].ToString();
            TypeID.ucTextData = dr["TypeID"].ToString();
            ItemID.ucTextData = dr["ItemID"].ToString();
            IsEnabled.ucSelectedID = dr["IsEnabled"].ToString();
            IsEnabled.Refresh();
            ItemName.ucTextData = dr["ItemName"].ToString();
            ItemProp1.ucTextData = dr["ItemProp1"].ToString();
            ItemProp2.ucTextData = dr["ItemProp2"].ToString();
            ItemProp3.ucTextData = dr["ItemProp3"].ToString();
            ItemJSON.ucTextData = dr["ItemJSON"].ToString();
            ItemRemark.ucTextData = dr["ItemRemark"].ToString();
        }

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 600;
        ucModalPopup1.ucPopupHeight = 500;
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
                sb.Append("(KindID,TypeID,ItemID,IsEnabled,ItemName,ItemProp1,ItemProp2,ItemProp3,ItemJSON,ItemRemark,UpdUser,UpdDateTime)");
                sb.Append(" Values (").AppendParameter("KindID", oData["KindID"]);
                sb.Append("        ,").AppendParameter("TypeID", oData["TypeID"]);
                sb.Append("        ,").AppendParameter("ItemID", oData["ItemID"]);
                sb.Append("        ,").AppendParameter("IsEnabled", oData["IsEnabled"]);
                sb.Append("        ,").AppendParameter("ItemName", oData["ItemName"]);
                sb.Append("        ,").AppendParameter("ItemProp1", oData["ItemProp1"]);
                sb.Append("        ,").AppendParameter("ItemProp2", oData["ItemProp2"]);
                sb.Append("        ,").AppendParameter("ItemProp3", oData["ItemProp3"]);
                sb.Append("        ,").AppendParameter("ItemJSON", oData["ItemJSON"]);
                sb.Append("        ,").AppendParameter("ItemRemark", oData["ItemRemark"]);
                sb.Append("        ,").AppendParameter("UpdUser", oUser.UserID);
                sb.Append("        ,").AppendDbDateTime();
                sb.Append(")");
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_AddFail, Util.NotifyKind.Error);
                }
                break;
            case "EDIT":
                sb.Reset();
                sb.AppendStatement(string.Format(" Update {0} ", _TableName));
                sb.Append(" Set IsEnabled = ").AppendParameter("IsEnabled", oData["IsEnabled"]);
                sb.Append("    ,ItemName = ").AppendParameter("ItemName", oData["ItemName"]);
                sb.Append("    ,ItemProp1 = ").AppendParameter("ItemProp1", oData["ItemProp1"]);
                sb.Append("    ,ItemProp2 = ").AppendParameter("ItemProp2", oData["ItemProp2"]);
                sb.Append("    ,ItemProp3 = ").AppendParameter("ItemProp3", oData["ItemProp3"]);
                sb.Append("    ,ItemJSON = ").AppendParameter("ItemJSON", oData["ItemJSON"]);
                sb.Append("    ,ItemRemark = ").AppendParameter("ItemRemark", oData["ItemRemark"]);
                sb.Append("    ,UpdUser = ").AppendParameter("UpdUser", oUser.UserID);
                sb.Append("    ,UpdDateTime = ").AppendDbDateTime();
                sb.Append(" Where KindID = ").AppendParameter("KindID", oData["KindID"]);
                sb.Append("   and TypeID = ").AppendParameter("TypeID", oData["TypeID"]);
                sb.Append("   and ItemID   = ").AppendParameter("ItemID", oData["ItemID"]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteSysLog(ex);
                    Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_EditFail, Util.NotifyKind.Error);
                }
                break;
            default:
                Util.MsgBox(string.Format(SinoPac.WebExpress.Common.Properties.Resources.Msg_Undefined1, oContext["Mode"]));
                break;
        }
        ucGridView1.Refresh(true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ucGridView1.Refresh();
    }
}