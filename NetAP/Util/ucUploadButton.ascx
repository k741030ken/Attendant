<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUploadButton.ascx.cs"
    Inherits="Util_ucUploadButton" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<span style="height: 25px;">
    <asp:Button ID="btnLaunch" runat="server" Text="Upload" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;"
        CausesValidation="false" Style="text-align: center;" UseSubmitBehavior="False" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
</span>