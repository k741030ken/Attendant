<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV8001.aspx.vb" Inherits="OV_OV8001" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>流程引擎設定(新增)</title>
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
            height:14px;
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
<script type="text/javascript">
    function ContinueCheck() {
        var message = document.getElementById("msg").value
        if (!confirm(message))
            return false;
        document.getElementById("btnContinue").click();
    }
    function Continue2Check() {
        var message = document.getElementById("msg").value
        if (!confirm(message))
            return false;
        document.getElementById("btnContinue2").click();
    }
</script>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%"  class="tbl_Condition">
        <tr>
            <td width="10%" class="style1" >
                <asp:Label ID="Label13" runat="server" Text="*公司代碼：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label14" runat="server" Text="*系統代碼：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:DropDownList ID="ddlSystemID" OnSelectedIndexChanged = "ddlSystemID_Changed" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1" >
                <asp:Label ID="Label1" runat="server" Text="*流程代碼：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:DropDownList ID="ddlFlowCode" OnSelectedIndexChanged = "ddlFlowCode_Changed" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label2" runat="server" Text="*流程名稱：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:TextBox ID="txtFlowName" runat="server" MaxLength="30" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label3" runat="server" Text="*流程識別碼：" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:TextBox ID="txtFlowSN"  OnTextChanged = "txtFlowSN_Changed" runat="server" MaxLength="12" AutoPostBack="true" Font-Names="微軟正黑體"></asp:TextBox>
            </td>
            <td width="10%" class="style1" >
                <asp:Label ID="Label4" runat="server" Text="*關卡序號：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:FilteredTextBoxExtender ID="fttxtFlowSeq" runat="server" TargetControlID="txtFlowSeq" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                <asp:TextBox ID="txtFlowSeq" runat="server" Width="30px" MaxLength="2" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtFlowSeqName" runat="server" Width="200px" MaxLength="30" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label5" runat="server" Text="流程起點註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:CheckBox ID="chkFlowStartFlag" runat="server" />
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label6" runat="server" Text="流程終點註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:CheckBox ID="chkFlowEndFlag" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label7" runat="server" Text="無效註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:CheckBox ID="chkInValidFlag" runat="server" />
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label8" runat="server" Text="*流程動作：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:DropDownList ID="ddlFlowAct" runat="server" Font-Names="微軟正黑體">
                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                    <asp:ListItem Text="1-正常" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2-跳過" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label12" runat="server" Text="隱藏流程註記：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:CheckBox ID="chkVisableFlag" runat="server" />
            </td>
        </tr>
        <tr>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label9" runat="server" Text="*簽核線定義：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:DropDownList ID="ddlSignLineDefine" runat="server" Font-Names="微軟正黑體">
                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                    <asp:ListItem Text="1-依行政組織" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2-依功能組織" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3-依特定人員" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="10%"  class="style1" >
                <asp:Label ID="Label10" runat="server" Text="*簽核者定義：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:DropDownList ID="ddlSingIDDefine" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                    <asp:ListItem Text="1-組織主管" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2-特定人員" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trSpec" runat="server" visible="false">
            <td width="10%"  class="style1" >
                <asp:Label ID="Label11" runat="server" Text="*特定人員：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" colspan="3">
                <asp:DropDownList ID="ddlSpeComp" runat="server" AutoPostBack="True" Font-Names="微軟正黑體">
                </asp:DropDownList>
                <asp:FilteredTextBoxExtender ID="fttxtSpeEmpID" runat="server" TargetControlID="txtSpeEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <asp:TextBox ID="txtSpeEmpID" runat="server" MaxLength="6" AutoPostBack="true" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                <asp:Label ID="lblSpeEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <uc:ButtonQuerySelectUserID ID="ucQuerySpeEmpID" runat="server" WindowHeight="800" WindowWidth="600"
                    ButtonText="..." />
            </td>
        </tr>
    </table>
    <table id="Table1" width="100%" height="auto" class="tbl_Content" runat="server">
        <tr>
            <td style="width:100%">
                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" CellPadding="2" DataKeyNames="FlowCode,FlowName,FlowSN"
                        Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" OnRowCreated="gvMain_RowCreated" >
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <Columns>
                        <asp:TemplateField HeaderText="序號">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="FlowCode" HeaderText="流程代碼" ReadOnly="True">
                            <HeaderStyle Width="3%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowName" HeaderText="流程名稱" HtmlEncode="false" ReadOnly="True">
                            <HeaderStyle Width="8%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowSN" HeaderText="流程識別碼" ReadOnly="True">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FlowSeq" HeaderText="關卡序號" ReadOnly="True" >
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShowFlowStartFlag" HeaderText="流程起點註記" ReadOnly="True">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShowFlowEndFlag" HeaderText="流程終點註記" ReadOnly="True">
                            <HeaderStyle Width="2%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShowSignLineDefine" HeaderText="簽核線定義" ReadOnly="True">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShowSingIDDefine" HeaderText="簽核者定義" ReadOnly="True">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpeComp" HeaderText="特定人員公司" ReadOnly="True">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SpeEmpID" HeaderText="特定人員員編" ReadOnly="True"  HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                         <asp:BoundField DataField="ShowStatus" HeaderText="是否生效" ReadOnly="True"  HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後異動人員公司" ReadOnly="True" 
                            DataField="LastChgComp" HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後異動人員" ReadOnly="True" 
                            DataField="LastChgID" HtmlEncode="False">
                            <HeaderStyle Width="5%" CssClass="td_header" Font-Names="微軟正黑體" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                        </asp:BoundField>
                            <asp:BoundField HeaderText="最後異動時間" ReadOnly="True" 
                             DataField="ShowLastChgDate">
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
    <asp:Button ID="btnContinue" runat="server" Text="btnContinueSave" Style="display: none"/>
    <asp:Button ID="btnContinue2" runat="server" Text="btnContinueSave" Style="display: none"/>
    <asp:TextBox ID="msg" runat="server" Style="display: none" ></asp:TextBox>
    </form>
</body>
</html>

