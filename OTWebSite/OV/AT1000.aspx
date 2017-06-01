<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AT1000.aspx.vb" Inherits="AT_AT1000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript" src="../ClientFun/DisableRightClick.js"></script>
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
                    else {
                        if (Param == "btnDelete") {
                            if (!confirm('確定刪除此筆資料？'))
                                return false;
                        }
                    }
            }
        }

        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex) {
                    console.log(ex.message);
                }
            }
        }
        
    -->
    </script>
    <style type="text/css">
        .style1
        {
            width: 20%;
        }
        input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="left" style="height: 30px;">
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%"
                    width="100%">
                    <tr>
                       <td width="10%"></td>
                        <td align="left" class="style1" style="display: none">
                            公司代碼：
                            <asp:Label ID="lblCompName" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            代碼類別：
                            <asp:DropDownList ID="ddlTabFldName" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="30%">
                            代碼：
                            <asp:DropDownList ID="ddlCode" runat="server" Font-Names="微軟正黑體">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                       <td width="10%"></td>
                        <td class="style1" style="display: none">
                        </td>
                        <td>
                            <asp:HiddenField ID="hidTabName" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidFldName" runat="server"></asp:HiddenField>
                        </td>
                        <td>
                            <asp:HiddenField ID="hidCode" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" width="100%">
                <table width="100%" height="100%" class="tbl_Content">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td  style="font-family: @微軟正黑體; width: 100%;">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="TabName,FldName,Code"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  Font-Names="微軟正黑體" />
                                        <HeaderStyle CssClass="td_header" Width="3%"  Font-Names="微軟正黑體"/>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                        <HeaderStyle CssClass="td_header"  Font-Names="微軟正黑體" />
                                        <ItemStyle CssClass="td_detail" Font-Size="12px" Width="5%" HorizontalAlign="center"   Font-Names="微軟正黑體"/>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                                Text="明細"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"
                                                Font-Names="微軟正黑體"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" Height="15px" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FldName" HeaderText="代碼類別" ReadOnly="True" SortExpression="FldName"
                                        Visible="false">
                                        <HeaderStyle Width="15%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodeName2" HeaderText="代碼類別名稱" ReadOnly="True" SortExpression="CodeName2">
                                        <HeaderStyle Width="15%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Code" HeaderText="代碼" ReadOnly="True" SortExpression="Code">
                                        <HeaderStyle Width="3%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodeCName" HeaderText="代碼名稱" ReadOnly="True" SortExpression="CodeCName">
                                        <HeaderStyle Width="15%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SortFld" HeaderText="排列順序" ReadOnly="True" SortExpression="SortFld">
                                        <HeaderStyle Width="3%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NotShowFlag" HeaderText="不顯示註記" ReadOnly="True" SortExpression="不顯示註記">
                                        <HeaderStyle Width="4%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp">
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID">
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate">
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"   Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"  Font-Names="微軟正黑體"></asp:Label>
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
