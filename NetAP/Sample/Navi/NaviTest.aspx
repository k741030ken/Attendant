<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NaviTest.aspx.cs" Inherits="Sample_NaviTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucTreeView.ascx" TagPrefix="uc1" TagName="ucTreeView" %>
<%@ Register Src="~/Util/ucMenuBar.ascx" TagPrefix="uc1" TagName="ucMenuBar" %>
<%@ Register Src="~/Util/ucActionBar.ascx" TagPrefix="uc1" TagName="ucActionBar" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>NaviTest</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="Util_Container">
            <uc1:ucMenuBar runat="server" ID="ucMenuBar" />
            <div style="padding-top: 20px;">
                <div style="border: 1px solid silver; width: 180px; height: 550px; padding: 10px; overflow: auto; float: left;">
                    <uc1:ucTreeView runat="server" ID="ucTreeView1" />
                </div>
                <div style="margin-left: 220px;">
                    <uc1:ucActionBar runat="server" ID="ucActionBar" />
                    <h4>導引類元件範例，說明如下：</h4>
                    <ul>
                        <li>頂部：[ucMenuBar]，資料來源可為[NodeInfo]資料表或是自訂 DtatTable 物件</li>
                        <li>左方：[ucTreeView]，同上</li>
                        <li>右方：[ucActionBar]，可自訂要顯示的按鈕及套用的CSS，並提供[按鈕點擊]事件供開發人員使用</li>
                    </ul>
                    <uc1:ucGridView runat="server" ID="ucGridView" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
