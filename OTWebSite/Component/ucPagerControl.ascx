<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucPagerControl.ascx.vb" Inherits="Component_ucPagerControl" %>
<table style="font-size:12px; width:100%" border="0" cellpadding="0" cellspacing="0">
    <tr style="height:12px">
        <td align="left">
             <asp:LinkButton ID="btnFirstPage" Enabled="false" runat="server" Text="第一頁" ForeColor="blue"></asp:LinkButton>&nbsp;
             <asp:LinkButton ID="btnPreviousPage" Enabled="false" runat="server" Text="上一頁" ForeColor="blue"></asp:LinkButton>&nbsp;
             <asp:LinkButton ID="btnNextPage" Enabled="false" runat="server" Text="下一頁" ForeColor="blue"></asp:LinkButton>&nbsp;
             <asp:LinkButton ID="btnLastPage" Enabled="false" runat="server" Text="最後頁" ForeColor="blue"></asp:LinkButton>
        </td>
        <td align="right">
            <asp:Label ID="lblPageNo" runat="server" Font-Names="新細明體" ForeColor="Blue" Width="30px" style="text-align:center; border-right: dimgray 0px solid; border-top: dimgray 0px solid; border-left: dimgray 0px solid; border-bottom: dimgray 1px solid"></asp:Label>&nbsp;
            <asp:Label ID="label8" runat="server" Text="/" Font-Names="新細明體"></asp:Label>&nbsp;
            <asp:Label ID="lblPageCount" runat="server" Font-Names="新細明體" ForeColor="Blue" Width="30px" style="text-align:center; border-right: dimgray 0px solid; border-top: dimgray 0px solid; border-left: dimgray 0px solid; border-bottom: dimgray 1px solid"></asp:Label>&nbsp;
            <asp:Label ID="label9" runat="server" Text="/" Font-Names="新細明體"></asp:Label>&nbsp;
            <asp:Label ID="lblRecordCount" runat="server" Font-Names="新細明體" ForeColor="Blue" Width="30px" style="text-align:center; border-right: dimgray 0px solid; border-top: dimgray 0px solid; border-left: dimgray 0px solid; border-bottom: dimgray 1px solid"></asp:Label>&nbsp;
            <asp:Label ID="label1" runat="server" Text="(頁次/總頁數/筆數)" Font-Names="新細明體"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="label4" runat="server" Text="每頁顯示：" Font-Names="新細明體"></asp:Label>
            <asp:TextBox ID="txtPerPageRecord" runat="server" ForeColor="blue" Width="30px" style="Text-align:center; BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid"></asp:TextBox>
            <asp:Label ID="label5" runat="server" Text="筆" Font-Names="新細明體"></asp:Label>
            <asp:ImageButton ID="btnPerPageRecord" runat="server" ImageUrl="~/images/go.gif" />&nbsp;&nbsp;&nbsp;
            <asp:Label ID="label6" runat="server" Text="至第" Font-Names="新細明體"></asp:Label>
            <asp:TextBox ID="txtGoPage" runat="server" ForeColor="blue" Width="30px" style="Text-align:center; BORDER-RIGHT: dimgray 1px solid; BORDER-TOP: dimgray 1px solid; BORDER-LEFT: dimgray 1px solid; BORDER-BOTTOM: dimgray 1px solid"></asp:TextBox>
            <asp:Label ID="label7" runat="server" Text="頁" Font-Names="新細明體"></asp:Label>
            <asp:ImageButton ID="btnGoPage" runat="server" ImageUrl="~/images/go.gif" />&nbsp;
        </td>
    </tr>
</table>