<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ATWF12.aspx.vb" Inherits="ATWF_ATWF12" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>流程適用人員設定(修改)</title>
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
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">

        function BossCheck() {
            var message = document.getElementById("msg").value
            if (!confirm(message))
                return false;
            document.getElementById("btnBoss").click();
        }
        function mainFlagCheck() {
            var message = document.getElementById("checkMsg").value
            if (!confirm(message))
                return false;
            document.getElementById("btnMessage").click();
        }
//        function ContinueCheck() {
//            var message = document.getElementById("msg").value
//            if (!confirm(message))
//                return false;
//            document.getElementById("btnContinue").click();
//        }
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                    if (!confirm('確定要修改？'))
                        return false;
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="td_detail">
        <tr>
            <td width="10%" class="style1" >
                <asp:Label ID="Label13" runat="server" Text="*公司代碼" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:Label ID="lblCompID" runat="server" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="10%"  class="style1" >
            <asp:Label ID="Label9" runat="server" Text="*系統代碼" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2" >
                                <asp:DropDownList ID="ddlSystemCode" runat="server" Font-Names="微軟正黑體">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="OT" Text="OT"></asp:ListItem>
                                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label1" runat="server" Text="*流程代碼" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlFlowCode" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label2" runat="server" Text="*流程識別碼" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlFlowSN" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label3" runat="server" Text="適用公司代碼" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlFlowCompID" runat="server" AutoPostBack="True" Enabled="false" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label4" runat="server" Text="適用員工編號" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:FilteredTextBoxExtender ID="fttxtEmpID" runat="server" TargetControlID="txtEmpID" FilterType="Numbers ,UppercaseLetters" ></asp:FilteredTextBoxExtender>
                <%--<asp:TextBox ID="txtEmpID" runat="server" MaxLength="6" AutoPostBack="True" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>--%>
                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" runat="server" Width="80px" AutoPostBack="true" MaxLength="6" style="TEXT-TRANSFORM:uppercase"></asp:TextBox>
                <asp:Label ID="lblEmpID" runat="server" Font-Names="微軟正黑體"></asp:Label>
                <%--<asp:Button ID="ValidDateB" runat="server" Style="display: none" />--%>
                <uc:ButtonQuerySelectUserID ID="ucQueryEmpID" runat="server" WindowHeight="800" WindowWidth="600"
                    ButtonText="..." />
            </td>
        </tr>
        <tr>
            <td width="10%" align="left" class="style1">
                <asp:Label ID="Label5" runat="server" Text="適用處/部門" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlDeptID" runat="server" Font-Names="微軟正黑體" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label12" runat="server" Text="適用科組課" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label7" runat="server" Text="適用職等" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:Label ID="lblRankText1" runat="server" Text=">="></asp:Label>
                <asp:DropDownList ID="ddlRankB" runat="server" AutoPostBack="true" Font-Names="微軟正黑體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                <asp:Label ID="lblRankText2" runat="server" Text="～"></asp:Label>
                <asp:Label ID="lblRankText3" runat="server" Text="<="></asp:Label>
                <asp:DropDownList ID="ddlRankE" runat="server" AutoPostBack="true"  Font-Names="微軟正黑體"></asp:DropDownList>
                <br />
                <asp:Label ID="lblRankNotice" Font-Size="12px" runat="server" Text="職等選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
            </td>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label8" runat="server" Text="適用職稱" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:Label ID="ddlTitleText1" runat="server" Text=">="></asp:Label>
                <asp:DropDownList ID="ddlTitleB" runat="server" Width="120px" Font-Names="微軟正黑體"></asp:DropDownList>
                <asp:Label ID="ddlTitleText2" runat="server" Text="～"></asp:Label>
                <asp:Label ID="ddlTitleText3" runat="server" Text="<="></asp:Label>
                <asp:DropDownList ID="ddlTitleE" runat="server" Width="120px" Font-Names="微軟正黑體"></asp:DropDownList>
                <br />
                <asp:Label ID="lblTitleNotice" Font-Size="12px" runat="server" Text="職稱選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label6" runat="server" Text="適用職位" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlPositionID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label10" runat="server" Text="適用工作性質" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlWorkTypeID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="BusinessType" runat="server" Visible="false">
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="lblBusinessType" runat="server" Text="適用業務類別" Visible="false" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlBusinessType" runat="server" Visible="false" Font-Names="微軟正黑體" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="lblEmpFlowRemark" runat="server" Visible="false" Text="適用功能備註" Font-Names="微軟正黑體"></asp:Label>
                <%--<asp:Label ID="Label11" runat="server" Text="無效註記" Font-Names="微軟正黑體"></asp:Label>--%>
            </td>
            <td width="40%" align="left" class="style2" >
                <asp:DropDownList ID="ddlEmpFlowRemark" Visible="false" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" align="left" class="style1" >
                <asp:Label ID="Label14" runat="server" Text="是否設為主要流程" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" align="left" class="style2" >
                <%--<asp:DropDownList ID="DropDownList3" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>--%>
                <asp:CheckBox ID="chkInValidFlag" runat="server" />
            </td>
            <td width="10%" align="left" class="style1" >
            </td>
            <td width="40%" align="left" class="style2" >
            <asp:Button ID="btnContinue" runat="server" Text="btnContinueSave" Style="display: none"/>
            <asp:Button ID="btnBoss" runat="server" Text="btnBossSave" Style="display: none"/>
            <asp:Button ID="btnMessage" runat="server" Style="display: none"/>
            <asp:TextBox ID="msg" runat="server" Style="display: none" ></asp:TextBox>
            <asp:TextBox ID="checkMsg" runat="server" Style="display: none" ></asp:TextBox>
            </td>
        </tr>
        <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgComp1" runat="server" Text="最後異動公司"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgComp" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgID1" runat="server" Text="最後異動人員"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgID" runat="server" > </asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="style1" width="15%">
                                            <asp:Label ID="lblLastChgDate1" runat="server" Text="最後異動日期"></asp:Label>
                                        </td>
                                        <td class="style2" style="width:35%" align="left" colspan="3">
                                            <asp:Label ID="lblLastChgDate" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
        <%--<tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label5" runat="server" Text="適用單位代碼：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label10" runat="server" Text="適用工作性質：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlWorkTypeID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" class="style1">
                <asp:Label ID="Label7" runat="server" Text="適用職等：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlRankID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label8" runat="server" Text="適用職位：" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlPositionID" runat="server" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="BusinessType">
            <td width="10%" class="style1">
                <asp:Label ID="lblBusinessType" runat="server" Text="適用業務類別：" Visible="false" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:DropDownList ID="ddlBusinessType" runat="server" Visible="false" Font-Names="微軟正黑體">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="Label6" runat="server" Text="無效註記" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td width="40%" class="style2">
                <asp:CheckBox ID="chkInValidFlag" runat="server" />
            </td>
        </tr>--%>

    </table>
    </form>
</body>
</html>