<%@ Page Title="" Language="C#" MasterPageFile="~/FlowExpress/Admin/FlowExpess.master"
    AutoEventWireup="true" CodeFile="FlowStepBtn.aspx.cs" Inherits="FlowExpress_Admin_FlowStepBtn"
    MaintainScrollPositionOnPostback="true" %>

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
            <legend class="Util_Legend">FlowStepBtn (流程關卡按鈕)</legend>
            <%--FlowStepBtn表單--%>
            <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound">
                <EditItemTemplate>
                    <%--[編輯]用範本--%>
                    <table class="Util_Frame" style="width:900px;">
                        <tr class="Util_clsRow1">
                            <td style="width: 150px;">
                                流程代號：
                            </td>
                            <td style="width: 85%;">
                                <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="150" />
                                關卡代號：
                                <uc1:ucTextBox ID="txtFlowStepID" runat="server" ucWidth="150" ucMaxLength="20" ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>
                                按鈕代號：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepBtnID" runat="server" ucWidth="150" ucMaxLength="20"
                                    ucIsDispEnteredWords="true" />
                                順序：
                                <uc1:ucTextBox ID="txtFlowStepBtnSeqNo" runat="server" ucWidth="20" ucMaxLength="3" />
                                按鈕抬頭：
                                <uc1:ucTextBox ID="txtFlowStepBtnCaption" runat="server" ucWidth="150" ucMaxLength="30"
                                    ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>
                                按鈕說明：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepBtnDesc" runat="server" ucWidth="500" ucMaxLength="200"
                                    ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>
                                確認提示：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepBtnConfirmMsg" runat="server" ucWidth="500" ucMaxLength="100"
                                    ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>
                                指派模式：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFlowStepBtnIsMultiSelect" runat="server">
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkFlowStepBtnIsNeedOpinion" runat="server" />必需輸入意見
                                <asp:CheckBox ID="chkFlowStepBtnIsSendMail" runat="server" />審核後發信
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>
                                審核前子流程檢查：
                            </td>
                            <td>
                                <uc1:ucCheckBoxList ID="chkFlowStepBtnChkSubFlowList" runat="server" ucWidth="500" />**檢查是否已經結案**
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>
                                下一流程關卡：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFlowStepBtnNextStepID" runat="server">
                                </asp:DropDownList>
                                按鈕成立條件：
                                <asp:DropDownList ID="ddlFlowStepBtnNextStepDealCondition" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>
                                審核後新增子流程：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAddSubFlowInfo" runat="server">
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkFlowStepBtnIsAddMultiSubFlow" runat="server" />
                                當為多個指派對象時，允許新增多個子流程
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>
                                審核後資料回寫：
                            </td>
                            <td>
                                <uc1:ucCheckBoxList ID="chkFlowStepBtnUpdCustIDList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2" style="white-space: nowrap;">
                            <td>
                                [指派]參考關卡：<asp:CheckBox ID="chkFlowStepBtnRefStepSW" runat="server" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFlowStepBtnRefStepID" runat="server">
                                </asp:DropDownList>
                                **若案件曾經過該指定關卡，則取其最後一次的指派對象**
                            </td>
                        </tr>
                        <tr class="Util_clsRow1" style="white-space: nowrap; vertical-align: top;">
                            <td>
                                [指派]自訂類別：<asp:CheckBox ID="chkFlowStepBtnCustClassSW" runat="server" />
                            </td>
                            <td>
                                方法名稱
                                <uc1:ucTextBox ID="txtFlowStepBtnCustClassMethod" runat="server" ucWidth="500" ucMaxLength="100"
                                    ucIsDispEnteredWords="true" />
                                <div>**可支援命名空間，例：[ SinoPac.WebExpress.Work.OrgExpress.getDeptBoss ]**</div>
                                <br />
                                參數清單
                                <uc1:ucTextBox ID="txtFlowStepBtnCustParaList" runat="server" ucWidth="500" ucMaxLength="100"
                                    ucIsDispEnteredWords="true" />
                                <div>**參數可使用自訂變數及下方的系統變數**</div>
                                <li style="color: Gray;"><b>_AssignTo,_CompID,_DeptID,_OrganID,_UserID</b></li>
                                <li style="color: Gray;"><b>_FlowID,_FlowCaseID,_FlowLogID</b></li>
                                <li style="color: Gray;"><b><asp:Label ID="labCustClassFlowPara" runat="server" /></b></li>
                            </td>
                        </tr>
                        <tr class="Util_clsRow2" style="white-space: nowrap; vertical-align: top;">
                            <td>
                                [指派]自訂群組：<asp:CheckBox ID="chkFlowStepBtnCustGrpSW" runat="server" />
                            </td>
                            <td>
                                <uc1:ucCheckBoxList ID="chkFlowStepBtnCustGrpIDList" runat="server" ucWidth="400" />
                                <asp:CheckBox ID="chkFlowStepBtnCustGrpIsDetail" runat="server" />允許指派群組成員
                            </td>
                        </tr>
                        <tr class="Util_clsRow1" style="white-space: nowrap; vertical-align: top;">
                            <td>
                                [指派]自訂變數：<asp:CheckBox ID="chkFlowStepBtnCustVarSW" runat="server" />
                            </td>
                            <td>
                                變數清單
                                <uc1:ucTextBox ID="txtFlowStepBtnCustVarNameList" runat="server" ucWidth="500" ucMaxLength="100"
                                    ucIsDispEnteredWords="true" />
                                <br />
                                額外套用
                                <uc1:ucTextBox ID="txtFlowStepBtnCustVarClassMethod" runat="server" ucWidth="500"
                                    ucMaxLength="100" ucIsDispEnteredWords="true" />(自訂類別方法)<br />
                                **將 [變數清單] 當作參數套入該方法取值**
                            </td>
                        </tr>
                        <tr class="Util_clsRow2" style="white-space: nowrap; vertical-align: top;">
                            <td>
                                [指派]自訂SQL：<asp:CheckBox ID="chkFlowStepBtnCustQrySW" runat="server" />
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowStepBtnCustQrySQL" runat="server" ucWidth="500" ucRows="5"
                                    ucMaxLength="500" ucIsDispEnteredWords="true" /><br />
                                **從 [表單DB] 執行 SQL 取值**
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>
                                備 註：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="500" ucRows="3" ucMaxLength="100"
                                    ucIsDispEnteredWords="true" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>
                                最近更新：
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
            <legend class="Util_Legend">FlowStepBtnHideExp (流程關卡按鈕隱藏條件)</legend>
            <uc1:ucGridView ID="gvFlowStepBtnHideExp" runat="server" />
        </fieldset>
    </asp:Panel>
    <%--固定隱藏區--%>
    <div id="DivHiddenArea" style="display: none;">
        <asp:Panel runat="server" ID="pnlFlowStepBtnHideExp" CssClass="Util_Frame">
            <asp:HiddenField ID="hidMode" runat="server" />
            <asp:HiddenField ID="hidFlowID" runat="server" />
            <asp:HiddenField ID="hidFlowStepID" runat="server" />
            <asp:HiddenField ID="hidFlowStepBtnID" runat="server" />
            <table style="border: 0px none #FFFFFF; width: 100%;" cellspacing="0">
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap; width: 80px;">
                        群組：
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <uc1:ucTextBox ID="txtFlowStepBtnChkGrpNo" runat="server" ucIsRequire="true" ucWidth="20"
                            ucMaxLength="3" />
                        順序：
                        <uc1:ucTextBox ID="txtFlowStepBtnChkSeqNo" runat="server" ucIsRequire="true" ucWidth="20"
                            ucMaxLength="3" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="text-align: right; white-space: nowrap; vertical-align: top;">
                        檢查Sess變數：<asp:CheckBox ID="chkFlowStepBtnChkSessSW" runat="server" />
                    </td>
                    <td style="vertical-align:top; text-align: center; width: 50px;">
                        Name<br />
                        Exp<br />
                        Value<br />
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtFlowStepBtnChkSessNameObjectProperty" runat="server" ucWidth="320"
                            ucMaxLength="50" ucIsDispEnteredWords="true" />
                        <br />
                        <asp:DropDownList ID="ddlFlowStepBtnChkSessExp" runat="server" DataValueField="key"
                            DataTextField="value">
                        </asp:DropDownList>
                        <br />
                        <uc1:ucTextBox ID="txtFlowStepBtnChkSessValue" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                        <br />
                        ** Name 支援 [名稱] / [物件.屬性名稱] 兩種表示用法**
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap; vertical-align: top;">
                        檢查自訂變數：<asp:CheckBox ID="chkFlowStepBtnChkCustVarSW" runat="server" />
                    </td>
                    <td style="vertical-align:top; text-align: center; width: 50px;">
                        Name<br />
                        Exp<br />
                        Value<br />
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtFlowStepBtnChkCustVarName" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                        <br />
                        <asp:DropDownList ID="ddlFlowStepBtnChkCustVarExp" runat="server" DataValueField="key"
                            DataTextField="value">
                        </asp:DropDownList>
                        <br />
                        <uc1:ucTextBox ID="txtFlowStepBtnChkCustVarValue" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="text-align: right; white-space: nowrap; vertical-align: top;">
                        檢查資料表：<asp:CheckBox ID="chkFlowStepBtnChkCustTableSW" runat="server" />
                    </td>
                    <td style="text-align: center; width: 50px;">
                        Table<br />
                        Field<br />
                        Exp<br />
                        Value<br />
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtFlowStepBtnChkCustTable" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                        <br />
                        <uc1:ucTextBox ID="txtFlowStepBtnChkCustField" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                        <br />
                        <asp:DropDownList ID="ddlFlowStepBtnChkCustExp" runat="server" DataValueField="key"
                            DataTextField="value">
                        </asp:DropDownList>
                        <br />
                        <uc1:ucTextBox ID="txtFlowStepBtnChkCustValue" runat="server" ucWidth="320" ucMaxLength="50"
                            ucIsDispEnteredWords="true" />
                        <br />
                         ** Value 可使用以下的變數： _Empty , _Null **
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap;">
                        備 註：
                    </td>
                    <td></td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="360" ucRows="3" ucIsDispEnteredWords="true"
                            ucMaxLength="100" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <hr class="Util_HR" />
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
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupWidth="450" ucPopupHeight="150"
        ucBtnCompleteEnabled="false" ucBtnCancelEnabled="false" />
</asp:Content>
