<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MuiAdmin.aspx.cs" Inherits="Util_MuiAdmin" %>

<!DOCTYPE html >
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DivError" runat="server" visible="false" >
        <asp:Label ID="labErrMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
    </div>
    <div id="DivNormal" runat="server" visible="true" >
        <table width="100%" border="0">
            <tr>
                <td>
                </td>
                <td colspan="3" align="left">
                    資料表：
                    <asp:Label ID="labTableName" runat="server" ForeColor="#0066FF"></asp:Label>
                    　語系欄位：
                    <asp:DropDownList ID="ddlMuiColumn" runat="server"  AutoPostBack="true"
                        onselectedindexchanged="ddlMuiColumn_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="Util_gvHeader" align="center">
                <td width="50">
                    序號
                </td>
                <td width="80">
                    語系
                </td>
                <td width="350">
                    內容
                </td>
                <td width="100">
                    
                </td>
            </tr>
            <tr class="Util_gvRowNormal">
                <td align="right">
                    ＊
                </td>
                <td>
                    <asp:TextBox ID="txtDefCode" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtDefData" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    (預設語系)
                </td>
            </tr>
            <tr class="Util_gvRowAlternate">
                <td align="right">
                    1
                </td>
                <td>
                    <asp:TextBox ID="txtCode01" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtData01" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnToSimp01" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="简体" CommandArgument="txtData01" onclick="btnToSimp_Click" />
                    <asp:Button ID="btnToTrad01" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="繁體" CommandArgument="txtData01" onclick="btnToTrad_Click" />
                </td>
            </tr>
            <tr class="Util_gvRowNormal">
                <td align="right">
                    2
                </td>
                <td>
                    <asp:TextBox ID="txtCode02" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtData02" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="简体" CommandArgument="txtData02" onclick="btnToSimp_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="繁體" CommandArgument="txtData02" onclick="btnToTrad_Click" />                </td>
            </tr>
            <tr class="Util_gvRowAlternate">
                <td align="right">
                    3
                </td>
                <td>
                    <asp:TextBox ID="txtCode03" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtData03" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="简体" CommandArgument="txtData03" onclick="btnToSimp_Click" />
                    <asp:Button ID="Button4" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="繁體" CommandArgument="txtData03" onclick="btnToTrad_Click" />                </td>
            </tr>
            <tr class="Util_gvRowNormal">
                <td align="right">
                    4
                </td>
                <td>
                    <asp:TextBox ID="txtCode04" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtData04" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="简体" CommandArgument="txtData04" onclick="btnToSimp_Click" />
                    <asp:Button ID="Button6" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="繁體" CommandArgument="txtData04" onclick="btnToTrad_Click" />                </td>
            </tr>
            <tr class="Util_gvRowAlternate">
                <td align="right">
                    5
                </td>
                <td>
                    <asp:TextBox ID="txtCode05" runat="server" Width="80"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtData05" runat="server" Width="350"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button7" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="简体" CommandArgument="txtData05" onclick="btnToSimp_Click" />
                    <asp:Button ID="Button8" runat="server" CssClass="Util_clsBtn" Width="36" 
                        Text="繁體" CommandArgument="txtData05" onclick="btnToTrad_Click" />                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <hr id="divHR" runat="server" class="Util_clsHR" style="width: 100%;" />
                    <asp:Button ID="btnSave" runat="server" CssClass="Util_clsBtn" width="200" 
                        Text="儲　　存" onclick="btnSave_Click" />　　
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
