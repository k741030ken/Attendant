<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCascadingDropDown.ascx.cs"
    Inherits="Util_ucCascadingDropDown" ViewStateMode="Disabled" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
    <asp:Label ID="labErrMsg" runat="server" Text="" Visible="false"></asp:Label>
    <span id="divHeadArea" runat="server" style="vertical-align: top;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="padding: 2px 0px 2px 0px; vertical-align: top;"><span>
        <asp:TextBox ID="txtSearch" runat="server" Visible="false"></asp:TextBox>
        <asp:Literal ID="LiteralSearch" runat="server"></asp:Literal>
        <asp:NoValidationDropDownList runat="server" ID="ddl01" CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl01"
            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </span><span>
        <asp:NoValidationDropDownList runat="server" ID="ddl02" CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddl02"
            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    </span><span>
        <asp:NoValidationDropDownList runat="server" ID="ddl03" CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddl03"
            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    </span><span>
        <asp:NoValidationDropDownList runat="server" ID="ddl04" CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddl04"
            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:Literal ID="Literal4" runat="server"></asp:Literal>
    </span><span>
        <asp:NoValidationDropDownList runat="server" ID="ddl05" CausesValidation="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddl05"
            Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
    </span><span>
        <ajaxToolkit:CascadingDropDown runat="server" ID="CascadingDropDown1" TargetControlID="ddl01"
            UseContextKey="true">
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown runat="server" ID="CascadingDropDown2" TargetControlID="ddl02"
            UseContextKey="true" ParentControlID="ddl01">
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown runat="server" ID="CascadingDropDown3" TargetControlID="ddl03"
            UseContextKey="true" ParentControlID="ddl02">
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown runat="server" ID="CascadingDropDown4" TargetControlID="ddl04"
            UseContextKey="true" ParentControlID="ddl03">
        </ajaxToolkit:CascadingDropDown>
        <ajaxToolkit:CascadingDropDown runat="server" ID="CascadingDropDown5" TargetControlID="ddl05"
            UseContextKey="true" ParentControlID="ddl04">
        </ajaxToolkit:CascadingDropDown>
    </span></span>
</div>
