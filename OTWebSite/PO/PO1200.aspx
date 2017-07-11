<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO1200.aspx.vb" Inherits="PO_PO1200" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
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
        .td_padding
        {
            width: 3%;
        }
         input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="center" style="height: 30px;">
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%"
                    width="100%">
                    <tr>
                        <td class="td_padding"></td>
                        <td align="left" width="10%">
                            <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display: none" />
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" Width="200px"></asp:Label>
                        </td>
                        <td align="left" width="10%">
                            <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server"
                                Style="text-transform: uppercase" AutoPostBack="true"></asp:TextBox>
                            <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600"
                                ButtonText="..." />
                        </td>
                        <td align="left" width="10%">
                            <asp:Label ID="lblEmpName" Font-Size="15px" runat="server" Text="員工姓名："></asp:Label>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpName" runat="server" AutoPostBack="true"></asp:TextBox>
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
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" DataKeyNames="CompID,EmpID,NameN"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px"  Font-Names="微軟正黑體"/>
                                        <HeaderStyle CssClass="td_header" Width="3%"  Font-Names="微軟正黑體"/>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                                Text="明細"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Font-Names="微軟正黑體">
                                            <%#Container.DataItemIndex+1 %></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" Height="15px" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID"
                                        Visible="false">
                                        <HeaderStyle Width="3%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID">
                                        <HeaderStyle Width="3%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NameN" HeaderText="員工姓名" ReadOnly="True" SortExpression="NameN">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrgList" HeaderText="行政組織" ReadOnly="True" SortExpression="OrgList"
                                        Visible="false">
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrgFlowList" HeaderText="功能組織" ReadOnly="True" SortExpression="OrgFlowList"
                                        Visible="false">
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp"
                                        >
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID"
                                        >
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate"
                                        >
                                        <HeaderStyle Width="8%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="目前尚未設定特殊人員"  Font-Names="微軟正黑體"></asp:Label>
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
        <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidEmpID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="IsDoUpdate" runat="server"></asp:HiddenField>
    </table>
    </form>
</body>
</html>
