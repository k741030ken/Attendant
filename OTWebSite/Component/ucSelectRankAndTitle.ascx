<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSelectRankAndTitle.ascx.vb" Inherits="Component_ucSelectRankAndTitle" %>
<asp:Label ID="Label1" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Text="(職等)"></asp:Label>
<asp:DropDownList ID="ddlRankID" runat="server" CssClass="DropDownListStyle" AutoPostBack="True"></asp:DropDownList>
<asp:Label ID="lblRankID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin"></asp:Label>
<asp:Label ID="Label2" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Text="/(職稱)"></asp:Label>
<asp:DropDownList ID="ddlTitleID" runat="server" CssClass="DropDownListStyle" AutoPostBack="True"></asp:DropDownList>
<asp:Label ID="lblTitleName" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin"></asp:Label>
