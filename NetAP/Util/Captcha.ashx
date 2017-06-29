<%@ WebHandler Language="C#" Class="Captcha" %>
 
using System;
using System.Collections.Generic;
using System.Web;
using System.Drawing;
using SinoPac.WebExpress.Common;

/// <summary>
/// 產生 Captcha 驗證圖形碼
/// <para>可隨機產生指定字符及數量的動態驗證圖形</para>
/// <para>URL參數：</para>
/// <para>[ChkSeed]   選擇性參數，指定可用來產生圖形碼的字符(預設: 2345689ABCDEFGHJKLMNPRSTWXY )</para>
/// <para>[ChkLen]    選擇性參數，圖形碼位數，預設為 4位</para>
/// <para>[Width]     選擇性參數，圖形碼的圖片寬度像素，預設80</para>
/// <para>[Height]    選擇性參數，圖形碼的圖片高度像素，預設24</para>
/// <para>[BackColor] 選擇性參數，圖形碼背景色，預設 FFFFFF</para>
/// <para>[ForeColor] 選擇性參數，圖形碼前景色，預設 777777</para>
/// <para>[DotColor]  選擇性參數，圖形碼雜點色，預設 777777</para>
/// <para>[DotQty]    選擇性參數，圖形碼雜點數量，預設 200</para>
/// <para>** 圖形碼產生時，會將驗證值自動存入 Session["Captcha"]方便後續驗證 **</para>
/// </summary>
public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        #region 初始變數
        Dictionary<string, string> oRequest = Util.getRequestQueryString();
        //可用的驗證字符(預設剔除易混淆字符)
        string strChkSeed = oRequest.IsNullOrEmpty("ChkSeed") ? "2345689ABCDEFGHJKLMNPRSTWXY" : oRequest["ChkSeed"];
        //驗證碼長度(預設 6)
        int intChkLen = oRequest.IsNullOrEmpty("ChkLen") ? 6 : int.Parse(oRequest["ChkLen"]);
        //驗證圖寬度像素 (預設每字 20)
        int imgWidth = oRequest.IsNullOrEmpty("Width") ? 20 * intChkLen : int.Parse(oRequest["Width"]);
        //驗證圖高度像素(預設 24)
        int imgHeight = oRequest.IsNullOrEmpty("Height") ? 24 : int.Parse(oRequest["Height"]);
        //驗證圖背景色(預設 #FFFFFF)
        Color backColor = ColorTranslator.FromHtml("#" + (oRequest.IsNullOrEmpty("BackColor") ? "FFFFFF" : oRequest["BackColor"]));
        //驗證圖前景色(預設 #777777)
        Brush foreColor = new SolidBrush(ColorTranslator.FromHtml("#" + (oRequest.IsNullOrEmpty("ForeColor") ? "777777" : oRequest["ForeColor"])));
        //驗證圖雜點色(預設 #777777)
        Color dotColor = ColorTranslator.FromHtml("#" + (oRequest.IsNullOrEmpty("DotColor") ? "777777" : oRequest["DotColor"]));
        //驗證圖雜點數量(預設 200)
        int dotQty = oRequest.IsNullOrEmpty("DotQty") ? 200 : int.Parse(oRequest["DotQty"]);
        //驗證圖字型大小(自動計算)
        int imgFontSize = (imgWidth / intChkLen) - 4;
        #endregion


        //產生隨機驗證碼        
        string strChkCode = Util.getRandomCode(intChkLen, strChkSeed);
        //將驗證碼存到 Session ，方便應用系統後續比對
        context.Session["Captcha"] = strChkCode;

        //將驗證碼轉換成圖形
        Bitmap bitmap = new Bitmap(imgWidth, imgHeight);
        Graphics graphics = Graphics.FromImage(bitmap);
        Font font = new Font("Arial", imgFontSize, System.Drawing.FontStyle.Italic);
        graphics.Clear(backColor);
        graphics.DrawString(strChkCode, font, foreColor, 0, 0);

        //加入雜點增加辨識難度 2017.04.07 改用 Util.getRandomNum()
        int[] posX = Util.getRandomNum(0, imgWidth, dotQty);
        int[] posY = Util.getRandomNum(0, imgHeight, dotQty);
        for (int i = 0; i < dotQty; i++)
        {
            bitmap.SetPixel(posX[i], posY[i], dotColor);
        }

        //輸出圖形
        context.Response.Clear();
        context.Response.ContentType = "image/jpeg";
        context.Response.BufferOutput = true;
        bitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        
        
        bitmap.Dispose();
        font.Dispose();
        foreColor.Dispose();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}