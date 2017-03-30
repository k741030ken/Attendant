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
/// TestLogic 的摘要描述
/// </summary>
public class Template //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    /// <summary>
    /// 取得DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetEmpFlowSNEmpAndSexUseDapper(TemplateModel model, out List<TemplateBean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<TemplateBean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                TemplateBean dataBean = new TemplateBean()
                {
                    CompID = model.OTCompID,
                    EmpID = model.OTEmpID,
                    NameN = model.NameN,
                    Sex = model.Sex
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.GetEmpFlowSNEmpAndSexSql(dataBean, ref sb);
                try
                {
                    datas = conn.Query<TemplateBean>(sb.ToString(), dataBean).ToList();
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
    /// 新增DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">新增筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool InsertEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111", LastChgComp = "ZZZZZZ", LastChgID = "111111", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222", LastChgComp = "ZZZZZZ", LastChgID = "222222", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectTemplateSql(ref sb); //建立查詢SqlCommand
                List<TemplateBean> newDataBean = new List<TemplateBean>();
                //新增前檢查資料庫是否有重複PK，沒有放進待新增容器
                foreach(var item in dataBean)
                {
                    try
                    {
                        var count = conn.Query<TemplateBean>(sb.ToString(), item).Count(); //執行查詢，結果回傳至TestBean物件
                        if (count == 0)
                        {
                            newDataBean.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                if (newDataBean.Count > 0)
                {
                    SqlCommand.InsertTemplateSql(ref sb, true); //建立新增SqlCommand
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
    /// 刪除DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">刪除筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool DeleteEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111"},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222"}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.DeleteTemplateSql(ref sb); //建立刪除SqlCommand
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans); //執行刪除，成功筆數回傳，並做Transaction機制
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
    /// 修改DB資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="seccessCount">變更筆數</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool UpdateEmpFlowSNEmp(TemplateModel model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";
        try
        {
            //測試資料
            List<TemplateBean> dataBean = new List<TemplateBean>()
            {
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "111111", LastChgComp = "ZZZZZZ", LastChgID = "111111", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                new TemplateBean(){CompID = "ZZZZZZ", EmpID = "222222", LastChgComp = "ZZZZZZ", LastChgID = "222222", LastChgDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}
            };
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                conn.Open();
                StringBuilder sb = new StringBuilder();
                SqlCommand.UpdateTemplateSql(ref sb); //建立修改SqlCommand
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        seccessCount = conn.Execute(sb.ToString(), dataBean, trans); //執行修改，成功筆數回傳，並做Transaction機制
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
    /// 將DB資料轉成作格式化
    /// </summary>
    /// <param name="dbDataList">DB資料</param>
    /// <returns>格式化後資料</returns>
    public static List<TemplateGridData> DataFormat(List<TemplateBean> dbDataList)
    {
        var result = new List<TemplateGridData>();
        foreach (var item in dbDataList)
        {
            var data = new TemplateGridData();
            data.OTCompID = item.CompID;
            data.OTEmpID = item.EmpID;
            data.Sex = item.Sex;
            data.NameN = item.NameN;
            data.ShowOTEmp = item.EmpID + item.NameN;
            switch (item.Sex)
            {
                case "1": { data.ShowSex = "男"; break; }
                case "2": { data.ShowSex = "女"; break; } 
            }
            result.Add(data);
        }
        return result;
    }

}