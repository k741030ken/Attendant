using System;
using System.Web.Security;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [檔案上傳]公用程式
/// </summary>
public partial class Util_Upload : BasePage
{
    private string _GUID = "";
    private int _UploadFileMaxQty = 1;
    private int _UploadFileMaxKB = 1024;
    private string _UploadFileExtList = ""; //ex: PDF,DOC,ZIP

    protected override void OnInitComplete(EventArgs e)
    {
        //關閉 PageIsMetaTags，原因如 Page_Load 所述
        PageIsMetaTags = false;
        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //2017.01.20 新增可自訂上傳成功時的顯示訊息
        string strIsDoneMsg = Util.getRequestQueryStringKey("IsDoneMsg", RS.Resources.Upload_Complete_Close);

        //2015.07.14 新增，若 IE 版本小於 8 ，或 IsLite=Y 則使用 Lite 版
        string strLiteURL = Request.Url.AbsoluteUri;
        strLiteURL = strLiteURL.Replace(".aspx", "Lite.aspx");
        if ((Util.getRequestQueryStringKey("IsLite", "N", true) == "Y") || (Util.getIEVersion() > 0 && Util.getIEVersion() < 8))
        {
            Response.Redirect(strLiteURL);
            return;
        }

        //2017.03.13 改用框架內建 jQuery 
        Util.setJS(Util._JSjQueryUrl);

        //2015.07.16 調整，若未安裝 Flash Player 則使用 Lite 版
        Util.setJS_ChkFlashPlayer("", strLiteURL);

        //獨立設定MetaTag，以免NetAP被套在 IE 相容模式 Frameset 時，檔案無法上傳
        System.Web.UI.HtmlControls.HtmlMeta oMeta = new System.Web.UI.HtmlControls.HtmlMeta();
        oMeta.HttpEquiv = "X-UA-Compatible";
        oMeta.Content = "IE=edge";
        this.Page.Header.Controls.Add(oMeta);

        //初始變數
        _GUID = Util.getRequestQueryStringKey("GUID");
        _UploadFileExtList = Util.getRequestQueryStringKey("UploadFileExtList","",true);
        _UploadFileMaxKB = int.Parse(Util.getRequestQueryStringKey("UploadFileMaxKB", "1024"));

        if (Util.getRequestQueryStringKey("IsDone","N",true) == "Y")
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, strIsDoneMsg);
            DivMsg.Visible = true;
            DivUploadObj.Visible = false;
        }
        else
        {
            DivMsg.Visible = false;
            DivUploadObj.Visible = true;
            RefreshUploadInfo();
        }
    }

    /// <summary>
    /// 處理上傳物件
    /// </summary>
    protected void RefreshUploadInfo()
    {
        //初始上傳元件
        //2017.03.08 加入多語系判斷
        switch (Util.getUICultureCode())
        {
            case "zh-TW":
            case "zh-CHT":
            case "zh-Hant":
                Util.setJS("./Uploadify/jquery.uploadify.min.cht.js", "Uploadify_Init");
                break;
            case "zh-CN":
            case "zh-CHS":
            case "zh-Hans":
                Util.setJS("./Uploadify/jquery.uploadify.min.chs.js", "Uploadify_Init");
                break;
            default:
                Util.setJS("./Uploadify/jquery.uploadify.min.js", "Uploadify_Init");
                break;
        }

        //fix [__flash__removeCallback] error when use IE8 ~ 11   Patched by Andrew 2014.05.31
        string strJS = "";
        strJS += "$(window).unload(function () {";
        strJS += "    if ($('#file_upload').length > 0) { $('#file_upload').uploadify('destroy'); }";
        strJS += "});";
        Util.setJSContent(strJS, "Uploadify_unload");

        strJS += "$(document).ready(function () {";
        strJS += "  $('#file_upload').uploadify({";
        strJS += "  'debug': false";
        strJS += " ,'queueID': 'fileQueue'";
        strJS += " ,'swf': './Uploadify/uploadify.swf'";
        strJS += " ,'uploader': './Uploadify/UploadifyToSession.ashx'"; //Server端處理常式
        strJS += " ,'height': 25";                       //上傳按鈕高度
        strJS += " ,'buttonClass': 'uploadify-button'";  //上傳按鈕CSS
        strJS += string.Format(" ,'buttonText': '{0}'", RS.Resources.Attach_buttonText);    //上傳按鈕文字
        strJS += " ,'progressData': 'percentage'";       // 上傳進度顯示方式[percentage][speed]
        strJS += " ,'removeCompleted': true";            //上傳完成後自動刪除隊列
        strJS += " ,'removeTimeout':  10";               //等待 N 秒後刪除隊列
        strJS += " ,'successTimeout': 10";               //上傳完畢後等待Server回應的秒數，若逾時則視為上傳成功
        strJS += " ,'auto': true";                       //選擇檔案後自動上傳
        strJS += " ,'multi': false";                     //可一次上傳多檔案
        strJS += "  ,'uploadLimit': 1";                  //可上傳檔案總數
        strJS += string.Format(" ,'fileSizeLimit': '{0}KB'", _UploadFileMaxKB);  //檔案大小限制 (KB,MB,GB)，若為 0 則不限制
        //strJS +=" ,'fileTypeDesc': 'Allow File Type'";
        if (string.IsNullOrEmpty(_UploadFileExtList))
        {
            strJS += " ,'fileTypeExts': '*.*'";
        }
        else
        {
            //'fileTypeExts': '*.zip; *.gif; *.jpg; *.png',
            strJS += string.Format(" ,'fileTypeExts': '*.{0}'", _UploadFileExtList.ToLower().Replace(",", ";*."));
        }

        //傳遞額外參數給上傳表單
        strJS += " ,'formData': { 'GUID':'" + _GUID + "'";

        //Fix Flash Player BUG for FireFox / MS-Edge 2017.03.13
        //加上專門參數，以便讓 Global.asax 內的相關函數作判斷
        strJS += ",'ASPSESSID':'" + Session.SessionID + "'";
        strJS += ",'AUTHID':'" + (Request.Cookies[FormsAuthentication.FormsCookieName] == null ? "" : Request.Cookies[FormsAuthentication.FormsCookieName].Value) + "'";

        strJS += "}";

        //上傳作業進行時，將上傳按鈕 Disable
        strJS += " ,'onDialogClose'  : function(queueData) {";
        strJS += " if (queueData.filesQueued > 0) $('#file_upload').uploadify('disable', true);";
        strJS += "}";

        //上傳作業完畢時，重新整理網頁
        strJS += " ,'onQueueComplete': function () {";
        strJS += "     window.location.href = window.location.href + '&IsDone=Y';";  //2017.01.20 移除 unescape(window.location.href)
        strJS += "     }";

        strJS += " });"; // uploadify()

        //fix IE9 Bug
        strJS += "if ($.browser.msie) {$('.swfupload').attr('classid','clsid:D27CDB6E-AE6D-11cf-96B8-444553540000');}";

        strJS += "});";  // ready()
        Util.setJSContent(strJS, "Uploadify");

        //處理訊息顯示
        labFileMaxQty.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_MaxNumber, _UploadFileMaxQty); //數量上限
        labFileSizeInfo.Text = string.Format("{0}：<Font color='red'> {1:N0} KB</Font>", RS.Resources.Attach_MaxSize, _UploadFileMaxKB); //大小限制
        labFileExtNameInfo.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_FileType, string.IsNullOrEmpty(_UploadFileExtList) ? "*.*" : _UploadFileExtList.ToUpper()); //限定類型
    }
}