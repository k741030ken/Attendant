using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// DateUtility 的摘要描述
/// </summary>
public class DateUtility
{

    /// <summary>
    /// 取得星期
    /// </summary>
    /// <param name="sDay">日期</param>
    /// <param name="formatFlg">C: 中文 E: 英文縮寫 預設為數值 周日~週六為0~6</param>
    /// <returns></returns>
    public static string GetDayOfWeek(DateTime day, string formatFlg = "")
    {
        if (formatFlg.Equals("C"))
        {
            switch (day.DayOfWeek.ToString())
            {
                case "Monday": return "一";
                case "Tuesday": return "二";
                case "Wednesday": return "三";
                case "Thursday": return "四";
                case "Friday": return "五";
                case "Saturday": return "六";
                case "Sunday": return "日";
                default: return "";
            }
        }
        else if (formatFlg.Equals("E"))
        {
            switch (day.DayOfWeek.ToString())
            {
                case "Monday": return "Mon.";
                case "Tuesday": return "Tue.";
                case "Wednesday": return "Wed.";
                case "Thursday": return "Thu.";
                case "Friday": return "Fri.";
                case "Saturday": return "Sat.";
                case "Sunday": return "Sun.";
                default: return "";
            }
        }
        else
        {
            switch (day.DayOfWeek.ToString())
            {
                case "Monday": return "1";
                case "Tuesday": return "2";
                case "Wednesday": return "3";
                case "Thursday": return "4";
                case "Friday": return "5";
                case "Saturday": return "6";
                case "Sunday": return "0";
                default: return "";
            }
        }
    }
}