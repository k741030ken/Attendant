<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MA1000.aspx.vb" Inherits="MA_MA1000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <style type="text/css">
        .TrInfo
        {
        	padding:15px 0px;
        	/*padding: 15px 0px 5px 15px;*/
        }
        .TitleCss
        {
        	padding:5px 15px;
        	/*background-color: #FFC0CB;*/
        	background-color: #CCE6FF;
        }
    </style>
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--
        function ScrollButtom() {
            setTimeout(function () {
                //window.scrollTo(0, document.body.scrollHeight);
                var TableX = document.body.scrollHeight - document.getElementById("gvMain").scrollHeight
                window.scrollTo(0, TableX);
            }, 50);
        }

        function ReLoadChkValue() {
            ChkValue('txtAgeB', 'txtAgeE', 'lblAgeNotice');
            ChkValue('txtEmpSeniorityB', 'txtEmpSeniorityE', 'lblEmpSeniorityNotice2');
            ChkValue('txtSinopacEmpSeniorityB', 'txtSinopacEmpSeniorityE', 'lblSinopacEmpSeniorityNotice2');
            ChkValue('txtEmpSenOrgTypeB', 'txtEmpSenOrgTypeE', 'lblEmpSenOrgTypeNotice2');
            ChkValue('txtEmpConSenOrgTypeB', 'txtEmpConSenOrgTypeE', 'lblEmpConSenOrgTypeNotice2');
            ChkValue('txtEmpSenOrgTypeFlowB', 'txtEmpSenOrgTypeFlowE', 'lblEmpSenOrgTypeFlowNotice2');
            ChkValue('txtEmpSenRankB', 'txtEmpSenRankE', 'lblEmpSenRankNotice2');
            ChkValue('txtEmpSenPositionB', 'txtEmpSenPositionE', 'lblEmpSenPositionNotice2');
            ChkValue('txtEmpConSenPositionB', 'txtEmpConSenPositionE', 'lblEmpConSenPositionNotice2');
            ChkValue('txtEmpSenWorkTypeB', 'txtEmpSenWorkTypeE', 'lblEmpSenWorkTypeNotice2');
            ChkValue('txtEmpConSenWorkTypeB', 'txtEmpConSenWorkTypeE', 'lblEmpConSenWorkTypeNotice2');
            ChkValue('ddlRankB', 'ddlRankE', 'lblRankNotice');
            ChkValue('ddlTitleB', 'ddlTitleE', 'lblTitleNotice');
            ChkValue('ddlHoldingRankB', 'ddlHoldingRankE', 'lblHoldingRankNotice');
            ChkValue('ddlHoldingTitleB', 'ddlHoldingTitleE', 'lblHoldingTitleNotice');
            ChkValue('txtLastWorkYearB', 'txtLastWorkYearE', 'lblLastWorkYearNotice2');
            ChkValue('txtYearSalaryB', 'txtYearSalaryE', 'lblYearSalaryNotice');
            ChkValue('txtMonthSalaryB', 'txtMonthSalaryE', 'lblMonthSalaryNotice');
            //window.scrollTo(0, document.body.scrollHeight);
        }

        function ChkValue(objB, objE, objN) {
            var ValueB = $("#" + objB).val();
            var ValueE = $("#" + objE).val();
            
            //var reg = /^[0-9]+(.[0-9]{1,2})?$/;
            
            //if (!reg.test(ValueB)) {
            //    alert("格式不正確！");
            //    return;
            //}

            //if (!reg.test(ValueE)) {
            //    alert("格式不正確！");
            //    return;
            //}

            if (ValueB != "" && ValueE != "") {
                if (objB.indexOf("ddlRank") >= 0) {
                    $.ajax({
                        type: "POST",
                        url: "MA1000.aspx/GetRankMapping",
                        data: "{CompID: '" + $("#ddlCompID").val() + "', Rank: '" + ValueB + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            ValueB = data.d
                        }
                    });

                    $.ajax({
                        type: "POST",
                        url: "MA1000.aspx/GetRankMapping",
                        data: "{CompID: '" + $("#ddlCompID").val() + "', Rank: '" + ValueE + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            ValueE = data.d
                        }
                    });
                }

                if (objB.indexOf("ddlTitle") >= 0) {
                    if ($("#ddlRankB").val() != $("#ddlRankE").val()) {
                        return;
                    }
                }

                var NumB = new Number(ValueB);
                var NumE = new Number(ValueE);

                if (!isNaN(ValueB) && !isNaN(ValueE)) {
                    if (NumB > NumE) {
                        $("#" + objN).show();
                    } else {
                        $("#" + objN).hide();
                    }
                } else {
                    $("#" + objN).hide();
                }
            } else {
                $("#" + objN).hide();
            }
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <asp:UpdatePanel ID="UpdMain" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%" frame="border">
                        <tr>
                            <td align="left" width="2%"></td>
                            <td align="left" width="7%"></td>
                            <td align="left" width="16%"></td>
                            <td align="left" width="1%" rowspan="50" style="border-left:1px solid #000000;"></td>
                            <td align="left" width="7%"></td>
                            <td align="left" width="16%"></td>
                            <td align="left" width="1%" rowspan="50" style="border-left:1px solid #000000;"></td>
                            <td align="left" width="9%"></td>
                            <td align="left" width="16%"></td>
                            <td align="left" width="1%" rowspan="50" style="border-left:1px solid #000000;"></td>
                            <td align="left" width="8%"></td>
                            <td align="left" width="16%"></td>
                            <td align="left" width="2%"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo" style="padding-top:5px;">
                                <div class="TitleCss">
                                    <asp:Label ID="lblEmpInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工基本資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCompID" AutoPostBack="true" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEmpID" CssClass="InputTextStyle_Thin" MaxLength="6" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                                <uc:ButtonQuerySelectUserID ID="ucSelectEmpID" runat="server" ButtonText="..." ButtonHint="選擇人員..." WindowHeight="550" WindowWidth="500" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblName" Font-Size="15px" runat="server" Text="員工姓名"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtName" CssClass="InputTextStyle_Thin" MaxLength="12" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEngName" Font-Size="15px" runat="server" Text="英文姓名"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEngName" CssClass="InputTextStyle_Thin" MaxLength="20" runat="server"></asp:TextBox>
                                <br />
                                <%--<asp:Label ID="lblEngNameNotice" Font-Size="12px" runat="server" Text="※請輸入大寫" ForeColor="Red"></asp:Label>--%>                            
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblIDNo" Font-Size="15px" runat="server" Text="身份證字號"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIDNo" CssClass="InputTextStyle_Thin" MaxLength="30" runat="server" style="text-transform: uppercase"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblBirthDate" Font-Size="15px" runat="server" Text="生日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtBirthDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblBirthDateTxt" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtBirthDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblAge" Font-Size="15px" runat="server" Text="年齡"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblAgeText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtAgeB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="2" runat="server"></asp:TextBox>
                                <asp:Label ID="lblAgeText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtAgeE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="2" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblAgeNotice" Font-Size="12px" runat="server" Text="年齡輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPassportName" Font-Size="15px" runat="server" Text="護照英文姓名"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPassportName" CssClass="InputTextStyle_Thin" MaxLength="30" runat="server"></asp:TextBox>
                                <br />
                                <%--<asp:Label ID="lblPassportNameNotice" Font-Size="12px" runat="server" Text="※請輸入大寫" ForeColor="Red"></asp:Label>--%>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblNation" Font-Size="15px" runat="server" Text="身分別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlNation" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-本國人"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-外國人"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblIDExpireDate" Font-Size="15px" runat="server" Text="工作證期限"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtIDExpireDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblIDExpireDateTxt" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtIDExpireDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblsex" Font-Size="15px" runat="server" Text="性別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSex" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-男"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-女"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblMarriage" Font-Size="15px" runat="server" Text="婚姻狀況"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlMarriage" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-未婚"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-已婚"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" style="padding:8px"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpType" Font-Size="15px" runat="server" Text="雇用類別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpType" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="txtWorkCode" Font-Size="15px" runat="server" Text="任職狀況"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWorkCode" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLocalHireFlag" Font-Size="15px" runat="server" Text="Local Hire註記"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLocalHireFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-是"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPassExamFlag" Font-Size="15px" runat="server" Text="新員招考註記"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPassExamFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-透過考試合格錄用"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-經驗行員"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblRank" Font-Size="15px" runat="server" Text="職等"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRankText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlRankB" runat="server" AutoPostBack="true" Width="50px" Font-Names="細明體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                                <asp:Label ID="lblRankText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="lblRankText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlRankE" runat="server" AutoPostBack="true" Width="50px" Font-Names="細明體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblRankNotice" Font-Size="12px" runat="server" Text="職等選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTitle" Font-Size="15px" runat="server" Text="職稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="ddlTitleText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlTitleB" runat="server" Width="120px" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="ddlTitleText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="ddlTitleText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlTitleE" runat="server" Width="120px" Font-Names="細明體"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblTitleNotice" Font-Size="12px" runat="server" Text="職稱選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblHoldingRank" Font-Size="15px" runat="server" Text="金控職等"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblHoldingRankText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlHoldingRankB" runat="server" AutoPostBack="true" Width="50px" Font-Names="細明體" OnSelectedIndexChanged="ddlHoldingRank_Changed"></asp:DropDownList>
                                <asp:Label ID="lblHoldingRankText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="lblHoldingRankText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlHoldingRankE" runat="server" AutoPostBack="true" Width="50px" Font-Names="細明體" OnSelectedIndexChanged="ddlHoldingRank_Changed"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblHoldingRankNotice" Font-Size="12px" runat="server" Text="金控職等選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblHoldingTitle" Font-Size="15px" runat="server" Text="金控職稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblHoldingTitleText1" runat="server" Text=">="></asp:Label>
                                <asp:DropDownList ID="ddlHoldingTitleB" runat="server" Width="120px" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblHoldingTitleText2" runat="server" Text="～"></asp:Label>
                                <asp:Label ID="lblHoldingTitleText3" runat="server" Text="<="></asp:Label>
                                <asp:DropDownList ID="ddlHoldingTitleE" runat="server" Width="120px" Font-Names="細明體"></asp:DropDownList>
                                <br />
                                <asp:Label ID="lblHoldingTitleNotice" Font-Size="12px" runat="server" Text="金控職稱選擇請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr id="TrPosWork" runat="server">
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblPosition1" Font-Size="15px" runat="server" Text="職位(主要)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPosition1" Width="150px" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonPosition ID="ucPosition1" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectPosition_Click" />
                                <asp:HiddenField ID="hidPosition1" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblPosition2" Font-Size="15px" runat="server" Text="職位(次要)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPosition2" runat="server" Width="150px" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonPosition ID="ucPosition2" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectPosition_Click" />
                                <asp:HiddenField ID="hidPosition2" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblWorkType1" Font-Size="15px" runat="server" Text="工作性質(主要)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWorkType1" runat="server" Width="150px" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonWorkType ID="ucWorkType1" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectWorkType_Click" />
                                <asp:HiddenField ID="hidWorkType1" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblWorkType2" Font-Size="15px" runat="server" Text="工作性質(次要)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWorkType2" runat="server" Width="150px" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonWorkType ID="ucWorkType2" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectWorkType_Click" />
                                <asp:HiddenField ID="hidWorkType2" runat="server" />
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblGroupID" Font-Size="15px" runat="server" Text="事業群"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlGroupID" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblOrgType" Font-Size="15px" runat="server" Text="單位類別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOrgType" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDeptID" Font-Size="15px" runat="server" Text="部門"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體" OnSelectedIndexChanged="ddlDept_Changed"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Text="科/組/課"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOrganID" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" style="padding:8px"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpDate" Font-Size="15px" runat="server" Text="公司到職日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtEmpDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblEmpDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtEmpDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSinopacEmpDate" Font-Size="15px" runat="server" Text="企業團到職日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtSinopacEmpDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblSinopacEmpDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtSinopacEmpDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblQuitDate" Font-Size="15px" runat="server" Text="公司離職日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtQuitDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblQuitDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtQuitDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSinopacQuitDate" Font-Size="15px" runat="server" Text="企業團離職日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtSinopacQuitDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblSinopacQuitDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtSinopacQuitDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblPublicTitle" Font-Size="15px" runat="server" Text="對外職稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlPublicTitle" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRankBeginDate" Font-Size="15px" runat="server" Text="升遷日/起始日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtRankBeginDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblRankBeginDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtRankBeginDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProbMonth" Font-Size="15px" runat="server" Text="試用期"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlProbMonth" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblProbDate" Font-Size="15px" runat="server" Text="試用考核試滿日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtProbDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblProbDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtProbDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblAboriginalFlag" Font-Size="15px" runat="server" Text="原住民註記"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAboriginalFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-是"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblAboriginalTribe" Font-Size="15px" runat="server" Text="原住民族別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAboriginalTribe" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="txtWTID" Font-Size="15px" runat="server" Text="公司班別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWTID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblWorkSite" Font-Size="15px" runat="server" Text="工作地點"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlWorkSite" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblOfficeLoginFlag" Font-Size="15px" runat="server" Text="刷卡註記(證券)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOfficeLoginFlag" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-否"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-是"></asp:ListItem>                                
                                </asp:DropDownList>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblEmpSenInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工年資資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblSinopacEmpSeniority" Font-Size="15px" runat="server" Text="企業團年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSinopacEmpSeniorityText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtSinopacEmpSeniorityB" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblSinopacEmpSeniorityText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtSinopacEmpSeniorityE" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblSinopacEmpSeniorityNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblSinopacEmpSeniorityNotice2" Font-Size="12px" runat="server" Text="企業團年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSeniority" Font-Size="15px" runat="server" Text="公司年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSeniorityText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSeniorityB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSeniorityText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSeniorityE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSeniorityNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSeniorityNotice2" Font-Size="12px" runat="server" Text="公司年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenOrgType" Font-Size="15px" runat="server" Text="單位年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenOrgTypeText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSenOrgTypeB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSenOrgTypeText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSenOrgTypeE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSenOrgTypeNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSenOrgTypeNotice2" Font-Size="12px" runat="server" Text="單位年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenOrgType" Font-Size="15px" runat="server" Text="單位年資(連續)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenOrgTypeText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenOrgTypeB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpConSenOrgTypeText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenOrgTypeE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpConSenOrgTypeNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpConSenOrgTypeNotice2" Font-Size="12px" runat="server" Text="單位年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenOrgTypeFlow" Font-Size="15px" runat="server" Text="簽核單位年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenOrgTypeFlowText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSenOrgTypeFlowB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSenOrgTypeFlowText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSenOrgTypeFlowE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSenOrgTypeFlowNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSenOrgTypeFlowNotice2" Font-Size="12px" runat="server" Text="簽核單位年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenRank" Font-Size="15px" runat="server" Text="職等年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenRankText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSenRankB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSenRankText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSenRankE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSenRankNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSenRankNotice2" Font-Size="12px" runat="server" Text="職等年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenPosition" Font-Size="15px" runat="server" Text="職位年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenPositionText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSenPositionB" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSenPositionText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSenPositionE" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSenPositionNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSenPositionNotice2" Font-Size="12px" runat="server" Text="職位年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenPosition" Font-Size="15px" runat="server" Text="職位年資(連續)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenPositionText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenPositionB" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpConSenPositionText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenPositionE" CssClass="InputTextStyle_Thin" Width="40px" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpConSenPositionNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpConSenPositionNotice2" Font-Size="12px" runat="server" Text="職位年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenWorkType" Font-Size="15px" runat="server" Text="工作性質年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpSenWorkTypeText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpSenWorkTypeB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpSenWorkTypeText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpSenWorkTypeE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpSenWorkTypeNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpSenWorkTypeNotice2" Font-Size="12px" runat="server" Text="工作性質年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenWorkType" Font-Size="15px" runat="server" Text="工作性質年資"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpConSenWorkType2" Font-Size="15px" runat="server" Text="(連續)"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpConSenWorkTypeText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenWorkTypeB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <asp:Label ID="lblEmpConSenWorkTypeText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtEmpConSenWorkTypeE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblEmpConSenWorkTypeNotice" Font-Size="12px" runat="server" Text="※小數點以下最多兩位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblEmpConSenWorkTypeNotice2" Font-Size="12px" runat="server" Text="工作性質年資(連續)輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblEmpAdditionInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工調兼資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpAddition" Font-Size="15px" runat="server" Text="調兼現況/記錄"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpAddition" runat="server" Font-Names="細明體" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="員工調兼記錄"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="員工調兼現況"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblEmpAdditionComp" Font-Size="15px" runat="server" Text="兼任公司"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpAdditionComp" runat="server" Font-Names="細明體" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpAdditionDept" Font-Size="15px" runat="server" Text="兼任部門"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpAdditionDept" runat="server" Font-Names="細明體" Enabled="false"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpAdditionReason" Font-Size="15px" runat="server" Text="兼任狀態"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEmpAdditionReason" runat="server" Font-Names="細明體" Enabled="false"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblEmpAdditionDate" Font-Size="15px" runat="server" Text="兼任生效日期"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtEmpAdditionDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="false" Width="80px" />
                                <asp:Label ID="lblEmpAdditionDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtEmpAdditionDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="false" Width="80px" />
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblScoolInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工學歷資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="txtEdu" Font-Size="15px" runat="server" Text="學歷"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEdu" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSchoolType" Font-Size="15px" runat="server" Text="學校類別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlSchoolType" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblSchool" Font-Size="15px" runat="server" Text="校名"></asp:Label>
                            </td>
                            <td align="left">
                                <%--<asp:TextBox ID="txtSchool" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlSchool" Width="150px" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonMultiListBox ID="ucSchool" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectSchool_Click" />
                                <asp:HiddenField ID="hidSchool" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblDepart" Font-Size="15px" runat="server" Text="科系"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDepart" CssClass="InputTextStyle_Thin" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblFamilyInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工眷屬資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblFamilyOccupation" Font-Size="15px" runat="server" Text="眷屬職業"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFamilyOccupation" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblFamilyIndustryType" Font-Size="15px" runat="server" Text="眷屬產業別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFamilyIndustryType" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblFamilyCompany" Font-Size="15px" runat="server" Text="眷屬服務機構"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFamilyCompany" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblCommInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工通訊資料"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblRegCityCode" Font-Size="15px" runat="server" Text="戶籍縣市別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRegCityCode" runat="server" Font-Names="細明體" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCode_SelectedChanged"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRegAddrCode" Font-Size="15px" runat="server" Text="戶籍郵遞區號"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRegAddrCode" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRegAddr" Font-Size="15px" runat="server" Text="戶籍地址"></asp:Label>
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtRegAddr" CssClass="InputTextStyle_Thin" Width="100%" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblCommCityCode" Font-Size="15px" runat="server" Text="通訊縣市別"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCommCityCode" runat="server" Font-Names="細明體" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCode_SelectedChanged"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCommAddrCode" Font-Size="15px" runat="server" Text="通訊郵遞區號"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCommAddrCode" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblCommAddr" Font-Size="15px" runat="server" Text="通訊地址"></asp:Label>
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtCommAddr" CssClass="InputTextStyle_Thin" Width="100%" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblLastJobInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工前職經歷"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblLastBeginDate" Font-Size="15px" runat="server" Text="起日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtLastBeginDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblLastBeginDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtLastBeginDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastEndDate" Font-Size="15px" runat="server" Text="迄日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtLastEndDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblLastEndDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtLastEndDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastIndustryType" Font-Size="15px" runat="server" Text="產業類別"></asp:Label>
                            </td>
                            <td align="left">
                               <asp:DropDownList ID="ddlLastIndustryType" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastCompany" Font-Size="15px" runat="server" Text="公司名稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastCompany" CssClass="InputTextStyle_Thin" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblLastDept" Font-Size="15px" runat="server" Text="部門"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastDept" CssClass="InputTextStyle_Thin" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastTitle" Font-Size="15px" runat="server" Text="職稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastTitle" CssClass="InputTextStyle_Thin" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastWorkType" Font-Size="15px" runat="server" Text="工作性質"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastWorkType" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastWorkYear" Font-Size="15px" runat="server" Text="年資"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastWorkYearText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtLastWorkYearB" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="4" runat="server"></asp:TextBox>
                                <asp:Label ID="lblLastWorkYearText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtLastWorkYearE" CssClass="InputTextStyle_Thin" Width="40px" MaxLength="4" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblLastWorkYearNotice" Font-Size="12px" runat="server" Text="※小數點以下最多一位" ForeColor="Red"></asp:Label>
                                <br />
                                <asp:Label ID="lblLastWorkYearNotice2" Font-Size="12px" runat="server" Text="年資輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblProfession" Font-Size="15px" runat="server" Text="專業記號"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlProfession" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-非專業"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-專業"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblEmpLog" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工企業團經歷"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblModifyDate" Font-Size="15px" runat="server" Text="生效日"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtModifyDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblModifyDateText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtModifyDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblReason" Font-Size="15px" runat="server" Text="異動原因"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlReason" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLogDept" Font-Size="15px" runat="server" Text="部門"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLogDept" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體" OnSelectedIndexChanged="ddlDept_Changed"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLogOrgan" Font-Size="15px" runat="server" Text="科/組/課"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLogOrgan" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblLogPosition" Font-Size="15px" runat="server" Text="職位"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLogPosition" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLogWorkType" Font-Size="15px" runat="server" Text="工作性質"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLogWorkType" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLogRank" Font-Size="15px" runat="server" Text="職等"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLogRank" runat="server" AutoPostBack="true" Font-Names="細明體" OnSelectedIndexChanged="ddlRank_Changed"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLogTitle" Font-Size="15px" runat="server" Text="職稱"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLogTitle" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblEmpLogRange" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工企業團經歷區間"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblOrgRange" Font-Size="15px" runat="server" Text="曾任部門區間"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtOrgRangeB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblOrgRangeText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtOrgRangeE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlOrgRange" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblOrgRangeNotice" Font-Size="12px" runat="server" Text="※日期區間與選單皆為必填" ForeColor="Red" style="background-color: #F3F8FF;"></asp:Label>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblPositionRange" Font-Size="15px" runat="server" Text="曾任職位區間"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtPositionRangeB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblPositionRangeText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtPositionRangeE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlPositionRange" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblPositionRangeNotice" Font-Size="12px" runat="server" Text="※日期區間與選單皆為必填" ForeColor="Red" style="background-color: #F3F8FF;"></asp:Label>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblWorkTypeRange" Font-Size="15px" runat="server" Text="曾任工作性質區間"></asp:Label>
                            </td>
                            <td align="left">
                                <uc:uccalender ID="txtWorkTypeRangeB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblWorkTypeRangeText" runat="server" Text="～"></asp:Label>
                                <uc:uccalender ID="txtWorkTypeRangeE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlWorkTypeRange" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblWorkTypeRangeNotice" Font-Size="12px" runat="server" Text="※日期區間與選單皆為必填" ForeColor="Red" style="background-color: #F3F8FF;"></asp:Label>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblLicenseInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工證照資料"></asp:Label>                                    
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblCategoryID" Font-Size="15px" runat="server" Text="證照代碼"></asp:Label>
                            </td>
                            <td align="left">
                                <%--<asp:DropDownList ID="ddlCategoryID" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlCategory" Width="150px" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <uc:ButtonMultiListBox ID="ucCategory" runat="server" ButtonText="..." ButtonHint="選取" WindowHeight="550" WindowWidth="1000" OnLoad="ucSelectCategory_Click" />
                                <asp:HiddenField ID="hidCategory" runat="server" />
                            </td>
                            <td align="left">
                                <%--<asp:Label ID="lblLicenseName" Font-Size="15px" runat="server" Text="證照名稱"></asp:Label>--%>
                            </td>
                            <td align="left">
                                <%--<asp:TextBox ID="txtLicenseName" CssClass="InputTextStyle_Thin" runat="server"></asp:TextBox>--%>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr id="TrGradeInfo" runat="server" visible="false">
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblGradeInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工考績資料(重要)"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr id="TrCommGrade" runat="server" visible="false">
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblCommGrade" Font-Size="15px" runat="server" Text="常用條件"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCommGrade" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblGradeYear" Font-Size="15px" runat="server" Text="考績年度"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlGradeYear" runat="server" Font-Names="細明體" AutoPostBack="true"></asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblGrade" Font-Size="15px" runat="server" Text="考績"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlGrade" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr id="TrGrade" runat="server" visible="false">
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblLastGrade1" Font-Size="15px" runat="server" Text="前一年考績"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLastGrade1" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblLastGrade1Text" Font-Size="15px" runat="server" Text="及"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastGrade2" Font-Size="15px" runat="server" Text="前二年考績"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLastGrade2" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblLastGrade2Text" Font-Size="15px" runat="server" Text="及"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastGrade3" Font-Size="15px" runat="server" Text="前三年考績"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLastGrade3" runat="server" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblLastGrade3Text" Font-Size="15px" runat="server" Text="及"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblLastGrade4" Font-Size="15px" runat="server" Text="前四年考績"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlLastGrade4" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                            <td align="left"></td>
                        </tr>
                        <tr id="TrSalaryInfo" runat="server" visible="false">
                            <td align="left" colspan="13" class="TrInfo">
                                <div class="TitleCss">
                                    <asp:Label ID="lblSalaryInfo" Font-Size="15px" Font-Underline="true" Font-Bold="true" runat="server" Text="員工薪資資料(重要)"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr id="TrSalary" runat="server" visible="false">
                            <td align="left"></td>
                            <td align="left">
                                <asp:Label ID="lblYearSalary" Font-Size="15px" runat="server" Text="年薪"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblYearSalaryText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtYearSalaryB" CssClass="InputTextStyle_Thin" Width="60px" MaxLength="32" runat="server"></asp:TextBox>
                                <asp:Label ID="lblYearSalaryText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtYearSalaryE" CssClass="InputTextStyle_Thin" Width="60px" MaxLength="32" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblYearSalaryNotice" Font-Size="12px" runat="server" Text="年薪輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblMonthSalary" Font-Size="15px" runat="server" Text="月薪"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblMonthSalaryText1" runat="server" Text=">="></asp:Label>
                                <asp:TextBox ID="txtMonthSalaryB" CssClass="InputTextStyle_Thin" Width="60px" MaxLength="32" runat="server"></asp:TextBox>
                                <asp:Label ID="lblMonthSalaryText2" runat="server" Text="～ <="></asp:Label>
                                <asp:TextBox ID="txtMonthSalaryE" CssClass="InputTextStyle_Thin" Width="60px" MaxLength="32" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblMonthSalaryNotice" Font-Size="12px" runat="server" Text="月薪輸入請由小到大" ForeColor="Red" style="display:none;"></asp:Label>
                            </td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                            <td align="left"></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="13" style="padding:8px"></td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCompID" />
                        <asp:PostBackTrigger ControlID="ucSelectEmpID" />
                        <asp:PostBackTrigger ControlID="ucPosition1" />
                        <asp:PostBackTrigger ControlID="ucPosition2" />
                        <asp:PostBackTrigger ControlID="ucWorkType1" />
                        <asp:PostBackTrigger ControlID="ucWorkType2" />
                        <asp:PostBackTrigger ControlID="ucSchool" />
                        <asp:PostBackTrigger ControlID="ucCategory" />
                        <asp:AsyncPostBackTrigger ControlID="ddlRankB" />
                        <asp:AsyncPostBackTrigger ControlID="ddlRankE" />
                        <asp:AsyncPostBackTrigger ControlID="ddlHoldingRankB" />
                        <asp:AsyncPostBackTrigger ControlID="ddlHoldingRankE" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDeptID" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEmpAddition" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEmpAdditionComp" />
                        <asp:AsyncPostBackTrigger ControlID="ddlRegCityCode" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCommCityCode" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLogDept" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLogRank" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGradeYear" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,CompName,EmpID,NameN,IDNo" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail" Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompID" HeaderText="公司代碼" ReadOnly="True" SortExpression="CompID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpID" HeaderText="員工編號" ReadOnly="True" SortExpression="EmpID" >
                                            <HeaderStyle Width="7%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NameN" HeaderText="員工姓名" ReadOnly="True" SortExpression="NameN" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeptID" HeaderText="部門代碼" ReadOnly="True" SortExpression="DeptID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeptName" HeaderText="部門名稱" ReadOnly="True" SortExpression="DeptName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganID" HeaderText="科組課代碼" ReadOnly="True" SortExpression="OrganID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganName" HeaderText="科組課名稱" ReadOnly="True" SortExpression="OrganName" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgComp" HeaderText="最後異動公司" ReadOnly="True" SortExpression="LastChgComp" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgID" HeaderText="最後異動者" ReadOnly="True" SortExpression="LastChgID" >
                                            <HeaderStyle Width="8%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastChgDate" HeaderText="最後異動日期" ReadOnly="True" SortExpression="LastChgDate" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
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
    </form>
</body>
</html>
