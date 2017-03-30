using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

public partial class FlowExpress_Admin_FlowExpess : System.Web.UI.MasterPage
{

    private string _FlowID = "";
    private string _FlowStepID = "";
    private string _FlowStepBtnID = "";

    protected Dictionary<string, string> _Dic_FlowAdminUserList
    {
        get
        {
            if (ViewState["_Dic_FlowAdminUserList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_FlowAdminUserList"];
            }
            else
            {
                ViewState["_Dic_FlowAdminUserList"] = Util.getCodeMap(FlowExpress._FlowSysDB, "FlowExpress", "AdminUser");
                return (Dictionary<string, string>)ViewState["_Dic_FlowAdminUserList"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo oUser = UserInfo.getUserInfo();
        bool IsAllowAdmin = false;
        divError.Visible = true;
        divAdminArea.Visible = false;
        labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_AccessDenied1, oUser.UserID + " - " + oUser.UserName));

        if (Util.getAppSetting("app://AdminUserID/").ToUpper() == oUser.UserID.ToUpper()) { IsAllowAdmin = true; }
        if (!IsAllowAdmin && _Dic_FlowAdminUserList != null && _Dic_FlowAdminUserList.Count > 0)
        {
            if (_Dic_FlowAdminUserList.ContainsKey(oUser.UserID))
            {
                IsAllowAdmin = true;
            }
        }

        if (IsAllowAdmin)
        {
            _FlowID = Util.getRequestQueryStringKey("FlowID");
            _FlowStepID = Util.getRequestQueryStringKey("FlowStepID");
            _FlowStepBtnID = Util.getRequestQueryStringKey("FlowStepBtnID");

            divError.Visible = false;
            divAdminArea.Visible = true;
            RefreshTreeView();
        }
    }

    public void RefreshTreeView()
    {
        //流程TreeView
        TreeView1.Nodes.Clear();
        TreeNode tRootNode = new TreeNode();
        tRootNode.Text = "流程清單";
        tRootNode.NavigateUrl = "Default.aspx";
        tRootNode.PopulateOnDemand = false;
        tRootNode.Expanded = true;
        TreeView1.Nodes.Add(tRootNode);

        TreeNode tUtilNode = new TreeNode();
        tUtilNode.Text = "流程工具";
        tUtilNode.NavigateUrl = "FlowUtil.aspx";
        tRootNode.ChildNodes.Add(tUtilNode);

        //sw.Stop();
        //System.Diagnostics.Debug.WriteLine("RefreshTreeView() Time : " + sw.ElapsedMilliseconds);
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dt = db.ExecuteDataSet(CommandType.Text, string.Format("Select FlowID,FlowName From viewFlowSpec Where CultureCode = '{0}'", UserInfo.getUserInfo().UICultureCode)).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //FlowSpec 節點
            TreeNode tSpecNode = new TreeNode();
            tSpecNode.PopulateOnDemand = false;
            DataRow dr = dt.Rows[i];
            tSpecNode.Text = string.Format("{0}-{1}", dr["FlowID"], dr["FlowName"]);
            tSpecNode.NavigateUrl = string.Format("{0}?FlowID={1}", "FlowSpec.aspx", dr["FlowID"]);
            tSpecNode.Expanded = false;
            tSpecNode.SelectAction = TreeNodeSelectAction.Expand;
            if (_FlowID == dr["FlowID"].ToString())
            {
                tSpecNode.Expanded = true;
                if (string.IsNullOrEmpty(_FlowStepID)) tSpecNode.Selected = true;
            }
            tRootNode.ChildNodes.Add(tSpecNode);
            AddStepNodes(tSpecNode, dr["FlowID"].ToString());
        }

    }

    protected void AddStepNodes(TreeNode tStepRootNode, string FlowID)
    {
        string strStepSQL = "Select FlowStepID, FlowStepName From viewFlowStep Where FlowID='{0}' And CultureCode = '{1}' Order By FlowStepID";
        strStepSQL = string.Format(strStepSQL, FlowID, UserInfo.getUserInfo().UICultureCode);
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dtStep = db.ExecuteDataSet(CommandType.Text, strStepSQL).Tables[0];
        //FlowStep 節點
        for (int j = 0; j < dtStep.Rows.Count; j++)
        {
            DataRow drStep = dtStep.Rows[j];
            string FlowStepID = drStep["FlowStepID"].ToString();
            string FlowStepDesc = drStep["FlowStepName"].ToString();

            TreeNode tStepNode = new TreeNode();
            tStepNode.Text = string.Format("{0}-{1}", FlowStepID, FlowStepDesc);
            tStepNode.NavigateUrl = string.Format("{0}?FlowID={1}&FlowStepID={2}", "FlowStep.aspx", FlowID, FlowStepID);
            tStepNode.PopulateOnDemand = true;
            tStepNode.Value = tStepNode.NavigateUrl.Split('?')[1];
            tStepNode.SelectAction = TreeNodeSelectAction.Expand;
            tStepNode.Expanded = false;
            if (FlowID == _FlowID && FlowStepID == _FlowStepID)
            {
                tStepNode.Expanded = true;
                if (string.IsNullOrEmpty(_FlowStepBtnID)) tStepNode.Selected = true;
            }
            tStepRootNode.ChildNodes.Add(tStepNode);
        }
    }

    protected void TreeView1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        //Ajax 載入 FlowStepBtn 節點
        AddStepBtnNodes(e.Node);
    }

    protected void AddStepBtnNodes(TreeNode tStepNode)
    {
        // FlowStepBtn 節點
        string tParaList = string.IsNullOrEmpty(tStepNode.NavigateUrl) ? tStepNode.Value : tStepNode.NavigateUrl.Split('?')[1];
        string[] arParas = tParaList.Split('&');
        string FlowID = "";
        string FlowStepID = "";
        for (int i = 0; i < arParas.Count(); i++)
        {
            string tKey = arParas[i].Split('=')[0];
            string tValue = arParas[i].Split('=')[1];
            if (tKey.ToUpper() == "FLOWID") FlowID = tValue;
            if (tKey.ToUpper() == "FLOWSTEPID") FlowStepID = tValue;
        }
        AddOneStepBtnNodes(tStepNode, FlowID, FlowStepID);
    }

    protected void AddOneStepBtnNodes(TreeNode tStepNode, string FlowID, string FlowStepID)
    {
        // FlowStepBtn 節點
        string strStepBtnSQL = "Select * From viewFlowStepBtn Where FlowID='{0}' And FlowStepID = '{1}' And CultureCode = '{2}' Order By FlowStepBtnSeqNo, FlowStepBtnID";
        strStepBtnSQL = string.Format(strStepBtnSQL, FlowID, FlowStepID, UserInfo.getUserInfo().UICultureCode);
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dtStepBtn = db.ExecuteDataSet(CommandType.Text, strStepBtnSQL).Tables[0];
        for (int r = 0; r < dtStepBtn.Rows.Count; r++)
        {
            DataRow dr = dtStepBtn.Rows[r];
            TreeNode tStepBtnNode = new TreeNode();
            tStepBtnNode.Text = dr["FlowStepBtnCaption"].ToString();
            tStepBtnNode.NavigateUrl = string.Format("{0}?FlowID={1}&FlowStepID={2}&FlowStepBtnID={3}", "FlowStepBtn.aspx", FlowID, FlowStepID, dr["FlowStepBtnID"].ToString());
            if (FlowID == _FlowID && FlowStepID == _FlowStepID && _FlowStepBtnID == dr["FlowStepBtnID"].ToString())
                tStepBtnNode.Selected = true;
            tStepNode.ChildNodes.Add(tStepBtnNode);
        }
    }

}
