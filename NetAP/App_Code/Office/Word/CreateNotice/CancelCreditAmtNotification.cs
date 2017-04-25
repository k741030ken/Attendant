using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Office.Word.CreateNotice
{
    public class CancelCreditAmtNotification
    {
        public static byte[] MakeNotice(string templateFile, InputMessage message, List<Dictionary<string, string>> tableForContactMan, List<Dictionary<string, string>> tableForCreditCancel, int CreditCancelCount)
        {
            // 建立暫存記憶體空間
            using (MemoryStream memStream = new MemoryStream())
            {
                // 將檔案寫入暫存記憶體
                using (FileStream fileStream = File.OpenRead(templateFile))
                {
                    memStream.SetLength(fileStream.Length);
                    fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                }

                // 以word格式讀取記憶體中的檔案並進行編輯
                using (WordprocessingDocument wd = WordprocessingDocument.Open(memStream, true))
                {
                    // 替換[$變數$]中的資料
                    WordUtility.Parse(wd.MainDocumentPart, message);
                    foreach (HeaderPart hp in wd.MainDocumentPart.HeaderParts)
                    {
                        WordUtility.Parse(hp, message);
                    }

                    foreach (FooterPart fp in wd.MainDocumentPart.FooterParts)
                    {
                        WordUtility.Parse(fp, message);
                    }

                    #region "聯絡人資料(多筆)"

                    Table table1 = WordUtility.GetTable(wd, 0);

                    for (int i = 0; i < tableForContactMan.Count; i++)
                    {
                        WordUtility.SetDataToRow(table1, i + 1, 0, tableForContactMan[i]["永豐銀行"]);
                        WordUtility.SetDataToRow(table1, i + 1, 1, tableForContactMan[i]["電話"]);
                        WordUtility.SetDataToRow(table1, i + 1, 2, tableForContactMan[i]["傳真"]);
                    }

                    #endregion "聯絡人資料(多筆)"

                    #region 附表一

                    Table table2 = WordUtility.GetTable(wd, 1);

                    for (int i = 0; i < tableForCreditCancel.Count; i++)
                    {
                        if (i % (CreditCancelCount + 2) == 0)
                        {
                            WordUtility.AppendTableRowStyle(table2, 2);
                        }
                        else if (i % (CreditCancelCount + 2) == CreditCancelCount + 1)
                        {
                            WordUtility.AppendTableRowStyle(table2, 5);
                        }
                        else
                        {
                            WordUtility.AppendTableRowStyle(table2, 3);
                        }
                    }
                    WordUtility.RemoveTemplateRow(table2, 2, 9);

                    for (int i = 0; i < tableForCreditCancel.Count; i++)
                    {
                        if (i % (CreditCancelCount + 2) == 0)
                        {
                            WordUtility.SetDataToRow(table2, i + 2, 0, tableForCreditCancel[i]["參貸行"]);
                        }
                        else if (i % (CreditCancelCount + 2) == CreditCancelCount + 1)
                        {
                            WordUtility.SetDataToRow(table2, i + 2, 5, tableForCreditCancel[i]["參貸行補償費小計"]);
                        }
                        else
                        {
                            WordUtility.SetDataToRow(table2, i + 2, 1, tableForCreditCancel[i]["分項額度"]);
                            WordUtility.SetDataToRow(table2, i + 2, 2, tableForCreditCancel[i]["取消額度"]);
                            WordUtility.SetDataToRow(table2, i + 2, 3, tableForCreditCancel[i]["取消後參貸額度"]);
                            WordUtility.SetDataToRow(table2, i + 2, 4, tableForCreditCancel[i]["補償費"]);
                        }
                    }

                    #endregion 附表一
                }
                return memStream.ToArray();
            }
        }
    }
}