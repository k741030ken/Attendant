<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV1201.aspx.vb" Inherits="OV_OV1200" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <style type="text/css">
        .table1
        {
            border-collapse: collapse;
            border: 1px solid #89b3f5;
            background-color: #f3f8ff;
        }
        .style1
        {
            font-size: 14px;
            font-family:  Calibri, 微軟正黑體;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width: 110px;
        }
        .style2
        {
            font-size: 14px;
            font-family:  Calibri, 微軟正黑體;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width: 110px;
        }
    </style>
    <title>流程改派</title>
    <script type="text/javascript">

        function funAction(Param) {
            switch (Param) {
                case "btnDelete":
                    if (!confirm('確定將調整處理人員作變更？'))
                        return false;
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="table1">
         <tr>
            <td width="20%" class="style1">
                <asp:Label ID="Label5" runat="server" Text="公司代碼" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblCompID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="表單名稱" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblFromName" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="lblEmpID" runat="server" Text="表單加班人" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="lblFormEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
        <td width="20%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="表單申請日期" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblAppDate" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
        <td width="20%" class="style1">
                <asp:Label ID="Label2" runat="server" Text="加班日期" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblAddDate" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
        <td width="20%" class="style1">
                <asp:Label ID="Label3" runat="server" Text="加班時間" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="80%" class="style2">
                <asp:Label ID="lblAddTime" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="目前處理人員" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="style2">
                <asp:Label ID="lblAppEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label7" runat="server" Text="調整處理人員" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="style2">
               <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtAdjustEmpID" MaxLength="6" AutoPostBack="true" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                <asp:Label ID="lblAdjustEmpID" runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgComp1" runat="server" Text="最後異動公司" Font-Names="微軟正黑體"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgComp" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgID1" runat="server" Text="最後異動人員" Font-Names="微軟正黑體"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgID" runat="server" Font-Names="微軟正黑體"> </asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgDate1" runat="server" Text="最後異動日期" Font-Names="微軟正黑體"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgDate" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                        </td>
                                    </tr>
       
    </table>
    </form>
</body>
</html>
