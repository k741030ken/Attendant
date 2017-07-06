<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO1000_Error.aspx.cs" Inherits="CheckIn_PO1000_ErrorView" %>
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
                        <asp:Button ID="btnDefine" runat="server" Text="確定" onclick="btnDefine_Click" CssClass="Util_clsBtn" Width = "8%" />
                        <asp:Label ID="Space1" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnCancel" runat="server" Text="取消" onclick="btnCancel_Click" CssClass="Util_clsBtn" Width = "8%" />
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

                    <tr id="Tr1" class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="Label1" runat="server" Text="異常類型" ></asp:Label>
                        </td>
                        <td width="80%" align="left">
                            <asp:RadioButton ID="rbtAbnormal" GroupName = "AbnormalFlag" Text = "處理公務" runat="server" AutoPostBack="true" oncheckedchanged="rbtAbnormal_CheckedChanged" />
                            <asp:RadioButton ID="rbtunAbnormal" GroupName = "AbnormalFlag" Text = "非處理公務" runat="server" AutoPostBack="true" oncheckedchanged="rbtunAbnormal_CheckedChanged" />
                                    <asp:DropDownList ID="ddlAbnormalReason" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="Tr3" class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="Label5" runat="server" Text="其他說明" ></asp:Label>
                        </td>
                        <td width="80%" align="left">
                         <asp:TextBox ID="AbnormalDesc" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr id="Tr4" class="Util_clsRow2" runat="server">
                        <td width="100%" align="center" colspan="2">
                         <asp:Label ID="ShowMsg" runat="server" ></asp:Label>
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
