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
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    //private string _DBName2 = "DB_VacSys";
    public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
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

    public static string addone(string one)
    {
        return Convert.ToString(int.Parse(one) + 1);
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

    #region"待辦畫面"
    #endregion"待辦畫面"
}
