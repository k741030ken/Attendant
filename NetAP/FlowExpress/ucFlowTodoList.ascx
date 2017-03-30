<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFlowTodoList.ascx.cs"
    Inherits="FlowExpress_ucFlowTodoList" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown"
    TagPrefix="uc2" %>
<div id="DivError" runat="server" visible="false">
    <asp:Label ID="labErrMsg" runat="server" Text=""></asp:Label>
</div>
<div id="DivSelfTodoList" runat="server" visible="true">
    <fieldset class='Util_Fieldset'>
        <legend class="Util_Legend">
            <asp:Label ID="labSelfTodoList" runat="server" Text="SelfTodoList" />
            <asp:ImageButton ID="btnSelfTodoList" runat="server" OnClick="btnSelfTodoList_Click" />
        </legend>
        <uc1:ucGridView ID="gvSelfTodoList" runat="server" />
    </fieldset>
</div>
<br />
<div id="DivProxyTodoList" runat="server" visible="true">
    <fieldset class='Util_Fieldset'>
        <legend class="Util_Legend">
            <asp:Label ID="labProxyTodoList" runat="server" Text="ProxyTodoList" />
            <asp:ImageButton ID="btnProxyTodoList" runat="server" OnClick="btnProxyTodoList_Click" />
        </legend>
        <uc1:ucGridView ID="gvProxyTodoList" runat="server" />
    </fieldset>
</div>
<%--隱藏區，由程式控制--%>
<div style="display: none;">
    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" />
</div>
<uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucBtnCompleteWidth="300" />
