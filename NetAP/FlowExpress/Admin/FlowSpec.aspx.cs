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
/// 「流程規格」維護
/// </summary>
public partial class FlowExpress_Admin_FlowSpec : SecurePage
{
    #region 共用屬性
    private string _FlowID
    {
        get
        {
            return Util.getRequestQueryStringKey("FlowID");
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlCustDataExp.Visible = false;
        pnlGrp.Visible = false;
        pnlGrpDetail.Visible = false;
        pnlNewFlowStep.Visible = false;
        pnlFlowSQL.Visible = false;

        Util.setRequestValidatorBypassIDList("*");

        if (string.IsNullOrEmpty(_FlowID))
        {
            labMsg.Visible = true;
            pnlFlowSpec.Visible = false;
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_ParaNotFoundList + "<br><br>", "FlowID"));
            return;
        }

        if (!IsPostBack)
        {
            DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
            DataTable dt = db.ExecuteDataSet(string.Format("Select * From FlowSpec Where FlowID = '{0}';", _FlowID)).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                //檢查 AppKeyMap 是否已有資料，並自動新增 2015.08.18
                int intKeyMap = (int)db.ExecuteScalar(string.Format("Select count(*) From AppKeyMap Where KeyID = '{0}';", _FlowID));
                if (intKeyMap < 1)
                {
                    string strKeyMapSQL = "Insert AppKeyMap (KeyID,KeyBaseDate,IsLock,SeqNoLen,LastSeqNo,KeyFormat,Remark) Values ";
                    strKeyMapSQL += "('" + _FlowID + "','20150101','N'," + FlowExpress._FlowCaseSeqLen + ",0,'{0}.{1}','" + dt.Rows[0]["FlowName"].ToString() + "')";
                    db.ExecuteNonQuery(strKeyMapSQL);
                }

                fmMain.ChangeMode(FormViewMode.Edit);
                fmMain.DataSource = dt;
                fmMain.DataBind();
                RefreshGridView(true);
            }
            else
            {
                labMsg.Visible = true;
                pnlFlowSpec.Visible = false;
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_DataNotFound1, _FlowID));
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

        string strJS = "";
        strJS += "var oID = document.getElementById('" + txtNewFlowStepID.ClientID + "_txtData');";
        strJS += "if (oID != null) ";
        strJS += "{ ";
        strJS += "    var chkList = '" + Util.getStringJoin(FlowExpress._FlowNativeCloseStepIDList) + "'; ";
        strJS += "    if (chkList.indexOf(oID.value.trim().toUpperCase()) > - 1) {alert('關卡代號與系統保留關卡「' + chkList + '」發生衝突！');return false;} ";
        strJS += "} ";
        btnComplete.OnClientClick = strJS;

        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        gvFlowStep.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowStep_RowCommand);
        gvFlowUpdCustDataExp.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowUpdCustDataExp_RowCommand);
        gvFlowCustGrp.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowCustGrpMain_RowCommand);
        gvFlowCustGrpDetail.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowCustGrpDetail_RowCommand);
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        RefreshGridView();
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            Util_ucTextBox oTxt;
            Util_ucCheckBoxList oChkList;

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
                oTxt.ucTextData = dr["FlowName"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 50;
            }

            ((CheckBox)Util.FindControlEx(fmMain, "chkFlowTraceEnabled", true)).Checked = (dr["FlowTraceEnabled"].ToString().ToUpper() == "Y") ? true : false;

            ((CheckBox)Util.FindControlEx(fmMain, "chkFlowBatchEnabled", true)).Checked = (dr["FlowBatchEnabled"].ToString().ToUpper() == "Y") ? true : false;

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowKeyFieldList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowKeyFieldList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 50;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowKeyCaptionList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowKeyCaptionList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 50;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowShowFieldList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowShowFieldList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 150;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowShowCaptionList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowShowCaptionList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 150;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowCustDB", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowCustDB"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 30;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowLogDB", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowLogDB"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 30;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowAttachDB", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowAttachDB"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 30;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowOpinionMaxLength", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowOpinionMaxLength"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowVerifyPopupWidth", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowVerifyPopupWidth"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowVerifyPopupHeight", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowVerifyPopupHeight"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowVerifyCustFormURL", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowVerifyCustFormURL"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 100;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowCustVarNameList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowCustVarNameList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 200;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowDefCustVarValueList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowDefCustVarValueList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 200;
            }

            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowHideOpinionStepList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                DbHelper dbChk = new DbHelper(FlowExpress._FlowSysDB);
                DataTable dtcHK = dbChk.ExecuteDataSet(string.Format("Select FlowStepID, FlowStepID + ' - ' + FlowStepName as 'FlowStepInfo' From FlowStep Where FlowID = '{0}'", dr["FlowID"])).Tables[0];
                oChkList.ucSourceDictionary = Util.getDictionary(dtcHK);
                oChkList.ucSelectedIDList = dr["FlowHideOpinionStepList"].ToString();
                oChkList.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowHideOpinionIgnoreList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowHideOpinionIgnoreList"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 200;
            }

            //FlowSendMailNewValueList
            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowSendMailNewValueList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                oDic.AddRange(Util.getCodeMap(FlowExpress._FlowSysDB, "FlowSpec", "SendMailNewValue"));

                for (int i = 0; i < dr["FlowKeyFieldList"].ToString().Split(',').Count(); i++)
                {
                    oDic.Add(dr["FlowKeyFieldList"].ToString().Split(',')[i], dr["FlowKeyFieldList"].ToString().Split(',')[i]);
                }

                for (int i = 0; i < dr["FlowShowFieldList"].ToString().Split(',').Count(); i++)
                {
                    if (!oDic.ContainsKey(dr["FlowShowFieldList"].ToString().Split(',')[i]))
                    {
                        oDic.Add(dr["FlowShowFieldList"].ToString().Split(',')[i], dr["FlowShowFieldList"].ToString().Split(',')[i]);
                    }
                }

                oChkList.ucSourceDictionary = oDic;
                oChkList.ucSelectedIDList = dr["FlowSendMailNewValueList"].ToString();
                oChkList.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowSendMailSubjectFormat", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowSendMailSubjectFormat"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 100;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowSendMailBodyFormat", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowSendMailBodyFormat"].ToString();
                oTxt.ucIsDispEnteredWords = true;
                oTxt.ucMaxLength = 500;
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
                oMUI.ucTableName = "FlowSpec";
                oMUI.ucPKFieldList = "FlowID".Split(',');
                oMUI.ucPKValueList = dr["FlowID"].ToString().Split(',');
            }
        }
    }

    protected void btnCustProperty_Click(object sender, EventArgs e)
    {
        if (!Util.getRequestQueryString().IsNullOrEmpty("FlowID"))
        {
            ucModalPopup1.Reset();
            ucModalPopup1.ucPopupWidth = 750;
            ucModalPopup1.ucPopupHeight = 450;
            ucModalPopup1.ucPopupHeader = "自訂屬性維護";
            ucModalPopup1.ucFrameURL = "FlowCustProperty.aspx?FlowID=" + Util.getRequestQueryString()["FlowID"];
            ucModalPopup1.Show();
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Msg_ParaError, Util.NotifyKind.Error);
        }
    }
    protected void btnFlowSQL_Click(object sender, EventArgs e)
    {
        if (!Util.getRequestQueryString().IsNullOrEmpty("FlowID"))
        {
            FlowExpress oFlow = new FlowExpress();
            string strFilePath = Server.MapPath("./Schema.tmp");
            if (System.IO.File.Exists(strFilePath))
            {
                string strContent = System.IO.File.ReadAllText(strFilePath);
                if (!string.IsNullOrEmpty(oFlow.FlowLogDB))
                {
                    strContent = string.Format("Use {0} \nGo\n", oFlow.FlowLogDB) + strContent;
                }
                txtFlowSQL.ucTextData = strContent.Replace("(xxx)", oFlow.FlowID);
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 850;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucPopupHeader = string.Format(" [ {0} ] 相關SQL", oFlow.FlowID);
                ucModalPopup1.ucPanelID = pnlFlowSQL.ID;
                ucModalPopup1.Show();
            }
            else
            {
                Util.NotifyMsg(RS.Resources.Msg_DataNotFound, Util.NotifyKind.Error);
            }
        }
        else
        {
            Util.NotifyMsg(RS.Resources.Msg_ParaError, Util.NotifyKind.Error);
        }
    }

    protected void btnMainUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);

        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Reset();
        sb.AppendStatement("Update FlowSpec Set ");
        sb.Append("  FlowName            = ").AppendParameter("txtFlowName", oDic["txtFlowName"]);
        sb.Append(", FlowTraceEnabled    = ").AppendParameter("chkFlowTraceEnabled", oDic["chkFlowTraceEnabled"]);
        sb.Append(", FlowBatchEnabled    = ").AppendParameter("chkFlowBatchEnabled", oDic["chkFlowBatchEnabled"]);
        sb.Append(", FlowKeyFieldList    = ").AppendParameter("txtFlowKeyFieldList", oDic["txtFlowKeyFieldList"]);
        sb.Append(", FlowKeyCaptionList  = ").AppendParameter("txtFlowKeyCaptionList", oDic["txtFlowKeyCaptionList"]);
        sb.Append(", FlowShowFieldList   = ").AppendParameter("txtFlowShowFieldList", oDic["txtFlowShowFieldList"]);
        sb.Append(", FlowShowCaptionList = ").AppendParameter("txtFlowShowCaptionList", oDic["txtFlowShowCaptionList"]);

        sb.Append(", FlowCustDB          = ").AppendParameter("txtFlowCustDB", oDic["txtFlowCustDB"]);
        sb.Append(", FlowLogDB           = ").AppendParameter("txtFlowLogDB", oDic["txtFlowLogDB"]);
        sb.Append(", FlowAttachDB        = ").AppendParameter("txtFlowAttachDB", oDic["txtFlowAttachDB"]);

        sb.Append(", FlowVerifyCustFormURL = ").AppendParameter("txtFlowVerifyCustFormURL", oDic["txtFlowVerifyCustFormURL"]);
        sb.Append(", FlowOpinionMaxLength  = ").AppendParameter("txtFlowOpinionMaxLength", oDic["txtFlowOpinionMaxLength"]);
        sb.Append(", FlowVerifyPopupWidth  = ").AppendParameter("txtFlowVerifyPopupWidth", oDic["txtFlowVerifyPopupWidth"]);
        sb.Append(", FlowVerifyPopupHeight = ").AppendParameter("txtFlowVerifyPopupHeight", oDic["txtFlowVerifyPopupHeight"]);

        sb.Append(", FlowCustVarNameList = ").AppendParameter("txtFlowCustVarNameList", oDic["txtFlowCustVarNameList"]);
        sb.Append(", FlowDefCustVarValueList = ").AppendParameter("txtFlowDefCustVarValueList", oDic["txtFlowDefCustVarValueList"]);
        sb.Append(", FlowHideOpinionStepList = ").AppendParameter("chkFlowHideOpinionStepList", oDic["chkFlowHideOpinionStepList"]);
        sb.Append(", FlowHideOpinionIgnoreList = ").AppendParameter("txtFlowHideOpinionIgnoreList", oDic["txtFlowHideOpinionIgnoreList"]);
        sb.Append(", FlowSendMailNewValueList  = ").AppendParameter("chkFlowSendMailNewValueList", oDic["chkFlowSendMailNewValueList"]);
        sb.Append(", FlowSendMailSubjectFormat = ").AppendParameter("txtFlowSendMailSubjectFormat", oDic["txtFlowSendMailSubjectFormat"]);
        sb.Append(", FlowSendMailBodyFormat = ").AppendParameter("txtFlowSendMailBodyFormat", oDic["txtFlowSendMailBodyFormat"]);
        sb.Append(", Remark = ").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append(", UpdUser = ").AppendParameter("UpdUser", oUser.UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append("  Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);

        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[更新]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowSpec", string.Format("{0}", oDic["txtFlowID"]), LogHelper.AppDataLogType.Update, oDic);

            string[] chkSameList = Util.getCompareList(oDic["txtFlowKeyFieldList"].Split(','), oDic["txtFlowShowFieldList"].Split(','), Util.ListCompareMode.Same);
            if (chkSameList != null && chkSameList.Count() > 0 && !string.IsNullOrEmpty(chkSameList[0]))
                Util.MsgBox(string.Format("資料已儲存，但「{0}」被重複定義在KeyFieldList及ShowFieldList，請調整！", Util.getStringJoin(chkSameList)));
            else
                Util.NotifyMsg("更新成功", Util.NotifyKind.Success);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
        RefreshGridView();
    }

    protected void RefreshGridView(bool IsInit = false)
    {
        Dictionary<string, string> oDisp1 = new Dictionary<string, string>();
        Dictionary<string, string> oDisp2 = new Dictionary<string, string>();
        Dictionary<string, string> oDisp3 = new Dictionary<string, string>();
        Dictionary<string, string> oDisp4 = new Dictionary<string, string>();
        //FlowStep
        oDisp1.Clear();
        oDisp1.Add("FlowStepID", "關卡代號");
        oDisp1.Add("FlowStepName", "關卡名稱");
        oDisp1.Add("FlowStepBatchEnabled", "批審@M");
        oDisp1.Add("FlowStepMailToList", "發信");
        oDisp1.Add("FlowStepAttachMaxQty", "附件@N0");
        oDisp1.Add("FlowStepCustVerifyURL", "自訂審核URL");
        oDisp1.Add("UpdUser", "更新人員");
        oDisp1.Add("UpdDateTime", "更新日期@T");

        gvFlowStep.ucDBName = FlowExpress._FlowSysDB;
        gvFlowStep.ucDataQrySQL = string.Format("Select * From FlowStep Where FlowID = '{0}'", _FlowID);
        gvFlowStep.ucDataKeyList = "FlowID,FlowStepID".Split(',');
        gvFlowStep.ucDataDisplayDefinition = oDisp1;
        gvFlowStep.ucSelectEnabled = true;
        gvFlowStep.ucAddEnabled = true;
        gvFlowStep.ucCopyEnabled = true;
        gvFlowStep.ucDeleteEnabled = true;
        gvFlowStep.ucDeleteConfirm = "將同時刪除此流程關卡所有相關資料，確定執行？";
        gvFlowStep.Refresh(IsInit);

        //FlowUpdCustDataExp
        oDisp2.Clear();
        oDisp2.Add("FlowUpdCustID", "回寫組別");
        oDisp2.Add("FlowUpdCustName", "名　　稱");
        oDisp2.Add("FlowUpdCustVarSW", "自訂變數@M");
        oDisp2.Add("FlowUpdCustTableSW", "自訂資料表@M");
        oDisp2.Add("UpdUser", "更新人員");
        oDisp2.Add("UpdDateTime", "更新日期@T");
        gvFlowUpdCustDataExp.ucDBName = FlowExpress._FlowSysDB;
        gvFlowUpdCustDataExp.ucDataQrySQL = string.Format("Select * From FlowUpdCustDataExp Where FlowID = '{0}'", _FlowID);
        gvFlowUpdCustDataExp.ucDataKeyList = "FlowID,FlowUpdCustID".Split(',');
        gvFlowUpdCustDataExp.ucDataDisplayDefinition = oDisp2;
        gvFlowUpdCustDataExp.ucAddEnabled = true;
        gvFlowUpdCustDataExp.ucEditEnabled = true;
        gvFlowUpdCustDataExp.ucDeleteEnabled = true;
        gvFlowUpdCustDataExp.Refresh(IsInit);

        //FlowCustGrp
        oDisp3.Clear();
        oDisp3.Add("FlowCustGrpID", "群組代號");
        oDisp3.Add("FlowCustGrpName", "群組名稱");
        oDisp3.Add("Remark", "備註");
        oDisp3.Add("UpdUser", "更新人員");
        oDisp3.Add("UpdDateTime", "更新日期@T");
        gvFlowCustGrp.ucDBName = FlowExpress._FlowSysDB;
        gvFlowCustGrp.ucDataQrySQL = string.Format("Select * From FlowCustGrp Where FlowID = '{0}'", _FlowID);
        gvFlowCustGrp.ucDataKeyList = "FlowID,FlowCustGrpID".Split(',');
        gvFlowCustGrp.ucDataDisplayDefinition = oDisp3;
        gvFlowCustGrp.ucAddEnabled = true;
        gvFlowCustGrp.ucEditEnabled = true;
        gvFlowCustGrp.ucDeleteEnabled = true;
        gvFlowCustGrp.ucSelectIcon = Util.Icon_Multilingual;
        gvFlowCustGrp.ucSelectToolTip = RS.Resources.MuiAdmin_btnLaunch;
        gvFlowCustGrp.ucSelectEnabled = true;
        gvFlowCustGrp.Refresh(IsInit);

        //FlowCustGrpDetail
        oDisp4.Clear();
        oDisp4.Add("FlowCustGrpID", "群組代號");
        oDisp4.Add("Value", "項目值");
        oDisp4.Add("Description", "項目內容");
        oDisp4.Add("Remark", "備註");
        oDisp4.Add("UpdUser", "更新人員");
        oDisp4.Add("UpdDateTime", "更新日期@T");
        gvFlowCustGrpDetail.ucDBName = FlowExpress._FlowSysDB;
        gvFlowCustGrpDetail.ucDataQrySQL = string.Format("Select * From FlowCustGrpDetail Where FlowID = '{0}'", _FlowID);
        gvFlowCustGrpDetail.ucDataKeyList = "FlowID,FlowCustGrpID,Value".Split(',');
        gvFlowCustGrpDetail.ucDataDisplayDefinition = oDisp4;
        gvFlowCustGrpDetail.ucAddEnabled = true;
        gvFlowCustGrpDetail.ucEditEnabled = true;
        gvFlowCustGrpDetail.ucDeleteEnabled = true;
        gvFlowCustGrpDetail.ucSelectIcon = Util.Icon_Multilingual;
        gvFlowCustGrpDetail.ucSelectToolTip = RS.Resources.MuiAdmin_btnLaunch;
        gvFlowCustGrpDetail.ucSelectEnabled = true;
        gvFlowCustGrpDetail.Refresh(IsInit);
    }

    #region Step 相關
    void gvFlowStep_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();

        switch (e.CommandName)
        {
            case "cmdSelect":
                Response.Redirect(string.Format("FlowStep.aspx?FlowID={0}&FlowStepID={1}", e.DataKeys[0], e.DataKeys[1]));
                break;
            case "cmdCopy":
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];

                DataRow dr = db.ExecuteDataSet(string.Format("Select * From viewFlowStep Where FlowID='{0}' and FlowStepID = '{1}' ", e.DataKeys[0], e.DataKeys[1])).Tables[0].Rows[0];
                txtNewFlowStepID.ucTextData = "Cpy-" + dr["FlowStepID"].ToString().Left(15);
                txtNewFlowStepName.ucTextData = "Cpy-" + dr["FlowStepName"].ToString().Left(25);

                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 450;
                ucModalPopup1.ucPopupHeight = 150;
                ucModalPopup1.ucPopupHeader = "複製流程關卡";
                ucModalPopup1.ucPanelID = pnlNewFlowStep.ID;
                ucModalPopup1.Show();
                break;
            case "cmdAdd":
                hidFlowID.Value = _FlowID;
                hidFlowStepID.Value = "";
                txtNewFlowStepID.ucTextData = "New-StepID";
                txtNewFlowStepName.ucTextData = "New-StepName";

                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 450;
                ucModalPopup1.ucPopupHeight = 150;
                ucModalPopup1.ucPopupHeader = "新增流程關卡";
                ucModalPopup1.ucPanelID = pnlNewFlowStep.ID;
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                sb.AppendStatement("Delete FlowStep      Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);

                sb.AppendStatement("Delete FlowStep_MUI  Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);

                sb.AppendStatement("Delete FlowStepBtn           Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);

                sb.AppendStatement("Delete FlowStepBtn_MUI       Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And FlowStepID =").AppendParameter("FlowStepID", e.DataKeys[1]);

                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStep", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
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

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        if (string.IsNullOrEmpty(hidFlowStepID.Value))
        {
            //新增關卡
            string strAddSQL = string.Format("Insert FlowStep (FlowID,FlowStepID,FlowStepName,FlowStepBatchEnabled,FlowStepMailToList,FlowStepAttachMaxQty) Values('{0}','{1}','{2}','N','','0');", hidFlowID.Value, txtNewFlowStepID.ucTextData.Trim(), txtNewFlowStepName.ucTextData.Trim());
            try
            {

                db.ExecuteNonQuery(strAddSQL);
                //[新增]資料異動Log
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStep", string.Format("{0},{1}", hidFlowID.Value, txtNewFlowStepID.ucTextData.Trim()), LogHelper.AppDataLogType.Create, Util.getControlEditResult(pnlNewFlowStep));
                Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
                (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
            }
            catch
            {
                Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
            }
        }
        else
        {
            //複製關卡
            ArrayList arTable = new ArrayList();
            arTable.Add("FlowStep");
            arTable.Add("FlowStepBtn");
            arTable.Add("FlowStepBtnHideExp");
            string strCopySQL = Util.getDataCopySQL(FlowExpress._FlowSysDB, "FlowID,FlowStepID".Split(','), (hidFlowID.Value + "," + hidFlowStepID.Value).Split(','), (hidFlowID.Value + "," + txtNewFlowStepID.ucTextData).Split(','), Util.getArray(arTable));
            strCopySQL += string.Format("Update FlowStep Set FlowStepName = N'{2}' Where FlowID = '{0}' and FlowStepID = '{1}' ;", hidFlowID.Value, txtNewFlowStepID.ucTextData, txtNewFlowStepName.ucTextData);
            try
            {
                db.ExecuteNonQuery(strCopySQL);
                //[複製]資料異動Log
                DataRow drOld = db.ExecuteDataSet(string.Format("Select * from FlowStep Where FlowID = '{0}' And FlowStepID = '{1}' ", hidFlowID.Value, hidFlowStepID.Value)).Tables[0].Rows[0];
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStep", hidFlowID.Value + "," + txtNewFlowStepID.ucTextData, LogHelper.AppDataLogType.Copy, Util.getDictionary(drOld));
                Util.NotifyMsg(RS.Resources.Msg_CopySucceed, Util.NotifyKind.Success);
                (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex);
                Util.NotifyMsg(RS.Resources.Msg_CopyFail, Util.NotifyKind.Error);
            }
        }
        RefreshGridView(true);
    }

    #endregion

    #region Grp 相關
    void gvFlowCustGrpMain_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 280;
        ucModalPopup1.ucPopupHeight = 220;
        ucModalPopup1.ucPanelID = pnlGrp.ID;
        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                fmGrp.ChangeMode(FormViewMode.Insert);
                fmGrp.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                fmGrp.ChangeMode(FormViewMode.Edit);
                fmGrp.DataSource = db.ExecuteDataSet(string.Format("Select * From FlowCustGrp Where FlowID = '{0}' and FlowCustGrpID = '{1}';", e.DataKeys[0], e.DataKeys[1])).Tables[0];
                fmGrp.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                sb.AppendStatement("Delete FlowCustGrp ");
                sb.Append(" Where FlowID = ").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And  FlowCustGrpID = ").AppendParameter("FlowCustGrpID", e.DataKeys[1]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrp", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdSelect":
                string strURL = string.Format("{0}", Util.getFixURL("~/Util/MuiAdmin.aspx"));
                strURL += string.Format("?DBName={0}&TableName={1}&PKFieldList={2}&PKValueList={3}", FlowExpress._FlowSysDB, "FlowCustGrp", "FlowID,FlowCustGrpID", Util.getStringJoin(e.DataKeys));
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 650;
                ucModalPopup1.ucPopupHeight = 350;
                ucModalPopup1.ucFrameURL = strURL;
                ucModalPopup1.ucBtnCloselEnabled = true;
                ucModalPopup1.ucBtnCancelEnabled = false;
                ucModalPopup1.ucBtnCompleteEnabled = false;
                ucModalPopup1.Show();
                break;
            default:
                Util.MsgBox(string.Format("cmd=[{0}],key=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
                break;
        }
    }
    protected void fmGrp_DataBound(object sender, EventArgs e)
    {
        Util_ucTextBox oTxt;
        if (fmGrp.CurrentMode == FormViewMode.Insert)
        {
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrp, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = _FlowID;
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }
        }


        if (fmGrp.CurrentMode == FormViewMode.Edit)
        {
            DataRow dr = ((DataTable)fmGrp.DataSource).Rows[0];
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrp, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrp, "txtFlowCustGrpID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowCustGrpID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrp, "txtFlowCustGrpName", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowCustGrpName"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrp, "txtRemark", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
            }
        }
    }
    protected void btnGrpAdd_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmGrp);

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Insert FlowCustGrp ");
        sb.Append("(FlowID,FlowCustGrpID,FlowCustGrpName,Remark,UpdUser,UpdDateTime)");
        sb.Append(" Values (").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append("        ,").AppendParameter("txtFlowCustGrpID", oDic["txtFlowCustGrpID"]);
        sb.Append("        ,").AppendParameter("txtFlowCustGrpName", oDic["txtFlowCustGrpName"]);
        sb.Append("        ,").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append("        ,").Append(UserInfo.getUserInfo().UserID);
        sb.Append("        ,").AppendDbDateTime();
        sb.Append("        )");
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[新增]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrp", string.Format("{0},{1}", oDic["txtFlowID"], oDic["txtFlowCustGrpID"]), LogHelper.AppDataLogType.Create, oDic);
            Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
        }
        catch
        {
            Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
        }
        RefreshGridView();
    }
    protected void btnGrpUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmGrp);

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Update FlowCustGrp Set ");
        sb.Append(" FlowCustGrpName = ").AppendParameter("txtFlowCustGrpName", oDic["txtFlowCustGrpName"]);
        sb.Append(",Remark = ").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append(",UpdUser = ").Append(UserInfo.getUserInfo().UserID);
        sb.Append(",UpdDateTime = ").AppendDbDateTime();
        sb.Append(" Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append(" And  FlowCustGrpID = ").AppendParameter("txtFlowCustGrpID", oDic["txtFlowCustGrpID"]);
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[更新]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrp", string.Format("{0},{1}", oDic["txtFlowID"], oDic["txtFlowCustGrpID"]), LogHelper.AppDataLogType.Update, oDic);
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
        }
        catch
        {
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
        }
        RefreshGridView();
    }
    #endregion

    #region GrpDetail 相關
    void gvFlowCustGrpDetail_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 280;
        ucModalPopup1.ucPopupHeight = 250;
        ucModalPopup1.ucPanelID = pnlGrpDetail.ID;
        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                fmGrpDetail.ChangeMode(FormViewMode.Insert);
                fmGrpDetail.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                fmGrpDetail.ChangeMode(FormViewMode.Edit);
                fmGrpDetail.DataSource = db.ExecuteDataSet(string.Format("Select * From FlowCustGrpDetail Where FlowID = '{0}' and FlowCustGrpID = '{1}' and Value = '{2}';", e.DataKeys[0], e.DataKeys[1], e.DataKeys[2])).Tables[0];
                fmGrpDetail.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                sb.AppendStatement("Delete FlowCustGrpDetail ");
                sb.Append(" Where FlowID = ").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And  FlowCustGrpID = ").AppendParameter("FlowCustGrpID", e.DataKeys[1]);
                sb.Append(" And  Value = ").AppendParameter("Value", e.DataKeys[2]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrpDetail", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdSelect":
                string strURL = string.Format("{0}", Util.getFixURL("~/Util/MuiAdmin.aspx"));
                strURL += string.Format("?DBName={0}&TableName={1}&PKFieldList={2}&PKValueList={3}", FlowExpress._FlowSysDB, "FlowCustGrpDetail", "FlowID,FlowCustGrpID,Value", Util.getStringJoin(e.DataKeys));
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 650;
                ucModalPopup1.ucPopupHeight = 350;
                ucModalPopup1.ucFrameURL = strURL;
                ucModalPopup1.ucBtnCloselEnabled = true;
                ucModalPopup1.ucBtnCancelEnabled = false;
                ucModalPopup1.ucBtnCompleteEnabled = false;
                ucModalPopup1.Show();
                break;
            default:
                Util.MsgBox(string.Format("cmd=[{0}],key=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
                break;
        }
    }
    protected void fmGrpDetail_DataBound(object sender, EventArgs e)
    {
        Util_ucTextBox oTxt;
        DropDownList oDdl;
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dt = new DataTable();
        CommandHelper sb = db.CreateCommandHelper();
        if (fmGrpDetail.CurrentMode == FormViewMode.Insert)
        {
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrpDetail, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = _FlowID;
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oDdl = (DropDownList)Util.FindControlEx(fmGrpDetail, "ddlFlowCustGrpID", true);
            if (oDdl != null)
            {
                sb.Reset();
                sb.AppendStatement("Select FlowCustGrpID,FlowCustGrpName from FlowCustGrp Where FlowID = ").AppendParameter("FlowID", _FlowID);
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                oDdl.DataSource = Util.getDictionary(dt, 0, 1, true);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                oDdl.DataBind();
            }
        }

        if (fmGrpDetail.CurrentMode == FormViewMode.Edit)
        {
            DataRow dr = ((DataTable)fmGrpDetail.DataSource).Rows[0];
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrpDetail, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oDdl = (DropDownList)Util.FindControlEx(fmGrpDetail, "ddlFlowCustGrpID", true);
            if (oDdl != null)
            {
                sb.Reset();
                sb.AppendStatement("Select FlowCustGrpID,FlowCustGrpName from FlowCustGrp Where FlowID = ").AppendParameter("FlowID", _FlowID);
                dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                oDdl.DataSource = Util.getDictionary(dt, 0, 1, true);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                oDdl.SelectedValue = dr["FlowCustGrpID"].ToString();
                oDdl.DataBind();
                oDdl.Enabled = false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrpDetail, "txtValue", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Value"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrpDetail, "txtDescription", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Description"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmGrpDetail, "txtRemark", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
            }
        }
    }
    protected void btnGrpDetailAdd_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmGrpDetail);

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Insert FlowCustGrpDetail ");
        sb.Append("(FlowID,FlowCustGrpID,Value,Description,Remark,UpdUser,UpdDateTime)");
        sb.Append(" Values (").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append("        ,").AppendParameter("ddlFlowCustGrpID", oDic["ddlFlowCustGrpID"]);
        sb.Append("        ,").AppendParameter("txtValue", oDic["txtValue"]);
        sb.Append("        ,").AppendParameter("txtDescription", oDic["txtDescription"]);
        sb.Append("        ,").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append("        ,").Append(UserInfo.getUserInfo().UserID);
        sb.Append("        ,").AppendDbDateTime();
        sb.Append("        )");
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[新增]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrpDetail", string.Format("{0},{1},{2}", oDic["txtFlowID"], oDic["ddlFlowCustGrpID"], oDic["txtValue"]), LogHelper.AppDataLogType.Create, oDic);
            Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
        }
        catch
        {
            Util.NotifyMsg(RS.Resources.Msg_AddFail, Util.NotifyKind.Error);
        }
        RefreshGridView();
    }
    protected void btnGrpDetailUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmGrpDetail);

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Update FlowCustGrpDetail Set ");
        sb.Append(" Description = ").AppendParameter("txtDescription", oDic["txtDescription"]);
        sb.Append(",Remark = ").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append(",UpdUser = ").Append(UserInfo.getUserInfo().UserID);
        sb.Append(",UpdDateTime = ").AppendDbDateTime();
        sb.Append(" Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append(" And  FlowCustGrpID = ").AppendParameter("ddlFlowCustGrpID", oDic["ddlFlowCustGrpID"]);
        sb.Append(" And  Value = ").AppendParameter("txtValue", oDic["txtValue"]);
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[更新]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowCustGrpDetail", string.Format("{0},{1},{2}", oDic["txtFlowID"], oDic["ddlFlowCustGrpID"], oDic["txtValue"]), LogHelper.AppDataLogType.Update, oDic);
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
        }
        catch
        {
            Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
        }
        RefreshGridView();
    }
    #endregion

    #region CustDataExp 相關
    void gvFlowUpdCustDataExp_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        UserInfo oUser = UserInfo.getUserInfo();
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 880;
        ucModalPopup1.ucPopupHeight = 680;
        ucModalPopup1.ucPanelID = pnlCustDataExp.ID;
        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                fmCustDataExp.ChangeMode(FormViewMode.Insert);
                fmCustDataExp.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                fmCustDataExp.ChangeMode(FormViewMode.Edit);
                fmCustDataExp.DataSource = db.ExecuteDataSet(string.Format("Select * From FlowUpdCustDataExp Where FlowID = '{0}' and FlowUpdCustID = '{1}';", e.DataKeys[0], e.DataKeys[1])).Tables[0];
                fmCustDataExp.DataBind();
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                sb.AppendStatement("Delete FlowUpdCustDataExp ");
                sb.Append(" Where FlowID = ").AppendParameter("FlowID", e.DataKeys[0]);
                sb.Append(" And  FlowUpdCustID = ").AppendParameter("FlowUpdCustID", e.DataKeys[1]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowUpdCustDataExp", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_DeleteFail, Util.NotifyKind.Error);
                }
                break;
            default:
                Util.MsgBox(string.Format("cmd=[{0}],key=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
                break;
        }

    }
    protected void fmCustDataExp_DataBound(object sender, EventArgs e)
    {
        Label oLab;
        Util_ucTextBox oTxt;
        CheckBox oChk;

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dt = new DataTable();
        CommandHelper sb = db.CreateCommandHelper();

        DataRow drFlow = db.ExecuteDataSet(string.Format("Select * From FlowSpec Where FlowID='{0}' ", _FlowID)).Tables[0].Rows[0];

        //顯示標籤
        oLab = (Label)Util.FindControlEx(fmCustDataExp, "labFlowKeyFieldList1", true);
        if (oLab != null)
        {
            oLab.Text = drFlow["FlowKeyFieldList"].ToString();
        }

        oLab = (Label)Util.FindControlEx(fmCustDataExp, "labFlowKeyFieldList2", true);
        if (oLab != null)
        {
            oLab.Text = drFlow["FlowKeyFieldList"].ToString();
        }

        oLab = (Label)Util.FindControlEx(fmCustDataExp, "labFlowCustVarNameList", true);
        if (oLab != null)
        {
            if (!string.IsNullOrEmpty(drFlow["FlowCustVarNameList"].ToString()))
                oLab.Text = drFlow["FlowCustVarNameList"].ToString();
            else
                oLab.Text = "N/A";
        }

        oLab = (Label)Util.FindControlEx(fmCustDataExp, "labFlowCustDB", true);
        if (oLab != null)
        {
            oLab.Text = drFlow["FlowCustDB"].ToString();
        }

        if (fmCustDataExp.CurrentMode == FormViewMode.Insert)
        {
            //新增模式
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = _FlowID;
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }
        }

        if (fmCustDataExp.CurrentMode == FormViewMode.Edit)
        {
            //編輯模式
            DataRow dr = ((DataTable)fmCustDataExp.DataSource).Rows[0];
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustName", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustName"].ToString();
            }

            oChk = (CheckBox)Util.FindControlEx(fmCustDataExp, "chkFlowUpdCustVarSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowUpdCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustVarNameList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustVarNameList"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustVarNewValueList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustVarNewValueList"].ToString();
            }

            oChk = (CheckBox)Util.FindControlEx(fmCustDataExp, "chkFlowUpdCustTableSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowUpdCustTableSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustTableName", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustTableName"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustTableKeyList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustTableKeyList"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustTableFieldList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustTableFieldList"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtFlowUpdCustTableNewValueList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowUpdCustTableNewValueList"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmCustDataExp, "txtRemark", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
            }
        }
    }

    protected void btnCustDataExpAdd_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmCustDataExp);

        if (!string.IsNullOrEmpty(oDic["txtFlowUpdCustVarNameList"]))
        {
            if (string.IsNullOrEmpty(oDic["txtFlowUpdCustVarNewValueList"]) || oDic["txtFlowUpdCustVarNameList"].Split(',').Count() != oDic["txtFlowUpdCustVarNewValueList"].Split(',').Count())
            {
                Util.MsgBox("CustVar 參數設定有誤");
                RefreshGridView();
                return;
            }
        }

        if (!string.IsNullOrEmpty(oDic["txtFlowUpdCustTableFieldList"]))
        {
            if (string.IsNullOrEmpty(oDic["txtFlowUpdCustTableNewValueList"]) || oDic["txtFlowUpdCustTableFieldList"].Split(',').Count() != oDic["txtFlowUpdCustTableNewValueList"].Split(',').Count())
            {
                Util.MsgBox("CustTable 參數設定有誤");
                RefreshGridView();
                return;
            }
        }

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Insert FlowUpdCustDataExp (");
        sb.Append("FlowID,FlowUpdCustID,FlowUpdCustName");
        sb.Append(",FlowUpdCustVarSW,FlowUpdCustVarNameList,FlowUpdCustVarNewValueList");
        sb.Append(",FlowUpdCustTableSW,FlowUpdCustTableName,FlowUpdCustTableKeyList");
        sb.Append(",FlowUpdCustTableFieldList,FlowUpdCustTableNewValueList");
        sb.Append(",Remark,UpdUser,UpdDateTime");
        sb.Append(") Values (").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustID", oDic["txtFlowUpdCustID"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustName", oDic["txtFlowUpdCustName"]);
        sb.Append("         ,").AppendParameter("chkFlowUpdCustVarSW", oDic["chkFlowUpdCustVarSW"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustVarNameList", oDic["txtFlowUpdCustVarNameList"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustVarNewValueList", oDic["txtFlowUpdCustVarNewValueList"]);
        sb.Append("         ,").AppendParameter("chkFlowUpdCustTableSW", oDic["chkFlowUpdCustTableSW"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustTableName", oDic["txtFlowUpdCustTableName"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustTableKeyList", oDic["txtFlowUpdCustTableKeyList"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustTableFieldList", oDic["txtFlowUpdCustTableFieldList"]);
        sb.Append("         ,").AppendParameter("txtFlowUpdCustTableNewValueList", oDic["txtFlowUpdCustTableNewValueList"]);
        sb.Append("         ,").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append("         ,").Append(UserInfo.getUserInfo().UserID);
        sb.Append("         ,").AppendDbDateTime();
        sb.Append(")");

        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[新增]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowUpdCustDataExp", string.Format("{0},{1}", oDic["txtFlowID"], oDic["txtFlowUpdCustID"]), LogHelper.AppDataLogType.Create, oDic);
            Util.NotifyMsg(RS.Resources.Msg_AddSucceed, Util.NotifyKind.Success);
        }
        catch (Exception ex)
        {
            Util.NotifyMsg(ex.Message, Util.NotifyKind.Error);
        }

        RefreshGridView();
    }


    protected void btnCustDataExpUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmCustDataExp);

        if (!string.IsNullOrEmpty(oDic["txtFlowUpdCustVarNameList"]))
        {
            if (string.IsNullOrEmpty(oDic["txtFlowUpdCustVarNewValueList"]) || oDic["txtFlowUpdCustVarNameList"].Split(',').Count() != oDic["txtFlowUpdCustVarNewValueList"].Split(',').Count())
            {
                Util.MsgBox("CustVar 參數設定有誤");
                RefreshGridView();
                return;
            }
        }

        if (!string.IsNullOrEmpty(oDic["txtFlowUpdCustTableFieldList"]))
        {
            if (string.IsNullOrEmpty(oDic["txtFlowUpdCustTableNewValueList"]) || oDic["txtFlowUpdCustTableFieldList"].Split(',').Count() != oDic["txtFlowUpdCustTableNewValueList"].Split(',').Count())
            {
                Util.MsgBox("CustTable 參數設定有誤");
                RefreshGridView();
                return;
            }
        }

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Update FlowUpdCustDataExp Set ");
        sb.Append(" FlowUpdCustName = ").AppendParameter("txtFlowUpdCustName", oDic["txtFlowUpdCustName"]);
        sb.Append(",FlowUpdCustVarSW = ").AppendParameter("chkFlowUpdCustVarSW", oDic["chkFlowUpdCustVarSW"]);
        sb.Append(",FlowUpdCustVarNameList = ").AppendParameter("txtFlowUpdCustVarNameList", oDic["txtFlowUpdCustVarNameList"]);
        sb.Append(",FlowUpdCustVarNewValueList = ").AppendParameter("txtFlowUpdCustVarNewValueList", oDic["txtFlowUpdCustVarNewValueList"]);
        sb.Append(",FlowUpdCustTableSW = ").AppendParameter("chkFlowUpdCustTableSW", oDic["chkFlowUpdCustTableSW"]);
        sb.Append(",FlowUpdCustTableName = ").AppendParameter("txtFlowUpdCustTableName", oDic["txtFlowUpdCustTableName"]);
        sb.Append(",FlowUpdCustTableKeyList = ").AppendParameter("txtFlowUpdCustTableKeyList", oDic["txtFlowUpdCustTableKeyList"]);
        sb.Append(",FlowUpdCustTableFieldList = ").AppendParameter("txtFlowUpdCustTableFieldList", oDic["txtFlowUpdCustTableFieldList"]);
        sb.Append(",FlowUpdCustTableNewValueList = ").AppendParameter("txtFlowUpdCustTableNewValueList", oDic["txtFlowUpdCustTableNewValueList"]);
        sb.Append(",Remark = ").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append(",UpdUser = ").Append(UserInfo.getUserInfo().UserID);
        sb.Append(",UpdDateTime = ").AppendDbDateTime();
        sb.Append(" Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append(" And  FlowUpdCustID = ").AppendParameter("txtFlowUpdCustID", oDic["txtFlowUpdCustID"]);
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[更新]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowUpdCustDataExp", string.Format("{0},{1}", oDic["txtFlowID"], oDic["txtFlowUpdCustID"]), LogHelper.AppDataLogType.Update, oDic);
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
        }
        catch (Exception ex)
        {
            //Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
            Util.NotifyMsg(ex.Message, Util.NotifyKind.Error);
        }

        RefreshGridView();
    }
    #endregion

}