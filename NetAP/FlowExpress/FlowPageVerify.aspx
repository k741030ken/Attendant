<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowPageVerify.aspx.cs" Inherits="FlowExpress_FlowPageVerify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagName="ucCommMultiSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagPrefix="uc1" TagName="ucCheckBoxList" %>
<%@ Register Src="~/Util/ucUserPicker.ascx" TagName="ucUserPicker" TagPrefix="uc1" %>
<%@ Register Src="ucFlowFullLogList.ascx" TagName="ucFlowFullLogList" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>
<%@ Register Src="~/Util/ucAttachAdminButton.ascx" TagPrefix="uc1" TagName="ucAttachAdminButton" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>
<%@ Register Src="~/Util/ucCommPhrase.ascx" TagPrefix="uc1" TagName="ucCommPhrase" %>
<%@ Register Src="~/Util/ucCommPhraseAdminButton.ascx" TagPrefix="uc1" TagName="ucCommPhraseAdminButton" %>
<%@ Register Src="~/Util/ucCommUserAdminButton.ascx" TagPrefix="uc1" TagName="ucCommUserAdminButton" %>
<%@ Register Src="~/Util/ucProcessButton.ascx" TagPrefix="uc1" TagName="ucProcessButton" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>FlowPageVerify</title>
    <style type="text/css">
        .FlowPageVerify_btnBack {
            font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
            position: absolute;
            top: 0px;
            left: 0px;
            text-align: center;
            font-size: 16px;
            margin: 0px;
            padding-top: 0px;
            padding-left: 8px;
            border-style: none;
            color: #666;
            background-color: #AAA;
            width: 100%;
            height: 33px;
            cursor: pointer;
            -webkit-appearance: none;
            -moz-appearance: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>

        <asp:TabContainer ID="TabMainContainer" runat="server" CssClass="Site_ajax__tab_theme"
            ScrollBars="Auto" ActiveTabIndex="0">
            <asp:TabPanel runat="server" ID="tabCustForm">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color1'></div>
                        <div class='tab-caption' style="background-color: transparent;">
                            <asp:Label runat="server" ID="labCustForm" Text="Form" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <%--案件內容--%>
                    <iframe id="CustFormFrame" runat="server" frameborder="0" clientidmode="Inherit"></iframe>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabFlowVerify" runat="server">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color2'></div>
                        <div class='tab-caption' style="background-color: transparent;">
                            <asp:Label runat="server" ID="labFlowVerify" Text="FlowVerify" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="padding: 10px;">
                        <%-- 流程審核 --%>
                        <fieldset class="Util_Fieldset">
                            <legend class="Util_Legend">
                                <%-- 上一關意見 --%>
                                <asp:Label ID="labPrevStepOpinion" runat="server" Text="Opinion" /></legend>
                            <uc1:ucGridView runat="server" ID="gvFlowPrevStepLog" />
                        </fieldset>
                        <div style="float: left; width: 49%; margin-right: 15px;">
                            <fieldset class="Util_Fieldset">
                                <legend class="Util_Legend">
                                    <asp:Label ID="labFlowOpinion" runat="server" Text="Opinion" />
                                    <asp:Label ID="dispFlowOpinion" runat="server" />
                                </legend>
                                <div style="padding: 5px 10px 10px 10px; min-height: 280px;">
                                    <div style="margin-bottom: 10px;">
                                        <%-- 常用片語 --%>
                                        <uc1:ucCommPhrase runat="server" ID="ucCommPhrase" />
                                        <%-- 流程片語 --%>
                                        <uc1:ucCommPhrase runat="server" ID="ucFlowPhrase" />
                                    </div>
                                    <%-- 流程意見 --%>
                                    <uc1:ucTextBox ID="txtFlowOpinion" runat="server" ucIsDispEnteredWords="true"></uc1:ucTextBox>
                                    <div style="width: 100%; margin-top: 5px; text-align: right;">
                                        <%-- 流程附件 --%>
                                        <uc1:ucAttachAdminButton runat="server" ID="btnFlowAttach" ucBtnCssClass="Util_clsBtnGray" ucIsPopNewWindow="true" />
                                        <%-- 常用人員 --%>
                                        <uc1:ucCommUserAdminButton runat="server" ID="ucCommUserAdminButton" ucBtnCssClass="Util_clsBtnGray" ucIsPopNewWindow="true" />
                                        <%-- 常用片語 --%>
                                        <uc1:ucCommPhraseAdminButton runat="server" ID="ucCommPhraseAdminButton" ucBtnCssClass="Util_clsBtnGray" />
                                        <%-- 意見暫存 --%>
                                        <asp:Button ID="btnSaveTempOpinion" runat="server" CssClass="Util_clsBtnGray" OnClick="btnSaveTempOpinion_Click"
                                            CausesValidation="true" Text="SaveOpinion"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div style="float: left; width: 49%;">
                            <fieldset class="Util_Fieldset">
                                <legend class="Util_Legend">
                                    <asp:Label ID="labFlowStepInfo" runat="server" Text="StepInfo"></asp:Label>
                                </legend>
                                <div style="padding: 5px 10px 10px 10px; min-height: 280px; text-align: center;">
                                    <asp:Label ID="labNotAllowVerify" runat="server" Visible="false" Text="Not Allow Verify" Width="90%" Height="120px"></asp:Label>
                                    <asp:Button ID="btnVerify01" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="01" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify02" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="02" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify03" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="03" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify04" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="04" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify05" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="05" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify06" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="06" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify07" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="07" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify08" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="08" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify09" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="08" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                    <asp:Button ID="btnVerify10" runat="server" CssClass="Util_clsBtn Util_Pointer" Visible="false" CausesValidation="false" UseSubmitBehavior="false" Text="08" OnClick="btnVerify_Click" Width="90%" Style="margin: 5px;"></asp:Button>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <iframe id="FlowLogFrame" runat="server" frameborder="0"></iframe>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabBack" runat="server">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color5'></div>
                        <div class='tab-caption' style="position: relative; cursor: default; vertical-align: top; padding-left: 0;">
                            <asp:Button runat="server" ID="btnBack" UseSubmitBehavior="false" CausesValidation="false" CssClass="FlowPageVerify_btnBack" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="padding-top: 50px; min-height: 650px;"></div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
        <br />
        <br />
        <br />
        <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
        <uc1:ucLightBox runat="server" ID="ucLightBox" />
    </form>
</body>
</html>
