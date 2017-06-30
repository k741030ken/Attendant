<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizReqModifyView.aspx.cs" Inherits="OnBiz_OnBizReqModifyView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGetEmpID.ascx" TagName="ucGetEmpID" TagPrefix="uc" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>公出修改</title>
    <script type="text/javascript" src="../Util/WebClient/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Util/WebClient/jquery-ui-1.8.24.custom.js"></script>
    <link rel="stylesheet" type="text/css" href="../Util/WebClient/jquery-ui-1.8.24.custom.css" />
       <style type="text/css">
        body
        {
            font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
            color :#000000;
        }
           
        .ui-autocomplete
        {
            text-align: left;
            font-size: 12px;
            max-height: 200px;
            overflow-x: hidden;
            overflow-y: auto;
        	font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        }
       .style1
       {
           width: 264px;
       }
        </style>
        <script type="text/javascript">
    function initDDL()
    {
        $(function () {
                var QueryData = [];
                $("#ddlInterLocationName").find("option").each(function () {
                    QueryData.push({ label: $(this).text(), value: $(this).text() });
                });
                $("#txtInterLocationName").autocomplete({
                    source: QueryData,
                    minLength: 0
                });
                $("#txtInterLocationName").focus(function () {
                    $(this).autocomplete("search");
                });
            });
    }
            
        
        $(function () {
            var TimeHH = [
                "00", "01", "02", "03", "04", "05", "06", "07", "08", "09",
                "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
                "20", "21", "22", "23",
            ];
            $("#ddlBeginTimeA").autocomplete({
                source: TimeHH,
                minLength: 0,
            });
            $("#ddlBeginTimeA").focus(function () {
                $(this).autocomplete("search");
            });
            $("#ddlEndTimeA").autocomplete({
                source: TimeHH,
                minLength: 0,
            });
            $("#ddlEndTimeA").focus(function () {
                $(this).autocomplete("search");
            });
        });
        $(function () {
            var TimeMM = [
                "00", "10", "20", "30", "40", "50", "59",
            ];
            $("#ddlBeginTimeB").autocomplete({
                source: TimeMM,
                minLength: 0
            });
            $("#ddlBeginTimeB").focus(function () {
                $(this).autocomplete("search");
            });
            $("#ddlEndTimeB").autocomplete({
                source: TimeMM,
                minLength: 0
            });
            $("#ddlEndTimeB").focus(function () {
                $(this).autocomplete("search");
            });
        });     
    </script>
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
                        <asp:Button ID="btnSave" runat="server" Text="暫存" onclick="btnSave_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space1" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnSend" runat="server" Text="送簽" onclick="btnSend_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space2" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space3" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnActionX" runat="server" Text="清除" onclick="btnActionX_Click" CssClass="Util_clsBtn" />                        
                    </td>
                    </tr>
                    <tr id="Tr4" class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblWriterID_Name" runat="server" Text="填單人員"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="WriterID_Name" runat="server"></asp:Label>
                        </td>
                        <td align="left" width = "20%">
                          <asp:Label ID="lblWriteDate" runat="server" Text="填單日期"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="WriteDate" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblEmpID_NameN" runat="server" Text="*公出人員"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                         <asp:Textbox ID="txtEmpID" runat="server" MaxLength= "6" AutoPostBack = "true" ontextchanged="txtEmpID_TextChanged"></asp:Textbox>
                         <uc:ucGetEmpID ID="btnEmpID" runat="server" />
                         <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblCompName" runat="server" Text="公司"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="CompName" runat="server" ></asp:Label>
                        </td>
                        <td align="left" width = "20%">
                          <asp:Label ID="lblOrganName" runat="server" Text="單位(處/部/科)"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="OrganName" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblTitleName" runat="server" Text="職稱"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="TitleName" runat="server"></asp:Label>
                        </td>
                        <td align="left" width = "20%">
                          <asp:Label ID="lblPosition" runat="server" Text="職位"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Label ID="Position" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblTel_1" runat="server" Text="*連絡電話一"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Textbox ID="txtTel_1" runat="server"></asp:Textbox>
                        </td>
                        <td align="left" width = "20%">
                          <asp:Label ID="lblTel_2" runat="server" Text="連絡電話二"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Textbox ID="txtTel_2" runat="server"></asp:Textbox>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblVisitBeginDate" runat="server" Text="*公出日期"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                          <asp:ucDate ID="txtVisitBeginDate" runat="server" />
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblTime" runat="server" Text="*公出時間"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                        <asp:FilteredTextBoxExtender ID="ftddlBeginTimeA" runat="server" TargetControlID="ddlBeginTimeA" FilterType="Numbers" ></asp:FilteredTextBoxExtender>
                            <asp:Textbox ID="ddlBeginTimeA" runat="server" Width = "5%" MaxLength="2" placeholder = "-請選擇-" Style="text-align: center" ></asp:Textbox>
                          <asp:Label ID="Label1" runat="server" text = "："></asp:Label>
                        <asp:FilteredTextBoxExtender ID="ftddlBeginTimeB" runat="server" TargetControlID="ddlBeginTimeB" FilterType="Numbers" ></asp:FilteredTextBoxExtender>
                            <asp:Textbox ID="ddlBeginTimeB" runat="server" Width = "5%" MaxLength="2" placeholder = "-請選擇-" Style="text-align: center"></asp:Textbox>
                            <asp:Label ID="Label2" runat="server" text ="～"></asp:Label>
                        <asp:FilteredTextBoxExtender ID="ftddlEndTimeA" runat="server" TargetControlID="ddlEndTimeA" FilterType="Numbers" ></asp:FilteredTextBoxExtender>
                            <asp:Textbox ID="ddlEndTimeA" runat="server" Width = "5%" MaxLength="2" placeholder = "-請選擇-" Style="text-align: center"></asp:Textbox>
                          <asp:Label ID="Label3" runat="server" text = "："></asp:Label>
                        <asp:FilteredTextBoxExtender ID="ftddlEndTimeB" runat="server" TargetControlID="ddlEndTimeB" FilterType="Numbers" ></asp:FilteredTextBoxExtender>
                            <asp:Textbox ID="ddlEndTimeB" runat="server" Width = "5%" MaxLength="2" placeholder = "-請選擇-" Style="text-align: center"></asp:Textbox>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblDeputyID_Name" runat="server" Text="*職務代理人員"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Textbox ID="txtDeputyID" runat="server" MaxLength= "6" AutoPostBack = "true" ontextchanged="txtDeputyID_TextChanged"></asp:Textbox>
                         <uc:ucGetEmpID ID="btnDeputyID" runat="server" />
                         <asp:Label ID="lblDeputyID" runat="server"></asp:Label>
                        </td>
                        <td align="left" width = "20%">
                          <%--<asp:Label ID="lblValid" runat="server" Text="*簽核主管"></asp:Label>--%>
                        </td>
                        <td align="left" width = "30%">
<%--                          <asp:DropDownList ID="ddlValid" runat="server"  width="25%">
                          </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True" UpdateMode="Conditional">
                            <ContentTemplate>
                                <script type="text/javascript">
                                    Sys.Application.add_load(initDDL);
                                </script>
                            <table width="100%" cellpadding="0" cellspacing="1" >
                                <tr id="Tr11" class="Util_clsRow1" runat="server">
                                    <td align="left" width = "20%" rowspan="2">
                                        <asp:Label ID="Label4" runat="server" Text="*前往地點單位"></asp:Label>
                                    </td>
                                    <td align="left" width = "15%">
                                        <asp:RadioButton ID="Inner" GroupName = "LocationType" runat="server" Text="內部" AutoPostBack="true" OnCheckedChanged="Inner_CheckedChanged" />
                                    </td>
                                    <td align="left" width = "15%">
                                      <asp:Label ID="Label5" runat="server" Text="地點"></asp:Label>
                                    </td>
                                    <td align="left" width = "50%" >
                                        <asp:Textbox ID="txtInterLocationName" runat="server" Style="text-align: center"></asp:Textbox>
                                        <asp:DropDownList ID="ddlInterLocationName" runat="server" style="display:none;"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="Util_clsRow2">
<%--                                    <td align="left">
                                        <asp:Label ID="Label7" runat="server" Text="*前往地點單位"></asp:Label>
                                    </td>--%>
                                    <td align="left" width = "15%">
                                    <asp:RadioButton ID="Outter" GroupName = "LocationType" runat="server" Text="外部" AutoPostBack="true" OnCheckedChanged="Outter_CheckedChanged" />
                                    </td>
                                    <td align="left" width = "15%">
                                      <asp:Label ID="Label6" runat="server" Text="機構/地點"></asp:Label>
                                    </td>
                                    <td align="left" width = "50%">
                                      <asp:Textbox ID="txtExterLocationName" runat="server" Width = "95%" ></asp:Textbox>
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblVisiterName" runat="server" Text="連絡人姓名"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Textbox ID="txtVisiterName" runat="server" ></asp:Textbox>
                        </td>
                        <td align="left" width = "20%">
                          <asp:Label ID="lblVisiterTel" runat="server" Text="連絡人電話"></asp:Label>
                        </td>
                        <td align="left" width = "30%">
                          <asp:Textbox ID="txtVisiterTel" runat="server" ></asp:Textbox>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblVisitReasonCN" runat="server" Text="洽辦事由"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                          <asp:DropDownList ID="ddlVisitReasonCN" runat="server"  width="25%">
                          </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblVisitReasonDesc" runat="server" Text="其他說明"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                          <asp:Textbox ID="txtVisitReasonDesc" runat="server" width="95%"></asp:Textbox>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblLastChgComp_Name" runat="server" Text="最後異動公司"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                          <asp:Label ID="LastChgComp_Name" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblLastChgID_NameN" runat="server" Text="最後異動人員"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
                          <asp:Label ID="LastChgID_NameN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" width = "20%">
                          <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動時間"></asp:Label>
                        </td>
                        <td align="left" width = "80%" colspan = "3">
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
