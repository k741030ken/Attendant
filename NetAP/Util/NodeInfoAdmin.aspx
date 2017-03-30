<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeInfoAdmin.aspx.cs" Inherits="Util_NodeInfoAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>NodeInfoAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <div id="DivQryArea" runat="server">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">查詢條件</legend>
                <div class="Util_clsRow1">
                    <uc1:ucTextBox ID="qryNodeID" ucCaption="NodeID" runat="server" ucMaxLength="30"
                        ucWidth="150" />
                    <asp:CheckBox ID="qryIsLikeNodeID" Text="[包含類似內容]" runat="server" />
                </div>
                <div class="Util_clsRow2">
                    <uc1:ucTextBox ID="qryNodeName" ucCaption="NodeName" runat="server" ucMaxLength="50"
                        ucWidth="150" />
                    <asp:CheckBox ID="qryIsLikeNodeName" Text="[包含類似內容]" runat="server" />
                </div>
                <div class="Util_clsRow1">
                    <uc1:ucCommSingleSelect ucCaption="Parent" ucIsSearchEnabled="false" runat="server" ID="qryParentNodeID" ucDropDownSourceListWidth="280" />
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
        <br />
        <div id="DivDataArea" runat="server">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">資料維護</legend>
                <div id="divMainGridview" runat="server" visible="true">
                    <%--資料列表區塊--%>
                    <uc1:ucGridView ID="ucGridView1" runat="server" />
                </div>
                <%-- FormView 實際處理 [新增][編輯] --%>
                <div id="divMainFormView" runat="server" visible="false">
                    <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound">
                        <InsertItemTemplate>
                            <%--[新增]範本--%>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="NodeID" ucCaption="NodeID" ucMaxLength="50" ucWidth="150"
                                    ucIsRequire="true" />
                                <uc1:ucTextBox runat="server" ID="NodeName" ucCaption="NodeName" ucMaxLength="150"
                                    ucWidth="150" ucIsRequire="true" />
                                <uc1:ucTextBox runat="server" ID="OrderSeq" ucCaption="OrderSeq" ucMaxLength="3"
                                    ucWidth="20" ucIsRequire="true" ucTextData="0" />
                                <asp:CheckBox runat="server" ID="IsEnabled" Text="IsEnabled" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCommSingleSelect runat="server" ID="ParentNodeID" ucCaption="ParentNodeID"
                                    ucCaptionWidth="150" />
                                (TargetUrl = '')
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucCommSingleSelect runat="server" ID="DefaultEnabledNodeID" ucCaption="DefaultEnabledNodeID"
                                    ucCaptionWidth="150" />
                                (TargetUrl != '')
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="ChkGrantID" ucCaption="ChkGrantID" ucMaxLength="50"
                                    ucWidth="150" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="TargetUrl" ucCaption="TargetUrl" ucMaxLength="150"
                                    ucWidth="650" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="TargetPara" ucCaption="TargetPara" ucMaxLength="300"
                                    ucWidth="650" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="ImageUrl" ucCaption="ImageUrl" ucMaxLength="100" ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="ToolTip" ucCaption="ToolTip" ucRows="3"
                                    ucMaxLength="100" ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="Remark" ucRows="5" ucMaxLength="200"
                                    ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnInsert" Text="新　　增" CssClass="Util_clsBtn" OnClick="btnInsert_Click" />
                                <asp:Button runat="server" ID="btnInsertCancel" Text="取　　消" CssClass="Util_clsBtn"
                                    OnClick="btnInsertCancel_Click" CausesValidation="false" />
                            </div>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <%--[編輯]範本--%>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="NodeID" ucCaption="NodeID" ucMaxLength="50" ucWidth="150"
                                    ucIsRequire="true" />
                                <uc1:ucTextBox runat="server" ID="NodeName" ucCaption="NodeName" ucMaxLength="150"
                                    ucWidth="150" ucIsRequire="true" />
                                <uc1:ucTextBox runat="server" ID="OrderSeq" ucCaption="OrderSeq" ucMaxLength="3"
                                    ucWidth="20" ucIsRequire="true" />
                                <asp:CheckBox runat="server" ID="IsEnabled" Text="IsEnabled" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCommSingleSelect runat="server" ID="ParentNodeID" ucCaption="ParentNodeID"
                                    ucCaptionWidth="150" />
                                (TargetUrl = '')
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucCommSingleSelect runat="server" ID="DefaultEnabledNodeID" ucCaption="DefaultEnabledNodeID"
                                    ucCaptionWidth="150" />
                                (TargetUrl != '')
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="ChkGrantID" ucCaption="ChkGrantID" ucMaxLength="50"
                                    ucWidth="150" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="TargetUrl" ucCaption="TargetUrl" ucMaxLength="150"
                                    ucWidth="650" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="TargetPara" ucCaption="TargetPara" ucMaxLength="300"
                                    ucWidth="650" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="ImageUrl" ucCaption="ImageUrl" ucMaxLength="100" ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="ToolTip" ucCaption="ToolTip" ucRows="3"
                                    ucMaxLength="100" ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="Remark" ucRows="5" ucMaxLength="200"
                                    ucWidth="450" ucIsDispEnteredWords="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox ID="UpdUser" runat="server" ucCaption="UpdUser" ucIsReadOnly="true"
                                    ucWidth="120" />
                                <uc1:ucTextBox ID="UpdDateTime" runat="server" ucCaption="UpdDateTime" ucIsReadOnly="true"
                                    ucWidth="120" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnUpdate" Text="更　　新" CssClass="Util_clsBtn" OnClick="btnUpdate_Click" />
                                <asp:Button runat="server" ID="btnUpdateCancel" Text="取　　消" CssClass="Util_clsBtn"
                                    OnClick="btnUpdateCancel_Click" CausesValidation="false" />
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </div>
            </fieldset>
        </div>
        <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupHeight="350" />
    </form>
</body>
</html>
