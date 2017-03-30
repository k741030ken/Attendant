using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using System.Text;

/// <summary>
/// [行事曆]控制項
/// </summary>
public partial class Util_ucScheduler : BaseUserControl
{

    #region 相關屬性

    /// <summary>
    /// 資料庫來源(預設從 [app://CfgDefConnDB/] 取得)
    /// </summary>
    public string ucDBName
    {
        get
        {
            if (ViewState["_DBName"] == null)
            {
                ViewState["_DBName"] = Util.getAppSetting("app://CfgDefConnDB/");
            }
            return ViewState["_DBName"].ToString();
        }
        set
        {
            ViewState["_DBName"] = value;
        }
    }

    /// <summary>
    /// 資料表名稱(預設 ResourceScheduler)
    /// </summary>
    public string ucTableName
    {
        get
        {
            if (ViewState["_TableName"] == null)
            {
                ViewState["_TableName"] = "ResourceScheduler";
            }
            return ViewState["_TableName"].ToString();
        }
        set
        {
            ViewState["_TableName"] = value;
        }
    }

    /// <summary>
    /// 顯示抬頭(預設：無)
    /// </summary>
    public string ucCaption
    {
        get
        {
            if (ViewState["_Caption"] == null)
            {
                ViewState["_Caption"] = "";
            }
            return ViewState["_Caption"].ToString();
        }
        set
        {
            ViewState["_Caption"] = value;
        }
    }
    /// <summary>
    /// 是否為 唯讀 (預設 [null]  由 ucResourceIDList 決定)
    /// 當 ResourceID 為複數時，自動設為 唯讀 
    /// </summary>
    public bool? ucIsReadOnly
    {
        get
        {
            return (bool?)(ViewState["_IsReadOnly"]);
        }
        set
        {
            ViewState["_IsReadOnly"] = value;
        }
    }

    /// <summary>
    /// 當唯讀時，若按下事件，是否彈出表單(預設true)
    /// </summary>
    public bool ucIsShowReadOnlyForm
    {
        //2015.11.26
        get
        {
            if (ViewState["_IsShowReadOnlyForm"] == null)
            {
                ViewState["_IsShowReadOnlyForm"] = true;
            }
            return (bool)(ViewState["_IsShowReadOnlyForm"]);
        }
        set
        {
            ViewState["_IsShowReadOnlyForm"] = value;
        }
    }
    /// <summary>
    /// 既有事件是否只有 Owner 才能編輯(預設 true)
    /// </summary>
    public bool ucIsOwnerEditOnly
    {
        get
        {
            if (ViewState["_IsOwnerEditOnly"] == null)
            {
                ViewState["_IsOwnerEditOnly"] = true;
            }
            return (bool)(ViewState["_IsOwnerEditOnly"]);
        }
        set
        {
            ViewState["_IsOwnerEditOnly"] = value;
        }
    }

    /// <summary>
    /// 同一時段內是否允許多個事件(預設 true)
    /// </summary>
    public bool ucIsConflictEnabled
    {
        get
        {
            if (ViewState["_IsConflictEnabled"] == null)
            {
                ViewState["_IsConflictEnabled"] = true;
            }
            return (bool)(ViewState["_IsConflictEnabled"]);
        }
        set
        {
            ViewState["_IsConflictEnabled"] = value;
        }
    }

    /// <summary>
    /// 是否顯示 導引列 (預設:true) 
    /// </summary>
    public bool ucIsNavBar
    {
        get
        {
            if (ViewState["_IsNavBar"] == null)
            {
                ViewState["_IsNavBar"] = true;
            }
            return (bool)(ViewState["_IsNavBar"]);
        }
        set
        {
            ViewState["_IsNavBar"] = value;
        }
    }

    /// <summary>
    /// 是否在導引列顯示 [小月曆] 按鈕(預設:true) 
    /// </summary>
    public bool ucIsMiniCalEnabled
    {
        get
        {
            if (ViewState["_IsMiniCalEnabled"] == null)
            {
                ViewState["_IsMiniCalEnabled"] = true;
            }
            return (bool)(ViewState["_IsMiniCalEnabled"]);
        }
        set
        {
            ViewState["_IsMiniCalEnabled"] = value;
        }
    }

    /// <summary>
    /// 是否僅顯示 [小月曆] (預設:false) 
    /// </summary>
    public bool ucIsMiniCalOnly
    {
        get
        {
            if (ViewState["_IsMiniCalOnly"] == null)
            {
                ViewState["_IsMiniCalOnly"] = false;
            }
            return (bool)(ViewState["_IsMiniCalOnly"]);
        }
        set
        {
            ViewState["_IsMiniCalOnly"] = value;
        }
    }

    /// <summary>
    /// 若 ucIsMiniCalOnly = true ，則點選日期時是否呼叫Scheduler UI (預設:false) 
    /// </summary>
    public bool ucIsMiniCalToScheduler
    {
        get
        {
            if (ViewState["_IsMiniCalToScheduler"] == null)
            {
                ViewState["_IsMiniCalToScheduler"] = false;
            }
            return (bool)(ViewState["_IsMiniCalToScheduler"]);
        }
        set
        {
            ViewState["_IsMiniCalToScheduler"] = value;
        }
    }

    /// <summary>
    /// 行事曆資料來源ID清單
    /// </summary>
    public string ucResourceIDList
    {
        get
        {
            if (ViewState["_ResourceIDList"] == null)
            {
                ViewState["_ResourceIDList"] = "";
            }
            return (string)(ViewState["_ResourceIDList"]);
        }
        set
        {
            ViewState["_ResourceIDList"] = value;
        }
    }

    /// <summary>
    /// 載入時的日期[YYYY,MM,DD] (預設：今天)  
    /// </summary>
    public string ucLoadDate
    {
        get
        {
            if (ViewState["_LoadDate"] == null)
            {
                ViewState["_LoadDate"] = string.Format("{0},{1},{2}", DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day); //month 需減一
            }
            return (string)(ViewState["_LoadDate"]);
        }
        set
        {
            ViewState["_LoadDate"] = value;
        }
    }

    /// <summary>
    /// 載入時的顯示模式，可用模式：month/week/day (預設：month)  
    /// </summary>
    public string ucLoadMode
    {
        get
        {
            if (ViewState["_LoadMode"] == null)
            {
                ViewState["_LoadMode"] = "month";
            }
            return (string)(ViewState["_LoadMode"]);
        }
        set
        {
            ViewState["_LoadMode"] = value;
        }
    }

    /// <summary>
    /// 顯示語系 zh-CHT/zh-CHS/en (預設：自動判斷目前語系)
    /// </summary>
    public string ucUICultureCode
    {
        get
        {
            if (ViewState["_UICultureCode"] == null)
            {
                ViewState["_UICultureCode"] = Util.getUICultureCode();
            }
            return (string)(ViewState["_UICultureCode"]);
        }
        set
        {
            ViewState["_UICultureCode"] = value;
        }
    }

    /// <summary>
    /// 新增行事曆事件時的文字顏色(預設：White)
    /// </summary>
    public string ucNewEventTextColor
    {
        get
        {
            if (ViewState["_NewEventTextColor"] == null)
            {
                ViewState["_NewEventTextColor"] = "White";
            }
            return (string)(ViewState["_NewEventTextColor"]);
        }
        set
        {
            ViewState["_NewEventTextColor"] = value;
        }
    }

    /// <summary>
    /// 新增行事曆事件時的背景顏色(預設：DodgerBlue)
    /// </summary>
    public string ucNewEventColor
    {
        get
        {
            if (ViewState["_NewEventColor"] == null)
            {
                ViewState["_NewEventColor"] = "DodgerBlue";
            }
            return (string)(ViewState["_NewEventColor"]);
        }
        set
        {
            ViewState["_NewEventColor"] = value;
        }
    }

    /// <summary>
    /// 是否以"週一"為一週的起始日(預設：false)
    /// </summary>
    public bool ucIsStartOnMonday
    {
        get
        {
            if (ViewState["_IsStartOnMonday"] == null)
            {
                ViewState["_IsStartOnMonday"] = false;
            }
            return (bool)(ViewState["_IsStartOnMonday"]);
        }
        set
        {
            ViewState["_IsStartOnMonday"] = value;
        }
    }

    /// <summary>
    /// 是否在導引列顯示年度控制按鈕(預設：false)
    /// </summary>
    public bool ucIsYearView
    {
        get
        {
            if (ViewState["_IsYearView"] == null)
            {
                ViewState["_IsYearView"] = false;
            }
            return (bool)(ViewState["_IsYearView"]);
        }
        set
        {
            ViewState["_IsYearView"] = value;
        }
    }

    /// <summary>
    /// 是否顯示事件提示內容(預設：true)
    /// </summary>
    public bool ucIsToolTip
    {
        get
        {
            if (ViewState["_IsToolTip"] == null)
            {
                ViewState["_IsToolTip"] = true;
            }
            return (bool)(ViewState["_IsToolTip"]);
        }
        set
        {
            ViewState["_IsToolTip"] = value;
        }
    }

    /// <summary>
    /// 顯示事件時，是否顯示「ID」 (預設：false)
    /// </summary>
    public bool ucIsShowEventID
    {
        get
        {
            if (ViewState["_IsShowEventID"] == null)
            {
                ViewState["_IsShowEventID"] = false;
            }
            return (bool)(ViewState["_IsShowEventID"]);
        }
        set
        {
            ViewState["_IsShowEventID"] = value;
        }
    }

    /// <summary>
    /// 顯示事件時，是否顯示「描述」 (預設：false)
    /// </summary>
    public bool ucIsShowEventDetail
    {
        get
        {
            if (ViewState["_IsShowEventDetail"] == null)
            {
                ViewState["_IsShowEventDetail"] = false;
            }
            return (bool)(ViewState["_IsShowEventDetail"]);
        }
        set
        {
            ViewState["_IsShowEventDetail"] = value;
        }
    }

    /// <summary>
    /// 顯示事件時，是否顯示「時間」 (預設：true)
    /// </summary>
    public bool ucIsShowEventTime
    {
        get
        {
            if (ViewState["_IsShowEventTime"] == null)
            {
                ViewState["_IsShowEventTime"] = true;
            }
            return (bool)(ViewState["_IsShowEventTime"]);
        }
        set
        {
            ViewState["_IsShowEventTime"] = value;
        }
    }

    /// <summary>
    /// 顯示事件時，是否顯示「更新資訊」 (預設：false)
    /// </summary>
    public bool ucIsShowEventUpdInfo
    {
        get
        {
            if (ViewState["_IsShowEventUpdInfo"] == null)
            {
                ViewState["_IsShowEventUpdInfo"] = false;
            }
            return (bool)(ViewState["_IsShowEventUpdInfo"]);
        }
        set
        {
            ViewState["_IsShowEventUpdInfo"] = value;
        }
    }

    /// <summary>
    /// 行事曆控制項寬度(預設：800)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null)
            {
                ViewState["_Width"] = 800;
            }
            return (int)(ViewState["_Width"]);
        }
        set
        {
            ViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 行事曆控制項高度(預設：600)
    /// </summary>
    public int ucHeight
    {
        get
        {
            if (ViewState["_Height"] == null)
            {
                ViewState["_Height"] = 600;
            }
            return (int)(ViewState["_Height"]);
        }
        set
        {
            ViewState["_Height"] = value;
        }
    }

    /// <summary>
    /// [小月曆]控制項寬度(預設：200)
    /// </summary>
    public int ucMiniWidth
    {
        get
        {
            if (ViewState["_MiniWidth"] == null)
            {
                ViewState["_MiniWidth"] = 200;
            }
            return (int)(ViewState["_MiniWidth"]);
        }
        set
        {
            ViewState["_MiniWidth"] = value;
        }
    }

    /// <summary>
    /// [小月曆]控制項高度(預設：200)
    /// </summary>
    public int ucMiniHeight
    {
        get
        {
            if (ViewState["_MiniHeight"] == null)
            {
                ViewState["_MiniHeight"] = 200;
            }
            return (int)(ViewState["_MiniHeight"]);
        }
        set
        {
            ViewState["_MiniHeight"] = value;
        }
    }

    /// <summary>
    /// 每日起始時刻 (預設： 8)
    /// </summary>
    public int ucFirstHour
    {
        get
        {
            if (ViewState["_FirstHour"] == null)
            {
                ViewState["_FirstHour"] = 8;
            }
            return (int)(ViewState["_FirstHour"]);
        }
        set
        {
            ViewState["_FirstHour"] = Math.Max(0, Math.Min(value, ucLastHour - 1));
        }
    }

    /// <summary>
    /// 每日結束時刻 (預設： 20)
    /// </summary>
    public int ucLastHour
    {
        get
        {
            if (ViewState["_LastHour"] == null)
            {
                ViewState["_LastHour"] = 20;
            }
            return (int)(ViewState["_LastHour"]);
        }
        set
        {
            ViewState["_LastHour"] = Math.Min(24, Math.Max(value, ucFirstHour + 1));
        }
    }

    /// <summary>
    /// 時間選項間隔分鐘，範圍 5 ~ 60 (預設：15)
    /// </summary>
    public int ucTimeStep
    {
        get
        {
            if (ViewState["_TimeStep"] == null)
            {
                ViewState["_TimeStep"] = 15;
            }
            return (int)(ViewState["_TimeStep"]);
        }
        set
        {
            // --- 會將設定值 調整為 符合5~60 間可被 60整除之數目; 目的在於讓選項中必定有"整點"時間可選
            int timeStep = Math.Max(5, Math.Min(value, 60));
            while (60 % timeStep != 0)
                timeStep++;
            ViewState["_TimeStep"] = timeStep;
        }
    }

    /// <summary>
    /// 事件預設使用分鐘 (預設：60)
    /// </summary>
    public int ucEventDuration
    {
        get
        {
            if (ViewState["_EventDuration"] == null)
            {
                ViewState["_EventDuration"] = 60;
            }
            return (int)(ViewState["_EventDuration"]);
        }
        set
        {
            ViewState["_EventDuration"] = Math.Max(5, value);
        }
    }

    /// <summary>
    /// 行事曆自訂樣式 (預設 [margin:auto;border: 1px solid #E0E0E0;] )
    /// </summary>
    public string ucCustStyle
    {
        get
        {
            if (ViewState["_CustStyle"] == null)
            {
                ViewState["_CustStyle"] = "margin:auto;border: 1px solid #E0E0E0;";
            }
            return (string)(ViewState["_CustStyle"]);
        }
        set
        {
            ViewState["_CustStyle"] = value;
        }
    }

    /// <summary>
    /// 迷你日曆自訂樣式 (預設 [空白] )
    /// </summary>
    public string ucMiniCustStyle
    {
        get
        {
            if (ViewState["_MiniCustStyle"] == null)
            {
                ViewState["_MiniCustStyle"] = "";
            }
            return (string)(ViewState["_MiniCustStyle"]);
        }
        set
        {
            ViewState["_MiniCustStyle"] = value;
        }
    }
    #endregion

    /// <summary>
    /// 內部語系物件
    /// </summary>
    class ResourceInfo
    {
        internal string ResourceID { get; set; }
        internal string Subject { get; set; }
        internal string Description { get; set; }
        internal string StartDate { get; set; }
        internal string EndDate { get; set; }
        internal string UpdInfo { get; set; }
        internal string Msg_EventConflict { get; set; }
        internal string Msg_NotEventOwner { get; set; }
    }

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        Refresh();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        if (this.Visible)
        {
            //若資料來源為 EWS ，則強制部份參數的值
            if (ucTableName.ToUpper() == "EWS")
            {
                ucIsReadOnly = true;
                ucIsShowEventDetail = false;
                ucIsShowEventUpdInfo = false;
            }

            SchedulerInitJS();

            string[] CustStyleList;
            if (!string.IsNullOrEmpty(ucCustStyle))
            {
                CustStyleList = ucCustStyle.Split(';');
                for (int i = 0; i < CustStyleList.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(CustStyleList[i]))
                    {
                        scheduler_here.Style[CustStyleList[i].Split(':')[0]] = CustStyleList[i].Split(':')[1];
                    }
                }
            }

            if (!string.IsNullOrEmpty(ucMiniCustStyle))
            {
                CustStyleList = ucMiniCustStyle.Split(';');
                for (int i = 0; i < CustStyleList.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(CustStyleList[i]))
                    {
                        mini_here.Style[CustStyleList[i].Split(':')[0]] = CustStyleList[i].Split(':')[1];
                    }
                }
            }

            mini_here.Style["width"] = string.Format("{0}px", ucMiniWidth); //"200px";
            mini_here.Style["height"] = string.Format("{0}px", ucMiniHeight); //"200px";
            scheduler_here.Style["width"] = string.Format("{0}px", ucWidth); //"800px";
            scheduler_here.Style["height"] = string.Format("{0}px", ucHeight); //"600px";
        }
    }

    /// <summary>
    /// 初始JS
    /// </summary>
    void SchedulerInitJS()
    {
        Random rnd = new Random();
        string strResourceIDList = ucResourceIDList;
        string strCultureCode = ucUICultureCode;
        string strSchedSysPath = Util._SysPath + "/dhtmlxScheduler/";
        Util.setJS(Util.getFixURL(strSchedSysPath + "dhtmlxscheduler.js"), "dhtmlx_JS");
        Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_active_links.js"), "dhtmlx_actlink");
        Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_recurring.js"), "dhtmlx_recurring");
        Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_minical.js"), "dhtmlx_minical");

        if (ucIsToolTip)
        {
            Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_tooltip.js"), "dhtmlx_tooltip");
        }

        if (ucIsYearView)
        {
            Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_year_view.js"), "dhtmlx_yearview");
        }

        Util.setCSS(Util.getFixURL(strSchedSysPath + "dhtmlxscheduler.css"));

        //判斷顯示語系
        switch (strCultureCode)
        {
            case "zh-CHT":
                Util.setJS(Util.getFixURL(strSchedSysPath + "locale/locale_tw.js"), "dhtmlx_Loc01");
                Util.setJS(Util.getFixURL(strSchedSysPath + "locale/recurring/locale_recurring_tw.js"), "dhtmlx_Loc02");
                break;
            case "zh-CHS":
            case "zh-Hans":
                Util.setJS(Util.getFixURL(strSchedSysPath + "locale/locale_cn.js"), "dhtmlx_Loc01");
                Util.setJS(Util.getFixURL(strSchedSysPath + "locale/recurring/locale_recurring_cn.js"), "dhtmlx_Loc02");
                break;
            default:
                break;
        }

        string strDataURL = string.Format("{0}?DBName={1}&TableName={2}&IsReadOnly={3}&ResourceIDList={4}&NewColor={5}&NewTextColor={6}", Util.getFixURL(strSchedSysPath + "SchedulerHandler.ashx"), ucDBName, ucTableName, (ucIsReadOnly == true) ? "Y" : "N", strResourceIDList, ucNewEventColor, ucNewEventTextColor);

        Util.setJSContent("");

        StringBuilder sb = new StringBuilder();

        sb.Append("\nfunction init() { \n"
                + "    scheduler.config.xml_date = '%Y-%m-%d %H:%i'; \n");
        sb.AppendFormat("    scheduler.config.first_hour = {0}; \n", ucFirstHour);
        sb.AppendFormat("    scheduler.config.last_hour = {0}; \n", ucLastHour);
        sb.AppendFormat("    scheduler.config.time_step = {0}; \n", ucTimeStep);
        sb.AppendFormat("    scheduler.config.start_on_monday = {0}; \n", ucIsStartOnMonday ? 1 : 0);
        sb.AppendFormat("    scheduler.config.event_duration = {0}; \n", ucEventDuration);
        sb.Append("    scheduler.config.auto_end_date = true; \n"
                + "    scheduler.config.details_on_create = true;\n"
                + "    scheduler.config.details_on_dblclick = true;\n");

        sb.Append("    scheduler.config.lightbox.sections=[\n"
                + "    {name:'text'   , height:24 ,  map_to:'text'   , type:'textarea' , focus:true },\n"
                + "    {name:'description' , height:200, map_to:'description' , type:'textarea' },\n"
                + "    {name:'recurring',type:'recurring',map_to:'rec_type',button:'recurring'},\n"
                + "    {name:'time'   , height:72 , map_to:'auto'   , type:'time' }\n"
                + "    ];\n");


        ResourceInfo oRS = new ResourceInfo();
        switch (strCultureCode)
        {
            case "en":
                oRS = new ResourceInfo()
                {
                    ResourceID = "Source :",
                    Subject = "Subject:",
                    Description = "Desc.  :",
                    StartDate = "Start  :",
                    EndDate = "End    :",
                    UpdInfo = "Update :",
                    Msg_EventConflict = "Event Collision !",
                    Msg_NotEventOwner = "Not Event Owner !"
                };
                break;
            case "zh-CHT":
            case "zh-Hant":
                oRS = new ResourceInfo()
                {
                    ResourceID = "識別:",
                    Subject = "主旨:",
                    Description = "描述:",
                    StartDate = "開始:",
                    EndDate = "結束:",
                    UpdInfo = "更新:",
                    Msg_EventConflict = "該時段已有事件存在 ！",
                    Msg_NotEventOwner = "無權編修此事件 ！"
                };
                break;
            case "zh-CHS":
            case "zh-Hans":
                oRS = new ResourceInfo()
                {
                    ResourceID = "识别:",
                    Subject = "主旨:",
                    Description = "描述:",
                    StartDate = "开始:",
                    EndDate = "结束:",
                    UpdInfo = "更新:",
                    Msg_EventConflict = "该时段已有事件存在！",
                    Msg_NotEventOwner = "无权编修此事件！"
                };
                break;
        }

        if (ucIsToolTip)
        {
            sb.Append("    scheduler.templates.tooltip_text = function(start,end,ev){{");
            sb.AppendFormat("    return '<b>{0}</b> '+ ev.text ", oRS.Subject);

            if (ucIsShowEventDetail)
            {
                sb.AppendFormat("    + '<br/><b>{0}</b> ' + ev.description ", oRS.Description);
            }

            if (ucIsShowEventID)
            {
                sb.AppendFormat("    + '<hr/><b>{0}</b> [' + ev.ResourceID + ']' ", oRS.ResourceID);
            }

            sb.AppendFormat("    + '<br/><b>{0}</b> ' + scheduler.templates.tooltip_date_format(start) ", oRS.StartDate);

            sb.AppendFormat("    + '<br/><b>{0}</b> ' + scheduler.templates.tooltip_date_format(end)", oRS.EndDate);
            if (ucIsShowEventUpdInfo)
            {
                sb.AppendFormat("    + '<br/><b>{0}</b> ' + ev.UpdDateTime.substr(0, 16) + ' By ' + ev.UpdUser + ' - ' + ev.UpdUserName ", oRS.UpdInfo);
            }
            sb.Append(" ;}};\n");
        }

        if (!ucIsShowEventTime)
        {
            sb.Append("scheduler.templates.event_date = function(start,end,ev){ return '' ;}; \n");
        }

        if (!ucIsNavBar)
        {
            sb.Append("    scheduler.xy.nav_height = 0;\n");
            dhx_cal_navline.Style["display"] = "none";
        }

        if (!ucIsMiniCalEnabled)
        {
            dhx_minical_icon.Style["display"] = "none";
        }

        if (ucIsYearView)
        {
            sb.Append("    document.getElementById('yearView').style.display = '';\n");
        }

        if (ucIsMiniCalOnly)
        {
            mini_here.Style["display"] = "";
            scheduler_here.Style["display"] = "none";
            sb.AppendFormat("    scheduler.load('{0}&Rnd={1}' ,", strDataURL, rnd.Next(10000, 99999));
            sb.Append("    function(){  scheduler.renderCalendar({\n"
                       + "    container:'mini_here',date:scheduler._date,navigation:true,handler:function(date,calendar){ \n"
                       );
            //在獨立的小日曆按下日期後的動作
            if (ucIsMiniCalToScheduler)
            {
                string strScheUrl = strScheUrl = string.Format("{0}?DBName={1}&TableName={2}&ResourceIDList={3}&IsReadOnly={4}&LoadMode={5}&Width={6}&Height={7}", Util.getFixURL("~/Util/SchedulerAdmin.aspx"), ucDBName, ucTableName, ucResourceIDList, (ucIsReadOnly == true) ? "Y" : "N", ucLoadMode, ucWidth, ucHeight);

                strScheUrl += string.Format("&IsShowEventDetail={0}&IsShowEventTime={1}&IsShowEventID={2}&IsShowEventUpdInfo={3}&IsConflictEnabled={4}&IsOwnerEditOnly={5}",
                (ucIsShowEventDetail) ? "Y" : "N",
                (ucIsShowEventTime) ? "Y" : "N",
                (ucIsShowEventID) ? "Y" : "N",
                (ucIsShowEventUpdInfo) ? "Y" : "N",
                (ucIsConflictEnabled) ? "Y" : "N",
                (ucIsOwnerEditOnly) ? "Y" : "N"
                );


                sb.Append("var PopUrl  = '" + strScheUrl + "&LoadDate=' + date.getFullYear() + ',' + date.getMonth() + ',' + date.getDate(); \n");
                string strSchePop = string.Format("var {0}_Pop=window.open(PopUrl + '&Caption={1}','{0}_PopWin','{2}');{0}_Pop.focus();return false;", "ScheAdmin", ucCaption, Util.getAppSetting("app://CfgPopupSpecs/"));
                sb.Append(strSchePop);
            }
            else
            {
                sb.Append("  alert('Selected ScheDate = [' + date.getFullYear() + ',' + date.getMonth() + ',' +  date.getDate() + ']'); \n");
            }

            sb.Append("      }\n"
                    + "    });\n"
                    + " });\n"
                    );
        }
        else
        {
            mini_here.Style["display"] = "none";
            scheduler_here.Style["display"] = "";
            sb.AppendFormat("    scheduler.load('{0}&Rnd={1}');\n", strDataURL, rnd.Next(10000, 99999));
        }

        if (ucIsReadOnly == true || strResourceIDList.Split(',').Length > 1)    // 只要 ResourceID 數量大於1 即為唯讀
        {
            //當 [唯讀] 或 [多個 ResourceID]
            Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_readonly.js"), "dhtmlx_readonly");
            sb.Append("    scheduler.config.drag_move = false;\n");

            //2015.11.26 add [ucIsShowReadOnlyForm]
            if (ucIsShowReadOnlyForm)
            {
                sb.Append("    scheduler.config.readonly = false;\n");
                sb.Append("    scheduler.config.readonly_form = true;\n");
            }
            else
            {
                sb.Append("    scheduler.config.readonly = true;\n");
                sb.Append("    scheduler.config.readonly_form = false;\n");
            }


            sb.Append("    scheduler.attachEvent('onEmptyClick', function(date,e) { return false; } ); \n");
            sb.Append("    scheduler.attachEvent('onClick', function(id, e) { return false; } ); \n");
        }
        else
        {
            //可編輯
            //Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_quick_info.js"), "dhtmlx_quickinfo");
            sb.AppendFormat("    var dp = new dataProcessor('{0}&Rnd={1}');\n", strDataURL, rnd.Next(10000, 99999));
            sb.Append("    dp.init(scheduler);\n");

            //非既有事件 Owner 不能進行修改/刪除
            if (ucIsOwnerEditOnly)
            {
                UserInfo oUser = UserInfo.getUserInfo();
                if (oUser != null && !string.IsNullOrEmpty(oUser.UserID))
                {
                    sb.Append("    scheduler.attachEvent('onClick', function(id, e) {\n"
                            + "       ev = scheduler.getEvent(id); \n"
                            + "       if (ev.UpdUser != undefined && ev.UpdUser != '" + oUser.UserID + "'){ \n"
                        //+ "          alert ('" + oRS.Msg_NotEventOwner +"'); \n"
                            + "          return false; }\n"
                            + "       return true; \n"
                            + "     }); \n");

                    sb.Append("    scheduler.attachEvent('onDblClick', function(id, e) {\n"
                            + "       ev = scheduler.getEvent(id); \n"
                            + "       if (ev.UpdUser != undefined && ev.UpdUser != '" + oUser.UserID + "'){ \n"
                            + "          alert ('" + oRS.Msg_NotEventOwner + "'); \n"
                            + "          return false; }\n"
                            + "       return true; \n"
                            + "     }); \n");

                    sb.Append("    scheduler.attachEvent('onBeforeDrag', function(id, e) {\n"
                            + "       ev = scheduler.getEvent(id); \n"
                            + "       if (ev.UpdUser != undefined && ev.UpdUser != '" + oUser.UserID + "'){ \n"
                        //+ "          alert ('" + oRS.Msg_NotEventOwner + "'); \n"
                            + "          return false; }\n"
                            + "       return true; \n"
                            + "     }); \n");
                }
            }
        }


        //若同時段只允許一個事件
        if (!ucIsConflictEnabled)
        {
            Util.setJS(Util.getFixURL(strSchedSysPath + "ext/dhtmlxscheduler_collision.js"), "dhtmlx_collision");
            sb.Append("    scheduler.attachEvent('onEventCollision', function (ev, evs) { \n"
                    + "       alert('" + oRS.Msg_EventConflict + "'); \n"
                    + "       return true; \n"
                    + "     }); \n");
        }

        sb.Append(" scheduler.config.show_loading = true;\n");
        sb.AppendFormat("    scheduler.init('scheduler_here', new Date({1}), '{0}');\n", ucLoadMode, ucLoadDate);

        sb.Append("} \n"
                + "window.onload = function () { init(); };\n");

        Util.setJSContent(sb.ToString(), "dhtmlx_Init");

        //-- 切換小月曆的 script
        Util.setJSContent("\nfunction show_minical() {\n"
            + "    if (scheduler.isCalendarVisible()) {\n"
            + "        scheduler.destroyCalendar();\n"
            + "    } else {\n"
            + "        scheduler.renderCalendar({\n"
            + "            position: 'dhx_minical_icon',\n"
            + "            date: scheduler._date,\n"
            + "            navigation: true,\n"
            + "            handler: function (date, calendar) {\n"
            + "                scheduler.setCurrentView(date);\n"
            + "                scheduler.destroyCalendar()\n"
            + "            }\n"
            + "        });\n"
            + "    }\n"
            + "}\n", "dhtmlx_showmini");
    }
}