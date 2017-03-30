<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA3700.aspx.vb" Inherits="PA_PA3700" %>

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
    <style type="text/css">
        .style1
        {
            width: 123px;
        }
        .style2
        {
            width: 14%;
        }
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="3%"></td>
                            <td align="left" class="style1">
                                <asp:Label ID="lblBeforeWorkStatusType" Font-Size="15px" runat="server" Text="異動前任職分類："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlBeforeWorkStatusType" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-在職"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非在職"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" class="style2">
                                <asp:Label ID="lblBeforeWorkStatus" Font-Size="15px" runat="server" Text="異動前任職狀況："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBeforeWorkStatus" runat="server" Font-Size="12px" Width="115px"></asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" class="style1">
                                <asp:Label ID="lblReason" Font-Size="15px" runat="server" Text="可輸入異動原因："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlReason" runat="server" Font-Size="12px" Width="115px"></asp:DropDownList>
                            </td>
                            <td align="left" class="style2">
                                <asp:Label ID="lblAfterWorkStatusType" Font-Size="15px" runat="server" Text="異動後任職狀況："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAfterWorkStatusType" runat="server" Font-Size="12px" Width="115px"></asp:DropDownList>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="BeforeWorkStatusType" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="BeforeWorkStatusType" HeaderText="異動前任職分類代碼" ReadOnly="True" SortExpression="BeforeWorkStatusType" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BeforeWorkStatusTypeName" HeaderText="異動前任職分類名稱" ReadOnly="True" SortExpression="BeforeWorkStatusTypeName" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BeforeWorkStatus" HeaderText="異動前任職狀況代碼" ReadOnly="True" SortExpression="BeforeWorkStatus" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BeforeWorkStatusName" HeaderText="異動前任職狀況名稱" ReadOnly="True" SortExpression="BeforeWorkStatusName" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" HeaderText="可輸入異動原因代碼" ReadOnly="True" SortExpression="Reason" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ReasonName" HeaderText="可輸入異動原因名稱" ReadOnly="True" SortExpression="ReasonName" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AfterWorkStatusType" HeaderText="異動後任職狀況代碼" ReadOnly="True" SortExpression="AfterWorkStatusType" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AfterWorkStatusTypeName" HeaderText="異動後任職狀況名稱" ReadOnly="True" SortExpression="AfterWorkStatusTypeName" >
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
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
