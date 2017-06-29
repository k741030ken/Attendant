using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Punch 的摘要描述
/// </summary>
public class Punch //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// 取得字串(去除null)
    /// </summary>
    private static string StringIIF(object str)
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

    /// <summary>
    /// 查詢最大PunchSeq
    /// </summary>
    private static string QueryMaxSeq(PunchModel viewData)
    {
        var result = "";
        var isSuccess = false;
        var msg = "";
        var datas = new PunchBean();

        isSuccess = GetMaxFormSeq(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            var seq = Int32.Parse(datas.PunchSeq) + 1;
            result = seq.ToString();
        }
        else
        {
            result = "1";
        }
        return result;
    }

    /// <summary>
    /// 取得PunchSeq
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetMaxFormSeq(PunchModel model, out PunchBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchBean dataBean = new PunchBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    PunchDate = model.PunchDate
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchLogMaxSeqSql(ref sb);
                try
                {
                    datas = conn.Query<PunchBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 取得DB資料
    /// 值勤班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetDutyReport(PunchModel model, out PunchBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchBean dataBean = new PunchBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    PunchDate = model.PunchDate
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectDutySql(ref sb);
                try
                {
                    datas = conn.Query<PunchBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 取得DB資料
    /// 個人班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetEmpWorkReport(PunchModel model, out PunchBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchBean dataBean = new PunchBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectEmpWorkSql(ref sb);
                try
                {
                    datas = conn.Query<PunchBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 取得DB資料
    /// 公司班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetPersonalOtherReport(PunchModel model, out PunchBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_eHRMSDB_ITRD).ConnectionString })
            {
                PunchBean dataBean = new PunchBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPersonalOtherSql(ref sb);
                try
                {
                    datas = conn.Query<PunchBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 取得DB資料
    /// 異常時間、提醒訊息Json字串
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetPunchPara(PunchParaModel model, out PunchParaBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchParaBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchParaBean dataBean = new PunchParaBean()
                {
                    CompID = model.CompID,
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchParaSql(ref sb);
                try
                {
                    datas = conn.Query<PunchParaBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 新增DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">新增筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool InsertPunchLog(PunchModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            PunchBean databean = new PunchBean()
            {
                CompID = StringIIF(model.CompID),
                EmpID = StringIIF(model.EmpID),
                EmpName = StringIIF(model.EmpName),
                PunchDate = StringIIF(model.PunchDate),
                PunchTime = StringIIF(model.PunchTime),
                PunchSeq = StringIIF(QueryMaxSeq(model)),
                DeptID = StringIIF(model.DeptID),
                DeptName = StringIIF(model.DeptName),
                OrganID = StringIIF(model.OrganID),
                OrganName = StringIIF(model.OrganName),
                Sex = StringIIF(model.Sex),
                PunchFlag = StringIIF(model.PunchFlag),
                WorkTypeID = StringIIF(model.WorkTypeID),
                WorkType = StringIIF(model.WorkType),
                MAFT10_FLAG = StringIIF(model.MAFT10_FLAG),
                AbnormalFlag = StringIIF(model.AbnormalFlag),
                AbnormalReasonID = StringIIF(model.AbnormalReasonID),
                AbnormalReasonCN = StringIIF(model.AbnormalReasonCN),
                AbnormalDesc = StringIIF(model.AbnormalDesc),
                BatchFlag = StringIIF("0"),
                Source = StringIIF("B"),
                APPContent = StringIIF(""),
                LastChgComp = StringIIF(model.LastChgComp),
                LastChgID = StringIIF(model.LastChgID)
            };

            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchLogSql(ref sb); //建立查詢SqlCommand
                List<PunchBean> newDataBean = new List<PunchBean>();
                //新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                try
                {
                    var count = conn.Query<PunchBean>(sb.ToString(), databean).Count(); //執行查詢，結果回傳至TestBean物件
                    if (count == 0)
                    {
                        newDataBean.Add(databean);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                if (newDataBean.Count > 0)
                {
                    SqlCommand.InsertPunchLogSql(ref sb, true); //建立新增SqlCommand
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            seccessCount = conn.Execute(sb.ToString(), newDataBean, trans); //執行新增，成功筆數回傳，並做Transaction機制
                            trans.Commit(); //成功Transaction直接Commit
                        }
                        catch (Exception)
                        {
                            trans.Rollback(); //失敗Transaction Rollback
                            throw;
                        }
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
    /// 取得DB資料
    /// 特殊單位
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetPunchSpecial(PunchModel model, out PunchBean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new PunchBean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                PunchBean dataBean = new PunchBean()
                {
                    CompID = model.CompID,
                    DeptID = model.DeptID,
                    OrganID = model.OrganID
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPunchSpecialUnitDefine(ref sb);
                try
                {
                    datas = conn.Query<PunchBean>(sb.ToString(), dataBean).FirstOrDefault();
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
    /// 將Json資料解析
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static ParaModel paraFormat(string jsonStr)
    {
        JArray jsAry = JsonConvert.DeserializeObject<JArray>(jsonStr);
        ParaModel paraData = new ParaModel();
        paraData.DutyInBT = jsAry[0]["DutyInBT"].ToString();
        paraData.DutyOutBT = jsAry[0]["DutyOutBT"].ToString();
        paraData.PunchInBT = jsAry[0]["PunchInBT"].ToString();
        paraData.PunchOutBT = jsAry[0]["PunchOutBT"].ToString();
        paraData.VisitOVBT = jsAry[0]["VisitOVBT"].ToString();
        return paraData;
    }

    /// <summary>
    /// 將Json資料解析
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static MsgParaModel paraMsgFormat(string jsonStr)
    {
        JArray jsAry = JsonConvert.DeserializeObject<JArray>(jsonStr);
        MsgParaModel paraMsgData = new MsgParaModel();
        paraMsgData.PunchInMsgFlag = jsAry[0]["PunchInMsgFlag"].ToString();
        paraMsgData.PunchInDefaultContent = jsAry[0]["PunchInDefaultContent"].ToString();
        paraMsgData.PunchInSelfContent = jsAry[0]["PunchInSelfContent"].ToString();
        paraMsgData.PunchOutMsgFlag = jsAry[0]["PunchOutMsgFlag"].ToString();
        paraMsgData.PunchOutDefaultContent = jsAry[0]["PunchOutDefaultContent"].ToString();
        paraMsgData.PunchOutSelfContent = jsAry[0]["PunchOutSelfContent"].ToString();
        paraMsgData.AffairMsgFlag = jsAry[0]["AffairMsgFlag"].ToString();
        paraMsgData.AffairDefaultContent = jsAry[0]["AffairDefaultContent"].ToString();
        paraMsgData.AffairSelfContent = jsAry[0]["AffairSelfContent"].ToString();
        paraMsgData.OVTenMsgFlag = jsAry[0]["OVTenMsgFlag"].ToString();
        paraMsgData.OVTenDefaultContent = jsAry[0]["OVTenDefaultContent"].ToString();
        paraMsgData.OVTenSelfContent = jsAry[0]["OVTenSelfContent"].ToString();
        paraMsgData.FemaleMsgFlag = jsAry[0]["FemaleMsgFlag"].ToString();
        paraMsgData.FemaleDefaultContent = jsAry[0]["FemaleDefaultContent"].ToString();
        paraMsgData.FemaleSelfContent = jsAry[0]["FemaleSelfContent"].ToString();
        return paraMsgData;
    }
}