<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachAdmin.aspx.cs" Inherits="Util_AttachAdmin" UICulture="auto" %>

<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register Src="ucAttachList.ascx" TagName="ucAttachList" TagPrefix="uc1" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title><%= Resources.Attach_FileUpload %></title>
    <link href="./Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <%--檔案上傳--%>
        <div id="DivUploadArea" runat="server" style="width: 620px;">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <%= Resources.Attach_FileUpload %></legend>
                <div id="DivUploadObj" runat="server" style="margin: 0px;">
                    <table id="tabUpload" runat="server" style="border: 0px solid #999999; padding: 0px; vertical-align: top;">
                        <tr>
                            <td style="width: 100px;">
                                <input type="file" name="file_upload" id="file_upload" />
                            </td>
                            <td style="width: 230px; padding-left: 15px; padding-bottom: 5px; vertical-align: top;">
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
                            </td>
                            <td style="width: 250px; text-align: left; vertical-align: top;">
                                <div id="fileQueue">
                                </div>
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
