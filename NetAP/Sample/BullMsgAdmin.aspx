<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BullMsgAdmin.aspx.cs" Inherits="Sample_BullMsgAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTimePicker.ascx" TagName="ucTimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>BullMsg</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Util_Container">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
            </asp:ToolkitScriptManager>
            <div id="DivQryArea" runat="server">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">查詢條件</legend>
                    <div class="Util_clsRow1">
                        <uc1:ucCommSingleSelect ID="qryMsgKind" runat="server" ucCaption="訊息類別" ucIsSearchEnabled="false" />
                    </div>
                    <div class="Util_clsRow2">
                        <uc1:ucTextBox ID="qryMsgBody" ucCaption="訊息內容" runat="server" ucMaxLength="50"
                            ucWidth="150" />
                        <asp:CheckBox ID="qryIsLikeMsgBody" Text="[包含類似內容]" runat="server" />
                    </div>
                    <div class="Util_clsRow1">
                        <uc1:ucDatePicker ID="qryStartDate1" runat="server" ucCaption="起始日期" />
                        ～
                <uc1:ucDatePicker ID="qryStartDate2" runat="server" />
                    </div>
                    <div class="Util_clsRow2">
                        <uc1:ucDatePicker ID="qryEndDate1" runat="server" ucCaption="截止日期" />
                        ～
                <uc1:ucDatePicker ID="qryEndDate2" runat="server" />
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
                    <div id="divMainFormView" runat="server" visible="false"  >
                        <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" Width="600">
                            <InsertItemTemplate>
                                <%--[新增]範本--%>
                                <div class="Util_clsRow1">
                                    <uc1:ucTextBox runat="server" ID="MsgID" ucCaption="訊息ID" ucMaxLength="50" ucWidth="100"
                                        ucIsRequire="true" />
                                    <uc1:ucCommSingleSelect runat="server" ID="MsgKind" ucCaption="訊息類別" ucIsRequire="true"
                                        ucIsSearchEnabled="false" />
                                    <asp:CheckBox ID="IsEnabled" Text="啟用" runat="server" />
                                </div>
                                <div class="Util_clsRow2">
                                    <uc1:ucDatePicker ID="StartDate" runat="server" ucCaption="起始時間" ucIsRequire="true" />
                                    <uc1:ucTimePicker ID="StartTime" runat="server" />
                                    <uc1:ucDatePicker ID="EndDate" runat="server" ucCaption="截止時間" ucIsRequire="true" />
                                    <uc1:ucTimePicker ID="EndTime" runat="server" />
                                </div>
                                <div class="Util_clsRow1">
                                    <uc1:ucTextBox runat="server" ID="MsgBody" ucCaption="訊息內容" ucRows="5" ucMaxLength="500"
                                        ucWidth="450" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow2">
                                    <uc1:ucTextBox runat="server" ID="MsgUrl" ucCaption="連結網址" ucMaxLength="150" ucWidth="450" />
                                </div>
                                <div class="Util_clsRow1">
                                    <uc1:ucTextBox runat="server" ID="Remark" ucCaption="備　　註" ucRows="5" ucMaxLength="200"
                                        ucWidth="450" />
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
                                    <uc1:ucTextBox runat="server" ID="MsgID" ucCaption="訊息ID" ucMaxLength="50" ucWidth="100"
                                        ucIsRequire="true" />
                                    <uc1:ucCommSingleSelect runat="server" ID="MsgKind" ucCaption="訊息類別" ucIsRequire="true"
                                        ucIsSearchEnabled="false" />
                                    <asp:CheckBox ID="IsEnabled" Text="啟用" runat="server" />
                                </div>
                                <div class="Util_clsRow2">
                                    <uc1:ucDatePicker ID="StartDate" runat="server" ucCaption="起始時間" ucIsRequire="true" />
                                    <uc1:ucTimePicker ID="StartTime" runat="server" ucIsRequire="true" />
                                    <uc1:ucDatePicker ID="EndDate" runat="server" ucCaption="截止時間" ucIsRequire="true" />
                                    <uc1:ucTimePicker ID="EndTime" runat="server" />
                                </div>
                                <div class="Util_clsRow1">
                                    <uc1:ucTextBox runat="server" ID="MsgBody" ucCaption="訊息內容" ucRows="5" ucMaxLength="500"
                                        ucWidth="450" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow2">
                                    <uc1:ucTextBox runat="server" ID="MsgUrl" ucCaption="連結網址" ucMaxLength="150" ucWidth="450" />
                                </div>
                                <div class="Util_clsRow1">
                                    <uc1:ucTextBox runat="server" ID="Remark" ucCaption="備　　註" ucRows="5" ucMaxLength="200"
                                        ucWidth="450" />
                                </div>
                                <div class="Util_clsRow2">
                                    <uc1:ucTextBox ID="UpdUser" runat="server" ucCaption="更新人員" ucIsReadOnly="true"
                                        ucWidth="120" />
                                    <uc1:ucTextBox ID="UpdDateTime" runat="server" ucCaption="更新時間" ucIsReadOnly="true"
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
        </div>
    </form>
</body>
</html>
