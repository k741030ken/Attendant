using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Office.Word
{
    public class WordUtility
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="oxp"></param>
        /// <param name="message"></param>
        public static void Parse(OpenXmlPart oxp, InputMessage message)
        {
            string wordContent = null;

            using (StreamReader sr = new StreamReader(oxp.GetStream()))
            {
                wordContent = sr.ReadToEnd();
            }

            List<string> lists = GetAllKeys(wordContent);
            StringBuilder sb = new StringBuilder(wordContent);

            foreach (var item in lists)
            {
                var field = message.Fields.Where(p => p.VariableID == item).FirstOrDefault();
                if (field != null)
                {
                    sb.Replace("[$" + item + "$]", field.VariableValue);
                }
                else
                {
                    sb.Replace("[$" + item + "$]", string.Empty);
                }
            }

            using (StreamWriter sw = new StreamWriter(oxp.GetStream(FileMode.Create)))
            {
                sw.Write(sb.ToString());
            }
        }

        /// <summary>
        /// 產生Word檔
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] MakeDocx(string templateFile, InputMessage message)
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (FileStream fileStream = File.OpenRead(templateFile))
                    {
                        memStream.SetLength(fileStream.Length);
                        fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                    }

                    using (WordprocessingDocument wd = WordprocessingDocument.Open(memStream, true))
                    {
                        Parse(wd.MainDocumentPart, message);

                        foreach (HeaderPart hp in wd.MainDocumentPart.HeaderParts)
                        {
                            Parse(hp, message);
                        }

                        foreach (FooterPart fp in wd.MainDocumentPart.FooterParts)
                        {
                            Parse(fp, message);
                        }
                    }

                    return memStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 產生Word檔
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] MakeDocx(List<InputDatas> inputDatas)
        {
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    if (inputDatas != null && inputDatas.Count > 0)
                    {
                        using (FileStream memFileStream = File.OpenRead(inputDatas[0].FilePath))
                        {
                            memStream.SetLength(memFileStream.Length);
                            memFileStream.Read(memStream.GetBuffer(), 0, (int)memFileStream.Length);
                        }

                        using (WordprocessingDocument mainWD = WordprocessingDocument.Open(memStream, true))
                        {
                            MainDocumentPart mainPart = mainWD.MainDocumentPart;
                            if (inputDatas[0].InputMessage != null)
                            {
                                //Replace key to data
                                Parse(mainPart, inputDatas[0].InputMessage);

                                foreach (HeaderPart hp in mainPart.HeaderParts)
                                {
                                    Parse(hp, inputDatas[0].InputMessage);
                                }

                                foreach (FooterPart fp in mainPart.FooterParts)
                                {
                                    Parse(fp, inputDatas[0].InputMessage);
                                }
                            }

                            for (int i = inputDatas.Count - 1; i >= 1; i--)
                            {
                                string altChunkId = "AltChunkId" + i;
                                AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(
                                    AlternativeFormatImportPartType.WordprocessingML, altChunkId);

                                using (MemoryStream childStream = new MemoryStream())
                                {
                                    using (FileStream childFileStream = File.OpenRead(inputDatas[i].FilePath))
                                    {
                                        childStream.SetLength(childFileStream.Length);
                                        childFileStream.Read(childStream.GetBuffer(), 0, (int)childFileStream.Length);
                                    }
                                    using (WordprocessingDocument childWD = WordprocessingDocument.Open(childStream, true))
                                    {
                                        MainDocumentPart childPart = childWD.MainDocumentPart;

                                        if (inputDatas[i].InputMessage != null)
                                        {
                                            //Replace key to data
                                            Parse(childPart, inputDatas[i].InputMessage);

                                            foreach (HeaderPart hp in childPart.HeaderParts)
                                            {
                                                Parse(hp, inputDatas[i].InputMessage);
                                            }

                                            foreach (FooterPart fp in childPart.FooterParts)
                                            {
                                                Parse(fp, inputDatas[i].InputMessage);
                                            }
                                        }

                                        childWD.Close();
                                    }
                                    childStream.Seek(0, SeekOrigin.Begin);
                                    chunk.FeedData(childStream);
                                }

                                AltChunk altChunk = new AltChunk();
                                altChunk.Id = altChunkId;
                                mainPart.Document.Body.InsertAfter(altChunk, mainPart.Document.Body.Elements<Paragraph>().Last());
                                mainPart.Document.Save();
                            }
                            mainWD.Close();
                        }
                    }
                    memStream.Seek(0, SeekOrigin.Begin);

                    return memStream.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }

        public static Table GetTable(WordprocessingDocument wd, int index)
        {
            IEnumerable<Table> documentBodyElement = null;
            if (wd != null && wd.MainDocumentPart != null && wd.MainDocumentPart.Document != null && wd.MainDocumentPart.Document.Body != null)
            {
                documentBodyElement = wd.MainDocumentPart.Document.Body.Elements<Table>();
            }            
            if (documentBodyElement != null && index <= wd.MainDocumentPart.Document.Body.Elements<Table>().Count() - 1)
            {
                return wd.MainDocumentPart.Document.Body.Elements<Table>().ElementAt(index);
            }
            return new Table();
        }

        public static void AppendTableRowStyle(Table table, int row)
        {
            if (table != null && table.Elements<TableRow>() != null)
            {
                if (row <= table.Elements<TableRow>().Count() - 1)
                {
                    table.Append(table.Elements<TableRow>().ElementAt(row).CloneNode(true));
                }
            }
        }

        public static void SetTableData(Table table, int styleRow, int dataRow, Dictionary<int, string> columnData)
        {
            AppendTableRowStyle(table, styleRow);

            foreach (var key in columnData.Keys)
            {
                SetDataToRow(table, dataRow, key, columnData[key]);
            }
        }

        public static void SetDataToRow(Table table, int row, int column, string value = "")
        {
            // Note: 防呆
            if (table != null && table.Elements<TableRow>() != null)
            {
                if (row <= table.Elements<TableRow>().Count() - 1)
                {
                    TableRow tableRow = table.Elements<TableRow>().ElementAt(row);
                    {
                        if (tableRow.Elements<TableCell>() != null)
                        {
                            if (column <= tableRow.Elements<TableCell>().Count() - 1)
                            {
                                TableCell cell = tableRow.Elements<TableCell>().ElementAt(column);
                                Paragraph p = cell.Elements<Paragraph>().First();
                                Run r = p.Elements<Run>().First();
                                if (r != null)
                                {
                                    Text t = r.Elements<Text>().First();
                                    if (t != null)
                                    {
                                        t.Text = value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void SetDataAndEnterToRow(Table table, int row, int column, List<string> values)
        {
            // Note: 防呆
            if (table != null && table.Elements<TableRow>() != null)
            {
                if (row <= table.Elements<TableRow>().Count() - 1)
                {
                    TableRow tableRow = table.Elements<TableRow>().ElementAt(row);
                    {
                        if (tableRow.Elements<TableCell>() != null)
                        {
                            if (column <= tableRow.Elements<TableCell>().Count() - 1)
                            {
                                TableCell cell = tableRow.Elements<TableCell>().ElementAt(column);
                                Paragraph p = cell.Elements<Paragraph>().FirstOrDefault();
                                if (p != null)
                                {
                                    for (int i = 0; i < values.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            Run r = p.Elements<Run>().First();
                                            if (r != null)
                                            {
                                                Text t = r.Elements<Text>().First();
                                                if (t != null)
                                                {
                                                    t.Text = values[i];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Note: 複製Cell第一個段落style
                                            OpenXmlElement paragraph = p.CloneNode(true);
                                            Run r = paragraph.Elements<Run>().First();
                                            if (r != null)
                                            {
                                                Text t = r.Elements<Text>().First();
                                                if (t != null)
                                                {
                                                    t.Text = values[i];
                                                }
                                                cell.Append(paragraph);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void RemoveTemplateRow(Table table, int startRow, int endRow)
        {
            // 務必以下往上刪除，不然以上往下刪除就要指定相同的index刪除，容易出現少刪或多刪的情形
            int tableRowCnt = table.Elements<TableRow>().Count();
            if (table.Elements<TableRow>() != null)
            {
                if (endRow <= tableRowCnt - 1)
                {
                    for (int i = endRow; i >= startRow; i--)
                    {
                        TableRow row = table.Elements<TableRow>().ElementAt(i);
                        if (row != null)
                        {
                            table.RemoveChild(row);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="wordContent"></param>
        /// <returns></returns>
        private static List<string> GetAllKeys(string wordContent)
        {
            List<string> lists = new List<string>();
            while (true)
            {
                int start = wordContent.IndexOf("[$");
                if (start >= 0)
                {
                    wordContent = wordContent.Substring(start + 2);
                    int end = wordContent.IndexOf("$]");

                    string name = wordContent.Substring(0, end);
                    lists.Add(name);
                    wordContent = wordContent.Substring(end + 2);
                }
                else
                {
                    break;
                }
            }
            return lists;
        }
    }
}