<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0060.aspx.vb" Inherits="SC_SC0060" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript">
    <!--
        var myPopup = window.createPopup();
        function funLoad() {
		    var msg;
		    try {
		        showSubmitting(myPopup);
			    var frame = window.parent.frames[1];
			    if ((frame.document.readyState == 'complete' || frame.document.readyState == 'interactive')) {
			        tblBody.style.display = "block";
			        hidePopupWindow(myPopup);
    			}
    			else {
    			    window.setTimeout("funLoad()", 100);
    			}
			}
			catch(ex) {
			    window.setTimeout("funLoad()", 100);
			}
		}
		
		function funCheckStatus() {
			var msg;
			try
			{
			    if ((window.parent.frames["frmMain"].document.readyState == 'complete' || window.parent.frames["frmMain"].document.readyState == 'interactive'))
			    {
				    trFun.disabled = false;
				    hidePopupWindow(myPopup);
			    }
			    else
			    {
				    trFun.disabled = true;	
				    window.setTimeout("funCheckStatus()", 300);
			    }
			}
			catch(ex)
			{
			    window.setTimeout("funCheckStatus()", 300);
			}
		}

		function funAction(Action) {
		    if (trFun.disabled == true) {
		        return false;
		    }
		    var objParam = window.parent.frames["frmMain"].document.getElementById('__ActionParam');
		    var objbtn = window.parent.frames["frmMain"].document.getElementById('__btnAction');
		    trFun.disabled = true;
		    objParam.value = Action;
		    showSubmitting(myPopup);

		    objbtn.click();
		    window.setTimeout("funCheckStatus()", 1000);
	        return false;
	    }
    -->
    </script>
</head>
<body onload="funLoad()" style="margin-left:0; margin-right:0; margin-top:0; margin-bottom:0;">
    <form id="frmContent" runat="server">
        <table id="tblBody" runat="server" border="0" style="height:50; display:none; width:100%" cellpadding="0" cellspacing="0">
            <tr style="height:22px" align="center">
                <td colspan="2" style="background: url(../images/title_bg.gif); width:100%" align="center"><uc:Title ID="ucTitle" runat="server" /></td>
                <td style="background: url(/images/title_bg.gif)">&nbsp;</td>
            </tr>
            <tr id="trFun" runat="server" style="height:28px;">
                <td><uc:ButtonPermission ID="ucButtonPermission" runat="server" /></td>
                <td align="right"><input id="txtMsg" readonly="true" style="text-align:right; display:none; border:0; font-size:12px; color:blue; width:100%" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
