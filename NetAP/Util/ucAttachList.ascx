<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAttachList.ascx.cs"
    Inherits="Util_ucAttachList" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>

<%--¿O½c±±¨î¶µ 2017.01.13--%>
<uc1:ucLightBox runat="server" ID="ucLightBox" />

<div id="DivError" runat="server" visible="false">
    <asp:Label ID="labErrMsg" runat="server" Text=""></asp:Label>
</div>
<div id="DivAttachList" runat="server" visible="true">
    <asp:GridView ID="gvAttach" runat="server" AutoGenerateColumns="False" DataKeyNames="AttachDB,AttachID,SeqNo,AnonymousAccess,IsEditMode"
        OnRowDataBound="gvAttach_RowDataBound" OnRowCommand="gvAttach_RowCommand" CellPadding="2"
        GridLines="Both" Width="100%" Font-Size="Small" ShowHeaderWhenEmpty="true" 
        HeaderStyle-CssClass="Util_gvHeader"
        FooterStyle-CssClass="Util_gvFooter"
        RowStyle-CssClass="Util_gvRowNormal"
        AlternatingRowStyle-CssClass="Util_gvRowAlternate"
        SortedAscendingHeaderStyle-CssClass="Util_gvHeaderAsc"
        SortedDescendingHeaderStyle-CssClass="Util_gvHeaderDesc"
        >
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="btnSelect" runat="server" CommandName="cmdSelect" Width="16" />
                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="cmdDelete" Width="16" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No.">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="FileName"></asp:BoundField>
            <asp:BoundField DataField="FileSizeKB" DataFormatString="{0:N2} KB">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="UpdDateInfo">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</div>
