<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV2102.aspx.vb" Inherits="OV_OV2102" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .table1
        {
            border-collapse: collapse;
            border: 1px solid #89b3f5;
            background-color: #f3f8ff;
        }
        
        
        .table1_input
        {
            width: 100px;
        }
        
        .tr_style
        {
            height: 20px;
        }
        
        
        .bt_style
        {
            margin: 5px;
        }
        .table1 td
        {
            border: 1px solid #5384e6;
            padding: 0;
        }
        .table2 td
        {
            border-collapse: collapse;
            border: 0;
        }
        .table_td_content
        {
            width: 23.3%;
        }
        .table_td_header
        {
            text-align: center;
            font-size: 14px;
            width: 10%;
            background-color: #e2e9fe;
        }
        
        .style2
        {
            text-align: center;
            font-size: 14px;
            width: 100%;
            background-color: #e2e9fe;
            height: 20px;
        }
        
        .style3
        {
            text-align: center;
            font-size: 14px;
            width: 10%;
            background-color: #e2e9fe;
            height: 20px;
        }
        
        input, option, span, div, select, label
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
        
        td_header_hidden
        {
            visibility: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
</asp:ToolkitScriptManager>
        <br />
        <table class="table1" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;font-family: @微軟正黑體;">
            <tr>
                <td class="style3">
                    <asp:Label ID="Label2" runat="server" Text="補休失效日期:"></asp:Label>
                </td>
                <td class="tr_style">
                    <uc:ucCalender ID="ucFailDate" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
            </tr>
        </table>
            <table width="100%" class="tbl_Content">
                <tr>
                    <td style="width: 100%">
                        <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord= "20" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;font-family: @微軟正黑體;">
                        <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTCompID,OTEmpID,OTStartDate,OTEndDate,OTSeq,GridViewIndex"
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體"
                            OnRowCreated="grvMergeHeader_RowCreated">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                      <ItemStyle CssClass="td_detail" Height="15px" />
                                       <HeaderStyle CssClass="td_header" Width="3%" />
                                       <HeaderTemplate>
                                         <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" runat="server"/>
                                       </HeaderTemplate>
                                       <ItemTemplate>
                                            <asp:CheckBox ID="chk_gvMain" runat="server" />
                                       </ItemTemplate>
                                       <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="3%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" Font-Names="微軟正黑體" HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="OTFormNO" HeaderText="表單編號" ReadOnly="True" SortExpression="OTFormNO">
                                    <HeaderStyle Width="19%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTStatusName" HeaderText="狀態" ReadOnly="True" SortExpression="OTStatusName">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTEmpName" HeaderText="加班人" ReadOnly="True" SortExpression="OTEmpName">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CodeCName" HeaderText="加班類型" ReadOnly="True" SortExpression="CodeCName">
                                    <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTDate" HeaderText="加班日期" ReadOnly="True" SortExpression="OTDate">
                                    <HeaderStyle Width="13%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTTime" HeaderText="加班起訖時間" ReadOnly="True" SortExpression="OTTime">
                                    <HeaderStyle Width="13%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OTTotalTime" HeaderText="加班時數" ReadOnly="True" SortExpression="OTTotalTime">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="OTTotalTime2" HeaderText="補休時數" ReadOnly="True" SortExpression="OTTotalTime2">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AdjustInvalidDateShow" HeaderText="補休失效日" ReadOnly="True" SortExpression="AdjustInvalidDateShow">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FileName" HeaderText="上傳附件" ReadOnly="True" SortExpression="FileName">
                                    <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
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
    </form>
</body>
</html>
