<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCheckBoxList.ascx.cs"
    Inherits="Util_ucCheckBoxList" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css" media="all">
    .checkboxlist_nowrap tr td label {
        white-space: nowrap;
        overflow: hidden;
    }
</style>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <div id="divHeadArea" runat="server" style="float: left; vertical-align: top; display: inline-block; padding: 1px 2px; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </div>
    <div id="divDataArea" runat="server" style="position: relative; top: 0px; vertical-align: top;">
        <div style="display: inline-block;">
            <asp:TextBox ID="txtInfoList" runat="server" Width="100" CausesValidation="true" ReadOnly="true"
                ViewStateMode="Inherit" Text=""></asp:TextBox>
            <div id="divPopPanel" runat="server" style="position: absolute; z-index: 9999; display: none; padding: 1px;">
                <asp:Panel ID="pnlBoxList" runat="server" CssClass="Util_clsShadow">
                    <div id="divBoxList" runat="server">
                        <asp:NoValidationCheckBoxList ID="ChkBoxList1" CssClass="checkboxlist_nowrap" runat="server" Width="100%"
                            BorderWidth="0" CellPadding="0" CellSpacing="0">
                        </asp:NoValidationCheckBoxList>
                    </div>
                    <div id="divButtonArea" runat="server">
                        <div id="divSearch" runat="server" style="width: 100%; display: inline-block; padding: 2px; text-align: left; vertical-align: top; *display: inline; *zoom: 1;">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        </div>
                        <div id="divSelect" runat="server" style="float: left; width: 100%; display: inline-block; padding: 1px; text-align: center; vertical-align: top; *display: inline; *zoom: 1;">
                            <asp:Button ID="btnAll" runat="server" CausesValidation="false" CssClass="Util_clsBtnGray" Style="width: 48%; float: left;" Text="All" />
                            <asp:Button ID="btnClear" runat="server" CausesValidation="false" CssClass="Util_clsBtnGray" Style="width: 48%; float: left;" Text="Clear" />
                        </div>
                        <div id="divExit" runat="server" style="float: left; width: 100%; display: inline-block; padding: 1px; text-align: center; vertical-align: top; *display: inline; *zoom: 1;">
                            <asp:Button ID="btnExit" runat="server" CausesValidation="false" CssClass="Util_clsBtnGray"
                                Width="97%" Text="Exit" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <asp:TextBox ID="txtIDList" runat="server" Width="100" CausesValidation="false" ViewStateMode="Inherit"
            Text=""></asp:TextBox>
        <asp:TextBox ID="txtIDQty" runat="server" Width="20" CausesValidation="false" ViewStateMode="Inherit"></asp:TextBox>
        <asp:RangeValidator ID="txtIDQty_RangeValidator" runat="server" Type="Integer" ControlToValidate="txtIDQty"
            Display="Dynamic" ForeColor="Red" Font-Size="10pt" ErrorMessage="*" Enabled="true"></asp:RangeValidator>
    </div>
</div>

