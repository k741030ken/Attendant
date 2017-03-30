<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA1200.aspx.vb" Inherits="PA_PA1200" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
            }
            
            switch(Param)
            {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }
        
        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                try
                {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch(ex)
                {}
            }
        }
       -->
    </script>
    <style type="text/css">
        .style1
        {
            width: 101px;
        }
        .style2
        {
            width: 124px;
        }
        .style3
        {
            width: 133px;
        }
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%"">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" colspan="2" width="20%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompName" Font-Size="15px" runat="server" Text="公司名稱："></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtCompName" runat="server"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompEngID" font-size="15px" runat="server" Text="英文名稱："></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtCompEngID" runat="server"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompChID" font-size="15px" runat="server" Text="中文名稱："></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtCompChID" runat="server"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblInValidFlag" font-size="15px" runat="server" Text="無效註記："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlInValidFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-有效"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-無效"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left"  width="10%">
                                <asp:Label ID="lblINotShowFlag" font-size="15px" runat="server" Text="不顯示註記："></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList ID="ddlNotShowFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-顯示"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-不顯示"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblHRISFlag" font-size="15px" runat="server" Text="資料轉入HRISDB："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlHRISFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-不轉"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-轉"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblRankIDMapFlag" font-size="15px" runat="server" Text="導入惠悅："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRankIDMapFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-無"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-有"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblNotShowRankID" font-size="15px" runat="server" Text="不顯示職等："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlNotShowRankID" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-顯示"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-不顯示"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblNotShowWorkType" font-size="15px" runat="server" Text="不顯示工作性質："></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList ID="ddlNotShowWorkType" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-顯示"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-不顯示"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblFeeShareFlag" font-size="15px" runat="server" Text="費用分攤註記："></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList ID="ddlFeeShareFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-是"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblSPHSC1GrpFlag" font-size="15px" runat="server" Text="證券團保公司註記："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSPHSC1GrpFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-是"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="3%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpSource" font-size="15px" runat="server" Text="員工資料來源："></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpSource" runat="server" Font-Size="12px" Width="115px"></asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCNFlag" font-size="15px" runat="server" Text="簡體註記："></asp:Label>
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList ID="ddlCNFlag" runat="server" Font-Size="12px" Width="115px">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-繁體"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-簡體"></asp:ListItem>
                                </asp:DropDownList>
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
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderStyle Width="1%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <HeaderStyle Width="2%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="無效註記" SortExpression="InValidFlag">
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsInValidFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsInValidFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="不顯示註記" SortExpression="NotShowFlag">
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsNotShowFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsNotShowFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="費用分攤註記" SortExpression="FeeShareFlag">
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsFeeShareFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "FeeShareFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsFeeShareFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "FeeShareFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="證券團保公司註記" SortExpression="SPHSC1GrpFlag">
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsSPHSC1GrpFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "SPHSC1GrpFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsSPHSC1GrpFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "SPHSC1GrpFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField><asp:TemplateField HeaderText="導入惠悅註記" SortExpression="RankIDMapFlag">
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsRankIDMapFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "RankIDMapFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsRankIDMapFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "RankIDMapFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RankIDMapValidDate" HeaderText="導入惠悅生效日" ReadOnly="True" SortExpression="RankIDMapValidDate" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="不顯示職等註記" SortExpression="NotShowRankID">
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsNotShowRankID" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowRankID")="1","True","False") %>' />
                                                <asp:Image ID="imgIsNotShowRankID1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowRankID")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="不顯示工作性質註記" SortExpression="NotShowWorkType">
                                            <HeaderStyle Width="6%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsNotShowWorkType" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowWorkType")="1","True","False") %>' />
                                                <asp:Image ID="imgIsNotShowWorkType1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "NotShowWorkType")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="轉入HRISDB註記" SortExpression="HRISFlag">
                                            <HeaderStyle Width="6%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsHRISFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "HRISFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsHRISFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "HRISFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="簡體註記" SortExpression="CNFlag">
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsCNFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "CNFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgIsCNFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "CNFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Payroll" HeaderText="計薪作業歸屬體系" ReadOnly="True" SortExpression="Payroll" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Calendar" HeaderText="年曆檔歸屬體系" ReadOnly="True" SortExpression="Calendar" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CheckInFile" HeaderText="報到文件歸屬體系" ReadOnly="True" SortExpression="CheckInFile" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpSource" HeaderText="員工資料來源" ReadOnly="True" SortExpression="EmpSource" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
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
