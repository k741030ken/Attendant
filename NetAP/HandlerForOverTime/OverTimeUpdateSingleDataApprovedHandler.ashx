<%@ WebHandler Language="C#" Class="OverTimeUpdateSingleDataApprovedHandler" %>

using System;
using System.Text;
using System.Data;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

using System.ComponentModel;

public class OverTimeUpdateSingleDataApprovedHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 跨域呼叫ashx測試
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// 

    public class DataInfo
    {
        [JsonProperty(PropertyName = "OTStartDate")]
        public string OTStartDate { get; set; }

        [JsonProperty(PropertyName = "OTEndDate")]
        public string OTEndDate { get; set; }

        [JsonProperty(PropertyName = "EmpID")]
        public string EmpID { get; set; }

        [JsonProperty(PropertyName = "OTStartTime")]
        public string OTStartTime { get; set; }

        [JsonProperty(PropertyName = "OTEndTime")]
        public string OTEndTime { get; set; }

        [JsonProperty(PropertyName = "OTCompID")]
        public string OTCompID { get; set; }

        [JsonProperty(PropertyName = "OTSeq")]
        public string OTSeq { get; set; }
    }


    [JsonObject("DataGrid")]
    public class DataListObject
    {
        [JsonProperty(PropertyName = "DataList")]
        public List<DataInfo> DataList { get; set; }
    }
    private string _overtimeDBName = Util.getAppSetting("app://AattendantDB_OverTime/");//"AattendantDB";
    private Aattendant at = new Aattendant();

    public void ProcessRequest(HttpContext context)
    {
        SinoPac.WebExpress.DAO.DbHelper db = new SinoPac.WebExpress.DAO.DbHelper(_overtimeDBName);
        SinoPac.WebExpress.DAO.CommandHelper sb = db.CreateCommandHelper();
        System.Data.Common.DbConnection cn = db.OpenConnection();
        System.Data.Common.DbTransaction tx = cn.BeginTransaction();
        FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OverTime/"));
        Dictionary<string, string> oAssTo = new Dictionary<string, string>();

        try
        {
            if (!UserInfo.Init(context.Request.QueryString["UserID"], true))
            {
                throw new Exception(Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Login Fail!"));
            }

            string UserComp = context.Request.QueryString["UserComp"];
            string User = context.Request.QueryString["UserID"];
            string AttachID = context.Request.QueryString["AttachID"];
            string txtOTComp = context.Request.QueryString["CompID"];
            string txtOTEmpID = context.Request.QueryString["OTEmpID"]; //txtOTEmpID.Text
            bool chkMealFlag = Boolean.Parse(context.Request.QueryString["MealFlag"]); //chkMealFlag.Checked
            string ucDateStart = context.Request.QueryString["DateStart"]; //ucDateStart.ucSelectedDate
            string ucDateEnd = context.Request.QueryString["DateEnd"]; //ucDateEnd.ucSelectedDate
            string ucOTTimeEndHH = context.Request.QueryString["OTTimeEndHH"];//OTTimeEnd.ucDefaultSelectedHH
            string ucOTTimeEndMM = context.Request.QueryString["OTTimeEndMM"];//OTTimeEnd.ucDefaultSelectedMM
            string ucOTTimeStartHH = context.Request.QueryString["OTTimeStartHH"];//OTTimeStart.ucDefaultSelectedHH
            string ucOTTimeStartMM = context.Request.QueryString["OTTimeStartMM"];//OTTimeStart.ucDefaultSelectedMM
            string txtOrganID = context.Request.QueryString["OrganID"]; //lblOrganID.Text
            string txtSalaryOrAdjust = context.Request.QueryString["SalaryOrAdjust"]; //ddlSalaryOrAdjust.SelectedValue
            string txtAdjustInvalidDate = context.Request.QueryString["AdjustInvalidDate"];//txtAdjustInvalidDate.Text
            string txtMealTime = context.Request.QueryString["MealTime"];//txtMealTime.Text
            string txtOTTypeID = context.Request.QueryString["OTTypeID"];//ddlOTTypeID.SelectedValue
            string txtOTReasonMemo = context.Request.QueryString["OTReasonMemo"]; //txtOTReasonMemo.ucTextData
            string _OTTxnID = context.Request.QueryString["OTTxnID"];

            double cntTotal = 0;
            double cntStart = 0;
            double cntEnd = 0;
            string strcheckMealFlag = (chkMealFlag == true) ? "1" : "0";
            string strMealTime = (chkMealFlag == true) ? txtMealTime : "0";

            //計算加班總時數(跨日)
            getCntStartAndCntEnd(ucOTTimeStartHH, ucOTTimeStartMM, ucOTTimeEndHH, ucOTTimeEndMM, out cntStart, out cntEnd);
            string mealOver = at.MealJudge(cntStart, Convert.ToDouble(strMealTime));

            //取得附件編號
            string attach = AttachID;
            //string attach = at.QueryAtt(AttachID, txtOTComp, txtOTEmpID);
            //if (string.IsNullOrEmpty(attach))
            //{
            //    AttachID = "test" + UserInfo.getUserInfo().UserID + Guid.NewGuid();
            //}
            //else
            //{
            //    AttachID = attach;
            //}


            int OTSeq = 0;
            int OTSeq_1 = 0;


            //剩下還沒有撈出的資料
            string OldDateStart = "";
            string OldDateEnd = "";
            string OldOTTimeStart = "";
            string OldOTTimeEnd = "";
            string OTRegisterComp = "";
            string OTRegisterID = "";


            //先查出這張加班單
            //組查詢字串
            sb.Reset();
            sb.Append(" SELECT * ");
            sb.Append(" FROM OverTimeDeclaration ");
            sb.Append(" WHERE OTTxnID ='" + _OTTxnID + "' ORDER BY OTSeqNo ");

            using (DataTable dt = db.ExecuteDataSet(sb.BuildCommand()).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    switch (dt.Rows.Count)
                    {
                        case 1:      //非跨日單
                            OldDateStart = dt.Rows[0]["OTStartDate"].ToString();
                            OldDateEnd = dt.Rows[0]["OTEndDate"].ToString();
                            OldOTTimeStart = dt.Rows[0]["OTStartTime"].ToString();
                            OldOTTimeEnd = dt.Rows[0]["OTEndTime"].ToString();
                            break;
                        case 2:      //是跨日單
                            OldDateStart = dt.Rows[0]["OTStartDate"].ToString();
                            OldDateEnd = dt.Rows[1]["OTEndDate"].ToString();
                            OldOTTimeStart = dt.Rows[0]["OTStartTime"].ToString();
                            OldOTTimeEnd = dt.Rows[1]["OTEndTime"].ToString();
                            break;
                    }

                    OTRegisterComp = dt.Rows[0]["OTRegisterComp"].ToString();
                    OTRegisterID = dt.Rows[0]["OTRegisterID"].ToString();
                }
                else
                {
                    SetResponse(context, new ReturnJsonModel()
                    {
                        IsSussecc = false,
                        Message = "送簽失敗(0)"
                    });
                    return;
                }
            }

            var empData = new Dictionary<string, string>();
            string flag;    //OTStatus
            flag = "2";

            //組要更新的字串
            sb.Reset();
            if (OldDateStart == OldDateEnd)//原本不跨日
            {
                if (ucDateStart == ucDateEnd) //不跨日
                {
                    string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                    getCntTotal(ucOTTimeStartHH, ucOTTimeStartMM, ucOTTimeEndHH, ucOTTimeEndMM, out cntTotal);  //計算加班總時數(不跨日)
                    OTSeq = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateStart);
                    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart + "',OTEndDate='" + ucDateEnd + "',OTStartTime='" + ucOTTimeStartHH + ucOTTimeStartMM + "', OTEndTime='" + ucOTTimeEndHH + ucOTTimeEndMM + "',");
                    sb.Append(" SalaryOrAdjust='" + txtSalaryOrAdjust + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" AdjustInvalidDate='', "); //失效時間
                    }
                    sb.Append(" OTAttachment='" + attach + "', ");
                    sb.Append(" OTTotalTime='" + cntTotal + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + strMealTime + "',OTTypeID='" + txtOTTypeID + "',OTReasonMemo='" + (txtOTReasonMemo).Replace("'", "''") + "',");
                    sb.Append(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserComp + "',LastChgID='" + User + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateStart + "'");
                    sb.Append(" AND OTEndDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTStartTime='" + OldOTTimeStart + "'");
                    sb.Append(" AND OTEndTime='" + OldOTTimeEnd + "'");
                    sb.Append(" AND OTStatus='1'");
                }
                else
                {
                    string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                    string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd + "'");
                    getCntStartAndCntEnd(ucOTTimeStartHH, ucOTTimeStartMM, ucOTTimeEndHH, ucOTTimeEndMM, out cntStart, out cntEnd);
                    OTSeq = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateStart);
                    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart + "',OTEndDate='" + ucDateStart + "',OTStartTime='" + ucOTTimeStartHH + ucOTTimeStartMM + "', OTEndTime='2359',");
                    sb.Append(" SalaryOrAdjust='" + txtSalaryOrAdjust + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" AdjustInvalidDate='', "); //失效時間
                    }
                    sb.Append(" OTAttachment='" + attach + "', ");
                    sb.Append(" OTTotalTime='" + cntStart + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[1] + "',OTTypeID='" + txtOTTypeID + "',OTReasonMemo='" + (txtOTReasonMemo).Replace("'", "''") + "',");
                    sb.Append(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserComp + "',LastChgID='" + User + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateStart + "'");
                    sb.Append(" AND OTEndDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTStartTime='" + OldOTTimeStart + "'");
                    sb.Append(" AND OTEndTime='" + OldOTTimeEnd + "'");
                    sb.Append(" AND OTStatus='1';");

                    OTSeq_1 = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateEnd);
                    sb.Append(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");
                    sb.Append(" SELECT  OTCompID,OTEmpID,'" + ucDateEnd + "','" + ucDateEnd + "','" + OTSeq_1 + "',OTTxnID,'2',OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID,'0000','" + ucOTTimeEndHH + ucOTTimeEndMM + "','" + cntEnd + "',SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,'" + mealOver.Split(',')[3] + "',OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,'" + strHo2 + "',ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp FROM OverTimeDeclaration");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + ucDateStart + "'");
                    sb.Append(" AND OTEndDate='" + ucDateStart + "'");
                    sb.Append(" AND OTStartTime='" + ucOTTimeStartHH + ucOTTimeStartMM + "'");
                    sb.Append(" AND OTEndTime='2359'");
                }
            }
            else
            {
                if (ucDateStart == ucDateEnd) //不跨日
                {
                    string strHo = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                    OTSeq = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateStart);
                    getCntTotal(ucOTTimeStartHH, ucOTTimeStartMM, ucOTTimeEndHH, ucOTTimeEndMM, out cntTotal);
                    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart + "',OTEndDate='" + ucDateEnd + "',OTStartTime='" + ucOTTimeStartHH + ucOTTimeStartMM + "', OTEndTime='" + ucOTTimeEndHH + ucOTTimeEndMM + "',");
                    sb.Append(" SalaryOrAdjust='" + txtSalaryOrAdjust + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" AdjustInvalidDate='', "); //失效時間
                    }
                    sb.Append(" OTAttachment='" + attach + "', ");
                    sb.Append(" OTTotalTime='" + cntTotal + "',MealFlag='" + strcheckMealFlag + "',MealTime='" + strMealTime + "',OTTypeID='" + txtOTTypeID + "',OTReasonMemo='" + (txtOTReasonMemo).Replace("'", "''") + "',");
                    sb.Append(" HolidayOrNot='" + strHo + "',LastChgComp='" + UserComp + "',LastChgID='" + User + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateStart + "'");
                    sb.Append(" AND OTEndDate='" + OldDateStart + "'");
                    sb.Append(" AND OTStartTime='" + OldOTTimeStart + "'");
                    sb.Append(" AND OTEndTime='2359'");
                    sb.Append(" AND OTStatus='1'");

                    sb.AppendStatement("DELETE FROM OverTimeDeclaration ");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTEndDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTStartTime='0000'");
                    sb.Append(" AND OTEndTime='" + OldOTTimeEnd + "'");
                }
                else
                {
                    string strHo1 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                    string strHo2 = at.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd + "'");
                    getCntStartAndCntEnd(ucOTTimeStartHH, ucOTTimeStartMM, ucOTTimeEndHH, ucOTTimeEndMM, out cntStart, out cntEnd);
                    OTSeq = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateStart);
                    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateStart + "',OTEndDate='" + ucDateStart + "',OTStartTime='" + ucOTTimeStartHH + ucOTTimeStartMM + "', OTEndTime='2359',");
                    sb.Append(" SalaryOrAdjust='" + txtSalaryOrAdjust + "',OTSeq='" + OTSeq + "',OTStatus='" + flag + "',");
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" AdjustInvalidDate='', "); //失效時間
                    }
                    sb.Append(" OTAttachment='" + attach + "', ");
                    sb.Append(" OTTotalTime='" + cntStart + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[1] + "',OTTypeID='" + txtOTTypeID + "',OTReasonMemo='" + (txtOTReasonMemo).Replace("'", "''") + "',");
                    sb.Append(" HolidayOrNot='" + strHo1 + "',LastChgComp='" + UserComp + "',LastChgID='" + User + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateStart + "'");
                    sb.Append(" AND OTEndDate='" + OldDateStart + "'");
                    sb.Append(" AND OTStartTime='" + OldOTTimeStart + "'");
                    sb.Append(" AND OTEndTime='2359'");
                    sb.Append(" AND OTStatus='1'");

                    OTSeq_1 = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateEnd);
                    sb.AppendStatement("UPDATE OverTimeDeclaration SET OTStartDate='" + ucDateEnd + "',OTEndDate='" + ucDateEnd + "',OTStartTime='0000', OTEndTime='" + ucOTTimeEndHH + ucOTTimeEndMM + "',");
                    sb.Append(" SalaryOrAdjust='" + txtSalaryOrAdjust + "',OTSeq='" + OTSeq_1 + "',OTStatus='" + flag + "',");
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" AdjustInvalidDate='" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" AdjustInvalidDate='', "); //失效時間
                    }
                    sb.Append(" OTAttachment='" + attach + "', ");
                    sb.Append(" OTTotalTime='" + cntEnd + "',MealFlag='" + mealOver.Split(',')[0] + "',MealTime='" + mealOver.Split(',')[3] + "',OTTypeID='" + txtOTTypeID + "',OTReasonMemo='" + (txtOTReasonMemo).Replace("'", "''") + "',");
                    sb.Append(" HolidayOrNot='" + strHo2 + "',LastChgComp='" + UserComp + "',LastChgID='" + User + "',LastChgDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'");
                    sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                    sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                    sb.Append(" AND OTStartDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTEndDate='" + OldDateEnd + "'");
                    sb.Append(" AND OTStartTime='0000'");
                    sb.Append(" AND OTEndTime='" + OldOTTimeEnd + "'");
                    sb.Append(" AND OTStatus='1'");
                }
            }

            ///
            /// 開始進行送簽
            /// 

            //DataTable dt1;
            //DataTable dt2;

            try
            {
                //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
                //db.ExecuteNonQuery(sb.BuildCommand(), tx);
                //tx.Commit();


                //若有指派對象，才開始組合新增流程 IsFlowInsVerify() 所需的參數
                oAssTo.Clear();
                using (DataTable dt = AattendantForHandler.getOrganHRBoss(UserComp, User, txtOTComp, txtOTEmpID))
                {
                    oAssTo.Add(dt.Rows[0]["Boss"].ToString(), dt.Rows[0]["oAssName"].ToString());
                }

                if (oAssTo.Count > 0)
                {
                    string strStartTime = ucOTTimeStartHH + ucOTTimeStartMM;
                    string strEndTime = ucOTTimeEndHH + ucOTTimeEndMM;
                    //string strQrySQL = @"select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTSeq,OTFormNO from OverTimeDeclaration where  OTStatus='2' AND OTRegisterID = '{0}' and OTEmpID='{1}' and OTStartDate='{2}' and OTEndDate='{3}' and OTStartTime='{4}' and OTEndTime='{5}'";
                    //if (ucDateStart == ucDateEnd) //不跨日送簽
                    //{
                    //    strQrySQL = string.Format(strQrySQL, User, txtOTEmpID, ucDateStart, ucDateEnd, strStartTime, strEndTime);
                    //}
                    //else
                    //{
                    //    strQrySQL = string.Format(strQrySQL, User, txtOTEmpID, ucDateStart, ucDateStart, strStartTime, "2359");
                    //}

                    //try
                    //{
                    //    dt1 = db.ExecuteDataSet(CommandType.Text, strQrySQL).Tables[0];
                    //}
                    //catch (Exception ex)
                    //{
                    //    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                    //    SetResponse(context, new ReturnJsonModel()
                    //    {
                    //        IsSussecc = false,
                    //        Message = "送簽失敗(2)"
                    //    });
                    //    return;
                    //}

                    //if (dt1 != null && dt1.Rows.Count > 0)
                    //{

                    //ex:編號送件者
                    string strKeyValue = "B," + txtOTComp + ",";       //"B," + dt1.Rows[0]["OTCompID"].ToString() + ",";
                    strKeyValue += txtOTEmpID + ",";     //dt1.Rows[0]["OTEmpID"].ToString() + ",";
                    strKeyValue += ucDateStart + ",";     //dt1.Rows[0]["OTStartDate"].ToString() + ",";
                    strKeyValue += ucDateEnd + ",";       //dt1.Rows[0]["OTEndDate"].ToString() + ",";
                    strKeyValue += OTSeq.ToString("00");    //dt1.Rows[0]["OTSeq"].ToString();

                    string strShowValue = txtOTEmpID + ",";      //dt1.Rows[0]["OTEmpID"].ToString() + ",";
                    strShowValue += at.QueryHRColumn("NameN", "Personal", "And CompID = '" + txtOTComp + "' And EmpID = '" + txtOTEmpID + "'") + ",";       //at.QueryHRColumn("NameN", "Personal", "And CompID = '" + dt1.Rows[0]["OTCompID"].ToString() + "' And EmpID = '" + dt1.Rows[0]["OTEmpID"].ToString() + "'") + ",";
                    strShowValue += ucDateStart + ",";        //dt1.Rows[0]["OTStartDate"].ToString() + ",";
                    strShowValue += ucOTTimeStartHH + "：" + ucOTTimeStartMM + ",";      //(dt1.Rows[0]["OTStartTime"].ToString()).Substring(0, 2) + "：" + (dt1.Rows[0]["OTStartTime"].ToString()).Substring(2, 2) + ",";
                    strShowValue += ucDateEnd + ",";
                    strShowValue += ucOTTimeEndHH + "：" + ucOTTimeEndMM;
                    //if (ucDateStart == ucDateEnd) //不跨日送簽
                    //{
                    //    strShowValue += dt1.Rows[0]["OTEndDate"].ToString() + ",";
                    //    strShowValue += (dt1.Rows[0]["OTEndTime"].ToString()).Substring(0, 2) + "：" + (dt1.Rows[0]["OTEndTime"].ToString()).Substring(2, 2);
                    //}
                    //else
                    //{
                    //    string strQrySQL2 = @"select OTCompID,OTEmpID,OTStartDate,OTEndDate,OTStartTime,OTEndTime,OTSeq,OTFormNO from OverTimeDeclaration where  OTStatus='2' AND OTRegisterID = '{0}' and OTEmpID='{1}' and OTStartDate='{2}' and OTEndDate='{3}' and OTStartTime='{4}' and OTEndTime='{5}'";
                    //    strQrySQL2 = string.Format(strQrySQL2, User, txtOTEmpID, ucDateEnd, ucDateEnd, "0000", strEndTime);
                    //    try
                    //    {
                    //        dt2 = db.ExecuteDataSet(CommandType.Text, strQrySQL2).Tables[0];
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                    //        SetResponse(context, new ReturnJsonModel()
                    //        {
                    //            IsSussecc = false,
                    //            Message = "送簽失敗(3)"
                    //        });
                    //        return;
                    //    }

                    //    strShowValue += dt2.Rows[0]["OTEndDate"].ToString() + ",";
                    //    strShowValue += (dt2.Rows[0]["OTEndTime"].ToString()).Substring(0, 2) + "：" + (dt2.Rows[0]["OTEndTime"].ToString()).Substring(2, 2);
                    //}

                    if (FlowExpress.IsFlowInsVerify(flow.FlowID, strKeyValue.Split(','), strShowValue.Split(','), "btnAfter", oAssTo, ""))
                    {
                        string flowCaseID = FlowExpress.getFlowCaseID(flow.FlowID, strKeyValue);
                        //更新AssignToName(部門+員工姓名)
                        if (!string.IsNullOrEmpty(flowCaseID))
                        {
                            //CommandHelper sb1 = db.CreateCommandHelper();
                            string NameN = "";
                            string RankID = "";
                            string TitleID = "";
                            string OrganID = "";
                            string UpOrganID = "";
                            string Boss = "";
                            string BossCompID = "";
                            string DeptID = "";
                            string PositionID = "";
                            string WorkTypeID = "";
                            string FlowOrganID = "";
                            string FlowDeptID = "";
                            string FlowUpOrganID = "";
                            string FlowBoss = "";
                            string FlowBossCompID = "";
                            string BusinessType = "";
                            string EmpFlowRemarkID = "";
                            string bBoss = "";
                            string bBossCompID = "";
                            string bOrganID = "";
                            //查詢申請人資料
                            EmpInfo.QueryEmpData(OTRegisterComp, OTRegisterID, DateTime.Now.ToString("yyyy/MM/dd"),
                                out NameN, out RankID, out TitleID, out OrganID, out UpOrganID, out Boss, out BossCompID, out DeptID, out PositionID, out WorkTypeID,
                                out FlowOrganID, out FlowDeptID, out FlowUpOrganID, out FlowBoss, out FlowBossCompID, out BusinessType, out EmpFlowRemarkID);
                            //查詢申請人主管資料
                            EmpInfo.QueryOrganData(OTRegisterComp, OrganID, DateTime.Now.ToString("yyyy/MM/dd"), out bOrganID, out bBoss, out bBossCompID);

                            /*20170323: 當HR審核主管與加班人是同一人時，要以加班人主管為審核主管*/
                            if (bBossCompID.Trim().Equals(txtOTComp.Trim()) && bBoss.Trim().Equals(txtOTEmpID.Trim()))
                            {
                                //查詢加班人資料
                                EmpInfo.QueryEmpData(txtOTComp, txtOTEmpID, DateTime.Now.ToString("yyyy/MM/dd"),
                                    out NameN, out RankID, out TitleID, out OrganID, out UpOrganID, out Boss, out BossCompID, out DeptID, out PositionID, out WorkTypeID,
                                    out FlowOrganID, out FlowDeptID, out FlowUpOrganID, out FlowBoss, out FlowBossCompID, out BusinessType, out EmpFlowRemarkID);
                                //查詢加班人主管資料
                                EmpInfo.QueryOrganData(txtOTComp, OrganID, DateTime.Now.ToString("yyyy/MM/dd"), out bOrganID, out bBoss, out bBossCompID);
                            }
                            
                            //加進HROverTimeLog
                            //FlowUtility.InsertHROverTimeLogCommand(flowCaseID, "1", flowCaseID + ".00001",
                            //   "D", txtOTEmpID, txtOrganID, "", OTRegisterID,
                            //   "", "", "", "4", OTRegisterComp, bBoss, bOrganID, "", "0", false, ref sb1, 1);
                            FlowUtility.InsertHROverTimeLogCommand(flowCaseID, "1", flowCaseID + ".00001",
                            "D", txtOTEmpID, txtOrganID, "", OTRegisterID,
                            "", "", "", "4", OTRegisterComp, bBoss, bOrganID, "", "0", false, ref sb, 1);

                            //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
                            //try
                            //{

                            //    db.ExecuteNonQuery(sb1.BuildCommand());
                            //}
                            //catch (Exception ex)
                            //{
                            //    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                            //    tx.Rollback();//資料更新失敗
                            //    Util.MsgBox("送簽失敗(6)"); //提案送審失敗
                            //    return;
                            //}

                            //回寫FlowCaseID
                            if (ucDateStart == ucDateEnd)
                            {
                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "'");
                                sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                                sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateStart + "'");
                                sb.Append(" AND OTEndDate='" + ucDateEnd + "'");
                                sb.Append(" AND OTStartTime='" + strStartTime + "'");
                                sb.Append(" AND OTEndTime='" + strEndTime + "'");
                                sb.Append(" AND OTTxnID='" + _OTTxnID + "'");
                                //sb.Append(" AND OTSeq='" + OTSeq + "'");
                            }
                            else
                            {
                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "'");
                                sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                                sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateStart + "'");
                                sb.Append(" AND OTEndDate='" + ucDateStart + "'");
                                sb.Append(" AND OTStartTime='" + strStartTime + "'");
                                sb.Append(" AND OTEndTime='2359'");
                                sb.Append(" AND OTTxnID='" + _OTTxnID + "'");
                                //sb.Append(" AND OTSeq='" + OTSeq + "'");

                                sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID='" + flowCaseID + "'");
                                sb.Append(" WHERE OTCompID='" + txtOTComp + "'");
                                sb.Append(" AND OTEmpID='" + txtOTEmpID + "'");
                                sb.Append(" AND OTStartDate='" + ucDateEnd + "'");
                                sb.Append(" AND OTEndDate='" + ucDateEnd + "'");
                                sb.Append(" AND OTStartTime='0000'");
                                sb.Append(" AND OTEndTime='" + strEndTime + "'");
                                sb.Append(" AND OTTxnID='" + _OTTxnID + "'");
                                //sb.Append(" AND OTSeq='" + OTSeq_1 + "'");
                            }


                            //if (ucDateStart != ucDateEnd)
                            //{
                            //    //sb.Reset();
                            //    sb.AppendStatement("UPDATE OverTimeDeclaration SET FlowCaseID=A.FlowCaseID");
                            //    sb.Append(" FROM (SELECT FlowCaseID FROM OverTimeDeclaration");
                            //    sb.Append(" WHERE OTEmpID='" + txtOTEmpID + "'");
                            //    sb.Append(" AND OTCompID='" + txtOTComp + "'");
                            //    sb.Append(" AND OTStartDate='" + ucDateStart + "'");
                            //    sb.Append(" AND OTEndDate='" + ucDateStart + "'");
                            //    sb.Append(" AND OTStartTime='" + strStartTime + "'");
                            //    sb.Append(" AND OTEndTime='2359'");
                            //    sb.Append(" ) A ");
                            //    sb.Append(" WHERE OTEmpID='" + txtOTEmpID + "'");
                            //    sb.Append(" AND OTCompID='" + txtOTComp + "'");
                            //    sb.Append(" AND OTStartDate='" + ucDateEnd + "'");
                            //    sb.Append(" AND OTEndDate='" + ucDateEnd + "'");
                            //    sb.Append(" AND OTStartTime='0000'");
                            //    sb.Append(" AND OTEndTime='" + strEndTime + "'");
                            //}

                            try
                            {
                                db.ExecuteNonQuery(sb.BuildCommand(), tx);
                                tx.Commit();
                                //Util.MsgBox("送簽成功");//提案送審成功
                                SetResponse(context, new ReturnJsonModel()
                                {
                                    IsSussecc = true,
                                    Message = "送簽成功"
                                });
                                return;
                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                                tx.Rollback();//資料更新失敗
                                SetResponse(context, new ReturnJsonModel()
                                {
                                    IsSussecc = false,
                                    Message = "送簽失敗(4)"
                                });
                                return;
                            }
                        }
                    }
                    else
                    {
                        //Util.MsgBox("送簽失敗"); //提案送審失敗
                        SetResponse(context, new ReturnJsonModel()
                        {
                            IsSussecc = false,
                            Message = "送簽失敗(5)"
                        });
                        return;
                    }
                    //}
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                tx.Rollback();//資料更新失敗
                SetResponse(context, new ReturnJsonModel()
                {
                    IsSussecc = false,
                    Message = "送簽失敗(6)"
                });
                return;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
                tx.Dispose();
            }
        }
        catch (Exception ex)
        {
            LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            SetResponse(context, new ReturnJsonModel()
            {
                IsSussecc = false,
                Message = "送簽失敗(6)"
            });
            return;
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
                cn.Dispose();
            }

            if (tx != null)
            {
                tx.Dispose();
            }
        }
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class RequestJsonModel
    {
        public string test01 { get; set; }
        public string test02 { get; set; }
    }

    /// <summary>
    /// 設定回傳至畫面的json變數
    /// </summary>
    private class ReturnJsonModel
    {
        public bool IsSussecc { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// 設定Response
    /// </summary>
    /// <param name="context">HttpResponse</param>
    /// <param name="jsonData">ReturnJsonModel</param>
    private static void SetResponse(HttpContext context, ReturnJsonModel jsonData)
    {
        string jsonCallback = context.Request.QueryString["jsoncallback"];

        if (string.IsNullOrEmpty(jsonCallback))
        {
            jsonCallback = context.Request.QueryString["callback"];
        }

        context.Response.ContentType = "application/json";
        context.Response.Charset = "utf-8";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        context.Response.Write(string.Format("{0}({1});", jsonCallback, serializer.Serialize(jsonData)));
    }

    /// <summary>
    /// 將Request.InputStream轉成城可讀字串
    /// </summary>
    /// <param name="context">HttpContext</param>
    /// <returns>string</returns>
    private static string GetFromInputStream(HttpContext context)
    {
        var reader = new System.IO.StreamReader(context.Request.InputStream);
        var result = reader.ReadToEnd();

        return result;
    }

    /// <summary>
    /// ashx預設參數
    /// </summary>
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// 計算加班總時數(跨日)
    /// </summary>
    /// <param name="cntStart">起日時數</param>
    /// <param name="cntEnd">迄日時數</param>
    private void getCntStartAndCntEnd(string OTStartTimeHH, string OTStartTimeMM, string OTEndTimeHH, string OTEndTimeMM, out double cntStart, out double cntEnd)
    {
        cntStart = 0;
        cntEnd = 0;
        if (OTStartTimeHH != "請選擇" && OTStartTimeMM != "請選擇" && OTEndTimeHH != "請選擇" && OTEndTimeMM != "請選擇" && OTStartTimeHH != "" && OTStartTimeMM != "" && OTEndTimeHH != "" && OTEndTimeMM != "")
        {
            cntStart = (23 - (Convert.ToDouble(OTStartTimeHH))) * 60 + (60 - Convert.ToDouble(OTStartTimeMM));
            cntEnd = (Convert.ToDouble(OTEndTimeHH)) * 60 + Convert.ToDouble(OTEndTimeMM);
        }
    }

    /// <summary>
    /// 計算加班總時數(不跨日)
    /// </summary>
    /// <param name="cntTotal">總加班時數</param>
    private void getCntTotal(string OTStartTimeHH, string OTStartTimeMM, string OTEndTimeHH, string OTEndTimeMM, out double cntTotal)
    {
        cntTotal = 0;
        if (OTStartTimeHH != "請選擇" && OTStartTimeMM != "請選擇" && OTEndTimeHH != "請選擇" && OTEndTimeMM != "請選擇" && OTStartTimeHH != "" && OTStartTimeMM != "" && OTEndTimeHH != "" && OTEndTimeMM != "")
        {
            cntTotal = (Convert.ToDouble(OTEndTimeHH) * 60 + Convert.ToDouble(OTEndTimeMM)) - (Convert.ToDouble(OTStartTimeHH) * 60 + Convert.ToDouble(OTStartTimeMM));
        }
    }
}

