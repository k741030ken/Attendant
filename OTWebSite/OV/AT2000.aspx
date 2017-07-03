<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AT2000.aspx.vb" Inherits="OV_AT2000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow('')) {
                        alert("未選取資料列！");
                        return false;
                    }
            }

            switch (Param) {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblOrgID" Font-Size="15px" runat="server" Text="單位代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlDeptID" AutoPostBack="true" runat="server" Font-Names="微軟正黑體"></asp:DropDownList>                                        
                                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體"></asp:DropDownList>
                            </td>
                            <td width="5%"></td>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,OrganID,DeptID,BranchFlag" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" Font-Names="微軟正黑體"/>
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="6%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeptID" HeaderText="部門代碼" ReadOnly="True" SortExpression="DeptID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeptName" HeaderText="部門名稱" ReadOnly="True" SortExpression="DeptName" >
                                            <HeaderStyle Width="14%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganID" HeaderText="科組課代碼" ReadOnly="True" SortExpression="OrganID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganName" HeaderText="科組課名稱" ReadOnly="True" SortExpression="OrganName" >
                                            <HeaderStyle Width="14%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BranchFlag" HeaderText="分行註記" ReadOnly="True" SortExpression="BranchFlag" >
                                            <HeaderStyle Width="6%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="12%" CssClass="td_header" Font-Names="微軟正黑體"/>
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體"/>
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
