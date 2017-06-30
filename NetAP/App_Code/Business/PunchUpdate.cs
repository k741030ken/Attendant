using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
/// <summary>
/// Punch 的摘要描述
/// </summary>
public class PunchUpdate //與交易同名
{
    private static string _attendantDBName = Aattendant._AattendantDBName;
    private static string _attendantFlowID = Aattendant._AattendantFlowID;
    private static string _eHRMSDB_ITRD = Aattendant._eHRMSDB_ITRD;

    #region"PunchUpdate_共用"
    public static void SelectValueDDL(DropDownList objDDL, String Value)
    {
        objDDL.SelectedIndex = objDDL.Items.IndexOf(objDDL.Items.FindByValue(Value));
    }

    public static List<Punch_Confirm_Remedy_Model> GridDataFormat(List<Punch_Confirm_Remedy_Bean> dbDataList)
    {
        var result = new List<Punch_Confirm_Remedy_Model>();
        foreach (var item in dbDataList)
        {
            var data = new Punch_Confirm_Remedy_Model();
            //Confirm

            data.CompID = string.IsNullOrEmpty(item.CompID) ? "" : item.CompID;
            data.EmpID = string.IsNullOrEmpty(item.EmpID) ? "" : item.EmpID;
            data.EmpName = string.IsNullOrEmpty(item.EmpName) ? "" : item.EmpName;
            data.DutyDate = string.IsNullOrEmpty(item.DutyDate) ? "" : item.DutyDate;
            data.DutyTime = string.IsNullOrEmpty(item.DutyTime) ? "" : item.DutyTime;
            data.PunchDate = string.IsNullOrEmpty(item.PunchDate) ? "" : item.PunchDate;
            data.PunchTime = string.IsNullOrEmpty(item.PunchTime) ? "" : item.PunchTime;
            data.PunchConfirmSeq = string.IsNullOrEmpty(item.PunchConfirmSeq) ? "" : item.PunchConfirmSeq;
            data.DeptID = string.IsNullOrEmpty(item.DeptID) ? "" : item.DeptID;
            data.DeptName = string.IsNullOrEmpty(item.DeptName) ? "" : item.DeptName;
            data.OrganID = string.IsNullOrEmpty(item.OrganID) ? "" : item.OrganID; ;
            data.OrganName = string.IsNullOrEmpty(item.OrganName) ? "" : item.OrganName;
            data.FlowOrganID = string.IsNullOrEmpty(item.FlowOrganID) ? "" : item.FlowOrganID;
            data.FlowOrganName = string.IsNullOrEmpty(item.FlowOrganName) ? "" : item.FlowOrganName;
            data.Sex = string.IsNullOrEmpty(item.Sex) ? "" : item.Sex;
            data.PunchFlag = string.IsNullOrEmpty(item.PunchFlag) ? "" : item.PunchFlag;
            data.WorkTypeID = string.IsNullOrEmpty(item.WorkTypeID) ? "" : item.WorkTypeID;
            data.WorkType = string.IsNullOrEmpty(item.WorkType) ? "" : item.WorkType;
            data.MAFT10_FLAG = string.IsNullOrEmpty(item.MAFT10_FLAG) ? "" : item.MAFT10_FLAG;
            data.ConfirmStatus = string.IsNullOrEmpty(item.ConfirmStatus) ? "" : item.ConfirmStatus;
            data.AbnormalType = string.IsNullOrEmpty(item.AbnormalType) ? "" : item.AbnormalType;
            data.ConfirmPunchFlag = string.IsNullOrEmpty(item.ConfirmPunchFlag) ? "" : item.ConfirmPunchFlag;
            data.PunchSeq = string.IsNullOrEmpty(item.PunchSeq) ? "" : item.PunchSeq;
            data.PunchRemedySeq = string.IsNullOrEmpty(item.PunchRemedySeq) ? "" : item.PunchRemedySeq;
            data.RemedyReasonID = string.IsNullOrEmpty(item.RemedyReasonID) ? "" : item.RemedyReasonID;
            data.RemedyReasonCN = string.IsNullOrEmpty(item.RemedyReasonCN) ? "" : item.RemedyReasonCN;
            data.RemedyPunchTime = string.IsNullOrEmpty(item.RemedyPunchTime) ? "" : item.RemedyPunchTime;
            data.AbnormalFlag = string.IsNullOrEmpty(item.AbnormalFlag) ? "" : item.AbnormalFlag;
            data.AbnormalReasonID = string.IsNullOrEmpty(item.AbnormalReasonID) ? "" : item.AbnormalReasonID;
            data.AbnormalReasonCN = string.IsNullOrEmpty(item.AbnormalReasonCN) ? "" : item.AbnormalReasonCN;
            data.AbnormalDesc = string.IsNullOrEmpty(item.AbnormalDesc) ? "" : item.AbnormalDesc;
            data.Remedy_AbnormalFlag = string.IsNullOrEmpty(item.Remedy_AbnormalFlag) ? "" : item.Remedy_AbnormalFlag;
            data.Remedy_AbnormalReasonID = string.IsNullOrEmpty(item.Remedy_AbnormalReasonID) ? "" : item.Remedy_AbnormalReasonID;
            data.Remedy_AbnormalReasonCN = string.IsNullOrEmpty(item.Remedy_AbnormalReasonCN) ? "" : item.Remedy_AbnormalReasonCN;
            data.Remedy_AbnormalDesc = string.IsNullOrEmpty(item.Remedy_AbnormalDesc) ? "" : item.Remedy_AbnormalDesc;
            data.Source = string.IsNullOrEmpty(item.Source) ? "" : item.Source;
            data.APPContent = string.IsNullOrEmpty(item.APPContent) ? "" : item.APPContent;
            data.LastChgComp = string.IsNullOrEmpty(item.LastChgComp) ? "" : item.LastChgComp;
            data.LastChgID = string.IsNullOrEmpty(item.LastChgID) ? "" : item.LastChgID;
            data.LastChgDate = string.IsNullOrEmpty(item.LastChgDate) ? "" : item.LastChgDate;

            //Confirm與Remedy沒有公用的欄位
            data.RemedyPunchFlag = string.IsNullOrEmpty(item.RemedyPunchFlag) ? "" : item.RemedyPunchFlag;
            data.BatchFlag = string.IsNullOrEmpty(item.BatchFlag) ? "" : item.BatchFlag;
            data.PORemedyStatus = string.IsNullOrEmpty(item.PORemedyStatus) ? "" : item.PORemedyStatus;
            data.RejectReason = string.IsNullOrEmpty(item.RejectReason) ? "" : item.RejectReason;
            data.RejectReasonCN = string.IsNullOrEmpty(item.RejectReasonCN) ? "" : item.RejectReasonCN;
            data.ValidDateTime = string.IsNullOrEmpty(item.ValidDateTime) ? "" : item.ValidDateTime;
            data.ValidCompID = string.IsNullOrEmpty(item.ValidCompID) ? "" : item.ValidCompID;
            data.ValidID = string.IsNullOrEmpty(item.ValidID) ? "" : item.ValidID;
            data.ValidName = string.IsNullOrEmpty(item.ValidName) ? "" : item.ValidName;
            data.Remedy_MAFT10_FLAG = string.IsNullOrEmpty(item.Remedy_MAFT10_FLAG) ? "" : item.Remedy_MAFT10_FLAG;

            //gridview顯示中文用
            data.ConfirmStatusGCN = string.IsNullOrEmpty(item.ConfirmStatusGCN) ? "" : item.ConfirmStatusGCN;
            data.ConfirmPunchFlagGCN = string.IsNullOrEmpty(item.ConfirmPunchFlagGCN) ? "" : item.ConfirmPunchFlagGCN;
            data.AbnormalReasonGCN = string.IsNullOrEmpty(item.AbnormalReasonGCN) ? "" : item.AbnormalReasonGCN;
            data.SourceGCN = string.IsNullOrEmpty(item.SourceGCN) ? "" : item.SourceGCN;
            data.SexGCN = string.IsNullOrEmpty(item.SexGCN) ? "" : item.SexGCN;

            //給預存PunchCheckData計算用資料
            data.SpecialFlag = string.IsNullOrEmpty(item.SpecialFlag) ? "" : item.SpecialFlag;
            data.RestBeginTime = string.IsNullOrEmpty(item.RestBeginTime) ? "" : item.RestBeginTime;
            data.RestEndTime = string.IsNullOrEmpty(item.RestEndTime) ? "" : item.RestEndTime;
            data.OtherPunchTime = string.IsNullOrEmpty(item.OtherPunchTime) ? "" : item.OtherPunchTime;

            //永豐流程FlowCaseID
            data.FlowCaseID = string.IsNullOrEmpty(item.FlowCaseID) ? "" : item.FlowCaseID;
            result.Add(data);
        }
        return result;
    }

    public static void GridViewToDictionary(GridView gv, out Dictionary<string, string> dic, int index, string strDataKeyNames)
    {
        dic = new Dictionary<string, string>();
        string[] strSplit = strDataKeyNames.Split(",");
        foreach (string str in strSplit)
        {
            dic.Add(str, gv.DataKeys[index].Values[str].ToString());
        }
    }
    #endregion"PunchUpdate_共用"

    #region"Dictionary&DataBean"
    public static Dictionary<string, string> Punch_Confirm_Remedy_BeanToDic(Punch_Confirm_Remedy_Bean Bean)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("CompID", Bean.CompID);
        dic.Add("EmpID", Bean.EmpID);
        dic.Add("EmpName", Bean.EmpName);
        dic.Add("DutyDate", Bean.DutyDate);
        dic.Add("DutyTime", Bean.DutyTime);
        dic.Add("PunchDate", Bean.PunchDate);
        dic.Add("PunchTime", Bean.PunchTime);
        dic.Add("DeptID", Bean.DeptID);
        dic.Add("DeptName", Bean.DeptName);
        dic.Add("OrganID", Bean.OrganID);
        dic.Add("OrganName", Bean.OrganName);
        dic.Add("FlowOrganID", Bean.FlowOrganID);
        dic.Add("FlowOrganName", Bean.FlowOrganName);
        dic.Add("MAFT10_FLAG", Bean.MAFT10_FLAG);
        dic.Add("PunchRemedySeq", Bean.PunchRemedySeq);
        dic.Add("RemedyReasonID", Bean.RemedyReasonID);
        dic.Add("RemedyReasonCN", Bean.RemedyReasonCN);
        dic.Add("RemedyPunchTime", Bean.RemedyPunchTime);
        dic.Add("AbnormalFlag", Bean.AbnormalFlag);
        dic.Add("AbnormalReasonID", Bean.AbnormalReasonID);
        dic.Add("AbnormalReasonCN", Bean.AbnormalReasonCN);
        dic.Add("AbnormalDesc", Bean.AbnormalDesc);
        dic.Add("Remedy_AbnormalFlag", Bean.Remedy_AbnormalFlag);
        dic.Add("Remedy_AbnormalReasonID", Bean.Remedy_AbnormalReasonID);
        dic.Add("Remedy_AbnormalReasonCN", Bean.Remedy_AbnormalReasonCN);
        dic.Add("Remedy_AbnormalDesc", Bean.Remedy_AbnormalDesc);
        dic.Add("LastChgComp", Bean.LastChgComp);
        dic.Add("LastChgID", Bean.LastChgID);
        dic.Add("LastChgDate", Bean.LastChgDate);
        //Remedy
        dic.Add("RemedyPunchFlag", Bean.RemedyPunchFlag);
        dic.Add("BatchFlag", Bean.BatchFlag);
        dic.Add("PORemedyStatus", Bean.PORemedyStatus);
        dic.Add("RejectReason", Bean.RejectReason);
        dic.Add("RejectReasonCN", Bean.RejectReasonCN);
        dic.Add("ValidDateTime", Bean.ValidDateTime);
        //dic.Add("ValidTime", Bean.ValidTime);
        dic.Add("ValidCompID", Bean.ValidCompID);
        dic.Add("ValidID", Bean.ValidID);
        dic.Add("ValidName", Bean.ValidName);
        dic.Add("FlowCaseID", Bean.FlowCaseID);
        return dic;
    }

    public static Punch_Confirm_Remedy_Bean Punch_Confirm_Remedy_DicToBean(Dictionary<string, string> dic)
    {
        Punch_Confirm_Remedy_Bean Bean = new Punch_Confirm_Remedy_Bean();
        Bean.CompID = dic["CompID"];
        Bean.EmpID = dic["EmpID"];
        Bean.EmpName = dic["EmpName"];
        Bean.DutyDate = dic["DutyDate"];
        Bean.DutyTime = dic["DutyTime"];
        Bean.PunchDate = dic["PunchDate"];
        Bean.PunchTime = dic["PunchTime"];
        Bean.PunchConfirmSeq = dic["PunchConfirmSeq"];
        Bean.DeptID = dic["DeptID"];
        Bean.DeptName = dic["DeptName"];
        Bean.OrganID = dic["OrganID"];
        Bean.OrganName = dic["OrganName"];
        Bean.FlowOrganID = dic["FlowOrganID"];
        Bean.FlowOrganName = dic["FlowOrganName"];
        Bean.Sex=dic["Sex"];
        Bean.ConfirmPunchFlag = dic["ConfirmPunchFlag"];
        Bean.MAFT10_FLAG = dic["MAFT10_FLAG"];
        Bean.PunchRemedySeq = dic["PunchRemedySeq"];
        Bean.RemedyReasonID = dic["RemedyReasonID"];
        Bean.RemedyReasonCN = dic["RemedyReasonCN"];
        Bean.RemedyPunchTime = dic["RemedyPunchTime"];
        Bean.AbnormalFlag = dic["AbnormalFlag"];
        Bean.AbnormalReasonID = dic["AbnormalReasonID"];
        Bean.AbnormalReasonCN = dic["AbnormalReasonCN"];
        Bean.AbnormalDesc = dic["AbnormalDesc"];
        Bean.Remedy_MAFT10_FLAG = dic["Remedy_MAFT10_FLAG"];
        Bean.Remedy_AbnormalFlag = dic["Remedy_AbnormalFlag"];
        Bean.Remedy_AbnormalReasonID = dic["Remedy_AbnormalReasonID"];
        Bean.Remedy_AbnormalReasonCN = dic["Remedy_AbnormalReasonCN"];
        Bean.Remedy_AbnormalDesc = dic["Remedy_AbnormalDesc"];
        Bean.LastChgComp = dic["LastChgComp"];
        Bean.LastChgID = dic["LastChgID"];
        Bean.LastChgDate = dic["LastChgDate"];
        //Remedy
        Bean.RemedyPunchFlag = dic["RemedyPunchFlag"];
        Bean.BatchFlag = dic["BatchFlag"];
        Bean.PORemedyStatus = dic["PORemedyStatus"];
        Bean.RejectReason = dic["RejectReason"];
        Bean.RejectReasonCN = dic["RejectReasonCN"];
        Bean.ValidDateTime = dic["ValidDateTime"];
        //Bean.ValidTime = dic["ValidTime"];
        Bean.ValidCompID = dic["ValidCompID"];
        Bean.ValidID = dic["ValidID"];
        Bean.ValidName = dic["ValidName"];
        Bean.FlowCaseID = dic["FlowCaseID"];
        Bean.AbnormalReasonGCN = dic["AbnormalReasonGCN"];
        Bean.ConfirmPunchFlagGCN = dic["ConfirmPunchFlagGCN"];
        Bean.ConfirmStatusGCN = dic["ConfirmStatusGCN"];
        Bean.SourceGCN = dic["SourceGCN"];
        return Bean;
    }
    #endregion"Dictionary&DataBean"

    #region"PunchUpdateInquire"
    /// <summary>
    /// 取得DB資料
    /// 個人班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool PunchUpdateInquire_DoQuery(Punch_Confirm_Remedy_Model model, out List<Punch_Confirm_Remedy_Bean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<Punch_Confirm_Remedy_Bean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                Punch_Confirm_Remedy_Bean dataBean = new Punch_Confirm_Remedy_Bean()
                {
                    EmpID = model.EmpID,
                    ConfirmStatus = model.ConfirmStatus,
                    AbnormalFlag = model.AbnormalFlag,
                    RemedyPunchFlag = model.RemedyPunchFlag,
                    ConfirmPunchFlag = model.ConfirmPunchFlag,
                    StartPunchDate = model.StartPunchDate,
                    EndPunchDate = model.EndPunchDate
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.Select_Punch_Confirm_Remedy(dataBean, ref sb, true);
                try
                {
                    datas = conn.Query<Punch_Confirm_Remedy_Bean>(sb.ToString(), dataBean).ToList();
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
    #endregion"PunchUpdateInquire"

    #region"PunchAppdOperation"
    /// <summary>
    /// 取得DB資料
    /// 個人班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool PunchAppdOperation_DoQuery(Punch_Confirm_Remedy_Model model, out List<Punch_Confirm_Remedy_Bean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<Punch_Confirm_Remedy_Bean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                Punch_Confirm_Remedy_Bean dataBean = new Punch_Confirm_Remedy_Bean()
                {
                    ValidCompID = model.ValidCompID,
                    ValidID = model.ValidID,
                    ConfirmStatus = model.ConfirmStatus
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.Select_Punch_Confirm_Remedy_OpenLog(dataBean, ref sb, true);
                try
                {
                    datas = conn.Query<Punch_Confirm_Remedy_Bean>(sb.ToString(), dataBean).ToList();
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
    /// 檢核所有東西，並將下一關相關資訊回傳
    /// </summary>
    public static bool nextAssignTo(Punch_Confirm_Remedy_Bean Bean, out Dictionary<string, string> toUserData)
    {
        string
        CompID = Bean.CompID,
        AssignTo = Bean.ValidID,
        OTStartDate = Bean.PunchDate,
        FlowCaseID = Bean.FlowCaseID;

        bool isLastFlow, nextIsLastFlow;
        string flowCode = "", flowSN = "", signLineDefine = "", meassge = "";

        string SignOrganID = "", SignID = "", SignIDComp = "";

        //讀取現在關卡與下一關相關資料，因為不論回傳是否，我還是要資料，所以沒檢核回傳值與錯誤訊息
        OBFlowUtility.QueryFlowDataAndToUserData(CompID, AssignTo, OTStartDate, FlowCaseID, "P",
out toUserData, out  flowCode, out  flowSN, out  signLineDefine, out  isLastFlow, out  nextIsLastFlow, out  meassge, "PO");

        //若是後台HR送簽依照填單人公司，否則用加班人公司
        //string HRLogCompID = signLineDefine == "4" || flowCode.Trim() == "" ?Bean.CompID :Bean.CompID;
        string HRLogCompID = Bean.CompID;
        //如果沒有下一關資料，則用現在關卡資料取代
        if (toUserData.Count == 0)
        {
            //取[最近的行政or功能]資料 取代 [現在關卡]資料
            DataTable dtHROtherFlowLog_toUD = PunchUpdate.HROtherFlowLog(FlowCaseID, true);
            toUserData.Add("SignLine", dtHROtherFlowLog_toUD.Rows[0]["SignLine"].ToString());
            toUserData.Add("SignIDComp", dtHROtherFlowLog_toUD.Rows[0]["SignIDComp"].ToString());
            toUserData.Add("SignID", AssignTo);
            toUserData.Add("SignOrganID", dtHROtherFlowLog_toUD.Rows[0]["SignOrganID"].ToString());
            toUserData.Add("SignFlowOrganID", dtHROtherFlowLog_toUD.Rows[0]["SignFlowOrganID"].ToString());
        }

        //如果下一關主管與現在主管相同，則再往上階找下一關主管資料
        if (toUserData["SignID"] == AssignTo && signLineDefine != "3")
        {
            switch (toUserData["SignLine"])
            {
                //HR線 或 行政線
                case "4":
                case "1":
                    if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = SignOrganID;
                        toUserData["SignFlowOrganID"] = "";
                    }
                    break;
                //功能線
                case "2":
                    if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                    {
                        toUserData["SignID"] = SignID;
                        toUserData["SignIDComp"] = SignIDComp;
                        toUserData["SignOrganID"] = "";
                        toUserData["SignFlowOrganID"] = SignOrganID;
                    }
                    break;

                //原本switch的是signLineDefine，現在改成toUserData["SignLine"]後，
                //case "3"裏頭的if基本只會用到else[非功能線一律走行政線]，以防萬一先保留。
                //改派
                case "3":
                    if (toUserData["SignLine"] == "2")
                    {
                        if (EmpInfo.QueryFlowOrganData(toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = "";
                            toUserData["SignFlowOrganID"] = SignOrganID;
                        }
                    }
                    else
                    {
                        if (EmpInfo.QueryOrganData(HRLogCompID, toUserData["SignOrganID"], Bean.PunchDate, out SignOrganID, out SignID, out SignIDComp))
                        {
                            toUserData["SignID"] = SignID;
                            toUserData["SignIDComp"] = SignIDComp;
                            toUserData["SignOrganID"] = SignOrganID;
                            toUserData["SignFlowOrganID"] = "";
                        }
                    }
                    break;
            }
        }

        //如果找不到下一關主管資料，彈跳視窗並且return false
        if (toUserData["SignID"] == "")
        {
            toUserData["SignIDComp"] = UserInfo.getUserInfo().CompID.Trim();
            toUserData["SignID"] = UserInfo.getUserInfo().UserID.Trim();
            //Util.MsgBox("查無下一關主管資料");
            if (isLastFlow) //最後一關不用找下一關主管
                return true;
            else
                return false;
        }
        return true;
    }

    public static DataTable HROtherFlowLog(string FlowCaseID, bool notSP)
    {
        DbHelper db = new DbHelper(_attendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Reset();
        if (notSP)
            sb.Append(" select Top 1 * from HROtherFlowLog where FlowCaseID='" + FlowCaseID + "'  and SignLine in ('1','2','4') order by Seq desc");
        else
            sb.Append(" select Top 1 * from HROtherFlowLog where FlowCaseID='" + FlowCaseID + "'  order by Seq desc");
        return db.ExecuteDataSet(sb.BuildCommand()).Tables[0];
    }

    #endregion"PunchAppdOperation"

    #region"PunchUpdateModify_ATCodeMap"
    /// <summary>
    /// 取得DB資料
    /// 個人班表
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool Select_AT_CodeMap(AT_CodeMap_Model model, out List<AT_CodeMap_Bean> datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new List<AT_CodeMap_Bean>();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                AT_CodeMap_Bean dataBean = new AT_CodeMap_Bean()
                {
                    TabName = model.TabName,
                    FldName = model.FldName
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.Select_AT_CodeMap(dataBean, ref sb, true);
                try
                {
                    datas = conn.Query<AT_CodeMap_Bean>(sb.ToString(), dataBean).ToList();
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

    public static List<AT_CodeMap_Model> GridDataFormat(List<AT_CodeMap_Bean> dbDataList)
    {
        var result = new List<AT_CodeMap_Model>();
        foreach (var item in dbDataList)
        {
            var data = new AT_CodeMap_Model();
            data.TabName = item.TabName;
            data.FldName = item.FldName;
            data.Code = item.Code;
            data.CodeCName = item.CodeCName;
            data.SortFld = item.SortFld;
            data.NotShowFlag = item.NotShowFlag;
            data.LastChgComp = item.LastChgComp;
            data.LastChgID = item.LastChgID;
            data.LastChgDate = item.LastChgDate;
            result.Add(data);
        }
        return result;
    }
    #endregion"PunchUpdateModify"

    #region"PunchUpdateModify_SaveData"
    /// <summary>
    /// 申請後送簽流程
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool PunchUpdateModify_SaveData(Punch_Confirm_Remedy_Bean model, out long seccessCount, out string msg)
    {
        bool result = false;
        seccessCount = 0;
        msg = "";

        bool validResult = false;
        Punch_Confirm_Remedy_Bean datas = new Punch_Confirm_Remedy_Bean();
        string validMsg = "";
        try
        {
            Dictionary<string, string> empData = new Dictionary<string, string>();
            Dictionary<string, string> toUserData = new Dictionary<string, string>();
            Dictionary<string, string> ValidUserData = new Dictionary<string, string>();
            string flowCode = "";
            string flowSN = "";
            bool nextIsLastFlow = false;
            string meassge = "";
            FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_PunchUpdate/"));
            string KeyValue = "";//字串
            string ShowValue = "";
            string strFlowCaseID = "";

            DbHelper db = new DbHelper(Aattendant._AattendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();

            Punch_Confirm_Remedy_Bean dataBean = new Punch_Confirm_Remedy_Bean()
            {
                FlowCaseID = model.FlowCaseID,
                CompID = model.CompID,
                EmpID = model.EmpID,
                EmpName = model.EmpName,
                DutyDate = model.DutyDate,
                DutyTime = model.DutyTime,
                PunchDate = model.PunchDate,
                PunchTime = model.PunchTime,
                PunchConfirmSeq = model.PunchConfirmSeq,
                DeptID = model.DeptID,
                DeptName = model.DeptName,
                OrganID = model.OrganID,
                OrganName = model.OrganName,
                FlowOrganID = model.FlowOrganID,
                FlowOrganName = model.FlowOrganName,
                MAFT10_FLAG = model.MAFT10_FLAG,
                ConfirmStatus = model.ConfirmStatus,
                PunchRemedySeq = model.PunchRemedySeq,
                RemedyReasonID = model.RemedyReasonID,
                RemedyReasonCN = model.RemedyReasonCN,
                RemedyPunchTime = model.RemedyPunchTime,
                AbnormalFlag = model.AbnormalFlag,
                AbnormalReasonID = model.AbnormalReasonID,
                AbnormalReasonCN = model.AbnormalReasonCN,
                AbnormalDesc = model.AbnormalDesc,
                Remedy_MAFT10_FLAG=model.Remedy_MAFT10_FLAG,
                Remedy_AbnormalFlag = model.Remedy_AbnormalFlag,
                Remedy_AbnormalReasonID = model.Remedy_AbnormalReasonID,
                Remedy_AbnormalReasonCN = model.Remedy_AbnormalReasonCN,
                Remedy_AbnormalDesc = model.Remedy_AbnormalReasonID == "99" ? model.Remedy_AbnormalDesc : "",
                LastChgComp = model.LastChgComp,
                LastChgID = model.LastChgID,
                LastChgDate = model.LastChgDate,
                //Remedy
                RemedyPunchFlag = model.RemedyPunchFlag,
                BatchFlag = model.BatchFlag,
                PORemedyStatus = model.PORemedyStatus,
                RejectReason = model.RejectReason,
                RejectReasonCN = model.RejectReasonCN,
                ValidDateTime = model.ValidDateTime,
                //ValidTime = model.ValidTime,
                ValidCompID = model.ValidCompID,
                ValidID = model.ValidID,
                ValidName = model.ValidName
            };

            OBFlowUtility.QueryFlowDataAndToUserData_First(dataBean.CompID, dataBean.OrganID, dataBean.FlowOrganID, dataBean.EmpID, dataBean.EmpID, dataBean.PunchDate, out empData, out toUserData, out flowCode, out flowSN, out nextIsLastFlow, out meassge, "PO01","PO");

            Punch_Confirm_Remedy_Model validInfo = new Punch_Confirm_Remedy_Model()
            {
                CompID = toUserData["SignIDComp"],
                EmpID = toUserData["SignID"]
            };
            validResult = GetValidInfo(validInfo, out datas, out validMsg);
            if (!validResult)
            {
                throw new Exception(validMsg);
            }
            dataBean.ValidCompID = toUserData["SignIDComp"];
            dataBean.ValidID = toUserData["SignID"];
            dataBean.ValidName = datas.ValidName;

            KeyValue = dataBean.CompID + "," + dataBean.EmpID + "," + dataBean.PunchDate + "," + dataBean.PunchRemedySeq;
            ShowValue = dataBean.CompID + "," + dataBean.EmpID + "," + dataBean.PunchDate + "," + dataBean.PunchTime;
            ValidUserData = CustVerify.getEmpID_Name_Dictionary(toUserData["SignID"], toUserData["SignIDComp"]);

            if (FlowExpress.IsFlowInsVerify(flow.FlowID, KeyValue.Split(","), ShowValue.Split(","), nextIsLastFlow ? "btnSendLast" : "btnSend", ValidUserData, ""))
            {
                strFlowCaseID = FlowExpress.getFlowCaseID(flow.FlowID, KeyValue);
                dataBean.FlowCaseID = strFlowCaseID;
                SqlCommand.PunchUpdateModify_InsertPunchRemedyLog(dataBean, ref sb);
                OBFlowUtility.ChangeFlowFlag(strFlowCaseID, flowCode, flowSN, dataBean.ValidCompID, dataBean.ValidID, "1", ref sb);
                OBFlowUtility.InsertHROtherFlowLogCommand(strFlowCaseID, "1", strFlowCaseID + "." + "1".PadLeft(5, '0'), "P", dataBean.EmpID, dataBean.OrganID,
                    dataBean.FlowOrganID, dataBean.ValidID, flowCode, flowSN, "1", "1", toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "1", false, ref sb);

                try
                {
                    //================
                    //sb.Append("錯誤製造");
                    //=================
                    seccessCount = db.ExecuteNonQuery(sb.BuildCommand(), tx); //執行新增，成功筆數回傳，並做Transaction機制
                    tx.Commit(); //成功Transaction直接Commit
                }
                catch (Exception)
                {
                    FlowExpress.IsFlowCaseDeleted(flow.FlowID, strFlowCaseID, true); //送簽用IsFlowCaseDeleted //審核用IsFlowRollBack
                    tx.Rollback(); //失敗Transaction Rollback
                    throw;
                }
                result = true;
            }
        }
        catch (Exception ex)
        {
            msg = ex.Message;
        }
        return result;
    }

    /// <summary>
    /// 取得簽核人員資料
    /// </summary>
    /// <param name="model">畫面model</param>
    /// <param name="datas">回傳資料</param>
    /// <param name="msg">回傳訊息</param>
    /// <returns>bool</returns>
    public static bool GetValidInfo(Punch_Confirm_Remedy_Model model, out Punch_Confirm_Remedy_Bean datas, out string msg)
    {
        bool result = false;
        msg = "";
        datas = new Punch_Confirm_Remedy_Bean();
        try
        {
            using (var conn = new SqlConnection() { ConnectionString = DbHelper.getConnectionStrings(_attendantDBName).ConnectionString })
            {
                Punch_Confirm_Remedy_Bean dataBean = new Punch_Confirm_Remedy_Bean()
                {
                    CompID = model.CompID,
                    EmpID = model.EmpID,
                };
                StringBuilder sb = new StringBuilder();
                SqlCommand.SelectPersonValid(ref sb);
                try
                {
                    datas = conn.Query<Punch_Confirm_Remedy_Bean>(sb.ToString(), dataBean).FirstOrDefault();
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
    #endregion"PunchUpdateModify_SaveData"

    #region"PunchAppdOperation_SaveData"
    public static bool PunchAppdOperation_SaveData(Punch_Confirm_Remedy_Bean model, Dictionary<string, string> oAssDic, string btnName, string FlowStepOpinion, out long seccessCount, out string msg, string flowCode = "", string flowSN = "", string flowSeq = "", string FlowLogBatNo = "", string FlowLogID = "", Dictionary<string, string> toUserData = null)
    {
        seccessCount = 0;
        msg = "";
        Punch_Confirm_Remedy_Bean datas = new Punch_Confirm_Remedy_Bean();
        try
        {
            FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_PunchUpdate/"), model.FlowCaseID, false);
            string strFlowLogID = flow.FlowCurrLastLogID;
            //string strFlowLogID=PunchUpdate.getFlowLogID(Util.getAppSetting("app://AattendantDB_PunchUpdate/"), model.FlowCaseID);
            //FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_PunchUpdate/"), model.FlowCaseID);
            //FlowExpress flow = new FlowExpress(Util.getAppSetting("app://AattendantDB_PunchUpdate/"), strFlowLogID);
            DbHelper db = new DbHelper(Aattendant._AattendantDBName);
            CommandHelper sb = db.CreateCommandHelper();
            DbConnection cn = db.OpenConnection();
            DbTransaction tx = cn.BeginTransaction();

            Punch_Confirm_Remedy_Bean dataBean = new Punch_Confirm_Remedy_Bean()
            {
                FlowCaseID = model.FlowCaseID,
                CompID = model.CompID,
                EmpID = model.EmpID,
                EmpName = model.EmpName,
                DutyDate = model.DutyDate,
                DutyTime = model.DutyTime,
                PunchDate = model.PunchDate,
                PunchTime = model.PunchTime,
                PunchConfirmSeq = model.PunchConfirmSeq,
                DeptID = model.DeptID,
                DeptName = model.DeptName,
                OrganID = model.OrganID,
                OrganName = model.OrganName,
                FlowOrganID = model.FlowOrganID,
                FlowOrganName = model.FlowOrganName,
                ConfirmStatus = model.ConfirmStatus,
                PunchRemedySeq = model.PunchRemedySeq,
                RemedyReasonID = model.RemedyReasonID,
                RemedyReasonCN = model.RemedyReasonCN,
                RemedyPunchTime = model.RemedyPunchTime,
                AbnormalFlag = model.AbnormalFlag,
                AbnormalReasonID = model.AbnormalReasonID,
                AbnormalReasonCN = model.AbnormalReasonCN,
                AbnormalDesc = model.AbnormalDesc,

                Remedy_MAFT10_FLAG = model.Remedy_MAFT10_FLAG,
                Remedy_AbnormalFlag = model.Remedy_AbnormalFlag,
                Remedy_AbnormalReasonID = model.Remedy_AbnormalReasonID,
                Remedy_AbnormalReasonCN = model.Remedy_AbnormalReasonCN,
                Remedy_AbnormalDesc = model.Remedy_AbnormalDesc,
                LastChgComp = model.LastChgComp,
                LastChgID = model.LastChgID,
                LastChgDate = model.LastChgDate,
                //Remedy
                RemedyPunchFlag = model.RemedyPunchFlag,
                BatchFlag = model.BatchFlag,
                PORemedyStatus = model.PORemedyStatus,
                RejectReason = model.RejectReason,
                RejectReasonCN = model.RejectReasonCN,
                ValidDateTime = model.ValidDateTime,
                ValidCompID = model.ValidCompID,
                ValidID = model.ValidID,
                ValidName = model.ValidName
            };
            try
            {
                SqlCommand.PunchAppdOperation_UpdatePunchRemedyLog(dataBean, ref sb);
                SqlCommand.PunchAppdOperation_UpdatePunchConfirm(dataBean, ref sb); //內部判定結案or駁回
                SqlCommand.PunchAppdOperation_UpdateHROtherFlowLog(dataBean.FlowCaseID, dataBean.ConfirmStatus == "4" ? "3" : "2", ref sb);

                if (dataBean.PORemedyStatus == "2")//沒結案(送簽中)
                {
                    OBFlowUtility.InsertHROtherFlowLogCommand(dataBean.FlowCaseID, CustVerify.addone(FlowLogBatNo), CustVerify.FlowLogIDadd(FlowLogID), "P", dataBean.EmpID, dataBean.OrganID, dataBean.FlowOrganID, UserInfo.getUserInfo().UserID, flowCode, flowSN, CustVerify.addone(flowSeq), toUserData["SignLine"], toUserData["SignIDComp"], toUserData["SignID"], toUserData["SignOrganID"], toUserData["SignFlowOrganID"], "1", false, ref sb);
                }
                else if (dataBean.PORemedyStatus == "3" || dataBean.PORemedyStatus == "4")//結案
                {
                    FlowUtility.ChangeFlowFlag(dataBean.FlowCaseID, flowCode, flowSN, UserInfo.getUserInfo().CompID, UserInfo.getUserInfo().UserID, "0", ref sb);
                }
                //======(test錯誤的存在)=======
                //sb.Append("test錯誤的存在");
                msg = "注定失敗";
                return false;
                //======(test錯誤的存在)=======
                seccessCount = db.ExecuteNonQuery(sb.BuildCommand(), tx); //執行新增，成功筆數回傳，並做Transaction機制
                if (seccessCount > 0)
                {
                    if (FlowExpress.IsFlowVerify(flow.FlowID, flow.FlowCurrLastLogID, btnName, oAssDic, FlowStepOpinion))
                    {
                        tx.Commit(); //成功Transaction直接Commit
                    }
                    else
                    {
                        tx.Rollback(); //失敗Transaction Rollback
                        msg = "永豐流程寫入失敗";
                    }
                }
                else
                {
                    tx.Rollback(); //失敗Transaction Rollback
                    msg = "審核失敗，更新0筆資料";
                }
            }
            catch (Exception ex)
            {
                tx.Rollback(); //失敗Transaction Rollback
                msg = ex.Message;
                throw ex;
            }
        }
        catch (Exception ex)
        {
            //tx.Rollback(); //失敗Transaction Rollback
            msg = ex.Message;
            return false;
        }
        return true;
    }

    public static bool PunchAppdOperation_EXEC_PunchCheckData(Punch_Confirm_Remedy_Bean model, out string ConfirmStatus, out string AbnormalType)
    {
        ConfirmStatus = "";
        AbnormalType = "";
        try
        {
            DbHelper db = new DbHelper(_attendantDBName);
            DbCommand dbcmd;
            dbcmd = db.GetSqlStringCommand("PunchCheckData");
            dbcmd.CommandType = CommandType.StoredProcedure;
            dbcmd.Parameters.Add(new SqlParameter("@CompID", model.CompID));
            dbcmd.Parameters.Add(new SqlParameter("@EmpID", model.EmpID));
            dbcmd.Parameters.Add(new SqlParameter("@PunchDate", model.PunchDate));
            dbcmd.Parameters.Add(new SqlParameter("@PunchTime", model.PunchTime));
            dbcmd.Parameters.Add(new SqlParameter("@PunchType", model.ConfirmPunchFlag));
            dbcmd.Parameters.Add(new SqlParameter("@OtherPunchTime", model.PunchTime));
            dbcmd.Parameters.Add(new SqlParameter("@DutyTime", model.DutyTime));
            dbcmd.Parameters.Add(new SqlParameter("@RestBeginTime", model.RestBeginTime));
            dbcmd.Parameters.Add(new SqlParameter("@RestEndTime", model.RestEndTime));
            SqlParameter SqlConfirmStatus = new SqlParameter("@ConfirmStatus", SqlDbType.Char,1, ParameterDirection.Output, false, 1, 0, "", DataRowVersion.Current, 0);
            dbcmd.Parameters.Add(SqlConfirmStatus);
            SqlParameter SqlAbnormalType = new SqlParameter("@AbnormalType", SqlDbType.Char, 1, ParameterDirection.Output, false, 1, 0, "", DataRowVersion.Current, 0);
            dbcmd.Parameters.Add(SqlAbnormalType);
            db.ExecuteNonQuery(dbcmd);
            ConfirmStatus = SqlConfirmStatus.Value.ToString();
            AbnormalType = SqlAbnormalType.Value.ToString();
        }
        catch (Exception ex)
        {
            ConfirmStatus = "";
            AbnormalType = "";
            return false;
        }
        return true;
    }

    public class DBParameterHelp
    {
        //StoredProcedure 傳入參數
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            //dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }
        //StoredProcedure傳出參數
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }
    }

    public static string getFlowLogID(string FlowCustDB, string FlowCaseID)
    {
        DbHelper db = new DbHelper(Aattendant._AattendantDBName);
        CommandHelper sb = db.CreateCommandHelper();
        sb.Append(" select FlowLogID from " + FlowCustDB + "FlowFullLog ");
        sb.Append(" WHERE FlowCaseID =").AppendParameter("FlowCaseID", FlowCaseID);
        sb.Append(" Order by FlowLogID desc ");
        return db.ExecuteScalar(sb.BuildCommand()).ToString();
    }
    #endregion"PunchAppdOperation_SaveData"
}