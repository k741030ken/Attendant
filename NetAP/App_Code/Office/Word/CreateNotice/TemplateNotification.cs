using System;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;

namespace Office.Word.CreateNotice
{
    public class TemplateNotification
    {
        public static byte[] MakeNotice(string templateFile, InputMessage message, List<Dictionary<string, string>> tableForContactMan, List<Dictionary<string, object>> tableForPayment)
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
                }
                return memStream.ToArray();
            }
        }
    }
}