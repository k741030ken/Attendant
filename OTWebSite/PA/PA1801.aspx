<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA1801.aspx.vb" Inherits="PA_PA1801" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        //職位名稱
        function displaycount_Remark() {
            var total = parseInt($('#txtRemark').val().length);
            $("#lblCount_Remark").html('(最多40字，已輸入 <b>' + total + '</b> 字 / 40字)');
        }

        //        隱藏TR
        function hide_tr(strID) {
            var result_style = document.getElementById(strID).style; result_style.display = 'none';
        }
        //        顯示TR
        function show_tr(strID) {
            var result_style = document.getElementById(strID).style; result_style.display = 'table-row';
        }
        function ChangeValue(e) {
            alert(e.id);         
        }
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
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCompID" ForeColor="Blue" runat="server" Text="*公司代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lbltxtCompID" runat="server" ></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPositionID" ForeColor="Blue" runat="server" Text="*職位代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtPositionID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRemark" ForeColor="Blue" runat="server" Text="*職位名稱"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="40" TextMode="MultiLine" Width="400px" Height="40px" onkeyup="displaycount_Remark()"></asp:TextBox>
                                            <asp:Label ID="lblCount_Remark" runat="server" ForeColor="Red" Font-Size="12px" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblInValidFlag" runat="server" Text="無效註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:CheckBox ID="chkInValidFlag" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblSortOrder" runat="server" Text="排序"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtSortOrder" runat="server" MaxLength="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblOrganPrintFlag" runat="server" Text="部門列印註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:DropDownList ID="ddlOrganPrintFlag" runat="server" Font-Names="細明體">
                                                <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="0-不可列印"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1-可列印"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblIsEVManager" runat="server" Text="績效考核表主管註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:CheckBox ID="chkIsEVManager" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCategoryI" runat="server" Text="大類"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:DropDownList ID="ddlCategoryI" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCategoryII" runat="server" Text="中類"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:UpdatePanel ID="UpdCategoryII" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCategoryII" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                                </ContentTemplate>                                                          
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlCategoryI" />
                                            </Triggers>
                                            </asp:UpdatePanel>                                            
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCategoryIII" runat="server" Text="細類"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                            <asp:UpdatePanel ID="UpdCategoryIII" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                     <asp:DropDownList ID="ddlCategoryIII" runat="server" Font-Names="細明體"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>                                            
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
