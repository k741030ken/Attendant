<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowPopVerify.aspx.cs" Inherits="FlowExpress_FlowPopVerify" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagName="ucCommMultiSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagPrefix="uc1" TagName="ucCheckBoxList" %>
<%@ Register Src="~/Util/ucUserPicker.ascx" TagName="ucUserPicker" TagPrefix="uc1" %>
<%@ Register Src="ucFlowFullLogList.ascx" TagName="ucFlowFullLogList" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title>FlowPopVerify</title>
    <script type="text/javascript">
        //按鈕頁籤改變時，將該頁籤按鈕的「確認訊息」，置換為「開始審核」的確認訊息
        function clientActiveTabChanged(sender, args) {
            var intIndex = sender.get_activeTabIndex() + 1;
            var strTabNo = '' + intIndex;
            var strPad = '00';
            strTabNo = strPad.substring(0, strPad.length - strTabNo.length) + strTabNo;

            var oBtn = document.getElementById('TabMainContainer_tabFlowVerify_TabVerifyContainer_TabVerify' + strTabNo + '_msgVerify' + strTabNo);
            var oStart = document.getElementById('TabMainContainer_tabFlowVerify_msgStartVerify');
            if (oBtn != null && oStart != null) {
                oStart.value = oBtn.value;
            }

            oBtn = document.getElementById('TabMainContainer_tabFlowVerify_TabVerifyContainer_TabVerify' + strTabNo + '_opiVerify' + strTabNo);
            oStart = document.getElementById('TabMainContainer_tabFlowVerify_opiStartVerify');
            if (oBtn != null && oStart != null) {
                oStart.value = oBtn.value;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <div id="divVerifyArea" runat="server">
            <%-- 2017.02.02 調整頁籤順序為 [案件內容][流程記錄][流程附件][案件審核]--%>
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
                <asp:TabPanel ID="tabFlowFullLog" runat="server">
                    <HeaderTemplate>
                        <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                            <div class='tab-color5'></div>
                            <div class='tab-caption' style="background-color: transparent;">
                                <asp:Label runat="server" ID="labFlowFullLog" Text="FlowFullLog" />
                            </div>
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <%--流程記錄--%>
                        <iframe id="FlowLogFrame" runat="server" frameborder="0" clientidmode="Inherit"></iframe>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabFlowAttach" runat="server">
                    <HeaderTemplate>
                        <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                            <div class='tab-color4'></div>
                            <div class='tab-caption' style="background-color: transparent;">
                                <asp:Label runat="server" ID="labFlowAttach" Text="FlowAttach" />
                            </div>
                        </div>
                    </HeaderTemplate>
                    <ContentTemplate>
                        <%--流程附件--%>
                        <iframe id="FlowAttachFrame" runat="server" frameborder="0" clientidmode="Inherit"></iframe>
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
                        <%-- 流程審核 --%>
                        <div id="DivVerifyBtnArea" runat="server" style="padding-top: 8px;">
                            <fieldset class="Util_Fieldset" style="height: auto; vertical-align: top;">
                                <legend class="Util_Legend">
                                    <asp:Label ID="labFlowOpinion" runat="server" Text="Opinion" />
                                    <asp:Label ID="dispFlowOpinion" runat="server" /></legend>
                                <uc1:ucTextBox ID="txtFlowOpinion" runat="server" ucIsDispEnteredWords="true" ucIsRequire="true"></uc1:ucTextBox>
                                <br />
                                <div style="width: 100%; text-align: center;">
                                    <asp:Button ID="btnSaveTempOpinion" runat="server" CssClass="Util_clsBtnGray" OnClick="btnSaveTempOpinion_Click"
                                        CausesValidation="true" Text="Save Temp Opinion" Width="200px"></asp:Button>
                                </div>
                            </fieldset>
                            <fieldset class="Util_Fieldset" style="height: auto; vertical-align: top; margin: 5px 0 15px 0;">
                                <legend class="Util_Legend" style="margin-bottom: 10px;">
                                    <asp:Label ID="labFlowVerifyButton" runat="server" Text="Button"></asp:Label>
                                </legend>
                                <asp:TabContainer ID="TabVerifyContainer" runat="server" ActiveTabIndex="0" CssClass="Site_ajax__tab_theme_Vertical"
                                    Height="250px" Width="100%" OnClientActiveTabChanged="clientActiveTabChanged">
                                    <asp:TabPanel ID="TabVerify01" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color1'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader01" Text="01" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify01" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify01" runat="server" Value="" />
                                                <asp:Label ID="labVerify01" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify01" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify01" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify01" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify01" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify01" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify01" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify02" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color2'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader02" Text="02" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify02" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify02" runat="server" Value="" />
                                                <asp:Label ID="labVerify02" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify02" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify02" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify02" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify02" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify02" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify02" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify03" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color3'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader03" Text="03" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify03" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify03" runat="server" Value="" />
                                                <asp:Label ID="labVerify03" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify03" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify03" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify03" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify03" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify03" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify03" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify04" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color4'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader04" Text="04" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify04" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify04" runat="server" Value="" />
                                                <asp:Label ID="labVerify04" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify04" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify04" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify04" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify04" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify04" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify04" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify05" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color5'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader05" Text="05" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify05" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify05" runat="server" Value="" />
                                                <asp:Label ID="labVerify05" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify05" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify05" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify05" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify05" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify05" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify05" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify06" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color6'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader06" Text="06" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify06" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify06" runat="server" Value="" />
                                                <asp:Label ID="labVerify06" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify06" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify06" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify06" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify06" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify06" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify06" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify07" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color7'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader07" Text="07" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify07" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify07" runat="server" Value="" />
                                                <asp:Label ID="labVerify07" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify07" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify07" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify07" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify07" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify07" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify07" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify08" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color1'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader08" Text="08" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify08" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify08" runat="server" Value="" />
                                                <asp:Label ID="labVerify08" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify08" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify08" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify08" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify08" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify08" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify08" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify09" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color2'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader09" Text="09" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify09" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify09" runat="server" Value="" />
                                                <asp:Label ID="labVerify09" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify09" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify09" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify09" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify09" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify09" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify09" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabVerify10" runat="server" Visible="False">
                                        <HeaderTemplate>
                                            <div class='Site_Tab' style="text-align: left; width: auto; white-space: nowrap; background-color: transparent; border-style: none;">
                                                <div class='tab-color3'></div>
                                                <div class='tab-caption' style="background-color: transparent;">
                                                    <asp:Label runat="server" ID="TabVerifyHeader10" Text="10" />
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div style="width: auto; height: 195px; padding: 5px;">
                                                <asp:HiddenField ID="opiVerify10" runat="server" Value="" />
                                                <asp:HiddenField ID="msgVerify10" runat="server" Value="" />
                                                <asp:Label ID="labVerify10" runat="server" Width="100%" Text="" />
                                                <uc1:ucCommSingleSelect ID="oneVerify10" runat="server" Visible="False" />
                                                <uc1:ucCheckBoxList ID="chkVerify10" runat="server" Visible="False" />
                                                <uc1:ucCommMultiSelect ID="muiVerify10" runat="server" Visible="False" />
                                                <uc1:ucUserPicker ID="anyVerify10" runat="server" Visible="False" />
                                                <asp:ListBox ID="allVerify10" runat="server" Visible="false" />
                                                <asp:Label ID="labAllVerify10" runat="server" Width="100%" Text="" Visible="false" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </fieldset>
                            <center>
                            <%--開始審核按鈕--%>
                            <asp:HiddenField ID="opiStartVerify" runat="server" Value="" />
                            <asp:HiddenField ID="msgStartVerify" runat="server" Value="" />
                            <asp:Button ID="btnStartVerify" runat="server" CssClass="Util_clsBtnGray" OnClick="btnStartVerify_Click"
                                CausesValidation="true" Text="Start Verify" Width="200px"></asp:Button>
                            <asp:Label ID="labNotVerify" runat="server" Visible="false" CssClass="Util_clsBtnGray"
                                Text="Not Allow Verify" Width="300px"></asp:Label>
                            </center>
                        </div>
                        <%--審核訊息區--%>
                        <div id="DivVerifyMsgArea" runat="server" visible="False">
                            <div style="margin: 10px;">
                                <asp:Label ID="labFlowVerifyMsg" runat="server" ForeColor="Blue" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
        <uc1:ucLightBox runat="server" ID="ucLightBox" />
    </form>
</body>
</html>
