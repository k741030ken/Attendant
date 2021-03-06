<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0251.aspx.vb" Inherits="SC_SC0251" %>

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
                    <table width="80%" height="100%" class="tbl_Edit">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblUserID_H" runat="server" Text="員工編號"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblUserID" runat="server" CssClass="InputTextStyle_Thin" Width="115px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblUserName_H" ForeColor="blue" runat="server" Text="員工姓名"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblUserName" CssClass="InputTextStyle_Thin" runat="server" Width="300px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 100%">
                                <table class="tbl_Content" width="100%">
                                    <tr><td>
                                        <uc:MultiSelect ID="ucGroupSelect" ShowDeptType="false" LeftCaption="未選群組" RightCaption="已選群組" ListRows=20 runat="server" />
                                    </td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
