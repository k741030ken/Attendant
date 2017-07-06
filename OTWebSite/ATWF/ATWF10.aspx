<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATWF10.aspx.vb" Inherits="ATWF_ATWF10" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>流程適用人員設定(查詢)</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) 
        {
            switch (Param) {
                case "btnDelete":
                    if (!hasSelectedRows('')) {
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
        function gridClear() {
            var griditem = document.getElementById("__SelectedRowsgvMain");
            griditem.value = ""
            var rows = document.getElementById("gvMain").rows;
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                if ((parseInt(i) + 1) % 2 == 0)
                    row.style.backgroundColor = 'white'
                else
                    row.style.backgroundColor = '#e2e9fe';

            }
        }
    </script> 
    <style type="text/css">
        .GridViewStyle
        {
            font-size: 15px;
            background-color: white;
            border-color: #3366CC;
            border-style: none;
            border-width: 1px;
        }
        .td_header
        {
            text-align: center;
            background-color: #89b3f5;
            color: dimgray;
            font-size: 14px;
            border-width: 1px;
            border-style: solid;
            border-color: #5384e6;
            font-weight: normal;
        }
        .tr_evenline
        {
            background-color: White;
        }
        .td_detail
        {
            border-width: 1px;
            border-style: solid;
            border-color: #89b3f5;
            font-size: 14px;
            font-family: Calibri, 新細明體;
            height: 16px;
        }
        .tr_oddline
        {
            background-color: #e2e9fe;
        }
        .buttonface
        {
            border: 1px solid gray;
            background-color:Silver;
            text-justify: distribute-all-lines;
            font-size: 14px;
            background-image: url('../images/bgSilverX.gif');
            cursor: hand;
            color: black; 
            padding-top: 3px;
            background-repeat: repeat-x;
            font-family: Calibri, 新細明體, 'Courier New' , 細明體; 
            letter-spacing: 1px;
            border-collapse: collapse;
            text-align: center;
        }
        .style1
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width:110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width:110px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                <asp:UpdatePanel ID="UpdMain" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label13" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblCompID" runat="server" CssClass="InputTextStyle_Thin" Width="200px" Font-Names="微軟正黑體"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" 
                                    style="display:none" />
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td width="15%" >
                                <asp:Label ID="Label6" runat="server" Text="系統代碼："></asp:Label>
                            </td>
                            <td width="25%" >
                                <asp:DropDownList ID="ddlSystemCode" runat="server" Font-Names="微軟正黑體">
                                
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label1" runat="server" Text="流程代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlFlowCode" runat="server" Font-Names="微軟正黑體" AutoPostBack="true" OnSelectedIndexChanged = "ddlFlowCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label2" runat="server" Text="流程識別碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlFlowSN" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label3" runat="server" Text="適用公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlFlowCompID" runat="server" AutoPostBack="True" Enabled="false" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label4" runat="server" Text="適用員工編號：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtEmpID" runat="server" MaxLength="6" AutoComplete="off" Font-Names="微軟正黑體" AutoPostBack ="true" OnTextChanged = "txtEmpID_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblEmpID" runat="server"  Font-Names="微軟正黑體"></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucQueryEmpID" runat="server" WindowHeight="800" WindowWidth="600"  ButtonText="..." />
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label9" runat="server" Text="適用處/部門：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlDeptID" runat="server" Font-Names="微軟正黑體" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label12" runat="server" Text="適用科組課：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label7" runat="server" Text="適用職等：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:Label ID="lblRankText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlRankB" runat="server" AutoPostBack="true" Width="50px" Font-Names="微軟正黑體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                                <asp:Label ID="lblRankText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="lblRankText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlRankE" runat="server" AutoPostBack="true" Width="50px" Font-Names="微軟正黑體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblRankNotice" Font-Size="12px" runat="server" Text="職等選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label8" runat="server" Text="適用職稱：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:Label ID="ddlTitleText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlTitleB" runat="server" Width="120px" Font-Names="微軟正黑體"></asp:DropDownList>
                                <asp:Label ID="ddlTitleText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="ddlTitleText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlTitleE" runat="server" Width="120px" Font-Names="微軟正黑體"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblTitleNotice" Font-Size="12px" runat="server" Text="職稱選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>

                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label5" runat="server" Text="適用職位：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlPositionID" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label10" runat="server" Text="適用工作性質：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlWorkTypeID" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr id="BusinessType">
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="lblBusinessType" runat="server" Text="適用業務類別：" Visible="false" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlBusinessType" runat="server" Visible="false" Font-Names="微軟正黑體" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" align="left" >
                                <asp:Label ID="lblEmpFlowRemark" runat="server" Visible="false" Text="適用功能備註：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" >
                                <asp:DropDownList ID="ddlEmpFlowRemark" runat="server" Visible="false" Font-Names="微軟正黑體"></asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td width="15%" align="left" >
                                <asp:Label ID="Label14" runat="server" Text="是否設為主要流程：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="25%" align="left" colspan="3" >
                                <asp:CheckBox ID="chkInValidFlag" runat="server" />
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCompID" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDeptID" />
                        <asp:AsyncPostBackTrigger ControlID="ddlFlowCode" />
                        <asp:AsyncPostBackTrigger ControlID="ddlRankB" />
                        <asp:AsyncPostBackTrigger ControlID="ddlRankE" />
                        <asp:AsyncPostBackTrigger ControlID="ddlBusinessType" />
                        
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content" id="ShowTable" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="FlowCode, SystemID,FlowSN ,CompID ,EmpID ,DeptID ,OrganID ,BusinessType ,RankID ,TitleID ,TitleIDTop ,TitleIDBottom ,PositionID ,EmpFlowRemark ,PrincipalFlag ,InValidFlag ,WorkTypeID,LastChgComp,LastChgID,LastChgDate "
                            CssClass="GridViewStyle" CellPadding="2" 
                            Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemStyle CssClass="td_detail" Height="15px" />
                                    <HeaderStyle CssClass="td_header" Width="2%" />
                                    <HeaderTemplate>
                                        <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" HeaderText="全選"
                                            runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_gvMain" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="序號">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FlowCode" HeaderText="流程代碼" ReadOnly="True" SortExpression="FlowCode">
                                    <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FlowSN" HeaderText="流程識別碼" ReadOnly="True" SortExpression="FlowSN">
                                    <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompName" HeaderText="適用公司" ReadOnly="True" SortExpression="CompName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NameN" HeaderText="適用員工編號" ReadOnly="True" SortExpression="NameN" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DeptName" HeaderText="適用處/部門" ReadOnly="True" SortExpression="DeptName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrganName" HeaderText="適用科組課" ReadOnly="True" SortExpression="OrganName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                
                                <asp:BoundField DataField="RankID" HeaderText="適用職等" ReadOnly="True" SortExpression="RankID">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TitleName" HeaderText="適用職稱" ReadOnly="True" SortExpression="TitleName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="PositionName" HeaderText="適用職位" ReadOnly="True" SortExpression="PositionName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="WorkTypeName" HeaderText="適用工作性質" ReadOnly="True" SortExpression="WorkTypeName" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BusinessTypeName" HeaderText="適用業務類別" ReadOnly="True" SortExpression="BusinessType" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmpFlowRemarkName" HeaderText="適用功能備註" ReadOnly="True" SortExpression="EmpFlowRemark" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="PrincipalFlag" HeaderText="是否為主要流程" ReadOnly="True" SortExpression="PrincipalFlag" HtmlEncode="false">
                                    <HeaderStyle Width="4%" CssClass="td_header" Font-Names="微軟正黑體" />
                                    <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="是否為主要流程" SortExpression="PrincipalFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemTemplate>
                                                <asp:Image id="imgRightA" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# Iif(Databinder.Eval(Container.DataItem, "PrincipalFlag")="1","True","False") %>' />
                                                <asp:Image id="imgRightA_E" runat="server" ImageUrl="~/images/chkboxE.gif" Visible='<%# Iif(Databinder.Eval(Container.DataItem, "PrincipalFlag")="0","True","False") %>' />
                                            </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="tr_evenline" />
                            <AlternatingRowStyle CssClass="tr_oddline" />
                            <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                            <HeaderStyle ForeColor="DimGray"></HeaderStyle>
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
