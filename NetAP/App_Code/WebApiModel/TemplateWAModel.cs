using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於顯示在view上的資料暫存(資料多以畫面顯示user的格式為主)
/// </summary>
public class TemplateWAModel //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    public class RequestModel
    {
        public string Test01 { get; set; }
        public string Test02 { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    public class ResponseModel
    {
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public RetureModel ReturnData { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的資料
    /// </summary>
    public class RetureModel
    {
        public string Test01 { get; set; }
        public string Test02 { get; set; }
    }

}