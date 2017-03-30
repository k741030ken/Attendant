<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowLogDisplay.aspx.cs" Inherits="FlowExpress_FlowLogDisplay"
    UICulture="auto" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ucFlowFullLogList.ascx" TagName="ucFlowFullLogList" TagPrefix="uc1" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>FlowLogDisplay</title>
</head>
<body style="overflow-y:hidden;">
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <asp:Label ID="labLogTitle" Text="FlowLog" runat="server" />
                    <asp:LinkButton ID="btnToggle" runat="server" OnClick="btnToggle_Click">
                        <asp:Image ID="imgLogToggle" runat="server" BorderWidth="0" Visible="false" />
                    </asp:LinkButton>
                </legend>
                <div id="divLogArea" runat="server" >
                    <asp:Label ID="labWaiting" runat="server" Style="display: none;"></asp:Label>
                    <uc1:ucFlowFullLogList ID="ucFlowFullLogList1" runat="server" />
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
