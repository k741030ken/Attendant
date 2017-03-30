<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucModalPopup.ascx.cs"
    Inherits="Util_ucModalPopup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="ClientWidth" runat="server" Value="0" />
<asp:HiddenField ID="ClientHeight" runat="server" Value="0" />
<%--彈出面板定義--%>
<asp:Panel ID="ModalPanel" runat="server" CssClass="Util_ModalPopup_PopPanel">
    <div class="Util_ModalPopup_PopHeader">
        <asp:Label ID="labPopupHeader" runat="server" ForeColor="#484848" Font-Bold="true"></asp:Label>
        <asp:Button ID="btnClose" runat="server" Text="X" OnClick="btnClose_Click" CssClass="Util_ModalPopup_PopClose" OnClientClick="Util_IsChkDirty = false;"
            CausesValidation="false" />
    </div>
    <div class="Util_ModalPopup_PopBody">
        <%--實際顯示內容，可為「ucFrameURL」或「ucHtmlContent」或「ucControlID」--%>
        <div>
            <iframe id="ModalFrame" runat="server" visible="false" frameborder="0"></iframe>
            <asp:Label ID="ModalHtmlMsg" runat="server" Visible="false"></asp:Label>
            <asp:Panel ID="ModalControl" runat="server" Visible="false"></asp:Panel>
        </div>
        <div style='text-align: center; margin: 2px auto;'>
            <asp:Button ID="btnComplete" CssClass="Util_clsBtn" runat="server" ClientIDMode="Inherit"
                Text="OK" OnClick="btnComplete_Click" CausesValidation="false" OnClientClick="Util_IsChkDirty = false;" />
            <asp:Button ID="btnCancel" CssClass="Util_clsBtn" runat="server" ClientIDMode="Inherit"
                Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" OnClientClick="Util_IsChkDirty = false;" />
        </div>
    </div>
</asp:Panel>
<%--
	彈出對話框定義，需注意 Page 的dtd定義
	<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
--%>
<asp:Button ID="btnDummy" runat="server" Text="Hidden" Style="display: none;" />
<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnDummy"
    PopupControlID="ModalPanel" BackgroundCssClass="Util_ModalPopup_PopOverlay" DropShadow="true">
</cc1:ModalPopupExtender>
<cc1:RoundedCornersExtender ID="rce" runat="server" TargetControlID="ModalPanel"
    Radius="6" Corners="All" />
