using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

public partial class LegalSample_DocQry : SecurePage
{
    //目前查詢條件SQL
    private string _QryResultSQL
    {
        get
        {
            if (ViewState["_QryResultSQL"] == null) { ViewState["_QryResultSQL"] = ""; }
            return (string)(ViewState["_QryResultSQL"]);
        }
        set
        {
            ViewState["_QryResultSQL"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        ucCascadingDropDown1.ucServiceMethod = LegalSample._LegalDocKindServiceMethod;
        ucCascadingDropDown1.ucCategory01 = "Kind1";
        ucCascadingDropDown1.ucCategory02 = "Kind2";
        ucCascadingDropDown1.ucCategory03 = "Kind3";
        ucCascadingDropDown1.ucDropDownListEnabled01 = true;
        ucCascadingDropDown1.ucDropDownListEnabled02 = true;
        ucCascadingDropDown1.ucDropDownListEnabled03 = true;
        ucCascadingDropDown1.ucDropDownListEnabled04 = false;
        ucCascadingDropDown1.ucDropDownListEnabled05 = false;
        ucGridView1.RowCommand += new Util_ucGridView.GridViewRowClick(ucGridView1_RowCommand);
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);

        if (!IsPostBack)
        {
            QryReset();
        }
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        ucGridView1.Refresh();
    }

    void ucGridView1_RowCommand(object sender, Util_ucGridView.RowCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "cmdSelect":
                ucModalPopup1.ucPopupWidth = 750;
                ucModalPopup1.ucPopupHeight = 550;
                ucModalPopup1.ucFrameURL = "DocAdmin.aspx?QryMode=Y&DocNo=" + e.DataKeys[0];
                ucModalPopup1.Show();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// [清除]按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        _QryResultSQL = "";
        DivQryResult.Visible = false;
        QryReset();
    }

    /// <summary>
    /// [查詢]按鈕
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQry_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.Append(string.Format("Select DocNo,Subject,Kind1Name,Kind2Name,Kind3Name,UpdUserName,UpdDateTime From view{0} Where IsRelease = 'Y' ", LegalSample._LegalDocTableName));
        //DocNo
        if (!string.IsNullOrEmpty(txtDocNo.Text))
        {
            sb.Append(" And DocNo = ").AppendParameter("DocNo", txtDocNo.Text.Trim());
        }

        //Kind1/Kind2/Kind3
        if (!string.IsNullOrEmpty(ucCascadingDropDown1.ucSelectedValue01))
        {
            ucCascadingDropDown1.ucDefaultSelectedValue01 = ucCascadingDropDown1.ucSelectedValue01;
            sb.Append(" And Kind1 = ").AppendParameter("Kind1", ucCascadingDropDown1.ucSelectedValue01);

            if (!string.IsNullOrEmpty(ucCascadingDropDown1.ucSelectedValue02)) 
            {
                ucCascadingDropDown1.ucDefaultSelectedValue02 = ucCascadingDropDown1.ucSelectedValue02;
                sb.Append(" And Kind2 = ").AppendParameter("Kind2", ucCascadingDropDown1.ucSelectedValue02);            
            }

            if (!string.IsNullOrEmpty(ucCascadingDropDown1.ucSelectedValue03))
            {
                ucCascadingDropDown1.ucDefaultSelectedValue03 = ucCascadingDropDown1.ucSelectedValue03;
                sb.Append(" And Kind3 = ").AppendParameter("Kind3", ucCascadingDropDown1.ucSelectedValue03);
            }            
            ucCascadingDropDown1.Refresh();
        }

        //Subject
        if (!string.IsNullOrEmpty(txtSubject.Text))
        {
            sb.Append(" And Subject Like N").AppendParameter("Subject", string.Format("%{0}%", txtSubject.Text.Trim()));
        }

        //Keyword
        if (!string.IsNullOrEmpty(txtKeyword.Text))
        {
            string[] keyList = txtKeyword.Text.Trim().Split(' ');
            sb.Append(" And (");
            for (int i = 0; i < keyList.Count();i++ )
            {
                if (!string.IsNullOrEmpty(keyList[i])) 
                {
                    if (i > 0)
                    {
                        sb.Append(" or ");
                    }

                    sb.Append(" Keyword Like N").AppendParameter("Key" + i, string.Format("%{0}%", keyList[i]));
                }
            }
            sb.Append(" ) ");
        }

        //更新查詢條件SQL
        _QryResultSQL = Util.getPureSQL(sb);
        //顯示查詢結果
        ShowQryResult();
    }

    /// <summary>
    /// 重置查詢條件
    /// </summary>
    protected void QryReset()
    {

        txtDocNo.Text = "";
        txtSubject.Text = "";
        txtKeyword.Text = "";
        ucCascadingDropDown1.ucDefaultSelectedValue01 = "";
        ucCascadingDropDown1.ucDefaultSelectedValue02 = "";
        ucCascadingDropDown1.ucDefaultSelectedValue03 = "";
        ucCascadingDropDown1.Refresh();
    }

    /// <summary>
    /// 顯示查詢結果
    /// </summary>
    protected void ShowQryResult()
    {
        DivQryResult.Visible = true;
        //設定GridView
        ucGridView1.ucDBName = LegalSample._LegalSysDBName;
        ucGridView1.ucDataQrySQL = _QryResultSQL;
        ucGridView1.ucDataKeyList = "DocNo".Split(',');
        ucGridView1.ucDefSortExpression = "DocNo";
        ucGridView1.ucDefSortDirection = "Desc";


        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        dicDisplay.Clear();
        dicDisplay.Add("DocNo", (string)GetLocalResourceObject("_DocNo")); //文件編號
        dicDisplay.Add("Subject", (string)GetLocalResourceObject("_Subject") + "@L250"); //文件名稱
        dicDisplay.Add("Kind2Name", (string)GetLocalResourceObject("_Kind2Name"));  //中類
        dicDisplay.Add("Kind3Name", (string)GetLocalResourceObject("_Kind3Name"));  //小類
        dicDisplay.Add("UpdUserName", (string)GetLocalResourceObject("_UpdUserName")); //更新人員
        dicDisplay.Add("UpdDateTime", (string)GetLocalResourceObject("_UpdDateTime") + "@T"); //最後更新
        ucGridView1.ucDataDisplayDefinition = dicDisplay;

        ucGridView1.ucDataGroupKey = "Kind1Name";

        ucGridView1.ucSelectEnabled = true;
        ucGridView1.ucSortEnabled = true;

        //重新整理GridView
        ucGridView1.Refresh(true);
    }
}