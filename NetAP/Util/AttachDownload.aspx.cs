using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using SinoPac.WebExpress.Common;

/// <summary>
/// [附件下載]公用程式
/// </summary>
public partial class Util_AttachDownload : BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string strAttachDB = Util.getRequestQueryStringKey("AttachDB");
        string strAttachID = Util.getRequestQueryStringKey("AttachID");
        int intWidth = 0;
        int intHeight = 0;

        if (!Util.getRequestQueryString().IsNullOrEmpty("Width")) intWidth = int.Parse(Util.getRequestQueryString()["Width"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("W")) intWidth = int.Parse(Util.getRequestQueryString()["W"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("Height")) intHeight = int.Parse(Util.getRequestQueryString()["Height"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("H")) intHeight = int.Parse(Util.getRequestQueryString()["H"].ToString());

        if (string.IsNullOrEmpty(strAttachDB) || string.IsNullOrEmpty(strAttachID))
        {
            DivNormal.Visible = false;
            DivError.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Parameter Error <ul><li>Require：<ul><li>AttachDB<li>AttachID</ul>");
        }
        else
        {
            ucAttachList1.ucAttachDB = strAttachDB;
            ucAttachList1.ucAttachID = strAttachID;
            ucAttachList1.ucIsEditMode = false;

            if (intWidth > 0)
            {
                ucAttachList1.ucWidth = intWidth;
            }

            if (intHeight > 0)
            {
                ucAttachList1.ucHeight = intHeight;
            }
        }

    }
}