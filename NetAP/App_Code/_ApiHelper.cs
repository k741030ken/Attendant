using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// Web API 輔助類別 2017.03.31
/// <para>** 強制使用 ApiResultModel 傳回 JSON 格式的執行結果資料 **</para>
/// <para>** 強制於 ApiExpress 資料庫的 CustProperty 資料表內，定義各 Controller 的專屬設定 **</para>
/// </summary>
public partial class ApiHelper
{
    /// <summary>
    ///  Api 系統資料庫名稱
    /// </summary>
    public static string _ApiSysDB = "ApiExpress";

    /// <summary>
    /// 檢核是否為有效的 Controller 
    /// <para>** 偵測到 [本機開發模式] 時一律放行 **</para>
    /// </summary>
    /// <param name="strController">Controller 名稱</param>
    /// <param name="strErrMsg">錯誤訊息</param>
    /// <returns></returns>
    public static bool IsValidController(string strCtrlName, out string strErrMsg)
    {
        //2017.03.30 新增
        //判斷 Controller 是否啟用
        //判斷 Controller 的呼叫來源是否為有效 IP
        bool oResult = false;
        strErrMsg = string.Format("[{0}] Controller Disabled !", strCtrlName); //預設錯誤訊息

        if (Util._IsLocalDebugMode)
        {
            //本機開發模式時，一律放行
            oResult = true;
            strErrMsg = "";
            return oResult;
        }

        if (!string.IsNullOrEmpty(strCtrlName))
        {
            if (!DbHelper.IsDBConfigDataExist(_ApiSysDB))
            {
                strErrMsg = string.Format("[{0}] Not in DB.Config !", _ApiSysDB);
            }
            else
            {
                Dictionary<string, string> oProp = Util.getCustProperty(_ApiSysDB, strCtrlName.ToUpper(), "Controller", "Setting"); //預防 DB 區分大小寫，固定轉大寫才比對
                oResult = oProp.IsNullOrEmpty("Prop1") ? false : (oProp["Prop1"].ToUpper() == "Y") ? true : false;                  //Prop1 => Controller 是否啟用(Y/N)

                if (!oResult)
                {
                    if (oProp.IsNullOrEmpty("Prop1"))
                    {
                        strErrMsg = string.Format("[{0}] Controller's Setting Not Found !", strCtrlName);
                    }
                }
                else
                {
                    strErrMsg = "";
                    string[] validIPList = oProp["PropJSON"].Split(','); //PropJSON => Controller 有效的來源 IP 清單 ([*] 代表全部允許)
                    if (validIPList.IsNullOrEmpty())
                    {
                        oResult = false;
                        strErrMsg = string.Format("[{0}] Controller does not define a valid ip list !", strCtrlName);
                    }
                    else
                    {
                        if (validIPList[0] != "*")
                        {
                            //若非允許所有 IP ，則需作 Client IP 檢查
                            string strClientIP = Util.getClientIPv4();
                            if (!validIPList.Contains(strClientIP))
                            {
                                oResult = false;
                                strErrMsg = string.Format("[{0}] is not a valid IP !", strClientIP);
                            }
                        }
                    }
                }  //oResult
            }  //IsDBConfigDataExist
        } //strCtrlName.IsNullOrEmpty

        return oResult;
    }

    /// <summary>
    /// 取得 Api 執行結果
    /// </summary>
    /// <param name="apiUrl"></param>
    /// <param name="apiMethod"></param>
    /// <param name="apiContent"></param>
    /// <param name="apiHeaders"></param>
    /// <returns></returns>
    public static ApiResultModel getApiResult(string apiUrl, HttpMethod apiMethod, Dictionary<string, string> apiFormBody = null, Dictionary<string, string> apiHeaders = null)
    {
        //2017.03.29
        if (string.IsNullOrEmpty(apiUrl) || apiMethod == null)
        {
            throw new Exception(string.Format(RS.Resources.Msg_ParaNotFoundList, "apiUrl or apiMethod")); //參數錯誤
        }

        HttpClient client = new HttpClient();
        HttpResponseMessage resp = new HttpResponseMessage();
        HttpContent content = apiFormBody.IsNullOrEmpty() ? null : new FormUrlEncodedContent(apiFormBody);

        if (!apiHeaders.IsNullOrEmpty())
        {
            //自訂 HttpHeader ，例：.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.46 Safari/536.5")
            foreach (var pair in apiHeaders)
            {
                client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
        }

        if (apiMethod == HttpMethod.Get)
        {
            resp = client.GetAsync(apiUrl).Result;
        }

        if (apiMethod == HttpMethod.Post)
        {
            resp = client.PostAsync(apiUrl, content).Result;
        }

        if (apiMethod == HttpMethod.Put)
        {
            resp = client.PutAsync(apiUrl, content).Result;
        }

        if (apiMethod == HttpMethod.Delete)
        {
            resp = client.DeleteAsync(apiUrl).Result;
        }

        return resp.Content.ReadAsAsync<ApiResultModel>().Result;
    }

}

/// <summary>
/// 自訂 Api 回傳執行結果物件
/// </summary>
public class ApiResultModel
{
    public HttpStatusCode Status { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}

/// <summary>
/// 自訂 Api 檢核
/// </summary>
public class ApiValidController : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        string strCtrlName = request.GetRouteData().Values["controller"].ToString();
        string strErrMsg = "";
        if (!ApiHelper.IsValidController(strCtrlName, out strErrMsg))
        {
            ApiResultModel oResult = new ApiResultModel();
            oResult.Status = HttpStatusCode.Forbidden;
            oResult.ErrorMessage = strErrMsg;
            var oTask = new TaskCompletionSource<HttpResponseMessage>();
            oTask.SetResult(request.CreateResponse(oResult.Status, oResult));
            return oTask.Task;
        }
        return base.SendAsync(request, cancellationToken);
    }
}

/// <summary>
/// 自訂 Api 執行結果
/// </summary>
public class ApiResultAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
        // 例外事件不在此處理
        if (actionExecutedContext.Exception != null)
        {
            return;
        }

        base.OnActionExecuted(actionExecutedContext);
        ApiResultModel oResult = new ApiResultModel();

        object oActContent = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
        if (oActContent.GetType().Equals(typeof(ApiResultModel)))
        {
            //若 ActionContext 傳回結果已是[ApiResultModel]，則直接傳回 2017.03.27
            oResult = (ApiResultModel)oActContent;
            if (oResult.Status == 0)
            {
                //若未指定 StatusCode ，預設為 [HttpStatusCode.OK (200)]
                oResult.Status = HttpStatusCode.OK;
            }
        }
        else
        {
            oResult.Status = actionExecutedContext.ActionContext.Response.StatusCode;                             // 取得 API 返回的狀態碼
            oResult.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;     // 取得 API 返回的資料
            if (Util._IsLocalDebugMode) 
            {
                oResult.ErrorMessage = "** [Local Debug Mode] **"; //提示目前為 「本機開發除錯模式」
            }
        }

        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(oResult.Status, oResult);   // 封裝回傳格式
    }
}

/// <summary>
/// 自訂 Api 例外處理
/// </summary>
public class ApiErrorHandleAttribute : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
        //處理例外事件
        base.OnException(actionExecutedContext);
        string errorMessage = actionExecutedContext.Exception.Message; // 取得錯誤訊息

        ApiResultModel oResult = new ApiResultModel();
        oResult.Status = HttpStatusCode.BadRequest;
        oResult.ErrorMessage = errorMessage;

        // 記錄 Log
        string strLogMsg = string.Format("Web API [{0}], Status:[{1}({2})], ErrorMessage:[{3}]", actionExecutedContext.Request.RequestUri.AbsolutePath, oResult.Status, (int)oResult.Status, oResult.ErrorMessage);
        LogHelper.WriteSysLog(strLogMsg);

        // 封裝回傳格式
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(oResult.Status, oResult);
    }
}
