using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;

/// <summary>
/// 萬用[功能選單列]控制項
/// <para></para>
/// <para>**欲使用此控制，資料來源的資料庫內需有[NodeInfo][NodeInfo_MUI][viewNodeInfo]等物件，請從[NetSample]資料庫中取得</para>
/// <para>**[NetSample]資料庫中另有[viewNodeInfoRoute]，方便開發人員取得資料階層的展開資料，若有自行開發相關應用的需求再套用即可</para>
/// </summary>
public partial class Util_ucMenuBar : BaseUserControl
{
    #region 變數
    string[] _NodeInfoData_NeedFldList = "NodeID,NodeName,ParentNodeID,TargetUrl".Split(',');  //使用[ucNodeInfoData]時的 必要性 欄位清單
    string[] _NodeInfoData_OptiFldList = "TargetPara,ChkGrantID,ToolTip".Split(',');  //使用[ucNodeInfoData]時的 選擇性 欄位清單
    #endregion

    #region 屬性
    /// <summary>
    /// 指定節點資料來源的資料庫名稱 (與 ucNodeInfoData 二擇一)
    /// <para>當有值時，便從指定 DB 讀取 [NodeInfo] 的內容</para>
    /// </summary>
    public string ucDBName
    {
        get
        {
            if (ViewState["_DBName"] == null)
            {
                ViewState["_DBName"] = Util.getRequestQueryStringKey("DBName");
            }
            return (string)(ViewState["_DBName"]);
        }
        set
        {
            ViewState["_DBName"] = value;
        }
    }

    /// <summary>
    /// 節點來源資料表(與 ucDBName 二擇一)
    /// <para>必要性欄位：[NodeID,NodeName,ParentNodeID,TargetUrl,TargetPara]</para>
    /// <para>選擇性欄位：[ChkGrantID,ToolTip,ImageUrl]</para>
    /// </summary>
    public DataTable ucNodeInfoData
    {
        set
        {
            if (value != null)
            {
                ViewState["_NodeInfoDataStatus"] = "W"; //For 程式內部辨識  [W:等待檢查  Y:已檢查成功 N:已檢查但有Error]
                ViewState["_NodeInfoData"] = value;
            }
        }
    }

    /// <summary>
    /// 無資料時的顯示訊息(預設 Msg_DataNotFound)
    /// </summary>
    public string ucDataNotFoundMsg
    {
        get
        {
            if (ViewState["_DataNotFoundMsg"] == null)
            {
                ViewState["_DataNotFoundMsg"] = RS.Resources.Msg_DataNotFound;
            }
            return (string)(ViewState["_DataNotFoundMsg"]);
        }
        set
        {
            ViewState["_DataNotFoundMsg"] = value;
        }
    }

    /// <summary>
    /// 根節點的 NodeID
    /// </summary>
    public string ucRootNodeID
    {
        get
        {
            if (ViewState["_RootNodeID"] == null)
            {
                ViewState["_RootNodeID"] = Util.getRequestQueryStringKey("RootNodeID");
            }
            return (string)(ViewState["_RootNodeID"]);
        }
        set
        {
            ViewState["_RootNodeID"] = value;
        }
    }

    /// <summary>
    /// 需檢查的 GrantID 清單
    /// </summary>
    public string[] ucChkGrantIDList
    {
        get
        {
            if (ViewState["_ChkGrantIDList"] == null)
            {
                return null;
            }
            return (string[])(ViewState["_ChkGrantIDList"]);
        }
        set
        {
            ViewState["_ChkGrantIDList"] = value;
        }
    }

    /// <summary>
    /// [節點連結]點擊時是否使用彈出視窗(預設 false)
    /// </summary>
    public bool ucIsPopup
    {
        get
        {
            if (ViewState["_IsPopup"] == null)
            {
                ViewState["_IsPopup"] = false;
            }
            return (bool)(ViewState["_IsPopup"]);
        }
        set
        {
            ViewState["_IsPopup"] = value;
        }
    }

    /// <summary>
    /// [節點連結]的目標 Frame 名稱
    /// </summary>
    public string ucTargetFrame
    {
        get
        {
            if (ViewState["_TargetFrame"] == null)
            {
                ViewState["_TargetFrame"] = "";
            }
            return (string)(ViewState["_TargetFrame"]);
        }
        set
        {
            ViewState["_TargetFrame"] = value;
        }
    }

    /// <summary>
    /// 彈出視窗的組態參數(預設讀取 [app://CfgPopupSpecs/] )
    /// </summary>
    public string ucPopupCfgSetting
    {
        get
        {
            if (ViewState["_PopupCfgSetting"] == null)
            {
                ViewState["_PopupCfgSetting"] = "app://CfgPopupSpecs/";
            }
            return (string)(ViewState["_PopupCfgSetting"]);
        }
        set
        {
            ViewState["_PopupCfgSetting"] = value;
        }
    }

    /// <summary>
    /// 套用的CSS Class(預設 Util_MenuBar)
    /// </summary>
    public string ucCssClass
    {
        get
        {
            if (ViewState["_CssClass"] == null)
            {
                ViewState["_CssClass"] = "Util_MenuBar";
            }
            return (string)(ViewState["_CssClass"]);
        }
        set
        {
            ViewState["_CssClass"] = value;
        }
    }

    /// <summary>
    /// 選單項目的 HorizontalPadding 像素(預設 5)
    /// </summary>
    public int ucMenuItemHorizontalPadding
    {
        get
        {
            if (ViewState["_MenuItemHorizontalPadding"] == null)
            {
                ViewState["_MenuItemHorizontalPadding"] = 5;
            }
            return (int)(ViewState["_MenuItemHorizontalPadding"]);
        }
        set
        {
            ViewState["_MenuItemHorizontalPadding"] = value;
        }
    }

    /// <summary>
    /// 選單項目的 VerticalPadding 像素(預設 5)
    /// </summary>
    public int ucMenuItemVerticalPadding
    {
        get
        {
            if (ViewState["_MenuItemVerticalPadding"] == null)
            {
                ViewState["_MenuItemVerticalPadding"] = 5;
            }
            return (int)(ViewState["_MenuItemVerticalPadding"]);
        }
        set
        {
            ViewState["_MenuItemVerticalPadding"] = value;
        }
    }

    //==== Main Menu ====

    /// <summary>
    /// 主選單CSS (預設 Util_Center)
    /// </summary>
    public string ucMainMenuCssClass
    {
        get
        {
            if (ViewState["_MainMenuCssClass"] == null)
            {
                ViewState["_MainMenuCssClass"] = "Util_Center";
            }
            return (string)(ViewState["_MainMenuCssClass"]);
        }
        set
        {
            ViewState["_MainMenuCssClass"] = value;
        }
    }

    /// <summary>
    /// 若有關聯選單，是否顯示相關圖示(預設 false)
    /// </summary>
    public bool ucMainMenuMoreImageEnabled
    {
        get
        {
            if (ViewState["_MainMenuMoreImageEnabled"] == null)
            {
                ViewState["_MainMenuMoreImageEnabled"] = false;
            }
            return (bool)(ViewState["_MainMenuMoreImageEnabled"]);
        }
        set
        {
            ViewState["_MainMenuMoreImageEnabled"] = value;
        }
    }

    /// <summary>
    /// 當主選單無所屬子項目時，是否仍然顯示(預設 true)
    /// </summary>
    public bool ucShowMainMenuWhenChildEmpty
    {
        //2016.07.19 新增
        get
        {
            if (ViewState["_ShowMainMenuWhenChildEmpty"] == null)
            {
                ViewState["_ShowMainMenuWhenChildEmpty"] = true;
            }
            return (bool)(ViewState["_ShowMainMenuWhenChildEmpty"]);
        }
        set
        {
            ViewState["_ShowMainMenuWhenChildEmpty"] = value;
        }
    }

    /// <summary>
    /// 主選單數量(預設 0，代表自動算)
    /// </summary>
    public int ucMainMenuQty
    {
        get
        {
            if (ViewState["_MainMenuQty"] == null)
            {
                ViewState["_MainMenuQty"] = 0;
            }
            return (int)(ViewState["_MainMenuQty"]);
        }
        set
        {
            ViewState["_MainMenuQty"] = value;
        }
    }

    /// <summary>
    /// 主選單寬度像素(預設 115)
    /// </summary>
    public int ucMainMenuWidth
    {
        get
        {
            if (ViewState["_MainMenuWidth"] == null)
            {
                ViewState["_MainMenuWidth"] = 115;
            }
            return (int)(ViewState["_MainMenuWidth"]);
        }
        set
        {
            ViewState["_MainMenuWidth"] = value;
        }
    }

    /// <summary>
    /// 主選單高度像素(預設 30)
    /// </summary>
    public int ucMainMenuHeight
    {
        get
        {
            if (ViewState["_MainMenuHeight"] == null)
            {
                ViewState["_MainMenuHeight"] = 30;
            }
            return (int)(ViewState["_MainMenuHeight"]);
        }
        set
        {
            ViewState["_MainMenuHeight"] = value;
        }
    }

    /// <summary>
    /// 主選單項目的 BorderWidth (預設 0)
    /// </summary>
    public int ucMainMenuBorderWidth
    {
        get
        {
            if (ViewState["_MainMenuBorderWidth"] == null)
            {
                ViewState["_MainMenuBorderWidth"] = 0;
            }
            return (int)(ViewState["_MainMenuBorderWidth"]);
        }
        set
        {
            ViewState["_MainMenuBorderWidth"] = value;
        }
    }

    /// <summary>
    /// 主選單字體像素(預設 16)
    /// </summary>
    public int ucMainMenuFontSize
    {
        get
        {
            if (ViewState["_MainMenuFontSize"] == null)
            {
                ViewState["_MainMenuFontSize"] = 16;
            }
            return (int)(ViewState["_MainMenuFontSize"]);
        }
        set
        {
            ViewState["_MainMenuFontSize"] = value;
        }
    }

    /// <summary>
    /// 主選單前景顏色(預設 #000)
    /// </summary>
    public string ucMainMenuForeColor
    {
        get
        {
            if (ViewState["_MainMenuForeColor"] == null)
            {
                ViewState["_MainMenuForeColor"] = "#000";
            }
            return (string)(ViewState["_MainMenuForeColor"]);
        }
        set
        {
            ViewState["_MainMenuForeColor"] = value;
        }
    }

    /// <summary>
    /// 主選單背景顏色(預設 #FFF)
    /// </summary>
    public string ucMainMenuBackColor
    {
        get
        {
            if (ViewState["_MainMenuBackColor"] == null)
            {
                ViewState["_MainMenuBackColor"] = "#FFF";
            }
            return (string)(ViewState["_MainMenuBackColor"]);
        }
        set
        {
            ViewState["_MainMenuBackColor"] = value;
        }
    }

    /// <summary>
    /// 主選單邊框顏色(預設 #E3E3E3)
    /// </summary>
    public string ucMainMenuBorderColor
    {
        get
        {
            if (ViewState["_MainMenuBorderColor"] == null)
            {
                ViewState["_MainMenuBorderColor"] = "#E3E3E3";
            }
            return (string)(ViewState["_MainMenuBorderColor"]);
        }
        set
        {
            ViewState["_MainMenuBorderColor"] = value;
        }
    }

    /// <summary>
    /// 主選單懸停前景顏色(預設 #FFF)
    /// </summary>
    public string ucMainMenuHoverForeColor
    {
        get
        {
            if (ViewState["_MainMenuHoverForeColor"] == null)
            {
                ViewState["_MainMenuHoverForeColor"] = "#FFF";
            }
            return (string)(ViewState["_MainMenuHoverForeColor"]);
        }
        set
        {
            ViewState["_MainMenuHoverForeColor"] = value;
        }
    }

    /// <summary>
    /// 主選單懸停背景顏色(預設 #D00F24)
    /// </summary>
    public string ucMainMenuHoverBackColor
    {
        get
        {
            if (ViewState["_MainMenuHoverBackColor"] == null)
            {
                ViewState["_MainMenuHoverBackColor"] = "#D00F24";
            }
            return (string)(ViewState["_MainMenuHoverBackColor"]);
        }
        set
        {
            ViewState["_MainMenuHoverBackColor"] = value;
        }
    }

    /// <summary>
    /// 主選單懸停邊框顏色(預設 #D00F24)
    /// </summary>
    public string ucMainMenuHoverBorderColor
    {
        get
        {
            if (ViewState["_MainMenuHoverBorderColor"] == null)
            {
                ViewState["_MainMenuHoverBorderColor"] = "#D00F24";
            }
            return (string)(ViewState["_MainMenuHoverBorderColor"]);
        }
        set
        {
            ViewState["_MainMenuHoverBorderColor"] = value;
        }
    }

    //==== Child Menu ====

    /// <summary>
    /// 子選單CSS (預設 Util_Left + Util_Frame)
    /// </summary>
    public string ucChildMenuCssClass
    {
        get
        {
            if (ViewState["_ChildMenuCssClass"] == null)
            {
                ViewState["_ChildMenuCssClass"] = "Util_Left Util_Frame";
            }
            return (string)(ViewState["_ChildMenuCssClass"]);
        }
        set
        {
            ViewState["_ChildMenuCssClass"] = value;
        }
    }

    /// <summary>
    /// 若有相關子選單，是否顯示相關提示圖示(預設 true)
    /// </summary>
    public bool ucChildMenuMoreImageEnabled
    {
        get
        {
            if (ViewState["_ChildMenuMoreImageEnabled"] == null)
            {
                ViewState["_ChildMenuMoreImageEnabled"] = true;
            }
            return (bool)(ViewState["_ChildMenuMoreImageEnabled"]);
        }
        set
        {
            ViewState["_ChildMenuMoreImageEnabled"] = value;
        }
    }

    /// <summary>
    /// 子選單最深顯示階層(預設 5)
    /// </summary>
    public int ucChildMenuMaxDisplayLevel
    {
        get
        {
            if (ViewState["_ChildMenuMaxDisplayLevel"] == null)
            {
                ViewState["_ChildMenuMaxDisplayLevel"] = 5;
            }
            return (int)(ViewState["_ChildMenuMaxDisplayLevel"]);
        }
        set
        {
            ViewState["_ChildMenuMaxDisplayLevel"] = value;
        }
    }

    /// <summary>
    /// 子選單寬度像素(預設 170)
    /// </summary>
    public int ucChildMenuWidth
    {
        get
        {
            if (ViewState["_ChildMenuWidth"] == null)
            {
                ViewState["_ChildMenuWidth"] = 170;
            }
            return (int)(ViewState["_ChildMenuWidth"]);
        }
        set
        {
            ViewState["_ChildMenuWidth"] = value;
        }
    }

    /// <summary>
    /// 子選單高度像素(預設 22)
    /// </summary>
    public int ucChildMenuHeight
    {
        get
        {
            if (ViewState["_ChildMenuHeight"] == null)
            {
                ViewState["_ChildMenuHeight"] = 22;
            }
            return (int)(ViewState["_ChildMenuHeight"]);
        }
        set
        {
            ViewState["_ChildMenuHeight"] = value;
        }
    }

    /// <summary>
    /// 子選單項目的 BorderWidth (預設 0)
    /// </summary>
    public int ucChildMenuBorderWidth
    {
        get
        {
            if (ViewState["_ChildMenuBorderWidth"] == null)
            {
                ViewState["_ChildMenuBorderWidth"] = 0;
            }
            return (int)(ViewState["_ChildMenuBorderWidth"]);
        }
        set
        {
            ViewState["_ChildMenuBorderWidth"] = value;
        }
    }

    /// <summary>
    /// 子選單字體像素(預設 12)
    /// </summary>
    public int ucChildMenuFontSize
    {
        get
        {
            if (ViewState["_ChildMenuFontSize"] == null)
            {
                ViewState["_ChildMenuFontSize"] = 12;
            }
            return (int)(ViewState["_ChildMenuFontSize"]);
        }
        set
        {
            ViewState["_ChildMenuFontSize"] = value;
        }
    }

    /// <summary>
    /// 子選單前景顏色(預設 #3C3C3C)
    /// </summary>
    public string ucChildMenuForeColor
    {
        get
        {
            if (ViewState["_ChildMenuForeColor"] == null)
            {
                ViewState["_ChildMenuForeColor"] = "#3C3C3C";
            }
            return (string)(ViewState["_ChildMenuForeColor"]);
        }
        set
        {
            ViewState["_ChildMenuForeColor"] = value;
        }
    }

    /// <summary>
    /// 子選單背景顏色(預設 #FFF)
    /// </summary>
    public string ucChildMenuBackColor
    {
        get
        {
            if (ViewState["_ChildMenuBackColor"] == null)
            {
                ViewState["_ChildMenuBackColor"] = "#FFF";
            }
            return (string)(ViewState["_ChildMenuBackColor"]);
        }
        set
        {
            ViewState["_ChildMenuBackColor"] = value;
        }
    }

    /// <summary>
    /// 子選單邊框顏色(預設 #808080)
    /// </summary>
    public string ucChildMenuBorderColor
    {
        get
        {
            if (ViewState["_ChildMenuBorderColor"] == null)
            {
                ViewState["_ChildMenuBorderColor"] = "#808080";
            }
            return (string)(ViewState["_ChildMenuBorderColor"]);
        }
        set
        {
            ViewState["_ChildMenuBorderColor"] = value;
        }
    }

    /// <summary>
    /// 子選單懸停前景顏色(預設 #D00F24)
    /// </summary>
    public string ucChildMenuHoverForeColor
    {
        get
        {
            if (ViewState["_ChildMenuHoverForeColor"] == null)
            {
                ViewState["_ChildMenuHoverForeColor"] = "#D00F24";
            }
            return (string)(ViewState["_ChildMenuHoverForeColor"]);
        }
        set
        {
            ViewState["_ChildMenuHoverForeColor"] = value;
        }
    }

    /// <summary>
    /// 子選單懸停背景顏色(預設 #E0E0E0)
    /// </summary>
    public string ucChildMenuHoverBackColor
    {
        get
        {
            if (ViewState["_ChildMenuHoverBackColor"] == null)
            {
                ViewState["_ChildMenuHoverBackColor"] = "#E0E0E0";
            }
            return (string)(ViewState["_ChildMenuHoverBackColor"]);
        }
        set
        {
            ViewState["_ChildMenuHoverBackColor"] = value;
        }
    }

    /// <summary>
    /// 子選單懸停邊框顏色(預設 #808080)
    /// </summary>
    public string ucChildMenuHoverBorderColor
    {
        get
        {
            if (ViewState["_ChildMenuHoverBorderColor"] == null)
            {
                ViewState["_ChildMenuHoverBorderColor"] = "#808080";
            }
            return (string)(ViewState["_ChildMenuHoverBorderColor"]);
        }
        set
        {
            ViewState["_ChildMenuHoverBorderColor"] = value;
        }
    }

    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }
    #endregion

    #region 產生 MenuBar
    /// <summary>
    /// 重置所有設定
    /// </summary>
    public void Reset()
    {
        this.ViewState.Clear();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        divMenuBar.Style["display"] = "none"; //準備就緒前隱藏 2016.12.09
        divMenuBar.Controls.Clear();
        if (string.IsNullOrEmpty(ucRootNodeID))
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "參數錯誤 ,  需要 [ucRootNodeID]  及 ( [ucDBName] / [ucNodeInfoData]二擇一 ) ");
            labErrMsg.Visible = true;
            return;
        }
        else
        {
            if (getNodeInfoData() == null)
            {
                if (ViewState["_NodeInfoDataStatus"] != null)
                {
                    //ucNodeInfoData
                    if (((string)ViewState["_NodeInfoDataStatus"]) == "N")
                    {
                        labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "[ucNodeInfoData] 自訂資料表的欄位名稱有錯誤 , 至少需有 [" + Util.getStringJoin(_NodeInfoData_NeedFldList) + "] 等欄位!");
                    }
                    if (((string)ViewState["_NodeInfoDataStatus"]) == "Y")
                    {
                        labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg + "(NodeInfoDataStatus=Y)");
                    }
                }
                else
                {
                    //SQL 
                    labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "[NodeInfo] 資料表內未發現符合條件的資料 !");
                }

                labErrMsg.Visible = true;
                return;
            }
        }

        Menu oMenuBar = new Menu();
        if (!string.IsNullOrEmpty(ucCssClass))
        {
            oMenuBar.CssClass = ucCssClass;
        }

        //Global
        divMenuBar.Style.Add("background-color", ucMainMenuBackColor);
        oMenuBar.Orientation = Orientation.Horizontal;
        oMenuBar.MaximumDynamicDisplayLevels = ucChildMenuMaxDisplayLevel;
        oMenuBar.StaticMenuItemStyle.HorizontalPadding = ucMenuItemHorizontalPadding;
        oMenuBar.DynamicMenuItemStyle.HorizontalPadding = ucMenuItemHorizontalPadding;
        oMenuBar.StaticMenuItemStyle.VerticalPadding = ucMenuItemVerticalPadding;
        oMenuBar.DynamicMenuItemStyle.VerticalPadding = ucMenuItemVerticalPadding;

        oMenuBar.StaticEnableDefaultPopOutImage = ucMainMenuMoreImageEnabled;
        oMenuBar.DynamicEnableDefaultPopOutImage = ucChildMenuMoreImageEnabled;
        oMenuBar.StaticMenuStyle.CssClass = ucMainMenuCssClass;
        oMenuBar.DynamicMenuStyle.CssClass = ucChildMenuCssClass;


        //Static
        oMenuBar.StaticMenuItemStyle.Height = ucMainMenuHeight;
        oMenuBar.StaticMenuItemStyle.Width = ucMainMenuWidth;
        oMenuBar.StaticMenuItemStyle.Font.Size = ucMainMenuFontSize;
        oMenuBar.StaticMenuItemStyle.ForeColor = Color.FromName(ucMainMenuForeColor);
        oMenuBar.StaticMenuItemStyle.BackColor = Color.FromName(ucMainMenuBackColor);
        if (ucMainMenuBorderWidth > 0)
        {
            oMenuBar.StaticMenuItemStyle.BorderWidth = ucMainMenuBorderWidth;
            oMenuBar.StaticMenuItemStyle.BorderStyle = BorderStyle.Solid;
            oMenuBar.StaticMenuItemStyle.BorderColor = Color.FromName(ucMainMenuBorderColor);
        }


        oMenuBar.StaticHoverStyle.ForeColor = Color.FromName(ucMainMenuHoverForeColor);
        oMenuBar.StaticHoverStyle.BackColor = Color.FromName(ucMainMenuHoverBackColor);
        if (ucMainMenuBorderWidth > 0)
        {
            oMenuBar.StaticHoverStyle.BorderWidth = ucMainMenuBorderWidth;
            oMenuBar.StaticHoverStyle.BorderStyle = BorderStyle.Solid;
            oMenuBar.StaticHoverStyle.BorderColor = Color.FromName(ucMainMenuHoverBorderColor);
        }


        //Dynamic
        oMenuBar.DynamicHorizontalOffset = 3;
        oMenuBar.DynamicMenuItemStyle.Height = ucChildMenuHeight;
        oMenuBar.DynamicMenuItemStyle.Width = ucChildMenuWidth;
        oMenuBar.DynamicMenuItemStyle.Font.Size = ucChildMenuFontSize;
        oMenuBar.DynamicMenuItemStyle.ForeColor = Color.FromName(ucChildMenuForeColor);
        oMenuBar.DynamicMenuItemStyle.BackColor = Color.FromName(ucChildMenuBackColor);
        if (ucChildMenuBorderWidth > 0)
        {
            oMenuBar.DynamicHorizontalOffset = 0;
            oMenuBar.DynamicMenuItemStyle.BorderWidth = ucChildMenuBorderWidth;
            oMenuBar.DynamicMenuItemStyle.BorderStyle = BorderStyle.Solid;
            oMenuBar.DynamicMenuItemStyle.BorderColor = Color.FromName(ucChildMenuBorderColor);
        }


        oMenuBar.DynamicHoverStyle.ForeColor = Color.FromName(ucChildMenuHoverForeColor);
        oMenuBar.DynamicHoverStyle.BackColor = Color.FromName(ucChildMenuHoverBackColor);
        if (ucChildMenuBorderWidth > 0)
        {
            oMenuBar.DynamicHoverStyle.BorderWidth = ucChildMenuBorderWidth;
            oMenuBar.DynamicHoverStyle.BorderStyle = BorderStyle.Solid;
            oMenuBar.DynamicHoverStyle.BorderColor = Color.FromName(ucChildMenuHoverBorderColor);
        }

        DataTable dtWholeMenu = getWholeNodeData(ucRootNodeID);
        if (dtWholeMenu == null)
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg + "(getWholeNodeData=null)");
            labErrMsg.Visible = true;
            return;
        }

        DataRow[] drRoot = dtWholeMenu.Select(string.Format("ParentNodeID = '' And TargetUrl = '' And NodeID = '{0}' ", ucRootNodeID));
        if (drRoot.Count() > 0)
        {
            //處理第一階(主)選單
            DataTable dtNodeInfo = getNodeInfoData();
            DataRow[] drNodes = dtNodeInfo.Select(string.Format("ParentNodeID = '{0}' ", ucRootNodeID), " OrderSeq Asc, NodeID Asc ");
            if (drNodes.Count() > 0)
            {
                for (int i = 0; i < ((ucMainMenuQty > 0) ? ucMainMenuQty : drNodes.Count()); i++)
                {
                    if (i < drNodes.Count())
                    {
                        MenuItem tChildNode = getSingleItem(drNodes[i]);
                        if (ucShowMainMenuWhenChildEmpty)
                        {
                            //固定產生
                            oMenuBar.Items.Add(tChildNode);
                            BuildChildItem(tChildNode);
                        }
                        else
                        {
                            //有子階資料時才產生
                            if (dtNodeInfo.Select(string.Format("ParentNodeID = '{0}' ", tChildNode.Value)).Count() > 0)
                            {
                                oMenuBar.Items.Add(tChildNode);
                                BuildChildItem(tChildNode);
                            }
                        }
                    }
                    else
                    {
                        //產生空白項目(for ucMainMenuQty)
                        oMenuBar.Items.Add(new MenuItem("", "", "", "javascript:void(0);"));
                    }
                }
            }
        }
        else
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg + "(ucRootNodeID=[" + ucRootNodeID + "] no match data)");
            labErrMsg.Visible = true;
        }

        if (!labErrMsg.Visible)
        {
            divMenuBar.Controls.Add(oMenuBar);
            //準備就緒後才顯示 2016.12.09
            string strDispJS = "\n\n dom.Ready(function(){ document.getElementById('" + divMenuBar.ClientID + "').style.display = ''; });\n\n";
            Util.setJSContent(strDispJS, this.ClientID + "_Ready");
        }
    }

    /// <summary>
    /// 產生子節點
    /// </summary>
    /// <param name="oParentItem"></param>
    protected void BuildChildItem(MenuItem oParentItem)
    {
        DataTable dtNodeInfo = getNodeInfoData();
        DataRow[] drNodes = dtNodeInfo.Select(string.Format("ParentNodeID = '{0}' ", oParentItem.Value), " OrderSeq Asc, NodeID Asc ");
        if (drNodes.Count() > 0)
        {
            for (int i = 0; i < drNodes.Count(); i++)
            {
                MenuItem tChildItem = getSingleItem(drNodes[i]);
                oParentItem.ChildItems.Add(tChildItem);
                BuildChildItem(tChildItem);
            }
        }
    }

    /// <summary>
    /// 產生單一節點
    /// </summary>
    /// <param name="drItem"></param>
    /// <returns></returns>
    protected MenuItem getSingleItem(DataRow drItem)
    {
        MenuItem oItem = new MenuItem();
        bool IsExpandItem = false;
        if (drItem == null) return oItem;

        oItem.Text = drItem["NodeName"].ToString();
        oItem.Value = drItem["NodeID"].ToString();
        if (!string.IsNullOrEmpty(drItem["ToolTip"].ToString()))
        {
            oItem.ToolTip = drItem["ToolTip"].ToString();
        }

        if (string.IsNullOrEmpty(drItem["TargetUrl"].ToString().Trim()))
        {
            if (getNodeInfoData().Select(string.Format(" ParentNodeID = '{0}' ", drItem["NodeID"].ToString())).Count() > 0)
            {
                IsExpandItem = true;
            }
        }

        if (IsExpandItem)
        {
            //展開用項
            oItem.NavigateUrl = "javascript:void(0);";
        }
        else
        {
            //程式連結
            oItem.NavigateUrl = Util.getUrlJSContent(drItem["TargetUrl"].ToString(), drItem["TargetPara"].ToString().Replace("＆", "_._"), ucIsPopup, drItem["NodeName"].ToString(), ucTargetFrame, ucPopupCfgSetting);
            if (!ucIsPopup && !string.IsNullOrEmpty(ucTargetFrame))
            {
                //更改視窗標題
                oItem.NavigateUrl = oItem.NavigateUrl.Replace("javascript:", string.Format("javascript:top.frames['top'].document.title='[{0}]';", drItem["NodeName"].ToString()));
            }
        }

        return oItem;
    }
    #endregion

    #region 處理節點資料
    /// <summary>
    /// 取得選單所需的所有節點資訊內容
    /// </summary>
    /// <returns></returns>
    protected DataTable getNodeInfoData()
    {
        if (ViewState["_NodeInfoData"] == null)
        {
            DbHelper db = new DbHelper(ucDBName);
            string strSQL = string.Format("Select * From viewNodeInfo Where IsEnabled = 'Y'  And CultureCode = '{0}' ", Util.getUICultureCode());

            //過濾 ChkGrantIDList
            if (ucChkGrantIDList != null && ucChkGrantIDList.Count() > 0)
            {
                strSQL += string.Format(" And ChkGrantID in ('','{0}') ", Util.getStringJoin(ucChkGrantIDList, "','"));
            }

            DataTable dt = db.ExecuteDataSet(strSQL).Tables[0];
            ViewState["_NodeInfoData"] = dt;
        }
        else
        {
            //利用 ViewState["_NodeInfoDataStatus"] 進行程式邏輯判斷   [W:等待檢查  Y:已檢查成功 N:已檢查但有Error]
            if (ViewState["_NodeInfoDataStatus"] != null && ((string)ViewState["_NodeInfoDataStatus"]) == "W")
            {
                //若使用了[ucNodeInfoData]屬性設定了自訂節點資料，則使用前需檢查資料欄位是否合理
                ViewState["_NodeInfoDataStatus"] = "Y";
                DataTable dtNode = (DataTable)ViewState["_NodeInfoData"];
                if (dtNode != null && dtNode.Rows.Count > 0)
                {
                    //檢查必要欄位(不存在就觸發Error並清空 ViewState["_NodeInfoData"])
                    for (int i = 0; i < _NodeInfoData_NeedFldList.Count(); i++)
                    {
                        if ((string)ViewState["_NodeInfoDataStatus"] == "Y")
                        {
                            if (!dtNode.Columns.Contains(_NodeInfoData_NeedFldList[i]))
                            {
                                ViewState["_NodeInfoDataStatus"] = "N";
                                ViewState["_NodeInfoData"] = null;
                            }
                        }
                    }
                    //處理非必要欄位(不存在就自動新增)
                    if ((string)ViewState["_NodeInfoDataStatus"] == "Y")
                    {
                        for (int i = 0; i < _NodeInfoData_OptiFldList.Count(); i++)
                        {
                            if (!dtNode.Columns.Contains(_NodeInfoData_OptiFldList[i]))
                            {
                                dtNode.Columns.Add(_NodeInfoData_OptiFldList[i]);
                                ViewState["_NodeInfoData"] = dtNode;
                            }
                        }
                    }
                }
                else
                {
                    ViewState["_NodeInfoData"] = null;
                }
            }
        }

        return (DataTable)(ViewState["_NodeInfoData"]);
    }

    /// <summary>
    /// 依據選單階層取得節點資料
    /// </summary>
    /// <param name="strRootNodeID"></param>
    /// <returns></returns>
    protected DataTable getWholeNodeData(string strRootNodeID)
    {
        DataTable dtNodeInfo = getNodeInfoData();
        DataTable dtResult = dtNodeInfo.Clone();
        if (!string.IsNullOrEmpty(strRootNodeID))
        {
            DataRow[] drRoots = dtNodeInfo.Select(string.Format("ParentNodeID = '' And TargetUrl = '' And NodeID = '{0}' ", strRootNodeID));
            DataTable dtChild;
            if (drRoots.Count() > 0)
            {
                dtResult.ImportRow(drRoots[0]);
                dtChild = getChildNodeData(drRoots[0]["NodeID"].ToString());
                if (dtChild != null && dtChild.Rows.Count > 0)
                {
                    for (int j = 0; j < dtChild.Rows.Count; j++)
                    {
                        dtResult.ImportRow(dtChild.Rows[j]);
                    }
                }
                else
                {
                    dtResult = null;
                }
            }
        }
        return dtResult;
    }

    /// <summary>
    /// 取得子節點資料
    /// </summary>
    /// <param name="strParentNodeID"></param>
    /// <returns></returns>
    protected DataTable getChildNodeData(string strParentNodeID)
    {
        DataTable dtNodeInfo = getNodeInfoData();
        DataTable dtResult = dtNodeInfo.Clone();
        DataTable dtChild;
        DataRow[] tmpRowArray = dtNodeInfo.Select(string.Format(" ParentNodeID = '{0}' ", strParentNodeID));
        DataRow tmpRow;
        for (int i = 0; i < tmpRowArray.Count(); i++)
        {
            tmpRow = tmpRowArray[i];
            if (string.IsNullOrEmpty(tmpRow["TargetUrl"].ToString().Trim()))
            {
                //展開項目
                if (dtNodeInfo.Select(string.Format(" ParentNodeID = '{0}' ", tmpRow["NodeID"].ToString())).Count() > 0)
                {

                    dtChild = getChildNodeData(tmpRow["NodeID"].ToString());
                    if (dtChild != null && dtChild.Rows.Count > 0)
                    {
                        //有Child 才往下處理
                        dtResult.ImportRow(tmpRowArray[i]);

                        for (int j = 0; j < dtChild.Rows.Count; j++)
                        {
                            dtResult.ImportRow(dtChild.Rows[j]);
                        }
                    }
                }
            }
            else
            {
                //程式項目
                dtResult.ImportRow(tmpRowArray[i]);
            }
        }
        return dtResult;
    }
    #endregion
}