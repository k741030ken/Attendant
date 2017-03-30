using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

public partial class LegalSample_CaseInfo : SecurePage
{
    public string _CurrCaseNo
    {
        get
        {
            if (ViewState["_CurrCaseNo"] == null)
            {
                ViewState["_CurrCaseNo"] = Util.getRequestQueryStringKey("CaseNo");
            }
            return (string)(ViewState["_CurrCaseNo"]);
        }
        set
        {
            ViewState["_CurrCaseNo"] = value;
        }
    }

    public string _CurrFlowID
    {
        get
        {
            if (ViewState["_CurrFlowID"] == null)
            {
                ViewState["_CurrFlowID"] = Util.getRequestQueryStringKey("FlowID");
            }
            return (string)(ViewState["_CurrFlowID"]);
        }
        set
        {
            ViewState["_CurrFlowID"] = value;
        }
    }

    public string _CurrFlowLogID
    {
        get
        {
            if (ViewState["_CurrFlowLogID"] == null)
            {
                ViewState["_CurrFlowLogID"] = Util.getRequestQueryStringKey("FlowLogID"); 
            }
            return (string)(ViewState["_CurrFlowLogID"]);
        }
        set
        {
            ViewState["_CurrFlowLogID"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //本頁使用時機為案件查詢及流程審核中引用(排除 CaseStatus = 'Draft')
        //流程審核時，會傳入 CaseNo / FlowID / FlowLogID
        //案件查詢時，只傳入 CaseNo 

        if (string.IsNullOrEmpty(_CurrCaseNo))
        {
            fmMain.Visible = false;
            labMsg.Visible = true;
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_ParaNotFoundList, "CaseNo") + "<br><br>");
            return;
        }

        if (!IsPostBack)
        {
            fmMainRefresh();
        }

    }

    protected void fmMainRefresh()
    {
        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        DataTable dt = null;
        CommandHelper sb = db.CreateCommandHelper();

        sb.Reset();
        sb.AppendStatement(string.Format("Select * From {0} Where CaseNo = ", LegalSample._LegalConsultCaseTableName)).AppendParameter("CaseNo", _CurrCaseNo).Append(" and CaseStatus <> 'Draft' ");
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            //DataRow dr = dt.Rows[0];
            if (string.IsNullOrEmpty(_CurrFlowID) && string.IsNullOrEmpty(_CurrFlowLogID))
            {
                fmMain.ChangeMode(FormViewMode.ReadOnly);
            }
            else
            {
                fmMain.ChangeMode(FormViewMode.Edit);
            }
            fmMain.DataSource = dt;
            fmMain.DataBind();
        }
        else
        {
            fmMain.Visible = false;
            labMsg.Visible = true;
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_DataNotFound1, _CurrCaseNo) + "<br><br>");
            return;
        }
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        //「唯讀」模式，即 ItemTemplate 範本
        // ** 配合CaseQry.aspx顯示案件資料 **
        if (fmMain.CurrentMode == FormViewMode.ReadOnly && fmMain.DataSource != null)
        {
            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            string strFrmWidth = "700";
            string strFrmHeight = "450";
            string strAttWidth = "650";
            string strAttHeight = "300";

            //Attach
            string strAttachID;
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}&Width={2}&Height={3}";
            System.Web.UI.HtmlControls.HtmlControl frmObj;

            // PropAttach Frame
            strAttachID = string.Format(LegalSample._PropAttachIDFormat, dr["CaseNo"].ToString());
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID, strAttWidth, strAttHeight);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmPropAttachQry");
            frmObj.Attributes["width"] = strFrmWidth;
            frmObj.Attributes["height"] = strFrmHeight;
            frmObj.Attributes["src"] = strAttachDownloadURL;

            //LegalAttach Frame
            strAttachID = string.Format(LegalSample._LegalAttachIDFormat, dr["CaseNo"].ToString());
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID, strAttWidth, strAttHeight);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmLegalAttachQry");
            frmObj.Attributes["width"] = strFrmWidth;
            frmObj.Attributes["height"] = strFrmHeight;
            frmObj.Attributes["src"] = strAttachDownloadURL;

            //FlowLogFrame
            string strFlowID = dr["FlowID"].ToString().Trim();
            string strFlowCaseID = dr["FlowCaseID"].ToString().Trim();
            if (!string.IsNullOrEmpty(strFlowID) && !string.IsNullOrEmpty(strFlowCaseID))
            {
                frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "FlowLogFrame");
                frmObj.Attributes["width"] = strFrmWidth;
                frmObj.Attributes["height"] = strFrmHeight;
                frmObj.Attributes["src"] = string.Format("{0}?FlowID={1}&FlowCaseID={2}", FlowExpress._FlowLogDisplayURL, strFlowID, strFlowCaseID);
            }
        }


        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit && fmMain.DataSource != null)
        {
            //判斷資料適用的模式
            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            bool IsCaseClose = (dr["CaseStatus"].ToString().ToUpper() == "CLOSE") ? true : false;
            bool IsAdjPropAll = false;   //編修提案內容及附件(配合流程)
            bool IsAdjLegelAll = false; //編修法務內容及附件(配合流程)
            bool IsAdjLegelAttach = false; //編修法務附件(配合流程)

            if (!IsCaseClose && !string.IsNullOrEmpty(_CurrFlowID) && !string.IsNullOrEmpty(_CurrFlowLogID))
            {
                FlowExpress oFlow = new FlowExpress(_CurrFlowID, _CurrFlowLogID);

                //可修改提案內容及附件的關卡清單
                string[] AdjPropAllStepList = "A11,B05".Split(',');
                //可修改法務內容及附件的關卡清單
                string[] AdjLegalAllStepList = "B30".Split(',');
                //可修改法務附件的關卡清單
                string[] AdjLegalAttStepList = "B15,B20,B25,B30".Split(',');

                if (AdjPropAllStepList.Contains(oFlow.FlowCurrStepID))
                {
                    IsAdjPropAll = true;
                }

                if (AdjLegalAllStepList.Contains(oFlow.FlowCurrStepID))
                {
                    IsAdjLegelAll = true;
                }

                if (AdjLegalAttStepList.Contains(oFlow.FlowCurrStepID))
                {
                    IsAdjLegelAttach = true;
                }
            }

            CheckBox oChkBox;
            Util_ucTextBox oTxtBox;

            //設定欄位
            Util.setReadOnly("txtCaseNo", true);

            Util_ucDatePicker oDate = (Util_ucDatePicker)Util.FindControlEx(fmMain, "ucExpectCloseDate");
            if (oDate != null && !string.IsNullOrEmpty(dr["ExpectCloseDate"].ToString()))
            {
                oDate.ucDefaultSelectedDate = DateTime.Parse(dr["ExpectCloseDate"].ToString());
            }
            if (!IsAdjPropAll || IsCaseClose) oDate.ucIsReadOnly = true;

            oChkBox = (CheckBox)Util.FindControlEx(fmMain, "chkIsUrgent");
            oChkBox.Checked = (dr["IsUrgent"].ToString().ToUpper() == "Y") ? true : false;
            if (!IsAdjPropAll || IsCaseClose) Util.setReadOnly(oChkBox, true);

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject");
            oTxtBox.ucTextData = dr["Subject"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispSubject").ClientID;
            oTxtBox.ucIsDispEnteredWords = true;
            oTxtBox.ucIsRequire = IsAdjPropAll ? true : false;
            oTxtBox.ucMaxLength = LegalSample._SubjectMaxLength;
            if (!IsAdjPropAll || IsCaseClose) oTxtBox.ucIsReadOnly = true;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucOutlined");
            oTxtBox.ucTextData = dr["Outlined"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispOutlined").ClientID;
            oTxtBox.ucIsDispEnteredWords = true;
            oTxtBox.ucIsRequire = IsAdjPropAll ? true : false;
            oTxtBox.ucMaxLength = LegalSample._OutlinedMaxLength;
            if (!IsAdjPropAll || IsCaseClose) oTxtBox.ucIsReadOnly = true;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucPropOpinion");
            oTxtBox.ucTextData = dr["PropOpinion"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispPropOpinion").ClientID;
            oTxtBox.ucIsDispEnteredWords = true;
            oTxtBox.ucIsRequire = IsAdjPropAll ? true : false;
            oTxtBox.ucMaxLength = LegalSample._PropOpinionMaxLength;
            if (!IsAdjPropAll || IsCaseClose) oTxtBox.ucIsReadOnly = true;
            oTxtBox.Refresh();

            oTxtBox = (Util_ucTextBox)Util.FindControlEx(fmMain, "ucLegalOpinion");
            oTxtBox.ucTextData = dr["LegalOpinion"].ToString();
            oTxtBox.ucDispEnteredWordsObjClientID = Util.FindControlEx(fmMain, "dispLegalOpinion").ClientID;
            oTxtBox.ucIsDispEnteredWords = true;
            oTxtBox.ucIsRequire = IsAdjLegelAttach ? true : false;
            oTxtBox.ucMaxLength = LegalSample._LegalOpinionMaxLength; ;
            if (IsCaseClose || !IsAdjLegelAttach) oTxtBox.ucIsReadOnly = true;
            oTxtBox.Refresh();

            //Attach 
            string strAttachID;
            string strAttachAdminURL;
            string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
            System.Web.UI.HtmlControls.HtmlControl frmObj;

            // PropAttach Frame
            strAttachID = string.Format(LegalSample._PropAttachIDFormat, dr["CaseNo"].ToString());
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, LegalSample._LegalSysDBName, strAttachID, LegalSample._PropAttachMaxQty, "0", LegalSample._PropAttachTotKB, "");
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmPropAttach");
            frmObj.Attributes["width"] = "650";
            frmObj.Attributes["height"] = "450";
            if (!IsCaseClose && IsAdjPropAll)
                frmObj.Attributes["src"] = strAttachAdminURL;
            else
                frmObj.Attributes["src"] = strAttachDownloadURL;

            //LegalAttach Frame
            strAttachID = string.Format(LegalSample._LegalAttachIDFormat, dr["CaseNo"].ToString());
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, LegalSample._LegalSysDBName, strAttachID, LegalSample._LegalAttachMaxQty, "0", LegalSample._LegalAttachTotKB, "");
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, LegalSample._LegalSysDBName, strAttachID);

            frmObj = (System.Web.UI.HtmlControls.HtmlControl)Util.FindControlEx(fmMain, "frmLegalAttach");
            frmObj.Attributes["width"] = "650";
            frmObj.Attributes["height"] = "450";
            if (!IsCaseClose && IsAdjLegelAttach)
                frmObj.Attributes["src"] = strAttachAdminURL;
            else
                frmObj.Attributes["src"] = strAttachDownloadURL;

            //提案修改
            Button oBtn;
            oBtn = (Button)Util.FindControlEx(fmMain, "btnUpdProp");
            if (oBtn != null)
            {
                if (IsAdjPropAll)
                {
                    oBtn.Visible = true;
                    Util.setJSContent("dom.Ready(function(){ document.getElementById('" + trLegalOpinion.ClientID + "').style.display = 'none';} );", "LegalOpinion");
                }
            }
            //法務彙整
            oBtn = (Button)Util.FindControlEx(fmMain, "btnUpdLegal");
            if (oBtn != null)
            {
                if (IsAdjLegelAll)
                {
                    oBtn.Visible = true;
                    Util.FindControlEx(fmMain, "TabLegalAttach").Visible = true;
                    Util.setJSContent("dom.Ready(function(){ document.getElementById('" + trLegalOpinion.ClientID + "').style.display = 'table-row';} );", "LegalOpinion");
                }
                else
                {
                    if (string.IsNullOrEmpty(dr["LegalOpinion"].ToString()))
                    {
                        Util.setJSContent("dom.Ready(function(){ document.getElementById('" + trLegalOpinion.ClientID + "').style.display = 'none';} );", "LegalOpinion");
                    }
                    else 
                    {
                        ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucLegalOpinion")).ucIsReadOnly = true;
                        Util.setJSContent("dom.Ready(function(){ document.getElementById('" + trLegalOpinion.ClientID + "').style.display = 'table-row';} );", "LegalOpinion");
                    }
                }
            }
            //判斷法務相關頁籤、意見是否顯示
            if (IsCaseClose || !string.IsNullOrEmpty(dr["LegalOpinion"].ToString()))
            {
                Util.FindControlEx(fmMain, "TabLegalAttach").Visible = true;
                Util.setJSContent("dom.Ready(function(){ document.getElementById('" + trLegalOpinion.ClientID + "').style.display = 'table-row';} );", "LegalOpinion");
            }
        }
    }

    /// <summary>
    /// 修改提案資料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdProp_Click(object sender, EventArgs e)
    {
        string strCaseNo = ((TextBox)Util.FindControlEx(fmMain, "txtCaseNo")).Text;
        string strIsUrgent = ((CheckBox)Util.FindControlEx(fmMain, "chkIsUrgent")).Checked ? "Y" : "N";
        string strSubject = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucSubject")).ucTextData;
        string strOutlined = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucOutlined")).ucTextData;
        string strPropOpinion = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucPropOpinion")).ucTextData;
        string strExpectCloseDate = ((Util_ucDatePicker)Util.FindControlEx(fmMain, "ucExpectCloseDate")).ucSelectedDate;


        //ex:主旨,提案单位,提案日期,急件,預定結案
        //Subject,PropDept,PropDate,IsUrgent,ExpectCloseDate
        //變更可能會變動的ShowValue (Subject,IsUrgent,ExpectCloseDate)
        FlowExpress oFlow = new FlowExpress(_CurrFlowID, _CurrFlowLogID);
        string strChgShowValueList = strSubject.Replace(",", "，") + "," + strIsUrgent + "," + ((string.IsNullOrEmpty(strExpectCloseDate)) ? "" : strExpectCloseDate);
        FlowExpress.IsFlowCaseValueChanged(oFlow.FlowID, oFlow.FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowShowValueList, strChgShowValueList.Split(','), "Subject,IsUrgent,ExpectCloseDate".Split(','));


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
                //資料更新成功
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                fmMainRefresh();
            }
        }
        catch (Exception ex)
        {
            //將 Exception 丟給 Log 模組
            LogHelper.WriteSysLog(ex);
            //資料更新失敗
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
            fmMainRefresh();
        }
    }

    /// <summary>
    /// 修改法務意見
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdLegal_Click(object sender, EventArgs e)
    {
        string strCaseNo = ((TextBox)Util.FindControlEx(fmMain, "txtCaseNo")).Text;
        string strLegalOpinion = ((Util_ucTextBox)Util.FindControlEx(fmMain, "ucLegalOpinion")).ucTextData;

        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement(string.Format("Update {0} Set ", LegalSample._LegalConsultCaseTableName));
        sb.Append(" LegalOpinion   = ").AppendParameter("LegalOpinion", strLegalOpinion);

        UserInfo oUser = UserInfo.getUserInfo();
        sb.Append(", UpdLegalUser = ").AppendParameter("UserID", oUser.UserID);
        sb.Append(", UpdLegalUserName = ").AppendParameter("UserName", oUser.UserName);
        sb.Append(", UpdLegalDateTime  = ").AppendDbDateTime();
        sb.Append("  Where CaseNo = ").AppendParameter("CaseNo", strCaseNo);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                //資料更新成功
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                fmMainRefresh();
            }
        }
        catch (Exception ex)
        {
            //將 Exception 丟給 Log 模組
            LogHelper.WriteSysLog(ex);
            //資料更新失敗
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
            fmMainRefresh();
        }
    }
}