﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2301.aspx.vb" Inherits="OM_OM2301" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
     <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="*公司代碼" ForeColor="blue"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="25%">
                                <asp:Label ID="txtCompID" Font-Size="15px" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidOrganType" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Text="*部門代碼" ForeColor="blue"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="25%">
                                <asp:Label ID="txtOrganID" Font-Size="15px" runat="server"></asp:Label>
                                <asp:HiddenField ID="hidOrganID" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblBossCompID" Font-Size="15px" runat="server" Text="*主管公司代碼" ForeColor="blue"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="25%">
                                <asp:DropDownList ID="ddlBossCompID" runat="server" AutoPostBack="true" Font-Names="7">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblBoss" Font-Size="15px" runat="server" Text="*主管員工編號" ForeColor="blue"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="25%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtBoss" runat="server" AutoPostBack="True"></asp:TextBox>
                                <asp:Label ID="lblBossName" Font-Size="15px" runat="server" Text=""></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucQueryBoss" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            
                        </tr>
                        <tr>
                            
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblBossType" Font-Size="15px" runat="server" Text="*主管任用方式" ForeColor="blue"></asp:Label>
                            </td>
                                <td class="td_Edit" align="left" width="25%">
                                <asp:DropDownList ID="ddlBossType" runat="server">
                                        <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="主要"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="兼任"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                            <td  class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblValidDate" Font-Size="15px" runat="server" Text="*生效起迄日" ForeColor="blue"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="25%">
                                <uc:ucCalender ID="txtValidDateBH" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="Label3" ForeColor="blue" runat="server" Text="～"></asp:Label>
                                <uc:ucCalender ID="txtValidDateEH" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
