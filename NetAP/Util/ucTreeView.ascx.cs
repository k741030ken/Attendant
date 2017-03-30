using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;


/// <summary>
/// 萬用[樹狀選單]控制項
/// <para></para>
/// <para>**欲使用此控制，資料來源的資料庫內需有[NodeInfo][NodeInfo_MUI][viewNodeInfo]等物件，請從[NetSample]資料庫中取得</para>
/// <para>**[NetSample]資料庫中另有[viewNodeInfoRoute]，方便開發人員取得資料階層的展開資料，若有自行開發相關應用的需求再套用即可</para>
/// </summary>
public partial class Util_ucTreeView : BaseUserControl
{
    #region 變數
    string[] _NodeInfoData_NeedFldList = "NodeID,NodeName,ParentNodeID,TargetUrl".Split(',');  //使用[ucNodeInfoData]時的 必要性 欄位清單
    string[] _NodeInfoData_OptiFldList = "TargetPara,ChkGrantID,ToolTip,ImageUrl".Split(',');  //使用[ucNodeInfoData]時的 選擇性 欄位清單
    bool _IsAjax = true;
    #endregion

    #region 屬性
    /// <summary>
    /// 指定節點資料的來源資料庫名稱 (與 ucNodeInfoData 二擇一)
    /// <para>當有值時，便從指定 DB 讀取 [NodeInfo] 的內容</para>
    /// </summary>
    public string ucDBName
    {
        get
        {
            if (ViewState["_DBName"] == null)
            {
                ViewState["_DBName"] = "";
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
                ViewState["_RootNodeID"] = "";
            }
            return (string)(ViewState["_RootNodeID"]);
        }
        set
        {
            ViewState["_RootNodeID"] = value;
        }
    }

    /// <summary>
    /// 選取節點 的 NodeID
    /// </summary>
    public string ucSelectedNodeID
    {
        get
        {
            if (ViewState["_SelectedNodeID"] == null)
            {
                ViewState["_SelectedNodeID"] = "";
            }
            return (string)(ViewState["_SelectedNodeID"]);
        }
        set
        {
            ViewState["_SelectedNodeID"] = value.Trim();
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
    /// 連結節點 點擊時是否使用 彈出視窗(預設 false)
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
    /// 連結節點 的目標 Frame 名稱
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
    /// 彈出視窗 的組態參數來源(預設讀取 [app://CfgPopupSpecs/] )
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
    /// 是否使用 工具列(預設 true)
    /// </summary>
    public bool ucIsToolBarEnabled
    {
        get
        {
            if (ViewState["_IsToolBarEnabled"] == null)
            {
                ViewState["_IsToolBarEnabled"] = true;
            }
            return (bool)(ViewState["_IsToolBarEnabled"]);
        }
        set
        {
            ViewState["_IsToolBarEnabled"] = value;
        }
    }

    /// <summary>
    /// 是否隱藏 根節點(預設 true)
    /// </summary>
    public bool ucIsHideRootNode
    {
        get
        {
            if (ViewState["_IsHideRootNode"] == null)
            {
                ViewState["_IsHideRootNode"] = true;
            }
            return (bool)(ViewState["_IsHideRootNode"]);
        }
        set
        {
            ViewState["_IsHideRootNode"] = value;
        }
    }

    /// <summary>
    /// 是否顯示節點間的連結線(預設 false)
    /// </summary>
    public bool ucIsShowLines
    {
        get
        {
            if (ViewState["_IsShowLines"] == null)
            {
                ViewState["_IsShowLines"] = false;
            }
            return (bool)(ViewState["_IsShowLines"]);
        }
        set
        {
            ViewState["_IsShowLines"] = value;
        }
    }

    /// <summary>
    /// 收合所有節點 按鈕圖示(預設 Util.Icon_Icon_Up)
    /// </summary>
    public string ucCollapseAllImageUrl
    {
        get
        {
            if (ViewState["_CollapseAllImageUrl"] == null)
            {
                ViewState["_CollapseAllImageUrl"] = Util.Icon_Up;
            }
            return (string)(ViewState["_CollapseAllImageUrl"]);
        }
        set
        {
            ViewState["_CollapseAllImageUrl"] = value;
        }
    }

    /// <summary>
    /// 展開所有節點 按鈕圖示(預設 Util.Icon_Down)
    /// </summary>
    public string ucExpandAllImageUrl
    {
        get
        {
            if (ViewState["_ExpandAllImageUrl"] == null)
            {
                ViewState["_ExpandAllImageUrl"] = Util.Icon_Down;
            }
            return (string)(ViewState["_ExpandAllImageUrl"]);
        }
        set
        {
            ViewState["_ExpandAllImageUrl"] = value;
        }
    }


    /// <summary>
    /// 收合節點 的使用圖示(預設 Util.Icon_FolderOpen)
    /// </summary>
    public string ucCollapseImageUrl
    {
        get
        {
            if (ViewState["_CollapseImageUrl"] == null)
            {
                ViewState["_CollapseImageUrl"] = Util.Icon_FolderOpen;
            }
            return (string)(ViewState["_CollapseImageUrl"]);
        }
        set
        {
            ViewState["_CollapseImageUrl"] = value;
        }
    }

    /// <summary>
    /// 展開節點 的使用圖示(預設 Util.Icon_FolderClose)
    /// </summary>
    public string ucExpandImageUrl
    {
        get
        {
            if (ViewState["_ExpandImageUrl"] == null)
            {
                ViewState["_ExpandImageUrl"] = Util.Icon_FolderClose;
            }
            return (string)(ViewState["_ExpandImageUrl"]);
        }
        set
        {
            ViewState["_ExpandImageUrl"] = value;
        }
    }

    /// <summary>
    /// 無法展開 的節點使用圖示(預設 Util.Icon_SpaceHalf)
    /// </summary>
    public string ucNoExpandImageUrl
    {
        get
        {
            if (ViewState["_NoExpandImageUrl"] == null)
            {
                ViewState["_NoExpandImageUrl"] = Util.Icon_SpaceHalf;
            }
            return (string)(ViewState["_NoExpandImageUrl"]);
        }
        set
        {
            ViewState["_NoExpandImageUrl"] = value;
        }
    }

    /// <summary>
    /// 連結節點 預設圖示(預設 Util.Icon_Detail)
    /// </summary>
    public string ucNodeDefaultImageUrl
    {
        get
        {
            if (ViewState["_NodeDefaultImageUrl"] == null)
            {
                ViewState["_NodeDefaultImageUrl"] = Util.Icon_Detail;
            }
            return (string)(ViewState["_NodeDefaultImageUrl"]);
        }
        set
        {
            ViewState["_NodeDefaultImageUrl"] = value;
        }
    }

    /// <summary>
    /// 控制項的 CSS Class(預設 Util_TreeView)
    /// </summary>
    public string ucCssClass
    {
        get
        {
            if (ViewState["_CssClass"] == null)
            {
                ViewState["_CssClass"] = "Util_TreeView";
            }
            return (string)(ViewState["_CssClass"]);
        }
        set
        {
            ViewState["_CssClass"] = value;
        }
    }

    /// <summary>
    /// 節點的前景色(預設 #555555)
    /// </summary>
    public string ucNodeForeColor
    {
        get
        {
            if (ViewState["_NodeForeColor"] == null)
            {
                ViewState["_NodeForeColor"] = "#555555";
            }
            return (string)(ViewState["_NodeForeColor"]);
        }
        set
        {
            ViewState["_NodeForeColor"] = value;
        }
    }

    /// <summary>
    /// 節點內縮像素(預設 8)
    /// </summary>
    public int ucNodeIndent
    {
        get
        {
            if (ViewState["_NodeIndent"] == null)
            {
                ViewState["_NodeIndent"] = 8;
            }
            return (int)(ViewState["_NodeIndent"]);
        }
        set
        {
            ViewState["_NodeIndent"] = value;
        }
    }

    /// <summary>
    /// 懸停節點 是否使用文字底線(預設 true)
    /// </summary>
    public bool ucIsHoverFontUnderline
    {
        get
        {
            if (ViewState["_IsHoverFontUnderline"] == null)
            {
                ViewState["_IsHoverFontUnderline"] = true;
            }
            return (bool)(ViewState["_IsHoverFontUnderline"]);
        }
        set
        {
            ViewState["_IsHoverFontUnderline"] = value;
        }
    }

    /// <summary>
    /// 懸停節點 是否使用粗體字(預設 true)
    /// </summary>
    public bool ucIsHoverFontBold
    {
        get
        {
            if (ViewState["_IsHoverFontBold"] == null)
            {
                ViewState["_IsHoverFontBold"] = true;
            }
            return (bool)(ViewState["_IsHoverFontBold"]);
        }
        set
        {
            ViewState["_IsHoverFontBold"] = value;
        }
    }

    /// <summary>
    /// 選取節點 的前景色(預設 #A31515)
    /// </summary>
    public string ucSelectedNodeForeColor
    {
        get
        {
            if (ViewState["_SelectedNodeForeColor"] == null)
            {
                ViewState["_SelectedNodeForeColor"] = "#A31515";
            }
            return (string)(ViewState["_SelectedNodeForeColor"]);
        }
        set
        {
            ViewState["_SelectedNodeForeColor"] = value;
        }
    }

    /// <summary>
    /// 選取節點 是否使用文字底線(預設 true)
    /// </summary>
    public bool ucIsSelectedFontUnderline
    {
        get
        {
            if (ViewState["_IsSelectedFontUnderline"] == null)
            {
                ViewState["_IsSelectedFontUnderline"] = true;
            }
            return (bool)(ViewState["_IsSelectedFontUnderline"]);
        }
        set
        {
            ViewState["_IsSelectedFontUnderline"] = value;
        }
    }

    /// <summary>
    /// 選取節點 是否使用粗體字(預設 true)
    /// </summary>
    public bool ucIsSelectedFontBold
    {
        get
        {
            if (ViewState["_IsSelectedFontBold"] == null)
            {
                ViewState["_IsSelectedFontBold"] = true;
            }
            return (bool)(ViewState["_IsSelectedFontBold"]);
        }
        set
        {
            ViewState["_IsSelectedFontBold"] = value;
        }
    }
    #endregion

    #region 事件
    protected void Page_Load(object sender, EventArgs e)
    {
        //將Style設定從Refresh() 移到 Page_Load() 2016.10.19
        MainTree.HoverNodeStyle.Font.Underline = ucIsHoverFontUnderline;
        MainTree.HoverNodeStyle.Font.Bold = ucIsSelectedFontBold;
        MainTree.SelectedNodeStyle.ForeColor = Color.FromName(ucSelectedNodeForeColor);
        MainTree.SelectedNodeStyle.Font.Underline = ucIsSelectedFontUnderline;
        MainTree.SelectedNodeStyle.Font.Bold = ucIsSelectedFontBold;

        MainTree.NodeStyle.ForeColor = Color.FromName(ucNodeForeColor);
        MainTree.NodeIndent = ucNodeIndent;
        MainTree.NodeStyle.NodeSpacing = 0;

        //2017.03.15 JS控制 從 Refresh() 移到 Page_Load()
        string strJS = "dom.Ready(function(){ var oID = document.getElementById('" + MainTree.ClientID + "');if (oID != null){oID.style.display='';}  });";
        Util.setJSContent(strJS, this.ClientID + "_Init");
    }

    protected void MainTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        if (_IsAjax)
        {
            //Ajax 才需動態產生子節點
            BuildChildNode(e.Node);
        }
    }

    protected void btnExpandAll_Click(object sender, EventArgs e)
    {
        ExpandAll();
    }

    protected void btnCollapseAll_Click(object sender, EventArgs e)
    {
        CollapseAll();
    }

    /// <summary>
    /// 展開所有節點
    /// </summary>
    public void ExpandAll()
    {
        ViewState["_IsExpandAll"] = true;
        ViewState["_IsCollapseAll"] = false;
        //處理根節點
        setRootNodeJS();
        if (MainTree.Nodes.Count > 0)
        {
            MainTree.Nodes[0].ExpandAll();
        }
    }

    /// <summary>
    /// 收合所有節點
    /// </summary>
    public void CollapseAll()
    {
        ViewState["_IsExpandAll"] = false;
        ViewState["_IsCollapseAll"] = true;
        //處理根節點
        setRootNodeJS();
        if (MainTree.Nodes.Count > 0)
        {
            MainTree.Nodes[0].CollapseAll();
            if (ucIsHideRootNode)
            {
                MainTree.Nodes[0].Expand();
            }
        }
    }

    #endregion

    #region 產生 TreeView
    /// <summary>
    /// 清除所有設定
    /// </summary>
    public void Reset()
    {
        this.ViewState.Clear();
    }

    /// <summary>
    /// 根節點處理JS 
    /// </summary>
    protected void setRootNodeJS()
    {
        //2016.12.07 獨立出來
        if (ucIsHideRootNode)
        {
            string strJS = "";
            strJS += "dom.Ready(function () { \n";
            strJS += "  oRootNode = document.getElementById('" + MainTree.ClientID + "n0');   \n";
            strJS += "  if (oRootNode) oRootNode.parentNode.parentNode.style.display = 'none'; \n";
            strJS += "}); \n";
            Util.setJSContent(strJS, this.ClientID + "_RootNode");
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    /// <param name="IsReset">是否重置</param>
    public void Refresh(bool IsReset = true)
    {
        MainTree.Style["display"] = "none";

        if (ViewState["_IsExpandAll"] == null)
        {
            ViewState["_IsExpandAll"] = false;
        }

        if (ViewState["_IsCollapseAll"] == null)
        {
            ViewState["_IsCollapseAll"] = false;
        }

        if (ViewState["_IsSelectedNode"] == null)
        {
            ViewState["_IsSelectedNode"] = false;
        }

        if (string.IsNullOrEmpty(ucRootNodeID))
        {
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "參數錯誤 ,  需要 [ucRootNodeID]  及 ( [ucDBName] / [ucNodeInfoData]二擇一 ) ");
            labErrMsg.Visible = true;
            MainTree.Visible = false;
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
                        labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg);
                    }
                }
                else
                {
                    //SQL 
                    labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, "[NodeInfo] 資料表內未發現符合條件的資料 !");
                }
                labErrMsg.Visible = true;
                MainTree.Visible = false;
                return;
            }
        }

        //重置節點
        if (IsReset)
        {
            MainTree.Nodes.Clear();
        }

        //檢查指定選取節點是否存在(需排除 ucRootNodeID)
        if (!string.IsNullOrEmpty(ucSelectedNodeID))
        {
            if (ucSelectedNodeID.ToUpper().Trim() != ucRootNodeID.ToUpper().Trim())
            {
                if (getNodeInfoData().Select(string.Format(" NodeID = '{0}' ", ucSelectedNodeID)).Count() > 0)
                {
                    ViewState["_IsSelectedNode"] = true;
                    _IsAjax = false;
                }
            }
        }

        //是否使用 Ajax
        if (_IsAjax)
        {
            Util.setTreeViewAjaxLoadingIcon(MainTree);
            MainTree.ExpandDepth = 0;
        }
        else
        {
            MainTree.ExpandDepth = 99;
        }

        //TreeView 指定 CSS
        if (!string.IsNullOrEmpty(ucCssClass))
        {
            MainTree.CssClass = ucCssClass;
        }

        imgCollapseAll.ImageUrl = ucCollapseAllImageUrl;
        imgExpandAll.ImageUrl = ucExpandAllImageUrl;
        divTreeToolBar.Visible = ucIsToolBarEnabled;
        MainTree.ShowLines = ucIsShowLines;
        MainTree.CollapseImageUrl = ucCollapseImageUrl;
        MainTree.ExpandImageUrl = ucExpandImageUrl;
        MainTree.NoExpandImageUrl = ucNoExpandImageUrl;

        DataTable dtWholeTree = getWholeNodeData(ucRootNodeID);
        if (dtWholeTree == null)
        {
            MainTree.Visible = false;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg);
            labErrMsg.Visible = true;
            return;
        }

        DataRow[] drRoot = dtWholeTree.Select(string.Format("ParentNodeID = '' And TargetUrl = '' And NodeID = '{0}' ", ucRootNodeID));
        if (drRoot.Count() > 0)
        {
            //處理根節點
            TreeNode oRootNode = new TreeNode();
            oRootNode.Text = drRoot[0]["NodeName"].ToString();
            oRootNode.Value = drRoot[0]["NodeID"].ToString();
            if (!string.IsNullOrEmpty(drRoot[0]["ToolTip"].ToString()))
            {
                oRootNode.ToolTip = drRoot[0]["ToolTip"].ToString();
            }

            oRootNode.PopulateOnDemand = false;
            oRootNode.Expanded = true;
            oRootNode.SelectAction = TreeNodeSelectAction.Expand;
            MainTree.Nodes.Add(oRootNode);

            //產生子節點
            BuildChildNode(oRootNode);

            //處理根節點
            setRootNodeJS();

            if ((bool)ViewState["_IsExpandAll"])
            {
                ExpandAll();
            }

            if ((bool)ViewState["_IsCollapseAll"])
            {
                CollapseAll();
            }

        }
        else
        {
            labErrMsg.Visible = true;
            MainTree.Visible = false;
            divTreeToolBar.Visible = false;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, ucDataNotFoundMsg);
        }

    }

    /// <summary>
    /// 產生子節點
    /// </summary>
    /// <param name="oParentNode"></param>
    protected void BuildChildNode(TreeNode oParentNode, bool IsBuildEmptyNode = false)
    {
        DataTable dtNodeInfo = getNodeInfoData();
        DataRow[] drNodes = dtNodeInfo.Select(string.Format("ParentNodeID = '{0}' ", oParentNode.Value), " OrderSeq Asc, NodeID Asc ");

        if (drNodes.Count() > 0)
        {
            for (int i = 0; i < drNodes.Count(); i++)
            {
                TreeNode tChildNode = getSingleNode(drNodes[i]);
                oParentNode.ChildNodes.Add(tChildNode);
                if (!_IsAjax)
                {
                    //非Ajax 才立即產生子節點
                    BuildChildNode(tChildNode);
                }
            }
        }
        else
        {
            if (IsBuildEmptyNode)
            {
                TreeNode tmpNode = new TreeNode();
                tmpNode.Text = ucDataNotFoundMsg;
                tmpNode.NavigateUrl = "javascript:void(0);";
                oParentNode.ChildNodes.Add(tmpNode);
            }
        }
    }

    /// <summary>
    /// 產生單一節點
    /// </summary>
    /// <param name="drNode"></param>
    /// <returns></returns>
    protected TreeNode getSingleNode(DataRow drNode)
    {
        TreeNode oNode = new TreeNode();
        bool IsExpandNode = false;
        if (drNode == null) return oNode;

        oNode.Text = drNode["NodeName"].ToString().Trim();
        oNode.Value = drNode["NodeID"].ToString().Trim();

        if (oNode.Value == ucSelectedNodeID)
        {
            if ((bool)ViewState["_IsSelectedNode"])
            {
                oNode.Selected = true;
            }
        }

        if (!string.IsNullOrEmpty(drNode["ToolTip"].ToString()))
        {
            oNode.ToolTip = drNode["ToolTip"].ToString();
        }

        if (string.IsNullOrEmpty(drNode["TargetUrl"].ToString().Trim()))
        {
            if (getNodeInfoData().Select(string.Format(" ParentNodeID = '{0}' ", drNode["NodeID"].ToString())).Count() > 0)
            {
                IsExpandNode = true;
            }
        }

        if (IsExpandNode)
        {
            //展開項目
            if (_IsAjax)
            {
                //Ajax 模式時，由於子項目還未產生，故必為收合狀態
                oNode.Expanded = false;
                oNode.PopulateOnDemand = true;
            }
            else
            {
                //非Ajax模式下，所有節點已產生，適合套用自動展開的判斷
                oNode.Expanded = true;
                oNode.PopulateOnDemand = false;
            }
            oNode.SelectAction = TreeNodeSelectAction.Expand;
        }
        else
        {
            //程式連結
            oNode.PopulateOnDemand = false;
            oNode.NavigateUrl = Util.getUrlJSContent(drNode["TargetUrl"].ToString(), drNode["TargetPara"].ToString().Replace("＆", "_._"), ucIsPopup, drNode["NodeName"].ToString(), ucTargetFrame, ucPopupCfgSetting);

            if (string.IsNullOrEmpty(drNode["ImageUrl"].ToString()))
            {
                oNode.ImageUrl = ucNodeDefaultImageUrl;
            }
            else
            {
                oNode.ImageUrl = Util.getFixURL(drNode["ImageUrl"].ToString(), false);
            }

            if (!ucIsPopup && !string.IsNullOrEmpty(ucTargetFrame))
            {
                //更改視窗標題
                oNode.NavigateUrl = oNode.NavigateUrl.Replace("javascript:", string.Format("javascript:top.frames['top'].document.title='[{0}]';", drNode["NodeName"].ToString()));
            }
        }

        return oNode;
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
                _IsAjax = false; //自動關閉Ajax模式，以提昇效能
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