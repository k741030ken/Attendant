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

public partial class CheckIn_PO1000_ErrorView : BasePage
{
    #region "1. 全域變數"

    /// <summary>
    /// _Paramodel
    /// </summary>
    private PunchModel _PunchModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_PunchModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<PunchModel>(ViewState["_PunchModel"].ToString());
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
            ViewState["_PunchModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// SessionID(跨頁找正確的Session用)
    /// </summary>
    public string _SessionID
    {
        get
        {
            if (ViewState["_SessionID"] == null)
            {
                ViewState["_SessionID"] = string.Empty;
            }
            return (string)ViewState["_SessionID"];
        }
        set
        {
            ViewState["_SessionID"] = value;
        }
    }

    /// <summary>
    /// _SessionPunchModel
    /// </summary>
    private PunchModel _SessionPunchModel
    {
        //get
        //{
        //    if (Session["_SessionPunchModel"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
        //    {
        //        Session["_SessionPunchModel"] = new PunchModel();
        //    }
        //    return JsonConvert.DeserializeObject<PunchModel>(Session["_SessionPunchModel"].ToString());
        //}
        //set
        //{
        //    Session["_SessionPunchModel"] = JsonConvert.SerializeObject(value);
        //}
        get
        {
            if (Session[_SessionID] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session[_SessionID] = new PunchModel();
            }
            return JsonConvert.DeserializeObject<PunchModel>(Session[_SessionID].ToString());
        }
        set
        {
            Session[_SessionID] = JsonConvert.SerializeObject(value);
        }
    }

    #endregion

    #region "2. 功能鍵處理邏輯"

    /// <summary>
    /// btnDefine_Click
    /// 確定
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDefine_Click(object sender, EventArgs e)
    {
        beforeSubmit();
    }

    /// <summary>
    /// btnCancel_Click
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
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
            //抓取Session ID
            string sessionId = Request["id"] != null ? Request["id"].ToString() : string.Empty;

            if (!string.IsNullOrEmpty(sessionId))
            {
                _SessionID = sessionId;

                //載入資料
                LoadData();
            }
            else
            {
                throw new Exception("系統錯誤,請重新打卡!");
            }
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
  
    }

    /// <summary>
    /// 公務
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtAbnormal_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rbt = sender as RadioButton;
        AbnormalFlagSwitch(rbt.ID);
    }

    /// <summary>
    /// 非公務
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtunAbnormal_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rbt = sender as RadioButton;
        AbnormalFlagSwitch(rbt.ID);
    }

    #endregion

    #region "6. 畫面檢核與確認"
    /// <summary>
    /// 異常類型檢核
    /// </summary>
    /// <returns>請選擇異常原因</returns>
    private string AbnormalReasonValidation()
    {
        string result = "";
        if (ddlAbnormalReason.SelectedValue == "" && rbtunAbnormal.Checked)
        {
            result = "請選擇異常原因!";
        }
        return result;
    }

    /// <summary>
    /// 其他說明檢核
    /// </summary>
    /// <returns>請填寫其他說明</returns>
    private string AbnormalDescValidation()
    {
        string result = "";
        if ("".Equals(AbnormalDesc.Text))
        {
            result = "請填寫其他說明!";
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
        _PunchModel = _SessionPunchModel;
        initAbnormalReason();
        initScreen();
    }

    /// <summary>
    /// 打卡前檢核
    /// </summary>
    private void beforeSubmit()
    {
        string errorMsg = "";
        if (!"".Equals(AbnormalReasonValidation()))
        {
            errorMsg = AbnormalReasonValidation();
        }
        else if (!"".Equals(AbnormalDescValidation()))
        {
            errorMsg = AbnormalDescValidation();
        }

        if (!"".Equals(errorMsg))
        {
            Util.MsgBox(errorMsg);
        }
        else
        {
            DoDefine();
        }
    }

    //-------------------------------------------------------------主要邏輯區

    /// <summary>
    /// 確定打卡邏輯
    /// </summary>
    private void DoDefine()
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            PunchModel model = _PunchModel;

            string abnormalFlag = rbtAbnormal.Checked ? "1" : "2";
            model.AbnormalFlag = StringIIF(abnormalFlag);
            model.AbnormalReasonID = StringIIF(ddlAbnormalReason.SelectedValue);
            model.AbnormalReasonCN = StringIIF("".Equals(ddlAbnormalReason.SelectedValue)?"":ddlAbnormalReason.SelectedItem.Text);
            model.AbnormalDesc = StringIIF(AbnormalDesc.Text);

            result = Punch.InsertPunchLog(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被新增!!");
            }
            _SessionPunchModel = model;
            Response.Redirect("~/Punch/PO1000_Finish.aspx" + "?id=" + _SessionID);
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    //-------------------------------------------------------------方法

    private void AbnormalFlagSwitch(string filedName) 
    {
        switch (filedName)
        {
            case "rbtAbnormal": 
                {
                    rbtAbnormal.Checked = true;
                    rbtunAbnormal.Checked = false;
                    ddlAbnormalReason.SelectedIndex = 0;
                    ddlAbnormalReason.Enabled = false;
                    break; 
                }
            case "rbtunAbnormal": 
                {
                    rbtAbnormal.Checked = false;
                    rbtunAbnormal.Checked = true;
                    ddlAbnormalReason.Enabled = true;
                    break; 
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

    //-------------------------------------------------------------初始物件

    /// <summary>
    /// 畫面初始值
    /// </summary>
    private void initScreen()
    {
        PunchModel model = _PunchModel;
        try
        {
            AbnormalFlagSwitch("rbtAbnormal");
            EmpID_NameN.Text = model.EmpID + " " + model.EmpName;
            PunchDateTime.Text = model.PunchDate + " " + model.PunchTime.Substring(0, model.PunchTime.Length - 7);
            ShowMsg.Text = model.RemindMsg;

        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    /// <summary>
    /// 非公務原因下拉選單
    /// </summary>
    private void initAbnormalReason()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<DropDownListModel>();

        isSuccess = Punch.SelectAT_CodeMap(out datas, out msg);
        if (isSuccess && datas != null && datas.Count > 0)
        {
            ddlAbnormalReason.DataSource = datas;
            ddlAbnormalReason.DataTextField = "DataText";
            ddlAbnormalReason.DataValueField = "DataValue";
            ddlAbnormalReason.DataBind();
            ddlAbnormalReason.Items.Insert(0, new ListItem("----請選擇----", ""));
        }
    }

    #endregion



}


