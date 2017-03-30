<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachAdminLite.aspx.cs" Inherits="Util_AttachAdminLite" %>

<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ucAttachList.ascx" TagName="ucAttachList" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title><%= Resources.Attach_FileUpload %>(Lite)</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <%--檔案上傳--%>
        <div id="DivUploadArea" runat="server" style="width: 620px;">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <%= Resources.Attach_FileUpload %>(Lite)</legend>
                <div id="DivUploadObj" runat="server" style="margin: 0px;">
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
                                <asp:Label ID="labFileTotSizeInfo" runat="server" Font-Size="small"></asp:Label>
                                <div style='width: 200px; height: 8px; text-align: left; margin: 0px; margin-top: 2px; border: 1px solid #333;'>
                                    <div id="DivQuotaStatus" runat="server" style='height: 8px; background-color: #3636A0;'>
                                    </div>
                                </div>
                                <asp:Label ID="labFileExtNameInfo" runat="server" Font-Size="small"></asp:Label>
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="DivError" runat="server">
                    <asp:Label ID="labErrMsg" runat="server" Text="" ForeColor="Red" CausesValidation="false"></asp:Label>
                </div>
            </fieldset>
        </div>
        <br />
        <%--檔案列表--%>
        <div id="DivListArea" runat="server" style="width: 620px;">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <%= Resources.Attach_FileList %></legend>
                <uc1:ucAttachList ID="ucAttachList1" runat="server" ucWidth="590" ucHeight="200"
                    CausesValidation="false" />
            </fieldset>
        </div>
    </form>
</body>
</html>
