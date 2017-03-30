using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

/// <summary>
/// TemplateWebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class TemplateWebService : System.Web.Services.WebService {

    public TemplateWebService () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession=true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public void Test(string Test01, string Test02)
    {
        var returnCode = "";
        var returnMessage = "";
        var returnData = new RetureModel();
        try
        {
            var getData = new RequestModel();
            //Get use QueryString
            if (string.IsNullOrEmpty(Test01)
                && string.IsNullOrEmpty(Test02))
            {
                returnCode = "01";
                throw new Exception("No Data !!");
            }
            else
            {
                if (!string.IsNullOrEmpty(Test01))
                {
                    getData.Test01 = Test01;
                }

                if (!string.IsNullOrEmpty(Test02))
                {
                    getData.Test02 = Test02;
                }
            }
            returnCode = "00";
            returnMessage = "Success";
            returnData = new RetureModel() { Test01 = getData.Test01, Test02 = getData.Test02 };
        }
        catch (Exception ex)
        {
            returnMessage = ex.Message;
        }

        var jsonData = new ResponseModel()
        {
            ReturnCode = returnCode,
            ReturnMessage = returnMessage,
            ReturnData = returnData
        };
        SetResponse(Context, jsonData);
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class RequestModel
    {
        public string Test01 { get; set; }
        public string Test02 { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class ResponseModel
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public RetureModel ReturnData { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的資料
    /// </summary>
    private class RetureModel
    {
        public string Test01 { get; set; }
        public string Test02 { get; set; }
    }

    /// <summary>
    /// 設定Response
    /// </summary>
    /// <param name="context">HttpResponse</param>
    /// <param name="jsonData">ReturnJsonModel</param>
    private static void SetResponse(HttpContext context, ResponseModel jsonData)
    {
        string jsonCallback = context.Request.QueryString["jsoncallback"];

        if (string.IsNullOrEmpty(jsonCallback))
        {
            jsonCallback = context.Request.QueryString["callback"];
        }

        context.Response.ContentType = "application/json";
        context.Response.Charset = "utf-8";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        context.Response.Write(string.Format("{0}({1});", jsonCallback, serializer.Serialize(jsonData)));
    }
    
}
