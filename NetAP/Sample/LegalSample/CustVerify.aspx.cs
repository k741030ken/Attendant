using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;

/// <summary>
/// 流程自訂審核範例
/// </summary>
public partial class Sample_LegalSample_CustVerify : SecurePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["FlowVerifyInfo"] != null)
            {
                //取出審核用參數物件(取完便立即清空，以防重用)
                Dictionary<string, string> oVerifyInfo = (Dictionary<string, string>)Session["FlowVerifyInfo"];
                Session["FlowVerifyInfo"] = null;

                //撰寫流程審核前，需處理的前置工作或檢查

                //若前置工作/檢查成功，開始流程審核
                if (FlowExpress.IsFlowVerify(Util.getRequestQueryStringKey("FlowID"), Util.getRequestQueryStringKey("FlowLogID"), oVerifyInfo["FlowStepBtnID"], Util.getDictionary(oVerifyInfo["FlowStepAssignToList"]), oVerifyInfo["FlowStepOpinion"]))
                {
                    //流程審核成功時的處理
                    //switch (oVerifyInfo["FlowStepBtnID"].ToString())  //針對特定[審核按鈕]作不同處理
                    //{
                    //    case "btn01":
                    //        break;
                    //    case "btn02":
                    //        break;
                    //    default:
                    //        break;
                    //}

                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed, "自訂審核成功");
                }
                else
                {
                    //流程審核失敗時的處理
                    labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "自訂審核失敗");
                }

                //無論審核成功與否，均顯示審核視窗的「關閉」按鈕物件，以便使用者關閉視窗
                Util.setJSContent(oVerifyInfo["FlowVerifyJS"]);
            }
            else
            {
                //參數錯誤
                labMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaError);
            }
        }
    }
}