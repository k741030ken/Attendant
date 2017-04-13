using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class UserInfoList : SecurePage
{
    //資料庫來源
    private string _DBName = "";
    //鍵值欄位
    private string[] _PKList = "UserID".Split(',');
    //查詢基礎 SQL
    private string _QryBaseSQL = @"Select CompID ,CompName ,DeptID ,DeptName ,UserID ,UserName ,UserStatusName ,UserKindName ,UserMail ,UserTel From " + Util.getAppSetting("app://CfgUserInfoSource/");
    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        { 
            if (PageViewState["_QryResultSQL"] == null) { PageViewState["_QryResultSQL"] = ""; }
            return (string)(PageViewState["_QryResultSQL"]);
        }
        set
        {
            PageViewState["_QryResultSQL"] = value;
        }
    }

    protected override void OnInitComplete(EventArgs e)
    {
        //PageIsMetaTags = false;               //頁面自訂 MetaTag
        //PageIsAlertSessionTimeout = true;     //頁面逾時提醒
        //PageIsRefreshSessionFrame = true;     //保持 Session 不會逾時
        //PageIsRequestQueryStringLog = true;   //記錄 Request.QueryString Log
        //PageIsRequestFormLog = true;          //記錄 Request.Form Log
        PageIsLock = true;                      //頁面鎖定(鎖住滑鼠右鍵選單及複製功能)
        PageIsWatermark = true;                 //頁面浮水印
        PageWatermarkMarginLeft = 120;          //浮水印左邊界，避免浮水印蓋到按鈕
        PageWatermarkFirstPosY = 420;           //浮水印首次出現 Y 軸像素
        PageWatermarkContentRepeatTimes = 2;    //浮水印重複次數

        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //提示訊息 Tooltip 範本
        HoverTooltip1.HtmlTemplate = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Dialog);
        HoverTooltip1.DefaultTooltipTitle = RS.Resources.Msg_DefaultTooltipTitle;

        //附掛提示訊息物件
        HoverTooltip1.AddTooltipControl(btnClear);
        btnClear.ToolTip = "清除現有查詢條件及查詢結果";
        HoverTooltip1.AddTooltipControl(btnQry);
        btnQry.ToolTip = "依據現有查詢條件進行資料查詢";

        //匯出按鈕 2016.11.22
        btnOrgExport.ucConfirmMsg = RS.Resources.Msg_ExportDataConfirm;
        btnOrgExport.ucProcessLightboxMsg = RS.Resources.Msg_ExportDataPreparing;
        btnOrgExport.onStart += btnOrgExport_onStart;

        // 事件訂閱
        ucGridView_Fix.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView_Fix_RowCommand);
        ucGridView_Fix.GridViewCommand += ucGridView_Fix_GridViewCommand;
        ucGridView_Roll.RowCommand += ucGridView_Roll_RowCommand;

        //設定關聯式下拉選單
        qryCompDeptUser.SetDefault();
        qryCompDeptUser.ucIsSearchEnabled = true;
        qryCompDeptUser.ucSearchBoxWaterMarkText = "搜尋公司";
        qryCompDeptUser.ucSearchBoxWidth = 120;
        qryCompDeptUser.ucIsVerticalLayout = false;
        if (!IsPostBack)
        {
            //處理預設值
            UserInfo oUser = UserInfo.getUserInfo();
            qryCompDeptUser.ucDefaultSelectedValue01 = oUser.CompID;
            qryCompDeptUser.ucDefaultSelectedValue02 = oUser.DeptID;
        }
        qryCompDeptUser.Refresh();
    }

    void btnOrgExport_onStart(object sender, EventArgs e)
    {
        //產生匯出資料
        string strFileName = "Org.xlsx";
        byte[] oBytes = Util.getBytes(Util.getExcelOpenXml(OrgInfo.getOrgData()));
        //將匯出資料設定為 FileInfoObj 物件
        if (FileInfoObj.setFileInfoObj(strFileName, oBytes, true))
        {
            //資料直接下載
            if (FileInfoObj.DirectDownload())
            {
                //下載正常
                //btnOrgExport.Complete(); //不顯示訊息
                btnOrgExport.Complete(RS.Resources.Msg_ExportDataReadyToDownload);  //匯出資料準備完成，請按[存檔]下載
            }
            else
            {
                //下載錯誤
                btnOrgExport.Complete(RS.Resources.Msg_ExportDataNotFound, Util.NotifyKind.Error); //查無可供匯出的資料
            }
        }
        else
        {
            //設定 FileInfoObj 物件失敗
            Util.NotifyMsg(RS.Resources.Msg_ExportDataError, Util.NotifyKind.Error); //匯出資料發生錯誤
        }

    }

    void ucGridView_Roll_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        Util.MsgBox(string.Format("<b>RowCommand</b><hr>Cmd=[{0}]<br>Keys=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
    }

    void ucGridView_Fix_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        if (e.CommandName == "cmdExportOpenXml" && !string.IsNullOrEmpty(ucGridView_Fix.ucExportOpenXmlPassword))
        {
            Util.MsgBox(string.Format("Excel 密碼為 [<b>{0}</b>] ，請妥善保存！", ucGridView_Fix.ucExportOpenXmlPassword));
        }
        else
        {
            Util.MsgBox(string.Format("<b>GridViewCommand</b><hr>Cmd=[{0}]", e.CommandName));
        }
    }

    void ucGridView_Fix_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        Util.MsgBox(string.Format("<b>RowCommand</b><hr>Cmd=[{0}]<br>Keys=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
    }

    /// <summary>
    /// 「清除」按鈕事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //清除目前查詢條件表單
        Util.setControlClear(DivQryCondition, true);
        _QryResultSQL = "";
        DivQryResult.Visible = false;
    }

    /// <summary>
    /// 「查詢」按鈕事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQry_Click(object sender, EventArgs e)
    {
        //組合查詢條件
        StringBuilder sb = new StringBuilder();
        sb.Append(_QryBaseSQL).Append(" Where 0=0 ");

        sb.Append(Util.getDataQueryConditionSQL("CompID", "=", qryCompDeptUser.ucSelectedValue01));
        sb.Append(Util.getDataQueryConditionSQL("DeptID", "=", qryCompDeptUser.ucSelectedValue02));
        sb.Append(Util.getDataQueryConditionSQL("UserID", "=", qryCompDeptUser.ucSelectedValue03));
        sb.Append(Util.getDataQueryConditionSQL("UserID", "=", qryUserID));
        if (qryIsLikeUserName.Checked)
        {
            sb.Append(Util.getDataQueryConditionSQL("UserName", "%", qryUserName));
        }
        else
        {
            sb.Append(Util.getDataQueryConditionSQL("UserName", "=", qryUserName));
        }

        //排除系統管理員
        if (!string.IsNullOrEmpty(Util.getAppSetting("app://AdminUserID/")))
            sb.AppendFormat(" And UserID <> '{0}' ", Util.getAppSetting("app://AdminUserID/"));

        //排除非在職
        sb.AppendFormat(" And UserStatus in ('{0}') ", Util.getStringJoin(Util.getAppSetting("app://UserStatusValidList/").Split(','), "','"));

        _QryResultSQL = sb.ToString();
        ShowQryResult();
    }

    /// <summary>
    /// 顯示查詢結果
    /// </summary>
    protected void ShowQryResult()
    {
        //初始
        DivQryResult.Visible = true;
        UserInfo oUser = UserInfo.getUserInfo();

        //顯示欄位
        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        dicDisplay.Clear();
        dicDisplay.Add("UserID", "員編");
        dicDisplay.Add("UserName", "姓名");
        dicDisplay.Add("CompName", "公司");
        dicDisplay.Add("DeptName", "單位@L200");
        dicDisplay.Add("UserKindName", "類別");
        dicDisplay.Add("UserStatusName", "狀態");
        dicDisplay.Add("UserTel", "電話");
        dicDisplay.Add("UserMail", "信箱");
        ucGridView_Fix.ucDataDisplayDefinition = dicDisplay;

        //===== GridView_Roll =====
        //示範欄位捲動功能
        ucGridView_Roll.ucDBName = _DBName;
        ucGridView_Roll.ucDataQrySQL = _QryResultSQL;
        ucGridView_Roll.ucDataKeyList = _PKList;
        ucGridView_Roll.ucDataDisplayDefinition = dicDisplay;
        ucGridView_Roll.ucSortEnabled = true;
        ucGridView_Roll.ucDisplayOnly = true;
        ucGridView_Roll.ucSelectRowEnabled = true; //可整列選取

        ucGridView_Roll.ucFreezeHeaderEnabled = true;   //凍結表頭(會自動強制資料不折行)
        ucGridView_Roll.ucFreezeColQty = 3;             //凍結欄位(若不設定則只鎖定表頭)
        ucGridView_Roll.ucPageSize = 30;                //方便示範表頭鎖定效果
        ucGridView_Roll.Refresh(true);

        //===== GridView Fix =====
        //設定全域 PDF 版面(Optional)
        PDFHelper oPDF = PDFHelper.getPDFHelper(true);
        oPDF.PageSize = PDFHelper.PDFPageSize.A3; //紙張
        oPDF.IsLandscapePage = true;              //橫印
        oPDF.FontName = PDFHelper.PDFFont.Kai;    //字體

        //設定 PDF 匯出參數
        ucGridView_Fix.ucExportPdfEnabled = true;
        ucGridView_Fix.ucExportPdfTitle = "員工基本資料";
        ucGridView_Fix.ucExportPdfWaterMark = oUser.DefaultWatermarkText;
        ucGridView_Fix.ucExportPdfWaterMarkTextSize = 72;

        //設定 OpenXml 匯出參數
        ucGridView_Fix.ucExportOpenXmlEnabled = true;
        ucGridView_Fix.ucExportOpenXmlPassword = Util.getRandomCode();                      //亂數產生密碼
        ucGridView_Fix.ucExportOpenXmlHeader = "部門員工資料表";                              //表頭 2017.03.21
        ucGridView_Fix.ucExportOpenXmlFooter = "　●員工資料有密碼保護\n　●僅供內部使用";  //表尾 2017.03.21



        ucGridView_Fix.ucDBName = _DBName;
        ucGridView_Fix.ucDataQrySQL = _QryResultSQL;
        ucGridView_Fix.ucDataKeyList = _PKList;
        ucGridView_Fix.ucSortEnabled = true;

        //表頭欄位合併
        ucGridView_Fix.ucHeaderColSpanQtyList = "3,5";
        ucGridView_Fix.ucHeaderColSpanCaptionList = "識　別,一　般　資　訊";

        ucGridView_Fix.ucCmdBtnHorizontalAlign = HorizontalAlign.Center; //按鈕水平對齊 2016.07.28 新增
        ucGridView_Fix.ucSelectEnabled = true;
        ucGridView_Fix.ucSelectRowEnabled = true; //可整列選取
        ucGridView_Fix.ucPageSize = 100; //每頁筆數
        ucGridView_Fix.ucDataGroupKey = "DeptName"; //設定群組

        ucGridView_Fix.Refresh(true);

    }
}