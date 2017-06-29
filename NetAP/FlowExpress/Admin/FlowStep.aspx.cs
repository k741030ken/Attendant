using System;
using System.Collections;
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
using SinoPac.WebExpress.Work;

/// <summary>
/// 「流程關卡」維護
/// </summary>
public partial class FlowExpress_Admin_FlowStep : SecurePage
{
    #region 共用屬性
    private string _FlowID
    {
        get
        {
            return Util.getRequestQueryStringKey("FlowID");
        }
    }

    private string _FlowStepID
    {
        get
        {
            return Util.getRequestQueryStringKey("FlowStepID");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlNewFlowStepBtn.Visible = false;

        if (string.IsNullOrEmpty(_FlowID) || string.IsNullOrEmpty(_FlowStepID))
        {
            labMsg.Visible = true;
            pnlFlowStep.Visible = false;
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_ParaNotFound1 + "<br><br>", "FlowID/FlowStepID"));
            return;
        }

        if (!IsPostBack)
        {
            DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
            DataTable dt = db.ExecuteDataSet(string.Format("Select * From FlowStep Where FlowID = '{0}' and FlowStepID = '{1}';", _FlowID, _FlowStepID)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                fmMain.ChangeMode(FormViewMode.Edit);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                RefreshGridView(true);
            }
            else
            {
                labMsg.Visible = true;
                pnlFlowStep.Visible = false;
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_DataNotFound1, _FlowID + " - " + _FlowStepID));
                return;
            }
        }
        else
        {
            //取得觸發PostBack的控制項
            Control oCtrl = GetPostBackControl();
            if (oCtrl != null)
            {
                //若為[多語系]按鈕，需手動Refresh
                if (oCtrl.ClientID.Contains("ucMuiAdminButton1_"))
                {
                    RefreshGridView();
                }
            }
        }

        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        gvFlowStepBtn.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowStepBtn_RowCommand);
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        RefreshGridView();
    }

    void gvFlowStepBtn_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        switch (e.CommandName)
        {
            case "cmdSelect":
                Response.Redirect(string.Format("FlowStepBtn.aspx?FlowID={0}&FlowStepID={1}&FlowStepBtnID={2}", HttpUtility.HtmlEncode(e.DataKeys[0]), HttpUtility.HtmlEncode(e.DataKeys[1]), HttpUtility.HtmlEncode(e.DataKeys[2])));
                break;
            case "cmdCopy":
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];
                hidFlowStepBtnID.Value = e.DataKeys[2];
                DataRow dr = db.ExecuteDataSet(string.Format("Select * From FlowStepBtn Where FlowID='{0}' and FlowStepID = '{1}' and FlowStepBtnID = '{2}'", e.DataKeys[0], e.DataKeys[1], e.DataKeys[2])).Tables[0].Rows[0];

                txtNewFlowStepBtnID.ucTextData = "Cpy-" + dr["FlowStepBtnID"].ToString().Left(15);
                txtNewFlowStepBtnCaption.ucTextData = "Cpy-" + dr["FlowStepBtnCaption"].ToString().Left(25);
                txtNewFlowStepBtnSeqNo.ucTextData = dr["FlowStepBtnSeqNo"].ToString();

                ucModalPopup1.ucPopupHeader = "複製流程按鈕";
                ucModalPopup1.ucPanelID = pnlNewFlowStepBtn.ID;
                ucModalPopup1.Show();
                break;
            case "cmdAdd":
                hidFlowID.Value = _FlowID;
                hidFlowStepID.Value = _FlowStepID;
                hidFlowStepBtnID.Value = "";
                txtNewFlowStepBtnID.ucTextData = "New-BtnID";
                txtNewFlowStepBtnCaption.ucTextData = "New-BtnCaprion";
                txtNewFlowStepBtnSeqNo.ucTextData = "99";

                ucModalPopup1.ucPopupHeader = "新增流程按鈕";
                ucModalPopup1.ucPanelID = pnlNewFlowStepBtn.ID;
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                sb.AppendStatement("Delete FlowStepBtn           Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);
                sb.Append(" And FlowStepBtnID =").AppendParameter("FlowStepBtnID", e.DataKeys[2]);

                sb.AppendStatement("Delete FlowStepBtn_MUI       Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);
                sb.Append(" And FlowStepBtnID =").AppendParameter("FlowStepBtnID", e.DataKeys[2]);

                sb.AppendStatement("Delete FlowStepBtnHideExp    Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);
                sb.Append(" And FlowStepBtnID =").AppendParameter("FlowStepBtnID", e.DataKeys[2]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStepBtn", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    Util.MsgBox(ex.Message);
                }
                break;
        }
        (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
    }

    protected void btnMainUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);

        string strFlowStepMailToList = ((Util_ucCheckBoxList)fmMain.FindControl("chkFlowStepMailToList")).ucSelectedIDList;
        strFlowStepMailToList = strFlowStepMailToList + "," + ((Util_ucTextBox)fmMain.FindControl("txtFlowStepMailToList")).ucTextData;
        strFlowStepMailToList = Util.getStringJoin(Util.getFixList(strFlowStepMailToList.Split(',')));

        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Reset();
        sb.AppendStatement("Update FlowStep Set ");
        sb.Append("  FlowStepName        = ").AppendParameter("txtFlowStepName", oDic["txtFlowStepName"]);
        sb.Append(", FlowStepBatchEnabled    = ").AppendParameter("chkFlowStepBatchEnabled", oDic["chkFlowStepBatchEnabled"]);
        sb.Append(", FlowStepMailToList    = ").AppendParameter("FlowStepMailToList", strFlowStepMailToList);
        sb.Append(", FlowStepCustVerifyURL    = ").AppendParameter("txtFlowStepCustVerifyURL", oDic["txtFlowStepCustVerifyURL"]);
        sb.Append(", FlowStepAttachMaxQty    = ").AppendParameter("txtFlowStepAttachMaxQty", oDic["txtFlowStepAttachMaxQty"]);
        sb.Append(", FlowStepAttachMaxKB    = ").AppendParameter("txtFlowStepAttachMaxKB", oDic["txtFlowStepAttachMaxKB"]);
        sb.Append(", FlowStepAttachTotKB    = ").AppendParameter("txtFlowStepAttachTotKB", oDic["txtFlowStepAttachTotKB"]);
        sb.Append(", FlowStepAttachExtList    = ").AppendParameter("txtFlowStepAttachExtList", oDic["txtFlowStepAttachExtList"]);
        sb.Append(", Remark    = ").AppendParameter("txtRemark", oDic["txtRemark"]);

        sb.Append(", UpdUser = ").AppendParameter("UpdUser", oUser.UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append("  Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append("    And FlowStepID = ").AppendParameter("txtFlowStepID", oDic["txtFlowStepID"]);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                //[更新]資料異動Log
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStep", string.Format("{0},{1}", oDic["txtFlowID"], oDic["txtFlowStepID"]), LogHelper.AppDataLogType.Update, oDic);
                Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }

        DataTable dt = db.ExecuteDataSet(string.Format("Select * From FlowStep Where FlowID = '{0}' and FlowStepID = '{1}';", _FlowID, _FlowStepID)).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            fmMain.ChangeMode(FormViewMode.Edit);
            fmMain.DataSource = dt;
            fmMain.DataBind();
        }
        RefreshGridView();
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            Util_ucTextBox oTxt;
            Util_ucCheckBoxList oChkList;
            DbHelper dbChk = new DbHelper(FlowExpress._FlowSysDB);
            DataTable dtcHK;

            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowID"].ToString();
                oTxt.ucIsReadOnly = true;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowName", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dbChk.ExecuteDataSet(string.Format("Select FlowName From FlowSpec Where FlowID='{0}'", dr["FlowID"])).Tables[0].Rows[0][0].ToString();
                oTxt.ucIsReadOnly = true;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepID"].ToString();
                oTxt.ucIsReadOnly = true;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepName", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepName"].ToString();
            }

            ((CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBatchEnabled", true)).Checked = (dr["FlowStepBatchEnabled"].ToString().ToUpper() == "Y") ? true : false;

            //郵件指派清單
            //分成兩塊處理，一是AssignTo及流程關卡，一是額外發送的員編清單

            dtcHK = dbChk.ExecuteDataSet(string.Format("Select FlowStepID, FlowStepID + ' - ' + FlowStepName as 'FlowStepInfo' From FlowStep Where FlowID = '{0}'", dr["FlowID"])).Tables[0];
            Dictionary<string, string> oDic = new Dictionary<string, string>();
            oDic.Add("AssignTo", "AssignTo - 下一關指派對象");
            oDic.AddRange(Util.getDictionary(dtcHK));
            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowStepMailToList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                oChkList.ucSourceDictionary = oDic;
                oChkList.ucSelectedIDList = dr["FlowStepMailToList"].ToString();
                oChkList.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepMailToList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = Util.getStringJoin(Util.getCompareList(dr["FlowStepMailToList"].ToString().Split(','), Util.getArray(oDic), Util.ListCompareMode.Subset));
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepCustVerifyURL", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepCustVerifyURL"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepAttachMaxQty", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepAttachMaxQty"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepAttachMaxKB", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepAttachMaxKB"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepAttachTotKB", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepAttachTotKB"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepAttachExtList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepAttachExtList"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtRemark", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 200;
            }

            Label oLab = (Label)Util.FindControlEx(fmMain, "labUpdInfo", true);
            if (oLab != null)
            {
                oLab.Text = string.Format("{0} - {1}  on {2:yyyy\\/MM\\/dd HH:mm}", dr["UpdUser"].ToString(), UserInfo.findUserName(dr["UpdUser"].ToString()), dr["UpdDateTime"]);
            }

            Util_ucMuiAdminButton oMUI = (Util_ucMuiAdminButton)Util.FindControlEx(fmMain, "ucMuiAdminButton1", false, typeof(Util_ucMuiAdminButton));
            if (oMUI != null)
            {
                oMUI.ucDBName = FlowExpress._FlowSysDB;
                oMUI.ucTableName = "FlowStep";
                oMUI.ucPKFieldList = "FlowID,FlowStepID".Split(',');
                oMUI.ucPKValueList = (dr["FlowID"].ToString() + "," + dr["FlowStepID"].ToString()).Split(',');
            }
        }
    }

    protected void RefreshGridView(bool IsInit = false)
    {
        Dictionary<string, string> oDisp1 = new Dictionary<string, string>();

        //FlowStepBtn
        oDisp1.Clear();
        oDisp1.Add("FlowStepBtnID", "代號");
        oDisp1.Add("FlowStepBtnSeqNo", "順序");
        oDisp1.Add("FlowStepBtnCaption", "抬頭");
        oDisp1.Add("FlowStepBtnIsMultiSelect", "多選@Y");
        oDisp1.Add("FlowStepBtnIsSendMail", "發信@Y");
        oDisp1.Add("FlowStepBtnNextStepDealCondition", "成立條件");
        //oDisp1.Add("FlowStepBtnNextStepID", "下一關");
        //oDisp1.Add("FlowStepBtnNextStepName", "下一關名稱");
        oDisp1.Add("FlowStepBtnNextStepInfo", "下一關卡");
        oDisp1.Add("FlowStepBtnAddSubFlowID", "新增子流程");
        oDisp1.Add("FlowStepBtnUpdCustIDList", "資料回寫");
        //oDisp1.Add("UpdUser", "更新人員");
        //oDisp1.Add("UpdDateTime", "更新日期@T");

        gvFlowStepBtn.ucDBName = FlowExpress._FlowSysDB;
        gvFlowStepBtn.ucDataQrySQL = string.Format("Select *,FlowStepBtnNextStepID + ' - ' + FlowStepBtnNextStepName as FlowStepBtnNextStepInfo  From viewFlowSpecStepBtn Where FlowID = '{0}' And FlowStepID = '{1}' and CultureCode = '{2}' ", _FlowID, _FlowStepID, FlowExpress._FlowNativeCultureCode);
        gvFlowStepBtn.ucDataKeyList = "FlowID,FlowStepID,FlowStepBtnID".Split(',');
        gvFlowStepBtn.ucDataDisplayDefinition = oDisp1;
        gvFlowStepBtn.ucSelectEnabled = true;
        gvFlowStepBtn.ucAddEnabled = true;
        gvFlowStepBtn.ucCopyEnabled = true;
        gvFlowStepBtn.ucDeleteEnabled = true;
        gvFlowStepBtn.ucDeleteConfirm = "將同時刪除此流程按鈕所有相關資料，確定執行？";
        gvFlowStepBtn.Refresh(IsInit);
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        if (string.IsNullOrEmpty(hidFlowStepBtnID.Value))
        {
            //新增
            string strAddSQL = string.Format("Insert FlowStepBtn (FlowID,FlowStepID,FlowStepBtnID,FlowStepBtnSeqNo,FlowStepBtnCaption) Values('{0}','{1}','{2}','{3}','{4}');", hidFlowID.Value, hidFlowStepID.Value, txtNewFlowStepBtnID.ucTextData, txtNewFlowStepBtnSeqNo.ucTextData, txtNewFlowStepBtnCaption.ucTextData);
            try
            {
                db.ExecuteNonQuery(strAddSQL);
                //[新增]資料異動Log
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStepBtn", string.Format("{0},{1},{2}", hidFlowID.Value, hidFlowStepID.Value, txtNewFlowStepBtnID.ucTextData), LogHelper.AppDataLogType.Create, Util.getControlEditResult(pnlNewFlowStepBtn));
                Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
                ((FlowExpress_Admin_FlowExpess)this.Master).RefreshTreeView();  //2017.03.07 改寫法
            }
            catch
            {
                Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
            }
        }
        else
        {
            //複製
            ArrayList arTable = new ArrayList();
            ArrayList arOldPKey = new ArrayList();
            ArrayList arNewPKey = new ArrayList();
            arTable.Add("FlowStepBtn");
            arTable.Add("FlowStepBtnHideExp");

            arOldPKey.Add(hidFlowID.Value);
            arOldPKey.Add(hidFlowStepID.Value);
            arOldPKey.Add(hidFlowStepBtnID.Value);

            arNewPKey.Add(hidFlowID.Value);
            arNewPKey.Add(hidFlowStepID.Value);
            arNewPKey.Add(txtNewFlowStepBtnID.ucTextData);

            string strCopySQL = Util.getDataCopySQL(FlowExpress._FlowSysDB, "FlowID,FlowStepID,FlowStepBtnID".Split(','), Util.getArray(arOldPKey), Util.getArray(arNewPKey), Util.getArray(arTable));
            strCopySQL += string.Format("Update FlowStepBtn Set FlowStepBtnSeqNo = '{3}' , FlowStepBtnCaption = N'{4}' Where FlowID = '{0}' And FlowStepID = '{1}' And FlowStepBtnID = '{2}';", hidFlowID.Value, hidFlowStepID.Value, txtNewFlowStepBtnID.ucTextData, txtNewFlowStepBtnSeqNo.ucTextData, txtNewFlowStepBtnCaption.ucTextData);
            try
            {
                db.ExecuteNonQuery(strCopySQL);
                //[複製]資料異動Log
                DataRow drOld = db.ExecuteDataSet(string.Format("Select * from FlowStepBtn Where FlowID = '{0}' And FlowStepID = '{1}' And FlowStepBtnID = '{2}'", hidFlowID.Value, hidFlowStepID.Value, hidFlowStepBtnID.Value)).Tables[0].Rows[0];
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStepBtn", Util.getStringJoin(arNewPKey), LogHelper.AppDataLogType.Copy, Util.getDictionary(drOld));
                Util.NotifyMsg(RS.Resources.Msg_CopySucceed, Util.NotifyKind.Success);
                (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex);
                Util.NotifyMsg(RS.Resources.Msg_CopyFail, Util.NotifyKind.Error);
            }
        }
        RefreshGridView();
    }
}