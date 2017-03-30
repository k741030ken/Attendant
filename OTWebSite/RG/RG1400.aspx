<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RG1400.aspx.vb" Inherits="RG_RG1400" %>

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
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpDay" Font-Size="15px" runat="server" Text="報到日迄今："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlEmpDay" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="4" Text="全部"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="3~7天"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="7~13天"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="14天以上"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%">
				                <asp:Label ID="lblStatus" Font-Size="15px" runat="server" Text="文件繳交狀態："></asp:Label>
			                </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="0" Text="全部"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="未繳齊"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="已繳齊"></asp:ListItem>
                                </asp:DropDownList>
			                </td>
                            <td width="5%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                            </td>
                            <td align="left" width="10%">
                                <asp:Label ID="lblName" Font-Size="15px" runat="server" Text="員工姓名："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" width="10%"></td>
                            <td align="left" width="20%"></td>
                            <td width="5%"></td>
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
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,EmpID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <HeaderStyle CssClass="td_header" Width="2%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Height="15px" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Size="12px" Width="3%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="員工姓名" ReadOnly="True" SortExpression="Name" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganName" HeaderText="部門" ReadOnly="True" SortExpression="OrganName" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpDate" HeaderText="報到日" ReadOnly="True" SortExpression="EmpDate" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpDay" HeaderText="報到天數" ReadOnly="True" SortExpression="EmpDay" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LackOfParts" HeaderText="缺件數" ReadOnly="True" SortExpression="LackOfParts" >
                                            <HeaderStyle Width="3%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CheckInFlag" HeaderText="繳交狀態" ReadOnly="True" SortExpression="CheckInFlag" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="1.身分證影本乙份" SortExpression="File1">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile1_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File1")="1","True","False") %>' />
                                                <asp:Image ID="imgFile1_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File1")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="2.學歷證件影本乙份" SortExpression="File2">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile2_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File2")="1","True","False") %>' />
                                                <asp:Image ID="imgFile2_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File2")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="3.戶籍謄本影本或戶口名簿影本乙份" SortExpression="File3">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile3_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File3")="1","True","False") %>' />
                                                <asp:Image ID="imgFile3_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File3")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="4.照片五張" SortExpression="File4">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile4_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File4")="1","True","False") %>' />
                                                <asp:Image ID="imgFile4_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File4")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="5.保證書二份" SortExpression="File5">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile5_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File5")="1","True","False") %>' />
                                                <asp:Image ID="imgFile5_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File5")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="6.承諾書乙份" SortExpression="File6">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile6_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File6")="1","True","False") %>' />
                                                <asp:Image ID="imgFile6_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File6")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="7.客戶資料保密切結書乙份" SortExpression="File7">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile7_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File7")="1","True","False") %>' />
                                                <asp:Image ID="imgFile7_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File7")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="8.扶養親屬表" SortExpression="File8">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile8_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File8")="1","True","False") %>' />
                                                <asp:Image ID="imgFile8_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File8")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="9.人事資料表" SortExpression="File9">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile9_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File9")="1","True","False") %>' />
                                                <asp:Image ID="imgFile9_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File9")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="10.原公司離職證明乙份" SortExpression="File10">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile10_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File10")="1","True","False") %>' />
                                                <asp:Image ID="imgFile10_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File10")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="11.退伍令" SortExpression="File11">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile11_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File11")="1","True","False") %>' />
                                                <asp:Image ID="imgFile11_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File11")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="12.永豐銀行存摺影本" SortExpression="File12">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile12_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File12")="1","True","False") %>' />
                                                <asp:Image ID="imgFile12_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File12")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="13.團體醫療保險申請表" SortExpression="File13">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile13_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File13")="1","True","False") %>' />
                                                <asp:Image ID="imgFile13_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File13")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="14.勞、健保加保申請書" SortExpression="File14">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile14_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File14")="1","True","False") %>' />
                                                <asp:Image ID="imgFile14_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File14")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="15.識別證製作申請書" SortExpression="File15">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile15_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File15")="1","True","False") %>' />
                                                <asp:Image ID="imgFile15_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File15")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="16.員工二親等眷屬資料表" SortExpression="File16">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile16_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File16")="1","True","False") %>' />
                                                <asp:Image ID="imgFile16_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File16")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="17.健康檢查報告" SortExpression="File17">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile17_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File17")="1","True","False") %>' />
                                                <asp:Image ID="imgFile17_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File17")="1","False","True") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="18.同意書及員工基本工作規範" SortExpression="File18">
                                            <HeaderStyle CssClass="td_header" Width="3%" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgFile18_1" runat="server" ImageUrl="~/Images/chkbox.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File18")="1","True","False") %>' />
                                                <asp:Image ID="imgFile18_0" runat="server" ImageUrl="~/Images/chkboxE.gif" Visible='<% # Iif(DataBinder.Eval(Container.DataItem, "File18")="1","False","True") %>' />
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
