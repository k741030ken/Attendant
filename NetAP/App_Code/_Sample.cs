using System.Collections.Specialized;
using System.Data;
using System.ServiceModel;
using AjaxControlToolkit;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

/// <summary>
/// [法務諮詢] 範例
/// </summary>
public class LegalSample
{
    public static string _LegalSysDBName = "NetSample";
    public static string _LegalConsultFlowID = "LegalMain";
    public static string _LegalConsultCaseTableName = "LegalConsultCase";
    public static int _CaseDiviTimeoutDays = 90; //分案逾時天數
    
    public static string _LegalDocTableName = "LegalDoc";
    public static string _LegalDocKindServiceMethod = "LegalSample_GetLegalDocKind";

    //欄位長度基本變數
    public static int _SubjectMaxLength = 50;
    public static int _KeywordMaxLength = 100;
    public static int _UsageMaxLength = 200;
    public static int _RemarkMaxLength = 200;
    public static int _OutlinedMaxLength = 500;
    public static int _PropOpinionMaxLength = 500;
    public static int _LegalOpinionMaxLength = 1500;

    //附件基本變數
    public static string _DocAttachIDFormat = "LegalDoc-{0}";
    public static string _PropAttachIDFormat = "LegalConsult-{0}-PropAttach";
    public static string _LegalAttachIDFormat = "LegalConsult-{0}-LegalAttach";
    
    public static int _DocAttachMaxQty = 5;
    public static int _DocAttachMaxKB = 0;
    public static int _DocAttachTotKB = 5120;
    public static string _DocAttachExtList = "pdf,doc,xls,ppt,docx,xlsx,pptx";

    public static int _PropAttachMaxQty = 5;
    public static int _PropAttachMaxKB = 0;
    public static int _PropAttachTotKB = 5500;
    public static string _PropAttachExtList = "";

    public static int _LegalAttachMaxQty = 10;
    public static int _LegalAttachMaxKB = 0;
    public static int _LegalAttachTotKB = 10500;
    public static string _LegalAttachExtList = "";
    
    public LegalSample() { }
}

/// <summary>
/// Wcf關聯選單應用範例
/// </summary>
public partial class WcfCascadingHelper
{
    /// <summary>
    /// [法律文件類別]關聯資料範例
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    [OperationContract]
    public CascadingDropDownNameValue[] LegalSample_GetLegalDocKind(string knownCategoryValues, string category, string contextKey)
    {
        //拆解 CategoryValues
        StringDictionary dicCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        StringDictionary dicContext = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);

        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        string strSQL;
        DataTable dt = new DataTable();
        //判斷要處理那一階選單
        switch (category)
        {
            case "Kind1":
                strSQL = "Select KindNo, KindNo + ' - ' + KindName as 'KindInfo' From LegalDocKind Where ParentKindNo = 'Root' ";
                if (dicContext.ContainsKey("IsEnabled"))
                    strSQL += string.Format(" and IsEnabled = '{0}' ", dicContext["IsEnabled"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            case "Kind2":
                strSQL = string.Format("Select KindNo,KindNo + ' - ' + KindName as 'KindInfo' From LegalDocKind Where ParentKindNo = '{0}' ", dicCategory["Kind1"]);
                if (dicContext.ContainsKey("IsEnabled"))
                    strSQL += string.Format(" and IsEnabled = '{0}' ", dicContext["IsEnabled"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            case "Kind3":
                strSQL = string.Format("Select KindNo,KindNo + ' - ' + KindName as 'KindInfo' From LegalDocKind Where ParentKindNo = '{0}' ", dicCategory["Kind2"]);
                if (dicContext.ContainsKey("IsEnabled"))
                    strSQL += string.Format(" and IsEnabled = '{0}' ", dicContext["IsEnabled"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            default:
                break;
        }
        return Util.getCascadingArray(dt);
    }
}
