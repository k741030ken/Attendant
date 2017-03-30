<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GS1220.aspx.vb" Inherits="GS_GS1220" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--        
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
                <td width="15%" align="left" style="height: 30px;">
                    <asp:Label ID="lbltype" font-size="15px" runat="server" Text="統計表類型：" Font-Names="微軟正黑體"></asp:Label>
                </td>
                <td width="85%" align="left" style="height: 30px;">
                    <asp:DropDownList ID="ddltype" runat="server" Font-Size="12px" Width="150px" Font-Names="微軟正黑體"></asp:DropDownList>
                    <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidGradeYear" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidGradeSeq" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidApplyID" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidApplyTime" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidSeq" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidMainFlag" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hidDeptEX" runat="server"></asp:HiddenField>
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="2" align="center" style="height: 30px;" >
                    <asp:Label ID="lblTitle" font-size="15px" runat="server" Font-Bold="True" Font-Names="微軟正黑體"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="2" align="left" style="height: 30px;" >
                    <table id="table1" width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain1" runat="server" AllowSorting="False" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="Type" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Names="微軟正黑體" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" 
                                                    CommandName="Detail" Text="明細" Font-Names="微軟正黑體"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="5%" Font-Names="微軟正黑體" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TypeName" HeaderText="單位" ReadOnly="True">
                                            <HeaderStyle Width="15%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade9" HeaderText="特優" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade1" HeaderText="優" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade6" HeaderText="甲上" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade2" HeaderText="甲" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade7" HeaderText="甲下" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade3" HeaderText="乙" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade4" HeaderText="丙" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalCnt" HeaderText="合計(人)" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" Visible="false">
                                            <HeaderStyle Width="0%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" Font-Names="微軟正黑體" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" Font-Names="微軟正黑體" />
                                    <AlternatingRowStyle CssClass="tr_oddline" Font-Names="微軟正黑體" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" Font-Names="微軟正黑體" />
                                    <PagerStyle CssClass="GridView_PagerStyle" Font-Names="微軟正黑體" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain2" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain2" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,EmpID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray" Font-Names="微軟正黑體">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="OrganName" HeaderText="單位名稱" ReadOnly="True">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID" >
                                            <HeaderStyle Width="5%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NameN" HeaderText="姓名" ReadOnly="True">
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RankID" HeaderText="職等" ReadOnly="True" SortExpression="RankID" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Position" HeaderText="職位" ReadOnly="True">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GradeOrder" HeaderText="排序" ReadOnly="True" SortExpression="GradeOrder" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Grade" HeaderText="考績" ReadOnly="True" >
                                            <HeaderStyle Width="4%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
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
