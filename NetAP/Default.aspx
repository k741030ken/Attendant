<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"
    UICulture="auto" %>

<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucPageInfo.ascx" TagName="ucPageInfo" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>
        <%=SinoPac.WebExpress.Common.Util.getAppSetting("app://ApplicationName/")%>
    </title>
    <style type="text/css">
        .style1 {
            position: absolute;
            top: 0%;
            right: 0%;
            color: #FFF;
            cursor: text;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnExecute" defaultfocus="txtUserID">
        <div id="divLogin" runat="server" visible="false">
            <fieldset class="Util_Fieldset" style="width: 450px;">
                <legend class="Util_Legend">Login</legend>
                UserID：
            <uc1:ucTextBox ID="txtUserID" runat="server" ucTextData="" ucIsRequire="true" />
                UI Culture：
            <asp:DropDownList ID="ddlUICulture" runat="server" AutoPostBack="false">
            </asp:DropDownList>
                <br />
                <hr class="Util_clsHR" />
                <center>
                <asp:Button ID="btnExecute" runat="server" CssClass="Util_clsBtn" Width="150px" OnClick="btnExecute_Click" 
                    Text="Local Login" />　　
                <asp:Button ID="btnSessClear" runat="server" CssClass="Util_clsBtn" Width="150px" CausesValidation="false"
                    Text="Clear Session" OnClick="btnSessClear_Click" Visible="false" />
            </center>
            </fieldset>
        </div>
        <br />
        <asp:Label ID="labMsg" runat="server" />
        <uc1:ucPageInfo runat="server" ID="ucPageInfo" Visible="false" />
        <asp:LinkButton ID="btn1" CausesValidation="false" runat="server" CssClass="style1" OnClick="btn1_Click">.</asp:LinkButton>
    </form>
</body>
</html>
