<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM2000.aspx.vb" Inherits="OM_OM2000" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<%--<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE11" />--%>
    <title></title>
    <%--↓用模糊搜尋UC時一定要加這三行↓--%>
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <%--↑用模糊搜尋UC時一定要加這三行↑--%>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case "btnUpdate":
                case "btnDelete":
                    if (!hasSelectedRow(''))
                    {
                        alert("未選取資料列！");
                        return false;
                    }
            }
            
            switch(Param)
            {
                case "btnDelete":
                    if (!confirm('確定刪除此筆資料？'))
                        return false;
                    break;
            }
        }
        
        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                try
                {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch(ex)
                {}
            }
        }

        $(function () {
            $.widget("custom.autocombo", {
                // default options
                options: {
                    source: [],
                    linkItem: null,
                    linkItemFix: true,
                    disabled: false,
                    using_first: false,
                    showItems: 10
                },
                //global vars
                vars: {
                    item_size: 0,
                    selectIndex: -1
                },
                _create: function () {
                    var self = this;
                    if (undefined != self.options.linkItem) {
                        self.options.linkItem.attr('readonly', self.options.linkItemFix);
                    }
                    var div = $("<div>")
			.addClass("div-pack div-container")
			.bind("mouseleave", function (e) {
			    //console.log("mouseleave");
			    setTimeout(function () {
			        ul.hide();
			    }, 500);
			}).appendTo(this.element.parent());
                    this.element
            .addClass("ui-autocomplete-input")
			.bind("input", function (e) {
			    self._showItems(ul, self.options.source, e.target.value);
			}).bind("click", function () {
			    self._showItems(ul, self.options.source, self.element.val());
			}).keyup(function (e) {
			    var keyCode = $.ui.keyCode;
			    switch (e.keyCode) {
			        case keyCode.UP:
			            $(ul).find("li:eq(" + self.vars.selectIndex + ")").removeClass("ui-state-highlight");
			            self.vars.selectIndex = self.vars.selectIndex - 1;
			            if (self.vars.selectIndex < 0) self.vars.selectIndex = 0;
			            if (self.vars.selectIndex != -1) {
			                $(ul).find("li:eq(" + self.vars.selectIndex + ")").addClass("ui-state-highlight");
			            }
			            self._changeScrollPosition(ul, self.vars.selectIndex, keyCode.UP);
			            break;
			        case keyCode.DOWN:
			            $(ul).find("li:eq(" + self.vars.selectIndex + ")").removeClass("ui-state-highlight");
			            self.vars.selectIndex = self.vars.selectIndex + 1;
			            if (self.vars.selectIndex > self.vars.item_size - 1) self.vars.selectIndex = self.vars.item_size - 1;
			            if (self.vars.selectIndex != -1) {
			                $(ul).find("li:eq(" + self.vars.selectIndex + ")").addClass("ui-state-highlight");
			            }
			            self._changeScrollPosition(ul, self.vars.selectIndex, keyCode.DOWN);
			            break;
			        case keyCode.ENTER:
			        case keyCode.TAB:
			            self._setValue(ul, self.vars.selectIndex, self.element, self.options.linkItem);
			            self.vars.selectIndex = -1;
			            ul.hide();
			            break;
			        case keyCode.ESCAPE:
			            self.vars.selectIndex = -1;
			            ul.hide();
			            break;
			        default:
			            self._showItems(ul, self.options.source, e.target.value);
			            break;
			    }
			    //console.log("li size : " + self.vars.item_size + " , keycode : " + e.keyCode + " , selectindex :" + self.vars.selectIndex + " , showItems :" + self.options.showItems);
			}).appendTo(div);
                    var ul = $("<ul>")
			.width(this.element.width() + 5)
			.position({
			    my: "left top",
			    at: "left top",
			    of: this.element
			})
			.css("top","60px")
			.css("height", "200px")
			.css("display", "block")
			.css("overflow", "scroll").css("overflow-x", "hidden")
			.addClass("ui-menu ui-widget ui-widget-content ui-autocomplete ui-front")
			.appendTo(div)
			.on("click", "li", function (e) {
			    self._setValue(ul, self.vars.selectIndex, self.element, self.options.linkItem);
			    self.vars.selectIndex = -1;
			    ul.hide();
			}).on("mouseenter", "li", function (e) {
			    self.vars.selectIndex = $(e.target).index();
			    //ul.find("li").removeClass("ui-state-highlight");
			    for (i = 0; i < self.options.showItems; i++) {
			        ul.find("li:eq(" + (self.vars.selectIndex + i) + ")").removeClass("ui-state-highlight");
			        ul.find("li:eq(" + (self.vars.selectIndex - i) + ")").removeClass("ui-state-highlight");
			    }
			    $(e.target).addClass("ui-state-highlight");
			    //console.log("select item index : " + self.vars.selectIndex );
			}).on("mouseleave", "li", function (e) {
			    self.vars.selectIndex = -1;
			    $(e.target).removeClass("ui-state-highlight");
			}).hide();
                },
                _destroy: function () {

                },
                _changeScrollPosition: function (ul, index, s) {
                    if (this.vars.item_size <= 0) return;
                    var li_top = $(ul).find("li:eq(" + index + ")").position().top;
                    var li_height = $(ul).find("li:eq(" + index + ")").height();
                    var ul_height = $(ul).height();
                    var ul_scroll_top = $(ul).scrollTop();
                    //console.log('li top : ' + li_top +', li height: ' + li_height +', ul height: ' + ul_height +', ul scroll top: ' + ul_scroll_top);

                    var keyCode = $.ui.keyCode;
                    switch (s) {
                        case keyCode.UP:
                            //console.log("UP");
                            if (li_top <= 0) ul_scroll_top = ul_scroll_top + li_top;
                            $(ul).scrollTop(ul_scroll_top);
                            break;
                        case keyCode.DOWN:
                            if ((li_top + li_height) > ul_height) ul_scroll_top = ul_scroll_top + ((li_top + li_height) - ul_height);
                            //console.log("DOWN");
                            $(ul).scrollTop(ul_scroll_top);
                            break;
                        default:
                            break;
                    }
                },
                _setValue: function (ul, index, input, linkItem) {
                    if (index > -1) {
                        var itemtext = $(ul).find("li:eq(" + index + ")").text();
                        if (undefined != linkItem) {
                            if (itemtext.indexOf("-") >= 0) {
                                input.val(itemtext.split("-")[0].trim());
                                linkItem.val(itemtext.slice(itemtext.indexOf("-") + 1, itemtext.indexOf("(")).trim());
                            } else {
                                input.val(itemtext);
                                linkItem.val(itemtext);
                            }
                        } else {
                            input.val(itemtext);
                        }
                    } else {
                        linkItem.val("");
                    }
                },
                _showItems: function (ul, items, s) {
                    var is = this._filterItems(items, s);
                    this._setItems(ul, is, s);
                    this.vars.selectIndex = -1;
                    if (this.vars.item_size > 0) {
                        //console.log('b li height: ' + $(ul).find( "li:eq(0)" ).height());
                        //console.log('b ul height: ' + $(ul).height());
                        ul.show();
                        ul.scrollTop(ul.find("li:eq(0)").position().top);
                    } else {
                        ul.hide();
                    }
                    if (this.options.using_first && this.vars.item_size > 0) {
                        this.vars.selectIndex = 0;
                        ul.find("li:eq(0)").addClass("ui-state-highlight");
                    }

                },
                _filterItems: function (items, s) {
                    var is = [];
                    jQuery.each(items, function (index, item) {
                        if (item.toUpperCase().indexOf(s.toUpperCase()) != -1)
                            is.push(item);
                    });
                    return is;
                },
                _setItems: function (ul, items, s) {
                    var h = "";
                    var item_size = 0;
                    jQuery.each(items, function (index, item) {
                        h += "<li class='ui-memu-item'>" + item + "</li>";
                        item_size++;
                    });
                    this.vars.item_size = item_size;
                    //console.log("li size : " + this.vars.item_size);
                    ul.html(h);
                }

            });

    var ddlOrganID = "ddlOrganID";
	var availableTags = [];
	$("#"+ddlOrganID).find("option").each(function () {
                availableTags.push($(this).val());
            });

    $("#ucOrganID").autocombo({
      source: availableTags,
	  using_first: true,
	  linkItem: $("#txtOrganName"),
	  showItems: 10
});	

  });
       -->
    </script>
      <style type="text/css">
    .ui-autocomplete
    {
        font-size: 12px;
        max-height: 200px;
        overflow-x: hidden;
        overflow-y: auto;
    }
</style>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
            EnableScriptLocalization="true" ID="ScriptManager1" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblCompID" runat="server" Text="公司代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" 
                                    style="display:none" />
                                <asp:HiddenField ID="IsDoQuery" runat="server"></asp:HiddenField>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganType" runat="server" Text="組織類型：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlOrganType" runat="server" Font-Names="微軟正黑體" AutoPostBack="True">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-行政組織"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-功能組織"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganID" runat="server" Text="部門代碼：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
<%--                                <asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="0" Text="----請選擇----"></asp:ListItem>
                                </asp:DropDownList>--%>
                                 <asp:TextBox ID="ucOrganID" runat="server" CssClass="InputTextStyle_Thin" Width="90%"></asp:TextBox>
                              <%--  <ajaxToolkit:AutoCompleteExtender ID="ucOrganID_AutoCompleteExtender"  ServicePath="AddressService.svc" ServiceMethod="GetCompletionList"
                                    runat="server"  TargetControlID="ucOrganID" 
                                    UseContextKey="True" MinimumPrefixLength="1">
                                </ajaxToolkit:AutoCompleteExtender>--%>
                                <asp:DropDownList ID="ddlOrganID" runat="server"  style="display:none;"></asp:DropDownList>
                                <asp:Button ID="btnTextChange" runat="server" style="display:none;"></asp:Button>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblOrganName" runat="server" Text="部門名稱：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:TextBox CssClass="InputTextStyle_Thin" ID="txtOrganName" runat="server" Font-Names="微軟正黑體" Width="100%"></asp:TextBox>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblInValidFlag" runat="server" Text="無效註記：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <asp:DropDownList ID="ddlInValidFlag" runat="server" Font-Names="微軟正黑體">
                                    <asp:ListItem Value="" Text="----請選擇----"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-有效"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-無效"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblValidDate" runat="server" Text="單位有效起日：" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="left" width="25%">
                                <uc:ucCalender ID="txtValidDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="Label1" ForeColor="blue" runat="server" Text="～"></asp:Label>
                                <uc:ucCalender ID="txtValidDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                                                    <td align="left" width="15%">
                                                        <asp:Label ID="lblBusinessType" runat="server" Font-Names="微軟正黑體" Text="業務類別："></asp:Label>
                        </td>
                         <td align="left" width="25%">
                             <asp:DropDownList ID="ddlBusinessType" runat="server" Font-Names="微軟正黑體">
                                 <asp:ListItem Text="----請選擇----" Value=""></asp:ListItem>
                             </asp:DropDownList>
                             <asp:Label ID="lblBusinessTypeMsg" runat="server" ForeColor="Red" 
                                 Text="(功能組織使用)"></asp:Label>
                        </td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblUnValidDate" runat="server" Font-Names="微軟正黑體" Text="單位有效迄日："></asp:Label>
                            </td>
                            <td align="left" width="25%">
                            <uc:ucCalender ID="txtUnValidDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="Label2" ForeColor="blue" runat="server" Text="～"></asp:Label>
                                <uc:ucCalender ID="txtUnValidDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td width="10%"></td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" class="tbl_Content">
                        <tr>
                            <td style="width:100%">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain" PerPageRecord="20"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="gvMain" runat="server" AllowSorting="True" 
                                    AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" 
                                    DataKeyNames="CompID,CompName,OrganID,OrganName,OrganType,ValidDateB,hidValidDateB,ValidDateE" Width="100%" 
                                    PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                                    Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" Width="4%" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="4%" Font-Names="微軟正黑體" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="公司名稱" ReadOnly="True" SortExpression="CompName" 
                                            DataField="CompName">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體"/>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="組織類型" ReadOnly="True" HtmlEncode="False"
                                            SortExpression="OrganType" DataField="OrganType">
                                            <HeaderStyle Width="6%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="業務類別" ReadOnly="True" SortExpression="BusinessType" 
                                            DataField="BusinessType">
                                            <HeaderStyle Width="6%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="無效註記" ReadOnly="True" SortExpression="InValidFlag" 
                                            DataField="InValidFlag">
                                            <HeaderStyle Width="9%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="部門代號" ReadOnly="True" SortExpression="OrganID" 
                                            DataField="OrganID">
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="部門名稱" ReadOnly="True" 
                                            SortExpression="OrganName" DataField="OrganName">
                                            <HeaderStyle Width="11%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位有效起日" SortExpression="ValidDateB" 
                                            DataField="ValidDateB">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位有效起日" SortExpression="hidValidDateB" 
                                            DataField="ValidDateB" Visible="false">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="單位有效迄日" SortExpression="ValidDateE" 
                                            DataField="ValidDateE">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動公司" SortExpression="LastChgComp" 
                                            DataField="LastChgComp">
                                            <HeaderStyle Width="10%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動者" SortExpression="LastChgID" 
                                            DataField="LastChgID">
                                            <HeaderStyle Width="7%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動日期" SortExpression="LastChgDate" 
                                            DataField="LastChgDate">
                                            <HeaderStyle Width="13%" CssClass="td_header" Font-Names="微軟正黑體" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" Font-Names="微軟正黑體" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" Font-Names="微軟正黑體"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                                    <HeaderStyle ForeColor="DimGray"></HeaderStyle>
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger  ControlID="ucOrganID"/>
    <asp:PostBackTrigger  ControlID="ddlOrganID"/>
    <asp:PostBackTrigger  ControlID="ddlOrganType"/>
    <asp:PostBackTrigger  ControlID="btnTextChange"/>
     <%--<asp:PostBackTrigger  ControlID="txtValidDateB"/>--%>
     <%--<asp:PostBackTrigger  ControlID="txtValidDateE"/>--%>
     <asp:PostBackTrigger  ControlID="pcMain"/>
     <asp:PostBackTrigger  ControlID="gvMain"/>
    </Triggers>
    
    </asp:UpdatePanel>
       
    </form>
</body>
</html>
