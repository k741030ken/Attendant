<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA070.aspx.vb" Inherits="WF_WFA070" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <link type="text/css" rel="stylesheet" href="../form.Css" />
    <script type="text/javascript">
    <!--
        function openWindow(url){
           window.open(url,"FlowDetail", 'height=600px,width=800px,top=20px,left=50px,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no, status=no')
        }
        
        
        function funAction(Param)
        {
              
            switch(Param)
            {
                case "btnUpdate":
                    
            }

        }
     
     
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="70%">
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label1" runat="server" Text="部門"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlOrgan" AutoPostBack="true" runat="server" CssClass="DropDownListStyle" Width="250px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label2" runat="server" text="待辦人員"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:UpdatePanel ID="udpOrgan" runat="server">
                                <ContentTemplate>
                                <asp:DropDownList ID="ddlAO" runat="server" CssClass="DropDownListStyle" Width="250px"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlOrgan" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="lblApplyCase" runat="server" text="申請期間"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                              <asp:TextBox ID="txtApplyCaseS" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                                <asp:ImageButton runat="Server" ID="imgApplyCaseS" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtApplyCaseS" PopupButtonID="imgExpireDate" Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>～
                              <asp:TextBox ID="txtApplyCaseE" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                              <asp:ImageButton runat="Server" ID="imgApplyCaseE" ImageUrl="~/images/Calendar.gif" AlternateText="Click to show calendar" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtApplyCaseE" PopupButtonID="imgExpireDate" Format="yyyy/MM/dd">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label6" runat="server" text="案件類別"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlFlowName" runat="server" DataTextField="FlowName" DataValueField="FlowID" AutoPostBack="False" CssClass="DropDownListStyle" Width="250px">
                                </asp:DropDownList>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
