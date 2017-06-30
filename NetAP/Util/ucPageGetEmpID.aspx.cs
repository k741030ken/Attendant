//20170524-leo
//給ucGetEmpID.ascx使用
//快速查詢EmpID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

public partial class Util_ucPageGetEmpID : System.Web.UI.Page
{
    #region"全域"
    //private static string _attendantDBName = Aattendant._AattendantDBName;
    //private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;
    private static List<Util_OrganDataBean> OrganDataTemp = new List<Util_OrganDataBean>();
    private static List<Util_EmpDataBean> EmpDataTemp = new List<Util_EmpDataBean>();
    #endregion"全域"

    #region"傳入"
    public string _CompID
    {
        get
        {
            if (ViewState["_CompID"] == null)
            {
                ViewState["_CompID"] = "";
            }
            return (string)(ViewState["_CompID"]);
        }
        set
        {
            ViewState["_CompID"] = value;
        }
    }
    public string _DeptID
    {
        get
        {
            if (ViewState["_DeptID"] == null)
            {
                ViewState["_DeptID"] = "";
            }
            return (string)(ViewState["_DeptID"]);
        }
        set
        {
            ViewState["_DeptID"] = value;
        }
    }
    public string _OrganID
    {
        get
        {
            if (ViewState["_OrganID"] == null)
            {
                ViewState["_OrganID"] = "";
            }
            return (string)(ViewState["_OrganID"]);
        }
        set
        {
            ViewState["_OrganID"] = value;
        }
    }
    public string _EmpID
    {
        get
        {
            if (ViewState["_EmpID"] == null)
            {
                ViewState["_EmpID"] = "";
            }
            return (string)(ViewState["_EmpID"]);
        }
        set
        {
            ViewState["_EmpID"] = value;
        }
    }
    public string _outEmpID
    {
        get
        {
            if (ViewState["_outEmpID"] == null)
            {
                ViewState["_outEmpID"] = "";
            }
            return (string)(ViewState["_outEmpID"]);
        }
        set
        {
            ViewState["_outEmpID"] = value;
        }
    }
    public string ModalPopupClientID
    {
        get
        {
            if (hidModalPopupClientID.Value == null)
            {
                hidModalPopupClientID.Value = "";
            }
            return hidModalPopupClientID.Value.ToString();
        }
        set
        {
            hidModalPopupClientID.Value = value;
        }
    }
    #endregion"傳入"

    #region "Bean"
    public class Util_OrganDataBean
    {
        /// <summary>
        /// 選擇公司ID
        /// </summary>
        public String CompID { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        public String CompName { get; set; }

        /// <summary>
        /// 所屬一級部門
        /// </summary>
        public String DeptID { get; set; }
        /// <summary>
        /// 所屬一級部門名稱
        /// </summary>
        public String DeptName { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        public String OrganID { get; set; }
        /// <summary>
        /// 部門名稱
        /// </summary>
        public String OrganName { get; set; }
    }

    public class Util_EmpDataBean //請取有意義的名稱+Bean結尾
    {
        /// <summary>
        /// 人員公司ID
        /// </summary>
        public String CompID { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public String CompName { get; set; }

        /// <summary>
        /// 公出人員ID
        /// </summary>
        public String EmpID { get; set; }

        /// <summary>
        /// 公出人員姓名
        /// </summary>
        public String EmpNameN { get; set; }

        /// <summary>
        /// 公出部門ID
        /// </summary>
        public String DeptID { get; set; }

        /// <summary>
        /// 公出部門名稱
        /// </summary>
        public String DeptName { get; set; }

        /// <summary>
        /// 公出單位ID
        /// </summary>
        public String OrganID { get; set; }

        /// <summary>
        /// 公出單位名稱
        /// </summary>
        public String OrganName { get; set; }

        /// <summary>
        /// 公出人員工作性質ID
        /// </summary>
        public String WorkTypeID { get; set; }

        /// <summary>
        /// 公出人員工作性質代號
        /// </summary>
        public String WorkType { get; set; }

        /// <summary>
        /// 公出單位ID(功能)
        /// </summary>
        public String FlowOrganID { get; set; }

        /// <summary>
        /// 公出單位名稱(功能)
        /// </summary>
        public String FlowOrganName { get; set; }

        /// <summary>
        /// 公出人員職稱ID
        /// </summary>
        public String TitleID { get; set; }

        /// <summary>
        /// 公出人員職稱
        /// </summary>
        public String TitleName { get; set; }

        /// <summary>
        /// 公出人員職位ID
        /// </summary>
        public String PositionID { get; set; }

        /// <summary>
        /// 公出人員職位
        /// </summary>
        public String Position { get; set; }

        /// <summary>
        /// 排除人員
        /// </summary>
        public String outEmpID { get; set; }

        /// <summary>
        /// gridview排序編號
        /// </summary>
        public String _Key { get; set; }

        /// <summary>
        /// ddlEmpID顯示資料
        /// </summary>
        public String ddlEmpName { get; set; }
    }
    #endregion "Bean"

    #region"Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _CompID = (Request["_CompID"] != null) ? Request["_CompID"].ToString() : "";
            _DeptID = (Request["_DeptID"] != null) ? Request["_DeptID"].ToString() : "";
            _OrganID = (Request["_OrganID"] != null) ? Request["_OrganID"].ToString() : "";
            _outEmpID = (Request["_outEmpID"] != null) ? Request["_outEmpID"].ToString() : "";
            ModalPopupClientID = (Request["ModalPopupClientID"] != null) ? Request["ModalPopupClientID"].ToString() : "ucGetEmpID";

            if (_CompID == "" && (_DeptID != "" || _OrganID != ""))
            {
                _CompID = UserInfo.getUserInfo().CompID;
            }

            DDLChanged("");

            if (!String.IsNullOrEmpty(_CompID))
            {
                DDLChanged("CompID");
                if (SetSelectedIndex(ddlEmpCompID, _CompID))
                {
                    ddlEmpCompID.Enabled = false;
                }
            }

            if (!String.IsNullOrEmpty(_OrganID))
            {
                DDLChanged("OrganID");
                if (SetSelectedIndex(ddlEmpOrganID, _OrganID))
                {
                    ddlEmpCompID.Enabled = false;
                    ddlEmpOrganID.Enabled = false;
                }
            }
        }
    }
    #endregion"Page_Load"

    #region"OrganData"
    public static bool LoadOrgan(Util_OrganDataBean model, out List<Util_OrganDataBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<Util_OrganDataBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<Util_OrganDataBean>(LoadOrganSQL(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    public static string LoadOrganSQL(Util_OrganDataBean model)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("select DISTINCT C.CompID,C.CompID+'-'+C.CompName as CompName ");
        sb.AppendLine(",O.OrganID,O.OrganID+'-'+Org.OrganName as OrganName ");
        sb.AppendLine(",O.DeptID,O.DeptID+'-'+Dpt.OrganName as DeptName ");

        sb.AppendLine("from dbo.Company C ");
        sb.AppendLine("left join dbo.Organization O on O.CompID=C.CompID ");
        sb.AppendLine("left join dbo.Organization Dpt on O.CompID=Dpt.CompID and O.DeptID=Dpt.OrganID");
        sb.AppendLine("left join dbo.Organization Org on O.CompID=Org.CompID and O.OrganID=Org.OrganID ");

        sb.AppendLine("where O.InValidFlag='0' and O.VirtualFlag='0' ");

        return sb.ToString();
    }
    #endregion"OrganData"

    #region"EmpData"
    public static bool LoadEmp(Util_EmpDataBean model, out List<Util_EmpDataBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<Util_EmpDataBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<Util_EmpDataBean>(LoadEmpSQL(model), model).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    public static string LoadEmpSQL(Util_EmpDataBean model)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("select isnull(P.CompID,'') as CompID,isnull(C.CompName,'') as CompName ");
        sb.AppendLine(",isnull(P.EmpID,'') as EmpID,isnull(P.NameN,'') as EmpNameN ,isnull(P.EmpID+'-'+P.NameN,'') as ddlEmpName ");
        sb.AppendLine(",isnull(P.DeptID,'') as DeptID,isnull(Dpt.OrganName,'') as DeptName ");
        sb.AppendLine(",isnull(P.OrganID,'') as OrganID,isnull(Org.OrganName,'') as OrganName ");
        sb.AppendLine(",isnull(WW.WorkTypeID,'') as WorkTypeID,isnull(WW.Remark,'') as WorkType ");
        sb.AppendLine(",isnull(PP.PositionID,'') as PositionID,isnull(PP.Remark,'') as Position ");
        sb.AppendLine(",isnull(OrgF.OrganID,'') as FlowOrganID,isnull(OrgF.OrganName,'') as FlowOrganName ");
        sb.AppendLine(",isnull(P.TitleID,'') as TitleID,isnull(T.TitleName,'') as TitleName ");
        sb.AppendLine(",right(REPLICATE('0',8)+ CAST( ROW_NUMBER() over (order by P.EmpID) as VARCHAR),8) as _Key ");
        sb.AppendLine("from dbo.Personal P ");
        sb.AppendLine("left join dbo.EmpFlow EF on P.CompID=EF.CompID and P.EmpID=EF.EmpID ");
        sb.AppendLine("left join dbo.OrganizationFlow OrgF on EF.OrganID=OrgF.OrganID ");
        sb.AppendLine("left join dbo.EmpWorkType EW on P.CompID=EW.CompID and P.EmpID=EW.EmpID and EW.PrincipalFlag='1' ");
        sb.AppendLine("left join dbo.WorkType WW on EW.CompID=WW.CompID and EW.WorkTypeID=WW.WorkTypeID ");
        sb.AppendLine("left join dbo.EmpPosition EP on P.CompID=EP.CompID and P.EmpID=EP.EmpID and EP.PrincipalFlag='1' ");
        sb.AppendLine("left join dbo.Position PP on EP.CompID=PP.CompID and EP.PositionID=PP.PositionID ");
        sb.AppendLine("left join dbo.Company C on P.CompID=C.CompID ");
        sb.AppendLine("left join dbo.Organization Dpt on P.CompID=Dpt.CompID and P.DeptID=Dpt.OrganID ");
        sb.AppendLine("left join dbo.Organization Org on P.CompID=Org.CompID and P.OrganID=Org.OrganID ");
        sb.AppendLine("left join dbo.Title T on P.CompID=T.CompID and P.TitleID=T.TitleID and P.RankID=T.RankID ");
        sb.AppendLine("where P.WorkStatus='1' ");

        if (!String.IsNullOrEmpty(model.CompID))
            sb.AppendLine("and P.CompID=@CompID");

        if (!String.IsNullOrEmpty(model.OrganID))
            sb.AppendLine("and P.OrganID=@OrganID");

        if (!String.IsNullOrEmpty(model.EmpID))
            sb.AppendLine("and P.EmpID like '%" + model.EmpID + "%'");

        if (!String.IsNullOrEmpty(model.outEmpID))
            sb.AppendLine("and P.EmpID!=@outEmpID");

        return sb.ToString();
    }
    #endregion"EmpData"

    #region"下拉功能"

    private bool SetSelectedIndex(DropDownList objDDL, string Value)
    {
        objDDL.SelectedIndex = objDDL.Items.IndexOf(objDDL.Items.FindByValue(Value));
        return objDDL.SelectedIndex <= 0 ? false : true;
    }

    private void DDLEnabled(string e)
    {
        switch (e)
        {
            case "OrganID":
                ddlEmpOrganID.Enabled = false;
                goto case "CompID";
            case "CompID":
                ddlEmpCompID.Enabled = false;
                break;
        }
    }

    private void DDLChanged(string e)
    {
        var isSuccess = false;
        var msg = "";
        var OrganData = new Util_OrganDataBean();
        var EmpDataLists = new List<Util_EmpDataBean>();
        var EmpData = new Util_EmpDataBean();
        EmpData.outEmpID = _outEmpID;
        switch (e)
        {
            case "":
                DDLClear(e);
                isSuccess = LoadOrgan(OrganData, out OrganDataTemp, out msg);
                if (isSuccess && OrganDataTemp != null && OrganDataTemp.Count > 0)
                {
                    ddlEmpCompID.DataSource = OrganDataTemp
                        .Select(x => new { x.CompID, x.CompName }).Distinct().ToList();
                    ddlEmpCompID.DataTextField = "CompName";
                    ddlEmpCompID.DataValueField = "CompID";
                    ddlEmpCompID.DataBind();
                    ddlEmpCompID.Items.Insert(0, new ListItem("---請選擇---", ""));
                    SetSelectedIndex(ddlEmpCompID, _CompID);
                }
                break;

            case "CompID":
                ddlEmpOrganID.DataSource = OrganDataTemp
                    .Where(x => x.CompID == ddlEmpCompID.SelectedValue)
                    .Select(x => new { x.OrganID, x.OrganName }).Distinct().ToList();
                ddlEmpOrganID.DataTextField = "OrganName";
                ddlEmpOrganID.DataValueField = "OrganID";
                ddlEmpOrganID.DataBind();
                ddlEmpOrganID.Items.Insert(0, new ListItem("---請選擇---", ""));
                SetSelectedIndex(ddlEmpOrganID, _OrganID);
                break;

            case "OrganID":
                //撈取DDLEmpID的內容----------------
                EmpData.CompID = ddlEmpCompID.SelectedValue;
                EmpData.OrganID = ddlEmpOrganID.SelectedValue;
                isSuccess = LoadEmp(EmpData, out EmpDataTemp, out msg);
                if (isSuccess && EmpDataTemp != null && EmpDataTemp.Count > 0)
                {
                    ddlEmpID.DataSource = EmpDataTemp.Select(x => new { x.EmpID, x.ddlEmpName }).Distinct().ToList();
                    ddlEmpID.DataTextField = "ddlEmpName";
                    ddlEmpID.DataValueField = "EmpID";
                    ddlEmpID.DataBind();
                    ddlEmpID.Items.Insert(0, new ListItem("---請選擇---", ""));
                }
                break;

            case "EmpID":
                if (EmpDataTemp != null && EmpDataTemp.Count > 0)
                {
                    var EmpTemp = EmpDataTemp.Find(x => x.EmpID.Contains(ddlEmpID.SelectedValue));
                    Dictionary<string, string> dctEmpData = new Dictionary<string, string>();
                    dctEmpData.Add("CompID", EmpTemp.CompID);
                    dctEmpData.Add("CompName", EmpTemp.CompName);
                    dctEmpData.Add("EmpID", EmpTemp.EmpID);
                    dctEmpData.Add("EmpNameN", EmpTemp.EmpNameN);
                    dctEmpData.Add("DeptID", EmpTemp.DeptID);
                    dctEmpData.Add("DeptName", EmpTemp.DeptName);
                    dctEmpData.Add("OrganID", EmpTemp.OrganID);
                    dctEmpData.Add("OrganName", EmpTemp.OrganName);
                    dctEmpData.Add("WorkTypeID", EmpTemp.WorkTypeID);
                    dctEmpData.Add("WorkType", EmpTemp.WorkType);
                    dctEmpData.Add("PositionID", EmpTemp.PositionID);
                    dctEmpData.Add("Position", EmpTemp.Position);
                    dctEmpData.Add("FlowOrganID", EmpTemp.FlowOrganID);
                    dctEmpData.Add("FlowOrganName", EmpTemp.FlowOrganName);
                    dctEmpData.Add("TitleID", EmpTemp.TitleID);
                    dctEmpData.Add("TitleName", EmpTemp.TitleName);
                    ViewState[ModalPopupClientID+"_Data"] = dctEmpData;
                }
                break;
        }
    }
    private void DDLClear(string e)
    {
        switch (e)
        {
            case "CompID":
                ddlEmpOrganID.Items.Clear();
                goto case "OrganID";

            case "OrganID":
                ddlEmpID.Items.Clear();
                break;
        }
    }
    protected void ddlEmpCompID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLClear("CompID");
        if (ddlEmpCompID.SelectedValue.Trim() != "")
        {
            DDLChanged("CompID");
        }
    }

    protected void ddlEmpOrganID_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLClear("OrganID");
        if (ddlEmpCompID.SelectedValue.Trim() != "")
        {
            DDLChanged("OrganID");
        }
    }

    protected void ddlEmpID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpOrganID.SelectedValue.Trim() != "")
        {
            DDLChanged("EmpID");
            Session[ModalPopupClientID+"_Data"] = ViewState[ModalPopupClientID+"_Data"];
            Dictionary<string, string> EmpData = (Dictionary<string, string>)Session[ModalPopupClientID+"_Data"];
        }
    }
    #endregion"下拉功能"

    #region"按鈕"
    private void GoBack(string e)
    {
        if (e.ToUpper() == "OK".ToUpper())
        {
            Dictionary<string, string> dctEmpData = (Dictionary<string, string>)ViewState[ModalPopupClientID+"_Data"];
            Session[ModalPopupClientID+"_Data"] = ViewState[ModalPopupClientID+"_Data"];
        }
        //else if (e.ToUpper() == "Cancel".ToUpper())
        //{
        //    Session[ModalPopupClientID+"_Data"] = null;
        //}
        ScriptManager.RegisterStartupScript(Page, GetType(), "CloseWindow", "CloseWindow()", true);
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        GoBack("OK");
    }

    #endregion"按鈕"

    #region"快速查詢"
    //查詢+顯示
    private void DoQuery()
    {
        //lblMsg.Visible = false;
        var isSuccess = false;
        var msg = "";
        var EmpDataLists = new List<Util_EmpDataBean>();
        var EmpData = new Util_EmpDataBean();
        EmpData.EmpID = txtQueryString.Text;
        EmpData.outEmpID = _outEmpID;
        isSuccess = LoadEmp(EmpData, out EmpDataTemp, out msg);
        if (isSuccess && EmpDataTemp != null )
        {
            if (EmpDataTemp.Count == 1)
            {
                SetSelectedIndex(ddlEmpCompID, EmpDataTemp[0].CompID);
                DDLChanged("CompID");
                SetSelectedIndex(ddlEmpOrganID, EmpDataTemp[0].OrganID);
                DDLChanged("OrganID");
                SetSelectedIndex(ddlEmpID, EmpDataTemp[0].EmpID);
                DDLChanged("EmpID");
            }
            else
            {
                gvMain.DataSource = EmpDataTemp;
                gvMain.DataBind();
                gvMain.Visible = true;
            }
        }
    }
    //查詢按鈕
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DoQuery();
    }

    ////取得index
    //public int getDataFieldColumnIndex(GridView gv, string DataColumnName)
    //{
    //    DataControlFieldCollection Columns = gv.Columns;
    //    int columnIndex = -1;
    //    foreach (DataControlField field in Columns)
    //    {
    //        if (field is System.Web.UI.WebControls.BoundField)
    //        {
    //            if (((BoundField)field).DataField == DataColumnName)
    //            {
    //                columnIndex = Columns.IndexOf(field);
    //            }
    //        }
    //    }
    //    return columnIndex;
    //}

    //gridview的btn[選取]事件
    protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {
            Button btn = (Button)e.CommandSource;
            GridViewRow gvMain_Row = (GridViewRow)btn.NamingContainer;
            Dictionary<string, string> dctEmpData = new Dictionary<string, string>();
            dctEmpData.Add("CompID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[0].ToString());
            dctEmpData.Add("CompName", gvMain.DataKeys[gvMain_Row.RowIndex].Values[1].ToString());
            dctEmpData.Add("EmpID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[2].ToString());
            dctEmpData.Add("EmpNameN", gvMain.DataKeys[gvMain_Row.RowIndex].Values[3].ToString());
            dctEmpData.Add("DeptID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[4].ToString());
            dctEmpData.Add("DeptName", gvMain.DataKeys[gvMain_Row.RowIndex].Values[5].ToString());
            dctEmpData.Add("OrganID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[6].ToString());
            dctEmpData.Add("OrganName", gvMain.DataKeys[gvMain_Row.RowIndex].Values[7].ToString());
            dctEmpData.Add("WorkTypeID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[8].ToString());
            dctEmpData.Add("WorkType", gvMain.DataKeys[gvMain_Row.RowIndex].Values[9].ToString());
            dctEmpData.Add("PositionID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[10].ToString());
            dctEmpData.Add("Position", gvMain.DataKeys[gvMain_Row.RowIndex].Values[11].ToString());
            dctEmpData.Add("FlowOrganID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[12].ToString());
            dctEmpData.Add("FlowOrganName", gvMain.DataKeys[gvMain_Row.RowIndex].Values[13].ToString());
            dctEmpData.Add("TitleID", gvMain.DataKeys[gvMain_Row.RowIndex].Values[14].ToString());
            dctEmpData.Add("TitleName", gvMain.DataKeys[gvMain_Row.RowIndex].Values[15].ToString());
            ViewState[ModalPopupClientID + "_Data"] = dctEmpData;
            GoBack("OK");
        }
    }
    #endregion"快速查詢"

    protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMain.PageIndex = e.NewPageIndex;
        DoQuery();
    }
}