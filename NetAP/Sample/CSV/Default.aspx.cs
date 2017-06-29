using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

public partial class Sample_CSV_Default : BasePage
{

    public Dictionary<string, Type> StockInfoColumnDefinition
    {
        //欄位清單：StockNo,StockDate,StockBody,IsRefund
        get
        {
            if (PageViewState["_ColumnDefinition"] == null)
            {
                Dictionary<string, Type> oDic = new Dictionary<string, Type>();
                oDic.Add("StockNo", typeof(int));
                oDic.Add("StockDate", typeof(DateTime));
                oDic.Add("StockBody", typeof(string));
                oDic.Add("IsRefund", typeof(bool));
                PageViewState["_ColumnDefinition"] = oDic;
            }
            return (Dictionary<string, Type>)(PageViewState["_ColumnDefinition"]);
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        btnFreeCsv1.onLaunch += btnUpload_onLaunch;
        btnFreeCsv2.onLaunch += btnUpload_onLaunch;
        btnStockInfo1.onLaunch += btnUpload_onLaunch;
        btnStockInfo1.onLaunch += btnUpload_onLaunch;

        btnFreeCsv1.onClose += btnFreeCsv1_onClose;
        btnFreeCsv2.onClose += btnFreeCsv2_onClose;
        btnStockInfo1.onClose += btnStockInfo1_onClose;
        btnStockInfo2.onClose += btnStockInfo2_onClose;
    }

    void btnUpload_onLaunch(object sender, EventArgs e)
    {
        //上傳前清除之前結果
        divResult.Visible = false;
        divErrInfo.Visible = false;

        labMsg.Text = "";
        gvResult.Reset();
        gvErrInfo.Reset();
    }

    void btnFreeCsv1_onClose(object sender, EventArgs e)
    {
        //任意 CSV (首列不含欄名)
        if (!string.IsNullOrEmpty(btnFreeCsv1.ucUploadedFileName) && btnFreeCsv1.ucUploadedFileBody.Length > 0)
        {
            string strCsvData = System.Text.Encoding.UTF8.GetString(btnFreeCsv1.ucUploadedFileBody);
            DataTable dtErrInfo;
            DataTable dtResult = Util.getDataTable(strCsvData, out dtErrInfo);  //首列不含欄名
            ShowResult(dtResult, dtErrInfo);
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "未收到檔案");
        }
    }

    void btnFreeCsv2_onClose(object sender, EventArgs e)
    {
        //任意 CSV (首列包含欄名)
        if (!string.IsNullOrEmpty(btnFreeCsv2.ucUploadedFileName) && btnFreeCsv2.ucUploadedFileBody.Length > 0)
        {
            string strCsvData = System.Text.Encoding.UTF8.GetString(btnFreeCsv2.ucUploadedFileBody);
            DataTable dtErrInfo;
            DataTable dtResult = Util.getDataTable(strCsvData, out dtErrInfo, true);  //首列包含欄名
            ShowResult(dtResult, dtErrInfo);
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "未收到檔案");
        }
    }

    void btnStockInfo1_onClose(object sender, EventArgs e)
    {
        //轉換StockInfo.csv
        //一發現資料錯誤就中斷
        if (!string.IsNullOrEmpty(btnStockInfo1.ucUploadedFileName) && btnStockInfo1.ucUploadedFileBody.Length > 0)
        {
            string strCsvData = System.Text.Encoding.UTF8.GetString(btnStockInfo1.ucUploadedFileBody);
            DataTable dtErrInfo;
            DataTable dtResult = Util.getDataTable(strCsvData, out dtErrInfo, false, ",", false, StockInfoColumnDefinition);  // IsKeepSuccessDataWhenHasError = false
            ShowResult(dtResult, dtErrInfo);
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "未收到檔案");
        }
    }

    void btnStockInfo2_onClose(object sender, EventArgs e)
    {
        //轉換StockInfo.csv
        //發現資料錯誤仍繼續轉換
        if (!string.IsNullOrEmpty(btnStockInfo2.ucUploadedFileName) && btnStockInfo2.ucUploadedFileBody.Length > 0)
        {
            string strCsvData = System.Text.Encoding.UTF8.GetString(btnStockInfo2.ucUploadedFileBody);
            DataTable dtErrInfo;
            DataTable dtResult = Util.getDataTable(strCsvData, out dtErrInfo, false, ",", true, StockInfoColumnDefinition); // IsKeepSuccessDataWhenHasError = true
            ShowResult(dtResult, dtErrInfo);
        }
        else
        {
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "未收到檔案");
        }
    }

    private void ShowResult(DataTable dtResult, DataTable dtErrInfo)
    {
        if (dtResult.IsNullOrEmpty())
        {
            if (dtErrInfo.IsNullOrEmpty())
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound);
            }
            else
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "轉換失敗");
                divErrInfo.Visible = true;
                gvErrInfo.ucDataKeyList = dtErrInfo.Columns[0].ColumnName.Split(',');
                gvErrInfo.ucDataQryTable = dtErrInfo;
                gvErrInfo.Refresh(true);
            }
        }
        else
        {
            divResult.Visible = true;
            gvResult.ucDataKeyList = dtResult.Columns[0].ColumnName.Split(',');
            gvResult.ucDataQryTable = dtResult;
            gvResult.Refresh(true);
            labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "轉換成功");

            if (!dtErrInfo.IsNullOrEmpty())
            {
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Done, "處理完畢，但部份資料轉換錯誤");
                divErrInfo.Visible = true;
                gvErrInfo.ucDataKeyList = dtErrInfo.Columns[0].ColumnName.Split(',');
                gvErrInfo.ucDataQryTable = dtErrInfo;
                gvErrInfo.Refresh(true);
            }
        }

    }
}