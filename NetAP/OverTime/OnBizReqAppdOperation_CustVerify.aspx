<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizReqAppdOperation_CustVerify.aspx.cs" Inherits="Overtime_OvertimeCustVerify" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="labMsg" runat="server" Text=""></asp:Label>
    <br /><br />
    <asp:TextBox ID="txtErrMsg" runat="server" ReadOnly="True" Rows="18"
            TextMode="MultiLine" Width="630px" Visible=false></asp:TextBox>
    <br />
    <%--<asp:Label ID="ErrMsgs" runat="server" Visible="False"></asp:Label>--%>
        <br />
    <%--<asp:Button ID="btnClose" runat="server" CssClass="Util_clsBtnGray" 
            OnClientClick="javascript:parent.window.location = parent.window.location.href;" 
            Text="返回待辦清單" Width="300px" onclick="btnClose_Click"></asp:Button>--%>
        
    </div>
    </form>
</body>
</html>
