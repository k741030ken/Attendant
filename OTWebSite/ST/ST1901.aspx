<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST1901.aspx.vb" Inherits="ST_ST1901" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

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
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCompID" runat="server" Text="公司代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="txtCompID" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpID" runat="server" Text="員工編號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="txtEmpID" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpName" Font-Size="15px" runat="server" Text="員工姓名"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="txtEmpName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblNotEmpReasonID" ForeColor="Blue" runat="server" Text="*中斷原因"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:DropDownList ID="ddlNotEmpReasonID" runat="server" Enabled="True" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblValidDateB" ForeColor="Blue" runat="server" Text="*中斷起日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <uc:uccalender ID="txtValidDateB" runat="server" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblValidDateE" ForeColor="Blue" runat="server" Text="*中斷迄日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                             <uc:uccalender ID="txtValidDateE" runat="server" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRemark" runat="server" Text="備註"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:TextBox ID="txtRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" Width="600px"></asp:TextBox>
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
