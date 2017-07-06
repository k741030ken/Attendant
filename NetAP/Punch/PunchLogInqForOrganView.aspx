<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PunchLogInqForOrganView.aspx.cs" Inherits="Punch_PunchLogInqForOrganView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGetEmpID.ascx" TagName="ucGetEmpID" TagPrefix="uc" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>打卡紀錄查詢--單位</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
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
                    <td colspan="10" align="left">
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space4" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnActionX" runat="server" Text="清除" onclick="btnActionX_Click" CssClass="Util_clsBtn" />
                    </td>
                    </tr>
                    <tr id="Tr2" class="Util_clsRow2" runat="server">
                        <td align="left" colspan="2" width = "20%">
                         <asp:Label ID="Label5" runat="server" Text="公司"></asp:Label>
                        </td>
                        <td align="left" colspan="8">
                         <asp:Label ID="lblCompID" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" colspan="2">
                         <asp:Label ID="Label2" runat="server" Text="組織別"></asp:Label>
                        </td>
                        <td align="left" colspan="8">
                         <asp:DropDownList ID="ddlOrganization" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrganization_Changed">
                            <asp:ListItem Value="Organ" Text="行政組織"></asp:ListItem>
                            <asp:ListItem Value="FlowOrgan" Text="功能組織"></asp:ListItem>
                         </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="Organ" class="Util_clsRow2" runat="server">
                        <td align="left" colspan="2">
                         <asp:Label ID="lblOrganID" runat="server" Text="行政組織："></asp:Label>
                        </td>
                        <td align="left" colspan = "8">
                         <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrgType_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptID_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlOrganID" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="FlowOrgan" class="Util_clsRow2" runat="server" visible = "false">
                        <td align="left" colspan="2">
                         <asp:Label ID="lblFlowOrganID" runat="server" Text="功能組織："></asp:Label>
                        </td>
                        <td align="left" colspan = "8">
                         <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode40_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode30_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoleCode20_Changed"></asp:DropDownList>
                         <asp:DropDownList ID="ddlRoleCode10" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                     <tr class="Util_clsRow1" runat="server">
                        <td align="left" colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="*查詢日期" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
                        </td>
                        <td align="left" colspan="8">
                            <asp:ucDate ID="ucStartDate" runat="server" />
                            <asp:Label ID="Label3" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="ucEndDate" runat="server" />
                            <asp:Label ID="Label4" runat="server" Text="(查詢區間限制最多1個月以內最近5年的資料)"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" colspan="2">
                            <asp:Label ID="lblTimeBegin" runat="server" Text="查詢時間：" Font-Names="微軟正黑體" ></asp:Label>
                        </td>
                        <td align="left" colspan="8">
                            <asp:DropDownList ID="StartTimeH" runat="server" Width = "8%">
                            <asp:ListItem Value="" Text="--請選擇--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                            <asp:DropDownList ID="StartTimeM" runat="server" Width = "8%">
                            <asp:ListItem Value="" Text="--請選擇--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label7" runat="server" Text=" ～ "></asp:Label>
                            <asp:DropDownList ID="EndTimeH" runat="server" Width = "8%">
                            <asp:ListItem Value="" Text="--請選擇--"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label8" runat="server" Text=" : "></asp:Label>
                            <asp:DropDownList ID="EndTimeM" runat="server" Width = "8%">
                            <asp:ListItem Value="" Text="--請選擇--"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td align="left" colspan = "2">
                         <asp:Label ID="lblquery" runat="server" Text="*查詢類別"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="space8" runat="server" Text=" "></asp:Label>
                            <asp:DropDownList ID="ddlSearchType" runat="server">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="未刷卡"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="上下班刷卡"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="逾正常工時"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="補刷卡"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width = "10%">
                         <asp:Label ID="Label9" runat="server" Text="類型"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label10" runat="server" Text=" "></asp:Label>
                            <asp:DropDownList ID="ddlConfirmPunchFlag" runat="server">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="上班"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="下班"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="午休開始"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="午休結束"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width = "10%">
                         <asp:Label ID="Label11" runat="server" Text="狀態"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:Label ID="Label12" runat="server" Text=" "></asp:Label>
                            <asp:DropDownList ID="ddlConfirmStatus" runat="server">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="異常"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="送簽中"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" width = "10%">
                         <asp:Label ID="Label13" runat="server" Text="原因"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="Label14" runat="server" Text=" "></asp:Label>
                            <asp:DropDownList ID="ddlRemedy_AbnormalFlag" Width="60%" runat="server">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="處理公務"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="非處理公務"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="Util_clsRow2" runat="server">
                        <td align="left" colspan="2">
                         <asp:Label ID="lblEmpID" runat="server" Text="員工編號"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                         <asp:TextBox ID="txtEmpID" runat="server" Width="40%" MaxLength="6" AutoPostBack = "true" ontextchanged="txtEmpID_TextChanged"></asp:TextBox>
                         <uc:ucGetEmpID ID="btnEmpID" runat="server" />
                        </td>
                        <td align="left">
                         <asp:Label ID="lblEmpName" runat="server" Text="員工姓名"></asp:Label>
                        </td>
                        <td align="left" colspan="5">
                         <asp:TextBox ID="txtEmpName" runat="server" Width="12%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    <div style ="height:100%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="AbnormalType_Show,PunchDate,PunchTime,ConfirmPunchFlag_Show,Source_Show,OrganName_Show,EmpID,EmpName,Remedy_AbnormalReasonCN,RotateFlag" 
            onrowdatabound="gvMain_RowDataBound" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="GridNo" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="狀態" DataField="AbnormalType_Show" SortExpression="AbnormalType_Show">
                <HeaderStyle Width="4%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="打卡日期" DataField="PunchDate" SortExpression="PunchDate">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="時間" DataField="PunchTime" SortExpression="PunchTime">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="類型" DataField="ConfirmPunchFlag_Show" SortExpression="ConfirmPunchFlag_Show">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="來源" DataField="Source_Show" SortExpression="Source_Show">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="打卡單位" DataField="OrganName_Show" SortExpression="OrganName_Show">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="員工編號" DataField="EmpID" SortExpression="EmpID">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="員工姓名" DataField="EmpName" SortExpression="EmpName">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="原因" DataField="Remedy_AbnormalReasonCN" SortExpression="Remedy_AbnormalReasonCN">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="輪班人員" DataField="RotateFlag" SortExpression="RotateFlag">
                <HeaderStyle Width="10%" CssClass="td_header" />
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
    </form>
</body>
</html>
