<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA091.aspx.vb" Inherits="WF_WFA091" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            if (Param == 'btnActionX') {
                window.top.close();
            }
        }

        function funClose(msg) {
            alert(msg);
            window.top.returnValue = 'OK';
            window.top.close();
        }
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <asp:ScriptManager ID="smBase" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="95%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label4" runat="server" Text="類別"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblFlowTypeNm" runat="Server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label1" runat="server" Text="案件編號"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblAppID" runat="Server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblCCID" runat="Server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblCRID" runat="Server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                <asp:Label ID="lblRAID" runat="Server" Font-Bold="true" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label2" runat="server" Text="代表客户"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblCustomer" runat="Server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="Label3" runat="server" Text="負責AO"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:Label ID="lblAO" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblRemark" runat="server" Text="退關卡说明"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtRemark" CssClass="InputTextStyle_Thin" runat="server" TextMode="MultiLine" Height="160px" Width="100%" MaxLength="1000"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
        </form>
</body>
</html>


