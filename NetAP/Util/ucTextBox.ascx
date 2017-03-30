<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTextBox.ascx.cs" Inherits="Util_ucTextBox" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <div id="divHeadArea" runat="server" style="float: left; vertical-align: top; display: inline-block; padding: 1px 2px; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </div>
    <div id="divDataArea" runat="server" style="float: left; display: inline-block; padding: 0;">
        <asp:TextBox ID="txtData" runat="server" CausesValidation="false" ReadOnly="false"
            Text=""></asp:TextBox>
        <asp:TextBox ID="txtTemp" runat="server" CausesValidation="false" ReadOnly="false"
            Visible="false" Text=""></asp:TextBox>
        <div id="divPopPanel" runat="server" style="position: absolute; z-index: 9999; padding: 8px 0 0 10px; display:none;">
            <asp:Label ID="labCapsLock" runat="server" Text="" Visible="true" ></asp:Label>
        </div>
        <asp:Label ID="labDataQty" runat="server" Text="" Visible="false"></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtData"
            Enabled="false" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^[-+]?\d+(\.\d+)?$"
            ControlToValidate="txtData" Enabled="false" Display="Dynamic" ErrorMessage="*"
            ForeColor="Red"></asp:RegularExpressionValidator>
    </div>
</div>
