using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;

/// <summary>
/// [複選]通用控制項
/// </summary>
public partial class Util_ucMultiPicker : BaseUserControl
{
    #region 相關屬性
    /// <summary>
    /// 視窗抬頭
    /// </summary>
    public string ucPopupHeader
    {
        get
        {
            if (ViewState["_PopupHeader"] == null)
            {
                ViewState["_PopupHeader"] = RS.Resources.ModalPopup_Header;
            }
            return (string)(ViewState["_PopupHeader"]);
        }
        set
        {
            ViewState["_PopupHeader"] = value;
        }
    }

    /// <summary>
    /// 是否 Require(預設 false)
    /// </summary>
    public bool ucIsRequire
    {
        get
        {
            if (ViewState["_IsRequire"] == null)
            {
                ViewState["_IsRequire"] = false;
                txtSelectedInfoList.CausesValidation = false;
                RequiredFieldValidator1.Enabled = false;
            }
            return (bool)(ViewState["_IsRequire"]);
        }
        set
        {
            ViewState["_IsRequire"] = value;
            txtSelectedInfoList.CausesValidation = value;
            RequiredFieldValidator1.Enabled = value;
        }
    }

    /// <summary>
    /// ReadOnly 時的 CSSClass(預設 Util_clsReadOnly)
    /// </summary>
    public string ucReadOnlyCSS
    {
        get
        {
            if (ViewState["_ReadOnlyCSS"] == null)
            {
                ViewState["_ReadOnlyCSS"] = "Util_clsReadOnly";
            }
            return (string)(ViewState["_ReadOnlyCSS"]);
        }
        set
        {
            ViewState["_ReadOnlyCSS"] = value;
        }
    }

    /// <summary>
    /// 按鈕的提示訊息
    /// </summary>
    public string ucBtnTooltip
    {
        get
        {
            if (ViewState["_BtnTooltip"] == null)
            {
                ViewState["_BtnTooltip"] = RS.Resources.MultiPicker_btnLaunch_ToolTip;
            }
            return (string)(ViewState["_BtnTooltip"]);
        }
        set
        {
            ViewState["_BtnTooltip"] = value;
        }
    }

    /// <summary>
    /// 文字框寬度(預設 350)
    /// </summary>
    public int ucWidth
    {
        get
        {
            if (ViewState["_Width"] == null)
            {
                ViewState["_Width"] = 350;
            }
            return (int)(ViewState["_Width"]);
        }
        set
        {
            ViewState["_Width"] = value;
        }
    }

    /// <summary>
    /// 文字框列高(預設 5)
    /// </summary>
    public int ucRows
    {
        get
        {
            return int.Parse(txtSelectedInfoList.Rows.ToString());
        }
        set
        {
            txtSelectedInfoList.Rows = value;
            txtSelectedInfoList.TextMode = (value > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
        }
    }

    /// <summary>
    /// 候選資料來源
    /// </summary>
    public Dictionary<string, string> ucSourceDictionary
    {
        get
        {
            if (ViewState["_SourceDictionary"] == null)
            {
                return null;
            }
            return (Dictionary<string, string>)(ViewState["_SourceDictionary"]);
        }
        set
        {
            ViewState["_SourceDictionary"] = value;
        }
    }

    /// <summary>
    /// 預設 SelectedIDList
    /// </summary>
    public string ucDefSelectedIDList
    {
        get
        {
            if (ViewState["_DefSelectedIDList"] == null)
            {
                ViewState["_DefSelectedIDList"] = "";
            }
            return (string)(ViewState["_DefSelectedIDList"]);
        }
        set
        {
            ViewState["_DefSelectedIDList"] = value;
        }
    }


    /// <summary>
    /// 已選取 SelectedIDList
    /// </summary>
    public string ucSelectedIDList
    {
        get
        {
            return (string)(txtSelectedIDList.Text);
        }
    }

    /// <summary>
    /// 已選取 SelectedInfoList
    /// </summary>
    public string ucSelectedInfoList
    {
        get
        {
            return (string)(txtSelectedInfoList.Text);
        }
    }


    /// <summary>
    /// 是否使用切換可見功能(預設 false)
    /// </summary>
    public bool ucIsToggleVisibility
    {
        get
        {
            if (ViewState["_IsToggleVisibility"] == null)
            {
                ViewState["_IsToggleVisibility"] = false;
            }
            return (bool)(ViewState["_IsToggleVisibility"]);
        }
        set
        {
            ViewState["_IsToggleVisibility"] = value;
            if (value == true)
            {
                chkVisibility.Visible = true;
                divDataArea.Style["visibility"] = "";
            }
            else
            {
                chkVisibility.Visible = false;
                divDataArea.Style["visibility"] = "hidden";
            }
        }
    }

    /// <summary>
    /// 控制項可見狀態
    /// </summary>
    public bool ucIsVisibility
    {
        get
        {
            return chkVisibility.Checked;
        }
        set
        {
            chkVisibility.Checked = value;
        }
    }

    /// <summary>
    /// 控制項顯示抬頭(空白時自動隱藏)
    /// </summary>
    public string ucCaption
    {
        get
        {
            return labCaption.Text.Trim();
        }

        set
        {
            value = value.Trim();
            labCaption.Text = value;
            if (string.IsNullOrEmpty(value))
            {
                labCaption.Visible = false;
            }
            else
            {
                labCaption.Visible = true;
            }
        }
    }

    /// <summary>
    /// 控制項顯示抬頭寬度(預設 80)
    /// </summary>
    public int ucCaptionWidth
    {
        get { return int.Parse(labCaption.Width.ToString()); }
        set
        {
            labCaption.Width = Unit.Pixel(value);
        }
    }
    #endregion

    #region 自訂事件
    /// <summary>
    /// 開啟[複選清單]的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Launch(object sender, EventArgs e);
    /// <summary>
    /// 關閉[複選清單]的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void Close(object sender, EventArgs e);
    public event Launch onLaunch;
    public event Close onClose;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
        if (!IsPostBack)
        {
            if (Util.getIEVersion() < 0)
            {
                //for Non IE
                txtSelectedInfoList.Style["white-space"] = "pre";
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(ucCommMultiSelect1.ucSelectedIDList))
            {
                txtSelectedIDList.Text = ucCommMultiSelect1.ucSelectedIDList;
                txtSelectedInfoList.Text = ucCommMultiSelect1.ucSelectedInfoList;
                breakInfoList();
            }
        }

        ucModalPopup1.onClose += new Util_ucModalPopup.btnCloseClick(ucModalPopup1_onClose);
    }

    /// <summary>
    /// 清除目前設定
    /// </summary>
    public void Clear()
    {
        ucDefSelectedIDList = "";
        txtSelectedIDList.Text = "";
        txtSelectedInfoList.Text = "";
        ((Util_ucCommMultiSelect)ucCommMultiSelect1).ucSelectedIDList = "";
        ((Util_ucCommMultiSelect)ucCommMultiSelect1).ucSelectedInfoList = "";
    }

    /// <summary>
    /// 重新整理/初始元件
    /// </summary>
    public void Refresh()
    {
        //chkVisibility 相關設定
        string strRequireJS = "";
        if (ucIsRequire)
        {
            //當 ucIsRequire=true ，還需 chkVisibility.checked 才發生作用
            strRequireJS += "var oValid = document.getElementById('" + RequiredFieldValidator1.ClientID + "');";
            strRequireJS += "if (this.checked) { ValidatorEnable(oValid, true); } else { ValidatorEnable(oValid, false); }";
        }
        strRequireJS += string.Format("Util_ChkBoxToggleVisibility('{0}', '{1}');", chkVisibility.ClientID, divDataArea.ClientID);
        chkVisibility.Attributes.Add("onclick", strRequireJS);
        if (chkVisibility.Checked)
        {
            divDataArea.Style["visibility"] = "";
        }
        else
        {
            divDataArea.Style["visibility"] = "hidden";
        }

        //txtSelectedInfoList 相關設定
        txtSelectedInfoList.Width = ucWidth;
        txtSelectedInfoList.TextMode = (ucRows > 1) ? TextBoxMode.MultiLine : TextBoxMode.SingleLine;
        txtSelectedInfoList.Attributes.Add("onclick", "Util_IsChkDirty = false;__doPostBack('" + btnLaunch.UniqueID + "','');");
        if (!string.IsNullOrEmpty(ucBtnTooltip))
        {
            txtSelectedInfoList.ToolTip = ucBtnTooltip;
        }


        if (!string.IsNullOrEmpty(ucReadOnlyCSS))
        {
            txtSelectedInfoList.CssClass = ucReadOnlyCSS;
        }
        else
        {
            txtSelectedInfoList.CssClass = "";
        }
        //處理預設ID清單
        string[] DefIDList = Util.getFixList(ucDefSelectedIDList.Split(','));
        if (DefIDList != null && DefIDList.Count() > 0 && ucSourceDictionary != null && ucSourceDictionary.Count() > 0)
        {
            Dictionary<string, string> dicDefList = new Dictionary<string, string>();
            for (int i = 0; i < DefIDList.Count(); i++)
            {
                if (ucSourceDictionary.ContainsKey(DefIDList[i]))
                {
                    dicDefList.Add(DefIDList[i], ucSourceDictionary[DefIDList[i]]);
                }
            }

            if (dicDefList.Count > 0)
            {
                txtSelectedIDList.Text = Util.getStringJoin(Util.getArray(dicDefList));
                txtSelectedInfoList.Text = Util.getStringJoin(Util.getArray(dicDefList, 1));
                ucDefSelectedIDList = txtSelectedIDList.Text;
            }
        }

        breakInfoList();
    }


    void ucModalPopup1_onClose(object sender, Util_ucModalPopup.btnCloseEventArgs e)
    {
        txtSelectedIDList.Text = ucCommMultiSelect1.ucSelectedIDList;
        txtSelectedInfoList.Text = ucCommMultiSelect1.ucSelectedInfoList;
        ucDefSelectedIDList = txtSelectedIDList.Text;

        breakInfoList();

        if (onClose != null)
        {
            onClose(this, e);
        }
    }

    protected void btnLaunch_Click(object sender, EventArgs e)
    {
        ucModalPopup1.Reset();
        ucModalPopup1.ucPopupHeader = ucPopupHeader;
        ucModalPopup1.ucPopupHeight = 340;
        ucModalPopup1.ucBtnCloselEnabled = true;
        ucModalPopup1.ucBtnCancelEnabled = false;
        ucModalPopup1.ucBtnCompleteEnabled = false;
        ucCommMultiSelect1.ucSourceDictionary = ucSourceDictionary;
        ucCommMultiSelect1.ucSelectedIDList = (ucSelectedIDList.Length > 0) ? ucSelectedIDList : ucDefSelectedIDList;
        ucCommMultiSelect1.ucSelectedIDListToParentObjClientID = txtSelectedIDList.ClientID;
        ucCommMultiSelect1.ucSelectedInfoListToParentObjClientID = txtSelectedInfoList.ClientID;
        ucCommMultiSelect1.Refresh();

        ucModalPopup1.ucPanelID = ModalPanel.ID;
        ucModalPopup1.Show();

        breakInfoList();

        if (onLaunch != null)
        {
            onLaunch(this, e);
        }
    }


    void breakInfoList()
    {
        if (ucRows > 1 && txtSelectedInfoList.Text.Length > 0)
        {
            txtSelectedInfoList.Text = txtSelectedInfoList.Text.Replace(",", "\n");
        }
    }
}