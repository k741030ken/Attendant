<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseInfo.aspx.cs" Inherits="LegalSample_CaseInfo"
    UICulture="auto" MaintainScrollPositionOnPostback="true" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc2" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>CaseInfo</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <div>
        <asp:Label ID="labMsg" runat="server" Visible="False" meta:resourcekey="labMsgResource1" />
        <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" meta:resourcekey="fmMainResource1">
            <EditItemTemplate>
                <%--資料編輯--%>
                <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Util_ajax__tab_theme"
                    ScrollBars="Auto" Width="100%" meta:resourcekey="TabMainContainerResource1">
                    <asp:TabPanel runat="server" HeaderText="Main" ID="TabMain" meta:resourcekey="tabMainResource1">
                        <ContentTemplate>
                            <table style="border: 0px none #FFF;" cellspacing="0">
                                <tr class="Util_clsRow1">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="labCaseNo" runat="server" Text="案號" meta:resourcekey="labCaseNoResource1" />：
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:TextBox ID="txtCaseNo" runat="server" Width="96px" Text='<%# Bind("CaseNo") %>'
                                            meta:resourcekey="txtCaseNoResource1" />
                                    </td>
                                    <td style="width: 400px;">
                                        <asp:Literal ID="labExpectCloseDate" runat="server" Text="預定結案" meta:resourcekey="labExpectCloseDateResource1" />：
                                        <uc1:ucDatePicker ID="ucExpectCloseDate" runat="server" />
                                        <asp:Literal ID="labIsUrgent" runat="server" Text="急件" meta:resourcekey="labIsUrgentResource1" />：
                                        <asp:CheckBox ID="chkIsUrgent" runat="server" meta:resourcekey="chkIsUrgentResource1" />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow2">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="labSubject" runat="server" Text="提案主題" meta:resourcekey="labSubjectResource1" />：<br />
                                        <asp:Label ID="dispSubject" runat="server" ForeColor="Brown" meta:resourcekey="dispSubjectResource1" />
                                    </td>
                                    <td colspan="2">
                                        <uc2:ucTextBox ID="ucSubject" runat="server" ucRows="1" ucWidth="550" />
                                        <br />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow1">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="labOutlined" runat="server" Text="狀況簡述" meta:resourcekey="labOutlinedResource1" />：<br />
                                        <asp:Label ID="dispOutlined" runat="server" ForeColor="Brown" meta:resourcekey="dispOutlinedResource1" />
                                    </td>
                                    <td colspan="2">
                                        <uc2:ucTextBox ID="ucOutlined" runat="server" ucRows="5"  ucWidth="550" />
                                        <br />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow2">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="labPropOpinion" runat="server" Text="單位意見" meta:resourcekey="labPropOpinionResource1" />：<br />
                                        <asp:Label ID="dispPropOpinion" runat="server" ForeColor="Brown" meta:resourcekey="dispPropOpinionResource1" />
                                    </td>
                                    <td colspan="2">
                                        <uc2:ucTextBox ID="ucPropOpinion" runat="server" ucRows="5"   ucWidth="550" />
                                        <br />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow1" id="trLegalOpinion" runat="server">
                                    <td style="width: 100px; text-align: right; vertical-align: top;" runat="server">
                                        <asp:Literal ID="labLegalOpinion" runat="server" Text="法務意見" meta:resourcekey="labLegalOpinionResource1" />：<br />
                                        <asp:Label ID="dispLegalOpinion" runat="server" ForeColor="Brown" meta:resourcekey="dispLegalOpinionResource1" />
                                    </td>
                                    <td colspan="2" runat="server">
                                        <uc2:ucTextBox ID="ucLegalOpinion" runat="server" ucRows="8" ucWidth="550" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <asp:Button runat="server" ID="btnUpdProp" Text="提案修改" CssClass="Util_clsBtn" Width="120px"
                                    Visible="False" OnClick="btnUpdProp_Click" meta:resourcekey="btnUpdPropResource1" />
                                <asp:Button runat="server" ID="btnUpdLegal" Text="法務彙整" CssClass="Util_clsBtn" Width="120px"
                                    Visible="False" OnClick="btnUpdLegal_Click" meta:resourcekey="btnUpdLegalResource1" />
                            </center>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Prop Att." ID="TabPropAttach" meta:resourcekey="tabPropAttachResource1">
                        <ContentTemplate>
                            <iframe id="frmPropAttach" runat="server" frameborder="0"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Legal Att." ID="TabLegalAttach" meta:resourcekey="tabLegalAttachResource1"
                        Visible="False">
                        <ContentTemplate>
                            <iframe id="frmLegalAttach" runat="server" frameborder="0"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </EditItemTemplate>
            <ItemTemplate>
                <%--資料顯示--%>
                <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Util_ajax__tab_theme"
                    ScrollBars="Auto" Width="100%" meta:resourcekey="TabMainContainerResource1">
                    <asp:TabPanel runat="server" HeaderText="Main" ID="TabMain" meta:resourcekey="tabMainResource1">
                        <ContentTemplate>
                            <table style="border: 1px solid #FFF; width: 700px;" cellspacing="2" cellpadding="2">
                                <tr class="Util_clsRow1">
                                    <td style="width: 100px; text-align: right; vertical-align: top;">
                                        <asp:Literal ID="lab01" runat="server" Text="案號" meta:resourcekey="labCaseNoResource1" />：
                                    </td>
                                    <td style="width: 200px;">
                                        <asp:Label ID="txt01" runat="server" ForeColor="Black" Width="96px" Text='<%# Bind("CaseNo") %>' />
                                    </td>
                                    <td style="width: 400px;">
                                        <asp:Literal ID="lab02" runat="server" Text="預定結案" meta:resourcekey="labExpectCloseDateResource1" />：
                                        <asp:Label ID="txt02" runat="server" ForeColor="Black" Text='<%# Bind("ExpectCloseDate","{0:yyyy/MM/dd}") %>' />
                                        <asp:Literal ID="lab03" runat="server" Text="急件" meta:resourcekey="labIsUrgentResource1" />：
                                        <asp:Label ID="txt03" runat="server" ForeColor="Black" Text='<%# Bind("IsUrgent") %>' />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow2">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="lab04" runat="server" Text="提案主題" meta:resourcekey="labSubjectResource1" />：
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txt04" runat="server" ForeColor="Black" Text='<%# Bind("Subject") %>' />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow1">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="lab05" runat="server" Text="狀況簡述" meta:resourcekey="labOutlinedResource1" />：
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txt05" runat="server" ForeColor="Black" Text='<%# Bind("Outlined") %>' />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow2">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="lab06" runat="server" Text="單位意見" meta:resourcekey="labPropOpinionResource1" />：
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txt06" runat="server" ForeColor="Black" Text='<%# Bind("PropOpinion") %>' />
                                    </td>
                                </tr>
                                <tr class="Util_clsRow1">
                                    <td style="width: 100px; text-align: right; vertical-align: top; white-space: nowrap;">
                                        <asp:Literal ID="lab07" runat="server" Text="法務意見" meta:resourcekey="labLegalOpinionResource1" />：
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="txt07" runat="server" ForeColor="Black" Text='<%# Bind("LegalOpinion") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Prop Att." ID="TabPropAttach" meta:resourcekey="tabPropAttachResource1">
                        <ContentTemplate>
                            <iframe id="frmPropAttachQry" runat="server" frameborder="0"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Legal Att." ID="TabLegalAttach" meta:resourcekey="tabLegalAttachResource1">
                        <ContentTemplate>
                            <iframe id="frmLegalAttachQry" runat="server" frameborder="0"></iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tabFlowFullLog" runat="server" HeaderText="FlowFullLog" Height="100%" meta:resourcekey="tabFlowFullLogResource1">
                        <ContentTemplate>
                            <%--流程記錄--%>
                            <iframe id="FlowLogFrame" runat="server" frameborder="0" clientidmode="Inherit">
                            </iframe>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </ItemTemplate>
        </asp:FormView>
    </div>
    </form>
</body>
</html>
