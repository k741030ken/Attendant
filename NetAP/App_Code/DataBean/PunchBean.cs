using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class PunchBean //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// PunchSeq
    /// </summary>
    public String PunchSeq { get; set; }

    /// <summary>
    /// 打卡人公司ID
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 部門ID
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 部門名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 單位ID
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 單位名稱
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 單位ID
    /// </summary>
    public String FlowOrganID { get; set; }

    /// <summary>
    /// 單位名稱
    /// </summary>
    public String FlowOrganName { get; set; }

    /// <summary>
    /// 特殊單位
    /// </summary>
    public String SpecialFlag { get; set; }

    /// <summary>
    /// 打卡人ID
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 打卡人姓名
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 打卡類型。0:出退勤打卡，1:午休打卡。
    /// </summary>
    public String PunchFlag { get; set; }

    /// <summary>
    /// 工作性質代碼
    /// </summary>
    public String WorkTypeID { get; set; }

    /// <summary>
    /// 工作性質
    /// </summary>
    public String WorkType { get; set; }

    /// <summary>
    /// 女性十點後打卡註記
    /// </summary>
    public String MAFT10_FLAG { get; set; }

    /// <summary>
    /// 異常類型
    /// </summary>
    public String AbnormalFlag { get; set; }

    /// <summary>
    /// 異常原因代碼
    /// </summary>
    public String AbnormalReasonID { get; set; }

    /// <summary>
    /// 異常原因中文
    /// </summary>
    public String AbnormalReasonCN { get; set; }

    /// <summary>
    /// 其他異常原因說明
    /// </summary>
    public String AbnormalDesc { get; set; }

    /// <summary>
    /// 批次處理註記，0:未處理，1:已處理。
    /// </summary>
    public String BatchFlag { get; set; }

    /// <summary>
    /// 來源別。A:APP，B:永豐雲
    /// </summary>
    public String Source { get; set; }

    /// <summary>
    /// PunchUserIP
    /// </summary>
    public String PunchUserIP { get; set; }

    /// <summary>
    /// RotateFlag
    /// </summary>
    public String RotateFlag { get; set; }

    /// <summary>
    /// 以Json字串格式，不包含換行格式。
    /// </summary>
    public String APPContent { get; set; }

    /// <summary>
    /// APP緯度
    /// </summary>
    public String Lat { get; set; }

    /// <summary>
    /// APP經度
    /// </summary>
    public String Lon { get; set; }

    /// <summary>
    /// APP定位類型。GPS:GPS定位，AGPS:輔助GPS定位(可能室內)
    /// </summary>
    public String GPSType { get; set; }

    /// <summary>
    /// APP系統別。A:Android，I:IOS
    /// </summary>
    public String OS { get; set; }

    /// <summary>
    ///APP設備別，字串長度255
    /// </summary>
    public String DeviceID { get; set; }

    /// <summary>
    /// 加班人性別。1:男，2:女。
    /// </summary>
    public String Sex { get; set; }

    /// <summary>
    /// 打卡日期
    /// </summary>
    public String PunchDate { get; set; }

    /// <summary>
    /// 打卡時間
    /// </summary>
    public String PunchTime { get; set; }

    /// <summary>
    /// 打卡時間計算用
    /// </summary>
    public String PunchTime4Count { get; set; }

    /// <summary>
    /// 班表值勤開始時間
    /// </summary>
    public String BeginTime { get; set; }

    /// <summary>
    /// 班表值勤結束時間
    /// </summary>
    public String EndTime { get; set; }

    /// <summary>
    /// 班表午休開始時間
    /// </summary>
    public String RestBeginTime { get; set; }

    /// <summary>
    /// 班表午休結束時間
    /// </summary>
    public String RestEndTime { get; set; }

    /// <summary>
    /// 異常註記
    /// </summary>
    public String ErrorFlag { get; set; }

    /// <summary>
    /// 處理訊息
    /// </summary>
    public String ResultMsg { get; set; }

    /// <summary>
    /// 提醒訊息
    /// </summary>
    public String RemindMsg { get; set; }

    /// <summary>
    /// 提醒訊息(提醒加班)
    /// </summary>
    public String RemindMsgAf { get; set; }

    /// <summary>
    /// 關懷訊息
    /// </summary>
    public String CareMsg { get; set; }

    /// <summary>
    /// 最後異動公司
    /// </summary>
    public String LastChgComp { get; set; }

    /// <summary>
    /// 最後異動者
    /// </summary>
    public String LastChgID { get; set; }

    /// <summary>
    /// 最後異動日期
    /// </summary>
    public String LastChgDate { get; set; }

}

public class PunchParaBean
{
    /// <summary>
    /// 公司
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 時間
    /// </summary>
    public String Para { get; set; }

    /// <summary>
    /// 訊息
    /// </summary>
    public String MsgPara { get; set; }
}

public class ParaBean
{
    /// <summary>
    /// DutyInBT
    /// </summary>
    public String DutyInBT { get; set; }

    /// <summary>
    /// DutyOutBT
    /// </summary>
    public String DutyOutBT { get; set; }

    /// <summary>
    /// PunchInBT
    /// </summary>
    public String PunchInBT { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String PunchOutBT { get; set; }

    /// <summary>
    /// VisitOVBT
    /// </summary>
    public String VisitOVBT { get; set; }
}

public class MsgParaBean
{
    /// <summary>
    /// PunchInBT
    /// </summary>
    public String PunchInMsgFlag { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String PunchInDefaultContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String PunchInSelfContent { get; set; }

    /// <summary>
    /// PunchInBT
    /// </summary>
    public String PunchOutMsgFlag { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String PunchOutDefaultContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String PunchOutSelfContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String AffairMsgFlag { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String AffairDefaultContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String AffairSelfContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String OVTenMsgFlag { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String OVTenDefaultContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String OVTenSelfContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String FemaleMsgFlag { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String FemaleDefaultContent { get; set; }

    /// <summary>
    /// PunchOutBT
    /// </summary>
    public String FemaleSelfContent { get; set; }
}