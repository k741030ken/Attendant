using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Text;

/// <summary>
/// TemplateSql 的摘要描述
/// </summary>
public partial class WorkTimeSql
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    #region "下拉選單"
    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    public static string LoadCompID(WorkTimeViewModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT CompID AS DataValue, CompID + '-' + CompName AS DataText");
        sb.AppendLine("FROM Company");
        if (!String.IsNullOrEmpty(model.CompID))
        {
            sb.AppendLine("WHERE CompID = @CompID");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    public static string LoadComp()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT DISTINCT C.CompID AS DataValue, C.CompID + '-' + C.CompName AS DataText");
        sb.AppendLine("FROM Organization O");
        sb.AppendLine("JOIN Company C ON O.CompID = C.CompID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.BossCompID = @UserComp");
        sb.AppendLine("AND O.Boss = @UserID");

        return sb.ToString();
    }

    /// <summary>
    /// 取得CompID下拉選單
    /// </summary>
    public static string LoadBothComp()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT C.CompID AS DataValue, C.CompID + '-' + C.CompName AS DataText");
        sb.AppendLine("FROM Organization O");
        sb.AppendLine("JOIN Company C ON O.CompID = C.CompID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.BossCompID = @UserComp");
        sb.AppendLine("AND O.Boss = @UserID");
        sb.AppendLine("UNION");
        sb.AppendLine("SELECT C.CompID AS DataValue, C.CompID + '-' + C.CompName AS DataText");
        sb.AppendLine("FROM OrganizationFlow O");
        sb.AppendLine("JOIN Company C ON O.CompID = C.CompID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.BossCompID = @UserComp");
        sb.AppendLine("AND O.Boss = @UserID");
        sb.AppendLine("AND BusinessType IN (SELECT Code FROM HRCodeMap WHERE TabName = 'Business' AND FldName = 'BusinessType' AND NotShowFlag = '0')");
        return sb.ToString();
    }

    /// <summary>
    /// 取得行政組織下拉選單
    /// </summary>
    public static string LoadOrgan(WorkTimeViewModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT O.OrgType, O.OrgType + '-' + O2.OrganName AS OrgTypeName");
        sb.AppendLine(", O.DeptID, O.DeptID + '-' + O3.OrganName AS DeptName");
        sb.AppendLine(", O.OrganID, O.OrganID + '-' + O.OrganName AS OrganName");
        sb.AppendLine("FROM Organization O");
        sb.AppendLine("LEFT JOIN Organization O2 ON O.CompID = O2.CompID AND O.OrgType = O2.OrganID");
        sb.AppendLine("LEFT JOIN Organization O3 ON O.CompID = O3.CompID AND O.DeptID = O3.OrganID");
        sb.AppendLine("WHERE O.CompID = @CompID");
        if (!String.IsNullOrEmpty(model.UserComp) && !String.IsNullOrEmpty(model.UserID))
        {
            sb.AppendLine("AND O.OrganID IN (");
            sb.AppendLine("SELECT OrganID FROM funGetAllowOrgan(@CompID, @UserComp, @UserID, '')");
            sb.AppendLine(")");
        }
        if (!String.IsNullOrEmpty(model.WorkSite))
        {
            sb.AppendLine("AND O.WorkSiteID = @WorkSite");
        }
        sb.AppendLine("AND O.InValidFlag = '0' And O.VirtualFlag = '0'");
        return sb.ToString();
    }

    /// <summary>
    /// 取得功能組織下拉選單
    /// </summary>
    public static string LoadFlowOrgan()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT BusinessType, RoleCode, UpOrganID, DeptID, OrganID");
        sb.AppendLine(", OrganLevel = CASE RoleCode WHEN '10' THEN OrganID WHEN '0' THEN UpOrganID ELSE OrganID END");
        sb.AppendLine(", OrganName = CASE WHEN RoleCode = '0' THEN '└─' + OrganID + '-' + OrganName ELSE OrganID + '-' + OrganName END");
        sb.AppendLine("FROM OrganizationFlow O");
        sb.AppendLine("WHERE CompID = @CompID");
        sb.AppendLine("AND OrganID IN (");
        sb.AppendLine("SELECT OrganID FROM funGetAllowOrgan(@CompID, @UserComp, @UserID, @FlowType)");
        sb.AppendLine(")");
        sb.AppendLine("ORDER BY BusinessType, OrganLevel, RoleCode DESC, OrganID");

        return sb.ToString();
    }

    /// <summary>
    /// 取得班別下拉選單
    /// </summary>
    public static string LoadWTID()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT O.WTID AS DataValue");
        sb.AppendLine(", O.WTID + '-' + LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2) + '　' + W.Remark + '-' + ISNULL(H.CodeCName, '') AS DataText");
        sb.AppendLine("FROM OrgWorkTime O");
        sb.AppendLine("LEFT JOIN WorkTime W ON O.CompID = W.CompID AND O.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN HRCodeMap H ON W.Remark = H.Code AND H.TabName = 'WorkTime' AND H.FldName = 'Remark'");
        sb.AppendLine("WHERE O.CompID = @CompID");
        sb.AppendLine("AND O.DeptID = @DeptID");
        sb.AppendLine("AND O.OrganID = @OrganID");
        sb.AppendLine("AND W.InValidFlag = '0'");
        sb.AppendLine("AND W.WTIDTypeFlag = '1'");
        sb.AppendLine("UNION");
        sb.AppendLine("SELECT W.WTID AS DataValue");
        sb.AppendLine(", W.WTID + '-' + LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2) + '　' + W.Remark + '-' + ISNULL(H.CodeCName, '') AS DataText");
        sb.AppendLine("FROM WorkTime W");
        sb.AppendLine("LEFT JOIN HRCodeMap H ON W.Remark = H.Code AND H.TabName = 'WorkTime' AND H.FldName = 'Remark'");
        sb.AppendLine("WHERE W.CompID = @CompID");
        sb.AppendLine("AND W.InValidFlag = '0'");
        sb.AppendLine("AND W.WTIDTypeFlag = '0'");

        return sb.ToString();
    }

    /// <summary>
    /// 取得值勤班別下拉選單
    /// </summary>
    public static string LoadGuardWTID()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT W.WTID AS WTID");
        sb.AppendLine(", W.WTID + '-' + LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2) + '　' + W.Remark + '-' + ISNULL(H.CodeCName, '') AS WorkTime");
        sb.AppendLine(", ISNULL(H.CodeCName, '') Remark");
        sb.AppendLine(", LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2) GuardTime");
        sb.AppendLine(", W.BeginTime");
        sb.AppendLine(", W.EndTime");
        sb.AppendLine("FROM WorkTime W");
        sb.AppendLine("LEFT JOIN HRCodeMap H ON W.Remark = H.Code AND H.TabName = 'WorkTime' AND H.FldName = 'Remark'");
        sb.AppendLine("WHERE W.CompID = @CompID");
        sb.AppendLine("AND W.InValidFlag = '0'");
        sb.AppendLine("AND W.WTIDTypeFlag = '2'");

        return sb.ToString();
    }

    /// <summary>
    /// 取得個人工作地點
    /// </summary>
    public static string LoadPerWorkSite()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT DISTINCT RTRIM(P.WorkSiteID) AS WorkSiteID, W.Remark AS WorkSiteName FROM Personal P");
        sb.AppendLine("JOIN WorkSite W ON W.CompID = P.CompID AND W.WorkSiteID = P.WorkSiteID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND P.CompID = @CompID");
        sb.AppendLine("AND P.OrganID = @OrganID");

        return sb.ToString();
    }

    /// <summary>
    /// 取得值勤人員下拉選單
    /// </summary>
    public static string LoadDutyEmpID(WorkTimeViewModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT EmpID, NameN As EmpName FROM Personal");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND WorkStatus = '1'");
        sb.AppendLine("AND CompID = @CompID");
        if (!String.IsNullOrEmpty(model.WorkSite))
        {
            sb.AppendLine("AND WorkSiteID = @WorkSite");
        }
        if (!String.IsNullOrEmpty(model.OrganID))
        {
            sb.AppendLine("AND OrganID = @OrganID");
        }
        if (!String.IsNullOrEmpty(model.EmpID))
        {
            sb.AppendLine("AND UPPER(EmpID) = @EmpID");
        }
        return sb.ToString();
    }

    /// <summary>
    /// 取得值勤單位下拉選單
    /// </summary>
    public static string LoadDutyOrgan(EmpWorkTimeModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT DISTINCT RTRIM(O.OrganID) AS DataValue");
        sb.AppendLine(", RTRIM(O.OrganID) + '-' + O.OrganName AS DataText");
        sb.AppendLine("FROM EmpFlow E");
        sb.AppendLine("JOIN Personal P ON P.CompID = E.CompID AND P.EmpID = E.EmpID");
        sb.AppendLine("JOIN Organization O ON O.CompID = P.CompID AND O.OrganID = P.DeptID");
        sb.AppendLine("WHERE E.ActionID = '01'");
        sb.AppendLine("AND E.OrganID IN ('" + model.FlowOrganID + "')");

        return sb.ToString();
    }
    #endregion

    #region "個人班表EmpWorkTime"
    /// <summary>
    /// 查詢EmpWorkTime資料
    /// </summary>
    public static string LoadEmpWorkTimeGridData(EmpWorkTimeModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT P.CompID, C.CompName, P.EmpID, P.NameN");
        sb.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName");
        sb.AppendLine(", P.DeptID, O1.OrganName AS DeptName");
        sb.AppendLine(", P.OrganID, O2.OrganName");
        sb.AppendLine(", ISNULL(W.WTID, '') WTID");
        sb.AppendLine(", WorkTime = LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)");
        sb.AppendLine(", RotateFlag = CASE WHEN EW.RotateFlag = '1' THEN '是' ELSE '' END");
        sb.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        //sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 111) + ' ' + CONVERT(VARCHAR, EW.LastChgDate, 24) END");
        sb.AppendLine("FROM EmpWorkTime EW");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company C ON P.CompID = C.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..EmpFlow E ON P.CompID = E.CompID AND P.EmpID = E.EmpID AND E.ActionID = '01'");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company LC ON EW.LastChgComp = LC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE P.CompID = @CompID");
        if (model.AllSearch)
        {
            sb.AppendLine("AND (P.OrganID IN ('" + model.OrganID + "') OR E.OrganID IN ('" + model.FlowOrganID + "'))");
        }
        else
        {
            if (!String.IsNullOrEmpty(model.OrganID))
            {
                sb.AppendLine("AND P.OrganID IN ('" + model.OrganID + "')");
            }
            if (!String.IsNullOrEmpty(model.FlowOrganID))
            {
                sb.AppendLine("AND E.OrganID IN ('" + model.FlowOrganID + "')");
            }
        }
        if (!String.IsNullOrEmpty(model.WTID))
        {
            sb.AppendLine("AND OW.WTID = @WTID");
        }
        if (!String.IsNullOrEmpty(model.EmpID))
        {
            sb.AppendLine("AND UPPER(P.EmpID) = UPPER(@EmpID)");
        }
        if (!String.IsNullOrEmpty(model.EmpName))
        {
            sb.AppendLine("AND P.NameN LIKE N'%" + model.EmpName + "%'");
        }
        sb.AppendLine("AND P.WorkStatus = '1'");
        sb.AppendLine("ORDER BY P.CompID, P.DeptID, P.OrganID, P.EmpID");
        return sb.ToString();
    }

    /// <summary>
    /// 查詢EmpWorkTime資料(新增頁)
    /// </summary>
    public static string LoadEmpWorkTimeGridData_Add(EmpWorkTimeModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT P.CompID, P.EmpID, P.NameN");
        sb.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName");
        sb.AppendLine(", P.DeptID, O1.OrganName AS DeptName");
        sb.AppendLine(", P.OrganID, O2.OrganName");
        sb.AppendLine(", ISNULL(W.WTID, '') WTID");
        sb.AppendLine(", WorkTime = LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)");
        sb.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        //sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 111) + ' ' + CONVERT(VARCHAR, EW.LastChgDate, 24) END");
        sb.AppendLine("FROM " + _eHRMSDB_ITRD + "..Personal P");
        sb.AppendLine("LEFT JOIN EmpWorkTime EW ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company LC ON EW.LastChgComp = LC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE P.WorkStatus = '1'");
        sb.AppendLine("AND P.CompID = @CompID");
        if (!String.IsNullOrEmpty(model.OrganID))
        {
            sb.AppendLine("AND P.OrganID IN ('" + model.OrganID + "')");
        }
        sb.AppendLine("AND P.WorkStatus = '1'");
        sb.AppendLine("ORDER BY P.CompID, P.DeptID, P.OrganID, P.EmpID");
        return sb.ToString();
    }

    /// <summary>
    /// 查詢EmpWorkTimeLog資料
    /// </summary>
    public static string LoadEmpWorkTimeLogGridData()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT P.CompID, C.CompName, P.EmpID, P.NameN");
        sb.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName");
        sb.AppendLine(", P.DeptID, O1.OrganName AS DeptName");
        sb.AppendLine(", P.OrganID, O2.OrganName");
        sb.AppendLine(", ISNULL(W.WTID, '') WTID");
        sb.AppendLine(", WorkTime = LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)");
        sb.AppendLine(", RotateFlag = CASE WHEN EW.RotateFlag = '1' THEN '是' ELSE '' END");
        sb.AppendLine(", Action = CASE EW.ActionFlag WHEN 'A' THEN '新增' WHEN 'U' THEN '修改' WHEN 'D' THEN '刪除' ELSE '' END");
        sb.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        //sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 111) + ' ' + CONVERT(VARCHAR, EW.LastChgDate, 24) END");
        sb.AppendLine("FROM EmpWorkTimeLog EW");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company C ON P.CompID = C.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company LC ON EW.LastChgComp = LC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE P.CompID = @CompID");
        sb.AppendLine("AND P.EmpID = @EmpID");
        sb.AppendLine("ORDER BY EW.LastChgDate DESC");
        return sb.ToString();
    }

    /// <summary>
    /// 查詢EmpWorkTime資料
    /// </summary>
    public static string SelectEmpWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT * FROM EmpWorkTime");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");

        return sb.ToString();
    }

    /// <summary>
    /// 新增EmpWorkTime資料
    /// </summary>
    public static string AddEmpWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("INSERT INTO EmpWorkTime (");
        sb.AppendLine("CompID, EmpID, WTCompID, WTID, RotateFlag, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(") VALUES (");
        sb.AppendLine("@CompID, @EmpID, @WTCompID, @WTID, '0', @LastChgComp, @LastChgID, @LastChgDate");
        sb.AppendLine(")");

        sb.AppendLine("INSERT INTO EmpWorkTimeLog (");
        sb.AppendLine("CompID, EmpID, WTCompID, WTID, RotateFlag, ActionFlag, StartDate, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(") VALUES (");
        sb.AppendLine("@CompID, @EmpID, @WTCompID, @WTID, '0', 'A', @StartDate, @LastChgComp, @LastChgID, @LastChgDate");
        sb.AppendLine(")");

        return sb.ToString();
    }

    /// <summary>
    /// 修改EmpWorkTime資料
    /// </summary>
    public static string UpdateEmpWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("UPDATE EmpWorkTime SET");
        sb.AppendLine("WTCompID = @WTCompID");
        sb.AppendLine(", WTID = @WTID");
        sb.AppendLine(", LastChgComp = @LastChgComp");
        sb.AppendLine(", LastChgID = @LastChgID");
        sb.AppendLine(", LastChgDate = @LastChgDate");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");

        sb.AppendLine("INSERT INTO EmpWorkTimeLog (");
        sb.AppendLine("CompID, EmpID, WTCompID, WTID, RotateFlag, ActionFlag, StartDate, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(") VALUES (");
        sb.AppendLine("@CompID, @EmpID, @WTCompID, @WTID, '0', 'U', @StartDate, @LastChgComp, @LastChgID, @LastChgDate");
        sb.AppendLine(")");

        return sb.ToString();
    }

    /// <summary>
    /// 刪除EmpWorkTime資料
    /// </summary>
    public static string DeleteEmpWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("DELETE FROM EmpWorkTime");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");

        sb.AppendLine("INSERT INTO EmpWorkTimeLog (");
        sb.AppendLine("CompID, EmpID, WTCompID, WTID, ActionFlag, StartDate, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(") VALUES (");
        sb.AppendLine("@CompID, @EmpID, @WTCompID, @WTID, 'D', @StartDate, @LastChgComp, @LastChgID, @LastChgDate");
        sb.AppendLine(")");

        return sb.ToString();
    }
    #endregion

    #region "行事曆"
    /// <summary>
    /// 取得Calendar假日資料
    /// </summary>
    public static string LoadHoliday()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT SysDate FROM Calendar");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND HolidayOrNot = '1'");
        sb.AppendLine("AND SysDate = @RenderDate");

        return sb.ToString();
    }

    /// <summary>
    /// 取得海外行事曆假日資料
    /// </summary>
    public static string LoadOverSeaHoliday()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT HDate AS SysDate FROM OverSeaHoliday");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND WorkSite = @WorkSite");
        sb.AppendLine("AND HDate = @RenderDate");

        return sb.ToString();
    }

    /// <summary>
    /// 取得單位工作地點
    /// </summary>
    public static string LoadOrgWorkSite()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT W.WorkSiteID, W.Remark AS WorkSiteName FROM Organization O");
        sb.AppendLine("JOIN WorkSite W ON W.CompID = O.CompID AND W.WorkSiteID = O.WorkSiteID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND O.CompID = @CompID");
        sb.AppendLine("AND O.OrganID = @OrganID");

        return sb.ToString();
    }

    /// <summary>
    /// 取得假單檔
    /// </summary>
    public static string GetVacInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT COUNT(*) FROM VacDocInfo");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND VacEmpID = @EmpID");
        sb.AppendLine("AND VacDateS >= @RenderDate");
        sb.AppendLine("AND VacDateE <= @RenderDate");
        sb.AppendLine("AND Status = '999'");

        return sb.ToString();
    }

    /// <summary>
    /// 取得值勤日期行事曆
    /// </summary>
    public static string LoadGuardCalendar(EmpGuardWorkTimeModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT EW.DutyDate, P.EmpID, P.NameN");
        sb.AppendLine(", ISNULL(W.WTID, '') WTID");
        sb.AppendLine(", DutyTime = CASE WHEN EW.BranchFlag = '1' THEN LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)");
        sb.AppendLine("ELSE LEFT(EW.WTBeginTime, 2) + ':' + RIGHT(EW.WTBeginTime, 2) + '~' + LEFT(EW.WTEndTime, 2) + ':' + RIGHT(EW.WTEndTime, 2) END");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine("FROM EmpGuardWorkTime EW");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND EW.DutyCompID = @CompID");
        //sb.AppendLine("AND EW.DutyDeptID = @DeptID");
        sb.AppendLine("AND EW.DutyOrganID = @OrganID");
        if (!String.IsNullOrEmpty(model.BranchFlag))
        {
            sb.AppendLine("AND EW.BranchFlag = @BranchFlag");
        }
        if (!String.IsNullOrEmpty(model.EmpID))
        {
            sb.AppendLine("AND UPPER(EW.EmpID) = UPPER(@EmpID)");
        }
        if (!String.IsNullOrEmpty(model.EmpName))
        {
            sb.AppendLine("AND P.NameN LIKE N'%" + model.EmpName + "%'");
        }
        if (!String.IsNullOrEmpty(model.DutyDate))
        {
            sb.AppendLine("AND DutyDate = @DutyDate");
        }
        if (!String.IsNullOrEmpty(model.DutyDateYear))
        {
            sb.AppendLine("AND YEAR(DutyDate) = @DutyDateYear");
        }
        if (!String.IsNullOrEmpty(model.DutyDateMonth))
        {
            sb.AppendLine("AND MONTH(DutyDate) = @DutyDateMonth");
        }

        return sb.ToString();
    }
    #endregion

    #region "值勤表EmpGuardWorkTime"
    /// <summary>
    /// 查詢EmpGuardWorkTime資料
    /// </summary>
    public static string LoadEmpGuardWorkTimeGridData(EmpGuardWorkTimeModel model)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT CONVERT(VARCHAR, EW.DutyDate, 111) AS DutyDate");
        sb.AppendLine(", EW.DutyCompID, C.CompName AS DutyCompName");
        sb.AppendLine(", EW.DutyOrganID, O.OrganName AS DutyOrganName");
        sb.AppendLine(", P.CompID, PC.CompName, P.EmpID, P.NameN");
        sb.AppendLine(", O2.OrgType, O3.OrganName AS OrgTypeName");
        sb.AppendLine(", P.DeptID, O1.OrganName AS DeptName");
        sb.AppendLine(", P.OrganID, O2.OrganName");
        sb.AppendLine(", ISNULL(W.WTID, '') WTID");
        //sb.AppendLine(", DutyTime = CASE WHEN EW.BranchFlag = '1' THEN LEFT(W.BeginTime, 2) + ':' + RIGHT(W.BeginTime, 2) + '~' + LEFT(W.EndTime, 2) + ':' + RIGHT(W.EndTime, 2)");
        //sb.AppendLine("ELSE LEFT(EW.WTBeginTime, 2) + ':' + RIGHT(EW.WTBeginTime, 2) + '~' + LEFT(EW.WTEndTime, 2) + ':' + RIGHT(EW.WTEndTime, 2) END");
        sb.AppendLine(", DutyTime = LEFT(EW.WTBeginTime, 2) + ':' + RIGHT(EW.WTBeginTime, 2) + '~' + LEFT(EW.WTEndTime, 2) + ':' + RIGHT(EW.WTEndTime, 2)");
        sb.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        //sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 111) + ' ' + CONVERT(VARCHAR, EW.LastChgDate, 24) END");
        sb.AppendLine("FROM EmpGuardWorkTime EW");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O ON O.CompID = EW.DutyCompID AND O.DeptID = EW.DutyDeptID AND O.OrganID = EW.DutyOrganID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company C ON EW.DutyCompID = C.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company PC ON P.CompID = PC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O1 ON O1.CompID = P.CompID AND O1.OrganID = P.DeptID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O2 ON O2.CompID = P.CompID AND O2.OrganID = P.OrganID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Organization O3 ON O3.CompID = P.CompID AND O3.OrganID = O2.OrgType");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..WorkTime W ON EW.WTCompID = W.CompID AND EW.WTID = W.WTID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company LC ON EW.LastChgComp = LC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE EW.DutyCompID = @CompID");
        sb.AppendLine("AND EW.DutyOrganID = @OrganID");
        sb.AppendLine("AND EW.DutyDate = @DutyDate");
        sb.AppendLine("AND EW.BranchFlag = @BranchFlag");
        sb.AppendLine("ORDER BY P.CompID, P.DeptID, P.OrganID, P.EmpID");
        return sb.ToString();
    }

    /// <summary>
    /// 查詢EmpGuardWorkTime資料(修改頁)
    /// </summary>
    public static string GetEmpGuardWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT EW.CompID, EW.EmpID, P.NameN");
        sb.AppendLine(", EW.DutyCompID, EW.DutyDeptID, EW.DutyOrganID");
        sb.AppendLine(", EW.WTID, EW.WTBeginTime, EW.WTEndTime");
        sb.AppendLine(", LastChgComp = ISNULL(LC.CompName, EW.LastChgComp)");
        sb.AppendLine(", LastChgID = ISNULL(LP.NameN, EW.LastChgID)");
        //sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 120) END");
        sb.AppendLine(", LastChgDate = CASE WHEN CONVERT(VARCHAR, EW.LastChgDate, 111) = '1900/01/01' THEN '' ELSE CONVERT(VARCHAR, EW.LastChgDate, 111) + ' ' + CONVERT(VARCHAR, EW.LastChgDate, 24) END");
        sb.AppendLine("FROM EmpGuardWorkTime EW");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal P ON EW.CompID = P.CompID AND EW.EmpID = P.EmpID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Company LC ON EW.LastChgComp = LC.CompID");
        sb.AppendLine("LEFT JOIN " + _eHRMSDB_ITRD + "..Personal LP ON EW.LastChgComp = LP.CompID AND EW.LastChgID = LP.EmpID");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND EW.CompID = @CompID");
        sb.AppendLine("AND EW.EmpID = @EmpID");
        sb.AppendLine("AND EW.DutyDate = @DutyDate");

        return sb.ToString();
    }

    /// <summary>
    /// 查詢EmpWorkTime資料
    /// </summary>
    public static string SelectEmpGuardWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT * FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND DutyDate = @DutyDate");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");

        return sb.ToString();
    }

    /// <summary>
    /// 新增EmpGuardWorkTime資料
    /// </summary>
    public static string AddEmpGuardWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("INSERT INTO EmpGuardWorkTime (");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(") VALUES (");
        sb.AppendLine("@DutyDate, @CompID, @EmpID, @DutyCompID, @DutyDeptID, @DutyOrganID, @WTCompID, @WTID, @WTBeginTime, @WTEndTime, @BranchFlag, @LastChgComp, @LastChgID, @LastChgDate");
        sb.AppendLine(")");

        sb.AppendLine("INSERT INTO EmpGuardWorkTimeLog (");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, ActionFlag, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(")");
        sb.AppendLine("SELECT ");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, 'A', @LastChgComp, @LastChgID, GETDATE()");
        sb.AppendLine("FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");
        sb.AppendLine("AND DutyDate = @DutyDate");

        return sb.ToString();
    }

    /// <summary>
    /// 修改EmpGuardWorkTime資料
    /// </summary>
    public static string UpdateEmpGuardWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("UPDATE EmpGuardWorkTime SET");
        sb.AppendLine("WTCompID = @WTCompID");
        sb.AppendLine(", WTID = @WTID");
        sb.AppendLine(", WTBeginTime = @WTBeginTime");
        sb.AppendLine(", WTEndTime = @WTEndTime");
        sb.AppendLine(", LastChgComp = @LastChgComp");
        sb.AppendLine(", LastChgID = @LastChgID");
        sb.AppendLine(", LastChgDate = @LastChgDate");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");

        sb.AppendLine("INSERT INTO EmpGuardWorkTimeLog (");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, ActionFlag, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(")");
        sb.AppendLine("SELECT ");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, 'U', @LastChgComp, @LastChgID, GETDATE()");
        sb.AppendLine("FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");
        sb.AppendLine("AND DutyDate = @DutyDate");

        return sb.ToString();
    }

    /// <summary>
    /// 刪除EmpGuardWorkTime資料
    /// </summary>
    public static string DeleteEmpGuardWorkTime()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("INSERT INTO EmpGuardWorkTimeLog (");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, ActionFlag, LastChgComp, LastChgID, LastChgDate");
        sb.AppendLine(")");
        sb.AppendLine("SELECT ");
        sb.AppendLine("DutyDate, CompID, EmpID, DutyCompID, DutyDeptID, DutyOrganID, WTCompID, WTID, WTBeginTime, WTEndTime, BranchFlag, 'D', @LastChgComp, @LastChgID, GETDATE()");
        sb.AppendLine("FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");
        sb.AppendLine("AND DutyDate = @DutyDate");

        sb.AppendLine("DELETE FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND EmpID = @EmpID");
        sb.AppendLine("AND DutyDate = @DutyDate");

        return sb.ToString();
    }

    /// <summary>
    /// 查詢值班人數
    /// </summary>
    public static string SelectDutyCnt()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("SELECT COUNT(*)");
        sb.AppendLine("FROM EmpGuardWorkTime");
        sb.AppendLine("WHERE 1 = 1");
        sb.AppendLine("AND DutyDate = @DutyDate");
        sb.AppendLine("AND DutyCompID = @DutyCompID");
        sb.AppendLine("AND DutyDeptID = @DutyDeptID");
        sb.AppendLine("AND DutyOrganID = @DutyOrganID");
        sb.AppendLine("AND BranchFlag = @BranchFlag");
        sb.AppendLine("AND WTCompID = @WTCompID");
        sb.AppendLine("AND WTID = @WTID");

        return sb.ToString();
    }
    #endregion

    /// <summary>
    /// 取得分行註記
    /// </summary>
    public static string LoadOrgBranchMark()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("SELECT BranchFlag FROM OrgBranchMark");
        sb.AppendLine("WHERE 1=1");
        sb.AppendLine("AND CompID = @CompID");
        sb.AppendLine("AND OrganID = @OrganID");

        return sb.ToString();
    }
}