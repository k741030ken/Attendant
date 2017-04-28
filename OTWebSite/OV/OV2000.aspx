<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV2000.aspx.vb" Inherits="OV2000" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .table1
        {
            background-color:#f3f8ff;
            width: 100px;
            font-size: 14px;
        }
        
        
        .table1_input
        {
            width: 100px;
        }
        
        .tr_style
        {
            height: 20px;
        }
        
        
        .bt_style
        {
            margin: 5px;
        }
        .table1 td
        {
            background-color:#f3f8ff;
            border: 1px solid #f3f8ff;
            padding: 0;
        }
        .table2 td
        {
            background-color:#f3f8ff;
            border-collapse: collapse;
            border: 0;
        }
        .table_td_content
        {
            width: 23.3%;
        }
       
        .table_td_header
        {
            text-align: left;
            width: 10%;
            background-color: #f3f8ff;
        }
        
        .style2
        {
            text-align: left;
            font-size: 14px;
            width: 100%;
            background-color: #f3f8ff;
            height: 20px;
        }
        
        .style3
        {
            text-align: left;
            font-size: 14px;
            width: 10%;
            background-color: #f3f8ff;
            height: 20px;
        }
        
        input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
        
        td_header_hidden
        {
            visibility: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <div style="width: 100%; height: 100%">
        <br />
        <table class="tbl_Condition" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
            font-family: @微軟正黑體;">
            <tr class="tr_style">
            <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="lblType" runat="server" Text="查詢類別"></asp:Label>
                    ：</td>
                <td colspan="5">
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="明細查詢" Value="detail"></asp:ListItem>
                        <asp:ListItem Text="統計查詢" Value="statistics"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hiddenType" runat="server" />
                </td>
            </tr>
            <tr class="tr_style">
            <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="lblOTCompName" runat="server" Text="公司"></asp:Label>
                    ：</td>
                <td class="table_td_content">
                    <asp:DropDownList ID="ddlCompID" runat="server" Font-Names="微軟正黑體">
                    </asp:DropDownList>
                    <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin"
                        Width="200px"></asp:Label>
                    <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display: none" />
                    <asp:Label ID="lblReleaseResult" runat="server" ForeColor="Blue" Text="" Style="display: none"></asp:Label>
                </td>
                <td >
                    <table style="width: 100%" class="table2" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="Label19" runat="server" Text="單位類別">
                                </asp:Label>
                                ：</td>
                        </tr>
                        <tr>
                            <td class="table_td_header" style="width: 100%">
                                <asp:Label ID="Label20" runat="server" Text="部門"></asp:Label>
                                ：</td>
                        </tr>
                    </table>
                </td>
                <td class="table_td_content">
                    <table style="width: 100%" class="table2">
                        <tr>
                            <td>
                                <!--<asp:UpdatePanel ID="UpdMain" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                             </asp:UpdatePanel>-->
                                <asp:DropDownList ID="ddlOrgType" runat="server" Style="width: 231px;" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlDeptID" runat="server" Style="width: 231px;" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="lblOrganName" runat="server" Text="科別"></asp:Label>
                    ：</td>
                <td class="table_td_content">
                    <asp:DropDownList ID="ddlOrganID" runat="server" Style="width: 231px;" AutoPostBack="true">
                    </asp:DropDownList>
                    <%-- <br />
                    <asp:CheckBox ID="chkOrganName" runat="server" ClientIDMode="AutoID" AutoPostBack="true"
                        Text="含無效組織" />--%>
                </td>
            </tr>
            <tr class="tr_style">
            <td width="2%"></td>
                <td class="style3">
                    <asp:Label ID="lblOTEmpID" runat="server" Text="員工編號"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTEmpID" MaxLength="6" runat="server">
                    </asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="tbOTEmpID_FilteredTextBoxExtender" 
                        runat="server" TargetControlID="tbOTEmpID" FilterType="UppercaseLetters,Numbers">
                    </asp:FilteredTextBoxExtender>
                    <uc:ButtonQuerySelectUserID ID="ucQueryEmpID" runat="server" ButtonText="..." ButtonHint="選擇人員..."
                        WindowHeight="550" WindowWidth="500" />
                </td>
                <td class="style3">
                    <asp:Label ID="Label1" runat="server" Text="員工姓名"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTEmpName" runat="server">
                    </asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="Label4" runat="server" Text="在職狀態"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlWorkStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
            <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="Label6" runat="server" Text="職等"></asp:Label>
                    ：</td>
                <td>
                    <asp:DropDownList ID="ddlRankIDMIN" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label8" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="ddlRankIDMAX" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblRankNotice" Font-Size="12px" runat="server" Text="職等選擇請由小到大" ForeColor="Red"
                        Visible="FALSE"></asp:Label>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label10" runat="server" Text="職稱"></asp:Label>
                    ：</td>
                <td>
                    <%--<asp:DropDownList ID="ddlTitleName" runat="server" Style="width: 231px;">
                    </asp:DropDownList>--%>

                    <asp:DropDownList ID="ddlTitleIDMIN" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label11" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="ddlTitleIDMAX" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblTitleNotice" Font-Size="12px" runat="server" Text="職稱選擇請由小到大" ForeColor="Red"
                    Visible="FALSE"></asp:Label>

                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label14" runat="server" Text="職位"></asp:Label>
                    ：</td>
                <td>
                    <asp:DropDownList ID="ddlPositionID" runat="server" Style="width: 231px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="2%"></td>
                <td class="style3">
               
                    <asp:Label ID="Label2" runat="server" Text="申請表單狀態"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlOTStatus" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="暫存" Value="1"></asp:ListItem>
                        <asp:ListItem Text="送簽" Value="2"></asp:ListItem>
                        <asp:ListItem Text="核准" Value="3"></asp:ListItem>
                        <asp:ListItem Text="駁回" Value="4"></asp:ListItem>
                        <asp:ListItem Text="刪除" Value="5"></asp:ListItem>
                        <asp:ListItem Text="取消" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:Label ID="Label3" runat="server" Text="表單號碼"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTFormNO" MaxLength="16" runat="server">
                    </asp:TextBox>
                    <asp:FilteredTextBoxExtender ID="tbOTFormNO_FilteredTextBoxExtender" 
                        runat="server" TargetControlID="tbOTFormNO" FilterType="Numbers">
                    </asp:FilteredTextBoxExtender>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="labOvertimeDate" runat="server" Text="加班日期"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <uc:ucCalender ID="txtOvertimeDateB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label ID="Label24" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtOvertimeDateE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
            </tr>
            <tr class="tr_style">
                 <td width="2%"></td>
             <td class="style3">
                    <asp:Label ID="Label7" runat="server" Text="申報表單狀態"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlOTStatus1" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="暫存" Value="1"></asp:ListItem>
                        <asp:ListItem Text="送簽" Value="2"></asp:ListItem>
                        <asp:ListItem Text="核准" Value="3"></asp:ListItem>
                        <asp:ListItem Text="駁回" Value="4"></asp:ListItem>
                        <asp:ListItem Text="刪除" Value="5"></asp:ListItem>
                        <asp:ListItem Text="取消" Value="6"></asp:ListItem>
                        <asp:ListItem Text="作廢" Value="7"></asp:ListItem>
                        <asp:ListItem Text="計薪後收回" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:Label ID="lblPayDate" runat="server" Text="計薪年月"></asp:Label>
                    ：</td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTPayDate" runat="server" MaxLength="6" Style="width: 80px" AutoPostBack="true">
                    </asp:TextBox>
                    <%--<asp:Label ID="tbOTPayDateErrorMsg" runat="server" Text="(YYYYMM)" Style="padding-left: 10px;
                        color: Red" Visible="false"></asp:Label>--%>
                    <asp:Label ID="Label9" runat="server" Text="(YYYYMM)"></asp:Label>
      <%--          &nbsp;<asp:CheckBox ID="ckOTSalaryPaid" runat="server" ClientIDMode="AutoID" AutoPostBack="true" Text="未計薪" />
--%>
                </td>
                <td class="style3">
                    <asp:Label ID="Label5" runat="server" Text="加班轉換方式"></asp:Label>
                    ：</td>
                <td class="tr_style">
                        <asp:DropDownList ID="ddlSalaryOrAdjust" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="轉薪資" Value="1"></asp:ListItem>
                        <asp:ListItem Text="轉補休" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div id="div_tb" runat="server">
            <table width="100%" class="tbl_Content">
                <tr>
                    <td style="width: 100%">
                        <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain"  PerPageRecord="20"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" style="font-family: @微軟正黑體;">
                        <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTCompID,EmpID,Name,OVADate,OVATime,OVAOTTypeName,OVAOTReasonMemo,OVAOTStatus,OVAOTTxnID,OVDDate,OVDTime,OVDOTTypeName,OVDOTReasonMemo,OVDOTStatus,OVDOTTxnID,OVDOTPayDate"
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體"
                            OnRowCreated="grvMergeHeader_RowCreated">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpID" HeaderText="加班人員編號" ReadOnly="True" SortExpression="EmpID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="加班人姓名" ReadOnly="True" SortExpression="Name">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="OVADetail" runat="server" CausesValidation="false" CommandName="OVADetail" Visible="false"
                                            Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="OVADate" HeaderText="加班起訖日期" ReadOnly="True" SortExpression="OVADate">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVATime" HeaderText="加班起訖時間" ReadOnly="True" SortExpression="OVATime">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVAOTTypeName" HeaderText="加班類型" ReadOnly="True" SortExpression="OVAOTTypeName">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVAOTReasonMemo" HeaderText="加班原因" ReadOnly="True" SortExpression="OVAOTReasonMemo">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVAOTStatus" HeaderText="表單狀態" ReadOnly="True" SortExpression="OVAOTStatus">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="OVDDetail" runat="server" CausesValidation="false" CommandName="OVDDetail" Visible="false"
                                            Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="OVDDate" HeaderText="加班起訖日期" ReadOnly="True" SortExpression="OVDDate">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDTime" HeaderText="加班起訖時間" ReadOnly="True" SortExpression="OVDTime">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDOTTypeName" HeaderText="加班類型" ReadOnly="True" SortExpression="OVDOTTypeName">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDOTReasonMemo" HeaderText="加班原因" ReadOnly="True" SortExpression="OVDOTReasonMemo">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDOTStatus" HeaderText="表單狀態" ReadOnly="True" SortExpression="OVDOTStatus">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="tr_evenline" Font-Names="微軟正黑體" />
                            <AlternatingRowStyle CssClass="tr_oddline" Font-Names="微軟正黑體" />
                            <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" Font-Names="微軟正黑體" />
                            <PagerStyle CssClass="GridView_PagerStyle" Font-Names="微軟正黑體" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>

        <div id="div_tb1" runat="server">
            <table width="100%" class="tbl_Content">
                <tr>
                    <td style="width: 100%">
                        <uc:PagerControl ID="pcMain1" runat="server" GridViewName="gvMain1"   PerPageRecord="20"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" style="font-family: @微軟正黑體;">
                        <asp:GridView ID="gvMain1" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTCompID,EmpID,Name,OVAStatusTimeFor2,OVAStatusTimeFor3,OVAStatusTimeFor4,OVDStatusTimeFor2,OVDStatusTimeFor3,OVDStatusTimeFor4"
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體"
                            OnRowCreated="grvMergeHeader_RowCreated1">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmpID" HeaderText="加班人員編號" ReadOnly="True" SortExpression="EmpID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="加班人姓名" ReadOnly="True" SortExpression="Name">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>

                                <asp:BoundField DataField="OVAStatusTimeFor2"  HeaderText="送簽" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVAStatusTimeFor3"  HeaderText="核准" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVAStatusTimeFor4"  HeaderText="駁回" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDStatusTimeFor2"  HeaderText="送簽" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDStatusTimeFor3"  HeaderText="核准" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OVDStatusTimeFor4"  HeaderText="駁回" ShowHeader="False">
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="tr_evenline" Font-Names="微軟正黑體" />
                            <AlternatingRowStyle CssClass="tr_oddline" Font-Names="微軟正黑體" />
                            <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" Font-Names="微軟正黑體" />
                            <PagerStyle CssClass="GridView_PagerStyle" Font-Names="微軟正黑體" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
   
    </div>
    </form>
</body>
</html>
