using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Common.Properties;

public partial class Punch_PunchUpdateInquire : BasePage
{
    private string _AattendantDB = Util.getAppSetting("app://AattendantDB_OverTime/");
    private string _PunchUpdate = Util.getAppSetting("app://AattendantDB_PunchUpdate/");
    private string strDataKeyNames = "CompID,EmpID,EmpName,DutyDate,DutyTime,PunchDate,PunchTime,PunchConfirmSeq,DeptID,DeptName,OrganID,OrganName,FlowOrganID,FlowOrganName,Sex,PunchFlag,WorkTypeID,WorkType,MAFT10_FLAG,ConfirmStatus,AbnormalType,ConfirmPunchFlag,PunchSeq,PunchRemedySeq,RemedyReasonID,RemedyReasonCN,RemedyPunchTime,AbnormalFlag,AbnormalReasonID,AbnormalReasonCN,AbnormalDesc,Remedy_AbnormalFlag,Remedy_AbnormalReasonID,Remedy_AbnormalReasonCN,Remedy_AbnormalDesc,Source,APPContent,LastChgComp,LastChgID,LastChgDate,RemedyPunchFlag,BatchFlag,PORemedyStatus,RejectReason,RejectReasonCN,ValidDateTime,ValidCompID,ValidID,ValidName,Remedy_MAFT10_FLAG,ConfirmStatusGCN,ConfirmPunchFlagGCN,AbnormalReasonGCN,SourceGCN,SexGCN,FlowCaseID";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            subGetDate();
        }
    }



    private void subGetDate()
    {
        lblCompName.Text = UserInfo.getUserInfo().CompName;
        lblOrganName.Text = UserInfo.getUserInfo().OrganName;
        lblEmpName.Text = UserInfo.getUserInfo().UserID;
        lblTitleName.Text = UserInfo.getUserInfo().UserTitle;
        lblPositionName.Text = UserInfo.getUserInfo().WorkTypeName;
    }



    /// <summary>
    /// 取得字串(去除null)
    /// </summary>
    private string StringIIF(object str)
    {
        string result = "";
        if (str != null)
        {
            if (!string.IsNullOrEmpty(str.ToString().Trim()))
            {
                result = str.ToString();
            }
        }
        return result;
    }

    #region"BtnClick"
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DoQuery();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        DoClear();
    }

    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Detail"))
        {
            GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            PunchUpdate.GridViewToDictionary(gvMain, out dic, clickedRow.RowIndex, strDataKeyNames);
            Session["Punch_Confirm_Remedy_Bean"] = dic;
            Response.Redirect("PunchUpdateModify.aspx");
        }
    }
    #endregion"BtnClick"

    #region"DoBtn"
    private void DoQuery()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<Punch_Confirm_Remedy_Bean>();
        var viewData = new Punch_Confirm_Remedy_Model()
        {
            //CompID = StringIIF(UserInfo.getUserInfo().CompID),
            EmpID = StringIIF(UserInfo.getUserInfo().UserID),
            StartPunchDate = StringIIF(ucStartPunchDate.ucSelectedDate),
            EndPunchDate = StringIIF(ucEndPunchDate.ucSelectedDate),
            ConfirmPunchFlag = StringIIF(ddlConfirmPunchFlag.SelectedValue),
            ConfirmStatus = StringIIF(ddlConfirmStatus.SelectedValue),
            AbnormalFlag = StringIIF(ddlAbnormalFlag.SelectedValue)
        };
        isSuccess = PunchUpdate.PunchUpdateInquire_DoQuery(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = PunchUpdate.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
    }

    private void DoClear()
    {
        PunchUpdate.SelectValueDDL(ddlConfirmStatus, "");
        PunchUpdate.SelectValueDDL(ddlConfirmPunchFlag, "");
        PunchUpdate.SelectValueDDL(ddlAbnormalFlag, "");
        ucStartPunchDate.ucSelectedDate = "";
        ucEndPunchDate.ucSelectedDate = "";
        gvMain.Visible = false;
    }
    #endregion"DoBtn"
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "送簽中")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                LinkButton lbtn = (LinkButton)e.Row.FindControl("btnDetail");
                lbtn.Enabled = false;
                lbtn.Visible = false;
            }
        }
    }
}