using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Sample_Captcha_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divCaptcha.Visible = true;
            divVerify.Visible = false;
            int intChkLen = 6; //圖形碼長度
            txtChkCode.ucMaxLength = intChkLen;
            txtChkCode.ucWidth = intChkLen * 10;

            imgCaptcha.ImageUrl = string.Format("{0}?ChkLen={1}&Dummy={2}", Util._CaptchaUrl, intChkLen, new Random((int)DateTime.Now.Ticks).Next(10000, 99999));
            imgCaptcha.ToolTip = RS.Resources.Msg_Refresh;
            imgCaptcha.CssClass = "Util_Pointer";
            //重新產生圖形碼 JS
            string strJS = string.Format("var oImg = document.getElementById('{0}');oImg.src='{1}?ChkLen={2}&Dummy=' + Util_GetRandom(10000,99999);", imgCaptcha.ClientID, Util._CaptchaUrl, intChkLen);
            imgCaptcha.Attributes.Add("onclick", strJS);
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        divCaptcha.Visible = false;
        divVerify.Visible = true;
        if (!string.IsNullOrEmpty(txtChkCode.ucTextData) && Session["Captcha"] != null)
        {
            if (txtChkCode.ucTextData.ToUpper() == Session["Captcha"].ToString().ToUpper()) //不分大小寫
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "驗證碼輸入正確");
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "驗證碼輸入錯誤");
            }
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "驗證失敗");
        }
    }
}