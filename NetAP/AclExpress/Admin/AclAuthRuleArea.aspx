<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclAuthRuleArea.aspx.cs" Inherits="AclExpress_Admin_AclAuthRuleArea" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagPrefix="uc1" TagName="ucCascadingDropDown" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagPrefix="uc1" TagName="ucCheckBoxList" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>AclAuthRuleArea</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <div id="DivQryArea" runat="server">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">查詢條件</legend>
                <div class="Util_clsRow1">
                    <uc1:ucCommSingleSelect runat="server" ID="qryRuleID" ucCaptionWidth="120" ucCaption="規則代號" ucIsSearchEnabled="false" />
                </div>
                <div class="Util_clsRow2">
                    <uc1:ucCascadingDropDown runat="server" ID="qryAreaGrant" ucCaptionWidth="120" ucCaption="區域／項目" />
                </div>
                <div class="Util_clsRow1">
                    <uc1:ucCommSingleSelect runat="server" ID="qryAuthType" ucCaptionWidth="120" ucCaption="授權類型" ucIsSearchEnabled="false" />
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
                                <uc1:ucCommSingleSelect runat="server" ID="RuleID" ucCaption="規則代號" ucIsRequire="true" ucIsSearchEnabled="false" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCascadingDropDown runat="server" ID="AreaGrant" ucCaption="區域／項目" ucIsRequire01="true" ucIsRequire02="true" ucRequireMsg01="*" ucRequireMsg02="*" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucCommSingleSelect runat="server" ID="AuthType" ucCaption="授權類型" ucIsRequire="true" ucIsSearchEnabled="false" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCheckBoxList runat="server" ID="AllowActList" ucCaption="限定動作" ucRows="5" ucWidth="320" />
                                <div style="display: inline-block; vertical-align: bottom;">*授權類型為[Allow]時才有作用*</div>
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="備　　註" ucRows="5" ucMaxLength="100"
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
                                <uc1:ucCommSingleSelect runat="server" ID="RuleID" ucCaption="規則代號" ucIsRequire="true" ucIsSearchEnabled="false" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCascadingDropDown runat="server" ID="AreaGrant" ucCaption="區域／項目" ucIsRequire01="true" ucIsRequire02="true" ucRequireMsg01="*" ucRequireMsg02="*" />
                            </div>
                            <div class="Util_clsRow1">
                                <uc1:ucCommSingleSelect runat="server" ID="AuthType" ucCaption="授權類型" ucIsRequire="true" ucIsSearchEnabled="false" />
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucCheckBoxList runat="server" ID="AllowActList" ucCaption="限定動作" ucRows="5" ucWidth="320" />
                                <div style="display: inline-block; vertical-align: bottom;">*授權類型為[Allow]時才有作用*</div>
                            </div>
                            <div class="Util_clsRow2">
                                <uc1:ucTextBox runat="server" ID="Remark" ucCaption="備　　註" ucRows="5" ucMaxLength="100"
                                    ucWidth="450" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnUpdate" Text="更　　新" CssClass="Util_clsBtn" OnClick="btnUpdate_Click" />
                                <asp:Button runat="server" ID="btnDelete" Text="刪　　除" CssClass="Util_clsBtn" CausesValidation="false" OnClick="btnDelete_Click" />
                                <asp:Button runat="server" ID="btnUpdateCancel" Text="取　　消" CssClass="Util_clsBtn" OnClick="btnUpdateCancel_Click" CausesValidation="false" />
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </div>
            </fieldset>
        </div>
        <%--固定隱藏區--%>
        <div id="DivHiddenArea" style="display: none;">
            <asp:Panel runat="server" ID="pnlDumpSQL" HorizontalAlign="Center">
                <uc1:ucTextBox ID="txtDumpSQL" runat="server" ucWidth="750" ucRows="35" ucIsReadOnly="true" />
            </asp:Panel>
        </div>
        <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
    </form>
</body>
</html>
