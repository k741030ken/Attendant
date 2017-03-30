using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

public partial class LegalSample_CaseTodoList : SecurePage
{
    //案件所屬的　資料庫/資料表/流程　名稱　

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //判斷是否需檢查超過90天未結案的Case
            DateTime chkDate = DateTime.Today.AddDays(-1); //預設今天還沒檢查過
            if (Application[LegalSample._LegalConsultCaseTableName + "ChkDiviDate"] != null)
            {
                chkDate = (DateTime)Application[LegalSample._LegalConsultCaseTableName + "ChkDiviDate"];
            }
            else
            {
                Application[LegalSample._LegalConsultCaseTableName + "ChkDiviDate"] = chkDate;
            }

            if (chkDate != DateTime.Today)
            {
                //代表今天沒檢查過
                Application[LegalSample._LegalConsultCaseTableName + "ChkDiviDate"] = DateTime.Today;
                //取出分案後，天數超過 _CaseIdleDays，需強制結案的資料
                string strChkSQL = string.Format("Select FlowID,FlowCaseID From {0} Where CaseStatus = 'Open' and CaseDiviDate is not null and DATEDIFF(day,CaseDiviDate,getdate()) > {1} ", LegalSample._LegalConsultCaseTableName, LegalSample._CaseDiviTimeoutDays);
                DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
                DataTable dt = db.ExecuteDataSet(CommandType.Text, strChkSQL).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        FlowExpress oFlow = new FlowExpress(dt.Rows[i]["FlowID"].ToString(), dt.Rows[i]["FlowCaseID"].ToString(), false);
                        string strOpinion = "逾時未結案／逾时未结案";
                        FlowExpress.IsFlowSysStepBtnVerify(oFlow.FlowID, oFlow.FlowCurrLastLogID, "FlowSystem", "Z02", null, strOpinion); //強制結案
                    }
                }
            }

            //設定待辦清單要處理的流程ID
            ucFlowTodoList1.ucFlowIDList = "LegalMain,LegalSub".Split(',');

            //ucFlowTodoList1.ucIsUsingCheckBoxListWhenMultiSelect = true;                      //複選指派對象時使用 ChkBoxLis
            //ucFlowTodoList1.ucChkMaxKeyLen = 5;                                               //若指派清單鍵值超出此長度，則顯示時不合併顯示鍵值
            //ucFlowTodoList1.ucIsPopupVerifyEnabled = false;                                   //將彈出式審核，強制改用轉址式審核
            //ucFlowTodoList1.ucPopupBtnCompleteEnabled = true;                                 //彈出的審核視窗，要顯示[關閉]按鈕
            //ucFlowTodoList1.ucIsEnabledProxyTodoList = false;                                 //隱藏代理待辦清單
            //ucFlowTodoList1.ucProxyFullFilter = "ProxyType='Full' and ProxyCompID = 'TW' ";   //從UserInfo.ProxyInfoTable過濾出指定代理人員
            //ucFlowTodoList1.ucProxySemiFilter = "ProxyType='Semi' and ProxyCompID = 'TW' ";   //從UserInfo.ProxyInfoTable過濾出指定助理人員

            ucFlowTodoList1.Refresh(true);
        }
    }
}