<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseAdmin.aspx.cs" Inherits="LegalSample_CaseAdmin"
    UICulture="auto" MaintainScrollPositionOnPostback="true" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc2" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc3" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>Case Admin</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
        </asp:ToolkitScriptManager>
        <fieldset class='Util_Fieldset' style="width: 98%">
            <legend class="Util_Legend">
                <asp:Literal ID="labCaseAdmin" runat="server" Text="案件維護" meta:resourcekey="labCaseAdminResource1" /></legend>
            <div id="divMainGridview" runat="server" visible="true">
                <%-- GridView --%>
                <center>
                    <uc1:ucGridView ID="ucGridView1" runat="server" />
                </center>
            </div>
            <%-- FormView --%>
            <div id="divMainFormView" runat="server" visible="false">
                <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" meta:resourcekey="fmMainResource1">
                    <EditItemTemplate>
                        <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Util_ajax__tab_theme"
                            ScrollBars="Auto" Width="100%">
                            <asp:TabPanel runat="server" HeaderText="Main" ID="TabMain" meta:resourcekey="tabMainResource1">
                                <ContentTemplate>
                                    <table style="border: 0px none #FFF;" cellspacing="0">
                                        <tr class="Util_clsRow1">
                                            <td style="width: 100px; text-align: right;">
                                                <asp:Literal ID="labCaseNo" runat="server" Text="案　　號" meta:resourcekey="labCaseNoResource1" />：
                                            </td>
                                            <td style="width: 200px;">
                                                <asp:TextBox ID="txtCaseNo" runat="server" Width="96px" Text='<%# Bind("CaseNo") %>'
                                                    meta:resourcekey="txtCaseNoResource1" />
                                            </td>
                                            <td style="width: 400px;">
                                                <asp:Literal ID="labExpectCloseDate" runat="server" Text="預定結案" meta:resourcekey="labExpectCloseDateResource1" />：
                                                <uc1:ucDatePicker ID="ucExpectCloseDate" runat="server" ucIsRequire="false" />
                                                <asp:Literal ID="labIsUrgent" runat="server" Text="急件" meta:resourcekey="labIsUrgentResource1" />：
                                                <asp:CheckBox ID="chkIsUrgent" runat="server" meta:resourcekey="chkIsUrgentResource1" />
                                            </td>
                                        </tr>
                                        <tr class="Util_clsRow2">
                                            <td style="width: 100px; text-align: right; vertical-align: top;">
                                                <asp:Literal ID="labSubject" runat="server" Text="提案主題" meta:resourcekey="labSubjectResource1" />：<br />
                                                <asp:Label ID="dispSubject" runat="server" ForeColor="Brown" meta:resourcekey="dispSubjectResource1" />
                                            </td>
                                            <td colspan="2">
                                                <uc2:ucTextBox ID="ucSubject" runat="server" ucRows="1" ucIsRequire="true" ucIsDispEnteredWords="true"
                                                    ucWidth="550" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr class="Util_clsRow1">
                                            <td style="width: 100px; text-align: right; vertical-align: top;">
                                                <asp:Literal ID="labOutlined" runat="server" Text="狀況簡述" meta:resourcekey="labOutlinedResource1" />：<br />
                                                <asp:Label ID="dispOutlined" runat="server" ForeColor="Brown" meta:resourcekey="dispOutlinedResource1" />
                                            </td>
                                            <td colspan="2">
                                                <uc2:ucTextBox ID="ucOutlined" runat="server" ucRows="10" ucIsRequire="true" ucIsDispEnteredWords="true"
                                                    ucWidth="550" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr class="Util_clsRow2">
                                            <td style="width: 100px; text-align: right; vertical-align: top;">
                                                <asp:Literal ID="labPropOpinion" runat="server" Text="單位意見" meta:resourcekey="labPropOpinionResource1" />：<br />
                                                <asp:Label ID="dispPropOpinion" runat="server" ForeColor="Brown" meta:resourcekey="dispPropOpinionResource1" />
                                            </td>
                                            <td colspan="2">
                                                <uc2:ucTextBox ID="ucPropOpinion" runat="server" ucRows="10" ucIsRequire="true" ucIsDispEnteredWords="true"
                                                    ucWidth="550" />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <hr class="Util_clsHR" />
                                                <asp:Button runat="server" ID="btnUpdate" Text="確　　定" CssClass="Util_clsBtn" Width="120px"
                                                    OnClick="btnUpdate_Click" meta:resourcekey="btnUpdateResource1" />
                                                <asp:Button runat="server" ID="btnUpdateCancel" Text="取　　消" CssClass="Util_clsBtn"
                                                    Width="120px" OnClick="btnUpdateCancel_Click" CausesValidation="False" meta:resourcekey="btnUpdateCancelResource1" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel runat="server" HeaderText="Prop Att." ID="TabPropAttach" meta:resourcekey="tabPropAttachResource1">
                                <ContentTemplate>
                                    <%--提案附件--%>
                                    <iframe id="frmPropAttach" runat="server" frameborder="0" clientidmode="Inherit">
                                    </iframe>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel runat="server" HeaderText="Legal Att." ID="TabLegalAttach" meta:resourcekey="tabLegalAttachResource1"
                                Visible="false">
                                <ContentTemplate>
                                    <%--法務附件--%>
                                    <iframe id="frmLegalAttach" runat="server" frameborder="0" clientidmode="Inherit">
                                    </iframe>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </fieldset>
    </div>
    <div style="display: none;">
        <!--固定隱藏區-->
        <asp:Panel ID="pnlVerify" runat="server">
            <br />
            <asp:Label ID="labVerifyCaseInfo" runat="server" Text="" />
            <br />
            <br />
            <asp:Label ID="labVerifyAssignTo" runat="server" Text="" /><asp:DropDownList ID="ddlAssignTo"
                runat="server" />
            <br />
            <hr class="Util_clsHR" />
            <center>
                <asp:Button ID="btnVerify" runat="server" Text="OK" CssClass="Util_clsBtn" OnClick="btnVerify_Click" />
                <asp:Button ID="btnVerifyCancel" runat="server" CausesValidation="False" Text="Cancel"
                    CssClass="Util_clsBtn" OnClick="btnVerifyCancel_Click" />
            </center>
        </asp:Panel>
    </div>
    <uc3:ucModalPopup ID="ucModalPopup1" runat="server" />
    </form>
</body>
</html>
