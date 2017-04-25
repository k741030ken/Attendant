<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrpCellMerge.aspx.cs" Inherits="Sample_Excel_GrpCellMerge" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>
<%@ Register Src="~/Util/ucProcessButton.ascx" TagPrefix="uc1" TagName="ucProcessButton" %>



<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <cc1:HoverTooltip ID="HoverTooltip1" runat="server">
        </cc1:HoverTooltip>
        <div>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">自訂匯出格式</legend>
                <uc1:ucProcessButton runat="server" ID="btnGrpMerge" ucBtnWidth="150" ucBtnCaption="分群並欄位合併匯出" />
                <uc1:ucGridView runat="server" ID="gvMain" />
            </fieldset>
        </div>
    </form>
</body>
</html>
