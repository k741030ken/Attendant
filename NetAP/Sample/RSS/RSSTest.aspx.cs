using System;
using SinoPac.WebExpress.Common;

public partial class RSSTest : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strRSS1 = "http://weather.yahooapis.com/forecastrss?w=2306179&u=c"; //Yahoo天氣
        string strRSS2 = "http://udn.com/udnrss/latest.xml"; //udn 新聞

        xmlRSS1.XPathNavigator = Util.getRSSNavigator(strRSS1);
        xmlRSS1.TransformSource = "RSSFormat.xsl"; //XML 顯示範本

        xmlRSS2.XPathNavigator = Util.getRSSNavigator(strRSS2);
        xmlRSS2.TransformSource = "RSSFormat.xsl"; //XML 顯示範本
    }
}