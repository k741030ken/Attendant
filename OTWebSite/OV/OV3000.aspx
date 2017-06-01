<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV3000.aspx.vb" Inherits="OV_OV3000" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
      <style type="text/css">
       input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
       </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td style="height: 30px;">
                        <table width="98%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                            <tr style="height:20px">
                                <td class="td_EditHeader"style="text-align:center" colspan="2">
                                    <asp:HiddenField ID="hidCompID" runat="server" />
                                    <asp:Label ID="Label1"  runat="server" Text="參數設定" align="center" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label19" runat="server" Text="公司" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="lblCompID" runat="server" Text="Label"></asp:Label>
                                 </td>
                            </tr>
                            <tr style="height:20px">
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label28" runat="server" Text="日期設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="Label2" runat="server" Text="加班預先申請日的範圍" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtAdvaceBegin" runat="server" TargetControlID="txtAdvaceBegin" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtAdvaceBegin" runat="server" Width="30px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="3"></asp:TextBox>
                                    <asp:Label ID="Label4" runat="server" Text="日前、" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtAdvanceEnd" runat="server" TargetControlID="txtAdvanceEnd" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtAdvanceEnd" runat="server" Width="30px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="3"></asp:TextBox>
                                    <asp:Label ID="Label5" runat="server" Text="日後" Font-Names="微軟正黑體"></asp:Label><br />
                                    <asp:Label ID="Label3" runat="server" Text="事後加班申報日的範圍" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtDeclarationBegin" runat="server" TargetControlID="txtDeclarationBegin" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtDeclarationBegin" runat="server" Width="30px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="3"></asp:TextBox>
                                    <asp:Label ID="Label6" runat="server" Text="日前" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label31" runat="server" Text="檢核設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="lblContinuDaysCheck" runat="server" Text="連續加班日數檢核" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:RadioButton ID="rbtnOTMustCheckYes" GroupName="OTMustCheck" runat="server" Text="需檢核" Font-Names="微軟正黑體" />
                                    <asp:RadioButton ID="rbtnOTMustCheckNo" GroupName="OTMustCheck" runat="server" Text="不需檢核" Font-Names="微軟正黑體" /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label16" runat="server" Text="日數限制" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtOTLimitDay" runat="server" TargetControlID="txtOTLimitDay" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtOTLimitDay" runat="server" Width="20px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="2"></asp:TextBox>
                                    <asp:Label ID="Label17" runat="server" Text="日" Font-Names="微軟正黑體"></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDealWithOverLimit" runat="server" Text="超過限制的處理" Font-Names="微軟正黑體"></asp:Label>&nbsp;
                                    <asp:RadioButton ID="rbtnOTLimitFlagPrompt" GroupName="OTLimitFlag" runat="server" Text="提示" Font-Names="微軟正黑體" />
                                    <asp:RadioButton ID="rbtnOTLimitFlagApply" GroupName="OTLimitFlag" runat="server" Text="限制" Font-Names="微軟正黑體" />
                                </td>
                            </tr>
                            <tr style="height:20px">
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label30" runat="server" Text="單日時數設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="Label7" runat="server" Text="單日可申請加班的時數上限" Font-Names="微軟正黑體"></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label8" runat="server" Text="平日" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtDayLimitHourN" runat="server" TargetControlID="txtDayLimitHourN" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtDayLimitHourN" runat="server" Width="20px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="2"></asp:TextBox>
                                    <asp:Label ID="Label9" runat="server" Text="小時" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label12" runat="server" Text="假日" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtDayLimitHourH" runat="server" TargetControlID="txtDayLimitHourH" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtDayLimitHourH" runat="server" Width="20px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="2"></asp:TextBox>
                                    <asp:Label ID="Label10" runat="server" Text="小時" Font-Names="微軟正黑體"></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label11" runat="server" Text="超過以上時數的動作" Font-Names="微軟正黑體"></asp:Label>&nbsp;
                                    <asp:RadioButton ID="rbtnDayLimitFlagPrompt" GroupName="DayLimitFlag" runat="server" Text="提示" Font-Names="微軟正黑體" />
                                    <asp:RadioButton ID="rbtnDayLimitFlagApply" GroupName="DayLimitFlag" runat="server" Text="限制申請" Font-Names="微軟正黑體" />
                                </td>
                            </tr>
                            <tr style="height:20px">
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label29" runat="server" Text="單月時數設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="Label13" runat="server" Text="每月加班的時數上限" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:FilteredTextBoxExtender ID="fttxtMonthLimitHour" runat="server" TargetControlID="txtMonthLimitHour" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtMonthLimitHour" runat="server" Width="20px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="2"></asp:TextBox>
                                    <asp:Label ID="Label15" runat="server" Text="小時" Font-Names="微軟正黑體"></asp:Label><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label18" runat="server" Text="超過以上時數的動作"></asp:Label>&nbsp;
                                    <asp:RadioButton ID="rbtnMonthLimitFlagPrompt" GroupName="MonthLimitFlag" runat="server" Text="提示" />
                                    <asp:RadioButton ID="rbtnMonthLimitFlagApply" GroupName="MonthLimitFlag" runat="server" Text="限制申請" />
                                </td>
                            </tr>
                            
                            <tr>
                                <td class="td_Edit">
                                    <asp:Label ID="Label25" runat="server" Text="加班超過兩小時用餐時數預設值"></asp:Label>
                                </td>
                                <td class="td_Edit">
                                    <asp:Label ID="Label23" runat="server" Text="平日"></asp:Label>
                                    <asp:TextBox ID="txtMealTimeN" runat="server" Width="30px"></asp:TextBox>
                                    <asp:Label ID="Label24" runat="server" Text="分鐘"></asp:Label><br />
                                    <asp:Label ID="Label26" runat="server" Text="假日"></asp:Label>
                                    <asp:TextBox ID="txtMealTimeH" runat="server" Width="30px"></asp:TextBox>
                                    <asp:Label ID="Label27" runat="server" Text="分鐘"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label32" runat="server" Text="申請加班職等設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="Label20" runat="server" Text="加班人職等"></asp:Label>
                                    <%--<asp:TextBox ID="txtEmpRankID" runat="server" Width="30px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEmpRankID" runat="server"></asp:DropDownList>
                                    <asp:Label ID="Label14" runat="server" Text="以上(含)，僅送簽一關"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label33" runat="server" Text="簽核主管職等設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="Label21" runat="server" Text="簽核主管職等小於"></asp:Label>
                                    <%--<asp:TextBox ID="txtValidRankID" runat="server" Width="30px"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlValidRankID" runat="server"></asp:DropDownList>
                                    <asp:Label ID="Label22" runat="server" Text="，需再往上送簽"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                            <td class="td_Edit" width="25%">
                                    <asp:Label ID="Label34" runat="server" Text="轉補休職等設定" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                <asp:Label ID="Label35" runat="server" Text="職等" Font-Names="微軟正黑體"></asp:Label>
                                    <asp:DropDownList ID="ddlAdjustRankID" runat="server"></asp:DropDownList>
                                    <asp:Label ID="Label36" runat="server" Text="以上(含)，加班僅能選擇轉補休" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblDefaultSalaryOrAdjust" runat="server" Text="加班轉換方式預設值" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbtnTurnAdjust" GroupName="SalaryOrAdjust" runat="server" Text="轉補休" Font-Names="微軟正黑體" />
                                    <asp:RadioButton ID="rbtnTurnSalary" GroupName="SalaryOrAdjust" runat="server" Text="轉薪資" Font-Names="微軟正黑體" />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblAdjustInvalidDate" runat="server" Text="補休失效日" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                <uc:ucCalender ID="txtAdjustInvalidDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            
                            
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgComp"  runat="server" Text="最後異動公司" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="txtLastChgComp"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgID"  runat="server" Text="最後異動人員" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="txtLastChgID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgDate"  runat="server" Text="最後異動時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="txtLastChgDate"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
