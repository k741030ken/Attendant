<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2300.aspx.vb" Inherits="OM_OM2300" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/jscript">
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
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblCompID" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:Label ID="txtCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganType" runat="server" Text="組織類型：" Font-Names="微軟正黑體" ></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:Label ID="txtOrganType" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidOrganType" runat="server"></asp:HiddenField>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganID" runat="server" Text="部門代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <%--<asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:Label ID="txtOrganID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganName" runat="server" Text="部門名稱：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <%--<asp:TextBox CssClass="InputTextStyle_Thin" ID="txtOrganName" runat="server"></asp:TextBox>--%>
                                <asp:Label ID="txtOrganName" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblNameN" runat="server" Text="現任部門主管：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:Label ID="txtBossName" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblValidDate" runat="server" Text="生效起迄日：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                            <asp:Label ID="txtValidDateBH" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <%--<uc:ucCalender ID="txtValidDateBH" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />--%>
                                <asp:Label ID="Label4" ForeColor="blue" runat="server" Text="～" Font-Names="微軟正黑體"></asp:Label>
                                <%--<uc:ucCalender ID="txtValidDateEH" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />--%>
                                <asp:Label ID="txtValidDateEH" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblBossType" runat="server" Text="主管任用方式：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:Label ID="txtBossType" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="15%"></td>
                            <td align="left" width="25%"></td>
                            <td width="10%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content" colspan="3">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" 
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" 
                                    DataKeyNames="CompID, OrganID, BossCompID, Boss, BossName, BossType, ValidDateBH, ValidDateEH" Width="100%" 
                                    PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="3%"  Font-Names="微軟正黑體"/>
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序號" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber" runat="server" 
                                                    Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" Height="15px" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" Font-Names="微軟正黑體"/>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateBH" 
                                            DataField="ValidDateBH">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateEH" 
                                            DataField="ValidDateEH">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管公司代碼" ReadOnly="True" SortExpression="BossCompID" 
                                            DataField="BossCompID">
                                            <HeaderStyle Width="6%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管員編" ReadOnly="True" SortExpression="Boss" 
                                            DataField="Boss">
                                            <HeaderStyle Width="8%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管姓名" ReadOnly="True" SortExpression="BossName" 
                                            DataField="BossName">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管任用方式" ReadOnly="True" 
                                            SortExpression="BossType" DataField="BossType">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動人員公司" ReadOnly="True" 
                                            SortExpression="LastChgComp" DataField="LastChgComp">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動人員" ReadOnly="True" 
                                            SortExpression="LastChgID" DataField="LastChgID">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="最後異動時間" ReadOnly="True" 
                                            SortExpression="LastChgDate" DataField="LastChgDate">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
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
