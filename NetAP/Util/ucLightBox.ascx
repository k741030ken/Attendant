<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLightBox.ascx.cs" Inherits="Util_ucLightBox" %>
<%--燈箱內容--%>
<div id="divLightBoxPopBody" runat="server" class="Util_Lightbox_PopBody" style="display: none;">
    <asp:Button ID="btnBreak" runat="server" Text="X" CssClass="Util_Lightbox_PopClose" CausesValidation="false" OnClick="btnBreak_Click" />
    <asp:Image ID="imgLightBoxWaiting" runat="server" ImageUrl="~/Util/WebClient/Legend_Waiting.gif" BorderWidth="0" /><br />
    <br />
    <asp:Label ID="labLightBoxMsg" runat="server" Text="" ForeColor="#6F6F6C" Font-Bold="true" />
</div>
<%--燈箱背景--%>
<div id="divLightBoxPopOverlay" runat="server" class="Util_Lightbox_PopOverlay" style="display: none;"></div>
