using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// FileUtility 的摘要描述
/// </summary>
public class FileUtility
{
	    /// <summary>
        /// 回傳檔案類型
        /// </summary>
        public static string GetFileType(string fileName)
        {
            string result = "";
            string sFileType = ((fileName.Split('.') != null && fileName.Split('.').Length > 0) ?
                                    fileName.Split('.')[fileName.Split('.').Length - 1] : "").ToUpper();
            if (sFileType.Equals(Constants.PDF))
            {
                result = Constants.PDF;
            }
            else if (sFileType.Equals(Constants.DOC))
            {
                result = Constants.DOC;
            }
            else if (sFileType.Equals(Constants.DOCX))
            {
                result = Constants.DOCX;
            }
            else if (sFileType.Equals(Constants.PPT))
            {
                result = Constants.PPT;
            }
            else if (sFileType.Equals(Constants.PPTX))
            {
                result = Constants.PPTX;
            }
            else if (sFileType.Equals(Constants.XLS))
            {
                result = Constants.XLS;
            }
            else if (sFileType.Equals(Constants.XLSX))
            {
                result = Constants.XLSX;
            }
            else if (sFileType.Equals(Constants.JPG))
            {
                result = Constants.JPG;
            }
            else if (sFileType.Equals(Constants.SEVEN_ZIP))
            {
                result = Constants.SEVEN_ZIP;
            }
            else if (sFileType.Equals(Constants.ZIP))
            {
                result = Constants.ZIP;
            }
            else
            {
                result = Constants.OTHER;
            }
            return result;
        }

        /// <summary>
        /// 下載File
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadFile(HttpResponse response, byte[] datas, string fileName, string contentType)
        {
            response.Clear();
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            response.AddHeader("Content-Length", datas.Length.ToString());
            response.ContentType = contentType;
            response.Flush();
            response.BinaryWrite(datas);
            response.End();
            response.Close();
        }

        /// <summary>
        /// 下載PDF
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadPDF(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".pdf");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_PDF);
        }

        /// <summary>
        /// 下載doc
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadDOC(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".doc");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_DOC);
        }

        /// <summary>
        /// 下載docx
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadDOCX(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".docx");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_DOCX);
        }

        /// <summary>
        /// 下載ppt
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadPPT(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".ppt");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_PPT);
        }

        /// <summary>
        /// 下載pptx
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadPPTX(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".pptx");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_PPTX);
        }

        /// <summary>
        /// 下載xls
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadXLS(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".xls");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_XLS);
        }

        /// <summary>
        /// 下載xlsx
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadXLSX(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".xlsx");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_XLSX);
        }

        /// <summary>
        /// 下載jpg
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadJPG(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".jpg");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_JPG);
        }

        /// <summary>
        /// 下載7z
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoad7Z(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".7z");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_7Z);
        }

        /// <summary>
        /// 下載zip
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadZIP(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".zip");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_ZIP);
        }

        /// <summary>
        /// 下載CSV
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadCSV(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            StringBuilder fileName = new StringBuilder().Append(fileDownloadName);
            if (isFileNameExtension)
            {
                fileName.Append(".csv");
            }
            DownLoadFile(response, datas, fileName.ToString(), Constants.FILETYPE_CSV);
        }

        /// <summary>
        /// 下載其他格式檔案
        /// </summary>
        /// <param name="fileID"></param>
        /// <param name="fileDownloadName"></param>
        /// <returns></returns>
        public static void DownLoadFile(HttpResponse response, byte[] datas, string fileDownloadName, bool isFileNameExtension)
        {
            DownLoadFile(response, datas, fileDownloadName, Constants.FILETYPE_STREAM);
        }
}