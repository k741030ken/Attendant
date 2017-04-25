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
    /// btnQuery_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRelease_Click(object sender, EventArgs e)
    {
       
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
    //protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}

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

    #endregion
}