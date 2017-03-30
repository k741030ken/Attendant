<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGridView.ascx.cs" Inherits="Util_ucGridView" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<%@ Register Src="~/Util/ucCheckBoxList.ascx" TagName="ucCheckBoxList" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucRadioList.ascx" TagName="ucRadioList" TagPrefix="uc1" %>
<%@ Register Src="~/Util/ucLightBox.ascx" TagPrefix="uc1" TagName="ucLightBox" %>

<cc1:HoverTooltip ID="HoverTooltip1" runat="server" OffsetX="10" OffsetY="10" DefaultTooltipTitle="Tip:">
</cc1:HoverTooltip>
<%--燈箱控制項 2017.01.13--%>
<uc1:ucLightBox runat="server" ID="ucLightBox" />

<asp:Label ID="labErrMsg" runat="server" Height="150" Visible="false"></asp:Label>

<div id="divGridview" runat="server" visible="true">
    <%--批次按鈕區塊--%>
    <div id="divDataEditButtonArea1" runat="server" style="width: 100%; text-align: center; display: none; margin-bottom: 10px;">
        <asp:Button ID="btnUpdateAll1" runat="server" Text="Update All" Style="display: none;" OnClick="btnUpdateAll_Click" />
        <asp:Button ID="btnDeleteAll1" runat="server" Text="Delete All" Style="display: none;" OnClick="btnDeleteAll_Click" />
    </div>
    <%--資料分頁區塊--%>
    <span id="divPageSizeChanger1" runat="server">
        <asp:Label ID="labPerPageItem1" runat="server" Text="" />
        <asp:DropDownList ID="ddlPageSize1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize1_SelectedIndexChanged" />
    </span>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PagingButtonSpacing="10px" Style="margin-bottom: 10px;"
        AlwaysShowFirstLastPageNumber="true" OnPageChanged="AspNetPager1_PageChanged"
        HorizontalAlign="Center" InvalidPageIndexErrorMessage="<%=  Resources.GridView_InvalidPageIndexErrorMessage %>"
        PageIndexOutOfRangeErrorMessage="<%= Resources.GridView_PageIndexOutOfRangeErrorMessage %>"
        ImagePath="~/Util/WebClient" NavigationButtonType="Image" NumericButtonType="Text"
        MoreButtonType="Text" ButtonImageNameExtension="n" CpiButtonImageNameExtension="n"
        DisabledButtonImageNameExtension="g" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList">
    </webdiyer:AspNetPager>
    <%--與分頁區的分隔線--%>
    <hr id="divPagerHR1" runat="server" class="Util_clsHR" />
    <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" AllowSorting="false"
        AllowPaging="false" CellPadding="4" GridLines="None" ShowHeaderWhenEmpty="true"
        OnRowCreated="gvMain_RowCreated" OnRowDataBound="gvMain_RowDataBound" Width="100%">
        <Columns>
            <asp:TemplateField HeaderStyle-Width="38px" ItemStyle-Wrap="False">
                <HeaderTemplate>
                    <asp:Image ID="imgGroupCollapse" runat="server" BorderWidth="0" BorderStyle="None"
                        ImageUrl="~/Util/WebClient/Icon_Collapse.png" />
                    <asp:Image ID="imgGroupExpand" runat="server" BorderWidth="0" BorderStyle="None"
                        ImageUrl="~/Util/WebClient/Icon_Expand.png" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="labGroup" runat="server" Text="" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="labFooter" runat="server" Text="" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderText="CHK">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkAllRow" runat="server" AutoPostBack="false" CausesValidation="false" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRow" runat="server" CausesValidation="false" AutoPostBack="false" />
                    <asp:HiddenField ID="chkRowKey" runat="server" />
                    <asp:HiddenField ID="chkRowData01" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData02" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData03" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData04" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData05" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData06" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData07" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData08" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData09" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData10" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData11" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData12" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData13" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData14" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData15" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData16" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData17" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData18" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData19" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData20" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData21" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData22" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData23" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData24" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData25" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData26" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData27" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData28" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData29" runat="server" Visible="false" />
                    <asp:HiddenField ID="chkRowData30" runat="server" Visible="false" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:HiddenField ID="JS_Dummy" runat="server" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Wrap="false" ShowHeader="True">
                <HeaderTemplate>
                    <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgAdd" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Add.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnDataDump" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgDataDump" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_DataDump.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnExportWord" runat="server" OnClick="btnExport_Click">
                        <asp:Image ID="imgExportWord" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Word.gif" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click">
                        <asp:Image ID="imgExport" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Excel.gif" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnExportOpenXml" runat="server" OnClick="btnExport_Click">
                        <asp:Image ID="imgExportOpenXml" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_ExcelOpenXml.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnExportPdf" runat="server" OnClick="btnExport_Click">
                        <asp:Image ID="imgExportPdf" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_PDF.gif" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server">
                        <asp:Image ID="imgPrint" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Print.gif" />
                    </asp:LinkButton>
                    <asp:NoValidationLinkButton ID="btnSortHidden" runat="server" CausesValidation="false" OnClick="btnSortHidden_Click"></asp:NoValidationLinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnMultilingual" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgMultilingual" runat="server" BorderWidth="0" BorderStyle="None"
                            ImageUrl="~/Util/WebClient/Icon_Multilingual.gif" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnInformation" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgInformation" runat="server" BorderWidth="0" BorderStyle="None"
                            ImageUrl="~/Util/WebClient/Icon_Information.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnSelect" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgSelect" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Select.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnDownload" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgDownload" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Download.gif" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgEdit" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Edit.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgDelete" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Delete.png" />
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCopy" runat="server" OnClick="btnLink_Click">
                        <asp:Image ID="imgCopy" runat="server" BorderWidth="0" BorderStyle="None" ImageUrl="~/Util/WebClient/Icon_Copy.gif" />
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="labRowSeqNo" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--與分頁區的分隔線--%>
    <hr id="divPagerHR2" runat="server" class="Util_clsHR" />
    <%--資料分頁區塊--%>
    <span id="divPageSizeChanger2" runat="server">
        <asp:Label ID="labPerPageItem2" runat="server" Text="" />
        <asp:DropDownList ID="ddlPageSize2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize2_SelectedIndexChanged" />
    </span>
    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" PagingButtonSpacing="10px" Style="margin-top: 10px;"
        AlwaysShowFirstLastPageNumber="true" OnPageChanged="AspNetPager2_PageChanged"
        HorizontalAlign="Center" InvalidPageIndexErrorMessage="<%=  Resources.GridView_InvalidPageIndexErrorMessage %>"
        PageIndexOutOfRangeErrorMessage="<%= Resources.GridView_PageIndexOutOfRangeErrorMessage %>"
        ImagePath="~/Util/WebClient" NavigationButtonType="Image" NumericButtonType="Text"
        MoreButtonType="Text" ButtonImageNameExtension="n" CpiButtonImageNameExtension="n"
        DisabledButtonImageNameExtension="g" ShowPageIndexBox="Always" PageIndexBoxType="DropDownList">
    </webdiyer:AspNetPager>
    <%--批次按鈕區塊--%>
    <div id="divDataEditButtonArea2" runat="server" style="width: 100%; text-align: center; display: none; margin-top: 10px;">
        <asp:Button ID="btnUpdateAll2" runat="server" Text="Update All" Style="display: none;" OnClick="btnUpdateAll_Click" />
        <asp:Button ID="btnDeleteAll2" runat="server" Text="Delete All" Style="display: none;" OnClick="btnDeleteAll_Click" />
    </div>
</div>
