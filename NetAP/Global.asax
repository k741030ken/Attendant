<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Web.Routing" %>
<script RunAt="server">
    void Application_Start(object sender, EventArgs e)
    {
        // === 刪除過時的 ELMAH ErrorLog  === 
        SinoPac.WebExpress.Common.LogHelper.CleanXmlLogFile();

        // === Web API 相關設定 ===
        // Andrew.sun 2016.12.14
        //自訂路由
        RouteTable.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "Api/{controller}/{id}",
              defaults: new { id = System.Web.Http.RouteParameter.Optional }
              );
        //停用 XML，只使用 JSON
        GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        //自訂 JsonFormatter
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
        {
            Formatting = Newtonsoft.Json.Formatting.Indented
        };
        //自訂控制處理 2017.03.30
        GlobalConfiguration.Configuration.MessageHandlers.Add(new ApiValidController());
        //自訂回傳處理
        GlobalConfiguration.Configuration.Filters.Add(new ApiResultAttribute());
        //自訂例外處理
        GlobalConfiguration.Configuration.Filters.Add(new ApiErrorHandleAttribute());
    }

    void Application_BeginRequest(object sender, EventArgs e)
    {
        // === 修正 Flash Player Cookie Bug ===
        try
        {
            string session_param_name = "ASPSESSID";
            string session_cookie_name = "ASP.NET_SESSIONID";

            if (HttpContext.Current.Request.Form[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            {
                UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
            }
        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write("Error Initializing Session");
        }

        try
        {
            string auth_param_name = "AUTHID";
            string auth_cookie_name = FormsAuthentication.FormsCookieName;

            if (HttpContext.Current.Request.Form[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
            }
            else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
            {
                UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
            }

        }
        catch (Exception)
        {
            Response.StatusCode = 500;
            Response.Write("Error Initializing Forms Authentication");
        }
    }

    void UpdateCookie(string cookie_name, string cookie_value)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        if (cookie == null)
        {
            cookie = new HttpCookie(cookie_name);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }
        cookie.Value = cookie_value;
        HttpContext.Current.Request.Cookies.Set(cookie);
    } 
</script>
