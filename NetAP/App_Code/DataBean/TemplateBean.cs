using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class TemplateBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 加班人公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 加班人ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 加班人姓名
    /// </summary>
    public String NameN { get; set; }

    /// <summary>
    /// 加班人性別
    /// </summary>
    public String Sex { get; set; }

     /// <summary>
    /// 最後變更者公司ID
    /// </summary>
    public String LastChgComp { get; set; }

     /// <summary>
    /// 最後變更者ID
    /// </summary>
    public String LastChgID { get; set; }

     /// <summary>
    /// 最後變更時間
    /// </summary>
    public String LastChgDate { get; set; }

}