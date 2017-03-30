<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageWorkType.aspx.vb" Inherits="Component_PageWorkType" EnableEventValidation="false" %>
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
                        <td align="right" width="70px">&nbsp;<asp:Label ID="lblQUeryLabel" runat="server" Text="查詢方式：" CssClass="f9"></asp:Label></td>
                        <td align="left">                                    
                            
                            <asp:DropDownList runat="server" ID="ddlField" CssClass="DropDownListStyle"></asp:DropDownList>
                            <asp:TextBox runat="server"  ID="txtQueryString" Width="40%"  CssClass="InputTextStyle_Thin" ></asp:TextBox>                    
                            <asp:Button runat="server" ID="btnQuery"  cssclass="buttonface" Text="查詢" height="25px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;</td>
                        <td align="left"><font style="font-size:12px; color: Dimgray">※若資料量過大，僅先撈取前200筆資料！請先輸入條件，再按查詢，以過濾筆數...</font></td>
                    </tr>              
                </table>
            </td>
        </tr>
    </table>
    <font style="font-size:12px; color: #FF0000">※已選第一筆工作性質，為主要工作性質</font>
    <table id="table1" class="tbl_Content" border="0" style="width:100%; height:100%; font-size:12px;font-family: 細明體;"  >
	    <tr  valign="middle" >
		    <td  style="width:45%" align="center" >
		        <asp:label id="lblLeftCaption" runat="server" ForeColor="blue" Text="未選工作性質"></asp:label>
		    </td>
		    <td  style="width:5%">&nbsp;</td>
		    <td  style="width:45%" align="center" >
		        <asp:label id="lblRightCaption" runat="server" ForeColor="blue" Text="已選工作性質"></asp:label>
		    </td>
		    <td  style="width:5%">&nbsp;</td>
	    </tr>
	    <tr>
		    <td  style="width:45%">
		        <asp:listbox id="lstLeft" Width="100%" runat="server" SelectionMode="Multiple" Rows="20" style="font-family:Calibri, 新細明體"></asp:listbox>
		    </td>
		    <td  style="width:5%" align="center">
		            <asp:imagebutton id="btnMoveRight" runat="server" ImageUrl="../images/Next.gif" CausesValidation="False"   ></asp:imagebutton>
			    <p><asp:imagebutton id="btnMoveLeft" runat="server" ImageUrl="../images/Prev.gif" CausesValidation="False"></asp:imagebutton></p>
		    </td>
		    <td  style="width:45%">
		        <asp:listbox id="lstRight" Width="100%" runat="server" SelectionMode="Multiple" Rows="20" style="font-family:Calibri, 新細明體"></asp:listbox>
		    </td>
		    <td  style="width:5%" align="center">
		            <asp:imagebutton id="btnMoveUp" runat="server" ImageUrl="../images/btnUp.gif" CausesValidation="False"></asp:imagebutton>
			    <p><asp:imagebutton id="btnMoveDown" runat="server" ImageUrl="../images/btnDown.gif" CausesValidation="False"></asp:imagebutton></p>
		    </td>
	    </tr>
	    <tr valign="middle">
		    <td  colspan="4">
		        <asp:TextBox ID="txtLeftResult" runat="server"  style="display:none"></asp:TextBox>
		        <asp:TextBox ID="txtRightResult" runat="server"  style="display:none"></asp:TextBox>		    		    
		        <!--回傳值(以,隔開)-->
		        <asp:TextBox ID="txtValueResult" runat="server" style="display:none"></asp:TextBox>
		        <asp:TextBox ID="txtTextResult" runat="server" style="display:none"></asp:TextBox>		    		    

                <asp:TextBox runat="server" ID="txtReturnValue" style="display:none" ></asp:TextBox>    
		     </td>
	    </tr>
    </table>

    </form>
</body>
</html>
