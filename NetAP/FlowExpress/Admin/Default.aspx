<%@ Page Title="" Language="C#" MasterPageFile="~/FlowExpress/Admin/FlowExpess.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FlowExpress_Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset class="Util_Fieldset">
        <legend class="Util_Legend">FlowSpec (流程規格)</legend>
        <uc1:ucGridView ID="ucGridView1" runat="server" />
    </fieldset>
    <%--固定隱藏區--%>
    <div id="DivHiddenArea" style="display: none;">
        <asp:Panel runat="server" ID="pnlNewFlow" CssClass="Util_Frame">
            <asp:HiddenField ID="hidFlowID" runat="server" />
            <table style="border: 0px none #FFFFFF;" cellspacing="0" width="100%">
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap; width: 80px;">流程代號：
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowID" runat="server" ucIsRequire="true" ucWidth="280" ucMaxLength="20" />
                    </td>
                    <td style="text-align: left; white-space: nowrap; width: 50px;">
                        <asp:Label ID="cntNewFlowID" runat="server" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="text-align: right; white-space: nowrap;">流程名稱：
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowName" runat="server" ucIsRequire="true" ucWidth="280" ucMaxLength="50" />
                    </td>
                    <td style="text-align: left; white-space: nowrap; width: 50px;">
                        <asp:Label ID="cntNewFlowName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <br />
                        <asp:Button ID="btnComplete" runat="server" Text="確　定" CssClass="Util_clsBtn"
                            OnClick="btnComplete_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDumpSQL" HorizontalAlign="Center" >
            <uc1:ucTextBox ID="txtDumpSQL" runat="server" ucWidth="750" ucRows="35" ucIsReadOnly="true" />
        </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucBtnCompleteEnabled="false" ucBtnCancelEnabled="false" />
</asp:Content>
