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
        sb.Append("SELECT CompID,EmpID,EmpNameN,CONVERT(NVARCHAR(10),WriteDate,111) AS WriteDate,DeputyID + '-' + DeputyName AS DeputyID_Name");
        sb.Append(" ,CONVERT(NVARCHAR(10),VisitBeginDate,111) AS VisitBeginDate,CONVERT(NVARCHAR (5),BeginTime) AS BeginTime,CONVERT(NVARCHAR(10)");
        sb.Append(" ,VisitEndDate,111) AS VisitEndDate,CONVERT(NVARCHAR (5),EndTime) AS EndTime,VisitReasonCN,FormSeq,VF.FlowCaseID,OBL.FlowLogID");
        sb.Append(" FROM VisitForm VF");
        sb.Append(" LEFT JOIN OnBizReqAppd_ITRDFlowOpenLog OBL ON VF.FlowCaseID = OBL.FlowCaseID ");
        sb.Append(" LEFT JOIN PS_UserProxy UP ON VF.ValidID = UP.UserID AND CONVERT(VARCHAR(8),GETDATE(),112) BETWEEN UP.ProxyStartDate AND UP.ProxyEndDate");
        sb.Append(" WHERE CompID = @CompID");
        sb.Append(" AND (ValidID = @ValidID");
        sb.Append(" OR UP.ProxyUser = @ValidID)");
        sb.Append(" AND EmpID <> @ValidID ");
        sb.Append(" AND OBFormStatus = '2'");
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CompID"></param>
    /// <param name="EmpID"></param>
    /// <param name="WriteDate"></param>
    /// <param name="FormSeq"></param>
    /// <param name="OBFormStatus"></param>
    /// <param name="RejectReasonCN"></param>
    /// <param name="sb"></param>
    public static void UpdateVisitForm(string CompID, string EmpID, string WriteDate, string FormSeq, string OBFormStatus, string RejectReasonCN, ref CommandHelper sb)
    {

        string SQL = @"	UPDATE VisitForm SET OBFormStatus = '{0}'
                        ,RejectReasonCN = '{1}'
                        WHERE CompID = '{2}'
                        AND EmpID = '{3}'
                        AND WriteDate = '{4}'
                        AND FormSeq = '{5}' ;";
        sb.AppendStatement(string.Format(SQL, OBFormStatus, RejectReasonCN, CompID, EmpID, WriteDate, FormSeq));
        
    }


    private void UpdateHROverTimeLog(string FlowCaseID, string FlowStatus, ref  CommandHelper sb)
    {
        string SQL = @"	UPDATE HROtherFlowLog SET FlowStatus = '{0}' 
                        WHERE FlowCaseID = '{1}'
                        AND Seq = (SELECT TOP 1 Seq FROM HROtherFlowLog WHERE FlowCaseID = '{1}' ORDER BY Seq DESC) ;";

        sb.AppendStatement(string.Format(SQL, FlowStatus, FlowCaseID));
    }
    
}