<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CaseQry.aspx.cs" Inherits="LegalSample_CaseQry"
    UICulture="auto" MaintainScrollPositionOnPostback="true" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommCascadeSelect.ascx" TagName="ucCommCascadeSelect"
    TagPrefix="uc2" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown"
    TagPrefix="uc3" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc4" %>
<!DOCTYPE html >
<html>
<head runat="server">
    <title>Case Query</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <div>
        <%--查詢條件--%>
        <fieldset class='Util_Fieldset'>
            <legend class="Util_Legend">
                <asp:Literal ID="labQryTitle" runat="server" Text="案件查詢" meta:resourcekey="labQryTitleResource1" />
            </legend>
            <table class="Util_Frame" cellspacing="2" style="width: 500px;">
                <tr class="Util_clsRow1">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal1" runat="server" Text="案件編號：" meta:resourcekey="Literal1Resource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseNo" runat="server" meta:resourcekey="txtCaseNoResource1" />
                        (ex:20140701.001)
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal2" runat="server" Text="提案單位：" meta:resourcekey="Literal2Resource1" />
                    </td>
                    <td>
                        <uc3:ucCascadingDropDown ID="ucCascadingDropDown1" runat="server" />
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal7" runat="server" Text="提案主題：" meta:resourcekey="Literal7Resource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSubject" runat="server" />
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal3" runat="server" Text="提案人：" meta:resourcekey="Literal3Resource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUpdUserName" runat="server" meta:resourcekey="txtUpdUserNameResource1" />
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal4" runat="server" Text="承辦人：" meta:resourcekey="Literal4Resource1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtUpdLegalUserName" runat="server" meta:resourcekey="txtUpdLegalUserNameResource1" />
                    </td>
                </tr>
                <tr class="Util_clsRow2">
                    <td style="width: 80px;">
                        <asp:Literal ID="Literal5" runat="server" Text="提案區間：" meta:resourcekey="Literal5Resource1" />
                    </td>
                    <td>
                        <uc1:ucDatePicker ID="ucQryDate1" runat="server" />
                        ～
                        <uc1:ucDatePicker ID="ucQryDate2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <hr class="Util_clsHR" />
                        <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="80px"
                            Text="查　　詢" OnClick="btnQry_Click" meta:resourcekey="btnQryResource1" />
                        <asp:Button runat="server" ID="btnClear" CssClass="Util_clsBtnGray" Width="80px"
                            Text="清　　除" OnClick="btnClear_Click" meta:resourcekey="btnClearResource1" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <%--查詢結果--%>
        <div runat="server" id="DivQryResult" visible="false">
            <fieldset class='Util_Fieldset' style="width: 98%">
                <legend class="Util_Legend">
                    <asp:Literal ID="Literal6" runat="server" Text="查詢結果" meta:resourcekey="labQryResultResource1" />
                </legend>
                <center>
                    <uc1:ucGridView ID="ucGridView1" runat="server" />
                </center>
            </fieldset>
        </div>
    </div>
    <uc4:ucModalPopup ID="ucModalPopup1" runat="server" ucBtnCancelEnabled="false" ucBtnCompleteEnabled="false"
        ucBtnCloselEnabled="true" />
    </form>
</body>
</html>
