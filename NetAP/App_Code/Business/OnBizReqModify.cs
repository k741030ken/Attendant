using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SinoPac.WebExpress.DAO;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;

/// <summary>
/// OnBizReqModify 的摘要描述
/// </summary>
public class OnBizReqModify //與交易同名
{
    private static string _attendantDBName = "AattendantDB";
    private static string _attendantFlowID = "AattendantDB";
    private static string _eHRMSDB_ITRD = "eHRMSDB";

    /// <summary>
    /// UpdateVisitForm
    /// 公出修改
    /// 更新DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">新增筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool UpdateVisitForm(OnBizPublicOutModel model, out long seccessCount, out string msg)
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
                    VisitFormNo = model.OBVisitFormNo,
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
                    BeginTime = model.OBBeginTimeH + ":" + model.OBBeginTimeM,
                    VisitEndDate = model.OBVisitEndDate,
                    EndTime = model.OBEndTimeH + ":" + model.OBEndTimeM,
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
                    LastChgDate = model.OBLastChgDate
                };

            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.UpdateVisitFormSql(ref sb, true); //建立新增SqlCommand
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            seccessCount = conn.Execute(sb.ToString(), databean, trans); //執行新增，成功筆數回傳，並做Transaction機制
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

    

}