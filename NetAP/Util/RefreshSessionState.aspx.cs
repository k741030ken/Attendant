using System;
using SinoPac.WebExpress.Common;

public partial class Util_RefreshSessionState : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //利用 Util.setRefreshSessionFrame() 將此頁以隱藏iFrame方式，放在需預防 Session Timeout 的頁面，自動預防逾時
        //重新整理間隔(預設 900 秒)
        string _GapTIme = Util.getRequestQueryStringKey("GapTime", "900");
        //重新整理次數
        int _Count = int.Parse(Util.getRequestQueryStringKey("Count", "0")) + 1;
        //植入所需Tag
        Util.setHtmlMeta("refresh", string.Format("{0};url=RefreshSessionState.aspx?GapTime={0}&Count={1}", _GapTIme, _Count)); 
        //顯示相關訊息
        labMsg.Text = string.Format("<p><b>Keep Session Alive</b></p>  Gap：{0} (s)<br/>Count：{1}", _GapTIme, _Count);
    }
}