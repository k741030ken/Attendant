<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM1000.aspx.vb" Inherits="OM_OM1000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
                case"btnExecutes":
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
        //====================================
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
            .css("top","70px")
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

            var ddlQueryValue = "ddlQueryValue";
            var availableTags = [];
            $("#" + ddlQueryValue).find("option").each(function () {
                availableTags.push($(this).val());
            });

            $("#ucOrganID").autocombo({
                source: availableTags,
                using_first: true,
                linkItem: $("#txtOrganName"),
                showItems: 10
            });

        });
        //====================================
//        $(function () {
//            var QueryData = [];
//            var ucOrganID = "ucOrganID";
//            var ddlQueryValue = "ddlQueryValue";
//            var btnTextChange = "btnTextChange";
//            var frmContent = "frmContent";
//            $("#" + frmContent).keydown(function (event) {
//                if (event.which == 40) {
//                    $(this).autocomplete("search");
//                }
//            });


//            $("#" + ddlQueryValue).find("option").each(function () {
//                QueryData.push({ value: $(this).text(), label: $(this).val() });
//            });
//            $("#" + ucOrganID).focus(function () {
//                $(this).autocomplete("search");
//            });
//            $("#" + ucOrganID).autocomplete({
//                source: QueryData,
//                minLength: 0,
//                focus: function (event, ui) {
//                    $("#" + ucOrganID).val(ui.item.label);
//                    return false;
//                },
//                select: function (event, ui) {
//                    $("#" + ucOrganID).val(ui.item.label.replace("-" + ui.item.value, ""));
//                    $("#txtOrganName").val(ui.item.value);
//                    $("#" + btnTextChange).click();
//                    return false;
//                } /*,
//                change: function (event, ui) {
//                    $("#" + btnTextChange).click();
//                }*/
//            }).data("autocomplete")._renderItem = function (ul, item) {
//                return $("<li></li>")
//			        .data("item.autocomplete", item)
//			        .append($("<a></a>").text(item.label))
//			        .appendTo(ul);
//            };
//        });
       -->
    </script>
    <style type="text/css">
        .style1
        {
            height: 34px;
            Font-family: 微軟正黑體;
            Font-Size:15px;
        }
        .style2
        {
            height: 34px;
            Font-family: 微軟正黑體;
            Font-Size:15px;
        }
        GridView
        {
            border-width: 1px;
	border-style: solid;
	border-color: #89b3f5;
	font-size: 14px;
	font-family:  Calibri, 微軟正黑體;
	height:16px;
            }
                .ui-autocomplete
    {
        font-size: 12px;
        max-height: 200px;
        overflow-x: hidden;
        overflow-y: auto;
    }
    .hahaha
    {
        font-size:15px;
	background-color:white;
	border-color:#3366CC;
	border-style:none;
	border-width:1px;
        }
    
    </style>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
     <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />
<%--     <asp:Button ID="Button1" runat="server" Text="OM1001Beta" />
     OM1001BETA畫面Crash測試用,無新增欄位FlowRodeCode<br />
     <asp:Button ID="Button2" runat="server" Text="OM1001B2" />
     OM1001B2畫面Crash測試用,無新增欄位FlowRodeCode、將subget拆開--%>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="0" style="font-family:@微軟正黑體; width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="10%" class="style1"></td>
                            <td align="left" width="15%" class="style1">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="25%" class="style1">
                                <asp:DropDownList ID="ddlCompID" runat="server"  Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                                <asp:Label ID="lblReleaseResult" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                            </td>
                            <td align="left" width="15%" class="style1">
                                <asp:Label ID="lblOrganType" Font-Size="15px" runat="server" Text="組織類型："></asp:Label>
                            </td>
                            <td align="left" width="25%" class="style1">
                                <asp:DropDownList ID="ddlOrganType" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-行政組織"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-功能組織"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3-行政與功能組織"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%" class="style1"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%" class="style1">
                                <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Text="部門代碼："></asp:Label>
                            </td>
                            <td align="left" width="20%" class="style1">
                                <%--<asp:DropDownList ID="ddlOrganID" runat="server" Font-Names="細明體" ></asp:DropDownList>--%>
                                <%--<uc:ucSelectTextBoxOnlyLabel ID="ucOrganID" runat="server" Font-Names="細明體"  ></uc:ucSelectTextBoxOnlyLabel>--%>
                                <asp:TextBox ID="ucOrganID" runat="server" CssClass="InputTextStyle_Thin" Width="90%"></asp:TextBox>
                                <asp:DropDownList ID="ddlQueryValue" runat="server" style="display:none;"></asp:DropDownList>
                                <asp:Button ID="btnTextChange" runat="server" style="display:none;"></asp:Button>
                                
                            </td>
                            <td align="left" width="10%" class="style1">
                                <asp:Label ID="lblOrganName" Font-Size="15px" runat="server" Text="部門名稱："></asp:Label>
                            </td>
                            <td align="left" width="20%">
<%--                                <asp:UpdatePanel ID="uppOrganName" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <asp:TextBox ID="txtOrganName" runat="server" 
                                    CssClass="InputTextStyle_Thin"  Width="100%"></asp:TextBox>
                                    <%--</ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnTextChange" EventName="Click">
                                        </asp:AsyncPostBackTrigger>
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%" class="style1">
                                <asp:Label ID="lblWaitStatus" Font-Size="15px" runat="server" Text="執行狀態："></asp:Label>
                            </td>
                            <td align="left" width="20%" class="style1">
                                <asp:DropDownList ID="ddlWaitStatus" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0-未生效"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-已生效"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="left" width="10%" class="style1">
                                <asp:Label ID="lblOrganReason" Font-Size="15px" runat="server" Text="異動原因："></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="ddlOrganReason" runat="server" Font-Names="細明體">
                                    <asp:ListItem Value="" Text="---請選擇---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1-組織新增"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2-組織無效"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3-組織異動"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4-組織更名"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="10%"></td>
                            <td align="left" width="10%" class="style1">
                                <asp:Label ID="lblValidDate" Font-Size="15px" runat="server" Text="生效日期："></asp:Label>
                            </td>
                            <td align="left" width="20%" class="style1">
                                <uc:ucCalender ID="txtValidDateB" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                                <asp:Label ID="lblValidDateText" ForeColor="blue" runat="server" Text="～"></asp:Label>
                                <uc:ucCalender ID="txtValidDateE" runat="server" CssClass="InputTextStyle_Thin" Enabled="True" Width="80px" />
                            </td>
                            <td align="left" width="10%">
                            </td>
                            <td align="left" width="20%">
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
                            <td class="style2">
                                <uc:PagerControl ID="pcMain" runat="server" GridViewName="gvMain"  PerPageRecord="20"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" class="style1">
                                <asp:GridView ID="gvMain" runat="server" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" CssClass="GridViewStyle" CellPadding="2" DataKeyNames="CompID,OrganReason,OrganType,ValidDate,OrganID,WaitStatus,Seq,OrganNameOld,UpOrganID" Width="100%" PageSize="15" HeaderStyle-ForeColor="dimgray">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" />
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdo_gvMain" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="明細" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CausesValidation="false" CommandName="Detail"
                                                    Text="明細"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="td_header" Width="5%" />
                                            <ItemStyle CssClass="td_detail" Font-Size="12px" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CompName" HeaderText="公司" ReadOnly="True" SortExpression="CompID">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganType" HeaderText="組織類型" HtmlEncode="false" ReadOnly="True" SortExpression="OrganType">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WaitStatus" HeaderText="執行狀態" ReadOnly="True" SortExpression="WaitStatus">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganID" HeaderText="部門代號" ReadOnly="True" SortExpression="OrganID">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganName" HeaderText="部門名稱" ReadOnly="True" SortExpression="OrganNameA">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganReason" HeaderText="異動原因" ReadOnly="True" SortExpression="OrganReason">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="異動生效日期" SortExpression="ValidDate" DataField="ValidDate">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動公司" SortExpression="LastChgComp" DataField="LastChgComp">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動者" SortExpression="LastChgID" DataField="LastChgID">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="最後異動日期" SortExpression="LastChgDate" DataField="LastChgDate">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="Seq" SortExpression="Seq" DataField="Seq" Visible="false" >
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="CompID" SortExpression="CompID" DataField="CompID"  Visible="false">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="OrganNameOld" SortExpression="OrganNameOld" DataField="OrganNameOld"  Visible="false">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                         <asp:BoundField HeaderText="UpOrganID" SortExpression="UpOrganID" DataField="UpOrganID"  Visible="false">
                                            <HeaderStyle Width="10%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
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
    <asp:PostBackTrigger  ControlID="ddlQueryValue"/>
    <asp:PostBackTrigger  ControlID="btnTextChange"/>
    <asp:PostBackTrigger  ControlID="txtValidDateB"/>
    <asp:PostBackTrigger  ControlID="txtValidDateE"/>
    <asp:PostBackTrigger  ControlID="pcMain"/>
     <asp:PostBackTrigger  ControlID="gvMain"/>
    </Triggers>
    </asp:UpdatePanel>
        
    </form>
</body>
</html>
