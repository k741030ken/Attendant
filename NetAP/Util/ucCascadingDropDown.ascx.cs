using System;
using System.Web;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [關聯式下拉選單]控制項
/// <para>使用 WCF 作為 Ajax 後端技術，請參考 WcfCascadingHelper 相關說明</para>
/// </summary>
public partial class Util_ucCascadingDropDown : BaseUserControl
{
    string _VerticalStyleHtmlTag = "<br />";

    #region 屬性定義
    /// <summary>
    /// 是否提供搜尋功能(預設 false)
    /// </summary>
    public bool ucIsSearchEnabled
    {
        //2015.08.12新增
        get
        {
            if (PageViewState["_IsSearchEnabled"] == null)
            {
                PageViewState["_IsSearchEnabled"] = false;
            }
            return (bool)(PageViewState["_IsSearchEnabled"]);
        }
        set
        {
            PageViewState["_IsSearchEnabled"] = value;
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
            if (PageViewState["_SearchBoxWidth"] == null)
            {
                PageViewState["_SearchBoxWidth"] = 100;
            }
            return (int)(PageViewState["_SearchBoxWidth"]);
        }
        set
        {
            PageViewState["_SearchBoxWidth"] = value;
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
            if (PageViewState["_SearchBoxWaterMarkText"] == null)
            {
                PageViewState["_SearchBoxWaterMarkText"] = RS.Resources.CommSingleSelect_WaterMarkText; //"請輸入搜尋文字"
            }
            return (string)(PageViewState["_SearchBoxWaterMarkText"]);
        }
        set
        {
            PageViewState["_SearchBoxWaterMarkText"] = value;
        }
    }

    /// <summary>
    /// 是否垂直排列(預設 false)
    /// </summary>
    public bool ucIsVerticalLayout
    {
        get
        {
            if (PageViewState["_IsVerticalLayout"] == null)
            {
                PageViewState["_IsVerticalLayout"] = false;
            }
            return (bool)(PageViewState["_IsVerticalLayout"]);
        }
        set
        {
            PageViewState["_IsVerticalLayout"] = value;
        }
    }

    /// <summary>
    /// 垂直排列時使用的Html標籤(預設 &lt;br&gt;)
    /// </summary>
    public string ucVerticalStyleHtmlTag
    {
        get
        {
            return _VerticalStyleHtmlTag;
        }
        set
        {
            _VerticalStyleHtmlTag = value;
        }
    }

    /// <summary>
    /// 是否[多選]的偵測字串(預設 [...])
    /// </summary>
    public string ucMultiSelectIndicator
    {
        get
        {
            if (PageViewState["_MultiSelectIndicator"] == null)
            {
                PageViewState["_MultiSelectIndicator"] = "…";  //偵測是否變成多選的辨識字串 2014.07.07
            }
            return (string)(PageViewState["_MultiSelectIndicator"]);
        }
        set
        {
            PageViewState["_MultiSelectIndicator"] = value;
        }
    }

    /// <summary>
    /// [多選]時的選單列數(預設 5)
    /// </summary>
    public string ucMultiSelectRowSize
    {
        get
        {
            if (PageViewState["_MultiSelectRowSize"] == null)
            {
                PageViewState["_MultiSelectRowSize"] = "5";
            }
            return (string)(PageViewState["_MultiSelectRowSize"]);
        }
        set
        {
            PageViewState["_MultiSelectRowSize"] = value;
        }
    }

    /// <summary>
    /// 控制項寬度(預設 -1，即自動計算)
    /// </summary>
    public int ucCascadingWidth
    {
        get
        {
            if (PageViewState["_CascadingWidth"] == null)
            {
                PageViewState["_CascadingWidth"] = -1;
            }
            return (int)(PageViewState["_CascadingWidth"]);
        }
        set
        {
            PageViewState["_CascadingWidth"] = value;
        }
    }

    /// <summary>
    /// 下拉選單寬度(預設 195)
    /// </summary>
    public int ucDropDownListWidth
    {
        get
        {
            if (PageViewState["_DropDownListWidth"] == null)
            {
                PageViewState["_DropDownListWidth"] = 195;
            }
            return (int)(PageViewState["_DropDownListWidth"]);
        }
        set
        {
            PageViewState["_DropDownListWidth"] = value;
        }
    }

    /// <summary>
    /// Service 服務路徑(預設 Util._CommCascadeServicePath)
    /// </summary>
    public string ucServicePath
    {
        get
        {
            if (PageViewState["_ServicePath"] == null)
            {
                PageViewState["_ServicePath"] = Util._CommCascadeServicePath;
            }
            return PageViewState["_ServicePath"].ToString();
        }
        set
        {
            PageViewState["_ServicePath"] = value;
        }
    }

    /// <summary>
    /// Service 服務方法(預設 Util._CommCascadeServiceMethod)
    /// </summary>
    public string ucServiceMethod
    {
        get
        {
            if (PageViewState["_ServiceMethod"] == null)
            {
                PageViewState["_ServiceMethod"] = Util._CommCascadeServiceMethod;
            }
            return PageViewState["_ServiceMethod"].ToString();
        }
        set
        {
            PageViewState["_ServiceMethod"] = value;
        }
    }

    /// <summary>
    /// 選單載入資料時的訊息
    /// </summary>
    public string ucLoadingText
    {
        get
        {
            if (PageViewState["_LoadingText"] == null)
            {
                PageViewState["_LoadingText"] = RS.Resources.Msg_Waiting;
            }
            return PageViewState["_LoadingText"].ToString();
        }
        set
        {
            PageViewState["_LoadingText"] = value;
        }
    }

    /// <summary>
    /// 控制項CSS
    /// </summary>
    public string ucCascadingCssClass
    {
        get
        {
            if (PageViewState["_CascadingCssClass"] == null)
            {
                PageViewState["_CascadingCssClass"] = "";
            }
            return PageViewState["_CascadingCssClass"].ToString();
        }
        set
        {
            PageViewState["_CascadingCssClass"] = value;
        }
    }

    /// <summary>
    /// 下拉選單CSS
    /// </summary>
    public string ucDropDownListCssClass
    {
        get
        {
            if (PageViewState["_DropDownListCssClass"] == null)
            {
                PageViewState["_DropDownListCssClass"] = "";
            }
            return PageViewState["_DropDownListCssClass"].ToString();
        }
        set
        {
            PageViewState["_DropDownListCssClass"] = value;
        }
    }

    /// <summary>
    /// 下拉選單唯讀時的CSS
    /// </summary>
    public string ucDropDownListReadOnlyCssClass
    {
        get
        {
            if (PageViewState["_DropDownListReadOnlyCssClass"] == null)
            {
                PageViewState["_DropDownListReadOnlyCssClass"] = "Util_clsDropDownListReadOnly";
            }
            return PageViewState["_DropDownListReadOnlyCssClass"].ToString();
        }
        set
        {
            PageViewState["_DropDownListReadOnlyCssClass"] = value;
        }
    }

    /// <summary>
    /// 是否使用第一層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled01
    {
        get
        {
            if (PageViewState["_IsDropDownListEnabled01"] == null)
            {
                PageViewState["_IsDropDownListEnabled01"] = true;
            }
            return (bool)(PageViewState["_IsDropDownListEnabled01"]);
        }
        set
        {
            PageViewState["_IsDropDownListEnabled01"] = value;
        }
    }

    /// <summary>
    /// 是否使用第二層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled02
    {
        get
        {
            if (PageViewState["_IsDropDownListEnabled02"] == null)
            {
                PageViewState["_IsDropDownListEnabled02"] = true;
            }
            return (bool)(PageViewState["_IsDropDownListEnabled02"]);
        }
        set
        {
            PageViewState["_IsDropDownListEnabled02"] = value;
        }
    }

    /// <summary>
    /// 是否使用第三層下拉選單(預設 true)
    /// </summary>
    public bool ucDropDownListEnabled03
    {
        get
        {
            if (PageViewState["_IsDropDownListEnabled03"] == null)
            {
                PageViewState["_IsDropDownListEnabled03"] = true;
            }
            return (bool)(PageViewState["_IsDropDownListEnabled03"]);
        }
        set
        {
            PageViewState["_IsDropDownListEnabled03"] = value;
        }
    }

    /// <summary>
    /// 是否使用第四層下拉選單(預設 false)
    /// </summary>
    public bool ucDropDownListEnabled04
    {
        get
        {
            if (PageViewState["_IsDropDownListEnabled04"] == null)
            {
                PageViewState["_IsDropDownListEnabled04"] = false;
            }
            return (bool)(PageViewState["_IsDropDownListEnabled04"]);
        }
        set
        {
            PageViewState["_IsDropDownListEnabled04"] = value;
        }
    }

    /// <summary>
    /// 是否使用第五層下拉選單(預設 false)
    /// </summary>
    public bool ucDropDownListEnabled05
    {
        get
        {
            if (PageViewState["_IsDropDownListEnabled05"] == null)
            {
                PageViewState["_IsDropDownListEnabled05"] = false;
            }
            return (bool)(PageViewState["_IsDropDownListEnabled05"]);
        }
        set
        {
            PageViewState["_IsDropDownListEnabled05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單提示訊息
    /// </summary>
    public string ucPromptText01
    {
        get
        {
            if (PageViewState["_PromptText01"] == null)
            {
                PageViewState["_PromptText01"] = RS.Resources.Msg_DDL_EmptyItem; 
            }
            return PageViewState["_PromptText01"].ToString();
        }
        set
        {
            PageViewState["_PromptText01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單提示訊息
    /// </summary>
    public string ucPromptText02
    {
        get
        {
            if (PageViewState["_PromptText02"] == null)
            {
                PageViewState["_PromptText02"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return PageViewState["_PromptText02"].ToString();
        }
        set
        {
            PageViewState["_PromptText02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單提示訊息
    /// </summary>
    public string ucPromptText03
    {
        get
        {
            if (PageViewState["_PromptText03"] == null)
            {
                PageViewState["_PromptText03"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return PageViewState["_PromptText03"].ToString();
        }
        set
        {
            PageViewState["_PromptText03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單提示訊息
    /// </summary>
    public string ucPromptText04
    {
        get
        {
            if (PageViewState["_PromptText04"] == null)
            {
                PageViewState["_PromptText04"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return PageViewState["_PromptText04"].ToString();
        }
        set
        {
            PageViewState["_PromptText04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單提示訊息
    /// </summary>
    public string ucPromptText05
    {
        get
        {
            if (PageViewState["_PromptText05"] == null)
            {
                PageViewState["_PromptText05"] = RS.Resources.Msg_DDL_EmptyItem;
            }
            return PageViewState["_PromptText05"].ToString();
        }
        set
        {
            PageViewState["_PromptText05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單類別
    /// </summary>
    public string ucCategory01
    {
        get
        {
            if (PageViewState["_Category01"] == null)
            {
                PageViewState["_Category01"] = "Category01";
            }
            return PageViewState["_Category01"].ToString();
        }
        set
        {
            PageViewState["_Category01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單類別
    /// </summary>
    public string ucCategory02
    {
        get
        {
            if (PageViewState["_Category02"] == null)
            {
                PageViewState["_Category02"] = "Category02";
            }
            return PageViewState["_Category02"].ToString();
        }
        set
        {
            PageViewState["_Category02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單類別
    /// </summary>
    public string ucCategory03
    {
        get
        {
            if (PageViewState["_Category03"] == null)
            {
                PageViewState["_Category03"] = "Category03";
            }
            return PageViewState["_Category03"].ToString();
        }
        set
        {
            PageViewState["_Category03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單類別
    /// </summary>
    public string ucCategory04
    {
        get
        {
            if (PageViewState["_Category04"] == null)
            {
                PageViewState["_Category04"] = "Category04";
            }
            return PageViewState["_Category04"].ToString();
        }
        set
        {
            PageViewState["_Category04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單類別
    /// </summary>
    public string ucCategory05
    {
        get
        {
            if (PageViewState["_Category05"] == null)
            {
                PageViewState["_Category05"] = "Category05";
            }
            return PageViewState["_Category05"].ToString();
        }
        set
        {
            PageViewState["_Category05"] = value;
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
            if (PageViewState["_DefaultSelectedValue01"] == null)
            {
                PageViewState["_DefaultSelectedValue01"] = "";
            }
            return (string)(PageViewState["_DefaultSelectedValue01"]);
        }
        set
        {
            PageViewState["_DefaultSelectedValue01"] = value;
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
            if (PageViewState["_DefaultSelectedValue02"] == null)
            {
                PageViewState["_DefaultSelectedValue02"] = "";
            }
            return (string)(PageViewState["_DefaultSelectedValue02"]);
        }
        set
        {
            PageViewState["_DefaultSelectedValue02"] = value;
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
            if (PageViewState["_DefaultSelectedValue03"] == null)
            {
                PageViewState["_DefaultSelectedValue03"] = "";
            }
            return (string)(PageViewState["_DefaultSelectedValue03"]);
        }
        set
        {
            PageViewState["_DefaultSelectedValue03"] = value;
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
            if (PageViewState["_DefaultSelectedValue04"] == null)
            {
                PageViewState["_DefaultSelectedValue04"] = "";
            }
            return (string)(PageViewState["_DefaultSelectedValue04"]);
        }
        set
        {
            PageViewState["_DefaultSelectedValue04"] = value;
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
            if (PageViewState["_DefaultSelectedValue05"] == null)
            {
                PageViewState["_DefaultSelectedValue05"] = "";
            }
            return (string)(PageViewState["_DefaultSelectedValue05"]);
        }
        set
        {
            PageViewState["_DefaultSelectedValue05"] = value;
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
            if (PageViewState["_IsRequire01"] == null)
            {
                PageViewState["_IsRequire01"] = false;
            }
            return (bool)PageViewState["_IsRequire01"];
        }
        set
        {
            PageViewState["_IsRequire01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire02
    {
        get
        {
            if (PageViewState["_IsRequire02"] == null)
            {
                PageViewState["_IsRequire02"] = false;
            }
            return (bool)PageViewState["_IsRequire02"];
        }
        set
        {
            PageViewState["_IsRequire02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire03
    {
        get
        {
            if (PageViewState["_IsRequire03"] == null)
            {
                PageViewState["_IsRequire03"] = false;
            }
            return (bool)PageViewState["_IsRequire03"];
        }
        set
        {
            PageViewState["_IsRequire03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire04
    {
        get
        {
            if (PageViewState["_IsRequire04"] == null)
            {
                PageViewState["_IsRequire04"] = false;
            }
            return (bool)PageViewState["_IsRequire04"];
        }
        set
        {
            PageViewState["_IsRequire04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否必需選擇
    /// </summary>
    public bool ucIsRequire05
    {
        get
        {
            if (PageViewState["_IsRequire05"] == null)
            {
                PageViewState["_IsRequire05"] = false;
            }
            return (bool)PageViewState["_IsRequire05"];
        }
        set
        {
            PageViewState["_IsRequire05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg01
    {
        get
        {
            if (PageViewState["_RequireMsg01"] == null)
            {
                PageViewState["_RequireMsg01"] = RS.Resources.Msg_RequireInput;
            }
            return (string)PageViewState["_RequireMsg01"];
        }
        set
        {
            PageViewState["_RequireMsg01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg02
    {
        get
        {
            if (PageViewState["_RequireMsg02"] == null)
            {
                PageViewState["_RequireMsg02"] = RS.Resources.Msg_RequireInput;
            }
            return (string)PageViewState["_RequireMsg02"];
        }
        set
        {
            PageViewState["_RequireMsg02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg03
    {
        get
        {
            if (PageViewState["_RequireMsg03"] == null)
            {
                PageViewState["_RequireMsg03"] = RS.Resources.Msg_RequireInput;
            }
            return (string)PageViewState["_RequireMsg03"];
        }
        set
        {
            PageViewState["_RequireMsg03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg04
    {
        get
        {
            if (PageViewState["_RequireMsg04"] == null)
            {
                PageViewState["_RequireMsg04"] = RS.Resources.Msg_RequireInput;
            }
            return (string)PageViewState["_RequireMsg04"];
        }
        set
        {
            PageViewState["_RequireMsg04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單必選時的錯誤訊息
    /// </summary>
    public string ucRequireMsg05
    {
        get
        {
            if (PageViewState["_RequireMsg05"] == null)
            {
                PageViewState["_RequireMsg05"] = RS.Resources.Msg_RequireInput;
            }
            return (string)PageViewState["_RequireMsg05"];
        }
        set
        {
            PageViewState["_RequireMsg05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly01
    {
        get
        {
            if (PageViewState["_IsReadOnly01"] == null)
            {
                PageViewState["_IsReadOnly01"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly01"]);
        }
        set
        {
            PageViewState["_IsReadOnly01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly02
    {
        get
        {
            if (PageViewState["_IsReadOnly02"] == null)
            {
                PageViewState["_IsReadOnly02"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly02"]);
        }
        set
        {
            PageViewState["_IsReadOnly02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly03
    {
        get
        {
            if (PageViewState["_IsReadOnly03"] == null)
            {
                PageViewState["_IsReadOnly03"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly03"]);
        }
        set
        {
            PageViewState["_IsReadOnly03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly04
    {
        get
        {
            if (PageViewState["_IsReadOnly04"] == null)
            {
                PageViewState["_IsReadOnly04"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly04"]);
        }
        set
        {
            PageViewState["_IsReadOnly04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否唯讀(預設 false)
    /// </summary>
    public bool ucIsReadOnly05
    {
        get
        {
            if (PageViewState["_IsReadOnly05"] == null)
            {
                PageViewState["_IsReadOnly05"] = false;
            }
            return (bool)(PageViewState["_IsReadOnly05"]);
        }
        set
        {
            PageViewState["_IsReadOnly05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid01
    {
        get
        {
            if (PageViewState["_IsChkValid01"] == null)
            {
                PageViewState["_IsChkValid01"] = true;
            }
            return (bool)PageViewState["_IsChkValid01"];
        }
        set
        {
            PageViewState["_IsChkValid01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid02
    {
        get
        {
            if (PageViewState["_IsChkValid02"] == null)
            {
                PageViewState["_IsChkValid02"] = true;
            }
            return (bool)PageViewState["_IsChkValid02"];
        }
        set
        {
            PageViewState["_IsChkValid02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid03
    {
        get
        {
            if (PageViewState["_IsChkValid03"] == null)
            {
                PageViewState["_IsChkValid03"] = true;
            }
            return (bool)PageViewState["_IsChkValid03"];
        }
        set
        {
            PageViewState["_IsChkValid03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid04
    {
        get
        {
            if (PageViewState["_IsChkValid04"] == null)
            {
                PageViewState["_IsChkValid04"] = true;
            }
            return (bool)PageViewState["_IsChkValid04"];
        }
        set
        {
            PageViewState["_IsChkValid04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單是否需檢查可用狀態(預設 true)
    /// </summary>
    public bool ucIsChkValid05
    {
        get
        {
            if (PageViewState["_IsChkValid05"] == null)
            {
                PageViewState["_IsChkValid05"] = true;
            }
            return (bool)PageViewState["_IsChkValid05"];
        }
        set
        {
            PageViewState["_IsChkValid05"] = value;
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
            if (PageViewState["_ValidKeyList01"] == null)
            {
                PageViewState["_ValidKeyList01"] = "";
            }
            return (string)PageViewState["_ValidKeyList01"];
        }
        set
        {
            PageViewState["_ValidKeyList01"] = value;
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
            if (PageViewState["_ValidKeyList02"] == null)
            {
                PageViewState["_ValidKeyList02"] = "";
            }
            return (string)PageViewState["_ValidKeyList02"];
        }
        set
        {
            PageViewState["_ValidKeyList02"] = value;
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
            if (PageViewState["_ValidKeyList03"] == null)
            {
                PageViewState["_ValidKeyList03"] = "";
            }
            return (string)PageViewState["_ValidKeyList03"];
        }
        set
        {
            PageViewState["_ValidKeyList03"] = value;
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
            if (PageViewState["_ValidKeyList04"] == null)
            {
                PageViewState["_ValidKeyList04"] = "";
            }
            return (string)PageViewState["_ValidKeyList04"];
        }
        set
        {
            PageViewState["_ValidKeyList04"] = value;
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
            if (PageViewState["_ValidKeyList05"] == null)
            {
                PageViewState["_ValidKeyList05"] = "";
            }
            return (string)PageViewState["_ValidKeyList05"];
        }
        set
        {
            PageViewState["_ValidKeyList05"] = value;
        }
    }

    /// <summary>
    /// 第一層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData01
    {
        get
        {
            if (PageViewState["_ContextData01"] == null)
            {
                PageViewState["_ContextData01"] = "";
            }
            return (string)(PageViewState["_ContextData01"]);
        }
        set
        {
            PageViewState["_ContextData01"] = value;
        }
    }

    /// <summary>
    /// 第二層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData02
    {
        get
        {
            if (PageViewState["_ContextData02"] == null)
            {
                PageViewState["_ContextData02"] = "";
            }
            return (string)(PageViewState["_ContextData02"]);
        }
        set
        {
            PageViewState["_ContextData02"] = value;
        }
    }

    /// <summary>
    /// 第三層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData03
    {
        get
        {
            if (PageViewState["_ContextData03"] == null)
            {
                PageViewState["_ContextData03"] = "";
            }
            return (string)(PageViewState["_ContextData03"]);
        }
        set
        {
            PageViewState["_ContextData03"] = value;
        }
    }

    /// <summary>
    /// 第四層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData04
    {
        get
        {
            if (PageViewState["_ContextData04"] == null)
            {
                PageViewState["_ContextData04"] = "";
            }
            return (string)(PageViewState["_ContextData04"]);
        }
        set
        {
            PageViewState["_ContextData04"] = value;
        }
    }

    /// <summary>
    /// 第五層下拉選單自訂ContextData (ex: [key1:value1;key2:value2;])
    /// </summary>
    public string ucContextData05
    {
        get
        {
            if (PageViewState["_ContextData05"] == null)
            {
                PageViewState["_ContextData05"] = "";
            }
            return (string)(PageViewState["_ContextData05"]);
        }
        set
        {
            PageViewState["_ContextData05"] = value;
        }
    }


    /// <summary>
    /// 是否使用切換可見功能(預設 false)
    /// </summary>
    public bool ucIsToggleVisibility
    {
        get
        {
            if (PageViewState["_IsToggleVisibility"] == null)
            {
                PageViewState["_IsToggleVisibility"] = false;
            }
            return (bool)(PageViewState["_IsToggleVisibility"]);
        }
        set
        {
            PageViewState["_IsToggleVisibility"] = value;
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


    /// <summary>
    /// 控制項顯示抬頭水平對齊方式(預設 Right)
    /// </summary>
    public HorizontalAlign ucCaptionHorizontalAlign
    {
        //2017.06.03 新增
        get
        {
            if (PageViewState["_CaptionHorizontalAlign"] == null)
            {
                PageViewState["_CaptionHorizontalAlign"] = HorizontalAlign.Right;
            }
            return (HorizontalAlign)(PageViewState["_CaptionHorizontalAlign"]);
        }
        set
        {
            PageViewState["_CaptionHorizontalAlign"] = value;
            if (value == HorizontalAlign.NotSet)
            {
                PageViewState["_CaptionHorizontalAlign"] = HorizontalAlign.Right;
            }
            labCaption.Style["text-align"] = ((HorizontalAlign)PageViewState["_CaptionHorizontalAlign"]).ToString().ToLower();
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        bool IsInit = (PageViewState["_IsInit"] != null) ? (bool)PageViewState["_IsInit"] : false;
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
            PageViewState["_IsInit"] = true;
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

