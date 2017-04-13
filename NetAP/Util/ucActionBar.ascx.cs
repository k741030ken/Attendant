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
            if (PageViewState["_CssClass"] == null)
            {
                PageViewState["_CssClass"] = "Util_ActionBar";
            }
            return (string)(PageViewState["_CssClass"]);
        }
        set
        {
            PageViewState["_CssClass"] = value;
        }
    }

    /// <summary>
    /// 套用的自訂 Style (預設 [border: 1px solid #C0C0C0;])
    /// </summary>
    public string ucCustStyle
    {
        get
        {
            if (PageViewState["_CustStyle"] == null)
            {
                PageViewState["_CustStyle"] = "border: 1px solid #C0C0C0;line-height:28px;";
            }
            return (string)(PageViewState["_CustStyle"]);
        }
        set
        {
            PageViewState["_CustStyle"] = value;
        }
    }

    /// <summary>
    /// 功能按鈕的CSS Class(預設 [Util_clsBtnGray + Util_Pointer])
    /// </summary>
    public string ucBtnCssClass
    {
        get
        {
            if (PageViewState["_BtnCssClass"] == null)
            {
                PageViewState["_BtnCssClass"] = "Util_clsBtnGray Util_Pointer";
            }
            return (string)(PageViewState["_BtnCssClass"]);
        }
        set
        {
            PageViewState["_BtnCssClass"] = value;
        }
    }

    /// <summary>
    /// 功能按鈕是否觸發CausesValidation(預設 false)
    /// </summary>
    public bool ucBtnCausesValidation
    {
        get
        {
            if (PageViewState["_BtnCausesValidation"] == null)
            {
                PageViewState["_BtnCausesValidation"] = false;
            }
            return (bool)(PageViewState["_BtnCausesValidation"]);
        }
        set
        {
            PageViewState["_BtnCausesValidation"] = value;
        }
    }

    /// <summary>
    /// 按鈕寬度(預設 80)
    /// </summary>
    public int ucButtonWidth
    {
        get
        {
            if (PageViewState["_ButtonWidth"] == null)
            {
                PageViewState["_ButtonWidth"] = 80;
            }
            return (int)(PageViewState["_ButtonWidth"]);
        }
        set
        {
            PageViewState["_ButtonWidth"] = value;
        }
    }

    /// <summary>
    /// 按鈕高度(預設 26)
    /// </summary>
    public int ucButtonHeight
    {
        get
        {
            if (PageViewState["_ButtonHeight"] == null)
            {
                PageViewState["_ButtonHeight"] = 26;
            }
            return (int)(PageViewState["_ButtonHeight"]);
        }
        set
        {
            PageViewState["_ButtonHeight"] = value;
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
            if (PageViewState["_AclEnabled"] == null)
            {
                PageViewState["_AclEnabled"] = false;
            }
            return (bool)(PageViewState["_AclEnabled"]);
        }
        set
        {
            PageViewState["_AclEnabled"] = value;
            if ((bool)value == true)
            {
                //需為 AclPage 才生效
                PageViewState["_AclEnabled"] = AclExpress.IsAclPage();
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
            if (PageViewState["_QueryEnabled"] == null)
            {
                PageViewState["_QueryEnabled"] = false;
            }
            return (bool)(PageViewState["_QueryEnabled"]);
        }
        set
        {
            PageViewState["_QueryEnabled"] = value;
        }
    }

    /// <summary>
    /// [Query]按鈕抬頭
    /// </summary>
    public string ucQueryCaption
    {
        get
        {
            if (PageViewState["_QueryCaption"] == null)
            {
                PageViewState["_QueryCaption"] = RS.Resources.ActionBar_Query;
            }
            return PageViewState["_QueryCaption"].ToString();
        }
        set
        {
            PageViewState["_QueryCaption"] = value;
        }
    }

    /// <summary>
    /// [Query]提示訊息
    /// </summary>
    public string ucQueryToolTip
    {
        get
        {
            if (PageViewState["_QueryToolTip"] == null)
            {
                PageViewState["_QueryToolTip"] = RS.Resources.ActionBar_Query;
            }
            return PageViewState["_QueryToolTip"].ToString();
        }
        set
        {
            PageViewState["_QueryToolTip"] = value;
        }
    }


    /// <summary>
    /// 使用[Add]功能(預設false)
    /// </summary>
    public bool ucAddEnabled
    {
        get
        {
            if (PageViewState["_AddEnabled"] == null)
            {
                PageViewState["_AddEnabled"] = false;
            }
            return (bool)(PageViewState["_AddEnabled"]);
        }
        set
        {
            PageViewState["_AddEnabled"] = value;
        }
    }

    /// <summary>
    /// [Add]按鈕抬頭
    /// </summary>
    public string ucAddCaption
    {
        get
        {
            if (PageViewState["_AddCaption"] == null)
            {
                PageViewState["_AddCaption"] = RS.Resources.ActionBar_Add;
            }
            return PageViewState["_AddCaption"].ToString();
        }
        set
        {
            PageViewState["_AddCaption"] = value;
        }
    }

    /// <summary>
    /// [Add]提示訊息
    /// </summary>
    public string ucAddToolTip
    {
        get
        {
            if (PageViewState["_AddToolTip"] == null)
            {
                PageViewState["_AddToolTip"] = RS.Resources.ActionBar_Add;
            }
            return PageViewState["_AddToolTip"].ToString();
        }
        set
        {
            PageViewState["_AddToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Edit]功能(預設false)
    /// </summary>
    public bool ucEditEnabled
    {
        get
        {
            if (PageViewState["_EditEnabled"] == null)
            {
                PageViewState["_EditEnabled"] = false;
            }
            return (bool)(PageViewState["_EditEnabled"]);
        }
        set
        {
            PageViewState["_EditEnabled"] = value;
        }
    }

    /// <summary>
    /// [Edit]按鈕抬頭
    /// </summary>
    public string ucEditCaption
    {
        get
        {
            if (PageViewState["_EditCaption"] == null)
            {
                PageViewState["_EditCaption"] = RS.Resources.ActionBar_Edit;
            }
            return PageViewState["_EditCaption"].ToString();
        }
        set
        {
            PageViewState["_EditCaption"] = value;
        }
    }

    /// <summary>
    /// [Edit]提示訊息
    /// </summary>
    public string ucEditToolTip
    {
        get
        {
            if (PageViewState["_EditToolTip"] == null)
            {
                PageViewState["_EditToolTip"] = RS.Resources.ActionBar_Edit;
            }
            return PageViewState["_EditToolTip"].ToString();
        }
        set
        {
            PageViewState["_EditToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Delete]功能(預設false)
    /// </summary>
    public bool ucDeleteEnabled
    {
        get
        {
            if (PageViewState["_DeleteEnabled"] == null)
            {
                PageViewState["_DeleteEnabled"] = false;
            }
            return (bool)(PageViewState["_DeleteEnabled"]);
        }
        set
        {
            PageViewState["_DeleteEnabled"] = value;
        }
    }

    /// <summary>
    /// [Delete]按鈕抬頭
    /// </summary>
    public string ucDeleteCaption
    {
        get
        {
            if (PageViewState["_DeleteCaption"] == null)
            {
                PageViewState["_DeleteCaption"] = RS.Resources.ActionBar_Delete;
            }
            return PageViewState["_DeleteCaption"].ToString();
        }
        set
        {
            PageViewState["_DeleteCaption"] = value;
        }
    }

    /// <summary>
    /// [Delete]提示訊息
    /// </summary>
    public string ucDeleteToolTip
    {
        get
        {
            if (PageViewState["_DeleteToolTip"] == null)
            {
                PageViewState["_DeleteToolTip"] = RS.Resources.ActionBar_Delete;
            }
            return PageViewState["_DeleteToolTip"].ToString();
        }
        set
        {
            PageViewState["_DeleteToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Copy]功能(預設false)
    /// </summary>
    public bool ucCopyEnabled
    {
        get
        {
            if (PageViewState["_CopyEnabled"] == null)
            {
                PageViewState["_CopyEnabled"] = false;
            }
            return (bool)(PageViewState["_CopyEnabled"]);
        }
        set
        {
            PageViewState["_CopyEnabled"] = value;
        }
    }

    /// <summary>
    /// [Copy]按鈕抬頭
    /// </summary>
    public string ucCopyCaption
    {
        get
        {
            if (PageViewState["_CopyCaption"] == null)
            {
                PageViewState["_CopyCaption"] = RS.Resources.ActionBar_Copy;
            }
            return PageViewState["_CopyCaption"].ToString();
        }
        set
        {
            PageViewState["_CopyCaption"] = value;
        }
    }

    /// <summary>
    /// [Copy]提示訊息
    /// </summary>
    public string ucCopyToolTip
    {
        get
        {
            if (PageViewState["_CopyToolTip"] == null)
            {
                PageViewState["_CopyToolTip"] = RS.Resources.ActionBar_Copy;
            }
            return PageViewState["_CopyToolTip"].ToString();
        }
        set
        {
            PageViewState["_CopyToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Export]功能(預設false)
    /// </summary>
    public bool ucExportEnabled
    {
        get
        {
            if (PageViewState["_ExportEnabled"] == null)
            {
                PageViewState["_ExportEnabled"] = false;
            }
            return (bool)(PageViewState["_ExportEnabled"]);
        }
        set
        {
            PageViewState["_ExportEnabled"] = value;
        }
    }

    /// <summary>
    /// [Export]按鈕抬頭
    /// </summary>
    public string ucExportCaption
    {
        get
        {
            if (PageViewState["_ExportCaption"] == null)
            {
                PageViewState["_ExportCaption"] = RS.Resources.ActionBar_Export;
            }
            return PageViewState["_ExportCaption"].ToString();
        }
        set
        {
            PageViewState["_ExportCaption"] = value;
        }
    }

    /// <summary>
    /// [Export]提示訊息
    /// </summary>
    public string ucExportToolTip
    {
        get
        {
            if (PageViewState["_ExportToolTip"] == null)
            {
                PageViewState["_ExportToolTip"] = RS.Resources.ActionBar_Export;
            }
            return PageViewState["_ExportToolTip"].ToString();
        }
        set
        {
            PageViewState["_ExportToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Download]功能(預設false)
    /// </summary>
    public bool ucDownloadEnabled
    {
        get
        {
            if (PageViewState["_DownloadEnabled"] == null)
            {
                PageViewState["_DownloadEnabled"] = false;
            }
            return (bool)(PageViewState["_DownloadEnabled"]);
        }
        set
        {
            PageViewState["_DownloadEnabled"] = value;
        }
    }

    /// <summary>
    /// [Download]按鈕抬頭
    /// </summary>
    public string ucDownloadCaption
    {
        get
        {
            if (PageViewState["_DownloadCaption"] == null)
            {
                PageViewState["_DownloadCaption"] = RS.Resources.ActionBar_Download;
            }
            return PageViewState["_DownloadCaption"].ToString();
        }
        set
        {
            PageViewState["_DownloadCaption"] = value;
        }
    }

    /// <summary>
    /// [Download]提示訊息
    /// </summary>
    public string ucDownloadToolTip
    {
        get
        {
            if (PageViewState["_DownloadToolTip"] == null)
            {
                PageViewState["_DownloadToolTip"] = RS.Resources.ActionBar_Download;
            }
            return PageViewState["_DownloadToolTip"].ToString();
        }
        set
        {
            PageViewState["_DownloadToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Information]功能(預設false)
    /// </summary>
    public bool ucInformationEnabled
    {
        get
        {
            if (PageViewState["_InformationEnabled"] == null)
            {
                PageViewState["_InformationEnabled"] = false;
            }
            return (bool)(PageViewState["_InformationEnabled"]);
        }
        set
        {
            PageViewState["_InformationEnabled"] = value;
        }
    }

    /// <summary>
    /// [Information]按鈕抬頭
    /// </summary>
    public string ucInformationCaption
    {
        get
        {
            if (PageViewState["_InformationCaption"] == null)
            {
                PageViewState["_InformationCaption"] = RS.Resources.ActionBar_Information;
            }
            return PageViewState["_InformationCaption"].ToString();
        }
        set
        {
            PageViewState["_InformationCaption"] = value;
        }
    }

    /// <summary>
    /// [Information]提示訊息
    /// </summary>
    public string ucInformationToolTip
    {
        get
        {
            if (PageViewState["_InformationToolTip"] == null)
            {
                PageViewState["_InformationToolTip"] = RS.Resources.ActionBar_Information;
            }
            return PageViewState["_InformationToolTip"].ToString();
        }
        set
        {
            PageViewState["_InformationToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Multilingual]功能(預設false)
    /// </summary>
    public bool ucMultilingualEnabled
    {
        get
        {
            if (PageViewState["_MultilingualEnabled"] == null)
            {
                PageViewState["_MultilingualEnabled"] = false;
            }
            return (bool)(PageViewState["_MultilingualEnabled"]);
        }
        set
        {
            PageViewState["_MultilingualEnabled"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]按鈕抬頭
    /// </summary>
    public string ucMultilingualCaption
    {
        get
        {
            if (PageViewState["_MultilingualCaption"] == null)
            {
                PageViewState["_MultilingualCaption"] = RS.Resources.ActionBar_Multilingual;
            }
            return PageViewState["_MultilingualCaption"].ToString();
        }
        set
        {
            PageViewState["_MultilingualCaption"] = value;
        }
    }

    /// <summary>
    /// [Multilingual]提示訊息
    /// </summary>
    public string ucMultilingualToolTip
    {
        get
        {
            if (PageViewState["_MultilingualToolTip"] == null)
            {
                PageViewState["_MultilingualToolTip"] = RS.Resources.ActionBar_Multilingual;
            }
            return PageViewState["_MultilingualToolTip"].ToString();
        }
        set
        {
            PageViewState["_MultilingualToolTip"] = value;
        }
    }

    /// <summary>
    /// 使用[Print]功能(預設false)
    /// </summary>
    public bool ucPrintEnabled
    {
        get
        {
            if (PageViewState["_PrintEnabled"] == null)
            {
                PageViewState["_PrintEnabled"] = false;
            }
            return (bool)(PageViewState["_PrintEnabled"]);
        }
        set
        {
            PageViewState["_PrintEnabled"] = value;
        }
    }

    /// <summary>
    /// [Print]按鈕抬頭
    /// </summary>
    public string ucPrintCaption
    {
        get
        {
            if (PageViewState["_PrintCaption"] == null)
            {
                PageViewState["_PrintCaption"] = RS.Resources.ActionBar_Print;
            }
            return PageViewState["_PrintCaption"].ToString();
        }
        set
        {
            PageViewState["_PrintCaption"] = value;
        }
    }

    /// <summary>
    /// [Print]提示訊息
    /// </summary>
    public string ucPrintToolTip
    {
        get
        {
            if (PageViewState["_PrintToolTip"] == null)
            {
                PageViewState["_PrintToolTip"] = RS.Resources.ActionBar_Print;
            }
            return PageViewState["_PrintToolTip"].ToString();
        }
        set
        {
            PageViewState["_PrintToolTip"] = value;
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
        this.PageViewState.Clear();
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