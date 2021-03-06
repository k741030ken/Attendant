<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST1B01.aspx.vb" Inherits="ST_ST1B01" %>

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
                                            <asp:Label ID="txtEmpID" runat="server" ></asp:Label>
                                             <asp:Label ID="txtEmpName" runat="server" ></asp:Label>
                                            <asp:HiddenField ID="hidCompID" runat="server" />
                                            <asp:HiddenField ID="hidEmpID" runat="server" />
                                            <asp:HiddenField ID="hidIDNo" runat="server" />
                                            <asp:HiddenField ID="hidEmpDate" runat="server" />
                                        </td>   
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblCompID" ForeColor="Blue" runat="server" Text="*現任公司"></asp:Label>
                                        </td>    
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <%--<asp:DropDownList ID="ddlCompID" runat="server"></asp:DropDownList>--%>
                                            <asp:Label ID="lblCompID_S" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblModifyDate" ForeColor="Blue" runat="server" Text="*生效日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <uc:uccalender ID="ucValidDate" runat="server" Enabled="True" />
                                        </td>                    
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionReason" ForeColor="Blue" runat="server" Text="*狀態"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:DropDownList ID="ddlEmpAdditionReason" runat="server"></asp:DropDownList>
                                        </td>
                                    </tr>       
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblFileNO" ForeColor="Blue" runat="server" Text="*人令"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="85%" align="left" colspan="3">
                                            <asp:TextBox ID="txtFileNO" CssClass="InputTextStyle_Thin" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                        </td>    
                                    </tr>                                                
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionCompID" ForeColor="Blue" runat="server" Text="*兼任公司"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">             
                                            <asp:DropDownList ID="ddlEmpAdditionCompID" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionDeptID" ForeColor="Blue" runat="server" Text="*兼任部門/科組課"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:UpdatePanel ID="UdpEmpAdditionDeptID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <uc:SelectHROrgan ID="ucSelectEmpAdditionHROrgan" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ucSelectEmpAdditionHROrgan" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionFlowOrganID" runat="server" Text="兼任簽核最小單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:UpdatePanel ID="UpdEmpAdditionFlowOrgnaID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlEmpAdditonFlowOrganID" runat="server"></asp:DropDownList>
                                            </ContentTemplate>                                                          
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionBossType" runat="server" Text="主管任用方式"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:DropDownList ID="ddlEmpAdditionBossType" runat="server"></asp:DropDownList>                                                             
                                        </td>
                                    </tr>
                                    <tr style="height:20px;DISPLAY:table-row" id="trBossType">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionOrganBoss" runat="server" Text="單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkEmpAdditionIsBoss" Text="主管" runat="server" />&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmpAdditionIsSecBoss" Text="副主管" runat="server" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpAdditionOrganFlowBoss" runat="server" Text="簽核單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:CheckBox ID="chkEmpAdditionIsGroupBoss" Text="主管" runat="server" />&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkEmpAdditionIsSecGroupBoss" Text="副主管" runat="server" />
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
