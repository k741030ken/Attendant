<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2200.aspx.vb" Inherits="OM_OM2200" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow('')) {
                        alert("未選取資料列！");
                        return false;
                    }
            }

            switch (Param) {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }
           -->
    </script>
    <style type="text/css">
        .style1
        {
            height: 17px;
            Font-family:Calibri, 微軟正黑體;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="center" style="height: 30px;">
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%"
                    width="100%">
                    <tr>
                        <td width="10%" class="style1">
                        </td>
                        <td align="center" width="15%" class="style1">
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td align="left" width="25%" class="style1">
                            <asp:Label ID="txtCompID" Font-Size="15px" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            <%--<asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>--%>
                        </td>
                        <td align="center" width="15%" class="style1">
                            <asp:Label ID="lblOrganType" Font-Size="15px" runat="server" Text="組織類型：" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td align="left" width="25%" class="style1">
                            <asp:Label ID="txtOrganType" Font-Size="15px" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            <asp:HiddenField ID="hidOrganType" runat="server" />
                            <asp:HiddenField ID="hidOrganReason" runat="server" />
                        </td>
                        <td width="10%" class="style1">
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                        </td>
                        <td align="center" width="15%">
                            <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Text="部門代碼：" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td align="left" width="25%">
                            <asp:Label ID="txtOrganID" Font-Size="15px" runat="server" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td align="center" width="15%">
                            <asp:Label ID="lblOrganName" Font-Size="15px" runat="server" Text="部門名稱：" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td align="left" width="25%">
                            <asp:Label ID="txtOrganName" Font-Size="15px" runat="server" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td width="10%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" width="100%">
                <table width="100%" height="100%" class="tbl_Content">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <%--(20161101-leo modify) DataKeyNames--%>
                            <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                CellPadding="2" DataKeyNames="CompID,OrganReason,ValidDateB,Seq,ValidDateE,OrganID,OrganType,OrganNameOld,OrganName"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" OnRowCreated="gvMain_RowCreated">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="3%" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                                Text="明細"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="td_header" Width="5%" />
                                        <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" />
                                    </asp:TemplateField>
                                    <%--      20161101-leo modify--%>
                                    <asp:BoundField HeaderText="生效日期" ReadOnly="True" SortExpression="ValidDateB" DataField="ValidDateB">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>" Font-Names="微軟正黑體"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" Height="15px" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="異動原因" ReadOnly="True" SortExpression="OrganReason" DataField="OrganReason">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門代號" ReadOnly="True" SortExpression="OrganID" DataField="OrganID">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門名稱" ReadOnly="True" SortExpression="OrganNameOld" DataField="OrganNameOld">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門主管" ReadOnly="True" SortExpression="BossOld" DataField="BossOld">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="上階部門" ReadOnly="True" SortExpression="UpOrganIDOld" DataField="UpOrganIDOld">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門代號" ReadOnly="True" SortExpression="OrganID" DataField="OrganID">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門名稱" ReadOnly="True" SortExpression="OrganName" DataField="OrganName">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="部門主管" ReadOnly="True" SortExpression="Boss" DataField="Boss">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="上階部門" ReadOnly="True" SortExpression="UpOrganID" DataField="UpOrganID">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%-- (20161101-leo modify) '組織類型--%>
                                    <asp:BoundField HeaderText="組織類型" ReadOnly="True" SortExpression="OrganType" DataField="OrganType"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" DataField="ValidDateE"
                                        Visible="false">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Seq" ReadOnly="True" SortExpression="Seq" DataField="Seq"
                                        Visible="false">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="tr_evenline" />
                                <AlternatingRowStyle CssClass="tr_oddline" />
                                <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                                <HeaderStyle ForeColor="DimGray"></HeaderStyle>
                                <PagerStyle CssClass="GridView_PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
