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

/// <summary>
/// OnBizReqAddes 的摘要描述
/// </summary>
public class OnBizReqAddes //與交易同名
{
    private static string _attendantDBName = "AattendantDB";
    private static string _attendantFlowID = "AattendantDB";
    private static string _eHRMSDB_ITRD = "eHRMSDB";

    /// <summary>
    /// 公出申請
    /// 新增DB資料，暫存檔案
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">新增筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool InsertVisitForm(OnBizPublicOutModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            var databean = new OnBizPublicOutBean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                    EmpNameN = model.EmpNameN,
                    WriteDate = model.OBWriteDate,
                    WriteTime = model.OBWriteTime,
                    WriterID = model.OBWriterID,
                    WriterName = model.OBWriterName,
                    FormSeq = model.OBFormSeq,
                    FlowCaseID = "",
                    TransactionSeq = "",
                    OBFormStatus = "1",
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

            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectSameKeySql(databean, ref sb); //建立查詢SqlCommand
                List<OnBizPublicOutBean> newDataBean = new List<OnBizPublicOutBean>();
                //新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                    try
                    {
                        var count = conn.Query<OnBizPublicOutBean>(sb.ToString(), databean).Count(); //執行查詢，結果回傳至TestBean物件
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
                    SqlCommand.InsertVisitFormSql(ref sb, true); //建立新增SqlCommand
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
    /// 公出申請
    /// 取得VisitForm最大的FormSeq
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool SelectVisitFormMaxSeq(OnBizPublicOutModel model, out OnBizPublicOutBean datas, out string msg)
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
                    WriterID = model.OBWriterID,
                    WriteDate = model.OBWriteDate
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectVisitFormMaxSeqSql(ref sb);
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

}