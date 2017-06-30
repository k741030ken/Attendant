using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Common.Properties;
using System.Web.UI.WebControls;

/// <summary>
/// SBUtility 的摘要描述
/// </summary>
public class SBUtility
{
    #region "全域變數"

    public static string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");

    #endregion

    public SBUtility()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    #region "附件相關"

    /// <summary>
    /// 刪除附件
    /// </summary>
    /// <param name="AttachID"></param>
    /// <returns></returns>
    public bool DeleteAttach(string AttachID)
    {
        bool isSuccess = false;

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper strSQL = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();

            strSQL.Reset();
            strSQL.AppendStatement(" UPDATE AttachInfo SET FileSize = -1,  FileBody = null WHERE AttachID = '" + AttachID + "'");

            using (DataTable dtSchedule = db.ExecuteDataSet(strSQL.BuildCommand()).Tables[0])
            {
                db.ExecuteNonQuery(strSQL.BuildCommand(), tx);
                tx.Commit();

                try
                {
                    Util.IsAttachInfoLog("AattendantDB", AttachID, 1, "Delete");
                    isSuccess = true;
                }
                catch (Exception es)
                {
                    Debug.Print(">>>IsAttachInfoLog: " + es.Message);
                    LogHelper.WriteSysLog(es.Message); //將 Exception 丟給 Log 模組
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>DeleteAttach: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
        }

        return isSuccess;
    }

    /// <summary>
    /// 新增附件
    /// </summary>
    /// <param name="AttachID"></param>
    /// <param name="attach"></param>
    /// <returns></returns>
    public bool InsertAttach(string AttachID, string attach)
    {
        bool isSuccess = false;

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper strSQL = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();

            strSQL.Reset();
            strSQL.AppendStatement(" UPDATE AttachInfo SET FileSize = -1,  FileBody = null WHERE AttachID = '" + AttachID + "'");
            strSQL.AppendStatement(" INSERT INTO AttachInfo (AttachID, SeqNo, FileName, FileExtName, FileSize, AnonymousAccess, UpdUser, UpdDate, UpdTime, FileBody, MD5Check) ");
            strSQL.Append(" SELECT '" + AttachID + "'");
            strSQL.Append(" , SeqNo, FileName, FileExtName, FileSize, AnonymousAccess, UpdUser, UpdDate, UpdTime, FileBody, MD5Check ");
            strSQL.Append(" FROM AttachInfo ");
            strSQL.Append(" WHERE AttachID = '" + attach + "'");

            using (DataTable dtSchedule = db.ExecuteDataSet(strSQL.BuildCommand()).Tables[0])
            {
                db.ExecuteNonQuery(strSQL.BuildCommand(), tx);
                tx.Commit();
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>InsertAttach: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
        }

        return isSuccess;
    }

    #endregion

    /// <summary>
    /// 取得員工基本資料
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <returns>DataTable</returns>
    public DataTable GetEmpData(string compID, string empID)
    {
        try
        {
            DbHelper db = new DbHelper(_eHRMSDB);
            CommandHelper strSQL = db.CreateCommandHelper();
            strSQL.Reset();
            strSQL.AppendStatement(" SELECT P.EmpID, P.Sex, P.RankID, P.EmpDate, P.WorkSiteID, P.DeptID, OD.OrganName AS DeptName, P.OrganID, O.OrganName ");
            strSQL.Append(" FROM Personal P ");
            strSQL.Append(" LEFT JOIN Organization OD ON P.CompID = OD.CompID AND P.DeptID = OD.OrganID AND OD.VirtualFlag='0' AND OD.InValidFlag = '0' ");
            strSQL.Append(" LEFT JOIN Organization O  ON P.CompID = O.CompID AND P.OrganID = O.OrganID AND O.VirtualFlag='0' AND O.InValidFlag = '0' ");
            strSQL.Append(" WHERE P.CompID ='" + compID + "'");
            strSQL.Append(" AND P.EmpID = '" + empID + "'");
            return db.ExecuteDataSet(strSQL.BuildCommand()).Tables[0];
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetEmpData: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            throw new Exception(ex.Message);
        }
    }

    #region "下拉選單"

    /// <summary>
    /// 顯示方式
    /// </summary>
    public enum DisplayType
    {
        OnlyName,
        //只顯示名字
        OnlyID,
        //顯示ID  
        Full
        //顯示ID + 名字
    }

    /// <summary>
    /// 組成下拉選單Function
    /// </summary>
    /// <param name="objDDL">選單物件</param>
    /// <param name="strConn">Datebase Connection名稱</param>
    /// <param name="strTabName">Table名稱</param>
    /// <param name="strValue">Value欄位名稱</param>
    /// <param name="strText">Text欄位名稱</param>
    /// <param name="Type">(非必要參數) 選單文字呈現方式，預設為DisplayType.Full</param>
    /// <param name="JoinStr">(非必要參數) 需額外Join的語法ex."Left Join XXX a On a.OOO = b.OOO"，預設為空值</param>
    /// <param name="WhereStr">(非必要參數) 需額外Where的語法ex."And XXX=OOO"，預設為空值</param>
    /// <param name="OrderByStr">(非必要參數) 需額外OrderBy的語法ex."Order By XXX"，預設為Order By strValue</param>
    public static void FillDDL(DropDownList objDDL, string strConn, string strTabName, string strValue, string strText, DisplayType Type = DisplayType.Full, string JoinStr = "", string WhereStr = "", string OrderByStr = "")
    {
        SBUtility objUt = new SBUtility();
        try
        {
            using (DataTable dt = objUt.GetDDLInfo(strConn, strTabName, strValue, strText, JoinStr, WhereStr, OrderByStr))
            {
                var _with1 = objDDL;
                _with1.Items.Clear();
                _with1.DataSource = dt;
                switch (Type)
                {
                    case DisplayType.OnlyID:
                        _with1.DataTextField = "Code";
                        break;
                    case DisplayType.OnlyName:
                        _with1.DataTextField = "CodeName";
                        break;
                    default:
                        _with1.DataTextField = "FullName";
                        break;
                }
                _with1.DataValueField = "Code";
                _with1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>FillDDL: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// 查詢選項資料
    /// </summary>
    /// <param name="strConn">Datebase Connection名稱</param>
    /// <param name="strTabName">Table名稱</param>
    /// <param name="strValue">Value欄位名稱</param>
    /// <param name="strText">Text欄位名稱</param>
    /// <param name="JoinStr">(非必要參數) 需額外Join的語法ex."Left Join XXX a On a.OOO = b.OOO"，預設為空值</param>
    /// <param name="WhereStr">(非必要參數) 需額外Where的語法ex."And XXX=OOO"，預設為空值</param>
    /// <param name="OrderByStr">(非必要參數) 需額外OrderBy的語法ex."Order By XXX"，預設為Order By strValue</param>
    /// <returns></returns>
    public DataTable GetDDLInfo(string strConn, string strTabName, string strValue, string strText, string JoinStr = "", string WhereStr = "", string OrderByStr = "")
    {
        try
        {
            StringBuilder strSQL = new StringBuilder();
            strSQL.Clear();
            strSQL.AppendLine("Select " + strValue + " AS Code");
            if (!string.IsNullOrEmpty(strText))
            {
                strSQL.AppendLine(", " + strText + " AS CodeName, " + strValue.Replace("distinct", "") + " + '-' + " + strText + " AS FullName ");
            }
            strSQL.AppendLine("FROM " + strTabName);
            if (!string.IsNullOrEmpty(JoinStr))
            {
                strSQL.AppendLine(JoinStr);
            }
            strSQL.AppendLine("Where 1=1");
            if (!string.IsNullOrEmpty(WhereStr))
            {
                strSQL.AppendLine(WhereStr);
            }
            if (!string.IsNullOrEmpty(OrderByStr))
            {
                strSQL.AppendLine(OrderByStr);
            }
            else
            {
                strSQL.AppendLine("Order By " + strValue.Replace("distinct", ""));
            }

            DbHelper db = new DbHelper(strConn);
            using (DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL.ToString()).Tables[0])
            {
                return dt;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetDDLInfo: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region "取得班表"

    /// <summary>
    /// 取得班表
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="schedule">班表</param>
    /// <returns>bool</returns>
    /// <remarks>
    /// 1. 取得班表順序:值勤 > 個人 > 公司
    /// 2. 時間格式為HHmm,譬如早上六點半就是0630
    /// </remarks>
    public bool GetSchedule(string compID, string empID, string SBStartDate, string SBEndDate, out StringBuilder errMsg, out DataTable schedule)
    {
        errMsg = new StringBuilder();
        schedule = new DataTable();

        try
        {
            if (!GetDutySchedule(compID, empID, SBStartDate, SBEndDate, out errMsg, out schedule))
            {
                if (!GetPersonalSchedule(compID, empID, SBStartDate, SBEndDate, out errMsg, out  schedule))
                {
                    if (!GetCompSchedule(compID, empID, out errMsg, out schedule))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetSchedule: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            schedule = null;
            return false;
        }
    }

    /// <summary>
    /// 值勤班表
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <param name="SBStartDate">留守起日</param>
    /// <param name="SBEndDate">留守迄日</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="schedule">班表</param>
    /// <returns></returns>
    private bool GetDutySchedule(string compID, string empID, string SBStartDate, string SBEndDate, out StringBuilder errMsg, out DataTable schedule)
    {
        errMsg = new StringBuilder();
        schedule = new DataTable();

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();
            sb.AppendStatement("SELECT");
            sb.Append(" WTBeginTime AS BeginTime, WTEndTime AS EndTime ");
            sb.Append(" FROM EmpGuardWorkTime ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND CompID = '" + compID + "' ");
            sb.Append(" AND EmpID = '" + empID + "' ");
            sb.Append(" AND DutyDate BETWEEN '" + SBStartDate + "' AND '" + SBEndDate + "' ");

            using (DataTable dtSchedule = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                schedule = dtSchedule;
                return dtSchedule.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetDutySchedule: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            schedule = null;
            return false;
        }
    }

    /// <summary>
    /// 取得個人班表
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <param name="SBStartDate">留守起日</param>
    /// <param name="SBEndDate">留守迄日</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="schedule">班表</param>
    /// <remarks>StartDate:生效日</remarks>
    /// <returns>bool</returns>
    private bool GetPersonalSchedule(string compID, string empID, string SBStartDate, string SBEndDate, out StringBuilder errMsg, out DataTable schedule)
    {
        errMsg = new StringBuilder();
        schedule = new DataTable();

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();

            sb.AppendStatement(" SELECT TOP 1 WT.BeginTime, WT.EndTime FROM EmpWorkTimeLog EL ");
            sb.Append(" LEFT JOIN [" + _eHRMSDB + "].[dbo].[WorkTime] WT ON WT.CompID = EL.CompID AND WT.WTID = EL.WTID ");
            sb.Append(" WHERE 1 = 1 ");
            sb.Append(" AND EL.CompID = '" + compID + "' ");
            sb.Append(" AND EL.EmpID = '" + empID + "' ");
            sb.Append(" AND CONVERT(VARCHAR, EL.StartDate, 111) <= '" + SBStartDate + "' ");
            sb.Append(" AND EL.LastChgDate = ( ");
            sb.Append(" SELECT TOP 1 LastChgDate FROM EmpWorkTimeLog WHERE 1 = 1 ");
            sb.Append(" AND CONVERT(VARCHAR, StartDate, 111) <= '" + SBStartDate + "' ");
            sb.Append(" AND ActionFlag <> 'D' ");
            sb.Append(" AND CompID = '" + compID + "' ");
            sb.Append(" AND EmpID = '" + empID + "' ");
            sb.Append(" ORDER BY StartDate DESC, LastChgDate DESC) ");

            //留守起迄日不同天需再取得迄日的班表
            if (!SBStartDate.Equals(SBEndDate))
            {
                sb.Append(" UNION ALL ");
                sb.Append(" SELECT TOP 1 WT.BeginTime, WT.EndTime FROM EmpWorkTimeLog EL ");
                sb.Append(" LEFT JOIN [" + _eHRMSDB + "].[dbo].[WorkTime] WT ON WT.CompID = EL.CompID AND WT.WTID = EL.WTID ");
                sb.Append(" WHERE 1 = 1 ");
                sb.Append(" AND EL.CompID = '" + compID + "' ");
                sb.Append(" AND EL.EmpID = '" + empID + "' ");
                sb.Append(" AND CONVERT(VARCHAR, EL.StartDate, 111) <= '" + SBEndDate + "' ");
                sb.Append(" AND EL.LastChgDate = ( ");
                sb.Append(" SELECT TOP 1 LastChgDate FROM EmpWorkTimeLog WHERE 1 = 1 ");
                sb.Append(" AND CONVERT(VARCHAR, StartDate, 111) <= '" + SBEndDate + "' ");
                sb.Append(" AND ActionFlag <> 'D' ");
                sb.Append(" AND CompID = '" + compID + "' ");
                sb.Append(" AND EmpID = '" + empID + "' ");
                sb.Append(" ORDER BY StartDate DESC, LastChgDate DESC) ");
            }

            using (DataTable dtSchedule = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                schedule = dtSchedule;
                return dtSchedule.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetPersonalSchedule: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            schedule = null;
            return false;
        }
    }

    /// <summary>
    /// 公司班表
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="schedule">班表</param>
    /// <returns>bool</returns>
    /// <remarks>目前不曉得該怎取得過去日期的班表，日後可能需要再修改</remarks>
    private bool GetCompSchedule(string compID, string empID, out StringBuilder errMsg, out DataTable schedule)
    {
        errMsg = new StringBuilder();
        schedule = new DataTable();

        try
        {
            DbHelper db = new DbHelper(_eHRMSDB);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();
            sb.AppendStatement("SELECT");
            sb.Append(" WT.BeginTime, WT.EndTime, WT.RestBeginTime, WT.RestEndTime ");
            sb.Append(" FROM PersonalOther PO ");
            sb.Append(" LEFT JOIN WorkTime WT ON WT.CompID = PO.CompID AND WT.WTID = PO.WTID ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND PO.CompID = '" + compID + "' ");
            sb.Append(" AND PO.EmpID = '" + empID + "' ");
            sb.Append(" ; ");

            using (DataTable dtSchedule = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                schedule = dtSchedule;
                return dtSchedule.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetCompSchedule: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            schedule = null;
            return false;
        }
    }

    #endregion

    #region "日期相關"

    /// <summary>
    /// 檢核日期是否合法
    /// </summary>
    /// <param name="dateStr">日期字串</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <returns>bool</returns>
    /// <remarks>如果日期是預設值則拋false回去</remarks>
    public bool CheckDate(string dateStr, out string errMsg)
    {
        string[] DateTimeList = GetDateTimeFormatList();
        errMsg = string.Empty;
        DateTime convertedDate = new DateTime();
        DateTime defaultDate = new DateTime(1900, 01, 01, 00, 00, 00, 000);

        try
        {
            convertedDate = DateTime.ParseExact(dateStr, DateTimeList, null, DateTimeStyles.AllowWhiteSpaces);
            //如果日期是預設值則拋false回去
            return DateTime.Compare(convertedDate, defaultDate) <= 0 ? false : true;
        }
        catch (Exception)
        {
            try
            {
                convertedDate = DateTime.ParseExact(dateStr, DateTimeList, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
                //如果日期是預設值則拋false回去
                return DateTime.Compare(convertedDate, defaultDate) <= 0 ? false : true;
            }
            catch (Exception ex)
            {
                convertedDate = new DateTime();
                errMsg = ex.Message;
                Debug.Print(">>>GetDateTime: " + ex.Message);
                return false;
            }
        }
    }

    /// <summary>
    /// 嘗試將日期字串轉為DateTime格式
    /// </summary>
    /// <param name="dateStr">日期字串</param>
    /// <param name="convertedDate">轉換後的日期</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <returns>bool</returns>
    public bool GetDateTime(string dateStr, out DateTime convertedDate, out string errMsg)
    {
        string[] DateTimeList = GetDateTimeFormatList();
        errMsg = string.Empty;

        try
        {
            convertedDate = DateTime.ParseExact(dateStr, DateTimeList, null, DateTimeStyles.AllowWhiteSpaces);
            return true;
        }
        catch (Exception)
        {
            try
            {
                convertedDate = DateTime.ParseExact(dateStr, DateTimeList, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
                return true;
            }
            catch (Exception ex)
            {
                convertedDate = new DateTime();
                errMsg = ex.Message;
                Debug.Print(">>>GetDateTime: " + ex.Message);
                return false;
            }
        }
    }

    /// <summary>
    /// 日期格式清單
    /// </summary>
    /// <returns>string array</returns>
    private string[] GetDateTimeFormatList()
    {
        string[] DateTimeList = { 
                                        "yyyy/M/d tt hh:mm", 
                                        "yyyy/MM/dd tt hh:mm", 
                                        "yyyy/MM/dd HH:mm", 
                                        "yyyy/M/d HH:mm", 
                                        "yyyy/M/d", 
                                        "yyyy/MM/dd",
                                        "yyyy-M-d tt hh:mm", 
                                        "yyyy-MM-dd tt hh:mm", 
                                        "yyyy-MM-dd HH:mm", 
                                        "yyyy-M-d HH:mm", 
                                        "yyyy-M-d", 
                                        "yyyy-MM-dd" 
                                    };
        return DateTimeList;
    }

    /// <summary>
    /// 檢核是否有未來日期
    /// </summary>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dt">檢核資料dataTable</param>
    /// <returns></returns>
    public bool IsFutureDay(ref StringBuilder errMsg, DataTable dt)
    {
        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            //紀錄原有多少筆資料
            int dataCount = dt.Rows.Count;

            string errMsgStr = string.Empty;

            //檢核狀態，只有狀態為暫存的單才能刪除
            foreach (DataRow row in dt.Rows)
            {
                //DateTime SBStartDate = Convert.ToDateTime(row["SBStartDate"].ToString() + " " + row["SBStartTime"].ToString().Substring(0, 2) + ":" + row["SBStartTime"].ToString().Substring(2, 2));
                //DateTime SBEndDate = Convert.ToDateTime(row["SBEndDate"].ToString() + " " + row["SBEndTime"].ToString().Substring(0, 2) + ":" + row["SBEndTime"].ToString().Substring(2, 2));
                DateTime SBStartDate;
                DateTime SBEndDate;

                if (!GetDateTime(row["SBStartDate"].ToString() + " " + row["SBStartTime"].ToString().Substring(0, 2) + ":" + row["SBStartTime"].ToString().Substring(2, 2), out SBStartDate, out errMsgStr) ||
                    !GetDateTime(row["SBEndDate"].ToString() + " " + row["SBEndTime"].ToString().Substring(0, 2) + ":" + row["SBEndTime"].ToString().Substring(2, 2), out SBEndDate, out errMsgStr))
                {
                    errMsg.AppendLine("第" + row["RowIndex"].ToString() + "筆資料留守日期格式錯誤");
                    dt.Rows.Remove(row);
                    continue;
                }

                if (DateTime.Compare(SBStartDate, DateTime.Now) > 0 || DateTime.Compare(SBEndDate, DateTime.Now) > 0)
                {
                    errMsg.AppendLine("第" + row["RowIndex"].ToString() + "筆資料狀態非暫存無法送簽");
                    dt.Rows.Remove(row);
                    continue;
                }
            }

            //如果與原始資料筆數相同代表全部檢核通過
            if (dt.Rows.Count != dataCount)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>isFutureDay: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            return false;
        }
    }

    #endregion

    /// <summary>
    /// 取得月份已申報時數合計字串
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">員工編號</param>
    /// <param name="SBStartDate">留守日期起日</param>
    /// <param name="SBEndDate">留守日期迄日</param>
    /// <returns>string</returns>
    public string GetStayBehindMonthTotal(string compID, string empID, string SBStartDate, string SBEndDate)
    {
        string monthTotalStr = string.Empty;
        string errMsg = string.Empty;
        DateTime monthDate = new DateTime();
        string monthStr = "0";

        //將日期字串轉為Datetime再取出月份部分
        if (GetDateTime(SBStartDate, out monthDate, out errMsg))
        {
            monthStr = monthDate.Month.ToString();
        }

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" 	ISNULL(CONVERT(DECIMAL(10, 1), SUM(A.Submit)), 0) AS Submit ");
            sb.Append("    ,ISNULL(CONVERT(DECIMAL(10, 1), SUM(A.Approval)), 0) AS Approval ");
            sb.Append("    ,ISNULL(CONVERT(DECIMAL(10, 1), SUM(A.Reject)), 0) AS Reject ");
            sb.Append(" FROM (SELECT ");
            sb.Append(" 		ROUND(CONVERT(DECIMAL(10, 2), SUM(CASE SBStatus WHEN '2' THEN ISNULL(SBTotalTime, 0) - ISNULL(MealTime, 0) ELSE 0 END)) / 60, 1) AS Submit ");
            sb.Append(" 	   ,ROUND(CONVERT(DECIMAL(10, 2), SUM(CASE SBStatus WHEN '3' THEN ISNULL(SBTotalTime, 0) - ISNULL(MealTime, 0) ELSE 0 END)) / 60, 1) AS Approval ");
            sb.Append(" 	   ,ROUND(CONVERT(DECIMAL(10, 2), SUM(CASE SBStatus WHEN '4' THEN ISNULL(SBTotalTime, 0) - ISNULL(MealTime, 0) ELSE 0 END)) / 60, 1) AS Reject ");
            sb.Append(" 	FROM StayBehindDeclaration ");
            sb.Append(" 	WHERE SBCompID = '" + compID + "' ");
            sb.Append(" 	AND MONTH(SBStartDate) = MONTH('" + SBStartDate + "') ");
            sb.Append(" 	AND YEAR(SBStartDate) = YEAR('" + SBEndDate + "') ");
            sb.Append(" 	AND SBEmpID = '" + empID + "' ");
            sb.Append(" 	GROUP BY SBTxnID) A ");

            using (DataTable dtTotal = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                monthTotalStr = monthStr + "月份已申報時數合計: 送簽 " + dtTotal.Rows[0]["Submit"].ToString() + "小時&nbsp;&nbsp;&nbsp;核准 " + dtTotal.Rows[0]["Approval"].ToString() + "小時&nbsp;&nbsp;&nbsp;駁回 " + dtTotal.Rows[0]["Reject"].ToString() + "小時";
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetStayBehindMonthTotal: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            monthTotalStr = monthStr + "月份已申報時數合計: 送簽 0.0小時&nbsp;&nbsp;&nbsp;核准 0.0小時&nbsp;&nbsp;&nbsp;駁回 0.0小時";
        }

        return monthTotalStr;
    }

    /// <summary>
    /// 取得留守設定
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">留守人員工編號</param>
    /// <param name="workSiteID">留守人工作地點</param>
    /// <param name="startDate">留守起日</param>
    /// <param name="endDate">留守迄日</param>
    /// <param name="dtSBSet">留守設定Table</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <returns>bool</returns>
    public bool GetStayBehindSet(string compID, string empID, string workSiteID, string startDate, string endDate, out DataTable dtSBSet, out string errMsg)
    {
        bool isSuccess = false;
        dtSBSet = null;
        errMsg = string.Empty;
        StringBuilder errMsgArr = new StringBuilder();

        try
        {
            //先查詢人員設定
            if (!GetNaturalDisasterByEmp(out errMsg, out dtSBSet, compID, empID, startDate, endDate, string.Empty, string.Empty))
            {
                //有錯誤發生，紀錄Error
                if (!string.IsNullOrEmpty(errMsg))
                {
                    errMsgArr.AppendLine(errMsg);
                }

                //再查詢縣市設定
                if (!GetNaturalDisasterByCity(out errMsg, out dtSBSet, compID, workSiteID, startDate, endDate, string.Empty, string.Empty))
                {
                    //有錯誤發生，紀錄Error
                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        errMsgArr.AppendLine(errMsg);
                    }
                }
                else
                {
                    isSuccess = true;
                }
            }
            else
            {
                isSuccess = true;
            }

            //將錯誤訊息串成字串
            if (errMsgArr.Length > 0)
            {
                errMsg = errMsgArr.ToString();
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetStayBehindSet: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 檢核是否在留守設定的範圍內
    /// </summary>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="compID">公司別</param>
    /// <param name="empID">留守人員工編號</param>
    /// <param name="workSiteID">工作地點代碼</param>
    /// <param name="startDate">留守起日</param>
    /// <param name="endDate">留守迄日</param>
    /// <param name="startTime">留守開始時間</param>
    /// <param name="endTime">留守結束時間</param>
    /// <returns>bool</returns>
    public bool CheckStayBehindSet(ref StringBuilder errMsg, string compID, string empID, string workSiteID, string startDate, string endDate, string startTime, string endTime)
    {
        bool isValid = false;
        string errMsgStr = string.Empty;
        DataTable dt;
        try
        {
            //先查詢人員設定
            if (!GetNaturalDisasterByEmp(out errMsgStr, out dt, compID, empID, startDate, endDate, startTime, endTime))
            {
                //有錯誤發生，紀錄Error
                if (!string.IsNullOrEmpty(errMsgStr))
                {
                    errMsg.AppendLine(errMsgStr);
                }

                //再查詢縣市設定
                if (!GetNaturalDisasterByCity(out errMsgStr, out dt, compID, workSiteID, startDate, endDate, startTime, endTime))
                {
                    //有錯誤發生，紀錄Error
                    if (!string.IsNullOrEmpty(errMsgStr))
                    {
                        errMsg.AppendLine(errMsgStr);
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = true;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>CheckStayBehindSet: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
        }
        return isValid;
    }

    /// <summary>
    /// 查詢HR留守設定-By縣市
    /// </summary>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dtCity">查詢結果table</param>
    /// <param name="compID">公司別</param>
    /// <param name="workSiteID">工作地點代碼</param>
    /// <param name="startDate">留守起日</param>
    /// <param name="endDate">留守迄日</param>
    /// <param name="startTime">留守開始時間</param>
    /// <param name="endTime">留守結束時間</param>
    /// <returns>bool</returns>
    ///<remarks>會取得</remarks>
    private bool GetNaturalDisasterByCity(out string errMsg, out DataTable dtCity, string compID, string workSiteID, string startDate, string endDate, string startTime, string endTime)
    {
        errMsg = string.Empty;

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();
            sb.AppendStatement(" SELECT * FROM NaturalDisasterByCity WHERE 1 = 1 ");
            sb.Append(" AND CompID = '" + compID + "' ");
            sb.Append(" AND WorkSiteID = '" + workSiteID + "' ");
            sb.Append(" AND DisasterStartDate <= '" + startDate + "' ");
            sb.Append(" AND DisasterEndDate >= '" + endDate + "' ");

            if (!string.IsNullOrEmpty(startTime))
            {
                sb.Append(" AND BeginTime <= '" + startTime + "' ");
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sb.Append(" AND EndTime >= '" + endTime + "' ");
            }

            sb.Append(" ; ");

            using (DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                dtCity = dt;
                return dt.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetNaturalDisasterByCity: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            dtCity = null;
            errMsg = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 查詢HR留守設定-By人員
    /// </summary>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dtPerson">查詢結果table</param>
    /// <param name="compID">公司別</param>
    /// <param name="empID">留守人員工編號</param>
    /// <param name="startDate">留守起日</param>
    /// <param name="endDate">留守迄日</param>
    /// <param name="startTime">留守開始時間</param>
    /// <param name="endTime">留守結束時間</param>
    /// <returns>bool</returns>
    private bool GetNaturalDisasterByEmp(out string errMsg, out DataTable dtPerson, string compID, string empID, string startDate, string endDate, string startTime, string endTime)
    {
        errMsg = string.Empty;

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();
            sb.AppendStatement(" SELECT * FROM NaturalDisasterByEmp WHERE 1 = 1 ");
            sb.Append(" AND CompID = '" + compID + "' ");
            sb.Append(" AND EmpID = '" + empID + "' ");
            sb.Append(" AND DisasterStartDate <= '" + startDate + "' ");
            sb.Append(" AND DisasterEndDate >= '" + endDate + "' ");

            if (!string.IsNullOrEmpty(startTime))
            {
                sb.Append(" AND BeginTime <= '" + startTime + "' ");
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                sb.Append(" AND EndTime >= '" + endTime + "' ");
            }

            sb.Append(" ; ");

            using (DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                dtPerson = dt;
                return dt.Rows.Count > 0 ? true : false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>GetNaturalDisasterByEmp: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            dtPerson = null;
            errMsg = ex.Message;
            return false;
        }
    }

    /// <summary>
    /// 檢核當日留守時數是否超過12小時上限
    /// </summary>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dt">檢核資料dataTable</param>
    /// <returns>boolean</returns>
    public bool CheckStayBehindDayLimit(ref StringBuilder errMsg, DataTable dt)
    {
        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();
            string curEmpID = string.Empty;
            string curSBDate = string.Empty;
            //紀錄原有多少筆資料
            int dataCount = dt.Rows.Count;

            //檢核狀態，只有狀態為暫存的單才能刪除
            foreach (DataRow row in dt.Rows)
            {
                if (!row["SBStatus"].ToString().Equals("1"))
                {
                    errMsg.AppendLine("第" + row["RowIndex"].ToString() + "筆資料狀態非暫存無法送簽");
                    dt.Rows.Remove(row);
                }
            }

            //如果全部資料檢核失敗直接return不繼續檢核下去
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            //檢核是否超過一天12小時留守上限
            //先排序
            dt.DefaultView.Sort = "SBEmpID,SBStartDate ASC";
            foreach (DataRow row in dt.Rows)
            {
                if (!curEmpID.Equals(row["SBEmpID"].ToString()) || !curSBDate.Equals(row["SBStartDate"].ToString()))
                {
                    sb.Reset();
                    sb.AppendStatement(" SELECT ISNULL(SUM(SBTotalTime) - SUM(MealTime), 0) AS TotalTime FROM StayBehindDeclaration ");
                    sb.Append(" WHERE SBCompID = '" + row["SBCompID"].ToString() + "' AND SBEmpID = '" + row["SBEmpID"].ToString() + "' AND SBStatus IN ('2', '3') AND SBStartDate = '" + row["SBStartDate"].ToString() + "' ");
                    using (DataTable dtTotal = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
                    {
                        double dbTotal = Convert.ToDouble(dtTotal.Rows[0]["TotalTime"].ToString());
                        if (dbTotal > 720)
                        {
                            errMsg.AppendLine("員工編號" + row["SBEmpID"].ToString() + "在" + row["SBStartDate"].ToString() + "已超過當日留守時數上限12小時!");
                            dt.Rows.Remove(row);
                        }
                    }
                }
            }

            //如果檢核完的資料筆數同原始筆數代表檢核通過
            if (dt.Rows.Count == dataCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.Print(">>>CheckStayBehindDayLimit: " + ex.Message);
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            errMsg.AppendLine(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// 計算留守時數
    /// </summary>
    /// <param name="resultTable">回傳結果table</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dtPara">加班和留守參數table</param>
    /// <param name="Schedule">班表</param>
    /// <param name="SBStartDate">留守起日日期、時間、用餐時數</param>
    /// <param name="SBEndDate">留守迄日日期、時間</param>
    /// <returns>bool</returns>
    public bool GetTotalStayBehindTime(out DataTable resultTable, out StringBuilder errMsg, DataTable dtPara, DataTable Schedule, ArrayList startDateInfo, ArrayList endDateInfo, string mealTime)
    {
        errMsg = new StringBuilder();
        resultTable = new DataTable();

        resultTable.Columns.Add("SBDate");
        resultTable.Columns.Add("SBStartTime");
        resultTable.Columns.Add("SBEndTime");
        resultTable.Columns.Add("MealTime");
        resultTable.Columns.Add("SBTotalTime");

        try
        {
            //如果留守起迄日期是同一天，單算當天就好
            if (startDateInfo[0].Equals(endDateInfo[0]))
            {
                DataRow dr = resultTable.NewRow();
                dr["SBDate"] = startDateInfo[0];
                dr["SBStartTime"] = startDateInfo[1];
                dr["SBEndTime"] = endDateInfo[1];

                //測試班表時間
                TimeSpan tstest1 = new TimeSpan(Convert.ToDateTime(startDateInfo[0] + " " + "16:00").Ticks);

                //起始時間
                TimeSpan tsStart = new TimeSpan(Convert.ToDateTime(startDateInfo[0] + " "
                    + startDateInfo[1].ToString().Substring(0, 2) + ":" + startDateInfo[1].ToString().Substring(2, 2)).Ticks);
                //結束時間
                TimeSpan tsEnd = new TimeSpan(Convert.ToDateTime(endDateInfo[0] + " "
                    + endDateInfo[1].ToString().Substring(0, 2) + ":" + endDateInfo[1].ToString().Substring(2, 2)).Ticks);
                //計算時間差
                TimeSpan tsDiff = new TimeSpan(tsEnd.Ticks - tsStart.Ticks);
                //時間差結果
                double diff = tsDiff.TotalMinutes - Convert.ToDouble(mealTime);
                string DateDiff = tsDiff.TotalMinutes.ToString();
                string timeDiff = diff.ToString();
            }
            else
            {
                //計算有幾天
                int dateCount = DateTime.Compare(Convert.ToDateTime(startDateInfo[0]), Convert.ToDateTime(endDateInfo[0]));

                //計算並塞入resultTable中
                for (int i = 0; i < dateCount; i++)
                {
                    DataRow dr = resultTable.NewRow();

                    //當下計算之日期
                    DateTime tmpDate = Convert.ToDateTime(startDateInfo[0]).AddDays(i + 1);

                    dr["SBDate"] = tmpDate.ToString("yyyy/MM/dd");

                    //將這筆資料加進resultTable中
                    resultTable.Rows.Add(dr);
                }
            }
            resultTable = null;
            return true;
        }
        catch (Exception ex)
        {
            errMsg.AppendLine(ex.Message);
            resultTable = null;
            return false;
        }
    }

    /// <summary>
    /// 計算時段
    /// </summary>
    /// <param name="compID">公司別</param>
    /// <param name="empID">留守人員工編號</param>
    /// <param name="cntStart">留守起日留守時數</param>
    /// <param name="cntMiddle">非留守起迄日之留守時數,如果他要跨多日時可用</param>
    /// <param name="cntEnd">留守迄日留守時數</param>
    /// <param name="startDate">留守起日</param>
    /// <param name="startTime">留守開始時間</param>
    /// <param name="endDate">留守迄日</param>
    /// <param name="endTime">留守結束時間</param>
    /// <param name="mealTime">用餐時數</param>
    /// <param name="chkMealFlag">用餐註記</param>
    /// <param name="sbTxnID">單號</param>
    /// <param name="errMsg">錯誤訊息</param>
    /// <param name="dateArr">日期清單</param>
    /// <param name="periodArr">各留守日留守時段時數</param>
    /// <returns>bool</returns>
    public bool PeriodCount(string compID, string empID, double cntStart, double cntMiddle, double cntEnd, string startDate, string startBeginTime, string startEndTime, string endDate, string endBeginTime, string endEndTime, double mealTime, string chkMealFlag, string sbTxnID, out string errMsg, out ArrayList dateArr, out ArrayList periodArr)
    {
        bool isSuccess = false;
        errMsg = string.Empty;
        dateArr = new ArrayList();
        periodArr = new ArrayList();

        DataTable dtStayBehindDatas;

        try
        {
            DbHelper db = new DbHelper(_overtimeDBName);
            CommandHelper sb = db.CreateCommandHelper();

            sb.Reset();
            sb.AppendStatement(" SELECT SBStartDate, SBEndDate, SBTxnID, SBStartTime, SBEndTime, SBTotalTime, MealFlag, MealTime ");
            sb.Append(" FROM StayBehindDeclaration WHERE 1 = 1 ");
            sb.Append(" AND MONTH(CONVERT(DATETIME, SBStartDate)) = MONTH(CONVERT(DATETIME, '" + startDate + "')) ");
            sb.Append(" AND SBStartDate <= '" + startDate + "' ");
            sb.Append(" AND SBCompID = '" + compID + "' ");
            sb.Append(" AND SBEmpID = '" + empID + "' ");
            sb.Append(" AND SBStatus IN ('2', '3') ");
            sb.Append(" AND NOT (SBTxnID = '" + sbTxnID + "') ");
            sb.Append(" ORDER BY SBStartDate, SBStartTime ;");

            //取得資料庫內的歷史單
            dtStayBehindDatas = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            //將當前這筆單新增進DataTable中
            //留守起迄日為同一天
            if (startDate.Equals(endDate))
            {
                //將留守起日總時數扣除用餐時數
                if (chkMealFlag.Equals("1"))
                {
                    cntStart -= mealTime;
                }

                DataRow currentStartDate = dtStayBehindDatas.NewRow();
                currentStartDate["SBStartDate"] = startDate;
                currentStartDate["SBEndDate"] = startDate;
                currentStartDate["SBTxnID"] = "Current";
                currentStartDate["SBStartTime"] = startBeginTime;
                currentStartDate["SBEndTime"] = startEndTime;
                currentStartDate["SBTotalTime"] = cntStart;
                //已扣掉用餐時數,flag立為0避免重複運算
                currentStartDate["MealFlag"] = "0";     //MealFlag;
                currentStartDate["MealTime"] = 0;       //MealTime;
                dtStayBehindDatas.Rows.Add(currentStartDate);
            }
            else
            {
                //扣除用餐時數
                if (chkMealFlag.Equals("1"))
                {
                    cntStart -= mealTime;
                    if (cntStart < 0.0)
                    {
                        cntEnd += cntStart;
                        cntStart = 0.0;
                    }
                }

                //非同一天,要拆成起迄兩張單
                DataRow currentStartData = dtStayBehindDatas.NewRow();
                currentStartData["SBStartDate"] = startDate;
                currentStartData["SBEndDate"] = startDate;
                currentStartData["SBTxnID"] = "Current";
                currentStartData["SBStartTime"] = startBeginTime;
                currentStartData["SBEndTime"] = startEndTime;
                currentStartData["SBTotalTime"] = cntStart;
                //已扣掉用餐時數,flag立為0避免重複運算
                currentStartData["MealFlag"] = "0";     //MealFlag;
                currentStartData["MealTime"] = 0;       //MealTime;
                dtStayBehindDatas.Rows.Add(currentStartData);

                DataRow currentEndData = dtStayBehindDatas.NewRow();
                currentEndData["SBStartDate"] = endDate;
                currentEndData["SBEndDate"] = endDate;
                currentEndData["SBTxnID"] = "Current";
                currentEndData["SBStartTime"] = endBeginTime;
                currentEndData["SBEndTime"] = endEndTime;
                currentEndData["SBTotalTime"] = cntEnd;
                //已扣掉用餐時數,flag立為0避免重複運算
                currentEndData["MealFlag"] = "0";     //MealFlag;
                currentEndData["MealTime"] = 0;       //MealTime;
                dtStayBehindDatas.Rows.Add(currentEndData);
            }

            //將資料排序
            dtStayBehindDatas.DefaultView.Sort = "SBStartDate ASC, SBStartTime ASC";
            dtStayBehindDatas = dtStayBehindDatas.DefaultView.ToTable();

            //紀錄上一筆的TxnID,用來辨識跨日單時數用
            string lastTxnID = string.Empty;
            double lastCntStart = 0.0, lastCntEnd = 0.0;

            //依照是否扣除用餐時數調整
            for (int i = 0; i < dtStayBehindDatas.Rows.Count; i++)
            {
                //將當下這筆TxnID紀錄進lastTxnID
                lastTxnID = dtStayBehindDatas.Rows[i]["SBTxnID"].ToString();

                //先存該筆單留守時數
                lastCntStart = Convert.ToDouble(dtStayBehindDatas.Rows[i]["SBTotalTime"].ToString());

                //TxnID相同代表是同一張單，依是否用餐調整時數
                if (dtStayBehindDatas.Rows[i]["SBTxnID"].ToString().Equals(lastTxnID))
                {
                    //如果起日之留守時數不夠扣,用迄日的去補
                    if (lastCntStart < 0.0 && dtStayBehindDatas.Rows[i]["MealFlag"].ToString().Equals("1"))
                    {
                        lastCntEnd = Convert.ToDouble(dtStayBehindDatas.Rows[i]["SBTotalTime"].ToString());
                        lastCntEnd -= lastCntStart;
                        dtStayBehindDatas.Rows[i - 1]["SBTotalTime"] = 0.0;
                    }
                }
                else
                {
                    if (dtStayBehindDatas.Rows[i]["MealFlag"].ToString().Equals("1"))
                    {
                        cntStart -= Convert.ToDouble(dtStayBehindDatas.Rows[i]["MealTime"].ToString());
                        dtStayBehindDatas.Rows[i]["SBTotalTime"] = cntStart;
                    }
                }
            }

            dtStayBehindDatas = dtStayBehindDatas.DefaultView.ToTable();

            //計算每天的總時數
            var countTotalTime = from row in dtStayBehindDatas.AsEnumerable()
                                 group row by row["SBStartDate"] into g
                                 select new
                                 {
                                     GroupKey = g.Key,
                                     SumValue = g.Sum(p => double.Parse(p["SBTotalTime"].ToString()))
                                 };

            //將計算結果存至ArrayList
            foreach (var date in countTotalTime)
            {
                //將留守總時數(分鐘)換算成留守總時數(小時)
                double dTotalSBTime = (Math.Round(date.SumValue / 6, MidpointRounding.AwayFromZero)) / 10;
                dateArr.Add(date.GroupKey);
                periodArr.Add(dTotalSBTime.ToString("0.0"));

                //印出結果
                Debug.Print("Date:{0}", date.GroupKey);
                Debug.Print("\nSBTotalTime(Minutes):{0}\n", date.SumValue);
                Debug.Print("\nSBTotalTime(Hour):{0}\n", dTotalSBTime);
            }

            isSuccess = true;
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
        }
        finally
        {
            dtStayBehindDatas = null;
        }

        return isSuccess;
    }
}