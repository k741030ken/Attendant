<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0700.aspx.vb" Inherits="SC_SC0700" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
            }
            
            switch(Param)
            {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }
        
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
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
     <%--<tr>
                <td align="center" width="100%">
                <table width="100%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblSysID" runat="server" ForeColor="blue" Text="系統別"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:Label ID="lblSysName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50"
                                    Width="360px"></asp:Label></td>
                </table>
                </td>
            </tr>--%>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left">
                                <asp:Label ID="lblSysID" runat="server" font-size="15px" ForeColor="blue" Text="系統別："></asp:Label>
                            </td>
                            <td colspan="5" align="left">
                                <asp:Label ID="lblSysName" runat="server" font-size="15px" CssClass="InputTextStyle_Thin" MaxLength="50" Width="200px"></asp:Label>
                            </td>
                            <td width="5%"></td>
                        </tr>                        
                        <tr>
                            <td width="5%"></td>
                            <td align="left"><asp:Label ID="lblAdminID" Font-Size="15px" runat="server" Text="系統管理者："></asp:Label>
                            </td>
                            <td align="left"><asp:TextBox CssClass="InputTextStyle_Thin" ID="txtSysID" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"><asp:Label ID="lblAdminName" font-size="15px" runat="server" Text="系統管理者名稱："></asp:Label>
                            </td>
                            <td align="left"><asp:TextBox CssClass="InputTextStyle_Thin" ID="txtSysName" runat="server"></asp:TextBox>
                            </td>
                            <td width="5%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">

                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="SysID,AdminComp,AdminID" Width="100%" PageSize="15">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                      <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SysID" HeaderText="系統別" ReadOnly="True" SortExpression="SysID" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="CompName" HeaderText="系統管理者公司" SortExpression="CompName" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="UserID" HeaderText="系統管理者" SortExpression="UserID" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" SortExpression="LastChgID" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="AdminComp" HeaderText="系統管理者公司" SortExpression="AdminComp" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AdminID" HeaderText="系統管理者代碼" SortExpression="AdminID" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
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
