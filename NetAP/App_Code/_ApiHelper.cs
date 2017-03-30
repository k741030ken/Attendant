using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using SinoPac.WebExpress.Common;

/// <summary>
/// Web API 輔助類別
/// </summary>
public partial class ApiHelper
{
    /// <summary>
    /// 是否為有效的請求 IP 位址
    /// </summary>
    /// <param name="arrValidIpList">有效的IP清單(null / * 代表所有IP)</param>
    /// <returns></returns>
    public static bool IsValidRequestIP(string[] arrValidIpList = null)
    {
        if (arrValidIpList == null)
        {
            return true;
        }
        else
        {
            if (arrValidIpList[0] != "*" && Array.IndexOf(arrValidIpList, Util.getClientIPv4()) < 0)
                return false;
            else
                return true;
        }
    }
}

// === 自訂 Web API 核心處理 === Andrew 2016.12.23
/// <summary>
/// 自訂API回傳格式
/// </summary>
public class ApiResultModel
{
    public HttpStatusCode Status { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}

/// <summary>
/// 覆寫 OnActionExecuted
/// </summary>
public class ApiResultAttribute : System.Web.Http.Filters.ActionFilterAttribute
{
    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
        // 此處不處理例外事件
        if (actionExecutedContext.Exception != null)
        {
            return;
        }

        base.OnActionExecuted(actionExecutedContext);
        ApiResultModel result = new ApiResultModel();
        result.Status = actionExecutedContext.ActionContext.Response.StatusCode;                                // 取得 API 返回的狀態碼
        result.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;        // 取得 API 返回的資料
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.Status, result);   // 封裝回傳格式
    }
}

/// <summary>
/// 覆寫 OnException
/// </summary>
public class ApiErrorHandleAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
{
    public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
    {
        base.OnException(actionExecutedContext);
        var errorMessage = actionExecutedContext.Exception.Message; // 取得錯誤訊息

        // 封裝回傳訊息
        ApiResultModel result = new ApiResultModel();
        result.Status = HttpStatusCode.BadRequest;
        result.ErrorMessage = errorMessage;
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(result.Status, result);

        // 記錄 Log
        string strLogMsg = string.Format("API [{0}], Status:[{1}({2})], ErrorMessage:[{3}]", actionExecutedContext.Request.RequestUri.AbsolutePath, result.Status, (int)result.Status, result.ErrorMessage);
        LogHelper.WriteSysLog(strLogMsg);
    }
}
