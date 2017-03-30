<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowCustProperty.aspx.cs" Inherits="FlowExpress_Admin_FlowCustProperty" %>

<%@ Register Src="~/Util/ucTextBox.ascx" TagPrefix="uc1" TagName="ucTextBox" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FlowCustProperty</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>
        <div id="divMain" runat="server">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">FlowCustProperty</legend>
                <div class="Util_Center">
                    <div class="Util_clsRow1">
                        <uc1:ucTextBox runat="server" ID="txtMailFrom" ucCaption="發信代表(MailFrom)" ucCaptionWidth="160" ucWidth="450" />
                        <li style="color: Gray;">預設值：[<asp:Label ID="labMailFrom" runat="server"></asp:Label>]</li>
                    </div>
                    <div class="Util_clsRow2">
                        <uc1:ucTextBox runat="server" ID="txtPhrase" ucCaption="流程片語(Phrase)" ucCaptionWidth="160" ucRows="5" ucWidth="450" />
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <hr class="Util_clsHR" />
                    <asp:Button ID="btnSave" runat="server" Text="儲　　存" Width="350" CssClass="Util_clsBtn" OnClick="btnSave_Click" />
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
