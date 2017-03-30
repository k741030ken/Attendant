using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;
using AclRS = SinoPac.WebExpress.Work.Properties;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// ACL 使用者授權作業
/// </summary>
public partial class AclExpress_AclAuthUserArea : AclPage
{

    #region 共用屬性
    private bool _IsDebug
    {
        get
        {
            if (ViewState["_IsDebug"] == null)
            {
                //預設以 Util._IsDebugMode 為主，若額外指定才例外處理
                ViewState["_IsDebug"] = Util._IsDebugMode;
                if (!Util.getRequestQueryString().IsNullOrEmpty("IsDebug"))
                {
                    ViewState["_IsDebug"] = (Util.getRequestQueryStringKey("IsDebug", "N", true) == "Y") ? true : false;
                }
            }
            return (bool)(ViewState["_IsDebug"]);
        }
        set
        {
            ViewState["_IsDebug"] = value;
        }
    }

    private string _CompID
    {
        get
        {
            if (ViewState["_CompID"] == null) { ViewState["_CompID"] = UserInfo.getUserInfo().CompID; }
            return (string)(ViewState["_CompID"]);
        }
        set
        {
            ViewState["_CompID"] = value;
        }
    }

    private string _DeptID
    {
        get
        {
            if (ViewState["_DeptID"] == null) { ViewState["_DeptID"] = UserInfo.getUserInfo().DeptID; }
            return (string)(ViewState["_DeptID"]);
        }
        set
        {
            ViewState["_DeptID"] = value;
        }
    }

    /// <summary>
    /// 部門人員清單
    /// </summary>
    private Dictionary<string, string> _DicUserList
    {
        get
        {
            if (ViewState["_DicUserList"] == null)
            {
                ViewState["_DicUserList"] = Util.getDictionary(OrgExpress.getDeptAllUser(string.Format("{0}|{1}", _CompID, _DeptID), true));
            }
            return (Dictionary<string, string>)ViewState["_DicUserList"];
        }
        set
        {
            ViewState["_DicUserList"] = value;
        }
    }

    /// <summary>
    /// 可授權的 GrantID 清單
    /// </summary>
    private Dictionary<string, string> _DicGrantList
    {
        get
        {
            if (ViewState["_DicGrantList"] == null)
            {
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                DataTable dt = db.ExecuteDataSet(string.Format("Select GrantID ,GrantName From AclAreaGrantList Where AreaID = '{0}' and GrantID in ('{1}') and IsEnabled = 'Y' ", PageAclAreaID, Util.getStringJoin(PageAclAdminGrantList, "','"))).Tables[0];
                ViewState["_DicGrantList"] = Util.getDictionary(dt, 0, 1, true);
            }
            return (Dictionary<string, string>)ViewState["_DicGrantList"];
        }
        set
        {
            ViewState["_DicGrantList"] = value;
        }
    }

    /// <summary>
    /// 使用者已被授權的 GrantID 清單
    /// </summary>
    private string _UserGrantList
    {
        get
        {
            if (ViewState["_UserGrantList"] == null && !string.IsNullOrEmpty(ucSeleUserID.ucSelectedID))
            {
                string strDeny = AclExpress.getAclAuthTypeList().Keys.First();
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                DataTable dt = db.ExecuteDataSet(string.Format("Select GrantID From AclAuthUserAreaGrantList Where UserID='{0}' and AreaID = '{1}' and AuthType <> '{2}' ", ucSeleUserID.ucSelectedID, PageAclAreaID, strDeny)).Tables[0];
                ViewState["_UserGrantList"] = Util.getStringJoin(Util.getArray(dt));
            }
            return (string)ViewState["_UserGrantList"];
        }
        set
        {
            ViewState["_UserGrantList"] = value;
        }
    }

    /// <summary>
    /// GrantID 已被授予的使用者清單
    /// </summary>
    private string _GrantUserList
    {
        get
        {
            if (ViewState["_GrantUserList"] == null && !string.IsNullOrEmpty(ucSeleGrantID.ucSelectedID))
            {
                string strDeny = AclExpress.getAclAuthTypeList().Keys.First();
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                DataTable dt = db.ExecuteDataSet(string.Format("Select UserID From AclAuthUserAreaGrantList Where UserID in ('{0}') and AreaID = '{1}' and GrantID = '{2}' and AuthType <> '{3}' ", Util.getStringJoin(Util.getArray(_DicUserList), "','"), PageAclAreaID, ucSeleGrantID.ucSelectedID, strDeny)).Tables[0];
                ViewState["_GrantUserList"] = Util.getStringJoin(Util.getArray(dt));
            }
            return (string)ViewState["_GrantUserList"];
        }
        set
        {
            ViewState["_GrantUserList"] = value;
        }
    }
    #endregion

    protected override void OnPreLoad(EventArgs e)
    {
        PageAclAreaID = Util.getRequestQueryStringKey("AreaID");
        PageAclGrantID = "AclAuthUserArea";

        PageAclAdminEnabled = true;  //Acl 管理模式
        PageAclForcedInit = true;

        base.OnPreLoad(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labAclInfo.Text = this.getAclPageInfo();

            if (string.IsNullOrEmpty(PageAclAreaID))
            {
                divBody.Visible = false;
                labErrMsg.Visible = true;
                labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_ParaError1, "AreaID"));
                return;
            }

            DivTabControl.Visible = false;
            btnScope.Text = "確　　定";
            labWaiting.Text = RS.Resources.Msg_Waiting;

            btnUpdByUserID.OnClientClick = "if (confirm('" + AclRS.Resources.Msg_AclAuthUpdateConfirm + "')) { ShowWaiting();this.style.display='none'; } else{ return false; }";
            btnUpdByGrantID.OnClientClick = "if (confirm('" + AclRS.Resources.Msg_AclAuthUpdateConfirm + "')) { ShowWaiting();this.style.display='none'; } else{ return false; }";
        }

        ucCompDept.SetDefault();
        ucCompDept.ucIsRequire01 = true;
        ucCompDept.ucIsRequire02 = true;
        ucCompDept.ucDropDownListEnabled03 = false;
        ucCompDept.ucDefaultSelectedValue01 = _CompID;
        ucCompDept.ucDefaultSelectedValue02 = _DeptID;

        switch (PageAclAdminType.ToUpper())
        {
            case "FULL":
                ucCompDept.ucIsReadOnly01 = false;
                ucCompDept.ucIsReadOnly02 = false;
                break;
            case "COMP":
                ucCompDept.ucIsReadOnly01 = true;
                ucCompDept.ucIsReadOnly02 = false;
                break;
            case "DEPT":
                ucCompDept.ucIsReadOnly01 = true;
                ucCompDept.ucIsReadOnly02 = true;
                break;
            default:
                break;
        }
        ucCompDept.Refresh();
    }

    /// <summary>
    /// 選擇授權範圍
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnScope_Click(object sender, EventArgs e)
    {
        btnScope.Text = "切　　換";
        _CompID = ucCompDept.ucSelectedValue01;
        _DeptID = ucCompDept.ucSelectedValue02;
        DivTabControl.Visible = true;
        TabContainer.ActiveTabIndex = 0;

        _DicGrantList = null;
        _DicUserList = null;

        ucSeleGrantID.ucSelectedID = "";
        ucSeleGrantID.ucSourceDictionary = _DicGrantList;
        ucSeleGrantID.Refresh();
        divSeleUserList.Visible = false;

        ucSeleUserID.ucSelectedID = "";
        ucSeleUserID.ucSourceDictionary = _DicUserList;
        ucSeleUserID.Refresh();
        divSeleGrantList.Visible = false;
    }

    /// <summary>
    /// 選擇 GrantID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSeleGrantID_Click(object sender, EventArgs e)
    {
        divSeleGrantList.Visible = false;
        divSeleUserList.Visible = false;

        string strKey = ucSeleGrantID.ucSelectedID;
        string strValue = ucSeleGrantID.ucSelectedInfo;
        if (string.IsNullOrEmpty(strKey))
        {
            Util.NotifyMsg(RS.Resources.Msg_DDL_NotSelected, Util.NotifyKind.Error);
        }
        else
        {
            _GrantUserList = null;
            divSeleUserList.Visible = true;
            ucSeleUserList.ucSourceDictionary = _DicUserList;
            ucSeleUserList.ucSelectedIDList = _GrantUserList;
            ucSeleUserList.Refresh();
            Util.NotifyMsg(string.Format(AclRS.Resources.Msg_AclAuthDataDisplay1, strValue));  //顯示 [{0}] 相關授權資料
        }
    }

    /// <summary>
    /// 選擇 UserID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSeleUserID_Click(object sender, EventArgs e)
    {
        divSeleGrantList.Visible = false;
        divSeleUserList.Visible = false;

        string strKey = ucSeleUserID.ucSelectedID;
        string strValue = ucSeleUserID.ucSelectedInfo;
        if (string.IsNullOrEmpty(strKey))
        {
            Util.NotifyMsg(RS.Resources.Msg_DDL_NotSelected, Util.NotifyKind.Error);
        }
        else
        {
            _UserGrantList = null;
            divSeleGrantList.Visible = true;
            ucSeleGrantList.ucSourceDictionary = _DicGrantList;
            ucSeleGrantList.ucSelectedIDList = _UserGrantList;
            ucSeleGrantList.Refresh();
            Util.NotifyMsg(string.Format(AclRS.Resources.Msg_AclAuthDataDisplay1, strValue));  //顯示 [{0}] 相關授權資料
        }
    }

    /// <summary>
    /// 更新授權資料(By Grant)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdByGrantID_Click(object sender, EventArgs e)
    {
        //by GrantID
        ucSeleUserList.ucSourceDictionary = _DicUserList;
        ucSeleUserList.Refresh();

        if (_GrantUserList != ucSeleUserList.ucSelectedIDList)
        {
            try
            {
                string[] tmpList = null;
                string strTable = "AclAuthUserAreaGrantList";
                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                string strSQL = "";
                //刪除既有授權資料
                if (!string.IsNullOrEmpty(_GrantUserList))
                {
                    tmpList = _GrantUserList.Split(',');
                    for (int i = 0; i < tmpList.Count(); i++)
                    {
                        dicKey.Clear();
                        dicKey.Add("UserID", tmpList[i]);
                        dicKey.Add("AreaID", PageAclAreaID);
                        dicKey.Add("GrantID", ucSeleGrantID.ucSelectedID);
                        AclExpress.IsAclTableLog(strTable, dicKey, LogHelper.AppTableLogType.Delete);

                        strSQL = Util.getDataDeleteSQL(Util.getArray(dicKey, 0), Util.getArray(dicKey, 1), strTable.Split(','));
                        db.ExecuteNonQuery(strSQL);
                    }
                }

                //添加新的授權資料
                if (!string.IsNullOrEmpty(ucSeleUserList.ucSelectedIDList))
                {
                    string strAuthType = AclExpress.getAclAuthTypeList().Keys.Last();
                    tmpList = ucSeleUserList.ucSelectedIDList.Split(',');
                    for (int i = 0; i < tmpList.Count(); i++)
                    {
                        dicKey.Clear();
                        dicKey.Add("UserID", tmpList[i]);
                        dicKey.Add("AreaID", PageAclAreaID);
                        dicKey.Add("GrantID", ucSeleGrantID.ucSelectedID);

                        strSQL = "Insert " + strTable + " (UserID,AreaID,GrantID,UserName,AuthType,AllowActList,Remark,UpdUser,UpdDateTime) ";
                        strSQL += string.Format(" Values ('{0}','{1}','{2}','{3}','{4}','','By AclAuthUtil','{5}',getdate());", dicKey["UserID"], dicKey["AreaID"], dicKey["GrantID"], UserInfo.findUserName(dicKey["UserID"]), strAuthType, UserInfo.getUserInfo().UserID);
                        db.ExecuteNonQuery(strSQL);

                        AclExpress.IsAclTableLog(strTable, dicKey, LogHelper.AppTableLogType.Create);
                    }
                }

                _GrantUserList = ucSeleUserList.ucSelectedIDList;
                Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateSucceed, Util.NotifyKind.Success);  //[ACL] 授權資料更新成功！
            }
            catch (Exception ex)
            {
                LogHelper.WriteAppLog(AclExpress._AclDBName, LogHelper.AppLogType.Error, "AclExpress", "AclAuthUtil", ex.Message);
                Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateFail, Util.NotifyKind.Error);  //[ACL] 授權資料更新失敗！
            }
        }
        else
        {
            Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateNoAction); //[ACL] 無異動，不需更新授權資料！
        }
    }

    /// <summary>
    /// 更新授權資料(By User)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdByUserID_Click(object sender, EventArgs e)
    {
        //by UserID
        ucSeleGrantList.ucSourceDictionary = _DicGrantList;
        ucSeleGrantList.Refresh();

        if (_UserGrantList != ucSeleGrantList.ucSelectedIDList)
        {
            try
            {
                string[] tmpList = null;
                string strTable = "AclAuthUserAreaGrantList";
                Dictionary<string, string> dicKey = new Dictionary<string, string>();
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                string strSQL = "";
                //刪除既有授權資料
                if (!string.IsNullOrEmpty(_UserGrantList))
                {
                    tmpList = _UserGrantList.Split(',');
                    for (int i = 0; i < tmpList.Count(); i++)
                    {
                        dicKey.Clear();
                        dicKey.Add("UserID", ucSeleUserID.ucSelectedID);
                        dicKey.Add("AreaID", PageAclAreaID);
                        dicKey.Add("GrantID", tmpList[i]);
                        AclExpress.IsAclTableLog(strTable, dicKey, LogHelper.AppTableLogType.Delete);

                        strSQL = Util.getDataDeleteSQL(Util.getArray(dicKey, 0), Util.getArray(dicKey, 1), strTable.Split(','));
                        db.ExecuteNonQuery(strSQL);
                    }
                }

                //添加新的授權資料
                if (!string.IsNullOrEmpty(ucSeleGrantList.ucSelectedIDList))
                {
                    string strAuthType = AclExpress.getAclAuthTypeList().Keys.Last();
                    tmpList = ucSeleGrantList.ucSelectedIDList.Split(',');
                    for (int i = 0; i < tmpList.Count(); i++)
                    {
                        dicKey.Clear();
                        dicKey.Add("UserID", ucSeleUserID.ucSelectedID);
                        dicKey.Add("AreaID", PageAclAreaID);
                        dicKey.Add("GrantID", tmpList[i]);

                        strSQL = "Insert " + strTable + " (UserID,AreaID,GrantID,UserName,AuthType,AllowActList,Remark,UpdUser,UpdDateTime) ";
                        strSQL += string.Format(" Values ('{0}','{1}','{2}','{3}','{4}','','By AclAuthUtil','{5}',getdate());", dicKey["UserID"], dicKey["AreaID"], dicKey["GrantID"], UserInfo.findUserName(dicKey["UserID"]), strAuthType, UserInfo.getUserInfo().UserID);
                        db.ExecuteNonQuery(strSQL);

                        AclExpress.IsAclTableLog(strTable, dicKey, LogHelper.AppTableLogType.Create);
                    }
                }

                _UserGrantList = ucSeleGrantList.ucSelectedIDList;
                Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateSucceed, Util.NotifyKind.Success);  //[ACL] 授權資料更新成功！
            }
            catch (Exception ex)
            {
                LogHelper.WriteAppLog(AclExpress._AclDBName, LogHelper.AppLogType.Error, "AclExpress", "AclAuthUtil", ex.Message);
                Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateFail, Util.NotifyKind.Error);  //[ACL] 授權資料更新失敗！
            }
        }
        else
        {
            Util.NotifyMsg(AclRS.Resources.Msg_AclAuthUpdateNoAction);  //[ACL] 無異動，不需更新授權資料！
        }
    }

}