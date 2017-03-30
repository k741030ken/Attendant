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

public partial class Template_TemplateView : SecurePage
{
    #region "1. 全域變數"
    /// <summary>
    /// _templateModel
    /// </summary>
    private TemplateModel _templateModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["Template_TemplateModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<TemplateModel>(ViewState["Template_TemplateModel"].ToString());
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
            ViewState["Template_TemplateModel"] = JsonConvert.SerializeObject(value);
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
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DoQuery();
    }

    /// <summary>
    /// btnAdd_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DoAdd();
    }

    /// <summary>
    /// btnEdit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DoUpdate();
    }

    /// <summary>
    /// btnDel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDel_Click(object sender, EventArgs e)
    {
        DoDelete();
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
        
    }

    /// <summary>
    /// ddlSexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
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
        List<string> sb = new List<string>();
        if (!string.IsNullOrEmpty(txtOTCompID.Text))
        {
            if (ValidationUtility.IsAnyOneChineseWord(txtOTCompID.Text) || ValidationUtility.IsAnyOneFullWidthWord(txtOTCompID.Text))
            {
                sb.Add("公司欄位請勿輸入全形字或中文!!");
                result = false;
            }
        }
        if (!string.IsNullOrEmpty(txtOTEmpID.Text))
        {
            if (!ValidationUtility.IsAllNumber(txtOTEmpID.Text))
            {
                sb.Add("員編欄位只能輸入數字!!");
                result = false;
            }
        }
        if (sb.Count > 0)
        {
            msg = string.Join("\n", sb);
        }
        return result;
    }

    /// <summary>
    /// 邏輯檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    private bool logicValidation(out string msg)
    {
        bool result = true;
        msg = "";
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
        var datas = new List<TemplateBean>();
        var viewData = new TemplateModel()
        {
            OTCompID = txtOTCompID.Text,
            OTEmpID = txtOTEmpID.Text,
            NameN = txtNameN.Text,
            Sex = ddlSex.SelectedValue
        };

        isSuccess = Template.GetEmpFlowSNEmpAndSexUseDapper(viewData, out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            viewData.TemplateGridDataList = Template.DataFormat(datas); //Format Data         
        }
        gvMain.DataSource = viewData.TemplateGridDataList;
        gvMain.DataBind();
        _templateModel = viewData;
    }

    /// <summary>
    /// 新增邏輯
    /// </summary>
    private void DoAdd()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            TemplateModel model = _templateModel;
            result = Template.InsertEmpFlowSNEmp(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被新增!!");
            }
            Util.MsgBox("新增筆數 : " + seccessCount);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 修改邏輯
    /// </summary>
    private void DoUpdate()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            TemplateModel model = _templateModel;
            result = Template.UpdateEmpFlowSNEmp(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被修改!!");
            }
            Util.MsgBox("修改筆數 : " + seccessCount);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 查詢邏輯
    /// </summary>
    private void DoQuery()
    {
        string msg = "";
        try
        {
            if (!viewValidation(out msg)) //畫面檢核
            {
                throw new Exception(msg);
            }
            if (!logicValidation(out msg)) //邏輯檢核
            {
                throw new Exception(msg);
            }
            LoadData();
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 刪除邏輯
    /// </summary>
    private void DoDelete()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            TemplateModel model = _templateModel;
            result = Template.DeleteEmpFlowSNEmp(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被刪除!!");
            }
            Util.MsgBox("刪除筆數 : " + seccessCount);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }
    #endregion
}