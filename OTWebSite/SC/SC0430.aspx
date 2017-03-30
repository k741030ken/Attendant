<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0430.aspx.vb" Inherits="SC_SC0430" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="80%">
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="lblInDept" runat="server" Text="員工編號"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:TextBox ID="txtUserID" runat="Server" MaxLength="6" style="text-transform:uppercase" CssClass="InputTextStyle_Thin"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
