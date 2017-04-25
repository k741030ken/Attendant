/***************************************
 * 功能說明：公出覆核明細畫面
 * 建立人員：John
 * 建立日期：2017/04/14
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

public partial class OverTime_OnBizReqAppdOperation_Detail : BasePage
{
    #region "1. 全域變數"

    #endregion
    /// <summary>
    /// _SessionCheckVisitGridDataModel
    /// </summary>
    private CheckVisitPKModel _SessionCheckVisitPKModel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (Session["CheckVisitPKModel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<CheckVisitPKModel>(Session["CheckVisitPKModel"].ToString());
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
            Session["CheckVisitPKModel"] = JsonConvert.SerializeObject(value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        subGetData();
    }

    protected void subGetData()
    {
        var isSuccess = false;
        var msg = "";
        var Detaildatas = new OnBizReqAppdOperationBean();
        var ViewData = new OnBizReqAppdOperationModel();
        CheckVisitPKModel QueryData = new CheckVisitPKModel();
        QueryData.CompID = _SessionCheckVisitPKModel.CompID;
        QueryData.EmpID = _SessionCheckVisitPKModel.EmpID;
        QueryData.WriteDate = _SessionCheckVisitPKModel.WriteDate;
        QueryData.FormSeq = _SessionCheckVisitPKModel.FormSeq;

        isSuccess = FN_OnBizReqAppdOperation.GetVisitFormDetailData(QueryData, out Detaildatas, out msg);
        if (isSuccess && Detaildatas != null)
        {
            ViewData = FN_OnBizReqAppdOperation.DetailDataFormat(Detaildatas); //Format Data         

            lblWriterID_Nametxt.Text = ViewData.WriterID_Name;
            lblWriteDatetxt.Text = ViewData.WriteDate;
            lblEmpID_NameNtxt.Text = ViewData.EmpID_NameN;
            lblVisitFormNotxt.Text = ViewData.VisitFormNo;
            lblCompNametxt.Text = ViewData.CompID_Name;
            lblDeptNametxt.Text = ViewData.DeptName;
            lblTitleNametxt.Text = ViewData.TitleName;
            lblPositiontxt.Text = ViewData.Position;
            lblTel_1txt.Text = ViewData.Tel_1;
            lblTel_2txt.Text = ViewData.Tel_2;
            lblVisitDatetxt.Text = ViewData.VisitDate;
            lblVisitTimetxt.Text = ViewData.VisitTime;
            lblDeputyID_Nametxt.Text = ViewData.DeputyID_Name;
            if (ViewData.LocationType.Equals("1")) {
                chkInterLocation.Checked = true;
                lblInterLocationNametxt.Text = ViewData.InterLocationName;
            }
            else if (ViewData.LocationType.Equals("2"))
            {
                chkExterLocation.Checked = true;
                lblExterLocationNametxt.Text = ViewData.ExterLocationName;
            }
            else if (ViewData.LocationType.Equals("3"))
            {
                chkInterLocation.Checked = true;
                lblInterLocationNametxt.Text = ViewData.InterLocationName;
                chkExterLocation.Checked = true;
                lblExterLocationNametxt.Text = ViewData.ExterLocationName;
            }
            lblVisiterNametxt.Text = ViewData.VisiterName;
            lblVisiterTeltxt.Text = ViewData.VisiterTel;
            lblVisitReasontxt.Text = ViewData.VisitReason;
            lblVisitReasonDesctxt.Text = ViewData.VisitReasonDesc;
            lblLastChgComptxt.Text = ViewData.LastChgComp_Name;
            lblLastChgIDtxt.Text = ViewData.LastChgID_Nanme;
            lblLastChgDatetxt.Text = ViewData.LastChgDate;

        }
        
    }

    protected void btnGoBack_Click(object sender, EventArgs e)//返回
    {
        Response.Redirect("OnBizReqAppdOperation.aspx");
    }
}