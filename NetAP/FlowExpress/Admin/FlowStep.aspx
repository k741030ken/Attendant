<%@ Page Title="" Language="C#" MasterPageFile="~/FlowExpress/Admin/FlowExpess.master"
    AutoEventWireup="true" CodeFile="FlowStep.aspx.cs" Inherits="FlowExpress_Admin_FlowStep" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagName="ucCheckBoxList" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucMuiAdminButton.ascx" TagName="ucMuiAdminButton" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="labMsg" runat="server" Visible="false" Text=""></asp:Label>
    <asp:Panel ID="pnlFlowStep" runat="server" Visible="true">
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowStep (流程關卡)</legend>
            <%--FlowStep表單--%>
            <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound">
                <EditItemTemplate>
                    <%--[編輯]用範本--%>
                    <table class="Util_Frame">
                        <tr class="Util_clsRow1">
                            <td style="width: 180px;">流程代號
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="150" ucIsReadOnly="true" />
                                流程名稱
                                <uc1:ucTextBox ID="txtFlowName" runat="server" ucWidth="180" ucIsReadOnly="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>關卡代號
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepID" runat="server" ucWidth="150" ucMaxLength="20" ucIsDispEnteredWords="true" ucIsReadOnly="true" />
                                關卡名稱
                                <uc1:ucTextBox ID="txtFlowStepName" runat="server" ucWidth="180" ucMaxLength="30"
                                    ucIsDispEnteredWords="true" />
                                <asp:CheckBox ID="chkFlowStepBatchEnabled" runat="server" />
                                允許批次審核
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td style="vertical-align: top;">關卡附件
                            </td>
                            <td style="padding-left: 5px;">最大數量<uc1:ucTextBox ID="txtFlowStepAttachMaxQty" runat="server" ucWidth="50" ucIsRequire="true" ucMaxLength="3" />
                                若為[0]代表不允許上傳
                                <br />
                                單檔限制<uc1:ucTextBox ID="txtFlowStepAttachMaxKB" runat="server" ucWidth="50" ucIsRequire="true" ucMaxLength="9" />
                                KB，若為[0]則預設為 1024 KB
                                <br />
                                總量限制<uc1:ucTextBox ID="txtFlowStepAttachTotKB" runat="server" ucWidth="50" ucIsRequire="true" ucMaxLength="9" />
                                KB，若為[0]則由系統自動計算
                                <br />
                                附件格式<uc1:ucTextBox ID="txtFlowStepAttachExtList" runat="server" ucWidth="200" ucMaxLength="50" ucIsDispEnteredWords="true" />
                                [空白]代表不限制，可設定多種格式，例：[<b>ZIP,PDF,DOC</b>]
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>自訂審核URL
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepCustVerifyURL" runat="server" ucWidth="500" ucMaxLength="50" ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td style="vertical-align: top;">審核後郵件發送清單
                            </td>
                            <td style="white-space: nowrap;padding-left:5px;">
                                <uc1:ucCheckBoxList ID="chkFlowStepMailToList" runat="server" ucCaption="參考關卡" ucCaptionWidth="84" ucWidth="350" />
                                <br />
                                <uc1:ucTextBox ID="txtFlowStepMailToList" runat="server" ucWidth="220" ucCaption="自訂員編" ucCaptionWidth="84" ucMaxLength="30" ucIsDispEnteredWords="true" />
                                <br />
                                ** 發信給該參考關卡最後一次的實際處理人員 (ToUser) **
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td style="vertical-align: top;">備　　註
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="500" ucRows="3" ucMaxLength="100" ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>最近更新
                            </td>
                            <td>
                                <asp:Label ID="labUpdInfo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <hr class="Util_clsHR" />
                                <asp:Button runat="server" ID="btnMainUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                    OnClick="btnMainUpdate_Click" />
                                <uc1:ucMuiAdminButton ID="ucMuiAdminButton1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </EditItemTemplate>
            </asp:FormView>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowStepBtn (流程關卡按鈕)</legend>
            <uc1:ucGridView ID="gvFlowStepBtn" runat="server" />
        </fieldset>
    </asp:Panel>
    <%--固定隱藏區--%>
    <div id="DivHiddenArea" style="display: none;">
        <asp:Panel runat="server" ID="pnlNewFlowStepBtn" CssClass="Util_Frame">
            <asp:HiddenField ID="hidFlowID" runat="server" />
            <asp:HiddenField ID="hidFlowStepID" runat="server" />
            <asp:HiddenField ID="hidFlowStepBtnID" runat="server" />
            <table style="border: 0px none #FFFFFF;" cellspacing="0" width="100%">
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap; width: 80px;">按鈕代號
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowStepBtnID" runat="server" ucIsRequire="true" ucWidth="180" ucMaxLength="20" />
                        順序
                        <uc1:ucTextBox ID="txtNewFlowStepBtnSeqNo" runat="server" ucIsRequire="true" ucWidth="25" ucMaxLength="3" ucTextData="99" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="text-align: right; white-space: nowrap;">按鈕抬頭
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowStepBtnCaption" runat="server" ucIsRequire="true" ucWidth="262" ucMaxLength="30" ucIsDispEnteredWords="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <br />
                        <asp:Button ID="btnComplete" runat="server" Text="確　定" CssClass="Util_clsBtn" OnClick="btnComplete_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <br />
    <br />
    <br />
    <br />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupWidth="450" ucPopupHeight="180"
        ucBtnCompleteEnabled="false" ucBtnCancelEnabled="false" />
</asp:Content>
