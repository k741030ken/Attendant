<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizReqDetailView.aspx.cs" Inherits="OnBiz_OnBizReqDetailView" %>
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
    
        .style1
        {
            height: 23px;
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
                    <td colspan="4" align="left">
                        <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" CssClass="Util_clsBtn" />                      
                    </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblWriterID_Name" runat="server" Text="登入人員"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="WriterID_Name" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblWriteDate" runat="server" Text="登入日期"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="WriteDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblEmpID_NameN" runat="server" Text="公出人員(員編-姓名)"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="EmpID_NameN" runat="server" Width="40%"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisitFormNo" runat="server" Text="公出號碼單"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="VisitFormNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblCompName" runat="server" Text="公司"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="CompName" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblDeptName" runat="server" Text="單位"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="OrganName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblTitleName" runat="server" Text="職稱"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="TitleName" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblPosition" runat="server" Text="職位"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="Position" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left" class="style1">
                          <asp:Label ID="lblTel_1" runat="server" Text="連絡電話一"></asp:Label>
                        </td>
                        <td width="30%" align="left" class="style1">
                          <asp:Label ID="Tel_1" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left" class="style1">
                          <asp:Label ID="lblTel_2" runat="server" Text="連絡電話二"></asp:Label>
                        </td>
                        <td width="30%" align="left" class="style1">
                          <asp:Label ID="Tel_2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisitBeginDate" runat="server" Text="公出日期"></asp:Label>
                        </td>
                        <td width="30%" align="left" colspan = "3">
                          <asp:Label ID="VisitBeginDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left" class="style1">
                          <asp:Label ID="lblTime" runat="server" Text="公出時間"></asp:Label>
                        </td>
                        <td width="30%" align="left" colspan = "3" class="style1">
                            <asp:Label ID="BeginTimeA" runat="server"></asp:Label>
                            <asp:Label runat="server" text = ":"></asp:Label>
                            <asp:Label ID="BeginTimeB" runat="server"></asp:Label>
                            <asp:Label runat="server" text ="~"></asp:Label>
                            <asp:Label ID="EndTimeA" runat="server"></asp:Label>
                            <asp:Label runat="server" text = ":"></asp:Label>
                            <asp:Label ID="EndTimeB" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblDeputyID_Name" runat="server" Text="職務代理人員"></asp:Label>
                        </td>
                        <td width="30%" align="left"> 
                          <asp:Label ID="DeputyID_NameN" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblLocationType" runat="server" Text="前往地點"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                            <asp:CheckBox ID="Inner" GroupName = "LocationType" runat="server" Text="內部" AutoPostBack="True"  />
                            <asp:CheckBox ID="Outter" GroupName = "LocationType" runat="server" Text="外部" AutoPostBack="True"  />                          
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblInterLocationName" runat="server" Text="內部地點"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="InterLocationName" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblExterLocationName" runat="server" Text="外部地點"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="ExterLocationName" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisiterName" runat="server" Text="連絡人姓名"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="VisiterName" runat="server" Width="40%"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisiterTel" runat="server" Text="連絡人電話"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="VisiterTel" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisitReasonCN" runat="server" Text="洽辦事由"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="VisitReasonCN" runat="server" Width="40%"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblVisitReasonDesc" runat="server" Text="其他說明"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                          <asp:Label ID="VisitReasonDesc" runat="server" Width="40%"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                        </td>
                        <td width="30%" align="left">
                        </td>
                    </tr>
                    <tr id="Tr1" class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblLastChgComp_Name" runat="server" Text="最後異動公司"></asp:Label>
                        </td>
                        <td width="30%" align="left" colspan = "3">
                          <asp:Label ID="LastChgComp_Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr2" class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblLastChgID_NameN" runat="server" Text="最後異動人員"></asp:Label>
                        </td>
                        <td width="80%" align="left" colspan = "3">
                          <asp:Label ID="LastChgID_NameN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="Tr3" class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                          <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動時間"></asp:Label>
                        </td>
                        <td width="80%" align="left" colspan = "3">
                          <asp:Label ID="LastChgDate" runat="server"></asp:Label>
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
