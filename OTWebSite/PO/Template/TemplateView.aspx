<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplateView.aspx.vb" Inherits="PO_Template_TemplateView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html>
<head id="Head1" runat="server">
    <title>TemplateView</title>
    <link type="text/css" rel="stylesheet" href="../../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
//            switch (Param) {
//                case "btnDelete":
//                    if (!hasSelectedRows('')) {
//                        alert("未選取資料列！");
//                        return false;
//                    }
//            }
//            switch (Param) {
//                case "btnDelete":
//                    if (!confirm('確定刪除此筆資料？'))
//                        return false;
//                    break;
//            }
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
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="lblOTCompID" runat="server" Text="公司：" Font-Names="微軟正黑體"></asp:Label>
                                <%--<asp:Button ID="btnDeleteInvisible" runat="server" Text="刪除" CssClass="Util_clsBtn" style="display:none;" onclick="btnDeleteInvisible_Click" />--%>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtOTCompID" runat="server" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td width="50%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="lblOTEmpID" runat="server" Text="員編："></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtOTEmpID" runat="server" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td width="50%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="lblNameN" runat="server" Text="姓名：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
                                <asp:TextBox ID="txtNameN" runat="server" AutoComplete="off" Font-Names="微軟正黑體"></asp:TextBox>
                            </td>
                            <td width="50%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%" >
                                <asp:Label ID="lblSex" runat="server" Text="性別：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%" >
								<asp:DropDownList ID="ddlSex" runat="server" Font-Names="微軟正黑體" AutoPostBack="true" onselectedindexchanged="ddlSexChanged">
                                    <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                                    <asp:ListItem Text="1-男" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2-女" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="50%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        <tr>
            <td align="center" width="100%">
                <table width="100%" class="tbl_Content">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames=""
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"
                                                Font-Names="微軟正黑體"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" Height="15px" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OTCompID" HeaderText="公司">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTEmpID" HeaderText="加班人員編">
                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ShowOTEmp" HeaderText="加班人員編+姓名">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ShowSex" HeaderText="性別">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NameN" HeaderText="加班人" ReadOnly="True" SortExpression="NameN">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
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
