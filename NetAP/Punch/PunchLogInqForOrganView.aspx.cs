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

public partial class Punch_PunchLogInqForOrganView : BasePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _AllowOrgan
    /// </summary>
    private List<OrganListModel> _AllowOrgan //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowOrgan"] != null)
                {
                    return JsonConvert.DeserializeObject<List<OrganListModel>>(ViewState["_AllowOrgan"].ToString());
                }
                else
                {
                    return new List<OrganListModel>();
                }
            }
            catch (Exception)
            {
                return new List<OrganListModel>();
            }
        }
        set
        {
            ViewState["_AllowOrgan"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _AllowFlowOrgan
    /// </summary>
    private List<FlowOrganListModel> _AllowFlowOrgan //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowFlowOrgan"] != null)
                {
                    return JsonConvert.DeserializeObject<List<FlowOrganListModel>>(ViewState["_AllowFlowOrgan"].ToString());
                }
                else
                {
                    return new List<FlowOrganListModel>();
                }
            }
            catch (Exception)
            {
                return new List<FlowOrganListModel>();
            }
        }
        set
        {
            ViewState["_AllowFlowOrgan"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _AllowFlowOrganView
    /// </summary>
    private List<FlowOrganListModel> _AllowFlowOrganView //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowFlowOrganView"] != null)
                {
                    return JsonConvert.DeserializeObject<List<FlowOrganListModel>>(ViewState["_AllowFlowOrganView"].ToString());
                }
                else
                {
                    return new List<FlowOrganListModel>();
                }
            }
            catch (Exception)
            {
                return new List<FlowOrganListModel>();
            }
        }
        set
        {
            ViewState["_AllowFlowOrganView"] = JsonConvert.SerializeObject(value);
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

    #endregion

    #region "2. 功能鍵處理邏輯"

    /// <summary>
    /// btnQuery_Click
    /// 查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        beforeQueryCheck();
        //DoQuery();
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
    /// gvMain_RowDataBound一定要存在的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    /// <summary>
    /// 員工編號查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {
        QueryEmpInfo(txtEmpID.Text);
    }

    /// <summary>
    /// 組織別下拉選單
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOrganization_Changed(object sender, EventArgs e)
    {
        string fieldID = StringIIF(ddlOrganization.SelectedValue);
        switch (fieldID) 
        {
            case "Organ":
                {
                    Organ.Visible = true;
                    FlowOrgan.Visible = false;
                    resetDDL(ddlOrgType);
                    LoadOrgType();
                    resetDDL(ddlDeptID);
                    resetDDL(ddlOrganID);
                    break;
                }
            case "FlowOrgan":
                {
                    Organ.Visible = false;
                    FlowOrgan.Visible = true;
                    resetDDL(ddlRoleCode40);
                    LoadRoleCode40();
                    resetDDL(ddlRoleCode30);
                    resetDDL(ddlRoleCode20);
                    resetDDL(ddlRoleCode10);
                    break;
                }
        }
    }

    /// <summary>
    /// ddlOrgType_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOrgType_Changed(object sender, EventArgs e)
    {
        if (ddlOrgType.SelectedValue == "")
        {
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
        }
        else
        {
            LoadDeptID();
        }
    }

    /// <summary>
    /// ddlDeptID_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDeptID_Changed(object sender, EventArgs e)
    {
        if (ddlDeptID.SelectedValue == "")
        {
            resetDDL(ddlOrganID);
        }
        else
        {
            LoadOrganID();
        }
    }

    /// <summary>
    /// ddlRoleCode40_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode40_Changed(object sender, EventArgs e)
    {
        if (ddlRoleCode40.SelectedValue != "")
        {
            LoadRoleCode30();
        }
        else
        {
            resetDDL(ddlRoleCode30);
        }
        resetDDL(ddlRoleCode20);
        resetDDL(ddlRoleCode10);
    }

    /// <summary>
    /// ddlRoleCode30_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode30_Changed(object sender, EventArgs e)
    {
        if (ddlRoleCode30.SelectedValue != "")
        {
            LoadRoleCode20();
        }
        else
        {
            resetDDL(ddlRoleCode20);
        }
        resetDDL(ddlRoleCode10);
    }

    /// <summary>
    /// ddlRoleCode20_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode20_Changed(object sender, EventArgs e)
    {
        if (ddlRoleCode20.SelectedValue != "")
        {
            LoadRoleCode10();
        }
        else
        {
            resetDDL(ddlRoleCode10);
        }
    }

    #endregion

    #region "6. 畫面檢核與確認"
    /// <summary>
    /// 日期檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool DateValidation(out string errorMsg)
    {
        bool result = true;
        bool startFlag = true;
        errorMsg = "";

        var after5YDate = StringIIF(DateTime.Now.AddYears(5).ToString("yyyy/MM/dd"));
        var before5YDate = StringIIF(DateTime.Now.AddYears(-5).ToString("yyyy/MM/dd"));
        var startDate = StringIIF(ucStartDate.ucSelectedDate);
        var endDate = StringIIF(ucEndDate.ucSelectedDate);

        if ("".Equals(startDate) && "".Equals(endDate))
        {
            errorMsg = "請輸入查詢日期!";
        }
        else if (!"".Equals(startDate) && "".Equals(endDate))
        {
            errorMsg = "請輸入查詢日期(迄日)!";
        }
        else if ("".Equals(startDate) && !"".Equals(endDate))
        {
            errorMsg = "請輸入查詢日期!(起日)";
        }

        if (startDate != "")
        {
            int afterChkDay = OnBizReq.GetTimeDiff(startDate, after5YDate, "Day");
            int beforeChkDay = OnBizReq.GetTimeDiff(startDate, before5YDate, "Day");
            if (afterChkDay < 0 || beforeChkDay > 0)
            {
                startFlag = false;
                errorMsg = "查詢日期限最近5年以內。";
            }
        }
        if (startDate != "" && endDate != "" && startFlag)
        {
            var sDate = Int32.Parse(startDate.Replace("/", ""));
            var eDate = Int32.Parse(endDate.Replace("/", ""));
            if (sDate > eDate)
            {
                errorMsg = "起日不得大於迄日";
            }
            else
            {
                string dayMonth = Convert.ToDateTime(startDate).AddMonths(1).ToString("yyyy/MM/dd");
                int datMonthDiff = OnBizReq.GetTimeDiff(startDate, dayMonth, "Day");
                int dayDiff = OnBizReq.GetTimeDiff(startDate, endDate, "Day");
                //int m1 = OnBizReq.GetTimeDiff("18:30", "23:55", "Minute");
                if (dayDiff > datMonthDiff)
                {
                    errorMsg = "查詢區間限最多1個月以內";
                }
            }
        }
        if (!"".Equals(errorMsg))
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 查詢類別檢核(必輸)
    /// </summary>
    /// <param name="errorMsg"></param>
    /// <returns></returns>
    private bool SearchTypeValidation(out string errorMsg)
    {
        bool result = true;
        errorMsg = "";
        if (String.IsNullOrEmpty(StringIIF(ddlSearchType.SelectedValue)))
        {
            errorMsg = "請選擇查詢類別";
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
        lblCompID.Text = UserInfo.getUserInfo().CompID + UserInfo.getUserInfo().CompName;
        btnEmpID.selectCompID = UserInfo.getUserInfo().CompID;
        ucStartDate.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
        ucEndDate.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
        initHrMin();
        LoadOrgType();
        resetDDL(ddlRoleCode40);
        resetDDL(ddlDeptID);
        resetDDL(ddlOrganID);
        resetDDL(ddlRoleCode30);
        resetDDL(ddlRoleCode20);
        resetDDL(ddlRoleCode10);
    }

    /// <summary>
    /// 查詢前的畫面檢核
    /// </summary>
    private void beforeQueryCheck()
    {
        string errorMsg = "";
        try
        {
            if (!DateValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            if (!SearchTypeValidation(out errorMsg))
            {
                throw new Exception(errorMsg);
            }
            DoQuery();
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    //-------------------------------------------------------------主要邏輯區

    /// <summary>
    /// 查詢邏輯
    /// </summary>
    private void DoQuery()
    {
        var isSuccess = false;
        var msg = "";
        var orgType = StringIIF(ddlOrganization.SelectedValue);
        var searchType = StringIIF(ddlSearchType.SelectedValue);
        var datas = new List<PunchConfirmBean>();
        var viewData = new PunchConfirmModel();

        viewData.CompID = StringIIF(UserInfo.getUserInfo().CompID);
        viewData.OrganID = StringIIF(GetOrganWhere());
        viewData.FlowOrganID = StringIIF(GetFlowOrganWhere());
        viewData.PunchSDate = StringIIF(ucStartDate.ucSelectedDate);
        viewData.PunchEDate = StringIIF(ucEndDate.ucSelectedDate);
        viewData.PunchSTime = StringIIF(StartTimeH.SelectedValue + ":" + StartTimeM.SelectedValue);
        viewData.PunchETime = StringIIF(EndTimeH.SelectedValue + ":" + EndTimeM.SelectedValue);
        viewData.ConfirmPunchFlag = StringIIF(ddlConfirmPunchFlag.SelectedValue);
        viewData.ConfirmStatus = StringIIF(ddlConfirmStatus.SelectedValue);
        viewData.Remedy_AbnormalFlag = StringIIF(ddlRemedy_AbnormalFlag.SelectedValue);
        viewData.EmpID = StringIIF(txtEmpID.Text);
        viewData.EmpName = StringIIF(txtEmpName.Text);

        isSuccess = PunchLogInqForOrgan.SelectPunchConfirmForAll(viewData, orgType, searchType, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = PunchLogInqForOrgan.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
    }

    /// <summary>
    /// 清除邏輯
    /// </summary>
    private void DoClear()
    {

        LoadOrgType();
        resetDDL(ddlRoleCode40);
        resetDDL(ddlDeptID);
        resetDDL(ddlOrganID);
        resetDDL(ddlRoleCode30);
        resetDDL(ddlRoleCode20);
        resetDDL(ddlRoleCode10);
    }

    //-------------------------------------------------------------方法

    #region "行政線"

    private void LoadOrgType()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<OrganListModel>();
        var viewData = new PunchConfirmModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID,
        };

        isSuccess = PunchLogInqForOrgan.SelectOrganForAll(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            _AllowOrgan = datas;

            ddlOrgType.DataSource = datas.Select(x => new { x.OrgType, x.OrgTypeName }).Distinct().ToList();
            ddlOrgType.DataTextField = "OrgTypeName";
            ddlOrgType.DataValueField = "OrgType";
            ddlOrgType.DataBind();
            ddlOrgType.Items.Insert(0, new ListItem("----請選擇----", ""));
        }
        else
        {
            _AllowOrgan = new List<OrganListModel>();
            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
        }
    }

    private void LoadDeptID()
    {
        ddlDeptID.DataSource = _AllowOrgan.Where(x => x.OrgType == ddlOrgType.SelectedValue).Select(x => new { x.DeptID, x.DeptName }).Distinct().ToList();
        ddlDeptID.DataTextField = "DeptName";
        ddlDeptID.DataValueField = "DeptID";
        ddlDeptID.DataBind();
        if (ddlDeptID.Items.FindByValue(ddlOrgType.SelectedValue) == null)
        {
            ddlDeptID.Items.Insert(0, new ListItem("----請選擇----", ""));
        }
        else
        {
            ddlDeptID.SelectedValue = ddlOrgType.SelectedValue;
            LoadOrganID();
        }
    }

    private void LoadOrganID()
    {
        ddlOrganID.DataSource = _AllowOrgan.Where(x => x.DeptID == ddlDeptID.SelectedValue).Select(x => new { x.OrganID, x.OrganName }).ToList();
        ddlOrganID.DataTextField = "OrganName";
        ddlOrganID.DataValueField = "OrganID";
        ddlOrganID.DataBind();
        ddlOrganID.Items.Insert(0, new ListItem("----請選擇----", ""));
    }

    /// <summary>
    /// 組OrganID選單
    /// </summary>
    /// <param name="DDL"></param>
    private string GetOrganWhere()
    {
        string orgWhere = "";
        bool AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");

        if ((ddlOrgType.SelectedValue != "" || ddlDeptID.SelectedValue != "" || ddlOrganID.SelectedValue != "") || AllSearch)
        {
            if (ddlOrganID.SelectedValue != "")
            {
                orgWhere += ddlOrganID.SelectedValue;
            }
            else if (ddlDeptID.SelectedValue != "" && ddlOrganID.SelectedValue == "")
            {
                orgWhere += string.Join("', '", _AllowOrgan.Where(x => x.DeptID == ddlDeptID.SelectedValue).Select(x => x.OrganID));
            }
            else if (ddlOrgType.SelectedValue != "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "")
            {
                orgWhere += string.Join("', '", _AllowOrgan.Where(x => x.OrgType == ddlOrgType.SelectedValue).Select(x => x.OrganID));
            }
            else
            {
                orgWhere += string.Join("', '", _AllowOrgan.Select(x => x.OrganID));
            }
        }

        return orgWhere;
    }

    #endregion

    #region "功能線"

    private void LoadRoleCode40()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<FlowOrganListModel>();
        var viewData = new PunchConfirmModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID,
        };

        isSuccess = PunchLogInqForOrgan.SelectFlowOrganForAll(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            _AllowFlowOrgan = datas;

            _AllowFlowOrganView = datas;
            ddlRoleCode40.DataSource = datas.Where(x => x.RoleCode == "40").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
            ddlRoleCode40.DataTextField = "OrganName";
            ddlRoleCode40.DataValueField = "OrganID";
            ddlRoleCode40.DataBind();
            ddlRoleCode40.Items.Insert(0, new ListItem("----請選擇----", ""));

        }
        else
        {
            _AllowFlowOrgan = new List<FlowOrganListModel>();
            resetDDL(ddlRoleCode40);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);
        }
    }

    private void LoadRoleCode30()
    {
        ddlRoleCode30.DataSource = _AllowFlowOrganView.Where(x => x.UpOrganID == ddlRoleCode40.SelectedValue && x.RoleCode == "30").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
        ddlRoleCode30.DataTextField = "OrganName";
        ddlRoleCode30.DataValueField = "OrganID";
        ddlRoleCode30.DataBind();
        ddlRoleCode30.Items.Insert(0, new ListItem("----請選擇----", ""));
    }

    private void LoadRoleCode20()
    {
        ddlRoleCode20.DataSource = _AllowFlowOrganView.Where(x => x.UpOrganID == ddlRoleCode30.SelectedValue && x.RoleCode == "20").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
        ddlRoleCode20.DataTextField = "OrganName";
        ddlRoleCode20.DataValueField = "OrganID";
        ddlRoleCode20.DataBind();
        ddlRoleCode20.Items.Insert(0, new ListItem("----請選擇----", ""));
    }

    private void LoadRoleCode10()
    {
        ddlRoleCode10.DataSource = _AllowFlowOrganView.Where(x => x.DeptID == ddlRoleCode20.SelectedValue && (x.RoleCode == "10" || x.RoleCode == "0")).Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
        ddlRoleCode10.DataTextField = "OrganName";
        ddlRoleCode10.DataValueField = "OrganID";
        ddlRoleCode10.DataBind();
        ddlRoleCode10.Items.Insert(0, new ListItem("----請選擇----", ""));
    }

    /// <summary>
    /// 組FlowOrganID選單
    /// </summary>
    /// <param name="DDL"></param>
    private string GetFlowOrganWhere()
    {
        string orgFlowWhere = "";
        bool AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");

        if ((ddlRoleCode40.SelectedValue != "" || ddlRoleCode30.SelectedValue != "" || ddlRoleCode20.SelectedValue != "" || ddlRoleCode10.SelectedValue != "") || AllSearch)
        {
            if (ddlRoleCode10.SelectedValue != "")
            {
                if (ddlRoleCode10.SelectedItem.Text.StartsWith("└"))
                {
                    orgFlowWhere = ddlRoleCode10.SelectedValue;
                }
                else
                {
                    orgFlowWhere = string.Join("', '", _AllowFlowOrgan.Where(x => x.OrganLevel == ddlRoleCode10.SelectedValue).Select(x => x.OrganID));
                }
            }
            else if (ddlRoleCode20.SelectedValue != "" && ddlRoleCode10.SelectedValue == "")
            {
                orgFlowWhere = string.Join("', '", _AllowFlowOrgan.Where(x => x.DeptID == ddlRoleCode20.SelectedValue).Select(x => x.OrganID));
            }
            else if (ddlRoleCode30.SelectedValue != "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "")
            {
                orgFlowWhere = ddlRoleCode30.SelectedValue;
                orgFlowWhere += "', '" + string.Join("', '", _AllowFlowOrgan.Where(x => x.UpOrganID == ddlRoleCode30.SelectedValue).Select(x => x.OrganID));

                var RoleCode20 = _AllowFlowOrgan.Where(x => x.UpOrganID == ddlRoleCode30.SelectedValue).Select(x => x.OrganID).ToArray();
                orgFlowWhere += "', '" + string.Join("', '", _AllowFlowOrgan.Where(x => RoleCode20.Contains(x.DeptID)).Select(x => x.OrganID));
            }
            else if (ddlRoleCode40.SelectedValue != "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "")
            {
                var BusinessType = _AllowFlowOrgan.Where(x => x.OrganID == ddlRoleCode40.SelectedValue).Select(x => x.BusinessType).ToArray();
                orgFlowWhere = string.Join("', '", _AllowFlowOrgan.Where(x => x.BusinessType == BusinessType[0]).Select(x => x.OrganID));
            }
            else
            {
                orgFlowWhere = string.Join("', '", _AllowFlowOrgan.Select(x => x.OrganID));
            }
        }

        return orgFlowWhere;
    }

    #endregion

    /// <summary>
    /// 從Session取得人員詳細資料
    /// </summary>
    private void SetEmpInfoFromSession()
    {
        if (_SessionEmpInfoModel.Count > 0)
        {
            Dictionary<string, string> dyEmp = _SessionEmpInfoModel;
            txtEmpID.Text = StringIIF(dyEmp["EmpID"].ToString());
            txtEmpName.Text = StringIIF(dyEmp["EmpNameN"].ToString());
        }
    }

    /// <summary>
    /// 查詢人員資料
    /// </summary>
    private void QueryEmpInfo(string txtEmp)
    {
        var isSuccess = false;
        var msg = "";
        var datas = new PunchConfirmBean();
        var viewData = new PunchConfirmModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = txtEmp
        };

        isSuccess = PunchLogInqForOrgan.SelectPerson(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            txtEmpID.Text = datas.EmpID;
            txtEmpName.Text = datas.EmpNameN;
        }
    }

    /// <summary>
    /// 重設DDL，給予請選擇
    /// </summary>
    /// <param name="DDL"></param>
    private void resetDDL(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    /// <summary>
    /// 取得字串(去除null)
    /// </summary>
    private string StringIIF(object str) 
    {
        string result = "";
        if(str != null)
        {
            if(!string.IsNullOrEmpty(str.ToString().Trim()))
            {
                result = str.ToString();
            }
        }

        return result;
    }

    //-------------------------------------------------------------初始物件

    /// <summary>
    /// 時間原件初始
    /// </summary>
    private void initHrMin()
    {
        for (int Hr = 0; Hr <= 23; Hr++)
        {
            string HH = Convert.ToString(Hr);
            StartTimeH.Items.Insert(Hr + 1, new ListItem(Hr < 10 ? "0" + HH : HH, Hr < 10 ? "0" + HH : HH));
            EndTimeH.Items.Insert(Hr + 1, new ListItem(Hr < 10 ? "0" + HH : HH, Hr < 10 ? "0" + HH : HH));
        }
        for (int Min = 0; Min <= 59; Min++)
        {
            string MM = Convert.ToString(Min);
            StartTimeM.Items.Insert(Min + 1, new ListItem(Min < 10 ? "0" + MM : MM, Min < 10 ? "0" + MM : MM));
            EndTimeM.Items.Insert(Min + 1, new ListItem(Min < 10 ? "0" + MM : MM, Min < 10 ? "0" + MM : MM));
        }
    }
    
    #endregion




}


