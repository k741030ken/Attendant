<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclRule.aspx.cs" Inherits="AclExpress_Admin_AclRule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AclRule</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div id="divError" runat="server" visible="false">
                <asp:Label ID="labErrMsg" runat="server"></asp:Label>
            </div>
            <div id="DivMainList" runat="server">
                <fieldset class="Util_Fieldset">
                    <legend class="Util_Legend">規則清單</legend>
                    <%--主檔列表--%>
                    <uc1:ucGridView ID="ucGridMain" runat="server" />
                </fieldset>
            </div>
            <%--主檔單筆顯示區--%>
            <div id="DivMainSingle" runat="server" style="width: 100%;">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">規則</legend>
                    <%--主檔表單--%>
                    <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" CssClass="Util_CenterBody">
                        <InsertItemTemplate>
                            <%--[Add,Copy]用範本--%>
                            <div class="Util_clsRow1" style="padding: 3px;">
                                <uc1:ucTextBox ID="RuleID" runat="server" ucCaption="規則代號" ucIsRequire="true" ucWidth="150" />
                                <uc1:ucTextBox ID="RuleName" runat="server" ucCaption="規則名稱" ucIsRequire="true" ucWidth="200" />
                                <asp:CheckBox ID="IsEnabled" runat="server" Text="啟用" />
                            </div>
                            <div class="Util_clsRow2" style="padding: 3px;">
                                <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucRows="5" ucIsDispEnteredWords="true"
                                    ucWidth="450" ucMaxLength="200" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnMainInsert" Text="新　　增" CssClass="Util_clsBtn"
                                    OnClick="btnMainInsert_Click" />
                                <asp:Button runat="server" ID="btnMainInsertCancel" Text="取　　消" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainCancel_Click" />
                            </div>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <%--[Edit]用範本--%>
                            <div class="Util_clsRow1" style="padding: 3px;">
                                <uc1:ucTextBox ID="RuleID" runat="server" ucCaption="規則代號" ucIsReadOnly="true" ucWidth="150" />
                                <uc1:ucTextBox ID="RuleName" runat="server" ucCaption="規則名稱" ucIsReadOnly="true" ucWidth="200" />
                                <asp:CheckBox ID="IsEnabled" runat="server" Text="啟用" />
                            </div>
                            <div class="Util_clsRow2" style="padding: 3px;">
                                <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucRows="5" ucIsDispEnteredWords="true" ucWidth="450" ucMaxLength="200" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnMainUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                    OnClick="btnMainUpdate_Click" />
                                <asp:Button runat="server" ID="btnMainDelete" Text="刪　　除" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainDelete_Click" />
                                <asp:Button runat="server" ID="btnMainUpdateCancel" Text="取　　消" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainCancel_Click" />
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </fieldset>
            </div>
            <br />
            <%--明細檔顯示區--%>
            <div id="DivDetailArea" runat="server">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">規則條件清單</legend>
                    <uc1:ucGridView ID="ucGridDetail" runat="server" />
                </fieldset>
            </div>
            <%--固定隱藏區--%>
            <div id="DivHiddenArea" style="display: none;">
                <%--明細檔單筆編輯表單--%>
                <asp:Panel runat="server" ID="pnlDetailForm" CssClass="Util_Frame">
                    <div style="text-align: left; padding: 5px;">
                        <asp:FormView ID="fmDetail" runat="server" OnDataBound="fmDetail_DataBound" Style="width: 100%;">
                            <InsertItemTemplate>
                                <%--[Add,Copy]用範本--%>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <asp:CheckBox ID="IsEnabled" Text="啟用" runat="server" />
                                    <uc1:ucTextBox ID="ChkGrpNo" runat="server" ucCaption="群組" ucWidth="20" ucIsRequire="true" ucCaptionWidth="70" />
                                    <uc1:ucTextBox ID="ChkSeqNo" runat="server" ucCaption="順序" ucWidth="20" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucCommSingleSelect ID="ChkOrgUserObjectProperty" runat="server" ucCaption="檢核物件" ucIsRequire="true" ucIsSearchEnabled="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <asp:CheckBox ID="ChkOrgUserSW" runat="server" Text="" />
                                    <uc1:ucCommSingleSelect ID="ChkOrgUserExp" runat="server" ucCaption="比對條件" ucCaptionWidth="100" ucIsSearchEnabled="false" ucDropDownSourceListWidth="150" />
                                    <uc1:ucTextBox ID="ChkOrgUserPropertyValue" runat="server" ucCaption="比對值" ucWidth="200" ucCaptionWidth="65" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <asp:CheckBox ID="ChkCodeMapSW" runat="server" Text="" />
                                    <uc1:ucTextBox ID="ChkCodeMapFldName" runat="server" ucCaption="對照表欄位" ucWidth="180" ucCaptionWidth="100" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucCaptionWidth="123" ucRows="3" ucIsDispEnteredWords="false" ucWidth="400" ucMaxLength="200" />
                                </div>
                                <div style="text-align: center; height: 50px;">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnDetailInsert" Text="新　　增" CssClass="Util_clsBtn"
                                        OnClick="btnDetailInsert_Click" />
                                    <asp:Button runat="server" ID="btnDetailInsertCancel" Text="取　　消" CssClass="Util_clsBtn"
                                        CausesValidation="false" OnClick="btnDetailInsertCancel_Click" />
                                </div>
                            </InsertItemTemplate>
                            <EditItemTemplate>
                                <%--[Edit]用範本--%>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <asp:CheckBox ID="IsEnabled" Text="啟用" runat="server" />
                                    <uc1:ucTextBox ID="ChkGrpNo" runat="server" ucCaption="群組" ucWidth="20" ucIsRequire="true" ucCaptionWidth="70" />
                                    <uc1:ucTextBox ID="ChkSeqNo" runat="server" ucCaption="順序" ucWidth="20" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucCommSingleSelect ID="ChkOrgUserObjectProperty" runat="server" ucCaption="檢核物件" ucIsRequire="true" ucIsSearchEnabled="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <asp:CheckBox ID="ChkOrgUserSW" runat="server" Text="" />
                                    <uc1:ucCommSingleSelect ID="ChkOrgUserExp" runat="server" ucCaption="比對條件" ucCaptionWidth="100" ucIsSearchEnabled="false" ucDropDownSourceListWidth="150" />
                                    <uc1:ucTextBox ID="ChkOrgUserPropertyValue" runat="server" ucCaption="比對值" ucWidth="200" ucCaptionWidth="65" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <asp:CheckBox ID="ChkCodeMapSW" runat="server" Text="" />
                                    <uc1:ucTextBox ID="ChkCodeMapFldName" runat="server" ucCaption="對照表欄位" ucWidth="180" ucCaptionWidth="100" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucCaptionWidth="123" ucRows="3" ucIsDispEnteredWords="false" ucWidth="400" ucMaxLength="200" />
                                </div>
                                <div style="text-align: center; height: 50px;">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnDetailUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                        OnClick="btnDetailUpdate_Click" />
                                    <asp:Button runat="server" ID="btnDetailUpdateCancel" Text="取　　消" CssClass="Util_clsBtn"
                                        CausesValidation="false" OnClick="btnDetailUpdateCancel_Click" />
                                </div>
                            </EditItemTemplate>
                        </asp:FormView>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDumpSQL" HorizontalAlign="Center">
                    <uc1:ucTextBox ID="txtDumpSQL" runat="server" ucWidth="750" ucRows="35" ucIsReadOnly="true" />
                </asp:Panel>
            </div>
            <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
        </div>
    </form>
</body>
</html>
