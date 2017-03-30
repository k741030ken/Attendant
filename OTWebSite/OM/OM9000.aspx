<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OM9000.aspx.vb" Inherits="OM_OM9000" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>

    <link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />
    <script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>

    <link rel="stylesheet" type="text/css" href="../css/primitives/demo/css/primitives.latest.css" media="screen" />
    <script type="text/javascript" src="../css/primitives/demo/js/primitives.min.js"></script>

    <style type="text/css">
        #diagram
        {
            width: 100%;
            height: 700px;
        }
        .bp-cursor-frame
        {
        	border-color: #FF7744;
        }
        .bp-item2
        {
            position: absolute;
            overflow: visible;/* redefine this atttribute in bp-item class in order to place items outside of boudaries*/
        }
        .bp-badge2
        {
            font-size: 12px;
            line-height: 12px;
            text-align: center;
            text-decoration: none;
            vertical-align: middle;
            font-weight: bold;
            padding: 4px;
            float: left;
            width: 14px; 
            height: 12px;
        }        
    </style>  

    <script type='text/javascript'>//<![CDATA[
        $(window).load(function () {
            $("#btnChange").toggle(
                function () {
                    $(this).val("縮合");
                    jQuery("#diagram").orgDiagram("option", {
                        minimalVisibility: parseInt(1, 10)
                    });
                    jQuery("#diagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
                },
                function () {
                    $(this).val("展開");
                    jQuery("#diagram").orgDiagram("option", {
                        minimalVisibility: parseInt(2, 10),
                        cursorItem: 0
                    });
                    jQuery("#diagram").orgDiagram("update", primitives.common.UpdateMode.Refresh);
                }
            );
        });      //]]>

        function EmpDetail(OrganID) {
            $("#hldOrganID").val(OrganID);
            $("#btnDetail").click();
        }

        function LoadData(OrgType, DeptID, OrganID) {
            $.ajax({
                type: "POST",
                url: "OM9000.aspx/SetOrganGraph",
                data: "{OrgType: '" + OrgType + "', DeptID: '" + DeptID + "', OrganID: '" + OrganID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    var options = new primitives.orgdiagram.Config();
                    var items = $.parseJSON(data.d);
                    if (items.length == 0) {
                        jQuery("#diagram").text("查無資料");
                    } else {
                        options.items = items;
                        options.cursorItem = 0;
                        options.hasSelectorCheckbox = primitives.common.Enabled.False;
                        options.templates = [getRegularTemplate()];
                        options.onItemRender = onTemplateRender;
                        options.onHighlightRender = onHighlightRender;
                        options.defaultTemplateName = "regularTemplate";
                        options.normalItemsInterval = 20; /*add space for badge */
                        jQuery("#diagram").orgDiagram(options);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        }

        function onTemplateRender(event, data) {
            var hrefElement = data.element.find("[name=readmore]");
            switch (data.renderingMode) {
                case primitives.common.RenderingMode.Create:
                    /* Initialize widgets here */
                    hrefElement.click(function (e) {
                        /* Block mouse click propogation in order to avoid layout updates before server postback*/
                        primitives.common.stopPropagation(e);
                    });
                    break;
                case primitives.common.RenderingMode.Update:
                    /* Update widgets here */
                    break;
            }

            var itemConfig = data.context;

            data.element.find("[name=titleBackground]").css({ "background": itemConfig.itemTitleColor });
            data.element.find("[name=photo]").attr({ "src": itemConfig.image });
            data.element.find("[name=title]").text(itemConfig.title);
            data.element.find("[name=boss]").text(itemConfig.boss);
            data.element.find("[name=bTitle]").text(itemConfig.bTitle);
            data.element.find("[name=EmpCnt]").text(itemConfig.EmpCnt + " 人");
            data.element.find("[name=UnderCnt]").text((itemConfig.TotCnt - itemConfig.EmpCnt) + " 人");
            data.element.find("[name=TotCnt]").text(itemConfig.TotCnt + "人");
            hrefElement.attr({ "href": itemConfig.href, "onclick": "EmpDetail('" + itemConfig.id + "')" });
        }

        function getRegularTemplate() {
            var result = new primitives.orgdiagram.TemplateConfig();
            result.name = "regularTemplate";
            result.itemSize = new primitives.common.Size(200, 140);
            result.minimizedItemSize = new primitives.common.Size(10, 10);
            result.highlightPadding = new primitives.common.Thickness(2, 2, 2, 2);
            result.minimizedItemBorderColor = primitives.common.Colors.Gray;

            var itemTemplate = jQuery(
			      '<div class="bp-item bp-corner-all bt-item-frame">'
				    + '<div name="titleBackground" class="bp-item bp-corner-all bp-title-frame" style="top: 2px; left: 2px; width: ' + (result.itemSize.width - 5) + 'px; height: 20px;">'
					    + '<div name="title" class="bp-item bp-title" style="top: 3px; left: 6px; width: ' + (result.itemSize.width - 12) + 'px; height: 18px; color: black;"></div>'
				    + '</div>'
				    + '<div class="bp-item bp-photo-frame" style="top: 26px; left: 2px; width: 50px; height: 60px;">'
					    + '<img name="photo" style="height: 60px; width:50px;" />'
				    + '</div>'
                    + '<div name="boss" class="bp-item" style="top: 26px; left: 56px; width: ' + (result.itemSize.width - 59) + 'px; height: 18px; font-size: 12px;"></div>'
                    + '<div name="bTitle" class="bp-item" style="top: 44px; left: 56px; width: ' + (result.itemSize.width - 59) + 'px; height: 18px; font-size: 12px;"></div>'
                    + '<div class="bp-item" style="top: 62px; left: 56px; height: 18px; font-size: 12px;">單位人員：</div><div name="EmpCnt" class="bp-item" style="top: 62px; right: 40px; height: 18px; font-size: 12px;"></div>'
                    + '<div class="bp-item" style="top: 80px; left: 56px; height: 18px; font-size: 12px;">轄下人員：</div><div name="UnderCnt" class="bp-item" style="top: 80px; right: 40px; height: 18px; font-size: 12px;"></div>'
                    + '<a name="readmore" class="bp-item" style="top: ' + (result.itemSize.height - 20) + 'px; right: 12px; font-size: 13px; font-family: Arial; text-align: right; font-weight: bold; text-decoration: none;">人員明細 (總人數<span name="TotCnt"></span>)</a>'
                + '</div>'
                ).css({
                    width: result.itemSize.width + "px",
                    height: result.itemSize.height + "px"
                }).addClass("bp-item bp-corner-all bt-item-frame");
            result.itemTemplate = itemTemplate.wrap('<div>').parent().html();

            var highlightTemplate = jQuery("<div></div>")
                    .css({
                        position: "absolute",
                        overflow: "visible",
                        width: (result.itemSize.width + result.highlightPadding.left + result.highlightPadding.right) + "px",
                        height: (result.itemSize.height + result.highlightPadding.top + result.highlightPadding.bottom) + "px",
                        "border-style": "solid",
                        "border-width": "1px"
                    }).addClass("bp-item2 bp-corner-all bp-cursor-frame");

            highlightTemplate.append("<div name='badge' class='bp-badge2 bp-item' style='top:45px; left:114px; z-index: 1000; color: white;'></div>");

            result.highlightTemplate = highlightTemplate.wrap('<div>').parent().html();

            return result;
        }

        function onHighlightRender(event, data) {
            switch (data.renderingMode) {
                case primitives.common.RenderingMode.Create:
                    /* Initialize widgets here */
                    break;
                case primitives.common.RenderingMode.Update:
                    /* Update widgets here */
                    break;
            }

            var itemConfig = data.context;

            var badge = data.element.find("[name=badge]");
            badge.text(itemConfig['badge']);
            badge.css({ "background-color": primitives.common.Colors.RoyalBlue });

            var width = data.element.outerWidth();
            var height = data.element.outerHeight();
            badge.css({ "left": width - 14, "top": height - 14 });
        }
    </script>

    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <asp:UpdatePanel ID="UpdMain" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" width="100%">
                    <tr>
                        <td width="10%"></td>
                        <td align="left">
                            <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:Label ID="txtCompID" Font-Size="15px" runat="server"></asp:Label>
                        </td>
                        <td width="10%"></td>
                    </tr>
                    <tr>
                        <td width="10%"></td>
                        <td align="left">
                            <asp:Label ID="lblOrgType" Font-Size="15px" runat="server" Text="單位類別："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOrgType" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblDeptID" Font-Size="15px" runat="server" Text="部門："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="true" Width="200px" Font-Names="細明體"></asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:Label ID="lblOrganID" Font-Size="15px" runat="server" Text="科/組/課："></asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlOrganID" runat="server" Width="200px" Font-Names="細明體"></asp:DropDownList>
                        </td>
                        <td width="10%"></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDeptID" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="btnDetail" Text="" style="display:none;" runat="server" />
        <asp:HiddenField ID="hldOrganID" runat="server" />
        <input type="button" id="btnChange" value="展開" />
        <div id="diagram"></div>
    </form>
</body>
</html>
