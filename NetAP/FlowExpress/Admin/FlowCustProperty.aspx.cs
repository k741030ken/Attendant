using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Work;
using RS = SinoPac.WebExpress.Common.Properties;


public partial class FlowExpress_Admin_FlowCustProperty : SecurePage
{
    private string _PKKind = "FlowCustProperty";
    private string _FlowID
    {
        get
        {
            return Util.getRequestQueryStringKey("FlowID");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setRequestValidatorBypassIDList("*");
        if (string.IsNullOrEmpty(_FlowID))
        {
            divMain.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.ParaDataError, string.Format(RS.Resources.Msg_ParaNotFound1, "FlowID"));
            return;
        }

        if (!IsPostBack)
        {
            labMailFrom.Text = string.Format("{0} 〈{1}〉", Util.getAppSetting("app://CfgMailFromName/"), Util.getAppSetting("app://CfgMailFromAddr/")); //預設 MailFrom 
            Refresh();
        }

    }

    protected void Refresh()
    {
        //MailFrom
        txtMailFrom.ucTextData = FlowExpress.getFlowCustProperty(_FlowID, "MailFrom")["Prop1"];

        //Phrase
        txtPhrase.ucTextData = string.Empty;
        Dictionary<string, string> dicPhrase = Util.getDictionary(FlowExpress.getFlowCustProperty(_FlowID, "Phrase")["PropJSON"]);
        if (!dicPhrase.IsNullOrEmpty())
        {
            string strTextData = string.Empty;
            foreach (var pair in dicPhrase)
            {
                if (strTextData.Length > 0)
                    strTextData += "\n";

                strTextData += pair.Value;
            }
            txtPhrase.ucTextData = strTextData;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //MailFrom 
        Util.setCustProperty(FlowExpress._FlowSysDB, _FlowID, _PKKind, "MailFrom", txtMailFrom.ucTextData);

        //Phrase
        string[] arList = (!String.IsNullOrEmpty(txtPhrase.ucTextData)) ? txtPhrase.ucTextData.Split('\n') : null;
        string strJSON = "";
        if (arList != null)
        {
            if (arList.Count() > 0)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                for (int i = 0; i < arList.Count(); i++)
                {
                    dic.Add((i + 1).ToString(), arList[i].Trim());
                }
                strJSON = Util.getJSON(dic);
            }
        }
        Util.setCustProperty(FlowExpress._FlowSysDB, _FlowID, _PKKind, "Phrase", null, null, null, strJSON);

        Util.NotifyMsg(RS.Resources.Msg_EditSucceed, Util.NotifyKind.Success);
    }
}