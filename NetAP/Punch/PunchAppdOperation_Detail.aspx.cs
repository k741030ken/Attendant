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

public partial class Punch_PunchAppdOperation_Detail : SecurePage
{

    #region "1. 全域變數"
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");
    private string _PunchUpdate = Util.getAppSetting("app://AattendantDB_PunchUpdate/"); //PunchUpdate_ITRD
    //private string _DBName2 = "DB_VacSys";
    //public static string _DBShare = Util.getAppSetting("app://DB_Share_OverTime/");
    private string FlowCustDB = "";
    private string strDataKeyNames = "CompID,EmpID,EmpName,DutyDate,DutyTime,PunchDate,PunchTime,PunchConfirmSeq,DeptID,DeptName,OrganID,OrganName,FlowOrganID,FlowOrganName,Sex,PunchFlag,WorkTypeID,WorkType,MAFT10_FLAG,ConfirmStatus,AbnormalType,ConfirmPunchFlag,PunchSeq,PunchRemedySeq,RemedyReasonID,RemedyReasonCN,RemedyPunchTime,AbnormalFlag,AbnormalReasonID,AbnormalReasonCN,AbnormalDesc,Remedy_AbnormalFlag,Remedy_AbnormalReasonID,Remedy_AbnormalReasonCN,Remedy_AbnormalDesc,Source,APPContent,LastChgComp,LastChgID,LastChgDate,RemedyPunchFlag,BatchFlag,PORemedyStatus,RejectReason,RejectReasonCN,ValidDateTime,ValidCompID,ValidID,ValidName,Remedy_MAFT10_FLAG,ConfirmStatusGCN,ConfirmPunchFlagGCN,AbnormalReasonGCN,SourceGCN,SexGCN,SpecialFlag,RestBeginTime,RestEndTime,FlowCaseID";
    #endregion "1. 全域變數"

    protected void Page_Load(object sender, EventArgs e)
    {
        FlowExpress oFlow = new FlowExpress(_PunchUpdate);
        FlowCustDB = _PunchUpdate;

        if (!IsPostBack)
        {
            subGetData();
        }
    }

    private void subGetData()
    {
        Dictionary<string, string> dic = (Dictionary<string, string>)Session["PunchAppdOperation_GridView"];
        lblEmpName.Text = dic["EmpName"];
        lblPunchDate.Text = dic["PunchDate"];
        //打卡資料
        lblPunchTime.Text = dic["PunchTime"];
        lblConfirmPunchFlag.Text = dic["ConfirmPunchFlag"];
        if (dic["AbnormalFlag"] == "1") rdoAbnormalFlag1.Checked = true;
        else if(dic["AbnormalFlag"] == "2")rdoAbnormalFlag2.Checked = true;
        rdoAbnormalFlag1.Enabled = false;
        rdoAbnormalFlag2.Enabled = false;
        lblAbnormalReasonCN.Text = (string.IsNullOrEmpty(dic["AbnormalReasonCN"]) ? "" : "-") + dic["AbnormalReasonCN"];
        lblAbnormalDesc.Text = dic["AbnormalDesc"];
        //補登打卡資料
        lblRemedyReasonID.Text = dic["RemedyReasonID"];
        lblRemedyPunchTime.Text = dic["RemedyPunchTime"];
        lblConfirmPunchFlag.Text = dic["ConfirmPunchFlag"];
        if (dic["Remedy_AbnormalFlag"] == "1") rdoRemedy_AbnormalFlag1.Checked = true;
        else if (dic["Remedy_AbnormalFlag"] == "2") rdoRemedy_AbnormalFlag2.Checked = true;
        rdoRemedy_AbnormalFlag1.Enabled = false;
        rdoRemedy_AbnormalFlag2.Enabled = false;
        lblRemedy_AbnormalReasonCN.Text = (string.IsNullOrEmpty(dic["Remedy_AbnormalReasonCN"]) ? "" : "-") + dic["AbnormalReasonCN"];
        lblRemedy_AbnormalDesc.Text = dic["Remedy_AbnormalDesc"];
        //再放一次
        Session["PunchAppdOperation_GridView"] = dic;
    }
}
