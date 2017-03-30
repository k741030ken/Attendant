<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1000.aspx.vb" Inherits="GS_GS1000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--                    
        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                try
                {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch(ex)
                {}
            }
        }
       -->
    </script>
    <style type="text/css">
        .style1
        {
            width: 101px;
        }
        .style2
        {
            width: 124px;
        }
        .style3
        {
            width: 133px;
        }
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="100%" style="font-family:@微軟正黑體"><asp:Label ID="lblTitle" runat="server" Font-Names="微軟正黑體" Text="考核待辦"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,ApplyID,ApplyTime,Seq,Status,MainFlag,CompIdentity,IsSignNext,Result" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" 
                                                    CommandName="Detail" Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApplyTypeName" HeaderText="簽核事項" ReadOnly="True" SortExpression="ApplyTypeName" >
                                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyID" HeaderText="員工/部門編號" ReadOnly="True" SortExpression="ApplyID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyName" HeaderText="姓名/部門名稱" ReadOnly="True" SortExpression="ApplyName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyTimeShow" HeaderText="送達時間" ReadOnly="True" SortExpression="ApplyTime" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StatusName" HeaderText="狀態" ReadOnly="True" SortExpression="StatusName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ArrivalDay" HeaderText="到件天數" ReadOnly="True" SortExpression="ArrivalDay" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompID" HeaderText="CompID" ReadOnly="True" SortExpression="CompID" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyID" HeaderText="ApplyID" ReadOnly="True" SortExpression="ApplyID" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyTime" HeaderText="ApplyTime" ReadOnly="True" SortExpression="ApplyTime" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Seq" HeaderText="Seq" ReadOnly="True" SortExpression="Seq" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" SortExpression="Status" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MainFlag" HeaderText="MainFlag" ReadOnly="True" SortExpression="MainFlag" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompIdentity" HeaderText="CompIdentity" ReadOnly="True" SortExpression="CompIdentity" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" Font-Names="微軟正黑體" />
                                    <AlternatingRowStyle CssClass="tr_oddline" Font-Names="微軟正黑體" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" Font-Names="微軟正黑體" />
                                    <PagerStyle CssClass="GridView_PagerStyle" Font-Names="微軟正黑體" />
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
