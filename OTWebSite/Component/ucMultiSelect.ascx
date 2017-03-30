<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucMultiSelect.ascx.vb" Inherits="Component_ucMultiSelect" %>

<table id="table1" class="tbl_Content" border="0" style="width:100%; height:100%; font-size:12px;font-name:細明體;"  >
    <tr>
        <td  colspan="4">
            <asp:panel runat="server" ID="pnlDeptTitle" style="width:100%">                                                 
               部門：<asp:DropDownList ID="ddlDeptType" runat="server" CssClass="DropDownListStyle" AutoPostBack="True"></asp:DropDownList>                                                       
            </asp:panel>
        </td>
    </tr>
	<tr  valign="middle" >
		<td  style="width:45%" align="center" >
		    <asp:label id="lblLeftCaption" runat="server" ForeColor="blue"></asp:label>
		</td>
		<td  style="width:5%">&nbsp;</td>
		<td  style="width:45%" align="center" >
		    <asp:label id="lblRightCaption" runat="server" ForeColor="blue"></asp:label>
		</td>
		<td  style="width:5%">&nbsp;</td>
	</tr>
	<tr>
		<td  style="width:45%">
		    <asp:listbox id="lstLeft" Width="100%" runat="server" SelectionMode="Multiple" Rows="15" style="font-family:Calibri, 新細明體"></asp:listbox>
		</td>
		<td  style="width:5%" align="center">
		        <asp:imagebutton id="btnMoveRight" runat="server" ImageUrl="../images/Next.gif" CausesValidation="False"   ></asp:imagebutton>
			<p><asp:imagebutton id="btnMoveLeft" runat="server" ImageUrl="../images/Prev.gif" CausesValidation="False"></asp:imagebutton></p>
		</td>
		<td  style="width:45%">
		    <asp:listbox id="lstRight" Width="100%" runat="server" SelectionMode="Multiple" Rows="15" style="font-family:Calibri, 新細明體"></asp:listbox>
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
            
		 </td>
	</tr>
</table>
