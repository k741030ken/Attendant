using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

//此處不適合繼承 BasePage
public partial class SOFun_SSORecvMode2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        //預設驗證失敗
        bool IsPass = false;
        string strMode2ChkURL = Util.getAppSetting("app://TimeStampChkURL/");
        if (string.IsNullOrEmpty(strMode2ChkURL))
        {
            labMsg.Text = "缺少 [app://TimeStampChkURL/] 參數！";
            return;
        }
        else
        {
            //只接受 Request.Form 參數
            if (Request.Form["UserID"] != null && Request.Form["MD5"] != null && Request.Form["SSOID"] != null && Request.Form["TimeStamp"] != null && Request.Form["Targetpage"] != null)
            {
                //初始變數
                string strUserID = Request.Form["UserID"].ToString();
                string strSSOID = Request.Form["SSOID"].ToString();
                string strMD5 = Request.Form["MD5"].ToString();
                string strTimeStamp = Request.Form["TimeStamp"].ToString();
                string strTargetPage = Request.Form["Targetpage"].ToString();

                //模式2 相關判斷
                strMode2ChkURL = string.Format("{0}?ChkUserID={1}&SSOID={2}&MD5={3}&TimeStamp={4}", strMode2ChkURL, strUserID, strSSOID, strMD5, strTimeStamp);
                string strChkResult = Util.getHtmlContent(strMode2ChkURL);
                if (!string.IsNullOrEmpty(strChkResult))
                {
                    if (strChkResult.Left(1).ToUpper() == "Y")
                    {
                        IsPass = true;
                    }
                    else
                    {
                        labMsg.Text = strChkResult;
                    }
                }

                // ** 若模式2驗證成功，則初始 UserInfo 物件
                // ** 因為UserInfo物件屬性更豐富，故不使用 SSO 傳入參數進行初始 **
                if (IsPass)
                {
                    if (UserInfo.Init(strUserID, true))
                    {
                        //處理顯示語系，可處理 LangCode 或 CultureCode
                        //若 SSO 有傳入指定 LangCode
                        string strLangCode = (Request.Form["LangCode"] != null) ? Request.Form["LangCode"].ToString().Trim() : "";
                        if (!string.IsNullOrEmpty(strLangCode))
                        {
                            UserInfo.setUserLangCode(strLangCode);
                        }
                        //若 SSO 有傳入指定 CultureCode
                        string strCultureCode = (Request.Form["CultureCode"] != null) ? Request.Form["CultureCode"].ToString().Trim() : "";
                        if (!string.IsNullOrEmpty(strCultureCode))
                        {
                            UserInfo.setUserUICulture(strCultureCode);
                        }

                        //將 SSO 傳入的參數儲存到 Session[SSOInfo] 物件備用
                        Dictionary<string, string> dicSSOInfo = new Dictionary<string, string>();
                        foreach (string Name in Request.Form)
                        {
                            dicSSOInfo.Add(Name, Request.Form[Name].Trim());
                        }
                        Session["SSOInfo"] = dicSSOInfo;

                        //登入並轉址
                        System.Web.Security.FormsAuthentication.SetAuthCookie(strUserID, false);
                        Response.Redirect(Util.getFixURL(Util._RootPath + strTargetPage));
                    }
                    else 
                    {
                        labMsg.Text = string.Format(RS.Resources.Msg_InitialFail, "UserInfo");  //2017.04.26 強化除錯資訊
                    }
                }
            }
            else
            {
                labMsg.Text = "SSO [UserID/SSOID/MD5/TimeStamp/Targetpage]參數不足！";
            }
        }
    }
}