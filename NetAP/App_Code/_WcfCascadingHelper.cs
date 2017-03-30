using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using AjaxControlToolkit;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

/// <summary>
/// Ajax 關聯式選單 WCF 核心模組模板
/// <para>開發人員在參考本模組實作自己的「Ajax 關聯式選單」時，不必異動本模組，</para>
/// <para>只需在自己的應用程式內，實作一個　public partial class WcfCascadingHelper{} </para>
/// <para>並加入所需的Server端 Method() 即可</para>
/// <para>例：public CascadingDropDownNameValue[] MyFun(string knownCategoryValues, string category ,string contextKey) 即可</para>
/// <para>＊＊自行實作的 WcfCascadingHelper 不需添加[ServiceContract][ServiceBehavior][AspNetCompatibilityRequirements]等屬性</para>
/// </summary>
[ServiceContract(Namespace = "", SessionMode = SessionMode.Allowed)]
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, MaxItemsInObjectGraph = 1048576)] //設定傳輸最大1MB
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public partial class WcfCascadingHelper
{

    // 實作所需函數，並加註 [OperationContract] 屬性
    // 函數命名原則為 [APName]_[MethdodName]，譬如 [Util]_[GetCompDeptUser] ，代表是 Util 下用來取得 Comp/Dept/User的方法

    /// <summary>
    /// Util 提供的公用服務，可取得以下關聯資料
    /// <para>CompID / DeptID / UserID</para>
    /// <para>CompID / DeptID / OrganID / UserID</para>
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    [OperationContract]
    public CascadingDropDownNameValue[] Util_GetCompDeptUser(string knownCategoryValues, string category, string contextKey)
    {
        //拆解 CategoryValues
        StringDictionary dicCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        StringDictionary dicContext = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);

        //資料改從 OrgInfo / UserInfo 的方法取得 2016.11.02
        bool IsChkValid = true;
        string[] ValidKeyList = null;
        DataTable dt = null;

        //判斷要處理那一階選單
        switch (category)
        {
            case "CompID":
                IsChkValid = (dicContext["IsChkValid"].ToString().ToUpper() == "Y") ? true : false;
                ValidKeyList = (!string.IsNullOrEmpty(dicContext["ValidKeyList"])) ? dicContext["ValidKeyList"].Split(',') : null;
                if (dicContext["IsReadOnly"].ToString().ToUpper() == "Y")
                {
                    ValidKeyList = dicContext["SelectedValue"].Split(','); //若為「唯讀」，自動限定資料為目前的 SelectedValue
                }

                dt = Util.getDataTable(OrgInfo.getOrgDictionary(true, false, false, IsChkValid, ValidKeyList));
                if (!dt.IsNullOrEmpty())
                {
                    dt.Columns.Remove("Value");
                }

                break;
            case "DeptID":
                IsChkValid = (dicContext["IsChkValid"].ToString().ToUpper() == "Y") ? true : false;
                ValidKeyList = (!string.IsNullOrEmpty(dicContext["ValidKeyList"])) ? dicContext["ValidKeyList"].Split(',') : null;
                if (dicContext["IsReadOnly"].ToString().ToUpper() == "Y")
                {
                    ValidKeyList = dicContext["SelectedValue"].Split(','); //若為「唯讀」，自動限定資料為目前的 SelectedValue
                }

                dt = Util.getDataTable(OrgInfo.getOrgDictionary(false, true, false, IsChkValid, dicCategory["CompID"].Split(','), ValidKeyList));
                if (!dt.IsNullOrEmpty())
                {
                    dt.Columns.Remove("Value");
                }

                break;
            case "OrganID":
                IsChkValid = (dicContext["IsChkValid"].ToString().ToUpper() == "Y") ? true : false;
                ValidKeyList = (!string.IsNullOrEmpty(dicContext["ValidKeyList"])) ? dicContext["ValidKeyList"].Split(',') : null;
                if (dicContext["IsReadOnly"].ToString().ToUpper() == "Y")
                {
                    ValidKeyList = dicContext["SelectedValue"].Split(','); //若為「唯讀」，自動限定資料為目前的 SelectedValue
                }

                dt = Util.getDataTable(OrgInfo.getOrgDictionary(false, false, true, IsChkValid, dicCategory["CompID"].Split(','), dicCategory["DeptID"].Split(','), ValidKeyList));
                if (!dt.IsNullOrEmpty())
                {
                    dt.Columns.Remove("Value");
                }

                break;
            case "UserID":
                IsChkValid = (dicContext["IsChkValid"].ToString().ToUpper() == "Y") ? true : false;
                ValidKeyList = (!string.IsNullOrEmpty(dicContext["ValidKeyList"])) ? dicContext["ValidKeyList"].Split(',') : null;
                if (dicContext["IsReadOnly"].ToString().ToUpper() == "Y")
                {
                    ValidKeyList = dicContext["SelectedValue"].Split(','); //若為「唯讀」，自動限定資料為目前的 SelectedValue
                }

                dt = UserInfo.getUserData(dicCategory["CompID"], dicCategory["DeptID"], dicCategory["OrganID"], IsChkValid);
                if (ValidKeyList != null && ValidKeyList.Length > 0)
                {
                    DataRow[] oRows = dt.Select(string.Format(" UserID in ('{0}') ", Util.getStringJoin(ValidKeyList, "','")));
                    if (oRows != null || oRows.Count() > 0)
                    {
                        dt = oRows.CopyToDataTable();
                    }
                }

                if (!dt.IsNullOrEmpty())
                {
                    dt = dt.DefaultView.ToTable(true, "UserID,UserName".Split(',')); //可能包含兼職資料，預防 UserID 發生重複
                    dt = Util.getDataTable(Util.getDictionary(dt));
                    dt.Columns.Remove("Value");
                }

                break;
            default:
                break;
        }
        return Util.getCascadingArray(dt);
    }


    /// <summary>
    /// AclExpress 提供的服務，可取得以下關聯資料
    /// <para>AreaID / GrantID</para>
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    [OperationContract]
    public CascadingDropDownNameValue[] AclExpress_GetAreaGrant(string knownCategoryValues, string category, string contextKey)
    {
        //2016.06.20 新增
        //拆解 CategoryValues
        StringDictionary dicCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        StringDictionary dicContext = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);

        DbHelper db = new DbHelper(AclExpress._AclDBName);
        string strSQL;
        DataTable dt = new DataTable();
        //判斷要處理那一階選單
        switch (category)
        {
            case "AreaID":
                strSQL = "Select AreaID, AreaID + ' - ' + AreaName + '【' + IsEnabled + '】' as 'AreaInfo' From AclArea ";
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            case "GrantID":
                strSQL = string.Format("Select GrantID, GrantID + ' - ' + GrantName + '【' + IsEnabled + '】' as 'GrantInfo' From AclAreaGrantList Where AreaID = '{0}' ", dicCategory["AreaID"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            default:
                break;
        }
        return Util.getCascadingArray(dt);
    }


    /// <summary>
    /// AclExpress 提供的服務，可取得以下關聯資料
    /// <para>RuleID / AreaID / GrantID</para>
    /// </summary>
    /// <param name="knownCategoryValues"></param>
    /// <param name="category"></param>
    /// <param name="contextKey"></param>
    /// <returns></returns>
    [OperationContract]
    public CascadingDropDownNameValue[] AclExpress_GetAuthRuleAreaGrant(string knownCategoryValues, string category, string contextKey)
    {
        //2016.06.20 新增
        //拆解 CategoryValues
        StringDictionary dicCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        StringDictionary dicContext = CascadingDropDown.ParseKnownCategoryValuesString(contextKey);

        DbHelper db = new DbHelper(AclExpress._AclDBName);
        string strSQL;
        DataTable dt = new DataTable();
        //判斷要處理那一階選單
        switch (category)
        {
            case "RuleID":
                strSQL = "Select Distinct RuleID, RuleID + ' - ' + RuleName + '【' + RuleEnabled + '】' as 'RuleInfo' From viewAclAuthRuleAreaGrantList ";
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            case "AreaID":
                strSQL = string.Format("Select Distinct AreaID, AreaID + ' - ' + AreaName + '【' + AreaEnabled + '】' as 'AreaInfo' From viewAclAuthRuleAreaGrantList Where RuleID = '{0}' ", dicCategory["RuleID"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            case "GrantID":
                strSQL = string.Format("Select Distinct GrantID, GrantID + ' - ' + GrantName + '【' + GrantEnabled + '】' as 'GrantInfo' From viewAclAuthRuleAreaGrantList Where RuleID= '{0}' and AreaID = '{1}' ", dicCategory["RuleID"], dicCategory["AreaID"]);
                dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
                break;
            default:
                break;
        }
        return Util.getCascadingArray(dt);
    }
}
