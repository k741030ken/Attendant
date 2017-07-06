<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpWorkTimeQuery_Detail.aspx.cs" Inherits="EmpWorkTimeQuery_Detail" %>
<!DOCTYPE html >

<html>
<head runat="server">    
    <title>個人班表查詢-異動紀錄</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
    .Util_clsBtn 
    {
    	margin-right: 10px;
    }
    .td_header
    {
    	background-color: White;
    }
    </style>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">個人班表查詢-異動紀錄</legend>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left">
                    <table cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr style="height:20px">
                            <td colspan="3" align="left">
                                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" CssClass="Util_clsBtn" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <div style ="height:75%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" AllowPaging="false" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="序號" >
                    <ItemTemplate>
                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="3%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="異動類型" DataField="Action">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="單位類別" DataField="OrgTypeName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部門" DataField="DeptName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="科組課" DataField="OrganName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="員編" DataField="EmpID">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="NameN">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="班別" DataField="WTID">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="時間" DataField="WorkTime">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="輪班註記" DataField="RotateFlag">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgID" HeaderText="最後異動者">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期">
                    <HeaderStyle Width="12%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
            <EmptyDataTemplate>
                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
            </EmptyDataTemplate>
            <RowStyle CssClass="Util_gvRowNormal" />
            <AlternatingRowStyle CssClass="Util_gvRowAlternate" />
            <PagerStyle CssClass="GridView_PagerStyle" />
        </asp:GridView>
    </div>
    </fieldset>

    </form>
</body>
</html>
