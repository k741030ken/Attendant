<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTreeView.ascx.cs" Inherits="Util_ucTreeView" %>
<div id="divTreeToolBar" runat="server" visible="false">
    <div style="border: 0px solid red; position: relative; top: -5px; float: right;">
        <div style="display: inline-block; margin: 5px;">
            <asp:LinkButton ID="btnExpandAll" runat="server" OnClick="btnExpandAll_Click">
                <asp:Image ID="imgExpandAll" runat="server" BorderWidth="0" />
            </asp:LinkButton>
        </div>
        <div style="display: inline-block; margin: 5px;">
            <asp:LinkButton ID="btnCollapseAll" runat="server" OnClick="btnCollapseAll_Click">
                <asp:Image ID="imgCollapseAll" runat="server" BorderWidth="0" />
            </asp:LinkButton>
        </div>
    </div>
</div>
<asp:TreeView ID="MainTree" Style="display: none;" runat="server" OnTreeNodePopulate="MainTree_TreeNodePopulate"></asp:TreeView>
<div style="clear: both;"></div>
<asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>
