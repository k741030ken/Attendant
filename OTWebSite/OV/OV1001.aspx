<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV1001.aspx.vb" Inherits="OV_OV1001" %>

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
</head>
<body>
    <form id="form1" runat="server">
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
                <td class="table_td_header">
                    <asp:Label ID="Label19" runat="server" Text="加班轉換方式："></asp:Label>
                </td>
                <td class="table_td_content" >
                   
                    <asp:Label ID="labSalaryOrAdjust" runat="server" ></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="補休失效日：" ></asp:Label>
                    <asp:Label ID="labAdjustInvalidDate" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr class="table_td_content">
                <td class="table_td_header">
                    加班日期
                </td>
                <td class="table_td_content" colspan="1">
                    <asp:Label ID="labOverTimeDate" runat="server"></asp:Label>
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
                    <asp:Label ID="labOTStartDate" runat="server" ></asp:Label>
                </td>
                <td class="table_td_header">
                    加班結束時間
                </td>
                <td class="table_td_content">
                    <asp:Label ID="labOTEndDate" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="table_td_content" colspan="2">
                 <asp:Label ID="labOverTimeStr" runat="server" style="margin-left: 5px;"></asp:Label>
                          <asp:Label ID="labOverTimeStr1" runat="server" style="margin-left: 5px; color:Red;"></asp:Label>
                    <br />
                     <br />
                    <asp:CheckBox ID="cbMealFlag" runat="server" Text="扣除用餐時數：" />
                    <asp:Label ID="labMealTime" runat="server"></asp:Label>
                    <asp:Label ID="Label5" runat="server" Text="分鐘"></asp:Label>
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
          <%--                  <td id="td_StayTime1"  runat="server">
                                留守時段
                            </td>--%>
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
<%--                            <td id="td_StayTime2"  runat="server">
                                <asp:Label ID="lalStay_Time" runat="server" Text=""></asp:Label>
                            </td>--%>
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
<%--                            <td id="td_StayTime3"  runat="server">
                                <asp:Label ID="lalStay_Time2" runat="server" Text=""></asp:Label>
                            </td>--%>
                        </tr>
                    </table>
                    </div>
        </td> </tr>
        <tr>
            <td class="table_td_header">
                加班類型
            </td>
            <td class="table_td_content" colspan="3">
                <asp:Label ID="labOTTypeID" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr class="table_td_content">
            <td class="table_td_header">
                加班原因
            </td>
            <td class="table_td_content" colspan="3">
              <asp:Label ID="labOTReasonID" runat="server"  AutoSize="true" Width="800px" Height="130px"></asp:Label>
            </td>

        </tr>
        <tr>
            <td class="table_td_header">
                上傳附件
            </td>
            <td class="table_td_content" colspan="3">
             <asp:HiddenField ID="labOTAttachment" runat="server" />
                <asp:Button ID="Button1" runat="server" Text="附件下載" />
               <asp:Label ID="fileName" Font-Size="15px" runat="server"></asp:Label>
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
