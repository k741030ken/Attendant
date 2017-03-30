<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4400.aspx.vb" Inherits="OV_OV4400" %>

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
            text-align: left;
            font-size: 14px;
            width: 10%;
        }
        .Mystyle
        {
            margin-left: 15%; 
         }
        
<%--        .style2
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
        }--%>
        
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
<%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">      
</asp:ToolkitScriptManager>--%>
<ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
    <div style="width: 100%; height: 100%">
        <br />
        <table class="tbl_Condition" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;
            font-family: @微軟正黑體;">
            <tr class="tr_style">
                <td class="table_td_header">
                    <asp:Label ID="lblOTCompName" runat="server" Text="公司代碼：" class="Mystyle"></asp:Label>
                </td>
                <td class="table_td_content">
                    <asp:DropDownList ID="ddlCompID" runat="server" Font-Names="微軟正黑體">
                    </asp:DropDownList>
                    <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin"
                        Width="200px"></asp:Label>
                    <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display: none" />
                    <asp:Label ID="lblReleaseResult" runat="server" ForeColor="Blue" Text="" Style="display: none"></asp:Label>
                </td>
                <td>
                    <table style="width: 100%" class="table2" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="table_td_header">
                                <asp:Label ID="Label19" runat="server" Text="單位類別：" class="Mystyle"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="table_td_header" style="width: 100%">
                                <asp:Label ID="Label20" runat="server" Text="部門：" class="Mystyle"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="table_td_content">
                    <table style="width: 100%" class="table2">
                        <tr>
                            <td>
                                <!--<asp:UpdatePanel ID="UpdMain" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                             </asp:UpdatePanel>-->
                                <asp:DropDownList ID="ddlOrgType" runat="server" Style="width: 231px;" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlDeptID" runat="server" Style="width: 231px;" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="lblOrganName" runat="server" Text="科別：" class="Mystyle"></asp:Label>
                </td>
                <td class="table_td_content">
                    <asp:DropDownList ID="ddlOrganID" runat="server" Style="width: 231px;" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
                <td class="table_td_header">
                    <asp:Label ID="lblOTEmpID" runat="server" Text="員工編號：" maxlength="6" class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="tbOTEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                    <asp:TextBox CssClass="InputTextStyle_Thin" ID="tbOTEmpID" runat="server" Width="80px" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase"></asp:TextBox>
                    <asp:Label ID="lblEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucQueryEmpID" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label1" runat="server" Text="員工姓名："  class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTEmpName" runat="server" maxlength="20"> </asp:TextBox>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label4" runat="server" Text="在職狀態：" class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:DropDownList ID="ddlWorkStatus" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tr_style">
                <td class="table_td_header">
                    <asp:Label ID="Label6" runat="server" Text="職等：" class="Mystyle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRankIDMIN" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRankIDMIN_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="Label8" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="ddlRankIDMAX" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRankIDMIN_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblRankNotice" Font-Size="12px" runat="server" Text="職等選擇請由小到大" ForeColor="Red"
                        Visible="FALSE"></asp:Label>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label10" runat="server" Text="職稱：" class="Mystyle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTitleIDMIN" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label13" runat="server" Text="~"></asp:Label>
                    <asp:DropDownList ID="ddlTitleIDMAX" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td class="table_td_header">
                    <asp:Label ID="Label14" runat="server" Text="職位：" class="Mystyle"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPositionID" runat="server" Style="width: 231px;">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
             <td class="table_td_header">
                    <asp:Label ID="Label7" runat="server" Text="補休失效日：" class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style" colspan = "5">
                    <uc:ucCalender ID="ucFailDateStart" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label ID="Label9" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="ucFailDateEnd" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
            </tr>
            <tr>
                <td class="table_td_header">
                    <asp:Label ID="Label2" runat="server" Text="加班日期：" class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style">
                    <uc:ucCalender ID="ucStartDate" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                    <asp:Label ID="Label11" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="ucEndDate" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                </td>
                 <td  class="table_td_header"  >
                <asp:Label ID="lblBTime" runat="server" Text="開始時間：" class="Mystyle"></asp:Label>
            </td>
            <td  class="tr_style" >
                <asp:DropDownList ID="StartTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label3" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="StartTimeM" runat="server">
                </asp:DropDownList>
            </td>
            <td width="10%" class="table_td_header" >
                <asp:Label ID="lblETime" runat="server" Text="結束時間：" class="Mystyle"></asp:Label>
            </td>
            <td width="40%" class="tr_style" >
                <asp:DropDownList ID="EndTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label12" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="EndTimeM" runat="server">
                </asp:DropDownList>
            </td>
                
            </tr>
            <tr class="tr_style">
                <td class="table_td_header">
                    <asp:Label ID="Label5" runat="server" Text="計薪年月：" class="Mystyle"></asp:Label>
                </td>
                <td class="tr_style">
                    <asp:TextBox ID="tbOTPayDate" runat="server" Style="width: 80px" AutoPostBack="true"> </asp:TextBox>
                    <asp:Label ID="tbOTPayDateErrorMsg" runat="server" Text="(YYYYMM)" Style="padding-left: 10px;
                        color: Red"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="div_tb" runat="server">
            <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%">
                <tr>
                    <td style="width: 100%">
                        <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;font-family: @微軟正黑體;">
                        <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                            AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTCompID,OTEmpID,OTStartDate,OTEndDate,OTEmpName,OTDate,OTTime,OTSeq,GridViewIndex,SalaryOrAdjustName,AdjustInvalidDateShow"
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
                                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1%>"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="3%" Height="15px" CssClass="td_header" />
                                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:ButtonField Text="明細" runat="server" HeaderText="明細" CommandName="Detail" ItemStyle-Width ="2%" ItemStyle-Font-Size ="12px" 
                                            HeaderStyle-CssClass ="td_header"  ItemStyle-CssClass ="td_detail"></asp:ButtonField>
                                <%--<asp:TemplateField HeaderText="明細" ShowHeader="False">
                                    <ItemTemplate>
                                     <asp:ButtonField Text="明細" runat="server" HeaderText="明細" CommandName="Detail" ItemStyle-Width ="2%" ItemStyle-Font-Size ="12px" 
                                            HeaderStyle-CssClass ="td_header"  ItemStyle-CssClass ="td_detail"></asp:ButtonField>
                                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細" Font-Names="微軟正黑體">
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
        </div>
    </div>
    </form>
</body>
</html>
