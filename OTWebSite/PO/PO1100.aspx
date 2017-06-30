<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO1100.aspx.vb" Inherits="PO_PO1100" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>打卡特殊單位參數設定</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow('')) {
                        alert("未選取資料列！");
                        return false;
                    }
                    else {
                        if (Param == "btnDelete") {
                            if (!confirm('確定刪除此筆資料？'))
                                return false;
                        }
                    }
            }
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="CompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblOrgID" Font-Size="15px" runat="server" Text="單位代碼："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlDeptID" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>                                        
                                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td width="5%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,CompName,DeptID,DeptName,OrganID,OrganName,SpecialFlag,SpecialFlagName,LastChgComp,LastChgCompName,LastChgCompID_Name,LastChgID,LastChgName,LastChgID_Name,LastChgDate"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="2%" />
                                           <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompID" HeaderText="公司代號" ReadOnly="True" SortExpression="CompID">
                                        <HeaderStyle Width="6%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>     
                                    <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="DeptID" HeaderText="部門代號" ReadOnly="True" SortExpression="DeptID">
                                        <HeaderStyle Width="6%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="部門名稱" ReadOnly="True" SortExpression="DeptName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrganID" HeaderText="科組課代號" ReadOnly="True" SortExpression="OrganID">
                                        <HeaderStyle Width="6%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrganName" HeaderText="科組課名稱" ReadOnly="True" SortExpression="OrganName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SpecialFlagName" HeaderText="特殊單位" ReadOnly="True" SortExpression="SpecialFlagName">
                                        <HeaderStyle Width="6%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgCompID_Name" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgCompID_Name" >
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgID_Name" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID_Name" >
                                        <HeaderStyle Width="12%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                        <HeaderStyle Width="14%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="tr_evenline"  Font-Names="微軟正黑體"/>
                                <AlternatingRowStyle CssClass="tr_oddline"  Font-Names="微軟正黑體"/>
                                <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff"  Font-Names="微軟正黑體"/>
                                <PagerStyle CssClass="GridView_PagerStyle"  Font-Names="微軟正黑體"/>
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
