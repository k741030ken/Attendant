<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPageInfo.ascx.cs" Inherits="Util_ucPageInfo" %>
<div id="DivQueryArea" runat="server" visible="false">
    <br />
    <fieldset class="Util_Fieldset" ><legend class="Util_Legend Util_Pointer" onclick="Util_ToggleDisplay('QueryStringInfo');" ><asp:Label ID="labQueryString" runat="server"></asp:Label></legend>
        <div id="QueryStringInfo" >
        <asp:Literal ID="ltlQueryString" runat="server"></asp:Literal>
        </div>
    </fieldset>
</div>
<div id="DivFormArea" runat="server" visible="false">
    <br />
    <fieldset class="Util_Fieldset" ><legend class="Util_Legend Util_Pointer" onclick="Util_ToggleDisplay('QueryFormInfo');" ><asp:Label ID="labQueryForm" runat="server"></asp:Label></legend>
        <div id="QueryFormInfo" >
        <asp:Literal ID="ltlQueryForm" runat="server"></asp:Literal>
        </div>
    </fieldset>
</div>
<div id="DivSessionArea" runat="server" visible="false">
    <br />
    <fieldset class="Util_Fieldset" ><legend class="Util_Legend Util_Pointer" onclick="Util_ToggleDisplay('SessionInfo');" ><asp:Label ID="labSession" runat="server"></asp:Label></legend>
        <div id="SessionInfo" >
        <asp:Literal ID="ltlSession" runat="server"></asp:Literal>
        </div>
    </fieldset>
</div>
<div id="DivApplicationArea" runat="server" visible="false">
    <br />
    <fieldset class="Util_Fieldset" ><legend class="Util_Legend Util_Pointer" onclick="Util_ToggleDisplay('ApplicationInfo');" ><asp:Label ID="labApplication" runat="server"></asp:Label></legend>
        <div id="ApplicationInfo" >
        <asp:Literal ID="ltlApplication" runat="server"></asp:Literal>
        </div>
    </fieldset>
</div>
<div id="DivBrowserArea" runat="server" visible="false">
    <br />
    <fieldset class="Util_Fieldset" ><legend class="Util_Legend Util_Pointer" onclick="Util_ToggleDisplay('EnvironmentInfo');" >Environment Information</legend>
        <div id="EnvironmentInfo" >
        <asp:Literal ID="ltlEnvironment" runat="server"></asp:Literal>
        </div>
    </fieldset>
</div>