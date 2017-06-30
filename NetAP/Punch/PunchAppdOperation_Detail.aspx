<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchAppdOperation_Detail.aspx.cs"
    Inherits="Punch_PunchAppdOperation_Detail" %>

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
</script>
<body>
    <form id="form1" runat="server">
    <fieldset class="Util_Fieldset">
        <table width="100%" cellpadding="1" cellspacing="1">
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
                    <asp:Label ID="lblRemedyReasonID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedyPunchTime_not" runat="server" Text="補登打卡時間："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <asp:Label ID="lblRemedyPunchTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow2">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedy_AbnormalReason" runat="server" Text="補登原因："></asp:Label>
                </td>
                <td width="45%" align="left" colspan="3">
                    <asp:RadioButton ID="rdoRemedy_AbnormalFlag1" Text="處理公務" runat="server" GroupName="grpRemedy_AbnormalFlag" />
                    <asp:RadioButton ID="rdoRemedy_AbnormalFlag2" Text="非處理公務" runat="server" GroupName="grpRemedy_AbnormalFlag" />
                    <asp:Label ID="lblRemedy_AbnormalReasonCN" runat="server"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px" class="Util_clsRow1">
                <td width="10%" align="left">
                    <asp:Label ID="lblRemedy_Abnormal" runat="server" Text="補登其他說明："></asp:Label>
                </td>
                <td style="width: 35%" align="left" colspan="3">
                    <asp:Label ID="lblRemedy_AbnormalDesc" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
    </form>
</body>
</html>
