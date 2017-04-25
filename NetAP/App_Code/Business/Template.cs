using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Office.Word.CreateNotice;
using Office.Word;
using Novacode;
using System.IO;

/// <summary>
/// TestLogic 的摘要描述
/// </summary>
public class Template //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// 取得DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetEmpFlowSNEmpAndSexUseDapper(TemplateModel model, out List<TemplateBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<TemplateBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                TemplateBean dataBean = new TemplateBean()
                {
                    CompID = model.OTCompID,
                    EmpID = model.OTEmpID,
                    NameN = model.NameN,
                    Sex = model.Sex
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.GetEmpFlowSNEmpAndSexSql(dataBean, ref sb);
                try
                {
                    datas = conn.Query<TemplateBean>(sb.ToString(), dataBean).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null || datas.Count == 0)
                {
                    throw new Exception("查無資料!");
                }
                
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 新增DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">新增筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool InsertEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111", LastChgComp = "ZZZZZZ", LastChgID = "111111", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222", LastChgComp = "ZZZZZZ", LastChgID = "222222", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectTemplateSql(ref sb); //建立查詢SqlCommand
                List<TemplateBean> newDataBean = new List<TemplateBean>();
                //新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                foreach(var item in dataBean)
                {
                    try
                    {
                        var count = conn.Query<TemplateBean>(sb.ToString(), item).Count(); //執行查詢，結果回傳至TestBean物件
                        if (count == 0)
                        {
                            newDataBean.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (newDataBean.Count > 0)
                {
                    SqlCommand.InsertTemplateSql(ref sb, true); //建立新增SqlCommand
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            seccessCount = conn.Execute(sb.ToString(), newDataBean, trans); //執行新增，成功筆數回傳，並做Transaction機制
                            trans.Commit(); //成功Transaction直接Commit
                        }
                        catch (Exception)
                        {
                            trans.Rollback(); //失敗Transaction Rollback
                            throw;
                        }
                    }
                }                
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 刪除DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">刪除筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool DeleteEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111"},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222"}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.DeleteTemplateSql(ref sb); //建立刪除SqlCommand
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans); //執行刪除，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 修改DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">變更筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool UpdateEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111", LastChgComp = "ZZZZZZ", LastChgID = "111111", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222", LastChgComp = "ZZZZZZ", LastChgID = "222222", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.UpdateTemplateSql(ref sb); //建立修改SqlCommand
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans); //執行修改，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {        
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 將DB資料轉成作格式化
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<TemplateGridData> DataFormat(List<TemplateBean> dbDataList)
    {
        var result = new List<TemplateGridData>();
        foreach (var item in dbDataList)
        {
            var data = new TemplateGridData();
            data.OTCompID = item.CompID;
            data.OTEmpID = item.EmpID;
            data.Sex = item.Sex;
            data.NameN = item.NameN;
            data.ShowOTEmp = item.EmpID + item.NameN;
            switch (item.Sex)
            {
                case "1": { data.ShowSex = "男"; break; }
                case "2": { data.ShowSex = "女"; break; } 
            }
            result.Add(data);
        }
        return result;
    }

    /// <summary>
    /// 套表有Table By Docx.dll (https://github.com/WordDocX/DocX/blob/master/Examples/Program.cs)
    /// </summary>
    /// <returns></returns>
    public static byte[] CreateTableRowsFromTemplate()
    {
        byte[] wordFileData = null;
        using (MemoryStream memStream = new MemoryStream())
        {
            var templatPath = HttpContext.Current.Server.MapPath("~/OpenXmlTemplateFiles/DocumentWithTemplateTable.docx");
            using (DocX docX = DocX.Load(templatPath))
            {
                //look for one specific table here
                Table orderTable = docX.Tables.First(t => t.TableCaption == "ORDER_TABLE");
                if (orderTable != null)
                {
                    //Row 0 and 1 are Headers
                    //Row 2 is pattern
                    if (orderTable.RowCount >= 2)
                    {
                        //get the Pattern row for duplication
                        Row orderRowPattern = orderTable.Rows[2];
                        //Add 5 lines of product
                        for (int i = 0; i < 5; i++)
                        {
                            //InsertRow performs a copy, so we get markup in new line ready for replacements
                            Row newOrderRow = orderTable.InsertRow(orderRowPattern, 2 + i);
                            newOrderRow.ReplaceText("%PRODUCT_NAME%", "Product_" + i);
                            newOrderRow.ReplaceText("%PRODUCT_PRICE1%", "$ " + i * new Random().Next(1, 50));
                            newOrderRow.ReplaceText("%PRODUCT_PRICE2%", "$ " + i * new Random().Next(1, 50));
                        }

                        //pattern row is at the end now, can be removed from table
                        orderRowPattern.Remove();

                    }
                    docX.SaveAs(memStream);
                    wordFileData = memStream.ToArray();
                }
            }
        }  
        return wordFileData;
    }

    /// <summary>
    /// 套表 By Docx.dll (https://github.com/WordDocX/DocX/blob/master/Examples/Program.cs)
    /// Loads a document 'DocumentWithBookmarks.docx' and changes text inside bookmark keeping formatting the same.
    /// This code creates the file 'BookmarksReplaceTextOfBookmarkKeepingFormat.docx'.
    /// </summary>
    public static byte[] BookmarksReplaceTextOfBookmarkKeepingFormat()
    {
        byte[] wordFileData = null;
        using (MemoryStream memStream = new MemoryStream())
        {
            var templatPath = HttpContext.Current.Server.MapPath("~/OpenXmlTemplateFiles/DocumentWithBookmarks.docx");
            using (DocX docX = DocX.Load(templatPath))
            {
                // Replace bookmars content
                docX.Bookmarks["bmkNoContent"].SetText("Here there was a bookmark");
                docX.Bookmarks["bmkContent"].SetText("Here there was a bookmark with a previous content");
                docX.Bookmarks["bmkFormattedContent"].SetText("Here there was a formatted bookmark");
                docX.SaveAs(memStream);
                wordFileData = memStream.ToArray();
            }
        }
        return wordFileData;
    }

    /// <summary>
    /// 取消額度通知書 By DocumentFormat.OpenXml.dll 
    /// </summary>
    /// <returns></returns>
    public static byte[] MakeCancelCreditAmtNotification()
    {
        byte[] wordFileData = null;
        // Note: 通知書路徑
        var templatPath = HttpContext.Current.Server.MapPath("~/OpenXmlTemplateFiles/TW_CancelCreditAmtNotification.docx");

        #region 資料準備

        string cancelWeek = "二";
        string noticeDay = "2017/4/25";

        #endregion 資料準備

        #region 固定格式資料組成

        InputMessage inputMessage = new InputMessage();
        inputMessage.Fields = new List<InputMessageField>();

        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field1", VariableValue = "CaseName" });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field2", VariableValue = "CompanyName" });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field3", VariableValue = "CancelDT" + " " + cancelWeek });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field4", VariableValue = "field4" });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field5", VariableValue = "CustomerAddress" });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field6", VariableValue = "CompanyName" });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field10", VariableValue = noticeDay });
        inputMessage.Fields.Add(new InputMessageField() { VariableID = "Field11", VariableValue = "CyID" });

        List<Dictionary<string, string>> tableForContactMan = new List<Dictionary<string, string>>();
        tableForContactMan.Add(new Dictionary<string, string>() {
                    { "永豐銀行", "XXX" },
                    { "電話", "0921321321"},
                    { "傳真", "02-24242424" }
                });
        tableForContactMan.Add(new Dictionary<string, string>() {
                    { "永豐銀行", "OOO" },
                    { "電話","0912123123"},
                    { "傳真", "02-23232323" }
                });

        #endregion 固定格式資料組成

        #region Table資料組成

        List<Dictionary<string, string>> tableForCreditCancel = new List<Dictionary<string, string>>();

        //int index = 0;
        //for (int i = 0; i < notice.CreditCancel.Count; i++)
        //{
        //    if (notice.CreditCancel[i].ShareFlag == "N")
        //    {
        //        index = i;
        //        break;
        //    }
        //}

        int creditCancelCount = 0;
        //for (int i = 0; i < notice.CreditCancel.Count; i++)
        //{
        //    if (notice.CreditCancel[i].ShareFlag == "N")
        //    {
        //        creditCancelCount++;
        //    }
        //}

        //if (creditCancelCount > 0)
        //{
        //    for (int i = 0; i < notice.CreditCancel[index].BankDistrib.Count; i++)
        //    {
        //        tableForCreditCancel.Add(new Dictionary<string, string>(){
        //                { "參貸行", notice.CreditCancel[index].BankDistrib[i].BankName }
        //            });

        //        decimal totalcompensation = 0;
        //        for (int j = 0; j < notice.CreditCancel.Count; j++)
        //        {
        //            if (notice.CreditCancel[j].ShareFlag == "N")
        //            {
        //                tableForCreditCancel.Add(new Dictionary<string, string>(){
        //                        { "分項額度", notice.CreditCancel[j].TotalSubItem },
        //                        { "取消額度", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, notice.CreditCancel[j].BankDistrib[i].CancelLimit) },
        //                        { "取消後參貸額度", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, notice.CreditCancel[j].BankDistrib[i].LimitAfterCancel) },
        //                        { "補償費", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, notice.CreditCancel[j].BankDistrib[i].CompensationAmt) }
        //                    });
        //                totalcompensation += NumberUtility.ParseToDecimal(notice.CreditCancel[j].BankDistrib[i].CompensationAmt);
        //            }
        //        }
        //        tableForCreditCancel.Add(new Dictionary<string, string>(){
        //                { "參貸行補償費小計", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, totalcompensation.ToString())}
        //            });
        //    }

        //    tableForCreditCancel.Add(new Dictionary<string, string>(){
        //            { "參貸行", "合計" }
        //        });

        //    decimal totalCom = 0;
        //    for (int j = 0; j < notice.CreditCancel.Count; j++)
        //    {
        //        decimal sumAmt = 0, sumAfterAmt = 0, sumCompensation = 0;
        //        if ("N".Equals(notice.CreditCancel[j].ShareFlag))
        //        {
        //            for (int i = 0; i < notice.CreditCancel[j].BankDistrib.Count; i++)
        //            {
        //                sumAmt += NumberUtility.ParseToDecimal(notice.CreditCancel[j].BankDistrib[i].CancelLimit);
        //                sumAfterAmt += NumberUtility.ParseToDecimal(notice.CreditCancel[j].BankDistrib[i].LimitAfterCancel);
        //                sumCompensation += NumberUtility.ParseToDecimal(notice.CreditCancel[j].BankDistrib[i].CompensationAmt);
        //            }

        //            tableForCreditCancel.Add(new Dictionary<string, string>(){
        //                    { "分項額度", notice.CreditCancel[j].TotalSubItem },
        //                    { "取消額度", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, sumAmt.ToString()) },
        //                    { "取消後參貸額度", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, sumAfterAmt.ToString()) },
        //                    { "補償費", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, sumCompensation.ToString()) }
        //                });
        //            totalCom += sumCompensation;
        //        }
        //    }
        //    tableForCreditCancel.Add(new Dictionary<string, string>(){
        //            { "參貸行補償費小計", EditMaskHelper.GetAfterEditMaskStringByCurrency(notice.CreditCancel[index].CreditCyID, totalCom.ToString())}
        //        });
        //}

        #endregion Table資料組成

        // Note: 產生通知書Word、Pdf
        wordFileData = CancelCreditAmtNotification.MakeNotice(templatPath, inputMessage, tableForContactMan, tableForCreditCancel, creditCancelCount);
        return wordFileData;
    }

    

}