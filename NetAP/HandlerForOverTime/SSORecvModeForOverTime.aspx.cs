using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SinoPac.WebExpress.Common;
using System.Net;
using System.IO;

public partial class SSORecvModeForOverTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        var canRedirectTxmIDs = new List<string>()
        {
            "OV4200","OV4201","OV4202"
        };
        var canRedirectUrl = new List<string>()
        {
            "/Util/AttachAdmin.aspx"
        };
        try
        {
            if (Request.QueryString["UserID"] != null
                && Request.QueryString["ReturnUrl"] != null
                && Request.QueryString["SystemID"] != null
                && Request.QueryString["TxnID"] != null)
            {
                //context.Request.QueryString["OTEmpID"]
                var strUserID = Request.QueryString["UserID"].ToString();
                var strRetUrl = Request.QueryString["ReturnUrl"].ToString();
                var strSystemID = Request.QueryString["SystemID"].ToString();
                var strTxnID = Request.QueryString["TxnID"].ToString();
                if (!canRedirectTxmIDs.Contains(strTxnID))
                {
                    throw new Exception("無開放" + strTxnID + "使用!!");
                }
                var checkBl = false;
                foreach (var st in canRedirectUrl)
                {
                    if (strRetUrl.StartsWith(st))
                    {
                        checkBl = true;
                        break;
                    }
                }
                if (!checkBl)
                {
                    throw new Exception("無開放此跳轉路徑:" + strRetUrl);
                }
                if (!UserInfo.Init(strUserID, true))
                {
                    throw new Exception(Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Login Fail!"));                 
                }

                System.Web.Security.FormsAuthentication.SetAuthCookie(strUserID, false);
                Response.Redirect(Util.getFixURL(strRetUrl));
            }
            else
            {
                throw new Exception("[UserID/ReturnUrl/SystemID/TxnID]參數不足！");
            }
        }
        catch (Exception ex)
        {
            labMsg.Text = "SSORecvModeForOverTime:" + ex.Message;
        }
        

    }
}