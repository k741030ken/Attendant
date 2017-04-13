using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Sample_Excel_GrpCellMerge : BasePage
{
    private DataTable _MainData
    {
        get
        {
            if (PageViewState["_MainData"] == null)
            {
                //產生匯出用資料表
                DataTable dt = new DataTable();
                dt.Columns.Add("CustName");                 //客戶名稱
                dt.Columns.Add("TotTxnAmt", typeof(int));   //總交易金額
                dt.Columns.Add("TxnName");                  //交易項目名稱
                dt.Columns.Add("TxnAmt", typeof(int));     //交易項目金額

                dt.Rows.Add("台灣電力", 35000, "授信", 5000);
                dt.Rows.Add("台灣電力", 35000, "有價證券", 18000);
                dt.Rows.Add("台灣電力", 35000, "衍生商品", 12000);
                dt.Rows.Add("中華票券", 25000, "債券", 15000);
                dt.Rows.Add("中華票券", 25000, "投資", 10000);
                dt.Rows.Add("陳X天", 5000, "授信", 1500);
                dt.Rows.Add("陳X天", 5000, "有價證券", 3500);
                dt.Rows.Add("王X明", 2000, "衍生商品", 2000);
                dt.Rows.Add("鴻海集團", 66000, "授信", 5000);
                dt.Rows.Add("鴻海集團", 66000, "有價證券", 61000);
                dt.Rows.Add("工商銀行", 35000, "衍生商品", 10000);
                dt.Rows.Add("工商銀行", 35000, "債券", 15000);
                dt.Rows.Add("工商銀行", 35000, "投資", 10000);
                dt.Rows.Add("統一超商", 62000, "投資", 62000);

                PageViewState["_MainData"] = dt;
            }
            return (DataTable)(PageViewState["_MainData"]);
        }
    }

    private Dictionary<string, string> _dicDisp
    {
        get
        {
            if (PageViewState["_dicDisp"] == null)
            {
                //資料欄位樣式
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("CustName", "客戶名稱@C");
                dic.Add("TotTxnAmt", "總交易金額@N0");
                dic.Add("TxnName", "交易項目@L");
                dic.Add("TxnAmt", "交易金額@N0");

                PageViewState["_dicDisp"] = dic;
            }
            return (Dictionary<string, string>)(PageViewState["_dicDisp"]);
        }
    }

    private Dictionary<string, string> _dicGrp
    {
        get
        {
            if (PageViewState["_dicGrp"] == null)
            {
                //資料群組分類
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("A.法　人", "台灣電力,中華票券");
                dic.Add("B.自然人", "陳X天,王X明");
                dic.Add("C.集　團", "鴻海集團,工商銀行,統一超商");

                PageViewState["_dicGrp"] = dic;
            }
            return (Dictionary<string, string>)(PageViewState["_dicGrp"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvMain.ucDataQryTable = _MainData;
            gvMain.ucDataDisplayDefinition = _dicDisp;
            gvMain.ucDataKeyList = "CustName".Split(',');
            gvMain.ucDisplayOnly = true;
            gvMain.ucPageSize = 20;
            gvMain.Refresh(true);
        }

        btnGrpMerge.onStart += btnGrpMerge_onStart;
    }

    void btnGrpMerge_onStart(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        //將資料表先加上表頭、表尾，再加上兩階的群組分類後匯出 Excel

        //來源資料表
        DataTable dt = _MainData;
        //資料欄位樣式
        Dictionary<string, string> dicColum = _dicDisp;
        //資料群組分類
        Dictionary<string, string> dicGrp = _dicGrp;
        //表頭
        string strHeader = "永豐金控大額交易申報表";
        //表尾
        string strFooter = "●金額單位：新台幣(萬)元\n●每月10號前申報上個月交易金額";
        //試算表名稱
        string strSheetName = "[YYYY/MM]申報資料";
        //匯出檔名
        string strFileName = strHeader + ".xlsx";

        //產生 Excel
        using (ExcelPackage package = new ExcelPackage(Util.getExcelOpenXml(dt, strSheetName, null, dicColum, strHeader, strFooter)))
        {
            //初始變數
            ExcelWorksheet oSheet = package.Workbook.Worksheets[1]; //試算表物件
            int RowPos = 3;  //起始列數 (跳過 [表頭] 及 [欄位抬頭] 列)
            int GrpQty = 0;  //單一群組資料筆數
            string[] CustGrpList = null; //單一群組清單

            //處理群組分類
            foreach (var pair in dicGrp)
            {
                //群組表頭 
                oSheet.InsertRow(RowPos, 1);
                oSheet.Cells[RowPos, 1].Value = pair.Key; //例：A.法　人

                ExcelRange oRange = oSheet.Cells[RowPos, 1, RowPos, dt.Columns.Count]; //儲存格選取範圍
                oRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                oRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                oRange.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                oRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#34466A"));
                oRange.Style.Font.Bold = true;
                oRange.Merge = true;

                RowPos += 1;
                CustGrpList = pair.Value.Split(','); //例： [台灣電力,中華票券]
                //群組明細
                for (int g = 0; g < CustGrpList.Count(); g++)
                {
                    GrpQty = dt.Select(string.Format("CustName = '{0}' ", CustGrpList[g])).Length; //例：CustName = [台灣電力] 的資料筆數
                    if (GrpQty > 0)
                    {
                        //欄一：客戶名稱
                        oSheet.Cells[RowPos, 1, RowPos + GrpQty - 1, 1].Merge = true;
                        oSheet.Cells[RowPos, 1, RowPos + GrpQty - 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //欄二：總交易金額
                        oSheet.Cells[RowPos, 2, RowPos + GrpQty - 1, 2].Merge = true;
                        oSheet.Cells[RowPos, 2, RowPos + GrpQty - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        //加上邊框
                        oSheet.Cells[RowPos + GrpQty - 1, 1, RowPos + GrpQty - 1, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        //加上背景色
                        if ((g % 2) == 0)
                            oSheet.Cells[RowPos, 1, RowPos + GrpQty - 1, 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#999999"));
                        else
                            oSheet.Cells[RowPos, 1, RowPos + GrpQty - 1, 2].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EEEEEE"));

                        RowPos += GrpQty;
                    }
                }
            }

            //將匯出資料設定為 FileInfoObj 物件
            byte[] oBytes = package.GetAsByteArray();
            if (FileInfoObj.setFileInfoObj(strFileName, oBytes, true))
            {
                //資料直接下載
                if (FileInfoObj.DirectDownload())
                {
                    //下載正常
                    btnGrpMerge.Complete(RS.Resources.Msg_ExportDataReadyToDownload);  //匯出資料準備完成，請按[存檔]下載
                }
                else
                {
                    //下載錯誤
                    btnGrpMerge.Complete(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error); //查無可供匯出的資料
                }
            }
            else
            {
                //設定 FileInfoObj 物件失敗
                Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error); //匯出資料發生錯誤
            }
        }
    }

}