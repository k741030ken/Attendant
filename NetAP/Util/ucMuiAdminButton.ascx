<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMuiAdminButton.ascx.cs"
    Inherits="Util_ucMuiAdminButton" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<span style="height: 25px;">
    <asp:Button ID="btnLaunch" runat="server" Text="MUI Admin" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;"
        CausesValidation="false" Style="text-align: center;" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
</span>