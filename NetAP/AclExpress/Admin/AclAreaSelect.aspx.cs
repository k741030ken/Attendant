using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;

public partial class AclExpress_Admin_AclAreaSelect : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            AclArea.ucSourceDictionary = Util.getDictionary(Util.getDictionary(AclExpress.getAclAreaData().Select(" IsEnabled = 'Y' ").CopyToDataTable()));
            AclArea.Refresh();
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string strUrl = Util.getRequestQueryStringKey("TargetPage", AclExpress._AclSysPath + "AclAuthUserArea.aspx");
        strUrl = string.Format("{0}?AreaID={1}", strUrl, AclArea.ucSelectedID);
        Response.Redirect(strUrl);
    }
}