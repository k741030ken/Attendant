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

    //public Dictionary<string, string> getOrganBoss_First_Close(string AssignTo, string CompID)
    //{
    //    DbHelper db = new DbHelper(Aattendant._AattendantDBName);
    //    CommandHelper sb = db.CreateCommandHelper();
    //    //為了迴避找不到主管使得按鈕生不出來，找現在本關主管
    //    DataTable dt;
    //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
    //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
    //    sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
    //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
    //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
    //    sb.Reset();
    //    Dictionary<string, string> _dic = new Dictionary<string, string>();
    //    _dic = Util.getDictionary(dt, 0, 1);
    //    return _dic;
    //}
    
    #region 20170207 leo 共用 (EmpID相關資料撈取)
//    #region"GetBoss更新，套用FlowUtility"
//    private DataTable HROverTimeLog(string FlowCaseID, bool notSP)
//    {
//        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
//        CommandHelper sb = db.CreateCommandHelper();
//        if (notSP)
//            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2') order by Seq desc");
//        else
//            sb.Append(" select Top 1 * from HROverTimeLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
//        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
//    }
//   /// <summary>
//   /// 舊版撈取主管資訊，之後將會被AbtnAdd與DbtnAdd取代
//   /// </summary>
//   /// <param name="CompID"></param>
//   /// <param name="AssignTo"></param>
//   /// <param name="OTStartDate"></param>
//   /// <param name="FlowCaseID"></param>
//   /// <returns></returns>
//    public Dictionary<string, string> getOrganBoss(string CompID, string AssignTo, string OTStartDate, string FlowCaseID)
//    {
//        //DataTable HRdt = HROverTimeLog(FlowCaseID, false);
//        //Dictionary<string, string> toUserData;
//        //bool isLastFlow, nextIsLastFlow;
//        //string flowCodeFlag="",
//        //    flowCode = "",
//        //    flowSN = "",
//        //    signLineDefine = "",
//        //    meassge = "";

//        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
//        CommandHelper sb = db.CreateCommandHelper();
//        //為了迴避找不到主管使得按鈕生不出來，找現在本關主管
//        DataTable dt;
//        sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
//        sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
//        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
//        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        sb.Reset();


/////*=========================================================*/
////        Dictionary<string, string> isnull=new Dictionary<string, string>();
////        if (CompID == "") return isnull;
////        if (HRdt.Rows.Count>0)flowCodeFlag = HRdt.Rows[0]["flowCodeFlag"].ToString() == "OT01" ? "0" : "1";
////        FlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, OTStartDate,FlowCaseID, flowCodeFlag,
////out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

////        if (toUserData.Count == 0 && HRdt.Rows.Count > 0)
////        {
////            toUserData.Add("SignLine", HRdt.Rows[0]["SignLine"].ToString());
////            toUserData.Add("SignIDComp", HRdt.Rows[0]["SignIDComp"].ToString());
////            toUserData.Add("SignID", AssignTo);
////            toUserData.Add("SignOrganID", HRdt.Rows[0]["SignOrganID"].ToString());
////            toUserData.Add("SignFlowOrganID", HRdt.Rows[0]["SignFlowOrganID"].ToString());
////        }

////        string SignOrganID="", SignID="",SignIDComp="";
////        if (toUserData.Count == 0)
////        {
////            toUserData["SignID"] = AssignTo;
////            toUserData["SignIDComp"] = CompID;
////            toUserData["SignOrganID"] = "";
////            toUserData["SignFlowOrganID"] = "";
////        }
////        else if (HRdt.Rows[0]["SignLine"].ToString() == "2")
////            {
////                if (EmpInfo.QueryFlowOrganData(HRdt.Rows[0]["SignFlowOrganID"].ToString(), OTStartDate, out SignOrganID, out SignID, out SignIDComp))
////                {
////                    toUserData["SignID"] = SignID;
////                    toUserData["SignIDComp"] = SignIDComp;
////                    toUserData["SignOrganID"] = "";
////                    toUserData["SignFlowOrganID"] = SignOrganID;
////                }
////            }
////            else
////            {
////                if (EmpInfo.QueryOrganData(HRdt.Rows[0]["SignCompID"].ToString(), HRdt.Rows[0]["SignOrganID"].ToString(), OTStartDate, out SignOrganID, out SignID, out SignIDComp))
////                {
////                    toUserData["SignID"] = SignID;
////                    toUserData["SignIDComp"] = SignIDComp;
////                    toUserData["SignOrganID"] = SignOrganID;
////                    toUserData["SignFlowOrganID"] = "";
////                }
////            }
//        /*========================================================================*/

//        //DataTable dt;
//        //sb.Append(" SELECT Top 1 P.EmpID, P.LogDate, (DATEDIFF(day,P.LogDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
//        //sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
//        //sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
//        //sb.Append(" AND P.LogDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
//        //sb.Append(" ORDER BY MinDiff ");
//        //dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //sb.Reset();
//        //if (dt.Rows.Count == 0 && DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
//        //{
//        //    dt.Reset();
//        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name, (DATEDIFF(day,O.SysDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
//        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
//        //    sb.Append(" left join " + _eHRMSDB + ".dbo.Organization_History O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
//        //    sb.Append(" AND O.SysDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
//        //    sb.Append(" ORDER BY MinDiff ");
//        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //    sb.Reset();
//        //    if (dt.Rows.Count == 0)
//        //    {
//        //        dt.Reset();
//        //        sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
//        //        sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
//        //        sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        //        sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
//        //        dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //        sb.Reset();
//        //    }
//        //}
//        //else if (dt.Rows.Count == 0)
//        //{
//        //    dt.Reset();
//        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
//        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Personal P ");
//        //    sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "'");
//        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //    sb.Reset();
//        //}
//        //else if (dt.Rows.Count > 0)
//        //{
//        //    string sLogDate = dt.Rows[0]["LogDate"].ToString();
//        //    dt.Reset();
//        //    sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name, (DATEDIFF(day,O.SysDate,'" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "')) AS MinDiff ");
//        //    sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
//        //    sb.Append(" left join " + _eHRMSDB + ".dbo. Organization_History O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        //    sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "' and P.LogDate='" + DateTime.Parse(sLogDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
//        //    sb.Append(" AND O.SysDate = '" + DateTime.Parse(OTStartDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "' ");
//        //    sb.Append(" ORDER BY MinDiff ");
//        //    dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //    sb.Reset();
//        //    if (dt.Rows.Count == 0)
//        //    {
//        //        if (DateTime.Parse(inDate).CompareTo(DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd"))) >= 0)
//        //        {
//        //            dt.Reset();
//        //            sb.Append(" SELECT Top 1 P.EmpID,O.OrganName+'-'+P.NameN as Name ");
//        //            sb.Append(" FROM " + _eHRMSDB + ".dbo.Staff_History_All P ");
//        //            sb.Append(" left join " + _eHRMSDB + ".dbo.Organization O on O.CompID=P.CompID and O.OrganID=P.DeptID");
//        //            sb.Append(" WHERE P.EmpID='" + AssignTo + "' and P.CompID='" + CompID + "' and P.LogDate='" + DateTime.Parse(sLogDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
//        //            dt = db.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
//        //            sb.Reset();
//        //        }
                
//        //    }
//        //}
        
//        Dictionary<string, string> _dic = new Dictionary<string, string>();
//        _dic = Util.getDictionary(dt, 0, 1);
//        return _dic;

// }
//    #endregion""
//    /*==============================================*/

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