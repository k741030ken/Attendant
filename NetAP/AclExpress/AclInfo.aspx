<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclInfo.aspx.cs" Inherits="AclExpress_AclInfo" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagPrefix="uc1" TagName="ucCascadingDropDown" %>
<%@ Register Src="~/AclExpress/ucAclInfo.ascx" TagPrefix="uc1" TagName="ucAclInfo" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>AclInfo</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <div>
            <fieldset class='Util_Fieldset' style="padding-top: 20px; padding-bottom: 20px;">
                <legend class="Util_Legend">ACL 權限查詢</legend>
                <uc1:ucCascadingDropDown runat="server" ID="ucCascadingDropDown" />
                <span style="margin-left: 20px;">
                    <asp:Button ID="btnQry" runat="server" Text="查　詢" CssClass="Util_clsBtnGray" OnClick="btnQry_Click" /></span>
                <br />
            </fieldset>
            <div id="divQryResult" runat="server" visible="false" style="margin-top: 10px;">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">查詢結果</legend>
                    <uc1:ucAclInfo runat="server" ID="ucAclInfo" />
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
