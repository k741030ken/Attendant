<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM9010.aspx.vb" Inherits="OM_OM9010" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery.blockUI.js"></script>

    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>

    <link rel="stylesheet" type="text/css" href="../css/primitives/demo/css/primitives.latest.css" media="screen" />
    <script type="text/javascript" src="../css/primitives/demo/js/primitives.min.js"></script>

    <style type="text/css">
        .ui-datepicker
        {
        	font-size: 10px;
        }
        .ui-datepicker-trigger 
        {
        	padding-left: 5px;
        }
        .bp-cursor-frame
        {
        	border-color: #FF7744;
        }
        .bp-item2
        {
            position: absolute;
            overflow: visible;/* redefine this atttribute in bp-item class in order to place items outside of boudaries*/
        }
        .placeholder
        {
        	margin-left: 12.5px;
        }        
        .bp-badge2
        {
            font-size: 12px;
            line-height: 12px;
            text-align: center;
            text-decoration: none;
            vertical-align: middle;
            font-weight: bold;
            padding: 4px;
            float: left;
            width: 14px; 
            height: 12px;
        }
        #MenuBox
        {
            height: 719px;
            /*width: 300px;*/
            margin: 2px;
            border: 1px solid gray;
            overflow-x: hidden;
            overflow-y: auto;
            display: inline-block;
        }
        #MainOrgan
        {
        	height: 725px;
       	    overflow-x: auto;
            overflow-y: hidden;
            display: inline-block;
        }
        #OrganBox
        {
       	    height: 719px;
        }
        .OrganBox
        {
            height: 719px;
            width: 500px;
            margin: 2px;
            border: 1px solid gray;
       	    float: left;
       	    text-align: center;
        }         
        .OrganCenter
        {
        	width: 450px;
       	    margin: 20px auto;
        }
        .OrganDiagram
        {
        	text-align: left;
        	width: 500px;
        	height: 450px;
        }
        @media print 
        {
            .bt-item-frame {
                border: 1px solid #666666;
            }
        	.bp-item { 
                margin: 2px;
                font-size: 12px;
                display: inline-block;
            }
            .bp-title {
                font-size: 14px;
            }
            .bp-photo-frame {
                float: left;
                margin-bottom: 10px;
            }
            .bp-badge2 { 
                display: none;
            }
        }
    </style>
    <script type="text/javascript">
        //$.datepicker.regional["zh-TW"] = {
        //    dayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
        //    dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
        //    monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        //    monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        //    prevText: "上個月",
        //    nextText: "下個月",
        //    weekHeader: "週",
        //    changeYear: false,
        //    changeMonth: false,
        //    showMonthAfterYear: false,
        //    showOtherMonths: true,
        //    showOn: "both",
        //    buttonImageOnly: true,
        //    buttonImage: "../images/calendar.gif",
        //    buttonText: "日曆"
        //};
        //$.datepicker.setDefaults($.datepicker.regional['zh-TW']);

        $(function () {
            //$("#frmContent").css("width", $("#MenuBox").width() + $("#MainOrgan").width() + 10);
            //var MainOrganWidth = $("#frmContent").width() - $("#MenuBox").width();
            //$("#MainOrgan").css("width", MainOrganWidth - 20);

            //$("#txtQryDate").datepicker();

            if ($("#tvOrgan").find("table").length > 0) {
                $("#tvOrgan").find("table").each(function () {
                    var color = $(this).find("span").attr("href");
                    $(this).css("background-color", color);
                    $(this).find("td:last").css("width", "100%");

                    var OrganID = $(this).find("span").attr("target");

                    if ($(this).find("span").text().endsWith("(未生效)")) {
                        $(this).find("input:checkbox").prop("disabled", true);
                    }

                    $(this).find("input:checkbox").click(function () {
                        if ($("#tvOrgan").find("table").find("input:checkbox:checked").length > 3) {
                            alert('最多比較三個單位!');
                            return false;
                        }
                    });

                    $(this).find("input:checkbox").change(function () {
                        if ($(this).prop("checked")) {
                            var Cnt = $("#tvOrgan").find("table").find("input:checkbox:checked").length;
                            if (Cnt <= 3) {
                                showSubmitting();
                                setTimeout(function () {
                                    LoadOrgan(OrganID);

//                                    if (Cnt == 1) {
//                                        $("#divSample").find("div.OrganDiagram").css("width", 1500);
//                                        LoadOrgan(OrganID);

//                                        $("#OrganBox").find("div.OrganBox").css("width", 1500);
//                                        $("#OrganBox").find("div.OrganDiagram").css("width", 1500);
//                                    }
//                                    if (Cnt == 2) {
//                                        $("#divSample").find("div.OrganDiagram").css("width", 750);
//                                        LoadOrgan(OrganID);

//                                        $("#OrganBox").find("div.OrganBox").css("width", 750);
//                                        $("#OrganBox").find("div.OrganDiagram").css("width", 750);
//                                        //$("#OrganBox").find("div.OrganDiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
//                                    }
//                                    if (Cnt == 3) {
//                                        $("#divSample").find("div.OrganDiagram").css("width", 500);
//                                        LoadOrgan(OrganID);

//                                        $("#OrganBox").find("div.OrganBox").css("width", 500);
//                                        $("#OrganBox").find("div.OrganDiagram").css("width", 500);
//                                        //$("#OrganBox").find("div.OrganDiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
//                                    }

                                    var OrganCnt = $("#OrganBox").find("div.OrganBox").length;
                                    $("#OrganBox").css("width", 3 * 506);

                                    hidePopupWindow();
                                }, 50);
                            }
                        }
                        else {
                            $("#OrganBox").find("#OrganBox_" + OrganID).remove();
                            DeleteOrgan(OrganID);

//                            var Cnt = $("#tvOrgan").find("table").find("input:checkbox:checked").length;
//                            if (Cnt == 1) {
//                                $("#OrganBox").find("div.OrganBox").css("width", 1500);
//                                $("#OrganBox").find("div.OrganDiagram").css("width", 1500);
//                                //$("#OrganBox").find("div.OrganDiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
//                            }
//                            if (Cnt == 2) {
//                                $("#OrganBox").find("div.OrganBox").css("width", 750);
//                                $("#OrganBox").find("div.OrganDiagram").css("width", 750);
//                                //$("#OrganBox").find("div.OrganDiagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
//                            }

                            var OrganCnt = $("#OrganBox").find("div.OrganBox").length;
                            $("#OrganBox").css("width", 3 * 506);
                        }

                    });
                });
            }

            //$("#frmContent").resize(function () {
            //    var MainOrganWidth = $("#frmContent").width() - $("#MenuBox").width();
            //    $("#MainOrgan").css("width", MainOrganWidth - 20);
            //});

            //$("#MenuBox").resize(function () {
            //    var MainOrganWidth = $("#frmContent").width() - $("#MenuBox").width();
            //    $("#MainOrgan").css("width", MainOrganWidth - 20);
            //});
        });

        function DoQuery() {
            var QryDate = $("#txtQryDate_txtDate").val();

            if (QryDate == "" || QryDate == "____/__/__") {
                alert("請輸入日期");
                return false;
            } else if ($("#txtQryDate_mevSDate").is(":visible")) {
                alert("日期格式錯誤");
                return false;
            }
        }

        function DeleteOrgan(OrganID) {
            $.ajax({
                type: "POST",
                url: "OM9010.aspx/DeleteOrganData",
                data: "{OrganID: '" + OrganID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {},
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function LoadOrgan(OrganID) {
            $.ajax({
                type: "POST",
                url: "OM9010.aspx/GetOrganData",
                data: "{OrganID: '" + OrganID + "', QueryDate: '" + $("#txtQryDate_txtDate").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    var content = $("#divSample").html();
                    content = content.replace("Organ_", "Organ_" + OrganID);
                    content = content.replace("OrganBox_", "OrganBox_" + OrganID);
                    content = content.replace("btnOpen_", "btnOpen_" + OrganID);
                    content = content.replace("btnClose_", "btnClose_" + OrganID);
                    content = content.replace("btnDownload_", "btnDownload_" + OrganID);
                    content = content.replace("btnPrint_", "btnPrint_" + OrganID);
                    content = content.replace("hidEmpCnt_", "hidEmpCnt_" + OrganID);
                    content = content.replace("hidUnderCnt_", "hidUnderCnt_" + OrganID);
                    content = content.replace("hidTotCnt_", "hidTotCnt_" + OrganID);
                    content = content.replace("ddlOrgan_", "ddlOrgan_" + OrganID);
                    content = content.replace("ddlWorkType_", "ddlWorkType_" + OrganID);
                    content = content.replace("ddlPosition_", "ddlPosition_" + OrganID);
                    content = content.replace("totSalary_", "totSalary_" + OrganID);
                    content = content.replace("avgTotSen_", "avgTotSen_" + OrganID);
                    content = content.replace("avgSen_", "avgSen_" + OrganID);
                    content = content.replace("avgOrg_", "avgOrg_" + OrganID);
                    content = content.replace("avgAge_", "avgAge_" + OrganID);
                    content = content.replace("avgRank_", "avgRank_" + OrganID);
                    content = content.replace("avgPos_", "avgPos_" + OrganID);
                    content = content.replace("avgWork_", "avgWork_" + OrganID);
                    content = content.replace("avgFlow_", "avgFlow_" + OrganID);
                    $("#OrganBox").append(content);

                    var options = new primitives.orgdiagram.Config();
                    var items = $.parseJSON(data.d);
                    options.items = items;
                    options.cursorItem = 0;
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.templates = [getRegularTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.onHighlightRender = onHighlightRender;
                    options.defaultTemplateName = "regularTemplate";
                    options.normalItemsInterval = 20;
                    options.selectionPathMode = primitives.orgdiagram.SelectionPathMode.None;
                    options.onCursorChanged = function (e, data) {
                        $("#ddlOrgan_" + OrganID).val(data.context.id);
                        $("#ddlOrgan_" + OrganID).change();
                    };
                    $("#Organ_" + OrganID).orgDiagram(options);

                    items.sort(function (a, b) {
                        return a["id"] > b["id"] ? 1 : (a["id"] == b["id"] ? 0 : -1);
                    });

                    $("#ddlOrgan_" + OrganID).append("<option value='ALL_" + OrganID + "'>全部</option>");
                    for (var i in items) {
                        $("#ddlOrgan_" + OrganID).append("<option value='" + items[i]["id"] + "'>" + items[i]["title"] + "</option>");
                        if (items[i]["id"] == OrganID) {
                            $("#hidEmpCnt_" + OrganID).val(items[i]["EmpCnt"]);
                            $("#hidUnderCnt_" + OrganID).val(items[i]["TotCnt"] - items[i]["EmpCnt"]);
                            $("#hidTotCnt_" + OrganID).text(items[i]["TotCnt"]);
                        }
                    }

                    $("#OrganBox_" + OrganID).attr("name", OrganID);
                    $("#btnOpen_" + OrganID).attr("name", OrganID);
                    $("#btnClose_" + OrganID).attr("name", OrganID);
                    $("#btnDownload_" + OrganID).attr("name", OrganID);
                    $("#btnPrint_" + OrganID).attr("name", OrganID);
                    $("#ddlOrgan_" + OrganID).attr("name", OrganID);
                    $("#ddlPosition_" + OrganID).attr("name", OrganID);
                    $("#ddlWorkType_" + OrganID).attr("name", OrganID);

                    GetPosition(OrganID, OrganID);
                    GetWorkType(OrganID, OrganID);
                    GetAvgData(OrganID, $("#txtQryDate_txtDate").val(), "", "", "", "");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function DiagramOpen(OrganID) {
            $("#Organ_" + OrganID).orgDiagram("option", {
                minimalVisibility: parseInt(1, 10),
                cursorItem: OrganID
            });
            $("#Organ_" + OrganID).orgDiagram("update", primitives.common.UpdateMode.Refresh);
        }

        function DiagramClose(OrganID) {
            $("#Organ_" + OrganID).orgDiagram("option", {
                minimalVisibility: parseInt(2, 10),
                cursorItem: 0
            });
            $("#Organ_" + OrganID).orgDiagram("update", primitives.common.UpdateMode.Refresh);
        }

        function DiagramPrint(OrganID) {
            var OrganCnt = $("#OrganBox").find("div.OrganBox").length;
            
            $("#plButton").hide();
            $(".TrMenuBox").hide();

            if (OrganID == "ALL") {
                //if (OrganCnt > 1) {
                $("body").css("zoom", "50%");
                //}
            } else {
                $("#OrganBox").find("div.OrganBox").hide();
                $("#OrganBox_" + OrganID).show();
            }

            window.print();

            $("#plButton").show();
            $(".TrMenuBox").show();

            if (OrganID == "ALL") {
                $("body").css("zoom", "100%");
            } else {
                $("#OrganBox").find("div.OrganBox").show();
            }
        }

        function OrganDownload(OrganID) {
            var EmpCnt = $("#hidEmpCnt_" + OrganID).val();
            var UnderCnt = $("#hidUnderCnt_" + OrganID).val();
            var TotCnt = $("#hidTotCnt_" + OrganID).text();

            var QryOrgID = $("#ddlOrgan_" + OrganID).val().replace("ALL_", "");
            var QryWork = $("#ddlWorkType_" + OrganID).val();
            var QryPos = $("#ddlPosition_" + OrganID).val();
            var QryDate = $("#txtQryDate_txtDate").val();

            var totSalary = $("#totSalary_" + OrganID).text();
            var avgTotSen = $("#avgTotSen_" + OrganID).text();
            var avgSen = $("#avgSen_" + OrganID).text();
            var avgOrg = $("#avgOrg_" + OrganID).text();
            var avgAge = $("#avgAge_" + OrganID).text();
            var avgRank = $("#avgRank_" + OrganID).text();
            var avgPos = $("#avgPos_" + OrganID).text();
            var avgWork = $("#avgWork_" + OrganID).text();
            var avgFlow = $("#avgFlow_" + OrganID).text();

            var param = "QryCnt=1&OrganID=" + OrganID + "&EmpCnt=" + EmpCnt + "&UnderCnt=" + UnderCnt + "&TotCnt=" + TotCnt;
            param = param + "&QryOrgID=" + QryOrgID + "&QryWork=" + QryWork + "&QryPos=" + QryPos + "&QryDate=" + QryDate;
            param = param + "&totSalary=" + escape(totSalary) + "&avgAge=" + avgAge + "&avgTotSen=" + avgTotSen + "&avgSen=" + avgSen;
            param = param + "&avgOrg=" + avgOrg + "&avgRank=" + avgRank + "&avgPos=" + avgPos + "&avgWork=" + avgWork + "&avgFlow=" + avgFlow;
            $("#Downloadframe").attr("src", "OM9011.aspx?" + param);
        }

        function OrganDownloadAll() {
            var OrganCnt = $("#OrganBox").find("div.OrganBox").length;
            if (OrganCnt <= 0) {
                alert("未勾選任何單位！");
                return;
            }
            else {
                var split = "", OrganID = "", QryDate = "", EmpCnt = "", UnderCnt = "", TotCnt = "", QryOrgID = "", QryWork = "", QryPos = "";
                var totSalary = "", avgTotSen = "", avgSen = "", avgOrg = "", avgAge = "", avgRank = "", avgPos = "", avgWork = "", avgFlow = "";

                for (var i = 0; i < OrganCnt; i++) {
                    if (i > 0) { split = "|"; }
                    var DownloadOrganID = $("#OrganBox").find("div.OrganBox").eq(i).attr("name");

                    OrganID += split + DownloadOrganID;
                    QryDate += split + $("#txtQryDate_txtDate").val();
                    EmpCnt += split + $("#hidEmpCnt_" + DownloadOrganID).val();
                    UnderCnt += split + $("#hidUnderCnt_" + DownloadOrganID).val();
                    TotCnt += split + $("#hidTotCnt_" + DownloadOrganID).text();

                    QryOrgID += split + $("#ddlOrgan_" + DownloadOrganID).val().replace("ALL_", "");
                    QryWork += split + $("#ddlWorkType_" + DownloadOrganID).val();
                    QryPos += split + $("#ddlPosition_" + DownloadOrganID).val();

                    totSalary += split + $("#totSalary_" + DownloadOrganID).text();
                    avgTotSen += split + $("#avgTotSen_" + DownloadOrganID).text();
                    avgSen += split + $("#avgSen_" + DownloadOrganID).text();
                    avgOrg += split + $("#avgOrg_" + DownloadOrganID).text();
                    avgAge += split + $("#avgAge_" + DownloadOrganID).text();
                    avgRank += split + $("#avgRank_" + DownloadOrganID).text();
                    avgPos += split + $("#avgPos_" + DownloadOrganID).text();
                    avgWork += split + $("#avgWork_" + DownloadOrganID).text();
                    avgFlow += split + $("#avgFlow_" + DownloadOrganID).text();
                }

                var param = "QryCnt=" + OrganCnt + "&OrganID=" + OrganID + "&EmpCnt=" + EmpCnt + "&UnderCnt=" + UnderCnt + "&TotCnt=" + TotCnt;
                param = param + "&QryOrgID=" + QryOrgID + "&QryWork=" + QryWork + "&QryPos=" + QryPos + "&QryDate=" + QryDate;
                param = param + "&totSalary=" + escape(totSalary) + "&avgAge=" + avgAge + "&avgTotSen=" + avgTotSen + "&avgSen=" + avgSen;
                param = param + "&avgOrg=" + avgOrg + "&avgRank=" + avgRank + "&avgPos=" + avgPos + "&avgWork=" + avgWork + "&avgFlow=" + avgFlow;

                $("#Downloadframe").attr("src", "OM9011.aspx?" + param);
            }
        }

        function changeOrgan(e) {
            var OrganID = e.name;
            var SelectedOrganID = e.value.replace("ALL_", "");
            var OrganFlag = (e.value.indexOf("ALL_") >= 0 ? "" : "S");

            $.ajax({
                type: "POST",
                url: "OM9010.aspx/GetSingleOrganData",
                data: "{OrganID: '" + OrganID + "', SelectedOrganID: '" + SelectedOrganID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    var options = new primitives.orgdiagram.Config();
                    var items = $.parseJSON(data.d);
                    options.items = items;
                    if (OrganFlag == "") {
                        options.cursorItem = 0;
                    } else {
                        options.cursorItem = SelectedOrganID;
                    }
                    options.hasSelectorCheckbox = primitives.common.Enabled.False;
                    options.templates = [getRegularTemplate()];
                    options.onItemRender = onTemplateRender;
                    options.onHighlightRender = onHighlightRender;
                    options.defaultTemplateName = "regularTemplate";
                    options.normalItemsInterval = 20;
                    options.selectionPathMode = primitives.orgdiagram.SelectionPathMode.None;
                    options.onCursorChanged = function (e, data) {
                        $("#ddlOrgan_" + OrganID).val(data.context.id);
                        $("#ddlOrgan_" + OrganID).change();
                    };
                    $("#Organ_" + OrganID).orgDiagram(options);
                    $("#Organ_" + OrganID).orgDiagram("update", primitives.common.UpdateMode.Refresh);

                    GetPosition(OrganID, SelectedOrganID);
                    GetWorkType(OrganID, SelectedOrganID);

                    GetAvgData(SelectedOrganID, $("#txtQryDate_txtDate").val(), OrganID, "", "", OrganFlag);

                    //for (var i in items) {
                    //    if (items[i]["id"] == SelectedOrganID) {
                    //        $("#hidTotCnt_" + OrganID).text(items[i]["TotCnt"]);
                    //    }
                    //}
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function changePosWork(e) {
            var OrganID = e.name;
            var SelectedOrganID = $("#ddlOrgan_" + OrganID).val().replace("ALL_", "");
            var SelectedPositionID = $("#ddlPosition_" + OrganID).val();
            var SelectedWorkTypeID = $("#ddlWorkType_" + OrganID).val();
            var OrganFlag = ($("#ddlOrgan_" + OrganID).val().indexOf("ALL_") >= 0 ? "" : "S");

            GetAvgData(SelectedOrganID, $("#txtQryDate_txtDate").val(), OrganID, SelectedPositionID, SelectedWorkTypeID, OrganFlag);
        }

        function GetWorkType(OrganID, SelectedOrganID) {
            $.ajax({
                type: "POST",
                url: "OM9010.aspx/GetWorkType",
                data: "{OrganID: '" + SelectedOrganID + "', QueryDate: '" + $("#txtQryDate_txtDate").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#ddlWorkType_" + OrganID).empty().append("<option value=''>---請選擇---</option>");

                    var items = $.parseJSON(data.d);
                    for (var i in items) {
                        $("#ddlWorkType_" + OrganID).append("<option value='" + items[i]["WorkTypeID"] + "'>" + items[i]["WorkTypeName"] + "</option>");
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function GetPosition(OrganID, SelectedOrganID) {
            $.ajax({
                type: "POST",
                url: "OM9010.aspx/GetPosition",
                data: "{OrganID: '" + SelectedOrganID + "', QueryDate: '" + $("#txtQryDate_txtDate").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    $("#ddlPosition_" + OrganID).empty().append("<option value=''>---請選擇---</option>");

                    var items = $.parseJSON(data.d);
                    for (var i in items) {
                        $("#ddlPosition_" + OrganID).append("<option value='" + items[i]["PositionID"] + "'>" + items[i]["PositionName"] + "</option>")
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function GetAvgData(OrganID, QueryDate, MainOrganID, PositionID, WorkTypeID, OrganFlag) {
            $.ajax({
                type: "POST",
                url: "OM9010.aspx/GetAvgData",
                data: "{OrganID: '" + OrganID + "', QueryDate: '" + QueryDate + "', PositionID: '" + PositionID + "', WorkTypeID: '" + WorkTypeID + "', OrganFlag: '" + OrganFlag + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    var items = $.parseJSON(data.d);
                    if (items.length > 0) {
                        if (MainOrganID != "") {
                            //$("#hidTotCnt_" + MainOrganID).text(items[0]["totCnt"]);
                            $("#avgAge_" + MainOrganID).text(items[0]["avgAge"]);
                            $("#totSalary_" + MainOrganID).text(items[0]["totSalary"]);
                            $("#avgSen_" + MainOrganID).text(items[0]["avgSen"]);
                            $("#avgTotSen_" + MainOrganID).text(items[0]["avgSen_SPHOLD"]);
                            $("#avgOrg_" + MainOrganID).text(items[0]["avgOrgSen"]);
                            $("#avgRank_" + MainOrganID).text(items[0]["avgRankSen"]);
                            $("#avgPos_" + MainOrganID).text(items[0]["avgPosSen"]);
                            $("#avgWork_" + MainOrganID).text(items[0]["avgWorkSen"]);
                            $("#avgFlow_" + MainOrganID).text(items[0]["avgFlowSen"]);
                        } else {
                            //$("#hidTotCnt_" + MainOrganID).text(items[0]["totCnt"]);
                            $("#avgAge_" + OrganID).text(items[0]["avgAge"]);
                            $("#totSalary_" + OrganID).text(items[0]["totSalary"]);
                            $("#avgSen_" + OrganID).text(items[0]["avgSen"]);
                            $("#avgTotSen_" + OrganID).text(items[0]["avgSen_SPHOLD"]);
                            $("#avgOrg_" + OrganID).text(items[0]["avgOrgSen"]);
                            $("#avgRank_" + OrganID).text(items[0]["avgRankSen"]);
                            $("#avgPos_" + OrganID).text(items[0]["avgPosSen"]);
                            $("#avgWork_" + OrganID).text(items[0]["avgWorkSen"]);
                            $("#avgFlow_" + OrganID).text(items[0]["avgFlowSen"]);
                        }
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function onTemplateRender(event, data) {
            var hrefElement = data.element.find("[name=readmore]");
            switch (data.renderingMode) {
                case primitives.common.RenderingMode.Create:
                    /* Initialize widgets here */
                    hrefElement.click(function (e) {
                        /* Block mouse click propogation in order to avoid layout updates before server postback*/
                        primitives.common.stopPropagation(e);
                    });
                    break;
                case primitives.common.RenderingMode.Update:
                    /* Update widgets here */
                    break;
            }

            var itemConfig = data.context;

            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });
            data.element.find("[name=photo]").attr({ "src": itemConfig.image });
            data.element.find("[name=title]").text(itemConfig.title);
            data.element.find("[name=boss]").text(itemConfig.boss);
            data.element.find("[name=bTitle]").text(itemConfig.bTitle);
            data.element.find("[name=EmpCnt]").text(itemConfig.EmpCnt + " 人");
            data.element.find("[name=UnderCnt]").text((itemConfig.TotCnt - itemConfig.EmpCnt) + " 人");
            data.element.find("[name=TotCnt]").text(itemConfig.TotCnt + "人");
            hrefElement.attr({ "href": itemConfig.href });
        }

        function getRegularTemplate() {
            var result = new primitives.orgdiagram.TemplateConfig();
            result.name = "regularTemplate";
            result.itemSize = new primitives.common.Size(200, 140);
            result.minimizedItemSize = new primitives.common.Size(10, 10);
            result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);
            result.minimizedItemBorderColor = primitives.common.Colors.Gray;

            var itemTemplate = jQuery(
			      '<div class="bp-item bp-corner-all bt-item-frame">'
				    + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: ' + (result.itemSize.width - 5) + 'px; height: 20px;">'
					    + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: ' + (result.itemSize.width - 12) + 'px; height: 18px; color: black;"></div>'
				    + '</div>'
				    + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 2px; width: 50px; height: 60px;">'
					    + '<img name="photo" style="height: 60px; width:50px;" />'
				    + '</div>'
                    + '<div name="boss" class="bp-item" style="top: 26px; left: 56px; width: ' + (result.itemSize.width - 59) + 'px; height: 18px; font-size: 12px;"></div>'
                    + '<div name="bTitle" class="bp-item" style="top: 44px; left: 56px; width: ' + (result.itemSize.width - 59) + 'px; height: 18px; font-size: 12px;"></div>'
                    + '<div class="bp-item" style="top: 62px; left: 56px; height: 18px; font-size: 12px;">單位人員：</div><div name="EmpCnt" class="bp-item" style="top: 62px; right: 2px; width: 75px; height: 18px; font-size: 12px;"></div>'
                    + '<div class="bp-item" style="top: 80px; left: 56px; height: 18px; font-size: 12px;">轄下人員：</div><div name="UnderCnt" class="bp-item" style="top: 80px; right: 2px; width: 75px; height: 18px; font-size: 12px;"></div>'
                    + '<a name="readmore" class="bp-item" style="top: ' + (result.itemSize.height - 20) + 'px; right: 12px; font-size: 13px; font-family: Arial; text-align: right; font-weight: bold; text-decoration: none;">人員明細 (總人數<span name="TotCnt"></span>)</a>'
                + '</div>'
                ).css({
                    width: result.itemSize.width + "px",
                    height: result.itemSize.height + "px"
                }).addClass("bp-item bp-corner-all bt-item-frame");
            result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

            var highlightTemplate = jQuery("<div></div>")
                    .css({
                        position: "absolute",
                        overflow: "visible",
                        width: (result.itemSize.width + result.highlightPadding.left + result.highlightPadding.right) + "px",
                        height: (result.itemSize.height + result.highlightPadding.top + result.highlightPadding.bottom) + "px",
                        "border-style": "solid",
                        "border-width": "1px"
                    }).addClass("bp-item2 bp-corner-all bp-cursor-frame");

            highlightTemplate.append("<div name='badge' class='bp-badge2 bp-item' style='top:45px; left:114px; z-index: 1000; color: white;'></div>");

            result.highlightTemplate = highlightTemplate.wrap('<div>').parent().html();

            return result;
        }

        function onHighlightRender(event, data) {
            switch (data.renderingMode) {
                case primitives.common.RenderingMode.Create:
                    /* Initialize widgets here */
                    break;
                case primitives.common.RenderingMode.Update:
                    /* Update widgets here */
                    break;
            }

            var itemConfig = data.context;

            var badge = data.element.find("[name=badge]");
            badge.text(itemConfig['badge']);
            badge.css({ "background-color": primitives.common.Colors.RoyalBlue });

            var width = data.element.outerWidth();
            var height = data.element.outerHeight();
            badge.css({ "left": width - 14, "top": height - 14 });
        }
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <asp:Panel ID="plButton" runat="server" style="width:100%; height: 28px; padding: 0px 14px 6px;">
            <asp:Button ID="btnQuery" CssClass="buttonface" style="width:75px;" Text="查詢" runat="server" Visible="false" OnClientClick="return DoQuery();" />
            <asp:Button ID="btnClear" CssClass="buttonface" style="width:75px;" Text="清除" runat="server" Visible="false" />
            <input type="button" id="btnPrint" class="buttonface" onclick="DiagramPrint('ALL');" style="width:75px;" value="列印" runat="server" Visible="false" />
            <input type="button" id="btnDownload" class="buttonface" onclick="OrganDownloadAll();" style="width:75px;" value="下傳" runat="server" Visible="false" />
        </asp:Panel>
        <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
            <tr>
                <td width="1%"></td>
                <td align="left">
                    查詢日期：<uc:ucCalender ID="txtQryDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                    (最多可比較三個單位)
                </td>
            </tr>
        </table>   
        <table id="tblContent" border="0" cellpadding="0" cellspacing="0" style="width:100%; height:730px">
            <tr>
                <td align="left" class="TrMenuBox" style="width:300px">
                    <div id="MenuBox">
                        <asp:TreeView ID="tvOrgan" runat="server" ShowLines="true"></asp:TreeView>
                    </div>
                </td>
                <td align="left">
                    <asp:UpdatePanel ID="MainOrgan" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="OrganBox" runat="server"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div id="divSample" style="display:none;">
                        <div id="OrganBox_" class="OrganBox">
                            <div id="Organ_" class="OrganDiagram"></div>
                            <div class="OrganCenter">
                                <div style="text-align:left; width:450px; margin-bottom:10px;">
                                    <input type="button" id="btnOpen_" onclick="DiagramOpen(this.name)" value="展開" />
                                    <input type="button" id="btnClose_" onclick="DiagramClose(this.name)" value="收合" />
                                    <input type="button" id="btnDownload_" onclick="OrganDownload(this.name)" value="下載" />
                                    <input type="button" id="btnPrint_" onclick="DiagramPrint(this.name)" value="列印" />
                                    <input type="hidden" id="hidEmpCnt_" value="" />
                                    <input type="hidden" id="hidUnderCnt_" value="" />
                                </div>
                                <table class="tbl_Condition" cellpadding="3" cellspacing="1" width="450">
                                    <tr>
                                        <td align="left" width="15%">單位別：</td>
                                        <td align="left" width="35%"><select id="ddlOrgan_" name="" onchange="changeOrgan(this)" style="width:140px;"></select></td>
                                        <td align="left" width="18%"></td>
                                        <td align="left" width="35%"></td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="15%">職位：</td>
                                        <td align="left" width="35%"><select id="ddlPosition_" name="" onchange="changePosWork(this)" style="width:140px;"></select></td>
                                        <td align="left" width="18%">工作性質：</td>
                                        <td align="left" width="35%"><select id="ddlWorkType_" name="" onchange="changePosWork(this)" style="width:140px;"></select></td>
                                    </tr>
                                </table>                                
                                <table class="tbl_Edit" cellpadding="3" cellspacing="1" width="450">
                                    <tr>
                                        <td align="center" class="td_EditHeader">單位總人數</td>
                                        <td align="center" class="td_Edit"><span id="hidTotCnt_"></span>人</td>
                                        <td align="center" class="td_EditHeader">平均單位年資</td>
                                        <td align="center" class="td_Edit"><span id="avgOrg_"></span></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="td_EditHeader">薪資總額</td>
                                        <td align="center" class="td_Edit"><span id="totSalary_"></span></td>
                                        <td align="center" class="td_EditHeader">平均職等年資</td>
                                        <td align="center" class="td_Edit"><span id="avgRank_"></span></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="td_EditHeader">平均年齡</td>
                                        <td align="center" class="td_Edit"><span id="avgAge_"></span></td>
                                        <td align="center" class="td_EditHeader">平均職位年資</td>
                                        <td align="center" class="td_Edit"><span id="avgPos_"></span></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="td_EditHeader">平均企業團年資</td>
                                        <td align="center" class="td_Edit"><span id="avgTotSen_"></span></td>
                                        <td align="center" class="td_EditHeader">平均工作年資</td>
                                        <td align="center" class="td_Edit"><span id="avgWork_"></span></td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="td_EditHeader">平均公司年資</td>
                                        <td align="center" class="td_Edit"><span id="avgSen_"></span></td>
                                        <td align="center" class="td_EditHeader">平均簽核年資</td>
                                        <td align="center" class="td_Edit"><span id="avgFlow_"></span></td>
                                    </tr>
                                </table>
                            </div>
                            <%--<p style="page-break-after: always;"></p>--%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <iframe id="Downloadframe" style="display:none;"></iframe>
    </form>
</body>

</html>
