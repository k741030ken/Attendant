<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PO1000.aspx.vb" Inherits="PO_PO1000" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblCompID" runat="server" Text="公司" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="lblCompIDtxt" runat="server" Width="200px"></asp:Label>
                                 </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblDutyInBT" runat="server" Text="*早到早退異常時間" Font-Names="微軟正黑體" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultDutyInBT" GroupName="DutyInBT" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnDutyInBT" GroupName="DutyInBT" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:TextBox ID="txtDutyInBT" runat="server" Width="50px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblDutyInBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblDutyOutBT" runat="server" Text="*晚到晚退異常時間" Font-Names="微軟正黑體" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultDutyOutBT" GroupName="DutyOutBT" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnDutyOutBT" GroupName="DutyOutBT" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:TextBox ID="txtDutyOutBT" runat="server" Width="50px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblDutyOutBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchInBT" runat="server" Text="出勤打卡緩衝時間" Font-Names="微軟正黑體" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultPunchInBT" GroupName="PunchInBT" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnPunchInBT" GroupName="PunchInBT" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:TextBox ID="txtPunchInBT" runat="server" Width="50px" AutoComplete="off" AutoPostBack="true" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblPunchInBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblVacTime" runat="server" Text="*請假判斷時間" Font-Names="微軟正黑體" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="lblVacDayBeginTime" runat="server" Text="全天開始" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlVacDayBeginTimeHH" runat="server" ></asp:DropDownList>
                                    <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                                    <asp:DropDownList ID="ddlVacDayBeginTimeMM" runat="server" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblVacAMEndTime" runat="server" Text="上午結束" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlVacAMEndTimeHH" runat="server" ></asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" Text=" : "></asp:Label>
                                    <asp:DropDownList ID="ddlVacAMEndTimeMM" runat="server" ></asp:DropDownList><br />
                                    <asp:Label ID="lblVacPMBeginTime" runat="server" Text="下午開始" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlVacPMBeginTimeHH" runat="server" ></asp:DropDownList>
                                    <asp:Label ID="Label3" runat="server" Text=" : "></asp:Label>
                                    <asp:DropDownList ID="ddlVacPMBeginTimeMM" runat="server" ></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="Label2" runat="server" Text="全天結束" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlVacDayEndTimeHH" runat="server" ></asp:DropDownList>
                                    <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label>
                                    <asp:DropDownList ID="ddlVacDayEndTimeMM" runat="server" ></asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchInMsg" runat="server" Text="出勤打卡提醒內容" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultPunchInMsg" GroupName="PunchInMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDefaultPunchInMsg" runat="server" Font-Names="微軟正黑體" AutoPostBack="true" ></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnCustomPunchInMsg" GroupName="PunchInMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtCustomPunchInMsg" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchOutBT" runat="server" Text="退勤打卡緩衝時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultPunchOutBT" GroupName="rbnPunchOutBT" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnPunchOutBT" GroupName="rbnPunchOutBT" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:TextBox ID="txtPunchOutBT" runat="server" Width="50px" AutoComplete="off" AutoPostBack="true" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblPunchOutBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchOutMsg" runat="server" Text="退勤打卡提醒內容" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultPunchOutMsg" GroupName="rbnPunchOutMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDefaultPunchOutMsg" runat="server" Font-Names="微軟正黑體" AutoPostBack="true" ></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnCustomPunchOutMsg" GroupName="rbnPunchOutMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtCustomPunchOutMsg" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblAffairMsg" runat="server" Text="打卡異常(處理公務) 提醒內容" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnAffairDefault" GroupName="rbnAffairMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblAffairDefault" runat="server" Text="提醒您，公務加班須經主管核准並填寫加班單。若需修改請於次日進入「出退勤補登」功能更改。" Font-Names="微軟正黑體"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnAffairSelf" GroupName="rbnAffairMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtAffairSelf" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblOVTenMsg" runat="server" Text="關懷內容(超過夜間10點)" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnOVTenDefault" GroupName="rbnOVTenMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblOVTenDefault" runat="server" Text="夜深了！現在時間已超過10點，感謝您的辛勤與付出，返家路程小心行走注意您的安全，盡量搭乘安全交通工具返家。" Font-Names="微軟正黑體"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnOVTenSelf" GroupName="rbnOVTenMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtOVTenSelf" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblFemaleMsg" runat="server" Text="關懷女性員工內容" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnFemaleDefault" GroupName="rbnFemaleMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblFemaleDefault" runat="server" Text="，敬請確認是否已事先通知工會及人資。" Font-Names="微軟正黑體"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnFemaleSelf" GroupName="rbnFemaleMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtFemaleSelf" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblExcludePara" runat="server" Text="*打卡異常排除條件" Font-Names="微軟正黑體" ForeColor="Blue"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:Label ID="lblExcludeRank" runat="server" Text="金控職等：" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnNoExcludeRankID" GroupName="rbnExcludeRank" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnExcludeRankID" GroupName="rbnExcludeRank" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlHoldingRankID" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:Label ID="lblRanktxt" runat="server" Text="職等(含)以上" Font-Names="微軟正黑體"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblPosition" runat="server" Text="職位：" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnNoExcludePositionID" GroupName="rbnExcludePosition" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnExcludePositionID" GroupName="rbnExcludePosition" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlPositionID" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:Label ID="lblSelectPositionID" runat="server" Style="display: none"></asp:Label>
                                    <asp:Label ID="lblPositionID" runat="server" Style="display: none"></asp:Label>
                                    <uc:ButtonPosition ID="ucSelectPosition" runat="server"  ButtonHint="選取"
                                        WindowHeight="550" WindowWidth="1000" />
                                    <br />
                                    <asp:Label ID="lblWorkType" runat="server" Text="工作性質：" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnNoExcludeWorkTypeID" GroupName="rbnExcludeWorkType" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnExcludeWorkTypeID" GroupName="rbnExcludeWorkType" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlWorkTypeID" runat="server"  AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblSelectWorkTypeID" runat="server" Style="display: none"></asp:Label>
                                    <asp:Label ID="lblWorkTypeID" runat="server" Style="display: none"></asp:Label>
                                    <uc:ButtonWorkType ID="ucSelectWorkType" runat="server"  ButtonHint="選取"
                                WindowHeight="550" WindowWidth="1000" />
                                    <br />
                                    <asp:Label ID="lblRotate" runat="server" Text="24小時輪班人員：" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnNoRotate" GroupName="rbnRotateFlag" runat="server" AutoPostBack="true" Text="不控管" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnRotate" GroupName="rbnRotateFlag" runat="server" AutoPostBack="true" Text="控管" />&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgComp"  runat="server" Text="最後異動公司" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="lblLastChgComptxt"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgID"  runat="server" Text="最後異動人員" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="lblLastChgIDtxt"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblLastChgDate"  runat="server" Text="最後異動時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" align="left" width="75%">
                                    <asp:Label ID="lblLastChgDatetxt"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
