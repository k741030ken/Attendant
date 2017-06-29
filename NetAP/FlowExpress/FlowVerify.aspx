<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowVerify.aspx.cs" Inherits="FlowExpress_FlowVerify"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucTextBox.ascx" TagName="ucTextBox" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommMultiSelect.ascx" TagName="ucCommMultiSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagPrefix="uc1" TagName="ucCheckBoxList" %>
<%@ Register Src="~/Util/ucUserPicker.ascx" TagName="ucUserPicker" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>FlowVerify</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <asp:Label ID="labIsInit" runat="server" Text="N" Visible="false"></asp:Label>
        <%--審核按鈕區--%>
        <div id="DivVerifyBtnArea" runat="server">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <asp:Label ID="labVerifyOpinion" runat="server" Text="Opinion" />
                </legend>
                <div style="border: 0px solid gray; width: 620px; height: 80px; overflow: auto;">
                    <asp:Label ID="labVerifyOpinionData" runat="server"></asp:Label>
                </div>
            </fieldset>
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <asp:Label ID="labVerifyStepInfo" runat="server" Text="StepInfo"></asp:Label>
                </legend>
                <div style="min-height: 250px; padding-left: 20px;">
                    <asp:Label ID="labVerify" runat="server" Width="100%" Text="" />
                    <uc1:ucCommSingleSelect ID="oneVerify" runat="server" Visible="false" />
                    <uc1:ucCheckBoxList ID="chkVerify" runat="server" Visible="false" />
                    <uc1:ucCommMultiSelect ID="muiVerify" runat="server" Visible="false" />
                    <uc1:ucUserPicker ID="anyVerify" runat="server" Visible="false" />
                    <asp:ListBox ID="allVerify" runat="server" Visible="false" />
                    <asp:Label ID="labAllVerify" runat="server" Width="100%" Text="" Visible="false" />
                </div>
            </fieldset>
            <div style="margin-top: 20px; width: 100%; text-align: center;">
                <asp:Button ID="btnStartVerify" runat="server" CssClass="Util_clsBtnGray Util_Pointer" OnClick="btnStartVerify_Click"
                    CausesValidation="true" Text="Start Verify" Width="200px"></asp:Button>
            </div>
        </div>
        <%--審核訊息區--%>
        <div id="DivVerifyMsgArea" runat="server" visible="false">
            <div style="margin: 10px;">
                <asp:Label ID="labFlowVerifyMsg" runat="server" />
            </div>
        </div>
        <uc1:ucLightBox runat="server" ID="ucLightBox" />
    </form>
</body>
</html>
