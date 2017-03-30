<%@ WebHandler Language="C#" Class="AttachThumbnail" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// 從AttachDB取出圖片附件Binary資料，並產生指定大小的縮略圖。
/// <para> [AttachDB/AttachID]為必要參數，[AttachSeqNo]若無指定，則自動找第一筆 FileSize > 0 的資料</para>
/// <para> [Scale][Width][Height]為選擇性參數</para>
/// <para> [Scale]數值為原圖的比例(ex. Scale=0.6)</para>
/// <para> [Width][Height]數值為正整數(像素，ex. Width=96)，可以只給其中一個，程式會自動照比例換算</para>
/// <para> **　[Scale]優先權較[Width][Height]為高，若已有[Scale]參數，則不再判斷[Width][Height]參數</para>
/// <para> **　若未指定，預設產生寬度為　96px　的等比例縮圖</para>
/// </summary>
public class AttachThumbnail : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        int defWidth = 96; //縮圖預設寬度
        string[] arExtList = "gif,jpg,png".Split(','); //可接受的圖形類別

        //嘗試取得參數內容
        string strAttachDB = Util.getRequestQueryStringKey("AttachDB");
        string strAttachID = Util.getRequestQueryStringKey("AttachID");
        string strIsDesc = Util.getRequestQueryStringKey("IsDesc", "N", true);
        int intAttachSeqNo = int.Parse(Util.getRequestQueryStringKey("AttachSeqNo", "0"));

        int intWidth = int.Parse(Util.getRequestQueryStringKey("Width", "0"));
        int intHeight = int.Parse(Util.getRequestQueryStringKey("Height", "0"));
        decimal decScale = decimal.Parse(Util.getRequestQueryStringKey("Scale", "0"));

        Bitmap bmpIn;
        //參數合理時才處理
        if (!string.IsNullOrEmpty(strAttachDB) && !string.IsNullOrEmpty(strAttachID))
        {
            string strSQL = string.Format("Select Top 1 * from AttachInfo Where AttachID = '{0}' And FileSize > 0 ", strAttachID);

            //未指定 SeqNo 則自動找第一筆
            if (intAttachSeqNo > 0) strSQL += " And SeqNo = " + intAttachSeqNo;

            if (strIsDesc == "Y")
                strSQL += " Order By SeqNo Desc ";
            else
                strSQL += " Order By SeqNo ";

            DbHelper db = new DbHelper(strAttachDB);
            DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string strFileName = dt.Rows[0]["FileName"].ToString();
                string strExtName = dt.Rows[0]["FileExtName"].ToString().Substring(1);
                Byte[] binFileBody = (Byte[])dt.Rows[0]["FileBody"];

                if (binFileBody.Length > 0 && arExtList.Contains(strExtName.ToLower()))
                {
                    //為有效圖檔
                    MemoryStream msIn = new MemoryStream((Byte[])dt.Rows[0]["FileBody"]);
                    bmpIn = (Bitmap)Bitmap.FromStream(msIn);
                    msIn.Close();

                    //計算縮放比例
                    if (decScale != 0)
                    {
                        intWidth = Convert.ToInt32(bmpIn.Width * decScale);
                        intHeight = Convert.ToInt32(bmpIn.Height * decScale);
                    }
                    else
                    {
                        if (intWidth <= 0 && intHeight <= 0)
                        {
                            intWidth = defWidth;
                            intHeight = Convert.ToInt32(bmpIn.Height * (decimal)defWidth / bmpIn.Width);
                        }
                        else
                        {
                            if (intWidth > 0)
                            {
                                if (intHeight <= 0)
                                {
                                    intHeight = Convert.ToInt32(bmpIn.Height * (decimal)intWidth / bmpIn.Width);
                                }
                            }
                            else
                            {
                                intWidth = Convert.ToInt32(bmpIn.Width * (decimal)intHeight / bmpIn.Height);
                            }
                        }
                    }
                }
                else
                {
                    //找不到有效圖檔
                    //根據輸出尺寸，使用適合的錯誤用圖檔
                    bmpIn = (Bitmap)Bitmap.FromStream(Util.getStream(Util._ServerPath + ((intWidth > 150) ? Util.Img_NotFound : Util.Legend_Unknow)));
                    //計算縮放比例(此處強制Scale固定為 1.0)
                    if (intWidth <= 0 && intHeight <= 0)
                    {
                        intWidth = bmpIn.Width;
                        intHeight = bmpIn.Height;
                    }
                    else
                    {
                        if (intWidth > 0)
                        {
                            if (intHeight <= 0)
                            {
                                intHeight = Convert.ToInt32(bmpIn.Height * (decimal)intWidth / bmpIn.Width);
                            }
                        }
                        else
                        {
                            intWidth = Convert.ToInt32(bmpIn.Width * (decimal)intHeight / bmpIn.Height);
                        }
                    }

                }

                //開始產生縮圖
                MemoryStream MemStream = new MemoryStream();
                Bitmap bmpOut = new Bitmap(intWidth, intHeight);
                bmpOut = (Bitmap)bmpIn.GetThumbnailImage(intWidth, intHeight, null, new System.IntPtr());
                bmpOut.Save(MemStream, bmpIn.RawFormat);

                //將產生結果輸出
                context.Response.ContentType = string.Format("image/{0}", strExtName);
                MemStream.WriteTo(HttpContext.Current.Response.OutputStream);

            }
            else
            {
                //找不到相關資料
                //根據輸出尺寸，使用適合的錯誤用圖檔
                bmpIn = (Bitmap)Bitmap.FromStream(Util.getStream(Util._ServerPath + ((intWidth > 150) ? Util.Img_NotFound : Util.Legend_Unknow)));
                //計算縮放比例(此處強制Scale固定為 1.0)
                if (intWidth <= 0 && intHeight <= 0)
                {
                    intWidth = bmpIn.Width;
                    intHeight = bmpIn.Height;
                }
                else
                {
                    if (intWidth > 0)
                    {
                        if (intHeight <= 0)
                        {
                            intHeight = Convert.ToInt32(bmpIn.Height * (decimal)intWidth / bmpIn.Width);
                        }
                    }
                    else
                    {
                        intWidth = Convert.ToInt32(bmpIn.Width * (decimal)intHeight / bmpIn.Height);
                    }
                }

                //開始產生縮圖
                MemoryStream MemStream = new MemoryStream();
                Bitmap bmpOut = new Bitmap(intWidth, intHeight);
                bmpOut = (Bitmap)bmpIn.GetThumbnailImage(intWidth, intHeight, null, new System.IntPtr());
                bmpOut.Save(MemStream, bmpIn.RawFormat);

                //將產生結果輸出
                context.Response.ContentType = string.Format("image/{0}", Util.Img_NotFound.Split('.')[1]);
                MemStream.WriteTo(HttpContext.Current.Response.OutputStream);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}