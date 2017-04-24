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
public class Aattendant : System.Web.SessionState.IRequiresSessionState
{
    public static string _flag = "false";
    public static string _AattendantDBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    public static string _AattendantFlowID = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    public static string _eHRMSDB_ITRD = Util.getAppSetting("app://eHRMSDB_OverTime/");
    //public static DbHelper db = new DbHelper(_AattendantDBName);
    
    public Aattendant(){}

    /// <summary>
    /// GetRankIDFormMapping
    /// </summary>
    /// <param name="compID">string</param>
    /// <param name="rankID">string</param>
    /// <returns>string</returns>
    public static string GetRankIDFormMapping(string compID, string rankID)
    {
        var result = rankID;
        var msg = "";
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["Common_RankIDMappingDatas"] == null)
            {
                dt = GetRankIDMappingData();
            }
            else
            {
                dt = HttpContext.Current.Session["Common_RankIDMappingDatas"] as DataTable;
            }
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Session["Common_RankIDMappingDatas"] = dt;
                var thisRow = dt.Rows.Cast<DataRow>().Where(row => row.Field<string>("CompID") == compID && row.Field<string>("RankID") == rankID).FirstOrDefault();
                if (thisRow != null && thisRow["RankIDMap"] != null)
                {
                    result = thisRow["RankIDMap"] as string;
                }                
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// GetRankIDMappingData
    /// </summary>
    /// <returns></returns>
    private static DataTable GetRankIDMappingData()
    {
        DataTable result = new DataTable();
        try
        {
            DbHelper db = new DbHelper("eHRMSDB");
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" CompID, RankID, RankIDMap ");
            sb.Append(" FROM RankMapping ");
            //sb.Append(" WHERE CompID = ").AppendParameter("CompID", compID);
            //sb.Append(" AND RankID = ").AppendParameter("RankID", rankID);
            sb.Append(" ; ");
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            result = ds.Tables[0];
        }
        catch (Exception)
        {
            throw;
        }
        return result;
    }

    /// <summary>
    /// 判斷是否為國定假日
    /// </summary>
    /// <param name="checkDay">判斷的日期(yyyy/MM/dd)</param>
    /// <returns>bool</returns>
    public static bool IsNationalHoliday(string checkDay)
    {
        bool result = false;
        var msg = "";
        try
        {
            DbHelper db = new DbHelper(_AattendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement(" SELECT ");
            sb.Append(" TabName, FldName, Code, CodeCName, SortFld, NotShowFlag ");
            sb.Append(" FROM AT_CodeMap ");
            sb.Append(" WHERE 0 = 0 ");
            sb.Append(" AND TabName = ").AppendParameter("TabName", "NationalHolidayDefine");
            sb.Append(" AND FldName = ").AppendParameter("FldName", "HolidayDate");
            sb.Append(" AND NotShowFlag = ").AppendParameter("NotShowFlag", "0");
            sb.Append(" AND Code = ").AppendParameter("Code", checkDay);
            var ds = db.ExecuteDataSet(sb.BuildCommand());
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("查無資料!");
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 判斷是否為國定假日
    /// </summary>
    /// <param name="checkDay">判斷的日期(DateTime)</param>
    /// <returns>bool</returns>
    public static bool IsNationalHoliday(DateTime checkDay)
    {
        bool result = false;
        try
        {
            result = IsNationalHoliday(checkDay.ToString("yyyy/MM/dd"));
        }
        catch (Exception ex)
        {
            throw;
        }
        return result;
    }

    /// <summary>
    /// 暫存Json的Model
    /// </summary>
    public class JsonOutputModel
    {
        public string name { get; set; }
        public string[] results { get; set; }
    }

    /// <summary>
    /// Json字串轉DataTable
    /// </summary>
    /// <param name="jsonStr">json格式的字串</param>
    /// <returns></returns>
    public DataTable Json2DataTable(string jsonStr)
    {
        var result = new DataTable();
        result = JsonConvert.DeserializeObject<DataTable>(jsonStr);
        return result;
    }
    /// <summary>
    /// 判斷迄日是否需要扣除不足的用餐時間
    /// </summary>
    /// <param name="cntStart">起日TotalTime(分鐘)</param>
    /// <param name="MealTime">用餐時間(分鐘)</param>
    /// <returns></returns>
    public string MealJudge(double cntStart, double MealTime)
    {
        string Result = "";
        string StartDayMealFlag = "";
        string StartDayMealTime = "";
        string EndDayMealFlag = "";
        string EndDayMealTime = "";
        if (MealTime == 0)
        {
            StartDayMealFlag = "0";
            StartDayMealTime = "0";
            EndDayMealFlag = "0";
            EndDayMealTime = "0";
        }
        else if ((cntStart - MealTime) < 0)
        {
            StartDayMealFlag = "1";
            StartDayMealTime = cntStart.ToString();
            EndDayMealFlag = "1";
            EndDayMealTime = (MealTime - cntStart).ToString();
        }
        else
        {
            StartDayMealFlag = "1";
            StartDayMealTime = MealTime.ToString();
            EndDayMealFlag = "1";
            EndDayMealTime = "0";
        }

        Result = StartDayMealFlag + "," + StartDayMealTime + "," + EndDayMealFlag + "," + EndDayMealTime;
        return Result;
    }

    /// <summary>
    /// 時段計算(單日、跨日共用)
    /// </summary>
    /// <param name="table">資料表名稱</param>
    /// <param name="strOTEmpID">加班人員工編號</param>
    /// <param name="cntStart">起日TotalTime</param>
    /// <param name="cntEnd">迄日TotalTime</param>
    /// <param name="StartbeginTime">起日開始加班時間</param>
    /// <param name="StartendTime">起日結束加班時間</param>
    /// <param name="StartDate">起日日期</param>
    /// <param name="EndbeginTime">迄日開始加班時間</param>
    /// <param name="EndendTime">迄日結束加班時間</param>
    /// <param name="EndDate">迄日日期</param>
    /// <param name="MealTime">用餐時間</param>
    /// <param name="MealFlag">用餐註記</param>
    /// <param name="ottxnid">單號</param>
    /// <param name="reMsg">回傳訊息</param>
    /// <returns>若回傳為true，則顯示加班時段，否則顯示錯誤訊息</returns>
    public bool PeriodCount(string table, string strOTEmpID, double cntStart, double cntEnd, int StartbeginTime, int StartendTime, string StartDate, int EndbeginTime, int EndendTime, string EndDate, double MealTime, string MealFlag, string ottxnid, out string reMsg)//跨日的時段計算
    {
        bool ExecResult = true;
        string Result = "";
        //TimePeriodBegin
        double StartDatePeriod1 = 0.0;
        double StartDatePeriod2 = 0.0;
        double StartDatePeriod3 = 0.0;
        string DateB = StartDate;

        //TimePeriodEnd
        double EndDatePeriod1 = 0.0;
        double EndDatePeriod2 = 0.0;
        double EndDatePeriod3 = 0.0;
        string DateE = StartDate;

        if ("1900/01/01".Equals(EndDate))
        {
            //TimePeriodEnd
            EndDatePeriod1 = 0.0;
            EndDatePeriod2 = 0.0;
            EndDatePeriod3 = 0.0;
            DateE = StartDate;
            cntEnd = cntStart;
        }
        else
        {
            //TimePeriodBegin
            StartDatePeriod1 = 0.0;
            StartDatePeriod2 = 0.0;
            StartDatePeriod3 = 0.0;
            DateB = StartDate;

            //TimePeriodEnd
            EndDatePeriod1 = 0.0;
            EndDatePeriod2 = 0.0;
            EndDatePeriod3 = 0.0;
            DateE = EndDate;
        }

        double LastPeriod1 = 0.0;
        double LastPeriod2 = 0.0;
        double LastPeriod3 = 0.0;
        string LastDate = "";
        string LastTxnID = "";

        string StartDateHo = "";
        string EndDateHo = "";
        //string strOTDateStart;
        //string strOTDateEnd;

        bool FlagPeriod1 = false;
        bool FlagPeriod2 = false;
        bool FlagPeriod3 = false;

        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();

        //畫面是否為假日
        if ("1900/01/01".Equals(EndDate))
        {
            DataTable dtStartHo = QueryData("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + StartDate + "'");
            if (dtStartHo.Rows[0]["HolidayOrNot"].ToString() == "0")
            {
                StartDateHo = "0";
            }
            else
            {
                StartDateHo = "1";
            }
        }
        else
        {
            DataTable dtStartHo = QueryData("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + StartDate + "'");
            if (dtStartHo.Rows[0]["HolidayOrNot"].ToString() == "0")
            {
                StartDateHo = "0";
            }
            else
            {
                StartDateHo = "1";
            }
            DataTable dtEndHo = QueryData("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + EndDate + "'");
            if (dtEndHo.Rows[0]["HolidayOrNot"].ToString() == "0")
            {
                EndDateHo = "0";
            }
            else
            {
                EndDateHo = "1";
            }
        }
        //計算吃飯時間
        if ("1900/01/01".Equals(EndDate))
        {
            if (MealFlag == "1") //如果有要扣吃飯時間
            {
                cntStart = cntStart - MealTime;
            }
            //cntStart = (Math.Round(cntStart / 6, MidpointRounding.AwayFromZero)) / 10; //轉小時
        }
        else
        {
            if (MealFlag == "1") //如果有要扣吃飯時間
            {
                cntStart = cntStart - MealTime;
                if (cntStart < 0.0)
                {
                    cntEnd = cntEnd + (cntStart);
                    cntStart = 0.0;
                }
            }
            //cntStart = (Math.Round(cntStart / 6, MidpointRounding.AwayFromZero)) / 10; //轉小時
            //cntEnd = (Math.Round(cntEnd / 6, MidpointRounding.AwayFromZero)) / 10; //轉小時
        }

        DataTable OverTimeDatas;
        //撈出正確的資料
        if ("1900/01/01".Equals(EndDate))
        {
            OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, '" + StartDate + "')) AND OTStartDate <= '" + StartDate + "' AND OTCompID='" + UserInfo.getUserInfo().CompID + "' AND OTEmpID='" + strOTEmpID + "' AND OTStatus IN ('2', '3') AND NOT(OTTxnID='" + ottxnid + "') ORDER BY OTStartDate, OTStartTime ");

        }
        else
        {
            //OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, '" + EndDate + "')) AND OTStartDate <= '" + EndDate + "' AND OTCompID='" + UserInfo.getUserInfo().CompID + "' AND OTEmpID='" + strOTEmpID + "' AND OTStatus IN ('2', '3') AND NOT(OTTxnID='" + ottxnid + "') ORDER BY OTStartDate, OTStartTime ");
            OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, '" + StartDate + "')) AND OTStartDate <= '" + StartDate + "' AND OTCompID='" + UserInfo.getUserInfo().CompID + "' AND OTEmpID='" + strOTEmpID + "' AND OTStatus IN ('2', '3') AND NOT(OTTxnID='" + ottxnid + "') ORDER BY OTStartDate, OTStartTime ");
        }
        //DataTable OverTimeDatas = QueryData("OTStartDate,OTEndDate,OTTxnID,OTStartTime,OTEndTime,OTTotalTime,MealFlag,MealTime,HolidayOrNot", table, "AND MONTH(CONVERT(DATETIME, OTStartDate))=MONTH(CONVERT(DATETIME, '" + EndDate + "')) AND OTStartDate <= '" + EndDate + "' AND OTCompID='" + UserInfo.getUserInfo().CompID + "' AND OTEmpID='" + strOTEmpID + "' AND OTStatus IN ('2', '3') AND NOT(OTTxnID='" + ottxnid + "') ORDER BY OTStartDate, OTStartTime ");

        if ("1900/01/01".Equals(EndDate))
        {
            DataRow EndDateViewData = OverTimeDatas.NewRow();
            EndDateViewData["OTStartDate"] = StartDate;
            EndDateViewData["OTEndDate"] = StartDate;
            EndDateViewData["OTTxnID"] = "ViewData";
            EndDateViewData["OTStartTime"] = StartbeginTime.ToString("0000");
            EndDateViewData["OTEndTime"] = StartendTime.ToString("0000");
            EndDateViewData["OTTotalTime"] = cntStart;
            EndDateViewData["MealFlag"] = "0";//MealFlag;
            EndDateViewData["MealTime"] = 0;//MealTime;
            EndDateViewData["HolidayOrNot"] = StartDateHo;
            OverTimeDatas.Rows.Add(EndDateViewData);
        }
        else
        {
            DataRow StartDateViewData = OverTimeDatas.NewRow();
            StartDateViewData["OTStartDate"] = StartDate;
            StartDateViewData["OTEndDate"] = StartDate;
            StartDateViewData["OTTxnID"] = "ViewData";
            StartDateViewData["OTStartTime"] = StartbeginTime.ToString("0000");
            StartDateViewData["OTEndTime"] = StartendTime.ToString("0000");
            StartDateViewData["OTTotalTime"] = cntStart;
            StartDateViewData["MealFlag"] = "0";//MealFlag;
            StartDateViewData["MealTime"] = 0;//MealTime;
            StartDateViewData["HolidayOrNot"] = StartDateHo;
            OverTimeDatas.Rows.Add(StartDateViewData);

            DataRow EndDateViewData = OverTimeDatas.NewRow();
            EndDateViewData["OTStartDate"] = EndDate;
            EndDateViewData["OTEndDate"] = EndDate;
            EndDateViewData["OTTxnID"] = "ViewData";
            EndDateViewData["OTStartTime"] = EndbeginTime.ToString("0000");
            EndDateViewData["OTEndTime"] = EndendTime.ToString("0000");
            EndDateViewData["OTTotalTime"] = cntEnd;
            EndDateViewData["MealFlag"] = "0";
            EndDateViewData["MealTime"] = 0;
            EndDateViewData["HolidayOrNot"] = EndDateHo;
            OverTimeDatas.Rows.Add(EndDateViewData);
        }

        // OverTimeDatas= OverTimeDatas.Sort("OTStartDate", "OTStartTime");
        OverTimeDatas.DefaultView.Sort = "OTStartDate asc, OTStartTime asc";
        OverTimeDatas = OverTimeDatas.DefaultView.ToTable();
        double LastRowTotalTime = 0;
        double LastRowMinTime = 0;
        double SameDayTotalMinTime = 0;
        for (int i = 0; i < OverTimeDatas.Rows.Count; i++)
        {
            double Period1 = 0;
            double Period2 = 0;
            double Period3 = 0;
            double RowTotalMinTime = Convert.ToDouble(OverTimeDatas.Rows[i]["OTTotalTime"].ToString());// * 60; //轉分鐘
            //double RowTotalMinTime = 0;
            double RowTotalTime = 0;


            string RowTxnID = OverTimeDatas.Rows[i]["OTTxnID"].ToString(); //單號
            string RowDate = OverTimeDatas.Rows[i]["OTStartDate"].ToString();
            string HolidayOrNot = OverTimeDatas.Rows[i]["HolidayOrNot"].ToString();
            string RowMealFlag = OverTimeDatas.Rows[i]["MealFlag"].ToString();
            double RowMealTime = Convert.ToDouble(OverTimeDatas.Rows[i]["MealTime"].ToString());
            if (RowMealFlag == "1") //如果有要扣吃飯時間
            {
                RowTotalMinTime = RowTotalMinTime - RowMealTime;
            }
            RowTotalTime = (Math.Round(RowTotalMinTime / 6, MidpointRounding.AwayFromZero)) / 10; //轉小時


            if (LastTxnID != RowTxnID) //單號不同
            {
                if (RowDate == DateB)
                {
                    if (HolidayOrNot == "0") //起日是平日
                    {
                        if (LastDate != DateB) //從頭算(重新來過)
                        {
                            SameDayTotalMinTime = RowTotalMinTime;
                            if (RowTotalTime > 4)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else if (LastDate == DateB) //繼續算(繼續算下一筆)
                        {
                            //if ((LastPeriod1 + LastPeriod2 + RowTotalTime) > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else if ((LastPeriod1 + LastPeriod2 + RowTotalTime) <= 4 && (LastPeriod1 + LastPeriod2 + RowTotalTime) > 2)
                            //{
                            //    Period1 = 2;
                            //    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                            //    FlagPeriod1 = false;
                            //    FlagPeriod2 = true;
                            //}
                            //else if ((LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2)
                            //{
                            //    Period1 = (LastPeriod1 + RowTotalTime);
                            //    Period2 = LastPeriod2;
                            //    if (Period1 < 2)
                            //    {
                            //        FlagPeriod1 = true;
                            //        FlagPeriod2 = false;
                            //    }
                            //    else
                            //    {
                            //        FlagPeriod1 = false;
                            //        FlagPeriod2 = true;
                            //    }
                            //}
                            //else 
                            if ((SameDayTotalMinTime + RowTotalMinTime) > 240)
                            {
                                Period1 = 2;
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 240 && (SameDayTotalMinTime + RowTotalMinTime) > 120)
                            {
                                if (LastPeriod1 + RowTotalTime < 2)
                                {
                                    Period1 = LastPeriod1 + RowTotalTime;
                                    Period2 = LastPeriod2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    Period1 = 2;
                                    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 120)
                            {
                                Period1 = (LastPeriod1 + RowTotalTime);//LastPeriod1 + LastPeriod2 + RowTotalTime
                                if (Period1 < 2)
                                {
                                    Period2 = LastPeriod2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }

                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        StartDatePeriod1 = LastPeriod1;
                        StartDatePeriod2 = LastPeriod2;
                        StartDatePeriod3 = LastPeriod3;

                    }
                    else if (HolidayOrNot == "1")//起日是假日
                    {
                        if (LastDate != DateB) //從頭算(重新來過)
                        {
                            //if (RowTotalTime <= 12)
                            //{
                                Period3 = RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        else if (LastDate == DateB)//繼續算(繼續算下一筆)
                        {
                            //if (LastPeriod3 + RowTotalTime > 12)
                            //{
                            //    Result = "累積時數超過12小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            //if (LastPeriod3 + RowTotalTime <= 12)
                            //{
                                Period3 = LastPeriod3 + RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        StartDatePeriod1 = LastPeriod1;
                        StartDatePeriod2 = LastPeriod2;
                        StartDatePeriod3 = LastPeriod3;
                    }
                }
                else if (LastDate == DateE)
                {
                    SameDayTotalMinTime = LastRowMinTime + RowTotalMinTime;
                    if (HolidayOrNot == "0") //迄日是平日
                    {
                        if (LastDate != DateE) //從頭算(重新來過)
                        {
                            //if (RowTotalTime > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else if (LastDate == DateE)//繼續算(繼續算下一筆)
                        {
                            //if ((LastPeriod1 + LastPeriod2 + RowTotalTime) > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            if ((SameDayTotalMinTime + RowTotalMinTime) > 240)
                            {
                                Period1 = 2;
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 240 && (SameDayTotalMinTime + RowTotalMinTime) > 120)
                            {
                                if (LastPeriod1 + RowTotalTime < 2)
                                {
                                    Period1 = LastPeriod1 + RowTotalTime;
                                    Period2 = LastPeriod2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    Period1 = 2;
                                    Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 120)
                            {
                                Period1 = (LastPeriod1 + RowTotalTime);//LastPeriod1 + LastPeriod2 + RowTotalTime
                                if (Period1 < 2)
                                {
                                    Period2 = LastPeriod2;
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        EndDatePeriod1 = LastPeriod1;
                        EndDatePeriod2 = LastPeriod2;
                        EndDatePeriod3 = LastPeriod3;
                    }
                    else if (HolidayOrNot == "1")//迄日是假日
                    {
                        if (LastDate != DateE) //從頭算(重新來過)
                        {
                            //if (RowTotalTime <= 12)
                            //{
                                Period3 = RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        else if (LastDate == DateE)//繼續算(繼續算下一筆)
                        {
                            //if (LastPeriod3 + RowTotalTime > 12)
                            //{
                            //    Result = "累積時數超過12小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            //if (LastPeriod3 + RowTotalTime <= 12)
                            //{
                                Period3 = LastPeriod3 + RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        EndDatePeriod1 = LastPeriod1;
                        EndDatePeriod2 = LastPeriod2;
                        EndDatePeriod3 = LastPeriod3;
                    }
                }
                else if (RowDate != DateB && RowDate != DateE)//一般情況
                {
                    if (HolidayOrNot == "0") //平日
                    {
                        if (LastDate != RowDate) //從頭算(重新來過)
                        {
                            //if (RowTotalTime > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else if (LastDate == RowDate) //繼續算(繼續算下一筆)
                        {
                            //if ((LastPeriod1 + LastPeriod2 + RowTotalTime) > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            if ((SameDayTotalMinTime + RowTotalMinTime) > 240)
                            {
                                Period1 = 2;
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 240 && (SameDayTotalMinTime + RowTotalMinTime) > 120)
                            {
                                Period1 = 2;
                                Period2 = (LastPeriod1 + LastPeriod2 + RowTotalTime) - 2;
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            //    else if ((LastPeriod1 + LastPeriod2 + RowTotalTime) <= 2)
                            //    {
                            //        //Period1 = (LastPeriod1 + LastPeriod2 + RowTotalTime);
                            //        //if (Period1 < 2)
                            //        //{
                            //        //    FlagPeriod1 = true;
                            //        //    FlagPeriod2 = false;
                            //        //}
                            //        //else
                            //        //{
                            //        //    FlagPeriod1 = false;
                            //        //    FlagPeriod2 = true;
                            //        //}
                            //        Period1 = (LastPeriod1 + RowTotalTime);//LastPeriod1 + LastPeriod2 + RowTotalTime
                            //        if (Period1 < 2)
                            //        {
                            //            Period2 = LastPeriod2;
                            //            FlagPeriod1 = true;
                            //            FlagPeriod2 = false;
                            //        }
                            //        else
                            //        {
                            //            FlagPeriod1 = false;
                            //            FlagPeriod2 = true;
                            //        }
                            //    }
                            //}
                            else if ((SameDayTotalMinTime + RowTotalMinTime) <= 120)
                            {
                                Period1 = (LastPeriod1 + RowTotalTime);
                                Period2 = LastPeriod2;
                                SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    SameDayTotalMinTime = SameDayTotalMinTime + RowTotalMinTime;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                    }
                    else if (HolidayOrNot == "1")//假日
                    {
                        if (LastDate != RowDate) //從頭算(重新來過)
                        {
                            //if (RowTotalTime <= 12)
                            //{
                                Period3 = RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        else if (LastDate == RowDate)//繼續算(繼續算下一筆)
                        {
                            //if (LastPeriod3 + RowTotalTime > 12)
                            //{
                            //    Result = "累積時數超過12小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            //if (LastPeriod3 + RowTotalTime <= 12)
                            //{
                                Period3 = LastPeriod3 + RowTotalTime;
                                FlagPeriod3 = true;
                            //}
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastRowMinTime = RowTotalMinTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                    }
                }
            }
            else if (LastTxnID == RowTxnID) //跨日
            {
                //20170303 add by John 跨日時段處理
                double SumMinTime = LastRowMinTime + RowTotalMinTime;   //計算當筆加班單總"分鐘"數
                double SumHrTime = (Math.Round(SumMinTime / 6, MidpointRounding.AwayFromZero)) / 10;   //計算當筆加班單總"小時"數
                RowTotalTime = SumHrTime - LastRowTotalTime;    //第二天的時數 = 總時數 - 第一天的時數
                if (LastPeriod1 >= 2 && LastPeriod2 >= 2)
                {
                    FlagPeriod1 = false;
                    FlagPeriod2 = true;
                }
                if (RowDate == DateB)
                {
                    if (HolidayOrNot == "0") //起日是平日
                    {
                        //if (FlagPeriod1 == true) //從時段一開始計算
                        //{
                        //    if (RowTotalTime > 4)
                        //    {
                        //        Result = "累積時數超過4小時!";
                        //        ExecResult = false;
                        //    }
                        //    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //    {
                        //        Period1 = 2;
                        //        Period2 = RowTotalTime - 2;
                        //    }
                        //    else if (RowTotalTime <= 2)
                        //    {
                        //        Period1 = RowTotalTime;
                        //    }
                        //}
                        //else if (FlagPeriod2 == true) //從時段二開始計算
                        //{
                        //    if (RowTotalTime > 4)
                        //    {
                        //        Result = "累積時數超過4小時!";
                        //        ExecResult = false;
                        //    }
                        //    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //    {
                        //        Period2 = 2;
                        //        Period1 = RowTotalTime - 2;
                        //    }
                        //    else if (RowTotalTime <= 2)
                        //    {
                        //        Period2 = RowTotalTime;
                        //    }
                        //}
                        //else if (FlagPeriod3 == true) //從時段三開始計算
                        //{
                        //    if (LastPeriod3 >= 2) //前筆的時段三大於2從時段二補
                        //    {
                        //        if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //        {
                        //            Period2 = 2;
                        //            Period1 = RowTotalTime - 2;
                        //        }
                        //        else if (RowTotalTime <= 2)
                        //        {
                        //            Period2 = RowTotalTime;
                        //        }
                        //    }
                        //    else if (LastPeriod3 < 2) //前筆的時段三小於2從時段一補
                        //    {
                        //        if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //        {
                        //            Period1 = 2 - LastPeriod3;
                        //            Period2 = (LastPeriod3 + RowTotalTime) - 2;
                        //        }
                        //        else if (RowTotalTime <= 2)
                        //        {
                        //            if (RowTotalTime > (2 - LastPeriod3))
                        //            {
                        //                Period1 = 2 - LastPeriod3;
                        //                Period2 = (LastPeriod3 + RowTotalTime) - 2;
                        //            }
                        //            else
                        //            {
                        //                Period1 = RowTotalTime;
                        //            }
                        //        }
                        //    }
                        //}
                        if (LastRowTotalTime == 0)//上一筆加班時數等於0
                        {
                            //if (RowTotalTime > 4)
                            //{
                            //    Result = "累積時數超過4小時!";
                            //    ExecResult = false;
                            //}
                            //else 
                            if (RowTotalTime > 4)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else
                        {
                            if (FlagPeriod1 == true) //從時段一開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    if (RowTotalTime + LastPeriod1 > 6)
                                    {
                                        Period1 = 2;
                                        Period2 = RowTotalTime - 2;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = RowTotalTime;
                                    }
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 6 && RowTotalTime + LastPeriod1 > 4)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                        if (Period2 <= 4 && Period2 > 2)
                                        {
                                            Period2 = 2;
                                            Period1 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = RowTotalTime;
                                    }
                                }
                            }
                            else if (FlagPeriod2 == true) //從時段二開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    Period1 = 2;
                                    Period2 = RowTotalTime - 2;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod2 <= 6 && RowTotalTime + LastPeriod2 > 4)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                        if (Period1 <= 4 && Period1 > 2)
                                        {
                                            Period1 = 2;
                                            Period2 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                        //Period1 = RowTotalTime - Period2;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    Period2 = RowTotalTime;
                                    //if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    //{
                                    //    Period2 = 2 - LastPeriod2;
                                    //    Period1 = RowTotalTime - Period2;
                                    //}
                                    //else if (RowTotalTime + LastPeriod2 <= 2)
                                    //{
                                    //    Period2 = RowTotalTime;
                                    //}
                                }
                            }
                            else if (FlagPeriod3 == true) //從時段三開始計算
                            {
                                if (LastPeriod3 >= 2) //前筆的時段三大於2從時段二補
                                {
                                    if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period2 = 2;
                                        Period1 = RowTotalTime - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                    }
                                }
                                else if (LastPeriod3 < 2) //前筆的時段三小於2從時段一補
                                {
                                    if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period1 = 2 - LastPeriod3;
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        if (RowTotalTime > (2 - LastPeriod3))
                                        {
                                            Period1 = 2 - LastPeriod3;
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                        }
                                        else
                                        {
                                            Period1 = RowTotalTime;
                                        }
                                    }
                                }
                            }
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        StartDatePeriod1 = LastPeriod1;
                        StartDatePeriod2 = LastPeriod2;
                        StartDatePeriod3 = LastPeriod3;
                    }
                    else if (HolidayOrNot == "1") //起日是假日
                    {
                        Period3 = RowTotalTime;
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        StartDatePeriod1 = LastPeriod1;
                        StartDatePeriod2 = LastPeriod2;
                        StartDatePeriod3 = LastPeriod3;
                    }
                }
                else if (RowDate == DateE) //迄日
                {
                    if (HolidayOrNot == "0") //迄日是平日
                    {
                        if (LastRowTotalTime == 0)//上一筆加班時數等於0
                        {
                            if (RowTotalTime > 4)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else
                        {
                            if (FlagPeriod1 == true) //從時段一開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    Period1 = 2;
                                    Period2 = RowTotalTime - 2;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod1 > 6)
                                    {
                                        Period1 = 2;
                                        Period2 = RowTotalTime - 2;
                                        FlagPeriod1 = false;
                                        FlagPeriod2 = true;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 6 && RowTotalTime + LastPeriod1 > 4)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                        if (Period2 <= 4 && Period2 > 2)
                                        {
                                            Period2 = 2;
                                            Period1 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = RowTotalTime;
                                    }
                                }
                            }
                            else if (FlagPeriod2 == true) //從時段二開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    Period1 = 2;
                                    Period2 = RowTotalTime - 2;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod2 > 6)
                                    {
                                        Period1 = RowTotalTime - 2;
                                        Period2 = 2;
                                        FlagPeriod1 = false;
                                        FlagPeriod2 = true;
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 6 && RowTotalTime + LastPeriod2 > 4)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                        if (Period1 <= 4 && Period1 > 2)
                                        {
                                            Period1 = 2;
                                            Period2 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                        //Period1 = RowTotalTime - Period2;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    Period2 = RowTotalTime;
                                    //if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    //{
                                    //    Period2 = 2 - LastPeriod2;
                                    //    Period1 = RowTotalTime - Period2;
                                    //}
                                    //else if (RowTotalTime + LastPeriod2 <= 2)
                                    //{
                                    //    Period2 = RowTotalTime;
                                    //}
                                }
                            }
                            else if (FlagPeriod3 == true) //從時段三開始計算
                            {
                                if (LastPeriod3 >= 2) //前筆的時段三大於2從時段二補
                                {
                                    if (RowTotalTime > 4)
                                    {
                                        Period1 = 2;
                                        Period2 = RowTotalTime - 2;
                                        FlagPeriod1 = false;
                                        FlagPeriod2 = true;
                                    }
                                    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period2 = 2;
                                        Period1 = RowTotalTime - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                    }
                                }
                                else if (LastPeriod3 < 2) //前筆的時段三小於2從時段一補
                                {
                                    if (RowTotalTime > 4)
                                    {
                                        Period1 = 2;
                                        Period2 = RowTotalTime - 2;
                                        FlagPeriod1 = false;
                                        FlagPeriod2 = true;
                                    }
                                    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period1 = 2 - LastPeriod3;
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        if (RowTotalTime > (2 - LastPeriod3))
                                        {
                                            Period1 = 2 - LastPeriod3;
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                        }
                                        else
                                        {
                                            Period1 = RowTotalTime;
                                        }
                                    }
                                }
                            }
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        EndDatePeriod1 = LastPeriod1;
                        EndDatePeriod2 = LastPeriod2;
                        EndDatePeriod3 = LastPeriod3;
                    }
                    else if (HolidayOrNot == "1") //迄日是假日
                    {
                        Period3 = RowTotalTime;
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                        EndDatePeriod1 = LastPeriod1;
                        EndDatePeriod2 = LastPeriod2;
                        EndDatePeriod3 = LastPeriod3;
                    }
                }
                else if (RowDate != DateB && RowDate != DateE)//一般情況
                {
                    if (HolidayOrNot == "0") //平日
                    {
                        //if (FlagPeriod1 == true) //從時段一開始計算
                        //{
                        //    if (RowTotalTime > 4)
                        //    {
                        //        Result = "累積時數超過4小時!";
                        //        ExecResult = false;
                        //    }
                        //    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //    {
                        //        Period1 = 2;
                        //        Period2 = RowTotalTime - 2;
                        //    }
                        //    else if (RowTotalTime <= 2)
                        //    {
                        //        Period1 = RowTotalTime;
                        //    }
                        //}
                        //else if (FlagPeriod2 == true) //從時段二開始計算
                        //{
                        //    if (RowTotalTime > 4)
                        //    {
                        //        Result = "累積時數超過4小時!";
                        //        ExecResult = false;
                        //    }
                        //    else if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //    {
                        //        Period2 = 2;
                        //        Period1 = RowTotalTime - 2;
                        //    }
                        //    else if (RowTotalTime <= 2)
                        //    {
                        //        Period2 = RowTotalTime;
                        //    }
                        //}
                        //else if (FlagPeriod3 == true) //從時段三開始計算
                        //{
                        //    if (LastPeriod3 >= 2) //前筆的時段三大於2從時段二補
                        //    {
                        //        if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //        {
                        //            Period2 = 2;
                        //            Period1 = RowTotalTime - 2;
                        //        }
                        //        else if (RowTotalTime <= 2)
                        //        {
                        //            Period2 = RowTotalTime;
                        //        }
                        //    }
                        //    else if (LastPeriod3 < 2) //前筆的時段三小於2從時段一補
                        //    {
                        //        if (RowTotalTime <= 4 && RowTotalTime > 2)
                        //        {
                        //            Period1 = 2 - LastPeriod3;
                        //            Period2 = (LastPeriod3 + RowTotalTime) - 2;
                        //        }
                        //        else if (RowTotalTime <= 2)
                        //        {
                        //            if (RowTotalTime > (2 - LastPeriod3))
                        //            {
                        //                Period1 = 2 - LastPeriod3;
                        //                Period2 = (LastPeriod3 + RowTotalTime) - 2;
                        //            }
                        //            else
                        //            {
                        //                Period1 = RowTotalTime;
                        //            }
                        //        }
                        //    }
                        //}
                        if (LastRowTotalTime == 0)//上一筆加班時數等於0
                        {
                            if (RowTotalTime > 4)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 4 && RowTotalTime > 2)
                            {
                                Period1 = 2;
                                Period2 = RowTotalTime - 2;
                                FlagPeriod1 = false;
                                FlagPeriod2 = true;
                            }
                            else if (RowTotalTime <= 2)
                            {
                                Period1 = RowTotalTime;
                                if (Period1 < 2)
                                {
                                    FlagPeriod1 = true;
                                    FlagPeriod2 = false;
                                }
                                else
                                {
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                            }
                        }
                        else
                        {
                            if (FlagPeriod1 == true) //從時段一開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    Period1 = 2;
                                    Period2 = RowTotalTime - 2;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 6 && RowTotalTime + LastPeriod1 > 4)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                        if (Period2 <= 4 && Period2 > 2)
                                        {
                                            Period2 = 2;
                                            Period1 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    if (RowTotalTime + LastPeriod1 <= 4 && RowTotalTime + LastPeriod1 > 2)
                                    {
                                        Period1 = 2 - LastPeriod1;
                                        Period2 = RowTotalTime - Period1;
                                    }
                                    else if (RowTotalTime + LastPeriod1 <= 2)
                                    {
                                        Period1 = RowTotalTime;
                                    }
                                }
                            }
                            else if (FlagPeriod2 == true) //從時段二開始計算
                            {
                                if (RowTotalTime > 4)
                                {
                                    Period1 = 2;
                                    Period2 = RowTotalTime - 2;
                                    FlagPeriod1 = false;
                                    FlagPeriod2 = true;
                                }
                                else if (RowTotalTime <= 4 && RowTotalTime > 2)
                                {
                                    if (RowTotalTime + LastPeriod2 <= 6 && RowTotalTime + LastPeriod2 > 4)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                        if (Period1 <= 4 && Period1 > 2)
                                        {
                                            Period1 = 2;
                                            Period2 = RowTotalTime - Period2;
                                        }
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    {
                                        Period2 = 2;// -LastPeriod2;
                                        Period1 = RowTotalTime - Period2;
                                    }
                                    else if (RowTotalTime + LastPeriod2 <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                        //Period1 = RowTotalTime - Period2;
                                    }
                                }
                                else if (RowTotalTime <= 2)
                                {
                                    Period2 = RowTotalTime;
                                    //if (RowTotalTime + LastPeriod2 <= 4 && RowTotalTime + LastPeriod2 > 2)
                                    //{
                                    //    Period2 = 2 - LastPeriod2;
                                    //    Period1 = RowTotalTime - Period2;
                                    //}
                                    //else if (RowTotalTime + LastPeriod2 <= 2)
                                    //{
                                    //    Period2 = RowTotalTime;
                                    //}
                                }
                            }
                            else if (FlagPeriod3 == true) //從時段三開始計算
                            {
                                if (LastPeriod3 >= 2) //前筆的時段三大於2從時段二補
                                {
                                    if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period2 = 2;
                                        Period1 = RowTotalTime - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        Period2 = RowTotalTime;
                                    }
                                }
                                else if (LastPeriod3 < 2) //前筆的時段三小於2從時段一補
                                {
                                    if (RowTotalTime <= 4 && RowTotalTime > 2)
                                    {
                                        Period1 = 2 - LastPeriod3;
                                        Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                    }
                                    else if (RowTotalTime <= 2)
                                    {
                                        if (RowTotalTime > (2 - LastPeriod3))
                                        {
                                            Period1 = 2 - LastPeriod3;
                                            Period2 = (LastPeriod3 + RowTotalTime) - 2;
                                        }
                                        else
                                        {
                                            Period1 = RowTotalTime;
                                        }
                                    }
                                }
                            }
                        }
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                    }
                    else if (HolidayOrNot == "1") //假日
                    {
                        Period3 = RowTotalTime;
                        LastTxnID = RowTxnID;
                        LastDate = RowDate;
                        LastRowTotalTime = RowTotalTime;
                        LastPeriod1 = Period1;
                        LastPeriod2 = Period2;
                        LastPeriod3 = Period3;
                    }
                }
            }
        }
        string strOTDateStart = StartDate + "," + Convert.ToDouble(StartDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(StartDatePeriod3).ToString("0.0");
        string strOTDateEnd = EndDate + "," + Convert.ToDouble(EndDatePeriod1).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod2).ToString("0.0") + "," + Convert.ToDouble(EndDatePeriod3).ToString("0.0");
        if (ExecResult == true)
        {
            reMsg = strOTDateStart + ";" + strOTDateEnd;
        }
        else
        {
            reMsg = Result;
        }
        return ExecResult;
    }
    /// <summary>
    /// 判斷是否為數字
    /// </summary>
    /// <param name="strNumber">string</param>
    /// <returns>bool</returns>
    public bool IsNumeric(String strNumber)
    {
        Regex NumberPattern = new Regex("[^0-9.-]");
        return !NumberPattern.IsMatch(strNumber);
    }

    /// <summary>
    /// 日期比較
    /// </summary>
    /// <param name="strDate01">string</param>
    /// <param name="strDate02">string</param>
    /// <param name="format">日期格式目前預設"yyyy/MM/dd"</param>
    /// <returns>0:相等 1:前大於後 -1:前小於後</returns>
    public int CompareDate(string strDate01, string strDate02, string format = "yyyy/MM/dd")
    {
        var result = 0;
        var dt1 = new DateTime();
        var dt2 = new DateTime();
        if (DateTime.TryParseExact(strDate01, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt1)
            && DateTime.TryParseExact(strDate02, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt2))
        {
            result = dt1.CompareTo(dt2);
        }
        return result;
    }

    /// <summary>
    /// 查假日檔查此天是否為假日
    /// </summary>
    /// <param name="strDate">string</param>
    /// <returns>bool</returns>
    public bool CheckHolidayOrNot(string strDate)
    {
        var result = false;
        var strTable = Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ";
        var strColumn = "HolidayOrNot";
        var strWhere = " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + strDate + "'";
        DataTable dt= QueryData(strColumn, strTable, strWhere);
        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["HolidayOrNot"] != null)
        {
            if (dt.Rows[0]["HolidayOrNot"].ToString() == "1")
            {
                result = true;
            }
        }
        return result;
    }
    public Dictionary<string, string> getOrganBoss(string _EmpID) //for Add 送簽
    {
        //主管需在找上一階，直到找到不是自己
        int intLoop = 0;
        DataTable dt;
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,P.NameN,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[EmpFlow] EF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF ON  EF.OrganID =OrF.OrganID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE EF.EmpID='{0}'", _EmpID)).Tables[0];
        //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,OrF1.OrganName+'-'+P.NameN,OrF.UpOrganID FROM  " + _eHRMSDB_ITRD + ".[dbo].[EmpFlow] EF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF ON  EF.OrganID =OrF.OrganID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF1 ON  OrF1.OrganID=OrF.DeptID WHERE EF.EmpID='{0}'", _EmpID)).Tables[0];
        //if (dt.Rows.Count>0 && dt.Rows[0]["Boss"].ToString() == _EmpID  )
        //{
        //    dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT OrF.Boss,P.NameN FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
        //}
        while (dt.Rows.Count > 0 && dt.Rows[0]["Boss"].ToString() == _EmpID && intLoop <= 10)
        {
            //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,OrF1.OrganName+'-'+P.NameN,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF1 ON  OrF1.OrganID=OrF.DeptID WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,P.NameN,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            intLoop = intLoop + 1;
        }

        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }
    public DataTable getOrganHRBoss(string _CompID, string _EmpID, string _OTCompID, string _OTEmpID) //for Add 送簽
    {
        //主管需在找上一階，直到找到不是自己
        int intLoop = 0;
        DataTable dt;
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT O.Boss,P2.NameN AS oAssName,O.UpOrganID FROM  " + _eHRMSDB_ITRD + ".[dbo].[Personal] P LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Organization] O ON O.OrganID=P.OrganID AND O.CompID=P.CompID  And O.InValidFlag = '0' And O.VirtualFlag = '0' LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P2 ON  P2.CompID = O.BossCompID And P2.EmpID = O.Boss WHERE P.EmpID='{0}'", _EmpID)).Tables[0];
        //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,P.NameN AS oAssName,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[EmpFlow] EF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF ON  EF.OrganID =OrF.OrganID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss And P.CompID=OrF.BossCompID WHERE EF.EmpID='{0}'", _EmpID)).Tables[0];
        dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.BossCompID,OrF.Boss,OrF1.OrganName+'-'+P.NameN AS oAssName,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[EmpFlow] EF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF ON  EF.OrganID =OrF.OrganID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss And P.CompID=OrF.BossCompID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF1 ON  OrF1.OrganID=OrF.DeptID WHERE EF.CompID='{0}' AND EF.EmpID='{1}'", _CompID, _EmpID)).Tables[0];
        while (dt.Rows.Count > 0 && ((dt.Rows[0]["BossCompID"].ToString() == _CompID && dt.Rows[0]["Boss"].ToString() == _EmpID) || (dt.Rows[0]["BossCompID"].ToString() == _OTCompID && dt.Rows[0]["Boss"].ToString() == _OTEmpID)) && intLoop <= 10)
        {
            //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,P.NameN AS oAssName,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss And P.CompID=OrF.BossCompID WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            //dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT O.Boss,P.NameN AS oAssName,O.UpOrganID FROM  " + _eHRMSDB_ITRD + ".[dbo].[Personal] P LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Organization] O ON O.Boss=P.EmpID AND O.BossCompID=P.CompID And O.InValidFlag = '0' And O.VirtualFlag = '0' WHERE O.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.BossCompID,OrF.Boss,OrF1.OrganName+'-'+P.NameN AS oAssName,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss And P.CompID=OrF.BossCompID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF1 ON  OrF1.OrganID=OrF.DeptID WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            intLoop = intLoop + 1;
        }
        return dt;
    }


    public Dictionary<string, string> getOrganUpBoss(string _EmpID) //for OvertimeCustVerify 審核
    {
        //主管需在找上一階，直到找到不是自己
        int intLoop = 0;
        bool SelfFlag = false;  //先尋找到自己後變成true，接著往上層找到高一階的主管迴圈break
        DataTable dt;
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT OrF.Boss,P.NameN,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[EmpFlow] EF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF ON  EF.OrganID =OrF.OrganID LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE EF.EmpID='{0}'", _EmpID)).Tables[0];
        //if (dt.Rows.Count>0 && dt.Rows[0]["Boss"].ToString() == _EmpID  )
        //{
        //    dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT OrF.Boss,P.NameN FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
        //}
        while (dt.Rows.Count > 0 && intLoop <= 10)
        {
            dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT OrF.Boss,P.NameN,OrF.UpOrganID FROM " + _eHRMSDB_ITRD + ".[dbo].[OrganizationFlow] OrF LEFT JOIN " + _eHRMSDB_ITRD + ".[dbo].[Personal] P ON P.EmpID=OrF.Boss WHERE OrF.OrganID='{0}'", dt.Rows[0]["UpOrganID"])).Tables[0];
            intLoop = intLoop + 1;
            if (dt.Rows[0]["Boss"].ToString() != _EmpID && SelfFlag) break;
            if (dt.Rows[0]["Boss"].ToString() == _EmpID) SelfFlag = true;

        }

        Dictionary<string, string> _dic = new Dictionary<string, string>();
        _dic = Util.getDictionary(dt, 0, 1);
        return _dic;
    }
    public DataTable QueryData(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Append("SELECT " + strColumn + " FROM " + strTable);
        sb.Append(" WHERE 1=1 ");
        sb.Append(strWhere);
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt;
    }

    public string QueryColumn(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();

        sb.Append("SELECT " + strColumn + " FROM " + strTable);
        sb.Append(" WHERE 1=1 ");
        sb.Append(strWhere);
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0][strColumn].ToString();
        }
        else
        {
            return "";
        }
    }

    public string QueryHRColumn(string strColumn, string strTable, string strWhere) //查詢datatable
    {
        DbHelper db = new DbHelper("eHRMSDB");
        CommandHelper sb = db.CreateCommandHelper();

        sb.Append("SELECT " + strColumn + " FROM " + strTable);
        sb.Append(" WHERE 1=1 ");
        sb.Append(strWhere);
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0][strColumn].ToString();
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 表單編號(登錄者)
    /// </summary>
    /// <param name="strColumn">string</param>
    /// <param name="strEmpID">string</param>
    /// <returns>string</returns>
    public string QueryFormNO(string strColumn, string strComp, string strEmpID)//表單編號(登錄者)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string strFormNO = "";
        if (!string.IsNullOrEmpty(strEmpID))
        {
            DataTable dt = QueryData(strColumn + ", FileSeq,CONVERT(CHAR(10), LastChgDate, 111) AS LastChgDate", "OverTimeSeq", "AND CompID='" + strComp + "' AND EmpID='" + strEmpID + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LastChgDate"].ToString() != DateTime.Now.ToString("yyyy/MM/dd"))//不是今天的資料
                {
                    sb.Reset();
                    sb.Append(" UPDATE OverTimeSeq SET AdvanceFormSeq='01',DeclarationFormSeq='01',FileSeq='01',LastChgDate=GETDATE()");
                    sb.Append(" WHERE CompID='" + strComp + "'");
                    sb.Append(" AND EmpID='" + strEmpID + "'");
                    db.ExecuteNonQuery(sb.BuildCommand());
                    strFormNO = DateTime.Now.ToString("yyyyMMdd") + "01" + strEmpID;
                }
                else
                {
                    sb.Reset();
                    int FormSeq = Convert.ToInt32(dt.Rows[0]["" + strColumn + ""].ToString()) + 1;
                    sb.Append(" UPDATE OverTimeSeq SET " + strColumn + "='" + FormSeq + "',LastChgDate=GETDATE()");
                    sb.Append(" WHERE CompID='" + strComp + "'");
                    sb.Append(" AND EmpID='" + strEmpID + "'");
                    db.ExecuteNonQuery(sb.BuildCommand());
                    strFormNO = DateTime.Now.ToString("yyyyMMdd") + FormSeq.ToString("00") + strEmpID;
                }
            }
            else
            {
                sb.Reset();
                sb.Append("INSERT INTO OverTimeSeq(CompID, EmpID, AdvanceFormSeq, DeclarationFormSeq, FileSeq, LastChgDate)");
                sb.Append(" VALUES('" + strComp + "', '" + strEmpID + "', '1' ,'1', '', GETDATE())");
                db.ExecuteNonQuery(sb.BuildCommand());
                strFormNO = DateTime.Now.ToString("yyyyMMdd") + "01" + strEmpID;
            }
        }
        return strFormNO;
    }

    public static string GetNumString(string data, int num)
    {
        string snum = "0";
        if (num > 0)
        {
            snum += ".";
            for (var i = 0; i < num; i++)
            {
                snum += "0";
            }
        }
        return GetDecimal(data).ToString(snum);
    }

    public static decimal GetDecimal(string data)
    {
        decimal result = 0;
        decimal.TryParse(data, out result);
        return result;
    }
    /// <summary>
    /// 檢查是否超過每個月可以加的時數(狀態為送簽、核准)
    /// </summary>
    /// <param name="Table">查詢本單的table</param>
    /// <param name="strComp">公司代碼</param>
    /// <param name="strEmp">員工編號</param>
    /// <param name="dateStart">開始日期</param>
    /// <param name="dateEnd">結束日期</param>
    /// <param name="MonthLimitHour">參數設定</param>
    /// <param name="totalTime">本單的加班時數</param>
    /// <param name="mealtime">用餐時數</param>
    /// <param name="cntStart">(跨日)開始時數</param>
    /// <param name="cntEnd">(跨日)結束時數</param>
    /// <param name="strOTFromAdvanceTxnId">從預先申請到事後申報的OTTxnID</param>
    /// <returns></returns>
    public bool checkMonthTime(string Table, string strComp, string strEmp, string dateStart, string dateEnd, double MonthLimitHour, double totalTime, double mealtime, double cntStart, double cntEnd, string strOTFromAdvanceTxnId)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        bool blstart = CheckHolidayOrNot(dateStart);
        bool blend = CheckHolidayOrNot(dateEnd);
        DataTable dt = null;
        double dbTotal = 0;
        double dbHoTotal = 0;        
        string mealOver = "";
        double datecheck = 0;
        //本月加班時數
        sb.Reset();
        sb.Append("SELECT ISNULL(SUM(ISNULL(E.TotalTime,0)),0) AS TotalTime FROM(");
        //平日計算
        sb.Append(" SELECT ISNULL(SUM(ISNULL(A.TotalTime,0)),0) AS TotalTime FROM(");
        //事前平日計算(如事後有此筆，以事後為主)
        sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime  FROM OverTimeAdvance");
        sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0'");
        sb.Append(" AND OTTxnID NOT IN");
        sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
        if (Table == "OverTimeDeclaration")
        {
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')");
        }
        
        sb.Append(" UNION ALL");
        //事後平日計算
        sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeDeclaration");
        sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='0'");
        sb.Append(" ) A");
        sb.Append(" UNION ALL");
        //假日計算會有成長時數，以及排除國定假日(如事後有此筆，以事後為主)
        sb.Append(" SELECT ISNULL(SUM(ISNULL(D.TotalTime,0)),0) AS TotalTime FROM (");
        sb.Append(" SELECT CASE WHEN C.TotalTime >0 AND C.TotalTime <= 240 THEN 240");
        sb.Append(" WHEN C.TotalTime > 240 AND C.TotalTime <= 480 THEN 480");
        sb.Append(" WHEN C.TotalTime > 480 AND C.TotalTime <= 720 THEN 720 END AS TotalTime");
        sb.Append(" FROM (");
        sb.Append(" SELECT SUM(ISNULL(B.OTTotalTime,0))-SUM(ISNULL(B.MealTime,0)) AS TotalTime FROM (");
        //事前假日(如事後有此筆，以事後為主)
        sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeAdvance");
        sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1'");
        sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')");
        sb.Append(" AND OTTxnID NOT IN");
        sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
        if (Table == "OverTimeDeclaration")
        {
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')");
        }
        sb.Append(" UNION ALL");
        //事後假日
        sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeDeclaration");
        sb.Append(" WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dateStart + "') AND MONTH(OTStartDate)=MONTH('" + dateStart + "') AND HolidayOrNot='1'");
        sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')");
        sb.Append(" ) B WHERE 1=1");
        if (blstart && !blend)//假日跨平日
        {
            sb.Append(" AND B.OTStartDate<> '" + dateStart + "'");
        }
        else if (!blstart && blend)//平日跨假日
        {
            sb.Append(" AND B.OTStartDate<> '" + dateEnd + "'");
        }
        else if (blstart && blend)//假日跨假日(國定假日不納入檢核)
        {
            if (IsNationalHoliday(dateEnd))//迄日假日為國定假日
            {
                sb.Append(" AND B.OTStartDate<> '" + dateStart + "'");
            }
            else//起日假日為國定假日
            {
                sb.Append(" AND B.OTStartDate<> '" + dateEnd + "'");
            }
        }
        sb.Append(" GROUP BY B.OTStartDate");
        sb.Append(" ) C ) D) E");

        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        dbTotal = Convert.ToDouble(dt.Rows[0]["TotalTime"].ToString());

        if (dateStart == dateEnd) //不跨日
        {
            datecheck = totalTime - mealtime;
            if (!blstart)//平日
            {
                if (dbTotal + datecheck > (MonthLimitHour * 60))
                {
                    return false;
                }
            }
            else
            {
                if (IsNationalHoliday(dateStart))//國定假日不納入檢核
                {
                    if (dbTotal > (MonthLimitHour* 60))
                    {
                        return false;
                    }
                }
                else
                {
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId);//假日時數
                    if (dbHoTotal + datecheck > 0 && dbHoTotal + datecheck <= 240)
                    {
                        dbHoTotal = 240;
                    }
                    else if (dbHoTotal + datecheck > 240 && dbHoTotal + datecheck <= 480)
                    {
                        dbHoTotal = 480;
                    }
                    else if (dbHoTotal + datecheck > 480 && dbHoTotal + datecheck <= 720)
                    {
                        dbHoTotal = 720;
                    }
                    if (dbHoTotal + dbTotal > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
            }
        }
        else //跨日
        {
            if (!blstart && !blend)//平日
            {
                datecheck = cntStart + cntEnd - mealtime;
                if (dbTotal + datecheck > (MonthLimitHour * 60))
                {
                    return false;
                }
            }
            else if (blstart && !blend)//假日跨平日
            {
                mealOver = MealJudge(cntStart, mealtime);
                if (IsNationalHoliday(dateStart))//國定假日不納入檢核
                {
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(',')[3]);
                    if (dbTotal + datecheck > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
                else
                {
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId); //假日時數
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(',')[1]);//本單假日
                    if (dbHoTotal + datecheck > 0 && dbHoTotal + datecheck <= 240)
                    {
                        dbHoTotal = 240;
                    }
                    else if (dbHoTotal + datecheck > 240 && dbHoTotal + datecheck <= 480)
                    {
                        dbHoTotal = 480;
                    }
                    else if (dbHoTotal + datecheck > 480 && dbHoTotal + datecheck <= 720)
                    {
                        dbHoTotal = 720;
                    }
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(',')[3]);//本單平日
                    if (dbHoTotal + dbTotal + datecheck > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
            }
            else if (!blstart && blend)//平日跨假日
            {
                mealOver = MealJudge(cntStart, mealtime);
                if (IsNationalHoliday(dateEnd))//國定假日不納入檢核
                {
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(',')[1]);//本單平日
                    if (dbTotal + datecheck > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
                else
                {
                    dbHoTotal = GetNTotal(strComp, strEmp, dateEnd, Table, strOTFromAdvanceTxnId);//假日時數
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(',')[3]);//本單假日
                    if (dbHoTotal + datecheck > 0 && dbHoTotal + datecheck <= 240)
                    {
                        dbHoTotal = 240;
                    }
                    else if (dbHoTotal + datecheck > 240 && dbHoTotal + datecheck <= 480)
                    {
                        dbHoTotal = 480;
                    }
                    else if (dbHoTotal + datecheck > 480 && dbHoTotal + datecheck <= 720)
                    {
                        dbHoTotal = 720;
                    }
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(',')[1]);//本單平日
                    if (dbHoTotal + dbTotal + datecheck > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
            }
            else if (blstart && blend)//假日跨假日
            {
                if (IsNationalHoliday(dateStart) && IsNationalHoliday(dateEnd))//國定假日不納入檢核
                {
                    if (dbTotal> (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
                //迄日假日為國定假日
                else if (IsNationalHoliday(dateEnd))//國定假日不納入檢核
                {
                    dbHoTotal = GetNTotal(strComp, strEmp, dateStart, Table, strOTFromAdvanceTxnId);//假日時數
                    mealOver = MealJudge(cntStart, mealtime);
                    datecheck = cntStart - Convert.ToDouble(mealOver.Split(',')[1]);//本單假日(起日)
                    if (dbHoTotal + datecheck > 0 && dbHoTotal + datecheck <= 240)
                    {
                        dbHoTotal = 240;
                    }
                    else if (dbHoTotal + datecheck > 240 && dbHoTotal + datecheck <= 480)
                    {
                        dbHoTotal = 480;
                    }
                    else if (dbHoTotal + datecheck > 480 && dbHoTotal + datecheck <= 720)
                    {
                        dbHoTotal = 720;
                    }
                    if (dbTotal + dbHoTotal > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
                //起日假日為國定假日
                else if (IsNationalHoliday(dateStart))//國定假日不納入檢核
                {
                    dbHoTotal = GetNTotal(strComp, strEmp, dateEnd,Table,strOTFromAdvanceTxnId);//假日時數
                    datecheck = cntEnd - Convert.ToDouble(mealOver.Split(',')[3]);//本單假日(迄日)
                    if (dbHoTotal + datecheck > 0 && dbHoTotal + datecheck <= 240)
                    {
                        dbHoTotal = 240;
                    }
                    else if (dbHoTotal + datecheck > 240 && dbHoTotal + datecheck <= 480)
                    {
                        dbHoTotal = 480;
                    }
                    else if (dbHoTotal + datecheck > 480 && dbHoTotal + datecheck <= 720)
                    {
                        dbHoTotal = 720;
                    }
                    if (dbTotal + dbHoTotal > (MonthLimitHour * 60))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    /// <summary>
    /// 算假日的成長時數
    /// </summary>
    /// <param name="strEmp">員工</param>
    /// <param name="date">本單日期</param>
    /// <returns></returns>
    public double GetNTotal(string strComp, string strEmp, string date, string Table, string strOTFromAdvanceTxnId)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        double dbHoTotal = 0;
        DataTable dt = null;
        sb.Reset();
        //以小時計算
        sb.Append("SELECT ISNULL(SUM(A.TotalTime),0) AS TotalTime FROM");
        sb.Append(" (SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime  FROM OverTimeAdvance WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStartDate='" + date + "' AND OTStatus in ('2','3')");
        if (Table == "OverTimeDeclaration")
        {
            sb.Append(" AND OTTxnID NOT IN('" + strOTFromAdvanceTxnId + "')");
        }
        sb.Append(" UNION ALL");
        sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM OverTimeDeclaration WHERE OTCompID='" + strComp + "' AND OTEmpID='" + strEmp + "' AND OTStartDate='" + date + "' AND OTStatus in ('2','3')) A"); 
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        //假日時數
        dbHoTotal = Convert.ToDouble(dt.Rows[0]["TotalTime"].ToString());
        return dbHoTotal;
    }
    /// <summary>
    /// 檢查是否超過每個月可以加的時數(狀態為送簽、核准)
    /// </summary>
    /// <param name="dt">多筆送簽資料</param>
    /// <param name="MonthLimitHour">參數設定</param>
    /// <param name="Table">事前或事後</param>
    /// <returns></returns>
    public string GetMulitTotal(DataTable dt, double MonthLimitHour, string Table)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        double dbTotal = 0;
        string strEmp = "";
        string strMonth = "";
        string strYear = "";
        string msg = "";
        DataTable dtTotal = null;
        dt.DefaultView.Sort = "EmpID,OTStartDate asc";
        string strOTTxnID = "";
        string strOTFromAdvanceTxnId = "";
        string strOTFromAdvanceTxnId_1 = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strOTTxnID += dt.Rows[i]["OTTxnID"].ToString() + ";"; 
            if (Table == "OverTimeDeclaration")
            {
                strOTFromAdvanceTxnId += dt.Rows[i]["OTFromAdvanceTxnId"].ToString() + ";";
            }
        }
        if (Table == "OverTimeDeclaration")
        {
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'";
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3);
        }
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'";
        string strOTTxnID_1 = strOTTxnID.Substring(0, strOTTxnID.Length - 3);

        for (int j = 0; j < dt.Rows.Count; j++)
        {
            if (strEmp != dt.Rows[j]["EmpID"].ToString() || strMonth != dt.Rows[j]["OTStartDate"].ToString().Substring(5, 2) || strYear != dt.Rows[j]["OTStartDate"].ToString().Substring(0, 4)) //不同員編
            {
                
                strEmp = dt.Rows[j]["EmpID"].ToString();
                strMonth = dt.Rows[j]["OTStartDate"].ToString().Substring(5, 2);
                strYear = dt.Rows[j]["OTStartDate"].ToString().Substring(0, 4);
                sb.Reset();
                //本月加班時數
                sb.Reset();
                sb.Append(" SELECT ISNULL(SUM(ISNULL(E.TotalTime,0)),0) AS TotalTime FROM(");
                //平日計算
                sb.Append(" SELECT ISNULL(SUM(ISNULL(A.TotalTime,0)),0) AS TotalTime FROM(");
                //事前平日計算(如事後有此筆，以事後為主)
                sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime  FROM OverTimeAdvance");
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='0'");
                sb.Append(" AND OTTxnID NOT IN");
                sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='0' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
                if (Table == "OverTimeDeclaration")
                {
                    sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")");
                }
                sb.Append(" UNION ALL");
                //事後平日計算
                sb.Append(" SELECT ISNULL(SUM(OTTotalTime)-SUM(MealTime),0) AS TotalTime  FROM OverTimeDeclaration");
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='0'");
                sb.Append(" UNION ALL ");
                //本單平日計算
                sb.Append(" SELECT ISNULL(SUM(ISNULL(OTTotalTime,0))-SUM(ISNULL(MealTime,0)),0) AS TotalTime FROM " + Table);
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='0' AND OTTxnID IN(" + strOTTxnID_1 + ")");
                sb.Append(" ) A");
                sb.Append(" UNION ALL");
                //假日計算會有成長時數，以及排除國定假日(如事後有此筆，以事後為主)
                sb.Append(" SELECT ISNULL(SUM(ISNULL(D.TotalTime,0)),0) AS TotalTime FROM (");
                sb.Append(" SELECT CASE WHEN C.TotalTime >0 AND C.TotalTime <= 240 THEN 240");
                sb.Append(" WHEN C.TotalTime > 240 AND C.TotalTime <= 480 THEN 480");
                sb.Append(" WHEN C.TotalTime > 480 AND C.TotalTime <= 720 THEN 720 END AS TotalTime");
                sb.Append(" FROM (");
                sb.Append(" SELECT ISNULL(SUM(ISNULL(B.OTTotalTime,0))-SUM(ISNULL(B.MealTime,0)),0) AS TotalTime FROM (");
                //事前假日(如事後有此筆，以事後為主)
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeAdvance");
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='1'");
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')");
                sb.Append(" AND OTTxnID NOT IN");
                sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='1' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
                if (Table == "OverTimeDeclaration")
                {
                    sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")");
                }
                sb.Append(" UNION ALL");
                //事後假日
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM OverTimeDeclaration");
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND OTStatus IN ('2','3') AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='1'");
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')");
                sb.Append(" UNION ALL");
                //本單假日
                sb.Append(" SELECT OTStartDate, ISNULL(OTTotalTime,0) OTTotalTime, ISNULL(MealTime,0) MealTime FROM " + Table);
                sb.Append(" WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND YEAR(OTStartDate)=YEAR('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND MONTH(OTStartDate)=MONTH('" + dt.Rows[j]["OTStartDate"].ToString() + "') AND HolidayOrNot='1'");
                sb.Append(" AND OTStartDate NOT IN (SELECT Code FROM AT_CodeMap WHERE TabName = 'NationalHolidayDefine' AND FldName='HolidayDate' AND NotShowFlag='0')");
                sb.Append(" AND OTTxnID IN(" + strOTTxnID_1 + ") ");
                sb.Append(" ) B ");
                sb.Append(" GROUP BY B.OTStartDate");
                sb.Append(" ) C ) D ) E");

                dtTotal = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                dbTotal = Convert.ToDouble(dtTotal.Rows[0]["TotalTime"].ToString());
                if (dbTotal > (MonthLimitHour * 60))
                {
                    msg = "false" + ";" + dt.Rows[j]["EmpID"].ToString() + ";" + dt.Rows[j]["OTStartDate"].ToString();
                    return msg;
                }
            }
        }
        return "true" + ";" + "";
    }
    /// <summary>
    /// 多筆每日上限
    /// </summary>
    /// <param name="dt">多筆送簽資料</param>
    /// <param name="dayNLimit">平日加班時數</param>
    /// <param name="dayHLimit">假日加班時數</param>
    /// <param name="Table">事前或事後</param>
    /// <returns></returns>
    public string GetCheckOverTimeIsOver(DataTable dt, double dayNLimit, double dayHLimit, string Table)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string msg = "";
        string strOTTxnID = "";
        string strOTFromAdvanceTxnId = "";
        string strOTFromAdvanceTxnId_1 = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strOTTxnID += dt.Rows[i]["OTTxnID"].ToString() + ";";
            if (Table == "OverTimeDeclaration")
            {
                strOTFromAdvanceTxnId += dt.Rows[i]["OTFromAdvanceTxnId"].ToString() + ";";
            }
        }
        if (Table == "OverTimeDeclaration")
        {
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'";
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3);
        }
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'";
        string strOTTxnID_1 = strOTTxnID.Substring(0, strOTTxnID.Length - 3);
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            sb.Reset();
            sb.Append("SELECT A.OTStartDate,SUM(OTTotalTime)-SUM(MealTime) AS TotalTime FROM (");
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM OverTimeAdvance WHERE OTCompID='" + dt.Rows[j]["OTCompID"] + "' AND OTEmpID='" + dt.Rows[j]["EmpID"] + "' AND OTStatus IN ('2','3')");
            if (dt.Rows[j]["OTStartDate"].ToString() == dt.Rows[j]["OTEndDate"].ToString())
            {
                sb.Append(" AND OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "'");
            }
            else
            {
                sb.Append(" AND (OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "' OR OTStartDate='" + dt.Rows[j]["OTEndDate"].ToString() + "')");
            }
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows[j]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[j]["EmpID"].ToString() + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");

            if (Table == "OverTimeDeclaration")
            {
                sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")");
            }

            sb.Append(" UNION ALL ");
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows[j]["OTCompID"] + "' AND OTEmpID='" + dt.Rows[j]["EmpID"] + "' AND OTStatus IN ('2','3')");
            if (dt.Rows[j]["OTStartDate"].ToString() == dt.Rows[j]["OTEndDate"].ToString())
            {
                sb.Append(" AND OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "'");
            }
            else
            {
                sb.Append(" AND (OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "' OR OTStartDate='" + dt.Rows[j]["OTEndDate"].ToString() + "')");
            }
            sb.Append(" UNION ALL");
            sb.Append(" SELECT OTStartDate,OTTotalTime,MealTime,OTTxnID FROM " + Table + " WHERE OTTxnID in(" + strOTTxnID_1 + ") AND OTCompID='" + dt.Rows[j]["OTCompID"] + "' AND OTEmpID='" + dt.Rows[j]["EmpID"] + "') A");
            if (dt.Rows[j]["OTStartDate"].ToString() == dt.Rows[j]["OTEndDate"].ToString())
            {
                sb.Append(" WHERE A.OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "'");
            }
            else
            {
                sb.Append(" WHERE A.OTStartDate='" + dt.Rows[j]["OTStartDate"].ToString() + "'");
                sb.Append(" OR A.OTStartDate='" + dt.Rows[j]["OTEndDate"].ToString() + "'");
            }
            sb.Append(" GROUP BY A.OTStartDate");
            DataTable dt1 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                bool blstart = CheckHolidayOrNot(dt1.Rows[i]["OTStartDate"].ToString());
                if (blstart)//假日
                {
                    if (Convert.ToDouble(dt1.Rows[i]["TotalTime"]) > (dayHLimit * 60))
                    {
                        msg = "false" + ";" + dt.Rows[j]["EmpID"].ToString() + ";" + dt1.Rows[i]["OTStartDate"].ToString() + ";" + Convert.ToString(dayHLimit);
                        return msg;
                    }
                }
                else
                {
                    if (Convert.ToDouble(dt1.Rows[i]["TotalTime"]) > (dayNLimit * 60))
                    {
                        msg = "false" + ";" + dt.Rows[j]["EmpID"].ToString() + ";" + dt1.Rows[i]["OTStartDate"].ToString() + ";" + Convert.ToString(dayNLimit);
                        return msg;
                    }
                }
            }
        }
        return "true" + ";" + "";
    }
    /// <summary>
    /// 連續加班日
    /// </summary>
    /// <param name="dt">多筆送簽資料</param>
    /// <param name="strOTLimitDay">參數檔連續加班日</param>
    /// <param name="Table">事前或事後</param>
    /// <returns></returns>
    public string GetCheckOTLimitDay(DataTable dt, string strOTLimitDay, string Table)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        dt.DefaultView.Sort = "EmpID asc";
        int OTLimitDay = Convert.ToInt32(strOTLimitDay);
        string strOTTxnID = "";
        string msg = "";
        string strEmp="";
        string strOTFromAdvanceTxnId = "";
        string strOTFromAdvanceTxnId_1 = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            strOTTxnID += dt.Rows[i]["OTTxnID"].ToString() + ";";
            if (Table == "OverTimeDeclaration")
            {
                strOTFromAdvanceTxnId += dt.Rows[i]["OTFromAdvanceTxnId"].ToString() + ";";
            }
        }
        if (Table == "OverTimeDeclaration")
        {
            strOTFromAdvanceTxnId = "'" + (strOTFromAdvanceTxnId.Replace(";", "','")) + "'";
            strOTFromAdvanceTxnId_1 = strOTFromAdvanceTxnId.Substring(0, strOTFromAdvanceTxnId.Length - 3);
        }
        strOTTxnID = "'" + (strOTTxnID.Replace(";", "','")) + "'";
        string strOTTxnID_1 = strOTTxnID.Substring(0, strOTTxnID.Length - 3);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int cnt = 0;
            strEmp = dt.Rows[i]["EmpID"].ToString();
            sb.Reset();
            sb.Append("SELECT C.SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (");
            sb.Append(" SELECT DISTINCT OTStartDate FROM " + Table + " WHERE OTTxnID in(" + strOTTxnID_1 + ") AND OTEmpID='" + dt.Rows[i]["EmpID"] + "' AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "') ");
            sb.Append(" UNION");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID='" + dt.Rows[i]["OTCompID"] + "' AND OTEmpID='" + dt.Rows[i]["EmpID"] + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + dt.Rows[i]["OTCompID"].ToString() + "' AND OTEmpID='" + dt.Rows[i]["EmpID"].ToString() + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            if (Table == "OverTimeDeclaration")
            {
                sb.Append(" AND OTTxnID NOT IN(" + strOTFromAdvanceTxnId_1 + ")");
            }
            sb.Append(" UNION");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID='" + dt.Rows[i]["OTCompID"] + "' AND OTEmpID='" + dt.Rows[i]["EmpID"] + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "')) O");
            sb.Append(" FULL OUTER JOIN(");
            sb.Append(" SELECT * FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] WHERE  CompID='" + dt.Rows[i]["OTCompID"] + "' AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "') AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + dt.Rows[i]["OTStartDate"] + "')) C ON O.OTStartDate=C.SysDate");
            sb.Append(" ORDER BY C.SysDate ASC");
            DataTable dt1 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["HolidayOrNot"].ToString() == "0")
                {
                    cnt += 1;
                }
                else
                {
                    if (!string.IsNullOrEmpty(dt1.Rows[j]["OTStartDate"].ToString()))
                    {
                        cnt += 1;
                    }
                    else
                    {
                        cnt = 0;
                    }
                }
                if (cnt >= OTLimitDay)
                {
                    msg = "false" + ";" + strEmp;
                    return msg;
                }
            }
        }
        return "true" + ";" + "";
    }
    /// <summary>
    /// 附件編號(加班人)
    /// </summary>
    /// <param name="strAttachID">string</param>
    /// <param name="strEmpID">string</param>
    /// <returns>string</returns>
    public string QueryAtt(string strAttachID, string strCompID,string strEmpID)//附件編號(加班人)
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string attach="";
        DataTable dtAtt = QueryData("AttachID", "AttachInfo", "AND AttachID='" + strAttachID + "' AND FileSize>0");
         if (dtAtt.Rows.Count > 0)
         {
             DataTable dt = QueryData("FileSeq,CONVERT(CHAR(10), LastChgDate, 111) AS LastChgDate", "OverTimeSeq", "AND CompID='" + strCompID + "' AND EmpID='" + strEmpID + "'");
             if (dt.Rows.Count > 0)
             {
                 if (dt.Rows[0]["LastChgDate"].ToString() != DateTime.Now.ToString("yyyy/MM/dd"))//不是今天的資料
                 {
                     sb.Reset();
                     sb.Append(" UPDATE OverTimeSeq SET FileSeq='1',LastChgDate=GETDATE()");
                     sb.Append(" WHERE CompID='" + strCompID + "'");
                     sb.Append(" AND EmpID='" + strEmpID + "'");
                     db.ExecuteNonQuery(sb.BuildCommand());
                     attach = DateTime.Now.ToString("yyyyMMdd") + "01" + strEmpID;
                 }
                 else
                 {
                     int AttSeq = Convert.ToInt32(dt.Rows[0]["FileSeq"].ToString()) + 1;
                     sb.Append(" UPDATE OverTimeSeq SET FileSeq='" + AttSeq + "',LastChgDate=GETDATE()");
                     sb.Append(" WHERE CompID='" + strCompID + "'");
                     sb.Append(" AND EmpID='" + strEmpID + "'");
                     db.ExecuteNonQuery(sb.BuildCommand());
                     attach = DateTime.Now.ToString("yyyyMMdd") + AttSeq.ToString("00") + strEmpID;
                 }
             }
             else
             {
                 sb.Reset();
                 sb.Append("INSERT INTO OverTimeSeq(CompID, EmpID, AdvanceFormSeq, DeclarationFormSeq, FileSeq, LastChgDate)");
                 sb.Append(" VALUES('" + strCompID + "', '" + strEmpID + "', '1' ,'1', '1', GETDATE())");
                 db.ExecuteNonQuery(sb.BuildCommand());
                 attach = DateTime.Now.ToString("yyyyMMdd") + "01" + strEmpID;
             }
             sb.Reset();
             if (!string.IsNullOrEmpty(attach))
             {
                 sb.Append(" UPDATE AttachInfo SET AttachID='" + attach + "'");
                 sb.Append(" WHERE AttachID='" + strAttachID + "'");
                 db.ExecuteNonQuery(sb.BuildCommand());
                 
             }
             else
             {
                 //sb.Append(" DELETE FROM AttachInfo ");
                 //sb.Append(" WHERE AttachID='" + strAttachID + "'");
             }           
         }
         else
         {
             attach = "";
             sb.Reset();
             sb.Append(" DELETE FROM AttachInfo ");
             sb.Append(" WHERE AttachID='" + strAttachID + "'");
             db.ExecuteNonQuery(sb.BuildCommand());
         }

         return attach;
    }
    public int QuerySeq(string strTable, string strCompID, string strEmpID, string date)//Seq
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        int OTSeq = 0;
        DataTable dt = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, "AND OTCompID='" + strCompID + "' AND OTEmpID='" + strEmpID + "' AND OTStartDate='" + date + "'");
        if (dt.Rows.Count == 0)
        {
            OTSeq = 1;
        }
        else
        {
            OTSeq = Convert.ToInt32(dt.Rows[0]["MAXOTSeq"].ToString() == "" ? "0" : dt.Rows[0]["MAXOTSeq"].ToString()) + 1;
        }
        return OTSeq;
    }
    public int QuerySeqNoAdd(string strTable, string strCompID, string strEmpID, string date)//Seq
    {
        DbHelper db = new DbHelper(_AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        int OTSeq = 0;
        DataTable dt = QueryData("MAX(OTSeq) AS MAXOTSeq", strTable, "AND OTCompID='" + strCompID + "' AND OTEmpID='" + strEmpID + "' AND OTStartDate='" + date + "'");
        if (dt.Rows.Count == 0)
        {
            OTSeq = 0;
        }
        else
        {
            OTSeq = Convert.ToInt32(dt.Rows[0]["MAXOTSeq"].ToString() == "" ? "0" : dt.Rows[0]["MAXOTSeq"].ToString());
        }
        return OTSeq;
    }
    #region "新增MailLog"
    /// <summary>
    /// 新增MailLog
    /// </summary>
    /// <param name="CreateTime">送簽成功時間</param>
    /// <param name="Seq">序號(從1開始)</param>
    /// <param name="Sender">人力資源處</param>
    /// <param name="AcceptorCompID">簽核主管公司</param>
    /// <param name="Acceptor">簽核主管</param>
    /// <param name="EMail">EMail(OHR-Judy@sinopac.com)</param>
    /// <param name="Subject">主旨</param>
    /// <param name="Content">內容</param>

    public static void InsertMailLogCommand(
        string Sender, string AcceptorCompID, string Acceptor,
        string EMail, string Subject_1, string Content_1, string Subject_2, string Content_2,
        bool isResetCommand, ref CommandHelper sb
        )
    {
        Aattendant at = new Aattendant();
        var dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        int seq = 1;
        DataTable dt = at.QueryData("ISNULL(MAX(Seq),0) AS Seq", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[MailLog]", " AND CreateTime='" + dateNow + "'");
        if (dt.Rows.Count > 0)
        {
            seq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString()) + 1;
        }
        
        if (isResetCommand)
        {
            sb.Reset();
        }
        sb.AppendStatement(" INSERT INTO " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[MailLog] ");
        sb.Append(" ( ");
        sb.Append(" CreateTime, Seq, Sender, AcceptorCompID, Acceptor, EMail, Subject, Content");
        sb.Append(" ) ");
        sb.Append(" VALUES ");
        sb.Append(" ('" + dateNow + "','"+ seq.ToString() +"','" + Sender + "','" + AcceptorCompID + "','" + Acceptor + "','" + EMail + "','" + Subject_2 + "','" + Content_2 + "' "); 
        sb.Append(" ); ");
    }
    #endregion "新增HROverTimeLog"
}
//特殊人員下拉選單
public partial class WcfCascadingHelper
{
    [OperationContract]
    public CascadingDropDownNameValue[] Util_GetUser(string knownCategoryValues, string category, string contextKey)
    {
        //拆解 CategoryValues
        StringDictionary dicCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        StringDictionary dicContext = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);
        DataTable dt = null;
        Aattendant at = new Aattendant();
        DbHelper db = new DbHelper(Util.getAppSetting("app://AattendantDB_OverTime/"));//"AattendantDB"
        CommandHelper sb = db.CreateCommandHelper();

        DataTable dtSPUser = at.QueryData("*", "OverTimeSPUser", " AND CompID='" + UserInfo.getUserInfo().CompID + "' AND EmpID='" + UserInfo.getUserInfo().UserID + "'");//特殊人員
        //DataTable dtOrg = at.QueryData("*", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", "  And InValidFlag = '0' And VirtualFlag = '0' ");
        string strOrgList = "";
        string strOrgFlowList = "";
        DataTable dtEmpty = new DataTable();
        dtEmpty.Columns.Add("TypeID");
        dtEmpty.Columns.Add("TypeName");

        //判斷要處理那一階選單
        switch (category)
        {
            case "CompID":
                dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select CompID, CompName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] Where InValidFlag = '0' And NotShowFlag = '0' Order By CompID")).Tables[0];
                break;
            case "DeptID":
                    DataTable dt3 = new DataTable();
                    dt3.Columns.Add("DeptID");
                    dt3.Columns.Add("DeptName");

                    if (dtSPUser.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtSPUser.Rows[0]["OrgList"].ToString()))
                        {
                            strOrgList = dtSPUser.Rows[0]["OrgList"].ToString() + "," + UserInfo.getUserInfo().OrganID;
                        }
                        else
                        {
                            strOrgList = UserInfo.getUserInfo().OrganID;
                        }
                        strOrgList = strOrgList.Replace(",", "','");
                        //for (int i = 0; i < strOrgList.Split(',').Length; i++)
                        //{
                        //    DataRow dr = dt3.NewRow();
                        //    var DeptID = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == strOrgList.Split(',')[i] && data.Field<string>("InValidFlag") == "0" && data.Field<string>("VirtualFlag") == "0" && data.Field<string>("CompID") == dicCategory["CompID"] select data).FirstOrDefault();
                        //    dr["DeptID"] = DeptID["DeptID"];
                        //    string OrganID = Convert.ToString(DeptID["DeptID"]);
                        //    var DeptName = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == OrganID && data.Field<string>("InValidFlag") == "0" && data.Field<string>("VirtualFlag") == "0" && data.Field<string>("CompID") == dicCategory["CompID"] select data).FirstOrDefault();
                        //    dr["DeptName"] = DeptName["OrganName"];
                        //    dt3.Rows.Add(dr);
                        //}
                        //dt = dt3.DefaultView.ToTable(true, new string[] { "DeptID", "DeptName" });
                        sb.Reset();
                        sb.Append("Select DISTINCT O.DeptID,O2.OrganName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] O");
                        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] O2 ON O.DeptID=O2.OrganID");
                        sb.Append(" Where O.CompID = '" + dicCategory["CompID"] + "' And O.InValidFlag = '0' And O.VirtualFlag = '0' ");
                        sb.Append(" AND O.OrganID IN('" + strOrgList + "')");
                        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                        //dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select DISTINCT DeptID,OrganName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] Where CompID = '{0}' And InValidFlag = '0' And VirtualFlag = '0' AND OrganID IN('" + strOrgList + "')", dicCategory["CompID"])).Tables[0];
                    }
                    else
                    {
                        dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select OrganID,OrganName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] Where CompID = '{0}' And InValidFlag = '0' And VirtualFlag = '0'", dicCategory["CompID"])).Tables[0];
                    }
                break;
            case "OrganID":
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("OrganID");
                    dt2.Columns.Add("OrganName");
                    dt2.Columns.Add("DeptID");
                    if (dtSPUser.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtSPUser.Rows[0]["OrgList"].ToString()))
                        {
                            strOrgList = dtSPUser.Rows[0]["OrgList"].ToString() + "," + UserInfo.getUserInfo().OrganID;
                        }
                        else
                        {
                            strOrgList = UserInfo.getUserInfo().OrganID;
                        }
                        strOrgList = strOrgList.Replace(",", "','");
                        //for (int i = 0; i < strOrgList.Split(',').Length; i++)
                        //{
                        //    DataRow dr = dt2.NewRow();
                        //    dr["OrganID"] = strOrgList.Split(',')[i];
                        //    var OrganName = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == strOrgList.Split(',')[i] && data.Field<string>("InValidFlag") == "0" && data.Field<string>("VirtualFlag") == "0" && data.Field<string>("CompID") == dicCategory["CompID"] select data).FirstOrDefault();
                        //    dr["OrganName"] = OrganName["OrganName"];
                        //    var DeptID = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == strOrgList.Split(',')[i] && data.Field<string>("InValidFlag") == "0" && data.Field<string>("VirtualFlag") == "0" && data.Field<string>("CompID") == dicCategory["CompID"] select data).FirstOrDefault();
                        //    dr["DeptID"] = DeptID["DeptID"];
                        //    dt2.Rows.Add(dr);
                        //}
                        //dt2 = dt2.DefaultView.ToTable(true, new string[] { "OrganID", "OrganName", "DeptID" });
                        //dt = dt2.Select("DeptID='" + dicCategory["DeptID"] + "'").CopyToDataTable();
                        dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select DISTINCT OrganID,OrganName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] Where CompID = '{0}' And InValidFlag = '0' And VirtualFlag = '0' And DeptID='{1}' AND OrganID IN('" + strOrgList + "')", dicCategory["CompID"], dicCategory["DeptID"])).Tables[0];
                    }
                    else
                    {
                        dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select OrganID,OrganName From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] Where CompID = '{0}' And InValidFlag = '0' And VirtualFlag = '0' And DeptID='{1}'", dicCategory["CompID"], dicCategory["DeptID"])).Tables[0];
                    }
                break;
            case "UserID":
                dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select EmpID,NameN From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] Where CompID = '{0}' And DeptID='{1}' And OrganID='{2}' AND WorkStatus = '1'", dicCategory["CompID"], dicCategory["DeptID"], dicCategory["OrganID"])).Tables[0];
                break;

            //特殊人員orgFlow
            case "OrgFlowDeptID":
                if (!string.IsNullOrEmpty(dtSPUser.Rows[0]["OrgFlowList"].ToString()))
                {
                    //strOrgFlowList = "'" + dtSPUser.Rows[0]["OrgFlowList"].ToString().Replace(",", "','") + "'";
                    //DataTable dtBusinType = at.QueryData("DISTINCT BusinessType", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID IN (" + strOrgFlowList + ")");
                    //string BusinType = "";
                    //if (dtBusinType.Rows.Count > 0)
                    //{
                    //    if (dtBusinType.Rows.Count > 1)
                    //    {
                    //        for (int i = 0; i < dtBusinType.Rows.Count; i++)
                    //        {
                    //            BusinType += dtBusinType.Rows[i]["BusinessType"].ToString() + ",";
                    //        }
                    //        BusinType = ("'" + BusinType.Substring(0, BusinType.Length - 1) + "'").Replace(",", "','");
                    //    }
                    //    else
                    //    {
                    //        BusinType = "'" + dtBusinType.Rows[0]["BusinessType"].ToString() + "'";
                    //    }
                        //DataTable dtRoleCode = at.QueryData("DISTINCT RoleCode", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID IN (" + strOrgFlowList + ") ORDER BY RoleCode ASC");
                        //if (dtRoleCode.Rows.Count > 0)
                        //{
                            //if (dtRoleCode.Rows[0]["RoleCode"].ToString() == "0")
                            //{
                            //    sb.Reset();
                            //    sb.Append("SELECT * FROM (");
                            //    sb.Append(" SELECT OrganID,OrganName,BusinessType,RoleCode");
                            //    sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode in('40') AND OrganID=DeptID AND BusinessType IN(" + BusinType + ")");
                            //    sb.Append(" UNION");
                            //    sb.Append(" SELECT OrganID,'　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='30' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='20' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='0' AND BusinessType IN(" + BusinType + "))))");
                            //    sb.Append(" UNION");
                            //    sb.Append(" SELECT OrganID,'　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='20' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='0' AND BusinessType IN(" + BusinType + ")))");
                            //    sb.Append(" UNION");
                            //    sb.Append(" SELECT OrganID,'　　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                            //    sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='0' AND BusinessType IN(" + BusinType + "))");
                            //    sb.Append(" UNION");
                            //    sb.Append(" SELECT OrganID,'　　　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ")");
                            //    sb.Append(" AND RoleCode='0' AND BusinessType IN(" + BusinType + ")");
                            //    sb.Append(" ) A");
                            //    sb.Append(" WHERE A.RoleCode IN('40','30','20')");
                            //    sb.Append(" ORDER BY A.BusinessType,A.RoleCode DESC");
                            //}
                            //else if (dtRoleCode.Rows[0]["RoleCode"].ToString() == "10")
                            //{
                        //sb.Reset();
                        //sb.Append("SELECT OrganID,OrganName,BusinessType,RoleCode");
                        //sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode in('40') AND OrganID=DeptID AND BusinessType IN(" + BusinType + ")");
                        //sb.Append(" UNION");
                        //sb.Append(" SELECT OrganID,'　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode='20' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN(" + strOrgFlowList + "))");
                        //sb.Append(" UNION");
                        //sb.Append(" SELECT OrganID,'　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode='30' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode='20' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN(" + strOrgFlowList + ")))");
                        //sb.Append(" ORDER BY BusinessType,RoleCode DESC");
                            //}
                        //}
                        //sb.Append("SELECT OrganID,CASE RoleCode WHEN '40' THEN  OrganName  WHEN '30' THEN  '　　' +OrganName ELSE '　　　　' +OrganName END  AS OrganName,BusinessType,RoleCode");
                        //sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE RoleCode in('40','30')");
                        //sb.Append(" AND OrganID=DeptID AND BusinessType IN('01','02','03')");
                        //sb.Append(" UNION");
                        //sb.Append(" SELECT OrganID,CASE RoleCode WHEN '40' THEN  OrganName  WHEN '30' THEN  '　　' +OrganName ELSE '　　　　' +OrganName END  AS OrganName,BusinessType,RoleCode");
                        //sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE RoleCode in('20')");
                        //sb.Append(" AND OrganID=DeptID AND BusinessType IN('01','02','03') AND OrganID IN (" + strOrgFlowList + ")");
                        //sb.Append(" UNION");
                        //sb.Append(" SELECT OrF.DeptID,'　　　　' +OrF2.OrganName AS OrganName,OrF2.BusinessType,OrF2.RoleCode");
                        //sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] OrF");
                        //sb.Append(" LEFT JOIN  " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] OrF2 ON OrF.DeptID=OrF2.OrganID");
                        //sb.Append(" WHERE OrF.BusinessType IN('01','02','03') AND OrF.OrganID IN (" + strOrgFlowList + ")");
                        //sb.Append(" AND OrF.RoleCode in('0','10')");
                        //sb.Append(" ORDER BY BusinessType,RoleCode DESC");
                    //}
                        sb.Reset();
                        sb.Append("SELECT OrganID,CASE RoleCode WHEN '40' THEN OrganName WHEN '30' THEN '　　' +OrganName ELSE '　　　' +OrganName END AS OrganName FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        sb.Append(" WHERE OrganID IN(SELECT * FROM funGetUnderOrganFlow_multi('" + UserInfo.getUserInfo().CompID + "','" + UserInfo.getUserInfo().UserID + "', 'B')) AND RoleCode IN('40','30','20') ORDER BY BusinessType,RoleCode DESC");
                        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                }
                else
                {
                    dt = dtEmpty;
                }
                break;
            case "OrgFlowOrganID":
                if (!string.IsNullOrEmpty(dtSPUser.Rows[0]["OrgFlowList"].ToString()))
                {
                    //strOrgFlowList = "'" + dtSPUser.Rows[0]["OrgFlowList"].ToString().Replace(",", "','") + "'";
                    //sb.Reset();
                    // DataTable dtBusinType = at.QueryData("DISTINCT BusinessType", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID IN (" + strOrgFlowList + ")");
                    //string BusinType = "";
                    //if (dtBusinType.Rows.Count > 0)
                    //{
                    //    if (dtBusinType.Rows.Count > 1)
                    //    {
                    //        for (int i = 0; i < dtBusinType.Rows.Count; i++)
                    //        {
                    //            BusinType += dtBusinType.Rows[i]["BusinessType"].ToString() + ",";
                    //        }
                    //        BusinType = ("'" + BusinType.Substring(0, BusinType.Length - 1) + "'").Replace(",", "','");
                    //    }
                    //    else
                    //    {
                    //        BusinType = "'" + dtBusinType.Rows[0]["BusinessType"].ToString() + "'";
                    //    }
                        //DataTable dtRoleCode = at.QueryData("DISTINCT RoleCode", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID IN (" + strOrgFlowList + ") AND UpOrganID='" + dicCategory["OrgFlowDeptID"] + "' ORDER BY RoleCode ASC");
                        //if (dtRoleCode.Rows.Count > 0)
                        //{
                        //    if (dtRoleCode.Rows[0]["RoleCode"].ToString() == "0")
                        //    {
                        //        sb.Reset();
                        //        sb.Append(" SELECT OrganID,'　　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                        //        sb.Append(" WHERE RoleCode='10' AND BusinessType IN(" + BusinType + ") AND OrganID IN (SELECT DISTINCT UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='0' AND BusinessType IN(" + BusinType + "))");
                        //        sb.Append(" UNION");
                        //        sb.Append(" SELECT OrganID,'　　　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ")");
                        //        sb.Append(" AND RoleCode='0' AND BusinessType IN(" + BusinType + ")");
                        //        sb.Append(" ORDER BY BusinessType,RoleCode DESC");
                        //    }
                        //    else if (dtRoleCode.Rows[0]["RoleCode"].ToString() == "10")
                        //    {
                        //        sb.Reset();
                        //        sb.Append(" SELECT OrganID,'　　　　' +OrganName AS OrganName,BusinessType,RoleCode,UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='10' AND BusinessType IN(" + BusinType + ")");
                        //        sb.Append(" AND UpOrganID='" + dicCategory["OrgFlowDeptID"] + "'");
                        //        sb.Append(" UNION");
                        //        sb.Append(" SELECT OrganID,'　　　　　' +OrganName,BusinessType,RoleCode FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] WHERE OrganID IN (" + strOrgFlowList + ")");
                        //        sb.Append(" AND RoleCode='0' AND BusinessType IN(" + BusinType + ")");
                        //        sb.Append(" ORDER BY BusinessType,RoleCode DESC");
                         //sb.Reset();
                         //sb.Append("SELECT OrganID,'　　　　' +OrganName AS OrganName,BusinessType,RoleCode,UpOrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                         //sb.Append(" WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='10' AND BusinessType IN(" + BusinType + ") AND UpOrganID='" + dicCategory["OrgFlowDeptID"] + "'");
                         //sb.Append(" UNION "); 
                         //sb.Append(" SELECT OrganID,'　　　　　' +OrganName,BusinessType,RoleCode,UpOrganID FROM eHRMSDB_ITRD.[dbo].[OrganizationFlow] ");
                         //sb.Append(" WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='0' AND BusinessType IN(" + BusinType + ") ");
                         //sb.Append(" AND UpOrganID IN (SELECT DISTINCT OrganID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                         //sb.Append(" WHERE OrganID IN (" + strOrgFlowList + ") AND RoleCode='10' AND BusinessType IN(" + BusinType + ") AND UpOrganID='" + dicCategory["OrgFlowDeptID"] + "'");
                         //sb.Append(" )");
                         //sb.Append(" ORDER BY BusinessType,RoleCode DESC"); 
                            //}
                        //}
                    //}
                    //sb.Append("SELECT OrganID,CASE RoleCode WHEN '10' THEN  OrganName  ELSE  '　　' +OrganName END  AS OrganName,BusinessType,RoleCode");
                    //sb.Append(" FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                    //sb.Append(" WHERE RoleCode in('0','10') AND BusinessType IN('01','02','03') AND UpOrganID='" + dicCategory["OrgFlowDeptID"] + "'");
                    //sb.Append(" AND OrganID IN (" + strOrgFlowList + ")");
                    //sb.Append(" ORDER BY BusinessType,RoleCode DESC");
                    sb.Reset();
                    sb.Append("SELECT OrganID,CASE RoleCode WHEN '10' THEN '　　　　' +OrganName  ELSE '　　　　　' +OrganName END AS OrganName FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]");
                    sb.Append(" WHERE OrganID IN(SELECT * FROM funGetUnderOrganFlow_multi('" + UserInfo.getUserInfo().CompID + "','" + UserInfo.getUserInfo().UserID + "', 'B')) AND RoleCode IN('10','0')  AND DeptID='" + dicCategory["OrgFlowDeptID"] + "' ORDER BY BusinessType,RoleCode DESC");
                    dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                }
                else
                {
                    dt = dtEmpty;
                }
                break;
            case "OrgFlowUserID":
                if (!string.IsNullOrEmpty(dtSPUser.Rows[0]["OrgFlowList"].ToString()))
                {
                    strOrgFlowList = "'" + dtSPUser.Rows[0]["OrgFlowList"].ToString().Replace(",", "','") + "'";
                    bool conrainsAny = false;
                    if (!string.IsNullOrEmpty(dicCategory["OrgFlowOrganID"]))
                    {
                        conrainsAny = strOrgFlowList.Contains(dicCategory["OrgFlowOrganID"].ToString());
                        if (conrainsAny)
                        {
                            dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select EF.EmpID,P.NameN From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[EmpFlow] EF LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON EF.EmpID=P.EmpID WHERE EF.OrganID='{0}'", dicCategory["OrgFlowOrganID"])).Tables[0];
                        }
                    }
                    else
                    {
                        conrainsAny = strOrgFlowList.Contains(dicCategory["OrgFlowDeptID"].ToString());
                        if (conrainsAny)
                        {
                            dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select EF.EmpID,P.NameN From " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[EmpFlow] EF LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON EF.EmpID=P.EmpID WHERE EF.OrganID='{0}'", dicCategory["OrgFlowDeptID"])).Tables[0];
                        }
                    }
                }
                else
                {
                    dt = dtEmpty;
                }
                break;
            default:
                break;
        }
        return Util.getCascadingArray(dt);
    }
}