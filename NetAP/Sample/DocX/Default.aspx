<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sample_DocX_Default" %>

<%@ Register Src="~/Util/ucProcessButton.ascx" TagPrefix="uc1" TagName="ucProcessButton" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">DocX 套版</legend>
                <uc1:ucProcessButton runat="server" ID="btnProcess1" ucBtnCaption="套版並匯出" />
                <br /><br />
                ** 底層使用開源第三方 <b>[DocX.dll]</b> 元件進行轉換，詳細用法請上 <asp:HyperLink ID="HyperLink1" NavigateUrl="https://github.com/WordDocX/DocX" Target="_blank" runat="server">官網</asp:HyperLink> **
                <br />
            </fieldset>
        </div>
    </form>
</body>
</html>
