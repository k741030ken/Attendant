using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using HtmlAgilityPack;
using SinoPac.WebExpress.Common;


public partial class Sample_Yahoo : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //讀取台灣奇摩股市的個股資料 2016.03.23 Andrew
        string strStockCode = "2890";  //永豐金
        string strUrl = "https://tw.stock.yahoo.com/q/q?s=" + strStockCode;
        string strHtml = Util.getHtmlContent(strUrl, 3000, null, null, Encoding.GetEncoding("Big5"));
        if (!string.IsNullOrEmpty(strHtml) && strHtml.IndexOf(strStockCode) > 0) 
        {
            HtmlDocument doc = new HtmlDocument();
            HtmlDocument hdc = new HtmlDocument();
            doc.LoadHtml(strHtml);
            //取出包含個股資訊的<table>物件
            hdc.LoadHtml(doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1]").InnerHtml);
            //取得並解析 個股標題 及 數值，依序為 [股票代號,時間,成交,買進,賣出,漲跌,張數,昨收,開盤,最高,最低,個股資料]
            string[] tmpArray = new string[15];
            string[] txtHeader = new string[11];
            string[] txtData = new string[11];
            tmpArray = hdc.DocumentNode.SelectSingleNode("./tr[1]").InnerText.Trim().Replace("\n\n", "\n").Split('\n');
            Array.ConstrainedCopy(tmpArray, 0, txtHeader, 0, 11);
            tmpArray = hdc.DocumentNode.SelectSingleNode("./tr[2]").InnerText.Trim().Replace("\n\n", "\n").Split('\n');
            Array.ConstrainedCopy(tmpArray, 0, txtData, 0, 11);
            //輸出結果
            labMsg.Text = "";
            for (int i = 0; i < txtHeader.Length; i++) 
            {
                labMsg.Text += string.Format("<li>{0}　=>　[{1}]</li>", txtHeader[i], txtData[i].Trim());
            }
        }
    }
}