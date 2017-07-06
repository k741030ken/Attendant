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

public partial class Punch_PunchLogInqForPersonView : BasePage
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
        beforeQuerySubmit();
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
        lblComp.Text = UserInfo.getUserInfo().CompID + " " + UserInfo.getUserInfo().CompName;
        lblOrgan.Text = UserInfo.getUserInfo().OrganName;
        lblEmp.Text = UserInfo.getUserInfo().UserID + " " + UserInfo.getUserInfo().UserName;
        lblTitle.Text = UserInfo.getUserInfo().UserTitle;
        lblPosition.Text = ""; //還須討論Position
    }

    /// <summary>
    /// 查詢前的畫面檢核
    /// 公出人員、公出日期
    /// </summary>
    private void beforeQuerySubmit()
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
        var searchType = StringIIF(ddlSearchType.SelectedValue);
        var datas = new List<PunchConfirmBean>();
        var viewData = new PunchConfirmModel()
        {
            CompID = StringIIF(UserInfo.getUserInfo().CompID),
            EmpID = StringIIF(UserInfo.getUserInfo().UserID),
            PunchSDate = StringIIF(ucStartDate.ucSelectedDate),
            PunchEDate = StringIIF(ucEndDate.ucSelectedDate),
            ConfirmPunchFlag = StringIIF(ddlConfirmPunchFlag.SelectedValue),
            ConfirmStatus = StringIIF(ddlConfirmStatus.SelectedValue),
            Remedy_AbnormalFlag = StringIIF(ddlRemedy_AbnormalFlag.SelectedValue)
        };

        isSuccess = PunchLogInqForPerson.SelectPunchConfirm(viewData, searchType, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.SelectGridDataList = PunchLogInqForPerson.GridDataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.SelectGridDataList;
        gvMain.DataBind();
    }

    /// <summary>
    /// 清除邏輯
    /// </summary>
    private void DoClear()
    {
        ucStartDate.ucSelectedDate = "";
        ucEndDate.ucSelectedDate = "";
        ddlSearchType.SelectedIndex = 0;
        ddlConfirmPunchFlag.SelectedIndex = 0;
        ddlConfirmStatus.SelectedIndex = 0;
        ddlRemedy_AbnormalFlag.SelectedIndex = 0;
    }

    //-------------------------------------------------------------方法

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


