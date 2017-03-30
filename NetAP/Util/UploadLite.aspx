<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadLite.aspx.cs" Inherits="Util_UploadLite" %>

<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html >
<head runat="server">
    <title><%= Resources.Attach_FileUpload %>(Lite)</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <%--檔案上傳--%>
        <div id="DivUploadArea" runat="server" style="width: 610px;">
            <fieldset class="Util_Fieldset" style="height: 90px;">
                <legend class="Util_Legend">
                    <%= Resources.Attach_FileUpload %>(Lite)</legend>
                <div id="DivMsg" runat="server" style="padding-top: 20px;">
                    <asp:Label ID="labMsg" runat="server" Text="" ></asp:Label>
                </div>
                <div id="DivUploadObj" runat="server">
                    <table style="border: 0px none #999999; vertical-align: top;">
                        <tr>
                            <td style="width: 300px;">
                                <asp:Image ID="imgUpload" runat="server" ImageUrl="~/Util/WebClient/Legend_Waiting.gif" Height="48" />
                                <asp:AsyncFileUpload ID="File1" Width="290px" ToolTip="" runat="server" ThrobberID="imgUpload" UploaderStyle="Modern" OnClientUploadStarted="UploadStart" OnClientUploadComplete="UploadEnd" />
                            </td>
                            <td style="width: 200px; padding-left: 15px;">
                                <asp:Label ID="labFileMaxQty" runat="server" Font-Size="small"></asp:Label>
                                <br />
                                <asp:Label ID="labFileSizeInfo" runat="server" Font-Size="small"></asp:Label>
                                <br />
                                <asp:Label ID="labFileExtNameInfo" runat="server" Font-Size="small"></asp:Label>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
