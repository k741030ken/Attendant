using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using SinoPac.WebExpress.Common;

/// <summary>
/// TemplateWA 的摘要描述
/// </summary>
public class TemplateController : ApiController
{

    /// <summary>
    /// GetTemplate
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public TemplateWAModel.ResponseModel GetTemplate()
    {
        var returnCode = "";
        var returnMessage = "";
        var returnData = new TemplateWAModel.RetureModel();
        try
        {
            var getData = new TemplateWAModel.RequestModel();
            //Dictionary<string, string> oQry = Util.getRequestQueryString();
            if (string.IsNullOrEmpty(Util.getRequestQueryStringKey("test01"))
                && string.IsNullOrEmpty(Util.getRequestQueryStringKey("test02")))
            {
                returnCode = "01";
                throw new Exception("No Data !!");
            }
            else
            {
                if (!string.IsNullOrEmpty(Util.getRequestQueryStringKey("test01")))
                {
                    getData.Test01 = Util.getRequestQueryStringKey("test01");
                }

                if (!string.IsNullOrEmpty(Util.getRequestQueryStringKey("test02")))
                {
                    getData.Test02 = Util.getRequestQueryStringKey("test02");
                }
            }
            returnCode = "00";
            returnMessage = "Success";
            returnData = new TemplateWAModel.RetureModel() { Test01 = getData.Test01, Test02 = getData.Test02 };
        }
        catch (Exception ex)
        {
            returnMessage = ex.Message;
        }

        var jsonData = new TemplateWAModel.ResponseModel()
        {
            ReturnCode = returnCode,
            ReturnMessage = returnMessage,
            ReturnData = returnData
        };
        return jsonData;
    }   

    /// <summary>
    /// PostTemplate
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public TemplateWAModel.ResponseModel PostTemplate()
    {
        var returnCode = "";
        var returnMessage = "";
        var returnData = new TemplateWAModel.RetureModel();
        try
        {
            var getData = new TemplateWAModel.RequestModel();
            //Dictionary<string, string> oQry = Util.getRequestForm();
            if (string.IsNullOrEmpty(Util.getRequestFormKey("test01"))
                && string.IsNullOrEmpty(Util.getRequestFormKey("test02")))
            {
                returnCode = "01";
                throw new Exception("No Data !!");
            }
            else
            {
                if (!string.IsNullOrEmpty(Util.getRequestFormKey("test01")))
                {
                    getData.Test01 = Util.getRequestFormKey("test01");
                }

                if (!string.IsNullOrEmpty(Util.getRequestFormKey("test02")))
                {
                    getData.Test02 = Util.getRequestFormKey("test02");
                }
            }
            returnCode = "00";
            returnMessage = "Success";
            returnData = new TemplateWAModel.RetureModel() { Test01 = getData.Test01, Test02 = getData.Test02 };
        }
        catch (Exception ex)
        {
            returnMessage = ex.Message;
        }

        var jsonData = new TemplateWAModel.ResponseModel()
        {
            ReturnCode = returnCode,
            ReturnMessage = returnMessage,
            ReturnData = returnData
        };
        return jsonData;
    }

    /// <summary>
    /// PutTemplate
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public bool PutTemplate()
    {
        bool oResult = false;
        return oResult;
    }

    /// <summary>
    /// DeleteTemplate
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public bool DeleteTemplate()
    {
        bool oResult = false;
        return oResult;
    }
}

