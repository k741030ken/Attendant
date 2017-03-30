<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclAuthUserArea.aspx.cs" Inherits="AclExpress_AclAuthUserArea" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagPrefix="uc1" TagName="ucCascadingDropDown" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagPrefix="uc1" TagName="ucCommSingleSelect" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagPrefix="uc1" TagName="ucCommMultiSelect" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AclAuthUser</title>
    <script type="text/javascript">
        function ShowWaiting() {
            //顯示[處理中]訊息
            oID = document.getElementById('divWaiting');
            if (oID != null) {
                oID.style.display = '';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="labErrMsg" runat="server" Text="" Visible="false"></asp:Label>
        <div id="divBody" runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
            </asp:ToolkitScriptManager>
            <div id="divWaiting" runat="server" style="position: absolute; z-index: 9999; display: none; top: -500px; left: 0; right: 0; bottom: 0; margin: auto; padding: 35px; width: 300px; height: 110px; overflow: hidden; background: #FFF; text-align: center; border: 1px solid #aaa; -webkit-box-shadow: 0px 0px 5px 0px #ccc; -moz-box-shadow: 0px 0px 5px 0px #ccc; box-shadow: 0px 0px 5px 0px #ccc;">
                <asp:Image runat="server" ImageUrl="~/Util/WebClient/Legend_Waiting.gif" BorderWidth="0" />
                <br />
                <br />
                <br />
                <asp:Label ID="labWaiting" runat="server" Text="Wait..." ForeColor="#6F6F6C" Font-Bold="true" />
            </div>
            <%--授權範圍--%>
            <div id="DivScope" runat="server">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">選擇授權單位</legend>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucCascadingDropDown runat="server" ID="ucCompDept" />
                        <asp:Button runat="server" ID="btnScope" CssClass="Util_clsBtnGray" Width="80" Text="Go" OnClick="btnScope_Click" />
                    </div>
                </fieldset>
            </div>
            <%--主操作區--%>
            <div id="DivTabControl" runat="server" style="margin-top: 20px;" visible="false">
                <asp:TabContainer ID="TabContainer" runat="server" CssClass="Util_ajax__tab_theme"
                    ScrollBars="Auto" Width="100%" Height="550px" ActiveTabIndex="0">
                    <asp:TabPanel runat="server" HeaderText="依單位人員" ID="tabUser">
                        <ContentTemplate>
                            <div style="padding: 15px 5px 5px 5px;">
                                <div id="divSeleUserID" runat="server">
                                    <uc1:ucCommSingleSelect runat="server" ID="ucSeleUserID" ucCaption="選擇人員：" />
                                    <asp:Button runat="server" ID="btnSeleUserID" Text="Go" OnClick="btnSeleUserID_Click" />
                                </div>
                                <div id="divSeleGrantList" runat="server" visible="False">
                                    <uc1:ucCommMultiSelect runat="server" ID="ucSeleGrantList" ucBoxListWidth="320" ucBoxListHeight="320" />
                                    <div style="margin: 10px auto 10px 20px;">
                                        <asp:Button runat="server" ID="btnUpdByUserID" Text="更新授權資料" Width="750px" Height="30" OnClick="btnUpdByUserID_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="依功能項目" ID="tabGrant">
                        <ContentTemplate>
                            <div style="padding: 15px 5px 5px 5px;">
                                <div id="divSeleGrantID" runat="server">
                                    <uc1:ucCommSingleSelect runat="server" ID="ucSeleGrantID" ucCaption="選擇項目：" />
                                    <asp:Button runat="server" ID="btnSeleGrantID" Text="Go" OnClick="btnSeleGrantID_Click" />
                                </div>
                                <div id="divSeleUserList" runat="server" visible="False">
                                    <uc1:ucCommMultiSelect runat="server" ID="ucSeleUserList" ucBoxListWidth="320" ucBoxListHeight="320" />
                                    <div style="margin: 10px auto 10px 20px;">
                                        <asp:Button runat="server" ID="btnUpdByGrantID" Text="更新授權資料" Width="750px" Height="30" OnClick="btnUpdByGrantID_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Info" ID="tabAclInfo">
                        <ContentTemplate>
                            <div style="padding: 15px 5px 5px 5px;">
                                <asp:Label ID="labAclInfo" runat="server"></asp:Label>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </div>
    </form>
</body>
</html>
