<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAclInfo.ascx.cs" Inherits="AclExpress_ucAclInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagPrefix="uc1" TagName="ucTextBox" %>
<asp:Label ID="labErrMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
<div id='divAclInfo' runat="server">
    <fieldset class='Util_Fieldset'>
        <legend class="Util_Legend">身份資訊</legend>
        <div class="Util_clsRow1">
            <uc1:ucTextBox runat="server" ID="txtUserInfo" ucCaption="個　　人" ucWidth="150" ucIsReadOnly="true" />
        </div>
        <div class="Util_clsRow2">
            <uc1:ucTextBox runat="server" ID="txtRuleInfo" ucCaption="角　　色" ucWidth="450" ucIsReadOnly="true" ucRows="2" />
        </div>
    </fieldset>

    <fieldset class='Util_Fieldset'>
        <legend class="Util_Legend">授權資訊</legend>
        <asp:TabContainer ID="TabContainer1" runat="server" CssClass="Site_ajax__tab_theme"
            ScrollBars="Auto" ActiveTabIndex="0">
            <asp:TabPanel runat="server" ID="tab01">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color1'></div>
                        <div class='tab-caption' style="background-color: transparent;">
                            <asp:Label runat="server" ID="labCustForm" Text="使用權限" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="margin: 10px;">
                        <uc1:ucGridView runat="server" ID="gvAuthMap" />
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="tab02">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color2'></div>
                        <div class='tab-caption' style="background-color: transparent;">
                            <asp:Label runat="server" ID="Label1" Text="管理權限" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="margin: 10px;">
                        <uc1:ucGridView runat="server" ID="gvAdminMap" />
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" ID="tab03">
                <HeaderTemplate>
                    <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                        <div class='tab-color3'></div>
                        <div class='tab-caption' style="background-color: transparent;">
                            <asp:Label runat="server" ID="Label2" Text="權限詳情" />
                        </div>
                    </div>
                </HeaderTemplate>
                <ContentTemplate>
                    <div style="margin: 20px;">
                        <asp:Label ID="labAclInfo" runat="server"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </fieldset>
</div>
