<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAttachAdminButton.ascx.cs" Inherits="Util_ucAttachAdminButton" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<span style="height: 25px;">
    <asp:Button ID="btnLaunch" runat="server" Text="Upload" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;"
        CausesValidation="false" Style="text-align: center;" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
</span>
