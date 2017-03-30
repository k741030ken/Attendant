<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4200.aspx.vb" Inherits="OV_OV4200" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--記得參考HR3000改GridView!!!--%>
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
            switch (Param) {
                case "btnExecutes":   //送簽
                case "btnReject":    //取消
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRows('')) {
                        alert("未選取資料列！");
                        return false;
                    }
                    else { }
                case "btnActionX":   //清除
                    break;
            }
        }

        //---------------------------------ajax區-------------------------------------
        function callashx() {
            var ajaxUrl = $('#callHandlerUrl').val() + 'HandlerForOverTime/OverTimeMultiDataApprovedHandler.ashx'
            if (ajaxUrl) {
                var postData = { //給參數 json格式
                    Platform: 'AP',
                    SystemID: 'OT',
                    TxnName: 'OV4200',
                    UserID: $('#hidUserID').val(),
                    UserComp: $('#hidOTRegisterCompID').val(),
                    CacheID: $('#hidGuidID').val()
                }
                type = "GET";
                //callAjaxControlForJson
                callAjaxControlForJson(ajaxUrl, postData, type);
            }
            else {
                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                btnExecutes.removeAttribute("disabled");
                return false;
            }
        }

        //callAjaxControlForJson
        function callAjaxControlForJson(ajaxUrl, postData, type) {
            var timeout = 60 * 1000; //60s
            var ajaxControl = $.ajax({
                url: ajaxUrl,
                timeout: timeout,
                type: type,
                data: postData,
                contentType: 'application/json; charset=utf-8',
                dataType: "jsonp",
                jsonp: "jsoncallback",
                async: false,
                cache: false
            });
            //ajax 連線成功
            ajaxControl.done(function (data) {
                if (data) {
                    if (data.IsSussecc) {
                        if (data.Message && data.Message != "") {
                            document.getElementById("btnQuery").click();
                            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                            btnExecutes.removeAttribute("disabled");
                            alert(data.Message);
                        }
                    }
                    else {
                        if (data.Message && data.Message != "") {
                            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                            btnExecutes.removeAttribute("disabled");
                            alert(data.Message);
                        }
                    }
                }
                else {
                    var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                    btnExecutes.removeAttribute("disabled");
                    alert("無回傳資料!");
                }
            });
            //ajax 連線失敗
            ajaxControl.fail(function (data) {
                alert("連線失敗!!");
                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                btnExecutes.removeAttribute("disabled");
                document.getElementById("ClearSubmitCache").click();
            });
        }


        //        function callashx() {
        //            //var ajaxUrl = $('#callHandlerUrl').val();
        //            var ajaxUrl = 'http://localhost:7218/HandlerForOverTime/OverTimeSingleDataApprovedHandler.ashx'
        //            if (ajaxUrl) {
        //                var OTEmpIDValue = $('#hidOTEmpID').val();
        //                var MealFlagValue = $('#hidMealFlag').val();
        //                var DateStartValue = $('#hidDateStart').val();
        //                var DateEndValue = $('#hidDateEnd').val();
        //                var OTTimeEndHHValue = $('#hidOTTimeEndHH').val();
        //                var OTTimeEndMMValue = $('#hidOTTimeEndMM').val();
        //                var OTTimeStartHHValue = $('#hidOTTimeStartHH').val();
        //                var OTTimeStartMMValue = $('#hidOTTimeStartMM').val();
        //                var DeptIDValue = $('#hidDeptID').val();
        //                var OrganIDValue = $('#hidOrganID').val();
        //                var DeptNameValue = $('#hidDeptName').val();
        //                var OrganNameValue = $('#hidOrganName').val();
        //                var SalaryOrAdjustVisibleValue = $('#hidSalaryOrAdjustVisible').val();
        //                var SalaryOrAdjustValue = $('#hidSalaryOrAdjust').val();
        //                var AdjustInvalidDateValue = $('#hidAdjustInvalidDate').val();
        //                var MealTimeValue = $('#hidMealTime').val();
        //                var OTTypeIDValue = $('#hidOTTypeID').val();
        //                var OTReasonMemoValue = $('#hidOTReasonMemo').val();

        //                //alert(OTEmpID)
        //                //var test02Value = $('#test02').val();
        //                var postData = { //給參數 json格式
        //                    OTEmpID: OTEmpIDValue,
        //                    MealFlag: MealFlagValue,
        //                    DateStart: DateStartValue,
        //                    DateEnd: DateEndValue,
        //                    OTTimeEndHH: OTTimeEndHHValue,
        //                    OTTimeEndMM: OTTimeEndMMValue,
        //                    OTTimeStartHH: OTTimeStartHHValue,
        //                    OTTimeStartMM: OTTimeEndMMValue,
        //                    DeptID: DeptIDValue,
        //                    OrganID: OrganIDValue,
        //                    DeptName: DeptNameValue,
        //                    OrganName: OrganNameValue,
        //                    SalaryOrAdjustVisible: SalaryOrAdjustVisibleValue,
        //                    SalaryOrAdjust: SalaryOrAdjustValue,
        //                    AdjustInvalidDate: AdjustInvalidDateValue,
        //                    MealTime: MealTimeValue,
        //                    OTTypeID: OTTypeIDValue,
        //                    OTReasonMemo: OTReasonMemoValue
        //                    //test02: test02Value
        //                }
        //                type = "GET";
        //                //callAjaxControlForJson
        //                callAjaxControlForJson(ajaxUrl, postData, type);
        //            }
        //            else {
        //                return false;
        //            }
        //        }

        //----------------------------------------------------------------------

        //----------彈出詢問視窗確認後才觸發隱藏跳轉btn進行下一步動作----------
        function SubmitAsk() {
            if (!confirm('確認是否要送簽？'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }

        function DeleteAsk() {
            if (!confirm('確定要刪除嗎？'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }

        function RejectAsk() {
            if (!confirm('確定要取消嗎？'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }
        //-------------------------------------------------------------------

        //----------------執行動作的隱藏btn--------------------
        function Submit() {
            document.getElementById("btnSubmit").click();
        }

        function Delete() {
            document.getElementById("btnDelete").click();
        }

        function Reject() {
            document.getElementById("btnReject").click();
        }
        //-----------------------------------------------------

        //執行送簽
        function ExecSubmit() {
            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
            btnExecutes.setAttribute("disabled", "disabled");

            document.getElementById("jqueryCall").click();
        }

        //按下Enter打開人員快查的子視窗
        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex)
                { }
            }
        }

        //清除畫面GridView的勾選狀態
        //這段就是要放在文件上方，不要丟到下面去了~
        function gridClear() {
            document.getElementById("__SelectedRowsgvMain").value = "";
        }
-->
    </script>
    <style type="text/css">
        .tdpadding
        {
            width: 3%;
            border: 1px solid #f3f8ff;
            background-color: #f3f8ff;
        }
        .style1
        {
            height: 34px;
            font-family: 微軟正黑體;
            font-size: 15px;
            border: 1px solid #f3f8ff;
            background-color: #f3f8ff;
        }
        GridView
        {
            border-width: 1px;
            border-style: solid;
            border-color: #89b3f5;
            font-size: 14px;
            font-family: 微軟正黑體;
            height: 16px;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="center" style="height: 30px;" class="style1">
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                    <tr>
                        <td class="tdpadding">
                        </td>
                        <td align="left" width="10%" class="style1">
                            公司：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Width="200px"></asp:Label>
                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                        </td>
                        <td align="left" width="10%" class="style1">
                            <%-- 單位：--%>
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:Label ID="lblDeptID" Font-Size="15px" runat="server" Width="200px" Visible="False"></asp:Label>
                        </td>
                        <td align="left" width="10%" class="style1">
                            <%-- 科別：--%>
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Width="200px" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdpadding">
                        </td>
                        <td align="left" width="10%" class="style1">
                            查詢條件：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:DropDownList ID="ddlQueryFactor" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="0" Text="填單人員"></asp:ListItem>
                                <asp:ListItem Value="1" Text="加班人員"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidQueryFactor" runat="server" />
                        </td>
                        <td align="left" width="10%" class="style1">
                            員工編號：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtEmpID" MaxLength="6" runat="server"
                                Style="text-transform: uppercase" AutoPostBack="true" Width="100px"></asp:TextBox>
                            <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600"
                                ButtonText="..." />
                        </td>
                        <td align="left" width="10%" class="style1">
                            員工姓名：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:Label ID="lblEmpNameN" Font-Size="15px" runat="server" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdpadding">
                        </td>
                        <td align="left" width="10%" class="style1">
                            表單編號：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtOTFormNo" MaxLength="16" runat="server"
                                Style="text-transform: uppercase" AutoPostBack="true"></asp:TextBox>
                        </td>
                        <td align="left" width="10%" class="style1">
                            狀態：
                        </td>
                        <td align="left" width="20%" class="style1">
                            <asp:DropDownList ID="ddlOTStatus" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="暫存"></asp:ListItem>
                                <asp:ListItem Value="2" Text="送簽"></asp:ListItem>
                                <asp:ListItem Value="3" Text="核准"></asp:ListItem>
                                <asp:ListItem Value="4" Text="駁回"></asp:ListItem>
                                <asp:ListItem Value="5" Text="刪除"></asp:ListItem>
                                <asp:ListItem Value="6" Text="取消"></asp:ListItem>
                                <asp:ListItem Value="7" Text="作廢"></asp:ListItem>
                                <asp:ListItem Value="9" Text="計薪後收回"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="hidOTStatus" runat="server" />
                        </td>
                        <td align="left" width="10%" class="style1">
                            加班日期：
                        </td>
                        <td align="left" width="40%" class="style1">
                            <uc:ucCalender ID="ucOTStartDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                            ~
                            <uc:ucCalender ID="ucOTEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td align="center" width="100%">
                <table width="100%" class="tbl_Content">
                    <tr>
                        <td style="width: 100%">
                            <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                                AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTFormNO,OTStatus,OTStatusName,OTCompID,NameN,OTEmpID,OTTypeID,CodeCName,OTDate,OTTime,OTTotalTime,FileName,OTFromAdvanceTxnId,OTSeq,OTTxnID,OTStartDate,OTEndDate,MealFlag,DeptID,OrganID,DeptName,OrganName,SalaryOrAdjust,AdjustInvalidDate,MealTime,OTReasonMemo,FlowCaseID,OTRegisterID,OTRegisterComp"
                                Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="2%" />
                                        <HeaderTemplate>
                                            <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" HeaderText="全選"
                                                runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_gvMain" runat="server" /><asp:HiddenField ID="hldOTStatus"
                                                Value='<%# Databinder.Eval(Container.DataItem, "OTStatus") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField Text="明細" HeaderText="明細" CommandName="Detail" ItemStyle-Width="1px"
                                        ItemStyle-Font-Size="12px" HeaderStyle-CssClass="td_header" ItemStyle-CssClass="td_detail" />
                                    <asp:TemplateField HeaderText="序號">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"
                                                Font-Names="微軟正黑體"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="3%" Height="15px" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="OTFormNO" HeaderText="表單編號" ItemStyle-Width="1px" ItemStyle-Wrap="true"
                                        ReadOnly="True" SortExpression="OTFormNO" HtmlEncode="true">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTStatus" HeaderText="狀態" ReadOnly="True" SortExpression="OTStatus"
                                        Visible="false">
                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTStatusName" HeaderText="狀態" ReadOnly="True" SortExpression="OTStatusName">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTEmpID" HeaderText="加班人員工編號" ReadOnly="True" SortExpression="OTEmpID"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NameN" HeaderText="加班人" ReadOnly="True" SortExpression="NameN">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTTypeID" HeaderText="加班類型編號" ReadOnly="True" SortExpression="OTTypeID"
                                        Visible="false">
                                        <HeaderStyle Width="3%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodeCName" HeaderText="加班類型" ReadOnly="True" SortExpression="CodeCName">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTDate" HeaderText="加班日期" ReadOnly="True" SortExpression="OTDate">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTTime" HeaderText="加班起迄時間" ReadOnly="True" SortExpression="OTTime">
                                        <HeaderStyle Width="15%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="加班時數" SortExpression="OTTotalTime" DataField="OTTotalTime">
                                        <HeaderStyle Width="15%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTCompID" HeaderText="加班人公司編號" ReadOnly="True" SortExpression="OTCompID"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="上傳附件" SortExpression="FileName" DataField="FileName">
                                        <HeaderStyle Width="10%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTFromAdvanceTxnId" HeaderText="" ReadOnly="True" SortExpression="OTFromAdvanceTxnId"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTStartDate" HeaderText="" ReadOnly="True" SortExpression="OTStartDate"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTEndDate" HeaderText="" ReadOnly="True" SortExpression="OTEndDate"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTStartTime" HeaderText="" ReadOnly="True" SortExpression="OTStartTime"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTEndTime" HeaderText="" ReadOnly="True" SortExpression="OTEndTime"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
                                        <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OTSeq" HeaderText="" ReadOnly="True" SortExpression="OTSeq"
                                        Visible="false">
                                        <HeaderStyle Width="9%" CssClass="td_header" />
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
        <tr>
            <td>
                <asp:HiddenField ID="hidOTEmpID" runat="server" />
                <asp:HiddenField ID="hidOTStartDate" runat="server" />
                <asp:HiddenField ID="hidOTEndDate" runat="server" />
                <asp:HiddenField ID="hidMealFlag" runat="server" />
                <asp:HiddenField ID="hidDateStart" runat="server" />
                <asp:HiddenField ID="hidDateEnd" runat="server" />
                <asp:HiddenField ID="hidOTTimeEndHH" runat="server" />
                <asp:HiddenField ID="hidOTTimeEndMM" runat="server" />
                <asp:HiddenField ID="hidOTTimeStartHH" runat="server" />
                <asp:HiddenField ID="hidOTTimeStartMM" runat="server" />
                <asp:HiddenField ID="hidDeptID" runat="server" />
                <asp:HiddenField ID="hidOrganID" runat="server" />
                <asp:HiddenField ID="hidDeptName" runat="server" />
                <asp:HiddenField ID="hidOrganName" runat="server" />
                <asp:HiddenField ID="hidSalaryOrAdjustVisible" runat="server" />
                <asp:HiddenField ID="hidSalaryOrAdjust" runat="server" />
                <asp:HiddenField ID="hidAdjustInvalidDate" runat="server" />
                <asp:HiddenField ID="hidMealTime" runat="server" />
                <asp:HiddenField ID="hidOTTypeID" runat="server" />
                <asp:HiddenField ID="hidOTReasonMemo" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="hidBtn">
                    <%--隱藏Button區--%>
                    <%--按下暫存或送簽時，需先檢核後沒問題才跳出是否暫存/送簽的提示訊息--%>
                    <asp:Button ID="btnTransportAsk" runat="server" Text="btnTransportAsk" Style="display: none" />
                    <%-- <asp:Button ID="btnTempSave" runat="server" Text="btnTempSave" Style="display: none" />--%>
                    <asp:Button ID="btnSubmit" runat="server" Text="btnSubmit" Style="display: none" />
                    <asp:Button ID="btnDelete" runat="server" Text="btnDelete" Style="display: none" />
                    <asp:Button ID="btnReject" runat="server" Text="btnReject" Style="display: none" />
                    <asp:Button ID="btnQuery" runat="server" Text="btnQuery" Style="display: none" />
                    <%-- <asp:Button ID="webserverCall" runat="server" Text="webserver call ashx" OnClick="btnOverTimeApprovedHandler_Click" />--%>
                    <asp:Button ID="jqueryCall" runat="server" Text="jquery call ashx" OnClientClick="callashx();return false;"
                        Style="display: none" />
                    <asp:Button ID="ClearSubmitCache" runat="server" Text="ClearSubmitCache" Style="display: none" />
                    <%--隱藏Button區--%>
                    <asp:HiddenField ID="callHandlerUrl" runat="server" />
                    <asp:HiddenField ID="hidGuidID" runat="server" />
                    <asp:HiddenField ID="hidUserID" runat="server" />
                    <asp:HiddenField ID="hidOTRegisterCompID" runat="server"></asp:HiddenField>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
<script type="text/javascript">
    <!--
    //GridView長顏色
    //可有效避免底層的GridView事件複寫掉寫好的長顏色事件
    //是的，這部分就是要置底，不要換位置
    $(function () {
        $("a[id$=btnFirstPage],a[id$=btnPreviousPage],a[id$=btnNextPage],a[id$=btnLastPage],input[id$=btnPerPageRecord],input[id$=btnGoPage]").click(function () {
            gridClear();
        });
        $("input[id$=hldOTStatus]").each(function () {
            if ($(this).val() == "2") {
                $(this).parent().parent().find("td").css("background-color", "Yellow");
            } else if ($(this).val() == "4" || $(this).val() == "5" || $(this).val() == "6") {
                $(this).parent().parent().find("td").css("background-color", "LightGray");
            } else {
                $(this).parent().parent().find("td").css("background-color", "White");
            }
        });
    })
       -->
</script>
</html>
