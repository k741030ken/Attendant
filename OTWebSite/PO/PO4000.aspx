<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO4000.aspx.vb" Inherits="PO_PO4000" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>打卡異常控管維護</title>
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
        <tr runat="server">
            <td width="20%" runat="server" class="td_Edit">
                <asp:Label ID="Label1" runat="server" Text="公司別" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" runat="server" class="td_Edit" colspan = "5">
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr runat="server"> 
            <td width="20%" class="td_Edit">
                <asp:Label ID="lblOrganization" runat="server" Text="組織別" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="td_Edit" colspan = "5">
                <asp:DropDownList ID="ddlOrganization" AutoPostBack="true" runat="server" Font-Names="微軟正黑體">
                    <asp:ListItem Text="行政組織" Value="Organ"></asp:ListItem>
                    <asp:ListItem Text="功能組織" Value="FlowOrgan"></asp:ListItem>
                </asp:DropDownList>                                        
            </td>
        </tr>
        <tr id="Organ" runat="server">
                        <td width="20%" align="left" class="td_Edit">
                         <asp:Label ID="lblOrganID" runat="server" Text="行政組織" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td width="30%" align="left" class="td_Edit" colspan = "5">
                         <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptID_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlOrganID" runat="server"></asp:DropDownList>
                        </td>
         </tr>
         <tr id="FlowOrgan" runat="server" visible = "false">
                        <td width="20%" align="left" class="td_Edit">
                         <asp:Label ID="lblFlowOrganID" runat="server" Text="功能組織" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td width="30%" align="left" class="td_Edit" colspan = "5">
                         <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode40_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode30_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode20_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode10" runat="server"></asp:DropDownList>
                        </td>
         </tr>
         <tr>
            <td width="20%" class="td_Edit">
                <asp:Label ID="lblDate" runat="server" Text="*查詢日期" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="80%" class="td_Edit" colspan="5">
                <uc:ucCalender ID="ucBeginDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ～ "></asp:Label>
                <uc:ucCalender ID="ucEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="lblTipMsg" runat="server" Text="(查詢區間最多1個月最近5年以內的資料)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="td_Edit">
                <asp:Label ID="lblTimeBegin" runat="server" Text="查詢時間：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="80%" class="td_Edit" colspan = "5">
                <asp:DropDownList ID="StartTimeH" runat="server" Width = "13%"></asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="StartTimeM" runat="server" Width = "13%"></asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Text=" ～ "></asp:Label>
                <asp:DropDownList ID="EndTimeH" runat="server" Width = "13%"></asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="EndTimeM" runat="server" Width = "13%"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td runat="server" class="td_Edit">
                <asp:Label ID="Label5" runat="server" Text="類型" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td runat="server" class="td_Edit" width = "15%">
                <asp:DropDownList ID="ddlConfirmPunchFlag" runat="server">
                    <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                    <asp:ListItem Text="出勤打卡" Value="1"></asp:ListItem>
                    <asp:ListItem Text="退勤打卡" Value="2"></asp:ListItem>
                    <asp:ListItem Text="午休開始" Value="3"></asp:ListItem>
                    <asp:ListItem Text="午休結束" Value="4"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td runat="server" class="td_Edit" width = "15%">
                <asp:Label ID="Label8" runat="server" Text="員工編號" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td runat="server" class="td_Edit" width = "15%">
                <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" runat="server" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase" width = "70%"></asp:TextBox>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
            <td runat="server" class="td_Edit" width = "15%">
                <asp:Label ID="Label10" runat="server" Text="姓名" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td runat="server" class="td_Edit" width = "15%">
                <asp:TextBox ID="txtEmpName" runat="server" width = "70%"></asp:TextBox>
            </td>
        </tr>
        <tr>
             <td align="center" width="100%" colspan = "6">
                    <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="CompID,DutyDate,PunchConfirmSeq,AbnormalType,PunchDate,PunchTime,ConfirmPunchFlag,OrganID,EmpID,EmpName,Remedy_AbnormalReasonCN,ConfirmStatus,ConfirmStatusCN"
                            CssClass="GridViewStyle" CellPadding="2" 
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle CssClass="td_detail" Height="15px" />
                                    <HeaderStyle CssClass="td_header" Width="2%" />
                                    <HeaderTemplate>
                                        <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" HeaderText="全選"
                                            runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_gvMain" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AbnormalType" HeaderText="狀態" ReadOnly="True" SortExpression="AbnormalType" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PunchDate" HeaderText="打卡日期" ReadOnly="True" SortExpression="PunchDate" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PunchTime" HeaderText="時間" ReadOnly="True" SortExpression="PunchTime" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ConfirmPunchFlag" HeaderText="類型" ReadOnly="True" SortExpression="ConfirmPunchFlag" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="OrganID" HeaderText="打卡單位" ReadOnly="True" SortExpression="OrganID">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpID" HeaderText="員編" ReadOnly="True" SortExpression="EmpID" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpName" HeaderText="姓名" ReadOnly="True" SortExpression="EmpName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remedy_AbnormalReasonCN" HeaderText="原因" ReadOnly="True" SortExpression="Remedy_AbnormalReasonCN" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ConfirmStatusCN" HeaderText="異常控管" ReadOnly="True" SortExpression="ConfirmStatus" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
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
