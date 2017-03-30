using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;
using AclRS = SinoPac.WebExpress.Work.Properties;

public partial class AclExpress_Admin_AclMenu : SecurePage
{
    private string _AreaID = "";

    private string _ContentUrlFormat = "javascript:top.frames['frmContent'].location.href='{0}';void(0);";

    protected Dictionary<string, string> _Dic_AclAdminUserList
    {
        get
        {
            if (ViewState["_Dic_AclAdminUserList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_AclAdminUserList"];
            }
            else
            {
                ViewState["_Dic_AclAdminUserList"] = AclExpress.getAclAdminUserList();
                return (Dictionary<string, string>)ViewState["_Dic_AclAdminUserList"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AclInfo.Init(true);
        UserInfo oUser = UserInfo.getUserInfo();
        bool IsAllowAdmin = false;
        divError.Visible = true;
        divAdminArea.Visible = false;
        labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, AclRS.Resources.Msg_AclAdminDeny);

        if (Util.getAppSetting("app://AdminUserID/").ToUpper() == oUser.UserID.ToUpper()) { IsAllowAdmin = true; }
        if (!IsAllowAdmin && _Dic_AclAdminUserList != null && _Dic_AclAdminUserList.Count > 0)
        {
            if (_Dic_AclAdminUserList.ContainsKey(oUser.UserID))
            {
                IsAllowAdmin = true;
            }
        }

        if (!IsAllowAdmin)
        {
            string[] AreaList = AclInfo.getAclInfo().getAdminAreaList();
            if (AreaList != null && AreaList.Length > 0)
            {
                IsAllowAdmin = true;
            }
        }

        if (IsAllowAdmin)
        {
            _AreaID = Util.getRequestQueryStringKey("AreaID");
            divError.Visible = false;
            divAdminArea.Visible = true;
            RefreshTreeView();
        }
    }

    public void RefreshTreeView()
    {
        TreeView1.Nodes.Clear();
        TreeNode tRootNode = new TreeNode();
        tRootNode.Text = "ACL資料查詢";
        tRootNode.NavigateUrl = string.Format(_ContentUrlFormat, AclExpress._AclSysPath + "AclInfo.aspx");
        tRootNode.PopulateOnDemand = false;
        tRootNode.Expanded = true;
        TreeView1.Nodes.Add(tRootNode);

        //管理專用
        if (AclExpress.IsAclAdminUser())
        {
            //Acl 管理員專用
            TreeNode tManageNode = new TreeNode();
            tManageNode.Text = "管理作業";
            tManageNode.Expanded = true;
            tManageNode.SelectAction = TreeNodeSelectAction.Expand;
            tRootNode.ChildNodes.Add(tManageNode);

            TreeNode tBaseNode = new TreeNode();
            tBaseNode.Text = "基本資料";
            tBaseNode.Expanded = true;
            tBaseNode.SelectAction = TreeNodeSelectAction.Expand;
            tManageNode.ChildNodes.Add(tBaseNode);

            TreeNode tChildNode = new TreeNode();
            tChildNode.Text = "區域資料(AclArea)";
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclArea.aspx");
            tBaseNode.ChildNodes.Add(tChildNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "規則資料(AclRule)";
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclRule.aspx");
            tBaseNode.ChildNodes.Add(tChildNode);

            TreeNode tAdminNode = new TreeNode();
            tAdminNode.Text = "管理權";
            tAdminNode.Expanded = true;
            tAdminNode.SelectAction = TreeNodeSelectAction.Expand;
            tManageNode.ChildNodes.Add(tAdminNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "規則<->區域(AclAdminRuleArea)"; //AclAdminRuleArea
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclAdminRuleArea.aspx");
            tAdminNode.ChildNodes.Add(tChildNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "使用者<->區域(AclAdminUserArea)"; //AclAdminUserArea
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclAdminUserArea.aspx");
            tAdminNode.ChildNodes.Add(tChildNode);

            tAdminNode = new TreeNode();
            tAdminNode.Text = "使用權";
            tAdminNode.Expanded = true;
            tAdminNode.SelectAction = TreeNodeSelectAction.Expand;
            tManageNode.ChildNodes.Add(tAdminNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "規則<->區域(AclAuthRuleArea)"; //AclAuthRuleArea
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclAuthRuleArea.aspx");
            tAdminNode.ChildNodes.Add(tChildNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "使用者<->區域(AclAuthUserArea)"; //AclAuthUserArea
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, "AclAreaSelect.aspx");
            tAdminNode.ChildNodes.Add(tChildNode);

            tAdminNode = new TreeNode();
            tAdminNode.Text = "工具";
            tAdminNode.Expanded = true;
            tAdminNode.SelectAction = TreeNodeSelectAction.Expand;
            tManageNode.ChildNodes.Add(tAdminNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "CodeMap";
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, string.Format("{0}?DBName={1}&LogDBName={2}", Util._CodeMapAdminUrl, AclExpress._AclDBName,AclExpress._AclLogDBName));
            tAdminNode.ChildNodes.Add(tChildNode);

            tChildNode = new TreeNode();
            tChildNode.Text = "AppLog";
            tChildNode.PopulateOnDemand = false;
            tChildNode.Expanded = false;
            tChildNode.SelectAction = TreeNodeSelectAction.Select;
            tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, string.Format("{0}?DBName={1}&AllowPurgeYN=Y", Util._AppLogQryUrl, AclExpress._AclDBName));
            tAdminNode.ChildNodes.Add(tChildNode);

        }

        //一般授權作業
        string[] tAreaList = AclInfo.getAclInfo().getAdminAreaList();
        if (tAreaList != null && tAreaList.Length > 0)
        {
            string[] tGrantIDList = AclInfo.getAclInfo().getAdminAreaGrantList(tAreaList[0]);
            if (tGrantIDList != null && !string.IsNullOrEmpty(tGrantIDList[0]))
            {
                //確定有資料才產生 TreeNode
                TreeNode tAreaNode = new TreeNode();
                tAreaNode.Text = "一般授權";
                tAreaNode.Expanded = true;
                tAreaNode.SelectAction = TreeNodeSelectAction.Expand;
                tRootNode.ChildNodes.Add(tAreaNode);

                DataTable dtArea = AclExpress.getAclAreaData().Select(string.Format(" AreaID in ('{0}') and IsEnabled = 'Y' ", Util.getStringJoin(tAreaList, "','"))).CopyToDataTable();
                if (dtArea != null && dtArea.Rows.Count > 0)
                {

                    for (int i = 0; i < dtArea.Rows.Count; i++)
                    {
                        DataRow dr = dtArea.Rows[i];
                        //AreaID 節點
                        TreeNode tChildNode = new TreeNode();
                        tChildNode.PopulateOnDemand = false;
                        tChildNode.Text = string.Format("{0}【{1}】", dr["AreaID"], dr["AreaName"]);
                        tChildNode.NavigateUrl = string.Format(_ContentUrlFormat, string.Format("{0}?AreaID={1}&IsDebug=Y", AclExpress._AclSysPath + "AclAuthUserArea.aspx", dr["AreaID"]));
                        tChildNode.Expanded = false;
                        tChildNode.SelectAction = TreeNodeSelectAction.Select;
                        tAreaNode.ChildNodes.Add(tChildNode);
                        //AddStepNodes(tSpecNode, dr["FlowID"].ToString());
                    }
                }

            }
        }

    }

    protected void AddStepNodes(TreeNode tStepRootNode, string FlowID)
    {
        //string strStepSQL = "Select FlowStepID, FlowStepName From viewFlowStep Where FlowID='{0}' And CultureCode = '{1}' Order By FlowStepID";
        //strStepSQL = string.Format(strStepSQL, FlowID, UserInfo.getUserInfo().UICultureCode);
        //DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        //DataTable dtStep = db.ExecuteDataSet(CommandType.Text, strStepSQL).Tables[0];
        ////FlowStep 節點
        //for (int j = 0; j < dtStep.Rows.Count; j++)
        //{
        //    DataRow drStep = dtStep.Rows[j];
        //    string FlowStepID = drStep["FlowStepID"].ToString();
        //    string FlowStepDesc = drStep["FlowStepName"].ToString();

        //    TreeNode tStepNode = new TreeNode();
        //    tStepNode.Text = string.Format("{0}-{1}", FlowStepID, FlowStepDesc);
        //    tStepNode.NavigateUrl = string.Format("{0}?FlowID={1}&FlowStepID={2}", "FlowStep.aspx", FlowID, FlowStepID);
        //    tStepNode.PopulateOnDemand = true;
        //    tStepNode.Value = tStepNode.NavigateUrl.Split('?')[1];
        //    tStepNode.SelectAction = TreeNodeSelectAction.Expand;
        //    tStepNode.Expanded = false;
        //    if (FlowID == _FlowID && FlowStepID == _FlowStepID)
        //    {
        //        tStepNode.Expanded = true;
        //        if (string.IsNullOrEmpty(_FlowStepBtnID)) tStepNode.Selected = true;
        //    }
        //    tStepRootNode.ChildNodes.Add(tStepNode);
        //}
    }

    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        //Ajax 載入 FlowStepBtn 節點
        //AddStepBtnNodes(e.Node);
    }
}