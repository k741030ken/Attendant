using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

public partial class Util_ucLightBox : BaseUserControl
{
    #region 自訂屬性
    /// <summary>
    /// 是否允許中斷(預設 false)
    /// </summary>
    public bool ucLightBoxBreakEnabled
    {
        get
        {
            if (PageViewState["_LightBoxBreakEnabled"] == null)
            {
                PageViewState["_LightBoxBreakEnabled"] = false;
            }
            return (bool)PageViewState["_LightBoxBreakEnabled"];
        }
        set
        {
            PageViewState["_LightBoxBreakEnabled"] = value;
        }
    }

    /// <summary>
    /// 燈箱訊息
    /// </summary>
    public string ucLightBoxMsg
    {
        get
        {
            return labLightBoxMsg.Text;
        }

        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                labLightBoxMsg.Text = value;
            }
            else
            {
                labLightBoxMsg.Text = RS.Resources.Msg_Waiting;
            }
        }
    }

    /// <summary>
    /// 燈箱圖形
    /// </summary>
    public string ucLightBoxImgUrl
    {
        get
        {
            return imgLightBoxWaiting.ImageUrl;
        }

        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                imgLightBoxWaiting.ImageUrl = value;
            }
            else
            {
                imgLightBoxWaiting.ImageUrl = RS.Resources.Msg_Waiting;
            }
        }
    }

    /// <summary>
    /// 顯示燈箱JS
    /// <para>**提供開發人員自行撰寫 Client 處理時引用**</para>
    /// </summary>
    public string ucShowClientJS
    {
        get
        {
            return string.Format("document.getElementById('{0}').style.display='';document.getElementById('{1}').style.display='';", divLightBoxPopBody.ClientID, divLightBoxPopOverlay.ClientID);
        }
    }

    /// <summary>
    /// 關閉燈箱JS
    /// <para>**提供開發人員自行撰寫 Client 處理時引用**</para>
    /// </summary>
    public string ucHideClientJS
    {
        get
        {
            return string.Format("document.getElementById('{0}').style.display='none';document.getElementById('{1}').style.display='none';", divLightBoxPopBody.ClientID, divLightBoxPopOverlay.ClientID);
        }
    }
    #endregion

    #region 自訂事件
    /// <summary>
    /// 控制項 Break 事件委派
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Break(object sender, EventArgs e);

    /// <summary>
    /// 控制項 Break 事件
    /// </summary>
    public event Break onBreak;

    protected void btnBreak_Click(object sender, EventArgs e)
    {
        if (onBreak != null)
        {
            onBreak(this, e);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Refresh();
        }
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        if (string.IsNullOrEmpty(labLightBoxMsg.Text))
        {
            labLightBoxMsg.Text = RS.Resources.Msg_Waiting;
        }
        btnBreak.Visible = ucLightBoxBreakEnabled;
    }

    /// <summary>
    /// 顯示燈箱
    /// </summary>
    /// <param name="IsAutoPostBackOrRefresh">是否重新整理頁面</param>
    /// <param name="strRefreshUrl">重新整理的指定Url</param>
    /// <param name="intAutoPostBackOrRefreshTimer">重新整理延遲時間(毫秒)</param>
    public void Show(bool IsAutoPostBackOrRefresh = false, string strRefreshUrl = "", int intAutoPostBackOrRefreshTimer = 300)
    {
        divLightBoxPopBody.Style["display"] = "";
        divLightBoxPopOverlay.Style["display"] = "";

        if (IsAutoPostBackOrRefresh)
        {
            string strJS = string.Empty;
            if (string.IsNullOrEmpty(strRefreshUrl))
            {
                //PostBack
                strJS += "setTimeout(function(){document.forms[0].submit()}, " + intAutoPostBackOrRefreshTimer + " );";
            }
            else
            {
                //Refresh Url
                strJS += "setTimeout(function(){location.href='" + strRefreshUrl + "';}, " + intAutoPostBackOrRefreshTimer + ");";
            }
            Util.setJSContent(strJS, this.ClientID + Guid.NewGuid().ToString());
        }
    }

    /// <summary>
    /// 關閉燈箱
    /// </summary>
    public void Hide()
    {
        divLightBoxPopBody.Style["display"] = "none";
        divLightBoxPopOverlay.Style["display"] = "none";
    }
}