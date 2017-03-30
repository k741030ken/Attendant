<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4202.aspx.vb" Inherits="OV_OV4202" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
    <!--

        function callashx() {

            var ajaxUrl = $('#callHandlerUrl').val() + 'HandlerForOverTime/OverTimeUpdateSingleDataApprovedHandler.ashx'
            if (ajaxUrl) {
                var CompIDValue = $('#lblCompID').text();
                var UserIDValue = $('#hidUserID').val();
                var UserCompValue = $('#hidUserComp').val();
                var OTEmpIDValue = $('#lblOTEmpID').text();
                var AttachIDValue = $('#hidAttachID').val();
                var MealFlagValue = ($('#chkMealFlag').prop("checked") ? "true" : "false");
                var DateStartValue = $('#ucOTStartDate_txtDate').val();
                var DateEndValue = $('#ucOTEndDate_txtDate').val();
                var OTTimeEndHHValue = $('#ucOTEndTime_ddlHH').val();
                var OTTimeEndMMValue = $('#ucOTEndTime_ddlMM').val();
                var OTTimeStartHHValue = $('#ucOTStartTime_ddlHH').val();
                var OTTimeStartMMValue = $('#ucOTStartTime_ddlMM').val();
                //var DeptIDValue = $('#lblDeptID').text();
                var OrganIDValue = $('#lblOrganID').text();
                var SalaryOrAdjustValue = $('#ddlSalaryOrAdjust').val();
                var AdjustInvalidDateValue = $('#lblAdjustInvalidDate').text();
                var MealTimeValue = $('#txtMealTime').val();
                var OTTypeIDValue = $('#ddlCodeCName').val();
                var OTReasonMemoValue = $('#txtOTReasonMemo').val();
                var OTTxnIDValue = $('#hidOTTxnID').val();

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
                    OrganID: OrganIDValue,
                    SalaryOrAdjust: SalaryOrAdjustValue,
                    AdjustInvalidDate: AdjustInvalidDateValue,
                    MealTime: MealTimeValue,
                    OTTypeID: OTTypeIDValue,
                    OTReasonMemo: OTReasonMemoValue,
                    OTTxnID: OTTxnIDValue
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

        //執行送簽
        function ExecSubmit() {
            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
            btnExecutes.setAttribute("disabled", "disabled");

            document.getElementById("jqueryCall").click();
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
                            if (data.Message = "送簽成功") {
                                $('#hidSubmitStatus').val("Y");
                                document.getElementById("btnAfterSubmit").click();
                                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                                btnExecutes.removeAttribute("disabled");
                            }
                        }
                    }
                    else {
                        if (data.Message && data.Message != "") {
                            //$('#getAjaxData').html(data.Message);
                            alert(data.Message);
                            var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                            btnExecutes.removeAttribute("disabled");
                        }
                    }
                }
                else {
                    //$('#getAjaxData').html("無回傳資料!");
                    alert("無回傳資料!");
                    var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                    btnExecutes.removeAttribute("disabled");
                }
            });
            //ajax 連線失敗
            ajaxControl.fail(function (data) {
                //$('#getAjaxData').html("連線失敗!!");
                alert("連線失敗!!");
                document.getElementById("ClearSubmitCache").click();
                var btnExecutes = window.parent.frames[0].document.getElementById("ucButtonPermission_btnExecutes");
                btnExecutes.removeAttribute("disabled");
            });
        }


        function TempSaveAsk() {
            if (!confirm('是否要將此筆資料暫存?'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }

        function SubmitAsk() {
            if (!confirm('是否要將此筆資料送簽?'))
                return false;

            document.getElementById("btnTransportAsk").click();
        }

        function TempSave() {
            document.getElementById("btnTempSave").click();
        }

        function Submit() {
            document.getElementById("btnSubmit").click();
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
            var filename = $('#lblAttachName').text(); //附件名
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
    -->
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
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 0">
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
    <table class="tbl_Edit" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td align="center" style="height: 30px;" class="td_EditHeader">
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *公司
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server"></asp:Label>
                            &nbsp&nbsp
                            <asp:Label ID="lblCompName" Font-Size="15px" runat="server"></asp:Label>
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
                            <asp:Label ID="lblOTEmpID" Font-Size="15px" runat="server"></asp:Label>
                            &nbsp&nbsp
                            <asp:Label ID="lblOTEmpNameN" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *填單人
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <asp:Label ID="lblOTRegisterID" Font-Size="15px" runat="server"></asp:Label>
                            &nbsp&nbsp
                            <asp:Label ID="lblOTRegisterNameN" Font-Size="15px" runat="server"></asp:Label>
                            <asp:HiddenField ID="hidOTFormNO" runat="server"></asp:HiddenField>
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
                            <asp:HiddenField ID="hidSalaryOrAdjust" runat="server" />
                            <asp:Label ID="lbl_lblAdjustInvalidDate" Font-Size="15px" runat="server" Visible="False">補休失效日: </asp:Label>
                            <asp:Label ID="lblAdjustInvalidDate" Font-Size="15px" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            *加班起迄日期
                        </td>
                        <td align="left" width="20%" class="td_Edit">
                            <uc:ucCalender ID="ucOTStartDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                            ~
                            <uc:ucCalender ID="ucOTEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                                Width="80px" />
                            <asp:Button ID="ValidStartDate" runat="server" Style="display: none" />
                            <asp:Button ID="ValidEndDate" runat="server" Style="display: none" />
                        </td>
                        <td colspan="2" class="td_Edit">
                            <asp:Panel ID="pnlOTPayDate" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td align="left" width="10%" class="td_EditHeader">
                                            *計薪年月
                                        </td>
                                        <td align="left" width="20%" class="td_Edit">
                                            <asp:Label ID="lblOTPayDate" Font-Size="15px" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                            <asp:CheckBox ID="chkMealFlag" runat="server" Text="扣除用餐時數: " Font-Names="微軟正黑體"
                                ForeColor="Red" AutoPostBack="true" />
                            <asp:TextBox ID="txtMealTime" runat="server" MaxLength="3" Width="30px" OnKeyPress="if(((event.keyCode >= 48) && (event.keyCode <= 57)) || (event.keyCode == 46)) {event.returnValue = true;} else{event.returnValue = false;}"
                                ForeColor="Red" AutoPostBack="true">0</asp:TextBox>
                            <font color="red">分鐘</font>
                        </td>
                        <td colspan="2" align="left" class="td_Edit">
                            <asp:Panel ID="pnlCalcTotalTime" runat="server" Visible="false">
                                <asp:Literal ID="litTable" runat="server" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            加班類型
                        </td>
                        <td colspan="3" align="left" width="20%" class="td_Edit">
                            <asp:DropDownList ID="ddlCodeCName" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            加班原因
                        </td>
                        <td colspan="3" class="td_Edit" style="width: 20%" align="left">
                            <asp:TextBox ID="txtOTReasonMemo" runat="server" class="defTxtBoxStyle" CssClass="InputTextStyle_Thin"
                                Height="50px" MaxLength="200" Width="80%" TextMode="MultiLine" onkeyup="javascript:txtCount(this);"></asp:TextBox>
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
                            <%--<asp:Button ID="btnDownloadAttachJ" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                OnClientClick="openDownloadWindow();return false;" Text="附件下載" />--%>
                            <asp:Button ID="btnDownloadAttach" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                Text="附件下載" />
                            <asp:Button ID="updateAttachName" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                Text="更新附件檔名" Style="display: none;" />
                            <asp:Label ID="lblAttachName" runat="server" Text="目前無附件"></asp:Label>
                            <asp:HiddenField ID="labOTAttachment" runat="server" />
                            <asp:HiddenField ID="frameAttach" runat="server"></asp:HiddenField>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            最後異動人員
                        </td>
                        <td align="left" width="20%" class="td_Edit" colspan="3">
                            <asp:Label ID="lblLastChgID" Font-Size="15px" runat="server"></asp:Label>
                            <asp:Label ID="lblLastChgNameN" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%" class="td_EditHeader">
                            最後異動日期
                        </td>
                        <td align="left" width="20%" class="td_Edit" colspan="3">
                            <asp:Label ID="LastChgDate" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%--LastChgPanel--%>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <%--
    <br />--%>
    <asp:Label ID="lblTotalOTCalc" Font-Size="15px" runat="server"></asp:Label>月份已申報時數合計:&nbsp&nbsp送簽
    <asp:Label ID="lblSubmit" Font-Size="15px" runat="server"></asp:Label>
    小時&nbsp&nbsp&nbsp&nbsp核准
    <asp:Label ID="lblApproval" Font-Size="15px" runat="server"></asp:Label>
    小時&nbsp&nbsp&nbsp&nbsp駁回
    <asp:Label ID="lblReject" Font-Size="15px" runat="server"></asp:Label>
    小時<br />
    <%--隱藏Button區--%>
    <%--按下暫存或送簽時，需先檢核後沒問題才跳出是否暫存/送簽的提示訊息--%>
    <asp:Button ID="btnTransportAsk" runat="server" Text="btnTransportAsk" Style="display: none" />
    <asp:Button ID="btnTempSave" runat="server" Text="btnTempSave" Style="display: none" />
    <asp:Button ID="btnSubmit" runat="server" Text="btnSubmit" Style="display: none" />
    <asp:Button ID="jqueryCall" runat="server" Text="jquery call ashx" OnClientClick="callashx();return false;"
        Style="display: none" />
    <%--    <asp:Button ID="jqueryCallMulti" runat="server" Text="jquery call ashxMulti" OnClientClick="callashxMulti();return false;"
        Style="display: none" />--%>
    <asp:Button ID="ClearSubmitCache" runat="server" Text="ClearSubmitCache" Style="display: none" />
    <asp:Button ID="btnAfterSubmit" runat="server" Text="btnAfterSubmit" Style="display: none" />
    <asp:HiddenField ID="callHandlerUrl" runat="server" />
    <asp:HiddenField ID="hidAttachID" runat="server" />
    <asp:HiddenField ID="hidGuidID" runat="server" />
    <asp:HiddenField ID="hidUserID" runat="server" />
    <asp:HiddenField ID="hidUserComp" runat="server" />
    <asp:HiddenField ID="hidOTTxnID" runat="server" />
    <asp:HiddenField ID="hidSubmitStatus" runat="server" />
    <%--隱藏Button區--%>
    </form>
</body>
</html>
