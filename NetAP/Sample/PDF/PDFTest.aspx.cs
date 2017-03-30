using System;
using System.Collections.Generic;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;


public partial class PDFTest : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnMerge_Click(object sender, EventArgs e)
    {
        //示範 PDF 合併功能
        UserInfo oUser = UserInfo.getUserInfo(); //登入人員資訊
        PDFHelper oPage = PDFHelper.getPDFHelper(true); //取得並重置 PDF 版面設定(全域物件)
        List<byte[]> oPDFList = new List<byte[]>(); //需合併的PDF清單

        //準備資料
        DbHelper db = new DbHelper();
        DataTable dtComp = db.ExecuteDataSet("Select CompID,CompName From SE_Company").Tables[0];
        DataTable dtDept = db.ExecuteDataSet("Select CompID,DeptID,DeptName From SE_Dept Where CompID='SPHBK1' and DeptID Like '000%' ").Tables[0];

        //加入第一份PDF (資料表 dtComp)
        oPage.HeaderTitle = "PDF1-[直][明]";
        oPDFList.Add(PDFHelper.getPDFStream(dtComp).ToArray());

        //加入第二份PDF (資料表 dtDept)
        oPage.HeaderTitle = "PDF2-[直][楷]";
        oPage.FontName = PDFHelper.PDFFont.Kai; //楷書
        oPDFList.Add((PDFHelper.getPDFStream(dtDept)).ToArray());

        //設定橫印
        oPage.IsLandscapePage = true;
        //加入第三份PDF (浮水印)
        oPage.HeaderTitle = "PDF3-[橫][黑][印]";
        oPage.FontName = PDFHelper.PDFFont.Black; //黑體
        oPDFList.Add((PDFHelper.getPDFwithWaterMark(PDFHelper.getPDFStream(dtComp).ToArray(), oUser.DefaultWatermarkText)).ToArray());

        //合併並輸出為 [Merge.pdf]
        Util.ExportBinary(PDFHelper.getMergedPDFStream(oPDFList).ToArray(), "Merge.pdf");
    }


    protected void btnLayout01_Click(object sender, EventArgs e)
    {
        //示範 PDF 套版功能
        UserInfo oUser = UserInfo.getUserInfo(); //登入人員資訊
        PDFHelper oPage = PDFHelper.getPDFHelper(true); //取得並重置 PDF 版面設定(全域物件)
        oPage.PageMarginBottom = 20;

        //取得套版內容
        string strXHtml = Util.getHtmlContent(Util.getFixURL("~/Sample/PDF/Page01.htm", true));
        //代換所需變數
        strXHtml = strXHtml.Replace("[經銷商全銜]", "大明事務機租賃公司");
        strXHtml = strXHtml.Replace("[員工姓名]", oUser.UserName);
        strXHtml = strXHtml.Replace("[借用設備]", "投影機");

        //輸出PDF(自訂頁首頁尾)
        Util.ExportBinary(PDFHelper.getPDFStream(strXHtml, false).ToArray(), "Page01.pdf");
    }


    protected void btnLayout02_Click(object sender, EventArgs e)
    {
        //示範 PDF 套版功能
        UserInfo oUser = UserInfo.getUserInfo(); //登入人員資訊
        PDFHelper oPage = PDFHelper.getPDFHelper(true); //取得並重置 PDF 版面設定(全域物件)
        oPage.HeaderTitle = "[網頁套版02]";
        oPage.FooterComment = "*限內部使用*";
        

        //取得套版內容
        string strXHtml = Util.getHtmlContent(Util.getFixURL("~/Sample/PDF/Page02.htm", true));
        //代換所需變數
        strXHtml = strXHtml.Replace("[經銷商全銜]", "大明事務機租賃公司");
        strXHtml = strXHtml.Replace("[員工姓名]", oUser.UserName);
        strXHtml = strXHtml.Replace("[借用設備]", "投影機");

        //輸出PDF(內建頁首頁尾)
        Util.ExportBinary(PDFHelper.getPDFStream(strXHtml, true).ToArray(), "Page02.pdf");
    }
}