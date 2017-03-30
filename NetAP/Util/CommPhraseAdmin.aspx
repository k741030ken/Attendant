<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommPhraseAdmin.aspx.cs" Inherits="Util_CommPhraseAdmin" %>

<%@ Register Src="~/Util/ucTextBox.ascx" TagPrefix="uc1" TagName="ucTextBox" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CommPhraceAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>
        <div id="divEdit" runat="server">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <asp:Label ID="labEdit" runat="server" Text="Edit"></asp:Label>
                </legend>
                <div class="Util_Center">
                    <uc1:ucTextBox runat="server" ID="txtPhrase" ucRows="10" ucWidth="450" />
                    <hr class="Util_clsHR" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="350" CssClass="Util_clsBtn" OnClick="btnSave_Click" />
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
