<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachDownload.aspx.cs" Inherits="Util_AttachDownload" UICulture="auto" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register src="ucAttachList.ascx" tagname="ucAttachList" tagprefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Download</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DivError" runat="server" visible="false" >
        <asp:Label ID="labErrMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <div id="DivNormal" runat="server" visible="true" >
        <div id="DivFileList" runat="server">
            <fieldset class="Util_Fieldset" ><legend class="Util_Legend"><%=Resources.Attach_FileList%></legend>
                    <uc1:ucAttachList ID="ucAttachList1" runat="server" ucWidth="590" ucHeight="240" />
             </fieldset>
        </div>    
    </div>
    </form>
</body>
</html>
