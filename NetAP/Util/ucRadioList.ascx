<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRadioList.ascx.cs"
    Inherits="Util_ucRadioList" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css" media="all">
    .RadioList_nowrap tr td label {
        white-space: nowrap;
        overflow: hidden;
    }
</style>
<div style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <span id="divHeadArea" runat="server" style="vertical-align: top;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span>
    <span id="divDataArea" runat="server" style="position: relative; top: 0px; vertical-align: top; float: right;">
        <asp:TextBox ID="txtInfo" runat="server" Width="100" CausesValidation="true" ReadOnly="true"
            ViewStateMode="Inherit" Text=""></asp:TextBox>
        <div id="divPopPanel" runat="server" style="position: absolute; z-index: 9999; display: none;">
            <asp:Panel ID="pnlBoxList" runat="server" CssClass="Util_clsShadow">
                <div id="divBoxList" runat="server">
                    <asp:RadioButtonList ID="RadioList1" runat="server" CssClass="RadioList_nowrap" Width="100%"
                        BorderWidth="0" CellPadding="0" CellSpacing="0">
                    </asp:RadioButtonList>
                </div>
                <div id="divButtonArea" runat="server">
                    <div id="divSearch" runat="server" style="width: 100%; display: inline-block; padding: 2px; text-align: left; vertical-align: top; *display: inline; *zoom: 1;">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    </div>
                    <div id="divExit" runat="server" style="width: 100%; display: inline-block; padding: 1px; text-align: center; vertical-align: top; *display: inline; *zoom: 1;">
                        <asp:Button ID="btnClear" runat="server" CausesValidation="false" CssClass="Util_clsBtnGray"
                            Width="46%" Text="Clear" />
                        <asp:Button ID="btnExit" runat="server" CausesValidation="false" CssClass="Util_clsBtnGray"
                            Width="46%" Text="Exit" />
                    </div>
                </div>
            </asp:Panel>
        </div>

        <asp:TextBox ID="txtID" runat="server" Width="100" CausesValidation="false" ViewStateMode="Inherit"
            Text=""></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtID"
            Enabled="false" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    </span>
</div>

