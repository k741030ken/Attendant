using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;

public partial class LegalSample_CaseQry : SecurePage
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
        ucCascadingDropDown1.SetDefault();
        ucCascadingDropDown1.ucDropDownListEnabled03 = false;
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
                ucModalPopup1.ucPopupWidth = 770;
                ucModalPopup1.ucPopupHeight = 600;
                ucModalPopup1.ucFrameURL = "CaseInfo.aspx?CaseNo=" + e.DataKeys[0];
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
        DbHelper db = new DbHelper( LegalSample._LegalSysDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.Append(string.Format("Select CaseNo,Subject,IsUrgent,CaseCurrFlowStepName,UpdDateTime,UpdUserName,UpdLegalUserName From {0} Where CaseStatus <> 'Draft' ", LegalSample._LegalConsultCaseTableName));

        //CaseNo
        if (!string.IsNullOrEmpty(txtCaseNo.Text))
        {
            sb.Append(" And CaseNo = ").AppendParameter("CaseNo", txtCaseNo.Text.Trim());
        }

        //UpdDept
        if (!string.IsNullOrEmpty(ucCascadingDropDown1.ucSelectedValue02))
        {
            ucCascadingDropDown1.ucDefaultSelectedValue01 = ucCascadingDropDown1.ucSelectedValue01;
            ucCascadingDropDown1.ucDefaultSelectedValue02 = ucCascadingDropDown1.ucSelectedValue02;
            ucCascadingDropDown1.Refresh();

            sb.Append(" And UpdDept = ").AppendParameter("UpdDept", ucCascadingDropDown1.ucSelectedValue02);
        }

        //UpdDateTime
        if (!string.IsNullOrEmpty(ucQryDate1.ucSelectedDate))
        {
            if (string.IsNullOrEmpty(ucQryDate2.ucSelectedDate))
            {
                sb.Append(" And UpdDateTime = ").AppendParameter("UpdDateTime1", ucQryDate1.ucSelectedDate);
            }
            else
            {
                if (string.Compare(ucQryDate1.ucSelectedDate, ucQryDate2.ucSelectedDate) > 0)
                {
                    //若起始日期　大於　終止日期，則自動交換
                    string tmpDate = ucQryDate2.ucSelectedDate;
                    ucQryDate2.ucSelectedDate = ucQryDate1.ucSelectedDate;
                    ucQryDate1.ucSelectedDate = tmpDate;
                }
                sb.Append(" And ( ");
                sb.Append(" UpdDateTime Between ").AppendParameter("UpdDateTime1", ucQryDate1.ucSelectedDate + " 00:00:00");
                sb.Append(" And ").AppendParameter("UpdDateTime2", ucQryDate2.ucSelectedDate + " 23:59:59");
                sb.Append(" ) ");
            }
        }

        //Subject
        if (!string.IsNullOrEmpty(txtSubject.Text))
        {
            sb.Append(" And Subject Like N").AppendParameter("Subject", string.Format("%{0}%", txtSubject.Text.Trim()));
        }

        //UpdUserName
        if (!string.IsNullOrEmpty(txtUpdUserName.Text))
        {
            sb.Append(" And UpdUserName Like N").AppendParameter("UpdUserName", string.Format("%{0}%", txtUpdUserName.Text.Trim()));
        }

        //LegalUserName
        if (!string.IsNullOrEmpty(txtUpdLegalUserName.Text))
        {
            sb.Append(" And UpdLegalUserName Like N").AppendParameter("UpdLegalUserName", string.Format("%{0}%", txtUpdLegalUserName.Text.Trim()));
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
        txtCaseNo.Text = "";
        txtSubject.Text = "";
        txtUpdUserName.Text = "";
        txtUpdLegalUserName.Text = "";
        
        //提案單位
        UserInfo oUser = UserInfo.getUserInfo();
        ucCascadingDropDown1.ucDefaultSelectedValue01 = oUser.CompID;
        ucCascadingDropDown1.ucDefaultSelectedValue02 = oUser.DeptID;
        ucCascadingDropDown1.Refresh();

        //提案區間
        ucQryDate1.ucDefaultSelectedDate = DateTime.Today.AddDays(-180);
        ucQryDate2.ucDefaultSelectedDate = DateTime.Today;
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
        ucGridView1.ucDataKeyList = "CaseNo".Split(',');
        ucGridView1.ucDefSortExpression = "CaseNo";
        ucGridView1.ucDefSortDirection = "Desc";


        Dictionary<string, string> dicDisplay = new Dictionary<string, string>();
        dicDisplay.Clear();
        dicDisplay.Add("CaseNo", (string)GetLocalResourceObject("_CaseNo")); //案號
        dicDisplay.Add("Subject", (string)GetLocalResourceObject("_Subject") + "@L350"); //提案主題
        dicDisplay.Add("IsUrgent", (string)GetLocalResourceObject("_IsUrgent")); //急件
        dicDisplay.Add("CaseCurrFlowStepName", (string)GetLocalResourceObject("_CaseCurrFlowStepName"));  //審核進度
        dicDisplay.Add("UpdDateTime", (string)GetLocalResourceObject("_UpdDateTime") + "@T"); //最後更新
        dicDisplay.Add("UpdUserName", (string)GetLocalResourceObject("_UpdUserName")); //提案人
        dicDisplay.Add("UpdLegalUserName", (string)GetLocalResourceObject("_UpdLegalUserName")); //承辦人
        ucGridView1.ucDataDisplayDefinition = dicDisplay;

        ucGridView1.ucDisplayOnly = true;         //隱藏按鈕區
        ucGridView1.ucSelectRowEnabled = true;    //使用整列選取
        ucGridView1.ucSortEnabled = true;
        //ucGridView1.ucExportEnabled = true;
        //ucGridView1.ucExportName = string.Format("LegalConsult_{0}.xls", DateTime.Today.ToString("yyyyMMdd"));

        //重新整理GridView
        ucGridView1.Refresh(true);
    }
}