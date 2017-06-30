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

public partial class OnBiz_OnBizReqInqForOrganView : BasePage
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

    /// <summary>
    /// _SessionDetailModel
    /// </summary>
    private OnBizPublicOutModel _SessionDetailModel
    {
        get
        {
            if (Session["OnBiz_OnBizReqModifyView"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session["OnBiz_OnBizReqModifyView"] = new OnBizPublicOutModel();
            }
            return JsonConvert.DeserializeObject<OnBizPublicOutModel>(Session["OnBiz_OnBizReqModifyView"].ToString());
        }
        set
        {
            Session["OnBiz_OnBizReqModifyView"] = JsonConvert.SerializeObject(value);
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
    /// gvMain_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
        if (e.CommandName == "Detail")
        {
            //DataKey gridData = gvMain.DataKeys[dataRow.RowIndex];
            var viewData = new OnBizPublicOutModel();
            viewData.CompID = gvMain.DataKeys[clickedRow.RowIndex].Values["CompID"].ToString();
            viewData.EmpID = gvMain.DataKeys[clickedRow.RowIndex].Values["EmpID"].ToString();
            viewData.OBWriteDate = gvMain.DataKeys[clickedRow.RowIndex].Values["OBWriteDate"].ToString();
            viewData.OBFormSeq = gvMain.DataKeys[clickedRow.RowIndex].Values["OBFormSeq"].ToString();
            viewData.PageName = "OnBizReqInqForOrganView";
            _SessionDetailModel = viewData;

            Response.Redirect("OnBizReqDetailView.aspx");
        }
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
    /// ddlCompID_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompID_Changed(object sender, EventArgs e)
    {
        if (ddlCompID.SelectedValue == "")
        {
            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRoleCode40);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);

            _AllowOrgan = new List<OrganListModel>();
            _AllowFlowOrgan = new List<FlowOrganListModel>();
            _AllowFlowOrganView = new List<FlowOrganListModel>();
        }
        else
        {
            LoadOrgType();
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
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
            resetDDL(ddlDeptID);
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
            resetDDL(ddlOrganID);
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
    /// 公司檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private string CompValidation()
    {
        string result = "";

        var compID = StringIIF(ddlCompID.SelectedValue);

        if (compID == "")
        {
                result = "請選擇公司";
        }

        return result;
    }

    /// <summary>
    /// 日期檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private string DateValidation()
    {
        string result = "";

        var startDate = StringIIF(ucStartDate.ucSelectedDate);
        var endDate = StringIIF(ucEndDate.ucSelectedDate);

        if (startDate != "" && endDate != "")
        {
            var sDate = Int32.Parse(startDate.Replace("/", ""));
            var eDate = Int32.Parse(endDate.Replace("/", ""));
            if (sDate > eDate)
            {
                result = "起日不得大於迄日";
            }
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
        initComp();

        if (ddlCompID.Items.Count > 1)
        {
            ddlCompID.Items.Insert(0, new ListItem("----請選擇----", ""));
            resetDDL(ddlOrgType);
        }
        else 
        {
            LoadOrgType();
        }
        btnEmpID.selectCompID = UserInfo.getUserInfo().CompID;
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

        if (CompValidation() != "")
        {
            errorMsg = CompValidation();
        }

        if (DateValidation() != "")
        {
            errorMsg = DateValidation();
        }

        if (errorMsg != "")
        {
            Util.MsgBox(errorMsg);
            return;
        }
        else
        {
            DoQuery();
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
        var fieldName = "";
        var datas = new List<OnBizPublicOutBean>();
        var viewData = new OnBizPublicOutModel()
        {
            CompID = StringIIF(ddlCompID.SelectedValue),
            EmpID = StringIIF(txtEmpID.Text),
            OBOrganID = StringIIF(GetOrganWhere()),
            OBFlowOrganID = StringIIF(GetFlowOrganWhere()),
            OBVisitBeginDate = StringIIF(ucStartDate.ucSelectedDate),
            OBVisitEndDate = StringIIF(ucEndDate.ucSelectedDate)
        };
        fieldName = ddlOrganID.SelectedValue;

        isSuccess = OnBizReqInqForOrgan.SelectVisitFormOrgan(viewData, fieldName, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = OnBizReq.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
        //_OnBizRegInquireModel = viewData;
    }

    /// <summary>
    /// 清除邏輯
    /// </summary>
    private void DoClear()
    {
        if (ddlCompID.Items.Count > 1)
        {
            ddlCompID.SelectedIndex = 0;
        }
        ddlOrganization.SelectedIndex = 0;
        ddlOrganization_Changed(null, EventArgs.Empty);
        txtEmpID.Text = "";
        ucStartDate.ucSelectedDate = "";
        ucEndDate.ucSelectedDate = "";

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
        var viewData = new OnBizPublicOutModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID,
            selectCompID = ddlCompID.SelectedValue
        };

        isSuccess = OnBizReqInqForOrgan.SelectOrgan(viewData, out datas, out msg);
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
        ddlOrganID.DataSource = _AllowOrgan.Where(x => x.DeptID == ddlDeptID.SelectedValue && x.OrganID != ddlDeptID.SelectedValue).Select(x => new { x.OrganID, x.OrganName }).ToList();
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
                //orgWhere += string.Join("', '", _AllowOrgan.Where(x => x.OrganID == ddlDeptID.SelectedValue).Select(x => x.OrganID));
            }
            else if (ddlOrgType.SelectedValue != "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "")
            {
                orgWhere += string.Join("', '", _AllowOrgan.Where(x => x.OrgType == ddlOrgType.SelectedValue).Select(x => x.OrganID));
                //orgWhere += string.Join("', '", _AllowOrgan.Where(x => x.OrganID == ddlOrgType.SelectedValue).Select(x => x.OrganID));
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
        var viewData = new OnBizPublicOutModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID,
            selectCompID = ddlCompID.SelectedValue,
            FlowType = "D"
        };

        isSuccess = OnBizReqInqForOrgan.SelectFlowOrgan(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            _AllowFlowOrgan = datas;

            viewData = new OnBizPublicOutModel()
            {
                CompID = UserInfo.getUserInfo().CompID,
                EmpID = UserInfo.getUserInfo().UserID,
                selectCompID = ddlCompID.SelectedValue,
                FlowType = "B"
            };

            isSuccess = OnBizReqInqForOrgan.SelectFlowOrgan(viewData, out datas, out msg);
            if (isSuccess && datas != null && datas.Count > 0)
            {
                _AllowFlowOrganView = datas;
                ddlRoleCode40.DataSource = datas.Where(x => x.RoleCode == "40").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
                ddlRoleCode40.DataTextField = "OrganName";
                ddlRoleCode40.DataValueField = "OrganID";
                ddlRoleCode40.DataBind();
                ddlRoleCode40.Items.Insert(0, new ListItem("----請選擇----", ""));
            }
            else
            {
                _AllowFlowOrganView = new List<FlowOrganListModel>();
                resetDDL(ddlRoleCode40);
                resetDDL(ddlRoleCode30);
                resetDDL(ddlRoleCode20);
                resetDDL(ddlRoleCode10);
            }
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
            lblEmpID.Text = StringIIF(dyEmp["EmpNameN"].ToString());
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
            lblEmpID.Text = datas.EmpNameN;
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
    /// 取得公司
    /// </summary>
    private void initComp()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<DropDownListModel>();
        var viewData = new OnBizPublicOutModel()
        {
            selectCompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID
        };

        isSuccess = OnBizReqInqForOrgan.SelectBothComp(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            ddlCompID.DataSource = datas;
            ddlCompID.DataTextField = "DataText";
            ddlCompID.DataValueField = "DataValue";
            ddlCompID.DataBind();
        }
    }
    
    #endregion



}


