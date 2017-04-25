using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Text;

/// <summary>
/// SqlCommand 的摘要描述
/// </summary>
public partial class OnBizReqAppdOperationSql
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// 審核畫面GridView
    /// </summary>
    /// <param name="dataBean">查詢條件</param>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void GetOnBizReqAppdOperationData(CheckVisitGridDataBean dataBean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append("SELECT CompID,EmpID,EmpNameN,CONVERT(NVARCHAR(10),WriteDate,111) AS WriteDate,DeputyID + '-' + DeputyName AS DeputyID_Name,CONVERT(NVARCHAR(10),VisitBeginDate,111) AS VisitBeginDate,CONVERT(NVARCHAR (5),BeginTime) AS BeginTime,CONVERT(NVARCHAR(10),VisitEndDate,111) AS VisitEndDate,CONVERT(NVARCHAR (5),EndTime) AS EndTime,VisitReasonCN,FormSeq");
        sb.Append(" FROM VisitForm");
        sb.Append(" WHERE CompID=@CompID");
        sb.Append(" AND ValidID=@ValidID");
        sb.Append(" AND OBFormStatus='2'");
        sb.Append(" ; ");
    }

    /// <summary>
    /// 單筆審核資料
    /// </summary>
    /// <param name="dataBean">查詢條件</param>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void GetVisitFormDetailData(OnBizReqAppdOperationBean dataBean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append("SELECT VF.CompID + '-' + C.CompName AS CompID_Name,VF.WriterID + '-' + VF.WriterName AS WriterID_Name");
        sb.Append(" ,CONVERT(NVARCHAR(10),VF.WriteDate,111) AS WriteDate,VF.EmpID + '-' + VF.EmpNameN AS EmpID_NameN,VF.VisitFormNo");
        sb.Append(" ,VF.DeptName,VF.TitleName,VF.Position,VF.Tel_1,VF.Tel_2");
        sb.Append(" ,CONVERT(NVARCHAR(10),VF.VisitBeginDate,111) + '~' + CONVERT(NVARCHAR(10),VF.VisitEndDate,111) AS VisitDate");
        sb.Append(" ,CONVERT(NVARCHAR (5),VF.BeginTime) + '~' + CONVERT(NVARCHAR (5),VF.EndTime) AS VisitTime");
        sb.Append(" ,VF.DeputyID + '-' + VF.DeputyName AS DeputyID_Name,VF.LocationType,VF.InterLocationName,VF.ExterLocationName,VF.VisiterName,VF.VisiterTel");
        sb.Append(" ,VF.VisitReasonID + '-' + VF.VisitReasonCN AS VisitReason,VF.VisitReasonDesc,VF.LastChgComp + '-' + Co.CompName AS LastChgComp_Name");
        sb.Append(" ,VF.LastChgID + '-' + P.NameN AS LastChgID_Nanme,REPLACE(CONVERT(NVARCHAR(19),VF.LastChgDate),'-','/') AS LastChgDate");
        sb.Append(" FROM VisitForm VF");
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".dbo.Company C ON VF.CompID = C.CompID");
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".dbo.Company Co ON VF.CompID = Co.CompID");
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".dbo.Personal P ON VF.LastChgComp = P.CompID AND VF.LastChgID = P.EmpID");
        sb.Append(" WHERE VF.CompID=@CompID");
        sb.Append(" AND VF.EmpID=@EmpID");
        sb.Append(" AND VF.WriteDate=@WriteDate");
        sb.Append(" AND VF.FormSeq=@FormSeq");
        sb.Append(" ; ");

    }
}