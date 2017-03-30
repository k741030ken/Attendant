using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程全頁審核
/// </summary>
public partial class FlowExpress_FlowVerify : SecurePage
{
    /// <summary>
    /// 指派清單是否同時顯示鍵值的鍵值最大長度
    /// </summary>
    public int _ChkMaxKeyLen
    {
        //2016.07.12 新增
        get
        {
            if (ViewState["_ChkMaxKeyLen"] == null)
            {
                if (ViewState["_ChkMaxKeyLen"] == null)
                {
                    ViewState["_ChkMaxKeyLen"] = int.Parse(Util.getRequestQueryStringKey("ChkMaxKeyLen", "0"));
                }
            }
            return (int)(ViewState["_ChkMaxKeyLen"]);
        }
    }

    /// <summary>
    /// 是否使用 CheckBoxList 顯示複選指派對象(預設 false)
    /// </summary>
    public bool _IsCheckBoxListEnabled
    {
        //2016.09.20 新增
        get
        {
            if (ViewState["_IsCheckBoxListEnabled"] == null)
            {
                ViewState["_IsCheckBoxListEnabled"] = (Util.getRequestQueryStringKey("IsShowCheckBoxList", "N", true) == "Y") ? true : false;
            }
            return (bool)(ViewState["_IsCheckBoxListEnabled"]);
        }
        set
        {
            ViewState["_IsCheckBoxListEnabled"] = value;
        }
    }

    public Dictionary<string, string> _dicBtnContext
    {
        get
        {
            return (Dictionary<string, string>)(ViewState["_dicBtnContext"]);
        }
        set
        {
            ViewState["_dicBtnContext"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            VerifyInit();
        }
    }

    protected void VerifyInit()
    {
        Dictionary<string, string> dicPara = Util.getDictionary(Request.QueryString);
        if (dicPara.IsNullOrEmpty())
        {             //缺少流程參數
            Util.setJSContent("alert('" + WorkRS.Resources.FlowFullLogMsg_FlowParaError + "');");
            return;
        }

        UserInfo oUser = UserInfo.getUserInfo();
        FlowExpress oFlow = new FlowExpress();
        if (oFlow.FlowCurrLogIsClose)
        {
            return;
        }

        DataTable dtBtn = Util.getDataTable(FlowExpress.getFlowOpenLogVerifyInfo(oFlow).Rows[0]["FlowStepBtnInfoJSON"].ToString());
        if (dtBtn != null && dtBtn.Select(string.Format("FlowStepBtnID = '{0}' ", dicPara["FlowStepBtnID"])).Count() > 0)
        {
            DataRow drBtn = dtBtn.Select(string.Format("FlowStepBtnID = '{0}' ", dicPara["FlowStepBtnID"]))[0];
            Dictionary<string, string> dicBtnAssignToList = new Dictionary<string, string>();
            Dictionary<string, string> dicBtnContext = new Dictionary<string, string>();

            dicBtnContext.Clear();
            dicBtnContext.Add("FlowID", oFlow.FlowID);
            dicBtnContext.Add("FlowLogID", oFlow.FlowLogID);
            dicBtnContext.Add("VerifyStepInfo", string.Format(" [{0}-{1}] ", oFlow.FlowCurrStepID, oFlow.FlowCurrStepName));
            dicBtnContext.Add("FlowStepBtnID", drBtn["FlowStepBtnID"].ToString());
            dicBtnContext.Add("FlowStepBtnIsAddMultiSubFlow", drBtn["FlowStepBtnIsAddMultiSubFlow"].ToString());
            dicBtnContext.Add("FlowStepBtnAddSubFlowID", drBtn["FlowStepBtnAddSubFlowID"].ToString());
            dicBtnContext.Add("FlowStepBtnAddSubFlowStepBtnID", drBtn["FlowStepBtnAddSubFlowStepBtnID"].ToString());
            if (!string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowID"].ToString()) && !string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString()))
            {
                //若會新增子流程
                if (oFlow.FlowCurrLogStepID != drBtn["FlowStepBtnNextStepID"].ToString())
                {
                    //若主流程下一關不為同關卡代號，才傳遞主流程的原始指派清單備用 2015.08.26 優化
                    dicBtnContext.Add("FlowStepBtnAssignToList", drBtn["FlowStepBtnAssignToList"].ToString());
                }
                else
                {
                    dicBtnContext.Add("FlowStepBtnAssignToList", "");
                }
            }
            _dicBtnContext = dicBtnContext;

            string strStartVerifyJS = "";
            strStartVerifyJS += "var isStartVerify = true;";
            if (!string.IsNullOrEmpty(drBtn["FlowStepBtnConfirmMsg"].ToString().Trim()))
            {
                strStartVerifyJS += "if (!confirm('" + drBtn["FlowStepBtnConfirmMsg"].ToString().Trim() + "')) {isStartVerify = false; return false;}";
            }
            strStartVerifyJS += "if (isStartVerify) {";
            strStartVerifyJS += "   this.style.display = 'none';";
            strStartVerifyJS += ucLightBox.ucShowClientJS;
            strStartVerifyJS += "   var oClose = window.parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
            strStartVerifyJS += "   if (oClose != null){oClose.style.display='none';}";
            strStartVerifyJS += "   var oComplete = window.parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
            strStartVerifyJS += "   if (oComplete != null){oComplete.style.display='none';}";
            strStartVerifyJS += "}";
            btnStartVerify.OnClientClick = strStartVerifyJS;
            btnStartVerify.Text = WorkRS.Resources.FlowVerifyTab_btnStartVerify;

            if (string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowID"].ToString()) && string.IsNullOrEmpty(drBtn["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString()))
            {
                //一般指派處理
                dicBtnAssignToList = Util.getDictionary(drBtn["FlowStepBtnAssignToList"].ToString());
                //按鈕提示訊息
                if (!string.IsNullOrEmpty(drBtn["FlowStepBtnDesc"].ToString()))
                {
                    labVerify.Text = "※ " + drBtn["FlowStepBtnDesc"].ToString();
                }
                else
                {
                    labVerify.Text = string.Format(WorkRS.Resources.FlowVerifyTab_labVerifyToolTip1, drBtn["FlowStepBtnNextStepID"].ToString() + "-" + drBtn["FlowStepBtnNextStepName"].ToString());
                }
            }
            else
            {
                //會新增子流程的指派處理
                dicBtnAssignToList = Util.getDictionary(drBtn["FlowStepBtnAddSubFlowStepBtnAssignToList"].ToString());
                //按鈕提示訊息
                if (!string.IsNullOrEmpty(drBtn["FlowStepBtnDesc"].ToString()))
                {
                    labVerify.Text = "※ " + drBtn["FlowStepBtnDesc"].ToString();
                }
                else
                {
                    FlowExpress oSubFlow = new FlowExpress(drBtn["FlowStepBtnAddSubFlowID"].ToString(), null, false, false);
                    labVerify.Text = string.Format(WorkRS.Resources.FlowVerifyTab_labVerifyAddSubToolTip1, drBtn["FlowStepBtnNextStepID"].ToString() + "-" + drBtn["FlowStepBtnNextStepName"].ToString(), oSubFlow.FlowName);
                }
            }
            
            if (dicBtnAssignToList.Count > 0)
            {
                //當指派清單只有一筆資料
                if (dicBtnAssignToList.Count == 1)
                {
                    if (dicBtnAssignToList.First().Key == "*")
                    {
                        //若為任意指派
                        if (anyVerify != null)
                        {
                            switch (dicBtnAssignToList.First().Value.ToUpper())
                            {
                                case "COMP":
                                    //同公司對象
                                    if (oUser != null)
                                    {
                                        anyVerify.ucIsSelectCommUserYN = "N";
                                        anyVerify.ucValidCompIDList = oUser.CompID;
                                    }
                                    break;
                                case "PART":
                                    //同公司(含兼職)對象
                                    if (oUser != null)
                                    {
                                        anyVerify.ucIsSelectCommUserYN = "N";
                                        string[] oPartList = Util.getArray(oUser.PartInfoTable, "CompID");
                                        if (oPartList != null)
                                        {
                                            anyVerify.ucValidCompIDList = Util.getStringJoin(Util.getCompareList(oUser.CompID.Split(','), oPartList, Util.ListCompareMode.Merge));
                                        }
                                        else
                                        {
                                            anyVerify.ucValidCompIDList = oUser.CompID;
                                        }
                                    }
                                    break;
                                default:
                                    // [* / ANY] 任意指派 2014.10.20 新增
                                    break;
                            }

                            if (drBtn["FlowStepBtnIsMultiSelect"].ToString().ToUpper() != "N")
                            {
                                //若不為[N]單選
                                anyVerify.ucIsMultiSelectYN = "Y";
                            }

                            if (oUser != null)
                            {
                                //預設為自己部門 2016.06.06
                                anyVerify.ucDefCompID = oUser.CompID;
                                anyVerify.ucDefDeptID = oUser.DeptID;
                            }
                            anyVerify.ucWidth = 350;
                            anyVerify.Visible = true;
                            anyVerify.Refresh();
                        }
                    }
                    else
                    {
                        //若為單一對象
                        if (allVerify != null)
                        {
                            //檢查可顯示鍵值的鍵值最大長度
                            if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                allVerify.DataSource = dicBtnAssignToList;
                            else
                                allVerify.DataSource = Util.getDictionary(dicBtnAssignToList);

                            allVerify.DataTextField = "value";
                            allVerify.DataValueField = "key";
                            allVerify.CssClass = "Util_clsDropDownListReadOnly";
                            allVerify.DataBind();
                            allVerify.Width = 400;
                            allVerify.Height = 22;
                            allVerify.Rows = 2; //避免出現捲軸
                            allVerify.Visible = true;
                        }
                    }
                }
                else
                {
                    //若指派清單有多筆資料
                    switch (drBtn["FlowStepBtnIsMultiSelect"].ToString().ToUpper())
                    {
                        case "A":
                            //全選 2014.11.05
                            if (allVerify != null && labAllVerify != null)
                            {
                                //檢查可顯示鍵值的鍵值最大長度
                                if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                    allVerify.DataSource = dicBtnAssignToList;
                                else
                                    allVerify.DataSource = Util.getDictionary(dicBtnAssignToList);

                                allVerify.DataTextField = "value";
                                allVerify.DataValueField = "key";
                                allVerify.CssClass = "Util_clsDropDownListReadOnly"; //2017.02.02
                                allVerify.DataBind();
                                allVerify.Width = 400;
                                allVerify.Rows = 10;

                                labAllVerify.ForeColor = System.Drawing.Color.DarkRed;
                                labAllVerify.Text = WorkRS.Resources.FlowVerifyTab_labAllAssigned;

                                allVerify.Visible = true;
                                labAllVerify.Visible = true;
                            }
                            break;
                        case "Y":
                            //複選
                            //2016.09.20 改成支援 ucCommMultiSelect 或是 ucCheckBoxList 實作
                            if (_IsCheckBoxListEnabled)
                            {
                                //使用 ucCheckBoxList
                                if (chkVerify != null)
                                {
                                    if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                        chkVerify.ucSourceDictionary = dicBtnAssignToList;
                                    else
                                        chkVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                    chkVerify.ucWidth = 400;
                                    chkVerify.ucRows = 10;
                                    chkVerify.ucChkBoxListWidth = 410;
                                    chkVerify.ucChkBoxListOffsetX = -8;
                                    chkVerify.ucChkBoxListHeight = 170;
                                    chkVerify.ucChkBoxListOffsetY = (Util.getIEVersion() > 0) ? -145 : -15;
                                    chkVerify.ucRangeMaxQty = dicBtnAssignToList.Count;
                                    chkVerify.ucIsAutoPopWhenNoSelection = true;  //若無選擇結果，就自動彈出候選清單 2016.09.01
                                    chkVerify.Visible = true;
                                    chkVerify.Refresh();
                                }
                            }
                            else
                            {
                                //使用 ucCommMultiSelect
                                if (muiVerify != null)
                                {
                                    if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                        muiVerify.ucSourceDictionary = dicBtnAssignToList;
                                    else
                                        muiVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                    muiVerify.ucBoxListHeight = 100;
                                    muiVerify.ucBoxListWidth = 200;
                                    muiVerify.Visible = true;
                                    muiVerify.Refresh();
                                }
                            }
                            break;
                        case "N":
                            //單選
                            if (oneVerify != null)
                            {
                                if (_ChkMaxKeyLen > 0 && dicBtnAssignToList.Where(p => p.Key.Length > _ChkMaxKeyLen).Count() > 0)
                                    oneVerify.ucSourceDictionary = dicBtnAssignToList;
                                else
                                    oneVerify.ucSourceDictionary = Util.getDictionary(dicBtnAssignToList);

                                oneVerify.ucDropDownSourceListWidth = 250;
                                oneVerify.Visible = true;
                                oneVerify.Refresh();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            //顯示審核條件畫面
            ucLightBox.ucLightBoxMsg = WorkRS.Resources.FlowVerifyMsg_FlowVerifyProcessing;
            labVerifyOpinion.Text = WorkRS.Resources.FlowVerifyTab_FlowOpinion;
            labVerifyStepInfo.Text = string.Format(" [{0}-{1}] ", oFlow.FlowCurrStepID, oFlow.FlowCurrStepName);
            txtVerifyOpinion.ucRows = 3;
            txtVerifyOpinion.ucWidth = 600;
            txtVerifyOpinion.ucTextData = dicPara["FlowOpinion"];
        }

    }

    /// <summary>
    /// 按下開始審核
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnStartVerify_Click(object sender, EventArgs e)
    {
        //取得 UI 指派對象
        Dictionary<string, string> oAssDic = new Dictionary<string, string>();
        //單選指派
        if (oneVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(oneVerify.ucSelectedDictionary, false));
        }
        //複選指派
        if (muiVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(muiVerify.ucSelectedDictionary, false));
        }
        if (chkVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(chkVerify.ucSelectedDictionary, false));
        }
        //任意指派
        if (anyVerify.Visible && !string.IsNullOrEmpty(anyVerify.ucSelectedUserIDList))
        {
            for (int i = 0; i < anyVerify.ucSelectedUserIDList.Split(',').Count(); i++)
            {
                oAssDic.Add(anyVerify.ucSelectedUserIDList.Split(',')[i], UserInfo.findUserName(anyVerify.ucSelectedUserIDList.Split(',')[i]));
            }
        }
        //全部指派
        if (allVerify.Visible)
        {
            oAssDic.AddRange(Util.getDictionary(Util.getDictionary(allVerify.GetAllItems()), false));
        }

        //開始判斷
        DivVerifyBtnArea.Visible = false;
        DivVerifyMsgArea.Visible = true;
        labFlowVerifyMsg.Text = "";

        if (oAssDic.Count <= 0)
        {
            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, WorkRS.Resources.FlowVerifyMsg_AssignToNotFound);  //缺少指派對象
        }
        else
        {
            //指派對象有值，可進行審核
            Dictionary<string, string> dicPara = Util.getDictionary(Request.QueryString);
            if (dicPara.IsNullOrEmpty())
            {             //缺少流程參數
                Util.setJSContent("alert('" + WorkRS.Resources.FlowFullLogMsg_FlowParaError + "');");
                return;
            }

            Dictionary<string, string> dicBtnContext = _dicBtnContext;  //取出btn參數
            FlowExpress oFlow = new FlowExpress(_dicBtnContext["FlowID"], _dicBtnContext["FlowLogID"], true, false);

            string strFlowStepBtnID = dicBtnContext["FlowStepBtnID"].ToString().Trim();
            string strFlowStepBtnIsAddMultiSubFlow = dicBtnContext["FlowStepBtnIsAddMultiSubFlow"].ToString().Trim().ToUpper();
            string strFlowStepBtnAddSubFlowID = dicBtnContext["FlowStepBtnAddSubFlowID"].ToString().Trim();
            string strFlowStepBtnAddSubFlowStepBtnID = dicBtnContext["FlowStepBtnAddSubFlowStepBtnID"].ToString().Trim();
            string strFlowStepOpinion = txtVerifyOpinion.ucTextData;

            bool IsNeedAddSubFlow = false;
            bool IsAddSubFlowSucceed = false;

            //檢查是否有自訂審核URL
            if (!string.IsNullOrEmpty(oFlow.FlowCurrStepCustVerifyURL))
            {
                if (System.IO.File.Exists(Server.MapPath(oFlow.FlowCurrStepCustVerifyURL)))
                {
                    //==若有自訂審核URL==
                    //設定Session傳遞參數[FlowVerifyInfo]

                    string strJS = "";
                    strJS += "var oClose = window.parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnClose');";
                    strJS += "if (oClose != null){oClose.style.display='';}";
                    strJS += "var oComplete = window.parent.document.getElementById('ucFlowTodoList1_ucModalPopup1_btnComplete');";
                    strJS += "if (oComplete != null){oComplete.style.display='';}";

                    dicBtnContext.TryAdd("FlowStepAssignToList", Util.getJSON(oAssDic));
                    dicBtnContext.TryAdd("FlowStepOpinion", strFlowStepOpinion);
                    dicBtnContext.TryAdd("FlowVerifyJS", strJS);
                    Session["FlowVerifyInfo"] = dicBtnContext;

                    //因為 Ajax Page 無法使用Server.Transfer，故 CustVerifyURL 改用 Response.Redirect()執行
                    //設定 CustVerifyURL
                    string strVerifyURL = string.Format("{0}?FlowID={1}&FlowLogID={2}", oFlow.FlowCurrStepCustVerifyURL, oFlow.FlowID, oFlow.FlowLogID);
                    for (int i = 0; i < oFlow.FlowKeyFieldList.Count(); i++)
                    {
                        if (oFlow.FlowKeyFieldList[i].ToUpper() != "AUTONO")
                        {
                            strVerifyURL += string.Format("&{0}={1}", oFlow.FlowKeyFieldList[i], oFlow.FlowKeyValueList[i]);
                        }
                    }
                    Response.Redirect(strVerifyURL);
                }
                else
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_NotExist1, oFlow.FlowCurrStepCustVerifyURL));
                }
                return;
            }

            //==無自訂審核URL==
            //處理[新增子流程]
            if (!string.IsNullOrEmpty(strFlowStepBtnAddSubFlowID) && !string.IsNullOrEmpty(strFlowStepBtnAddSubFlowStepBtnID))
            {
                //檢查是否自動新增子流程
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
                                        if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
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
                                if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, oFlow.FlowKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
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
                                    if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oTmpAss, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
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
                            if (FlowExpress.IsFlowInsVerify(oSubFlow.FlowID, subKeyValueList, oFlow.FlowShowValueList, strFlowStepBtnAddSubFlowStepBtnID, oAssDic, strFlowStepOpinion, oFlow.FlowID, oFlow.FlowLogID))
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
                if (IsAddSubFlowSucceed)
                {
                    //若[新增子流程]成功
                    //當[FlowStepBtnAssignToList]有值，才執行主流程的[一般審核]  2015.08.26 優化
                    oAssDic.Clear();
                    oAssDic = Util.getDictionary(dicBtnContext["FlowStepBtnAssignToList"].ToString());
                    if (oAssDic != null && oAssDic.Count > 0)
                    {
                        if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinion))
                        {
                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowID + "-" + oFlow.FlowLogID));
                        }
                        else
                        {
                            labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowID + "-" + oFlow.FlowLogID));
                            //2014.11.27 顯示審核錯誤原因
                            DataTable dtError = FlowExpressTraceLog.getFlowExpressErrorLogData(oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                            if (dtError != null && dtError.Rows.Count > 0)
                            {
                                labFlowVerifyMsg.Text += "<ul>";
                                for (int i = 0; i < dtError.Rows.Count; i++)
                                {
                                    labFlowVerifyMsg.Text += string.Format("<li style='color:gray;'>{0}</li>", dtError.Rows[i]["LogDesc"].ToString().Trim());
                                }
                                labFlowVerifyMsg.Text += "</ul>";
                            }
                        }
                    }
                }
            }
            else
            {
                //一般審核
                if (FlowExpress.IsFlowVerify(oFlow.FlowID, oFlow.FlowLogID, strFlowStepBtnID, oAssDic, strFlowStepOpinion))
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifySucceed1, oFlow.FlowID + "-" + oFlow.FlowLogID));
                }
                else
                {
                    labFlowVerifyMsg.Text += Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowVerifyMsg_FlowVerifyError1, oFlow.FlowID + "-" + oFlow.FlowLogID));
                    //2014.11.27 顯示審核錯誤原因
                    DataTable dtError = FlowExpressTraceLog.getFlowExpressErrorLogData(oFlow.FlowID, oFlow.FlowCaseID, oFlow.FlowLogID);
                    if (dtError != null && dtError.Rows.Count > 0)
                    {
                        labFlowVerifyMsg.Text += "<ul>";
                        for (int i = 0; i < dtError.Rows.Count; i++)
                        {
                            labFlowVerifyMsg.Text += string.Format("<li style='color:gray;'>{0}</li>", dtError.Rows[i]["LogDesc"].ToString().Trim());
                        }
                        labFlowVerifyMsg.Text += "</ul>";
                    }
                }
            }
        }
    }

}