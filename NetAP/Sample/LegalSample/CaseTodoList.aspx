<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseTodoList.aspx.cs" Inherits="LegalSample_CaseTodoList" UICulture="auto" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/FlowExpress/ucFlowTodoList.ascx" TagName="ucFlowTodoList" TagPrefix="uc1" %>
<!DOCTYPE html >
<html >
<head runat="server">
    <title>Case TodoList</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <uc1:ucFlowTodoList ID="ucFlowTodoList1" runat="server" />
    </div>
    </form>
</body>
</html>
