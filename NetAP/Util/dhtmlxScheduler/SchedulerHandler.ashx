<%@ WebHandler Language="C#" Class="SchedulerHandler" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
using dhtmlxConnectors;

/// <summary>
/// 與指定[資料庫/資料表/資源ID清單]的行事曆資料表連動
/// *建議的必要參數*
/// [DBName/TableName](若無則自動預設為 [NetSample / AppScheduler])
/// [ResourceIDList]指定要關聯的資源ID清單，若為空白則全部顯示
/// *選擇性參數*
/// [NewColor][NewTextColor]當新增行程事件，要套用的[背景/文字顏色]，預設是 [DodgerBlue/White]
/// ******
/// 欲套用此功能，該資料庫內需有和 [AppScheduler] 相同Schma的資料表，該範例資料表可從 [NetSample] 取得
/// 當[ResourceIDList]為複數時，會自動亂數產生同數量的背景顏色清單，並在顯示內容前自動加上所屬 ResourceID 以資區別
/// </summary>
public class SchedulerHandler : dhtmlxRequestHandler, IReadOnlySessionState
{
    string _DBName = "";
    string _TableName = "";
    string _NewColor = "";
    string _NewTextColor = "";
    string[] _ResourceIDList = "".Split(',');
    bool _IsReadOnly = false;
    UserInfo _User;

    //唯讀時的顏色 2015.03.26
    string _ReadOnlyColor = "#EBEBEB";
    string _ReadOnlyTextColor = "#3C3C3C";
    //可編輯時，非Owner的顏色 2015.06.01
    string _NonOwnerColor = "#3EA9D9";
    string _NonOwnerTextColor = "#EBEBEB";
    public override void ProcessRequest(HttpContext context)
    {
        string _DBName = Util.getRequestQueryStringKey("DBName");
        string _TableName = Util.getRequestQueryStringKey("TableName");
        string _NewColor = Util.getRequestQueryStringKey("NewColor","DodgerBlue");
        string _NewTextColor = Util.getRequestQueryStringKey("NewTextColor","White");
        string[] _ResourceIDList = Util.getRequestQueryStringKey("ResourceIDList").Split(',');
        bool _IsReadOnly = (Util.getRequestQueryStringKey("IsReadOnly", "N", true) == "Y") ? true : false;
        base.ProcessRequest(context);
    }

    public override IdhtmlxConnector CreateConnector(HttpContext context)
    {
        //建立連結物件
        dhtmlxSchedulerConnector connector = null;
        if (_DBName.ToUpper() == "EWS" || _TableName.ToUpper() == "EWS")
        {
            //2016.05.19 當 _TableName=[EWS] ，可用 _ResourceIDList=[user@xxx.com] 讀取指定的行事曆(若為空值則讀取預設行事曆)
            connector = new dhtmlxSchedulerConnector(
                  "EWS"
                , "EventID"
                , dhtmlxDatabaseAdapterType.Exchange
                , _ResourceIDList[0]
                , "fmDateTime"
                , "toDateTime"
                , "ResourceID, text, description, rec_type, event_pid, event_length, textColor, color, UpdUser, UpdUserName, UpdDateTime"
            );
        }
        else
        {
            connector = new dhtmlxSchedulerConnector(
                  _TableName
                , "EventID"
                , dhtmlxDatabaseAdapterType.SqlServer2005
                , DbHelper.getConnectionStrings(_DBName).ConnectionString
                , "fmDateTime"
                , "toDateTime"
                , "ResourceID, text, description, rec_type, event_pid, event_length, textColor, color, UpdUser, UpdUserName, UpdDateTime"
            );
        }



        //處理 ResourceIDList
        if (_ResourceIDList.Count() > 1)
        {
            //Multi ResourceID , force [Read Only]
            connector.Request.Rules.Add(new ExpressionRule("ResourceID in ('" + Util.getStringJoin(_ResourceIDList, "','") + "')"));
            connector.Request.AllowedAccess = AccessRights.Select;
        }
        else
        {
            //Single ResourceID
            if (!string.IsNullOrEmpty(_ResourceIDList[0]))
            {
                connector.Request.Rules.Add(new FieldRule("ResourceID", Operator.Equals, _ResourceIDList[0]));
            }
        }

        connector.BeforeProcessing += new EventHandler<DataActionProcessingEventArgs>(connector_BeforeProcessing);
        connector.AfterSelect += new EventHandler<DataSelectedEventArgs>(connector_AfterSelect);
        return connector;
    }

    void connector_BeforeProcessing(object sender, DataActionProcessingEventArgs e)
    {
        _User = UserInfo.getUserInfo();
        if ((e.DataAction.ActionType == ActionType.Inserted || e.DataAction.ActionType == ActionType.Updated) && _ResourceIDList.Count() == 1)
        {
            if (!e.DataAction.Data.ContainsKey((TableField)"ResourceID"))
            {
                e.DataAction.Data.Add((TableField)"ResourceID", _ResourceIDList[0]);
            }

            if (!e.DataAction.Data.ContainsKey((TableField)"textColor"))
            {
                e.DataAction.Data.Add((TableField)"textColor", _NewTextColor);
            }
            else
            {
                e.DataAction.Data[(TableField)"textColor"] = _NewTextColor;
            }

            if (!e.DataAction.Data.ContainsKey((TableField)"color"))
            {
                e.DataAction.Data.Add((TableField)"color", _NewColor);
            }
            else
            {
                e.DataAction.Data[(TableField)"color"] = _NewColor;
            }

            if (!e.DataAction.Data.ContainsKey((TableField)"UpdUser"))
            {
                e.DataAction.Data.Add((TableField)"UpdUser", _User.UserID);
            }
            else
            {
                e.DataAction.Data[(TableField)"UpdUser"] = _User.UserID;
            }

            if (!e.DataAction.Data.ContainsKey((TableField)"UpdUserName"))
            {
                e.DataAction.Data.Add((TableField)"UpdUserName", _User.UserName);
            }
            else
            {
                e.DataAction.Data[(TableField)"UpdUserName"] = _User.UserName;
            }

            if (!e.DataAction.Data.ContainsKey((TableField)"UpdDateTime"))
            {
                e.DataAction.Data.Add((TableField)"UpdDateTime", DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss"));
            }
            else
            {
                e.DataAction.Data[(TableField)"UpdDateTime"] = DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss");
            }
        }
    }

    void connector_AfterSelect(object sender, DataSelectedEventArgs e)
    {
        if (_ResourceIDList.Count() > 1)
        {
            //多重 ResourceID 時
            //2015.07.01 改變取背景色邏輯
            string[] bgColorArray = Util._DeepColorNameList.Split(',');
            int bgColorMaxIdx = bgColorArray.Count() - 1;
            int intPos = 0;
            foreach (System.Data.DataRow row in e.Data.Rows)
            {
                intPos = Array.IndexOf(_ResourceIDList, row["ResourceID"]);
                if (intPos > bgColorMaxIdx)
                {
                    intPos = intPos - (bgColorMaxIdx * (intPos / bgColorMaxIdx) + 1);
                }
                //代換顏色  
                row["Color"] = bgColorArray[intPos];
                row["textColor"] = _NewTextColor;
            }
        }
        else
        {
            //單一ResourceID時
            if (_IsReadOnly)
            {
                //唯讀
                foreach (System.Data.DataRow row in e.Data.Rows)
                {
                    row["Color"] = _ReadOnlyColor;
                    row["textColor"] = _ReadOnlyTextColor;
                }
            }
            else
            {
                //非Owner
                _User = UserInfo.getUserInfo();
                if (_User != null)
                {
                    foreach (System.Data.DataRow row in e.Data.Rows)
                    {

                        if (row["UpdUser"].ToString().Trim() != _User.UserID)
                        {
                            row["Color"] = _NonOwnerColor;
                            row["textColor"] = _NonOwnerTextColor;
                        }
                    }
                }
            }
        }
    }
}