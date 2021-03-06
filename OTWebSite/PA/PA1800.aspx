﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA1800.aspx.vb" Inherits="PA_PA1800" %>

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
    <style type="text/css">
        .style1
        {
            width: 153px;
        }
        .style2
        {
            width: 12%;
        }
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblPositionID" Font-Size="15px" runat="server" Text="職位代碼："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:UpdatePanel ID="UpdPositionID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlPositionID" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCompID" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblRemark" Font-Size="15px" runat="server" Text="職位名稱："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRemark" runat="server"></asp:TextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblInValidFlag" Font-Size="15px" runat="server" Text="無效註記："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:UpdatePanel ID="UpdInValidFlag" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlInValidFlag" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                    </ContentTemplate>                                                          
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCompID" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblSortOrder" Font-Size="15px" runat="server" Text="排序："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtSortOrder" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" class="10%">
                                <asp:Label ID="lblOrganPrintFlag" Font-Size="15px" runat="server" Text="部門列印註記："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlOrganPrintFlag" AutoPostBack="true" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-不可列印"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-可列印"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" class="15%">
                                <asp:Label ID="lblIsEVManager" Font-Size="15px" runat="server" Text="績效考核表主管註記："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:UpdatePanel ID="UpdIsEVManager" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlIsEVManager" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                    </ContentTemplate>                                                          
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCompID" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" class="10%">
                                <asp:Label ID="lblCategoryI" Font-Size="15px" runat="server" Text="大類："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlCategoryI" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left" class="15%">
                                <asp:Label ID="lblCategoryII" Font-Size="15px" runat="server" Text="中類："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:UpdatePanel ID="UpdCategoryII" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCategoryII" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                    </ContentTemplate>                                                          
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCategoryI" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="left" class="10%">
                                <asp:Label ID="lblCategoryIII" Font-Size="15px" runat="server" Text="細類："></asp:Label>
                            </td>
                            <td align="left" width="15%">
                                <asp:UpdatePanel ID="UpdCategoryIII" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCategoryIII" runat="server" Font-Names="細明體"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="PositionID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PositionID" HeaderText="職位代碼" ReadOnly="True" SortExpression="PositionID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="職位名稱" ReadOnly="True" SortExpression="Remark" >
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="無效註記" SortExpression="InValidFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="7%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgInValidFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgInValidFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                       
                                        <asp:BoundField DataField="SortOrder" HeaderText="排序" ReadOnly="True" SortExpression="SortOrder" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="部門列印註記" SortExpression="OrganPrintFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="8%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgOrganPrintFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "OrganPrintFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgOrganPrintFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "OrganPrintFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="績效考核表主管註記" SortExpression="IsEVManager">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsEVManager" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "IsEVManager")="1","True","False") %>' />
                                                <asp:Image ID="imgIsEVManager1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "IsEVManager")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="CategoryI" HeaderText="大類" ReadOnly="True" SortExpression="CategoryI" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryII" HeaderText="中類" ReadOnly="True" SortExpression="CategoryII" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIII" HeaderText="細類" ReadOnly="True" SortExpression="CategoryIII" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
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
