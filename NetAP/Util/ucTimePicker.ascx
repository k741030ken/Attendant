<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTimePicker.ascx.cs" Inherits="Util_ucTimePicker" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <div id="divHeadArea" runat="server" style="float: left; vertical-align: top; display: inline-block; padding: 1px 2px; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </div>
    <div id="divDataArea" runat="server" style="float: left; display: inline-block; padding: 0;">
        <asp:DropDownList ID="ddlHH" runat="server" Visible="true" />
        <asp:Literal ID="litMM" runat="server" Text=" : " Visible="true" />
        <asp:DropDownList ID="ddlMM" runat="server" Visible="true" />
        <asp:Literal ID="litSS" runat="server" Text=" : " Visible="false" />
        <asp:DropDownList ID="ddlSS" runat="server" Visible="false" />
    </div>
</div>
