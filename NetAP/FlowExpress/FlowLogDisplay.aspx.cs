using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using WorkRS = SinoPac.WebExpress.Work.Properties;

/// <summary>
/// 流程記錄顯示公用程式
/// </summary>
public partial class FlowExpress_FlowLogDisplay : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strFlowID = Util.getRequestQueryStringKey("FlowID");
        string strFlowCaseID = Util.getRequestQueryStringKey("FlowCaseID");
        string strFlowLogID = Util.getRequestQueryStringKey("FlowLogID");

        string strIsShowCaseHeader = Util.getRequestQueryStringKey("IsShowCaseHeader","Y",true);
        string strIsAutoFromRootCase = Util.getRequestQueryStringKey("IsAutoFromRootCase", "Y", true);
        string strIsDisplaySubFlow = Util.getRequestQueryStringKey("IsDisplaySubFlow", "Y", true);
        string strIsAutoRefresh = Util.getRequestQueryStringKey("IsAutoRefresh", "Y", true); //2017.02.07 新增

        ucFlowFullLogList1.ucFlowID = strFlowID;
        ucFlowFullLogList1.ucFlowCaseID = strFlowCaseID;
        ucFlowFullLogList1.ucFlowLogID = strFlowLogID;
        ucFlowFullLogList1.ucIsShowCaseHeader = (strIsShowCaseHeader == "Y") ? true : false;
        ucFlowFullLogList1.ucIsAutoFromRootCase = (strIsAutoFromRootCase == "Y") ? true : false;
        ucFlowFullLogList1.ucIsDisplaySubFlow = (strIsDisplaySubFlow == "Y") ? true : false;
        ucFlowFullLogList1.ucIsAutoRefresh = (strIsAutoRefresh == "Y") ? true : false;

        labLogTitle.Text = WorkRS.Resources.FlowVerifyTab_FlowFullLog;

        if (ucFlowFullLogList1.ucIsAutoRefresh)
        {
            //ucIsAutoRefresh = [true]
            labLogTitle.Style["cursor"] = "pointer";
            labLogTitle.Attributes.Add("title", WorkRS.Resources.FlowLogMsg_Print);
            labLogTitle.Attributes.Add("onclick", "javascript:Util_PrintBlock('" + divLogArea.ClientID + "');void(0);");
        }
        else
        {
            //ucIsAutoRefresh = [false]
            //2017.02.07 新增
            labWaiting.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Waiting, string.Format(RS.Resources.Msg_Waiting1, WorkRS.Resources.FlowVerifyTab_FlowFullLog));
            imgLogToggle.Visible = true;
            imgLogToggle.ImageUrl = Util.Icon_Expand;
            imgLogToggle.ToolTip = string.Format(RS.Resources.Msg_Expand1, labLogTitle.Text);
            btnToggle.OnClientClick = "Util_ToggleDisplay('" + labWaiting.ClientID + "');if (window.frameElement && window.frameElement.nodeName == 'IFRAME'){window.frameElement.height='220px;';}";
        }
    }

    /// <summary>
    /// 顯示全部記錄
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnToggle_Click(object sender, EventArgs e)
    {
        imgLogToggle.Visible = false;
        labWaiting.Visible = false;
        labLogTitle.Style["cursor"] = "pointer";
        labLogTitle.Attributes.Add("title", WorkRS.Resources.FlowLogMsg_Print);
        labLogTitle.Attributes.Add("onclick", "javascript:Util_PrintBlock('" + divLogArea.ClientID + "');void(0);");
        ucFlowFullLogList1.Refresh();
    }
}