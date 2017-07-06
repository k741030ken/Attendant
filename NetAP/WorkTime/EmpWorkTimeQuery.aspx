<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpWorkTimeQuery.aspx.cs" Inherits="WorkTime_EmpWorkTimeQuery" %>
<!DOCTYPE>

<html>
<head runat="server">
    <title>個人班表查詢</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
    .Util_clsBtn 
    {
    	margin-right: 10px;
    }
    .Util_hide
    {
    	display: none;
    }
    .td_header
    {
    	background-color: White;
    }
    </style>
    <script type="text/javascript">

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">個人班表查詢</legend>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Util_hide" PostBackUrl="EmpWorkTimeQuery_Detail.aspx" />
                    <asp:HiddenField ID="hidCompID" runat="server" />
                    <asp:HiddenField ID="hidEmpID" runat="server" />
                    <table cellpadding="1" cellspacing="1" style="width: 100%">
                        <asp:Panel ID="plOrg" runat="server">
                            <tr style="height:20px">
                                <td colspan="3" align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />
                                    <asp:Button ID="btnClear" runat="server" Text="清除" onclick="btnClear_Click" CssClass="Util_clsBtn" />
                                </td>
                            </tr>
                            <tr style="height:20px" class="Util_clsRow1">
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblCompID" runat="server" Text="公司："></asp:Label>
                                    <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompID_Changed"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height:20px" class="Util_clsRow2">
                                <td width="20%" align="left">
                                    <asp:Label ID="lblQryType" runat="server" Text="查詢類別："></asp:Label>
                                    <asp:DropDownList ID="ddlQryType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlQryType_Changed">
                                        <asp:ListItem Text="1-行政組織" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="2-功能組織" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="80%" align="left" colspan="2">
                                    <asp:Panel ID="plOrganID" runat="server">
                                        <asp:Label ID="lblOrganID" runat="server" Text="行政組織："></asp:Label>
                                        <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_Changed"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptID_Changed"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlOrganID" runat="server"></asp:DropDownList>
                                    </asp:Panel>
                                    <asp:Panel ID="plFlowOrganID" runat="server">
                                        <asp:Label ID="lblFlowOrganID" runat="server" Text="功能組織："></asp:Label>
                                        <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode40_Changed"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode30_Changed"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode20_Changed"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode10" runat="server"></asp:DropDownList>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr style="height:20px" class="Util_clsRow1">
                                <td width="20%" align="left">
                                    <asp:Label ID="lblEmpID" runat="server" Text="員工編號："></asp:Label>
                                    <asp:TextBox ID="txtEmpID" runat="server" MaxLength="6"></asp:TextBox>
                                </td>
                                <td width="80%" align="left" colspan="2">
                                    <asp:Label ID="lblEmpName" runat="server" Text="員工姓名："></asp:Label>
                                    <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
                                </td>
                            </tr>                        
                        </asp:Panel>
                        <asp:Panel ID="plPer" runat="server">
                            <tr style="height:20px" class="Util_clsRow1">
                                <td width="20%" align="left">
                                    <asp:Label ID="UserComp" runat="server" Text="公司："></asp:Label>
                                    <asp:Label ID="txtCompID" runat="server" Text=""></asp:Label>
                                </td>
                                <td width="30%" align="left">                                
                                    <asp:Label ID="UserDept" runat="server" Text="單位："></asp:Label>
                                    <asp:Label ID="txtDeptID" runat="server" Text=""></asp:Label>
                                </td>
                                <td width="50%" align="left">                                
                                    <asp:Label ID="UserOrgan" runat="server" Text="科別："></asp:Label>
                                    <asp:Label ID="txtOrganID" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>                        
                        </asp:Panel>
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <div style ="height:75%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataKeyNames="CompID,DeptID,OrganID,EmpID,WTID" OnRowCommand="gvMain_RowCommand" AllowPaging="false" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="序號" >
                    <ItemTemplate>
                        <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="3%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="異動紀錄" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" CommandArgument="<%# Container.DataItemIndex %>" Text="明細"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="td_header" Width="4%" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="單位類別" DataField="OrgTypeName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部門" DataField="DeptName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="科組課" DataField="OrganName">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="員編" DataField="EmpID">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="NameN">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="班別" DataField="WTID">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="時間" DataField="WorkTime">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="輪班註記" DataField="RotateFlag">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgID" HeaderText="最後異動者">
                    <HeaderStyle Width="8%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期">
                    <HeaderStyle Width="12%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
            <EmptyDataTemplate>
                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
            </EmptyDataTemplate>
            <RowStyle CssClass="Util_gvRowNormal" />
            <AlternatingRowStyle CssClass="Util_gvRowAlternate" />
            <PagerStyle CssClass="GridView_PagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
