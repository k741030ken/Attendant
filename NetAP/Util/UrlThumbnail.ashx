<%@ WebHandler Language="C#" Class="UrlThumbnail" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using SinoPac.WebExpress.Common;
/// <summary>
/// 利用圖片 URL 網址，產生指定大小的縮略圖。
/// [URL]參數為必要項目，[Scale][Width][Height]參數則可有可無
/// [Scale]數值則為原圖的比例(譬如 Scale=0.6)
/// [Width][Height]數值為正整數(像素，譬如　Width==96)，可以只給其中一個，程式會自動照比例換算
/// *　[Scale]優先權較[Width][Height]為高，若已有[Scale]參數，則不再判斷[Width][Height]參數　*
/// *　若只有[URL]參數，則自動產生寬度為　96像素　的等比例縮圖
/// </summary>
public class UrlThumbnail : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int defWidth = 96; //縮圖預設寬度
        string[] arExtList = "gif,jpg,png".Split(','); //可接受的圖形類別
                                                       //嘗試取得參數內容
        string strURL = Util.getRequestQueryStringKey("URL");
        int intWidth = int.Parse(Util.getRequestQueryStringKey("Width", "0"));
        int intHeight = int.Parse(Util.getRequestQueryStringKey("Height", "0"));
        decimal decScale = decimal.Parse(Util.getRequestQueryStringKey("Scale", "0"));

        //URL有值，且有副檔名時才處理
        if (!string.IsNullOrEmpty(strURL) && strURL.LastIndexOf('.') > 0)
        {
            int intPos = strURL.LastIndexOf('.') + 1;
            string strExtName = strURL.Substring(intPos, strURL.Length - intPos);
            //需為圖檔才處理
            if (arExtList.Contains(strExtName.ToLower()))
            {
                WebResponse wRS = WebRequest.Create(strURL).GetResponse();
                if (wRS.ContentLength > 0)
                {
                    Bitmap bmpIn = new Bitmap(wRS.GetResponseStream());
                    //Bitmap bmpOut = null;
                    if (decScale != 0)
                    {
                        intWidth = Convert.ToInt32(bmpIn.Width * decScale);
                        intHeight = Convert.ToInt32(bmpIn.Height * decScale);
                    }
                    else
                    {
                        if (intWidth <= 0 && intHeight <= 0)
                        {
                            intWidth = defWidth;
                            intHeight = Convert.ToInt32(bmpIn.Height * (decimal)defWidth / bmpIn.Width);
                        }
                        else
                        {
                            if (intWidth > 0)
                            {
                                if (intHeight <= 0)
                                {
                                    intHeight = Convert.ToInt32(bmpIn.Height * (decimal)intWidth / bmpIn.Width);
                                }
                            }
                            else
                            {
                                intWidth = Convert.ToInt32(bmpIn.Width * (decimal)intHeight / bmpIn.Height);
                            }
                        }
                    }

                    //開始產生縮圖
                    MemoryStream MemStream = new MemoryStream();
                    Bitmap bmpOut = new Bitmap(intWidth, intHeight);

                    //方式一
                    bmpOut = (Bitmap)bmpIn.GetThumbnailImage(intWidth, intHeight, null, new System.IntPtr());
                    bmpOut.Save(MemStream, bmpIn.RawFormat);

                    //方式二
                    //Graphics g = Graphics.FromImage(bmpOut);
                    //g.SmoothingMode = SmoothingMode.Default;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //g.DrawImage(bmpIn, 0, 0, intWidth, intHeight);
                    //bmpIn.Save(MemStream,bmpIn.RawFormat);
                    //bmpIn.Dispose();

                    //將產生結果輸出
                    context.Response.ContentType = string.Format("image/{0}", strExtName);
                    MemStream.WriteTo(HttpContext.Current.Response.OutputStream);

                } //wRS.ContentLength > 0
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