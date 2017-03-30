<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC1000.aspx.vb" Inherits="SC_SC1000" %>

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
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow(''))
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
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left">
                                <asp:Label ID="lblCompRole" ForeColor="blue" Font-Size="15px" runat="server" Text="授權公司："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCompRoleName" runat="server" Font-Size="15px" Width="300px" CssClass="InputTextStyle_Thin"></asp:Label>
                                <asp:DropDownList ID="ddlCompRoleID" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td width="10%"></td>                            
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblGroupID" Font-Size="15px" runat="server" Text="授權群組："></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlGroupID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblQueryFlag" Font-Size="15px" runat="server" Text="查詢權限："></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlQueryFlag" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                    </table>
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
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" 
                                              DataKeyNames="CompRoleID,GroupID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <HeaderStyle CssClass="td_header" HorizontalAlign="Center" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="5%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompRoleID" HeaderText="授權公司代碼" ReadOnly="True" SortExpression="CompRoleID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompRoleName" HeaderText="授權公司名稱" ReadOnly="True" SortExpression="CompRoleName" >
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupID" HeaderText="群組代碼" ReadOnly="True" SortExpression="GroupID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" HorizontalAlign="Center" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupName" HeaderText="群組名稱" ReadOnly="True" SortExpression="GroupName" >
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="考績查詢" SortExpression="Query0">
                                            <HeaderStyle CssClass="td_header" Width="8%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image id="imgQuery0_1" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# IIF(Databinder.Eval(Container.DataItem, "Query0") = "1", "True", "False") %>' />
                                                <asp:Image id="imgQuery0_0" runat="server" ImageUrl="~/images/chkboxE.gif" Visible='<%# IIF(Databinder.Eval(Container.DataItem, "Query0") = "0", "True", "False") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="薪資查詢" SortExpression="Query1">
                                            <HeaderStyle CssClass="td_header" Width="8%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image id="imgQuery1_1" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# IIF(Databinder.Eval(Container.DataItem, "Query1") = "1", "True", "False") %>' />
                                                <asp:Image id="imgQuery1_0" runat="server" ImageUrl="~/images/chkboxE.gif" Visible='<%# IIF(Databinder.Eval(Container.DataItem, "Query1") = "0", "True", "False") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgCompName" >
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
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
        </table>
    </form>
</body>
</html>
