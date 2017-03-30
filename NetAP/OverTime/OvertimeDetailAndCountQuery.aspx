<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OvertimeDetailAndCountQuery.aspx.cs" Inherits="OverTime_OvertimeDetailAndCountQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Util/ucDate.ascx" TagName="ucDate" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>

<!DOCTYPE>
<html>
<head runat="server">
    <title></title>
    <style type="text/css">
    body
    {
        font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        color :#000000;
    }
        .Util_clsBtn
        {}
    </style>
    <script type="text/javascript">
    function PageLoad() {
        $("#ScreenWidth").val($(window).width());
        $("#ScreenHeight").val($(window).height());
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <asp:HiddenField ID="ScreenWidth" runat="server" />
    <asp:HiddenField ID="ScreenHeight" runat="server" />
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">加班明細統計查詢</legend>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td align="left">
                <table width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height:20px">
                        <td colspan="3">
                            <asp:Button ID="btnQuery" runat="server" Text="查詢" CssClass="Util_clsBtn" 
                                onclick="btnQuery_Click"/>&nbsp;&nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="清除" CssClass="Util_clsBtn" 
                                onclick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow1">
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="查詢類別：" ></asp:Label>
                            <asp:DropDownList ID="ddlQueryType" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlQueryType_SelectedIndexChanged" >
                                <asp:ListItem Value="1" Text="明細查詢"></asp:ListItem>
                                <asp:ListItem Value="2" Text="統計查詢"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Panel ID="PanelAssignTo" runat="server">
                                <asp:CheckBox ID="chkAssignTo" runat="server" AutoPostBack="true" oncheckedchanged="chkAssignTo_CheckedChanged" />
                                <asp:Label ID="Label24" runat="server" Text="依簽核人員("></asp:Label>
                                <asp:Label ID="lblOTEmpID" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblOTEmpName" runat="server" Text=""></asp:Label>
                                <asp:Label ID="Label27" runat="server" Text=")查詢"></asp:Label>
                            </asp:Panel>
                        </td>
                        <td></td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow2">
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="公"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label18" runat="server" Text="司："></asp:Label>
                            <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlCompID_SelectedIndexChanged" 
                                style="height: 19px">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label3" runat="server" Text="行政組織："></asp:Label>
                            <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlOrgType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlDeptID_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOrganID" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlOrganID_SelectedIndexChanged">
                            </asp:DropDownList><br />
                             <asp:Label ID="Label4" runat="server" Text="功能組織："></asp:Label>
                            <asp:DropDownList ID="ddlRoleCode40" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlRoleCode40_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRoleCode30" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlRoleCode30_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRoleCode20" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlRoleCode20_SelectedIndexChanged" >
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRoleCode10" runat="server" AutoPostBack="true" >
                            </asp:DropDownList>
                            <%--<asp:DropDownList ID="ddlRoleCode0" runat="server" AutoPostBack="true" >
                            </asp:DropDownList>--%>
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow1">
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="員工編號："></asp:Label>
                            <asp:TextBox ID="txtEmpID" runat="server" MaxLength="6" AutoComplete="off"></asp:TextBox>
                        </td>
                        <td style="margin-left: 40px">
                            <asp:Label ID="Label6" runat="server" Text="員工姓名："></asp:Label>
                            <asp:TextBox ID="txtEmpName" runat="server" AutoComplete="off"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="任職狀況："></asp:Label>
                            <asp:DropDownList ID="ddlWorkStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow2">
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="職"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label21" runat="server" Text="等："></asp:Label>
                            <asp:DropDownList ID="ddlRankIDMIN" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlRankIDMIN_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label9" runat="server" Text="~"></asp:Label>
                            <asp:DropDownList ID="ddlRankIDMAX" runat="server"  AutoPostBack="true"
                                onselectedindexchanged="ddlRankIDMAX_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="職"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label22" runat="server" Text="稱："></asp:Label>
                            <asp:DropDownList ID="ddlTitleMIN" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlTitleMIN_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="Label25" runat="server" Text="~"></asp:Label>
                            <asp:DropDownList ID="ddlTitleMAX" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddlTitleMAX_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="職"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label23" runat="server" Text="位："></asp:Label>
                            <asp:DropDownList ID="ddlPosition" runat="server" >
                            </asp:DropDownList>
                            <asp:Label ID="Label20" runat="server" Text="(功能組織無職位)"></asp:Label>
                        </td>
                    </tr>
                    <asp:Panel ID="PanelDetail" runat="server">
                    <tr style="height:20px" class="Util_clsRow1">
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="申請表單狀態："></asp:Label>
                            <asp:DropDownList ID="ddlOTStatus" runat="server">
                                <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                <asp:ListItem Value="1" Text="暫存"></asp:ListItem>
                                <asp:ListItem Value="2" Text="送簽"></asp:ListItem>
                                <asp:ListItem Value="3" Text="核准"></asp:ListItem>
                                <asp:ListItem Value="4" Text="駁回"></asp:ListItem>
                                <asp:ListItem Value="5" Text="刪除"></asp:ListItem>
                                <asp:ListItem Value="9" Text="取消"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server" Text="申報表單狀態："></asp:Label>
                            <asp:DropDownList ID="ddlAfterOTStatus" runat="server">
                                <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
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
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="表單編號："></asp:Label>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFormNO" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtFormNO" runat="server" MaxLength="16" AutoComplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow2">
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="加班日期："></asp:Label>
                            <asp:ucDate ID="txtOTDateBegin" runat="server" AutoPostBack="true" />
                            <asp:Label ID="Label15" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="txtOTDateEnd" runat="server" AutoPostBack="true" />
                        </td>
                        <td>
                            <asp:Label ID="Label16" runat="server" Text="計薪年月："></asp:Label>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtOTPayDate" FilterType="Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="txtOTPayDate" runat="server" MaxLength="6" AutoComplete="off"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Text="(YYYYMM)"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    </asp:Panel>
                    <asp:Panel ID="PanelCount" runat="server">
                    <tr style="height:20px" class="Util_clsRow1">
                        <td>
                            <asp:Label ID="Label29" runat="server" Text="加班日期："></asp:Label>
                            <asp:ucDate ID="txtOTDateBeginCount" runat="server" />
                            <asp:Label ID="Label30" runat="server" Text="~"></asp:Label>
                            <asp:ucDate ID="txtOTDateEndCount" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td></td>
                    </tr>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
    </fieldset>
    <table style="width:100%">
        <asp:GridView ID="gvMain" runat="server" AllowPaging="False" 
            AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" 
            CellPadding="2"
            DataKeyNames="OTCompID,OTEmpID,OTDate,OTTime,OTTxnIDBefore,FlowCaseIDBefore,OTTxnIDAfter,FlowCaseIDAfter" PageSize="15" 
            HeaderStyle-ForeColor="dimgray" Visible="false" OnRowCreated="gd_RowCreated" onrowcommand="gvMain_RowCommand" onrowdatabound="gvMain_RowDataBound" Width="100%">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <Columns>
                <asp:TemplateField HeaderText="序號" >
                    <ItemTemplate>
                        <asp:Label ID="lblNumber" runat="server" 
                            Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <%--加班單預先申請--%>
                <asp:BoundField DataField="OTEmpID" HeaderText="員工編號">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTNameN" HeaderText="加班人">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>      
                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetail" runat="server" CausesValidation="false" CommandName="DetailA"
                            Text="明細" Enabled="false" ></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle CssClass="td_header" Width="4%" />
                    <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" HorizontalAlign="Center"/>
                </asp:TemplateField>                                
                <asp:BoundField DataField="OTDate" HeaderText="加班日期">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTTime" HeaderText="加班起迄時間">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTTypeID" HeaderText="加班類型">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTReasonMemo" HeaderText="加班原因">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTStatus" HeaderText="狀態">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <%--加班單事後申報--%>
                <asp:TemplateField HeaderText="明細" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="AfterbtnDetail" runat="server" CausesValidation="false" CommandName="DetailD"
                        Text="明細" Enabled="false" ></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="td_header" Width="4%" />
                <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" HorizontalAlign="Center"/>
            </asp:TemplateField>
                <asp:BoundField DataField="AfterOTDate" HeaderText="加班日期">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterOTTime" HeaderText="加班起迄時間">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterOTTypeID" HeaderText="加班類型">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterOTReasonMemo" HeaderText="加班原因">
                    <HeaderStyle Width="10%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterOTStatus" HeaderText="狀態">
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
        <asp:GridView ID="gvMain2" runat="server" AllowPaging="False" 
            AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" 
            CellPadding="2" Enabled="false"
            DataKeyNames="" PageSize="15" 
            HeaderStyle-ForeColor="dimgray" Visible="false" OnRowCreated="gd2_RowCreated" onrowdatabound="gvMain2_RowDataBound" Width="100%">
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <Columns>
                <asp:TemplateField HeaderText="序號" >
                    <ItemTemplate>
                        <asp:Label ID="lblNumber" runat="server" 
                            Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:BoundField DataField="OTEmpID" HeaderText="員工編號">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OTNameN" HeaderText="加班人">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>    
                <%--加班單預先申請--%>
                <asp:BoundField DataField="Submit" HeaderText="送簽">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Approval" HeaderText="核准">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Reject" HeaderText="駁回">
                    <HeaderStyle Width="7%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <%--加班單事後申報--%>
                <asp:BoundField DataField="AfterSubmit" HeaderText="送簽">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterApproval" HeaderText="核准">
                    <HeaderStyle Width="5%" CssClass="td_header" />
                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AfterReject" HeaderText="駁回">
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
    </table>
    <uc:ucModalPopup ID="ucModalPopup1" runat="server" />
    </form>
</body>
</html>
