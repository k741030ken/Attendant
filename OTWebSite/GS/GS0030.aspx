<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS0030.aspx.vb" Inherits="GS_GS0030" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript">
    <!--
        function redirectPage(Path) {
            var aryParam = new Array();
            aryParam = Path.split(',');

            if (aryParam[1].toString().toUpperCase().indexOf('SC0031.ASPX') >= 0) {
                OpenWindow(aryParam[1]);
            }
            else {
                //showSubmitting(window.top);
                try {
                    window.parent.frames[3].location = "../SC/SC0050.aspx?FunID=" + aryParam[0] + "&Path=" + aryParam[1] + "&CompRoleID=" + aryParam[2];
                    window.parent.frames[2].imgSplit.src = "../images/mclose_r.png";
                    window.parent.frames[2].imgSplit.alt = "Open menu";
                    window.parent.frames[2].imgSplit_WorkFlow.style.display = 'none';
                    window.parent.frmSet.cols = "0,0,20,*";
                    //window.parent.frames[2].document.bgColor = '#c10c0c'; //20150320 wei del
                    window.parent.frames[2].document.bgColor = '#f1f1f1';   //20150320 wei modify
                    showSubmitting();
                    //checkStatus();
                    window.setTimeout("checkStatus()", 500);
                }
                catch (ex) {
                    window.setTimeout("redirectPage('" + Path + "')", 500);
                }
            }
        }

        function checkStatus() {
            var msg;
            try {
                //if (window.parent.frames[3].
                var obj = window.parent.frames[3].frames[1].document.getElementById('__ActionParam');
                msg = obj.value;
                hidePopupWindow();
            }
            catch (ex) {
                window.setTimeout("checkStatus()", 100);
            }
        }

        
    -->
    </script>
</head>
<body style="margin-right:10; margin-left:10; margin-top:10; margin-bottom:10; background: url(../images/menu_bg.gif)">
    <form id="frmContent" runat="server" style="margin-left:10px">
    </form>
</body>
</html>
