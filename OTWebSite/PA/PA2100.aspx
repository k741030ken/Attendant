<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PA2100.aspx.vb" Inherits="PA_PA2100" %>

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
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblWTID" Font-Size="15px" runat="server" Text="班別代碼："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlWTID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblWorkTime" Font-Size="15px" runat="server" Text="上班時間："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtBeginTimeStart" runat="server" Width="50px" MaxLength="4"></asp:TextBox>~
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtBeginTimeEnd" runat="server" Width="50px" MaxLength="4"></asp:TextBox> (格式：mmss)
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblRestTime" Font-Size="15px" runat="server" Text="休息開始時間："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRestBeginTimeStart" runat="server" Width="50px" MaxLength="4"></asp:TextBox>~
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRestBeginTimeEnd" runat="server" Width="50px" MaxLength="4"></asp:TextBox> (格式：mmss)
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="Label1" Font-Size="15px" runat="server" Text="下班時間："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEndTimeStart" runat="server" Width="50px" MaxLength="4"></asp:TextBox>~
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEndTimeEnd" runat="server" Width="50px" MaxLength="4"></asp:TextBox> (格式：mmss)
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="Label2" Font-Size="15px" runat="server" Text="休息結束時間："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRestEndTimeStart" runat="server" Width="50px" MaxLength="4"></asp:TextBox>~
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRestEndTimeEnd" runat="server" Width="50px" MaxLength="4"></asp:TextBox> (格式：mmss)
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblAcrossFlag" Font-Size="15px" runat="server" Text="跨日註記："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlAcrossFlag" AutoPostBack="true" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非跨日"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-跨日"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblInValidFlag" Font-Size="15px" runat="server" Text="無效註記："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlInValidFlag" AutoPostBack="true" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---" Selected="true"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-有效"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-無效"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblWTIDTypeFlag" Font-Size="15px" runat="server" Text="班別類型："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlWTIDTypeFlag" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblRemark" Font-Size="15px" runat="server" Text="班別說明："></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <asp:DropDownList ID="ddlRemark" runat="server" Font-Names="細明體"></asp:DropDownList>
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
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,WTID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="3%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WTID" HeaderText="班別代碼" ReadOnly="True" SortExpression="WTID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BeginTime" HeaderText="上班時間" ReadOnly="True" SortExpression="BeginTime" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EndTime" HeaderText="下班時間" ReadOnly="True" SortExpression="EndTime" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RestBeginTime" HeaderText="休息開始時間" ReadOnly="True" SortExpression="RestBeginTime" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RestEndTime" HeaderText="休息結束時間" ReadOnly="True" SortExpression="RestEndTime" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="跨日註記" SortExpression="AcrossFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgAcrossFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "AcrossFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgAcrossFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "AcrossFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="無效註記" SortExpression="InValidFlag">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgInValidFlag" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","True","False") %>' />
                                                <asp:Image ID="imgInValidFlag1" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "InValidFlag")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>     
                                        <asp:BoundField DataField="WTIDTypeFlag" HeaderText="班別類型" ReadOnly="True" SortExpression="WTIDTypeFlag" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Remark" HeaderText="班別說明" ReadOnly="True" SortExpression="Remark" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>                                              
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
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
