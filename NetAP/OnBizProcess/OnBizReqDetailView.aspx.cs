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

public partial class OnBiz_OnBizReqDetailView : BasePage
{
    #region "1. 全域變數"

    /// <summary>
    /// _OnBizRegInquireModel
    /// </summary>
    private OnBizPublicOutModel _OnBizReqDetailModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["OnBizReqDetailModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<OnBizPublicOutModel>(ViewState["OnBizReqDetailModel"].ToString());
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
            ViewState["OnBizReqDetailModel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _BackPage
    /// </summary>
    private string _BackPage //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["BackPage"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<string>(ViewState["BackPage"].ToString());
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
            ViewState["BackPage"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _SessionDetailModel
    /// </summary>
    private OnBizPublicOutModel _SessionDetailModel
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
    /// btnBack_Click
    /// 返回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(_BackPage + ".aspx");
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

    #endregion

    #region "6. 畫面檢核與確認"
    

    #endregion

    #region "7. private Method"
    /// <summary>
    /// 載入資料(原件初始化)
    /// </summary>
    private void LoadData()
    {
        _OnBizReqDetailModel = _SessionDetailModel;
        _BackPage = _OnBizReqDetailModel.PageName;
        _SessionDetailModel = null;

        Inner.Enabled = false;
        Outter.Enabled = false;
        QueryOBEmpInfo();
        initScreen();
    }

    //-------------------------------------------------------------主要邏輯區

    /// <summary>
    /// 查詢個人公出單資料
    /// </summary>
    private void QueryOBEmpInfo()
    {
        var isSuccess = false;
        var msg = "";
        var datas = new OnBizPublicOutBean();
        var viewData = new OnBizPublicOutModel()
        {
            CompID = StringIIF(_OnBizReqDetailModel.CompID),
            EmpID = StringIIF(_OnBizReqDetailModel.EmpID),
            OBWriteDate = StringIIF(_OnBizReqDetailModel.OBWriteDate),
            OBFormSeq = StringIIF(_OnBizReqDetailModel.OBFormSeq)
        };

        isSuccess = OnBizReq.SelectOnlyVisitForm(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            viewData = OnBizReq.DetailFormat(datas); //Format Data
            _OnBizReqDetailModel = viewData;
        }
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

    //-------------------------------------------------------------初始物件

    /// <summary>
    /// 畫面初始狀態
    /// </summary>
    private void initScreen()
    {
        WriterID_Name.Text = _OnBizReqDetailModel.OBWriterID_Name;
        WriteDate.Text = _OnBizReqDetailModel.OBWriteDate;
        EmpID_NameN.Text = _OnBizReqDetailModel.EmpID +" "+_OnBizReqDetailModel.EmpNameN;
        VisitFormNo.Text = _OnBizReqDetailModel.OBVisitFormNo;
        CompName.Text = _OnBizReqDetailModel.CompID_Name;
        OrganName.Text = _OnBizReqDetailModel.OBOrganName;
        TitleName.Text = _OnBizReqDetailModel.OBTitleName;
        Position.Text = _OnBizReqDetailModel.OBPosition;
        Tel_1.Text = _OnBizReqDetailModel.OBTel_1;
        Tel_2.Text = _OnBizReqDetailModel.OBTel_2;
        VisitBeginDate.Text = _OnBizReqDetailModel.OBVisitBeginDate;
        BeginTimeA.Text = _OnBizReqDetailModel.OBBeginTimeH;
        BeginTimeB.Text = _OnBizReqDetailModel.OBBeginTimeM;
        EndTimeA.Text = _OnBizReqDetailModel.OBEndTimeH;
        EndTimeB.Text = _OnBizReqDetailModel.OBEndTimeM;
        DeputyID_NameN.Text = _OnBizReqDetailModel.OBDeputyID +" "+_OnBizReqDetailModel.OBDeputyName;
        chkBoxControl(_OnBizReqDetailModel.OBLocationType);
        InterLocationName.Text = _OnBizReqDetailModel.OBInterLocationName;
        ExterLocationName.Text = _OnBizReqDetailModel.OBExterLocationName;
        VisiterName.Text = _OnBizReqDetailModel.OBVisiterName;
        VisiterTel.Text = _OnBizReqDetailModel.OBVisiterTel;
        VisitReasonCN.Text = _OnBizReqDetailModel.OBVisitReasonCN;
        VisitReasonDesc.Text = _OnBizReqDetailModel.OBVisitReasonDesc;
        LastChgComp_Name.Text = _OnBizReqDetailModel.OBLastChgCompID_Name;
        LastChgID_NameN.Text = _OnBizReqDetailModel.OBLastChgID_Name;
        LastChgDate.Text = _OnBizReqDetailModel.OBLastChgDate;
    }

    /// <summary>
    /// 畫面CheckBox初始狀態鎖定
    /// </summary>
    /// <param name="type"></param>
    private void chkBoxControl(string type)
    {
        switch (type)
        {
            case "1":
                {
                    Inner.Checked = true;
                    break;
                }
            case "2":
                {
                    Outter.Checked = true;
                    break;
                }
            case "3":
                {
                    Inner.Checked = true; 
                    Outter.Checked = true; 
                    break;
                }
        }
    }

    #endregion
    
}


