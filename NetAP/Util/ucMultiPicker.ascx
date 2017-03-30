<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMultiPicker.ascx.cs" Inherits="Util_ucMultiPicker" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagPrefix="uc1" TagName="ucCommMultiSelect" %>

<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="position: relative; vertical-align: top; *display: inline;">
        <asp:TextBox ID="txtSelectedInfoList" runat="server" CausesValidation="false" ReadOnly="true" Rows="5" Text="" Style="word-wrap: normal; overflow: auto; cursor: pointer;"></asp:TextBox>
        <asp:Button ID="btnLaunch" runat="server" Text="" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;"
            CausesValidation="false" Style="display: none;" UseSubmitBehavior="False" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtSelectedInfoList" Enabled="false" Display="Dynamic" ErrorMessage="*"
            ForeColor="Red" Style="position: relative;"></asp:RequiredFieldValidator>
        <asp:TextBox ID="txtSelectedIDList" runat="server" Width="80" CausesValidation="false"
            ReadOnly="true" Text="" Style="display: none;"></asp:TextBox>
    </span>
</div>
<uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
<div style="display: none;">
    <asp:Panel ID="ModalPanel" runat="server" CausesValidation="false">
        <uc1:ucCommMultiSelect runat="server" ID="ucCommMultiSelect1" />
    </asp:Panel>
</div>
