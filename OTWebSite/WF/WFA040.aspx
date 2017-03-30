<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA040.aspx.vb" Inherits="WF_WFA040" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <link type="text/css" rel="stylesheet" href="../form.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
                case "btnActionX":
                    Close();
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
        
         
         function Close()
         {
            //alert(window.top.opener);
            window.top.opener.__doPostBack('btnRefresh','');
            window.top.close(); 
         }
       -->
    </script>
</head>
<base target="_self"/>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <center>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 98%;">
            <tr style="height:30px">
                <td class="tbl_Condition" align="right" width ='50%'>
                    流程：<asp:DropDownList ID="ddlFlowName" runat="server" CssClass="DropDownListStyle" AutoPostBack="true" Width="200px">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                </td>
                <td class="tbl_Condition" width="50%" align="left">　
                    關卡名稱：
                    <asp:DropDownList ID="ddlFlowStep" runat="server" CssClass="DropDownListStyle"  DataTextField="FlowStepDesc" AutoPostBack="true" Width="200px">
                     </asp:DropDownList>
                </td>
            </tr>  
            <tr>
                <td colspan = '2' align="center" width="100%" >
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="2" Width="100%" PageSize="15" CssClass="GridViewStyle" DataKeyNames="FlowID,FlowStepID,UserID,SeqNo">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnSelect" runat="server" CommandName="Edit" Text="修改"></asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" Text="刪除" OnClientClick="return funAction('btnDelete')"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FlowName" HeaderText="流程" ReadOnly="True" SortExpression="FlowName" meta:resourcekey="BoundFieldResource1" >
                                <HeaderStyle Width="15%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FlowStepID" HeaderText="關卡代號" ReadOnly="True" SortExpression="FlowStepID" meta:resourcekey="BoundFieldResource3" >
                                <HeaderStyle Width="8%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="關卡名稱" ReadOnly="True" SortExpression="FlowStepDesc" meta:resourcekey="BoundFieldResource5" >
                                <HeaderStyle Width="15%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FlowPhrase" HeaderText="片語" ReadOnly="True" SortExpression="FlowPhrase" meta:resourcekey="BoundFieldResource3" >
                                <HeaderStyle Width="22%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="最後異動日期" SortExpression="LastChgDate">
                                <ItemStyle CssClass="td_detail" />
                                <HeaderStyle CssClass="td_header" Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("LastChgDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="LastChgID" HeaderText="更新人員" ReadOnly="True" SortExpression="LastChgID" meta:resourcekey="BoundFieldResource3" >
                                <HeaderStyle Width="15%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" meta:resourcekey="lblNoDataResource1"></asp:Label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="tr_evenline" />
                        <AlternatingRowStyle CssClass="tr_oddline" />
                        <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                        <PagerStyle CssClass="GridView_PagerStyle" />
                        <PagerSettings Position="Top" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
         </center>
         <br />
         <div id= 'DivToDoLst' runat="server" >
        </div>
    </form>
</body>
</html>
