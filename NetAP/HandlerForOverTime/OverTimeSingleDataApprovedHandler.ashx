<%@ WebHandler Language="C#" Class="OverTimeSingleDataApprovedHandler" %>

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

public class OverTimeSingleDataApprovedHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
            string _FormNo = at.QueryFormNO("AdvanceFormSeq", UserComp, User);//context.Request.QueryString["_FormNo"];
            string _AttachID = context.Request.QueryString["AttachID"];
            string txtOTComp = context.Request.QueryString["CompID"];
            string txtOTEmpID = context.Request.QueryString["OTEmpID"]; //txtOTEmpID.Text
            bool chkMealFlag = Boolean.Parse(context.Request.QueryString["MealFlag"]); //chkMealFlag.Checked
            string ucDateStart = context.Request.QueryString["DateStart"]; //ucDateStart
            string ucDateEnd = context.Request.QueryString["DateEnd"]; //ucDateEnd
            string ucOTTimeEndHH = context.Request.QueryString["OTTimeEndHH"];//OTTimeEnd.ucDefaultSelectedHH
            string ucOTTimeEndMM = context.Request.QueryString["OTTimeEndMM"];//OTTimeEnd.ucDefaultSelectedMM
            string ucOTTimeStartHH = context.Request.QueryString["OTTimeStartHH"];//OTTimeStart.ucDefaultSelectedHH
            string ucOTTimeStartMM = context.Request.QueryString["OTTimeStartMM"];//OTTimeStart.ucDefaultSelectedMM
            string txtDeptID = context.Request.QueryString["DeptID"]; //lblDeptID.Text
            string txtOrganID = context.Request.QueryString["OrganID"]; //lblOrganID.Text
            string txtDeptName = context.Request.QueryString["DeptName"]; //txtDeptName.Text
            string txtOrganName = context.Request.QueryString["OrganName"]; //txtOrganName.Text
            string txtSalaryOrAdjust = context.Request.QueryString["SalaryOrAdjust"]; //ddlSalaryOrAdjust.SelectedValue
            string txtAdjustInvalidDate = context.Request.QueryString["AdjustInvalidDate"];//txtAdjustInvalidDate.Text
            string txtMealTime = context.Request.QueryString["MealTime"];//txtMealTime.Text
            string txtOTTypeID = context.Request.QueryString["OTTypeID"];//ddlOTTypeID.SelectedValue
            string txtOTReasonMemo = context.Request.QueryString["OTReasonMemo"]; //txtOTReasonMemo.ucTextData
            string _OTTxnID = "";

            var toUserData = new Dictionary<string, string>();
            var empData = new Dictionary<string, string>();

            double cntTotal = 0;
            double cntStart = 0;
            double cntEnd = 0;
            double total = 0.0;
            string strAttachID = at.QueryAtt(_AttachID, txtOTComp, txtOTEmpID);
            string strcheckMealFlag = (chkMealFlag == true) ? "1" : "0";
            string strMealTime = (chkMealFlag == true) ? txtMealTime : "0";
            int OTSeq = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, ucDateStart);
            int OTSeqs = 0;
            _OTTxnID = (txtOTComp + txtOTEmpID + Convert.ToDateTime(ucDateStart).ToString("yyyyMMdd") + OTSeq.ToString("00"));
            sb.Reset();
            if (ucDateStart == ucDateEnd) //不跨日送簽
            {
                string strHo = AattendantForHandler.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                cntTotal = (Convert.ToDouble(ucOTTimeEndHH) * 60 + Convert.ToDouble(ucOTTimeEndMM)) - (Convert.ToDouble(ucOTTimeStartHH) * 60 + Convert.ToDouble(ucOTTimeStartMM));
                sb.Reset();
                sb.Append(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");
                sb.Append(" VALUES('" + txtOTComp + "', '" + txtOTEmpID + "',");
                sb.Append(" '" + ucDateStart + "', '" + ucDateEnd + "',");
                sb.Append(" '" + OTSeq.ToString("00") + "',");
                sb.Append(" '" + _OTTxnID + "',");
                sb.Append(" '1','',");//OTSeqNo
                sb.Append(" '" + txtDeptID + "', '" + txtOrganID + "','" + txtDeptName + "','" + txtOrganName + "',");
                sb.Append(" '', "); //流程ID(10)
                sb.Append(" '" + ucOTTimeStartHH + ucOTTimeStartMM + "', '" + ucOTTimeEndHH + ucOTTimeEndMM + "',");
                sb.Append(" '" + cntTotal + "', ");
                sb.Append(" '" + txtSalaryOrAdjust + "' ,"); //轉薪資或補休
                if (txtSalaryOrAdjust == "2")
                {
                    sb.Append(" '" + txtAdjustInvalidDate + "', "); //失效時間
                }
                else
                {
                    //sb.Append(" '1900-01-01 00:00:00.000', "); //失效時間
                    sb.Append(" '', "); //失效時間
                }

                sb.Append(" '', '1900-01-01 00:00:00.000',");//轉補休狀態、補休日
                sb.Append(" '" + strcheckMealFlag + "', '" + txtMealTime + "',");
                sb.Append(" '" + txtOTTypeID + "', '', '" + txtOTReasonMemo.Replace("'", "''") + "',");//加班原因的
                sb.Append(" '" + strAttachID + "', '" + _FormNo + "',"); //上傳附件表單編號
                sb.Append(" '" + User + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                sb.Append(" '2',"); //申請單狀態 
                sb.Append(" '1900-01-01 00:00:00.000','', '1900-01-01 00:00:00.000','','',");//公文單號
                sb.Append(" '0','" + strHo + "',");//計薪註記 OTSalaryPaid=0
                sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000',''");//處理日期D,OTPayDate,OTModifyDate D,OTRemark
                //sb.Append(" ,'" + UserComp + "','" + User + "','1',");//KeyInComp,KeyInID,HRKeyInFlag
                sb.Append(" ,'','','1',");//KeyInComp,KeyInID,HRKeyInFlag
                sb.Append(" '" + UserComp + "', ");
                sb.Append(" '" + User + "', ");
                sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                sb.Append(" '" + UserComp + "')");  //填單人公司代碼
            }
            else//跨日送簽
            {
                cntStart = (23 - (Convert.ToDouble(ucOTTimeStartHH))) * 60 + (60 - Convert.ToDouble(ucOTTimeStartMM));
                cntEnd = Convert.ToDouble(ucOTTimeEndHH) * 60 + Convert.ToDouble(ucOTTimeEndMM);
                total = Math.Round(((cntStart + cntEnd) - Convert.ToDouble(strMealTime)) / 60, 2);

                //判斷迄日是否需要扣除不足的用餐時間
                string mealOver = at.MealJudge(cntStart, Convert.ToDouble(txtMealTime));

                string strHo1 = AattendantForHandler.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateStart + "'");
                string strHo2 = AattendantForHandler.QueryColumn("HolidayOrNot", Util.getAppSetting("app://eHRMSDB_OverTime/") + ".[dbo].[Calendar] ", " AND CompID = '" + txtOTComp + "' AND CONVERT(CHAR(10),SysDate, 111) ='" + ucDateEnd + "'");
                string crossDayArray = ucDateStart + "," + ucDateEnd;
                _OTTxnID = (txtOTComp + txtOTEmpID + Convert.ToDateTime(ucDateStart).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                sb.Reset();
                for (int i = 0; i < crossDayArray.Split(',').Length; i++)
                {
                    sb.Append(" INSERT INTO OverTimeDeclaration(OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID,OTSeqNo,OTFromAdvanceTxnId,DeptID,OrganID,DeptName,OrganName,FlowCaseID,OTStartTime,OTEndTime,OTTotalTime,SalaryOrAdjust,AdjustInvalidDate,AdjustStatus,AdjustDate,MealFlag,MealTime,OTTypeID,OTReasonID,OTReasonMemo,OTAttachment,OTFormNO,OTRegisterID,OTRegisterDate,OTStatus,OTValidDate,OTValidID,OTRejectDate,OTRejectID,OTGovernmentNo,OTSalaryPaid,HolidayOrNot,ProcessDate,OTPayDate,OTModifyDate,OTRemark,KeyInComp,KeyInID,HRKeyInFlag,LastChgComp,LastChgID,LastChgDate,OTRegisterComp) ");
                    sb.Append(" VALUES('" + txtOTComp + "', '" + txtOTEmpID + "',");
                    if (crossDayArray.Split(',')[i] == ucDateStart)
                    {
                        sb.Append(" '" + crossDayArray.Split(',')[0] + "', '" + crossDayArray.Split(',')[0] + "','" + OTSeq.ToString("00") + "',");
                    }
                    else
                    {
                        OTSeqs = at.QuerySeq("OverTimeDeclaration", txtOTComp, txtOTEmpID, crossDayArray.Split(',')[1]);
                        sb.Append(" '" + crossDayArray.Split(',')[1] + "', '" + crossDayArray.Split(',')[1] + "','" + OTSeqs.ToString("00") + "',");
                    }
                    if (OTSeqs.ToString("00") != _OTTxnID.Substring(20, 2))
                    {
                        _OTTxnID = (txtOTComp + txtOTEmpID + Convert.ToDateTime(ucDateStart).ToString("yyyyMMdd") + OTSeq.ToString("00"));
                    }
                    sb.Append(" '" + _OTTxnID + "',");
                    if (crossDayArray.Split(',')[i] == ucDateStart)//OTSeqNo
                    {
                        sb.Append(" '1',");
                    }
                    else
                    {
                        sb.Append(" '2',");
                    }
                    sb.Append(" '','" + txtDeptID + "', '" + txtOrganID + "','" + txtDeptName + "','" + txtOrganName + "',");
                    sb.Append(" '', "); //流程ID(10)

                    if (crossDayArray.Split(',')[i] == ucDateStart)
                    {
                        sb.Append(" '" + ucOTTimeStartHH + ucOTTimeStartMM + "','2359','" + cntStart + "',  ");
                    }
                    else
                    {
                        sb.Append(" '0000', '" + ucOTTimeEndHH + ucOTTimeEndMM + "','" + cntEnd + "',");
                    }
                    sb.Append(" '" + txtSalaryOrAdjust + "' ,"); //轉薪資或補休
                    if (txtSalaryOrAdjust == "2")
                    {
                        sb.Append(" '" + txtAdjustInvalidDate + "', "); //失效時間
                    }
                    else
                    {
                        sb.Append(" '', "); //失效時間
                    }
                    sb.Append(" '','','" + mealOver.Split(',')[0] + "', ");
                    if (crossDayArray.Split(',')[i] == ucDateStart)
                    {
                        sb.Append(" '" + mealOver.Split(',')[1] + "', ");
                    }
                    else
                    {
                        sb.Append(" '" + mealOver.Split(',')[3] + "', ");
                    }
                    sb.Append(" '" + txtOTTypeID + "','','" + txtOTReasonMemo + "',  ");
                    sb.Append(" '" + strAttachID + "', '" + _FormNo + "', "); //上傳附件表單編號
                    sb.Append(" '" + User + "', ");
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', ");
                    sb.Append(" '2', "); //申請單狀態(25)
                    sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','','','0', "); //公文單號(25)
                    if (crossDayArray.Split(',')[i] == ucDateStart)
                    {
                        sb.Append(" '" + strHo1 + "', ");
                    }
                    else
                    {
                        sb.Append(" '" + strHo2 + "', ");
                    }
                    sb.Append(" '1900-01-01 00:00:00.000','','1900-01-01 00:00:00.000','',");//處理日期D,OTPayDate,OTModifyDate D,OTRemark
                    sb.Append(" '" + UserComp + "','" + User + "','1',");//KeyInComp,KeyInID,HRKeyInFlag
                    sb.Append(" '" + UserComp + "', ");
                    sb.Append(" '" + User + "', ");
                    sb.Append(" '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
                    sb.Append(" '" + UserComp + "');");
                }
            }

            //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
            //try
            //{
            //    db.ExecuteNonQuery(sb.BuildCommand(), tx);
            //    tx.Commit();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
            //    tx.Rollback();//資料更新失敗
            //    SetResponse(context, new ReturnJsonModel()
            //    {
            //        IsSussecc = false,
            //        Message = "送簽失敗(1)"
            //    });
            //    return;
            //}

            //DataTable dt1;
            //DataTable dt2;

            //若有指派對象，才開始組合新增流程 IsFlowInsVerify() 所需的參數
            oAssTo.Clear();
            using (DataTable dt = AattendantForHandler.getOrganHRBoss(UserComp, User, txtOTComp, txtOTEmpID))
            {
                oAssTo.Add(dt.Rows[0]["Boss"].ToString(), dt.Rows[0]["oAssName"].ToString());
            }

            if (oAssTo.Count > 0)
            {
                //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
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
                string strKeyValue = "B," + txtOTComp + ",";     //"B," + dt1.Rows[0]["OTCompID"].ToString() + ",";
                strKeyValue += txtOTEmpID + ",";     //dt1.Rows[0]["OTEmpID"].ToString() + ",";
                strKeyValue += ucDateStart + ",";     //dt1.Rows[0]["OTStartDate"].ToString() + ",";
                strKeyValue += ucDateEnd + ",";    //dt1.Rows[0]["OTEndDate"].ToString() + ",";
                strKeyValue += OTSeq.ToString("00");       //dt1.Rows[0]["OTSeq"].ToString();

                string strShowValue = txtOTEmpID + ",";     //dt1.Rows[0]["OTEmpID"].ToString() + ",";
                strShowValue += at.QueryHRColumn("NameN", "Personal", "And CompID = '" + txtOTComp + "' And EmpID = '" + txtOTEmpID + "'") + ",";       //at.QueryHRColumn("NameN", "Personal", "And CompID = '" + dt1.Rows[0]["OTCompID"].ToString() + "' And EmpID = '" + dt1.Rows[0]["OTEmpID"].ToString() + "'") + ",";
                strShowValue += ucDateStart + ",";      //dt1.Rows[0]["OTStartDate"].ToString() + ",";
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
                        EmpInfo.QueryEmpData(UserComp, User, DateTime.Now.ToString("yyyy/MM/dd"),
                            out NameN, out RankID, out TitleID, out OrganID, out UpOrganID, out Boss, out BossCompID, out DeptID, out PositionID, out WorkTypeID,
                            out FlowOrganID, out FlowDeptID, out FlowUpOrganID, out FlowBoss, out FlowBossCompID, out BusinessType, out EmpFlowRemarkID);
                        //查詢申請人主管資料
                        EmpInfo.QueryOrganData(UserComp, OrganID, DateTime.Now.ToString("yyyy/MM/dd"), out bOrganID, out bBoss, out bBossCompID);

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

                        //2017/03/10-原本是先新增進資料庫再送簽，現在要整合在一起送簽
                        //回寫FlowCaseID
                        if (!string.IsNullOrEmpty(flowCaseID))
                        {
                            if (ucDateStart == ucDateEnd) //回寫FlowCaseID
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
                                //sb.Append(" AND OTSeq='" + OTSeqs + "'");
                            }

                            //加進HROverTimeLog
                            //FlowUtility.InsertHROverTimeLogCommand(flowCaseID, "1", flowCaseID + ".00001",
                            //   "D", txtOTEmpID, txtOrganID, "", User,
                            //   "", "", "", "4", UserComp, bBoss, bOrganID, "", "0", false, ref sb1, 1);
                            FlowUtility.InsertHROverTimeLogCommand(flowCaseID, "1", flowCaseID + ".00001",
                               "D", txtOTEmpID, txtOrganID, "", User,
                               "", "", "", "4", UserComp, bBoss, bOrganID, "", "0", false, ref sb, 1);
                        }

                        try
                        {
                            //db.ExecuteNonQuery(sb1.BuildCommand());
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
                            Util.MsgBox("送簽失敗(6)"); //提案送審失敗
                            return;
                        }
                    }

                    //回寫FlowCaseID
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

                    //    try
                    //    {
                    //        //tx = cn.BeginTransaction();
                    //        db.ExecuteNonQuery(sb.BuildCommand(), tx);
                    //        tx.Commit();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        LogHelper.WriteSysLog(ex); //將 Exception 丟給 Log 模組
                    //        tx.Rollback();//資料更新失敗
                    //        SetResponse(context, new ReturnJsonModel()
                    //        {
                    //            IsSussecc = false,
                    //            Message = "送簽失敗(4)"
                    //        });
                    //        return;
                    //    }
                    //}
                    ////Util.MsgBox("送簽成功");//提案送審成功
                    //SetResponse(context, new ReturnJsonModel()
                    //{
                    //    IsSussecc = true,
                    //    Message = "送簽成功"
                    //});
                    //return;
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

        //try
        //{
        //    var message = "";
        //    //Get QueryString
        //    //result = context.Request.QueryString["test01"];

        //    //取得整個Request傳過來的InputStream資料
        //    //string requestJsonData = GetFromInputStream(context);
        //    //if (string.IsNullOrEmpty(requestJsonData))
        //    //{
        //    //    throw new Exception("No Data !!");
        //    //}

        //    //var jData = new JavaScriptSerializer().Deserialize<RequestJsonModel>(requestJsonData);
        //    if (string.IsNullOrEmpty(context.Request.QueryString["test01"]))
        //    {
        //        throw new Exception("test01 no data !!");
        //    }

        //    if (string.IsNullOrEmpty(context.Request.QueryString["test02"]))
        //    {
        //        throw new Exception("test02 no data !!");
        //    }
        //    message = "test01 :" + context.Request.QueryString["test01"] + ", test02 :" + context.Request.QueryString["test02"];

        //    var jsonData = new ReturnJsonModel()
        //    {
        //        IsSussecc = true,
        //        Message = message
        //    };

        //    SetResponse(context, jsonData);

        //}
        //catch (Exception ex)
        //{
        //    var jsonData = new ReturnJsonModel()
        //    {
        //        IsSussecc = false,
        //        Message = ex.Message
        //    };
        //    SetResponse(context, jsonData);
        //}
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

}

