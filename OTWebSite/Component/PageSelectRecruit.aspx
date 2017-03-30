<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageSelectRecruit.aspx.vb" Inherits="Component_PageSelectRecruit" EnableEventValidation="false" %>
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
                
		    switch (Param) {
		        case "btnActionC":
//		        case "btnDownload":
//		            if (!hasSelectedRow('')) {
//		                alert("未選取資料列！");
//		                return false;
//		            }
		    }
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
                        <td align="right" width="10%">&nbsp;<asp:Label ID="lblQUeryLabel" runat="server" Text="公司代碼：" CssClass="f9"></asp:Label></td>
                        <td align="left" width="40%" colspan="3">                                                                
                            <asp:Label ID="txtCompID" runat="server" ></asp:Label>
                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                            <asp:TextBox runat="server" ID="txtReturnValue" style="display:none" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td align="right" width="10%">&nbsp;<asp:Label ID="Label1" runat="server" Text="應試者編號：" CssClass="f9"></asp:Label></td>
                        <td align="left" width="40%">                                                                
                            <asp:TextBox ID="txtRecID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td align="right" width="10%">&nbsp;<asp:Label ID="Label4" runat="server" Text="應試者名稱：" CssClass="f9"></asp:Label></td>
                        <td align="left" width="40%">                                                                
                            <asp:TextBox ID="txtNameN" CssClass="InputTextStyle_Thin" runat="server" MaxLength="12"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
               <asp:GridView Font-Size="12px" Font-Names="Microsoft Sans Serif" cssclass="GridViewStyle" HeaderStyle-CssClass="td_header" RowStyle-CssClass="td_detail" AllowPaging="true" PageSize="12" runat="server" ID="gvMain" width="100%" AutoGenerateColumns="false" DataKeyNames="CompID,RecID,CheckInDate">
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-Width="7%">
                            <ItemStyle CssClass="td_detail" Height="15px" />
                            <HeaderStyle CssClass="td_header" Width="3%" />
                            <ItemTemplate>
                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                <asp:TextBox ID="txtKey" runat="server" style="display:none" Text='<%# Eval("_Key") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="應試編號" DataField="RecID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="7%" HeaderText="姓名" DataField="NameN" ItemStyle-HorizontalAlign="Center" />                                                     
                        <asp:BoundField HeaderStyle-Width="13%" HeaderText="身分證字號" DataField="IDNo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="預計報到日" DataField="ContractDate" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="用人單位部門" DataField="DeptID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="科組課" DataField="OrganID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="7%" HeaderText="身分別" DataField="RecIdentity" ItemStyle-HorizontalAlign="Center" />                                                     
                        <asp:BoundField HeaderStyle-Width="7%" HeaderText="身分別-註明說明" DataField="RecIdentityRemark" ItemStyle-HorizontalAlign="Center" /> 
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="職位" DataField="PositionID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="生日" DataField="BirthDate" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderStyle-Width="0%" HeaderText="CheckInDate" DataField="CheckInDate" ItemStyle-HorizontalAlign="Center" Visible=false />
                    </Columns>
                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoData" runat="server" Text="無資料下傳"></asp:Label>
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
