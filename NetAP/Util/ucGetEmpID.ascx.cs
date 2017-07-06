using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common;
using System.Data.SqlClient;
using System.Text;

public partial class Util_ucGetEmpID : System.Web.UI.UserControl
{
    #region"全域"
    //private static string _attendantDBName = Aattendant._AattendantDBName;
    //private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;
    #endregion"全域"

    #region"傳入"
    public string selectCompID
    {
        get
        {
            if (ViewState["selectCompID"] == null)
            {
                ViewState["selectCompID"] = "";
            }
            return (string)(ViewState["selectCompID"]);
        }
        set
        {
            ViewState["selectCompID"] = value;
        }
    }
    public string selectOrgTypeID
    {
        get
        {
            if (ViewState["selectOrgTypeID"] == null)
            {
                ViewState["selectOrgTypeID"] = "";
            }
            return (string)(ViewState["selectOrgTypeID"]);
        }
        set
        {
            ViewState["selectOrgTypeID"] = value;
        }
    }
    public string selectDeptID
    {
        get
        {
            if (ViewState["selectDeptID"] == null)
            {
                ViewState["selectDeptID"] = "";
            }
            return (string)(ViewState["selectDeptID"]);
        }
        set
        {
            ViewState["selectDeptID"] = value;
        }
    }
    public string selectOrganID
    {
        get
        {
            if (ViewState["selectOrganID"] == null)
            {
                ViewState["selectOrganID"] = "";
            }
            return (string)(ViewState["selectOrganID"]);
        }
        set
        {
            ViewState["selectOrganID"] = value;
        }
    }
    public string selectOutEmpID
    {
        get
        {
            if (ViewState["selectOutEmpID"] == null)
            {
                ViewState["selectOutEmpID"] = "";
            }
            return (string)(ViewState["selectOutEmpID"]);
        }
        set
        {
            ViewState["selectOutEmpID"] = value;
        }
    }
    public string ModalPopupClientID
    {
        get
        {
            return this.ucEmpModalPopup.ClientID.Split("_")[0];
        }
    }
    #endregion"傳入"

    #region"回傳"
    /// <summary>
    /// 人員公司ID
    /// </summary>
    public string reCompID
    {
        get
        {
            if (ViewState["CompID"] == null)
            {
                ViewState["CompID"] = "";
            }
            return (string)(ViewState["CompID"]);
        }
        set
        {
            ViewState["CompID"] = value;
        }
    }
    /// <summary>
    /// 公司名稱
    /// </summary>
    public string reCompName
    {
        get
        {
            if (ViewState["CompName"] == null)
            {
                ViewState["CompName"] = "";
            }
            return (string)(ViewState["CompName"]);
        }
        set
        {
            ViewState["CompName"] = value;
        }
    }
    /// <summary>
    /// 公出人員ID
    /// </summary>
    public string reEmpID
    {
        get
        {
            if (ViewState["EmpID"] == null)
            {
                ViewState["EmpID"] = "";
            }
            return (string)(ViewState["EmpID"]);
        }
        set
        {
            ViewState["EmpID"] = value;
        }
    }
    /// <summary>
    /// 公出人員姓名
    /// </summary>
    public string reEmpNameN
    {
        get
        {
            if (ViewState["EmpNameN"] == null)
            {
                ViewState["EmpNameN"] = "";
            }
            return (string)(ViewState["EmpNameN"]);
        }
        set
        {
            ViewState["EmpNameN"] = value;
        }
    }
    /// <summary>
    /// 公出部門ID
    /// </summary>
    public string reDeptID
    {
        get
        {
            if (ViewState["DeptID"] == null)
            {
                ViewState["DeptID"] = "";
            }
            return (string)(ViewState["DeptID"]);
        }
        set
        {
            ViewState["DeptID"] = value;
        }
    }
    /// <summary>
    /// 公出部門名稱
    /// </summary>
    public string reDeptName
    {
        get
        {
            if (ViewState["DeptName"] == null)
            {
                ViewState["DeptName"] = "";
            }
            return (string)(ViewState["DeptName"]);
        }
        set
        {
            ViewState["DeptName"] = value;
        }
    }
    /// <summary>
    /// 公出單位ID
    /// </summary>
    public string reOrganID
    {
        get
        {
            if (ViewState["OrganID"] == null)
            {
                ViewState["OrganID"] = "";
            }
            return (string)(ViewState["OrganID"]);
        }
        set
        {
            ViewState["OrganID"] = value;
        }
    }
    /// <summary>
    /// 公出單位名稱
    /// </summary>
    public string reOrganName
    {
        get
        {
            if (ViewState["OrganName"] == null)
            {
                ViewState["OrganName"] = "";
            }
            return (string)(ViewState["OrganName"]);
        }
        set
        {
            ViewState["OrganName"] = value;
        }
    }
    /// <summary>
    /// 公出人員工作性質ID
    /// </summary>
    public string reWorkTypeID
    {
        get
        {
            if (ViewState["WorkTypeID"] == null)
            {
                ViewState["WorkTypeID"] = "";
            }
            return (string)(ViewState["WorkTypeID"]);
        }
        set
        {
            ViewState["WorkTypeID"] = value;
        }
    }
    /// <summary>
    /// 公出人員工作性質代號
    /// </summary>
    public string reWorkType
    {
        get
        {
            if (ViewState["WorkType"] == null)
            {
                ViewState["WorkType"] = "";
            }
            return (string)(ViewState["WorkType"]);
        }
        set
        {
            ViewState["WorkType"] = value;
        }
    }
    /// <summary>
    /// 公出單位ID(功能)
    /// </summary>
    public string reFlowOrganID
    {
        get
        {
            if (ViewState["FlowOrganID"] == null)
            {
                ViewState["FlowOrganID"] = "";
            }
            return (string)(ViewState["FlowOrganID"]);
        }
        set
        {
            ViewState["FlowOrganID"] = value;
        }
    }
    /// <summary>
    /// 公出單位名稱(功能)
    /// </summary>
    public string reFlowOrganName
    {
        get
        {
            if (ViewState["FlowOrganName"] == null)
            {
                ViewState["FlowOrganName"] = "";
            }
            return (string)(ViewState["FlowOrganName"]);
        }
        set
        {
            ViewState["FlowOrganName"] = value;
        }
    }
    /// <summary>
    /// 公出人員職稱ID
    /// </summary>
    public string reTitleID
    {
        get
        {
            if (ViewState["TitleID"] == null)
            {
                ViewState["TitleID"] = "";
            }
            return (string)(ViewState["TitleID"]);
        }
        set
        {
            ViewState["TitleID"] = value;
        }
    }
    /// <summary>
    /// 公出人員職稱
    /// </summary>
    public string reTitleName
    {
        get
        {
            if (ViewState["TitleName"] == null)
            {
                ViewState["TitleName"] = "";
            }
            return (string)(ViewState["TitleName"]);
        }
        set
        {
            ViewState["TitleName"] = value;
        }
    }
    /// <summary>
    /// 公出人員職位ID
    /// </summary>
    public string rePositionID
    {
        get
        {
            if (ViewState["PositionID"] == null)
            {
                ViewState["PositionID"] = "";
            }
            return (string)(ViewState["PositionID"]);
        }
        set
        {
            ViewState["PositionID"] = value;
        }
    }
    /// <summary>
    /// 公出人員職位
    /// </summary>
    public string rePosition
    {
        get
        {
            if (ViewState["Position"] == null)
            {
                ViewState["Position"] = "";
            }
            return (string)(ViewState["Position"]);
        }
        set
        {
            ViewState["Position"] = value;
        }
    }
    #endregion"回傳"

    #region"Bean"
    //public class Util_EmpNameDataBean
    //{
    //    /// <summary>
    //    /// 公司ID
    //    /// </summary>
    //    public String CompID { get; set; }

    //    /// <summary>
    //    /// 人員ID
    //    /// </summary>
    //    public String EmpID { get; set; }

    //    /// <summary>
    //    /// 人員姓名
    //    /// </summary>
    //    public String EmpNameN { get; set; }
    //}
    public class Util_EmpNameDataBean //請取有意義的名稱+Bean結尾
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
    }

    #endregion"Bean"

    #region"EmpNameData_SQL"
    public static bool LoadEmpName(Util_EmpNameDataBean model, out List<Util_EmpNameDataBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<Util_EmpNameDataBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                try
                {
                    datas = conn.Query<Util_EmpNameDataBean>(LoadEmpNameSQL(model), model).ToList();
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

    public static string LoadEmpNameSQL(Util_EmpNameDataBean model)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("select  Top 1 P.CompID,C.CompName ");
        sb.AppendLine(",P.EmpID,P.NameN as EmpNameN ");
        sb.AppendLine(",P.DeptID,Dpt.OrganName as DeptName ");
        sb.AppendLine(",P.OrganID,Org.OrganName ");
        sb.AppendLine(",WW.WorkTypeID,WW.Remark as WorkType ");
        sb.AppendLine(",PP.PositionID,PP.Remark as Position ");
        sb.AppendLine(",OrgF.OrganID as FlowOrganID,Org.OrganName as FlowOrganName ");
        sb.AppendLine(",P.TitleID,T.TitleName ");
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
        sb.AppendLine("where P.WorkStatus='1' and P.CompID =@CompID and P.EmpID=@EmpID ");
        if (!string.IsNullOrEmpty(model.outEmpID))
        {
            sb.AppendLine("and P.EmpID!=@outEmpID");
        }
        return sb.ToString();
    }
    #endregion"EmpNameDataSQL"

    #region"Page_Load"
    //public void DoAction()
    //{ 
    
    //}

    //protected void Page_Init(object sender, EventArgs e)
    //{
    //    Dictionary<string, string> EmpData = (Dictionary<string, string>)Session[ModalPopupClientID+"_Data"];
    //    if (EmpData != null)
    //    {
    //        reCompID = EmpData["CompID"];
    //        reCompName = EmpData["CompName"];
    //        reEmpID = EmpData["EmpID"];
    //        reEmpNameN = EmpData["EmpNameN"];
    //        reDeptID = EmpData["DeptID"];
    //        reDeptName = EmpData["DeptName"];
    //        reOrganID = EmpData["OrganID"];
    //        reOrganName = EmpData["OrganName"];
    //        reWorkTypeID = EmpData["WorkTypeID"];
    //        reWorkType = EmpData["WorkType"];
    //        reFlowOrganID = EmpData["FlowOrganID"];
    //        reFlowOrganName = EmpData["FlowOrganName"];
    //        reTitleID = EmpData["TitleID"];
    //        reTitleName = EmpData["TitleName"];
    //        rePositionID = EmpData["PositionID"];
    //        rePosition = EmpData["Position"];
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        Dictionary<string, string> EmpData = (Dictionary<string, string>)Session[ModalPopupClientID+"_Data"];
        if (EmpData != null)
        {
            //reCompID = EmpData["CompID"];
            //reCompName = EmpData["CompName"];
            //reEmpID = EmpData["EmpID"];
            //reEmpNameN = EmpData["EmpNameN"];
            //reDeptID = EmpData["DeptID"];
            //reDeptName = EmpData["DeptName"];
            //reOrganID = EmpData["OrganID"];
            //reOrganName = EmpData["OrganName"];
            //reWorkTypeID = EmpData["WorkTypeID"];
            //reWorkType = EmpData["WorkType"];
            //reFlowOrganID = EmpData["FlowOrganID"];
            //reFlowOrganName = EmpData["FlowOrganName"];
            //reTitleID = EmpData["TitleID"];
            //reTitleName = EmpData["TitleName"];
            //rePositionID = EmpData["PositionID"];
            //rePosition = EmpData["Position"];
            //txtEmpID.Text = EmpData["EmpID"];
            //lblEmpName.Text = EmpData["EmpNameN"];
            Session[ModalPopupClientID+"_Data"] = null;
        }
        //string test = reOrganID;
    }
    #endregion"Page_Load"

    #region"按鈕與輸入欄位觸發事件"

    protected void imgEmpID_Click(object sender, ImageClickEventArgs e)
    {
        ucEmpModalPopup.Reset();
        ucEmpModalPopup.ucPopupWidth =800;
        ucEmpModalPopup.ucPopupHeight = 600;
        ucEmpModalPopup.ucPopupHeader = "";
        ucEmpModalPopup.ucFrameURL = "../Util/ucPageGetEmpID.aspx?_CompID=" + selectCompID + "&_OrgTypeID=" + selectOrgTypeID + "&_DeptID=" + selectDeptID + "&_OrganID=" + selectOrganID + "&_outEmpID=" + selectOutEmpID + "&ModalPopupClientID=" + ModalPopupClientID;
        ucEmpModalPopup.Show();
    }
    protected void txtEmpID_TextChanged(object sender, EventArgs e)
    {
        var isSuccess = false;
        var msg = "";
        var EmpNameDataLists = new List<Util_EmpNameDataBean>();
        var EmpNameData = new Util_EmpNameDataBean();
        EmpNameData.CompID = string.IsNullOrEmpty( selectCompID)? UserInfo.getUserInfo().CompID:selectCompID;
        EmpNameData.EmpID = ViewState["EmpID"].ToString().Trim();
        EmpNameData.outEmpID = selectOutEmpID;
        isSuccess = LoadEmpName(EmpNameData, out EmpNameDataLists, out msg);
        if (isSuccess && EmpNameDataLists != null && EmpNameDataLists.Count > 0)
        {
            reCompID = EmpNameDataLists[0].CompID;
            reCompName = EmpNameDataLists[0].CompName;
            reEmpID = EmpNameDataLists[0].EmpID;
            reEmpNameN = EmpNameDataLists[0].EmpNameN;
            reDeptID = EmpNameDataLists[0].DeptID;
            reDeptName = EmpNameDataLists[0].DeptName;
            reOrganID = EmpNameDataLists[0].OrganID;
            reOrganName = EmpNameDataLists[0].OrganName;
            reWorkTypeID = EmpNameDataLists[0].WorkTypeID;
            reWorkType = EmpNameDataLists[0].WorkType;
            reFlowOrganID = EmpNameDataLists[0].FlowOrganID;
            reFlowOrganName = EmpNameDataLists[0].FlowOrganName;
            reTitleID = EmpNameDataLists[0].TitleID;
            reTitleName = EmpNameDataLists[0].TitleName;
            rePositionID = EmpNameDataLists[0].PositionID;
            rePosition = EmpNameDataLists[0].Position;
        }
        else
        {
            reCompID = "";
            reCompName = "";
            reEmpID = "";
            reEmpNameN = "";
            reDeptID = "";
            reDeptName = "";
            reOrganID = "";
            reOrganName = "";
            reWorkTypeID = "";
            reWorkType = "";
            reFlowOrganID = "";
            reFlowOrganName = "";
            reTitleID = "";
            reTitleName = "";
            rePositionID = "";
            rePosition = "";
        }
    }
    #endregion"按鈕與輸入欄位觸發事件"
}