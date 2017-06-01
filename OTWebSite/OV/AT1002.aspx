<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AT1002.aspx.vb" Inherits="AT_AT1002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
        }

    -->
    </script>
        <style  type="text/css">
        input, option, span, div, select,label,td,tr,table
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
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
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        <asp:Label ID="lblFldName" ForeColor="Blue" runat="server" Text="*代碼類別"></asp:Label>
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:DropDownList ID="ddlTabFldName" runat="server" Font-Names="微軟正黑體">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hidTabName" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="hidFldName" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        <asp:Label ID="lblCode" ForeColor="Blue" runat="server" Text="*代碼"></asp:Label>
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:TextBox ID="txtCode" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"
                                            Width="300px"></asp:TextBox>
                                        <asp:HiddenField ID="hidCode" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        <asp:Label ID="lblCodeCName" ForeColor="Blue" runat="server" Text="*代碼名稱"></asp:Label>
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:TextBox ID="txtCodeCName" runat="server" CssClass="InputTextStyle_Thin" 
                                            Height="50px" MaxLength="1000" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        <asp:HiddenField ID="hidCodeCName" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        <asp:Label ID="lblSortFld" ForeColor="Blue" runat="server" Text="*排序順序"></asp:Label>
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:TextBox ID="txtSortFld" CssClass="InputTextStyle_Thin" runat="server" MaxLength="3"
                                            OnKeyPress="if(((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46)) {event.returnValue = true;} else{event.returnValue = false;}">0</asp:TextBox>
                                        <asp:DropDownList ID="ddlSortFld" runat="server" Visible="false">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hidSortFld" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        <asp:Label ID="lblNotShowingFlag" runat="server" Text="不顯示註記"></asp:Label>
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:CheckBox ID="chkNotShowingFlag" runat="server" />
                                        <asp:HiddenField ID="hidNotShowingFlag" runat="server"></asp:HiddenField>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        最後異動公司
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:Label ID="lblLastChgComp" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        最後異動人員
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:Label ID="lblLastChgID" runat="server"> </asp:Label>
                                    </td>
                                </tr>
                                <tr style="height: 20px">
                                    <td class="td_EditHeader" width="15%" align="center">
                                        最後異動日期
                                    </td>
                                    <td class="td_Edit" style="width: 35%" align="left" colspan="3">
                                        <asp:Label ID="lblLastChgDate" runat="server"></asp:Label>
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
