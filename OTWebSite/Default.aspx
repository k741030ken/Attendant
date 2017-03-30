<%@ Page Language="VB" Debug="true" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
    <script type="text/javascript" language="javascript" src="../ClientFun/DisableRightClick.js"></script>
    <script type="text/javascript" language="javascript" src="../ClientFun/ClientFun.js"></script>
    <script type="text/javascript" language="javascript">
    <!--
        function funOnLoad()
        {
            //setStatus("none");
//            frmContent.txtUserID.value = '113699';
        }
        
        function funAction(action)
        {
            switch(action)
            {
                case "changepwd":
                    var objchange = document.getElementById("btnChangePwd");
                    var objlogin = document.getElementById("btnLogin");
                    
                    if (objchange != null && objlogin != null)
                    {
                        if (objchange.value == "修改密碼")
                        {
                            setStatus("block");
                            objchange.value = "取消修改";
                            objlogin.value = "确定修改";
                            frmContent.txtUserID.focus();
                        }
                        else
                        {
                            setStatus("none");
                            objchange.value = "修改密碼";
                            objlogin.value = "登入系統";
                        }
                    }
                    return false;
                    break;
                case "login":
//                    if (frmContent.txtUserID.value == "")
//                        frmContent.txtUserID.value = "113699";
//                        
//                    if (frmContent.txtPwd.value == "")
//                        frmContent.txtPwd.value = '0000';

                    if (checkData())
                        frmContent.btnRunLogin.click();
                    break;
            }
        }
        
        function checkData()
        {
            if (frmContent.txtUserID.value.length == 0)
            {
                alert("請輸入使用者帳號");
                frmContent.txtUserID.focus();
                return false;
            }
            
//            if (frmContent.txtPwd.value.length == 0)
//			{
//			    window.alert("請輸入使用者密碼");
//				frmContent.txtPwd.focus();
//				return false;
//			}

			if (frmContent.txtPwd.value == "NEWUSER" && trNewPwd.style.display == "none")
			{
			    window.alert("請修改密碼後重新登入");
				funAction('changepwd');
				frmContent.txtNewPwd.focus();
				return false;
			}
			
			if (trNewPwd.style.display == "block")
			{
				
				if (frmContent.txtNewPwd.value.length == 0) 
				{
				    window.alert("請輸入使用者新密碼");
					frmContent.txtNewPwd.focus();
					return false;
				}
				
				if (frmContent.txtPwd.value == frmContent.txtNewPwd.value)
				{
				    window.alert("舊密碼與新密碼不可相同");
					frmContent.txtPwd.focus();
					return false;
				}

				if (frmContent.txtNewPwd.value != frmContent.txtConfirmPwd.value)
				{
				    window.alert("新密碼與確認密碼不同");
					frmContent.txtNewPwd.focus();
					return false;
				}
			}
			
			return true;
        }
        
        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                if (checkData())
                    frmContent.btnRunLogin.click();
            }
        }
        
        function setStatus(show)
        {
            trNewPwd.style.display = show;
            trConfPwd.style.display = show;
        }
    -->
    </script>
</head>
<body onload="return funOnLoad();" style="BACKGROUND-ATTACHMENT: fixed; BACKGROUND-IMAGE: url(images/signin2.jpg); BACKGROUND-REPEAT: no-repeat; background-color:#3282e3">
    <form id="frmContent" runat="server">
    <div style="LEFT: 320px; WIDTH: 238px; PADDING-TOP: 50px; POSITION: absolute; TOP: 130px; HEIGHT: 256px; text-align:left">
        <table style="WIDTH: 237px; HEIGHT: 195px" border="0">
			<tr>
				<td style="HEIGHT: 155px" valign="bottom" align="center">
					<table style="WIDTH: 226px" border="0">
						<tr>
							<td style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 102px; COLOR: #606261; HEIGHT: 31px" align="center" colspan="0" rowspan="0"><asp:label ID="lblUserID" runat="server" Text="使用者帳號" ></asp:label></td>
							<td style="HEIGHT: 31px"><asp:textbox id="txtUserID" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; TEXT-TRANSFORM: uppercase; BORDER-LEFT: 1px solid; COLOR: black; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: white;" runat="server" BorderColor="Gray" MaxLength="6" Width="100px" Font-Size="9pt" >102119</asp:textbox></td>
						</tr>
						<tr style="display:none">
							<td style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 102px; COLOR: #606261; HEIGHT: 25px" align="center"><asp:label ID="lblPassword" runat="server" Text="使用者密碼" ></asp:label></td>
							<td style="HEIGHT: 25px"><asp:textbox id="txtPwd" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; COLOR: black; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: white" runat="server" BorderColor="Gray" MaxLength="30" TextMode="Password" Width="100px" Font-Size="9pt" ></asp:textbox></td>
						</tr>
						<tr id="trNewPwd" style="DISPLAY: none">
							<td style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 102px; COLOR: #606261; HEIGHT: 29px" align="center"><asp:label ID="lblNewPassword" runat="server" Text="新密碼" ></asp:label></td>
							<td style="HEIGHT: 29px"><asp:textbox id="txtNewPwd" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; COLOR: black; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: white" runat="server" BorderColor="Gray" MaxLength="30" TextMode="Password" Width="100px" Font-Size="9pt" ></asp:textbox></td>
						</tr>
						<tr id="trConfPwd" style="DISPLAY: none">
							<td style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 102px; COLOR: #606261" align="center"><asp:label ID="lblConfirmPassword" runat="server" Text="確認密碼" ></asp:label></td>
							<td><asp:textbox id="txtConfirmPwd" style="BORDER-RIGHT: 1px solid; BORDER-TOP: 1px solid; BORDER-LEFT: 1px solid; COLOR: black; BORDER-BOTTOM: 1px solid; BACKGROUND-COLOR: white" runat="server" BorderColor="Gray" MaxLength="30" TextMode="Password" Width="100px" Font-Size="9pt"></asp:textbox></td>
						</tr>
					</table>
					<font face="新細明體">&nbsp;</font>
				</td>
			</tr>
			<tr>
				<td style="height: 54px">
					<table width="100%" border="0">
						<tr>
							<td align="center" style="width:50%">
							    <asp:button id="btnRunLogin" runat="server" Width="93px" Font-Size="11pt" Font-Bold="True" ForeColor="White" BackColor="#9999FF" Height="27px" Text="登入系統" style="display:none"></asp:button>
							    <input type="button" id="btnLogin" style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 92px; COLOR: white; HEIGHT: 27px; BACKGROUND-COLOR: #9999ff" value="登入系統" onclick="return funAction('login');" />
							</td>
							<td align="center" style="width:50%"><asp:button id="btnRunChange" runat="server" style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 92px; COLOR: white; HEIGHT: 27px; BACKGROUND-COLOR: #9999ff; display:none" Text="修改密碼" OnClientClick="return funAction('changepwd');"></asp:button>
							    <input type="button" id="btnChangePwd" style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; WIDTH: 92px; COLOR: white; HEIGHT: 27px; BACKGROUND-COLOR: #9999ff" value="修改密碼" onclick="return funAction('changepwd');" />
							</td>
						</tr>
					</table>
			</tr>
		</table>
    </div>
    </form>
</body>
</html>
