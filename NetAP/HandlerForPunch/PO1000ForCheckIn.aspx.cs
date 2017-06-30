using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SinoPac.WebExpress.Common;
using System.Net;
using System.IO;
using Newtonsoft.Json;

public partial class PO1000ForCheckIn : System.Web.UI.Page
{
    #region "1. 全域變數"
    /// <summary>
    /// _Paramodel
    /// </summary>
    private MsgParaModel _MsgParamodel //全域private變數要為('_'+'小駝峰')
    {
        get
        {
            try
            {
                if (ViewState["_MsgParamodel"] != null) //ViewState當頁暫存使用
                {
                    return JsonConvert.DeserializeObject<MsgParaModel>(ViewState["_MsgParamodel"].ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        set
        {
            ViewState["_MsgParamodel"] = JsonConvert.SerializeObject(value);
        }
    }

    /// <summary>
    /// SessionID(跨頁找正確的Session用)
    /// </summary>
    public string _SessionID
    {
        get
        {
            if (ViewState["_SessionID"] == null)
            {
                ViewState["_SessionID"] = string.Empty;
            }
            return (string)ViewState["_SessionID"];
        }
        set
        {
            ViewState["_SessionID"] = value;
        }
    }

    /// <summary>
    /// _SessionPunchModel
    /// </summary>
    private PunchModel _SessionPunchModel
    {
        //get
        //{
        //    if (Session["_SessionPunchModel"] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
        //    {
        //        Session["_SessionPunchModel"] = new PunchModel();
        //    }
        //    return JsonConvert.DeserializeObject<PunchModel>(Session["_SessionPunchModel"].ToString());
        //}
        //set
        //{
        //    Session["_SessionPunchModel"] = JsonConvert.SerializeObject(value);
        //}

        get
        {
            if (Session[_SessionID] == null) //Session當下瀏覽器暫存使用(用於跨頁但需要做好用完就clear的處理)
            {
                Session[_SessionID] = new PunchModel();
            }
            return JsonConvert.DeserializeObject<PunchModel>(Session[_SessionID].ToString());
        }
        set
        {
            Session[_SessionID] = JsonConvert.SerializeObject(value);
        }
    }
    #endregion

    #region "2. 功能鍵處理邏輯"
    #endregion

    #region "3. Override Method"

    #endregion

    #region "4. Page_Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        try
        {
            if (Request.QueryString["CompID"] != null && Request.QueryString["UserID"] != null)
            {
                //context.Request.QueryString["OTEmpID"]
                var strCompID = Request.QueryString["CompID"].ToString();
                var strUserID = Request.QueryString["UserID"].ToString();

                if (!UserInfo.Init(strUserID, true))
                {
                    throw new Exception(Util.getHtmlMessage(Util.HtmlMessageKind.Error, "Login Fail!"));
                }

                System.Web.Security.FormsAuthentication.SetAuthCookie(strUserID, false);
                LoadData();
            }
            else
            {
                throw new Exception("[CompID/UserID]參數不足！");
            }
        }
        catch (Exception ex)
        {
            labMsg.Text = "PO1000ForCheckIn:" + ex.Message;
        }
    }

    #endregion

    #region "5. 畫面事件"

    #endregion

    #region "6. 畫面檢核與確認"

    #endregion

    #region "7. private Method"

    private void LoadData()
    {
        bool hasFlow = false;                   //FlowOrgan
        bool hasReport = false;                 //是否有班表
        bool isSpecial = getPunchSpecial();     //是否為特殊單位
        String errorFlag = "";                  //是否打卡時間異常(早到晚退 T-上班,F-下班)
        String url = "";                        //目的網頁
        PunchModel report = new PunchModel();
        try
        {
            hasReport = getPunchReport(out report);
            if (!hasReport)
            {
                throw new Exception("查無班表，無法打卡!");
            }
            hasFlow = getEmpFlowOrgan(ref report);
            if (!hasFlow)
            {
                throw new Exception("查無FlowOrganID");
            }
            errorFlag = isPunchError(report);
            report.ErrorFlag = errorFlag;
            url = comBinMsg(report, isSpecial);
            if (!isSpecial && "".Equals(errorFlag))
            {
                DoDefine(report);
            }
            else
            {
                Response.Redirect(url);
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    //-------------------------------------------------------------主要邏輯區

    /// <summary>
    /// 確定打卡邏輯
    /// </summary>
    private void DoDefine(PunchModel model)
    {
        bool result = false;
        long seccessCount = 0;
        string msg = "";
        try
        {
            result = Punch.InsertPunchLog(model, out seccessCount, out msg);
            if (!result)
            {
                throw new Exception(msg);
            }
            if (seccessCount == 0)
            {
                throw new Exception("無資料被新增!!");
            }
        }
        catch (Exception ex)
        {
            Util.MsgBox(ex.Message);
        }
    }

    //-------------------------------------------------------------方法

    /// <summary>
    /// 特殊單位 
    /// </summary>
    /// <returns></returns>
    private bool getPunchSpecial()
    {
        bool result = false;
        bool isSuccess = false;
        var msg = "";
        var datas = new PunchBean();
        PunchModel viewData = new PunchModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            DeptID = UserInfo.getUserInfo().DeptID,
            OrganID = UserInfo.getUserInfo().OrganID,
        };
        isSuccess = Punch.GetPunchSpecial(viewData, out datas, out msg);
        if (isSuccess && datas != null)
        {
            if ("1".Equals(datas.SpecialFlag))
            {
                result = true;
            }
        }

        return result;
    }

    /// <summary>
    /// 取得班表，順序:值勤 > 個人 > 公司
    /// </summary>
    /// <param name="datas"></param>
    /// <returns></returns>
    private bool getPunchReport(out PunchModel datas)
    {
        bool result = true;
        datas = new PunchModel();

        if (!getReport("Duty", out datas))
        {
            if (!getReport("EmpWork", out datas))
            {
                if (!getReport("PersonOther", out datas))
                {
                    return false;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 取得執勤、個人、公司班表 
    /// </summary>
    /// <returns></returns>
    private bool getReport(string regStr, out PunchModel viewData)
    {
        bool result = false;
        string NowTime = DateTime.Now.ToString("HH:mm:ss.fff");
        var msg = "";
        var datas = new PunchBean();
        viewData = new PunchModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
            EmpID = UserInfo.getUserInfo().UserID,
            EmpName = UserInfo.getUserInfo().UserName,
            DeptID = UserInfo.getUserInfo().DeptID,
            DeptName = UserInfo.getUserInfo().DeptName,
            OrganID = UserInfo.getUserInfo().OrganID,
            OrganName = UserInfo.getUserInfo().OrganName,
            WorkTypeID = UserInfo.getUserInfo().WorkTypeID,
            WorkType = UserInfo.getUserInfo().WorkTypeName,
            Sex = UserInfo.getUserInfo().Sex,
            PunchFlag = "0",
            MAFT10_FLAG = "0",
            PunchDate = DateTime.Now.ToString("yyyy/MM/dd"),
            //PunchDate = "2017/05/02",
            PunchTime = NowTime,
            PunchTime4Count = NowTime.Replace(":", "").Substring(0, 4),
            LastChgComp = UserInfo.getUserInfo().CompID,
            LastChgID = UserInfo.getUserInfo().UserID

        };

        switch (regStr)
        {
            case "Duty":
                {
                    result = Punch.GetDutyReport(viewData, out datas, out msg);
                    break;
                }
            case "EmpWork":
                {
                    result = Punch.GetEmpWorkReport(viewData, out datas, out msg);
                    break;
                }
            case "PersonOther":
                {
                    result = Punch.GetPersonalOtherReport(viewData, out datas, out msg);
                    break;
                }
        }

        if (result && datas != null)
        {
            viewData.BeginTime = datas.BeginTime;
            viewData.EndTime = datas.EndTime;
            viewData.RestBeginTime = datas.RestBeginTime;
            viewData.RestEndTime = datas.RestEndTime;
        }

        return result;
    }

    /// <summary>
    /// 取得個人FlowOrganID
    /// </summary>
    /// <returns></returns>
    private bool getEmpFlowOrgan(ref PunchModel viewData)
    {
        bool result = false;
        var msg = "";
        var datas = new PunchBean();

        result = Punch.SelectEmpFlowOrgan(viewData, out datas, out msg);
        if (result && datas != null)
        {
            viewData.FlowOrganID = datas.FlowOrganID;
            viewData.FlowOrganName = datas.FlowOrganName;
        }
        else
        {
            throw new Exception("".Equals(msg) ? "查無資料" : msg);
        }
        return result;
    }

    /// <summary>
    /// 取得異常時間、提醒訊息
    /// </summary>
    /// <returns></returns>
    private bool getPunchPara(out ParaModel paraData, out MsgParaModel paraMsgData)
    {
        bool result = false;
        var msg = "";
        paraData = new ParaModel();
        paraMsgData = new MsgParaModel();

        var datas = new PunchParaBean();
        var viewData = new PunchParaModel()
        {
            CompID = UserInfo.getUserInfo().CompID,
        };

        result = Punch.GetPunchPara(viewData, out datas, out msg);
        if (result && datas != null)
        {
            paraData = Punch.paraFormat(datas.Para);
            paraMsgData = Punch.paraMsgFormat(datas.MsgPara);
        }
        else
        {
            throw new Exception("".Equals(msg) ? "查無資料" : msg);
        }
        return result;
    }

    /// <summary>
    /// 檢查打卡時間是否異常
    /// </summary>
    /// <param name="viewData"></param>
    /// <returns></returns>
    private string isPunchError(PunchModel viewData)
    {
        string result = "";
        bool hasPara = false;
        ParaModel paraTime = new ParaModel();
        MsgParaModel paraMsg = new MsgParaModel();
        try
        {
            hasPara = getPunchPara(out paraTime, out paraMsg);
            if (hasPara && paraMsg != null && paraTime != null)
            {
                int punchTime = Int32.Parse(viewData.PunchTime4Count);//要把時間:去掉，轉換格式，Model還要再多一個存取欄位
                //int punchTime = Int32.Parse("0630");
                int beginTime = Int32.Parse(viewData.BeginTime);
                int endTime = Int32.Parse(viewData.EndTime);
                int punchInBT = Int32.Parse(paraTime.PunchInBT);
                int punchOutBT = Int32.Parse(paraTime.PunchOutBT);
                int regularIn = beginTime - punchInBT;
                int regularOut = endTime + punchOutBT;

                if (punchTime < regularIn)
                {
                    result = "T";
                }
                if (punchTime > regularOut)
                {
                    result = "F";
                    if (punchTime >= 2200 && punchTime < 2400 || punchTime >= 0 && punchTime < 600)
                    {
                        result += "L";
                    }
                }
                _MsgParamodel = paraMsg;
            }
        }
        catch (Exception ex)
        {
            //Util.MsgBox(ex.Message);
            throw new Exception(ex.Message);
        }
        return result;
    }

    /// <summary>
    /// 組成訊息
    /// </summary>
    /// <returns></returns>
    private string comBinMsg(PunchModel model, bool specialFlag)
    {
        string result = "~/Punch/";

        if (!model.ErrorFlag.Equals(""))        //下班異常訊息 + 關懷訊息
        {
            if (model.ErrorFlag.StartsWith("F"))
            {
                string punchOutFlag = _MsgParamodel.PunchOutMsgFlag;
                model.RemindMsg = punchOutFlag.Equals("0") ? _MsgParamodel.PunchOutDefaultContent : _MsgParamodel.PunchOutSelfContent;
                if (model.ErrorFlag.Length == 2)
                {
                    string ovTenFlag = _MsgParamodel.OVTenMsgFlag;
                    model.CareMsg = ovTenFlag.Equals("0") ? _MsgParamodel.OVTenDefaultContent : _MsgParamodel.OVTenSelfContent;
                }
                if (model.Sex.Equals("2"))
                {
                    string femaleFlag = _MsgParamodel.FemaleMsgFlag;
                    string femaleMsg = femaleFlag.Equals("0") ? _MsgParamodel.FemaleDefaultContent : _MsgParamodel.FemaleSelfContent;
                    model.CareMsg = model.CareMsg.Substring(0, model.CareMsg.Length - 1) + femaleMsg;
                    model.MAFT10_FLAG = "1";
                }
            }
            else if ("T".Equals(model.ErrorFlag))   //上班異常訊息
            {
                string punchInFlag = _MsgParamodel.PunchInMsgFlag;
                model.RemindMsg = punchInFlag.Equals("0") ? _MsgParamodel.PunchInDefaultContent : _MsgParamodel.PunchInSelfContent;
            }

            string affairFlag = _MsgParamodel.AffairMsgFlag;
            model.RemindMsgAf = affairFlag.Equals("0") ? _MsgParamodel.AffairDefaultContent : _MsgParamodel.AffairSelfContent;
            result += specialFlag ? "PO1000_Special.aspx" : "PO1000_Error.aspx";
        }
        else if ("".Equals(model.ErrorFlag))    //正常上班
        {
            result += specialFlag ? "PO1000_Special.aspx" : "PO1000_Finish.aspx";
        }
        model.ResultMsg = "打卡成功";
        //串Key給下一頁尋找Session用
        string sessionId = model.EmpID + '_' + DateTime.Now.ToString("HH:mm:ss.fff");
        result += "?id=" + sessionId;
        _SessionID = sessionId;
        _SessionPunchModel = model;
        return result;
    }

    /// <summary>
    /// 將Model轉換為Json字串(沒用到)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string reverToJson(PunchModel model)
    {
        string result = "";
        if (model == null)
        {
            throw new Exception("");
        }
        result = JsonConvert.SerializeObject(model);
        return result;
    }

    #endregion

    
}