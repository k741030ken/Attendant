<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1160.aspx.vb" Inherits="GS_GS1160" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td colspan="2" style="padding-bottom:5px;">
                    <asp:Label ID="txtDeptID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-bottom:5px;width: 30%">
                    <asp:Label ID="txtGroup" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>    
                </td> 
                <td style="padding-bottom:5px;width: 70%">
                    <asp:CheckBox ID="chkShowComment" Text="僅顯示必填名單" Font-Names="微軟正黑體" Checked="true" runat="server" AutoPostBack="true" />
                </td>
            </tr>                  
            <tr>
                <td colspan="2">            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" 
                                              DataKeyNames="CompID,ApplyID,EmpID,IsExit,Grade1,Grade2" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Names="微軟正黑體" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="編輯">
                                            <HeaderStyle CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Height="15px" Width="5%" Font-Names="微軟正黑體" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibtnEdit" runat="server" CausesValidation="false" CommandName="btnEdit" ImageUrl="../images/edit.gif" ToolTip="修改"></asp:ImageButton>
                                                <asp:ImageButton ID="ibtnSave" runat="server" CausesValidation="false" CommandName="btnSave" ImageUrl="../images/page_save.png" ToolTip="儲存" Visible="false"></asp:ImageButton>
                                                <asp:ImageButton ID="ibtnCancel" runat="server" CausesValidation="false" CommandName="btnCancel" ImageUrl="../images/cancel.png" ToolTip="取消" Visible="false"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號">
                                            <HeaderStyle Width="6%" CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NameN" HeaderText="姓名">
                                            <HeaderStyle Width="8%" CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GradeOrder" HeaderText="排序">
                                            <HeaderStyle Width="4%" CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade1" HeaderText="整體評量考核結果<br/>(考核表)" HtmlEncode="false">
                                            <HeaderStyle Width="10%" CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade2" HeaderText="排序整體評量">
                                            <HeaderStyle Width="10%" CssClass="td_header" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="考績補充說明或調整原因">
                                            <ItemStyle CssClass="td_detail" Height="15px" Font-Names="微軟正黑體" />
                                            <HeaderStyle CssClass="td_header" Width="60%" VerticalAlign="Middle" Font-Names="微軟正黑體" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblComment" Text='<%# Databinder.Eval(Container.DataItem, "Comment") %>' runat="server" Font-Names="微軟正黑體"></asp:Label>
                                                <asp:TextBox ID="txtComment" runat="server" Text='<%# Databinder.Eval(Container.DataItem, "Comment") %>' CssClass="InputTextStyle_Thin" Width="800" MaxLength="100" Visible="false" Font-Names="微軟正黑體"></asp:TextBox>
                                            </ItemTemplate> 
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" Font-Names="微軟正黑體" />
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
