using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using System.Diagnostics;
using System.Data;
using System.Text;
using Newtonsoft.Json;

public partial class WorkTime_EmpWorkTimeQuery : SecurePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _AllowOrgan
    /// </summary>
    private List<OrganListMobel> _AllowOrgan //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowOrgan"] != null)
                {
                    return JsonConvert.DeserializeObject<List<OrganListMobel>>(ViewState["_AllowOrgan"].ToString());
                }
                else
                {
                    return new List<OrganListMobel>();
                }
            }
            catch (Exception)
            {
                return new List<OrganListMobel>();
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
    private List<FlowOrganListMobel> _AllowFlowOrgan //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowFlowOrgan"] != null)
                {
                    return JsonConvert.DeserializeObject<List<FlowOrganListMobel>>(ViewState["_AllowFlowOrgan"].ToString());
                }
                else
                {
                    return new List<FlowOrganListMobel>();
                }
            }
            catch (Exception)
            {
                return new List<FlowOrganListMobel>();
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
    private List<FlowOrganListMobel> _AllowFlowOrganView //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_AllowFlowOrganView"] != null)
                {
                    return JsonConvert.DeserializeObject<List<FlowOrganListMobel>>(ViewState["_AllowFlowOrganView"].ToString());
                }
                else
                {
                    return new List<FlowOrganListMobel>();
                }
            }
            catch (Exception)
            {
                return new List<FlowOrganListMobel>();
            }
        }
        set
        {
            ViewState["_AllowFlowOrganView"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _IsBoss
    /// </summary>
    private bool _IsBoss //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_IsBoss"] != null)
                {
                    return JsonConvert.DeserializeObject<bool>(ViewState["_IsBoss"].ToString());
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        set
        {
            ViewState["_IsBoss"] = JsonConvert.SerializeObject(value);
        }
    }
    #endregion

    #region "2. 功能鍵處理邏輯"
    /// <summary>
    /// btnQuery_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DoQuery();
    }

    /// <summary>
    /// btnClear_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        DoClear();
    }

    /// <summary>
    /// 查詢邏輯
    /// </summary>
    private void DoQuery()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<QueryListBean>();
        var viewData = new EmpWorkTimeModel();

        if (_IsBoss)
        {
            if (ddlCompID.SelectedValue == "")
            {
                gvMain.DataSource = null;
                gvMain.DataBind();
                return;
            }

            viewData.CompID = ddlCompID.SelectedValue;
            viewData.OrganID = GetOrganWhere();
            viewData.FlowOrganID = GetFlowOrganWhere();
            viewData.EmpID = txtEmpID.Text.Trim();
            viewData.EmpName = txtEmpName.Text.Trim();
            //viewData.AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");
        }
        else
        {
            viewData.CompID = UserInfo.getUserInfo().CompID;
            viewData.EmpID = UserInfo.getUserInfo().UserID;
        }

        isSuccess = WorkTime.LoadEmpWorkTimeGridData(viewData, out datas, out msg);
        if (msg != "")
        {
            Util.MsgBox(msg);
            gvMain.DataSource = null;
            gvMain.DataBind();
            return;
        }
        if (isSuccess && datas != null)
        {
            gvMain.DataSource = datas;
            gvMain.DataBind();
        }
    }

    /// <summary>
    /// 清除邏輯
    /// </summary>
    private void DoClear()
    {
        Response.Redirect("EmpWorkTimeQuery.aspx");
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
            LoadData(); //載入資料
        }
    }
    #endregion

    #region "5. 畫面事件"
    /// <summary>
    /// ddlCompID_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompID_Changed(object sender, EventArgs e)
    {
        ddlQryType.SelectedIndex = 0;
        ddlQryType.Items[1].Enabled = false;
        plOrganID.Visible = true;
        plFlowOrganID.Visible = false;

        if (ddlCompID.SelectedValue == "")
        {
            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRoleCode40);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);

            _AllowOrgan = new List<OrganListMobel>();
            _AllowFlowOrgan = new List<FlowOrganListMobel>();
            _AllowFlowOrganView = new List<FlowOrganListMobel>();
        }
        else
        {
            if (ddlCompID.SelectedValue == "SPHBK1")
            {
                ddlQryType.Items[1].Enabled = true;
            }

            LoadOrgType();
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);

            LoadRoleCode40();
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);
        }
    }

    /// <summary>
    /// ddlQryType_Changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlQryType_Changed(object sender, EventArgs e)
    {
        if (ddlQryType.SelectedValue == "1")
        {
            plOrganID.Visible = true;
            plFlowOrganID.Visible = false;
        }
        else if (ddlQryType.SelectedValue == "2")
        {
            plOrganID.Visible = false;
            plFlowOrganID.Visible = true;

        }
        ddlRoleCode40.SelectedIndex = 0;
        resetDDL(ddlRoleCode30);
        resetDDL(ddlRoleCode20);
        resetDDL(ddlRoleCode10);

        ddlOrgType.SelectedIndex = 0;
        resetDDL(ddlDeptID);
        resetDDL(ddlOrganID);
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

    /// <summary>
    /// gvMain_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            hidCompID.Value = gvMain.DataKeys[index].Values["CompID"].ToString();
            hidEmpID.Value = gvMain.DataKeys[index].Values["EmpID"].ToString();

            ClientScript.RegisterStartupScript(this.GetType(), "submit", "document.getElementById('btnSubmit').click();", true);
        }
    }
    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        LoadComp();

        if (_IsBoss)
        {
            ddlQryType.SelectedIndex = 0;
            plOrganID.Visible = true;
            plFlowOrganID.Visible = false;

            if (ddlCompID.Items.Count > 1)
            {
                ddlCompID.Items.Insert(0, new ListItem("---請選擇---", ""));
                ddlQryType.Items[1].Enabled = false;

                resetDDL(ddlOrgType);
                resetDDL(ddlRoleCode40);
            }
            else
            {
                if (ddlCompID.SelectedValue != "SPHBK1")
                {
                    ddlQryType.Items[1].Enabled = false;
                }

                LoadOrgType();
                LoadRoleCode40();
            }
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);
        }
    }

    private void LoadComp()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<DropDownListMobel>();
        var viewData = new WorkTimeViewModel()
        {
            UserComp = UserInfo.getUserInfo().CompID,
            UserID = UserInfo.getUserInfo().UserID
        };

        isSuccess = WorkTime.LoadBothComp(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            ddlCompID.DataSource = datas;
            ddlCompID.DataTextField = "DataText";
            ddlCompID.DataValueField = "DataValue";
            ddlCompID.DataBind();

            _IsBoss = true;
            plPer.Visible = false;
        }
        else
        {
            _IsBoss = false;
            plOrg.Visible = false;
            txtCompID.Text = UserInfo.getUserInfo().CompName;
            txtDeptID.Text = UserInfo.getUserInfo().DeptName;
            txtOrganID.Text = UserInfo.getUserInfo().OrganName;

            DoQuery();
        }
    }

    private void LoadOrgType()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<OrganListMobel>();
        var viewData = new WorkTimeViewModel()
        {
            UserComp = UserInfo.getUserInfo().CompID,
            UserID = UserInfo.getUserInfo().UserID,
            CompID = ddlCompID.SelectedValue
        };

        isSuccess = WorkTime.LoadOrgan(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            _AllowOrgan = datas;

            ddlOrgType.DataSource = datas.Select(x => new { x.OrgType, x.OrgTypeName }).Distinct().ToList();
            ddlOrgType.DataTextField = "OrgTypeName";
            ddlOrgType.DataValueField = "OrgType";
            ddlOrgType.DataBind();
            ddlOrgType.Items.Insert(0, new ListItem("---請選擇---", ""));
        }
        else
        {
            _AllowOrgan = new List<OrganListMobel>();
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
            ddlDeptID.Items.Insert(0, new ListItem("---請選擇---", ""));
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
        ddlOrganID.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    private void LoadRoleCode40()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<FlowOrganListMobel>();
        var viewData = new WorkTimeViewModel()
        {
            UserComp = UserInfo.getUserInfo().CompID,
            UserID = UserInfo.getUserInfo().UserID,
            CompID = ddlCompID.SelectedValue,
            FlowType = "D"
        };

        isSuccess = WorkTime.LoadFlowOrgan(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            _AllowFlowOrgan = datas;

            viewData = new WorkTimeViewModel()
            {
                UserComp = UserInfo.getUserInfo().CompID,
                UserID = UserInfo.getUserInfo().UserID,
                CompID = ddlCompID.SelectedValue,
                FlowType = "B"
            };

            isSuccess = WorkTime.LoadFlowOrgan(viewData, out datas, out msg);
            if (isSuccess && datas != null && datas.Count > 0)
            {
                _AllowFlowOrganView = datas;
                ddlRoleCode40.DataSource = datas.Where(x => x.RoleCode == "40").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
                ddlRoleCode40.DataTextField = "OrganName";
                ddlRoleCode40.DataValueField = "OrganID";
                ddlRoleCode40.DataBind();
                ddlRoleCode40.Items.Insert(0, new ListItem("---請選擇---", ""));
            }
            else
            {
                _AllowFlowOrganView = new List<FlowOrganListMobel>();
                resetDDL(ddlRoleCode40);
                resetDDL(ddlRoleCode30);
                resetDDL(ddlRoleCode20);
                resetDDL(ddlRoleCode10);
            }
        }
        else
        {
            _AllowFlowOrgan = new List<FlowOrganListMobel>();
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
        ddlRoleCode30.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    private void LoadRoleCode20()
    {
        ddlRoleCode20.DataSource = _AllowFlowOrganView.Where(x => x.UpOrganID == ddlRoleCode30.SelectedValue && x.RoleCode == "20").Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
        ddlRoleCode20.DataTextField = "OrganName";
        ddlRoleCode20.DataValueField = "OrganID";
        ddlRoleCode20.DataBind();
        ddlRoleCode20.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    private void LoadRoleCode10()
    {
        ddlRoleCode10.DataSource = _AllowFlowOrganView.Where(x => x.DeptID == ddlRoleCode20.SelectedValue && (x.RoleCode == "10" || x.RoleCode == "0")).Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
        ddlRoleCode10.DataTextField = "OrganName";
        ddlRoleCode10.DataValueField = "OrganID";
        ddlRoleCode10.DataBind();
        ddlRoleCode10.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    /// <summary>
    /// 組OrganID選單
    /// </summary>
    /// <param name="DDL"></param>
    private string GetOrganWhere()
    {
        string orgWhere = "";

        //bool AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");

        //if ((ddlOrgType.SelectedValue != "" || ddlDeptID.SelectedValue != "" || ddlOrganID.SelectedValue != "") || AllSearch)
        if (ddlQryType.SelectedValue == "1")
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

        /// <summary>
    /// 組OrganID選單
    /// </summary>
    /// <param name="DDL"></param>
    private string GetFlowOrganWhere()
    {
        string orgFlowWhere = "";
        //bool AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");

        //if ((ddlRoleCode40.SelectedValue != "" || ddlRoleCode30.SelectedValue != "" || ddlRoleCode20.SelectedValue != "" || ddlRoleCode10.SelectedValue != "") || AllSearch)
        if (ddlQryType.SelectedValue == "2")
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

    /// <summary>
    /// 重設DDL，給予請選擇
    /// </summary>
    /// <param name="DDL"></param>
    private void resetDDL(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Insert(0, new ListItem("---請選擇---", ""));
    }
    #endregion
}