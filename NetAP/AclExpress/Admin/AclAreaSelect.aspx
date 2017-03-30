<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclAreaSelect.aspx.cs" Inherits="AclExpress_Admin_AclAreaSelect" %>

<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagPrefix="uc1" TagName="ucCommSingleSelect" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:ucCommSingleSelect runat="server" ID="AclArea" ucCaption="授權區域" ucDropDownSourceListWidth="350" ucIsSearchEnabled="false" ucIsRequire="true"/>
            <asp:Button ID="btnSelect" runat="server" Text="Go" OnClick="btnSelect_Click" />
        </div>
    </form>
</body>
</html>
