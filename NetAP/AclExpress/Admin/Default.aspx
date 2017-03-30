<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AclExpress_Admin_Default" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>AclExpress</title>
</head>
<frameset cols="220px,*" id="frmMain" name="frmMain" runat="server" framespacing="3" >
    <frame id="frmMenu"      name="frmMenu"      runat="server" src="AclMenu.aspx" scrolling="auto" />
    <frame id="frmContent"   name="frmContent"   runat="server" src="../AclInfo.aspx" scrolling="auto" />
</frameset>
<body >
    <div id="divFrmErr" runat="server">
        <h3><asp:Label ID="labFrameSetError" runat="server" ></asp:Label></h3>
    </div>
    <div id="divClose" runat="server" style="display: none;">
        <p>
            <asp:Label ID="labErrMsg" runat="server" style="line-height:30px;" ></asp:Label>
            <br />
            <input type="button" id="btnCloseWindow" runat="server" value="Close" onclick="javascript: self.close();"
                style="width: 200px;" />
        </p>
    </div>
</body>
</html>