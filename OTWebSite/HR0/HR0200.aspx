<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR0200.aspx.vb" Inherits="HR_HR0200" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
		{
		    if (Param == 'btnActionX')
		        window.top.close();
		}
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:10px; margin-right:10px; margin-bottom:5px" >
    <form id="frmContent" runat="server"> 
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

    <table width="100%" border="0">
        <tr>
            <td align="center">
                <table width="100%" cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" >
                     <tr>
                        <td align="right" width="100px">&nbsp;<asp:Label ID="lblQUeryLabel" runat="server" Text="公司代碼：" CssClass="f9"></asp:Label></td>
                        <td align="left">                                                                
                            <asp:DropDownList ID="ddlCompID" runat="server" Font-Names="細明體"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100px">&nbsp;<asp:Label ID="Label2" runat="server" Text="主管代碼：" CssClass="f9"></asp:Label></td>
                        <td align="left">                                                                
                            <asp:TextBox runat="server" ID="txtEmpID" MaxLength="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="100px">&nbsp;<asp:Label ID="Label1" runat="server" Text="主管密碼：" CssClass="f9"></asp:Label></td>
                        <td align="left">                          
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtReturnValue" style="display:none" ></asp:TextBox> 
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
