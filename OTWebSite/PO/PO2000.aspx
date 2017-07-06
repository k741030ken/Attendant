<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO2000.aspx.vb" Inherits="PO_PO2000" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>打卡記錄查詢--經管單位</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
        function gridClear() {
            var griditem = document.getElementById("__SelectedRowsgvMain");
            griditem.value = ""
            var rows = document.getElementById("gvMain").rows;
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                if ((parseInt(i) + 1) % 2 == 0)
                    row.style.backgroundColor = 'white'
                else
                    row.style.backgroundColor = '#e2e9fe';

            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table class="tbl_Content" style="width: 100%; height: 100%">
        <tr class="td_Edit" runat="server">
            <td align="left" colspan="2" width = "20%" class="td_Edit">
                <asp:Label ID="Label15" runat="server" Text="公司別" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" colspan="8" class="td_Edit">
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr id="Tr2" class="td_Edit" runat="server">
            <td align="left" colspan="2" class="td_Edit">
                <asp:Label ID="lblOrganization" runat="server" Text="組織別" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" colspan="8" class="td_Edit">
                <asp:DropDownList ID="ddlOrganization" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged" Font-Names="微軟正黑體">
                <asp:ListItem Value="Organ" Text="行政組織"></asp:ListItem>
                <asp:ListItem Value="FlowOrgan" Text="功能組織"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="Organ" runat="server" class="td_Edit">
            <td align="left" class="td_Edit" colspan="2">
                <asp:Label ID="lblOrganID" runat="server" Text="行政組織" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" class="td_Edit" colspan = "8">
                <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_Changed"></asp:DropDownList>
                <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptID_Changed"></asp:DropDownList>
                <asp:DropDownList ID="ddlOrganID" runat="server"></asp:DropDownList>
            </td>
         </tr>
         <tr id="FlowOrgan" runat="server" visible = "false">
            <td align="left" class="td_Edit" colspan = "2">
                <asp:Label ID="lblFlowOrganID" runat="server" Text="功能組織" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" class="td_Edit" colspan = "8">
                <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode40_Changed"></asp:DropDownList>
                <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode30_Changed"></asp:DropDownList>
                <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode20_Changed"></asp:DropDownList>
                <asp:DropDownList ID="ddlRoleCode10" runat="server"></asp:DropDownList>
            </td>
         </tr>
         <tr>
            <td align="left" class="td_Edit" colspan="2">
                <asp:Label ID="lblDate" runat="server" Text="*查詢日期" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td align="left" class="td_Edit" colspan="8">
                <uc:ucCalender ID="ucBeginDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ～ "></asp:Label>
                <uc:ucCalender ID="ucEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="lblTipMsg" runat="server" Text="(查詢區間最多1個月最近5年以內的資料)" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="td_Edit" colspan="2">
                <asp:Label ID="lblTimeBegin" runat="server" Text="查詢時間：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td align="left" class="td_Edit" colspan = "8">
                <asp:DropDownList ID="StartTimeH" runat="server" Width = "10%"></asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="StartTimeM" runat="server" Width = "10%"></asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text=" ～ "></asp:Label>
                <asp:DropDownList ID="EndTimeH" runat="server" Width = "10%"></asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="EndTimeM" runat="server" Width = "10%"></asp:DropDownList>
            </td>
        </tr>
        <tr id="Tr1" class="td_Edit" runat="server">
            <td align="left" class="td_Edit" colspan = "2">
                <asp:Label ID="lblquery" runat="server" Text="*查詢類別" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" class="td_Edit" width = "15%">
                <asp:Label ID="space8" runat="server" Text=" "></asp:Label>
                <asp:DropDownList ID="ddlType" runat="server" Font-Names="微軟正黑體" AutoPostBack ="true">
                        <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="未刷卡"></asp:ListItem>
                        <asp:ListItem Value="2" Text="上下班刷卡"></asp:ListItem>
                        <asp:ListItem Value="3" Text="逾正常工時"></asp:ListItem>
                        <asp:ListItem Value="4" Text="補刷卡"></asp:ListItem>
                        <asp:ListItem Value="5" Text="全部刷卡紀錄"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" class="td_Edit" width = "8%">
                <asp:Label ID="Label9" runat="server" Text="類型" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" class="td_Edit" width = "15%">
                <asp:Label ID="Label7" runat="server" Text=" "></asp:Label>
                <asp:DropDownList ID="ddlConfirmPunchFlag" runat="server" Font-Names="微軟正黑體">
                        <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="出勤打卡"></asp:ListItem>
                        <asp:ListItem Value="2" Text="退勤打卡"></asp:ListItem>
                        <asp:ListItem Value="3" Text="午休開始"></asp:ListItem>
                        <asp:ListItem Value="4" Text="午休結束"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" class="td_Edit" width = "8%">
                <asp:Label ID="Label11" runat="server" Text="狀態" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td align="left" class="td_Edit"  width = "15%">
                <asp:Label ID="Label12" runat="server" Text=" "></asp:Label>
                <asp:DropDownList ID="ddlConfirmStatus" runat="server" Font-Names="微軟正黑體">
                        <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="異常"></asp:ListItem>
                        <asp:ListItem Value="2" Text="送簽中"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="left" class="td_Edit" width = "8%">
                <asp:Label ID="Label13" runat="server" Text="原因" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="td_Edit" align="left">
                <asp:Label ID="Label14" runat="server" Text=" "></asp:Label>
                <asp:DropDownList ID="ddlRemedy_AbnormalFlag" Width="80%" runat="server" Font-Names="微軟正黑體">
                        <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="處理公務"></asp:ListItem>
                        <asp:ListItem Value="2" Text="非處理公務"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td runat="server" class="td_Edit" colspan="2">
                <asp:Label ID="Label8" runat="server" Text="員工編號" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td runat="server" class="td_Edit">
                <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" runat="server" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase" width = "50%"></asp:TextBox>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
            <td runat="server" class="td_Edit">
                <asp:Label ID="Label10" runat="server" Text="姓名" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td runat="server" class="td_Edit" colspan="6">
                <asp:TextBox ID="txtEmpName" runat="server" width = "13%"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td align="center" width="100%" colspan = "10">
                    <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AbnormalType,PunchDate,PunchTime,ConfirmPunchFlag,Source,OrganName,EmpID,EmpName,Remedy_AbnormalReasonCN,RotateFlag"
                            CssClass="GridViewStyle" CellPadding="2" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="序號" >
                                    <ItemTemplate>
                                        <asp:Label ID="GridNo" runat="server" 
                                            Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                                </asp:TemplateField>
            
                                <asp:BoundField HeaderText="狀態" DataField="AbnormalType" SortExpression="AbnormalType">
                                    <HeaderStyle Width="4%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="打卡日期" DataField="PunchDate" SortExpression="PunchDate">
                                    <HeaderStyle Width="10%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="時間" DataField="PunchTime" SortExpression="PunchTime">
                                    <HeaderStyle Width="10%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="類型" DataField="ConfirmPunchFlag" SortExpression="ConfirmPunchFlag">
                                    <HeaderStyle Width="10%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="來源" DataField="Source" SortExpression="Source">
                                    <HeaderStyle Width="9%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="打卡單位" DataField="OrganID" SortExpression="OrganID">
                                    <HeaderStyle Width="9%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="員工編號" DataField="EmpID" SortExpression="EmpID">
                                    <HeaderStyle Width="9%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="員工姓名" DataField="EmpName" SortExpression="EmpName">
                                    <HeaderStyle Width="9%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="原因" DataField="Remedy_AbnormalReasonCN" SortExpression="Remedy_AbnormalReasonCN">
                                    <HeaderStyle Width="9%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="輪班人員" DataField="RotateFlag" SortExpression="RotateFlag">
                                    <HeaderStyle Width="10%" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="tr_evenline" />
                            <AlternatingRowStyle CssClass="tr_oddline" />
                            <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                            <HeaderStyle ForeColor="DimGray"></HeaderStyle>
                            <PagerStyle CssClass="GridView_PagerStyle" />
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
