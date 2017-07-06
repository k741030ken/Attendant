<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchUpdateInquire.aspx.cs" Inherits="Punch_PunchUpdateInquire" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>打卡補登查詢</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
    </style>
</head>
<script type="text/javascript">
    </script>

<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <uc1:ucLightBox runat="server" ID="ucLightBox" />
    <asp:HiddenField ID="ScreenWidth" runat="server" />
    <asp:HiddenField ID="ScreenHeight" runat="server" />
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">打卡補登查詢</legend>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">

        <tr>
            <td align="left">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height:20px">
                    <td colspan="5" align="left">
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnClear" runat="server" Text="清除" onclick="btnClear_Click" CssClass="Util_clsBtn" />
                    </td>
                    </tr>
                    <tr style="height: 20px" class="Util_clsRow1" ID="trDate" runat="server" >
                        <td  colspan="5">
                            <asp:Label ID="lblPunchDate" runat="server" Text="*查詢日期(起、迄)："></asp:Label>
                             <asp:ucDate ID="ucStartPunchDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px"/>
                            <asp:Label ID="lblW" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="ucEndPunchDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                        </td>
                        </tr>

                    <tr style="height:20px" class="Util_clsRow2" id="trDDL" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblConfirmPunchFlag" runat="server" Text="打卡類型：" ></asp:Label>
                            <asp:DropDownList ID="ddlConfirmPunchFlag" runat="server">
                                <asp:ListItem Value="">---請選擇---</asp:ListItem>
                                <asp:ListItem Value="1">1:上班</asp:ListItem>
                                <asp:ListItem Value="2">2:下班</asp:ListItem>
                                <asp:ListItem Value="3">3:午休開始</asp:ListItem>
                                <asp:ListItem Value="4">4:午休結束</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td width="20%" align="left" >
                         <asp:Label ID="lblStatus" runat="server" Text="狀態：" ></asp:Label>
                            <asp:DropDownList ID="ddlConfirmStatus" runat="server">
                                <asp:ListItem Value="">---請選擇---</asp:ListItem>
                                <asp:ListItem Value="1">異常</asp:ListItem>
                                <asp:ListItem Value="2">送簽中</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                         <td width="20%" align="left" >
                         <asp:Label ID="lblAbnormalFlag" runat="server" Text="原因：" ></asp:Label>
                            <asp:DropDownList ID="ddlAbnormalFlag" runat="server">
                                <asp:ListItem Value="">---請選擇---</asp:ListItem>
                                <asp:ListItem Value="1">1:處理公務</asp:ListItem>
                                <asp:ListItem Value="2">2:非處理公務</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow1" id="trLabel">
                        <td width="20%" align="left">
                        <asp:Label ID="lblComp" runat="server" Text="公司："></asp:Label>
                        <asp:Label ID="lblCompName" runat="server"></asp:Label>
                        </td>
                        <td style="width:20%" align="left">                                
                            <asp:Label ID="lblOrgan" runat="server" Text="單位："></asp:Label>
                            <asp:Label ID="lblOrganName" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                            <asp:Label ID="lblEmp" runat="server" Text="員編姓名："></asp:Label>
                            <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="職稱："></asp:Label>
                            <asp:Label ID="lblTitleName" runat="server"></asp:Label>
                        </td>
                        <td width="20%" align="left">
                            <asp:Label ID="lblPosition" runat="server" Text="職位："></asp:Label>
                            <asp:Label ID="lblPositionName" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    <div style ="height:75%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" 
DataKeyNames="CompID,EmpID,EmpName,DutyDate,DutyTime,PunchDate,PunchTime,PunchConfirmSeq,DeptID,DeptName,OrganID,OrganName,FlowOrganID,FlowOrganName,Sex,PunchFlag,WorkTypeID,WorkType,MAFT10_FLAG,ConfirmStatus,AbnormalType,ConfirmPunchFlag,PunchSeq,PunchRemedySeq,RemedyReasonID,RemedyReasonCN,RemedyPunchTime,AbnormalFlag,AbnormalReasonID,AbnormalReasonCN,AbnormalDesc,Remedy_AbnormalFlag,Remedy_AbnormalReasonID,Remedy_AbnormalReasonCN,Remedy_AbnormalDesc,Source,APPContent,LastChgComp,LastChgID,LastChgDate,RemedyPunchFlag,BatchFlag,PORemedyStatus,RejectReason,RejectReasonCN,ValidDateTime,ValidCompID,ValidID,ValidName,Remedy_MAFT10_FLAG,ConfirmStatusGCN,ConfirmPunchFlagGCN,AbnormalReasonGCN,SourceGCN,SexGCN,FlowCaseID" 
            Width="100%" onrowcommand="gvMain_RowCommand" 
             onrowdatabound="gvMain_RowDataBound" >
        <Columns>
             <asp:TemplateField HeaderText="功能" ShowHeader="False" >
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                        Text="修改" ></asp:LinkButton>
                </ItemTemplate>
                 <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="lblNumber" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:BoundField HeaderText="狀態" DataField="ConfirmStatusGCN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="打卡日期" DataField="PunchDate">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="時間" DataField="PunchTime">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="類型" DataField="ConfirmPunchFlagGCN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="來源" DataField="SourceGCN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="原因" DataField="AbnormalReasonGCN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="其他說明" DataField="AbnormalDesc">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="tr_evenline" />
                        <AlternatingRowStyle CssClass="tr_oddline" />
                        <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                        <PagerStyle CssClass="GridView_PagerStyle" />
    </asp:GridView>
    </div>
                <uc:ucModalPopup ID="ucModalPopup1" runat="server" />
    </form>
</body>
</html>
