<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST2200.aspx.vb" Inherits="ST_ST2200" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <link type="text/css" rel="stylesheet" href="../css/uploadify/uploadify.css" />
    <style type="text/css">
        .btnUploadClass
        {
            background-color:Silver;
            border-right: gray 1px solid;
            border-top: gray 1px solid;
            font-size: 14px;
            background-image: url(../images/bgSilverX.gif);
            border-left: gray 1px solid;
            color: black; 
            padding-top: 3px;
            border-bottom: gray 1px solid;
            background-repeat: repeat-x;
            font-family: Calibri, 新細明體, 'Courier New' , 細明體; 
            letter-spacing: 1px;
            border-collapse: collapse;
            text-align: center;
            font-weight: normal;
        }

        #FileQueue 
        {
        	width: 500px;
        	height: 350px;
        	background-color: #FFFFFF;
        	border: gray 1px solid;
        	overflow-y: scroll;
        }
        
        #UploadBarDiv
        {
        	width: 500px;
        	margin: 10px 0px;
        	text-align:center;
        }
        /*
        #UploadBar
        {
        	position: absolute;
        	width: 0px;
        	height: 18px;
        	background-color: Yellow;
        	display: block;
        }    
        #UploadBarText
        {
        	position: relative;
        }
        */
    </style>

    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <script type="text/javascript" src="../css/uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript">
    <!--
        $(function () {

            var TotalCount = 0;
            var SuccessCount = 0;
            var ErrorCount = 0;
            var TotalBytes = 0;
            var TotalBytesUploaded = 0;

            $("#btnUpload").uploadify({
                //"removeCompleted": false,
                "removeTimeout": 0.01,
                "queueID": "FileQueue",
                "swf": "../css/uploadify/uploadify.swf",
                "uploader": "ST2201.aspx",
                "buttonClass": "btnUploadClass",
                "buttonText": "瀏覽",
                "auto": false,
                "multi": true,
                "fileTypeExts": "*.jpg;*.jpeg",
                "width": "75",
                "height": "20",
                "onSelect": function (file) {
                    TotalCount++;
                    TotalBytes += file.size;
                    //$("#UploadBar").css("width", "0px");
                    $("#UploadBarText").text("");
                },
                "onCancel": function (file) {
                    TotalCount--;
                    TotalBytes -= file.size;
                },
                "onUploadStart": function (file) {
                    $("#UploadBarText").text("檔案上傳中，請稍後...");
                },
                "onUploadSuccess": function (file, data, response) {
                    if (data == "Success") {
                        SuccessCount++;
                    }
                    if (data == "Error") {
                        ErrorCount++;
                    }
                    TotalBytesUploaded += file.size;
                    //pos = Math.round(TotalBytesUploaded / TotalBytes * 10000) * 0.0001;
                    //bars = 500 * pos

                    //$("#UploadBar").css("width", bars + "px");
                },
                "onUploadError": function (file, errorCode, errorMsg, errorString) {
                    ErrorCount++;
                    TotalBytes -= file.siz
                },
                "onQueueComplete": function () {
                    //$("#UploadBarText").text("整批上傳完畢...");
                    $("#UploadBarText").html("<span style='color:red'>整批上傳完畢...</span>");

                    var SuccessMessage = "上傳檔案成功！總筆數：{0}";
                    var ErrorMessage = "上傳檔案失敗！總筆數：{0}，成功筆數：{1}，失敗筆數：{2}，請下載錯誤記錄Log檔案！";

                    if (ErrorCount == "0") {
                        var result = SuccessMessage.replace("{0}", TotalCount);
                        alert(result);
                    }
                    else {
                        var result = ErrorMessage.replace("{0}", TotalCount).replace("{1}", SuccessCount).replace("{2}", ErrorCount);
                        alert(result);
                        $("#btnDownload").click();
                    }
                    TotalCount = 0;
                    SuccessCount = 0;
                    ErrorCount = 0;
                    TotalBytes = 0;
                    TotalUploaded = 0;
                    TotalBytesUploaded = 0;
                }
            });
        });

        function uploadStrat() {
            var count = $("#FileQueue").find(".uploadify-queue-item").length
            if (count > 0) {
                if (confirm("確定上傳檔案？")) {
                    var strCompRoleID = "<%= UserProfile.SelectCompRoleID %>";

                    var hh = pad(new Date().getHours(), "0", 2, "left");
                    var mm = pad(new Date().getMinutes(), "0", 2, "left");
                    var ss = pad(new Date().getSeconds(), "0", 2, "left");
                    var fff = pad(new Date().getMilliseconds(), "0", 3, "left");

                    var datetime = $.datepicker.formatDate("yymmdd", new Date()) + hh + mm + ss + fff;
                    var mathRandom = Math.round(Math.random() * 100000);

                    var strLogName = "ST2200" + datetime + mathRandom + ".err"
                    $("#hldLogName").val(strLogName);

                    $("#btnUpload").uploadify("settings", "formData", { "strCompRoleID": strCompRoleID, "strLogName": strLogName });
                    $("#btnUpload").uploadify("upload", "*");
                }
            }
            else {
                alert("未選擇上傳檔案！");
            }
        }
        function uploadClear() {
            $("#btnUpload").uploadify("cancel", "*");
        }

        function pad(inputText, paddingChar, paddingLength, paddingType) {
            var inputText = $.trim(inputText);
            var outputText = inputText;
            if (inputText.length < paddingLength) {
                for (var i = inputText.length; i < paddingLength; i++)
                {
                    if (paddingType == "left") {
                        outputText = paddingChar + outputText;
                    }
                    else if (paddingType == "right") {
                        outputText = outputText + paddingChar;
                    }
                }
            }
            return outputText;
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <asp:Panel ID="plUpload" runat="server">
        <table style="width:100%;" border="0" cellspacing="0" cellpadding="0">
            <tr align="center" style="height: 28px;">
                <td align="left" style="padding: 0px 14px 6px;">
                    <input type="button" id="btnUploadStrat" class="buttonface" onclick="uploadStrat()" style="width:75px;" value="上傳" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="70%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" cellspacing="15" class="tbl_Content">
                        <tr>
                            <td align="left" width="510px">
                                <div id="FileQueue" class="InputTextStyle_Thin"></div>
                                <div id="UploadBarDiv" class="InputTextStyle_Thin">
                                    <%--<div id="UploadBar"></div>--%>
                                    <label id="UploadBarText"></label>
                                </div>
                            </td>
                            <td align="left" valign="top">
                                <input type="file" id="btnUpload" name="btnUpload" />
                                <input type="button" id="btnCancel" class="buttonface" onclick="uploadClear()" style="width:75px;" value="全部清除" />
                                <asp:Button ID="btnDownload" style="display:none;" runat="server" />
                                <asp:HiddenField ID="hldLogName" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
