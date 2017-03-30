<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PageQuerySelectOrgan.aspx.vb" Inherits="Component_PageQuerySelectOrgan" EnableEventValidation="false" %>
<%@ Register TagPrefix="uc1" TagName="PagerControl" Src="~/Component/ucPagerControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
		{
		    if (Param == 'btnActionX')
		        window.top.close();
		}
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:10px; margin-right:10px; margin-bottom:5px" >
    <form id="frmContent" runat="server"> 
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table width="100%" border="0">
            <tr>
                <td align="center">
                    <table width="100%" cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" >
                         <tr>
                            <td align="right" width="120px">
                                <asp:Label ID="lblCompID" runat="server" Text="請選擇公司：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadOrganID" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" width="120px">
                                <asp:Label ID="lblOrgType" runat="server" Text="請選擇組織類型：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="LoadOrganID" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="120px">
                                <asp:Label ID="lblOrganID" runat="server" Text="請選擇單位：" CssClass="f9"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="細明體"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="120px">
                                <asp:TextBox runat="server" ID="txtQueryString" Width="120px" CssClass="InputTextStyle_Thin"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Button runat="server" ID="btnQuery" cssclass="buttonface" Text="單位代碼或名稱速查" height="25px" />
                            </td>
                        </tr>          
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 30px;">
                    <table width="100%" class="tbl_Content" ID="tblMain" runat="server">
                        <tr>
                            <td style="width:100%">
                                <uc1:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AutoGenerateColumns="False" Font-Size="12px" width="100%"
                                    CssClass="GridViewStyle" HeaderStyle-CssClass="td_header" RowStyle-CssClass="td_detail" DataKeyNames="CompID,CompName,OrganID,OrganName">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Button ID="btnSelect" runat="server" Text="選取" CommandName="select" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="公司代碼" DataField="CompID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="公司名稱" DataField="CompName" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="單位代碼" DataField="OrganID" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderStyle-Width="20%" HeaderText="單位名稱" DataField="OrganName" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderStyle-Width="10%" HeaderText="單位狀態" DataField="InValidFlag" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
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
        </table>
    </form>
</body>
</html>
