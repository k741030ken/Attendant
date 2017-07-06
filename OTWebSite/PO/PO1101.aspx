<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO1101.aspx.vb" Inherits="PO_PO1101" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>打卡特殊單位參數設定</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table class="tbl_Content" style="width: 100%; height: 100%">
        <tr runat="server">
            <td width="10%" runat="server" class="td_Edit">
                <asp:Label ID="Label1" runat="server" Text="公司" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" runat="server" class="td_Edit">
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>

        </tr>
        <tr runat="server">
            <td width="10%" class="td_Edit">
                <asp:Label ID="lblAreaCode" runat="server" Text="單位" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="td_Edit">
                <asp:DropDownList ID="ddlDeptID" AutoPostBack="true" runat="server" Font-Names="微軟正黑體"></asp:DropDownList>                                        
                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體"></asp:DropDownList>
            </td>
        </tr>
        <tr runat="server">
            <td width="10%" class="td_Edit">
                <asp:Label ID="Label2" runat="server" Text="特殊打卡單位" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="td_Edit">
                <asp:RadioButton ID="rbtYesFlag" GroupName = "SpecialFlag" runat="server" Text ="是" />
                <asp:RadioButton ID="rbtNoFlag" GroupName = "SpecialFlag" runat="server" Text ="否"/>
            </td>
        </tr>
        <tr id = "lastComp" runat="server" style="height:20px" visible="false">
            <td class="td_Edit" width="10%">
                <asp:Label ID="lblLastChgComp"  runat="server" Text="最後異動公司" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="td_Edit" align="left" width="40%">
                <asp:Label ID="txtLastChgComp"  runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr >
        <tr id = "lastEmp" runat="server" style="height:20px" visible="false">
            <td class="td_Edit" width="10%">
                <asp:Label ID="lblLastChgID"  runat="server" Text="最後異動人員" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="td_Edit" align="left" width="40%">
                <asp:Label ID="txtLastChgID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr id = "lastDate" runat="server" style="height:20px" visible="false">
            <td class="td_Edit" width="10%">
                <asp:Label ID="lblLastChgDate"  runat="server" Text="最後異動時間" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="td_Edit" align="left" width="40%">
                <asp:Label ID="txtLastChgDate"  runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
    </table>
   </form>
</body>
</html>
