<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchAppdOperation_CustVerify.aspx.cs"
    Inherits="Punch_PunchAppdOperation_CustVerify" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
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
