<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA3101.aspx.vb" Inherits="PA_PA3101" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--

    -->
    </script>
    <style type="text/css">
        .style1
        {
            border: 1px solid #5384e6;
            font-size: 14px;
            background-color: #e2e9fe;
            height: 23px;
        }
        .style2
        {
            border: 1px solid #89b3f5;
            font-size: 14px;
            width: 35%;
            height: 23px;
        }
    </style>
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
                                        <td class="style1" width="15%" align="center">
                                            <asp:Label ID="lblEduID" ForeColor="Blue" runat="server" Text="*學歷代碼"></asp:Label>
                                        </td>
                                        <td class="style2" align="left" colspan="3">
                                            <asp:TextBox ID="txtEduID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="3"></asp:TextBox>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEduName" ForeColor="Blue" runat="server" Text="*學歷名稱(繁)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEduName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEduNameCN" ForeColor="Blue" runat="server" Text="*學歷名稱(簡)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEduNameCN" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="300px"></asp:TextBox>
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
