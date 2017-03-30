<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0001.aspx.vb" Inherits="SC_SC0001" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    <!--
		function funOnLoad()
		{
			window.setTimeout("funResponse();", 180000);
		}
		
		function funResponse()
		{
			frmContent.btnResponse.click();
		}
		
		function funShowMessage(strMsg)
		{
			window.open("../SC/SC0002.aspx?strMsg=" + strMsg, "", "channelmode=no,directories=no,height=265px,menubar=no,resizable=no,toolbar=no,width=350px");
		}
    
    -->
    </script>
</head>
<body onload="funOnLoad();">
    <form id="frmContent" runat="server">
    <div>
        <asp:Button ID="btnResponse" Runat="server" style="DISPLAY:none"></asp:Button>    
    </div>
    </form>
</body>
</html>
