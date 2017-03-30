using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;

public partial class LegalSample_DocAdmin : SecurePage
{
    #region 屬性

    /// <summary>
    /// 查詢模式參數
    /// </summary>
    private string _QryMode
    {
        get
        {
            if (ViewState["_QryMode"] == null)
            {
                ViewState["_QryMode"] = Util.getRequestQueryStringKey("QryMode", "N", true);
            }
            return (string)(ViewState["_QryMode"]);
        }
        set
        {
            ViewState["_QryMode"] = value.ToUpper();
        }
    }

    /// <summary>
    /// 查詢DocNo
    /// </summary>
    private string _DocNo
    {
        get
        {
            if (ViewState["_DocNo"] == null)
            {
                ViewState["_DocNo"] = Util.getRequestQueryStringKey("DocNo");
            }
            return (string)(ViewState["_DocNo"]);
        }
        set
        {
            ViewState["_DocNo"] = value;
        }
    }

    /// <summary>
    /// 關鍵字項目數量最小值
    /// </summary>
    private int _MinKeywordQty = 0;

    /// <summary>
    /// 關鍵字項目數量最大值
    /// </summary>
    private int _MaxKeywordQty = 3;

    /// <summary>
    /// 關鍵字項目清單
    /// </summary>
    private Dictionary<string, string> _DicKeyword
    {
        get
        {
            if (ViewState["_DicKeyword"] != null)
            {
                return (Dictionary<string, string>)ViewState["_DicKeyword"];
            }
            else
            {
                ViewState["_DicKeyword"] = Util.getCodeMap(LegalSample._LegalSysDBName, "LegalDoc", "Keyword");
                return (Dictionary<string, string>)ViewState["_DicKeyword"];
            }
        }
    }


    #endregion

    /// <summary>
    /// 本頁可供文件[編輯]及[查詢]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //平時為文件[編輯]模式
        //當收到[查詢]專用的 _QryMode / _DocNo參數時，本頁自動切換為文件顯示模式
        if (_QryMode == "Y" && !string.IsNullOrEmpty(_DocNo))
        {
            //DocQry
            DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
            DataTable dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select * From view{0} Where DocNo = '{1}' And IsRelease = 'Y' ", LegalSample._LegalDocTableName, _DocNo)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                divMainGridview.Visible = false;
                divMainFormView.Visible = true;
                fmMain.ChangeMode(FormViewMode.ReadOnly);
                fmMain.DataSource = dt;
                fmMain.DataBind();
            }
            else
            {
                divMainGridview.Visible = false;
                divMainFormView.Visible = false;
                labErrMsg.Visible = true;
                labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound, string.Format(RS.Resources.Msg_NotExist1, _DocNo));
            }
        }
        else
        {
            //DocAdmin
            if (!IsPostBack)
            {
                //初始
                ucGridView1.ucDBName = LegalSample._LegalSysDBName;
                ucGridView1.ucDataQrySQL = string.Format("Select * From view{0} ", LegalSample._LegalDocTableName);
                ucGridView1.ucDataKeyList = "DocNo".Split(',');
                ucGridView1.ucDefSortExpression = "DocNo";
                ucGridView1.ucDefSortDirection = "Desc";

                Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
                dicDisplay.Clear();
                dicDisplay.Add("DocNo", (string)GetLocalResourceObject("_DocNo")); //編號
                dicDisplay.Add("Subject", (string)GetLocalResourceObject("_Subject") + "@L250"); //主旨
                dicDisplay.Add("IsRelease", (string)GetLocalResourceObject("_IsRelease")); //發布
                dicDisplay.Add("Kind1Name", (string)GetLocalResourceObject("_Kind1Name")); //大類
                dicDisplay.Add("Kind2Name", (string)GetLocalResourceObject("_Kind2Name")); //中類
                dicDisplay.Add("Kind3Name", (string)GetLocalResourceObject("_Kind3Name")); //小類
                dicDisplay.Add("Keyword", (string)GetLocalResourceObject("_Keyword")); //關鍵字
                //dicDisplay.Add("Keyword", (string)GetLocalResourceObject("_Keyword") + "@L150," + Util.getJSON(_DicKeyword)); //關鍵字(項目清單用法１，當只需顯示時使用)
                dicDisplay.Add("UpdUserName", (string)GetLocalResourceObject("_UpdUserName")); //更新人員
                dicDisplay.Add("UpdDateTime", (string)GetLocalResourceObject("_UpdDateTime") + "@T"); //最後更新
                ucGridView1.ucDataDisplayDefinition = dicDisplay;


                Dictionary<string, string> oDicProperty = new Dictionary<string, string>();
                Dictionary<string, string> dicEdit = new Dictionary<string, string>();

                oDicProperty.Clear();
                oDicProperty.Add("ucIsRequire", "True");
                dicEdit.Add("Subject", "TextBox@" + Util.getJSON(oDicProperty));
                
                oDicProperty.Clear();
                oDicProperty.Add("ucRangeMinQty", _MinKeywordQty.ToString());
                oDicProperty.Add("ucRangeMaxQty", _MaxKeywordQty.ToString());
                oDicProperty.Add("ucRangeErrorMessage", "<br>" + string.Format((string)GetLocalResourceObject("_KeywordExceedsMaxQty"), _MaxKeywordQty)); //關鍵字最多選擇[{0}]項
                oDicProperty.Add("ucSourceDictionary", Util.getJSON(Util.getDictionary(_DicKeyword)));
                dicEdit.Add("Keyword", "CheckBoxList@" + Util.getJSON(oDicProperty)); //項目清單用法２，顯示/編輯同時適用
                
                ucGridView1.ucDataEditDefinition = dicEdit;


                ucGridView1.ucAddEnabled = true;
                ucGridView1.ucEditEnabled = true;
                ucGridView1.ucCopyEnabled = true;
                ucGridView1.ucDeleteEnabled = true;

                ucGridView1.Refresh(true);
            }
            ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
            ucGridView1.GridViewCommand += new Util_ucGridView.GridViewClick(ucGridView1_GridViewCommand);
        }
    }

    void ucGridView1_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        string strCmd = e.CommandName;
        DataTable dt = e.DataTable;
        if (dt != null && dt.Rows.Count > 0)
        {
            UserInfo oUser = UserInfo.getUserInfo();
            DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (e.CommandName)
                {
                    case "cmdUpdateAll":
                        sb.AppendStatement(string.Format("Update {0} Set DocNo = DocNo ", LegalSample._LegalDocTableName));
                        for (int j = 1; j < dt.Columns.Count; j++)
                        {
                            sb.Append(" ," + dt.Columns[j].ColumnName + " = ").AppendParameter(dt.Columns[j].ColumnName + i, dt.Rows[i][j].ToString());
                        }
                        sb.Append(" ,UpdUser = ").AppendParameter("UserID", oUser.UserID);
                        sb.Append(" ,UpdUserName = ").AppendParameter("UserName", oUser.UserName);
                        sb.Append(" ,UpdDateTime = ").AppendDbDateTime();
                        sb.Append(" Where 0=0 ");
                        sb.Append(" And DocNo  =").AppendParameter("DocNo" + i, dt.Rows[i][0].ToString().Split(',')[0]);
                        break;
                    default:
                        break;
                }
            }

            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                Util.NotifyMsg(RS.Resources.Msg_Succeed, Util.NotifyKind.Success); //處理成功
            }
            catch
            {
                tx.Rollback();
                Util.NotifyMsg(RS.Resources.Msg_Error, Util.NotifyKind.Error); //處理失敗
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
        else
        {
            Util.MsgBox(RS.Resources.Msg_DataNotFound);
        }
    }

    /// <summary>
    /// GridView RowCommand 事件訂閱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        DataTable dt = null;
        CommandHelper sb = db.CreateCommandHelper();
        var AppKey = new Tuple<string[], string>(new string[] { "" }, "");
        string strNewDocNo = "";
        string[] oDataKeys = e.DataKeys;
        switch (e.CommandName)
        {
            case "cmdAdd":
                //新增模式
                divMainGridview.Visible = false;
                AppKey = Util.getAppKey(LegalSample._LegalSysDBName, LegalSample._LegalDocTableName);
                if (string.IsNullOrEmpty(AppKey.Item2))
                {
                    strNewDocNo = AppKey.Item1[0];
                    UserInfo oUser = UserInfo.getUserInfo();

                    sb.Reset();
                    sb.AppendStatement(string.Format("Insert {0} ", LegalSample._LegalDocTableName));
                    sb.Append("(DocNo,IsRelease,Subject,CrUser,CrUserName,CrDateTime,UpdUser,UpdUserName,UpdDateTime)");
                    sb.Append(" Values (").AppendParameter("DocNo", strNewDocNo);
                    sb.Append("        ,").AppendParameter("IsRelease", "N");
                    sb.Append("        ,").AppendParameter("Subject", "New Doc");
                    sb.Append("        ,").AppendParameter("CrUser", oUser.UserID);
                    sb.Append("        ,").AppendParameter("CrUserName", oUser.UserName);
                    sb.Append("        ,").AppendDbDateTime();
                    sb.Append("        ,").AppendParameter("CrUser", oUser.UserID);
                    sb.Append("        ,").AppendParameter("CrUserName", oUser.UserName);
                    sb.Append("        ,").AppendDbDateTime();
                    sb.Append(")");

                    if (db.ExecuteNonQuery(sb.BuildCommand()) > 0)
                    {
                        dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select * From {0} Where DocNo = '{1}' ", LegalSample._LegalDocTableName, strNewDocNo)).Tables[0];
                        divMainGridview.Visible = false;
                        fmMain.ChangeMode(FormViewMode.Edit);
                        fmMain.DataSource = dt;
                        fmMain.DataBind();
                        divMainFormView.Visible = true;
                        //新增成功，直接編輯
                        Util.NotifyMsg(RS.Resources.Msg_AddSucceed_Edit, Util.NotifyKind.Success);

                    }
                    else
                    {
                        //新增失敗
                        Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
                    }
                }
                else
                {
                    //取 CaseNo 失敗
                    Util.MsgBox(AppKey.Item2);
                }
                break;
            case "cmdCopy":
                //資料複製
                AppKey = Util.getAppKey(LegalSample._LegalSysDBName, LegalSample._LegalDocTableName);
                strNewDocNo = "";
                if (string.IsNullOrEmpty(AppKey.Item2))
                {
                    strNewDocNo = AppKey.Item1[0];
                    string strSQL = Util.getDataCopySQL(LegalSample._LegalSysDBName, "DocNo".Split(','), e.DataKeys[0].Split(','), strNewDocNo.Split(','), LegalSample._LegalDocTableName.Split(','));
                    try
                    {
                        strSQL += string.Format(";Update {0} set CrDateTime = getdate(),UpdDateTime = getdate() Where DocNo = '{1}' ;", LegalSample._LegalDocTableName, strNewDocNo);
                        db.ExecuteNonQuery(CommandType.Text, strSQL);
                        //複製成功，直接切到編輯模式
                        divMainGridview.Visible = false;
                        sb.Reset();
                        sb.AppendStatement(string.Format("Select * From {0} Where DocNo = ", LegalSample._LegalDocTableName)).AppendParameter("DocNo", strNewDocNo);
                        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                        fmMain.ChangeMode(FormViewMode.Edit);
                        fmMain.DataSource = dt;
                        fmMain.DataBind();
                        divMainFormView.Visible = true;

                        //複製成功
                        Util.NotifyMsg(string.Format(RS.Resources.Msg_CopySucceed_Edit1, strNewDocNo), Util.NotifyKind.Success);
                    }
                    catch
                    {
                        //複製失敗
                        Util.NotifyMsg(RS.Resources.Msg_CopyFail, Util.NotifyKind.Error);
                    }
                }
                else
                {
                    Util.MsgBox(AppKey.Item2);
                }
                break;
            case "cmdEdit":
                //資料編輯

                sb.Reset();
                sb.AppendStatement(string.Format("Select * From {0} Where DocNo = ", LegalSample._LegalDocTableName)).AppendParameter("DocNo", oDataKeys[0]);
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                DataRow dr = dt.Rows[0];

                fmMain.ChangeMode(FormViewMode.Edit);
                fmMain.DataSource = dt;
                fmMain.DataBind();

                divMainGridview.Visible = false;
                divMainFormView.Visible = true;
                break;
            case "cmdDelete":
                //資料刪除
                sb.Reset();
                sb.AppendStatement(string.Format("Delete {0} Where DocNo = ", LegalSample._LegalDocTableName)).AppendParameter("DocNo", oDataKeys[0]);
                if (db.ExecuteNonQuery(sb.BuildCommand()) > 0)
                {
                    //刪除成功
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                else
                {
                    //刪除失敗
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            default:
                //未定義
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, e.CommandName));
                break;
        }
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {

        //System.Web.UI.HtmlControls.HtmlControl frmObj;
        Util_ucTextBox oTxtBox;
        Util_ucCheckBoxList oChkBox;

        //「查詢」模式，即 ItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.ReadOnly && fmMain.DataSource != null)
        {
            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];

            Label oLab = (Label)Util.FindControlEx(fmMain, "labKeywordResult");
            oLab.Text = "";
            if (!string.IsNullOrEmpty(dr["Keyword"].ToString()))
            {
                oLab.Text = Util.getStringJoin(Util.getArray(Util.getDictionary(_DicKeyword, dr["Keyword"].ToString().Trim().Split(',')), 1));
            }

            //Attach 
            string strAttachID;
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";

            // DocAttach Frame
            strAttachID = string.Format(LegalSample._DocAttachIDFormat, dr["DocNo"].ToString());
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            //frmDocAttachQry物件不在FormView內，可直接存取
            frmDocAttachQry.Attributes["width"] = "650";
            frmDocAttachQry.Attributes["height"] = "450";
            frmDocAttachQry.Attributes["src"] = strAttachDownloadURL;
        }

        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit && fmMain.DataSource != null)
        {
            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            //設定欄位
            Util.setReadOnly("txtDocNo", true);
            ((CheckBox)Util.FindControlEx(fmMain, "chkIsRelease")).Checked = (dr["IsRelease"].ToString().ToUpper() == "Y") ? true : false;

            Util_ucCascadingDropDown oCascading = (Util_ucCascadingDropDown)Util.FindControlEx(fmMain, "ucCascadingKind");
            oCascading.ucServiceMethod = LegalSample._LegalDocKindServiceMethod;

            oCascading.ucCategory01 = "Kind1";
            oCascading.ucCategory02 = "Kind2";
            oCascading.ucCategory03 = "Kind3";

            oCascading.ucDefaultSelectedValue01 = dr["Kind1"].ToString();
            oCascading.ucDefaultSelectedValue02 = dr["Kind2"].ToString();
            oCascading.ucDefaultSelectedValue03 = dr["Kind3"].ToString();

            oCascading.ucDropDownListEnabled01 = true;
            oCascading.ucDropDownListEnabled02 = true;
            oCascading.ucDropDownListEnabled03 = true;
            oCascading.ucDropDownListEnabled04 = false;
            oCascading.ucDropDownListEnabled05 = false;

            oCascading.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject");
            oTxtBox.ucTextData = dr["Subject"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispSubject").ClientID;
            oTxtBox.ucMaxLength = LegalSample._SubjectMaxLength;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucUsage");
            oTxtBox.ucTextData = dr["Usage"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispUsage").ClientID;
            oTxtBox.ucMaxLength = LegalSample._UsageMaxLength;
            oTxtBox.Refresh();

            oChkBox = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "ucKeyword");
            if (oChkBox != null)
            {
                oChkBox.ucRangeMinQty = _MinKeywordQty;
                oChkBox.ucRangeMaxQty = _MaxKeywordQty;
                oChkBox.ucRangeErrorMessage = "<br>" + string.Format((string)GetLocalResourceObject("_KeywordExceedsMaxQty"), _MaxKeywordQty); //關鍵字最多選擇[{0}]項
                oChkBox.ucSourceDictionary = _DicKeyword;
                oChkBox.ucSelectedIDList = dr["Keyword"].ToString();
                oChkBox.Refresh();
            }

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucRemark");
            oTxtBox.ucTextData = dr["Remark"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispRemark").ClientID;
            oTxtBox.ucMaxLength = LegalSample._RemarkMaxLength;
            oTxtBox.Refresh();

            //Attach 
            string strAttachID;
            string strAttachAdminURL;
            string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";

            // DocAttach Frame
            strAttachID = string.Format(LegalSample._DocAttachIDFormat, dr["DocNo"].ToString());
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, LegalSample._LegalSysDBName, strAttachID, LegalSample._DocAttachMaxQty, LegalSample._DocAttachMaxKB, LegalSample._DocAttachTotKB, LegalSample._DocAttachExtList);
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            //frmDocAttach物件不在FormView內，可直接存取
            frmDocAttach.Attributes["width"] = "650";
            frmDocAttach.Attributes["height"] = "450";
            frmDocAttach.Attributes["src"] = strAttachAdminURL;

            Button oBtn;
            oBtn = (Button)Util.FindControlEx(fmMain, "btnUpdate");
            if (oBtn != null)
            {
                oBtn.Text = RS.Resources.Msg_Confirm_btnOK;
            }

            oBtn = (Button)Util.FindControlEx(fmMain, "btnUpdateCancel");
            if (oBtn != null)
            {
                oBtn.Text = RS.Resources.Msg_Confirm_btnCancel;
            }
        }
    }

    /// <summary>
    /// 確定更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strDocNo = ((TextBox)Util.FindControlEx(fmMain, "txtDocNo")).Text;
        string strIsRelease = ((CheckBox)Util.FindControlEx(fmMain, "chkIsRelease")).Checked ? "Y" : "N";
        string strSubject = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject")).ucTextData;
        string strUsage = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucUsage")).ucTextData;
        string strKeyword = ((Util_ucCheckBoxList)Util.FindControlEx(fmMain, "ucKeyword")).ucSelectedIDList;
        string strRemark = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucRemark")).ucTextData;
        Util_ucCascadingDropDown oCascading = (Util_ucCascadingDropDown)Util.FindControlEx(fmMain, "ucCascadingKind");


        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement(string.Format("Update {0} Set ", LegalSample._LegalDocTableName));
        sb.Append("  IsRelease   = ").AppendParameter("IsRelease", strIsRelease);
        sb.Append(", Subject     = ").AppendParameter("Subject", strSubject);
        sb.Append(", Kind1       = ").AppendParameter("Kind1", oCascading.ucSelectedValue01);
        sb.Append(", Kind2       = ").AppendParameter("Kind2", oCascading.ucSelectedValue02);
        sb.Append(", Kind3       = ").AppendParameter("Kind3", oCascading.ucSelectedValue03);
        sb.Append(", Usage       = ").AppendParameter("Usage", strUsage);
        sb.Append(", Keyword     = ").AppendParameter("Keyword", strKeyword);
        sb.Append(", Remark      = ").AppendParameter("Remark", strRemark);

        UserInfo oUser = UserInfo.getUserInfo();
        sb.Append(", UpdUser = ").AppendParameter("User", oUser.UserID);
        sb.Append(", UpdUserName = ").AppendParameter("UserName", oUser.UserName);
        sb.Append(", UpdDateTime  = ").AppendDbDateTime();
        sb.Append("  Where DocNo = ").AppendParameter("DocNo", strDocNo);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success); //資料更新成功
                fmMain.DataSource = null;
                fmMain.DataBind();
                divMainFormView.Visible = false;
                divMainGridview.Visible = true;
                ucGridView1.Refresh(true);
            }
        }
        catch (Exception ex)
        {
            //將 Exception 丟給 Log 模組
            LogHelper.WriteSysLog(ex);
            //資料更新失敗
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
        }
    }

    /// <summary>
    /// 放棄更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateCancel_Click(object sender, EventArgs e)
    {
        fmMain.DataSource = null;
        fmMain.DataBind();
        divMainFormView.Visible = false;
        divMainGridview.Visible = true;
        ucGridView1.Refresh();
    }
}