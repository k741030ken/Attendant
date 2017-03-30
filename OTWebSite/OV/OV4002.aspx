<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV4002.aspx.vb" Inherits="OV_1_OV4002" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        .table1
        {
            border-collapse: collapse;
            border: 1px solid #89b3f5;
            background-color: #f3f8ff;
        }
        
        
        .table1_input
        {
            width: 100px;
        }

        .bt_style
        {
            margin: 5px;
        }
        .table1 td
        {
            border: 1px solid #5384e6;
            padding: 10px;
        }
        .table2 td
        {
            border-collapse: collapse;
            border: 0;
        }
        .table_td_content
        {
            width: 35%;
        }
        .table_td_header
        {
            text-align: left;
            font-size: 14px;
            width: 15%;
            background-color: #e2e9fe;
            padding: 5px;
        }
        

        
        input, option, span, div, select,label,td,tr
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
        p.MsoNormal
        {
            margin-bottom: .0001pt;
            font-size: 12.0pt;
            font-family: "Times New Roman" ,serif;
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        p.MsoCommentText
        {
            margin-bottom: .0001pt;
            font-size: 12.0pt;
            font-family: "Times New Roman" ,serif;
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        .style4
        {
            border-collapse: collapse;
            font-size: 10.0pt;
            font-family: 微軟正黑體, sans-serif;
            border: 1.0pt solid black;
        }
        
        .style4 td
        {
            border-collapse: collapse;
            font-size: 10.0pt;
            font-family: 微軟正黑體, sans-serif;
            border: 1px solid black;
        }
        #Label1
        {
            margin-left: 50px;
            
            }
    </style>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <link type="text/css" rel="stylesheet" href="../Component/css/smoothness/jquery-ui-1.8.24.custom.css" />
    <script type="text/javascript">
        function TempSave() {
            document.getElementById("btnTempSave").click();
        }

        function Submit() {
            document.getElementById("btnSubmit").click();
        }
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
        $(function () {
            $("#txtOvertimeDateB_txtDate").change(function () {
                $("#OvertimeDate").click();
            });
        });
        $(function () {
            $("#txtOvertimeDateE_txtDate").change(function () {
                $("#OvertimeDate").click();
            });
        });
//        function DateClickFun() {
//        alert("test")
//            $('#dateClick').val="change"
//        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <div style="width: 100%; height: 100%">
        <br />
        <table class="table1" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr class="table_td_content">
                <td class="table_td_header">
                    公司
                </td>
                <td colspan="1">
                    <asp:Label ID="labCompID" runat="server" ></asp:Label>
                </td>
                <td class="table_td_header">
                   單位類別
                </td>
                <td colspan="1">
                    <asp:Label ID="labDeptID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="table_td_content">
                <td class="table_td_header">
                    科別
                </td>
                <td class="table_td_content">
                    <asp:Label ID="labOrganID" runat="server" ></asp:Label>
                </td>
                <td class="table_td_header">
                    加班人
                </td>
                <td class="table_td_content">
                    <asp:Label ID="labOTEmpName" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr class="table_td_content">
                <td class="table_td_header">
                    填單人
                </td>
                <td class="table_td_content">
                    <asp:Label ID="labOTRegisterID" runat="server" ></asp:Label>
                </td>
                <td class="table_td_header" >
          <asp:Label ID="Label19" runat="server" Text="加班轉換方式："></asp:Label>
                </td>
                <td class="table_td_content">
                              
                    <asp:HiddenField ID="labSalaryOrAdjust" runat="server" ></asp:HiddenField>
                     <asp:DropDownList ID="ddlSalaryOrAdjust" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="轉薪資" Value="1"></asp:ListItem>
                        <asp:ListItem Text="轉補休" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server" Text="補休失效日：" ></asp:Label>
                    <asp:Label ID="labAdjustInvalidDate" runat="server" ></asp:Label>
                    <asp:HiddenField ID="hidAdjustInvalidDate" runat="server" ></asp:HiddenField>
                </td>
            </tr>
            <tr class="table_td_content">
                <td class="table_td_header">
                    加班日期
                </td>
                <td class="table_td_content" colspan="1">
                    <asp:HiddenField ID="labOverTimeDate" runat="server"></asp:HiddenField>
                    <br/>
                       <uc:ucCalender ID="txtOvertimeDateB" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />

                    <asp:Label ID="Label3" runat="server" Text="～"></asp:Label>
                    <uc:ucCalender ID="txtOvertimeDateE" runat="server" CssClass="InputTextStyle_Thin"
                        Enabled="True" width="80px" />
                          <asp:Button ID="OvertimeDate" runat="server" Style="display: none"  AutoPostBack="true"/>
                </td>
                  <td class="table_td_header">
                <span>計薪年月</span>
            </td>
            <td class="table_td_content">
            <asp:Label ID="labOTPayDate" runat="server"></asp:Label>
            </td>
            </tr>
            <tr class="table_td_content">
                <td class="table_td_header">
                    加班開始時間
                </td>
                <td class="table_td_content">
                    <asp:HiddenField ID="labOTStartDate" runat="server" ></asp:HiddenField>
                     <asp:DropDownList ID="OTStartTimeH" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label2" runat="server" Text="："></asp:Label>
                    <asp:DropDownList ID="OTStartTimeM" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td class="table_td_header">
                    加班結束時間
                </td>
                <td class="table_td_content">
                    <asp:HiddenField ID="labOTEndDate" runat="server" ></asp:HiddenField>

                    <asp:DropDownList ID="OTEndTimeH" runat="server"  AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:Label ID="Label4" runat="server" Text="："></asp:Label>
                    <asp:DropDownList ID="OTEndTimeM" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <asp:HiddenField ID="hidMealFlag" runat="server" ></asp:HiddenField>
                 <asp:HiddenField ID="hidMealTime" runat="server" ></asp:HiddenField>
                <td class="table_td_content" colspan="2">
                 <asp:Label ID="labOverTimeStr" runat="server" style="margin-left: 5px;"></asp:Label>
                          <asp:Label ID="labOverTimeStr1" runat="server" style="margin-left: 5px; color:Red;"></asp:Label>
                    <br />
                     <br />
                    <asp:CheckBox ID="cbMealFlag" runat="server" Text="扣除用餐時數："  AutoPostBack="true"/>
                        <asp:TextBox ID="labMealTime" runat="server"  AutoPostBack="true" MaxLength="4">
                    </asp:TextBox>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="labMealTime" FilterType="Numbers">
                    </ajaxToolkit:FilteredTextBoxExtender>
               
                   <%-- <asp:Label ID="labMealTime" runat="server"></asp:Label>--%>
                    <asp:Label ID="Label5" runat="server" Text="分鐘"></asp:Label>
                               <asp:Label ID="tbOTlabMealTimeErrorMsg" runat="server" Text="(YYYYMM)" Style="padding-left: 10px;
                        color: Red" Visible="false">請填入數字</asp:Label>
                </td>
                <td class="table_td_content" colspan="2"  >
           
               <div id="table_tr_Time3" runat="server">
          
                    含本次累計時數如下:
                    <br />
                    <table border="1" cellpadding="0" cellspacing="0" class="style4">
                    <tr>
                    <td rowspan="2" align="center">
         日 期
                    </td>
                    <td colspan="3" align="center">
                    小時
                    </td>
                    </tr>
                        <tr>
                          
                            <td>
                                時段(一)
                            </td>
                            <td>
                                時段(二)
                            </td>
                            <td>
                                時段(三)
                            </td>

                        </tr>
                        <tr style="height:30px">
                            <td>
                                <asp:Label ID="lalOTStartTimeDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td>

                                <asp:Label ID="lalTime_one" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lalTime_two" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lalTime_three" runat="server" Text=""></asp:Label>
                            </td>

                        </tr>
                        <tr style="height:30px" id="table_tr_Time2" runat="server">
                            <td>
                                <asp:Label ID="lalOTStartTimeDate2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lalTime_one2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lalTime_two2" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lalTime_three2" runat="server" Text=""></asp:Label>
                            </td>

                        </tr>
                    </table>
                    </div>
        </td> </tr>
        <tr>
            <td class="table_td_header">
                加班類型
            </td>
            <td class="table_td_content" colspan="3">
              <%--  <asp:Label ID="labOTTypeID" runat="server" ></asp:Label>--%>
                  <asp:HiddenField ID="labOTTypeID" runat="server" />
                <asp:DropDownList ID="ddlOTTypeID" runat="server">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                加班原因
            </td>
            <td class="table_td_content" colspan="3">
<%--              <asp:Label ID="labOTReasonID" runat="server"  AutoSize="true" Width="800px" Height="130px"></asp:Label>--%>

              <asp:TextBox ID="labOTReasonID" runat="server" MaxLength="200" Width="535px" Height="50px" TextMode="MultiLine" style="resize:none;"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td class="table_td_header">
    上傳附件
            </td>
            <td class="table_td_content" colspan="3">

            <asp:Button ID="Button1" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                OnClientClick="openUploadWindow();return false;" Text="附件上傳" />
                            <asp:Label ID="lblUploadStatus" Font-Size="15px" runat="server"></asp:Label>
                            <asp:CheckBox ID="chkCopyAtt" runat="server" Text="同上筆附檔" Visible="False" />
                            <br />
                            <asp:Button ID="updateAttachName" runat="server" CausesValidation="false" CssClass="Util_clsBtn"
                                Text="更新附件檔名" Style="display: none;" />
                            <asp:HiddenField ID="frameAttach" runat="server"></asp:HiddenField>
                           <asp:HiddenField ID="labOTAttachment" runat="server" />

          <%--   <asp:HiddenField ID="labOTAttachment" runat="server" />--%>
            <%--    <asp:Button ID="Button1" runat="server" Text="附件上傳"  OnclientClick="openUploadWindow();return false;"/>--%>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                在職狀態
                        </td>
            <td class="table_td_content">
              <asp:Label ID="labWorkStatus" runat="server" ></asp:Label>
            </td>
            <td class="table_td_header">
                <span>工作性質</span>
            </td>
            <td class="table_td_content">
              <asp:Label ID="labWorkType" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                性別
            </td>
            <td class="table_td_content">
              <asp:Label ID="labSex" runat="server" ></asp:Label>
            </td>
            <td class="table_td_header">
                <span>職等 </span>
            </td>
            <td class="table_td_content">
            <asp:Label ID="labRankID" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="table_td_header">
                <span>職稱</span>
            </td>
            <td class="table_td_content">
              <asp:Label ID="labTitleName" runat="server" ></asp:Label>
            </td>
            <td class="table_td_header">
                <span>職位</span>
            </td>
            <td class="table_td_content">
             <asp:Label ID="labPositionID" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                加班日期類型
            </td>
            <td class="table_td_content">
             <asp:Label ID="labHolidayOrNot" runat="server"></asp:Label>
            </td>
            <td class="table_td_header">
                <span>拋轉狀態</span>
            </td>
            <td class="table_td_content">
            <asp:Label ID="labIsProcessDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                表單狀態
            </td>
            <td class="table_td_content">
              <asp:Label ID="labOTStatus" runat="server"></asp:Label>
            </td>
            <td class="table_td_header">
                <span>表單編號</span>
            </td>
            <td class="table_td_content">
              <asp:Label ID="labOTFormNO" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                <span>到職日期</span>
            </td>
            <td class="table_td_content">
               <asp:Label ID="labTakeOfficeDate" runat="server"></asp:Label>
            </td>
            <td class="table_td_header">
                <span>離職日期</span>
            </td>
            <td class="table_td_content">
               <asp:Label ID="labLeaveOfficeDate" runat="server"></asp:Label>
            </td>
            <tr class="table_td_content">
                <td class="table_td_header">
                    <span>核准日期</span>
                </td>
                <td class="table_td_content">
                <asp:Label ID="labDateOfApproval" runat="server"></asp:Label>
                </td>
                <td class="table_td_header">
                    <span>申請日期</span>
                </td>
                <td class="table_td_content">
                <asp:Label ID="labDateOfApplication" runat="server"></asp:Label>
                </td>
            </tr>
        </tr>
        <tr>
            <td class="table_td_header">
                <span>最後異動人員</span>
            </td>
            <td class="table_td_content" colspan="3">
              <asp:Label ID="labLastChgID" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="table_td_header">
                <span>最後異動時間</span>
            </td>
            <td class="table_td_content" colspan="3">
              <asp:Label ID="labLastChgDate" runat="server" ></asp:Label>
            </td>
        </tr>
        </table>  <asp:Label ID="myData" runat="server" Text="Label"></asp:Label>
          <asp:Label ID="labSum" runat="server" ></asp:Label>
        <asp:Label ID="labSum1" runat="server" ></asp:Label>小時 核准 
        <asp:Label ID="labSum2" runat="server" ></asp:Label>小時 駁回
        <asp:Label ID="labSum3" runat="server" ></asp:Label>小時
 
        </br> </br>
    </div>
    </form>
</body>

</html>
