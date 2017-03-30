using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於顯示在view上的資料暫存(資料多以畫面顯示user的格式為主)
/// </summary>
public class TemplateModel //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 加班人公司ID
    /// </summary>
    public String OTCompID { get; set; }

    /// <summary>
    /// 加班人ID
    /// </summary>
    public String OTEmpID { get; set; }

    /// <summary>
    /// 加班人姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 加班人性別
    /// </summary>
    public String Sex { get; set; }

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<TemplateGridData> TemplateGridDataList { get; set; }
}

/// <summary>
/// Grid Data
/// </summary>
public class TemplateGridData //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 加班人公司ID
    /// </summary>
    public String OTCompID { get; set; }

    /// <summary>
    /// 加班人ID
    /// </summary>
    public String OTEmpID { get; set; }

    /// <summary>
    /// 加班人姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 加班人姓名
    /// </summary>
    public String ShowOTEmp { get; set; }

    /// <summary>
    /// 加班人性別
    /// </summary>
    public String Sex { get; set; }

    /// <summary>
    /// 加班人性別
    /// </summary>
    public String ShowSex { get; set; }
}