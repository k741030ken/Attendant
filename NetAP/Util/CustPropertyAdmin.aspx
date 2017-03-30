<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustPropertyAdmin.aspx.cs" Inherits="Util_CustPropertyAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>CustProperty</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <div id="DivQryArea" runat="server">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">查詢條件</legend>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="qryPKID" ucCaption="PKID" runat="server" ucMaxLength="30" ucWidth="250" />
                    <asp:CheckBox ID="qryIsLikePKID" Text="[包含類似內容]" runat="server" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucTextBox ID="qryPKKind" ucCaption="PKKind" runat="server" ucMaxLength="20" ucWidth="250" />
                    <asp:CheckBox ID="qryIsLikePKKind" Text="[包含類似內容]" runat="server" />
                </div>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="qryPropID" ucCaption="PropID" runat="server" ucMaxLength="50" ucWidth="250" />
                    <asp:CheckBox ID="qryIsLikePropID" Text="[包含類似內容]" runat="server" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucCommSingleSelect runat="server" ID="qryIsPropJSON" ucCaption="PropJSON" />
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
                                <uc1:ucTextBox runat="server" ID="PKID" ucCaption="PKID" ucMaxLength="30" ucWidth="250" ucIsRequire="true" />
                                <uc1:ucTextBox runat="server" ID="PKKind" ucCaption="PKKind" ucMaxLength="20" ucWidth="250" ucIsRequire="true" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="PropID" ucCaption="PropID" ucMaxLength="50" ucWidth="250" ucIsRequire="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="Prop1" ucCaption="Prop1" ucMaxLength="50" ucWidth="250" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="Prop2" ucCaption="Prop2" ucMaxLength="50" ucWidth="250" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="Prop3" ucCaption="Prop3" ucMaxLength="50" ucWidth="250" />
                                <br />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="PropJSON" ucCaption="PropJSON" ucRows="5"
                                    ucMaxLength="5000" ucWidth="450" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="Remark" ucRows="5" ucMaxLength="100"
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
                                <uc1:ucTextBox runat="server" ID="PKID" ucCaption="PKID" ucMaxLength="30" ucWidth="250" ucIsRequire="true" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="PKKind" ucCaption="PKKind" ucMaxLength="20" ucWidth="250" ucIsRequire="true" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="PropID" ucCaption="PropID" ucMaxLength="50" ucWidth="250" ucIsRequire="true" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="Prop1" ucCaption="Prop1" ucMaxLength="50" ucWidth="250" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="Prop2" ucCaption="Prop2" ucMaxLength="50" ucWidth="250" />
                                <br />
                                <uc1:ucTextBox runat="server" ID="Prop3" ucCaption="Prop3" ucMaxLength="50" ucWidth="250" />
                                <br />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="PropJSON" ucCaption="PropJSON" ucRows="5"
                                    ucMaxLength="5000" ucWidth="450" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="Remark" ucRows="5" ucMaxLength="100"
                                    ucWidth="450" />
                            </div>
                            <div class="Util_clsRow1">
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
    </form>
</body>
</html>
