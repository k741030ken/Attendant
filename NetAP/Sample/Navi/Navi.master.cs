using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

public partial class Sample_Navi_Navi : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgLogo.ImageUrl = Util._ImagePath + "LogoCloud.png";
        UserInfo oUser = UserInfo.getUserInfo();
        labUserInfo.Text = string.Format("{0}-{1}-{2}-IP:{3}", oUser.CompName, oUser.UserName, oUser.UserID, oUser.ClientIP);  //永豐銀行-王大明-103980-IP:10.11.10.30 │

        if (!IsPostBack)
        {
            //功能選單 [ucMenuBar]
            ucMenuBar.ucDBName = "NetSample";
            ucMenuBar.ucRootNodeID = "P0000059-Root";
            ucMenuBar.ucMainMenuBorderWidth = 0; //由 CloudUtil.css 控制

            //樹狀選單 [ucTreeView]
            ucTreeView1.ucDBName = "NetSample";
            ucTreeView1.ucRootNodeID = "P0000059-Root";
            ucTreeView1.ucNodeDefaultImageUrl = Util.Icon_User;
            ucTreeView1.ucIsShowLines = true;
            ucTreeView1.ucIsHideRootNode = false;
            ucTreeView1.Refresh();
        }
    }
}
