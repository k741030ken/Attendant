<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucProcessButton.ascx.cs" Inherits="Util_ucProcessButton" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>

<%--發動按鈕--%>
<asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" CausesValidation="false" Style="text-align: center;" UseSubmitBehavior="False" />
<%--燈箱控制項 2017.01.13--%>
<uc1:ucLightBox runat="server" ID="ucLightBox" />
