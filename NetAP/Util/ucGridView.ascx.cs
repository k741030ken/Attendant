using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

/// <summary>
/// 萬用[資料清單]控制項
/// <para></para>
/// <para>不必寫Code，只需定義即可作到：</para>
/// <para>    -訂閱相關事件(單筆：RowCommand / 批次：GridViewCommand)</para>
/// <para>    -自訂要使用的功能按鈕/圖示/提示訊息</para>
/// <para>        目前功能按鈕有：新增、匯出、列印、勾選、多語系、資訊、選擇、下載、編輯、刪除、複製</para>
/// <para>    -自訂是否顯示資料列的流水號</para>
/// <para>    -自訂資料來源種類(SQL / DataTable)</para>
/// <para>    -自訂顯示欄位(順序/顯示樣式/寬度/提示訊息)</para>
/// <para>        目前可支援顯示型態：文字、數字、日期、圖片、超連結</para>
/// <para>    -自訂線上編輯欄位(可使用 TextBox/Calendar/DropDownList/DropDownFirst/CheckBoxList/RadioList 物件並設定專用屬性；若不設定，控制項會自動判斷並處理必要的檢核)</para>
/// <para>    -自訂加總欄位，則自動於表尾顯示加總值(模組會自動調整格式)</para>
/// <para>    -自訂每頁顯示筆數清單(頁面切換及導引工具列會自動顯示/隱藏)</para>
/// <para>    -自訂群組欄位，以分群方式顯示資料(自動產生群組抬頭列，並支援展開/收合/加總)</para>
/// <para>    -自訂排序欄位(自動計算當搭配不同的分頁筆數/頁數/群組時，資料呈現的合理性)</para>
/// <para>    -自訂顯示訊息(模組內建訊息已支援多語系)</para>
/// <para>    -自訂顯示介面的CssClass，預設使用Util.css內建的第一組設定(有四組可選用)</para>
/// <para>    -匯出Excel(2003/2007)時，可限定輸出筆數/僅輸出顯示欄位或所有欄位</para>
/// <para>    -匯出PDF時，可限定輸出筆數/僅輸出顯示欄位或所有欄位/自訂浮水印文字內容、字體、大小及旋轉角度</para>
/// <remarks>
/// <para>** 控制項資料讀取的來源資料庫內需有[sp_GetPageData][sp_GetPageDataCount]等預存程序，可從[NetSample]資料庫中取得 **</para>
/// </remarks>
/// </summary>
public partial class Util_ucGridView : BaseUserControl
{

    //初始變數
    private decimal[] _SubtotalList;                //加總值暫存陣列
    private int _GroupOffset = 0;                   //群組列偏移植，每產生一個群組列就 + 1
    private int _DataEditDefinitionMaxQty = 30;  //最大可編輯欄位數量，擴充為[30]個，並在輸出網頁時，根據實際需要數量輸出 2016.03.11
    private string[] _RowQtyChgCmdNameList = "cmdAdd,cmdCopy,cmdDelete,cmdDeleteAll".Split(',');  //會改變資料筆數的命令清單 2016.11.14 加入 [cmdDeleteAll]

    //2016.11.24
    protected string _defPopDisplayClientJS
    {
        get
        {
            string strJS = "Util_IsChkDirty = false;";
            strJS += ucLightBox.ucShowClientJS;
            return strJS;
        }
    }

    //2016.11.24
    protected string _defPopCloseClientJS
    {
        get
        {
            string strJS = "dom.Ready(function(){ ";
            strJS += ucLightBox.ucHideClientJS;
            strJS += "});";
            return strJS;
        }
    }


    /// <summary>
    /// 是否啟用ACL相關控制(預設 false)
    /// <remarks>**當頁面繼承自 [AclPage] 才會生效**</remarks>
    /// </summary>
    public bool ucAclEnabled
    {
        get
        {
            if (PageViewState["_AclEnabled"] == null)
            {
                PageViewState["_AclEnabled"] = false;
            }
            return (bool)(PageViewState["_AclEnabled"]);
        }
        set
        {
            PageViewState["_AclEnabled"] = value;
            if ((bool)value == true)
            {
                PageViewState["_AclEnabled"] = AclExpress.IsAclPage();
            }
        }
    }

    /// <summary>
    /// 命令按鈕水平對齊方式(預設 Left)
    /// </summary>
    public HorizontalAlign ucCmdBtnHorizontalAlign
    {
        //2016.07.28 新增
        get
        {
            if (PageViewState["_CmdBtnHorizontalAlign"] == null)
            {
                PageViewState["_CmdBtnHorizontalAlign"] = HorizontalAlign.Left;
            }
            return (HorizontalAlign)(PageViewState["_CmdBtnHorizontalAlign"]);
        }
        set
        {
            PageViewState["_CmdBtnHorizontalAlign"] = value;
        }

    }

    /// <summary>
    /// 當PostBack後，控制項是否自動判斷並發動Refresh(預設 true)
    /// <para>**此功能會自動處理頁面上所有 ucGridView 實體物件的自動Refresh，故若非進階使用者，強烈建議不要關閉此功能**</para>
    /// </summary>
    public bool ucIsAutoRefresh // 2014.09.19
    {
        get
        {
            if (PageViewState["_IsAutoRefresh"] == null)
            {
                PageViewState["_IsAutoRefresh"] = true;
            }
            return (bool)(PageViewState["_IsAutoRefresh"]);
        }
        set
        {
            PageViewState["_IsAutoRefresh"] = value;
        }
    }

    /// <summary>
    /// 處理呼叫 _doPostBack 時適用的 ClientID
    /// </summary>
    protected string _doPostBackClientID
    {
        get
        {
            if (PageViewState["_doPostBackClientID"] == null)
            {
                PageViewState["_doPostBackClientID"] = this.UniqueID; // 2016.06.29 調整作法
            }
            return (string)(PageViewState["_doPostBackClientID"]);
        }
    }

    #region [ToolTip] 相關定義
    /// <summary>
    /// ToolTip預設抬頭
    /// </summary>
    public string ucDefaultTooltipTitle
    {
        get
        {
            if (PageViewState["_DefaultTooltipTitle"] == null)
            {
                PageViewState["_DefaultTooltipTitle"] = RS.Resources.Msg_DefaultTooltipTitle; //2016.07.29 調整
            }
            return PageViewState["_DefaultTooltipTitle"].ToString();
        }
        set
        {
            PageViewState["_DefaultTooltipTitle"] = value;
        }
    }

    /// <summary>
    /// ToolTip樣式範本(預設 Simple)
    /// </summary>
    public HoverTooltipTemplete ucHoverTooltipTemplete
    {
        get
        {
            if (PageViewState["_HoverTooltipTemplete"] == null)
            {
                PageViewState["_HoverTooltipTemplete"] = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Simple);
            }
            return (HoverTooltipTemplete)(PageViewState["_HoverTooltipTemplete"]);
        }
        set
        {
            PageViewState["_HoverTooltipTemplete"] = value;
        }
    }


    /// <summary>
    /// 欲顯示 ToolTip 的欄位及其格式
    /// <para></para>
    /// <para>依據選定的 ToolTip 範本，可能需 ToolTip / TooltipTitle + Tooltip 來當作提示訊息來源</para>
    /// <para>例: 'POID' , 'ToolTip'          (只需ToolTip)</para>
    /// <para>　　'POID' , 'TipTitle,ToolTip' (需要TooltipTitle 及 Tooltip)</para>
    /// </summary>
    public Dictionary<string, string> ucDataDisplayToolTipDefinition
    {
        get
        {
            if (PageViewState["_DataDisplayToolTipDefinition"] == null)
            {
                PageViewState["_DataDisplayToolTipDefinition"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)(PageViewState["_DataDisplayToolTipDefinition"]);
        }
        set
        {
            PageViewState["_DataDisplayToolTipDefinition"] = value;
        }
    }
    #endregion

    #region [Add] 相關定義
    /// <summary>
    /// 使用[Add]功能(預設 false)
    /// </summary>
    public bool ucAddEnabled
    {
        get
        {
            if (PageViewState["_IsEnableAdd"] == null)
            {
                PageViewState["_IsEnableAdd"] = false;
            }
            return (bool)(PageViewState["_IsEnableAdd"]);
        }
        set
        {
            PageViewState["_IsEnableAdd"] = value;
        }
    }

    /// <summary>
    /// [Add]提示訊息
    /// </summary>
    public string ucAddToolTip
    {
        get
        {
            if (PageViewState["_Msg_Add"] == null)
            {
                PageViewState["_Msg_Add"] = RS.Resources.GridView_Add;
            }
            return PageViewState["_Msg_Add"].ToString();
        }
        set
        {
            PageViewState["_Msg_Add"] = value;
        }
    }

    /// <summary>
    /// [Add]圖示
    /// </summary>
    public string ucAddIcon
    {
        get
        {
            if (PageViewState["_Icon_Add"] == null)
            {
                PageViewState["_Icon_Add"] = Util.Icon_Add;
            }
            return PageViewState["_Icon_Add"].ToString();
        }
        set
        {
            PageViewState["_Icon_Add"] = value;
        }
    }
    #endregion

    #region [DataDump] 相關定義 2016.11.08
    /// <summary>
    /// 使用[DataDump]功能(預設 false)
    /// </summary>
    public bool ucDataDumpEnabled
    {
        get
        {
            if (PageViewState["_IsEnableDataDump"] == null)
            {
                PageViewState["_IsEnableDataDump"] = false;
            }
            return (bool)(PageViewState["_IsEnableDataDump"]);
        }
        set
        {
            PageViewState["_IsEnableDataDump"] = value;
        }
    }

    /// <summary>
    /// [DataDump]提示訊息
    /// </summary>
    public string ucDataDumpToolTip
    {
        get
        {
            if (PageViewState["_Msg_DataDump"] == null)
            {
                PageViewState["_Msg_DataDump"] = RS.Resources.GridView_DataDump;
            }
            return PageViewState["_Msg_DataDump"].ToString();
        }
        set
        {
            PageViewState["_Msg_DataDump"] = value;
        }
    }

    /// <summary>
    /// [DataDump]圖示
    /// </summary>
    public string ucDataDumpIcon
    {
        get
        {
            if (PageViewState["_Icon_DataDump"] == null)
            {
                PageViewState["_Icon_DataDump"] = Util.Icon_DataDump;
            }
            return PageViewState["_Icon_DataDump"].ToString();
        }
        set
        {
            PageViewState["_Icon_DataDump"] = value;
        }
    }
    #endregion

    #region [Check] 相關定義
    /// <summary>
    /// [Check]提示訊息
    /// </summary>
    public string ucCheckToolTip
    {
        get
        {
            if (PageViewState["_Msg_Check"] == null)
            {
                PageViewState["_Msg_Check"] = RS.Resources.GridView_Check;
            }
            return PageViewState["_Msg_Check"].ToString();
        }
        set
        {
            PageViewState["_Msg_Check"] = value;
        }
    }

    /// <summary>
    /// 使用[Check]功能(預設 false)
    /// </summary>
    public bool ucCheckEnabled
    {
        get
        {
            if (PageViewState["_IsEnableCheck"] == null)
            {
                PageViewState["_IsEnableCheck"] = false;
            }
            return (bool)(PageViewState["_IsEnableCheck"]);
        }
        set
        {
            PageViewState["_IsEnableCheck"] = value;
        }
    }

    /// <summary>
    /// 當 ucCheckEnabled=true ，若想控制各資料列是否能被勾選
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，才出現 CheckBox </para>
    /// </summary>
    public string ucCheckEnabledDataColName
    {
        get
        {
            if (PageViewState["_CheckEnabledDataColName"] == null)
            {
                PageViewState["_CheckEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_CheckEnabledDataColName"]);
        }
        set
        {
            PageViewState["_CheckEnabledDataColName"] = value;
        }
    }
    #endregion

    #region [CheckAll] 相關定義

    /// <summary>
    /// [CheckAll]提示訊息
    /// </summary>
    public string ucCheckAllToolTip
    {
        get
        {
            if (PageViewState["_Msg_CheckAll"] == null)
            {
                PageViewState["_Msg_CheckAll"] = RS.Resources.GridView_CheckAll;
            }
            return PageViewState["_Msg_CheckAll"].ToString();
        }
        set
        {
            PageViewState["_Msg_CheckAll"] = value;
        }
    }
    #endregion

    #region [Copy] 相關定義

    /// <summary>
    /// 使用[Copy]功能(預設 false)
    /// </summary>
    public bool ucCopyEnabled
    {
        get
        {
            if (PageViewState["_IsEnableCopy"] == null)
            {
                PageViewState["_IsEnableCopy"] = false;
            }
            return (bool)(PageViewState["_IsEnableCopy"]);
        }
        set
        {
            PageViewState["_IsEnableCopy"] = value;
        }
    }

    /// <summary>
    /// 當 ucCopyEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucCopyEnabledDataColName
    {
        get
        {
            if (PageViewState["_CopyEnabledDataColName"] == null)
            {
                PageViewState["_CopyEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_CopyEnabledDataColName"]);
        }
        set
        {
            PageViewState["_CopyEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Copy]提示訊息
    /// </summary>
    public string ucCopyToolTip
    {
        get
        {
            if (PageViewState["_Msg_Copy"] == null)
            {
                PageViewState["_Msg_Copy"] = RS.Resources.GridView_Copy;
            }
            return PageViewState["_Msg_Copy"].ToString();
        }
        set
        {
            PageViewState["_Msg_Copy"] = value;
        }
    }

    /// <summary>
    /// [Copy]圖示
    /// </summary>
    public string ucCopyIcon
    {
        get
        {
            if (PageViewState["_Icon_Copy"] == null)
            {
                PageViewState["_Icon_Copy"] = Util.Icon_Copy;
            }
            return PageViewState["_Icon_Copy"].ToString();
        }
        set
        {
            PageViewState["_Icon_Copy"] = value;
        }
    }
    #endregion

    #region [Delete] 相關定義
    /// <summary>
    /// 使用[Delete]功能(預設 false)
    /// </summary>
    public bool ucDeleteEnabled
    {
        get
        {
            if (PageViewState["_IsEnableDelete"] == null)
            {
                PageViewState["_IsEnableDelete"] = false;
            }
            return (bool)(PageViewState["_IsEnableDelete"]);
        }
        set
        {
            PageViewState["_IsEnableDelete"] = value;
        }
    }

    /// <summary>
    /// 當 ucDeleteEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucDeleteEnabledDataColName
    {
        get
        {
            if (PageViewState["_DeleteEnabledDataColName"] == null)
            {
                PageViewState["_DeleteEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_DeleteEnabledDataColName"]);
        }
        set
        {
            PageViewState["_DeleteEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Delete]提示訊息
    /// </summary>
    public string ucDeleteToolTip
    {
        get
        {
            if (PageViewState["_Msg_Delete"] == null)
            {
                PageViewState["_Msg_Delete"] = RS.Resources.GridView_Delete;
            }
            return PageViewState["_Msg_Delete"].ToString();
        }
        set
        {
            PageViewState["_Msg_Delete"] = value;
        }
    }

    /// <summary>
    /// [DeleteConfirm]提示訊息
    /// </summary>
    public string ucDeleteConfirm
    {
        get
        {
            if (PageViewState["_Msg_DeleteConfirm"] == null)
            {
                PageViewState["_Msg_DeleteConfirm"] = RS.Resources.GridView_DeleteConfirm;
            }
            return PageViewState["_Msg_DeleteConfirm"].ToString();
        }
        set
        {
            PageViewState["_Msg_DeleteConfirm"] = value;
        }
    }

    /// <summary>
    /// [Delete]圖示
    /// </summary>
    public string ucDeleteIcon
    {
        get
        {
            if (PageViewState["_Icon_Delete"] == null)
            {
                PageViewState["_Icon_Delete"] = Util.Icon_Delete;
            }
            return PageViewState["_Icon_Delete"].ToString();
        }
        set
        {
            PageViewState["_Icon_Delete"] = value;
        }
    }
    #endregion

    #region [Download] 相關定義
    /// <summary>
    /// 使用[Download]功能(預設 false)
    /// </summary>
    public bool ucDownloadEnabled
    {
        get
        {
            if (PageViewState["_IsEnableDownload"] == null)
            {
                PageViewState["_IsEnableDownload"] = false;
            }
            return (bool)(PageViewState["_IsEnableDownload"]);
        }
        set
        {
            PageViewState["_IsEnableDownload"] = value;
        }
    }

    /// <summary>
    /// 當 ucDownloadEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucDownloadEnabledDataColName
    {
        get
        {
            if (PageViewState["_DownloadEnabledDataColName"] == null)
            {
                PageViewState["_DownloadEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_DownloadEnabledDataColName"]);
        }
        set
        {
            PageViewState["_DownloadEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Download]提示訊息
    /// </summary>
    public string ucDownloadToolTip
    {
        get
        {
            if (PageViewState["_Msg_Download"] == null)
            {
                PageViewState["_Msg_Download"] = RS.Resources.GridView_Download;
            }
            return PageViewState["_Msg_Download"].ToString();
        }
        set
        {
            PageViewState["_Msg_Download"] = value;
        }
    }

    /// <summary>
    /// [Download]圖示
    /// </summary>
    public string ucDownloadIcon
    {
        get
        {
            if (PageViewState["_Icon_Download"] == null)
            {
                PageViewState["_Icon_Download"] = Util.Icon_Download;
            }
            return PageViewState["_Icon_Download"].ToString();
        }
        set
        {
            PageViewState["_Icon_Download"] = value;
        }
    }
    #endregion

    #region [Edit] 相關定義
    /// <summary>
    /// 使用[Edit]功能(預設 false)
    /// </summary>
    public bool ucEditEnabled
    {
        get
        {
            if (PageViewState["_IsEnableEdit"] == null)
            {
                PageViewState["_IsEnableEdit"] = false;
            }
            return (bool)(PageViewState["_IsEnableEdit"]);
        }
        set
        {
            PageViewState["_IsEnableEdit"] = value;
        }
    }

    /// <summary>
    /// 當 ucEditEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucEditEnabledDataColName
    {
        get
        {
            if (PageViewState["_EditEnabledDataColName"] == null)
            {
                PageViewState["_EditEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_EditEnabledDataColName"]);
        }
        set
        {
            PageViewState["_EditEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Edit]提示訊息
    /// </summary>
    public string ucEditToolTip
    {
        get
        {
            if (PageViewState["_Msg_Edit"] == null)
            {
                PageViewState["_Msg_Edit"] = RS.Resources.GridView_Edit;
            }
            return PageViewState["_Msg_Edit"].ToString();
        }
        set
        {
            PageViewState["_Msg_Edit"] = value;
        }
    }

    /// <summary>
    /// [Edit]圖示
    /// </summary>
    public string ucEditIcon
    {
        get
        {
            if (PageViewState["_Icon_Edit"] == null)
            {
                PageViewState["_Icon_Edit"] = Util.Icon_Edit;
            }
            return PageViewState["_Icon_Edit"].ToString();
        }
        set
        {
            PageViewState["_Icon_Edit"] = value;
        }
    }
    #endregion

    #region [Export][ExportOpenXml][ExportPdf] 相關定義

    /// <summary>
    /// 使用[Export]功能(預設 false)
    /// </summary>
    public bool ucExportEnabled
    {
        get
        {
            if (PageViewState["_IsEnableExport"] == null)
            {
                PageViewState["_IsEnableExport"] = false;
            }
            return (bool)(PageViewState["_IsEnableExport"]);
        }
        set
        {
            PageViewState["_IsEnableExport"] = value;
        }
    }

    /// <summary>
    /// 使用[ExportOpenXml]功能(預設 false)
    /// </summary>
    public bool ucExportOpenXmlEnabled
    {
        get
        {
            if (PageViewState["_IsEnabledExportOpenXml"] == null)
            {
                PageViewState["_IsEnabledExportOpenXml"] = false;
            }
            return (bool)(PageViewState["_IsEnabledExportOpenXml"]);
        }
        set
        {
            PageViewState["_IsEnabledExportOpenXml"] = value;
        }
    }

    /// <summary>
    /// 使用[ExportOpenXml]功能時，欄位資料是否依據 ucDisplayDefinition 進行格式化(預設 true)
    /// </summary>
    public bool ucExportOpenXmlFormatByDataDisplayDefinition
    {
        get
        {
            if (PageViewState["_ExportOpenXmlFormatByDataDisplayDefinition"] == null)
            {
                PageViewState["_ExportOpenXmlFormatByDataDisplayDefinition"] = true;
            }
            return (bool)(PageViewState["_ExportOpenXmlFormatByDataDisplayDefinition"]);
        }
        set
        {
            PageViewState["_ExportOpenXmlFormatByDataDisplayDefinition"] = value;
        }
    }


    /// <summary>
    /// 使用[ExportWord]功能(預設 false)
    /// </summary>
    public bool ucExportWordEnabled
    {
        // 2016.07.29 新增
        get
        {
            if (PageViewState["_ExportWordEnabled"] == null)
            {
                PageViewState["_ExportWordEnabled"] = false;
            }
            return (bool)(PageViewState["_ExportWordEnabled"]);
        }
        set
        {
            PageViewState["_ExportWordEnabled"] = value;
        }
    }

    /// <summary>
    /// 使用[ExportPdf]功能(預設 false)
    /// </summary>
    public bool ucExportPdfEnabled
    {
        get
        {
            if (PageViewState["_IsEnabledExportPdf"] == null)
            {
                PageViewState["_IsEnabledExportPdf"] = false;
            }
            return (bool)(PageViewState["_IsEnabledExportPdf"]);
        }
        set
        {
            PageViewState["_IsEnabledExportPdf"] = value;
        }
    }

    /// <summary>
    /// 是否匯出所有欄位(預設 false)
    /// </summary>
    public bool ucExportAllField
    {
        get
        {
            if (PageViewState["_ExportAllField"] == null)
            {
                PageViewState["_ExportAllField"] = false;
            }
            return (bool)(PageViewState["_ExportAllField"]);
        }
        set
        {
            PageViewState["_ExportAllField"] = value;
        }
    }

    /// <summary>
    /// [Export]提示訊息
    /// </summary>
    public string ucExportToolTip
    {
        get
        {
            if (PageViewState["_Msg_Export"] == null)
            {
                PageViewState["_Msg_Export"] = RS.Resources.GridView_Export;
            }
            return PageViewState["_Msg_Export"].ToString();
        }
        set
        {
            PageViewState["_Msg_Export"] = value;
        }
    }

    /// <summary>
    /// [ExportOpenXml]提示訊息
    /// </summary>
    public string ucExportOpenXmlToolTip
    {
        get
        {
            if (PageViewState["_Msg_ExportOpenXml"] == null)
            {
                PageViewState["_Msg_ExportOpenXml"] = RS.Resources.GridView_Export;
            }
            return PageViewState["_Msg_ExportOpenXml"].ToString();
        }
        set
        {
            PageViewState["_Msg_ExportOpenXml"] = value;
        }
    }

    /// <summary>
    /// [ExportWord]提示訊息
    /// </summary>
    public string ucExportWordToolTip
    {
        get
        {
            if (PageViewState["_Msg_ExportWord"] == null)
            {
                PageViewState["_Msg_ExportWord"] = RS.Resources.GridView_Export;
            }
            return PageViewState["_Msg_ExportWord"].ToString();
        }
        set
        {
            PageViewState["_Msg_ExportWord"] = value;
        }
    }

    /// <summary>
    /// [ExportPDF]提示訊息
    /// </summary>
    public string ucExportPdfToolTip
    {
        get
        {
            if (PageViewState["_Msg_ExportPDF"] == null)
            {
                PageViewState["_Msg_ExportPDF"] = RS.Resources.GridView_Export;
            }
            return PageViewState["_Msg_ExportPDF"].ToString();
        }
        set
        {
            PageViewState["_Msg_ExportPDF"] = value;
        }
    }

    /// <summary>
    /// [Export]圖示
    /// </summary>
    public string ucExportIcon
    {
        get
        {
            if (PageViewState["_Icon_Export"] == null)
            {
                PageViewState["_Icon_Export"] = Util.Icon_Excel;
            }
            return PageViewState["_Icon_Export"].ToString();
        }
        set
        {
            PageViewState["_Icon_Export"] = value;
        }
    }

    /// <summary>
    /// [ExportOpenXml]圖示
    /// </summary>
    public string ucExportOpenXmlIcon
    {
        get
        {
            if (PageViewState["_Icon_ExportOpenXml"] == null)
            {
                PageViewState["_Icon_ExportOpenXml"] = Util.Icon_ExcelOpenXml;
            }
            return PageViewState["_Icon_ExportOpenXml"].ToString();
        }
        set
        {
            PageViewState["_Icon_ExportOpenXml"] = value;
        }
    }

    /// <summary>
    /// [ExportWord]圖示
    /// </summary>
    public string ucExportWordIcon
    {
        get
        {
            if (PageViewState["_Icon_ExportWord"] == null)
            {
                PageViewState["_Icon_ExportWord"] = Util.Icon_Word;
            }
            return PageViewState["_Icon_ExportWord"].ToString();
        }
        set
        {
            PageViewState["_Icon_ExportWord"] = value;
        }
    }

    /// <summary>
    /// [ExportPDF]圖示
    /// </summary>
    public string ucExportPdfIcon
    {
        get
        {
            if (PageViewState["_Icon_ExportPDF"] == null)
            {
                PageViewState["_Icon_ExportPDF"] = Util.Icon_PDF;
            }
            return PageViewState["_Icon_ExportPDF"].ToString();
        }
        set
        {
            PageViewState["_Icon_ExportPDF"] = value;
        }
    }


    /// <summary>
    /// 匯出提示訊息
    /// </summary>
    public string ucExportConfirm
    {   //2016.11.23
        get
        {
            if (PageViewState["_ExportConfirm"] == null)
            {
                PageViewState["_ExportConfirm"] = "";
            }
            return PageViewState["_ExportConfirm"].ToString();
        }
        set
        {
            PageViewState["_ExportConfirm"] = value;
        }
    }

    /// <summary>
    /// 匯出OpenXml提示訊息
    /// </summary>
    public string ucExportOpenXmlConfirm
    {   //2016.11.23
        get
        {
            if (PageViewState["_ExportOpenXmlConfirm"] == null)
            {
                PageViewState["_ExportOpenXmlConfirm"] = "";
            }
            return PageViewState["_ExportOpenXmlConfirm"].ToString();
        }
        set
        {
            PageViewState["_ExportOpenXmlConfirm"] = value;
        }
    }

    /// <summary>
    /// 匯出Word提示訊息
    /// </summary>
    public string ucExportWordConfirm
    {   //2016.11.23
        get
        {
            if (PageViewState["_ExportWordConfirm"] == null)
            {
                PageViewState["_ExportWordConfirm"] = "";
            }
            return PageViewState["_ExportWordConfirm"].ToString();
        }
        set
        {
            PageViewState["_ExportWordConfirm"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf提示訊息
    /// </summary>
    public string ucExportPdfConfirm
    {   //2016.11.23
        get
        {
            if (PageViewState["_ExportPdfConfirm"] == null)
            {
                PageViewState["_ExportPdfConfirm"] = "";
            }
            return PageViewState["_ExportPdfConfirm"].ToString();
        }
        set
        {
            PageViewState["_ExportPdfConfirm"] = value;
        }
    }

    /// <summary>
    /// 匯出最大筆數(預設 10,000 筆)
    /// </summary>
    public int ucExportMaxQty
    {
        get
        {
            if (PageViewState["_ExportMaxQty"] == null)
            {
                PageViewState["_ExportMaxQty"] = 10000;
            }
            return (int)PageViewState["_ExportMaxQty"];
        }
        set
        {
            PageViewState["_ExportMaxQty"] = value;
        }
    }

    /// <summary>
    /// 匯出OpenXml最大筆數(預設 50000)
    /// </summary>
    public int ucExportOpenXmlMaxQty
    {
        get
        {
            if (PageViewState["_ExportOpenXmlMaxQty"] == null)
            {
                PageViewState["_ExportOpenXmlMaxQty"] = 50000;
            }
            return (int)PageViewState["_ExportOpenXmlMaxQty"];
        }
        set
        {
            PageViewState["_ExportOpenXmlMaxQty"] = value;
        }
    }

    /// <summary>
    /// 匯出Word最大筆數(預設 10000)
    /// </summary>
    public int ucExportWordMaxQty
    {
        get
        {
            if (PageViewState["_ExportWordMaxQty"] == null)
            {
                PageViewState["_ExportWordMaxQty"] = 10000;
            }
            return (int)PageViewState["_ExportWordMaxQty"];
        }
        set
        {
            PageViewState["_ExportWordMaxQty"] = value;
        }
    }


    /// <summary>
    /// 匯出PDF最大筆數(預設 1000)
    /// </summary>
    public int ucExportPdfMaxQty
    {
        get
        {
            if (PageViewState["_ExportPdfMaxQty"] == null)
            {
                PageViewState["_ExportPdfMaxQty"] = 1000;
            }
            return (int)PageViewState["_ExportPdfMaxQty"];
        }
        set
        {
            PageViewState["_ExportPdfMaxQty"] = value;
        }
    }

    /// <summary>
    /// 匯出Excel檔名
    /// </summary>
    public string ucExportName
    {
        get
        {
            if (PageViewState["_ExportName"] == null)
            {
                PageViewState["_ExportName"] = string.Format("Export_{0}.xls", DateTime.Today.ToString("yyyyMMdd"));
            }
            return PageViewState["_ExportName"].ToString();
        }
        set
        {
            PageViewState["_ExportName"] = value;
        }
    }

    /// <summary>
    /// 匯出ExcelOpenXml檔名
    /// </summary>
    public string ucExportOpenXmlName
    {
        get
        {
            if (PageViewState["_ExcelOpenXmlName"] == null)
            {
                PageViewState["_ExcelOpenXmlName"] = string.Format("Export2007_{0}.xlsx", DateTime.Today.ToString("yyyyMMdd"));
            }
            return PageViewState["_ExcelOpenXmlName"].ToString();
        }
        set
        {
            PageViewState["_ExcelOpenXmlName"] = value;
        }
    }

    /// <summary>
    /// 匯出ExcelOpenXml密碼(預設 null)
    /// </summary>
    public string ucExportOpenXmlPassword
    {
        //2016.12.13 新增
        get
        {
            return (string)PageViewState["_ExportOpenXmlPassword"];
        }
        set
        {
            PageViewState["_ExportOpenXmlPassword"] = value;
        }
    }

    /// <summary>
    /// 匯出ExcelOpenXml表頭(預設 null)
    /// </summary>
    public string ucExportOpenXmlHeader
    {
        //2017.03.21 新增
        get
        {
            return (string)PageViewState["_ExportOpenXmlHeader"];
        }
        set
        {
            PageViewState["_ExportOpenXmlHeader"] = value;
        }
    }

    /// <summary>
    /// 匯出ExcelOpenXml表尾(預設 null)
    /// <para>** 可用 \n 將資料換行 **</para>
    /// </summary>
    public string ucExportOpenXmlFooter
    {
        //2017.03.21 新增
        get
        {
            return (string)PageViewState["_ExportOpenXmlFooter"];
        }
        set
        {
            PageViewState["_ExportOpenXmlFooter"] = value;
        }
    }


    /// <summary>
    /// 匯出Word檔名
    /// </summary>
    public string ucExportWordName
    {
        get
        {
            if (PageViewState["_ExportWordName"] == null)
            {
                PageViewState["_ExportWordName"] = string.Format("Export_{0}.doc", DateTime.Today.ToString("yyyyMMdd"));
            }
            return PageViewState["_ExportWordName"].ToString();
        }
        set
        {
            PageViewState["_ExportWordName"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf檔名
    /// </summary>
    public string ucExportPdfName
    {
        get
        {
            if (PageViewState["_ExportPdfName"] == null)
            {
                PageViewState["_ExportPdfName"] = string.Format("Export_{0}.pdf", DateTime.Today.ToString("yyyyMMdd"));
            }
            return PageViewState["_ExportPdfName"].ToString();
        }
        set
        {
            PageViewState["_ExportPdfName"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf抬頭
    /// </summary>
    public string ucExportPdfTitle
    {
        get
        {
            if (PageViewState["_ExportPdfTitle"] == null)
            {
                PageViewState["_ExportPdfTitle"] = "";
            }
            return PageViewState["_ExportPdfTitle"].ToString();
        }
        set
        {
            PageViewState["_ExportPdfTitle"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf浮水印文字
    /// </summary>
    public string ucExportPdfWaterMark
    {
        get
        {
            if (PageViewState["_ExportPdfWatermark"] == null)
            {
                PageViewState["_ExportPdfWatermark"] = "";
            }
            return PageViewState["_ExportPdfWatermark"].ToString();
        }
        set
        {
            PageViewState["_ExportPdfWatermark"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf浮水印文字旋轉角度(預設 45)
    /// </summary>
    public float ucExportPdfWaterMarkRotation
    {
        get
        {
            if (PageViewState["_ExportPdfWatermarkRotation"] == null)
            {
                PageViewState["_ExportPdfWatermarkRotation"] = 45;
            }
            return float.Parse(PageViewState["_ExportPdfWatermarkRotation"].ToString());
        }
        set
        {
            PageViewState["_ExportPdfWatermarkRotation"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf浮水印文字大小(預設 80)
    /// </summary>
    public float ucExportPdfWaterMarkTextSize
    {
        get
        {
            if (PageViewState["_ExportPdfWatermarkTextSize"] == null)
            {
                PageViewState["_ExportPdfWatermarkTextSize"] = 80;
            }
            return float.Parse(PageViewState["_ExportPdfWatermarkTextSize"].ToString());
        }
        set
        {
            PageViewState["_ExportPdfWatermarkTextSize"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf是否允許列印(預設 false)
    /// </summary>
    public bool ucExportPdfAllowPrint
    {
        get
        {
            if (PageViewState["_ExportPdfPrint"] == null)
            {
                PageViewState["_ExportPdfPrint"] = false;
            }
            return (bool)(PageViewState["_ExportPdfPrint"]);
        }
        set
        {
            PageViewState["_ExportPdfPrint"] = value;
        }
    }

    /// <summary>
    /// 匯出Pdf是否允許複製(預設 false)
    /// </summary>
    public bool ucExportPdfAllowCopy
    {
        get
        {
            if (PageViewState["_ExportPdfCopy"] == null)
            {
                PageViewState["_ExportPdfCopy"] = false;
            }
            return (bool)(PageViewState["_ExportPdfCopy"]);
        }
        set
        {
            PageViewState["_ExportPdfCopy"] = value;
        }
    }

    #endregion

    #region [Multilingual] 相關定義
    /// <summary>
    /// 使用[Multilingual]功能(預設 false)
    /// </summary>
    public bool ucMultilingualEnabled
    {
        get
        {
            if (PageViewState["_IsEnableMultilingual"] == null)
            {
                PageViewState["_IsEnableMultilingual"] = false;
            }
            return (bool)(PageViewState["_IsEnableMultilingual"]);
        }
        set
        {
            PageViewState["_IsEnableMultilingual"] = value;
        }
    }

    /// <summary>
    /// 當 ucMultilingualEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucMultilingualEnabledDataColName
    {
        get
        {
            if (PageViewState["_MultilingualEnabledDataColName"] == null)
            {
                PageViewState["_MultilingualEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_MultilingualEnabledDataColName"]);
        }
        set
        {
            PageViewState["_MultilingualEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]提示訊息
    /// </summary>
    public string ucMultilingualToolTip
    {
        get
        {
            if (PageViewState["_Msg_Multilingual"] == null)
            {
                PageViewState["_Msg_Multilingual"] = RS.Resources.GridView_Multilingual;
            }
            return PageViewState["_Msg_Multilingual"].ToString();
        }
        set
        {
            PageViewState["_Msg_Multilingual"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]圖示
    /// </summary>
    public string ucMultilingualIcon
    {
        get
        {
            if (PageViewState["_Icon_Multilingual"] == null)
            {
                PageViewState["_Icon_Multilingual"] = Util.Icon_Multilingual;
            }
            return PageViewState["_Icon_Multilingual"].ToString();
        }
        set
        {
            PageViewState["_Icon_Multilingual"] = value;
        }
    }
    #endregion

    #region [Information] 相關定義
    /// <summary>
    /// 使用[Information]功能(預設 false)
    /// </summary>
    public bool ucInformationEnabled
    {
        get
        {
            if (PageViewState["_IsEnableInformation"] == null)
            {
                PageViewState["_IsEnableInformation"] = false;
            }
            return (bool)(PageViewState["_IsEnableInformation"]);
        }
        set
        {
            PageViewState["_IsEnableInformation"] = value;
        }
    }

    /// <summary>
    /// 當 ucInformationEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucInformationEnabledDataColName
    {
        get
        {
            if (PageViewState["_InformationEnabledDataColName"] == null)
            {
                PageViewState["_InformationEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_InformationEnabledDataColName"]);
        }
        set
        {
            PageViewState["_InformationEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Information]提示訊息
    /// </summary>
    public string ucInformationToolTip
    {
        get
        {
            if (PageViewState["_Msg_Information"] == null)
            {
                PageViewState["_Msg_Information"] = RS.Resources.GridView_Information;
            }
            return PageViewState["_Msg_Information"].ToString();
        }
        set
        {
            PageViewState["_Msg_Information"] = value;
        }
    }

    /// <summary>
    /// [Information]圖示
    /// </summary>
    public string ucInformationIcon
    {
        get
        {
            if (PageViewState["_Icon_Information"] == null)
            {
                PageViewState["_Icon_Information"] = Util.Icon_Information;
            }
            return PageViewState["_Icon_Information"].ToString();
        }
        set
        {
            PageViewState["_Icon_Information"] = value;
        }
    }
    #endregion

    #region [Print] 相關定義
    /// <summary>
    /// 使用[Print]功能(預設 false)
    /// </summary>
    public bool ucPrintEnabled
    {
        get
        {
            if (PageViewState["_IsPrintEnabled"] == null)
            {
                PageViewState["_IsPrintEnabled"] = false;
            }
            return (bool)(PageViewState["_IsPrintEnabled"]);
        }
        set
        {
            PageViewState["_IsPrintEnabled"] = value;
        }
    }

    /// <summary>
    /// [Print]圖示
    /// </summary>
    public string ucPrintIcon
    {
        get
        {
            if (PageViewState["_Icon_Print"] == null)
            {
                PageViewState["_Icon_Print"] = Util.Icon_Print;
            }
            return PageViewState["_Icon_Print"].ToString();
        }
        set
        {
            PageViewState["_Icon_Print"] = value;
        }
    }

    /// <summary>
    /// [Print]提示訊息
    /// </summary>
    public string ucPrintToolTip
    {
        get
        {
            if (PageViewState["_Msg_Print"] == null)
            {
                PageViewState["_Msg_Print"] = RS.Resources.GridView_Print;
            }
            return PageViewState["_Msg_Print"].ToString();
        }
        set
        {
            PageViewState["_Msg_Print"] = value;
        }
    }
    #endregion

    #region [Select] 相關定義
    /// <summary>
    /// 使用[Select]功能(預設 false)
    /// </summary>
    public bool ucSelectEnabled
    {
        get
        {
            if (PageViewState["_IsEnableSelect"] == null)
            {
                PageViewState["_IsEnableSelect"] = false;
            }
            return (bool)(PageViewState["_IsEnableSelect"]);
        }
        set
        {
            PageViewState["_IsEnableSelect"] = value;
        }
    }

    /// <summary>
    /// 使用整個資料列進行 Select 功能(預設 false)
    /// <remarks>由於啟用時，會將整個資料列都變成 Select 感測區，故資料列的其他功能按鈕將失去作用</remarks>
    /// </summary>
    public bool ucSelectRowEnabled
    {
        //2016.08.04 新增
        get
        {
            if (PageViewState["_SelectRowEnabled"] == null)
            {
                PageViewState["_SelectRowEnabled"] = false;
            }
            return (bool)(PageViewState["_SelectRowEnabled"]);
        }
        set
        {
            PageViewState["_SelectRowEnabled"] = value;
        }
    }

    /// <summary>
    /// 當 ucSelectEnabled=true ，想進一步控制各資料列是否出現圖示
    /// <para>可利用本屬性設定需檢查的資料欄位，若判斷後為[Y]，圖示才會出現</para>
    /// </summary>
    public string ucSelectEnabledDataColName
    {
        get
        {
            if (PageViewState["_SelectEnabledDataColName"] == null)
            {
                PageViewState["_SelectEnabledDataColName"] = "";
            }
            return (string)(PageViewState["_SelectEnabledDataColName"]);
        }
        set
        {
            PageViewState["_SelectEnabledDataColName"] = value;
        }
    }

    /// <summary>
    /// [Select]提示訊息
    /// </summary>
    public string ucSelectToolTip
    {
        get
        {
            if (PageViewState["_Msg_Select"] == null)
            {
                PageViewState["_Msg_Select"] = RS.Resources.GridView_Select;
            }
            return PageViewState["_Msg_Select"].ToString();
        }
        set
        {
            PageViewState["_Msg_Select"] = value;
        }
    }

    /// <summary>
    /// [Select]圖示
    /// </summary>
    public string ucSelectIcon
    {
        get
        {
            if (PageViewState["_Icon_Select"] == null)
            {
                PageViewState["_Icon_Select"] = Util.Icon_Select;
            }
            return PageViewState["_Icon_Select"].ToString();
        }
        set
        {
            PageViewState["_Icon_Select"] = value;
        }
    }
    #endregion

    #region [Subtotal] 相關定義
    /// <summary>
    /// [Subtotal]提示訊息
    /// </summary>
    public string ucSubtotalToolTip
    {
        get
        {
            if (PageViewState["_Msg_Subtotal"] == null)
            {
                PageViewState["_Msg_Subtotal"] = RS.Resources.GridView_Subtotal;
            }
            return PageViewState["_Msg_Subtotal"].ToString();
        }
        set
        {
            PageViewState["_Msg_Subtotal"] = value;
        }
    }
    #endregion

    #region [GroupSubtotal] 相關定義
    /// <summary>
    /// [GroupSubtotal]提示訊息
    /// </summary>
    public string ucGroupSubtotalToolTip
    {
        get
        {
            if (PageViewState["_Msg_GroupSubtotal"] == null)
            {
                PageViewState["_Msg_GroupSubtotal"] = RS.Resources.GridView_GroupSubtotal;
            }
            return PageViewState["_Msg_GroupSubtotal"].ToString();
        }
        set
        {
            PageViewState["_Msg_GroupSubtotal"] = value;
        }
    }
    #endregion

    #region DataEdit 按鈕相關定義

    /// <summary>
    /// 線上批次編輯按鈕顯示位置(預設 Top)
    /// </summary>
    public PagerPosition ucDataEditButtonPosition
    {
        get
        {
            if (PageViewState["_DataEditButtonPosition"] == null)
            {
                PageViewState["_DataEditButtonPosition"] = PagerPosition.Top;
            }
            return (PagerPosition)PageViewState["_DataEditButtonPosition"];
        }
        set
        {
            PageViewState["_DataEditButtonPosition"] = value;
        }
    }


    /// <summary>
    /// 使用[UpdateAll]功能(搭配批次編輯使用，預設 true)
    /// </summary>
    public bool ucUpdateAllEnabled
    {
        get
        {
            if (PageViewState["_UpdateAllEnabled"] == null)
            {
                PageViewState["_UpdateAllEnabled"] = true;
            }
            return (bool)(PageViewState["_UpdateAllEnabled"]);
        }
        set
        {
            PageViewState["_UpdateAllEnabled"] = value;
        }
    }

    /// <summary>
    /// [UpdateAll]按鈕抬頭
    /// </summary>
    public string ucUpdateAllCaption
    {
        get
        {
            if (PageViewState["_Msg_UpdateAll"] == null)
            {
                PageViewState["_Msg_UpdateAll"] = RS.Resources.GridView_UpdateAll;
            }
            return PageViewState["_Msg_UpdateAll"].ToString();
        }
        set
        {
            PageViewState["_Msg_UpdateAll"] = value;
        }
    }

    /// <summary>
    /// [UpdateAllConfirm]提示訊息
    /// </summary>
    public string ucUpdateAllConfirm
    {
        get
        {
            if (PageViewState["_Msg_UpdateAllConfirm"] == null)
            {
                PageViewState["_Msg_UpdateAllConfirm"] = RS.Resources.GridView_UpdateAllConfirm;
            }
            return PageViewState["_Msg_UpdateAllConfirm"].ToString();
        }
        set
        {
            PageViewState["_Msg_UpdateAllConfirm"] = value;
        }

    }

    /// <summary>
    /// 使用[DeleteAll]功能(搭配批次編輯使用，預設 false)
    /// </summary>
    public bool ucDeleteAllEnabled
    {
        get
        {
            if (PageViewState["_DeleteAllEnabled"] == null)
            {
                PageViewState["_DeleteAllEnabled"] = false;
            }
            return (bool)(PageViewState["_DeleteAllEnabled"]);
        }
        set
        {
            PageViewState["_DeleteAllEnabled"] = value;
        }
    }

    /// <summary>
    /// [DeleteAll]按鈕抬頭
    /// </summary>
    public string ucDeleteAllCaption
    {
        get
        {
            if (PageViewState["_Msg_DeleteAll"] == null)
            {
                PageViewState["_Msg_DeleteAll"] = RS.Resources.GridView_DeleteAll;
            }
            return PageViewState["_Msg_DeleteAll"].ToString();
        }
        set
        {
            PageViewState["_Msg_DeleteAll"] = value;
        }
    }

    /// <summary>
    /// [DeleteAllConfirm]提示訊息
    /// </summary>
    public string ucDeleteAllConfirm
    {
        get
        {
            if (PageViewState["_Msg_DeleteAllConfirm"] == null)
            {
                PageViewState["_Msg_DeleteAllConfirm"] = RS.Resources.GridView_DeleteAllConfirm;
            }
            return PageViewState["_Msg_DeleteAllConfirm"].ToString();
        }
        set
        {
            PageViewState["_Msg_DeleteAllConfirm"] = value;
        }
    }
    #endregion

    #region CSS 相關屬性
    /// <summary>
    /// GridLines 樣式
    /// </summary>
    public GridLines ucGridLines
    {
        get
        {
            if (PageViewState["_GridLines"] == null)
            {
                PageViewState["_GridLines"] = GridLines.Both;
            }
            return (GridLines)PageViewState["_GridLines"];
        }
        set
        {
            PageViewState["_GridLines"] = value;
        }
    }

    /// <summary>
    /// 表頭CSS
    /// </summary>
    public string ucHeaderCssClass
    {
        get
        {
            if (PageViewState["_HeaderCssClass"] == null)
            {
                PageViewState["_HeaderCssClass"] = "Util_gvHeader";
            }
            return PageViewState["_HeaderCssClass"].ToString();
        }
        set
        {
            PageViewState["_HeaderCssClass"] = value;
        }
    }

    /// <summary>
    /// 群組列CSS
    /// </summary>
    public string ucGroupHeaderCssClass
    {
        get
        {
            if (PageViewState["_GroupHeaderCssClass"] == null)
            {
                PageViewState["_GroupHeaderCssClass"] = "Util_gvGroupHeader";
            }
            return PageViewState["_GroupHeaderCssClass"].ToString();
        }
        set
        {
            PageViewState["_GroupHeaderCssClass"] = value;
        }
    }

    /// <summary>
    /// 表尾CSS
    /// </summary>
    public string ucFooterCssClass
    {
        get
        {
            if (PageViewState["_FooterCssClass"] == null)
            {
                PageViewState["_FooterCssClass"] = "Util_gvFooter";
            }
            return PageViewState["_FooterCssClass"].ToString();
        }
        set
        {
            PageViewState["_FooterCssClass"] = value;
        }
    }

    /// <summary>
    /// 單數資料列CSS
    /// </summary>
    public string ucRowCssClass
    {
        get
        {
            if (PageViewState["_RowCssClass"] == null)
            {
                PageViewState["_RowCssClass"] = "Util_gvRowNormal";
            }
            return PageViewState["_RowCssClass"].ToString();
        }
        set
        {
            PageViewState["_RowCssClass"] = value;
        }
    }

    /// <summary>
    /// 偶數資料列CSS
    /// </summary>
    public string ucAlternatingRowCssClass
    {
        get
        {
            if (PageViewState["_AlternatingRowCssClass"] == null)
            {
                PageViewState["_AlternatingRowCssClass"] = "Util_gvRowAlternate";
            }
            return PageViewState["_AlternatingRowCssClass"].ToString();
        }
        set
        {
            PageViewState["_AlternatingRowCssClass"] = value;
        }
    }

    /// <summary>
    /// 可排序表頭CSS
    /// </summary>
    public string ucSortableHeaderCssClass
    {
        get
        {
            if (PageViewState["_SortableHeaderCssClass"] == null)
            {
                PageViewState["_SortableHeaderCssClass"] = "Util_gvHeaderSortable";
            }
            return PageViewState["_SortableHeaderCssClass"].ToString();
        }
        set
        {
            PageViewState["_SortableHeaderCssClass"] = value;
        }
    }

    /// <summary>
    /// 表頭升冪欄位CSS
    /// </summary>
    public string ucSortedAscendingHeaderCssClass
    {
        get
        {
            if (PageViewState["_SortedAscendingHeaderCssClass"] == null)
            {
                PageViewState["_SortedAscendingHeaderCssClass"] = "Util_gvHeaderAsc";
            }
            return PageViewState["_SortedAscendingHeaderCssClass"].ToString();
        }
        set
        {
            PageViewState["_SortedAscendingHeaderCssClass"] = value;
        }
    }

    /// <summary>
    /// 表頭降冪欄位CSS
    /// </summary>
    public string ucSortedDescendingHeaderCssClass
    {
        get
        {
            if (PageViewState["_SortedDescendingHeaderCssClass"] == null)
            {
                PageViewState["_SortedDescendingHeaderCssClass"] = "Util_gvHeaderDesc";
            }
            return PageViewState["_SortedDescendingHeaderCssClass"].ToString();
        }
        set
        {
            PageViewState["_SortedDescendingHeaderCssClass"] = value;
        }
    }

    /// <summary>
    /// [整批更新] CSS
    /// </summary>
    public string ucUpdateAllCssClass
    {
        get
        {
            if (PageViewState["_UpdateAllCssClass"] == null)
            {
                PageViewState["_UpdateAllCssClass"] = "Util_clsBtnGray";
            }
            return PageViewState["_UpdateAllCssClass"].ToString();
        }
        set
        {
            PageViewState["_UpdateAllCssClass"] = value;
        }
    }


    /// <summary>
    /// [整批刪除] CSS
    /// </summary>
    public string ucDeleteAllCssClass
    {
        get
        {
            if (PageViewState["_DeleteAllCssClass"] == null)
            {
                PageViewState["_DeleteAllCssClass"] = "Util_clsBtnGray";
            }
            return PageViewState["_DeleteAllCssClass"].ToString();
        }
        set
        {
            PageViewState["_DeleteAllCssClass"] = value;
        }
    }


    /// <summary>
    /// Width 計量單位是否為[像素]
    /// </summary>
    public bool ucIsWidthByPixel
    {
        get
        {
            if (PageViewState["_IsWidthByPixel"] == null)
            {
                PageViewState["_IsWidthByPixel"] = true;
            }
            return (bool)(PageViewState["_IsWidthByPixel"]);
        }
        set
        {
            PageViewState["_IsWidthByPixel"] = value;
        }
    }

    /// <summary>
    /// [整批更新] Width
    /// </summary>
    public int ucUpdateAllWidth
    {
        get
        {
            if (PageViewState["_UpdateAllWidth"] == null)
            {
                PageViewState["_UpdateAllWidth"] = 150;
            }
            return (int)(PageViewState["_UpdateAllWidth"]);
        }
        set
        {
            PageViewState["_UpdateAllWidth"] = value;
        }
    }

    /// <summary>
    /// [整批刪除] Width
    /// </summary>
    public int ucDeleteAllWidth
    {
        get
        {
            if (PageViewState["_DeleteAllWidth"] == null)
            {
                PageViewState["_DeleteAllWidth"] = 150;
            }
            return (int)(PageViewState["_DeleteAllWidth"]);
        }
        set
        {
            PageViewState["_DeleteAllWidth"] = value;
        }
    }
    #endregion



    #region 凍結相關
    /// <summary>
    /// 啟用凍結表頭(預設 false)
    /// </summary>
    public bool ucFreezeHeaderEnabled
    {
        get
        {
            if (PageViewState["_FreezeHeaderEnabled"] == null)
            {
                PageViewState["_FreezeHeaderEnabled"] = false;
            }
            return (bool)(PageViewState["_FreezeHeaderEnabled"]);
        }
        set
        {
            PageViewState["_FreezeHeaderEnabled"] = value;
        }
    }

    /// <summary>
    /// 欄位凍結數量
    /// </summary>
    public int ucFreezeColQty
    {
        get
        {
            if (PageViewState["_FreezeColQty"] == null)
            {
                PageViewState["_FreezeColQty"] = -1;
            }
            return (int)(PageViewState["_FreezeColQty"]);
        }
        set
        {
            PageViewState["_FreezeColQty"] = value;
        }
    }

    /// <summary>
    /// 凍結時的清單寬度(預設 750)
    /// </summary>
    public int ucFreezeWidth
    {
        get
        {
            if (PageViewState["_FreezeWidth"] == null)
            {
                PageViewState["_FreezeWidth"] = 750;
            }
            return (int)(PageViewState["_FreezeWidth"]);
        }
        set
        {
            PageViewState["_FreezeWidth"] = value;
        }
    }

    /// <summary>
    /// 凍結時的清單高度(預設 350)
    /// </summary>
    public int ucFreezeHeight
    {
        get
        {
            if (PageViewState["_FreezeHeight"] == null)
            {
                PageViewState["_FreezeHeight"] = 350;
            }
            return (int)(PageViewState["_FreezeHeight"]);
        }
        set
        {
            PageViewState["_FreezeHeight"] = value;
        }
    }

    #endregion

    #region 其他屬性定義

    /// <summary>
    /// [整批更新]按鈕ClientID
    /// </summary>
    public string ucUpdateAllClientID
    {
        get
        {
            return btnUpdateAll1.ClientID;
        }
    }

    /// <summary>
    /// [整批刪除]按鈕ClientID
    /// </summary>
    public string ucDeleteAllClientID
    {
        get
        {
            return btnDeleteAll1.ClientID;
        }
    }

    /// <summary>
    /// 資料內容強制不折行(預設 false)
    /// </summary>
    public bool ucDataForceNoWrap
    {
        get
        {
            if (PageViewState["_DataForceNoWrap"] == null)
            {
                PageViewState["_DataForceNoWrap"] = false;
            }
            return (bool)(PageViewState["_DataForceNoWrap"]);
        }
        set
        {
            PageViewState["_DataForceNoWrap"] = value;
        }
    }


    /// <summary>
    /// 等待匯出處理時的顯示訊息
    /// </summary>
    public string ucExportWaitMsg
    {
        get
        {
            if (PageViewState["_ExportWaitMsg"] == null)
            {
                PageViewState["_ExportWaitMsg"] = RS.Resources.Msg_ExportDataPreparing;
            }
            return PageViewState["_ExportWaitMsg"].ToString();
        }
        set
        {
            PageViewState["_ExportWaitMsg"] = value;
        }
    }

    /// <summary>
    /// 無資料時的顯示訊息
    /// </summary>
    public string ucEmptyDataHtmlMsg
    {
        get
        {
            if (PageViewState["_EmptyDataHtmlMsg"] == null)
            {
                PageViewState["_EmptyDataHtmlMsg"] = Util.getHtmlMessage(Util.HtmlMessageKind.DataNotFound);
            }
            return PageViewState["_EmptyDataHtmlMsg"].ToString();
        }
        set
        {
            PageViewState["_EmptyDataHtmlMsg"] = value;
        }
    }

    /// <summary>
    /// 是否只顯示資料(隱藏功能按鈕區)
    /// </summary>
    public bool ucDisplayOnly
    {
        get
        {
            if (PageViewState["_IsDisplayOnly"] == null)
            {
                PageViewState["_IsDisplayOnly"] = false;
            }
            return (bool)(PageViewState["_IsDisplayOnly"]);
        }
        set
        {
            PageViewState["_IsDisplayOnly"] = value;
        }
    }

    /// <summary>
    /// 資料是否允許排序(預設 true)
    /// </summary>
    public bool ucSortEnabled
    {
        get
        {
            if (PageViewState["_IsEnableSorting"] == null)
            {
                PageViewState["_IsEnableSorting"] = true;
            }
            return (bool)(PageViewState["_IsEnableSorting"]);
        }
        set
        {
            PageViewState["_IsEnableSorting"] = value;
        }
    }

    /// <summary>
    /// 禁用排序欄位清單(預設 null)
    /// <para>** 彈性指定部份欄位(例：運算式欄位)不能自訂排序 **</para>
    /// </summary>
    public string[] ucSortDisabledFieldList
    {
        //2017.02.13 新增
        get
        {
            return (string[])(PageViewState["_SortDisabledFieldList"]);
        }
        set
        {
            PageViewState["_SortDisabledFieldList"] = value;
        }
    }

    /// <summary>
    /// 功能按鈕是否觸發檢核事件(預設 false)
    /// </summary>
    public bool ucCausesValidation
    {
        get
        {
            if (PageViewState["_CausesValidation"] == null)
            {
                PageViewState["_CausesValidation"] = false;
            }
            return (bool)(PageViewState["_CausesValidation"]);
        }
        set
        {
            PageViewState["_CausesValidation"] = value;
        }
    }

    /// <summary>
    /// 是否使用[流水號]功能
    /// </summary>
    public bool ucSeqNoEnabled
    {
        get
        {
            if (PageViewState["_IsEnableSeqNo"] == null)
            {
                PageViewState["_IsEnableSeqNo"] = true;
            }
            return (bool)(PageViewState["_IsEnableSeqNo"]);
        }
        set
        {
            PageViewState["_IsEnableSeqNo"] = value;
        }
    }

    /// <summary>
    /// [流水號]抬頭
    /// </summary>
    public string ucSeqNoCaption
    {
        get
        {
            if (PageViewState["_Msg_SeqNo"] == null)
            {
                PageViewState["_Msg_SeqNo"] = RS.Resources.GridView_SeqNo;
            }
            return PageViewState["_Msg_SeqNo"].ToString();
        }
        set
        {
            PageViewState["_Msg_SeqNo"] = value;
        }
    }

    /// <summary>
    /// 資料庫來源
    /// </summary>
    public string ucDBName
    {
        get
        {
            if (PageViewState["_DBName"] == null)
            {
                PageViewState["_DBName"] = Util.getAppSetting("app://CfgDefConnDB/");
            }
            return PageViewState["_DBName"].ToString();
        }
        set
        {
            PageViewState["_DBName"] = value;
        }
    }

    /// <summary>
    /// 資料來源SQL(與 ucDataQryTable 二擇一)
    /// </summary>
    /// <remarks>
    /// **若以此屬性當作清單的資料來源，則當來源資料有異動時，控制項會自動連動**
    /// </remarks>
    public string ucDataQrySQL
    {
        get
        {
            if (PageViewState["_DataQrySQL"] == null)
            {
                PageViewState["_DataQrySQL"] = String.Empty;
            }
            return PageViewState["_DataQrySQL"].ToString();
        }
        set
        {
            PageViewState["_DataQrySQL"] = value;
        }
    }

    /// <summary>
    /// 資料計數SQL
    /// <para>** 若 ucDataQrySQL 內含多個 [From] 以致造成 sp_GetPageDataCount 拆解錯誤，可另訂此屬性避開 **</para>
    /// </summary>
    public string ucDataCountSQL
    {
        //2017.02.10 新增
        get
        {
            if (PageViewState["_DataCountSQL"] == null)
            {
                PageViewState["_DataCountSQL"] = String.Empty;
            }
            return PageViewState["_DataCountSQL"].ToString();
        }
        set
        {
            PageViewState["_DataCountSQL"] = value;
        }
    }

    /// <summary>
    /// 資料來源DataTable(與 ucDataQrySQL 二擇一)
    /// </summary>
    /// <remarks>
    /// **若以此屬性當作清單的資料來源，則當來源資料有異動時，仍需由應用系統將更新後的內容自行綁定到此屬性**
    /// </remarks>
    public DataTable ucDataQryTable
    {
        get
        {
            if (PageViewState["_DataQryTable"] == null)
            {
                PageViewState["_DataQryTable"] = null;
            }
            return (DataTable)PageViewState["_DataQryTable"];
        }
        set
        {
            PageViewState["_DataQryTable"] = value;
        }
    }

    /// <summary>
    /// 當此 ucGridView 重新整理(Refresh)時，需一併同步重新整理同頁面其他 ucGridView 物件的ID清單
    /// <para>**當同一頁面使用多個ucGridView，且彼此間的顯示資料有相關聯時，若在其中一個清單做了更新/刪除的動作，但其他 ucGridView 未自動同步更新時，勢必造成使用者的困擾；若將其他需與本 ucGridView 同步的 ucGridView ID 設定在此屬性，即可避免此問題**</para>
    /// </summary>
    public string[] ucSyncRefreshGridViewIDList
    {
        get
        {
            if (PageViewState["_SyncRefreshGridViewIDList"] == null)
            {
                PageViewState["_SyncRefreshGridViewIDList"] = "".Split(',');
            }
            return (string[])PageViewState["_SyncRefreshGridViewIDList"];
        }
        set
        {
            PageViewState["_SyncRefreshGridViewIDList"] = value;
        }
    }


    /// <summary>
    /// 表頭欄位合併數列清單
    /// <para>例： [3,4,2] </para>
    /// </summary>
    public string ucHeaderColSpanQtyList
    {
        //2015.10.08
        get
        {
            if (PageViewState["_HeaderColSpanQtyList"] == null)
            {
                PageViewState["_HeaderColSpanQtyList"] = "";
            }
            return (string)(PageViewState["_HeaderColSpanQtyList"]);
        }
        set
        {
            PageViewState["_HeaderColSpanQtyList"] = value;
        }
    }

    /// <summary>
    /// 表頭欄位合併抬頭清單
    /// <para>例： [基本,附屬,性質] </para>
    /// </summary>
    public string ucHeaderColSpanCaptionList
    {
        //2015.10.08
        get
        {
            if (PageViewState["_HeaderColSpanCaptionList"] == null)
            {
                PageViewState["_HeaderColSpanCaptionList"] = "";
            }
            return (string)(PageViewState["_HeaderColSpanCaptionList"]);
        }
        set
        {
            PageViewState["_HeaderColSpanCaptionList"] = value;
        }
    }


    /// <summary>
    /// 資料群組欄位
    /// </summary>
    public string ucDataGroupKey
    {
        get
        {
            if (PageViewState["_DataGroupKey"] == null)
            {
                PageViewState["_DataGroupKey"] = "";
            }
            return (string)(PageViewState["_DataGroupKey"]);
        }
        set
        {
            PageViewState["_DataGroupKey"] = value;
        }
    }

    /// <summary>
    /// 資料群組欄位抬頭
    /// </summary>
    public string ucDataGroupCaption
    {
        //2016.12.09 新增，用於資料匯出時
        get
        {
            if (PageViewState["_ucDataGroupCaption"] == null)
            {
                PageViewState["_ucDataGroupCaption"] = RS.Resources.GridView_Group;
            }
            return PageViewState["_ucDataGroupCaption"].ToString();
        }
        set
        {
            PageViewState["_ucDataGroupCaption"] = value;
        }
    }

    /// <summary>
    /// 群組加總欄位清單
    /// </summary>
    public string[] ucDataGroupSubtotalList
    {
        get
        {
            if (PageViewState["_DataGroupSubtotalList"] == null)
            {
                PageViewState["_DataGroupSubtotalList"] = new string[] { };
            }
            return (string[])(PageViewState["_DataGroupSubtotalList"]);
        }
        set
        {
            PageViewState["_DataGroupSubtotalList"] = value;
        }
    }

    /// <summary>
    /// 預設展開所有群組(預設 true)
    /// </summary>
    public bool ucDefGroupExpandAll
    {   //2016.12.13 新增
        get
        {
            if (PageViewState["_DefGroupExpandAll"] == null)
            {
                PageViewState["_DefGroupExpandAll"] = true;
            }
            return (bool)(PageViewState["_DefGroupExpandAll"]);
        }
        set
        {
            PageViewState["_DefGroupExpandAll"] = value;
        }
    }

    /// <summary>
    /// [展開所有群組]提示訊息
    /// </summary>
    public string ucGroupExpandAll
    {
        get
        {
            if (PageViewState["_GroupExpandAll"] == null)
            {
                PageViewState["_GroupExpandAll"] = RS.Resources.GridView_GroupExpandAll;
            }
            return PageViewState["_GroupExpandAll"].ToString();
        }
        set
        {
            PageViewState["_GroupExpandAll"] = value;
        }
    }

    /// <summary>
    /// [收合所有群組]提示訊息
    /// </summary>
    public string ucGroupCollapseAll
    {
        get
        {
            if (PageViewState["_GroupCollapseAll"] == null)
            {
                PageViewState["_GroupCollapseAll"] = RS.Resources.GridView_GroupCollapseAll;
            }
            return PageViewState["_GroupCollapseAll"].ToString();
        }
        set
        {
            PageViewState["_GroupCollapseAll"] = value;
        }
    }

    /// <summary>
    /// 資料群組欄位顯示格式
    /// </summary>
    public string ucGroupHeaderFormat
    {
        get
        {
            if (PageViewState["_GroupHeaderFormat"] == null)
            {
                PageViewState["_GroupHeaderFormat"] = "[{0}]";
            }
            return (string)(PageViewState["_GroupHeaderFormat"]);
        }
        set
        {
            PageViewState["_GroupHeaderFormat"] = value;
        }
    }


    /// <summary>
    /// 資料鍵值欄位分隔字符(預設 [,])
    /// </summary>
    public string ucDataKeyDelimiter
    {
        //2017.01.24 新增，讓AP可自訂合適的分隔符號
        get
        {
            if (PageViewState["_DataKeyDelimiter"] == null)
            {
                PageViewState["_DataKeyDelimiter"] = ",";
            }
            return (string)(PageViewState["_DataKeyDelimiter"]);
        }
        set
        {
            PageViewState["_DataKeyDelimiter"] = value;
        }
    }

    /// <summary>
    /// 資料鍵值欄位清單
    /// </summary>
    public string[] ucDataKeyList
    {
        get
        {
            if (PageViewState["_DataKeyList"] == null)
            {
                PageViewState["_DataKeyList"] = new string[] { };
            }
            return (string[])(PageViewState["_DataKeyList"]);
        }
        set
        {
            PageViewState["_DataKeyList"] = value;
        }
    }


    /// <summary>
    /// 是否支援自動產生顯示欄位(預設 true)
    /// </summary>
    public bool ucDataDisplayAutoGenerateEnabled
    {
        get
        {
            if (PageViewState["_DataDisplayAutoGenerateEnabled"] == null)
            {
                PageViewState["_DataDisplayAutoGenerateEnabled"] = true;
            }
            return (bool)(PageViewState["_DataDisplayAutoGenerateEnabled"]);
        }
        set
        {
            PageViewState["_DataDisplayAutoGenerateEnabled"] = value;
        }
    }

    /// <summary>
    /// 顯示欄位格式(使用Dictionary物件)
    /// <para></para>
    /// <para>文字資料　，格式[欄位名稱]　[抬頭[@[x]yyy]]] 　x為(L/C/R)、yyy為指定寬度(整數為像素，小數為百分比)，例: 'POID'      , '訂單號碼@C80'</para>
    /// <para>日期資料　，格式[欄位名稱]　[抬頭[@[x]yyy]]] 　x為(D/T/S)、yyy為指定寬度(整數為像素，小數為百分比)，例: 'PODate'    , '下單日期@D0.15'</para>
    /// <para>數值資料　，格式[欄位名稱]　[抬頭[@N[x]yyy]]]　x為小數位 、yyy為指定寬度(整數為像素，小數為百分比)，例: 'TotAmt'    , '訂單總額@N1'</para>
    /// <para>百分比資料，格式[欄位名稱]　[抬頭[@P[x]yyy]]]　x為小數位 、yyy為指定寬度(整數為像素，小數為百分比)，例: 'TotPect'   , '佔單比例@P1'</para>
    /// <para>圖片資料　，格式[欄位名稱]　[抬頭[@I[xxx[,yyy[,zzz]]]]]    xxx為指定寬度(整數為像素，小數為百分比)、yyy為URL欄位、zzz為Target欄位，例: 'ProdGraph', '產品圖片@I96,SysUrl,SysTarget'</para>
    /// <para>超連結資料，格式[欄位名稱]　[抬頭[@A[xxx[,yyy[,zzz]]]]]    xxx為指定寬度(整數為像素，小數為百分比)、yyy為URL欄位、zzz為Target欄位，例: 'SysName'  , '系統名稱@A120,SysUrl,SysTarget'</para>
    /// <para>布林資料　，格式[欄位名稱]　[抬頭[@Y[xxx]]]                xxx為指定寬度(整數為像素，小數為百分比)，例: 'IsEnabled', '啟用狀態@Y'　（欄位資料除支援Bool型態外，文字或數字型態的 Y/1 , N/0 也都支援）</para>
    /// <para>布林資料　，格式[欄位名稱]　[抬頭[@M[xxx[,yyy[,zzz]]]]] 　 xxx為指定寬度(整數為像素，小數為百分比)、yyy為「是」時的顯示內容、zzz為「否」時的顯示內容，例: 'IsMail', '發送郵件@M,★,☆'　（欄位資料除支援Bool型態外，文字或數字型態的 Y/1 , N/0 也都支援）</para>
    /// <para></para>
    /// <para>若設定「文字資料」欄位時，格式為[抬頭[@[x]yyy[,zzz]]]] ，則zzz視為[項目清單]顯示文字來源JSON(若此處未設定，但在 ucDataEditDefinition 的 CheckBoxList/RadioList有定義 ucSourceDictionary，則一樣會自動偵測並轉換)</para>
    /// </summary>
    public Dictionary<string, string> ucDataDisplayDefinition
    {
        get
        {
            if (PageViewState["_DataDisplayDefinition"] == null)
            {
                PageViewState["_DataDisplayDefinition"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)(PageViewState["_DataDisplayDefinition"]);
        }
        set
        {
            PageViewState["_DataDisplayDefinition"] = value;
        }
    }

    /// <summary>
    /// 線上編輯欄位定義(欄位數量請盡量精簡，以提昇執行效能)
    /// <para></para>
    /// <para>在可用的控制項清單內，選擇一種用來編輯欄位，例: 'PODate', 'Calendar'</para>
    /// <para>在選定編輯的控制項物件後面，可外加 @[xxDictionary]來指定要套用的屬性，例:</para>
    /// <para>　Dictionary&lt;string, string&gt; oEdit = new Dictionary&lt;string, string&gt;(); //編輯欄位</para>
    /// <para>　Dictionary&lt;string, string&gt; oProp = new Dictionary&lt;string, string&gt;(); //物件屬性</para>
    /// <para>　oProp.Add('ucIsRequire', 'True');</para>
    /// <para>　oEdit.Add('ItemDesc', 'TextBox@' + Util.getJSON(oDicProperty)); </para>
    /// <para></para>
    /// <para>可用的控制項如下：</para>
    /// <para>　** TextBox       參數係以 ucTextBox      實作，參考該物件提供的屬性即可套用</para>
    /// <para>　** Calendar      參數係以 ucDatePicker   實作，參考該物件提供的屬性即可套用</para>
    /// <para>　** DropDownList  參數係以 DropDownList   實作</para>
    /// <para>　** DropDownFirst 參數係以 DropDownList   實作，預設選擇第一個項目</para>
    /// <para>　** CheckBox      參數係以 CheckBox       實作</para>
    /// <para>　** CheckBoxList  參數係以 ucCheckBoxList 實作，參考該物件提供的屬性即可套用</para>
    /// <para>　** RadioList     參數係以 ucRadioList    實作，參考該物件提供的屬性即可套用</para>
    /// <remarks>
    /// 若 DropDownList 的來源對照表需動態從資料列特定欄位取得，則其 Dictionary 物件只需定義一筆，key='JSON' ,value='來源欄位名稱'
    /// 若 CheckBoxList 的來源對照表需動態從資料列特定欄位取得，則其 ucSourceDictionary 屬性的 Dictionary物件 只需定義一筆，key='JSON' ,value='來源欄位名稱'
    /// 若 RadioList    的來源對照表需動態從資料列特定欄位取得，則其 ucSourceDictionary 屬性的 Dictionary物件 只需定義一筆，key='JSON' ,value='來源欄位名稱'
    /// </remarks>
    /// </summary>
    public Dictionary<string, string> ucDataEditDefinition
    {
        get
        {
            if (PageViewState["_DataEditDefinition"] == null)
            {
                PageViewState["_DataEditDefinition"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)(PageViewState["_DataEditDefinition"]);
        }
        set
        {
            PageViewState["_DataEditDefinition"] = value;
            if (value.Count > 0)
            {
                //若有定義可編輯欄位，自動啟用 [Check] 功能
                ucCheckEnabled = true;
            }
        }
    }

    /// <summary>
    /// 加總欄位清單
    /// </summary>
    public string[] ucDataSubtotalList
    {
        get
        {
            if (PageViewState["_DataSubtotalList"] == null)
            {
                PageViewState["_DataSubtotalList"] = new string[] { };
            }
            return (string[])(PageViewState["_DataSubtotalList"]);
        }
        set
        {
            PageViewState["_DataSubtotalList"] = value;
        }
    }

    /// <summary>
    /// 顯示分頁導引列分隔線(預設 false)
    /// </summary>
    public bool ucPagerDivLineEnabled
    {
        //2015.10.08
        get
        {
            if (PageViewState["_PagerDivLineEnabled"] == null)
            {
                PageViewState["_PagerDivLineEnabled"] = false;
            }
            return (bool)(PageViewState["_PagerDivLineEnabled"]);
        }
        set
        {
            PageViewState["_PagerDivLineEnabled"] = value;
        }
    }

    /// <summary>
    /// 分頁導引列位置(預設 Bottom)
    /// </summary>
    public PagerPosition ucPagerPosition
    {
        get
        {
            if (PageViewState["_PagerPosition"] == null)
            {
                PageViewState["_PagerPosition"] = PagerPosition.Bottom;
            }
            return (PagerPosition)PageViewState["_PagerPosition"];
        }
        set
        {
            PageViewState["_PagerPosition"] = value;
        }
    }

    /// <summary>
    /// 預設每頁筆數清單(預設 [10,20,30,50,100])
    /// </summary>
    public int[] ucDefPageSizeList
    {
        get
        {
            if (PageViewState["_DefPageSizeList"] == null)
            {
                PageViewState["_DefPageSizeList"] = new int[] { 10, 20, 30, 50, 100 };
            }
            return (int[])(PageViewState["_DefPageSizeList"]);
        }
        set
        {
            PageViewState["_DefPageSizeList"] = value;
        }
    }

    /// <summary>
    /// 預設每頁筆數(預設 10)
    /// </summary>
    public int ucDefPageSize
    {
        get
        {
            if (PageViewState["_DefPageSize"] == null)
            {
                PageViewState["_DefPageSize"] = 10;
            }
            return (int)PageViewState["_DefPageSize"];
        }
        set
        {
            PageViewState["_DefPageSize"] = value;
        }
    }

    /// <summary>
    /// 預設顯示頁次(預設 1)
    /// </summary>
    public int ucDefPageNo
    {
        get
        {
            if (PageViewState["_DefPageNo"] == null)
            {
                PageViewState["_DefPageNo"] = 1;
            }
            return (int)PageViewState["_DefPageNo"];
        }
        set
        {
            PageViewState["_DefPageNo"] = value;
        }
    }

    /// <summary>
    /// 預設排序欄位(預設 空白)
    /// </summary>
    public string ucDefSortExpression
    {
        get
        {
            if (PageViewState["_DefSortExpression"] == null)
            {
                PageViewState["_DefSortExpression"] = string.Empty;
            }
            return PageViewState["_DefSortExpression"].ToString();
        }
        set
        {
            PageViewState["_DefSortExpression"] = value;
        }
    }

    /// <summary>
    /// 預設升降冪(預設 空白)
    /// </summary>
    public string ucDefSortDirection
    {
        get
        {
            if (PageViewState["_DefSortDirection"] == null)
            {
                PageViewState["_DefSortDirection"] = string.Empty;
            }
            return PageViewState["_DefSortDirection"].ToString();
        }
        set
        {
            PageViewState["_DefSortDirection"] = value;
        }
    }

    /// <summary>
    /// 目前資料總筆數
    /// </summary>
    public int ucTotQty
    {
        get
        {
            if (PageViewState["_TotQty"] == null)
            {
                PageViewState["_TotQty"] = -1;  //未初始過
            }
            return (int)(PageViewState["_TotQty"]);
        }
        set
        {
            PageViewState["_TotQty"] = value;
        }
    }

    /// <summary>
    /// 每頁筆數清單(預設 ucDefPageSizeList)
    /// </summary>
    public int[] ucPageSizeList
    {
        get
        {
            if (PageViewState["_PageSizeList"] == null)
            {
                PageViewState["_PageSizeList"] = ucDefPageSizeList;
            }
            return (int[])(PageViewState["_PageSizeList"]);
        }
        set
        {
            PageViewState["_PageSizeList"] = value;
        }
    }

    /// <summary>
    /// 目前每頁筆數(預設 ucDefPageSize)
    /// </summary>
    public int ucPageSize
    {
        get
        {
            if (PageViewState["_PageSize"] == null)
            {
                PageViewState["_PageSize"] = ucDefPageSize;
            }
            return (int)(PageViewState["_PageSize"]);
        }
        set
        {
            PageViewState["_PageSize"] = value;
        }
    }

    /// <summary>
    /// 目前顯示頁次(預設 ucDefPageNo)
    /// </summary>
    public int ucPageNo
    {
        get
        {
            if (PageViewState["_PageNo"] == null)
            {
                PageViewState["_PageNo"] = ucDefPageNo;
            }
            return (int)(PageViewState["_PageNo"]);
        }
        set
        {
            PageViewState["_PageNo"] = value;
        }
    }

    /// <summary>
    /// 目前排序欄位(預設 ucDefSortExpression)
    /// </summary>
    public string ucSortExpression
    {
        get
        {
            if (PageViewState["_SortExpression"] == null)
            {
                PageViewState["_SortExpression"] = ucDefSortExpression;
            }
            return PageViewState["_SortExpression"].ToString();
        }
        set
        {
            PageViewState["_SortExpression"] = value;
        }
    }

    /// <summary>
    /// 目前排序欄位(預設 ucDefSortDirection)
    /// </summary>
    public string ucSortDirection
    {
        get
        {
            if (PageViewState["_SortDirection"] == null)
            {
                PageViewState["_SortDirection"] = ucDefSortDirection;
            }
            return PageViewState["_SortDirection"].ToString();
        }
        set
        {
            PageViewState["_SortDirection"] = value;
        }
    }

    /// <summary>
    /// 已選取的資料鍵值清單
    /// </summary>
    public string[] ucCheckedKeyList
    {
        get
        {
            ArrayList oList = new ArrayList();
            foreach (GridViewRow row in gvMain.Rows)
            {
                CheckBox oChk = (CheckBox)row.FindControl("chkRow");
                HiddenField oKey = (HiddenField)row.FindControl("chkRowKey");
                if (oChk != null && oKey != null)
                {
                    if (oChk.Checked == true)
                    {
                        oList.Add(oKey.Value);
                    }
                }
            }

            return oList.ToArray(typeof(string)) as string[];
        }
    }
    #endregion

    #region 自訂 GrdiView 事件
    /// <summary>
    /// [萬用GridView] 控制項 GridView 事件參數
    /// </summary>
    public class GridViewEventArgs : EventArgs
    {
        string _CommandName;
        DataTable _DataTable;

        /// <summary>
        /// 命令名稱
        /// </summary>
        public string CommandName
        {
            set { _CommandName = value; }
            get { return _CommandName; }
        }

        /// <summary>
        /// 資料表
        /// </summary>
        public DataTable DataTable
        {
            set { _DataTable = value; }
            get { return _DataTable; }
        }
    }

    /// <summary>
    /// [萬用GridView] 控制項 GridView 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void GridViewClick(object sender, GridViewEventArgs e);
    /// <summary>
    /// [萬用GridView] 控制項 GridView 事件
    /// </summary>
    public event GridViewClick GridViewCommand;

    #endregion

    #region 自訂 RowCommand 事件
    /// <summary>
    /// [萬用GridView] 控制項 RowCommand 事件參數
    /// </summary>
    public class RowCommandEventArgs : EventArgs
    {
        //CommandName, e.CommandArgument
        string _CommandName;
        string[] _DataKeys;

        /// <summary>
        /// 命令名稱
        /// </summary>
        public string CommandName
        {
            set { _CommandName = value; }
            get { return _CommandName; }
        }

        /// <summary>
        /// 資料鍵值
        /// </summary>
        public string[] DataKeys
        {
            set { _DataKeys = value; }
            get { return _DataKeys; }
        }
    }

    /// <summary>
    /// [萬用GridView] 控制項 RowCommand 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void GridViewRowClick(object sender, RowCommandEventArgs e);

    /// <summary>
    /// [萬用GridView] 控制項 RowCommand 事件
    /// </summary>
    public event GridViewRowClick RowCommand;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ucLightBox.ucLightBoxMsg = ucExportWaitMsg;
        if (IsPostBack)
        {
            string evt = Util.getRequestFormKey("__EVENTTARGET").Replace('$', '_');
            if (!string.IsNullOrEmpty(evt))
            {
                //避開 MS 在 GridView 使用 ImageButton會引發兩次PostBack 的 BUG，全面改用 LinkButton
                //處理 LinkButton 事件
                // __EVENTTARGET 格式: ucGridMain$gvMain$ctl01$lnkPOID

                //當觸發的物件為 ucGridView 才需處理
                if (sender.GetType().Name == this.GetType().Name)
                {
                    if (evt.IndexOf(this.ClientID + "_") < 0)
                    {
                        // 當事件來源的ucGridView實體「不是」自己
                        // 而且不是由 AspNetPager1 或是 ddlPageSize1 或是 ddlPageSize2 觸發的事件
                        if (evt.IndexOf(AspNetPager1.ClientID) < 0 && evt.IndexOf(ddlPageSize1.ClientID) < 0 && evt.IndexOf(ddlPageSize2.ClientID) < 0)
                        {
                            if (this.Visible && ucIsAutoRefresh)
                            {
                                //如果沒有需同步重新整理的ucGridView
                                if (string.IsNullOrEmpty(ucSyncRefreshGridViewIDList[0]))
                                {
                                    Refresh();
                                }
                                else
                                {
                                    Util_ucGridView oGridView;
                                    for (int i = 0; i < ucSyncRefreshGridViewIDList.Count(); i++)
                                    {
                                        oGridView = (Util_ucGridView)Util.FindControlEx(Page, ucSyncRefreshGridViewIDList[i]);
                                        if (oGridView != null)
                                        {
                                            if (evt.IndexOf(oGridView.ClientID) < 0)
                                            {
                                                //與來源的ucGridView實體[不需]連動更新
                                                Refresh();
                                                break;
                                            }
                                            else
                                            {
                                                //與來源的ucGridView實體[需要]連動更新
                                                if (evt.LastIndexOf("_btnSortHidden") > 0 || evt.LastIndexOf("_AspNetPager1") > 0 || evt.LastIndexOf("_ddlPageSize1") > 0 || evt.LastIndexOf("_ddlPageSize2") > 0)
                                                {
                                                    //排序、匯出或換頁事件
                                                    Refresh();
                                                }
                                                else
                                                {
                                                    //若為 btnLink / btnExport 相關按鈕，則在 btnLink_Click() 時會處理
                                                }
                                            }
                                        }
                                    }
                                }
                            }   //當目前物件 Visable = true
                        }
                    } //當事件來源的ucGridView實體「不是」自己

                } //this.GetType()

            }  //[evt] Not Empty

        } //IsPostBack
    }

    /// <summary>
    /// 產生 GridView 範本欄位
    /// </summary>
    private void gvMain_AddTempleteColumns()
    {
        if (ucDataSubtotalList.Count() > 0) gvMain.ShowFooter = true;

        foreach (var pair in this.ucDataDisplayDefinition)
        {
            bool IsSortEnabled = ucSortEnabled;
            //2017.02.13 新增禁用排序欄位判斷
            if (ucSortDisabledFieldList != null && ucSortDisabledFieldList.Contains(pair.Key))
            {
                IsSortEnabled = false;
            }

            string strHeader = pair.Value.Split('@')[0];
            string strHeaderCSS = (IsSortEnabled) ? ucSortableHeaderCssClass : ucHeaderCssClass;

            if (IsSortEnabled && ucSortExpression == pair.Key)
            {
                if (ucSortDirection.ToUpper() == "ASC")
                    strHeaderCSS = ucSortedAscendingHeaderCssClass;
                else
                    strHeaderCSS = ucSortedDescendingHeaderCssClass;
            }

            string strFormat = "";
            if (pair.Value.Contains('@'))
            {
                strFormat = pair.Value.Split('@')[1];
            }

            TemplateField tmpField = new TemplateField();
            string strBtnSortPostBackClientID = _doPostBackClientID + "$gvMain" + ((string.IsNullOrEmpty(this.ucHeaderColSpanQtyList)) ? "$ctl01" : "$ctl02") + "$btnSortHidden";

            if (ucDataEditDefinition.ContainsKey(pair.Key))
            {
                //若該欄位需提供編輯功能
                string strEditType = ucDataEditDefinition[pair.Key].Split('@')[0];
                string strEditDataSource = null;
                if (ucDataEditDefinition[pair.Key].Split('@').Count() > 1)
                {
                    //避免 strEditDataSource 內容包含 @ 字元造成誤判，改用Substring()方式取值
                    strEditDataSource = ucDataEditDefinition[pair.Key].Substring(ucDataEditDefinition[pair.Key].IndexOf('@') + 1);
                }
                tmpField.HeaderTemplate = new AddGridViewTemplete(DataControlRowType.Header, pair.Key, IsSortEnabled, strBtnSortPostBackClientID, strHeader, strHeaderCSS);
                tmpField.ItemTemplate = new AddGridViewTemplete(DataControlRowType.DataRow, pair.Key, true, strBtnSortPostBackClientID, strHeader, strFormat, strEditType, Util.getDictionary(strEditDataSource));

                gvMain.Columns.Add(tmpField);
            }
            else
            {
                //該欄位只供顯示
                tmpField.HeaderTemplate = new AddGridViewTemplete(DataControlRowType.Header, pair.Key, IsSortEnabled, strBtnSortPostBackClientID, strHeader, strHeaderCSS);
                tmpField.ItemTemplate = new AddGridViewTemplete(DataControlRowType.DataRow, pair.Key, false, strBtnSortPostBackClientID, strHeader, strFormat);
                gvMain.Columns.Add(tmpField);
            }
        }
    }

    /// <summary>
    /// 取出 gvMain 所需的單頁資料 
    /// </summary>
    /// <param name="IsInit">是否需初始計算</param>
    /// <returns></returns>
    protected DataTable getMainPageData(bool IsInit = false)
    {
        DbHelper db = new DbHelper(this.ucDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dtPage = null;

        //若需初始
        if (IsInit)
        {
            if (this.ucDataQryTable == null)
            {
                //ucDataQrySQL
                sb.Reset();
                // 2017.02.10 增加 ucDataCountSQL 判斷
                if (string.IsNullOrEmpty(this.ucDataCountSQL))
                    sb.AppendStatement("Exec sp_GetPageDataCount ").AppendParameter("MainSQL", this.ucDataQrySQL);
                else
                    sb.AppendStatement("Exec sp_GetPageDataCount ").AppendParameter("MainSQL", this.ucDataCountSQL);

                this.ucTotQty = int.Parse(db.ExecuteScalar(sb.BuildCommand()).ToString());
            }
            else
            {
                //ucDataQryTable
                this.ucTotQty = ucDataQryTable.Rows.Count;
            }

            AspNetPager1.RecordCount = this.ucTotQty;
            AspNetPager1.PageSize = this.ucPageSize;
            AspNetPager1.CurrentPageIndex = this.ucPageNo;

            AspNetPager2.RecordCount = this.ucTotQty;
            AspNetPager2.PageSize = this.ucPageSize;
            AspNetPager2.CurrentPageIndex = this.ucPageNo;
        }

        //取出對應的單頁資料
        if (this.ucDataQryTable == null)
        {
            //ucDataQrySQL
            sb.Reset();
            sb.AppendStatement("Exec sp_GetPageData ");
            sb.AppendParameter("MainSQL", this.ucDataQrySQL);

            if (string.IsNullOrEmpty(this.ucSortExpression))
            {
                //若無 ucSortExpression 傳入 
                if (string.IsNullOrEmpty(ucDataGroupKey))
                {
                    // 無 ucDataGroupKey ，套用 ucDataKeyList 
                    sb.Append(", ").AppendParameter("SortExpDir", Util.getStringJoin(this.ucDataKeyList));
                }
                else
                {
                    // 有 ucDataGroupKey ，需考慮是否與 ucDataKeyList 有衝突
                    if (Util.getStringJoin(this.ucDataKeyList).IndexOf(ucDataGroupKey) > 0)
                    {
                        sb.Append(", ").AppendParameter("SortExpDir", Util.getStringJoin(this.ucDataKeyList));
                    }
                    else
                    {
                        sb.Append(", ").AppendParameter("SortExpDir", ucDataGroupKey + " ," + Util.getStringJoin(this.ucDataKeyList));
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ucDataGroupKey))
                {
                    // 無 ucDataGroupKey ，套用 ucSortExpression 
                    sb.Append(", ").AppendParameter("SortExpDir", this.ucSortExpression + " " + this.ucSortDirection);
                }
                else
                {
                    // 有 ucDataGroupKey ，需考慮是否與 ucSortExpression 有衝突
                    if (ucSortExpression.IndexOf(ucDataGroupKey) > 0)
                    {
                        sb.Append(", ").AppendParameter("SortExpDir", this.ucSortExpression + " " + this.ucSortDirection);
                    }
                    else
                    {
                        sb.Append(", ").AppendParameter("SortExpDir", ucDataGroupKey + " ," + this.ucSortExpression + " " + this.ucSortDirection);
                    }
                }
            }

            sb.Append(", ").AppendParameter("PageSize", this.ucPageSize);
            sb.Append(", ").AppendParameter("PageNo", this.ucPageNo);
            DbCommand oCmd = sb.BuildCommand();  // 可監看 oCmd.CommandText 知道實際的 SQL
            dtPage = db.ExecuteDataSet(oCmd).Tables[0];
        }
        else
        {
            // 2016.12.13 調整判斷邏輯
            if (ucDataQryTable != null && ucDataQryTable.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(this.ucSortExpression))
                {
                    if (string.IsNullOrEmpty(ucDataGroupKey))
                    {
                        ucDataQryTable.DefaultView.Sort = Util.getStringJoin(this.ucDataKeyList);
                    }
                    else
                    {
                        ucDataQryTable.DefaultView.Sort = ucDataGroupKey + " ," + Util.getStringJoin(this.ucDataKeyList);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(ucDataGroupKey))
                    {
                        // 無 ucDataGroupKey ，套用 ucSortExpression 
                        ucDataQryTable.DefaultView.Sort = this.ucSortExpression + " " + this.ucSortDirection;
                    }
                    else
                    {
                        // 有 ucDataGroupKey ，需考慮是否與 ucSortExpression 有衝突
                        if (ucSortExpression.IndexOf(ucDataGroupKey) > 0)
                        {
                            ucDataQryTable.DefaultView.Sort = this.ucSortExpression + " " + this.ucSortDirection;
                        }
                        else
                        {
                            ucDataQryTable.DefaultView.Sort = ucDataGroupKey + " ," + this.ucSortExpression + " " + this.ucSortDirection;
                        }
                    }

                }
            }
            int idxStart = (this.ucPageNo - 1) * this.ucPageSize;
            int idxEnd = (this.ucPageNo * this.ucPageSize) - 1;

            dtPage = ucDataQryTable.Clone();
            for (int i = idxStart; i <= idxEnd; i++)
            {
                if (i < ucTotQty)
                {
                    dtPage.ImportRow(ucDataQryTable.DefaultView[i].Row);
                }
            }
        }

        //2016.06.30 新增 ucDataDisplayAutoGenerateEnabled
        if (dtPage != null && ucDataDisplayDefinition.IsNullOrEmpty() && ucDataDisplayAutoGenerateEnabled)
        {
            ucDataDisplayDefinition.Clear();
            for (int i = 0; i < dtPage.Columns.Count; i++)
            {
                ucDataDisplayDefinition.Add(dtPage.Columns[i].ColumnName, dtPage.Columns[i].ColumnName);
            }
        }

        return dtPage;
    }

    /// <summary>
    /// 取得匯出資料表
    /// </summary>
    /// <param name="strExportType"></param>
    /// <param name="IsReplaceColumnName"></param>
    /// <returns></returns>
    protected DataTable getExportData(string strExportType = "XLS", bool IsReplaceColumnName = true)
    {
        //2016.05.24 從事件處理中獨立出來
        //調整 PageNo / PageSize，以便匯出全部資料
        int tmpPageNo = this.ucPageNo;
        int tmpPageSize = this.ucPageSize;
        this.ucPageNo = 1;

        //匯出上限
        switch (strExportType.ToUpper())
        {
            case "DOC":
                this.ucPageSize = this.ucExportWordMaxQty;
                break;
            case "XLS":
                this.ucPageSize = this.ucExportMaxQty;
                break;
            case "PDF":
                this.ucPageSize = this.ucExportPdfMaxQty;
                break;
            case "XLSX":
                this.ucPageSize = this.ucExportOpenXmlMaxQty;
                break;
            default:
                this.ucPageSize = this.ucExportMaxQty;
                break;
        }
        DataTable dtExport = getMainPageData();

        //處理欄位抬頭及不在GirdView的顯示欄位清單
        //移除欄位抬頭可能包含的 Html Tag 2016.11.25
        string strColName = "";
        ArrayList RemoveColList = new ArrayList();

        if (IsReplaceColumnName)  //代換欄位名稱 2017.02.16 新增
        {
            for (int i = 0; i < dtExport.Columns.Count; i++)
            {
                strColName = dtExport.Columns[i].ColumnName;
                if (this.ucDataDisplayDefinition.ContainsKey(strColName))
                {
                    dtExport.Columns[i].ColumnName = Regex.Replace(this.ucDataDisplayDefinition[strColName].Split('@')[0], "<.*?>", string.Empty); //移除 Html Tag 
                }
                else
                {
                    if (!string.IsNullOrEmpty(ucDataGroupKey) && strColName == ucDataGroupKey)
                    {
                        //2016.12.09 新增群組欄位抬頭的處理
                        dtExport.Columns[i].ColumnName = Regex.Replace(this.ucDataGroupCaption, "<.*?>", string.Empty); //移除 Html Tag 
                    }
                    else
                    {
                        //序號欄位
                        if (strColName == "RowNo" && this.ucSeqNoEnabled)
                            dtExport.Columns[i].ColumnName = Regex.Replace(this.ucSeqNoCaption, "<.*?>", string.Empty); //移除 Html Tag 
                        else
                            RemoveColList.Add(strColName);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < dtExport.Columns.Count; i++)
            {
                strColName = dtExport.Columns[i].ColumnName;
                if (strColName != "RowNo" && !this.ucDataDisplayDefinition.ContainsKey(strColName))
                {
                    RemoveColList.Add(strColName);
                }
            }

            if (!this.ucSeqNoEnabled)
            {
                RemoveColList.Add("RowNo");
            }
        }

        //移除不需匯出的欄位
        if (!ucExportAllField)
        {
            if (RemoveColList.Count > 0)
            {
                foreach (string colName in RemoveColList)
                {
                    if (dtExport.Columns.Contains(colName))
                        dtExport.Columns.Remove(colName);
                }
            }
        }

        //還原 PageNo / PageSize
        this.ucPageNo = tmpPageNo;
        this.ucPageSize = tmpPageSize;

        return dtExport;
    }

    /// <summary>
    /// 取出插入群組列後的單頁資料 
    /// </summary>
    /// <param name="dtMainPageData">原始單頁資料</param>
    /// <returns></returns>
    protected DataTable getGroupMainPageData(DataTable dtMainPageData)
    {
        if (string.IsNullOrEmpty(ucDataGroupKey))
        {
            //若無群組Key
            return dtMainPageData;
        }
        else
        {
            //有群組Key
            DataTable dtGroup = dtMainPageData.Clone();
            DataRow dtGroupRow;
            string strCurrGrpKey = "";
            string strDataGrpKey = "";

            decimal[] decCurrGrpSubtotal = new decimal[ucDataGroupSubtotalList.Count()];
            for (int i = 0; i < dtMainPageData.Rows.Count; i++)
            {
                strDataGrpKey = dtMainPageData.Rows[i][ucDataGroupKey].ToString().Trim();
                if (strCurrGrpKey != strDataGrpKey)
                {
                    //處理上一個群組的加總值(GroupFooter)
                    if (!string.IsNullOrEmpty(strCurrGrpKey) && ucDataGroupSubtotalList.Count() > 0)
                    {
                        dtGroupRow = dtGroup.NewRow();
                        dtGroupRow[ucDataGroupKey] = string.Format("GF_{0}", strCurrGrpKey);
                        for (int j = 0; j < ucDataGroupSubtotalList.Count(); j++)
                        {
                            dtGroupRow[ucDataGroupSubtotalList[j]] = decCurrGrpSubtotal[j];
                        }
                        decCurrGrpSubtotal = new decimal[dtGroup.Columns.Count];
                        dtGroup.Rows.Add(dtGroupRow);

                    }
                    //增加群組抬頭列，故意加上[GH_]以資區別(GroupHeader)
                    dtGroupRow = dtGroup.NewRow();
                    dtGroupRow[ucDataGroupKey] = string.Format("GH_{0}", strDataGrpKey);
                    dtGroup.Rows.Add(dtGroupRow);
                    strCurrGrpKey = strDataGrpKey;
                }

                if (ucDataGroupSubtotalList.Count() > 0)
                {
                    for (int j = 0; j < ucDataGroupSubtotalList.Count(); j++)
                    {
                        decCurrGrpSubtotal[j] += decimal.Parse("0" + dtMainPageData.Rows[i][ucDataGroupSubtotalList[j]].ToString());
                    }
                }

                dtGroup.ImportRow(dtMainPageData.Rows[i]);
            }
            //處理 for 迴圈不會處理到的最後一個群組
            if (!string.IsNullOrEmpty(strCurrGrpKey) && ucDataGroupSubtotalList.Count() > 0)
            {
                dtGroupRow = dtGroup.NewRow();
                dtGroupRow[ucDataGroupKey] = string.Format("GF_{0}", strCurrGrpKey);
                for (int j = 0; j < ucDataGroupSubtotalList.Count(); j++)
                {
                    dtGroupRow[ucDataGroupSubtotalList[j]] = decCurrGrpSubtotal[j];
                }
                dtGroup.Rows.Add(dtGroupRow);
            }
            return dtGroup;
        }
    }

    /// <summary>
    /// 收合所有群組
    /// </summary>
    public void GroupCollapseAll()
    {
        if (!string.IsNullOrEmpty(ucDataGroupKey))
        {
            Image oImg = (Image)gvMain.HeaderRow.FindControl("imgGroupCollapse");
            if (oImg != null)
            {
                Util.setJSContent("dom.Ready(function(){ var oImg = document.getElementById('" + oImg.ClientID + "');if(oImg != null)oImg.click(); });", this.ClientID + "_GroupCollapseAll");
            }
        }
    }

    /// <summary>
    /// 展開所有群組
    /// </summary>
    public void GroupExpandAll()
    {
        if (!string.IsNullOrEmpty(ucDataGroupKey))
        {
            Image oImg = (Image)gvMain.HeaderRow.FindControl("imgGroupExpand");
            if (oImg != null)
            {
                Util.setJSContent("dom.Ready(function(){ var oImg = document.getElementById('" + oImg.ClientID + "');if(oImg != null)oImg.click(); });", this.ClientID + "_GroupExpandAll");
            }
        }
    }

    /// <summary>
    /// 重置所有設定
    /// </summary>
    public void Reset()
    {
        this.PageViewState.Clear();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    /// <param name="IsInit">是否需初始計算(預設 false)</param>
    /// <remarks>初始時，才會計算要套用的CSS、資料總筆數、頁面下拉清單。。。等</remarks>
    public void Refresh(bool IsInit = false)
    {
        //必要參數檢查
        ArrayList oErrParaList = new ArrayList();
        if (string.IsNullOrEmpty(ucDataQrySQL) && ucDataQryTable == null) oErrParaList.Add("Need [ucDataQrySQL]  or  [ucDataQryTable] , [ucDBName] is Optional");
        if (ucDataKeyList.Count() <= 0) oErrParaList.Add("Need [ucDataKeyList]");

        if (ucDataDisplayDefinition.Count <= 0)
        {
            //檢查 ucDataDisplayAutoGenerateEnabled 2016.06.30 新增
            if (ucDataDisplayAutoGenerateEnabled)
                getMainPageData();
            else
                oErrParaList.Add("Need [ucDataDisplayDefinition] or [ucDataDisplayAutoGenerateEnabled = true]");
        }

        //檢查 ucAclEnabled & ucDataEditDefinition 2016.05.13
        if (ucAclEnabled && ucDataEditDefinition.Count > 0)
        {
            if (!AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Edit))
            {
                ucDataEditDefinition.Clear();
                ucCheckEnabled = false;
            }
        }

        //檢查 ucAclEnabled & ucCheckEnabled 2016.05.13
        if (ucAclEnabled && ucCheckEnabled)
        {
            ucCheckEnabled = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Query);
        }

        //檢查最大可編輯欄位數量 2016.03.11
        if (ucDataEditDefinition.Count > _DataEditDefinitionMaxQty) oErrParaList.Add(string.Format("The [ucDataEditDefinition] Maximum Qty are [{0}]", _DataEditDefinitionMaxQty));

        if (ucDataKeyList.Count() > 0 && ucDataEditDefinition.Count() > 0)
        {
            for (int i = 0; i < ucDataKeyList.Count(); i++)
            {
                if (ucDataEditDefinition.ContainsKey(ucDataKeyList[i]))
                {
                    oErrParaList.Add("Key Filed [" + ucDataKeyList[i] + "] Can not in  ucDataEditDefinition");
                }
            }
        }

        if (!string.IsNullOrEmpty(ucHeaderColSpanQtyList) || !string.IsNullOrEmpty(ucHeaderColSpanCaptionList))
        {
            if (string.IsNullOrEmpty(ucHeaderColSpanQtyList) || string.IsNullOrEmpty(ucHeaderColSpanCaptionList))
            {
                oErrParaList.Add("[ucHeaderColSpanQtyList] or [ucHeaderColSpanCpationList] is Empty ");
            }
            else
            {
                if (ucHeaderColSpanQtyList.Split(',').Count() != ucHeaderColSpanCaptionList.Split(',').Count())
                {
                    oErrParaList.Add("[ucHeaderColSpanQtyList] and [ucHeaderColSpanCpationList] item qty not matched ");
                }

                if (ucDataDisplayDefinition.Count > 0)
                {
                    string[] arQtyList = ucHeaderColSpanQtyList.Split(',');
                    int intTotQty = 0;
                    int intChkQty = 0;
                    for (int i = 0; i < arQtyList.Count(); i++)
                    {
                        int.TryParse(arQtyList[i], out intChkQty);
                        intTotQty += intChkQty;
                    }

                    if (intTotQty != ucDataDisplayDefinition.Count)
                    {
                        oErrParaList.Add(string.Format("[ucHeaderColSpanQtyList] must be equal to the sum of the number [{0}]", ucDataDisplayDefinition.Count));
                    }
                }
            }
        }

        if (ucFreezeHeaderEnabled)
        {
            if (!string.IsNullOrEmpty(ucDataGroupKey) || !string.IsNullOrEmpty(ucHeaderColSpanQtyList))
            {
                oErrParaList.Add("When [ucFreezeHeaderEnabled] = true , [ucDataGroupKey] or [ucHeaderColSpanQtyList] will not be used");
            }
        }

        if (ucFreezeColQty > 0)
        {
            if (ucDataEditDefinition != null && ucDataEditDefinition.Count > 0)
            {
                oErrParaList.Add("When [ucFreezeColQty] > 0 , [ucDataEditDefinition] can not be used");
            }
        }

        if (oErrParaList.Count > 0)
        {
            //參數檢核錯誤
            divGridview.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(" [{0}] Error !<hr><li>{1}", this.ID, Util.getStringJoin(oErrParaList, "<li>")));
        }
        else
        {
            //參數檢核成功
            if (IsInit)
            {
                //初始化
                //處理每頁筆數下拉選單
                ddlPageSize1.Items.Clear();
                for (int i = 0; i < this.ucPageSizeList.Count(); i++)
                {
                    ddlPageSize1.Items.Add(new ListItem(this.ucPageSizeList[i].ToString()));
                }
                ddlPageSize1.SelectedValue = this.ucPageSize.ToString();

                ddlPageSize2.Items.Clear();
                for (int i = 0; i < this.ucPageSizeList.Count(); i++)
                {
                    ddlPageSize2.Items.Add(new ListItem(this.ucPageSizeList[i].ToString()));
                }
                ddlPageSize2.SelectedValue = this.ucPageSize.ToString();

                //目前頁次
                this.ucPageNo = this.ucDefPageNo;

                //線上編輯按鈕顯示位置 2016.10.20
                switch (ucDataEditButtonPosition)
                {
                    case PagerPosition.Bottom:
                        divDataEditButtonArea2.Style["display"] = "";
                        break;
                    case PagerPosition.Top:
                        divDataEditButtonArea1.Style["display"] = "";
                        break;
                    case PagerPosition.TopAndBottom:
                        divDataEditButtonArea1.Style["display"] = "";
                        divDataEditButtonArea2.Style["display"] = "";
                        break;
                }

                //初始GridView所需屬性及物件
                gvMain.GridLines = ucGridLines;
                gvMain.HeaderStyle.CssClass = ucHeaderCssClass;
                gvMain.FooterStyle.CssClass = ucFooterCssClass;
                gvMain.RowStyle.CssClass = ucRowCssClass;
                gvMain.AlternatingRowStyle.CssClass = ucAlternatingRowCssClass;
                gvMain.SortedAscendingHeaderStyle.CssClass = ucSortedAscendingHeaderCssClass;
                gvMain.SortedDescendingHeaderStyle.CssClass = ucSortedDescendingHeaderCssClass;
            }

            //一般Refresh()
            gvMain.EmptyDataText = ucEmptyDataHtmlMsg;
            labPerPageItem1.Text = RS.Resources.GridView_PerPageItem;
            labPerPageItem2.Text = RS.Resources.GridView_PerPageItem;
            //產生範本欄位
            gvMain_AddTempleteColumns();
            //Tooltip範本
            HoverTooltip1.HtmlTemplate = ucHoverTooltipTemplete;
            //初始加總值暫存陣列
            if (ucDataSubtotalList.Count() > 0)
                _SubtotalList = new decimal[ucDataSubtotalList.Count()];
            //取得目前顯示頁面資料
            DataTable dt = getMainPageData(IsInit);

            //若有群組GroupKey
            if (!string.IsNullOrEmpty(ucDataGroupKey))
            {
                //加工處理，插入群組列
                dt = getGroupMainPageData(dt);
            }

            gvMain.DataKeyNames = this.ucDataKeyList;
            gvMain.DataSource = dt;
            gvMain.DataBind();
            ddlPageSize1.SelectedValue = this.ucPageSize.ToString();
            ddlPageSize2.SelectedValue = this.ucPageSize.ToString();

            divPagerHR1.Style["display"] = "none";
            divPageSizeChanger1.Style["display"] = "none";
            AspNetPager1.Visible = false;
            divPagerHR2.Style["display"] = "none";
            divPageSizeChanger2.Style["display"] = "none";
            AspNetPager2.Visible = false;

            string strJS = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                //凍結時的相關處理 2015.11.27
                if (ucFreezeHeaderEnabled)
                {
                    Util.setJS(Util._JSjQueryUrl);
                    Util.setJS(Util._JSjQueryUiUrl);
                    Util.setJS(Util._JSGridViewScrollUrl);

                    string strScrollJS = "\n\n";
                    strScrollJS += "$(function () { \n";

                    strScrollJS += "$('#" + gvMain.ClientID + " th').css('white-space', 'nowrap'); \n";
                    strScrollJS += "$('#" + gvMain.ClientID + " td').css('white-space', 'nowrap'); \n";

                    strScrollJS += "$('#" + gvMain.ClientID + "').gridviewScroll({ \n";
                    strScrollJS += "       varrowtopimg: '" + Util.getFixURL(Util._ImagePath + "/arrowvt.png") + "', \n";
                    strScrollJS += "    varrowbottomimg: '" + Util.getFixURL(Util._ImagePath + "/arrowvb.png") + "', \n";
                    strScrollJS += "      harrowleftimg: '" + Util.getFixURL(Util._ImagePath + "/arrowhl.png") + "', \n";
                    strScrollJS += "     harrowrightimg: '" + Util.getFixURL(Util._ImagePath + "/arrowhr.png") + "', \n";

                    strScrollJS += "    width:  " + ucFreezeWidth + ", \n";
                    strScrollJS += "    height: " + ucFreezeHeight + ", \n";
                    strScrollJS += "    headerrowcount: " + (string.IsNullOrEmpty(ucHeaderColSpanQtyList) ? 1 : 2) + ", \n";
                    if (ucFreezeColQty > 0)
                    {
                        strScrollJS += "    freezesize: " + (ucFreezeColQty + 3) + ", \n";
                    }

                    strScrollJS += "    arrowsize: 30, railsize: 16, barsize: 10 \n";
                    strScrollJS += "  }); \n";
                    strScrollJS += "}); \n";

                    Util.setJSContent(strScrollJS, this.ClientID + "_Scroll");

                    AspNetPager1.Width = ucFreezeWidth;
                    AspNetPager2.Width = ucFreezeWidth;
                }
                else
                {
                    if (ucDataForceNoWrap)
                    {
                        Util.setJS(Util._JSjQueryUrl);

                        string strNoWrapJS = "\n\n";
                        strNoWrapJS += "$(function () { \n";

                        strNoWrapJS += "$('#" + gvMain.ClientID + " th').css('white-space', 'nowrap'); \n";
                        strNoWrapJS += "$('#" + gvMain.ClientID + " td').css('white-space', 'nowrap'); \n";

                        strNoWrapJS += "}); \n";

                        Util.setJSContent(strNoWrapJS, this.ClientID + "_NoWrap");
                    }
                }

                //處理分頁列
                if (this.ucTotQty > this.ucPageSize)
                {
                    switch (ucPagerPosition)
                    {
                        case PagerPosition.Bottom:
                            //處理「分頁物件」
                            if (ucPagerDivLineEnabled)
                            {
                                divPagerHR2.Style["display"] = "";
                            }
                            AspNetPager2.Visible = true;

                            //處理「每頁筆數」
                            strJS = "dom.Ready(function(){var oPager = document.getElementById('" + AspNetPager2.ClientID + "');var oSizeList = document.getElementById('" + divPageSizeChanger2.ClientID + "');if (oPager != null && oSizeList != null){oPager.insertBefore(oSizeList, oPager.firstChild); oSizeList.style.display='';} else { if (oSizeList != null) oSizeList.style.display='none';} });";
                            Util.setJSContent(strJS, this.ClientID + "_AspNetPage2");
                            AspNetPager2.TextAfterPageIndexBox = string.Format(RS.Resources.GridView_TextAfterPageIndexBox, AspNetPager2.PageCount, this.ucTotQty); //" ／ {0:N0} 頁，共　<b>{1:N0}</b> 筆資料"
                            break;
                        case PagerPosition.Top:
                            //處理「分頁物件」
                            if (ucPagerDivLineEnabled)
                            {
                                divPagerHR1.Style["display"] = "";
                            }
                            AspNetPager1.Visible = true;

                            //處理「每頁筆數」
                            strJS = "dom.Ready(function(){var oPager = document.getElementById('" + AspNetPager1.ClientID + "');var oSizeList = document.getElementById('" + divPageSizeChanger1.ClientID + "');if (oPager != null && oSizeList != null){oPager.insertBefore(oSizeList, oPager.firstChild); oSizeList.style.display='';} else { if (oSizeList != null) oSizeList.style.display='none';} });";
                            Util.setJSContent(strJS, this.ClientID + "_AspNetPage1");
                            AspNetPager1.TextAfterPageIndexBox = string.Format(RS.Resources.GridView_TextAfterPageIndexBox, AspNetPager1.PageCount, this.ucTotQty); //" ／ {0:N0} 頁，共　<b>{1:N0}</b> 筆資料"
                            break;
                        case PagerPosition.TopAndBottom:
                            //處理「分頁物件」
                            if (ucPagerDivLineEnabled)
                            {
                                divPagerHR1.Style["display"] = "";
                                divPagerHR2.Style["display"] = "";
                            }
                            AspNetPager1.Visible = true;
                            AspNetPager2.Visible = true;

                            //處理「每頁筆數」
                            strJS = "dom.Ready(function(){var oPager = document.getElementById('" + AspNetPager1.ClientID + "');var oSizeList = document.getElementById('" + divPageSizeChanger1.ClientID + "');if (oPager != null && oSizeList != null){oPager.insertBefore(oSizeList, oPager.firstChild); oSizeList.style.display='';} else { if (oSizeList != null) oSizeList.style.display='none';} });";
                            Util.setJSContent(strJS, this.ClientID + "_AspNetPage1");
                            AspNetPager1.TextAfterPageIndexBox = string.Format(RS.Resources.GridView_TextAfterPageIndexBox, AspNetPager1.PageCount, this.ucTotQty); //" ／ {0:N0} 頁，共　<b>{1:N0}</b> 筆資料"

                            strJS = "dom.Ready(function(){var oPager = document.getElementById('" + AspNetPager2.ClientID + "');var oSizeList = document.getElementById('" + divPageSizeChanger2.ClientID + "');if (oPager != null && oSizeList != null){oPager.insertBefore(oSizeList, oPager.firstChild); oSizeList.style.display='';} else { if (oSizeList != null) oSizeList.style.display='none';} });";
                            Util.setJSContent(strJS, this.ClientID + "_AspNetPage2");
                            AspNetPager2.TextAfterPageIndexBox = string.Format(RS.Resources.GridView_TextAfterPageIndexBox, AspNetPager2.PageCount, this.ucTotQty); //" ／ {0:N0} 頁，共　<b>{1:N0}</b> 筆資料"
                            break;
                    }
                }
            }
        }

    }

    /// <summary>
    /// GridView 資料Created事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //保留欄位
        //[0] Group
        //[1] CheckBox
        //[2] 功能按鈕
        //[3] 流水號

        //Header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (((DataTable)((GridView)sender).DataSource) != null)
            {
                if (((DataTable)((GridView)sender).DataSource).Rows.Count <= 0)
                {
                    //無資料
                    e.Row.Cells[0].Style["display"] = "none";
                    e.Row.Cells[1].Style["display"] = "none";
                    if (this.ucDisplayOnly)
                    {
                        e.Row.Cells[2].Style["display"] = "none";
                    }
                }
                else
                {
                    //有資料
                    if (this.ucDisplayOnly)
                    {
                        e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";
                        e.Row.Cells[1].Style["display"] = "none";
                        e.Row.Cells[2].Style["display"] = "none";
                        e.Row.Cells[3].Text = this.ucSeqNoCaption;
                        e.Row.Cells[3].CssClass = "Util_WordBreak";
                        e.Row.Cells[3].Visible = this.ucSeqNoEnabled;
                    }
                    else
                    {
                        e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";

                        e.Row.Cells[1].ToolTip = this.ucCheckAllToolTip;
                        e.Row.Cells[1].Style["display"] = this.ucCheckEnabled ? "" : "none";

                        e.Row.Cells[3].Text = this.ucSeqNoCaption;
                        e.Row.Cells[3].CssClass = "Util_WordBreak";
                        e.Row.Cells[3].Visible = this.ucSeqNoEnabled;
                    }
                }
            }

            //處理跨欄位抬頭 2015.10.08 
            if (!string.IsNullOrEmpty(ucHeaderColSpanQtyList) && !string.IsNullOrEmpty(ucHeaderColSpanCaptionList))
            {
                string[] arQtyList = ucHeaderColSpanQtyList.Split(',');
                string[] arCaptionList = ucHeaderColSpanCaptionList.Split(',');
                if (arQtyList.Count() == arCaptionList.Count())
                {
                    GridViewRow oColSpanRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell oCell = new TableCell();

                    int intColSpan = 0;
                    //保留欄位[0]
                    if (!string.IsNullOrEmpty(ucDataGroupKey))
                        intColSpan += 1;
                    //保留欄位[1]
                    if (ucCheckEnabled && !ucDisplayOnly)
                        intColSpan += 1;
                    //保留欄位[2]
                    if (!ucDisplayOnly)
                        intColSpan += 1;
                    //保留欄位[3]
                    if (ucSeqNoEnabled)
                        intColSpan += 1;

                    if (intColSpan > 0)
                    {
                        oCell.ColumnSpan = intColSpan;
                        oColSpanRow.Cells.Add(oCell);
                    }


                    //資料欄位
                    for (int i = 0; i < arQtyList.Count(); i++)
                    {
                        oCell = new TableCell();
                        oCell.ColumnSpan = int.Parse(arQtyList[i]);
                        oCell.Text = arCaptionList[i];
                        oCell.HorizontalAlign = HorizontalAlign.Center;
                        oColSpanRow.Cells.Add(oCell);
                    }
                    GridView oGridView = (GridView)sender;
                    oGridView.Controls[0].Controls.AddAt(0, oColSpanRow);
                }
            }
        }

        //DataRow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.ucDisplayOnly)
            {
                e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";
                e.Row.Cells[1].Style["display"] = "none";
                e.Row.Cells[2].Style["display"] = "none";
                e.Row.Cells[3].Visible = this.ucSeqNoEnabled;
            }
            else
            {
                e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";
                e.Row.Cells[1].Style["display"] = this.ucCheckEnabled ? "" : "none";
                e.Row.Cells[3].Visible = this.ucSeqNoEnabled;
            }
        }

        //Footer
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            if (this.ucDisplayOnly)
            {
                e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";
                e.Row.Cells[1].Style["display"] = "none";
                e.Row.Cells[2].Style["display"] = "none";
                e.Row.Cells[3].Visible = this.ucSeqNoEnabled;

            }
            else
            {
                e.Row.Cells[0].Style["display"] = string.IsNullOrEmpty(ucDataGroupKey) ? "none" : "";
                e.Row.Cells[1].Style["display"] = this.ucCheckEnabled ? "" : "none";
                e.Row.Cells[3].Visible = this.ucSeqNoEnabled;
            }
        }
    }

    /// <summary>
    /// GridView 繫結
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label tmpLabel;
        LinkButton tmpBtn;
        Image tmpIcon;
        CheckBox tmpChk;
        HiddenField tmpHid;
        DataRow tmpRow;

        int intTmpPos = -1;
        int intPos = -1;

        string strGroupValue = "";
        string strGroupHeader = "";

        string strRowCheckedJS = "";    //單筆CheckBox選取時的JS
        string strRowUnCheckedJS = "";  //單筆CheckBox取消時的JS
        string strRowGetDataJS = "";    //單筆CheckBox選取時，取的上面所有編輯元件傳回值的JS
        string strChk_ChkAllJS = "";
        string strChk_UpdateAllJS = "";

        strChk_ChkAllJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
        strChk_ChkAllJS += "for (i = 1; i < oGrid.rows.length; i++) {";
        //strChk_ChkAllJS += "  alert('row=[' + i + '],cell[1]=[' + oGrid.rows[i].cells[1].innerHTML + ']');";
        //strChk_ChkAllJS += "  alert('row=[' + i + '],cell[1]=[' + oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0].type + ']');";
        //strChk_ChkAllJS += "  alert ('row=[' + i + '],cell count=[' + oGrid.rows[i].cells.length + ']');";
        strChk_ChkAllJS += "  if (oGrid.rows[i].cells.length > 1){";
        strChk_ChkAllJS += "     var oChk = oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0];";
        strChk_ChkAllJS += "     if (oChk != null && oChk.type == 'checkbox' ) {";
        strChk_ChkAllJS += "       if (!oChk.checked) oGrid.rows[0].cells[1].getElementsByTagName('INPUT')[0].checked = oChk.checked;";
        strChk_ChkAllJS += "     }";
        strChk_ChkAllJS += "  }";
        strChk_ChkAllJS += "}";

        strChk_UpdateAllJS += "document.getElementById('" + btnUpdateAll1.ClientID + "').style.display='none';";
        strChk_UpdateAllJS += "document.getElementById('" + btnUpdateAll2.ClientID + "').style.display='none';";
        strChk_UpdateAllJS += "document.getElementById('" + btnDeleteAll1.ClientID + "').style.display='none';";
        strChk_UpdateAllJS += "document.getElementById('" + btnDeleteAll2.ClientID + "').style.display='none';";
        //停止編輯時還原分頁列
        if (ucTotQty > ucPageSize)
        {
            switch (ucPagerPosition)
            {
                case PagerPosition.Bottom:
                    strChk_UpdateAllJS += "document.getElementById('" + AspNetPager2.ClientID + "').style.display='';";
                    break;
                case PagerPosition.Top:
                    strChk_UpdateAllJS += "document.getElementById('" + AspNetPager1.ClientID + "').style.display='';";
                    break;
                case PagerPosition.TopAndBottom:
                    strChk_UpdateAllJS += "document.getElementById('" + AspNetPager1.ClientID + "').style.display='';";
                    strChk_UpdateAllJS += "document.getElementById('" + AspNetPager2.ClientID + "').style.display='';";
                    break;
            }
        }

        strChk_UpdateAllJS += "for (i = 1; i < oGrid.rows.length; i++) {";
        strChk_UpdateAllJS += "  if (oGrid.rows[i].cells.length > 1){";
        strChk_UpdateAllJS += "     var oChk = oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0];";
        strChk_UpdateAllJS += "     if (oChk != null) {";

        if (ucUpdateAllEnabled)
        {
            strChk_UpdateAllJS += "       if (oChk.checked) document.getElementById('" + btnUpdateAll1.ClientID + "').style.display='';";
            strChk_UpdateAllJS += "       if (oChk.checked) document.getElementById('" + btnUpdateAll2.ClientID + "').style.display='';";
        }

        if (ucDeleteAllEnabled)
        {
            strChk_UpdateAllJS += "       if (oChk.checked) document.getElementById('" + btnDeleteAll1.ClientID + "').style.display='';";
            strChk_UpdateAllJS += "       if (oChk.checked) document.getElementById('" + btnDeleteAll2.ClientID + "').style.display='';";
        }

        //開始編輯時隱藏分頁列
        if (ucTotQty > ucPageSize && (ucUpdateAllEnabled || ucDeleteAllEnabled))
        {
            switch (ucPagerPosition)
            {
                case PagerPosition.Bottom:
                    strChk_UpdateAllJS += "if (oChk.checked) document.getElementById('" + AspNetPager2.ClientID + "').style.display='none';";
                    break;
                case PagerPosition.Top:
                    strChk_UpdateAllJS += "if (oChk.checked) document.getElementById('" + AspNetPager1.ClientID + "').style.display='none';";
                    break;
                case PagerPosition.TopAndBottom:
                    strChk_UpdateAllJS += "if (oChk.checked) document.getElementById('" + AspNetPager1.ClientID + "').style.display='none';";
                    strChk_UpdateAllJS += "if (oChk.checked) document.getElementById('" + AspNetPager2.ClientID + "').style.display='none';";
                    break;
            }
        }

        strChk_UpdateAllJS += "     }";
        strChk_UpdateAllJS += "  }";
        strChk_UpdateAllJS += "}";

        btnUpdateAll1.Text = ucUpdateAllCaption;
        btnUpdateAll1.CssClass = ucUpdateAllCssClass;
        btnUpdateAll1.Width = ucIsWidthByPixel ? Unit.Pixel(ucUpdateAllWidth) : Unit.Percentage(ucUpdateAllWidth);  //2017.02.23
        btnUpdateAll2.Text = ucUpdateAllCaption;
        btnUpdateAll2.CssClass = ucUpdateAllCssClass;
        btnUpdateAll2.Width = ucIsWidthByPixel ? Unit.Pixel(ucUpdateAllWidth) : Unit.Percentage(ucUpdateAllWidth);  //2017.02.23

        btnDeleteAll1.Text = ucDeleteAllCaption;
        btnDeleteAll1.CssClass = ucDeleteAllCssClass;
        btnDeleteAll1.Width = ucIsWidthByPixel ? Unit.Pixel(ucDeleteAllWidth) : Unit.Percentage(ucDeleteAllWidth);  //2017.02.23
        btnDeleteAll2.Text = ucDeleteAllCaption;
        btnDeleteAll2.CssClass = ucDeleteAllCssClass;
        btnDeleteAll2.Width = ucIsWidthByPixel ? Unit.Pixel(ucDeleteAllWidth) : Unit.Percentage(ucDeleteAllWidth);  //2017.02.23

        if (ucDataEditDefinition.Count > 0)
        {
            int i = 0;
            foreach (var pair in ucDataEditDefinition)
            {
                i++;
                strRowCheckedJS += " document.getElementById(prefix + 'lab" + pair.Key + "').style.display='none';";
                strRowUnCheckedJS += " document.getElementById(prefix + 'lab" + pair.Key + "').style.display='';";
                switch (pair.Value.Split('@')[0].ToUpper())
                {
                    case "TEXTBOX": //TextBox
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'txt" + pair.Key + "_txtData').value;";
                        break;
                    case "CHECKBOX": //ChkecBox
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " if (document.getElementById(prefix + 'chk" + pair.Key + "').checked){ document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = 'Y';}else{ document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = 'N';};";
                        break;
                    case "CHECKBOXLIST": //CheckBoxList
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'chk" + pair.Key + "_txtIDList').value;";
                        break;
                    case "RADIOLIST":    //RadioList
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'rad" + pair.Key + "_txtID').value;";
                        break;
                    case "DROPDOWNLIST":   //DropDownList
                    case "DROPDOWNFIRST":  //DropDownFirst  2016.08.05 新增
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'ddl" + pair.Key + "').options[document.getElementById(prefix + 'ddl" + pair.Key + "').selectedIndex].value;";
                        break;
                    //case "CASCADING": //CasCading
                    //    strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                    //    strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                    //    //ddl01
                    //    strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'cas" + pair.Key + "_ddl01').options[document.getElementById(prefix + 'cas" + pair.Key + "_ddl01').selectedIndex].value;";
                    //    //ddl02
                    //    strRowGetDataJS += "if (document.getElementById(prefix + 'cas" + pair.Key + "_ddl02') != null){ ";
                    //    strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ',' + document.getElementById(prefix + 'cas" + pair.Key + "_ddl02').options[document.getElementById(prefix + 'cas" + pair.Key + "_ddl02').selectedIndex].value;";
                    //    strRowGetDataJS += "}";
                    //    //ddl03
                    //    strRowGetDataJS += "if (document.getElementById(prefix + 'cas" + pair.Key + "_ddl03') != null){ ";
                    //    strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ',' + document.getElementById(prefix + 'cas" + pair.Key + "_ddl03').options[document.getElementById(prefix + 'cas" + pair.Key + "_ddl03').selectedIndex].value;";
                    //    strRowGetDataJS += "}";
                    //    //ddl04
                    //    strRowGetDataJS += "if (document.getElementById(prefix + 'cas" + pair.Key + "_ddl04') != null){ ";
                    //    strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ',' + document.getElementById(prefix + 'cas" + pair.Key + "_ddl04').options[document.getElementById(prefix + 'cas" + pair.Key + "_ddl04').selectedIndex].value;";
                    //    strRowGetDataJS += "}";
                    //    //ddl05
                    //    strRowGetDataJS += "if (document.getElementById(prefix + 'cas" + pair.Key + "_ddl05') != null){ ";
                    //    strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ',' + document.getElementById(prefix + 'cas" + pair.Key + "_ddl05').options[document.getElementById(prefix + 'cas" + pair.Key + "_ddl05').selectedIndex].value;";
                    //    strRowGetDataJS += "}";
                    //    break;
                    case "CALENDAR": //Calendar
                        strRowCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='';";
                        strRowUnCheckedJS += " document.getElementById(prefix + 'pnl" + pair.Key + "').style.display='none';";
                        strRowGetDataJS += " document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value = document.getElementById(prefix + 'cal" + pair.Key + "_txtDate').value;";
                        //HH:MM
                        strRowGetDataJS += "if (document.getElementById(prefix + 'ddlMM" + pair.Key + "') != null){ ";
                        strRowGetDataJS += "   document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ' ' + document.getElementById(prefix + 'ddlHH" + pair.Key + "').options[document.getElementById(prefix + 'ddlHH" + pair.Key + "').selectedIndex].value;";
                        strRowGetDataJS += "   document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ':' + document.getElementById(prefix + 'ddlMM" + pair.Key + "').options[document.getElementById(prefix + 'ddlMM" + pair.Key + "').selectedIndex].value;";
                        strRowGetDataJS += "}";
                        //SS
                        strRowGetDataJS += "if (document.getElementById(prefix + 'ddlSS" + pair.Key + "') != null){ ";
                        strRowGetDataJS += "   document.getElementById(prefix + 'chkRowData" + i.ToString().PadLeft(2, '0') + "').value += ':' + document.getElementById(prefix + 'ddlSS" + pair.Key + "').options[document.getElementById(prefix + 'ddlSS" + pair.Key + "').selectedIndex].value;";
                        strRowGetDataJS += "}";
                        break;
                    default:
                        break;
                }
            }
        }

        //Header
        if (gvMain.HeaderRow != null)
        {
            //2016.07.28 新增
            gvMain.HeaderRow.Cells[2].HorizontalAlign = ucCmdBtnHorizontalAlign;

            //Group
            if (!string.IsNullOrEmpty(this.ucDataGroupKey))
            {
                int _ColSpanOffset = 0;
                if (!string.IsNullOrEmpty(ucHeaderColSpanQtyList)) { _ColSpanOffset = 1; }

                string strJS = "";
                strJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";

                strJS += "for (i = 1 + " + _ColSpanOffset + " ; i < oGrid.rows.length; i++) {";
                strJS += "  if (oGrid.rows[i].cells[0].getElementsByTagName('span')[0].innerText == '' ) {";
                strJS += "     oGrid.rows[i].style.display = 'none';";
                strJS += "  } else {";
                strJS += "     oGrid.rows[i].cells[0].getElementsByTagName('img')[0].src = '" + Util.Icon_Expand + "'";
                strJS += "  }";
                strJS += "}";
                Image imgCollapse = (Image)gvMain.HeaderRow.FindControl("imgGroupCollapse");
                imgCollapse.ToolTip = ucGroupCollapseAll;
                imgCollapse.Style.Add("cursor", "pointer");
                imgCollapse.Attributes.Add("onclick", strJS);
                strJS = "";
                strJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
                strJS += "for (i = 1 + " + _ColSpanOffset + " ; i < oGrid.rows.length; i++) {";
                strJS += "  if (oGrid.rows[i].cells[0].getElementsByTagName('span')[0].innerText == '' ) {";
                strJS += "     oGrid.rows[i].style.display = '';";
                strJS += "  } else {";
                strJS += "     oGrid.rows[i].cells[0].getElementsByTagName('img')[0].src = '" + Util.Icon_Collapse + "'";
                strJS += "  }";
                strJS += "}";
                Image imgExpand = (Image)gvMain.HeaderRow.FindControl("imgGroupExpand");
                imgExpand.ToolTip = ucGroupExpandAll;
                imgExpand.Style.Add("cursor", "pointer");
                imgExpand.Attributes.Add("onclick", strJS);
            }
            //CheckBox
            tmpChk = (CheckBox)gvMain.HeaderRow.FindControl("ChkAllRow");
            if (tmpChk != null)
            {
                //全選 CheckBox
                tmpChk.CausesValidation = this.ucCausesValidation;
                tmpChk.ToolTip = this.ucCheckToolTip;
                string strJS = "";
                string strGetAllRowDataJS = "";
                if (ucDataEditDefinition.Count > 0)
                {
                    //可供編輯
                    strJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
                    strJS += "for (i = 1; i < oGrid.rows.length; i++) {";
                    strJS += "  if (oGrid.rows[i].cells.length > 1){";
                    strJS += "     var oChk = oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0];";
                    strJS += "     if (oChk != null && oChk.type == 'checkbox') {";
                    strJS += "        var prefix = oChk.id.substring(0,oChk.id.lastIndexOf('_')+1);";
                    strJS += "        oChk.checked = this.checked;";
                    strJS += "        if (oChk.checked){";

                    if (ucUpdateAllEnabled)
                    {
                        strJS += "     document.getElementById('" + btnUpdateAll1.ClientID + "').style.display='';";
                        strJS += "     document.getElementById('" + btnUpdateAll2.ClientID + "').style.display='';";
                    }

                    if (ucDeleteAllEnabled)
                    {
                        strJS += "     document.getElementById('" + btnDeleteAll1.ClientID + "').style.display='';";
                        strJS += "     document.getElementById('" + btnDeleteAll2.ClientID + "').style.display='';";
                    }

                    strJS += strRowCheckedJS;
                    strJS += "        } else {";
                    strJS += "        document.getElementById('" + btnUpdateAll1.ClientID + "').style.display='none';";
                    strJS += "        document.getElementById('" + btnUpdateAll2.ClientID + "').style.display='none';";
                    strJS += "        document.getElementById('" + btnDeleteAll1.ClientID + "').style.display='none';";
                    strJS += "        document.getElementById('" + btnDeleteAll2.ClientID + "').style.display='none';";
                    strJS += strRowUnCheckedJS;
                    strJS += "       }";
                    strJS += "     }";
                    strJS += "  }";
                    strJS += "}";
                    tmpChk.Attributes.Add("onclick", strJS + ';' + strChk_ChkAllJS + ';' + strChk_UpdateAllJS);

                    strGetAllRowDataJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
                    strGetAllRowDataJS += "for (i = 1; i < oGrid.rows.length; i++) {";
                    strGetAllRowDataJS += "  if (oGrid.rows[i].cells.length > 1){";
                    strGetAllRowDataJS += "     var oChk = oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0];";
                    strGetAllRowDataJS += "     if (oChk != null && oChk.type == 'checkbox') {";
                    strGetAllRowDataJS += "       var prefix = oChk.id.substring(0,oChk.id.lastIndexOf('_')+1);";
                    strGetAllRowDataJS += "       if (oChk.checked){";
                    strGetAllRowDataJS += strRowGetDataJS;
                    strGetAllRowDataJS += "        }";
                    strGetAllRowDataJS += "     }";
                    strGetAllRowDataJS += "  }";
                    strGetAllRowDataJS += "}";

                    //根據是否需顯示確認訊息附掛Client事件
                    if (string.IsNullOrEmpty(ucUpdateAllConfirm))
                    {
                        btnUpdateAll1.OnClientClick = strGetAllRowDataJS;
                        btnUpdateAll2.OnClientClick = strGetAllRowDataJS;
                    }
                    else
                    {
                        btnUpdateAll1.OnClientClick = "if (confirm('" + ucUpdateAllConfirm + "')) {" + strGetAllRowDataJS + "}else{ return false;}";
                        btnUpdateAll2.OnClientClick = "if (confirm('" + ucUpdateAllConfirm + "')) {" + strGetAllRowDataJS + "}else{ return false;}";
                    }
                    //加入資料檢核JS 2015.12.14 / 2016.09.29 優化
                    btnUpdateAll1.OnClientClick = "if (Util_AlertPageNotValid('')){" + btnUpdateAll1.OnClientClick + "}else{ return false;}";
                    btnUpdateAll2.OnClientClick = "if (Util_AlertPageNotValid('')){" + btnUpdateAll2.OnClientClick + "}else{ return false;}";

                    if (string.IsNullOrEmpty(ucDeleteAllConfirm))
                    {
                        btnDeleteAll1.OnClientClick = strGetAllRowDataJS;
                        btnDeleteAll2.OnClientClick = strGetAllRowDataJS;
                    }
                    else
                    {
                        btnDeleteAll1.OnClientClick = "if (confirm('" + ucDeleteAllConfirm + "')) {" + strGetAllRowDataJS + "}else{ return false;}";
                        btnDeleteAll2.OnClientClick = "if (confirm('" + ucDeleteAllConfirm + "')) {" + strGetAllRowDataJS + "}else{ return false;}";
                    }

                }
                else
                {
                    //僅供選取
                    strJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
                    strJS += "for (i = 1; i < oGrid.rows.length; i++) {";
                    strJS += "  if (oGrid.rows[i].cells.length > 1){";
                    strJS += "     var oChk = oGrid.rows[i].cells[1].getElementsByTagName('INPUT')[0];";
                    strJS += "     if (oChk != null && oChk.type == 'checkbox') oChk.checked = this.checked;";
                    strJS += "  }";
                    strJS += "}";
                    tmpChk.Attributes.Add("onclick", strJS + ';' + strChk_ChkAllJS);
                }
            }

            //btnAdd
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnAdd");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgAdd");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucAddIcon;
                    tmpIcon.Visible = this.ucAddEnabled;
                }
                tmpBtn.CommandName = "cmdAdd";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucAddToolTip;
                tmpBtn.Visible = this.ucAddEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Add);
            }

            //btnDataDump 2016.11.08
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnDataDump");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgDataDump");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucDataDumpIcon;
                    tmpIcon.Visible = this.ucDataDumpEnabled;
                }
                tmpBtn.CommandName = "cmdDataDump";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucDataDumpToolTip;
                tmpBtn.Visible = this.ucDataDumpEnabled;
                //加入 ucAclEnabled 判斷
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);
            }

            //btnExportWord
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnExportWord");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgExportWord");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucExportWordIcon;
                    tmpIcon.Visible = this.ucExportWordEnabled;
                }
                tmpBtn.CommandName = "cmdExportWord";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucExportWordToolTip;
                tmpBtn.Visible = this.ucExportWordEnabled;
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);

                //警示訊息 2016.11.23 調整
                if (!string.IsNullOrEmpty(ucExportWordConfirm) || this.ucTotQty > this.ucExportWordMaxQty)
                {
                    string strConfirmMsg = (!string.IsNullOrEmpty(ucExportWordConfirm)) ? ucExportWordConfirm : string.Format(RS.Resources.GridView_ExportConfirm, this.ucExportWordMaxQty);
                    tmpBtn.OnClientClick = "if (confirm('" + strConfirmMsg + "')) { " + _defPopDisplayClientJS + " } else{ return false; }";
                }
                else
                {
                    tmpBtn.OnClientClick = _defPopDisplayClientJS;
                }

                //無資料時強制隱藏
                if (((DataTable)gvMain.DataSource).Rows.Count <= 0) tmpBtn.Visible = false;
            }

            //btnExport
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnExport");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgExport");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucExportIcon;
                    tmpIcon.Visible = this.ucExportEnabled;
                }
                tmpBtn.CommandName = "cmdExport";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucExportToolTip;
                tmpBtn.Visible = this.ucExportEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);

                //警示訊息 2016.11.23 調整
                if (!string.IsNullOrEmpty(ucExportConfirm) || this.ucTotQty > this.ucExportMaxQty)
                {
                    string strConfirmMsg = (!string.IsNullOrEmpty(ucExportConfirm)) ? ucExportConfirm : string.Format(RS.Resources.GridView_ExportConfirm, this.ucExportMaxQty);
                    tmpBtn.OnClientClick = "if (confirm('" + strConfirmMsg + "')) { " + _defPopDisplayClientJS + " } else{ return false; }";
                }
                else
                {
                    tmpBtn.OnClientClick = _defPopDisplayClientJS;
                }

                //無資料時強制隱藏
                if (((DataTable)gvMain.DataSource).Rows.Count <= 0) tmpBtn.Visible = false;
            }


            //btnExportOpenXml
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnExportOpenXml");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgExportOpenXml");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucExportOpenXmlIcon;
                    tmpIcon.Visible = this.ucExportOpenXmlEnabled;
                }
                tmpBtn.CommandName = "cmdExportOpenXml";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucExportOpenXmlToolTip;
                tmpBtn.Visible = this.ucExportOpenXmlEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);

                //警示訊息 2016.11.23 調整
                if (!string.IsNullOrEmpty(ucExportOpenXmlConfirm) || this.ucTotQty > this.ucExportOpenXmlMaxQty)
                {
                    string strConfirmMsg = (!string.IsNullOrEmpty(ucExportOpenXmlConfirm)) ? ucExportOpenXmlConfirm : string.Format(RS.Resources.GridView_ExportConfirm, this.ucExportOpenXmlMaxQty);
                    tmpBtn.OnClientClick = "if (confirm('" + strConfirmMsg + "')) { " + _defPopDisplayClientJS + " } else{ return false; }";
                }
                else
                {
                    tmpBtn.OnClientClick = _defPopDisplayClientJS;
                }

                //無資料時強制隱藏
                if (((DataTable)gvMain.DataSource).Rows.Count <= 0) tmpBtn.Visible = false;
            }

            //btnExportPdf
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnExportPdf");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgExportPdf");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucExportPdfIcon;
                    tmpIcon.Visible = this.ucExportPdfEnabled;
                }
                tmpBtn.CommandName = "cmdExportPdf";
                tmpBtn.CommandArgument = "";
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucExportPdfToolTip;
                tmpBtn.Visible = this.ucExportPdfEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);

                //警示訊息 2016.11.23 調整
                if (!string.IsNullOrEmpty(ucExportPdfConfirm) || this.ucTotQty > this.ucExportPdfMaxQty)
                {
                    string strConfirmMsg = (!string.IsNullOrEmpty(ucExportPdfConfirm)) ? ucExportPdfConfirm : string.Format(RS.Resources.GridView_ExportConfirm, this.ucExportPdfMaxQty);
                    tmpBtn.OnClientClick = "if (confirm('" + strConfirmMsg + "')) { " + _defPopDisplayClientJS + " } else{ return false; }";
                }
                else
                {
                    tmpBtn.OnClientClick = _defPopDisplayClientJS;
                }

                //無資料時強制隱藏
                if (((DataTable)gvMain.DataSource).Rows.Count <= 0) tmpBtn.Visible = false;
            }

            //btnPrint
            //純Client端按鈕，不觸發Server事件
            tmpBtn = (LinkButton)gvMain.HeaderRow.FindControl("btnPrint");
            if (tmpBtn != null && string.IsNullOrEmpty(tmpBtn.CommandName))
            {
                tmpIcon = (Image)gvMain.HeaderRow.FindControl("imgPrint");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucPrintIcon;
                    tmpIcon.Visible = this.ucPrintEnabled;
                }
                tmpBtn.OnClientClick = "javascript:Util_PrintBlock('" + divGridview.ClientID + "');return false;";
                tmpBtn.ToolTip = this.ucPrintToolTip;
                tmpBtn.Visible = this.ucPrintEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Print);

                //無資料時強制隱藏
                if (((DataTable)gvMain.DataSource).Rows.Count <= 0) tmpBtn.Visible = false;
            }

        }

        //DataRow
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count >= ucDataDisplayDefinition.Count)
        {
            //2016.07.28 新增
            e.Row.Cells[2].HorizontalAlign = ucCmdBtnHorizontalAlign;

            if (!string.IsNullOrEmpty(this.ucDataGroupKey))
            {
                //處理群組抬頭、結尾列(GroupHeader、GroupFooter)
                strGroupValue = (e.Row.DataItem as DataRowView)[ucDataGroupKey].ToString();

                switch (strGroupValue.Left(3))
                {
                    case "GH_":
                        //GroupHeader
                        strGroupHeader = string.Format(ucGroupHeaderFormat, strGroupValue.Substring(3));
                        //群組抬頭列只留第一個Cell，其他作隱藏處理
                        for (int j = 1; j < gvMain.Columns.Count; j++)
                        {
                            e.Row.Cells[j].Visible = false;
                        }

                        //群組抬頭列自動跨欄
                        e.Row.CssClass = ucGroupHeaderCssClass;
                        e.Row.Cells[0].ColumnSpan = gvMain.Columns.Count;
                        e.Row.Cells[0].Attributes.Add("title", string.Format(RS.Resources.GridView_GroupHeader, strGroupHeader));

                        tmpLabel = (Label)e.Row.Cells[0].FindControl("labGroup");
                        tmpLabel.Text = string.Format("<image src='{0}' border='0' /> {1}", ucDefGroupExpandAll ? Util.Icon_Collapse : Util.Icon_Expand, strGroupHeader);
                        tmpLabel.Style.Add("display", "");

                        int _ColSpanOffset = 0;
                        if (!string.IsNullOrEmpty(ucHeaderColSpanQtyList)) { _ColSpanOffset = 1; }

                        string strJS = "";
                        strJS += "var oGrid = document.getElementById('" + gvMain.ClientID + "');";
                        //strJS += "alert(oGrid.rows[" + (e.Row.RowIndex + 1) + "].cells[0].innerHTML);";
                        //strJS += "alert(oGrid.rows[" + (e.Row.RowIndex + 1) + "].cells[0].getElementsByTagName('img')[0].src);";
                        //群組抬頭JS
                        strJS += "if (oGrid.rows[" + (e.Row.RowIndex + 2 + _ColSpanOffset) + "].style.display == '') {";
                        strJS += "    oGrid.rows[" + (e.Row.RowIndex + 1 + _ColSpanOffset) + "].cells[0].getElementsByTagName('img')[0].src = '" + Util.Icon_Expand + "'";
                        strJS += "} else { ";
                        strJS += "    oGrid.rows[" + (e.Row.RowIndex + 1 + _ColSpanOffset) + "].cells[0].getElementsByTagName('img')[0].src = '" + Util.Icon_Collapse + "'";
                        strJS += "}";
                        //群組成員JS
                        strJS += "for (i = " + (e.Row.RowIndex + 2 + _ColSpanOffset) + "; i < oGrid.rows.length; i++) {";
                        //strJS += "  alert(oGrid.rows[i].cells.length);";
                        //strJS += "  alert(oGrid.rows[i].cells[0].firstChild.innerText);";
                        //strJS += "  alert('span=[' + oGrid.rows[i].cells[0].getElementsByTagName('span')[0].innerText + ']');";
                        //利用非群組列時，其 Span 的值固定為空白，判斷該列是否需處理收合
                        strJS += "  if (oGrid.rows[i].cells[0].getElementsByTagName('span')[0].innerText == '' ) {";
                        strJS += "     oGrid.rows[i].style.display = (oGrid.rows[i].style.display == '')?'none':'';";
                        strJS += "  } else {";
                        strJS += "     break; ";
                        strJS += "  }";
                        strJS += "}";
                        e.Row.Attributes.Add("onclick", strJS);
                        _GroupOffset += 1; //偏移值 +1 ，方便計算顯示序號

                        break;
                    case "GF_":
                        //GroupFooter
                        //CSS　樣式
                        e.Row.Style["background-color"] = "Silver";
                        if (ucDisplayOnly)
                        {
                            e.Row.Cells[1].Style["display"] = "none";
                            e.Row.Cells[2].Style["display"] = "none";
                        }
                        e.Row.Cells[3].Visible = this.ucSeqNoEnabled;

                        //清空不需顯示內容的欄位
                        e.Row.Cells[2].Text = "";
                        e.Row.Cells[3].Text = "";
                        //清空ucDataGroupKey所在欄位
                        intPos = Array.IndexOf(Util.getArray(ucDataDisplayDefinition), ucDataGroupKey) + 4;
                        e.Row.Cells[intPos].Text = "";

                        //處理加總值前方的 Caption
                        intPos = 99;
                        intTmpPos = -1;
                        for (int i = 0; i < ucDataGroupSubtotalList.Count(); i++)
                        {
                            intTmpPos = Array.IndexOf(Util.getArray(ucDataDisplayDefinition), ucDataGroupSubtotalList[i]) + 3;
                            if (intPos > intTmpPos) intPos = intTmpPos;
                        }

                        if (intPos >= 0)
                        {
                            e.Row.Cells[intPos].HorizontalAlign = HorizontalAlign.Right;
                            e.Row.Cells[intPos].Text = ucGroupSubtotalToolTip;
                        }
                        _GroupOffset += 1; //偏移值 +1 ，方便計算顯示序號

                        break;
                    default:
                        if (!ucDefGroupExpandAll)
                        {
                            e.Row.Style["display"] = "none";
                        }
                        break;
                }

            }

            //流水號
            tmpLabel = (Label)e.Row.FindControl("labRowSeqNo");
            if (tmpLabel != null)
                tmpLabel.Text = ((this.ucPageSize * (this.ucPageNo - 1)) + e.Row.DataItemIndex + 1 - _GroupOffset).ToString();

            //取得資料鍵值
            string[] DataKeyValues = new string[gvMain.DataKeyNames.Count()];
            for (int i = 0; i < gvMain.DataKeyNames.Count(); i++)
            {
                DataKeyValues[i] = (e.Row.DataItem as DataRowView)[gvMain.DataKeyNames[i]].ToString();
            }

            //處理線上編輯的控制項 2016.09.26
            if (ucDataEditDefinition.Count > 0)
            {
                int i = 0;
                Label oLabel;
                Util_ucCheckBoxList oChkList;
                Util_ucRadioList oRadList;
                foreach (var pair in ucDataEditDefinition)
                {
                    i++;
                    strRowCheckedJS += " document.getElementById(prefix + 'lab" + pair.Key + "').style.display='none';";
                    strRowUnCheckedJS += " document.getElementById(prefix + 'lab" + pair.Key + "').style.display='';";
                    switch (pair.Value.Split('@')[0].ToUpper())
                    {
                        case "CHECKBOXLIST": //CheckBoxList
                            string strChkIDList = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][pair.Key].ToString();
                            oLabel = (Label)e.Row.FindControl("lab" + pair.Key);
                            oChkList = (Util_ucCheckBoxList)e.Row.FindControl("chk" + pair.Key);
                            if (oChkList != null)
                            {
                                if (oChkList.ucSourceDictionary.ContainsKey("JSON"))
                                {
                                    string strJSON = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][oChkList.ucSourceDictionary["JSON"]].ToString();
                                    if (!string.IsNullOrEmpty(strJSON))
                                    {
                                        oChkList.ucSourceDictionary = Util.getDictionary(strJSON);
                                        if (oLabel != null)
                                        {
                                            oChkList.ucSelectedIDList = strChkIDList;
                                            string[] arIDList = strChkIDList.Split(',');
                                            string strInfoList = "";
                                            for (int j = 0; j < arIDList.Count(); j++)
                                            {
                                                //已選取項目必需在候選清單內
                                                if (oChkList.ucSourceDictionary.ContainsKey(arIDList[j]))
                                                {
                                                    if (!string.IsNullOrEmpty(strInfoList))
                                                    {
                                                        strInfoList += ",";
                                                    }
                                                    strInfoList += oChkList.ucSourceDictionary[arIDList[j]];
                                                }
                                            }
                                            oLabel.Text = strInfoList;
                                        }
                                    }
                                }
                                oChkList.Refresh();
                            }
                            break;
                        case "RADIOLIST":    //RadioList
                            string strRadID = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][pair.Key].ToString();
                            oLabel = (Label)e.Row.FindControl("lab" + pair.Key);
                            oRadList = (Util_ucRadioList)e.Row.FindControl("rad" + pair.Key);
                            if (oRadList != null)
                            {
                                if (oRadList.ucSourceDictionary.ContainsKey("JSON"))
                                {
                                    string strJSON = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][oRadList.ucSourceDictionary["JSON"]].ToString();
                                    if (!string.IsNullOrEmpty(strJSON))
                                    {
                                        oRadList.ucSourceDictionary = Util.getDictionary(strJSON);
                                        if (oLabel != null)
                                        {
                                            oRadList.ucSelectedID = strRadID;
                                            //已選取項目必需在候選清單內
                                            if (oRadList.ucSourceDictionary.ContainsKey(strRadID))
                                            {
                                                oLabel.Text = oRadList.ucSourceDictionary[strRadID];
                                            }
                                        }
                                    }
                                }
                                oRadList.Refresh();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }


            //chkRow
            tmpChk = (CheckBox)e.Row.FindControl("chkRow");
            tmpHid = (HiddenField)e.Row.FindControl("chkRowKey");

            if (tmpChk != null && tmpHid != null)
            {
                tmpHid.Value = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpChk.CausesValidation = this.ucCausesValidation;
                tmpChk.ToolTip = this.ucCheckToolTip;
                tmpChk.Visible = this.ucCheckEnabled;

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpChk.Visible && !string.IsNullOrEmpty(this.ucCheckEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucCheckEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpChk.Visible = true;
                    else
                        tmpChk.Visible = false;
                }

                //如果會顯示 CheckBox
                if (tmpChk.Visible)
                {
                    if (ucDataEditDefinition.Count > 0)
                    {
                        //可線上編輯
                        string strJS = "var prefix = this.id.substring(0,this.id.lastIndexOf('_')+1);";
                        strJS += "if (this.checked){";
                        strJS += strRowCheckedJS;
                        strJS += "} else {";
                        strJS += strRowUnCheckedJS;
                        strJS += "}";
                        tmpChk.Attributes.Add("onclick", strJS + ';' + strChk_ChkAllJS + ';' + strChk_UpdateAllJS);

                        //設定需要用到的 chkRowData[XX] 欄位 2016.11.16 調整 Visible 為預設 false ，要用到才 true
                        for (int i = 1; i <= ucDataEditDefinition.Count; i++)
                        {
                            tmpHid = (HiddenField)e.Row.FindControl("chkRowData" + i.ToString().PadLeft(2, '0'));
                            if (tmpHid != null)
                            {
                                tmpHid.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        //僅供選取
                        tmpChk.Attributes.Add("onclick", strChk_ChkAllJS);
                    }
                }
            }

            //btnCopy
            tmpBtn = (LinkButton)e.Row.FindControl("btnCopy");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgCopy");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucCopyIcon;
                    tmpIcon.Visible = this.ucCopyEnabled;
                }
                tmpBtn.CommandName = "cmdCopy";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucCopyToolTip;
                tmpBtn.Visible = this.ucCopyEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Copy);

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucCopyEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucCopyEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnDelete
            tmpBtn = (LinkButton)e.Row.FindControl("btnDelete");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgDelete");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucDeleteIcon;
                    tmpIcon.Visible = this.ucDeleteEnabled;
                }
                tmpBtn.CommandName = "cmdDelete";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucDeleteToolTip;
                tmpBtn.Visible = this.ucDeleteEnabled;
                Util.ConfirmBox(tmpBtn, this.ucDeleteConfirm);
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Delete);


                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucDeleteEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucDeleteEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnDownload
            tmpBtn = (LinkButton)e.Row.FindControl("btnDownload");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgDownload");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucDownloadIcon;
                    tmpIcon.Visible = this.ucDownloadEnabled;
                }
                tmpBtn.CommandName = "cmdDownload";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucDownloadToolTip;
                tmpBtn.Visible = this.ucDownloadEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Download);

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucDownloadEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucDownloadEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnEdit
            tmpBtn = (LinkButton)e.Row.FindControl("btnEdit");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgEdit");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucEditIcon;
                    tmpIcon.Visible = this.ucEditEnabled;
                }
                tmpBtn.CommandName = "cmdEdit";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucEditToolTip;
                tmpBtn.Visible = this.ucEditEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Edit);

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucEditEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucEditEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnInformation
            tmpBtn = (LinkButton)e.Row.FindControl("btnInformation");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgInformation");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucInformationIcon;
                    tmpIcon.Visible = this.ucInformationEnabled;
                }
                tmpBtn.CommandName = "cmdInformation";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucInformationToolTip;
                tmpBtn.Visible = this.ucInformationEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Information);

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucInformationEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucInformationEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnMultilingual
            tmpBtn = (LinkButton)e.Row.FindControl("btnMultilingual");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgMultilingual");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucMultilingualIcon;
                    tmpIcon.Visible = this.ucMultilingualEnabled;
                }
                tmpBtn.CommandName = "cmdMultilingual";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucMultilingualToolTip;
                tmpBtn.Visible = this.ucMultilingualEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Multilingual);

                // 從資料列判斷是否顯示 2014.12.26
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucMultilingualEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucMultilingualEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }
            }

            //btnSelect
            tmpBtn = (LinkButton)e.Row.FindControl("btnSelect");
            if (tmpBtn != null)
            {
                tmpIcon = (Image)e.Row.FindControl("imgSelect");
                if (tmpIcon != null)
                {
                    tmpIcon.ImageUrl = this.ucSelectIcon;
                    tmpIcon.Visible = this.ucSelectEnabled;
                }
                tmpBtn.CommandName = "cmdSelect";
                tmpBtn.CommandArgument = Util.getStringJoin(DataKeyValues, ucDataKeyDelimiter);
                tmpBtn.CausesValidation = this.ucCausesValidation;
                tmpBtn.ToolTip = this.ucSelectToolTip;
                tmpBtn.Visible = this.ucSelectEnabled;
                //加入 ucAclEnabled 判斷 2016.05.12
                if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Query);

                // 從資料列判斷是否顯示 2014.09.16
                if (tmpBtn.Visible && !string.IsNullOrEmpty(this.ucSelectEnabledDataColName))
                {
                    if (((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][this.ucSelectEnabledDataColName].ToString().ToUpper() == "Y")
                        tmpBtn.Visible = true;
                    else
                        tmpBtn.Visible = false;
                }

                //增加處理 ucSelectRowEnabled  2016.08.05 新增
                if (ucSelectRowEnabled)
                {
                    if (!tmpBtn.Visible && !ucAclEnabled)
                    {
                        //若 ucSelectEnabled 禁用與 Acl 無關
                        tmpBtn.Visible = true;
                        if (tmpIcon != null)
                        {
                            tmpIcon.Visible = false;
                        }
                    }

                    if (tmpBtn.Visible)
                    {
                        e.Row.Attributes.Add("onclick", "javascript:__doPostBack('" + tmpBtn.UniqueID + "','');");
                        e.Row.Style.Add("cursor", "pointer");
                    }
                }

            }




            //處理ToopTip
            if (ucDataDisplayToolTipDefinition.Count > 0)
            {
                intPos = -1;
                foreach (var pair in this.ucDataDisplayToolTipDefinition)
                {
                    //因為使用範本方式產生欄位內容，intPos 無法用 Util.getGridviewColumnIndexByDataField 取得
                    intPos = Array.IndexOf(Util.getArray(ucDataDisplayDefinition), pair.Key) + 4;
                    if (intPos > 0)
                    {
                        HoverTooltip1.AddTooltipControl(e.Row.Cells[intPos]);
                        if (pair.Value.Split(',').Count() > 1)
                        {
                            e.Row.Cells[intPos].Attributes["tooltiptitle"] = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][pair.Value.Split(',')[0]].ToString();
                            e.Row.Cells[intPos].Attributes["title"] = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][pair.Value.Split(',')[1]].ToString();
                        }
                        else
                        {
                            e.Row.Cells[intPos].Attributes["tooltiptitle"] = ucDefaultTooltipTitle;
                            e.Row.Cells[intPos].Attributes["title"] = ((DataTable)gvMain.DataSource).Rows[e.Row.RowIndex][pair.Value].ToString();
                        }
                    }

                }
            }

            //處理SubTotal
            if (gvMain.ShowFooter)
            {
                tmpRow = ((DataTable)((GridView)sender).DataSource).Rows[e.Row.RowIndex];
                string tmpTotal;
                bool IsGroupRow = false;
                if (!string.IsNullOrEmpty(this.ucDataGroupKey) && ucDataGroupSubtotalList.Count() > 0)
                {
                    //strGroupValue = ((DataTable)((GridView)sender).DataSource).Rows[e.Row.DataItemIndex][ucDataGroupKey].ToString();
                    strGroupValue = (e.Row.DataItem as DataRowView)[ucDataGroupKey].ToString();
                    //GroupHeader
                    if (strGroupValue.StartsWith("GH_")) IsGroupRow = true;
                    //GroupFooter
                    if (strGroupValue.StartsWith("GF_")) IsGroupRow = true;
                }

                //非群組列才可進行加總
                if (!IsGroupRow)
                {
                    for (int i = 0; i < ucDataSubtotalList.Count(); i++)
                    {
                        tmpTotal = tmpRow[ucDataSubtotalList[i]].ToString();
                        if (!string.IsNullOrEmpty(tmpTotal))
                            _SubtotalList[i] += decimal.Parse(tmpTotal);
                    }
                }
            }

        }

        //Footer
        if (gvMain.ShowFooter && e.Row.RowType == DataControlRowType.Footer)
        {
            intPos = -1;
            string strFormat = "";
            //處理加總值
            for (int i = 0; i < ucDataSubtotalList.Count(); i++)
            {
                intPos = Array.IndexOf(Util.getArray(ucDataDisplayDefinition), ucDataSubtotalList[i]) + 4;

                if (intPos > 0)
                {
                    strFormat = ucDataDisplayDefinition[ucDataSubtotalList[i]].Split('@')[1];
                    e.Row.Cells[intPos].CssClass = "Util_WordBreak";
                    e.Row.Cells[intPos].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[intPos].Width = gvMain.Rows[0].Cells[intPos].Width;
                    if (strFormat.Length <= 2)
                        e.Row.Cells[intPos].Text = string.Format("{0:" + strFormat + "}", _SubtotalList[i]); //只取[Nx]
                    else
                        e.Row.Cells[intPos].Text = string.Format("{0:" + strFormat.Substring(0, 2) + "}", _SubtotalList[i]); //只取[Nx]
                }
            }
            //處理加總值前方的 Caption
            intPos = 99;
            intTmpPos = -1;
            for (int i = 0; i < ucDataSubtotalList.Count(); i++)
            {
                intTmpPos = Array.IndexOf(Util.getArray(ucDataDisplayDefinition), ucDataSubtotalList[i]) + 3;
                if (intPos > intTmpPos) intPos = intTmpPos;
            }

            if (intPos >= 0)
            {
                e.Row.Cells[intPos].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[intPos].Text = ucSubtotalToolTip;
            }
        }
    }

    /// <summary>
    /// 下拉選單換頁事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPageSize1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ucPageSize != int.Parse(ddlPageSize1.SelectedValue))
        {
            this.ucPageNo = this.ucDefPageNo;
            AspNetPager1.CurrentPageIndex = this.ucPageNo;
            AspNetPager2.CurrentPageIndex = this.ucPageNo;

            this.ucPageSize = int.Parse(ddlPageSize1.SelectedValue);
            ddlPageSize2.SelectedValue = ddlPageSize1.SelectedValue;

            Refresh(true);
        }
    }

    protected void ddlPageSize2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ucPageSize != int.Parse(ddlPageSize2.SelectedValue))
        {
            this.ucPageNo = this.ucDefPageNo;
            AspNetPager2.CurrentPageIndex = this.ucPageNo;
            AspNetPager1.CurrentPageIndex = this.ucPageNo;

            this.ucPageSize = int.Parse(ddlPageSize2.SelectedValue);
            ddlPageSize1.SelectedValue = ddlPageSize2.SelectedValue;

            Refresh(true);
        }
    }

    /// <summary>
    /// AspNetPage換頁事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        this.ucPageNo = AspNetPager1.CurrentPageIndex;
        AspNetPager2.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
        Refresh();
    }

    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        this.ucPageNo = AspNetPager2.CurrentPageIndex;
        AspNetPager1.CurrentPageIndex = AspNetPager2.CurrentPageIndex;
        Refresh();
    }

    /// <summary>
    /// 匯出按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        //產生匯出資料
        string strCommandName = ((LinkButton)sender).CommandName;
        string strExportFileName = "";
        byte[] objExportBytes = null;
        DataTable dtExport = null;
        switch (strCommandName)
        {
            case "cmdExport":
                strExportFileName = this.ucExportName;
                dtExport = getExportData("XLS");
                if (dtExport != null && dtExport.Rows.Count > 0)
                {
                    GridView gv = new GridView();
                    gv.DataSource = dtExport;
                    gv.DataBind();
                    Page oPage = new Page();
                    HtmlForm oForm = new HtmlForm();
                    oPage.Controls.Add(oForm);
                    oForm.Controls.Add(gv);
                    string strForm = Util.getHtmlContent(oForm);
                    //確定有資料才輸出
                    if (strForm.IndexOf("<table") > 0)
                    {
                        strForm = strForm.Substring(strForm.IndexOf("<table")).Replace("</div></form>", "");
                        objExportBytes = Util.getBytes(strForm);
                    }
                }
                break;
            case "cmdExportWord":
                strExportFileName = this.ucExportWordName;
                dtExport = getExportData("DOC");
                if (dtExport != null && dtExport.Rows.Count > 0)
                {
                    GridView gv = new GridView();
                    gv.DataSource = dtExport;
                    gv.DataBind();
                    Page oPage = new Page();
                    HtmlForm oForm = new HtmlForm();
                    oPage.Controls.Add(oForm);
                    oForm.Controls.Add(gv);
                    string strForm = Util.getHtmlContent(oForm);
                    //確定有資料才輸出
                    if (strForm.IndexOf("<table") > 0)
                    {
                        strForm = strForm.Substring(strForm.IndexOf("<table")).Replace("</div></form>", "");
                        objExportBytes = Util.getBytes(strForm);
                    }
                }
                break;
            case "cmdExportOpenXml":
                strExportFileName = this.ucExportOpenXmlName;
                //2017.02.16 加入 ucExportOpenXmlFormatByDataDisplayDefinition 及 ucDataDisplayDefinition 判斷
                //2017.03.21 加入 ucExportOpenXmlHeader, ucExportOpenXmlFooter 
                if (!ucExportOpenXmlFormatByDataDisplayDefinition || ucDataDisplayDefinition.IsNullOrEmpty())
                {
                    objExportBytes = Util.getBytes(Util.getExcelOpenXml(getExportData("XLSX", true), "Sheet1", ucExportOpenXmlPassword, null, ucExportOpenXmlHeader, ucExportOpenXmlFooter));
                }
                else
                {
                    if (this.ucSeqNoEnabled)
                    {
                        Dictionary<string, string> dicTmp = new Dictionary<string, string>();
                        dicTmp.AddRange(ucDataDisplayDefinition);
                        dicTmp.TryAdd("RowNo", this.ucSeqNoCaption + "@N0"); //序號欄位
                        objExportBytes = Util.getBytes(Util.getExcelOpenXml(getExportData("XLSX", false), "Sheet1", ucExportOpenXmlPassword, dicTmp, ucExportOpenXmlHeader, ucExportOpenXmlFooter));
                    }
                    else
                        objExportBytes = Util.getBytes(Util.getExcelOpenXml(getExportData("XLSX", false), "Sheet1", ucExportOpenXmlPassword, ucDataDisplayDefinition, ucExportOpenXmlHeader, ucExportOpenXmlFooter));
                }
                break;
            case "cmdExportPdf":
                strExportFileName = this.ucExportPdfName;
                dtExport = getExportData("PDF");
                if (!string.IsNullOrEmpty(ucExportPdfTitle))
                    dtExport.TableName = ucExportPdfTitle;
                if (dtExport != null && dtExport.Rows.Count > 0)
                {
                    System.IO.MemoryStream oStream = PDFHelper.getPDFStream(dtExport);
                    oStream = PDFHelper.getPDFwithWaterMark(oStream.ToArray(), this.ucExportPdfWaterMark, this.ucExportPdfWaterMarkRotation, this.ucExportPdfWaterMarkTextSize);
                    oStream = PDFHelper.getPDFwithEncrypt(oStream.ToArray(), this.ucExportPdfAllowPrint, this.ucExportPdfAllowCopy);
                    objExportBytes = oStream.ToArray();
                }
                break;
            default:
                break;
        }

        if (objExportBytes != null && objExportBytes.Length > 0)
        {
            if (FileInfoObj.setFileInfoObj(strExportFileName, objExportBytes, true))
            {
                if (FileInfoObj.DirectDownload())
                {
                    //處理正常，傳回事件
                    if (GridViewCommand != null)
                    {
                        GridViewEventArgs eArgs = new GridViewEventArgs();
                        eArgs.CommandName = strCommandName;
                        eArgs.DataTable = null;
                        GridViewCommand(this, eArgs);
                    }
                }
                else
                {
                    //處理錯誤
                    Util.NotifyMsg(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error);
                }
            }
            else
            {
                //處理失敗
                Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error);
            }
        }
        else
        {
            //匯出資料失敗
            Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error);
        }

        Util.setJSContent(_defPopCloseClientJS, this.ClientID + "_PopClose");
        Refresh();
    }

    /// <summary>
    /// linkButton 公用函數
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLink_Click(object sender, EventArgs e)
    {
        LinkButton oBtn = (LinkButton)sender;
        RowCommandEventArgs eArgs = new RowCommandEventArgs();
        eArgs.CommandName = oBtn.CommandName;
        eArgs.DataKeys = (string.IsNullOrEmpty(oBtn.CommandArgument)) ? null : oBtn.CommandArgument.ToString().Split(ucDataKeyDelimiter); //改成可自訂分隔字符 2017.01.24

        if (RowCommand != null)
        {
            RowCommand(this, eArgs);
        }

        //2016.11.14 改用統一作法
        aftCmdRefresh(oBtn.CommandName);
    }
    /// <summary>
    /// 將GridView中可編輯資料列的欄位轉成DataTable傳回
    /// </summary>
    /// <returns></returns>
    protected DataTable getEditResultData()
    {
        //產生 DataTable Schema
        DataTable dtEditResult = new DataTable();
        dtEditResult.Columns.Add("Key"); //存放資料鍵值
        foreach (var pair in ucDataEditDefinition)
        {
            dtEditResult.Columns.Add(pair.Key);
        }
        //將資料塞到 DataTable
        foreach (GridViewRow row in gvMain.Rows)
        {
            CheckBox oChk = (CheckBox)row.FindControl("chkRow");
            HiddenField oKey = (HiddenField)row.FindControl("chkRowKey");
            if (oChk != null && oKey != null)
            {
                if (oChk.Checked == true)
                {
                    DataRow drNew = dtEditResult.NewRow();
                    drNew["Key"] = oKey.Value;
                    int i = 0;
                    foreach (var pair in ucDataEditDefinition)
                    {
                        i++;
                        drNew[pair.Key] = ((HiddenField)row.FindControl("chkRowData" + i.ToString().PadLeft(2, '0'))).Value;
                    }
                    dtEditResult.Rows.Add(drNew);
                }
            }
        }
        return dtEditResult;
    }

    /// <summary>
    /// 全部更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        DataTable dt = getEditResultData();
        GridViewEventArgs eArgs = new GridViewEventArgs();
        eArgs.CommandName = "cmdUpdateAll";
        eArgs.DataTable = dt;
        if (GridViewCommand != null)
        {
            GridViewCommand(this, eArgs);
        }

        //2016.11.14 改用統一作法
        aftCmdRefresh(eArgs.CommandName);
    }

    /// <summary>
    /// 全部刪除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        DataTable dt = getEditResultData();
        GridViewEventArgs eArgs = new GridViewEventArgs();
        eArgs.CommandName = "cmdDeleteAll";
        eArgs.DataTable = dt;
        if (GridViewCommand != null)
        {
            GridViewCommand(this, eArgs);
        }

        //2016.11.14 改用統一作法
        aftCmdRefresh(eArgs.CommandName);
    }

    /// <summary>
    /// 變更排序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSortHidden_Click(object sender, EventArgs e)
    {
        //隱藏式按鈕
        string strSortExpression = Util.getRequestFormKey("__EVENTARGUMENT");
        if (!string.IsNullOrEmpty(strSortExpression))
        {
            if (this.ucSortExpression != strSortExpression)
            {
                //若有換排序欄位，則固定為升冪
                this.ucSortExpression = strSortExpression;
                this.ucSortDirection = "ASC";
            }
            else
            {
                //若為現有排序欄位，則判斷升降冪
                if (this.ucSortDirection.ToUpper() == "ASC")
                {
                    this.ucSortDirection = "DESC";
                }
                else
                {
                    this.ucSortDirection = "ASC";
                }
            }
            //變更排序後需Refresh
            Refresh();
        }
    }

    /// <summary>
    /// 發生命令事件後的重新整理
    /// </summary>
    /// <param name="strCmdName"></param>
    protected void aftCmdRefresh(string strCmdName)
    {
        //2016.11.14 新增
        if (ucIsAutoRefresh && !string.IsNullOrEmpty(strCmdName))
        {
            Util_ucGridView oGridView;
            oGridView = (Util_ucGridView)this;
            if (_RowQtyChgCmdNameList.Contains(strCmdName))
            {
                oGridView.Refresh(true); //需初始
            }
            else
            {
                oGridView.Refresh(false); //不需初始
            }

            //處理需同步重新整理的ucGridView
            if (!string.IsNullOrEmpty(ucSyncRefreshGridViewIDList[0]))
            {
                for (int i = 0; i < ucSyncRefreshGridViewIDList.Count(); i++)
                {
                    oGridView = (Util_ucGridView)Util.FindControlEx(Page, ucSyncRefreshGridViewIDList[i]);
                    if (oGridView != null)
                    {
                        if (_RowQtyChgCmdNameList.Contains(strCmdName))
                        {
                            oGridView.Refresh(true); //需初始
                        }
                        else
                        {
                            oGridView.Refresh(false); //不需初始
                        }
                    }
                }
            }
        }
    }
}

/// <summary>
/// 新增 GridView 範本欄位
/// </summary>
public partial class AddGridViewTemplete : Page, ITemplate
{
    private DataControlRowType _RowType;            //資料列型別(Header/DataRow)
    private string _ColName;                        //欄位名稱
    private bool _AllowSortEdit = false;            //是否允許排序(Header)或編輯(DataRow)
    private string _BtnSortPostBackClientID;        //若可排序，其發動按鈕 doPostBack 用的ClientID
    private string _HeaderText;                     //欄位顯示抬頭
    private string _DisplayFormat;                  //欄位顯示格式，例：日期@D15,金額@N2,文字@R40  除Nx是小數位之外，其他樣式後面的數字是顯示寬度(px)
    private string _EditType;                                //若GridView允許編輯，其編輯使用的物件型別(TextBox/DropDownList/DropDownFirst/Calendar)
    private Dictionary<string, string> _EditDataDictionary;　//若GridView允許編輯，使用物件需要的參數資料來源

    /// <summary>
    /// 新增 GridView 範本欄位
    /// </summary>
    /// <param name="type"></param>
    /// <param name="colname"></param>
    /// <param name="allowSort_or_Edit"></param>
    /// <param name="btnSortPostBackClientID"></param>
    /// <param name="header"></param>
    /// <param name="dispFormat"></param>
    /// <param name="editType"></param>
    /// <param name="editDataSource"></param>
    public AddGridViewTemplete(DataControlRowType type, string colname, bool allowSort_or_Edit = false, string btnSortPostBackClientID = "", string header = "", string dispFormat = "", string editType = "TextBox", Dictionary<string, string> editDataSource = null)
    {
        _RowType = type;
        _ColName = colname;
        _AllowSortEdit = allowSort_or_Edit;
        _BtnSortPostBackClientID = btnSortPostBackClientID;
        _HeaderText = header;
        _DisplayFormat = dispFormat;
        _EditType = editType;
        _EditDataDictionary = editDataSource;
    }

    /// <summary>
    /// 將範本實例化
    /// </summary>
    /// <param name="container"></param>
    public void InstantiateIn(System.Web.UI.Control container)
    {
        switch (_RowType)
        {
            case DataControlRowType.Header:  // GridView表頭
                if (_AllowSortEdit)
                {
                    //可排序 
                    //假借傳遞 [btnSortHidden] 按鈕事件來達到目的 2015.12.17
                    HyperLink lnkHeader = new HyperLink();
                    lnkHeader.Text = _HeaderText;
                    lnkHeader.CssClass = _DisplayFormat;

                    lnkHeader.NavigateUrl = "javascript:__doPostBack('" + _BtnSortPostBackClientID + "','" + _ColName + "');";  // ucGridView$gvMain$ctlxx$btnSortHidden
                    container.Controls.Add(lnkHeader);
                }
                else
                {
                    //只顯示
                    Label labHeader = new Label();
                    labHeader.Text = _HeaderText;
                    labHeader.CssClass = _DisplayFormat;
                    container.Controls.Add(labHeader);
                }
                break;
            case DataControlRowType.DataRow:  // Gridview資料列
                Panel pnl1 = new Panel();
                //固定產生Label物件供顯示用
                Label lab1 = new Label();
                lab1.ID = "lab" + _ColName;

                //處理顯示格式
                //文字資料　，格式[欄位名稱]　[抬頭[@[x]yyy]]] 　x為(L/C/R)、yyy為指定寬度(整數為像素，小數為百分比)，例: 'POID'     , '訂單號碼@C80'
                //日期資料　，格式[欄位名稱]　[抬頭[@[x]yyy]]] 　x為(D/T/S)、yyy為指定寬度(整數為像素，小數為百分比)，例: 'PODate'   , '下單日期@D0.15'
                //數值資料　，格式[欄位名稱]　[抬頭[@N[x]yyy]]]　x為小數位 、yyy為指定寬度(整數為像素，小數為百分比)，例: 'TotAmt'   , '訂單總額@N1'
                //百分比資料，格式[欄位名稱]　[抬頭[@P[x]yyy]]]　x為小數位 、yyy為指定寬度(整數為像素，小數為百分比)，例: 'TotPect'  , '佔單比例@P1'
                //圖片資料　，格式[欄位名稱]　[抬頭[@I[xxx[,yyy[,zzz]]]]]    xxx為指定寬度(整數為像素，小數為百分比)、yyy為URL欄位、zzz為Target欄位，例: 'ProdGraph', '產品圖片@I96,SysUrl,SysTarget'
                //超連結資料，格式[欄位名稱]　[抬頭[@A[xxx[,yyy[,zzz]]]]]    xxx為指定寬度(整數為像素，小數為百分比)、yyy為URL欄位、zzz為Target欄位，例: 'SysName', '系統名稱@A120,SysUrl,SysTarget'
                //布林資料　，格式[欄位名稱]　[抬頭[@Y[xxx]]]                xxx為指定寬度(整數為像素，小數為百分比)，例: 'IsEnabled', '啟用狀態@Y'　（欄位資料除支援Bool型態外，文字或數字型態的 Y/1 , N/0 也都支援）
                //布林資料　，格式[欄位名稱]　[抬頭[@M[xxx[,yyy[,zzz]]]]] 　 xxx為指定寬度(整數為像素，小數為百分比)、yyy為「是」時的顯示內容、zzz為「否」時的顯示內容，例: 'IsMail', '發送郵件@M,★,☆'　（欄位資料除支援Bool型態外，文字或數字型態的 Y/1 , N/0 也都支援）
                //
                //若設定「文字資料」欄位時，格式為[抬頭[@[x]yyy[,zzz]]]] ，則zzz視為[項目清單]顯示文字來源JSON(若此處未設定，但在 ucDataEditDefinition 的 CheckBoxList/RadioList 有定義 ucSourceDictionary，則一樣會自動偵測並轉換)
                //

                string tmpMask = "";
                decimal tmpMaskWidth = 0;
                if (_DisplayFormat.Length > 0)
                {
                    tmpMask = _DisplayFormat.Substring(0, 1);
                    if ("N,P".Split(',').Contains(tmpMask.ToUpper()))  //若為 [N] or [P]
                    {
                        if (_DisplayFormat.Length > 2)
                        {
                            tmpMaskWidth = decimal.Parse(_DisplayFormat.Substring(2));
                        }
                    }
                    else
                    {
                        if (_DisplayFormat.Length > 1)
                        {
                            tmpMaskWidth = decimal.Parse('0' + _DisplayFormat.Substring(1).Split(',')[0]);
                        }
                    }
                }

                if (tmpMaskWidth > 0)
                {
                    if (tmpMaskWidth < 1)
                    {
                        pnl1.Width = new Unit(tmpMaskWidth * 100 + "%");
                    }
                    else
                    {
                        pnl1.Width = new Unit(tmpMaskWidth + "px");
                    }
                }
                else
                {
                    pnl1.Width = new Unit("100%");
                }

                switch (tmpMask.ToUpper())
                {
                    case "D": //DateTime (Date) 
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd}");
                            lab1.Style["white-space"] = ""; //加入換行處理 2016.10.19
                        };
                        break;
                    case "T": //DateTime (Hour)
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd HH:mm}");
                        };
                        break;
                    case "S": //DateTime (Second)
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd HH:mm:ss}");
                        };
                        break;
                    case "N": //Number
                    case "P": //Percent
                        pnl1.CssClass = "Util_NoWrap";
                        pnl1.HorizontalAlign = HorizontalAlign.Right;
                        if (_DisplayFormat.Length <= 2)
                        {
                            // [N] or [Nx]
                            // [P] or [Px]
                            lab1.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                                if (Util.IsNumeric(lab1.Text))
                                {
                                    //考慮文字型別的[數值] 2017.02.14
                                    lab1.Text = string.Format("{0:" + _DisplayFormat + "}", Convert.ToDouble(lab1.Text));
                                }
                            };
                        }
                        else
                        {
                            // [N[x[yyy]]]
                            // [P[x[yyy]]]
                            lab1.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                                if (Util.IsNumeric(lab1.Text))
                                {
                                    //考慮文字型別的[數值] 2017.02.14
                                    lab1.Text = string.Format("{0:" + _DisplayFormat.Substring(0, 2) + "}", Convert.ToDouble(lab1.Text));
                                }
                            };
                        }
                        break;
                    case "L": //Left
                        pnl1.HorizontalAlign = HorizontalAlign.Left;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                            lab1.Text = Util.getHtmlNewLine(lab1.Text); //置換折行符號 2016.10.20 (不可用CSS [pre-wrap]方式，因為容易與其他CSS樣式衝突)
                        };
                        break;
                    case "C": //Center
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                            lab1.Text = Util.getHtmlNewLine(lab1.Text); //置換折行符號 2016.10.20 (不可用CSS [pre-wrap]方式，因為容易與其他CSS樣式衝突)
                        };
                        break;
                    case "R": //Right
                        pnl1.HorizontalAlign = HorizontalAlign.Right;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                            lab1.Text = Util.getHtmlNewLine(lab1.Text); //置換折行符號 2016.10.20 (不可用CSS [pre-wrap]方式，因為容易與其他CSS樣式衝突)
                        };
                        break;
                    case "I": //Image [抬頭[@I[xxx[,yyy[,zzz]]]]]    xxx為指定寬度、yyy為URL欄位、zzz為Target欄位，例: 'ProdGraph', '產品圖片@I96,SysUrl,_blank'
                        pnl1.Width = new Unit("100%");
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            string strWidth = "100%";
                            if (tmpMaskWidth > 0)
                            {
                                if (tmpMaskWidth < 1)
                                {
                                    strWidth = tmpMaskWidth * 100 + "%";
                                }
                                else
                                {
                                    strWidth = tmpMaskWidth + "px";
                                }
                            }
                            lab1.Text = string.Format("<img src='{0}' width='{1}' class='Util_clsShadow' />", DataBinder.Eval(dataItem, _ColName).ToString(), strWidth);

                            //Check URL,Target
                            string strURL = "";
                            string strTarget = "";
                            string[] intParaList = _DisplayFormat.Substring(1).Split(',');
                            if (intParaList.Count() == 3)  //xxx,yyy,zzz
                            {
                                strURL = intParaList[1];
                                strTarget = intParaList[2];
                            }
                            if (intParaList.Count() == 2) //xxx,yyy
                            {
                                strURL = intParaList[1];
                            }
                            if (!string.IsNullOrEmpty(strURL))
                            {
                                lab1.Text = string.Format("<a href='{1}' target='{2}' />{0}</a>", lab1.Text, DataBinder.Eval(dataItem, strURL).ToString(), string.IsNullOrEmpty(strTarget) ? "_blank" : DataBinder.Eval(dataItem, strTarget).ToString());
                            }
                        };
                        break;
                    case "A": //Anchor [抬頭[@A[xxx[,yyy[,zzz]]]]]　xxx為指定寬度、yyy為URL欄位、zzz為Target欄位
                        pnl1.HorizontalAlign = HorizontalAlign.Left;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            string strURL = _ColName;
                            string strTarget = "";
                            string[] intParaList = _DisplayFormat.Substring(1).Split(',');
                            if (intParaList.Count() == 3)  //xxx,yyy,zzz
                            {
                                strURL = intParaList[1];
                                strTarget = intParaList[2];
                            }
                            if (intParaList.Count() == 2) //xxx,yyy
                            {
                                strURL = intParaList[1];
                            }
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = string.Format("<a href='{1}' target='{2}' class='Util_clsHref' />{0}</a>", DataBinder.Eval(dataItem, _ColName).ToString(), DataBinder.Eval(dataItem, strURL).ToString(), string.IsNullOrEmpty(strTarget) ? "_blank" : DataBinder.Eval(dataItem, strTarget).ToString());
                        };
                        break;
                    case "Y": //[抬頭[@Y[xxx]  xxx為指定寬度
                        pnl1.HorizontalAlign = HorizontalAlign.Center;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString().Trim();
                            switch (lab1.Text.ToUpper())
                            {
                                case "TRUE":
                                case "Y":
                                case "1":
                                    lab1.Text = string.Format("<img src='{0}' border='0' />", Util.Icon_Yes);
                                    break;
                                case "FALSE":
                                case "N":
                                case "0":
                                    lab1.Text = string.Format("<img src='{0}' border='0' />", Util.Icon_No);
                                    break;
                                default:
                                    break;
                            }
                        };
                        break;
                    case "M": //[抬頭[@M[xxx[,yyy[,zzz]]]]] 　 xxx為指定寬度、yyy為「是」時的顯示內容、zzz為「否」時的顯示內容，例: 'IsMail', '發送郵件@M,★,☆'
                        pnl1.HorizontalAlign = HorizontalAlign.Center;

                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString().Trim();
                            string strYes = "<span style='font-size:18pt;font-weight:bold;' >●</span>";
                            string strNo = "<span style='font-size:18pt;font-weight:bold;' >○</span>";
                            string[] intParaList = _DisplayFormat.Substring(1).Split(',');

                            if (intParaList.Count() == 3)  //xxx,yyy,zzz
                            {
                                strYes = intParaList[1];
                                strNo = intParaList[2];
                            }
                            if (intParaList.Count() == 2) //xxx,yyy
                            {
                                strYes = intParaList[1];
                            }

                            switch (lab1.Text.ToUpper())
                            {
                                case "TRUE":
                                case "Y":
                                case "1":
                                    lab1.Text = strYes;
                                    break;
                                case "FALSE":
                                case "N":
                                case "0":
                                    lab1.Text = strNo;
                                    break;
                                default:
                                    break;
                            }
                        };
                        break;
                    default:
                        pnl1.HorizontalAlign = HorizontalAlign.Left;
                        lab1.DataBinding += delegate (object sender, EventArgs e)
                        {
                            object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                            lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                            lab1.Text = Util.getHtmlNewLine(lab1.Text); //置換折行符號 2016.10.20 (不可用CSS [pre-wrap]方式，因為容易與其他CSS樣式衝突)
                        };
                        break;
                }

                //判斷是否為「項目清單」
                //方法: 1/2 當「文字欄位」直接在 DisplayFormat 定義所需的 JSON
                if (_DisplayFormat.Length > 0)
                {
                    if ("L,C,R".Split(',').Contains(_DisplayFormat.Substring(0, 1).ToUpper()) && _DisplayFormat.Substring(1).Split(',').Count() > 1)
                    {
                        string strJSON = _DisplayFormat.Substring(_DisplayFormat.IndexOf(',', 0) + 1);
                        if (!string.IsNullOrEmpty(strJSON))
                        {
                            lab1.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                if (!string.IsNullOrEmpty(DataBinder.Eval(dataItem, _ColName).ToString()))
                                {
                                    string[] arIDList = DataBinder.Eval(dataItem, _ColName).ToString().Split(',');
                                    string strInfoList = "";
                                    Dictionary<string, string> oSourceDictionary = Util.getDictionary(strJSON);
                                    for (int i = 0; i < arIDList.Count(); i++)
                                    {
                                        //該項目必須有在候選清單內才合理
                                        if (oSourceDictionary.ContainsKey(arIDList[i]))
                                        {
                                            if (!string.IsNullOrEmpty(strInfoList))
                                            {
                                                strInfoList += ",";
                                            }
                                            strInfoList += oSourceDictionary[arIDList[i]];
                                        }
                                    }
                                    lab1.Text = strInfoList;
                                }
                            };
                        }
                    }
                }


                //方法: 2/2 DisplayFormat 未定義，但在 _EditDataDictionary 有定義 CheckBoxList / RadioList
                if (_EditType.ToUpper() == "CHECKBOXLIST" || _EditType.ToUpper() == "RADIOLIST")
                {
                    //若該欄位可編輯
                    if (_EditDataDictionary.ContainsKey("ucSourceDictionary"))
                    {
                        if (!string.IsNullOrEmpty(_EditDataDictionary["ucSourceDictionary"]))
                        {
                            lab1.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                if (!string.IsNullOrEmpty(DataBinder.Eval(dataItem, _ColName).ToString()))
                                {
                                    lab1.Text = DataBinder.Eval(dataItem, _ColName).ToString();
                                    Dictionary<string, string> oSourceDictionary = Util.getDictionary(_EditDataDictionary["ucSourceDictionary"]);
                                    if (!oSourceDictionary.ContainsKey("JSON"))
                                    {
                                        string[] arIDList = DataBinder.Eval(dataItem, _ColName).ToString().Split(',');
                                        string strInfoList = "";
                                        for (int i = 0; i < arIDList.Count(); i++)
                                        {
                                            //該項目必須有在候選清單內才合理
                                            if (oSourceDictionary.ContainsKey(arIDList[i]))
                                            {
                                                if (!string.IsNullOrEmpty(strInfoList))
                                                {
                                                    strInfoList += ",";
                                                }
                                                strInfoList += oSourceDictionary[arIDList[i]];
                                            }
                                        }
                                        lab1.Text = strInfoList;
                                    }
                                }
                            };
                        }
                    }
                }

                pnl1.Controls.Add(lab1);
                container.Controls.Add(pnl1);

                if (_AllowSortEdit)
                {
                    //編輯物件外層用Panel包裹，處理 Client 端的 顯示/隱藏
                    Panel pnl2 = new Panel();
                    pnl2.ID = "pnl" + _ColName;
                    pnl2.Style["display"] = "none";

                    switch (_EditType.ToUpper())
                    {
                        case "TEXTBOX":
                            Control oTxt = this.Page.LoadControl("~/Util/ucTextBox.ascx");
                            oTxt.ID = "txt" + _ColName;
                            if (pnl1.Width.Type == UnitType.Pixel && Convert.ToInt32(pnl1.Width.Value) > 0)
                            {
                                //若有指定寬度px (此處若是百分比，顯示時會變成該Cell寬度的百分比，不符所需)
                                Util.setObjectProperty(oTxt, "ucIsWidthByPixel", true);
                                Util.setObjectProperty(oTxt, "ucWidth", Convert.ToInt32(pnl1.Width.Value));
                            }
                            else
                            {
                                //考慮後方要顯示Validator的[*]，預設值為 90%
                                Util.setObjectProperty(oTxt, "ucIsWidthByPixel", false);
                                Util.setObjectProperty(oTxt, "ucWidth", 90);
                            }
                            if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            {
                                foreach (var pair in this._EditDataDictionary)
                                {
                                    Util.setObjectProperty(oTxt, pair.Key, pair.Value);
                                }
                            }

                            oTxt.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                switch (tmpMask.ToUpper())
                                {
                                    case "N":
                                        Util.setObjectProperty(oTxt, "ucTextData", DataBinder.Eval(dataItem, _ColName).ToString());
                                        Util.setObjectProperty(oTxt, "ucIsRegExp", true);
                                        break;
                                    case "D":
                                        Util.setObjectProperty(oTxt, "ucTextData", DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd}"));
                                        break;
                                    case "T":
                                        Util.setObjectProperty(oTxt, "ucTextData", DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd HH:mm}"));
                                        break;
                                    case "S":
                                        Util.setObjectProperty(oTxt, "ucTextData", DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd HH:mm:ss}"));
                                        break;
                                    default:
                                        Util.setObjectProperty(oTxt, "ucTextData", DataBinder.Eval(dataItem, _ColName).ToString());
                                        break;
                                }
                            };
                            oTxt.GetType().GetMethod("Refresh").Invoke(oTxt, null);
                            pnl2.Controls.Add(oTxt);
                            break;
                        case "CHECKBOX":
                            CheckBox oChk = new CheckBox();
                            oChk.ID = "chk" + _ColName;
                            oChk.Checked = false;
                            oChk.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                if ("TRUE,Y,1".Split(',').Contains(DataBinder.Eval(dataItem, _ColName).ToString().ToUpper()))
                                {
                                    oChk.Checked = true;
                                }
                            };
                            pnl2.HorizontalAlign = HorizontalAlign.Center;
                            pnl2.Controls.Add(oChk);
                            break;
                        case "CHECKBOXLIST":
                            Control oChkList = this.Page.LoadControl("~/Util/ucCheckBoxList.ascx");
                            oChkList.ID = "chk" + _ColName;
                            if (pnl1.Width.Type == UnitType.Pixel && Convert.ToInt32(pnl1.Width.Value) > 0)
                            {
                                //若有指定寬度px (此處若是百分比，顯示時會變成該Cell寬度的百分比，不符所需)
                                Util.setObjectProperty(oChkList, "ucIsWidthByPixel", true);
                                Util.setObjectProperty(oChkList, "ucWidth", Convert.ToInt32(pnl1.Width.Value));
                            }
                            else
                            {
                                //考慮後方要顯示Validator的[*]，預設值為 90%
                                Util.setObjectProperty(oChkList, "ucIsWidthByPixel", false);
                                Util.setObjectProperty(oChkList, "ucWidth", 90);
                            }

                            if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            {

                                oChkList.DataBinding += delegate (object sender, EventArgs e)
                                {
                                    //設定屬性
                                    foreach (var pair in this._EditDataDictionary)
                                    {
                                        Util.setObjectProperty(oChkList, pair.Key, pair.Value);
                                    }

                                    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                    Util.setObjectProperty(oChkList, "ucSelectedIDList", DataBinder.Eval(dataItem, _ColName).ToString());
                                };
                                pnl2.Controls.Add(oChkList);  // RowDataBound 時才執行元件的 Refresh() 2016.09.29
                            }

                            break;
                        case "RADIOLIST":
                            Control oRadList = this.Page.LoadControl("~/Util/ucRadioList.ascx");
                            oRadList.ID = "rad" + _ColName;
                            if (pnl1.Width.Type == UnitType.Pixel && Convert.ToInt32(pnl1.Width.Value) > 0)
                            {
                                //若有指定寬度px (此處若是百分比，顯示時會變成該Cell寬度的百分比，不符所需)
                                Util.setObjectProperty(oRadList, "ucIsWidthByPixel", true);
                                Util.setObjectProperty(oRadList, "ucWidth", Convert.ToInt32(pnl1.Width.Value));
                            }
                            else
                            {
                                //考慮後方要顯示Validator的[*]，預設值為 90%
                                Util.setObjectProperty(oRadList, "ucIsWidthByPixel", false);
                                Util.setObjectProperty(oRadList, "ucWidth", 90);
                            }

                            if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            {

                                oRadList.DataBinding += delegate (object sender, EventArgs e)
                                {
                                    //設定屬性
                                    foreach (var pair in this._EditDataDictionary)
                                    {
                                        Util.setObjectProperty(oRadList, pair.Key, pair.Value);
                                    }

                                    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                    Util.setObjectProperty(oRadList, "ucSelectedID", DataBinder.Eval(dataItem, _ColName).ToString());
                                };
                                pnl2.Controls.Add(oRadList);  // RowDataBound 時才執行元件的 Refresh() 2016.09.29
                            }

                            break;
                        case "DROPDOWNLIST":
                        case "DROPDOWNFIRST":
                            if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            {
                                DropDownList ddl = new DropDownList();  //動態加入 DropDownList
                                ddl.ID = "ddl" + _ColName;
                                ddl.Items.Clear();
                                if (_EditDataDictionary.ContainsKey("JSON"))
                                {
                                    //2014.10.21 新增，DropDownList資料來源可為資料列的指定欄位
                                    ddl.DataBound += delegate (object sender, EventArgs e)
                                    {
                                        object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                        int rowindex = ((sender as Control).NamingContainer as GridViewRow).RowIndex;
                                        DataTable datasource = (DataTable)((sender as Control).NamingContainer.NamingContainer as GridView).DataSource;
                                        string strJSON = datasource.Rows[rowindex][_EditDataDictionary["JSON"]].ToString();
                                        if (!string.IsNullOrEmpty(strJSON))
                                        {
                                            Dictionary<string, string> oDataDic = Util.getDictionary(strJSON, true);
                                            foreach (var pair in oDataDic)
                                            {
                                                ddl.Items.Add(new ListItem(pair.Value, pair.Key));
                                            }

                                        }

                                        if (_EditType.ToUpper() == "DROPDOWNFIRST")
                                        {
                                            //DropDownFirst
                                            //固定選擇第一項
                                            if (ddl.Items.Count <= 0)
                                            {
                                                ddl.Items.Add(new ListItem("", ""));
                                            }
                                            ddl.SelectedIndex = 0;
                                        }
                                        else
                                        {
                                            //DropDownList
                                            //偵測並新增空白項目
                                            if (!_EditDataDictionary.ContainsKey(""))
                                            {
                                                ddl.Items.Insert(0, new ListItem(RS.Resources.Msg_DDL_EmptyItem, ""));
                                            }
                                            ddl.SelectedValue = DataBinder.Eval(dataItem, _ColName).ToString();
                                        }
                                    };
                                    pnl2.Controls.Add(ddl);
                                }
                                else
                                {
                                    //自動偵測並新增空白項目
                                    if (!_EditDataDictionary.ContainsKey(""))
                                    {
                                        ddl.Items.Add(new ListItem(RS.Resources.Msg_DDL_EmptyItem, ""));
                                    }

                                    foreach (var pair in _EditDataDictionary)
                                    {
                                        ddl.Items.Add(new ListItem(pair.Value, pair.Key));
                                    }

                                    ddl.DataBound += delegate (object sender, EventArgs e)
                                    {
                                        object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                        ddl.SelectedValue = DataBinder.Eval(dataItem, _ColName).ToString();
                                    };
                                    pnl2.Controls.Add(ddl);
                                }
                            }
                            break;
                        case "CASCADING":
                            //Cascading
                            //Control oCascading = this.Page.LoadControl("~/Util/ucCascadingDropDown.ascx");
                            //oCascading.ID = "cas" + _ColName;
                            //if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            //{
                            //    foreach (var pair in this._EditDataDictionary)
                            //    {
                            //        Util.setObjectProperty(oCascading, pair.Key, pair.Value);
                            //    }
                            //}

                            //oCascading.DataBinding += delegate(object sender, EventArgs e)
                            //{
                            //    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);

                            //    //處理執行期 ContextKey
                            //    if (!string.IsNullOrEmpty(_EditDataDictionary["ucGridView_ContextKeyFieldList"]))
                            //    {
                            //        string strContextData = "";
                            //        string[] arList = _EditDataDictionary["ucGridView_ContextKeyFieldList"].Split(',');  //ex: FlowID,FlowLogID
                            //        for (int i = 0; i < arList.Count(); i++)
                            //        {
                            //            strContextData += string.Format("{0}:{1};", arList[i], (string)DataBinder.Eval(dataItem, arList[i]));
                            //        }

                            //        if (!string.IsNullOrEmpty(strContextData))
                            //        {
                            //            for (int i = 1; i <= 5; i++)
                            //            {
                            //                Util.setObjectProperty(oCascading, "ucContextData" + i.ToString().PadLeft(2, '0'), strContextData);
                            //            }
                            //        }
                            //    }

                            //    //處理執行期各階 DefaultSelectedValue
                            //    for (int i = 1; i <= 5; i++)
                            //    {
                            //        string strKey = "ucCategory" + i.ToString().PadLeft(2, '0');
                            //        if (_EditDataDictionary.ContainsKey(strKey) &&  !string.IsNullOrEmpty(_EditDataDictionary[strKey]))
                            //        {
                            //            Util.setObjectProperty(oCascading, "ucDefaultSelectedValue" + i.ToString().PadLeft(2, '0'), DataBinder.Eval(dataItem, _EditDataDictionary[strKey]).ToString());
                            //        }
                            //    }
                            //};
                            //pnl2.Controls.Add(oCascading);
                            break;
                        case "CALENDAR":
                            //DATE
                            pnl2.HorizontalAlign = HorizontalAlign.Center;
                            Control oCal = this.Page.LoadControl("~/Util/ucDatePicker.ascx");
                            oCal.ID = "cal" + _ColName;
                            Util.setObjectProperty(oCal, "ucWidth", 80);
                            if (pnl1.Width.Type == UnitType.Pixel && pnl1.Width.Value > 80)
                                pnl2.Width = pnl1.Width;

                            if (_EditDataDictionary != null && _EditDataDictionary.Count > 0)
                            {
                                foreach (var pair in this._EditDataDictionary)
                                {
                                    Util.setObjectProperty(oCal, pair.Key, pair.Value);
                                }
                            }

                            oCal.DataBinding += delegate (object sender, EventArgs e)
                            {
                                object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                string strDate = DataBinder.Eval(dataItem, _ColName, "{0:yyyy\\/MM\\/dd}");
                                if (!string.IsNullOrEmpty(strDate))
                                    Util.setObjectProperty(oCal, "ucDefaultSelectedDate", DateTime.ParseExact(strDate, "yyyy\\/MM\\/dd", null));
                            };
                            oCal.GetType().GetMethod("Refresh").Invoke(oCal, null);
                            pnl2.Controls.Add(oCal);
                            //HH:MM:SS
                            HtmlGenericControl oBR = new HtmlGenericControl();
                            oBR.InnerHtml = "<br>";
                            Dictionary<string, string> dicHH = new Dictionary<string, string>();
                            for (int i = 0; i < 24; i++)
                            {
                                dicHH.Add(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0'));
                            }
                            Dictionary<string, string> dicMM = new Dictionary<string, string>();
                            for (int i = 0; i < 60; i += 5)
                            {
                                dicMM.Add(i.ToString().PadLeft(2, '0'), i.ToString().PadLeft(2, '0'));
                            }
                            Dictionary<string, string> dicSS = new Dictionary<string, string>() { { "00", "00" }, { "30", "30" } };

                            if (tmpMask.ToUpper() == "T" || tmpMask.ToUpper() == "S")
                            {
                                DropDownList ddlHH = new DropDownList();  //動態加入 DropDownList
                                ddlHH.ID = "ddlHH" + _ColName;
                                ddlHH.Items.Clear();
                                foreach (var pair in dicHH)
                                {
                                    ddlHH.Items.Add(new ListItem(pair.Value, pair.Key));
                                }
                                ddlHH.DataBound += delegate (object sender, EventArgs e)
                                {
                                    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                    ddlHH.SelectedValue = DataBinder.Eval(dataItem, _ColName, "{0:HH}");
                                };

                                pnl2.Controls.Add(oBR);
                                pnl2.Controls.Add(ddlHH);
                                DropDownList ddlMM = new DropDownList();  //動態加入 DropDownList
                                ddlMM.ID = "ddlMM" + _ColName;
                                ddlMM.Items.Clear();
                                foreach (var pair in dicMM)
                                {
                                    ddlMM.Items.Add(new ListItem(pair.Value, pair.Key));
                                }
                                ddlMM.DataBound += delegate (object sender, EventArgs e)
                                {
                                    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                    ddlMM.SelectedValue = DataBinder.Eval(dataItem, _ColName, "{0:mm}");
                                };
                                pnl2.Controls.Add(ddlMM);
                            }

                            if (tmpMask.ToUpper() == "S")
                            {
                                DropDownList ddlSS = new DropDownList();  //動態加入 DropDownList
                                ddlSS.ID = "ddlSS" + _ColName;
                                ddlSS.Items.Clear();
                                foreach (var pair in dicSS)
                                {
                                    ddlSS.Items.Add(new ListItem(pair.Value, pair.Key));
                                }
                                ddlSS.DataBound += delegate (object sender, EventArgs e)
                                {
                                    object dataItem = DataBinder.GetDataItem(((Control)sender).NamingContainer);
                                    ddlSS.SelectedValue = DataBinder.Eval(dataItem, _ColName, "{0:ss}");
                                };
                                pnl2.Controls.Add(ddlSS);
                            }
                            break;
                        default:
                            break;
                    }
                    container.Controls.Add(pnl2);
                }
                break;
            default:
                break;
        }
    }
}