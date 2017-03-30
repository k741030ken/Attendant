using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SinoPac.WebExpress.Common;
using SinoPac.WebExpress.DAO;
/// <summary>
/// [多語系資源檔]維護公用程式
/// </summary>
public partial class Util_MuiAdmin : SecurePage
{
    private string _DBName = "";
    private string _TableName = "";
    private string _PKFieldList = "";
    private string _PKValueList = "";
    private string _PKSQLFilter = "";

    private string _MuiTableSuffix = "_MUI";
    private string _MuiCultureField = "CultureCode";
    private string _MuiOtherFieldList = "";

    private DbHelper db = null;
    private DataTable dtTmp = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Util.setRequestValidatorBypassIDList("*");

        Util.setReadOnly("txtDefCode", true);
        Util.setReadOnly("txtDefData", true);
        //初始變數
        _DBName = Util.getRequestQueryStringKey("DBName");
        _TableName = Util.getRequestQueryStringKey("TableName");
        _PKFieldList = Util.getRequestQueryStringKey("PKFieldList");
        _PKValueList = Util.getRequestQueryStringKey("PKValueList");

        if (string.IsNullOrEmpty(_TableName) || string.IsNullOrEmpty(_PKFieldList) || string.IsNullOrEmpty(_PKValueList))
        {
            DivNormal.Visible = false;
            DivError.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "參數錯誤，需 [DBName][TableName][PKFieldList][PKValueList]");
            return;
        }

        //初始PK過濾SQL
        for (int i = 0; i < _PKFieldList.Split(',').Count(); i++)
        {
            _PKSQLFilter += string.Format(" And {0} = '{1}' ", _PKFieldList.Split(',')[i], _PKValueList.Split(',')[i]);
        }


        if (!IsPostBack)
        {
            labTableName.Text = _TableName;
            txtDefCode.Text = Util.getUICultureMap().Select(" IsDefault = 'Y' ")[0]["CultureCode"].ToString();
            //初始 CodeXX 欄位
            for (int i = 0; i < 5; i++)
            {
                ((TextBox)this.FindControl("txtCode" + (i + 1).ToString().PadLeft(2, '0'))).Text = "";
                Util.setReadOnly("txtCode" + (i + 1).ToString().PadLeft(2, '0'), true);
            }
            //找出需多語的欄位
            db = new DbHelper(_DBName);
            string strSQL = @" Select b.name as 'FieldName', '　' + b.name+ '　' as 'FieldInfo' From  sysobjects a,syscolumns b,systypes c 
                        Where  a.name = '{0}{1}'  And  a.id = b.id   And  b.xtype = c.xtype  
                        And  c.name <> 'sysname' And b.name Not in ('{2}') Order By b.colid";
            strSQL = string.Format(strSQL, _TableName, _MuiTableSuffix, (_PKFieldList + "," + _MuiCultureField).Replace(",", "','"));
            dtTmp = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
            //設定多語欄位清單變數
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                if (_MuiOtherFieldList.Length > 0) { _MuiOtherFieldList += ","; }
                _MuiOtherFieldList += dtTmp.Rows[i][0].ToString().Trim();
            }
            //設定多語欄位下拉選單
            ddlMuiColumn.DataSource = dtTmp;
            ddlMuiColumn.DataTextField = "FieldInfo";
            ddlMuiColumn.DataValueField = "FieldName";
            ddlMuiColumn.DataBind();
            ddlMuiColumn.SelectedIndex = 0;

            InitEditArea();
            RefreshEditArea();

        }

    }

    private void InitEditArea()
    {
        //初始多語系資料表(xx_MUI)，補足不夠的語系資料

        //取出需多語的CultureCode清單
        dtTmp = Util.getUICultureMap().Select(" IsDefault = 'N'").CopyToDataTable();
        string[] arCulture = Util.getArray(dtTmp);

        //取出既有多語過的CultureCode清單
        string strChkSQL = "Select {0} From {1}{2} Where 0 = 0 {3} ";
        strChkSQL = string.Format(strChkSQL, _MuiCultureField, _TableName, _MuiTableSuffix, _PKSQLFilter);
        db = new DbHelper(_DBName);
        dtTmp = db.ExecuteDataSet(CommandType.Text, strChkSQL).Tables[0];
        string[] arMui = Util.getArray(dtTmp);
        ArrayList arGapList = new ArrayList();
        for (int i = 0; i < arCulture.Count(); i++)
        {
            if (!arMui.Contains(arCulture[i].ToString()))
            {
                arGapList.Add(arCulture[i].ToString());
            }
        }
        if (arGapList.Count > 0)
        {
            //若需補資料
            string strGapSQL = "";
            for (int i = 0; i < arGapList.Count; i++)
            {
                //strGapSQL += "Insert {0}{1} Values ('{2}','{3}','{4}') ;";
                //strGapSQL = string.Format(strGapSQL, _TableName, _MuiTableSuffix, Util.getStringJoin(_PKValueList.Split(','), "','"), arGapList[i].ToString(), Util.getStringJoin(_MuiOtherFieldList.Split(','), "','"));
                strGapSQL += "Insert {0}{1} Select '{2}','{3}',{4} From {0} Where 0=0 {5} ;";
                strGapSQL = string.Format(strGapSQL, _TableName, _MuiTableSuffix, Util.getStringJoin(_PKValueList.Split(','), "','"), arGapList[i].ToString(), _MuiOtherFieldList, _PKSQLFilter);
            }
            if (!string.IsNullOrEmpty(strGapSQL))
            {
                db.ExecuteNonQuery(CommandType.Text, strGapSQL);
            }
        }

    }

    protected void ddlMuiColumn_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshEditArea();
    }

    private void RefreshEditArea()
    {
        //throw new NotImplementedException();
        //重新整理資料編輯區
        if (string.IsNullOrEmpty(ddlMuiColumn.SelectedValue))
        {
            DivNormal.Visible = false;
            DivError.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, "取不到下拉選單的值");
        }
        else
        {
            db = new DbHelper(_DBName);
            string strSQL = "Select {1} From {0} Where 0 = 0 {2}";
            strSQL = string.Format(strSQL, _TableName, ddlMuiColumn.SelectedValue, _PKSQLFilter);
            dtTmp = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
            if (dtTmp == null || dtTmp.Rows.Count <= 0)
            {
                return;
            }
            txtDefData.Text = dtTmp.Rows[0][0].ToString();
            strSQL = "Select * From {0}{1} Where 0=0 {2}";
            strSQL = string.Format(strSQL, _TableName, _MuiTableSuffix, _PKSQLFilter);
            dtTmp = db.ExecuteDataSet(CommandType.Text, strSQL).Tables[0];
            for (int i = 0; i < dtTmp.Rows.Count; i++)
            {
                string strSeq = (i + 1).ToString().PadLeft(2, '0');
                TextBox oCode = (TextBox)this.FindControl("txtCode" + strSeq);
                if (oCode != null)
                {
                    oCode.Text = dtTmp.Rows[i][_MuiCultureField].ToString().Trim();
                }
                TextBox oData = (TextBox)this.FindControl("txtData" + strSeq);
                if (oData != null)
                {
                    oData.Text = dtTmp.Rows[i][ddlMuiColumn.SelectedValue].ToString().Trim();
                }

            }

        }

    }


    protected void btnToSimp_Click(object sender, EventArgs e)
    {
        Button oBtn = (Button)sender;
        TextBox oData = (TextBox)this.FindControl(oBtn.CommandArgument);
        if (oData != null)
        {
            oData.Text = Util.getChinese(oData.Text, true);
        }
    }


    protected void btnToTrad_Click(object sender, EventArgs e)
    {
        Button oBtn = (Button)sender;
        TextBox oData = (TextBox)this.FindControl(oBtn.CommandArgument);
        if (oData != null)
        {
            oData.Text = Util.getChinese(oData.Text, false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strSQL = "";
        string tmpSQL1 = "";
        string tmpSQL2 = "";
        for (int i = 0; i < 5; i++)
        {
            string strSeq = (i + 1).ToString().PadLeft(2, '0');
            TextBox oCode = (TextBox)this.FindControl("txtCode" + strSeq);
            TextBox oData = (TextBox)this.FindControl("txtData" + strSeq);
            if ((oCode != null) && (oData != null))
            {
                if (!string.IsNullOrEmpty(oCode.Text))
                {
                    //預防 oData.Text 內含 {X} 語法，需分段組合更新 2014.10.08
                    tmpSQL1 = string.Format("Update {0}{1} Set {2} = N'", _TableName, _MuiTableSuffix, ddlMuiColumn.SelectedValue);
                    tmpSQL2 = string.Format("' Where 0=0 {0} And {1} = '{2}' ;", _PKSQLFilter, _MuiCultureField, oCode.Text);
                    strSQL += tmpSQL1 + oData.Text + tmpSQL2;
                    //strSQL += "Update {0}{1} Set {2} = N'{3}' Where 0=0 {4} And {5} = '{6}' ;";
                    //strSQL = string.Format(strSQL, _TableName, _MuiTableSuffix, ddlMuiColumn.SelectedValue, oData.Text, _PKSQLFilter, _MuiCultureField, oCode.Text);
                }
            }
        }

        if (string.IsNullOrEmpty(strSQL))
        {
            Util.NotifyMsg("資料不需更新");
        }
        else
        {
            DbHelper db = new DbHelper(_DBName);
            int intEffQty = db.ExecuteNonQuery(CommandType.Text, strSQL);
            if (intEffQty > 0)
                Util.NotifyMsg(string.Format("更新 {0} 筆資料成功", intEffQty), Util.NotifyKind.Success);
            else
                Util.NotifyMsg("資料更新失敗", Util.NotifyKind.Error);
        }

    }
}