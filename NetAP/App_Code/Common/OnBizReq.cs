using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Text;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using SinoPac.WebExpress.Common.Properties;

/// <summary>
/// OnBizReq 的摘要描述
/// </summary>
public class OnBizReq 
{
    private static string _attendantDBName = "AattendantDB";
    private static string _attendantFlowID = "AattendantDB";
    private static string _eHRMSDB_ITRD = "eHRMSDB";

    //--------------------------------------------------------------------------------------------------------------------------For : 資料格式化
    /// <summary>
    /// WorkSiteFormat
    /// 公出申請、公出修改
    /// 將工作地點資料作格式處理
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<PersonGridData> WorkSiteFormat(List<OnBizPublicOutBean> dbDataList)
    {
        var result = new List<PersonGridData>();
        foreach (var item in dbDataList)
        {
            var data = new PersonGridData();
            data.OBInterLocationID = item.InterLocationID;
            data.OBInterLocationName = item.InterLocationID + " " + item.InterLocationName;
            //data.OBInterLocationName = item.InterLocationName;
            result.Add(data);
        }
        return result;
    }

    /// <summary>
    /// ValidFormat
    /// 公出申請、公出修改
    /// 將簽核主管資料作格式處理
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<PersonGridData> ValidFormat(List<DropDownListModel> dbDataList)
    {
        var result = new List<PersonGridData>();
        foreach (var item in dbDataList)
        {
            var data = new PersonGridData();
            data.EmpID = item.DataValue;
            data.NameN = item.DataText;
            data.Emp_NameN = item.DataValue + " " + item.DataText;
            result.Add(data);
        }
        return result;
    }

    /// <summary>
    /// GridDataFormat
    /// 公出查詢
    /// 將Grid資料作格式化
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<gridDataModel> GridDataFormat(List<OnBizPublicOutBean> dbDataList)
    {
        var result = new List<gridDataModel>();
        foreach (var item in dbDataList)
        {
            var data = new gridDataModel();
            data.CompID = item.CompID;
            data.EmpID = item.EmpID;
            data.OBWriteDate = item.WriteDate;
            data.OBFormSeq = item.FormSeq;
            data.OBFormStatus = item.OBFormStatus;
            data.OBFormStatusName = item.OBFormStatusName;
            data.FlowCaseID = item.FlowCaseID;
            data.FlowLogID = item.FlowLogID;
            data.ValidID = item.ValidID;
            data.ValidID_Name = item.ValidID.Equals("") ? "" : item.ValidID + "-" + item.ValidName;
            data.EmpID_NameN = item.EmpID + "-" + item.EmpNameN;
            data.DeputyID_Name = item.DeputyID + "-" + item.DeputyName;
            data.VisitBeginDate = item.VisitBeginDate;
            data.BeginTime = item.BeginTime;
            data.VisitEndDate = item.VisitEndDate;
            data.EndTime = item.EndTime;
            data.VisitReasonCN = item.VisitReasonCN;
            result.Add(data);
        }
        return result;
    }

    /// <summary>
    /// DetailFormat
    /// 公出修改、公出明細
    /// 將DB資料轉成ViewModel
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static OnBizPublicOutModel DetailFormat(OnBizPublicOutBean dbDataList)
    {
        var result = new OnBizPublicOutModel()
        {
            CompID = dbDataList.CompID,
            CompName = dbDataList.CompName,
            CompID_Name = dbDataList.CompID + " " + dbDataList.CompName,
            EmpID = dbDataList.EmpID,
            EmpNameN = dbDataList.EmpNameN,
            EmpID_Name = dbDataList.EmpID + " " + dbDataList.EmpNameN,
            OBWriteDate = dbDataList.WriteDate,
            OBWriterID = dbDataList.WriterID,
            OBWriterName = dbDataList.WriterName,
            OBWriterID_Name = dbDataList.WriterID + " " + dbDataList.WriterName,
            OBFormSeq = dbDataList.FormSeq,
            OBVisitFormNo = dbDataList.VisitFormNo,
            OBOrganID = dbDataList.OrganID,
            OBOrganName = dbDataList.OrganName,
            OBTitleID = dbDataList.TitleID,
            OBTitleName = dbDataList.TitleName,
            OBPositionID = dbDataList.PositionID,
            OBPosition = dbDataList.Position,
            OBTel_1 = dbDataList.Tel_1,
            OBTel_2 = dbDataList.Tel_2,
            OBVisitBeginDate = dbDataList.VisitBeginDate,
            OBBeginTime = dbDataList.BeginTime,
            OBBeginTimeH = dbDataList.BeginTime.Split(":")[0],
            OBBeginTimeM = dbDataList.BeginTime.Split(":")[1],
            OBVisitEndDate = dbDataList.VisitBeginDate,
            OBEndTime = dbDataList.EndTime,
            OBEndTimeH = dbDataList.EndTime.Split(":")[0],
            OBEndTimeM = dbDataList.EndTime.Split(":")[1],
            OBDeputyID = dbDataList.DeputyID,
            OBDeputyName = dbDataList.DeputyName,
            OBLocationType = dbDataList.LocationType,
            OBInterLocationID = dbDataList.InterLocationID,
            OBInterLocationName = dbDataList.InterLocationName,
            OBExterLocationName = dbDataList.ExterLocationName,
            OBVisiterName = dbDataList.VisiterName,
            OBVisiterTel = dbDataList.VisiterTel,
            OBVisitReasonID = dbDataList.VisitReasonID,
            OBVisitReasonCN = dbDataList.VisitReasonCN,
            OBVisitReasonDesc = dbDataList.VisitReasonDesc,
            OBLastChgComp = dbDataList.LastChgComp,
            OBLastChgCompName = dbDataList.LastChgCompName,
            OBLastChgCompID_Name = dbDataList.LastChgComp + " " + dbDataList.LastChgCompName,
            OBLastChgID = dbDataList.LastChgID,
            OBLastChgName = dbDataList.LastChgName,
            OBLastChgID_Name = dbDataList.LastChgID + " " + dbDataList.LastChgName,
            OBLastChgDate = dbDataList.LastChgDate
        };

        return result;
    }

    /// <summary>
    /// EmpInfoFormat
    /// 公出修改、公出申請
    /// 將DB資料轉成ViewModel
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static OnBizPublicOutModel EmpInfoFormat(OnBizPublicOutBean dbDataList)
    {
        var result = new OnBizPublicOutModel()
        {
            CompID = dbDataList.CompID,
            CompName = dbDataList.CompName,
            EmpID = dbDataList.EmpID,
            EmpNameN = dbDataList.EmpNameN,
            OBDeptID = dbDataList.DeptID,
            OBDeptName = dbDataList.DeptName,
            OBOrganID = dbDataList.OrganID,
            OBOrganName = dbDataList.OrganName,
            OBWorkTypeID = dbDataList.WorkTypeID,
            OBWorkType = dbDataList.WorkType,
            OBFlowOrganID = dbDataList.FlowOrganID,
            OBFlowOrganName = dbDataList.FlowOrganName,
            OBTitleID = dbDataList.TitleID,
            OBTitleName = dbDataList.TitleName,
            OBPositionID = dbDataList.PositionID,
            OBPosition = dbDataList.Position,
        };

        return result;
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出查詢

    /// <summary>
    /// DeleteVisitForm
    /// 公出查詢
    /// 暫存後刪除流程
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool DeleteVisitForm(List<OnBizPublicOutModel> model, out long seccessCount, out string msg)
    {
        bool result = false;
        msg = "";
        seccessCount = 0;
        List<OnBizPublicOutBean> datas = new List<OnBizPublicOutBean>();
        try
        {
            foreach (OnBizPublicOutModel item in model)
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean();
                dataBean.CompID = item.CompID;
                dataBean.EmpID = item.EmpID;
                dataBean.WriteDate = item.OBWriteDate;
                dataBean.FormSeq = item.OBFormSeq;
                dataBean.LastChgComp = item.OBLastChgComp;
                dataBean.LastChgID = item.OBLastChgID;
                dataBean.LastChgDate = item.OBLastChgDate;
                datas.Add(dataBean);
            }
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.DeleteVisitFormSql(ref sb); //建立刪除SqlCommand
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = conn.Execute(sb.ToString(), datas, trans); //執行刪除，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        throw;
                    }
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// CancelVisitForm
    /// 公出查詢
    /// 送簽後取消流程
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool CancelVisitForm(List<OnBizPublicOutModel> model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            Dictionary<string, string> ValidUserData = new Dictionary<string, string>();
            FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OnBiz/"));
            DbHelper db = new DbHelper(_attendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            string flowCaseIDForRollBack = "";
            try
            {
                using (var trans = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (OnBizPublicOutModel item in model)
                        {
                            OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                            {
                                CompID = item.CompID,
                                EmpID = item.EmpID,
                                WriteDate = item.OBWriteDate,
                                FormSeq = item.OBFormSeq,
                                FlowCaseID = item.FlowCaseID,
                                FlowLogID = item.FlowLogID,
                                ValidID = item.ValidID,
                                LastChgComp = item.OBLastChgComp,
                                LastChgID = item.OBLastChgID,
                                LastChgDate = item.OBLastChgDate
                            };

                            ValidUserData = CustVerify.getEmpID_Name_Dictionary(dataBean.ValidID, dataBean.CompID);

                            if (FlowExpress.IsFlowVerify(flow.FlowID, dataBean.FlowLogID, "btnCancel", ValidUserData, "取消"))
                            {
                                sb.Reset();
                                flowCaseIDForRollBack += dataBean.FlowCaseID + ",";
                                OBFlowUtility.ChangeFlowFlag(dataBean.FlowCaseID, "OB01", "0001", dataBean.CompID, dataBean.ValidID, "0", ref sb);
                                SqlCommand.CancelVisitFormSql(dataBean, ref sb);
                                SqlCommand.UpdateHROtherFlowLogSql(dataBean, ref sb);
                            }
                        }
                        seccessCount = db.ExecuteNonQuery(sb.BuildCommand(), trans); //執行新增，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit

                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        string[] flowCaseIDAry = flowCaseIDForRollBack.Split(",");
                        for (int i = 0; i < flowCaseIDAry.Length - 1; i++)
                        {
                            FlowExpress.IsFlowRollBack(flow.FlowID, flowCaseIDAry[i]);
                        }
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// LogoffVisitForm
    /// 公出查詢
    /// 核准後註銷流程
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool LogoffVisitForm(List<OnBizPublicOutModel> model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";

        bool flowFlag = false;
        string flowLogNo = "";
        string flowMsg = "";
        int sflowLogNo = 0;
        try
        {
            Dictionary<string, string> ValidUserData = new Dictionary<string, string>();
            FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OnBiz/"));
            DbHelper db = new DbHelper(_attendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();

            try
            {
                using (var trans = cn.BeginTransaction())
                {
                    try
                    {
                        foreach (OnBizPublicOutModel item in model)
                        {
                            OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                            {
                                CompID = item.CompID,
                                EmpID = item.EmpID,
                                WriterID = item.OBWriterID,
                                WriterName = item.OBWriterName,
                                DeptID = item.OBDeptID,
                                DeptName = item.OBDeptName,
                                WriteDate = item.OBWriteDate,
                                FormSeq = item.OBFormSeq,
                                FlowCaseID = item.FlowCaseID,
                                LastChgComp = item.OBLastChgComp,
                                LastChgID = item.OBLastChgID,
                            };

                            flowFlag = SelectOnBizReqAppd_ITRDFlowFullLog(dataBean.FlowCaseID, out flowLogNo, out flowMsg);
                            if (!flowFlag)
                            {
                                throw new Exception("找不到FlowLogBatNo的資料");
                            }
                            else
                            {
                                sflowLogNo = Int32.Parse(flowLogNo) + 1;
                                flowLogNo = sflowLogNo.ToString();
                            }
                            SqlCommand.LogoffVisitFormSql(dataBean, ref sb);
                            SqlCommand.UpdateHROtherFlowLogSql(dataBean, ref sb);
                            SqlCommand.UpdateOnBizReqAppd_ITRDFlowFullLogSql(dataBean, ref sb);
                            SqlCommand.UpdateOnBizReqAppd_ITRDFlowCaseSql(dataBean, ref sb);
                            SqlCommand.InsertOnBizReqAppd_ITRDFlowFullLogSql(dataBean, flowLogNo, ref sb);
                        }
                        seccessCount = db.ExecuteNonQuery(sb.BuildCommand(), trans); //執行新增，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectOnBizReqAppd_ITRDFlowFullLog
    /// 公出查詢
    /// 取得FlowLogBatNo資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectOnBizReqAppd_ITRDFlowFullLog(string flowCaseID, out string flowLogBatNo, out string msg)
    {
        bool result = false;
        flowLogBatNo = "";
        msg = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    FlowCaseID = flowCaseID
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectOnBizReqAppd_ITRDFlowFullLogSql(ref sb);
                try
                {
                    flowLogBatNo = conn.Query<string>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (flowLogBatNo == null)
                {
                    throw new Exception("查無資料!");
                }

            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出申請、公出修改

    /// <summary>
    /// SelectSupervisor
    /// 公出申請、公出修改
    /// 取得主管人員姓名
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static string SelectSupervisor(string compID,string empID)
    {
        string name = "";
        OnBizPublicOutBean datas = new OnBizPublicOutBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    CompID = compID,
                    EmpID = empID
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectSupervisorSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null)
                {
                    throw new Exception("查無資料!");
                }
                else
                {
                    name = datas.EmpNameN;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return name;
    }

    /// <summary>
    /// SelectWorkSite
    /// 公出申請、公出修改
    /// 取得公司人員資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectPerson(OnBizPublicOutModel model, out OnBizPublicOutBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new OnBizPublicOutBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPersonSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null)
                {
                    throw new Exception("查無資料!");
                }

            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectWorkSite
    /// 公出申請、公出修改
    /// 取得所有內部單位工作地點
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectWorkSite(OnBizPublicOutModel model, out List<OnBizPublicOutBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<OnBizPublicOutBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings("eHRMSDB").ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    CompID = model.CompID,
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectWorkSiteSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), dataBean).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null || datas.Count == 0)
                {
                    throw new Exception("查無資料!");
                }

            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectAT_CodeMap
    /// 公出申請、公出修改
    /// 取得ReasonCN下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectAT_CodeMap(out List<DropDownListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListModel>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {

                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectAT_CodeMapSql("On_Business", "VisitReasonCN", ref sb);
                try
                {
                    datas = conn.Query<DropDownListModel>(sb.ToString()).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectEmpFlowValid
    /// 公出申請、公出修改
    /// 取得最小簽核主管下拉選單
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectEmpFlowValid(OnBizPublicOutModel model,out List<DropDownListModel> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<DropDownListModel>();
        try
        {
            OnBizPublicOutBean dataBean = new OnBizPublicOutBean() 
            {
                CompID = model.CompID,
                EmpID = model.EmpID
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {

                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectEmpFlowValidSql(ref sb);
                try
                {
                    datas = conn.Query<DropDownListModel>(sb.ToString(), dataBean).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectPersonMail
    /// 公出申請、公出修改
    /// 取得公出人員、職務代理人、審核主管的EMail
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectPersonMail(OnBizPublicOutBean model, string regStr,out string EMail)
    {
        OnBizPublicOutBean datas = new OnBizPublicOutBean();
        bool result = false;
        EMail = "";
        try
        {
            datas.CompID = model.CompID;
            switch (regStr)
                {
                    case "EmpID": { datas.EmpID = datas.EmpID; break; }
                    case "DeputyID": { datas.EmpID = datas.DeputyID; break; }
                    case "ValidID": { datas.EmpID = datas.ValidID; break; }
                } 
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {

                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPersonMailSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), model).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas != null)
                {
                    EMail = datas.EmpID_EMail;
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            //msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// SelectHRMail
    /// 公出申請、公出修改
    /// 取得HR所有人的的EMail
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static void SelectHRMail(string CompID,out string EMail)
    {
        List<OnBizPublicOutBean> datas = new List<OnBizPublicOutBean>();
        EMail = "";
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {

                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectHRMailSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), datas).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas != null || datas.Count == 0)
                {
                    foreach (var item in datas)
                    {
                        EMail += item.EmpID_EMail;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //msg = ex.Message;
        }
    }

    /// <summary>
    /// Insert_UpdateVisitFormSign
    /// 公出申請、公出修改
    /// 申請後送簽流程
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool Insert_UpdateVisitFormSign(OnBizPublicOutModel model, string regStr, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            Dictionary<string, string> ValidUserData = new Dictionary<string, string>();
            FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_OnBiz/"));
            string KeyValue = "";//字串
            string[] strarray = null;
            string strFlowCaseID = "";
            Dictionary<string, string> empData = new Dictionary<string,string>();
            Dictionary<string, string> toUserData = new Dictionary<string,string>();
            string flowCode = "";
            string flowSN = "";
            bool nextIsLastFlow = false;
            string meassge = "";

            DbHelper db = new DbHelper(_attendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();

            OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
            {
                CompID = model.CompID,
                oldCompID = model.oldCompID,
                EmpID = model.EmpID,
                oldEmpID = model.oldEmpID,
                EmpNameN = model.EmpNameN,
                WriteDate = model.OBWriteDate,
                WriteTime = model.OBWriteTime,
                WriterID = model.OBWriterID,
                oldWriteDate = model.oldOBWriteDate,
                WriterName = model.OBWriterName,
                FormSeq = model.OBFormSeq,
                oldFormSeq = model.oldOBFormSeq,
                FlowCaseID = "",
                TransactionSeq = "",
                OBFormStatus = "2",
                ValidDate = "",
                ValidID = "",
                ValidName = "",
                RejectReasonID = "",
                RejectReasonCN = "",
                DeptID = model.OBDeptID,
                DeptName = model.OBDeptName,
                OrganID = model.OBOrganID,
                OrganName = model.OBOrganName,
                WorkTypeID = model.OBWorkTypeID,
                WorkType = model.OBWorkType,
                FlowOrganID = model.OBFlowOrganID,
                FlowOrganName = model.OBFlowOrganName,
                TitleID = model.OBTitleID,
                TitleName = model.OBTitleName,
                PositionID = model.OBPositionID,
                Position = model.OBPosition,
                Tel_1 = model.OBTel_1,
                Tel_2 = model.OBTel_2,
                VisitBeginDate = model.OBVisitBeginDate,
                BeginTime = model.OBBeginTime,
                VisitEndDate = model.OBVisitEndDate,
                EndTime = model.OBEndTime,
                DeputyID = model.OBDeputyID,
                DeputyName = model.OBDeputyName,
                LocationType = model.OBLocationType,
                InterLocationID = model.OBInterLocationID,
                InterLocationName = model.OBInterLocationName,
                ExterLocationName = model.OBExterLocationName,
                VisiterName = model.OBVisiterName,
                VisiterTel = model.OBVisiterTel,
                VisitReasonID = model.OBVisitReasonID,
                VisitReasonCN = model.OBVisitReasonCN,
                VisitReasonDesc = model.OBVisitReasonDesc,
                LastChgComp = model.OBLastChgComp,
                LastChgID = model.OBLastChgID,
            };
            OBFlowUtility.QueryFlowDataAndToUserData_First(dataBean.CompID, dataBean.OrganID, dataBean.FlowOrganID, dataBean.EmpID, dataBean.WriterID, dataBean.VisitBeginDate,
                out empData, out toUserData, out flowCode, out flowSN, out nextIsLastFlow, out meassge);

            dataBean.ValidID = toUserData["SignID"].ToString();
            dataBean.ValidName = SelectSupervisor(toUserData["SignIDComp"], toUserData["SignID"]);

            KeyValue = dataBean.CompID + "," + dataBean.EmpID + "," + dataBean.WriteDate + "," + dataBean.FormSeq;
            strarray = (dataBean.EmpID + "," + dataBean.EmpNameN + "," + dataBean.VisitBeginDate + "," + dataBean.BeginTime + "," + dataBean.EndTime).Split(",");
            ValidUserData = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);  
            if (FlowExpress.IsFlowInsVerify(flow.FlowID, KeyValue.Split(","), strarray, "btnBefore", ValidUserData, ""))
            {
                strFlowCaseID = FlowExpress.getFlowCaseID(flow.FlowID, KeyValue);
                dataBean.FlowCaseID = strFlowCaseID;
                switch (regStr)
                {
                    case "Addes": { SqlCommand.InesrtVisitFormSendSql(dataBean, ref sb); break; }
                    case "Modify": { SqlCommand.UpdateVisitFormSend(dataBean, ref sb); break; }
                }
                OBFlowUtility.InsertHROtherFlowLogCommand(strFlowCaseID, "1", strFlowCaseID + "1".PadLeft(5, '0'), "B", dataBean.EmpID, dataBean.OrganID, dataBean.FlowOrganID, dataBean.WriterID,
                    flowCode, flowSN, "1", "1", toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "1", false, ref sb);

                sendEMail(dataBean, ref sb);
            }
            try
            {
                using (var trans = cn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = db.ExecuteNonQuery(sb.BuildCommand(), trans); //執行新增，成功筆數回傳，並做Transaction機制
                        trans.Commit(); //成功Transaction直接Commit
                    }
                    catch (Exception)
                    {
                        trans.Rollback(); //失敗Transaction Rollback
                        FlowExpress.IsFlowRollBack(flow.FlowID, dataBean.FlowCaseID);
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 公出修改、公出明細

    /// <summary>
    /// SelectOnlyVisitForm
    /// 公出修改、公出明細
    /// 取得公出個人資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectOnlyVisitForm(OnBizPublicOutModel model, out OnBizPublicOutBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new OnBizPublicOutBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                OnBizPublicOutBean dataBean = new OnBizPublicOutBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    WriteDate = model.OBWriteDate,
                    FormSeq = model.OBFormSeq
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectOnlyVisitFormSql(ref sb);
                try
                {
                    datas = conn.Query<OnBizPublicOutBean>(sb.ToString(), dataBean).FirstOrDefault();
                }
                catch (Exception)
                {
                    throw;
                }
                if (datas == null)
                {
                    throw new Exception("查無資料!");
                }

            }
            result = true;
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    //--------------------------------------------------------------------------------------------------------------------------For : 共用方法

    /// <summary>
    /// 日期算相差月數與日數
    /// </summary>
    /// <param name="strFrom"></param>
    /// <param name="strTo"></param>
    /// <param name="strType">D-天,M-月</param>
    /// <returns></returns>
    public static int GetTimeDiff(string SDateStr, string EDateStr, string DateType)
    {
        DateTime dtStart = DateTime.Parse(SDateStr);
        DateTime dtEnd = DateTime.Parse(EDateStr);

        if (DateType == "Day")
        {
            //使用TimeSpan提供的Days屬性
            TimeSpan ts = (dtEnd - dtStart);
            int iDays = ts.Days + 1;
            return iDays;
        }
        else if (DateType == "Month")
        {
            int iMonths = dtEnd.Year * 12 + dtEnd.Month - (dtStart.Year * 12 + dtStart.Month) + 1;
            return iMonths;
        }
        else if (DateType == "Minute")
        {
            TimeSpan ts = (dtEnd - dtStart);
            return (int)ts.TotalMinutes;
        }
        else return 0;
    }

    /// <summary>
    /// 寄EMail Log
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    private static void sendEMail(OnBizPublicOutBean datas,ref CommandHelper sb)
    {
        string Subject_1 = "";
        string Content_1 = "";
        string[] regArray = {"EmpID","DeputyID","ValidID"};

        for (int i = 0; i < regArray.Length; i++)
        {
            string mail = "";
            if (!SelectPersonMail(datas, regArray[i], out mail))
            {
                SelectHRMail(datas.CompID, out mail);
                Subject_1 = "系統查無通知者E-mail";
                if ("ValidID".Equals(regArray[i]))
                {
                    Content_1 = "OnBizForValid||BM@SenderName||" + datas.ValidID + "-" + datas.ValidName + "||BM@EmpID||" + datas.EmpID + "||BM@EmpName||" + datas.EmpNameN;
                }
                else
                {
                    Content_1 = "OnBizForEmp||BM@SenderName||" + datas.ValidID + "-" + datas.ValidName + "||BM@EmpID||" + datas.EmpID + "||BM@EmpName||" + datas.EmpNameN;
                }
            }
            else
            {
                Subject_1 = "【公出-待簽核通知】"+datas.OrganName+"(部門)-"+datas.EmpNameN+"(公出人)公出申請待簽核";
                if ("ValidID".Equals(regArray[i]))
                {
                    Content_1 = "OnBizForValid||BM@SenderName||" + datas.ValidID + "-" + datas.ValidName + "||BM@EmpID||" + datas.EmpID + "||BM@EmpName||" + datas.EmpNameN;
                }
                else
                {
                    Content_1 = "OnBizForEmp||BM@SenderName||" + datas.ValidID + "-" + datas.ValidName + "||BM@EmpID||" + datas.EmpID + "||BM@EmpName||" + datas.EmpNameN;
                }
                
            }
            Aattendant.InsertMailLogCommand("人力資源處", datas.CompID, datas.ValidID, mail, "", "", Subject_1, Content_1, false, ref sb);
        }
    }
}