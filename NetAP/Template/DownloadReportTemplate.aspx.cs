using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Template_DownloadReportTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ReadExcel02_Click(object sender, EventArgs e)
    {
        Template.ReadExcelTest02();
    }

    protected void ReadExcel01_Click(object sender, System.EventArgs e)
    {
        if (File1.PostedFile != null)
        {
            // Get a reference to PostedFile object
            HttpPostedFile myFile = File1.PostedFile;
            Template.ReadExcelTest01(myFile.InputStream);
        }
        else
        {
            // No file
        }
    }

    protected void OpenXmlWordDownload01_Click(object sender, EventArgs e)
    {

    }

    protected void OpenXmlWordDownload02_Click(object sender, EventArgs e)
    {
        var isSuccess = false;
        byte[] fileData = null;
        try
        {
            fileData = Template.MakeCancelCreditAmtNotification();
            if (fileData.Length > 0)
            {
                isSuccess = true;
            }
        }
        catch (Exception ex)
        {
            string errMassge = ex.Message;
        }
        if (isSuccess)
        {
            FileUtility.DownLoadDOCX(Response, fileData, "TestCancelCreditAmtNotice", true);
        }  
    }

    protected void OpenXmlWordDownload03_Click(object sender, EventArgs e)
    {

    }

    protected void DocxWordDownload01_Click(object sender, EventArgs e)
    {
        var isSuccess = false;
        byte[] fileData = null;
        try
        {
            fileData = Template.BookmarksReplaceTextOfBookmarkKeepingFormat();
            if (fileData.Length > 0)
            {
                isSuccess = true;
            }
        }
        catch (Exception ex)
        {
            string errMassge = ex.Message;
        }
        if (isSuccess)
        {
            FileUtility.DownLoadDOCX(Response, fileData, "TestDocumentWithBookmarks.docx", false);
        }        
    }

    protected void DocxWordDownload02_Click(object sender, EventArgs e)
    {
        var isSuccess = false;
        byte[] fileData = null;
        try
        {
            fileData = Template.CreateTableRowsFromTemplate();
            if (fileData.Length > 0)
            {
                isSuccess = true;
            }
        }
        catch (Exception ex)
        {
            string errMassge = ex.Message;
        }
        if (isSuccess)
        {
            FileUtility.DownLoadDOCX(Response, fileData, "TestDocumentWithTemplateTable", true);
        }        
    }

    protected void DocxWordDownload03_Click(object sender, EventArgs e)
    {
        var isSuccess = false;
        byte[] fileData = null;
        try
        {
            fileData = Template.MakeDutyReport001();
            if (fileData.Length > 0)
            {
                isSuccess = true;
            }
        }
        catch (Exception ex)
        {
            string errMassge = ex.Message;
        }
        if (isSuccess)
        {
            FileUtility.DownLoadDOCX(Response, fileData, "測試值勤套表", true);
        }
    }
}