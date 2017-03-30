using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;

/// <summary>
/// 流程待辦數字公用程式
/// <para>**此程式可能會被外部系統用來取相關待辦數字，故需繼承自 BasePage，並在 [web.config] 設定排除SSO檢核</para>
/// </summary>
public partial class FlowExpress_FlowTodoQty : BasePage
{
    protected override void OnInitComplete(EventArgs e)
    {
        PageIsMetaTags = false; //本頁只輸出數字，故不產生 MetaTags
        base.OnInitComplete(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //取出使用者流程待辦數字(所有角色，含代理及流程群組)　2014.09.26
        string strUserID = Util.getRequestQueryStringKey("UserID");
        string strFlowIDList = Util.getRequestQueryStringKey("FlowIDList");
        //代理/助理過濾條件
        string strProxyFullFilter = Util.getRequestQueryStringKey("ProxyFullFilter"); //ex: "ProxyType='Full' and ProxyCompID = 'TW' "; //從UserInfo.ProxyInfoTable過濾出指定代理人員
        string strProxySemiFilter = Util.getRequestQueryStringKey("ProxySemiFilter"); //ex: "ProxyType='Semi' and ProxyCompID = 'TW' "; //從UserInfo.ProxyInfoTable過濾出指定助理人員

        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(strUserID) || string.IsNullOrEmpty(strFlowIDList))
            {
                //若參數有錯
                Response.Write("F");
            }
            else
            {
                if (UserInfo.Init(strUserID, true))
                {
                    Response.Write(FlowExpress.getFlowTodoQty(FlowExpress.TodoListAssignKind.All, UserInfo.getUserInfo(), strFlowIDList.Split(','), strProxyFullFilter, strProxySemiFilter));
                }
                else
                {
                    Response.Write("E");
                }
            }
        }
    }
}