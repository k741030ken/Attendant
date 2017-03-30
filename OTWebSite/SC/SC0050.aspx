<%@ Page %>
<html>
	<head>
		<TITLE>HR管理系統</TITLE>
		<meta name="GENERATOR" Content="Microsoft FrontPage 4.0" />
		<meta http-equiv="Content-Type" content="text/html; charset=big5" />
		<meta http-equiv="Content-Language" content="zh-tw" />
	</head>
	<frameset id="fraSubmain" name="fraSubmain" border="0" framespacing="0" rows="50,*" frameborder="0">
		<frame id="frmFunction" name="frmFunction" src="../SC/SC0060.aspx?FunID=<% = Request("FunID") %>&CompRoleID=<% = UserProfile.SelectCompRoleID %>" noresize scrolling="no">
		<frame id="frmMain" name="frmMain" src="<% = iif(Request("Path").indexOf("?")>=0,Request("Path") & "&_FunID=" & Request("FunID") & "&CompRoleID=" & UserProfile.SelectCompRoleID,Request("Path") & "?_FunID=" & Request("FunID") & "&CompRoleID=" & UserProfile.SelectCompRoleID) %>" scrolling="yes">
		<noframes>
			<body>
				<p>此網頁使用框架,但是您的瀏覽器並不支援，請使用Internet Explorer 4.0以後版本.</p>
			</body>
		</noframes>
	</frameset>
</html>
