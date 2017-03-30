<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppTableLogQry.aspx.cs" Inherits="Util_AppTableLogQry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucUserPicker.ascx" TagPrefix="uc1" TagName="ucUserPicker" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AppLogQry</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:Label ID="labErrMsg" runat="server" Text="" Visible="false"></asp:Label>
            <div runat="server" id="DivSelectTable" visible="false">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">欲查詢的資料表</legend>
                    <uc1:ucCommSingleSelect runat="server" ID="ddlTableName" />
                    <asp:Button ID="btnTableName" runat="server" CssClass="Util_clsBtnGray" Width="80" Text="確　　定" OnClick="btnTableName_Click" />
                </fieldset>
            </div>
            <div runat="server" id="DivQryCondition" visible="true">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">
                        <asp:Label ID="labQryHeader" runat="server"></asp:Label>
                        <asp:LinkButton ID="btnPurgeTable" runat="server" OnClick="btnPurgeTable_Click">
                            <asp:Image ID="imgPurce" runat="server" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Delete.png" />
                        </asp:LinkButton></legend>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <div style="width: 800px;">
                            <uc1:ucTextBox ID="PKey01" ucCaption="PK01" runat="server" />
                            <asp:CheckBox ID="IsLikePKey01" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey02" ucCaption="PK02" runat="server" />
                            <asp:CheckBox ID="IsLikePKey02" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey03" ucCaption="PK03" runat="server" />
                            <asp:CheckBox ID="IsLikePKey03" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey04" ucCaption="PK04" runat="server" />
                            <asp:CheckBox ID="IsLikePKey04" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey05" ucCaption="PK05" runat="server" />
                            <asp:CheckBox ID="IsLikePKey05" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey06" ucCaption="PK06" runat="server" />
                            <asp:CheckBox ID="IsLikePKey06" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey07" ucCaption="PK07" runat="server" />
                            <asp:CheckBox ID="IsLikePKey07" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey08" ucCaption="PK08" runat="server" />
                            <asp:CheckBox ID="IsLikePKey08" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey09" ucCaption="PK09" runat="server" />
                            <asp:CheckBox ID="IsLikePKey09" Text="[包含類似內容]" runat="server" />
                            <uc1:ucTextBox ID="PKey10" ucCaption="PK10" runat="server" />
                            <asp:CheckBox ID="IsLikePKey10" Text="[包含類似內容]" runat="server" />
                        </div>
                    </div>
                    <div class="Util_clsRow2" style="padding: 3px;">
                        <uc1:ucUserPicker runat="server" ID="LogUser" ucCaption="LogUser" ucCaptionWidth="100" ucWidth="200" />
                    </div>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucCommSingleSelect ID="LogType" ucCaption="LogType" ucCaptionWidth="100" runat="server" ucIsHasEmptyItem="true" ucIsSearchEnabled="false" />
                    </div>
                    <div class="Util_clsRow2" style="padding: 3px;">
                        <uc1:ucDatePicker ID="LogDate1" ucCaption="LogDate" ucCaptionWidth="100" runat="server" />
                        ～
                        <uc1:ucDatePicker ID="LogDate2" runat="server" />
                    </div>
                    <div style="text-align: center; height: 50px;">
                        <hr class="Util_clsHR" />
                        <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="80" Text="查　　詢"
                            OnClick="btnQry_Click" />
                        <asp:Button runat="server" ID="btnClear" CssClass="Util_clsBtnGray" Width="80"
                            Text="清　　除" OnClick="btnClear_Click" />
                        <asp:Button runat="server" ID="btnClearTableName" CssClass="Util_clsBtnGray" Width="80"
                            Text="選擇資料表" OnClick="btnClearTableName_Click" />
                    </div>
                </fieldset>
            </div>
            <br />
            <div runat="server" id="DivQryResult" visible="false">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend"><asp:Label ID="labQryResultHeader" runat="server"></asp:Label></legend>
                    <div class="Util_Frame">
                        <uc1:ucGridView ID="ucGridView1" runat="server" />
                    </div>
                </fieldset>
            </div>
        </div>
    </form>
</body>
</html>
