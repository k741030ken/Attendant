<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR9100.aspx.vb" Inherits="HR_HR9100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case "btnActionJ":
                case "btnUpdate":
                case "btnDelete":
                case "btnRelease":
                    if (!hasSelectedRows(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
            }
            
            switch(Param)
            {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }

            switch (Param) {
                case "btnRelease":
                    if (!confirm('請確認是否放行？'))
                        return false;
                    break;
            }
        }
        
        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                try
                {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch(ex)
                {}
            }
        }       
        
       -->
    </script>
    <%--<style type="text/css">
        .style1
        {
            border: 1px solid #89b3f5;
            font-size: 12px;
            height: 29px;
        }
    </style>--%>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
     <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
    <%--<tr>
                <td align="center" width="100%">
                <table width="100%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblCompID" runat="server" ForeColor="blue" Text="公司代碼"></asp:Label>
                                <asp:CheckBox ID="CheckBox2" runat="server" />
                </td>
                <td class="td_Edit" style="width: 35%" align="left">
                                <asp:Label ID="lblCompName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                </td>
                <td class="td_EditHeader" width="15%" align="center">
                    <asp:CheckBox ID="CheckBox3" runat="server" />
                <asp:CheckBox ID="CheckBox1" runat="server" />
                <td>
                    123ID="lblReason" runat="server" ForeColor="blue" Text="異動原因"></asp:Label>
                </td>
                <td class="td_Edit" style="width: 35%" align="left">
                                <asp:DropDownList ID="ddlReason" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                </td>
                </table>
                </td>
            </tr>--%>
             <tr>
                <td align="center" width="100%">
    <%--<table style="width:100%;">--%>
    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
        <tr>
            <td width="15%" align="center">
                <asp:Label ID="lblCompID" ForeColor="blue" Font-Size="15px" runat="server" Text="公司代碼"></asp:Label>
            </td>
            <td width="15%" align="left">
                <asp:Label ID="lblCompRoleID" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>                
            </td>
            <td width="15%" align="center">
                <asp:CheckBox ID="chkReason" Font-Size="15px" runat="server" Text="異動原因"/>
            </td>
            <td width="15%" align="left">
                <asp:DropDownList ID="ddlReason" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="15%" align="center">
                <asp:CheckBox ID="chkEmpID" Font-Size="15px" runat="server" Text="員工編號"/>
            </td>
            <td width="15%" align="left">
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server"></asp:TextBox>
            </td>
            <td width="15%" align="center">
                <asp:CheckBox ID="chkName" Font-Size="15px" runat="server" Text="員工姓名"/>
            </td>
            <td width="15%" align="left">
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtName" MaxLength="15" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="15%" align="center">
                <asp:CheckBox ID="chkApplyDate" Font-Size="15px" runat="server" Text="申請日期"/>
            </td>
             <td align="left" colspan ="2">
                <asp:TextBox ID="txtApplyDateB" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="imgApplyDateB" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                <ajaxToolkit:CalendarExtender ID="CalendarApplyDateB" runat="server" TargetControlID="txtApplyDateB" PopupButtonID="imgApplyDateB" Format="yyyy/MM/dd" />
                <asp:Label ID="Label1" ForeColor="blue" runat="server" Text="～"></asp:Label>
                <asp:TextBox ID="txtApplyDateE" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                <asp:ImageButton runat="Server" ID="imgApplyDateE" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                <ajaxToolkit:CalendarExtender ID="CalendarApplyDateE" runat="server" TargetControlID="txtApplyDateE" PopupButtonID="imgApplyDateE" Format="yyyy/MM/dd" />
            </td>
        </tr>
    </table>
                </td>
            </tr>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,EmpID,RelativeIDNo,ApplyDate,Reason,OldData,ApplyDate1" Width="100%" PageSize="15" 
                                onrowdatabound="gvMain_RowDataBound" onrowcommand="gvMain_RowCommand" onrowupdating="gvMain_RowUpdating"  >
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <HeaderTemplate>
                                                <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" HeaderText="全選" runat="server"  />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>    
                                        <asp:TemplateField HeaderText="動作" ShowHeader="False">
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="ibnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細" ToolTip="明細" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                                    <asp:Button ID="ibnRelative" runat="server" CausesValidation="false" CommandName="Update" Text="眷屬" ToolTip="眷屬" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />--%>
                                                    <asp:LinkButton ID="ibnDetail" CommandName="Detail" Font-Size="12px" runat="server">明細</asp:LinkButton><br>
                                                    <asp:LinkButton ID="ibnRelative" CommandName="Update" Font-Size="12px" runat="server">眷屬</asp:LinkButton>                                            
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="td_header" Width="3%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ApplyDate1" HeaderText="申請日期" ReadOnly="True" SortExpression="ApplyDate1">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>                                            
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID">
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="NameN" HeaderText="姓名" ReadOnly="True" SortExpression="NameN" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="RelativeIDNo" HeaderText="眷屬身份證字號" ReadOnly="True" SortExpression="RelativeIDNo" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApplyDate" HeaderText="申請日期" ReadOnly="True" SortExpression="ApplyDate">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" HeaderText="異動原因代碼" ReadOnly="True" SortExpression="Reason" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReasonName" HeaderText="異動原因" ReadOnly="True" SortExpression="ReasonName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NewData" HeaderText="變更後" SortExpression="NewData" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="OldData" HeaderText="變更前" SortExpression="OldData" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="Remark" HeaderText="備註" SortExpression="Remark" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark1" HeaderText="拆字姓名" SortExpression="Remark1" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp">
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID">
                                            <HeaderStyle Width="6%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate">
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
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
                                    <PagerSettings Position="Top" />
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
