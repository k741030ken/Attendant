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
    //private string ADTable(string AD)
    //{
    //    switch (AD)
    //    {
    //        case "A":
    //        case "1":
    //            return "OverTimeAdvance";
    //        case "D":
    //        case "2":
    //            return "OverTimeDeclaration";
    //    }
    //    return AD;
    //}
    /// <summary>
    ///Judy切割出來多筆審核撈取FlowLogID
    /// </summary>
    private string getFlowLogID(string FlowCaseID, string AD)
    {
        FlowExpress tbFlow = new FlowExpress(_CurrFlowID, _CurrFlowLogID, false);
        AD = CustVerify.ADTable(AD);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.AppendStatement(" SELECT top 1 AL.FlowLogID,* FROM " + AD + " OT  ");
        sb.Append("  LEFT JOIN " + tbFlow.FlowCustDB + "FlowFullLog AL ON OT.FlowCaseID=AL.FlowCaseID  ");
        sb.Append(" WHERE OT.FlowCaseID =").AppendParameter("FlowCaseID", FlowCaseID);
        sb.Append(" Order by AL.FlowLogID desc ");
        return db.ExecuteScalar(sb.BuildCommand()).ToString();
    }

    /// <summary>
    /// 將EmpID組成Dictionary<EmpID,EmpName>方便丟給oAssDic，AssignToName的[組織-人名]可由這裡更動
    /// </summary>
    /// <param name="EmpID">下一關主管</param>
    /// <param name="CompID">下一關主管公司</param>
    /// <returns>for流程oAssDic用</returns>
    //public Dictionary<string, string> getEmpID_Name_Dictionary(string EmpID, string CompID)
    //{
    //    DbHelper db = new DbHelper(Aattendant._AattendantDBName);
    //    CommandHelper sb = db.CreateCommandHelper();
    //    DataTable dt;
    //    sb.Append(" SELECT DISTINCT P.EmpID,O.OrganName+'-'+P.Name as Name ");
    //    sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
    //    sb.Append(" left join " + _eHRMSDB + ".dbo. Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
    //    sb.Append(" WHERE P.EmpID=").AppendParameter("EmpID", EmpID);
    //    sb.Append(" and P.CompID=").AppendParameter("CompID", CompID);
    //    dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    //    Dictionary<string, string> _dic = new Dictionary<string, string>();
    //    _dic = Util.getDictionary(dt, 0, 1);
    //    return _dic;
    //}

    ///// <summary>
    ///// 用FlowCase撈取A或D的資料
    ///// </summary>
    //private DataTable OverTime_find_by_FlowCaseID(string FlowCase, string AD)
    //{
    //    DbHelper db = new DbHelper(Aattendant._AattendantDBName);
    //    CommandHelper sb = db.CreateCommandHelper();
    //    string Table = ADTable(AD);
    //    sb.AppendStatement(" SELECT O1.OTRegisterComp,O1.OTCompID,O1.OTFormNO,O1.OTEmpID,P.Name ");
    //    sb.Append(" ,O1.OTStartDate+'~'+isnull(O2.OTEndDate,O1.OTEndDate) as OTDate");
    //    sb.Append(" ,O1.OTStartDate,isnull(O2.OTEndDate,O1.OTEndDate) as OTEndDate");
    //    sb.Append(" ,O1.OTStartTime,isnull(O2.OTEndTime,O1.OTEndTime) as OTEndTime");
    //    sb.Append("  FROM " + Table + " O1");
    //    sb.Append("  left join " + Table + " O2   on  O1.OTTxnID=O2.OTTxnID and O2.OTSeqNo='2'  ");
    //    sb.Append("  left join " + _eHRMSDB + ".dbo.Personal P on P.EmpID=O1.OTEmpID and P.CompID=O1.OTCompID");
    //    sb.Append("  where O1.OTSeqNo='1'and O1.FlowCaseID=").AppendParameter("FlowCase", FlowCase);

    //    return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    //}
    //
    /// <summary>
    /// 更新AD的Table狀態(2:送審 3:核准 4:駁回)
    /// </summary>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="OTStatus">狀態</param>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="sb">回傳SQL語法</param>
    private void UpdateOverTime(string AD, string OTStatus, string FlowCaseID, ref CommandHelper sb)
    {
        AD = CustVerify.ADTable(AD);
        sb.Append(" UPDATE " + AD + " SET OTStatus='" + OTStatus + "',");
        //審核人員
        sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "' ");
        //sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "', ");
        ////最後異動人員
        //sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        //sb.Append(" LastChgID='" + UserInfo.getUserInfo().UserID + "', ");
        //sb.Append(" LastChgComp='" + UserInfo.getUserInfo().CompID + "' ");
        sb.Append(" WHERE FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
    }
    private void AfterReject_CheckAndInsert(string FlowCaseID, ref CommandHelper strSQL)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //sb.Append(" SELECT DISTINCT OTFromAdvanceTxnId ");
        //sb.Append(" FROM OverTimeDeclaration");
        //sb.Append(" WHERE FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
        //sb.Append(" and OTSeqNo='1' ");
        /*================*/
        sb.Append(" SELECT isnull(A.FlowCaseID,'') as AFlowCaseID,D.FlowCaseID as DFlowCaseID ");
          sb.Append(" FROM OverTimeDeclaration D ");
          sb.Append(" left join OverTimeAdvance A on D.OTFromAdvanceTxnId=A.OTTxnID and A.OTSeqNo='1' and A.OTTxnID!='' ");
          sb.Append(" WHERE D.FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
          sb.Append(" and D.OTSeqNo='1' ");
          string AFlowCaseID = db.ExecuteScalar (sb.BuildCommand()).ToString();
          if (AFlowCaseID.Length > 0) insertData(AFlowCaseID, ref strSQL);
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
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
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
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
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
        DataTable dtLastAssign = HROverTimeLogAD(FlowCase, AD);
        DataTable dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(FlowCase, AD);
        DataTable dtMail;
        string OTEmpID = dtOverTime.Rows[0]["OTEmpID"].ToString();
        string OTCompID = dtOverTime.Rows[0]["OTCompID"].ToString();
        string AssignID = dtLastAssign.Rows[0]["SignID"].ToString();
        string AssignCompID = dtLastAssign.Rows[0]["SignIDComp"].ToString();
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
                dtMail = getMailInfo(AssignCompID, AssignID);
                if (dtMail.Rows.Count > 0)
                {
                    Mail1 = dtMail.Rows[0]["EMail"].ToString();
                    //Name1 = Maildt.Rows[0]["Name"].ToString();
                }
                Subject2 = "加班單流程案件核准通知";//(申請人)
                Title2 = "您" + FromDT + "的加班單已『核准』!";
                dtMail = getMailInfo(OTCompID, OTEmpID);
                if (dtMail.Rows.Count > 0)
                {
                    Mail2 = dtMail.Rows[0]["EMail"].ToString();
                    //Name2 = Maildt.Rows[0]["Name"].ToString();
                }
            }
            else //非最後一關+待辦
            {
                Subject1 = "加班單流程案件簽核通知";//(主管)
                Title1 = "您簽核的加班單已送簽至下一關主管簽核";
                dtMail = getMailInfo(AssignCompID, AssignID);
                if (dtMail.Rows.Count > 0)
                {
                    Mail1 = dtMail.Rows[0]["EMail"].ToString();
                    //Name1 = Maildt.Rows[0]["Name"].ToString();
                }

                Subject2 = "加班單流程案件核准通知";//(申請人)
                Title2 = "您" + FromDT + "的加班單已送簽下一關主管簽核";
                dtMail = getMailInfo(OTCompID, OTEmpID);
                if (dtMail.Rows.Count > 0)
                {
                    Mail2 = dtMail.Rows[0]["EMail"].ToString();
                    //Name2 = Maildt.Rows[0]["Name"].ToString();
                }

                Subject3 = "加班單流程待辦案件通知"; //(主管)
                dtMail = getMailInfo(nextAssignCompID, nextAssignID);
                if (dtMail.Rows.Count > 0)
                {
                    Mail3 = dtMail.Rows[0]["EMail"].ToString();
                    //Name3 = Maildt.Rows[0]["Name"].ToString();
                    Title3 = dtMail.Rows[0]["Name"].ToString(); //代辦第一個欄位直接放BossName
                }
            }
        }
        else if (OTStatus == "4")//駁回
        {
            Subject1 = "加班單流程案件駁回通知";//(主管)
            Title1 = "您簽核的加班單已完成『駁回』!";
            dtMail = getMailInfo(AssignCompID, AssignID);
            if (dtMail.Rows.Count > 0)
            {
                Mail1 = dtMail.Rows[0]["EMail"].ToString();
                //Name1 = Maildt.Rows[0]["Name"].ToString();
            }

            Subject2 = "加班單流程案件駁回通知";//(申請人)
            Title2 = "您" + FromDT + "的加班單已被『駁回』!";
            dtMail = getMailInfo(OTCompID, OTEmpID);
            if (dtMail.Rows.Count > 0)
            {
                Mail2 = dtMail.Rows[0]["EMail"].ToString();
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

        if (dtOverTime.Rows.Count > 0)
        {
            //主管
            Content1 =
                    "NoticeOverTime" +
                    "||BM@QuitMailContent1||" + Title1 +
                    "||BM@QuitMailContent2||" + dtOverTime.Rows[0]["OTFormNo"].ToString() +
                    "||BM@QuitMailContent3||" + dtOverTime.Rows[0]["OTEmpID"].ToString() +
                    "||BM@QuitMailContent4||" + dtOverTime.Rows[0]["Name"].ToString() +
                    "||BM@QuitMailContent5||" + dtOverTime.Rows[0]["OTDate"].ToString() +
                    "||BM@QuitMailContent6||" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                    "||BM@QuitMailContent7||" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            if (Mail1 != "")
            {
                InsertMailLogCommand("人力資源處", AssignCompID, AssignID, Mail1, Subject1, Content1, false, ref sb2);
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
                    "||BM@QuitMailContent2||" + dtOverTime.Rows[0]["OTFormNo"].ToString() +
                    "||BM@QuitMailContent3||" + dtOverTime.Rows[0]["OTEmpID"].ToString() +
                    "||BM@QuitMailContent4||" + dtOverTime.Rows[0]["Name"].ToString() +
                    "||BM@QuitMailContent5||" + dtOverTime.Rows[0]["OTDate"].ToString() +
                    "||BM@QuitMailContent6||" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                    "||BM@QuitMailContent7||" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            if (Mail2 != "")
            {
                InsertMailLogCommand("人力資源處", OTCompID, OTEmpID, Mail2, Subject2, Content2, false, ref sb2);
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
            //下一關主管
            if (OTStatus == "2" || OTStatus == "3")//核准
            {
                if (!isLastFlow && nextAssignID != "") //最後一關結案
                {
                    Content3 =
                            "OverTimeTodoList" +
                            "||BM@QuitMailContent1||" + Title3 +
                            "||BM@QuitMailContent2||" + dtOverTime.Rows[0]["OTFormNo"].ToString() +
                            "||BM@QuitMailContent3||" + dtOverTime.Rows[0]["OTEmpID"].ToString() +
                            "||BM@QuitMailContent4||" + dtOverTime.Rows[0]["Name"].ToString() +
                            "||BM@QuitMailContent5||" + dtOverTime.Rows[0]["OTDate"].ToString() +
                            "||BM@QuitMailContent6||" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTStartTime"].ToString().Substring(2, 2) +
                            "||BM@QuitMailContent7||" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + "：" + dtOverTime.Rows[0]["OTEndTime"].ToString().Substring(2, 2) +
                            "||BM@QuitMailContent8||" + "Total";
                    if (Mail3 != "")
                    {
                        InsertMailLogCommand("人力資源處", nextAssignCompID, nextAssignID, Mail3, Subject3, Content3, false, ref sb2);
                        try
                        {
                            db.ExecuteNonQuery(sb2.BuildCommand());
                        }
                        catch (Exception ex)
                        {
                            Util.MsgBox("下一關主管E-Mail寄送失敗-" + ex);
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
    //private DataTable HROverTimeLog(string FlowCaseID, bool notSP)
    //{
    //    DbHelper db = new DbHelper(Aattendant._AattendantDBName);
    //    CommandHelper sb = db.CreateCommandHelper();
    //    if (notSP)
    //        sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2','4') order by Seq desc");
    //    else
    //        sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
    //    return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    //}
    /// <summary>
    ///遇到什麼奇怪狀況沒辦法知道現在關卡實際簽核人(非代理人)時，使用
    /// </summary>
    private string getLastAssignTo(string FlowCaseID)
    {
        return CustVerify.HROverTimeLog(FlowCaseID, false).Rows[0]["SignID"].ToString();
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

    private void oAssDicCheck(string SignID, string SignIDComp, ref  Dictionary<string, string> oAssDic)
{
    if (SignID.Trim().Equals(UserInfo.getUserInfo().UserID.Trim()))
        {
            oAssDic.Clear();
        }
        else
        {
            oAssDic = CustVerify.getEmpID_Name_Dictionary(SignID, SignIDComp);
        }
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

        Dictionary<string, string> toUserData = (Dictionary<string, string>)Session["toUserData"];

        //EmpInfo.QueryOrganData || EmpInfo.QueryFlowOrganData 使用
        //string SignOrganID = "", SignID = "", SignIDComp = "";

        if (!IsPostBack)
        {
            if (Session["FlowVerifyInfo"] != null)
            {
                #region"變數"
                //TrtCatchIsFlowVerify()使用
                DbHelper db = new DbHelper(Aattendant._AattendantDBName);
                CommandHelper sb = db.CreateCommandHelper();
                DbConnection cn = db.OpenConnection();
                DbTransaction tx = cn.BeginTransaction();

                //撈取該筆加班單相關資料
                DataTable dtOverTime;

                //預設下關待辦人(會變，沒變代表沒找到相關資料)
                Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
                Session["FlowVerifyInfo"] = null;
                Dictionary<string, string> oAssDic = Util.getDictionary(oVerifyInfo["FlowStepAssignToList"]);
                
                //共用檔
                Aattendant _Aattendant = new Aattendant();

                //Para撈取參數設定
                DataTable dtOverTimePara_All = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];

                string AD="";
                #endregion"變數"
                switch (oVerifyInfo["FlowStepBtnID"].ToString())
                {
                    case "btnClose":
                    case "btnReApprove":
                    case "FlowReassign":
                    case "btnApprove":  //審核
                        //單筆審核(使用oFlow.FlowCaseID判斷)
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null)
                        {
                            #region"單筆審核"
                            //給IsFlowVerify的下一關簽核主管
                            //oAssDicCheck(toUserData["SignID"], toUserData["SignIDComp"], ref oAssDic);
                            oAssDic = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);
                            switch (oFlow.FlowCurrStepID)
                            {
                                    //事先
                                case "A10":
                                case "A20":
                                   AD="A";
                                    break;
                                    //事後
                                case "A30":
                                case "A40":
                                    AD="D";
                                    break;
                            }
                            dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(oFlow.FlowCaseID, AD);
                            if (oVerifyInfo["FlowStepBtnID"].ToString() == "btnClose") //結案
                            {
                                sb.Reset();
                                UpdateOverTime(AD, "3", oFlow.FlowCaseID, ref sb);
                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                if(AD=="A")insertData(oFlow.FlowCaseID, ref sb);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], oVerifyInfo["FlowStepBtnID"], oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0], true))
                                {
                                    MailLogContent(oFlow.FlowCaseID, "3", AD, "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                            }
                            else //沒結案
                            {
                                sb.Reset();
                                DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(oFlow.FlowCaseID, false);
                                UpdateOverTime(AD, "2", oFlow.FlowCaseID, ref sb);
                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "2", ref sb);
                                FlowUtility.InsertHROverTimeLogCommand(
                                    oFlow.FlowCaseID,
                                    addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                    FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                    AD,
                                    dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                    dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                    dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                    UserInfo.getUserInfo().UserID,
                                    dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                    dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                    addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                    toUserData["SignLine"],
                                    toUserData["SignIDComp"],
                                    toUserData["SignID"],
                                    toUserData["SignOrganID"],
                                    toUserData["SignFlowOrganID"],
                                    "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                    );

                                if (TryCatchIsFlowVerify(Request["FlowLogID"], oVerifyInfo["FlowStepBtnID"], oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0],true))
                                {
                                    MailLogContent(oFlow.FlowCaseID, "2", AD, "", "", false);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
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
                            #region"事先多筆"
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                AD = "A";
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    if (dtOverTimeAdvance.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);
                                        string btnName = dtOverTimeAdvance.Rows[i]["btnName"].ToString();
                                        //oAssDicCheck(dtOverTimeAdvance.Rows[i]["SignID"].ToString(), dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString(), ref oAssDic);
                                        oAssDic = CustVerify.getEmpID_Name_Dictionary(dtOverTimeAdvance.Rows[i]["SignID"].ToString(), dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString());
                                        if (btnName == "btnClose") //結案
                                        {

                                            sb.Reset();
                                            UpdateOverTime(AD, "3", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            CloseHROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                            insertData(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                            {
                                                MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "3", AD, "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                        else //沒結案
                                        {
                                            sb.Reset();
                                            DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), false);
                                            UpdateOverTime(AD, "2", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            FlowUtility.InsertHROverTimeLogCommand(
                                                dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(),
                                                addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                                AD,
                                                dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                                UserInfo.getUserInfo().UserID,
                                                dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                                dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                                addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                                //toUserData start
                                                dtOverTimeAdvance.Rows[i]["SignLine"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignIDComp"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignID"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignOrganID"].ToString(),
                                                dtOverTimeAdvance.Rows[i]["SignFlowOrganID"].ToString(),
                                                //toUserData end
                                                "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                                );

                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                            {
                                                MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "2", AD, "", "", false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion"事先多筆"
                            /*------------------*/
                            #region"事後多筆"
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                                AD = "D";
                                for (int i = 0; i < dtOverTimeDeclaration.Rows.Count; i++)
                                {
                                    if (dtOverTimeDeclaration.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "D"), true);
                                        string btnName = dtOverTimeDeclaration.Rows[i]["btnName"].ToString();
                                        //oAssDicCheck(dtOverTimeDeclaration.Rows[i]["SignID"].ToString(), dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString(), ref oAssDic);
                                        oAssDic = CustVerify.getEmpID_Name_Dictionary(dtOverTimeDeclaration.Rows[i]["SignID"].ToString(), dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString());
                                        if (btnName == "btnClose") //結案
                                        {
                                            sb.Reset();
                                            UpdateOverTime(AD, "3", dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            UpdateHROrverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            CloseHROverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                            
                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                            {
                                                MailLogContent(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "3", AD, "", "", true);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                        else //沒結案
                                        {
                                            sb.Reset();
                                            DataTable dtHROverTimeLog = CustVerify.HROverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), false);
                                            UpdateOverTime(AD, "2", dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                            UpdateHROrverTimeLog(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", ref sb);
                                            FlowUtility.InsertHROverTimeLogCommand(
                                                dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(),
                                                addone(dtHROverTimeLog.Rows[0]["FlowLogBatNo"].ToString()),
                                                FlowLogIDadd(dtHROverTimeLog.Rows[0]["FlowLogID"].ToString()),
                                                AD,
                                                dtHROverTimeLog.Rows[0]["OTEmpID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpOrganID"].ToString(),
                                                dtHROverTimeLog.Rows[0]["EmpFlowOrganID"].ToString(),
                                                UserInfo.getUserInfo().UserID,
                                                dtHROverTimeLog.Rows[0]["FlowCode"].ToString(),
                                                dtHROverTimeLog.Rows[0]["FlowSN"].ToString(),
                                                addone(dtHROverTimeLog.Rows[0]["FlowSeq"].ToString()),
                                                //toUserData start
                                                dtOverTimeDeclaration.Rows[i]["SignLine"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignIDComp"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignID"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignOrganID"].ToString(),
                                                dtOverTimeDeclaration.Rows[i]["SignFlowOrganID"].ToString(),
                                                //toUserData end
                                                "1", false, ref sb, int.Parse(dtHROverTimeLog.Rows[0]["Seq"].ToString()) + 1
                                                );

                                            if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, btnName, oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                            {
                                                MailLogContent(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "2", AD, "", "", false);
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                            }
                                            else
                                            {
                                                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion"事後多筆"
                        }
                        break;

                    /*=================================================*/
                    case "btnReject":  //駁回
                        if (dtOverTimeAdvance == null && dtOverTimeDeclaration == null) //true 印章 false 多筆審核按鈕
                        {
                            #region"單筆駁回"
                            switch (oFlow.FlowCurrStepID)
                            {
                                //事先
                                case "A10":
                                case "A20":
                                    AD = "A";
                                    break;
                                //事後
                                case "A30":
                                case "A40":
                                    AD = "D";
                                    break;
                            }
                                dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(oFlow.FlowCaseID, AD);
                                sb.Reset();
                                UpdateOverTime(AD, "4", oFlow.FlowCaseID, ref sb);
                                UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                if (AD == "D") AfterReject_CheckAndInsert(oFlow.FlowCaseID, ref sb);
                                //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                if (TryCatchIsFlowVerify(Request["FlowLogID"], "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTime.Rows[0],true))
                                {
                                    MailLogContent(oFlow.FlowCaseID, "4", AD, "", "", true);
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                }
                                else
                                {
                                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                }
                            #endregion"單筆駁回"
                        }

                        else //多筆審核按鈕
                        {
                            #region"多筆駁回"
                            if (dtOverTimeAdvance.Rows.Count > 0)
                            {
                                AD = "A";
                                for (int i = 0; i < dtOverTimeAdvance.Rows.Count; i++)
                                {
                                    if (dtOverTimeAdvance.Rows[i]["OTSeqNo"].ToString() == "1")
                                    {
                                        oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "A"), true);
                                        sb.Reset();
                                        UpdateOverTime(AD, "4", dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), ref sb);
                                        UpdateHROrverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "3", ref sb);
                                        CloseHROverTimeLog(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), AD, ref sb);
                                        //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                        if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeAdvance.Rows[i], false))
                                        {
                                            MailLogContent(dtOverTimeAdvance.Rows[i]["FlowCaseID"].ToString(), "4", AD, "", "", true);
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                        }
                                        else
                                        {
                                            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                        }
                                    }
                                }
                            }
                            if (dtOverTimeDeclaration.Rows.Count > 0)
                            {
                               AD = "D";
                               for (int i = 0; i < dtOverTimeDeclaration.Rows.Count; i++)
                               {
                                   if (dtOverTimeDeclaration.Rows[i]["OTSeqNo"].ToString() == "1")
                                   {
                                       oFlow = new FlowExpress(Aattendant._AattendantFlowID, getFlowLogID(dtOverTimeDeclaration.Rows[i]["FlowCaseID"].ToString(), "D"), true);
                                       sb.Reset();
                                       UpdateOverTime(AD, "4", oFlow.FlowCaseID, ref sb);
                                       UpdateHROrverTimeLog(oFlow.FlowCaseID, "3", ref sb);
                                       CloseHROverTimeLog(oFlow.FlowCaseID, AD, ref sb);
                                       AfterReject_CheckAndInsert(oFlow.FlowCaseID, ref sb);
                                       //ClearBtn(oFlow.FlowCurrLogAssignTo);
                                       if (TryCatchIsFlowVerify(oFlow.FlowCurrLastLogID, "btnReject", oAssDic, oVerifyInfo["FlowStepOpinion"].ToString().Replace("'", "''"), sb, out ErrMsg, dtOverTimeDeclaration.Rows[i], false))
                                       {
                                           MailLogContent(oFlow.FlowCaseID, "4", AD, "", "", true);
                                           labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "審核成功!");
                                       }
                                       else
                                       {
                                           labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "審核失敗");
                                       }
                                   }
                               }
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
    /// <param name="drOverTime">失敗清單，找尋單筆相關資料用</param>
    /// <param name="ADType">失敗清單，找尋單筆相關資料用</param>
    /// <returns></returns>
    private bool TryCatchIsFlowVerify(string FlowLogID, string btn, Dictionary<string, string> oAssDic, string FlowStepOpinion, CommandHelper sb, out string ErrMsg, DataRow drOverTime = null, bool isOnly = true)
    {
        ErrMsg = "";
        if(oAssDic.IsNullOrEmpty())
        {
            MsgOut(drOverTime, "查無下一關審核主管。", isOnly);
            return false;
        }
        else if (oAssDic.Count == 0)
        {
            MsgOut(drOverTime, "查無下一關審核主管。", isOnly);
            return false;
        }
        else if (oAssDic.Keys.First().Trim().Equals(UserInfo.getUserInfo().UserID.Trim()) 
            && !btn.Trim().Equals("btnClose") 
            && !btn.Trim().Equals("btnReject"))
        {
            MsgOut(drOverTime, "查無下一關審核主管。", isOnly);
            return false;
        }
        //測試用test
        //sb.Append("test我是來製作錯誤的!!");
        //if (btn == "btnClose") btn = "btnApprove";
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
                if (drOverTime != null)
                {
                    MsgOut(drOverTime, "IsFlowVerify執行出錯。", isOnly);
                }
                tx.Rollback();
                return false;
            }
        }
        catch (Exception ex)
        {
            ErrMsg = ex.ToString();
            if (drOverTime != null)
            {
                  MsgOut(drOverTime, "加班單相關Table新增、修改出錯。", isOnly);
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
    private void MsgOut(DataRow drOverTime, string Msg, bool isOnly)
    {
        if (isOnly)
        {
            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                    "-------------------------------------" + Environment.NewLine +
                    "公司：" + drOverTime["OTCompID"].ToString() + Environment.NewLine +
                    "加班人：" + drOverTime["OTEmpID"].ToString() + Environment.NewLine +
                    "起迄日期：" + drOverTime["OTDate"].ToString() + Environment.NewLine +
                    "開始時間：" + drOverTime["OTStartTime"].ToString() + Environment.NewLine +
                    "結束時間：" + drOverTime["OTEndTime"].ToString() + Environment.NewLine +
                    "錯誤原因：" + Msg;
        }
        else
        {
            txtErrMsg.Text = txtErrMsg.Text + Environment.NewLine +
                               "-------------------------------------" + Environment.NewLine +
                               "公司：" + drOverTime["CompID"].ToString() + Environment.NewLine +
                               "加班人：" + drOverTime["EmpID"].ToString() + Environment.NewLine +
                               "起迄日期：" + drOverTime["OTStartDate"].ToString() +
                               "～" + drOverTime["OTEndDate"].ToString() + Environment.NewLine +
                               "開始時間：" + drOverTime["OTStartTime"].ToString() + Environment.NewLine +
                               "結束時間：" + drOverTime["OTEndTime"].ToString() + Environment.NewLine +
                               "錯誤原因：" + Msg;
        }
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

    //protected void insertData(DataTable dt)
    //{
    //    DbHelper db = new DbHelper(Aattendant._AattendantDBName);
    //    CommandHelper sb = db.CreateCommandHelper();
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        DbConnection cn = db.OpenConnection();
    //        DbTransaction tx = cn.BeginTransaction();
    //        sb.Reset();
    //        //sb.Append(" SELECT * FROM OverTimeAdvance WHERE OTEmpID='" + dt.Rows[i]["EmpID"] + "' AND OTStartDate='" + dt.Rows[i]["OTStartDate"] + "' AND OTStartTime='" + dt.Rows[i]["OTStartTime"] + "' AND OTEndTime='" + dt.Rows[i]["OTEndTime"] + "'");
    //        sb.Append(" SELECT * FROM OverTimeAdvance WHERE FlowCaseID='" + dt.Rows[i]["FlowCaseID"] + "' AND OTSeqNo='" + dt.Rows[i]["OTSeqNo"] + "'");
    //        sb.Append(" Order by FlowCaseID,OTSeqNo asc ");
    //        using (DataTable dtOT = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
    //        {
    //            if (dtOT.Rows.Count > 0)
    //            {
    //                sb.Reset();
    //                //排列位置跟下方select相同
    //                sb.Append("  insert into OverTimeDeclaration (OTCompID,OTEmpID,OTStartDate,OTEndDate ");
    //                sb.Append("  ,OTSeq ");
    //                sb.Append(" ,OTTxnID ");
    //                sb.Append(" ,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID ");
    //                sb.Append(" ,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus ");
    //                sb.Append(" ,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment ");
    //                sb.Append(" ,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OverTimeFlag ");
    //                sb.Append(" ,ToOverTimeDate,ToOverTimeFlag,OTRejectDate,OTRejectID,OTGovernmentNo ");
    //                sb.Append(" ,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark ");
    //                sb.Append(" ,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");

    //                sb.Append("  select OTCompID,OTEmpID,OTStartDate,OTEndDate ");
    //                //seq
    //                sb.Append("  ,(select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) ");
    //                //OTTxnID開始
    //                sb.Append(" ,case OTSeqNo when'1'then OTCompID+OTEmpID+ ");
    //                sb.Append(" replace((select OTStartDate from OverTimeAdvance OA where OA.FlowCaseID=A.FlowCaseID and OA.OTSeqNo='1'),'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) as VARCHAR),2) ");
    //                sb.Append(" else (select Top 1 OTTxnID from OverTimeDeclaration where OTFromAdvanceTxnId=A.OTTxnID and OTSeqNo='1')end as OTTxnID ");
    //                //OTTxnID結束
    //                //--
    //                //sb.Append("  ,OTCompID+OTEmpID+replace((select OTStartDate from OverTimeAdvance OA where OA.FlowCaseID=A.FlowCaseID and OA.OTSeqNo='1'),'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=(select OTStartDate from OverTimeAdvance where FlowCaseID=A.FlowCaseID and OTSeqNo='1')) as VARCHAR),2) as OTTxnID ");
    //                //--
    //                //sb.Append("  ,OTCompID+OTEmpID+replace(OTStartDate,'/','')+ RIGHT('0'+cast((select isnull(MAX(OTSeq),'0')+1 from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=(select OTStartDate from OverTimeAdvance where FlowCaseID=A.FlowCaseID and OTSeqNo='1'))+1 as VARCHAR),2) as OTTxnID ");
    //                /*===================================*/
    //                //(select MAX(OTSeq) from OverTimeDeclaration D where D.OTCompID=A.OTCompID and D.OTEmpID=A.OTEmpID and D.OTStartDate=A.OTStartDate) as OTSeq
    //                //上面的OTSeq放入下面的OTSeq
    //                //OTCompID+OTEmpID+replace(OTStartDate,'/','')+ RIGHT('0'+cast(OTSeq as VARCHAR),2) as OTTxnID
    //                /*===================================*/
    //                sb.Append(" ,OTSeqNo,OTTxnID,DeptID,OrganID,DeptName,OrganName,'' ");
    //                sb.Append(" ,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,'' ");
    //                sb.Append(" ,'',MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment ");
    //                sb.Append(" ,OTFormNO,OTRegisterID,OTRegisterDate,'1',OTValidDate,OTValidID,'1' ");
    //                sb.Append(" ,'1900-01-01 00:00:00.000','0',OTRejectDate,OTRejectID,OTGovernmentNo ");
    //                sb.Append(" ,'0',HolidayOrNot,'1900-01-01 00:00:00.000','0','1900-01-01 00:00:00.000','' ");
    //                sb.Append(" ,'','','',LastChgComp,LastChgID,LastChgDate,OTRegisterComp ");
    //                sb.Append(" from OverTimeAdvance A ");
    //                sb.Append("where FlowCaseID='" + dt.Rows[i]["FlowCaseID"] + "' and OTSeqNo='" + dt.Rows[i]["OTSeqNo"] + "'");
    //                try
    //                {
    //                    db.ExecuteNonQuery(sb.BuildCommand(), tx);
    //                    tx.Commit();
    //                }
    //                catch (Exception ex)
    //                {
    //                    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
    //                    tx.Rollback();//資料更新失敗
    //                }
    //                finally
    //                {
    //                    cn.Close();
    //                    cn.Dispose();
    //                    tx.Dispose();
    //                }
    //            }
    //        }
    //    }
    //}

    #endregion"事先到事後的insert，Judy的微更動"
}