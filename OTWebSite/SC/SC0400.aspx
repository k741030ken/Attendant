<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0400.aspx.vb" Inherits="SC_SC0400" %>

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
        <table width="100%" class="tbl_Edit" cellpadding="1" cellspacing="1">
            <tr>
                <td class="td_EditHeader" width="15%" align="center">
                    <asp:Label ID="lblSysID" runat="server" ForeColor="blue" Text="系統別"></asp:Label>
                </td>
                <td class="td_Edit" style="width: 35%" align="left">
                    <asp:Label ID="lblSysName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                    <asp:Label ID="lblSysNameD" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px" Visible="false" ></asp:Label>
                </td>
                <td class="td_EditHeader" width="15%" align="center">
                    <asp:Label ID="lblCompRoleID" runat="server" ForeColor="blue" Text="授權公司"></asp:Label>
                </td>
                <td class="td_Edit" style="width: 35%" align="left">
                    <asp:Label ID="lblCompRoleName" runat="server" CssClass="InputTextStyle_ReadOnly" MaxLength="50" Width="360px"></asp:Label>
                    <asp:DropDownList ID="ddlCompRoleName" runat="server" AutoPostBack="true" CssClass="DropDownList"></asp:DropDownList>
                    <asp:Label ID="lblCompRoleIDN" runat="server" Visible = "False" ></asp:Label>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="right"><asp:Label ID="lblGroupID" Font-Size="15px" runat="server" Text="群組代碼："></asp:Label>
                            </td>
                            <td align="left">
                                <%--<asp:TextBox CssClass="InputTextStyle_Thin" ID="txtGroupID" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlGroupID" Width="300px" runat="server" CssClass="DropDownList"></asp:DropDownList>
                            </td>
                            <td align="right"><asp:Label ID="lblGroupName" font-size="15px" runat="server" Text="群組名稱："></asp:Label>
                            </td>
                            <td align="left"><asp:TextBox CssClass="InputTextStyle_Thin" ID="txtGroupName" runat="server"></asp:TextBox>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="SysID,CompRoleID,GroupID" Width="100%" PageSize="15">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SysID" HeaderText="系統別代碼" ReadOnly="True" SortExpression="SysID" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="CompRoleName" HeaderText="授權公司" ReadOnly="True" SortExpression="CompRoleName" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompRoleID" HeaderText="授權公司" ReadOnly="True" SortExpression="CompRoleID" Visible="False">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupID" HeaderText="群組代碼" ReadOnly="True" SortExpression="GroupID" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupName" HeaderText="群組名稱" SortExpression="GroupName" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" SortExpression="LastChgID" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="群組類別" SortExpression="GroupType">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupTypeName" runat="server" Text='<%# IIf(DataBinder.Eval(Container.DataItem, "GroupType")="0","一般群组","个人群组") %>'></asp:Label>
                                                <asp:Label ID="lblGroupType" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GroupType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
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
