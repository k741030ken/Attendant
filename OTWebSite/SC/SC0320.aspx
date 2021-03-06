<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0320.aspx.vb" Inherits="SC_SC0320" %>

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
                            <td width="5%"></td>
                            <td align="right">
                                <asp:Label ID="lblType" Font-Size="15px" runat="server" Text="代碼類別："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCode" Font-Size="15px" runat="server" Text="代碼："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtCode" runat="server"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDefine" Font-Size="15px" runat="server" Text="代碼說明："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtDefine" runat="server"></asp:TextBox>
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
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" DataKeyNames="Type,Code" AllowPaging="False" AutoGenerateColumns="False" CellPadding="2" Width="100%" PageSize="15">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TypeName" HeaderText="類別" ReadOnly="True" SortExpression="Type">
                                            <HeaderStyle Width="19%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Code" HeaderText="代碼" ReadOnly="True" SortExpression="Code" >
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Define" HeaderText="代碼說明" ReadOnly="True" SortExpression="Define" >
                                            <HeaderStyle Width="28%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="有效註記" SortExpression="ValidFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="8%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgValidFlag" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# iif(DataBinder.Eval(Container.DataItem, "ValidFlag")="1","true","false") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OrderSeq" HeaderText="排序" ReadOnly="True" SortExpression="OrderSeq" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Note" HeaderText="補充說明" ReadOnly="True" SortExpression="Note" >
                                            <HeaderStyle Width="18%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="left" />
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
