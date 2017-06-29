using System;
using System.Collections;
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

/// <summary>
/// 「流程關卡按鈕」維護
/// </summary>
public partial class FlowExpress_Admin_FlowStepBtn : SecurePage
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

    private string _FlowStepBtnID
    {
        get
        {
            return Util.getRequestQueryStringKey("FlowStepBtnID");
        }
    }
    #endregion

    private Dictionary<string, string> _Dic_RefStepID
    {
        get
        {
            if (ViewState["_Dic_RefStepID"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_RefStepID"];
            }
            else
            {
                Dictionary<string, string> oRefStepID = new Dictionary<string, string>();
                DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
                oRefStepID.Clear();
                oRefStepID.Add("", "-- 請選擇 --");
                oRefStepID.Add("ANY", "任意指派對象");
                oRefStepID.Add("COMP", "同公司任意指派對象"); //2016.08.16 新增
                oRefStepID.Add("PART", "同公司(含兼職)任意指派對象"); //2016.08.16 新增

                oRefStepID.AddRange(Util.getDictionary(db.ExecuteDataSet(string.Format("Select FlowStepID,FlowStepName From FlowStep Where FlowID = '{0}'", _FlowID)).Tables[0]));
                ViewState["_Dic_RefStepID"] = oRefStepID;
                return (Dictionary<string, string>)ViewState["_Dic_RefStepID"];
            }
        }
    }

    private Dictionary<string, string> _Dic_Exp
    {
        get
        {
            if (ViewState["_Dic_Exp"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_Exp"];
            }
            else
            {
                Dictionary<string, string> oExp = new Dictionary<string, string>();
                oExp.Add("", "無");
                oExp.AddRange(Util.getCodeMap(FlowExpress._FlowSysDB, "FlowStepBtnHideExp", "Exp"));
                ViewState["_Dic_Exp"] = oExp;
                return (Dictionary<string, string>)ViewState["_Dic_Exp"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlFlowStepBtnExp.Visible = false;

        if (string.IsNullOrEmpty(_FlowID) || string.IsNullOrEmpty(_FlowStepID) || string.IsNullOrEmpty(_FlowStepBtnID))
        {
            labMsg.Visible = true;
            pnlFlowStep.Visible = false;
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_ParaNotFound1 + "<br><br>", "FlowID/FlowStepID/FlowStepBtnID"));
            return;
        }

        if (!IsPostBack)
        {
            DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
            DataTable dt = db.ExecuteDataSet(string.Format("Select * From FlowStepBtn Where FlowID = '{0}' and FlowStepID = '{1}' and FlowStepBtnID = '{2}';", _FlowID, _FlowStepID, _FlowStepBtnID)).Tables[0];
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
        gvFlowStepBtnHideExp.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowStepBtnHideExp_RowCommand);
        gvFlowStepBtnStopExp.RowCommand += new Util_ucGridView.GridViewRowClick(gvFlowStepBtnStopExp_RowCommand);
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        RefreshGridView();
    }

    protected void btnMainUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);

        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Reset();
        sb.AppendStatement("Update FlowStepBtn Set ");
        sb.Append("  FlowStepBtnSeqNo        = ").AppendParameter("txtFlowStepBtnSeqNo", oDic["txtFlowStepBtnSeqNo"]);
        sb.Append(", FlowStepBtnCaption    = ").AppendParameter("txtFlowStepBtnCaption", oDic["txtFlowStepBtnCaption"]);
        sb.Append(", FlowStepBtnDesc    = ").AppendParameter("txtFlowStepBtnDesc", oDic["txtFlowStepBtnDesc"]);
        sb.Append(", FlowStepBtnConfirmMsg    = ").AppendParameter("txtFlowStepBtnConfirmMsg", oDic["txtFlowStepBtnConfirmMsg"]);
        sb.Append(", FlowStepBtnIsMultiSelect    = ").AppendParameter("ddlFlowStepBtnIsMultiSelect", oDic["ddlFlowStepBtnIsMultiSelect"]);
        sb.Append(", FlowStepBtnIsNeedOpinion    = ").AppendParameter("chkFlowStepBtnIsNeedOpinion", oDic["chkFlowStepBtnIsNeedOpinion"]);
        sb.Append(", FlowStepBtnIsSendMail    = ").AppendParameter("chkFlowStepBtnIsSendMail", oDic["chkFlowStepBtnIsSendMail"]);
        sb.Append(", FlowStepBtnNextStepID    = ").AppendParameter("ddlFlowStepBtnNextStepID", oDic["ddlFlowStepBtnNextStepID"]);
        sb.Append(", FlowStepBtnNextStepDealCondition    = ").AppendParameter("ddlFlowStepBtnNextStepDealCondition", oDic["ddlFlowStepBtnNextStepDealCondition"]);
        sb.Append(", FlowStepBtnIsAddMultiSubFlow    = ").AppendParameter("chkFlowStepBtnIsAddMultiSubFlow", oDic["chkFlowStepBtnIsAddMultiSubFlow"]);
        sb.Append(", FlowStepBtnAddSubFlowID    = ").AppendParameter("FlowStepBtnAddSubFlowID", oDic["ddlAddSubFlowInfo"].Split(',')[0]);
        sb.Append(", FlowStepBtnAddSubFlowStepBtnID  = ").AppendParameter("FlowStepBtnAddSubFlowStepBtnID", oDic["ddlAddSubFlowInfo"].Split(',')[1]);
        sb.Append(", FlowStepBtnChkSubFlowList  = ").AppendParameter("chkFlowStepBtnChkSubFlowList", oDic["chkFlowStepBtnChkSubFlowList"]);
        sb.Append(", FlowStepBtnUpdCustIDList  = ").AppendParameter("chkFlowStepBtnUpdCustIDList", oDic["chkFlowStepBtnUpdCustIDList"]);

        sb.Append(", FlowStepBtnRefStepSW  = ").AppendParameter("chkFlowStepBtnRefStepSW", oDic["chkFlowStepBtnRefStepSW"]);
        sb.Append(", FlowStepBtnRefStepID  = ").AppendParameter("ddlFlowStepBtnRefStepID", oDic["ddlFlowStepBtnRefStepID"]);

        sb.Append(", FlowStepBtnCustClassSW  = ").AppendParameter("chkFlowStepBtnCustClassSW", oDic["chkFlowStepBtnCustClassSW"]);
        sb.Append(", FlowStepBtnCustClassMethod  = ").AppendParameter("txtFlowStepBtnCustClassMethod", oDic["txtFlowStepBtnCustClassMethod"]);
        sb.Append(", FlowStepBtnCustParaList  = ").AppendParameter("txtFlowStepBtnCustParaList", oDic["txtFlowStepBtnCustParaList"]);

        sb.Append(", FlowStepBtnCustGrpSW  = ").AppendParameter("chkFlowStepBtnCustGrpSW", oDic["chkFlowStepBtnCustGrpSW"]);
        sb.Append(", FlowStepBtnCustGrpIDList  = ").AppendParameter("chkFlowStepBtnCustGrpIDList", oDic["chkFlowStepBtnCustGrpIDList"]);
        sb.Append(", FlowStepBtnCustGrpIsDetail  = ").AppendParameter("chkFlowStepBtnCustGrpIsDetail", oDic["chkFlowStepBtnCustGrpIsDetail"]);

        sb.Append(", FlowStepBtnCustVarSW  = ").AppendParameter("chkFlowStepBtnCustVarSW", oDic["chkFlowStepBtnCustVarSW"]);
        sb.Append(", FlowStepBtnCustVarClassMethod  = ").AppendParameter("txtFlowStepBtnCustVarClassMethod", oDic["txtFlowStepBtnCustVarClassMethod"]);
        sb.Append(", FlowStepBtnCustVarNameList  = ").AppendParameter("txtFlowStepBtnCustVarNameList", oDic["txtFlowStepBtnCustVarNameList"]);

        sb.Append(", FlowStepBtnCustQrySW  = ").AppendParameter("chkFlowStepBtnCustQrySW", oDic["chkFlowStepBtnCustQrySW"]);
        sb.Append(", FlowStepBtnCustQrySQL  = ").AppendParameter("txtFlowStepBtnCustQrySQL", oDic["txtFlowStepBtnCustQrySQL"]);

        sb.Append(", Remark    = ").AppendParameter("txtRemark", oDic["txtRemark"]);
        sb.Append(", UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
        sb.Append(", UpdDateTime = ").AppendDbDateTime();
        sb.Append("  Where FlowID = ").AppendParameter("txtFlowID", oDic["txtFlowID"]);
        sb.Append("    And FlowStepID = ").AppendParameter("txtFlowStepID", oDic["txtFlowStepID"]);
        sb.Append("    And FlowStepBtnID = ").AppendParameter("txtFlowStepBtnID", oDic["txtFlowStepBtnID"]);

        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
            //[更新]資料異動Log
            LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowStepBtn", string.Format("{0},{1},{2}", oDic["txtFlowID"], oDic["txtFlowStepID"], oDic["txtFlowStepBtnID"]), LogHelper.AppDataLogType.Update, oDic);
            Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }

        DataTable dt = db.ExecuteDataSet(string.Format("Select * From FlowStepBtn Where FlowID = '{0}' and FlowStepID = '{1}' and FlowStepBtnID = '{2}';", _FlowID, _FlowStepID, _FlowStepBtnID)).Tables[0];
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
            FlowExpress oFlow = new FlowExpress(_FlowID);

            Util_ucTextBox oTxt;
            DropDownList oDdl;
            CheckBox oChk;
            Label oLab;
            Util_ucCheckBoxList oChkList;
            Dictionary<string, string> oTmpDic = new Dictionary<string, string>();

            DbHelper dbChk = new DbHelper(FlowExpress._FlowSysDB);
            string strChkSQL = "";

            DataRow dr = ((DataTable)fmMain.DataSource).Rows[0];
            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnID", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnSeqNo", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnSeqNo"].ToString();
                oTxt.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
                oTxt.ucIsRequire = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCaption", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCaption"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnDesc", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnDesc"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnConfirmMsg", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnConfirmMsg"].ToString();
            }

            oDdl = (DropDownList)Util.FindControlEx(fmMain, "ddlFlowStepBtnIsMultiSelect", true);
            if (oDdl != null)
            {
                oDdl.DataSource = Util.getDictionary(Util.getCodeMap(FlowExpress._FlowSysDB, "FlowStepBtn", "IsMultiSelect"), true);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                oDdl.SelectedValue = dr["FlowStepBtnIsMultiSelect"].ToString();
                oDdl.DataBind();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnIsNeedOpinion", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnIsNeedOpinion"].ToString().ToUpper() == "Y") ? true : false;
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnIsSendMail", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnIsSendMail"].ToString().ToUpper() == "Y") ? true : false;
            }

            oDdl = (DropDownList)Util.FindControlEx(fmMain, "ddlFlowStepBtnNextStepID", true);
            if (oDdl != null)
            {
                oTmpDic.Clear();
                oTmpDic.AddRange(Util.getDictionary(dbChk.ExecuteDataSet(string.Format("Select FlowStepID,FlowStepName From FlowStep Where FlowID = '{0}'", _FlowID)).Tables[0]));
                oTmpDic.Add("FromStepID", "來源關卡");
                for (int i = 0; i < FlowExpress._FlowNativeCloseStepIDList.Count(); i++)
                {
                    oTmpDic.Add(FlowExpress._FlowNativeCloseStepIDList[i], FlowExpress._FlowNativeCloseStepNameList[i]);
                }
                oDdl.DataSource = Util.getDictionary(oTmpDic);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                if (!string.IsNullOrEmpty(dr["FlowStepBtnNextStepID"].ToString()))
                {
                    oDdl.SelectedValue = dr["FlowStepBtnNextStepID"].ToString();
                }
                oDdl.DataBind();
            }


            oDdl = (DropDownList)Util.FindControlEx(fmMain, "ddlFlowStepBtnNextStepDealCondition", true);
            if (oDdl != null)
            {
                oDdl.DataSource = Util.getDictionary(Util.getCodeMap(FlowExpress._FlowSysDB, "FlowStepBtn", "NextStepDealCondition"), true);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                if (!string.IsNullOrEmpty(dr["FlowStepBtnNextStepDealCondition"].ToString()))
                {
                    oDdl.SelectedValue = dr["FlowStepBtnNextStepDealCondition"].ToString();
                }
                oDdl.DataBind();
            }

            oDdl = (DropDownList)Util.FindControlEx(fmMain, "ddlAddSubFlowInfo", true);
            if (oDdl != null)
            {
                strChkSQL = "Select FlowID + ',' + FlowStepBtnID as 'key' ,FlowID + ',' + FlowStepBtnID + ' - ' + FlowName + '：' + FlowStepBtnCaption as 'value' ";
                strChkSQL += " From  viewFlowSpecStepBtn Where FlowStepID = '{0}' and CultureCode = '{1}' ";
                strChkSQL = string.Format(strChkSQL, FlowExpress._FlowNativeBeginStepID, FlowExpress._FlowNativeCultureCode);

                oTmpDic.Clear();
                oTmpDic.Add(",", "-- 請選擇「子流程：按鈕」--");
                oTmpDic.AddRange(Util.getDictionary(dbChk.ExecuteDataSet(strChkSQL).Tables[0]));
                oDdl.DataSource = oTmpDic;
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                oDdl.SelectedValue = dr["FlowStepBtnAddSubFlowID"].ToString().Trim() + "," + dr["FlowStepBtnAddSubFlowStepBtnID"].ToString().Trim();
                oDdl.DataBind();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnIsAddMultiSubFlow", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnIsAddMultiSubFlow"].ToString().ToUpper() == "Y") ? true : false;
            }

            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowStepBtnChkSubFlowList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                oTmpDic.Clear();
                oTmpDic.Add("ALL", "所有子流程");
                oTmpDic.AddRange(Util.getDictionary(dbChk.ExecuteDataSet("Select FlowID, FlowName From FlowSpec").Tables[0]));

                oChkList.ucSourceDictionary = Util.getDictionary(oTmpDic);
                oChkList.ucSelectedIDList = dr["FlowStepBtnChkSubFlowList"].ToString();
                oChkList.Refresh();
            }

            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowStepBtnUpdCustIDList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                oTmpDic.Clear();
                oTmpDic.AddRange(Util.getDictionary(dbChk.ExecuteDataSet(string.Format("Select FlowUpdCustID, FlowUpdCustName From FlowUpdCustDataExp Where FlowID = '{0}'", _FlowID)).Tables[0]));

                oChkList.ucSourceDictionary = Util.getDictionary(oTmpDic);
                oChkList.ucSelectedIDList = dr["FlowStepBtnUpdCustIDList"].ToString();
                oChkList.Refresh();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnRefStepSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnRefStepSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oDdl = (DropDownList)Util.FindControlEx(fmMain, "ddlFlowStepBtnRefStepID", true);
            if (oDdl != null)
            {
                oDdl.DataSource = Util.getDictionary(_Dic_RefStepID);
                oDdl.DataValueField = "key";
                oDdl.DataTextField = "value";
                oDdl.SelectedValue = dr["FlowStepBtnRefStepID"].ToString().Trim();
                oDdl.DataBind();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnCustClassSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnCustClassSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCustClassMethod", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCustClassMethod"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCustParaList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCustParaList"].ToString();
            }

            oLab = (Label)Util.FindControlEx(fmMain, "labCustClassFlowPara", true);
            if (oLab != null)
            {
                oLab.Text = "_FlowKeyValue";
                for (int k = 0; k < oFlow.FlowKeyFieldList.Count(); k++)
                {
                    oLab.Text += string.Format(",_{0}", oFlow.FlowKeyFieldList[k]);
                }
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnCustGrpSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnCustGrpSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oChkList = (Util_ucCheckBoxList)Util.FindControlEx(fmMain, "chkFlowStepBtnCustGrpIDList", false, typeof(Util_ucCheckBoxList));
            if (oChkList != null)
            {
                oTmpDic.Clear();
                oTmpDic.AddRange(Util.getDictionary(dbChk.ExecuteDataSet(string.Format("Select FlowCustGrpID, FlowCustGrpName From FlowCustGrp Where FlowID = '{0}'", _FlowID)).Tables[0]));

                oChkList.ucSourceDictionary = Util.getDictionary(oTmpDic);
                oChkList.ucSelectedIDList = dr["FlowStepBtnCustGrpIDList"].ToString();
                oChkList.Refresh();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnCustGrpIsDetail", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnCustGrpIsDetail"].ToString().ToUpper() == "Y") ? true : false;
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnCustVarSW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCustVarClassMethod", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCustVarClassMethod"].ToString();
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCustVarNameList", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCustVarNameList"].ToString();
            }

            oChk = (CheckBox)Util.FindControlEx(fmMain, "chkFlowStepBtnCustQrySW", true);
            if (oChk != null)
            {
                oChk.Checked = (dr["FlowStepBtnCustQrySW"].ToString().ToUpper() == "Y") ? true : false;
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtFlowStepBtnCustQrySQL", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["FlowStepBtnCustQrySQL"].ToString();
            }

            oLab = (Label)Util.FindControlEx(fmMain, "labFlowCustDB", true);
            if (oLab != null)
            {
                oLab.Text = string.Format("[{0}]", !string.IsNullOrEmpty(oFlow.FlowCustDB) ? oFlow.FlowCustDB : "[表單來源DB]");
            }

            oTxt = (Util_ucTextBox)Util.FindControlEx(fmMain, "txtRemark", false, typeof(Util_ucTextBox));
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
            }

            oLab = (Label)Util.FindControlEx(fmMain, "labUpdInfo", true);
            if (oLab != null)
            {
                oLab.Text = string.Format("{0} - {1}  on {2:yyyy\\/MM\\/dd HH:mm}", dr["UpdUser"].ToString(), UserInfo.findUserName(dr["UpdUser"].ToString()), dr["UpdDateTime"]);
            }

            Util_ucMuiAdminButton oMUI = (Util_ucMuiAdminButton)Util.FindControlEx(fmMain, "ucMuiAdminButton1", false, typeof(Util_ucMuiAdminButton));
            if (oMUI != null)
            {
                oMUI.ucDBName = FlowExpress._FlowSysDB;
                oMUI.ucTableName = "FlowStepBtn";
                oMUI.ucPKFieldList = "FlowID,FlowStepID,FlowStepBtnID".Split(',');
                oMUI.ucPKValueList = (dr["FlowID"].ToString() + "," + dr["FlowStepID"].ToString() + "," + dr["FlowStepBtnID"].ToString()).Split(',');
            }
        }
    }

    void gvFlowStepBtnHideExp_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataRow dr;
        ucModalPopup1.Reset();
        ucModalPopup1.ucPanelID = pnlFlowStepBtnExp.ID;
        ucModalPopup1.ucPopupWidth = 650;
        ucModalPopup1.ucPopupHeight = 520;
        txtFlowStepBtnChkGrpNo.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
        txtFlowStepBtnChkSeqNo.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);

        hidExpTable.Value = "FlowStepBtnHideExp";
        //隱藏 StopExp 專用設定
        string strJS = @"dom.Ready(function(){ 
                            document.getElementById('forStopExpOnlyRow1').style.display = 'none';
                            document.getElementById('forStopExpOnlyRow2').style.display = 'none';
                        });";
        Util.setJSContent(strJS, "pnlFlowStepBtnExp_Init");
        switch (e.CommandName)
        {
            case "cmdEdit":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Edit;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];
                hidFlowStepBtnID.Value = e.DataKeys[2];
                txtFlowStepBtnChkGrpNo.ucTextData = e.DataKeys[3];
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = true;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = e.DataKeys[4];
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = true;
                txtFlowStepBtnChkSeqNo.Refresh();
                dr = db.ExecuteDataSet(string.Format("Select * From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4])).Tables[0].Rows[0];

                chkFlowStepBtnChkSessSW.Checked = (dr["FlowStepBtnChkSessSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = dr["FlowStepBtnChkSessNameObjectProperty"].ToString();
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = dr["FlowStepBtnChkSessExp"].ToString();
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = dr["FlowStepBtnChkSessValue"].ToString();

                chkFlowStepBtnChkCustVarSW.Checked = (dr["FlowStepBtnChkCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustVarName.ucTextData = dr["FlowStepBtnChkCustVarName"].ToString();
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = dr["FlowStepBtnChkCustVarExp"].ToString();
                ddlFlowStepBtnChkCustVarExp.Refresh();
                txtFlowStepBtnChkCustVarValue.ucTextData = dr["FlowStepBtnChkCustVarValue"].ToString();

                chkFlowStepBtnChkCustTableSW.Checked = (dr["FlowStepBtnChkCustTableSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustTable.ucTextData = dr["FlowStepBtnChkCustTable"].ToString();
                txtFlowStepBtnChkCustField.ucTextData = dr["FlowStepBtnChkCustField"].ToString();
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = dr["FlowStepBtnChkCustExp"].ToString();
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = dr["FlowStepBtnChkCustValue"].ToString();

                txtRemark.ucTextData = dr["Remark"].ToString();

                ucModalPopup1.Show();
                break;
            case "cmdCopy":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Copy;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];
                hidFlowStepBtnID.Value = e.DataKeys[2];
                txtFlowStepBtnChkGrpNo.ucTextData = e.DataKeys[3];
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = false;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = e.DataKeys[4];
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = false;
                txtFlowStepBtnChkSeqNo.Refresh();
                dr = db.ExecuteDataSet(string.Format("Select * From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4])).Tables[0].Rows[0];

                chkFlowStepBtnChkSessSW.Checked = (dr["FlowStepBtnChkSessSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = dr["FlowStepBtnChkSessNameObjectProperty"].ToString();
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = dr["FlowStepBtnChkSessExp"].ToString();
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = dr["FlowStepBtnChkSessValue"].ToString();

                chkFlowStepBtnChkCustVarSW.Checked = (dr["FlowStepBtnChkCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustVarName.ucTextData = dr["FlowStepBtnChkCustVarName"].ToString();
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = dr["FlowStepBtnChkCustVarExp"].ToString();
                ddlFlowStepBtnChkCustVarExp.Refresh();
                txtFlowStepBtnChkCustVarValue.ucTextData = dr["FlowStepBtnChkCustVarValue"].ToString();

                chkFlowStepBtnChkCustTableSW.Checked = (dr["FlowStepBtnChkCustTableSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustTable.ucTextData = dr["FlowStepBtnChkCustTable"].ToString();
                txtFlowStepBtnChkCustField.ucTextData = dr["FlowStepBtnChkCustField"].ToString();
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = dr["FlowStepBtnChkCustExp"].ToString();
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = dr["FlowStepBtnChkCustValue"].ToString();

                txtRemark.ucTextData = dr["Remark"].ToString();
                ucModalPopup1.Show();
                break;
            case "cmdAdd":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Add;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = _FlowID;
                hidFlowStepID.Value = _FlowStepID;
                hidFlowStepBtnID.Value = _FlowStepBtnID;
                txtFlowStepBtnChkGrpNo.ucTextData = "9";
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = false;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = "9";
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = false;
                txtFlowStepBtnChkSeqNo.Refresh();

                chkFlowStepBtnChkSessSW.Checked = false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = "";
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = "";
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = "";

                chkFlowStepBtnChkCustVarSW.Checked = false;
                txtFlowStepBtnChkCustVarName.ucTextData = "";
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = "";
                ddlFlowStepBtnChkCustVarExp.Refresh();

                txtFlowStepBtnChkCustVarValue.ucTextData = "";

                chkFlowStepBtnChkCustTableSW.Checked = false;
                txtFlowStepBtnChkCustTable.ucTextData = "";
                txtFlowStepBtnChkCustField.ucTextData = "";
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = "";
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = "";

                txtRemark.ucTextData = "";
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                try
                {
                    db.ExecuteNonQuery(string.Format("Delete From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4]));
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, hidExpTable.Value, Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    Util.MsgBox(ex.Message);
                }
                break;
        }
    }

    void gvFlowStepBtnStopExp_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        //2017.05.23
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataRow dr;
        ucModalPopup1.Reset();
        ucModalPopup1.ucPanelID = pnlFlowStepBtnExp.ID;
        ucModalPopup1.ucPopupWidth = 820;
        ucModalPopup1.ucPopupHeight = 700;
        txtFlowStepBtnChkGrpNo.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);
        txtFlowStepBtnChkSeqNo.ucRegExp = Util.getRegExp(Util.CommRegExp.Integer);

        hidExpTable.Value = "FlowStepBtnStopExp";

        //顯示 StopExp 專用設定
        string strJS = @"dom.Ready(function(){ 
                            document.getElementById('forStopExpOnlyRow1').style.display = '';
                            document.getElementById('forStopExpOnlyRow2').style.display = '';
                        });";
        Util.setJSContent(strJS, "pnlFlowStepBtnExp_Init");

        //處理 [labCustClassChkFlowPara] 顯示
        FlowExpress oFlow = new FlowExpress(_FlowID);
        labCustClassChkFlowPara.Text = "_FlowKeyValue";
        for (int k = 0; k < oFlow.FlowKeyFieldList.Count(); k++)
        {
            labCustClassChkFlowPara.Text += string.Format(",_{0}", oFlow.FlowKeyFieldList[k]);
        }

        switch (e.CommandName)
        {
            case "cmdEdit":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Edit;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];
                hidFlowStepBtnID.Value = e.DataKeys[2];
                txtFlowStepBtnChkGrpNo.ucTextData = e.DataKeys[3];
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = true;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = e.DataKeys[4];
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = true;
                txtFlowStepBtnChkSeqNo.Refresh();
                dr = db.ExecuteDataSet(string.Format("Select * From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4])).Tables[0].Rows[0];

                txtFlowStepBtnChkReason.ucTextData = dr["FlowStepBtnChkReason"].ToString();
                chkFlowStepBtnChkCustClassSW.Checked = (dr["FlowStepBtnChkCustClassSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustClassMethod.ucTextData = dr["FlowStepBtnChkCustClassMethod"].ToString();
                txtFlowStepBtnChkCustParaList.ucTextData = dr["FlowStepBtnChkCustParaList"].ToString();

                chkFlowStepBtnChkSessSW.Checked = (dr["FlowStepBtnChkSessSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = dr["FlowStepBtnChkSessNameObjectProperty"].ToString();
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = dr["FlowStepBtnChkSessExp"].ToString();
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = dr["FlowStepBtnChkSessValue"].ToString();

                chkFlowStepBtnChkCustVarSW.Checked = (dr["FlowStepBtnChkCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustVarName.ucTextData = dr["FlowStepBtnChkCustVarName"].ToString();
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = dr["FlowStepBtnChkCustVarExp"].ToString();
                ddlFlowStepBtnChkCustVarExp.Refresh();
                txtFlowStepBtnChkCustVarValue.ucTextData = dr["FlowStepBtnChkCustVarValue"].ToString();

                chkFlowStepBtnChkCustTableSW.Checked = (dr["FlowStepBtnChkCustTableSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustTable.ucTextData = dr["FlowStepBtnChkCustTable"].ToString();
                txtFlowStepBtnChkCustField.ucTextData = dr["FlowStepBtnChkCustField"].ToString();
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = dr["FlowStepBtnChkCustExp"].ToString();
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = dr["FlowStepBtnChkCustValue"].ToString();

                txtRemark.ucTextData = dr["Remark"].ToString();

                ucModalPopup1.Show();
                break;
            case "cmdCopy":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Copy;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = e.DataKeys[0];
                hidFlowStepID.Value = e.DataKeys[1];
                hidFlowStepBtnID.Value = e.DataKeys[2];
                txtFlowStepBtnChkGrpNo.ucTextData = e.DataKeys[3];
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = false;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = e.DataKeys[4];
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = false;
                txtFlowStepBtnChkSeqNo.Refresh();
                dr = db.ExecuteDataSet(string.Format("Select * From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4])).Tables[0].Rows[0];

                txtFlowStepBtnChkReason.ucTextData = dr["FlowStepBtnChkReason"].ToString();
                chkFlowStepBtnChkCustClassSW.Checked = (dr["FlowStepBtnChkCustClassSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustClassMethod.ucTextData = dr["FlowStepBtnChkCustClassMethod"].ToString();
                txtFlowStepBtnChkCustParaList.ucTextData = dr["FlowStepBtnChkCustParaList"].ToString();

                chkFlowStepBtnChkSessSW.Checked = (dr["FlowStepBtnChkSessSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = dr["FlowStepBtnChkSessNameObjectProperty"].ToString();
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = dr["FlowStepBtnChkSessExp"].ToString();
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = dr["FlowStepBtnChkSessValue"].ToString();

                chkFlowStepBtnChkCustVarSW.Checked = (dr["FlowStepBtnChkCustVarSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustVarName.ucTextData = dr["FlowStepBtnChkCustVarName"].ToString();
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = dr["FlowStepBtnChkCustVarExp"].ToString();
                ddlFlowStepBtnChkCustVarExp.Refresh();
                txtFlowStepBtnChkCustVarValue.ucTextData = dr["FlowStepBtnChkCustVarValue"].ToString();

                chkFlowStepBtnChkCustTableSW.Checked = (dr["FlowStepBtnChkCustTableSW"].ToString().ToUpper() == "Y") ? true : false;
                txtFlowStepBtnChkCustTable.ucTextData = dr["FlowStepBtnChkCustTable"].ToString();
                txtFlowStepBtnChkCustField.ucTextData = dr["FlowStepBtnChkCustField"].ToString();
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = dr["FlowStepBtnChkCustExp"].ToString();
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = dr["FlowStepBtnChkCustValue"].ToString();

                txtRemark.ucTextData = dr["Remark"].ToString();
                ucModalPopup1.Show();
                break;
            case "cmdAdd":
                ucModalPopup1.ucPopupHeader = RS.Resources.Msg_Add;
                hidCmdName.Value = e.CommandName;
                hidFlowID.Value = _FlowID;
                hidFlowStepID.Value = _FlowStepID;
                hidFlowStepBtnID.Value = _FlowStepBtnID;
                txtFlowStepBtnChkGrpNo.ucTextData = "9";
                txtFlowStepBtnChkGrpNo.ucIsReadOnly = false;
                txtFlowStepBtnChkGrpNo.Refresh();
                txtFlowStepBtnChkSeqNo.ucTextData = "9";
                txtFlowStepBtnChkSeqNo.ucIsReadOnly = false;
                txtFlowStepBtnChkSeqNo.Refresh();

                txtFlowStepBtnChkReason.ucTextData = "";
                chkFlowStepBtnChkCustClassSW.Checked = false;
                txtFlowStepBtnChkCustClassMethod.ucTextData = "";
                txtFlowStepBtnChkCustParaList.ucTextData = "";

                chkFlowStepBtnChkSessSW.Checked = false;
                txtFlowStepBtnChkSessNameObjectProperty.ucTextData = "";
                ddlFlowStepBtnChkSessExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkSessExp.ucSelectedID = "";
                ddlFlowStepBtnChkSessExp.Refresh();
                txtFlowStepBtnChkSessValue.ucTextData = "";

                chkFlowStepBtnChkCustVarSW.Checked = false;
                txtFlowStepBtnChkCustVarName.ucTextData = "";
                ddlFlowStepBtnChkCustVarExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustVarExp.ucSelectedID = "";
                ddlFlowStepBtnChkCustVarExp.Refresh();
                txtFlowStepBtnChkCustVarValue.ucTextData = "";

                chkFlowStepBtnChkCustTableSW.Checked = false;
                txtFlowStepBtnChkCustTable.ucTextData = "";
                txtFlowStepBtnChkCustField.ucTextData = "";
                ddlFlowStepBtnChkCustExp.ucSourceDictionary = Util.getDictionary(_Dic_Exp);
                ddlFlowStepBtnChkCustExp.ucSelectedID = "";
                ddlFlowStepBtnChkCustExp.Refresh();
                txtFlowStepBtnChkCustValue.ucTextData = "";

                txtRemark.ucTextData = "";
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                try
                {
                    db.ExecuteNonQuery(string.Format("Delete From {0} Where FlowID='{1}' and FlowStepID = '{2}' and FlowStepBtnID = '{3}' and FlowStepBtnChkGrpNo = '{4}' and FlowStepBtnChkSeqNo = '{5}'", hidExpTable.Value, e.DataKeys[0], e.DataKeys[1], e.DataKeys[2], e.DataKeys[3], e.DataKeys[4]));
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, hidExpTable.Value, Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    Util.MsgBox(ex.Message);
                }
                break;
        }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        //bool ss = forStopExpOnlyRow1.Visible;
        //forStopExpOnlyRow1.Visible = true;
        Dictionary<string, string> oDic = Util.getControlEditResult(pnlFlowStepBtnExp);
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        CommandHelper sb = db.CreateCommandHelper();
        switch (hidCmdName.Value)
        {
            case "cmdEdit":
                sb.Reset();
                sb.AppendStatement(string.Format("Update {0} Set ", oDic["hidExpTable"]));

                sb.Append("  FlowStepBtnChkSessSW        = ").AppendParameter("chkFlowStepBtnChkSessSW", oDic["chkFlowStepBtnChkSessSW"]);
                sb.Append(", FlowStepBtnChkSessNameObjectProperty      = ").AppendParameter("txtFlowStepBtnChkSessNameObjectProperty", oDic["txtFlowStepBtnChkSessNameObjectProperty"]);
                sb.Append(", FlowStepBtnChkSessExp       = ").AppendParameter("ddlFlowStepBtnChkSessExp", oDic["ddlFlowStepBtnChkSessExp"]);
                sb.Append(", FlowStepBtnChkSessValue     = ").AppendParameter("txtFlowStepBtnChkSessValue", oDic["txtFlowStepBtnChkSessValue"]);

                sb.Append(", FlowStepBtnChkCustVarSW     = ").AppendParameter("chkFlowStepBtnChkCustVarSW", oDic["chkFlowStepBtnChkCustVarSW"]);
                sb.Append(", FlowStepBtnChkCustVarName   = ").AppendParameter("txtFlowStepBtnChkCustVarName", oDic["txtFlowStepBtnChkCustVarName"]);
                sb.Append(", FlowStepBtnChkCustVarExp    = ").AppendParameter("ddlFlowStepBtnChkCustVarExp", oDic["ddlFlowStepBtnChkCustVarExp"]);
                sb.Append(", FlowStepBtnChkCustVarValue  = ").AppendParameter("txtFlowStepBtnChkCustVarValue", oDic["txtFlowStepBtnChkCustVarValue"]);

                sb.Append(", FlowStepBtnChkCustTableSW   = ").AppendParameter("chkFlowStepBtnChkCustTableSW", oDic["chkFlowStepBtnChkCustTableSW"]);
                sb.Append(", FlowStepBtnChkCustTable     = ").AppendParameter("txtFlowStepBtnChkCustTable", oDic["txtFlowStepBtnChkCustTable"]);
                sb.Append(", FlowStepBtnChkCustField     = ").AppendParameter("txtFlowStepBtnChkCustField", oDic["txtFlowStepBtnChkCustField"]);
                sb.Append(", FlowStepBtnChkCustExp       = ").AppendParameter("ddlFlowStepBtnChkCustExp", oDic["ddlFlowStepBtnChkCustExp"]);
                sb.Append(", FlowStepBtnChkCustValue     = ").AppendParameter("txtFlowStepBtnChkCustValue", oDic["txtFlowStepBtnChkCustValue"]);

                sb.Append(", Remark      = ").AppendParameter("txtRemark", oDic["txtRemark"]);
                sb.Append(", UpdUser = ").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
                sb.Append(", UpdDateTime = ").AppendDbDateTime();

                if (oDic["hidExpTable"] == "FlowStepBtnStopExp")
                {
                    sb.Append(", FlowStepBtnChkReason        = ").AppendParameter("txtFlowStepBtnChkReason", oDic["txtFlowStepBtnChkReason"]);
                    sb.Append(", FlowStepBtnChkCustClassSW   = ").AppendParameter("chkFlowStepBtnChkCustClassSW", oDic["chkFlowStepBtnChkCustClassSW"]);
                    sb.Append(", FlowStepBtnChkCustClassMethod  = ").AppendParameter("txtFlowStepBtnChkCustClassMethod", oDic["txtFlowStepBtnChkCustClassMethod"]);
                    sb.Append(", FlowStepBtnChkCustParaList     = ").AppendParameter("txtFlowStepBtnChkCustParaList", oDic["txtFlowStepBtnChkCustParaList"]);
                }

                sb.Append(" Where 0=0 ");
                sb.Append(" And FlowID    =").AppendParameter("hidFlowID", oDic["hidFlowID"]);
                sb.Append(" And FlowStepID =").AppendParameter("hidFlowStepID", oDic["hidFlowStepID"]);
                sb.Append(" And FlowStepBtnID =").AppendParameter("hidFlowStepBtnID", oDic["hidFlowStepBtnID"]);
                sb.Append(" And FlowStepBtnChkGrpNo =").AppendParameter("txtFlowStepBtnChkGrpNo", oDic["txtFlowStepBtnChkGrpNo"]);
                sb.Append(" And FlowStepBtnChkSeqNo =").AppendParameter("txtFlowStepBtnChkSeqNo", oDic["txtFlowStepBtnChkSeqNo"]);

                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[更新]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, oDic["hidExpTable"], string.Format("{0},{1},{2},{3},{4}", oDic["hidFlowID"], oDic["hidFlowStepID"], oDic["hidFlowStepBtnID"], oDic["txtFlowStepBtnChkGrpNo"], oDic["txtFlowStepBtnChkSeqNo"]), LogHelper.AppDataLogType.Update, oDic);
                    Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_EditFail, Util.NotifyKind.Error);
                }
                break;
            case "cmdAdd":
            case "cmdCopy":
                sb.AppendStatement(string.Format("Insert {0} (", oDic["hidExpTable"]));
                sb.Append(" FlowID,FlowStepID,FlowStepBtnID,FlowStepBtnChkGrpNo,FlowStepBtnChkSeqNo");
                if (oDic["hidExpTable"] == "FlowStepBtnStopExp")
                {
                    sb.Append(",FlowStepBtnChkReason,FlowStepBtnChkCustClassSW,FlowStepBtnChkCustClassMethod,FlowStepBtnChkCustParaList");
                }
                sb.Append(",FlowStepBtnChkSessSW,FlowStepBtnChkSessNameObjectProperty,FlowStepBtnChkSessExp,FlowStepBtnChkSessValue");
                sb.Append(",FlowStepBtnChkCustVarSW,FlowStepBtnChkCustVarName,FlowStepBtnChkCustVarExp,FlowStepBtnChkCustVarValue");
                sb.Append(",FlowStepBtnChkCustTableSW,FlowStepBtnChkCustTable,FlowStepBtnChkCustField,FlowStepBtnChkCustExp,FlowStepBtnChkCustValue");
                sb.Append(",Remark,UpdUser,UpdDateTime ) ");

                sb.Append(" Values (").AppendParameter("hidFlowID", oDic["hidFlowID"]);
                sb.Append("        ,").AppendParameter("hidFlowStepID", oDic["hidFlowStepID"]);
                sb.Append("        ,").AppendParameter("hidFlowStepBtnID", oDic["hidFlowStepBtnID"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkGrpNo", oDic["txtFlowStepBtnChkGrpNo"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkSeqNo", oDic["txtFlowStepBtnChkSeqNo"]);

                if (oDic["hidExpTable"] == "FlowStepBtnStopExp")
                {
                    sb.Append("        ,").AppendParameter("txtFlowStepBtnChkReason", oDic["txtFlowStepBtnChkReason"]);
                    sb.Append("        ,").AppendParameter("chkFlowStepBtnChkCustClassSW", oDic["chkFlowStepBtnChkCustClassSW"]);
                    sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustClassMethod", oDic["txtFlowStepBtnChkCustClassMethod"]);
                    sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustParaList", oDic["txtFlowStepBtnChkCustParaList"]);
                }

                sb.Append("        ,").AppendParameter("chkFlowStepBtnChkSessSW", oDic["chkFlowStepBtnChkSessSW"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkSessNameObjectProperty", oDic["txtFlowStepBtnChkSessNameObjectProperty"]);
                sb.Append("        ,").AppendParameter("ddlFlowStepBtnChkSessExp", oDic["ddlFlowStepBtnChkSessExp"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkSessValue", oDic["txtFlowStepBtnChkSessValue"]);

                sb.Append("        ,").AppendParameter("chkFlowStepBtnChkCustVarSW", oDic["chkFlowStepBtnChkCustVarSW"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustVarName", oDic["txtFlowStepBtnChkCustVarName"]);
                sb.Append("        ,").AppendParameter("ddlFlowStepBtnChkCustVarExp", oDic["ddlFlowStepBtnChkCustVarExp"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustVarValue", oDic["txtFlowStepBtnChkCustVarValue"]);

                sb.Append("        ,").AppendParameter("chkFlowStepBtnChkCustTableSW", oDic["chkFlowStepBtnChkCustTableSW"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustTable", oDic["txtFlowStepBtnChkCustTable"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustField", oDic["txtFlowStepBtnChkCustField"]);
                sb.Append("        ,").AppendParameter("ddlFlowStepBtnChkCustExp", oDic["ddlFlowStepBtnChkCustExp"]);
                sb.Append("        ,").AppendParameter("txtFlowStepBtnChkCustValue", oDic["txtFlowStepBtnChkCustValue"]);

                sb.Append("        ,").AppendParameter("txtRemark", oDic["txtRemark"]);
                sb.Append("        ,").AppendParameter("UpdUser", UserInfo.getUserInfo().UserID);
                sb.Append("        ,").AppendDbDateTime();
                sb.Append(")");

                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[新增/複製]資料異動Log
                    //因為會先編修後才儲存，故LogType = [Create]
                    string strNewKey = string.Format("{0},{1},{2},{3},{4}", oDic["hidFlowID"], oDic["hidFlowStepID"], oDic["hidFlowStepBtnID"], oDic["txtFlowStepBtnChkGrpNo"], oDic["txtFlowStepBtnChkSeqNo"]);
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, oDic["hidExpTable"], strNewKey, LogHelper.AppDataLogType.Create, oDic);
                    Util.NotifyMsg(RS.Resources.Msg_Succeed, Util.NotifyKind.Success);
                }
                catch
                {
                    Util.NotifyMsg(RS.Resources.Msg_Error, Util.NotifyKind.Error);
                }
                break;
            default:
                break;
        }
        RefreshGridView();
    }

    protected void RefreshGridView(bool IsInit = false)
    {
        //FlowStepBtnHideExp
        Dictionary<string, string> oHideExpDisp = new Dictionary<string, string>();
        oHideExpDisp.Clear();
        oHideExpDisp.Add("FlowStepBtnChkGrpNo", "群組@C");
        oHideExpDisp.Add("FlowStepBtnChkSeqNo", "順序@C");
        oHideExpDisp.Add("FlowStepBtnChkSessSW", "Sess@Y");
        oHideExpDisp.Add("FlowStepBtnChkCustVarSW", "自訂變數@Y");
        oHideExpDisp.Add("FlowStepBtnChkCustTableSW", "資料表@Y");
        oHideExpDisp.Add("Remark", "備　　註@L150");
        oHideExpDisp.Add("UpdUser", "更新人員");
        oHideExpDisp.Add("UpdDateTime", "更新日期@T");

        gvFlowStepBtnHideExp.ucDBName = FlowExpress._FlowSysDB;
        gvFlowStepBtnHideExp.ucDataQrySQL = string.Format("Select * From FlowStepBtnHideExp Where FlowID = '{0}' And FlowStepID = '{1}' and FlowStepBtnID = '{2}' ", _FlowID, _FlowStepID, _FlowStepBtnID);
        gvFlowStepBtnHideExp.ucDataKeyList = "FlowID,FlowStepID,FlowStepBtnID,FlowStepBtnChkGrpNo,FlowStepBtnChkSeqNo".Split(',');
        gvFlowStepBtnHideExp.ucDataDisplayDefinition = oHideExpDisp;
        gvFlowStepBtnHideExp.ucSortEnabled = false;
        gvFlowStepBtnHideExp.ucSeqNoEnabled = false;
        gvFlowStepBtnHideExp.ucAddEnabled = true;
        gvFlowStepBtnHideExp.ucCopyEnabled = true;
        gvFlowStepBtnHideExp.ucEditEnabled = true;
        gvFlowStepBtnHideExp.ucDeleteEnabled = true;
        gvFlowStepBtnHideExp.Refresh(IsInit);


        //FlowStepBtnStopExp  2017.05.24 新增
        Dictionary<string, string> oStopExpDisp = new Dictionary<string, string>();
        oStopExpDisp.Clear();
        oStopExpDisp.Add("FlowStepBtnChkGrpNo", "群組@C");
        oStopExpDisp.Add("FlowStepBtnChkSeqNo", "順序@C");
        oStopExpDisp.Add("FlowStepBtnChkCustClassSW", "自訂類別@Y");
        oStopExpDisp.Add("FlowStepBtnChkSessSW", "Sess@Y");
        oStopExpDisp.Add("FlowStepBtnChkCustVarSW", "自訂變數@Y");
        oStopExpDisp.Add("FlowStepBtnChkCustTableSW", "資料表@Y");
        oStopExpDisp.Add("FlowStepBtnChkReason", "原　　因@L150");
        oStopExpDisp.Add("UpdUser", "更新人員");
        oStopExpDisp.Add("UpdDateTime", "更新日期@T");

        gvFlowStepBtnStopExp.ucDBName = FlowExpress._FlowSysDB;
        gvFlowStepBtnStopExp.ucDataQrySQL = string.Format("Select * From FlowStepBtnStopExp Where FlowID = '{0}' And FlowStepID = '{1}' and FlowStepBtnID = '{2}' ", _FlowID, _FlowStepID, _FlowStepBtnID);
        gvFlowStepBtnStopExp.ucDataKeyList = "FlowID,FlowStepID,FlowStepBtnID,FlowStepBtnChkGrpNo,FlowStepBtnChkSeqNo".Split(',');
        gvFlowStepBtnStopExp.ucDataDisplayDefinition = oStopExpDisp;
        gvFlowStepBtnStopExp.ucSortEnabled = false;
        gvFlowStepBtnStopExp.ucSeqNoEnabled = false;
        gvFlowStepBtnStopExp.ucAddEnabled = true;
        gvFlowStepBtnStopExp.ucCopyEnabled = true;
        gvFlowStepBtnStopExp.ucEditEnabled = true;
        gvFlowStepBtnStopExp.ucDeleteEnabled = true;
        gvFlowStepBtnStopExp.Refresh(IsInit);
    }
}