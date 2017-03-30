<%@ Page Title="" Language="C#" MasterPageFile="~/FlowExpress/Admin/FlowExpess.master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="FlowSpec.aspx.cs"
    Inherits="FlowExpress_Admin_FlowSpec" %>

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
    <asp:Panel ID="pnlFlowSpec" runat="server" Visible="true">
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowSpec (流程規格)</legend>
            <%--FlowSpec表單--%>
            <asp:FormView ID="fmMain" runat="server" OnDataBound="fmMain_DataBound">
                <EditItemTemplate>
                    <%--[編輯]用範本--%>
                    <table class="Util_Frame">
                        <tr class="Util_clsRow1">
                            <td style="width: 150px;">流程代號：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="150" />
                                流程名稱：
                                <uc1:ucTextBox ID="txtFlowName" runat="server" ucWidth="150" />
                                <asp:CheckBox ID="chkFlowTraceEnabled" runat="server" />
                                啟用審核追蹤
                                <asp:CheckBox ID="chkFlowBatchEnabled" runat="server" />
                                啟用批次審核
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>鍵值欄位清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowKeyFieldList" runat="server" ucWidth="200" />
                                **若最後一個欄位為 _AutoNo，則由系統自動代編流水號**
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>鍵值抬頭清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowKeyCaptionList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>顯示欄位清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowShowFieldList" runat="server" ucWidth="500" />
                                <div>**可使用以下的系統變數**</div>
                                <li style="color: Gray;"><b>_AssignTo,_ProxyType,_FlowID,_FlowCaseID,_FlowLogID,_FlowKey</b></li>
                                <li style="color: Gray;"><b>_LogDate,_LogDateTime,_CurrStepID,_CurrStepName</b></li>
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>顯示抬頭清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowShowCaptionList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>表單來源DB：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowCustDB" runat="server" />
                                流程Log來源DB：
                                <uc1:ucTextBox ID="txtFlowLogDB" runat="server" />
                                流程附件來源DB：
                                <uc1:ucTextBox ID="txtFlowAttachDB" runat="server" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>自訂表單URL：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowVerifyCustFormURL" runat="server" ucWidth="220" />
                                意見最多字元：
                                <uc1:ucTextBox ID="txtFlowOpinionMaxLength" runat="server" ucWidth="40" />
                                審核彈出視窗寬度：
                                <uc1:ucTextBox ID="txtFlowVerifyPopupWidth" runat="server" ucWidth="40" />
                                px&nbsp;&nbsp;高度：
                                <uc1:ucTextBox ID="txtFlowVerifyPopupHeight" runat="server" ucWidth="40" />
                                px
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>自訂變數名稱清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowCustVarNameList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>自訂變數預設值清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowDefCustVarValueList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>需隱藏意見的關卡清單：
                            </td>
                            <td>
                                <uc1:ucCheckBoxList ID="chkFlowHideOpinionStepList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>隱藏關卡例外成員清單：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowHideOpinionIgnoreList" runat="server" ucWidth="500" />
                                <br />
                                **可填入DeptID,UserID...等UserInfo的屬性，ex: [L01000,100479]**
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>郵件變數清單：
                            </td>
                            <td>
                                <uc1:ucCheckBoxList ID="chkFlowSendMailNewValueList" runat="server" ucWidth="500" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>郵件主旨格式：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowSendMailSubjectFormat" runat="server" ucWidth="500" />
                                <br />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>郵件本文格式：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtFlowSendMailBodyFormat" runat="server" ucWidth="500" ucRows="5" />
                                <br />
                            </td>
                        </tr>
                        <tr class="Util_clsRow1">
                            <td>備 註：
                            </td>
                            <td>
                                <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="500" ucRows="5" />
                            </td>
                        </tr>
                        <tr class="Util_clsRow2">
                            <td>最近更新：
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
            <legend class="Util_Legend">流程輔助工具</legend>
            <div style="padding-left: 20px;">
                <asp:Button runat="server" ID="btnCustProperty" Text="自訂屬性" CssClass="Util_clsBtn" Width="150px"
                    OnClick="btnCustProperty_Click" />
                <asp:Button runat="server" ID="btnFlowSQL" Text="LogDB Script" CssClass="Util_clsBtn" Width="150px"
                    OnClick="btnFlowSQL_Click" />
            </div>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowStep (流程關卡)</legend>
            <uc1:ucGridView ID="gvFlowStep" runat="server" />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowUpdCustDataExp (資料回寫)</legend>
            <uc1:ucGridView ID="gvFlowUpdCustDataExp" runat="server" />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowCustGrp (自訂群組)</legend>
            <uc1:ucGridView ID="gvFlowCustGrp" runat="server" />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">FlowCustGrpDetail (自訂群組成員)</legend>
            <uc1:ucGridView ID="gvFlowCustGrpDetail" runat="server" />
        </fieldset>
    </asp:Panel>
    <div id="DivHiddenArea" style="display: none;">
        <%--CustDataExp單筆編輯表單--%>
        <asp:Panel runat="server" ID="pnlCustDataExp" CssClass="Util_Frame">
            <div style="text-align: center; padding: 5px;">
                <asp:FormView ID="fmCustDataExp" runat="server" OnDataBound="fmCustDataExp_DataBound">
                    <InsertItemTemplate>
                        <%--[新增]用範本--%>
                        <table style="border: 0px none #FFFFFF; width: 820px;" cellspacing="0">
                            <tr class="Util_clsRow1" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap; width: 100px;">流程代號：
                                </td>
                                <td style="width: 20px;"></td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" ucIsRequire="true" />
                                    組別代號：
                                    <uc1:ucTextBox ID="txtFlowUpdCustID" runat="server" ucWidth="100" ucMaxLength="20"
                                        ucIsRequire="true" ucIsDispEnteredWords="true" />
                                    <uc1:ucTextBox ID="txtFlowUpdCustName" runat="server" ucWidth="250" ucMaxLength="50"
                                        ucIsRequire="true" ucIsDispEnteredWords="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">自訂變數：
                                </td>
                                <td style="width: 20px;">
                                    <asp:CheckBox ID="chkFlowUpdCustVarSW" runat="server" />
                                </td>
                                <td style="text-align: left;">FlowUpdCustVarNameList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustVarNameList" runat="server" ucWidth="500" ucMaxLength="200"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從流程：<b><asp:Label ID="labFlowCustVarNameList" runat="server"></asp:Label></b></li>
                                    <hr class="Util_clsHR" />
                                    FlowUpdCustVarNewValueList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustVarNewValueList" runat="server" ucWidth="500" ucMaxLength="200"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <br />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">自訂資料表：
                                </td>
                                <td style="width: 20px;">
                                    <asp:CheckBox ID="chkFlowUpdCustTableSW" runat="server" />
                                </td>
                                <td style="text-align: left;">FlowUpdCustTableName<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableName" runat="server" ucWidth="500" ucMaxLength="50"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">位於[ <b>
                                        <asp:Label ID="labFlowCustDB" runat="server"></asp:Label></b> ]資料庫內的任一資料表</li>
                                    <hr class="Util_clsHR" />
                                    FlowUpdCustTableKeyList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableKeyList" runat="server" ucWidth="500" ucMaxLength="50"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從表單：<b><asp:Label ID="labFlowKeyFieldList1" runat="server"></asp:Label></b></li>
                                    <li style="color: Gray;">從流程：<b>_FlowID,_FlowCaseID,_FlowLogID</b></li>
                                    <li style="color: Gray;">固定值：ex: <b>Fld01=AA,Fld02=BB ...</b></li>
                                    <hr class="Util_clsHR" />
                                    FlowUpdCustTableFieldList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableFieldList" runat="server" ucWidth="500" ucMaxLength="100"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">[ <b>FlowUpdCustTableName</b> ]定義資料表內的任意欄位清單</li>
                                    <hr class="Util_clsHR" />
                                    FlowUpdCustTableNewValueList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableNewValueList" runat="server" ucWidth="500"
                                        ucMaxLength="100" ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從表單：<b><asp:Label ID="labFlowKeyFieldList2" runat="server"></asp:Label></b></li>
                                    <li style="color: Gray;">從流程：<b>_FlowID,_FlowCaseID,_FlowLogID,_CurrStepID,_CurrStepName,_CurrDate,_CurrDateTime</b></li>
                                    <li style="color: Gray;">運算式：以 [ __ ] 開頭的內容，ex: [<b> __Fld01 = Fld01 + 5 </b>]</li>
                                    <li style="color: Gray;">固定值：上述以外的內容，則視為固定內容，ex: <b>Open , Close , Y , N ...</b></li>
                                </td>
                            </tr>
                            <tr class="Util_clsRow2" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td></td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="500" ucMaxLength="100" ucIsDispEnteredWords="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnCustDataExpAdd" Text="新　　增" CssClass="Util_clsBtn"
                                        OnClick="btnCustDataExpAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <%--[編輯]用範本--%>
                        <table style="border: 0px none #FFFFFF; width: 820px;" cellspacing="0">
                            <tr class="Util_clsRow1" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap; width: 100px;">流程代號：
                                </td>
                                <td style="width: 20px;"></td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" ucIsRequire="true"
                                        ucIsDispEnteredWords="true" />
                                    組別代號：
                                    <uc1:ucTextBox ID="txtFlowUpdCustID" runat="server" ucWidth="100" ucMaxLength="20"
                                        ucIsRequire="true" ucIsDispEnteredWords="true" />
                                    <uc1:ucTextBox ID="txtFlowUpdCustName" runat="server" ucWidth="250" ucMaxLength="50"
                                        ucIsRequire="true" ucIsDispEnteredWords="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">自訂變數：
                                </td>
                                <td style="width: 20px;">
                                    <asp:CheckBox ID="chkFlowUpdCustVarSW" runat="server" />
                                </td>
                                <td style="text-align: left;">FlowUpdCustVarNameList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustVarNameList" runat="server" ucWidth="500" ucMaxLength="200"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從流程：<b><asp:Label ID="labFlowCustVarNameList" runat="server"></asp:Label></b></li>
                                    FlowUpdCustVarNewValueList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustVarNewValueList" runat="server" ucWidth="500" ucMaxLength="200"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <br />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">自訂資料表：
                                </td>
                                <td style="width: 20px;">
                                    <asp:CheckBox ID="chkFlowUpdCustTableSW" runat="server" />
                                </td>
                                <td style="text-align: left;">FlowUpdCustTableName<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableName" runat="server" ucWidth="500" ucMaxLength="50"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">位於[ <b>
                                        <asp:Label ID="labFlowCustDB" runat="server"></asp:Label></b> ]資料庫內的任一資料表</li>
                                    FlowUpdCustTableKeyList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableKeyList" runat="server" ucWidth="500" ucMaxLength="50"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從表單：<b><asp:Label ID="labFlowKeyFieldList1" runat="server"></asp:Label></b></li>
                                    <li style="color: Gray;">從流程：<b>_FlowID,_FlowCaseID,_FlowLogID</b></li>
                                    <li style="color: Gray;">固定值：ex: <b>Fld01=AA,Fld02=BB ...</b></li>
                                    FlowUpdCustTableFieldList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableFieldList" runat="server" ucWidth="500" ucMaxLength="100"
                                        ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">[ <b>FlowUpdCustTableName</b> ]定義資料表內的任意欄位清單</li>
                                    FlowUpdCustTableNewValueList<br />
                                    <uc1:ucTextBox ID="txtFlowUpdCustTableNewValueList" runat="server" ucWidth="500"
                                        ucMaxLength="100" ucIsRequire="false" ucIsDispEnteredWords="true" />
                                    <li style="color: Gray;">從表單：<b><asp:Label ID="labFlowKeyFieldList2" runat="server"></asp:Label></b></li>
                                    <li style="color: Gray; padding-left: 1em; text-indent: -1em;">從流程：<b>_FlowID,_FlowCaseID,_FlowLogID,_CurrDate,_CurrDateTime<br />
                                        ,_CurrStepID,_CurrStepName,_CurrLogStepID,_CurrLogStepName</b></li>
                                    <li style="color: Gray;">運算式：以 [ __ ] 開頭的內容，ex: [<b> __Fld01 = Fld01 + 5 </b>]</li>
                                    <li style="color: Gray;">固定值：上述以外的內容，則視為固定內容，ex: <b>Open , Close , Y , N ...</b></li>
                                </td>
                            </tr>
                            <tr class="Util_clsRow2" style="vertical-align: top;">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td></td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="500" ucMaxLength="100" ucIsDispEnteredWords="true" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnCustDataExpUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                        OnClick="btnCustDataExpUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </asp:Panel>
        <%--Grp單筆編輯表單--%>
        <asp:Panel runat="server" ID="pnlGrp" CssClass="Util_Frame">
            <div style="text-align: center; padding: 5px;">
                <asp:FormView ID="fmGrp" runat="server" OnDataBound="fmGrp_DataBound">
                    <InsertItemTemplate>
                        <%--[新增]用範本--%>
                        <table style="border: 0px none #FFFFFF;" cellspacing="0">
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">流程代號：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">群組代號：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtFlowCustGrpID" runat="server" ucWidth="100" ucMaxLength="30"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">群組名稱：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowCustGrpName" runat="server" ucWidth="150" ucMaxLength="50"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="150" ucMaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnGrpAdd" Text="新　　增" CssClass="Util_clsBtn" OnClick="btnGrpAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <%--[編輯]用範本--%>
                        <table style="border: 0px none #FFFFFF;" cellspacing="0">
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">流程代號：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">群組代號：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtFlowCustGrpID" runat="server" ucWidth="100" ucMaxLength="30"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">群組名稱：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowCustGrpName" runat="server" ucWidth="150" ucMaxLength="50"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="150" ucMaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnGrpUpdate" Text="更　　新" CssClass="Util_clsBtn" OnClick="btnGrpUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </asp:Panel>
        <%--GrpDetail單筆編輯表單--%>
        <asp:Panel runat="server" ID="pnlGrpDetail" CssClass="Util_Frame">
            <div style="text-align: center; padding: 5px;">
                <asp:FormView ID="fmGrpDetail" runat="server" OnDataBound="fmGrpDetail_DataBound">
                    <InsertItemTemplate>
                        <%--[新增]用範本--%>
                        <table style="border: 0px none #FFFFFF;" cellspacing="0">
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">流程代號：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">群組代號：
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlFlowCustGrpID" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">項 目 值：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtValue" runat="server" ucWidth="150" ucMaxLength="50" ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">項目內容：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtDescription" runat="server" ucWidth="150" ucMaxLength="50"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="150" ucMaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnGrpDetailAdd" Text="新　　增" CssClass="Util_clsBtn"
                                        Height="21px" OnClick="btnGrpDetailAdd_Click" />
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <%--[編輯]用範本--%>
                        <table style="border: 0px none #FFFFFF;" cellspacing="0">
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">流程代號：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtFlowID" runat="server" ucWidth="100" ucMaxLength="20" ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">群組代號：
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlFlowCustGrpID" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">項 目 值：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtValue" runat="server" ucWidth="150" ucMaxLength="50" ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow2">
                                <td style="text-align: right; white-space: nowrap;">項目內容：
                                </td>
                                <td style="text-align: left; white-space: nowrap;">
                                    <uc1:ucTextBox ID="txtDescription" runat="server" ucWidth="150" ucMaxLength="50"
                                        ucIsRequire="true" />
                                </td>
                            </tr>
                            <tr class="Util_clsRow1">
                                <td style="text-align: right; white-space: nowrap;">備 註：
                                </td>
                                <td style="text-align: left;">
                                    <uc1:ucTextBox ID="txtRemark" runat="server" ucWidth="150" ucMaxLength="100" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <hr class="Util_clsHR" />
                                    <asp:Button runat="server" ID="btnGrpDetailUpdate" Text="更　　新" CssClass="Util_clsBtn"
                                        OnClick="btnGrpDetailUpdate_Click" />
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </asp:FormView>
            </div>
        </asp:Panel>
        <%--Step 表單--%>
        <asp:Panel runat="server" ID="pnlNewFlowStep" CssClass="Util_Frame">
            <asp:HiddenField ID="hidFlowID" runat="server" />
            <asp:HiddenField ID="hidFlowStepID" runat="server" />
            <table style="border: 0px none #FFFFFF;" cellspacing="0" width="100%">
                <tr class="Util_clsRow2">
                    <td style="text-align: right; white-space: nowrap; width: 80px;">關卡代號：
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowStepID" runat="server" ucIsRequire="true" ucWidth="250"
                            ucMaxLength="20" ucIsDispEnteredWords="true" />
                    </td>
                </tr>
                <tr class="Util_clsRow1">
                    <td style="text-align: right; white-space: nowrap;">關卡名稱：
                    </td>
                    <td style="text-align: left;">
                        <uc1:ucTextBox ID="txtNewFlowStepName" runat="server" ucIsRequire="true" ucWidth="250"
                            ucMaxLength="30" ucIsDispEnteredWords="true" />
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
        <%-- Flow SQL Templete --%>
        <asp:Panel runat="server" ID="pnlFlowSQL" HorizontalAlign="Center">
            <uc1:ucTextBox ID="txtFlowSQL" runat="server" ucWidth="750" ucRows="35" ucIsReadOnly="true" />
        </asp:Panel>
    </div>
    <%--DivHiddenArea--%>
    <br />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucBtnCompleteEnabled="false"
        ucBtnCancelEnabled="false" />
</asp:Content>
