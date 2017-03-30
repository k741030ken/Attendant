using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Util_CommUserAdmin : BasePage
{
    private string _PropKind = "User";
    private string _PropID = "CommUserList";

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setJS(Util._JSjQueryUrl, this.ClientID + "_jQuery");
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
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, RS.Resources.CommUser_SessionNotFound);
            return;
        }

        labEdit.Text = RS.Resources.CommUser_labEdit;
        btnSave.Text = RS.Resources.CommUser_btnSave;

        //設定Ajax關聯式下拉選單
        ucCascadingDropDown1.SetDefault();
        ucCascadingDropDown1.ucIsVerticalLayout = true;
        ucCascadingDropDown1.Refresh();

        //設定按鈕
        btnAddResult.Text = RS.Resources.CommCascadeSelect_AddResult;

        //初始傳回值欄位
        idCompID.Text = "";
        idDeptID.Text = "";
        idUserIDList.Text = "";
        idCompInfo.Text = "";
        idDeptInfo.Text = "";
        idUserInfoList.Text = "";

        idCompID.Style.Add("display", "none");
        idDeptID.Style.Add("display", "none");
        idUserIDList.Style.Add("display", "none");
        idCompInfo.Style.Add("display", "none");
        idDeptInfo.Style.Add("display", "none");
        idUserInfoList.Style.Add("display", "none");

        //處理現有常用人員清單
        defResultItem.Text = "";
        string strCurrCommUserList = UserInfo.getUserProperty(Session["UserID"].ToString(), _PropKind, _PropID)["PropJSON"].Trim();
        if (!string.IsNullOrEmpty(strCurrCommUserList))
        {
            Dictionary<string, string> oCurrList = Util.getDictionary(UserInfo.findUserName(strCurrCommUserList.Split(',')));
            //ListItem tmpItem;

            foreach (var pair in oCurrList)
            {
                defResultItem.Text += string.Format(Util._ReorderItemHtmlFormat, pair.Key, pair.Value);
            }

            Util.setJSContent("$(function() {" + this.ClientID + "_calData(); \n }); \n", this.ClientID + "_Selected_Init");
        }

        //btnAddResult
        StringBuilder strJS = new StringBuilder();
        strJS.Clear();
        strJS.Append("var chkValue = $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').val();"
            + "if (chkValue.length > 0){"
            + "  if ( $('#" + ResultItemContent.ClientID + " li:contains(\"' + chkValue + '\")').length < 1 ){"
            + "     $('#" + ResultItemContent.ClientID + "').append('" + string.Format(Util._ReorderItemHtmlFormat, "' + $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').val() + '", "' + $('#" + ucCascadingDropDown1.FindControl("ddl03").ClientID + " :selected').text() + '") + "');"
            + "     }else{alert(JS_Alert_RepeatChoose);}"
            + "  }"
            );
        strJS.Append(this.ClientID + "_calData();return false;");
        btnAddResult.OnClientClick = strJS.ToString();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            divEdit.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, RS.Resources.CommUser_SessionNotFound);
            return;
        }
        else
        {
            UserInfo.setUserProperty(Session["UserID"].ToString(), _PropKind, _PropID, null, null, null, idUserIDList.Text);
            Refresh();
            Util.NotifyMsg(string.Format(RS.Resources.Msg_Succeed1, btnSave.Text));
        }
    }

}