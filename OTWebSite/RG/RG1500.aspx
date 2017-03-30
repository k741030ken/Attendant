<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RG1500.aspx.vb" Inherits="RG_RG1500" %>

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
                    if (!hasSelectedRow(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
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
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblContractDate" Font-Size="15px" runat="server" Text="預計報到日："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox ID="txtContractDateB" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10" Width="80" AutoPostBack="true" OnTextChanged="ContractDate_TextChanged"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="imgContractDateB" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarContractDateB" runat="server" TargetControlID="txtContractDateB" PopupButtonID="imgContractDateB" Format="yyyy/MM/dd"  />
                                <asp:Label ID="lblContractDateText" runat="server" Text="～"></asp:Label>
                                <asp:TextBox ID="txtContractDateE" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10" Width="80" AutoPostBack="true" OnTextChanged="ContractDate_TextChanged"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="imgContractDateE" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarContractDateE" runat="server" TargetControlID="txtContractDateE" PopupButtonID="imgContractDateE" Format="yyyy/MM/dd" />
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblRecID" Font-Size="15px" runat="server" Text="應試者編號："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlRecID" runat="server" Font-Names="細明體"></asp:DropDownList>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" 
                                              CellPadding="2" DataKeyNames="CompID,Name,RecID,CheckInDate" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="4%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RecID" HeaderText="應試者編號" ReadOnly="True" SortExpression="RecID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="姓名" ReadOnly="True" SortExpression="Name" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="不報到註記" SortExpression="CheckInFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="8%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgCheckInFlag_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "CheckInFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgCheckInFlag_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "CheckInFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="ContractDate" HeaderText="預計報到日" ReadOnly="True" SortExpression="ContractDate" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NotCheckInRemark" HeaderText="不報到原因" ReadOnly="True" SortExpression="NotCheckInRemark" >
                                            <HeaderStyle Width="30%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
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
