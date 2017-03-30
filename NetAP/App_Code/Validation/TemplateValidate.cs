using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// OTValidate 的摘要描述
/// </summary>
public partial class ValidationUtility
{
    /// <summary>
    /// 字串是否為數字
    /// </summary>
    /// <param name="str">string</param>
    /// <returns>bool</returns>
    public static bool IsAllNumber(string str)
    {
        var result = false;
        double i = 0;
        result = double.TryParse(str, out i);
        return result;
    }

}