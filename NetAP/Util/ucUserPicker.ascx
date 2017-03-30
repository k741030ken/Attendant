<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUserPicker.ascx.cs"
    Inherits="Util_ucUserPicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="ucCommCascadeSelect.ascx" TagName="ucCommCascadeSelect" TagPrefix="uc1" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="vertical-align: top;">
        <asp:TextBox ID="txtUserInfoList" runat="server" CausesValidation="false" ReadOnly="true"
            Text=""></asp:TextBox>
        <asp:LinkButton ID="btnLaunch" runat="server" CausesValidation="false" OnClick="btnLaunch_Click" OnClientClick="Util_IsChkDirty = false;">
            <asp:Image ID="imgLaunch" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Group.png"
                ImageAlign="AbsBottom" />
        </asp:LinkButton>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ControlToValidate="txtUserInfoList" Enabled="false" Display="Dynamic" ErrorMessage="*"
            ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:HiddenField ID="hidClearData" runat="server" Value="N" />
        <%--輔助記錄當使用者使用JS清除選取人員--%>
        <asp:TextBox ID="txtUserIDList" runat="server" Width="80" CausesValidation="false"
            ReadOnly="true" Text=""></asp:TextBox>
    </span>
</div>
<uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucCausesValidation="false" />
<div style="display: none;">
    <asp:Panel ID="ModalPanel" runat="server" CausesValidation="false">
        <uc1:ucCommCascadeSelect ID="ucCommCascadeSelect1" runat="server" />
    </asp:Panel>
</div>
