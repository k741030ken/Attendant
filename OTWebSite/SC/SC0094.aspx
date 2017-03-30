<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0094.aspx.vb" Inherits="SC_SC0094" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
	<script type="text/javascript">
	<!--
		function funAction(Param)
		{
		    if (Param == "btnActionC")
		    {
			    if (frmContent.txtServer.value == '')
			    {
				    alert('[伺服器]未輸入！');
				    frmContent.txtServer.focus();
				    return false;
			    }
    			
			    if (frmContent.txtDB.value == '')
			    {
				    alert('[資料庫]未輸入！');
				    frmContent.txtDB.focus();
				    return false;
			    }

			    if (frmContent.txtID.value == '')
			    {
				    alert('[登入帳號]未輸入！');
				    frmContent.txtID.focus();
				    return false;
			    }

			    window.top.returnValue = 'server=' + frmContent.txtServer.value + ';' + 
									     'database=' + frmContent.txtDB.value + ';' + 
									     'uid=' + frmContent.txtID.value + ';' + 
									     'pwd=' + frmContent.txtPassword.value;
		    }
		    window.top.close();
		}
		
		function funKeyPress()
		{
			if (window.event.keyCode == 13)
			{
				funAction('btnActionC');
			}
		}
	-->
	</script>
	</head>
<body>
    <form id="frmContent" runat="server">
		<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
			<TR>
				<TD>
					<P align="center">
						<TABLE id="Table2" style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid; BORDER-COLLAPSE: collapse; BACKGROUND-COLOR: #f4faff"
							cellSpacing="1" cellPadding="1" width="400" border="1">
							<TR height="24">
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; FONT-SIZE: 15px; BORDER-LEFT: dimgray 1px solid; COLOR: navy; BORDER-BOTTOM: dimgray 1px solid"
									align="center" width="40%">伺服器</TD>
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid" align="left"
									width="60%">
									<asp:TextBox class="InputTextStyle_Thin" id="txtServer" runat="server"></asp:TextBox></TD>
							</TR>
							<TR height="24">
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; COLOR: navy; BORDER-BOTTOM: dimgray 1px solid"
									align="center" width="40%">資料庫</TD>
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid" align="left"
									width="60%">
									<asp:TextBox class="InputTextStyle_Thin" id="txtDB" runat="server"></asp:TextBox></TD>
							</TR>
							<TR height="24">
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; COLOR: navy; BORDER-BOTTOM: dimgray 1px solid"
									align="center" width="40%">登入帳號</TD>
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid" align="left"
									width="60%">
									<asp:TextBox class="InputTextStyle_Thin" id="txtID" runat="server"></asp:TextBox></TD>
							</TR>
							<TR height="24">
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; COLOR: navy; BORDER-BOTTOM: dimgray 1px solid"
									align="center" width="40%">密碼</TD>
								<TD style="BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid" align="left"
									width="60%">
									<asp:TextBox class="InputTextStyle_Thin" id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></TD>
							</TR>
						</TABLE>
					</P>
				</TD>
			</TR>
		</TABLE>
    </form>
</body>
</html>
