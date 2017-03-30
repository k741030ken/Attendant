<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Overtime_Detail.aspx.cs" Inherits="OverTime_Overtime_Detail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../Util/ucAttachDownloadButton.ascx" tagname="ucAttachDownloadButton" tagprefix="uc1" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
<title>明細頁</title>   
</head>
 <style type="text/css">
        body
        {
        	font-family: "微軟正黑體", "微软雅黑", "Microsoft JhengHei", Arial, sans-serif;
        	color :#000000;
        }
 </style>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True"></asp:ToolkitScriptManager>
    <table style="width:100%;border:1px solid" rules="all">
        <tr class="Util_clsRow1">
            <td>
                <asp:Label ID="lblComp" runat="server" Text="*公司：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <%--<asp:Label ID="lblCompID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;--%>
                <asp:Label ID="lblCompName" runat="server" Text="" Width="80px" ></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDept" runat="server" Text="*單位：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <%--<asp:Label ID="lblDeptID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;--%>
                <asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow2">
            <td>
                <asp:Label ID="lblOrgan" runat="server" Text="*科別：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <%--<asp:Label ID="lblOrganID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;--%>
                <asp:Label ID="lblOrganName" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmp" runat="server" Text="*加班人：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmpID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow1">
            <td>
                <asp:Label ID="lblOTRegister" runat="server" Text="*填單人：" ForeColor="blue"></asp:Label>
            </td>
            <td colspan="1">
                <asp:Label ID="lblOTRegisterID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                <asp:Label ID="lblOTRegisterName" runat="server" Text=""></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblSalaryOrAdjustWay" runat="server" Text="*加班轉換方式：" ForeColor="blue"></asp:Label>
                <asp:Label ID="lblSalaryOrAdjust" runat="server" Text="轉薪資"></asp:Label>  
                &nbsp;&nbsp;
                <%--<br />--%>
                <asp:Label ID="lblAdjustInvalidDate" runat="server" Text="補休失效日：" Visible="false"></asp:Label>
                <asp:Label ID="txtAdjustInvalidDate" runat="server" Text="yyyy/MM/dd" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow2">
            <td>
                <asp:Label ID="lblOTDate" runat="server" Text="*加班日期：" ForeColor="blue"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblOTDateValue" runat="server" Text=""></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="~"></asp:Label>
                <asp:Label ID="lblOTDateValueEnd" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow1">
            <td>
                <asp:Label ID="lblBeginTimeHour" runat="server" Text="*加班開始時間：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBeginTime" runat="server" Text=""></asp:Label> 
            </td>
            <td>
                <asp:Label ID="lblEndTimeHour" runat="server" Text="*加班結束時間：" ForeColor="blue"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEndTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow2">
            <td colspan="2">
                <asp:Label ID="lblTotalTime" runat="server" Text="*加班時數：" ForeColor="Blue"></asp:Label>
                <asp:Label ID="lblOTTotalTime" runat="server"></asp:Label>
                <asp:Label ID="lblOTTotalTimeHour" runat="server" Text="小時"></asp:Label>
                <asp:Label ID="txtTotalDescription" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:CheckBox ID="chkMealFlag" runat="server" Checked="true" Enabled="false"  />
                <asp:Label ID="lblMinusMealTime" runat="server" Text="扣除用餐時數：" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblMealTime" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblMealTimeMinute" runat="server" Text="分鐘" ForeColor="Red"></asp:Label>
            </td>
            <td colspan="2">
                 <asp:Label ID="lblPeriod" runat="server" Text="含本次累計時數如下：" ></asp:Label>
               <%-- <asp:Label ID="lblPeriod" runat="server" Text="含本次累計時數如下：" Visible="False"></asp:Label>--%>
               <br />
                <table width="80%" align="left" id="tbTime"  
                                style="border: 1px solid black;" runat="server" rules="all" >
                     <tr id="Tr1" align="center" runat="server">
                                <td id="Td1" width="25%" rowspan="2" runat="server">日期</td>
                                <td id="Td2" width="25%" colspan="3" runat="server">小時</td>
                            </tr>
                            <tr id="Tr2" align="center" runat="server">
                                <td id="Td3" width="25%" runat="server">時段(一)</td>
                                <td id="Td4" width="25%" runat="server">時段(二)</td>
                                <td id="Td5" width="25%" runat="server">時段(三)</td>
                            </tr> 
                    <tr id="trOne" visible="False" runat="server" align="center">
                        <td id="Td6" runat="server"><asp:Label ID="lblDateOne" runat="server" ></asp:Label></td>
                        <td id="Td7" runat="server"><asp:Label ID="lblDateOne_0" runat="server" ></asp:Label></td>
                        <td id="Td8" runat="server"><asp:Label ID="lblDateOne_1" runat="server" ></asp:Label></td>
                        <td id="Td9" runat="server"><asp:Label ID="lblDateOne_2" runat="server" ></asp:Label></td>
                    </tr>
                    <tr id="trTwo" visible="False" runat="server" align="center">
                        <td id="Td10" runat="server"><asp:Label ID="lblDateTwo" runat="server" ></asp:Label></td>
                        <td id="Td11" runat="server"><asp:Label ID="lblDateTwo_0" runat="server" ></asp:Label></td>
                        <td id="Td12" runat="server"><asp:Label ID="lblDateTwo_1" runat="server" ></asp:Label></td>
                        <td id="Td13" runat="server"><asp:Label ID="lblDateTwo_2" runat="server" ></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="Util_clsRow1">
            <td>
                <asp:Label ID="lblOTTypeName" runat="server" Text="*加班類型：" ForeColor="Blue"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblOTTypeID" runat="server" Text="業務量增加"></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow2">
            <td>
                <asp:Label ID="lblOTReasonName" runat="server" Text="*加班原因：" ForeColor="Blue"></asp:Label>
                </td>
                <td colspan="3">
                <asp:Label ID="lblOTReasonMemo" runat="server" Width="300px" Text=""></asp:Label>
                </td>
        </tr>
        <tr class="Util_clsRow1">
            <td>
                <asp:Label ID="lblFileUpload" runat="server" Text="上傳附件："></asp:Label>
            </td>
            <td colspan="3">
                <uc1:ucAttachDownloadButton ID="btnAttachDownload" runat="server" ucBtnCaption="檢視附件"/>
                <asp:Label ID="lblAttachName" runat="server" Text="目前無附件"></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow2">
            <td colspan="4">
                <asp:Label ID="lblLastChg" runat="server" Text="最後異動人員："></asp:Label>
                <asp:Label ID="lblLastChgID" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                <asp:Label ID="lblLastChgName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr class="Util_clsRow1">
            <td colspan="4">
                <asp:Label ID="lblLastChgDateName" runat="server" Text="最後異動時間："></asp:Label>
                <asp:Label ID="lblLastChgDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <div>
       <asp:Label ID="txtOTDateMonth" runat="server"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="月已申請時數合計：  送簽"></asp:Label>
        <asp:Label ID="lblSubmit" runat="server"></asp:Label>
        <asp:Label ID="Label23" runat="server" Text="小時  核准"></asp:Label>
        <asp:Label ID="lblApproval" runat="server"></asp:Label>
        <asp:Label ID="Label25" runat="server" Text="小時  駁回"></asp:Label>
        <asp:Label ID="lblReject" runat="server"></asp:Label>
        <asp:Label ID="Label27" runat="server" Text="小時"></asp:Label>
    </div>
    </form>
</body>
</html>


