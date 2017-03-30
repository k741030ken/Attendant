using System;
using System.Data;
using System.IO;
using System.Linq;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程完整審核記錄控制項
/// </summary>
public partial class FlowExpress_ucFlowFullLogList : BaseUserControl
{
    private bool _IsShowCaseHeader = false;
    private bool _IsAutoFromRootCase = true;
    private bool _IsDisplaySubFlow = true;
    private bool _IsShowWaitingMsg = true;
    private bool _IsShowClosedReason = true; //2016.08.24 新增

    /// <summary>
    /// 指定要顯示的FlowID
    /// </summary>
    public string ucFlowID { get; set; }

    /// <summary>
    /// 指定要顯示的FlowCaseID
    /// <para>與 FlowLogID 二擇一</para>
    /// </summary>
    public string ucFlowCaseID { get; set; }

    /// <summary>
    /// 指定要顯示的FlowLogID (優先使用)
    /// <para>與 FlowCaseID 二擇一</para>
    /// </summary>
    public string ucFlowLogID { get; set; } //2016.10.14 新增

    /// <summary>
    /// 是否自動重新整理(預設 true)
    /// </summary>
    public bool ucIsAutoRefresh
    {
        //2017.02.07 新增
        get
        {
            if (ViewState["_IsAutoRefresh"] == null)
            {
                ViewState["_IsAutoRefresh"] = false; //debug
            }
            return (bool)(ViewState["_IsAutoRefresh"]);
        }
        set
        {
            ViewState["_IsAutoRefresh"] = value;
        }
    }

    /// <summary>
    /// 是否自動從根(Root)案件開始顯示(預設 true)
    /// </summary>
    public bool ucIsAutoFromRootCase
    {
        get { return _IsAutoFromRootCase; }
        set { _IsAutoFromRootCase = value; }
    }

    /// <summary>
    /// 是否顯示相關子流程(預設 true)
    /// </summary>
    public bool ucIsDisplaySubFlow
    {
        get { return _IsDisplaySubFlow; }
        set { _IsDisplaySubFlow = value; }
    }

    /// <summary>
    /// 是否顯示案件表頭(預設 false)
    /// </summary>
    public bool ucIsShowCaseHeader
    {
        get { return _IsShowCaseHeader; }
        set { _IsShowCaseHeader = value; }
    }

    /// <summary>
    /// 是否顯示 [載入中...] 訊息(預設 true)
    /// </summary>
    public bool ucIsShowWaitingMsg
    {
        get { return _IsShowWaitingMsg; }
        set { _IsShowWaitingMsg = value; }
    }

    /// <summary>
    /// 已結案流程是否顯示結案緣由(預設 true)
    /// </summary>
    public bool ucIsShowClosedReason
    {
        get { return _IsShowClosedReason; }
        set { _IsShowClosedReason = value; }
    }

    /// <summary>
    /// 附件下載是否為彈出新視窗模式(預設 true)
    /// </summary>
    public bool ucIsPopNewWindow
    {
        //2017.03.01 新增
        get
        {
            if (ViewState["_IsPopNewWindow"] == null)
            {
                ViewState["_IsPopNewWindow"] = true;
            }
            return (bool)(ViewState["_IsPopNewWindow"]);
        }
        set
        {
            ViewState["_IsPopNewWindow"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible) return;
        
        if (ucIsAutoRefresh)  //2017.02.07 加入 ucIsAutoRefresh
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(ucFlowID) && (!string.IsNullOrEmpty(ucFlowCaseID) || !string.IsNullOrEmpty(ucFlowLogID)))
                {
                    if (ucIsShowWaitingMsg)
                    {
                        //顯示「載入中」訊息
                        labIsSelfRefresh.Text = "Y";
                        labFullLogList.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Waiting, string.Format(RS.Resources.Msg_Waiting1, WorkRS.Resources.FlowVerifyTab_FlowFullLog), 300);
                    }
                    else
                    {
                        labIsSelfRefresh.Text = "N";
                        Refresh();
                    }
                }
                else
                {
                    labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(WorkRS.Resources.FlowFullLogMsg_FlowParaError1, "FlowID - FlowCaseID"));
                    labErrMsg.Visible = true;
                    divFlowFullLogList.Visible = false;
                }
            }
            else
            {
                if (labIsSelfRefresh.Text.ToUpper() == "Y")
                {
                    labIsSelfRefresh.Text = "N";
                    Refresh();
                }
            }
        }
    }

    public void Refresh()
    {
        FlowExpress oFlow;
        //進行初始(優先使用 ucFlowLogID) 2016.10.14
        if (string.IsNullOrEmpty(ucFlowLogID))
            oFlow = new FlowExpress(ucFlowID, ucFlowCaseID, false, false);
        else
            oFlow = new FlowExpress(ucFlowID, ucFlowLogID, true, false);

        labFullLogList.Text = "";
        StringWriter sw = new StringWriter();
        if (string.IsNullOrEmpty(oFlow.FlowID) || string.IsNullOrEmpty(oFlow.FlowCaseID))
        {
            return;
        }

        if (ucIsAutoFromRootCase)
        {
            //取得本案的 RootCase
            string[] RootCaseInfo = FlowExpress.getRootFlowCaseInfo(oFlow.FlowID, oFlow.FlowCaseID);
            sw = getFlowFullLogList(RootCaseInfo[0], RootCaseInfo[1], ucIsDisplaySubFlow);
            if (oFlow.FlowID != RootCaseInfo[0] || oFlow.FlowCaseID != RootCaseInfo[1])
            {
                oFlow = new FlowExpress(RootCaseInfo[0], RootCaseInfo[1], false, false);
            }
        }
        else
        {
            sw = getFlowFullLogList(oFlow.FlowID, oFlow.FlowCaseID, ucIsDisplaySubFlow);
        }

        if (string.IsNullOrEmpty(sw.ToString()))
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound);
            labErrMsg.Visible = true;
            divFlowFullLogList.Visible = false;
        }
        else
        {
            labErrMsg.Visible = false;
            divFlowFullLogList.Visible = true;

            if (ucIsShowCaseHeader)
            {
                labFullLogList.Text += getCaseHeader(oFlow).ToString();
            }
            labFullLogList.Text += sw.ToString();
        }

    }

    public StringWriter getCaseHeader(FlowExpress oCaseFlow)
    {
        StringWriter swResult = new StringWriter();
        string strHeader = "";
        string strHeaderBody = "";

        //預防要顯示的變數需從Log取出 2016.10.14 
        FlowExpress oFlow = oCaseFlow;
        if (string.IsNullOrEmpty(oCaseFlow.FlowLogID))
            oFlow = new FlowExpress(oCaseFlow.FlowID, oCaseFlow.FlowCurrLastLogID, true, false);

        for (int i = 0; i < oFlow.FlowShowCaptionList.Count(); i++)
        {
            string strFldlName = oFlow.FlowShowFieldList[i];
            string strFldValue = "";
            //處理顯示欄位
            //若為流程內建變數，則自動代換 2016.10.14
            switch (strFldlName.ToUpper())
            {
                case "_ASSIGNTO":
                    strFldValue = oFlow.FlowCurrLogAssignTo;
                    break;
                case "_FLOWID":
                    strFldValue = oFlow.FlowID;
                    break;
                case "_FLOWCASEID":
                    strFldValue = oFlow.FlowCaseID;
                    break;
                case "_FLOWLOGID":
                    strFldValue = oFlow.FlowLogID;
                    break;
                case "_FLOWKEY":
                    strFldValue = string.Format("{0}-{1}", oFlow.FlowID, oFlow.FlowLogID);
                    break;
                case "_LOGDATE":
                    strFldValue = string.Format("{0:yyyy\\/MM\\/dd}", oFlow.FlowCurrLogCrDateTime);
                    break;
                case "_LOGDATETIME":
                    strFldValue = string.Format("{0:yyyy\\/MM\\/dd HH:mm}", oFlow.FlowCurrLogCrDateTime);
                    break;
                case "_CURRSTEPID":
                    strFldValue = oFlow.FlowCurrLogStepID;
                    break;
                case "_CURRSTEPNAME":
                    strFldValue = oFlow.FlowCurrLogStepName;
                    break;
                case "_PROXYTYPE":
                    strFldValue = "N/A";
                    break;
                default:
                    // 從 FlowShowFieldList 反推
                    strFldValue = "N/A";
                    for (int j = 0; j < oFlow.FlowShowFieldList.Count(); j++)
                    {
                        if (oFlow.FlowShowFieldList[j] == strFldlName)
                        {
                            strFldValue = oFlow.FlowShowValueList[j];
                            if (strFldlName.Contains("@D"))
                            {
                                strFldValue = string.Format("{0:yyyy\\/MM\\/dd}", DateTime.ParseExact(strFldValue, "yyyyMMdd", null));
                            }
                            if (strFldlName.Contains("@N"))
                            {
                                string strFormat = strFldlName.Split('@')[1];
                                strFormat = "{0:" + ((strFormat.Length > 2) ? strFormat.Left(2) : strFormat) + "}";
                                strFldValue = string.Format(strFormat, decimal.Parse(strFldValue));
                            }
                            break;
                        }
                    }
                    break;
            }
            strHeader += string.Format("<td>{0}</td>", oFlow.FlowShowCaptionList[i].Split('@')[0]);  //ex: 主旨@L300,提案單位,提案日期,急件,預定結案
            strHeaderBody += string.Format("<td>{0}</td>", strFldValue);
        }
        swResult.WriteLine("<table class='clsCaseTable' cellspacing='1' cellpadding='5' width='100%' >");
        swResult.WriteLine(string.Format("<tr class='clsCaseHeader' >{0}</tr>", strHeader));
        swResult.WriteLine(string.Format("<tr class='clsCaseHeaderBody' >{0}</tr>", strHeaderBody));
        swResult.WriteLine("</table>");
        return swResult;
    }



    /// <summary>
    /// 遞迴顯示流程紀錄
    /// </summary>
    /// <param name="strFlowID"></param>
    /// <param name="strFlowCaseID"></param>
    /// <param name="intLevelindex">流程階層的索引值(從 0 開始)</param>
    /// <param name="bolIsDisplay">是否展開</param>
    /// <returns></returns>
    public StringWriter getFlowFullLogList(string strFlowID, string strFlowCaseID, bool bolIsDisplaySubFlow = true, int intLevelindex = 0)
    {
        //變數初始化
        FlowExpress logFlow = new FlowExpress(strFlowID, strFlowCaseID, false, false); //需將 autodetect 關閉以免誤判

        StringWriter swResult = new StringWriter();
        StringWriter tmpWriter = new StringWriter(); //暫存區，若 FlowCaseStatus=[Close] ，則暫存結案前一關卡資料

        bool IsRowODD = true;
        string strCSSRow1 = string.Format("cls{0}_Row1", intLevelindex + 1);
        string strCSSRow2 = string.Format("cls{0}_Row2", intLevelindex + 1);
        //取 本階流程 及其 下一階流程 資料備用
        string strMainSQL = @"Select FlowID,FlowCaseID,FlowKeyValueList,FlowLogBatNo,FlowLogID,FlowCaseStatus,FlowLogIsClose
							,FlowCurrStepID,FlowCurrStepName,FlowStepID,FlowStepName,FlowStepBtnID,FlowStepBtnCaption
							,AssignTo,AssignToName,IsProxy,AttachID,FlowStepOpinion
							,FromDept,FromDeptName,FromUser,FromUserName
							,ToDept,ToDeptName,ToUser,ToUserName
							,LogCrDateTime,LogUpdDateTime,LogRemark 
							FROM view{0}FlowFullLog  Where FlowCaseID='{1}' 
							Order By FlowID,FlowLogID ";

        DbHelper db = new DbHelper(logFlow.FlowLogDB);
        DataTable dt = db.ExecuteDataSet(CommandType.Text, string.Format(strMainSQL, strFlowID, strFlowCaseID)).Tables[0];
        DataTable dtSub = FlowExpress.getSubFlowData(strFlowID, strFlowCaseID);

        string strFlowCurrStepName = "";
        string strFlowStepName = "";
        string strFlowStepBtnCaption = "";
        //開始顯示 本階流程
        if (dt != null && dt.Rows.Count > 0)
        {
            //因為FullLog儲存時固定使用 Native 語系，故顯示前，需處理與目前語系的代換
            if (Util.getUICultureCode() != FlowExpress._FlowNativeCultureCode)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //處理關卡/按鈕目前語系名稱（若找不到則顯示 Native 語系）
                    strFlowCurrStepName = FlowExpress.getFlowStepName(logFlow.FlowID, dt.Rows[i]["FlowCurrStepID"].ToString());
                    strFlowStepName = FlowExpress.getFlowStepName(logFlow.FlowID, dt.Rows[i]["FlowStepID"].ToString());
                    strFlowStepBtnCaption = FlowExpress.getFlowStepBtnCaption(logFlow.FlowID, dt.Rows[i]["FlowStepID"].ToString(), dt.Rows[i]["FlowStepBtnID"].ToString());
                    if (!string.IsNullOrEmpty(strFlowCurrStepName))
                    {
                        dt.Rows[i]["FlowCurrStepName"] = strFlowCurrStepName;
                    }
                    if (!string.IsNullOrEmpty(strFlowStepName))
                    {
                        dt.Rows[i]["FlowStepName"] = strFlowStepName;
                    }
                    if (!string.IsNullOrEmpty(strFlowStepBtnCaption))
                    {
                        dt.Rows[i]["FlowStepBtnCaption"] = strFlowStepBtnCaption;
                    }

                    //處理SysAgent
                    if (dt.Rows[i]["FlowLogIsClose"].ToString().ToUpper() == "Y")
                    {
                        if (dt.Rows[i]["ToDept"].ToString() == FlowExpress._FlowNativeSysAgent[0] && dt.Rows[i]["ToUser"].ToString() == FlowExpress._FlowNativeSysAgent[2])
                        {
                            dt.Rows[i]["ToDeptName"] = logFlow.FlowSysAgent[1];
                            dt.Rows[i]["ToUserName"] = logFlow.FlowSysAgent[3];
                        }
                    }
                }
            }

            //LogStart
            string strToggleID = string.Format("Flow=[{0}-{1}]．StepID=[{2}]．KeyValueList=[{3}]", strFlowID, strFlowCaseID, dt.Rows[0]["FlowCurrStepID"], dt.Rows[0]["FlowKeyValueList"]);

            //案件 Log 抬頭
            bool bolIsFlowOpen = dt.Rows[0]["FlowCaseStatus"].ToString().ToUpper() == "OPEN" ? true : false;
            int tmpBatNo = 0; //若流程已結案，其前一關卡的批號 2016.08.24
            if (!bolIsFlowOpen)
            {
                tmpBatNo = int.Parse(dt.Rows[dt.Rows.Count - 2]["FlowLogBatNo"].ToString());
            }

            //LogIndicator
            swResult.Write(LogIndicator(strToggleID, logFlow.FlowName, logFlow.FlowCurrStepName, bolIsFlowOpen));
            //LogStart
            swResult.Write(LogStart(strToggleID, true, ((intLevelindex > 0) && (!bolIsDisplaySubFlow)) ? false : true));


            //處理 Log 明細
            //int intStartCnt = intLevelindex <= 0 ? 0 : 1;  //利用 intLevelindex 判斷是否為子流程，若是則其第一筆 Log 不顯示
            int intStartCnt = 0;  // 子流程改為全部顯示
            string strStepName = "";
            for (int dtCnt = intStartCnt; dtCnt < dt.Rows.Count; dtCnt++)
            {
                DataRow dr = dt.Rows[dtCnt];
                //第一筆明細資料，需處理 LogHeader
                if (dtCnt == intStartCnt) swResult.Write(LogHeader(intLevelindex));

                //LogBody
                //<tr>	
                //    <td class='cls2_Row1' >單位副主管</td>
                //    <td class='cls2_Row1' >銀行.法令遵循處-廖順興</td>
                //    <td class='cls2_Row1' >101.07.23-12:06</td>
                //    <td class='cls2_Row1' >呈單位主管</td>
                //    <td class='cls2_Row1' >擬如琪苑意見。</td>
                //</tr>	

                //自訂 FlowLogStatus=OPEN 的Style
                string strRowCustStyle = "";
                if (dr["FlowLogIsClose"].ToString().ToUpper() == "N")
                {
                    strRowCustStyle = "style='color:#FE0000;'";
                }

                swResult.WriteLine("<tr style='vertical-align:top;' >");
                string strCurrStepName = dr["FlowStepName"].ToString().Trim();
                string strCurrStepInfo = string.Format("{0}-{1}\n[{2}-{3}]", dr["FlowStepID"].ToString(), dr["FlowStepName"].ToString(), dr["FlowID"].ToString(), dr["FlowLogID"].ToString());
                int intCurrStepBatNo = int.Parse(dr["FlowLogBatNo"].ToString());

                if (strStepName != strCurrStepName)
                {
                    //當資料屬於同一關卡時，只在第一筆顯示 關卡名稱
                    strStepName = strCurrStepName;
                    swResult.WriteLine(string.Format("<td class='{0}' {1} Title='{3}' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strCurrStepName, strCurrStepInfo));
                }
                else
                {
                    swResult.WriteLine(string.Format("<td class='{0}' {1} Title='{3}' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, "　　　　", strCurrStepInfo));
                }

                if (tmpBatNo > 0)
                {
                    //結案前一關
                    if (tmpBatNo == intCurrStepBatNo)
                    {
                        tmpWriter.Write(LogStart(strToggleID, false, ((intLevelindex > 0) && (!bolIsDisplaySubFlow)) ? true : false));
                        tmpWriter.Write(LogHeader(intLevelindex));
                        tmpWriter.WriteLine(string.Format("<td class='{0}' {1} Title='{3}' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strCurrStepName, strCurrStepInfo));
                    }

                    //結案關卡
                    if (tmpBatNo < intCurrStepBatNo)
                    {
                        tmpWriter.Write(LogFooter(intLevelindex, true));
                        tmpWriter.Write(LogEnd());
                    }
                }
                string strAssName = "";
                string strAssDeptName = "";
                string strAssInfo = "";
                string strAssHint = "";
                string strDateTime = "";
                string strBtnID = "";
                string strBtnCaption = "";
                string strAttachID = "";
                string strOpinoin = "";
                string strLogRemark = "";

                if (dr["FlowLogIsClose"].ToString().ToUpper() != "Y")
                {
                    //FlowLogIsClose != [Y]
                    strAssName = string.IsNullOrEmpty(dr["AssignToName"].ToString().Trim()) ? UserInfo.findUserName(dr["AssignTo"].ToString()) : dr["AssignToName"].ToString().Trim();
                    strAssInfo = strAssName;
                    strAssHint = string.Format("AssignTo=[{0}-{1}]", dr["AssignTo"], strAssName);
                }
                else
                {
                    //FlowLogIsClose = [Y]
                    strAssName = dr["ToUserName"].ToString().Trim();
                    strAssDeptName = dr["ToDeptName"].ToString().Trim();
                    if (string.IsNullOrEmpty(strAssName))
                    {
                        strAssName = UserInfo.findUserName(dr["ToUser"].ToString());
                    }
                    if (string.IsNullOrEmpty(strAssDeptName))
                    {
                        strAssDeptName = UserInfo.findUser(dr["ToUser"].ToString()).DeptName;
                    }
                    strAssInfo = string.Format("{0}-{1}", strAssDeptName, strAssName);
                    if (dr["IsProxy"].ToString().ToUpper() == "Y")
                    {
                        //若為代理人
                        string strOriAssName = dr["AssignToName"].ToString();
                        string strOriAssDeptName = UserInfo.findUser(dr["AssignTo"].ToString()).DeptName;
                        strAssInfo = string.Format("{0}-{1}<br>／{2}{3}", strOriAssDeptName, strOriAssName, strAssName, WorkRS.Resources.FlowLogMsg_IsProxy); //(代)
                    }
                    strAssHint = string.Format("ToUser=[{0}-{1}]", dr["ToUser"], strAssName);
                    strDateTime = string.Format("{0:yyyy\\/MM\\/dd HH:mm}", dr["LogUpdDateTime"]);
                    strBtnID = dr["FlowStepBtnID"].ToString();
                    strBtnCaption = dr["FlowStepBtnCaption"].ToString();
                    strAttachID = dr["AttachID"].ToString().Trim();
                    strLogRemark = dr["LogRemark"].ToString().Trim();

                    strOpinoin = "";
                    //附件 Link
                    if (!string.IsNullOrEmpty(strAttachID))
                    {
                        strOpinoin += string.Format("<span title='{4}' ><a class='{0}' {1} style='text-decoration: none' href=javascript:__doPostBack('ucFlowFullLogList1$btnAttach','{2}');><img src='{3}' width='13' height='13' style='vertical-align:bottom;' border='0'></a></span>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, string.Format("{0},{1}", logFlow.FlowAttachDB, strAttachID), Util.Icon_Attach, WorkRS.Resources.FlowFullLogMsg_AttachToolTip);
                    }
                    //LogRemark
                    if (!string.IsNullOrEmpty(strLogRemark))
                    {

                        strOpinoin += string.Format("<span title='{0}' ><img src='{1}' width='14' height='14' style='vertical-align:bottom;' border='0'></span>", strLogRemark, Util.Icon_Memo);
                    }

                    //FlowHideOpinionStepList
                    bool IsHideOpnion = false;
                    if (logFlow.FlowHideOpinionStepList.Count() > 0)
                    {
                        //若關卡需作隱藏處理
                        if (logFlow.FlowHideOpinionStepList.Contains(dr["FlowStepID"].ToString()))
                        {
                            //先預設需隱藏
                            IsHideOpnion = true;

                            //判斷目前使用者與AssignTo/ToUser是否相同，或為AssignTo同DeptID的就能查閱
                            if (IsHideOpnion && UserInfo.getUserInfo().UserID == logFlow.FlowCurrLogAssignTo)
                            {
                                IsHideOpnion = false;
                            }

                            if (IsHideOpnion && UserInfo.getUserInfo().UserID == dr["ToUser"].ToString().Trim())
                            {
                                IsHideOpnion = false;
                            }

                            if (IsHideOpnion && UserInfo.getUserInfo().DeptID == dr["ToDept"].ToString().Trim())
                            {
                                IsHideOpnion = false;
                            }

                            //若上述判斷結果需隱藏，則再判斷是否有例外清單
                            if (IsHideOpnion && logFlow.FlowHideOpinionIgnoreList.Count() > 0)
                            {
                                //若有例外清單，則目前使用者的 UserID or DeptID在清單內就能查閱
                                if (IsHideOpnion && logFlow.FlowHideOpinionIgnoreList.Contains(UserInfo.getUserInfo().UserID))
                                {
                                    IsHideOpnion = false;
                                }

                                if (IsHideOpnion && logFlow.FlowHideOpinionIgnoreList.Contains(UserInfo.getUserInfo().DeptID))
                                {
                                    IsHideOpnion = false;
                                }
                            }
                        }
                    }
                    //根據 IsHideOpnion 設定顯示方式
                    if (IsHideOpnion)
                    {
                        strOpinoin = WorkRS.Resources.FlowLogMsg_NoPermission; //無權查閱
                    }
                    else
                    {
                        strOpinoin += dr["FlowStepOpinion"].ToString().Replace(Environment.NewLine, "<br/>");
                    }
                }
                swResult.WriteLine(string.Format("<td class='{0}' {1} Title='{3}' >{2}　</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strAssInfo, strAssHint));
                swResult.WriteLine(string.Format("<td class='{0}' {1} style='white-space:nowrap;padding-right:10px;' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strDateTime));
                if (tmpBatNo == intCurrStepBatNo)
                {
                    tmpWriter.WriteLine(string.Format("<td class='{0}' {1} Title='{3}' >{2}　</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strAssInfo, strAssHint));
                    tmpWriter.WriteLine(string.Format("<td class='{0}' {1} style='white-space:nowrap;padding-right:10px;' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strDateTime));
                }

                if (string.IsNullOrEmpty(strBtnID.Trim()))
                {
                    swResult.WriteLine(string.Format("<td class='{0}' {1} >　</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle));
                    if (tmpBatNo == intCurrStepBatNo)
                    {
                        tmpWriter.WriteLine(string.Format("<td class='{0}' {1} >　</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle));
                    }
                }
                else
                {
                    swResult.WriteLine(string.Format("<td class='{0}' {1} Title='StepBtnID=[{3}]' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strBtnCaption, strBtnID));
                    if (tmpBatNo == intCurrStepBatNo)
                    {
                        tmpWriter.WriteLine(string.Format("<td class='{0}' {1} Title='StepBtnID=[{3}]' >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strBtnCaption, strBtnID));
                    }
                }

                swResult.WriteLine(string.Format("<td class='{0}' {1} >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strOpinoin));
                swResult.WriteLine("</tr>");
                if (tmpBatNo == intCurrStepBatNo)
                {
                    tmpWriter.WriteLine(string.Format("<td class='{0}' {1} >{2}</td>", IsRowODD == true ? strCSSRow1 : strCSSRow2, strRowCustStyle, strOpinoin));
                    tmpWriter.WriteLine("</tr>");
                }

                //單雙列使用不同 CSS
                IsRowODD = IsRowODD == true ? false : true;

                //檢查是否需處理 子流程
                bool IsNeedCheckSubFlow = false;
                if (dtCnt + 1 == dt.Rows.Count)
                {
                    //當為資料末筆 
                    IsNeedCheckSubFlow = true;
                }
                else
                {
                    //當本筆資料是本批號最後一筆
                    if (dt.Rows[dtCnt]["FlowLogBatNo"].ToString() != dt.Rows[dtCnt + 1]["FlowLogBatNo"].ToString()) IsNeedCheckSubFlow = true;
                }



                //IsNeedCheckSubFlow = false;
                //若需處理　子流程
                if (IsNeedCheckSubFlow && dtSub != null)
                {
                    //過濾出需處理的子流程(們)
                    DataRow[] drSubList = dtSub.Select(string.Format(" ParentFlowLogBatNo = '{0}' ", int.Parse(dt.Rows[dtCnt]["FlowLogBatNo"].ToString())));
                    if (drSubList.Count() > 0)
                    {
                        //處理子流程前，先將本階流程收尾，處理　LogFooter
                        swResult.Write(LogFooter(intLevelindex));
                        //使用遞迴處理子流程
                        for (int j = 0; j < drSubList.Count(); j++)
                        {
                            string strSubFlowID = drSubList[j]["FlowID"].ToString();
                            string strSubFlowCaseID = drSubList[j]["FlowCaseID"].ToString();
                            swResult.Write(getFlowFullLogList(strSubFlowID, strSubFlowCaseID, bolIsDisplaySubFlow, intLevelindex + 1));
                        }
                        //返回本階
                        if (dtCnt + 1 == dt.Rows.Count)
                            swResult.Write(LogHeader(intLevelindex, true));
                        else
                            swResult.Write(LogHeader(intLevelindex));
                    }
                }

                //若為本階最後一筆，需處理 LogFooter
                if (dtCnt + 1 == dt.Rows.Count) swResult.Write(LogFooter(intLevelindex, true));
            }
            //LogEnd
            swResult.Write(LogEnd());
        }

        if (ucIsShowClosedReason)
        {
            swResult.Write(tmpWriter);
        }
        return swResult;
    }

    public StringWriter LogIndicator(string strToggleKeyID, string strFlowCaption, string strFlowCurrStepName, bool IsFlowOpen = true)
    {
        //** FlowLogIndicator **
        //<div onclick=ToggleDisplay('eDoc-20130101.00001'); >
        //<span class='FlowCaption' >公文主流程《銀行.法令遵循處》</span>
        //<span class='FlowArrow' >▶</span>
        //<span class='FlowOpen'>未結案</span>
        //</div>
        StringWriter swResult = new StringWriter();
        strToggleKeyID = strToggleKeyID.Replace(" ", "");

        //swResult.WriteLine(string.Format("<div style='cursor: pointer;' onclick=\"oID=document.getElementById('{0}');(oID.style.display == 'none')?oID.style.display = '':oID.style.display = 'none';\" >", strToggleKeyID));
        swResult.WriteLine(string.Format("<div style='cursor: pointer;' onclick=\"Util_ToggleDisplay('{0}Full');Util_ToggleDisplay('{0}Lite');\" >", strToggleKeyID));
        swResult.WriteLine(string.Format("<span class='FlowCaption' >{0}</span>", strFlowCaption));
        swResult.WriteLine("<span class='FlowArrow' >▶</span>");
        swResult.WriteLine(string.Format("<span class='{2}' title='{1}'>{0}</span>", strFlowCurrStepName, strToggleKeyID, IsFlowOpen ? "FlowOpen" : "FlowClose"));
        swResult.WriteLine("</div>");
        return swResult;
    }

    public StringWriter LogStart(string strToggleKeyID, bool IsScopeFull = true, bool IsDisplay = true)
    {
        //** Level Start **
        //<table border='0' >
        //<tr id='eDoc-20130101.00001' ><td class='FlowScopeFull'></td><td>
        StringWriter swResult = new StringWriter();
        strToggleKeyID = strToggleKeyID.Replace(" ", "");

        swResult.WriteLine("<table border='0' >");
        if (IsScopeFull)
            swResult.WriteLine(string.Format("<tr id='{0}Full' style='border-style:none;display:{1};' ><td class='FlowScopeFull'>　</td><td>", strToggleKeyID, IsDisplay ? "" : "none"));
        else
            swResult.WriteLine(string.Format("<tr id='{0}Lite' style='border-style:none;display:{1};' ><td class='FlowScopeLite'>　</td><td>", strToggleKeyID, IsDisplay ? "" : "none"));

        return swResult;
    }

    public StringWriter LogEnd()
    {
        //** Level End **
        //</td></tr></table>
        StringWriter swResult = new StringWriter();
        swResult.WriteLine("</td></tr></table>");
        return swResult;
    }

    public StringWriter LogHeader(int intLevelindex = 0, bool IsEnd = false)
    {
        //<table border='0' cellspacing='0' cellpadding='0' width='800' >
        //<tr >
        //    <td class='cls1_Header' width='130' >關卡</td>
        //    <td class='cls1_Header' width='150' >處理者</td>
        //    <td class='cls1_Header' width='100' >時間</td>
        //    <td class='cls1_Header' width='120' >動作</td>
        //    <td class='cls1_Header' width='300' >意見</td>
        //</tr>
        string strCSSName = string.Format("cls{0}_Header", intLevelindex + 1);
        StringWriter swResult = new StringWriter();
        swResult.WriteLine("<table border='0' cellspacing='0' cellpadding='0' width='100%' >");
        if (!IsEnd)
        {
            swResult.WriteLine("<tr >");
            swResult.WriteLine(string.Format("<td class='{0}' width='130' >{1}</td>", strCSSName, WorkRS.Resources.FlowLogMsg_StepName)); //關卡
            swResult.WriteLine(string.Format("<td class='{0}' width='150' >{1}</td>", strCSSName, WorkRS.Resources.FlowLogMsg_AssignTo)); //處理者
            swResult.WriteLine(string.Format("<td class='{0}' width='100' >{1}</td>", strCSSName, WorkRS.Resources.FlowLogMsg_UpdDateTime)); //時間
            swResult.WriteLine(string.Format("<td class='{0}' width='120' >{1}</td>", strCSSName, WorkRS.Resources.FlowLogMsg_VerifyBtn)); //動作
            swResult.WriteLine(string.Format("<td class='{0}' width='300' >{1}</td>", strCSSName, WorkRS.Resources.FlowLogMsg_VerifyOpinion)); //意見
            swResult.WriteLine("</tr>");
        }
        return swResult;
    }

    public StringWriter LogFooter(int intLevelindex = 0, bool IsEnd = false)
    {
        //<tr>
        //    <td class='cls2_Footer' colspan='5'></td>
        //</tr>
        //</table>
        string strCSSName = string.Format("cls{0}_Footer", intLevelindex + 1);
        StringWriter swResult = new StringWriter();
        if (!IsEnd)
        {
            swResult.WriteLine("<tr >");
            swResult.WriteLine(string.Format("<td class='{0}' colspan='5' >　</td>", strCSSName));
            swResult.WriteLine("</tr>");
        }
        swResult.WriteLine("</table>");
        return swResult;
    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {
        //ex:  <a href="javascript:__doPostBack('ucFlowFullLogList1$btnAttach','eDoc,eDoc-20130130.00002.00010');" >Test</a>
        string[] strPara = Util.getRequestForm()["__EVENTARGUMENT"].Split(',');

        if (strPara.Count() != 2)
        {
            Util.MsgBox(WorkRS.Resources.FlowFullLogMsg_AttachParaError);
        }
        string strAttachDB = strPara[0];
        string strAttachID = strPara[1];

        if (!string.IsNullOrEmpty(strAttachID))
        {
            if (Util.getAttachIDEffectQty(strAttachDB, strAttachID) <= 0)
            {
                Util.MsgBox(WorkRS.Resources.FlowFullLogMsg_AttachNotFound);
            }
            else
            {
                string strFlowLogAttachUrl = string.Format("{0}?AttachDB={1}&AttachID={2}", Util._AttachDownloadUrl , strAttachDB, strAttachID);
                if (ucIsPopNewWindow)
                {
                    //開新視窗模式 2017.03.01 新增
                    string strJS = "dom.Ready(function(){ var AttachPopWin=window.open('" + strFlowLogAttachUrl + "','Attach01'";
                    strJS += ",'width=650,height=350,top=' + Util_getPopTop(350) + ',left=' + Util_getPopLeft(650) + '";
                    strJS += ",status=no,toolbar=no,menubar=no,location=no,resizable=yes,scrollbars=yes');";
                    strJS += "AttachPopWin.focus(); });";
                    Util.setJSContent(strJS, this.ClientID + "_FlowLogAttach_Popup");
                }
                else
                {
                    //彈出子視窗
                    ucModalPopup1.ucPopupHeader = WorkRS.Resources.FlowVerifyTab_FlowAttach; //流程附件
                    ucModalPopup1.ucFrameURL = strFlowLogAttachUrl;
                    ucModalPopup1.ucBtnCloselEnabled = true;
                    ucModalPopup1.ucBtnCancelEnabled = false;
                    ucModalPopup1.ucBtnCompleteEnabled = false;
                    ucModalPopup1.Show();
                }
            }
        }
    }
}