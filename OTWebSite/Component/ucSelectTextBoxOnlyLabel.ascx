<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSelectTextBoxOnlyLabel.ascx.vb" Inherits="Component_ucSelectTextBoxOnlyLabel" %>
<asp:TextBox ID="txtSelectText" runat="server" CssClass="InputTextStyle_Thin"></asp:TextBox>
<asp:DropDownList ID="ddlQueryValue" runat="server" style="display:none;"></asp:DropDownList>
<asp:Button ID="btnTextChange" runat="server" style="display:none;"></asp:Button>


<script type="text/javascript" id="jqueryjs" runat="server"></script>
<%--<script type="text/javascript" src="../ClientFun/jquery-1.8.3.min.js"></script>--%>
<%--<script type="text/javascript" src="../ClientFun/jquery-ui-1.8.24.custom.js"></script>--%>
<%--<link rel="stylesheet" type="text/css" href="../css/smoothness/jquery-ui-1.8.24.custom.css" />--%>
<style type="text/css">
    .ui-autocomplete
    {
        font-size: 12px;
        max-height: 200px;
        overflow-x: hidden;
        overflow-y: auto;
    }
</style>
<script type="text/javascript">
    $(function () {
        var QueryData = [];
        var txtSelectText = "<%= txtSelectText.ClientID %>";
        var ddlQueryValue = "<%= ddlQueryValue.ClientID %>";
        var btnTextChange = "<%= btnTextChange.ClientID %>";

        $("#" + ddlQueryValue).find("option").each(function () {
            QueryData.push({ label: $(this).val() });
        });
        $("#" + txtSelectText).autocomplete({
            source: QueryData,
            focus: function (event, ui) {
                $("#" + txtSelectText).val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#" + txtSelectText).val(ui.item.label);
                return false;
            },
            change: function (event, ui) {
                $("#" + btnTextChange).click();
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
			        .data("item.autocomplete", item)
			        .append($("<a></a>").text(item.label))
			        .appendTo(ul);
        };
    });

    
</script>

