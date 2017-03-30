using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class AclExpress_Admin_Default : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labFrameSetError.Text = RS.Resources.Msg_NotSupportFrameSet; //不支援框架顯示
    }
}