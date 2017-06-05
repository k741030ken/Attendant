<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV8300.aspx.vb" Inherits="OV_OV8300" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <style type="text/css">   
        .table1
        {
            border-collapse: collapse;
            border: 1px solid #89b3f5;
            background-color: #f3f8ff;
        }      
        .style3
        {
            margin-left: 25%;
            
         }      
<%--        .style1
        {
            font-size: 14px;
            font-family:  Calibri, 微軟正黑體;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width: 110px;
        }
        .style2
        {
            font-size: 14px;
            font-family:  Calibri, 微軟正黑體;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width: 110px;
        }--%>
        .my_detail
        {
	        border-width: 1px;
	        border-style: solid;
	        border-color: #89b3f5;
	        font-size: 14px;
	        font-family: Calibri, 新細明體;
	        height:30px;
        }
    </style>
    <script type="text/javascript">
//    <!--
//        function funAction(Param) {
//            switch (Param) {
//                case "btnUpdate":
//                    if (!hasSelectedRow('')) {
//                        alert("未選取資料列！");
//                        return false;
//                    }
//            }
//        }
//       -->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="tbl_Condition">
         <tr>
            <td width="20%" >
                <asp:Label ID="Label5" runat="server" Text="公司代碼：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="表單名稱：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:DropDownList ID="ddlFormName" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                    <asp:ListItem Text="加班預先申請" Value="ddlAfter"></asp:ListItem>
                    <asp:ListItem Text="加班事後申報" Value="ddlBefore"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblEmpID" runat="server" Text="表單加班人：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td class="style2">
                <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtFormEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <%--<asp:TextBox CssClass="InputTextStyle_Thin" ID="txtFormEmpID" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>--%>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtFormEmpID" runat="server" Width="80px" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase"></asp:TextBox>
                <asp:Label ID="lblFormEmpID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp1" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="表單目前處理人員：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td class="style2">
                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAdjustEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtAdjustEmpID" runat="server" Width="80px" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase"></asp:TextBox>
                <%--<asp:TextBox CssClass="InputTextStyle_Thin" ID="txtAdjustEmpID" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>--%>
                <asp:Label ID="lblAdjustEmpID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp2" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
        </tr>
        <tr>
        <td width="20%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="表單申請日期：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <uc:ucCalender ID="ucAppSDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="ucAppEDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr>
        <td width="20%" class="style1">
                <asp:Label ID="Label2" runat="server" Text="加班日期：" Font-Names="微軟正黑體" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <uc:ucCalender ID="ucAddSDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label6" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="ucAddEDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="FormName,AddEmpName,AdjustEmpName,StartDate,EndDate,StartTime,EndTime,LastChgComp,LastChgID,LastChgDate"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="4%" />
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FormName" HeaderText="表單名稱" ReadOnly="True" SortExpression="FormName">
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center"/>
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="AddEmpName" HeaderText="加班人"  ReadOnly="True" SortExpression="AddEmpName">
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AdjustEmpName" HeaderText="目前處理人員" ReadOnly="True" SortExpression="AdjustEmpName">
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="加班開始日期" ReadOnly="True" SortExpression="StartDate">
                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndDate" HeaderText="加班結束日期" ReadOnly="True" SortExpression="EndDate">
                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="StartTime" HeaderText="加班開始時間" ReadOnly="True" SortExpression="StartTime">
                                        <HeaderStyle Width="11%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EndTime" HeaderText="加班結束時間" ReadOnly="True" SortExpression="EndTime">
                                        <HeaderStyle Width="11%" CssClass="td_header" />
                                        <ItemStyle CssClass="my_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgCompName" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgCompID" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgName" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgEmpID" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="tr_evenline" />
                                <AlternatingRowStyle CssClass="tr_oddline" />
                                <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
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
