<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC02A2.aspx.vb" Inherits="SC_SC02A2" %>
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
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblAgentUserID_H" CssClass="MustInputCaption" runat="server" Text="*代理人"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblAgentUserName" Font-Bold="true" CssClass="InputTextStyle_Thin" runat="server" Width="115px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblAgencyType" runat="server" Text="代理種類"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:DropDownList ID="ddlAgencyType" runat="server" CssClass="DropDownListStyle" Width="115px">
                            <asp:ListItem Selected="true" Text="權限" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblValidFrom" CssClass="MustInputCaption" runat="server" Text="代理起日"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtValidFrom" MaxLength="10" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox><!--<uc:Date ID="ucValidFrom" runat="server" DateControlName="txtValidFrom" />-->
                                <asp:ImageButton runat="Server" ID="imgValidFrom" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtValidFrom" PopupButtonID="imgValidFrom" Format="yyyy/MM/dd" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblValidTo" CssClass="MustInputCaption" runat="server" Text="代理迄日"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtValidTo" MaxLength="10" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox><!--<uc:Date ID="ucValidTo" runat="server" DateControlName="txtValidTo" />-->
                                <asp:ImageButton runat="Server" ID="imgValidTo" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidTo" PopupButtonID="imgValidTo" Format="yyyy/MM/dd" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblValidFlag" runat="server" Text="有效註記"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:CheckBox ID="chkValidFlag" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblCreateDate_H" runat="server" Text="建立日期"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblCreateDate" runat="server" CssClass="InputTextStyle_Thin"
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
