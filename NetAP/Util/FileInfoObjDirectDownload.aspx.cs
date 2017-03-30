using System;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Util_FileInfoObjDirectDownload : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //直接下載 FileInfoObj 物件並自動清除 2016.11.18
        FileInfoObj oFile = FileInfoObj.getFileInfoObj();
        if (oFile != null && oFile.FileSize > 0)
        {
            string strFileNme = oFile.FileName;
            byte[] binFileBody = oFile.FileBody;

            FileInfoObj.Clear();
            Util.ExportBinary(binFileBody, strFileNme);
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, RS.Resources.Msg_ParaError);
        }
    }
}