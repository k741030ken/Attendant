using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

public partial class Sample_NaviTest : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //功能選單列 [ucMenuBar]
            ucMenuBar.ucDBName = "NetSample";
            ucMenuBar.ucRootNodeID = "P0000059-Root";

            //樹狀選單 [ucTreeView]
            ucTreeView1.ucDBName = "NetSample";
            ucTreeView1.ucRootNodeID = "P0000059-Root";
            ucTreeView1.ucNodeDefaultImageUrl = Util.Icon_User;
            ucTreeView1.ucIsShowLines = true;
            ucTreeView1.ucIsHideRootNode = false;

            //動作按鈕列 [ucActionBar]
            ucActionBar.ucBtnCssClass = "Util_clsBtnGray Util_Pointer";
            ucActionBar.ucQueryEnabled = true;
            ucActionBar.ucAddEnabled = true;
            ucActionBar.ucEditEnabled = true;
            ucActionBar.ucDeleteEnabled = true;
            ucActionBar.ucCopyEnabled = true;
            ucActionBar.ucExportEnabled = true;

            //資料清單
            Dictionary<string, string> oDisp = new Dictionary<string, string>();
            oDisp.Add("TabName", "資料表");
            oDisp.Add("FldName", "欄位");
            oDisp.Add("Value", "值");
            oDisp.Add("Description", "說明");

            ucGridView.ucDBName = "NetSample";
            ucGridView.ucDataQrySQL = "Select * From CodeMap ";
            ucGridView.ucDataDisplayDefinition = oDisp;
            ucGridView.ucDataKeyList = "TabName,FldName,Value".Split(',');
            ucGridView.ucSelectEnabled = true;
            ucGridView.ucExportOpenXmlEnabled = true;

            ucGridView.Refresh(true);
        }
        ucTreeView1.Refresh();

        ucActionBar.BarCommand += ucActionBar_BarCommand;
        ucGridView.RowCommand += ucGridView_RowCommand;
    }

    void ucGridView_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        Util.NotifyMsg(string.Format("資料鍵值為 [{0}] ", Util.getStringJoin(e.DataKeys)), Util.NotifyKind.Success);
    }

    void ucActionBar_BarCommand(object sender, Util_ucActionBar.BarCommandEventArgs e)
    {
        Util.NotifyMsg(string.Format("按下 [{0}] 按鈕", e.CommandName));
    }
}