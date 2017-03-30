<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0402.aspx.vb" Inherits="SC_SC0402" %>

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
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0">
    <form id="frmContent" runat="server">
     <tr>
                <td align="center" width="80%">
                <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                <td class="td_EditHeader" width="10%" align="center">
                                <asp:Label ID="lblSysID" runat="server" ForeColor="blue" Text="系統別"></asp:Label>
                </td>
                <td class="td_Edit" style="width: 30%" align="left">
                                <asp:Label ID="lblSysName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                </td>
                <td class="td_EditHeader" width="10%" align="center">
                                <asp:Label ID="lblCompRoleID" runat="server" ForeColor="blue" Text="授權公司"></asp:Label>
                </td>
                <td class="td_Edit" style="width: 30%" align="left">
                                <asp:Label ID="lblCompRoleName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                                <asp:DropDownList ID="ddlCompRoleName" runat="server" CssClass="DropDownListStyle" AutoPostBack = "true" ></asp:DropDownList>
                </td>
                </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblGroupID_H" ForeColor="Blue" runat="server" Text="*群組代碼"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblGroupID" Font-Bold="true" ForeColor="blue" CssClass="InputTextStyle_Thin" runat="server" Width="115px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblGroupName" runat="server" ForeColor="blue" Text="*群組名稱"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30"
                                    Width="360px"></asp:TextBox></td>
                            
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblLastChgComp_H" runat="server" Text="最後異動公司"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblLastChgComp" runat="server" CssClass="InputTextStyle_Thin"
                                    Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblLastChgID_H" runat="server" Text="最後異動人員"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblLastChgID" runat="server" CssClass="InputTextStyle_Thin"
                                    Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblLastChgDate_H" runat="server" Text="最後異動日期"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblLastChgDate" runat="server" CssClass="InputTextStyle_Thin"
                                    Width="200px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
        </form>
</body>
</html>
