<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadReportTemplate.aspx.cs" Inherits="Template_DownloadReportTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DownloadReportTemplate</title>
</head>
<body>
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
    <div>
    
<input type="file" id="File1" name="File1" runat="server" />
 <asp:Button ID="ReadExcel01" runat="server" Height="60px" 
            onclick="ReadExcel01_Click" Text="upload" Width="310px" />
        <br />
    <asp:Button ID="ReadExcel02" runat="server" Height="60px" 
            onclick="ReadExcel02_Click" Text="讀取Excel2" Width="310px" />
        <br />
        <asp:Button ID="OpenXmlWordDownload01" runat="server" Height="60px" 
            onclick="OpenXmlWordDownload01_Click" Text="下載基本套表(Word)" Width="310px" />
        <br />
        <asp:Button ID="OpenXmlWordDownload02" runat="server" Height="60px" 
            onclick="OpenXmlWordDownload02_Click" Text="下載取消額度通知書(Word)" Width="310px" />
        <br />
        <asp:Button ID="OpenXmlWordDownload03" runat="server" Height="60px" 
            onclick="OpenXmlWordDownload03_Click" Text="下載值勤表(Word)" Width="310px" />
        <br />
        <asp:Button ID="DocxWordDownload01" runat="server" Height="60px" 
            onclick="DocxWordDownload01_Click" Text="下載docx.dll基本套表(Word)" 
            Width="310px" />
        <br />
        <asp:Button ID="DocxWordDownload02" runat="server" Height="60px" 
            onclick="DocxWordDownload02_Click" Text="下載docx.dll基本Table套表(Word)" 
            Width="310px" />
        <br />
        <asp:Button ID="DocxWordDownload03" runat="server" Height="60px" 
            onclick="DocxWordDownload03_Click" Text="測試值勤套表" 
            Width="310px" />
        <br />
    </div>   
    </form>
</body>
</html>
