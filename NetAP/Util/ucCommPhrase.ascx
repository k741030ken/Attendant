<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommPhrase.ascx.cs" Inherits="Util_ucCommPhrase" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagPrefix="uc1" TagName="ucCommSingleSelect" %>
<asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>
<div id="divPick" runat="server" style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <div style="float: left;">
        <uc1:ucCommSingleSelect runat="server" ID="ddlPhrase" />
    </div>
    <div style="padding-top: 3px; padding-left: 3px; float: left;">
        <asp:Button ID="btnAppend" runat="server" Text="Append" />
    </div>
</div>

