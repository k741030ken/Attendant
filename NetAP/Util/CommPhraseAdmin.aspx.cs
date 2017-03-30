using System;
using System.Collections.Generic;
using System.Linq;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Util_CommPhraseAdmin : BasePage
{
    private string _PropKind = "User";
    private string _PropID = "CommPhrase";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh();
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        if (Session["UserID"] == null)
        {
            divEdit.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, SinoPac.WebExpress.Common.Properties.Resources.CommPhrase_SessionNotFound);
            return;
        }

        labEdit.Text = RS.Resources.CommPhrase_labEdit;
        btnSave.Text = RS.Resources.CommPhrase_btnSave;
        txtPhrase.ucTextData = string.Empty;

        Dictionary<string, string> dicPhrase = Util.getDictionary(UserInfo.getUserProperty(UserInfo.getUserInfo().UserID, _PropKind, _PropID)["PropJSON"]);
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
        if (Session["UserID"] == null)
        {
            divEdit.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, SinoPac.WebExpress.Common.Properties.Resources.CommPhrase_SessionNotFound);
            return;
        }
        else
        {
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
            UserInfo.setUserProperty(UserInfo.getUserInfo().UserID, _PropKind, _PropID, null, null, null, strJSON);
            Util.NotifyMsg(string.Format(RS.Resources.Msg_Succeed1, btnSave.Text));
        }
    }

}