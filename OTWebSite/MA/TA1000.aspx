<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TA1000.aspx.vb" Inherits="TA_TA1000" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />

    <%--↓用模糊搜尋UC時一定要加這三行↓--%>
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <%--↑用模糊搜尋UC時一定要加這三行↑--%>
</head>
<body>
    <form id="frmContent" runat="server">
        <fieldset style="width: 400px;">
            <legend>UC-模糊搜尋</legend>
            部門：<uc:SelectTextBox ID="txtDeptID" runat="server" Width="200" />
            <asp:Label ID="lblDeptID" runat="server" Text=""></asp:Label>
            <br />
            科/組/課：<uc:SelectTextBox ID="txtOrganID" runat="server" Width="200" />
            <asp:Label ID="lblOrganID" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset style="width: 400px;">
            <legend>UC-簽核比對單位</legend>
            簽核比對單位：<asp:DropDownList ID="ddlFlowOrganID" Width="200" runat="server"></asp:DropDownList>
            <uc:ButtonFlowOrgan ID="ucFlowOrgan" runat="server" />
        <asp:HiddenField ID="hldFlowOrgan" runat="server" />
        </fieldset>
        <br />
        <br />
        <fieldset style="width: 400px;">
            <legend>UC-單位代碼快速查詢</legend>
            公司代碼：<asp:Label ID="lblSelectCompID" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblSelectCompName" runat="server" Text=""></asp:Label>
            <br />
            單位代碼：<asp:TextBox ID="txtSelectOrganID" runat="server"></asp:TextBox>
            <uc:ButtonQuerySelectOrgan ID="ucSelectOrgan" runat="server" />
            <br />
            單位名稱：<asp:Label ID="lblSelectOrganName" runat="server" Text=""></asp:Label>
            <br />
            組織來源：<asp:Label ID="lblSelectOrganType" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset style="width: 800px;">
            <legend>組織SubMenu</legend>
            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,CompName,OrganID,OrganName,OrganType" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <Columns>
                    <asp:TemplateField HeaderText="明細" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrganType" HeaderText="組織類型" ReadOnly="True" SortExpression="OrganType" >
                        <HeaderStyle Width="10%" CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                        <HeaderStyle Width="8%" CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />                                        
                    </asp:BoundField>
                    <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                        <HeaderStyle Width="10%" CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OrganID" HeaderText="單位代碼" ReadOnly="True" SortExpression="OrganID" >
                        <HeaderStyle Width="8%" CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="OrganName" HeaderText="單位名稱" ReadOnly="True" SortExpression="OrganName" >
                        <HeaderStyle Width="10%" CssClass="td_header" />
                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                <EmptyDataTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                </EmptyDataTemplate>
                <RowStyle CssClass="tr_evenline" />
                <AlternatingRowStyle CssClass="tr_oddline" />
                <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                <PagerStyle CssClass="GridView_PagerStyle" />
            </asp:GridView>
        </fieldset>
    </form>
</body>
</html>
