<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommSingleSelect.ascx.cs"
    Inherits="Util_ucCommSingleSelect" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <div id="divHeadArea" runat="server" style="float: left; vertical-align: top; display: inline-block; padding: 1px 2px; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Width="80" Style="text-align: right;"></asp:Label>
    </div>
    <div id="divDataArea" runat="server" style="float: left; display: inline-block; padding: 0;">
        <span style="padding-top: 0px; float: left;">
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        </span>
        <span style="padding-top: 1px; float: left;">
            <asp:DropDownList ID="ddlSourceList" runat="server"></asp:DropDownList>
        </span>
    </div>
</div>
<asp:TextBox ID="idSelectedID" runat="server" Width="50" Style="display: none;" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSourceList"
    Enabled="false" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
