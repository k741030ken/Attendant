<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4300.aspx.vb" Inherits="OV_OV4300" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>HR薪資拋轉</title>
   <style type="text/css">
         .table1
        {
            font-size: 14px;
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
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width:110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width:110px;
        }--%>

    </style>
    <script type="text/javascript">
        function ActionVisable() {
        var btnAdd = window.parent.frames[0].document.getElementById("ucButtonPermission_btnAdd");
        var btnQuery = window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery");
        var btnDownload = window.parent.frames[0].document.getElementById("ucButtonPermission_btnDownload");

        var ddlist = document.getElementById("ddlPersonOrDate").value
        
        if (ddlist == "DateBlock") {
            //拋轉
            btnAdd.disabled = false
            btnAdd.style.color = '#000000'
            //查詢
            btnQuery.disabled = true
            btnQuery.style.color = '#FF0088'
//            //匯出
//            btnDownload.disabled = true
//            btnDownload.style.color = '#FF0088'
        }else if (ddlist == "PersonBlock") {
            //拋轉
            btnAdd.disabled = false
            btnAdd.style.color = '#000000'
            //查詢
            btnQuery.disabled = false
            btnQuery.style.color = '#000000'
//            //匯出
//            btnDownload.disabled = true
//            btnDownload.style.color = '#FF0088'
        }else if (ddlist == "") {
            //拋轉
            btnAdd.disabled = false
            btnAdd.style.color = '#000000'
            //查詢
            btnQuery.disabled = false
            btnQuery.style.color = '#000000'
            //匯出
//            btnDownload.disabled = false
//            btnDownload.style.color = '#000000'
        }
    }
    function btnClear() {
        var btnAdd = window.parent.frames[0].document.getElementById("ucButtonPermission_btnAdd");
        var btnQuery = window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery");
//        var btnDownload = window.parent.frames[0].document.getElementById("ucButtonPermission_btnDownload");

        btnAdd.disabled = false
        btnAdd.style.color = '#000000'
        //查詢
        btnQuery.disabled = false
        btnQuery.style.color = '#000000'
//        //匯出
//        btnDownload.disabled = false
//        btnDownload.style.color = '#000000'
    }
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
 </asp:ToolkitScriptManager>
    <table style="width: 100%" class="tbl_Condition table1">
        <tr>
            <td width="20%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="*拋轉範圍：" Font-Names="微軟正黑體" ForeColor="blue" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:DropDownList ID="ddlPersonOrDate" runat="server" AutoPostBack="True" onchange="ActionVisable()">
                    <asp:ListItem Text="請選擇" Value=""></asp:ListItem>
                    <asp:ListItem Text="日期" Value="DateBlock"></asp:ListItem>
                    <asp:ListItem Text="人員" Value="PersonBlock"></asp:ListItem>
<%--                    <asp:ListItem Text="留守" Value="StayBlock"></asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id = "trDate" runat="server" visible="false">
        <td width="20%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="*核准日期：" Font-Names="微軟正黑體" ForeColor="blue" class="style3"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <uc:ucCalender ID="ucDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr id = "trEmp1" runat="server" visible="false">
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text="*拋轉人員：" Font-Names="微軟正黑體" ForeColor="blue" class="style3"></asp:Label>
            </td>
            <td class="style2">
            <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <%--<asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>--%>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" runat="server" Width="80px" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase"></asp:TextBox>
                <asp:Label ID="lblEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
        </tr>
        <tr id = "trEmp2" runat="server" visible="false">
            <td class="style1">
                <asp:Label ID="Label3" runat="server" Text=" 核准日期：" class="style3"></asp:Label>
            </td>
            <td class="style2">
            <uc:ucCalender ID="ucEmpDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                </td>
        </tr>
<%--        <tr id="trStay1" runat="server" visible="false">
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="核准日期"></asp:Label>
            </td>
            <td class="style2">
            <uc:ucCalender ID="ucStayDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                </td>
        </tr>
        <tr id = "trStay2" runat="server" visible="false">
            <td class="style1">
                <asp:Label ID="lblEmpID" runat="server" Text="寫入計薪年月"></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox ID="txtPayDate" runat="server" Width="10%"></asp:TextBox>
                <asp:Button ID="btnPayDate" runat="server" Text="計薪" />
                <asp:Label ID="Label5" runat="server" Text="(YYYYMM)"></asp:Label>
            </td>
           
        </tr>--%>
        <tr> <%--記得要更改Key值--%>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="PersonTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" align ="center">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="DeptName,OrganName,EmpID,EmpName,Date,Time,Reason,SeqNo,TxnID"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="4%" />
                                        <HeaderTemplate>
                                        <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_gvMain" runat="server" AutoPostBack = "true" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DeptName" HeaderText="單位類別/部門" ReadOnly="True" SortExpression="DeptName">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="OrganName" HeaderText="科別" HtmlEncode="false" ReadOnly="True" SortExpression="OrganName">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="加班人員編" ReadOnly="True" SortExpression="EmpID">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpName" HeaderText="加班人姓名" ReadOnly="True" SortExpression="EmpName">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date" HeaderText="加班日期" ReadOnly="True" SortExpression="Date">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Time" HeaderText="加班起迄時間" ReadOnly="True" SortExpression="Time">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Reason" HeaderText="加班類型" ReadOnly="True" SortExpression="Reason">
                                        <HeaderStyle Width="12%" CssClass="td_header" />
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
                                <PagerStyle CssClass="GridView_PagerStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain1" runat="server" GridViewName="gvMain1" PerPageRecord="20"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain1" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="TxnID,SeqNo,EmpID,StartTime,EndTime,MealFlag,MealTime,Date,Time_one,Time_two,Time_three,TimeAcc_one,TimeAcc_two,TimeAcc_three,Time_acc,HolidayOrNot,DayAcross,SalaryPaid"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:BoundField DataField="TxnID" HeaderText="單號" ReadOnly="True" SortExpression="TxnID">
                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail myMouse2" HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="SeqNo" HeaderText="單號序列" ReadOnly="True" SortExpression="SeqNo">
                                        <HeaderStyle Width="2%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="StartTime" HeaderText="開始時間" ReadOnly="True" SortExpression="StartTime">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="EndTime" HeaderText="結束時間" HtmlEncode="false" ReadOnly="True" SortExpression="EndTime">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MealFlag" HeaderText="用餐註記" ReadOnly="True" SortExpression="MealFlag">
                                        <HeaderStyle Width="2%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="MealTime" HeaderText="用餐時間" HtmlEncode="false" ReadOnly="True" SortExpression="MealTime">
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Date" HeaderText="日期" ReadOnly="True" SortExpression="Date">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Time_one" HeaderText="時段<一>" ReadOnly="True" SortExpression="Time_one">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Time_two" HeaderText="時段<二>" ReadOnly="True" SortExpression="Time_two">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Time_three" HeaderText="時段<三>" ReadOnly="True" SortExpression="Time_three">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TimeAcc_one" HeaderText="時段<一>累加" ReadOnly="True" SortExpression="TimeAcc_one">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TimeAcc_two" HeaderText="時段<二>累加" ReadOnly="True" SortExpression="TimeAcc_two">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TimeAcc_three" HeaderText="時段<三>累加" ReadOnly="True" SortExpression="TimeAcc_three">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Time_acc" HeaderText="累積時間" ReadOnly="True" SortExpression="Time_acc">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HolidayOrNot" HeaderText="假日註記" ReadOnly="True" SortExpression="HolidayOrNot">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DayAcross" HeaderText="跨日註記" ReadOnly="True" SortExpression="DayAcross">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SalaryPaid" HeaderText="計薪註記" ReadOnly="True" SortExpression="SalaryPaid">
                                        <HeaderStyle Width="6%" CssClass="td_header" />
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