<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1170.aspx.vb" Inherits="GS_GS1170" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        
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
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;font-family:@微軟正黑體">
            <tr style="font-family:@微軟正黑體">
                <td style="font-family:@微軟正黑體">            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;font-family:@微軟正黑體">
                        <tr style="font-family:@微軟正黑體"> 
                            <td align="center" style="font-family:@微軟正黑體">
                                <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1" style="font-family:@微軟正黑體">
                                    <tr style="font-family:@微軟正黑體">
                                        <td align="center" width="100%" colspan="2" style="font-family:@微軟正黑體">
                                            <table width="100%" height="100%" class="tbl_Content" style="font-family:@微軟正黑體">
                                                <tr>
                                                    <td style="width: 100%;font-family:@微軟正黑體">
                                                        <table class="tbl_Edit" cellpadding="1" cellspacing="1" width="100%" style="font-family:@微軟正黑體">
                                                            <tr style="height:20px;font-family:@微軟正黑體">
                                                                <%--<td width="20%" class="td_EditHeader" align="center">簽核主管</td>--%>
                                                                <td width="100%" class="td_EditHeader" align="center" style="font-family:@微軟正黑體"><asp:Label ID="Label1" runat="server" Font-Names="微軟正黑體" Text="考績補充說明"></asp:Label></td>
                                                                <%--<td width="40%" class="td_EditHeader" align="center" style="font-family:@微軟正黑體">整體評量調整說明</td>--%>
                                                            </tr>
                                                            <tr style="height:20px;font-family:@微軟正黑體">
                                                                <%--<td width="20%" class="td_Edit" align="left"><asp:Label ID="lblSignName" runat="server"></asp:Label></td>--%>
                                                                <td width="100%" class="td_Edit" align="left" style="font-family:@微軟正黑體"><asp:Label ID="lblComment" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                                <%--<td width="40%" class="td_Edit" align="left" style="font-family:@微軟正黑體"><asp:Label ID="lblComment_Adjust" runat="server" Font-Names="微軟正黑體"></asp:Label></td>--%>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
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
