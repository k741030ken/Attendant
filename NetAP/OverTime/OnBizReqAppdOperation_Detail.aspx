<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizReqAppdOperation_Detail.aspx.cs" Inherits="OverTime_OnBizReqAppdOperation_Detail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Util/ucAttachDownloadButton.ascx" tagname="ucAttachDownloadButton" tagprefix="uc1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
<title>明細頁</title>   
</head>
 <style type="text/css">
        body
        {
        	font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        	color :#000000;
        }
 </style>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table style="width:100%;border:1px solid" rules="all">
            <tr style="height:20px">
                    <td colspan="4" align="left">
                        <asp:Button ID="btnGoBack" runat="server" Text="返回" OnClientClick="return confirm('確定要返回？')" onclick="btnGoBack_Click" CssClass="Util_clsBtn" />
                    </td>
                    </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblWriterID_Name" runat="server" Text="登錄人員" ForeColor="blue" ></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWriterID_Nametxt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWriteDate" runat="server" Text="登錄日期" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWriteDatetxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblEmpID_NameN" runat="server" Text="公出人員" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmpID_NameNtxt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVisitFormNo" runat="server" Text="公出單號碼" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVisitFormNotxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblCompName" runat="server" Text="公司" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCompNametxt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDeptName" runat="server" Text="單位" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDeptNametxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblTitleName" runat="server" Text="職稱" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTitleNametxt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPosition" runat="server" Text="職位" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPositiontxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblTel_1" runat="server" Text="聯絡電話一" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTel_1txt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTel_2" runat="server" Text="聯絡電話二" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTel_2txt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblVisitDate" runat="server" Text="公出日期" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblVisitDatetxt" runat="server"></asp:Label>
                </td>
            </tr><tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblVisitTime" runat="server" Text="公出時間" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblVisitTimetxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblDeputyID_Name" runat="server" Text="職務代理人員" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblDeputyID_Nametxt" runat="server"></asp:Label> 
                </td>
            </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblLocationType" runat="server" Text="前往地點" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                     <asp:CheckBox ID="chkInterLocation" Enabled="false" runat="server" Text="內部"/>
                     <asp:CheckBox ID="chkExterLocation" Enabled="false" runat="server" Text="外部"/>
                </td>
                <td>
                     <asp:Label ID="lblInterLocationName" runat="server" Text="內部地點" ForeColor="Blue"></asp:Label>
                </td>
                <td>
                     <asp:Label ID="lblInterLocationNametxt" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblExterLocationName" runat="server" Text="外部地點" ForeColor="Blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblExterLocationNametxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblVisiterName" runat="server" Text="聯絡人姓名" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVisiterNametxt" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVisiterTel" runat="server" Text="聯絡人電話" ForeColor="blue"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblVisiterTeltxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblVisitReason" runat="server" Text="洽辦事由" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblVisitReasontxt" runat="server"></asp:Label>
                </td>
            </tr>
             <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblVisitReasonDesc" runat="server" Text="其他說明" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblVisitReasonDesctxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblLastChgComp" runat="server" Text="最後異動公司" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblLastChgComptxt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow1">
                <td>
                    <asp:Label ID="lblLastChgID" runat="server" Text="最後異動人員" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblLastChgIDtxt" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="Util_clsRow2">
                <td>
                    <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動時間" ForeColor="blue"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:Label ID="lblLastChgDatetxt" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


