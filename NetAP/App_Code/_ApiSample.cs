using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

// === Web API 範例 ===
/// <summary>
/// 取出指定的 UserInfo 物件
/// </summary>
public class UserInfoController : ApiController
{
    //允許執行此 Api 的 IP 位址清單，若為 [*] 則代表所有IP皆允許
    //** 建議將 IP 清單存放在資料庫內再取用 **
    string[] apiValidIpList = new string[] { "*" };

    // GET api/<controller>
    public string Get()
    {
        if (!ApiHelper.IsValidRequestIP(apiValidIpList))
        {
            throw new Exception(RS.Resources.Msg_NotAuthorizedIpAddress); //未授權的 IP 位址！
        }
        else
        {
            string strMsg = "Usage : [/api/<controller>/<id>]";
            return strMsg;
        }
    }

    // GET api/<controller>/<id>
    public UserInfo Get(string id)
    {
        if (!ApiHelper.IsValidRequestIP(apiValidIpList))
        {
            throw new Exception(RS.Resources.Msg_NotAuthorizedIpAddress); //未授權的 IP 位址！
        }
        else
        {
            if (!Regex.IsMatch(id, "^.{6}$"))
            {
                throw new Exception(string.Format(RS.Resources.Msg_NotValidLength1, 6)); //長度需為 6 字元
            }
            else
            {
                id = id.ToUpper(); //轉成大寫才比對
                UserInfo oUser = UserInfo.findUser(id);
                if (oUser != null && !string.IsNullOrEmpty(oUser.UserID))
                    return oUser;
                else
                    throw new Exception(string.Format(RS.Resources.Msg_DataNotFound1, id)); //無 [id] 相關資料！            
            }
        }
    }
}
