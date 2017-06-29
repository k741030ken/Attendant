using System;
using System.Collections.Generic;
using System.IO;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// DocX 套版範例 2017.05.25
/// </summary>
public partial class Sample_DocX_Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnProcess1.onStart += btnProcess1_onStart;
    }

    void btnProcess1_onStart(object sender, EventArgs e)
    {
        //設定替換清單
        Dictionary<string, string> oReplaceList = new Dictionary<string, string>();
        oReplaceList.Add("[$King$]", "柯潔");
        oReplaceList.Add("[$Master$]", "李世乭");

        try
        {
            //讀取套版範本
            //Stream oTemplate = Util.getStream(Util.getAttachFileBody("AttachDBName", "AttachID"));      //從附件資料庫取得版型範本
            Stream oTemplate = File.OpenRead(Server.MapPath("Test.docx"));                               //從檔案系統取得版型範本

            //套印成 DocX
            Stream oOutput = Util.getDocOpenXml(oTemplate, oReplaceList); //從檔案系統取得版型範本

            if (!oOutput.IsNullOrEmpty())
            {
                //檔案匯出
                FileInfoObj.setFileInfoObj(string.Format("Sample_{0}.docx", DateTime.Today.ToString("yyyyMMdd")), Util.getBytes(oOutput));
                FileInfoObj.DirectDownload();
                btnProcess1.Complete(RS.Resources.Msg_ExportDataReadyToDownload);
            }
            else
            {
                btnProcess1.Complete(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error);
            }
        }
        catch
        {
            btnProcess1.Complete(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error);
        }
    }
}