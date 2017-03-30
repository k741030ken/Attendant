using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SinoPac.WebExpress.Common;

public partial class Sample_Navi_NaviMaster : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //仿製頁籤
            tabDiv1.Attributes["class"] = "tab-caption-active";
            //動作按鈕列 [ucActionBar]
            ucActionBar.ucQueryEnabled = true;
            ucActionBar.ucAddEnabled = true;
            ucActionBar.ucEditEnabled = true;
            ucActionBar.ucDeleteEnabled = true;
            ucActionBar.ucCopyEnabled = true;
            ucActionBar.ucExportEnabled = true;
            //ucActionBar.ucDownloadEnabled = true;
            //ucActionBar.ucInformationEnabled = true;
            //ucActionBar.ucMultilingualEnabled = true;
            //ucActionBar.ucPrintEnabled = true;

            //資料清單
            Dictionary<string, string> oDisp = new Dictionary<string, string>();
            oDisp.Add("TabName", "資料表");
            oDisp.Add("FldName", "欄位");
            oDisp.Add("Value", "值");
            oDisp.Add("Description", "說明");

            ucGridView1.ucDBName = "NetSample";
            ucGridView1.ucDataQrySQL = "Select * From CodeMap";
            ucGridView1.ucDataKeyList = "TabName,FldName,Value".Split(',');
            ucGridView1.ucDataDisplayDefinition = oDisp;
            ucGridView1.ucCheckEnabled = true;
            ucGridView1.ucSelectEnabled = true;
            ucGridView1.ucExportOpenXmlEnabled = true;
            ucGridView1.Refresh(true);
        }
        ucActionBar.BarCommand += ucActionBar_BarCommand;
        ucGridView1.RowCommand += ucGridView1_RowCommand;
    }

    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        Util.NotifyMsg(string.Format("資料鍵值為 [{0}] ", Util.getStringJoin(e.DataKeys)),Util.NotifyKind.Success);
    }

    void ucActionBar_BarCommand(object sender, Util_ucActionBar.BarCommandEventArgs e)
    {
        Util.NotifyMsg(string.Format("按下 [{0}] 按鈕", e.CommandName));
    }

    protected void tabBtn_Click(object sender, EventArgs e)
    {
        //仿製頁籤
        LinkButton oBtn = (LinkButton)sender;
        HtmlGenericControl oDiv;
        for (int i = 1; i <= 7; i++)
        {
            oDiv = (HtmlGenericControl)Util.FindControlEx(this.Page, ("tabDiv" + i));
            if (oDiv != null)
            {
                oDiv.Attributes["class"] = "tab-caption";
            }
        }
        oDiv = (HtmlGenericControl)Util.FindControlEx(this.Page, ("tabDiv" + oBtn.ID.Right(1)));
        oDiv.Attributes["class"] = "tab-caption-active";
        Util.NotifyMsg(string.Format("選擇了 [{0}]", oBtn.ID));
    }
}