<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0441.aspx.vb" Inherits="SC_SC0441" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
</head>
<body style="margin-top:0px; margin-left:5px; margin-right:5px; margin-bottom:0">
    <form id="frmConent" runat="server">

        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <th class="td_header" width="30%">*年份</th>
                            <td class="td_detail" style="width:70%" align="left">
                                <%--<asp:DropDownList runat="server" ID="ddlYear" CssClass="DropDownListStyle" />--%>
                                <asp:TextBox ID="tbxYear" runat="server" MaxLength="4" CssClass="InputTextStyle_Thin number"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <th class="td_header" width="30%">*類別</th>
                            <td class="td_detail" style="width:70%" align="left">
                                <asp:DropDownList runat="server" ID="ddlType" CssClass="DropDownListStyle" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <th class="td_header" width="30%">*評等</th>
                            <td class="td_detail" style="width:70%" align="left">
                                <asp:DropDownList runat="server" ID="ddlRank" CssClass="DropDownListStyle">
                                <asp:ListItem Value="H1">H1</asp:ListItem>
                                <asp:ListItem Value="H2">H2</asp:ListItem>
                                <asp:ListItem Value="H3">H3</asp:ListItem>
                                <asp:ListItem Value="H4">H4</asp:ListItem>
                                <asp:ListItem Value="H5">H5</asp:ListItem>
                                <asp:ListItem Value="H6">H6</asp:ListItem>
                                <asp:ListItem Value="H7">H7</asp:ListItem>
                                <asp:ListItem Value="H8">H8</asp:ListItem>
                                <asp:ListItem Value="H9">H9</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <th class="td_header" width="30%">*限額(USD/仟元)</th>
                            <td class="td_detail" style="width:70%" align="left">
                                <asp:TextBox ID="tbxRankLimit" CssClass="InputTextStyle_Thin" runat="server" rel="decimal,12,2" />
                            </td>
                        </tr>                        
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <script src="../ClientFun/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../ClientFun/jquery.validate.js" type="text/javascript"></script>
    <script src="../ClientFun/jquery.NobleCount.js" type="text/javascript"></script>
    <script src="../ClientFun/jquery.CurrencyFormat.js" type="text/javascript"></script>
    <script src="../ClientFun/CustomValidator.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tbxRankLimit').rules("add", { required: true, messages: { required: "請填寫限額"} });
            $('#tbxYear').rules("add", { required: true, messages: { required: "請填寫年份"} });
            $('#ddlType').rules("add", { required: true, messages: { required: "請選擇類別"} });
            $('#ddlRank').rules("add", { required: true, messages: { required: "請選擇評等"} });
        });

        function funAction(Param) {
            switch (Param) {
                case "btnAdd":
                case "btnUpdate":
                    return checkFunAction();
                    break;
            }
        }
    </script>
</body>
</html>
