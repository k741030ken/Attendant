<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucActionBar.ascx.cs" Inherits="Util_ucActionBar" %>
<div id="divActionBar" runat="server">
    <asp:Button ID="btnQuery" runat="server" Text="Query" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnAdd" runat="server" Text="Add" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnEdit" runat="server" Text="Edit" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnCopy" runat="server" Text="Copy" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnExport" runat="server" Text="Export" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnDownload" runat="server" Text="Download" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnInformation" runat="server" Text="Information" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnMultilingual" runat="server" Text="Multilingual" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
    <asp:Button ID="btnPrint" runat="server" Text="Print" UseSubmitBehavior="false" CausesValidation="false" OnClick="barButton_Click" />
</div>
<asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>

