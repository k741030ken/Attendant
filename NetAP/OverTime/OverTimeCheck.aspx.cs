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
    public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
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
    sb.Append(" (AOL.AssignTo in(" + strUserID + ")and isnull(OT.OTEmpID,OD.OTEmpID)<>'" + UserInfo.getUserInfo().UserID + "') or ");
}
sb.Append(" AOL.AssignTo = '" + UserInfo.getUserInfo().UserID + "')"); //20170213 再修改，加入代理人判斷
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
        LoadGridViewData();
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dtFlowCaseID;
        string AD="A";
        if (ViewState["dtOverTimeAdvance"] == null && ViewState["dtOverTimeDeclaration"] == null)
        {
            Util.MsgBox("尚未有資料可以審核");
            return;
        }
        else
        {
            DataTable dt = (DataTable)ViewState["dtOverTimeAdvance"];
            DataTable dt1 = (DataTable)ViewState["dtOverTimeDeclaration"];
            if (dt.Rows.Count <= 0 && dt1.Rows.Count <= 0)
            {
                Util.MsgBox("尚未勾選資料");
                return;
            }
            else
            {
                if (dt.Rows.Count > 0 && dt1.Rows.Count > 0)
                {
                    AD="A";
                    sb.Reset();
                    sb.Append("SELECT Top 1 FlowLogID,OTCompID,FlowCaseID,AssignTo FROM(");
                    sb.Append(" SELECT top 1 FlowLogID,OTCompID,OT.FlowCaseID,AssignTo FROM " + FlowCustDB + "FlowFullLog AL");
                    sb.Append(" LEFT JOIN OverTimeAdvance OT ON OT.FlowCaseID=AL.FlowCaseID");
                    sb.Append(" WHERE OT.OTEmpID='" + dt.Rows[0]["EmpID"] + "'");
                    sb.Append(" AND OT.OTStartDate='" + dt.Rows[0]["OTStartDate"].ToString().Split('~')[0] + "'");
                    sb.Append(" AND OT.OTStartTime='" + dt.Rows[0]["OTStartTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OT.OTEndTime='" + dt.Rows[0]["OTEndTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OT.OTSeq='" + dt.Rows[0]["OTSeq"].ToString() + "'");
                    sb.Append(" order by FlowLogID desc ");
                    sb.Append(" UNION");
                    sb.Append(" SELECT top 1 FlowLogID,OTCompID,OD.FlowCaseID,AssignTo FROM " + FlowCustDB + "FlowFullLog ADL");
                    sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.FlowCaseID=ADL.FlowCaseID");
                    sb.Append(" WHERE OD.OTEmpID='" + dt1.Rows[0]["EmpID"] + "'");
                    sb.Append(" AND OD.OTStartDate='" + dt1.Rows[0]["AfterOTStartDate"].ToString().Split('~')[0] + "'");
                    sb.Append(" AND OD.OTStartTime='" + dt1.Rows[0]["AfterOTStartTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OD.OTEndTime='" + dt1.Rows[0]["AfterOTEndTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OD.OTSeq='" + dt1.Rows[0]["AfterOTSeq"].ToString() + "'");
                    sb.Append(" order by FlowLogID desc ");
                    sb.Append(" ) A order by FlowLogID desc");
                    dtFlowCaseID = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                }
                else if (dt.Rows.Count > 0 && dt1.Rows.Count <= 0)
                {
                    AD="A";
                    sb.Reset();
                    sb.Append("SELECT top 1 FlowLogID,OTCompID,OT.FlowCaseID,AssignTo FROM " + FlowCustDB + "FlowFullLog AL");
                    sb.Append(" LEFT JOIN OverTimeAdvance OT ON OT.FlowCaseID=AL.FlowCaseID");
                    sb.Append(" WHERE OT.OTEmpID='" + dt.Rows[0]["EmpID"] + "'");
                    sb.Append(" AND OT.OTStartDate='" + dt.Rows[0]["OTStartDate"].ToString().Split('~')[0] + "'");
                    sb.Append(" AND OT.OTStartTime='" + dt.Rows[0]["OTStartTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OT.OTEndTime='" + dt.Rows[0]["OTEndTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OT.OTSeq='" + dt.Rows[0]["OTSeq"].ToString() + "'");
                    sb.Append(" order by FlowLogID desc ");
                    dtFlowCaseID = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                }
                else
                {
                    AD="D";
                    sb.Reset();
                    sb.Append("SELECT top 1 FlowLogID,OTCompID,OD.FlowCaseID,AssignTo FROM " + FlowCustDB + "FlowFullLog ADL");
                    sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.FlowCaseID=ADL.FlowCaseID");
                    sb.Append(" WHERE OD.OTEmpID='" + dt1.Rows[0]["EmpID"] + "'");
                    sb.Append(" AND OD.OTStartDate='" + dt1.Rows[0]["AfterOTStartDate"].ToString().Split('~')[0] + "'");
                    sb.Append(" AND OD.OTStartTime='" + dt1.Rows[0]["AfterOTStartTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OD.OTEndTime='" + dt1.Rows[0]["AfterOTEndTime"].ToString().Replace(":", "") + "'");
                    sb.Append(" AND OD.OTSeq='" + dt1.Rows[0]["AfterOTSeq"].ToString() + "'");
                    sb.Append(" order by FlowLogID desc ");
                    dtFlowCaseID = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                }
            }

        }
      
        //sb.Append("SELECT MAX(FlowLogID) AS FlowLogID FROM AattendantDBFlowFullLog WHERE FlowCaseID='" + ViewState["FlowCaseID"].ToString() + "'");
        //DataTable dtFlowCaseID = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        //Type = 1 --> 批次審核  Type = 2 --> 單筆審核
        //ucModalPopup.Reset();
        ////HiddenField1.Value = dtFlowCaseID.Rows[0]["FlowLogID"].ToString();
        //ucModalPopup.ucFrameURL = string.Format("FlowVerify.aspx?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}&Type={6}", "AattendantDB", dtFlowCaseID.Rows[0]["FlowLogID"].ToString(), "Self", "N", "N", "","1");
        //ucModalPopup.ucPopupWidth = 1020;
        //ucModalPopup.ucPopupHeight = 625;
        //ucModalPopup.Show();

        //for (int i = 0; i < dtFlowCaseID.Rows.Count; i++)
        //FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowCaseID.Rows[i]["AssignTo"].ToString(), _DBName.Split(','), null, false, "", "");


        //string strAssignTo = "";
        //for (int i = 0; i < dtFlowCaseID.Rows.Count; i++)
        //{
        //    if (strAssignTo.IndexOf(dtFlowCaseID.Rows[i]["AssignTo"].ToString())==-1)
        //    strAssignTo = strAssignTo + "'"+dtFlowCaseID.Rows[i]["AssignTo"].ToString()+"',";
        //}
        ////strAssignTo.TrimEnd();
        //strClearBtn(oFlow.FlowCustDB, strAssignTo.TrimEnd(','));

        //只有一筆，不要在迴圈了
        ClearBtn(FlowCustDB, dtFlowCaseID.Rows[0]["AssignTo"].ToString(), dtFlowCaseID.Rows[0]["OTCompID"].ToString(), dtFlowCaseID.Rows[0]["FlowCaseID"].ToString(), AD);
        
            FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowCaseID.Rows[0]["AssignTo"].ToString(), _DBName.Split(','), null, false, "", "");

            //FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, UserInfo.getUserInfo(), _DBName.Split(','), null, false, "", "");
        Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}", _DBName, dtFlowCaseID.Rows[0]["FlowLogID"].ToString(), "Self", "N", "N", ""));


    }

    protected void LoadGridViewData()
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
        DataTable dtOverTimeDeclaration = new DataTable();
        dtOverTimeDeclaration.Columns.Add("OTRegisterComp");
        dtOverTimeDeclaration.Columns.Add("AssignTo");
        dtOverTimeDeclaration.Columns.Add("CompID");
        dtOverTimeDeclaration.Columns.Add("EmpID");
        dtOverTimeDeclaration.Columns.Add("AfterOTStartDate");
        dtOverTimeDeclaration.Columns.Add("AfterOTEndDate");
        dtOverTimeDeclaration.Columns.Add("AfterOTStartTime");
        dtOverTimeDeclaration.Columns.Add("AfterOTEndTime");
        dtOverTimeDeclaration.Columns.Add("AfterOTSeq");
        dtOverTimeDeclaration.Columns.Add("AfterOTSeqNo");
        dtOverTimeDeclaration.Columns.Add("AfterFlowCaseID");
        foreach (GridViewRow row in gvMain.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_chkOverTimeA = (CheckBox)row.Cells[2].FindControl("chkOverTimeA");
                if (chk_chkOverTimeA.Checked)
                {
                    string aaa = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0];
                    string bbb=gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
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
                        dtOverTimeAdvance.Rows.Add(drOverTimeAdvance);
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
                        dtOverTimeAdvance.Rows.Add(drOverTimeAdvance);
                        DataRow drOverTimeAdvance2 = dtOverTimeAdvance.NewRow();
                        drOverTimeAdvance2["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeAdvance2["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                        drOverTimeAdvance2["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                        drOverTimeAdvance2["OTStartTime"] = "0000";
                        drOverTimeAdvance2["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeAdvance["OTSeq"] = gvMain.DataKeys[row.RowIndex].Values["OTSeq"].ToString();
                        drOverTimeAdvance2["OTSeqNo"] = "2";
                        drOverTimeAdvance2["FlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["FlowCaseID"].ToString();
                        dtOverTimeAdvance.Rows.Add(drOverTimeAdvance2);
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
                        drOverTimeDeclaration["AfterOTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["AfterOTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration["AfterOTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeDeclaration["AfterOTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeDeclaration["AfterOTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration["AfterOTSeqNo"] = "1";
                        drOverTimeDeclaration["AfterFlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                    }
                    else
                    {
                        DataRow drOverTimeDeclaration = dtOverTimeDeclaration.NewRow();
                        drOverTimeDeclaration["OTRegisterComp"] = gvMain.DataKeys[row.RowIndex].Values["OTRegisterComp"].ToString();
                        drOverTimeDeclaration["AssignTo"] = gvMain.DataKeys[row.RowIndex].Values["AssignTo"].ToString();
                        drOverTimeDeclaration["CompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                        drOverTimeDeclaration["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeDeclaration["AfterOTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["AfterOTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[0];
                        drOverTimeDeclaration["AfterOTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[0]).Replace(":", "");
                        drOverTimeDeclaration["AfterOTEndTime"] = "2359";
                        drOverTimeDeclaration["AfterOTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration["AfterOTSeqNo"] = "1";
                        drOverTimeDeclaration["AfterFlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration);
                        DataRow drOverTimeDeclaration2 = dtOverTimeDeclaration.NewRow();
                        drOverTimeDeclaration2["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                        drOverTimeDeclaration2["AfterOTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration2["AfterOTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTDate"].ToString().Split('~')[1];
                        drOverTimeDeclaration2["AfterOTStartTime"] = "0000";
                        drOverTimeDeclaration2["AfterOTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["AfterOTTime"].ToString().Split('~')[1]).Replace(":", "");
                        drOverTimeDeclaration["AfterOTSeq"] = gvMain.DataKeys[row.RowIndex].Values["AfterOTSeq"].ToString();
                        drOverTimeDeclaration2["AfterOTSeqNo"] = "2";
                        drOverTimeDeclaration2["AfterFlowCaseID"] = gvMain.DataKeys[row.RowIndex].Values["AfterFlowCaseID"].ToString();
                        dtOverTimeDeclaration.Rows.Add(drOverTimeDeclaration2);
                    }
                }
                ViewState["dtOverTimeDeclaration"] = dtOverTimeDeclaration;
                Session["dtOverTimeDeclaration"] = dtOverTimeDeclaration;
            }
        }
    }

    //protected void gvMain_RowDataBound(Object sender, System.Web.UI.WebControls.GridViewRowEventArgs e) 
    //{
    //    DataRowView oRow;
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        oRow = (DataRowView)e.Row.DataItem;
    //        e.Row.Cells[6].ToolTip = oRow["AfterOTReasonMemo"] + "" ;
    //    }
    //}
    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //FlowExpress oFlow = new FlowExpress(_DBName);
        GridViewRow clickedRow = ((ImageButton)e.CommandSource).NamingContainer as GridViewRow;
        //GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        string strEmpID=gvMain.Rows[clickedRow.RowIndex].Cells[0].Text;

        
        switch (e.CommandName)
        {
            case "DetailA":
                sb.Reset();
                sb.Append("SELECT OL.FlowLogID,OL.AssignTo FROM " + FlowCustDB + "FlowOpenLog OL");
                sb.Append(" LEFT JOIN OverTimeAdvance OT ON OL.FlowCaseID=OT.FlowCaseID");
                sb.Append(" WHERE OT.FlowCaseID='" + gvMain.DataKeys[clickedRow.RowIndex].Values[8] + "'");
                sb.Append(" AND OT.OTSeqNo='1'");

                DataTable dtFlowLogIDA = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                //按鈕清單
                ClearBtn(FlowCustDB, dtFlowLogIDA.Rows[0]["AssignTo"].ToString(), gvMain.DataKeys[clickedRow.RowIndex].Values[2].ToString(), gvMain.DataKeys[clickedRow.RowIndex].Values[8].ToString(), "A");
                
                FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowLogIDA.Rows[0]["AssignTo"].ToString(), _DBName.Split(','), "".Split(','), false, "", "");
                Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}"
                        , _DBName, dtFlowLogIDA.Rows[0]["FlowLogID"], "Self", "N", "N", ""));

                break;
            case "DetailD":
                sb.Reset();
                sb.Append("SELECT OL.FlowLogID,OL.AssignTo FROM " + FlowCustDB + "FlowOpenLog OL");
                sb.Append(" LEFT JOIN OverTimeDeclaration OT ON OL.FlowCaseID=OT.FlowCaseID");
                sb.Append(" WHERE OT.FlowCaseID='" + gvMain.DataKeys[clickedRow.RowIndex].Values[9] + "'");
                sb.Append(" AND OT.OTSeqNo='1'");
                DataTable dtFlowLogIDD = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                ucModalPopup.Reset();

                //按鈕清單
                ClearBtn(FlowCustDB, dtFlowLogIDD.Rows[0]["AssignTo"].ToString(), gvMain.DataKeys[clickedRow.RowIndex].Values[2].ToString(), gvMain.DataKeys[clickedRow.RowIndex].Values[9].ToString(), "D");

                FlowExpress.getFlowTodoList(FlowExpress.TodoListAssignKind.All, dtFlowLogIDD.Rows[0]["AssignTo"].ToString(), _DBName.Split(','), null, false, "", "");

                Response.Redirect(string.Format(FlowExpress._FlowPageVerifyURL + "?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}"
                                            , _DBName, dtFlowLogIDD.Rows[0]["FlowLogID"].ToString(), "Self", "N", "N", ""));
                //Response.Redirect(string.Format("FlowPageVerify.aspx?FlowID={0}&FlowLogID={1}&ProxyType={2}&IsShowBtnComplete={3}&IsShowCheckBoxList={4}&ChkMaxKeyLen={5}&Type={6}"
                //                           , "AattendantDB", dtFlowLogIDD.Rows[0]["FlowLogID"], "Self", "N", "N", "", "2"));
                break;

        }
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
    private string ADTable(string AD)
    {
        switch (AD)
        {
            case "A":
            case "1":
                return "OverTimeAdvance";
            case "D":
            case "2":
                return "OverTimeDeclaration";
        }
        return AD;
    }
    private bool boolUpEmpRankID(string AD, string FlowCaseID, string UpEmpRankID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt;
        string MapEmpRankID, OTEmpID;
        sb.Append(" SELECT Top 1 isnull(P.CompID,'') as CompID,isnull(P.RankID,'')as RankID FROM " + ADTable(AD) + " OT ");
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

        //16  //Jason提取所需字串
        ValidRankID = RankPara(Paradt, UserInfo.getUserInfo().CompID, "ValidRankID");
        //RankMapping
        ValidRankID = RankIDMapping(UserInfo.getUserInfo().CompID, ValidRankID);
        //若無資料
        if (ValidRankID == "") ValidRankID = "-1";
        //若登入者無資料，預設大於等於ValidRankID
        if (UserRankID == "") UserRankID = ValidRankID;
        //是否大於
        IsUpValidRankID = Convert.ToInt32(UserRankID) >= int.Parse(ValidRankID) ? true : false;

        //19
        EmpRankID = RankPara(Paradt, CompID, "EmpRankID");
        if (EmpRankID == "") EmpRankID = "100";
        IsUpEmpRankID = boolUpEmpRankID("D", FlowCaseID, EmpRankID);

    }
    private void ClearBtn(string FlowCustDB, string AssignTo, string CompID, string FlowCaseID, string AD)
    {
        //FlowExpress tbFlow = new FlowExpress(_CurrFlowID);
        bool IsUpValidRankID = true;
        bool IsUpEmpRankID = false;
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" update " + FlowCustDB + "FlowOpenLog ");
        sb.Append(" set FlowStepBatchEnabled=null ");
        sb.Append(" ,FlowStepOpinion=null ");
        sb.Append(" ,FlowStepBtnInfoCultureCode=null ");
        sb.Append(" ,FlowStepBtnInfoJSON=null ");
        sb.Append(" where AssignTo='" + AssignTo + "' ");
        db.ExecuteNonQuery(sb.BuildCommand());

        #region"前置資料(沒用)"
//        Dictionary<string, string> toUserData;
//        bool isLastFlow, nextIsLastFlow;
//        string 
//            flowCode = "",
//            flowSN = "",
//            signLineDefine = "",
//            meassge = "";

//        FlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, OTStartDate, FlowCaseID, flowCodeFlag,
//out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge);

//        #region"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"
//        if (toUserData.Count == 0)
//        {
//            //取[最近的行政or功能]資料 取代 [現在關卡]資料
//            DataTable toUDdt = HROverTimeLog(FlowCaseID, true);
//            toUserData.Add("SignLine", toUDdt.Rows[0]["SignLine"].ToString());
//            toUserData.Add("SignIDComp", toUDdt.Rows[0]["SignIDComp"].ToString());
//            toUserData.Add("SignID", AssignTo);
//            toUserData.Add("SignOrganID", toUDdt.Rows[0]["SignOrganID"].ToString());
//            toUserData.Add("SignFlowOrganID", toUDdt.Rows[0]["SignFlowOrganID"].ToString());
//        }
//        #endregion"下一關主管，如果行政功能線互轉主管剛好一樣再向上送一層"

        
        #endregion"前置資料"
        //if (AD == "A")
        //    AD = "0";
        //else
        //    AD = "1";

        RankIDCheck(CompID, FlowCaseID, out IsUpValidRankID, out IsUpEmpRankID);
        if (isLastFlowNow(CompID, FlowCaseID, AD))
        {
            if (IsUpValidRankID) Session["btnVisible"] = "2";
            else Session["btnVisible"] = "1";
        }
        else
        {
            if (IsUpEmpRankID) Session["btnVisible"] = "2";
            else Session["btnVisible"] = "1";
        }
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
    