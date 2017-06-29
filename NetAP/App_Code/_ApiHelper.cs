using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// Web API 輔助類別
/// <para>** 委派時強制進行 IsValidController() 檢核 **</para>
/// <para>** 執行結果強制以 ApiResultModel 物件架構傳回 JSON 格式資料 **</para>
/// <para>** 請在 ApiExpress 資料庫的 CustProperty 資料表內，定義各 Controller 專屬設定 **</para>
/// </summary>
public partial class ApiHelper
{
    /// <summary>
    ///  Api 系統資料庫名稱
    /// </summary>
    public static string _ApiSysDB = "ApiExpress";

    /// <summary>
    /// 檢核是否為可合理使用的 Controller
    /// <para>** 當偵測到 [本機開發模式] 時一律放行 **</para>
    /// </summary>
    /// <param name="strCtrlName">Controller 名稱</param>
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
    /// <param name="apiUrl">Api 來源網址</param>
    /// <param name="apiHttpMethod">Api 使用的 HttpMethod</param>
    /// <param name="apiFormBody">Api 的表單內容 (apiHttpMethod = [Post / Put] 時才有作用)</param>
    /// <param name="apiCustRequestHeaders">Api 自訂的 RequestHeader 集合</param>
    /// <returns></returns>
    public static ApiResultModel getApiResult(string apiUrl, HttpMethod apiHttpMethod, Dictionary<string, string> apiFormBody = null, Dictionary<string, string> apiCustRequestHeaders = null)
    {
        //2017.03.29
        if (string.IsNullOrEmpty(apiUrl) || apiHttpMethod == null)
        {
            throw new Exception(string.Format(RS.Resources.Msg_ParaNotFoundList, "apiUrl or apiMethod")); //參數錯誤
        }

        HttpClient client = new HttpClient();
        HttpResponseMessage resp = new HttpResponseMessage();
        HttpContent content = apiFormBody.IsNullOrEmpty() ? null : new FormUrlEncodedContent(apiFormBody);

        if (!apiCustRequestHeaders.IsNullOrEmpty())
        {
            //自訂 HttpHeader ，例：.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.46 Safari/536.5")
            foreach (var pair in apiCustRequestHeaders)
            {
                client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
        }

        if (apiHttpMethod == HttpMethod.Get)
        {
            resp = client.GetAsync(apiUrl).Result;
        }

        if (apiHttpMethod == HttpMethod.Post)
        {
            resp = client.PostAsync(apiUrl, content).Result;
        }

        if (apiHttpMethod == HttpMethod.Put)
        {
            resp = client.PutAsync(apiUrl, content).Result;
        }

        if (apiHttpMethod == HttpMethod.Delete)
        {
            resp = client.DeleteAsync(apiUrl).Result;
        }

        return resp.Content.ReadAsAsync<ApiResultModel>().Result;
    }

    /// <summary>
    /// 取得 Api 執行結果
    /// </summary>
    /// <param name="apiUrl">Api 來源網址</param>
    /// <param name="apiHttpMethod">Api 使用的 HttpMethod</param>
    /// <param name="apiFormJSON">Api 的表單 JSON 內容 (apiHttpMethod = [Post / Put] 時才有作用)</param>
    /// <param name="apiCustRequestHeaders">Api 自訂的 RequestHeader 集合</param>
    /// <returns></returns>
    public static ApiResultModel getApiResult(string apiUrl, HttpMethod apiHttpMethod, string apiFormJSON = null, Dictionary<string, string> apiCustRequestHeaders = null)
    {
        //2017.04.14
        if (string.IsNullOrEmpty(apiUrl) || apiHttpMethod == null)
        {
            throw new Exception(string.Format(RS.Resources.Msg_ParaNotFoundList, "apiUrl or apiMethod")); //參數錯誤
        }

        HttpClient client = new HttpClient();
        HttpResponseMessage resp = new HttpResponseMessage();
        HttpContent content = string.IsNullOrEmpty(apiFormJSON) ? null : new StringContent(apiFormJSON, Encoding.UTF8, "application/json");

        if (!apiCustRequestHeaders.IsNullOrEmpty())
        {
            //自訂 HttpHeader ，例：.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.46 Safari/536.5")
            foreach (var pair in apiCustRequestHeaders)
            {
                client.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
        }

        if (apiHttpMethod == HttpMethod.Get)
        {
            resp = client.GetAsync(apiUrl).Result;
        }

        if (apiHttpMethod == HttpMethod.Post)
        {
            resp = client.PostAsync(apiUrl, content).Result;
        }

        if (apiHttpMethod == HttpMethod.Put)
        {
            resp = client.PutAsync(apiUrl, content).Result;
        }

        if (apiHttpMethod == HttpMethod.Delete)
        {
            resp = client.DeleteAsync(apiUrl).Result;
        }

        return resp.Content.ReadAsAsync<ApiResultModel>().Result;
    }
}

/// <summary>
/// Api 執行結果物件
/// </summary>
public class ApiResultModel
{
    public HttpStatusCode Status { get; set; }
    public object Data { get; set; }
    public string ErrorMessage { get; set; }
}

/// <summary>
/// Api 自訂委派控制
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
/// Api 執行結果自訂屬性
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
/// Api 例外處理(包含[警告]及[錯誤] )
/// </summary>
public class ApiErrorHandleAttribute : ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext actionExecutedContext)
    {
        base.OnException(actionExecutedContext);

        //是否為警告
        bool IsWarning = (actionExecutedContext.Exception.GetType() == typeof(WarningException)) ? true : false;

        //初始回傳物件
        ApiResultModel oResult = new ApiResultModel();
        oResult.Status = IsWarning ? HttpStatusCode.Accepted : HttpStatusCode.BadRequest; //根據[警告][錯誤]例外，傳回不同 HttpStatusCode 2017.04.12
        oResult.ErrorMessage = actionExecutedContext.Exception.Message;

        // 記錄 Log
        string strLogMsg = string.Format("Web API [{0}], Status:[{1}({2})], ErrorMessage:[{3}]", actionExecutedContext.Request.RequestUri.AbsolutePath, oResult.Status, (int)oResult.Status, oResult.ErrorMessage);
        LogHelper.WriteSysLog(strLogMsg);

        // 封裝回傳格式
        actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(oResult.Status, oResult);
    }
}
