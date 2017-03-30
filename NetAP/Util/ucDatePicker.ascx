<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDatePicker.ascx.cs"
    Inherits="Util_ucDatePicker" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="vertical-align: top;">
        <asp:TextBox ID="txtDate" runat="server" Width="80" CausesValidation="false" Text=""></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
            TargetControlID="txtDate">
        </asp:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
            Enabled="false" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    </span>
</div>
