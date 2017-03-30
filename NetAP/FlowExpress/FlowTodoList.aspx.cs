using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// 流程待辦清單公用程式
/// </summary>
public partial class FlowExpress_FlowTodoList : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //取出使用者流程待辦清單(所有角色，含代理及流程群組)　2014.09.26
        bool IsReBuildEnabled = (Util.getRequestQueryStringKey("IsReBuildEnabled", "Y", true) == "Y") ? true : false; //是否能重建指派清單(預設 true)
        string strFlowIDList = Util.getRequestQueryStringKey("FlowIDList");

        if (!IsPostBack)
        {
            //設定待辦清單屬性
            if (string.IsNullOrEmpty(strFlowIDList))
            {
                labErrMag.Visible = true;
                labErrMag.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError, string.Format(RS.Resources.Msg_ParaError1, "FlowIDList"));
                ucFlowTodoList1.Visible = false;
            }
            else 
            {
                //ucFlowTodoList1.ucProxyFullFilter = "ProxyType='Full' and ProxyCompID = 'TW' "; //從UserInfo.ProxyInfoTable過濾出指定代理人員
                //ucFlowTodoList1.ucProxySemiFilter = "ProxyType='Semi' and ProxyCompID = 'TW' "; //從UserInfo.ProxyInfoTable過濾出指定助理人員
                ucFlowTodoList1.ucIsEnabledUserReBuild = IsReBuildEnabled; //是否能重建指派清單
                ucFlowTodoList1.ucFlowIDList = strFlowIDList.Split(',');
                ucFlowTodoList1.Refresh(true);
            }
        }
    }
}