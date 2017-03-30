<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA080.aspx.vb" Inherits="WF_WFA080" EnableEventValidation="false" %>

<%@ Register TagPrefix="uc" TagName="MultiSelect" Src="../Component/ucMultiSelect.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <link type="text/css" rel="stylesheet" href="../Form.Css" />

<%--    <script type="text/javascript">
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
        
        function WFredirectPage(Path,ErrMsg)
        {
            window.parent.parent.frames[1].location='CCA030.aspx';
            window.parent.parent.frames[2].imgSplit_WorkFlow.src = "../images/mopen.png";
            window.parent.parent.frames[2].imgSplit_WorkFlow.alt = "Close WorkFlow menu";
            window.parent.parent.frmSet.cols = "0,280,20,*";
            window.parent.parent.frames[2].imgSplit_WorkFlow.style.display = "";  
            if (ErrMsg != '') {
               alert(ErrMsg);
           }
            if (Path != '') {
              window.parent.parent.frames[3].location = '/WF/CCA031.aspx?Path=' + Path;
           }
        }
       -->
    </script>--%>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <center>
        <table border="0" cellpadding="0" cellspacing="0" style="width:95%;">
            <tr> 
                <td class="td_Edit" align="left" >                               
                    <asp:Label ID="Label1" runat="server" Font-Size="12px"></asp:Label>
                    <asp:DropDownList ID="ddlOrgan" AutoPostBack="true" runat="server" CssClass="DropDownListStyle" Width="250px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td>
                    <uc:MultiSelect ID="UcMultiSelect1" ShowDeptType="false" LeftCaption="未選簽核主管" DisplayType="Full" RightCaption="已選簽核主管" ListRows="10"  runat="server" />
                </td>
            </tr>

            <tr>
                 <td class="divSepTitle"  align="left" style="height: 34px">
                 <div class="divSepTitle">
                                <div class="Head">簽核主管清單</div>
                                    <div class="Body">
                                        <span style="float:left"></span>
                                        <span style="float:right"></span>
                                    </div>
                                </div>
                 </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <asp:GridView ID="gvMain" Font-Size="12px" runat="server"  AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" DataSourceID="sdsMain" CellPadding="2" Width="100%" AllowSorting="True">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="tr_evenline" />
                        <AlternatingRowStyle CssClass="tr_oddline" />
                        <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                        <PagerStyle CssClass="GridView_PagerStyle" />
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:BoundField DataField="CASEState" >
                                <HeaderStyle Width="20%" CssClass="td_header" Font-Size="12px" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrganName" HeaderText="單位" >
                                <HeaderStyle Width="40%" CssClass="td_header" Font-Size="12px" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UserName" HeaderText="姓名"  >
                                <HeaderStyle Width="40%" CssClass="td_header" Font-Size="12px" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMain" runat="server" ConnectionString="<%$ ConnectionStrings:DB_CCNJ %>" >
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 95%;">
            <tr>
                 <td class="divSepTitle" colspan="2" align="left" style="height: 34px">
                 <div class="divSepTitle">
                                <div class="Head">簽核主管異動明細</div>
                                    <div class="Body">
                                        <span style="float:left"></span>
                                        <span style="float:right"></span>
                                    </div>
                                </div>
                 </td>
            </tr>
            <tr>
                <td align="center" width="100%" >
                                <asp:GridView ID="LogGridView" runat="server" AllowPaging="False" AutoGenerateColumns="False" CellPadding="2" Width="100%" CssClass="GridViewStyle" meta:resourcekey="gvMainResource2"  >
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:BoundField DataField="LoanCheckKind" HeaderText="核貸種類" ReadOnly="True" SortExpression="LoanCheckKind" meta:resourcekey="BoundFieldResource2" >
                                            <HeaderStyle Width="8%" CssClass="td_header" Font-Size="12px" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FlowSeq" HeaderText="簽核順序" ReadOnly="True" SortExpression="FlowSeq" meta:resourcekey="BoundFieldResource3" >
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Size="12px" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LoanCheckIDName" HeaderText="簽核主管" ReadOnly="True" SortExpression="LoanCheckIDName" meta:resourcekey="BoundFieldResource3" >
                                            <HeaderStyle Width="14%" CssClass="td_header" Font-Size="12px" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="簽核作業(起)" SortExpression="LoanCheckdateBeg">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="13%" Font-Size="12px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("LoanCheckdateBeg", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="簽核作業(迄)" SortExpression="LoanCheckdateEnd">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="13%" Font-Size="12px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("LoanCheckdateEnd", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LogChgIDName" HeaderText="異動人員" ReadOnly="True" SortExpression="LogChgIDName" meta:resourcekey="BoundFieldResource5" >
                                            <HeaderStyle Width="14%" CssClass="td_header" Font-Size="12px" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="異動時間" SortExpression="LogChgDate">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="13%" Font-Size="12px" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("LogChgDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LogDesc" HeaderText="異動說明" ReadOnly="True" SortExpression="LogDesc" meta:resourcekey="BoundFieldResource6" >
                                            <HeaderStyle Width="18%" CssClass="td_header" Font-Size="12px" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無異動資料顯示！" meta:resourcekey="lblNoDataResource1"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="sdsMainLog" runat="server" ConnectionString="<%$ ConnectionStrings:DB_CCNJ %>" >
                                </asp:SqlDataSource>
                </td>
            </tr>
        </table> 
        </center>
        <br /> 
    </form>
</body>
</html>
