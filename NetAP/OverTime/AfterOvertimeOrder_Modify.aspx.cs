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
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

public partial class OverTime_AfterOvertimeOrder_Modify : BasePage
{
    #region "全域變數"
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");// "AattendantDB";
    private Aattendant at = new Aattendant();
    public string _AttachID
    {
        get
        {
            if (ViewState["_AttachID"] == null)
            {
                ViewState["_AttachID"] = "";
            }
            return (string)ViewState["_AttachID"];
        }
        set
        {
            ViewState["_AttachID"] = value;
        }

    }
    public string _rankID
    {
        get
        {
            if (ViewState["_rankID"] == null)
            {
                ViewState["_rankID"] = "";
            }
            return (string)ViewState["_rankID"];
        }
        set
        {
            ViewState["_rankID"] = value;
        }

    }
    public string _EmpDate
    {
        get
        {
            if (ViewState["_EmpDate"] == null)
            {
                ViewState["_EmpDate"] = "";
            }
            return (string)ViewState["_EmpDate"];
        }
        set
        {
            ViewState["_EmpDate"] = value;
        }
    }
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
    public string _OTTxnID
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
    public string _OTSeq
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
    public string _Sex
    {
        get
        {
            if (ViewState["_Sex"] == null)
            {
                ViewState["_Sex"] = "";
            }
            return (string)ViewState["_Sex"];
        }
        set
        {
            ViewState["_Sex"] = value;
        }

    }
    public string _WorkSiteID
    {
        get
        {
            if (ViewState["_WorkSiteID"] == null)
            {
                ViewState["_WorkSiteID"] = "";
            }
            return (string)ViewState["_WorkSiteID"];
        }
        set
        {
            ViewState["_WorkSiteID"] = value;
        }
    }
    public string _OTFromAdvanceTxnId
    {
        get
        {
            if (ViewState["_OTFromAdvanceTxnId"] == null)
            {
                ViewState["_OTFromAdvanceTxnId"] = "";
            }
            return (string)ViewState["_OTFromAdvanceTxnId"];
        }
        set
        {
            ViewState["_OTFromAdvanceTxnId"] = value;
        }

    }
    public string _strAttachID
    {
        get
        {
            if (ViewState["_strAttachID"] == null)
            {
                ViewState["_strAttachID"] = "";
            }
            return (string)ViewState["_strAttachID"];
        }
        set
        {
            ViewState["_strAttachID"] = value;
        }

    }
    public string _OTFormNO
    {
        get
        {
            if (ViewState["_OTFormNO"] == null)
            {
                ViewState["_OTFormNO"] = "";
            }
            return (string)ViewState["_OTFormNO"];
        }
        set
        {
            ViewState["_OTFormNO"] = value;
        }
    }
    public DataTable _dtPara
    {
        get
        {
            if (ViewState["_dtPara"] == null)
            {
                ViewState["_dtPara"] = null;
            }
            return (DataTable)(ViewState["_dtPara"]);
        }
        set
        {
            ViewState["_dtPara"] = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _OTCompID = (Request["OTCompID"] != null) ? Request["OTCompID"].ToString() : "";
            _EmpID = (Request["OTEmpID"] != null) ? Request["OTEmpID"].ToString() : "";
            _OTStartDate = (Request["OTStartDate"] != null) ? Request["OTStartDate"].ToString() : "";
            _OTEndDate = (Request["OTEndDate"] != null) ? Request["OTEndDate"].ToString() : "";
            _OTStartTime = (Request["OTStartTime"] != null) ? Request["OTStartTime"].ToString() : "";
            _OTEndTime = (Request["OTEndTime"] != null) ? Request["OTEndTime"].ToString() : "";
            _OTSeq = (Request["OTSeq"] != null) ? Request["OTSeq"].ToString() : "";
            _OTFromAdvanceTxnId = (Request["OTFromAdvanceTxnId"] != null) ? Request["OTFromAdvanceTxnId"].ToString() : "";
            _OTFormNO = (Request["OTFormNO"] != null) ? Request["OTFormNO"].ToString() : "";

            /// <summary>
            /// //-------2017/04/20-進行修改後要同步更新OTTxnID
            /// 利用ViewState紀錄進行修改後的最新OTTxnID值，只要Commit成功則把ViewState和_OTTxnID更新成新的OTTxnID值
            /// </summary>
            //_OTTxnID = (Request["OTTxnID"] != null) ? Request["OTTxnID"].ToString() : "";
            _OTTxnID = (ViewState["_OTTxnID"] != null && ViewState["_OTTxnID"].ToString().Trim() != string.Empty) ? ViewState["_OTTxnID"].ToString() : Request["OTTxnID"] != null ? Request["OTTxnID"].ToString() : string.Empty;
            //--------------------------------

            LoadData();
            //加班類型
            DataTable dtType = at.QueryData("Code,CodeCName", "AT_CodeMap", " AND TabName='OverTime' AND FldName='OverTimeType'");
            ddlOTTypeID.DataSource = dtType;
            ddlOTTypeID.DataValueField = "Code";
            ddlOTTypeID.DataTextField = "CodeCName";
            ddlOTTypeID.DataBind();
            ddlOTTypeID.Items.Insert(0, new ListItem("　- -請選擇- -", "0"));
        }
        TextBox txtStartDate = (TextBox)ucDateStart.FindControl("txtDate");
        txtStartDate.AutoPostBack = true;
        txtStartDate.CausesValidation = true;
        txtStartDate.TextChanged += StartDate_TextChanged;

        TextBox txtEndDate = (TextBox)ucDateEnd.FindControl("txtDate");
        txtEndDate.AutoPostBack = true;
        txtEndDate.CausesValidation = true;
        txtEndDate.TextChanged += EndDate_TextChanged;

        //加班開始時間
        DropDownList ddlStart = (DropDownList)OTTimeStart.FindControl("ddlHH");
        ddlStart.AutoPostBack = true;
        ddlStart.CausesValidation = false;
        ddlStart.SelectedIndexChanged += StartTime_SelectedIndexChanged;

        ddlStart = (DropDownList)OTTimeStart.FindControl("ddlMM");
        ddlStart.AutoPostBack = true;
        ddlStart.CausesValidation = false;
        ddlStart.SelectedIndexChanged += StartTime_SelectedIndexChanged;

        //加班結束時間
        DropDownList ddlEnd = (DropDownList)OTTimeEnd.FindControl("ddlHH");
        ddlEnd.AutoPostBack = true;
        ddlEnd.CausesValidation = false;
        ddlEnd.SelectedIndexChanged += EndTime_SelectedIndexChanged;

        ddlEnd = (DropDownList)OTTimeEnd.FindControl("ddlMM");
        ddlEnd.AutoPostBack = true;
        ddlEnd.CausesValidation = false;
        ddlEnd.SelectedIndexChanged += EndTime_SelectedIndexChanged;

        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopAttach_onClose);
    }
    protected void subGetData()
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        _dtPara = at.Json2DataTable(at.QueryColumn("Para", "OverTimePara", " AND CompID = '" + _OTCompID + "'"));
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (_dtPara.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(ddlSalaryOrAdjust.SelectedValue) || ddlSalaryOrAdjust.SelectedValue == "0")
                {
                    ddlSalaryOrAdjust.SelectedValue = _dtPara.Rows[0]["SalaryOrAjust"].ToString();
                }
            }
            else
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
        }
        sb.Append(" SELECT * FROM (");
        sb.Append(" SELECT OT.OTEmpID,OT.OTCompID,C.CompName,OT.DeptID,OT.DeptName,OT.OrganID,OT.OrganName,OT.OTFormNO,P.NameN,P.RankID,P.EmpDate,P.Sex,P.WorkSiteID,OT.OTRegisterID,PR.NameN AS RegisterNameN,OT.OTTxnID,OTT.CodeCName,ISNULL(AI.FileName,'') AS FileName,OT.MealFlag,isnull(OT.MealTime,0)+isnull(OT2.MealTime,0) AS MealTime, OT.OTSeq,");
        sb.Append(" OT.OTStatus,CASE OT.SalaryOrAdjust WHEN '1' THEN '轉薪資' WHEN '2' THEN '轉補休' END AS SalaryOrAdjustName,OT.OTAttachment,OT.OTTypeID, ");
        sb.Append(" (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,");
        sb.Append(" (OT.OTStartTime+'~'+isnull(OT2.OTEndTime,OT.OTEndTime)) AS OTTime,OT.AdjustInvalidDate,OT.SalaryOrAdjust,");
        //sb.Append(" Convert(Decimal(10,1),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))) AS OTTotalTime,");
        sb.Append(" Convert(Decimal(10,1),ROUND(Convert(Decimal(10,2),((CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))/CAST(60 AS FLOAT))+((CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))/CAST(60 AS FLOAT))),1)) AS OTTotalTime,");
        sb.Append(" OT.LastChgID,PL.NameN AS LastChgNameN,OT.LastChgDate,OT.OTReasonMemo");
        sb.Append(" FROM OverTimeDeclaration OT ");
        sb.Append(" LEFT JOIN OverTimeDeclaration OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2 AND OT2.OverTimeFlag='1'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Company] C ON C.CompID=OT.OTCompID AND C.InValidFlag = '0' And C.NotShowFlag = '0'");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.CompID = OT.OTCompID AND P.EmpID=OT.OTEmpID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PR ON PR.CompID = OT.OTRegisterComp AND PR.EmpID=OT.OTRegisterID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] PL ON PL.CompID = OT.OTCompID AND PL.EmpID=OT.LastChgID");
        sb.Append(" LEFT JOIN AttachInfo AI ON AI.AttachID IS NOT NULL AND AI.AttachID <> '' AND AI.AttachID = OT.OTAttachment  AND FileSize > 0");
        sb.Append(" LEFT JOIN AT_CodeMap AS OTT ON OT.OTTypeID = OTT.Code AND OTT.TabName='OverTime' AND OTT.FldName='OverTimeType'");
        sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OverTimeFlag='1' AND OT.OTTxnID='" + _OTTxnID + "') A");
        sb.Append(" WHERE 1=1 AND A.OTCompID='" + _OTCompID + "' AND A.OTEmpID = '" + _EmpID + "'");
        sb.Append(" AND A.OTDate='" + _OTStartDate + "~" + _OTEndDate + "'");
        sb.Append(" AND A.OTTime='" + _OTStartTime + "~" + _OTEndTime + "'");

        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        txtOTCompName.Text = dt.Rows[0]["CompName"].ToString();
        txtDeptName.Text = dt.Rows[0]["DeptName"].ToString();
        txtOrganName.Text = dt.Rows[0]["OrganName"].ToString();
        lblOTEmpID.Text = _EmpID;// +"　" + dt.Rows[0]["NameN"].ToString();
        lblOTEmpName.Text = dt.Rows[0]["NameN"].ToString();
        txtOTRegisterName.Text = dt.Rows[0]["OTRegisterID"].ToString() + "　" + dt.Rows[0]["RegisterNameN"].ToString();
        ucDateStart.ucSelectedDate = _OTStartDate;
        ucDateEnd.ucSelectedDate = _OTEndDate;
        OTTimeStart.ucDefaultSelectedHH = _OTStartTime.Substring(0, 2);
        OTTimeStart.ucDefaultSelectedMM = _OTStartTime.Substring(2, 2);
        OTTimeEnd.ucDefaultSelectedHH = _OTEndTime.Substring(0, 2);
        OTTimeEnd.ucDefaultSelectedMM = _OTEndTime.Substring(2, 2);
        chkMealFlag.Checked = (dt.Rows[0]["MealFlag"].ToString() == "1") ? true : false;
        chkMealFlag_CheckedChanged(null, null);
        txtMealTime.Text = dt.Rows[0]["MealTime"].ToString();
        txtOTTotalTime.Text = Convert.ToDouble(dt.Rows[0]["OTTotalTime"].ToString()).ToString("0.0");
        ddlOTTypeID.SelectedValue = dt.Rows[0]["OTTypeID"].ToString();
        txtOTReasonMemo.ucTextData = dt.Rows[0]["OTReasonMemo"].ToString();
        //_OTTxnID = dt.Rows[0]["OTTxnID"].ToString();

        ddlSalaryOrAdjust.SelectedValue = dt.Rows[0]["SalaryOrAdjust"].ToString();
        //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
        _rankID = Aattendant.GetRankIDFormMapping(_OTCompID, dt.Rows[0]["RankID"].ToString());  //dt.Rows[0]["RankID"].ToString();
        _EmpDate = dt.Rows[0]["EmpDate"].ToString();
        _Sex = dt.Rows[0]["Sex"].ToString();
        _WorkSiteID = dt.Rows[0]["WorkSiteID"].ToString();
        //ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
        ddlSalaryOrAdjust_SelectedIndexChanged(null, null);

        SignData();
        if (dt.Rows[0]["FileName"].ToString() != "")
        {
            lblAttachName.Text = "附件檔名：" + dt.Rows[0]["FileName"].ToString();
        }

        //附件Attach
        string strAttachID = "";
        string strAttachAdminURL;
        string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
        string strAttachDownloadURL;
        string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
        //附件編號
        _AttachID = dt.Rows[0]["OTAttachment"].ToString().Trim();
        if (string.IsNullOrEmpty(_AttachID))
        {
            _AttachID = "test" + UserInfo.getUserInfo().UserID + Guid.NewGuid();
        }
        strAttachID = _AttachID;
        ViewState["attch"] = _AttachID;
        strAttachAdminURL = string.Format(strAttachAdminBaseURL, _overtimeDBName, strAttachID, "1", "3072", "3072", "");
        strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, _overtimeDBName, strAttachID);
        frameAttach.Value = strAttachAdminURL;

        if (_Sex != "" && _Sex == "2")
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                //從10點開始
                if (Convert.ToInt32(_OTStartTime.Substring(0, 2)) == 22)
                {
                    lblStartSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                else if (Convert.ToInt32(_OTStartTime.Substring(0, 2)) > 22)
                {
                    lblStartSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                //從凌晨開始到六點
                else if (Convert.ToInt32(_OTStartTime.Substring(0, 2)) >= 0 && Convert.ToInt32(_OTStartTime.Substring(0, 2)) < 6)
                {
                    lblStartSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                else
                {
                    lblStartSex.Visible = false;
                }
                //從10點開始
                if (Convert.ToInt32(_OTEndTime.Substring(0, 2)) == 22)
                {
                    lblEndSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                else if (Convert.ToInt32(_OTEndTime.Substring(0, 2)) > 22)
                {
                    lblEndSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                //從凌晨開始到六點
                else if (Convert.ToInt32(_OTEndTime.Substring(0, 2)) >= 0 && Convert.ToInt32(_OTEndTime.Substring(0, 2)) < 6)
                {
                    lblEndSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                else
                {
                    lblEndSex.Visible = false;
                }
            }
        }
        else
        {
            lblStartSex.Visible = false;
            lblEndSex.Visible = false;
        }
    }

    protected void LoadData()
    {
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;

        subGetData();
        string sOTTimeStart = _OTStartTime;
        string sOTTimeEnd = _OTEndTime;

        if (_OTStartDate == _OTEndDate)
        {
            cntTotal = (Convert.ToDouble(_OTEndTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2))) - (Convert.ToDouble(_OTStartTime.Substring(0, 2)) * 60 + Convert.ToDouble(_OTStartTime.Substring(2, 2)));
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                #region "計算時段"
                string returnPeriodCount = "";
                trTwo.Visible = false;

                int iOTTimeStart = 0;
                int iOTTimeEnd = 0;
                if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                {
                    string mealFlag = chkMealFlag.Checked ? "1" : "0";
                    string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                    int iMealTime = 0;
                    int.TryParse(sMealTime, out iMealTime);

                    bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                        ucDateStart.ucSelectedDate, 0, 0, "1900/01/01", iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                        txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                        txtTotalDescription.Visible = true;
                        string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                        meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                        txtOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                    }
                    else
                    {
                        Util.MsgBox(returnPeriodCount);
                        initalData();
                        return;
                    }
                }
                #endregion "計算時段"
            }
        }
        else
        {
            cntStart = (23 - (Convert.ToDouble(_OTStartTime.Substring(0, 2)))) * 60 + (60 - Convert.ToDouble(_OTStartTime.Substring(2, 2)));
            cntEnd = (Convert.ToDouble(_OTEndTime.Substring(0, 2))) * 60 + Convert.ToDouble(_OTEndTime.Substring(2, 2));
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                #region "計算時段"
                string returnPeriodCount = "";
                trTwo.Visible = false;
                int iOTTimeStart = 0;
                int iOTTimeEnd = 0;
                if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                {
                    string mealFlag = chkMealFlag.Checked ? "1" : "0";
                    string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                    int iMealTime = 0;
                    int.TryParse(sMealTime, out iMealTime);

                    bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntStart, cntEnd, iOTTimeStart, 2359,
                        ucDateStart.ucSelectedDate, 0, iOTTimeEnd, ucDateEnd.ucSelectedDate, iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                        txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                        txtTotalDescription.Visible = true;
                        string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                        meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                        txtOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                    }
                    else
                    {
                        Util.MsgBox(returnPeriodCount);
                        initalData();
                        return;
                    }
                }
                #endregion "計算時段"
            }
        }
    }
    protected void initalData()
    {
        OTTimeStart.ucDefaultSelectedHH = "請選擇";
        OTTimeStart.ucDefaultSelectedMM = "請選擇";
        OTTimeEnd.ucDefaultSelectedHH = "請選擇";
        OTTimeEnd.ucDefaultSelectedMM = "請選擇";
        txtMealTime.Text = "0";
        txtOTTotalTime.Text = "";
        txtTotalDescription.Visible = false;
        tbTime.Visible = false;
        lblPeriod.Visible = false;
        txtOTTotalTimeHour.Text = "小時";
        lblStartSex.Visible = false;
        lblEndSex.Visible = false;
    }
    protected void StartDate_TextChanged(object sender, EventArgs e) //開始日期
    {
        initalData();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate))
            {
                ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                {
                    //加班申請範圍
                    TimeSpan totalBefore = (DateTime.Now.Date).Subtract(Convert.ToDateTime(ucDateStart.ucSelectedDate));
                    if (DateTime.Now > Convert.ToDateTime(ucDateStart.ucSelectedDate))
                    {
                        if (Convert.ToInt32(totalBefore.Days) > Convert.ToInt32(_dtPara.Rows[0]["DeclarationBegin"].ToString()))
                        {
                            Util.MsgBox("加班申報不可早於前" + _dtPara.Rows[0]["DeclarationBegin"].ToString() + "日");
                            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                            ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                            return;
                        }
                    }
                    else //if (DateTime.Now < Convert.ToDateTime(ucDateStart.ucSelectedDate))
                    {
                        Util.MsgBox("加班日期不可以選擇未來日期");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        //檢查到職日以前不可以加
                        if (Convert.ToDateTime(ucDateStart.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                        {
                            Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                            ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                            return;
                        }
                        else if (Convert.ToDateTime(ucDateEnd.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                        {
                            Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                            ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                            ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                            return;
                        }
                        else
                        {
                            SignData();
                        }
                    }
                }
                //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
                ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
                ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
            }
        }
    }
    protected void EndDate_TextChanged(object sender, EventArgs e) //結束日期
    {
        initalData();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return;
        }
        else
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                TimeSpan total = (Convert.ToDateTime(ucDateEnd.ucSelectedDate)).Subtract(Convert.ToDateTime(ucDateStart.ucSelectedDate));
                TimeSpan totalBefore = (DateTime.Now.Date).Subtract(Convert.ToDateTime(ucDateStart.ucSelectedDate));
                if (DateTime.Now > Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    if (Convert.ToInt32(totalBefore.Days) > Convert.ToInt32(_dtPara.Rows[0]["DeclarationBegin"].ToString()))
                    {
                        Util.MsgBox("加班申報不可早於前" + _dtPara.Rows[0]["DeclarationBegin"].ToString() + "日");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                }
                else //if (DateTime.Now < Convert.ToDateTime(ucDateEnd.ucSelectedDate))
                {
                    Util.MsgBox("加班日期不可以選擇未來日期");
                    ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                    ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                    return;
                }
                if (Convert.ToDateTime(ucDateEnd.ucSelectedDate) < Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    Util.MsgBox("加班結束日期不得小於加班開始日期");
                    ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                    ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                    return;
                }
                else if (Convert.ToInt32(total.Days) > 1)
                {
                    Util.MsgBox("加班日期間隔不得大於1日");
                    ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                    ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                    return;
                }
                if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                {
                    //檢查到職日以前不可以加
                    if (Convert.ToDateTime(ucDateStart.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                    {
                        Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                    else if (Convert.ToDateTime(ucDateEnd.ucSelectedDate) < Convert.ToDateTime(_EmpDate))
                    {
                        Util.MsgBox("到職日(" + Convert.ToDateTime(_EmpDate).ToString("yyyy/MM/dd") + ")以前無法申請");
                        ucDateStart.ucSelectedDate = DateTime.Now.ToString("yyyy/MM/dd");
                        ucDateEnd.ucSelectedDate = ucDateStart.ucSelectedDate;
                        return;
                    }
                    else
                    {
                        SignData();
                    }
                }
            }
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
            {
                lblDateTwo.Text = "";
                lblDateTwo_0.Text = "";
                lblDateTwo_1.Text = "";
                lblDateTwo_2.Text = "";
            }
            //依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
            ddlSalaryOrAdjustChange(_rankID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate);
            ddlSalaryOrAdjust_SelectedIndexChanged(null, null);
        }
    }

    protected void StartTime_SelectedIndexChanged(object sender, EventArgs e) //開始時間
    {
        if (_Sex != "" && _Sex == "2")
        {
            if (OTTimeStart.ucDefaultSelectedHH != "請選擇")
            {
                if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) == 22) //OTTimeStart.ucDefaultSelectedMM != "01")
                {
                    lblStartSex.Visible = true;
                }
                else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > 22)
                {
                    lblStartSex.Visible = true;
                }
                //從凌晨開始到六點
                else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) >= 0 && Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) < 6)
                {
                    lblStartSex.Visible = true;
                }
                else
                {
                    lblStartSex.Visible = false;
                }
            }
        }
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇")
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate))
            {
                if (DateTime.Now.Date == Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > Convert.ToInt32(DateTime.Now.Hour))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        OTTimeStart.ucDefaultSelectedMM = "請選擇";
                        OTTimeStart.ucDefaultSelectedHH = "請選擇";
                        tbTime.Visible = false;
                        lblPeriod.Visible = false;
                        txtOTTotalTime.Text = "";
                        txtTotalDescription.Visible = false;
                        return;
                    }
                    else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) == Convert.ToInt32(DateTime.Now.Hour) && Convert.ToInt32(OTTimeStart.ucDefaultSelectedMM) > Convert.ToInt32(DateTime.Now.Minute))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        OTTimeStart.ucDefaultSelectedMM = "請選擇";
                        OTTimeStart.ucDefaultSelectedHH = "請選擇";
                        tbTime.Visible = false;
                        lblPeriod.Visible = false;
                        txtOTTotalTime.Text = "";
                        txtTotalDescription.Visible = false;
                        return;
                    }
                }
            }
        }
        if (OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇")
        {
            EndTime_SelectedIndexChanged(null, null);
        }

    }
    protected void EndTime_SelectedIndexChanged(object sender, EventArgs e) //結束時間
    {
        string strcheckMealFlag = (chkMealFlag.Checked == true) ? "1" : "0";
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            if (_Sex != "" && _Sex == "2")
            {
                if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) == 22)
                {
                    if (OTTimeEnd.ucDefaultSelectedMM != "請選擇" && Convert.ToInt32(OTTimeEnd.ucDefaultSelectedMM) >= 1)
                    {
                        lblEndSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                    }
                    else
                    {
                        lblEndSex.Visible = false;
                    }
                }
                else if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) > 22)
                {
                    lblEndSex.Visible = true; //Util.MsgBox("女性不可以10點後加班");
                }
                else if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) >= 0 && Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) < 6)
                {
                    lblEndSex.Visible = true;
                }
                else
                {
                    lblEndSex.Visible = false;
                }
            }
            else
            {
                lblEndSex.Visible = false;
            }
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                if (DateTime.Now.Date == Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) > Convert.ToInt32(DateTime.Now.Hour))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                        OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                        tbTime.Visible = false;
                        lblPeriod.Visible = false;
                        txtOTTotalTime.Text = "";
                        txtTotalDescription.Visible = false;
                        return;
                    }
                    else if (Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) == Convert.ToInt32(DateTime.Now.Hour) && Convert.ToInt32(OTTimeEnd.ucDefaultSelectedMM) > Convert.ToInt32(DateTime.Now.Minute))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                        OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                        tbTime.Visible = false;
                        lblPeriod.Visible = false;
                        txtOTTotalTime.Text = "";
                        txtTotalDescription.Visible = false;
                        return;
                    }
                }
                if (OTTimeEnd.ucDefaultSelectedHH == "00" & OTTimeEnd.ucDefaultSelectedMM == "00")
                {
                    Util.MsgBox("最大時間上限為23:59");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }
                if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH))
                {
                    Util.MsgBox("結束時間小於開始時間");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }
                if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) >= Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH) && Convert.ToInt32(OTTimeStart.ucDefaultSelectedMM) > Convert.ToInt32(OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("結束時間小於開始時間");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }

                getCntTotal(out cntTotal);

                if (cntTotal <= 0)
                {
                    Util.MsgBox("加班時間不可以低於一小時");
                    OTTimeEnd.ucDefaultSelectedMM = "請選擇";
                    OTTimeEnd.ucDefaultSelectedHH = "請選擇";
                    lblEndSex.Visible = false;
                    return;
                }
                if (_EmpID != "")
                {
                    if (cntTotal > 120) //加班超過兩小時需扣用餐時間60分鐘
                    {
                        EtxtMealTimeChecked(null, null, true);
                    }
                    else
                    {
                        EtxtMealTimeChecked(null, null, false);
                    }
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                                    ucDateStart.ucSelectedDate, 0, 0, "1900/01/01", iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }

                }
            }
            else //跨日
            {
                if (_EmpID != "")
                {
                    getCntStartAndCntEnd(out cntStart, out cntEnd);
                    if (cntEnd + cntStart > 120) //加班超過兩小時需扣用餐時間60分鐘
                    {
                        EtxtMealTimeChecked(null, null, true);
                    }
                    else
                    {
                        EtxtMealTimeChecked(null, null, false);
                    }
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntStart, cntEnd, iOTTimeStart, 2359,
                                    ucDateStart.ucSelectedDate, 0, iOTTimeEnd, ucDateEnd.ucSelectedDate, iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }

                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cntStart"></param>
    /// <param name="cntEnd"></param>
    private void getCntStartAndCntEnd(out double cntStart, out double cntEnd)
    {
        cntStart = 0;
        cntEnd = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            cntStart = (23 - (Convert.ToDouble(OTTimeStart.ucDefaultSelectedHH))) * 60 + (60 - Convert.ToDouble(OTTimeStart.ucDefaultSelectedMM));
            cntEnd = (Convert.ToDouble(OTTimeEnd.ucDefaultSelectedHH)) * 60 + Convert.ToDouble(OTTimeEnd.ucDefaultSelectedMM);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cntTotal"></param>
    private void getCntTotal(out double cntTotal)
    {
        cntTotal = 0;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "")
        {
            cntTotal = (Convert.ToDouble(OTTimeEnd.ucDefaultSelectedHH) * 60 + Convert.ToDouble(OTTimeEnd.ucDefaultSelectedMM)) - (Convert.ToDouble(OTTimeStart.ucDefaultSelectedHH) * 60 + Convert.ToDouble(OTTimeStart.ucDefaultSelectedMM));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="isChecked"></param>
    protected void EtxtMealTimeChecked(object sender, EventArgs e, bool isChecked)
    {
        chkMealFlag.Checked = isChecked;
        if (isChecked)
        {
            txtMealTime.Enabled = true;
            //txtMealTime.Text = "60";
            string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            if (!string.IsNullOrEmpty(strHo))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return;
                }
                else
                {
                    if (strHo == "0")
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeN"].ToString();
                    }
                    else
                    {
                        txtMealTime.Text = _dtPara.Rows[0]["MealTimeH"].ToString();
                    }
                }
            }
            else
            {
                txtMealTime.Text = "60";
            }
        }
        else
        {
            txtMealTime.Text = "0";
            txtMealTime.Enabled = false;
        }
    }
    protected void chkMealFlag_CheckedChanged(object sender, EventArgs e)
    {
        if (!chkMealFlag.Checked)
        {
            txtMealTime.Text = "0";
            txtMealTime.Enabled = false;
        }
        else
        {
            double cntTotal = 0;
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                getCntTotal(out cntTotal);
            }
            else
            {
                double cntStart = 0;
                double cntEnd = 0;
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                cntTotal = cntStart + cntEnd;
            }

            if (cntTotal > 120) //加班超過兩小時需扣用餐時間60分鐘
            {
                txtMealTime.Enabled = true;
                string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                if (!string.IsNullOrEmpty(strHo))
                {
                    if (_dtPara == null)
                    {
                        Util.MsgBox("請聯絡HR確認是否有設定參數值");
                        return;
                    }
                    else
                    {
                        if (strHo == "0")
                        {
                            txtMealTime.Text = _dtPara.Rows[0]["MealTimeN"].ToString();
                        }
                        else
                        {
                            txtMealTime.Text = _dtPara.Rows[0]["MealTimeH"].ToString();
                        }
                    }
                }
                else
                {
                    txtMealTime.Text = "60";
                }
            }
            else
            {
                txtMealTime.Text = "0";
                txtMealTime.Enabled = true;
            }
        }
        txtMealTime_TextChanged(null, null);
    }
    protected void txtMealTime_TextChanged(object sender, EventArgs e)
    {
        double cntStart;
        double cntEnd;
        double cntTotal;
        if (OTTimeStart.ucDefaultSelectedHH != "請選擇" && OTTimeStart.ucDefaultSelectedMM != "請選擇" && OTTimeEnd.ucDefaultSelectedHH != "請選擇" && OTTimeEnd.ucDefaultSelectedMM != "請選擇" && OTTimeStart.ucDefaultSelectedHH != "" && OTTimeStart.ucDefaultSelectedMM != "" && OTTimeEnd.ucDefaultSelectedHH != "" && OTTimeEnd.ucDefaultSelectedMM != "" && _EmpID != "")
        {
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                if (OTTimeEnd.ucDefaultSelectedHH == "00" & OTTimeEnd.ucDefaultSelectedMM == "00")
                {
                    Util.MsgBox("最大時間上限為23:59");
                    initalData();
                    return;
                }
                getCntTotal(out cntTotal);
                if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntTotal))
                {
                    txtMealTime.Focus();
                    Util.MsgBox("用餐時數超過加班時數");
                }
                else
                {
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntTotal, 0, iOTTimeStart, iOTTimeEnd,
                                    ucDateStart.ucSelectedDate, 0, 0, "1900/01/01", iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntTotal - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
            else //跨日
            {
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntEnd + cntStart))
                {
                    txtMealTime.Focus();
                    Util.MsgBox("用餐時數超過加班時數");
                }
                else
                {
                    if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
                    {
                        #region "計算時段"
                        string returnPeriodCount = "";
                        bool bOTTimeStart = !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedHH) && OTTimeStart.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeStart.ucDefaultSelectedMM) && OTTimeStart.ucDefaultSelectedMM != "請選擇";
                        bool bOTTimeEnd = !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedHH) && OTTimeEnd.ucDefaultSelectedHH != "請選擇"
                            && !string.IsNullOrEmpty(OTTimeEnd.ucDefaultSelectedMM) && OTTimeEnd.ucDefaultSelectedMM != "請選擇";

                        if (bOTTimeStart && bOTTimeEnd)
                        {
                            trTwo.Visible = false;
                            string sOTTimeStart = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                            string sOTTimeEnd = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                            int iOTTimeStart = 0;
                            int iOTTimeEnd = 0;
                            if (int.TryParse(sOTTimeStart, out iOTTimeStart) && int.TryParse(sOTTimeEnd, out iOTTimeEnd))
                            {
                                string mealFlag = chkMealFlag.Checked ? "1" : "0";
                                string sMealTime = string.IsNullOrEmpty(txtMealTime.Text) ? "0" : txtMealTime.Text.Trim();
                                int iMealTime = 0;
                                int.TryParse(sMealTime, out iMealTime);

                                bool bPeriodCount = at.PeriodCount("OverTimeDeclaration", _EmpID, cntStart, cntEnd, iOTTimeStart, 2359,
                                    ucDateStart.ucSelectedDate, 0, iOTTimeEnd, ucDateEnd.ucSelectedDate, iMealTime, mealFlag, _OTTxnID, out returnPeriodCount);

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
                                    txtTotalDescription.Text = (txtMealTime.Text != "" && Convert.ToDouble(txtMealTime.Text) > 0 && chkMealFlag.Checked == true) ? "(已扣除用餐時數" + txtMealTime.Text + "分鐘)" : "";
                                    txtTotalDescription.Visible = true;
                                    string meal = (chkMealFlag.Checked == false) ? "0" : txtMealTime.Text;
                                    meal = (string.IsNullOrEmpty(meal)) ? "0" : txtMealTime.Text;
                                    txtOTTotalTime.Text = Convert.ToDouble((cntEnd + cntStart - Convert.ToDouble(meal)) / 60).ToString("0.0");
                                }
                                else
                                {
                                    Util.MsgBox(returnPeriodCount);
                                    initalData();
                                    return;
                                }
                            }
                        }
                        #endregion "計算時段"
                    }
                }
            }
        }

    }
    /// <summary>
    /// 依照RankID階級與加班起迄日來控制 加班轉換方式的下拉選項
    /// </summary>
    /// <remarks>
    /// RankID大於等於19 : 只能轉補休
    /// RankID小於19且兩天皆為假日 : 可轉補休或轉薪資
    /// RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
    /// </remarks>
    /// <param name="sRankID">string</param>
    /// <param name="startDate">string</param>
    /// <param name="endDate">string</param>
    private void ddlSalaryOrAdjustChange(string sRankID, string startDate, string endDate)
    {
        if (!string.IsNullOrEmpty(sRankID) && !string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
        {
            bool bRankIDisNumeric = at.IsNumeric(sRankID);
            decimal dRankID = Aattendant.GetDecimal(sRankID);       //將加班人的RankID轉為Decimal
            bool bRankIDMapisNumeric = at.IsNumeric(sRankID);      //判斷參數設定的轉補休職等設定有設定(也就是參數必需是數字!)
            bool datesIsDifferent = at.CompareDate(startDate, endDate) != 0 ? true : false;     //判斷加班起訖日是否是同一天,false為同一天,true為不同天
            bool dateStartIsHoliday = at.CheckHolidayOrNot(startDate);
            bool dateEndIsHoliday = at.CheckHolidayOrNot(endDate);

            //先回到預設值並Enable
            ddlSalaryOrAdjust.Enabled = true;
            ddlSalaryOrAdjust.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(_dtPara.Rows[0]["AdjustRankID"].ToString()))
            {
                //取得轉補休職等設定之職等數值
                var dRankIDMap = Aattendant.GetDecimal(Aattendant.GetRankIDFormMapping(_OTCompID, _dtPara.Rows[0]["AdjustRankID"].ToString()));

                //如果加班人職等大於等於參數設定的職等，僅能選擇轉補休
                if (dRankID >= dRankIDMap)
                {
                    if (ddlSalaryOrAdjust.Items.Count >= 1)
                    {
                        ddlSalaryOrAdjust.Items[0].Selected = false;
                    }
                    if (ddlSalaryOrAdjust.Items.Count >= 2)
                    {
                        ddlSalaryOrAdjust.Items[1].Enabled = false;
                        ddlSalaryOrAdjust.Items[1].Selected = false;
                    }
                    if (ddlSalaryOrAdjust.Items.Count >= 3)
                    {
                        ddlSalaryOrAdjust.Items[2].Enabled = true;
                        ddlSalaryOrAdjust.Items[2].Selected = true;
                    }
                }
                //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資，反之則看參數設定
                else if (dRankID < dRankIDMap)
                {
                    //是跨日單
                    if (datesIsDifferent)
                    {
                        //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資
                        if (dateStartIsHoliday && dateEndIsHoliday)
                        {
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                //ddlSalaryOrAdjust.Items[0].Selected = true;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                //ddlSalaryOrAdjust.Items[1].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                //ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //假日預設選項為轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                        }
                        else
                        { //RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = false;
                                ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //重新再指向預設選項
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"));
                        }
                    }
                    else    //是單日單
                    {
                        if (dateStartIsHoliday)     //加班日為假日
                        {
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                //ddlSalaryOrAdjust.Items[1].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                //ddlSalaryOrAdjust.Items[2].Selected = false;
                            }
                            //假日預設選項為轉補休
                            ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                        }
                        else        //加班日為平日
                        {
                            //RankID小於19且加班日為平日 : 看參數設定且要將選項disable
                            if (ddlSalaryOrAdjust.Items.Count >= 1)
                            {
                                ddlSalaryOrAdjust.Items[0].Selected = false;
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 2)
                            {
                                if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                                {
                                    ddlSalaryOrAdjust.Items[1].Enabled = true;
                                    ddlSalaryOrAdjust.Items[1].Selected = true;
                                }
                            }
                            if (ddlSalaryOrAdjust.Items.Count >= 3)
                            {
                                if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                                {
                                    ddlSalaryOrAdjust.Items[1].Enabled = true;
                                    ddlSalaryOrAdjust.Items[1].Selected = true;
                                }
                                else
                                {
                                    ddlSalaryOrAdjust.Items[2].Enabled = true;
                                    ddlSalaryOrAdjust.Items[2].Selected = true;
                                }
                            }

                            //將選項鎖住
                            ddlSalaryOrAdjust.Enabled = false;
                        }
                    }
                }
            }
            //當轉補休職等設定為請選擇的時候，必須依據參數檔的加班轉換方式預設值(SalaryOrAjust) 
            else
            {
                //是跨日單
                if (datesIsDifferent)
                {
                    //如果加班人職小於於參數設定的職等且加班起訖日都是假日，可選擇轉補休或轉薪資
                    if (dateStartIsHoliday && dateEndIsHoliday)
                    {
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            //ddlSalaryOrAdjust.Items[0].Selected = true;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            //ddlSalaryOrAdjust.Items[1].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = true;
                            //ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //假日預設選項為轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                    }
                    else
                    { //RankID小於19且除了兩天皆為假日以外 : 只能轉薪資
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            ddlSalaryOrAdjust.Items[1].Selected = true;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = false;
                            ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //重新再指向預設選項
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉薪資"));
                    }
                }
                else    //是單日單
                {
                    if (dateStartIsHoliday)     //加班日為假日
                    {
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            ddlSalaryOrAdjust.Items[1].Enabled = true;
                            //ddlSalaryOrAdjust.Items[1].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            ddlSalaryOrAdjust.Items[2].Enabled = true;
                            //ddlSalaryOrAdjust.Items[2].Selected = false;
                        }
                        //假日預設選項為轉補休
                        ddlSalaryOrAdjust.SelectedIndex = ddlSalaryOrAdjust.Items.IndexOf(ddlSalaryOrAdjust.Items.FindByText("轉補休"));
                    }
                    else        //加班日為平日
                    {
                        //RankID小於19且加班日為平日 : 看參數設定且要將選項disable
                        if (ddlSalaryOrAdjust.Items.Count >= 1)
                        {
                            ddlSalaryOrAdjust.Items[0].Selected = false;
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 2)
                        {
                            if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                        }
                        if (ddlSalaryOrAdjust.Items.Count >= 3)
                        {
                            if (_dtPara.Rows[0]["SalaryOrAjust"].ToString() == "1")
                            {
                                ddlSalaryOrAdjust.Items[1].Enabled = true;
                                ddlSalaryOrAdjust.Items[1].Selected = true;
                            }
                            else
                            {
                                ddlSalaryOrAdjust.Items[2].Enabled = true;
                                ddlSalaryOrAdjust.Items[2].Selected = true;
                            }
                        }

                        //將選項鎖住
                        ddlSalaryOrAdjust.Enabled = false;
                    }
                }
            }
        }
    }
    public void SignData()//本月的總時數
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        //以月計算時數
        //sb.Append("SELECT A.Submit,A.Approval,(A.Submit+A.Approval) AS Total,A.Reject FROM (");
        //sb.Append(" SELECT SUM(CASE OTStatus WHEN '2' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Submit, ");
        //sb.Append(" SUM(CASE OTStatus WHEN '3' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END)  AS Approval, ");
        //sb.Append(" SUM(CASE OTStatus WHEN '4' THEN (CASE MealFlag WHEN 1 THEN Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)-CAST(MealTime AS FLOAT))/CAST(60 AS FLOAT)) ELSE Convert(Decimal(10,2),(CAST(OTTotalTime AS FLOAT)/CAST(60 AS FLOAT))) END) ELSE '0.0' END) AS Reject ");
        //sb.Append(" FROM OverTimeDeclaration ");
        //sb.Append(" WHERE OTCompID = '" + _OTCompID + "' AND OTEmpID = '" + _EmpID + "'");
        //sb.Append(" AND MONTH(OTStartDate) = MONTH('" + ucDateStart.ucSelectedDate + "') ");
        //sb.Append(" AND YEAR(OTStartDate) = YEAR('" + ucDateStart.ucSelectedDate + "') ");
        //sb.Append(" AND OTEmpID = '" + _EmpID + "') A");
        //以單計算時數
        sb.Append("SELECT ISNULL(Convert(Decimal(10,1),SUM(A.Submit)),0) AS Submit,ISNULL(Convert(Decimal(10,1),SUM(A.Approval)),0) AS Approval,ISNULL(Convert(Decimal(10,1),SUM(A.Reject)),0) AS Reject FROM (");
        sb.Append(" SELECT ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '2' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Submit,");
        sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '3' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Approval,");
        sb.Append("        ROUND(Convert(Decimal(10,2),SUM(CASE OTStatus WHEN '4' THEN ISNULL(OTTotalTime,0)-ISNULL(MealTime,0) ELSE 0 END))/60,1)  AS Reject");
        sb.Append(" FROM OverTimeDeclaration");
        sb.Append(" WHERE OTCompID = '" + _OTCompID + "'");
        sb.Append(" AND MONTH(OTStartDate) = MONTH('" + ucDateStart.ucSelectedDate + "') ");
        sb.Append(" AND YEAR(OTStartDate) = YEAR('" + ucDateStart.ucSelectedDate + "') ");
        sb.Append(" AND OTEmpID = '" + _EmpID + "'");
        sb.Append(" GROUP BY OTTxnID");
        sb.Append(" ) A");

        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        txtOTDateMonth.Text = (ucDateStart.ucSelectedDate.Substring(5, 2).Substring(0, 1).ToString() == "0") ? ucDateStart.ucSelectedDate.Substring(5, 2).Substring(1, 1).ToString() : ucDateStart.ucSelectedDate.Substring(5, 2).ToString();
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
    protected void ddlSalaryOrAdjust_SelectedIndexChanged(object sender, EventArgs e)//轉補休：顯示補修失效日
    {
        if (ddlSalaryOrAdjust.SelectedValue == "2")
        {
            if (_dtPara == null)
            {
                Util.MsgBox("請聯絡HR確認是否有設定參數值");
                return;
            }
            else
            {
                lblAdjustInvalidDate.Visible = true;
                txtAdjustInvalidDate.Visible = true;
                //DataTable dtPara = at.QueryData("AdjustInvalidDate", "OverTimePara", " AND CompID = '" + UserInfo.getUserInfo().CompID + "'");
                txtAdjustInvalidDate.Text = Convert.ToDateTime(_dtPara.Rows[0]["AdjustInvalidDate"].ToString()).ToString("yyyy/MM/dd");
            }
        }
        else
        {
            lblAdjustInvalidDate.Visible = false;
            txtAdjustInvalidDate.Visible = false;
        }
    }

    public bool checkData(string flag)
    {
        if (flag == "2")
        {
            if (Aattendant.DateCheck(ucDateStart.ucSelectedDate) && Aattendant.DateCheck(ucDateEnd.ucSelectedDate))
            {
                if (DateTime.Now < Convert.ToDateTime(ucDateStart.ucSelectedDate) || DateTime.Now < Convert.ToDateTime(ucDateEnd.ucSelectedDate))
                {
                    Util.MsgBox("加班日期不可以選擇未來日期");
                    return false;
                }
                else if (DateTime.Now.Date == Convert.ToDateTime(ucDateStart.ucSelectedDate))
                {
                    if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) > Convert.ToInt32(DateTime.Now.Hour))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        return false;
                    }
                    else if (Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH) == Convert.ToInt32(DateTime.Now.Hour) && Convert.ToInt32(OTTimeStart.ucDefaultSelectedMM) > Convert.ToInt32(DateTime.Now.Minute))
                    {
                        Util.MsgBox("不可以申請未來時間點");
                        return false;
                    }
                }
            }
        }
        if (ddlSalaryOrAdjust.SelectedValue == "0")
        {
            Util.MsgBox("需選擇加班轉換方式");
            return false;
        }
        if (ucDateStart.ucSelectedDate == "")
        {
            Util.MsgBox("您必須輸入加班開始日期");
            return false;
        }
        if (ucDateEnd.ucSelectedDate == "")
        {
            Util.MsgBox("您必須輸入加班結束日期");
            return false;
        }

        if (OTTimeStart.ucDefaultSelectedHH == "請選擇" || OTTimeStart.ucDefaultSelectedMM == "請選擇")
        {
            Util.MsgBox("您必須輸入加班開始時間");
            return false;
        }
        if (OTTimeEnd.ucDefaultSelectedHH == "請選擇" || OTTimeEnd.ucDefaultSelectedMM == "請選擇")
        {
            Util.MsgBox("您必須輸入加班結束時間");
            return false;
        }
        if (ddlOTTypeID.SelectedValue == "0")
        {
            Util.MsgBox("需選擇加班類型");
            return false;
        }
        if (txtOTReasonMemo.ucTextData == "")
        {
            Util.MsgBox("需填寫加班原因");
            return false;
        }
        if (chkMealFlag.Checked)
        {
            if (txtMealTime.Text == "" || Convert.ToDouble(txtMealTime.Text) <= 0)
            {
                Util.MsgBox("您必須填寫用餐時間");
                return false;
            }
        }
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = null;
        //用餐時數大於加班時間
        double cntTotal = 0.0;
        double cntStart = 0.0;
        double cntEnd = 0.0;
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
        {
            getCntTotal(out cntTotal);
            if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntTotal))
            {
                Util.MsgBox("用餐時數超過加班時數");
                return false;
            }
        }
        else
        {
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            cntTotal = cntStart + cntEnd;
            if (txtMealTime.Text != "" && Convert.ToInt32(txtMealTime.Text) >= (cntEnd + cntStart))
            {
                Util.MsgBox("用餐時數超過加班時數");
                return false;
            }
        }

        if (!Aattendant.IsNationalHoliday(ucDateStart.ucSelectedDate)) //檢查是否為國定假日
        {
            //檢查連續加班如果開始日期為假日
            if (at.CheckHolidayOrNot(ucDateStart.ucSelectedDate))
            {
                DateTime a = Convert.ToDateTime(ucDateStart.ucSelectedDate).AddDays(-1);//檢查前一天是否有存在資料庫
                DateTime b = Convert.ToDateTime(ucDateStart.ucSelectedDate).AddDays(1);//檢查後一天是否有存在資料庫
                //檢查事後申報是否連續加班
                if (at.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                else if (at.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeDeclaration", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                //檢查事先申請是否連續加班
                if (at.CheckHolidayOrNot(a.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + a.ToString("yyyy/MM/dd") + "' AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
                else if (at.CheckHolidayOrNot(b.ToString("yyyy/MM/dd")))
                {
                    dt = at.QueryData("*", "OverTimeAdvance", " AND OTStartDate='" + b.ToString("yyyy/MM/dd") + "' AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('1','2','3')");
                    if (dt.Rows.Count > 0)
                    {
                        Util.MsgBox("不能假日連續加班");
                        return false;
                    }
                }
            }
        }
        //if (flag == "2")
        //{
        //檢查加班時數(含已核准)申請時數是否超過上限
        string message = "";
        ViewState["message"] = message;
        if (!checkOverTimeIsOver(out message))
        {
            if (_dtPara.Rows[0]["DayLimitFlag"].ToString() == "1")
            {
                Util.MsgBox(message);
                return false;
            }
            else
            {
                ViewState["message"] = message;
            }
        }
        //檢查每個月的上限
        if ((ucDateStart.ucSelectedDate).ToString().Substring(5, 2) == ucDateEnd.ucSelectedDate.ToString().Substring(5, 2))
        {
            if (!at.checkMonthTime("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntTotal, Convert.ToDouble(txtMealTime.Text), cntStart, cntEnd, _OTFromAdvanceTxnId))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    //Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox(message);
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }
        }
        else
        {
            string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text));
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            if (!at.checkMonthTime("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate, ucDateStart.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntStart, Convert.ToDouble(mealOver.Split(',')[1]), cntStart, 0, _OTFromAdvanceTxnId))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    //Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox(message);
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }

            if (!at.checkMonthTime("OverTimeDeclaration", _OTCompID, _EmpID, ucDateEnd.ucSelectedDate, ucDateEnd.ucSelectedDate, Convert.ToDouble(_dtPara.Rows[0]["MonthLimitHour"].ToString()), cntEnd, Convert.ToDouble(mealOver.Split(',')[3]), cntEnd, 0, _OTFromAdvanceTxnId))
            {
                if (_dtPara == null)
                {
                    Util.MsgBox("請聯絡HR確認是否有設定參數值");
                    return false;
                }
                else
                {
                    //Util.MsgBox("每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時");
                    message = "每月上限加班申請時數為" + _dtPara.Rows[0]["MonthLimitHour"] + "小時";
                    if (_dtPara.Rows[0]["MonthLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox(message);
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                }
            }
        }

        //}
        int cnt = 0;
        //檢查連續上班日
        if (_dtPara.Rows[0]["OTMustCheck"].ToString() == "0")
        {
            int OTLimitDay = Convert.ToInt32(_dtPara.Rows[0]["OTLimitDay"].ToString());
            sb.Reset();
            sb.Append("SELECT Convert(varchar,C.SysDate,111) as SysDate,ISNULL(O.OTStartDate,'') AS OTStartDate,C.Week,C.HolidayOrNot FROM (");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeAdvance WHERE  OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')");
            sb.Append(" AND OTTxnID NOT IN('" + _OTFromAdvanceTxnId + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION");
            sb.Append(" SELECT DISTINCT OTStartDate FROM OverTimeDeclaration WHERE  OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTStartDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  OTStartDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')) O");
            sb.Append(" FULL OUTER JOIN(");
            sb.Append(" SELECT * FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] WHERE  CompID='" + _OTCompID + "' AND SysDate>= DATEADD(DAY,-" + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "') AND  SysDate<= DATEADD(DAY," + (OTLimitDay - 1).ToString() + ",'" + ucDateStart.ucSelectedDate + "')) C ON O.OTStartDate=C.SysDate");
            sb.Append(" ORDER BY C.SysDate ASC");

            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SysDate"].ToString() == ucDateStart.ucSelectedDate || dt.Rows[i]["SysDate"].ToString() == ucDateEnd.ucSelectedDate)//本單
                {
                    cnt += 1;
                }
                else
                {
                    if (dt.Rows[i]["HolidayOrNot"].ToString() == "0")
                    {
                        cnt += 1;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[i]["OTStartDate"].ToString()))
                        {
                            cnt += 1;
                        }
                        else
                        {
                            cnt = 0;
                        }
                    }
                }
                if (cnt >= OTLimitDay)
                {
                    //Util.MsgBox("不得連續上班超過" + OTLimitDay.ToString() + "天");
                    message = "不得連續上班超過" + OTLimitDay.ToString() + "天";
                    if (_dtPara.Rows[0]["OTLimitFlag"].ToString() == "1")
                    {
                        Util.MsgBox(message);
                        return false;
                    }
                    else
                    {
                        ViewState["message"] = message;
                    }
                    break;
                }
            }
        }


        //畫面上的時間
        int starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
        int endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);

        //檢核時間重疊(OverTime_BK)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime FROM OverTime_BK  WHERE CompID = '" + _OTCompID + "' AND EmpID='" + _EmpID + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND Convert(varchar,OTDate,111) ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND Convert(varchar,OTDate,111) IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }
                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("該時段已存在人力資源-加班系統，<br/>請由人力資源-加班進行查詢");
                        return false;
                    }
                }
            }
        }

        //檢核時間重疊(NaturalDisasterByCity)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime FROM NaturalDisasterByCity  WHERE WorkSiteID='" + _WorkSiteID + "' AND CompID='" + _OTCompID + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND DisasterStartDate ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND DisasterStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("留守時段重複");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
            }
        }

        //檢核時間重疊(NaturalDisasterByCity)
        sb.Reset();
        sb.Append(" SELECT BeginTime,EndTime,LEFT(BeginTime,2) AS StartTimeHr,RIGHT(BeginTime,2) AS StartTimeM,LEFT(EndTime,2) AS EndTimeHr,RIGHT(EndTime,2) AS EndTimeM FROM NaturalDisasterByEmp  WHERE EmpID='" + _EmpID + "' AND CompID='" + _OTCompID + "' ");
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Append(" AND DisasterStartDate ='" + ucDateStart.ucSelectedDate + "'");
        }
        else
        {
            sb.Append(" AND DisasterStartDate IN ('" + ucDateStart.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "')  ");
        }
        dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //起迄時間都有重疊
                if ((dt.Rows[i]["BeginTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["EndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                {
                    Util.MsgBox("留守時段重複");
                    return false;
                }
                //開始時間小於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) > starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間等於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) == starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
                //開始時間大於資料庫開始時間
                if (Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) < starttime)
                {
                    //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                    if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                    else if (endtime > Convert.ToInt32(dt.Rows[i]["BeginTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()))
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["EndTime"].ToString()) > starttime)
                    {
                        Util.MsgBox("留守時段重複");
                        return false;
                    }
                }
            }
        }

        //檢核時間重疊(事後申報)
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Reset();
            sb.Reset();
            sb.Append(" SELECT OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration  WHERE OTStatus in ('1','2','3') AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "') ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //起迄時間都有重疊 
                    if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["OTEndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                    {
                        Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                        return false;
                    }
                    //開始時間小於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間等於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間大於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        ////結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        //else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        //{
                        //    lblMsg.Text = "您欲申請的加班時間區間已有紀錄10";
                        //    return;
                        //}
                        else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            sb.Reset();
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
            sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "')");
            sb.Append(" UNION");
            sb.Append(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucDateEnd.ucSelectedDate + "'");
            sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTTxnID + "')");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["OTStartDate"].ToString() == ucDateStart.ucSelectedDate)//起日
                    {
                        //起迄日重疊
                        endtime = 2359;
                        starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
                        string strTxnID = at.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "' AND OTSeqNo='2'");
                        if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && strTxnID == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                    else//迄日
                    {
                        starttime = 0000;
                        endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        //檢核時間重疊(預先申請)
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
        {
            sb.Reset();
            sb.Append(" SELECT OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance  WHERE OTStatus in ('1','2','3') AND OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "' ");
            //sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ");
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTStatus in ('1','2','3')) ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
            endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //起迄時間都有重疊 
                    if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && (dt.Rows[i]["OTEndTime"].ToString() == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM))
                    {
                        Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                        return false;
                    }
                    //開始時間小於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間等於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                    //開始時間大於資料庫開始時間
                    if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                    {
                        //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                        if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                        else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                    }
                }
            }
        }
        else
        {
            sb.Reset();
            sb.Append("SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
            //sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ");
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTStatus in ('1','2','3')) ");
            sb.Append(" UNION");
            sb.Append(" SELECT OTTxnID,OTStartDate,OTStartTime,OTEndTime,LEFT(OTStartTime,2) AS StartTimeHr,RIGHT(OTStartTime,2) AS StartTimeM,LEFT(OTEndTime,2) AS EndTimeHr,RIGHT(OTEndTime,2) AS EndTimeM FROM OverTimeAdvance WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus IN ('1','2','3') AND OTStartDate='" + ucDateEnd.ucSelectedDate + "'");
            //sb.Append(" AND NOT(OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTTxnID='" + _OTFromAdvanceTxnId + "') ");
            sb.Append(" AND OTTxnID NOT IN (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateEnd.ucSelectedDate + "' AND OTStatus in ('1','2','3')) ");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["OTStartDate"].ToString() == ucDateStart.ucSelectedDate)//起日
                    {
                        //起迄日重疊
                        endtime = 2359;
                        starttime = Convert.ToInt32(OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM);
                        string strTxnID = at.QueryColumn("OTEndTime", "OverTimeDeclaration", " AND OTTxnID='" + dt.Rows[i]["OTTxnID"] + "' AND OTSeqNo='2'");
                        if ((dt.Rows[i]["OTStartTime"].ToString() == OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM) && strTxnID == OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM)
                        {
                            Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                            return false;
                        }
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                    else//迄日
                    {
                        starttime = 0000;
                        endtime = Convert.ToInt32(OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM);
                        //開始時間小於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) > starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間等於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) == starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，大於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime > Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                        //開始時間大於資料庫開始時間
                        if (Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) < starttime)
                        {
                            //結束時間大於資料庫的開始時間，小於資料庫的結束時間
                            if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime < Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            //結束時間大於資料庫的開始時間，等於資料庫的結束時間
                            else if (endtime > Convert.ToInt32(dt.Rows[i]["OTStartTime"].ToString()) && endtime == Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()))
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                            else if (Convert.ToInt32(dt.Rows[i]["OTEndTime"].ToString()) > starttime)
                            {
                                Util.MsgBox("您欲申請的加班時間區間已有紀錄");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    private bool checkOverTimeIsOver(out string message)
    {
        bool result = true;
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        double dayNLimit = 0;
        double dayHLimit = 0;
        double hr = 0;
        double iMealTime = 0;
        double.TryParse(txtMealTime.Text, out iMealTime);
        message = "";
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        if (_dtPara == null)
        {
            Util.MsgBox("請聯絡HR確認是否有設定參數值");
            return false;
        }
        else
        {
            dayNLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourN"].ToString());//平日可申請
            dayHLimit = Convert.ToDouble(_dtPara.Rows[0]["DayLimitHourH"].ToString());//假日可申請
        }
        if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
        {
            DataTable dt = null;
            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "') A ");

            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                hr = double.Parse(dt.Rows[0]["TotalTime"].ToString());
            }
            getCntTotal(out cntTotal);
            cntTotal -= iMealTime;
            bool blHo = at.CheckHolidayOrNot(ucDateStart.ucSelectedDate);
            if (!blHo)//平日檢查
            {
                if (cntTotal + hr > (dayNLimit * 60))
                {
                    message = "加班時數(含已核准)申請時數已超過上限" + _dtPara.Rows[0]["DayLimitHourN"].ToString() + "小時";
                    result = false;
                }
            }
            else//假日
            {
                if (cntTotal + hr > (dayHLimit * 60))
                {
                    message = "加班時數(含已核准)申請時數已超過上限" + _dtPara.Rows[0]["DayLimitHourH"].ToString() + "小時";
                    result = false;
                }
            }
        }
        else
        {
            getCntStartAndCntEnd(out cntStart, out cntEnd);
            //資料庫的加班總時數
            DataTable dtStart = null;
            DataTable dtEnd = null;
            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateStart.ucSelectedDate + "'");
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateStart.ucSelectedDate + "' AND OTEndDate='" + ucDateStart.ucSelectedDate + "') A ");
            dtStart = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            sb.Reset();
            sb.Append(" SELECT ISNULL(SUM(A.OTTotalTime),0) AS TotalTime FROM(");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeAdvance WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateEnd.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
            sb.Append(" AND OTTxnID NOT IN ('" + _OTFromAdvanceTxnId + "')");
            sb.Append(" AND OTTxnID NOT IN");
            sb.Append(" (SELECT OTFromAdvanceTxnId FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTEmpID='" + _EmpID + "' AND OTStatus in ('2','3') AND OTFromAdvanceTxnId<>'')");
            sb.Append(" UNION ALL");
            sb.Append(" SELECT SUM(OTTotalTime)-SUM(MealTime) AS OTTotalTime FROM OverTimeDeclaration WHERE OTCompID='" + _OTCompID + "' AND OTStatus in('2','3') AND OTEmpID='" + _EmpID + "' AND OTStartDate='" + ucDateEnd.ucSelectedDate + "' AND OTEndDate='" + ucDateEnd.ucSelectedDate + "') A ");
            dtEnd = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];

            double hrStart = 0;
            double hrEnd = 0;
            if (dtStart.Rows.Count > 0)
            {
                hrStart = double.Parse(dtStart.Rows[0]["TotalTime"].ToString());
            }
            if (dtEnd.Rows.Count > 0)
            {
                hrEnd = double.Parse(dtEnd.Rows[0]["TotalTime"].ToString());
            }
            //國定假日若在平日是可以加班的，不算在連續加班
            string strStart = at.QueryColumn("Week", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
            string strEnd = at.QueryColumn("Week", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");

            bool blStartHo = at.CheckHolidayOrNot(ucDateStart.ucSelectedDate);
            bool blEndHo = at.CheckHolidayOrNot(ucDateEnd.ucSelectedDate);
            string mealOver = at.MealJudge(cntStart, iMealTime);

            if (blStartHo == false && blEndHo == false)//平日跨平日
            {
                if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayNLimit * 60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayNLimit * 60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
            else if (blStartHo == true && blEndHo == true)//假日跨假日
            {
                if (Aattendant.IsNationalHoliday(ucDateStart.ucSelectedDate))//國定假日跨假日不算連續加班
                {
                    if (strStart != "6" || strStart != "7")
                    {
                        if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit * 60))
                        {
                            message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit * 60))
                        {
                            message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        //result = true;
                    }
                    else
                    {
                        message = "不能假日連續加班";
                        result = false;
                    }
                }
                else if (Aattendant.IsNationalHoliday(ucDateEnd.ucSelectedDate))//假日跨國定假日不算連續加班
                {
                    if (strStart != "6" || strStart != "7")
                    {
                        if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit * 60))
                        {
                            message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit * 60))
                        {
                            message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                            result = false;
                        }
                        //result = true;
                    }
                    else
                    {
                        message = "不能假日連續加班";
                        result = false;
                    }
                }
                else
                {
                    message = "不能假日連續加班";
                    result = false;
                }
            }
            else if (blStartHo == false && blEndHo == true)//平日跨假日
            {
                if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayNLimit * 60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayHLimit * 60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
            else if (blStartHo == true && blEndHo == false)//假日跨平日
            {
                if (hrStart + (cntStart - Convert.ToDouble(mealOver.Split(',')[1])) > (dayHLimit * 60))
                {
                    message = ucDateStart.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
                if (hrEnd + (cntEnd - Convert.ToDouble(mealOver.Split(',')[3])) > (dayNLimit * 60))
                {
                    message = ucDateEnd.ucSelectedDate + "(含已核准)申請時數已超過上限";
                    result = false;
                }
            }
        }
        return result;
    }

    //暫存按鈕
    protected void btnTempSave_Click(object sender, EventArgs e)
    {
        if (checkData("1"))
        {
            ShowTempSaveConfirm();
        }
    }
    protected void ShowTempSaveConfirm()
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave();", true);
        string msg = "";
        if (string.IsNullOrEmpty(ViewState["message"].ToString()))
        {
            msg = "是否要暫存？";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave('" + msg + "');", true);
        }
        else
        {
            msg = ViewState["message"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave_Reminder('" + msg + "');", true);
        }
    }
    protected void btnTempSaveInvisible_Reminder_Click(object sender, EventArgs e)
    {
        //SaveData("1");
        string msg = "是否要暫存？";
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoTempSave('" + msg + "');", true);
    }
    protected void btnTempSaveInvisible_Click(object sender, EventArgs e)
    {
        SaveData("1");
    }

    //送簽按鈕
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (checkData("2"))//為了檢查日期是否為未來日期
        {
            ShowSubmitConfirm();
        }
    }
    protected void ShowSubmitConfirm()
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit();", true);
        //ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
        //btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
        string msg = "";
        if (string.IsNullOrEmpty(ViewState["message"].ToString()))
        {
            msg = "是否要送簽？";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit('" + msg + "');", true);
            //燈箱
            ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
            btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
        }
        else
        {
            msg = ViewState["message"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit_Reminder('" + msg + "');", true);
        }
    }
    protected void btnSubmitInvisible_Reminder_Click(object sender, EventArgs e)
    {
        string msg = "是否要送簽？";
        ClientScript.RegisterStartupScript(this.GetType(), "confirm", "funYesOrNoSubmit('" + msg + "');", true);
        //燈箱
        ucLightBox.ucLightBoxMsg = "送簽中，請稍後";
        btnSubmitInvisible.OnClientClick = ucLightBox.ucShowClientJS;
    }
    protected void btnSubmitInvisible_Click(object sender, EventArgs e)
    {
        SaveData("2");
    }

    public void SaveData(string flag)
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DbConnection cn = db.OpenConnection();
        DbTransaction tx = cn.BeginTransaction();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        string strcheckMealFlag = (chkMealFlag.Checked == true) ? "1" : "0";
        double cntStart = 0;
        double cntEnd = 0;
        double cntTotal = 0;
        getCntStartAndCntEnd(out cntStart, out cntEnd);
        string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime.Text));
        //-------2017/04/20-進行修改後要同步更新OTTxnID
        string strOTTxnID = string.Empty;
        //--------------------------------
        string attach = at.QueryAtt(_AttachID, _OTCompID, _EmpID);
        if (string.IsNullOrEmpty(attach))
        {
            ViewState["attch"] = "test" + UserInfo.getUserInfo().UserID + Guid.NewGuid();
        }
        else
        {
            ViewState["attch"] = attach;
        }
        _AttachID = attach;
        var isSuccess = false;
        var toUserData = new Dictionary<string, string>();
        var empData = new Dictionary<string, string>();
        var flowCode = "";
        var flowSN = "";
        var nextIsLastFlow = false;
        var meassge = "";

        int OTSeq = 0;
        int OTSeq_1 = 0;
        if (flag == "2")
        {
            string strDirectSubmit = DirectSubmit(_OTCompID, ucDateStart.ucSelectedDate, ucDateEnd.ucSelectedDate, _EmpID, OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM, OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM, _OTFromAdvanceTxnId, txtOTTotalTime.Text);
            if (strDirectSubmit == "Y")//判斷是否直接送簽
            {
                flag = "3";
            }
            else
            {
                isSuccess = FlowUtility.QueryFlowDataAndToUserData_First(_OTCompID, "", "", _EmpID, UserInfo.getUserInfo().UserID, ucDateStart.ucSelectedDate, "1",
                out empData, out toUserData, out flowCode, out flowSN, out nextIsLastFlow, out meassge);
                if (!isSuccess)
                {
                    Util.MsgBox(meassge);
                    return;
                }
                if ("".Equals(toUserData["SignID"]))
                {
                    Util.MsgBox("查無審核人員，故無法送簽。");
                    return;
                }
            }
        }

        if (_OTStartDate == _OTEndDate)//原本不跨日
        {
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                getCntTotal(out cntTotal);
                OTSeq = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate);
                sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart.ucSelectedDate + "',OTEndDate='" + ucDateEnd.ucSelectedDate + "',OTStartTime='" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', OTEndTime='" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "',");
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" AdjustInvalidDate='', "); //失效時間
                }
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                strOTTxnID = (_OTCompID + _EmpID + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Append(" OTTxnID='" + strOTTxnID + "', ");
                //--------------------------------
                sb.Append(" OTAttachment='" + attach + "', ");
                sb.Append(" OTTotalTime='" + cntTotal + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + txtMealTime.Text + "',OTTypeID='" + ddlOTTypeID.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
                sb.Append(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserInfo.getUserInfo().CompID + "',LastChgID='" + UserInfo.getUserInfo().UserID + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTStartDate + "'");
                sb.Append(" AND OTEndDate='" + _OTEndDate + "'");
                sb.Append(" AND OTStartTime='" + _OTStartTime + "'");
                sb.Append(" AND OTEndTime='" + _OTEndTime + "'");
                sb.Append(" AND OTStatus='1'");
            }
            else
            {
                string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                OTSeq = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate);
                sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart.ucSelectedDate + "',OTEndDate='" + ucDateStart.ucSelectedDate + "',OTStartTime='" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', OTEndTime='2359',");
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" AdjustInvalidDate='', "); //失效時間
                }
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                strOTTxnID = (_OTCompID + _EmpID + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Append(" OTTxnID='" + strOTTxnID + "', ");
                //--------------------------------
                sb.Append(" OTAttachment='" + attach + "', ");
                sb.Append(" OTTotalTime='" + cntStart + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[1] + "',OTTypeID='" + ddlOTTypeID.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
                sb.Append(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserInfo.getUserInfo().CompID + "',LastChgID='" + UserInfo.getUserInfo().UserID + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTStartDate + "'");
                sb.Append(" AND OTEndDate='" + _OTEndDate + "'");
                sb.Append(" AND OTStartTime='" + _OTStartTime + "'");
                sb.Append(" AND OTEndTime='" + _OTEndTime + "'");
                sb.Append(" AND OTStatus='1';");

                OTSeq_1 = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateEnd.ucSelectedDate);
                sb.Append(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTFromAdvanceTxnId,OTTxnID,OTSeqNo,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate) ");
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                //sb.Append(" SELECT  OTCompID,OTEmpID,'" + ucDateEnd.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "','" + OTSeq_1 + "',OTFromAdvanceTxnId,OTTxnID,'2',DeptID,OrganID,DeptName,OrganName,FlowCaseID,'0000','" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "','" + cntEnd + "',SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,'" + mealOver.Split(',')[3] + "',OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,'" + strHo2 + "',ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate FROM OverTimeDeclaration");
                sb.Append(" SELECT  OTCompID,OTEmpID,'" + ucDateEnd.ucSelectedDate + "','" + ucDateEnd.ucSelectedDate + "','" + OTSeq_1 + "',OTFromAdvanceTxnId,'" + strOTTxnID + "','2',DeptID,OrganID,DeptName,OrganName,FlowCaseID,'0000','" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "','" + cntEnd + "',SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,'" + mealOver.Split(',')[3] + "',OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,'" + strHo2 + "',ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate FROM OverTimeDeclaration");
                //--------------------------------
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
                sb.Append(" AND OTEndDate='" + ucDateStart.ucSelectedDate + "'");
                sb.Append(" AND OTStartTime='" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "'");
                sb.Append(" AND OTEndTime='2359'");
            }
        }
        else //原本是跨日單
        {
            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate) //不跨日
            {
                string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + UserInfo.getUserInfo().CompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                OTSeq = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate);
                getCntTotal(out cntTotal);
                sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart.ucSelectedDate + "',OTEndDate='" + ucDateEnd.ucSelectedDate + "',OTStartTime='" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', OTEndTime='" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "',");
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" AdjustInvalidDate='', "); //失效時間
                }
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                strOTTxnID = (_OTCompID + _EmpID + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Append(" OTTxnID='" + strOTTxnID + "', ");
                //--------------------------------
                sb.Append(" OTAttachment='" + attach + "', ");
                sb.Append(" OTTotalTime='" + cntTotal + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + txtMealTime.Text + "',OTTypeID='" + ddlOTTypeID.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
                sb.Append(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserInfo.getUserInfo().CompID + "',LastChgID='" + UserInfo.getUserInfo().UserID + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTStartDate + "'");
                sb.Append(" AND OTEndDate='" + _OTStartDate + "'");
                sb.Append(" AND OTStartTime='" + _OTStartTime + "'");
                sb.Append(" AND OTEndTime='2359'");
                sb.Append(" AND OTStatus='1'");

                sb.AppendStatement("DELETE FROM OverTimeDeclaration ");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTEndDate + "'");
                sb.Append(" AND OTEndDate='" + _OTEndDate + "'");
                sb.Append(" AND OTStartTime='0000'");
                sb.Append(" AND OTEndTime='" + _OTEndTime + "'");
            }
            else
            {
                string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart.ucSelectedDate + "'");
                string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + _OTCompID + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd.ucSelectedDate + "'");
                getCntStartAndCntEnd(out cntStart, out cntEnd);
                OTSeq = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateStart.ucSelectedDate);
                sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart.ucSelectedDate + "',OTEndDate='" + ucDateStart.ucSelectedDate + "',OTStartTime='" + OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM + "', OTEndTime='2359',");
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" AdjustInvalidDate='', "); //失效時間
                }
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                strOTTxnID = (_OTCompID + _EmpID + Convert.ToDateTime(ucDateStart.ucSelectedDate).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Append(" OTTxnID='" + strOTTxnID + "', ");
                //--------------------------------
                sb.Append(" OTAttachment='" + attach + "', ");
                sb.Append(" OTTotalTime='" + cntStart + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[1] + "',OTTypeID='" + ddlOTTypeID.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
                sb.Append(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserInfo.getUserInfo().CompID + "',LastChgID='" + UserInfo.getUserInfo().UserID + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTStartDate + "'");
                sb.Append(" AND OTEndDate='" + _OTStartDate + "'");
                sb.Append(" AND OTStartTime='" + _OTStartTime + "'");
                sb.Append(" AND OTEndTime='2359'");
                sb.Append(" AND OTStatus='1'");

                OTSeq_1 = at.QuerySeq("OverTimeDeclaration", _OTCompID, _EmpID, ucDateEnd.ucSelectedDate);
                sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateEnd.ucSelectedDate + "',OTEndDate='" + ucDateEnd.ucSelectedDate + "',OTStartTime='0000', OTEndTime='" + OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM + "',");
                sb.Append(" SalaryOrAdjust='" + ddlSalaryOrAdjust.SelectedValue + "',OTSeq='" + OTSeq_1 + "',OTStatus='" + flag + "',");
                if (ddlSalaryOrAdjust.SelectedValue == "2")
                {
                    sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate.Text + "', "); //失效時間
                }
                else
                {
                    sb.Append(" AdjustInvalidDate='', "); //失效時間
                }
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                sb.Append(" OTTxnID='" + strOTTxnID + "', ");
                //--------------------------------
                sb.Append(" OTAttachment='" + attach + "', ");
                sb.Append(" OTTotalTime='" + cntEnd + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[3] + "',OTTypeID='" + ddlOTTypeID.SelectedValue + "',OTReasonMemo='" + (txtOTReasonMemo.ucTextData).Replace("'", "''") + "',");
                sb.Append(" HolidayOrNot='" + strHo2 + "',LastChgComp='" + UserInfo.getUserInfo().CompID + "',LastChgID='" + UserInfo.getUserInfo().UserID + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                sb.Append(" AND OTStartDate='" + _OTEndDate + "'");
                sb.Append(" AND OTEndDate='" + _OTEndDate + "'");
                sb.Append(" AND OTStartTime='0000'");
                sb.Append(" AND OTEndTime='" + _OTEndTime + "'");
                sb.Append(" AND OTStatus='1'");
            }
        }

        if (flag == "1")//暫存
        {
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                //如果Commit成功則將全域的OTTxnID與ViewState更新成最新的OTTxnID
                _OTTxnID = strOTTxnID;
                ViewState["_OTTxnID"] = strOTTxnID;
                //--------------------------------
                _OTStartDate = ucDateStart.ucSelectedDate;
                _OTEndDate = ucDateEnd.ucSelectedDate;
                _OTStartTime = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                _OTEndTime = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;
                _OTSeq = Convert.ToString(OTSeq);
                LoadAttch();
                ViewState["ChangeDateFlag"] = true;
                Util.MsgBox("暫存成功！");
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                tx.Rollback();//資料更新失敗
                //-------2017/04/20-進行修改後要同步更新OTTxnID
                //如果Commit成功則將全域的OTTxnID與ViewState更新成最新的OTTxnID
                //失敗則清空ViewState值
                ViewState["_OTTxnID"] = string.Empty;
                //--------------------------------
                Util.MsgBox("暫存失敗！");
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
        else if (flag == "2")//送簽
        {
            try
            {
                Dictionary<string, string> oAssTo = new Dictionary<string, string>();
                //逐筆送簽
                oAssTo.Clear();
                string name = at.QueryColumn("NameN", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal]", " AND EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "'");
                //需加部門
                string organName = "";
                //if (toUserData["SignLine"] == "1")
                //{
                //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + _OTCompID + "')");
                //}
                //else if (toUserData["SignLine"] == "2")
                //{
                //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow] where OrganID='" + toUserData["SignFlowOrganID"] + "')");
                //}
                //else if (toUserData["SignLine"] == "3")
                //{
                //    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization] where OrganID='" + toUserData["SignOrganID"] + "' AND CompID='" + _OTCompID + "')");
                //}
                if (toUserData["SignLine"] == "1")
                {
                    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                }
                else if (toUserData["SignLine"] == "2")
                {
                    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[OrganizationFlow]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                }
                else if (toUserData["SignLine"] == "3")
                {
                    organName = at.QueryColumn("OrganName", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Organization]", " AND OrganID=(SELECT DeptID FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] WHERE EmpID='" + toUserData["SignID"] + "' AND CompID='" + toUserData["SignIDComp"] + "')");
                }
                oAssTo.Add(toUserData["SignID"], organName + "-" + name);//oAssTo.Add(toUserData["SignID"], name);

                if (oAssTo.Count > 0)
                {
                    //若有指派對象，才開始組合新增流程 IsFlowInsVerify() 所需的參數
                    string strStartTime = OTTimeStart.ucDefaultSelectedHH + OTTimeStart.ucDefaultSelectedMM;
                    string strEndTime = OTTimeEnd.ucDefaultSelectedHH + OTTimeEnd.ucDefaultSelectedMM;

                    string strKeyValue = "A," + _OTCompID + ",";
                    strKeyValue += _EmpID + ",";
                    strKeyValue += ucDateStart.ucSelectedDate + ",";
                    strKeyValue += ucDateEnd.ucSelectedDate + ",";
                    strKeyValue += OTSeq.ToString("00");

                    string strShowValue = _EmpID + ",";
                    strShowValue += lblOTEmpName.Text + ",";
                    strShowValue += ucDateStart.ucSelectedDate + ",";
                    strShowValue += OTTimeStart.ucDefaultSelectedHH + "：" + OTTimeStart.ucDefaultSelectedMM + ",";
                    strShowValue += ucDateEnd.ucSelectedDate + ",";
                    strShowValue += OTTimeEnd.ucDefaultSelectedHH + "：" + OTTimeEnd.ucDefaultSelectedMM;

                    if (FlowExpress.IsFlowInsVerify(flow.FlowID, strKeyValue.Split(','), strShowValue.Split(','), nextIsLastFlow ? "btnAfterLast" : "btnAfter", oAssTo, ""))
                    {
                        string a = FlowExpress.getFlowCaseID(flow.FlowID, strKeyValue);
                        //更新AssignToName(部門+員工姓名)
                        if (!string.IsNullOrEmpty(a))
                        {
                            if (ucDateStart.ucSelectedDate == ucDateEnd.ucSelectedDate)
                            {
                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + a + "'");
                                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
                                sb.Append(" AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
                                sb.Append(" AND OTStartTime='" + strStartTime + "'");
                                sb.Append(" AND OTEndTime='" + strEndTime + "'");
                                sb.Append(" AND OTSeq='" + OTSeq + "'");
                            }
                            else
                            {
                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + a + "'");
                                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateStart.ucSelectedDate + "'");
                                sb.Append(" AND OTEndDate='" + ucDateStart.ucSelectedDate + "'");
                                sb.Append(" AND OTStartTime='" + strStartTime + "'");
                                sb.Append(" AND OTEndTime='2359'");
                                sb.Append(" AND OTSeq='" + OTSeq + "'");

                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + a + "'");
                                sb.Append(" WHERE OTCompID='" + _OTCompID + "'");
                                sb.Append(" AND OTEmpID='" + _EmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateEnd.ucSelectedDate + "'");
                                sb.Append(" AND OTEndDate='" + ucDateEnd.ucSelectedDate + "'");
                                sb.Append(" AND OTStartTime='0000'");
                                sb.Append(" AND OTEndTime='" + strEndTime + "'");
                                sb.Append(" AND OTSeq='" + OTSeq_1 + "'");
                            }

                            //加進HROverTimeLog
                            FlowUtility.InsertHROverTimeLogCommand(a, "1", a + ".00001",
                               "D", empData["EmpID"], empData["OrganID"], empData["FlowOrganID"], UserInfo.getUserInfo().UserID,
                               flowCode, flowSN, "1", toUserData["SignLine"],
                               toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "0",
                               false, ref sb, 1, "1");

                            //加進MailLog
                            string Subject_1 = "";
                            string Content_1 = "";
                            string mail = "";
                            CommandHelper sbselect = db.CreateCommandHelper();
                            sbselect.Append("SELECT isnull(C.EMail,'') AS EMail FROM " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P");
                            sbselect.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Communication] C ON P.IDNo=C.IDNo");
                            sbselect.Append(" WHERE P.CompID='" + toUserData["SignIDComp"] + "' AND P.EmpID='" + toUserData["SignID"] + "'");
                            DataTable dtSelect = db.ExecuteDataSet(sbselect.BuildCommand()).Tables[0];
                            if (dtSelect.Rows.Count > 0)
                            {
                                if (string.IsNullOrEmpty(dtSelect.Rows[0]["EMail"].ToString()))
                                {
                                    mail = HREmail();
                                    Subject_1 = "系統查無通知者E-mail";
                                    Content_1 = "OverTimeExpedite||BM@QuitMailContent1||系統查無通知者<br/>" + toUserData["SignID"] + "-" + name;
                                }
                                else
                                {
                                    mail = dtSelect.Rows[0]["EMail"].ToString();
                                    Subject_1 = "加班單流程待辦案件通知";
                                    Content_1 = "OverTimeTodoList||BM@QuitMailContent1||" + name + "||BM@QuitMailContent2||" + _OTFormNO + "||BM@QuitMailContent3||" + strKeyValue.Split(',')[2] + "||BM@QuitMailContent4||" + lblOTEmpName.Text + "||BM@QuitMailContent5||" + strShowValue.Split(',')[2] + "~" + strShowValue.Split(',')[4] + "||BM@QuitMailContent6||" + strShowValue.Split(',')[3] + "||BM@QuitMailContent7||" + strShowValue.Split(',')[5] + "||BM@QuitMailContent8||" + txtOTTotalTime.Text;
                                }
                            }
                            Aattendant.InsertMailLogCommand("人力資源處", toUserData["SignIDComp"], toUserData["SignID"], mail, "", "", Subject_1, Content_1, false, ref sb);
                        }
                        try
                        {
                            db.ExecuteNonQuery(sb.BuildCommand());
                            tx.Commit();
                            Util.MsgBox("送簽成功", "AfterOvertimeOrder.aspx");//提案送審成功
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                            tx.Rollback();//資料更新失敗
                            Util.MsgBox("送簽失敗(3)"); //提案送審失敗
                            return;
                        }
                    }
                    else
                    {
                        Util.MsgBox("送簽失敗"); //提案送審失敗
                        tx.Rollback();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                tx.Rollback();//資料更新失敗
                Util.MsgBox("送簽失敗");
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
        else //直接送簽
        {
            try
            {
                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                tx.Commit();
                Util.MsgBox("送簽成功", "AfterOvertimeOrder.aspx");//提案送審成功
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                tx.Rollback();//資料更新失敗
                Util.MsgBox("送簽失敗！");
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
    }
    protected string HREmail()
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        string mail = "";
        sb.Append("SELECT SC.UserID,C.EMail FROM " + Util.getAppSetting("app://HRDB_OverTime/") + ".[dbo].[SC_UserGroup] SC");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Personal] P ON P.EmpID=SC.UserID AND P.CompID=SC.CompID");
        sb.Append(" LEFT JOIN " + Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Communication] C ON P.IDNo=C.IDNo");
        sb.Append(" WHERE SC.CompID='" + UserInfo.getUserInfo().CompID + "' AND SC.GroupID='Adm-OT'");
        DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            mail += dt.Rows[i]["EMail"].ToString() + ";";
        }
        return mail;
    }
    /// <summary>
    /// 檢查是否直接送簽，修改後必須小於等於修改前的時段
    /// </summary>
    protected string DirectSubmit(string strComp, string strOTStartDate, string strOTEndDate, string strEmpID, string strOTStartTime, string strOTEndTime, string strOTFromAdvanceTxnId, string strOTTotalTime)
    {
        DbHelper db = new DbHelper(_overtimeDBName);
        CommandHelper sb = db.CreateCommandHelper();
        DataTable dt = null;
        string result = "";
        if (strOTStartDate == strOTEndDate)
        {
            sb.Append("SELECT SUM(OT.OTTotalTime)-SUM(OT.MealTime) AS TotalTime,CASE WHEN (OT.OTStartTime='" + strOTStartTime + "' AND OT.OTEndTime='" + strOTEndTime + "') THEN 'Y'");
            sb.Append(" WHEN (CONVERT(int,OT.OTStartTime)<=" + Convert.ToInt32(strOTStartTime) + " ) AND");
            sb.Append(" (CONVERT(int,OT.OTEndTime)>=" + Convert.ToInt32(strOTEndTime) + " )THEN 'Y'");
            sb.Append(" ELSE 'N'");
            sb.Append(" END AS CheckTime FROM OverTimeAdvance OT");
            sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.OTFromAdvanceTxnId=OT.OTTxnID");
            sb.Append(" WHERE OT.OTStatus='3' AND OT.OTCompID='" + strComp + "' AND OT.OTEmpID='" + strEmpID + "' AND OT.OTStartDate='" + strOTStartDate + "' AND OT.OTEndDate='" + strOTEndDate + "' AND OTFromAdvanceTxnId='" + strOTFromAdvanceTxnId + "' ");//AND OT.OTStartTime='" + _OTStartTime + "' AND OT.OTEndTime='" + _OTEndTime+ "'
            sb.Append(" GROUP BY OT.OTStartTime,OT.OTEndTime");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        }
        else
        {
            sb.Append("SELECT TotalTime,CASE WHEN (LEFT(A.OTTime,4)='" + strOTStartTime + "' AND RIGHT(A.OTTime,4)='" + strOTEndTime + "') THEN 'Y'");
            sb.Append(" WHEN (CONVERT(int,LEFT(A.OTTime,4))<=" + Convert.ToInt32(strOTStartTime) + ") AND");
            sb.Append(" (CONVERT(int,RIGHT(A.OTTime,4))>=" + Convert.ToInt32(strOTEndTime) + ")");
            sb.Append(" THEN 'Y' ELSE 'N'");
            sb.Append(" END AS CheckTime");
            sb.Append(" FROM");
            sb.Append(" (SELECT (OT.OTStartDate+'~'+isnull(OT2.OTEndDate,OT.OTEndDate)) AS OTDate,");
            sb.Append(" (LEFT(OT.OTStartTime,2)+RIGHT(OT.OTStartTime,2)+'~'+ isnull(LEFT(OT2.OTEndTime,2)+RIGHT(OT2.OTEndTime,2),LEFT(OT.OTEndTime,2)+RIGHT(OT.OTEndTime,2))) AS OTTime ,");
            sb.Append(" Convert(Decimal(10,1),(CAST(OT.OTTotalTime AS FLOAT)-CAST(OT.MealTime AS FLOAT))+(CAST(ISNULL(OT2.OTTotalTime,0) AS FLOAT)-CAST(ISNULL(OT2.MealTime,0) AS FLOAT))) AS TotalTime");
            sb.Append(" FROM OverTimeAdvance OT ");
            sb.Append(" LEFT JOIN OverTimeDeclaration OD ON OD.OTFromAdvanceTxnId=OT.OTTxnID AND OD.OTSeqNo=2");
            sb.Append(" LEFT JOIN OverTimeAdvance OT2 on OT2.OTTxnID=OT.OTTxnID AND OT2.OTSeqNo=2");
            sb.Append(" WHERE OT.OTSeqNo=1 AND OT.OTCompID='" + strComp + "' AND OT.OTEmpID='" + strEmpID + "' AND OT.OTStatus='3' AND OD.OTFromAdvanceTxnId='" + strOTFromAdvanceTxnId + "') A");
            sb.Append(" WHERE LEFT(A.OTDate,10) <> RIGHT(A.OTDate,10) AND LEFT(A.OTDate,10)='" + strOTStartDate + "' AND RIGHT(A.OTDate,10)='" + strOTEndDate + "'");
            dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
        }

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["CheckTime"].ToString() == "Y")
            {
                double datecheck = (Math.Round((Convert.ToDouble(dt.Rows[0]["TotalTime"].ToString())) / 6, MidpointRounding.AwayFromZero)) / 10;//申請時數
                if (Convert.ToDouble(strOTTotalTime) <= datecheck)//申報時數、時間<=申請時數、時間
                {
                    result = "Y";
                }
                else
                {
                    result = "N";
                }
            }
        }
        else
        {
            result = "N";
        }
        return result;
    }
    protected void btnUpdateAttachName_Click(object sender, EventArgs e)
    {
        getAttachName();
    }
    protected void getAttachName()
    {
        DataTable dt = at.QueryData("isnull(FileName,'') as FileName", "AttachInfo", " AND FileSize > 0 AND AttachID='" + ViewState["attch"].ToString() + "'");
        _AttachID = ViewState["attch"].ToString();
        //DataTable dt = at.QueryData("isnull(FileName,'') as FileName", "AttachInfo", " AND FileSize > 0 AND AttachID='" + _AttachID + "'");
        if (dt.Rows.Count > 0)
            lblAttachName.Text = "附件檔名：" + dt.Rows[0]["FileName"].ToString();
        else
            lblAttachName.Text = "(目前無附件)";
    }
    protected void ucModalPopAttach_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        getAttachName();
    }

    protected void btnClear_Click(object sender, EventArgs e) //清除
    {
        LoadData();
    }
    protected void btnExit_Click(object sender, EventArgs e)//返回
    {
        Response.Redirect("AfterOvertimeOrder.aspx");
    }
    protected void LoadAttch()
    {
        //附件Attach  
        if (string.IsNullOrEmpty(_AttachID))
        {
            _AttachID = "test" + UserInfo.getUserInfo().UserID + Guid.NewGuid();
        }
        string strAttachAdminURL;
        string strAttachAdminBaseURL = Util._AttachAdminUrl + "?AttachDB={0}&AttachID={1}&AttachFileMaxQty={2}&AttachFileMaxKB={3}&AttachFileTotKB={4}&AttachFileExtList={5}";
        string strAttachDownloadURL;
        string strAttachDownloadBaseURL = Util._AttachDownloadUrl + "?AttachDB={0}&AttachID={1}";
        //附件編號
        strAttachAdminURL = string.Format(strAttachAdminBaseURL, _overtimeDBName, ViewState["attch"].ToString(), "1", "3072", "3072", "");
        strAttachDownloadURL = string.Format(strAttachDownloadBaseURL, _overtimeDBName, ViewState["attch"].ToString());
        frameAttach.Value = strAttachAdminURL;
        getAttachName();

    }
}