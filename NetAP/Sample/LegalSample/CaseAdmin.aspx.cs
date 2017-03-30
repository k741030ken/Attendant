using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

public partial class LegalSample_CaseAdmin : SecurePage
{

    //案件資料表基本屬性
    private string _MainQrySQL = "Select CaseNo,Subject,IsUrgent,ExpectCloseDate,UpdDateTime From {0} ";
    private string[] _MainPKList = "CaseNo".Split(',');
    private string _DefSortExpression = "CaseNo";
    private string _DefSortDirection = "Desc";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //強迫初始
            ucGridView1.ucDBName = LegalSample._LegalSysDBName;
            ucGridView1.ucDataQrySQL = string.Format(_MainQrySQL, LegalSample._LegalConsultCaseTableName) + string.Format(" Where CrUser = '{0}' and CaseStatus = 'Draft' ", UserInfo.getUserInfo().UserID);
            ucGridView1.ucDataKeyList = _MainPKList;
            ucGridView1.ucDefSortExpression = _DefSortExpression;
            ucGridView1.ucDefSortDirection = _DefSortDirection;
            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();

            dicDisplay.Add("CaseNo", (string)GetLocalResourceObject("_CaseNo")); //案號
            dicDisplay.Add("Subject", (string)GetLocalResourceObject("_Subject") + "@L350"); //提案主題
            dicDisplay.Add("IsUrgent", (string)GetLocalResourceObject("_IsUrgent")); //急件
            dicDisplay.Add("ExpectCloseDate", (string)GetLocalResourceObject("_ExpectCloseDate") + "@D");  //預定結案
            dicDisplay.Add("UpdDateTime", (string)GetLocalResourceObject("_UpdDateTime") + "@T"); //最後更新


            ucGridView1.ucDataDisplayDefinition = dicDisplay;

            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucEditEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDeleteEnabled = true;

            ucGridView1.ucSelectEnabled = true;
            ucGridView1.ucSelectIcon = Util.Icon_Approve;
            ucGridView1.ucSelectToolTip = (string)GetLocalResourceObject("Msg_ToFlowVerify");  //提案送審

            ucGridView1.Refresh(true);
        }

        //事件訂閱
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        //ucModalPopup1.onCancel += new Util_ucModalPopup.btnCancelClick(ucModalPopup1_onCancel);
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        //ucModalPopup1.onComplete += new Util_ucModalPopup.btnCompleteClick(ucModalPopup1_onComplete);
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
        string strNewCaseNo = "";
        string[] oDataKeys = e.DataKeys;
        switch (e.CommandName)
        {
            case "cmdAdd":
                //新增模式
                divMainGridview.Visible = false;
                AppKey = Util.getAppKey(LegalSample._LegalSysDBName, LegalSample._LegalConsultCaseTableName);
                if (string.IsNullOrEmpty(AppKey.Item2))
                {
                    strNewCaseNo = AppKey.Item1[0];
                    UserInfo oUser = UserInfo.getUserInfo();

                    sb.Reset();
                    sb.AppendStatement(string.Format("Insert {0} ", LegalSample._LegalConsultCaseTableName));
                    sb.Append(" (CaseNo,CaseStatus,Subject,IsUrgent,CrDept,CrDeptName,CrUser,CrUserName,CrDateTime,UpdDept,UpdDeptName,UpdUser,UpdUserName,UpdDateTime )");
                    sb.Append(" Values (").AppendParameter("CaseNo", strNewCaseNo);
                    sb.Append("        ,").AppendParameter("CaseStatus", "Draft");
                    sb.Append("        ,").AppendParameter("Subject", "New Case");
                    sb.Append("        ,").AppendParameter("IsUrgent", "N");
                    sb.Append("        ,").AppendParameter("CrDept", oUser.DeptID);
                    sb.Append("        ,").AppendParameter("CrDeptName", oUser.DeptName);
                    sb.Append("        ,").AppendParameter("CrUser", oUser.UserID);
                    sb.Append("        ,").AppendParameter("CrUserName", oUser.UserName);
                    sb.Append("        ,").AppendDbDateTime();
                    sb.Append("        ,").AppendParameter("UpdDept", oUser.DeptID);
                    sb.Append("        ,").AppendParameter("UpdDeptName", oUser.DeptName);
                    sb.Append("        ,").AppendParameter("UpdUser", oUser.UserID);
                    sb.Append("        ,").AppendParameter("UpdUserName", oUser.UserName);
                    sb.Append("        ,").AppendDbDateTime();
                    sb.Append(")");

                    if (db.ExecuteNonQuery(sb.BuildCommand()) > 0)
                    {
                        dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select * From {0} Where CaseNo = '{1}' and CaseStatus = 'Draft' ", LegalSample._LegalConsultCaseTableName, strNewCaseNo)).Tables[0];
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
                AppKey = Util.getAppKey(LegalSample._LegalSysDBName, LegalSample._LegalConsultCaseTableName);
                if (string.IsNullOrEmpty(AppKey.Item2))
                {
                    strNewCaseNo = AppKey.Item1[0];
                    string strSQL = Util.getDataCopySQL(LegalSample._LegalSysDBName, "CaseNo".Split(','), e.DataKeys[0].Split(','), strNewCaseNo.Split(','), LegalSample._LegalConsultCaseTableName.Split(','));
                    try
                    {
                        strSQL += string.Format("Update {0} set CrDateTime = getdate(),UpdDateTime = getdate() Where CaseNo = '{1}';", LegalSample._LegalConsultCaseTableName, strNewCaseNo);
                        db.ExecuteNonQuery(CommandType.Text, strSQL);
                        //複製成功，直接切到編輯模式
                        divMainGridview.Visible = false;
                        sb.Reset();
                        sb.AppendStatement(string.Format("Select * From {0} Where CaseNo = ", LegalSample._LegalConsultCaseTableName)).AppendParameter("CaseNo", strNewCaseNo);
                        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                        fmMain.ChangeMode(FormViewMode.Edit);
                        fmMain.DataSource = dt;
                        fmMain.DataBind();
                        divMainFormView.Visible = true;

                        //複製成功
                        Util.NotifyMsg(string.Format(RS.Resources.Msg_CopySucceed_Edit1, strNewCaseNo), Util.NotifyKind.Success);
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
                sb.AppendStatement(string.Format("Select * From {0} Where CaseNo = ", LegalSample._LegalConsultCaseTableName)).AppendParameter("CaseNo", oDataKeys[0]);
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
                sb.AppendStatement(string.Format("Delete {0} Where CaseNo = ", LegalSample._LegalConsultCaseTableName)).AppendParameter("CaseNo", oDataKeys[0]);
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
            case "cmdSelect":
                //提案送審

                //流程第一關[A00]的指派對象，有兩種作法：
                //01.自行設定指派對象，例：
                //　　Dictionary<string, string> oAssTo = OrgExpress.getDeptOrganBoss(); //取得所屬Dept及轄下Organ所有主管
                //02.將資料設定在 流程關卡[FlowStepBtn]，再利用 getNewFlowStepBtnAssignList() 取得所需資料
                Dictionary<string, string> oAssTo = FlowExpress.getNewFlowStepBtnAssignList(LegalSample._LegalConsultFlowID, "btn2Prop");

                if (oAssTo.Count > 0)
                {
                    //若有指派對象，才開始組合新增流程 IsFlowInsVerify() 所需的參數
 
                    string strQrySQL = @"Select CaseNo,Case When LEN(subject) > 25 then SUBSTRING(Subject,1,25) + '…' else Subject end as 'Subject' 
                                        ,IsUrgent,Convert(char(10),ExpectCloseDate,111) as 'ExpectCloseDate', Convert(char(10),getdate(),111) as 'PropDate' 
                                        From {0} Where CaseNo = '{1}' and CaseStatus = 'Draft' ";
                    strQrySQL = string.Format(strQrySQL, LegalSample._LegalConsultCaseTableName, oDataKeys[0]);
                    dt = db.ExecuteDataSet(CommandType.Text, strQrySQL).Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //ex:主旨,提案单位,提案日期,急件,預定結案
                        string strShowValue = dt.Rows[0]["Subject"].ToString().Replace(",", "，") + ",";
                        strShowValue += UserInfo.getUserInfo().DeptName + ",";
                        strShowValue += dt.Rows[0]["PropDate"].ToString() + ",";
                        strShowValue += dt.Rows[0]["IsUrgent"].ToString() + ",";
                        strShowValue += (string.IsNullOrEmpty(dt.Rows[0]["ExpectCloseDate"].ToString())) ? "" : dt.Rows[0]["ExpectCloseDate"].ToString();

                        //使用 ContextData 傳遞參數
                        Dictionary<string, string> oContext = new Dictionary<string, string>();
                        oContext.Add("CaseNo", oDataKeys[0]);
                        oContext.Add("FlowID", LegalSample._LegalConsultFlowID);
                        oContext.Add("FlowKeyValue", oDataKeys[0]);
                        oContext.Add("FlowShowValue", strShowValue);
                        oContext.Add("FlowStepBtnID", "btn2Prop");

                        ucModalPopup1.Reset();
                        ucModalPopup1.ucPopupWidth = 320;
                        ucModalPopup1.ucPopupHeight = 180;
                        ucModalPopup1.ucContextData = Util.getJSON(oContext);

                        //組合彈出畫面所需顯示訊息及審核清單
                        labVerifyCaseInfo.Text = string.Format((string)GetLocalResourceObject("Msg_FlowVerifyCaseInfo"), oDataKeys[0]);
                        labVerifyAssignTo.Text = (string)GetLocalResourceObject("Msg_FlowVerifyAssignTo");
                        ddlAssignTo.Items.Clear();
                        ddlAssignTo.DataSource = Util.getDictionary(oAssTo);
                        ddlAssignTo.DataValueField = "key";
                        ddlAssignTo.DataTextField = "value";
                        ddlAssignTo.DataBind();
                        ucModalPopup1.ucPanelID = pnlVerify.ID;

                        btnVerify.Text = RS.Resources.Msg_Confirm_btnOK;
                        btnVerifyCancel.Text = RS.Resources.Msg_Confirm_btnCancel;
                        ucModalPopup1.ucBtnCloselEnabled = false;
                        ucModalPopup1.ucBtnCompleteEnabled = false;
                        ucModalPopup1.ucBtnCancelEnabled = false;
                        ucModalPopup1.Show();
                    }
                    else
                    {
                        Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound));
                    }
                }
                else
                {
                    Util.MsgBox(Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound));
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
        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit && fmMain.DataSource != null)
        {
            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            Util_ucTextBox oTxtBox;

            //設定欄位
            Util.setReadOnly("txtCaseNo", true);
            ((CheckBox)Util.FindControlEx(fmMain, "chkIsUrgent")).Checked = (dr["IsUrgent"].ToString().ToUpper() == "Y") ? true : false;
            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject");
            oTxtBox.ucTextData = dr["Subject"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispSubject").ClientID;
            oTxtBox.ucMaxLength = LegalSample._SubjectMaxLength;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucOutlined");
            oTxtBox.ucTextData = dr["Outlined"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispOutlined").ClientID;
            oTxtBox.ucMaxLength = LegalSample._OutlinedMaxLength;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucPropOpinion");
            oTxtBox.ucTextData = dr["PropOpinion"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispPropOpinion").ClientID;
            oTxtBox.ucMaxLength = LegalSample._PropOpinionMaxLength;
            oTxtBox.Refresh();

            Util_ucDatePicker oDate = (Util_ucDatePicker)Util.FindControlEx(fmMain, "ucExpectCloseDate");
            if (oDate != null && !string.IsNullOrEmpty(dr["ExpectCloseDate"].ToString()))
            {
                oDate.ucDefaultSelectedDate = DateTime.Parse(dr["ExpectCloseDate"].ToString());
            }


            //Attach 
            string strAttachID;
            string strAttachAdminURL;
            string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
            System.Web.UI.HtmlControls.HtmlControl frmObj;

            // PropAttach Frame
            strAttachID = string.Format(LegalSample._PropAttachIDFormat, dr["CaseNo"].ToString());
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, LegalSample._LegalSysDBName, strAttachID, LegalSample._PropAttachMaxQty, LegalSample._PropAttachMaxKB, LegalSample._PropAttachTotKB, LegalSample._PropAttachExtList);
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmPropAttach");
            frmObj.Attributes["width"] = "650";
            frmObj.Attributes["height"] = "450";
            frmObj.Attributes["src"] = strAttachAdminURL;

            //LegalAttach Frame
            strAttachID = string.Format(LegalSample._LegalAttachIDFormat, dr["CaseNo"].ToString());
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, LegalSample._LegalSysDBName, strAttachID, LegalSample._LegalAttachMaxQty, LegalSample._LegalAttachMaxKB, LegalSample._LegalAttachTotKB, LegalSample._LegalAttachExtList);
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmLegalAttach");
            frmObj.Attributes["width"] = "650";
            frmObj.Attributes["height"] = "450";
            frmObj.Attributes["src"] = strAttachAdminURL;

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

    //確定更新
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strCaseNo = ((TextBox)Util.FindControlEx(fmMain, "txtCaseNo")).Text;
        string strIsUrgent = ((CheckBox)Util.FindControlEx(fmMain, "chkIsUrgent")).Checked ? "Y" : "N";
        string strSubject = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject")).ucTextData;
        string strOutlined = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucOutlined")).ucTextData;
        string strPropOpinion = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucPropOpinion")).ucTextData;
        string strExpectCloseDate = ((Util_ucDatePicker)Util.FindControlEx(fmMain, "ucExpectCloseDate")).ucSelectedDate;


        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement(string.Format("Update {0} Set ", LegalSample._LegalConsultCaseTableName));
        sb.Append("  IsUrgent   = ").AppendParameter("IsUrgent", strIsUrgent);
        sb.Append(", Subject   = ").AppendParameter("Subject", strSubject);
        sb.Append(", Outlined   = ").AppendParameter("Outlined", strOutlined);
        sb.Append(", PropOpinion   = ").AppendParameter("PropOpinion", strPropOpinion);

        if (!string.IsNullOrEmpty(strExpectCloseDate))
        {
            sb.Append(", ExpectCloseDate   = ").AppendParameter("ExpectCloseDate", strExpectCloseDate);
        }
        else 
        {
            sb.Append(", ExpectCloseDate   = null ");
        }

        UserInfo oUser = UserInfo.getUserInfo();
        sb.Append(", UpdDept   = ").AppendParameter("Dept", oUser.DeptID);
        sb.Append(", UpdDeptName = ").AppendParameter("DeptName", oUser.DeptName);
        sb.Append(", UpdUser = ").AppendParameter("User", oUser.UserID);
        sb.Append(", UpdUserName = ").AppendParameter("UserName", oUser.UserName);
        sb.Append(", UpdDateTime  = ").AppendDbDateTime();
        sb.Append("  Where CaseNo = ").AppendParameter("CaseNo", strCaseNo);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success); //資料更新成功
                fmMain.DataSource = null;
                fmMain.DataBind();
                divMainFormView.Visible = false;
                ucGridView1.Refresh(true);
                divMainGridview.Visible = true;
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

    //取消更新
    protected void btnUpdateCancel_Click(object sender, EventArgs e)
    {
        fmMain.DataSource = null;
        fmMain.DataBind();
        divMainFormView.Visible = false;
        ucGridView1.Refresh();
        divMainGridview.Visible = true;
    }

    //關閉送件視窗
    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        ucModalPopup1.Hide();
        ucGridView1.Refresh();
    }

    //流程送件
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        ucModalPopup1.Hide();
        Dictionary<string, string> oContext = Util.getDictionary(ucModalPopup1.ucContextData);
        Dictionary<string, string> oAssTo = new Dictionary<string, string>();
        oAssTo.Add(ddlAssignTo.SelectedValue, UserInfo.findUserName(ddlAssignTo.SelectedValue));

        if (FlowExpress.IsFlowInsVerify(oContext["FlowID"], oContext["FlowKeyValue"].Split(','), oContext["FlowShowValue"].Split(','), oContext["FlowStepBtnID"], oAssTo, new FlowExpress(oContext["FlowID"]).FlowDefOpinion))
        {
            Util.NotifyMsg((string)GetLocalResourceObject("Msg_ToFlowVerify_Succeed"), Util.NotifyKind.Success);  //提案送審成功
            ucGridView1.Refresh(true);
        }
        else
        {
            Util.NotifyMsg((string)GetLocalResourceObject("Msg_ToFlowVerify_Fail"), Util.NotifyKind.Error);  //提案送審失敗
            ucGridView1.Refresh();
        }
    }

    //取消送件
    protected void btnVerifyCancel_Click(object sender, EventArgs e)
    {
        ucModalPopup1.Hide();
        ucGridView1.Refresh();
    }
}