<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderAdmin.aspx.cs" Inherits="Sample_OrderAdmin"
    UICulture="auto" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucUserPicker.ascx" TagName="ucUserPicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucToggleControlVisibility.ascx" TagName="ucToggleControlVisibility" TagPrefix="uc1" %>

<!DOCTYPE html >
<html>
<head runat="server">
    <title>訂單管理</title>
    <style>
        /* 示範自訂 ucGridMain [訂單總額] 欄位 CSS (相容IE) */
        #ucGridMain_gvMain .Util_gvHeader th:first-child + th + th + th + th + th + th + th {
            color: #FFFFFF;
            background-color: #C95FCB;
        }

            #ucGridMain_gvMain .Util_gvHeader th:first-child + th + th + th + th + th + th + th .Util_gvHeaderSortable {
                color: #FFFFFF;
                background-color: #C95FCB;
            }

            #ucGridMain_gvMain .Util_gvHeader th:first-child + th + th + th + th + th + th + th .Util_gvHeaderAsc {
                color: #FFFFFF;
                background-color: #C95FCB;
            }

            #ucGridMain_gvMain .Util_gvHeader th:first-child + th + th + th + th + th + th + th .Util_gvHeaderDesc {
                color: #FFFFFF;
                background-color: #C95FCB;
            }

        #ucGridMain_gvMain .Util_gvRowNormal td:first-child + td + td + td + td + td + td + td {
            color: #FFFFFF;
            background-color: #666565;
        }

        #ucGridMain_gvMain .Util_gvRowAlternate td:first-child + td + td + td + td + td + td + td {
            color: #FFFFFF;
            background-color: #C0C0C0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Util_Container">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
            </asp:ToolkitScriptManager>
            <%--主檔列表顯示區--%>
            <div id="DivMainList" runat="server">
                <%--查詢條件--%>
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">查詢條件</legend>
                    <div class="Util_clsRow1" style="padding: 3px;">
                        <uc1:ucDatePicker ID="QryDate1" runat="server" ucCaption="下單日期" />
                        ～
                            <uc1:ucDatePicker ID="QryDate2" runat="server" />
                    </div>
                    <hr class="Util_clsHR" />
                    <div style="text-align: center; height: 50px;">
                        <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="80" Text="查　　詢"
                            OnClick="btnQry_Click" />
                        <asp:Button runat="server" ID="btnClear" CssClass="Util_clsBtnGray" Width="80"
                            Text="清　　除" OnClick="btnClear_Click" />
                    </div>
                </fieldset>
                <br />
                <fieldset class="Util_Fieldset">
                    <legend class="Util_Legend">訂單列表</legend>
                    <%--主檔列表--%>
                    <uc1:ucGridView ID="ucGridMain" runat="server" />
                </fieldset>
            </div>
            <%--主檔單筆顯示區--%>
            <div id="DivMainSingle" runat="server" style="width: 100%;">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">單筆訂單</legend>
                    <%--主檔表單--%>
                    <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound" CssClass="Util_CenterBody">
                        <InsertItemTemplate>
                            <%--[新增]用範本--%>
                            <div class="Util_clsRow1" style="padding: 3px;">
                                <uc1:ucDatePicker ID="ShipDate" runat="server" ucCaption="出貨日期" ucIsRequire="true" />
                                <uc1:ucUserPicker ID="UserPicker" runat="server" ucCaption="下訂人員" ucIsRequire="true" />
                            </div>
                            <div class="Util_clsRow2" style="padding: 3px;">
                                <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucRows="5" ucIsDispEnteredWords="true"
                                    ucWidth="450" ucMaxLength="100" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnMainInsert" Text="新　　增" CssClass="Util_clsBtn"
                                    OnClick="btnMainInsert_Click" />
                                <asp:Button runat="server" ID="btnMainInsertCancel" Text="取　　消" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainInsertCancel_Click" />
                            </div>
                        </InsertItemTemplate>
                        <EditItemTemplate>
                            <%--[編輯]用範本--%>
                            <div class="Util_clsRow1" style="padding: 3px;">
                                <uc1:ucTextBox ID="POID" runat="server" ucCaption="訂單編號" ucIsReadOnly="true" ucWidth="80" />
                                <uc1:ucTextBox ID="PODate" runat="server" ucCaption="訂單日期" ucIsReadOnly="true" ucWidth="80" />
                            </div>
                            <div class="Util_clsRow2" style="padding: 3px;">
                                <uc1:ucUserPicker ID="UserPicker" runat="server" ucCaption="下訂人員" />
                                <uc1:ucDatePicker ID="ShipDate" runat="server" ucCaption="出貨日期" ucIsRequire="true" />
                                訂單總額：
                        <asp:Label runat="server" ID="labTotAmt" Width="80" Text='<%# string.Format("{0:N2}",Eval("TotAmt")) %>'
                            Style="text-align: right;" />
                            </div>
                            <div class="Util_clsRow1" style="padding: 3px;">
                                <uc1:ucTextBox ID="Remark" runat="server" ucCaption="備　　註" ucRows="5" ucIsDispEnteredWords="true" ucWidth="450" ucMaxLength="100" />
                            </div>
                            <div style="text-align: center; height: 50px;">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnMainUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                    OnClick="btnMainUpdate_Click" />
                                <asp:Button runat="server" ID="btnMainDelete" Text="刪　　除" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainDelete_Click" />
                                <asp:Button runat="server" ID="btnMainUpdateCancel" Text="返回清單" CssClass="Util_clsBtn"
                                    CausesValidation="false" OnClick="btnMainUpdateCancel_Click" />
                            </div>
                        </EditItemTemplate>
                    </asp:FormView>
                </fieldset>
            </div>
            <br />
            <%--明細檔顯示區--%>
            <div id="DivDetailArea" runat="server">
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">訂單明細(作法一)</legend>
                    <%--[選取項目]按鈕--%>
                    <div style="text-align: center; margin-bottom: 10px;">
                        <asp:Button ID="btnDetailBatchEdit" runat="server" Text="編輯[選取項目]" CssClass="Util_clsBtnGray"
                            Width="150px" OnClick="btnDetailBatchEdit_Click" />
                        <asp:Button ID="btnDetailBatchDelete" runat="server" Text="刪除[選取項目]" CssClass="Util_clsBtnGray"
                            Width="150px" OnClick="btnDetailBatchDelete_Click" />
                    </div>
                    <%--明細檔列表1--%>
                    <uc1:ucGridView ID="ucGridDetail1" runat="server" />
                </fieldset>
                <br />
                <br />
                <fieldset class='Util_Fieldset'>
                    <legend class="Util_Legend">訂單明細(作法二)</legend>
                    <%--明細檔列表2--%>
                    <uc1:ucGridView ID="ucGridDetail2" runat="server" />
                </fieldset>
            </div>
            <%--固定隱藏區
        因為本區內的Server元件，可能會在 Client端 動態顯示，故本 DivHiddenArea 必須使用 CSS 作隱藏，
        不可用Server端的Visable屬性
            --%>
            <div id="DivHiddenArea" style="display: none;">
                <%--明細檔單筆編輯表單--%>
                <asp:Panel runat="server" ID="pnlDetailForm" CssClass="Util_Frame">
                    <div style="text-align: left; padding: 5px;">
                        <asp:FormView ID="fmDetail" runat="server" OnDataBound="fmDetail_DataBound" Style="width: 100%;">
                            <InsertItemTemplate>
                                <%--[新增]用範本--%>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="ItemDesc" runat="server" ucCaption="項目說明" ucWidth="180" ucIsRequire="true" />
                                    <uc1:ucCommSingleSelect ID="Unit" runat="server" ucCaption="單位" ucIsRequire="true"
                                        ucDropDownSourceListWidth="100" ucIsSearchEnabled="false" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucDatePicker ID="ItemExpDate" runat="server" ucCaption="到期日期" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="UnitPrice" runat="server" ucCaption="項目單價" ucIsRequire="true"
                                        ucTextData="1" />
                                    <uc1:ucTextBox ID="Qty" runat="server" ucCaption="數量" ucIsRequire="true" ucTextData="1" />
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
                                <%--[編輯]用範本--%>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="ItemDesc" runat="server" ucCaption="項目說明" ucWidth="180" ucIsRequire="true" />
                                    <uc1:ucCommSingleSelect ID="Unit" runat="server" ucCaption="單位" ucIsRequire="true"
                                        ucDropDownSourceListWidth="100" ucIsSearchEnabled="false" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucDatePicker ID="ItemExpDate" runat="server" ucCaption="到期日期" ucIsRequire="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="UnitPrice" runat="server" ucCaption="項目單價" ucIsRequire="true" />
                                    <uc1:ucTextBox ID="Qty" runat="server" ucCaption="數量" ucIsRequire="true" />
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
                <%--明細檔整批編輯表單--%>
                <asp:Panel runat="server" ID="pnlDetailBatchForm" CssClass="Util_Frame">
                    <div style="text-align: left; padding: 5px;">
                        <asp:FormView ID="fmDetailBatch" runat="server" OnDataBound="fmDetailBatch_DataBound" Style="margin-left: auto; margin-right: auto;">
                            <InsertItemTemplate>
                                <div class="Util_gvHeader">
                                    <uc1:ucToggleControlVisibility ID="ucToggleControlVisibility1" runat="server" ucCaption="套用" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="ItemDesc" runat="server" ucCaption="項目說明" ucWidth="240" ucIsToggleVisibility="true" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucDatePicker ID="ItemExpDate" runat="server" ucCaption="到期日期" ucIsToggleVisibility="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucCommSingleSelect ID="Unit" runat="server" ucCaption="單　　位" ucIsToggleVisibility="true"
                                        ucIsSearchEnabled="false" />
                                </div>
                                <div class="Util_clsRow2" style="padding: 3px;">
                                    <uc1:ucTextBox ID="UnitPrice" runat="server" ucCaption="項目單價" ucIsToggleVisibility="true" />
                                </div>
                                <div class="Util_clsRow1" style="padding: 3px;">
                                    <uc1:ucTextBox ID="Qty" runat="server" ucCaption="項目數量" ucIsToggleVisibility="true" />
                                </div>
                                <div style="text-align: center; height: 50px;">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnDetailBatchUpdate" Text="整批更新" CssClass="Util_clsBtn"
                                        OnClick="btnDetailBatchUpdate_Click" />
                                    <asp:Button runat="server" ID="btnDetailBatchUpdateCancel" Text="取　　消" CssClass="Util_clsBtn"
                                        CausesValidation="false" OnClick="btnDetailBatchUpdateCancel_Click" />
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
                    </div>
                </asp:Panel>
            </div>
            <br />
            <br />
            <br />
            <br />
            <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
        </div>
    </form>
</body>
</html>
