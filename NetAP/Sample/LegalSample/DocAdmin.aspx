<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocAdmin.aspx.cs" Inherits="LegalSample_DocAdmin"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown"
    TagPrefix="uc1" %>
<%@ Register src="~/Util/ucCheckBoxList.ascx" tagname="ucCheckBoxList" tagprefix="uc1" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>LegalDocAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
        </asp:ToolkitScriptManager>
        <asp:Label ID="labErrMsg"  runat="server" Text="" Visible="false" />
        <div id="divMainGridview" runat="server" visible="true">
            <%-- GridView --%>
            <center>
                <uc1:ucGridView ID="ucGridView1" runat="server" />
            </center>
        </div>
        <%-- FormView --%>
        <div id="divMainFormView" runat="server" visible="false" >
            <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" meta:resourcekey="fmMainResource1">
                <EditItemTemplate>
                    <%--資料編輯--%>
                    <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Util_ajax__tab_theme"
                        ScrollBars="Auto" Width="100%" Height="360px" ActiveTabIndex="0" meta:resourcekey="TabMainContainerResource1">
                        <asp:TabPanel runat="server" HeaderText="Main" ID="TabMain" meta:resourcekey="tabMainResource1">
                            <ContentTemplate>
                                <table style="border: 0px none #FFF;" cellspacing="0">
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right;">
                                            <asp:Literal ID="labCaseNo" runat="server" Text="編　　號" meta:resourcekey="labCaseNoResource1" />：
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:TextBox ID="txtDocNo" runat="server" Width="96px" Text='<%# Bind("DocNo") %>'
                                                meta:resourcekey="txtDocNoResource1" />
                                        </td>
                                        <td class="style1">
                                            <asp:Literal ID="labIsReleae" runat="server" Text="發佈" meta:resourcekey="labIsReleaeResource1" />：
                                            <asp:CheckBox ID="chkIsRelease" runat="server" meta:resourcekey="chkIsReleaseResource1" />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="labSubject" runat="server" Text="文件名稱" meta:resourcekey="labSubjectResource1" />：<br />
                                            <asp:Label ID="dispSubject" runat="server" ForeColor="Brown" meta:resourcekey="dispSubjectResource1" />
                                        </td>
                                        <td colspan="2">
                                            <uc1:ucTextBox ID="ucSubject" runat="server" ucRows="1" ucIsRequire="true" ucIsDispEnteredWords="true"
                                                ucWidth="550" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="labKind" runat="server" Text="產品類別" meta:resourcekey="labKindResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <uc1:ucCascadingDropDown ID="ucCascadingKind" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="labUsage" runat="server" Text="文件用途" meta:resourcekey="labUsageResource1" />：<br />
                                            <asp:Label ID="dispUsage" runat="server" ForeColor="Brown" meta:resourcekey="dispUsageResource1" />
                                        </td>
                                        <td colspan="2">
                                            <uc1:ucTextBox ID="ucUsage" runat="server" ucRows="5" ucIsRequire="true" ucIsDispEnteredWords="true"
                                                ucWidth="550" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="labKeyword" runat="server" Text="關 鍵 字" meta:resourcekey="labKeywordResource1" />：<br />
                                        </td>
                                        <td colspan="2">
                                            <uc1:ucCheckBoxList ID="ucKeyword" runat="server" ucWidth="550" />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="labRemark" runat="server" Text="備　　註" meta:resourcekey="labRemarkResource1" />：<br />
                                            <asp:Label ID="dispRemark" runat="server" ForeColor="Brown" meta:resourcekey="dispRemarkResource1" />
                                        </td>
                                        <td colspan="2">
                                            <uc1:ucTextBox ID="ucRemark" runat="server" ucRows="5" ucIsRequire="false" ucIsDispEnteredWords="true"
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
                        <asp:TabPanel runat="server" HeaderText="Doc Att." ID="TabDocAttach" meta:resourcekey="TabDocAttachResource1">
                            <ContentTemplate>
                                <iframe id="frmDocAttach" runat="server" frameborder="0"></iframe>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </EditItemTemplate>
                <ItemTemplate>
                    <%--資料顯示--%>
                    <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Util_ajax__tab_theme"
                        ScrollBars="Auto" Width="100%" ActiveTabIndex="0" meta:resourcekey="TabMainContainerResource1">
                        <asp:TabPanel runat="server" HeaderText="Main" ID="TabMain" meta:resourcekey="tabMainResource1">
                            <ContentTemplate>
                                <table style="border: 0px none #FFF;" cellspacing="0">
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right;">
                                            <asp:Literal ID="lab01" runat="server" Text="編　　號" meta:resourcekey="labCaseNoResource1" />：
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:Label ID="txt01" runat="server" Width="96px" ForeColor="Black" Text='<%# Bind("DocNo") %>' />
                                        </td>
                                        <td >

                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="lab03" runat="server" Text="文件名稱" meta:resourcekey="labSubjectResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="txt03" runat="server" ForeColor="Black" Text='<%# Bind("Subject") %>' />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="lab04" runat="server" Text="產品類別" meta:resourcekey="labKindResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="txt041" runat="server" Width="190px" ForeColor="Black" Text='<%# Bind("Kind1Name") %>' />
                                            <asp:Label ID="txt042" runat="server" Width="190px" ForeColor="Black" Text='<%# Bind("Kind2Name") %>' />
                                            <asp:Label ID="txt043" runat="server" Width="190px" ForeColor="Black" Text='<%# Bind("Kind3Name") %>' />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="lab05" runat="server" Text="文件用途" meta:resourcekey="labUsageResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="txt05" runat="server" ForeColor="Black" Text='<%# Bind("Usage") %>' />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow1">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="lab06" runat="server" Text="關 鍵 字" meta:resourcekey="labKeywordResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="labKeywordResult" runat="server" ForeColor="Black" Text='' />
                                        </td>
                                    </tr>
                                    <tr class="Util_clsRow2">
                                        <td style="width: 100px; text-align: right; vertical-align: top;">
                                            <asp:Literal ID="lab07" runat="server" Text="備　　註" meta:resourcekey="labRemarkResource1" />：
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="txt07" runat="server" ForeColor="Black" Text='<%# Bind("Remark") %>' />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel runat="server" HeaderText="Doc Att." ID="TabDocAttach" meta:resourcekey="TabDocAttachResource1">
                            <ContentTemplate>
                                <iframe id="frmDocAttachQry" runat="server" frameborder="0"></iframe>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    </form>
</body>
</html>
