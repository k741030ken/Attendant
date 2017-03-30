<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2201.aspx.vb" Inherits="OM_OM2201" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <style type="text/css">
        .style1
        {
            Font-Size:15px;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width:110px;
            Font-family:微軟正黑體;
        }
        .style2
        {
            Font-Size:15px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width:110px;
            Font-family:微軟正黑體;
        }
        
        .NoPic
        {
            color: Silver;
            font-size: 30px;
            font-family: Arial;
            font-weight: bolder;
        }
        .imgPhoto
        {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            margin: auto;
            max-height: 150px;
            max-width: 150px;
        }
                .style1
        {
            Font-Size:15px;
            height: 20px;
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width:110px;
        }
        .style2
        {
            Font-Size:15px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width:110px;
        }
    </style>
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function ActionVisable() {
            var btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnUpdate");
            btn.style.display = 'none';
            btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnActionC");
            btn.style.display = 'none';
            btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnCancel");
            btn.style.display = 'none';
            alert('存檔成功');
        }
        $(function () {
            $("#ucValidDateB_txtDate").change(function () {
                $("#ValidDateB").click();
            });
        });
       -->
    </script>
</head>
<body style="font-family:@微軟正黑體; margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table class="tbl_Edit" style="width: 80%; height: 100%" align="center" cellpadding="1"
        cellspacing="1">
        <tr style="height: 20px">
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblCompID" runat="server" Text="公司代碼" ></asp:Label>
            </td>
            <td class="style2" style="width: 35%" align="left">
                <asp:Label ID="txtCompID" runat="server" Text="txtCompID" ></asp:Label>
                <asp:HiddenField ID="hidCompID" runat="server" />
            </td>
            <td class="style1" style="width: 15%" align="center">
                <asp:Label ID="lblOrganReason" ForeColor="Blue" runat="server" Text="*異動原因" align="center" ></asp:Label>
            </td>
            <td class="style2" style="width: 35%" align="left">
                <asp:DropDownList      ID="ddlOrganReason" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                    <asp:ListItem Value="1" Text="1-組織新增"></asp:ListItem>
                    <asp:ListItem Value="2" Text="2-組織無效"></asp:ListItem>
                    <asp:ListItem Value="3" Text="3-組織異動"></asp:ListItem>
                </asp:DropDownList>
                <asp:HiddenField ID="hidOrganReason" runat="server" />
            </td>
        </tr>
        <tr style="height: 20px">
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lalOrganType" runat="server" Text="組織類型" ></asp:Label>
            </td>
            <td class="style2" style="width: 35%" align="left">
                <asp:Label ID="lblOrganType" runat="server" Text="" ></asp:Label>
                <asp:HiddenField ID="hidOrganType" runat="server" />
            </td>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblOrganID" runat="server" Text="部門代號" ></asp:Label>
            </td>
            <td class="style2" width="35%" align="left">
                <asp:Label ID="txtOrganID" runat="server" Text="txtOrganID" ></asp:Label>
                <asp:HiddenField ID="hidOrganID" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblValidDateB" ForeColor="Blue" runat="server" Text="*生效日期" ></asp:Label>
            </td>
            <td class="style2" style="width: 35%" align="left">
                <uc:ucCalender ID="ucValidDateB" runat="server" Enabled="True" />
                <asp:HiddenField ID="hidValidDateB" runat="server" />
                <asp:Button ID="ValidDateB" runat="server" Style="display: none" />
            </td>
            <td class="style1" width="15%" align="center">
                <asp:Label ID="lblValidDateE" runat="server" Text="生效迄日" ></asp:Label>
            </td>
            <td class="style2" style="width: 35%" align="left">
                <uc:ucCalender ID="ucValidDateE" runat="server" Enabled="True" />
            </td>
        </tr>
        <asp:Panel ID="pnlOld0" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="left" colspan="4">
                    <asp:Label ID="lblOld" runat="server" Text="異動前" ></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" align="center">
                    <asp:Label ID="lblOrganNameOld" ForeColor="Blue" runat="server" Text="*部門名稱" align="center" ></asp:Label>
                </td>
                <td class="style2" align="left">
                    <asp:TextBox ID="txtOrganNameOld" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"
                        Width="200px" ></asp:TextBox>
                </td>
                <td class="style1" align="center">
                </td>
                <td class="style2" align="left">
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblOrganEngNameOld" runat="server" Text="部門英文名稱" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtOrganEngNameOld" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="60" Width="200px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtOrganEngNameOld" runat="server" TargetControlID="txtOrganEngNameOld" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"  ValidChars=" ~!@#$%^&*()_+-;:/?<>[]{}">
                </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblInValidFlagOld" runat="server" Text="無效註記" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkInValidFlagOld" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblVirtualFlagOld" runat="server" Text="虛擬部門" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkVirtualFlagOld" runat="server" Text="" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBranchFlagOld" runat="server" Text="分行註記" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkBranchFlagOld" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblOrgTypeOld" ForeColor="Blue" runat="server" Text="*單位類別" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlOrgTypeOld" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblGroupIDOld" runat="server" ForeColor="Blue" Text="*所屬事業群" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlGroupIDOld" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblUpOrganIDOld" ForeColor="Blue" runat="server" Text="*上階部門" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlUpOrganIDOld" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblGroupTypeOld" runat="server" ForeColor="Blue" Text="*事業群類別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlGroupTypeOld" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblDeptIDOld" runat="server" ForeColor="Blue" Text="*所屬一級部門" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlDeptIDOld" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblRoleCodeOld" runat="server" ForeColor="Blue" Text="*部門主管角色" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlRoleCodeOld" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBossOld" runat="server" ForeColor="Blue" Text="*部門主管" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlBossCompIDOld" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBossOld" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                        Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtBossOld" runat="server" TargetControlID="txtBossOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblBossNameOld" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucQueryBossOld" runat="server" WindowHeight="800"
                        WindowWidth="600" ButtonText="..." />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBossTypeOld" runat="server" ForeColor="Blue" Text="*主管任用方式" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlBossTypeOld" runat="server">
                        <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                        <asp:ListItem Value="1" Text="主要"></asp:ListItem>
                        <asp:ListItem Value="2" Text="兼任"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblSecBossOld" runat="server" Text="副主管" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlSecBossCompIDOld" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSecBossOld" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                        Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecBossOld" runat="server" TargetControlID="txtSecBossOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblSecBossNameOld" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucSecQueryBossOld" runat="server" WindowHeight="800"
                        WindowWidth="600" ButtonText="..." />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBossTemporaryOld" runat="server" Text="主管暫代" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkBossTemporaryOld" runat="server" Text="" />
                </td>
            </tr>
        </asp:Panel>
        <%--=========================================================
                                                                        * 行政組織資料*
                                                                        * 行政組織資料*
                                                                        * 功能組織資料*
                        =========================================================--%>
        <asp:Panel ID="pnlOld1" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="center" colspan="4">
                    <asp:Label ID="lbloneOld" runat="server" Text="行政組織資料" ></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblPersonPartOld" runat="server" Text="人事管理員" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtPersonPartOld" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="6" Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtPersonPartOld" runat="server" TargetControlID="txtPersonPartOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblPersonPartNameOld" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucPersonPartOld" runat="server" ButtonText="..."
                        WindowHeight="800" WindowWidth="600" />
                    <br />
                    <asp:TextBox ID="txtSecPersonPartOld" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="6" Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecPersonPartOld" runat="server" TargetControlID="txtSecPersonPartOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblSecPersonPartNameOld" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucSecPersonPartOld" runat="server" ButtonText="..."
                        WindowHeight="800" WindowWidth="600" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblWorkSiteIDOld" ForeColor="Blue" runat="server" align="center" Text="*工作地點" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlWorkSiteIDOld" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCheckPartOld" runat="server" Text="自行查核主管" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtCheckPartOld" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                        Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtCheckPartOld" runat="server" TargetControlID="txtCheckPartOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblCheckPartNameOld" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucCheckPartOld" runat="server" ButtonText="..." WindowHeight="800"
                        WindowWidth="600" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblPositionIDOld" runat="server" Text="職位" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlPositionIDOld" runat="server" AutoPostBack="True" >
                    </asp:DropDownList>
                    <asp:Label ID="lblSelectPositionIDOld" runat="server" ForeColor="Blue" Text="" Style="display: none" ></asp:Label>
                    <uc:ButtonPosition ID="ucSelectPositionOld" runat="server" ButtonText="..." ButtonHint="選取"
                        WindowHeight="550" WindowWidth="1000" />
                    <%--ucPositionID改成ucSelectPosition--%>
                    <asp:HiddenField ID="hidPositionIDOld" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCostDeptIDOld" runat="server" Text="費用分攤部門" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlCostDeptIDOld" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblWorkTypeIDOld" ForeColor="Blue" runat="server" Text="*工作性質" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlWorkTypeIDOld" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="lblSelectWorkTypeIDOld" runat="server" ForeColor="Blue" Text="" Style="display: none" ></asp:Label>
                    <uc:ButtonWorkType ID="ucSelectWorkTypeOld" runat="server" ButtonText="..." ButtonHint="選取"
                        WindowHeight="550" WindowWidth="1000" />
                    <asp:HiddenField ID="hidWorkTypeIDOld" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblAccountBranchOld" runat="server" Text="會計分行別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtAccountBranchOld" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="3" Style="text-transform: uppercase" Width="80px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtAccountBranchOld" runat="server" TargetControlID="txtAccountBranchOld" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCostTypeOld" runat="server" Text="費用分攤科目別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlCostTypeOld" runat="server">
                        <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                        <asp:ListItem Value="A" Text="管理-5821"></asp:ListItem>
                        <asp:ListItem Value="B" Text="業務-5811"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </asp:Panel>
        <%--=========================================================
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                        =========================================================--%>
        <asp:Panel ID="pnlOld2" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="center" colspan="4">
                    <asp:Label ID="lbltwoOld" runat="server" Text="功能組織資料" ></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblFlowOrganIDOld" runat="server" Text="比對簽核單位" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlFlowOrganIDOld" runat="server">
                    </asp:DropDownList>
                    <uc:ButtonFlowOrgan ID="ucFlowOrganOld" runat="server" />
                    <asp:HiddenField ID="hidFlowOrganOld" runat="server" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCompareFlagOld" runat="server" Text="HR內部比對單位註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkCompareFlagOld" runat="server" Text="" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblDelegateFlagOld" runat="server" Text="授權註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkDelegateFlagOld" runat="server" Text="" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblOrganNoOld" runat="server" Text="處級單位註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkOrganNoOld" runat="server" Text="" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBusinessTypeOld" runat="server" Text="業務類別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <%--<td class="style2" width="35%" align="left" colspan="3">--%>
                    <asp:DropDownList     ID="ddlBusinessTypeOld" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    &nbsp;
                </td>
                <td class="style2" width="35%" align="left">
                    &nbsp;
                </td>
            </tr>
        </asp:Panel>
        <%--=========================================================
                                                                        * 異動後*
                                                                        * 異動後*
                                                                        * 異動後*
                        =========================================================--%>
        <asp:Panel ID="PnlNew0" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="left" colspan="4">
                    <asp:Label ID="lblNew" runat="server" Text="異動後" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1">
                    <asp:Label ID="lblOrganName" runat="server" align="center" ForeColor="Blue" Text="*部門名稱" ></asp:Label>
                </td>
                <td align="left" class="style2">
                    <asp:TextBox ID="txtOrganName" runat="server" CssClass="InputTextStyle_Thin" MaxLength="30"
                        Width="200px" ></asp:TextBox>
                </td>
                <td align="center" class="style1">
                </td>
                <td align="left" class="style2">
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblOrganEngName" runat="server" Text="部門英文名稱" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:TextBox ID="txtOrganEngName" runat="server" CssClass="InputTextStyle_Thin" MaxLength="60"
                        Width="200px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtOrganEngName" runat="server" TargetControlID="txtOrganEngName" FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters"  ValidChars=" ~!@#$%^&*()_+-;:/?<>[]{}">
                </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblInValidFlag" runat="server" Text="無效註記" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:CheckBox ID="chkInValidFlag" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblVirtualFlag" runat="server" Text="虛擬部門" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:CheckBox ID="chkVirtualFlag" runat="server" Text="" />
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblBranchFlag" runat="server" Text="分行註記" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:CheckBox ID="chkBranchFlag" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblOrgType" runat="server" ForeColor="Blue" Text="*單位類別" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlOrgType" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblGroupID" runat="server" ForeColor="Blue" Text="*所屬事業群" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlGroupID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblUpOrganID" runat="server" align="center" ForeColor="Blue" Text="*上階部門" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlUpOrganID" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblGroupType" runat="server" align="center" ForeColor="Blue" Text="*事業群類別" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlGroupType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblDeptID" runat="server" align="center" ForeColor="Blue" Text="*所屬一級部門" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlDeptID" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblRoleCode" runat="server" align="center" ForeColor="Blue" Text="*部門主管角色" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlRoleCode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblBoss" runat="server" align="center" ForeColor="Blue" Text="*部門主管" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlBossCompID" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtBoss" runat="server" AutoPostBack="True" CssClass="InputTextStyle_Thin"
                        MaxLength="6" Style="text-transform: uppercase" Width="80px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtBoss" runat="server" TargetControlID="txtBoss" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblBossName" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucQueryBoss" runat="server" ButtonText="..." WindowHeight="800"
                        WindowWidth="600" />
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblBossType" runat="server" align="center" ForeColor="Blue" Text="*主管任用方式" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlBossType" runat="server">
                        <asp:ListItem Text="---請選擇---" Value=""></asp:ListItem>
                        <asp:ListItem Text="主要" Value="1"></asp:ListItem>
                        <asp:ListItem Text="兼任" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblSecBoss" runat="server" align="center" Text="副主管" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:DropDownList     ID="ddlSecBossCompID" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSecBoss" runat="server" AutoPostBack="True" CssClass="InputTextStyle_Thin"
                        MaxLength="6" Style="text-transform: uppercase" Width="80px" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecBoss" runat="server" TargetControlID="txtSecBoss" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblSecBossName" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucSecQueryBoss" runat="server" ButtonText="..." WindowHeight="800"
                        WindowWidth="600" />
                </td>
                <td align="center" class="style1" width="15%">
                    <asp:Label ID="lblBossTemporary" runat="server" align="center" Text="主管暫代" ></asp:Label>
                </td>
                <td align="left" class="style2" width="35%">
                    <asp:CheckBox ID="chkBossTemporary" runat="server" Text="" />
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlNew1" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="center" colspan="4">
                    <asp:Label ID="lblNew1" runat="server" Text="行政組織資料" ></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblPersonPart" runat="server" Text="人事管理員" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtPersonPart" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                        Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtPersonPart" runat="server" TargetControlID="txtPersonPart" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblPersonPartName" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucPersonPart" runat="server" ButtonText="..." WindowHeight="800"
                        WindowWidth="600" />
                    <br />
                    <asp:TextBox ID="txtSecPersonPart" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="6" Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecPersonPart" runat="server" TargetControlID="txtSecPersonPart" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblsecPersonPartName" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucSecPersonPart" runat="server" ButtonText="..."
                        WindowHeight="800" WindowWidth="600" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblWorkSiteID" ForeColor="Blue" runat="server" align="center" Text="*工作地點" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlWorkSiteID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCheckPart" runat="server" Text="自行查核主管" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtCheckPart" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                        Style="text-transform: uppercase" Width="80px" AutoPostBack="True" ></asp:TextBox>
                         <ajaxToolkit:FilteredTextBoxExtender ID="ftxtCheckPart" runat="server" TargetControlID="txtCheckPart" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                    <asp:Label ID="lblCheckPartName" runat="server" ></asp:Label>
                    <uc:ButtonQuerySelectUserID ID="ucCheckPart" runat="server" ButtonText="..." WindowHeight="800"
                        WindowWidth="600" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblPositionID" runat="server" Text="職位" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlPositionID" runat="server" AutoPostBack="True" >
                    </asp:DropDownList>
                    <uc:ButtonPosition ID="ucSelectPosition" runat="server" ButtonHint="選取" ButtonText="..."
                        WindowHeight="550" WindowWidth="1000" />
                    <asp:Label ID="lblSelectPositionID" runat="server" ForeColor="Blue" Text="" Style="display: none" ></asp:Label>
                    <%--ucPositionID改成ucSelectPosition--%>
                    <asp:HiddenField ID="hidPositionID" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCostDeptID" runat="server" Text="費用分攤部門" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlCostDeptID" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblWorkTypeID" ForeColor="Blue" runat="server" Text="*工作性質" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlWorkTypeID" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <uc:ButtonWorkType ID="ucSelectWorkType" runat="server" ButtonHint="選取" ButtonText="..."
                        WindowHeight="550" WindowWidth="1000" />
                    <asp:Label ID="lblSelectWorkTypeID" runat="server" ForeColor="Blue" Text="" Style="display: none" ></asp:Label>
                    <asp:HiddenField ID="hidWorkTypeID" runat="server" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblAccountBranch" runat="server" Text="會計分行別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:TextBox ID="txtAccountBranch" CssClass="InputTextStyle_Thin" runat="server"
                        MaxLength="3" Style="text-transform: uppercase" Width="80px" ></asp:TextBox>
                        <ajaxToolkit:FilteredTextBoxExtender ID="ftxtAccountBranch" runat="server" TargetControlID="txtAccountBranch" FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                </ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCostType" runat="server" Text="費用分攤科目別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlCostType" runat="server">
                        <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                        <asp:ListItem Value="A" Text="管理-5821"></asp:ListItem>
                        <asp:ListItem Value="B" Text="業務-5811"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </asp:Panel>
        <%--=========================================================
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                        =========================================================--%>
        <asp:Panel ID="pnlNew2" runat="server">
            <tr style="height: 35px">
                <td class="style1" width="15%" align="center" colspan="4">
                    <asp:Label ID="lblNew2" runat="server" Text="功能組織資料" ></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblFlowOrganID" runat="server" Text="比對簽核單位" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:DropDownList     ID="ddlFlowOrganID" runat="server">
                    </asp:DropDownList>
                    <uc:ButtonFlowOrgan ID="ucFlowOrgan" runat="server" />
                    <asp:HiddenField ID="hidFlowOrgan" runat="server" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblCompareFlag" runat="server" Text="HR內部比對單位註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkCompareFlag" runat="server" Text="" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblDelegateFlag" runat="server" Text="授權註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkDelegateFlag" runat="server" Text="" />
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblOrganNo" runat="server" Text="處級單位註記" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <asp:CheckBox ID="chkOrganNo" runat="server" Text="" />
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblBusinessType" runat="server" Text="業務類別" align="center" ></asp:Label>
                </td>
                <td class="style2" width="35%" align="left">
                    <%--<td class="style2" width="35%" align="left" colspan="3">--%>
                    <asp:DropDownList     ID="ddlBusinessType" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="style1" width="15%" align="center">
                    &nbsp;
                </td>
                <td class="style2" width="35%" align="left">
                    &nbsp;
                </td>
            </tr>
        </asp:Panel>
    </table>
    </form>
</body>
</html>
