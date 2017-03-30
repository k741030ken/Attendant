<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA092.aspx.vb" Inherits="WF_WFA092" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="/StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
        }

        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex)
                { }
            }
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="95%">
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label1" runat="server" Text="客戶統編"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlCustomerID" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="="></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like" Selected="True"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtCustomerID" runat="server" CssClass="InputTextStyle_Thin" Width="100px" MaxLength="10" style="text-transform:uppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label5" runat="server" Text="公司名稱"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlCName" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="="></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like" Selected="True"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtCName" runat="server" CssClass="InputTextStyle_Thin" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label2" runat="server" Text="案件編號"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlAppID" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="="></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like" Selected="True"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtAppID" runat="server" CssClass="InputTextStyle_Thin"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label3" runat="server" Text="執行人员"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlUserID" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="95%">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="True"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="" Width="100%" HeaderStyle-ForeColor="Dimgray" RowStyle-Height="18px">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="LogDate" HeaderText="異動時間" ReadOnly="True">
                                            <HeaderStyle Width="14%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FlowStepDesc" HeaderText="種類" ReadOnly="True">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FlowCaseID" HeaderText="流程編號" ReadOnly="True">
                                            <HeaderStyle Width="13%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AppID" HeaderText="案件編號" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustomerNm" HeaderText="代表客户" ReadOnly="True">
                                            <HeaderStyle Width="20%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AONm" HeaderText="AO" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UserNm" HeaderText="異動人員" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="異動說明" ReadOnly="True">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
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