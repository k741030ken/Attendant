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
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

public partial class Punch_PunchUpdateModify : BasePage
{
    #region "全域變數"
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    private string _PunchUpdate = Util.getAppSetting("app://AattendantDB_PunchUpdate/"); //PunchUpdate_ITRD
    private Aattendant at = new Aattendant();
    private static Punch_Confirm_Remedy_Bean Quire_to_Modify;
    private static string ErrorMsg = "";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            subGetData();
        }
    }

    private void subGetData()
    {
        if (Session["Punch_Confirm_Remedy_Bean"] == null)
        {
            //Response.Redirect("PunchUpdateInquire.aspx");
        }
        else
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)Session["Punch_Confirm_Remedy_Bean"];
            ViewState["Punch_Confirm_Remedy_Bean"] = Session["Punch_Confirm_Remedy_Bean"];
            Quire_to_Modify = PunchUpdate.Punch_Confirm_Remedy_DicToBean(dic);
            DoClear();
            //Session["Quire_to_Modify"] = null;
        }

    }

    private void DoClear()
    {
        if (!string.IsNullOrEmpty(Quire_to_Modify.EmpID))
        {
            lblEmpName.Text = Quire_to_Modify.EmpName;
            lblPunchDate.Text = Quire_to_Modify.PunchDate;
            lblPunchTime.Text = Quire_to_Modify.PunchTime;
            lblConfirmPunchFlag.Text = Quire_to_Modify.ConfirmPunchFlagGCN;
            ucRemedyPunchTime.ucDefaultSelectedHH = Quire_to_Modify.PunchTime.Split(":")[0];
            ucRemedyPunchTime.ucDefaultSelectedMM = Quire_to_Modify.PunchTime.Split(":")[1];

            //補登理由-下拉塞值
            ddlRemedyReasonID_Data();
            //非公務-下拉塞值
            ddlRemedy_AbnormalReasonID_Data();

            //修改前原因
            if (Quire_to_Modify.AbnormalFlag.Trim() == "2")
            {
                rdoAbnormalFlag2.Checked = true;
                lblAbnormalReasonCN.Text = Quire_to_Modify.AbnormalReasonCN.Trim();
                lbl_.Visible = true;
            }
            else
            {
                rdoAbnormalFlag1.Checked = true;
                lblAbnormalReasonCN.Visible = false;
            }

            //補登原因
            if (Quire_to_Modify.Remedy_AbnormalFlag.Trim() == "2")
            {
                rdoRemedy_AbnormalFlag2.Checked = true;
                PunchUpdate.SelectValueDDL(ddlRemedy_AbnormalReasonID, Quire_to_Modify.Remedy_AbnormalReasonID.Trim());
                ddlRemedy_AbnormalReasonID.Enabled = true;
            }
            else
            {
                rdoRemedy_AbnormalFlag1.Checked = true;
                ddlRemedy_AbnormalReasonID.Enabled = false;
            }

            //其他說明
            lblAbnormalDesc.Text = Quire_to_Modify.AbnormalDesc.Trim();
            txtRemedy_AbnormalDesc.Text = Quire_to_Modify.AbnormalDesc.Trim();
        }
        else
        {
            //Response.Redirect("PunchUpdateInquire.aspx");
        }
    }
    private void ddlRemedyReasonID_Data()
    {
        ////測試用===
        //ddlRemedyReasonID.Items.Clear();
        //ddlRemedyReasonID.Items.Insert(0, new ListItem("僅共測試", "99"));
        //ddlRemedyReasonID.Items.Insert(0, new ListItem("---請選擇---", ""));
        ////測試用===
        var isSuccess = false;
        var msg = "";
        var datas = new List<AT_CodeMap_Bean>();
        var viewData = new AT_CodeMap_Model()
        {
            TabName = "Punch",
            FldName = "RemedySeason"
        };
        ddlRemedyReasonID.Items.Clear();
        isSuccess = PunchUpdate.Select_AT_CodeMap(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectDDLDataList = PunchUpdate.GridDataFormat(datas); //Format Data         
        }
        try
        {
            ddlRemedyReasonID.DataSource = viewData.SelectDDLDataList;
            ddlRemedyReasonID.DataTextField = "CodeCName";
            ddlRemedyReasonID.DataValueField = "Code";
            ddlRemedyReasonID.DataBind();
        }
        catch (Exception)
        {
            Util.MsgBox("補登理由撈取資料失敗，請通知相關設定人員!!");
        }
        ddlRemedyReasonID.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    private void ddlRemedy_AbnormalReasonID_Data()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<AT_CodeMap_Bean>();
        var viewData = new AT_CodeMap_Model()
        {
            TabName = "Punch",
            FldName = "PunchSeason"
        };
        ddlRemedy_AbnormalReasonID.Items.Clear();
        isSuccess = PunchUpdate.Select_AT_CodeMap(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectDDLDataList = PunchUpdate.GridDataFormat(datas); //Format Data         
        }
        try
        {
            ddlRemedy_AbnormalReasonID.DataSource = viewData.SelectDDLDataList;
            ddlRemedy_AbnormalReasonID.DataTextField = "CodeCName";
            ddlRemedy_AbnormalReasonID.DataValueField = "Code";
            ddlRemedy_AbnormalReasonID.DataBind();
        }
        catch (Exception)
        {
            Util.MsgBox("非公務-補登原因撈取資料失敗，請通知相關設定人員!!");
        }
        ddlRemedy_AbnormalReasonID.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    #region"btnClick"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckData())
        {
            lblEmpName_Send.Text = lblEmpName.Text;
            lblPunchDate_Send.Text = lblPunchDate.Text;
            lblPunchTime_Send.Text = lblPunchTime.Text;
            lblConfirmPunchFlag_Send.Text = lblConfirmPunchFlag.Text;
            lblRemedyPunchTime_Send.Text = ucRemedyPunchTime.ucSelectedTime;
            lblRemedyReasonID_Send.Text = ddlRemedyReasonID.SelectedItem.Text;
            rdoRemedy_AbnormalFlag1_Send.Checked = rdoRemedy_AbnormalFlag1.Checked;
            rdoRemedy_AbnormalFlag2_Send.Checked = rdoRemedy_AbnormalFlag2.Checked;
            lblRemedy_AbnormalReasonID_Send.Text = ddlRemedy_AbnormalReasonID.SelectedItem.Text.Trim();
            lbl_Send.Visible = !string.IsNullOrEmpty(ddlRemedy_AbnormalReasonID.SelectedItem.Text.Trim());
            lblRemedy_AbnormalDesc_Send.Text = txtRemedy_AbnormalDesc.Text;

            ucModalPopup1.Reset();
            ucModalPopup1.Hide();
            ucModalPopup1.ucPanelID = pnlVerify.ID;
            ucModalPopup1.ucPopupHeight = 450;
            ucModalPopup1.ucPopupWidth = 800;
            ucModalPopup1.Show();
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            btnSubmit.Enabled = false;
            btnSubmit.Visible = false;
            btnClear.Enabled = false;
            btnClear.Visible = false;
            Util.MsgBox("送簽成功!!");
        }
        else
        {
            Util.MsgBox("送簽失敗!!");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DoClear();
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PunchUpdateInquire.aspx");
    }
    #endregion"btnClick"

    #region"Dobtn"
    private bool CheckData()
    {
        if (ddlRemedyReasonID.SelectedValue == "")
        {
            ddlRemedyReasonID.Focus();
            Util.MsgBox("請選擇補登理由!!");
            return false;
        }
        int TryTime = 0;
        if (!int.TryParse(ucRemedyPunchTime.ucDefaultSelectedHH.Trim(), out TryTime))
        {
            ucRemedyPunchTime.Focus();
            Util.MsgBox("請選擇補登打卡時間!!");
            return false;
        }
        if (!int.TryParse(ucRemedyPunchTime.ucDefaultSelectedMM.Trim(), out TryTime))
        {
            ucRemedyPunchTime.Focus();
            Util.MsgBox("請選擇補登打卡時間!!");
            return false;
        }
        if (rdoRemedy_AbnormalFlag2.Checked)
        {
            if (ddlRemedy_AbnormalReasonID.SelectedValue == "")
            {
                ddlRemedy_AbnormalReasonID.Focus();
                Util.MsgBox("請選擇補登原因!!");
                return false;
            }
            if (ddlRemedy_AbnormalReasonID.SelectedValue == "99")
            {
                if (txtRemedy_AbnormalDesc.Text == "")
                {
                    txtRemedy_AbnormalDesc.Focus();
                    Util.MsgBox("補登異常原因為”99:其他”，「補登異常其他說明」欄位不可空白!!");
                    return false;
                }
            }
        }
        return true;
    }

    private bool SaveData()
    {
        Dictionary<string, string> dic = (Dictionary<string, string>)ViewState["Punch_Confirm_Remedy_Bean"];
        Quire_to_Modify = PunchUpdate.Punch_Confirm_Remedy_DicToBean(dic);
        int Seq = 0;
        bool result = false;
        long seccessCount = 0;
        string msg = ""; //因為Exception用ShowMsg會不能顯示，所以取得msg後不使用(這就是沒用的變數)
        int PunchTimeHH = 0;
        if (!int.TryParse(ucRemedyPunchTime.ucDefaultSelectedHH, out PunchTimeHH)) PunchTimeHH = 0;

        Punch_Confirm_Remedy_Bean model = new Punch_Confirm_Remedy_Bean()
        {
            //跟Confirm一樣的內容，不修改
            FlowCaseID = "",
            CompID = Quire_to_Modify.CompID.Trim(),
            EmpID = Quire_to_Modify.EmpID.Trim(),
            EmpName = Quire_to_Modify.EmpName.Trim(),
            DutyDate = Quire_to_Modify.DutyDate.Trim(),
            DutyTime = Quire_to_Modify.DutyTime.Replace(":", "").Trim(),
            PunchDate = Quire_to_Modify.PunchDate.Trim(),
            PunchTime = Quire_to_Modify.PunchTime.Trim(),
            PunchConfirmSeq = Quire_to_Modify.PunchConfirmSeq.Trim(),
            DeptID = Quire_to_Modify.DeptID.Trim(),
            DeptName = Quire_to_Modify.DeptName.Trim(),
            OrganID = Quire_to_Modify.OrganID.Trim(),
            OrganName = Quire_to_Modify.OrganName.Trim(),
            FlowOrganID = Quire_to_Modify.FlowOrganID.Trim(),
            FlowOrganName = Quire_to_Modify.FlowOrganName.Trim(),
            MAFT10_FLAG = Quire_to_Modify.MAFT10_FLAG.Trim(),
            AbnormalFlag = Quire_to_Modify.AbnormalFlag.Trim(),
            AbnormalReasonID = Quire_to_Modify.AbnormalReasonID.Trim(),
            AbnormalReasonCN = Quire_to_Modify.AbnormalReasonCN.Trim(),
            AbnormalDesc = Quire_to_Modify.AbnormalDesc.Trim(),

            //Remedy修改項目
            PunchRemedySeq = (int.TryParse(Quire_to_Modify.PunchRemedySeq, out Seq) ? Seq + 1 : 1).ToString().Trim(),

            RemedyReasonID = ddlRemedyReasonID.SelectedValue.Trim(),
            RemedyReasonCN = ddlRemedyReasonID.SelectedValue.Trim() == "" ? "" : ddlRemedyReasonID.SelectedItem.Text.Trim(),
            RemedyPunchTime = ucRemedyPunchTime.ucSelectedTime.Trim(),

            Remedy_MAFT10_FLAG = (Quire_to_Modify.Sex.Trim() == "2" && (PunchTimeHH >= 22)) ? "1" : "0",
            Remedy_AbnormalFlag = rdoRemedy_AbnormalFlag1.Checked ? "1" : "2",  //待John檢核公用
            Remedy_AbnormalReasonID = ddlRemedy_AbnormalReasonID.SelectedValue.Trim(),
            Remedy_AbnormalReasonCN = ddlRemedy_AbnormalReasonID.SelectedValue.Trim() == "" ? "" : ddlRemedy_AbnormalReasonID.SelectedItem.Text.Trim(),
            Remedy_AbnormalDesc = txtRemedy_AbnormalDesc.Text.Trim(),

            LastChgComp = UserInfo.getUserInfo().CompID.Trim(),
            LastChgID = UserInfo.getUserInfo().UserID.Trim(),
            LastChgDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),

            //Remedy特有
            RemedyPunchFlag = Quire_to_Modify.ConfirmPunchFlag.Trim(), //補登檔RemedyPunchFlag=確認檔ConfirmPunchFlag=紀錄檔PunchFlag
            BatchFlag = Quire_to_Modify.BatchFlag.Trim(), //批次更新確認檔處理註記，0:未處理，1:已處理。(保留暫定)
            PORemedyStatus = "2",
            RejectReason = "",
            RejectReasonCN = "",
            ValidDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            //ValidTime = "",
            ValidCompID = UserInfo.getUserInfo().CompID.Trim(),
            ValidID = UserInfo.getUserInfo().UserID.Trim(),
            ValidName = UserInfo.getUserInfo().UserName.Trim()
        };
        result = PunchUpdate.PunchUpdateModify_SaveData(model, out seccessCount, out msg);
        if (!result)
        {
            msg = "送簽失敗!!-"+msg;
            return false;
        }
        if (seccessCount == 0)
        {
            msg = "送簽失敗!!";
            return false;
        }
        msg = "送簽成功!!";
        return true;
    }
    #endregion"Dobtn"
    protected void rdoRemedy_AbnormalFlag1_CheckedChanged(object sender, EventArgs e)
    {
        PunchUpdate.SelectValueDDL(ddlRemedy_AbnormalReasonID, "");
        ddlRemedy_AbnormalReasonID.Enabled = false;
    }
    protected void rdoRemedy_AbnormalFlag2_CheckedChanged(object sender, EventArgs e)
    {
        ddlRemedy_AbnormalReasonID.Enabled = true;
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {

    }
}