<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV1400.aspx.vb" Inherits="OV_OV1400" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            background-color: #f3f8ff;
            min-width: 110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            min-width: 110px;
        }
        input, option, span, div, select,label
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
            
    </style>
    <title>加班拋轉狀態查詢</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="tbl_Condition">
        <tr>
        <td width="10%"></td>
        <td width="10%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="*拋轉執行日期：" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" colspan="3" class="style2">
                <uc:ucCalender ID="runTimeBeginDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="runTimeEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr>
        <td width="10%"></td>
        <td width="10%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="拋轉日期："></asp:Label>
            </td>
            <td width="40%" colspan="3" class="style2">
                <uc:ucCalender ID="toOverTimeDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr>
        <td width="10%"></td>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text=" 拋轉狀態："></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Text=" 請選擇" Value=""></asp:ListItem>
                <asp:ListItem Text=" 成功" Value="00"></asp:ListItem>
                <asp:ListItem Text=" 失敗" Value="01"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
            </td>
            <td width="40%" class="style2">
            </td>
        </tr>
        <tr>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames=""
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CompID_Name" HeaderText="公司" ReadOnly="True" SortExpression="CompID_Name">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>     
                                    <asp:BoundField DataField="EmpID_Name" HeaderText="員工" ReadOnly="True" SortExpression="EmpID_Name">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="OTDate" HeaderText="加班起迄日期" HtmlEncode="false" ReadOnly="True" SortExpression="OTDate">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTTime" HeaderText="加班起迄時間" ReadOnly="True" SortExpression="OTTime">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ToOverTimeStatus" HeaderText="狀態" ReadOnly="True" SortExpression="ToOverTimeStatus">
                                        <HeaderStyle Width="7%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Message" HeaderText="訊息" ReadOnly="True" SortExpression="Message">
                                        <HeaderStyle Width="16%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
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
