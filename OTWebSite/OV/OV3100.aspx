<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV3100.aspx.vb" Inherits="OV_OV3100" %>

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
    <title>天然災害設定(查詢)</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <%--<asp:Button ID="OV2000" runat="server" Text="OV2000" />
    <asp:Button ID="OV3000" runat="server" Text="OV3000" />--%>
    <table style="width: 100%" class="tbl_Condition">
        <tr>
         <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="*查詢條件：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlPersonOrCity" runat="server" AutoPostBack="True">
                    <asp:ListItem Text=" 請選擇" Value=""></asp:ListItem>
                    <asp:ListItem Text=" 縣市" Value="CityTable"></asp:ListItem>
                    <asp:ListItem Text=" 人員" Value="PersonTable"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
            </td>
            <td width="40%" class="style2">
            </td>
        </tr>
        <tr>
        <td width="10%"></td>
        <td width="10%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="留守日期："></asp:Label>
            </td>
            <td width="40%" colspan="3" class="style2">
                <uc:ucCalender ID="ucBeginDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="ucEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr>
        <td width="10%"></td>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text=" 留守類型："></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlDisasterType" runat="server">

                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
            </td>
            <td width="40%" class="style2">
            </td>
        </tr>
        <tr id="trCity" runat="server" visible="false">
        <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="Label1" runat="server" Text=" 縣市："></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlCityCode" runat="server" AutoPostBack="true" >
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="lblAreaCode" runat="server" Text="工作地點："></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlBranchName" runat="server" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr id = "trEmp" runat="server" visible="false">
        <td width="10%"></td>
            <td class="style1">
                <asp:Label ID="lblEmpID" runat="server" Text=" 員工編號："></asp:Label>
            </td>
            <td class="style2">
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="tbOTEmpID_FilteredTextBoxExtender" 
                        runat="server" TargetControlID="txtEmpID" FilterType="UppercaseLetters,Numbers">
                    </asp:FilteredTextBoxExtender>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
            </td>
            <td width="10%" class="style1">
            </td>
            <td width="40%" class="style2">
            </td>
        </tr>
        <tr>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="CityTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CityCode,CityName,BranchName,DisasterTime,DisasterDate,DisasterType,WorkSiteID,ddlType,LastChgCompID,LastChgCompName,LastChgEmpID,LastChgEmpName,LastChgDate"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="2%" />
                                           <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CityCode" HeaderText="縣市代號" Visible="false" ReadOnly="True" SortExpression="CityCode">
                                        <HeaderStyle Width="12%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>     
                                    <asp:BoundField DataField="CityName" HeaderText="縣市" ReadOnly="True" SortExpression="CityName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>                          
                                    <asp:BoundField DataField="BranchName" HeaderText="工作地點" HtmlEncode="false" ReadOnly="True" SortExpression="BranchName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterDate" HeaderText="留守起迄日期" ReadOnly="True" SortExpression="DisasterDate">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterTime" HeaderText="留守起迄時間" ReadOnly="True" SortExpression="DisasterTime">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterType" HeaderText="留守類型" ReadOnly="True" SortExpression="DisasterType">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgCompName" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgCompName" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgEmpName" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgEmpName" >
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
        <tr>
            <td style="width: 100%" colspan="5">
                <table width="100%" height="100%" class="tbl_Content" id="PersonTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain1" runat="server" GridViewName="gvMain1" PerPageRecord="20"  />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain1" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="EmpName,Remark,DisasterTime,DisasterDate,DisasterType,ddlType,LastChgCompID,LastChgEmpID,LastChgCompName,LastChgEmpName,LastChgDate"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="2%"  Font-Names="微軟正黑體"/>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdo_gvMain1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmpName" HeaderText="員工編號-姓名" ReadOnly="True" SortExpression="EmpName">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Remark" HeaderText="說明" ReadOnly="True" HtmlEncode="false" SortExpression="Remark">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterDate" HeaderText="留守起迄日期" ReadOnly="True" SortExpression="DisasterDate">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterTime" HeaderText="留守起迄時間" ReadOnly="True" SortExpression="DisasterTime">
                                        <HeaderStyle Width="14%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DisasterType" HeaderText="留守類型" ReadOnly="True" SortExpression="DisasterType">
                                        <HeaderStyle Width="10%" CssClass="td_header"  Font-Names="微軟正黑體"/>
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgCompName" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgCompName" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgEmpName" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgEmpName" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center"  Font-Names="微軟正黑體" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle"  Font-Names="微軟正黑體"/>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="tr_evenline" />
                                <AlternatingRowStyle CssClass="tr_oddline" />
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
