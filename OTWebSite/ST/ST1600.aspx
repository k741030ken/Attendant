<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST1600.aspx.vb" Inherits="ST_ST2402" %>

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
                                            <asp:Label ID="lblCompID" runat="server" Text="公司代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtCompID" runat="server"></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpID" runat="server" Text="員工編號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtEmpID" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblName" runat="server" Text="員工姓名"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRegTel" runat="server" Text="戶籍電話"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblRCountry" runat="server" Text="(國別)"></asp:Label>
                                            <asp:DropDownList ID="ddlRCountry" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblRCountryCode" runat="server" Text="(國碼)"></asp:Label>
                                            <asp:TextBox ID="txtRCountryCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblRAreaCode" runat="server" Text="(區碼)"></asp:Label>
                                            <asp:TextBox ID="txtRAreaCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="20"></asp:TextBox>
                                            <asp:Label ID="lblRPhone" runat="server" Text="(電話)"></asp:Label>
                                            <asp:TextBox ID="txtRPhone" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>                                              
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCommTel" runat="server" Text="通訊電話"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblCCountry" runat="server" Text="(國別)"></asp:Label>
                                            <asp:DropDownList ID="ddlCCountry" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblCCountryCode" runat="server" Text="(國碼)"></asp:Label>
                                            <asp:TextBox ID="txtCCountryCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblCAreaCode" runat="server" Text="(區碼)"></asp:Label>
                                            <asp:TextBox ID="txtCAreaCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="20"></asp:TextBox>
                                            <asp:Label ID="lblCPhone" runat="server" Text="(電話)"></asp:Label>
                                            <asp:TextBox ID="txtCPhone" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>                                              
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblMobile" ForeColor="Blue" runat="server" Text="*行動電話1"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblMCountry" runat="server" Text="(國別)"></asp:Label>
                                            <asp:DropDownList ID="ddlMCountry" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblMCountryCode" runat="server" Text="(國碼)"></asp:Label>
                                            <asp:TextBox ID="txtMCountryCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblMPhone" runat="server" Text="(電話)"></asp:Label>
                                            <asp:TextBox ID="txtMPhone" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>                                            
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblMobile2" runat="server" Text="行動電話2"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblMCountry2" runat="server" Text="(國別)"></asp:Label>
                                            <asp:DropDownList ID="ddlMCountry2" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblMCountry2Code" runat="server" Text="(國碼)"></asp:Label>
                                            <asp:TextBox ID="txtMCountry2Code" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblMPhone2" runat="server" Text="(電話)"></asp:Label>
                                            <asp:TextBox ID="txtMPhone2" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRegAddr" runat="server" Text="戶籍地址"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblRegCityCode" runat="server" Text="(縣市別)"></asp:Label>
                                            <asp:DropDownList ID="ddlRegCityCode" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCode_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblRegAddrCode" runat="server" Text="(郵遞區號)"></asp:Label>
                                            <asp:DropDownList ID="ddlRegAddrCode" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlAddrCode_SelectedChanged"></asp:DropDownList>
                                            <asp:TextBox ID="txtRegAddr" CssClass="InputTextStyle_Thin" runat="server" Width="350px" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCommAddr" runat="server" Text="通訊地址"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:CheckBox ID="cbSameReg" runat="server" Text="同戶籍" AutoPostBack="true" OnCheckedChanged="cbSameReg_CheckedChanged"></asp:CheckBox>
                                            <br />
                                            <asp:Label ID="lblCommCityCode" runat="server" Text="(縣市別)"></asp:Label>
                                            <asp:DropDownList ID="ddlCommCityCode" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCode_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblCommAddrCode" runat="server" Text="(郵遞區號)"></asp:Label>
                                            <asp:DropDownList ID="ddlCommAddrCode" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlAddrCode_SelectedChanged"></asp:DropDownList>
                                            <asp:TextBox ID="txtCommAddr" CssClass="InputTextStyle_Thin" runat="server" Width="350px" MaxLength="100"></asp:TextBox>                                         
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRelName" runat="server" Text="緊急連絡人"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtRelName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRelRelation" runat="server" Text="緊急連絡人關係"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:DropDownList ID="ddlRelRelation" runat="server" Font-Size="12px"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRelTel" runat="server" Text="緊急聯絡人電話"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtRelTel" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmail" runat="server" Text="公司Email"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEmail" CssClass="InputTextStyle_Thin" Width="350px" runat="server" MaxLength="60" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmail2" runat="server" Text="Email2"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEmail2" CssClass="InputTextStyle_Thin" Width="350px" runat="server" MaxLength="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCompTel" ForeColor="Blue" runat="server" Text="*公司電話"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblCompCountry" runat="server" Text="(國別)"></asp:Label>
                                            <asp:DropDownList ID="ddlCompCountry" runat="server" Font-Size="12px" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedChanged"></asp:DropDownList>
                                            <asp:Label ID="lblCompCountryCode" runat="server" Text="(國碼)"></asp:Label>
                                            <asp:TextBox ID="txtCompCountryCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="6"></asp:TextBox>
                                            <asp:Label ID="lblCompAreaCode" runat="server" Text="(區碼)"></asp:Label>
                                            <asp:TextBox ID="txtCompAreaCode" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="3"></asp:TextBox>
                                            <asp:Label ID="lblCompPhone" runat="server" Text="(電話)"></asp:Label>
                                            <asp:TextBox ID="txtCompPhone" CssClass="InputTextStyle_Thin" runat="server" MaxLength="22"></asp:TextBox>
                                            <asp:Label ID="lblCompExt" runat="server" Text="分機"></asp:Label>
                                            <asp:TextBox ID="txtCompExt" CssClass="InputTextStyle_Thin" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgComp" runat="server" Text="最後異動公司"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgComp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgID" runat="server" Text="最後異動人員"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgID" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動時間"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="txtLastChgDate" runat="server"></asp:Label>
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
