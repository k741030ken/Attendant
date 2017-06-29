using System;
using System.Text;
using System.Web;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [檔案上傳]公用程式Lite
/// </summary>
public partial class Util_UploadLite : BasePage
{
    /// <summary>
    /// 傳遞 Session 的PK
    /// </summary>
    public string _GUID
    {
        get
        {
            if (PageViewState["_GUID"] == null)
            {
                PageViewState["_GUID"] = "";
            }
            return (string)(PageViewState["_GUID"]);
        }
        set
        {
            PageViewState["_GUID"] = value;
        }
    }

    /// <summary>
    /// 檔案上傳數量
    /// </summary>
    public int _UploadFileMaxQty
    {
        get
        {
            if (PageViewState["_UploadFileMaxQty"] == null)
            {
                PageViewState["_UploadFileMaxQty"] = 1;
            }
            return (int)(PageViewState["_UploadFileMaxQty"]);
        }
        set
        {
            PageViewState["_UploadFileMaxQty"] = value;
        }
    }

    /// <summary>
    /// 檔案限制大小(預設 1024KB)
    /// </summary>
    public int _UploadFileMaxKB
    {
        get
        {
            if (PageViewState["_UploadFileMaxKB"] == null)
            {
                PageViewState["_UploadFileMaxKB"] = 1024;
            }
            return (int)(PageViewState["_UploadFileMaxKB"]);
        }
        set
        {
            PageViewState["_UploadFileMaxKB"] = value;
        }
    }

    /// <summary>
    /// 允許上傳的副檔名(ex: PDF,DOC,ZIP)
    /// </summary>
    public string _UploadFileExtList
    {
        get
        {
            if (PageViewState["_UploadFileExtList"] == null)
            {
                PageViewState["_UploadFileExtList"] = "";
            }
            return HttpUtility.HtmlEncode((string)(PageViewState["_UploadFileExtList"]));
        }
        set
        {
            PageViewState["_UploadFileExtList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //2017.01.20 新增可自訂上傳成功時的顯示訊息
        string strIsDoneMsg = Util.getRequestQueryStringKey("IsDoneMsg", RS.Resources.Upload_Complete_Close);

        //初始變數
        _GUID = Util.getRequestQueryStringKey("GUID");
        _UploadFileExtList = Util.getRequestQueryStringKey("UploadFileExtList", "", true);
        _UploadFileMaxKB = int.Parse(Util.getRequestQueryStringKey("UploadFileMaxKB", "1024"));

        if (Util.getRequestQueryStringKey("IsDone", "N", true) == "Y")
        {
            if (Session["FileError_" + _GUID] != null && !string.IsNullOrEmpty(Session["FileError_" + _GUID].ToString()))
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, Session["FileError_" + _GUID].ToString());
                Session["FileError_" + _GUID] = null;
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, strIsDoneMsg);
            }
            DivMsg.Visible = true;
            DivUploadObj.Visible = false;
        }
        else
        {
            DivMsg.Visible = false;
            DivUploadObj.Visible = true;

            RefreshUploadInfo();
        }

        File1.UploadedComplete += File1_UploadedComplete;
    }

    /// <summary>
    /// 初始上傳物件
    /// </summary>
    protected void RefreshUploadInfo()
    {
        File1.ToolTip = RS.Resources.Attach_buttonText;
        imgUpload.Style["padding-left"] = "20px;";
        StringBuilder sb = new StringBuilder();
        sb.Append("function UploadStart(sender, args) { \n"
            + "  var ErrMsg = ''; \n"
            + "  var allowExtList = '" + _UploadFileExtList.ToUpper() + "'; \n"
            //檢查檔案類型
            + "  if (allowExtList.length > 0) \n"
            + "  { \n"
            + "    var filename = args.get_fileName(); \n"
            + "    var filext = filename.substring(filename.lastIndexOf('.') + 1).toUpperCase(); \n"
            + "    if (allowExtList.indexOf(filext, 0 ) < 0) \n"
            + "    {  ErrMsg = ErrMsg + '\\n" + string.Format(RS.Resources.Attach_FileType1, _UploadFileExtList.ToUpper()) + "'; } \n"
            + "  } \n"
            //檢查檔案大小 [localhost] ONLY
            + "  if (sender._inputFile.files != undefined && sender._inputFile.files[0].size > " + (_UploadFileMaxKB * 1024) + ") \n"
            + "  {  \n"
            + "     if (ErrMsg != '') { ErrMsg = ErrMsg + '\\n'; } \n"
            + "     ErrMsg = ErrMsg + '" + string.Format(RS.Resources.Attach_MaxSize1, _UploadFileMaxKB) + "'; \n "
            + "  }  \n"
            //處理錯誤訊息
            + "  if (ErrMsg != '') \n"
            + "  {  var err = new Error(); err.message = ErrMsg; throw (err); return false; } \n"
            + "  else \n"
            + "  { \n"
            + "    document.getElementById('" + File1.ClientID + "').style.display = 'none';"
            + "    return true; \n"
            + "  } \n"
            + "} \n\n"

            + "function UploadEnd(sender, args) { \n"
            + "   window.location.href = window.location.href + '&IsDone=Y'; \n" //2017.01.20 移除 unescape(window.location.href)
            + "} \n"

            );

        Util.setJSContent(sb.ToString(), "InitUpload");

        labFileMaxQty.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_MaxNumber, _UploadFileMaxQty); //數量上限
        labFileSizeInfo.Text = string.Format("{0}：<Font color='red'> {1:N0} KB</Font>", RS.Resources.Attach_MaxSize, _UploadFileMaxKB); //大小限制
        labFileExtNameInfo.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_FileType, string.IsNullOrEmpty(_UploadFileExtList) ? "*.*" : _UploadFileExtList.ToUpper()); //限定類型
    }

    /// <summary>
    /// 檔案上傳後的處理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void File1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        //使用 Session 進行傳遞
        if (Session["FileName_" + _GUID] != null) Session["FileName_" + _GUID] = null;
        if (Session["FileBody_" + _GUID] != null) Session["FileBody_" + _GUID] = null;
        if (Session["FileError_" + _GUID] != null) Session["FileError_" + _GUID] = null;

        if (File1.HasFile && !string.IsNullOrEmpty(_GUID))
        {
            if (File1.PostedFile.ContentLength > (_UploadFileMaxKB * 1024))
            {
                Session["FileError_" + _GUID] = string.Format(RS.Resources.Attach_SizeExceedLimit, (File1.PostedFile.ContentLength / 1024) + " KB");
                return;
            }
            Session["FileName_" + _GUID] = File1.FileName;
            Session["FileBody_" + _GUID] = File1.FileBytes;
        }
        else
        {
            Session["FileError_" + _GUID] = RS.Resources.Msg_AttachNotFound;
        }

    }


}