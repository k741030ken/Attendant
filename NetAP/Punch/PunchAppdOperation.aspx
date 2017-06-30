<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchAppdOperation.aspx.cs"
    Inherits="Punch_PunchAppdOperation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
<%@ Register Src="../Util/ucUserPicker.ascx" TagName="ucUserPicker" TagPrefix="uc1" %>
<!DOCTYPE html>
<script type="text/javascript">


    function chkAllAdvance_CheckAll(ChkA) {
        var chk = 'gvMain.*chkChoiced';
        re = new RegExp(chk);
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'checkbox') {
                if (re.test(elm.name)) {
                    if (elm.disabled == false) {
                        if (ChkA.checked == true) {
                            elm.checked = true;
                        }
                        else {
                            elm.checked = false;
                        }
                    }
                }
            }
        }
    }
</script>
<style type="text/css">
    body
    {
        font-family: "微軟正黑體" , "微软雅黑" , "Microsoft JhengHei" , Arial, sans-serif;
        color: #000000;
    }
</style>
<html>
<head runat="server">
    <title>打卡補登修改－複核畫面</title>
</head>


<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Button ID="btnCheck" runat="server" Text="審核" OnClick="btnCheck_Click" CssClass="Util_clsBtnGray Util_Pointer" />
                <%--<asp:Button ID="btnReject" runat="server" Text="駁回" onclick="btnReject_Click" CssClass="Util_clsBtnGray Util_Pointer" />--%>
                <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" PageSize="15" HeaderStyle-ForeColor="dimgray" onrowcommand="gvMain_RowCommand"
DataKeyNames="CompID ,EmpID ,EmpName ,DutyDate ,DutyTime ,PunchDate ,PunchTime ,PunchConfirmSeq ,DeptID ,DeptName ,OrganID ,OrganName ,FlowOrganID ,FlowOrganName ,Sex ,PunchFlag ,WorkTypeID ,WorkType ,MAFT10_FLAG ,ConfirmStatus ,AbnormalType ,ConfirmPunchFlag ,PunchSeq ,PunchRemedySeq ,RemedyReasonID ,RemedyReasonCN ,RemedyPunchTime ,AbnormalFlag ,AbnormalReasonID ,AbnormalReasonCN ,AbnormalDesc ,Remedy_AbnormalFlag ,Remedy_AbnormalReasonID ,Remedy_AbnormalReasonCN ,Remedy_AbnormalDesc ,Source ,APPContent ,LastChgComp ,LastChgID ,LastChgDate,RemedyPunchFlag,BatchFlag ,PORemedyStatus ,RejectReason ,RejectReasonCN ,ValidDateTime ,ValidCompID ,ValidID ,ValidName  ,Remedy_MAFT10_FLAG,ConfirmStatusGCN,ConfirmPunchFlagGCN,AbnormalReasonGCN,SourceGCN,SexGCN,SpecialFlag,RestBeginTime,RestEndTime,FlowCaseID " 
            Width="100%"  >
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />

        <Columns>
        <asp:TemplateField HeaderText="選取">
                            <HeaderStyle CssClass="td_header" Width="3%" />
                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkChoiced" runat="server" />
                                <%--<asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
             <asp:TemplateField HeaderText="明細" ShowHeader="False" ControlStyle-Font-Underline="true">
                            <ItemTemplate>
                                <asp:ImageButton ID="iBtnApprove" runat="server" ImageUrl="../Util/WebClient/Icon_Approve.png"
                                    CommandName="Detail" Enabled="true" />
                                <%--<asp:LinkButton ID="lbtnDetailA" runat="server" CausesValidation="false" CommandName="DetailA" Text="明細" Enabled="false" ></asp:LinkButton>--%>
                            </ItemTemplate>
                            <HeaderStyle CssClass="td_header" />
                            <ItemStyle CssClass="td_detail" Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="lblNumber" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
             <asp:BoundField HeaderText="員編" DataField="EmpID">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:BoundField HeaderText="姓名" DataField="EmpName">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:BoundField HeaderText="性別" DataField="Sex">
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
            <asp:BoundField HeaderText="原因" DataField="AbnormalReasonGCN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="其他說明" DataField="AbnormalDesc">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="核准/駁回">
                            <ItemTemplate>
                                <asp:RadioButton ID="rbnApproved" GroupName="rbnCheckType" runat="server" Text="核准" Checked="true"
                                    Font-Names="微軟正黑體" />
                                <asp:RadioButton ID="rbnReject" GroupName="rbnCheckType" runat="server" Text="駁回"
                                    Font-Names="微軟正黑體" />
                            </ItemTemplate>
                            <HeaderStyle Width="8%" Height="15px" CssClass="td_header" />
                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="審核意見">
                            <ItemTemplate>
                                <asp:TextBox ID="txtReason" Rows="2" runat="server" class="InputTextStyle_Thin" TextMode="MultiLine"
                                    Width="95%" Height="60px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" Height="15px" CssClass="td_header" />
                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                        </asp:TemplateField>
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
            </td>
        </tr>
    </table>
    <uc:ucModalPopup ID="ucModalPopup" runat="server" />
    </form>
</body>
</html>
