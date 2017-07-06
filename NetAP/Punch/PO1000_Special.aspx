<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO1000_Special.aspx.cs" Inherits="CheckIn_PO1000_SpecialView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>公出查詢</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <fieldset class="Util_Fieldset">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height:20px">
                    <td colspan="3" align="left">
                        <asp:Button ID="btnCheckIn" runat="server" Text="出退勤打卡" onclick="btnCheckIn_Click" CssClass="Util_clsBtn" Width = "8%" />
                        <asp:Label ID="Space1" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnNapCheckIn" runat="server" Text="午休打卡" onclick="btnNapCheckIn_Click" CssClass="Util_clsBtn" Width = "8%" />
                    </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblEmpID_NameN" runat="server" Text="員編姓名"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="EmpID_NameN" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>

                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblOBFormStatus" runat="server" Text="打卡時間" ></asp:Label>
                        </td>
                        <td width="80%" align="left">
                         <asp:Label ID="PunchDateTime" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>

                    <tr class="Util_clsRow3" runat="server">
                        <td colspan="3" align="center" style="padding-top: 2em">請選擇您的打卡類別</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td><br /></td>
        </tr>
    </table>
    </fieldset>
    </form>
</body>
</html>
