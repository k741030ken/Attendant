<%@ WebHandler Language="C#" Class="UrlDownload" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using SinoPac.WebExpress.Common;

/// <summary>
/// 將 URL 網址強迫下載成檔案，[URL]參數為必要項目，[FileName]參數則可有可無，若無會自動從[URL]解析
/// **[FileName]應為完整檔名(ex: Apple.jpg)，若不給副檔名(ex: Apple)，程式也會自動嘗試從[URL]解析
/// </summary>
public class UrlDownload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //嘗試取得參數內容
        string strURL = Util.getRequestQueryStringKey("URL");
        string strFileName = Util.getRequestQueryStringKey("FileName");

        if (string.IsNullOrEmpty(strURL))
        {
            context.Response.Write("Need Parameter , ex: URL , [FileName]");
        }
        else
        {
            if (string.IsNullOrEmpty(strFileName))
            {
                //若未定義 [FileName] 參數
                int intPos = strURL.LastIndexOf('/') + 1;
                strFileName = strURL.Substring(intPos, strURL.Length - intPos);
            }
            else
            {
                //若 [FileName] 參數不含副檔名
                int intPos = strURL.LastIndexOf('.') + 1;
                if (intPos > 0) strFileName += "." + strURL.Substring(intPos, strURL.Length - intPos);
            }

            try
            {
                WebResponse wRS = WebRequest.Create(strURL).GetResponse();
                int length = (int)wRS.ContentLength;
                if (length > 0)
                {
                    BinaryReader br = new BinaryReader(wRS.GetResponseStream());
                    byte[] buffers = br.ReadBytes(length);
                    Util.ExportBinary(buffers, strFileName);
                }
                else
                {
                    context.Response.Write(string.Format("[{0}] length is empty !", strURL));
                }
            }
            catch (WebException ex)
            {
                context.Response.Write(ex.Message);
            }
            finally
            {

            }

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}