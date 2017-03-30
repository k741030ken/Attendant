using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Data;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Work.Properties;
using System.Data.Common;

public partial class Overtime_OvertimeCustVerify : SecurePage
{
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    public static string _HRDB = Util.getAppSetting("app://HRDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");

    #region"網頁傳值"

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

    public string _ReturnUrl
    {
        get
        {
            if (ViewState["_ReturnUrl"] == null)
            {
                ViewState["_ReturnUrl"] = "";
            }
            return (string)(ViewState["_ReturnUrl"]);
        }
        set
        {
            ViewState["_ReturnUrl"] = value;
        }
    }
    #endregion"網頁傳值"
    /// <summary>
    /// 1.重新產生按鈕，等榮威改版後再做修改。
    /// 2.目前是待辦畫面產生兩個，審核畫面清理按鈕後，再將全部按鈕都生出來。
    /// 3.因為部分判斷移至待辦畫面，所以其實此Function可以用其他更好的作法取代了。
    /// </summary>
    /// <param name="AssignTo"></param>
    private void ClearBtn(string AssignTo)
    {
        FlowExpress tbFlow = new FlowExpress(_CurrFlowID);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" update " + tbFlow.FlowCustDB + "FlowOpenLog ");
        sb.Append(" set FlowStepBatchEnabled=null ");
        sb.Append(" ,FlowStepOpinion=null ");
        sb.Append(" ,FlowStepBtnInfoCultureCode=null ");
        sb.Append(" ,FlowStepBtnInfoJSON=null ");
        sb.Append(" where AssignTo='" + AssignTo + "' ");
        db.ExecuteNonQuery(sb.BuildCommand());
        //Session["btnVisible"] 1：(顯示核准，隱藏覆核) , 2：(顯示覆核，隱藏核准) , 12：(都不隱藏)
        Session["btnVisible"] = "12";
        FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, AssignTo, _DBName.Split(','), null, false, "", "");
        Session["btnVisible"] = "1";
    }
    /// <summary>
    /// RankID19，因為不是登入者而是要各筆加班人的資料，所以獨立出一個Function，方便多筆審核時使用。
    /// </summary>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="UpEmpRankID">從Para撈取的EmpRankID(Mapping前)</param>
    /// <returns>是否大於19</returns>
    private bool boolUpEmpRankID(string AD, string FlowCaseID, string UpEmpRankID)
    {
        if (UpEmpRankID == "") return false;
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        string MapEmpRankID, OTEmpID;
        sb.Append(" SELECT Top 1 isnull(P.CompID,'') as CompID,isnull(P.RankID,'')as RankID FROM " + ADTable(AD) + " OT ");
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
    /// 將傳入的RankID去Mapping，以供作RankID大小檢核
    /// </summary>
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
        //return db.ExecuteScalar(CommandType.Text, sb.ToString()).ToString();
    }

    /// <summary>
    /// 結案後HROverTimeLog的Flag解除
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="sb">回傳SQL語法，統一給TryCatchIsFlowVerify執行</param>
    private void CloseHROverTimeLog(string FlowCaseID, string AD, ref CommandHelper sb)
    {
        //AD = ADTable(AD);  //HROverTimeLogAD不要加
        DataTable dt = HROverTimeLogAD(FlowCaseID, AD);
        FlowUtility.ChangeFlowFlag(FlowCaseID, dt.Rows[0]["FlowCode"].ToString(), dt.Rows[0]["FlowSN"].ToString(), dt.Rows[0]["SignIDComp"].ToString(), dt.Rows[0]["SignID"].ToString(), "0", ref sb);
    }

    #region Judy的
    //Judy在Aattendant撈取StartDate會有問題改成EndDate 去掉CompID
    public DataTable QueryData(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Append("SELECT " + strColumn + " FROM " + strTable);
        sb.Append(" WHERE 1=1 ");
        sb.Append(strWhere);
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt;
    }
    public int QuerySeq(string strTable, string strEmpID, string date)//Seq
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        int OTSeq = 0;
        DataTable dt = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, " AND OTEmpID='" + strEmpID + "' AND OTEndDate='" + date + "'");
        if (dt.Rows.Count == 0)
        {
            OTSeq = 1;
        }
        else
        {
            OTSeq = Convert.ToInt32(dt.Rows[0]["MAXOTSeq"].ToString() == "" ? "0" : dt.Rows[0]["MAXOTSeq"].ToString()) + 1;
        }
        return OTSeq;
    }
    #endregion Judy的

    /// <summary>
    ///方便將A,D或1,2轉換成Table名稱OverTimeAdvanc,OverTimeDeclaration
    /// </summary>
    private string ADTable(string AD)
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
    /// <summary>
    ///Judy切割出來多筆審核撈取FlowLogID
    /// </summary>
    private string getFlowLogID(string FlowCaseID, string AD)
    {
        FlowExpress tbFlow = new FlowExpress(_CurrFlowID, _CurrFlowLogID, false);
        AD = ADTable(AD);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.AppendStatement(" SELECT top 1 AL.FlowLogID,* FROM " + AD + " OT  ");
        sb.Append("  LEFT JOIN " + tbFlow.FlowCustDB + "FlowFullLog AL ON OT.FlowCaseID=AL.FlowCaseID  ");
        sb.Append(" WHERE OT.FlowCaseID ='" + FlowCaseID + "' ");
        sb.Append(" Order by AL.FlowLogID desc ");
        return db.ExecuteScalar(CommandType.Text, sb.ToString()).ToString();
    }

    /// <summary>
    /// 將EmpID組成Dictionary<EmpID,EmpName>方便丟給oAssDic，AssignToName的[組織-人名]可由這裡更動
    /// </summary>
    /// <param name="EmpID">下一關主管</param>
    /// <param name="CompID">下一關主管公司</param>
    /// <returns>for流程oAssDic用</returns>
    public Dictionary<string, string> getEmpID_Name_Dictionary(string EmpID, string CompID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        sb.Append(" SELECT DISTINCT P.EmpID,O.OrganName+'-'+P.Name as Name ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".dbo. Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        sb.Append(" WHERE P.EmpID='" + EmpID + "' and P.CompID='" + CompID + "' ");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }

    /// <summary>
    /// 用FlowCase撈取A或D的資料
    /// </summary>
    private DataTable FlowCaseToAD(string FlowCase, string AD)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string Table = ADTable(AD);
        sb.AppendStatement(" SELECT O1.OTRegisterComp,O1.OTCompID,O1.OTFormNO,O1.OTEmpID,P.Name ");
        sb.Append(" ,O1.OTStartDate+'~'+isnull(O2.OTEndDate,O1.OTEndDate) as OTDate");
        sb.Append(" ,O1.OTStartDate,isnull(O2.OTEndDate,O1.OTEndDate) as OTEndDate");
        sb.Append(" ,O1.OTStartTime,isnull(O2.OTEndTime,O1.OTEndTime) as OTEndTime");
        sb.Append("  FROM " + Table + " O1");
        sb.Append("  left join " + Table + " O2   on  O1.OTTxnID=O2.OTTxnID and O2.OTSeqNo='2'  ");
        sb.Append("  left join " + _eHRMSDB + ".dbo.Personal P on P.EmpID=O1.OTEmpID and P.CompID=O1.OTCompID");
        sb.Append("  where O1.OTSeqNo='1'and O1.FlowCaseID='" + FlowCase + "' ");

        return db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
    }
    //
    /// <summary>
    /// 更新AD的Table狀態(2:送審 3:核准 4:駁回)
    /// </summary>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="OTStatus">狀態</param>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="sb">回傳SQL語法</param>
    private void UpdateAorD(string AD, string OTStatus, string FlowCaseID, ref CommandHelper sb)
    {
        AD = ADTable(AD);
        sb.Append(" UPDATE " + AD + " SET OTStatus='" + OTStatus + "',");
        //審核人員
        sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "' ");
        //sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "', ");
        ////最後異動人員
        //sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        //sb.Append(" LastChgID='" + UserInfo.getUserInfo().UserID + "', ");
        //sb.Append(" LastChgComp='" + UserInfo.getUserInfo().CompID + "' ");
        sb.Append(" WHERE FlowCaseID='" + FlowCaseID + "' ; ");
    }

    /// <summary>
    /// 給FlowLogID加1動作
    /// </summary>
    private string FlowLogIDadd(string FlowLogID)
    {
        return FlowLogID.Split('.')[0] + "." + FlowLogID.Split('.')[1] + "." + Convert.ToString(int.Parse(FlowLogID.Split('.')[2]) + 1).PadLeft(5, '0');
    }

    #region "新增MailLog"
    /// <summary>
    /// 取得收件人相關資料
    /// </summary>
    /// <returns>回傳DataTable[EMail],[Name]</returns>
    private DataTable getMailInfo(string CompID, string EmpID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" select distinct isnull(C.EMail,'') as EMail,P.Name from " + _eHRMSDB + ".[dbo].Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".[dbo].[Communication] C on C.IDNo=P.IDNo ");
        sb.Append(" where P.CompID='" + CompID + "' and P.EmpID='" + EmpID + "' ");
        return db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
    }

    /// <summary>
    /// 當收件人Mail是空的，通知預設的HR人員
    /// </summary>
    /// <param name="Subject">主旨</param>
    /// <param name="Content">內容</param>
    /// <param name="CompID">收件人公司</param>
    /// <param name="EmpID">收件人員編</param>
    /// <returns>是否成功</returns>
    private bool MailisNull(string Subject, string Content, string CompID, string EmpID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        string Title = "";
        string Mail = "";

        Subject = "FW:" + Subject;

        sb.Append("select P.CompID,P.EmpID,P.Name,C.EMail ");
        sb.Append(" from " + _HRDB + ".[dbo].SC_UserGroup UG ");
        sb.Append("left join " + _eHRMSDB + ".[dbo].Personal P on P.EmpID=UG.UserID and P.CompID=UG.CompID ");
        sb.Append("left join " + _eHRMSDB + ".[dbo].Communication C on C.IDNo=P.IDNo ");
        sb.Append("where UG.CompID='SPHBK1' and UG.GroupID='Adm-OT'");
        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
        if (dt.Rows.Count == 0) return true;
        sb.Reset();
        DataRow[] dr = dt.Select();
        Title = EmpID + "-" + getMailInfo(CompID, EmpID).Rows[0]["Name"].ToString();
        Content = "||BM@QuitMailContent99||" + Content;
        foreach (DataRow row in dt.Rows)
        {
            Mail = row["EMail"].ToString();
            InsertMailLogCommand("人力資源處", row["CompID"].ToString(), row["EmpID"].ToString(), Mail, Subject, Content, false, ref sb);
        }
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand());
        }
        catch (Exception ex)
        {
            Util.MsgBox("人力資源處E-Mail寄送失敗-" + ex);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 寄信範本套用前區別狀態
    /// </summary>
    /// <param name="FlowCase">依FlowCaseID找尋相關資料</param>
    /// <param name="OTStatus">(2、3核准) (4駁回)</param>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="nextAssignID">下一關待辦人ID</param>
    /// <param name="nextAssignCompID">下一關待辦人CompID</param>
    /// <param name="isLastFlow">是否最後一關結案(判斷是否送下一關待辦人)</param>
    /// <returns>是否成功</returns>
    private bool MailLogContent(string FlowCase, string OTStatus, string AD, string nextAssignID, string nextAssignCompID, bool isLastFlow)
    {
        //DataTable ADBatchdt = FlowCaseToAD(FlowCase, AD);
        DataTable LastAssigndt = HROverTimeLogAD(FlowCase, AD);
        DataTable OTdt = FlowCaseToAD(FlowCase, AD);
        DataTable Maildt;
        string OTEmpID = OTdt.Rows[0]["OTEmpID"].ToString();
        string OTCompID = OTdt.Rows[0]["OTCompID"].ToString();
        string AssignID = LastAssigndt.Rows[0]["SignID"].ToString();
        string AssignCompID = LastAssigndt.Rows[0]["SignIDComp"].ToString();
        string FromDT = "";
        string Subject1 = "", Subject2 = "", Subject3 = "";
        string Content1 = "", Content2 = "", Content3 = "";
        string Title1 = "", Title2 = "", Title3 = "";
        string Mail1 = "", Mail2 = "", Mail3 = "";
        //string Name1 = "", Name2 = "", Name3 = "";
        //DataTable Maildt; //Maildt = getMailInfo(OTEmpID);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        CommandHelper sb2 = db.CreateCommandHelper();
        switch (AD)
        {
            case "A":
            case "0":
                FromDT = "申請";
                break;
            case "D":
            case "1":
                FromDT = "申報";
                break;
        }
        if (OTStatus == "2" || OTStatus == "3")//核准
        {
            if (isLastFlow) //最後一關結案
            {
                Subject1 = "加班單流程案件結案通知"; //(主管)
                Title1 = "您簽核的加班單已經完成『結案』!";
                Maildt = getMailInfo(AssignCompID, AssignID);
                if (Maildt.Rows.Count > 0)
                {
                    Mail1 = Maildt.Rows[0]["EMail"].ToString();
                    //Name1 = Maildt.Rows[0]["Name"].ToString();
                }
                Subject2 = "加班單流程案件核准通知";//(申請人)
                Title2 = "您" + FromDT + "的加班單已『核准』!";
                Maildt = getMailInfo(OTCompID, OTEmpID);
                if (Maildt.Rows.Count > 0)
                {
                    Mail2 = Maildt.Rows[0]["EMail"].ToString();
                    //Name2 = Maildt.Rows[0]["Name"].ToString();
                }
            }
            else //非最後一關+待辦
            {
                Subject1 = "加班單流程案件簽核通知";//(主管)
                Title1 = "您簽核的加班單已送簽至下一關主管簽核";
                Maildt = getMailInfo(AssignCompID, AssignID);
                if (Maildt.Rows.Count > 0)
                {
                    Mail1 = Maildt.Rows[0]["EMail"].ToString();
                    //Name1 = Maildt.Rows[0]["Name"].ToString();
                }

                Subject2 = "加班單流程案件核准通知";//(申請人)
                Title2 = "您" + FromDT + "的加班單已送簽下一關主管簽核";
                Maildt = getMailInfo(OTCompID, OTEmpID);
                if (Maildt.Rows.Count > 0)
                {
                    Mail2 = Maildt.Rows[0]["EMail"].ToString();
                    //Name2 = Maildt.Rows[0]["Name"].ToString();
                }

                Subject3 = "加班單流程待辦案件通知"; //(主管)
                Maildt = getMailInfo(nextAssignCompID, nextAssignID);
                if (Maildt.Rows.Count > 0)
                {
                    Mail3 = Maildt.Rows[0]["EMail"].ToString();
                    //Name3 = Maildt.Rows[0]["Name"].ToString();
                    Title3 = Maildt.Rows[0]["Name"].ToString(); //代辦第一個欄位直接放BossName
                }
            }
        }
        else if (OTStatus == "4")//駁回
        {
            Subject1 = "加班單流程案件駁回通知";//(主管)
            Title1 = "您簽核的加班單已完成『駁回』!";
            Maildt = getMailInfo(AssignCompID, AssignID);
            if (Maildt.Rows.Count > 0)
            {
                Mail1 = Maildt.Rows[0]["EMail"].ToString();
                //Name1 = Maildt.Rows[0]["Name"].ToString();
            }

            Subject2 = "加班單流程案件駁回通知";//(申請人)
            Title2 = "您" + FromDT + "的加班單已被『駁回』!";
            Maildt = getMailInfo(OTCompID, OTEmpID);
            if (Maildt.Rows.Count > 0)
            {
                Mail2 = Maildt.Rows[0]["EMail"].ToString();
                //Name2 = Maildt.Rows[0]["Name"].ToString();
            }
        }
        else
        {
            Subject1 = "";
            Title1 = "";
            Mail1 = "";
            Subject2 = "";
            Title2 = "";
            Mail2 = "";
            Subject3 = "";
            Title3 = "";
            Mail3 = "";
            //Name1 = ""; Name2 = ""; Name3 = "";
        }
        //測試用
        //Subject1 = "(leo)" + Subject1;
        //Subject2 = "(leo)" + Subject2;
        //Subject3 = "(leo)" + Subject3;

        if (OTdt.Rows.Count > 0)
        {
            //主管
            Content1 =
                    "NoticeOverTime" +
                    "||BM@QuitMailContent1||" + Title1 +
                    "||BM@QuitMailContent2||" + OTdt.Rows[0]["OTFormNo"].ToString() +
                    "||BM@QuitMailContent3||" + OTdt.Rows[0]["OTEmpID"].ToString() +
                    "||BM@QuitMailContent4||" + OTdt.Rows[0]["Name"].ToString() +
                    "||BM@QuitMailContent5||" + OTdt.Rows[0]["OTDate"].ToString() +
                    "||BM@QuitMailContent6||" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + " ：" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                    "||BM@QuitMailContent7||" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            if (Mail1 != "")
            {
                InsertMailLogCommand("人力資源處", OTdt.Rows[0]["OTCompID"].ToString(), OTdt.Rows[0]["OTEmpID"].ToString(), Mail1, Subject1, Content1, false, ref sb2);
                try
                {
                    db.ExecuteNonQuery(sb2.BuildCommand());
                    sb2.Reset();
                }
                catch (Exception ex)
                {
                    Util.MsgBox("加班人E-Mail寄送失敗-" + ex);
                    return false;
                }
            }
            else
            {
                MailisNull(Subject1, Content1, OTCompID, OTEmpID);
            }

            //申請人
            Content2 =
                    "NoticeOverTime" +
                    "||BM@QuitMailContent1||" + Title2 +
                    "||BM@QuitMailContent2||" + OTdt.Rows[0]["OTFormNo"].ToString() +
                    "||BM@QuitMailContent3||" + OTdt.Rows[0]["OTEmpID"].ToString() +
                    "||BM@QuitMailContent4||" + OTdt.Rows[0]["Name"].ToString() +
                    "||BM@QuitMailContent5||" + OTdt.Rows[0]["OTDate"].ToString() +
                    "||BM@QuitMailContent6||" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + " ：" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                    "||BM@QuitMailContent7||" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            if (Mail2 != "")
            {
                InsertMailLogCommand("人力資源處", OTdt.Rows[0]["OTCompID"].ToString(), OTdt.Rows[0]["OTEmpID"].ToString(), Mail2, Subject2, Content2, false, ref sb2);
                try
                {
                    db.ExecuteNonQuery(sb2.BuildCommand());
                    sb2.Reset();
                }
                catch (Exception ex)
                {
                    Util.MsgBox("審核人E-Mail寄送失敗-" + ex);
                    return false;
                }
            }
            else
            {
                MailisNull(Subject2, Content2, AssignCompID, AssignID);
            }
            //代辦專用
            if (OTStatus == "2" || OTStatus == "3")//核准
            {
                if (!isLastFlow && nextAssignID != "") //最後一關結案
                {
                    Content3 =
                            "OverTimeTodoList" +
                            "||BM@QuitMailContent1||" + Title3 +
                            "||BM@QuitMailContent2||" + OTdt.Rows[0]["OTFormNo"].ToString() +
                            "||BM@QuitMailContent3||" + OTdt.Rows[0]["OTEmpID"].ToString() +
                            "||BM@QuitMailContent4||" + OTdt.Rows[0]["Name"].ToString() +
                            "||BM@QuitMailContent5||" + OTdt.Rows[0]["OTDate"].ToString() +
                            "||BM@QuitMailContent6||" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + " ：" + OTdt.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                            "||BM@QuitMailContent7||" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + OTdt.Rows[0]["OTEndTime"].ToString().Substring(2, 2) +
                            "||BM@QuitMailContent8||" + "Total";
                    if (Mail3 != "")
                    {
                        InsertMailLogCommand("人力資源處", OTdt.Rows[0]["OTCompID"].ToString(), OTdt.Rows[0]["OTEmpID"].ToString(), Mail3, Subject3, Content3, false, ref sb2);
                        try
                        {
                            db.ExecuteNonQuery(sb2.BuildCommand());
                        }
                        catch (Exception ex)
                        {
                            Util.MsgBox("待辦人E-Mail寄送失敗-" + ex);
                            return false;
                        }
                    }
                    else
                    {
                        MailisNull(Subject3, Content3, nextAssignCompID, nextAssignID);
                    }
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Judy給的寄信範本
    /// </summary>
    public static void InsertMailLogCommand(string Sender, string AcceptorCompID, string Acceptor, string EMail, string Subject_1, string Content_1, bool isResetCommand, ref CommandHelper sb)
    {
        Aattendant at = new Aattendant();
        var dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        int seq = 1;
        DataTable dt = at.QueryData("ISNULL(MAX(Seq),0) AS Seq", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[MailLog]", " AND CreateTime='" + dateNow + "'");
        if (dt.Rows.Count > 0)
        {
            seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()) + 1;
        }

        if (isResetCommand)
        {
            sb.Reset();
        }
        sb.AppendStatement(" INSERT INTO " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[MailLog] ");
        sb.Append(" ( ");
        sb.Append(" CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content");
        sb.Append(" ) ");
        sb.Append(" VALUES ");
        sb.Append(" ('" + dateNow + "','" + seq.ToString() + "','" + Sender + "','" + AcceptorCompID + "','" + Acceptor + "','" + EMail + "','" + Subject_1 + "','" + Content_1 + "' ");
        sb.Append(" ); ");
    }
    #endregion "新增HROverTimeLog"

    #region "HROverTimeLog相關資料"

    /// <summary>
    /// 1.當共用檔使用有問題時，須額外撈取現在是否最後一關時使用。
    /// 2.只有最基本的流程走法不會用到，其他情況幾乎看這。
    /// </summary>
    /// <param name="OTCompID">加班人公司</param>
    /// <param name="flowCaseID">FlowCaseID</param>
    /// <param name="otModel">A/D</param>
    /// <returns>現在是否為最後一關</returns>
    private bool isLastFlowNow(string OTCompID, string flowCaseID, string otModel)
    {
        DataRow retrunRow;
        string message = "";
        try
        {
            if (!FlowUtility.QueryHRFlowEngineDatas_Now(OTCompID, flowCaseID, otModel, out retrunRow, out message))
                return false;
            else if (retrunRow.Table.Rows.Count > 0)
            {
                string FlowEndFlag = retrunRow["FlowEndFlag"].ToString();
                return FlowEndFlag == "1" ? true : false;
            }
            else
                return false;
        }
        catch (Exception ex)
        {
            return true;
        }
    }

    /// <summary>
    /// 修改現在關卡在HROrverTimeLog的狀態，比照Ken的 0：申請送出, (1：簽核中), (2：同意), (3：駁回), 4：表單取消, 5：退闗
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="FlowStatus">狀態</param>
    private void UpdateHROrverTimeLog(string FlowCaseID, string FlowStatus, ref  CommandHelper sb)
    {
        sb.Append("  UPDATE HROverTimeLog set FlowStatus='" + FlowStatus + "'  where FlowCaseID='" + FlowCaseID + "'  and Seq=  (select Top 1 Seq from HROverTimeLog  where FlowCaseID='" + FlowCaseID + "'  order by Seq desc) ; ");
    }
    //字串+1用
    private string addone(string one)
    {
        return Convert.ToString(int.Parse(one) + 1);
    }

    /// <summary>
    /// 依照事先事後找尋最近一筆(行政或功能)線資料，依照AD判別
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="AD">A事先/D事後</param>
    /// <returns>全部欄位</returns>
    private DataTable HROverTimeLogAD(string FlowCaseID, string AD)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'   and OTMode='" + AD + "' order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }

    /// <summary>
    /// notSP:true 現在是改派人員，需要重複向上送時，撈取最近一筆非特殊人員(行政、功能或HR?)線資料
    /// notSP:false 直接找最新一筆(現在)關卡資訊
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="notSP">現在關卡是否為指定人員</param>
    /// <returns>DataTable[FlowCaseID],[FlowLogBatNo],[FlowLogID],[Seq],[OTMode],[ApplyID],[OTEmpID],[EmpOrganID],[EmpFlowOrganID],[SignOrganID],[SignFlowOrganID],[FlowCode],[FlowSN],[FlowSeq],[SignLine],[SignIDComp],[SignID],[SignTime],[FlowStatus],[ReAssignFlag],[ReAssignComp],[ReAssignEmpID],[Remark],[LastChgComp],[LastChgID],[LastChgDate]</returns>
    private DataTable HROverTimeLog(string FlowCaseID, bool notSP)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        if (notSP)
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2','4') order by Seq desc");
        else
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }
    /// <summary>
    ///遇到什麼奇怪狀況沒辦法知道現在關卡實際簽核人(非代理人)時，使用
    /// </summary>
    private string getLastAssignTo(string FlowCaseID)
    {
        return HROverTimeLog(FlowCaseID, false).Rows[0]["SignID"].ToString();
    }
    #endregion"HROverTime"

    /// <summary>
    /// 從Para取得的dt中，撈取該公司Valid與Emp的RankID
    /// </summary>
    /// <param name="dt">完整的Para資料表</param>
    /// <param name="CompID">欲找尋RankMapping的公司</param>
    /// <param name="RankType">ValidRankID與EmpRankID欄位名稱擇一</param>
    /// <returns>欄位內容</returns>
    private string RankPara(DataTable dt, string CompID, string RankType)
    {
        string RankID = "0";
        Aattendant a = new Aattendant();
        try
        {
            if (dt.Select("CompID='" + CompID + "'").Count() > 0)
            {
                RankID = a.Json2DataTable(dt.Select("CompID='" + CompID + "'").CopyToDataTable().Rows[0]["Para"].ToString()).Rows[0][RankType].ToString();
                return RankID;
            }
        }catch(Exception ex)
        {
            return RankID;
        }
        return RankID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //代辦畫面傳來的多筆審核資料
        DataTable dtOverTimeAdvance = (DataTable)Session["dtOverTimeAdvance"];
        DataTable dtOverTimeDeclaration = (DataTable)Session["dtOverTimeDeclaration"];

        //永豐流程相關資料
        FlowExpress oFlow = new FlowExpress(Aattendant._AattendantFlowID, Request["FlowLogID"], true);

        //錯誤訊息儲存
        string ErrMsg = "";

        //流程引擎Function接收用
        Dictionary<string, string> toUserData;
        bool isLastFlow, nextIsLastFlow;
        string flowCodeFlag = oFlow.FlowCurrStepID == "A20" ? "0" : "1",
            otModel = oFlow.FlowCurrStepID == "A20" ? "A" : "D",
            flowCode = "",
            flowSN = "",
            signLineDefine = "",
            meassge = "";

        //EmpInfo.QueryOrganData || EmpInfo.QueryFlowOrganData 使用
        string SignOrganID = "", SignID = "", SignIDComp = "";

        if (!IsPostBack)
        {
            if (Session["FlowVerifyInfo"] != null)
            {
                #region"變數"
                //DB使用
                DbHelper db = new DbHelper(Aattendant._AattendantDBName);
                CommandHelper sb = db.CreateCommandHelper();
                DbConnection cn = db.OpenConnection();
                DbTransaction tx = cn.BeginTransaction();

                //該筆加班單永豐流程資料(單筆)
                FlowExpress tbFlow = new FlowExpress(_CurrFlowID, _CurrFlowLogID, false);

                //預設下關待辦人(會變，沒變代表沒找到相關資料)
                Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
                Session["FlowVerifyInfo"] = null;
                Dictionary<string, string> oAssDic = Util.getDictionary(oVerifyInfo["FlowStepAssignToList"]);

                //共用檔
                Aattendant a = new Aattendant();

                //撈取該筆加班單相關資料
                DataTable ADdt = FlowCaseToAD(oFlow.FlowCaseID, otModel);

                //RankID判斷
                string ValidRankID = "",
                          EmpRankID = "";
                bool IsUpValidRankID = true,
                        IsUpEmpRankID = false;
                //登入者RankID
                string UserRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().RankID);

                //Para撈取參數設定
                DataTable Paradt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];

                //16        //Jason提取所需字串
                ValidRankID = RankPara(Paradt, UserInfo.getUserInfo().CompID, "ValidRankID");
                //RankMapping
                ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, ValidRankID);
                //若無資料弄成負數
                if (ValidRankID == "") ValidRankID = "-1";
                //若登入者無資料，預設大於等於ValidRankID
                if (UserRankID == "") UserRankID = ValidRankID;
                //是否大於
                IsUpValidRankID = Convert.ToInt32(UserRankID) >= int.Parse(ValidRankID) ? true : false;

                //19
                EmpRankID = RankPara(Paradt, ADdt.Rows[0]["OTCompID"].ToString(), "EmpRankID");
                //若無資料弄成最大數100?
                if (EmpRankID == "") EmpRankID = "100";
                //因為多筆審核時，每筆加班人與加班公司都不同，要多次找尋EmpRankID，省麻煩所以包成一個Function
                IsUpEmpRankID = boolUpEmpRankID("D", oFlow.FlowCaseID, EmpRankID);

                #endregion"變數"
                switch (oVerifyInfo["FlowStepBtnID"].ToString())
                {
                    //應急用，其實代辦畫面已經把檢核動作處裡的差不多了，可以直接依按鈕做審核，
                    //以防萬一還是都用btnApprove進去再做一次檢核。
                    case "btnClose":
                        oVerifyInfo["FlowStepBtnID"] = "btnApprove";
                        goto case "btnApprove";
                    case "btnReApprove":
                        oVerifyInfo["FlowStepBtnID"] = "btnApprove";
                        goto case "btnApprove";
                    case "FlowReassign":
                        oVerifyInfo["FlowStepBtnID"] = "btnApprove";
                        goto case "btnApprove";

                    case "btnApprove":  //審核
                        //單筆審核(使用oFlow.FlowCaseID判斷)
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null)
                        {
                            #region"單筆審核"
                           
                            #region"前置資料"
                            //讀取現在關卡與下一關相關資料，因為不論回傳是否，我還是要資料，所以沒檢核回傳值與錯誤訊息
                            FlowUtility.QueryFlowDataAndToUserData(ADdt.Rows[0]["OTCompID"].ToString(), oFlow.FlowCurrLogAssignTo, ADdt.Rows[0]["OTStartDate"].ToString(), oFlow.FlowCaseID, flowCodeFlag,
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

                            #region"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"

                            //若是後台HR送簽依照填單人公司，否則用加班人公司
                            string HRLogCompID = signLineDefine == "4" || flowCode.Trim() == "" ?
                                                ADdt.Rows[0]["OTRegisterComp"].ToString() :
                                                ADdt.Rows[0]["OTCompID"].ToString();

                            //如果沒有下一關資料，則用現在關卡資料取代
                            if (toUserData.Count == 0)
                            {
                                //取[最近的行政or功能]資料 取代 [現在關卡]資料
                                DataTable toUDdt = HROverTimeLog(oFlow.FlowCaseID, true);
                                toUserData.Add("SignLine", toUDdt.Rows[0]["SignLine"].ToString());
                                toUserData.Add("SignIDComp", toUDdt.Rows[0]["SignIDComp"].ToString());
                                toUserData.Add("SignID", oFlow.FlowCurrLogAssignTo);
                                toUserData.Add("SignOrganID", toUDdt.Rows[0]["SignOrganID"].ToString());
                                toUserData.Add("SignFlowOrganID", toUDdt.Rows[0]["SignFlowOrganID"].ToString());
                            }

                            //如果下一關主管與現在主管相同，則再往上階找下一關主管資料
                            if (toUserData["SignID"] == oFlow.FlowCurrLogAssignTo)
                            {
                                switch (toUserData["SignLine"])
                                {
                                        //HR線 或 行政線
                                    case "4":
                                    case "1":
                                        if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], ADdt.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                        {
                                            toUserData["SignID"] = SignID;
                                            toUserData["SignIDComp"] = SignIDComp;
                                            toUserData["SignOrganID"] = SignOrganID;
                                            toUserData["SignFlowOrganID"] = "";
                                        }
                                        break;

                                    case "2":
                                        if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], ADdt.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                        {
                                            toUserData["SignID"] = SignID;
                                            toUserData["SignIDComp"] = SignIDComp;
                                            toUserData["SignOrganID"] = "";
                                            toUserData["SignFlowOrganID"] = SignOrganID;
                                        }
                                        break;

                                    //原本switch的是signLineDefine，現在改成toUserData["SignLine"]後，
                                    //case "3"裏頭的if基本只會用到else[非功能線一律走行政線]，以防萬一先保留。
                                    case "3":
                                        if (toUserData["SignLine"] == "2")
                                        {
                                            if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], ADdt.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                            {
                                                toUserData["SignID"] = SignID;
                                                toUserData["SignIDComp"] = SignIDComp;
                                                toUserData["SignOrganID"] = "";
                                                toUserData["SignFlowOrganID"] = SignOrganID;
                                            }
                                        }
                                        else
                                        {
                                            if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], ADdt.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
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

                            //如果找不到下一關主管資料，會等於現在關卡主管，這時候就直接拋審核失敗
                            if (toUserData["SignID"] == oFlow.FlowCurrLogAssignTo||toUserData["SignID"] == "")
                            {
                                txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                                "-------------------------------------" + Environment.NewLine +
                                "公司：" + ADdt.Rows[0]["OTCompID"].ToString() + Environment.NewLine +
                                "加班人：" + ADdt.Rows[0]["OTEmpID"].ToString() + Environment.NewLine +
                                "起迄日期：" + ADdt.Rows[0]["OTDate"].ToString() + Environment.NewLine +
                                "開始時間：" + ADdt.Rows[0]["OTStartTime"].ToString() + Environment.NewLine +
                                "結束時間：" + ADdt.Rows[0]["OTEndTime"].ToString() + Environment.NewLine +
                                "錯誤原因：查無下一關主管資料。" + Environment.NewLine;
                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                break;
                            }

                            //給IsFlowVerify的下一關簽核主管
                            oAssDic = getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);

                            #endregion"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"
                            //單筆審核應急用，寫死判斷(因為單筆是用永豐的Function傳遞值，無OverTimeA/DTable的資料)
                            //if (flowCodeFlag=="0") //猶豫要用哪個判斷式
                            if (oFlow.FlowCurrStepID == "A20" || oFlow.FlowCurrStepID == "OT01")
                            {
                                isLastFlow = isLastFlowNow(ADdt.Rows[0]["OTCompID"].ToString(), oFlow.FlowCaseID, "A");
                            }
                            else
                            {
                                isLastFlow = isLastFlowNow(ADdt.Rows[0]["OTCompID"].ToString(), oFlow.FlowCaseID, "D");
                            }

                            #endregion"前置資料"

                            switch (flowCodeFlag)
                            {
                                case "0"://事先
                                    if (!isLastFlow)//A20
                                    {
                                        sb.Reset();
                                        if (IsUpEmpRankID) //RankID19以上
                                        {
                                            #region"RankID19以上強制結案"
                                            sb.Reset();
                                            UpdateAorD("A", "3", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                            CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);

                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "3", "A", "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                            #endregion"RankID19以上強制結案"
                                        }
                                        else //RankID19未滿
                                        {
                                            #region"A20->A20 Re"
                                            sb.Reset();
                                            DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                            UpdateAorD("A", "2", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                            FlowUtility.InsertHROverTimeLogCommand(
                                                oFlow.FlowCaseID,
                                                addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                               "A",
                                               dt.Rows[0]["OTEmpID"].ToString(),
                                               dt.Rows[0]["EmpOrganID"].ToString(),
                                               dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                               UserInfo.getUserInfo().UserID,
                                               flowCode, flowSN,
                                               addone(dt.Rows[0]["FlowSeq"].ToString()),
                                               toUserData["SignLine"],
                                               toUserData["SignIDComp"],
                                               toUserData["SignID"],
                                               toUserData["SignOrganID"],
                                               toUserData["SignFlowOrganID"],
                                               "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                               );

                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "2", "A",
                                                toUserData["SignID"], toUserData["SignIDComp"], false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }

                                            #endregion"A20->A20Re"
                                        }
                                    }
                                    else//A20
                                    {
                                        #region"A20"
                                        //大於16 正常下去
                                        if (IsUpValidRankID)
                                        {
                                            #region"A20正常結案"
                                            sb.Reset();
                                            UpdateAorD("A", "3", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                            CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);
                                            insertData(oFlow.FlowCurrLastLogID, ref sb);

                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "2", "A", "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }

                                            #endregion"A20正常結案"
                                        }
                                        //小於16 
                                        else
                                        {
                                            #region"A20小於16繼續"
                                            sb.Reset();

                                            DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);

                                            UpdateAorD("A", "2", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                            FlowUtility.InsertHROverTimeLogCommand(
                                                oFlow.FlowCaseID,
                                                addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                               "A",
                                               dt.Rows[0]["OTEmpID"].ToString(),
                                               dt.Rows[0]["EmpOrganID"].ToString(),
                                               dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                               UserInfo.getUserInfo().UserID,
                                               flowCode,
                                               flowSN,
                                               addone(dt.Rows[0]["FlowSeq"].ToString()),
                                               toUserData["SignLine"],
                                               toUserData["SignIDComp"],
                                               toUserData["SignID"],
                                               toUserData["SignOrganID"],
                                               toUserData["SignFlowOrganID"],
                                               "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                               );
                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "2", "A", toUserData["SignID"], toUserData["SignIDComp"], false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                            #endregion"A20小於16繼續"
                                        }
                                        #endregion"A20"
                                    }
                                    break;

                                case "1"://事後
                                    if (signLineDefine == "4" || flowCode.Trim() == "")
                                    {
                                        #region"HR關"
                                        //大於16或大於19就結案
                                        if (IsUpValidRankID || IsUpEmpRankID)
                                        {
                                            #region"HR關正常結案"
                                            sb.Reset();
                                            UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                            CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);
                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                            #endregion"HR關正常結案"
                                        }
                                        else
                                        {
                                            //等一下寫
                                            #region"HR關未滿16"
                                            sb.Reset();
                                            DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                            UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                            UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                            FlowUtility.InsertHROverTimeLogCommand(
                                                oFlow.FlowCaseID,
                                                addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                "D",
                                                dt.Rows[0]["OTEmpID"].ToString(),
                                                dt.Rows[0]["EmpOrganID"].ToString(),
                                                dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                UserInfo.getUserInfo().UserID,
                                                flowCode, flowSN,
                                                addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                toUserData["SignLine"],
                                                toUserData["SignIDComp"],
                                                toUserData["SignID"],
                                                toUserData["SignOrganID"],
                                                toUserData["SignFlowOrganID"],
                                                "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                );

                                            ClearBtn(oFlow.FlowCurrLogAssignTo);
                                            if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                            {
                                                MailLogContent(oFlow.FlowCaseID, "2", "D", "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                            #endregion"HR關未滿16"
                                        }
                                        #endregion"HR關"
                                    }
                                    else
                                    {
                                        if (!isLastFlow)//A30 不是最後一關
                                        {
                                            sb.Reset();
                                            if (IsUpEmpRankID) //RankID19以上
                                            {
                                                #region"RankID19以上強制結案"
                                                sb.Reset();
                                                UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"RankID19以上強制結案"
                                            }
                                            else //RankID19未滿
                                            {
                                                #region"A30->A30Re"
                                                sb.Reset();
                                                DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                                FlowUtility.InsertHROverTimeLogCommand(
                                                    oFlow.FlowCaseID,
                                                    addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                    FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                   "D",
                                                   dt.Rows[0]["OTEmpID"].ToString(),
                                                   dt.Rows[0]["EmpOrganID"].ToString(),
                                                   dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                   UserInfo.getUserInfo().UserID,
                                                   flowCode, flowSN,
                                                   addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                   toUserData["SignLine"],
                                                   toUserData["SignIDComp"],
                                                   toUserData["SignID"],
                                                   toUserData["SignOrganID"],
                                                   toUserData["SignFlowOrganID"],
                                                   "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                   );

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "2", "D",
                                                    toUserData["SignID"], toUserData["SignIDComp"], false);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }

                                                #endregion"A30->A30Re"
                                            }
                                        }
                                        else //A30 最後一關
                                        {
                                            #region"A30"

                                            if (IsUpValidRankID || IsUpEmpRankID)//大於16 或大於19 正常結案
                                            {
                                                #region"A30正常結案"
                                                sb.Reset();
                                                UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);
                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"A30正常結案"
                                            }
                                            else//小於16
                                            {
                                                #region"A30小於16繼續"
                                                sb.Reset();
                                                DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                FlowUtility.InsertHROverTimeLogCommand(
                                            oFlow.FlowCaseID,
                                            addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                            FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                           "D",
                                           dt.Rows[0]["OTEmpID"].ToString(),
                                           dt.Rows[0]["EmpOrganID"].ToString(),
                                           dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                           UserInfo.getUserInfo().UserID,
                                           flowCode,
                                           flowSN,
                                           addone(dt.Rows[0]["FlowSeq"].ToString()),
                                           toUserData["SignLine"],
                                           toUserData["SignIDComp"],
                                           toUserData["SignID"],
                                           toUserData["SignOrganID"],
                                           toUserData["SignFlowOrganID"],
                                           "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                           );
                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "2", "D", toUserData["SignID"], toUserData["SignIDComp"], false);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }

                                                #endregion"A30小於16繼續"
                                            }

                                            #endregion"A30"
                                        }
                                    }
                                    break;
                            }
                            #endregion"單筆審核"
                        }
                        /*================================================*/
                        //多筆審核有四個迴圈，事先兩個是後兩個
                        //UpdateAD、HROverTimeLog、MailLog在第一迴圈
                        //其他在第二迴圈
                        //多筆審核按鈕(非印章)
                        else
                        {
                            #region"多筆審核"
                            //事先
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    #region"事先多筆"
                                    if (dtOverTimeAdvance.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        #region"前置資料"
                                        //永豐流程相關資料
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);

                                        //加班人RankID是否大於19
                                        EmpRankID = RankPara(Paradt, dtOverTimeAdvance.Rows[i]["CompID"].ToString(), "EmpRankID");
                                        IsUpEmpRankID = boolUpEmpRankID("A", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), EmpRankID);

                                        //關卡資料
                                        FlowUtility.QueryFlowDataAndToUserData(dtOverTimeAdvance.Rows[i]["CompID"].ToString(), getLastAssignTo(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString()), dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString(), dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "0",
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

                                        #region"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"

                                        if (toUserData.Count == 0)
                                        {
                                            //取[最近的行政or功能]資料 取代 [現在關卡]資料
                                            DataTable toUDdt = HROverTimeLog(oFlow.FlowCaseID, true);
                                            toUserData.Add("SignLine", toUDdt.Rows[0]["SignLine"].ToString());
                                            toUserData.Add("SignIDComp", toUDdt.Rows[0]["SignIDComp"].ToString());
                                            toUserData.Add("SignID", oFlow.FlowCurrLogAssignTo);
                                            toUserData.Add("SignOrganID", toUDdt.Rows[0]["SignOrganID"].ToString());
                                            toUserData.Add("SignFlowOrganID", toUDdt.Rows[0]["SignFlowOrganID"].ToString());
                                        }

                                        if (toUserData["SignID"] == dtOverTimeAdvance.Rows[i]["AssignTo"].ToString())
                                        {
                                            //DataTable toUDdt;
                                            switch (toUserData["SignLine"])
                                            {
                                                case "4":
                                                case "1":
                                                    if (EmpInfo.QueryOrganData(dtOverTimeAdvance.Rows[i]["CompID"].ToString(), toUserData["SignOrganID"], dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                    {
                                                        toUserData["SignID"] = SignID;
                                                        toUserData["SignIDComp"] = SignIDComp;
                                                        toUserData["SignOrganID"] = SignOrganID;
                                                        toUserData["SignFlowOrganID"] = "";
                                                    }
                                                    break;
                                                case "2":
                                                    if (EmpInfo.QueryFlowOrganData(toUserData["SignFlowOrganID"], dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                    {
                                                        toUserData["SignID"] = SignID;
                                                        toUserData["SignIDComp"] = SignIDComp;
                                                        toUserData["SignOrganID"] = "";
                                                        toUserData["SignFlowOrganID"] = SignOrganID;
                                                    }
                                                    break;
                                                case "3":
                                                    if (toUserData["SignLine"] == "2")
                                                    {
                                                        if (EmpInfo.QueryFlowOrganData(toUserData["SignFlowOrganID"], dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                        {
                                                            toUserData["SignID"] = SignID;
                                                            toUserData["SignIDComp"] = SignIDComp;
                                                            toUserData["SignOrganID"] = "";
                                                            toUserData["SignFlowOrganID"] = SignOrganID;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (EmpInfo.QueryOrganData(dtOverTimeAdvance.Rows[i]["CompID"].ToString(), toUserData["SignOrganID"], dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
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

                                        //如果找不到下一關主管資料，會等於現在關卡主管，這時候就直接拋審核失敗
                                        if (toUserData["SignID"] == oFlow.FlowCurrLogAssignTo)
                                        {
                                            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                                            "-------------------------------------" + Environment.NewLine +
                                            "公司：" + dtOverTimeAdvance.Rows[i]["CompID"].ToString() + Environment.NewLine +
                                            "加班人：" + dtOverTimeAdvance.Rows[i]["EmpID"].ToString() + Environment.NewLine +
                                            "起迄日期：" + dtOverTimeAdvance.Rows[i]["OTStartDate"].ToString() +
                                            "～" + dtOverTimeAdvance.Rows[i]["OTEndDate"].ToString() + Environment.NewLine +
                                            "開始時間：" + dtOverTimeAdvance.Rows[i]["OTStartTime"].ToString() + Environment.NewLine +
                                            "結束時間：" + dtOverTimeAdvance.Rows[i]["OTEndTime"].ToString() + Environment.NewLine +
                                            "錯誤原因：查無下一關主管資料。" + Environment.NewLine;
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            continue;
                                        }

                                        oAssDic = getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);

                                        #endregion"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"

                                        isLastFlow = isLastFlowNow(dtOverTimeAdvance.Rows[i]["CompID"].ToString(), dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A");
                                        #endregion"前置資料"

                                        #region"A20"
                                        sb.Reset();
                                        //大於16或大於19 正常結案
                                        if (isLastFlow)
                                        {
                                            if (IsUpValidRankID || IsUpEmpRankID)
                                            {
                                                #region"A20正常結案"

                                                sb.Reset();
                                                UpdateAorD("A", "3", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);
                                                insertData(oFlow.FlowCurrLastLogID, ref sb);
                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], "A"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "3", "A", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {

                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"最後一關正常結案"
                                            }
                                            else//小於16繼續上送
                                            {
                                                #region"最後一關小於16繼續"
                                                sb.Reset();

                                                DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                //-----
                                                UpdateAorD("A", "2", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                                FlowUtility.InsertHROverTimeLogCommand(
                                                    oFlow.FlowCaseID,
                                                    addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                    FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                   "A",
                                                   dt.Rows[0]["OTEmpID"].ToString(),
                                                   dt.Rows[0]["EmpOrganID"].ToString(),
                                                   dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                   UserInfo.getUserInfo().UserID,
                                                   flowCode,
                                                   flowSN,
                                                   addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                   toUserData["SignLine"],
                                                   toUserData["SignIDComp"],
                                                   toUserData["SignID"],
                                                   toUserData["SignOrganID"],
                                                   toUserData["SignFlowOrganID"],
                                                   "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                   );

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], "A"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "2", "A", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"A20小於16繼續"
                                            }
                                        }
                                        else
                                        {
                                            if (IsUpEmpRankID)
                                            {
                                                #region"非最後一關大於19結案"

                                                sb.Reset();
                                                UpdateAorD("A", "3", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);
                                                insertData(oFlow.FlowCurrLastLogID, ref sb);
                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], "A"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "3", "A", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {

                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"非最後一關大於19結案"
                                            }
                                            else
                                            {
                                                #region"非最後一關正常繼續送"
                                                sb.Reset();

                                                DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                //-----
                                                UpdateAorD("A", "2", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                                FlowUtility.InsertHROverTimeLogCommand(
                                                    oFlow.FlowCaseID,
                                                    addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                    FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                   "A",
                                                   dt.Rows[0]["OTEmpID"].ToString(),
                                                   dt.Rows[0]["EmpOrganID"].ToString(),
                                                   dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                   UserInfo.getUserInfo().UserID,
                                                   flowCode,
                                                   flowSN,
                                                   addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                   toUserData["SignLine"],
                                                   toUserData["SignIDComp"],
                                                   toUserData["SignID"],
                                                   toUserData["SignOrganID"],
                                                   toUserData["SignFlowOrganID"],
                                                   "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                   );

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], "A"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "2", "A", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"非最後一關正常繼續送"
                                            }
                                        }
                                        #endregion"A20"
                                    }
                                    #endregion"事先多筆"
                                }
                            }
                            /*========================================*/
                            //事後核准
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                                //多筆處理
                                for (int j = 0; j < dtOverTimeDeclaration.Rows.Count; j++)
                                {
                                    EmpRankID = RankPara(Paradt, dtOverTimeDeclaration.Rows[j]["CompID"].ToString(), "EmpRankID");
                                    IsUpEmpRankID = boolUpEmpRankID("D", dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), EmpRankID);
                                    oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), "D"), true);
                                    if (dtOverTimeDeclaration.Rows[j]["AfterOTSeqNo"].ToString() == "1")
                                    {
                                        FlowUtility.QueryFlowDataAndToUserData(dtOverTimeDeclaration.Rows[j]["CompID"].ToString(), getLastAssignTo(dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString()), dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString(), dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), "1",
    out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

                                        #region"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"
                                        string HRLogCompID = signLineDefine == "4" || flowCode.Trim() == "" ?
                                                dtOverTimeDeclaration.Rows[j]["OTRegisterComp"].ToString() :
                                                dtOverTimeDeclaration.Rows[j]["CompID"].ToString();

                                        if (toUserData.Count == 0)
                                        {
                                            //取[最近的行政or功能]資料 取代 [現在關卡]資料
                                            DataTable toUDdt = HROverTimeLog(oFlow.FlowCaseID, true);
                                            toUserData.Add("SignLine", toUDdt.Rows[0]["SignLine"].ToString());
                                            toUserData.Add("SignIDComp", toUDdt.Rows[0]["SignIDComp"].ToString());
                                            toUserData.Add("SignID", oFlow.FlowCurrLogAssignTo);
                                            toUserData.Add("SignOrganID", toUDdt.Rows[0]["SignOrganID"].ToString());
                                            toUserData.Add("SignFlowOrganID", toUDdt.Rows[0]["SignFlowOrganID"].ToString());
                                        }

                                        if (toUserData["SignID"] == dtOverTimeDeclaration.Rows[j]["AssignTo"].ToString())
                                        {
                                            switch (toUserData["SignLine"])
                                            {
                                                case "4":
                                                case "1":
                                                    if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                    {
                                                        toUserData["SignID"] = SignID;
                                                        toUserData["SignIDComp"] = SignIDComp;
                                                        toUserData["SignOrganID"] = SignOrganID;
                                                        toUserData["SignFlowOrganID"] = "";
                                                    }
                                                    break;
                                                case "2":
                                                    if (EmpInfo.QueryFlowOrganData(toUserData["SignFlowOrganID"], dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                    {
                                                        toUserData["SignID"] = SignID;
                                                        toUserData["SignIDComp"] = SignIDComp;
                                                        toUserData["SignOrganID"] = "";
                                                        toUserData["SignFlowOrganID"] = SignOrganID;
                                                    }
                                                    break;
                                                case "3":
                                                    if (toUserData["SignLine"] == "2")
                                                    {
                                                        if (EmpInfo.QueryFlowOrganData(toUserData["OrganID"], dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                                                        {
                                                            toUserData["SignID"] = SignID;
                                                            toUserData["SignIDComp"] = SignIDComp;
                                                            toUserData["SignOrganID"] = "";
                                                            toUserData["SignFlowOrganID"] = SignOrganID;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
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

                                        //如果找不到下一關主管資料，會等於現在關卡主管，這時候就直接拋審核失敗
                                        if (toUserData["SignID"] == oFlow.FlowCurrLogAssignTo)
                                        {
                                            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                                            "-------------------------------------" + Environment.NewLine +
                                            "公司：" + dtOverTimeDeclaration.Rows[j]["CompID"].ToString() + Environment.NewLine +
                                            "加班人：" + dtOverTimeDeclaration.Rows[j]["EmpID"].ToString() + Environment.NewLine +
                                            "起迄日期：" + dtOverTimeDeclaration.Rows[j]["AfterOTStartDate"].ToString() +
                                            "～" + dtOverTimeDeclaration.Rows[j]["AfterOTEndDate"].ToString() + Environment.NewLine +
                                            "開始時間：" + dtOverTimeDeclaration.Rows[j]["AfterOTStartTime"].ToString() + Environment.NewLine +
                                            "結束時間：" + dtOverTimeDeclaration.Rows[j]["AfterOTEndTime"].ToString() + Environment.NewLine +
                                            "錯誤原因：查無下一關主管資料。" + Environment.NewLine;
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            continue;
                                        }

                                        oAssDic = getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);

                                        #endregion"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"

                                        isLastFlow = isLastFlowNow(dtOverTimeDeclaration.Rows[j]["CompID"].ToString(), dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), "D");


                                        if (signLineDefine == "4" || flowCode.Trim() == "")
                                        {
                                            //大於16或大於19就結案
                                            if (IsUpValidRankID || IsUpEmpRankID)
                                            {
                                                #region"HR關"
                                                sb.Reset();
                                                UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"HR關"
                                            }
                                            else
                                            {
                                                //等一下寫
                                                #region"HR關未滿16"
                                                sb.Reset();
                                                DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                                FlowUtility.InsertHROverTimeLogCommand(
                                                    oFlow.FlowCaseID,
                                                    addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                    FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                    "D",
                                                    dt.Rows[0]["OTEmpID"].ToString(),
                                                    dt.Rows[0]["EmpOrganID"].ToString(),
                                                    dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                    UserInfo.getUserInfo().UserID,
                                                    flowCode, flowSN,
                                                    addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                    toUserData["SignLine"],
                                                    toUserData["SignIDComp"],
                                                    toUserData["SignID"],
                                                    toUserData["SignOrganID"],
                                                    toUserData["SignFlowOrganID"],
                                                    "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                    );

                                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                {
                                                    MailLogContent(oFlow.FlowCaseID, "2", "D", "", "", true);
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                }
                                                else
                                                {
                                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                }
                                                #endregion"HR關未滿16"
                                            }
                                        }
                                        else //非HR關卡
                                        {
                                            if (!isLastFlow)//A30
                                            {
                                                #region"A30"
                                                sb.Reset();
                                                //大於19強制結案
                                                if (IsUpEmpRankID)
                                                {
                                                    #region"RankID19以上強制結案"
                                                    sb.Reset();
                                                    UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                    CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);

                                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                    {
                                                        MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                    }
                                                    else
                                                    {
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                    }
                                                    #endregion"RankID19以上強制結案"
                                                }
                                                else //A30->A30 非最後一關
                                                {
                                                    #region"A30->A30Re"

                                                    DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);
                                                    //-----
                                                    sb.Reset();
                                                    UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);

                                                    FlowUtility.InsertHROverTimeLogCommand(
                                                        oFlow.FlowCaseID,
                                                        addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                        FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                                       "D",
                                                       dt.Rows[0]["OTEmpID"].ToString(),
                                                       dt.Rows[0]["EmpOrganID"].ToString(),
                                                       dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                                       UserInfo.getUserInfo().UserID,
                                                       flowCode, flowSN,
                                                       addone(dt.Rows[0]["FlowSeq"].ToString()),
                                                       toUserData["SignLine"],
                                                       toUserData["SignIDComp"],
                                                       toUserData["SignID"],
                                                       toUserData["SignOrganID"],
                                                       toUserData["SignFlowOrganID"],
                                                       "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                                       );

                                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                    {
                                                        MailLogContent(oFlow.FlowCaseID, "2", "D",
                                                        toUserData["SignID"], toUserData["SignIDComp"], false);
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                    }
                                                    else
                                                    {
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                    }
                                                    #endregion"A30->A30Re"
                                                }
                                                #endregion"A30"
                                            }
                                            else//A30 最後一關
                                            {
                                                #region"A30最後一關"
                                                sb.Reset();
                                                //大於16或大於19正常結案
                                                if (IsUpValidRankID || IsUpEmpRankID)
                                                {
                                                    #region"A30End"

                                                    sb.Reset();
                                                    UpdateAorD("D", "3", oFlow.FlowCaseID, ref sb);
                                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                    CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);

                                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnClose", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                    {
                                                        MailLogContent(oFlow.FlowCaseID, "3", "D", "", "", true);
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                    }
                                                    else
                                                    {
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                    }
                                                    #endregion"A30正常結案"
                                                }
                                                else //小於16
                                                {
                                                    #region"A30小於16繼續"

                                                    DataTable dt = HROverTimeLog(oFlow.FlowCaseID, false);

                                                    sb.Reset();
                                                    UpdateAorD("D", "2", oFlow.FlowCaseID, ref sb);
                                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                                    FlowUtility.InsertHROverTimeLogCommand(
                                                oFlow.FlowCaseID,
                                                addone(dt.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dt.Rows[0]["FlowLogID"].ToString()),
                                               "D",
                                               dt.Rows[0]["OTEmpID"].ToString(),
                                               dt.Rows[0]["EmpOrganID"].ToString(),
                                               dt.Rows[0]["EmpFlowOrganID"].ToString(),
                                               UserInfo.getUserInfo().UserID,
                                               flowCode,
                                               flowSN,
                                               addone(dt.Rows[0]["FlowSeq"].ToString()),
                                               toUserData["SignLine"],
                                               toUserData["SignIDComp"],
                                               toUserData["SignID"],
                                               toUserData["SignOrganID"],
                                               toUserData["SignFlowOrganID"],
                                               "1", false, ref sb, int.Parse(dt.Rows[0]["Seq"].ToString()) + 1
                                               );

                                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReApprove", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                                    {
                                                        MailLogContent(oFlow.FlowCaseID, "2", "D", toUserData["SignID"], toUserData["SignIDComp"], false);
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                                    }
                                                    else
                                                    {
                                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                                    }
                                                    #endregion"A40小於16繼續"
                                                }
                                                #endregion"A40"
                                            }
                                        }
                                    }
                                }//for多筆處理
                            }//if事後
                            #endregion"多筆審核"
                        }
                        break;

                    /*=================================================*/
                    case "btnReject":  //駁回
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null) //true 印章 false 多筆審核按鈕
                        {
                            #region"單筆駁回"
                            oFlow = new FlowExpress(Aattendant._AattendantFlowID, Request["FlowLogID"], true);
                            FlowUtility.QueryFlowDataAndToUserData(UserInfo.getUserInfo().CompID, oFlow.FlowCurrLogAssignTo, oFlow.FlowCurrLogCrDateTime.Date.ToString("yyyy/MM/dd"), oFlow.FlowCaseID, flowCodeFlag,
                                out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);
                            if (flowCodeFlag == "0")
                            {
                                #region"事先駁回"
                                sb.Reset();
                                UpdateAorD("A", "4", oFlow.FlowCaseID, ref sb);
                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);
                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                {
                                    MailLogContent(oFlow.FlowCaseID, "4", "A", "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                                #endregion"事先駁回"
                            }
                            else
                            {
                                #region"事後駁回"
                                sb.Reset();
                                UpdateAorD("D", "4", oFlow.FlowCaseID, ref sb);
                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);
                                ClearBtn(oFlow.FlowCurrLogAssignTo);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, ADdt.Rows[0], ""))
                                {
                                    MailLogContent(oFlow.FlowCaseID, "4", "D", "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                                #endregion"事後駁回"
                            }
                            #endregion"單筆駁回"
                        }

                        else //多筆審核按鈕
                        {
                            #region"多筆駁回"
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                #region"多筆事先駁回"
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    sb.Reset();
                                    oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);
                                    FlowUtility.QueryFlowDataAndToUserData(UserInfo.getUserInfo().CompID, oFlow.FlowCurrLogAssignTo, oFlow.FlowCurrLogCrDateTime.Date.ToString("yyyy/MM/dd"), oFlow.FlowCaseID, "0",
                                        out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);
                                    isLastFlow = isLastFlowNow(dtOverTimeAdvance.Rows[i]["CompID"].ToString(), dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A");
                                    //---------------
                                    sb.Reset();
                                    UpdateAorD("A", "4", oFlow.FlowCaseID, ref sb);
                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                    CloseHROverTimeLog(oFlow.FlowCaseID, "A", ref sb);
                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], "A"))
                                    {
                                        MailLogContent(oFlow.FlowCaseID, "4", "A", "", "", true);
                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                    }
                                    else
                                    {
                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                    }
                                }
                                #endregion"多筆事先駁回"
                            }
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                                #region"多筆事後駁回"
                                for (int j = 0; j < dtOverTimeDeclaration.Rows.Count; j++)
                                {
                                    sb.Reset();
                                    oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), "D"), true);
                                    FlowUtility.QueryFlowDataAndToUserData(UserInfo.getUserInfo().CompID, oFlow.FlowCurrLogAssignTo, oFlow.FlowCurrLogCrDateTime.Date.ToString("yyyy/MM/dd"), oFlow.FlowCaseID, "0",
                                        out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);
                                    isLastFlow = isLastFlowNow(dtOverTimeDeclaration.Rows[j]["CompID"].ToString(), dtOverTimeDeclaration.Rows[j]["AfterFlowCaseID"].ToString(), "D");
                                    //---------------
                                    sb.Reset();
                                    UpdateAorD("D", "4", oFlow.FlowCaseID, ref sb);
                                    UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                    CloseHROverTimeLog(oFlow.FlowCaseID, "D", ref sb);
                                    ClearBtn(oFlow.FlowCurrLogAssignTo);
                                    if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[j], "D"))
                                    {
                                        MailLogContent(oFlow.FlowCaseID, "4", "D", "", "", true);
                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                    }
                                    else
                                    {
                                        labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                    }
                                }
                                #endregion"多筆事後駁回"
                            }
                            #endregion"多筆駁回"
                        }
                        break;
                    /*==============================*/
                }
                if (txtErrMsg.Text != "")
                {
                    txtErrMsg.Text = "審核失敗清單：" + txtErrMsg.Text;
                    txtErrMsg.Visible = true;
                }
                Util.setJSContent(oVerifyInfo["FlowVerifyJS"]);
            }
            else
            {
                //參數錯誤
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError);
            }
        }
    }
    /// <summary>
    /// 核准動作：IsFlowVerify()、其他Table新刪修的sb、try-catch與失敗清單
    /// </summary>
    /// <param name="FlowLogID">IsFlowVerify用</param>
    /// <param name="btn">IsFlowVerify用</param>
    /// <param name="oAssDic">IsFlowVerify用</param>
    /// <param name="FlowStepOpinion">IsFlowVerify用</param>
    /// <param name="sb">全部Table的新刪修SQL</param>
    /// <param name="ErrMsg">回報錯誤(目前沒用)</param>
    /// <param name="dt">失敗清單，找尋單筆相關資料用</param>
    /// <param name="ADType">失敗清單，找尋單筆相關資料用</param>
    /// <returns></returns>
    private bool TryCatchIsFlowVerify(string FlowLogID, string btn, Dictionary<string, string> oAssDic, string FlowStepOpinion, CommandHelper sb, out string ErrMsg, DataRow dt = null, string ADType = "")
    {
        ErrMsg = "";

        //測試用test
        //sb.Append("test我是來製作錯誤的!!");

        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            if (FlowExpress.IsFlowVerify(Request["FlowID"], FlowLogID, btn, oAssDic, FlowStepOpinion))
            {
                tx.Commit();
                return true;
            }
            else
            {
                if (dt != null)
                {
                    if (ADType == "A") //多筆A
                    {
                        txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                        "-------------------------------------" + Environment.NewLine +
                        "公司：" + dt["CompID"].ToString() + Environment.NewLine +
                        "加班人：" + dt["EmpID"].ToString() + Environment.NewLine +
                        "起迄日期：" + dt["OTStartDate"].ToString() +
                        "～" + dt["OTEndDate"].ToString() + Environment.NewLine +
                        "開始時間：" + dt["OTStartTime"].ToString() + Environment.NewLine +
                        "結束時間：" + dt["OTEndTime"].ToString() + Environment.NewLine +
                        "錯誤原因：IsFlowVerify執行出錯。";
                    }
                    else if (ADType == "D") //多筆D
                    {
                        txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                        "-------------------------------------" + Environment.NewLine +
                        "公司：" + dt["CompID"].ToString() + Environment.NewLine +
                        "加班人：" + dt["EmpID"].ToString() + Environment.NewLine +
                        "起迄日期：" + dt["AfterOTStartDate"].ToString() +
                        "～" + dt["AfterOTEndDate"].ToString() + Environment.NewLine +
                        "開始時間：" + dt["AfterOTStartTime"].ToString() + Environment.NewLine +
                        "結束時間：" + dt["AfterOTEndTime"].ToString() + Environment.NewLine +
                        "錯誤原因：IsFlowVerify執行出錯。";
                    }
                    else //單筆AD
                    {
                        txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                        "-------------------------------------" + Environment.NewLine +
                        "公司：" + dt["OTCompID"].ToString() + Environment.NewLine +
                        "加班人：" + dt["OTEmpID"].ToString() + Environment.NewLine +
                        "起迄日期：" + dt["OTDate"].ToString() + Environment.NewLine +
                        "開始時間：" + dt["OTStartTime"].ToString() + Environment.NewLine +
                        "結束時間：" + dt["OTEndTime"].ToString() + Environment.NewLine +
                        "錯誤原因：IsFlowVerify執行出錯。";
                    }
                    //txtErrMsg.Text = txtErrMsg.Text + 
                    //    "錯誤訊息："+ex + Environment.NewLine;
                }
                tx.Rollback();
                return false;
            }
        }
        catch (Exception ex)
        {
            ErrMsg = ex.ToString();
            if (dt != null)
            {
                if (ADType == "A") //多筆A
                {
                    txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                    "-------------------------------------" + Environment.NewLine +
                    "公司：" + dt["CompID"].ToString() + Environment.NewLine +
                    "加班人：" + dt["EmpID"].ToString() + Environment.NewLine +
                    "起迄日期：" + dt["OTStartDate"].ToString() +
                    "～" + dt["OTEndDate"].ToString() + Environment.NewLine +
                    "開始時間：" + dt["OTStartTime"].ToString() + Environment.NewLine +
                    "結束時間：" + dt["OTEndTime"].ToString() + Environment.NewLine +
                    "錯誤原因：加班單相關Table新增、修改出錯。";
                }
                else if (ADType == "D") //多筆D
                {
                    txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                    "-------------------------------------" + Environment.NewLine +
                    "公司：" + dt["CompID"].ToString() + Environment.NewLine +
                    "加班人：" + dt["EmpID"].ToString() + Environment.NewLine +
                    "起迄日期：" + dt["AfterOTStartDate"].ToString() +
                    "～" + dt["AfterOTEndDate"].ToString() + Environment.NewLine +
                    "開始時間：" + dt["AfterOTStartTime"].ToString() + Environment.NewLine +
                    "結束時間：" + dt["AfterOTEndTime"].ToString() + Environment.NewLine +
                    "錯誤原因：加班單相關Table新增、修改出錯。";
                }
                else //單筆AD
                {
                    txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                    "-------------------------------------" + Environment.NewLine +
                    "公司：" + dt["OTCompID"].ToString() + Environment.NewLine +
                    "加班人：" + dt["OTEmpID"].ToString() + Environment.NewLine +
                    "起迄日期：" + dt["OTDate"].ToString() + Environment.NewLine +
                    "開始時間：" + dt["OTStartTime"].ToString() + Environment.NewLine +
                    "結束時間：" + dt["OTEndTime"].ToString() + Environment.NewLine +
                    "錯誤原因：加班單相關Table新增、修改出錯。";
                }
                //txtErrMsg.Text = txtErrMsg.Text + 
                //    "錯誤訊息："+ex + Environment.NewLine;
            }
            tx.Rollback();
            return false;
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
        return true;
    }
    #region"事先到事後的insert，Judy的微更動"
    protected void insertData(string flow, ref CommandHelper sb)
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb2 = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        Aattendant At = new Aattendant();
        string OTTxnID = "";
        sb2.Append(" SELECT * FROM OverTimeAdvance WHERE FlowCaseID='" + flow.Substring(0, 14) + "'");
        DataTable dtOT = db.ExecuteDataSet(sb2.BuildCommand()).Tables[0];
        if (dtOT.Rows.Count > 0)
        {
            sb2.Reset();
            for (int i = 0; i < dtOT.Rows.Count; i++)
            {
                //leo modify 20170110 OTTxnID OTSeqNo
                int OTSeq = At.QuerySeq("OverTimeDeclaration", dtOT.Rows[i]["OTCompID"].ToString(), dtOT.Rows[i]["OTEmpID"].ToString(), dtOT.Rows[i]["OTStartDate"].ToString());
                if (dtOT.Rows[i]["OTSeqNo"].ToString() == "1")
                {
                    OTTxnID = (UserInfo.getUserInfo().CompID + dtOT.Rows[i]["OTEmpID"].ToString() + Convert.ToDateTime(dtOT.Rows[i]["OTStartDate"]).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                }
                sb.Append(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");
                sb.Append(" VALUES('" + dtOT.Rows[i]["OTCompID"] + "', '" + dtOT.Rows[i]["OTEmpID"] + "', '" + dtOT.Rows[i]["OTStartDate"] + "', '" + dtOT.Rows[i]["OTEndDate"] + "', '" + OTSeq + "',");
                sb.Append(" '" + OTTxnID + "','" + dtOT.Rows[i]["OTSeqNo"] + "','" + dtOT.Rows[i]["OTTxnID"] + "',");  //leo modify 20170110
                sb.Append(" '" + dtOT.Rows[i]["DeptID"] + "', '" + dtOT.Rows[i]["OrganID"] + "','" + dtOT.Rows[i]["DeptName"] + "','" + dtOT.Rows[i]["OrganName"] + "',");
                sb.Append(" '', "); //流程ID
                sb.Append(" '" + dtOT.Rows[i]["OTStartTime"] + "', '" + dtOT.Rows[i]["OTEndTime"] + "', '" + dtOT.Rows[i]["OTTotalTime"] + "',");
                sb.Append(" '" + dtOT.Rows[i]["SalaryOrAdjust"] + "' ,"); //轉薪資或補休
                sb.Append(" '" + Convert.ToDateTime(dtOT.Rows[i]["AdjustInvalidDate"]).ToString("yyyy-MM-dd") + "', ");
                //失效時間 " '1900-01-01 00:00:00.000', " 
                //失效時間 " '" +  dtOT.Rows[i]["AdjustInvalidDate"] + "', "
                //20170309-leo modify
                //失效時間 " '" + Convert.ToDateTime(dtOT.Rows[i]["AdjustInvalidDate"]).ToString("yyyy-MM-dd") + "', "
                sb.Append(" '', ");
                sb.Append(" '1900-01-01 00:00:00.000', ");
                sb.Append(" '" + dtOT.Rows[i]["MealFlag"] + "', '" + dtOT.Rows[i]["MealTime"] + "', '" + dtOT.Rows[i]["OTTypeID"] + "',");
                sb.Append(" '" + dtOT.Rows[i]["OTReasonID"] + "', "); //加班原因的代號
                sb.Append(" '" + dtOT.Rows[i]["OTReasonMemo"] + "', ");
                sb.Append(" '" + dtOT.Rows[i]["OTAttachment"] + "', '" + dtOT.Rows[i]["OTFormNO"] + "', ");
                sb.Append(" '" + dtOT.Rows[i]["OTRegisterID"] + "', '" + Convert.ToDateTime(dtOT.Rows[i]["OTRegisterDate"]).ToString("yyyy-MM-dd HH:mm:ss.fff") + "', '1',");//申請單狀態
                sb.Append(" '1900-01-01 00:00:00.000', ");
                sb.Append(" '', ");
                sb.Append(" '1900-01-01 00:00:00.000', ");
                sb.Append(" '', ");
                sb.Append(" '', ");
                sb.Append(" '0', ");
                sb.Append(" '" + dtOT.Rows[i]["HolidayOrNot"] + "', ");
                sb.Append(" '1900-01-01 00:00:00.000', ");
                sb.Append(" '', ");
                sb.Append(" '1900-01-01 00:00:00.000', ");
                sb.Append(" '', ");
                sb.Append(" '" + dtOT.Rows[i]["LastChgComp"] + "', '" + dtOT.Rows[i]["OTRegisterID"] + "', ");
                sb.Append(" '', ");
                sb.Append(" '" + dtOT.Rows[i]["LastChgComp"] + "', ");
                sb.Append(" '" + dtOT.Rows[i]["OTRegisterID"] + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                sb.Append(" '" + dtOT.Rows[0]["OTRegisterComp"] + "'); ");
            }
        }
    }

    protected void insertData(DataTable dt)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            sb.Reset();
            //sb.Append(" SELECT * FROM OverTimeAdvance WHERE OTEmpID='" + dt.Rows[i]["EmpID"] + "' AND OTStartDate='" + dt.Rows[i]["OTStartDate"] + "' AND OTStartTime='" + dt.Rows[i]["OTStartTime"] + "' AND OTEndTime='" + dt.Rows[i]["OTEndTime"] + "'");
            sb.Append(" SELECT * FROM OverTimeAdvance WHERE FlowCaseID='" + dt.Rows[i]["FlowCaseID"] + "' AND OTSeqNo='" + dt.Rows[i]["OTSeqNo"] + "'");
            sb.Append(" Order by FlowCaseID,OTSeqNo asc ");
            using (DataTable dtOT = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                if (dtOT.Rows.Count > 0)
                {
                    sb.Reset();
                    //排列位置跟下方select相同
                    sb.Append("  insert into OverTimeDeclaration (OTCompID,OTEmpID,OTStartDate,OTEndDate ");
                    sb.Append("  ,OTSeq ");
                    sb.Append(" ,OTTxnID ");
                    sb.Append(" ,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID ");
                    sb.Append(" ,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus ");
                    sb.Append(" ,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment ");
                    sb.Append(" ,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OverTimeFlag ");
                    sb.Append(" ,ToOverTimeDate,ToOverTimeFlag,OTRejectDate,OTRejectID,OTGovernmentNo ");
                    sb.Append(" ,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark ");
                    sb.Append(" ,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");

                    sb.Append("  select OTCompID,OTEmpID,OTStartDate,OTEndDate ");
                    //seq
                    sb.Append("  ,(select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) ");
                    //OTTxnID開始
                    sb.Append(" ,case OTSeqNo when'1'then OTCompID+OTEmpID+ ");
                    sb.Append(" replace((select OTStartDate from OverTimeAdvance OA where OA.FlowCaseID=A.FlowCaseID and OA.OTSeqNo='1'),'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) as VARCHAR),2) ");
                    sb.Append(" else (select Top 1 OTTxnID from OverTimeDeclaration where OTFromAdvanceTxnId=A.OTTxnID and OTSeqNo='1')end as OTTxnID ");
                    //OTTxnID結束
                    //--
                    //sb.Append("  ,OTCompID+OTEmpID+replace((select OTStartDate from OverTimeAdvance OA where OA.FlowCaseID=A.FlowCaseID and OA.OTSeqNo='1'),'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=(select OTStartDate from OverTimeAdvance where FlowCaseID=A.FlowCaseID and OTSeqNo='1')) as VARCHAR),2) as OTTxnID ");
                    //--
                    //sb.Append("  ,OTCompID+OTEmpID+replace(OTStartDate,'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=(select OTStartDate from OverTimeAdvance where FlowCaseID=A.FlowCaseID and OTSeqNo='1'))+1 as VARCHAR),2) as OTTxnID ");
                    /*===================================*/
                    //(select MAX(OTSeq) from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) as OTSeq
                    //上面的OTSeq放入下面的OTSeq
                    //OTCompID+OTEmpID+replace(OTStartDate,'/','')+ RIGHT('0'+cast(OTSeq as VARCHAR),2) as OTTxnID
                    /*===================================*/
                    sb.Append(" ,OTSeqNo,OTTxnID,DeptID,OrganID,DeptName,OrganName,'' ");
                    sb.Append(" ,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,'' ");
                    sb.Append(" ,'',MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment ");
                    sb.Append(" ,OTFormNO,OTRegisterID,OTRegisterDate,'1',OTValidDate,OTValidID,'1' ");
                    sb.Append(" ,'1900-01-01 00:00:00.000','0',OTRejectDate,OTRejectID,OTGovernmentNo ");
                    sb.Append(" ,'0',HolidayOrNot,'1900-01-01 00:00:00.000','0','1900-01-01 00:00:00.000','' ");
                    sb.Append(" ,'','','',LastChgComp,LastChgID,LastChgDate,OTRegisterComp ");
                    sb.Append(" from OverTimeAdvance A ");
                    sb.Append("where FlowCaseID='" + dt.Rows[i]["FlowCaseID"] + "' and OTSeqNo='" + dt.Rows[i]["OTSeqNo"] + "'");
                    try
                    {
                        db.ExecuteNonQuery(sb.BuildCommand(), tx);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                        tx.Rollback();//資料更新失敗
                    }
                    finally
                    {
                        cn.Close();
                        cn.Dispose();
                        tx.Dispose();
                    }
                }
            }
        }
    }

    #endregion"事先到事後的insert，Judy的微更動"
}