<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA021.aspx.vb" Inherits=" WF_WFA021" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Message</title>
    <link href="../StyleSheet.css" type="text/css" rel="Stylesheet" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script type="text/javascript" language="javascript">
	<!--		
		function funGoPage(WebPage)
		{
			window.location = WebPage;
		}
		
		function funGo()
		{
		    if (frmContent.NextWebPage.value == '')
		        window.top.close()
		    else {
		        //frmContent.btnGo.click();
		        window.self.location = frmContent.NextWebPage.value;
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
					<table cellspacing="0" cellpadding="0" width="85%" border="0">
						<tr>
							<td style="vertical-align:bottom; width:100%"><img height="67" src="../images/message_A.gif" width="100%" style="border:0"></td>
						</tr>
						<tr>
							<td width="100%" bgColor="#e7efef">
								<table cellspacing="0" cellpadding="5" width="100%" border="0">
									<tr>
										<td valign="top" align="center" width="20%" rowspan="2">&nbsp;
											<asp:image id="imgInfo" runat="server" ImageUrl="../images/error.gif"></asp:image></td>
										<td width="80%" style="text-align:left"><asp:label id="lblInforPath" runat="server" ForeColor="Blue" Font-Bold="True" Font-Names="新細明體" Font-Size="12pt">Label</asp:label></td>
									</tr>
									<tr>
										<td colspan='2' style="text-align:left"><asp:label id="lblMessage" runat="server" Font-Names="新細明體" Font-Size="12px">錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！錯誤訊息！</asp:label></td>
									</tr>
								</table>
							</td>
						</tr>
						
						<tr>
							<td width="100%"><IMG height="40" src="../images/message_B.gif" width="100%" border="0">
							</td>
						</tr>
						<tr>
							<td width="100%">&nbsp;</td>
						</tr>
						<tr>
							<td align="middle" width="100%"><asp:button id="btnGo" runat="server" CssClass="buttonface" Font-Names="12" Height="28" Width="105" Text="Button"></asp:button>&nbsp;
								<asp:textbox id="NextWebPage" style="DISPLAY: none" runat="server"></asp:textbox></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td align="middle">&nbsp;</td>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
