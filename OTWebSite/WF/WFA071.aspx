<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA071.aspx.vb" Inherits="WF_WFA071" %>
<%@ Register TagPrefix="uc" TagName="PagerControl" Src="~/Component/ucPagerControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="True"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="FlowID,FlowCaseID,FlowLogBatNo,FlowLogID" Width="100%" HeaderStyle-ForeColor="Dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UserName" HeaderText="姓名" ReadOnly="True" SortExpression="UserName">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PaperID" HeaderText="案件編號" ReadOnly="True" SortExpression="PaperID">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustomerName" HeaderText="客戶" ReadOnly="True" SortExpression="CustomerName">
                                            <HeaderStyle Width="30%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FlowStepDesc" HeaderText="流程關卡" ReadOnly="True" SortExpression="FlowStepDesc">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="到件時間" SortExpression="FromDate">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("FromDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="流程明细">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbnFlowDetail" runat="server" CommandName="FlowDetail" ForeColor="blue" Text="Flow Detail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="沒有資料！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                    <HeaderStyle ForeColor="DimGray" />
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
