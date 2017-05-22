<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnBizReqAppdOperation.aspx.cs" Inherits="OverTime_OnBizReqAppdOperation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
<%@ Register src="../Util/ucUserPicker.ascx" tagname="ucUserPicker" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>公出單－審核畫面</title>
</head>
<script type="text/javascript">
    function chkAllAdvance_CheckAll(ChkA) {
        var chk = 'gvMain.*chkOverTimeA';
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
    function chkDeclaration_CheckAll(ChkD) {
        var chk = 'gvMain.*chkOverTimeD';
        re = new RegExp(chk);
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'checkbox') {
                if (re.test(elm.name)) {
                    if (elm.disabled == false) {
                        if (ChkD.checked == true) {
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
        	font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        	color :#000000;
        }
 </style>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
        <table style="width:100%">
            <tr>
                <td>   
                    <asp:Button ID="btnRelease" runat="server" Text="審核" onclick="btnRelease_Click" CssClass="Util_clsBtnGray Util_Pointer" />  
                    <%--<asp:Button ID="btnReject" runat="server" Text="駁回" onclick="btnReject_Click" CssClass="Util_clsBtnGray Util_Pointer" /> --%> 
                </td>
            </tr>   
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="gvCheckVisitForm" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,EmpID,EmpID_NameN,WriteDate,DeputyID_Name,VisitBeginDate,BeginTime,VisitEndDate,EndTime,VisitReasonCN,FormSeq,FlowCaseID,FlowLogID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" onrowcommand="gvCheckVisitForm_RowCommand">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <Columns>
                            <%--加班單預先申請--%>
                            <asp:TemplateField HeaderText="選取">
                                <HeaderStyle CssClass="td_header" Width="3%" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkChoiced" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="序號" >
                                <ItemTemplate>
                                    <asp:Label ID="lblGridNo" runat="server" 
                                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="內容" ShowHeader="False" ControlStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細" ></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpID_NameN" HeaderText="公出人員">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeputyID_Name" HeaderText="代理人員">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>               
                            <asp:BoundField DataField="VisitBeginDate" HeaderText="開始日期">
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BeginTime" HeaderText="開始時間">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisitEndDate" HeaderText="結束日期">
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EndTime" HeaderText="結束時間">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisitReasonCN" HeaderText="洽辦事由">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CompID" HeaderText="公出人員公司" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpID" HeaderText="公出人員編號" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WriteDate" HeaderText="登錄日期" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="FormSeq" HeaderText="打卡紀錄確認序號" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="核准/駁回" >
                                <ItemTemplate>
                                    <asp:RadioButton ID="rbnApproved" GroupName="rbnCheckType" runat="server" Text="核准" AutoPostBack="true" Font-Names="微軟正黑體" />
                                    <asp:RadioButton ID="rbnReject" GroupName="rbnCheckType" runat="server" Text="駁回" AutoPostBack="true" Font-Names="微軟正黑體" />
                                </ItemTemplate>
                                <HeaderStyle Width="8%" Height="15px" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="審核意見" >
                                <ItemTemplate>
                                    <asp:TextBox id="txtReson" rows="2" runat="server" class="InputTextStyle_Thin" TextMode="MultiLine" Width="95%" Height="60px" ></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" Height="15px" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
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
               <%-- <uc:ucModalPopup ID="ucModalPopup" runat="server" />--%>
    </form>
</body>
</html>

