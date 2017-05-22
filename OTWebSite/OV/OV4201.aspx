<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4201.aspx.vb" Inherits="OV_OV4201" %>

<%@ Register Src="~/component/uc1mintime.ascx" TagPrefix="uc" TagName="uc1MinTime" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <script type="text/javascript">


        //多筆送簽
        function callashxMulti() {
            var ajaxUrl = $('#callHandlerUrl').val() + 'HandlerForOverTime/OverTimeMultiDataApprovedHandler.ashx'
            if (ajaxUrl) {
                var postData = { //給參數 json格式
                    Platform: 'AP',
                    SystemID: 'OT',
                    TxnName: 'OV4201',
                    UserID: $('#lblOTRegisterID').text(),
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

        //單筆送簽-直接送簽
        function callashx() {

            var ajaxUrl = $('#callHandlerUrl').val() + 'HandlerForOverTime/OverTimeSingleDataApprovedHandler.ashx'
            if (ajaxUrl) {
                var CompIDValue = $('#hidCompID').val();
                var UserIDValue = $('#lblOTRegisterID').text();
                var UserCompValue = $('#hidOTRegisterCompID').val();
                var OTEmpIDValue = $('#txtOTEmpID').val();
                var AttachIDValue = $('#hidAttachID').val();
                var MealFlagValue = ($('#MealFlag').prop("checked") ? "true" : "false");
                var DateStartValue = $('#ucOTStartDate_txtDate').val();
                var DateEndValue = $('#ucOTEndDate_txtDate').val();
                var OTTimeEndHHValue = $('#ucOTEndTime_ddlHH').val();
                var OTTimeEndMMValue = $('#ucOTEndTime_ddlMM').val();
                var OTTimeStartHHValue = $('#ucOTStartTime_ddlHH').val();
                var OTTimeStartMMValue = $('#ucOTStartTime_ddlMM').val();
                var DeptIDValue = $('#lblDeptID').text();
                var OrganIDValue = $('#lblOrganID').text();
                var DeptNameValue = $('#lblDeptName').text();
                var OrganNameValue = $('#lblOrganName').text();
                var SalaryOrAdjustValue = $('#ddlSalaryOrAdjust').val();
                var AdjustInvalidDateValue = $('#lblAdjustInvalidDate').text();
                var MealTimeValue = $('#txtMealTime').val();
                var OTTypeIDValue = $('#ddlCodeCName').val();
                var OTReasonMemoValue = $('#txtOTReasonMemo').val();

                var postData = { //給參數 json格式
                    CompID: CompIDValue,
                    UserID: UserIDValue,
                    UserComp: UserCompValue,
                    OTEmpID: OTEmpIDValue,
                    AttachID: AttachIDValue,
                    MealFlag: MealFlagValue,
                    DateStart: DateStartValue,
                    DateEnd: DateEndValue,
                    OTTimeEndHH: OTTimeEndHHValue,
                    OTTimeEndMM: OTTimeEndMMValue,
                    OTTimeStartHH: OTTimeStartHHValue,
                    OTTimeStartMM: OTTimeStartMMValue,
                    DeptID: DeptIDValue,
                    OrganID: OrganIDValue,
                    DeptName: DeptNameValue,
                    OrganName: OrganNameValue,
                    SalaryOrAdjust: SalaryOrAdjustValue,
                    AdjustInvalidDate: AdjustInvalidDateValue,
                    MealTime: MealTimeValue,
                    OTTypeID: OTTypeIDValue,
                    OTReasonMemo: OTReasonMemoValue
                }
                type = "GET";
                //callAjaxControlForJson
                callAjaxControlForJson(ajaxUrl, postData, type);
            }
            else {
                return false;
                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                btnExecutes.removeAttribute("disabled");
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
                            //$('#getAjaxData').html(data.Message);
                            alert(data.Message);
                            document.getElementById("btnClearData").click();
                            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                            btnExecutes.removeAttribute("disabled");
                        }
                    }
                    else {
                        if (data.Message && data.Message != "") {
                            //$('#getAjaxData').html(data.Message);
                            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                            btnExecutes.removeAttribute("disabled");
                            alert(data.Message);
                        }
                    }
                }
                else {
                    //$('#getAjaxData').html("無回傳資料!");
                    var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                    btnExecutes.removeAttribute("disabled");
                    alert("無回傳資料!");
                }
            });
            //ajax 連線失敗
            ajaxControl.fail(function (data) {
                //$('#getAjaxData').html("連線失敗!!");
                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                btnExecutes.removeAttribute("disabled");
                alert("連線失敗!!");
                document.getElementById("ClearSubmitCache").click();
            });
        }

        //執行送簽
        function ExecSubmit(flag) {
            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
            btnExecutes.setAttribute("disabled", "disabled");

            if (flag == '1') {
                document.getElementById("jqueryCall").click();
            } else if (flag == '2') {
                document.getElementById("jqueryCallMulti").click();
            }
        }

        $(function () {
            $("#ucOTStartDate_txtDate").change(function () {
                $("#ValidStartDate").click();
            });
            $("#ucOTEndDate_txtDate").change(function () {
                $("#ValidEndDate").click();
            });
        });

        $(function () {
            $("#ucOTStartTime_ddlHH").change(function () {
                $("#ValidStartTime").click();
            });
            $("#ucOTStartTime_ddlMM").change(function () {
                $("#ValidStartTime").click();
            });
            $("#ucOTEndTime_ddlHH").change(function () {
                $("#ValidEndTime").click();
            });
            $("#ucOTEndTime_ddlMM").change(function () {
                $("#ValidEndTime").click();
            });
        });

        $(function () {
            $("#dialog-form").dialog({
                autoOpen: false,
                resizable: false,
                height: 240,
                width: 580,
                modal: false
            }).prev("#dialog-form").css("background", "red");
        });

        function OpenMsgBox() {
            $("#dialog-form").dialog("open");
        }

        function TempSaveAsk() {
            if (!confirm('是否要將此筆資料暫存?'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }

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

        function TempSave() {
            document.getElementById("btnTempSave").click();
        }

        function Submit() {
            document.getElementById("btnSubmit").click();
        }

        function Delete() {
            document.getElementById("btnDelete").click();
        }

        function txtCount(textControl) {
            //alert("Hi:" + textControl);
            try {
                var strLength = document.getElementById('txtOTReasonMemo').value.length
                //alert("Text Count = " + strLength)
                if (strLength >= 200) {
                    document.getElementById("lblOTReasonMemoCount").style.color = "Red";
                    alert("加班原因不得超出200字!");
                }
                else {
                    document.getElementById("lblOTReasonMemoCount").style.color = "Blue";
                }
                document.getElementById("lblOTReasonMemoCount").innerHTML = strLength + "/200";
            }
            catch (ex) {
                alert(ex.message);
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

        //上傳附件時，另開子視窗
        function openUploadWindow() {
            var url = document.getElementById("frameAttach").value;
            var popup = window.open(url, '檔案上傳', 'width = 650, height = 500');
            this.pollForWindowClosure(popup); //檢查子視窗是否被關，被關時觸發事件
        }

        //檢查子視窗是否被關，被關時觸發事件
        function pollForWindowClosure(popup) {
            if (popup.closed) {
                queryFileNameCount = 0;
                queryFileName();
                return;
            }
            setTimeout(function () { this.pollForWindowClosure(popup) }, 10);
        }

        //查詢附件名稱
        var queryFileNameCount = 0; //目前查詢次數
        var queryFileNameMaxCount = 10; //最多查詢次數
        function queryFileName() {
            queryFileNameCount++;
            var filename = $('#lblUploadStatus').text(); //附件名
            var searchstr = "無附件"; //要搜索的字串
            var start = 0; //開始搜索的位置
            var checkFilename = filename.indexOf(searchstr, start); //搜索是否有休關字串
            if (queryFileNameCount > queryFileNameMaxCount) { //有顯示附件名或查詢次達到最高次數就不再查詢
                return;
            }
            else {
                $('#updateAttachName').click(); //查詢
            }
            setTimeout(function () { this.queryFileName() }, 100); //0.1秒後再次查詢
        }


        //清除畫面的勾選狀態
        function gridClear() {
            document.getElementById("__SelectedRowsgvMain").value = "";
        }

        function funAction(Param) {
            switch (Param) {
                case "btnDelete":                
                    if (!hasSelectedRows('')) {
                        alert("未選取資料列！");
                        return false;
                    }
            }
        }
    
    </script>
    <style type="text/css">
        .td_EditHeader
        {
            border: 1px solid #5384e6;
            background-color: #e2e9fe;
            min-width: 110px;
            height: 20px;
            font-family: 微軟正黑體;
            font-size: 15px;
        }
        
        .td_Edit
        {
            font-size: 15px;
            height: 20px;
            border: 1px solid #89b3f5;
            min-width: 110px;
            font-family: 微軟正黑體;
        }
        
        .defTxtBoxStyle
        {
            font-size: 15px;
            font-family: 微軟正黑體;
            color: #AA9FAA;
        }
        
        divdialog
        {
            background-color: White;
            display: none;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr class="table_td_content">
            <td align="center" style="height: 30px;" class="td_EditHeader">
                <table class="tbl_Edit" cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *公司
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidCompID" runat="server"></asp:HiddenField>
                        </td>
                        <td align="left" width="10%" class="td_EditHeader">
                            *單位
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblDeptID" Font-Size="15px" runat="server"></asp:Label>
                            &nbsp&nbsp
                            <asp:Label ID="lblDeptName" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *科別
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblOrganID" Font-Size="15px" runat="server"></asp:Label>
                            &nbsp&nbsp
                            <asp:Label ID="lblOrganName" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班人
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:RadioButtonList ID="rblOTPerson" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">單筆加班人</asp:ListItem>
                                <asp:ListItem Value="1">多筆加班人</asp:ListItem>
                            </asp:RadioButtonList>
                            <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600"
                                ButtonText="..." />
                            <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtOTEmpID" MaxLength="6" runat="server"
                                Width="45px" Style="text-transform: uppercase" AutoPostBack="true"></asp:TextBox>
                            &nbsp&nbsp
                            <asp:Label ID="lblOTEmpNameN" Font-Size="15px" runat="server"></asp:Label>
                            <%-- <a href="#" class="btn btn-send" onclick="javascript:OpenMsgBox();">
                                <img src="../images/Icon_FAQ.png" alt="" />
                            </a>--%>
                            <a href="#" class="btn btn-send" onclick="javascript:alert('操作說明\n\n1.選擇多筆加班人且需上傳附檔時，請於第一筆資料建檔時上傳附檔\n2.若為相同檔案上傳，請勾選☑同上筆附檔');">
                                <img src="../images/Icon_FAQ.png" alt="" />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *填單人
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblOTRegisterID" Font-Size="15px" runat="server"></asp:Label>
                            <asp:Label ID="lblOTRegisterNameN" Font-Size="15px" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidOTRegisterCompID" runat="server"></asp:HiddenField>
                        </td>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班轉換方式:
                        </td>
                        <td align="left" class="td_Edit">
                            <asp:DropDownList ID="ddlSalaryOrAdjust" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="轉薪資"></asp:ListItem>
                                <asp:ListItem Value="2" Text="轉補休"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbl_lblAdjustInvalidDate" Font-Size="15px" runat="server" Visible="False">補休失效日</asp:Label>
                            <asp:Label ID="lblAdjustInvalidDate" Font-Size="15px" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班起迄日期
                        </td>
                        <td colspan="3" align="left" class="td_Edit">
                            <uc:ucCalender ID="ucOTStartDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                            ~
                            <uc:ucCalender ID="ucOTEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                            <asp:Button ID="ValidStartDate" runat="server" Style="display: none" />
                            <asp:Button ID="ValidEndDate" runat="server" Style="display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班開始時間
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <uc:uc1MinTime runat="server" ID="ucOTStartTime" />
                            <asp:Label ID="lblStartSex" runat="server" Text="女性不可以10點後加班" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Button ID="ValidStartTime" runat="server" Style="display: none" />
                        </td>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班結束時間
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <uc:uc1MinTime runat="server" ID="ucOTEndTime" />
                            <asp:Label ID="lblEndSex" runat="server" Text="女性不可以10點後加班" ForeColor="Red" Visible="false"></asp:Label>
                            <asp:Button ID="ValidEndTime" runat="server" Style="display: none" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" class="td_Edit">
                            *加班時數
                            <asp:Label ID="lblOTTotalTime" Font-Size="15px" runat="server"></asp:Label>
                            小時
                            <asp:Label ID="txtOTTotalDescript" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <br />
                            <asp:CheckBox ID="MealFlag" runat="server" Text="扣除用餐時數:" Font-Names="微軟正黑體" ForeColor="Red"
                                AutoPostBack="True" />
                            <asp:TextBox ID="txtMealTime" runat="server" MaxLength="3" Width="30px" OnKeyPress="if(((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46)) {event.returnValue = true;} else{event.returnValue = false;}"
                                AutoPostBack="True">0</asp:TextBox>
                            <font color="red">分鐘</font>
                        </td>
                        <td colspan="2" align="left" class="td_Edit">
                            <asp:Panel ID="pnlCalcTotalTime" runat="server" Visible="true">
                                <asp:Literal ID="litTable" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班類型
                        </td>
                        <td colspan="3" align="left" width="20%" class="td_Edit">
                            <asp:DropDownList ID="ddlCodeCName" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班原因
                        </td>
                        <td colspan="3" class="td_Edit" style="width: 20%" align="left">
                            <asp:TextBox ID="txtOTReasonMemo" runat="server" class="defTxtBoxStyle" CssClass="InputTextStyle_Thin"
                                Height="50px" MaxLength="200" Width="80%" TextMode="MultiLine" onchange="javascript:txtCount(this);"></asp:TextBox>
                            <%--<asp:TextBox ID="txtOTReasonMemo1" runat="server" class="defTxtBoxStyle" CssClass="InputTextStyle_Thin"
                                Height="50px" MaxLength="200" Width="80%" TextMode="MultiLine" onFocus="this.value=''"
                                onchange="javascript:txtCount(this);"></asp:TextBox>--%>
                            <asp:Label ID="lblOTReasonMemoCount" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            上傳附件
                        </td>
                        <td colspan="3" class="td_Edit" style="width: 20%" align="left">
                            <asp:Button ID="btnUploadAttach" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                OnClientClick="openUploadWindow();return false;" Text="附件上傳" />
                            <asp:Label ID="lblUploadStatus" Font-Size="15px" runat="server"></asp:Label>
                            <asp:CheckBox ID="chkCopyAtt" runat="server" Text="同上筆附檔" Visible="False" AutoPostBack="True" />
                            <br />
                            <asp:Button ID="updateAttachName" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                Text="更新附件檔名" Style="display: none;" />
                            <asp:HiddenField ID="frameAttach" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblTotalOTCalc" Font-Size="15px" runat="server"></asp:Label>
    <div class="hidField">
        <asp:HiddenField ID="hidGridViewStartTime" runat="server" />
        <asp:HiddenField ID="hidGridViewSearchStartTime" runat="server" />
        <asp:HiddenField ID="hidGridViewSearchEndTime" runat="server" />
    </div>
    <div class="hidBtn">
        <%--隱藏Button區--%>
        <%--按下暫存或送簽時，需先檢核後沒問題才跳出是否暫存/送簽的提示訊息--%>
        <asp:Button ID="btnTransportAsk" runat="server" Text="btnTransportAsk" Style="display: none" />
        <asp:Button ID="btnTempSave" runat="server" Text="btnTempSave" Style="display: none" />
        <asp:Button ID="btnSubmit" runat="server" Text="btnSubmit" Style="display: none" />
        <asp:Button ID="btnDelete" runat="server" Text="btnDelete" Style="display: none" />
        <asp:Button ID="btnReject" runat="server" Text="btnReject" Style="display: none" />
        <asp:Button ID="btnClearData" runat="server" Text="btnClearData" Style="display: none" />
        <asp:Button ID="jqueryCall" runat="server" Text="jquery call ashx" OnClientClick="callashx();return false;"
            Style="display: none" />
        <asp:Button ID="jqueryCallMulti" runat="server" Text="jquery call ashxMulti" OnClientClick="callashxMulti();return false;"
            Style="display: none" />
        <asp:Button ID="ClearSubmitCache" runat="server" Text="ClearSubmitCache" Style="display: none" />
        <%--隱藏Button區--%>
        <asp:HiddenField ID="callHandlerUrl" runat="server" />
        <asp:HiddenField ID="hidAttachID" runat="server" />
        <asp:HiddenField ID="test02" runat="server" />
        <asp:HiddenField ID="hidGuidID" runat="server" />
        <asp:HiddenField ID="SubmitFlag" runat="server" />
    </div>
    <div id="getAjaxData">
    </div>
    <br />
    <br />
<%--    <asp:Panel ID="PnlgvMain" runat="server" Visible="false">--%>
         <table width="100%" height="100%" class="tbl_Content" id="PnlgvMain" runat="server" style="font-family:@微軟正黑體; width: 100%; height: 100%" visible="false">
            <%--            <tr>
                <td style="width: 100%">
                    <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                </td>
            </tr>--%>
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true"
                        AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="OTFormNO,OTStatus,OTCompID,NameN,OTEmpID,OTTypeID,CodeCName,OTDate,OTTime,OTTotalTime,FileName,OTSeq,OTRegisterID,OTRegisterComp,OTFromAdvanceTxnId,OTTxnID"
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
                            <asp:BoundField DataField="CodeCName" HeaderText="加班類型名稱" ReadOnly="True" SortExpression="CodeCName">
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
<%--    </asp:Panel>--%>
    <br />
    <br />
    <div id="dialog-form" title="操作說明" class="divdialog" style="display: none;">
        <div style="background-attachment: fixed; background-image: url(images/signin2.jpg);
            background-repeat: no-repeat; background-color: #DDDDDD;">
            <p class="validateTips">
            </p>
            <table>
                <tr>
                    <td>
                        1.選擇多筆加班人且需上傳附檔時，請於第一筆資料建檔時上傳附檔
                    </td>
                </tr>
                <tr>
                    <td>
                        2.若為相同檔案上傳，請勾選<input type="checkbox" checked="true" disabled="false" />同上筆附檔
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript">
    <!--
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
    });
       -->
</script>
</html>
