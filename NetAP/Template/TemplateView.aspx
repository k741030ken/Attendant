<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TemplateView.aspx.cs" Inherits="Template_TemplateView" %>

<!DOCTYPE>

<html>
<head runat="server">
    <title>TemplateView</title>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset class="Util_Fieldset">
    <legend class="Util_Legend">Template</legend>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
            <td align="left">
                <table cellpadding="1" cellspacing="1" style="width: 100%">
                <tr style="height:20px">
                    <td colspan="3" align="left">
                        <asp:Button ID="btnQuery" runat="server" Text="查詢" onclick="btnQuery_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnAdd" runat="server" Text="新增" onclick="btnAdd_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="修改" onclick="btnEdit_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                        <asp:Button ID="btnDel" runat="server" Text="刪除" onclick="btnDel_Click" CssClass="Util_clsBtn" />&nbsp;&nbsp;
                    </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow1">
                        <td width="10%" align="left">
                        <asp:Label ID="lblOTCompID" runat="server" Text="公司：" ></asp:Label>
                        </td>
                        <td style="width:35%" align="left">                                
                            <asp:TextBox ID="txtOTCompID" runat="server" AutoComplete="off" Width="70px"  ></asp:TextBox>
                        </td>
                        <td width="10%" align="left">
                            <asp:Label ID="lblOTEmpID" runat="server" Text="員編：" ></asp:Label>
                        </td>
                        <td style="width:35%" align="left">     
                            <asp:TextBox ID="txtOTEmpID" runat="server" AutoComplete="off" Width="70px"  ></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height:20px" class="Util_clsRow2">
                        <td width="10%" align="left">
                            <asp:Label ID="lblNameN" runat="server" Text="姓名：" ></asp:Label>
                        </td>
                        <td style="width:35%" align="left">                                
                            <asp:TextBox ID="txtNameN" runat="server" AutoComplete="off" Width="70px"  ></asp:TextBox>
                        </td>
                        <td width="10%" align="left">
                            <asp:Label ID="lblSex" runat="server" Text="性別：" ></asp:Label>
                        </td>
                        <td width="35%" align="left">
                            <asp:DropDownList ID="ddlSex" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSexChanged" >
                            <asp:ListItem Value="" Text="請選擇"></asp:ListItem>
                            <asp:ListItem Value="1" Text="男"></asp:ListItem>
                            <asp:ListItem Value="2" Text="女"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        </tr>
                </table>
                </td>
                </tr>

    </table>
    </fieldset>
    <div style ="height:75%; width:100%; overflow:auto; ">
        <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" DataKeyNames="OTCompID, OTEmpID, Sex, NameN" AllowPaging="false" 
    onrowdatabound="gvMain_RowDataBound" onrowcommand="gvMain_RowCommand" Width="100%">
        <Columns>       
            <asp:TemplateField HeaderText="序號" >
                <ItemTemplate>
                    <asp:Label ID="lblNumber" runat="server" 
                        Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="4%" Height="15px" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center"/>
            </asp:TemplateField>
            <asp:BoundField HeaderText="公司" DataField="OTCompID">
                <HeaderStyle Width="80px" Height="15px"/>
                <ItemStyle HorizontalAlign="Center" Width="25px"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="加班人員編" DataField="OTEmpID">
                <HeaderStyle Width="80px" Height="15px"/>
                <ItemStyle HorizontalAlign="Center" Width="25px"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="加班人員編+姓名" DataField="ShowOTEmp">
                <HeaderStyle Width="" CssClass="td_header" />
                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="性別" DataField="ShowSex">
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
    </form>
</body>
</html>
