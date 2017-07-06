using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Data;

using RS = SinoPac.WebExpress.Common.Properties;
using System.Data.Common;
using System.Drawing;
using SinoPac.WebExpress.Work;

public partial class Punch_PunchAppdOperation : SecurePage
{
    public string _CurrFlowID
    {
        get
        {
            if (ViewState["_CurrFlowID"] == null)
            {
                ViewState["_CurrFlowID"] = (Request["FlowID"] != null) ? Request["FlowID"].ToString() : "";
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
                ViewState["_CurrFlowLogID"] = (Request["FlowLogID"] != null) ? Request["FlowLogID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowLogID"]);
        }
        set
        {
            ViewState["_CurrFlowLogID"] = value;
        }
    }
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private string _PunchUpdate = Util.getAppSetting("app://AattendantDB_PunchUpdate/"); //PunchUpdate_ITRD
    //private string _DBName2 = "DB_VacSys";
    //public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
    private string FlowCustDB = "";
    private string strDataKeyNames = "CompID,EmpID,EmpName,DutyDate,DutyTime,PunchDate,PunchTime,PunchConfirmSeq,DeptID,DeptName,OrganID,OrganName,FlowOrganID,FlowOrganName,Sex,PunchFlag,WorkTypeID,WorkType,MAFT10_FLAG,ConfirmStatus,AbnormalType,ConfirmPunchFlag,PunchSeq,PunchRemedySeq,RemedyReasonID,RemedyReasonCN,RemedyPunchTime,AbnormalFlag,AbnormalReasonID,AbnormalReasonCN,AbnormalDesc,Remedy_AbnormalFlag,Remedy_AbnormalReasonID,Remedy_AbnormalReasonCN,Remedy_AbnormalDesc,Source,APPContent,LastChgComp,LastChgID,LastChgDate,RemedyPunchFlag,BatchFlag,PORemedyStatus,RejectReason,RejectReasonCN,ValidDateTime,ValidCompID,ValidID,ValidName,Remedy_MAFT10_FLAG,ConfirmStatusGCN,ConfirmPunchFlagGCN,AbnormalReasonGCN,SourceGCN,SexGCN,SpecialFlag,RestBeginTime,RestEndTime,FlowCaseID";

    protected void ucModalPopup_btnClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        LoadData();
        ucModalPopup.Reset();
        Session["FlowVerifyInfo"] = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress(_PunchUpdate);
        FlowCustDB = _PunchUpdate;

        if (!IsPostBack)
        {
            LoadData();
        }
        ucModalPopup.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup_btnClose);
    }

    /// <summary>
    /// 取得字串(去除null)
    /// </summary>
    private string StringIIF(object str)
    {
        string result = "";
        if (str != null)
        {
            if (!string.IsNullOrEmpty(str.ToString().Trim()))
            {
                result = str.ToString();
            }
        }
        return result;
    }

    protected void LoadData()
    {
        gvMain.Visible = true;
        var isSuccess = false;
        var msg = "";
        var datas = new List<Punch_Confirm_Remedy_Bean>();
        var viewData = new Punch_Confirm_Remedy_Model()
        {
            ValidCompID = StringIIF(UserInfo.getUserInfo().CompID),
            ValidID = StringIIF(UserInfo.getUserInfo().UserID),
            ConfirmStatus = "2"
        };
        isSuccess = PunchUpdate.PunchAppdOperation_DoQuery(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = PunchUpdate.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
    }


    protected void btnCheck_Click(object sender, EventArgs e)//簽核需呼叫流程引擎
    {
        if (!CheckData()) return;
        SaveData();
        LoadData();
        Session["FlowVerifyInfo"] = null;

    }

    private void SaveData()
    {
        string ErrorIndex = "";
        for (int index = 0; index < gvMain.Rows.Count; index++)
        {
            CheckBox objchk = (CheckBox)gvMain.Rows[index].FindControl("chkChoiced");
            if (objchk.Checked == true)
            {
                bool result = false;
                long seccessCount = 0;
                string msg = "", flowCode = "", flowSN = "";
                string btnName = "";

                RadioButton rdoApp = (RadioButton)gvMain.Rows[index].FindControl("rbnApproved");
                RadioButton rdoRej = (RadioButton)gvMain.Rows[index].FindControl("rbnReject");
                TextBox txtReason = (TextBox)gvMain.Rows[index].FindControl("txtReason");

                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> toUserData = new Dictionary<string, string>();
                PunchUpdate.GridViewToDictionary(gvMain, out dic, index, strDataKeyNames);
                Punch_Confirm_Remedy_Bean model = new Punch_Confirm_Remedy_Bean()
                {
                    //跟共用條件
                    CompID = dic["CompID"],
                    EmpID = dic["EmpID"],
                    EmpName = dic["EmpName"],
                    LastChgComp = UserInfo.getUserInfo().CompID.Trim(),
                    LastChgID = UserInfo.getUserInfo().UserID.Trim(),
                    LastChgDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),

                    //預存所需資料
                    PunchTime = dic["PunchTime"],
                    ConfirmPunchFlag = dic["ConfirmPunchFlag"],
                    DutyTime = dic["DutyTime"],
                    RestBeginTime=dic["RestBeginTime"],
                    RestEndTime = dic["RestEndTime"],
                    //Remedy所需資料
                    PunchDate = dic["PunchDate"],
                    PunchRemedySeq = dic["PunchRemedySeq"],
                    FlowCaseID = dic["FlowCaseID"],
                    ValidDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ValidCompID = UserInfo.getUserInfo().CompID.Trim(),
                    ValidID = UserInfo.getUserInfo().UserID.Trim(),
                    ValidName = UserInfo.getUserInfo().UserName.Trim(),

                    //Confirm所需資料
                    DutyDate = dic["DutyDate"],
                    PunchConfirmSeq = dic["PunchConfirmSeq"],
                    RemedyReasonID = dic["RemedyReasonID"],
                    RemedyReasonCN = dic["RemedyReasonCN"],
                    RemedyPunchTime = dic["RemedyPunchTime"],
                    Remedy_MAFT10_FLAG = dic["Remedy_MAFT10_FLAG"],
                    Remedy_AbnormalFlag = dic["Remedy_AbnormalFlag"],
                    Remedy_AbnormalReasonID = dic["Remedy_AbnormalReasonID"],
                    Remedy_AbnormalReasonCN = dic["Remedy_AbnormalReasonCN"],
                    Remedy_AbnormalDesc = dic["Remedy_AbnormalDesc"]

                };

                if (rdoApp.Checked == true)//核准
                {
                    if (!nextFlowBtn(model, out  flowCode, out  flowSN, ref btnName, out toUserData, out msg)) //判斷審核按鈕
                    {
                        Util.MsgBox(msg);
                        return;
                    }
                    switch (btnName)
                    {
                        //PORemedyStatus。0:未處理，1:未送簽，2:送簽中，3:核准，4:駁回
                        //ConfirmStatus。0:正常，1:異常，2:送簽中，3:異常不控管
                        case "btnClose":
                            model.PORemedyStatus = "3";
                             string strConfirmStatus = "", strAbnormalType="";
                             if (!PunchUpdate.PunchAppdOperation_EXEC_PunchCheckData(model, out strConfirmStatus, out strAbnormalType))
                             {
                                 Util.MsgBox("打卡異常檢核失敗!!");
                                 return;
                             }
                            model.ConfirmStatus = strConfirmStatus; //0 or 1記得用John的TSQL
                            model.AbnormalType = strAbnormalType;
                            break;
                        case "btnApprove":
                        case "btnReApprove":
                            model.PORemedyStatus = "2";
                            model.ConfirmStatus = "2";
                            break;
                    }
                }
                else if (rdoRej.Checked == true)//駁回
                {
                    btnName = "btnReject";
                    model.PORemedyStatus = "4";
                    model.ConfirmStatus = "1";
                }

                DataTable LastHROtherFlowLog = PunchUpdate.HROtherFlowLog(model.FlowCaseID, true);
                Dictionary<string, string> oAssDic = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);
                
                if (!string.IsNullOrEmpty(btnName))
                {
                    //審核動作
                    result = PunchUpdate.PunchAppdOperation_SaveData(model, oAssDic, btnName, txtReason.Text, out seccessCount, out msg, flowCode, flowSN, LastHROtherFlowLog.Rows[0]["FlowSeq"].ToString(), LastHROtherFlowLog.Rows[0]["FlowLogBatNo"].ToString(), LastHROtherFlowLog.Rows[0]["FlowLogID"].ToString(), toUserData);
                }
                else
                {
                    result = false;
                }

                if (!result)
                {
                    //Util.MsgBox(msg);
                    ErrorIndex += (index + 1).ToString() + ",";
                    continue;
                }
                if (seccessCount == 0)
                {
                    ErrorIndex += (index + 1).ToString() + ",";
                    continue;
                }
            }
        }
        if (ErrorIndex.Length > 0)
        {
            ErrorIndex = ErrorIndex.Substring(0, ErrorIndex.Length - 1);
            Util.MsgBox("第" + ErrorIndex + "筆審核失敗");
        }
        else
        {
            Util.MsgBox("審核成功");
        }
    }

    private bool CheckData()
    {
        for (int introw = 0; introw < gvMain.Rows.Count; introw++)
        {
            CheckBox objchk = (CheckBox)gvMain.Rows[introw].FindControl("chkChoiced");
            RadioButton rdoApp = (RadioButton)gvMain.Rows[introw].FindControl("rbnApproved");
            RadioButton rdoRej = (RadioButton)gvMain.Rows[introw].FindControl("rbnReject");
            TextBox txtReason = (TextBox)gvMain.Rows[introw].FindControl("txtReason");
            if (objchk.Checked == true)
            {
                if (rdoApp.Checked || rdoRej.Checked)
                {
                    if (txtReason.Text.Length > 200)
                    {
                        Util.MsgBox("審核意見大於200字");
                        return false;
                    }
                }
                if (rdoRej.Checked)
                {
                    if (string.IsNullOrEmpty(txtReason.Text))
                    {
                        Util.MsgBox("駁回意見須填寫");
                        return false;
                    }
                }
                if (!rdoApp.Checked && !rdoRej.Checked)
                {
                    Util.MsgBox("請點選 核准/駁回");
                    return false;
                }
            }
        }
        return true;
    }

    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow clickedRow = ((ImageButton)e.CommandSource).NamingContainer as GridViewRow;
        
        switch (e.CommandName)
        {
            case "Detail":
                string flowCode = "", flowSN = "", msg = "",btnName = "";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Dictionary<string, string> toUserData;
                PunchUpdate.GridViewToDictionary(gvMain, out dic, clickedRow.RowIndex, strDataKeyNames);
                FlowExpress oFlow = new FlowExpress(FlowCustDB, dic["FlowCaseID"], false);
                Punch_Confirm_Remedy_Bean model = new Punch_Confirm_Remedy_Bean()
                {
                    //跟共用條件
                    CompID = dic["CompID"],
                    EmpID = dic["EmpID"],
                    EmpName = dic["EmpName"],
                    LastChgComp = UserInfo.getUserInfo().CompID.Trim(),
                    LastChgID = UserInfo.getUserInfo().UserID.Trim(),
                    LastChgDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),

                    //預存所需資料
                    PunchTime = dic["PunchTime"],
                    ConfirmPunchFlag = dic["ConfirmPunchFlag"],
                    DutyTime = dic["DutyTime"],
                    RestBeginTime = dic["RestBeginTime"],
                    RestEndTime = dic["RestEndTime"],
                    //Remedy所需資料
                    PunchDate = dic["PunchDate"],
                    PunchRemedySeq = dic["PunchRemedySeq"],
                    FlowCaseID = dic["FlowCaseID"],
                    ValidDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ValidCompID = UserInfo.getUserInfo().CompID.Trim(),
                    ValidID = UserInfo.getUserInfo().UserID.Trim(),
                    ValidName = UserInfo.getUserInfo().UserName.Trim(),

                    //Confirm所需資料
                    DutyDate = dic["DutyDate"],
                    PunchConfirmSeq = dic["PunchConfirmSeq"],
                    RemedyReasonID = dic["RemedyReasonID"],
                    RemedyReasonCN = dic["RemedyReasonCN"],
                    RemedyPunchTime = dic["RemedyPunchTime"],
                    Remedy_MAFT10_FLAG = dic["Remedy_MAFT10_FLAG"],
                    Remedy_AbnormalFlag = dic["Remedy_AbnormalFlag"],
                    Remedy_AbnormalReasonID = dic["Remedy_AbnormalReasonID"],
                    Remedy_AbnormalReasonCN = dic["Remedy_AbnormalReasonCN"],
                    Remedy_AbnormalDesc = dic["Remedy_AbnormalDesc"]
                };
                //按鈕清單
                ClearBtn(model.FlowCaseID);

                //下一關邏輯判斷，產生審核按鈕資訊，給出Ken流程相關資訊
                if (!nextFlowBtn(model, out  flowCode, out  flowSN, ref btnName, out toUserData, out msg)) //判斷審核按鈕
                {
                    Util.MsgBox(msg);
                    return;
                }
                CustVerify.setFlowSignID_CompID(toUserData["SignID"].ToString() + "," + toUserData["SignIDComp"].ToString());
                //傳給審核畫面-單筆審核使用
                Session["PunchAppdOperation_toUserData"] = toUserData;
                Session["PunchAppdOperation_GridView"] = dic;
                //產生審核按鈕
                FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, model.ValidID.Trim(), FlowCustDB.Split(','), "".Split(','), false, "", "");
                //跳轉畫面
                Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}", oFlow.FlowID, oFlow.FlowCurrLastLogID, "Self", "N", "N", ""));
                break;
        }
    }

    #region"噁心的流程相關邏輯(來自OverTimeCheck.aspx.cs)"

    private String RankIDMapping(string CompID, string RankID)
    {
        if (CompID == "") return "";
        if (RankID == "") return "";
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append("SELECT Top 1 [RankIDMap]");
        sb.Append("FROM " + _eHRMSDB + ".[dbo].[RankMapping]");
        sb.Append("where CompID='" + CompID + "' and RankID='" + RankID + "' ;");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["RankIDMap"].ToString() : "";
    }

    private string RankPara(DataTable dt, string CompID, string RankType)
    {
        string RankID = "0";
        Aattendant a = new Aattendant();
        if (dt.Select("CompID='" + CompID + "'").Count() > 0)
        {
            RankID = a.Json2DataTable(dt.Select("CompID='" + CompID + "'").CopyToDataTable().Rows[0]["Para"].ToString()).Rows[0][RankType].ToString();
            return RankID;
        }
        return RankID;
    }

    private bool boolUpEmpRankID(string AD, string FlowCaseID, string UpEmpRankID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        string MapEmpRankID, OTEmpID;
        sb.Append(" SELECT Top 1 isnull(P.CompID,'') as CompID,isnull(P.RankID,'')as RankID FROM " + CustVerify.ADTable(AD) + " OT ");
        sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Personal] P ON OT.OTEmpID=P.EmpID and OT.OTCompID=P.CompID");
        sb.Append(" WHERE OT.FlowCaseID='" + FlowCaseID + "' and OTSeqNo='1'");
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count == 0) return false;
        MapEmpRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UpEmpRankID);
        if (MapEmpRankID == "") return false;
        OTEmpID = RankIDMapping(dt.Rows[0]["CompID"].ToString(), dt.Rows[0]["RankID"].ToString());
        if (OTEmpID == "") return false;
        return int.Parse(MapEmpRankID) <= int.Parse(OTEmpID) ? true : false;
    }

    /// <summary>
    /// 找ValidRankID、EmpRankID的訊息
    /// 當找不到資料時，IsUpValidRankID預設True、IsUpEmpRankID預設False
    /// </summary>
    /// <param name="IsUpValidRankID">審核人是否大於</param>
    /// <param name="IsUpEmpRankID">加班人是否大於</param>
    private void RankIDCheck(string CompID, string FlowCaseID, out bool IsUpValidRankID, out bool IsUpEmpRankID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string ValidRankID = "", EmpRankID = "";
        IsUpValidRankID = true;
        IsUpEmpRankID = false;

        string UserRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().RankID);

        //Para撈取參數設定
        DataTable Paradt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];

        //16 登入者公司
        ValidRankID = RankPara(Paradt, UserInfo.getUserInfo().CompID, "ValidRankID");
        //RankMapping
        ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, ValidRankID);
        //若無資料
        if (ValidRankID == "")
        {
            IsUpValidRankID = true;
        }
        else
        {
            //若登入者無資料，預設大於等於ValidRankID
            if (UserRankID == "")
            {
                IsUpValidRankID = true;
            }
            else
            {
                //是否大於
                IsUpValidRankID = Convert.ToInt32(UserRankID) >= int.Parse(ValidRankID) ? true : false;
            }
        }

        //19 加班人公司
        EmpRankID = RankPara(Paradt, CompID, "EmpRankID");
        if (EmpRankID == "")
        {
            IsUpEmpRankID = false;
        }
        else
        {
            //因為東西偏多，獨立為一個程式
            IsUpEmpRankID = boolUpEmpRankID("D", FlowCaseID, EmpRankID);
        }
    }

    private bool nextFlowBtn(Punch_Confirm_Remedy_Bean Bean, out string flowCode, out string flowSN, ref string btnName, out Dictionary<string, string> toUserData, out string msg)
    {
        flowCode = ""; flowSN = ""; msg = "";
        toUserData = new Dictionary<string, string>();
        string OTEmpID = Bean.EmpID,
            AssignTo = Bean.ValidID,
            CompID = Bean.CompID,
            PunchDate = Bean.PunchDate,
            FlowCaseID = Bean.FlowCaseID;

        bool IsUpValidRankID = true;
        bool IsUpEmpRankID = false;

        bool isLastFlow, nextIsLastFlow;
        string signLineDefine = "", meassge = "";
        string SignOrganID = "", SignID = "", SignIDComp = "";

        string FlowStepID = "";
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.Append("SELECT FlowStepID from " + FlowCustDB + "FlowOpenLog ");
        sb.Append(" where FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
        try
        {
            FlowStepID = db.ExecuteScalar(sb.BuildCommand()).ToString();
        }
        catch (Exception)
        {
            FlowStepID = "";
            msg = "永豐流程查無資料";
            return false;
        }
        //讀取現在關卡與下一關相關資料，因為不論回傳是否，我還是要資料，所以沒檢核return與meassge
        OBFlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, PunchDate, FlowCaseID, "P",
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge, "PO");

        //如果QueryFlowDataAndToUserData出錯(HR關與改派容易出現沒有下一關卻無法判定現在是最後一關的狀況)，會找不到現在是否為最後一關，這裡單獨再找一次
        isLastFlow = isLastFlowNow(Bean.CompID, FlowCaseID, "P", "PO");

        //如果沒有下一關資料，則用現在關卡資料取代(對應上述QueryFlowDataAndToUserData問題 )
        if (toUserData.Count == 0)
        {
            //取[最近的行政or功能]資料 取代 [現在關卡]資料
            DataTable dtHROtherFlowLog_toUD = PunchUpdate.HROtherFlowLog(FlowCaseID, true);
            toUserData.Add("SignLine", dtHROtherFlowLog_toUD.Rows[0]["SignLine"].ToString());
            toUserData.Add("SignIDComp", dtHROtherFlowLog_toUD.Rows[0]["SignIDComp"].ToString());
            toUserData.Add("SignID", AssignTo);
            toUserData.Add("SignOrganID", dtHROtherFlowLog_toUD.Rows[0]["SignOrganID"].ToString());
            toUserData.Add("SignFlowOrganID", dtHROtherFlowLog_toUD.Rows[0]["SignFlowOrganID"].ToString());
        }

        //如果下一關主管與現在主管相同，則再往上階找下一關主管資料
        if (toUserData["SignID"] == AssignTo && signLineDefine != "3")
        {
            switch (toUserData["SignLine"])
            {
                //HR線 或 行政線
                case "4":
                case "1":
                    if (EmpInfo.QueryOrganData(Bean.CompID, toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = SignOrganID;
                        toUserData["SignFlowOrganID"] = "";
                    }
                    break;
                //功能線
                case "2":
                    if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = "";
                        toUserData["SignFlowOrganID"] = SignOrganID;
                    }
                    break;

                //原本switch的是signLineDefine，現在改成toUserData["SignLine"]後，
                //case "3"裏頭的if基本只會用到else[非功能線一律走行政線]，以防萬一先保留。
                //改派
                case "3":
                    if (toUserData["SignLine"] == "2")
                    {
                        if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = "";
                            toUserData["SignFlowOrganID"] = SignOrganID;
                        }
                    }
                    else
                    {
                        if (EmpInfo.QueryOrganData(Bean.CompID, toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = SignOrganID;
                            toUserData["SignFlowOrganID"] = "";
                        }
                    }
                    break;
            }
        }

        RankIDCheck(CompID, FlowCaseID, out IsUpValidRankID, out IsUpEmpRankID);
        if (signLineDefine == "4")//HR特別關(後台沒有送簽功能的話，這裡不會用到)
        {
            if (IsUpValidRankID)//大於Rank16
            {
                Session["btnVisible"] = "2";//給永豐流程按鈕隱藏判斷
                btnName = "btnClose";//回傳按鈕名稱，給多筆審核組進DataTable傳給審核畫面
            }
            else
            {
                if (FlowStepID == "A10") //預防HR送錯關，本來資料上HR關是只有A20或A40等最後一關
                {
                    Session["btnVisible"] = "0";
                    btnName = "btnApprove";
                }
                else
                {
                    Session["btnVisible"] = "1";
                    btnName = "btnReApprove";
                }
            }
        }
        else if (!isLastFlowNow(CompID, FlowCaseID, "P", "PO"))//非最後一關
        {
            if (IsUpEmpRankID)//大於Rank19
            {
                Session["btnVisible"] = "2";
                btnName = "btnClose";
            }
            else
            {
                if (nextIsLastFlow) //下一關是否進入A40最後一關
                {
                    Session["btnVisible"] = "0";
                    btnName = "btnApprove";
                }
                else
                {
                    Session["btnVisible"] = "1";
                    btnName = "btnReApprove";
                }
            }
        }
        else//最後一關
        {
            if (OTEmpID.Trim().Equals(UserInfo.getUserInfo().UserID.Trim()))//當加班人是自己主管的代理人並審自己的加班單
            {
                if (IsUpEmpRankID)//加班人RankID>19 
                {
                    Session["btnVisible"] = "2";
                    btnName = "btnClose";
                }
                else
                {
                    Session["btnVisible"] = "1";
                    btnName = "btnReApprove";
                }
            }
            else if (IsUpValidRankID)//大於Rank16
            {
                Session["btnVisible"] = "2";
                btnName = "btnClose";
            }
            else
            {
                Session["btnVisible"] = "1";
                btnName = "btnReApprove";
            }
        }

        //如果找不到下一關主管資料，彈跳視窗並且return false
        if (toUserData["SignID"] == "")
        {
            toUserData["SignIDComp"] = UserInfo.getUserInfo().CompID.Trim();
            toUserData["SignID"] = UserInfo.getUserInfo().UserID.Trim();
            //Util.MsgBox("查無下一關主管資料");
            if (isLastFlow) //最後一關不用找下一關主管
            {
                return true;
            }
            else
            {
                msg = "查無下一關主管資料";
                return false;
            }
        }
        return true;
    }

    private void ClearBtn(string FlowCaseID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" update " + FlowCustDB + "FlowOpenLog ");
        sb.Append(" set FlowStepBatchEnabled=null ");
        sb.Append(" ,FlowStepOpinion=null ");
        sb.Append(" ,FlowStepBtnInfoCultureCode=null ");
        sb.Append(" ,FlowStepBtnInfoJSON=null ");
        sb.Append(" where FlowCaseID='" + FlowCaseID + "' ");
        db.ExecuteNonQuery(sb.BuildCommand());
    }

    private bool isLastFlowNow(string CompID, string flowCaseID, string Model, string insystemID)
    {
        DataRow retrunRow;
        string message = "";
        try
        {
            if (!OBFlowUtility.QueryHRFlowEngineDatas_Now(CompID, flowCaseID, Model, out retrunRow, out message, insystemID))
                return false;
            else if (retrunRow.Table.Rows.Count > 0)
            {
                string FlowEndFlag = retrunRow["FlowEndFlag"].ToString();
                return FlowEndFlag == "1" ? true : false;
            }
            else
                return true;
        }
        catch (Exception)
        {
            return true;
        }
    }
    #endregion"噁心的流程相關邏輯"
}
