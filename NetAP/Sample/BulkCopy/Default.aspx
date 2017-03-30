<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sample_BulkCopy_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucProcessButton.ascx" TagPrefix="uc1" TagName="ucProcessButton" %>
<%@ Register Src="~/Util/ucUploadButton.ascx" TagPrefix="uc1" TagName="ucUploadButton" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>BulkCopy Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
        <uc1:ucLightBox runat="server" ID="ucLightBox" />
        <asp:HiddenField ID="txtAction" runat="server" Value="" />

        <div id="divMsg" runat="server" style="display: none;">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">批量作業結果</legend>
                <asp:Label ID="labMsg" runat="server"></asp:Label>
            </fieldset>
        </div>

        <div id="divMain" runat="server">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">批量資料作業</legend>
                <div class=" Util_Center">
                    <span style="margin-right: 10px;">
                        <uc1:ucProcessButton runat="server" ID="btnCreate" ucBtnCaption="產生並下載測試資料" ucBtnWidth="200" ucBtnCssClass="Util_clsBtn" />
                    </span>
                    <span>
                        <uc1:ucUploadButton runat="server" ID="btnUpload" ucBtnCaption="上傳匯入資料" ucBtnWidth="200" ucBtnCssClass="Util_clsBtn" />
                    </span>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
