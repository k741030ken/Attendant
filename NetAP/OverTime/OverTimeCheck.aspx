<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverTimeCheck.aspx.cs" Inherits="OverTime_OverTimeCheck" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
<%@ Register src="../Util/ucUserPicker.ascx" tagname="ucUserPicker" tagprefix="uc1" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>加班單－審核畫面</title>
</head>
<script type="text/javascript">
    function chkAllAdvance_CheckAll(ChkA) {
        var chk = 'gvMain.*chkOverTimeA';
        re = new RegExp(chk);
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'checkbox' ) {
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
                    <asp:Button ID="btnCheck" runat="server" Text="審核" onclick="btnCheck_Click" CssClass="Util_clsBtnGray Util_Pointer" />  <%--
                    <asp:Button ID="btnReject" runat="server" Text="駁回" onclick="btnReject_Click" CssClass="Util_clsBtnGray Util_Pointer" /> --%> 
                    <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
                </td>
            </tr>   
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="False" 
                        AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" 
                        CellPadding="2"
                        DataKeyNames="OTRegisterComp,AssignTo,OTCompID,OTEmpID,OTDate,OTTime,AfterOTDate,AfterOTTime,FlowCaseID,AfterFlowCaseID,OTSeq,AfterOTSeq" Width="100%" PageSize="15" 
                        HeaderStyle-ForeColor="dimgray" onrowdatabound="gvMain_RowDataBound" 
                        onrowcreated="gvMain_RowCreated" onrowcommand="gvMain_RowCommand">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <Columns>
                            <%--加班單預先申請--%>
                            
                            <asp:BoundField DataField="OTEmpID" HeaderText="員工編號">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OTNameN" HeaderText="加班人">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>      
                            <asp:TemplateField HeaderText="選取">
                                <HeaderStyle CssClass="td_header" Width="3%" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOverTimeA" runat="server" Enabled="false"  />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="明細" ShowHeader="False" ControlStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <asp:ImageButton ID="iBtnApproveA" runat="server" ImageUrl="../Util/WebClient/Icon_Approve.png"  CommandName="DetailA" Enabled="false"/>
                                    <%--<asp:LinkButton ID="lbtnDetailA" runat="server" CausesValidation="false" CommandName="DetailA" Text="明細" Enabled="false" ></asp:LinkButton>--%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>                                
                            <asp:BoundField DataField="OTTypeID" HeaderText="加班類型">
                                <HeaderStyle Width="7%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OTReasonMemo" HeaderText="加班理由">
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OTDate" HeaderText="加班日期">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OTTime" HeaderText="加班起迄時間">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="OTFormNo" HeaderText="OTFormNo" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <%--加班單事後申報--%>
                            <asp:TemplateField HeaderText="選取">
                                <HeaderStyle CssClass="td_header" Width="3%" />
                                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkOverTimeD" runat="server" Enabled="false"/>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="明細" ShowHeader="False" ControlStyle-Font-Underline="true">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lbtnDetailD" runat="server" CausesValidation="false" CommandName="DetailD" Text="明細" Enabled="false" ></asp:LinkButton>--%>
                                    <asp:ImageButton ID="iBtnApproveD" runat="server" ImageUrl="../Util/WebClient/Icon_Approve.png"  CommandName="DetailD" Enabled="false"/>
                                </ItemTemplate>
                                <HeaderStyle CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="AfterOTTypeID" HeaderText="加班類型">
                                <HeaderStyle Width="7%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AfterOTReasonMemo" HeaderText="加班理由">
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AfterOTDate" HeaderText="加班日期">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AfterOTTime" HeaderText="加班起迄時間">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FlowCaseID" HeaderText="FlowCaseID" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="AfterFlowCaseID" HeaderText="AfterFlowCaseID" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OTSeq" HeaderText="FlowCaseID" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                            </asp:BoundField>
                             <asp:BoundField DataField="AfterOTSeq" HeaderText="AfterFlowCaseID" Visible="false">
                                <HeaderStyle Width="5%" CssClass="td_header" />
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
                 </td>
            </tr>
        </table>    
                <uc:ucModalPopup ID="ucModalPopup" runat="server" />
    </form>
</body>
</html>
