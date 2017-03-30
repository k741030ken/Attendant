using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// === Web API 範例 ===
/// <summary>
/// 取出指定的 UserInfo 物件
/// </summary>
[AllowAnonymous]
public class TemplateController : ApiController
{
    //允許執行此 Api 的 IP 位址清單，若為 [*] 則代表所有IP皆允許
    //** 建議將 IP 清單存放在資料庫內再取用 **

    // GET api/<controller>
    [AllowAnonymous]
    public string Get()
    {
        string strMsg = "Usage : [/api/<controller>/<id>]";
        return strMsg;
    }

    // GET api/<controller>/<id>
    [AllowAnonymous]
    public string Get(string id)
    {
        return id;
    }
}
