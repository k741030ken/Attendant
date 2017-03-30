<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowBatchVerify.aspx.cs"
    Inherits="FlowExpress_FlowBatchVerify" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>Batch Verify</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>
    <asp:Label ID="labIsSelfRefresh" runat="server" Text="N" Visible="false"></asp:Label>
    <div>
        <center>
            <asp:Label ID="labFlowVerifyMsg" runat="server" ForeColor="Silver" text="BatchVerify"/>
        </center>
    </div>
    </form>
</body>
</html>
