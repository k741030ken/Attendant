<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV1002.aspx.vb" Inherits="OV_OV1002" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>天然災害設定(修改)</title>
   <style type="text/css">
        .style1
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            background-color: #f3f8ff;
            min-width: 110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            min-width: 110px;
        }
<%--        .style
        {
          Width:99.7%;
           Height="80px"
            
            
            
         }--%>
        
        input, option, span, div, select,label
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table style="width: 100%" class="tbl_Condition">
     
         <tr id="trCity" runat="server"  visible="false">
  <td width="2%"></td>
                 <td width="10%"  class="style1" >
                <asp:Label ID="Label1" runat="server" Text="縣市："></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:Label ID="lblCityName" runat="server" ></asp:Label>
            </td>
            <td width="10%" class="style1" >
            <asp:Label ID="Label3" runat="server" Text="工作地點："></asp:Label>
            </td>
            <td width="40%"  class="style2" >
                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
            </td>

        </tr>
        <tr id="trEmp" runat="server"  visible="false">
        <td width="2%"></td>
                 <td width="10%"  class="style1" >
                <asp:Label ID="lblEmpID" runat="server" Text="員工編號："></asp:Label>
            </td>
            <td width="40%" colspan="3" class="style2" >
                <asp:Label ID="lblEmpIDtxt" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr >
        <td width="2%"></td>
            <td width="10%"  class="style1" >
                <asp:Label ID="lblDate" runat="server" Text="留守日期："></asp:Label>
            </td>
            <td width="40%" colspan = "3" class="style2" >
                <uc:ucCalender ID="ucBeginFixDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                <asp:Label ID="Label7" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="ucEndFixDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
            </td>
        </tr>
        <tr >
        <td width="2%"></td>
            <td  class="style1"  >
                <asp:Label ID="lblBTime" runat="server" Text="留守開始時間："></asp:Label>
            </td>
            <td  class="style2" >
                <asp:DropDownList ID="StartTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="StartTimeM" runat="server">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1" >
                <asp:Label ID="lblETime" runat="server" Text="留守結束時間："></asp:Label>
            </td>
            <td width="40%" class="style2" >
                <asp:DropDownList ID="EndTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="EndTimeM" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr >

        <td width="2%"></td>
            <td  class="style1"  >
            <asp:Label ID="Label2" runat="server" Text="留守類型："></asp:Label>
            </td>
            <td  class="style2"  >
                
                <asp:DropDownList ID="ddlType" runat="server">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1" >
            </td>
            <td width="40%" class="style2" >
            </td>
        </tr>
        <tr id="remarkField" runat="server"  visible="false">
        <td width="2%"></td>
            <td  class="style1"  >
            <asp:Label ID="Label5" runat="server" Text="說明："></asp:Label>
            </td>
            <td  class="style2" colspan="3">
                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" TextMode="MultiLine"  class="txtRemarkStyle"></asp:TextBox>
            </td>
        </tr>
        <tr style="height:20px">
        <td width="2%"></td>
            <td class="style1" width="15%">
                <asp:Label ID="lblLastChgComp" runat="server" Text="最後異動公司："></asp:Label>
            </td>
            <td class="style2" style="width:35%" align="left" colspan="3">
                <asp:Label ID="lblLastChgComptxt" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr style="height:20px">
        <td width="2%"></td>
            <td class="style1" width="15%">
                <asp:Label ID="lblLastChgID" runat="server" Text="最後異動人員："></asp:Label>
            </td>
            <td class="style2" style="width:35%" align="left" colspan="3">
                <asp:Label ID="lblLastChgIDtxt" runat="server" > </asp:Label>
            </td>
        </tr>
        <tr style="height:20px">
        <td width="2%"></td>
            <td class="style1" width="15%">
                <asp:Label ID="lblLastChgDate" runat="server" Text="最後異動日期："></asp:Label>
            </td>
            <td class="style2" style="width:35%" align="left" colspan="3">
                <asp:Label ID="lblLastChgDatetxt" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>