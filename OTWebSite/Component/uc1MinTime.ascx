<%@ Control Language="VB" AutoEventWireup="false" CodeFile="uc1MinTime.ascx.vb" Inherits="Component_uc1MinTime" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divDataArea" runat="server" style="vertical-align: top;">
        <asp:DropDownList ID="ddlHH" runat="server" Visible="true" />
        <asp:Literal ID="litMM" runat="server" Text=" : " Visible="true" />
        <asp:DropDownList ID="ddlMM" runat="server" Visible="true" />
        <asp:Literal ID="litSS" runat="server" Text=" : " Visible="false" />
        <asp:DropDownList ID="ddlSS" runat="server" Visible="false" />
    </span>
</div>
