<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1140.aspx.vb" Inherits="GS_GS1140" %>

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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td>            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="center">
                                <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                                    <tr style="height:20px">
                                        <td width="80%" align="left">
                                            <asp:Label ID="title" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>                                            
                                            <asp:HiddenField ID="hidEmpID" runat="server"></asp:HiddenField>
                                        </td>
                                        <td width="20%" align="right">
                                            <!--<asp:Button ID="btnclose" Text="關閉" runat="server" />-->
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td align="center" width="100%" colspan="2">
                                            <table class="tbl_Edit" cellpadding="1" cellspacing="1" style="font-family:@微軟正黑體">
                                                <tr style="height:20px">
                                                    <td width="12%" rowspan="3" class="td_EditHeader" align="center">Q1</td>
                                                    <td width="50%" colspan="5" class="td_EditHeader" align="center">個金業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">理財業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">法金業務(單位)</td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" align="center">房貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">信貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">個金<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">理財<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">單位職系<br>達成率</td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_0" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_1" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_2" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_3" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_4" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_5" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_6" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_7" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_8" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td width="12%" rowspan="3" class="td_EditHeader" align="center">Q2</td>
                                                    <td width="50%" colspan="5" class="td_EditHeader" align="center">個金業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">理財業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">法金業務(單位)</td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" align="center">房貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">信貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">個金<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">理財<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">單位職系<br>達成率</td>
                                                </tr>
                                                <tr style="height:20px">                                                    
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_9" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_10" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_11" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_12" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_13" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_14" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_15" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_16" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_17" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td width="12%" rowspan="3" class="td_EditHeader" align="center">Q3</td>
                                                    <td width="50%" colspan="5" class="td_EditHeader" align="center">個金業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">理財業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">法金業務(單位)</td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" align="center">房貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">信貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">個金<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">理財<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">單位職系<br>達成率</td>
                                                </tr>
                                                <tr style="height:20px">                                                    
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_18" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_19" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_20" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_21" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_22" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_23" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_24" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_25" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_26" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td width="12%" rowspan="3" class="td_EditHeader" align="center"><asp:Label ID="lblEPyear" runat="server"></asp:Label>年度<br>平均</td>
                                                    <td width="50%" colspan="5" class="td_EditHeader" align="center">個金業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">理財業務</td>
                                                    <td width="19%" colspan="2" class="td_EditHeader" align="center">法金業務(單位)</td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" align="center">房貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">信貸撥<br>款量<br>(仟元)</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">個金<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">理財<br>Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">Score Card</td>
                                                    <td class="td_EditHeader" align="center">總AP<br>(仟點)</td>
                                                    <td class="td_EditHeader" align="center">單位職系<br>達成率</td>
                                                </tr>
                                                <tr style="height:20px">                                                    
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_27" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_28" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_29" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_30" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_31" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_32" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_33" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_34" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
                                                    <td class="td_Edit" align="center"><asp:Label ID="lblPerformance_35" runat="server" Font-Names="微軟正黑體"></asp:Label></td>
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
