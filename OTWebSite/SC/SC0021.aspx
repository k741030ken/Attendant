<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0021.aspx.vb" Inherits="SC_SC0021" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    <!--
        var w;
        function funSplitClick()
        {
            if (window.parent.frmSet.cols.split(',')[0] == '360')
            {
                imgSplit.src = "../images/mclose_r.png";
                imgSplit.alt = "Open menu";
                w = 360;
                funClose();
            }
            else
            {
                imgSplit.src = "../images/mclose.png";
                imgSplit.alt = "Close menu";
                imgSplit_WorkFlow.src = "../images/mopen_r.png";
                imgSplit_WorkFlow.alt = "Open WorkFlow menu";
                w = 0;
                funOpen();
            }
        }
        
        function funOpen()
        {
            document.body.bgColor = '#f1f1f1'; 
            //w = w + 115;
            w = 360;
            if (w > 360) w = 360;
            window.parent.frmSet.cols = w.toString() + ",0,20,*"
            
            if (w < 360)
                window.setTimeout("funOpen()", 5);
        }
        
        function funClose()
        {
            //w = w - 115;
            w = 0;
            if (w <= 0) 
            {
                w = 0;
                //document.body.bgColor = '#c10c0c'; //20150320 wei del
                document.body.bgColor = '#f1f1f1';  //20150320 wei modify
            }
            window.parent.frmSet.cols = w.toString() + ",0,20,*"
            
            if (w > 0)
                window.setTimeout("funClose()", 5);
        }
        
        function funSplitClick_WorkFlow()
        {
            if (window.parent.frmSet.cols.split(',')[1] == '360')
            {
                imgSplit_WorkFlow.src = "../images/mopen_r.png";
                imgSplit_WorkFlow.alt = "Open WorkFlow menu";
                w = 360;
                funWorkFlowClose();
            }
            else
            {
                imgSplit_WorkFlow.src = "../images/mopen.png";
                imgSplit_WorkFlow.alt = "Close WorkFlow menu";
                imgSplit.src = "../images/mclose_r.png";
                imgSplit.alt = "Open menu";
                w = 0;
                funWorkFlowOpen();
            }
        }
        
        function funWorkFlowOpen()
        {
            document.body.bgColor = '#f1f1f1'; 
            //w = w + 115;
            w = 360;
            if (w > 360) w = 360;
            window.parent.frmSet.cols = '0,' + w.toString() + ",20,*"
            
            if (w < 360)
                window.setTimeout("funWorkFlowOpen()", 5);
        }
        
        function funWorkFlowClose()
        {
            //w = w - 115;
            w = 0;
            if (w <= 0)
            {
                w = 0;
                //document.body.bgColor = '#c10c0c'; //20150320 wei del
                document.body.bgColor = '#f1f1f1';  //20150320 wei modify
            }
            window.parent.frmSet.cols = '0,' + w.toString() + ",20,*"
            
            if (w > 0)
                window.setTimeout("funWorkFlowClose()", 5);
        }

        var timer;
        function OpenMenu(type) {
            if (type == 'SplitClick')
                timer = setTimeout('funSplitClick();', 100);
            else
                timer = setTimeout('funSplitClick_WorkFlow();', 100);
        }

        function StopOpen() {
            if (timer != undefined)
                clearTimeout(timer);
        }
    -->
    </script>
</head>
<body style="margin-top:0px; margin-left:0px" bgColor="#f1f1f1">
    <img id="imgSplit" src="../images/mclose_r.png" style="cursor: hand; height: 240px; width: 20px;" alt="Open menu" onclick="funSplitClick();" <%--onmouseover="OpenMenu('SplitClick');" onmouseout="StopOpen();"--%> /><br />
    <img id="imgSplit_WorkFlow" src="../images/mopen_r.png" style="display:none; cursor: hand; height: 240px; width: 20px;" alt="Open WorkFlow menu" onclick="funSplitClick_WorkFlow();" <%--onmouseover="OpenMenu('SplitClick_WorkFlow');" onmouseout="StopOpen();"--%> />
</body>
</html>
