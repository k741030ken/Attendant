using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Class1 的摘要描述
/// </summary>
public class TestMain
{
    public static void Main()
    {
        var isSuccess = false;
        var compID = "";
        var flowCaseID = "";
        var otModel = "";
        DataTable dt = new DataTable("Test");
        DataRow row = dt.NewRow();
        var message = "";
        isSuccess  = FlowUtility.QueryHRFlowEngineDatas_Now(compID, flowCaseID, otModel, out row, out message);
    }
}