<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sample_Captcha_Default" %>

<%@ Register Src="~/Util/ucTextBox.ascx" TagPrefix="uc1" TagName="ucTextBox" %>


<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Captcha Test</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">圖形鎖範例</legend>
                <div id="divCaptcha" runat="server">
                    <div class="Util_clsRow1">
                        <asp:Image ID="imgCaptcha" runat="server" BorderColor="DarkGray" BorderWidth="1" />
                        <uc1:ucTextBox runat="server" ID="txtChkCode" ucCaption="請輸入驗證碼：" ucCaptionWidth="120" ucIsRequire="true" />
                    </div>
                    <div class="Util_clsRow2">
                        <asp:Button ID="btnVerify" Text="進行驗證" CssClass="Util_clsBtn" runat="server" Width="270" OnClick="btnVerify_Click" />
                    </div>
                </div>
                <div id="divVerify" runat="server">
                    <asp:Label ID="labMsg" runat="server" Text=""></asp:Label>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
