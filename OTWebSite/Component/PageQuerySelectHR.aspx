<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageQuerySelectHR.aspx.vb" Inherits="Component_PageQuerySelectHR" %>
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
    <table width="100%" border="0">
        <tr>
            <td align="center">
                <table width="100%" cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" >
                     <tr>
                        <td align="right" width="70px">&nbsp;<asp:Label ID="lblQUeryLabel" runat="server" Text="查詢欄位：" CssClass="f9"></asp:Label></td>
                        <td align="left">                                    
                            
                            <asp:DropDownList runat="server" ID="ddlField" CssClass="DropDownListStyle"></asp:DropDownList>
                            <asp:TextBox runat="server"  ID="txtQueryString" Width="40%"  CssClass="InputTextStyle_Thin" ></asp:TextBox>                    
                            <asp:Button runat="server" ID="btnQuery"  cssclass="buttonface" Text="查詢" height="25px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;</td>
                        <td align="left"><font style="font-size:12px; color: Dimgray">※若資料量過大，僅會先撈取前200筆資料！請先輸入條件，再按下查詢，以過濾資料筆數...</font></td>
                    </tr>              
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                    <asp:GridView Font-Size="12px" Font-Names="Microsoft Sans Serif"  
                        cssclass="GridViewStyle" HeaderStyle-CssClass="td_header" 
                        RowStyle-CssClass="td_detail" AllowPaging="True" PageSize="12" runat="server" 
                        ID="gvMain" width="100%" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Left" />
                                <HeaderStyle CssClass="td_header" Width="5%" />
                                <ItemTemplate>
                                    <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                    <asp:TextBox ID="txtKey" runat="server" style="display:none" Text='<%# Eval("_Key") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="tr_evenline" />
                        <AlternatingRowStyle CssClass="tr_oddline" />
                        <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                        <PagerStyle CssClass="GridView_PagerStyle" />

<HeaderStyle CssClass="td_header"></HeaderStyle>

                        <PagerSettings Position="Top" />    
                    </asp:GridView> 
                <asp:TextBox runat="server" ID="txtReturnValue" style="display:none" ></asp:TextBox>    
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
