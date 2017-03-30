using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using SinoPac.WebExpress.Work;

/// <summary>
/// 檢視 AclInfo 物件資訊
/// </summary>
public partial class AclExpress_ucAclInfo : BaseUserControl
{
    #region 屬性
    /// <summary>
    /// 指定要顯示的 AclInfo 物件
    /// </summary>
    public AclInfo ucAclInfo
    {
        get
        {
            return (AclInfo)(ViewState["_AclInfo"]);
        }
        set
        {
            ViewState["_AclInfo"] = value;
        }
    }

    protected Dictionary<string, string> dicAclArea
    {
        get
        {
            if (ViewState["_dicAclArea"] == null)
            {
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                ViewState["_dicAclArea"] = Util.getDictionary(db.ExecuteDataSet("Select AreaID ,AreaName from AclArea").Tables[0]);
            }
            return (Dictionary<string, string>)(ViewState["_dicAclArea"]);
        }
    }

    protected Dictionary<string, string> dicAclAreaGrant
    {
        get
        {
            if (ViewState["_dicAclAreaGrant"] == null)
            {
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                ViewState["_dicAclAreaGrant"] = Util.getDictionary(db.ExecuteDataSet("Select AreaID + '|' + GrantID as 'PKey' ,GrantName from AclAreaGrantList").Tables[0]);
            }
            return (Dictionary<string, string>)(ViewState["_dicAclAreaGrant"]);
        }
    }

    protected Dictionary<string, string> dicAclRule
    {
        get
        {
            if (ViewState["_dicAclRule"] == null)
            {
                DbHelper db = new DbHelper(AclExpress._AclDBName);
                ViewState["_dicAclRule"] = Util.getDictionary(db.ExecuteDataSet("Select RuleID,RuleName From AclRule").Tables[0]);
            }
            return (Dictionary<string, string>)(ViewState["_dicAclRule"]);
        }
    }

    protected Dictionary<string, string> dicAclAdminType
    {
        get
        {
            if (ViewState["_dicAclAdminType"] == null)
            {
                ViewState["_dicAclAdminType"] = AclExpress.getAclAdminTypeList();
            }
            return (Dictionary<string, string>)(ViewState["_dicAclAdminType"]);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ucAclInfo == null)
        {
            divAclInfo.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_ParaNotFoundList, "ucAclInfo"));
        }

        if (!IsPostBack) 
        {
            if (AclExpress.IsAclAdminUser())
            {
                tab03.Visible = true;
            }
            else
            {
                tab03.Visible = false;
            }        
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        if (ucAclInfo != null)
        {
            divAclInfo.Visible = true;
            labErrMsg.Visible = false;
            TabContainer1.ActiveTabIndex = 0;

            DataTable dtTemp;
            Dictionary<string, Dictionary<string, string>> oMap;
            Dictionary<string, string> oDicDisp;
            string strGrantName;

            //個人
            txtUserInfo.ucTextData = string.Format("{0} - {1}", ucAclInfo.UserID, UserInfo.findUserName(ucAclInfo.UserID));

            //角色
            StringBuilder sb = new StringBuilder();
            if (ucAclInfo.RuleList.Length > 0)
            {
                txtRuleInfo.ucRows = ucAclInfo.RuleList.Length + 1;
                for (int i = 0; i < ucAclInfo.RuleList.Length; i++)
                {
                    sb.AppendLine(string.Format("{0} [{1}]", ucAclInfo.RuleList[i], dicAclRule[ucAclInfo.RuleList[i]]));
                }
            }
            txtRuleInfo.ucTextData = sb.ToString();

            //詳情
            labAclInfo.Text = AclInfo.getAclInfoPage(ucAclInfo.UserID);

            //使用權限
            dtTemp = new DataTable();
            dtTemp.Columns.Add("AreaID");
            dtTemp.Columns.Add("GrantID");
            dtTemp.Columns.Add("AreaName");
            dtTemp.Columns.Add("GrantName");
            dtTemp.Columns.Add("ActList");

            oMap = ucAclInfo.AuthMap;
            foreach (var area in oMap)
            {
                foreach (var grant in oMap[area.Key])
                {
                    strGrantName = dicAclAreaGrant.ContainsKey(area.Key + "|" + grant.Key) ? dicAclAreaGrant[area.Key + "|" + grant.Key] : "N/A";
                    dtTemp.Rows.Add(area.Key, grant.Key, string.Format("{0} - {1}", area.Key, dicAclArea[area.Key]), strGrantName, grant.Value);
                }
            }

            oDicDisp = new Dictionary<string, string>();
            oDicDisp.Clear();
            oDicDisp.Add("GrantID", "項目代號");
            oDicDisp.Add("GrantName", "項目名稱");
            oDicDisp.Add("ActList", "授予權限");
            gvAuthMap.ucDataDisplayDefinition = oDicDisp;

            gvAuthMap.ucDataGroupKey = "AreaName";
            gvAuthMap.ucGroupHeaderFormat = "《{0}》";
            gvAuthMap.ucDataKeyList = "AreaID,GrantID".Split(',');
            gvAuthMap.ucDataQryTable = dtTemp;
            gvAuthMap.ucExportAllField = true;
            gvAuthMap.ucExportOpenXmlEnabled = true;
            gvAuthMap.Refresh(true);

            //管理權限
            dtTemp = new DataTable();
            dtTemp.Columns.Add("AreaID");
            dtTemp.Columns.Add("AdminType");
            dtTemp.Columns.Add("AdminTypeName");
            dtTemp.Columns.Add("GrantID");
            dtTemp.Columns.Add("AreaName");
            dtTemp.Columns.Add("GrantName");

            oMap = ucAclInfo.AdminMap;
            string[] grantList;
            foreach (var area in oMap)
            {
                foreach (var adminType in oMap[area.Key])
                {
                    grantList = oMap[area.Key][adminType.Key].Split(',');
                    for (int i = 0; i < grantList.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(grantList[i]))
                        {
                            //略過空白項目
                            strGrantName = dicAclAreaGrant.ContainsKey(area.Key + "|" + grantList[i]) ? dicAclAreaGrant[area.Key + "|" + grantList[i]] : "N/A";
                            dtTemp.Rows.Add(area.Key, adminType.Key
                                , string.Format("{0} - {1}", adminType.Key, dicAclAdminType[adminType.Key])
                                , grantList[i], string.Format("{0} - {1}", area.Key, dicAclArea[area.Key]), strGrantName);
                        }
                    }
                }
            }

            oDicDisp = new Dictionary<string, string>();
            oDicDisp.Clear();
            oDicDisp.Add("AdminTypeName", "管理類型@L150");
            oDicDisp.Add("GrantID", "項目代號");
            oDicDisp.Add("GrantName", "項目名稱");
            gvAdminMap.ucDataDisplayDefinition = oDicDisp;

            gvAdminMap.ucDataGroupKey = "AreaName";
            gvAdminMap.ucGroupHeaderFormat = "《{0}》";
            gvAdminMap.ucDataKeyList = "AreaID,AdminType".Split(',');
            gvAdminMap.ucDataQryTable = dtTemp;
            gvAdminMap.ucExportAllField = true;
            gvAdminMap.ucExportOpenXmlEnabled = true;
            gvAdminMap.Refresh(true);

        }
    }

}