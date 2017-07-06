using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using System.Data;

/// <summary>
/// CustVerify 的摘要描述
/// </summary>
public class CustVerify
{
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private static string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    //private string _DBName2 = "DB_VacSys";
    public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
    public static string _HRDB = Util.getAppSetting("app://HRDB_OverTime/");
    private string FlowCustDB="";

    private static string FlowSignID_CompID = "";

    #region"給永豐流程找AssignTo用"
    
    public static string getFlowSignID_CompID()
    {
        return FlowSignID_CompID;
    }
    public static void setFlowSignID_CompID(string data)
    {
        FlowSignID_CompID = data;
    }

    public Dictionary<string, string> getOrganBoss()
    {
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        string[] SignID_CompID = FlowSignID_CompID.Split("|$|");
        string SignID = "";
        string CompID = "";
        for (int i = 0; i < SignID_CompID.Length; i++)
        {
            string[] data = SignID_CompID[i].Split(",");
            SignID = data[0] + "','";
            CompID = data[1] + "','";
        }
        _dic = getEmpID_Name_Dictionary(SignID, CompID);
        return _dic;
    }

    public static Dictionary<string, string> getEmpID_Name_Dictionary(string EmpID, string CompID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        sb.Append(" SELECT DISTINCT P.EmpID,O.OrganName+'-'+P.Name as Name ");
        sb.Append(" FROM " + _eHRMSDB + ".dbo. Personal P ");
        sb.Append(" left join " + _eHRMSDB + ".dbo. Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
        sb.Append(" WHERE P.EmpID in('").Append( EmpID).Append("')");
        sb.Append(" and P.CompID in('").Append(CompID).Append("')");
        //sb.Append(" UNION select  'Test' as EmpID , 'Test' as Name "); //測試用
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }
    #endregion"給永豐流程找AssignTo用"

    #region"雜物+小動作"
    /// <summary>
    /// 給FlowLogID加1動作
    /// </summary>
    public static string FlowLogIDadd(string FlowLogID)
    {
        return FlowLogID.Split('.')[0] + "." + FlowLogID.Split('.')[1] + "." + Convert.ToString(int.Parse(FlowLogID.Split('.')[2]) + 1).PadLeft(5, '0');
    }

    //public static string addone(string one)
    //{
    //    return Convert.ToString(int.Parse(one) + 1);
    //}
    //字串+1用
    public static string addone(string one)
    {
        int newone;
        if (int.TryParse(one, out newone))
            return Convert.ToString(newone + 1);
        else
            return "";
    }

    public static string ADTable(string AD)
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
    #endregion"雜物+小動作"

    #region"待辦畫面+審核畫面 的查找資料"
    /// <summary>
    /// 用FlowCaseID在OverTimeAorD找單筆資料，跨日會組合成一筆
    /// </summary>
    /// <param name="FlowCase">FlowCase</param>
    /// <param name="AD">"A","D"</param>
    /// <returns></returns>
    public static  DataTable OverTime_find_by_FlowCaseID(string FlowCase, string AD)
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
        sb.Append("  where O1.OTSeqNo='1'and O1.FlowCaseID=").AppendParameter("FlowCase", FlowCase);

        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }

    /// <summary>
    /// notSP:true 現在是改派人員，需要重複向上送時，撈取最近一筆非特殊人員(行政、功能或HR?)線資料
    /// notSP:false 直接找最新一筆(現在)關卡資訊
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="notSP">現在關卡是否為指定人員</param>
    /// <returns>HROverTimeLog整個Table</returns>
    public static DataTable HROverTimeLog(string FlowCaseID, bool notSP)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        if (notSP)
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2','4') order by Seq desc");
        else
            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }
    #endregion"待辦畫面+審核畫面 的查找資料"

    #region"審核Mail"
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
    public bool MailLogContent(string FlowCase, string OTStatus, string AD, string nextAssignID, string nextAssignCompID, bool isLastFlow)
    {
        //DataTable ADBatchdt = FlowCaseToAD(FlowCase, AD);
        DataTable dtLastAssign = CustVerify.HROverTimeLogAD(FlowCase, AD);
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
        sb.Append("where UG.CompID='SPHBK1' and UG.GroupID='ADM-OT'");
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
    #endregion"審核Mail"

    #region"多筆送簽往前搬移"
    /// <summary>
    /// 事先申請到事後申報的送簽單被駁回，須重寫一筆有OTFromAdvanceTxnId的暫存到事後申報，供使用者再送簽
    /// </summary>
    /// <param name="FlowCaseID">事後的</param>
    /// <param name="strSQL">要組合並一起執行的SQL字串</param>
    public static void AfterReject_CheckAndInsert(string FlowCaseID, ref CommandHelper strSQL)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        
        //sb.Append(" SELECT DISTINCT OTFromAdvanceTxnId ");
        //sb.Append(" FROM OverTimeDeclaration");
        //sb.Append(" WHERE FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
        //sb.Append(" and OTSeqNo='1' ");
        /*================*/
        sb.Append(" SELECT isnull(A.FlowCaseID,'') as AFlowCaseID ");
        sb.Append(" FROM OverTimeDeclaration D ");
        sb.Append(" left join OverTimeAdvance A on D.OTFromAdvanceTxnId=A.OTTxnID and A.OTSeqNo='1' and A.OTTxnID!='' ");
        sb.Append(" WHERE D.FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
        sb.Append(" and D.OTSeqNo='1' ");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            string AFlowCaseID = dt.Rows[0]["AFlowCaseID"].ToString();
            if (!string.IsNullOrEmpty(AFlowCaseID)) insertFromAdvanceToData(AFlowCaseID, ref strSQL);
        }
    }

    /// <summary>
    /// 更新AD的Table狀態(2:送審 3:核准 4:駁回)
    /// </summary>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="OTStatus">狀態</param>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="sb">回傳SQL語法</param>
    public static void UpdateOverTime(string AD, string OTStatus, string FlowCaseID, ref CommandHelper sb)
    {
        AD = CustVerify.ADTable(AD);
        sb.Append(" UPDATE " + AD + " SET OTStatus='" + OTStatus + "',");
        //審核人員
        sb.Append(" OTValidDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        sb.Append(" OTValidID='" + UserInfo.getUserInfo().UserID + "' ");
        ////最後異動人員(Kat說不更新)
        //sb.Append(" LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',");
        //sb.Append(" LastChgID='" + UserInfo.getUserInfo().UserID + "', ");
        //sb.Append(" LastChgComp='" + UserInfo.getUserInfo().CompID + "' ");
        sb.Append(" WHERE FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
    }

    /// <summary>
    /// 修改現在關卡在HROrverTimeLog的狀態，比照Ken的 0：申請送出, (1：簽核中), (2：同意), (3：駁回), 4：表單取消, 5：退闗
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="FlowStatus">狀態</param>
    public static void UpdateHROrverTimeLog(string FlowCaseID, string FlowStatus, ref  CommandHelper sb)
    {
        sb.Append("  UPDATE HROverTimeLog set FlowStatus='" + FlowStatus + "'  where FlowCaseID='" + FlowCaseID + "'  and Seq=  (select Top 1 Seq from HROverTimeLog  where FlowCaseID='" + FlowCaseID + "'  order by Seq desc) ; ");
    }

    /// <summary>
    /// 結案後HROverTimeLog的Flag解除
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="AD">A事先/D事後</param>
    /// <param name="sb">回傳SQL語法，統一給TryCatchIsFlowVerify執行</param>
    public static void CloseHROverTimeLog(string FlowCaseID, string AD, ref CommandHelper sb)
    {
        //AD = ADTable(AD);  //HROverTimeLogAD不要加
        DataTable dt = HROverTimeLogAD(FlowCaseID, AD);
        FlowUtility.ChangeFlowFlag(FlowCaseID, dt.Rows[0]["FlowCode"].ToString(), dt.Rows[0]["FlowSN"].ToString(), dt.Rows[0]["SignIDComp"].ToString(), dt.Rows[0]["SignID"].ToString(), "0", ref sb);
    }

    /// <summary>
    /// 依照事先事後找尋最近一筆(行政或功能)線資料，依照AD判別
    /// </summary>
    /// <param name="FlowCaseID">FlowCaseID</param>
    /// <param name="AD">A事先/D事後</param>
    /// <returns>全部欄位</returns>
    public static DataTable HROverTimeLogAD(string FlowCaseID, string AD)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'   and OTMode='" + AD + "' order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }

    /// <summary>
    ///Judy切割出來多筆審核撈取FlowLogID
    /// </summary>
    public static string getFlowLogID(string FlowCaseID, string AD)
    {
        FlowExpress tbFlow = new FlowExpress(Aattendant._AattendantFlowID);
        AD = CustVerify.ADTable(AD);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.AppendStatement(" SELECT top 1 AL.FlowLogID,* FROM " + AD + " OT  ");
        sb.Append("  LEFT JOIN " + tbFlow.FlowCustDB + "FlowFullLog AL ON OT.FlowCaseID=AL.FlowCaseID  ");
        sb.Append(" WHERE OT.FlowCaseID =").AppendParameter("FlowCaseID", FlowCaseID);
        sb.Append(" Order by AL.FlowLogID desc ");
        return db.ExecuteScalar(sb.BuildCommand()).ToString();
    }

    #region"事先到事後的insert，Judy的，只做微更動"
    public static void insertFromAdvanceToData(string flow, ref CommandHelper sb)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb2 = db.CreateCommandHelper();
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
    #endregion"多筆送簽往前搬移"
}
