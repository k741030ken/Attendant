<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfoList.aspx.cs" Inherits="UserInfoList"
    UICulture="auto" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown"
    TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucGridView.ascx" TagName="ucGridView" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucProcessButton.ascx" TagPrefix="uc1" TagName="ucProcessButton" %>

<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>單位人員檢視</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <cc1:HoverTooltip ID="HoverTooltip1" runat="server">
        </cc1:HoverTooltip>
        <div runat="server" id="DivQryCondition" visible="true">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">查詢條件</legend>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucCascadingDropDown ID="qryCompDeptUser" runat="server" ucCaption="查詢單位" />
                </div>
                <div class="Util_clsRow2" style="padding: 3px;">
                    <uc1:ucTextBox ID="qryUserID" ucCaption="員工編號" ucWidth="120" runat="server" />
                </div>
                <div class="Util_clsRow1" style="padding: 3px;">
                    <uc1:ucTextBox ID="qryUserName" ucCaption="員工姓名" ucWidth="120" runat="server" />
                    <asp:CheckBox ID="qryIsLikeUserName" Text="[包含類似內容]" runat="server" />
                </div>
                <div style="text-align: center; height: 50px;">
                    <hr class="Util_clsHR" />
                    <asp:Button runat="server" ID="btnQry" CssClass="Util_clsBtnGray" Width="80" Text="查　　詢"
                        OnClick="btnQry_Click" />
                    <asp:Button runat="server" ID="btnClear" CssClass="Util_clsBtnGray" Width="80"
                        Text="清　　除" OnClick="btnClear_Click" />
                    <uc1:ucProcessButton runat="server" ID="btnOrgExport" ucBtnCaption="組織資料" />
                </div>
            </fieldset>
        </div>
        <br />
        <div runat="server" id="DivQryResult" visible="false">
            <fieldset class='Util_Fieldset'>
                <legend class="Util_Legend">查詢結果</legend>
                <div style="margin-top: 30px; margin-bottom: 30px;">
                    <uc1:ucGridView ID="ucGridView_Roll" runat="server" />
                </div>
                <div>
                    <uc1:ucGridView ID="ucGridView_Fix" runat="server" />
                </div>

            </fieldset>
        </div>
    </form>
</body>
</html>
