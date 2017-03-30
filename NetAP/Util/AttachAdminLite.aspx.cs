using System;
using System.Text;
using System.Web.Security;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [附件管理]公用程式 Lite
/// </summary>
/// <remarks>2015.07.14 新增</remarks>
public partial class Util_AttachAdminLite : SecurePage
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

    private int _AttachCurrQty = 0;     //目前已上傳數量
    private int _AttachCurrTotKB = 0;   //目前已使用配額

    //自訂[附件清單]的 Width/Height 功能
    private int intWidth = 0;
    private int intHeight = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
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

        File1.UploadedComplete += File1_UploadedComplete;
        ucAttachList1.AttachDeleted += ucAttachList1_AttachDeleted;
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

        labErrMsg.Text = "";
        File1.ToolTip = RS.Resources.Attach_buttonText;
        imgUpload.Style["padding-left"] = "20px;";
        imgUpload.Style["padding-right"] = "100px;";
        StringBuilder sb = new StringBuilder();
        sb.Append("function UploadStart(sender, args) { \n"
            + "  var ErrMsg = ''; \n"
            + "  var allowExtList = '" + _AttachFileExtList.ToUpper() + "'; \n"
            //檢查檔案類型
            + "  if (allowExtList.length > 0) \n"
            + "  { \n"
            + "    var filename = args.get_fileName(); \n"
            + "    var filext = filename.substring(filename.lastIndexOf('.') + 1).toUpperCase(); \n"
            + "    if (allowExtList.indexOf(filext, 0 ) < 0) \n"
            + "    {  ErrMsg = ErrMsg + '\\n" + string.Format(RS.Resources.Attach_FileType1, _AttachFileExtList.ToUpper()) + "'; } \n"
            + "  } \n"
            //檢查檔案大小 [localhost] ONLY
            + "  if (sender._inputFile.files != undefined && sender._inputFile.files[0].size > " + (_AttachFileMaxKB * 1024) + ") \n"
            + "  {  \n"
            + "     if (ErrMsg != '') { ErrMsg = ErrMsg + '\\n'; } \n"
            + "     ErrMsg = ErrMsg + '" + string.Format(RS.Resources.Attach_MaxSize1, _AttachFileMaxKB) + "'; \n "
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
            + "   window.location.href = window.location.href; \n" //2017.01.20 移除 unescape(window.location.href)
            + "} \n"

            );

        Util.setJSContent(sb.ToString(), "InitUpload");

        DivUploadObj.Style["display"] = "";
        DivError.Style["display"] = "none";
        labFileMaxQty.Text = string.Format("{0}：<Font color='red'> {1}</Font>", RS.Resources.Attach_MaxNumber, _AttachFileMaxQty); //數量上限
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
    /// 檔案上傳後的處理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void File1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (File1.HasFile)
        {
            if (File1.PostedFile.ContentLength <= (_AttachFileMaxKB * 1024))
            {
                Util.IsAttachInserted(_AttachDB, _AttachID, File1.FileName, File1.FileBytes, _AnonymousYN.ToUpper() == "Y"?true:false);
            }
        }
    }


    /// <summary>
    /// 檔案刪除事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucAttachList1_AttachDeleted(object sender, Util_ucAttachList.AttachDeletedEventArgs e)
    {
        string strJS = "";
        strJS += "     window.location.href = window.location.href;"; //2017.01.20 移除 unescape(window.location.href)
        Util.setJSContent(strJS);
    }
}