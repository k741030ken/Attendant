<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0010.aspx.vb" Inherits="SC_SC0010" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
		<style type="text/css">
			A { FONT-SIZE: 11px; COLOR: #0033cc; LINE-HEIGHT: 20px; FONT-FAMILY: arial; LETTER-SPACING: 0px; TEXT-DECORATION: none; font-color: #000000 }
			A:hover { COLOR: red; TEXT-DECORATION: none }
			B { FONT-SIZE: 11px; COLOR: #000066; LINE-HEIGHT: 20px; FONT-FAMILY: arial; LETTER-SPACING: 0px }
			.baner_item { BORDER-RIGHT: silver 1pt solid; PADDING-RIGHT: 1px; BORDER-TOP: silver 1pt solid; PADDING-LEFT: 1px; FONT-SIZE: 12px; BORDER-LEFT: silver 1pt solid; WIDTH: 66px; CURSOR: hand; COLOR: white; PADDING-TOP: 2px; BORDER-BOTTOM: silver 1pt solid; FONT-FAMILY: 新細明體; HEIGHT: 12px; BACKGROUND-COLOR: #9999ff; TEXT-ALIGN: center }
			.baner_item_blank { BORDER-RIGHT: 1pt; PADDING-RIGHT: 1px; BORDER-TOP: 1pt; PADDING-LEFT: 1px; FONT-SIZE: 12px; BORDER-LEFT: 1pt; WIDTH: 66px; PADDING-TOP: 2px; BORDER-BOTTOM: 1pt; FONT-FAMILY: 新細明體; HEIGHT: 12px; TEXT-ALIGN: center }
		</style>
	<script type="text/javascript" language="javascript">
	<!--
	    function funLoad()
		{
			if (frmContent.txtStatus.value=="0")
			{
				 funBackhome();
			}
		}
		
		function funChangePwd()
		{
		    alert("網頁尚在製作中~~");
		    /*
			var strRtn;
			try
			{
				strRtn = window.showModalDialog("../A11/ECA110.aspx" ,"","dialogHeight:290px; dialogWidth: 292px; edge: Raised;center: Yes; help: No; resizable: No; status: No;").toString();
			}
			catch (ex)
			{	
				strRtn = "";
			}
			*/
		}
		
		function ChangeColor(obj, sColor)
		{
			obj.style.color = sColor;
		}
		
		function ChangeColor2(obj, sColor)
		{
		}

		function funContactUs() {
		/*
		    var strRtn;
		    strRtn = ShowDialogWithHeader2("?FunID=SC0070&Path=../SC/SC0070.aspx", "640", "480");
		    return false;
		    */
			//window.top.frames("fraContent").location = "/A00/ECA060.aspx?Path=/A00/ECA080.aspx&FunID=ECA080"
		}
		
		function funChangeImg(obj, sType)
		{
		}
		
		function funChangeImgAgent(obj, sType)
		{
			if (sType == '1')
			{					
				obj.src = '../images/EndAgBtn4.gif';
			}
			else
			{				
				obj.src = '../images/EndAgBtn3.gif';
			}
		}
		
		function funBackhome()
		{
		    window.parent.frames[1].frmSet.cols = "230,0,0,*";
		    frmContent.btnBackHome.click();    
		}
		
		function funBackWork()
		{
		    alert("網頁尚在製作中~");
			//window.top.frames[1].location = '/A00/ECA060.aspx?FunID=ECB000&Path=/B00/ECB000.aspx';
		}
		
		function funSetSubMenu(strType)
		{	
			if (strType=="1") 
				{
					tbl_submenu.style.display="block";
				}	
			else
				{
					tbl_submenu.style.display="none";
				}	
				
		}
		
		function funChangeTextColor(strObj,TypeFlg)				
		{
			
			if (TypeFlg=="1")
				{
					tbl_submenu.style.display="block";
					strObj.style.color="#99ffff";
					strObj.style.fontweight="bolder";					
					strObj.style.display="block";
				}					
			else
				{
					tbl_submenu.style.display="none";
					strObj.style.color="white";
					strObj.style.fontweight="";
				}															
		}
	
		function funDocument()
		{
		}
		
		function funDownload()
		{
		}
	-->
	</script>
</head>
<body leftmargin="0" topMargin="0" rightMargin="0" onload="funLoad()" background="../images/bannerbg.gif">
    <form id="frmContent" runat="server">
    <div>
        <table cellspacing="0" cellpadding="0" width="1000px" border="0">
			<tr>
				<td style="vertical-align:text-top" width="200"><IMG src="../images/logo.gif" border="0" ></td>
				<td vAlign="middle" align="left" width="350">
					<table border="0" borderColor="#800000" height="60" cellSpacing="0" cellPadding="0" width="100%"
						align="left">
						<tr vAlign="middle" height="30">
							<td vAlign="middle" align="left" width="100%" style="HEIGHT: 24px"><IMG src="../images/nav_dot.gif">&nbsp;<B><FONT style="FONT-SIZE: 10pt" color="#113093">員工編號：

										<asp:label id="lblUserID" style="BACKGROUND-COLOR: transparent; TEXT-ALIGN: left" Font-Names="Calibri"
											runat="server" Font-Size="10pt" ForeColor="#3A66F3">Label</asp:label>&nbsp;&nbsp;</FONT></B><IMG src="../images/nav_dot.gif">&nbsp;<B><FONT style="FONT-SIZE: 10pt" color="#113093">員工姓名：</FONT><asp:label id="lblUserName" style="BACKGROUND-COLOR: transparent; TEXT-ALIGN: left" Font-Names="Calibri"
										runat="server" Font-Size="10pt" ForeColor="#3A66F3">Label</asp:label></B><asp:Label ID="lblProductionFlag" runat="server" ForeColor="Blue" Font-Size="12px" Font-Bold="True" Text="   (測試環境)"></asp:Label>
							</td>
						</tr>
						<tr>
							<td align="left" valign="top">
								<table border="0" cellpadding="0" cellspacing="0" height="18">
									<tr>
										<td align="left" valign="top" width="185">
											<asp:label width="185px" id="lblAgent" style="BORDER-RIGHT:white 1px solid; BORDER-TOP:white 1px solid; FONT-WEIGHT:bold; BORDER-LEFT:white 1px solid; BORDER-BOTTOM:white 1px solid; BACKGROUND-COLOR:#d1e0ff; TEXT-ALIGN:left; TEXT-VALIGN:TOP"
												runat="server" Font-Size="12px" Font-Names=" Microsoft Sans Serif" ForeColor="Red" Height="19px"></asp:label>
										</td>
										<td>
											<asp:LinkButton width="30px" ID="btCloseAgency" Runat="server">
												<IMG src="../images/EndAgBtn3.gif" border="0" onmouseover="funChangeImgAgent(this, '1')"
													onmouseout="funChangeImgAgent(this, '2')" style="CURSOR: hand"></asp:LinkButton>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr height="20">
							<td>&nbsp;</td>
						</tr>
					</table>
				</td>
				<td vAlign="top" align="right" style="WIDTH: 1px">
					<!--<asp:label id="lb_1" Runat="server">
						<IMG id="imgHome" onmouseover="funChangeImg(this, '1')" style="CURSOR: hand" onclick="funBackhome();"
							onmouseout="funChangeImg(this, '2')" src="../images/home1.gif" border="0"></asp:label>-->
				</td>
				<td width="260" align="right" style="vertical-align:top">
					<!--<div align="right" width="100%">-->
					<table border="0" borderColor="#800000" height="30" cellSpacing="0" cellPadding="0" width="260" style="display:none">
						<tr vAlign="middle" height="14">
							<td width="18%" align="center" onmouseover="ChangeColor2(this, '#fce031')" style="CURSOR: hand"
								onmouseout="ChangeColor2(this, '#fff799')">
								<asp:label id="lb_2" Runat="server">
									<a id="backtoHome" style="color:#fff799; cursor: pointer" onclick="funBackhome()" onmouseover="ChangeColor(this, '#fce031')" onmouseout="ChangeColor(this, '#fff799')">
										回首頁</a>
								</asp:label><asp:label id="lb_3" Runat="server">
									<a id="backtoWork" style="color:#fff799" onclick="funBackWork()" onmouseover="ChangeColor(this, '#fce031')" onmouseout="ChangeColor(this, '#fff799')">
										回待辦</a>
								</asp:label>
								<a style="color:#fff799">│</a>
							</td>
							<td align="center" width="20%" style="CURSOR: hand">
								<a onmouseover="ChangeColor(this, '#fce031')" style="color:#fff799; cursor: pointer" onmouseout="ChangeColor(this, '#fff799')" onclick="return funContactUs();">
									聯絡我們 </a><a style="color:#fff799">│</a>
							</td>
							<td align="center" width="22%" style="CURSOR: hand" onclick="funChangePwd()">
								<a onmouseover="ChangeColor(this, '#fce031')" style="color:#fff799; cursor: pointer" onmouseout="ChangeColor(this, '#fff799')">
									修改密碼&nbsp;&nbsp; </a><a style="color:#fff799">│</a>
							</td>
							<td align="center" width="20%">
							    <asp:LinkButton ID="btnLogout" runat="server" Text="Log Out" ForeColor="#fff799"></asp:LinkButton><asp:LinkButton ID="btnClose" runat="server" Text="关闭" ForeColor="#fff799"></asp:LinkButton>&nbsp;&nbsp; <a style="color:#fff799">│</a>
								<!--<A href="../Default.aspx" style="color:#ffffff" target="_top">登出&nbsp;&nbsp;</A> <a style="color:#ffffff">│</a>-->
							</td>
						</tr>
					</table>
					<!--</div>-->
				</td>
			</tr>
		</table>
		<asp:TextBox ID="txtStatus" style="DISPLAY:none" Runat="server"></asp:TextBox>
		<asp:TextBox ID="txtAgencyType" style="DISPLAY:none" Runat="server"></asp:TextBox>    
		<asp:Button ID="btnBackHome" runat="server" style="display:none" />
		</div>
    </form>
</body>
</html>
