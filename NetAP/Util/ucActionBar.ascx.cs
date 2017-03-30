using System;
using System.Collections;
using System.Web.UI.WebControls;
using SinoPac.WebExpress.Common;
using RS = SinoPac.WebExpress.Common.Properties;
using SinoPac.WebExpress.Work;

/// <summary>
/// [功能按鈕列]控制項
/// <para></para>
/// <para>**可利用此控制項，定義此頁面可讓使用者使用的功能按鈕**</para>
/// </summary>
public partial class Util_ucActionBar : BaseUserControl
{

    #region 屬性
    /// <summary>
    /// 套用的CSS Class(預設 Util_ActionBar)
    /// </summary>
    public string ucCssClass
    {
        get
        {
            if (ViewState["_CssClass"] == null)
            {
                ViewState["_CssClass"] = "Util_ActionBar";
            }
            return (string)(ViewState["_CssClass"]);
        }
        set
        {
            ViewState["_CssClass"] = value;
        }
    }

    /// <summary>
    /// 套用的自訂 Style (預設 [border: 1px solid #C0C0C0;])
    /// </summary>
    public string ucCustStyle
    {
        get
        {
            if (ViewState["_CustStyle"] == null)
            {
                ViewState["_CustStyle"] = "border: 1px solid #C0C0C0;line-height:28px;";
            }
            return (string)(ViewState["_CustStyle"]);
        }
        set
        {
            ViewState["_CustStyle"] = value;
        }
    }

    /// <summary>
    /// 功能按鈕的CSS Class(預設 [Util_clsBtnGray + Util_Pointer])
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (ViewState["_BtnCssClass"] == null)
            {
                ViewState["_BtnCssClass"] = "Util_clsBtnGray Util_Pointer";
            }
            return (string)(ViewState["_BtnCssClass"]);
        }
        set
        {
            ViewState["_BtnCssClass"] = value;
        }
    }

    /// <summary>
    /// 功能按鈕是否觸發CausesValidation(預設 false)
    /// </summary>
    public bool ucBtnCausesValidation
    {
        get
        {
            if (ViewState["_BtnCausesValidation"] == null)
            {
                ViewState["_BtnCausesValidation"] = false;
            }
            return (bool)(ViewState["_BtnCausesValidation"]);
        }
        set
        {
            ViewState["_BtnCausesValidation"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucButtonWidth
    {
        get
        {
            if (ViewState["_ButtonWidth"] == null)
            {
                ViewState["_ButtonWidth"] = 80;
            }
            return (int)(ViewState["_ButtonWidth"]);
        }
        set
        {
            ViewState["_ButtonWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕高度(預設 26)
    /// </summary>
    public int ucButtonHeight
    {
        get
        {
            if (ViewState["_ButtonHeight"] == null)
            {
                ViewState["_ButtonHeight"] = 26;
            }
            return (int)(ViewState["_ButtonHeight"]);
        }
        set
        {
            ViewState["_ButtonHeight"] = value;
        }
    }

    /// <summary>
    /// 是否啟用ACL相關控制(預設false)
    /// <remarks>**當頁面繼承自 [AclPage] 才會生效**</remarks>
    /// </summary>
    public bool ucAclEnabled
    {
        get
        {
            if (ViewState["_AclEnabled"] == null)
            {
                ViewState["_AclEnabled"] = false;
            }
            return (bool)(ViewState["_AclEnabled"]);
        }
        set
        {
            ViewState["_AclEnabled"] = value;
            if ((bool)value == true)
            {
                //需為 AclPage 才生效
                ViewState["_AclEnabled"] = AclExpress.IsAclPage();
            }
        }
    }

    /// <summary>
    /// 使用[Query]功能(預設false)
    /// </summary>
    public bool ucQueryEnabled
    {
        get
        {
            if (ViewState["_QueryEnabled"] == null)
            {
                ViewState["_QueryEnabled"] = false;
            }
            return (bool)(ViewState["_QueryEnabled"]);
        }
        set
        {
            ViewState["_QueryEnabled"] = value;
        }
    }

    /// <summary>
    /// [Query]按鈕抬頭
    /// </summary>
    public string ucQueryCaption
    {
        get
        {
            if (ViewState["_QueryCaption"] == null)
            {
                ViewState["_QueryCaption"] = RS.Resources.ActionBar_Query;
            }
            return ViewState["_QueryCaption"].ToString();
        }
        set
        {
            ViewState["_QueryCaption"] = value;
        }
    }

    /// <summary>
    /// [Query]提示訊息
    /// </summary>
    public string ucQueryToolTip
    {
        get
        {
            if (ViewState["_QueryToolTip"] == null)
            {
                ViewState["_QueryToolTip"] = RS.Resources.ActionBar_Query;
            }
            return ViewState["_QueryToolTip"].ToString();
        }
        set
        {
            ViewState["_QueryToolTip"] = value;
        }
    }


    /// <summary>
    /// 使用[Add]功能(預設false)
    /// </summary>
    public bool ucAddEnabled
    {
        get
        {
            if (ViewState["_AddEnabled"] == null)
            {
                ViewState["_AddEnabled"] = false;
            }
            return (bool)(ViewState["_AddEnabled"]);
        }
        set
        {
            ViewState["_AddEnabled"] = value;
        }
    }

    /// <summary>
    /// [Add]按鈕抬頭
    /// </summary>
    public string ucAddCaption
    {
        get
        {
            if (ViewState["_AddCaption"] == null)
            {
                ViewState["_AddCaption"] = RS.Resources.ActionBar_Add;
            }
            return ViewState["_AddCaption"].ToString();
        }
        set
        {
            ViewState["_AddCaption"] = value;
        }
    }

    /// <summary>
    /// [Add]提示訊息
    /// </summary>
    public string ucAddToolTip
    {
        get
        {
            if (ViewState["_AddToolTip"] == null)
            {
                ViewState["_AddToolTip"] = RS.Resources.ActionBar_Add;
            }
            return ViewState["_AddToolTip"].ToString();
        }
        set
        {
            ViewState["_AddToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Edit]功能(預設false)
    /// </summary>
    public bool ucEditEnabled
    {
        get
        {
            if (ViewState["_EditEnabled"] == null)
            {
                ViewState["_EditEnabled"] = false;
            }
            return (bool)(ViewState["_EditEnabled"]);
        }
        set
        {
            ViewState["_EditEnabled"] = value;
        }
    }

    /// <summary>
    /// [Edit]按鈕抬頭
    /// </summary>
    public string ucEditCaption
    {
        get
        {
            if (ViewState["_EditCaption"] == null)
            {
                ViewState["_EditCaption"] = RS.Resources.ActionBar_Edit;
            }
            return ViewState["_EditCaption"].ToString();
        }
        set
        {
            ViewState["_EditCaption"] = value;
        }
    }

    /// <summary>
    /// [Edit]提示訊息
    /// </summary>
    public string ucEditToolTip
    {
        get
        {
            if (ViewState["_EditToolTip"] == null)
            {
                ViewState["_EditToolTip"] = RS.Resources.ActionBar_Edit;
            }
            return ViewState["_EditToolTip"].ToString();
        }
        set
        {
            ViewState["_EditToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Delete]功能(預設false)
    /// </summary>
    public bool ucDeleteEnabled
    {
        get
        {
            if (ViewState["_DeleteEnabled"] == null)
            {
                ViewState["_DeleteEnabled"] = false;
            }
            return (bool)(ViewState["_DeleteEnabled"]);
        }
        set
        {
            ViewState["_DeleteEnabled"] = value;
        }
    }

    /// <summary>
    /// [Delete]按鈕抬頭
    /// </summary>
    public string ucDeleteCaption
    {
        get
        {
            if (ViewState["_DeleteCaption"] == null)
            {
                ViewState["_DeleteCaption"] = RS.Resources.ActionBar_Delete;
            }
            return ViewState["_DeleteCaption"].ToString();
        }
        set
        {
            ViewState["_DeleteCaption"] = value;
        }
    }

    /// <summary>
    /// [Delete]提示訊息
    /// </summary>
    public string ucDeleteToolTip
    {
        get
        {
            if (ViewState["_DeleteToolTip"] == null)
            {
                ViewState["_DeleteToolTip"] = RS.Resources.ActionBar_Delete;
            }
            return ViewState["_DeleteToolTip"].ToString();
        }
        set
        {
            ViewState["_DeleteToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Copy]功能(預設false)
    /// </summary>
    public bool ucCopyEnabled
    {
        get
        {
            if (ViewState["_CopyEnabled"] == null)
            {
                ViewState["_CopyEnabled"] = false;
            }
            return (bool)(ViewState["_CopyEnabled"]);
        }
        set
        {
            ViewState["_CopyEnabled"] = value;
        }
    }

    /// <summary>
    /// [Copy]按鈕抬頭
    /// </summary>
    public string ucCopyCaption
    {
        get
        {
            if (ViewState["_CopyCaption"] == null)
            {
                ViewState["_CopyCaption"] = RS.Resources.ActionBar_Copy;
            }
            return ViewState["_CopyCaption"].ToString();
        }
        set
        {
            ViewState["_CopyCaption"] = value;
        }
    }

    /// <summary>
    /// [Copy]提示訊息
    /// </summary>
    public string ucCopyToolTip
    {
        get
        {
            if (ViewState["_CopyToolTip"] == null)
            {
                ViewState["_CopyToolTip"] = RS.Resources.ActionBar_Copy;
            }
            return ViewState["_CopyToolTip"].ToString();
        }
        set
        {
            ViewState["_CopyToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Export]功能(預設false)
    /// </summary>
    public bool ucExportEnabled
    {
        get
        {
            if (ViewState["_ExportEnabled"] == null)
            {
                ViewState["_ExportEnabled"] = false;
            }
            return (bool)(ViewState["_ExportEnabled"]);
        }
        set
        {
            ViewState["_ExportEnabled"] = value;
        }
    }

    /// <summary>
    /// [Export]按鈕抬頭
    /// </summary>
    public string ucExportCaption
    {
        get
        {
            if (ViewState["_ExportCaption"] == null)
            {
                ViewState["_ExportCaption"] = RS.Resources.ActionBar_Export;
            }
            return ViewState["_ExportCaption"].ToString();
        }
        set
        {
            ViewState["_ExportCaption"] = value;
        }
    }

    /// <summary>
    /// [Export]提示訊息
    /// </summary>
    public string ucExportToolTip
    {
        get
        {
            if (ViewState["_ExportToolTip"] == null)
            {
                ViewState["_ExportToolTip"] = RS.Resources.ActionBar_Export;
            }
            return ViewState["_ExportToolTip"].ToString();
        }
        set
        {
            ViewState["_ExportToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Download]功能(預設false)
    /// </summary>
    public bool ucDownloadEnabled
    {
        get
        {
            if (ViewState["_DownloadEnabled"] == null)
            {
                ViewState["_DownloadEnabled"] = false;
            }
            return (bool)(ViewState["_DownloadEnabled"]);
        }
        set
        {
            ViewState["_DownloadEnabled"] = value;
        }
    }

    /// <summary>
    /// [Download]按鈕抬頭
    /// </summary>
    public string ucDownloadCaption
    {
        get
        {
            if (ViewState["_DownloadCaption"] == null)
            {
                ViewState["_DownloadCaption"] = RS.Resources.ActionBar_Download;
            }
            return ViewState["_DownloadCaption"].ToString();
        }
        set
        {
            ViewState["_DownloadCaption"] = value;
        }
    }

    /// <summary>
    /// [Download]提示訊息
    /// </summary>
    public string ucDownloadToolTip
    {
        get
        {
            if (ViewState["_DownloadToolTip"] == null)
            {
                ViewState["_DownloadToolTip"] = RS.Resources.ActionBar_Download;
            }
            return ViewState["_DownloadToolTip"].ToString();
        }
        set
        {
            ViewState["_DownloadToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Information]功能(預設false)
    /// </summary>
    public bool ucInformationEnabled
    {
        get
        {
            if (ViewState["_InformationEnabled"] == null)
            {
                ViewState["_InformationEnabled"] = false;
            }
            return (bool)(ViewState["_InformationEnabled"]);
        }
        set
        {
            ViewState["_InformationEnabled"] = value;
        }
    }

    /// <summary>
    /// [Information]按鈕抬頭
    /// </summary>
    public string ucInformationCaption
    {
        get
        {
            if (ViewState["_InformationCaption"] == null)
            {
                ViewState["_InformationCaption"] = RS.Resources.ActionBar_Information;
            }
            return ViewState["_InformationCaption"].ToString();
        }
        set
        {
            ViewState["_InformationCaption"] = value;
        }
    }

    /// <summary>
    /// [Information]提示訊息
    /// </summary>
    public string ucInformationToolTip
    {
        get
        {
            if (ViewState["_InformationToolTip"] == null)
            {
                ViewState["_InformationToolTip"] = RS.Resources.ActionBar_Information;
            }
            return ViewState["_InformationToolTip"].ToString();
        }
        set
        {
            ViewState["_InformationToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Multilingual]功能(預設false)
    /// </summary>
    public bool ucMultilingualEnabled
    {
        get
        {
            if (ViewState["_MultilingualEnabled"] == null)
            {
                ViewState["_MultilingualEnabled"] = false;
            }
            return (bool)(ViewState["_MultilingualEnabled"]);
        }
        set
        {
            ViewState["_MultilingualEnabled"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]按鈕抬頭
    /// </summary>
    public string ucMultilingualCaption
    {
        get
        {
            if (ViewState["_MultilingualCaption"] == null)
            {
                ViewState["_MultilingualCaption"] = RS.Resources.ActionBar_Multilingual;
            }
            return ViewState["_MultilingualCaption"].ToString();
        }
        set
        {
            ViewState["_MultilingualCaption"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]提示訊息
    /// </summary>
    public string ucMultilingualToolTip
    {
        get
        {
            if (ViewState["_MultilingualToolTip"] == null)
            {
                ViewState["_MultilingualToolTip"] = RS.Resources.ActionBar_Multilingual;
            }
            return ViewState["_MultilingualToolTip"].ToString();
        }
        set
        {
            ViewState["_MultilingualToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Print]功能(預設false)
    /// </summary>
    public bool ucPrintEnabled
    {
        get
        {
            if (ViewState["_PrintEnabled"] == null)
            {
                ViewState["_PrintEnabled"] = false;
            }
            return (bool)(ViewState["_PrintEnabled"]);
        }
        set
        {
            ViewState["_PrintEnabled"] = value;
        }
    }

    /// <summary>
    /// [Print]按鈕抬頭
    /// </summary>
    public string ucPrintCaption
    {
        get
        {
            if (ViewState["_PrintCaption"] == null)
            {
                ViewState["_PrintCaption"] = RS.Resources.ActionBar_Print;
            }
            return ViewState["_PrintCaption"].ToString();
        }
        set
        {
            ViewState["_PrintCaption"] = value;
        }
    }

    /// <summary>
    /// [Print]提示訊息
    /// </summary>
    public string ucPrintToolTip
    {
        get
        {
            if (ViewState["_PrintToolTip"] == null)
            {
                ViewState["_PrintToolTip"] = RS.Resources.ActionBar_Print;
            }
            return ViewState["_PrintToolTip"].ToString();
        }
        set
        {
            ViewState["_PrintToolTip"] = value;
        }
    }

    #endregion

    #region BarCommand 相關
    protected void barButton_Click(object sender, EventArgs e)
    {
        Button oBtn = (Button)sender;
        BarCommandEventArgs oArgs = new BarCommandEventArgs();
        oArgs.CommandName = oBtn.CommandName;
        if (BarCommand != null)
        {
            BarCommand(this, oArgs);
        }
    }

    /// <summary>
    /// [功能按鈕列] 控制項 BarCommand 事件參數
    /// </summary>
    public class BarCommandEventArgs : EventArgs
    {
        string _CommandName;
        /// <summary>
        /// 命令名稱
        /// </summary>
        public string CommandName
        {
            set { _CommandName = value; }
            get { return _CommandName; }
        }
    }

    /// <summary>
    /// [功能按鈕列] 控制項 ActionBar 事件委派 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ActionBarClick(object sender, BarCommandEventArgs e);

    /// <summary>
    /// [功能按鈕列] 控制項 BarCommand 事件
    /// </summary>
    public event ActionBarClick BarCommand;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Refresh();
    }

    /// <summary>
    /// 重置所有設定
    /// </summary>
    public void Reset()
    {
        this.ViewState.Clear();
    }

    /// <summary>
    /// 重新整理
    /// </summary>
    public void Refresh()
    {
        divActionBar.Visible = true;
        labErrMsg.Visible = false;
        if (!string.IsNullOrEmpty(ucCssClass))
        {
            divActionBar.Attributes["class"] = ucCssClass;
        }

        if (!string.IsNullOrEmpty(ucCustStyle))
        {
            divActionBar.Attributes["style"] = ucCustStyle;
        }

        Button tmpBtn;
        ArrayList tmpBtnList = new ArrayList();
        //Query
        tmpBtn = (Button)this.FindControl("btnQuery");
        tmpBtn.CommandName = "cmdQuery";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucQueryCaption;
        tmpBtn.ToolTip = this.ucQueryToolTip;
        tmpBtn.Visible = this.ucQueryEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Query);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Add
        tmpBtn = (Button)this.FindControl("btnAdd");
        tmpBtn.CommandName = "cmdAdd";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucAddCaption;
        tmpBtn.ToolTip = this.ucAddToolTip;
        tmpBtn.Visible = this.ucAddEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Add);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Edit
        tmpBtn = (Button)this.FindControl("btnEdit");
        tmpBtn.CommandName = "cmdEdit";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucEditCaption;
        tmpBtn.ToolTip = this.ucEditToolTip;
        tmpBtn.Visible = this.ucEditEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Edit);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Delete
        tmpBtn = (Button)this.FindControl("btnDelete");
        tmpBtn.CommandName = "cmdDelete";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucDeleteCaption;
        tmpBtn.ToolTip = this.ucDeleteToolTip;
        tmpBtn.Visible = this.ucDeleteEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Delete);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Copy
        tmpBtn = (Button)this.FindControl("btnCopy");
        tmpBtn.CommandName = "cmdCopy";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucCopyCaption;
        tmpBtn.ToolTip = this.ucCopyToolTip;
        tmpBtn.Visible = this.ucCopyEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Copy);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Export
        tmpBtn = (Button)this.FindControl("btnExport");
        tmpBtn.CommandName = "cmdExport";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucExportCaption;
        tmpBtn.ToolTip = this.ucExportToolTip;
        tmpBtn.Visible = this.ucExportEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Export);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Download
        tmpBtn = (Button)this.FindControl("btnDownload");
        tmpBtn.CommandName = "cmdDownload";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucDownloadCaption;
        tmpBtn.ToolTip = this.ucDownloadToolTip;
        tmpBtn.Visible = this.ucDownloadEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Download);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Information
        tmpBtn = (Button)this.FindControl("btnInformation");
        tmpBtn.CommandName = "cmdInformation";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucInformationCaption;
        tmpBtn.ToolTip = this.ucInformationToolTip;
        tmpBtn.Visible = this.ucInformationEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Information);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Multilingual
        tmpBtn = (Button)this.FindControl("btnMultilingual");
        tmpBtn.CommandName = "cmdMultilingual";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucMultilingualCaption;
        tmpBtn.ToolTip = this.ucMultilingualToolTip;
        tmpBtn.Visible = this.ucMultilingualEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Multilingual);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        //Print
        tmpBtn = (Button)this.FindControl("btnPrint");
        tmpBtn.CommandName = "cmdPrint";
        tmpBtn.CommandArgument = "";
        tmpBtn.CssClass = ucBtnCssClass;
        tmpBtn.Width = ucButtonWidth;
        tmpBtn.Height = ucButtonHeight;
        tmpBtn.CausesValidation = this.ucBtnCausesValidation;
        tmpBtn.Text = this.ucPrintCaption;
        tmpBtn.ToolTip = this.ucPrintToolTip;
        tmpBtn.Visible = this.ucPrintEnabled;
        if (ucAclEnabled && tmpBtn.Visible) tmpBtn.Visible = AclExpress.IsAclPageAuthAct(AclExpress.AclAuthActList.Print);
        if (tmpBtn.Visible) tmpBtnList.Add(tmpBtn.CommandName);

        if (tmpBtnList.Count <= 0)
        {
            divActionBar.Visible = false;
            labErrMsg.Visible = true;
            labErrMsg.Text = Util.getHtmlMessage(Util.HtmlMessageKind.Error, string.Format(RS.Resources.Msg_AccessDenied1, "ActionBar"));
        }
    }

}