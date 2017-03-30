﻿<%@ WebHandler Language="C#" Class="UploadifyHandler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.SessionState;
using SinoPac.WebExpress.Common;

/// <summary>
///　Uploadify　元件後台處理機制 Andrew 2014.01.20
/// </summary>
public class UploadifyHandler : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        //由於uploadify的flash是採用utf-8的編碼方式，所以上傳頁面也要用utf-8編碼，才能正常上傳中文檔名的文件
        context.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
        context.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
        context.Response.Charset = "UTF-8";

        HttpPostedFile objFile = context.Request.Files["Filedata"];
        string strGUID = Util.getRequestFormKey("GUID");
        
        if (objFile != null && !string.IsNullOrEmpty(strGUID))
        {
            if (context.Session["FileName_" + strGUID] != null) context.Session["FileName_" + strGUID] = null;
            if (context.Session["FileBody_" + strGUID] != null) context.Session["FileBody_" + strGUID] = null;
            //處理要附檔名稱及大小
            string[] arFileName = objFile.FileName.Split('.');
            string strFileExtName = arFileName[arFileName.Count() - 1];
            string strFileName = objFile.FileName.Replace("." + strFileExtName, "");
            if (strFileName.Length > 30) strFileName = strFileName.Substring(0, 30);
            strFileName = string.Format("{0}.{1}", strFileName, strFileExtName);
            int intFileSize = objFile.ContentLength;

            //處理附檔的二進位資料
            byte[] oBuffer = new byte[objFile.InputStream.Length];
            objFile.InputStream.Read(oBuffer, 0, objFile.ContentLength);
            
            //設定為Session以便傳遞
            context.Session["FileName_" + strGUID] = strFileName;
            context.Session["FileBody_" + strGUID] = oBuffer;
            context.Response.Write("1");

        }
        else
        {
            context.Response.Write("0");
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