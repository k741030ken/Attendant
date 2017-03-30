<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs"  Inherits="ErrorPage" UICulture="auto" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend"><asp:Label ID="labApplicationName" runat="server" Text="" /></legend><br />
            <asp:Label ID="labErrType" runat="server" Text="" />
            <asp:Label ID="labErrMsg" runat="server" Text="" /><br />
        </fieldset>
    </div>
    </form>
</body>
</html>
