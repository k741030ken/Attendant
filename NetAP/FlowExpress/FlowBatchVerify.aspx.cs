using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading;
using System.Web;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程批次審核公用程式
/// </summary>
public partial class FlowExpress_FlowBatchVerify : SecurePage
{
    //private string IsDone;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (labIsSelfRefresh.Text.ToUpper() == "Y")
            {
                labIsSelfRefresh.Text = "N";
                StartBatchVerify();
            }
            //顯示「關閉」按鈕，並將頁面自動捲到最下方以便看到最後訊息
            string strJS = "";
            strJS += "dom.Ready(function(){ ";
            strJS += "var oClose = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
            strJS += "if (oClose != null){oClose.style.display='';}";
            strJS += "var oComplete = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
            strJS += "if (oComplete != null){oComplete.style.display='';}";
            strJS += "window.scrollTo(0, document.body.scrollHeight);";
            strJS += "});";
            Util.setJSContent(strJS, "Done");
            return;
        }
        else
        {
            labIsSelfRefresh.Text = "Y";
            //顯示「處理中」訊息
            string strJS = "";
            strJS += "dom.Ready(function(){ ";
            strJS += "var oClose = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
            strJS += "if (oClose != null){oClose.style.display='none';}";
            strJS += "var oComplete = parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
            strJS += "if (oComplete != null){oComplete.style.display='none';}";
            strJS += "});";
            Util.setJSContent(strJS, "Init");
            labFlowVerifyMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Waiting, WorkRS.Resources.FlowVerifyMsg_BatchVerifyStarting, 300);
            return;
        }
    }

    protected void StartBatchVerify()
    {
        labFlowVerifyMsg.Text = "";
        //取資料後立即清除，避免重複執行
        DataTable dtVerify = (DataTable)Session["BatchVerifyData"];
        Session["BatchVerifyData"] = null;
        UserInfo oUser = UserInfo.getUserInfo();

        if (oUser != null && dtVerify != null && dtVerify.Rows.Count > 0)
        {
            //執行審核
            BatchVerifyProcess(dtVerify);

            //審核結束
            labFlowVerifyMsg.Text += "<hr class='Util_clsHR'>";
            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Default, WorkRS.Resources.FlowVerifyMsg_BatchVerifyFinish);
            labFlowVerifyMsg.Text += "<br><br>";
        }
        else
        {
            //審核參數錯誤
            labFlowVerifyMsg.Text += "<hr class='Util_clsHR'>";
            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.ParaDataError);
            labFlowVerifyMsg.Text += "<br><br>";
        }

    }

    protected void BatchVerifyProcess(DataTable dtVerify)
    {

        UserInfo oUser = UserInfo.getUserInfo();
        if (oUser != null && dtVerify != null && dtVerify.Rows.Count > 0)
        {
            for (int dtCnt = 0; dtCnt < dtVerify.Rows.Count; dtCnt++)
            {
                Thread.Sleep(200);
                DataRow dr = dtVerify.Rows[dtCnt];
                string strFlowID = dr["FlowID"].ToString();
                string strFlowLogID = dr["FlowLogID"].ToString();
                string strProxyType = dr["ProxyType"].ToString();
                string strFlowStepBtnID = dr["FlowStepBtnID"].ToString();

                string strFlowStepBtnIsAddMultiSubFlow = dr["FlowStepBtnIsAddMultiSubFlow"].ToString();
                string strFlowStepBtnAddSubFlowID = dr["FlowStepBtnAddSubFlowID"].ToString();
                string strFlowStepBtnAddSubFlowStepBtnID = dr["FlowStepBtnAddSubFlowStepBtnID"].ToString();
                string strFlowStepOpinion = dr["FlowStepOpinion"].ToString();
                string strFlowStepOpinionForSQL = strFlowStepOpinion.Replace("'", "''");        //預防 FlowStepOpinion 含有單引號 2017.04.28

                bool IsNeedAddSubFlow = false;
                bool IsAddSubFlowSucceed = false;
                FlowExpress oFlow = new FlowExpress(strFlowID, strFlowLogID, true, false);
                Dictionary<string, string> oAssDic = Util.getDictionary(dr["FlowBatchVerifyStepBtnAssignToList"].ToString()); //取得批審指派對象


                if (strProxyType.ToUpper() == "SEMI")
                {
                    //助理無權審核，跳至下一筆
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_AssistantNotAllowVerify1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                    continue;
                }

                //檢查是否有按鈕停止條件 2017.05.25 新增
                string[] oStopReasonList;
                string strStopResonMsg;
                if (FlowExpress.IsFlowStepButtonStop(oFlow, strFlowStepBtnID, out oStopReasonList))
                {
                    //符合停止條件，跳至下一筆
                    strStopResonMsg = string.Format(WorkRS.Resources.FlowVerifyMsg_StopVerify1, oFlow.FlowCaseHtmlInfo);
                    if (!oStopReasonList.IsNullOrEmpty())
                    {
                        string strStopReasonTip = string.Format(" {0} \n", WorkRS.Resources.FlowVerifyMsg_StopReasonTipTitle);
                        for (int i = 0; i < oStopReasonList.Length; i++)
                        {
                            strStopReasonTip += string.Format(" ● {0}\n", oStopReasonList[i]);
                        }
                        strStopResonMsg = string.Format("<span title=\"{0}\">{1}</span>", strStopReasonTip, strStopResonMsg);
                    }
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, strStopResonMsg);
                    continue;
                }


                //=== BEGIN from FlowVerify 
                //處理[新增子流程]
                if (!string.IsNullOrEmpty(strFlowStepBtnAddSubFlowID) && !string.IsNullOrEmpty(strFlowStepBtnAddSubFlowStepBtnID))
                {
                    //檢查是否需自動新增子流程
                    FlowExpress oSubFlow = new FlowExpress(strFlowStepBtnAddSubFlowID, null, false, false);
                    //該子流程需符合以下條件才能自動新增：
                    //01.子流程與父流程名稱不同
                    //  子流程的 KeyFieldList 需只比主流程多出 _AutoNo 欄位　
                    //  子流程的 KeyFieldsList 需與主流程的 KeyShowFieldList 相同
                    //02.子流程與父流程名稱相同(遞迴)
                    //  KeyFieldList 最後一欄需為 _AutoNo 欄位　
                    if (oFlow.FlowID != strFlowStepBtnAddSubFlowID)
                    {
                        //01.子流程與父流程名稱不同
                        string[] diffList = Util.getCompareList(oFlow.FlowKeyFieldList, oSubFlow.FlowKeyFieldList, Util.ListCompareMode.Diff);
                        if (diffList.Count() == 1 && diffList[0].ToUpper() == "_AUTONO")
                        {
                            if (Util.getCompareList(oFlow.FlowShowFieldList, oSubFlow.FlowShowFieldList, Util.ListCompareMode.Diff).Count() == 0)
                            {
                                IsNeedAddSubFlow = true;
                                if (strFlowStepBtnIsAddMultiSubFlow == "Y")
                                {
                                    Dictionary<string, string> oTmpAss = new Dictionary<string, string>();
                                    //每個指派對象都產生獨立子流程(只要其中一個子流程新增失敗，就自動中斷)
                                    IsAddSubFlowSucceed = true;
                                    foreach (var pair in oAssDic)
                                    {
                                        oTmpAss.Clear();
                                        oTmpAss.Add(pair.Key, pair.Value);
                                        if (IsAddSubFlowSucceed)
                                        {
                                            if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinionForSQL, oFlow.FlowID, oFlow.FlowLogID))
                                            {
                                                IsAddSubFlowSucceed = true;
                                                labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                            }
                                            else
                                            {
                                                IsAddSubFlowSucceed = false;
                                                labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //所有指派對象只產生一個子流程
                                    if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinionForSQL, oFlow.FlowID, oFlow.FlowLogID))
                                    {
                                        IsAddSubFlowSucceed = true;
                                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                    }
                                    else
                                    {
                                        IsAddSubFlowSucceed = false;
                                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //02.子流程與父流程名稱相同(遞迴)
                        if (oFlow.FlowKeyFieldList[oFlow.FlowKeyFieldList.Count() - 1].ToUpper() == "_AUTONO")
                        {
                            IsNeedAddSubFlow = true;
                            //組合子流程的 KeyValueList
                            string[] subKeyValueList = new string[oFlow.FlowKeyFieldList.Count() - 1];
                            for (int i = 0; i < subKeyValueList.Count(); i++)
                            {
                                subKeyValueList[i] = oFlow.FlowKeyValueList[i];
                            }

                            if (strFlowStepBtnIsAddMultiSubFlow == "Y")
                            {
                                Dictionary<string, string> oTmpAss = new Dictionary<string, string>();
                                //每個指派對象都產生獨立子流程(只要其中一個子流程新增失敗，就自動中斷)
                                IsAddSubFlowSucceed = true;
                                foreach (var pair in oAssDic)
                                {
                                    oTmpAss.Clear();
                                    oTmpAss.Add(pair.Key, pair.Value);
                                    if (IsAddSubFlowSucceed)
                                    {
                                        if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinionForSQL, oFlow.FlowID, oFlow.FlowLogID))
                                        {
                                            IsAddSubFlowSucceed = true;
                                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                        }
                                        else
                                        {
                                            IsAddSubFlowSucceed = false;
                                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //所有指派對象只產生一個子流程
                                if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinionForSQL, oFlow.FlowID, oFlow.FlowLogID))
                                {
                                    IsAddSubFlowSucceed = true;
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowSucceed);
                                }
                                else
                                {
                                    IsAddSubFlowSucceed = false;
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_FlowVerifyAddSubFlowError);
                                }
                            }
                        }
                    }
                }

                //處理[主流程審核]
                if (IsNeedAddSubFlow)
                {
                    //需先[新增子流程]
                    if (IsAddSubFlowSucceed)
                    {
                        //若[新增子流程]成功
                        //當主流程目前關卡與[FlowStepBtnAssignToList]不同，才執行其[一般審核]  2015.08.26 優化
                        if (oFlow.FlowCurrLogStepID != dr["FlowStepBtnNextStepID"].ToString())
                        {
                            oAssDic.Clear();
                            oAssDic = Util.getDictionary(dr["FlowStepBtnAssignToList"].ToString());
                            if (oAssDic != null && oAssDic.Count > 0)
                            {
                                if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinionForSQL))
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                                else
                                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                            }
                        }
                    }
                }
                else
                {
                    //一般審核
                    if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinionForSQL))
                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                    else
                        labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowCaseHtmlInfo)); //2017.05.11 改顯示 FlowCaseHtmlInfo
                }

                //=== END from FlowVerify
            }
        }

    }
}