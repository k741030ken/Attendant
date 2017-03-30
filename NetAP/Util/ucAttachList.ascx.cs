using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [附件清單]控制項
/// </summary>
public partial class Util_ucAttachList : BaseUserControl
{
    /// <summary>
    /// 附檔資料庫
    /// </summary>
    public string ucAttachDB
    {
        get
        {
            if (ViewState["_AttachDB"] == null)
            {
                ViewState["_AttachDB"] = "";
            }
            return (string)(ViewState["_AttachDB"]);
        }
        set
        {
            ViewState["_AttachDB"] = value;
        }
    }

    /// <summary>
    /// 附檔鍵值
    /// </summary>
    public string ucAttachID
    {
        get
        {
            if (ViewState["_AttachID"] == null)
            {
                ViewState["_AttachID"] = "";
            }
            return (string)(ViewState["_AttachID"]);
        }
        set
        {
            ViewState["_AttachID"] = value;
        }
    }

    /// <summary>
    /// 是否為編輯模式(預設為 false)
    /// </summary>
    public bool ucIsEditMode
    {
        get
        {
            if (ViewState["_IsEditMode"] == null)
            {
                ViewState["_IsEditMode"] = false;
            }
            return (bool)(ViewState["_IsEditMode"]);
        }
        set
        {
            ViewState["_IsEditMode"] = value;
        }
    }

    /// <summary>
    /// 控制項寬度(預設 0，即自動計算)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null)
            {
                ViewState["_Width"] = 0;
            }
            return (int)(ViewState["_Width"]);
        }
        set
        {
            ViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 控制項高度(預設 0，即自動計算)
    /// </summary>
   public int ucHeight
    {
        get
        {
            if (ViewState["_Height"] == null)
            {
                ViewState["_Height"] = 0;
            }
            return (int)(ViewState["_Height"]);
        }
        set
        {
            ViewState["_Height"] = value;
        }
    }

    /// <summary>
    /// [附件刪除] 事件參數
    /// </summary>
    public class AttachDeletedEventArgs : EventArgs
    {
        string _AttachDB = string.Empty;
        string _AttachID = string.Empty;
        int _SeqNo;
        /// <summary>
        /// 附件資料庫
        /// </summary>
        public string AttachDB
        {
            set { _AttachDB = value; }
            get { return _AttachDB; }
        }

        /// <summary>
        /// 附檔鍵值
        /// </summary>
        public string AttachID
        {
            set { _AttachID = value; }
            get { return _AttachID; }
        }

        /// <summary>
        /// 附檔序號
        /// </summary>
        public int SeqNo
        {
            set { _SeqNo = value; }
            get { return _SeqNo; }
        }
    }

    /// <summary>
    /// [附件刪除] 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void AttachDeletedClick(object sender, AttachDeletedEventArgs e);

    /// <summary>
    /// [附件刪除] 事件
    /// </summary>
    public event AttachDeletedClick AttachDeleted;

    protected void Page_Load(object sender, EventArgs e)
    {
        //偵測控制項是否被放入 UpdatePanel Andrew Sun. 2013.05.03
        Page oPage = (Page)HttpContext.Current.CurrentHandler;
        ScriptManager oManager = ToolkitScriptManager.GetCurrent(oPage);
        if (oManager != null)
        {
            oManager.RegisterPostBackControl(gvAttach);
        }
        DivAttachList.Attributes.Add("style", string.Format("border: 0px solid; overflow: auto; width: {0}; height: {1};padding-bottom:1px;", (ucWidth <= 0) ? "auto" : ucWidth + "px", (ucHeight <= 0) ? "auto" : ucHeight + "px"));

        gvAttach.EmptyDataText = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound);

        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(ucAttachDB) && !string.IsNullOrEmpty(ucAttachID))
            {
                //若屬性有值才自動Refresh()
                string strAttachDB = ucAttachDB;
                string strAttachID = ucAttachID;
                bool bolIsEditMode = ucIsEditMode;
                Refresh(strAttachDB, strAttachID, bolIsEditMode);
            }
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    /// <param name="strAttachDB"></param>
    /// <param name="strAttachID"></param>
    /// <param name="bolIsEditMode"></param>
    public void Refresh(string strAttachDB, string strAttachID, bool bolIsEditMode)
    {
        if (string.IsNullOrEmpty(strAttachDB) || string.IsNullOrEmpty(strAttachID))
        {
            DivError.Visible = true;
            DivAttachList.Visible = false;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaDataError);
        }
        else
        {
            string strSQL = "Select '{0}' as AttachDB,'{2}' as IsEditMode, AttachID,SeqNo,FileName, CAST(FileSize/1024.0 AS DECIMAL(10,2)) as 'FileSizeKB' ,AnonymousAccess,MD5Check ";
            strSQL += ", LEFT(UpdDate,4) + '/' + SUBSTRING(UpdDate,5,2) + '/' + Right(UpdDate,2) + '-' + UpdTime as UpdDateInfo From AttachInfo Where AttachID = '{1}' And FileSize > 0 ";
            strSQL = string.Format(strSQL, strAttachDB, strAttachID, bolIsEditMode ? "Y" : "N");
            DbHelper db = new DbHelper(strAttachDB);
            DataTable dtAttach = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
            gvAttach.DataSource = dtAttach;
            gvAttach.DataBind();
        }
    }

    protected void gvAttach_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intIndex = gvAttach.SelectedIndex;
        string strAttachDB = gvAttach.DataKeys[intIndex].Values[0].ToString();
        string strAttachID = gvAttach.DataKeys[intIndex].Values[1].ToString();
        int intSeqNo = int.Parse(gvAttach.DataKeys[intIndex].Values[2].ToString());
        string strAnonymousAccess = gvAttach.DataKeys[intIndex].Values[3].ToString();

        string strUserID = "";
        if (Session["UserID"] != null) strUserID = Session["UserID"].ToString().Trim();
        bool IsAllowAccess = true;
        bool IsAccessMD5 = false;
        //檢查是否許匿名存取
        if (strAnonymousAccess.ToUpper() == "N" && string.IsNullOrEmpty(strUserID)) IsAllowAccess = false;

        //當不能匿名存取，但傳入參數符合「檢核碼」規範時，則允許存取   2013.07.25 新增
        if (IsAllowAccess == false)
        {
            if (!string.IsNullOrEmpty(Util.getRequestQueryStringKey("UserID")) && !string.IsNullOrEmpty(Util.getRequestQueryStringKey("AccessMD5")))
            {
                //產生檢核碼( yyyyMMdd + UserID + AttachDB + AttachID)
                string strMD5Chk = Util.getMD5Hash(DateTime.Today.ToString("yyyyMMdd") + Util.getRequestQueryStringKey("UserID") + strAttachDB + strAttachID);
                if (Util.getRequestQueryStringKey("AccessMD5","",true) == strMD5Chk.ToUpper())
                {
                    Session["UserID"] = Util.getRequestQueryStringKey("UserID");
                    IsAccessMD5 = true;
                    IsAllowAccess = true;
                }
            }
        }

        if (IsAllowAccess)
        {
            if (Util.IsAttachInfoLog(strAttachDB, strAttachID, intSeqNo))
            {
                string strSQL = string.Format("Select * from AttachInfo Where AttachID = '{0}' And SeqNo ='{1}' ;", strAttachID, intSeqNo);

                DbHelper db = new DbHelper(strAttachDB);
                DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    string strFileName = dt.Rows[0]["Filename"].ToString();
                    Byte[] binFileBody = (Byte[])dt.Rows[0]["FileBody"];
                    //將下載檔案設定為 FileInfoObj 物件 2016.11.24
                    if (FileInfoObj.setFileInfoObj(strFileName, binFileBody, true))
                    {
                        //直接下載
                        //Util.setJSContent(_defPopCloseClientJS, this.ClientID + "PopBody_Close");
                        if (!FileInfoObj.DirectDownload())
                        {
                            Util.NotifyMsg(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error);
                        }
                    }
                    else
                    {
                        //匯出資料設為 FileInfoObj 失敗
                        Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error);
                    }
                }
            }
            //若為暫時允許匿名存取，需還原Session
            if (IsAccessMD5)
            {
                Session["UserID"] = null;
            }
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Attach_AccessDenied, Util.NotifyKind.Error);
        }
    }

    protected void gvAttach_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //標題列
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //欄位抬頭
            e.Row.Cells[Util.getGridviewColumnIndexByDataField(gvAttach, "FileName")].Text = RS.Resources.Attach_FileListHeader_FileName; //檔案名稱
            e.Row.Cells[Util.getGridviewColumnIndexByDataField(gvAttach, "FileSizeKB")].Text = RS.Resources.Attach_FileListHeader_FileSizeKB; //檔案大小
            e.Row.Cells[Util.getGridviewColumnIndexByDataField(gvAttach, "UpdDateInfo")].Text = RS.Resources.Attach_FileListHeader_UpdDateInfo; //更新資訊
        }

        //資料列
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //嘗試取得目前資料列的 GridView 鍵值，並放到 CommandArgument
            //  （鍵值欄位是使用 GridView 的 DataKeyNames 屬性來定義，鍵值可為單一或是多個欄位，
            //    譬如 DataKeyNames="CompID,DeptID,UserID" ）
            string[] DataKeyValues = new string[gvAttach.DataKeyNames.Count()];
            for (int i = 0; i < gvAttach.DataKeyNames.Count(); i++)
            {
                DataKeyValues[i] = gvAttach.DataKeys[e.Row.RowIndex].Values[i].ToString();
            }

            ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
            ImageButton btnSelect = (ImageButton)e.Row.FindControl("btnSelect");
            btnDelete.CommandArgument = Util.getStringJoin(DataKeyValues);
            btnDelete.ImageUrl = Util.Icon_Delete;
            btnDelete.ToolTip = RS.Resources.AttachList_Delete; //檔案刪除
            btnDelete.OnClientClick = string.Format("return confirm('{0}');", RS.Resources.AttachList_Delete);
            btnDelete.Visible = (DataKeyValues[4].ToUpper() == "Y") ? true : false; //根據 IsEditMode 判斷是否顯示

            btnSelect.CommandArgument = Util.getStringJoin(DataKeyValues);
            btnSelect.ImageUrl = Util.Icon_Download;
            btnSelect.ToolTip = RS.Resources.AttachList_Select;  //檔案下載
            btnSelect.OnClientClick = ucLightBox.ucShowClientJS; //燈箱控制項 2017.01.13
            btnSelect.Visible = true;

            GridView gv = (GridView)sender;
            DataTable dt = (DataTable)gv.DataSource;
            string strKey = dt.Rows[e.Row.RowIndex]["SeqNo"].ToString().PadLeft(2, '0');
            string strMD5 = dt.Rows[e.Row.RowIndex]["MD5Check"].ToString();
            e.Row.Cells[Util.getGridviewColumnIndexByDataField(gv, "FileSizeKB")].ToolTip = string.Format("Seq=[{0}]\nMD5=[{1}]", strKey, strMD5);
        }
    }

    protected void gvAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //處理使用 ImageButton 物件發動的自訂命令
        if (e.CommandSource.GetType() == typeof(ImageButton))
        {
            int intIndex = ((GridViewRow)((ImageButton)e.CommandSource).NamingContainer).RowIndex;
            if (intIndex >= 0)
            {
                //從 CommandArgument 取出鍵值陣列
                string[] DataKeyValues = new string[e.CommandArgument.ToString().Split(',').Count()];
                string strAttachDB = e.CommandArgument.ToString().Split(',')[0];
                string strAttachID = e.CommandArgument.ToString().Split(',')[1];
                int intSeqNo = int.Parse(e.CommandArgument.ToString().Split(',')[2]);
                string strAnonymousAccess = e.CommandArgument.ToString().Split(',')[3];
                string strIsEditMode = e.CommandArgument.ToString().Split(',')[4];

                //處理自訂命令，AP 可視需要自行增加想要的 CommandName
                switch (e.CommandName)
                {
                    case "cmdSelect":
                        //選擇檔案
                        string strUserID = "";
                        if (Session["UserID"] != null) strUserID = Session["UserID"].ToString().Trim();
                        bool IsAllowAccess = true;
                        bool IsAccessMD5 = false;
                        //檢查是否許匿名存取
                        if (strAnonymousAccess.ToUpper() == "N" && string.IsNullOrEmpty(strUserID)) IsAllowAccess = false;

                        //當不能匿名存取，但傳入參數符合「存取檢核碼」規範時，則允許存取   2013.07.25 新增
                        //「存取檢核碼」適用當 URL 放到 Mail ，又允許下載的情況中
                        if (IsAllowAccess == false)
                        {
                            if (!string.IsNullOrEmpty(Util.getRequestQueryStringKey("UserID")) && !string.IsNullOrEmpty(Util.getRequestQueryStringKey("AccessMD5")))
                            {
                                //產生檢核碼( yyyyMMdd + UserID + AttachDB + AttachID)
                                string strMD5Chk = Util.getMD5Hash(DateTime.Today.ToString("yyyyMMdd") + Util.getRequestQueryStringKey("UserID") + strAttachDB + strAttachID);
                                if (Util.getRequestQueryStringKey("AccessMD5", "", true) == strMD5Chk.ToUpper())
                                {
                                    Session["UserID"] = Util.getRequestQueryStringKey("UserID");
                                    IsAccessMD5 = true;
                                    IsAllowAccess = true;
                                }
                            }
                        }

                        if (IsAllowAccess)
                        {
                            if (Util.IsAttachInfoLog(strAttachDB, strAttachID, intSeqNo))
                            {
                                string strSQL = string.Format("Select * from AttachInfo Where AttachID = '{0}' And SeqNo ='{1}' ;", strAttachID, intSeqNo);

                                DbHelper db = new DbHelper(strAttachDB);
                                DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string strFileName = dt.Rows[0]["Filename"].ToString();
                                    Byte[] binFileBody = (Byte[])dt.Rows[0]["FileBody"];
                                    //將下載檔案設定為 FileInfoObj 物件 2016.11.24
                                    if (FileInfoObj.setFileInfoObj(strFileName, binFileBody, true))
                                    {
                                        //直接下載
                                        //Util.setJSContent(_defPopCloseClientJS, this.ClientID + "PopBody_Close");
                                        if (!FileInfoObj.DirectDownload())
                                        {
                                            Util.NotifyMsg(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error);
                                        }
                                    }
                                    else
                                    {
                                        //匯出資料設為 FileInfoObj 失敗
                                        Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error);
                                    }
                                }
                            }
                            //若為暫時允許匿名存取，需還原Session
                            if (IsAccessMD5)
                            {
                                Session["UserID"] = null;
                            }
                        }
                        else
                        {
                            Util.NotifyMsg(RS.Resources.Attach_AccessDenied, Util.NotifyKind.Error);
                        }
                        ucLightBox.Hide();  //燈箱控制項 2017.01.13
                        break;
                    case "cmdDelete":
                        //刪除檔案
                        string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' And SeqNo = '{1}' ;", strAttachID, intSeqNo);
                        DbHelper dbDel = new DbHelper(strAttachDB);
                        if (dbDel.ExecuteNonQuery(CommandType.Text, strDelSQL) >= 0)
                        {
                            Util.IsAttachInfoLog(strAttachDB, strAttachID, intSeqNo, "Delete");
                            Refresh(strAttachDB, strAttachID, strIsEditMode == "Y" ? true : false);
                        }
                        //事件處理
                        AttachDeletedEventArgs eArgs = new AttachDeletedEventArgs();
                        eArgs.AttachDB = strAttachDB;
                        eArgs.AttachID = strAttachID;
                        eArgs.SeqNo = intSeqNo;
                        if (AttachDeleted != null)
                        {
                            AttachDeleted(this, eArgs);
                        }
                        break;
                    default:
                        //未定義的命令
                        Util.MsgBox(string.Format("[{0}] - {1}", e.CommandName, RS.Resources.Msg_Undefined));
                        break;
                }
            }
        }
    }
}