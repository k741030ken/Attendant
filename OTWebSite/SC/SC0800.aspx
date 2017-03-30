<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0800.aspx.vb" Inherits="SC_SC0800" %>

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
                    if (frmContent.txtPassword.value.length == 0) 
                    {
                        window.alert("請輸入使用者密碼");
                        frmContent.txtPassword.focus();
                        return false;
                    }
                    if (frmContent.txtConfirmPassword.value.length == 0) {
                        window.alert("請輸入使用者新密碼");
                        frmContent.txtConfirmPassword.focus();
                        return false;
                    }
                    if (frmContent.txtPassword.value != frmContent.txtConfirmPassword.value) {
                        window.alert("新密碼與確認密碼不同");
                        frmContent.txtConfirmPassword.focus();
                        return false;
                    }
                    alert("修改完成！");

                case "btnDelete":
//                    if (!hasSelectedRow(''))
//                    {
//                        alert("未選取資料列！");
//                        return false;
//                    }
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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="100%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                       <%--<td style="width: 50%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                       </td>--%>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="20%">
                                <asp:Label ID="lblComp_H" ForeColor="Blue" runat="server" Text="公司"></asp:Label></td>
                            <td class="td_Edit" width="30%" align="left">
                                <asp:Label ID="lblCompID" runat="server"></asp:Label>
                                <asp:Label ID="lblComp" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="lblCompName" runat="server"></asp:Label></td>
                            <td class="td_EditHeader" width="20%">
                                <asp:Label ID="lblUser_H" ForeColor="Blue" runat="server" Text="員工"></asp:Label></td>
                            <td class="td_Edit" width="30%" align="left">
                                <asp:Label ID="lblUserID" runat="server"></asp:Label>
                                <asp:Label ID="lblUser" runat="server" Text="-"></asp:Label>
                                <asp:Label ID="lblUserName" runat="server"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="20%">
                                <asp:Label ID="lblDept_H" ForeColor="blue" runat="server" Text="部門"></asp:Label></td>
                            <td class="td_Edit" width="30%" align="left">
                                <asp:Label ID="lblDeptName" runat="server"></asp:Label></td>
                            <td class="td_EditHeader" width="20%">
                                <asp:Label ID="lblOrgan_H" ForeColor="blue" runat="server" Text="科組課"></asp:Label></td>
                            <td class="td_Edit" width="30%" align="left">
                                <asp:Label ID="lblOrganName" runat="server"></asp:Label></td>
                        </tr>
                        <tr id="trPassWord" style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblPassword" runat="server" ForeColor="blue" Text="*請輸入新密碼"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30" Width="360px" TextMode="Password"></asp:TextBox></td>
                            
                        </tr>
                        <tr id="trGroupUser" style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblConfirmPassword" runat="server" ForeColor="blue" Text="*請確認新密碼"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30" Width="360px" TextMode="Password"></asp:TextBox></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblExpireDate_H" runat="server" Text="密碼有效期限"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:Label ID="lblExpireDate" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblLastChgComp_H" runat="server" Text="最後異動公司"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:Label ID="lblLastChgComp" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblLastChgID_H" runat="server" Text="最後異動人員"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:Label ID="lblLastChgID" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="center">
                                <asp:Label ID="lblLastChgDate_H" runat="server" Text="最後異動日期"></asp:Label></td>
                            <td class="td_Edit" align="left" colspan="3">
                                <asp:Label ID="lblLastChgDate" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
