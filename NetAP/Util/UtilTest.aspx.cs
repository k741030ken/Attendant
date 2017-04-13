using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;

/// <summary>
/// Util 功能展示網頁
/// </summary>
public partial class Util_Test : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Chche List
        labCacheList.Text = string.Format("快取清單：<br><ul><li>{0}</ul>", Util.getStringJoin(CacheHelper.getCacheList(),"<li>"));

        //設定 ucPageInfo1
        ucPageInfo1.ucIsShowApplication = true;
        ucPageInfo1.ucIsShowEnvironmentInfo = true;
        ucPageInfo1.ucIsShowQueryString = false;
        ucPageInfo1.ucIsShowRequestForm = false;
        ucPageInfo1.ucIsShowSession = true;

        //設定 TinyMCE
        //預設MCE
        Util.setJS_TinyMCE(txtMCE.ID);
        btnMCEClear.OnClientClick = string.Format("document.getElementById('{0}').innerHTML = '';return false;", txtMCEResult.ClientID);
        //自訂MCE
        string strCustToolList1 = "newdocument undo redo | fontsizeselect bold italic underline | bullist numlist outdent indent";
        string strCustToolList2 = "cut copy paste pastetext | alignleft aligncenter alignright | forecolor backcolor removeformat | table link";
        Util.setJS_TinyMCE(txtCustMCE.ID, 500, 150, false, strCustToolList1, strCustToolList2);
        btnCustMCEClear.OnClientClick = string.Format("document.getElementById('{0}').innerHTML = '';return false;", txtCustMCEResult.ClientID);

        //設定可繞過Valid檢查的相關物件
        ArrayList arBypassList = new ArrayList();
        arBypassList.Add(txtMCE.ID);
        arBypassList.Add(txtMCEResult.ID);
        arBypassList.Add(txtCustMCE.ID);
        arBypassList.Add(txtCustMCEResult.ID);

        arBypassList.Add(txtMailFrom.ID);
        arBypassList.Add(txtMailTo.ID);
        arBypassList.Add(txtMailCC.ID);
        arBypassList.Add(txtMailBCC.ID);

        arBypassList.Add(txtMailFrom2.ID);
        arBypassList.Add(txtMailTo2.ID);
        arBypassList.Add(txtMailCC2.ID);
        arBypassList.Add(txtMailBCC2.ID);

        Util.setRequestValidatorBypassIDList(Util.getStringJoin(arBypassList));

        //Upload相關
        ucUploadButton1.ucBtnWidth = 100;
        ucUploadButton1.ucBtnCaption = "上傳(對話框)";
        ucUploadButton1.ucPopupHeader = "匯入作業";
        ucUploadButton1.ucUploadFileExtList = "xls,xlsx";
        ucUploadButton1.onClose += new Util_ucUploadButton.Close(ucUploadButton1_onClose);

        //2015.06.24 新增
        ucUploadButton2.ucBtnWidth = 100;
        ucUploadButton2.ucBtnCaption = "上傳(新視窗)";
        ucUploadButton2.ucPopupHeader = "匯入作業";
        ucUploadButton2.ucUploadFileExtList = "xls,xlsx";
        ucUploadButton2.ucIsPopNewWindow = true;
        ucUploadButton2.onClose += ucUploadButton2_onClose;


        //附檔相關
        ucAttachAdminButton1.ucBtnWidth = 100;
        ucAttachAdminButton1.ucBtnCaption = "管理(對話框)";
        ucAttachAdminButton1.ucAttachDB = txtAttachDB.Text;
        ucAttachAdminButton1.ucAttachID = txtAttachID.Text;
        ucAttachAdminButton1.ucAttachFileMaxQty = int.Parse("0" + txtAttachFileMaxQty.Text);
        ucAttachAdminButton1.ucAttachFileMaxKB = int.Parse("0" + txtAttachFileMaxKB.Text);
        ucAttachAdminButton1.ucAttachFileTotKB = int.Parse("0" + txtAttachFileTotKB.Text);
        ucAttachAdminButton1.ucAnonymousYN = ddlAnonymousYN.SelectedValue;
        ucAttachAdminButton1.ucAttachFileExtList = txtAttachFileExtList.Text;

        ucAttachAdminButton2.ucBtnWidth = 100;
        ucAttachAdminButton2.ucBtnCaption = "管理(新視窗)";
        ucAttachAdminButton2.ucIsPopNewWindow = true;
        ucAttachAdminButton2.ucAttachDB = txtAttachDB.Text;
        ucAttachAdminButton2.ucAttachID = txtAttachID.Text;
        ucAttachAdminButton2.ucAttachFileMaxQty = int.Parse("0" + txtAttachFileMaxQty.Text);
        ucAttachAdminButton2.ucAttachFileMaxKB = int.Parse("0" + txtAttachFileMaxKB.Text);
        ucAttachAdminButton2.ucAttachFileTotKB = int.Parse("0" + txtAttachFileTotKB.Text);
        ucAttachAdminButton2.ucAnonymousYN = ddlAnonymousYN.SelectedValue;
        ucAttachAdminButton2.ucAttachFileExtList = txtAttachFileExtList.Text;

        ucAttachDownloadButton1.ucBtnWidth = 100;
        ucAttachDownloadButton1.ucBtnCaption = "清單(對話框)";
        ucAttachDownloadButton1.ucAttachDB = txtAttachDB.Text;
        ucAttachDownloadButton1.ucAttachID = txtAttachID.Text;

        ucAttachDownloadButton2.ucBtnWidth = 100;
        ucAttachDownloadButton2.ucBtnCaption = "清單(新視窗)";
        ucAttachDownloadButton2.ucIsPopNewWindow = true;
        ucAttachDownloadButton2.ucAttachDB = txtAttachDB.Text;
        ucAttachDownloadButton2.ucAttachID = txtAttachID.Text;

        //設定單選的「候選清單」
        ucCommSingleSelect1.ucSourceDictionary = Util.getDictionary(getSampleData(), 0, 1, true);

        //彈出[複選清單](ucButton)

        ucCommMultiSelectButton1.ucSourceDictionary = OrgInfo.getOrgDictionary(); //2016.07.01 調整
        ucCommMultiSelectButton1.ucSelectedIDListToParentObjClientID = txtPopupID1.ClientID;
        ucCommMultiSelectButton1.ucSelectedInfoListToParentObjClientID = txtPopupInfo1.ClientID;
        ucCommMultiSelectButton1.ucSelectAllConfirmMsg = "這樣會造成災難，確定？";

        //設定[關聯選單](ucButton)
        ucCommCascadeSelectButton1.ucIsSelectUserYN = ddlIsSeleUser1.SelectedValue;
        ucCommCascadeSelectButton1.ucIsMultiSelectYN = ddlIsMultiSele1.SelectedValue;
        ucCommCascadeSelectButton1.ucDefCompID = txtDefComp1.Text;
        ucCommCascadeSelectButton1.ucDefDeptID = txtDefDept1.Text;
        ucCommCascadeSelectButton1.ucDefUserIDList = txtDefUser1.Text;
        ucCommCascadeSelectButton1.ucSelectedCompIDToParentObjClientID = txtComp1.ClientID;
        ucCommCascadeSelectButton1.ucSelectedDeptIDToParentObjClientID = txtDept1.ClientID;
        ucCommCascadeSelectButton1.ucSelectedUserIDListToParentObjClientID = txtUser1.ClientID;
        ucCommCascadeSelectButton1.ucSelectedCompInfoToParentObjClientID = txtCompInfo1.ClientID;
        ucCommCascadeSelectButton1.ucSelectedDeptInfoToParentObjClientID = txtDeptInfo1.ClientID;
        ucCommCascadeSelectButton1.ucSelectedUserInfoListToParentObjClientID = txtUserInfo1.ClientID;

        //設定Ajax關聯式下拉選單
        ucCascadingDropDown1.SetDefault(); //使用預設設定
        ucCascadingDropDown1.Refresh();

        //訂閱所需的彈出視窗事件，方便關閉視窗後，視需要進行相關後續處理
        ucModalPopup1.onComplete += new Util_ucModalPopup.btnCompleteClick(ucModalPopup1_onComplete);
        ucModalPopup1.onCancel += new Util_ucModalPopup.btnCancelClick(ucModalPopup1_onCancel);
        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);

        if (!IsPostBack)
        {
            //設定彈出視窗內的頁框來源URL
            txtPopupURL.Text = "";

            //設定 Cust64Key
            txtCust64Seed.Text = Util.getAppSetting("app://CfgCust64Seed/");

            //設定 HtmlMsg Kind
            ddlHtmlMsg.DataSource = Util.getDictionary(typeof(Util.HtmlMessageKind));
            ddlHtmlMsg.DataTextField = "value";
            ddlHtmlMsg.DataValueField = "key";
            ddlHtmlMsg.DataBind();

            //設定Mail
            txtMailFrom.Text = string.Format("{0} < {1} >", Util.getAppSetting("appMailAddrDisplay"), Util.getAppSetting("appMailFrom"));
            txtMailFrom2.Text = string.Format("{0} < {1} >", Util.getAppSetting("appMailAddrDisplay"), Util.getAppSetting("appMailFrom"));
            //txtMailAttach2.Text = "{\"AttachDB\":\"ezFlowAttach\",\"AttachID\":\"eDoc-20130130.00002.00010\",\"SeqNo\":\"1\"}";
            txtMailAttach2.Text = "[{'AttachDB':'ezFlowAttach','AttachID':'eDoc-20130130.00002.00010','SeqNo':'10'}]";

        }
        else
        {
            //顯示　ucCommCascadeSelect1　傳回值
            txtComp2.Text = ucCommCascadeSelect1.ucSelectedCompID;
            txtDept2.Text = ucCommCascadeSelect1.ucSelectedDeptID;
            txtUser2.Text = ucCommCascadeSelect1.ucSelectedUserIDList;
            txtCompInfo2.Text = ucCommCascadeSelect1.ucSelectedCompInfo;
            txtDeptInfo2.Text = ucCommCascadeSelect1.ucSelectedDeptInfo;
            txtUserInfo2.Text = ucCommCascadeSelect1.ucSelectedUserInfoList;
        }
    }


    void ucUploadButton1_onClose(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ucUploadButton1.ucUploadedFileName) && ucUploadButton1.ucUploadedFileBody.Length > 0)
        {
            //取得上傳結果
            MemoryStream ms = new MemoryStream((byte[])ucUploadButton1.ucUploadedFileBody);
            //判斷格式是Excel2003 or Excel2007
            bool isXMLFormat = (ucUploadButton1.ucUploadedFileName.ToUpper().IndexOf(".XLSX") > 0) ? true : false;
            //將Excel內容轉成DataSet
            DataSet ds = Util.getDataSetFromExcel(ms, isXMLFormat);

            try
            {
                //直接將上傳結果用GridView顯示在頁面上
                gvUpload.Visible = true;
                gvUpload.DataSource = ds;
                gvUpload.DataBind();
                Util.NotifyMsg("Excel轉換成功", Util.NotifyKind.Success);
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex);
                Util.NotifyMsg("Excel轉換失敗", Util.NotifyKind.Error);
            }
        }
        else 
        {
            Util.NotifyMsg("未接收到檔案", Util.NotifyKind.Error);
        }
    }

    //2015.06.24 新增
    void ucUploadButton2_onClose(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ucUploadButton2.ucUploadedFileName) && ucUploadButton2.ucUploadedFileBody.Length > 0)
        {
            //取得上傳結果
            MemoryStream ms = new MemoryStream((byte[])ucUploadButton2.ucUploadedFileBody);
            //判斷格式是Excel2003 or Excel2007
            bool isXMLFormat = (ucUploadButton2.ucUploadedFileName.ToUpper().IndexOf(".XLSX") > 0) ? true : false;
            //將Excel內容轉成DataSet
            DataSet ds = Util.getDataSetFromExcel(ms, isXMLFormat);

            try
            {
                //直接將上傳結果用GridView顯示在頁面上
                gvUpload.Visible = true;
                gvUpload.DataSource = ds;
                gvUpload.DataBind();
                Util.NotifyMsg("Excel轉換成功", Util.NotifyKind.Success);
            }
            catch (Exception ex)
            {
                LogHelper.WriteSysLog(ex);
                Util.NotifyMsg("Excel轉換失敗", Util.NotifyKind.Error);
            }
        }
        else
        {
            Util.NotifyMsg("未接收到檔案", Util.NotifyKind.Error);
        }
    }

    protected DataTable getSampleData()
    {
        //產生測試資料表
        DataTable dt = new DataTable();
        if (PageViewState["dtSample"] != null)
        {
            dt = (DataTable)PageViewState["dtSample"];
        }
        else
        {
            dt.Columns.Add("RoleID");
            dt.Columns.Add("RoleName");
            dt.Columns.Add("ActName");
            dt.Columns.Add("RoleDesc");
            dt.Rows.Add("ROLE001", "高長恭", "馮紹峰", "蘭陵王。因面相長得太柔美，他每次上陣作戰都要戴上猙獰的面具以威赫敵人，是心性柔善的不敗戰神。");
            dt.Rows.Add("ROLE002", "楊雪舞", "林依晨", "巫鹹天女、蘭陵王王妃。自小與奶奶隱居在如桃花源般的白山村，為了避免禍端，奶奶刻意不讓雪舞學會巫鹹族的預知先機能力，也企圖改變雪舞的命運。");
            dt.Rows.Add("ROLE003", "宇文邕", "陳曉東", "周武帝，冷靜、睿智、善於策略工計，自信以自己的能力足以稱霸北朝。但蘭陵王的不敗戰績，是他唯一攻不破的北齊城牆。");
            dt.Rows.Add("ROLE004", "高延宗", "胡宇威", "安德王，高長恭同父異母的弟弟兼副將。對高長恭永遠兄弟相挺。無法理解高長恭不像其他人那樣早早娶妃納妾，直到雪舞出現，讓他覺得能匹配高長恭的女人終於出現，便拚命地湊合兩人。");
            dt.Rows.Add("ROLE005", "鄭兒", "毛林林", "原為胡皇后的婢女、後為高緯的皇后。一開始被胡皇后和祖珽藉由選妃派去接近蘭陵王，事發後被流放為官奴。對蘭陵王由愛生恨，回宮後冒充為馮小憐便開始在高緯和蘭陵王的身上使了很多心計。");
            PageViewState["dtSample"] = dt;
        }
        return dt;
    }

    protected void btnShowPopup_Click(object sender, EventArgs e)
    {
        //顯示彈出FrameURL
        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = "彈出視窗";
        ucModalPopup1.ucBtnCompleteHeader = "返　回";
        ucModalPopup1.ucBtnCompleteWidth = 200;
        ucModalPopup1.ucBtnCompleteEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucFrameURL = txtPopupURL.Text;

        ucModalPopup1.Show();
    }

    protected void btnShowPopupRefresh_Click(object sender, EventArgs e)
    {
        //顯示彈出FrameURL並Refresh
        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = "彈出視窗並重新整理";
        ucModalPopup1.ucBtnCompleteHeader = "返　回";
        ucModalPopup1.ucBtnCompleteWidth = 200;
        ucModalPopup1.ucBtnCompleteEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucFrameURL = txtPopupURL.Text;

        ucModalPopup1.Show();
        //可視需要，強迫重新整理 Frame 內容
        ucModalPopup1.RefreshFrame();
    }

    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        Util.MsgBox(string.Format("按下[{0}]", e.Header));
    }

    protected void ucModalPopup1_onComplete(object sender, Util_ucModalPopup.btnCompleteEventArgs e)
    {
        Util.MsgBox(string.Format("按下[{0}]", e.Header));
    }

    protected void ucModalPopup1_onCancel(object sender, Util_ucModalPopup.btnCancelEventArgs e)
    {
        Util.MsgBox(string.Format("按下[{0}]", e.Header));
    }

    protected void btnPopupCascade_Click(object sender, EventArgs e)
    {
        ucCommCascadeSelect1.ucIsSelectUserYN = ddlIsSeleUser2.SelectedValue;
        ucCommCascadeSelect1.ucIsMultiSelectYN = ddlIsMultiSele2.SelectedValue;
        ucCommCascadeSelect1.ucDefCompID = txtDefComp2.Text;
        ucCommCascadeSelect1.ucDefDeptID = txtDefDept2.Text;
        ucCommCascadeSelect1.ucDefUserIDList = txtDefUser2.Text;
        ucCommCascadeSelect1.Refresh();

        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeight = 320;
        ucModalPopup1.ucPanelID = pnlCascading1.ID;
        ucModalPopup1.Show();
    }

    protected void btnStringJoin_Click(object sender, EventArgs e)
    {
        labStringJoin.Text = Util.getStringJoin(txtStringJoin1.Text.Split(','), txtStringJoin2.Text);
    }
    protected void btnHtmlMsg_Click(object sender, EventArgs e)
    {
        labHtmlMsg.Text = Util.getHtmlMessage((Util.HtmlMessageKind)int.Parse(ddlHtmlMsg.SelectedValue), txtHtmlMsg.Text);
    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        //附件以 FilePath 方式傳送
        string strErrMsg = "";
        if (Util.IsSendMail(txtMailFrom.Text, txtMailTo.Text.Split(','), txtMailSubject.Text, txtMailBody.Text, out strErrMsg, txtMailAttach.Text.Split(','), txtMailCC.Text.Split(','), txtMailBCC.Text.Split(',')))
            labMailMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed);
        else
            labMailMsg.Text = strErrMsg;
    }
    protected void btnMail2_Click(object sender, EventArgs e)
    {
        //附件以 JSON 方式傳送
        string strErrMsg = "";
        if (Util.IsSendMail(txtMailFrom2.Text, txtMailTo2.Text.Split(','), txtMailSubject2.Text, txtMailBody2.Text, out strErrMsg, txtMailAttach2.Text, txtMailCC2.Text.Split(','), txtMailBCC2.Text.Split(',')))
            labMailMsg2.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Succeed);
        else
            labMailMsg2.Text = strErrMsg;
    }

    protected void btnShowPopupContent_Click(object sender, EventArgs e)
    {
        //顯示彈出Content
        ucModalPopup1.Reset();
        ucModalPopup1.ucHtmlContent = txtPopupContent.Text;
        ucModalPopup1.Show();
    }

    protected void btnCust64Encode_Click(object sender, EventArgs e)
    {
        labCustMsg.Text = Util.getCust64EnCode(txtCustContent.Text,txtCust64Seed.Text);
    }
    protected void btnCust64Decode_Click(object sender, EventArgs e)
    {
        labCustMsg.Text = Util.getCust64DeCode(txtCustContent.Text,txtCust64Seed.Text);
    }
    protected void btnMD5Hash_Click(object sender, EventArgs e)
    {
        labCustMsg.Text = Util.getMD5Hash(txtCustContent.Text);
    }
    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        Util.ExportCalendar(dtStart.SelectedDate, dtEnd.SelectedDate, txtCalSubject.Text, txtCalLocation.Text, txtCalBody.Text);
    }
    protected void btnCalender2003_Click(object sender, EventArgs e)
    {
        Util.ExportCalendar2003(dtStart.SelectedDate, dtEnd.SelectedDate, txtCalSubject.Text, txtCalLocation.Text, txtCalBody.Text);
    }

    protected void btnDomain_Click(object sender, EventArgs e)
    {
        labDomain.Text = Util.getDomainNameFromURL(txtDomaimURL.Text);
    }
    protected void btnHost_Click(object sender, EventArgs e)
    {
        string strHost = Util.getDomainNameFromURL(txtHost01.Text);
        if (string.IsNullOrEmpty(txtHost02.Text))
            labHost.Text = Util.IsHostAlive(strHost).ToString();
        else
            labHost.Text = Util.IsHostAlive(strHost, int.Parse(txtHost02.Text)).ToString();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label tmbLabel = new Label();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //設定序號值
            tmbLabel = (Label)e.Row.Cells[1].FindControl("labSeqNo");
            tmbLabel.Text = (e.Row.DataItemIndex + 1).ToString();
        }
    }

    protected void btnMCE_Click(object sender, EventArgs e)
    {
        txtMCEResult.Text = txtMCE.Text;
    }

    protected void btnCustMCE_Click(object sender, EventArgs e)
    {
        txtCustMCEResult.Text = txtCustMCE.Text;
    }

    protected void btnAppKey_Click(object sender, EventArgs e)
    {
        labAppKey.Text = "";
        labAppKey.ForeColor = System.Drawing.Color.Black;
        var AppKeyResult = Util.getAppKey(txtAppKeyDB.Text, txtAppKeyID.Text, int.Parse(txtAppKeyQty.Text));
        if (string.IsNullOrEmpty(AppKeyResult.Item2))
        {
            labAppKey.ForeColor = System.Drawing.Color.Green;
            labAppKey.Text = string.Format("[{0}]", Util.getStringJoin(AppKeyResult.Item1, "]["));
        }
        else
        {
            labAppKey.ForeColor = System.Drawing.Color.Red;
            labAppKey.Text = AppKeyResult.Item2;
        }
    }

    protected void btnAppDetailLastSeqNo_Click(object sender, EventArgs e)
    {
        labAppDetailLastSeqNo.Text = "";
        labAppDetailLastSeqNo.ForeColor = System.Drawing.Color.Green;
        int intResult = Util.getLastKeySeqNo(txtDetailDB.Text, txtDetailTable.Text, txtDetailKeyFieldList.Text.Split(','), txtMasterKeyValueList.Text.Split(','));
        labAppDetailLastSeqNo.Text = string.Format("[{0}]", intResult);
    }

    protected void btnMsgBox_Click(object sender, EventArgs e)
    {
        Util.MsgBox(txtMsg.Text);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Util.ConfirmBox((Button)sender, txtMsg.Text);
    }

    protected void btnNotify1_Click(object sender, EventArgs e)
    {
        Util.NotifyMsg(txtMsg.Text);
    }

    protected void btnNotify2_Click(object sender, EventArgs e)
    {
        Util.NotifyMsg(txtMsg.Text, Util.NotifyKind.Success);
    }

    protected void btnNotify3_Click(object sender, EventArgs e)
    {
        Util.NotifyMsg(txtMsg.Text, Util.NotifyKind.Error);
    }

    protected void btnAppClear_Click(object sender, EventArgs e)
    {
        Application.Clear();
        Util.NotifyMsg("Application 已清除!", Util.NotifyKind.Success);
    }
    protected void btnSessClear_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Util.NotifyMsg("Session 已清除!", Util.NotifyKind.Success);
    }
    protected void btnCleanCache_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtCacheName.Text))
        {
            Util.MsgBox(SinoPac.WebExpress.Common.Properties.Resources.Msg_ParaNotFound);
        }
        else 
        {
            CacheHelper.setCacheClear(txtCacheName.Text);
            labCacheList.Text = string.Format("快取清單：<br><ul><li>{0}</ul>", Util.getStringJoin(CacheHelper.getCacheList(), "<li>"));
            Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_Succeed, Util.NotifyKind.Success);
        }
    }
    protected void btnCleanAllCache_Click(object sender, EventArgs e)
    {
        CacheHelper.setAllCacheClear();
        labCacheList.Text = string.Format("快取清單：<br><ul><li>{0}</ul>", Util.getStringJoin(CacheHelper.getCacheList(), "<li>"));
        Util.NotifyMsg(SinoPac.WebExpress.Common.Properties.Resources.Msg_Succeed, Util.NotifyKind.Success);
    }
}