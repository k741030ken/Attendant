<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM1002.aspx.vb" Inherits="OM_OM1002" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function ActionVisable() {
            var btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnActionC");
            btn.style.display = 'none';
            btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnUpdate");
            btn.style.display = 'none';
            btn = window.parent.frames[0].document.getElementById("ucButtonPermission_btnCancel");
            btn.style.display = 'none';
            alert('修改成功');
        }
        function funAction(Param) {
            switch (Param) {
                case "btnUpdate":
                case "btnActionC":
                    if (!confirm('是否儲存此筆資料？'))
                        return false;
                    break;
            }
        }
//        $(function () {
//            $("#ucValidDate_txtDate").change(function () {
//                $("#btnValidDate").click();
//            });
//        });
    -->
    </script>
    <style type="text/css">
        .PhotoPanel
        {
        	width: 150px;
        	height: 150px;
        	text-align: center;
        	line-height: 150px;
        	vertical-align: middle;
        	position: relative;
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
        .NoPic
        {
            color: Silver;
            font-size: 30px;
            font-family: Arial;
            font-weight: bolder;
            Font-family:微軟正黑體;
        }
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
        
    </style>
</head>
<body style="font-family:@微軟正黑體; margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:UpdatePanel ID="updBody" runat="server" >
    <ContentTemplate>
    <table class="tbl_Edit" style="width: 80%; height: 100%" align="center" cellpadding="1"
        cellspacing="1">
        <tr>
            <td>
                <table id="Panel0" runat="server" width="100%">
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblCompID" ForeColor="Blue" runat="server" Text="*公司代碼"></asp:Label>
                        </td>
                        <td class="style2" style="width: 40%" align="left">
                            <asp:Label ID="txtCompID" runat="server" Text="txtCompID"></asp:Label>
                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="hidSeq" runat="server" />
                            <asp:HiddenField ID="hidSeqNew" runat="server" />
                        </td>
                        <td class="style1" style="width: 15%" align="center">
                            <asp:Label ID="lblOrganReason" ForeColor="Blue" runat="server" Text="*異動原因" align="center"></asp:Label>
                        </td>
                        <td class="style2" style="width: 40%" align="left">
                            <asp:DropDownList ID="ddlOrganReason" runat="server" AutoPostBack="True">
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
                            <asp:Label ID="lalOrganType" ForeColor="Blue" runat="server" Text="*待異動組織類型"></asp:Label>
                        </td>
                        <td class="style2" style="width: 40%" align="left">
                            <asp:DropDownList ID="ddlOrganType" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1-行政組織"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2-功能組織"></asp:ListItem>
                                <asp:ListItem Value="3" Text="3-行政與功能組織"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidOrganType" runat="server" />
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblRecID" ForeColor="Blue" runat="server" Text="*生效日期"></asp:Label>
                        </td>
                        <td class="style2" style="width: 40%" align="left">
                            <asp:UpdatePanel ID="updValidDate" runat="server">
                            <ContentTemplate>
                            <uc:ucCalender ID="ucValidDate" runat="server" Enabled="True" />
                            <asp:HiddenField ID="hidValidDate" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                            <asp:PostBackTrigger ControlID ="ucValidDate" />
                            </Triggers>
                            </asp:UpdatePanel>
                            
                            <%--<asp:Button ID="btnValidDate" runat="server" style="display:none" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblOrganID" ForeColor="Blue" runat="server" Text="*部門代號"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <%--            <asp:TextBox ID="TextBox1" runat="server" MaxLength="12" CssClass="InputTextStyle_Thin"
                    Style="text-transform: uppercase" Width="200px" AutoPostBack="True" ></asp:TextBox>--%>
                            <asp:Label ID="txtOrganID" runat="server" Text="txtOrganID"></asp:Label>
                            <%--                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                            <%--                </ContentTemplate>
                </asp:UpdatePanel>--%>
                        </td>
                        <td class="style1" align="center">
                            <asp:Label ID="lblOrganName" ForeColor="Blue" runat="server" Text="*部門名稱" align="center"></asp:Label>
                        </td>
                        <td class="style2" align="left">
                            <asp:TextBox ID="txtOrganName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"
                                Width="200px"></asp:TextBox>
                            <asp:HiddenField ID="hidOrganNameOld" runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblOrganEngName" runat="server" Text="部門英文名稱"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:TextBox ID="txtOrganEngName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="60"
                                Width="200px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtOrganEngName" runat="server" TargetControlID="txtOrganEngName"
                                FilterType="Custom,Numbers,UppercaseLetters,LowercaseLetters" ValidChars=" ~!@#$^&*()+-;:/?<>[]{}`">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <%--  <asp:TextBox ID="TextBox1" CssClass="InputTextStyle_Thin" runat="server" MaxLength="60"
                                    Style="text-transform: uppercase" width="200px"></asp:TextBox>--%>
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblInValidFlag" runat="server" Text="無效註記"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkInValidFlag" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblVirtualFlag" runat="server" Text="虛擬部門"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkVirtualFlag" runat="server" Text="" />
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblBranchFlag" runat="server" Text="分行註記"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkBranchFlag" runat="server" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblOrgType" ForeColor="Blue" runat="server" Text="*單位類別"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlOrgType" runat="server">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblGroupID" runat="server" ForeColor="Blue" Text="*所屬事業群"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlGroupID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblUpOrganID" ForeColor="Blue" runat="server" Text="*上階部門" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlUpOrganID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblGroupType" runat="server" ForeColor="Blue" Text="*事業群類別" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlGroupType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblDeptID" runat="server" ForeColor="Blue" Text="*所屬一級部門" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlDeptID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblRoleCode" runat="server" ForeColor="Blue" Text="*部門主管角色" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlRoleCode" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblBoss" runat="server" ForeColor="Blue" Text="*部門主管" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:UpdatePanel ID="updBossCompID" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                             <asp:DropDownList ID="ddlBossCompID" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtBoss" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                                Style="text-transform: uppercase" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtBoss" runat="server" TargetControlID="txtBoss"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <br />
                            <uc:ButtonQuerySelectUserID ID="ucQueryBoss" runat="server" WindowHeight="800" WindowWidth="600"
                                ButtonText="..." />
                            <asp:Label ID="lblBossName" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                            <asp:PostBackTrigger  ControlID="ucQueryBoss"/>
                            </Triggers>
                            </asp:UpdatePanel>
                           
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblBossType" runat="server" ForeColor="Blue" Text="*主管任用方式" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlBossType" runat="server">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="主要"></asp:ListItem>
                                <asp:ListItem Value="2" Text="兼任"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblSecBoss" runat="server" Text="副主管" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:UpdatePanel ID="updSecBossCompID" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:DropDownList ID="ddlSecBossCompID" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSecBoss" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                                Style="text-transform: uppercase" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecBoss" runat="server" TargetControlID="txtSecBoss"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <br />
                            <uc:ButtonQuerySelectUserID ID="ucSecQueryBoss" runat="server" WindowHeight="800"
                                WindowWidth="600" ButtonText="..." />
                            <asp:Label ID="lblSecBossName" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                            <asp:PostBackTrigger  ControlID="ucSecQueryBoss"/>
                            </Triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblBossTemporary" runat="server" Text="主管暫代" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkBossTemporary" runat="server" Text="" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--=========================================================
                                                                        * 行政組織資料*
                                                                        * 行政組織資料*
                                                                        * 功能組織資料*
                        =========================================================--%>
        <tr>
            <td>
                <table align="center" runat="server" id="Panel1" width="100%">
                    <tr style="height: 35px">
                        <td class="style1" width="15%" align="center" colspan="4">
                            <asp:Label ID="lblone" runat="server" Text="行政組織資料"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblPersonPart" runat="server" Text="人事管理員" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:UpdatePanel ID="updPersonPart" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:TextBox ID="txtPersonPart" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                                Style="text-transform: uppercase" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtPersonPart" runat="server" TargetControlID="txtPersonPart"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <uc:ButtonQuerySelectUserID ID="ucPersonPart" runat="server" ButtonText="..." WindowHeight="800"
                                WindowWidth="600" />
                            <asp:Label ID="lblPersonPartName" runat="server"></asp:Label>
                                                       </ContentTemplate>
                            <Triggers>
                            <asp:PostBackTrigger  ControlID="ucPersonPart"/>
                            </Triggers>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="updSecPersonPart" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            <asp:TextBox ID="txtSecPersonPart" CssClass="InputTextStyle_Thin" runat="server"
                                MaxLength="6" Style="text-transform: uppercase" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtSecPersonPart" runat="server" TargetControlID="txtSecPersonPart"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <uc:ButtonQuerySelectUserID ID="ucSecPersonPart" runat="server" ButtonText="..."
                                WindowHeight="800" WindowWidth="600" />
                            <asp:Label ID="lblSecPersonPartName" runat="server"></asp:Label>
                                                                                       </ContentTemplate>
                            <Triggers>
                            <asp:PostBackTrigger  ControlID="ucSecPersonPart"/>
                            </Triggers>
                            </asp:UpdatePanel>
                            
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblWorkSiteID" ForeColor="Blue" runat="server" align="center" Text="*工作地點"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlWorkSiteID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="Label25" runat="server" Text="自行查核主管" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                        <asp:UpdatePanel ID="updCheckPart" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <asp:TextBox ID="txtCheckPart" CssClass="InputTextStyle_Thin" runat="server" MaxLength="6"
                                Style="text-transform: uppercase" Width="80px" AutoPostBack="True"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtCheckPart" runat="server" TargetControlID="txtCheckPart"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                            <uc:ButtonQuerySelectUserID ID="ucCheckPart" runat="server" ButtonText="..." WindowHeight="800"
                                WindowWidth="600" />
                            <asp:Label ID="lblCheckPart" runat="server"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ucCheckPart" />
            </Triggers>
        </asp:UpdatePanel>
                            
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblPositionID" runat="server" Text="職位" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                        <asp:UpdatePanel ID="updPositionID" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
             <asp:DropDownList ID="ddlPositionID" runat="server" AutoPostBack="True" >
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="lblSelectPositionID" runat="server" ForeColor="Blue" Text="" Style="display: none"></asp:Label>
                            <uc:ButtonPosition ID="ucSelectPosition" runat="server" ButtonText="..." ButtonHint="選取"
                                WindowHeight="550" WindowWidth="1000" />
                            <%--ucPositionID改成ucSelectPosition--%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="ucSelectPosition"/>
                <%--<asp:AsyncPostBackTrigger ControlID ="ddlPositionID" />--%>
            </Triggers>
        </asp:UpdatePanel>
                           
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblCostDeptID" runat="server" Text="費用分攤部門" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlCostDeptID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblWorkTypeID" ForeColor="Blue" runat="server" Text="*工作性質" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                        <asp:UpdatePanel ID="updWorkTypeID" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <asp:DropDownList ID="ddlWorkTypeID" runat="server"  AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:Label ID="lblSelectWorkTypeID" runat="server" ForeColor="Blue" Text="" Style="display: none"></asp:Label>
                            <br />
                            <uc:ButtonWorkType ID="ucSelectWorkType" runat="server" ButtonText="..." ButtonHint="選取"
                                WindowHeight="550" WindowWidth="1000" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="ucSelectWorkType"/>
            </Triggers>
        </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblAccountBranch" runat="server" Text="會計分行別" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:TextBox ID="txtAccountBranch" CssClass="InputTextStyle_Thin" runat="server"
                                MaxLength="3" Style="text-transform: uppercase" Width="80px"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="ftxtAccountBranch" runat="server" TargetControlID="txtAccountBranch"
                                FilterType="Numbers,UppercaseLetters,LowercaseLetters">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblCostType" runat="server" Text="費用分攤科目別" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlCostType" runat="server">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="A" Text="管理-5821"></asp:ListItem>
                                <asp:ListItem Value="B" Text="業務-5811"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--=========================================================
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                                                                        * 功能組織資料*
                        =========================================================--%>
        <tr>
            <td>
                <table align="center" runat="server" id="Panel2" width="100%">
                    <tr style="height: 35px">
                        <td class="style1" width="15%" align="center" colspan="4">
                            <asp:Label ID="lbltwo" runat="server" Text="功能組織資料"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblFlowOrganID" runat="server" Text="比對簽核單位" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                        <asp:UpdatePanel ID="updFlowOrganID" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
            <asp:DropDownList ID="ddlFlowOrganID" runat="server">
                            </asp:DropDownList>
                            <uc:ButtonFlowOrgan ID="ucFlowOrgan" runat="server" />
                            <asp:HiddenField ID="hidFlowOrgan" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger  ControlID="ucFlowOrgan"/>
            </Triggers>
        </asp:UpdatePanel>
                            
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="CompareFlag" runat="server" Text="HR內部比對單位註記" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkCompareFlag" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblDelegateFlag" runat="server" Text="授權註記" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkDelegateFlag" runat="server" Text="" />
                        </td>
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="lblOrganNo" runat="server" Text="處級單位註記" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:CheckBox ID="chkOrganNo" runat="server" Text="" />
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td class="style1" width="15%" align="center">
                            <asp:Label ID="Label32" runat="server" Text="業務類別" align="center"></asp:Label>
                        </td>
                        <td class="style2" width="40%" align="left">
                            <asp:DropDownList ID="ddlBusinessType" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style1" width="15%" align="center">
                            &nbsp;
                        </td>
                        <td class="style2" width="40%" align="left">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" runat="server" id="Panel3" width="100%">
            <%--                        <tr style="height: 35px">
                            <td class="style1" width="15%" align="center" colspan="4">
                                <asp:Label ID="Label1" runat="server" Text="最後異動"></asp:Label>
                            </td>
                        </tr>--%>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblLastChgComp" runat="server" Text="最後異動公司" align="center"></asp:Label>
                </td>
                <td class="style2" width="40%" align="left">
                    <asp:Label ID="txtLastChgComp" runat="server" Text="LastChgComp"></asp:Label>
                </td>
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblLastChgID" runat="server" Text="最後異動者" align="center"></asp:Label>
                </td>
                <td class="style2" width="40%" align="left">
                    <asp:Label ID="txtLastChgID" runat="server" Text="LastChgID"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style1" width="15%" align="center">
                    <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動日期" align="center"></asp:Label>
                </td>
                <td class="style2" width="40%" align="left">
                    <asp:Label ID="txtLastChgDate" runat="server" Text="LastChgDate"></asp:Label>
                </td>
                <td class="style1" width="15%" align="center">
                    &nbsp;
                </td>
                <td class="style2" width="40%" align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
            </td>
        </tr>
        <%--  /*===========照片====================*/--%>
        <tr>
            <td>
                <table align="center" runat="server" id="Panel4">
                    <tr>
                        <td align="center" class="style1" width="40%">
                            <asp:Label ID="UpOrganName" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="UpTitle" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="UpRank" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="UpBossID" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="UpBossName" runat="server" Text="" align="center"></asp:Label>
                            <asp:Panel ID="pnlPhoto" runat="server" CssClass="PhotoPanel">
                                <asp:Label ID="lblPhoto_NoPic" CssClass="NoPic" runat="server" Text="NoPicture"></asp:Label>
                                <asp:Image ID="imgPhoto" CssClass="imgPhoto" Visible="false" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Center" runat="server" Text="|" align="center" ForeColor="#5384e6"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="style1" width="40%">
                            <asp:Label ID="OrganName" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="Title" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="Rank" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="BossID" runat="server" Text="" align="center"></asp:Label>
                            <br />
                            <asp:Label ID="BossName" runat="server" Text="" align="center"></asp:Label>
                            <asp:Panel ID="pnlPhoto2" runat="server" CssClass="PhotoPanel">
                                <asp:Label ID="lblPhoto_NoPic2" runat="server" CssClass="NoPic" Text="NoPicture"></asp:Label>
                                <asp:Image ID="imgPhoto2" CssClass="imgPhoto" runat="server" Visible="false" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    <Triggers>
    <%--<asp:PostBackTrigger ControlID ="ucSelectOrgan" />--%>
    <%--<asp:PostBackTrigger ControlID ="ucValidDate" />--%>
    <%--<asp:PostBackTrigger ControlID ="ucQueryBoss" />--%>
    <%--<asp:PostBackTrigger ControlID ="ucSecQueryBoss" />
    <asp:PostBackTrigger ControlID ="ucPersonPart" />
    <asp:PostBackTrigger ControlID ="ucSecPersonPart" />
    <asp:PostBackTrigger ControlID ="ucCheckPart" />
    <asp:PostBackTrigger ControlID ="ucSelectPosition" />
    <asp:PostBackTrigger ControlID ="ucSelectWorkType" />
    <asp:PostBackTrigger ControlID ="ucFlowOrgan" />--%>
    </Triggers>
    </asp:UpdatePanel>
    
    </form>
</body>
</html>
