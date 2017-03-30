<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST1900.aspx.vb" Inherits="ST_ST1900" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    if (!confirm('您確定要刪除此筆資料？'))
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
                    <table cellpadding="3" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%"> 
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="lblCompRoleID" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtEmpID" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpName" Font-Size="15px" runat="server" Text="員工姓名："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtEmpName" runat="server"></asp:Label>
                            </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%"> 
                                <asp:Label ID="lblEmpDate" Font-Size="15px" runat="server" Text="公司到職日："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtEmpDate" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblSinopacEmpDate" Font-Size="15px" runat="server" Text="企業團到職日："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtSinopacEmpDate" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%"></td>
                            <td align="left" width="20%"></td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%"> 
                                <asp:Label ID="lblQuitDate" Font-Size="15px" runat="server" Text="公司離職日："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtQuitDate" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblSinopacQuitDate" Font-Size="15px" runat="server" Text="企業團離職日："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtSinopacQuitDate" runat="server"></asp:Label>
                            </td>
                            <td align="left" width="10%"></td>
                            <td align="left" width="20%"></td>
                            <td width="5%"></td>
                        </tr>
                    </table>
                    <table cellpadding="10" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td width="30%" align="left" style="vertical-align:text-top" runat="server" id="tbCompEmpSen">
                                <asp:Label ID="lblCompEmpSen" Font-Size="15px" runat="server" Text="► 公司年資"></asp:Label>
                                <table width="90%" class="tbl_Edit" style="border-collapse:collapse;margin:10px 0;" cellpadding="10">
                                    <tr>
                                        <td class="td_EditHeader" width="50%" align="left">
                                            <asp:Label ID="lblTotSen" runat="server" Text="年資(年/天)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="50%" align="left">
                                            <asp:Label ID="lblNotEmpSen" runat="server" Text="修正年資(年/天)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="50%" align="left">
                                            <asp:Label ID="lblCompSenText1" runat="server" Text="公司年資 "></asp:Label>
                                            <asp:Label ID="txtTotSen" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompSenText2" runat="server" Text="年 / "></asp:Label>
                                            <asp:Label ID="txtTotDays" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompSenText3" runat="server" Text="天"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="50%" align="left">
                                            <asp:Label ID="lblCompSenText4" runat="server" Text="中斷年資 "></asp:Label>
                                            <asp:Label ID="txtNotEmpSen" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompSenText5" runat="server" Text="年 / "></asp:Label>
                                            <asp:Label ID="txtNotEmpDay" runat="server"></asp:Label>
                                            <asp:Label ID="lblCompSenText6" runat="server" Text="天"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="60%" align="left" style="vertical-align:text-top">
                                <asp:Label ID="lblSinopacEmpSen" Font-Size="15px" runat="server" Text="► 企業團年資"></asp:Label>
                                <table width="60%" id="tbCompSen" runat="server" class="tbl_Edit" style="border-collapse:collapse;margin:10px 0;" cellpadding="10">
                                    <tr>
                                        <td class="td_EditHeader" width="40%" align="left">
                                            <asp:Label ID="lblTotSen_SPHOLD" runat="server" Text="年資(年/天)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="60%" colspan="2" align="left">
                                            <asp:Label ID="lblCompSen" runat="server" Text="公司別年資(年/天)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="40%" align="left" rowspan="50">
                                            <asp:Label ID="lblSTotSenText1" runat="server" Text="企業團年資 "></asp:Label>
                                            <asp:Label ID="txtTotSen_SPHOLD" runat="server"></asp:Label>
                                            <asp:Label ID="lblSTotSenText2" runat="server" Text="年 / "></asp:Label>
                                            <asp:Label ID="txtDays_SPHOLD" runat="server"></asp:Label>
                                            <asp:Label ID="lblSTotSenText3" runat="server" Text="天"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:Label ID="txtCompName" runat="server"></asp:Label>
                                            <asp:Label ID="lblSTotSenText4" runat="server" Text="年資 "></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="25%" align="center">
                                            <asp:Label ID="txtCompSen" runat="server"></asp:Label>
                                            <asp:Label ID="lblSTotSenText5" runat="server" Text="年 / "></asp:Label>
                                            <asp:Label ID="txtCompDays" runat="server"></asp:Label>
                                            <asp:Label ID="lblSTotSenText6" runat="server" Text="天"></asp:Label>
                                        </td>
                                    </tr>
                                </table>                                
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="Seq,CompID,EmpID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderStyle Width="3%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <HeaderStyle Width="3%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="6%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="中斷起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateE" HeaderText="中斷迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="NotEmpReasonID" HeaderText="中斷原因" ReadOnly="True" SortExpression="NotEmpReasonID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NotEmpDay" HeaderText="中斷天數(公司)" ReadOnly="True" SortExpression="NotEmpDay" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SinopacNotEmpDay" HeaderText="中斷天數(企業團)" ReadOnly="True" SortExpression="SinopacNotEmpDay" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="備註" ReadOnly="True" SortExpression="Remark" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate">
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
