<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATWF00.aspx.vb" Inherits="ATWF_ATWF00" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>流程引擎設定(查詢)</title>
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
            griditem.value = "";
            var rows = document.getElementById("gvMain").rows;
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                if ((parseInt(i) + 1) % 2 == 0)
                    row.style.backgroundColor = 'white'
                else
                    row.style.backgroundColor = '#e2e9fe';

            }
        }
-->
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label13" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                                <%--<asp:Button ID="btnDeleteInvisible" runat="server" Text="刪除" CssClass="Util_clsBtn" style="display:none;" onclick="btnDeleteInvisible_Click" />--%>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblCompID" runat="server" CssClass="InputTextStyle_Thin" Width="200px" Font-Names="微軟正黑體"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                                <asp:Label ID="lblReleaseResult" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label14" runat="server" Text="系統代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlSystemID" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label1" runat="server" Text="流程代碼："></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtFlowCode" runat="server" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label2" runat="server" Text="流程名稱：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtFlowName" runat="server" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label3" runat="server" Text="流程識別碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtFlowSN" runat="server" MaxLength="12" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label4" runat="server" Text="關卡序號：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:FilteredTextBoxExtender ID="fttxtFlowSeq" runat="server" TargetControlID="txtFlowSeq" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtFlowSeq" runat="server" Width="30px" MaxLength="2" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtFlowSeqName" runat="server" Width="220px" MaxLength="30" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label5" runat="server" Text="流程起點註記：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlFlowStartFlag" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-有" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="0-無" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label6" runat="server" Text="流程終點註記：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlFlowEndFlag" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-有" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="0-無" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
							
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label7" runat="server" Text="無效註記：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlInValidFlag" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-有" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="0-無" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label8" runat="server" Text="流程動作：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlFlowAct" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-正常" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2-跳過" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label12" runat="server" Text="隱藏流程註記：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlVisableFlag" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-有" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="0-無" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>         
                            <td align="left" width="15%" >
                                <asp:Label ID="Label95" runat="server" Text="是否生效：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlStatus" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-是" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="0-否" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>							
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label9" runat="server" Text="簽核線定義：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlSignLineDefine" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-依行政組織" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2-依功能組織" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3-依特定人員" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label10" runat="server" Text="簽核者定義：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:DropDownList ID="ddlSingIDDefine" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-組織主管" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2-特定人員" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr id="trSpec" runat="server" visible="false">
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="Label11" runat="server" Text="特定人員：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="65%" colspan="3">
                                <asp:DropDownList ID="ddlSpeComp" runat="server" AutoPostBack="True" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                                <asp:FilteredTextBoxExtender ID="fttxtSpeEmpID" runat="server" TargetControlID="txtSpeEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                                <asp:TextBox ID="txtSpeEmpID" runat="server" MaxLength="6" AutoPostBack="true" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                                <asp:Label ID="lblSpeEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucQuerySpeEmpID" runat="server" WindowHeight="800" WindowWidth="600"
                                    ButtonText="..." />
                            </td>
                            <td width="10%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width: 100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                            </td>
                        </tr> 
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,SystemID,FlowCode,FlowName,FlowSN,FlowSeq,FlowSeqName,FlowStartFlag,FlowEndFlag,SignLineDefine,SingIDDefine,SpeComp,SpeEmpID,InValidFlag,VisableFlag,FlowAct,LastChgComp,LastChgID,LastChgDate"
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
                            <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                        Text="明細"></asp:LinkButton>
                                </ItemTemplate>
                            <HeaderStyle CssClass="td_header" Width="2%" />
                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序號">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="FlowCode" HeaderText="流程代碼" ReadOnly="True" SortExpression="FlowCode">
                            <HeaderStyle Width="3%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowName" HeaderText="流程名稱" HtmlEncode="false" ReadOnly="True"
                            SortExpression="FlowName">
                            <HeaderStyle Width="8%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowSN" HeaderText="流程識別碼" ReadOnly="True" SortExpression="FlowSN">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowSeq" HeaderText="關卡序號" ReadOnly="True" SortExpression="FlowSeq">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowStartFlagShow" HeaderText="流程起點註記" ReadOnly="True" SortExpression="FlowStartFlag">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowEndFlagShow" HeaderText="流程終點註記" ReadOnly="True" SortExpression="FlowEndFlag">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SignLineDefine" HeaderText="簽核線定義" ReadOnly="True" SortExpression="SignLineDefine">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SingIDDefine" HeaderText="簽核者定義" ReadOnly="True" SortExpression="SingIDDefine">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpeCompName" HeaderText="特定人員公司" ReadOnly="True" SortExpression="SpeCompName">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpeEmpName" HeaderText="特定人員員編" ReadOnly="True" SortExpression="SpeEmpName" HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="StatusShow" HeaderText="是否生效" ReadOnly="True"  HtmlEncode="False" SortExpression="StatusShow">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後異動人員公司" ReadOnly="True" 
                            SortExpression="LastChgCompName" DataField="LastChgCompName" HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後異動人員" ReadOnly="True" 
                            SortExpression="LastChgName" DataField="LastChgName" HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                            <asp:BoundField HeaderText="最後異動時間" ReadOnly="True" 
                            SortExpression="LastChgDate" DataField="LastChgDate">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                    </Columns>
                   <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
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
