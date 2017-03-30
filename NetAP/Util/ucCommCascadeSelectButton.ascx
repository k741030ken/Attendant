<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommCascadeSelectButton.ascx.cs"
    Inherits="Util_ucCommCascadeSelectButton" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="ucCommCascadeSelect.ascx" TagName="ucCommCascadeSelect" TagPrefix="uc2" %>
<span>
    <asp:Button ID="btnLaunch" runat="server" Text="CommCascadeSelect" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;" 
        CausesValidation="false" Style="text-align: center;" 
    UseSubmitBehavior="False" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
    <span style="display: none;">
        <asp:Panel ID="ModalPanel" runat="server" Visible="false" CausesValidation="false">
            <uc2:ucCommCascadeSelect ID="ucCommCascadeSelect1" runat="server" CausesValidation="false" />
        </asp:Panel>
    </span>
</span>