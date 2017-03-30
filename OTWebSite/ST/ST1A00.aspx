<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST1A00.aspx.vb" Inherits="ST_ST1A00" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow('')) {
                        alert("未選取資料列！");
                        return false;
                    }
            }

            switch (Param) {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }

        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex)
                { }
            }
        }
       -->
    </script>
    <style type="text/css">
        .style1
        {
            width: 153px;
        }
        .style2
        {
            width: 12%;
        }
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="20%" colspan="3">                                
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtEmpID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpName" Font-Size="15px" runat="server" Text="員工姓名："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:Label ID="txtEmpName" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td width="5%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label1" Font-Size="15px" runat="server" Text="► 企業團年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenComp_SPHOLD" runat="server" GridViewName="gvMain_EmpSenComp_SPHOLD" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenComp_SPHOLD" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkStatus" HeaderText="任職代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="任職狀態" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ConSen" HeaderText="公司連續年資" ReadOnly="True" SortExpression="ConSen" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="公司累計年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen_SPHOLD" HeaderText="企業團年資" ReadOnly="True" SortExpression="TotSen_SPHOLD" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label2" Font-Size="15px" runat="server" Text="► 公司年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenComp" runat="server" GridViewName="gvMain_EmpSenComp" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenComp" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkStatus" HeaderText="任職代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="任職狀態" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ConSen" HeaderText="公司連續年資" ReadOnly="True" SortExpression="ConSen" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="公司累計年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen_SPHOLD" HeaderText="企業團年資" ReadOnly="True" SortExpression="TotSen_SPHOLD" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label4" Font-Size="15px" runat="server" Text="► 單位年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenOrgType" runat="server" GridViewName="gvMain_EmpSenOrgType" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenOrgType" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OrgTypeName" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="OrgType" HeaderText="單位類別代碼" ReadOnly="True" SortExpression="OrgType" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrgTypeName" HeaderText="單位類別名稱" ReadOnly="True" SortExpression="OrgTypeName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurrentSen" HeaderText="現況單位年資" ReadOnly="True" SortExpression="CurrentSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="累計單位年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label5" Font-Size="15px" runat="server" Text="► 簽核單位年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenOrgTypeFlow" runat="server" GridViewName="gvMain_EmpSenOrgTypeFlow" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenOrgTypeFlow" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OrgTypeFlowName" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="OrgType" HeaderText="簽核單位代碼" ReadOnly="True" SortExpression="OrgType" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrgTypeFlowName" HeaderText="簽核單位名稱" ReadOnly="True" SortExpression="OrgTypeFlowName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurrentSen" HeaderText="現況單位年資" ReadOnly="True" SortExpression="CurrentSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="累計單位年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label3" Font-Size="15px" runat="server" Text="► 職等年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenRank" runat="server" GridViewName="gvMain_EmpSenRank" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenRank" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="RankID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="RankID" HeaderText="職等代碼" ReadOnly="True" SortExpression="RankID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="TitleName" HeaderText="職稱" ReadOnly="True" SortExpression="TitleName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurrentSen" HeaderText="現況職等年資" ReadOnly="True" SortExpression="CurrentSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="累計職等年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label6" Font-Size="15px" runat="server" Text="► 工作性質年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenWorkType" runat="server" GridViewName="gvMain_EmpSenWorkType" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenWorkType" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="WorkTypeName" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="WorkTypeID" HeaderText="工作性質代碼" ReadOnly="True" SortExpression="WorkTypeID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WorkTypeName" HeaderText="工作性質名稱" ReadOnly="True" SortExpression="WorkTypeName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurrentSen" HeaderText="現況工作性質年資" ReadOnly="True" SortExpression="CurrentSen" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="累計工作性質年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIName" HeaderText="大類" ReadOnly="True" SortExpression="CategoryIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryI" HeaderText="大類累計年資" ReadOnly="True" SortExpression="TotCategoryI" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIIName" HeaderText="中類" ReadOnly="True" SortExpression="CategoryIIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryII" HeaderText="中類累計年資" ReadOnly="True" SortExpression="TotCategoryII" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIIIName" HeaderText="細類" ReadOnly="True" SortExpression="CategoryIIIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryIII" HeaderText="細類累計年資" ReadOnly="True" SortExpression="TotCategoryIII" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
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
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="100%">
                        <tr>
                            <td width="4%"></td>
                            <td align="left" width="97%">
                                <asp:Label ID="Label7" Font-Size="15px" runat="server" Text="► 職位年資"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain_EmpSenPosition" runat="server" GridViewName="gvMain_EmpSenPosition" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain_EmpSenPosition" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="PositionName" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="PositionID" HeaderText="職位代碼" ReadOnly="True" SortExpression="PositionID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PositionName" HeaderText="職位名稱" ReadOnly="True" SortExpression="PositionName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ValidDateB" HeaderText="生效起日" ReadOnly="True" SortExpression="ValidDateB" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="ValidDateE" HeaderText="生效迄日" ReadOnly="True" SortExpression="ValidDateE" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CurrentSen" HeaderText="現況職位年資" ReadOnly="True" SortExpression="CurrentSen" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotSen" HeaderText="累計職位年資" ReadOnly="True" SortExpression="TotSen" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIName" HeaderText="大類" ReadOnly="True" SortExpression="CategoryIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryI" HeaderText="大類累計年資" ReadOnly="True" SortExpression="TotCategoryI" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIIName" HeaderText="中類" ReadOnly="True" SortExpression="CategoryIIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryII" HeaderText="中類累計年資" ReadOnly="True" SortExpression="TotCategoryII" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CategoryIIIName" HeaderText="細類" ReadOnly="True" SortExpression="CategoryIIIName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotCategoryIII" HeaderText="細類累計年資" ReadOnly="True" SortExpression="TotCategoryIII" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動人員" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動時間" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
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
                </td>
            </tr>

        </table>
    </form>
</body>
</html>
