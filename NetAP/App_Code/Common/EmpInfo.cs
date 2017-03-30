using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;
using System.Data;

/// <summary>
/// EmpInfo 的摘要描述
/// </summary>
public class EmpInfo
{
    #region "全域變數"
    private static string _AattendantDBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private static DbHelper db = new DbHelper(_AattendantDBName);
    //private static DbHelper db = new DbHelper(_eHRMSDB_ITRD);
    #endregion "全域變數"

    #region"審核用(test)，沒其他人會用"
    //將EmpID組成Dictionary<EmpID,EmpName>方便丟給oAssDic
    public Dictionary<string, string> getEmpID_Name_Dictionary(string EmpID, string CompID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        sb.Append(" SELECT DISTINCT P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".dbo. Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        sb.Append(" WHERE P.EmpID='" + EmpID + "' and P.CompID='" + CompID + "' ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }
    //行政線下一關主管資訊(目前不限制是否為(無效、虛擬)組織)
    private DataTable QueryNextOrganData(string inCompID, string inOrganID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        int intLoop = 0;
        string TempBoss = "";
        //sb.Append(" SELECT DISTINCT O.CompID,O.OrganID,O.Boss,O.UpOrganID,O.DeptID,P.NameN ");
        sb.Append(" SELECT  Top 1 ");
        sb.Append(" O.CompID, O.OrganID, O.Boss, O.UpOrganID, O.DeptID,O1.OrganName as DeptName, P.NameN, O.InValidFlag ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Organization O ");
        sb.Append(" Left join " + _eHRMSDB + ".dbo. Personal P on P.EmpID=O.Boss ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O1 on O1.CompID=O.CompID and O1.OrganID=O.DeptID ");
        //sb.Append(" WHERE InValidFlag='0' and O.CompID='" + inCompID + "' and O.OrganID='" + inOrganID + "'");
        sb.Append(" WHERE  O.CompID='" + inCompID + "' and O.OrganID='" + inOrganID + "' Order By O.InValidFlag asc ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();
        if (dt.Rows.Count == 0) return null;

        TempBoss = dt.Rows[0]["Boss"].ToString();
        while (dt.Rows.Count > 0 && dt.Rows[0]["Boss"].ToString() == TempBoss && intLoop <= 10)
        {
            sb.Append(" SELECT  Top 1 ");
            sb.Append(" O.CompID, O.OrganID, O.Boss, O.UpOrganID, O.DeptID,O1.OrganName as DeptName, P.NameN, O.InValidFlag ");
            //sb.Append(" SELECT DISTINCT O.CompID,O.OrganID,O.Boss,O.UpOrganID,O.DeptID,P.NameN ");
            sb.Append(" FROM " + _eHRMSDB + ".dbo.Organization O ");
            sb.Append(" Left join " + _eHRMSDB + ".dbo. Personal P on P.EmpID=O.Boss ");
            sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O1 on O1.CompID=O.CompID and O1.OrganID=O.DeptID");
            //sb.Append(" WHERE InValidFlag='0' and O.CompID='" + dt.Rows[0]["CompID"] + "' and O.OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
            sb.Append(" WHERE O.CompID='" + dt.Rows[0]["CompID"] + "' and O.OrganID='" + dt.Rows[0]["UpOrganID"] + "' Order By O.InValidFlag asc ");
            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            sb.Reset();
            intLoop = intLoop + 1;
        }
        return dt;
    }
    //功能線下一關主管資訊(目前不限制是否為(無效、虛擬)組織)
    private DataTable QueryNextFlowOrganData(string inOrganID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        int intLoop = 0;
        string TempBoss = "";
        sb.Append(" SELECT Top 1 O.CompID,O.OrganID,O.Boss,O.UpOrganID,O.DeptID,O1.OrganName as DeptName,P.NameN ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. OrganizationFlow O ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.OrganizationFlow O1 on  O1.OrganID=O.DeptID");
        sb.Append(" Left join " + _eHRMSDB + ".dbo. Personal P on P.EmpID=O.Boss ");
        sb.Append(" WHERE O.OrganID='" + inOrganID + "'");
        sb.Append(" Order By O.InValidFlag asc ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();
        if (dt.Rows.Count == 0) return null;

        TempBoss = dt.Rows[0]["Boss"].ToString();
        while (dt.Rows.Count > 0 && dt.Rows[0]["Boss"].ToString() == TempBoss && intLoop <= 10)
        {
            sb.Append(" SELECT Top 1 O.CompID,O.OrganID,O.Boss,O.UpOrganID,O.DeptID,O1.OrganName as DeptName,P.NameN ");
            sb.Append(" FROM " + _eHRMSDB + ".dbo.OrganizationFlow O ");
            sb.Append(" left join " + _eHRMSDB + ".dbo.OrganizationFlow O1 on  O1.OrganID=O.DeptID ");
            sb.Append(" Left join " + _eHRMSDB + ".dbo. Personal P on P.EmpID=O.Boss ");
            sb.Append(" WHERE O.OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
            sb.Append(" Order By O.InValidFlag asc ");
            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            sb.Reset();
            intLoop = intLoop + 1;
        }
        return dt;
    }

    public string ADTable(string AD)
    {
        switch (AD)
        {
            case "A":
            case "1":
                return "OverTimeAdvance";
            case "D":
            case "2":
                return "OverTimeDeclaration";
        }
        return AD;
    }

    private String RankIDMapping(string CompID, string RankID)
    {
        if (CompID == "") return "";
        if (RankID == "") return "";
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append("SELECT Top 1 [RankIDMap]");
        sb.Append("FROM [eHRMSDB_ITRD].[dbo].[RankMapping]");
        sb.Append("where CompID='" + CompID + "' and RankID='" + RankID + "' ;");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["RankIDMap"].ToString() : "";
        //return db.ExecuteScalar(CommandType.Text, sb.ToString()).ToString();
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
    /// <summary>
    /// 事先按鈕檢查
    /// </summary>
    /// <param name="CompID"></param>
    /// <param name="AssignTo"></param>
    /// <param name="OTStartDate"></param>
    /// <param name="FlowCaseID"></param>
    /// <param name="btn"></param>
    /// <returns></returns>
    public Dictionary<string, string> AbtnAdd(string CompID, string AssignTo, string OTStartDate, string FlowCaseID, string btn)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        DataTable dtO;
        FlowExpress oFlow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        Aattendant a = new Aattendant();
        sb.Append("  SELECT *  FROM OverTimeAdvance O ");
        sb.Append(" left join " + _AattendantDBName + "FlowOpenLog L on O.FlowCaseID=L.FlowCaseID ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.Personal P on P.CompID=O.OTCompID and P.EmpID=O.OTEmpID ");
        sb.Append(" WHERE O.FlowCaseID='" + FlowCaseID + "' and O.OTSeqNo='1'");
        dtO = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

        //RankID比較
        //DataTable dt; //16與19的Json暫存
        DataTable rdt;
        string ValidRankID = "", EmpRankID = "";
        //登入者
        string UserRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().RankID);
        //加班人
        string OTEmpRankID = RankIDMapping(dtO.Rows[0]["CompID"].ToString(), dtO.Rows[0]["RankID"].ToString());
        //Para
        rdt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];
        //16
        ValidRankID = RankPara(rdt, UserInfo.getUserInfo().CompID, "ValidRankID");
        ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, ValidRankID);
        if (ValidRankID == "") ValidRankID = "-1";

        //19
        EmpRankID = RankPara(rdt, dtO.Rows[0]["OTCompID"].ToString(), "EmpRankID");
        EmpRankID = RankIDMapping(dtO.Rows[0]["OTCompID"].ToString(), EmpRankID);
        if (EmpRankID == "") EmpRankID = "100";

        //dt = a.Json2DataTable(db.ExecuteScalar(CommandType.Text, string.Format("SELECT DISTINCT [Para] FROM [OverTimePara]  WHERE [CompID]='" + UserInfo.getUserInfo().CompID) + "'").ToString());
        //string ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, dt.Rows[0]["ValidRankID"].ToString());

        //dt = a.Json2DataTable(db.ExecuteScalar(CommandType.Text, string.Format("SELECT DISTINCT [Para] FROM [OverTimePara]  WHERE [CompID]='" + dtO.Rows[0]["OTCompID"].ToString()) + "'").ToString());
        //string EmpRankID = RankIDMapping(dtO.Rows[0]["OTCompID"].ToString(), dt.Rows[0]["EmpRankID"].ToString());

        int OTEmpRankIDNo, UserRankIDNo, ValidRankIDNo, EmpRankIDNo;

        if (!int.TryParse(ValidRankID, out ValidRankIDNo)) ValidRankIDNo = -1;
        if (!int.TryParse(EmpRankID, out EmpRankIDNo)) EmpRankIDNo = 100;

        if (!int.TryParse(OTEmpRankID, out OTEmpRankIDNo)) OTEmpRankIDNo = 99;
        if (!int.TryParse(UserRankID, out UserRankIDNo)) UserRankIDNo = 99;

        //QueryFlowDataAndToUserData
        Dictionary<string, string> toUserData;
        bool isLastFlow, nextIsLastFlow;
        string flowCodeFlag = "0";
        //string otModel = "A";
        string flowCode = "";
        string flowSN = "";
        string signLineDefine = "";
        string meassge = "";
        FlowUtility.QueryFlowDataAndToUserData(dtO.Rows[0]["OTCompID"].ToString(), dtO.Rows[0]["AssignTo"].ToString(), dtO.Rows[0]["OTStartDate"].ToString(), FlowCaseID, flowCodeFlag,
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);
        /*==========================================*/
        sb.Append(" SELECT DISTINCT P.EmpID,P.NameN ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
        sb.Append(" WHERE P.EmpID='" + dtO.Rows[0]["AssignTo"].ToString() + "'");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);

        /*===========================================*/
        if (isLastFlow) //事先最後一關
        {
            if (UserRankIDNo < ValidRankIDNo)//小於16
            {
                //btnReApprove
                if (btn == "btnReApprove") return _dic;
            }
            else //不小於16
            {
                //btnClose
                if (btn == "btnClose") return _dic;
            }
        }
        else //事先非最後一關
        {
            if (nextIsLastFlow)
            {
                //btnApprove
                if (btn == "btnApprove") return _dic;
            }
            else if (UserRankIDNo < EmpRankIDNo)//小於19
            {
                //btnReApprove
                if (btn == "btnReApprove") return _dic;
            }
            else //不小於19
            {
                //btnClose
                if (btn == "btnClose") return _dic;
            }
        }
        return null;
    }
    /// <summary>
    /// 事後按鈕檢查
    /// </summary>
    /// <param name="CompID"></param>
    /// <param name="AssignTo"></param>
    /// <param name="OTStartDate"></param>
    /// <param name="FlowCaseID"></param>
    /// <param name="btn"></param>
    /// <returns></returns>
    public Dictionary<string, string> DbtnAdd(string CompID, string AssignTo, string OTStartDate, string FlowCaseID, string btn)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string FlowDB = Util.getAppSetting("app://AattendantDB_OverTime/");
        FlowExpress oFlow = new FlowExpress(FlowDB);
        Aattendant a = new Aattendant();
        sb.Append("  SELECT *  FROM OverTimeDeclaration O ");
        sb.Append(" left join " + _AattendantDBName + "FlowOpenLog L on O.FlowCaseID=L.FlowCaseID ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.Personal P on P.CompID=O.OTCompID and P.EmpID=O.OTEmpID ");
        sb.Append(" WHERE O.FlowCaseID='" + FlowCaseID + "' and O.OTSeqNo='1'");
        DataTable dtO = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

        //RankID比較
        DataTable dt;

        string UserRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().RankID);
        string OTEmpRankID = RankIDMapping(dtO.Rows[0]["CompID"].ToString(), dtO.Rows[0]["RankID"].ToString());

        dt = a.Json2DataTable(db.ExecuteScalar(CommandType.Text, string.Format("SELECT DISTINCT [Para] FROM [OverTimePara]  WHERE [CompID]='" + UserInfo.getUserInfo().CompID) + "'").ToString());
        string ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, dt.Rows[0]["ValidRankID"].ToString());

        dt = a.Json2DataTable(db.ExecuteScalar(CommandType.Text, string.Format("SELECT DISTINCT [Para] FROM [OverTimePara]  WHERE [CompID]='" + dtO.Rows[0]["OTCompID"].ToString()) + "'").ToString());
        string EmpRankID = RankIDMapping(dtO.Rows[0]["OTCompID"].ToString(), dt.Rows[0]["EmpRankID"].ToString());

        int OTEmpRankIDNo, UserRankIDNo, ValidRankIDNo, EmpRankIDNo;

        if (!int.TryParse(ValidRankID, out ValidRankIDNo)) ValidRankIDNo = -1;
        if (!int.TryParse(EmpRankID, out EmpRankIDNo)) EmpRankIDNo = 100;

        if (!int.TryParse(OTEmpRankID, out OTEmpRankIDNo)) OTEmpRankIDNo = 99;
        if (!int.TryParse(UserRankID, out UserRankIDNo)) UserRankIDNo = 99;


        //QueryFlowDataAndToUserData
        Dictionary<string, string> toUserData;
        bool isLastFlow, nextIsLastFlow;
        string flowCodeFlag = "1";
        //string otModel = "D";
        string flowCode = "";
        string flowSN = "";
        string signLineDefine = "";
        string meassge = "";
        FlowUtility.QueryFlowDataAndToUserData(dtO.Rows[0]["OTCompID"].ToString(), dtO.Rows[0]["AssignTo"].ToString(), dtO.Rows[0]["OTStartDate"].ToString(), FlowCaseID, flowCodeFlag,
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);
        /*==========================================*/
        sb.Reset();
        sb.Append(" SELECT DISTINCT P.EmpID,P.NameN ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
        sb.Append(" WHERE P.EmpID='" + toUserData["SignID"].ToString() + "' and P.CompID='" + toUserData["SignIDComp"].ToString() + "' ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        /*===========================================*/
        #region"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"
        string HRLogCompID = signLineDefine == "4" ?
                dtO.Rows[0]["OTRegisterComp"].ToString() :
                dtO.Rows[0]["OTCompID"].ToString();

        if (toUserData.Count == 0)
        {
            //取[最近的行政or功能]資料 取代 [現在關卡]資料
            DataTable toUDdt = HROverTimeLog(oFlow.FlowCaseID, false);
            toUserData.Add("SignLine", toUDdt.Rows[0]["SignLine"].ToString());
            toUserData.Add("SignIDComp", toUDdt.Rows[0]["SignIDComp"].ToString());
            toUserData.Add("SignID", oFlow.FlowCurrLogAssignTo);
            toUserData.Add("SignOrganID", toUDdt.Rows[0]["SignOrganID"].ToString());
            toUserData.Add("SignFlowOrganID", toUDdt.Rows[0]["SignFlowOrganID"].ToString());
        }

        if (toUserData["SignID"] == dtO.Rows[0]["AssignTo"].ToString())
        {
            DataTable toUDdt;
            switch (toUserData["SignLine"])
            {
                case "4":
                case "1":
                    toUDdt = QueryNextOrganData(HRLogCompID, toUserData["SignOrganID"]);
                    toUserData["SignID"] = toUDdt.Rows[0]["Boss"].ToString();
                    toUserData["SignIDComp"] = toUDdt.Rows[0]["BossCompID"].ToString();
                    toUserData["SignOrganID"] = toUDdt.Rows[0]["OrganID"].ToString();
                    toUserData["SignFlowOrganID"] = "";
                    break;
                case "2":
                    toUDdt = QueryNextFlowOrganData(toUserData["SignFlowOrganID"]);
                    toUserData["SignID"] = toUDdt.Rows[0]["Boss"].ToString();
                    toUserData["SignIDComp"] = toUDdt.Rows[0]["BossCompID"].ToString();
                    toUserData["SignOrganID"] = "";
                    toUserData["SignFlowOrganID"] = toUDdt.Rows[0]["OrganID"].ToString();
                    break;
                case "3":
                    if (toUserData["SignLine"] == "2")
                    {
                        toUDdt = QueryNextFlowOrganData(toUserData["OrganID"]);
                        toUserData["SignFlowOrganID"] = toUDdt.Rows[0]["OrganID"].ToString();
                        toUserData["SignID"] = toUDdt.Rows[0]["Boss"].ToString();
                        toUserData["SignIDComp"] = toUDdt.Rows[0]["BossCompID"].ToString();
                    }
                    else
                    {
                        toUDdt = QueryNextOrganData(HRLogCompID, toUserData["SignOrganID"]);
                        toUserData["SignOrganID"] = toUDdt.Rows[0]["SOrganID"].ToString();
                        toUserData["SignID"] = toUDdt.Rows[0]["Boss"].ToString();
                        toUserData["SignIDComp"] = toUDdt.Rows[0]["BossCompID"].ToString();
                    }
                    break;
            }
        }
        _dic = getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);

        #endregion"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"
        /*===========================================*/
        //str[0] = "Y";
        //FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, strValue, strName);
        if (signLineDefine == "4") //HR關卡，需檢查是否大於16
        {
            if (UserRankIDNo < ValidRankIDNo)//小於16
            {
                //btnReApprove
                if (btn == "btnReApprove") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnReApprove".Split(','));
            }
            else
            {
                //btnClose
                if (btn == "btnClose") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnClose".Split(','));
            }
        }
        else if (isLastFlow) //事後最後一關
        {
            if (UserRankIDNo < ValidRankIDNo)//小於16
            {
                //btnReApprove
                if (btn == "btnReApprove") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnReApprove".Split(','));
            }
            else //不小於16
            {
                //btnClose

                if (btn == "btnClose") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnClose".Split(','));
            }
        }
        else //事後非最後一關
        {
            if (nextIsLastFlow)
            {
                //btnApprove
                if (btn == "btnApprove") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnApprove".Split(','));
            }
            else if (OTEmpRankIDNo < EmpRankIDNo)//小於19
            {
                //btnReApprove
                if (btn == "btnReApprove") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnReApprove".Split(','));
            }
            else //不小於19
            {
                //btnClose
                if (btn == "btnClose") FlowExpress.IsFlowCaseValueChanged(FlowDB, FlowCaseID, FlowExpress.FlowCaseValueChangeKind.FlowCustVarValueList, btn.Split(','), "btnClose".Split(','));
            }
        }
        return _dic;

    }
    #endregion"審核用"

    #region 20170207 leo 共用 (EmpID相關資料撈取)
    #region"GetBoss更新，套用FlowUtility"
    private DataTable HROverTimeLog(string FlowCaseID, bool notSP)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        if (notSP)
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2') order by Seq desc");
        else
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }
   /// <summary>
   /// 舊版撈取主管資訊，之後將會被AbtnAdd與DbtnAdd取代
   /// </summary>
   /// <param name="CompID"></param>
   /// <param name="AssignTo"></param>
   /// <param name="OTStartDate"></param>
   /// <param name="FlowCaseID"></param>
   /// <returns></returns>
    public Dictionary<string, string> getOrganBoss(string CompID, string AssignTo, string OTStartDate, string FlowCaseID)
    {
        //DataTable HRdt = HROverTimeLog(FlowCaseID, false);
        //Dictionary<string, string> toUserData;
        //bool isLastFlow, nextIsLastFlow;
        //string flowCodeFlag="",
        //    flowCode = "",
        //    flowSN = "",
        //    signLineDefine = "",
        //    meassge = "";

        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //為了迴避找不到主管使得按鈕生不出來，找現在本關主管
        DataTable dt;
        sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();


///*=========================================================*/
//        Dictionary<string, string> isnull=new Dictionary<string, string>();
//        if (CompID == "") return isnull;
//        if (HRdt.Rows.Count>0)flowCodeFlag = HRdt.Rows[0]["flowCodeFlag"].ToString() == "OT01" ? "0" : "1";
//        FlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, OTStartDate,FlowCaseID, flowCodeFlag,
//out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

//        if (toUserData.Count == 0 && HRdt.Rows.Count > 0)
//        {
//            toUserData.Add("SignLine", HRdt.Rows[0]["SignLine"].ToString());
//            toUserData.Add("SignIDComp", HRdt.Rows[0]["SignIDComp"].ToString());
//            toUserData.Add("SignID", AssignTo);
//            toUserData.Add("SignOrganID", HRdt.Rows[0]["SignOrganID"].ToString());
//            toUserData.Add("SignFlowOrganID", HRdt.Rows[0]["SignFlowOrganID"].ToString());
//        }

//        string SignOrganID="", SignID="",SignIDComp="";
//        if (toUserData.Count == 0)
//        {
//            toUserData["SignID"] = AssignTo;
//            toUserData["SignIDComp"] = CompID;
//            toUserData["SignOrganID"] = "";
//            toUserData["SignFlowOrganID"] = "";
//        }
//        else if (HRdt.Rows[0]["SignLine"].ToString() == "2")
//            {
//                if (EmpInfo.QueryFlowOrganData(HRdt.Rows[0]["SignFlowOrganID"].ToString(), OTStartDate, out SignOrganID, out SignID, out SignIDComp))
//                {
//                    toUserData["SignID"] = SignID;
//                    toUserData["SignIDComp"] = SignIDComp;
//                    toUserData["SignOrganID"] = "";
//                    toUserData["SignFlowOrganID"] = SignOrganID;
//                }
//            }
//            else
//            {
//                if (EmpInfo.QueryOrganData(HRdt.Rows[0]["SignCompID"].ToString(), HRdt.Rows[0]["SignOrganID"].ToString(), OTStartDate, out SignOrganID, out SignID, out SignIDComp))
//                {
//                    toUserData["SignID"] = SignID;
//                    toUserData["SignIDComp"] = SignIDComp;
//                    toUserData["SignOrganID"] = SignOrganID;
//                    toUserData["SignFlowOrganID"] = "";
//                }
//            }
        /*========================================================================*/

        //DataTable dt;
        //sb.Append(" SELECT Top 1 P.EmpID, P.LogDate, (DATEDIFF(day,P.LogDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
        //sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
        //sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        //sb.Append(" AND P.LogDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        //sb.Append(" ORDER BY MinDiff ");
        //dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //sb.Reset();
        //if (dt.Rows.Count == 0 && DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
        //{
        //    dt.Reset();
        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name, (DATEDIFF(day,O.SysDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
        //    sb.Append(" left join " + _eHRMSDB + ".dbo.Organization_History O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        //    sb.Append(" AND O.SysDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        //    sb.Append(" ORDER BY MinDiff ");
        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //    sb.Reset();
        //    if (dt.Rows.Count == 0)
        //    {
        //        dt.Reset();
        //        sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        //        sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
        //        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        //        sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        //        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //        sb.Reset();
        //    }
        //}
        //else if (dt.Rows.Count == 0)
        //{
        //    dt.Reset();
        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
        //    sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //    sb.Reset();
        //}
        //else if (dt.Rows.Count > 0)
        //{
        //    string sLogDate = dt.Rows[0]["LogDate"].ToString();
        //    dt.Reset();
        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name, (DATEDIFF(day,O.SysDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
        //    sb.Append(" left join " + _eHRMSDB + ".dbo. Organization_History O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "' and P.LogDate='" + DateTime.Parse(sLogDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
        //    sb.Append(" AND O.SysDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        //    sb.Append(" ORDER BY MinDiff ");
        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //    sb.Reset();
        //    if (dt.Rows.Count == 0)
        //    {
        //        if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
        //        {
        //            dt.Reset();
        //            sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        //            sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
        //            sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        //            sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "' and P.LogDate='" + DateTime.Parse(sLogDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
        //            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        //            sb.Reset();
        //        }
                
        //    }
        //}
        
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;

 }
    #endregion""
    /*==============================================*/
    public Dictionary<string, string> getOrganBoss_First_Close(string AssignTo, string CompID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //為了迴避找不到主管使得按鈕生不出來，找現在本關主管
        DataTable dt;
        sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }
    /// <summary>
    /// [行政組織]
    /// 1.若找到的上階Boss等於現在Boss，則迴圈往上找
    /// 2.上接部門與目前部門是同樣CompID，故無需回傳部門的CompID
    /// 3.部門Boss可以是其他公司的人員，故回傳BossCompID以做區分
    /// </summary>
    /// <param name="inCompID">輸入公司</param>
    /// <param name="inOrganID">輸入組織</param>
    /// <param name="inDate">日期，目前沒有用到</param>
    /// <param name="OrganID">目前關卡經過的OrganID，流程資料不使用</param>
    /// <param name="Boss">關卡審核主管EmpID</param>
    /// <param name="BossCompID">關卡審核主管CompID</param>
    /// <returns>是否有找到下一關主管相關資料(EmpID!=Boss的上階部門)</returns>
    public static bool QueryOrganData(string inCompID, string inOrganID, string inDate, out string OrganID, out string Boss, out string BossCompID)
    {
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        int intLoop = 0;
        string TempBoss = "", TempCompID = "", TempUpOrganID = "";
        OrganID = "";
        BossCompID = "";
        Boss = "";
        sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID, (DATEDIFF(day,SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
        sb.Append("FROM " + _eHRMSDB + ".dbo.Organization_History ");
        sb.Append("WHERE CompID='" + inCompID + "' and OrganID='" + inOrganID + "'");
        //sb.Append("WHERE InValidFlag='0' and CompID='" + inCompID + "' and OrganID='" + inOrganID + "'");
        sb.Append(" AND SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        sb.Append(" ORDER BY MinDiff ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();
        if (dt.Rows.Count == 0) 
        {
            if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
            {
                dt.Reset();
                sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID ");
                sb.Append("FROM " + _eHRMSDB + ".dbo.Organization ");
                sb.Append("WHERE CompID='" + inCompID + "' and OrganID='" + inOrganID + "'");
                //sb.Append("WHERE InValidFlag='0' and CompID='" + inCompID + "' and OrganID='" + inOrganID + "'");
                dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                sb.Reset();
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        } 

        TempBoss = dt.Rows[0]["Boss"].ToString();
        while (dt.Rows.Count > 0 && dt.Rows[0]["Boss"].ToString() == TempBoss && intLoop <= 10)
        {
            TempCompID = dt.Rows[0]["CompID"].ToString();
            TempUpOrganID = dt.Rows[0]["UpOrganID"].ToString();
            sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID, (DATEDIFF(day,SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
            sb.Append("FROM " + _eHRMSDB + ".dbo.Organization_History ");
            sb.Append("WHERE  CompID='" + TempCompID + "' and OrganID='" + TempUpOrganID + "' ");
            //sb.Append("WHERE InValidFlag='0' and CompID='" + dt.Rows[0]["CompID"] + "' and OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
            sb.Append(" AND SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
            sb.Append(" ORDER BY MinDiff ");
            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            sb.Reset();
            if (dt.Rows.Count == 0)
            {
                if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
                {
                    dt.Reset();
                    sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID ");
                    sb.Append("FROM " + _eHRMSDB + ".dbo.Organization ");
                    sb.Append("WHERE  CompID='" + TempCompID + "' and OrganID='" + TempUpOrganID + "' ");
                    //sb.Append("WHERE InValidFlag='0' and CompID='" + dt.Rows[0]["CompID"] + "' and OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
                    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                    sb.Reset();
                }
                
            };
            intLoop = intLoop + 1;
        }
        if (dt.Rows.Count > 0)
        {
            OrganID = dt.Rows[0]["OrganID"].ToString();
            Boss = dt.Rows[0]["Boss"].ToString();
            BossCompID = dt.Rows[0]["BossCompID"].ToString();
            return true;
        }
        else
        {
            OrganID = "";
            Boss = "";
            BossCompID = "";
            return false;
        }
    }

    /// <summary>
    /// [功能組織]
    /// 1.若找到的上階Boss等於現在Boss，則迴圈往上找
    /// 2.上接部門與目前部門是同樣CompID，故無需回傳部門的CompID
    /// 3.部門Boss可以是其他公司的人員，故回傳BossCompID以做區分
    /// </summary>
    /// <param name="inOrganID">輸入組織</param>
    /// <param name="inDate">日期，目前沒有用到，待Staff完整後再添加</param>
    /// <param name="OrganID">目前關卡經過的OrganID，流程資料不使用</param>
    /// <param name="Boss">關卡審核主管EmpID</param>
    /// <param name="BossCompID">關卡審核主管CompID</param>
    /// <returns>是否有找到下一關主管相關資料(EmpID!=Boss的上階部門)</returns>
    public static bool QueryFlowOrganData(string inOrganID, string inDate, out string OrganID, out string Boss,out string BossCompID)
    {
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        int intLoop = 0;
        string TempBoss = "",TempUpOrganID="";
        Boss = "";
        OrganID = "";
        BossCompID = "";
        sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID, (DATEDIFF(day,SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff  ");
        sb.Append("FROM " + _eHRMSDB + ".dbo.OrganizationFlow_History ");
        sb.Append("WHERE OrganID='" + inOrganID + "'");
        //sb.Append("WHERE InValidFlag='0' and OrganID='" + inOrganID + "'");
        sb.Append(" AND SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        sb.Append(" ORDER BY MinDiff ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        sb.Reset();
        if (dt.Rows.Count == 0)
        {
            if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
            {
                dt.Reset();
                sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID  ");
                sb.Append("FROM " + _eHRMSDB + ".dbo.OrganizationFlow ");
                sb.Append("WHERE OrganID='" + inOrganID + "'");
                //sb.Append("WHERE InValidFlag='0' and OrganID='" + inOrganID + "'");
                dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                sb.Reset();
                if (dt.Rows.Count == 0)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
        TempBoss = dt.Rows[0]["Boss"].ToString();
        while (dt.Rows.Count > 0 && dt.Rows[0]["Boss"].ToString() == TempBoss && intLoop <= 10)
        {
            TempUpOrganID = dt.Rows[0]["UpOrganID"].ToString();
            sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID, (DATEDIFF(day,SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
            sb.Append("FROM " + _eHRMSDB + ".dbo.OrganizationFlow_History ");
            sb.Append("WHERE OrganID='" + TempUpOrganID + "' ");
            //sb.Append("WHERE InValidFlag='0' and OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
            sb.Append(" AND SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
            sb.Append(" ORDER BY MinDiff ");
            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            sb.Reset();
            if (dt.Rows.Count == 0)
            {
                if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
                {
                    sb.Append("SELECT TOP 1 CompID,OrganID,Boss,BossCompID,UpOrganID ");
                    sb.Append("FROM " + _eHRMSDB + ".dbo.OrganizationFlow ");
                    sb.Append("WHERE OrganID='" + TempUpOrganID + "' ");
                    //sb.Append("WHERE InValidFlag='0' and OrganID='" + dt.Rows[0]["UpOrganID"] + "' ");
                    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                    sb.Reset();
                }                
            };
            intLoop = intLoop + 1;
        }
        if (dt.Rows.Count > 0)
        {
            Boss = dt.Rows[0]["Boss"].ToString();
            OrganID = dt.Rows[0]["OrganID"].ToString();
            BossCompID = dt.Rows[0]["BossCompID"].ToString();
            return true;
        }
        else
        {
            Boss = "";
            OrganID = "";
            BossCompID = "";
            return false;
        }
    }
    /// <summary>
    /// 撈取該人員的相關資料
    /// CompID,EmpID 輸入人員編號與所屬公司
    /// NameN,RankID 人員名稱與職等
    /// OrganID, UpOrganID, Boss, BossCompID, DeptID, PositionID, WorkTypeID 行政組織資料
    ///  FlowOrganID,  FlowDeptID,  FlowUpOrganID,  FlowBoss, FlowBossCompID,  BusinessType,  EmpFlowRemarkID 功能組織資料
    ///  註：若流程需要持續向上送簽，送簽是依照組織向上尋找不是主管，
    ///  可將CompID與OrganID放入QueryBossData(行政)QueryBossFlowData(功能)查詢(下一關)的部門與部門主管
    /// </summary>
    public static bool QueryEmpData
        (string CompID, string EmpID, string inDate, out string NameN, out string RankID, out string TitleID, 
        out string OrganID, out string UpOrganID, out string Boss,out string BossCompID, out string DeptID, out string PositionID, out string WorkTypeID, 
        out string FlowOrganID, out string FlowDeptID, out string FlowUpOrganID, out string FlowBoss,out string FlowBossCompID, out string BusinessType, out string EmpFlowRemarkID)
    {
        //Emp資訊
        NameN = "";
        RankID = "";
        TitleID = "";
        //行政
        OrganID = "";
        UpOrganID = "";
        Boss = "";
        BossCompID = "";
        DeptID = "";
        PositionID = "";
        WorkTypeID = "";
        //功能
        FlowOrganID = "";
        FlowUpOrganID = "";
        FlowBoss = "";
        FlowBossCompID = "";
        FlowDeptID = "";
        BusinessType = "";
        EmpFlowRemarkID = "";

        bool result = false;
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt, dt2, dt3;
        sb.Reset();
        sb.Append(" SELECT Top 1 P.EmpID, P.LogDate, (DATEDIFF(day,P.LogDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
        sb.Append(" WHERE P.EmpID='" + EmpID + "' and P.CompID='" + CompID + "'");
        sb.Append(" AND P.LogDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
        sb.Append(" ORDER BY MinDiff ");
        try
        {
            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            sb.Reset();
            if (dt.Rows.Count == 0 && DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
            {
                dt.Reset();
                sb.Append("SELECT TOP 1 P.NameN,P.RankID,P.TitleID,P.OrganID,P.DeptID"); //p
                sb.Append(",EP.PositionID,EW.WorkTypeID"); //EP/EW
                sb.Append(",EF.OrganID as FlowOrganID,EF.EmpFlowRemarkID "); //EF
                sb.Append("FROM " + _eHRMSDB + ".dbo.Personal P ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpPosition EP on EP.EmpID=P.EmpID and EP.CompID=P.CompID ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpWorkType EW on EW.EmpID=P.EmpID and EW.CompID=P.CompID ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpFlow EF on EF.CompID=P.CompID and EF.EmpID=P.EmpID ");
                sb.Append("WHERE P.CompID='" + CompID + "' and P.EmpID='" + EmpID + "' ");
                dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                sb.Reset();
                if (dt.Rows.Count > 0)
                {
                    //Emp資訊
                    NameN = dt.Rows[0]["NameN"].ToString();
                    RankID = dt.Rows[0]["RankID"].ToString();
                    TitleID = dt.Rows[0]["TitleID"].ToString();
                    //行政
                    OrganID = dt.Rows[0]["OrganID"].ToString();
                    DeptID = dt.Rows[0]["DeptID"].ToString();
                    PositionID = dt.Rows[0]["PositionID"].ToString();
                    WorkTypeID = dt.Rows[0]["WorkTypeID"].ToString();
                    //功能
                    FlowOrganID = dt.Rows[0]["FlowOrganID"].ToString();
                    EmpFlowRemarkID = dt.Rows[0]["EmpFlowRemarkID"].ToString();
                    result = true;
                }
                else
                {
                    return false;
                }
            }
            else if (dt.Rows.Count == 0)
            {
                return false;
            }
            else if (dt.Rows.Count > 0)
            {
                string sLogDate = dt.Rows[0]["LogDate"].ToString();
                dt.Reset();
                sb.Append("SELECT TOP 1 P.NameN,P.RankID,P.OrganID,P.DeptID"); //p
                sb.Append(",EP.PositionID,EW.WorkTypeID"); //EP/EW
                sb.Append(",EF.OrganID as FlowOrganID,EF.EmpFlowRemarkID "); //EF
                sb.Append(",TT.TitleID "); //TT
                sb.Append("FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpPosition EP on EP.EmpID=P.EmpID and EP.CompID=P.CompID ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpWorkType EW on EW.EmpID=P.EmpID and EW.CompID=P.CompID ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.EmpFlow EF on EF.CompID=P.CompID and EF.EmpID=P.EmpID ");
                sb.Append("LEFT JOIN  " + _eHRMSDB + ".dbo.Title TT on TT.CompID=P.CompID and TT.RankID=P.RankID ");
                sb.Append("WHERE P.CompID='" + CompID + "' and P.EmpID='" + EmpID + "' and P.LogDate='" + DateTime.Parse(sLogDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
                dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                sb.Reset();
                if (dt.Rows.Count > 0)
                {
                    //Emp資訊
                    NameN = dt.Rows[0]["NameN"].ToString();
                    RankID = dt.Rows[0]["RankID"].ToString();
                    TitleID = dt.Rows[0]["TitleID"].ToString();
                    //行政
                    OrganID = dt.Rows[0]["OrganID"].ToString();
                    DeptID = dt.Rows[0]["DeptID"].ToString();
                    PositionID = dt.Rows[0]["PositionID"].ToString();
                    WorkTypeID = dt.Rows[0]["WorkTypeID"].ToString();
                    //功能
                    FlowOrganID = dt.Rows[0]["FlowOrganID"].ToString();
                    EmpFlowRemarkID = dt.Rows[0]["EmpFlowRemarkID"].ToString();
                    result = true;
                }
            }
        }
        catch (Exception)
        {           
            throw;
        }

        //行政
        if (!string.IsNullOrEmpty(OrganID))
        {
            sb.Append(" SELECT TOP 1 ");
            sb.Append(" Org.UpOrganID,Org.Boss,Org.BossCompID,(DATEDIFF(day,Org.SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff "); //Org
            sb.Append(" FROM " + _eHRMSDB + ".dbo.Organization_History Org ");
            sb.Append(" WHERE Org.OrganID='" + OrganID + "' ");
            sb.Append(" AND Org.SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
            sb.Append(" ORDER BY MinDiff ");
            try
            {
                dt2 = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

                sb.Reset();
                if (dt2.Rows.Count == 0 && DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
                {
                    dt2.Reset();
                    sb.Append(" SELECT TOP 1 ");
                    sb.Append(" Org.UpOrganID,Org.Boss,Org.BossCompID "); //Org
                    sb.Append(" FROM " + _eHRMSDB + ".dbo.Organization Org ");
                    sb.Append(" WHERE Org.OrganID='" + OrganID + "' ");
                    dt2 = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                    sb.Reset();
                }
                if (dt2.Rows.Count > 0)
                {
                    //行政
                    UpOrganID = dt2.Rows[0]["UpOrganID"].ToString();
                    Boss = dt2.Rows[0]["Boss"].ToString();
                    BossCompID = dt2.Rows[0]["BossCompID"].ToString();
                }
            }
            catch (Exception)
            {

            }
        }
        //功能
        if (!string.IsNullOrEmpty(FlowOrganID))
        {
            sb.Append(" SELECT TOP 1 ");
            sb.Append(" OrF.UpOrganID as FlowUpOrganID,OrF.Boss as FlowBoss,OrF.BossCompID as FlowBossCompID,OrF.DeptID as FlowDeptID,OrF.BusinessType,(DATEDIFF(day,OrF.SysDate,'" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff "); //Org
            sb.Append(" FROM " + _eHRMSDB + ".dbo.OrganizationFlow_History OrF ");
            sb.Append(" WHERE OrF.OrganID='" + FlowOrganID + "' ");
            sb.Append(" AND OrF.SysDate = '" + DateTime.Parse(inDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
            sb.Append(" ORDER BY MinDiff ");
            try
            {
                dt3 = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                sb.Reset();
                if (dt3.Rows.Count == 0 && DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
                {
                    dt3.Reset();
                    sb.Append(" SELECT TOP 1 ");
                    sb.Append(" OrF.UpOrganID as FlowUpOrganID,OrF.Boss as FlowBoss,OrF.BossCompID as FlowBossCompID,OrF.DeptID as FlowDeptID,OrF.BusinessType "); //OrF
                    sb.Append(" FROM " + _eHRMSDB + ".dbo.OrganizationFlow OrF ");
                    sb.Append(" WHERE OrF.OrganID='" + FlowOrganID + "' ");
                    dt3 = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
                    sb.Reset();
                }
                if (dt3.Rows.Count > 0)
                {
                    //功能
                    FlowUpOrganID = dt3.Rows[0]["FlowUpOrganID"].ToString();
                    FlowBoss = dt3.Rows[0]["FlowBoss"].ToString();
                    FlowBossCompID = dt3.Rows[0]["FlowBossCompID"].ToString();
                    FlowDeptID = dt3.Rows[0]["FlowDeptID"].ToString();
                    BusinessType = dt3.Rows[0]["BusinessType"].ToString();
                }
            }
            catch (Exception)
            {
                
            }
        }
        
        return result;
    }
    #endregion
}