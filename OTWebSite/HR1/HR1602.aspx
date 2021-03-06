<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR1602.aspx.vb" Inherits="HR_HR1602" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        //        隱藏TR
        function hide_tr(strID) {
            var result_style = document.getElementById(strID).style; result_style.display = 'none';
        }
        //        顯示TR
        function show_tr(strID) {
            var result_style = document.getElementById(strID).style; result_style.display = 'table-row';
        }
        function funAction(Param) {
            //debugger;
            if (Param == 'btnQueryEmp') {
                fromField = document.getElementById("btnQueryEmp");
                fromField.click()
            }            
        }
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    一、員工調兼資料
                </td>
            </tr>
            <tr>
                <td>            
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                        <tr>
                            <td align="center">
                                <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpID" ForeColor="Blue" runat="server" Text="*員工編號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">  
                                            <asp:Label ID="lblEmpID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                            <asp:Label ID="lblUserName_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>                            
                                            <asp:HiddenField ID="hidIDNo" runat="server" />
                                            <asp:HiddenField ID="hidEmpDate" runat="server" />
                                        </td>   
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCompID" ForeColor="Blue" runat="server" Text="*現任公司"></asp:Label>
                                        </td>    
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblCompID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblDeptID" runat="server" Text="現任部門/科組課"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblDeptID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                            <asp:Label ID="lblOrganID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblTitle" runat="server" Text="子公司職稱(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblTitle_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPosition" runat="server" Text="職位(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblPosition_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblWorkType" runat="server" Text="工作性質(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblWorkType_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblHoldingRank" runat="server" Text="金控職等(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblHoldingRank_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblHoldingTitle" runat="server" Text="金控職級(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblHoldingTitle_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblAdditionDeptBoss" runat="server" Text="兼任部門主管(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkAdditionDeptBoss" runat="server" Enabled="false" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblAdditionOrganBoss" runat="server" Text="兼任科組課主管(現況)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkAdditionOrganBoss" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblModifyDate" ForeColor="Blue" runat="server" Text="*生效日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblValidDate_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>                                         
                                        </td>                    
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionReason" ForeColor="Blue" runat="server" Text="*狀態"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblReason_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                        </td>
                                    </tr>       
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblFileNO" ForeColor="Blue" runat="server" Text="*人令"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="85%" align="left" colspan="3">
                                            <asp:Label ID="lblFileNO_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                        </td>    
                                    </tr>                                                
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionCompID" ForeColor="Blue" runat="server" Text="*兼任公司"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">    
                                            <asp:Label ID="lblAdditionCompID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>          
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionDeptID" ForeColor="Blue" runat="server" Text="*兼任部門/科組課"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblAdditionDeptID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                            <asp:Label ID="lblAdditionOrganID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionFlowOrganID" runat="server" Text="兼任簽核最小單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblAdditionFlowOrganID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label> 
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionBossType" runat="server" Text="主管任用方式"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:RadioButton ID="rbnEmpAdditionBossType1" GroupName="rbnEmpAdditionBossType" runat="server" Text="主要" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbnEmpAdditionBossType2" GroupName="rbnEmpAdditionBossType" runat="server" Text="兼任" Enabled="false" />                                            
                                        </td>
                                    </tr>
                                    <tr style="height:20px;DISPLAY:table-row" id="trBossType">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionOrganBoss" runat="server" Text="單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkEmpAdditionIsBoss" Text="主管" runat="server" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmpAdditionIsSecBoss" Text="副主管" runat="server" Enabled="false" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionOrganFlowBoss" runat="server" Text="簽核單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkEmpAdditionIsGroupBoss" Text="主管" runat="server" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmpAdditionIsSecGroupBoss" Text="副主管" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px;DISPLAY:table-row" id="trIsBoss">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionRemark" runat="server" Text="備註"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="85%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEmpAdditionRemark" CssClass="InputTextStyle_Thin" runat="server" 
                                                MaxLength="500" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCreateDate" runat="server" Text="建檔日期"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblCreateDate_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCreateID" runat="server" Text="建檔人員"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblCreateID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                    </tr>                               
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動日期"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblLastChgDate_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLastChgID" runat="server" Text="最後異動人員"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:Label ID="lblLastChgID_S" CssClass="InputTextStyle_Thin" runat="server"></asp:Label>
                                        </td>
                                    </tr>               
                                </table>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td align="center">
                    二、員工調兼異動紀錄
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">                        
                        <tr>
                            <td align="center">
                                <table width="80%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100%">
                                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                                        </td> 
                                    </tr>
                                    <tr>
                                        <td align="left" style="width:100%">
                                            <asp:Panel ID="Panel1" ScrollBars="Horizontal" runat="server" Width="1580px" >
                                            <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="2" Width="100%" PageSize="15" CssClass="GridViewStyle" 
                                                DataKeyNames="CompID,EmpID,ValidDate,Reason,AddCompID,AddDeptID,AddOrganID">
                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                <Columns>
                                                    <asp:BoundField DataField="AddCompName" HeaderText="調兼公司" ReadOnly="True" SortExpression="AddCompName">
                                                        <HeaderStyle Width="5%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AddDeptName" HeaderText="調兼部門" ReadOnly="True" SortExpression="AddDeptName">
                                                        <HeaderStyle Width="8%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AddOrganName" HeaderText="調兼科組課" ReadOnly="True" SortExpression="AddOrganName">
                                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>                                                
                                                    <asp:BoundField DataField="ReasonName" HeaderText="異動原因" ReadOnly="True" SortExpression="ReasonName">
                                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ValidDate" HeaderText="生效日期" ReadOnly="True" SortExpression="ValidDate">
                                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FileNo" HeaderText="人令" ReadOnly="True" SortExpression="FileNo">
                                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>                                                    
                                                    <asp:BoundField DataField="BossType" HeaderText="主管任用方式" ReadOnly="True" SortExpression="BossType">
                                                        <HeaderStyle Width="3%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsBoss" HeaderText="主管" InsertVisible="False" ReadOnly="True" SortExpression="IsBoss">
                                                        <HeaderStyle CssClass="td_header" Width="3%" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:CheckBoxField>
                                                    <asp:CheckBoxField DataField="IsSecBoss" HeaderText="副主管" InsertVisible="False" ReadOnly="True" SortExpression="IsSecBoss">
                                                        <HeaderStyle CssClass="td_header" Width="3%" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:CheckBoxField>
                                                    <asp:CheckBoxField DataField="IsGroupBoss" HeaderText="簽核主管" InsertVisible="False" ReadOnly="True" SortExpression="IsGroupBoss">
                                                        <HeaderStyle CssClass="td_header" Width="3%" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:CheckBoxField>                                           
                                                    <asp:CheckBoxField DataField="IsSecGroupBoss" HeaderText="簽核副主管" InsertVisible="False" ReadOnly="True" SortExpression="IsSecGroupBoss">
                                                        <HeaderStyle CssClass="td_header" Width="3%" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:CheckBoxField>                                                      
                                                    <asp:BoundField DataField="PositionName" HeaderText="職位" ReadOnly="True" SortExpression="PositionName">
                                                        <HeaderStyle Width="8%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WorkTypeName" HeaderText="工作性質" ReadOnly="True" SortExpression="WorkTypeName">
                                                        <HeaderStyle Width="8%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TitleName" HeaderText="職稱" ReadOnly="True" SortExpression="TitleName">
                                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>                                                
                                                    <asp:BoundField DataField="RankID" HeaderText="職等" ReadOnly="True" SortExpression="RankID">
                                                        <HeaderStyle Width="1%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="HoldingTitleName" HeaderText="金控職稱" ReadOnly="True" SortExpression="HoldingTitleName">
                                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="HoldingRankID" HeaderText="金控職等" ReadOnly="True" SortExpression="HoldingRankID">
                                                        <HeaderStyle Width="1%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Remark" HeaderText="備註" ReadOnly="True" SortExpression="Remark">
                                                        <HeaderStyle Width="10%" CssClass="td_header"/>
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LastChg" HeaderText="最後異動日及人員" ReadOnly="True" SortExpression="LastChg">
                                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                                        <ItemStyle CssClass="td_detail" />
                                                    </asp:BoundField>                                                
                                                    <asp:BoundField DataField="CreateData" HeaderText="建檔日及人員" ReadOnly="True" SortExpression="CreateData">
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
                                                <PagerSettings Position="Top" />
                                            </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <%--<tr style="height:20px">
                                    </tr>
                                    <tr>
                                        <td align="left" style="width:100%">
                                            <asp:Panel ID="Panel_EmpAdditionLog" Visible="false" runat="server" Width="100%" >
                                                 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td align="left" style="width:100%">                                
                                                            <table class="tbl_Edit" cellpadding="1" cellspacing="1" style="width: 100%">
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogFileNo" runat="server" Text="人令"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:Label ID="lblEmpAdditionLogFileNo_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogBossType" runat="server" Text="主管任用方式"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:RadioButton ID="rbnEmpAdditionLogBossType1" GroupName="rbnEmpAdditionLogBossType" runat="server" Text="主要" Enabled="false"/>&nbsp;&nbsp;&nbsp;
                                                                        <asp:RadioButton ID="rbnEmpAdditionLogBossType2" GroupName="rbnEmpAdditionLogBossType" runat="server" Text="兼任" Enabled="false" />  
                                                                    </td>
                                                                </tr>    
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogOrganBoss" runat="server" Text="單位"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:CheckBox ID="chkEmpAdditionLogIsBoss" Text="主管" runat="server" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkEmpAdditionLogIsSecBoss" Text="副主管" runat="server" Enabled="false" />
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogOrganFlowBoss" runat="server" Text="簽核單位"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:CheckBox ID="chkEmpAdditionLogIsGroupBoss" Text="主管" runat="server" Enabled="false" />&nbsp;&nbsp;&nbsp;
                                                                        <asp:CheckBox ID="chkEmpAdditionLogIsSecGroupBoss" Text="副主管" runat="server" Enabled="false" />
                                                                    </td>
                                                                </tr> 
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogPosition" runat="server" Text="職位"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:Label ID="lblEmpAdditionLogPosition_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogWorkType" runat="server" Text="工作性質"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:Label ID="lblEmpAdditionLogWorkType_S" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr> 
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogTitle" runat="server" Text="職稱"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:15%" align="left">
                                                                        <asp:Label ID="lblEmpAdditionLogTitle_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogRank" runat="server" Text="職等"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:15%" align="left">
                                                                        <asp:Label ID="lblEmpAdditionLogRank_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogHoldingTitle" runat="server" Text="金控職稱"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:15%" align="left">
                                                                        <asp:Label ID="lblEmpAdditionLogHoldingTitle_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogHoldingRank" runat="server" Text="金控職等"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:15%" align="left">
                                                                        <asp:Label ID="lblEmpAdditionLogHoldingRank_S" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr> 
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogRemark" runat="server" Text="備註"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:90%" align="left" colspan="7">
                                                                         <asp:Label ID="lblEmpAdditionLogRemark_S" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr> 
                                                                <tr style="height:20px">
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogLastChg" runat="server" Text="最後異動日期及人員"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:Label ID="lblEmpAdditionLogLastChg_S" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td class="td_EditHeader" width="10%" align="center">
                                                                        <asp:Label ID="lblEmpAdditionLogCreate" runat="server" Text="建檔日期及人員"></asp:Label>
                                                                    </td>
                                                                    <td class="td_Edit" style="width:40%" align="left" colspan="3">
                                                                        <asp:Label ID="lblEmpAdditionLogCreate_S" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>      
                                                            </table>
                                                        </td>
                                                    </tr>
                                                 </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
