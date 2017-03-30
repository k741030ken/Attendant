<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0002.aspx.vb" Inherits="SC_SC0002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HR管理系統</title>
    <script type="text/javascript">
    <!--
		function funClose()
		{
			window.top.close();
		}
    -->
    </script>
</head>
<body>
    <form id="frmContent" runat="server">
		<table border="0" width="100%">
			<tr>
				<td align="center">
					<table style="WIDTH: 300px" height="240" cellSpacing="0" cellPadding="0">
						<tr>
							<td height="32"><IMG src="/images/Title001.gif"></td>
						</tr>
						<tr>
							<td height="180" vAlign="middle" align="center">
								<TABLE style="BORDER-RIGHT: steelblue 1px solid; BORDER-TOP: steelblue 1px solid; BORDER-LEFT: steelblue 1px solid; BORDER-BOTTOM: steelblue 1px solid; BACKGROUND-COLOR: #f6f9ff"
									height="100%" cellSpacing="1" cellPadding="1" width="100%">
									<TR>
										<TD height="100%" valign="top" id="tdMsg" runat="server" style="FONT-SIZE: 12px">&nbsp;
										</TD>
									</TR>
								</TABLE>
							</td>
						</tr>
						<tr>
							<td align="center">
								<input type="button" value="關閉視窗" onclick="funClose();" style="BORDER-RIGHT:darkgray 1px solid; BORDER-TOP:darkgray 1px solid; FONT-SIZE:12px; BORDER-LEFT:darkgray 1px solid; CURSOR:hand; BORDER-BOTTOM:darkgray 1px solid; FONT-FAMILY:Microsoft Sans Serif; BACKGROUND-COLOR:gainsboro">
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
