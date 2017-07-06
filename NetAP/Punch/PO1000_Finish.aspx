<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO1000_Finish.aspx.cs" Inherits="CheckIn_PO1000_FinishView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>打卡完成</title>
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
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblEmpID_NameN" runat="server" Text="員編姓名"></asp:Label>
                        </td>
                        <td width="80%" align="left">
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

                    <tr id="Tr1" class="Util_clsRow1" runat="server">
                        <td width="100%" align="center" colspan = "2">
                         <asp:Label ID="ResultMsg" runat="server" Width="100%"></asp:Label>
                         <br />
                         <asp:Label ID="RemindMsg" runat="server" Width="100%"></asp:Label>
                         <br />
                         <asp:Label ID="CareMsg" runat="server" Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    </form>
</body>
</html>
