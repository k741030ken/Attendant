<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0413.aspx.vb" Inherits="SC_SC0413" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            if (Param == 'btnActionX')
            {
                window.top.close();
            }
        }
        
        function funClose(msg)
        {
            window.top.returnValue = 'OK';
            window.top.close();
        }
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="Left" style="width:100%; font-size:12px">
                    &nbsp;&nbsp;功能按鍵：<asp:Label ID="lblButtonName" runat="server" ForeColor="Brown"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="width:100%">
                    <asp:TextBox ID="txtAfterSQL" runat="server" Font-Names="calibri" TextMode="MultiLine" Columns="500" Height="250px" MaxLength="8000" Width="99%" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
        </form>
</body>
</html>
