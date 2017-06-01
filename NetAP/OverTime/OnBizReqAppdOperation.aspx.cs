/***************************************
 * 功能說明：公出覆核
 * 建立人員：John
 * 建立日期：2017/04/12
***************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Diagnostics;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using System.Data;

using RS = SinoPac.WebExpress.Common.Properties;
using System.Data.Common;
using System.Drawing;
using SinoPac.WebExpress.Work;

public partial class OverTime_OnBizReqAppdOperation : BasePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _OnBizReqAppdOperationModel
    /// </summary>
    private OnBizReqAppdOperationModel _OnBizReqAppdOperationModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["OnBizReqAppdOperationModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizReqAppdOperationModel>(ViewState["OnBizReqAppdOperationModel"].ToString());
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
            ViewState["OnBizReqAppdOperationModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _SessionCheckVisitGridDataModel
    /// </summary>
    //private CheckVisitPKModel _SessionCheckVisitPKModel //全域private變數要為('_'+'小駝峰')
    //{
    //    get
    //    {
    //        try
    //        {
    //            if (Session["CheckVisitPKModel"] != null) //ViewState當頁暫存使用
    //            {
    //                return JsonConvert.DeserializeObject<CheckVisitPKModel>(ViewState["CheckVisitPKModel"].ToString());
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            return null;
    //        }
    //    }
    //    set
    //    {
    //        Session["CheckVisitPKModel"] = JsonConvert.SerializeObject(value);
    //    }
    //}

    /// <summary>
    /// _SessionCheckVisitPKModel
    /// </summary>
    private CheckVisitPKModel _SessionCheckVisitPKModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            if (Session["CheckVisitPKModel"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session["CheckVisitPKModel"] = new CheckVisitPKModel();
            }
            return JsonConvert.DeserializeObject<CheckVisitPKModel>(Session["CheckVisitPKModel"].ToString());
        }
        set
        {
            Session["CheckVisitPKModel"] = JsonConvert.SerializeObject(value);
        }
    }

    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;
    /// <summary>
    /// _templateModel
    /// </summary>
    //private TemplateModel _templateModel //全域private變數要為('_'+'小駝峰')
    //{
    //    get
    //    {
    //        if (Session["Template_TemplateModel"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
    //        {
    //            Session["Template_TemplateModel"] = new TemplateModel();
    //        }
    //        return (TemplateModel)Session["Template_TemplateModel"];
    //    }
    //    set
    //    {
    //        Session["Template_TemplateModel"] = value;
    //    }
    //}
    #endregion

    #region "2. 功能鍵處理邏輯"
    /// <summary>
    /// btnRelease_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRelease_Click(object sender, EventArgs e)
    {
        if (CheckData()) {
            DoRelease();
            LoadData();
        }
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
    /// gvMain_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCheckVisitForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView oRow;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            oRow = (DataRowView)e.Row.DataItem;
            e.Row.Cells[9].ToolTip = oRow["VisitReasonCN"] + "";
            e.Row.Cells[9].Text = (oRow["VisitReasonCN"] + "").Substring(0, (oRow["VisitReasonCN"] + "").Length <= 10 ? (oRow["VisitReasonCN"] + "").Length : 10);
        }
    }

    /// <summary>
    /// gvMain_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCheckVisitForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Detail")) {
            GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

            CheckVisitPKModel RowData = new CheckVisitPKModel();

            RowData.CompID = gvCheckVisitForm.DataKeys[clickedRow.RowIndex].Values["CompID"].ToString();
            RowData.EmpID = gvCheckVisitForm.DataKeys[clickedRow.RowIndex].Values["EmpID"].ToString();
            RowData.WriteDate = gvCheckVisitForm.DataKeys[clickedRow.RowIndex].Values["WriteDate"].ToString();
            RowData.FormSeq = gvCheckVisitForm.DataKeys[clickedRow.RowIndex].Values["FormSeq"].ToString();
            

            _SessionCheckVisitPKModel = RowData;

            Response.Redirect("OnBizReqAppdOperation_Detail.aspx");

        }
    }

    /// <summary>
    /// ddlSexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlSexChanged(object sender, EventArgs e)
    //{
    //    LoadData();
    //}
    #endregion

    #region "6. 畫面檢核與確認"
    /// <summary>
    /// 畫面檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool viewValidation(out string msg)
    {
        bool result = true;
        msg = "";
        //List<string> sb = new List<string>();
        //if (!string.IsNullOrEmpty(txtOTCompID.Text))
        //{
        //    if (ValidationUtility.IsAnyOneChineseWord(txtOTCompID.Text) || ValidationUtility.IsAnyOneFullWidthWord(txtOTCompID.Text))
        //    {
        //        sb.Add("公司欄位請勿輸入全形字或中文!!");
        //        result = false;
        //    }
        //}
        //if (!string.IsNullOrEmpty(txtOTEmpID.Text))
        //{
        //    if (!ValidationUtility.IsAllNumber(txtOTEmpID.Text))
        //    {
        //        sb.Add("員編欄位只能輸入數字!!");
        //        result = false;
        //    }
        //}
        //if (sb.Count > 0)
        //{
        //    msg = string.Join("\n", sb);
        //}
        return result;
    }


    private bool CheckData()
    {
        int DataCount = 0;
        for (int introw = 0; introw < gvCheckVisitForm.Rows.Count; introw++)
        {
            CheckBox objchk = (CheckBox)gvCheckVisitForm.Rows[introw].FindControl("chkChoiced");
            RadioButton rdoApp = (RadioButton)gvCheckVisitForm.Rows[introw].FindControl("rbnApproved");
            RadioButton rdoRej = (RadioButton)gvCheckVisitForm.Rows[introw].FindControl("rbnReject");
            TextBox txtReson = (TextBox)gvCheckVisitForm.Rows[introw].FindControl("txtReson");
            if (objchk.Checked == true) {
                DataCount = DataCount + 1;
                if (rdoApp.Checked == true || rdoRej.Checked == true) {
                    if (txtReson.Text.Length > 200) {
                        Util.MsgBox("審核意見大於200字");
                        return false;
                    }
                }
                if (rdoApp.Checked == false && rdoRej.Checked == false)
                {
                    Util.MsgBox("請選擇核准或駁回");
                    return false;
                }
                if (rdoRej.Checked == true)
                {
                    if (txtReson.Text.Length <= 0)
                    {
                        Util.MsgBox("請輸入審核意見");
                        return false;
                    }
                }
            }
        }

        if (DataCount == 0)
        {
            Util.MsgBox("未選取任何資料");
            return false;
        }

        return true;
    }

    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<CheckVisitGridDataBean>();
        var viewData = new OnBizReqAppdOperationModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            ValidID = UserInfo.getUserInfo().UserID
        };

        isSuccess = FN_OnBizReqAppdOperation.GetVisitFormGridViewData(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.CheckVisitGridDataList = FN_OnBizReqAppdOperation.GridDataFormat(datas); //Format Data         
        }
        gvCheckVisitForm.DataSource = viewData.CheckVisitGridDataList;
        gvCheckVisitForm.DataBind();
        _OnBizReqAppdOperationModel = viewData;
    }

    /// <summary>
    /// 審核
    /// </summary>
    private void DoRelease()
    {
        
        FlowExpress oFlow = new FlowExpress("OnBizReqAppd_ITRD");
        string FlowCustDB = oFlow.FlowID;
        Dictionary<string, string> oAssDic = CustVerify.getEmpID_Name_Dictionary(UserInfo.getUserInfo().UserID, UserInfo.getUserInfo().CompID);
        for (int introw = 0; introw < gvCheckVisitForm.Rows.Count; introw++)
        {
            DbHelper db = new DbHelper(_attendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();
            CheckBox objchk = (CheckBox)gvCheckVisitForm.Rows[introw].FindControl("chkChoiced");
            try
            {
                if (objchk.Checked == true)
                {
                    RadioButton rdoApp = (RadioButton)gvCheckVisitForm.Rows[introw].FindControl("rbnApproved");
                    RadioButton rdoRej = (RadioButton)gvCheckVisitForm.Rows[introw].FindControl("rbnReject");
                    TextBox txtReson = (TextBox)gvCheckVisitForm.Rows[introw].FindControl("txtReson");
                    string rowCompID = gvCheckVisitForm.DataKeys[introw].Values["CompID"].ToString();
                    string rowEmpID = gvCheckVisitForm.DataKeys[introw].Values["EmpID"].ToString();
                    string rowWriteDate = gvCheckVisitForm.DataKeys[introw].Values["WriteDate"].ToString();
                    string rowFormSeq = gvCheckVisitForm.DataKeys[introw].Values["FormSeq"].ToString();
                    string rowFlowCaseID = gvCheckVisitForm.DataKeys[introw].Values["FlowCaseID"].ToString();
                    string rowFlowLogID = gvCheckVisitForm.DataKeys[introw].Values["FlowLogID"].ToString();
                    string btnAct = "";
                    string OBFormStatus = "";
                    if (rdoApp.Checked == true)
                    {
                        btnAct = "btnClose";
                        OBFormStatus = "3";
                    }
                    else if (rdoRej.Checked == true)
                    {
                        btnAct = "btnReject";
                        OBFormStatus = "4";
                    }

                    OnBizReqAppdOperationSql.UpdateVisitForm(rowCompID, rowEmpID, rowWriteDate, rowFormSeq, OBFormStatus, txtReson.Text.ToString(), ref sb);
                    FlowUtility.ChangeFlowFlag(rowFlowCaseID, "OB01", "0001", UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID, "0", ref sb);

                    if (FlowExpress.IsFlowVerify(FlowCustDB, rowFlowLogID, btnAct, oAssDic, txtReson.Text.ToString()))
                    {
                        db.ExecuteNonQuery(sb.BuildCommand(), tx);
                        tx.Commit();
                        Util.MsgBox("審核成功!");
                    }
                    else
                    {
                        throw new Exception("審核失敗!");
                    }

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                Util.MsgBox(ex.Message);
            }
        }
    }
    #endregion
}