using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

public partial class Default : BasePage
{
    string strHomeUrl = Util.getAppSetting("app://HomeURL/");
    protected void Page_Load(object sender, EventArgs e)
    {
        ucPageInfo.ucIsShowApplication = false;
        ucPageInfo.ucIsShowEnvironmentInfo = true;
        ucPageInfo.ucIsShowQueryString = false;
        ucPageInfo.ucIsShowRequestForm = false;
        ucPageInfo.ucIsShowSession = true;
        ucPageInfo.Visible = false;

        labMsg.Text = "";
        divLogin.Visible = false;
        btnSessClear.Visible = false;
        string IsDebug = Util.getRequestQueryStringKey("IsDebug", "N",true);

        if (!IsPostBack)
        {
            ddlUICulture.DataSource = Util.getDictionary(Util.getUICultureMap());
            ddlUICulture.DataValueField = "Key";
            ddlUICulture.DataTextField = "Value";
            ddlUICulture.SelectedValue = Util.getUICultureCode();
            ddlUICulture.DataBind();
        }

        UserInfo oUser = UserInfo.getUserInfo();
        if (oUser != null && !string.IsNullOrEmpty(oUser.UserID))
        {
            if (!string.IsNullOrEmpty(strHomeUrl) && !strHomeUrl.Contains("Default.aspx"))
            {
                if (IsDebug == "Y")
                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "<a href='" + strHomeUrl + "'>Home Page</a><br/>");
                else
                    Response.Redirect(strHomeUrl);
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "Logged!");
                ucPageInfo.Visible = true;
                ucPageInfo.Refresh();
            }
            if (Util.getServerIPv4() == Util.getClientIPv4()) { divLogin.Visible = true; btnSessClear.Visible = true; }
        }
        else
        {
            if (!System.Web.Security.FormsAuthentication.LoginUrl.Contains("Default.aspx"))
            {
                Response.Redirect(System.Web.Security.FormsAuthentication.LoginUrl);
            }
            else
            {
                if (Util.getServerIPv4() != Util.getClientIPv4()) { labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Please log in using SSO"); }
                else
                {
                    divLogin.Visible = true;
                    txtUserID.ucIsFocus = true;
                }
            }
        }
    }
    protected void btn1_Click(object sender, EventArgs e) { divLogin.Visible = true; }
    protected void btnSessClear_Click(object sender, EventArgs e) { Session.Clear(); Response.Redirect(strHomeUrl); }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        string strUserID = txtUserID.ucTextData;
        if (!string.IsNullOrEmpty(strUserID))
        {
            if (UserInfo.Init(strUserID, true))
            {
                if (!string.IsNullOrEmpty(ddlUICulture.SelectedValue)) { UserInfo.setUserUICulture(ddlUICulture.SelectedValue); }
                System.Web.Security.FormsAuthentication.SetAuthCookie(strUserID, false);
                string strRetUrl = Util.getRequestQueryStringKey("ReturnUrl");

                if (string.IsNullOrEmpty(strRetUrl))
                {
                    if (!string.IsNullOrEmpty(strHomeUrl) && !strHomeUrl.Contains("Default.aspx"))
                    {
                        Response.Redirect(Util.getFixURL(strHomeUrl));
                    }
                    ucPageInfo.Visible = true;
                    ucPageInfo.Refresh();
                    if (Util.getServerIPv4() == Util.getClientIPv4()) { divLogin.Visible = true; btnSessClear.Visible = true; }
                }
                else
                {
                    Response.Redirect(Util.getFixURL(strRetUrl));
                }
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Login Fail!");
            }
        }
    }
}