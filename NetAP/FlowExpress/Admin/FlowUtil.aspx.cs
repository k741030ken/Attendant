using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

public partial class FlowExpress_Admin_FlowUtil : SecurePage
{
    private Dictionary<string, string> _Dic_FlowIDList
    {
        get
        {
            if (ViewState["_Dic_FlowIDList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_FlowIDList"];
            }
            else
            {
                DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
                ViewState["_Dic_FlowIDList"] = Util.getDictionary(db.ExecuteDataSet("Select FlowID, FlowName From FlowSpec").Tables[0]);
                return (Dictionary<string, string>)ViewState["_Dic_FlowIDList"];
            }
        }
    }

    private Dictionary<string, string> _Dic_FlowAttDBList
    {
        get
        {
            if (ViewState["_Dic_FlowAttDBList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_FlowAttDBList"];
            }
            else
            {
                DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
                ViewState["_Dic_FlowAttDBList"] = Util.getDictionary(db.ExecuteDataSet("Select FlowID + '．' + FlowAttachDB , FlowName From FlowSpec").Tables[0]);
                return (Dictionary<string, string>)ViewState["_Dic_FlowAttDBList"];
            }
        }
    }

    private Dictionary<string, string> _Dic_FlowCloseStepList
    {
        get
        {
            if (ViewState["_Dic_FlowCloseStepList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_FlowCloseStepList"];
            }
            else
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                for (int i = 0; i < FlowExpress._FlowNativeCloseStepIDList.Length; i++)
                {
                    oDic.Add(FlowExpress._FlowNativeCloseStepIDList[i], FlowExpress._FlowNativeCloseStepNameList[i]);
                }
                ViewState["_Dic_FlowCloseStepList"] = oDic;
                return (Dictionary<string, string>)ViewState["_Dic_FlowCloseStepList"];
            }
        }
    }


    private Dictionary<string, string> _Dic_SysStepBtnList
    {
        get
        {
            if (ViewState["_Dic_SysStepBtnList"] != null)
            {
                return (Dictionary<string, string>)ViewState["_Dic_SysStepBtnList"];
            }
            else
            {
                Dictionary<string, string> oDic = new Dictionary<string, string>();
                for (int i = 0; i < FlowExpress._FlowNativeSysStepBtnIDList.Length; i++)
                {
                    oDic.Add(FlowExpress._FlowNativeSysStepBtnIDList[i], FlowExpress._FlowNativeSysStepBtnCaptionList[i]);
                }
                ViewState["_Dic_SysStepBtnList"] = oDic;
                return (Dictionary<string, string>)ViewState["_Dic_SysStepBtnList"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OrgExpressTest_MethodList.ucSourceDictionary = Util.getClassMethodList("SinoPac.WebExpress.Work.OrgExpress");
            OrgExpressTest_MethodList.Refresh();

            getFlowCaseValues_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            getFlowCaseValues_FlowID.Refresh();

            GetFlowLogValues_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            GetFlowLogValues_FlowID.Refresh();

            FlowRollBack_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FlowRollBack_FlowID.Refresh();

            FlowCaseValueChanged_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FlowCaseValueChanged_FlowID.Refresh();

            FlowDeleted_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FlowDeleted_FlowID.Refresh();

            DelFlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            DelFlowID.Refresh();

            FixFlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FixFlowID.Refresh();

            TraceFlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            TraceFlowID.Refresh();

            FullLogFlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FullLogFlowID.Refresh();

            AdjFlowAttachDB.ucSourceDictionary = Util.getDictionary(_Dic_FlowAttDBList);
            AdjFlowAttachDB.Refresh();

            getFlowCaseValues_Field.DataSource = Util.getDictionary(typeof(FlowExpress.FlowCaseFieldList), false);
            getFlowCaseValues_Field.DataValueField = "key";
            getFlowCaseValues_Field.DataTextField = "value";
            getFlowCaseValues_Field.DataBind();

            GetFlowLogValues_Field.DataSource = Util.getDictionary(typeof(FlowExpress.FlowLogFieldList), false);
            GetFlowLogValues_Field.DataValueField = "key";
            GetFlowLogValues_Field.DataTextField = "value";
            GetFlowLogValues_Field.DataBind();

            GetFlowLogValues_Scope.DataSource = Util.getDictionary(typeof(FlowExpress.FlowLogDataScope), false, true);
            GetFlowLogValues_Scope.DataValueField = "key";
            GetFlowLogValues_Scope.DataTextField = "value";
            GetFlowLogValues_Scope.DataBind();

            FlowCaseValueChanged_ChangeKind.DataSource = Util.getDictionary(typeof(FlowExpress.FlowCaseValueChangeKind), false, true);
            FlowCaseValueChanged_ChangeKind.DataValueField = "key";
            FlowCaseValueChanged_ChangeKind.DataTextField = "value";
            FlowCaseValueChanged_ChangeKind.DataBind();

            IsFlowReAssign_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            IsFlowReAssign_FlowID.Refresh();

            FlowSysBtnVerify_FlowID.ucSourceDictionary = Util.getDictionary(_Dic_FlowIDList);
            FlowSysBtnVerify_FlowID.Refresh();
            FlowSysBtnVerify_FlowCloseStepID.ucSourceDictionary = Util.getDictionary(_Dic_FlowCloseStepList);
            FlowSysBtnVerify_FlowCloseStepID.Refresh();
            FlowSysBtnVerify_FlowSysStepBtnID.ucSourceDictionary = Util.getDictionary(_Dic_SysStepBtnList);
            FlowSysBtnVerify_FlowSysStepBtnID.Refresh();

        }
    }

    protected void btnGetFlowCaseValues_Click(object sender, EventArgs e)
    {
        FlowExpress.FlowCaseFieldList tField = (FlowExpress.FlowCaseFieldList)int.Parse(getFlowCaseValues_Field.SelectedValue);
        string[] arResult = FlowExpress.getFlowCaseValues(getFlowCaseValues_FlowID.ucSelectedID, getFlowCaseValues_FlowCaseID.Text, tField);
        if (arResult != null)
            labGetFlowCaseValues.Text = string.Format("[{0}]", string.Join("][", arResult));
        else
            labGetFlowCaseValues.Text = "[null]";
    }

    protected void btnGetFlowLogValues_Click(object sender, EventArgs e)
    {
        FlowExpress.FlowLogFieldList tField = (FlowExpress.FlowLogFieldList)int.Parse(GetFlowLogValues_Field.SelectedValue);
        FlowExpress.FlowLogDataScope tScope = (FlowExpress.FlowLogDataScope)int.Parse(GetFlowLogValues_Scope.SelectedValue);
        bool tAutoDetect = (GetFlowLogValues_AutoDetectStepID.SelectedValue == "Y") ? true : false;
        string[] arResult = FlowExpress.getFlowLogValues(GetFlowLogValues_FlowID.ucSelectedID, GetFlowLogValues_FlowLogID.Text, GetFlowLogValues_SpecStepID.Text, tField, tScope, tAutoDetect);
        if (arResult != null)
            labGetFlowLogValues.Text = string.Format("[{0}]", string.Join("][", arResult));
        else
            labGetFlowLogValues.Text = "[null]";
    }

    protected void btnFlowRollBack_Click(object sender, EventArgs e)
    {
        FlowExpressTraceLog.Init(true);
        labFlowRollBack.Text = FlowExpress.IsFlowRollBack(FlowRollBack_FlowID.ucSelectedID, FlowRollBack_FlowCaseID.Text, FlowRollBack_IsRun.Checked).ToString();
        if (FlowExpressTraceLog.IsError())
        {
            labFlowRollBack.Text += "<hr>" + FlowExpressTraceLog.getFlowExpressTraceLogInfoPage();
        }
    }

    protected void btnFlowDeleted_Click(object sender, EventArgs e)
    {
        FlowExpressTraceLog.Init(true);
        labFlowDeleted.Text = FlowExpress.IsFlowCaseDeleted(FlowDeleted_FlowID.ucSelectedID, FlowDeleted_FlowCaseID.Text, FlowDeleted_IsRun.Checked).ToString();
        if (FlowExpressTraceLog.IsError())
        {
            labFlowDeleted.Text += "<hr>" + FlowExpressTraceLog.getFlowExpressTraceLogInfoPage();
        }
    }

    protected void FlowCaseValueChanged_Click(object sender, EventArgs e)
    {
        FlowExpressTraceLog.Init(true);
        FlowExpress.FlowCaseValueChangeKind tKind = (FlowExpress.FlowCaseValueChangeKind)int.Parse(FlowCaseValueChanged_ChangeKind.SelectedValue);
        labFlowCaseValueChanged.Text = FlowExpress.IsFlowCaseValueChanged(FlowCaseValueChanged_FlowID.ucSelectedID, FlowCaseValueChanged_FlowCaseID.Text, tKind, FlowCaseValueChanged_NewValues.Text.Split(','), FlowCaseValueChanged_ChkFields.Text.Split(',')).ToString();
        if (FlowExpressTraceLog.IsError())
        {
            labFlowCaseValueChanged.Text += "<hr>" + FlowExpressTraceLog.getFlowExpressTraceLogInfoPage();
        }
    }

    protected void btnTraceLog_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(TraceFlowID.ucSelectedID) && !string.IsNullOrEmpty(TraceFlowCaseID.Text))
        {
            gvTraceLogRefresh();
        }
        else
        {
            Util.MsgBox("參數不全");
        }
    }

    protected void btnTraceLogExport_Click(object sender, EventArgs e)
    {
        gvTraceLogRefresh(true);
    }

    protected void btnFullLog_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(FullLogFlowID.ucSelectedID) && !string.IsNullOrEmpty(FullLogFlowCaseID.Text))
        {
            gvFullLogRefresh();
        }
        else
        {
            Util.MsgBox("參數不全");
        }
    }

    protected void btnFullLogExport_Click(object sender, EventArgs e)
    {
        gvFullLogRefresh(true);
    }

    protected void gvTraceLogRefresh(bool IsExport = false)
    {
        string strSQL = "Select LogNo,FlowLogID,IsErrorLog,LogFrom,LogPara,LogDesc,LogSQL From FlowTraceLog Where FlowID = '{0}' And FlowCaseID ='{1}' Order By LogNo {2}";
        strSQL = string.Format(strSQL, TraceFlowID.ucSelectedID, TraceFlowCaseID.Text, TraceSort.SelectedValue);
        DbHelper db = new DbHelper(FlowExpress._FlowSysDB);
        DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            btnTraceLogExport.Visible = true;
            gvTraceLog.Visible = true;
            gvTraceLog.AllowSorting = false;
            gvTraceLog.DataSource = dt.DefaultView;
            gvTraceLog.DataBind();
            if (IsExport) Util.ExportExcel(gvTraceLog);
        }
        else
            Util.MsgBox("找不到相符的資料");

    }

    protected void gvFullLogRefresh(bool IsExport = false)
    {
        string strSQL = "Select * From {0}FlowFullLog Where FlowCaseID ='{1}' Order By FlowLogID {2} ";
        strSQL = string.Format(strSQL, FullLogFlowID.ucSelectedID, FullLogFlowCaseID.Text, FullLogSort.SelectedValue);
        FlowExpress oFlow = new FlowExpress(FullLogFlowID.ucSelectedID, FullLogFlowCaseID.Text, false, false);
        DbHelper db = new DbHelper(oFlow.FlowLogDB);
        DataTable dt = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
        if (dt != null && dt.Rows.Count > 0)
        {
            btnFullLogExport.Visible = true;
            gvFullLog.Visible = true;
            gvFullLog.AllowSorting = false;
            gvFullLog.DataSource = dt.DefaultView;
            gvFullLog.DataBind();
            if (IsExport) Util.ExportExcel(gvFullLog);
        }
        else
            Util.MsgBox("找不到相符的資料");

    }

    protected void btnAttach_Click(object sender, EventArgs e)
    {
        //彈出子視窗
        if (string.IsNullOrEmpty(AdjFlowAttachDB.ucSelectedID) || string.IsNullOrEmpty(txtAttachID.Text))
        {
            Util.NotifyMsg("參數不能空白", Util.NotifyKind.Error);
        }
        else
        {
            ucModalPopup1.ucPopupHeader = "流程相關附件";
            ucModalPopup1.ucFrameURL = string.Format("{0}?AttachDB={1}&AttachID={2}", Util._AttachAdminUrl, AdjFlowAttachDB.ucSelectedID.Split('．')[1], txtAttachID.Text);
            ucModalPopup1.ucBtnCloselEnabled = true;
            ucModalPopup1.ucBtnCancelEnabled = false;
            ucModalPopup1.ucBtnCompleteEnabled = false;
            ucModalPopup1.Show();
        }
    }

    protected void btnDelFlowLog_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(DelFlowID.ucSelectedID) && !string.IsNullOrEmpty(txtDelFlowLogID.Text))
        {
            string strSQL = "Delete {0}FlowFullLog Where FlowLogID = '{1}' ;Delete {0}FlowOpenLog Where FlowLogID = '{1}';";
            strSQL = string.Format(strSQL, DelFlowID.ucSelectedID, HttpUtility.HtmlEncode(txtDelFlowLogID.Text));
            FlowExpress oFlow = new FlowExpress(DelFlowID.ucSelectedID, txtDelFlowLogID.Text, true, false);
            DbHelper db = new DbHelper(oFlow.FlowLogDB);
            int intEffQty = db.ExecuteNonQuery(CommandType.Text, strSQL);
            labDelLog.Text = strSQL;
            if (intEffQty >= 0)
            {
                labDelLog.Text += string.Format("<br>刪除成功，影響 {0} 筆資料！", intEffQty);
            }
            else
            {
                labDelLog.Text += "<br>刪除失敗！";
            }
        }
        else
        {
            Util.MsgBox("參數不全");
        }

    }

    protected void btnFixOpenLog_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(FixFlowID.ucSelectedID))
        {
            string strSQL = "Delete {0}FlowOpenLog Where FlowLogID in (Select FlowLogID From {0}FlowFullLog where FlowLogStatus='Close' )";
            strSQL = string.Format(strSQL, FixFlowID.ucSelectedID);
            FlowExpress oFlow = new FlowExpress(FixFlowID.ucSelectedID, "", false, false);
            DbHelper db = new DbHelper(oFlow.FlowLogDB);
            int intEffQty = db.ExecuteNonQuery(CommandType.Text, strSQL);
            labFixOpenLog.Text = strSQL;
            if (intEffQty >= 0)
            {
                labFixOpenLog.Text += string.Format("<br>刪除成功，影響 {0} 筆資料！", intEffQty);
            }
            else
            {
                labFixOpenLog.Text += "<br>刪除失敗！";
            }
        }
        else
        {
            Util.MsgBox("參數不全");
        }
    }

    protected void btnClearCache_Click(object sender, EventArgs e)
    {
        string strCacheName = "FlowExpress_FlowSpecStepBtn";
        CacheHelper.setCacheClear(strCacheName);
        Util.NotifyMsg("流程快取已清除！", Util.NotifyKind.Success);
    }

    protected void btnOrgExpressTest_Click(object sender, EventArgs e)
    {
        labOrgExpressTest.Text = "";
        if (!string.IsNullOrEmpty(OrgExpressTest_MethodList.ucSelectedID))
        {
            string strFullClassMethodName = "SinoPac.WebExpress.Work.OrgExpress." + OrgExpressTest_MethodList.ucSelectedID;
            labOrgExpressTest.Text = string.Format("<ul>{0}</ul>", Util.getObjectInfo(OrgExpressTest_MethodList.ucSelectedID, Util.getClassMethod(strFullClassMethodName, OrgExpressTest_ParaList.Text.Split(','))));
        }
    }

    protected void IsFlowReAssign_Click(object sender, EventArgs e)
    {
        FlowExpressTraceLog.Init(true);
        if (IsFlowReAssign_ReAssignTo.Text.IsJSON())
        {
            //2017.01.25 新增改派對象可使用 JSON 格式
            labIsFlowReAssign.Text = FlowExpress.IsFlowReAssign(IsFlowReAssign_FlowID.ucSelectedID, IsFlowReAssign_FlowLogID.Text, Util.getDictionary(IsFlowReAssign_ReAssignTo.Text), IsFlowReAssign_Opinion.Text, IsFlowReAssign_IsRun.Checked).ToString();
        }
        else
        {
            labIsFlowReAssign.Text = FlowExpress.IsFlowReAssign(IsFlowReAssign_FlowID.ucSelectedID, IsFlowReAssign_FlowLogID.Text, IsFlowReAssign_ReAssignTo.Text, IsFlowReAssign_Opinion.Text, IsFlowReAssign_IsRun.Checked).ToString();
        }

        if (FlowExpressTraceLog.IsError())
        {
            labIsFlowReAssign.Text += "<hr>" + FlowExpressTraceLog.getFlowExpressTraceLogInfoPage();
        }
    }

    //2015.07.24 新增
    protected void btnFlowSysBtnVerify_Click(object sender, EventArgs e)
    {
        FlowExpressTraceLog.Init(true);
        FlowExpress oFlow = new FlowExpress(FlowSysBtnVerify_FlowID.ucSelectedID, FlowSysBtnVerify_FlowCaseID.Text, false, false);
        if (oFlow != null)
        {
            labFlowSysBtnVerify.Text = FlowExpress.IsFlowSysStepBtnVerify(oFlow.FlowID, oFlow.FlowCurrLastLogID, FlowSysBtnVerify_FlowSysStepBtnID.ucSelectedID, FlowSysBtnVerify_FlowCloseStepID.ucSelectedID, null, FlowSysBtnVerify_Opinion.Text).ToString();
            if (FlowExpressTraceLog.IsError())
            {
                labFlowSysBtnVerify.Text += "<hr>" + FlowExpressTraceLog.getFlowExpressTraceLogInfoPage();
            }
        }
        else
        {
            labFlowSysBtnVerify.Text = "初始 FlowExpress 物件錯誤";
        }
    }
}