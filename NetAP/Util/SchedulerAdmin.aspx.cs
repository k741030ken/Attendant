using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

/// <summary>
/// 因為可能登入前就需顯示行事曆，改繼承自 BasePage
/// **若登入前使用，會強制設定唯讀模式**
/// **若DBName='EWS'，則TableName自動為'EWS'，代表要從 EWS 讀取 Exchange 資料**
/// </summary>
public partial class Util_SchedulerAdmin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Scheduler1.ucDBName = Util.getRequestQueryStringKey("DBName");
        Scheduler1.ucTableName = Util.getRequestQueryStringKey("TableName");
        Scheduler1.ucResourceIDList = Util.getRequestQueryStringKey("ResourceIDList");

        bool IsParaError = false;
        //參數檢核 2016.05.18 調整
        if (!IsParaError && string.IsNullOrEmpty(Scheduler1.ucDBName))
        {
            if (Scheduler1.ucTableName.ToUpper() != "EWS")
            {
                IsParaError = true;
            }
        }

        if (!IsParaError && string.IsNullOrEmpty(Scheduler1.ucDBName) && string.IsNullOrEmpty(Scheduler1.ucTableName) && string.IsNullOrEmpty(Scheduler1.ucResourceIDList))
        {
            IsParaError = true;
        }

        if (IsParaError)
        {
            string strErrMsg = @"Parameter Error <ul>
            <li>必要參數：
                <ul>
                    <li>資料庫</li>
                    <ul>
                      <li>DBName<li>ResourceIDList
                    </ul>
                </ul>
                <ul>
                    <li>Exchange</li>
                    <ul>
                      <li>DBName or TableName =[EWS]<li>ResourceIDList (空值讀取預設行事曆，若 = [xxx@aaa.com]，則讀取該帳號行事曆)
                    </ul>
                </ul>
                <li>選擇性參數：
                <ul>
                    <li>TableName<li>Caption<li>IsRefreshParent
                    <li>IsNavBar<li>IsReadOnly<li>IsYearView<li>IsToolTip<li>UICultureCode
                    <li>IsMiniCalOnly
                    <li>IsMiniCalToScheduler
                    <li>LoadDate(YYYYMMDD)
                    <li>LoadMode([month],week,day)
                    <li>MiniWidth<li>MiniHeight<li>Width<li>Height
                    <li>IsShowEventID<li>IsShowEventTime<li>IsShowEventDetail<li>IsShowEventUpdInfo
                    <li>IsConflictEnabled<li>IsOwnerEditOnly
                </ul>
            ";
            Scheduler1.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, strErrMsg);
            return;
        }

        UserInfo oUser = UserInfo.getUserInfo();

        //判斷 [EWS] 所需環境
        if (Scheduler1.ucDBName.ToUpper() == "EWS" || Scheduler1.ucTableName.ToUpper() == "EWS")
        {
            Util.chkSession();
            Scheduler1.ucDBName = "EWS";
            Scheduler1.ucTableName = "EWS";
        }

        #region 設定 Scheduler 相關屬性
        //檢查是否需強制唯讀
        if (oUser != null)
        {
            Scheduler1.ucIsReadOnly = (Util.getRequestQueryStringKey("IsReadOnly", "N", true) == "Y") ? true : false;
        }
        else
        {
            Scheduler1.ucIsReadOnly = true;
        }

        if (!Util.getRequestQueryString().IsNullOrEmpty("LoadDate"))
            Scheduler1.ucLoadDate = Util.getRequestQueryStringKey("LoadDate");

        if (!Util.getRequestQueryString().IsNullOrEmpty("LoadMode"))
            Scheduler1.ucLoadMode = Util.getRequestQueryStringKey("LoadMode").ToLower(); //[month],week,day

        if (!Util.getRequestQueryString().IsNullOrEmpty("UICultureCode"))
            Scheduler1.ucUICultureCode = Util.getRequestQueryStringKey("UICultureCode");

        Scheduler1.ucIsNavBar = (Util.getRequestQueryStringKey("IsNavBar", "Y", true) == "Y") ? true : false;
        Scheduler1.ucIsMiniCalOnly = (Util.getRequestQueryStringKey("IsMiniCalOnly", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsMiniCalToScheduler = (Util.getRequestQueryStringKey("IsMiniCalToScheduler", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsYearView = (Util.getRequestQueryStringKey("IsYearView", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsToolTip = (Util.getRequestQueryStringKey("IsToolTip", "Y", true) == "Y") ? true : false;
        Scheduler1.ucCaption = Util.getRequestQueryStringKey("Caption");
        Scheduler1.ucWidth = int.Parse(Util.getRequestQueryStringKey("Width", "800"));
        Scheduler1.ucHeight = int.Parse(Util.getRequestQueryStringKey("Height", "600"));
        Scheduler1.ucMiniWidth = int.Parse(Util.getRequestQueryStringKey("MiniWidth", "200"));
        Scheduler1.ucMiniHeight = int.Parse(Util.getRequestQueryStringKey("MiniHeight", "200"));
        Scheduler1.ucIsShowEventID = (Util.getRequestQueryStringKey("IsShowEventID", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsShowEventTime = (Util.getRequestQueryStringKey("IsShowEventTime", "Y", true) == "Y") ? true : false;
        Scheduler1.ucIsShowEventDetail = (Util.getRequestQueryStringKey("IsShowEventDetail", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsShowEventUpdInfo = (Util.getRequestQueryStringKey("IsShowEventUpdInfo", "N", true) == "Y") ? true : false;
        Scheduler1.ucIsConflictEnabled = (Util.getRequestQueryStringKey("IsConflictEnabled", "Y", true) == "Y") ? true : false;
        Scheduler1.ucIsOwnerEditOnly = (Util.getRequestQueryStringKey("IsOwnerEditOnly", "Y", true) == "Y") ? true : false;

        #endregion

        //網頁標題
        if (!Scheduler1.ucIsMiniCalOnly)
        {
            if (!string.IsNullOrEmpty(Scheduler1.ucCaption))
            {
                labCaption.Visible = true;
                labCaption.Text = Scheduler1.ucCaption;
            }
        }

        //當視窗關閉時，是否自動重新整理父視窗
        if (Scheduler1.ucIsReadOnly != true)
        {
            if (Util.getRequestQueryStringKey("IsRefreshParent", "N", true) == "Y")
            {
                bodyScheduler.Attributes["onunload"] = "refreshSchedulerParent();";
            }
        }
    }
}