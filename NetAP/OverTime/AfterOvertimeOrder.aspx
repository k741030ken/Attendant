<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AfterOvertimeOrder.aspx.cs" Inherits="OverTime_AfterOvertimeOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>
<!DOCTYPE">
<html>
<head id="Head1" runat="server">
    <title>事後申報</title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
    </style>
</head>
<script type="text/javascript" src="../Util/WebClient/jquery-1.8.3.min.js"></script>
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
    </script>

<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <uc1:ucLightBox runat="server" ID="ucLightBox" />
    <asp:HiddenField ID="ScreenWidth" runat="server" />
    <asp:HiddenField ID="ScreenHeight" runat="server" />
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">事後申報</legend>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height:20px">
                    <td colspan="3" align="left">
                        <asp:Button ID="btnAdd" runat="server" Text="新增" onclick="btnAdd_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="修改" onclick="btnUpdate_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" onclick="btnDelete_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnDeleteInvisible" runat="server" Text="刪除"  CssClass="Util_clsBtn" style="display:none;" onclick="btnDeleteInvisible_Click" />
                        <asp:Button ID="DeleteUpdateAdv" runat="server" Text=""  CssClass="Util_clsBtn" style="display:none;" onclick="DeleteUpdateAdv_Click"  />
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="送簽" CssClass="Util_clsBtn" onclick="btnSubmit_Click"/>&nbsp;&nbsp;
                        <asp:Button ID="btnSubmitInvisible" runat="server" Text="送簽"  CssClass="Util_clsBtn" style="display:none;" onclick="btnSubmitInvisible_Click" />
                        <asp:Button ID="btnSubmitInvisible_Reminder" runat="server" Text="送簽" CssClass="Util_clsBtn" style="display:none;"
                                    onclick="btnSubmitInvisible_Reminder_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="取消" CssClass="Util_clsBtn" onclick="btnCancel_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancelInvisible" runat="server" Text="取消"  CssClass="Util_clsBtn" style="display:none;" onclick="btnCancelInvisible_Click" />
                        <asp:Button ID="UpdateAdvance" runat="server" Text=""  CssClass="Util_clsBtn" style="display:none;" onclick="UpdateAdvance_Click" />
                        <asp:Button ID="btnActionX" runat="server" Text="清除" onclick="btnActionX_Click" CssClass="Util_clsBtn" />
                    </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow1">
                        <td width="30%" align="left">
                        <asp:Label ID="lblComp" runat="server" Text="公司："></asp:Label>
                        <asp:Label ID="lblOTCompName" runat="server"></asp:Label>
                        </td>
                        <td style="width:30%" align="left">                                
                            <asp:Label ID="lblDept" runat="server" Text="單位："></asp:Label>
                            <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="lblOrgan" runat="server" Text="科別："></asp:Label>
                            <asp:Label ID="lblOrganName" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr style="height: 20px" class="Util_clsRow2" ID="trSPUserOrgAndFlow" runat="server" >
                        <td>
                            <asp:Label ID="lblOrgAndFlow" runat="server" Text="特殊人員查詢權限："></asp:Label>
                            <asp:DropDownList ID="ddlOrgAndFlow" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrgAndFlow_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="----不使用----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="行政組織"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="功能組織"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="行政或功能組織"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        </tr>

                        <tr style="height: 20px" class="Util_clsRow1" id="trSPUser" runat="server">
                            <%--20170207 leo modify 增加該<tr>特殊人員登入可選下拉(行政-處/部/科，功能RoleCode40~0)--%>
                            <td width="30%" align="left" colspan="3">
                                <asp:Label ID="lblOrganization" runat="server" Text="行政組織："></asp:Label>
                                <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrgType_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDeptID_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlOrganID" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlOrganID_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                </td>
                                </tr>

                        <tr style="height: 20px" class="Util_clsRow2" id="trSPUserFlow" runat="server">
                        <td width="30%" align="left" colspan="3">
                                 <asp:Label ID="lblOrganizationFlow" runat="server" Text="功能組織："></asp:Label>
                                        <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoleCode40_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoleCode30_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoleCode20_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlRoleCode10" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoleCode10_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:DropDownList ID="ddlRoleCode0" runat="server">--%>
                                   <%--     <asp:DropDownList ID="ddlRoleCode0" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlRoleCode0_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                        </asp:DropDownList>--%>
                            </td>

                        </tr>

                    <tr style="height:20px" class="Util_clsRow1" id="trQuery" runat="server">
                        <td width="30%" align="left">
                        <asp:Label ID="lblQueryConditions" runat="server" Text="查詢條件："></asp:Label>
                        <asp:DropDownList ID="ddlQueryConditions" runat="server" style="font-family: '微軟正黑體', '微软雅黑', 'Microsoft JhengHei', 'Arial, sans-serif';" width="170px">
                        <asp:ListItem Value="0" Text="----請選擇----"></asp:ListItem>
                        <asp:ListItem Value="1" Text="填單人員"></asp:ListItem>
                        <asp:ListItem Value="2" Text="加班人員"></asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td style="width:30%" align="left">                                
                             <asp:Label ID="lblOTEmpID" runat="server" Text="員工編號："></asp:Label>
                             <asp:TextBox ID="txtOTEmpID" runat="server" Text="" AutoComplete="off"></asp:TextBox>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="lblEmpName" runat="server" Text="員工姓名："></asp:Label>&nbsp;
                            <asp:TextBox ID="txtOTEmpName" runat="server" Text="" AutoComplete="off"></asp:TextBox>
                            <%--<asp:ucSelectTextBox ID="txtOTEmpName" runat="server"/>--%>
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow2" id="trQuery2" runat="server">
                        <td width="30%" align="left">
                         <asp:Label ID="lblOTFormNO" runat="server" Text="表單編號："></asp:Label>
                         <asp:TextBox ID="txtOTFormNO" runat="server" Text="" AutoComplete="off" MaxLength="16"></asp:TextBox>
                        </td>
                        <td style="width:30%" align="left">                                
                                <asp:Label ID="lblOTStatus" runat="server" Text="狀態："></asp:Label>
                                <asp:DropDownList ID="ddlOTStatus" runat="server" style="font-family: '微軟正黑體', '微软雅黑', 'Microsoft JhengHei', 'Arial, sans-serif';" width="170px">
                                    <asp:ListItem Value="0" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="暫存"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="送簽"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="核准"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="駁回"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="刪除"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="取消"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="作廢"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="計薪後收回"></asp:ListItem>
                                </asp:DropDownList>
                        </td>
                        <td width="30%" align="left">
                            <asp:Label ID="lblOTDate" runat="server" Text="加班日期："></asp:Label>
                            <asp:ucDate ID="txtOTDateBegin" runat="server" />
                            <asp:Label ID="Label6" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="txtOTDateEnd" runat="server" />
                        </td>
                    </tr>

                    <tr style="height:20px" class="Util_clsRow1" id="trQuery3" runat="server">
                        <td width="30%" align="left" colspan="3">
                         <asp:Label ID="lblOTPayDate" runat="server" Text="計薪年月：" ForeColor="Red"></asp:Label>
                         <asp:TextBox ID="txtOTPayDate" runat="server" Text="" AutoComplete="off" MaxLength="6" ForeColor="Red"   ></asp:TextBox>
                         <asp:RegularExpressionValidator ID="regUnusualTime" runat="server" ControlToValidate="txtOTPayDate" Display="Dynamic" ErrorMessage="*[計薪年月格式請輸入 Ex:YYYYMM]" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                         &nbsp;&nbsp;
                         <asp:Label ID="lblOTPayDateMemo" runat="server" Text="(YYYYMM)" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    <div style ="height:75%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataKeyNames="OTCompID, OTEmpID, OTDate,OTTime,OTStatusName,OTTotalTime,OTTxnID,OTStatus,OTFormNO,OTFromAdvanceTxnId,FlowCaseID" AllowPaging="false" 
    onrowdatabound="gvMain_RowDataBound" onrowcommand="gvMain_RowCommand" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="全選">
                <HeaderTemplate> 
                    <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="全選" ToolTip="按一次全選，再按一次取消全選" /> 
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkChoose" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="30px" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="明細" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                        Text="明細" ></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="td_header" Width="4%" />
                <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="lblNumber" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:BoundField HeaderText="表單編號" ItemStyle-Width="1px" ItemStyle-Wrap="true" SortExpression="OTFormNO" DataField="OTFormNO" HtmlEncode="false">
                <HeaderStyle Width="80px" Height="15px"/>
                <ItemStyle HorizontalAlign="Center" Width="25px"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="狀態" DataField="OTStatusName">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班人編號" DataField="OTEmpID">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班人" DataField="NameN">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班類型" DataField="CodeCName">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班日期" DataField="OTDate">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班起迄時間" DataField="OTTime">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="加班時數" DataField="OTTotalTime">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="上傳附件" DataField="FileName">
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
    <!--送簽小視窗-->
         <%--<div style="display: none;">
        <!--固定隱藏區-->
        <asp:Panel ID="pnlFlow" runat="server">
            <br />
            <asp:Label ID="labVerifyCaseInfo" runat="server" Text="" />
            <br />
            <asp:Label ID="labOTEmpID" runat="server" Text="" />
            <br />
            <asp:Label ID="labVerifyAssignTo" runat="server" Text="" /><asp:DropDownList ID="ddlAssignTo"
                runat="server" />
            <br />
            <hr class="Util_clsHR" />
            <center>
                <asp:Button ID="btnVerify" runat="server" Text="OK" CssClass="Util_clsBtn" OnClick="btnVerify_Click" />
            </center>
        </asp:Panel>
    </div>--%>
                <uc:ucModalPopup ID="ucModalPopup1" runat="server" />
    </form>
</body>
</html>
