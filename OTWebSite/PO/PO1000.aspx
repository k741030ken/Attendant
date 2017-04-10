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
                                    <asp:Label ID="lblSpecialUnit" runat="server" Text="特殊打卡單位" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnNotSpecialUnit" GroupName="rbnSpecialUnit" runat="server" Text="否" />&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rbnIsSpecialUnit" GroupName="rbnSpecialUnit" runat="server" Text="是" />              
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblDutyInBT" runat="server" Text="出勤異常時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:TextBox ID="txtDutyInBT" runat="server" Width="50px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblDutyInBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblDutyOutBT" runat="server" Text="退勤異常時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:TextBox ID="txtDutyOutBT" runat="server" Width="50px" AutoComplete="off" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblDutyOutBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchInBT" runat="server" Text="出勤打卡提醒時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:TextBox ID="txtPunchInBT" runat="server" Width="50px" AutoComplete="off" AutoPostBack="true" Font-Names="微軟正黑體" MaxLength="4"></asp:TextBox>
                                    <asp:Label ID="lblPunchInBTMintxt" runat="server" Text="分鐘" Font-Names="微軟正黑體"></asp:Label><br />
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchInMsg" runat="server" Text="出勤打卡提醒內容" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
                                    <asp:RadioButton ID="rbnDefaultPunchInMsg" GroupName="rbnPunchInMsg" runat="server" AutoPostBack="true" Text="預設:" />&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblDefaultPunchInMsg" runat="server" Font-Names="微軟正黑體" AutoPostBack="true" ></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rbnCustomPunchInMsg" GroupName="rbnPunchInMsg" runat="server" AutoPostBack="true" Text="自訂:" />
                                    <br />
                                    <asp:TextBox id="txtCustomPunchInMsg" rows="2" runat="server" class="InputTextStyle_Thin" MaxLength="200" TextMode="MultiLine" Width="100%" Height="60px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height:20px">
                                <td class="td_Edit" width="25%">
                                    <asp:Label ID="lblPunchOutBT" runat="server" Text="退勤打卡提醒時間" Font-Names="微軟正黑體"></asp:Label>
                                </td>
                                <td class="td_Edit" width="75%">
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
