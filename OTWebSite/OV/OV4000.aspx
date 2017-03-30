<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4000.aspx.vb" Inherits="OV4000" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    </style>
    <script type="text/javascript">
        function funAction(Param) 
        {

            if (Param == 'btnActionC') 
            {
         
                if ((!hasSelectedRow('gvMain1')) && (!hasSelectedRow('gvMain')))
                {
                    alert("未選取資料列！");
                    return false;
                }
                else 
                {
                        if (!confirm('確定計薪收回此筆資料？'))
                        {
                            return false;
                            }
                }

                }

                if (Param == 'btnExecutes') {

                    if ((!hasSelectedRow('gvMain1')) && (!hasSelectedRow('gvMain'))) {
                        alert("未選取資料列！");
                        return false;
                    }
                    else {
                        if (!confirm('確定作廢此筆資料？')) {
                            return false;
                        }
                    }

                }

        }
       
		  
		

    </script>
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
                    <asp:Label ID="lblType" runat="server" Text="查詢表單類別："></asp:Label>
                </td>
                <td colspan="5">
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="預先申請" Value="bef"></asp:ListItem>
                        <asp:ListItem Text="事後申報" Value="after"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField ID="hiddenType" runat="server" />
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="lblOTCompName" runat="server" Text="公司:"></asp:Label>
                </td>
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
                                <asp:Label ID="Label19" runat="server" Text="單位類別：">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_td_header" style="width: 100%">
                                <asp:Label ID="Label20" runat="server" Text="部門："></asp:Label>
                            </td>
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
                    <asp:Label ID="lblOrganName" runat="server" Text="科別："></asp:Label>
                </td>
                <td class="table_td_content">
                    <asp:DropDownList ID="ddlOrganID" runat="server" Style="width: 231px;" AutoPostBack="true">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="style3">
                    <asp:Label ID="lblOTEmpID" runat="server" Text="員工編號："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTEmpID" runat="server" MaxLength="6">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="tbOTEmpID" FilterType="UppercaseLetters,Numbers">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <uc:ButtonQuerySelectUserID ID="ucQueryEmpID" runat="server" ButtonText="..." ButtonHint="選擇人員..."
                        WindowHeight="550" WindowWidth="500" />
                </td>
                <td class="style3">
                    <asp:Label runat="server" Text="員工姓名："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTEmpName" runat="server">
                    </asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Label ID="Label4" runat="server" Text="在職狀態："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlWorkStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="Label6" runat="server" Text="職等："></asp:Label>
                </td>
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
                    <asp:Label ID="Label10" runat="server" Text="職稱："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTitleIDMIN" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label11" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="ddlTitleIDMAX" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblTitleNotice" Font-Size="12px" runat="server" Text="職稱選擇請由小到大" ForeColor="Red"
                        Visible="FALSE"></asp:Label>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label12" runat="server" Text="性別:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSex" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="男" Value="1"></asp:ListItem>
                        <asp:ListItem Text="女" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="Label14" runat="server" Text="職位："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPositionID" runat="server" Style="width: 231px;">
                    </asp:DropDownList>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label16" runat="server" Text="工作性質："></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlWorkType" runat="server" Style="width: 231px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="labOvertimeDate" runat="server" Text="加班日期："></asp:Label>
                </td>
                <td class="tr_style">
                    <uc:ucCalender ID="txtOvertimeDateB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label ID="Label3" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtOvertimeDateE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
                <td class="table_td_header">
                    <asp:Label ID="lblOTStartTime" runat="server" Text="開始時間："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="OTStartTimeH" runat="server">
                    </asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Text="："></asp:Label>
                    <asp:DropDownList ID="OTStartTimeM" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="lblOTEndTime" runat="server" Text="結束時間："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="OTEndTimeH" runat="server">
                    </asp:DropDownList>
                    <asp:Label runat="server" Text="："></asp:Label>
                    <asp:DropDownList ID="OTEndTimeM" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%" class="tr_style"></td>
                <td class="style3">
                    <asp:Label runat="server" ID="lblOTTypeID" Text="加班類型："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlOTTypeID" runat="server">
                    </asp:DropDownList>
                </td>
                <!--   <td class="style3">
                    <asp:Label runat="server" Text="加班原因:" ID="lblOTReasonID"></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlOTReasonID" runat="server">
                    </asp:DropDownList>
                </td>
                -->
                <td class="style3">
                    <asp:Label ID="Label1" runat="server" Text="加班日期類型："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlHolidayOrNot" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="平日" Value="0"></asp:ListItem>
                        <asp:ListItem Text="假日" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="加班時段："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlTime" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="時段一" Value="1"></asp:ListItem>
                        <asp:ListItem Text="時段二" Value="2"></asp:ListItem>
                        <asp:ListItem Text="時段三" Value="3"></asp:ListItem>
                        <%-- <asp:ListItem Text="留守時段" Value="4"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
             <td width="2%"></td>
                <td class="style3">
                    <asp:Label runat="server" Text="表單狀態："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlOTStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    <asp:Label runat="server" Text="表單號碼："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTFormNO" runat="server" MaxLength="16">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="tbOTFormNO" FilterType="Numbers">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td class="style3">
                    <asp:Label runat="server" Text="計薪年月："></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTPayDate" runat="server" Style="width: 80px" AutoPostBack="true" MaxLength="6">
                    </asp:TextBox>
                          <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="tbOTPayDate" FilterType="Numbers">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="tbOTPayDateErrorMsg" runat="server" Text="(YYYYMM)" Style="padding-left: 10px;
                        color: Red" Visible="false"></asp:Label>
                    &nbsp;<asp:CheckBox ID="ckOTSalaryPaid" runat="server" ClientIDMode="AutoID" AutoPostBack="true"
                        Text="已計薪" />
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label ID="labTakeOfficeDate" runat="server" Text="到職日期："></asp:Label>
                </td>
                <td>
                    <uc:ucCalender ID="txtTakeOfficeDateB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtTakeOfficeDateE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
                <td class="table_td_header">
                    <asp:Label runat="server" Text="離職日期：" ID="LeaveOfficeDate"></asp:Label>
                </td>
                <td colspan="1">
                    <uc:ucCalender ID="txtLeaveOfficeDateB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtLeaveOfficeDateE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label9" runat="server" Text="拋轉狀態："></asp:Label>
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="ddlIsProcessDate" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="已拋轉" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未拋轉" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
             <td width="2%"></td>
                <td class="table_td_header">
                    <asp:Label runat="server" Text="核准日期：" ID="labDateOfApproval"></asp:Label>
                </td>
                <td>
                    <uc:ucCalender ID="txtDateOfApprovalB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label ID="Label7" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtDateOfApprovalE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
                <td class="table_td_header">
                    <asp:Label runat="server" Text="申請日期：" ID="labDateOfApplication" Font-Names="微軟正黑體"></asp:Label>
                </td>
                <td colspan="3">
                    <uc:ucCalender ID="txtDateOfApplicationB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtDateOfApplicationE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <div id="div_tb" runat="server">
            <table width="100%" class="tbl_Content">
                <tr>
                    <td style="width: 100%">
                        <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20"/>
                    </td>
                </tr>
                <tr>
                    <td  style="font-family: @微軟正黑體; width: 100%;">
                        <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="DeptID,DeptID1,OrganID,EmpID,Name,OTDate,OTTime,OTType,OTStatus,OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,OTTxnID"
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle CssClass="td_detail" Height="15px" />
                                    <HeaderStyle CssClass="td_header" Width="2%" />
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                            Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeptID" HeaderText="單位類別" ReadOnly="True" SortExpression="DeptID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DeptID1" HeaderText="部門" ReadOnly="True" SortExpression="DeptID1">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrganID" HeaderText="科別" ReadOnly="True" SortExpression="OrganID">
                                    <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpID" HeaderText="加班人員編號" ReadOnly="True" SortExpression="EmpID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="加班人姓名" ReadOnly="True" SortExpression="Name">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTDate" HeaderText="加班起訖日期" ReadOnly="True" SortExpression="OTDate">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTTime" HeaderText="加班起訖時間" ReadOnly="True" SortExpression="OTTime">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTType" HeaderText="加班類型" ReadOnly="True" SortExpression="OTType">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTStatus" HeaderText="表單狀態" ReadOnly="True" SortExpression="OTStatus">
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
                        <uc:PagerControl ID="pcMain1" runat="server" GridViewName="gvMain1"  PerPageRecord="20"/>
                    </td>
                </tr>
                <tr>
                    <td  style="font-family: @微軟正黑體; width: 100%;">
                        <asp:GridView ID="gvMain1" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="DeptID,DeptID1,OrganID,EmpID,Name,OTDate,OTTime,OTType,OTStatus,OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq, OTStartTime,OTEndTime,OTTxnID,OTSeqNo,MealFlag,MealTime,OTSalaryPaid,HolidayOrNot,Time_one,Time_two,Time_three,ToOverTimeFlag,FlowCaseID,OTFromAdvanceTxnId"
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle CssClass="td_detail" Height="15px" />
                                    <HeaderStyle CssClass="td_header" Width="2%" />
                                    <ItemTemplate>
                                        <asp:RadioButton ID="rdo_gvMain1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                            Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DeptID" HeaderText="單位類別" ReadOnly="True" SortExpression="DeptID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                     <asp:BoundField DataField="DeptID1" HeaderText="部門" ReadOnly="True" SortExpression="DeptID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrganID" HeaderText="科別" ReadOnly="True" SortExpression="OrganID">
                                    <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpID" HeaderText="加班人員編號" ReadOnly="True" SortExpression="EmpID">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="加班人姓名" ReadOnly="True" SortExpression="Name">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTDate" HeaderText="加班起訖日期" ReadOnly="True" SortExpression="OTDate">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTTime" HeaderText="加班起訖時間" ReadOnly="True" SortExpression="OTTime">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTType" HeaderText="加班類型" ReadOnly="True" SortExpression="OTType">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Time_one" HeaderText="時段一" ReadOnly="True">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Time_two" HeaderText="時段二" ReadOnly="True">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Time_three" HeaderText="時段三" ReadOnly="True">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <%--                                  <asp:BoundField DataField="Stay_Time" HeaderText="留守時數" ReadOnly="True" >
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="OTStatus" HeaderText="表單狀態" ReadOnly="True" SortExpression="OTStatus">
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
    </div>
    </form>
</body>
</html>
