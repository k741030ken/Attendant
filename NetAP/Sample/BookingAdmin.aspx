<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingAdmin.aspx.cs" Inherits="Sample_BookingAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucScheduler.ascx" TagPrefix="uc1" TagName="ucScheduler" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagPrefix="uc1" TagName="ucCommSingleSelect" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagPrefix="uc1" TagName="ucCommMultiSelect" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Booking</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Util_Container">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
            </asp:ToolkitScriptManager>
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">會議室預約</legend>
                <asp:TabContainer ID="TabContainer1" runat="server" CssClass="Util_ajax__tab_theme"
                    Width="100%" AutoPostBack="false">
                    <asp:TabPanel runat="server" ID="TabKind1" HeaderText="進行預約">
                        <ContentTemplate>
                            <div class="Util_clsRow1" style="padding-left: 10px;">
                                <uc1:ucCommSingleSelect runat="server" ID="ddlRoom" ucSearchBoxWaterMarkText="搜尋會議室" />
                            </div>
                            <div class="Util_clsRow2" style="padding-left: 10px;">
                                <asp:CheckBox ID="chkAllowNonOwnerEdit" Checked="false" runat="server" Text="允許修改別人的預約" />
                            </div>
                            <div class="Util_clsRow1" style="padding-left: 10px;">
                                <asp:CheckBox ID="chkAllowConflict" Checked="false" runat="server" Text="同時段允許多個預約" />
                            </div>
                            <hr class="Util_clsHR" />
                            <div style="padding-left: 80px; height: 50px;">
                                <asp:Button runat="server" ID="btnBooking" CssClass="Util_clsBtnGray" Width="180px" Text="開　　始" OnClick="btnBooking_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabKind2" runat="server" HeaderText="預約查詢">
                        <ContentTemplate>
                            <div class="Util_clsRow2" style="padding-left: 10px;">
                                <uc1:ucCommMultiSelect runat="server" ID="qryRoomList" />
                            </div>
                            <hr class="Util_clsHR" />
                            <div style="padding-left: 170px; height: 50px;">
                                <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="280px" Text="開　　始" OnClick="btnQry_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </fieldset>
            <br />
            <br />
            <div id="divRoomScheArea" runat="server" style="display: none;">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">
                        <asp:Label ID="labScheInfo" Text="" runat="server" />
                    </legend>
                    <div style="float: left;">
                        <uc1:ucScheduler runat="server" ID="ucScheduler" />
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
