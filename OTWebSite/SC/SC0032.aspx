<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0032.aspx.vb" Inherits="SC_SC0032" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript">
    <!--
        function redirectPage(Path) {
            
        }
        
        function checkStatus()
        {

        }
        function funOnLoad() {

        }
        
        
    -->
    </script>
</head>
<body style="margin-right:10; margin-left:10; margin-top:10; margin-bottom:10; background: url(../images/menu_bg.gif)" onload="funOnLoad();">
    <form id="frmContent" runat="server" style="margin-left:10px">
    <div style="">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lblCompID" ForeColor="Blue" Font-Size="15px" runat="server" Text="授權公司："></asp:Label>
                </td>
                <td align="left" >
                    <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                </td> 
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
