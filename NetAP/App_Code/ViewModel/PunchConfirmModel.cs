using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 適用於連接DB資料用(資料多以可存入DB的格式為主)
/// </summary>
public class PunchConfirmModel //請取有意義的名稱+Bean結尾
{
    /// <summary>
    /// 公司代碼
    /// </summary>
    public String CompID { get; set; }

    /// <summary>
    /// 員工編號
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 應打卡日期yyyyMMdd，無格式的日期。
    /// </summary>
    public String DutyDate { get; set; }

    /// <summary>
    /// 應打卡時間(HHmm)
    /// </summary>
    public String DutyTime { get; set; }

    /// <summary>
    /// 打卡開始日期yyyyMMdd，無格式的日期。
    /// </summary>
    public String PunchSDate { get; set; }

    /// <summary>
    /// 打卡結束日期yyyyMMdd，無格式的日期。
    /// </summary>
    public String PunchEDate { get; set; }

    /// <summary>
    /// 打卡時間HH:mm:ss.fff有格式的時間。
    /// </summary>
    public String PunchTime { get; set; }

    /// <summary>
    /// 打卡時間HH:mm:ss.fff有格式的時間，開始時間
    /// </summary>
    public String PunchSTime { get; set; }

    /// <summary>
    /// 打卡時間HH:mm:ss.fff有格式的時間，結束時間
    /// </summary>
    public String PunchETime { get; set; }

    /// <summary>
    /// 依CompID、EmpID、DutyDate查詢打卡確認檔序號，取得PunchConfirmSeq最大值+1。
    /// </summary>
    public String PunchConfirmSeq { get; set; }

    /// <summary>
    /// 部門代碼
    /// </summary>
    public String DeptID { get; set; }

    /// <summary>
    /// 部門名稱
    /// </summary>
    public String DeptName { get; set; }

    /// <summary>
    /// 單位代碼(科組課代號)
    /// </summary>
    public String OrganID { get; set; }

    /// <summary>
    /// 單位名稱(科組課名稱)
    /// </summary>
    public String OrganName { get; set; }

    /// <summary>
    /// 功能線單位代號(科組課代號)
    /// </summary>
    public String FlowOrganID { get; set; }

    /// <summary>
    /// 功能線單位名稱(科組課名稱)
    /// </summary>
    public String FlowOrganName { get; set; }

    /// <summary>
    /// 性別。1:男，2:女。
    /// </summary>
    public String Sex { get; set; }

    /// <summary>
    /// 打卡類型。0:出退勤打卡，1:午休打卡。
    /// </summary>
    public String PunchFlag { get; set; }

    /// <summary>
    /// 工作性質代碼(多個工作性質，用”| ”隔開)
    /// </summary>
    public String WorkTypeID { get; set; }

    /// <summary>
    /// 工作性質名稱
    /// </summary>
    public String WorkType { get; set; }

    /// <summary>
    /// 女性十點後打卡註記。0:否，1:是。
    /// </summary>
    public String MAFT10_FLAG { get; set; }

    /// <summary>
    /// 出退勤狀態。0:正常，1:異常，2:補登作業中，3:異常不控管。
    /// </summary>
    public String ConfirmStatus { get; set; }

    /// <summary>
    /// 異常代碼(1~6)
    /// </summary>
    public String AbnormalType { get; set; }

    /// <summary>
    /// 畫面上顯示，異常代碼(1~6)
    /// </summary>
    public String AbnormalType_Show { get; set; }

    /// <summary>
    /// 出退勤類型。1:出勤打卡，2:退勤打卡，3:午休開始，4:午休結束。
    /// </summary>
    public String ConfirmPunchFlag { get; set; }

    /// <summary>
    /// 畫面顯示，出退勤類型。1:出勤打卡，2:退勤打卡，3:午休開始，4:午休結束。
    /// </summary>
    public String ConfirmPunchFlag_Show { get; set; }

    /// <summary>
    /// 打卡紀錄檔序號。
    /// </summary>
    public String PunchSeq { get; set; }

    /// <summary>
    /// 打卡補登檔序號。
    /// </summary>
    public String PunchRemedySeq { get; set; }

    /// <summary>
    /// 補登原因代碼
    /// </summary>
    public String RemedyReasonID { get; set; }

    /// <summary>
    /// 補登原因中文
    /// </summary>
    public String RemedyReasonCN { get; set; }

    /// <summary>
    /// 補登打卡時間(HHmm)
    /// </summary>
    public String RemedyPunchTime { get; set; }

    /// <summary>
    /// 太早或太晚打卡原因。0:無異常，1:公務，2:非公務。
    /// </summary>
    public String AbnormalFlag { get; set; }

    /// <summary>
    /// 非公務原因代碼
    /// </summary>
    public String AbnormalReasonID { get; set; }

    /// <summary>
    /// 非公務原因中文
    /// </summary>
    public String AbnormalReasonCN { get; set; }

    /// <summary>
    /// 其他原因說明
    /// </summary>
    public String AbnormalDesc { get; set; }

    /// <summary>
    /// 修改後太早或太晚打卡原因。0:無異常，1:公務，2:非公務。
    /// </summary>
    public String Remedy_AbnormalFlag { get; set; }

    /// <summary>
    /// 修改後非公務原因代碼
    /// </summary>
    public String Remedy_AbnormalReasonID { get; set; }

    /// <summary>
    /// 修改後非公務原因中文
    /// </summary>
    public String Remedy_AbnormalReasonCN { get; set; }

    /// <summary>
    /// 修改後其他原因說明
    /// </summary>
    public String Remedy_AbnormalDesc { get; set; }

    /// <summary>
    /// 來源別。A:APP，B:永豐雲
    /// </summary>
    public String Source { get; set; }

    /// <summary>
    /// 畫面顯示，來源別。A:APP，B:永豐雲
    /// </summary>
    public String Source_Show { get; set; }

    /// <summary>
    /// 以Json字串格式，不包含換行格式。(Lat、Lon、GPSType、OS、DeviceID)
    /// </summary>
    public String APPContent { get; set; }

    /// <summary>
    /// 永豐雲打卡者IP
    /// </summary>
    public String PunchUserIP { get; set; }

    /// <summary>
    /// 輪班人員。0:否，1:是
    /// </summary>
    public String RotateFlag { get; set; }

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

    /// <summary>
    /// Grid Data(List)
    /// </summary>
    public List<PunchConfirmDataModel> SelectGridDataList { get; set; }
}


public class PunchConfirmDataModel //請取有意義的名稱+Model結尾
{
    /// <summary>
    /// 狀態
    /// </summary>
    public String AbnormalType_Show { get; set; }

    /// <summary>
    /// 打卡日期
    /// </summary>
    public String PunchDate { get; set; }

    /// <summary>
    /// 時間
    /// </summary>
    public String PunchTime { get; set; }

    /// <summary>
    /// 類型
    /// </summary>
    public String ConfirmPunchFlag_Show { get; set; }

    /// <summary>
    /// 來源
    /// </summary>
    public String Source_Show { get; set; }

    /// <summary>
    /// 科組課
    /// </summary>
    public String OrganName_Show { get; set; }

    /// <summary>
    /// 員工編號
    /// </summary>
    public String EmpID { get; set; }

    /// <summary>
    /// 員工姓名
    /// </summary>
    public String EmpName { get; set; }

    /// <summary>
    /// 原因
    /// </summary>
    public String Remedy_AbnormalReasonCN { get; set; }

    /// <summary>
    /// 其他說明
    /// </summary>
    public String Remedy_AbnormalDesc { get; set; }

    /// <summary>
    /// 輪班人員
    /// </summary>
    public String RotateFlag { get; set; }
}