using System;
using System.Web.Security;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [附件管理]公用程式
/// </summary>
public partial class Util_AttachAdmin : SecurePage
{
    private string _AttachDB = "";
    private string _AttachID = "";
    private string _AttachFileExtList = ""; //ex: PDF,DOC,ZIP
    private string _AnonymousYN = "N";

    private int _AttachFileDefKB = 1024;
    private int _AttachFileDefMaxQty = 1;

    private int _AttachFileMaxQty = 1;
    private int _AttachFileMaxKB = 0;
    private int _AttachFileTotKB = 0;

    private int _AttachCurrQty = 0;     //目前已上傳數量 2014.06.23新增
    private int _AttachCurrTotKB = 0;   //目前已使用配額 2014.06.23新增

    //2014.09.25 新增自訂[附件清單]的 Width/Height 功能
    private int intWidth = 0;
    private int intHeight = 0;

    protected override void OnInitComplete(EventArgs e)
    {
        //關閉 PageIsMetaTags，原因如 Page_Load 所述
        PageIsMetaTags = false;
        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
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

        //獨立設定MetaTag，不套用SecurePage的P3P設定，以免NetAP被套在ePortal的相容模式Frame時，檔案無法上傳 2015.01.06
        System.Web.UI.HtmlControls.HtmlMeta oMeta = new System.Web.UI.HtmlControls.HtmlMeta();
        oMeta.HttpEquiv = "X-UA-Compatible";
        oMeta.Content = "IE=edge";
        this.Page.Header.Controls.Add(oMeta);

        //訂閱 uc 的事件
        ucAttachList1.AttachDeleted += new Util_ucAttachList.AttachDeletedClick(Event_AttachDeleted);

        //初始變數
        _AttachDB = Util.getRequestQueryStringKey("AttachDB");
        _AttachID = Util.getRequestQueryStringKey("AttachID");
        _AttachFileExtList = Util.getRequestQueryStringKey("AttachFileExtList", "", true);
        _AnonymousYN = Util.getRequestQueryStringKey("AnonymousYN", "N", true);

        if (string.IsNullOrEmpty(_AttachDB) || string.IsNullOrEmpty(_AttachID))
        {
            DivUploadObj.Style["display"] = "none";
            DivListArea.Style["display"] = "none";
            DivError.Style["display"] = "";
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "Parameter Error <ul><li>Require：<ul><li>AttachDB<li>AttachID</ul><li>Optional：<ul><li>AttachFileTotKB  []<li>AttachFileMaxQty　[1]<li>AttachFileMaxKB　[1024]<li>AttachFileExtList　[]<li>AnonymousYN　[N]</ul></ul>");
        }

        if (!IsPostBack)
        {
            RefreshUploadInfo();
        }
    }

    /// <summary>
    /// 處理上傳元件
    /// </summary>
    protected void RefreshUploadInfo()
    {
        if (!Util.getRequestQueryString().IsNullOrEmpty("Width")) intWidth = int.Parse(Util.getRequestQueryString()["Width"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("W")) intWidth = int.Parse(Util.getRequestQueryString()["W"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("Height")) intHeight = int.Parse(Util.getRequestQueryString()["Height"].ToString());
        if (!Util.getRequestQueryString().IsNullOrEmpty("H")) intHeight = int.Parse(Util.getRequestQueryString()["H"].ToString());

        //計算目前　總配額/已用配額/單檔大小的相互關係
        _AttachCurrQty = Util.getAttachIDEffectQty(_AttachDB, _AttachID);
        _AttachCurrTotKB = Util.getAttachIDEffectFileTotKB(_AttachDB, _AttachID);

        _AttachFileMaxQty = int.Parse(Util.getRequestQueryStringKey("AttachFileMaxQty", "0"));
        _AttachFileMaxKB = int.Parse(Util.getRequestQueryStringKey("AttachFileMaxKB", "0"));
        _AttachFileTotKB = int.Parse(Util.getRequestQueryStringKey("AttachFileTotKB", "0"));

        if (_AttachFileMaxQty <= 0) _AttachFileMaxQty = _AttachFileDefMaxQty;
        if (_AttachFileMaxKB <= 0) _AttachFileMaxKB = _AttachFileDefKB;
        if (_AttachFileTotKB <= 0) _AttachFileTotKB = _AttachFileMaxQty * _AttachFileMaxKB;

        //若剩餘配額小於單檔最大容量，則自動限縮
        if ((_AttachFileTotKB - _AttachCurrTotKB) < _AttachFileMaxKB)
        {
            _AttachFileMaxKB = _AttachFileTotKB - _AttachCurrTotKB;
        }

        //設定 ucAttachList1 屬性
        ucAttachList1.ucAttachDB = _AttachDB;
        ucAttachList1.ucAttachID = _AttachID;
        ucAttachList1.ucIsEditMode = true;

        if (intWidth > 0)
        {
            tabUpload.Style["width"] = string.Format("{0}px", intWidth);
            ucAttachList1.ucWidth = intWidth;
        }

        if (intHeight > 0)
        {
            ucAttachList1.ucHeight = intHeight;
        }


        //檔案數量已達上限
        if (_AttachFileMaxQty <= _AttachCurrQty)
        {
            DivUploadObj.Style["display"] = "none";
            DivError.Style["display"] = "";
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Attach_NumberExceedLimit, _AttachFileMaxQty));
            return;
        }

        //檔案配額已達上限
        if (_AttachFileTotKB <= _AttachCurrTotKB)
        {
            DivUploadObj.Style["display"] = "none";
            DivError.Style["display"] = "";
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Attach_TotSizeExceedLimit, _AttachFileTotKB)); //檔案數量( {0} )已達上限！
            return;
        }

        //初始 Uploadify 元件　2014.10.22調整
        labErrMsg.Text = "";
        string strJS = "";

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
        strJS += "$(window).unload(function () {";
        strJS += "    if ($('#file_upload').length > 0) { $('#file_upload').uploadify('destroy'); }";
        strJS += "});";
        Util.setJSContent(strJS, "Uploadify_unload");

        strJS = "";
        strJS += "$(document).ready(function () {";
        strJS += "if ($('#file_upload').length > 0) {";
        strJS += "  $('#file_upload').uploadify({";
        strJS += "  'debug': false";
        strJS += " ,'queueID': 'fileQueue'";
        strJS += " ,'swf': './Uploadify/uploadify.swf'";
        strJS += " ,'uploader': './Uploadify/UploadifyToAttachDB.ashx'"; //Server端處理常式
        strJS += " ,'height': 25";                       //上傳按鈕高度
        strJS += " ,'buttonClass': 'uploadify-button'";  //上傳按鈕CSS
        strJS += string.Format(" ,'buttonText': '{0}'", RS.Resources.Attach_buttonText);    //上傳按鈕文字
        strJS += " ,'progressData': 'percentage'";       // 上傳進度顯示方式[percentage][speed]
        strJS += " ,'removeCompleted': true";            //上傳完成後自動刪除隊列
        strJS += " ,'removeTimeout':  10";               //等待 N 秒後刪除隊列
        strJS += " ,'successTimeout': 10";               //上傳完畢後等待Server回應的秒數，若逾時則視為上傳成功
        strJS += " ,'auto': true";                       //選擇檔案後自動上傳

        //可否一次上傳多檔案
        if (_AttachFileMaxQty > 1)
            strJS += " ,'multi': true";
        else
            strJS += " ,'multi': false";

        //貯列檔案總數
        strJS += string.Format(" ,'queueSizeLimit': {0}", _AttachFileMaxQty - _AttachCurrQty);

        strJS += string.Format(" ,'fileSizeLimit': '{0}KB'", _AttachFileMaxKB);  //檔案大小限制 (KB,MB,GB)，若為 0 則不限制

        //上傳檔案類型
        if (string.IsNullOrEmpty(_AttachFileExtList))
        {
            strJS += " ,'fileTypeExts': '*.*'";
        }
        else
        {
            //'fileTypeExts': '*.zip; *.gif; *.jpg; *.png',
            strJS += string.Format(" ,'fileTypeExts': '*.{0}'", _AttachFileExtList.ToLower().Replace(",", ";*."));
        }

        //傳遞額外參數給上傳表單
        strJS += " ,'formData': { 'AttachDB':'" + _AttachDB + "', 'AttachID':'" + _AttachID + "', 'AnonymousAccess':'" + _AnonymousYN + "'";

        //Fix Flash Player BUG for FireFox / MS-Edge 2017.03.13
        //加上專門參數，以便讓 Global.asax 內的相關函數作判斷
        strJS += ",'ASPSESSID':'" + Session.SessionID + "'";
        strJS += ",'AUTHID':'" + (Request.Cookies[FormsAuthentication.FormsCookieName] == null ? "" : Request.Cookies[FormsAuthentication.FormsCookieName].Value) + "'";

        strJS += "}";


        //上傳作業進行時，將上傳按鈕 Disable
        strJS += " ,'onDialogClose'  : function(queueData) {";
        strJS += " if (queueData.filesQueued > 0) $('#file_upload').uploadify('disable', true);";
        strJS += "}";

        //所有檔案都上傳作業完畢時，才重新整理網頁
        strJS += " ,'onQueueComplete': function () {";
        strJS += "     window.location.href = window.location.href;"; //2017.01.20 移除 unescape(window.location.href)
        strJS += "     }";

        strJS += " });"; // uploadify()
        strJS += "}";    // file_upload.length > 0

        //fix IE9 Bug
        strJS += "if ($.browser.msie) {$('.swfupload').attr('classid','clsid:D27CDB6E-AE6D-11cf-96B8-444553540000');}";

        strJS += "});";  // ready()
        Util.setJSContent(strJS, "Uploadify");

        //正常情況
        DivUploadObj.Style["display"] = "";
        DivError.Style["display"] = "none";
        labFileMaxQty.Text = string.Format("{0}：<Font color='red'>  {1:N0} ／ {2:N0}</Font>", RS.Resources.Attach_MaxNumber, _AttachCurrQty, _AttachFileMaxQty); //數量上限
        labFileSizeInfo.Text = string.Format("{0}：<Font color='red'> {1:N0} KB</font>", RS.Resources.Attach_MaxSize, _AttachFileMaxKB); //單檔限制
        labFileTotSizeInfo.Text = string.Format("{0}：<Font color='red'> {1:N0} ／ {2:N0} KB</font>", RS.Resources.Attach_TotSize, _AttachCurrTotKB, _AttachFileTotKB); //總量限制
        //有限定檔案類型
        if (string.IsNullOrEmpty(_AttachFileExtList))
        {
            labFileExtNameInfo.Visible = false;
        }
        else
        {
            labFileExtNameInfo.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_FileType, _AttachFileExtList.ToUpper());
        }
        //顯示使用百分比
        int intCurrStatus = (int)(((float)_AttachCurrTotKB / _AttachFileTotKB) * 100);
        DivQuotaStatus.Style["width"] = string.Format("{0}%", intCurrStatus);
        if (intCurrStatus > 0)
        {
            DivQuotaStatus.Style["background-color"] = "YellowGreen";
            if (intCurrStatus > 30)
            {
                DivQuotaStatus.Style["background-color"] = "LimeGreen";
            }
            if (intCurrStatus > 50)
            {
                DivQuotaStatus.Style["background-color"] = "OrangeRed";
            }
            if (intCurrStatus > 80)
            {
                DivQuotaStatus.Style["background-color"] = "Red";
            }
        }
    }

    /// <summary>
    /// 檔案刪除事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Event_AttachDeleted(object sender, Util_ucAttachList.AttachDeletedEventArgs e)
    {
        //_AttachDB = e.AttachDB;
        //_AttachID = e.AttachID;
        //RefreshUploadInfo();
        string strJS = "";
        strJS += "     window.location.href = window.location.href;";  //2017.01.20 移除 unescape(window.location.href)
        Util.setJSContent(strJS);
    }
}