<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RSSTest.aspx.cs" Inherits="RSSTest" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>RSS</title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset class='Util_Fieldset' style='width: 350px; height: 350px; margin-bottom:50px;'>
            <legend class="Util_Legend">天氣</legend>
            <asp:Xml ID="xmlRSS1" runat="server"></asp:Xml>
        </fieldset>
        <fieldset class='Util_Fieldset' style='width: 500px; height: auto;'>
            <legend class="Util_Legend">新聞</legend>
            <asp:Xml ID="xmlRSS2" runat="server"></asp:Xml>
        </fieldset>
    </form>
</body>
</html>
