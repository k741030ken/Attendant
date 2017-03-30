using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Sample_BookingAdmin : SecurePage
{
    public string _DBName = "NetSample";
    public string _TabName = "Resource";
    public string _FldName = "MeetingRoom";
    /// <summary>
    /// 會議室清單
    /// </summary>
    public Dictionary<string, string> _dicRoomList
    {
        get
        {
            if (ViewState["_dicRoomList"] == null)
            {
                ViewState["_dicRoomList"] = Util.getCodeMap(_DBName, _TabName, _FldName);
            }
            return (Dictionary<string, string>)(ViewState["_dicRoomList"]);
        }
        set
        {
            ViewState["_dicRoomList"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TabContainer1.ActiveTabIndex = 0;
        }
        ucScheduler.Visible = false;

        ddlRoom.ucSourceDictionary = Util.getDictionary(_dicRoomList);
        ddlRoom.Refresh();
        qryRoomList.ucSourceDictionary = Util.getDictionary(_dicRoomList);
        qryRoomList.Refresh();
    }

    /// <summary>
    /// 開始預約
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBooking_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlRoom.ucSelectedID))
        {
            divRoomScheArea.Style["display"] = "";
            labScheInfo.Text = string.Format("《{0}》預約", ddlRoom.ucSelectedID);
            ucScheduler.ucDBName = _DBName;
            ucScheduler.ucResourceIDList = ddlRoom.ucSelectedID;
            ucScheduler.ucIsReadOnly = false;
            ucScheduler.ucIsOwnerEditOnly = chkAllowNonOwnerEdit.Checked?false:true;
            ucScheduler.ucIsConflictEnabled = chkAllowConflict.Checked?true:false;
            ucScheduler.ucIsShowEventID = false;
            ucScheduler.ucIsShowEventUpdInfo = true;
            ucScheduler.ucLoadMode = "week";
            ucScheduler.Visible = true;
            ucScheduler.Refresh();
        }
        else
        {
            divRoomScheArea.Style["display"] = "none";
            Util.MsgBox("請選擇要預約的會議室");
        }
    }

    /// <summary>
    /// 開始查詢
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQry_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(qryRoomList.ucSelectedIDList))
        {
            divRoomScheArea.Style["display"] = "";
            labScheInfo.Text = "預約狀態查詢";
            ucScheduler.ucDBName = _DBName;
            ucScheduler.ucResourceIDList = qryRoomList.ucSelectedIDList;
            ucScheduler.ucIsReadOnly = true;
            ucScheduler.ucIsShowEventID = true;
            ucScheduler.ucIsShowEventUpdInfo = true;
            ucScheduler.ucLoadMode = "month";
            ucScheduler.Visible = true;
            ucScheduler.Refresh();
        }
        else
        {
            divRoomScheArea.Style["display"] = "none";
            Util.MsgBox("請選擇要查詢的會議室");
        }
    }
}