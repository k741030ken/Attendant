<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA041.aspx.vb" Inherits="WF_WFA041" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:10" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 80%">
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1" runat="server" id="T_Form">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblFlowName" CssClass="MustInputCaption" runat="server" Text="流程"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:DropDownList ID="ddlFlowName" runat="server" CssClass="DropDownListStyle" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblFlowStepDesc" CssClass="MustInputCaption" runat="server" Text="關卡名稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:DropDownList ID="ddlFlowStep" runat="server"  DataTextField="FlowStepDesc" CssClass="DropDownListStyle" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblFlowPhrase" CssClass="MustInputCaption" runat="server" Text="片語"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:TextBox ID="txtFlowPhrase" runat="server" Width="500px" Rows="5" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
