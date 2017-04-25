using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Http;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using RS = SinoPac.WebExpress.Common.Properties;


//=== Web API 範例 2017.03.31 Andrew ===
// ASP.Net Web API 介紹 http://blog.kkbruce.net/2012/04/aspnet-web-api-2-rest.html
// RESTful 設計原則所指的 [POST] [GET] [PUT] [DELETE] 方法，正好對應到資料庫的 CRUD (Create, Read, Update, Delete) 操作
// 可在 Chrome 瀏覽器下安裝 [Restlet Client] 擴充元件，方便進行 API 測試

/// <summary>
/// [UserInfo]Controller
/// </summary>
public class UserInfoController : ApiController
{
    /// <summary>
    /// 取得 UserInfo
    /// </summary>
    public void Get()
    {
        throw new Exception("Syntax Error !  Usage : [/api/UserInfo/{UserID}]");
    }

    /// <summary>
    /// 取得指定 UserID 的 UserInfo 資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public UserInfo GetUserInfo(string id)
    {
        if (!Regex.IsMatch(id, "^.{6}$"))
        {
            throw new Exception(string.Format(RS.Resources.Msg_NotValidLength1, 6)); //長度需為 6 字元
        }
        else
        {
            UserInfo oUser = UserInfo.findUser(id.ToUpper());
            if (oUser != null && !string.IsNullOrEmpty(oUser.UserID))
                return oUser;
            else
                throw new Exception(string.Format(RS.Resources.Msg_DataNotFound1, id)); //無 [id] 相關資料！            
        }
    }
}


/// <summary>
/// [BullMsg]Controller
/// <remarks>** 示範如何不用 ORM (Entity Frameowrk) 的情況下進行實作 **</remarks>
/// </summary>
public class BullMsgController : ApiController
{
    //資料庫名稱
    string _DBName = "NetSample";
    //資料表名稱
    string _TableName = "BullMsg";

    /// <summary>
    /// 取得公告(單筆/最多 MaxQty 筆)
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public DataTable GetBullMsg()
    {
        //由於 MsgID (例：201703.00001) 資料型別會被誤判，故以 QueryString 方式判斷，例: http://localhost:55020/api/BullMsg/?MsgID=201703.00032
        //可用的 QueryString 選擇性參數如下：

        // MsgID       =>  公告ID             例: [201703.00001]
        // IsEnabled   =>  是否啟用           例: [Y]
        // StartDate1  =>  起始日期1          例: [2017/03/01]
        // StartDate2  =>  起始日期2          例: [2017/03/10]
        // EndDate1    =>  截止日期1          例: [2017/03/01]
        // EndDate2    =>  截止日期2          例: [2017/03/10]
        // MaxQty      =>  資料最大筆數       例: [100]
        // SortExp     =>  資料排序運算式     例: [MsgID Desc]

        Dictionary<string, string> oQry = Util.getRequestQueryString();
        string qryMsgID = Util.getRequestQueryStringKey("MsgID");
        string qryEnabled = Util.getRequestQueryStringKey("IsEnabled", "Y", true); //預設 [Y]
        string qryStartDate1 = oQry.IsNullOrEmpty("StartDate1") ? "" : string.Format("{0} 00:00:00", oQry["StartDate1"]); //轉成全天
        string qryStartDate2 = oQry.IsNullOrEmpty("StartDate2") ? "" : string.Format("{0} 23:59:59", oQry["StartDate2"]); //轉成全天
        string qryEndDate1 = oQry.IsNullOrEmpty("EndDate1") ? "" : string.Format("{0} 00:00:00", oQry["EndDate1"]); //轉成全天
        string qryEndDate2 = oQry.IsNullOrEmpty("EndDate2") ? "" : string.Format("{0} 23:59:59", oQry["EndDate2"]); //轉成全天
        string qrySortExp = oQry.IsNullOrEmpty("SortExp") ? "" : string.Format(" Order By {0} ", oQry["SortExp"]);

        string strQrySQL = string.Format("Select Top {1} * From {0} Where 0 = 0 ", _TableName, Util.getRequestQueryStringKey("MaxQty", "100")); //預設取前 100 筆
        strQrySQL += Util.getDataQueryConditionSQL("MsgID", "=", qryMsgID);
        strQrySQL += Util.getDataQueryConditionSQL("IsEnabled", "=", qryEnabled);
        strQrySQL += Util.getDataQueryConditionSQL("StartDateTime", "Between", qryStartDate1, qryStartDate2);
        strQrySQL += Util.getDataQueryConditionSQL("EndDateTime", "Between", qryEndDate1, qryEndDate2);
        strQrySQL += qrySortExp;

        DbHelper db = new DbHelper(_DBName);
        return db.ExecuteDataSet(strQrySQL).Tables[0];
    }

    /// <summary>
    /// 刪除一筆公告
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    public bool DeleteBullMsg()
    {
        //由於 MsgID (例：201703.00001) 資料型別會被誤判，故以 QueryString 方式判斷，例: http://localhost:55020/api/BullMsg/?MsgID=201703.00032
        bool oResult = false;
        Dictionary<string, string> dicQry = Util.getRequestQueryString(); //取得 QueryString 內容
        if (!dicQry.IsNullOrEmpty())
        {
            string strErrMsg = null;
            if (!Util.IsDataDeleted(_DBName, _TableName, dicQry, out strErrMsg))
            {
                throw new Exception(strErrMsg); //資料更新錯誤
            }
            oResult = true;
        }
        else
        {
            throw new Exception(RS.Resources.Msg_ParaNotEmpty); //參數不可為空白
        }
        return oResult;
    }

    /// <summary>
    /// 新增一筆公告(新增成功時會傳回 MsgID，其餘都觸發例外處理)
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Dictionary<string, string> InsertBullMsg()
    {
        Dictionary<string, string> dicForm = Util.getRequestForm(); //取得 Form 內容
        if (!dicForm.IsNullOrEmpty())
        {
            string strMsgID = Util.getAppKey(_DBName, _TableName).Item1[0]; //產生新公告的 MsgID
            string strErrMsg = null;
            dicForm.Add("MsgID", strMsgID);
            if (!Util.IsDataInserted(_DBName, _TableName, dicForm, out strErrMsg))
            {
                throw new Exception(strErrMsg); //資料新增錯誤
            }
            return new Dictionary<string, string>() { { "MsgID", strMsgID } }; //成功時傳回 MsgID 的值
        }
        else
        {
            throw new Exception(RS.Resources.Msg_ParaNotEmpty); //參數不可為空白
        }
    }

    /// <summary>
    /// 更新一筆公告(可更新 全部/部份 欄位)
    /// </summary>
    /// <returns></returns>
    [HttpPut]
    public bool UpdateBullMsg()
    {
        bool oResult = false;
        Dictionary<string, string> dicForm = Util.getRequestForm(); //取得 Form 內容

        if (!dicForm.IsNullOrEmpty())
        {
            string strErrMsg = null;
            if (!Util.IsDataUpdated(_DBName, _TableName, dicForm, out strErrMsg))
            {
                throw new Exception(strErrMsg); //資料更新錯誤
            }
            oResult = true;
        }
        else
        {
            throw new Exception(RS.Resources.Msg_ParaNotEmpty); //參數不可為空白
        }

        return oResult;
    }
}
