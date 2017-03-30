<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AclMenu.aspx.cs" Inherits="AclExpress_Admin_AclMenu" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AclMenu</title>
</head>
<body style="background-color:#D1F7C4;">
    <form id="form1" runat="server">
        <div id="divError" runat="server">
            <asp:Label ID="labErrMsg" runat="server" ></asp:Label>
        </div>
        <div id="divAdminArea" runat="server" style="width: 100%;">
            <asp:TreeView ID="TreeView1" runat="server" ImageSet="XPFileExplorer" ShowLines="true" OnTreeNodePopulate="TreeView1_TreeNodePopulate"
                ViewStateMode="Disabled" CssClass="Util_TreeView">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="0px"
                    NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"
                    ForeColor="#5555DD" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
