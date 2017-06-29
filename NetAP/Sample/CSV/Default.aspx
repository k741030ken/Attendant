<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Sample_CSV_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagPrefix="uc1" TagName="ucGridView" %>
<%@ Register Src="~/Util/ucUploadButton.ascx" TagPrefix="uc1" TagName="ucUploadButton" %>



<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CSV Sample</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <cc1:HoverTooltip ID="HoverTooltip1" runat="server"></cc1:HoverTooltip>
        <div>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">CSV 檔案上傳</legend>
                <uc1:ucUploadButton runat="server" ID="btnFreeCsv1" ucBtnWidth="180" ucBtnCaption="任意 CSV (首列不含欄名)" ucUploadFileExtList="csv" />
                <uc1:ucUploadButton runat="server" ID="btnFreeCsv2" ucBtnWidth="180" ucBtnCaption="任意 CSV (首列包含欄名)" ucUploadFileExtList="csv" />
                <uc1:ucUploadButton runat="server" ID="btnStockInfo1" ucBtnWidth="180" ucBtnCaption="轉換StockInfo.csv(模式一)" ucUploadFileExtList="csv" ucBtnToolTip="指定CSV欄位型別，一發現資料錯誤就中斷" />
                <uc1:ucUploadButton runat="server" ID="btnStockInfo2" ucBtnWidth="180" ucBtnCaption="轉換StockInfo.csv(模式二)" ucUploadFileExtList="csv" ucBtnToolTip="指定CSV欄位型別，發現資料錯誤仍繼續轉換" />
            </fieldset>
            <fieldset class="Util_Fieldset" style="min-height: 150px;">
                <legend class="Util_Legend">CSV 解析結果</legend>
                <asp:Label ID="labMsg" runat="server"></asp:Label>
                <div id="divResult" runat="server" visible="false">
                    <fieldset class="Util_Fieldset">
                        <legend class="Util_Legend" style="background-color:green;">轉換成功</legend>
                        <uc1:ucGridView runat="server" ID="gvResult" ucDisplayOnly="true" />
                    </fieldset>
                </div>
                <div id="divErrInfo" runat="server" visible="false">
                    <fieldset class="Util_Fieldset">
                        <legend class="Util_Legend" style="background-color:red;">錯誤原因</legend>
                        <uc1:ucGridView runat="server" ID="gvErrInfo" ucDisplayOnly="true" />
                    </fieldset>
                </div>
            </fieldset>
        </div>
    </form>
</body>
</html>
