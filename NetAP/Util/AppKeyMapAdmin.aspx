<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppKeyMapAdmin.aspx.cs" Inherits="Util_AppKeyMapAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>AppKeyMap</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>
    <div id="DivQryArea" runat="server">
        <fieldset class='Util_Fieldset'>
            <legend class="Util_Legend">查詢條件</legend>
            <div class="Util_clsRow1">
                <uc1:ucTextBox ID="qryKeyID" ucCaption="編號鍵值" runat="server" ucMaxLength="50" ucWidth="150" />
                <asp:CheckBox ID="qryIsLikeMsgBody" Text="[包含類似內容]" runat="server" />
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
                            <uc1:ucTextBox runat="server" ID="KeyID" ucCaption="編號鍵值" ucMaxLength="20" ucWidth="100"
                                ucIsRequire="true" />
                            <asp:CheckBox ID="IsLock" Text="鎖定" runat="server" />
                        </div>
                        <div class="Util_clsRow2">
                            <uc1:ucTextBox runat="server" ID="KeyBaseDate" ucCaption="編號基礎" ucMaxLength="8" ucWidth="100"
                                ucTextData="YYYYMMDD" />
                            <ul>
                                <li>[編號基礎]適用以下任一規則</li>
                                <li>以 [YYYY] or [YYYYMM] or [YYYYMMDD]為計算基礎，當基礎變動時，流水號會自動歸零重算</li>
                                <li>當[編號基礎]與[編號鍵值]內容相同或為[空白]時，流水號不會歸零，一直續編</li>
                            </ul>
                        </div>
                        <div class="Util_clsRow1">
                            <uc1:ucTextBox runat="server" ID="KeyFormat" ucCaption="編號格式" ucMaxLength="50" ucWidth="200"
                                ucTextData="{0}.{1}" ucIsRequire="true" />
                            <uc1:ucTextBox runat="server" ID="SeqNoLen" ucCaption="流水號長度" ucMaxLength="3" ucWidth="25"
                                ucTextData="3" ucIsRequire="true" />
                            <uc1:ucTextBox runat="server" ID="LastSeqNo" ucCaption="最後流水號" ucMaxLength="9" ucWidth="100"
                                ucTextData="0" ucIsRequire="true" />
                        </div>
                        <div class="Util_clsRow2">
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
                            <uc1:ucTextBox runat="server" ID="KeyID" ucCaption="編號鍵值" ucMaxLength="20" ucWidth="100"
                                ucIsRequire="true" />
                            <asp:CheckBox ID="IsLock" Text="鎖定" runat="server" />
                        </div>
                        <div class="Util_clsRow2">
                            <uc1:ucTextBox runat="server" ID="KeyBaseDate" ucCaption="編號基礎" ucMaxLength="8" ucWidth="100"
                                ucTextData="YYYYMMDD" />
                            <ul>
                                <li>[編號基礎]適用以下任一規則</li>
                                <li>以 [YYYY] or [YYYYMM] or [YYYYMMDD]為計算基礎，當基礎變動時，流水號會自動歸零重算</li>
                                <li>當[編號基礎]與[編號鍵值]內容相同或為[空白]時，流水號不會歸零，一直續編</li>
                            </ul>
                        </div>
                        <div class="Util_clsRow1">
                            <uc1:ucTextBox runat="server" ID="KeyFormat" ucCaption="編號格式" ucMaxLength="50" ucWidth="200"
                                ucTextData="{0}.{1}" ucIsRequire="true" />
                            <uc1:ucTextBox runat="server" ID="SeqNoLen" ucCaption="流水號長度" ucMaxLength="3" ucWidth="25"
                                ucTextData="3" ucIsRequire="true" />
                            <uc1:ucTextBox runat="server" ID="LastSeqNo" ucCaption="最後流水號" ucMaxLength="9" ucWidth="100"
                                ucTextData="0" ucIsRequire="true" />
                        </div>
                        <div class="Util_clsRow2">
                            <uc1:ucTextBox runat="server" ID="Remark" ucCaption="備　　註" ucRows="5" ucMaxLength="100"
                                ucWidth="450" />
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
