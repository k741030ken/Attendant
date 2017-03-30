<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CodeMapAdmin.aspx.cs" Inherits="Util_CodeMapAdmin"
    MaintainScrollPositionOnPostback="true" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>CodeMapAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <br />
            <div id="DivQryArea" runat="server">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">查詢條件</legend>
                    <div class="Util_clsRow1">
                        <uc1:ucCommSingleSelect ID="qryTabName" runat="server" ucCaption="TabName" ucIsSearchEnabled="false" />
                    </div>
                    <div class="Util_clsRow2">
                        <uc1:ucTextBox ID="qryFldName" ucCaption="FldName" runat="server" ucMaxLength="50" ucWidth="150" />
                        <asp:CheckBox ID="qryIsLikeFldName" Text="[包含類似內容]" runat="server" />
                    </div>
                    <div class="Util_clsRow1">
                        <uc1:ucTextBox ID="qryValue" ucCaption="Value" runat="server" ucMaxLength="50" ucWidth="150" />
                        <asp:CheckBox ID="qryIsLikeValue" Text="[包含類似內容]" runat="server" />
                    </div>
                    <div class="Util_clsRow2">
                        <uc1:ucTextBox ID="qryDescription" ucCaption="Description" runat="server" ucMaxLength="50"
                            ucWidth="150" />
                        <asp:CheckBox ID="qryIsLikeDescription" Text="[包含類似內容]" runat="server" />
                    </div>
                    <div class="Util_clsRow1">
                        <uc1:ucTextBox ID="qryRemark" ucCaption="Remark" runat="server" ucMaxLength="50"
                            ucWidth="150" />
                        <asp:CheckBox ID="qryIsLikeRemark" Text="[包含類似內容]" runat="server" />
                    </div>
                    <div style="text-align: center; height: 50px;">
                        <hr class="Util_clsHR" />
                        <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="80" Text="查　　詢"
                            OnClick="btnQry_Click" />
                        <asp:Button runat="server" ID="btnQryClear" CssClass="Util_clsBtnGray" Width="80"
                            Text="清　　除" OnClick="btnQryClear_Click" />
                    </div>
                </fieldset>
            </div>
            <asp:Label ID="labErrMsg" runat="server" Text="" Visible="false"></asp:Label>
            <br />
            <uc1:ucGridView ID="ucGridView1" runat="server" />
            <div style="display: none;">
                <!--固定隱藏區 -->
                <asp:Panel ID="pnlEdit" runat="server" meta:resourcekey="pnlEditResource1">
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucTextBox ID="TabName" runat="server" ucWidth="400" ucMaxLength="30" ucIsDispEnteredWords="true" />
                    </div>
                    <div class="Util_clsRow2" style="padding: 3px;">
                        <uc1:ucTextBox ID="FldName" runat="server" ucWidth="400" ucMaxLength="30" ucIsDispEnteredWords="true" />
                    </div>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucTextBox ID="Value" runat="server" ucWidth="400" ucMaxLength="20" ucIsDispEnteredWords="true" />
                    </div>
                    <div class="Util_clsRow2" style="padding: 3px;">
                        <uc1:ucTextBox ID="Description" runat="server" ucWidth="400" ucMaxLength="100" ucIsDispEnteredWords="true" />
                    </div>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucTextBox ID="Remark" runat="server" ucRows="3" ucWidth="400" ucMaxLength="100"
                            ucIsDispEnteredWords="true" />
                    </div>
                    <div style="text-align: center; height: 50px;">
                        <hr class="Util_clsHR" />
                        <asp:Button ID="btnSave" runat="server" Text="確　定" CssClass="Util_clsBtn" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="取　消" CssClass="Util_clsBtn"
                            OnClick="btnCancel_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDumpSQL" HorizontalAlign="Center">
                    <uc1:ucTextBox ID="txtDumpSQL" runat="server" ucWidth="750" ucRows="35" ucIsReadOnly="true" />
                </asp:Panel>
            </div>
            <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupHeight="350" />
        </div>
    </form>
</body>
</html>
