using System;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [關聯式下拉選單]控制項
/// <para>使用 WCF 作為 Ajax 後端技術，請參考 WcfCascadingHelper 相關說明</para>
/// </summary>
public partial class Util_ucCascadingDropDown : BaseUserControl
{
    #region 屬性定義

    /// <summary>
    /// 是否提供搜尋功能(預設 false)
    /// </summary>
    public bool ucIsSearchEnabled
    {
        //2015.08.12新增
        get
        {
            if (ViewState["_IsSearchEnabled"] == null)
            {
                ViewState["_IsSearchEnabled"] = false;
            }
            return (bool)(ViewState["_IsSearchEnabled"]);
        }
        set
        {
            ViewState["_IsSearchEnabled"] = value;
        }
    }

    /// <summary>
    /// 搜尋框寬度(預設 100)
    /// </summary>
    public int ucSearchBoxWidth
    {
        //2015.08.12新增
        get
        {
            if (ViewState["_SearchBoxWidth"] == null)
            {
                ViewState["_SearchBoxWidth"] = 100;
            }
            return (int)(ViewState["_SearchBoxWidth"]);
        }
        set
        {
            ViewState["_SearchBoxWidth"] = value;
        }
    }

    /// <summary>
    /// 搜尋框浮水印內容
    /// </summary>
    public string ucSearchBoxWaterMarkText
    {
        //2015.08.12新增
        get
        {
            if (ViewState["_SearchBoxWaterMarkText"] == null)
            {
                ViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommSingleSelect_WaterMarkText; //"請輸入搜尋文字"
            }
            return (string)(ViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            ViewState["_SearchBoxWaterMarkText"] = value;
        }
    }

    /// <summary>
    /// 是否垂直排列(預設 false)
    /// </summary>
    public bool ucIsVerticalLayout
    {
        get
        {
            if (ViewState["_IsVerticalLayout"] == null)
            {
                ViewState["_IsVerticalLayout"] = false;
            }
            return (bool)(ViewState["_IsVerticalLayout"]);
        }
        set
        {
            ViewState["_IsVerticalLayout"] = value;
        }
    }

    /// <summary>
    /// 垂直排列時使用的Html標籤(預設 &lt;br&gt;)
    /// </summary>
    public string ucVerticalStyleHtmlTag
    {
        get
        {
            if (ViewState["_VerticalStyleHtmlTag"] == null)
            {
                ViewState["_VerticalStyleHtmlTag"] = "<br />"; //垂直排列時的Html標籤
            }
            return (string)(ViewState["_VerticalStyleHtmlTag"]);
        }
        set
        {
            ViewState["_VerticalStyleHtmlTag"] = value;
        }
    }

    /// <summary>
    /// 是否[多選]的偵測字串(預設 [...])
    /// </summary>
    public string ucMultiSelectIndicator
    {
        get
        {
            if (ViewState["_MultiSelectIndicator"] == null)
            {
                ViewState["_MultiSelectIndicator"] = "…";  //偵測是否變成多選的辨識字串 2014.07.07
            }
            return (string)(ViewState["_MultiSelectIndicator"]);
        }
        set
        {
            ViewState["_MultiSelectIndicator"] = value;
        }
    }

    /// <summary>
    /// [多選]時的選單列數(預設 5)
    /// </summary>
    public string ucMultiSelectRowSize
    {
        get
        {
            if (ViewState["_MultiSelectRowSize"] == null)
            {
                ViewState["_MultiSelectRowSize"] = "5";
            }
            return (string)(ViewState["_MultiSelectRowSize"]);
        }
        set
        {
            ViewState["_MultiSelectRowSize"] = value;
        }
    }

    /// <summary>
    /// 控制項寬度(預設 -1，即自動計算)
    /// </summary>
    public int ucCascadingWidth
    {
        get
        {
            if (ViewState["_CascadingWidth"] == null)
            {
                ViewState["_CascadingWidth"] = -1;
            }
            return (int)(ViewState["_CascadingWidth"]);
        }
        set
        {
            ViewState["_CascadingWidth"] = value;
        }
    }

    /// <summary>
    /// 下拉選單寬度(預設 195)
    /// </summary>
    public int ucDropDownListWidth
    {
        get
        {
            if (ViewState["_DropDownListWidth"] == null)
            {
                ViewState["_DropDownListWidth"] = 195;
            }
            return (int)(ViewState["_DropDownListWidth"]);
        }
        set
        {
            ViewState["_DropDownListWidth"] = value;
        }
    }

    /// <summary>
    /// Service 服務路徑(預設 Util._CommCascadeServicePath)
    /// </summary>
    public string ucServicePath
    {
        get
        {
            if (ViewState["_ServicePath"] == null)
            {
                ViewState["_ServicePath"] = Util._CommCascadeServicePath;
            }
            return ViewState["_ServicePath"].ToString();
        }
        set
        {
            ViewState["_ServicePath"] = value;
        }
    }

    /// <summary>
    /// Service 服務方法(預設 Util._CommCascadeServiceMethod)
    /// </summary>
    public string ucServiceMethod
    {
        get
        {
            if (ViewState["_ServiceMethod"] == null)
            {
                ViewState["_ServiceMethod"] = Util._CommCascadeServiceMethod;
            }
            return ViewState["_ServiceMethod"].ToString();
        }
        set
        {
            ViewState["_ServiceMethod"] = value;
        }
    }

    /// <summary>
    /// 選單載入資料時的訊息
    /// </summary>
    public string ucLoadingText
    {
        get
        {
            if (ViewState["_LoadingText"] == null)
            {
                ViewState["_LoadingText"] = RS.Resources.Msg_Waiting;
            }
            return ViewState["_LoadingText"].ToString();
        }
        set
        {
            ViewState["_LoadingText"] = value;
        }
    }

    /// <summary>
    /// 控制項CSS
    /// </summary>
    public string ucCascadingCssClass
    {
        get
        {
            if (ViewState["_CascadingCssClass"] == null)
            {
                ViewState["_CascadingCssClass"] = "";
            }
            return ViewState["_CascadingCssClass"].ToString();
        }
        set
        {
            ViewState["_CascadingCssClass"] = value;
        }
    }

    /// <summary>
    /// 下拉選單CSS
    /// </summary>
    public string ucDropDownListCssClass
    {
        get
        {
            if (ViewState["_DropDownListCssClass"] == null)
            {
                ViewState["_DropDownListCssClass"] = "";
            }
            return ViewState["_DropDownListCssClass"].ToString();
        }
        set
        {
            ViewState["_DropDownListCssClass"] = value;
        }
    }

    /// <summary>
    /// 下拉選單唯讀時的CSS
    /// </summary>
    public string ucDropDownListReadOnlyCssClass
    {
        get
        {
            if (ViewState["_DropDownListReadOnlyCssClass"] == null)
            {
                ViewState["_DropDownListReadOnlyCssClass"] = "Util_clsDropDownListReadOnly";
            }
            return ViewState["_DropDownListReadOnlyCssClass"].ToString();
        }
        set
        {
            ViewState["_DropDownListReadOnlyCssClass"] = value;
        }
    }

    /// <summary>
    /// 是否使用第一層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled01
    {
        get
        {
            if (ViewState["_IsDropDownListEnabled01"] == null)
            {
                ViewState["_IsDropDownListEnabled01"] = true;
            }
            return (bool)(ViewState["_IsDropDownListEnabled01"]);
        }
        set
        {
            ViewState["_IsDropDownListEnabled01"] = value;
        }
    }

    /// <summary>
    /// 是否使用第二層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled02
    {
        get
        {
            if (ViewState["_IsDropDownListEnabled02"] == null)
            {
                ViewState["_IsDropDownListEnabled02"] = true;
            }
            return (bool)(ViewState["_IsDropDownListEnabled02"]);
        }
        set
        {
            ViewState["_IsDropDownListEnabled02"] = value;
        }
    }

    /// <summary>
    /// 是否使用第三層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled03
    {
        get
        {
            if (ViewState["_IsDropDownListEnabled03"] == null)
            {
                ViewState["_IsDropDownListEnabled03"] = true;
            }
            return (bool)(ViewState["_IsDropDownListEnabled03"]);
        }
        set
        {
            ViewState["_IsDropDownListEnabled03"] = value;
        }
    }

    /// <summary>
    /// 是否使用第四層下拉選單(預設 false)
    /// </summary>
    public bool ucDropDownListEnabled04
    {
        get
        {
            if (ViewState["_IsDropDownListEnabled04"] == null)
            {
                ViewState["_IsDropDownListEnabled04"] = false;
            }
            return (bool)(ViewState["_IsDropDownListEnabled04"]);
        }
        set
        {
            ViewState["_IsDropDownListEnabled04"] = value;
        }
    }

    /// <summary>
    /// 是否使用第五層下拉選單(預設 false)
    /// </summary>
    public bool ucDropDownListEnabled05
    {
        get
        {
            if (ViewState["_IsDropDownListEnabled05"] == null)
            {
                ViewState["_IsDropDownListEnabled05"] = false;
            }
            return (bool)(ViewState["_IsDropDownListEnabled05"]);
        }
        set
        {
            ViewState["_IsDropDownListEnabled05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單提示訊息
    /// </summary>
    public string ucPromptText01
    {
        get
        {
            if (ViewState["_PromptText01"] == null)
            {
                ViewState["_PromptText01"] = RS.Resources.Msg_DDL_EmptyItem; 
            }
            return ViewState["_PromptText01"].ToString();
        }
        set
        {
            ViewState["_PromptText01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單提示訊息
    /// </summary>
    public string ucPromptText02
    {
        get
        {
            if (ViewState["_PromptText02"] == null)
            {
                ViewState["_PromptText02"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return ViewState["_PromptText02"].ToString();
        }
        set
        {
            ViewState["_PromptText02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單提示訊息
    /// </summary>
    public string ucPromptText03
    {
        get
        {
            if (ViewState["_PromptText03"] == null)
            {
                ViewState["_PromptText03"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return ViewState["_PromptText03"].ToString();
        }
        set
        {
            ViewState["_PromptText03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單提示訊息
    /// </summary>
    public string ucPromptText04
    {
        get
        {
            if (ViewState["_PromptText04"] == null)
            {
                ViewState["_PromptText04"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return ViewState["_PromptText04"].ToString();
        }
        set
        {
            ViewState["_PromptText04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單提示訊息
    /// </summary>
    public string ucPromptText05
    {
        get
        {
            if (ViewState["_PromptText05"] == null)
            {
                ViewState["_PromptText05"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return ViewState["_PromptText05"].ToString();
        }
        set
        {
            ViewState["_PromptText05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單類別
    /// </summary>
    public string ucCategory01
    {
        get
        {
            if (ViewState["_Category01"] == null)
            {
                ViewState["_Category01"] = "Category01";
            }
            return ViewState["_Category01"].ToString();
        }
        set
        {
            ViewState["_Category01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單類別
    /// </summary>
    public string ucCategory02
    {
        get
        {
            if (ViewState["_Category02"] == null)
            {
                ViewState["_Category02"] = "Category02";
            }
            return ViewState["_Category02"].ToString();
        }
        set
        {
            ViewState["_Category02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單類別
    /// </summary>
    public string ucCategory03
    {
        get
        {
            if (ViewState["_Category03"] == null)
            {
                ViewState["_Category03"] = "Category03";
            }
            return ViewState["_Category03"].ToString();
        }
        set
        {
            ViewState["_Category03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單類別
    /// </summary>
    public string ucCategory04
    {
        get
        {
            if (ViewState["_Category04"] == null)
            {
                ViewState["_Category04"] = "Category04";
            }
            return ViewState["_Category04"].ToString();
        }
        set
        {
            ViewState["_Category04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單類別
    /// </summary>
    public string ucCategory05
    {
        get
        {
            if (ViewState["_Category05"] == null)
            {
                ViewState["_Category05"] = "Category05";
            }
            return ViewState["_Category05"].ToString();
        }
        set
        {
            ViewState["_Category05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單選定的值
    /// </summary>
    public string ucSelectedValue01
    {
        get
        {
            return ddl01.SelectedValue;
        }
    }

    /// <summary>
    /// 第二層下拉選單選定的值
    /// </summary>
    public string ucSelectedValue02
    {
        get
        {
            return ddl02.SelectedValue;
        }
    }

    /// <summary>
    /// 第三層下拉選單選定的值
    /// </summary>
    public string ucSelectedValue03
    {
        get
        {
            return ddl03.SelectedValue;
        }
    }

    /// <summary>
    /// 第四層下拉選單選定的值
    /// </summary>
    public string ucSelectedValue04
    {
        get
        {
            return ddl04.SelectedValue;
        }
    }

    /// <summary>
    /// 第五層下拉選單選定的值
    /// </summary>
    public string ucSelectedValue05
    {
        get
        {
            return ddl05.SelectedValue;
        }
    }

    /// <summary>
    /// 第一層下拉選單選定項目的文字
    /// </summary>
    public string ucSelectedText01
    {
        get
        {
            return ddl01.SelectedItem.Text;
        }
    }

    /// <summary>
    /// 第二層下拉選單選定項目的文字
    /// </summary>
    public string ucSelectedText02
    {
        get
        {
            return ddl02.SelectedItem.Text;
        }
    }

    /// <summary>
    /// 第三層下拉選單選定項目的文字
    /// </summary>
    public string ucSelectedText03
    {
        get
        {
            return ddl03.SelectedItem.Text;
        }
    }

    /// <summary>
    /// 第四層下拉選單選定項目的文字
    /// </summary>
    public string ucSelectedText04
    {
        get
        {
            return ddl04.SelectedItem.Text;
        }
    }

    /// <summary>
    /// 第五層下拉選單選定項目的文字
    /// </summary>
    public string ucSelectedText05
    {
        get
        {
            return ddl05.SelectedItem.Text;
        }
    }

    /// <summary>
    /// 第一層下拉選單預設值
    /// </summary>
    public string ucDefaultSelectedValue01
    {
        get
        {
            if (ViewState["_DefaultSelectedValue01"] == null)
            {
                ViewState["_DefaultSelectedValue01"] = "";
            }
            return (string)(ViewState["_DefaultSelectedValue01"]);
        }
        set
        {
            ViewState["_DefaultSelectedValue01"] = value;
            CascadingDropDown1.SelectedValue = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單預設值
    /// </summary>
    public string ucDefaultSelectedValue02
    {
        get
        {
            if (ViewState["_DefaultSelectedValue02"] == null)
            {
                ViewState["_DefaultSelectedValue02"] = "";
            }
            return (string)(ViewState["_DefaultSelectedValue02"]);
        }
        set
        {
            ViewState["_DefaultSelectedValue02"] = value;
            CascadingDropDown2.SelectedValue = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單預設值
    /// </summary>
    public string ucDefaultSelectedValue03
    {
        get
        {
            if (ViewState["_DefaultSelectedValue03"] == null)
            {
                ViewState["_DefaultSelectedValue03"] = "";
            }
            return (string)(ViewState["_DefaultSelectedValue03"]);
        }
        set
        {
            ViewState["_DefaultSelectedValue03"] = value;
            CascadingDropDown3.SelectedValue = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單預設值
    /// </summary>
    public string ucDefaultSelectedValue04
    {
        get
        {
            if (ViewState["_DefaultSelectedValue04"] == null)
            {
                ViewState["_DefaultSelectedValue04"] = "";
            }
            return (string)(ViewState["_DefaultSelectedValue04"]);
        }
        set
        {
            ViewState["_DefaultSelectedValue04"] = value;
            CascadingDropDown4.SelectedValue = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單預設值
    /// </summary>
    public string ucDefaultSelectedValue05
    {
        get
        {
            if (ViewState["_DefaultSelectedValue05"] == null)
            {
                ViewState["_DefaultSelectedValue05"] = "";
            }
            return (string)(ViewState["_DefaultSelectedValue05"]);
        }
        set
        {
            ViewState["_DefaultSelectedValue05"] = value;
            CascadingDropDown5.SelectedValue = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire01
    {
        get
        {
            if (ViewState["_IsRequire01"] == null)
            {
                ViewState["_IsRequire01"] = false;
            }
            return (bool)ViewState["_IsRequire01"];
        }
        set
        {
            ViewState["_IsRequire01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire02
    {
        get
        {
            if (ViewState["_IsRequire02"] == null)
            {
                ViewState["_IsRequire02"] = false;
            }
            return (bool)ViewState["_IsRequire02"];
        }
        set
        {
            ViewState["_IsRequire02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire03
    {
        get
        {
            if (ViewState["_IsRequire03"] == null)
            {
                ViewState["_IsRequire03"] = false;
            }
            return (bool)ViewState["_IsRequire03"];
        }
        set
        {
            ViewState["_IsRequire03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire04
    {
        get
        {
            if (ViewState["_IsRequire04"] == null)
            {
                ViewState["_IsRequire04"] = false;
            }
            return (bool)ViewState["_IsRequire04"];
        }
        set
        {
            ViewState["_IsRequire04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire05
    {
        get
        {
            if (ViewState["_IsRequire05"] == null)
            {
                ViewState["_IsRequire05"] = false;
            }
            return (bool)ViewState["_IsRequire05"];
        }
        set
        {
            ViewState["_IsRequire05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg01
    {
        get
        {
            if (ViewState["_RequireMsg01"] == null)
            {
                ViewState["_RequireMsg01"] = RS.Resources.Msg_RequireInput;
            }
            return (string)ViewState["_RequireMsg01"];
        }
        set
        {
            ViewState["_RequireMsg01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg02
    {
        get
        {
            if (ViewState["_RequireMsg02"] == null)
            {
                ViewState["_RequireMsg02"] = RS.Resources.Msg_RequireInput;
            }
            return (string)ViewState["_RequireMsg02"];
        }
        set
        {
            ViewState["_RequireMsg02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg03
    {
        get
        {
            if (ViewState["_RequireMsg03"] == null)
            {
                ViewState["_RequireMsg03"] = RS.Resources.Msg_RequireInput;
            }
            return (string)ViewState["_RequireMsg03"];
        }
        set
        {
            ViewState["_RequireMsg03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg04
    {
        get
        {
            if (ViewState["_RequireMsg04"] == null)
            {
                ViewState["_RequireMsg04"] = RS.Resources.Msg_RequireInput;
            }
            return (string)ViewState["_RequireMsg04"];
        }
        set
        {
            ViewState["_RequireMsg04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg05
    {
        get
        {
            if (ViewState["_RequireMsg05"] == null)
            {
                ViewState["_RequireMsg05"] = RS.Resources.Msg_RequireInput;
            }
            return (string)ViewState["_RequireMsg05"];
        }
        set
        {
            ViewState["_RequireMsg05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly01
    {
        get
        {
            if (ViewState["_IsReadOnly01"] == null)
            {
                ViewState["_IsReadOnly01"] = false;
            }
            return (bool)(ViewState["_IsReadOnly01"]);
        }
        set
        {
            ViewState["_IsReadOnly01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly02
    {
        get
        {
            if (ViewState["_IsReadOnly02"] == null)
            {
                ViewState["_IsReadOnly02"] = false;
            }
            return (bool)(ViewState["_IsReadOnly02"]);
        }
        set
        {
            ViewState["_IsReadOnly02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly03
    {
        get
        {
            if (ViewState["_IsReadOnly03"] == null)
            {
                ViewState["_IsReadOnly03"] = false;
            }
            return (bool)(ViewState["_IsReadOnly03"]);
        }
        set
        {
            ViewState["_IsReadOnly03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly04
    {
        get
        {
            if (ViewState["_IsReadOnly04"] == null)
            {
                ViewState["_IsReadOnly04"] = false;
            }
            return (bool)(ViewState["_IsReadOnly04"]);
        }
        set
        {
            ViewState["_IsReadOnly04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly05
    {
        get
        {
            if (ViewState["_IsReadOnly05"] == null)
            {
                ViewState["_IsReadOnly05"] = false;
            }
            return (bool)(ViewState["_IsReadOnly05"]);
        }
        set
        {
            ViewState["_IsReadOnly05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid01
    {
        get
        {
            if (ViewState["_IsChkValid01"] == null)
            {
                ViewState["_IsChkValid01"] = true;
            }
            return (bool)ViewState["_IsChkValid01"];
        }
        set
        {
            ViewState["_IsChkValid01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid02
    {
        get
        {
            if (ViewState["_IsChkValid02"] == null)
            {
                ViewState["_IsChkValid02"] = true;
            }
            return (bool)ViewState["_IsChkValid02"];
        }
        set
        {
            ViewState["_IsChkValid02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid03
    {
        get
        {
            if (ViewState["_IsChkValid03"] == null)
            {
                ViewState["_IsChkValid03"] = true;
            }
            return (bool)ViewState["_IsChkValid03"];
        }
        set
        {
            ViewState["_IsChkValid03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid04
    {
        get
        {
            if (ViewState["_IsChkValid04"] == null)
            {
                ViewState["_IsChkValid04"] = true;
            }
            return (bool)ViewState["_IsChkValid04"];
        }
        set
        {
            ViewState["_IsChkValid04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid05
    {
        get
        {
            if (ViewState["_IsChkValid05"] == null)
            {
                ViewState["_IsChkValid05"] = true;
            }
            return (bool)ViewState["_IsChkValid05"];
        }
        set
        {
            ViewState["_IsChkValid05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單項目有效鍵值清單
    /// </summary>
    public string ucValidKeyList01
    {
        //2016.08.16 新增
        get
        {
            if (ViewState["_ValidKeyList01"] == null)
            {
                ViewState["_ValidKeyList01"] = "";
            }
            return (string)ViewState["_ValidKeyList01"];
        }
        set
        {
            ViewState["_ValidKeyList01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單項目有效鍵值清單
    /// </summary>
    public string ucValidKeyList02
    {
        //2016.08.16 新增
        get
        {
            if (ViewState["_ValidKeyList02"] == null)
            {
                ViewState["_ValidKeyList02"] = "";
            }
            return (string)ViewState["_ValidKeyList02"];
        }
        set
        {
            ViewState["_ValidKeyList02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單項目有效鍵值清單
    /// </summary>
    public string ucValidKeyList03
    {
        //2016.08.16 新增
        get
        {
            if (ViewState["_ValidKeyList03"] == null)
            {
                ViewState["_ValidKeyList03"] = "";
            }
            return (string)ViewState["_ValidKeyList03"];
        }
        set
        {
            ViewState["_ValidKeyList03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單項目有效鍵值清單
    /// </summary>
    public string ucValidKeyList04
    {
        //2016.08.16 新增
        get
        {
            if (ViewState["_ValidKeyList04"] == null)
            {
                ViewState["_ValidKeyList04"] = "";
            }
            return (string)ViewState["_ValidKeyList04"];
        }
        set
        {
            ViewState["_ValidKeyList04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單項目有效鍵值清單
    /// </summary>
    public string ucValidKeyList05
    {
        //2016.08.16 新增
        get
        {
            if (ViewState["_ValidKeyList05"] == null)
            {
                ViewState["_ValidKeyList05"] = "";
            }
            return (string)ViewState["_ValidKeyList05"];
        }
        set
        {
            ViewState["_ValidKeyList05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData01
    {
        get
        {
            if (ViewState["_ContextData01"] == null)
            {
                ViewState["_ContextData01"] = "";
            }
            return (string)(ViewState["_ContextData01"]);
        }
        set
        {
            ViewState["_ContextData01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData02
    {
        get
        {
            if (ViewState["_ContextData02"] == null)
            {
                ViewState["_ContextData02"] = "";
            }
            return (string)(ViewState["_ContextData02"]);
        }
        set
        {
            ViewState["_ContextData02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData03
    {
        get
        {
            if (ViewState["_ContextData03"] == null)
            {
                ViewState["_ContextData03"] = "";
            }
            return (string)(ViewState["_ContextData03"]);
        }
        set
        {
            ViewState["_ContextData03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData04
    {
        get
        {
            if (ViewState["_ContextData04"] == null)
            {
                ViewState["_ContextData04"] = "";
            }
            return (string)(ViewState["_ContextData04"]);
        }
        set
        {
            ViewState["_ContextData04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData05
    {
        get
        {
            if (ViewState["_ContextData05"] == null)
            {
                ViewState["_ContextData05"] = "";
            }
            return (string)(ViewState["_ContextData05"]);
        }
        set
        {
            ViewState["_ContextData05"] = value;
        }
    }


    /// <summary>
    /// 是否使用切換可見功能(預設 false)
    /// </summary>
    public bool ucIsToggleVisibility
    {
        get
        {
            if (ViewState["_IsToggleVisibility"] == null)
            {
                ViewState["_IsToggleVisibility"] = false;
            }
            return (bool)(ViewState["_IsToggleVisibility"]);
        }
        set
        {
            ViewState["_IsToggleVisibility"] = value;
            if (value == true)
            {
                chkVisibility.Visible = true;
                divDataArea.Style["visibility"] = "";
            }
            else
            {
                chkVisibility.Visible = false;
                divDataArea.Style["visibility"] = "hidden";
            }
        }
    }

    /// <summary>
    /// 控制項可見狀態
    /// </summary>
    public bool ucIsVisibility
    {
        get
        {
            return chkVisibility.Checked;
        }
        set
        {
            chkVisibility.Checked = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭(空白時自動隱藏)
    /// </summary>
    public string ucCaption
    {
        get
        {
            return labCaption.Text.Trim();
        }

        set
        {
            value = value.Trim();
            labCaption.Text = value;
            if (string.IsNullOrEmpty(value))
            {
                labCaption.Visible = false;
            }
            else
            {
                labCaption.Visible = true;
            }
        }
    }

    /// <summary>
    /// 控制項顯示抬頭寬度(預設 80)
    /// </summary>
    public int ucCaptionWidth
    {
        get
        {
            return int.Parse(labCaption.Width.ToString().Replace("px", ""));
        }
        set
        {
            labCaption.Width = Unit.Pixel(value);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        bool IsInit = (ViewState["_IsInit"] != null) ? (bool)ViewState["_IsInit"] : false;
        if (!IsInit)
        {
            Refresh();
        }

        if (IsPostBack)
        {
            if (!string.IsNullOrEmpty(ucSelectedValue01))
            {
                ucDefaultSelectedValue01 = ucSelectedValue01;
            }
            if (!string.IsNullOrEmpty(ucSelectedValue02))
            {
                ucDefaultSelectedValue02 = ucSelectedValue02;
            }
            if (!string.IsNullOrEmpty(ucSelectedValue03))
            {
                ucDefaultSelectedValue03 = ucSelectedValue03;
            }
            if (!string.IsNullOrEmpty(ucSelectedValue04))
            {
                ucDefaultSelectedValue04 = ucSelectedValue04;
            }
            if (!string.IsNullOrEmpty(ucSelectedValue05))
            {
                ucDefaultSelectedValue05 = ucSelectedValue05;
            }
        }
    }


    /// <summary>
    /// 套用預設階層選單
    /// <para>IsOrganID = false，則為 CompID / DeptID / UserID</para>
    /// <para>IsOrganID = true ，則為 CompID / DeptID / OrganID / UserID</para>
    /// </summary>
    /// <param name="IsOrganID">是否含OrganID，預設 false</param>
    public void SetDefault(bool IsOrganID = false)
    {
        labErrMsg.Text = "";
        labErrMsg.Visible = false;
        divDataArea.Visible = true;

        ucIsVerticalLayout = false;
        ucCascadingCssClass = ucCascadingCssClass;
        ucCascadingWidth = ucCascadingWidth;
        ucDropDownListCssClass = ucDropDownListCssClass;
        ucDropDownListReadOnlyCssClass = ucDropDownListReadOnlyCssClass;
        ucDropDownListWidth = ucDropDownListWidth;
        ucLoadingText = RS.Resources.Msg_Waiting;

        ucRequireMsg01 = "*";
        ucRequireMsg02 = "*";
        ucRequireMsg03 = "*";
        ucRequireMsg04 = "*";
        ucRequireMsg05 = "*";

        if (IsOrganID)
        {
            ucCategory01 = "CompID";
            ucCategory02 = "DeptID";
            ucCategory03 = "OrganID";
            ucCategory04 = "UserID";
            ucCategory05 = "";

            ucPromptText01 = RS.Resources.CommCascadeSelect_SelectComp;
            ucPromptText02 = RS.Resources.CommCascadeSelect_SelectDept;
            ucPromptText03 = RS.Resources.CommCascadeSelect_SelectOrgan;
            ucPromptText04 = RS.Resources.CommCascadeSelect_SelectUser;
            ucPromptText05 = RS.Resources.Msg_DDL_EmptyItem;

            ucDropDownListEnabled01 = true;
            ucDropDownListEnabled02 = true;
            ucDropDownListEnabled03 = true;
            ucDropDownListEnabled04 = true;
            ucDropDownListEnabled05 = false;
        }
        else
        {
            ucCategory01 = "CompID";
            ucCategory02 = "DeptID";
            ucCategory03 = "UserID";
            ucCategory04 = "";
            ucCategory05 = "";

            ucPromptText01 = RS.Resources.CommCascadeSelect_SelectComp;
            ucPromptText02 = RS.Resources.CommCascadeSelect_SelectDept;
            ucPromptText03 = RS.Resources.CommCascadeSelect_SelectUser;
            ucPromptText04 = RS.Resources.Msg_DDL_EmptyItem;
            ucPromptText05 = RS.Resources.Msg_DDL_EmptyItem;

            ucDropDownListEnabled01 = true;
            ucDropDownListEnabled02 = true;
            ucDropDownListEnabled03 = true;
            ucDropDownListEnabled04 = false;
            ucDropDownListEnabled05 = false;
        }

        ucDefaultSelectedValue01 = "";
        ucDefaultSelectedValue02 = "";
        ucDefaultSelectedValue03 = "";
        ucDefaultSelectedValue04 = "";
        ucDefaultSelectedValue05 = "";

        ucIsReadOnly01 = false;
        ucIsReadOnly02 = false;
        ucIsReadOnly03 = false;
        ucIsReadOnly04 = false;
        ucIsReadOnly05 = false;
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {

        //chkVisibility 相關設定
        string strRequireJS = "";
        if (ucIsRequire01)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator1.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        if (ucIsRequire02)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator2.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        if (ucIsRequire03)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator3.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        if (ucIsRequire04)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator4.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        if (ucIsRequire05)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator5.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        strRequireJS += string.Format("Util_ChkBoxToggleVisibility('{0}', '{1}');", chkVisibility.ClientID, divDataArea.ClientID);
        chkVisibility.Attributes.Add("onclick", strRequireJS);
        if (chkVisibility.Checked)
        {
            divDataArea.Style["visibility"] = "";
        }
        else
        {
            divDataArea.Style["visibility"] = "hidden";
        }


        labErrMsg.Text = "";
        divDataArea.Visible = true;
        labErrMsg.Visible = false;
        if (!string.IsNullOrEmpty(ucServicePath) && !string.IsNullOrEmpty(ucServiceMethod))
        {
            ViewState["_IsInit"] = true;
            if (!string.IsNullOrEmpty(ucCascadingCssClass)) divDataArea.Attributes.Add("Class", ucCascadingCssClass);
            if (ucCascadingWidth > 0) divDataArea.Style.Add("Width", ucCascadingWidth.ToString() + "px");

            CascadingDropDown1.ServicePath = ucServicePath;
            CascadingDropDown2.ServicePath = ucServicePath;
            CascadingDropDown3.ServicePath = ucServicePath;
            CascadingDropDown4.ServicePath = ucServicePath;
            CascadingDropDown5.ServicePath = ucServicePath;

            CascadingDropDown1.ServiceMethod = ucServiceMethod;
            CascadingDropDown2.ServiceMethod = ucServiceMethod;
            CascadingDropDown3.ServiceMethod = ucServiceMethod;
            CascadingDropDown4.ServiceMethod = ucServiceMethod;
            CascadingDropDown5.ServiceMethod = ucServiceMethod;

            CascadingDropDown1.LoadingText = ucLoadingText;
            CascadingDropDown2.LoadingText = ucLoadingText;
            CascadingDropDown3.LoadingText = ucLoadingText;
            CascadingDropDown4.LoadingText = ucLoadingText;
            CascadingDropDown5.LoadingText = ucLoadingText;

            CascadingDropDown1.Category = ucCategory01;
            CascadingDropDown2.Category = ucCategory02;
            CascadingDropDown3.Category = ucCategory03;
            CascadingDropDown4.Category = ucCategory04;
            CascadingDropDown5.Category = ucCategory05;

            RequiredFieldValidator1.ErrorMessage = ucRequireMsg01;
            RequiredFieldValidator2.ErrorMessage = ucRequireMsg02;
            RequiredFieldValidator3.ErrorMessage = ucRequireMsg03;
            RequiredFieldValidator4.ErrorMessage = ucRequireMsg04;
            RequiredFieldValidator5.ErrorMessage = ucRequireMsg05;

            RequiredFieldValidator1.Enabled = ucIsRequire01;
            RequiredFieldValidator2.Enabled = ucIsRequire02;
            RequiredFieldValidator3.Enabled = ucIsRequire03;
            RequiredFieldValidator4.Enabled = ucIsRequire04;
            RequiredFieldValidator5.Enabled = ucIsRequire05;

            ddl01.CssClass = ucIsReadOnly01 ? ucDropDownListReadOnlyCssClass : ucDropDownListCssClass;
            ddl02.CssClass = ucIsReadOnly02 ? ucDropDownListReadOnlyCssClass : ucDropDownListCssClass;
            ddl03.CssClass = ucIsReadOnly03 ? ucDropDownListReadOnlyCssClass : ucDropDownListCssClass;
            ddl04.CssClass = ucIsReadOnly04 ? ucDropDownListReadOnlyCssClass : ucDropDownListCssClass;
            ddl05.CssClass = ucIsReadOnly05 ? ucDropDownListReadOnlyCssClass : ucDropDownListCssClass;

            ddl01.BorderWidth = 1;
            ddl01.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            ddl02.BorderWidth = 1;
            ddl02.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            ddl03.BorderWidth = 1;
            ddl03.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            ddl04.BorderWidth = 1;
            ddl04.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");
            ddl05.BorderWidth = 1;
            ddl05.BorderColor = System.Drawing.ColorTranslator.FromHtml("#C0C0C0");

            ddl01.Width = ucDropDownListWidth;
            ddl02.Width = ucDropDownListWidth;
            ddl03.Width = ucDropDownListWidth;
            ddl04.Width = ucDropDownListWidth;
            ddl05.Width = ucDropDownListWidth;

            //2014.07.25 加入
            ddl01.Enabled = ucIsReadOnly01 ? false : true;
            ddl02.Enabled = ucIsReadOnly02 ? false : true;
            ddl03.Enabled = ucIsReadOnly03 ? false : true;
            ddl04.Enabled = ucIsReadOnly04 ? false : true;
            ddl05.Enabled = ucIsReadOnly05 ? false : true;

            CascadingDropDown1.PromptText = ucIsReadOnly01 ? "" : ucPromptText01;
            CascadingDropDown2.PromptText = ucIsReadOnly02 ? "" : ucPromptText02;
            CascadingDropDown3.PromptText = ucIsReadOnly03 ? "" : ucPromptText03;
            CascadingDropDown4.PromptText = ucIsReadOnly04 ? "" : ucPromptText04;
            CascadingDropDown5.PromptText = ucIsReadOnly05 ? "" : ucPromptText05;

            //2014.07.07 加入自訂 ContextDataXX 
            //2016.08.16 加入 ValidKeyListXX
            CascadingDropDown1.ContextKey = string.Format("IsReadOnly:{0};SelectedValue:{1};IsChkValid:{2};ValidKeyList:{3};", ucIsReadOnly01 ? "Y" : "N", CascadingDropDown1.SelectedValue, ucIsChkValid01 ? "Y" : "N", ucValidKeyList01) + ucContextData01;
            CascadingDropDown2.ContextKey = string.Format("IsReadOnly:{0};SelectedValue:{1};IsChkValid:{2};ValidKeyList:{3};", ucIsReadOnly02 ? "Y" : "N", CascadingDropDown2.SelectedValue, ucIsChkValid02 ? "Y" : "N", ucValidKeyList02) + ucContextData02;
            CascadingDropDown3.ContextKey = string.Format("IsReadOnly:{0};SelectedValue:{1};IsChkValid:{2};ValidKeyList:{3};", ucIsReadOnly03 ? "Y" : "N", CascadingDropDown3.SelectedValue, ucIsChkValid03 ? "Y" : "N", ucValidKeyList03) + ucContextData03;
            CascadingDropDown4.ContextKey = string.Format("IsReadOnly:{0};SelectedValue:{1};IsChkValid:{2};ValidKeyList:{3};", ucIsReadOnly04 ? "Y" : "N", CascadingDropDown4.SelectedValue, ucIsChkValid04 ? "Y" : "N", ucValidKeyList04) + ucContextData04;
            CascadingDropDown5.ContextKey = string.Format("IsReadOnly:{0};SelectedValue:{1};IsChkValid:{2};ValidKeyList:{3};", ucIsReadOnly05 ? "Y" : "N", CascadingDropDown5.SelectedValue, ucIsChkValid05 ? "Y" : "N", ucValidKeyList05) + ucContextData05;

            ddl01.Visible = ucDropDownListEnabled01;
            ddl02.Visible = ucDropDownListEnabled02;
            ddl03.Visible = ucDropDownListEnabled03;
            ddl04.Visible = ucDropDownListEnabled04;
            ddl05.Visible = ucDropDownListEnabled05;

            //加入下拉選單，動態依據項目顯示內容，自動變成「多選」的功能(利用JS處理)  2014.07.07
            string strJS = "";
            if (ddl01.Visible && ddl02.Visible)
            {
                strJS = "var oParent = document.getElementById('" + ddl01.ClientID + "');";
                strJS += "var oChild = document.getElementById('" + ddl02.ClientID + "');";
                strJS += "if (oParent.options[oParent.selectedIndex].text.indexOf('" + ucMultiSelectIndicator + "') > 0) {";
                strJS += "    oChild.multiple = 'multiple';oChild.size ='" + ucMultiSelectRowSize + "';";
                strJS += "} else {";
                strJS += "    oChild.multiple = '';oChild.size ='1';";
                strJS += "} ";
                ddl01.Attributes.Add("onchange", strJS);
            }

            if (ddl02.Visible && ddl03.Visible)
            {
                strJS = "var oParent = document.getElementById('" + ddl02.ClientID + "');";
                strJS += "var oChild = document.getElementById('" + ddl03.ClientID + "');";
                strJS += "if (oParent.options[oParent.selectedIndex].text.indexOf('" + ucMultiSelectIndicator + "') > 0) {";
                strJS += "    oChild.multiple = 'multiple';oChild.size ='" + ucMultiSelectRowSize + "';";
                strJS += "} else {";
                strJS += "    oChild.multiple = '';oChild.size ='1';";
                strJS += "} ";
                ddl02.Attributes.Add("onchange", strJS);
            }

            if (ddl03.Visible && ddl04.Visible)
            {
                strJS = "var oParent = document.getElementById('" + ddl03.ClientID + "');";
                strJS += "var oChild = document.getElementById('" + ddl04.ClientID + "');";
                strJS += "if (oParent.options[oParent.selectedIndex].text.indexOf('" + ucMultiSelectIndicator + "') > 0) {";
                strJS += "    oChild.multiple = 'multiple';oChild.size ='" + ucMultiSelectRowSize + "';";
                strJS += "} else {";
                strJS += "    oChild.multiple = '';oChild.size ='1';";
                strJS += "} ";
                ddl03.Attributes.Add("onchange", strJS);
            }

            if (ddl04.Visible && ddl05.Visible)
            {
                strJS = "var oParent = document.getElementById('" + ddl04.ClientID + "');";
                strJS += "var oChild = document.getElementById('" + ddl05.ClientID + "');";
                strJS += "if (oParent.options[oParent.selectedIndex].text.indexOf('" + ucMultiSelectIndicator + "') > 0) {";
                strJS += "    oChild.multiple = 'multiple';oChild.size ='" + ucMultiSelectRowSize + "';";
                strJS += "} else {";
                strJS += "    oChild.multiple = '';oChild.size ='1';";
                strJS += "} ";
                ddl04.Attributes.Add("onchange", strJS);
            }

            //判斷水平 or 垂直版面
            string strBreakStyle = (ucIsVerticalLayout) ? ucVerticalStyleHtmlTag : "";
            if (!string.IsNullOrEmpty(strBreakStyle) && !string.IsNullOrEmpty(ucCaption))
            {
                strBreakStyle += string.Format("<span style='display:inline-block;width:{0}px;' ></span>", ucCaptionWidth);
            }
            if (ucIsSearchEnabled)
            {
                LiteralSearch.Text = strBreakStyle;
            }
            if (ucDropDownListEnabled02)
            {
                Literal1.Text = strBreakStyle;
            }
            if (ucDropDownListEnabled03)
            {
                Literal2.Text = strBreakStyle;
            }
            if (ucDropDownListEnabled04)
            {
                Literal3.Text = strBreakStyle;
            }
            if (ucDropDownListEnabled05)
            {
                Literal4.Text = strBreakStyle;
            }

            //判斷搜尋框 2015.08.12
            txtSearch.Visible = false;
            if (ucIsSearchEnabled)
            {
                txtSearch.Visible = true;
                txtSearch.Width = ucSearchBoxWidth;
                txtSearch.Text = ucSearchBoxWaterMarkText;
                txtSearch.CssClass = "Util_WaterMarkedTextBox";
                txtSearch.Attributes.Add("OnFocus", string.Format("Util_WaterMark_Focus('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
                txtSearch.Attributes.Add("OnBlur", string.Format("Util_WaterMark_Blur('{0}', '{1}');", txtSearch.ClientID, ucSearchBoxWaterMarkText.Replace("'", "\'")));
                txtSearch.Attributes.Add("onkeydown", "return (event.keyCode!=13);");  //預防按了 Enter 送出 PostBack
                txtSearch.Attributes.Add("onkeyup", string.Format(this.ClientID + "_SearchDropDownListItem('{0}','{1}');", txtSearch.ClientID, ddl01.ClientID));

                //從Util.js複製過來，產生對應的專用JS，解決同一頁多個相同控制項問題 2016.03.29
                string strFilterJS = @"

                // === [{Ctrl}] 專用JS [Begin]===
                var {Ctrl}ddl_already_init = false;
                var {Ctrl}ddl_NameSearch_value = '';
                var {Ctrl}ddl_ct_name = [];
                var {Ctrl}ddl_ct_val = [];

                function {Ctrl}SearchDropDownListItem(SearchTextID, DropDownListID) {
                    var myvalue = document.getElementById(SearchTextID).value;
                    if ({Ctrl}ddl_NameSearch_value == myvalue)
                        return false;
                    else
                        {Ctrl}ddl_NameSearch_value = myvalue;

                    var RegExp_NameSearch = new RegExp(myvalue, 'i');
                    var Name_DropDownList = document.getElementById(DropDownListID);
                    if (Name_DropDownList == null) {
                        return false;
                    }

                    {Ctrl}Dropdown_arrayInit(Name_DropDownList); //初始化陣列...
                    {Ctrl}RemoveAllOptions(Name_DropDownList); //清除選單...

                    //因Server端會固定在[0]產生一個空項目，該項目需固定保留
                    {Ctrl}AddOption(Name_DropDownList, {Ctrl}ddl_ct_name[0], {Ctrl}ddl_ct_val[0]);
                    //其餘項目則需內容符合才能建成選單項目
                    for (i = 1; i < {Ctrl}ddl_ct_name.length; i++) {
                        if (RegExp_NameSearch.test({Ctrl}ddl_ct_name[i])) //只列出符合條件的...
                        {
                            {Ctrl}AddOption(Name_DropDownList, {Ctrl}ddl_ct_name[i], {Ctrl}ddl_ct_val[i]);
                        }
                    }

                    Name_DropDownList.options[0].selected = true;
                }


                function {Ctrl}Dropdown_arrayInit(DDL_obj) {
                    if (!{Ctrl}ddl_already_init) {
                        for (var i = 0; i < DDL_obj.options.length; i++) {
                            {Ctrl}ddl_ct_name[i] = DDL_obj.options[i].text;
                            {Ctrl}ddl_ct_val[i] = DDL_obj.options[i].value;
                        }

                        {Ctrl}ddl_already_init = true;
                    }
                }

                //清除所有清單選項
                function {Ctrl}RemoveAllOptions(objList) {
                    if (objList != null) {
                        objList.options.length = 0;
                    }
                }

                //增加清單選項
                function {Ctrl}AddOption(objList, text, value) {
                    if (objList != null) {
                        var optn = document.createElement('OPTION');
                        optn.text = text;
                        optn.value = value;
                        objList.options.add(optn);
                    }
                } 
                // === [{Ctrl}] 專用JS [End]===

                ";
                strFilterJS = strFilterJS.Replace("{Ctrl}", this.ClientID + "_");
                Util.setJSContent(strFilterJS, this.ClientID + "_FilterJS_Init");
            }
        }
        else
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "[ucCascadingDropDown.ascx] Init Fail !<br>You can use [SetDefault()] or Setting [ucServicePath][ucServiceMethod] to make it work.");
            divDataArea.Visible = false;
            labErrMsg.Visible = true;
        }
    }
}

