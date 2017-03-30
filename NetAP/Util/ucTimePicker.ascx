<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTimePicker.ascx.cs" Inherits="Util_ucTimePicker" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span>
    <span id="divDataArea" runat="server" style="vertical-align: top;">
        <asp:DropDownList ID="ddlHH" runat="server" Visible="true" />
        <asp:Literal ID="litMM" runat="server" Text=" : " Visible="true" />
        <asp:DropDownList ID="ddlMM" runat="server" Visible="true" />
        <asp:Literal ID="litSS" runat="server" Text=" : " Visible="false" />
        <asp:DropDownList ID="ddlSS" runat="server" Visible="false" />
    </span>
</div>
