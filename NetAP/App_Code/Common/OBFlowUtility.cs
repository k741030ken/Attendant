using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common;
using System.Data;
using Newtonsoft.Json;


/// <summary>
/// OBFlowUtility 的摘要描述
/// </summary>
public class OBFlowUtility
{
    #region "全域變數"
    private static string _AattendantDBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private static string _eHRMSDB_ITRD = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private static DbHelper db = new DbHelper(_AattendantDBName);
    #endregion "全域變數"

    #region "第一位/下位簽核者"
    /// <summary>
    /// 第一位簽核者
    /// </summary>
    /// <param name="compID">公出人員公司</param>
    /// <param name="EmpID">公出人員</param>
    /// <param name="applyID">登錄者</param>
    /// <param name="queryDate">公出開始日</param>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="flowSN">流程識別碼</param>
    /// <param name="toUserData">下位簽核者資料</param>
    /// <param name="nextIsLastFlow">下位簽核者是否為最後一位簽核</param>
    /// <param name="message">訊息</param>
    /// <returns>bool</returns>
    private static bool QueryToUserDataFirst(string compID, string EmpID, string organID, string flowOrganID, string applyID, string queryDate, string flowCode, string flowSN,
        out Dictionary<string, string> toUserData, out bool nextIsLastFlow, out string message)
    {
        var isSuccess = false;
        nextIsLastFlow = false;
        message = "";
        var dt = new DataTable();
        var nextRow = dt.NewRow();
        toUserData = new Dictionary<string, string>();
        try
        {
            if (QueryFirstHRFlowEngineData(compID, flowCode, flowSN, out nextRow, out message))
            {
                nextIsLastFlow = nextRow["FlowEndFlag"].ToString() == "1";
                var signLine = nextRow["SignLineDefine"].ToString(); // 1:行政 2:功能 3: 特殊人員
                isSuccess = QueryToUserData(compID, EmpID, organID, flowOrganID, queryDate, signLine, nextRow, true, out toUserData, out message);
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 下位簽核者
    /// </summary>
    /// <param name="compID">公司</param>
    /// <param name="applyID">目前簽核者</param>
    /// <param name="queryDate">加班開始日</param>
    /// <param name="flowCaseID">流程系統ID</param>
    /// <param name="otModel">A:預先申請 D:事後申報</param>
    /// <param name="toUserData">下位簽核者資料</param>
    /// <param name="isLastFlow">本次是否為最後一位簽核</param>
    /// <param name="nextIsLastFlow">下位簽核者是否為最後一位簽核</param>
    /// <param name="message">訊息</param>
    /// <returns>bool</returns>
    private static bool QueryToUserData(string compID, string applyID, string queryDate, string flowCaseID, string otModel,
        out Dictionary<string, string> toUserData, out string flowCode, out string flowSN, out string signLineDefine, out bool isLastFlow, out bool nextIsLastFlow, out string message)
    {
        var isSuccess = false;
        nextIsLastFlow = false;
        isLastFlow = false;
        message = "";
        flowCode = "";
        flowSN = "";
        signLineDefine = "";
        var dt = new DataTable();
        var logdt = new DataTable();
        var nextRow = dt.NewRow();
        var thisLogRow = logdt.NewRow();
        var index = 0;
        toUserData = new Dictionary<string, string>();
        try
        {
            if (QueryNextHRFlowEngineData(compID, flowCaseID, otModel, out thisLogRow, out logdt, out dt, out index, out nextRow, out flowCode, out flowSN, out signLineDefine, out message))
            {
                nextIsLastFlow = nextRow["FlowEndFlag"].ToString() == "1";
                isLastFlow = dt.Rows[index]["FlowEndFlag"].ToString() == "1";
                var empID = applyID;
                var sOrganID = thisLogRow["SignOrganID"].ToString();
                var sFlowOrganID = thisLogRow["SignFlowOrganID"].ToString();
                var signLine = nextRow["SignLineDefine"].ToString();
                if (signLine != "3") //下一關不為特定人員
                {
                    if (signLineDefine == "3") //為特定人員時回去找與下一關相同的線的核准者或加班人
                    {
                        empID = thisLogRow["OTEmpID"].ToString();
                        sOrganID = thisLogRow["EmpOrganID"].ToString();
                        sFlowOrganID = thisLogRow["EmpFlowOrganID"].ToString();
                        for (var i = logdt.Rows.Count - 2; i >= 0; i--)
                        {
                            var item = logdt.Rows[i];
                            if (item["SignLine"].ToString() != "3" && item["SignLine"].ToString() == signLine)
                            {
                                empID = item["ApplyID"].ToString();
                                sOrganID = item["SignOrganID"].ToString();
                                sFlowOrganID = item["SignFlowOrganID"].ToString();
                                break;
                            }
                        }
                    }
                    else if (signLineDefine != nextRow["SignLineDefine"].ToString()) //與下一關不同線時回去找同一線的核准者或加班人
                    {
                        empID = thisLogRow["OTEmpID"].ToString();
                        sOrganID = thisLogRow["EmpOrganID"].ToString();
                        sFlowOrganID = thisLogRow["EmpFlowOrganID"].ToString();
                        for (var i = logdt.Rows.Count - 2; i >= 0; i--)
                        {
                            var item = logdt.Rows[i];
                            if (item["SignLine"].ToString() == signLine)
                            {
                                empID = item["ApplyID"].ToString();
                                sOrganID = item["SignOrganID"].ToString();
                                sFlowOrganID = item["SignFlowOrganID"].ToString();

                                break;
                            }
                        }
                    }
                }
                isSuccess = QueryToUserData(compID, empID, sOrganID, sFlowOrganID, queryDate, signLine, nextRow, false, out toUserData, out message);
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢下位簽核者
    /// </summary>
    /// <param name="compID">string</param>
    /// <param name="empID">string</param>
    /// <param name="sOrganID">string</param>
    /// <param name="sFlowOrganID">string</param>
    /// <param name="inDate">string</param>
    /// <param name="signLine">string</param>
    /// <param name="nextRow">DataRow</param>
    /// <param name="toUserData"> Dictionary<string, string> </param>
    /// <param name="message">string</param>
    /// <returns>bool</returns>
    private static bool QueryToUserData(string compID, string empID, string sOrganID, string sFlowOrganID, string inDate, string signLine, DataRow nextRow, bool isFirst, out Dictionary<string, string> toUserData, out string message)
    {

        var isSuccess = false;
        message = "";
        toUserData = new Dictionary<string, string>();
        var SignIDComp = "";
        var SignID = "";
        var SignOrganID = "";
        var SignFlowOrganID = "";
        var sNameN = "";
        var rankID = "";
        var upOrganID = "";
        var boss = "";
        var bossCompID = "";
        var deptID = "";
        var positionID = "";
        var workTypeID = "";
        var flowDeptID = "";
        var flowUpOrganID = "";
        var flowBoss = "";
        var flowBossCompID = "";
        var businessType = "";
        var empFlowRemark = "";
        var titleID = "";
        try
        {
            switch (signLine)
            {
                case "1": // 1:行政 
                    {
                        if (isFirst)
                        {
                            if (EmpInfo.QueryEmpData(compID, empID, inDate, out sNameN, out rankID, out titleID,
                            out SignOrganID, out upOrganID, out SignID, out SignIDComp, out deptID, out positionID, out workTypeID,
                            out SignFlowOrganID, out flowDeptID, out flowUpOrganID, out flowBoss, out flowBossCompID, out businessType, out empFlowRemark))
                            {
                                SignFlowOrganID = "";
                                if (empID == SignID)
                                {
                                    if (EmpInfo.QueryOrganData(compID, sOrganID, inDate, out SignOrganID, out SignID, out SignIDComp))
                                    {
                                        isSuccess = true;
                                    }
                                    else
                                    {
                                        throw new Exception("流程關卡無簽核者資料!!");
                                    }
                                }
                                else
                                {
                                    isSuccess = true;
                                }
                            }
                            else
                            {
                                throw new Exception("流程關卡無簽核者資料!!");
                            }
                        }
                        else
                        {
                            if (EmpInfo.QueryOrganData(compID, sOrganID, inDate, out SignOrganID, out SignID, out SignIDComp))
                            {
                                isSuccess = true;
                            }
                            else
                            {
                                throw new Exception("流程關卡無簽核者資料!!");
                            }
                        }

                        break;
                    }
                case "2": // 2:功能
                    {
                        if (isFirst)
                        {
                            if (EmpInfo.QueryEmpData(compID, empID, inDate, out sNameN, out rankID, out titleID,
                            out SignOrganID, out upOrganID, out boss, out bossCompID, out deptID, out positionID, out workTypeID,
                            out SignFlowOrganID, out flowDeptID, out flowUpOrganID, out SignID, out SignIDComp, out businessType, out empFlowRemark))
                            {
                                SignOrganID = "";
                                if (empID == SignID)
                                {
                                    if (EmpInfo.QueryFlowOrganData(sFlowOrganID, inDate, out SignFlowOrganID, out SignID, out SignIDComp))
                                    {
                                        isSuccess = true;
                                    }
                                    else
                                    {
                                        throw new Exception("流程關卡無簽核者資料!!");
                                    }
                                }
                                else
                                {
                                    isSuccess = true;
                                }

                            }
                            else
                            {
                                throw new Exception("流程關卡無簽核者資料!!");
                            }
                        }
                        else
                        {
                            if (EmpInfo.QueryFlowOrganData(sFlowOrganID, inDate, out SignFlowOrganID, out SignID, out SignIDComp))
                            {
                                isSuccess = true;
                            }
                            else
                            {
                                throw new Exception("流程關卡無簽核者資料!!");
                            }
                        }

                        break;
                    }
                case "3": // 3: 特殊人員
                    {
                        SignIDComp = nextRow["SpeComp"].ToString();
                        SignID = nextRow["SpeEmpID"].ToString();
                        if (string.IsNullOrEmpty(SignID))
                        {
                            throw new Exception("流程關卡無設定特定人員資料!!");
                        }

                        if (EmpInfo.QueryEmpData(SignIDComp, SignID, inDate, out sNameN, out rankID, out titleID,
                            out SignOrganID, out upOrganID, out boss, out bossCompID, out deptID, out positionID, out workTypeID,
                            out SignFlowOrganID, out flowDeptID, out flowUpOrganID, out flowBoss, out flowBossCompID, out businessType, out empFlowRemark))
                        {
                            isSuccess = true;
                        }
                        else
                        {
                            throw new Exception("無特定人員資料!!");
                        }
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        toUserData.Add("SignIDComp", SignIDComp);
        toUserData.Add("SignID", SignID);
        toUserData.Add("SignOrganID", SignOrganID);
        toUserData.Add("SignFlowOrganID", SignFlowOrganID);
        toUserData.Add("SignLine", signLine);
        return isSuccess;
    }

    #endregion "第一個/下位簽核者"

    #region "公出流程所需資料"
    /// <summary>
    /// 公出流程所需資料
    /// </summary>
    /// <param name="compID">公出人員公司</param>
    /// <param name="sOrganID">公出人員行政線organID</param>
    /// <param name="sFlowOrganID">公出人員功能線organID</param>
    /// <param name="empID">公出人員</param>
    /// <param name="applyID">登錄人員</param>
    /// <param name="queryDate">公出日期</param>
    /// <param name="empData">回傳: 公出人員資訊</param>
    /// <param name="toUserData">回傳: 下位簽核者資訊</param>
    /// <param name="flowCode">回傳: 流程代號</param>
    /// <param name="flowSN">回傳: 流程識別碼</param>
    /// <param name="nextIsLastFlow">回傳: 下個關卡是否為最後關卡</param>
    /// <param name="meassge">回傳: 訊息</param>
    /// <returns>bool</returns>
    public static bool QueryFlowDataAndToUserData_First(string compID, string sOrganID, string sFlowOrganID, string empID, string applyID, string queryDate,
        out Dictionary<string, string> empData, out Dictionary<string, string> toUserData, out string flowCode, out string flowSN, out bool nextIsLastFlow, out string meassge)
    {
        var isSuccess = false;
        meassge = "";
        toUserData = new Dictionary<string, string>();
        empData = new Dictionary<string, string>();
        flowCode = "OB01";
        flowSN = "";
        nextIsLastFlow = false;
        string sNameN = "";
        string rankID = "";
        string organID = "";
        string upOrganID = "";
        string boss = "";
        string bossCompID = "";
        string deptID = "";
        string positionID = "";
        string workTypeID = "";
        string flowOrganID = "";
        string flowDeptID = "";
        string flowUpOrganID = "";
        string flowBoss = "";
        string flowBossCompID = "";
        string businessType = "";
        string empFlowRemark = "";
        string titleID = "";

        try
        {
            if (EmpInfo.QueryEmpData(compID, empID, queryDate, out sNameN, out rankID, out titleID,
                out organID, out upOrganID, out boss, out bossCompID, out deptID, out positionID, out workTypeID,
                out flowOrganID, out flowDeptID, out flowUpOrganID, out flowBoss, out flowBossCompID, out businessType, out empFlowRemark))
            {
                if (!string.IsNullOrEmpty(sOrganID))
                {
                    organID = sOrganID;
                }
                if (!string.IsNullOrEmpty(sFlowOrganID))
                {
                    flowOrganID = sFlowOrganID;
                }
                if (QueryOTFlowData(compID, empID, deptID, organID, businessType, rankID, titleID, empFlowRemark, positionID, workTypeID, out flowCode, out flowSN, out meassge))
                {
                    if (QueryToUserDataFirst(compID, empID, organID, flowOrganID, applyID, queryDate, flowCode, flowSN, out toUserData, out nextIsLastFlow, out meassge))
                    {
                        isSuccess = true;
                    }
                }
            }
            else
            {
                throw new Exception("無加班者資料!!");
            }
            empData.Add("EmpID", empID);
            empData.Add("NameN", sNameN);
            empData.Add("RankID", rankID);
            empData.Add("OrganID", organID);
            empData.Add("UpOrganID", upOrganID);
            empData.Add("Boss", boss);
            empData.Add("DeptID", deptID);
            empData.Add("PositionID", positionID);
            empData.Add("WorkTypeID", workTypeID);
            empData.Add("FlowOrganID", flowOrganID);
            empData.Add("FlowUpOrganID", flowUpOrganID);
            empData.Add("FlowBoss", flowBoss);
            empData.Add("FlowDeptID", flowDeptID);
            empData.Add("BusinessType", businessType);
            empData.Add("EmpFlowRemark", empFlowRemark);

        }
        catch (Exception ex)
        {
            meassge = ex.Message;
        }
        return isSuccess;
    }
    #endregion "公出流程所需資料"

    #region "第一個/下一個流程關卡資料"

    /// <summary>
    /// 第一個流程關卡資料
    /// </summary>
    /// <param name="compID">公司</param>
    /// <param name="flowCaseID">流程系統ID</param>
    /// <param name="nextRow">DataRow</param>
    /// <param name="message">string</param>
    /// <returns>bool</returns>
    private static bool QueryFirstHRFlowEngineData(string compID, string flowCode, string flowSN, out DataRow nextRow, out string message)
    {
        var isSuccess = false;
        var systemID = "OB";
        message = "";
        var returnTable = new DataTable();

        if (QueryHRFlowEngineDatas(compID, systemID, flowCode, flowSN, out returnTable, out message) && returnTable.Rows.Count > 0)
        {
            nextRow = returnTable.Rows[0];
            isSuccess = true;
        }
        else
        {
            nextRow = returnTable.NewRow();
            message = "無流程關卡資料!!";
        }
        return isSuccess;
    }

    /// <summary>
    /// 下一個流程關卡資料
    /// </summary>
    /// <param name="compID">公司</param>
    /// <param name="flowCaseID">流程系統ID</param>
    /// <param name="otModel">A:預先申請 D:事後申報</param>
    /// <param name="returnTable">列出關卡</param>
    /// <param name="thisRowIndex">目前關卡所在</param>
    /// <param name="nextRow">DataRow</param>
    /// <param name="flowCode">目前flowCode</param>
    /// <param name="flowSN">目前flowSN</param>
    /// <param name="signLineDefine">目前signLineDefine</param>
    /// <param name="message">string</param>
    /// <returns>bool</returns>
    private static bool QueryNextHRFlowEngineData(string compID, string flowCaseID, string otModel, out DataRow returnRow, out DataTable returnTable, out DataTable returnTable2, out int thisRowIndex, out DataRow nextRow, out string flowCode, out string flowSN, out string signLineDefine, out string message)
    {
        var isSuccess = false;
        var systemID = "OB";
        message = "";
        flowCode = "";
        flowSN = "";
        signLineDefine = "";
        returnTable = new DataTable();
        returnTable2 = new DataTable();
        thisRowIndex = 0;
        returnRow = returnTable.NewRow();
        if (QueryHROverTimeLogLast(flowCaseID, otModel, out returnTable, out returnRow, out message))
        {
            flowCode = returnRow["FlowCode"].ToString();
            flowSN = returnRow["FlowSN"].ToString();
            signLineDefine = returnRow["SignLine"].ToString();
            var flowSeq = returnRow["FlowSeq"].ToString();
            if (QueryHRFlowEngineDatas(compID, systemID, flowCode, flowSN, out returnTable2, out message) && returnTable2.Rows.Count > 0)
            {
                var thisRow = returnTable2.Rows.Cast<DataRow>().Where(row => row.Field<string>("FlowSeq") == flowSeq).FirstOrDefault();
                thisRowIndex = returnTable2.Rows.IndexOf(thisRow);
                if (thisRowIndex < returnTable2.Rows.Count - 1)
                {
                    nextRow = returnTable2.Rows[thisRowIndex + 1];
                    isSuccess = true;
                }
                else
                {
                    nextRow = returnTable2.NewRow();
                    message = "無下一個流程關卡資料!!";
                }
            }
            else
            {
                nextRow = returnTable2.NewRow();
                message = "無下一個流程關卡資料!!";
            }
        }
        else
        {
            nextRow = returnTable2.NewRow();
        }
        return isSuccess;
    }
    #endregion "第一個/下一個流程關卡資料"

    #region "FlowLog"
    /// <summary>
    /// 查詢流程最後一筆紀錄
    /// </summary>
    /// <param name="flowCaseID">流程系統ID</param>
    /// <param name="otModel">A:預先申請 D:事後申報</param>
    /// <param name="returnTable">DataTable</param>
    /// <param name="returnRow">DataRow</param>
    /// <param name="message">string</param>
    /// <returns>bool</returns>
    public static bool QueryHROverTimeLogLast(string flowCaseID, string otModel, out DataTable returnTable, out DataRow returnRow, out string message)
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        if (QueryHROverTimeLog(flowCaseID, otModel, out returnTable, out message))
        {
            if (string.IsNullOrEmpty(message) && returnTable.Rows.Count > 0)
            {
                returnRow = returnTable.Rows.Cast<DataRow>().LastOrDefault();
                isSuccess = true;
            }
            else
            {
                returnRow = returnTable.NewRow();
                message = "無流程最後一筆紀錄!!";
            }
        }
        else
        {
            returnRow = returnTable.NewRow();
        }
        return isSuccess;
    }
    #endregion "FlowLog"

    #region "適用流程"

    /// <summary>
    /// 查詢OT的flowCode & flowSN
    /// </summary>
    /// <param name="compID">公司</param>
    /// <param name="empID">人員</param>
    /// <param name="flowCodeFlag">0: 事前; 1: 事後</param>
    /// <param name="deptID">部門</param>
    /// <param name="organID">單位</param>
    /// <param name="businessType">業務類別</param>
    /// <param name="rankID">職等</param>
    /// <param name="titleID">職稱</param>
    /// <param name="empFlowRemark">功能備註</param>
    /// <param name="positionID">職位</param>
    /// <param name="workTypeID">工作性質</param>
    /// <param name="flowCode">return: flowCode</param>
    /// <param name="flowSN">return: flowSN</param>
    /// <param name="meassge">return: meassge</param>
    /// <returns>bool</returns>
    private static bool QueryOTFlowData(string compID, string empID, string deptID, string organID, string businessType,
        string rankID, string titleID, string empFlowRemark, string positionID, string workTypeID, out string flowCode, out string flowSN,
        out string meassge)
    {
        var isSuccess = false;
        flowSN = "";
        flowCode = "OB01";
        meassge = "";
        flowSN = QueryEmpFlowSN(compID, empID, flowCode);
        var returnTable = new DataTable("returnTable");
        var returnTable2 = new DataTable("returnTable2");
        if (!string.IsNullOrEmpty(flowSN) &&
            QueryHRFlowEmpDefineDatas(compID, "OB", flowCode, flowSN, out returnTable, out meassge) &&
            string.IsNullOrEmpty(meassge) &&
            QueryHRFlowEngineDatas(compID, "OB", flowCode, flowSN, out returnTable2, out meassge) &&
            returnTable2.Rows.Count > 0 &&
            string.IsNullOrEmpty(meassge))
        {
            isSuccess = true;
        }
        else
        {
            isSuccess = QueryHRFlowEmpDefineDatas(flowCode, empID, compID, deptID, organID, businessType, rankID, titleID, empFlowRemark, positionID, workTypeID, out flowSN, out meassge);
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢該員FlowSN
    /// </summary>
    /// <param name="compID">公司</param>
    /// <param name="empID">人員</param>
    /// <param name="flowCode">要查詢的流程代碼</param>
    /// <returns>string</returns>
    private static string QueryEmpFlowSN(string compID, string empID, string flowCode)
    {
        var result = "";
        var isSuccess = false;
        var message = "";
        var systemID = "OB";
        var returnTable = new DataTable("returnTable");
        isSuccess = QueryEmpFlowSN(compID, empID, systemID, flowCode, out returnTable, out message);
        if (isSuccess && string.IsNullOrEmpty(message))
        {
            var rowData = returnTable.Rows[0];
            if (rowData["FlowSN"] != null && !string.IsNullOrEmpty(rowData["FlowSN"].ToString()))
            {
                result = rowData["FlowSN"].ToString();
            }
        }
        return result;
    }

    /// <summary>
    /// 查詢OT特殊流程與預設流程
    /// </summary>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="empID">員編</param>
    /// <param name="flowCompID">公司</param>
    /// <param name="deptID">部門</param>
    /// <param name="organID">單位</param>
    /// <param name="businessType">業務類別</param>
    /// <param name="rankID">職等</param>
    /// <param name="titleID">職稱</param>
    /// <param name="empFlowRemark">功能備註</param>
    /// <param name="positionID">職位</param>
    /// <param name="workTypeID">工作性質</param>
    /// <param name="flowSN">return: flowSN</param>
    /// <param name="message">return: message</param>
    /// <returns>bool</returns>
    private static bool QueryHRFlowEmpDefineDatas(string flowCode, string empID, string flowCompID, string deptID, string organID, string businessType,
        string rankID, string titleID, string empFlowRemark, string positionID, string workTypeID, out string flowSN, out string message)
    {
        var isSuccess = false;
        flowSN = "";
        message = "";
        try
        {
            var returnTable = new DataTable("returnTable");
            if (QueryHRFlowEmpDefineDatas("OB", flowCode, flowCompID, deptID, organID, empID, businessType, rankID, titleID, empFlowRemark, positionID, workTypeID, out returnTable, out message))
            {
                //篩選關卡設定有誤或沒有的
                for (var i = (returnTable.Rows.Count - 1); i >= 0; i--)
                {
                    var isDataOK = false;
                    var itemData = returnTable.Rows[i];
                    if (itemData != null && itemData["FlowSN"] != null && itemData["FlowSN"].ToString() != "")
                    {
                        var sFlowSN = itemData["FlowSN"].ToString();
                        var returnTable2 = new DataTable("returnTable2");
                        if (QueryHRFlowEngineDatas(flowCompID, "OB", flowCode, sFlowSN, out returnTable2, out message) && returnTable2.Rows.Count > 0)
                        {
                            int flowStartFlagCount = returnTable2.Rows.Cast<DataRow>().Where(row => row.Field<string>("FlowStartFlag") == "1").Count();
                            int flowEndFlagCount = returnTable2.Rows.Cast<DataRow>().Where(row => row.Field<string>("FlowEndFlag") == "1").Count();
                            if (flowStartFlagCount == 1 && flowEndFlagCount == 1)
                            {
                                isDataOK = true;
                            }
                        }
                    }
                    if (!isDataOK)
                    {
                        returnTable.Rows.RemoveAt(i);
                    }
                }

                if (returnTable.Rows.Count > 0)
                {
                    var rowData = returnTable.Rows.Cast<DataRow>().Where(row => row.Field<string>("EmpID") == empID).FirstOrDefault();
                    if (rowData != null && rowData["FlowSN"] != null && rowData["FlowSN"].ToString() != "")
                    {
                        flowSN = rowData["FlowSN"].ToString();
                        isSuccess = true;
                    }
                }

                if (!isSuccess)
                {
                    var rowData = returnTable.Rows.Cast<DataRow>().Where(row => row.Field<string>("PrincipalFlag") == "1").FirstOrDefault();
                    if (rowData != null && rowData["FlowSN"] != null && rowData["FlowSN"].ToString() != "")
                    {
                        flowSN = rowData["FlowSN"].ToString();
                        isSuccess = true;
                    }
                }
            }
            if (!isSuccess)
            {
                message = "查無流程設定，請洽資訊人員!!";
            }
        }
        catch (Exception ex)
        {

            message = ex.Message;
        }
        return isSuccess;
    }

    #endregion "適用流程"

    #region "DB"

    /// <summary>
    /// 查詢流程log
    /// </summary>
    /// <param name="flowCaseID">流程ID</param>
    /// <param name="otMode">A:預先申請 D:事後申報</param>
    /// <param name="returnTable">return: returnTable</param>
    /// <param name="message">return: message</param>
    /// <returns>bool</returns>
    private static bool QueryHROverTimeLog(string flowCaseID, string otMode, out DataTable returnTable, out string message)
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" FlowCaseID, Seq "); //PK
            sb.Append(" , FlowLogBatNo, FlowLogID "); //流程系統所需key
            sb.Append(" , OTMode, OTEmpID, EmpOrganID, EmpFlowOrganID, ApplyID "); //申請相關資料
            sb.Append(" , FlowCode, FlowSN, FlowSeq, SignLine "); //流程設定資料
            sb.Append(" , SignIDComp, SignID, SignOrganID, SignFlowOrganID, SignTime "); //簽核者資料
            sb.Append(" , ReAssignFlag, ReAssignComp, ReAssignEmpID "); //改派資料
            sb.Append(" , FlowStatus, Remark "); //狀態與備註
            sb.Append(" FROM HROverTimeLog ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND OTMode = ").AppendParameter("OTMode", otMode); //A:預先申請 D:事後申報
            sb.Append(" AND FlowCaseID = ").AppendParameter("FlowCaseID", flowCaseID); //流程ID
            sb.Append(" ORDER BY FlowCaseID, Seq ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            isSuccess = true;
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            returnTable = ds.Tables[0];
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢人員流程設定檔
    /// </summary>
    /// <param name="compID">公司代碼</param>
    /// <param name="empID">人員</param>
    /// <param name="compID">系統別 : OT=>加班</param>
    /// <param name="empID">流程代碼</param>
    /// <param name="returnTable">return: returnTable</param>
    /// <param name="message">return: message</param>
    /// <returns>bool</returns>
    private static bool QueryEmpFlowSN(string compID, string empID, string systemID, string flowCode, out DataTable returnTable, out string message)
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" ES.CompID, ES.EmpID "); //PK
            sb.Append(" , ESD.SystemID, ESD.FlowCode, ESD.FlowSN, ESD.PrincipalFlag ");
            sb.Append(" FROM EmpFlowSN AS ES ");
            sb.Append(" JOIN EmpFlowSNDefine AS ESD ");
            sb.Append(" ON ES.CompID = ESD.CompID AND ES.EmpID = ESD.EmpID ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND ES.CompID = ").AppendParameter("CompID", compID); //公司代碼
            sb.Append(" AND ES.EmpID = ").AppendParameter("EmpID", empID); //公司代碼
            sb.Append(" AND ESD.SystemID = ").AppendParameter("SystemID", systemID); //系統別 : OT=>加班
            sb.Append(" AND ESD.FlowCode = ").AppendParameter("FlowCode", flowCode); //流程代碼
            sb.Append(" AND ESD.PrincipalFlag = ").AppendParameter("PrincipalFlag", "1"); //主要註記
            sb.Append(" ORDER BY CompID, EmpID, SystemID ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            isSuccess = true;
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            returnTable = ds.Tables[0];

        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢流程適用人員設定檔
    /// </summary>
    /// <param name="compID">公司代碼</param>
    /// <param name="systemID">系統別 : OT=>加班</param>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="flowSN">流程識別碼</param>
    /// <param name="returnTable">return: returnTable</param>
    /// <param name="message">return: message</param>
    /// <returns>bool</returns>
    private static bool QueryHRFlowEmpDefineDatas(string compID, string systemID, string flowCode, string flowSN, out DataTable returnTable, out string message)
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" CompID, SystemID, FlowCode, FlowSN, Seq "); //PK
            sb.Append(" , FlowCompID, DeptID, OrganID, EmpID, BusinessType, RankIDTop, RankIDBottom, TitleIDTop, TitleIDBottom, PositionID, WorkTypeID, EmpFlowRemark, PrincipalFlag, InValidFlag ");
            sb.Append(" FROM HRFlowEmpDefine ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND InValidFlag = ").AppendParameter("InValidFlag", "0"); //無效註記;
            sb.Append(" AND CompID = ").AppendParameter("CompID", compID); //公司代碼
            sb.Append(" AND SystemID = ").AppendParameter("SystemID", systemID); //系統別 : OT=>加班
            sb.Append(" AND FlowCode = ").AppendParameter("FlowCode", flowCode); //流程代碼
            sb.Append(" AND FlowSN = ").AppendParameter("FlowSN", flowSN); //流程識別碼
            sb.Append(" ORDER BY CompID, SystemID, FlowCode, Seq, FlowSN ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            isSuccess = true;
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            returnTable = ds.Tables[0];

        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢適用流程
    /// </summary>
    /// <param name="systemID">系統代碼</param>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="flowCompID">公司</param>
    /// <param name="deptID">部門</param>
    /// <param name="organID">單位</param>
    /// <param name="empID">員編</param>
    /// <param name="businessType">業務類別</param>
    /// <param name="rankID">職等</param>
    /// <param name="titleID">職稱</param>
    /// <param name="empFlowRemark">功能備註</param>
    /// <param name="positionID">職位</param>
    /// <param name="workTypeID">工作性質</param>
    /// <param name="returnTable">return: returnTable</param>
    /// <param name="message">return: message</param>
    /// <returns>bool</returns>
    private static bool QueryHRFlowEmpDefineDatas(string systemID, string flowCode, string flowCompID, string deptID, string organID,
        string empID, string businessType, string rankID, string titleID, string empFlowRemark,
        string positionID, string workTypeID, out DataTable returnTable, out string message)
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" hr.CompID, hr.SystemID, hr.FlowCode, hr.FlowSN, hr.Seq "); //PK
            sb.Append(" , hr.FlowCompID, hr.DeptID, hr.OrganID, hr.EmpID, hr.BusinessType, hr.RankIDTop, hr.RankIDBottom, hr.TitleIDTop, hr.TitleIDBottom, hr.PositionID, hr.WorkTypeID, hr.EmpFlowRemark, hr.PrincipalFlag, hr.InValidFlag ");
            sb.Append(" FROM HRFlowEmpDefine AS hr ");
            sb.Append(" LEFT JOIN " + Aattendant._eHRMSDB_ITRD + ".dbo.RankMapping AS rmT ");
            sb.Append(" ON hr.CompID = rmT.CompID AND hr.RankIDTop = rmT.RankID ");
            sb.Append(" LEFT JOIN " + Aattendant._eHRMSDB_ITRD + ".dbo.RankMapping AS rmB ");
            sb.Append(" ON hr.CompID = rmB.CompID AND hr.RankIDBottom = rmB.RankID ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND hr.SystemID = ").AppendParameter("SystemID", systemID); //系統代碼
            sb.Append(" AND hr.FlowCode = ").AppendParameter("FlowCode", flowCode); //流程代碼
            sb.Append(" AND hr.InValidFlag = ").AppendParameter("InValidFlag", "0"); //無效註記;
            if (string.IsNullOrEmpty(flowCompID)) //適用公司代碼
            {
                sb.Append(" AND hr.FlowCompID = ").AppendParameter("FlowCompID", "");
            }
            else
            {
                sb.Append(" AND hr.FlowCompID IN ( ").AppendParameter("FlowCompID01", flowCompID);
                sb.Append(" , ").AppendParameter("FlowCompID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(deptID)) //適用部門代碼
            {
                sb.Append(" AND hr.DeptID = ").AppendParameter("DeptID", "");
            }
            else
            {
                sb.Append(" AND hr.DeptID IN ( ").AppendParameter("DeptID01", deptID);
                sb.Append(" , ").AppendParameter("DeptID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(organID)) //適用單位代碼
            {
                sb.Append(" AND hr.OrganID = ").AppendParameter("OrganID", "");
            }
            else
            {
                sb.Append(" AND hr.OrganID IN ( ").AppendParameter("OrganID01", organID);
                sb.Append(" , ").AppendParameter("OrganID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(businessType)) //適用業務類別
            {
                sb.Append(" AND hr.BusinessType = ").AppendParameter("BusinessType", "");
            }
            else
            {
                sb.Append(" AND hr.BusinessType IN ( ").AppendParameter("BusinessType01", businessType);
                sb.Append(" , ").AppendParameter("BusinessType02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(empID)) //適用員編
            {
                sb.Append(" AND hr.EmpID = ").AppendParameter("EmpID", "");
            }
            else
            {
                sb.Append(" AND hr.EmpID IN ( ").AppendParameter("EmpID01", empID);
                sb.Append(" , ").AppendParameter("EmpID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(positionID)) //適用職位
            {
                sb.Append(" AND hr.PositionID = ").AppendParameter("PositionID", "");
            }
            else
            {
                sb.Append(" AND hr.PositionID IN ( ").AppendParameter("PositionID01", positionID);
                sb.Append(" , ").AppendParameter("PositionID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(workTypeID)) //適用工作性質
            {
                sb.Append(" AND hr.WorkTypeID = ").AppendParameter("WorkTypeID", "");
            }
            else
            {
                sb.Append(" AND hr.WorkTypeID IN ( ").AppendParameter("WorkTypeID01", workTypeID);
                sb.Append(" , ").AppendParameter("WorkTypeID02", "");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(empFlowRemark)) //適用功能備註
            {
                sb.Append(" AND hr.EmpFlowRemark = ").AppendParameter("EmpFlowRemark", "");
            }
            else
            {
                sb.Append(" AND hr.EmpFlowRemark IN ( ").AppendParameter("EmpFlowRemark01", empFlowRemark);
                sb.Append(" , ").AppendParameter("EmpFlowRemark02", "");
                sb.Append(" ) ");
            }
            var rankIDMap = Aattendant.GetRankIDFormMapping(flowCompID, rankID);
            if (string.IsNullOrEmpty(rankIDMap)) //適用職等
            {
                sb.Append(" AND ( ");
                sb.Append(" rmT.RankIDMap IS NULL ");
                sb.Append(" AND rmB.RankIDMap IS NULL ");
                sb.Append(" ) ");
            }
            else
            {
                sb.Append(" AND ( ");
                sb.Append(" ( ");
                sb.Append(" rmT.RankIDMap IS NULL ");
                sb.Append(" AND rmB.RankIDMap IS NULL ");
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" rmT.RankIDMap >= ").AppendParameter("RankIDTop02", rankIDMap);
                sb.Append(" AND rmB.RankIDMap IS NULL ");
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" rmT.RankIDMap >= ").AppendParameter("RankIDTop03", rankIDMap);
                sb.Append(" AND rmB.RankIDMap <= ").AppendParameter("RankIDBottom03", rankIDMap);
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" rmT.RankIDMap IS NULL ");
                sb.Append(" AND rmB.RankIDMap <= ").AppendParameter("RankIDBottom04", rankIDMap);
                sb.Append(" ) ");
                sb.Append(" ) ");
            }
            if (string.IsNullOrEmpty(titleID)) //適用職稱
            {
                sb.Append(" AND ( ");
                sb.Append(" hr.TitleIDTop = ").AppendParameter("TitleIDTop", "");
                sb.Append(" AND hr.TitleIDBottom = ").AppendParameter("TitleIDBottom", "");
                sb.Append(" ) ");

            }
            else
            {
                sb.Append(" AND ( ");
                sb.Append(" ( ");
                sb.Append(" hr.TitleIDTop = ").AppendParameter("TitleIDTop01", "");
                sb.Append(" AND hr.TitleIDBottom = ").AppendParameter("TitleIDBottom01", "");
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" hr.TitleIDTop >= ").AppendParameter("TitleIDTop02", titleID);
                sb.Append(" AND hr.TitleIDBottom = ").AppendParameter("TitleIDBottom02", "");
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" hr.TitleIDTop >= ").AppendParameter("TitleIDTop03", titleID);
                sb.Append(" AND hr.TitleIDBottom <= ").AppendParameter("TitleIDBottom03", titleID);
                sb.Append(" ) ");
                sb.Append(" OR ");
                sb.Append(" ( ");
                sb.Append(" hr.TitleIDTop = ").AppendParameter("TitleIDTop04", "");
                sb.Append(" AND hr.TitleIDBottom <= ").AppendParameter("TitleIDBottom04", titleID);
                sb.Append(" ) ");
                sb.Append(" ) ");
            }
            sb.Append(" ORDER BY hr.CompID, hr.SystemID, hr.FlowCode, hr.Seq, hr.FlowSN ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            isSuccess = true;
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            returnTable = ds.Tables[0];

        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢HR流程引擎檔
    /// </summary>
    /// <param name="compID">公司代碼</param>
    /// <param name="systemID">系統別 : OT=>加班</param>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="flowSN">流程識別碼</param>
    /// <param name="returnTable">return: returnTable</param>
    /// <param name="message">return: message</param>
    /// <param name="speComp">特定人員公司代碼:預設空值</param>
    /// <param name="speEmpID">特定人員編號:預設空值</param>
    /// <returns>bool</returns>
    public static bool QueryHRFlowEngineDatas(string compID, string systemID, string flowCode, string flowSN, out DataTable returnTable, out string message, string speComp = "", string speEmpID = "")
    {
        var isSuccess = false;
        returnTable = new DataTable();
        message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" CompID, SystemID, FlowCode, FlowSN, FlowSeq "); //PK
            sb.Append(" , FlowName, FlowSeqName, FlowStartFlag, FlowEndFlag, InValidFlag, FlowAct, SignLineDefine, SingIDDefine, SpeComp, SpeEmpID, VisableFlag, Status ");
            sb.Append(" FROM HRFlowEngine ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND InValidFlag = ").AppendParameter("InValidFlag", "0"); //無效註記;
            sb.Append(" AND VisableFlag = ").AppendParameter("VisableFlag", "0"); //隱藏註記;
            sb.Append(" AND Status = ").AppendParameter("Status", "1"); //生效;
            sb.Append(" AND CompID = ").AppendParameter("CompID", compID); //公司代碼
            sb.Append(" AND SystemID = ").AppendParameter("SystemID", systemID); //系統別 : OT=>加班
            sb.Append(" AND FlowCode = ").AppendParameter("FlowCode", flowCode); //流程代碼
            sb.Append(" AND FlowSN = ").AppendParameter("FlowSN", flowSN); //流程識別碼
            if (!string.IsNullOrEmpty(speComp))
            {
                sb.Append(" AND SpeComp = ").AppendParameter("SpeComp", speComp); //特定人員公司代碼
            }
            if (!string.IsNullOrEmpty(speEmpID))
            {
                sb.Append(" AND SpeEmpID = ").AppendParameter("SpeEmpID", speEmpID); //特定人員編號
            }
            sb.Append(" ORDER BY CompID, SystemID, FlowCode, FlowSN, FlowSeq ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            isSuccess = true;
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            returnTable = ds.Tables[0];
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    /// <summary>
    /// 查詢流程使用狀態
    /// </summary>
    /// <param name="compID">公司代碼</param>
    /// <param name="flowCaseID">flowCaseID</param>
    /// <param name="flowCode">流程代碼</param>
    /// <param name="flowSN">流程識別碼</param>
    /// <param name="flowFlag">0:未使用 1:使用中</param>
    /// <returns>bool</returns>
    private static bool hadHROverTimeMainData(string compID, string flowCaseID, string flowCode, string flowSN)
    {
        var isSuccess = false;
        var message = "";
        try
        {
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" CompID, FlowCaseID, FlowCode, FlowSN, FlowFlag ");
            sb.Append(" FROM HROverTimeMain ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND CompID = ").AppendParameter("CompID", compID);
            sb.Append(" AND FlowCaseID = ").AppendParameter("FlowCaseID", flowCaseID);
            sb.Append(" AND FlowCode = ").AppendParameter("FlowCode", flowCode);
            sb.Append(" AND FlowSN = ").AppendParameter("FlowSN", flowSN);
            sb.Append(" ORDER BY CompID, FlowCaseID, FlowCode, FlowSN ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());

            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            isSuccess = true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
        }
        return isSuccess;
    }

    #endregion "DB"
}