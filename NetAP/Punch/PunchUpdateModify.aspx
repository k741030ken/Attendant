<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchUpdateModify.aspx.cs"
    Inherits="Punch_PunchUpdateModify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="../Util/ucTimePickerNew.ascx" TagName="ucTimePickerNew" TagPrefix="asp" %>
<%@ Register Src="../Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<%@ Register Src="../Util/ucAttachDownloadButton.ascx" TagName="ucAttachDownloadButton"
    TagPrefix="uc1" %>
<%@ Register Src="../Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>
<!DOCTYPE>
<html>
<head id="Head1" runat="server">
    <title>打卡補登修改</title>
    <style type="text/css">
        body
        {
            font-family: "微軟正黑體" , "微软雅黑" , "Microsoft JhengHei" , Arial, sans-serif;
            color: #000000;
        }
    </style>
</head>
<script type="text/javascript">
   
    function funHolidayOrNot(msg) {
        alert(msg);
    }
    function funYesOrNoTempSave(msg) {
        if (confirm(msg)) {
            document.getElementById('btnTempSaveInvisible').click();
        }
    }
    function funYesOrNoTempSave_Reminder(msg) {
        alert(msg);
        document.getElementById('btnTempSaveInvisible_Reminder').click();
    }

    function funYesOrNoSubmit(msg) {
        if (confirm(msg)) {
            //                document.getElementById('btnSubmit').setAttribute("disabled", "disabled");
            //                document.getElementById('btnSubmitInvisible').click();
            document.getElementById('btnSubmitInvisible').click();
        }
    }
    function funYesOrNoSubmit_Reminder(msg) {
        //            if (confirm(msg)) {
        //                document.getElementById('btnSubmit').setAttribute("disabled", "disabled");
        //                document.getElementById('btnSubmitInvisible').click();
        //            }
        alert(msg);
        document.getElementById('btnSubmitInvisible_Reminder').click();

    }
    
</script>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <uc1:ucLightBox runat="server" ID="ucLightBox" />
    <fieldset class="Util_Fieldset">
        <legend class="Util_Legend">打卡補登修改</legend>
        <table width="100%" cellpadding="1" cellspacing="1">
            <tr style="height: 20px">
                <td colspan="2" align="left">
                    <asp:Button ID="btnSubmit" runat="server" Text="送簽" OnClick="btnSubmit_Click" CssClass="Util_clsBtn"
                        OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />&nbsp;&nbsp;
                    <asp:Button ID="btnClear" runat="server" Text="清除" OnClientClick="return confirm('確定要清除？');javascript:funcClear();"
                        OnClick="btnClear_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                    <asp:Button ID="btnExit" runat="server" Text="返回" OnClientClick="return confirm('確定要返回？')"
                        OnClick="btnExit_Click" CssClass="Util_clsBtn" />
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblEmp" runat="server" Text="員編姓名："></asp:Label>
                </td>
                <td style="width: 35%" align="left">
                    <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblDate" runat="server" Text="打卡日期："></asp:Label>
                </td>
                <td style="width: 35%" align="left">
                    <asp:Label ID="lblPunchDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblTime" runat="server" Text="打卡時間："></asp:Label>
                </td>
                <td style="width: 35%" align="left">
                    <asp:Label ID="lblPunchTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblConfirmPunchFlagT" runat="server" Text="打卡類型："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <asp:Label ID="lblConfirmPunchFlag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblAbnormalReason" runat="server" Text="原因："></asp:Label>
                </td>
                <td width="45%" align="left" colspan="3">
                    <asp:RadioButton ID="rdoAbnormalFlag1" Text="處理公務" runat="server" GroupName="grpAbnormalFlag"
                        Enabled="False" />
                    <asp:RadioButton ID="rdoAbnormalFlag2" Text="非處理公務" runat="server" GroupName="grpAbnormalFlag"
                        Enabled="False" />
                    <asp:Label ID="lbl_" runat="server" Text="-" Visible="false"></asp:Label>
                    <asp:Label ID="lblAbnormalReasonCN" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblAbnormal" runat="server" Text="其他說明："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <asp:Label ID="lblAbnormalDesc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedyReason" runat="server" Text="補登理由："></asp:Label>
                </td>
                <td style="width: 35%" align="left">
                    <%--<asp:Label ID="lblRemedyReasonID" runat="server"></asp:Label>--%>
                    <asp:DropDownList ID="ddlRemedyReasonID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedyPunchTime_not" runat="server" Text="補登打卡時間："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <%--<asp:Label ID="lblRemedyPunchTime" runat="server"></asp:Label>--%>
                    <asp:ucTimePickerNew ID="ucRemedyPunchTime" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" Width="80px" AutoPostBack="true" />
                    <%--<br />
                        <asp:DropDownList ID="ddlPunchTimeHH" runat="server">
                        <asp:ListItem Selected="True">---請選擇---</asp:ListItem>
                        <asp:ListItem Value="00">00</asp:ListItem>
                        <asp:ListItem Value="01">01</asp:ListItem>
                        <asp:ListItem Value="02">02</asp:ListItem>
                        <asp:ListItem Value="03">03</asp:ListItem>
                        <asp:ListItem Value="04">04</asp:ListItem>
                        <asp:ListItem Value="05">05</asp:ListItem>
                        <asp:ListItem Value="06">06</asp:ListItem>
                        <asp:ListItem Value="07">07</asp:ListItem>
                        <asp:ListItem Value="08">08</asp:ListItem>
                        <asp:ListItem Value="09">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlPunchTimeMM" runat="server">
                        <asp:ListItem Selected="True" Value="">---請選擇---</asp:ListItem>
                        <asp:ListItem Value="00">00</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="40">40</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="59">59</asp:ListItem>
                    </asp:DropDownList>--%>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedy_AbnormalReason" runat="server" Text="補登原因："></asp:Label>
                </td>
                <td width="45%" align="left" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:RadioButton ID="rdoRemedy_AbnormalFlag1" Text="處理公務" runat="server" GroupName="grpRemedy_AbnormalFlag"
                                AutoPostBack="true" OnCheckedChanged="rdoRemedy_AbnormalFlag1_CheckedChanged" />
                            <asp:RadioButton ID="rdoRemedy_AbnormalFlag2" Text="非處理公務" runat="server" GroupName="grpRemedy_AbnormalFlag"
                                AutoPostBack="true" OnCheckedChanged="rdoRemedy_AbnormalFlag2_CheckedChanged" />
                            <asp:DropDownList ID="ddlRemedy_AbnormalReasonID" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="rdoRemedy_AbnormalFlag1" />
                            <asp:PostBackTrigger ControlID="rdoRemedy_AbnormalFlag2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedy_Abnormal" runat="server" Text="補登其他說明："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <%--<asp:Label ID="lblRemedy_AbnormalDesc" runat="server"></asp:Label>--%>
                    <asp:TextBox ID="txtRemedy_AbnormalDesc" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="frameAttach" runat="server" />
        <uc1:ucModalPopup ID="ucModalPopup1" runat="server" />
    </fieldset>
    <%--<div id="DivHiddenArea" style="height: auto; overflow: scroll;">--%>
        <div id="DivHiddenArea" style="display: none; height: auto; overflow: scroll;">
        <asp:Panel ID="pnlVerify" runat="server">
        <fieldset class="Util_Fieldset">
        <legend class="Util_Legend">修改後資料</legend>
            <table width="100%" cellpadding="1" cellspacing="1">
                <tr class="Util_clsRow1">
                    <td width="40%" align="left"  >
                        <asp:Label ID="Label1" runat="server" Text="員編姓名："></asp:Label>
                    </td>
                    <td align="left"  >
                        <asp:Label ID="lblEmpName_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow2">
                    <td width="40%" align="left">
                        <asp:Label ID="Label3" runat="server" Text="打卡日期："></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:Label ID="lblPunchDate_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow1">
                    <td width="40%" align="left">
                        <asp:Label ID="Label5" runat="server" Text="打卡時間："></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:Label ID="lblPunchTime_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow2">
                    <td width="40%" align="left">
                        <asp:Label ID="Label7" runat="server" Text="打卡類型："></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:Label ID="lblConfirmPunchFlag_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow1">
                    <td width="40%" align="left">
                        <asp:Label ID="Label14" runat="server" Text="補登理由："></asp:Label>
                    </td>
                    <td align="left" >
                        <%--<asp:Label ID="lblRemedyReasonID" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblRemedyReasonID_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow2">
                    <td width="40%" align="left">
                        <asp:Label ID="Label15" runat="server" Text="補登打卡時間："></asp:Label>
                    </td>
                    <td align="left" class="style3">
                        <asp:Label ID="lblRemedyPunchTime_Send" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow1">
                    <td width="40%" align="left">
                        <asp:Label ID="Label9" runat="server" Text="補登原因："></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:RadioButton ID="rdoRemedy_AbnormalFlag1_Send" Text="處理公務" runat="server" GroupName="grpAbnormalFlag"
                            Enabled="False" />
                        <asp:RadioButton ID="rdoRemedy_AbnormalFlag2_Send" Text="非處理公務" runat="server" GroupName="grpAbnormalFlag"
                            Enabled="False" />
                        <asp:Label ID="lbl_Send" runat="server" Text="-" Visible="false"></asp:Label>
                        <asp:Label ID="lblRemedy_AbnormalReasonID_Send" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px" class="Util_clsRow2">
                    <td width="40%" align="left">
                        <asp:Label ID="Label12" runat="server" Text="補登其他說明："></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:Label ID="lblRemedy_AbnormalDesc_Send" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
            <br />
            <center>
            <asp:Label ID="Label18" runat="server" Text=" 修改資料確定送簽?" Font-Size="20"></asp:Label>
            <hr class="Util_clsHR" />
                <br />
                <asp:Button ID="btnOK" runat="server" Text="確定" CssClass="Util_clsBtn" OnClick="btnOK_Click" />
                <asp:Button ID="btnNo" runat="server" CausesValidation="False" Text="取消" CssClass="Util_clsBtn"
                    OnClick="btnNo_Click" />
            </center>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
