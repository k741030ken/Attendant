<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDate.ascx.cs"
    Inherits="Util_ucDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="vertical-align: top;">
        <asp:TextBox ID="txtDate" runat="server" Width="80" Text=""></asp:TextBox>
        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
            TargetControlID="txtDate">
        </asp:CalendarExtender>
        <asp:RegularExpressionValidator ID="regUnusualTime" runat="server" ControlToValidate="txtDate" Display="Dynamic" ErrorMessage="*[日期格式請輸入 Ex:2017/01/01]" ForeColor="Red" ValidationExpression="[0-9]{4}/[0-9]{2}/[0-9]{2}"></asp:RegularExpressionValidator>
        <asp:CompareValidator runat="server" ID="CompareUnusualTime1" ControlToValidate="txtDate"
             Operator="DataTypeCheck" Type="Date" Display="Dynamic" ErrorMessage="*[日期欄位格式錯誤]"  ForeColor="Red"></asp:CompareValidator>
    </span>
</div>
