<%@ WebHandler Language="C#" Class="AttachDirectDownload" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// 從AttachDB取出附件並直接下載
/// **[AttachDB][AttachID]為必要參數，[AttachSeqNo][IsDesc]為選擇性參數
/// </summary>
public class AttachDirectDownload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //嘗試取得參數內容
        string strAttachDB = Util.getRequestQueryStringKey("AttachDB");
        string strAttachID = Util.getRequestQueryStringKey("AttachID"); 
        string strIsDesc = Util.getRequestQueryStringKey("IsDesc","N",true); 
        int intAttachSeqNo = int.Parse(Util.getRequestQueryStringKey("AttachSeqNo","0"));
                
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
                intAttachSeqNo = int.Parse(dt.Rows[0]["SeqNo"].ToString());
                string strFileName = strAttachID + dt.Rows[0]["FileExtName"].ToString();
                Byte[] binFileBody = (Byte[])dt.Rows[0]["FileBody"];
                Util.IsAttachInfoLog(strAttachDB, strAttachID, intAttachSeqNo, "Direct");
                Util.ExportBinary(binFileBody, strFileName);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}