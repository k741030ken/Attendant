<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR3001.aspx.vb" Inherits="HR_HR3001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function IsTOConfirm(Param) {
            switch (Param) {
                case "EmployeeLogWait":
                    if (!confirm('此員工生效日後，已有『已生效』的企業團經歷存在，請確認是否要調整資料？')) {
                        fromField = document.getElementById("btnBack");
                        fromField.click()
//                        return false;
                        break;
                    }
                    fromField = document.getElementById("btnEmployeeLogWait");
                    fromField.click()
                    break;
                case "BackQuery":
                    if (!confirm('此員工生效日後，已有『未生效』的待異動資料存在，請確認是否要調整資料？')) {
                        fromField = document.getElementById("btnBack");
                        fromField.click()
//                        return false;
                        break;
                    }
                    fromField = document.getElementById("btnBackQuery");
                    fromField.click()
                    break;
                case "OPBoss":
                    if (!confirm('工作性質是分行作業主管，但未勾選簽核主管註記，請確認是否繼續儲存？')) {
                        //return false;
                        break;
                    }
                    fromField = document.getElementById("btnCheckSave");
                    fromField.click()
                    break;                                     
            }
        }
        function IsTOConfirm1(Param) {
            if (confirm(Param)) {
                fromField = document.getElementById("btnCheckSave");
                fromField.click()
                //return false;
                //break;
            }
            //break;         
        }

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

        $(function () {
            $("#ucValidDate_txtDate").change(function () {
                $("#btnDateChange").click();
            });
        })
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">異動後資料</td>
            </tr>
            <tr>
                <td align="center">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblModifyDate" ForeColor="Blue" runat="server" Text="*生效日"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left" colspan="3">
                                <uc:uccalender ID="ucValidDate" runat="server" Enabled="True" />
                                <asp:Label ID="lblDueDate" runat="server" Text="～" Visible="false"></asp:Label>
                                <uc:uccalender ID="ucDueDate" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblEmpID" ForeColor="Blue" runat="server" Text="*員工編號"></asp:Label>
                            </td>
                            <td colspan="3" class="td_Edit" style="width:85%" align="left">
                                <asp:TextBox ID="txtEmpID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"></asp:TextBox>
                                <asp:TextBox ID="txtUserName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" Width="100px" ReadOnly="True" TabIndex="1"></asp:TextBox>
                                <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                                <asp:Button ID="btnQueryEmp" runat="server" Text="Query" style="height: 21px;display:none" Enabled="True" />
                                <asp:HiddenField ID="hidCompID" runat="server" />
                                <asp:HiddenField ID="hidEmpID" runat="server" />
                                <asp:HiddenField ID="hidIDNo" runat="server" />
                                <asp:HiddenField ID="hidEmpDate" runat="server" />
                                <asp:HiddenField ID="hidSeq" runat="server" />
                                <%--<asp:HiddenField ID="hidWorkTypeID" runat="server" />
                                <asp:HiddenField ID="hidPositionID" runat="server" />--%>
                                <asp:Button ID = "btnEmployeeLogWait" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                                <asp:Button ID = "btnBackQuery" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                                <asp:Button ID = "btnBack" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                                <asp:Button ID = "btnCheckSave" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                                <asp:Button ID = "btnDateChange" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblReason" ForeColor="Blue" runat="server" Text="*異動原因"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True" TabIndex="2"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblNotice" runat="server" Text="" ForeColor="Red" Font-Size="Small"></asp:Label>
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblQuitReason" runat="server" Text="離職原因"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:DropDownList ID="ddlQuitReason" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblNewCompID" ForeColor="Blue" runat="server" Text="*異動後公司代碼"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:DropDownList ID="ddlNewCompID" runat="server" Enabled="False" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblDeptID" runat="server" Text="部門/科組課"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:UpdatePanel ID="UdpDeptID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <uc:SelectHROrgan ID="ucSelectHROrgan" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ucSelectHROrgan" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblFlowOrganID" runat="server" Text="最小功能單位"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:UpdatePanel ID="UpdFlowOrgnaID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <%--<asp:DropDownList ID="ddlFlowOrganID" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                                    <uc:ucSelectFlowOrgan ID="ucSelectFlowOrgan" runat="server" Enabled="True" AutoPostBack="true" />
                                </ContentTemplate>                                   
                                </asp:UpdatePanel>
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblSalaryPaid" runat="server" Text="計薪註計"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:CheckBox ID="chkSalaryPaid" Text="" runat="server" Enabled="false"/>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblEmpFlowRemarkID" runat="server" Text="功能備註"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left" colspan="3">
                                <asp:UpdatePanel ID="UpdEmpFlowRemark" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlBusinessType" runat="server" Enabled="false"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlEmpFlowRemarkID" runat="server"></asp:DropDownList>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="height:20px;DISPLAY:table-row" id="trBossType">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblBossType" runat="server" Text="主管任用方式"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left" colspan="3">
                                <asp:DropDownList ID="ddlBossType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px;DISPLAY:table-row" id="trIsBoss">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblOrganBoss" runat="server" Text="單位"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:CheckBox ID="chkIsBoss" Text="主管" runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkIsSecBoss" Text="副主管" runat="server" />
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblOrganFlowBoss" runat="server" Text="簽核單位"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:CheckBox ID="chkIsGroupBoss" Text="主管" runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="chkIsSecGroupBoss" Text="副主管" runat="server" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblRankID" runat="server" Text="職等職稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:UpdatePanel ID="UpdRankID" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlRankID" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlTitleID" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    <asp:TextBox ID="txtRankIDMap" CssClass="InputTextStyle_Thin" Style="text-transform: uppercase" runat="server" MaxLength="1" Height="5px" Width="5px" Visible ="false"  ></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel> 
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblWorkSiteID" runat="server" Text="工作地點"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:UpdatePanel ID="UpdWorkSite" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlWorkSiteID" runat="server"></asp:DropDownList>    
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblWorkTypeID" runat="server" Text="工作性質"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left" colspan="3">
                                <asp:UpdatePanel ID="UpdWorkType" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlWorkType" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <uc:ButtonWorkType ID="ucSelectWorkType" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" />
                                <asp:Label ID="lblSelectWorkType" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>  
                            </td>
                        </tr>
                        <tr style="height:20px;DISPLAY:table-row" id="trPosition">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblPositionID" runat="server" Text="職位"></asp:Label>
                            </td>
                            <td  class="td_Edit" style="width:85%" align="left" colspan="3">
                                <asp:UpdatePanel ID="UpdPosition" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <uc:ButtonPosition ID="ucSelectPosition" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" />
                                <asp:Label ID="lblSelectPosition" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label> 
                            </td>
                        </tr>  
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblRecID" runat="server" Text="應試者編號"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtRecID" style="text-transform:uppercase" runat="server" MaxLength="14" Width="100px"></asp:TextBox>
                            </td>
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblContractDate" runat="server" Text="預計報到日"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:35%" align="left">
                                <uc:uccalender ID="ucContractDate" runat="server" Enabled="True" />
                            </td>
                        </tr>
                        <tr style="height:20px;DISPLAY:table-row" id="trWTID">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblWTID" runat="server" Text="班別"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left" colspan="3">
                                <asp:DropDownList ID="ddlWTID" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center">
                                <asp:Label ID="lblRemark" runat="server" Text="備註"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left" colspan="3">
                                <asp:TextBox ID="txtRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" Width="600px"></asp:TextBox>
                                <uc:ButtonQuerySelectHR ID="ucSelectRemark" runat="server" ButtonText="..." ButtonHint="常用備註..." WindowHeight="550" WindowWidth="500" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">調兼資料</td>
            </tr>
            <tr>
                <td align="center"> 
                    <table width="80%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnAddEmpAddition" runat="server" CssClass="button" Text="新增" onclick="btnAddEmpAddition_Click" />
                                <asp:HiddenField ID="hdfEmpAdditionMaxAddSeqNo" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="80%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width:100%">
                                <asp:Panel ID="Panel_EmpAddition"  runat="server" Visible="False" Width="100%" >
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td align="left" style="width:100%">
                                            <table class="tbl_Edit" cellpadding="1" cellspacing="1" style="width: 100%">
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblEmpAdditionReason" ForeColor="Blue" runat="server" Text="*狀態"></asp:Label>
                                                    </td>
                                                    <td class="td_Edit" style="width:35%" align="left">
                                                        <asp:DropDownList ID="ddlEmpAdditionReason" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblFileNO" ForeColor="Blue" runat="server" Text="*人令"></asp:Label>
                                                    </td>
                                                    <td class="td_Edit" width="35%" align="left">
                                                        <asp:TextBox ID="txtFileNO" CssClass="InputTextStyle_Thin" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height:20px">
                                                    <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblEmpAdditionCompID" ForeColor="Blue" runat="server" Text="*兼任公司"></asp:Label>
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
                                                        <asp:Label ID="lblEmpAdditionFlowOrganID" runat="server" Text="兼任最小功能單位"></asp:Label>
                                                    </td>
                                                    <td class="td_Edit" style="width:35%" align="left">
                                                        <asp:UpdatePanel ID="UpdEmpAdditionFlowOrgnaID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <%--<asp:DropDownList ID="ddlEmpAdditonFlowOrganID" runat="server"></asp:DropDownList>--%>
                                                            <uc:ucSelectFlowOrgan ID="ucSelectAddFlowOrgan" runat="server" Enabled="True" AutoPostBack="true" />
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
                                                <tr style="height:20px">
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
                                                <tr style="height:20px">                                                           
                                                    <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblEmpAdditionRemark" runat="server" Text="調兼備註"></asp:Label>
                                                    </td>
                                                    <td class="td_Edit" width="85%" align="left" colspan="3">
                                                        <asp:TextBox ID="txtEmpAdditionRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="500" Width="500px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnEmpAdditionInsert" runat="server" Text="確定" CssClass="button" onclick="btnEmpAdditionInsert_Click" />
                                            <asp:Button ID="btnEmpAdditionUpdate" runat="server" Text="確定"  CssClass="button" onclick="btnEmpAdditionUpdate_Click"/>
                                            <asp:Button ID="btnEmpAdditionCancel" runat="server" Text="取消"  CssClass="button" onclick="btnEmpAdditionCancel_Click"/>
                                            <asp:HiddenField ID="hdfEmpAdditionAddSeqNo" runat="server" />
                                            <asp:HiddenField ID="hdfEmpAdditionSeqNo" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                                <asp:GridView ID="gvEmpAddition" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                        CellPadding="3" Width="100%" DataKeyNames="Seq,AddSeq" onrowcommand="gvEmpAddition_RowCommand" onrowupdating="gvEmpAddition_RowUpdating" 
                                                        onrowdeleting="gvEmpAddition_RowDeleting" onrowdatabound="gvEmpAddition_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="動作" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnUpdate" runat="server" CausesValidation="false" CommandName="Update" ImageUrl="~/images/edit.gif" Text="編輯" ToolTip="編輯" />
                                            <asp:ImageButton ID="ibnDelete" runat="server" CausesValidation="false" OnClientClick="if (confirm('確定要刪除?') == false) return false;" CommandName="Delete" ImageUrl="~/images/delete.gif" Text="刪除" ToolTip="刪除" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="序號" DataField="AddSeq">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="序號" DataField="ShowAddSeqNo">
                                            <HeaderStyle Width="2%" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="狀態"  DataField="ReasonName">
                                            <HeaderStyle Width="5%" />
                                            <HeaderStyle Width="3%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="狀態"  DataField="Reason" >
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任公司"  DataField="AddCompName">
                                            <HeaderStyle Width="6%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任公司"  DataField="AddCompID">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任部門"  DataField="AddDeptName">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任部門"  DataField="AddDeptID">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任科組課"  DataField="AddOrganName">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="兼任科組課"  DataField="AddOrganID">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="簽核最小單位"  DataField="AddFlowOrganName">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="簽核最小單位"  DataField="AddFlowOrganID">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管任用方式"  DataField="BossTypeName">
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="主管任用方式"  DataField="BossType">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="IsBossValue" HeaderText="主管" InsertVisible="False" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="3%" />
                                        </asp:CheckBoxField>
                                        <asp:BoundField HeaderText="主管"  DataField="IsBoss">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="IsSecBossValue" HeaderText="副主管" InsertVisible="False" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="3%" />
                                        </asp:CheckBoxField>
                                        <asp:BoundField HeaderText="副主管"  DataField="IsSecBoss">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="IsGroupBossValue" HeaderText="簽核單位主管" InsertVisible="False" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="3%" />
                                        </asp:CheckBoxField>
                                        <asp:BoundField HeaderText="簽核單位主管"  DataField="IsGroupBoss">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CheckBoxField DataField="IsSecGroupBossValue" HeaderText="簽核單位副主管" InsertVisible="False" ReadOnly="True">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="3%" />
                                        </asp:CheckBoxField>
                                        <asp:BoundField HeaderText="簽核單位副主管"  DataField="IsSecGroupBoss">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="人令"  DataField="FileNo">
                                            <HeaderStyle Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="備註"  DataField="Remark">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ExistsEmpAddition"  DataField="ExistsEmpAddition">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Seq"  DataField="Seq">
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <sortedascendingcellstyle backcolor="#F1F1F1" />
                                    <sortedascendingheaderstyle backcolor="#007DBB" />
                                    <sorteddescendingcellstyle backcolor="#CAC9C9" />
                                    <sorteddescendingheaderstyle backcolor="#00547E" />
                                </asp:GridView>
                                <asp:Label ID="lblEmpAdditionWaitMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </form>
</body>
</html>
