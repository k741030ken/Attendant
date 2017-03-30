<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2100.aspx.vb" Inherits="OM_OM2100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <style type="text/css">
        .backcolor
        {
            background-color:#99CCCC;
            font-size:16px;
            font-weight:bold;
            height:30px;
        }
    </style>
    <script type="text/javascript">
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                    if (!confirm('確定修改資料？'))
                        return false;
                    break;
            }
        }
        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex)
                { }
            }
        }
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table width="80%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblCompID"  runat="server" Text="公司代碼" align="center" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtCompID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidCompID" runat="server" Visible="False"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganType" ForeColor="blue"  runat="server" Text="*組織類型" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtOrganType"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidOrganType" runat="server" Visible="False"></asp:HiddenField>
                                <asp:HiddenField ID="hidOrganReason" runat="server" Visible="False"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganID" ForeColor="blue"  runat="server" Text="*部門代碼" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtOrganID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidOrganID" runat="server" Visible="False"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganName" ForeColor="blue"  runat="server" Text="*部門名稱" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtOrganName"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganEngName"  runat="server" Text="部門英文名稱" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtOrganEngName"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblInValidFlag"  runat="server" Text="無效註記" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkInValidFlag" runat="server" disabled="disabled" />
                                <asp:HiddenField ID="hidInValidFlag" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblVirtualFlag"  runat="server" Text="虛擬部門" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkVirtualFlag" runat="server" disabled="disabled" />
                                <asp:HiddenField ID="hidVirtualFlag" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" style="text-align:center">
                                <asp:Label ID="lblBranchFlag"  runat="server" Text="分行註記" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkBranchFlag" runat="server" disabled="disabled" />
                                <asp:HiddenField ID="hidBranchFlag" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrgType" ForeColor="blue"  runat="server" Text="*單位類別" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtOrgType"  runat="server" style="text-align:center" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidOrgType" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblGroupID" ForeColor="blue"  runat="server" Text="*所屬事業群" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtGroupID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidGroupID" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblUpOrganID" ForeColor="blue"  runat="server" Text="*上階部門" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtUpOrganID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidUpOrganID" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblGroupType" ForeColor="blue"  runat="server" Text="*事業群類別" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtGroupType"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidGroupType" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblDeptID" ForeColor="blue"  runat="server" Text="*所屬一級部門" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtDeptID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidDeptID" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblRoleCode" ForeColor="blue"  runat="server" Text="*部門主管角色" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtRoleCode"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidRoleCode" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblBoss" ForeColor="blue"  runat="server" Text="*部門主管" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtBoss"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidBossCompID" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidBoss" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblBossType" ForeColor="blue"  runat="server" Text="*主管任用方式" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtBossType"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidBossType" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader"width="15%" style="text-align:center">
                                <asp:Label ID="lblSecBoss"  runat="server" Text="副主管" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:Label ID="txtSecBoss"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidSecBossCompID" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidSecBoss" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblBossTemporary"  runat="server" Text="主管暫代" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkBossTemporary" runat="server" disabled="disabled" />
                                <asp:HiddenField ID="hidBossTemporary" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel1" runat="server">
                        <tr style="height:35px">
                            <td class="td_EditHeader" width="15%" align="center" colspan="4">
                                <asp:Label ID="lblone" runat="server" Text="行政組織資料" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblPersonPart"  runat="server" Text="人事管理員" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftxtPersonPart" runat="server" TargetControlID="txtPersonPart" FilterType="Numbers,UppercaseLetters,LowercaseLetters"></ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtPersonPart" runat="server" MaxLength='6' AutoPostBack="True" Font-Names="微軟正黑體"></asp:TextBox>
                                <asp:Label ID="lblPersonPartName"  runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucPersonPart" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <br />
                                <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecPersonPart" runat="server" TargetControlID="txtSecPersonPart" FilterType="Numbers,UppercaseLetters,LowercaseLetters"></ajaxToolkit:FilteredTextBoxExtender>
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtSecPersonPart" runat="server" MaxLength='6' AutoPostBack="True" Font-Names="微軟正黑體"></asp:TextBox>
                                <asp:Label ID="lblSecPersonPartName"  runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucSecPersonPart" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:HiddenField ID="hidPersonPart_Old" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidSecPersonPart_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblWorkSiteID" ForeColor="blue"  runat="server" Text="*工作地點" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlWorkSiteID" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hidWorkSiteID_Old" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblCheckPart"  runat="server" Text="自行查核主管" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit"" align="left" width="35%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtCheckPart" runat="server" MaxLength='6' AutoPostBack="True" Font-Names="微軟正黑體"></asp:TextBox>
                                <asp:Label ID="lblCheckPartName"  runat="server" Text="" Font-Names="微軟正黑體"></asp:Label>
                                <uc:ButtonQuerySelectUserID ID="ucCheckPart" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:HiddenField ID="hidCheckPart_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblPositionID"  runat="server" Text="職位" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlPositionID" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblSelectPositionID" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                <asp:Label ID="lblSelectPositionName" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidPositionID" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidPositionID_Old" runat="server" />
                                <uc:ButtonPosition ID="ucSelectPosition" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblCostDeptID"  runat="server" Text="費用分攤部門" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlCostDeptID" runat="server" Font-Names="微軟正黑體">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hidCostDeptID_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblWorkTypeID" ForeColor="blue"  runat="server" Text="*工作性質" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlWorkTypeID" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblSelectWorkTypeID" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                            <asp:Label ID="lblSelectWorkTypeName" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                            <asp:HiddenField ID="hidWorkTypeID" runat="server" />
                                            <asp:HiddenField ID="hidWorkTypeID_Old" runat="server" />
                                <uc:ButtonWorkType ID="ucSelectWorkType" runat="server" 
                                    CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblAccountBranch"  runat="server" Text="會計分行別" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtAccountBranch" runat="server" MaxLength='3' Font-Names="微軟正黑體"></asp:TextBox>
                                <asp:HiddenField ID="hidAccountBranch_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                                <asp:Label ID="lblCostType"  runat="server" Text="費用分攤科目別" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlCostType" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="管理"></asp:ListItem>
                                    <asp:ListItem Value="B" Text="業務"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hidCostType_Old" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server">
                        <tr style="height: 35px">
                            <td align="center" class="td_EditHeader" colspan="4" width="15%">
                                <asp:Label ID="lbltwo" runat="server" Text="功能組織資料" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblFlowOrganID"  runat="server" Text="比對簽核單位" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                            <asp:DropDownList ID="ddlFlowOrganID" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblSelectFlowOrganID" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                <asp:Label ID="lblSelectFlowOrganName" runat="server" ForeColor="Blue" Text="" style="display:none" Font-Names="微軟正黑體"></asp:Label>
                                <asp:HiddenField ID="hidFlowOrganID" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidFlowOrganID_Old" runat="server" />
                                <uc:ButtonFlowOrgan ID="ucFlowOrgan" runat="server" />
                                <asp:HiddenField ID="hidFlowOrgan" runat="server" />
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblCompareFlag"  runat="server" Text="HR內部比對單位註記" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkCompareFlag" runat="server" />
                                <asp:HiddenField ID="hidCompareFlag" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidCompareFlag_Old" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblDelegateFlag"  runat="server" Text="授權單位" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkDelegateFlag" runat="server" />
                                <asp:HiddenField ID="hidDelegateFlag" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidDelegateFlag_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblOrganNo"  runat="server" Text="處級單位註記" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:CheckBox ID="chkOrganNo" runat="server" />
                                <asp:HiddenField ID="hidOrganNo" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidOrganNo_Old" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" align="left" width="15%" style="text-align:center">
                                <asp:Label ID="lblBusinessType"  runat="server" Text="業務類別" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hidBusinessType" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidBusinessType_Old" runat="server"></asp:HiddenField>
                            </td>
                            <td class="td_EditHeader" width="15%" style="text-align:center">
                            </td>
                            <td class="td_Edit" align="left" width="35%">
                            </td>
                        </tr>
                        </asp:Panel>
                        <tr style="height:20px">
                        <td class="td_EditHeader" width="15%" style="text-align:center">
                            <asp:Label ID="lblLastChgComp"  runat="server" Text="最後異動人員公司" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td class="td_Edit" align="left" width="35%">
                            <asp:Label ID="txtLastChgComp"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td class="td_EditHeader" width="15%" style="text-align:center">
                            <asp:Label ID="lblLastChgID"  runat="server" Text="最後異動人員" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td class="td_Edit" align="left" width="35%">
                            <asp:Label ID="txtLastChgID"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td class="td_EditHeader"width="15%" style="text-align:center">
                            <asp:Label ID="lblLastChgDate"  runat="server" Text="最後異動時間" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td class="td_Edit" align="left" width="35%">
                            <asp:Label ID="txtLastChgDate"  runat="server" Font-Names="微軟正黑體"></asp:Label>
                        </td>
                        <td class="td_EditHeader" width="15%" style="text-align:center">
                        </td>
                        <td class="td_Edit" align="left" width="35%">
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
