<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Util_Upload" UICulture="auto" %>

<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title><%= Resources.Attach_FileUpload %></title>
    <link href="./Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <%--檔案上傳--%>
        <div id="DivUploadArea" runat="server" style="width: 610px;">
            <fieldset class="Util_Fieldset" style="height: 90px;">
                <legend class="Util_Legend">
                    <%= Resources.Attach_FileUpload %></legend>
                <div id="DivMsg" runat="server" style="padding-top: 20px;">
                    <asp:Label ID="labMsg" runat="server" Text=""></asp:Label>
                </div>
                <div id="DivUploadObj" runat="server">
                    <table style="border: 0px none #999999; vertical-align: top;">
                        <tr>
                            <td style="width: 100px;">
                                <input type="file" name="file_upload" id="file_upload" />
                            </td>
                            <td style="width: 200px; padding-left: 15px;">
                                <asp:Label ID="labFileMaxQty" runat="server" Font-Size="small"></asp:Label>
                                <br />
                                <asp:Label ID="labFileSizeInfo" runat="server" Font-Size="small"></asp:Label>
                                <br />
                                <asp:Label ID="labFileExtNameInfo" runat="server" Font-Size="small"></asp:Label>
                                <br />
                            </td>
                            <td style="width: 270px;">
                                <div id="fileQueue">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
