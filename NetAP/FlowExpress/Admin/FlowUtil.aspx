<%@ Page Title="" Language="C#" MasterPageFile="~/FlowExpress/Admin/FlowExpess.master"
    AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="FlowUtil.aspx.cs"
    Inherits="FlowExpress_Admin_FlowUtil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Button ID="btnClearCache" runat="server" Text="清除流程快取" OnClick="btnClearCache_Click" />
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>OrgExpress 測試</legend>
            Method
            <uc1:ucCommSingleSelect ID="OrgExpressTest_MethodList" runat="server" ucIsSearchEnabled="false" ucDropDownSourceListWidth="700" />
            <br />
            Parameter 
            <asp:TextBox ID="OrgExpressTest_ParaList" runat="server" Width="350"></asp:TextBox>
            <asp:Button ID="btnOrgExpressTest" runat="server" Text="Go"
                OnClick="btnOrgExpressTest_Click" />
            <br />
            <asp:Label ID="labOrgExpressTest" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>getFlowCaseValues() 取得流程案件欄位值</legend>FlowID
            <uc1:ucCommSingleSelect ID="getFlowCaseValues_FlowID" runat="server" ucIsSearchEnabled="false" />
            <br />
            FlowCaseID<asp:TextBox ID="getFlowCaseValues_FlowCaseID" runat="server"></asp:TextBox><br />
            Field
            <asp:DropDownList ID="getFlowCaseValues_Field" runat="server" />
            可不指定(FlowCaseHisTag)<br />
            <asp:Button ID="btnGetFlowCaseValues" runat="server" Text="Go" OnClick="btnGetFlowCaseValues_Click" />
            <br />
            <asp:Label ID="labGetFlowCaseValues" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>getFlowLogValues() 取得流程Log 欄位值</legend>FlowID
            <uc1:ucCommSingleSelect ID="GetFlowLogValues_FlowID" runat="server" ucIsSearchEnabled="false" />
            <br />
            FlowLogID<asp:TextBox ID="GetFlowLogValues_FlowLogID" runat="server"></asp:TextBox><br />
            SpecStepID<asp:TextBox ID="GetFlowLogValues_SpecStepID" runat="server"></asp:TextBox><br />
            Field
            <asp:DropDownList ID="GetFlowLogValues_Field" runat="server" />
            Scope
            <asp:DropDownList ID="GetFlowLogValues_Scope" runat="server" />
            IsAutoDetectCurrStepID
            <asp:DropDownList ID="GetFlowLogValues_AutoDetectStepID" runat="server">
                <asp:ListItem Value="Y">是</asp:ListItem>
                <asp:ListItem Selected="True" Value="N">否</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnGetFlowLogValues" runat="server" Text="Go" OnClick="btnGetFlowLogValues_Click" />
            <br />
            <asp:Label ID="labGetFlowLogValues" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsFlowRollBack() 流程退一關</legend>FlowID
            <uc1:ucCommSingleSelect ID="FlowRollBack_FlowID" runat="server" ucIsSearchEnabled="false" />
            </asp:TextBox>FlowCaseID<asp:TextBox ID="FlowRollBack_FlowCaseID" runat="server"></asp:TextBox>
            <asp:CheckBox ID="FlowRollBack_IsRun" runat="server" Text="IsRun" />
            <asp:Button ID="btnFlowRollBack" runat="server" Text="Go" OnClick="btnFlowRollBack_Click" />
            <br />
            <asp:Label ID="labFlowRollBack" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsFlowCaseValueChanged() 變更流程案件的值</legend>FlowID
            <uc1:ucCommSingleSelect ID="FlowCaseValueChanged_FlowID" runat="server" ucIsSearchEnabled="false" />
            FlowCaseID<asp:TextBox ID="FlowCaseValueChanged_FlowCaseID" runat="server"></asp:TextBox>
            <br />
            ChangeKind
            <asp:DropDownList ID="FlowCaseValueChanged_ChangeKind" runat="server" />
            NewValues
            <asp:TextBox ID="FlowCaseValueChanged_NewValues" runat="server"></asp:TextBox>
            ChkFields
            <asp:TextBox ID="FlowCaseValueChanged_ChkFields" runat="server"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="Go" OnClick="FlowCaseValueChanged_Click" />
            <br />
            <asp:Label ID="labFlowCaseValueChanged" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsFlowDeleted() 流程刪除(含子流程)</legend>FlowID
            <uc1:ucCommSingleSelect ID="FlowDeleted_FlowID" runat="server" ucIsSearchEnabled="false" />
            FlowCaseID<asp:TextBox ID="FlowDeleted_FlowCaseID" runat="server"></asp:TextBox>
            <asp:CheckBox ID="FlowDeleted_IsRun" runat="server" Text="IsRun" />
            <asp:Button ID="Button1" runat="server" Text="Go" OnClick="btnFlowDeleted_Click" />
            <br />
            <asp:Label ID="labFlowDeleted" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>流程 附件調整</legend>FlowAttachDB
            <uc1:ucCommSingleSelect ID="AdjFlowAttachDB" runat="server" ucIsSearchEnabled="false" />
            AttachID<asp:TextBox ID="txtAttachID" runat="server" Width="280px"></asp:TextBox>
            <asp:Button ID="btnAttach" runat="server" Text="Go" OnClick="btnAttach_Click" />
            <br />
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>流程Log刪除(含FlowFullLog 及 FlowOpenLog)</legend>FlowID
            <uc1:ucCommSingleSelect ID="DelFlowID" runat="server" ucIsSearchEnabled="false" />
            FlowLogID<asp:TextBox ID="txtDelFlowLogID" runat="server" Width="280px"></asp:TextBox>
            <asp:Button ID="btnDelFlowLog" runat="server" Text="刪除" OnClientClick="return confirm('此作業無法還原，確定刪除Log？');"
                OnClick="btnDelFlowLog_Click" />
            <br />
            <asp:Label ID="labDelLog" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>流程OpenLog修正(刪除已經Close但還殘留的OpenLog)</legend>FlowID
            <uc1:ucCommSingleSelect ID="FixFlowID" runat="server" ucIsSearchEnabled="false" />
            <asp:Button ID="btnFixOpenLog" runat="server" Text="修正" OnClick="btnFixOpenLog_Click" />
            <br />
            <asp:Label ID="labFixOpenLog" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>調閱 FlowTraceLog</legend>FlowID
            <uc1:ucCommSingleSelect ID="TraceFlowID" runat="server" ucIsSearchEnabled="false" />
            FlowCaseID<asp:TextBox ID="TraceFlowCaseID" runat="server"></asp:TextBox>
            <asp:DropDownList ID="TraceSort" runat="server">
                <asp:ListItem Value="Asc">遞增</asp:ListItem>
                <asp:ListItem Value="Desc">遞減</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnTraceLog" runat="server" Text="Go" OnClick="btnTraceLog_Click" />
            <asp:Button ID="btnTraceLogExport" runat="server" Text="Export" Visible="False" OnClick="btnTraceLogExport_Click" />
            <asp:GridView ID="gvTraceLog" runat="server" Visible="False" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>調閱 FullLog</legend>FlowID
            <uc1:ucCommSingleSelect ID="FullLogFlowID" runat="server" ucIsSearchEnabled="false" />
            FlowCaseID<asp:TextBox ID="FullLogFlowCaseID" runat="server"></asp:TextBox>
            <asp:DropDownList ID="FullLogSort" runat="server">
                <asp:ListItem Value="Asc">遞增</asp:ListItem>
                <asp:ListItem Value="Desc">遞減</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnFullLog" runat="server" Text="Go" OnClick="btnFullLog_Click" />
            <asp:Button ID="btnFullLogExport" runat="server" Text="Export" Visible="False" OnClick="btnFullLogExport_Click" />
            <asp:GridView ID="gvFullLog" runat="server" Visible="False" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsFlowReAssign() 流程改派</legend>
            FlowID<uc1:ucCommSingleSelect ID="IsFlowReAssign_FlowID" runat="server" ucIsSearchEnabled="false" />
            <br />
            FlowLogID<asp:TextBox ID="IsFlowReAssign_FlowLogID" runat="server"></asp:TextBox>
            <br />
            ReAssignTo (可為 JSON 格式，例: {"GP01":"審核小組"} )
            <asp:TextBox ID="IsFlowReAssign_ReAssignTo" runat="server" Width="300px"></asp:TextBox>
            <br />
            Opinion
            <asp:TextBox ID="IsFlowReAssign_Opinion" runat="server" Width="300px"></asp:TextBox>
            <asp:CheckBox ID="IsFlowReAssign_IsRun" runat="server" Text="IsRun" />
            <asp:Button ID="Button3" runat="server" Text="Go" OnClick="IsFlowReAssign_Click" />
            <br />
            <asp:Label ID="labIsFlowReAssign" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>流程[系統]審核結案</legend>
            FlowID<uc1:ucCommSingleSelect ID="FlowSysBtnVerify_FlowID" runat="server" ucIsSearchEnabled="false" />
            <br />
            FlowCaseID<asp:TextBox ID="FlowSysBtnVerify_FlowCaseID" runat="server"></asp:TextBox><br />
            FlowSysStepBtnID<uc1:ucCommSingleSelect ID="FlowSysBtnVerify_FlowSysStepBtnID" runat="server" ucIsSearchEnabled="false" />
            <br />
            FlowCloseStepID<uc1:ucCommSingleSelect ID="FlowSysBtnVerify_FlowCloseStepID" runat="server" ucIsSearchEnabled="false" />
            <br />
            Opinion<asp:TextBox ID="FlowSysBtnVerify_Opinion" runat="server" Width="300px"></asp:TextBox>
            <asp:Button ID="btnFlowSysBtnVerify" runat="server" Text="Go" OnClick="btnFlowSysBtnVerify_Click" />
            <br />
            <asp:Label ID="labFlowSysBtnVerify" runat="server" Text="" ForeColor="#009933"></asp:Label>
        </fieldset>
    </div>
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
</asp:Content>
