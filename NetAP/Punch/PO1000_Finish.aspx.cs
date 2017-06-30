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

public partial class CheckIn_PO1000_FinishView : BasePage
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

    #endregion

    #region "6. 畫面檢核與確認"
    /// <summary>
    /// 員工編號檢核
    /// </summary>
    /// <param name="msg">檢核失敗訊息</param>
    /// <returns>bool</returns>
    //private string EmpIDValidation()
    //{
    //    string result = "";
    //    bool flag = true;

    //    var compID = StringIIF(UserInfo.getUserInfo().CompID);
    //    var empID = StringIIF(txtEmpID.Text);

    //    if (empID == "") 
    //    {
    //        result = "請輸入員工編號!";
    //    }
    //    else if (empID != "")
    //    {
            
    //        flag = OnBizReq.QueryEmpExist(compID,empID);
    //        if(!flag)
    //        {
    //            result = "該員工編號人事尚未建檔";
    //        }
    //    }
    //    return result;
    //}

    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        _PunchModel = _SessionPunchModel;
        //Session.Clear();
        initScreen();
    }

    /// <summary>
    /// 取得字串(去除null)
    /// </summary>
    private string StringIIF(object str) 
    {
        string result = "";
        if(str != null)
        {
            if(!string.IsNullOrEmpty(str.ToString().Trim()))
            {
                result = str.ToString();
            }
        }

        return result;
    }

    /// <summary>
    /// 將Json字串轉換為Model
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private PunchModel reverToObject(string json)
    {
        PunchModel result = new PunchModel();
        if (json.Equals(""))
        {
            throw new Exception("");
        }
        result = JsonConvert.DeserializeObject<PunchModel>(json);
        return result;
    }

    private void initScreen()
    {
        PunchModel model = _PunchModel;

        try
        {
            EmpID_NameN.Text = model.EmpID + " " + model.EmpName;
            PunchDateTime.Text = model.PunchDate + " " + model.PunchTime.Substring(0, model.PunchTime.Length - 7);
            ResultMsg.Text = model.ResultMsg;
            if ("0".Equals(model.PunchFlag))
            {
                if (!"2".Equals(model.AbnormalFlag))
                {
                    RemindMsg.Text = model.RemindMsgAf;
                }
                CareMsg.Text = model.CareMsg;
            }
        }
        catch(Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    #endregion
    
}


