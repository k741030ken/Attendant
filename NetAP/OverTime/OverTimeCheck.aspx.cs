using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Data;

using RS = SinoPac.WebExpress.Common.Properties;
using System.Data.Common;
using System.Drawing;
using SinoPac.WebExpress.Work;

public partial class OverTime_OverTimeCheck : SecurePage
{
    public string _CurrFlowID
    {
        get
        {
            if (ViewState["_CurrFlowID"] == null)
            {
                ViewState["_CurrFlowID"] = (Request["FlowID"] != null) ? Request["FlowID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowID"]);
        }
        set
        {
            ViewState["_CurrFlowID"] = value;
        }
    }
    public string _CurrFlowLogID
    {
        get
        {
            if (ViewState["_CurrFlowLogID"] == null)
            {
                ViewState["_CurrFlowLogID"] = (Request["FlowLogID"] != null) ? Request["FlowLogID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowLogID"]);
        }
        set
        {
            ViewState["_CurrFlowLogID"] = value;
        }
    }
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    //private string _DBName2 = "DB_VacSys";
    //public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
    private string FlowCustDB="";

    protected void ucModalPopup_btnClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        LoadData();
        ucModalPopup.Reset();
        Session["FlowVerifyInfo"] = null;
        Session["dtOverTimeAdvance"] = null;
        Session["dtOverTimeDeclaration"] = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress(_DBName);
        FlowCustDB = oFlow.FlowCustDB;
        Session["A30"] = 1;
        if (!IsPostBack)
        {
            LoadData();
            Session["dtOverTimeAdvance"] = null;
            Session["dtOverTimeDeclaration"] = null;
        }
        ucModalPopup.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup_btnClose);
    }
    protected void LoadData()
    {
       
        gvMain.Visible = true;

        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //註：上頭宣告的public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
        //上面_DBShare會從App.Config取得字串
        //接著再把字串丟入DbHelper建立資料庫連接
        //若DB.Config設定的_DBShare沒錯應該就會連過去

        //DbHelper db2 = new DbHelper(_DBShare);
        CommandHelper sb2 = db.CreateCommandHelper();
        //若想確認代理人是否有撈取到請檢視sb2的SQL語法，sb是撈取待辦清單

        sb2.Append("select UserID from PS_UserProxy UP ");
        sb2.Append(" where UP.ProxyUser ='" + UserInfo.getUserInfo().UserID + "' ");
        sb2.Append(" and UP.ProxyType = 'Full' ");
        sb2.Append(" and '" + string.Format("{0:yyyyMMdd}", DateTime.Now) + 
            "' BETWEEN UP.ProxyStartDate AND UP.ProxyEndDate; ");
        //過版記得換
        //DataTable dt2 = db2.ExecuteDataSet(sb2.BuildCommand()).Tables[0];
        DataTable dt2 = db.ExecuteDataSet(sb2.BuildCommand()).Tables[0];

        string strUserID = "";
        foreach (DataRow row in dt2.Rows)
        {
            strUserID = "'"+row["UserID"].ToString()+"',";
        }
        if (strUserID.Length > 0) strUserID = strUserID.Remove(strUserID.Length - 1, 1);
        

        //加班人資料
        sb.Append("select AOL.AssignTo,isnull(OT.OTRegisterComp,OD.OTRegisterComp) as OTRegisterComp,isnull(OT.OTCompID,OD.OTCompID)as OTCompID,isnull(OT.OTEmpID,OD.OTEmpID)as OTEmpID,");
sb.Append(" isnull(PT.NameN,PD.NameN) as OTNameN,");
        //事先資料OverTimeAdvance 或 OverTimeAdvance+OverTimeDeclaration
sb.Append(" OT.FlowCaseID,");
sb.Append(" OT.OTSeq as OTSeq, ");
sb.Append(" OTT.CodeCName  as OTTypeID,");
sb.Append(" OT.OTReasonMemo  as OTReasonMemo,");
sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate))  as OTDate,");
sb.Append(" (LEFT(OT.OTStartTime,2)+':'+ RIGHT(OT.OTStartTime,2)+'~'+LEFT(isnull(OT2.OTEndTime,OT.OTEndTime),2)+':'+ RIGHT(isnull(OT2.OTEndTime,OT.OTEndTime),2))   as OTTime,");
//事後資料OverTimeDeclaration
sb.Append(" OD.OTSeq as AfterOTSeq, ");
sb.Append(" OD.FlowCaseID as AfterFlowCaseID, ");
sb.Append(" OTD.CodeCName as AfterOTTypeID,");
sb.Append(" OD.OTReasonMemo as AfterOTReasonMemo,");
sb.Append(" (OD.OTStartDate+'~'+isnull(OD2.OTEndDate,OD.OTEndDate)) as AfterOTDate,");
sb.Append(" (LEFT(OD.OTStartTime,2)+':'+ RIGHT(OD.OTStartTime,2)+'~'+LEFT(isnull(OD2.OTEndTime,OD.OTEndTime),2)+':'+ RIGHT(isnull(OD2.OTEndTime,OD.OTEndTime),2)) as AfterOTTime,");
sb.Append(" OD.FlowCaseID as  AfterFlowCaseID ,");
sb.Append(" Case when  OD.OTStartDate is not null then 'D' else 'A' end as  SortNo ");
//簽核人員
sb.Append(" from  " + FlowCustDB + "FlowOpenLog AOL");
//sb.Append(" left join " + _DBShare + ".dbo.PS_UserProxy UP on UP.UserID=AOL.AssignTo"); //20170213 再修改，加入代理人判斷 //20170310 獨立出另一個查詢
//OverTimeDeclaration
sb.Append(" LEFT JOIN OverTimeDeclaration OD ON AOL.FlowCaseID=OD.FlowCaseID ");
sb.Append(" LEFT JOIN OverTimeDeclaration OD2 on OD2.OTTxnID=OD.OTTxnID and OD2.OTSeqNo=2 ");
sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PD ON PD.EmpID=OD.OTEmpID AND PD.CompID = OD.OTCompID ");
sb.Append(" LEFT JOIN AT_CodeMap AS OTD ON OTD.TabName='OverTime' and OTD.FldName='OverTimeType' and OTD.Code=OD.OTTypeID");

//OverTimeAdvance
sb.Append(" LEFT JOIN OverTimeAdvance OT ON AOL.FlowCaseID=OT.FlowCaseID or (OT.OTTxnID=OD.OTFromAdvanceTxnId and OT.OTSeqNo=1) ");
sb.Append(" left join OverTimeAdvance OT2 on OT2.OTTxnID=OT.OTTxnID and OT2.OTSeqNo=2 ");
sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PT ON PT.EmpID=OT.OTEmpID AND PT.CompID = OT.OTCompID ");
sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OTT.TabName='OverTime' and OTT.FldName='OverTimeType' and OTT.Code=OT.OTTypeID");

sb.Append(" where (");
if (strUserID != "")
{
    sb.Append(" (AOL.AssignTo in(" + strUserID + ")and isnull(OT.OTEmpID,OD.OTEmpID)<>'" + UserInfo.getUserInfo().UserID + "') or ");//20170213 再修改，加入代理人判斷
}
sb.Append(" AOL.AssignTo = '" + UserInfo.getUserInfo().UserID + "')");  //本人單
sb.Append(" AND ( ((OT.OTSeqNo=1 and OT.OTStatus='2')OR (OT.OTSeqNo=1 and OT.OTSeqNo=OD.OTSeqNo and OT.OTTxnID=OD.OTFromAdvanceTxnId))OR (OD.OTSeqNo=1 and OD.OTStatus='2') ) ");
sb.Append(" ORDER BY SortNo ,OTDate,OTTime,AfterOTDate,AfterOTTime ASC");
        /*=====================================================*/
       
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

        gvMain.DataSource = dt;
        gvMain.DataBind();

    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView oRow;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            oRow = (DataRowView)e.Row.DataItem;
            e.Row.Cells[5].ToolTip = oRow["OTReasonMemo"] + "";
            e.Row.Cells[5].Text = (oRow["OTReasonMemo"] + "").Substring(0, (oRow["OTReasonMemo"] + "").Length <= 10 ? (oRow["OTReasonMemo"] + "").Length :10);
            
            e.Row.Cells[11].ToolTip = oRow["AfterOTReasonMemo"] + "";
            e.Row.Cells[11].Text = (oRow["AfterOTReasonMemo"] + "").Substring(0, (oRow["AfterOTReasonMemo"] + "").Length <= 10 ? (oRow["AfterOTReasonMemo"] + "").Length : 10);

            if (e.Row.Cells[4].Text != "&nbsp;" && e.Row.Cells[10].Text != "&nbsp;")
            {
                CheckBox chk_chkOverTimeD = (CheckBox)e.Row.FindControl("chkOverTimeD");
                chk_chkOverTimeD.Enabled = true;
                ImageButton lnk_iBtnApproveD = (ImageButton)e.Row.FindControl("iBtnApproveD");
                lnk_iBtnApproveD.Enabled = true;

                //LinkButton lnk_lbtnDetailD = (LinkButton)e.Row.FindControl("lbtnDetailD");
                //lnk_lbtnDetailD.Enabled = true;

                e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            }
            else if (e.Row.Cells[10].Text != "&nbsp;")
            {
                CheckBox chk_chkOverTimeD = (CheckBox)e.Row.FindControl("chkOverTimeD");
                chk_chkOverTimeD.Enabled = true;
                ImageButton lnk_iBtnApproveD = (ImageButton)e.Row.FindControl("iBtnApproveD");
                lnk_iBtnApproveD.Enabled = true;

                //LinkButton lnk_lbtnDetailD = (LinkButton)e.Row.FindControl("lbtnDetailD");
                //lnk_lbtnDetailD.Enabled = true;

                e.Row.Cells[2].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            }
            else if (e.Row.Cells[4].Text != "&nbsp;")
            {
                CheckBox chk_chkOverTimeA = (CheckBox)e.Row.FindControl("chkOverTimeA");
                chk_chkOverTimeA.Enabled = true;
                ImageButton lnk_iBtnApproveA = (ImageButton)e.Row.FindControl("iBtnApproveA");
                lnk_iBtnApproveA.Enabled = true;
                //LinkButton lnk_lbtnDetailA = (LinkButton)e.Row.FindControl("lbtnDetailA");
                //lnk_lbtnDetailA.Enabled = true;

                e.Row.Cells[8].BackColor  = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[9].BackColor  = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[13].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");

            }
        }
    }

    protected void gvMain_RowCreated(object sender, GridViewRowEventArgs e) //合併表頭欄位
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView gv = (GridView)sender;
            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow gvRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell tc1 = new TableCell();
            tc1.Text = "員工編號";
            tc1.HorizontalAlign = HorizontalAlign.Center;
            tc1.RowSpan = 2;
            tc1.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc1);
            TableCell tc2 = new TableCell();
            tc2.Text = "加班人";
            tc2.HorizontalAlign = HorizontalAlign.Center;
            tc2.RowSpan = 2;
            tc2.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc2);

            TableCell tc3 = new TableCell();
            tc3.Text = "加班單預先申請";
            tc3.HorizontalAlign = HorizontalAlign.Center;
            tc3.ColumnSpan = 6;
            tc3.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc3);

            TableCell tc4 = new TableCell();
            tc4.Text = "加班單事後申報";
            tc4.ColumnSpan = 6;
            tc4.CssClass = "Util_gvHeader";
            tc4.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc4);

            CheckBox chkAdvance = new CheckBox();
            chkAdvance.ID = "chkAllAdvance";
            chkAdvance.Attributes.Add("OnClick", "chkAllAdvance_CheckAll(gvMain_ctl02_chkAllAdvance)");
            TableCell HeaderCell = new TableCell();
            HeaderCell.Controls.Add(chkAdvance);
            HeaderCell.CssClass = "Util_gvHeader";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell);

            TableCell HeaderCell_0 = new TableCell();
            HeaderCell_0.Text = "";
            HeaderCell_0.CssClass = "Util_gvHeader";
            HeaderCell_0.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_0);

            TableCell HeaderCell_1 = new TableCell();
            HeaderCell_1.Text = "加班類型";
            HeaderCell_1.CssClass = "Util_gvHeader";
            HeaderCell_1.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_1);

            TableCell HeaderCell_2 = new TableCell();
            HeaderCell_2.Text = "加班原因";
            HeaderCell_2.CssClass = "Util_gvHeader";
            HeaderCell_2.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_2);

            TableCell HeaderCell_3 = new TableCell();
            HeaderCell_3.Text = "加班日期";
            HeaderCell_3.CssClass = "Util_gvHeader";
            HeaderCell_3.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_3);

            TableCell HeaderCell_4 = new TableCell();
            HeaderCell_4.Text = "加班起迄時間";
            HeaderCell_4.CssClass = "Util_gvHeader";
            HeaderCell_4.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_4);

            CheckBox chkDeclaration = new CheckBox();
            chkDeclaration.ID = "chkAllDeclaration";
            chkDeclaration.Attributes.Add("OnClick", "chkDeclaration_CheckAll(gvMain_ctl02_chkAllDeclaration)");
            TableCell HeaderCell_5 = new TableCell();
            HeaderCell_5.Controls.Add(chkDeclaration);
            HeaderCell_5.CssClass = "Util_gvHeader";
            HeaderCell_5.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_5);

            TableCell HeaderCell_6 = new TableCell();
            HeaderCell_6.Text = "";
            HeaderCell_6.CssClass = "Util_gvHeader";
            HeaderCell_6.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_6);

            TableCell HeaderCell_7 = new TableCell();
            HeaderCell_7.Text = "加班類型";
            HeaderCell_7.CssClass = "Util_gvHeader";
            HeaderCell_7.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_7);

            TableCell HeaderCell_8 = new TableCell();
            HeaderCell_8.Text = "加班原因";
            HeaderCell_8.CssClass = "Util_gvHeader";
            HeaderCell_8.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_8);

            TableCell HeaderCell_9 = new TableCell();
            HeaderCell_9.Text = "加班日期";
            HeaderCell_9.CssClass = "Util_gvHeader";
            HeaderCell_9.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_9);

            TableCell HeaderCell_10 = new TableCell();
            HeaderCell_10.Text = "加班起迄時間";
            HeaderCell_10.CssClass = "Util_gvHeader";
            HeaderCell_10.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_10);

            e.Row.Cells.Clear();
            gv.Controls[0].Controls.AddAt(0, gvRow);
            gv.Controls[0].Controls.AddAt(1, gvRow1);
        }

    }

    protected void btnCheck_Click(object sender, EventArgs e)//簽核需呼叫流程引擎
    {
        //FlowExpress oFlow = new FlowExpress(_DBName, _CurrFlowLogID, false);
        if (!LoadGridViewData())return;
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        string SignID_CompID = "";
        string strFlowCaseID = "";
        if (ViewState["dtOverTimeAdvance"] == null && ViewState["dtOverTimeDeclaration"] == null)
        {
            Util.MsgBox("尚未有資料可以審核");
            return;
        }
        else
        {
            DataTable dtOTA = (DataTable)ViewState["dtOverTimeAdvance"];
            DataTable dtOTD = (DataTable)ViewState["dtOverTimeDeclaration"];
            if (dtOTA.Rows.Count <= 0 && dtOTD.Rows.Count <= 0)
            {
                Util.MsgBox("尚未勾選資料");
                return;
            }
            else
            {
                if (dtOTA.Rows.Count > 0 && dtOTD.Rows.Count > 0)
                {
                    SignID_CompID = dtOTA.Rows[0]["SignID"].ToString().Trim() + "," + dtOTA.Rows[0]["SignIDComp"].ToString().Trim();
                    strFlowCaseID = dtOTA.Rows[0]["FlowCaseID"].ToString().Trim();
                }
                else if (dtOTA.Rows.Count > 0 && dtOTD.Rows.Count <= 0)
                {
                    SignID_CompID = dtOTA.Rows[0]["SignID"].ToString().Trim() + "," + dtOTA.Rows[0]["SignIDComp"].ToString().Trim();
                    strFlowCaseID = dtOTA.Rows[0]["FlowCaseID"].ToString().Trim();
                }
                else
                {
                    SignID_CompID = dtOTD.Rows[0]["SignID"].ToString().Trim() + "," + dtOTD.Rows[0]["SignIDComp"].ToString().Trim();
                    strFlowCaseID = dtOTD.Rows[0]["FlowCaseID"].ToString().Trim();
                }
            }
        }
        //只有要傳第一筆過去，不要作迴圈了
        //CustVerify.setFlowSignID_CompID是多筆，由上面組ViewState["dtOverTimeAdvance"]來做
        ClearBtn(strFlowCaseID);
        CustVerify.setFlowSignID_CompID(SignID_CompID);
        Session["btnVisible"] = "0";
        //單筆產生按鈕
        sb.Reset();
        sb.Append("SELECT top 1 FlowLogID FROM " + FlowCustDB + "FlowFullLog ");
        sb.Append(" WHERE FlowCaseID='" + strFlowCaseID + "'");
        string strFlowLogID = db.ExecuteScalar(sb.BuildCommand()).ToString();
        FlowExpress oFlowReload = new FlowExpress(_DBName, strFlowLogID, true);
        FlowExpress.setFlowOpenLogVerifyInfo(oFlowReload, true);

        Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}", _DBName, strFlowLogID, "Self", "N", "N", ""));


    }

    protected bool LoadGridViewData()
    {
        DataTable dtOverTimeAdvance = new DataTable();
        dtOverTimeAdvance.Columns.Add("OTRegisterComp");
        dtOverTimeAdvance.Columns.Add("AssignTo");
        dtOverTimeAdvance.Columns.Add("CompID");
        dtOverTimeAdvance.Columns.Add("EmpID");
        dtOverTimeAdvance.Columns.Add("OTStartDate");
        dtOverTimeAdvance.Columns.Add("OTEndDate");
        dtOverTimeAdvance.Columns.Add("OTStartTime");
        dtOverTimeAdvance.Columns.Add("OTEndTime");
        dtOverTimeAdvance.Columns.Add("OTSeq");
        dtOverTimeAdvance.Columns.Add("OTSeqNo");
        dtOverTimeAdvance.Columns.Add("FlowCaseID");
        //btnName
        dtOverTimeAdvance.Columns.Add("btnName");
        //toUserData
        dtOverTimeAdvance.Columns.Add("SignLine");
        dtOverTimeAdvance.Columns.Add("SignIDComp");
        dtOverTimeAdvance.Columns.Add("SignID");
        dtOverTimeAdvance.Columns.Add("SignOrganID");
        dtOverTimeAdvance.Columns.Add("SignFlowOrganID");

        DataTable dtOverTimeDeclaration = new DataTable();
        dtOverTimeDeclaration.Columns.Add("OTRegisterComp");
        dtOverTimeDeclaration.Columns.Add("AssignTo");
        dtOverTimeDeclaration.Columns.Add("CompID");
        dtOverTimeDeclaration.Columns.Add("EmpID");
        dtOverTimeDeclaration.Columns.Add("OTStartDate");
        dtOverTimeDeclaration.Columns.Add("OTEndDate");
        dtOverTimeDeclaration.Columns.Add("OTStartTime");
        dtOverTimeDeclaration.Columns.Add("OTEndTime");
        dtOverTimeDeclaration.Columns.Add("OTSeq");
        dtOverTimeDeclaration.Columns.Add("OTSeqNo");
        dtOverTimeDeclaration.Columns.Add("FlowCaseID");
        //btnName
        dtOverTimeDeclaration.Columns.Add("btnName");
        //toUserData
        dtOverTimeDeclaration.Columns.Add("SignLine");
        dtOverTimeDeclaration.Columns.Add("SignIDComp");
        dtOverTimeDeclaration.Columns.Add("SignID");
        dtOverTimeDeclaration.Columns.Add("SignOrganID");
        dtOverTimeDeclaration.Columns.Add("SignFlowOrganID");

        int Num = 0;
        string strNum = "";
        foreach (GridViewRow row in gvMain.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Num++;
                CheckBox chk_chkOverTimeA = (CheckBox)row.Cells[2].FindControl("chkOverTimeA");
                if (chk_chkOverTimeA.Checked)
                {
                    if (gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0] == gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1])
                    {
                        DataRow drOverTimeAdvance = dtOverTimeAdvance.NewRow();
                        drOverTimeAdvance["OTRegisterComp"] = gvMain.DataKeys[row.RowIndex].Values["OTRegisterComp"].ToString();
                        drOverTimeAdvance["AssignTo"] = gvMain.DataKeys[row.RowIndex].Values["AssignTo"].ToString();
                        drOverTimeAdvance["CompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                        drOverTimeAdvance["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeAdvance["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0];
                        drOverTimeAdvance["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                        drOverTimeAdvance["OTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeAdvance["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeAdvance["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["OTSeq"].ToString();
                        drOverTimeAdvance["OTSeqNo"] = "1";
                        drOverTimeAdvance["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["FlowCaseID"].ToString();
                        //DataRow併入btnName與toUserData的資料
                        if (nextOverTime("A", ref drOverTimeAdvance))
                        {
                            dtOverTimeAdvance.Rows.Add(drOverTimeAdvance);
                        }
                        else
                        { 
                            //紀錄gv上第幾個單無下一關主管
                            strNum = strNum + Num.ToString() + ","; 
                        }
                    }
                    else
                    {
                        DataRow drOverTimeAdvance = dtOverTimeAdvance.NewRow();
                        drOverTimeAdvance["OTRegisterComp"] = gvMain.DataKeys[row.RowIndex].Values["OTRegisterComp"].ToString();
                        drOverTimeAdvance["AssignTo"] = gvMain.DataKeys[row.RowIndex].Values["AssignTo"].ToString();
                        drOverTimeAdvance["CompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                        drOverTimeAdvance["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeAdvance["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0];
                        drOverTimeAdvance["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0];
                        drOverTimeAdvance["OTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeAdvance["OTEndTime"] = "2359";
                        drOverTimeAdvance["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["OTSeq"].ToString();
                        drOverTimeAdvance["OTSeqNo"] = "1";
                        drOverTimeAdvance["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["FlowCaseID"].ToString();
                       
                        //dtOverTimeAdvance.Rows.Add(drOverTimeAdvance);
                        DataRow drOverTimeAdvance2 = dtOverTimeAdvance.NewRow();
                        drOverTimeAdvance2["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeAdvance2["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                        drOverTimeAdvance2["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                        drOverTimeAdvance2["OTStartTime"] = "0000";
                        drOverTimeAdvance2["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeAdvance["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["OTSeq"].ToString();
                        drOverTimeAdvance2["OTSeqNo"] = "2";
                        drOverTimeAdvance2["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["FlowCaseID"].ToString();
                        if (nextOverTime("A", ref drOverTimeAdvance))
                        {
                            dtOverTimeAdvance.Rows.Add(drOverTimeAdvance);
                            dtOverTimeAdvance.Rows.Add(drOverTimeAdvance2);
                        }
                        else
                        {
                            strNum = strNum + Num.ToString() + ",";
                        }
                    }
                }
                ViewState["dtOverTimeAdvance"] = dtOverTimeAdvance;
                Session["dtOverTimeAdvance"] = dtOverTimeAdvance;
                
                CheckBox chk_chkOverTimeD = (CheckBox)row.Cells[9].FindControl("chkOverTimeD");
                if (chk_chkOverTimeD.Checked)
                {
                    if (gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0] == gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1])
                    {
                        DataRow drOverTimeDeclaration = dtOverTimeDeclaration.NewRow();
                        drOverTimeDeclaration["OTRegisterComp"] = gvMain.DataKeys[row.RowIndex].Values["OTRegisterComp"].ToString();
                        drOverTimeDeclaration["AssignTo"] = gvMain.DataKeys[row.RowIndex].Values["AssignTo"].ToString();
                        drOverTimeDeclaration["CompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                        drOverTimeDeclaration["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeDeclaration["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration["OTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeDeclaration["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeDeclaration["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration["OTSeqNo"] = "1";
                        drOverTimeDeclaration["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        if (nextOverTime("D", ref drOverTimeDeclaration))
                        {
                            dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                        }
                        else
                        {
                            strNum = strNum + Num.ToString() + ",";
                        }
                        //nextOverTime("D", ref drOverTimeDeclaration);
                        //dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                    }
                    else
                    {
                        DataRow drOverTimeDeclaration = dtOverTimeDeclaration.NewRow();
                        drOverTimeDeclaration["OTRegisterComp"] = gvMain.DataKeys[row.RowIndex].Values["OTRegisterComp"].ToString();
                        drOverTimeDeclaration["AssignTo"] = gvMain.DataKeys[row.RowIndex].Values["AssignTo"].ToString();
                        drOverTimeDeclaration["CompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                        drOverTimeDeclaration["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeDeclaration["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["OTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeDeclaration["OTEndTime"] = "2359";
                        drOverTimeDeclaration["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration["OTSeqNo"] = "1";
                        drOverTimeDeclaration["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        //nextOverTime("D", ref drOverTimeDeclaration);
                        //dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                        DataRow drOverTimeDeclaration2 = dtOverTimeDeclaration.NewRow();
                        drOverTimeDeclaration2["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeDeclaration2["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration2["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration2["OTStartTime"] = "0000";
                        drOverTimeDeclaration2["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeDeclaration["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration2["OTSeqNo"] = "2";
                        drOverTimeDeclaration2["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        if (nextOverTime("D", ref drOverTimeDeclaration))
                        {
                            dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                            dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration2);
                        }
                        else
                        {
                            strNum = strNum + Num.ToString() + ",";
                        }
                    }
                }
                ViewState["dtOverTimeDeclaration"] = dtOverTimeDeclaration;
                Session["dtOverTimeDeclaration"] = dtOverTimeDeclaration;
            }
        }
        if(strNum!="")
        {
            Util.MsgBox("第" + strNum.Remove(strNum.Length-1) + "項 查無下一關主管資料");
            return false;
        }
        return true;
    }

    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //FlowExpress oFlow = new FlowExpress(_DBName);
        GridViewRow clickedRow = ((ImageButton)e.CommandSource).NamingContainer as GridViewRow;
        //GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        string strEmpID=gvMain.Rows[clickedRow.RowIndex].Cells[0].Text;
        Dictionary<string, string> toUserData;
        String gvFlowCaseID="",btnName="";
        switch (e.CommandName)
        {
            case "DetailA":
                gvFlowCaseID=gvMain.DataKeys[clickedRow.RowIndex].Values[8].ToString();
                sb.Reset();
                sb.Append("SELECT OL.FlowLogID,OL.AssignTo,OT.OTCompID,OT.OTStartDate FROM " + FlowCustDB + "FlowOpenLog OL");
                sb.Append(" LEFT JOIN OverTimeAdvance OT ON OL.FlowCaseID=OT.FlowCaseID");
                sb.Append(" WHERE OT.FlowCaseID='" + gvFlowCaseID + "'");
                sb.Append(" AND OT.OTSeqNo='1'");

                DataTable dtFlowLogIDA = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                //取得下一關主管相關訊息
                if (!nextAssignTo(
                    dtFlowLogIDA.Rows[0]["OTCompID"].ToString(),
                    dtFlowLogIDA.Rows[0]["AssignTo"].ToString(),
                    dtFlowLogIDA.Rows[0]["OTStartDate"].ToString(),
                    gvFlowCaseID,
                    "A",
                    out toUserData))
                {
                    Util.MsgBox("查無下一關主管資料");
                    return;
                }
                //將下一關主管資料丟給共用檔，待之後永豐流程去撈取
                CustVerify.setFlowSignID_CompID(toUserData["SignID"].ToString() + "," + toUserData["SignIDComp"].ToString());
                //傳給審核畫面-單筆審核使用
                Session["toUserData"] = toUserData;
                //按鈕清單
                ClearBtn(gvFlowCaseID);
                //下一關邏輯判斷，產生審核按鈕資訊
                nextFlowBtn(dtFlowLogIDA.Rows[0]["AssignTo"].ToString(), dtFlowLogIDA.Rows[0]["OTCompID"].ToString(), gvFlowCaseID, "A",ref  btnName);
                //產生審核按鈕
                FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowLogIDA.Rows[0]["AssignTo"].ToString(), _DBName.Split(','), "".Split(','), false, "", "");
                //跳轉畫面
                Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}"
                        , _DBName, dtFlowLogIDA.Rows[0]["FlowLogID"], "Self", "N", "N", ""));

                break;
            case "DetailD":
                gvFlowCaseID = gvMain.DataKeys[clickedRow.RowIndex].Values[9].ToString();
                sb.Reset();
                sb.Append("SELECT OL.FlowLogID,OL.AssignTo,OT.OTCompID,OT.OTStartDate  FROM " + FlowCustDB + "FlowOpenLog OL");
                sb.Append(" LEFT JOIN OverTimeDeclaration OT ON OL.FlowCaseID=OT.FlowCaseID");
                sb.Append(" WHERE OT.FlowCaseID='" + gvFlowCaseID + "'");
                sb.Append(" AND OT.OTSeqNo='1'");
                DataTable dtFlowLogIDD = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                if(!nextAssignTo(
                    dtFlowLogIDD.Rows[0]["OTCompID"].ToString(),
                    dtFlowLogIDD.Rows[0]["AssignTo"].ToString(),
                    dtFlowLogIDD.Rows[0]["OTStartDate"].ToString(),
                    gvFlowCaseID, 
                    "D",
                    out toUserData))
                {
                    Util.MsgBox("查無下一關主管資料");
                    return;
                }
                CustVerify.setFlowSignID_CompID(toUserData["SignID"].ToString() + "," + toUserData["SignIDComp"].ToString());
                Session["toUserData"] = toUserData;

                //按鈕清單
                ClearBtn(gvFlowCaseID);
                nextFlowBtn(dtFlowLogIDD.Rows[0]["AssignTo"].ToString(), dtFlowLogIDD.Rows[0]["OTCompID"].ToString(), gvFlowCaseID, "D", ref  btnName);
                FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowLogIDD.Rows[0]["AssignTo"].ToString(), _DBName.Split(','), null, false, "", "");

                Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}"
                                            , _DBName, dtFlowLogIDD.Rows[0]["FlowLogID"].ToString(), "Self", "N", "N", ""));
                break;

        }
    }

    /// <summary>
    /// 多筆審核，組DataTable給Session送到審核畫面的資料，
    /// 在尾端追加btnName審核按鈕、toUserData下一關主管訊息。
    /// 組合時，順便將按鈕產生出來。
    /// </summary>
    private Boolean  nextOverTime(string AD,ref DataRow OverTimeTable)
    {
        //input
        string OTStartDate, FlowCaseID, btnName="";

         Dictionary<string, string> toUserData;
        OTStartDate = OverTimeTable["OTStartDate"].ToString();
        FlowCaseID = OverTimeTable["FlowCaseID"].ToString();

        //清除按鈕內容
        if (!nextAssignTo(OverTimeTable["CompID"].ToString(), OverTimeTable["AssignTo"].ToString(), OTStartDate, FlowCaseID, AD, out toUserData))return false;

        nextFlowBtn(OverTimeTable["AssignTo"].ToString(), OverTimeTable["CompID"].ToString(), OverTimeTable["FlowCaseID"].ToString(),AD, ref btnName);
        
        //btnName
        OverTimeTable["btnName"] = btnName;
        //toUserData
        OverTimeTable["SignLine"] = toUserData["SignLine"].ToString();
        OverTimeTable["SignIDComp"] = toUserData["SignIDComp"].ToString();
        OverTimeTable["SignID"] = toUserData["SignID"].ToString();
        OverTimeTable["SignOrganID"] = toUserData["SignOrganID"].ToString();
        OverTimeTable["SignFlowOrganID"] = toUserData["SignFlowOrganID"].ToString();
        return true;
    }

    /// <summary>
    /// 檢核所有東西，並將下一關相關資訊回傳
    /// </summary>
    private bool nextAssignTo(string CompID, string AssignTo, string OTStartDate, string FlowCaseID, string AD, out Dictionary<string, string> toUserData)
    {
        //QueryFlowDataAndToUserData用input
        DataTable dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(FlowCaseID, AD);
        String flowCodeFlag = AD == "A" ? "0" : "1";

        bool isLastFlow, nextIsLastFlow;
        string flowCode = "", flowSN = "", signLineDefine = "", meassge = "";

        //EmpInfo.QueryOrganData || EmpInfo.QueryFlowOrganData 使用
        string SignOrganID = "", SignID = "", SignIDComp = "";
                  
        //讀取現在關卡與下一關相關資料，因為不論回傳是否，我還是要資料，所以沒檢核回傳值與錯誤訊息
        FlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, OTStartDate, FlowCaseID, flowCodeFlag,
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

        //若是後台HR送簽依照填單人公司，否則用加班人公司
        string HRLogCompID = signLineDefine == "4" || flowCode.Trim() == "" ?
                            dtOverTime.Rows[0]["OTRegisterComp"].ToString() :
                            dtOverTime.Rows[0]["OTCompID"].ToString();

        //如果沒有下一關資料，則用現在關卡資料取代
        if (toUserData.Count == 0)
        {
            //取[最近的行政or功能]資料 取代 [現在關卡]資料
            DataTable dtHROverTimeLog_toUD = CustVerify.HROverTimeLog(FlowCaseID, true);
            toUserData.Add("SignLine", dtHROverTimeLog_toUD.Rows[0]["SignLine"].ToString());
            toUserData.Add("SignIDComp", dtHROverTimeLog_toUD.Rows[0]["SignIDComp"].ToString());
            toUserData.Add("SignID", AssignTo);
            toUserData.Add("SignOrganID", dtHROverTimeLog_toUD.Rows[0]["SignOrganID"].ToString());
            toUserData.Add("SignFlowOrganID", dtHROverTimeLog_toUD.Rows[0]["SignFlowOrganID"].ToString());
        }

        //如果下一關主管與現在主管相同，則再往上階找下一關主管資料
        if (toUserData["SignID"] == AssignTo && signLineDefine!="3")
        {
            switch (toUserData["SignLine"])
            {
                //HR線 或 行政線
                case "4":
                case "1":
                    if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], dtOverTime.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = SignOrganID;
                        toUserData["SignFlowOrganID"] = "";
                    }
                    break;
                    //功能線
                case "2":
                    if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], dtOverTime.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = "";
                        toUserData["SignFlowOrganID"] = SignOrganID;
                    }
                    break;

                //原本switch的是signLineDefine，現在改成toUserData["SignLine"]後，
                //case "3"裏頭的if基本只會用到else[非功能線一律走行政線]，以防萬一先保留。
                    //改派
                case "3":
                    if (toUserData["SignLine"] == "2")
                    {
                        if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], dtOverTime.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = "";
                            toUserData["SignFlowOrganID"] = SignOrganID;
                        }
                    }
                    else
                    {
                        if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], dtOverTime.Rows[0]["OTStartDate"].ToString(), out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = SignOrganID;
                            toUserData["SignFlowOrganID"] = "";
                        }
                    }
                    break;
            }
        }

        //如果找不到下一關主管資料，彈跳視窗並且return false
        if (toUserData["SignID"] == "")
        {
            toUserData["SignIDComp"] = UserInfo.getUserInfo().CompID.Trim();
            toUserData["SignID"] = UserInfo.getUserInfo().UserID.Trim();
            //Util.MsgBox("查無下一關主管資料");
            if (isLastFlow) //最後一關不用找下一關主管
                return true;
            else
                return false;
        }
        return true;
    }
    private String RankIDMapping(string CompID, string RankID)
    {
        if (CompID == "") return "";
        if (RankID == "") return "";
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append("SELECT Top 1 [RankIDMap]");
        sb.Append("FROM " + _eHRMSDB + ".[dbo].[RankMapping]");
        sb.Append("where CompID='" + CompID + "' and RankID='" + RankID + "' ;");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["RankIDMap"].ToString() : "";
        //return db.ExecuteScalar(CommandType.Text, sb.ToString()).ToString();
    }
    private string RankPara(DataTable dt, string CompID, string RankType)
    {
        string RankID = "0";
        Aattendant a = new Aattendant();
        if (dt.Select("CompID='" + CompID + "'").Count() > 0)
        {
            RankID = a.Json2DataTable(dt.Select("CompID='" + CompID + "'").CopyToDataTable().Rows[0]["Para"].ToString()).Rows[0][RankType].ToString();
            return RankID;
        }
        return RankID;
    }

    private bool boolUpEmpRankID(string AD, string FlowCaseID, string UpEmpRankID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        string MapEmpRankID, OTEmpID;
        sb.Append(" SELECT Top 1 isnull(P.CompID,'') as CompID,isnull(P.RankID,'')as RankID FROM " + CustVerify.ADTable(AD) + " OT ");
        sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Personal] P ON OT.OTEmpID=P.EmpID and OT.OTCompID=P.CompID");
        sb.Append(" WHERE OT.FlowCaseID='" + FlowCaseID + "' and OTSeqNo='1'");
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count == 0) return false;
        MapEmpRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UpEmpRankID);
        if (MapEmpRankID == "") return false;
        OTEmpID = RankIDMapping(dt.Rows[0]["CompID"].ToString(), dt.Rows[0]["RankID"].ToString());
        if (OTEmpID == "") return false;
        return int.Parse(MapEmpRankID) <= int.Parse(OTEmpID) ? true : false;
    }

    /// <summary>
    /// 找ValidRankID、EmpRankID的訊息
    /// 當找不到資料時，IsUpValidRankID預設True、IsUpEmpRankID預設False
    /// </summary>
    /// <param name="CompID"></param>
    /// <param name="FlowCaseID"></param>
    /// <param name="IsUpValidRankID">審核人是否大於</param>
    /// <param name="IsUpEmpRankID">加班人是否大於</param>
    private void RankIDCheck(string CompID, string FlowCaseID, out bool IsUpValidRankID, out bool IsUpEmpRankID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string ValidRankID = "",
                          EmpRankID = "";
        IsUpValidRankID = true;
        IsUpEmpRankID = false;

        string UserRankID = RankIDMapping(UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().RankID);

        //Para撈取參數設定
        DataTable Paradt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT DISTINCT CompID,Para FROM OverTimePara ")).Tables[0];

        //16 登入者公司
        ValidRankID = RankPara(Paradt, UserInfo.getUserInfo().CompID, "ValidRankID");
        //RankMapping
        ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, ValidRankID);
        //若無資料
        if (ValidRankID == "")
        {
            IsUpValidRankID = true;
        }
        else
        {
            //若登入者無資料，預設大於等於ValidRankID
            if (UserRankID == "")
            {
                IsUpValidRankID = true;
            }
            else
            {
                //是否大於
                IsUpValidRankID = Convert.ToInt32(UserRankID) >= int.Parse(ValidRankID) ? true : false;
            }
        }

        //19 加班人公司
        EmpRankID = RankPara(Paradt, CompID, "EmpRankID");
        if (EmpRankID == "")
        {
            IsUpEmpRankID = false;
        }
        else
        { 
            //因為東西偏多，獨立為一個程式
            IsUpEmpRankID = boolUpEmpRankID("D", FlowCaseID, EmpRankID); 
        }

    }
    private void nextFlowBtn(string AssignTo, string CompID, string FlowCaseID, string AD, ref string btnName)
    {
        
        bool IsUpValidRankID = true;
        bool IsUpEmpRankID = false;
        String FlowStepID = "";
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        sb.Append("SELECT FlowStepID from " + FlowCustDB + "FlowOpenLog ");
        sb.Append(" where FlowCaseID=").AppendParameter("FlowCaseID", FlowCaseID);
        FlowStepID = db.ExecuteScalar(sb.BuildCommand()).ToString();

        //撈取該筆加班單相關資料
        DataTable dtOverTime = CustVerify.OverTime_find_by_FlowCaseID(FlowCaseID, AD);

        Dictionary<string, string> toUserData;
        bool isLastFlow, nextIsLastFlow;
        string flowCode = "", flowSN = "", signLineDefine = "", meassge = "",
                  flowCodeFlag = AD == "A" ? "0" : "1";

        //讀取現在關卡與下一關相關資料，因為不論回傳是否，我還是要資料，所以沒檢核回傳值與錯誤訊息
        FlowUtility.QueryFlowDataAndToUserData(dtOverTime.Rows[0]["OTCompID"].ToString(), AssignTo, dtOverTime.Rows[0]["OTStartDate"].ToString(), FlowCaseID, flowCodeFlag,
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);


        //若是後台HR送簽依照填單人公司，否則用加班人公司
        string HRLogCompID = signLineDefine == "4" || flowCode.Trim() == "" ?
                            dtOverTime.Rows[0]["OTRegisterComp"].ToString() :
                            dtOverTime.Rows[0]["OTCompID"].ToString();

        //單筆審核應急用，寫死判斷(因為單筆是用永豐的Function傳遞值，無OverTimeA/DTable的資料)
        if (flowCodeFlag == "0") //猶豫要用哪個判斷式
        {
            isLastFlow = isLastFlowNow(dtOverTime.Rows[0]["OTCompID"].ToString(), FlowCaseID, "A");
        }
        else
        {
            isLastFlow = isLastFlowNow(dtOverTime.Rows[0]["OTCompID"].ToString(), FlowCaseID, "D");
        }

        RankIDCheck(CompID, FlowCaseID, out IsUpValidRankID, out IsUpEmpRankID);
        //HR特別關
        if (signLineDefine == "4")
        {
            //大於Rank16
            if (IsUpValidRankID)
            {
                //給永豐流程按鈕隱藏判斷
                Session["btnVisible"] = "2";
                //回傳按鈕名稱，給多筆審核組進DataTable傳給審核畫面
                btnName = "btnClose";
            }
            else
            {
                if (FlowStepID == "A30") //預防HR送錯關，本來資料上HR關是只有A40的
                {
                    Session["btnVisible"] = "0";
                    btnName = "btnApprove";
                }
                else
                {
                    Session["btnVisible"] = "1";
                    btnName = "btnReApprove";
                }
            }
        }
        //非最後一關
        else if (!isLastFlowNow(CompID, FlowCaseID, AD))
        {
            //大於Rank19
            if (IsUpEmpRankID)
            {
                Session["btnVisible"] = "2";
                btnName = "btnClose";
            }
            else
            {
                //下一關是否進入A40最後一關
                if (nextIsLastFlow)
                {
                    Session["btnVisible"] = "0";
                    btnName = "btnApprove";
                }
                else
                {
                    Session["btnVisible"] = "1";
                    btnName = "btnReApprove";
                }
            }
        }
        //最後一關
        else
        {
            //大於Rank16
            if (IsUpValidRankID)
            {
                Session["btnVisible"] = "2";
                btnName = "btnClose";
            }
            else
            {
                Session["btnVisible"] = "1";
                btnName = "btnReApprove";
            }
        }
    }
    private void ClearBtn(string FlowCaseID)
    {
        //FlowExpress tbFlow = new FlowExpress(_CurrFlowID);
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" update " + FlowCustDB + "FlowOpenLog ");
        sb.Append(" set FlowStepBatchEnabled=null ");
        sb.Append(" ,FlowStepOpinion=null ");
        sb.Append(" ,FlowStepBtnInfoCultureCode=null ");
        sb.Append(" ,FlowStepBtnInfoJSON=null ");
        sb.Append(" where FlowCaseID='" + FlowCaseID + "' ");
        db.ExecuteNonQuery(sb.BuildCommand());
    }
    private bool isLastFlowNow(string OTCompID, string flowCaseID, string otModel)
    {
        DataRow retrunRow;
        string message = "";
        try
        {
            if (!FlowUtility.QueryHRFlowEngineDatas_Now(OTCompID, flowCaseID, otModel, out retrunRow, out message))
                return false;
            else if (retrunRow.Table.Rows.Count > 0)
            {
                string FlowEndFlag = retrunRow["FlowEndFlag"].ToString();
                return FlowEndFlag == "1" ? true : false;
            }
            else
                return true;
        }
        catch (Exception ex)
        {
            return true;
        }
    }
}
    