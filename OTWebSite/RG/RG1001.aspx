<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RG1001.aspx.vb" Inherits="RG_RG1001" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        $(function () {
            $("#txtIDNo").change(function () {
                $("#btnIDNo").click();
            });
            $("#txtEmpDate_txtDate").change(function () {
                $("#btnValidDate").click();
            });
        });

        function confirmAdd() {
            var msg = ""
            if ($("#ddlIDType").val() == "1") {
                msg = '「身分證字號邏輯有誤，是否重新輸入？【確定】表示重新輸入，【取消】表示忽略且同意修改」'
            }
            else if ($("#ddlIDType").val() == "2") {
                msg = '「居留證字號邏輯有誤，是否重新輸入？【確定】表示重新輸入，【取消】表示忽略且同意修改」'
            }

            if (confirm(msg)) {
                document.getElementById('btnYes').click();
            }
            else {
                document.getElementById('btnNo').click();
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
                                            <asp:Label ID="lblCompID" runat="server" Text="公司代碼"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left" colspan="3">                                
                                            <asp:Label ID="txtCompID" runat="server" ></asp:Label>
                                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr> 
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpID" ForeColor="Blue" runat="server" Text="*員工編號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:TextBox ID="txtEmpID" CssClass="InputTextStyle_Thin" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                        </td>                    
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRecID" runat="server" Text="應試者編號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" style="width:35%" align="left">
                                            <asp:TextBox ID="txtRecID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20" ReadOnly="true"></asp:TextBox>
                                            <uc:ucSelectRecruit ID="ucSelectRecruit" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                                            <asp:HiddenField ID="hidReturnCompID" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="hidCheckInDate" runat="server"></asp:HiddenField>
                                        </td>
                                    </tr>       
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpName" ForeColor="Blue" runat="server" Text="*員工姓名"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtNameN" CssClass="InputTextStyle_Thin" runat="server" MaxLength="12" AutoPostBack="true"></asp:TextBox>
                                            <asp:Label ID="lblNameN" runat="server" Text="(難字)"></asp:Label>
                                            <asp:TextBox ID="txtName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="12"></asp:TextBox>
                                            <asp:Label ID="lblName" runat="server" Text="(拆字)"></asp:Label>
                                            <asp:TextBox ID="txtNameB" CssClass="InputTextStyle_Thin" runat="server" MaxLength="12"></asp:TextBox>
                                            <asp:Label ID="lblNameB" runat="server" Text="(造字)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblIDNo" ForeColor="Blue" runat="server" Text="*身分證字號"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:TextBox ID="txtIDNo" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30" Style="text-transform: uppercase"></asp:TextBox>
                                            <asp:Button ID="btnIDNo" runat="server" style="display:none" />
                                            <asp:Button ID="btnYes" Text="是" runat="server" style="display:none" />
                                            <asp:Button ID="btnNo" Text="否" runat="server" style="display:none" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblIDType" runat="server" ForeColor="Blue" Text="*證件類型"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlIDType" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEngName" runat="server" Text="英文姓名"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:TextBox ID="txtEngName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPassportName" runat="server" Text="護照英文名字"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:TextBox ID="txtPassportName" CssClass="InputTextStyle_Thin" runat="server" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblBirthDate" runat="server" ForeColor="Blue" Text="*生日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtBirthDate" runat="server" Enabled="True" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblSex" runat="server" ForeColor="Blue" Text="*性別"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlSex" runat="server" Font-Names="細明體">
                                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1-男"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2-女"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblNationID" runat="server" ForeColor="Blue" Text="*身分別"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlNationID" runat="server" Font-Names="細明體">
                                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1-本國人"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2-外國人"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3-大陸人"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4-香港人"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblIDExpireDate" runat="server" Text="工作證期限"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtIDExpireDate" runat="server" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEduID" runat="server" Text="學歷"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlEduID" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblMarriage" runat="server" ForeColor="Blue" Text="*婚姻狀況"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlMarriage" runat="server" Font-Names="細明體">
                                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1-未婚"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2-已婚"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblWorkStatus" runat="server" ForeColor="Blue" Text="*任職狀況"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlWorkStatus" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpType" runat="server" Text="僱用類別"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlEmpType" runat="server" Font-Names="細明體">
                                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1-已婚"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2-未婚"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpDate" runat="server" ForeColor="Blue" Text="*到職日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtEmpDate" runat="server" Enabled="True" />
                                            <asp:Button ID="btnValidDate" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblSinopacEmpDate" runat="server" ForeColor="Blue" Text="*企業團到職日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtSinopacEmpDate" runat="server" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblProbMonth" runat="server" Text="試用期"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left" colspan="3">
                                            <asp:DropDownList ID="ddlProbMonth" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <%--<td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblProbDate" runat="server" Text="試用考核試滿日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtProbDate" runat="server" Enabled="True" />
                                        </td>--%>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="*職等/*職稱"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left" colspan="3">
                                            <uc:ucSelectRankAndTitle ID="ucSelectRankAndTitle" runat="server"/>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblHoldingRankID" runat="server" ForeColor="Blue" Text="*金控職等"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlHoldingRankID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblHoldingTitle" runat="server" Text="金控職稱"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:Label ID="txtHoldingTitle" runat="server" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPublicTitleID" runat="server" Text="對外職稱"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlPublicTitleID" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblRankBeginDate" runat="server" ForeColor="Blue" Text="*最近升遷日\本階起始日"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:uccalender ID="txtRankBeginDate" runat="server" Enabled="True" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblGroupID" runat="server" Text="事業群"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:Label ID="txtGroupID" runat="server" ></asp:Label>
                                            <asp:Label ID="txtGroupName" runat="server" ></asp:Label>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblDeptID" runat="server" ForeColor="Blue" Text="*部門/*科組課"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <uc:SelectHROrgan ID="ucSelectHROrgan" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPositionID" runat="server" Text="職位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlPositionID" runat="server" AutoPostBack="True" Font-Names="細明體"></asp:DropDownList>
                                            <asp:Label ID="lblSelectPosition" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                                            <asp:Label ID="lblSelectPositionName" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                                            <asp:HiddenField ID="hidPositionID" runat="server" />
                                            <uc:ButtonPosition ID="ucSelectPosition" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblWorkTypeID" runat="server" ForeColor="Blue" Text="*工作性質"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlWorkTypeID" runat="server" Font-Names="細明體" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="lblSelectWorkTypeID" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                                            <asp:Label ID="lblSelectWorkTypeName" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                                            <asp:HiddenField ID="hidWorkTypeID" runat="server" />
                                            <uc:ButtonWorkType ID="ucSelectWorkType" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblWorkSiteID" runat="server" Text="工作地點"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlWorkSiteID" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblWTID" runat="server" Text="班別類型/班別"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:DropDownList ID="ddlWTIDTypeFlag" runat="server" Font-Names="細明體" AutoPostBack="true"></asp:DropDownList>
                                            <asp:DropDownList ID="ddlWTID" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblLocalHireFlag" runat="server" Text="LocalHire註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:CheckBox ID="chkLocalHireFlag" runat="server" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblPassExamFlag" runat="server" Text="新員招考註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:CheckBox ID="chkPassExamFlag" runat="server" />
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblOfficeLoginFlag" runat="server" Text="需刷卡註記"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:CheckBox ID="chkOfficeLoginFlag" runat="server" />
                                        </td>
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblAboriginalFlag" runat="server" Text="原住民註記/族別"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left">
                                            <asp:CheckBox ID="chkAboriginalFlag" AutoPostBack="true" runat="server" />
                                            <asp:DropDownList ID="ddlAboriginalTribe" runat="server" Font-Names="細明體"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmpFlowOrganID" runat="server" ForeColor="Blue" Text="*最小功能單位"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left" colspan="3">
                                            <asp:UpdatePanel ID="UpdFlowOrgnaID" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <uc:ucSelectFlowOrgan ID="ucSelectFlowOrgan" runat="server" Enabled="True" AutoPostBack="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr style="height:20px">
                                        <td class="td_EditHeader" width="15%" align="center">
                                            <asp:Label ID="lblEmployeeLogRemark" runat="server" Text="企業團經歷的備註(最多100字)"></asp:Label>
                                        </td>
                                        <td class="td_Edit" width="35%" align="left" colspan="3">
                                            <asp:TextBox ID="txtEmployeeLogRemark" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" TextMode="MultiLine" Width="600px" Height="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="ReIDNo" runat="server" visible="false">
                                        <td colspan="4">
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                                <tr style="height:20px">
                                                    <td class="td_Edit" width="35%" align="left" colspan="4">
                                                        <asp:Label ID="lblReIDNo" runat="server" Text="[身份證字號重複員工資料]"></asp:Label>
                                                    </td>
                                                </tr>
                                                    <tr style="height:20px">
                                                        <td class="td_Edit" width="35%" align="left" colspan="4">
                                                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="EmpID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID" >
                                                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Name" HeaderText="姓名" ReadOnly="True" SortExpression="Name" >
                                                                        <HeaderStyle Width="5%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                                                    </asp:BoundField>                                        
                                                                    <asp:BoundField DataField="WorkStatusName" HeaderText="任職狀況" ReadOnly="True" SortExpression="WorkStatusName" >
                                                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CompName" HeaderText="公司" ReadOnly="True" SortExpression="CompName" >
                                                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="OrganName" HeaderText="部門" ReadOnly="True" SortExpression="OrganName" >
                                                                        <HeaderStyle Width="6%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="EmpDate" HeaderText="到職日" ReadOnly="True" SortExpression="EmpDate" >
                                                                        <HeaderStyle Width="8%" CssClass="td_header" />
                                                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                        <asp:BoundField DataField="QuitDate" HeaderText="離職日" ReadOnly="True" SortExpression="QuitDate" >
                                                                        <HeaderStyle Width="8%" CssClass="td_header" />
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
