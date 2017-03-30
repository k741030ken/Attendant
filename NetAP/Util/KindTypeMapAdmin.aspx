<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KindTypeMapAdmin.aspx.cs"
    Inherits="Util_KindTypeMapAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>KindTypeMapAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <br />
        <asp:Label ID="labErrMsg" runat="server" Text="" Visible="false"></asp:Label>
        <uc1:ucGridView ID="ucGridView1" runat="server" />
        <div style="display: none;">
            <!--固定隱藏區 -->
            <asp:Panel ID="pnlEdit" runat="server" meta:resourcekey="pnlEditResource1">
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="KindID" runat="server" ucWidth="200" ucMaxLength="30" ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucTextBox ID="TypeID" runat="server" ucWidth="200" ucMaxLength="30" ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="ItemID" runat="server" ucWidth="400" ucMaxLength="50" ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucCommSingleSelect ID="IsEnabled" runat="server" />
                </div>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="ItemName" runat="server" ucWidth="400" ucMaxLength="50" ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucTextBox ID="ItemProp1" runat="server" ucWidth="400" ucMaxLength="50" ucIsDispEnteredWords="true" />
                    <br />
                    <uc1:ucTextBox ID="ItemProp2" runat="server" ucWidth="400" ucMaxLength="50" ucIsDispEnteredWords="true" />
                    <br />
                    <uc1:ucTextBox ID="ItemProp3" runat="server" ucWidth="400" ucMaxLength="50" ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="ItemJSON" runat="server" ucRows="5" ucWidth="400" ucMaxLength="1000"
                        ucIsDispEnteredWords="true" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucTextBox ID="ItemRemark" runat="server" ucRows="3" ucWidth="400" ucMaxLength="200"
                        ucIsDispEnteredWords="true" />
                </div>
                <div style="text-align: center; height: 50px;">
                    <hr class="Util_clsHR" />
                    <asp:Button ID="btnSave" runat="server" Text="確　定" CssClass="Util_clsBtn" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="取　消" CssClass="Util_clsBtn"
                        OnClick="btnCancel_Click" />
                </div>
            </asp:Panel>
        </div>
        <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupHeight="350" />
    </div>
    </form>
</body>
</html>
