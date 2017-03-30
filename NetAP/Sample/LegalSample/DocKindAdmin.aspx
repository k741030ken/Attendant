<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocKindAdmin.aspx.cs" Inherits="LegalSample_DocKindAdmin"
    UICulture="auto" MaintainScrollPositionOnPostback="true" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown"
    TagPrefix="uc2" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc3" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc4" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>Kind Admin</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
        </asp:ToolkitScriptManager>
        <br />
        <asp:TabContainer ID="TabContainer1" runat="server" CssClass="Util_ajax__tab_theme"
            Width="100%" AutoPostBack="True" OnActiveTabChanged="TabContainer1_ActiveTabChanged" >
            <asp:TabPanel runat="server" ID="TabKind1" HeaderText="第一階" meta:resourcekey="TabKind1Resource1">
                <ContentTemplate>
                    <br />
                    <uc1:ucGridView ID="ucKind1" runat="server" />
                    <br />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabKind2" runat="server" HeaderText="第二階" meta:resourcekey="TabKind2Resource1">
                <ContentTemplate>
                    <br />
                    <asp:Literal ID="Literal2" runat="server" Text="上階代號：" meta:resourcekey="Literal2Resource1"></asp:Literal>
                    <uc2:ucCascadingDropDown ID="ucCascadingKind1" runat="server" />
                    <%--<asp:Button ID="btnKind1" runat="server" Text="Set" OnClick="btnKind1_Click" meta:resourcekey="btnKind1Resource1" />--%>
                    <hr class="Uitl_HR" />
                    <uc1:ucGridView ID="ucKind2" runat="server" />
                    <br />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="TabKind3" runat="server" HeaderText="第三階" meta:resourcekey="TabKind3Resource1">
                <ContentTemplate>
                    <br />
                    <asp:Literal ID="Literal1" runat="server" Text="上階代號：" meta:resourcekey="Literal1Resource1"></asp:Literal>
                    <asp:Label ID="labParentKindInfo2" runat="server" />
                    <uc2:ucCascadingDropDown ID="ucCascadingKind2" runat="server" />
                    <%--<asp:Button ID="btnKind2" runat="server" Text="Set" OnClick="btnKind2_Click" meta:resourcekey="btnKind2Resource1" />--%>
                    <hr class="Uitl_HR" />
                    <uc1:ucGridView ID="ucKind3" runat="server" />
                    <br />
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <div style="display: none;">
        <!--固定隱藏區-->
        <asp:Panel ID="pnlEdit" runat="server" meta:resourcekey="pnlEditResource1">
            <table style="width: 510px;">
                <tr class="Util_clsRow1">
                    <td style="width: 80px;">
                        <asp:Literal ID="litKindNo" runat="server" Text="類別代號：" meta:resourcekey="litKindNoResource1"></asp:Literal>
                    </td>
                    <td>
                        <uc3:ucTextBox ID="ucKindNo" runat="server" ucIsAutoToUpperCase="true" ucIsRequire="true" ucWidth="180"
                            ucMaxLength="20" />
                        <asp:Literal ID="litIsEnabled" runat="server" Text="啟動：" meta:resourcekey="litIsEnabledResource1"></asp:Literal>
                        <asp:DropDownList ID="ddlIsEnabled" runat="server" meta:resourcekey="ddlIsEnabledResource1" />
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="litKindName" runat="server" Text="類別名稱：" meta:resourcekey="litKindNameResource1"></asp:Literal>
                    </td>
                    <td>
                        <uc3:ucTextBox ID="ucKindName" runat="server" ucIsRequire="true" ucWidth="400" ucMaxLength="30" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="width: 80px;">
                        <asp:Literal ID="litRemark" runat="server" Text="備　　註：" meta:resourcekey="litRemarkResource1"></asp:Literal>
                    </td>
                    <td>
                        <uc3:ucTextBox ID="ucRemark" runat="server" ucWidth="400" ucMaxLength="200" ucRows="3" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Button ID="btnOK" runat="server" Text="確　定" CssClass="Util_clsBtn" OnClick="btnOK_Click"
                            meta:resourcekey="btnOKResource1" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="取　消" CssClass="Util_clsBtn"
                            OnClick="btnCancel_Click" meta:resourcekey="btnCancelResource1" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <uc4:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupHeight="200" ucPopupWidth="530" ucBtnCompleteEnabled="false" ucBtnCancelEnabled="false"/>
    </form>
</body>
</html>
