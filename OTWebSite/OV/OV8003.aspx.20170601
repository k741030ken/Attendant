<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV8003.aspx.vb" Inherits="OV_OV8003" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>流程引擎設定(明細)</title>
    <style type="text/css">
        .GridViewStyle
        {
            font-size: 15px;
            background-color: white;
            border-color: #3366CC;
            border-style: none;
            border-width: 1px;
        }
        .td_header
        {
            text-align: center;
            background-color: #89b3f5;
            color: dimgray;
            font-size: 14px;
            border-width: 1px;
            border-style: solid;
            border-color: #5384e6;
            font-weight: normal;
        }
        .tr_evenline
        {
            background-color: White;
        }
        .td_detail
        {
            border-width: 1px;
            border-style: solid;
            border-color: #89b3f5;
            font-size: 14px;
            font-family: Calibri, 新細明體;
            height: 16px;
        }
        .tr_oddline
        {
            background-color: #e2e9fe;
        }
        .style1
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width:110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width:110px;
        }
        .buttonface
        {
            border: 1px solid gray;
            background-color:Silver;
            text-justify: distribute-all-lines;
            font-size: 14px;
            background-image: url('../images/bgSilverX.gif');
            cursor: hand;
            color: black; 
            padding-top: 3px;
            background-repeat: repeat-x;
            font-family: Calibri, 新細明體, 'Courier New' , 細明體; 
            letter-spacing: 1px;
            border-collapse: collapse;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="td_detail">
        <tr>
            <td width="10%" class="style1" >
                <asp:Label ID="Label13" runat="server" Text="公司代碼：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label14" runat="server" Text="系統代碼：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblOTSystemID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1" >
                <asp:Label ID="Label1" runat="server" Text="流程代碼：" Font-Names="微軟正黑體"  ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblFlowCode" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label2" runat="server" Text="流程名稱：" Font-Names="微軟正黑體"  ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblFlowName" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label3" runat="server" Text="流程識別碼：" Font-Names="微軟正黑體"  ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblFlowSN" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="關卡序號：" Font-Names="微軟正黑體"  ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblFlowSeq" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label5" runat="server" Text="流程起點註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
            <asp:Label ID="lblFlowStartFlag" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label6" runat="server" Text="流程終點註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
            <asp:Label ID="lblFlowEndFlag" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label7" runat="server" Text="無效註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
            <asp:Label ID="lblInValidFlag" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label8" runat="server" Text="流程動作：" Font-Names="微軟正黑體"  ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblFlowAct" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label16" runat="server" Text="隱藏流程註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
            <asp:Label ID="lblVisableFlag" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label9" runat="server" Text="簽核線定義：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblSignLineDefine" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label10" runat="server" Text="簽核者定義：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblSingIDDefine" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr id="trSpec" runat="server" visible="false">
            <td width="10%"  class="style1" >
                <asp:Label ID="Label11" runat="server" Text="特定人員：" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:Label ID="lblSpeCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="Label12" runat="server" Text="-" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="lblSpeCompName" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="Label17" runat="server" Text="-" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="txtSpeEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="Label15" runat="server" Text="-" Font-Names="微軟正黑體"></asp:Label>
                <asp:Label ID="lblSpeEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%"  class="style1" ></td>
            <td width="40%" class="style2"></td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label91" runat="server" Text="最後異動人員公司" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblLastChgComp" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label101" runat="server" Text="最後異動人員" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblLastChgID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
        <tr>
        <td width="10%" class="style1">
                <asp:Label ID="Label18" runat="server" Text="最後異動時間" Font-Names="微軟正黑體" ></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:Label ID="lblLastChgDate" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>