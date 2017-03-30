<%@ WebHandler Language="C#" Class="TemplateHandler" %>

using System;
using System.Text;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.SessionState;

public class TemplateHandler : IHttpHandler,IReadOnlySessionState
{
    /// <summary>
    /// 跨域呼叫ashx測試
    /// </summary>
    /// <param name="context">HttpContext</param>
    public void ProcessRequest (HttpContext context) 
    {
        var returnCode = "";
        var returnMessage = "";
        var returnData = new RetureModel();
        try
        {            
            var getData = new RequestModel();
            //Get use QueryString
            if (string.IsNullOrEmpty(context.Request.QueryString["test01"])
                && string.IsNullOrEmpty(context.Request.QueryString["test02"]))
            {
                //Post use 取得整個Request傳過來的InputStream資料 Post
                string requestJsonData = GetFromInputStream(context);
                if (string.IsNullOrEmpty(requestJsonData))
                {
                    returnCode = "01";
                    throw new Exception("No Data !!");
                }
                else
                {
                    getData = new JavaScriptSerializer().Deserialize<RequestModel>(requestJsonData);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(context.Request.QueryString["test01"]))
                {
                    getData.Test01 = context.Request.QueryString["test01"];
                }

                if (!string.IsNullOrEmpty(context.Request.QueryString["test02"]))
                {
                    getData.Test02 = context.Request.QueryString["test02"];
                }
            }
            returnCode = "00";
            returnMessage = "Success";
            returnData = new RetureModel(){ Test01 = getData.Test01, Test02 = getData.Test02};                       
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
        SetResponse(context, jsonData);
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

    /// <summary>
    /// 將Request.InputStream轉成城可讀字串
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <returns>string</returns>
    private static string GetFromInputStream(HttpContext context)
    {
        var reader = new System.IO.StreamReader(context.Request.InputStream);
        var result = reader.ReadToEnd();

        return result;
    }

    /// <summary>
    /// ashx預設參數
    /// </summary>
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

