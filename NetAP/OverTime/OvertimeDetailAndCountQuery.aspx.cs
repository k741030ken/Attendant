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

public partial class OverTime_OvertimeDetailAndCountQuery : SecurePage
{
    #region 參數
    public DataTable _ddlOrgTable
    {
        get
        {
            if (Session["_ddlOrgTable"] == null)
            {
                Session["_ddlOrgTable"] = new DataTable();
            }
            return (DataTable)(Session["_ddlOrgTable"]);
        }
        set
        {
            Session["_ddlOrgTable"] = value;
        }
    }

    public DataTable _ddlUnderOrg
    {
        get
        {
            if (Session["_ddlUnderOrg"] == null)
            {
                Session["_ddlUnderOrg"] = new DataTable();
            }
            return (DataTable)(Session["_ddlUnderOrg"]);
        }
        set
        {
            Session["_ddlUnderOrg"] = value;
        }
    }

    public DataTable _ddlOrgFlowTable
    {
        get
        {
            if (Session["_ddlOrgFlowTable"] == null)
            {
                Session["_ddlOrgFlowTable"] = new DataTable();
            }
            return (DataTable)(Session["_ddlOrgFlowTable"]);
        }
        set
        {
            Session["_ddlOrgFlowTable"] = value;
        }
    }

    public DataTable _ddlUnderOrgFlow
    {
        get
        {
            if (Session["_ddlUnderOrgFlow"] == null)
            {
                Session["_ddlUnderOrgFlow"] = new DataTable();
            }
            return (DataTable)(Session["_ddlUnderOrgFlow"]);
        }
        set
        {
            Session["_ddlUnderOrgFlow"] = value;
        }
    }
    #endregion
    
    private bool haveOrNot;
    private string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    //private string _DBName2 = "DB_VacSys";
    private string _hrDBName = "eHRMSDB";
    private DataTable DDLOrganization = new DataTable();
    public static string _eHRMSDB = Util.getAppSetting("app://eHRMSDB_OverTime/");
    private Aattendant at = new Aattendant();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
        TextBox OTDateBegin = (TextBox)txtOTDateBegin.FindControl("txtDate");
        OTDateBegin.AutoPostBack = true;
        OTDateBegin.CausesValidation = true;
        OTDateBegin.TextChanged += OTDateBegin_TextChanged;

        TextBox OTDateEnd = (TextBox)txtOTDateEnd.FindControl("txtDate");
        OTDateEnd.AutoPostBack = true;
        OTDateEnd.CausesValidation = true;
        OTDateEnd.TextChanged += OTDateEnd_TextChanged;

        TextBox OTDateBeginCount = (TextBox)txtOTDateBeginCount.FindControl("txtDate");
        OTDateBeginCount.AutoPostBack = true;
        OTDateBeginCount.CausesValidation = true;
        OTDateBeginCount.TextChanged += OTDateBeginCount_TextChanged;

        TextBox OTDateEndCount = (TextBox)txtOTDateEndCount.FindControl("txtDate");
        OTDateEndCount.AutoPostBack = true;
        OTDateEndCount.CausesValidation = true;
        OTDateEndCount.TextChanged += OTDateEndCount_TextChanged;
    }

    protected void LoadData()
    {
        PanelDetail.Visible = true; //明細查詢與統計查詢欄位不一樣
        PanelCount.Visible = false;
        //公司
        LoadComp();
        DropDownListDefault();

        lblOTEmpID.Text = UserInfo.getUserInfo().UserID;
        lblOTEmpName.Text = UserInfo.getUserInfo().UserName;
        /************行政組織************/

        /************功能組織************/
        
        /************其餘下拉************/
        
    }

    /// <summary>
    /// 載入登入者擔任之主管公司
    /// </summary>
    protected void LoadComp()
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //公司(同一人可能身兼多個主管職位)
        sb.Append(" SELECT C.CompID, C.CompID + '-' + C.CompName AS CompName");
        sb.Append(" FROM " + _eHRMSDB + ".[dbo].[Organization] O");
        sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[Company] C ON O.CompID = C.CompID");
        sb.Append(" WHERE O.InValidFlag = '0' AND O.VirtualFlag = '0'");
        sb.Append(" AND BossCompID = '" + UserInfo.getUserInfo().CompID + "' AND Boss = '" + UserInfo.getUserInfo().UserID + "'");
        sb.Append(" UNION");
        sb.Append(" SELECT C.CompID, C.CompID + '-' + C.CompName AS CompName");
        sb.Append(" FROM " + _eHRMSDB + ".[dbo].[Personal] P");
        sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[Company] C ON P.CompID = C.CompID");
        sb.Append(" WHERE P.CompID = '" + UserInfo.getUserInfo().CompID + "' AND P.EmpID = '" + UserInfo.getUserInfo().UserID + "'");
        DataTable dtComp = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dtComp.Rows.Count == 1)
        {
            FillDDLWithOutChoose(ddlCompID, dtComp, "CompID", "CompName");
        }
        else
        {
            FillDDL(ddlCompID, dtComp, "CompID", "CompName");

            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRoleCode40);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);
            resetDDL(ddlWorkStatus);
            resetDDL(ddlRankIDMIN);
            resetDDL(ddlRankIDMAX);
            resetDDL(ddlTitleMIN);
            resetDDL(ddlTitleMAX);
            resetDDL(ddlPosition);
        }
    }

    #region 下拉選單
    /// <summary>
    /// 載入下拉預設值
    /// </summary>
    protected void DropDownListDefault()
    {
        if (ddlCompID.SelectedValue != "")
        {
            DbHelper db = new DbHelper(_hrDBName);
            CommandHelper sb = db.CreateCommandHelper();
            CommandHelper sbUnder = db.CreateCommandHelper();
            string strSql = "";
            #region 行政組織下拉

            sb.Reset();
            sb.Append(" SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[Organization]");
            sb.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
            sb.Append(" AND BossCompID = '" + UserInfo.getUserInfo().CompID + "' AND Boss = '" + UserInfo.getUserInfo().UserID + "'");
            sb.Append(" AND CompID = '" + ddlCompID.SelectedValue + "'");

            using (DataTable dtOrg = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                if (dtOrg.Rows.Count > 0)
                {
                    strSql = "";
                    for (int i = 0; i < dtOrg.Rows.Count; i++)
                    {
                        strSql += "SELECT * FROM " + _eHRMSDB + ".[dbo].[funGetUnderOrgan] ('" + ddlCompID.SelectedValue + "', '" + dtOrg.Rows[i][0] + "', '')";
                        if (i != (dtOrg.Rows.Count - 1))
                        {
                            strSql += " UNION ";
                        }
                    }
                    sbUnder.Reset();
                    sbUnder.Append(strSql);
                    using (DataTable dtOrgAll = db.ExecuteDataSet(sbUnder.BuildCommand()).Tables[0])
                    {
                        _ddlUnderOrg = dtOrgAll;
                    }

                    sb.Reset();
                    sb.Append(" SELECT O.OrgType, O.OrgType + '-' + O2.OrganName AS OrgTypeName");
                    sb.Append(", O.DeptID, O.DeptID + '-' + O3.OrganName AS DeptName");
                    sb.Append(", O.OrganID, O.OrganID + '-' + O.OrganName AS OrganName");
                    sb.Append(" FROM " + _eHRMSDB + ".[dbo].[Organization] O");
                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Organization] O2 ON O.CompID = O2.CompID AND O.OrgType = O2.OrganID");
                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Organization] O3 ON O.CompID = O3.CompID AND O.DeptID = O3.OrganID");
                    sb.Append(" WHERE O.CompID = '" + ddlCompID.SelectedValue + "'");
                    sb.Append(" AND O.OrganID IN (");
                    sb.Append(strSql);
                    sb.Append(")");

                    using (DataTable dtOrgAll = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
                    {
                        _ddlOrgTable = dtOrgAll;
                        FillDDL(ddlOrganID, DataTableFilterSort(dtOrgAll, "DeptID <> OrganID", "OrganID"), "OrganID", "OrganName");
                        FillDDL(ddlDeptID, dtOrgAll.DefaultView.ToTable(true, new string[] { "DeptID", "DeptName" }), "DeptID", "DeptName");
                        FillDDL(ddlOrgType, dtOrgAll.DefaultView.ToTable(true, new string[] { "OrgType", "OrgTypeName" }), "OrgType", "OrgTypeName");
                    }
                }
                else
                {
                    resetDDL(ddlOrgType);
                    resetDDL(ddlDeptID);
                    resetDDL(ddlOrganID);
                }
            }
            #endregion

            #region 功能組織下拉
            if (ddlCompID.SelectedValue == "SPHBK1")
            {
                sb.Reset();
                sb.Append(" SELECT BusinessType, OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                sb.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
                sb.Append(" AND BusinessType IN (SELECT Code FROM " + _eHRMSDB + ".[dbo].HRCodeMap WHERE TabName = 'Business' AND FldName = 'BusinessType' AND NotShowFlag = '0')");
                sb.Append(" AND BossCompID = '" + UserInfo.getUserInfo().CompID + "' AND Boss = '" + UserInfo.getUserInfo().UserID + "'");
                sb.Append(" UNION");
                sb.Append(" SELECT BusinessType, OrganID FROM " + _eHRMSDB + ".[dbo].OrganizationFlow");
                sb.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
                sb.Append(" AND BusinessType IN (SELECT Code FROM " + _eHRMSDB + ".[dbo].HRCodeMap WHERE TabName = 'Business' AND FldName = 'BusinessType' AND NotShowFlag = '0')");
                sb.Append(" AND UpOrganID IN (");
                sb.Append(" SELECT OrganID FROM " + _eHRMSDB + ".[dbo].OrganizationFlow");
                sb.Append(" WHERE OrganID IN (");
                sb.Append(" SELECT UpOrganID FROM " + _eHRMSDB + ".[dbo].OrganizationFlow");
                sb.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
                sb.Append(" AND BusinessType IN (SELECT Code FROM " + _eHRMSDB + ".[dbo].HRCodeMap WHERE TabName = 'Business' AND FldName = 'BusinessType' AND NotShowFlag = '0')");
                sb.Append(" AND RoleCode = '40'");
                sb.Append(" )");
                sb.Append(" AND BossCompID = '" + UserInfo.getUserInfo().CompID + "' AND Boss = '" + UserInfo.getUserInfo().UserID + "'");
                sb.Append(" )");

                using (DataTable dtOrg = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
                {
                    if (dtOrg.Rows.Count > 0)
                    {
                        strSql = "";
                        for (int i = 0; i < dtOrg.Rows.Count; i++)
                        {
                            strSql += "SELECT * FROM " + _eHRMSDB + ".[dbo].[funGetUnderOrganFlow] ('" + dtOrg.Rows[i][0] + "', '" + dtOrg.Rows[i][1] + "', 'D')";
                            if (i != (dtOrg.Rows.Count - 1))
                            {
                                strSql += " UNION ";
                            }
                        }
                        sbUnder.Reset();
                        sbUnder.Append(" SELECT BusinessType, RoleCode, UpOrganID, DeptID, OrganID");
                        sbUnder.Append(" , OrganLevel = CASE RoleCode WHEN '10' THEN OrganID WHEN '0' THEN UpOrganID ELSE '' END");
                        sbUnder.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                        sbUnder.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
                        sbUnder.Append(" AND OrganID IN (");
                        sbUnder.Append(strSql);
                        sbUnder.Append(")");
                        using (DataTable dtOrgAll = db.ExecuteDataSet(sbUnder.BuildCommand()).Tables[0])
                        {
                            _ddlUnderOrgFlow = dtOrgAll;
                        }

                        sb.Reset();
                        sb.Append(" SELECT BusinessType, RoleCode, UpOrganID, DeptID, OrganID, CASE WHEN RoleCode = '0' THEN '└─' + OrganID + '-' + OrganName ELSE OrganID + '-' + OrganName END OrganName");
                        sb.Append(" , OrganLevel = CASE RoleCode WHEN '10' THEN OrganID WHEN '0' THEN UpOrganID ELSE '' END");
                        sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                        sb.Append(" WHERE InValidFlag = '0' AND VirtualFlag = '0'");
                        sb.Append(" AND OrganID IN (");
                        for (int i = 0; i < dtOrg.Rows.Count; i++)
                        {
                            sb.Append("SELECT * FROM " + _eHRMSDB + ".[dbo].[funGetUnderOrganFlow] ('" + dtOrg.Rows[i][0] + "', '" + dtOrg.Rows[i][1] + "', 'B')");
                            if (i != (dtOrg.Rows.Count - 1))
                            {
                                sb.Append(" UNION ");
                            }
                        }
                        sb.Append(")");

                        using (DataTable dtOrgAll = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
                        {
                            _ddlOrgFlowTable = dtOrgAll;
                            FillDDL(ddlRoleCode40, DataTableFilterSort(dtOrgAll, "RoleCode = '40'", "BusinessType, OrganID"), "OrganID", "OrganName");
                            FillDDL(ddlRoleCode30, DataTableFilterSort(dtOrgAll, "RoleCode = '30'", "BusinessType, OrganID"), "OrganID", "OrganName");
                            FillDDL(ddlRoleCode20, DataTableFilterSort(dtOrgAll, "RoleCode = '20'", "BusinessType, OrganID"), "OrganID", "OrganName");
                            FillDDL(ddlRoleCode10, DataTableFilterSort(dtOrgAll, "RoleCode IN ('10', '0')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
                        }
                    }
                    else
                    {
                        resetDDL(ddlRoleCode40);
                        resetDDL(ddlRoleCode30);
                        resetDDL(ddlRoleCode20);
                        resetDDL(ddlRoleCode10);
                    }
                }
            }
            #endregion

            //任職狀況
            sb.Reset();
            sb.Append("SELECT WorkCode, Remark AS WorkStatusName");
            sb.Append(" FROM " + _eHRMSDB + ".[dbo].[WorkStatus]");
            DataTable dtWorkStatus = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            FillDDL(ddlWorkStatus, dtWorkStatus, "WorkCode", "WorkStatusName");

            //職等
            sb.Reset();
            sb.Append("SELECT DISTINCT RTrim(RankID) AS RankID FROM " + _eHRMSDB + ".[dbo].[Title]");
            sb.Append(" WHERE CompID='" + ddlCompID.SelectedValue + "'");
            sb.Append(" ORDER BY RankID");
            DataTable dtRank = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            FillDDL(ddlRankIDMIN, dtRank, "RankID", "RankID");
            FillDDL(ddlRankIDMAX, dtRank, "RankID", "RankID");

            resetDDL(ddlTitleMIN);
            resetDDL(ddlTitleMAX);
        }
    }

    /// <summary>
    /// 職等MIN
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRankIDMIN_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlRankIDMIN.SelectedValue != "")
        {
            DbHelper db = new DbHelper(_hrDBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Append("SELECT TitleID, TitleName FROM " + _eHRMSDB + ".[dbo].[Title]");
            sb.Append(" WHERE CompID='" + ddlCompID.SelectedValue + "' AND RankID = '" + ddlRankIDMIN.SelectedValue + "' ");
            sb.Append(" ORDER BY TitleID");
            using (DataTable dtTitle = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                FillDDL(ddlTitleMIN, dtTitle, "TitleID", "TitleName");
            }
            ChkRankID();
        }
        else
        {
            resetDDL(ddlTitleMIN);
        }
    }

    /// <summary>
    /// 職等MAX
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRankIDMAX_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRankIDMAX.SelectedValue != "")
        {
            DbHelper db = new DbHelper(_hrDBName);
            CommandHelper sb = db.CreateCommandHelper();
            sb.Append("SELECT TitleID, TitleName FROM " + _eHRMSDB + ".[dbo].[Title]");
            sb.Append(" WHERE CompID='" + ddlCompID.SelectedValue + "' AND RankID = '" + ddlRankIDMAX.SelectedValue + "' ");
            sb.Append(" ORDER BY TitleID");
            using (DataTable dtTitle = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                FillDDL(ddlTitleMAX, dtTitle, "TitleID", "TitleName");
            }
            ChkRankID();
        }
        else
        {
            resetDDL(ddlTitleMAX);
        }
    }

    /// <summary>
    /// 職稱MIN
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTitleMIN_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChkTitleID();
    }

    /// <summary>
    /// 職稱MAX
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTitleMAX_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChkTitleID();
    }

    /// <summary>
    /// RoleCode='40'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode40_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoleCode40.SelectedValue != "")
        {
            FillDDL(ddlRoleCode30, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '30' AND UpOrganID = '" + ddlRoleCode40.SelectedValue + "'", "BusinessType, OrganID"), "OrganID", "OrganName");

            string orgid30 = string.Join("', '", ddlRoleCode30.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode20, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '20' AND UpOrganID IN ('" + orgid30 + "')", "BusinessType, OrganID"), "OrganID", "OrganName");

            string orgid20 = string.Join("', '", ddlRoleCode20.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0') AND DeptID IN ('" + orgid20 + "')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
        else
        {
            FillDDL(ddlRoleCode30, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '30'", "BusinessType, OrganID"), "OrganID", "OrganName");
            FillDDL(ddlRoleCode20, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '20'", "BusinessType, OrganID"), "OrganID", "OrganName");
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
    }

    /// <summary>
    /// RoleCode='30'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode30_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoleCode30.SelectedValue != "")
        {
            FillDDL(ddlRoleCode20, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '20' AND UpOrganID = '" + ddlRoleCode30.SelectedValue + "'", "BusinessType, OrganID"), "OrganID", "OrganName");

            string orgid20 = string.Join("', '", ddlRoleCode20.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0') AND DeptID IN ('" + orgid20 + "')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
        else
        {
            string orgid30 = string.Join("', '", ddlRoleCode30.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode20, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode = '20' AND UpOrganID IN ('" + orgid30 + "')", "BusinessType, OrganID"), "OrganID", "OrganName");

            string orgid20 = string.Join("', '", ddlRoleCode20.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0') AND DeptID IN ('" + orgid20 + "')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
    }

    /// <summary>
    /// RoleCode='20'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRoleCode20_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoleCode20.SelectedValue != "")
        {
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0') AND DeptID = '" + ddlRoleCode20.SelectedValue + "'", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
        else
        {
            string orgid20 = string.Join("', '", ddlRoleCode20.Items.Cast<ListItem>().Select(i => i.Value));
            FillDDL(ddlRoleCode10, DataTableFilterSort(_ddlOrgFlowTable, "RoleCode IN ('10', '0') AND DeptID IN ('" + orgid20 + "')", "BusinessType, OrganLevel, RoleCode DESC"), "OrganID", "OrganName");
        }
    }

    protected void ddlQueryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQueryType.SelectedValue == "1")
        {
            PanelDetail.Visible = true;
            PanelCount.Visible = false;
            DataTable temp = null;
            gvMain.DataSource = temp;
            gvMain.DataBind();
            //gvMain.Visible = true;
            gvMain2.Visible = false;
            PanelAssignTo.Visible = true;
        }
        else if (ddlQueryType.SelectedValue == "2")
        {
            PanelDetail.Visible = false;
            PanelCount.Visible = true;
            gvMain.Visible = false;
            DataTable temp = null;
            gvMain2.DataSource = temp;
            gvMain2.DataBind();
            //gvMain2.Visible = true;
            PanelAssignTo.Visible = false;
        }
        chkAssignTo_CheckedChanged(null, null);
    }
    #endregion

    #region 按鈕
    /// <summary>
    /// 查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper ch = db.CreateCommandHelper();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        //依簽核人員            
        if (PanelAssignTo.Visible == true && chkAssignTo.Checked)
        {
            gvMain.Enabled = true;
            gvMain.Visible = true;
            gvMain2.Enabled = false;
            gvMain2.Visible = false;

            sb.Clear();
            //sb.AppendLine("SELECT PD.CompID OTCompID,PD.EmpID OTEmpID,PD.NameN OTNameN,'' OTDate, '' OTTime,D.OTStartDate+'~'+ISNULL(D2.OTEndDate,D.OTEndDate) AfterOTDate,STUFF(D.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(D2.OTEndTime,D.OTEndTime),3,0,':') AfterOTTime,'' OTTxnIDBefore,'' OTSeqNoBefore, '' OTTypeID, '' OTReasonMemo, '' AS OTStatus, D.OTTxnID OTTxnIDAfter,D.OTSeqNo OTSeqNoAfter, AT.CodeCName AfterOTTypeID, D.OTReasonMemo AfterOTReasonMemo, CASE D.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END AS AfterOTStatus");
            sb.AppendLine("SELECT PD.CompID OTCompID, PD.EmpID OTEmpID, PD.NameN OTNameN");
            sb.AppendLine(", '' OTDate, '' OTTime");
            sb.AppendLine(", '' OTTxnIDBefore");
            sb.AppendLine(", '' OTSeqNoBefore");
            sb.AppendLine(", '' FlowCaseIDBefore");
            sb.AppendLine(", '' OTTypeID, '' OTReasonMemo");
            sb.AppendLine(", '' AS OTStatus");
            sb.AppendLine(", D.OTStartDate+'~'+ISNULL(D2.OTEndDate,D.OTEndDate) AfterOTDate");
            sb.AppendLine(", STUFF(D.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(D2.OTEndTime,D.OTEndTime),3,0,':') AfterOTTime");
            sb.AppendLine(", D.OTTxnID OTTxnIDAfter");
            sb.AppendLine(", D.OTSeqNo OTSeqNoAfter");
            sb.AppendLine(", D.FlowCaseID FlowCaseIDAfter");
            sb.AppendLine(", AT.CodeCName AfterOTTypeID");
            sb.AppendLine(", D.OTReasonMemo AfterOTReasonMemo");
            sb.AppendLine(", AfterOTStatus = CASE D.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END");
            sb.AppendLine(", '2' As GroupType");
            sb.AppendLine("FROM " + flow.FlowCustDB + "FlowFullLog AOL");
            sb.AppendLine("LEFT JOIN OverTimeDeclaration D ON AOL.FlowCaseID=D.FlowCaseID");
            sb.AppendLine("LEFT JOIN OverTimeDeclaration D2 ON D.OTTxnID=D2.OTTxnID AND D2.OTSeqNo='2'");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal PD ON D.OTCompID=PD.CompID AND D.OTEmpID=PD.EmpID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..RankMapping AS RM ON PD.CompID=RM.CompID AND PD.RankID=RM.RankID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Title AS TD ON PD.CompID=TD.CompID AND PD.RankID=TD.RankID AND PD.TitleID=TD.TitleID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosD ON D.OTCompID=EmpPosD.CompID AND D.OTEmpID=EmpPosD.EmpID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Position PosD ON D.OTCompID=PosD.CompID AND EmpPosD.CompID = PosD.CompID AND PosD.PositionID = EmpPosD.PositionID AND PosD.InValidFlag='0'");
            sb.AppendLine("LEFT JOIN AT_CodeMap AS AT ON AT.TabName='OverTime' and AT.FldName='OverTimeType' AND AT.Code=D.OTTypeID");
            sb.AppendLine("WHERE D.OTSeqNo<>'2' AND (AOL.AssignTo = '" + UserInfo.getUserInfo().UserID + "' OR AOL.ToUser = '" + UserInfo.getUserInfo().UserID + "') AND AOL.FlowStepID IN ('A10', 'A20','A30','A40') AND AOL.FlowStepBtnID NOT IN ('btnCancel', 'FlowReassign')");
            //D.OTFromAdvanceTxnId='' AND 
            //公司
            if (ddlCompID.SelectedValue != "")
            {
                sb.AppendLine(" AND D.OTCompID='" + ddlCompID.SelectedValue + "'");
            }
            //員編
            if (txtEmpID.Text != "")
            {
                sb.AppendLine(" AND D.OTEmpID='" + txtEmpID.Text + "'");
            }
            //姓名
            if (txtEmpName.Text != "")
            {
                sb.AppendLine(" AND PD.NameN LIKE N'%" + txtEmpName.Text + "%'");
            }
            //任職狀況
            if (ddlWorkStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND PD.WorkStatus='" + ddlWorkStatus.SelectedValue + "' ");
            }
            //職等+職稱
            if (ddlRankIDMIN.SelectedValue != "")
            {
                if (ddlTitleMIN.SelectedValue != "")
                {
                    sb.AppendLine(" AND RM.RankIDMap + PD.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                }
                else
                {
                    sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                }
            }
            if (ddlRankIDMAX.SelectedValue != "")
            {
                if (ddlTitleMAX.SelectedValue != "")
                {
                    sb.AppendLine(" AND RM.RankIDMap + PD.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                }
                else
                {
                    sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                }
            }
            //職位
            if (ddlPosition.SelectedValue != "")
            {
                sb.AppendLine(" AND EmpPosD.PositionID='" + ddlPosition.SelectedValue + "'");
            }
            //事前申請表單狀態
            if (ddlOTStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND 1 <> 1");
            }
            //事後申報表單狀態
            if (ddlAfterOTStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND D.OTStatus = '" + ddlAfterOTStatus.SelectedValue + "'");
            }
            if (txtFormNO.Text != "")
            {
                sb.AppendLine(" AND D.OTFormNO = '" + txtFormNO.Text + "'");
            }
            //加班日期
            if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
            {
                sb.AppendLine(" AND (ISNULL(D2.OTEndDate,D.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' AND (D.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' OR D2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')) ");
            }
            else if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate == "")
            {
                sb.AppendLine(" AND (D.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')");
            }
            else if (txtOTDateBegin.ucSelectedDate == "" && txtOTDateEnd.ucSelectedDate != "")
            {
                sb.AppendLine(" AND (ISNULL(D2.OTEndDate,D.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "')");
            }
            //計薪年月
            if (txtOTPayDate.Text != "")
            {
                if (txtOTPayDate.Text == "0")
                {
                    sb.AppendLine(" AND 1 <> 1 ");
                }
                else
                {
                    sb.AppendLine(" AND D.OTPayDate='" + txtOTPayDate.Text + "' ");
                }
            }
            sb.AppendLine(" UNION");
            //sb.AppendLine(" SELECT PA.CompID OTCompID,PA.EmpID OTEmpID,PA.NameN OTNameN,AA.OTStartDate+'~'+ISNULL(AA2.OTEndDate,AA.OTEndDate) OTDate,STUFF(AA.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(AA2.OTEndTime,AA.OTEndTime),3,0,':') OTTime,DD.OTStartDate+'~'+ISNULL(DD2.OTEndDate,DD.OTEndDate) AfterOTDate,STUFF(DD.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(DD2.OTEndTime,DD.OTEndTime),3,0,':') AfterOTTime ,AA.OTTxnID OTTxnIDBefore,AA.OTSeqNo OTSeqNoBefore, AT.CodeCName OTTypeID, AA.OTReasonMemo, CASE AA.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' ELSE '' END AS OTStatus, ISNULL(DD.OTFromAdvanceTxnId,'') OTTxnIDAfter,ISNULL(DD.OTSeqNo,'') OTSeqNoAfter, ATD.CodeCName AfterOTTypeID, DD.OTReasonMemo AfterOTReasonMemo, CASE DD.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END AS AfterOTStatus");
            sb.AppendLine("SELECT PA.CompID OTCompID, PA.EmpID OTEmpID, PA.NameN OTNameN");
            sb.AppendLine(", AA.OTStartDate+'~'+ISNULL(AA2.OTEndDate,AA.OTEndDate) OTDate");
            sb.AppendLine(", STUFF(AA.OTStartTime,3,0,':') + '~' + STUFF(ISNULL(AA2.OTEndTime,AA.OTEndTime),3,0,':') OTTime");
            sb.AppendLine(", AA.OTTxnID OTTxnIDBefore");
            sb.AppendLine(", AA.OTSeqNo OTSeqNoBefore");
            sb.AppendLine(", AA.FlowCaseID FlowCaseIDBefore");
            sb.AppendLine(", AT.CodeCName OTTypeID");
            sb.AppendLine(", AA.OTReasonMemo");
            sb.AppendLine(", OTStatus = CASE AA.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' ELSE '' END");
            sb.AppendLine(", DD.OTStartDate+'~'+ISNULL(DD2.OTEndDate,DD.OTEndDate) AfterOTDate");
            sb.AppendLine(", STUFF(DD.OTStartTime,3,0,':') + '~' + STUFF(ISNULL(DD2.OTEndTime,DD.OTEndTime),3,0,':') AfterOTTime");
            sb.AppendLine(", ISNULL(DD.OTTxnID,'') OTTxnIDAfter");
            sb.AppendLine(", ISNULL(DD.OTSeqNo,'') OTSeqNoAfter");
            sb.AppendLine(", ISNULL(DD.FlowCaseID,'') FlowCaseIDAfter");
            sb.AppendLine(", ATD.CodeCName AfterOTTypeID");
            sb.AppendLine(", DD.OTReasonMemo AfterOTReasonMemo");
            sb.AppendLine(", AfterOTStatus = CASE DD.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END");
            sb.AppendLine(", GroupType = CASE WHEN DD.OTFromAdvanceTxnId IS NULL THEN '1' ELSE '3' END");
            sb.AppendLine("FROM " + flow.FlowCustDB + "FlowFullLog AOL");
            sb.AppendLine("LEFT JOIN OverTimeAdvance AA ON AOL.FlowCaseID=AA.FlowCaseID");
            sb.AppendLine("LEFT JOIN OverTimeAdvance AA2 ON AA.OTTxnID=AA2.OTTxnID AND AA2.OTSeqNo='2'");
            sb.AppendLine("LEFT JOIN OverTimeDeclaration DD ON AA.OTTxnID=DD.OTFromAdvanceTxnId AND AA.OTSeqNo=DD.OTSeqNo");
            sb.AppendLine("LEFT JOIN OverTimeDeclaration DD2 ON DD.OTTxnID=DD2.OTTxnID AND DD2.OTSeqNo='2'");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal PA ON AA.OTCompID=PA.CompID AND AA.OTEmpID=PA.EmpID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..RankMapping AS RM ON PA.CompID=RM.CompID AND PA.RankID=RM.RankID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Title AS TA ON PA.CompID=TA.CompID AND PA.RankID=TA.RankID AND PA.TitleID=TA.TitleID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosA ON AA.OTCompID=EmpPosA.CompID AND AA.OTEmpID=EmpPosA.EmpID");
            sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Position PosA ON AA.OTCompID=PosA.CompID AND EmpPosA.CompID = PosA.CompID AND PosA.PositionID = EmpPosA.PositionID AND PosA.InValidFlag='0'");
            sb.AppendLine("LEFT JOIN AT_CodeMap AS AT ON AT.TabName='OverTime' AND AT.FldName='OverTimeType' AND AT.Code=AA.OTTypeID");
            sb.AppendLine("LEFT JOIN AT_CodeMap AS ATD ON ATD.TabName='OverTime' AND ATD.FldName='OverTimeType' AND ATD.Code=DD.OTTypeID");
            sb.AppendLine("WHERE AA.OTSeqNo<>'2' AND (AOL.AssignTo = '" + UserInfo.getUserInfo().UserID + "' OR AOL.ToUser = '" + UserInfo.getUserInfo().UserID + "') AND AOL.FlowStepID IN ('A10', 'A20','A30','A40') AND AOL.FlowStepBtnID NOT IN ('btnCancel', 'FlowReassign')");
            //公司
            if (ddlCompID.SelectedValue != "")
            {
                sb.AppendLine(" AND AA.OTCompID='" + ddlCompID.SelectedValue + "'");
            }
            if (txtEmpID.Text != "")
            {
                sb.AppendLine(" AND AA.OTEmpID='" + txtEmpID.Text + "'");
            }
            if (txtEmpName.Text != "")
            {
                sb.AppendLine(" AND PA.NameN LIKE '%" + txtEmpName.Text + "%'");
            }
            //任職狀況
            if (ddlWorkStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND PA.WorkStatus='" + ddlWorkStatus.SelectedValue + "' ");
            }
            //職等+職稱
            if (ddlRankIDMIN.SelectedValue != "")
            {
                if (ddlTitleMIN.SelectedValue != "")
                {
                    sb.AppendLine(" AND RM.RankIDMap + PA.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                }
                else
                {
                    sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                }
            }
            if (ddlRankIDMAX.SelectedValue != "")
            {
                if (ddlTitleMAX.SelectedValue != "")
                {
                    sb.AppendLine(" AND RM.RankIDMap + PA.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                }
                else
                {
                    sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                }
            }
            //職位
            if (ddlPosition.SelectedValue != "")
            {
                sb.AppendLine(" AND EmpPosA.PositionID='" + ddlPosition.SelectedValue + "'");
            }
            //事前申請表單狀態
            if (ddlOTStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND AA.OTStatus = '" + ddlOTStatus.SelectedValue + "'");
            }
            //事後申報表單狀態
            if (ddlAfterOTStatus.SelectedValue != "")
            {
                sb.AppendLine(" AND DD.OTStatus = '" + ddlAfterOTStatus.SelectedValue + "'");
            }
            if (txtFormNO.Text != "")
            {
                sb.AppendLine(" AND AA.OTFormNO = '" + txtFormNO.Text + "'");
            }
            if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
            {
                sb.AppendLine(" AND (ISNULL(AA2.OTEndDate,AA.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' AND (AA.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' OR AA2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')) ");
            }
            else if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate == "")
            {
                sb.AppendLine(" AND AA.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' ");
            }
            else if (txtOTDateBegin.ucSelectedDate == "" && txtOTDateEnd.ucSelectedDate != "")
            {
                sb.AppendLine(" AND ISNULL(AA2.OTEndDate,AA.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' ");
            }
            //計薪年月
            if (txtOTPayDate.Text != "")
            {
                if (txtOTPayDate.Text == "0")
                {
                    sb.AppendLine(" AND 1 <> 1 ");
                }
                else
                {
                    sb.AppendLine(" AND DD.OTPayDate='" + txtOTPayDate.Text + "' ");
                }
            }
            sb.AppendLine("ORDER BY GroupType, OTCompID, OTEmpID, OTDate, OTTime");
            ch.Reset();
            ch.AppendStatement(sb.ToString());
            gvMain.DataSource = db.ExecuteDataSet(ch.BuildCommand()).Tables[0];
            gvMain.DataBind();
            gvMain.Visible = true;
        }
        else
        {
            string orgWhere = "";
            string orgFlowWhere = "";
            bool AllSearch = (ddlOrgType.SelectedValue == "" && ddlDeptID.SelectedValue == "" && ddlOrganID.SelectedValue == "" && ddlRoleCode40.SelectedValue == "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "");
            if ((ddlOrgType.SelectedValue != "" || ddlDeptID.SelectedValue != "" || ddlOrganID.SelectedValue != "") || AllSearch)
            {
                if (ddlOrganID.SelectedValue != "")
                {
                    orgWhere += "', '" + ddlOrganID.SelectedValue;
                }
                else if (ddlDeptID.SelectedValue != "" && ddlOrganID.SelectedValue == "")
                {
                    orgWhere += string.Join("', '", ddlOrganID.Items.Cast<ListItem>().Select(i => i.Value));
                    if (_ddlUnderOrg.AsEnumerable().Any(row => ddlDeptID.SelectedValue == row.Field<String>("OrganID")))
                    {
                        orgWhere += "', '" + ddlDeptID.SelectedValue;
                    }
                }
                else
                {
                    orgWhere += string.Join("', '", ddlOrganID.Items.Cast<ListItem>().Select(i => i.Value));
                    foreach (ListItem list in ddlDeptID.Items)
                    {
                        if (_ddlUnderOrg.AsEnumerable().Any(row => list.Value == row.Field<String>("OrganID")))
                        {
                            orgWhere += "', '" + list.Value;
                        }
                    }
                }
            }

            if ((ddlRoleCode40.SelectedValue != "" || ddlRoleCode30.SelectedValue != "" || ddlRoleCode20.SelectedValue != "" || ddlRoleCode10.SelectedValue != "") || AllSearch)
            {
                if (ddlRoleCode10.SelectedValue != "")
                {
                    if (ddlRoleCode10.SelectedItem.Text.StartsWith("└"))
                    {
                        orgFlowWhere += "', '" + ddlRoleCode10.SelectedValue;
                    }
                    else
                    {
                        foreach (DataRow dr in _ddlUnderOrgFlow.Rows)
                        {
                            if (dr["OrganLevel"].ToString() == ddlRoleCode10.SelectedValue)
                            {
                                orgFlowWhere += "', '" + dr["OrganID"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in _ddlUnderOrgFlow.Rows)
                    {
                        if (dr["RoleCode"].ToString().Trim() == "0" || dr["RoleCode"].ToString() == "10")
                        {
                            orgFlowWhere += "', '" + dr["OrganID"].ToString();
                        }
                    }

                    if (ddlRoleCode20.SelectedValue != "")
                    {
                        if (_ddlUnderOrgFlow.AsEnumerable().Any(row => ddlRoleCode20.SelectedValue == row.Field<String>("OrganID")))
                        {
                            orgFlowWhere += "', '" + ddlRoleCode20.SelectedValue;
                        }
                    }
                    else
                    {
                        foreach (ListItem list in ddlRoleCode20.Items)
                        {
                            if (_ddlUnderOrgFlow.AsEnumerable().Any(row => list.Value == row.Field<String>("OrganID")))
                            {
                                orgFlowWhere += "', '" + list.Value;
                            }
                        }
                    }

                    if (ddlRoleCode30.SelectedValue != "")
                    {
                        if (_ddlUnderOrgFlow.AsEnumerable().Any(row => ddlRoleCode30.SelectedValue == row.Field<String>("OrganID")))
                        {
                            orgFlowWhere += "', '" + ddlRoleCode30.SelectedValue;
                        }
                    }
                    else
                    {
                        foreach (ListItem list in ddlRoleCode30.Items)
                        {
                            if (_ddlUnderOrgFlow.AsEnumerable().Any(row => list.Value == row.Field<String>("OrganID")))
                            {
                                orgFlowWhere += "', '" + list.Value;
                            }
                        }
                    }

                    if (ddlRoleCode40.SelectedValue != "")
                    {
                        if (_ddlUnderOrgFlow.AsEnumerable().Any(row => ddlRoleCode40.SelectedValue == row.Field<String>("OrganID")))
                        {
                            orgFlowWhere += "', '" + ddlRoleCode40.SelectedValue;
                        }
                    }
                    else
                    {
                        foreach (ListItem list in ddlRoleCode40.Items)
                        {
                            if (_ddlUnderOrgFlow.AsEnumerable().Any(row => list.Value == row.Field<String>("OrganID")))
                            {
                                orgFlowWhere += "', '" + list.Value;
                            }
                        }
                    }
                }
            }


            if (ddlQueryType.SelectedValue == "1") //明細
            {
                gvMain.Enabled = true;
                gvMain.Visible = true;
                gvMain2.Enabled = false;
                gvMain2.Visible = false;

                sb.Clear();
                //sb.AppendLine("SELECT D.OTCompID,D.OTEmpID,PD.NameN OTNameN,'' OTDate, '' OTTime,D.OTStartDate+'~'+ISNULL(D2.OTEndDate,D.OTEndDate) AfterOTDate,STUFF(D.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(D2.OTEndTime,D.OTEndTime),3,0,':') AfterOTTime,'' OTTxnIDBefore,'' OTSeqNoBefore,");
                //sb.AppendLine(" '' OTTypeID,");
                //sb.AppendLine(" '' OTReasonMemo,");
                //sb.AppendLine(" '' AS OTStatus,");
                //sb.AppendLine(" D.OTTxnID OTTxnIDAfter,D.OTSeqNo OTSeqNoAfter,");
                //sb.AppendLine(" AT.CodeCName AfterOTTypeID,");
                //sb.AppendLine(" D.OTReasonMemo AfterOTReasonMemo,");
                //sb.AppendLine(" CASE D.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END AS AfterOTStatus");
                //sb.AppendLine(" FROM OverTimeDeclaration D");
                //sb.AppendLine(" LEFT JOIN OverTimeDeclaration D2 ON D.OTTxnID=D2.OTTxnID AND D2.OTSeqNo='2'");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Personal PD ON D.OTCompID=PD.CompID AND D.OTEmpID=PD.EmpID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..RankMapping AS RM ON PD.CompID=RM.CompID AND PD.RankID=RM.RankID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Title AS TD ON PD.CompID=TD.CompID AND PD.RankID=TD.RankID AND PD.TitleID=TD.TitleID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosD ON D.OTCompID=EmpPosD.CompID AND D.OTEmpID=EmpPosD.EmpID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Position PosD ON D.OTCompID=PosD.CompID AND EmpPosD.CompID = PosD.CompID AND PosD.PositionID = EmpPosD.PositionID AND PosD.InValidFlag='0'");
                //sb.AppendLine(" LEFT JOIN AT_CodeMap AS AT ON AT.TabName='OverTime' and AT.FldName='OverTimeType' AND AT.Code=D.OTTypeID");
                //sb.AppendLine(" WHERE D.OTFromAdvanceTxnId='' AND D.OTSeqNo<>'2'");
                //sb.AppendLine(" AND D.OTCompID = '" + ddlCompID.SelectedValue + "'");
                //sb.AppendLine(" AND D.OrganID IN ('" + orgWhere + "') ");

                sb.AppendLine("SELECT D.OTCompID, D.OTEmpID, PD.NameN OTNameN");
                sb.AppendLine(", '' OTDate, '' OTTime");
                sb.AppendLine(", '' OTTxnIDBefore");
                sb.AppendLine(", '' OTSeqNoBefore");
                sb.AppendLine(", '' FlowCaseIDBefore");
                sb.AppendLine(", '' OTTypeID");
                sb.AppendLine(", '' OTReasonMemo");
                sb.AppendLine(", '' AS OTStatus");
                sb.AppendLine(", D.OTStartDate + '~' + ISNULL(D2.OTEndDate, D.OTEndDate) AfterOTDate");
                sb.AppendLine(", STUFF(D.OTStartTime,3,0,':') + '~' + STUFF(ISNULL(D2.OTEndTime,D.OTEndTime),3,0,':') AfterOTTime");
                sb.AppendLine(", D.OTTxnID OTTxnIDAfter");
                sb.AppendLine(", D.OTSeqNo OTSeqNoAfter");
                sb.AppendLine(", D.FlowCaseID FlowCaseIDAfter");
                sb.AppendLine(", AT.CodeCName AfterOTTypeID");
                sb.AppendLine(", D.OTReasonMemo AfterOTReasonMemo");
                sb.AppendLine(", AfterOTStatus = CASE D.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END");
                sb.AppendLine(", '2' As GroupType");
                sb.AppendLine("FROM OverTimeDeclaration D");
                sb.AppendLine("LEFT JOIN OverTimeDeclaration D2 ON D.OTTxnID = D2.OTTxnID AND D2.OTSeqNo = '2'");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal PD ON D.OTCompID = PD.CompID AND D.OTEmpID = PD.EmpID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpFlow ED ON PD.CompID = ED.CompID AND PD.EmpID = ED.EmpID AND ED.ActionID='01'");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..RankMapping RM ON PD.CompID = RM.CompID AND PD.RankID = RM.RankID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Title TD ON PD.CompID = TD.CompID AND PD.RankID = TD.RankID AND PD.TitleID = TD.TitleID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosD ON D.OTCompID = EmpPosD.CompID AND D.OTEmpID = EmpPosD.EmpID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Position PosD ON D.OTCompID = PosD.CompID AND EmpPosD.CompID = PosD.CompID AND PosD.PositionID = EmpPosD.PositionID AND PosD.InValidFlag = '0'");
                sb.AppendLine("LEFT JOIN AT_CodeMap AT ON AT.TabName = 'OverTime' and AT.FldName = 'OverTimeType' AND AT.Code = D.OTTypeID");
                sb.AppendLine("WHERE D.OTFromAdvanceTxnId = '' AND D.OTSeqNo <> '2'");
                sb.AppendLine("AND D.OTCompID = '" + ddlCompID.SelectedValue + "'");
                if (AllSearch)
                {
                    sb.AppendLine("AND (PD.OrganID IN ('" + orgWhere + "') OR ED.OrganID IN ('" + orgFlowWhere + "')) ");
                }
                else
                {
                    if (orgWhere != "")
                    {
                        sb.AppendLine("AND PD.OrganID IN ('" + orgWhere + "') ");
                    }
                    if (orgFlowWhere != "")
                    {
                        sb.AppendLine("AND ED.OrganID IN ('" + orgFlowWhere + "') ");
                    }
                }
                //員編
                if (txtEmpID.Text != "")
                {
                    sb.AppendLine(" AND D.OTEmpID = '" + txtEmpID.Text + "'");
                }
                //姓名
                if (txtEmpName.Text != "")
                {
                    sb.AppendLine(" AND PD.NameN LIKE N'%" + txtEmpName.Text + "%'");
                }
                //任職狀況
                if (ddlWorkStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND PD.WorkStatus = '" + ddlWorkStatus.SelectedValue + "' ");
                }
                //職等+職稱
                if (ddlRankIDMIN.SelectedValue != "")
                {
                    if (ddlTitleMIN.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + PD.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                    }
                }
                if (ddlRankIDMAX.SelectedValue != "")
                {
                    if (ddlTitleMAX.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + PD.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                    }
                }
                //職位
                if (ddlPosition.SelectedValue != "")
                {
                    sb.AppendLine(" AND EmpPosD.PositionID = '" + ddlPosition.SelectedValue + "'");
                }
                //事前申請表單狀態
                if (ddlOTStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND 1 <> 1");
                }
                //事後申報表單狀態
                if (ddlAfterOTStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND D.OTStatus = '" + ddlAfterOTStatus.SelectedValue + "'");
                }
                if (txtFormNO.Text != "")
                {
                    sb.AppendLine(" AND D.OTFormNO = '" + txtFormNO.Text + "'");
                }
                //加班日期
                if (txtOTDateBegin.ucSelectedDate != "")
                {
                    sb.AppendLine(" AND (D.OTStartDate >= '" + txtOTDateBegin.ucSelectedDate + "' OR D2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')");
                }
                if (txtOTDateEnd.ucSelectedDate != "")
                {
                    sb.AppendLine(" AND ISNULL(D2.OTEndDate, D.OTEndDate) <= '" + txtOTDateEnd.ucSelectedDate + "'");
                }

                //if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND (ISNULL(D2.OTEndDate,D.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' AND (D.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' OR D2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')) ");
                //}
                //else if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate == "")
                //{
                //    sb.AppendLine(" AND (D.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')");
                //}
                //else if (txtOTDateBegin.ucSelectedDate == "" && txtOTDateEnd.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND (ISNULL(D2.OTEndDate,D.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "')");
                //}
                //計薪年月
                if (txtOTPayDate.Text != "")
                {
                    if (txtOTPayDate.Text == "0")
                    {
                        sb.AppendLine(" AND 1 <> 1 ");
                    }
                    else
                    {
                        sb.AppendLine(" AND D.OTPayDate='" + txtOTPayDate.Text + "' ");
                    }
                }

                //sb.AppendLine("");
                //sb.AppendLine(" UNION");
                //sb.AppendLine(" SELECT AA.OTCompID,AA.OTEmpID,PA.NameN OTNameN,AA.OTStartDate+'~'+ISNULL(AA2.OTEndDate,AA.OTEndDate) OTDate,STUFF(AA.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(AA2.OTEndTime,AA.OTEndTime),3,0,':') OTTime,DD.OTStartDate+'~'+ISNULL(DD2.OTEndDate,DD.OTEndDate) AfterOTDate,STUFF(DD.OTStartTime,3,0,':')+'~'+STUFF(ISNULL(DD2.OTEndTime,DD.OTEndTime),3,0,':') AfterOTTime ,AA.OTTxnID OTTxnIDBefore,AA.OTSeqNo OTSeqNoBefore,");
                //sb.AppendLine(" AT.CodeCName OTTypeID,");
                //sb.AppendLine(" AA.OTReasonMemo,");
                //sb.AppendLine(" CASE AA.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' ELSE '' END AS OTStatus,");
                //sb.AppendLine(" ISNULL(DD.OTFromAdvanceTxnId,'') OTTxnIDAfter,ISNULL(DD.OTSeqNo,'') OTSeqNoAfter,");
                //sb.AppendLine(" ATD.CodeCName AfterOTTypeID,");
                //sb.AppendLine(" DD.OTReasonMemo AfterOTReasonMemo,");
                //sb.AppendLine(" CASE DD.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END AS AfterOTStatus");
                //sb.AppendLine(" FROM OverTimeAdvance AA");
                //sb.AppendLine(" LEFT JOIN OverTimeAdvance AA2 ON AA.OTTxnID=AA2.OTTxnID AND AA2.OTSeqNo='2'");
                //sb.AppendLine(" LEFT JOIN OverTimeDeclaration DD ON AA.OTTxnID=DD.OTFromAdvanceTxnId AND AA.OTSeqNo=DD.OTSeqNo");
                //sb.AppendLine(" LEFT JOIN OverTimeDeclaration DD2 ON DD.OTTxnID=DD2.OTTxnID AND DD2.OTSeqNo='2'");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Personal PA ON AA.OTCompID=PA.CompID AND AA.OTEmpID=PA.EmpID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..RankMapping AS RM ON PA.CompID=RM.CompID AND PA.RankID=RM.RankID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Title AS TA ON PA.CompID=TA.CompID AND PA.RankID=TA.RankID AND PA.TitleID=TA.TitleID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosA ON AA.OTCompID=EmpPosA.CompID AND AA.OTEmpID=EmpPosA.EmpID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + "..Position PosA ON AA.OTCompID=PosA.CompID AND EmpPosA.CompID = PosA.CompID AND PosA.PositionID = EmpPosA.PositionID AND PosA.InValidFlag='0'");
                //sb.AppendLine(" LEFT JOIN AT_CodeMap AS AT ON AT.TabName='OverTime' and AT.FldName='OverTimeType' AND AT.Code=AA.OTTypeID");
                //sb.AppendLine(" LEFT JOIN AT_CodeMap AS ATD ON ATD.TabName='OverTime' and ATD.FldName='OverTimeType' AND ATD.Code=DD.OTTypeID");
                //sb.AppendLine(" WHERE AA.OTSeqNo<>'2'"); //跨日單合併不顯示迄日單(重複顯示)
                //sb.AppendLine(" AND AA.OTCompID = '" + ddlCompID.SelectedValue + "' ");
                //sb.AppendLine(" AND AA.OrganID IN ('" + orgWhere + "') ");

                sb.AppendLine("UNION");
                sb.AppendLine("SELECT AA.OTCompID, AA.OTEmpID, PA.NameN OTNameN");
                sb.AppendLine(", AA.OTStartDate + '~' + ISNULL(AA2.OTEndDate,AA.OTEndDate) OTDate");
                sb.AppendLine(", STUFF(AA.OTStartTime,3,0,':') + '~' + STUFF(ISNULL(AA2.OTEndTime,AA.OTEndTime),3,0,':') OTTime");
                sb.AppendLine(", AA.OTTxnID OTTxnIDBefore");
                sb.AppendLine(", AA.OTSeqNo OTSeqNoBefore");
                sb.AppendLine(", AA.FlowCaseID FlowCaseIDBefore");
                sb.AppendLine(", AT.CodeCName OTTypeID");
                sb.AppendLine(", AA.OTReasonMemo");
                sb.AppendLine(", OTStatus = CASE AA.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' ELSE '' END");
                sb.AppendLine(", ISNULL(DD.OTStartDate + '~' + ISNULL(DD2.OTEndDate,DD.OTEndDate),'') AfterOTDate");
                sb.AppendLine(", ISNULL(STUFF(DD.OTStartTime,3,0,':') + '~' + STUFF(ISNULL(DD2.OTEndTime,DD.OTEndTime),3,0,':'),'') AfterOTTime");
                sb.AppendLine(", ISNULL(DD.OTFromAdvanceTxnId, '') OTTxnIDAfter");
                sb.AppendLine(", ISNULL(DD.OTSeqNo, '') OTSeqNoAfter");
                sb.AppendLine(", ISNULL(DD.FlowCaseID, '') FlowCaseIDAfter");
                sb.AppendLine(", ISNULL(ATD.CodeCName, '') AfterOTTypeID");
                sb.AppendLine(", ISNULL(DD.OTReasonMemo, '') AfterOTReasonMemo");
                sb.AppendLine(", AfterOTStatus = CASE DD.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '6' THEN '取消' WHEN '7' THEN '作廢' WHEN '9' THEN '計薪後收回' ELSE '' END");
                sb.AppendLine(", GroupType = CASE WHEN DD.OTFromAdvanceTxnId IS NULL THEN '1' ELSE '3' END");
                sb.AppendLine("FROM OverTimeAdvance AA");
                sb.AppendLine("LEFT JOIN OverTimeAdvance AA2 ON AA.OTTxnID = AA2.OTTxnID AND AA2.OTSeqNo = '2'");
                sb.AppendLine("LEFT JOIN OverTimeDeclaration DD ON AA.OTTxnID = DD.OTFromAdvanceTxnId AND AA.OTSeqNo = DD.OTSeqNo");
                sb.AppendLine("LEFT JOIN OverTimeDeclaration DD2 ON DD.OTTxnID = DD2.OTTxnID AND DD2.OTSeqNo = '2'");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal PA ON AA.OTCompID = PA.CompID AND AA.OTEmpID = PA.EmpID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpFlow EA ON PA.CompID = EA.CompID AND PA.EmpID = EA.EmpID AND EA.ActionID='01'");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..RankMapping RM ON PA.CompID = RM.CompID AND PA.RankID = RM.RankID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Title TA ON PA.CompID = TA.CompID AND PA.RankID = TA.RankID AND PA.TitleID = TA.TitleID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpPosition EmpPosA ON AA.OTCompID = EmpPosA.CompID AND AA.OTEmpID = EmpPosA.EmpID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Position PosA ON AA.OTCompID = PosA.CompID AND EmpPosA.CompID = PosA.CompID AND PosA.PositionID = EmpPosA.PositionID AND PosA.InValidFlag = '0'");
                sb.AppendLine("LEFT JOIN AT_CodeMap AS AT ON AT.TabName = 'OverTime' and AT.FldName = 'OverTimeType' AND AT.Code = AA.OTTypeID");
                sb.AppendLine("LEFT JOIN AT_CodeMap AS ATD ON ATD.TabName = 'OverTime' and ATD.FldName = 'OverTimeType' AND ATD.Code = DD.OTTypeID");
                sb.AppendLine("WHERE AA.OTSeqNo <> '2'");
                sb.AppendLine("AND AA.OTCompID = '" + ddlCompID.SelectedValue + "' ");
                if (AllSearch)
                {
                    sb.AppendLine("AND (PA.OrganID IN ('" + orgWhere + "') OR EA.OrganID IN ('" + orgFlowWhere + "')) ");
                }
                else
                {
                    if (orgWhere != "")
                    {
                        sb.AppendLine("AND PA.OrganID IN ('" + orgWhere + "') ");
                    }
                    if (orgFlowWhere != "")
                    {
                        sb.AppendLine("AND EA.OrganID IN ('" + orgFlowWhere + "') ");
                    }
                }
                if (txtEmpID.Text != "")
                {
                    sb.AppendLine(" AND AA.OTEmpID='" + txtEmpID.Text + "'");
                }
                if (txtEmpName.Text != "")
                {
                    sb.AppendLine(" AND PA.NameN LIKE N'%" + txtEmpName.Text + "%'");
                }
                //任職狀況
                if (ddlWorkStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND PA.WorkStatus='" + ddlWorkStatus.SelectedValue + "' ");
                }
                //職等+職稱
                if (ddlRankIDMIN.SelectedValue != "")
                {
                    if (ddlTitleMIN.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + PA.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                    }
                }
                if (ddlRankIDMAX.SelectedValue != "")
                {
                    if (ddlTitleMAX.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + PA.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                    }
                }
                //職位
                if (ddlPosition.SelectedValue != "")
                {
                    sb.AppendLine(" AND EmpPosA.PositionID='" + ddlPosition.SelectedValue + "'");
                }

                //事前申請表單狀態
                if (ddlOTStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND AA.OTStatus = '" + ddlOTStatus.SelectedValue + "'");
                }
                //事後申報表單狀態
                if (ddlAfterOTStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND DD.OTStatus = '" + ddlAfterOTStatus.SelectedValue + "'");
                }

                if (txtFormNO.Text != "")
                {
                    sb.AppendLine(" AND AA.OTFormNO = '" + txtFormNO.Text + "'");
                }

                //加班日期
                if (txtOTDateBegin.ucSelectedDate != "")
                {
                    sb.AppendLine(" AND (AA.OTStartDate >= '" + txtOTDateBegin.ucSelectedDate + "' OR AA2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')");
                }
                if (txtOTDateEnd.ucSelectedDate != "")
                {
                    sb.AppendLine(" AND ISNULL(AA2.OTEndDate, AA.OTEndDate) <= '" + txtOTDateEnd.ucSelectedDate + "'");
                }

                //if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND (ISNULL(AA2.OTEndDate,AA.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' AND (AA.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' OR AA2.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "')) ");
                //}
                //else if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate == "")
                //{
                //    sb.AppendLine(" AND AA.OTStartDate>='" + txtOTDateBegin.ucSelectedDate + "' ");
                //}
                //else if (txtOTDateBegin.ucSelectedDate == "" && txtOTDateEnd.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND ISNULL(AA2.OTEndDate,AA.OTEndDate)<='" + txtOTDateEnd.ucSelectedDate + "' ");
                //}
                //計薪年月
                if (txtOTPayDate.Text != "")
                {
                    if (txtOTPayDate.Text == "0")
                    {
                        sb.AppendLine(" AND 1 <> 1 ");
                    }
                    else
                    {
                        sb.AppendLine(" AND DD.OTPayDate='" + txtOTPayDate.Text + "' ");
                    }
                }
                sb.AppendLine("ORDER BY GroupType, OTCompID, OTEmpID, OTDate, OTTime");
                ch.Reset();
                ch.AppendStatement(sb.ToString());
                gvMain.DataSource = db.ExecuteDataSet(ch.BuildCommand()).Tables[0];
                gvMain.DataBind();
                gvMain.Visible = true;

            }
            else if (ddlQueryType.SelectedValue == "2") //統計
            {
                gvMain2.Enabled = true;
                gvMain2.Visible = true;
                gvMain.Enabled = false;
                gvMain.Visible = false;

                sb.Clear();
                //sb.AppendLine("");
                //sb.AppendLine("SELECT ISNULL(A.OTEmpID, C.OTEmpID) as OTEmpID,ISNULL(A.NameN, C.NameN) AS OTNameN,A.Submit,A.Approval,A.Reject,C.AfterSubmit,C.AfterApproval,C.AfterReject");
                //sb.AppendLine(" FROM ( ");
                //sb.AppendLine(" SELECT Before.OTEmpID, P.NameN,");
                //sb.AppendLine(" SUM(CASE Before.OTStatus WHEN '2' THEN (Before.OTTotalTime-CAST(Before.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS Submit, ");
                //sb.AppendLine(" SUM(CASE Before.OTStatus WHEN '3' THEN (Before.OTTotalTime-CAST(Before.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS Approval, ");
                //sb.AppendLine(" SUM(CASE Before.OTStatus WHEN '4' THEN (Before.OTTotalTime-CAST(Before.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS Reject ");
                //sb.AppendLine(" FROM OverTimeAdvance AS Before");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Personal] AS P ON Before.OTEmpID=P.EmpID AND Before.OTCompID=P.CompID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[RankMapping] AS RM ON P.CompID=RM.CompID AND P.RankID=RM.RankID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Title] AS T ON P.CompID=T.CompID AND P.RankID=T.RankID AND P.TitleID=T.TitleID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[EmpPosition] AS EmpP ON P.CompID=EmpP.CompID AND P.EmpID=EmpP.EmpID");
                //sb.AppendLine(" WHERE Before.OTCompID = '" + ddlCompID.SelectedValue + "'");
                ////sb.AppendLine(" AND Before.OTStatus IN ('2','3','4') ");
                //sb.AppendLine(" AND Before.OrganID IN ('" + orgWhere + "') ");
                //if (txtEmpID.Text!="" && ddlCompID.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P.EmpID='" + txtEmpID.Text + "'");
                //}
                //if (txtEmpName.Text != "" && ddlCompID.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P.NameN LIKE '%" + txtEmpName.Text + "%'");
                //}
                //if (ddlWorkStatus.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P.WorkStatus='" + ddlWorkStatus.SelectedValue + "'");
                //}
                ////職等+職稱
                //if (ddlRankIDMIN.SelectedValue != "")
                //{
                //    if (ddlTitleMIN.SelectedValue != "")
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap + P.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                //    }
                //    else
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                //    }
                //}
                //if (ddlRankIDMAX.SelectedValue != "")
                //{
                //    if (ddlTitleMAX.SelectedValue != "")
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap + P.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                //    }
                //    else
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                //    }
                //}
                //if (ddlPosition.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND EmpP.PositionID='" + ddlPosition.SelectedValue + "'");
                //}
                //if (txtOTDateBeginCount.ucSelectedDate!="" && txtOTDateEndCount.ucSelectedDate!="")
                //{
                //    sb.AppendLine(" AND (Before.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "' AND Before.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "')");
                //}
                //else if (txtOTDateBeginCount.ucSelectedDate != "" && txtOTDateEndCount.ucSelectedDate == "")
                //{
                //    sb.AppendLine(" AND Before.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "'");
                //}
                //else if (txtOTDateBeginCount.ucSelectedDate == "" && txtOTDateEndCount.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND Before.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "'");
                //}
                //sb.AppendLine(" GROUP BY Before.OTEmpID,P.NameN ) A");
                //sb.AppendLine(" FULL OUTER JOIN ( ");
                //sb.AppendLine(" SELECT * FROM ( SELECT AfterOT.OTEmpID,P2.NameN,");
                //sb.AppendLine(" SUM(CASE AfterOT.OTStatus WHEN '2' THEN (AfterOT.OTTotalTime-CAST(AfterOT.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS AfterSubmit, ");
                //sb.AppendLine(" SUM(CASE AfterOT.OTStatus WHEN '3' THEN (AfterOT.OTTotalTime-CAST(AfterOT.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS AfterApproval, ");
                //sb.AppendLine(" SUM(CASE AfterOT.OTStatus WHEN '4' THEN (AfterOT.OTTotalTime-CAST(AfterOT.MealTime AS FLOAT))/CAST(60 AS FLOAT) ELSE '0.0' END) AS AfterReject ");
                //sb.AppendLine(" FROM OverTimeDeclaration AS AfterOT ");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Personal] AS P2 ON AfterOT.OTEmpID=P2.EmpID AND AfterOT.OTCompID=P2.CompID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[RankMapping] AS RM ON P2.CompID=RM.CompID AND P2.RankID=RM.RankID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[Title] AS T ON P2.CompID=T.CompID AND P2.RankID=T.RankID AND P2.TitleID=T.TitleID");
                //sb.AppendLine(" LEFT JOIN " + _eHRMSDB + ".[dbo].[EmpPosition] AS EmpP ON P2.CompID=EmpP.CompID AND P2.EmpID=EmpP.EmpID");
                //sb.AppendLine(" WHERE AfterOT.OTCompID = '" + ddlCompID.SelectedValue + "' ");
                ////sb.AppendLine(" AND AfterOT.OTStatus IN ('2','3','4') ");
                //sb.AppendLine(" AND AfterOT.OrganID IN ('" + orgWhere + "') ");
                //if (txtEmpID.Text!= "" && ddlCompID.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P2.EmpID='" + txtEmpID.Text + "'");
                //}
                //if (txtEmpName.Text != "" && ddlCompID.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P2.NameN LIKE '%" + txtEmpName.Text + "%'");
                //}
                //if (ddlWorkStatus.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND P2.WorkStatus='" + ddlWorkStatus.SelectedValue + "'");
                //}
                ////職等+職稱
                //if (ddlRankIDMIN.SelectedValue != "")
                //{
                //    if (ddlTitleMIN.SelectedValue != "")
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap + P2.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                //    }
                //    else
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                //    }
                //}
                //if (ddlRankIDMAX.SelectedValue != "")
                //{
                //    if (ddlTitleMAX.SelectedValue != "")
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap + P2.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                //    }
                //    else
                //    {
                //        sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                //    }
                //}
                ////職位
                //if (ddlPosition.SelectedValue != "")
                //{
                //    sb.AppendLine(" AND EmpP.PositionID='" + ddlPosition.SelectedValue + "'");
                //}
                //if (txtOTDateBeginCount.ucSelectedDate != "" && txtOTDateEndCount.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND (AfterOT.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "' AND AfterOT.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "')");
                //}
                //else if (txtOTDateBeginCount.ucSelectedDate == "" && txtOTDateEndCount.ucSelectedDate != "")
                //{
                //    sb.AppendLine(" AND AfterOT.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "'");
                //}
                //else if (txtOTDateBeginCount.ucSelectedDate != "" && txtOTDateEndCount.ucSelectedDate == "")
                //{
                //    sb.AppendLine(" AND AfterOT.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "'");
                //}
                //sb.AppendLine(" GROUP BY AfterOT.OTEmpID,P2.NameN )B) C ");
                //sb.AppendLine(" ON A.OTEmpID = C.OTEmpID");

                sb.AppendLine("SELECT P.NameN OTNameN, TOTAL.* FROM(SELECT ISNULL(Before.OTEmpID, AfterOT.OTEmpID) OTEmpID,");
                sb.AppendLine("Before.Submit, Before.Approval, Before.Reject, AfterOT.AfterSubmit, AfterOT.AfterApproval, AfterOT.AfterReject");
                sb.AppendLine("FROM (");
                sb.AppendLine("SELECT Before.OTEmpID, SUM(Before.Submit) Submit, SUM(Before.Approval) Approval, SUM(Before.Reject) Reject");
                sb.AppendLine("FROM (SELECT Before.OTEmpID, Before.OTTxnID,");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE Before.OTStatus WHEN '2' THEN CAST(Before.OTTotalTime-Before.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Submit, ");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE Before.OTStatus WHEN '3' THEN CAST(Before.OTTotalTime-Before.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Approval, ");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE Before.OTStatus WHEN '4' THEN CAST(Before.OTTotalTime-Before.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Reject ");
                sb.AppendLine("FROM OverTimeAdvance AS Before");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal AS PA on PA.CompID=Before.OTCompID AND PA.EmpID=Before.OTEmpID ");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpFlow AS EA on EA.CompID=PA.CompID AND EA.EmpID=PA.EmpID AND EA.ActionID='01' ");
                sb.AppendLine("WHERE Before.OTCompID = '" + ddlCompID.SelectedValue + "'");
                if (AllSearch)
                {
                    sb.AppendLine("AND (PA.OrganID IN ('" + orgWhere + "') OR EA.OrganID IN ('" + orgFlowWhere + "') )");
                }
                else
                {
                    if (orgWhere != "")
                    {
                        sb.AppendLine("AND PA.OrganID IN ('" + orgWhere + "') ");
                    }
                    if (orgFlowWhere != "")
                    {
                        sb.AppendLine("AND EA.OrganID IN ('" + orgFlowWhere + "') ");
                    }
                }
                if (txtOTDateBeginCount.ucSelectedDate != "")
                {
                    sb.AppendLine("AND Before.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "'");
                }
                if (txtOTDateEndCount.ucSelectedDate != "")
                {
                    sb.AppendLine("AND Before.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "'");
                }

                sb.AppendLine("GROUP BY Before.OTEmpID, Before.OTTxnID");
                sb.AppendLine(") Before GROUP BY Before.OTEmpID) Before");
                sb.AppendLine("FULL OUTER JOIN (");
                sb.AppendLine("SELECT AfterOT.OTEmpID, SUM(AfterOT.Submit) AfterSubmit, SUM(AfterOT.Approval) AfterApproval, SUM(AfterOT.Reject) AfterReject");
                sb.AppendLine("FROM (SELECT AfterOT.OTEmpID, AfterOT.OTTxnID,");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE AfterOT.OTStatus WHEN '2' THEN CAST(AfterOT.OTTotalTime-AfterOT.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Submit, ");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE AfterOT.OTStatus WHEN '3' THEN CAST(AfterOT.OTTotalTime-AfterOT.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Approval, ");
                sb.AppendLine("CAST(ROUND(CAST(SUM(CASE AfterOT.OTStatus WHEN '4' THEN CAST(AfterOT.OTTotalTime-AfterOT.MealTime AS FLOAT)/60 ELSE 0 END) AS Decimal(10,2)),1) AS Decimal(10,1)) Reject ");
                sb.AppendLine("FROM OverTimeDeclaration AS AfterOT");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..Personal AS PD on PD.CompID=AfterOT.OTCompID AND PD.EmpID=AfterOT.OTEmpID ");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + "..EmpFlow AS ED on ED.CompID=PD.CompID AND ED.EmpID=PD.EmpID AND ED.ActionID='01' ");
                sb.AppendLine("WHERE AfterOT.OTCompID = '" + ddlCompID.SelectedValue + "'");
                if (AllSearch)
                {
                    sb.AppendLine("AND (PD.OrganID IN ('" + orgWhere + "') OR ED.OrganID IN ('" + orgFlowWhere + "')) ");
                }
                else
                {
                    if (orgWhere != "")
                    {
                        sb.AppendLine("AND PD.OrganID IN ('" + orgWhere + "') ");
                    }
                    if (orgFlowWhere != "")
                    {
                        sb.AppendLine("AND ED.OrganID IN ('" + orgFlowWhere + "') ");
                    }
                }
                if (txtOTDateBeginCount.ucSelectedDate != "")
                {
                    sb.AppendLine("AND AfterOT.OTStartDate >= '" + txtOTDateBeginCount.ucSelectedDate + "'");
                }
                if (txtOTDateEndCount.ucSelectedDate != "")
                {
                    sb.AppendLine("AND AfterOT.OTStartDate <= '" + txtOTDateEndCount.ucSelectedDate + "'");
                }

                sb.AppendLine("GROUP BY AfterOT.OTEmpID, AfterOT.OTTxnID");
                sb.AppendLine(") AfterOT GROUP BY AfterOT.OTEmpID");
                sb.AppendLine(") AfterOT ON Before.OTEmpID = AfterOT.OTEmpID");
                sb.AppendLine(") TOTAL");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + ".[dbo].[Personal] AS P ON TOTAL.OTEmpID=P.EmpID AND P.CompID = '" + ddlCompID.SelectedValue + "'");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + ".[dbo].[RankMapping] AS RM ON P.CompID=RM.CompID AND P.RankID=RM.RankID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + ".[dbo].[Title] AS T ON P.CompID=T.CompID AND P.RankID=T.RankID AND P.TitleID=T.TitleID");
                sb.AppendLine("LEFT JOIN " + _eHRMSDB + ".[dbo].[EmpPosition] AS EmpP ON P.CompID=EmpP.CompID AND P.EmpID=EmpP.EmpID");
                sb.AppendLine("WHERE 1=1");
                if (txtEmpID.Text != "" && ddlCompID.SelectedValue != "")
                {
                    sb.AppendLine(" AND P.EmpID='" + txtEmpID.Text + "'");
                }
                if (txtEmpName.Text != "" && ddlCompID.SelectedValue != "")
                {
                    sb.AppendLine(" AND P.NameN LIKE '%" + txtEmpName.Text + "%'");
                }
                if (ddlWorkStatus.SelectedValue != "")
                {
                    sb.AppendLine(" AND P.WorkStatus='" + ddlWorkStatus.SelectedValue + "'");
                }
                //職等+職稱
                if (ddlRankIDMIN.SelectedValue != "")
                {
                    if (ddlTitleMIN.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + P.TitleID >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + ddlTitleMIN.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap >= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue) + "'");
                    }
                }
                if (ddlRankIDMAX.SelectedValue != "")
                {
                    if (ddlTitleMAX.SelectedValue != "")
                    {
                        sb.AppendLine(" AND RM.RankIDMap + P.TitleID <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + ddlTitleMAX.SelectedValue + "'");
                    }
                    else
                    {
                        sb.AppendLine(" AND RM.RankIDMap <= '" + Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue) + "'");
                    }
                }
                //職位
                if (ddlPosition.SelectedValue != "")
                {
                    sb.AppendLine(" AND EmpP.PositionID='" + ddlPosition.SelectedValue + "'");
                }
                sb.AppendLine("ORDER BY OTEmpID");
                ch.Reset();
                ch.AppendStatement(sb.ToString());

                gvMain2.DataSource = db.ExecuteDataSet(ch.BuildCommand()).Tables[0];
                gvMain2.DataBind();
                gvMain2.Visible = true;
            }

        }
    }

    /// <summary>
    /// 清除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlQueryType.SelectedValue = "1";
        txtEmpID.Text = "";
        txtEmpName.Text = "";
        txtFormNO.Text = "";
        txtOTDateBegin.ucSelectedDate = "";
        txtOTDateEnd.ucSelectedDate = "";
        txtOTDateBeginCount.ucSelectedDate = "";
        txtOTDateEndCount.ucSelectedDate = "";
        txtOTPayDate.Text = "";
        chkAssignTo.Checked = false;
        ddlCompID.SelectedIndex = 0;
        if (ddlCompID.Items.Count > 1)
        {
            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRoleCode40);
            resetDDL(ddlRoleCode30);
            resetDDL(ddlRoleCode20);
            resetDDL(ddlRoleCode10);
            resetDDL(ddlWorkStatus);
            resetDDL(ddlRankIDMIN);
            resetDDL(ddlRankIDMAX);
            resetDDL(ddlTitleMIN);
            resetDDL(ddlTitleMAX);
            resetDDL(ddlPosition);
        }
        gvMain.Visible = false;
        gvMain2.Visible = false;
        ddlQueryType_SelectedIndexChanged(null, null);
    }
    #endregion

    #region GridView
    /// <summary>
    /// 明細
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        string strFlowCaseID = "";
        string strAfterFlowCaseID = "";
        string strStatus = LoadStatus(gvMain.Rows[clickedRow.RowIndex].Cells[8].Text);
        string strAfterStatus = LoadAfterStatus(gvMain.Rows[clickedRow.RowIndex].Cells[14].Text);
        string strSeq = "";
        string strAfterSeq = "";
        if (e.CommandName == "DetailA")
        {
            if ((gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] == (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[1])
            {
                strSeq = at.QueryColumn("OTSeq", "OverTimeAdvance", " AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[1].Replace(":", "") + "' AND OTStatus='" + strStatus + "'");
                strFlowCaseID = at.QueryColumn("FlowCaseID", "OverTimeAdvance", "AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[1].Replace(":", "") + "' AND OTStatus='" + strStatus + "' AND OTSeq='" + strSeq + "'");
            }
            else
            {
                strSeq = at.QueryColumn("OTSeq", "OverTimeAdvance", " AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='2359' AND OTStatus='" + strStatus + "'");
                strFlowCaseID = at.QueryColumn("FlowCaseID", "OverTimeAdvance", "AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='2359' AND OTStatus='" + strStatus + "' AND OTSeq='" + strSeq + "'");
            }
        }
        else
        {
            if ((gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] == (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[1])
            {
                strAfterSeq = at.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[1].Replace(":", "") + "' AND OTStatus='" + strAfterStatus + "'");
                strAfterFlowCaseID = at.QueryColumn("FlowCaseID", "OverTimeDeclaration", "AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[1].Replace(":", "") + "' AND OTStatus='" + strAfterStatus + "' AND OTSeq='" + strAfterSeq + "'");
            }
            else
            {
                strAfterSeq = at.QueryColumn("OTSeq", "OverTimeDeclaration", " AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='2359' AND OTStatus='" + strAfterStatus + "'");
                strAfterFlowCaseID = at.QueryColumn("FlowCaseID", "OverTimeDeclaration", "AND OTEmpID='" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "' AND OTStartDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTEndDate='" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "' AND OTStartTime='" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[0].Replace(":", "") + "' AND OTEndTime='2359' AND OTStatus='" + strAfterStatus + "' AND OTSeq='" + strAfterSeq + "'");
            }
        }
        switch (e.CommandName)
        {
            case "DetailA":
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = (ScreenWidth.Value == "" ? 1020 : (int.Parse(ScreenWidth.Value) > 1020 ? 1020 : int.Parse(ScreenWidth.Value) - 100));
                ucModalPopup1.ucPopupHeight = (ScreenHeight.Value == "" ? 625 : (int.Parse(ScreenHeight.Value) > 625 ? 625 : int.Parse(ScreenHeight.Value) - 100));
                ucModalPopup1.ucPopupHeader = "";
                ucModalPopup1.ucFrameURL = "OvertimePreOrder_Detail.aspx?OTCompID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["OTCompID"].ToString() + "&EmpID=" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "&OTStartDate=" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[0] + "&OTEndDate=" + (gvMain.Rows[clickedRow.RowIndex].Cells[4].Text).Split('~')[1] + "&OTStartTime=" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[0].Replace(":", "") + "&OTEndTime=" + gvMain.Rows[clickedRow.RowIndex].Cells[5].Text.Split('~')[1].Replace(":", "") + "&FlowID=" + flow.FlowCustDB + "&FlowCaseID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["FlowCaseIDBefore"].ToString() + "&OTTxnID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["OTTxnIDBefore"].ToString();
                ucModalPopup1.Show();
                break;
            case "DetailD":
                ucModalPopup1.Reset();
                ucModalPopup1.ucPopupWidth = (ScreenWidth.Value == "" ? 1020 : (int.Parse(ScreenWidth.Value) > 1020 ? 1020 : int.Parse(ScreenWidth.Value) - 100));
                ucModalPopup1.ucPopupHeight = (ScreenHeight.Value == "" ? 625 : (int.Parse(ScreenHeight.Value) > 625 ? 625 : int.Parse(ScreenHeight.Value) - 100));
                ucModalPopup1.ucPopupHeader = "";
                ucModalPopup1.ucFrameURL = "AfterOvertimeOrder_Detail.aspx?OTCompID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["OTCompID"].ToString() + "&EmpID=" + gvMain.Rows[clickedRow.RowIndex].Cells[1].Text + "&OTStartDate=" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[0] + "&OTEndDate=" + (gvMain.Rows[clickedRow.RowIndex].Cells[10].Text).Split('~')[1] + "&OTStartTime=" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[0].Replace(":", "") + "&OTEndTime=" + gvMain.Rows[clickedRow.RowIndex].Cells[11].Text.Split('~')[1].Replace(":", "") + "&FlowID=" + flow.FlowCustDB + "&FlowCaseID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["FlowCaseIDAfter"].ToString() + "&OTTxnID=" + gvMain.DataKeys[clickedRow.RowIndex].Values["OTTxnIDAfter"].ToString();
                ucModalPopup1.Show();
                break;
        }
    }

    /// <summary>
    /// 把沒有資料的位置著色(Grey)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView oRow;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            oRow = (DataRowView)e.Row.DataItem;
            e.Row.Cells[7].ToolTip = oRow["OTReasonMemo"] + "";
            e.Row.Cells[7].Text = (oRow["OTReasonMemo"] + "").Substring(0, (oRow["OTReasonMemo"] + "").Length <= 10 ? (oRow["OTReasonMemo"] + "").Length : 10);

            e.Row.Cells[13].ToolTip = oRow["AfterOTReasonMemo"] + "";
            e.Row.Cells[13].Text = (oRow["AfterOTReasonMemo"] + "").Substring(0, (oRow["AfterOTReasonMemo"] + "").Length <= 10 ? (oRow["AfterOTReasonMemo"] + "").Length : 10);

            if (e.Row.Cells[4].Text != "&nbsp;" && e.Row.Cells[10].Text != "&nbsp;")
            {
                LinkButton lnk_lbtnDetailD = (LinkButton)e.Row.FindControl("btnDetail");
                lnk_lbtnDetailD.Enabled = true;
                LinkButton lnk_lbtnDetailA = (LinkButton)e.Row.FindControl("AfterbtnDetail");
                lnk_lbtnDetailA.Enabled = true;
            }
            else if (e.Row.Cells[10].Text != "&nbsp;")
            {
                LinkButton lnk_lbtnDetailD = (LinkButton)e.Row.FindControl("btnDetail");
                //lnk_lbtnDetailD.Enabled = true;
                lnk_lbtnDetailD.Text = "";
                LinkButton lnk_lbtnDetailA = (LinkButton)e.Row.FindControl("AfterbtnDetail");
                lnk_lbtnDetailA.Enabled = true;

                e.Row.Cells[3].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[4].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[8].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            }
            else if (e.Row.Cells[4].Text != "&nbsp;")
            {
                LinkButton lnk_lbtnDetailA = (LinkButton)e.Row.FindControl("AfterbtnDetail");
                //lnk_lbtnDetailA.Enabled = true;
                lnk_lbtnDetailA.Text = "";
                LinkButton lnk_lbtnDetailD = (LinkButton)e.Row.FindControl("btnDetail");
                lnk_lbtnDetailD.Enabled = true;

                e.Row.Cells[9].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[10].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[11].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[12].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[13].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
                e.Row.Cells[14].BackColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF");
            }
        }
    }

    /// <summary>
    /// 把GridView空白取代為'-'
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMain2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i <= gvMain2.Columns.Count - 1; i++)
            {
                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0.0") //2017/03/07 將'0.0'改成'-'
                {
                    e.Row.Cells[i].Text = "-";
                }
                else if (i >= 3 && i <= 8)
                {
                    e.Row.Cells[i].Text = Convert.ToDouble(e.Row.Cells[i].Text).ToString("0.0");
                }
            }
        }
    }

    /// <summary>
    /// 明細查詢合併表頭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView gv = (GridView)sender;
            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow gvRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell tc0 = new TableCell();
            tc0.Text = "序號";
            tc0.HorizontalAlign = HorizontalAlign.Center;
            tc0.RowSpan = 2;
            tc0.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc0);
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

            TableCell HeaderCell_1 = new TableCell();
            HeaderCell_1.Text = "明細";
            HeaderCell_1.CssClass = "Util_gvHeader";
            HeaderCell_1.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_1);

            TableCell HeaderCell_2 = new TableCell();
            HeaderCell_2.Text = "加班日期";
            HeaderCell_2.CssClass = "Util_gvHeader";
            HeaderCell_2.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_2);

            TableCell HeaderCell_3 = new TableCell();
            HeaderCell_3.Text = "加班起迄時間";
            HeaderCell_3.CssClass = "Util_gvHeader";
            HeaderCell_3.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_3);

            TableCell HeaderCell_4 = new TableCell();
            HeaderCell_4.Text = "加班類型";
            HeaderCell_4.CssClass = "Util_gvHeader";
            HeaderCell_4.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_4);

            TableCell HeaderCell_5 = new TableCell();
            HeaderCell_5.Text = "加班原因";
            HeaderCell_5.CssClass = "Util_gvHeader";
            HeaderCell_5.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_5);

            TableCell HeaderCell_6 = new TableCell();
            HeaderCell_6.Text = "狀態";
            HeaderCell_6.CssClass = "Util_gvHeader";
            HeaderCell_6.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_6);

            TableCell HeaderCell_7 = new TableCell();
            HeaderCell_7.Text = "明細";
            HeaderCell_7.CssClass = "Util_gvHeader";
            HeaderCell_7.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_7);

            TableCell HeaderCell_8 = new TableCell();
            HeaderCell_8.Text = "加班日期";
            HeaderCell_8.CssClass = "Util_gvHeader";
            HeaderCell_8.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_8);

            TableCell HeaderCell_9 = new TableCell();
            HeaderCell_9.Text = "加班起迄時間";
            HeaderCell_9.CssClass = "Util_gvHeader";
            HeaderCell_9.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_9);

            TableCell HeaderCell_10 = new TableCell();
            HeaderCell_10.Text = "加班類型";
            HeaderCell_10.CssClass = "Util_gvHeader";
            HeaderCell_10.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_10);

            TableCell HeaderCell_11 = new TableCell();
            HeaderCell_11.Text = "加班原因";
            HeaderCell_11.CssClass = "Util_gvHeader";
            HeaderCell_11.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_11);

            TableCell HeaderCell_12 = new TableCell();
            HeaderCell_12.Text = "狀態";
            HeaderCell_12.CssClass = "Util_gvHeader";
            HeaderCell_12.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_12);

            e.Row.Cells.Clear();
            gv.Controls[0].Controls.AddAt(0, gvRow);
            gv.Controls[0].Controls.AddAt(1, gvRow1);
        }
    }

    /// <summary>
    /// 統計查詢合併表頭
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gd2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView gv = (GridView)sender;
            GridViewRow gvRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow gvRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell tc0 = new TableCell();
            tc0.Text = "序號";
            tc0.HorizontalAlign = HorizontalAlign.Center;
            tc0.RowSpan = 2;
            tc0.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc0);
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
            tc3.Text = "加班單預先申請(時數)";
            tc3.HorizontalAlign = HorizontalAlign.Center;
            tc3.ColumnSpan = 3;
            tc3.CssClass = "Util_gvHeader";
            gvRow.Cells.Add(tc3);

            TableCell tc4 = new TableCell();
            tc4.Text = "加班單事後申報(時數)";
            tc4.ColumnSpan = 3;
            tc4.CssClass = "Util_gvHeader";
            tc4.HorizontalAlign = HorizontalAlign.Center;
            gvRow.Cells.Add(tc4);

            TableCell HeaderCell_1 = new TableCell();
            HeaderCell_1.Text = "送簽";
            HeaderCell_1.CssClass = "Util_gvHeader";
            HeaderCell_1.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_1);

            TableCell HeaderCell_2 = new TableCell();
            HeaderCell_2.Text = "核准";
            HeaderCell_2.CssClass = "Util_gvHeader";
            HeaderCell_2.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_2);

            TableCell HeaderCell_3 = new TableCell();
            HeaderCell_3.Text = "駁回";
            HeaderCell_3.CssClass = "Util_gvHeader";
            HeaderCell_3.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_3);

            TableCell HeaderCell_7 = new TableCell();
            HeaderCell_7.Text = "送簽";
            HeaderCell_7.CssClass = "Util_gvHeader";
            HeaderCell_7.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_7);

            TableCell HeaderCell_8 = new TableCell();
            HeaderCell_8.Text = "核准";
            HeaderCell_8.CssClass = "Util_gvHeader";
            HeaderCell_8.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_8);

            TableCell HeaderCell_9 = new TableCell();
            HeaderCell_9.Text = "駁回";
            HeaderCell_9.CssClass = "Util_gvHeader";
            HeaderCell_9.HorizontalAlign = HorizontalAlign.Center;
            gvRow1.Cells.Add(HeaderCell_9);

            e.Row.Cells.Clear();
            gv.Controls[0].Controls.AddAt(0, gvRow);
            gv.Controls[0].Controls.AddAt(1, gvRow1);
        }
    }
    #endregion

    #region 公司、處部科下拉改變
    /// <summary>
    /// 公司下拉選單改變
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompID.SelectedValue != "")
        {
            //ddlQueryType.SelectedValue = "1";
            txtEmpID.Text = "";
            txtEmpName.Text = "";
            txtFormNO.Text = "";
            txtOTDateBegin.ucSelectedDate = "";
            txtOTDateEnd.ucSelectedDate = "";
            txtOTPayDate.Text = "";
            ddlOrgType.Items.Clear();
            ddlDeptID.Items.Clear();
            ddlOrganID.Items.Clear();
            DropDownListDefault();
        }
        else
        {
            //ddlQueryType.SelectedValue = "1";
            txtEmpID.Text = "";
            txtEmpName.Text = "";
            txtFormNO.Text = "";
            txtOTDateBegin.ucSelectedDate = "";
            txtOTDateEnd.ucSelectedDate = "";
            txtOTPayDate.Text = "";
            #region 待修改
            //公司選請選擇
            //有帶值的下拉清空，給請選擇
            resetDDL(ddlOrgType);
            resetDDL(ddlDeptID);
            resetDDL(ddlOrganID);
            resetDDL(ddlRankIDMIN);
            resetDDL(ddlRankIDMAX);
            resetDDL(ddlPosition);
            resetDDL(ddlTitleMIN);
            resetDDL(ddlTitleMAX);
            #endregion
            //沒選公司，不給預設下拉
            //DropDownListDefault();
        }
    }

    /// <summary>
    /// 單位類別(處)下拉選單改變
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOrgType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //單位類別改變，部門&科別回到預設
        if (ddlOrgType.SelectedValue != "")
        {
            DataTable dtDept = DataTableFilterSort(_ddlOrgTable, "OrgType = '" + ddlOrgType.SelectedValue + "'", "OrganID");
            FillDDL(ddlDeptID, dtDept.DefaultView.ToTable(true, new string[] { "DeptID", "DeptName" }), "DeptID", "DeptName");

            DataTable dtOrg = DataTableFilterSort(_ddlOrgTable, "OrgType = '" + ddlOrgType.SelectedValue + "' AND DeptID <> OrganID", "OrganID");
            if (dtOrg.Rows.Count > 0)
            {
                FillDDL(ddlOrganID, dtOrg.DefaultView.ToTable(true, new string[] { "OrganID", "OrganName" }), "OrganID", "OrganName");
            }
            else
            {
                resetDDL(ddlOrganID);
            }
        }
        else //部門---請選擇---&科別---請選擇---
        {
            FillDDL(ddlDeptID, _ddlOrgTable.DefaultView.ToTable(true, new string[] { "DeptID", "DeptName" }), "DeptID", "DeptName");
            FillDDL(ddlOrganID, DataTableFilterSort(_ddlOrgTable, "DeptID <> OrganID", "OrganID"), "OrganID", "OrganName");

            resetDDL(ddlPosition);
        }
    }

    /// <summary>
    /// 部門下拉選單改變
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDeptID_SelectedIndexChanged(object sender, EventArgs e)
    {
        //部門改變，科別回到預設
        if (ddlDeptID.SelectedValue != "")
        {
            FillDDL(ddlOrganID, DataTableFilterSort(_ddlOrgTable, "DeptID = '" + ddlDeptID.SelectedValue + "' AND DeptID <> OrganID", "OrganID"), "OrganID", "OrganName");
            SetPosition(ddlDeptID.SelectedValue);
        }
        else if (ddlDeptID.SelectedValue == "")//科別---請選擇---
        {
            if (ddlOrgType.SelectedValue != "")
            {
                DataTable dtOrg = DataTableFilterSort(_ddlOrgTable, "OrgType = '" + ddlOrgType.SelectedValue + "' AND DeptID <> OrganID", "OrganID");
                FillDDL(ddlOrganID, dtOrg.DefaultView.ToTable(true, new string[] { "OrganID", "OrganName" }), "OrganID", "OrganName");
            }
            else
            {
                FillDDL(ddlOrganID, DataTableFilterSort(_ddlOrgTable, "DeptID <> OrganID", "OrganID"), "OrganID", "OrganName");
            }
            resetDDL(ddlPosition);
        }
    }

    /// <summary>
    /// 科別下拉選單改變
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOrganID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrganID.SelectedValue != "")
        {
            SetPosition(ddlOrganID.SelectedValue);
        }
        else
        {
            SetPosition(ddlDeptID.SelectedValue);
        }
    }
    
    //職位
    private void SetPosition(string OrganID)
    {
        DbHelper db = new DbHelper(_hrDBName);
        CommandHelper sb = db.CreateCommandHelper();
        
        sb.Reset();
        sb.Append("SELECT OrgP.PositionID, P.Remark AS PositionName FROM " + _eHRMSDB + ".[dbo].[OrgPosition] OrgP ");
        sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[Position] P ON OrgP.CompID = P.CompID AND P.PositionID = OrgP.PositionID AND P.InValidFlag = '0'");
        sb.Append(" WHERE OrgP.OrganID = '" + OrganID + "' AND OrgP.CompID = '" + ddlCompID.SelectedValue + "'");
        sb.Append(" ORDER BY OrgP.PositionID");
        DataTable dtPosition = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

        FillDDL(ddlPosition, dtPosition, "PositionID", "PositionName");
    }
    #endregion

    /// <summary>
    /// 轉換預先申請狀態(文字→數字)
    /// </summary>
    /// <param name="OTStatusName"></param>
    /// <returns></returns>
    protected string LoadStatus(string OTStatusName)
    {
        string strStatus = "";
        switch (OTStatusName)
        {
            case "暫存":
                strStatus = "1";
                break;
            case "送簽":
                strStatus = "2";
                break;
            case "核准":
                strStatus = "3";
                break;
            case "駁回":
                strStatus = "4";
                break;
            case "刪除":
                strStatus = "5";
                break;
            case "取消":
                strStatus = "9";
                break;
        }
        return strStatus;
    }

    /// <summary>
    /// 轉換事後申報狀態(文字→數字)
    /// </summary>
    /// <param name="OTAfterStatusName"></param>
    /// <returns></returns>
    protected string LoadAfterStatus(string OTAfterStatusName)
    {
        string strAfterStatus = "";
        switch (OTAfterStatusName)
        {
            case "暫存":
                strAfterStatus = "1";
                break;
            case "送簽":
                strAfterStatus = "2";
                break;
            case "核准":
                strAfterStatus = "3";
                break;
            case "駁回":
                strAfterStatus = "4";
                break;
            case "刪除":
                strAfterStatus = "5";
                break;
            case "取消":
                strAfterStatus = "6";
                break;
            case "作廢":
                strAfterStatus = "7";
                break;
            case "計薪後收回":
                strAfterStatus = "9";
                break;
        }
        return strAfterStatus;
    }

    /// <summary>
    /// 勾選依簽核人員查詢checkbox，關閉下拉選擇功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkAssignTo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAssignTo.Checked)
        {
            //ddlCompID.Enabled = false;
            ddlOrgType.Enabled = false;
            ddlDeptID.Enabled = false;
            ddlOrganID.Enabled = false;
            ddlRoleCode40.Enabled = false;
            ddlRoleCode30.Enabled = false;
            ddlRoleCode20.Enabled = false;
            ddlRoleCode10.Enabled = false;
        }
        else
        {
            //ddlCompID.Enabled = true;
            ddlOrgType.Enabled = true;
            ddlDeptID.Enabled = true;
            ddlOrganID.Enabled = true;
            ddlRoleCode40.Enabled = true;
            ddlRoleCode30.Enabled = true;
            ddlRoleCode20.Enabled = true;
            ddlRoleCode10.Enabled = true;
        }
        gvMain.Visible = false;
        gvMain2.Visible = false;
        DropDownListDefault();
    }

    #region 20170207 leo modify (增加特殊人員登入後的處/部/科下拉)
    private void ColumnAdd(DataTable dt, string Name, string DataType)
    {
        DataColumn NewColumn;
        NewColumn = new DataColumn();
        NewColumn.DataType = Type.GetType(DataType);
        NewColumn.ColumnName = Name;
        dt.Columns.Add(NewColumn);
    }
    //下拉function 非共用
    private void FillDDL(string str, DropDownList DDL) //行政組織
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        DataTable dt = db.ExecuteDataSet(str).Tables[0];
        FillDDL(dt, DDL, "Code", "CodeName");
    }
    private void FillDDL(DataTable dt, DropDownList DDL, string DataValueField, string DataTextField) //功能組織
    {
        DDL.Items.Clear();
        DDL.DataSource = dt.DefaultView.ToTable(true, DataValueField, DataTextField);
        DDL.DataValueField = DataValueField;
        DDL.DataTextField = DataTextField;
        DDL.DataBind();
        DDL.Items.Insert(0, new ListItem("----請選擇----", ""));
    }
    private void save_DeptID(string str, string Type) //行政組織
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        DataTable dt = db.ExecuteDataSet(str).Tables[0];
        save_DeptID(dt, Type, "Code", "CodeName");
    }
    //private void save_DeptID(DataTable dt, string Type, int RoleCode)
    //{
    //    for (int i = RoleCode; i >= 0; i = i - 10)
    //    {
    //        save_DeptID(NoWhite(dt, "O"+i+"OrganID"), "DeptIDFlow", "O"+i+"OrganID", "O"+i+"OrganName");
    //    }
    //        save_DeptID(NoWhite(dt, "O20OrganID"), "DeptIDFlow", "O20OrganID", "O20OrganName");
    //}
    private void save_DeptID(DataTable dt, string Type, string DataValueField, string DataTextField) //功能組織
    {
        if (Type == "DeptID")
        {
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = dt.DefaultView.ToTable(true, DataValueField, DataTextField).Select(DataValueField + "<>''").CopyToDataTable();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ViewState["DeptID"] = "'" + dt1.Rows[i][DataValueField].ToString() + "'," + ViewState["DeptID"];
                }
            }
        }
        else if (Type == "DeptIDFlow")
        {
            if (dt.Rows.Count > 0)
            {
                //DataTable dt1 = dt.DefaultView.ToTable(true, DataValueField, DataTextField).Select(DataValueField + "<>''").CopyToDataTable();
                DataTable dt1 = NoWhite(dt.DefaultView.ToTable(true, DataValueField, DataTextField), DataValueField);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    ViewState["DeptIDFlow"] = "'" + dt1.Rows[i][DataValueField].ToString() + "'," + ViewState["DeptIDFlow"];
                }
            }
        }
    }
    private DataTable NoWhite(DataTable dt, string self) //去除空白資料and檢查是否為空Table
    {
        DataTable dClear = dt.Copy();
        dClear.Clear();
        dt = dt.Select(self + "<>''").Length > 0 ? dt.Select(self + "<>''").CopyToDataTable() : dClear;
        return dt;
    }
    private DataTable SelectRoleCode(DataTable dt, string ddlRoleCodeSelect, string DDL, string View)
    {
        if (ddlRoleCodeSelect == "")
        {
            return NoWhite(dt, "O" + View + "OrganID");
        }
        else
        {
            if (dt.Select("O" + DDL + "OrganID = '" + ddlRoleCodeSelect + "' and O" + View + "OrganID<>''").Length > 0)
                return dt.Select("O" + DDL + "OrganID = '" + ddlRoleCodeSelect + "' and O" + View + "OrganID<>''").CopyToDataTable();
            else
                dt.Clear();
        }
        return dt;
    }
    //功能組織下拉
    private void ddlGetFlow(int RoleCode)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        DataTable dt = db.ExecuteDataSet(ViewState["OrgFlowList"].ToString()).Tables[0];
        DataTable dClear = dt.Copy();
        dClear.Clear();
        ViewState["DeptIDFlow"] = "";
        string self = "";
        switch (RoleCode)
        {
            case 40: //PageLoad
                FillDDL(NoWhite(dt, "O40OrganID"), ddlRoleCode40, "O40OrganID", "O40OrganName");
                save_DeptID(NoWhite(dt, "O40OrganID"), "DeptIDFlow", "O40OrganID", "O40OrganName");
                goto case 30;
            case 30: //ddlRoleCode40 selectchange
                dt = SelectRoleCode(dt, ddlRoleCode40.SelectedValue, "40", "30");
                FillDDL(dt, ddlRoleCode30, "O30OrganID", "O30OrganName");
                save_DeptID(NoWhite(dt, "O30OrganID"), "DeptIDFlow", "O30OrganID", "O30OrganName");
                goto case 20;
            case 20: //ddlRoleCode30 selectchange
                dt = SelectRoleCode(dt, ddlRoleCode30.SelectedValue, "30", "20");
                FillDDL(dt, ddlRoleCode20, "O20OrganID", "O20OrganName");
                save_DeptID(NoWhite(dt, "O20OrganID"), "DeptIDFlow", "O20OrganID", "O20OrganName");
                goto case 10;
            /* //10與0舊版本
        case 10: //ddlRoleCode20 selectchange
            //self = ddlRoleCode20.SelectedValue;
            dt = ddlRoleCode20.SelectedValue == "" ? NoWhite(dt, "O10OrganID") : NoWhite(dt.Select("O20OrganID = '" + ddlRoleCode20.SelectedValue + "'").CopyToDataTable(), "O10OrganID");
            FillDDL(dt, ddlRoleCode10, "O10OrganID", "O10OrganName");
            save_DeptID(NoWhite(dt, "O10OrganID"), "DeptIDFlow", "O10OrganID", "O10OrganName");
            goto case 0;
        case 0: //ddlRoleCode10 selectchange
            //self = ddlRoleCode10.SelectedValue;
            dt = ddlRoleCode10.SelectedValue == "" ? NoWhite(dt, "O0OrganID") : NoWhite(dt.Select("O10OrganID = '" + ddlRoleCode10.SelectedValue + "'").CopyToDataTable(), "O0OrganID");
            FillDDL(dt, ddlRoleCode0, "O0OrganID", "O0OrganName");
            save_DeptID(NoWhite(dt, "O0OrganID"), "DeptIDFlow", "O0OrganID", "O0OrganName");
            break;
            */
            case 10: //10與0新版本(特殊處理) //ddlRoleCode20 selectchange
                ColumnAdd(dt, "OOrganID", "System.String");
                ColumnAdd(dt, "OOrganName", "System.String");
                if (dt.Select("O0OrganID<>''").Length > 0)
                {
                    DataTable dt1 = dt.Select("O0OrganID<>''").CopyToDataTable().DefaultView.ToTable(true, "O40OrganID", "O30OrganID", "O20OrganID", "O10OrganID", "O10OrganName");
                    //ColumnAdd(dt, "10Rank", "System.String");
                    ColumnAdd(dt1, "O0OrganID", "System.String");
                    ColumnAdd(dt1, "OOrganID", "System.String");
                    ColumnAdd(dt1, "OOrganName", "System.String");
                    ColumnAdd(dt1, "10Rank", "System.String");
                    DataRow[] arrRows = dt1.Select("");
                    foreach (DataRow row in arrRows)
                    {
                        row["OOrganID"] = row["O10OrganID"];
                        row["OOrganName"] = row["O10OrganName"];
                        //row["10Rank"] = "A10";
                    }
                    //dt1.DefaultView.ToTable(true, "O10OrganID", "O10OrganName");
                    arrRows = dt.Select("");
                    foreach (DataRow row in arrRows)
                    {
                        if (row["O0OrganID"].ToString() == "")
                        {
                            row["OOrganID"] = row["O10OrganID"];
                            row["OOrganName"] = row["O10OrganName"];
                            //row["10Rank"] = "A10";
                        }
                        else
                        {
                            row["OOrganID"] = row["O0OrganID"];
                            row["OOrganName"] = "└─" + row["O0OrganName"];
                            //row["10Rank"] = "B0";
                        }
                        //row["OOrganID"] = row["O0OrganID"].ToString()=="" ? row["O10OrganID"] :row["O0OrganID"];
                        //row["OOrganName"] = row["O0OrganID"].ToString() == "" ? row["O10OrganName"] : "└─" + row["O0OrganName"];
                        //if (row["O0OrganID"].ToString() == "") row["O0OrganID"]= "0";
                    }
                    dt = ddlRoleCode20.SelectedValue == "" ? NoWhite(dt, "O10OrganID") : NoWhite(dt.Select("O20OrganID = '" + ddlRoleCode20.SelectedValue + "'").CopyToDataTable(), "O10OrganID");
                    dt.Merge(dt1, true);
                    dt = SelectRoleCode(dt, ddlRoleCode20.SelectedValue, "20", "");
                    dt.DefaultView.Sort = "O10OrganID asc,10Rank asc,O0OrganID asc,OOrganID asc";
                    dt = dt.DefaultView.ToTable();
                }//if (dt.Select("O0OrganID<>''").Length > 0)
                FillDDL(dt, ddlRoleCode10, "OOrganID", "OOrganName");
                if (ddlRoleCode10.SelectedItem.Text.ToString().Left(1) != "└" && ddlRoleCode10.SelectedItem.Text.ToString().Left(1) != "")
                {
                    save_DeptID(NoWhite(dt, "O0OrganID"), "DeptIDFlow", "O0OrganID", "O0OrganName");
                }
                break;
            case 0:
                if (ddlRoleCode10.SelectedItem.Text.ToString().Left(1) != "└")
                {
                    dt = NoWhite(dt.Select("O10OrganID = '" + ddlRoleCode10.SelectedValue + "'").CopyToDataTable(), "O0OrganID");
                    save_DeptID(NoWhite(dt, "O0OrganID"), "DeptIDFlow", "O0OrganID", "O0OrganName");
                }
                else
                    ViewState["DeptIDFlow"] = "";
                break;
        }
        self = ddlRoleCode10.SelectedValue != "" ? ddlRoleCode10.SelectedValue : ddlRoleCode20.SelectedValue != "" ? ddlRoleCode20.SelectedValue : ddlRoleCode30.SelectedValue != "" ? ddlRoleCode30.SelectedValue : ddlRoleCode40.SelectedValue;
        //if (ddlRoleCode0.SelectedValue != "")
        //    self = ddlRoleCode0.SelectedValue;
        //else if (ddlRoleCode10.SelectedValue != "") 
        //    self = ddlRoleCode10.SelectedValue;
        //else if (ddlRoleCode20.SelectedValue != "")
        //    self = ddlRoleCode20.SelectedValue;
        //else if (ddlRoleCode30.SelectedValue != "")
        //    self = ddlRoleCode30.SelectedValue;
        //else
        //    self = ddlRoleCode40.SelectedValue;
        if (self != "")
            ViewState["DeptIDFlow"] = ViewState["DeptIDFlow"].ToString() + "'" + self + "'";
        else
            ViewState["DeptIDFlow"] = ViewState["DeptIDFlow"].ToString().TrimEnd(',');
        string test = ViewState["DeptIDFlow"].ToString();
    }
    //行政組織下拉
    private void ddlGet(string Type)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        ViewState["DeptID"] = "";
        string self = "";
        switch (Type)
        {
            case "OrgType":
                sb.Append("select DISTINCT O1.OrgType as Code,O1.OrgType+'-'+O2.OrganName as CodeName from " + _eHRMSDB + ".[dbo].[Organization] O1 ");
                sb.Append("left join " + _eHRMSDB + ".[dbo].[Organization] O2 on O1.OrgType=O2.OrganID ");
                sb.Append("where O1.OrganID in(" + ViewState["OrgList"].ToString() + ") ");
                sb.Append("and O1.CompID='" + ViewState["CompID"] + "' ");
                FillDDL(sb.ToString(), ddlOrgType);
                save_DeptID(sb.ToString(), "DeptID");
                sb.Reset();
                goto case "DeptID";
            case "DeptID":
                self = ddlOrgType.SelectedValue;
                sb.Append("select DISTINCT O1.DeptID as Code,O1.DeptID+'-'+O2.OrganName as CodeName from " + _eHRMSDB + ".[dbo].[Organization] O1 ");
                sb.Append("left join " + _eHRMSDB + ".[dbo].[Organization] O2 on O1.DeptID=O2.OrganID ");
                sb.Append("where O1.OrganID in(" + ViewState["OrgList"].ToString() + ") ");
                sb.Append("and O1.CompID='" + ViewState["CompID"] + "' ");
                if (ddlOrgType.SelectedValue != "") sb.Append("and O1.OrgType='" + ddlOrgType.SelectedValue + "' ");
                FillDDL(sb.ToString(), ddlDeptID);
                save_DeptID(sb.ToString(), "DeptID");
                sb.Reset();
                goto case "OrganID";
            case "OrganID":
                self = ddlDeptID.SelectedValue;
                sb.Append("Select  OrganID as Code,OrganID+'-'+OrganName as CodeName from " + _eHRMSDB + ".[dbo].[Organization] O1 where OrganID in(" + ViewState["OrgList"].ToString() + ")");
                sb.Append("and O1.CompID='" + ViewState["CompID"] + "' ");
                if (ddlDeptID.SelectedValue != "") sb.Append("and O1.DeptID='" + ddlDeptID.SelectedValue + "' ");
                FillDDL(sb.ToString(), ddlOrganID);
                save_DeptID(sb.ToString(), "DeptID");
                sb.Reset();
                break;
            case "One":
                self = ddlOrganID.SelectedValue;
                break;
        }
        if (self != "")
            ViewState["DeptID"] = ViewState["DeptID"].ToString() + "'" + self + "'";
        else
            ViewState["DeptID"] = ViewState["DeptID"].ToString().TrimEnd(',');
        string test = ViewState["DeptID"].ToString();
    }
    //行政組織下拉
    //protected void ddlOrgType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlOrgType.SelectedValue == "")
    //        ddlGet("OrgType");
    //    else
    //        ddlGet("DeptID");
    //}
    //protected void ddlDeptID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDeptID.SelectedValue == "")
    //        ddlGet("DeptID");
    //    else
    //        ddlGet("OrganID");
    //}
    //protected void ddlOrganID_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlOrganID.SelectedValue == "")
    //        ddlGet("OrganID");
    //    else
    //        ddlGet("One");
    //}
    //功能組織下拉
    //protected void ddlRoleCode40_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRoleCode40.SelectedValue == "")
    //        ddlGetFlow(40);
    //    else
    //        ddlGetFlow(30);
    //}
    //protected void ddlRoleCode30_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRoleCode30.SelectedValue == "")
    //        ddlGetFlow(30);
    //    else
    //        ddlGetFlow(20);
    //}
    //protected void ddlRoleCode20_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRoleCode20.SelectedValue == "")
    //        ddlGetFlow(20);
    //    else
    //        ddlGetFlow(10);
    //}
    //protected void ddlRoleCode10_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRoleCode10.SelectedValue == "")
    //        ddlGetFlow(10);
    //    else
    //        ddlGetFlow(0);
    //}
    //protected void ddlRoleCode0_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlRoleCode0.SelectedValue == "")
    //        ddlGetFlow(0);
    //    else
    //        ddlGetFlow(1);
    //}
    #endregion

    //找UpOrganID是否是自己
    public void getOrganBoss(string _EmpID) //for 功能組織找RoleCode='40'的UpOrganID主管
    {
        //主管需在找上一階，直到找到是自己
        int intLoop = 0;
        haveOrNot=false;
        DataTable dt;
        DataTable dtTemp;
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        dt = db.ExecuteDataSet(CommandType.Text, string.Format("SELECT OrganID, OrganID + '-' + OrganName AS OrganName, Boss, UpOrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType WHERE OrgF.RoleCode='40' AND Boss='"+UserInfo.getUserInfo().UserID+"'")).Tables[0];
        Dictionary<string, string> _dic = new Dictionary<string, string>();
        
        if(dt.Rows.Count == 1)
        {
            FillDDL(ddlRoleCode40, dt, "OrganID", "OrganName");
            haveOrNot = true;
        }
        else
        {
            intLoop++;
            DataTable aa;
            aa = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganID+'-'+OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType WHERE OrgF.RoleCode='40'")).Tables[0];
            for (int i = 0; i < aa.Rows.Count; i++)
            {
                CommandHelper sbRoleCode = db.CreateCommandHelper();
                if (aa.Rows[i]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID) //RoleCode='40'的上一階是登入者
                {
                    FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                    haveOrNot = true;
                }
                else
                {
                    intLoop++;
                    DataTable bb;
                    bb = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' AND H.Code=OrgF.BusinessType WHERE OrgF.OrganID='" + aa.Rows[i]["UpOrganID"] + "'")).Tables[0];
                    for (int j = 0; j < bb.Rows.Count; j++)
                    {
                        if (bb.Rows[j]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID)
                        {
                            FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                            haveOrNot = true;
                            //if (haveOrNot) break;
                        }
                        else
                        {
                            intLoop++;
                            DataTable cc;
                            cc = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' AND H.Code=OrgF.BusinessType WHERE OrgF.OrganID='" + bb.Rows[j]["UpOrganID"] + "'")).Tables[0];
                            for (int k = 0; k < cc.Rows.Count; k++)
                            {
                                if (cc.Rows[k]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID)
                                {
                                    FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                                    haveOrNot = true;
                                }
                                else
                                {
                                    intLoop++;
                                    DataTable dd;
                                    dd = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' AND H.Code=OrgF.BusinessType WHERE OrgF.OrganID='" + cc.Rows[k]["UpOrganID"] + "'")).Tables[0];
                                    for (int l = 0; l < dd.Rows.Count; l++)
                                    {
                                        if (dd.Rows[l]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID)
                                        {
                                            FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                                            haveOrNot = true;
                                        }
                                        else
                                        {
                                            intLoop++;
                                            DataTable ee;
                                            ee = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' AND H.Code=OrgF.BusinessType WHERE OrgF.OrganID='" + dd.Rows[l]["UpOrganID"] + "'")).Tables[0];
                                            for (int m = 0; m < ee.Rows.Count; m++)
                                            {
                                                if (dd.Rows[m]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID)
                                                {
                                                    FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                                                    haveOrNot = true;
                                                }
                                                else
                                                {
                                                    intLoop++;
                                                    DataTable ff;
                                                    ff = db.ExecuteDataSet(CommandType.Text, string.Format(" SELECT OrgF.OrganID,OrgF.OrganName AS OrganName, OrgF.Boss, OrgF.UpOrganID, OrgF2.Boss AS UpOrganBoss FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] OrgF2 ON OrgF.UpOrganID=OrgF2.OrganID LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' AND H.Code=OrgF.BusinessType WHERE OrgF.OrganID='" + ee.Rows[m]["UpOrganID"] + "'")).Tables[0];
                                                    for (int n = 0; n < ff.Rows.Count; n++)
                                                    {
                                                        if (ff.Rows[n]["UpOrganBoss"].ToString() == UserInfo.getUserInfo().UserID)
                                                        {
                                                            FillDDL(ddlRoleCode40, aa, "OrganID", "OrganName");
                                                            haveOrNot = true;
                                                        }
                                                        else
                                                        {

                                                        }
                                                        if (haveOrNot) break;
                                                    }
                                                }
                                                if (haveOrNot) break;
                                            }
                                        }
                                        if (haveOrNot) break;
                                    }
                                }
                                if (haveOrNot) break;
                            }
                        }
                        if (haveOrNot) break;
                    }
                }
                if (haveOrNot) break;
            }
        }
    }
    protected void ChkRankID()
    {
        if (ddlRankIDMIN.SelectedValue != "" && ddlRankIDMAX.SelectedValue != "")
        {
            int RankIDMIN = 0;
            int RankIDMAX = 0;
            Int32.TryParse(Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMIN.SelectedValue), out RankIDMIN);
            Int32.TryParse(Aattendant.GetRankIDFormMapping(ddlCompID.SelectedValue, ddlRankIDMAX.SelectedValue), out RankIDMAX);

            if (RankIDMIN > RankIDMAX)
            {
                Util.MsgBox("左邊職等不可大於右邊職等！");
            }
        }
    }

    protected void ChkTitleID()
    {
        if (ddlRankIDMIN.SelectedValue == ddlRankIDMAX.SelectedValue)
        {
            if (ddlTitleMIN.SelectedValue != "" && ddlTitleMAX.SelectedValue != "")
            {
                if (Convert.ToInt32(ddlTitleMIN.SelectedValue) > Convert.ToInt32(ddlTitleMAX.SelectedValue))
                {
                    Util.MsgBox("左邊職稱不可大於右邊職稱！");
                }
            }
        }
    }

    /// <summary>
    /// 如果查詢功能組織為高階
    /// 找尋轄下OrganID
    /// </summary>
    /// <returns></returns>
    protected string LoadRoleCode()
    {
        DbHelper db = new DbHelper(_DBName);
        string strRoleCode = "";
        //找10
        if (ddlRoleCode10.SelectedValue != "")
        {
            return ddlRoleCode10.SelectedValue;
            //sb.Append(" AND D.OrganID = '" + ddlRoleCode10.SelectedValue + "' ");
        }
        //找20
        else if (ddlRoleCode20.SelectedValue != "" && ddlRoleCode10.SelectedValue == "")
        {
            CommandHelper sbRoleCode = db.CreateCommandHelper();
            //
            DataTable DDLRoleCode = new DataTable();
            DDLRoleCode.Columns.Add("OrganID");
            DDLRoleCode.Rows.Add(ddlRoleCode20.SelectedValue); //加20
            sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
            sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
            sbRoleCode.Append(" WHERE RoleCode='10' AND UpOrganID =");
            sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='20' AND OrganID='" + ddlRoleCode20.SelectedValue + "' )");
            DataTable tempDDLRoleCode10 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0]; //10
            if (tempDDLRoleCode10.Rows.Count > 0)
            {
                for (int i = 0; i < tempDDLRoleCode10.Rows.Count; i++)
                {
                    DDLRoleCode.Rows.Add(tempDDLRoleCode10.Rows[i]["OrganID"]); //加10
                    sbRoleCode.Reset();
                    sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                    sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                    sbRoleCode.Append(" WHERE RoleCode='0' AND UpOrganID =");
                    sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='10' AND OrganID='" + tempDDLRoleCode10.Rows[i]["OrganID"] + "' )");
                    DataTable tempDDLRoleCode0 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                    if (tempDDLRoleCode0.Rows.Count > 0)
                    {
                        for (int j = 0; j < tempDDLRoleCode0.Rows.Count; j++)
                        {
                            DDLRoleCode.Rows.Add(tempDDLRoleCode0.Rows[j]["OrganID"]);
                        }
                    }
                }
            }
            strRoleCode = RoleCodeArray(DDLRoleCode);
        }
        //找30
        else if (ddlRoleCode30.SelectedValue != "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "")
        {
            CommandHelper sbRoleCode = db.CreateCommandHelper();
            //
            DataTable DDLRoleCode = new DataTable();
            DDLRoleCode.Columns.Add("OrganID");
            DDLRoleCode.Rows.Add(ddlRoleCode30.SelectedValue); //加30
            sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
            sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
            sbRoleCode.Append(" WHERE RoleCode='20' AND UpOrganID =");
            sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='30' AND OrganID='" + ddlRoleCode30.SelectedValue + "' )");
            DataTable tempDDLRoleCode20 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0]; //10
            if (tempDDLRoleCode20.Rows.Count > 0)
            {
                for (int i = 0; i < tempDDLRoleCode20.Rows.Count; i++)
                {
                    DDLRoleCode.Rows.Add(tempDDLRoleCode20.Rows[i]["OrganID"]); //加20
                    sbRoleCode.Reset();
                    sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                    sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                    sbRoleCode.Append(" WHERE RoleCode='10' AND UpOrganID =");
                    sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='20' AND OrganID='" + tempDDLRoleCode20.Rows[i]["OrganID"] + "' )");
                    DataTable tempDDLRoleCode10 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                    if (tempDDLRoleCode10.Rows.Count > 0)
                    {
                        for (int j = 0; j < tempDDLRoleCode10.Rows.Count; j++)
                        {
                            DDLRoleCode.Rows.Add(tempDDLRoleCode10.Rows[j]["OrganID"]);//加10
                            sbRoleCode.Reset();
                            sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                            sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                            sbRoleCode.Append(" WHERE RoleCode='0' AND UpOrganID =");
                            sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='10' AND OrganID='" + tempDDLRoleCode10.Rows[j]["OrganID"] + "' )");
                            DataTable tempDDLRoleCode0 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                            if (tempDDLRoleCode0.Rows.Count > 0)
                            {
                                for (int k = 0; k < tempDDLRoleCode0.Rows.Count; k++)
                                {
                                    DDLRoleCode.Rows.Add(tempDDLRoleCode0.Rows[k]["OrganID"]);
                                }
                            }
                        }
                    }
                }
            }
            strRoleCode = RoleCodeArray(DDLRoleCode);
        }
        //找40
        else if (ddlRoleCode40.SelectedValue != "" && ddlRoleCode30.SelectedValue == "" && ddlRoleCode20.SelectedValue == "" && ddlRoleCode10.SelectedValue == "")
        {
            CommandHelper sbRoleCode = db.CreateCommandHelper();
            //
            DataTable DDLRoleCode = new DataTable();
            DDLRoleCode.Columns.Add("OrganID");
            DDLRoleCode.Rows.Add(ddlRoleCode40.SelectedValue); //加40
            sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
            sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
            sbRoleCode.Append(" WHERE RoleCode='30' AND UpOrganID =");
            sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='40' AND OrganID='" + ddlRoleCode40.SelectedValue + "' )");
            DataTable tempDDLRoleCode30 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0]; //10
            if (tempDDLRoleCode30.Rows.Count > 0)
            {
                for (int i = 0; i < tempDDLRoleCode30.Rows.Count; i++)
                {
                    DDLRoleCode.Rows.Add(tempDDLRoleCode30.Rows[i]["OrganID"]); //加20
                    sbRoleCode.Reset();
                    sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                    sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                    sbRoleCode.Append(" WHERE RoleCode='20' AND UpOrganID =");
                    sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='30' AND OrganID='" + tempDDLRoleCode30.Rows[i]["OrganID"] + "' )");
                    DataTable tempDDLRoleCode20 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                    if (tempDDLRoleCode20.Rows.Count > 0)
                    {
                        for (int j = 0; j < tempDDLRoleCode20.Rows.Count; j++)
                        {
                            DDLRoleCode.Rows.Add(tempDDLRoleCode20.Rows[j]["OrganID"]);
                            sbRoleCode.Reset();
                            sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                            sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                            sbRoleCode.Append(" WHERE RoleCode='10' AND UpOrganID =");
                            sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='20' AND OrganID='" + tempDDLRoleCode20.Rows[j]["OrganID"] + "' )");
                            DataTable tempDDLRoleCode10 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                            if (tempDDLRoleCode10.Rows.Count > 0)
                            {
                                for (int k = 0; k < tempDDLRoleCode10.Rows.Count; k++)
                                {
                                    DDLRoleCode.Rows.Add(tempDDLRoleCode10.Rows[k]["OrganID"]);
                                    sbRoleCode.Reset();
                                    sbRoleCode.Append(" SELECT OrganID,OrganID + '-' + OrganName AS OrganName");
                                    sbRoleCode.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow]");
                                    sbRoleCode.Append(" WHERE RoleCode='0' AND UpOrganID =");
                                    sbRoleCode.Append(" ( SELECT OrganID FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] WHERE RoleCode='10' AND OrganID='" + tempDDLRoleCode10.Rows[k]["OrganID"] + "' )");
                                    DataTable tempDDLRoleCode0 = db.ExecuteDataSet(sbRoleCode.BuildCommand()).Tables[0];
                                    if (tempDDLRoleCode0.Rows.Count > 0)
                                    {
                                        for (int l = 0; l < tempDDLRoleCode0.Rows.Count; l++)
                                        {
                                            DDLRoleCode.Rows.Add(tempDDLRoleCode0.Rows[l]["OrganID"]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            strRoleCode = RoleCodeArray(DDLRoleCode);
        }
        return strRoleCode;
    }

    /// <summary>
    /// 找尋RoleCode=40(不含)以下的主管
    /// </summary>
    protected void funRoleCodeUnder40()
    {
        DbHelper db = new DbHelper(_hrDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dtRoleCode40 = null;
        sb.Reset();
        sb.Append("SELECT OrgF.OrganID,OrgF.OrganID+'-'+OrgF.OrganName AS OrganName");
        sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
        sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
        sb.Append(" WHERE OrgF.RoleCode='40'");
        dtRoleCode40 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dtRoleCode40.Rows.Count > 0)
        {
            string UpOrganIDfor30 = "";
            for (int i = 0; i < dtRoleCode40.Rows.Count; i++)
            {
                if (i == 0)
                {
                    UpOrganIDfor30 = "'" + dtRoleCode40.Rows[i]["OrganID"] + "'";
                }
                else
                {
                    UpOrganIDfor30 += ",'" + dtRoleCode40.Rows[i]["OrganID"] + "'";
                }
            }
            sb.Reset();
            sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
            sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
            sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
            sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
            sb.Append(" WHERE OrgF.RoleCode='30'");
            sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor30 + ")");
            sb.Append(" AND OrgF.Boss='" + UserInfo.getUserInfo().UserID + "'");
            DataTable dtRoleCode30 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            //RoleCode=30主管
            if (dtRoleCode30.Rows.Count > 0)
            {
                ddlRoleCode30.Items.Clear();
                FillDDL(ddlRoleCode30, dtRoleCode30,"OrganID","OrganName");

                resetDDL(ddlRoleCode40);
                resetDDL(ddlRoleCode20);
                resetDDL(ddlRoleCode10);
            }
            else
            {
                sb.Reset();
                sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                sb.Append(" WHERE OrgF.RoleCode='30'");
                sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor30 + ")");
                dtRoleCode30.Reset();
                dtRoleCode30 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                string UpOrganIDfor20 = "";
                for (int i = 0; i < dtRoleCode30.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        UpOrganIDfor20 = "'" + dtRoleCode30.Rows[i]["OrganID"] + "'";
                    }
                    else
                    {
                        UpOrganIDfor20 += ",'" + dtRoleCode30.Rows[i]["OrganID"] + "'";
                    }
                }
                sb.Reset();
                sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                sb.Append(" WHERE OrgF.RoleCode='20'");
                sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor20 + ")");
                sb.Append(" AND OrgF.Boss='" + UserInfo.getUserInfo().UserID + "'");
                DataTable dtRoleCode20 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                //RoleCode=20主管
                if (dtRoleCode20.Rows.Count > 0)
                {
                    ddlRoleCode20.Items.Clear();
                    FillDDL(ddlRoleCode20, dtRoleCode20, "OrganID", "OrganName");

                    resetDDL(ddlRoleCode40);
                    resetDDL(ddlRoleCode30);
                    resetDDL(ddlRoleCode10);
                }
                else
                {
                    sb.Reset();
                    sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                    sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                    sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                    sb.Append(" WHERE OrgF.RoleCode='20'");
                    sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor20 + ")");
                    dtRoleCode20 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                    string UpOrganIDfor10 = "";
                    for (int i = 0; i < dtRoleCode20.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            UpOrganIDfor10 = "'" + dtRoleCode20.Rows[i]["OrganID"] + "'";
                        }
                        else
                        {
                            UpOrganIDfor10 += ",'" + dtRoleCode20.Rows[i]["OrganID"] + "'";
                        }
                    }
                    //RoleCode=10主管
                    if (dtRoleCode20.Rows.Count > 0)
                    {
                        sb.Reset();
                        sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                        sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                        sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                        sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                        sb.Append(" WHERE OrgF.RoleCode='10'");
                        sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor10 + ")");
                        sb.Append(" AND OrgF.Boss='" + UserInfo.getUserInfo().UserID + "'");
                        DataTable dtRoleCode10 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                        if (dtRoleCode10.Rows.Count > 0)
                        {
                            DataTable DDLRoleCode10;
                            DataTable DDLRoleCode0;
                            DataTable DDLRoleCodeCount = new DataTable();
                            DDLRoleCodeCount.Columns.Add("OrganID");
                            DDLRoleCodeCount.Columns.Add("OrganName");
                            DataRow[] DTRowCount = DDLRoleCodeCount.Select();
                            //查出選擇的RoleCode='20'轄下的RoleCode='10'
                            DDLRoleCode10 = dtRoleCode10;
                            //DDLRoleCode10 = at.QueryData("OrgF.OrganID,OrgF.OrganID+'-'+OrgF.OrganName AS OrganName ", _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType ", "AND OrgF.RoleCode='10' AND UpOrganID='" + ddlRoleCode20.SelectedValue + "' ");
                            for (int i = 0; i < DDLRoleCode10.Rows.Count; i++)
                            {
                                DDLRoleCodeCount.Rows.Add(DDLRoleCode10.Rows[i]["OrganID"], DDLRoleCode10.Rows[i]["OrganName"]);
                                //查出選擇的RoleCode='10'轄下的RoleCode='0'
                                DDLRoleCode0 = at.QueryData("OrgF.OrganID,'└─'+OrgF.OrganID+'-'+OrgF.OrganName AS OrganName ", _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF LEFT JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType ", "AND OrgF.RoleCode='0' AND UpOrganID='" + DDLRoleCode10.Rows[i]["OrganID"] + "' ");
                                for (int j = 0; j < DDLRoleCode0.Rows.Count; j++)
                                {
                                    DDLRoleCodeCount.Rows.Add(DDLRoleCode0.Rows[j]["OrganID"], DDLRoleCode0.Rows[j]["OrganName"]);
                                }
                            }
                            FillDDL(ddlRoleCode10, DDLRoleCodeCount, "OrganID", "OrganName");

                            resetDDL(ddlRoleCode40);
                            resetDDL(ddlRoleCode30);
                            resetDDL(ddlRoleCode20);
                        }
                        else
                        {
                            sb.Reset();
                            sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                            sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                            sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                            sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                            sb.Append(" WHERE OrgF.RoleCode='10'");
                            sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor10 + ")");
                            dtRoleCode10 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                            string UpOrganIDfor0 = "";
                            for (int i = 0; i < dtRoleCode10.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    UpOrganIDfor0 = "'" + dtRoleCode10.Rows[i]["OrganID"] + "'";
                                }
                                else
                                {
                                    UpOrganIDfor0 += ",'" + dtRoleCode10.Rows[i]["OrganID"] + "'";
                                }
                            }
                            //RoleCode=0主管
                            if (dtRoleCode10.Rows.Count > 0)
                            {
                                sb.Reset();
                                sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                                sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                                sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                                sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                                sb.Append(" WHERE OrgF.RoleCode='0'");
                                sb.Append(" AND OrgF.UpOrganID IN (" + UpOrganIDfor0 + ")");
                                sb.Append(" AND OrgF.Boss='" + UserInfo.getUserInfo().UserID + "'");
                                DataTable dtRoleCode0 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

                                if (dtRoleCode0.Rows.Count > 0)
                                {
                                    FillDDL(ddlRoleCode10, dtRoleCode0, "OrganID", "OrganName");
                                    sb.Reset();
                                    sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                                    sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                                    sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                                    sb.Append(" WHERE OrgF.RoleCode='10' AND OrgF.OrganID='" + dtRoleCode0.Rows[0]["UpOrganID"]+ "'");
                                    dtRoleCode20 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                                    FillDDLWithOutChoose(ddlRoleCode20, dtRoleCode20, "UpOrganID", "UpOrganName");

                                    sb.Reset();
                                    sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                                    sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                                    sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                                    sb.Append(" WHERE OrgF.RoleCode='20' AND OrgF.OrganID='" + dtRoleCode20.Rows[0]["UpOrganID"] + "'");
                                    dtRoleCode30 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                                    FillDDLWithOutChoose(ddlRoleCode30, dtRoleCode30, "UpOrganID", "UpOrganName");

                                    sb.Reset();
                                    sb.Append("SELECT OrgF.OrganID, OrgF.OrganID + '-' + OrgF.OrganName AS OrganName, OrgF.UpOrganID AS UpOrganID, OrgF.UpOrganID + '-' + OrgF2.OrganName AS UpOrganName");
                                    sb.Append(" FROM " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF");
                                    sb.Append(" LEFT JOIN " + _eHRMSDB + ".[dbo].[OrganizationFlow] AS OrgF2 ON OrgF2.OrganID=OrgF.UpOrganID");
                                    sb.Append(" JOIN " + _eHRMSDB + ".[dbo].[HRCodeMap] AS H ON H.TabName='Business' AND H.FldName='BusinessType' AND H.NotShowFlag='0' and H.Code=OrgF.BusinessType");
                                    sb.Append(" WHERE OrgF.RoleCode='30' AND OrgF.OrganID='" + dtRoleCode30.Rows[0]["UpOrganID"] + "'");
                                    dtRoleCode40 = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
                                    FillDDLWithOutChoose(ddlRoleCode40, dtRoleCode40, "UpOrganID", "UpOrganName");
                                }
                                else
                                {
                                    resetDDL(ddlRoleCode40);
                                    resetDDL(ddlRoleCode30);
                                    resetDDL(ddlRoleCode20);
                                    resetDDL(ddlRoleCode10);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 明細查詢加班日期檢測
    /// 加班起日日否大於迄日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OTDateBegin_TextChanged(object sender, EventArgs e) //開始日期(明細)
    {
        if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
        {
            if (Convert.ToDateTime(txtOTDateBegin.ucSelectedDate) > Convert.ToDateTime(txtOTDateEnd.ucSelectedDate))
            {
                Util.MsgBox("加班日期起日大於迄日");
                txtOTDateBegin.ucSelectedDate = "";
                txtOTDateEnd.ucSelectedDate = "";
            }
        }
    }

    /// <summary>
    /// 明細查詢加班日期檢測
    /// 加班起日日否大於迄日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OTDateEnd_TextChanged(object sender, EventArgs e) //結束日期(明細)
    {
        if (txtOTDateBegin.ucSelectedDate != "" && txtOTDateEnd.ucSelectedDate != "")
        {
            if (Convert.ToDateTime(txtOTDateBegin.ucSelectedDate) > Convert.ToDateTime(txtOTDateEnd.ucSelectedDate))
            {
                Util.MsgBox("加班日期起日大於迄日");
                txtOTDateBegin.ucSelectedDate = "";
                txtOTDateEnd.ucSelectedDate = "";
            }
        }
    }

    /// <summary>
    /// 統計查詢加班日期檢測
    /// 加班起日日否大於迄日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OTDateBeginCount_TextChanged(object sender, EventArgs e) //開始日期(統計)
    {
        if(txtOTDateBeginCount.ucSelectedDate != "" && txtOTDateEndCount.ucSelectedDate != "")
        {
            if (Convert.ToDateTime(txtOTDateBeginCount.ucSelectedDate) > Convert.ToDateTime(txtOTDateEndCount.ucSelectedDate))
            {
                Util.MsgBox("加班日期起日大於迄日");
                txtOTDateBeginCount.ucSelectedDate = "";
                txtOTDateEndCount.ucSelectedDate = "";
            }
        }
    }

    /// <summary>
    /// 統計查詢加班日期檢測
    /// 加班起日日否大於迄日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void OTDateEndCount_TextChanged(object sender, EventArgs e) //結束日期(統計)
    {
        if (txtOTDateBeginCount.ucSelectedDate != "" && txtOTDateEndCount.ucSelectedDate != "")
        {
            if (Convert.ToDateTime(txtOTDateBeginCount.ucSelectedDate) > Convert.ToDateTime(txtOTDateEndCount.ucSelectedDate) )
            {
                Util.MsgBox("加班日期起日大於迄日");
                txtOTDateBeginCount.ucSelectedDate = "";
                txtOTDateEndCount.ucSelectedDate = "";
            }
        }
    }

    /// <summary>
    /// 把RoleCode串成字串作為搜尋條件
    /// </summary>
    /// <param name="DDLRoleCode"></param>
    /// <returns></returns>
    protected string RoleCodeArray(DataTable DDLRoleCode)
    {
        string strRoleCode = "";
        for (int i = 0; i < DDLRoleCode.Rows.Count; i++)
        {
            if (i == 0)
            {
                strRoleCode = "'" + DDLRoleCode.Rows[i]["OrganID"] + "'";
            }
            else
            {
                strRoleCode += ",'" + DDLRoleCode.Rows[i]["OrganID"] + "'";
            }
        }
        return strRoleCode;
    }

    /// <summary>
    /// 重設DDL，給予請選擇
    /// </summary>
    /// <param name="DDL"></param>
    protected void resetDDL(DropDownList DDL)
    {
        DDL.Items.Clear();
        DDL.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    /// <summary>
    /// 塞DDL
    /// </summary>
    /// <param name="DDL"></param>
    /// <param name="DT"></param>
    /// <param name="strID"></param>
    /// <param name="strName"></param>
    protected void FillDDL(DropDownList DDL, DataTable DT, string strID, string strName)
    {
        DDL.DataSource = DT;
        DDL.DataValueField = strID;
        DDL.DataTextField = strName;
        DDL.DataBind();
        DDL.Items.Insert(0, new ListItem("---請選擇---", ""));
    }

    protected void FillDDLWithOutChoose(DropDownList DDL, DataTable DT, string strID, string strName)
    {
        DDL.DataSource = DT;
        DDL.DataValueField = strID;
        DDL.DataTextField = strName;
        DDL.DataBind();
    }

    //DataTable過濾條件
    private DataTable DataTableFilterSort(DataTable oTable, string filterExpression, string sortExpression)
    {
        DataTable nTable = new DataTable();
        if (oTable.Select(filterExpression, sortExpression).Length != 0)
        {
            nTable = oTable.Select(filterExpression, sortExpression).CopyToDataTable();
        }
        return nTable;
    }
}