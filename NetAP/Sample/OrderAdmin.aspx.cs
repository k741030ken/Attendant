using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;

public partial class Sample_OrderAdmin : SecurePage
{
    #region 頁面共用屬性
    //是否為主檔新增模式
    private bool _IsADD
    {
        get
        {
            if (ViewState["_IsADD"] == null) { ViewState["_IsADD"] = false; }
            return (bool)(ViewState["_IsADD"]);
        }
        set
        {
            ViewState["_IsADD"] = value;
        }
    }

    //主檔ID
    private string _MainID
    {
        get
        {
            if (ViewState["_MainID"] == null) { ViewState["_MainID"] = ""; }
            return (string)(ViewState["_MainID"]);
        }
        set
        {
            ViewState["_MainID"] = value;
        }
    }

    //明細Seq值
    private int _DetailSeq
    {
        get
        {
            if (ViewState["_DetailSeq"] == null) { ViewState["_DetailSeq"] = -1; }
            return (int)(ViewState["_DetailSeq"]);
        }
        set
        {
            ViewState["_DetailSeq"] = value;
        }
    }

    //Unit欄位的下拉清單來源
    private Dictionary<string, string> _dicUnit
    {
        get
        {
            if (ViewState["_dicUnit"] == null)
            {
                ViewState["_dicUnit"] = Util.getCodeMap(_DBName, "PODetail", "Unit");
            }
            return (Dictionary<string, string>)(ViewState["_dicUnit"]);
        }
        set
        {
            ViewState["_dicUnit"] = value;
        }
    }

    #endregion

    #region 頁面共用常數
    //資料庫來源
    private string _DBName = "NetSample";

    //計算主檔鍵值的AppKeyID
    private string _AppKeyID = "TestPO";

    private string[] _Main_KeyList = "POID".Split(',');

    private string[] _Detail_KeyList = "POID,POSeq".Split(',');

    private string _Main_BaseQrySQL = "Select POID ,PODate ,ShipDate ,TotAmt From PO ";

    private string _Detail_BaseQrySQL = @"Select POID ,POSeq ,ItemDesc ,ItemExpDate ,Unit ,UnitPrice ,Qty ,Subtotal
                                    ,cast(SubTotal as decimal) / cast( (Select TotAmt From PO Where POID = '{0}') as decimal) as ItemPercent
                                    ,CONVERT(char(7),ItemExpDate,102) as ItemExpMM
                                    ,'Seq[' + convert(varchar,POseq) + ']' as TipTitle
                                    ,'單價[' + convert(varchar,UnitPrice) + ']' as TipInfo 
                                    From PODetail Where POID = '{0}' ";

    //由於 _Detail_BaseQrySQL 內有子查詢，引用了多個 [From] 關鍵字，會造成 sp_GetPageDataCount 拆解時出錯 ，故另訂專用SQL 2017.02.10
    private string _Detail_BaseCountSQL = @"Select POID ,POSeq ,ItemDesc From PODetail Where POID = '{0}' "; 

    //重算指定主檔及其明細檔的加總值（若追求效能，應該採累加累減的作法，但 SQL Code的難度較高，易發生錯誤）
    private string _ReCal_BaseUpdSQL = @"Update PODetail Set Subtotal = UnitPrice * Qty Where POID = '{0}';
                                    Update PO Set TotAmt = (Select ISNULL(SUM(SubTotal),0) From PODetail Where POID = '{0}' ) 
                                    Where POID = '{0}' ";

    private string _ConfirmMsg = "確定刪除？";
    #endregion

    protected override void OnInitComplete(EventArgs e)
    {
        //示範 NetAP 提供的頁面強化屬性

        //PageIsMetaTags = false;             //頁面自訂 MetaTage
        //PageIsLock = true;                  //頁面鎖定
        //PageIsWatermark = true;             //頁面浮水印
        //PageIsAlertSessionTimeout = true;   //頁面逾時提醒
        //PageIsRefreshSessionFrame = true;   //保持 Session 不會逾時
        //PageIsRequestQueryStringLog = true; //記錄 Request.QueryString
        //PageIsRequestFormLog = true;        //記錄 Request.Form

        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //首次進入
            //強制顯示主檔清單
            _IsADD = false;
            _MainID = "";
            _DetailSeq = -1;

            Refresh(true);
        }

        //按鈕附加確認對話訊息
        Util.ConfirmBox(btnDetailBatchDelete, _ConfirmMsg);

        //彈出視窗 事件訂閱
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
        //主檔清單 事件訂閱
        ucGridMain.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridMain_RowCommand);

        //明細檔清單 訂閱 RowCommand 事件
        ucGridDetail1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridDetail_RowCommand);
        ucGridDetail2.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridDetail_RowCommand);

        //明細檔清單 需訂閱 GridViewCommand 事件
        ucGridDetail1.GridViewCommand += new Util_ucGridView.GridViewClick(ucGridDetail_GridViewCommand);
        ucGridDetail2.GridViewCommand += new Util_ucGridView.GridViewClick(ucGridDetail_GridViewCommand);

        //因為有些被包在 fmMain 內，又會彈出視窗的自訂控制項，由於執行期才會被實作，故無法直接用事件訂閱方式處理
        //偵測造成此次 PostBack 的事件控制項
        Control oCtrl = this.GetPostBackControl();
        if (oCtrl != null && oCtrl.ID == "btnClose")
        {
            if (oCtrl.Parent.Parent.Parent.NamingContainer.ID == "fmMain")
            {
                if (oCtrl.NamingContainer.GetType().BaseType.Name == "Util_ucModalPopup")
                {
                    Util.setJS_AlertDirtyData(fmMain);
                    ucGridDetail1.Refresh();
                    ucGridDetail2.Refresh();
                }
            }
        }

    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        //非 ucGridView 所引發的事件，需自行 Refresh 所需的 ucGridView
        Util.setJS_AlertDirtyData(fmMain);
        ucGridDetail1.Refresh();
        ucGridDetail2.Refresh();
    }

    /// <summary>
    /// 重新整理全頁
    /// <para>自動處理 fmMain / ucGridMain / ucGridDetail1 / ucGridDetail2 之間的連動關係</para>
    /// </summary>
    /// <param name="InitGridMain">初始主檔清單</param>
    /// <param name="InitGridDetail">初始明細清單</param>
    protected void Refresh(bool InitGridMain = false, bool InitGridDetail = false)
    {

        #region 設定主檔清單
        //「主檔清單」屬性
        ucGridMain.ucDBName = _DBName;
        ucGridMain.ucDataQrySQL = _Main_BaseQrySQL;
        ucGridMain.ucDataKeyList = _Main_KeyList;
        ucGridMain.ucDefSortExpression = "POID";
        ucGridMain.ucDefSortDirection = "Desc";
        Dictionary<string, string> oDicMain = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicMain.Clear();
        oDicMain.Add("POID", "訂單號碼@C");
        oDicMain.Add("PODate", "下單日期@D");
        oDicMain.Add("ShipDate", "出貨日期@D");
        oDicMain.Add("TotAmt", "訂單總額@N2");
        ucGridMain.ucDataDisplayDefinition = oDicMain;

        //設定其他屬性
        ucGridMain.ucPageSizeList = new int[] { 5, 10, 15 };   //自訂分頁清單(預設 10, 20, 30, 50, 100 )
        ucGridMain.ucPageSize = 5;                             //設定每頁筆數(預設 10 )
        //ucGridMain.ucHeaderCssClass += " Util_Left";           //標題列強制靠左(預設 置中 )
        ucGridMain.ucAddEnabled = true;
        ucGridMain.ucCopyEnabled = true;
        ucGridMain.ucDeleteEnabled = true;
        ucGridMain.ucSelectEnabled = true;

        #endregion

        #region 設定明細檔清單
        //「明細檔清單1」屬性
        ucGridDetail1.ucDBName = _DBName;
        ucGridDetail1.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
        ucGridDetail1.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
        ucGridDetail1.ucDataKeyList = _Detail_KeyList;
        ucGridDetail1.ucDefSortExpression = "POSeq";
        ucGridDetail1.ucDefSortDirection = "Asc";
        ucGridDetail1.ucSortDisabledFieldList = "ItemPercent".Split(',');  // [佔單比例] 是動態計算欄位，不適用自訂排序 2017.02.13

        Dictionary<string, string> oDicDetail = new Dictionary<string, string>();
        //設定顯示欄位，可用格式請參考電子書
        oDicDetail.Clear();
        oDicDetail.Add("POSeq", "Seq@R");
        oDicDetail.Add("ItemDesc", "項目說明@L");
        oDicDetail.Add("ItemExpDate", "到期日期@D");
        oDicDetail.Add("Unit", "單位@C60," + Util.getJSON(_dicUnit));
        oDicDetail.Add("UnitPrice", "單　價@N0");
        oDicDetail.Add("Qty", "數　量@N030");
        oDicDetail.Add("Subtotal", "小　　計@N2");
        oDicDetail.Add("ItemPercent", "佔單比例@P2");

        ucGridDetail1.ucDataDisplayDefinition = oDicDetail;

        //指定Tooltip 範本（預設是 Simple）
        //ucGridDetail1.ucHoverTooltipTemplete = new HoverTooltipTemplete(HoverTooltipTemplete.TooltipType.Dialog);
        //自訂Tooltip 範本版型
        string strTipHtml = "<div style='color:yellow;background-color:#DF0024;padding:3px;'>{1}</div>";
        ucGridDetail1.ucHoverTooltipTemplete = new HoverTooltipTemplete(strTipHtml);

        //指定滑鼠移到那個欄位時會顯示 Tooltip
        //ucGridDetail1.ucDataDisplayToolTipDefinition = new Dictionary<string, string>() { { "ItemDesc", "TipInfo" } };  //不出現標題
        ucGridDetail1.ucDataDisplayToolTipDefinition = new Dictionary<string, string>() { { "ItemDesc", "TipTitle,TipInfo" } }; //出現標題

        //設定其他屬性
        ucGridDetail1.ucPrintEnabled = true;
        ucGridDetail1.ucExportAllField = false;
        ucGridDetail1.ucExportOpenXmlEnabled = true;

        ucGridDetail1.ucAddEnabled = true;
        ucGridDetail1.ucCheckEnabled = true;
        ucGridDetail1.ucCopyEnabled = true;
        ucGridDetail1.ucSelectEnabled = false;
        ucGridDetail1.ucEditEnabled = true;
        ucGridDetail1.ucDeleteEnabled = true;

        //「明細檔清單2」屬性
        ucGridDetail2.ucDBName = _DBName;
        ucGridDetail2.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
        ucGridDetail2.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
        ucGridDetail2.ucDataKeyList = _Detail_KeyList;
        ucGridDetail2.ucDefSortExpression = "POSeq";
        ucGridDetail2.ucDefSortDirection = "Asc";
        ucGridDetail2.ucSortDisabledFieldList = "ItemPercent".Split(',');  // [佔單比例] 是動態計算欄位，不適用自訂排序 2017.02.13
        ucGridDetail2.ucDataDisplayDefinition = oDicDetail;

        //設定線上編輯欄位
        Dictionary<string, string> oDicDetailEdit = new Dictionary<string, string>();
        oDicDetailEdit.Clear();

        //設定編輯欄位可用的自訂屬性
        Dictionary<string, string> oDicProperty = new Dictionary<string, string>();
        oDicProperty.Clear();
        oDicProperty.Add("ucIsRequire", "True"); //布林值，可寫True/true/False/false(系統會自動轉換)

        oDicDetailEdit.Add("ItemDesc", "TextBox@" + Util.getJSON(oDicProperty));        //使用 TextBox
        oDicDetailEdit.Add("ItemExpDate", "Calendar@" + Util.getJSON(oDicProperty));    //日期欄位，可用 TextBox 或 Calendar 進行編輯


        //示範 Unit 欄位使用 DropDownList
        //oDicDetailEdit.Add("Unit", "DropDownList@" + Util.getJSON(Util.getDictionary(_dicUnit)));           

        //示範 Unit 欄位使用RadioList  2016.04.28
        oDicProperty.Clear();
        oDicProperty.Add("ucIsRequire", "True");
        oDicProperty.Add("ucRadioListHeight", "100");
        oDicProperty.Add("ucIsSearchBoxEnabled", "False");
        oDicProperty.Add("ucSourceDictionary", Util.getJSON(_dicUnit));
        oDicDetailEdit.Add("Unit", "RadioList@" + Util.getJSON(oDicProperty));

        oDicProperty.Clear();
        oDicProperty.Add("ucIsRequire", "True");
        oDicProperty.Add("ucRegExp", Util.getRegExp(Util.CommRegExp.PositiveInteger));  //限定數值範圍為正整數
        oDicDetailEdit.Add("UnitPrice", "TextBox@" + Util.getJSON(oDicProperty));　     //若未加此自訂屬性，ucGridView會自動判斷需為[數字]才能通過
        oDicDetailEdit.Add("Qty", "TextBox@" + Util.getJSON(oDicProperty));             //若未加此自訂屬性，ucGridView會自動判斷需為[數字]才能通過
        ucGridDetail2.ucDataEditDefinition = oDicDetailEdit;

        //設定要顯示加總值的欄位
        //ucGridView 只會動態加總同一頁的資料，故使用清單請「不要」分頁，以免加總值與實際資料不合
        ucGridDetail2.ucPageSize = 100;
        ucGridDetail2.ucDataSubtotalList = "Subtotal,ItemPercent".Split(',');

        //設定其他屬性
        ucGridDetail2.ucAddEnabled = true;
        ucGridDetail2.ucCheckEnabled = true;
        ucGridDetail2.ucCopyEnabled = true;
        ucGridDetail2.ucSelectEnabled = false;
        ucGridDetail2.ucEditEnabled = true;
        ucGridDetail2.ucDeleteEnabled = true;
        ucGridDetail2.ucDeleteAllEnabled = true;

        //由於 ucGridDetail1 與 ucGridDetail2 為相關連資料(資料來源相同)
        //則任一清單資料異動時，其相關連的清單需自動同步重新整理
        ucGridDetail1.ucSyncRefreshGridViewIDList = ucGridDetail2.ID.Split(',');
        ucGridDetail2.ucSyncRefreshGridViewIDList = ucGridDetail1.ID.Split(',');
        #endregion

        if (_IsADD)
        {
            //主檔新增模式
            DivMainList.Visible = false;
            DivMainSingle.Visible = true;
            DivDetailArea.Visible = true;
            fmMainRefresh();

            btnDetailBatchEdit.Visible = false;
            btnDetailBatchDelete.Visible = false;
            ucGridDetail1.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
            ucGridDetail1.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
            ucGridDetail1.ucAddEnabled = false;
            ucGridDetail1.Refresh(InitGridDetail);
            ucGridDetail2.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
            ucGridDetail2.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
            ucGridDetail2.ucAddEnabled = false;
            ucGridDetail2.Refresh(InitGridDetail);
        }
        else
        {
            //非主檔新增模式
            if (string.IsNullOrEmpty(_MainID))
            {
                //顯示主檔清單
                DivMainList.Visible = true;
                DivMainSingle.Visible = false;
                DivDetailArea.Visible = false;
                ucGridMain.Refresh(InitGridMain); //重新整理「主檔清單」
            }
            else
            {
                //顯示指定主檔及其明細
                DivMainList.Visible = false;
                DivMainSingle.Visible = true;
                DivDetailArea.Visible = true;
                fmMainRefresh();

                btnDetailBatchEdit.Visible = true;
                btnDetailBatchDelete.Visible = true;
                ucGridDetail1.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
                ucGridDetail1.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
                ucGridDetail1.Refresh(InitGridDetail); //重新整理「明細檔清單1」

                ucGridDetail2.ucDataCountSQL = string.Format(_Detail_BaseCountSQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
                ucGridDetail2.ucDataQrySQL = string.Format(_Detail_BaseQrySQL, (string.IsNullOrEmpty(_MainID)) ? "---" : _MainID);
                ucGridDetail2.Refresh(InitGridDetail); //重新整理「明細檔清單2」
            }
        }
    }

    #region 查詢相關
    protected void btnQry_Click(object sender, EventArgs e)
    {
        ucGridMain.ucDataQrySQL = _Main_BaseQrySQL + " Where 0=0 " + Util.getDataQueryConditionSQL("PODate", "Between", QryDate1, QryDate2);
        ucGridMain.Refresh(true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        QryDate1.ucSelectedDate = "";
        QryDate2.ucSelectedDate = "";
        ucGridMain.Refresh();
    }
    #endregion

    #region 主檔相關

    void ucGridMain_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();

        //取得相關Key值
        _MainID = e.DataKeys[0];
        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                _IsADD = true;
                _MainID = "";
                Refresh(true, true);
                break;
            case "cmdCopy":
                _IsADD = false;
                var AppKeyResult = Util.getAppKey(_DBName, _AppKeyID);
                string strPOID = AppKeyResult.Item1[0];
                string strErrMsg = AppKeyResult.Item2;
                if (!string.IsNullOrEmpty(strErrMsg))
                {
                    Util.MsgBox(strErrMsg);
                    return;
                }

                string strSQL = Util.getDataCopySQL(_DBName, "POID".Split(','), e.DataKeys[0].Split(','), strPOID.Split(','), "PO,PODetail".Split(','));
                try
                {
                    db.ExecuteNonQuery(CommandType.Text, strSQL, tx);
                    tx.Commit();
                    _MainID = strPOID; //新訂單編號
                    Util.NotifyMsg("訂單複製成功，可直接進行相關編修", Util.NotifyKind.Success);
                }
                catch
                {
                    tx.Rollback();
                    Util.NotifyMsg("訂單複製失敗", Util.NotifyKind.Error);
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                    tx.Dispose();
                }
                Refresh(false, true);
                break;
            case "cmdDelete":
                _IsADD = false;
                _MainID = "";
                _DetailSeq = -1;
                sb.Reset();
                sb.AppendStatement("Delete PO       Where POID = ").AppendParameter("POID", e.DataKeys[0]);
                sb.AppendStatement("Delete PODetail Where POID = ").AppendParameter("POID", e.DataKeys[0]);
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    tx.Commit();
                    Util.NotifyMsg("主檔及相關明細檔刪除成功", Util.NotifyKind.Success);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                    tx.Dispose();
                }
                break;
            case "cmdSelect":
                _IsADD = false;
                Refresh(false, true);
                break;
            default:
                Util.MsgBox(string.Format("cmd=[{0}],key=[{1}]", e.CommandName, Util.getStringJoin(e.DataKeys)));
                break;
        }
    }

    protected void fmMainRefresh()
    {
        if (string.IsNullOrEmpty(_MainID))
        {
            if (_IsADD)
            {
                //新增訂單模式
                fmMain.ChangeMode(FormViewMode.Insert);
                //設定唯讀欄位
                Util.setReadOnly("txtCompID", true);
                Util.setReadOnly("txtDeptID", true);
                Util.setReadOnly("txtUserID", true);
                Util.setReadOnly("txtCompName", true);
                Util.setReadOnly("txtDeptName", true);
                Util.setReadOnly("txtUserName", true);
                fmMain.DataSource = null;
                fmMain.DataBind();
            }
        }
        else
        {
            //顯示既有訂單
            DbHelper db = new DbHelper(_DBName);
            DataTable dt = null;
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement("Select * From PO Where POID = ").AppendParameter("POID", _MainID);
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            fmMain.ChangeMode(FormViewMode.Edit);
            fmMain.DataSource = dt;
            fmMain.DataBind();
        }
        Util.setJS_AlertDirtyData(fmMain);
    }

    protected void fmMain_DataBound(object sender, EventArgs e)
    {
        Util_ucTextBox oTxt;
        Util_ucUserPicker oUserPicker;
        Util_ucDatePicker oDatePicker;

        DataRow dr = null;

        //「新增」模式，即 InsertItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Insert)
        {
            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnMainInsert");

            //設定[下訂人員]相關欄位 2014.10.16 改用 Util_ucUserPicker 元件
            oUserPicker = (Util_ucUserPicker)fmMain.FindControl("UserPicker1");
            if (oUserPicker != null)
            {
                oUserPicker.ucIsRequire = true;
                oUserPicker.ucDefUserIDList = UserInfo.getUserInfo().UserID;
                oUserPicker.Refresh();
            }
        }

        //「編輯」模式，即 EditItemTemplate 範本
        if (fmMain.CurrentMode == FormViewMode.Edit)
        {
            dr = ((DataTable)fmMain.DataSource).Rows[0];

            //自訂檢核失敗時的JS提醒訊息
            Util.setJS_AlertPageNotValid("btnMainUpdate");

            oTxt = (Util_ucTextBox)fmMain.FindControl("POID");
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["POID"].ToString();
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)fmMain.FindControl("PODate");
            if (oTxt != null)
            {
                oTxt.ucTextData = string.Format("{0:yyyy\\/MM\\/dd}", dr["PODate"]);
                oTxt.ucIsReadOnly = true;
                oTxt.Refresh();
            }

            oTxt = (Util_ucTextBox)fmMain.FindControl("Remark");
            if (oTxt != null)
            {
                oTxt.ucTextData = dr["Remark"].ToString();
            }

            //設定[下訂人員]相關欄位
            oUserPicker = (Util_ucUserPicker)fmMain.FindControl("UserPicker");
            if (oUserPicker != null)
            {
                oUserPicker.ucIsRequire = true;
                oUserPicker.ucDefUserIDList = dr["POUser"].ToString();
                oUserPicker.Refresh();
            }

            oDatePicker = (Util_ucDatePicker)fmMain.FindControl("ShipDate");
            if (oDatePicker != null)
            {
                oDatePicker.ucDefaultSelectedDate = DateTime.Parse(dr["ShipDate"].ToString());
            }

            Button oBtn = (Button)fmMain.FindControl("btnMainDelete");
            if (oBtn != null)
            {
                Util.ConfirmBox(oBtn, _ConfirmMsg);
            }
        }
    }

    /// <summary>
    /// 確認刪除主檔(自動刪除相關明細檔)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMainDelete_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Delete PO       Where POID = ").AppendParameter("POID", _MainID);
        sb.AppendStatement("Delete PODetail Where POID = ").AppendParameter("POID", _MainID);

        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            tx.Commit();
            _MainID = "";
            _DetailSeq = -1;
            Refresh(true, true);
            Util.NotifyMsg("主檔及相關明細檔刪除成功", Util.NotifyKind.Success);
        }
        catch
        {
            tx.Rollback();
            Util.NotifyMsg("主檔及相關明細檔刪除失敗", Util.NotifyKind.Error);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }

    /// <summary>
    /// 確認新增主檔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMainInsert_Click(object sender, EventArgs e)
    {
        //取得表單輸入資料
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);
        //計算訂單鍵值
        var AppKeyResult = Util.getAppKey(_DBName, _AppKeyID);
        string strPOID = AppKeyResult.Item1[0];
        string strErrMsg = AppKeyResult.Item2;
        if (!string.IsNullOrEmpty(strErrMsg))
        {
            Util.MsgBox(strErrMsg);
            return;
        }
        else
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            sb.AppendStatement("Insert PO ");
            sb.Append("(POID,PODate,ShipDate,POUser,Remark,TotAmt)");
            sb.Append(" Values (").AppendParameter("POID", strPOID);
            sb.Append("        ,").AppendDbDateTime();
            sb.Append("        ,").AppendParameter("ShipDate", oDic["ShipDate"]);
            sb.Append("        ,").AppendParameter("POUser", oDic["UserPicker"]);
            sb.Append("        ,").AppendParameter("Remark", oDic["Remark"]);
            sb.Append("        ,0)");

            try
            {
                if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
                {
                    _IsADD = false;
                    _MainID = strPOID;
                    Util.NotifyMsg(string.Format("訂單[{0}]新增成功，可進行後續編修。", strPOID), Util.NotifyKind.Success);
                    Refresh(true, true);
                }
                else
                {
                    Util.NotifyMsg("新增失敗", Util.NotifyKind.Error);
                }
            }
            catch
            {
                Util.NotifyMsg("新增錯誤，請檢查資料是否重複", Util.NotifyKind.Error);
            }
        }

    }

    /// <summary>
    /// 取消新增主檔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMainInsertCancel_Click(object sender, EventArgs e)
    {
        _IsADD = false;
        Refresh();
    }

    /// <summary>
    /// 確認更新主檔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMainUpdate_Click(object sender, EventArgs e)
    {
        //取得表單輸入資料
        Dictionary<string, string> oDic = Util.getControlEditResult(fmMain);

        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.AppendStatement("Update PO Set ");
        sb.Append("  ShipDate = ").AppendParameter("ShipDate", oDic["ShipDate"]);
        sb.Append(", POUser   = ").AppendParameter("POUser", oDic["UserPicker"]);
        sb.Append(", Remark   = ").AppendParameter("Remark", oDic["Remark"]);
        sb.Append("  Where POID = ").AppendParameter("POID", _MainID);

        try
        {
            if (db.ExecuteNonQuery(sb.BuildCommand()) >= 0)
            {
                Util.NotifyMsg("更新成功", Util.NotifyKind.Success);
                _IsADD = false;
                _MainID = "";
                Refresh(false, true);
            }
        }
        catch (Exception ex)
        {
            string strErrMsg = "更新主檔失敗";
            //將錯誤記錄到 Log 模組
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            Util.NotifyMsg(strErrMsg, Util.NotifyKind.Error);
            //錯誤發生時，因為頁面需保留目前主檔表單輸入的內容，故只更新明細清單清單
            ucGridDetail1.Refresh();
            ucGridDetail2.Refresh();
        }
    }

    /// <summary>
    /// 取消更新主檔
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMainUpdateCancel_Click(object sender, EventArgs e)
    {
        _MainID = "";
        Refresh();
    }

    #endregion

    #region 明細檔相關

    void ucGridDetail_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        //承接明細檔　GridView　單一按鈕事件
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        string[] oDataKeys = e.DataKeys;
        if (oDataKeys != null)
        {
            _MainID = oDataKeys[0];
            _DetailSeq = int.Parse("0" + oDataKeys[1].ToString());
        }

        //初始彈出視窗
        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupWidth = 550;
        ucModalPopup1.ucPopupHeight = 280;

        //處理自訂命令，AP 可視需要自行增加想要的 CommandName
        switch (e.CommandName)
        {
            case "cmdAdd":
                //新增模式
                fmDetail.ChangeMode(FormViewMode.Insert);
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);

                ucModalPopup1.ucPopupHeader = "新增明細";
                ucModalPopup1.ucPanelID = pnlDetailForm.ID;
                ucModalPopup1.Show();
                break;
            case "cmdCopy":
                //複製模式
                fmDetail.ChangeMode(FormViewMode.Insert);
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);
                sb.Reset();
                sb.AppendStatement("Select * From PODetail Where 0=0 ");
                sb.Append(" And POID = ").AppendParameter("POID", _MainID);
                sb.Append(" And POSeq = ").AppendParameter("POSeq", _DetailSeq);
                DataRow dr = db.ExecuteDataSet(sb.BuildCommand()).Tables[0].Rows[0];
                ((Util_ucTextBox)fmDetail.FindControl("ItemDesc")).ucTextData = dr["ItemDesc"].ToString();
                ((Util_ucDatePicker)fmDetail.FindControl("ItemExpDate")).ucSelectedDate = string.Format("{0:yyyy\\/MM\\/dd}", dr["ItemExpDate"]);
                ((Util_ucTextBox)fmDetail.FindControl("UnitPrice")).ucTextData = dr["UnitPrice"].ToString();
                ((Util_ucTextBox)fmDetail.FindControl("Qty")).ucTextData = dr["Qty"].ToString();
                ((Util_ucCommSingleSelect)fmDetail.FindControl("Unit")).ucSelectedID = dr["Unit"].ToString();
                ((Util_ucCommSingleSelect)fmDetail.FindControl("Unit")).Refresh();

                ucModalPopup1.ucPopupHeader = "複製明細";
                ucModalPopup1.ucPanelID = pnlDetailForm.ID;
                ucModalPopup1.Show();
                break;
            case "cmdEdit":
                sb.Reset();
                sb.AppendStatement("Select * From PODetail Where 0=0 ");
                sb.Append(" And POID = ").AppendParameter("POID", _MainID);
                sb.Append(" And POSeq = ").AppendParameter("POSeq", _DetailSeq);

                fmDetail.ChangeMode(FormViewMode.Edit);
                fmDetail.DataSource = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                fmDetail.DataBind();
                Util.setJS_AlertDirtyData(fmDetail);

                ucModalPopup1.ucPopupHeader = "編輯明細";
                ucModalPopup1.ucPanelID = pnlDetailForm.ID;
                ucModalPopup1.Show();
                break;
            case "cmdDelete":
                sb.Reset();
                //刪除明細
                sb.AppendStatement("Delete PODetail Where 0=0 ");
                sb.Append(" And POID  =").AppendParameter("POID", _MainID);
                sb.Append(" And POSeq =").AppendParameter("POSeq", _DetailSeq);
                //重新計算加總值
                sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

                DbConnection cn = db.OpenConnection();
                DbTransaction tx = cn.BeginTransaction();
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    tx.Commit();
                    _DetailSeq = -1;
                    Util.NotifyMsg("明細刪除成功", Util.NotifyKind.Success);
                }
                catch
                {
                    tx.Rollback();
                    Util.NotifyMsg("明細刪除失敗", Util.NotifyKind.Error);
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                    tx.Dispose();
                }
                break;
            default:
                //未定義的命令
                Util.MsgBox(string.Format(RS.Resources.Msg_Undefined1, e.CommandName));
                break;
        }
        fmMainRefresh();
    }

    void ucGridDetail_GridViewCommand(object sender, Util_ucGridView.GridViewEventArgs e)
    {
        if (e.CommandName.Contains("cmdExport")) 
        {
            ucGridDetail2.Refresh();
            return;
        }

        //承接明細檔　GridView　線上編輯模式結果
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = e.DataTable;
        if (dt == null || dt.Rows.Count <= 0)
        {
            Refresh();
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (e.CommandName)
                {
                    case "cmdDeleteAll":
                        sb.AppendStatement("Delete PODetail Where 0=0 ");
                        sb.Append(" And POID  =").AppendParameter("POID" + i, dt.Rows[i][0].ToString().Split(',')[0]);
                        sb.Append(" And POSeq =").AppendParameter("POSeq" + i, dt.Rows[i][0].ToString().Split(',')[1]);
                        break;
                    case "cmdUpdateAll":
                        sb.AppendStatement("Update PODetail Set POID = POID ");
                        for (int j = 1; j < dt.Columns.Count; j++)
                        {
                            sb.Append(" ," + dt.Columns[j].ColumnName + " = ").AppendParameter(dt.Columns[j].ColumnName + i, dt.Rows[i][j].ToString());
                        }
                        sb.Append(" Where 0=0 ");
                        sb.Append(" And POID  =").AppendParameter("POID" + i, dt.Rows[i][0].ToString().Split(',')[0]);
                        sb.Append(" And POSeq =").AppendParameter("POSeq" + i, dt.Rows[i][0].ToString().Split(',')[1]);
                        break;
                    default:
                        break;
                }
            }
            //重新計算加總值
            sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                Util.NotifyMsg("處理成功", Util.NotifyKind.Success);
                fmMainRefresh();
            }
            catch
            {
                tx.Rollback();
                Util.NotifyMsg("處理失敗", Util.NotifyKind.Error);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }

        }
    }


    protected void fmDetail_DataBound(object sender, EventArgs e)
    {
        //初始
        Util_ucTextBox oText;
        Util_ucCommSingleSelect oDrpList;
        Util_ucDatePicker oDatePicker;
        Button oBtn;
        string strObjID;

        DataRow dr = null;

        if (fmDetail.CurrentMode == FormViewMode.Edit)
        {
            dr = ((DataTable)fmDetail.DataSource).Rows[0];
            strObjID = "ItemDesc";
            oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
            oText.ucTextData = dr["ItemDesc"].ToString();
            oText.Refresh();

            strObjID = "ItemExpDate";
            oDatePicker = (Util_ucDatePicker)fmDetail.FindControl(strObjID);
            oDatePicker.ucSelectedDate = string.Format("{0:yyyy\\/MM\\/dd}", dr["ItemExpDate"]);
            oDatePicker.Refresh();

            oBtn = (Button)Util.FindControlEx(fmDetail, "btnDetailUpdate");
            if (oBtn != null)
            {
                Util.setJS_AlertPageNotValid(oBtn.ID);
            }

        }

        strObjID = "Unit";
        oDrpList = (Util_ucCommSingleSelect)fmDetail.FindControl(strObjID);
        oDrpList.ucSourceDictionary = Util.getDictionary(_dicUnit);
        if (dr != null)
        {
            oDrpList.ucSelectedID = dr["Unit"].ToString();
        }
        oDrpList.Refresh();

        strObjID = "UnitPrice";
        oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
        oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
        oText.ucIsActiveIME = false;
        if (dr != null)
        {
            oText.ucTextData = dr["UnitPrice"].ToString();
        }
        oText.Refresh();

        strObjID = "Qty";
        oText = (Util_ucTextBox)fmDetail.FindControl(strObjID);
        oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
        oText.ucIsActiveIME = false;
        if (dr != null)
        {
            oText.ucTextData = dr["Qty"].ToString();
        }
        oText.Refresh();
    }

    protected void fmDetailBatch_DataBound(object sender, EventArgs e)
    {
        //初始
        Util_ucTextBox oText;
        Util_ucCommSingleSelect oDrpList;
        Util_ucDatePicker oDate;
        Button oBtn;
        string strObjID;

        strObjID = "ItemDesc";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucIsVisibility = true;
        oText.ucIsRequire = true;
        oText.Refresh();

        strObjID = "ItemExpDate";
        oDate = (Util_ucDatePicker)fmDetailBatch.FindControl(strObjID);
        oDate.ucIsVisibility = true;
        oDate.ucIsRequire = true;
        oDate.Refresh();

        strObjID = "Unit";
        oDrpList = (Util_ucCommSingleSelect)fmDetailBatch.FindControl(strObjID);
        oDrpList.ucSourceDictionary = Util.getDictionary(_dicUnit);
        oDrpList.ucIsVisibility = true;
        oDrpList.ucIsRequire = true;
        oDrpList.Refresh();

        strObjID = "UnitPrice";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
        oText.ucIsVisibility = true;
        oText.ucIsRequire = true;
        oText.Refresh();

        strObjID = "Qty";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucRegExp = Util.getRegExp(Util.CommRegExp.PositiveInteger);
        oText.ucIsVisibility = true;
        oText.ucIsRequire = true;
        oText.Refresh();

        Util_ucToggleControlVisibility oToggle = (Util_ucToggleControlVisibility)fmDetailBatch.FindControl("ucToggleControlVisibility1");
        if (oToggle != null)
        {
            oToggle.ucParentControlID = fmDetailBatch.ID;
            oToggle.Refresh();
        }

        Util.setJS_AlertDirtyData(fmDetailBatch);

        oBtn = (Button)Util.FindControlEx(fmDetailBatch, "btnDetailBatchUpdate");
        if (oBtn != null)
        {
            Util.setJS_AlertPageNotValid(oBtn.ID);
        }
    }

    /// <summary>
    /// 確認新增明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailInsert_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmDetail);

        if (!string.IsNullOrEmpty(_MainID))
        {
            //取出目前 PODetail最後一筆的 POSeq 備用(若無資料會傳回 -1)
            int intLastPOSeq = Util.getLastKeySeqNo(_DBName, "PODetail", "POID,POSeq".Split(','), _MainID.Split(','));
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Reset();
            //新增明細
            sb.AppendStatement("Insert PODetail ");
            sb.Append("(POID,POSeq,ItemDesc,ItemExpDate,Unit,UnitPrice,Qty,Subtotal) ");
            sb.Append(" Values (").AppendParameter("POID", _MainID);
            sb.Append("        ,").AppendParameter("POSeq", (intLastPOSeq >= 0) ? intLastPOSeq + 1 : 1);
            sb.Append("        ,").AppendParameter("ItemDesc", oDic["ItemDesc"]);
            sb.Append("        ,").AppendParameter("ItemExpDate", oDic["ItemExpDate"]);
            sb.Append("        ,").AppendParameter("Unit", oDic["Unit"]);
            sb.Append("        ,").AppendParameter("UnitPrice", oDic["UnitPrice"]);
            sb.Append("        ,").AppendParameter("Qty", oDic["Qty"]);
            sb.Append("        ,").AppendParameter("Subtotal", int.Parse(oDic["UnitPrice"]) * int.Parse(oDic["Qty"]));
            sb.Append(")");

            //重新計算加總值
            sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                _DetailSeq = (intLastPOSeq >= 0) ? intLastPOSeq + 1 : 1;
                Util.NotifyMsg("明細新增成功", Util.NotifyKind.Success);
                Refresh(false, true);
            }
            catch
            {
                tx.Rollback();
                Util.NotifyMsg("明細新增失敗", Util.NotifyKind.Error);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
    }

    /// <summary>
    /// 取消新增明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailInsertCancel_Click(object sender, EventArgs e)
    {
        ucModalPopup1.Hide();
        Refresh();
    }

    /// <summary>
    /// 確認更新明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailUpdate_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> oDic = Util.getControlEditResult(fmDetail);

        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        //更新明細
        sb.AppendStatement("Update PODetail Set ");
        sb.Append("  ItemDesc = ").AppendParameter("ItemDesc", oDic["ItemDesc"]);
        sb.Append(", ItemExpDate = ").AppendParameter("ItemExpDate", oDic["ItemExpDate"]);
        sb.Append(", Unit = ").AppendParameter("Unit", oDic["Unit"]);
        sb.Append(", UnitPrice   = ").AppendParameter("UnitPrice", oDic["UnitPrice"]);
        sb.Append(", Qty   = ").AppendParameter("Qty", oDic["Qty"]);
        sb.Append(" Where 0=0 ");
        sb.Append(" And POID  =").AppendParameter("POID", _MainID);
        sb.Append(" And POSeq =").AppendParameter("POSeq", _DetailSeq);

        //重新計算加總值
        sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            tx.Commit();
            Util.NotifyMsg("明細更新成功", Util.NotifyKind.Success);
            Refresh();
        }
        catch
        {
            tx.Rollback();
            Util.NotifyMsg("明細更新失敗", Util.NotifyKind.Error);
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }

    /// <summary>
    /// 取消更新明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailUpdateCancel_Click(object sender, EventArgs e)
    {
        //若是利用「彈出視窗」模式作編輯
        ucModalPopup1.Hide();
        Refresh();
    }

    /// <summary>
    /// 明細整批編輯
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailBatchEdit_Click(object sender, EventArgs e)
    {
        //Util.MsgBox(string.Format("選取資料筆數：[{0}]", ucGridDetail1.ucCheckKeyList.Count()));
        ViewState["ucCheckedKeyList"] = new string[] { };
        if (ucGridDetail1.ucCheckedKeyList.Count() < 1)
        {
            Util.NotifyMsg(RS.Resources.Msg_NoDataToModify);
        }
        else
        {
            ViewState["ucCheckedKeyList"] = ucGridDetail1.ucCheckedKeyList;
            fmDetailBatch.ChangeMode(FormViewMode.Insert);
            fmDetailBatch.DataBind();

            ucModalPopup1.Reset();
            ucModalPopup1.ucPopupWidth = 430;
            ucModalPopup1.ucPopupHeight = 320;
            ucModalPopup1.ucPopupHeader = "明細批次編輯";
            ucModalPopup1.ucPanelID = pnlDetailBatchForm.ID;
            ucModalPopup1.Show();
        }
        Refresh();
    }

    /// <summary>
    /// 明細整批刪除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailBatchDelete_Click(object sender, EventArgs e)
    {
        string[] CheckedKeyList = ucGridDetail1.ucCheckedKeyList;
        if (CheckedKeyList.Count() < 1)
        {
            Util.NotifyMsg(RS.Resources.Msg_NoDataToModify);
        }
        else
        {
            DbHelper db = new DbHelper(_DBName);
            CommandHelper sb = db.CreateCommandHelper();
            string[] oKey;
            sb.Reset();
            //批次明細刪除(SQL Parameter名稱需 Unique)
            for (int i = 0; i < CheckedKeyList.Count(); i++)
            {
                oKey = CheckedKeyList[i].Split(',');
                sb.AppendStatement("Delete PODetail Where 0=0 ");
                sb.Append(" And POID  =").AppendParameter("POID" + i, oKey[0]);
                sb.Append(" And POSeq =").AppendParameter("POSeq" + i, oKey[1]);
            }
            //重新計算加總值
            sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                _DetailSeq = -1;
                Util.NotifyMsg("[選取項目]刪除成功", Util.NotifyKind.Success);
            }
            catch
            {
                tx.Rollback();
                Util.NotifyMsg("[選取項目]刪除失敗", Util.NotifyKind.Error);
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
        fmMainRefresh();
        Refresh(true, true);
    }

    /// <summary>
    /// 確認明細整批更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailBatchUpdate_Click(object sender, EventArgs e)
    {
        //根據取得的鍵值清單及套用項目，整批更新明細
        string[] CheckedKeyList = (string[])ViewState["ucCheckedKeyList"];
        if (CheckedKeyList.Count() < 1)
        {
            Util.NotifyMsg(RS.Resources.Msg_NoDataToModify);
        }
        else
        {
            Dictionary<string, string> oDic = Util.getControlEditResult(fmDetailBatch);
            if (oDic.Count == 0)
            {
                Util.NotifyMsg(RS.Resources.Msg_NoDataToModify);
            }
            else
            {
                DbHelper db = new DbHelper(_DBName);
                CommandHelper sb = db.CreateCommandHelper();
                string[] oKey;
                sb.Reset();

                //批次明細更新(SQL Parameter名稱需 Unique)
                for (int i = 0; i < CheckedKeyList.Count(); i++)
                {
                    //取單筆鍵值
                    oKey = CheckedKeyList[i].Split(',');
                    //組合更新用SQL (記得加上Server端的資料檢核，確認後才允許更新)
                    sb.AppendStatement("Update PODetail Set POID = POID ");

                    if (oDic.ContainsKey("ItemDesc") && !string.IsNullOrEmpty(oDic["ItemDesc"]))
                        sb.Append(", ItemDesc = ").AppendParameter("ItemDesc" + i, oDic["ItemDesc"]);

                    if (oDic.ContainsKey("ItemExpDate") && !string.IsNullOrEmpty(oDic["ItemExpDate"]))
                        sb.Append(", ItemExpDate = ").AppendParameter("ItemExpDate" + i, oDic["ItemExpDate"]);

                    if (oDic.ContainsKey("Unit") && !string.IsNullOrEmpty(oDic["Unit"]))
                        sb.Append(", Unit = ").AppendParameter("Unit" + i, oDic["Unit"]);

                    if (oDic.ContainsKey("UnitPrice") && !string.IsNullOrEmpty(oDic["UnitPrice"]))
                        sb.Append(", UnitPrice   = ").AppendParameter("UnitPrice" + i, oDic["UnitPrice"]);

                    if (oDic.ContainsKey("Qty") && !string.IsNullOrEmpty(oDic["Qty"]))
                        sb.Append(", Qty   = ").AppendParameter("Qty" + i, oDic["Qty"]);

                    sb.Append(" Where 0=0 ");
                    sb.Append(" And POID  =").AppendParameter("POID" + i, oKey[0]);
                    sb.Append(" And POSeq =").AppendParameter("POSeq" + i, oKey[1]);
                }
                //重新計算加總值
                sb.AppendStatement(string.Format(_ReCal_BaseUpdSQL, _MainID));

                DbConnection cn = db.OpenConnection();
                DbTransaction tx = cn.BeginTransaction();
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    tx.Commit();
                    Util.NotifyMsg("[選取項目]更新成功", Util.NotifyKind.Success);
                }
                catch
                {
                    tx.Rollback();
                    Util.NotifyMsg("[選取項目]更新失敗", Util.NotifyKind.Error);
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                    tx.Dispose();
                }
            }
        }
        fmDetailBatchReset();
        fmMainRefresh();
        Refresh(true,true);
    }

    /// <summary>
    /// 取消明細整批更新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetailBatchUpdateCancel_Click(object sender, EventArgs e)
    {
        fmDetailBatchReset();
        ucModalPopup1.Hide();
        Refresh();
    }
    #endregion

    /// <summary>
    /// 重置 fmDetailBatch ，以便下次呼叫
    /// </summary>
    protected void fmDetailBatchReset()
    {
        Util_ucTextBox oText;
        Util_ucCommSingleSelect oDrpList;
        Util_ucDatePicker oDate;
        string strObjID;

        strObjID = "ItemDesc";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucIsRequire = false;
        oText.ucIsRegExp = false;
        oText.Refresh();

        strObjID = "ItemExpDate";
        oDate = (Util_ucDatePicker)fmDetailBatch.FindControl(strObjID);
        oDate.ucIsRequire = false;
        oDate.Refresh();

        strObjID = "Unit";
        oDrpList = (Util_ucCommSingleSelect)fmDetailBatch.FindControl(strObjID);
        oDrpList.ucSourceDictionary = Util.getDictionary(_dicUnit);
        oDrpList.ucIsRequire = false;
        oDrpList.Refresh();

        strObjID = "UnitPrice";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucIsRequire = false;
        oText.ucIsRegExp = false;
        oText.Refresh();

        strObjID = "Qty";
        oText = (Util_ucTextBox)fmDetailBatch.FindControl(strObjID);
        oText.ucIsRequire = false;
        oText.ucIsRegExp = false;
        oText.Refresh();
    }

}