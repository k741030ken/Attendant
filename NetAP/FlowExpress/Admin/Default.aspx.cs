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
/// 流程管理入口
/// </summary>
public partial class FlowExpress_Admin_Default : SecurePage
{
    private string[] _MainPKList = "FlowID".Split(',');
    private string _MainQrySQL = "Select FlowID,FlowName,FlowKeyFieldList,FlowKeyCaptionList,FlowShowFieldList,FlowShowCaptionList,UpdUser,UpdDateTime From FlowSpec ";

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setRequestValidatorBypassIDList("*");
        pnlNewFlow.Visible = false;
        if (!IsPostBack) { Refresh(true); }
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        //throw new NotImplementedException();
        ucGridView1.Refresh();
    }

    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        //Util.MsgBox(e.CommandName);
        switch (e.CommandName)
        {
            case "cmdSelect":
                Response.Redirect(string.Format("FlowSpec.aspx?FlowID={0}", e.DataKeys[0]));
                break;
            case "cmdCopy":
                hidFlowID.Value = e.DataKeys[0];
                FlowExpress oFlow = new FlowExpress(e.DataKeys[0], null, false, false);
                txtNewFlowID.ucTextData = "Cpy-" + oFlow.FlowID.Left(15);
                txtNewFlowName.ucTextData = "Cpy-" + oFlow.FlowName;

                txtNewFlowID.ucDispEnteredWordsObjClientID = cntNewFlowID.ClientID;
                txtNewFlowID.ucIsDispEnteredWords = true;
                txtNewFlowID.Refresh();
                txtNewFlowName.ucDispEnteredWordsObjClientID = cntNewFlowName.ClientID;
                txtNewFlowName.ucIsDispEnteredWords = true;
                txtNewFlowName.Refresh();

                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 450;
                ucModalPopup1.ucPopupHeight = 150;
                ucModalPopup1.ucPopupHeader = "複製流程";
                ucModalPopup1.ucPanelID = pnlNewFlow.ID;
                ucModalPopup1.Show();
                break;
            case "cmdAdd":
                hidFlowID.Value = "";
                txtNewFlowID.ucTextData = "New-ID";
                txtNewFlowName.ucTextData = "New-Name";

                txtNewFlowID.ucDispEnteredWordsObjClientID = cntNewFlowID.ClientID;
                txtNewFlowID.ucIsDispEnteredWords = true;
                txtNewFlowID.Refresh();
                txtNewFlowName.ucDispEnteredWordsObjClientID = cntNewFlowName.ClientID;
                txtNewFlowName.ucIsDispEnteredWords = true;
                txtNewFlowName.Refresh();

                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 450;
                ucModalPopup1.ucPopupHeight = 150;
                ucModalPopup1.ucPopupHeader = "新增流程";
                ucModalPopup1.ucPanelID = pnlNewFlow.ID;
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
                CommandHelper sb = db.CreateCommandHelper();

                sb.Reset();
                sb.AppendStatement("Delete FlowSpec      Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowSpec_MUI  Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);

                sb.AppendStatement("Delete FlowStep      Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowStep_MUI  Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);

                sb.AppendStatement("Delete FlowStepBtn           Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowStepBtn_MUI       Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowStepBtnHideExp    Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);

                sb.AppendStatement("Delete FlowUpdCustDataExp    Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);

                sb.AppendStatement("Delete FlowCustGrp           Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowCustGrp_MUI       Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowCustGrpDetail     Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);
                sb.AppendStatement("Delete FlowCustGrpDetail_MUI Where FlowID =").AppendParameter("FlowID", e.DataKeys[0]);

                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand());
                    //[刪除]資料異動Log
                    LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowSpec", Util.getStringJoin(e.DataKeys), LogHelper.AppDataLogType.Delete);
                    Util.NotifyMsg(RS.Resources.Msg_DeleteSucceed, Util.NotifyKind.Success);
                }
                catch (Exception ex)
                {
                    Util.MsgBox(ex.Message);
                }
                break;
            case "cmdDownload":
                //Data Dump
                string strHeader = string.Format("[{0}] 資料轉儲 SQL", e.DataKeys[0]);
                string strDataFilter = string.Format(" FlowID = '{0}' ", e.DataKeys[0]);
                string strDumpSQL = string.Empty;

                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("/* {0}     on [{1}] */ \n", strHeader, DateTime.Now);
                strDumpSQL += "/* ==================================================================== */ \n";
                strDumpSQL += string.Format("Use {0} \nGo\n", FlowExpress._FlowSysDB);
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* FlowSpec    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowSpec", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowSpec_MUI", strDataFilter);

                strDumpSQL += "\n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* FlowStep    相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowStep", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowStep_MUI", strDataFilter);

                strDumpSQL += "\n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* FlowStepBtn 相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowStepBtn", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowStepBtn_MUI", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowStepBtnHideExp", strDataFilter);

                strDumpSQL += "\n";
                strDumpSQL += "/* =========================== */ \n";
                strDumpSQL += "/* FlowUpdCustDataExp 相關物件 */ \n";
                strDumpSQL += "/* =========================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowUpdCustDataExp", strDataFilter);

                strDumpSQL += "\n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += "/* FlowCustGrp 相關物件 */ \n";
                strDumpSQL += "/* ==================== */ \n";
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowCustGrp", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowCustGrp_MUI", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowCustGrpDetail", strDataFilter);
                strDumpSQL += Util.getDataDumpSQL(FlowExpress._FlowSysDB, "FlowCustGrpDetail_MUI", strDataFilter);

                txtDumpSQL.ucTextData = strDumpSQL;
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = 850;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucPopupHeader = strHeader;
                ucModalPopup1.ucPanelID = pnlDumpSQL.ID;
                ucModalPopup1.Show();

                break;
        }
        (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
    }

    private void Refresh(bool IsInit = false)
    {
        if (IsInit)
        {
            ucGridView1.ucDBName = FlowExpress._FlowSysDB;
            ucGridView1.ucDataQrySQL = _MainQrySQL;
            ucGridView1.ucDataKeyList = _MainPKList;

            Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
            dicDisplay.Clear();
            dicDisplay.Add("FlowID", "FlowID");
            dicDisplay.Add("FlowName", "FlowName");
            dicDisplay.Add("FlowKeyFieldList", "KeyField");
            dicDisplay.Add("FlowKeyCaptionList", "KeyCaption");
            dicDisplay.Add("FlowShowCaptionList", "ShowCaption@L330");
            //dicDisplay.Add("UpdUser", "UpdUser");
            dicDisplay.Add("UpdDateTime", "UpdTime@T");
            ucGridView1.ucDataDisplayDefinition = dicDisplay;

            ucGridView1.ucSelectEnabled = true;
            ucGridView1.ucAddEnabled = true;
            ucGridView1.ucCopyEnabled = true;
            ucGridView1.ucDownloadEnabled = true;
            ucGridView1.ucDownloadIcon = Util.Icon_DataDump;
            ucGridView1.ucDownloadToolTip = RS.Resources.GridView_DataDump;
            ucGridView1.ucDeleteEnabled = true;
            ucGridView1.ucDeleteConfirm = "將刪除此流程所有相關資料，確定執行？";
            ucGridView1.Refresh(true);
        }

    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        if (string.IsNullOrEmpty(hidFlowID.Value))
        {
            //新增
            string strAddSQL = string.Format("Insert FlowSpec (FlowID,FlowName,FlowTraceEnabled,FlowBatchEnabled,FlowKeyFieldList,FlowKeyCaptionList,FlowShowFieldList,FlowShowCaptionList) Values('{0}','{1}','N','N','CaseID','案號','Subject','主旨');", txtNewFlowID.ucTextData, txtNewFlowName.ucTextData);
            try
            {
                db.ExecuteNonQuery(strAddSQL);
                //[新增]資料異動Log
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowSpec", string.Format("{0}", txtNewFlowID.ucTextData), LogHelper.AppDataLogType.Create, Util.getControlEditResult(pnlNewFlow));
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
            //複製
            ArrayList arTable = new ArrayList();
            arTable.Add("FlowCustGrp");
            arTable.Add("FlowCustGrpDetail");
            arTable.Add("FlowSpec");
            arTable.Add("FlowStep");
            arTable.Add("FlowStepBtn");
            arTable.Add("FlowStepBtnHideExp");
            arTable.Add("FlowUpdCustDataExp");
            string strCopySQL = Util.getDataCopySQL(FlowExpress._FlowSysDB, "FlowID".Split(','), hidFlowID.Value.Split(','), txtNewFlowID.ucTextData.Split(','), Util.getArray(arTable));
            strCopySQL += string.Format("Update FlowSpec Set FlowName = N'{1}' Where FlowID = '{0}' ;", txtNewFlowID.ucTextData, txtNewFlowName.ucTextData);
            try
            {
                db.ExecuteNonQuery(strCopySQL);
                //[複製]資料異動Log
                DataRow drOld = db.ExecuteDataSet(string.Format("Select * from FlowSpec Where FlowID = '{0}' ", hidFlowID.Value)).Tables[0].Rows[0];
                LogHelper.WriteAppDataLog(FlowExpress._FlowSysDB, "FlowSpec", txtNewFlowID.ucTextData, LogHelper.AppDataLogType.Copy, Util.getDictionary(drOld));
                Util.NotifyMsg(RS.Resources.Msg_CopySucceed, Util.NotifyKind.Success);
                (this.Master as FlowExpress_Admin_FlowExpess).RefreshTreeView();
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex);
                Util.NotifyMsg(RS.Resources.Msg_CopyFail, Util.NotifyKind.Error);
            }
        }
        ucGridView1.Refresh();
    }
}