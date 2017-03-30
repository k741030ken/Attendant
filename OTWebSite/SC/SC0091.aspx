<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0091.aspx.vb" Inherits="SC_SC0091" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
	<style type="text/css">	
		.areaStyle { BORDER-RIGHT: #808080 1px solid; BORDER-TOP: #808080 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #808080 1px solid; BORDER-BOTTOM: #808080 1px solid; FONT-FAMILY: Microsoft Sans Serif }	
	</style>
	<link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
	<script type="text/javascript">
	<!--
			function funAction(strFun)
			{
				funSetButton(true);
				if (strFun == '1')
				{
					window.parent.frames('main').frmContent.txtConnStr.value = frmContent.ddlServer.value;
					window.parent.frames('main').funQuery(frmContent.txtSQL.value.toString());		
					//window.parent.frames('main').funQuery(Replace(frmContent.txtSQL.value.toString(), '\r\n', ' '));		
				}
				else if (strFun == '2')
				{
					if (gfunTrim(frmContent.txtSQL.value) == '')
					{
					    alert('未輸入查詢字串！');
						funSetButton(false);
						return false;
					}
					frmContent.btnExport.click();
				}
				/*
				else if (strFun == '3')
				{
					if (gfunTrim(frmContent.txtSQL.value) == '')
					{
						alert('未輸入查詢字串！');
						funSetButton(false);
						return false;
					}
					frmContent.btnExportXML.click();
				}
				*/
				else if(strFun == '4')
				{
					if (gfunTrim(frmContent.txtSQL.value) == '')
					{
					    alert('未輸入查詢字串！');
						funSetButton(false);
						return false;
					}
					frmContent.btnExportData.click();
				}
				funSetButton(false);
			}
			
			function funConnError()
			{
			    Alert('連線錯誤，請重新輸入！');
				funAddServer();
			}
			
			function funSetButton(boldisabled)
			{
				frmContent.btnQuery.disabled = boldisabled;
				frmContent.btnExportExcel.disabled = boldisabled;
				//frmContent.btnExpXML.disabled = boldisabled;
				frmContent.btnTransfer.disabled = boldisabled;
			}
	-->
	</script>
</head>
<body style="margin-bottom:0; margin-left:10; margin-right:10; margin-top:5;">
    <form id="frmContent" runat="server">
		<table width="100%" style="height:100%" border="0">
			<tr style="height:30; width:100%">
				<td style="width:70%">
				    <input type="button" class="buttonface" id="btnQuery" value="查詢" onclick="funAction('1');" />&nbsp;
				    <input class="buttonface" onclick="return funAction('2');" id="btnExportExcel" type="button" value="匯出Excel" />&nbsp;				    
				</td>
				<td style="width:30%">
					<asp:DropDownList id="ddlServer" CssClass="DropDownListStyle" runat="server" Font-Names="細明體" Width="180px" Font-Size="12px" AutoPostBack="True">
						<asp:ListItem Value="PD">PD</asp:ListItem>
					</asp:DropDownList>&nbsp;<asp:LinkButton ID="btnLinkServer" runat="server" Font-Size="12px" ForeColor="blue" Text="新增"></asp:LinkButton></td>
			</tr>
			<tr style="height:100%">
				<td width="100%" style="height:100%" colspan="2"><asp:TextBox id="txtSQL" runat="server" Font-Names="細明體"  Font-Size="12px" CssClass="areaStyle" TextMode="MultiLine" Width="100%" Height="100%" Rows="10"></asp:TextBox></td>
			</tr>
			<tr style="height:30; width:100%">
				<td width="100%" colspan="2"><input type="button" id="btnTransfer" value="將查詢結果轉入" class="buttonface" onclick="funAction('4');" />&nbsp;<asp:DropDownList id="ddlImportServer" CssClass="DropDownListStyle" runat="server" Font-Names="細明體" Width="180px" Font-Size="12px"
						AutoPostBack="False"></asp:DropDownList></td>
			</tr>
		</table>
		<asp:Button ID="btnExport" Runat="server" style="DISPLAY:none"></asp:Button>
		<asp:TextBox ID="txtConnStr" Runat="server" style="DISPLAY:none"></asp:TextBox>
		<asp:Button ID="btnAddServer" Runat="server" style="DISPLAY:none"></asp:Button>
		<asp:Button ID="btnExportXML" Runat="server" style="DISPLAY:none"></asp:Button>
		<asp:Button ID="btnExportData" Runat="server" style="DISPLAY:none"></asp:Button>
	</form>
</body>
</html>
