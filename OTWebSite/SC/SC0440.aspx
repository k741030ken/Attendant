<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0440.aspx.vb" Inherits="SC_SC0440" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 96%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                        <tr>
                            <td align="center">
                                <asp:Label ID="lbYear" Font-Size="12px" runat="server" Text="年份：" CssClass="MustInputCaption"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>　
                                <asp:Label ID="lbType" runat="server" Font-Size="12px" Text="類別："></asp:Label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                <%--<asp:Label ID="lbRank" runat="server" Font-Size="12px" Text="评等："></asp:Label>
                                <asp:DropDownList ID="ddlRank" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>--%>
                            </td>                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="5" Width="100%" DataKeyNames="Year,Type,Rank"  >
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399"  />
                                    <Columns>   
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>         
                                        <asp:BoundField DataField="Year" HeaderText="年份" ReadOnly="True" SortExpression="Year" >
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TypeShow" HeaderText="類別" ReadOnly="True" SortExpression="TypeShow" >
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Type" Visible="false" HeaderText="類別" ReadOnly="True" SortExpression="Type" >
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Rank" HeaderText="評等" ReadOnly="True" SortExpression="Rank" >
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RankLimitShow" HeaderText="限額(USD/仟元)" ReadOnly="True" SortExpression="RankLimitShow" >
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Right" />
                                        </asp:BoundField>                                     
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline"  />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />                                    
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

