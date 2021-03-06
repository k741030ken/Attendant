<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0322.aspx.vb" Inherits="SC_SC0322" %>

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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblType_H" CssClass="MustInputCaption" runat="server" Text="*代碼類別"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblType" runat="server" CssClass="f9"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblCode_H" CssClass="MustInputCaption" runat="server" Text="*代碼"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblCode" runat="server" Width="115px" CssClass="f9"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblDefine" runat="server" Text="代碼說明"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtDefine" CssClass="InputTextStyle_Thin" runat="server" MaxLength="200" Width="420"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblValidFlag" runat="server" Text="有效註記"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:CheckBox ID="chkValidFlag" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblOrderSeq" runat="server" Text="排序"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtOrderSeq" CssClass="InputTextStyle_Thin" runat="server" MaxLength="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblNote" runat="server" Text="補充說明"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtNote" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label1" runat="server" Text="預留欄位1"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRsvCol1" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label2" runat="server" Text="預留欄位2"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRsvCol2" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label3" runat="server" Text="預留欄位3"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRsvCol3" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label4" runat="server" Text="預留欄位4"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRsvCol4" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label5" runat="server" Text="預留欄位5"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRsvCol5" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblCreateDate_H" runat="server" Text="建立日期"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblCreateDate" runat="server"  CssClass="f9"
                                    Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblLastChgID_H" runat="server" Text="最後異動人員"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblLastChgID" runat="server" CssClass="f9"
                                    Width="200px"></asp:Label></td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center">
                                <asp:Label ID="lblLastChgDate_H" runat="server" Text="最後異動日期"></asp:Label></td>
                            <td class="td_Edit" style="width: 70%;" align="left">
                                <asp:Label ID="lblLastChgDate" runat="server" CssClass="f9"
                                    Width="200px"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
        </form>
</body>
</html>
