using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using System.Data;
using SinoPac.WebExpress.Common.Properties;




public partial class OverTime_Overtime_Detail : SecurePage
{
    int Hours2 = 120;
    int Hours12 = 1440;
    public string _CurrOTCompID
    {
        get
        {
            if (ViewState["_CurrOTCompID"] == null)
            {
                ViewState["_CurrOTCompID"] = (Request["OTCompID"] != null) ? Request["OTCompID"].ToString() : "";
            }
            return (string)(ViewState["_CurrOTCompID"]);
        }
        set
        {
            ViewState["_CurrOTCompID"] = value;
        }
    }
    public string _CurrOTEmpID
    {
        get
        {
            if (ViewState["_CurrOTEmpID"] == null)
            {
                ViewState["_CurrOTEmpID"] = (Request["OTEmpID"] != null) ? Request["OTEmpID"].ToString() : "";
            }
            return (string)(ViewState["_CurrOTEmpID"]);
        }
        set
        {
            ViewState["_CurrOTEmpID"] = value;
        }
    }
    public string _CurrOTStartDate
    {
        get
        {
            if (ViewState["_CurrOTStartDate"] == null)
            {
                ViewState["_CurrOTStartDate"] = (Request["OTStartDate"] != null) ? Request["OTStartDate"].ToString() : "";
            }
            return (string)(ViewState["_CurrOTStartDate"]);
        }
        set
        {
            ViewState["_CurrOTStartDate"] = value;
        }
    }
    public string _CurrOTSeq
    {
        get
        {
            if (ViewState["_CurrOTSeq"] == null)
            {
                ViewState["_CurrOTSeq"] = (Request["OTSeq"] != null) ? Request["OTSeq"].ToString() : "";
            }
            return (string)(ViewState["_CurrOTSeq"]);
        }
        set
        {
            ViewState["_CurrOTSeq"] = value;
        }
    }

    public string _CurrOTEndDate
    {
        get
        {
            if (ViewState["_CurrOTEndDate"] == null)
            {
                ViewState["_CurrOTEndDate"] = (Request["OTEndDate"] != null) ? Request["OTEndDate"].ToString() : "";
            }
            return (string)(ViewState["_CurrOTEndDate"]);
        }
        set
        {
            ViewState["_CurrOTEndDate"] = value;
        }
    }
    public string _CurrFlowCaseID
    {
        get
        {
            if (ViewState["_CurrFlowCaseID"] == null)
            {
                ViewState["_CurrFlowCaseID"] = (Request["FlowCaseID"] != null) ? Request["FlowCaseID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowCaseID"]);
        }
        set
        {
            ViewState["_CurrFlowCaseID"] = value;
        }
    }
    public string _CurrFlowLogID
    {
        get
        {
            if (ViewState["_CurrFlowLogID"] == null)
            {
                ViewState["_CurrFlowLogID"] = (Request["FlowLogID"] != null) ? Request["FlowLogID"].ToString() : "";
            }
            return (string)(ViewState["_CurrFlowLogID"]);
        }
        set
        {
            ViewState["_CurrFlowLogID"] = value;
        }
    }

    public string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            subGetData();
            ViewState["Input"] = "";
            TabDisable();
        }
    }
    private void TabDisable()
    {
        DataTable dtOverTimeAdvance = (DataTable)Session["dtOverTimeAdvance"];
        DataTable dtOverTimeDeclaration = (DataTable)Session["dtOverTimeDeclaration"];
        if (!(dtOverTimeAdvance == null && dtOverTimeDeclaration == null))
        {
            string strTabInitJS = @"dom.Ready(function(){ 
                var oTabHead1 = window.parent.document.getElementById('TabMainContainer_tabCustForm_tab');
                var oTabHead2 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_tab');
                var oTabBody1 = window.parent.document.getElementById('TabMainContainer_tabCustForm');
                var oTabBody2 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify');
                var oTabBody3 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_FlowLogFrame');
                var oBtn1 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_ucCommUserAdminButton_btnLaunch');
//                var oTabHead4 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_labPrevStepOpinion');
//                var oTabBody4 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_gvFlowPrevStepLog_divGridview');

                if (oTabHead1 != null && oTabHead1 != null && oTabBody1 != null && oTabBody2 != null){
                        oTabHead1.style.display='none';
                        oTabHead2.className='ajax__tab_active';
                        oTabBody1.style.visibility='hidden';
                        oTabBody2.style.visibility='visible';
                        oTabBody1.style.display='none';
                        oTabBody2.style.display='';
                        oTabBody3.style.visibility='visible';
                        oTabBody3.style.display='none';
                        oBtn1.style.visibility='visible';
                        oBtn1.style.display='none';
//                        oTabHead4.style.visibility='visible';
//                        oTabHead4.style.display='none';
//                        oTabBody4.style.visibility='visible';
//                        oTabBody4.style.display='none';
                }
        });";
            Util.setJSContent(strTabInitJS, "Parent_TabMainContainer_Init");
        }
        else
        {
            string strTabInitJS = @"dom.Ready(function(){ 
                var oBtn1 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_ucCommUserAdminButton_btnLaunch');
//                var oTabHead4 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_labPrevStepOpinion');
//                var oTabBody4 = window.parent.document.getElementById('TabMainContainer_tabFlowVerify_gvFlowPrevStepLog_divGridview');
                oBtn1.style.visibility='visible';
                oBtn1.style.display='none';
//                oTabHead4.style.visibility='visible';
//                oTabHead4.style.display='none';
//                oTabBody4.style.visibility='visible';
//                oTabBody4.style.display='none';
        });";
            Util.setJSContent(strTabInitJS, "Parent_TabMainContainer_Init");
        }
    }
    public void subGetData()
    {
        _CurrFlowCaseID = _CurrFlowLogID.Remove(14);
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        Aattendant at = new Aattendant();
        string strdata = "";
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        sb.Append("SELECT OT.OTCompID,C.CompName,OT.DeptID,OT.DeptName,OT.OrganID,OT.OrganName,OT.SalaryOrAdjust,CONVERT(varchar(12),OT.AdjustInvalidDate,111) as AdjustInvalidDate,P.NameN,OT.OTRegisterID,PR.NameN AS RegisterNameN,");
        sb.Append("OT.OTTxnID,OT.OTStartTime,");
        sb.Append("isnull(OT1.OTEndTime,OT.OTEndTime) as OTEndTime,isnull(OT1.OTEndDate,OT.OTEndDate) as OTEndDate,");
        sb.Append(" OT.MealFlag,OT.MealTime+isnull(OT1.MealTime,0) as MealTime,OT.OTTotalTime+isnull(OT1.OTTotalTime,0) as OTTotalTime,OTT.CodeCName as OTTypeID,OT.OTReasonID,OT.OTReasonMemo,");
        //sb.Append(" OT.MealFlag,OT.MealTime+isnull(OT1.MealTime,0) as MealTime,OT.OTTotalTime+isnull(OT1.OTTotalTime,0) as OTTotalTime,OTT.OTTypeName as OTTypeID,OT.OTReasonID,OT.OTReasonMemo,");
        sb.Append(" OT.LastChgID,PL.NameN AS LastChgNameN,CONVERT(varchar(12),OT.LastChgDate,111)+' '+CONVERT(varchar(8),OT.LastChgDate,108) as LastChgDate,ISNULL(AI.FileName,'') AS FileName,OT.OTAttachment,'A' as Input ");
        //時間分割
        //sb.Append(" OT1.OTTotalTime as StartHr,OT.OTTotalTime as EndHr");
        sb.Append(" FROM OverTimeAdvance OT");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] C ON C.CompID=OT.OTCompID AND C.InValidFlag = '0' And C.NotShowFlag = '0'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.CompID = OT.OTCompID AND P.EmpID=OT.OTEmpID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PR ON PR.CompID = OT.OTRegisterComp AND PR.EmpID=OT.OTRegisterID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PL ON PL.CompID = OT.LastChgComp AND PL.EmpID=OT.LastChgID");
        //sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID = OT.OTAttachment"); 
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment  AND FileSize > 0");//20170209 leo modify -參考Judy的After
        //sb.Append(" LEFT JOIN OverTimeAdvance OT1 on OT1.OTCompID='" + _CurrOTCompID + "' AND OT1.OTEmpID='" + _CurrOTEmpID + "' AND OT1.OTStartDate='" + _CurrOTEndDate + "' AND OT1.OTSeqNo='" + 2 + "'  AND OT1.OTEndDate='" + _CurrOTEndDate + "' AND OT1.FlowCaseID='" + _CurrFlowCaseID + "'");
        sb.Append(" LEFT JOIN OverTimeAdvance OT1 on OT1.OTCompID='" + _CurrOTCompID + "' AND OT1.OTEmpID='" + _CurrOTEmpID + "' AND OT1.OTSeqNo='" + 2 + "' AND OT1.FlowCaseID='" + _CurrFlowCaseID + "'");
        sb.Append(" LEFT JOIN AT_CodeMap OTT  ON OTT.TabName='OverTime' and OTT.FldName='OverTimeType' and OT.OTTypeID = OTT.Code");
        //sb.Append(" LEFT JOIN OverTimeType OTT ON OT.OTCompID = OTT.CompID and OT.OTTypeID = OTT.OTTypeId");
        sb.Append(" WHERE OT.OTCompID='" + _CurrOTCompID + "'");
        sb.Append(" AND OT.OTEmpID='" + _CurrOTEmpID + "'");
        sb.Append(" AND OT.OTStartDate='" + _CurrOTStartDate + "'");
        sb.Append(" AND OT.OTSeq='" + _CurrOTSeq + "'");
        sb.Append(" AND OT.OTEndDate='" + _CurrOTStartDate + "'");
        sb.Append(" AND OT.FlowCaseID='" + _CurrFlowCaseID + "'");
        sb.Append(" UNION");
        sb.Append(" SELECT OD.OTCompID,C.CompName,OD.DeptID,OD.DeptName,OD.OrganID,OD.OrganName,OD.SalaryOrAdjust,CONVERT(varchar(12),OD.AdjustInvalidDate,111) as AdjustInvalidDate,P.NameN,OD.OTRegisterID,PR.NameN AS RegisterNameN,");
        sb.Append("OD.OTTxnID,OD.OTStartTime,");
        //sb.Append(" OD.OTEndTime,OD.OTEndDate,");
        sb.Append("isnull(OD1.OTEndTime,OD.OTEndTime) as OTEndTime,isnull(OD1.OTEndDate,OD.OTEndDate) as OTEndDate,");
        sb.Append(" OD.MealFlag,OD.MealTime+isnull(OD1.MealTime,0) as MealTime,OD.OTTotalTime+isnull(OD1.OTTotalTime,0) as OTTotalTime,ODT.CodeCName as OTTypeID,OD.OTReasonID,OD.OTReasonMemo,");
        sb.Append(" OD.LastChgID,PL.NameN AS LastChgNameN,CONVERT(varchar(12),OD.LastChgDate,111)+' '+CONVERT(varchar(8),OD.LastChgDate,108) as LastChgDate,ISNULL(AI.FileName,'') AS FileName,OD.OTAttachment,'D' as Input");
        sb.Append(" FROM OverTimeDeclaration OD");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] C ON C.CompID=OD.OTCompID AND C.InValidFlag = '0' And C.NotShowFlag = '0'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.CompID = OD.OTCompID AND P.EmpID=OD.OTEmpID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PR ON PR.CompID = OD.OTRegisterComp AND PR.EmpID=OD.OTRegisterID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PL ON PL.CompID = OD.LastChgComp AND PL.EmpID=OD.LastChgID");
        //sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID = OD.OTAttachment");
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OD.OTAttachment  AND FileSize > 0");//20170209 leo modify -參考Judy的After
        sb.Append(" LEFT JOIN OverTimeDeclaration OD1 on OD1.OTCompID='" + _CurrOTCompID + "' AND OD1.OTEmpID='" + _CurrOTEmpID + "' AND OD1.OTSeqNo='" + 2 + "' AND OD1.FlowCaseID='" + _CurrFlowCaseID + "'");
        sb.Append(" LEFT JOIN AT_CodeMap ODT  ON ODT.TabName='OverTime' and ODT.FldName='OverTimeType' and OD.OTTypeID = ODT.Code");
        //sb.Append(" LEFT JOIN OverTimeType ODT ON OD.OTCompID = ODT.CompID and OD.OTTypeID = ODT.OTTypeId");
        sb.Append(" WHERE OD.OTCompID='" + _CurrOTCompID + "'");
        sb.Append(" AND OD.OTEmpID='" + _CurrOTEmpID + "'");
        sb.Append(" AND OD.OTStartDate='" + _CurrOTStartDate + "'");
        sb.Append(" AND OD.OTSeq='" + _CurrOTSeq + "'");
        sb.Append(" AND OD.OTEndDate='" + _CurrOTStartDate + "'");
        sb.Append(" AND OD.FlowCaseID='" + _CurrFlowCaseID + "'");

        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Input"].ToString() == "D") Label3.Text = "月已申報時數合計：  送簽";
            //lblCompID.Text = dt.Rows[0]["OTCompID"].ToString();
            lblCompName.Text = dt.Rows[0]["CompName"].ToString();
            //lblDeptID.Text = dt.Rows[0]["DeptID"].ToString();
            lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
            //lblOrganID.Text = dt.Rows[0]["OrganID"].ToString();
            lblOrganName.Text = dt.Rows[0]["OrganName"].ToString();
            lblEmpID.Text = _CurrOTEmpID;
            lblSalaryOrAdjust.Text = dt.Rows[0]["SalaryOrAdjust"].ToString() == "2" ? "轉補休" : "轉薪資";
            lblAdjustInvalidDate.Visible = dt.Rows[0]["SalaryOrAdjust"].ToString() == "2";
            txtAdjustInvalidDate.Visible = dt.Rows[0]["SalaryOrAdjust"].ToString() == "2";
            txtAdjustInvalidDate.Text = dt.Rows[0]["AdjustInvalidDate"].ToString();
            lblEmpName.Text = dt.Rows[0]["NameN"].ToString();
            lblOTRegisterID.Text = dt.Rows[0]["OTRegisterID"].ToString();
            lblOTRegisterName.Text = dt.Rows[0]["RegisterNameN"].ToString();
            lblOTDateValue.Text = _CurrOTStartDate;
            lblOTDateValueEnd.Text = dt.Rows[0]["OTEndDate"].ToString();
            lblBeginTime.Text = dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + ":" + dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2);
            //lblBeginTime.Text = dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) + ":" + dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2);
            lblEndTime.Text = dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + ":" + dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            //lblEndTime.Text = dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2) + ":" + dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2);
            chkMealFlag.Checked = (dt.Rows[0]["MealFlag"].ToString() == "1") ? true : false;
            lblMealTime.Text = dt.Rows[0]["MealTime"].ToString();
            //lblOTTotalTime.Text = (Convert.ToDouble(dt.Rows[0]["OTTotalTime"].ToString()) - Convert.ToDouble(dt.Rows[0]["MealTime"].ToString())/60).ToString("0.0").ToString();
            //20170213 leo modify TotalTime 依照起訖時間分鐘計算
            Double TotalTime =
                _CurrOTStartDate.ToString() == dt.Rows[0]["OTEndDate"].ToString() ?
                 (Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2)) * 60) + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2)) - (Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2)) * 60) - Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2)) :
                1440.0 - (Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2)) * 60) - Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2)) + (Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2)) * 60) + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2));
            TotalTime = TotalTime - (chkMealFlag.Checked ? Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()) : 0);
            //Double TotalTime = 0.0;
            //if (_CurrOTStartDate.ToString() == dt.Rows[0]["OTEndDate"].ToString())
            //    TotalTime = (Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2)) * 60) + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2)) - (Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2)) * 60) - Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2));
            //else
            //TotalTime = 1440.0 - (Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2)) * 60) - Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2)) + (Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2)) * 60) + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2));

            lblOTTotalTime.Text = Math.Round(TotalTime / 60, 1, MidpointRounding.AwayFromZero).ToString("0.0");

            lblOTTypeID.Text = dt.Rows[0]["OTTypeID"].ToString();

            lblOTReasonMemo.Text = dt.Rows[0]["OTReasonMemo"].ToString();
            lblLastChgID.Text = dt.Rows[0]["LastChgID"].ToString();
            lblLastChgName.Text = dt.Rows[0]["LastChgNameN"].ToString();
            lblLastChgDate.Text = dt.Rows[0]["LastChgDate"].ToString();
            ViewState["Input"] = dt.Rows[0]["Input"].ToString() == "A" ? "OverTimeAdvance" : "OverTimeDeclaration";
            //StartHr = Convert.ToDouble(dt.Rows[0]["StartHr"].ToString());
            //EndHr = Convert.ToDouble(dt.Rows[0]["EndHr"].ToString());
            if (dt.Rows[0]["FileName"].ToString() == "")
            {
                btnAttachDownload.Visible = false;
            }
            else
            {
                lblAttachName.Text = dt.Rows[0]["FileName"].ToString();
                btnAttachDownload.ucAttachDB = _DBName;
                btnAttachDownload.ucAttachID = dt.Rows[0]["OTAttachment"].ToString();
            }

            SignData();
            if (_CurrOTStartDate == dt.Rows[0]["OTEndDate"].ToString())
            {
                if (dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2) == "00")
                {
                    cntStart = Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2));
                }
                else
                {
                    cntStart = ((Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2))) * 60) + Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2));
                }
                cntEnd = ((Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2))) * 60) + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2));

                cntTotal = cntEnd - cntStart;

                DataTable dt1 = at.QueryData("SUM(OTTotalTime) AS OTTotalTime", "OverTimeDeclaration", " AND OTStatus='3' AND OTStartDate='" + _CurrOTStartDate + "' AND OTEndDate='" + dt.Rows[0]["OTEndDate"].ToString() + "'");
                double hr;//資料庫的加班總時數
                if (dt1.Rows[0]["OTTotalTime"].ToString() == "")
                {
                    hr = 0.0;
                }
                else
                {
                    hr = Convert.ToDouble(dt.Rows[0]["OTTotalTime"].ToString());
                }
                if (at.PeriodCount(ViewState["Input"].ToString(), _CurrOTEmpID, cntEnd - cntStart, 0,
                        Convert.ToInt32(dt.Rows[0]["OTStartTime"]), Convert.ToInt32(dt.Rows[0]["OTEndTime"]), _CurrOTStartDate,
                        0, 0, "1900/01/01",
                        Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()), dt.Rows[0]["MealFlag"].ToString(), dt.Rows[0]["OTTxnID"].ToString(), out strdata))
                { }
                else
                { Util.MsgBox("時段有問題"); }
                //strdata = at.OneDay(ViewState["Input"].ToString(), _CurrOTEmpID, cntTotal / 60, hr, _CurrOTStartDate, Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()) / 60, dt.Rows[0]["MealFlag"].ToString(), dt.Rows[0]["OTStartTime"].ToString(), dt.Rows[0]["OTEndTime"].ToString(), _CurrOTSeq);
                //strdata = at.OneDay(cntTotal, hr, _CurrOTStartDate, Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()), dt.Rows[0]["MealFlag"].ToString());
                strdata = "0;0;" + strdata;
                trOne.Visible = true;
                lblDateOne.Text = (strdata.Split(';')[2]).Split(',')[0];
                lblDateOne_0.Text = ((strdata.Split(';')[2]).Split(',')[1] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[1];
                lblDateOne_1.Text = ((strdata.Split(';')[2]).Split(',')[2] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[2];
                lblDateOne_2.Text = ((strdata.Split(';')[2]).Split(',')[3] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[3];
            }
            else
            {
                cntStart = (23 - (Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(0, 2)))) * 60 + (60 - Convert.ToDouble(dt.Rows[0]["OTStartTime"].ToString().Substring(2, 2)));
                cntEnd = (Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(0, 2))) * 60 + Convert.ToDouble(dt.Rows[0]["OTEndTime"].ToString().Substring(2, 2));
                DataTable dtStart = at.QueryData("SUM(OTTotalTime) AS OTTotalTime", "OverTimeDeclaration", " AND OTStatus='3' AND OTStartDate='" + _CurrOTStartDate + "' AND OTEndDate='" + _CurrOTStartDate + "'");
                DataTable dtEnd = at.QueryData("SUM(OTTotalTime) AS OTTotalTime", "OverTimeDeclaration", " AND OTStatus='3' AND OTStartDate='" + dt.Rows[0]["OTEndDate"].ToString() + "' AND OTEndDate='" + dt.Rows[0]["OTEndDate"].ToString() + "'");
                double hrStart;//資料庫的加班總時數
                if (dtStart.Rows[0]["OTTotalTime"].ToString() == "")
                {
                    hrStart = 0.0;
                }
                else
                {
                    hrStart = Convert.ToDouble(dtStart.Rows[0]["OTTotalTime"].ToString());
                }
                double hrEnd;//資料庫的加班總時數
                if (dtEnd.Rows[0]["OTTotalTime"].ToString() == "")
                {
                    hrEnd = 0.0;
                }
                else
                {
                    hrEnd = Convert.ToDouble(dtEnd.Rows[0]["OTTotalTime"].ToString());
                }
                if (at.PeriodCount(ViewState["Input"].ToString(), _CurrOTEmpID, cntStart, cntEnd,
                        Convert.ToInt32(dt.Rows[0]["OTStartTime"]), 2359, _CurrOTStartDate,
                        0, Convert.ToInt32(dt.Rows[0]["OTEndTime"]), dt.Rows[0]["OTEndDate"].ToString(),
                        Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()), dt.Rows[0]["MealFlag"].ToString(), dt.Rows[0]["OTTxnID"].ToString(), out strdata))
                { }
                else
                { Util.MsgBox("時段有問題"); }
                //strdata = at.TwoDay(ViewState["Input"].ToString(), _CurrOTEmpID, cntStart / 60, cntEnd / 60, hrStart / 60, hrEnd / 60, _CurrOTStartDate, dt.Rows[0]["OTEndDate"].ToString(), Convert.ToDouble(dt.Rows[0]["MealTime"].ToString()) / 60, dt.Rows[0]["MealFlag"].ToString(), dt.Rows[0]["OTTxnID"].ToString());
                strdata = "0;0;" + strdata;
                trOne.Visible = true;
                trTwo.Visible = true;
                lblDateOne.Text = (strdata.Split(';')[2]).Split(',')[0];
                lblDateOne_0.Text = ((strdata.Split(';')[2]).Split(',')[1] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[1];
                lblDateOne_1.Text = ((strdata.Split(';')[2]).Split(',')[2] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[2];
                lblDateOne_2.Text = ((strdata.Split(';')[2]).Split(',')[3] == "0.0") ? "-" : (strdata.Split(';')[2]).Split(',')[3];

                lblDateTwo.Text = (strdata.Split(';')[3]).Split(',')[0];
                lblDateTwo_0.Text = ((strdata.Split(';')[3]).Split(',')[1] == "0.0") ? "-" : (strdata.Split(';')[3]).Split(',')[1];
                lblDateTwo_1.Text = ((strdata.Split(';')[3]).Split(',')[2] == "0.0") ? "-" : (strdata.Split(';')[3]).Split(',')[2];
                lblDateTwo_2.Text = ((strdata.Split(';')[3]).Split(',')[3] == "0.0") ? "-" : (strdata.Split(';')[3]).Split(',')[3];
            }
        }
        else
        {
            return;
        }
        string meal = (chkMealFlag.Checked == false) ? "0" : lblMealTime.Text;
        //lblOTTotalTime.Text = Convert.ToString(Convert.ToDouble(cntStart + cntEnd - Convert.ToDouble(meal)) / 60);
        txtTotalDescription.Text = (lblMealTime.Text != "" && Convert.ToDouble(lblMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + lblMealTime.Text + "分鐘)" : "";
        txtTotalDescription.Visible = true;
    }

    public bool IsDataExists(String strTable, String strWhere)
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append("Select Count(*) Cnt From " + strTable);
        sb.Append(" Where 1 = 1 " + strWhere);
        return int.Parse(db.ExecuteScalar(sb.BuildCommand()).ToString()) == 0 ? true : false;
    }
    //public int[] testtest(int a, int b ,int c)
    //{
    //    int[] d = new int[3];
    //    if (b <= (c - a))
    //    {
    //        d[0] = a + b;
    //        d[1] = 0;
    //    }
    //    else
    //    {
    //        d[0] = c;
    //        d[1]=b-(c-a);
    //    }
    //    return d;
    //}
    public void SignData()//本月的總時數
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //sb.Append("SELECT A.Submit,A.Approval,(A.Submit+A.Approval) AS Total,A.Reject FROM (");
        ////sb.Append(" SELECT SUM(CASE OTStatus WHEN '1' THEN OTTotalTime ELSE '0.0' END) AS Submit,");
        ////sb.Append(" SUM(CASE OTStatus WHEN '2' THEN OTTotalTime ELSE '0.0' END)  AS Approval,");
        ////sb.Append(" SUM(CASE OTStatus WHEN '3' THEN OTTotalTime ELSE '0.0' END) AS Reject");
        //sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE MealFlag WHEN 1 THEN (OTTotalTime-CAST(MealTime AS FLOAT)) ELSE OTTotalTime END) ELSE '0.0' END) AS Submit,");
        //sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE MealFlag WHEN 1 THEN (OTTotalTime-CAST(MealTime AS FLOAT)) ELSE OTTotalTime END) ELSE '0.0' END)  AS Approval,");
        //sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE MealFlag WHEN 1 THEN (OTTotalTime-CAST(MealTime AS FLOAT)) ELSE OTTotalTime END) ELSE '0.0' END) AS Reject");

        //sb.Append(" FROM "+ViewState["Input"].ToString());
        //sb.Append(" WHERE OTCompID = '" + _CurrOTCompID + "' AND OTEmpID = '" + _CurrOTEmpID + "'");
        //sb.Append(" AND MONTH(OTStartDate) = MONTH('" + _CurrOTStartDate + "') ");
        //sb.Append(" AND YEAR(OTStartDate) = YEAR('" + _CurrOTStartDate + "') ");
        //sb.Append(" AND OTEmpID = '" + _CurrOTEmpID + "') A");

        /*========================================*/
        //20170306 leo-modify
        sb.Append("SELECT sum(Round(A.Submit/60,1)) as Submit,sum(Round(A.Approval/60,1))as Approval,sum(Round((A.Submit+A.Approval)/60,1)) AS Total,sum(Round(A.Reject /60,1))as Reject FROM ( ");
        sb.Append("select ");
        sb.Append(" CASE O1.OTStatus WHEN '2' THEN (CASE O1.MealFlag WHEN 1 THEN (O1.OTTotalTime-CAST(O1.MealTime AS DECIMAL(10,2)))+isnull(O2.OTTotalTime-CAST(O2.MealTime AS DECIMAL(10,2)),0) ELSE O1.OTTotalTime+isnull(O2.OTTotalTime,0) END) ELSE 0 END as Submit,");
        sb.Append(" CASE O1.OTStatus WHEN '3' THEN (CASE O1.MealFlag WHEN 1 THEN (O1.OTTotalTime-CAST(O1.MealTime AS DECIMAL(10,2)))+isnull(O2.OTTotalTime-CAST(O2.MealTime AS DECIMAL(10,2)),0) ELSE O1.OTTotalTime+isnull(O2.OTTotalTime,0) END) ELSE 0 END  AS Approval,");
        sb.Append(" CASE O1.OTStatus WHEN '4' THEN (CASE O1.MealFlag WHEN 1 THEN (O1.OTTotalTime-CAST(O1.MealTime AS DECIMAL(10,2)))+isnull(O2.OTTotalTime-CAST(O2.MealTime AS DECIMAL(10,2)),0) ELSE O1.OTTotalTime+isnull(O2.OTTotalTime,0) END) ELSE 0 END AS Reject ");

        sb.Append(" FROM " + ViewState["Input"].ToString() + " O1 ");
        sb.Append(" left join " + ViewState["Input"].ToString() + " O2 on O1.OTTxnID=O2.OTTxnID and O2.OTSeqNo='2' ");
        sb.Append(" WHERE O1.OTCompID = '" + _CurrOTCompID + "'");
        sb.Append(" AND O1.OTEmpID = '" + _CurrOTEmpID + "'");
        sb.Append(" AND MONTH(O1.OTStartDate) = MONTH('" + _CurrOTStartDate + "') ");
        sb.Append(" AND YEAR(O1.OTStartDate) = YEAR('" + _CurrOTStartDate + "') ");
        sb.Append(" AND O1.OTSeqNo = '1') A");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        txtOTDateMonth.Text = (_CurrOTStartDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? _CurrOTStartDate.Substring(5, 2).Substring(1, 1).ToString() : _CurrOTStartDate.Substring(5, 2);
        //string test1 = dt.Rows[0]["Submit"].ToString(), test2 = dt.Rows[0]["Approval"].ToString(), test3 = dt.Rows[0]["Reject"].ToString();
        if (dt.Rows.Count > 0)
        {
            lblSubmit.Text = (dt.Rows[0]["Submit"].ToString() == "") ? "0.0" : Convert.ToDouble(dt.Rows[0]["Submit"].ToString()).ToString("0.0");//本月送簽總時數
            lblApproval.Text = (dt.Rows[0]["Approval"].ToString() == "") ? "0.0" : Convert.ToDouble(dt.Rows[0]["Approval"].ToString()).ToString("0.0");//本月核准總時數
            lblReject.Text = (dt.Rows[0]["Reject"].ToString() == "") ? "0.0" : Convert.ToDouble(dt.Rows[0]["Reject"].ToString()).ToString("0.0");//本月駁回總時數
        }
    }
}