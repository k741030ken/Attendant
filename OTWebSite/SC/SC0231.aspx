<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0231.aspx.vb" Inherits="SC_SC0231" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funGroupChange()
        {
            var obj = document.getElementById('ddlGroupType');
            
            if (obj != undefined)
            {
                if (obj.value == '0')
                {
                    trGroupID.style.display = 'block';
                    trGroupName.style.display = 'block';
                    trGroupUser.style.display = 'none';
                }
                else
                {
                    trGroupID.style.display = 'none';
                    trGroupName.style.display = 'none';
                    trGroupUser.style.display = 'block';
                }
            }
        }
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" onload="funGroupChange();">
    <form id="frmContent" runat="server">
        <asp:ScriptManager ID="smBase" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblGroupType" ForeColor="Blue" runat="server" Text="*群組類别"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left">
                                <asp:DropDownList ID="ddlGroupType" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="0">一般群組</asp:ListItem>
                                    <asp:ListItem Value="1">個人群組</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr id="trGroupID" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblGroupID" ForeColor="Blue" runat="server" Text="*群組代碼"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtGroupID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6" style="text-transform:uppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trGroupName" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblGroupName" runat="server" ForeColor="blue" Text="*組名"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30"
                                    Width="360px"></asp:TextBox></td>
                            
                        </tr>
                        <tr id="trGroupUser" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="Label1" runat="server" ForeColor="blue" Text="*人員選取"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:UpdatePanel ID="udpDept" runat="server">
                                   <ContentTemplate><uc:OneUserSelect ID="ucUser" runat="server" DeptType="Dept" UserType="ValidUser" MustSelect="true" DisplayType="Full" /></ContentTemplate>
                                   <Triggers><asp:AsyncPostBackTrigger ControlID="ucUser" /></Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
        </form>
</body>
</html>
