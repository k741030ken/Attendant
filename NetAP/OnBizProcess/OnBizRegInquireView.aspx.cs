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
using Microsoft.VisualBasic;

public partial class OnBiz_OnBizRegInquireView : BasePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _templateModel
    /// </summary>
    private OnBizPublicOutModel _OnBizRegInquireModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_OnBizRegInquireModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizPublicOutModel>(ViewState["_OnBizRegInquireModel"].ToString());
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
            ViewState["_OnBizRegInquireModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _ddlPersonModel
    /// </summary>
    private OnBizPublicOutModel _ddlPersonModel
    {
        get
        {
            try
            {
                if (ViewState["PersonModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizPublicOutModel>(ViewState["PersonModel"].ToString());
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
            ViewState["PersonModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _SessionModifyModel
    /// </summary>
    private OnBizPublicOutModel _SessionModifyModel
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
    /// 新增
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("OnBizReqAddesView.aspx");
        
    }
    /// <summary>
    /// btnUpdate_Click
    /// 修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        beforeSubmit("1","Update");
        
    }
    /// <summary>
    /// btnDelete_Click
    /// 刪除
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        beforeSubmit("1","Delete");
    }
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
    /// btnCancel_Click
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        beforeSubmit("2","Cancel");

    }
    /// <summary>
    /// btnLogoff_Click
    /// 註銷
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogoff_Click(object sender, EventArgs e)
    {
        beforeSubmit("3","Logoff");
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
    /// 點選明細跳轉至明細頁
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
            viewData.PageName = "OnBizRegInquireView";
            _SessionModifyModel = viewData;

            Response.Redirect("OnBizReqDetailView.aspx");
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
        bool result  = true;
        bool startFlag = true;
        errorMsg = "";

        var after5YDate = StringIIF(DateTime.Now.AddYears(5).ToString("yyyy/MM/dd"));
        var before5YDate = StringIIF(DateTime.Now.AddYears(-5).ToString("yyyy/MM/dd"));
        var startDate = StringIIF(ucStartDate.ucSelectedDate);
        var endDate = StringIIF(ucEndDate.ucSelectedDate);

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
                //int startYear = Int32.Parse(Convert.ToDateTime(startDate).ToString("yyyy"));
                //int startMonth = Int32.Parse(Convert.ToDateTime(startDate).ToString("MM"));
                //int dayInMonth = DateTime.DaysInMonth(startYear, startMonth);
                string dayMonth = Convert.ToDateTime(startDate).AddMonths(1).ToString("yyyy/MM/dd");
                int datMonthDiff = OnBizReq.GetTimeDiff(startDate, dayMonth, "Day");
                int dayDiff = OnBizReq.GetTimeDiff(startDate, endDate, "Day");
                int m1 = OnBizReq.GetTimeDiff("18:30", "23:55", "Minute");
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

    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        _SessionModifyModel = null;
        lblEmpID.Text = UserInfo.getUserInfo().UserID + " " + UserInfo.getUserInfo().UserName;

    }

    /// <summary>
    /// 執行修改、刪除、取消、註銷前檢核
    /// </summary>
    /// <param name="regStr">識別字</param>
    /// <returns></returns>
    private void beforeSubmit(string regNO,string regStr)
    {
        string errorMsg = "";
        bool flag = true;
        bool checkBoxFlag = true;
        try
        {
            if (gvMain.Rows.Count == 0)
            {
                errorMsg = "請先查詢資料";
            }
            else
            {
                foreach (GridViewRow dataRow in gvMain.Rows)
                {
                    if (dataRow.RowType == DataControlRowType.DataRow) 
                    {
                        CheckBox chk_Update = (CheckBox)dataRow.Cells[0].FindControl("chkChoose");
                        if (chk_Update.Checked)
                        {
                            checkBoxFlag = true;
                            string formStatus = gvMain.DataKeys[dataRow.RowIndex].Values["OBFormStatus"].ToString();
                            if (!regNO.Equals(formStatus))
                            {
                                flag = false;
                                break;
                            }
                            else
                            {
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            checkBoxFlag = false;
                            flag = false;
                        }
                    }
                }
            }

            if (!flag)
            {
                if (!checkBoxFlag)
                {
                    errorMsg = "請選擇資料";
                }
                else
                {
                    if ("1".Equals(regNO))
                    {
                        errorMsg = "暫存中的資料才可以做修改、刪除";
                    }
                    else if ("2".Equals(regNO))
                    {
                        errorMsg = "送簽中的資料才可以做取消";
                    }
                    else if ("3".Equals(regNO))
                    {
                        errorMsg = "核准過的資料才可以做註銷";
                    }
                }
            }

            if (!errorMsg.Equals(""))
            {
                //Util.MsgBox(errorMsg);
                throw new Exception(errorMsg);
            }

            if (errorMsg.Equals("") && flag)
            {
                switch (regStr)
                {
                    case "Update": { DoUpdate(); break; }
                    case "Delete": { DoDelete(); break; }
                    case "Cancel": { DoCancel(); break; }
                    case "Logoff": { DoLogoff(); break; }
                }
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 查詢前的畫面檢核
    /// 公出人員、公出日期
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
            DoQuery();
        }
        catch(Exception ex)
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
        var datas = new List<OnBizPublicOutBean>();
        var viewData = new OnBizPublicOutModel()
        {
            OBUseType = StringIIF(ddlOBUseType.SelectedValue),
            CompID = StringIIF(UserInfo.getUserInfo().CompID),
            EmpID = StringIIF(UserInfo.getUserInfo().UserID),
            OBVisitBeginDate = StringIIF(ucStartDate.ucSelectedDate),
            OBVisitEndDate = StringIIF(ucEndDate.ucSelectedDate),
            OBFormStatus = StringIIF(ddlOBFormStatus.SelectedValue)
        };

        isSuccess = OnBizRegInquire.SelectVisitForm(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = OnBizReq.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
    }

    /// <summary>
    /// 刪除邏輯
    /// </summary>
    private void DoDelete()
    {
        var isSuccess = false;
        string msg = "";
        long seccessCount = 0;
        var datas = new List<OnBizPublicOutModel>();

        foreach (GridViewRow dataRow in gvMain.Rows)
        {
            if (dataRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_Update = (CheckBox)dataRow.Cells[0].FindControl("chkChoose");
                if (chk_Update.Checked)
                {
                    var viewData = new OnBizPublicOutModel();
                    viewData.CompID = gvMain.DataKeys[dataRow.RowIndex].Values["CompID"].ToString();
                    viewData.EmpID = gvMain.DataKeys[dataRow.RowIndex].Values["EmpID"].ToString();
                    viewData.OBWriteDate = gvMain.DataKeys[dataRow.RowIndex].Values["OBWriteDate"].ToString();
                    viewData.OBFormSeq = gvMain.DataKeys[dataRow.RowIndex].Values["OBFormSeq"].ToString();
                    viewData.OBLastChgComp = UserInfo.getUserInfo().CompID;
                    viewData.OBLastChgID = UserInfo.getUserInfo().UserID;
                    viewData.OBLastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    datas.Add(viewData);
                }
            }
        }
        isSuccess = OnBizReq.DeleteVisitForm(datas, out seccessCount, out msg);
        if (!isSuccess)
        {
            throw new Exception(msg);
        }
        if (seccessCount == 0)
        {
            throw new Exception("無資料被刪除!!");
        }
        Util.MsgBox("刪除成功");
        DoQuery();
    }

    /// <summary>
    /// 取消邏輯
    /// </summary>
    private void DoCancel()
    {
        var isSuccess = false;
        string msg = "";
        long seccessCount = 0;
        var datas = new List<OnBizPublicOutModel>();

        foreach (GridViewRow dataRow in gvMain.Rows)
        {
            if (dataRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_Update = (CheckBox)dataRow.Cells[0].FindControl("chkChoose");
                if (chk_Update.Checked)
                {
                    var viewData = new OnBizPublicOutModel();
                    viewData.CompID = gvMain.DataKeys[dataRow.RowIndex].Values["CompID"].ToString();
                    viewData.EmpID = gvMain.DataKeys[dataRow.RowIndex].Values["EmpID"].ToString();
                    viewData.OBWriteDate = gvMain.DataKeys[dataRow.RowIndex].Values["OBWriteDate"].ToString();
                    viewData.OBFormSeq = gvMain.DataKeys[dataRow.RowIndex].Values["OBFormSeq"].ToString();
                    viewData.FlowCaseID = gvMain.DataKeys[dataRow.RowIndex].Values["FlowCaseID"].ToString();
                    viewData.ValidID = gvMain.DataKeys[dataRow.RowIndex].Values["ValidID"].ToString();
                    viewData.OBLastChgComp = UserInfo.getUserInfo().CompID;
                    viewData.OBLastChgID = UserInfo.getUserInfo().UserID;
                    viewData.OBLastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    datas.Add(viewData);
                }
            }
        }
        isSuccess = OnBizReq.CancelVisitForm(datas, out seccessCount, out msg);//取消流程拔掉
        if (!isSuccess)
        {
            throw new Exception(msg);
        }
        if (seccessCount == 0)
        {
            throw new Exception("無資料被取消!!");
        }
        Util.MsgBox("取消成功");
        DoQuery();
    }

    /// <summary>
    /// 註銷邏輯
    /// </summary>
    private void DoLogoff()
    {
        var isSuccess = false;
        string msg = "";
        long seccessCount = 0;
        var datas = new List<OnBizPublicOutModel>();

        foreach (GridViewRow dataRow in gvMain.Rows)
        {
            if (dataRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_Update = (CheckBox)dataRow.Cells[0].FindControl("chkChoose");
                if (chk_Update.Checked)
                {
                    var viewData = new OnBizPublicOutModel();
                    viewData.CompID = gvMain.DataKeys[dataRow.RowIndex].Values["CompID"].ToString();
                    viewData.EmpID = gvMain.DataKeys[dataRow.RowIndex].Values["EmpID"].ToString();
                    viewData.OBWriterID = UserInfo.getUserInfo().UserID;
                    viewData.OBWriterName = UserInfo.getUserInfo().UserName;
                    viewData.OBDeptID = UserInfo.getUserInfo().DeptID;
                    viewData.OBDeptName = UserInfo.getUserInfo().DeptName;
                    viewData.OBWriteDate = gvMain.DataKeys[dataRow.RowIndex].Values["OBWriteDate"].ToString();
                    viewData.OBFormSeq = gvMain.DataKeys[dataRow.RowIndex].Values["OBFormSeq"].ToString();
                    viewData.FlowCaseID = gvMain.DataKeys[dataRow.RowIndex].Values["FlowCaseID"].ToString();
                    viewData.OBLastChgComp = UserInfo.getUserInfo().CompID;
                    viewData.OBLastChgID = UserInfo.getUserInfo().UserID;
                    viewData.OBLastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    datas.Add(viewData);
                }
            }
        }
        isSuccess = OnBizReq.LogoffVisitForm(datas, out seccessCount, out msg);//註銷邏輯拔掉
        if (!isSuccess)
        {
            throw new Exception(msg);
        }
        if (seccessCount == 0)
        {
            throw new Exception("無資料被註銷!!");
        }
        Util.MsgBox("註銷成功");
        DoQuery();
    }

    /// <summary>
    /// 修改邏輯
    /// 選取資料跳轉至修改頁
    /// </summary>
    private void DoUpdate()
    {
        try
        {
            selectedGridData();
            Response.Redirect("OnBizReqModifyView.aspx");
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 清除邏輯
    /// </summary>
    private void DoClear()
    {
        ddlOBUseType.SelectedIndex = 0;
        ucStartDate.ucSelectedDate = "";
        ucEndDate.ucSelectedDate = "";
        ddlOBFormStatus.SelectedIndex = 0;
    }

    //-------------------------------------------------------------方法

    /// <summary>
    /// 修改邏輯
    /// 勾選一筆資料存入Session
    /// </summary>
    private void selectedGridData()
    {
        foreach (GridViewRow dataRow in gvMain.Rows)
        {
            if (dataRow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_Update = (CheckBox)dataRow.Cells[0].FindControl("chkChoose");
                if (chk_Update.Checked)
                {
                    var viewData = new OnBizPublicOutModel();
                    viewData.CompID = gvMain.DataKeys[dataRow.RowIndex].Values["CompID"].ToString();
                    viewData.EmpID = gvMain.DataKeys[dataRow.RowIndex].Values["EmpID"].ToString();
                    viewData.OBWriteDate = gvMain.DataKeys[dataRow.RowIndex].Values["OBWriteDate"].ToString();
                    viewData.OBFormSeq = gvMain.DataKeys[dataRow.RowIndex].Values["OBFormSeq"].ToString();
                    _SessionModifyModel = viewData;
                }
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

    #endregion

}


