using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;

/// <summary>
/// ACL 資訊查詢作業
/// </summary>
public partial class AclExpress_AclInfo : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucCascadingDropDown.SetDefault();
        ucCascadingDropDown.ucIsRequire01 = true;
        ucCascadingDropDown.ucIsRequire02 = true;
        ucCascadingDropDown.ucIsRequire03 = true;
        if (!IsPostBack)
        {
            divQryResult.Visible = false;
            UserInfo oUser = UserInfo.getUserInfo();
            if (oUser != null)
            {
                ucCascadingDropDown.ucDefaultSelectedValue01 = oUser.CompID;
                ucCascadingDropDown.ucDefaultSelectedValue02 = oUser.DeptID;
                ucCascadingDropDown.ucDefaultSelectedValue03 = oUser.UserID;
            }
        }
        Util.setJS_AlertPageNotValid(btnQry.ID);
    }
    protected void btnQry_Click(object sender, EventArgs e)
    {
        divQryResult.Visible = false;
        if (!string.IsNullOrEmpty(ucCascadingDropDown.ucSelectedValue03))
        {
            divQryResult.Visible = true;
            ucAclInfo.ucAclInfo = AclInfo.findAclInfo(ucCascadingDropDown.ucSelectedValue03, true);
            ucAclInfo.Refresh();
        }
    }
}