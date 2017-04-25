using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Template_DownloadReportTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
}