<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0401.aspx.vb" Inherits="SC_SC0401" %>

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
                                <asp:DropDownList ID="ddlCompRoleName" runat="server" CssClass="DropDownListStyle" AutoPostBack ="true" ></asp:DropDownList>
                </td>
                </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <%--<tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblGroupType" ForeColor="Blue" runat="server" Text="*群組類别"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left">
                                <asp:DropDownList ID="ddlGroupType" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="0">一般群組</asp:ListItem>
                                    <asp:ListItem Value="1">個人群組</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>--%>
                        <tr id="trGroupID" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblGroupID" ForeColor="Blue" runat="server" Text="*群組代碼"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtGroupID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"></asp:TextBox><%--style="text-transform:uppercase" 20150401 Beatrice del--%>
                            </td>
                        </tr>
                        <tr id="trGroupName" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblGroupName" runat="server" ForeColor="blue" Text="*群組名稱"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30"
                                    Width="150px"></asp:TextBox></td>
                            
                        </tr>
                        <%--<tr id="trGroupUser" style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="Label1" runat="server" ForeColor="blue" Text="*人員選取"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%" align="left">
                                <asp:UpdatePanel ID="udpDept" runat="server">
                                   <ContentTemplate><uc:OneUserSelect ID="ucUser" runat="server" DeptType="Dept" UserType="ValidUser" MustSelect="true" DisplayType="Full" /></ContentTemplate>
                                   <Triggers><asp:AsyncPostBackTrigger ControlID="ucUser" /></Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
        </form>
</body>
</html>
