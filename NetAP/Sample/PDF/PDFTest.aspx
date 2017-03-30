<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDFTest.aspx.cs" Inherits="PDFTest" %>

<!DOCTYPE html>

<html >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p><asp:Button ID="btnMerge" runat="server" Text="PDF 合併" OnClick="btnMerge_Click" /></p>
        <p><asp:Button ID="btnLayout01" runat="server" Text="PDF 網頁套版(自訂頁首頁尾)" OnClick="btnLayout01_Click" /></p>
        <p><asp:Button ID="btnLayout02" runat="server" Text="PDF 網頁套版(內建頁首頁尾)" OnClick="btnLayout02_Click" /></p>
    </div>
    </form>
</body>
</html>
