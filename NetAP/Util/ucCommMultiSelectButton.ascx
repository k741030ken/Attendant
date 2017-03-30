<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommMultiSelectButton.ascx.cs"
    Inherits="Util_ucCommMultiSelectButton" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="ucCommMultiSelect.ascx" TagName="ucCommMultiSelect" TagPrefix="uc2" %>
<span style="height: 25px;">
    <asp:Button ID="btnLaunch" runat="server" Text="CommMultiSelect" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;" 
        CausesValidation="false" Style="text-align: center;" 
    UseSubmitBehavior="False" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
    <div style="display: none;">
        <asp:Panel ID="ModalPanel" runat="server" Visible="false" CausesValidation="false">
            <uc2:ucCommMultiSelect ID="ucCommMultiSelect1" runat="server" CausesValidation="false" />
        </asp:Panel>
    </div>
</span>