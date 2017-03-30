<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2400.aspx.vb" Inherits="OM_OM2400" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
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
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" Width="100%" 
                                    PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumber" runat="server" 
                                                    Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="2%" Height="15px" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="主管流水號" ReadOnly="True" SortExpression="BossSeq" 
                                            DataField="BossSeq">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管公司代碼" ReadOnly="True" SortExpression="BossCompID" 
                                            DataField="BossCompID">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        
                                        <asp:BoundField HeaderText="主管公司名稱" ReadOnly="True" SortExpression="CompName" 
                                            DataField="CompName">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管員工編號" ReadOnly="True" SortExpression="Boss" 
                                            DataField="Boss">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管姓名" ReadOnly="True" SortExpression="BossName" 
                                            DataField="BossName">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="上階部門代碼" ReadOnly="True" 
                                            SortExpression="ReportLineOrganID" DataField="ReportLineOrganID">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="上階部門名稱" ReadOnly="True" 
                                            SortExpression="ReportLineOrganName" DataField="ReportLineOrganName">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
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
