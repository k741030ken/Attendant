<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MessagePage.aspx.vb" Inherits="SC_MessagePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Message</title>
    <link href="../StyleSheet.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" language="javascript" src="../ClientFun/DisableRightClick.js"></script>
    <script type="text/javascript" language="javascript">
	<!--
		function funClose()
		{
			window.top.close();
		}
		
		function funGoPage(WebPage)
		{
			window.location = WebPage;
		}
		
		function funGo() {
		    
		    if (frmContent.NextWebPage.value == '') {
		        window.top.close();

		        return false;
		    }
		    else {
		        frmContent.btnGo.click();
		    }
		}
	-->
	</script>
</head>
<body>
    <form id="frmContent" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr>
				<td align="center" style="width:100%; background-color:#808080">&nbsp;</td>
			</tr>
			<tr>
				<td align="center" style="width:100%">&nbsp;</td>
			</tr>
			<tr>
				<td align="center" style="width:100%">
					<table cellspacing="0" cellpadding="0" width="528" border="0">
						<tr>
							<td style="vertical-align:bottom; width:100%"><Img ID="TopMessageGif" runat="server" height="67" src="../images/message_A.gif" width="528" style="border:0"></td>
						</tr>
						<tr>
							<td width="100%" bgColor="#e7efef">
								<table cellspacing="0" cellpadding="5" width="100%" border="0">
									<tr>
										<td valign="top" align="center" width="20%" rowSpan="2">&nbsp;
											<asp:image id="imgInfo" runat="server" ImageUrl="../images/error.gif"></asp:image></td>
										<td width="80%" style="text-align:left"><asp:label id="lblInforPath" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="新細明體" Font-Size="12pt">Label</asp:label></td>
									</tr>
									<tr>
										<td width="80%" style="text-align:left"><asp:label id="lblMessage" runat="server" Font-Size="12px">錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！</asp:label></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="100%"><IMG id="BottomMessageGif" runat="server" height="40" src="../images/message_B.gif" width="528" border="0">
							</td>
						</tr>
						<tr>
							<td width="100%">&nbsp;</td>
						</tr>
						<tr>
							<td align="middle" width="100%"><INPUT id="btnClientGo" class="buttonface" style="WIDTH: 105px; HEIGHT: 28px" onclick="return funGo();" type="button" value="<% = btnGo.Text %>" style="display:<%= buttonDisplay %>"><asp:button id="btnGo" runat="server" CssClass="buttonface" Font-Names="12" Height="28" Width="105" Text="Button" style="DISPLAY:none"></asp:button>&nbsp;
								<asp:textbox id="NextWebPage" style="DISPLAY: none" runat="server"></asp:textbox></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="middle" width="600">&nbsp;</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
