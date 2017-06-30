<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizRegInquireView.aspx.cs" Inherits="OnBiz_OnBizRegInquireView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGetEmpID.ascx" TagName="ucGetEmpID" TagPrefix="asp" %>
<%--<%@ Register Src="~/Util/ucWorkSite.ascx" TagName="ucWorkSite" TagPrefix="uc" %>--%>
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
    </style>
</head>
<%--<script type="text/javascript" src="../Util/WebClient/jquery-1.8.3.min.js"></script>
<script type="text/javascript">

    function PageLoad() {
        $("#ScreenWidth").val($(window).width());
        $("#ScreenHeight").val($(window).height());
    }
    function SelectAllCheckboxes(spanChk) {
        elm = document.forms[0];
        for (i = 0; i <= elm.length - 1; i++) {
            if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
                if (elm.elements[i].checked != spanChk.checked)
                    elm.elements[i].click();
            }
        }
    }
    function funYesOrNoDelete() {
        if (confirm('是否要刪除？')) {
            document.getElementById('btnDeleteInvisible').click();
        }
    }
    function funYesOrNoCancel() {
        if (confirm('是否要取消？')) {
            document.getElementById('btnCancelInvisible').click();
        }
    }
//    function funYesOrNoSubmit() {
//        if (confirm('是否要送簽？')) {
//            document.getElementById('btnSubmit').setAttribute("disabled", "disabled");
//            document.getElementById('btnSubmitInvisible').click();
//        }
//    }

    function funYesOrNoSubmit(msg) {
        if (confirm(msg)) {
            document.getElementById('btnSubmitInvisible').click();
        }
    }
    function funYesOrNoSubmit_Reminder(msg) {
        alert(msg);
        document.getElementById('btnSubmitInvisible_Reminder').click();

    }
    function funYesOrNoUpdateAdvance(msg) {
        if (confirm(msg)) {
            document.getElementById('UpdateAdvance').click();
        }
    }
    function funYesOrNoDeleUpdateAdv(msg) {
        if (confirm(msg)) {
            document.getElementById('DeleteUpdateAdv').click();
        }
    }
    </script>--%>

<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <fieldset class="Util_Fieldset">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height:20px">
                    <td colspan="3" align="left">
                        <asp:Button ID="btnAdd" runat="server" Text="新增" onclick="btnAdd_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space1" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnUpdate" runat="server" Text="修改" onclick="btnUpdate_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space2" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" onclick="btnDelete_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space3" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space4" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnCancel" runat="server" Text="取消" onclick="btnCancel_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space5" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnLogoff" runat="server" Text="註銷" onclick="btnLogoff_Click" CssClass="Util_clsBtn" />
                        <asp:Label ID="Space7" runat="server" Text="　"></asp:Label>
                        <asp:Button ID="btnActionX" runat="server" Text="清除" onclick="btnActionX_Click" CssClass="Util_clsBtn" />
                    </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblquery" runat="server" Text="*查詢條件"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="space8" runat="server" Text=" "></asp:Label>
                            <asp:DropDownList ID="ddlOBUseType" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="填單人"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="公出人"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="20%" align="left">
                         <asp:Label ID="lblEmpID_NameN" runat="server" Text=" 員編姓名"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                        <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                        
                        </td>
                    </tr>
                    <tr id="Tr1" class="Util_clsRow2" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="Label1" runat="server" Text="日期區間"></asp:Label>
                        </td>
                        <td width="30%" align="left" colspan = "3">
                            <asp:ucDate ID="ucStartDate" runat="server" />
                            <asp:Label ID="Label5" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="ucEndDate" runat="server" />
                            <asp:Label ID="Label3" runat="server" Text="(查詢區間限制最多1個月以內最近5年的資料)"></asp:Label>
                        </td>
                    </tr>
                    <tr class="Util_clsRow1" runat="server">
                        <td width="20%" align="left">
                         <asp:Label ID="lblOBFormStatus" runat="server" Text="狀態" ></asp:Label>
                        </td>
                        <td width="30%" align="left" colspan="3">
                        <asp:Label ID="Space9" runat="server" Text=" "></asp:Label>
                         <asp:DropDownList ID="ddlOBFormStatus" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="暫存"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="送簽"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="核准"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="駁回"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    <div style ="height:100%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="CompID,OBWriteDate,OBFormSeq,OBFormStatus,OBFormStatusName,FlowCaseID,FlowLogID,ValidID,ValidName,ValidID_Name,EmpID,EmpNameN,EmpID_NameN,DeputyID,DeputyName,DeputyID_Name,VisitBeginDate,BeginTime,VisitEndDate,EndTime,VisitReasonID,VisitReasonCN" 
    onrowdatabound="gvMain_RowDataBound" onrowcommand="gvMain_RowCommand" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="全選">
                <HeaderTemplate> 
                    <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="全選" ToolTip="按一次全選，再按一次取消全選" /> 
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="GridNo" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="內容" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細" disabled="true"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="td_header" Width="4%" />
                <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:BoundField HeaderText="狀態" DataField="OBFormStatusName" SortExpression="OBFormStatusName">
                <HeaderStyle Width="4%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>

            <asp:BoundField HeaderText="公出人員" DataField="EmpID_NameN" SortExpression="EmpID_NameN">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="代理人員" DataField="DeputyID_Name" SortExpression="DeputyID_Name">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="公出日期" DataField="VisitBeginDate" SortExpression="VisitBeginDate">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="開始時間" DataField="BeginTime" SortExpression="BeginTime">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="結束時間" DataField="EndTime" SortExpression="EndTime">
                <HeaderStyle Width="9%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="洽辦事由" DataField="VisitReasonCN" SortExpression="VisitReasonCN">
                <HeaderStyle Width="10%" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="簽核主管" DataField="ValidID_Name" SortExpression="ValidID_Name">
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
