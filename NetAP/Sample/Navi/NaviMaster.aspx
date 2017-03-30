<%@ Page Title="" Language="C#" MasterPageFile="~/Sample/Navi/Navi.master" AutoEventWireup="true" CodeFile="NaviMaster.aspx.cs" Inherits="Sample_Navi_NaviMaster" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/Util/ucActionBar.ascx" TagPrefix="uc1" TagName="ucActionBar" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>
<%@ Register Src="~/Util/ucPageInfo.ascx" TagPrefix="uc1" TagName="ucPageInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ucActionBar runat="server" ID="ucActionBar" />
    <h4><font color="red">導引類元件</font>+ <font color="red">MasterPage</font>+ <font color="red">多頁籤元件</font>整合範例：</h4>
    <ul>
        <li>頂部/左方：[ucMenuBar] / [ucTreeView]，資料來源可為[NodeInfo]資料表或自訂 DataTable</li>
        <li>右上：[ucActionBar]，可自訂要顯示的按鈕及套用的CSS，並提供[按鈕點擊]事件</li>
        <li>右下：多頁籤展示，分別使用 [Ajax TabContainer] / [Site_Tab] 進行實作</li>
    </ul>
    <asp:TabContainer ID="TabContainerLeft" runat="server" CssClass="Site_ajax__tab_theme" ScrollBars="Auto" ActiveTabIndex="0">
        <asp:TabPanel runat="server" ID="tab1">
            <HeaderTemplate>
                <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                    <div class='tab-color1'></div>
                    <div class='tab-caption' style="background-color: transparent;">
                        主要內容
                    </div>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="margin: 10px; margin-bottom: 30px;">
                    <h3>[Site_Tab] Usage：</h3>
                    <div class='Site_Tab' style="width:220px;">
                        <div class='tab-color2'></div>
                        <div class='tab-caption Util_Pointer' onclick="alert('click [Normal]');">
                            Normal
                        </div>
                    </div>
                    <br />
                    <div class='Site_Tab' style="width:220px;">
                        <div class='tab-color5'></div>
                        <div class='tab-caption-active'>
                            Acitve
                        </div>
                    </div>
                    <hr />
                    <div style="height: 34px;">
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color1'></div>
                            <div class='tab-caption' id='tabDiv1' runat="server">
                                <asp:LinkButton ID="tabBtn1" runat="server" Text="頁籤一" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color2'></div>
                            <div class='tab-caption' id='tabDiv2' runat="server">
                                <asp:LinkButton ID="tabBtn2" runat="server" Text="頁籤二" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color3'></div>
                            <div class='tab-caption' id='tabDiv3' runat="server">
                                <asp:LinkButton ID="tabBtn3" runat="server" Text="頁籤三" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color4'></div>
                            <div class='tab-caption' id='tabDiv4' runat="server">
                                <asp:LinkButton ID="tabBtn4" runat="server" Text="頁籤四" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color5'></div>
                            <div class='tab-caption' id='tabDiv5' runat="server">
                                <asp:LinkButton ID="tabBtn5" runat="server" Text="頁籤五" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color6'></div>
                            <div class='tab-caption' id='tabDiv6' runat="server">
                                <asp:LinkButton ID="tabBtn6" runat="server" Text="頁籤六" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class='Site_Tab' style="float: left;">
                            <div class='tab-color7'></div>
                            <div class='tab-caption' id='tabDiv7' runat="server">
                                <asp:LinkButton ID="tabBtn7" runat="server" Text="頁籤七" OnClick="tabBtn_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div style="width: 100%; border: 1px solid #EBEBEB;">
                        <div style="padding: 10px;">
                            <uc1:ucGridView ID="ucGridView1" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" ID="tab2">
            <HeaderTemplate>
                <div class='Site_Tab' style="background-color: transparent; border-style: none;">
                    <div class='tab-color5'></div>
                    <div class='tab-caption' style="background-color: transparent;">
                        相關資訊
                    </div>
                </div>
            </HeaderTemplate>
            <ContentTemplate>
                <uc1:ucPageInfo runat="server" ID="ucPageInfo2" ucIsShowApplication="false" ucIsShowQueryString="false" ucIsShowRequestForm="false" ucIsShowEnvironmentInfo="false" />
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

</asp:Content>

