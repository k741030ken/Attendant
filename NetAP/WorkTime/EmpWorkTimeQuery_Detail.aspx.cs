using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.DAO;
using AjaxControlToolkit;
using Newtonsoft.Json;

public partial class EmpWorkTimeQuery_Detail : SecurePage
{
    /// <summary>
    /// _CompID
    /// </summary>
    public string _CompID
    {
        get
        {
            if (ViewState["_CompID"] != null)
            {
                return JsonConvert.DeserializeObject<string>(ViewState["_CompID"].ToString());
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_CompID"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// _EmpID
    /// </summary>
    private string _EmpID //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_EmpID"] != null)
                {
                    return JsonConvert.DeserializeObject<string>(ViewState["_EmpID"].ToString());
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        set
        {
            ViewState["_EmpID"] = JsonConvert.SerializeObject(value);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData(); //載入資料
        }
    }

    /// <summary>
    /// 載入資料
    /// </summary>
    private void LoadData()
    {
        if (Request.Form["hidCompID"] != null)
        {
            _CompID = Request.Form["hidCompID"].ToString();
            _EmpID = Request.Form["hidEmpID"].ToString();

            GetData(_CompID, _EmpID);
        }
    }

    private void GetData(string CompID, string EmpID)
    {
        var isSuccess = false;
        var msg = "";
        var datas = new List<QueryListBean>();
        var viewData = new EmpWorkTimeModel()
        {
            CompID = CompID,
            EmpID = EmpID
        };

        isSuccess = WorkTime.LoadEmpWorkTimeLogGridData(viewData, out datas, out msg);
        if (msg != "")
        {
            Util.MsgBox(msg);
            gvMain.DataSource = null;
            gvMain.DataBind();
            return;
        }
        if (isSuccess && datas != null)
        {
            gvMain.DataSource = datas;
            gvMain.DataBind();
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpWorkTimeQuery.aspx");
    }
}