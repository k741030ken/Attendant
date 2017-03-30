<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFlowFullLogList.ascx.cs"
    Inherits="FlowExpress_ucFlowFullLogList" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc1" %>
<style type="text/css">
    /* 客製 Tooltip 效果 ，繼承自 Util.css  */
    .FlowHint.Util_data-hint:hover:after {
        width: 200px;
        white-space: pre-wrap;
    }

    /* 標題,文字,連結 */
    .FlowCaption {
        color: #0080FF;
        font-size: 10pt;
        cursor: pointer;
    }

    .FlowArrow {
        color: #0080FF;
        font-size: 12pt;
    }

    .FlowClose {
        color: #006400;
        font-size: 10pt;
    }

    .FlowOpen {
        color: #0080FF;
        font-size: 10pt;
    }

    .FlowScopeFull
    {
        border: 1px solid #000;
        border-right-width: 0px;
        width: 5px;
    }
    
    .FlowScopeLite
    {
        border: 2px dotted #F75733;
        border-right-width: 0px;
        width: 5px;
    }

    /* Case Header*/
    .clsCaseTable {
        Height: 16pt;
        width: 100%;
        border-width: 0pt;
        text-align: left;
        text-decoration: none;
        background-color: #AACFE9;
        vertical-align: middle;
        font-size: 10pt;
        color: #555555;
    }

    .clsCaseHeader {
        height: 16pt;
        text-align: center;
        text-decoration: none;
        background-color: #E1F2FF;
        vertical-align: top;
        font-size: 10pt;
        color: #555555;
    }

    .clsCaseHeaderBody {
        height: 12pt;
        text-align: center;
        background-color: #F0FCFF;
        vertical-align: top;
        font-size: 10pt;
        color: #555555;
        text-decoration: none;
    }


    /* Level 1*/
    .cls1_Header {
        border-left: none;
        border-right: none;
        border-top: solid #4F81BD 1px;
        border-bottom: solid #4F81BD 1px;
        color: Gray;
        font-size: 10pt;
        font-weight: Bold;
    }

    .cls1_Footer {
        border: none;
        border-top: solid #4F81BD 1px;
        font-size: 2pt;
        height: 1px;
    }

    .cls1_Row1 {
        border: none;
        background: #D3DFEE;
        color: #365F91;
        font-size: 10pt;
        word-break: break-all;
    }

    .cls1_Row2 {
        border: none;
        background: #FFFFFF;
        color: #365F91;
        font-size: 10pt;
        word-break: break-all;
    }

    /* Level 2*/
    .cls2_Header {
        border-left: none;
        border-right: none;
        border-top: solid #8064A2 1px;
        border-bottom: solid #8064A2 1px;
        color: Gray;
        font-size: 10pt;
        font-weight: Bold;
    }

    .cls2_Footer {
        border: none;
        border-top: solid #8064A2 1px;
        font-size: 2pt;
        height: 1px;
    }

    .cls2_Row1 {
        border: none;
        background: #DFD8E8;
        color: #5F497A;
        font-size: 10pt;
        word-break: break-all;
    }

    .cls2_Row2 {
        border: none;
        background: #FFFFFF;
        color: #5F497A;
        font-size: 10pt;
        word-break: break-all;
    }

    /* Level 3*/
    .cls3_Header {
        border-left: none;
        border-right: none;
        border-top: solid #FAAF3E 1px;
        border-bottom: solid #FAAF3E 1px;
        color: Gray;
        font-size: 10pt;
        font-weight: Bold;
    }

    .cls3_Footer {
        border: none;
        border-top: solid #FAAF3E 1px;
        font-size: 2pt;
        height: 1px;
    }

    .cls3_Row1 {
        border: none;
        background: #FDEFD9;
        color: #D17E0A;
        font-size: 10pt;
        word-break: break-all;
    }

    .cls3_Row2 {
        border: none;
        background: #FFFFFF;
        color: #D17E0A;
        font-size: 10pt;
        word-break: break-all;
    }

    /* Level 4*/
    .cls4_Header {
        border-left: none;
        border-right: none;
        border-top: solid #B9E534 1px;
        border-bottom: solid #B9E534 1px;
        color: Gray;
        font-size: 10pt;
        font-weight: Bold;
    }

    .cls4_Footer {
        border: none;
        border-top: solid #B9E534 1px;
        font-size: 2pt;
        height: 1px;
    }

    .cls4_Row1 {
        border: none;
        background: #D9F28C;
        color: #506708;
        font-size: 10pt;
        word-break: break-all;
    }

    .cls4_Row2 {
        border: none;
        background: #FFFFFF;
        color: #506708;
        font-size: 10pt;
        word-break: break-all;
    }
</style>
<asp:Label ID="labErrMsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
<div id='divFlowFullLogList' runat="server">
    <asp:Literal ID="labFullLogList" runat="server"></asp:Literal>
    <%--判斷是否為控制項自行發動 PostBack 的旗標，解決當控制項被直接崁入 UpdatePanel 內時，每次觸發都會被重新整理的狀況--%>
    <asp:Label ID="labIsSelfRefresh" runat="server" Text="N" Visible="false"></asp:Label>
    <%--隱藏按鈕，方便呼叫 CodeBehind--%>
    <asp:NoValidationLinkButton ID="btnAttach" runat="server" Text="btnAttach" OnClick="btnAttach_Click" Visible="false" CausesValidation="false" />
    <uc1:ucModalPopup ID="ucModalPopup1" runat="server" ucPopupHeight="350" />
</div>
