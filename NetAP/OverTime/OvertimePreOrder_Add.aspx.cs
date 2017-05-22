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

public partial class OverTime_OvertimePreOrder_Add : SecurePage
{
    #region "全域變數"
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    private Aattendant at = new Aattendant();
    public string _AttachID
    {
        get
        {
            if (ViewState["_AttachID"] == null)
            {
                ViewState["_AttachID"] = "";
            }
            return (string)ViewState["_AttachID"];
        }
        set
        {
            ViewState["_AttachID"] = value;
        }

    }
    public string _rankID
    {
        get
        {
            if (ViewState["_rankID"] == null)
            {
                ViewState["_rankID"] = "";
            }
            return (string)ViewState["_rankID"];
        }
        set
        {
            ViewState["_rankID"] = value;
        }
    }
    public string _EmpDate
    {
        get
        {
            if (ViewState["_EmpDate"] == null)
            {
                ViewState["_EmpDate"] = "";
            }
            return (string)ViewState["_EmpDate"];
        }
        set
        {
            ViewState["_EmpDate"] = value;
        }
    }
    public string _WorkSiteID
    {
        get
        {
            if (ViewState["_WorkSiteID"] == null)
            {
                ViewState["_WorkSiteID"] = "";
            }
            return (string)ViewState["_WorkSiteID"];
        }
        set
        {
            ViewState["_WorkSiteID"] = value;
        }
    }
    public string _FormNo
    {
        get
        {
            if (ViewState["_FormNo"] == null)
            {
                ViewState["_FormNo"] = "";
            }
            return (string)ViewState["_FormNo"];
        }
        set
        {
            ViewState["_FormNo"] = value;
        }

    }
    public string _OTTxnID
    {
        get
        {
            if (ViewState["_OTTxnID"] == null)
            {
                ViewState["_OTTxnID"] = "";
            }
            return (string)ViewState["_OTTxnID"];
        }
        set
        {
            ViewState["_OTTxnID"] = value;
        }

    }
    public string _OTSeq
    {
        get
        {
            if (ViewState["_OTSeq"] == null)
            {
                ViewState["_OTSeq"] = "";
            }
            return (string)ViewState["_OTSeq"];
        }
        set
        {
            ViewState["_OTSeq"] = value;
        }

    }
    public string _Sex
    {
        get
        {
            if (ViewState["_Sex"] == null)
            {
                ViewState["_Sex"] = "";
            }
            return (string)ViewState["_Sex"];
        }
        set
        {
            ViewState["_Sex"] = value;
        }

    }
    public string _FormNoRecord
    {
        get
        {
            if (ViewState["_FormNoRecord"] == null)
            {
                ViewState["_FormNoRecord"] = "";
            }
            return (string)ViewState["_FormNoRecord"];
        }
        set
        {
            ViewState["_FormNoRecord"] = value;
        }

    }
    public DataTable _dtPara
    {
        get
        {
            if (ViewState["_dtPara"] == null)
            {
                ViewState["_dtPara"] = null;
            }
            return (DataTable)(ViewState["_dtPara"]);
        }
        set
        {
            ViewState["_dtPara"] = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            //附件Attach
            string strAttachID = "";
            string strAttachAdminURL;
            string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
            string strAttachDownloadURL;
            string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
            //附件編號
            strAttachID = "test" + UserInfo.getUserInfo().UserID + Guid.NewGuid();
            ViewState["attach"] = strAttachID;
            _AttachID = strAttachID;
            strAttachAdminURL = string.Format(strAttachAdminBaseURL, _overtimeDBName, strAttachID, "1", "3072", "3072", "");
            strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, _overtimeDBName, strAttachID);
            frameAttach.Value = strAttachAdminURL;
            getAttachName();
            ddlOrg.SelectedValue = "1";
        }
        //特殊人員設定
        ucCascadingDropDown1.ucCategory01 = "CompID";
        ucCascadingDropDown1.ucCategory02 = "DeptID";
        ucCascadingDropDown1.ucCategory03 = "OrganID";
        ucCascadingDropDown1.ucCategory04 = "UserID";
        ucCascadingDropDown1.ucDropDownListEnabled04 = true;
        ucCascadingDropDown1.ucDefaultSelectedValue01 = UserInfo.getUserInfo().CompID;
        ucCascadingDropDown1.ucDefaultSelectedValue02 = UserInfo.getUserInfo().DeptID;
        ucCascadingDropDown1.ucDefaultSelectedValue03 = UserInfo.getUserInfo().OrganID;
        ucCascadingDropDown1.ucIsReadOnly01 = true;
        ucCascadingDropDown1.ucIsVerticalLayout = true;
        ucCascadingDropDown1.ucDropDownListReadOnlyCssClass = "clsDropDownListReadOnly";
        ucCascadingDropDown1.ucDropDownListCssClass = "clsDropDownList";//人員的CSS(白底黑字)

        TextBox txtStartDate = (TextBox)ucDateStart.FindControl("txtDate");
        txtStartDate.AutoPostBack = true;
        txtStartDate.CausesValidation = true;
        txtStartDate.TextChanged += StartDate_TextChanged;

        TextBox txtEndDate = (TextBox)ucDateEnd.FindControl("txtDate");
        txtEndDate.AutoPostBack = true;
        txtEndDate.CausesValidation = true;
        txtEndDate.TextChanged += EndDate_TextChanged;

        //加班開始時間
        DropDownList ddlStart = (DropDownList)OTTimeStart.FindControl("ddlHH");
        ddlStart.AutoPostBack = true;
        ddlStart.CausesValidation = false;
        ddlStart.SelectedIndexChanged += StartTime_SelectedIndexChanged;

        ddlStart = (DropDownList)OTTimeStart.FindControl("ddlMM");
        ddlStart.AutoPostBack = true;
        ddlStart.CausesValidation = false;
        ddlStart.SelectedIndexChanged += StartTime_SelectedIndexChanged;

        //加班結束時間
        DropDownList ddlEnd = (DropDownList)OTTimeEnd.FindControl("ddlHH");
        ddlEnd.AutoPostBack = true;
        ddlEnd.CausesValidation = false;
        ddlEnd.SelectedIndexChanged += EndTime_SelectedIndexChanged;

        ddlEnd = (DropDownList)OTTimeEnd.FindControl("ddlMM");
        ddlEnd.AutoPostBack = true;
        ddlEnd.CausesValidation = false;
        ddlEnd.SelectedIndexChanged += EndTime_SelectedIndexChanged;
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopAttach_onClose);
        
    }
    protected void LoadData()
    {
        //畫面預設帶入的值
        lblCompID.Text = UserInfo.getUserInfo().CompID;
        txtOTCompName.Text = UserInfo.getUserInfo().CompName;
        lblDeptID.Text = UserInfo.getUserInfo().DeptID;
        txtDeptName.Text = UserInfo.getUserInfo().DeptName;
        lblOrganID.Text = UserInfo.getUserInfo().OrganID;
        txtOrganName.Text = UserInfo.getUserInfo().OrganName;
        txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
        txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
        txtOTRegisterName.Text = UserInfo.getUserInfo().UserID + "　" + UserInfo.getUserInfo().UserName;
        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
        txtMealTime.Text = "0";

        if (txtOTEmpID.Text != "" && Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
        {
            SignData();//加班人當月送簽核准駁回的時數
        }
        //加班類型
        DataTable dt = at.QueryData("Code,CodeCName", "AT_CodeMap", " AND TabName='OverTime' AND FldName='OverTimeType'");
        ddlOTTypeID.DataSource = dt;
        ddlOTTypeID.DataValueField = "Code";
        ddlOTTypeID.DataTextField = "CodeCName";
        ddlOTTypeID.DataBind();
        ddlOTTypeID.Items.Insert(0, new ListItem("　- -請選擇- -", "0"));
        //帶入參數設定檔
        _dtPara = at.Json2DataTable(at.QueryColumn("Para", "OverTimePara", " AND CompID = '" + lblCompID.Text + "'"));
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (_dtPara.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(ddlSalaryOrAdjust.SelectedValue) || ddlSalaryOrAdjust.SelectedValue == "0")
                {
                    ddlSalaryOrAdjust.SelectedValue = _dtPara.Rows[0]["SalaryOrAjust"].ToString();
                }
            }
            else
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
        }
        //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
        GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
    }
    protected void btnUpdateAttachName_Click(object sender, EventArgs e)
    {
        getAttachName();
    }
    /// <summary>
    /// 查詢附件的檔案名稱
    /// </summary>
    protected void getAttachName()
    {
        string strattach = (chkCopyAtt.Checked && Convert.ToBoolean(ViewState["AttchIn"]) == true) ? ViewState["attach"].ToString() : _AttachID;
        DataTable dt = at.QueryData("isnull(FileName,'') as FileName", "AttachInfo", " AND FileSize > 0 AND AttachID='" + strattach + "'");
        if (dt.Rows.Count > 0)
            lblAttachName.Text = "附件檔名：" + dt.Rows[0]["FileName"].ToString();
        else
            lblAttachName.Text = "(目前無附件)";
    }

    //讓畫面上的值恢復到一開始
    protected void initalData()
    {
        OTTimeStart.ucDefaultSelectedHH = "請選擇";
        OTTimeStart.ucDefaultSelectedMM = "請選擇";
        OTTimeEnd.ucDefaultSelectedHH = "請選擇";
        OTTimeEnd.ucDefaultSelectedMM = "請選擇";
        txtMealTime.Text = "0";
        txtOTTotalTime.Text = "";
        txtTotalDescription.Visible = false;
        tbTime.Visible = false;
        lblPeriod.Visible = false;
        txtOTTotalTimeHour.Text = "小時";
        lblStartSex.Visible = false;
        lblEndSex.Visible = false;
    }
    //開始日期(檢核申請範圍、到職日以前不能申請加班)
    protected void StartDate_TextChanged(object sender, EventArgs e) 
    {
        initalData();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate))
            {
                ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                string msg = "";
                //加班申請範圍
                TimeSpan totalBefore = (DateTime.Now.Date).Subtract(Convert.ToDateTime(ucDateStart.ucSelectedDate));
                TimeSpan totalAfter = (Convert.ToDateTime(ucDateStart.ucSelectedDate)).Subtract(DateTime.Now.Date);
                if (at.CheckHolidayOrNot(ucDateStart.ucSelectedDate))
                {
                    msg = "此筆為假日加班，請記得至公文系統申請出勤核備。";
                    ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funHolidayOrNot('" + msg + "');", true);
                }
                if (DateTime.Now > Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    if (Convert.ToInt32(totalBefore.Days) > Convert.ToInt32(_dtPara.Rows[0]["AdvaceBegin"].ToString()))
                    {
                        Util.MsgBox("加班申請不可早於前" + _dtPara.Rows[0]["AdvaceBegin"].ToString() + "日");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                }
                else
                {
                    if (Convert.ToInt32(totalAfter.Days) > Convert.ToInt32(_dtPara.Rows[0]["AdvanceEnd"].ToString()))
                    {
                        Util.MsgBox("加班申請不可晚於後" + _dtPara.Rows[0]["AdvanceEnd"].ToString() + "日");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                }
                if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                {
                    if (txtOTEmpID.Text != "")
                    {
                        //檢查到職日以前不可以加
                        if (Convert.ToDateTime(ucDateStart.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                        {
                            Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                            ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                            return;
                        }
                        else
                        {
                            SignData();
                        }
                    }
                }
                //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
                ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
            }
        }
    }
    //結束日期(檢核申請範圍、到職日以前不能申請加班)
    protected void EndDate_TextChanged(object sender, EventArgs e) 
    {
        initalData();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                string msg = "";
                TimeSpan total = (Convert.ToDateTime(ucDateEnd.ucSelectedDate)).Subtract(Convert.ToDateTime(ucDateStart.ucSelectedDate));
                if (Convert.ToDateTime(ucDateEnd.ucSelectedDate) < Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    Util.MsgBox("加班結束日期不得小於加班開始日期");
                    ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                    ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                    return;
                }
                else if (Convert.ToInt32(total.Days) > 1)
                {
                    Util.MsgBox("加班日期間隔不得大於1日");
                    ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                    ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                    return;
                }
                if (at.CheckHolidayOrNot(ucDateEnd.ucSelectedDate))
                {
                    msg = "此筆為假日加班，請記得至公文系統申請出勤核備。";
                    ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funHolidayOrNot('" + msg + "');", true);
                }
                //加班申請範圍
                TimeSpan totalBefore = (DateTime.Now.Date).Subtract(Convert.ToDateTime(ucDateEnd.ucSelectedDate));
                TimeSpan totalAfter = (Convert.ToDateTime(ucDateEnd.ucSelectedDate)).Subtract(DateTime.Now.Date);
                if (DateTime.Now > Convert.ToDateTime(ucDateEnd.ucSelectedDate))
                {
                    if (Convert.ToInt32(totalBefore.Days) > Convert.ToInt32(_dtPara.Rows[0]["AdvaceBegin"].ToString()))
                    {
                        Util.MsgBox("加班申請不可早於前" + _dtPara.Rows[0]["AdvaceBegin"].ToString() + "日");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                }
                else
                {
                    if (Convert.ToInt32(totalAfter.Days) > Convert.ToInt32(_dtPara.Rows[0]["AdvanceEnd"].ToString()))
                    {
                        Util.MsgBox("加班申請不可晚於後" + _dtPara.Rows[0]["AdvanceEnd"].ToString() + "日");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                }
                if (txtOTEmpID.Text != "")
                {
                    //檢查到職日以前不可以加
                    if (Convert.ToDateTime(ucDateStart.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                    {
                        Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                    else if (Convert.ToDateTime(ucDateEnd.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                    {
                        Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                    else
                    {
                        SignData();
                    }
                }
            }
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
            {
                trTwo.Visible = false;
                lblDateTwo.Text = "";
                lblDateTwo_0.Text = "";
                lblDateTwo_1.Text = "";
                lblDateTwo_2.Text = "";
            }
            //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
            ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
            ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
        }
    }
    //開始時間(女性十點過後到隔天凌晨六點之前不能加班(需顯示提醒))
    protected void StartTime_SelectedIndexChanged(object sender, EventArgs e) 
    {
        if(txtOTEmpID.Text!="")
        {
            if (_Sex != "" && _Sex == "2")
            {
                if (OTTimeStart.ucDefaultSelectedHH != "請選擇")
                {
                    //從10點開始
                    if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) == 22)
                    {
                        lblStartSex.Visible = true;
                    }
                    else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > 22)
                    {
                        lblStartSex.Visible = true; 
                    }
                    //從凌晨開始到六點
                    else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) >= 0 && Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) <6 )
                    {
                        lblStartSex.Visible = true; 
                    }
                    else
                    {
                        lblStartSex.Visible = false;
                    }
                }
            }
            else
            {
                lblStartSex.Visible = false;
                lblEndSex.Visible = false;
            }
        }
        if (OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇")
        {
            EndTime_SelectedIndexChanged(null, null);
        }
    }
    //結束時間(女性十點過後到隔天凌晨六點之前不能加班(需顯示提醒)、結束時間不可以小於開始時間、加班超過兩小時需扣用餐時間、時段計算)
    protected void EndTime_SelectedIndexChanged(object sender, EventArgs e) 
    {
        string strcheckMealFlag = (chkMealFlag.Checked == true) ? "1" : "0";
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            if (_Sex != "" && _Sex == "2")
            {
                if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) == 22)
                {
                    if (OTTimeEnd.ucDefaultSelectedMM != "請選擇" && Convert.ToInt32(OTTimeEnd.ucDefaultSelectedMM) >= 1)
                    {
                        lblEndSex.Visible = true; 
                    }
                    else
                    {
                        lblEndSex.Visible = false;
                    }
                }
                else if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) > 22)
                {
                    lblEndSex.Visible = true; 
                }
                else if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) >= 0 && Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) < 6)
                {
                    lblEndSex.Visible = true;
                }
                else
                {
                    lblEndSex.Visible = false;
                }
            }
            else
            {
                lblEndSex.Visible = false;
            }

            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                if (OTTimeEnd.ucDefaultSelectedHH == "00" & OTTimeEnd.ucDefaultSelectedMM == "00")
                {
                    Util.MsgBox("最大時間上限為23:59");
                }
                if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH))
                {
                    Util.MsgBox("結束時間小於開始時間");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }
                if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) >= Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) && Convert.ToInt32(OTTimeStart.ucDefaultSelectedMM) > Convert.ToInt32(OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("結束時間小於開始時間");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }

                getCntTotal(out cntTotal);

                if (cntTotal <= 0)
                {
                    Util.MsgBox("加班時間不可以低於一分鐘");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }
                if (txtOTEmpID.Text != "")
                {
                    if (cntTotal > 120) //加班超過兩小時需扣用餐時間60分鐘
                    {
                        EtxtMealTimeChecked(null, null, true);
                    }
                    else
                    {
                        EtxtMealTimeChecked(null, null, false);
                    }
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeAdvance", txtOTEmpID.Text, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                                    ucDateStart.ucSelectedDate, 0, 0, "1900/01/01", iMealTime, mealFlag, "", out returnPeriodCount);

                                if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                                {
                                    var sReturnPeriodList = returnPeriodCount.Split(';');

                                    for (var i = 0; i < sReturnPeriodList.Length; i++)
                                    {
                                        var datas = sReturnPeriodList[i];
                                        lblPeriod.Visible = true;
                                        tbTime.Visible = true;
                                        if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trOne.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }
                                        if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trTwo.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }
                                    }
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
            else //跨日
            {
                if (txtOTEmpID.Text != "")
                {
                    getCntStartAndCntEnd(out cntStart, out cntEnd);

                    if (cntEnd + cntStart > 120) //加班超過兩小時需扣用餐時間60分鐘
                    {
                        EtxtMealTimeChecked(null, null, true);
                    }
                    else
                    {
                        EtxtMealTimeChecked(null, null, false);
                    }
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeAdvance", txtOTEmpID.Text, cntStart, cntEnd, iOTTimeStart, 2359,
                                    ucDateStart.ucSelectedDate, 0, iOTTimeEnd, ucDateEnd.ucSelectedDate, iMealTime, mealFlag, "", out returnPeriodCount);

                                if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                                {
                                    var sReturnPeriodList = returnPeriodCount.Split(';');

                                    for (var i = 0; i < sReturnPeriodList.Length; i++)
                                    {
                                        var datas = sReturnPeriodList[i];
                                        lblPeriod.Visible = true;
                                        tbTime.Visible = true;
                                        if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trOne.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }

                                        if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trTwo.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }
                                    }
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
        }
    }

    /// <summary>
    /// 跨日分鐘
    /// </summary>
    /// <param name="cntStart"></param>
    /// <param name="cntEnd"></param>
    private void getCntStartAndCntEnd(out double cntStart, out double cntEnd)
    {
        cntStart = 0;
        cntEnd = 0;

        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            cntStart = (23 - (Convert.ToDouble(OTTimeStart.ucDefaultSelectedHH))) * 60 + (60 - Convert.ToDouble(OTTimeStart.ucDefaultSelectedMM));
            cntEnd = (Convert.ToDouble(OTTimeEnd.ucDefaultSelectedHH)) * 60 + Convert.ToDouble(OTTimeEnd.ucDefaultSelectedMM);
        }

    }

    /// <summary>
    /// 不跨日分鐘
    /// </summary>
    /// <param name="cntTotal"></param>
    private void getCntTotal(out double cntTotal)
    {
        cntTotal = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            cntTotal = (Convert.ToDouble(OTTimeEnd.ucDefaultSelectedHH) * 60 + Convert.ToDouble(OTTimeEnd.ucDefaultSelectedMM)) - (Convert.ToDouble(OTTimeStart.ucDefaultSelectedHH) * 60 + Convert.ToDouble(OTTimeStart.ucDefaultSelectedMM));
        }
    }

    /// <summary>
    /// 用餐時數(參數檔)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="isChecked"></param>
    protected void EtxtMealTimeChecked(object sender, EventArgs e, bool isChecked)
    {
        chkMealFlag.Checked = isChecked;
        if (isChecked)
        {
            txtMealTime.Enabled = true;
            string  strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            if (!string.IsNullOrEmpty(strHo))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return;
                }
                else
                {
                    if (strHo == "0")
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeN"].ToString();
                    }
                    else
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeH"].ToString();
                    }
                }
            }
            else
            {
                txtMealTime.Text = "60";
            }
        }
        else
        {
            txtMealTime.Text = "0";
            txtMealTime.Enabled = false;
        }
    }
    protected void chkMealFlag_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkMealFlag.Checked)
        {
            txtMealTime.Text = "0";
            txtMealTime.Enabled = false;
        }
        else
        {
            double cntTotal = 0;
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                getCntTotal(out cntTotal);
            }
            else
            {
                double cntStart = 0;
                double cntEnd = 0;
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                cntTotal = cntStart + cntEnd;
            }

            if (cntTotal > 120) //加班超過兩小時需扣用餐時間60分鐘
            {
                txtMealTime.Enabled = true;
                string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                if (!string.IsNullOrEmpty(strHo))
                {
                    if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return;
                }
                else
                {
                    if (strHo == "0")
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeN"].ToString();
                    }
                    else
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeH"].ToString();
                    }
                        }
                }
                else
                {
                    txtMealTime.Text = "60";
                }
            }
            else
            {
                txtMealTime.Text = "0";
                txtMealTime.Enabled = true;
            }
        }
        txtMealTime_TextChanged(null, null);
    }
    protected void txtMealTime_TextChanged(object sender, EventArgs e)
    {
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "" && txtOTEmpID.Text != "")
        {
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                if (OTTimeEnd.ucDefaultSelectedHH == "00" & OTTimeEnd.ucDefaultSelectedMM == "00")
                {
                    Util.MsgBox("最大時間上限為23:59");
                    initalData();
                    return;
                }
                getCntTotal(out cntTotal);
                if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntTotal))
                {
                    txtMealTime.Focus();
                    Util.MsgBox("用餐時數超過加班時數");
                }
                else
                {
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeAdvance", txtOTEmpID.Text, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                                    ucDateStart.ucSelectedDate, 0, 0, "1900/01/01", iMealTime, mealFlag, "", out returnPeriodCount);

                                if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                                {
                                    var sReturnPeriodList = returnPeriodCount.Split(';');

                                    for (var i = 0; i < sReturnPeriodList.Length; i++)
                                    {
                                        var datas = sReturnPeriodList[i];
                                        lblPeriod.Visible = true;
                                        tbTime.Visible = true;
                                        if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trOne.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }

                                        if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trTwo.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }
                                    }
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
            else //跨日
            {
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntEnd + cntStart))
                {
                    Util.MsgBox("用餐時數超過加班時數");
                    txtMealTime.Focus();
                }
                else
                {
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeAdvance", txtOTEmpID.Text, cntStart, cntEnd, iOTTimeStart, 2359,
                                    ucDateStart.ucSelectedDate, 0, iOTTimeEnd, ucDateEnd.ucSelectedDate, iMealTime, mealFlag, "", out returnPeriodCount);

                                if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                                {
                                    var sReturnPeriodList = returnPeriodCount.Split(';');

                                    for (var i = 0; i < sReturnPeriodList.Length; i++)
                                    {
                                        var datas = sReturnPeriodList[i];
                                        lblPeriod.Visible = true;
                                        tbTime.Visible = true;
                                        if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trOne.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }

                                        if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                                        {
                                            trTwo.Visible = true;
                                            if (datas.Split(',').Length >= 1)
                                            {
                                                lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                            }
                                            if (datas.Split(',').Length >= 2)
                                            {
                                                lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                            }
                                            if (datas.Split(',').Length >= 3)
                                            {
                                                lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                            }
                                            if (datas.Split(',').Length >= 4)
                                            {
                                                lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                            }
                                        }
                                    }
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
        }
    }
    //轉補休：顯示補修失效日
    protected void ddlSalaryOrAdjust_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryOrAdjust.SelectedValue == "2")
        {
            //DataTable dtPara = at.QueryData("AdjustInvalidDate", "OverTimePara", " AND CompID = '" + UserInfo.getUserInfo().CompID + "'");
            if (_dtPara == null)
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
            else
            {
                if (_dtPara.Rows.Count > 0)
                {
                    lblAdjustInvalidDate.Visible = true;
                    txtAdjustInvalidDate.Visible = true;
                    txtAdjustInvalidDate.Text = Convert.ToDateTime(_dtPara.Rows[0]["AdjustInvalidDate"].ToString()).ToString("yyyy/MM/dd");
                }
                else
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return;
                }
            }
        }
        else
        {
            lblAdjustInvalidDate.Visible = false;
            txtAdjustInvalidDate.Visible = false;
        }
    }
    /// <summary>
    /// 依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
    /// </summary>
    /// <remarks>
    /// RankID大於等於19 : 只能轉補休
    /// RankID小於19且兩天皆為假日 : 可轉補休或轉薪資
    /// RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
    /// </remarks>
    /// <param name="sRankID">string</param>
    /// <param name="startDate">string</param>
    /// <param name="endDate">string</param>
    private void ddlSalaryOrAdjustChange(string sRankID, string startDate, string endDate)
    {
        if (!string.IsNullOrEmpty(sRankID) && !string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
        {
            bool bRankIDisNumeric = at.IsNumeric(sRankID);
            decimal dRankID = Aattendant.GetDecimal(sRankID);       //將加班人的RankID轉為Decimal
            bool bRankIDMapisNumeric = at.IsNumeric(sRankID);      //判斷參數設定的轉補休職等設定有設定(也就是參數必需是數字!)
            bool datesIsDifferent = at.CompareDate(startDate, endDate) != 0 ? true : false;     //判斷加班起訖日是否是同一天,false為同一天,true為不同天
            bool dateStartIsHoliday = at.CheckHolidayOrNot(startDate);
            bool dateEndIsHoliday = at.CheckHolidayOrNot(endDate);

            //先回到預設值並Enable
            ddlSalaryOrAdjust.Enabled = true;
            ddlSalaryOrAdjust.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(_dtPara.Rows[0]["AdjustRankID"].ToString()))
            {
                //取得轉補休職等設定之職等數值
                var dRankIDMap = Aattendant.GetDecimal(Aattendant.GetRankIDFormMapping(lblCompID.Text, _dtPara.Rows[0]["AdjustRankID"].ToString()));

                //如果加班人職等大於等於參數設定的職等，僅能選擇轉補休
                if (dRankID >= dRankIDMap)
                {
                    if (ddlSalaryOrAdjust.Items.Count >= 1)
                    {
                        ddlSalaryOrAdjust.Items[0].Selected = false;
                    }
                    if (ddlSalaryOrAdjust.Items.Count >= 2)
                    {
                        ddlSalaryOrAdjust.Items[1].Enabled = false;
                        ddlSalaryOrAdjust.Items[1].Selected = false;
                    }
                    if (ddlSalaryOrAdjust.Items.Count >= 3)
                    {
                        ddlSalaryOrAdjust.Items[2].Enabled = true;
                        ddlSalaryOrAdjust.Items[2].Selected = true;
                    }
                }
                //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資，反之則看參數設定
                else if (dRankID < dRankIDMap)
                {
                    //是跨日單
                    if (datesIsDifferent)
                    {
                        //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資
                        if (dateStartIsHoliday && dateEndIsHoliday)
                        {
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                //ddlSalaryOrAdjust.Items[0].Selected = true;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                //ddlSalaryOrAdjust.Items[1].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                //ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //假日預設選項為轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                        }
                        else
                        { //RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = false;
                                ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //重新再指向預設選項
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"));
                        }
                    }
                    else    //是單日單
                    {
                        if (dateStartIsHoliday)     //加班日為假日
                        {
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                //ddlSalaryOrAdjust.Items[1].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                //ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //假日預設選項為轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                        }
                        else        //加班日為平日
                        {
                            //RankID小於19且加班日為平日 : 看參數設定且要將選項disable
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                                {
                                    ddlSalaryOrAdjust.Items[1].Enabled = true;
                                    ddlSalaryOrAdjust.Items[1].Selected = true;
                                }
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                                {
                                    ddlSalaryOrAdjust.Items[1].Enabled = true;
                                    ddlSalaryOrAdjust.Items[1].Selected = true;
                                }
                                else
                                {
                                    ddlSalaryOrAdjust.Items[2].Enabled = true;
                                    ddlSalaryOrAdjust.Items[2].Selected = true;
                                }
                            }

                            //將選項鎖住
                            ddlSalaryOrAdjust.Enabled = false;
                        }
                    }
                }
            }
            //當轉補休職等設定為請選擇的時候，必須依據參數檔的加班轉換方式預設值(SalaryOrAjust) 
            else
            {
                //是跨日單
                if (datesIsDifferent)
                {
                    //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資
                    if (dateStartIsHoliday && dateEndIsHoliday)
                    {
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            //ddlSalaryOrAdjust.Items[0].Selected = true;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            //ddlSalaryOrAdjust.Items[1].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = true;
                            //ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //假日預設選項為轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                    }
                    else
                    { //RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            ddlSalaryOrAdjust.Items[1].Selected = true;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = false;
                            ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //重新再指向預設選項
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"));
                    }
                }
                else    //是單日單
                {
                    if (dateStartIsHoliday)     //加班日為假日
                    {
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            //ddlSalaryOrAdjust.Items[1].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = true;
                            //ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //假日預設選項為轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                    }
                    else        //加班日為平日
                    {
                        //RankID小於19且加班日為平日 : 看參數設定且要將選項disable
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                            else
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                ddlSalaryOrAdjust.Items[2].Selected = true;
                            }
                        }

                        //將選項鎖住
                        ddlSalaryOrAdjust.Enabled = false;
                    }
                }
            }
        }
    }
    //多筆附件
    protected void chkCopyAtt_CheckedChanged(object sender, EventArgs e) 
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string strAttachID = "";
        if (!chkCopyAtt.Checked)
        {
            if (ViewState["attach"].ToString().IndexOf("test") < 0)
            {
                //附件也需要清掉
                string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1  Where AttachID = '{0}' ;", _AttachID);
                if (db.ExecuteNonQuery(CommandType.Text, strDelSQL) >= 0)
                {
                    Util.IsAttachInfoLog(_overtimeDBName, _AttachID, 1, "Delete");
                }
                strAttachID = _AttachID;
                btnUploadAttach.Visible = true;
                chkCopyAtt.Visible = true;
                if (ViewState["attach"].ToString() == "")
                {
                    chkCopyAtt.Visible = false;
                }
            }
            else
            {
                chkCopyAtt.Visible = false;
            }
        }
        else
        {
            if (ViewState["attach"].ToString().IndexOf("test") < 0)
            {
                btnUploadAttach.Visible = false;
                _AttachID = UserInfo.getUserInfo().UserID + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                ViewState["AttchIn"] = true;
                strAttachID = ViewState["attach"].ToString();
                sb.AppendStatement(" INSERT INTO AttachInfo(AttachID,SeqNo,FileName,FileExtName,FileSize,AnonymousAccess,UpdUser,UpdDate,UpdTime,FileBody,MD5Check)");
                sb.Append(" SELECT  '" + _AttachID + "',SeqNo,FileName,FileExtName,FileSize,AnonymousAccess,UpdUser,UpdDate,UpdTime,FileBody,MD5Check FROM AttachInfo");
                sb.Append(" WHERE AttachID='" + ViewState["attach"] + "'");
                db.ExecuteNonQuery(sb.BuildCommand());
                strAttachID = _AttachID;
            }
            else
            {
                btnUploadAttach.Visible = true;
                ViewState["AttchIn"] = false;
                strAttachID = _AttachID;
            }
        }
        //附件Attach        
        string strAttachAdminURL;
        string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
        string strAttachDownloadURL;
        string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
        strAttachAdminURL = string.Format(strAttachAdminBaseURL, _overtimeDBName, strAttachID, "1", "3072", "3072", "");
        strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, _overtimeDBName, strAttachID);
        frameAttach.Value = strAttachAdminURL;
        getAttachName();
    }
    //多筆勾選資料組成DATATABLE
    public void LoadCheckData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EmpID");
        dt.Columns.Add("OTStartDate");
        dt.Columns.Add("OTEndDate");
        dt.Columns.Add("OTStartTime");
        dt.Columns.Add("OTEndTime");
        dt.Columns.Add("OTCompID");
        dt.Columns.Add("OTFormNO");
        dt.Columns.Add("OTTotalTime");
        dt.Columns.Add("OTTxnID");
        foreach (GridViewRow row in gvMain.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_chkOverTime = (CheckBox)row.Cells[0].FindControl("chkChoose");
                if (chk_chkOverTime.Checked)
                {
                    DataRow dr = dt.NewRow();
                    dr["EmpID"] = gvMain.DataKeys[row.RowIndex].Values["OTEmpID"].ToString();
                    dr["OTStartDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[0];
                    dr["OTEndDate"] = gvMain.DataKeys[row.RowIndex].Values["OTDate"].ToString().Split('~')[1];
                    dr["OTStartTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[0]).Replace(":", "");
                    dr["OTEndTime"] = (gvMain.DataKeys[row.RowIndex].Values["OTTime"].ToString().Split('~')[1]).Replace(":", "");
                    dr["OTTotalTime"] = gvMain.DataKeys[row.RowIndex].Values["OTTotalTime"].ToString();
                    dr["OTCompID"] = gvMain.DataKeys[row.RowIndex].Values["OTCompID"].ToString();
                    dr["OTFormNO"] = gvMain.DataKeys[row.RowIndex].Values["OTFormNO"].ToString();
                    dr["OTTxnID"] = gvMain.DataKeys[row.RowIndex].Values["OTTxnID"].ToString();
                    dt.Rows.Add(dr);
                }
                ViewState["dt"] = dt;
            }
        }
    }
    //gridview data
    public void RefreshGrid()
    {
        string b = "";//利用表單編號尋找gridview
        if (!string.IsNullOrEmpty(_FormNoRecord))
        {
            string a = "'" + (_FormNoRecord.Replace(";", "','")) + "'";
            b = a.Substring(0, a.Length - 3);
        }
        else
        {
            b = "''";
        }
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" SELECT OT.OTEmpID,OT.OTCompID,OT.DeptID,OT.OrganID,OT.OTFormNO,P.NameN,OT.OTRegisterID,OT.OTTxnID,OTT.CodeCName,ISNULL(AI.FileName,'') AS FileName,OT.OTStatus,");
        sb.Append(" Case OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' END AS OTStatusName,");
        sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,");
        sb.Append(" (Left(OT.OTStartTime,2)+':'+Right(OT.OTStartTime,2)+'~'+ isnull(Left(OT2.OTEndTime,2)+':'+Right(OT2.OTEndTime,2),Left(OT.OTEndTime,2)+':'+Right(OT.OTEndTime,2))) AS OTTime,");
        //sb.Append(" Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime");
        sb.Append(" Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1)) AS OTTotalTime ");
        sb.Append(" FROM OverTimeAdvance OT ");
        sb.Append(" LEFT JOIN OverTimeAdvance OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.EmpID=OT.OTEmpID AND P.CompID = OT.OTCompID ");
        sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND OTT.TabName='OverTime' AND OTT.FldName='OverTimeType'");
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment AND FileSize>0");
        sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTRegisterComp = '" + UserInfo.getUserInfo().CompID + "' AND OT.OTRegisterID='" + UserInfo.getUserInfo().UserID + "' AND OT.OTFormNO IN (" + b + ") AND OT.OTStatus='1'");
        sb.Append(" ORDER BY OTTxnID");

        DataTable dtGrid = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        gvMain.DataSource = dtGrid;
        gvMain.DataBind();
        gvMain.Visible = true;

    }
    public void SignData()//本月的總時數
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //以單計算時數
        sb.Append("SELECT ISNULL(Convert(Decimal(10,1),SUM(A.Submit)),0) AS Submit,ISNULL(Convert(Decimal(10,1),SUM(A.Approval)),0) AS Approval,ISNULL(Convert(Decimal(10,1),SUM(A.Reject)),0) AS Reject FROM (");
        sb.Append(" SELECT ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '2' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Submit,"); 
	    sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '3' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Approval,");
	    sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '4' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Reject");
        sb.Append(" FROM OverTimeAdvance");
        sb.Append(" WHERE OTCompID = '" + lblCompID.Text + "'");
        sb.Append(" AND MONTH(OTStartDate) = MONTH('" + ucDateStart.ucSelectedDate + "') ");
        sb.Append(" AND YEAR(OTStartDate) = YEAR('" + ucDateStart.ucSelectedDate + "') ");
        sb.Append(" AND OTEmpID = '" + txtOTEmpID.Text + "'"); 
        sb.Append(" GROUP BY OTTxnID");
        sb.Append(" ) A");

        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        txtOTDateMonth.Text = (ucDateStart.ucSelectedDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? ucDateStart.ucSelectedDate.Substring(5, 2).Substring(1, 1).ToString() : ucDateStart.ucSelectedDate.Substring(5, 2).ToString();
        if (dt.Rows.Count > 0)
        {
            lblSubmit.Text = dt.Rows[0]["Submit"].ToString();//本月送簽總時數
            lblApproval.Text = dt.Rows[0]["Approval"].ToString();//本月核准總時數
            lblReject.Text = dt.Rows[0]["Reject"].ToString();//本月駁回總時數
        }
    }
    /// <summary>
    /// 暫存與送簽需檢核
    /// 1.畫面上的欄位都為必輸
    /// 2.用餐時數是否超過加班時數
    /// 3.檢查連續加班(假日不可以連續加班，也需檢查事前事後的資料表)
    /// 4.檢查申請時數是否超過參數檔的值(含當日申請)
    /// 5.檢查每個月申請上限的時數(國定假日不包含此檢查)
    /// 6.檢查連續上班日
    /// 7.檢查時間重疊：OverTime_BK、NaturalDisasterByCity、NaturalDisasterByEmp、OverTimeAdvance、OverTimeDeclaration
    /// </summary>
    /// <returns></returns>
    public bool checkData()//flag=1 暫存 flag=2 送簽string flag
    {
        if (txtOTEmpID.Text == "")
        {
            Util.MsgBox("您必須輸入加班人員工編號");
            return false;
        }
        if (ddlSalaryOrAdjust.SelectedValue == "0")
        {
            Util.MsgBox("需選擇加班轉換方式");
            return false;
        }
        if (ucDateStart.ucSelectedDate == "")
        {
            Util.MsgBox("您必須輸入加班開始日期");
            return false;
        }
        if (ucDateEnd.ucSelectedDate == "")
        {
            Util.MsgBox("您必須輸入加班結束日期");
            return false;
        }
        if (OTTimeStart.ucDefaultSelectedHH == "請選擇" || OTTimeStart.ucDefaultSelectedMM == "請選擇")
        {
            Util.MsgBox("您必須輸入加班開始時間");
            return false;
        }
        if (OTTimeEnd.ucDefaultSelectedHH == "請選擇" || OTTimeEnd.ucDefaultSelectedMM == "請選擇")
        {
            Util.MsgBox("您必須輸入加班結束時間");
            return false;
        }
        if (ddlOTTypeID.SelectedValue == "0")
        {
            Util.MsgBox("您必須選擇加班類型");
            return false;
        }
        if (txtOTReasonMemo.ucTextData == "")
        {
            Util.MsgBox("您必須填寫加班原因");
            return false;
        }
        if (chkMealFlag.Checked)
        {
            if (txtMealTime.Text == "" || Convert.ToDouble(txtMealTime.Text) <= 0)
            {
                Util.MsgBox("您必須填寫用餐時間");
                return false;
            }
        }
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = null;
        
        //用餐時數大於加班時間
        double cntTotal = 0.0;
        double cntStart = 0.0;
        double cntEnd = 0.0;
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
        {
            getCntTotal(out cntTotal);
            if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntTotal))
            {
                Util.MsgBox("用餐時數超過加班時數");
                return false;
            }
        }
        else
        {
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            cntTotal = cntStart + cntEnd;
            if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntTotal))
            {
                Util.MsgBox("用餐時數超過加班時數");
                return false;
            }
        }

        if (!Aattendant.IsNationalHoliday(ucDateStart.ucSelectedDate)) //檢查是否為國定假日
        {
            //檢查連續加班如果開始日期為假日
            if (at.CheckHolidayOrNot(ucDateStart.ucSelectedDate))
            {
                DateTime a = Convert.ToDateTime(ucDateStart.ucSelectedDate).AddDays(-1);//檢查前一天是否有存在資料庫
                DateTime b = Convert.ToDateTime(ucDateStart.ucSelectedDate).AddDays(1);//檢查後一天是否有存在資料庫

                //檢查事先申請是否連續加班
                if (at.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                else if (at.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                //檢查事後申報是否連續加班
                if (at.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                else if (at.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
            }
        }

        //檢查加班時數(含已核准)申請時數是否超過上限
        string message = "";
        ViewState["message"] = message;
        if (!checkOverTimeIsOver(out message))
        {
            if (_dtPara.Rows[0]["DayLimitFlag"].ToString() == "1")
            {
                Util.MsgBox(message);
                return false;
            }
            else
            {
                ViewState["message"] = message;
            }
        }
        //檢查每個月的上限
        if ((ucDateStart.ucSelectedDate).ToString().Substring(5, 2) == ucDateEnd.ucSelectedDate.ToString().Substring(5, 2)) //不跨月
        {
            if (!at.checkMonthTime("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntTotal, Convert.ToDouble(txtMealTime.Text), cntStart, cntEnd, ""))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }
        }
        else//跨月
        {
            string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text));
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            if (!at.checkMonthTime("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, ucDateStart.ucSelectedDate, ucDateStart.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntStart, Convert.ToDouble(mealOver.Split(',')[1]), cntStart, 0, ""))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }

            if (!at.checkMonthTime("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, ucDateEnd.ucSelectedDate, ucDateEnd.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntEnd, Convert.ToDouble(mealOver.Split(',')[3]), cntEnd, 0, ""))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }
        }
        int cnt = 0;
        //檢查連續上班日
        if (_dtPara.Rows[0]["OTMustCheck"].ToString() == "0")
        {
            int OTLimitDay = Convert.ToInt32(_dtPara.Rows[0]["OTLimitDay"].ToString());
            sb.Reset();
            sb.Append("SELECT Convert(varchar,C.SysDate,111) as SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')) O");
            sb.Append(" FULL OUTER JOIN(");
            sb.Append(" SELECT * FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] WHERE  CompID='" + lblCompID.Text + "' AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')) C ON O.OTStartDate=C.SysDate");
            sb.Append(" ORDER BY C.SysDate ASC");

            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SysDate"].ToString() == ucDateStart.ucSelectedDate || dt.Rows[i]["SysDate"].ToString() == ucDateEnd.ucSelectedDate)//本單
                {
                    cnt += 1;
                }
                else
                {
                    if (dt.Rows[i]["HolidayOrNot"].ToString() == "0")
                    {
                        cnt += 1;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i]["OTStartDate"].ToString()))
                        {
                            cnt += 1;
                        }
                        else
                        {
                            cnt = 0;
                        }
                    }
                }
                if (cnt >= OTLimitDay)
                {
                    message = "不得連續上班超過" + OTLimitDay.ToString() + "天";
                    if (_dtPara.Rows[0]["OTLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox(message);
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                    break;
                }
            }
        }

        //畫面上的時間
        int starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
        int endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);

        //檢核時間重疊(OverTime_BK)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime FROM OverTime_BK  WHERE CompID = '" + lblCompID.Text + "' AND EmpID='" + txtOTEmpID.Text + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND Convert(varchar,OTDate,111) ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND Convert(varchar,OTDate,111) IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++) //0500~0700
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }

                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }
            }
        }
        //檢核時間重疊(NaturalDisasterByCity)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime FROM NaturalDisasterByCity  WHERE WorkSiteID='" + _WorkSiteID + "' AND CompID='" + lblCompID.Text + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND DisasterStartDate ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND DisasterStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("留守時段重複");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }

                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
            }
        }
        //檢核時間重疊(NaturalDisasterByEmp)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime,LEFT(BeginTime,2) AS StartTimeHr,RIGHT(BeginTime,2) AS StartTimeM,LEFT(EndTime,2) AS EndTimeHr,RIGHT(EndTime,2) AS EndTimeM FROM NaturalDisasterByEmp  WHERE EmpID='" + txtOTEmpID.Text + "' AND CompID='" + lblCompID.Text + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND DisasterStartDate ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND DisasterStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("留守時段重複");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }

                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
            }
        }
        //檢核時間重疊(預先申請)
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Reset();
            sb.Append(" SELECT OTStartTime,OTEndTime FROM OverTimeAdvance  WHERE OTStatus in ('1','2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //起迄時間都有重疊 
                    if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["OTEndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                    {
                        Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                        return false;
                    }
                    //開始時間小於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間等於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間大於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            sb.Reset();
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime FROM OverTimeAdvance WHERE  OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus IN ('1','2','3') AND OTStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["OTStartDate"].ToString() == ucDateStart.ucSelectedDate)//起日
                    {
                        //起迄日重疊
                        endtime = 2359;
                        starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);

                        string strTxnID = at.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "' AND OTSeqNo='2'");
                        if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && strTxnID == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                    else//迄日
                    {
                        starttime = 0000;
                        endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                }
            }
        }

        //檢核時間重疊(事後申報)
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Reset();
            sb.Append(" SELECT OTStartTime,OTEndTime FROM OverTimeDeclaration  WHERE OTStatus in ('1','2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
            endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //起迄時間都有重疊 
                    if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["OTEndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                    {
                        Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                        return false;
                    }
                    //開始時間小於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間等於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間大於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            sb.Reset();
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus IN ('1','2','3') AND OTStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["OTStartDate"].ToString() == ucDateStart.ucSelectedDate)//起日
                    {
                        //起迄日重疊
                        endtime = 2359;
                        starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
                        string strTxnID = at.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "' AND OTSeqNo='2'");
                        if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && strTxnID == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                    else//迄日
                    {
                        starttime = 0000;
                        endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }

    //檢查是否當天超過加班時數(參數檔)
    private bool checkOverTimeIsOver(out string message)
    {
        bool result = true;
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        double dayNLimit = 0;
        double dayHLimit = 0;
        double hr = 0;
        double iMealTime = 0;
        double.TryParse(txtMealTime.Text, out iMealTime);
        message = "";
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return false;
        }
        else
        {
            dayNLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourN"].ToString());//平日可申請
            dayHLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourH"].ToString());//假日可申請
        }
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
        {
            DataTable dt = null;
            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "'  AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL"); 
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "') A ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                hr = double.Parse(dt.Rows[0]["TotalTime"].ToString());
            }
            getCntTotal(out cntTotal);
            cntTotal -= iMealTime;
            
            bool blHo = at.CheckHolidayOrNot(ucDateStart.ucSelectedDate);
            if (!blHo)//平日檢查
            {
                if (hr + cntTotal > (dayNLimit * 60))
                {
                    message = "加班時數(含已核准)申請時數已超過上限" + _dtPara.Rows[0]["DayLimitHourN"].ToString() + "小時";
                    result = false;
                }
            }
            else//假日
            {
                if (hr + cntTotal > (dayHLimit * 60))
                {
                    message = "加班時數(含已核准)申請時數已超過上限" + _dtPara.Rows[0]["DayLimitHourH"].ToString() + "小時";
                    result = false;
                }
            }
        }
        else
        {
            getCntStartAndCntEnd(out cntStart, out cntEnd);

            //資料庫的加班總時數
            DataTable dtStart = null;
            DataTable dtEnd = null;
            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateStart.ucSelectedDate + "' ");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL"); 
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateStart.ucSelectedDate + "') A ");
            dtStart = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateEnd.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL"); 
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTStatus in('2','3') AND OTCompID='" + lblCompID.Text + "' AND OTEmpID='" + txtOTEmpID.Text + "' AND OTStartDate='" + ucDateEnd.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "') A ");
            dtEnd = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            
            double hrStart = 0; 
            double hrEnd = 0; 
            if (dtStart.Rows.Count > 0)
            {
                hrStart = double.Parse(dtStart.Rows[0]["TotalTime"].ToString());
            }
            if (dtEnd.Rows.Count > 0)
            {
                hrEnd = double.Parse(dtEnd.Rows[0]["TotalTime"].ToString());
            }
            //國定假日若在平日是可以加班的，不算在連續加班
            string strStart = at.QueryColumn("Week", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            string strEnd = at.QueryColumn("Week", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + lblCompID.Text + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");
            
            bool blStartHo = at.CheckHolidayOrNot(ucDateStart.ucSelectedDate);
            bool blEndHo = at.CheckHolidayOrNot(ucDateEnd.ucSelectedDate);
            string mealOver = at.MealJudge(cntStart, iMealTime);
            
            if (blStartHo == false && blEndHo == false)//平日跨平日
            {
                if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayNLimit * 60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayNLimit * 60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
            else if (blStartHo == true && blEndHo == true)//假日跨假日
            {
                if (Aattendant.IsNationalHoliday(ucDateStart.ucSelectedDate))//國定假日跨假日不算連續加班
                {
                    if (strStart != "6" || strStart != "7")
                    {
                        if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit * 60))
                        {
                            message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit * 60))
                        {
                            message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        //result = true;
                    }
                    else
                    {
                        message = "不能假日連續加班";
                        result = false;
                    }
                }
                else if (Aattendant.IsNationalHoliday(ucDateEnd.ucSelectedDate))//假日跨國定假日不算連續加班
                {
                    if (strStart != "6" || strStart != "7")
                    {
                        if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit * 60))
                        {
                            message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit * 60))
                        {
                            message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        //result = true;
                    }
                    else
                    {
                        message = "不能假日連續加班";
                        result = false;
                    }
                }
                else
                {
                    message = "不能假日連續加班";
                    result = false;
                }
            }
            else if (blStartHo == false && blEndHo == true)//平日跨假日
            {
                if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayNLimit*60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit*60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
            else if (blStartHo == true && blEndHo == false)//假日跨平日
            {
                if (hrStart +  (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit*60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3]))  > (dayNLimit*60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
        }
        return result;
    }

    //暫存按鈕
    protected void btnTempSave_Click(object sender, EventArgs e)
    {
        if (checkData())
        {
            ShowTempSaveConfirm();
        }
    }
    //詢問是否要暫存
    protected void ShowTempSaveConfirm() 
    {
        string msg = "";
        if (string.IsNullOrEmpty(ViewState["message"].ToString()))
        {
            msg = "是否要暫存？";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave('" + msg + "');", true);
        }
        else
        {
            msg = ViewState["message"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave_Reminder('" + msg + "');", true);
        }
    }
    protected void btnTempSaveInvisible_Reminder_Click(object sender, EventArgs e)
    {
        string msg = "是否要暫存？";
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave('" + msg + "');", true);
    }
    protected void btnTempSaveInvisible_Click(object sender, EventArgs e)
    {
        SaveTempData();
        getAttachName();
        if (rbtSingleEmpID.Checked) //選擇單筆加班人，全部欄位回到預設值及清空
        {
            lblCompID.Text = UserInfo.getUserInfo().CompID;
            txtOTCompName.Text = UserInfo.getUserInfo().CompName;
            txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
            txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
            lblDeptID.Text = UserInfo.getUserInfo().DeptID;
            txtDeptName.Text = UserInfo.getUserInfo().DeptName;
            lblOrganID.Text = UserInfo.getUserInfo().OrganID;
            txtOrganName.Text = UserInfo.getUserInfo().OrganName;
            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            ucDateEnd.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            OTTimeStart.ucDefaultSelectedHH = "請選擇";
            OTTimeStart.ucDefaultSelectedMM = "請選擇";
            OTTimeEnd.ucDefaultSelectedHH = "請選擇";
            OTTimeEnd.ucDefaultSelectedMM = "請選擇";
            ddlOTTypeID.SelectedValue = "0";
            if (_dtPara == null)
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
            else
            {
                ddlSalaryOrAdjust.SelectedValue = _dtPara.Rows[0]["SalaryOrAjust"].ToString();
            }
            GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
            chkMealFlag.Checked = true;
            txtMealTime.Enabled = true;
            txtOTTotalTime.Text = "0.0";
            txtOTReasonMemo.ucTextData = "";
            txtTotalDescription.Visible = false;
            lblPeriod.Visible = false;
            tbTime.Visible = false;
            lblPeriod.Visible = false;
            lblStartSex.Visible = false;
            lblEndSex.Visible = false;
            txtOTTotalTimeHour.Text = "小時";
            txtOTTotalTime.Text = "";
            txtMealTime.Text = "0";
            txtOTDateMonth.Text = (ucDateStart.ucSelectedDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? ucDateStart.ucSelectedDate.Substring(5, 2).Substring(1, 1).ToString() : ucDateStart.ucSelectedDate.Substring(5, 2).ToString();
            lblSubmit.Text = "0.0";
            lblApproval.Text = "0.0";
            lblReject.Text = "0.0";
            _FormNoRecord += _FormNo + ";";
            _FormNo = "";//表單編號須重找
            chkCopyAtt.Visible = false;
        }
        else
        {
            chkCopyAtt.Visible = true;
            chkCopyAtt.Enabled = true;
            chkCopyAtt.Checked = false;
            chkCopyAtt_CheckedChanged(null, null);
            _FormNoRecord = _FormNo + ";";
            ViewState["SalaryOrAdjust"] = ddlSalaryOrAdjust.SelectedValue;//多筆加班人記住上一筆選的
        }
        RefreshGrid();
    }
    public void SaveTempData()
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        int OTSeq = 0;
        int OTSeqs = 0;

        if (_FormNo == "")
        {
            _FormNo = at.QueryFormNO("AdvanceFormSeq", UserInfo.getUserInfo().CompID,UserInfo.getUserInfo().UserID);
        }
        else if (rbtSingleEmpID.Checked)
        {
            _FormNo = at.QueryFormNO("AdvanceFormSeq", UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID);
        }
        //控制同上筆附檔
        string attach = at.QueryAtt(_AttachID, lblCompID.Text,txtOTEmpID.Text);
        ViewState["attach"] = attach;
        if (ViewState["attach"].ToString().IndexOf("test") >= 0)
        {
            chkCopyAtt.Visible = false;
        }
        else
        {
            chkCopyAtt.Visible = true;
        }
        
        string strcheckMealFlag = (chkMealFlag.Checked == true) ? "1" : "0";
        string strMealTime = (chkMealFlag.Checked == true) ? txtMealTime.Text : "0";
        string strAdjustInvalidDate = (txtAdjustInvalidDate.Visible == true) ? txtAdjustInvalidDate.Text : "";
        OTSeq = at.QuerySeq("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, ucDateStart.ucSelectedDate);
        
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
        {
            string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            double cntTotal = 0;
            getCntTotal(out cntTotal);
            sb.Reset();
            sb.Append(" INSERT INTO OverTimeAdvance(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,HolidayOrNot,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,LastChgComp,LastChgID,LastChgDate,OTRegisterComp)");
            sb.Append(" VALUES('" + UserInfo.getUserInfo().CompID + "', '" + txtOTEmpID.Text + "',");
            sb.Append(" '" + ucDateStart.ucSelectedDate + "', '" + ucDateEnd.ucSelectedDate + "',");
            sb.Append(" '" + OTSeq.ToString("00") + "',");
            sb.Append(" '" + (lblCompID.Text + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00")) + "',");
            sb.Append(" '1',");
            sb.Append(" '" + lblDeptID.Text + "', '" + lblOrganID.Text + "','" + txtDeptName.Text + "','" + txtOrganName.Text + "','',"); //流程ID(10)
            sb.Append(" '" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', '" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "',");
            sb.Append(" '" + cntTotal + "', ");
            sb.Append(" '" + ddlSalaryOrAdjust.SelectedValue + "' ,"); //轉薪資或補休
            if (ddlSalaryOrAdjust.SelectedValue == "2")
            {
                sb.Append(" '" + txtAdjustInvalidDate.Text + "', "); //失效時間
            }
            else
            {
                sb.Append(" '', "); //失效時間
            }
            sb.Append(" '" + strcheckMealFlag + "','" + strMealTime + "', ");
            sb.Append(" '" + ddlOTTypeID.SelectedValue + "','', '" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
            sb.Append(" '" + attach + "', '" + _FormNo + "', "); //上傳附件
            sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
            sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
            sb.Append(" '1','" + strHo + "', "); //申請單狀態(25)
            sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','','',  ");
            sb.Append(" '" + UserInfo.getUserInfo().CompID + "', ");
            sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
            sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            sb.Append(" '" + UserInfo.getUserInfo().CompID + "') ");
        }
        else
        {
            double cntStart = 0;
            double cntEnd = 0;
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text));

            string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");
            string crossDayArray = ucDateStart.ucSelectedDate + "," + ucDateEnd.ucSelectedDate;
            _OTTxnID = (UserInfo.getUserInfo().CompID + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
            sb.Reset();
            for (int i = 0; i < crossDayArray.Split(',').Length; i++)
            {
                sb.Append(" INSERT INTO OverTimeAdvance(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,HolidayOrNot,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,LastChgComp,LastChgID,LastChgDate,OTRegisterComp)");
                sb.Append(" VALUES('" + UserInfo.getUserInfo().CompID + "', '" + txtOTEmpID.Text + "',");
                if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                {
                    sb.Append(" '" + crossDayArray.Split(',')[0] + "', '" + crossDayArray.Split(',')[0] + "', '" + OTSeq.ToString("00") + "',");
                }
                else
                {
                    OTSeqs = at.QuerySeq("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, crossDayArray.Split(',')[1]);
                    sb.Append(" '" + crossDayArray.Split(',')[1] + "','" + crossDayArray.Split(',')[1] + "', '" + OTSeqs.ToString("00") + "',");
                }
                if (OTSeqs.ToString("00") != _OTTxnID.Substring(20, 2))
                {
                    _OTTxnID = (lblCompID.Text + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                }
                sb.Append(" '" + _OTTxnID + "',");
                if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                {
                    sb.Append(" '1',");
                }
                else
                {
                    sb.Append(" '2',");
                }
                sb.Append(" '" + lblDeptID.Text + "','" + lblOrganID.Text + "', '" + txtDeptName.Text + "','" + txtOrganName.Text + "','',");

                if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                {
                    sb.Append(" '" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', '2359','" + cntStart + "', ");
                }
                else
                {
                    sb.Append(" '0000', '" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "','" + cntEnd + "',");
                }
                sb.Append(" '" + ddlSalaryOrAdjust.SelectedValue + "' ,"); //轉薪資或補休
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" '" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" '', "); //失效時間
                }
                sb.Append(" '" + mealOver.Split(',')[0] + "', ");
                if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                {
                    sb.Append(" '" + mealOver.Split(',')[1] + "', ");
                }
                else
                {
                    sb.Append(" '" + mealOver.Split(',')[3] + "', ");
                }
                sb.Append(" '" + ddlOTTypeID.SelectedValue + "','','" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "', ");
                sb.Append(" '" + attach + "', '" + _FormNo + "',"); //上傳附件
                sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                sb.Append(" '1', "); //申請單狀態(25)

                if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                {
                    sb.Append(" '" + strHo1 + "', ");
                }
                else
                {
                    sb.Append(" '" + strHo2 + "', ");
                }
                sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','','', "); //公文單號(25)
                sb.Append(" '" + UserInfo.getUserInfo().CompID + "', ");
                sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                sb.Append(" '" + UserInfo.getUserInfo().CompID + "'); ");
            }
        }
        try
        {
            db.ExecuteNonQuery(sb.BuildCommand(), tx);
            tx.Commit();
            Util.MsgBox("暫存成功");
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            tx.Rollback();//資料更新失敗
            Util.MsgBox("暫存失敗");
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }

    //送簽按鈕
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (gvMain.Visible && gvMain.Rows.Count > 0)
        {
            if (checkMulitData())
            {
                ShowSubmitConfirm();
            }
        }
        else
        {
            if (checkData())//"2"
            {
                ShowSubmitConfirm();
            }
        }
    }
    public bool checkMulitData()
    {
        if (gvMain.Visible && gvMain.Rows.Count > 0)
        {
            LoadCheckData();
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt.Rows.Count <= 0)
            {
                Util.MsgBox("尚未勾選資料");
                return false;
            }
            string strMsg = at.GetMulitTotal(dt, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), "OverTimeAdvance");
            string message = "";
            ViewState["message"] = message;
            double dayNLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourN"].ToString());//平日可申請
            double dayHLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourH"].ToString());//假日可申請
            string strcheckOverTimeIsOver = at.GetCheckOverTimeIsOver(dt, dayNLimit, dayHLimit, "OverTimeAdvance");
            string strGetCheckOTLimitDay = at.GetCheckOTLimitDay(dt, _dtPara.Rows[0]["OTLimitDay"].ToString(), "OverTimeAdvance");
            if (!Convert.ToBoolean(strcheckOverTimeIsOver.Split(';')[0]))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    if (_dtPara.Rows[0]["DayLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox("員編(" + strcheckOverTimeIsOver.Split(';')[1] + ")" + strcheckOverTimeIsOver.Split(';')[2] + "已超過每天上限加班時數" + strcheckOverTimeIsOver.Split(';')[3] + "小時");
                        return false;
                    }
                    else {
                        message = "員編(" + strcheckOverTimeIsOver.Split(';')[1] + ")" + strcheckOverTimeIsOver.Split(';')[2] + "已超過每天上限加班時數" + strcheckOverTimeIsOver.Split(';')[3] + "小時";
                        ViewState["message"] = message;
                    }
                }
            }
            if (!Convert.ToBoolean(strMsg.Split(';')[0]))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox("員編(" + strMsg.Split(';')[1] + ")" + (strMsg.Split(';')[2]).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                        return false;
                    }
                    else {
                        message = "員編(" + strMsg.Split(';')[1] + ")" + (strMsg.Split(';')[2]).ToString().Substring(5, 2) + "月已超過每月上限加班時數" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                        ViewState["message"] = message;
                    }
                }
            }
            if (!Convert.ToBoolean(strGetCheckOTLimitDay.Split(';')[0]))
            {
                
                if (_dtPara.Rows[0]["OTLimitFlag"].ToString() == "1")
                {
                    Util.MsgBox("員編(" + strGetCheckOTLimitDay.Split(';')[1] + ")" + "不得連續上班超過" + _dtPara.Rows[0]["OTLimitDay"].ToString() + "天");
                    return false;
                }
                else {
                        message = "員編(" + strGetCheckOTLimitDay.Split(';')[1] + ")" + "不得連續上班超過" + _dtPara.Rows[0]["OTLimitDay"].ToString() + "天";
                        ViewState["message"] = message;
                }
            }
        }
        return true;
    }
    protected void ShowSubmitConfirm()
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit();", true);
        ////燈箱
        //ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
        //btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
        string msg = "";
        if (string.IsNullOrEmpty(ViewState["message"].ToString()))
        {
            msg = "是否要送簽？";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit('" + msg + "');", true);
            //燈箱
            ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
            btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
        }
        else
        {
            msg = ViewState["message"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit_Reminder('" + msg + "');", true);
        }
    }
    protected void btnSubmitInvisible_Reminder_Click(object sender, EventArgs e)
    {
        string msg = "是否要送簽？";
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit('" + msg + "');", true);
        //燈箱
        ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
        btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
    }
    protected void btnSubmitInvisible_Click(object sender, EventArgs e)
    {
        if (gvMain.Visible && gvMain.Rows.Count > 0)
        {
            if (checkMulitData())
            {
                SaveMuliData();
            }
        }
        else
        {
            SaveSinData();
            getAttachName();
            if (rbtSingleEmpID.Checked) //選擇單筆加班人，全部欄位回到預設值及清空
            {
                lblCompID.Text = UserInfo.getUserInfo().CompID;
                txtOTCompName.Text = UserInfo.getUserInfo().CompName;
                txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
                txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
                lblDeptID.Text = UserInfo.getUserInfo().DeptID;
                txtDeptName.Text = UserInfo.getUserInfo().DeptName;
                lblOrganID.Text = UserInfo.getUserInfo().OrganID;
                txtOrganName.Text = UserInfo.getUserInfo().OrganName;
                ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                ucDateEnd.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                OTTimeStart.ucDefaultSelectedHH = "請選擇";
                OTTimeStart.ucDefaultSelectedMM = "請選擇";
                OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                ddlOTTypeID.SelectedValue = "0";
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return;
                }
                else
                {
                    ddlSalaryOrAdjust.SelectedValue = _dtPara.Rows[0]["SalaryOrAjust"].ToString();
                }
                GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
                chkMealFlag.Checked = true;
                txtOTTotalTime.Text = "0.0";
                txtOTReasonMemo.ucTextData = "";
                txtTotalDescription.Visible = false;
                txtMealTime.Enabled = true;
                lblPeriod.Visible = false;
                tbTime.Visible = false;
                lblPeriod.Visible = false;
                txtOTTotalTimeHour.Text = "小時";
                txtOTTotalTime.Text = "";
                txtMealTime.Text = "0";
                txtOTDateMonth.Text = (ucDateStart.ucSelectedDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? ucDateStart.ucSelectedDate.Substring(5, 2).Substring(1, 1).ToString() : ucDateStart.ucSelectedDate.Substring(5, 2).ToString();
                lblSubmit.Text = "0.0";
                lblApproval.Text = "0.0";
                lblReject.Text = "0.0";
                lblStartSex.Visible = false;
                lblEndSex.Visible = false;
                //表單編號須重找
                _FormNo = "";
                chkCopyAtt.Visible = false;
            }
            else
            {
                chkCopyAtt.Visible = true;
                chkCopyAtt.Enabled = true;
                chkCopyAtt.Checked = false;
                chkCopyAtt_CheckedChanged(null, null);
                ViewState["SalaryOrAdjust"] = ddlSalaryOrAdjust.SelectedValue;//多筆加班人記住上一筆選的
            }
            RefreshGrid();
            if (gvMain.Rows.Count <= 0)
            {
                gvMain.Visible = false;
            }
        }
    }
    public void SaveSinData()//單筆送簽
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        Dictionary<string, string> oAssTo = new Dictionary<string, string>();
        
        double total = 0.0;
        var isSuccess = false;
        var toUserData = new Dictionary<string, string>();
        var empData = new Dictionary<string, string>();
        var flowCode = "";
        var flowSN = "";
        var nextIsLastFlow = false;
        var meassge = "";
        int OTSeqs = 0;
        isSuccess = FlowUtility.QueryFlowDataAndToUserData_First(lblCompID.Text, "", "", txtOTEmpID.Text, UserInfo.getUserInfo().UserID, ucDateStart.ucSelectedDate, "0",
            out empData, out toUserData, out flowCode, out flowSN, out nextIsLastFlow, out meassge);
        if (!isSuccess)
        {
            Util.MsgBox(meassge);
            return;
        }
        if ("".Equals(toUserData["SignID"]))
        {
            Util.MsgBox("查無審核人員，故無法送簽。");
            return;
        }
        try
        {
            if (_FormNo == "")
            {
                _FormNo = at.QueryFormNO("AdvanceFormSeq", UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID);
            }
            else if (rbtSingleEmpID.Checked)
            {
                _FormNo = at.QueryFormNO("AdvanceFormSeq", UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID);
            }
            string attach = at.QueryAtt(_AttachID, lblCompID.Text, txtOTEmpID.Text);
            ViewState["attach"] = attach;
            if (ViewState["attach"].ToString().IndexOf("test") >= 0)
            {
                chkCopyAtt.Visible = false;
            }
            else
            {
                chkCopyAtt.Visible = true;
            }
            int OTSeq = at.QuerySeq("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, ucDateStart.ucSelectedDate);
            string strcheckMealFlag = (chkMealFlag.Checked == true) ? "1" : "0";
            string strAdjustInvalidDate = (txtAdjustInvalidDate.Visible == true) ? txtAdjustInvalidDate.Text : "";
            string strMealTime = (chkMealFlag.Checked == true) ? txtMealTime.Text : "0";
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日送簽
            {
                string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                double cntTotal = 0;
                getCntTotal(out cntTotal);
                total = Math.Round((cntTotal - Convert.ToDouble(strMealTime)) / 60, 2);
                sb.Reset();
                sb.Append(" INSERT INTO OverTimeAdvance(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,HolidayOrNot,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,LastChgComp,LastChgID,LastChgDate,OTRegisterComp)");
                sb.Append(" VALUES('" + lblCompID.Text + "', '" + txtOTEmpID.Text + "',");
                sb.Append(" '" + ucDateStart.ucSelectedDate + "', '" + ucDateEnd.ucSelectedDate + "',");
                sb.Append(" '" + OTSeq.ToString("00") + "',");
                sb.Append(" '" + (lblCompID.Text + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00")) + "',");
                sb.Append(" '1',");//OTSeqNo
                sb.Append(" '" + lblDeptID.Text + "', '" + lblOrganID.Text + "','" + txtDeptName.Text + "','" + txtOrganName.Text + "',");
                sb.Append(" '', "); //流程ID(10)
                sb.Append(" '" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', '" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "',");
                sb.Append(" '" + cntTotal + "', ");
                sb.Append(" '" + ddlSalaryOrAdjust.SelectedValue + "' ,"); //轉薪資或補休
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" '" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" '', "); //失效時間
                }
                sb.Append(" '" + strcheckMealFlag + "', '" + strMealTime + "',");
                sb.Append(" '" + ddlOTTypeID.SelectedValue + "', '', '" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");//加班原因的
                sb.Append(" '" + attach + "', '" + _FormNo + "',"); //上傳附件表單編號
                sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                sb.Append(" '2', '" + strHo + "',"); //申請單狀態
                sb.Append(" '1900-01-01 00:00:00.000','', '1900-01-01 00:00:00.000','','',");//公文單號
                sb.Append(" '" + UserInfo.getUserInfo().CompID + "', ");
                sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                sb.Append(" '" + UserInfo.getUserInfo().CompID + "');");
            }
            else//跨日送簽
            {
                double cntStart = 0;
                double cntEnd = 0;
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text));
                total = Math.Round(((cntStart + cntEnd) - Convert.ToDouble(strMealTime)) / 60, 2);
                
                string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");
                string crossDayArray = ucDateStart.ucSelectedDate + "," + ucDateEnd.ucSelectedDate;
                _OTTxnID = (UserInfo.getUserInfo().CompID + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Reset();
                for (int i = 0; i < crossDayArray.Split(',').Length; i++)
                {
                    sb.Append(" INSERT INTO OverTimeAdvance(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,HolidayOrNot,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");
                    sb.Append(" VALUES('" + UserInfo.getUserInfo().CompID + "', '" + txtOTEmpID.Text + "',");
                    if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                    {
                        sb.Append(" '" + crossDayArray.Split(',')[0] + "', '" + crossDayArray.Split(',')[0] + "','" + OTSeq.ToString("00") + "',");
                    }
                    else
                    {
                        OTSeqs = at.QuerySeq("OverTimeAdvance", lblCompID.Text, txtOTEmpID.Text, crossDayArray.Split(',')[1]);
                        sb.Append(" '" + crossDayArray.Split(',')[1] + "', '" + crossDayArray.Split(',')[1] + "','" + OTSeqs.ToString("00") + "',");
                    }
                    if (OTSeqs.ToString("00") != _OTTxnID.Substring(20, 2))
                    {
                        _OTTxnID = (lblCompID.Text + txtOTEmpID.Text + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                    }
                    sb.Append(" '" + _OTTxnID + "',");
                    if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)//OTSeqNo
                    {
                        sb.Append(" '1',");
                    }
                    else
                    {
                        sb.Append(" '2',");
                    }
                    sb.Append(" '" + lblDeptID.Text + "', '" + lblOrganID.Text + "','" + txtDeptName.Text + "','" + txtOrganName.Text + "',");
                    sb.Append(" '', "); //流程ID(10)

                    if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                    {
                        sb.Append(" '" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "','2359','" + cntStart + "',  ");
                    }
                    else
                    {
                        sb.Append(" '0000', '" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "','" + cntEnd + "',");
                    }
                    sb.Append(" '" + ddlSalaryOrAdjust.SelectedValue + "' ,"); //轉薪資或補休
                    if (ddlSalaryOrAdjust.SelectedValue == "2")
                    {
                        sb.Append(" '" + txtAdjustInvalidDate.Text + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" '', "); //失效時間
                    }

                    sb.Append(" '" + mealOver.Split(',')[0] + "', ");
                    if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                    {
                        sb.Append(" '" + mealOver.Split(',')[1] + "', ");
                    }
                    else
                    {
                        sb.Append(" '" + mealOver.Split(',')[3] + "', ");
                    }
                    sb.Append(" '" + ddlOTTypeID.SelectedValue + "','','" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',  ");
                    sb.Append(" '" + attach + "', '" + _FormNo + "', "); //上傳附件表單編號
                    sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                    sb.Append(" '2', "); //申請單狀態(25)
                    if (crossDayArray.Split(',')[i] == ucDateStart.ucSelectedDate)
                    {
                        sb.Append(" '" + strHo1 + "', ");
                    }
                    else
                    {
                        sb.Append(" '" + strHo2 + "', ");
                    }
                    sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','','', "); //公文單號(25)
                    sb.Append(" '" + UserInfo.getUserInfo().CompID + "', ");
                    sb.Append(" '" + UserInfo.getUserInfo().UserID + "', ");
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                    sb.Append(" '" + UserInfo.getUserInfo().CompID + "');");
                }
            }
            //若有指派對象，才開始組合新增流程 IsFlowInsVerify() 所需的參數
            oAssTo.Clear();
            string name = at.QueryColumn("NameN", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", " AND EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "'");
            //需加部門
            string organName = "";
            //if (toUserData["SignLine"] == "1")
            //{
            //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + lblCompID.Text + "')");
            //}
            //else if (toUserData["SignLine"] == "2")
            //{
            //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] where OrganID='" + toUserData["SignFlowOrganID"] + "')");
            //}
            //else if (toUserData["SignLine"] == "3")
            //{
            //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + lblCompID.Text + "')");
            //}
            if (toUserData["SignLine"] == "1")//行政線
            {
                organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
            }
            else if (toUserData["SignLine"] == "2")//功能線
            {
                organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
            }
            else if (toUserData["SignLine"] == "3")//特殊人員線
            {
                organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
            }
            oAssTo.Add(toUserData["SignID"], organName + "-" + name);//oAssTo.Add(toUserData["SignID"], name);

            if (oAssTo.Count > 0)
            {
                string strStartTime = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                string strEndTime = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;

                string strKeyValue = "A," + lblCompID.Text + ",";
                strKeyValue += txtOTEmpID.Text + ",";
                strKeyValue += ucDateStart.ucSelectedDate + ",";
                strKeyValue += ucDateEnd.ucSelectedDate + ",";
                strKeyValue += OTSeq.ToString("00");

                string strShowValue = txtOTEmpID.Text + ",";
                strShowValue += txtOTEmpName.Text + ",";
                strShowValue += ucDateStart.ucSelectedDate + ",";
                strShowValue += OTTimeStart.ucDefaultSelectedHH + "：" + OTTimeStart.ucDefaultSelectedMM + ",";
                strShowValue += ucDateEnd.ucSelectedDate + ",";
                strShowValue += OTTimeEnd.ucDefaultSelectedHH + "：" + OTTimeEnd.ucDefaultSelectedMM;

                if (FlowExpress.IsFlowInsVerify(flow.FlowID, strKeyValue.Split(','), strShowValue.Split(','), nextIsLastFlow ? "btnBeforeLast" : "btnBefore", oAssTo, ""))
                {
                    //更新AssignToName(部門+員工姓名)
                    string a = FlowExpress.getFlowCaseID(flow.FlowID, strKeyValue);
                    if (!string.IsNullOrEmpty(a))
                    {
                        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
                        {
                            sb.AppendStatement("UPDATE OverTimeAdvance SET FlowCaseID='" + a + "'");
                            sb.Append(" WHERE OTCompID='" + lblCompID.Text + "'");
                            sb.Append(" AND OTEmpID='" + txtOTEmpID.Text + "'");
                            sb.Append(" AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
                            sb.Append(" AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
                            sb.Append(" AND OTStartTime='" + strStartTime + "'");
                            sb.Append(" AND OTEndTime='" + strEndTime + "'");
                            sb.Append(" AND OTSeq='" + OTSeq + "'");
                        }
                        else
                        {
                            sb.AppendStatement("UPDATE OverTimeAdvance SET FlowCaseID='" + a + "'");
                            sb.Append(" WHERE OTCompID='" + lblCompID.Text + "'");
                            sb.Append(" AND OTEmpID='" + txtOTEmpID.Text + "'");
                            sb.Append(" AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
                            sb.Append(" AND OTEndDate='" + ucDateStart.ucSelectedDate + "'");
                            sb.Append(" AND OTStartTime='" + strStartTime + "'");
                            sb.Append(" AND OTEndTime='2359'");
                            sb.Append(" AND OTSeq='" + OTSeq + "'");

                            sb.AppendStatement("UPDATE OverTimeAdvance SET FlowCaseID='" + a + "'");
                            sb.Append(" WHERE OTCompID='" + lblCompID.Text + "'");
                            sb.Append(" AND OTEmpID='" + txtOTEmpID.Text + "'");
                            sb.Append(" AND OTStartDate='" + ucDateEnd.ucSelectedDate + "'");
                            sb.Append(" AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
                            sb.Append(" AND OTStartTime='0000'");
                            sb.Append(" AND OTEndTime='" + strEndTime + "'");
                            sb.Append(" AND OTSeq='" + OTSeqs + "'");
                        }

                        //加進HROverTimeLog
                        FlowUtility.InsertHROverTimeLogCommand(a, "1", a + ".00001",
                           "A", empData["EmpID"], empData["OrganID"], empData["FlowOrganID"], UserInfo.getUserInfo().UserID,
                           flowCode, flowSN, "1", toUserData["SignLine"],
                           toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "0",
                           false, ref sb, 1, "1");

                        //加進MailLog
                        string Subject_1 = "";
                        string Content_1 = "";
                        string mail = "";
                        CommandHelper sbselect = db.CreateCommandHelper();
                        sbselect.Append("SELECT isnull(C.EMail,'') AS EMail FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P");
                        sbselect.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Communication] C ON P.IDNo=C.IDNo");
                        sbselect.Append(" WHERE P.CompID='" + toUserData["SignIDComp"] + "' AND P.EmpID='" + toUserData["SignID"] + "'");
                        DataTable dtSelect = db.ExecuteDataSet(sbselect.BuildCommand()).Tables[0];
                        if (dtSelect.Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(dtSelect.Rows[0]["EMail"].ToString()))
                            {
                                mail = HREmail();
                                Subject_1 = "系統查無通知者E-mail";
                                Content_1 = "OverTimeExpedite||BM@QuitMailContent1||系統查無通知者<br/>" + toUserData["SignID"] + "-" + name;
                            }
                            else
                            {
                                mail = dtSelect.Rows[0]["EMail"].ToString();
                                Subject_1 = "加班單流程待辦案件通知";
                                Content_1 = "OverTimeTodoList||BM@QuitMailContent1||" + name + "||BM@QuitMailContent2||" + _FormNo + "||BM@QuitMailContent3||" + strKeyValue.Split(',')[2] + "||BM@QuitMailContent4||" + txtOTEmpName.Text + "||BM@QuitMailContent5||" + strShowValue.Split(',')[2] + "~" + strShowValue.Split(',')[4] + "||BM@QuitMailContent6||" + strShowValue.Split(',')[3] + "||BM@QuitMailContent7||" + strShowValue.Split(',')[5] + "||BM@QuitMailContent8||" + total.ToString("0.0");
                            }
                        }
                        Aattendant.InsertMailLogCommand("人力資源處", toUserData["SignIDComp"], toUserData["SignID"], mail, "", "", Subject_1, Content_1, false, ref sb);
                    }
                    try
                    {
                        db.ExecuteNonQuery(sb.BuildCommand());
                        tx.Commit();
                        Util.MsgBox("送簽成功");//提案送審成功
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                        tx.Rollback();//資料更新失敗
                        Util.MsgBox("送簽失敗(3)"); //提案送審失敗
                        return;
                    }
                }
                else
                {
                    Util.MsgBox("送簽失敗(3)"); //提案送審失敗
                    tx.Rollback();//資料更新失敗
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            tx.Rollback();//資料更新失敗
            Util.MsgBox("送簽失敗(5)");
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }
    protected string HREmail() //尚未有email的人從SC_UserGroup這張表找
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string mail = "";
        sb.Append("SELECT SC.UserID,C.EMail FROM " + Util.getAppSetting("app://HRDB_OverTime/") + ".[dbo].[SC_UserGroup] SC");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.EmpID=SC.UserID AND P.CompID=SC.CompID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Communication] C ON P.IDNo=C.IDNo");
        sb.Append(" WHERE SC.CompID='" + UserInfo.getUserInfo().CompID + "' AND SC.GroupID='Adm-OT'");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            mail += dt.Rows[i]["EMail"].ToString() + ";";
        }
        return mail;
    }
    public void SaveMuliData()//多筆送簽
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        string strName = "";
        Dictionary<string, string> oAssTo = new Dictionary<string, string>();
        LoadCheckData();
        DataTable DT = (DataTable)ViewState["dt"];
        var isSuccess = false;
        var toUserData = new Dictionary<string, string>();
        var empData = new Dictionary<string, string>();
        var flowCode = "";
        var flowSN = "";
        var nextIsLastFlow = false;
        var meassge = "";
        try
        {
            if (gvMain.Visible && gvMain.Rows.Count > 0)
            {
                if (DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        isSuccess = FlowUtility.QueryFlowDataAndToUserData_First(lblCompID.Text, "", "", DT.Rows[i]["EmpID"].ToString(), UserInfo.getUserInfo().UserID, DT.Rows[i]["OTStartDate"].ToString(), "0",
                        out empData, out toUserData, out flowCode, out flowSN, out nextIsLastFlow, out meassge);
                        if (!isSuccess)
                        {
                            Util.MsgBox(meassge);
                            return;
                        }
                        if ("".Equals(toUserData["SignID"]))
                        {
                            Util.MsgBox("查無審核人員，故無法送簽。");
                            return;
                        }
                        //逐筆送簽
                        oAssTo.Clear();
                        string name = at.QueryColumn("NameN", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", " AND EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "'");
                        //需加部門
                        string organName = "";
                        //if (toUserData["SignLine"] == "1")
                        //{
                        //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + lblCompID.Text + "')");
                        //}
                        //else if (toUserData["SignLine"] == "2")
                        //{
                        //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] where OrganID='" + toUserData["SignFlowOrganID"] + "')");
                        //}
                        //else if (toUserData["SignLine"] == "3")
                        //{
                        //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + lblCompID.Text + "')");
                        //}
                        if (toUserData["SignLine"] == "1")
                        {
                            organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                        }
                        else if (toUserData["SignLine"] == "2")
                        {
                            organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                        }
                        else if (toUserData["SignLine"] == "3")
                        {
                            organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                        }
                        oAssTo.Add(toUserData["SignID"], organName + "-" + name);//oAssTo.Add(toUserData["SignID"], name);

                        string strStartTime = (DT.Rows[i]["OTStartTime"]).ToString().Substring(0, 2) + "：" + (DT.Rows[i]["OTStartTime"]).ToString().Substring(2, 2);
                        string strEndTime = (DT.Rows[i]["OTEndTime"]).ToString().Substring(0, 2) + "：" + (DT.Rows[i]["OTEndTime"]).ToString().Substring(2, 2);
                        string OTSeq = at.QueryColumn("OTSeq", " OverTimeAdvance", " AND OTCompID='" + DT.Rows[i]["OTCompID"].ToString() + "' AND OTEmpID='" + DT.Rows[i]["EmpID"] + "' AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                        string FlowKeyValue = "A," + DT.Rows[i]["OTCompID"] + "," + DT.Rows[i]["EmpID"] + "," + DT.Rows[i]["OTStartDate"] + "," + DT.Rows[i]["OTEndDate"] + "," + OTSeq;
                        strName = at.QueryColumn("NameN", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", " AND EmpID='" + DT.Rows[i]["EmpID"].ToString() + "' AND CompID='"+DT.Rows[i]["OTCompID"].ToString()+"'");
                        string FlowShowValue = DT.Rows[i]["EmpID"] + "," + strName + "," + DT.Rows[i]["OTStartDate"] + "," + strStartTime + "," + DT.Rows[i]["OTEndDate"] + "," + strEndTime;
                        if (FlowExpress.IsFlowInsVerify(flow.FlowID, FlowKeyValue.Split(','), FlowShowValue.Split(','), nextIsLastFlow ? "btnBeforeLast" : "btnBefore", oAssTo, ""))
                            {
                                //更新AssignToName(部門+員工姓名)
                                string a = FlowExpress.getFlowCaseID(flow.FlowID, FlowKeyValue);
                                if (!string.IsNullOrEmpty(a))
                                {
                                    CommandHelper sb2 = db.CreateCommandHelper();
                                    //加進HROverTimeLog
                                    FlowUtility.InsertHROverTimeLogCommand(a, "1", a + ".00001",
                                       "A", empData["EmpID"], empData["OrganID"], empData["FlowOrganID"], UserInfo.getUserInfo().UserID,
                                       flowCode, flowSN, "1", toUserData["SignLine"],
                                       toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "0",
                                       false, ref sb2, 1, "1");

                                    //加進MailLog
                                    string Subject_1 = "";
                                    string Content_1 = "";
                                    string mail = "";
                                    CommandHelper sbselect = db.CreateCommandHelper();
                                    sbselect.Append("SELECT isnull(C.EMail,'') AS EMail FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P");
                                    sbselect.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Communication] C ON P.IDNo=C.IDNo");
                                    sbselect.Append(" WHERE P.CompID='" + toUserData["SignIDComp"] + "' AND P.EmpID='" + toUserData["SignID"] + "'");
                                    DataTable dtSelect = db.ExecuteDataSet(sbselect.BuildCommand()).Tables[0];
                                    if (dtSelect.Rows.Count > 0)
                                    {
                                        if (string.IsNullOrEmpty(dtSelect.Rows[0]["EMail"].ToString()))
                                        {
                                            mail = HREmail();
                                            Subject_1 = "系統查無通知者E-mail";
                                            Content_1 = "OverTimeExpedite||BM@QuitMailContent1||系統查無通知者<br/>" + toUserData["SignID"] + "-" + name;
                                        }
                                        else
                                        {
                                            mail = dtSelect.Rows[0]["EMail"].ToString();
                                            Subject_1 = "加班單流程待辦案件通知";
                                            Content_1 = "OverTimeTodoList||BM@QuitMailContent1||" + name + "||BM@QuitMailContent2||" + DT.Rows[i]["OTFormNO"] + "||BM@QuitMailContent3||" + DT.Rows[i]["EmpID"] + "||BM@QuitMailContent4||" + strName + "||BM@QuitMailContent5||" + DT.Rows[i]["OTStartDate"] + "~" + DT.Rows[i]["OTEndDate"] + "||BM@QuitMailContent6||" + strStartTime + "||BM@QuitMailContent7||" + strEndTime + "||BM@QuitMailContent8||" + DT.Rows[i]["OTTotalTime"];
                                        }
                                    }
                                    Aattendant.InsertMailLogCommand("人力資源處", toUserData["SignIDComp"], toUserData["SignID"], mail, "", "", Subject_1, Content_1, false, ref sb2);

                                    try
                                    {
                                        db.ExecuteNonQuery(sb2.BuildCommand());
                                    }
                                    catch (Exception ex)
                                    {
                                        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                                        tx.Rollback();//資料更新失敗
                                        Util.MsgBox("送簽失敗(6)"); //提案送審失敗
                                        return;
                                    }
                                }

                                if (DT.Rows[i]["OTStartDate"].ToString() != DT.Rows[i]["OTEndDate"].ToString())//當跨日的時候須回寫FlowCaseID到迄日
                                {
                                    CommandHelper sb1 = db.CreateCommandHelper();
                                    sb1.AppendStatement("UPDATE OverTimeAdvance SET FlowCaseID=A.FlowCaseID");
                                    sb1.Append(" FROM (SELECT FlowCaseID FROM OverTimeAdvance");
                                    sb1.Append(" WHERE OTCompID='" + DT.Rows[i]["OTCompID"] + "'");
                                    sb1.Append(" AND OTEmpID='" + DT.Rows[i]["EmpID"] + "'");
                                    sb1.Append(" AND OTStartDate='" + DT.Rows[i]["OTStartDate"] + "'");
                                    sb1.Append(" AND OTEndDate='" + DT.Rows[i]["OTStartDate"] + "'");
                                    sb1.Append(" AND OTStartTime='" + DT.Rows[i]["OTStartTime"] + "'");
                                    sb1.Append(" AND OTEndTime='2359'");
                                    sb1.Append(" AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                                    sb1.Append(" ) A ");
                                    sb1.Append(" WHERE OTCompID='" + DT.Rows[i]["OTCompID"] + "'");
                                    sb1.Append(" AND OTEmpID='" + DT.Rows[i]["EmpID"] + "'");
                                    sb1.Append(" AND OTStartDate='" + DT.Rows[i]["OTEndDate"] + "'");
                                    sb1.Append(" AND OTEndDate='" + DT.Rows[i]["OTEndDate"] + "'");
                                    sb1.Append(" AND OTStartTime='0000'");
                                    sb1.Append(" AND OTEndTime='" + DT.Rows[i]["OTEndTime"] + "'");
                                    sb1.Append(" AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                                    try
                                    {
                                        db.ExecuteNonQuery(sb1.BuildCommand());
                                    }
                                    catch (Exception ex)
                                    {
                                        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                                        tx.Rollback();//資料更新失敗
                                        Util.MsgBox("送簽失敗(1)");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Util.MsgBox("送簽失敗(2)");
                                tx.Rollback();//資料更新失敗
                                return;
                            }

                        if (DT.Rows[i]["OTStartDate"].ToString() == DT.Rows[i]["OTEndDate"].ToString())
                        {
                            sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='2'");
                            sb.Append(" WHERE OTCompID='" + DT.Rows[i]["OTCompID"] + "'");
                            sb.Append(" AND OTEmpID='" + DT.Rows[i]["EmpID"] + "'");
                            sb.Append(" AND OTStartDate='" + DT.Rows[i]["OTStartDate"] + "'");
                            sb.Append(" AND OTEndDate='" + DT.Rows[i]["OTEndDate"] + "'");
                            sb.Append(" AND OTStartTime='" + DT.Rows[i]["OTStartTime"] + "'");
                            sb.Append(" AND OTEndTime='" + DT.Rows[i]["OTEndTime"] + "'");
                            sb.Append(" AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                        }
                        else
                        {
                            string crossDayArray = DT.Rows[i]["OTStartDate"] + "," + DT.Rows[i]["OTEndDate"];
                            for (int j = 0; j < crossDayArray.Split(',').Length; j++)
                            {
                                if (crossDayArray.Split(',')[j] == DT.Rows[i]["OTStartDate"].ToString())
                                {
                                    sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='2'");
                                    sb.Append(" WHERE OTCompID='" + DT.Rows[i]["OTCompID"] + "'");
                                    sb.Append(" AND OTEmpID='" + DT.Rows[i]["EmpID"] + "'");
                                    sb.Append(" AND OTStartDate='" + DT.Rows[i]["OTStartDate"] + "'");
                                    sb.Append(" AND OTEndDate='" + DT.Rows[i]["OTStartDate"] + "'");
                                    sb.Append(" AND OTStartTime='" + DT.Rows[i]["OTStartTime"] + "'");
                                    sb.Append(" AND OTEndTime='2359'");
                                    sb.Append(" AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                                }
                                else
                                {
                                    sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='2'");
                                    sb.Append(" WHERE OTCompID='" + DT.Rows[i]["OTCompID"] + "'");
                                    sb.Append(" AND OTEmpID='" + DT.Rows[i]["EmpID"] + "'");
                                    sb.Append(" AND OTStartDate='" + DT.Rows[i]["OTEndDate"] + "'");
                                    sb.Append(" AND OTEndDate='" + DT.Rows[i]["OTEndDate"] + "'");
                                    sb.Append(" AND OTStartTime='0000'");
                                    sb.Append(" AND OTEndTime='" + DT.Rows[i]["OTEndTime"] + "'");
                                    sb.Append(" AND OTTxnID='" + DT.Rows[i]["OTTxnID"] + "'");
                                }
                            }
                        }
                    }
                }
            }
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                ucLightBox.Hide();
                Util.MsgBox("送簽成功");
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                tx.Rollback();//資料更新失敗
                Util.MsgBox("送簽失敗(3)");
                return;
            }
            RefreshGrid();
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            Util.MsgBox("送簽失敗(4)");
        }
        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }
    
    //刪除按鈕
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LoadCheckData();
        if (gvMain.Visible == false && gvMain.Rows.Count <= 0)
        {
            Util.MsgBox("尚未有資料可以刪除");
            return;
        }
        else if (gvMain.Visible == true && gvMain.Rows.Count == 0)
        {
            Util.MsgBox("尚未有資料可以刪除");
            return;
        }
        else if (ViewState["dt"] != null)
        {
            DataTable dt = (DataTable)ViewState["dt"];
            if (dt.Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoDelete();", true);
            }
            else
            {
                Util.MsgBox("尚未勾選資料");
                return;
            }
        }
    }
    protected void btnDeleteInvisible_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        LoadCheckData();
        try
        {
            if (gvMain.Visible == true && gvMain.Rows.Count > 0)
            {
                DataTable dt = (DataTable)ViewState["dt"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["OTStartDate"].ToString() == dt.Rows[i]["OTEndDate"].ToString())
                        {
                            sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='5'");
                            sb.Append(" WHERE OTCompID='" + dt.Rows[i]["OTCompID"] + "'");
                            sb.Append(" AND OTEmpID='" + dt.Rows[i]["EmpID"] + "'");
                            sb.Append(" AND OTStartDate='" + dt.Rows[i]["OTStartDate"] + "'");
                            sb.Append(" AND OTEndDate='" + dt.Rows[i]["OTEndDate"] + "'");
                            sb.Append(" AND OTStartTime='" + dt.Rows[i]["OTStartTime"] + "'");
                            sb.Append(" AND OTEndTime='" + dt.Rows[i]["OTEndTime"] + "'");
                            sb.Append(" AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "'");
                        }
                        else
                        {
                            string crossDayArray = dt.Rows[i]["OTStartDate"] + "," + dt.Rows[i]["OTEndDate"];
                            for (int j = 0; j < crossDayArray.Split(',').Length; j++)
                            {
                                if (crossDayArray.Split(',')[j] == dt.Rows[i]["OTStartDate"].ToString())
                                {
                                    sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='5'");
                                    sb.Append(" WHERE OTCompID='" + dt.Rows[i]["OTCompID"] + "'");
                                    sb.Append(" AND OTEmpID='" + dt.Rows[i]["EmpID"] + "'");
                                    sb.Append(" AND OTStartDate='" + dt.Rows[i]["OTStartDate"] + "'");
                                    sb.Append(" AND OTEndDate='" + dt.Rows[i]["OTStartDate"] + "'");
                                    sb.Append(" AND OTStartTime='" + dt.Rows[i]["OTStartTime"] + "'");
                                    sb.Append(" AND OTEndTime='2359'");
                                    sb.Append(" AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "'");
                                }
                                else
                                {
                                    sb.AppendStatement("UPDATE OverTimeAdvance SET OTStatus='5'");
                                    sb.Append(" WHERE OTCompID='" + dt.Rows[i]["OTCompID"] + "'");
                                    sb.Append(" AND OTEmpID='" + dt.Rows[i]["EmpID"] + "'");
                                    sb.Append(" AND OTStartDate='" + dt.Rows[i]["OTEndDate"] + "'");
                                    sb.Append(" AND OTEndDate='" + dt.Rows[i]["OTEndDate"] + "'");
                                    sb.Append(" AND OTStartTime='0000'");
                                    sb.Append(" AND OTEndTime='" + dt.Rows[i]["OTEndTime"] + "'");
                                    sb.Append(" AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "'");
                                }
                            }
                        }
                    }
                }
                try
                {
                    db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    tx.Commit();
                    Util.MsgBox("刪除成功！");

                }
                catch (Exception ex)
                {
                    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                    tx.Rollback();//資料更新失敗
                    Util.MsgBox("刪除失敗(1)！");
                }
                RefreshGrid();
            }
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            Util.MsgBox("刪除失敗(2)！");
        }

        finally
        {
            cn.Close();
            cn.Dispose();
            tx.Dispose();
        }
    }
    
    //清除按鈕
    protected void btnClear_Click(object sender, EventArgs e) 
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        try
        {
            ucModalPopup1.ucPanelID = pnlVerify.ID;
            ucModalPopup1.Hide();

            lblCompID.Text = UserInfo.getUserInfo().CompID;
            txtOTCompName.Text = UserInfo.getUserInfo().CompName;
            lblDeptID.Text = UserInfo.getUserInfo().DeptID;
            txtDeptName.Text = UserInfo.getUserInfo().DeptName;
            lblOrganID.Text = UserInfo.getUserInfo().OrganID;
            txtOrganName.Text = UserInfo.getUserInfo().OrganName;
            txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
            txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
            CompareValidator StartDateCompare = ucDateStart.FindControl("CompareUnusualTime1") as CompareValidator;
            RegularExpressionValidator StartDateUnusual = ucDateStart.FindControl("regUnusualTime") as RegularExpressionValidator;
            StartDateCompare.Style.Add("display", "none");
            StartDateUnusual.Style.Add("display", "none");
            CompareValidator EndDateCompare = ucDateEnd.FindControl("CompareUnusualTime1") as CompareValidator;
            RegularExpressionValidator EndDateUnusual = ucDateEnd.FindControl("regUnusualTime") as RegularExpressionValidator;
            EndDateCompare.Style.Add("display", "none");
            EndDateUnusual.Style.Add("display", "none");
            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            ucDateEnd.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            chkMealFlag.Checked = true;
            txtMealTime.Enabled = true;
            txtOTReasonMemo.ucTextData = "";
            ddlOTTypeID.SelectedValue = "0";
            chkCopyAtt.Visible = false;
            rbtSingleEmpID.Checked = true;
            rbtMultiEmpID.Checked = false;
            //附件也需要清掉
            string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' ;", _AttachID);
            try
            {
                if (db.ExecuteNonQuery(CommandType.Text, strDelSQL) >= 0)
                {
                    Util.IsAttachInfoLog(_overtimeDBName, _AttachID, 1, "Delete");
                }
            }
            catch (Exception ex)
            {
                Util.MsgBox("清除失敗(2)！");
            }
            GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
            getAttachName();
            initalData();
        }
        catch (Exception ex)
        {
            Util.MsgBox("清除失敗(1)！");
        }
    }
    //返回按鈕
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("OvertimePreOrder.aspx");
    }
    //人員選取
    protected void imgLaunch_Click(object sender, ImageClickEventArgs e)
    {
        ddlOrg_SelectedIndexChanged(null, null);
        ucModalPopup1.Reset();
        ucModalPopup1.ucPanelID = pnlVerify.ID;
        ucModalPopup1.ucPopupHeight = 230;
        ucModalPopup1.ucPopupWidth = 300;
        ucModalPopup1.Show();
    }
    //轉換行政功能組織
    protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        //判斷顯示行政的下拉選單或是功能的下拉選單
        if (ddlOrg.SelectedValue == "1")
        {
            ucCascadingDropDown1.Visible = true;
            ucCascadingDropDown2.Visible = false;
        }
        DataTable dt = at.QueryData("*", "OverTimeSPUser", " AND EmpID='" + UserInfo.getUserInfo().UserID + "'");
        if (dt.Rows.Count > 0)
        {
            if (ddlOrg.SelectedValue == "2")
            {
                ucCascadingDropDown1.Visible = false;
                ucCascadingDropDown2.Visible = true;
                ucCascadingDropDown2.ucDefaultSelectedValue01 = UserInfo.getUserInfo().CompID;
                ucCascadingDropDown2.ucCategory01 = "CompID";
                ucCascadingDropDown2.ucCategory02 = "OrgFlowDeptID";
                ucCascadingDropDown2.ucCategory03 = "OrgFlowOrganID";
                ucCascadingDropDown2.ucCategory04 = "OrgFlowUserID";

                ucCascadingDropDown2.ucDropDownListEnabled04 = true;
                ucCascadingDropDown2.ucIsReadOnly01 = true;
                ucCascadingDropDown2.ucIsVerticalLayout = true;
                ucCascadingDropDown2.ucDropDownListReadOnlyCssClass = "clsDropDownListReadOnly";
                ucCascadingDropDown2.ucDropDownListCssClass = "clsDropDownList";//人員的CSS(白底黑字)
            }
        }
        else
        {
            ddlOrg.Enabled = false;
            ucCascadingDropDown1.ucIsReadOnly01 = true;
            ucCascadingDropDown1.ucIsReadOnly02 = true;
            ucCascadingDropDown1.ucIsReadOnly03 = true;
        }
        ucCascadingDropDown1.Refresh();
        ucCascadingDropDown2.Refresh();
        ucModalPopup1.Reset();
        ucModalPopup1.ucPanelID = pnlVerify.ID;
        ucModalPopup1.ucPopupHeight = 230;
        ucModalPopup1.ucPopupWidth = 300;
        ucModalPopup1.Show();
    }
    protected void btnOK_Click(object sender, EventArgs e) //人員選取確認
    {
        if (ddlOrg.SelectedValue == "1")
        {
            if (ucCascadingDropDown1.ucDefaultSelectedValue04 == "")
            {
                Util.MsgBox("尚未選取人");
                return;
            }
            //將值帶回到畫面上
            lblCompID.Text = ucCascadingDropDown1.ucDefaultSelectedValue01;
            txtOTCompName.Text = ucCascadingDropDown1.ucSelectedText01.ToString();
            lblDeptID.Text = ucCascadingDropDown1.ucDefaultSelectedValue02;
            txtDeptName.Text = ucCascadingDropDown1.ucSelectedText02.ToString().TrimStart();
            lblOrganID.Text = ucCascadingDropDown1.ucDefaultSelectedValue03;
            txtOrganName.Text = ucCascadingDropDown1.ucSelectedText03.ToString().TrimStart();
            txtOTEmpID.Text = ucCascadingDropDown1.ucDefaultSelectedValue04;
            txtOTEmpName.Text = ucCascadingDropDown1.ucSelectedText04;
        }
        else
        {
            if (ucCascadingDropDown2.ucDefaultSelectedValue04 == "")
            {
                Util.MsgBox("尚未選取人");
                return;
            }
            //將值帶回到畫面上
            lblCompID.Text = ucCascadingDropDown2.ucDefaultSelectedValue01;
            txtOTCompName.Text = ucCascadingDropDown2.ucSelectedText01.ToString();
            lblDeptID.Text = ucCascadingDropDown2.ucDefaultSelectedValue02;
            txtDeptName.Text = ucCascadingDropDown2.ucSelectedText02.ToString().TrimStart();
            lblOrganID.Text = ucCascadingDropDown2.ucDefaultSelectedValue03;
            txtOrganName.Text = ucCascadingDropDown2.ucSelectedText03.ToString().TrimStart();
            txtOTEmpID.Text = ucCascadingDropDown2.ucDefaultSelectedValue04;
            txtOTEmpName.Text = ucCascadingDropDown2.ucSelectedText04;
        }
        ucCascadingDropDown1.ucDefaultSelectedValue04 = "";
        ucCascadingDropDown2.ucDefaultSelectedValue04 = "";
        ucCascadingDropDown1.Refresh();
        ucCascadingDropDown2.Refresh();

        //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
        GetPersonal(lblCompID.Text, txtOTEmpID.Text);
        string a = "";
        //將用餐時數先記下來
        if (chkMealFlag.Checked)
        {
            a = txtMealTime.Text;
        }
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "")
        {
            StartTime_SelectedIndexChanged(null, null);
        }
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "" && txtOTEmpID.Text != "")
        {
            EndTime_SelectedIndexChanged(null, null);
        }
        if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate) && txtOTEmpID.Text != "")
        {
            SignData();
        }
        if (!string.IsNullOrEmpty(a))
        {
            txtMealTime.Text = a;
            chkMealFlag.Checked = true;
            txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
            txtTotalDescription.Visible = true;
        }
    }
    protected void btnNo_Click(object sender, EventArgs e) //人員選取取消
    {
        txtOTEmpID.Text = "";
        txtOTEmpName.Text = "";
        ucCascadingDropDown1.ucDefaultSelectedValue04 = "";
        ucCascadingDropDown2.ucDefaultSelectedValue04 = "";
    }
    protected void txtOTEmpID_TextChanged(object sender, EventArgs e)//員工姓名
    {
        if (!string.IsNullOrEmpty(txtOTEmpID.Text) && (txtOTEmpID.Text.Length==6))
        {
            DataTable dtPersonal = at.QueryData("*", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", "  AND CompID = '" + UserInfo.getUserInfo().CompID + "'");
            DataTable dtEmpflow = at.QueryData("*", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[EmpFlow]", " AND CompID = '" + UserInfo.getUserInfo().CompID + "'");
            DataTable dtSPUser = at.QueryData("*", "OverTimeSPUser", " AND EmpID='" + UserInfo.getUserInfo().UserID + "'");//特殊人員
            if (dtSPUser.Rows.Count > 0)
            {
                string q = "," + "" + "," + UserInfo.getUserInfo().OrganID + "," + dtSPUser.Rows[0]["OrgList"].ToString() + "," + dtSPUser.Rows[0]["OrgFlowList"].ToString() + ",";
                var EmpID = (from data in dtPersonal.AsEnumerable() where data.Field<string>("EmpID") == txtOTEmpID.Text select data).FirstOrDefault();
                if(EmpID==null)
                {
                    Util.MsgBox("未被授權申請");
                    txtOTEmpName.Text = "";
                    txtOTEmpID.Text = "";
                    return;
                }
                string a = Convert.ToString(EmpID["OrganID"]);
                var EmpFlowID = (from data in dtEmpflow.AsEnumerable() where data.Field<string>("EmpID") == txtOTEmpID.Text select data).FirstOrDefault();
                if (EmpFlowID == null)
                {
                    Util.MsgBox("未被授權申請");
                    txtOTEmpName.Text = "";
                    txtOTEmpID.Text = "";
                    return;
                }
                string b = Convert.ToString(EmpFlowID["OrganID"]);

                if (q.IndexOf("," + a.Trim() + ",") > 0)
                {
                    lblOrganID.Text = a;
                    DataTable dtOrg = at.QueryData("*", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' And InValidFlag = '0' And VirtualFlag = '0'");
                    var Org = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == lblOrganID.Text select data).FirstOrDefault();
                    txtOrganName.Text = Convert.ToString(Org["OrganName"]);
                    lblDeptID.Text = Convert.ToString(EmpID["DeptID"]);
                    var Dept = (from data in dtOrg.AsEnumerable() where data.Field<string>("OrganID") == lblDeptID.Text select data).FirstOrDefault();
                    txtDeptName.Text = Convert.ToString(Dept["OrganName"]);
                    lblCompID.Text = UserInfo.getUserInfo().CompID;
                }
                else if (q.IndexOf("," + b.Trim() + ",") > 0)
                {
                    lblOrganID.Text = b;
                    DataTable dtOrgFlow = at.QueryData("*", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND CompID = '" + UserInfo.getUserInfo().CompID + "'");
                    var Org = (from data in dtOrgFlow.AsEnumerable() where data.Field<string>("OrganID") == lblOrganID.Text select data).FirstOrDefault();
                    txtOrganName.Text = Convert.ToString(Org["OrganName"]);
                    lblDeptID.Text = Convert.ToString(Org["DeptID"]);
                    var Dep = (from data in dtOrgFlow.AsEnumerable() where data.Field<string>("OrganID") == lblOrganID.Text select data).FirstOrDefault();
                    txtDeptName.Text = Convert.ToString(Dep["OrganName"]);
                    lblCompID.Text = UserInfo.getUserInfo().CompID;
                }
                else
                {
                    Util.MsgBox("未被授權申請");
                    txtOTEmpName.Text = "";
                    txtOTEmpID.Text = "";
                    return;
                }
            }
            else
            {
                DataTable dtOrgan = at.QueryData("P.OrganID,O.OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] O  LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON O.OrganID=P.OrganID", " AND P.CompID = '" + UserInfo.getUserInfo().CompID + "' AND P.OrganID = '" + UserInfo.getUserInfo().OrganID + "' AND P.EmpID='"+txtOTEmpID.Text+"'");
                if (dtOrgan.Rows.Count > 0)
                {
                    lblCompID.Text = UserInfo.getUserInfo().CompID;
                    txtOTCompName.Text = UserInfo.getUserInfo().CompName;
                    lblOrganID.Text = dtOrgan.Rows[0]["OrganID"].ToString();
                    txtOrganName.Text = dtOrgan.Rows[0]["OrganName"].ToString();
                    lblDeptID.Text = UserInfo.getUserInfo().DeptID;
                    txtDeptName.Text = UserInfo.getUserInfo().DeptName;
                }
                else
                {
                    Util.MsgBox("未被授權申請");
                    txtOTEmpName.Text = "";
                    txtOTEmpID.Text = "";
                    return;
                }
            }
            DataTable dt = at.QueryData("NameN, Sex, RankID,WorkSiteID", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] ", " AND EmpID='" + txtOTEmpID.Text + "' AND CompID = '" + lblCompID.Text + "'");
            if (dt.Rows.Count > 0)
            {
                txtOTEmpName.Text = dt.Rows[0]["NameN"].ToString();
                _Sex = dt.Rows[0]["Sex"].ToString();
                _rankID = Aattendant.GetRankIDFormMapping(lblCompID.Text, dt.Rows[0]["RankID"].ToString()); //dt.Rows[0]["RankID"].ToString();
                _WorkSiteID = dt.Rows[0]["WorkSiteID"].ToString();
            }
        }
        else
        {
            Util.MsgBox("請輸入員工編號");
            txtOTEmpName.Text = "";
            return;
        }
        lblStartSex.Visible = false;
        lblEndSex.Visible = false;
        //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
        ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
        ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "")
        {
            StartTime_SelectedIndexChanged(null, null);
        }
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "" && txtOTEmpID.Text != "")
        {
            EndTime_SelectedIndexChanged(null, null);
        }
        if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate) && txtOTEmpID.Text != "")
        {
            SignData();
        }
    }
    //多筆加班人的提示訊息
    protected void imgQuestion_Click(object sender, ImageClickEventArgs e) 
    {
        Util.MsgBox("操作說明：<br/><div style =text-align:left;>1.選擇多筆加班人且需上傳附檔時，請於第一筆資料建檔時上傳附檔<br/>2.若為相同檔案上傳，請勾選<input type=checkbox Checked=true disabled=false/>同上筆附檔</div>");
    }
    protected void ucModalPopAttach_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        getAttachName();
        if (gvMain.Rows.Count > 0)
        {
            gvMain.Visible = true;
            RefreshGrid();
        }
        else
        {
            gvMain.Visible = false;
        }
        ucCascadingDropDown1.ucDefaultSelectedValue05 = "";
    }
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvMain.Rows.Count >= 1)
            {
                e.Row.Cells[2].Style.Add("word-break", "break-all");
            }
        }
    }
    //單筆加班人轉換到多筆加班人須清除資料
    protected void rbtSingleEmpID_CheckedChanged(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        ucModalPopup1.ucPanelID = pnlVerify.ID;
        ucModalPopup1.Hide();
        if (rbtSingleEmpID.Checked)
        {
            _FormNoRecord = "";
            _FormNo = "";
            RefreshGrid();
            if (gvMain.Rows.Count <= 0)
            {
                gvMain.Visible = false;
            }
            //從單筆到多筆資料須清除
            lblCompID.Text = UserInfo.getUserInfo().CompID;
            txtOTCompName.Text = UserInfo.getUserInfo().CompName;
            lblDeptID.Text = UserInfo.getUserInfo().DeptID;
            txtDeptName.Text = UserInfo.getUserInfo().DeptName;
            lblOrganID.Text = UserInfo.getUserInfo().OrganID;
            txtOrganName.Text = UserInfo.getUserInfo().OrganName;
            txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
            txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            ucDateEnd.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            chkMealFlag.Checked = true;
            txtMealTime.Enabled = true;
            txtOTReasonMemo.ucTextData = "";
            ddlOTTypeID.SelectedValue = "0";
            rbtSingleEmpID.Checked = true;
            rbtMultiEmpID.Checked = false;
            chkCopyAtt.Visible = false;
            //附件也需要清掉
            string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' ;", _AttachID);
            try
            {
                if (db.ExecuteNonQuery(CommandType.Text, strDelSQL) >= 0)
                {
                    Util.IsAttachInfoLog(_overtimeDBName, _AttachID, 1, "Delete");
                }
            }
            catch (Exception ex)
            {
                Util.MsgBox("清除失敗(2)！");
            }
            GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
            getAttachName();
            initalData();
        }
    }
    //多筆加班人轉換到單筆加班人須清除資料
    protected void rbtMultiEmpID_CheckedChanged(object sender, EventArgs e) 
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();

        if (rbtMultiEmpID.Checked)
        {
            _FormNoRecord = "";
            _FormNo = "";
            RefreshGrid();
            if (gvMain.Rows.Count > 0 && gvMain.Visible == true )
            {
                chkCopyAtt.Enabled = true;
                chkCopyAtt.Visible = true;
            }
            else
            {
                chkCopyAtt.Visible = false;
                chkCopyAtt.Enabled = false;
                gvMain.Visible = false;
            }

            //從多筆到單筆資料須清除
            lblCompID.Text = UserInfo.getUserInfo().CompID;
            txtOTCompName.Text = UserInfo.getUserInfo().CompName;
            lblDeptID.Text = UserInfo.getUserInfo().DeptID;
            txtDeptName.Text = UserInfo.getUserInfo().DeptName;
            lblOrganID.Text = UserInfo.getUserInfo().OrganID;
            txtOrganName.Text = UserInfo.getUserInfo().OrganName;
            txtOTEmpID.Text = UserInfo.getUserInfo().UserID;
            txtOTEmpName.Text = UserInfo.getUserInfo().UserName;
            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            ucDateEnd.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
            chkMealFlag.Checked = true;
            txtMealTime.Enabled = true;
            txtOTReasonMemo.ucTextData = "";
            ddlOTTypeID.SelectedValue = "0";
            //附件也需要清掉
            string strDelSQL = string.Format("Update AttachInfo Set FileSize = -1 ,FileBody = null Where AttachID = '{0}' ;", _AttachID);
            try
            {
                if (db.ExecuteNonQuery(CommandType.Text, strDelSQL) >= 0)
                {
                    Util.IsAttachInfoLog(_overtimeDBName, _AttachID, 1, "Delete");
                }
            }
            catch (Exception ex)
            {
                Util.MsgBox("清除失敗(2)！");
            }
            GetPersonal(UserInfo.getUserInfo().CompID, txtOTEmpID.Text);
            getAttachName();
            initalData();
            getAttachName();
        }
    }

    //清除資料或回到原始資料須重新找登入者的基本資料
    protected void GetPersonal(string strComp,string strEmp)
    {
        DataTable dt = at.QueryData("Sex, RankID,EmpDate,WorkSiteID", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] ", " AND EmpID='" + strEmp + "' AND CompID = '" + strComp + "'");
        if (dt.Rows.Count > 0)
        {
            _Sex = dt.Rows[0]["Sex"].ToString();
            _rankID = Aattendant.GetRankIDFormMapping(strComp, dt.Rows[0]["RankID"].ToString());
            _EmpDate = dt.Rows[0]["EmpDate"].ToString();
            _WorkSiteID = dt.Rows[0]["WorkSiteID"].ToString();

            if (_dtPara == null)
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
            else
            {
                ddlSalaryOrAdjust.SelectedValue = _dtPara.Rows[0]["SalaryOrAjust"].ToString();
            }
            if (!string.IsNullOrEmpty(ucDateStart.ucSelectedDate) && !string.IsNullOrEmpty(ucDateEnd.ucSelectedDate) && !string.IsNullOrEmpty(_rankID))
            {
                //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
                if (ViewState["SalaryOrAdjust"]!=null && !string.IsNullOrEmpty(ViewState["SalaryOrAdjust"].ToString()))
                {
                    ddlSalaryOrAdjust.SelectedValue = ViewState["SalaryOrAdjust"].ToString();
                }
                ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
            }  
        }
    }
}