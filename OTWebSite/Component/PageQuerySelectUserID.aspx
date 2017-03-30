<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageQuerySelectUserID.aspx.vb" Inherits="Component_PageQuerySelectUserID" EnableEventValidation="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
		{
		    if (Param == 'btnActionX')
		        window.top.close();
		}
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:10px; margin-right:10px; margin-bottom:5px" >
    <form id="frmContent" runat="server"> 
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table width="100%" border="0">
            <tr>
                <td align="center">
                    <table width="100%" cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" >
                         <tr>
                            <td align="right" width="100px">
                                <asp:Label ID="lblCompID" runat="server" Text="請選擇公司：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">                                                                
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100px">
                                <asp:Label ID="lblDeptID" runat="server" Text="請選擇單位：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">                                                                
                                <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="100px">
                                <asp:Label ID="lblUserID" runat="server" Text="請選擇人員：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">    
                                <asp:DropDownList ID="ddlUserID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">                                                                                                         
                                <asp:TextBox runat="server" ID="txtQueryString" Width="100%" CssClass="InputTextStyle_Thin" ></asp:TextBox>                    
                            </td>
                            <td align="left">
                                <asp:Button runat="server" ID="btnQuery" cssclass="buttonface" Text="員編或姓名速查" height="25px" />
                            </td>
                        </tr>          
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 30px;">
                        <asp:GridView Font-Size="12px" Font-Names="Microsoft Sans Serif" cssclass="GridViewStyle" HeaderStyle-CssClass="td_header" RowStyle-CssClass="td_detail" AllowPaging="true" PageSize="12" runat="server" ID="gvMain" width="100%" AutoGenerateColumns="false" DataKeyNames="CompID,CompName,EmpID,Name">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle CssClass="td_detail" Height="15px" />
                                    <HeaderStyle CssClass="td_header" Width="5%" />
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="選取" CommandName="select" />
                                        <asp:RadioButton ID="rdo_gvMain" style="display:none" runat="server" />
                                        <asp:TextBox ID="txtKey" runat="server" style="display:none" Text='<%# Eval("_Key") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="部門" DataField="DeptName" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="科組課" DataField="OrganName" ItemStyle-HorizontalAlign="Center" />                                                     
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="員工姓名" DataField="Name" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="員工編號" DataField="EmpID" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="職位" DataField="Position" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="工作性質" DataField="WorkType" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="職等 職稱" DataField="RankTitleName" ItemStyle-HorizontalAlign="Center" />                                                     
                                <asp:BoundField HeaderStyle-Width="10%" HeaderText="工作地點" DataField="WorkSiteName" ItemStyle-HorizontalAlign="Center" /> 
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
                    <asp:TextBox runat="server" ID="TextBox1" style="display:none" ></asp:TextBox>    
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
