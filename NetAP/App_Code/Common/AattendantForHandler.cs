using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using System.Data;
using SinoPac.WebExpress.Common;
using System.ServiceModel;
using AjaxControlToolkit;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

/// <summary>
/// UnusualReport 的摘要描述
/// </summary>
public class AattendantForHandler
{
    public static DataTable getOrganHRBoss(string _CompID, string _EmpID, string _OTCompID, string _OTEmpID) //for Add 送簽
    {
        var at = new Aattendant();
        return at.getOrganHRBoss(_CompID, _EmpID, _OTCompID, _OTEmpID);
    }

    public static string QueryColumn(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        var at = new Aattendant();
        return at.QueryColumn(strColumn, strTable, strWhere);
    }
    
}