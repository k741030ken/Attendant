<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0501.aspx.vb" Inherits="SC_SC0501" %>

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
        <table border="0" class="tbl_Edit" cellpadding="1" cellspacing="1" style="width: 100%; height: 100%">
            <tr>
                <td class="td_EditHeader" width="15%" align="center">
                    <asp:Label ID="lblSysID" runat="server" ForeColor="blue" Text="系統別"></asp:Label>
                </td>
                <td class="td_Edit" width="30%" align="left">
                    <asp:Label ID="lblSysName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                </td>
                <td class="td_EditHeader" width="15%" align="center">
                    <asp:Label ID="lblCompRoleID" runat="server" ForeColor="blue" Text="授權公司"></asp:Label>
                </td>
                <td class="td_Edit" width="40%" align="left">
                    <asp:Label ID="lblCompRoleName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                    <asp:DropDownList ID="ddlCompRoleName" runat="server" CssClass="DropDownList" AutoPostBack ="true" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_EditHeader" align="center"><asp:Label ID="lblGroup" ForeColor="Blue" runat="server" Text="*群組"></asp:Label>
                </td>
                <td class="td_Edit" align="left"><asp:DropDownList ID="ddlGroup" AutoPostBack="true" runat="server" CssClass="DropDownList"></asp:DropDownList>
                </td>
                <td class="td_EditHeader" align="center"><asp:Label ID="lblFun" ForeColor="Blue" runat="server" Text="*功能"></asp:Label>
                </td>
                <td class="td_Edit" align="left"><asp:DropDownList ID="ddlFun" AutoPostBack="True" runat="server" CssClass="DropDownList"></asp:DropDownList>
                </td>
            </tr>
            <tr style="height:30px">
                <td rowspan="15" class="td_EditHeader" align="center"><asp:Label ID="lblRight" ForeColor="Blue" runat="server" Text="權限"></asp:Label>
                </td>
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightA" runat="server" Text="新增" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightU" runat="server" Text="修改" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightD" runat="server" Text="刪除" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightI" runat="server" Text="查詢" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightE" runat="server" Text="執行" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightC" runat="server" Text="確認" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightP" runat="server" Text="列印" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightL" runat="server" Text="下傳" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightF" runat="server" Text="上傳" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightR" runat="server" Text="放行" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightG" runat="server" Text="複製" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightB" runat="server" Text="駁回" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightX" runat="server" Text="清除" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightH" runat="server" Text="保留" />
                </td>
            </tr>
            <tr style="height:30px">
                <td class="td_Edit" colspan="3" align="left"><asp:CheckBox ID="chkRightJ" runat="server" Text="保留" />
                </td>
            </tr>
        </table>            
        </form>
</body>
</html>
