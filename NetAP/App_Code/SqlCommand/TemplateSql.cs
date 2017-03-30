using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Text;

/// <summary>
/// TemplateSql 的摘要描述
/// </summary>
public partial class SqlCommand
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// GetEmpFlowSNEmpAndSex
    /// </summary>
    /// <param name="dataBean">data</param>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void GetEmpFlowSNEmpAndSexSql(TemplateBean dataBean, ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" EFSN.CompID, EFSN.EmpID ");
        sb.Append(" ,P.NameN, P.Sex ");
        sb.Append(" FROM EmpFlowSN AS EFSN ");
        sb.Append(" LEFT JOIN " + _eHRMSDB_ITRD + ".dbo.Personal AS P");
        sb.Append(" ON EFSN.CompID = P.CompID AND EFSN.EmpID = P.EmpID ");
        sb.Append(" WHERE 0 = 0 ");
        if (!String.IsNullOrEmpty(dataBean.CompID))
        {
            sb.Append(" AND EFSN.CompID=@CompID ");
        }
        if (!String.IsNullOrEmpty(dataBean.EmpID))
        {
            sb.Append(" AND EFSN.EmpID=@EmpID ");
        }
        if (!String.IsNullOrEmpty(dataBean.NameN))
        {
            sb.Append(" AND P.NameN=@NameN ");
        }
        if (!String.IsNullOrEmpty(dataBean.Sex))
        {
            sb.Append(" AND P.Sex =@Sex ");
        }
        sb.Append(" ; ");
    }

    /// <summary>
    /// SelectTemplateSql
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void SelectTemplateSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" SELECT ");
        sb.Append(" CompID, EmpID, LastChgComp, LastChgID, LastChgDate ");
        sb.Append(" FROM EmpFlowSN ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// DeleteTemplateSql
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void DeleteTemplateSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" DELETE ");
        sb.Append(" FROM EmpFlowSN ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// UpdateTemplateSql
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void UpdateTemplateSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" UPDATE EmpFlowSN SET ");
        sb.Append(" LastChgComp=@LastChgComp ");
        sb.Append(" , LastChgID=@LastChgID ");
        sb.Append(" , LastChgDate=@LastChgDate ");
        sb.Append(" WHERE 0 = 0 ");
        sb.Append(" AND CompID=@CompID ");
        sb.Append(" AND EmpID=@EmpID ");
        sb.Append(" ; ");
    }

    /// <summary>
    /// InsertTemplateSql
    /// </summary>
    /// <param name="sb">傳入之前組好Command</param>
    /// <param name="isReset">StringBuilder Reset or not</param>
    public static void InsertTemplateSql(ref StringBuilder sb, bool isReset = false)
    {
        if (isReset)
        {
            sb = new StringBuilder();
        }
        sb.Append(" INSERT INTO EmpFlowSN ( ");
        sb.Append(" CompID, EmpID, LastChgComp, LastChgID, LastChgDate ");
        sb.Append(" ) VALUES ( ");
        sb.Append(" @CompID, @EmpID, @LastChgComp, @LastChgID, @LastChgDate ");
        sb.Append(" ); ");
    }
}