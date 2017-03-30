using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using SinoPac.WebExpress.Common;

/// <summary>
/// [頁面資訊顯示]控制項
/// </summary>
public partial class Util_ucPageInfo : BaseUserControl
{
    /// <summary>
    /// 是否顯示執行環境資訊
    /// </summary>
    public bool ucIsShowEnvironmentInfo { get { return _IsShowEnvironmentInfo; } set { _IsShowEnvironmentInfo = value; } }

    /// <summary>
    /// 是否顯示Request.QueryString內容
    /// </summary>
    public bool ucIsShowQueryString { get { return _IsShowQueryString; } set { _IsShowQueryString = value; } }

    /// <summary>
    /// 是否顯示Request.Form內容
    /// </summary>
    public bool ucIsShowRequestForm { get { return _IsShowRequestForm; } set { _IsShowRequestForm = value; } }

    /// <summary>
    /// 是否顯示Session內容
    /// </summary>
    public bool ucIsShowSession { get { return _IsShowSession; } set { _IsShowSession = value; } }

    /// <summary>
    /// 是否顯示目前Application內容
    /// </summary>
    public bool ucIsShowApplication { get { return _IsShowApplication; } set { _IsShowApplication = value; } }

    //顯示與否切換開關
    private bool _IsShowEnvironmentInfo = true;
    private bool _IsShowQueryString = true;
    private bool _IsShowRequestForm = true;
    private bool _IsShowSession = true;
    private bool _IsShowApplication = false;

    //文化特性
    private string _Culture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
    private string _ParentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Parent.ToString();
    private string _UICulture = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
    private string _UIParentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Parent.ToString();
    //顯示顏色定義
    private string _txtColor = "#000000";
    private string _valColor = "#A31515";
    private string _objColor = "#174BAF";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Visible)
        {
            Refresh();
        }
    }

    public void Refresh()
    {
        //BrowserInfo
        if (_IsShowEnvironmentInfo)
        {
            DivBrowserArea.Visible = true;
            ltlEnvironment.Text = "";

            ltlEnvironment.Text += "　　<Span class='Util_Legend'>IP Info</Span>";
            ltlEnvironment.Text += "<ul>";
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Server IPv4", Util.getServerIPv4(), _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Client IPv4", Util.getClientIPv4(), _txtColor, _valColor);
            ltlEnvironment.Text += "</ul>";

            HttpBrowserCapabilities obj = Request.Browser;
            ltlEnvironment.Text += "　　<Span class='Util_Legend'>Browser Info</Span>";
            ltlEnvironment.Text += "<ul>";
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.Name", obj.Browser, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.Type", obj.Type, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.Version", obj.Version, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.MajorVersion", obj.MajorVersion, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.MinorVersion", obj.MinorVersion, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.Platform", obj.Platform, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Frames", obj.Frames, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Tables", obj.Tables, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.Cookies", obj.Cookies, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Browser.EcmaScriptVersion", obj.EcmaScriptVersion, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Java Applets", obj.JavaApplets, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Beta", obj.Beta, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Crawler", obj.Crawler, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "AOL", obj.AOL, _txtColor, _valColor);
            //ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "CDF", obj.CDF, _txtColor, _valColor);
            ltlEnvironment.Text += "</ul>";

            ltlEnvironment.Text += "　　<Span class='Util_Legend'>OS & Culture Info</Span>";
            ltlEnvironment.Text += "<ul>";
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Request.UserAgent", Request.UserAgent, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "Request.UserLanguages", string.Join(",", Request.UserLanguages), _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "CurrentThread.Culture", _Culture, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "CurrentThread.Culture.Parent", _ParentCulture, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "CurrentThread.UICulture", _UICulture, _txtColor, _valColor);
            ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", "CurrentThread.UICulture.Parent", _UIParentCulture, _txtColor, _valColor);
            ltlEnvironment.Text += "</ul>";

            // AppSetting Info 2016.12.28

            ltlEnvironment.Text += string.Format("　　<Span class='Util_Legend'>AppSetting Info [{0}]</Span>", ConfigurationManager.AppSettings.Count);
            ltlEnvironment.Text += "<ul>";
            if (ConfigurationManager.AppSettings.Count > 0)
            {
                for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
                {
                    ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", ConfigurationManager.AppSettings.Keys[i], ConfigurationManager.AppSettings[i], _txtColor, _valColor);
                }
            }
            ltlEnvironment.Text += "</ul>";

            // Assembly Info  2016.07.26
            Dictionary<string, string> dicAsm = Util.getAssemblyDictionary();
            ltlEnvironment.Text += string.Format("　　<Span class='Util_Legend'>Assembly Info [{0}]</Span>", (dicAsm != null) ? dicAsm.Count : 0);
            ltlEnvironment.Text += "<ul>";
            if (dicAsm != null && dicAsm.Count > 0)
            {
                foreach (var pair in dicAsm)
                {
                    ltlEnvironment.Text += string.Format("<li><font color='{2}'>{0} <font color='{3}'>[{1}]</font></font><br/>", pair.Key, pair.Value, _txtColor, _valColor);
                }
            }
            ltlEnvironment.Text += "</ul>";

        }

        //QueryString
        if (_IsShowQueryString)
        {
            DivQueryArea.Visible = true;
            labQueryString.Text = string.Format("Request.QueryString [{0}]", Request.QueryString.Count);
            ltlQueryString.Text = "";
            for (int i = 0; i < Request.QueryString.Count; i++)
            {
                string tKey = Request.QueryString.Keys[i];
                var tValue = Request.QueryString[i];
                ltlQueryString.Text += string.Format("<li><font color='{2}'>{0} = <font color='{3}'>[{1}]</font></font><br/>", tKey, tValue.ToString(), _txtColor, _valColor);
            }
            ltlQueryString.Text = string.Format("<ul>{0}</ul>", ltlQueryString.Text);
        }
        //RequestForm
        if (_IsShowRequestForm)
        {
            DivFormArea.Visible = true;
            labQueryForm.Text = string.Format("Request.Form [{0}]", Request.Form.Count);
            ltlQueryForm.Text = "";
            for (int i = 0; i < Request.Form.Count; i++)
            {
                string tKey = Request.Form.Keys[i];
                var tValue = Request.Form[i];
                ltlQueryForm.Text += Util.getObjectInfo(tKey, tValue, _txtColor, _valColor, _objColor);
            }
            ltlQueryForm.Text = string.Format("<ul>{0}</ul>", ltlQueryForm.Text);
        }
        //Session
        if (_IsShowSession)
        {
            DivSessionArea.Visible = true;
            labSession.Text = string.Format("Session [{0}]", Session.Count);
            ltlSession.Text = "";
            for (int i = 0; i < Session.Count; i++)
            {
                string tKey = Session.Keys[i];
                var tValue = Session[i];
                ltlSession.Text += Util.getObjectInfo(tKey, tValue, _txtColor, _valColor, _objColor);
            }
            ltlSession.Text = string.Format("<ul>{0}</ul>", ltlSession.Text);
        }
        //Application
        if (_IsShowApplication)
        {
            DivApplicationArea.Visible = true;
            labApplication.Text = string.Format("Application [{0}]", Application.Count);
            ltlApplication.Text = "";
            for (int i = 0; i < Application.Count; i++)
            {
                string tKey = Application.Keys[i];
                var tValue = Application[i];
                ltlApplication.Text += Util.getObjectInfo(tKey, tValue, _txtColor, _valColor, _objColor);
            }
            ltlApplication.Text = string.Format("<ul>{0}</ul>", ltlApplication.Text);
        }
    }
}