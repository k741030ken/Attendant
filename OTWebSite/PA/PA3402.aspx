<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA3402.aspx.vb" Inherits="PA_PA3402" %>

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
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td>            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="center">
                                <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblSchoolID" runat="server" Text="學校代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtSchoolID" runat="server"></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRemark" ForeColor="Blue" runat="server" Text="*學校名稱"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="400" Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPrimaryFlag" runat="server" Text="重點學校註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:CheckBox ID="cbPrimaryFlag" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgComp" runat="server" Text="最後異動公司"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgComp" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgID" runat="server" Text="最後異動人員"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgID" runat="server" > </asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動日期"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgDate" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
