<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA050.aspx.vb" Inherits="WF_WFA050" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <link type="text/css" rel="stylesheet" href="../form.Css" />
    <script type="text/javascript">
    <!--
        function openWindow(url){
           window.open(url,"FlowDetail", 'height=600px,width=800px,top=20px,left=50px,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no, status=no')
        }
        
        
        function funAction(Param)
        {
              
            switch(Param)
            {
                case "btnUpdate":
                    var ComfirmMsgStr = '您要進行改派動作?';
                    var objInput = document.getElementById("UserSelect_ddlUser");
                    var objInputAgn = document.getElementById("OneUserSelectAssign_ddlUser");
                    if (objInput) {
                        if (objInput.options[objInput.selectedIndex].value == '') {
                            alert("原處理人員未選取！");
                            return false;
                        }
                    }
                    if (objInputAgn) {
                        if (objInputAgn.options[objInputAgn.selectedIndex].value == '') {
                            alert("改派給予人員未選取！");
                            return false;
                        }
                    }

                    if ((objInput) && (objInputAgn)) {
                        if (objInput.options[objInput.selectedIndex].value == objInputAgn.options[objInputAgn.selectedIndex].value) {
                            alert("改派與被改派人員為同一人！");
                            return false;
                        }
                    }

                    if (document.getElementById("TxtAreaOption").value == '') {
                        alert("意見內容未登打！");
                        return false;
                    }
                    if (document.getElementById("__SelectedRowsgvMain").value == '') {
                        alert('未選取任何改派資料列!');
                        return false;
                    } else {
                        //alert(document.getElementById("__SelectedRowsgvMain").value);
                    }
                    ComfirmMsgStr += '\r原處理人員【：' + objInput.options[objInput.selectedIndex].value + '】';
                    ComfirmMsgStr += '\r改派给【：' + objInputAgn.options[objInputAgn.selectedIndex].value + '】';
                    ComfirmMsgStr += '\r意見內容：' + document.getElementById("TxtAreaOption").value;
                    if (confirm(ComfirmMsgStr)) {
                        return true;
                    } else {
                        return false;
                    }
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
        
        
        function attachPostBack(){
            objDDL = document.getElementById("UserSelect_ddlUser");
            if( objDDL ){
                objDDL.attachEvent("onchange",PostBack_ddlUser);
            }
        }
        function PostBack_ddlUser(){
            __doPostBack("UserSelect_ddlUser","");
        }
        window.attachEvent("onload",attachPostBack);
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <center>
        <table border="0" cellpadding="1" cellspacing="1" style="width: 95%;">
            <tr>
                <td class="td_EditHeader" width="25%" align="center">
                      <asp:Label ID="lblUserID"  runat="server" Text="案件處理人" CssClass="MustInputCaption"></asp:Label>
                </td>
                 <td class="td_Edit" style="width:75%" align="left">
                    <uc:OneUserSelect ID="UserSelect" runat="server"  UserType="ValidUser"  />                
                </td>
            </tr>
            <tr>
                <td class="td_EditHeader" width="25%" align="center">
                      <asp:Label ID="FlowKind" runat="server" Text="案件類別" CssClass="MustInputCaption"></asp:Label>
                </td>
                 <td class="td_Edit" style="width:75%" align="left">
                    <asp:DropDownList ID="ddlFlowName" runat="server" DataTextField="FlowName" DataValueField="FlowID" AutoPostBack="True" CssClass="DropDownListStyle">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_EditHeader" width="25%" align="center">
                     <asp:Label ID="Label3"  runat="server" Text="案件改派至" CssClass="MustInputCaption"></asp:Label>
                </td>
                 <td class="td_Edit" style="width:75%" align="left">
                     <uc:OneUserSelect ID="OneUserSelectAssign" runat="server"  UserType="ValidUser" />                    
                 </td>
            </tr>
            <tr>
                  <td class="td_EditHeader" width="25%" align="center">
                      <asp:Label ID="lblOption" runat="server" Text="改派意見"></asp:Label>
                  </td>
                   <td class="td_Edit" style="width:75%" align="left">
                        <textarea id="TxtAreaOption" rows="2" style="width:99%" runat="server" ></textarea>
                  </td>
            </tr>        
         </table>
         </center>
         <br />
         <center>
         <table border="0" cellpadding="0" cellspacing="0" style="width: 95%;">
            <tr>
                 <td class="divSepTitle" colspan="2" align="left">
                    <div class="divSepTitle">
                        <div class="Head">人員待辦清單</div>
                        <div class="Body">
                            <span style="float:left"></span>
                            <span style="float:right"></span>
                        </div>
                    </div>
                 </td>
            </tr>
            <tr>
                <td colspan = '2' align="center" width="100%" >
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataSourceID="sdsMain" CellPadding="2" Width="100%" PageSize="15" CssClass="GridViewStyle" DataKeyNames="FlowID,FlowCaseID,FlowLogID">
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <Columns>
                           <asp:TemplateField HeaderText="選取" meta:resourcekey="TemplateFieldResource1">
                                <ItemStyle CssClass="td_detail" Height="15px" />
                                <HeaderStyle CssClass="td_header" Width="5%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_gvMain" runat="server" />
                                    <asp:Label ID="lblFlowKeyStr" runat="server" style="display:none" Text='<%# Eval("FlowKeyStr") %>'></asp:Label>
                                    <asp:Label ID="APPIDStr" runat="server" style="display:none" Text='<%# Eval("KeyID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText="明细" meta:resourcekey="TemplateFieldResource2">
                                <ItemStyle CssClass="td_detail" Height="15px" />
                                <HeaderStyle CssClass="td_header" Width="4%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbnShowDetail" CommandName="Edit" runat="server"><Font style="font-size:12pt;color=green;font-family:Wingdings">&</font></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="Seq" HeaderText="序號" ReadOnly="True" SortExpression="Seq">
                                <HeaderStyle Width="4%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FlowDispatchFlag" HeaderText="急件" ReadOnly="True" SortExpression="FlowDispatchFlag">
                                <HeaderStyle Width="4%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FLowStepDesc" HeaderText="關卡" ReadOnly="True" SortExpression="FlowStepID">
                                <HeaderStyle Width="15%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="KeyID" HeaderText="申請編號" ReadOnly="True" SortExpression="APPID">
                                <HeaderStyle Width="10%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomerName" HeaderText="借戶" ReadOnly="True" SortExpression="CustomerName">
                                <HeaderStyle Width="17%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Note" HeaderText="備註" ReadOnly="True" SortExpression="Note">
                                <HeaderStyle Width="15%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FromBrName" HeaderText="發出部門" ReadOnly="True" SortExpression="FromBrName">
                                <HeaderStyle Width="8%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FromUserName" HeaderText="發出人" ReadOnly="True" SortExpression="FromUserName">
                                <HeaderStyle Width="8%" CssClass="td_header" />
                                <ItemStyle CssClass="td_detail" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="發出時間" SortExpression="FromDate">
                                <ItemStyle CssClass="td_detail" />
                                <HeaderStyle CssClass="td_header" Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FromDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                        </EmptyDataTemplate>
                        <RowStyle CssClass="tr_evenline" />
                        <AlternatingRowStyle CssClass="tr_oddline" />
                        <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                        <PagerStyle CssClass="GridView_PagerStyle" />
                        <PagerSettings Position="Top" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsMain" runat="server" ConnectionString="<%$ ConnectionStrings:DB_CCNJ %>">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </center>
    </form>
</body>
</html>
