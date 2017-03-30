<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1100.aspx.vb" Inherits="GS_GS1100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnActionC":
                    if (!confirm('是否呈上階主管審核或送出?'))
                        return false;
                    break;
            }
        }

        function ChangeGroup(Param) {
//            if (!confirm('是否暫存考績?')) {
//                return false;
//            }
            fromField = document.getElementById("btnChangeGroup");
            fromField.click()
            
        }

        function btnRefer_Click(type) {
            if (type == "1") {
                $("#hiddenForm").prop("action", "GS1110.aspx")
            } else if (type == "2") {
                $("#hiddenForm").prop("action", "GS1120.aspx")
            } else if (type == "3") {
                $("#hiddenForm").prop("action", "GS1130.aspx")
            } else if (type == "4") {
                $("#hiddenForm").prop("action", "GS1140.aspx")
            }

            $("#hidSelectedCompID").val($("#hidCompID").val());
            $("#hidSelectedEmpID").val($("#hidEmpID").val());
            $("#hiddenForm").submit();
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="hiddenForm" method="post" action="GS1110.aspx" target="_blank" style="display:none;">
        <input type="hidden" id="hidSelectedCompID" name="CompID" />
        <input type="hidden" id="hidSelectedEmpID" name="EmpID" />
    </form>
    <form id="frmContent" runat="server">
         <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="left" width="100%">
                    <asp:Label ID="lblDeptID" runat="server" Text="部門：" Font-Names="微軟正黑體"></asp:Label>
                    <asp:Label ID="txtDeptID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                </td>
            </tr>
            <tr id="trGroup" runat="server">
                <td align="left" width="100%">
                    <asp:Label ID="lblGroup" runat="server" Text="群組：" Font-Names="微軟正黑體"></asp:Label>
                    <asp:RadioButton ID="rbnGroup3" GroupName="rbnGroupType" runat="server" Text="單位主管" AutoPostBack="true" Font-Names="微軟正黑體" />
                    <asp:RadioButton ID="rbnGroup2" GroupName="rbnGroupType" runat="server" Text="科主管" AutoPostBack="true" Font-Names="微軟正黑體" />
                    <asp:RadioButton ID="rbnGroup1" GroupName="rbnGroupType" runat="server" Text="非管理職" AutoPostBack="true" Font-Names="微軟正黑體" />   
                    <asp:HiddenField ID="hidGroupID" runat="server" />
                    <asp:Button ID = "btnChangeGroup" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>                                
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="font-family:@微軟正黑體">                                
                                <img border="0" src="../images/premium_support.png"><asp:Label ID="Label1" runat="server" Text="詳細資料" Font-Names="微軟正黑體"></asp:Label>
                                <img border="0" src="../images/application_view_list.png"><asp:Label ID="Label2" runat="server" Text="考核表綜合評量" Font-Names="微軟正黑體"></asp:Label>
                                <img border="0" src="../images/report_user.png"><asp:Label ID="Label3" runat="server" Text="奬懲資料" Font-Names="微軟正黑體"></asp:Label>
                                <img border="0" src="../images/chart_bar.png"><asp:Label ID="Label4" runat="server" Text="業績資料" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidCompID" runat="server" />
                                <asp:HiddenField ID="hidEmpID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" 
                                              DataKeyNames="CompID,EmpID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Names="微軟正黑體" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="考績">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle Width="10%" CssClass="td_detail" Height="15px" Font-Names="微軟正黑體" />
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="upGrade" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlGrade" runat="server" Font-Names="微軟正黑體" OnSelectedIndexChanged="ddlGrade_Changed" AutoPostBack="true" Width="70%"></asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlGrade" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GradeOrder" HeaderText="排序">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle Width="6%" CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="6%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NameN" HeaderText="姓名">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="8%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Position" HeaderText="職位" SortExpression="Position" >
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="15%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RankID" HeaderText="職等" SortExpression="RankID" >
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="3%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Y3Grade" HeaderText="">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="3%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Y2Grade" HeaderText="">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="3%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Y1Grade" HeaderText="">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Width="3%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="參考資料">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Height="15px" Width="10%" Font-Names="微軟正黑體" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnDetail" runat="server" CausesValidation="false" CommandName="Detail" ImageUrl="../images/premium_support.png" ToolTip="詳細資料"></asp:ImageButton>
                                                <asp:ImageButton ID="ibtnEvaluation" runat="server" CausesValidation="false" CommandName="Evaluation" ImageUrl="../images/application_view_list.png" ToolTip="考核表綜合評量"></asp:ImageButton>
                                                <asp:ImageButton ID="ibtnReward" runat="server" CausesValidation="false" CommandName="Reward" ImageUrl="../images/report_user.png" ToolTip="獎懲資料" Visible="false"></asp:ImageButton>
                                                <asp:ImageButton ID="ibtnPerformance" runat="server" CausesValidation="false" CommandName="Performance" ImageUrl="../images/chart_bar.png" ToolTip="業績資料" Visible="false"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Memo" HeaderText="備註">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Width="10%" Font-Names="微軟正黑體" />
                                        </asp:BoundField>         
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" Font-Names="微軟正黑體" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" Font-Names="微軟正黑體" />
                                    <AlternatingRowStyle CssClass="tr_oddline" Font-Names="微軟正黑體" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                                    <PagerStyle CssClass="GridView_PagerStyle" Font-Names="微軟正黑體" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
