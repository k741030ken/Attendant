<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA4100.aspx.vb" Inherits="PA_PA4100" %>

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
                case "btnCopy":
                    if (!hasSelectedRow(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
            }
        }

        function confirmDel() {
            var item = document.getElementById('ddlDelItem').value;
            if (item == "") {
                alert("下拉選單值未選擇！");
                return false;
            }
            else {
                if (!confirm('確定刪除此筆資料？')) return false;
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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="left" style="height: 30px;">
                    <asp:RadioButton ID="rbVIP" GroupName="VIPFlag" AutoPostBack="true" Text="查詢VIPParameter" Checked="true" runat="server" />
                    <asp:RadioButton ID="rbVIPFlow" GroupName="VIPFlag" AutoPostBack="true" Text="查詢VIPParameterFlow" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:TextBox ID="txtEmpID" runat="server" CssClass="InputTextStyle_Thin" Style="text-transform: uppercase"></asp:TextBox>
                                <uc:ButtonQuerySelectUserID ID="ucSelectEmpID" runat="server" ButtonText="..." ButtonHint="選擇人員..." WindowHeight="550" WindowWidth="500" />
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblName" Font-Size="15px" runat="server" Text="員工姓名："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:TextBox ID="txtName" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblAllCompIDFlag" Font-Size="15px" runat="server" Text="金控全選註記："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlAllCompIDFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-全選"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非全選"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblAllGroupIDFlag" Font-Size="15px" runat="server" Text="事業群全選註記："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlAllGroupIDFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-全選"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非全選"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblAllOrganIDFlag" Font-Size="15px" runat="server" Text="部門全選註記："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlAllOrganIDFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-全選"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非全選"></asp:ListItem>                                    
                                </asp:DropDownList>
                            </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblUseCompID" Font-Size="15px" runat="server" Text="授權公司："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlUseCompID" runat="server" AutoPostBack="true" Font-Names="細明體">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblUseGroupID" Font-Size="15px" runat="server" Text="授權事業群："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlUseGroupID" runat="server" AutoPostBack="true" Font-Names="細明體">
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblUseOrganID" Font-Size="15px" runat="server" Text="授權部門："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlUseOrganID" runat="server" Font-Names="細明體">
                                </asp:DropDownList>
                            </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblGrantFlag" Font-Size="15px" runat="server" Text="授權/排除授權："></asp:Label>
                            </td>
                            <td align="left" colspan="5">
                                <asp:DropDownList ID="ddlGrantFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-授權"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-排除授權"></asp:ListItem>   
                                </asp:DropDownList>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,EmpID,GrantFlag,UseCompID,UseGroupID,UseOrganID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="2%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" Visible="false" >
                                            <HeaderStyle Width="0%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="員工姓名" ReadOnly="True" SortExpression="Name" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GrantFlag" HeaderText="授權/排除授權" ReadOnly="True" SortExpression="GrantFlag" Visible="false" >
                                            <HeaderStyle Width="0%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GrantFlagName" HeaderText="授權/排除授權" ReadOnly="True" SortExpression="GrantFlagName" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseCompID" HeaderText="查詢權限公司" ReadOnly="True" SortExpression="UseCompID" Visible="false" >
                                            <HeaderStyle Width="0%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseCompName" HeaderText="查詢權限公司" ReadOnly="True" SortExpression="UseCompName" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseGroupID" HeaderText="查詢權限事業群" ReadOnly="True" SortExpression="UseGroupID" Visible="false" >
                                            <HeaderStyle Width="0%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseGroupName" HeaderText="查詢權限事業群" ReadOnly="True" SortExpression="UseGroupName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseOrganID" HeaderText="查詢權限部門" ReadOnly="True" SortExpression="UseOrganID" Visible="false" >
                                            <HeaderStyle Width="0%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseOrganName" HeaderText="查詢權限部門" ReadOnly="True" SortExpression="UseOrganName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseRankID" HeaderText="查詢權限金控職等" ReadOnly="True" SortExpression="UseRankID" >
                                            <HeaderStyle Width="6%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UseOurColleagues" HeaderText="查詢權限頁籤" ReadOnly="True" SortExpression="UseOurColleagues" >
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BeginDate" HeaderText="查詢起日" ReadOnly="True" SortExpression="BeginDate" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EndDate" HeaderText="查詢迄日" ReadOnly="True" SortExpression="EndDate" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgName" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
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
