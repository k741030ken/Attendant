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
using Newtonsoft.Json;

public partial class OnBiz_OnBizReqAddesView : BasePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _OnBizEmpModel
    /// 公出人基本資料
    /// </summary>
    private OnBizPublicOutModel _OnBizEmpInfoModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["OnBizEmpInfoModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizPublicOutModel>(ViewState["OnBizEmpInfoModel"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            ViewState["OnBizEmpInfoModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _OnBizDeputyInfoModel
    /// 職務代理人基本資料
    /// </summary>
    private OnBizPublicOutModel _OnBizDeputyInfoModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["OnBizDeputyInfoModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizPublicOutModel>(ViewState["OnBizDeputyInfoModel"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            ViewState["OnBizDeputyInfoModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _SessionEmpInfoModel
    /// </summary>
    private Dictionary<string, string> _SessionEmpInfoModel
    {
        get
        {
            if (Session["btnEmpID_Data"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session["btnEmpID_Data"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)Session["btnEmpID_Data"];
        }
        set
        {
            Session["btnEmpID_Data"] = value;
        }
    }

    /// <summary>
    /// _SessionDeputyInfoModel
    /// </summary>
    private Dictionary<string, string> _SessionDeputyInfoModel
    {
        get
        {
            if (Session["btnDeputyID_Data"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session["btnDeputyID_Data"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)Session["btnDeputyID_Data"];
        }
        set
        {
            Session["btnDeputyID_Data"] = value;
        }
    }

    /// <summary>
    /// _maxSeq
    /// </summary>
    private string _maxSeq
    {
        get
        {
            try
            {
                if (ViewState["MaxSeq"] != null) //ViewState當頁暫存使用
                {
                    return ViewState["MaxSeq"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            ViewState["MaxSeq"] = value;
        }
    }

    #endregion

    #region "2. 功能鍵處理邏輯"
    /// <summary>
    /// btnSave_Click
    /// 暫存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        beforeSubmit("Save");
    }
    /// <summary>
    /// btnSend_Click
    /// 送簽
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        beforeSubmit("Send");
    }
    /// <summary>
    /// btnBack_Click
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnBizRegInquireView.aspx");
    }
    /// <summary>
    /// btnActionX_Click
    /// 清除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActionX_Click(object sender, EventArgs e)
    {
        DoClear();
    }

    #endregion

    #region "3. Override Method"

    #endregion

    #region "4. Page_Load"
    /// <summary>
    /// 起始
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //載入資料
            LoadData();
        }
        else
        {
            SetEmpInfoFromSession();
        }
    }
    #endregion

    #region "5. 畫面事件"
    /// <summary>
    /// 公出人員
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {
        QueryEmpInfo(txtEmpID.Text, "EmpID");
    }

    /// <summary>
    /// 職務代理人員
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtDeputyID_TextChanged(object sender, EventArgs e)
    {
        if (txtEmpID.Text.Equals(txtDeputyID.Text))
        {
            txtDeputyID.Text = "";
            lblDeputyID.Text = "";
            txtDeputyID.Focus();
        }
        else
        {
            QueryEmpInfo(txtDeputyID.Text, "DeputyID");
        }
    }

    /// <summary>
    /// Outter_CheckedChanged
    /// 外部選項RadioButton
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Outter_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton outCheck = sender as RadioButton;
        OutInSwitch(outCheck);
    }

    /// <summary>
    /// Inner_CheckedChanged
    /// 內部選項RadioButton
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Inner_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton inCheck = sender as RadioButton;
        OutInSwitch(inCheck);
    }


    #endregion

    #region "6. 畫面檢核與確認"
    /// <summary>
    /// 公出員工編號檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool EmpIDValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        if (String.IsNullOrEmpty(lblEmpID.Text))
        {
            errorMsg = "請輸入公出人員工編號";
        }
        if (!"".Equals(errorMsg)) 
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 連絡電話一檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool Tel_1Validation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        var tel_1 = StringIIF(txtTel_1.Text);

        if (tel_1 == "")
        {
            errorMsg = "請輸入連絡電話一";
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 公出日期檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool VisitBeginDateValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        var VisitBeginDate = StringIIF(txtVisitBeginDate.ucSelectedDate);
        var NowDate = DateTime.Now.ToString("yyyy/MM/dd");

        if (VisitBeginDate == "")
        {
            errorMsg = "請輸入公出日期";
        }
        else if (VisitBeginDate != "")
        {
            var sDate = Int32.Parse(VisitBeginDate.Replace("/", ""));
            var eDate = Int32.Parse(NowDate.Replace("/", ""));
            if (sDate < eDate)
            {
                errorMsg = "公出日期不得小於今日";
            }
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 公出時間檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool TimeValidation(out string errorMsg,out string tipMsg)
    {
        bool result = true;
        errorMsg = "";
        tipMsg = "";
        var BeginTimeA = StringIIF(ddlBeginTimeA.Text);
        var BeginTimeB = StringIIF(ddlBeginTimeB.Text);
        var EndTimeA = StringIIF(ddlEndTimeA.Text);
        var EndTimeB = StringIIF(ddlEndTimeB.Text);

        if (BeginTimeA == "" || BeginTimeB == "" || EndTimeA == "" || EndTimeB == "")
        {
            errorMsg = "請輸入公出時間";
        }
        else
        {
            var BTimeA = BeginTimeA.StartsWith("0") ? Int32.Parse(BeginTimeA.Substring(1)) : Int32.Parse(BeginTimeA);
            var BTimeB = BeginTimeB.StartsWith("0") ? Int32.Parse(BeginTimeB.Substring(1)) : Int32.Parse(BeginTimeB);
            var ETimeA = EndTimeA.StartsWith("0") ? Int32.Parse(EndTimeA.Substring(1)) : Int32.Parse(EndTimeA);
            var ETimeB = EndTimeB.StartsWith("0") ? Int32.Parse(EndTimeB.Substring(1)) : Int32.Parse(EndTimeB);
            
            if (BTimeA > 23 || ETimeA > 23)
            {
                errorMsg = "時間格式輸入錯誤(24小時制)";
            }
            else
            {
                if (BTimeA > ETimeA)
                {
                    errorMsg = "開始時間不可以晚於結束時間";
                }
                else if ((BTimeA == ETimeA))
                {
                    if (BTimeB > 59 || ETimeB > 59)
                    {
                        errorMsg = "時間格式輸入錯誤(60分鐘制)";
                    }
                    else if (BTimeB > ETimeB)
                    {
                        errorMsg = "開始時間不可以晚於結束時間";
                    }
                }
                else if (BTimeA < ETimeA)
                {
                    string startTime = BTimeA + ":" + BTimeB;
                    string endTime = ETimeA + ":" + ETimeB;
                    int totalMin = OnBizReq.GetTimeDiff(startTime, endTime, "Minute");
                    if (totalMin > 720)
                    {
                        errorMsg = "公出時間不可以超過12小時";
                    }
                }
            }
        }

        PunchModel report = new PunchModel();
        bool hasReport = getPunchReport(out report);
        if (hasReport)
        {
            int chkEndTime = Int32.Parse(report.EndTime);
            int EndTime = Int32.Parse(EndTimeA + EndTimeB);
            if (chkEndTime < EndTime)
            {
                tipMsg = "您的公出時間已超過下班時間，請至加班系統填寫加班單。";
            }
        }

        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 代理員工編號檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool DeputyIDValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        if (String.IsNullOrEmpty(lblDeputyID.Text))
        {
            errorMsg = "請輸入代理人員工編號";
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 前往地點檢核(修改)
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool LocationTypeValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        bool inner = Inner.Checked;
        bool outter = Outter.Checked;
        string InterLocationName = StringIIF(txtInterLocationName.Text);
        string ExterLocationName = StringIIF(txtExterLocationName.Text);

        if (!inner && !outter)
        {
            errorMsg = "請選擇前往地點";
        }
        if (inner && !outter)
        {
            if (InterLocationName == "")
            {
                errorMsg = "請選擇內部地點";
            }
        }
        if (!inner && outter)
        {
            if (ExterLocationName == "")
            {
                errorMsg = "請輸入外部地點";
            }
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 洽辦事由檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool VisitReasonCNValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        var VisitReasonCN = StringIIF(ddlVisitReasonCN.SelectedValue);
        var VisitReasonDesc = StringIIF(txtVisitReasonDesc.Text);
        if (VisitReasonCN == "99" && VisitReasonDesc == "")
        {
            errorMsg = "洽辦事由選擇為其他，請輸入其他說明的欄位";
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        initReasonCN();         //洽辦事由下拉選單
        QueryMaxSeq();          //Seq Number
        QueryWorkSite();
        DoClear();

        WriterID_Name.Text = UserInfo.getUserInfo().UserID + " " + UserInfo.getUserInfo().UserName;
        WriteDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        //VisitFormNo.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "") + _maxSeq.PadLeft(4, '0');

        //ddlValid.Items.Clear();
        //ddlValid.Items.Insert(0, new ListItem("----請選擇----", ""));

        btnEmpID.selectCompID = UserInfo.getUserInfo().CompID;
        btnDeputyID.selectCompID = UserInfo.getUserInfo().CompID;
    }

    /// <summary>
    /// 暫存、送簽前的畫面檢核
    /// </summary>
    private void beforeSubmit(string regStr)
    {
        string errorMsg = "";
        string tipMsg = "";
        try 
        {
            if (!EmpIDValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!Tel_1Validation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!VisitBeginDateValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!TimeValidation(out errorMsg,out tipMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!DeputyIDValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!LocationTypeValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            else if (!VisitReasonCNValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }

            if(!"".Equals(tipMsg))
            {
                Util.MsgBox(tipMsg);
            }
            if ("Save".Equals(regStr))
            {
                DoSave();
            }
            else if ("Send".Equals(regStr))
            {
                DoSend();
            }
        }
        catch(Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    //-------------------------------------------------------------主要邏輯區

    /// <summary>
    /// 新增邏輯
    /// </summary>
    private void DoSave()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            OnBizPublicOutModel model = setViewModel();
            
            result = OnBizReqAddes.InsertVisitForm(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被新增!!");
            }
            Util.MsgBox("新增成功");
            LoadData();
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 送簽流程
    /// </summary>
    private void DoSend()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            OnBizPublicOutModel model = setViewModel();

            result = OnBizReq.Insert_UpdateVisitFormSign(model, "Addes", out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("送簽失敗!!");
            }
            Util.MsgBox("送簽成功!!");
            LoadData();
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 畫面初始狀態
    /// </summary>
    private void DoClear()
    {
        txtEmpID.Text = "";
        lblEmpID.Text = "";
        CompName.Text = "";
        OrganName.Text = "";
        TitleName.Text = "";
        Position.Text = "";
        txtTel_1.Text = "";
        txtTel_2.Text = "";
        txtVisitBeginDate.ucSelectedDate = "";
        ddlBeginTimeA.Text = "";
        ddlBeginTimeB.Text = "";
        ddlEndTimeA.Text = "";
        ddlEndTimeB.Text = "";
        txtDeputyID.Text = "";
        lblDeputyID.Text = "";
        Inner.Checked = false;
        Outter.Checked = false;
        txtInterLocationName.Text = "";
        txtExterLocationName.Text = "";
        txtInterLocationName.Enabled = false;
        txtExterLocationName.Enabled = false;
        txtVisiterName.Text = "";
        txtVisiterTel.Text = "";
        ddlVisitReasonCN.SelectedIndex = 0;
        //ddlValid.SelectedIndex = 0;
        txtVisitReasonDesc.Text = "";
    }

    //-------------------------------------------------------------方法

    /// <summary>
    /// 查詢最大FormSeq
    /// </summary>
    private void QueryMaxSeq()
    {
        var result = "";
        var isSuccess = false;
        var msg = "";
        var datas = new OnBizPublicOutBean();
        var viewData = new OnBizPublicOutModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            OBWriterID = UserInfo.getUserInfo().UserID,
            OBWriteDate = DateTime.Now.ToString("yyyy/MM/dd")
        };
        isSuccess = OnBizReqAddes.SelectVisitFormMaxSeq(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            var seq = Int32.Parse(datas.FormSeq) + 1;
            result = seq.ToString();
        }
        else
        {
            result = "1";
        }
        _maxSeq = result;
    }

    /// <summary>
    /// 查詢工作地點資料
    /// </summary>
    private void QueryWorkSite()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<OnBizPublicOutBean>();
        var viewData = new OnBizPublicOutModel()
        {
            CompID = UserInfo.getUserInfo().CompID
        };

        isSuccess = OnBizReq.SelectWorkSite(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            viewData.PersonGridDataList = OnBizReq.WorkSiteFormat(datas);
            ddlInterLocationName.DataSource = viewData.PersonGridDataList;
            ddlInterLocationName.DataValueField = "OBInterLocationID";
            ddlInterLocationName.DataTextField = "OBInterLocationName";
            ddlInterLocationName.DataBind();
        }
    }

    /// <summary>
    /// 查詢人員資料
    /// </summary>
    private void QueryEmpInfo(string txtEmp, string regStr)
    {
        var isSuccess = false;
        var msg = "";
        var datas = new OnBizPublicOutBean();
        var viewData = new OnBizPublicOutModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = txtEmp
        };

        isSuccess = OnBizReq.SelectPerson(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            viewData = OnBizReq.EmpInfoFormat(datas);
            switch (regStr)
            {
                case "EmpID":
                    {
                        SetEmpInfoDetail(viewData);
                        _OnBizEmpInfoModel = viewData;
                        break;
                    }
                case "DeputyID":
                    {
                        SetDeputyInfoDetail(viewData);
                        _OnBizDeputyInfoModel = viewData;
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// 取得執勤、個人、公司班表 
    /// </summary>
    /// <returns></returns>
    private bool QueryReport(string regStr, out PunchModel viewData)
    {
        bool result = false;
        string NowTime = DateTime.Now.ToString("HH:mm:ss");
        var msg = "";
        var datas = new PunchBean();
        viewData = new PunchModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = StringIIF(txtEmpID.Text),
            PunchDate = StringIIF(txtVisitBeginDate.ucSelectedDate)
        };

        switch (regStr)
        {
            case "Duty":
                {
                    result = Punch.GetDutyReport(viewData, out datas, out msg);
                    break;
                }
            case "EmpWork":
                {
                    result = Punch.GetEmpWorkReport(viewData, out datas, out msg);
                    break;
                }
        }
        if (result && datas != null)
        {
            viewData.BeginTime = datas.BeginTime;
            viewData.EndTime = datas.EndTime;
            viewData.RestBeginTime = datas.RestBeginTime;
            viewData.RestEndTime = datas.RestEndTime;
        }

        return result;
    }

    /// <summary>
    /// 公出人員資料明細
    /// </summary>
    private void SetEmpInfoDetail(OnBizPublicOutModel model)
    {
        txtEmpID.Text = model.EmpID;
        lblEmpID.Text = model.EmpNameN;
        CompName.Text = model.CompID + " " + model.CompName;
        OrganName.Text = model.OBOrganID + " " + model.OBOrganName;
        TitleName.Text = model.OBTitleID + " " + model.OBTitleName;
        Position.Text = model.OBPositionID + " " + model.OBPosition;
        btnDeputyID.selectOutEmpID = model.EmpID;
    }

    /// <summary>
    /// 職務代理人員資料明細
    /// </summary>
    private void SetDeputyInfoDetail(OnBizPublicOutModel model)
    {
        txtDeputyID.Text = model.EmpID;
        lblDeputyID.Text = model.EmpNameN;
    }

    /// <summary>
    /// 從Session取得公出、職務代理人員詳細資料
    /// </summary>
    private void SetEmpInfoFromSession()
    {
        if (_SessionEmpInfoModel.Count > 0)
        {
            Dictionary<string, string> dyEmp = _SessionEmpInfoModel;
            OnBizPublicOutModel model = new OnBizPublicOutModel()
            {
                CompID = StringIIF(dyEmp["CompID"].ToString()),
                CompName = StringIIF(dyEmp["CompName"].ToString()),
                EmpID = StringIIF(dyEmp["EmpID"].ToString()),
                EmpNameN = StringIIF(dyEmp["EmpNameN"].ToString()),
                OBDeptID = StringIIF(dyEmp["DeptID"].ToString()),
                OBDeptName = StringIIF(dyEmp["DeptName"].ToString()),
                OBOrganID = StringIIF(dyEmp["OrganID"].ToString()),
                OBOrganName = StringIIF(dyEmp["OrganName"].ToString()),
                OBWorkTypeID = StringIIF(dyEmp["WorkTypeID"].ToString()),
                OBWorkType = StringIIF(dyEmp["WorkType"].ToString()),
                OBPositionID = StringIIF(dyEmp["PositionID"].ToString()),
                OBPosition = StringIIF(dyEmp["Position"].ToString()),
                OBFlowOrganID = StringIIF(dyEmp["FlowOrganID"].ToString()),
                OBFlowOrganName = StringIIF(dyEmp["FlowOrganName"].ToString()),
                OBTitleID = StringIIF(dyEmp["TitleID"].ToString()),
                OBTitleName = StringIIF(dyEmp["TitleName"].ToString())
            };
            SetEmpInfoDetail(model);
            _OnBizEmpInfoModel = model;
        }
        if (_SessionDeputyInfoModel.Count > 0)
        {
            Dictionary<string, string> dyDeputy = _SessionDeputyInfoModel;
            OnBizPublicOutModel model = new OnBizPublicOutModel()
            {
                EmpID = StringIIF(dyDeputy["EmpID"].ToString()),
                EmpNameN = StringIIF(dyDeputy["EmpNameN"].ToString()),
            };
            SetDeputyInfoDetail(model);
            _OnBizDeputyInfoModel = model;
        }
    }

    /// <summary>
    /// 將畫面上的直塞入ViewModel
    /// </summary>
    /// <returns></returns>
    private OnBizPublicOutModel setViewModel()
    {
        OnBizPublicOutModel model = new OnBizPublicOutModel()
        {
            CompID = StringIIF(_OnBizEmpInfoModel.CompID),
            EmpID = StringIIF(_OnBizEmpInfoModel.EmpID),
            EmpNameN = StringIIF(_OnBizEmpInfoModel.EmpNameN),
            OBWriteDate = StringIIF(WriteDate.Text),
            OBWriteTime = StringIIF(DateTime.Now.ToString("HH:mm")),
            OBWriterID = StringIIF(UserInfo.getUserInfo().UserID),
            OBWriterName = StringIIF(UserInfo.getUserInfo().UserName),
            OBFormSeq = StringIIF(_maxSeq), //還需再修改
            OBDeptID = StringIIF(_OnBizEmpInfoModel.OBDeptID),
            OBDeptName = StringIIF(_OnBizEmpInfoModel.OBDeptName),
            OBOrganID = StringIIF(_OnBizEmpInfoModel.OBOrganID),
            OBOrganName = StringIIF(_OnBizEmpInfoModel.OBOrganName),
            OBWorkTypeID = StringIIF(_OnBizEmpInfoModel.OBWorkTypeID),
            OBWorkType = StringIIF(_OnBizEmpInfoModel.OBWorkType),
            OBFlowOrganID = StringIIF(_OnBizEmpInfoModel.OBFlowOrganID),
            OBFlowOrganName = StringIIF(_OnBizEmpInfoModel.OBFlowOrganName),
            OBTitleID = StringIIF(_OnBizEmpInfoModel.OBTitleID),
            OBTitleName = StringIIF(_OnBizEmpInfoModel.OBTitleName),
            OBPositionID = StringIIF(_OnBizEmpInfoModel.OBPositionID),
            OBPosition = StringIIF(_OnBizEmpInfoModel.OBPosition),
            OBTel_1 = StringIIF(txtTel_1.Text),
            OBTel_2 = StringIIF(txtTel_2.Text),
            OBVisitBeginDate = StringIIF(txtVisitBeginDate.ucSelectedDate),
            OBBeginTime = StringIIF((ddlBeginTimeA.Text + ":" + ddlBeginTimeB.Text)),
            OBBeginTimeH = StringIIF(ddlBeginTimeA.Text),
            OBBeginTimeM = StringIIF(ddlBeginTimeB.Text),
            OBVisitEndDate = StringIIF(txtVisitBeginDate.ucSelectedDate),
            OBEndTime = StringIIF((ddlEndTimeA.Text + ":" + ddlEndTimeB.Text)),
            OBEndTimeH = StringIIF(ddlEndTimeA.Text),
            OBEndTimeM = StringIIF(ddlEndTimeB.Text),
            OBDeputyID = StringIIF(_OnBizDeputyInfoModel.EmpID),
            OBDeputyName = StringIIF(_OnBizDeputyInfoModel.EmpNameN),
            OBLocationType = StringIIF(Inner.Checked ? "1" : "2"),
            OBInterLocationID = StringIIF(Inner.Checked ? txtInterLocationName.Text.Split(" ")[0] : ""),
            OBInterLocationName = StringIIF(Inner.Checked ? txtInterLocationName.Text.Split(" ")[1] : ""),
            OBExterLocationName = StringIIF(txtExterLocationName.Text),
            OBVisiterName = StringIIF(txtVisiterName.Text),
            OBVisiterTel = StringIIF(txtVisiterTel.Text),
            OBVisitReasonID = StringIIF(ddlVisitReasonCN.SelectedValue),
            OBVisitReasonCN = StringIIF("".Equals(ddlVisitReasonCN.SelectedValue)?"":ddlVisitReasonCN.SelectedItem.Text),
            OBVisitReasonDesc = StringIIF(txtVisitReasonDesc.Text),
            OBLastChgComp = UserInfo.getUserInfo().CompID,
            OBLastChgID = UserInfo.getUserInfo().UserID,
        };

        return model;
    }

    /// <summary>
    /// 取得班表，順序:值勤 > 個人
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    private bool getPunchReport(out PunchModel datas)
    {
        bool result = true;
        datas = new PunchModel();

        if (!QueryReport("Duty", out datas))
        {
            if (!QueryReport("EmpWork", out datas))
            {
                return false;
            }
        }
        return result;
    }

    /// <summary>
    /// CheckedChanged
    /// 內部外部欄位開關
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OutInSwitch(RadioButton radioButton)
    {
        string fieldName = radioButton.ID;
        switch (fieldName)
        {
            case "Inner":
                {
                    txtInterLocationName.Enabled = radioButton.Checked;
                    txtExterLocationName.Enabled = false;
                    txtExterLocationName.Text = "";

                    break;
                }
            case "Outter":
                {
                    txtExterLocationName.Enabled = radioButton.Checked;
                    txtInterLocationName.Enabled = false;
                    txtInterLocationName.Text = "";
                    break;
                }
        }
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

    //-------------------------------------------------------------初始物件

    /// <summary>
    /// 洽辦事由下拉選單
    /// </summary>
    private void initReasonCN()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<DropDownListModel>();

        isSuccess = OnBizReq.SelectAT_CodeMap(out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            ddlVisitReasonCN.DataSource = datas;
            ddlVisitReasonCN.DataTextField = "DataText";
            ddlVisitReasonCN.DataValueField = "DataValue";
            ddlVisitReasonCN.DataBind();
            ddlVisitReasonCN.Items.Insert(0, new ListItem("----請選擇----", ""));
        }
    }

    #endregion





}


