<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR9101.aspx.vb" Inherits="HR_HR9101" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
    -->
    </script>
    <style type="text/css">
        .style1
        {
            border: 1px solid #89b3f5;
            font-size: 12px;
            height: 29px;
        }
    </style>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0">
    <form id="frmContent" runat="server">
    <table style="width:100%;">
        <tr>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblEmpID" runat="server" Text="員工編號"></asp:Label>
            </td>
            <td class="style1" width="15%" align="left">
                <asp:Label ID="lblEmpID1" runat="server" BorderStyle="Solid" BorderWidth="1px" Height = "20px" Width = "70px"></asp:Label>
                <asp:Label ID="lblEmpName" runat="server" BorderStyle="Solid" BorderWidth="1px" Height = "20px" Width = "70px"></asp:Label>
                <asp:Label ID="lblCompID" runat="server" Visible="False" Height = "20px" Width = "50px"></asp:Label>
            </td>
            <td class="style1" width="15%" align="center">
                <asp:CheckBox ID="chkReason" runat="server" Text="異動原因" />
            </td>
            <td class="style1" width="15%" align="left">
                <asp:DropDownList ID="ddlReason" runat="server" Enabled="False" >
                </asp:DropDownList>
            </td>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblApplyDate" runat="server" Text="申請日期" ></asp:Label>
            </td>
            <td class="style1" width="15%" align="left">
                <asp:Label ID="lblApplyDate1" runat="server" ></asp:Label>                
            </td>
        </tr>
        <tr>
            <td class="style1" width="15%" align="center" >
                <asp:Label ID="lblOldData" runat="server" Text="變更前"></asp:Label>
            </td>
            <td class="style1" width="30%" align="left" colspan="5">
                <asp:Label ID="lblOldData1" runat="server" Width="800px"></asp:Label>                
            </td>           
        </tr>
        <tr>
         <td class="style1" width="15%" align="center">
                <asp:Label ID="lblNewData" runat="server" Text="變更後"></asp:Label>
            </td>
            <td class="style1" width="30%" align="left" colspan="5">
                <asp:Label ID="lblNewData1" runat="server" Width="800px"></asp:Label>              
            </td>
        </tr>
        <tr>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblRemark" runat="server" Text="備註"></asp:Label>
            </td>
            <td class="style1" width="30%" align="left" colspan="5">
                <asp:TextBox ID="txtRemark" runat="server" Width="800px" MaxLength="20"></asp:TextBox>
            </td>         
        </tr>
    </table>
     <br />
        </form>
</body>
</html>
