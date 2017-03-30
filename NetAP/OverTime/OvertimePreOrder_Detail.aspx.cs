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

public partial class OverTime_OvertimePreOrder_Detail : BasePage
{
    private string _OTCompID
    {
        get
        {
            if (ViewState["_OTCompID"] == null)
            {
                ViewState["_OTCompID"] = "";
            }
            return (string)ViewState["_OTCompID"];
        }
        set
        {
            ViewState["_OTCompID"] = value;
        }
    }
    private string _EmpID
    {
        get
        {
            if (ViewState["_EmpID"] == null)
            {
                ViewState["_EmpID"] = "";
            }
            return (string)ViewState["_EmpID"];
        }
        set
        {
            ViewState["_EmpID"] = value;
        }
    }
    private string _OTStartDate
    {
        get
        {
            if (ViewState["_OTStartDate"] == null)
            {
                ViewState["_OTStartDate"] = "";
            }
            return (string)ViewState["_OTStartDate"];
        }
        set
        {
            ViewState["_OTStartDate"] = value;
        }
    }
    private string _OTEndDate
    {
        get
        {
            if (ViewState["_OTEndDate"] == null)
            {
                ViewState["_OTEndDate"] = "";
            }
            return (string)ViewState["_OTEndDate"];
        }
        set
        {
            ViewState["_OTEndDate"] = value;
        }
    }
    private string _OTStartTime
    {
        get
        {
            if (ViewState["_OTStartTime"] == null)
            {
                ViewState["_OTStartTime"] = "";
            }
            return (string)ViewState["_OTStartTime"];
        }
        set
        {
            ViewState["_OTStartTime"] = value;
        }
    }
    private string _OTEndTime
    {
        get
        {
            if (ViewState["_OTEndTime"] == null)
            {
                ViewState["_OTEndTime"] = "";
            }
            return (string)ViewState["_OTEndTime"];
        }
        set
        {
            ViewState["_OTEndTime"] = value;
        }
    }
    private string _OTSeq
    {
        get
        {
            if (ViewState["_OTSeq"] == null)
            {
                ViewState["_OTSeq"] = "";
            }
            return (string)ViewState["_OTSeq"];
        }
        set
        {
            ViewState["_OTSeq"] = value;
        }
    }
    private string _OTTxnID
    {
        get
        {
            if (ViewState["_OTTxnID"] == null)
            {
                ViewState["_OTTxnID"] = "";
            }
            return (string)ViewState["_OTTxnID"];
        }
        set
        {
            ViewState["_OTTxnID"] = value;
        }
    }
    
    public string _DBName = Util.getAppSetting("app://AattendantDB_OverTime/");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _OTCompID = (Request["OTCompID"] != null) ? Request["OTCompID"].ToString() : "";
            _EmpID = (Request["EmpID"] != null) ? Request["EmpID"].ToString() : "";
            _OTStartDate = (Request["OTStartDate"] != null) ? Request["OTStartDate"].ToString() : "";
            _OTEndDate = (Request["OTEndDate"] != null) ? Request["OTEndDate"].ToString() : "";
            _OTStartTime = (Request["OTStartTime"] != null) ? Request["OTStartTime"].ToString() : "";
            _OTEndTime = (Request["OTEndTime"] != null) ? Request["OTEndTime"].ToString() : "";
            _OTSeq = (Request["OTSeq"] != null) ? Request["OTSeq"].ToString() : "";
            _OTTxnID = (Request["OTTxnID"] != null) ? Request["OTTxnID"].ToString() : "";
            subGetData();
            if (Request["FlowCaseID"] != "")
            {
                string strFlowLogDisplayURL = string.Format("{0}?FlowID={1}&FlowCaseID={2}", FlowExpress._FlowLogDisplayURL, Request["FlowID"], Request["FlowCaseID"]);
                FlowLogFrame.Attributes["src"] = strFlowLogDisplayURL;
                FlowLogFrame.Attributes["width"] = "960";
                FlowLogFrame.Attributes["height"] = "750";
            }
        }
    }

    public void subGetData()
    {
        Aattendant at = new Aattendant();
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = null;
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        sb.Append(" SELECT * FROM (");
        sb.Append(" SELECT OT.OTEmpID,OT.OTCompID,C.CompName,OT.DeptID,OT.DeptName,OT.OrganID,OT.OrganName,OT.OTFormNO,P.NameN,OT.OTRegisterID,PR.NameN AS RegisterNameN,OT.OTTxnID,OTT.CodeCName,ISNULL(AI.FileName,'') AS FileName,OT.OTStatus,OT.MealFlag,isnull(OT.MealTime,0)+isnull(OT2.MealTime,0) AS MealTime, OT.OTSeq,");
        sb.Append(" Case OT.OTStatus WHEN '1' THEN '暫存' WHEN '2' THEN '送簽' WHEN '3' THEN '核准' WHEN '4' THEN '駁回' WHEN '5' THEN '刪除' WHEN '9' THEN '取消' END AS OTStatusName,");
        sb.Append(" CASE OT.SalaryOrAdjust WHEN '1' THEN '轉薪資' WHEN '2' THEN '轉補休' END AS SalaryOrAdjustName,OT.OTAttachment, ");
        sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,");
        sb.Append(" (OT.OTStartTime+'~'+isnull(OT2.OTEndTime,OT.OTEndTime)) AS OTTime, OT.AdjustInvalidDate,");
        sb.Append(" Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1)) AS OTTotalTime,");
        //sb.Append(" ((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT)) AS OTTotalTime,");
        sb.Append(" OT.LastChgID,PL.NameN AS LastChgNameN,OT.LastChgDate,OT.OTReasonMemo");
        sb.Append(" FROM OverTimeAdvance OT ");
        sb.Append(" LEFT JOIN OverTimeAdvance OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] C ON C.CompID=OT.OTCompID AND C.InValidFlag = '0' And C.NotShowFlag = '0'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.CompID = OT.OTCompID AND P.EmpID=OT.OTEmpID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PR ON PR.CompID = OT.OTRegisterComp AND PR.EmpID=OT.OTRegisterID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] C2 ON C2.CompID=OT.LastChgComp AND C2.InValidFlag = '0' And C2.NotShowFlag = '0'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PL ON PL.CompID = C2.CompID AND PL.EmpID=OT.LastChgID");
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment  AND FileSize > 0");
        sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND OTT.TabName='OverTime' AND OTT.FldName='OverTimeType'");
        sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTTxnID='" + _OTTxnID + "') A");
        sb.Append(" WHERE 1=1 AND A.OTCompID = '" + _OTCompID + "' AND A.OTEmpID = '" + _EmpID + "'");
        sb.Append(" AND A.OTDate='" + _OTStartDate + "~" + _OTEndDate + "'");
        sb.Append(" AND A.OTTime='" + _OTStartTime + "~" + _OTEndTime + "'");
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

        btnAttachDownload.ucAttachDB = _DBName;
        btnAttachDownload.ucAttachID = dt.Rows[0]["OTAttachment"].ToString();

        if (dt.Rows[0]["FileName"].ToString() == "")
        {
            btnAttachDownload.Visible = false;
        }
        else
        {
            lblAttachName.Text = dt.Rows[0]["FileName"].ToString();
        }
        lblCompName.Text = dt.Rows[0]["CompName"].ToString();
        lblDeptName.Text = dt.Rows[0]["DeptName"].ToString();
        lblOrganName.Text = dt.Rows[0]["OrganName"].ToString();
        lblEmpName.Text = dt.Rows[0]["OTEmpID"].ToString() + "　" + dt.Rows[0]["NameN"].ToString();
        lblOTRegisterName.Text = dt.Rows[0]["OTRegisterID"].ToString() + "　" + dt.Rows[0]["RegisterNameN"].ToString();
        lblOTDateValue.Text = _OTStartDate;
        lblOTDateValueEnd.Text = _OTEndDate;
        lblBeginTime.Text = _OTStartTime.Substring(0, 2) + "：" + _OTStartTime.Substring(2, 2);
        lblEndTime.Text = _OTEndTime.Substring(0, 2) + "：" + _OTEndTime.Substring(2, 2); 
        chkMealFlag.Checked = (dt.Rows[0]["MealFlag"].ToString() == "1") ? true : false;
        lblMealTime.Text = dt.Rows[0]["MealTime"].ToString();
        lblOTTotalTime.Text = Aattendant.GetNumString(dt.Rows[0]["OTTotalTime"].ToString(), 1);
        lblOTTypeID.Text = dt.Rows[0]["CodeCName"].ToString();
        lblSalaryOrAdjust.Text = dt.Rows[0]["SalaryOrAdjustName"].ToString();
        if (lblSalaryOrAdjust.Text == "轉補休")
        {
            lblAdjustInvalidDate.Visible = true;
            txtAdjustInvalidDate.Visible = true;
        }
        txtAdjustInvalidDate.Text = Convert.ToDateTime(dt.Rows[0]["AdjustInvalidDate"].ToString()).ToString("yyyy/MM/dd");
        lblOTReasonMemo.Text = dt.Rows[0]["OTReasonMemo"].ToString();
        lblLastChgID.Text = dt.Rows[0]["LastChgID"].ToString();
        lblLastChgName.Text = dt.Rows[0]["LastChgNameN"].ToString();
        lblLastChgDate.Text = Convert.ToDateTime(dt.Rows[0]["LastChgDate"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
        string otseq = dt.Rows[0]["OTSeq"].ToString();
        string ottxnid = dt.Rows[0]["OTTxnID"].ToString();
        SignData();
        
        if (_OTStartDate == _OTEndDate)
        {
            cntTotal = (Convert.ToDouble(_OTEndTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2))) - (Convert.ToDouble(_OTStartTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTStartTime.Substring(2, 2)));
            #region "計算時段"
            string returnPeriodCount = "";
            bool bOTTimeStart = !string.IsNullOrEmpty(_OTStartTime);
            bool bOTTimeEnd = !string.IsNullOrEmpty(_OTEndTime);

            if (bOTTimeStart && bOTTimeEnd)
            {
                trTwo.Visible = false;
                int iOTTimeStart = 0;
                int iOTTimeEnd = 0;
                if (int.TryParse(_OTStartTime, out iOTTimeStart) && int.TryParse(_OTEndTime, out iOTTimeEnd))
                {
                    string mealFlag = chkMealFlag.Checked ? "1" : "0";
                    string sMealTime = string.IsNullOrEmpty(lblMealTime.Text) ? "0" : lblMealTime.Text.Trim();
                    int iMealTime = 0;
                    int.TryParse(sMealTime, out iMealTime);

                    bool bPeriodCount = at.PeriodCount("OverTimeAdvance", _EmpID, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                        _OTStartDate, 0, 0, "1900/01/01", iMealTime, mealFlag, ottxnid, out returnPeriodCount);

                    if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                    {
                        var sReturnPeriodList = returnPeriodCount.Split(';');

                        for (var i = 0; i < sReturnPeriodList.Length; i++)
                        {
                            var datas = sReturnPeriodList[i];
                            lblPeriod.Visible = true;
                            tbTime.Visible = true;
                            if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                            {
                                trOne.Visible = true;
                                if (datas.Split(',').Length >= 1)
                                {
                                    lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                }
                                if (datas.Split(',').Length >= 2)
                                {
                                    lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                }
                                if (datas.Split(',').Length >= 3)
                                {
                                    lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                }
                                if (datas.Split(',').Length >= 4)
                                {
                                    lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                }
                            }

                            if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                            {
                                trTwo.Visible = true;
                                if (datas.Split(',').Length >= 1)
                                {
                                    lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                }
                                if (datas.Split(',').Length >= 2)
                                {
                                    lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                }
                                if (datas.Split(',').Length >= 3)
                                {
                                    lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                }
                                if (datas.Split(',').Length >= 4)
                                {
                                    lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                }
                            }
                        }
                        txtTotalDescription.Text = (lblMealTime.Text != "" && Convert.ToDouble(lblMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + lblMealTime.Text + "分鐘)" : "";
                        txtTotalDescription.Visible = true;
                        string meal = (chkMealFlag.Checked == false) ? "0" : lblMealTime.Text;
                        lblOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                    }
                }
            }
            #endregion "計算時段"
        }
        else
        {
            cntStart = (23 - (Convert.ToDouble(_OTStartTime.Substring(0, 2)))) * 60 + (60 - Convert.ToDouble(_OTStartTime.Substring(2, 2)));
            cntEnd = (Convert.ToDouble(_OTEndTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2)));
            #region "計算時段"
            string returnPeriodCount = "";
            bool bOTTimeStart = !string.IsNullOrEmpty(_OTStartTime);
            bool bOTTimeEnd = !string.IsNullOrEmpty(_OTEndTime);

            if (bOTTimeStart && bOTTimeEnd)
            {
                trTwo.Visible = false;
                int iOTTimeStart = 0;
                int iOTTimeEnd = 0;
                if (int.TryParse(_OTStartTime, out iOTTimeStart) && int.TryParse(_OTEndTime, out iOTTimeEnd))
                {
                    string mealFlag = chkMealFlag.Checked ? "1" : "0";
                    string sMealTime = string.IsNullOrEmpty(lblMealTime.Text) ? "0" : lblMealTime.Text.Trim();
                    int iMealTime = 0;
                    int.TryParse(sMealTime, out iMealTime);

                    bool bPeriodCount = at.PeriodCount("OverTimeAdvance", _EmpID, cntStart, cntEnd, iOTTimeStart, 2359,
                        _OTStartDate, 0, iOTTimeEnd, _OTEndDate, iMealTime, mealFlag, ottxnid, out returnPeriodCount);

                    if (bPeriodCount && !string.IsNullOrEmpty(returnPeriodCount) && returnPeriodCount.Split(';').Length > 0)
                    {
                        var sReturnPeriodList = returnPeriodCount.Split(';');

                        for (var i = 0; i < sReturnPeriodList.Length; i++)
                        {
                            var datas = sReturnPeriodList[i];
                            lblPeriod.Visible = true;
                            tbTime.Visible = true;
                            if (i == 0 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                            {
                                trOne.Visible = true;
                                if (datas.Split(',').Length >= 1)
                                {
                                    lblDateOne.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                }
                                if (datas.Split(',').Length >= 2)
                                {
                                    lblDateOne_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                }
                                if (datas.Split(',').Length >= 3)
                                {
                                    lblDateOne_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                }
                                if (datas.Split(',').Length >= 4)
                                {
                                    lblDateOne_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                }
                            }

                            if (i == 1 && datas.Split(',').Length > 0 && datas.Split(',')[0] != "1900/01/01")
                            {
                                trTwo.Visible = true;
                                if (datas.Split(',').Length >= 1)
                                {
                                    lblDateTwo.Text = datas.Split(',')[0] == "1900/01/01" ? "-" : datas.Split(',')[0];
                                }
                                if (datas.Split(',').Length >= 2)
                                {
                                    lblDateTwo_0.Text = datas.Split(',')[1] == "0.0" ? "-" : datas.Split(',')[1];
                                }
                                if (datas.Split(',').Length >= 3)
                                {
                                    lblDateTwo_1.Text = datas.Split(',')[2] == "0.0" ? "-" : datas.Split(',')[2];
                                }
                                if (datas.Split(',').Length >= 4)
                                {
                                    lblDateTwo_2.Text = datas.Split(',')[3] == "0.0" ? "-" : datas.Split(',')[3];
                                }
                            }
                        }
                        txtTotalDescription.Text = (lblMealTime.Text != "" && Convert.ToDouble(lblMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + lblMealTime.Text + "分鐘)" : "";
                        txtTotalDescription.Visible = true;
                        string meal = (chkMealFlag.Checked == false) ? "0" : lblMealTime.Text;
                        lblOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                    }
                }
            }
            #endregion "計算時段"
        }
    }
    protected void SignData()
    {
        DbHelper db = new DbHelper(_DBName);
        CommandHelper sb = db.CreateCommandHelper();
        //以月計算時數
        //sb.Append("SELECT A.Submit,A.Approval,(A.Submit+A.Approval) AS Total,A.Reject FROM (");
        ////四捨五入取到第二位再做四捨五入(取一位會無條件捨去)
        //sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Submit, ");
        //sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END)  AS Approval, ");
        //sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Reject ");
        //sb.Append(" FROM OverTimeAdvance ");
        //sb.Append(" WHERE OTCompID = '" + _OTCompID+ "' AND OTEmpID = '" + _EmpID + "'");
        //sb.Append(" AND YEAR(OTStartDate) = YEAR('" + _OTStartDate + "') ");
        //sb.Append(" AND MONTH(OTStartDate) = MONTH('" + _OTStartDate + "') ");
        //sb.Append(" AND OTEmpID = '" + _EmpID + "') A");
        //以單計算時數
        sb.Append("SELECT ISNULL(Convert(Decimal(10,1),SUM(A.Submit)),0) AS Submit,ISNULL(Convert(Decimal(10,1),SUM(A.Approval)),0) AS Approval,ISNULL(Convert(Decimal(10,1),SUM(A.Reject)),0) AS Reject FROM (");
        sb.Append(" SELECT ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '2' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Submit,");
        sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '3' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Approval,");
        sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '4' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Reject");
        sb.Append(" FROM OverTimeAdvance");
        sb.Append(" WHERE OTCompID = '" + _OTCompID + "'");
        sb.Append(" AND MONTH(OTStartDate) = MONTH('" + _OTStartDate + "') ");
        sb.Append(" AND YEAR(OTStartDate) = YEAR('" + _OTStartDate + "') ");
        sb.Append(" AND OTEmpID = '" + _EmpID + "'");
        sb.Append(" GROUP BY OTTxnID");
        sb.Append(" ) A");

        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        txtOTDateMonth.Text = (_OTStartDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? _OTStartDate.Substring(5, 2).Substring(1, 1).ToString() : _OTStartDate.Substring(5, 2);
        if (dt.Rows.Count > 0)
        {
            //lblSubmit.Text = (dt.Rows[0]["Submit"].ToString() == "0.00" || dt.Rows[0]["Submit"].ToString() == "") ? "0.0" : (Math.Round(Convert.ToDouble(dt.Rows[0]["Submit"]), 1)).ToString("0.0");//本月送簽總時數
            //lblApproval.Text = (dt.Rows[0]["Approval"].ToString() == "0.00" || dt.Rows[0]["Approval"].ToString() == "") ? "0.0" : (Math.Round(Convert.ToDouble(dt.Rows[0]["Approval"]), 1)).ToString("0.0");//本月核准總時數
            //lblReject.Text = (dt.Rows[0]["Reject"].ToString() == "0.00" || dt.Rows[0]["Reject"].ToString() == "") ? "0.0" : (Math.Round(Convert.ToDouble(dt.Rows[0]["Reject"]), 1)).ToString("0.0");//本月駁回總時數
            lblSubmit.Text = dt.Rows[0]["Submit"].ToString();//本月送簽總時數
            lblApproval.Text = dt.Rows[0]["Approval"].ToString();//本月核准總時數
            lblReject.Text = dt.Rows[0]["Reject"].ToString();//本月駁回總時數
        }
    }
}