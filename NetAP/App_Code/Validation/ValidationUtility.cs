using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// ValidationUtility 的摘要描述
/// </summary>
public partial class ValidationUtility
{
    /// <summary>
    /// 字串是否為數字
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>bool</returns>
    //public static bool IsAllNumber(string str)
    //{
    //    var result = false;
    //    double i = 0;
    //    result = double.TryParse(str, out i);
    //    return result;
    //}

    /// <summary>
    /// String判斷全形(不含中文)
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>bool</returns>
    public static bool IsAnyOneFullWidthWord(string str)
    {
        bool result = false;
        string pattern = @"^[\u4E00-\u9fa5]+$";
        foreach (char item in str)
        {
            //以Regex判斷是否為中文字，中文字視為全形
            if (!Regex.IsMatch(item.ToString(), pattern))
            {
                //以16進位值長度判斷是否為全形字
                if (string.Format("{0:X}", Convert.ToInt32(item)).Length != 2)
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// String判斷中文字
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>bool</returns>
    public static bool IsAnyOneChineseWord(string str)
    {
        bool result = false;
        string pattern = @"^[\u4E00-\u9fa5]+$";
        foreach (char item in str)
        {
            //以Regex判斷是否為中文字，中文字視為全形
            if (Regex.IsMatch(item.ToString(), pattern))
            {
                result = true;
                break;
            }
        }
        return result;
    }
}