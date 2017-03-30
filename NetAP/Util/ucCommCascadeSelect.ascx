<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommCascadeSelect.ascx.cs"
    Inherits="Util_ucCommCascadeSelect" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register Src="ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc1" %>
<script type="text/javascript">
    //往上
    $(document).on('click', '#<%=ResultItemContent.ClientID%> .reorder-up', function () {
        var $current = $(this).closest('li')
        var $previous = $current.prev('li');
        if ($previous.length !== 0) {
            $current.insertBefore($previous);
            <%= this.ClientID%>_calData();
        }
        return false;
    });

    //往下
    $(document).on('click', '#<%=ResultItemContent.ClientID%> .reorder-down', function () {
        var $current = $(this).closest('li')
        var $next = $current.next('li');
        if ($next.length !== 0) {
            $current.insertAfter($next);
            <%= this.ClientID%>_calData();
        }
        return false;
    });

    //移除
    $(document).on('click', '#<%=ResultItemContent.ClientID%> .reorder-remove', function () {
        $(this).closest('li').remove();
        <%= this.ClientID%>_calData();
        return false;
    });

    //公司選單
    $(document).on('click', '#<%=ucCascadingDropDown1.FindControl("ddl01").ClientID%>', function () {
        <%= this.ClientID%>_calData();
        return false;
    });

    //部門選單
    $(document).on('click', '#<%=ucCascadingDropDown1.FindControl("ddl02").ClientID%>', function () {
        <%= this.ClientID%>_calData();
        return false;
    });

    //收集資料並傳回
    function <%= this.ClientID%>_calData() {

        // 2016.03.31 加入[parent.document]偵測
        var oParentAccess = true;
        try {
            parent.document;
        } catch (e) {
            oParentAccess = false;
        }

        var oValue = $("#<%=ucCascadingDropDown1.FindControl("ddl01").ClientID%> :selected").val();
        $('#<%=idCompID.ClientID%>').val(oValue);
        var oParentID = '<%=ucSelectedCompIDToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oValue;
            }
        }

        var oText = "";
        if (oValue.length > 0) oText = $("#<%=ucCascadingDropDown1.FindControl("ddl01").ClientID%> :selected").text();
        $('#<%=idCompInfo.ClientID%>').val(oText);
        var oParentID = '<%=ucSelectedCompInfoToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oText;
            }
        }

        var oValue = $("#<%=ucCascadingDropDown1.FindControl("ddl02").ClientID%> :selected").val();
        $('#<%=idDeptID.ClientID%>').val(oValue);
        var oParentID = '<%=ucSelectedDeptIDToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oValue;
            }
        }

        var oText = "";
        if (oValue.length > 0) oText = $("#<%=ucCascadingDropDown1.FindControl("ddl02").ClientID%> :selected").text();
        $('#<%=idDeptInfo.ClientID%>').val(oText);
        var oParentID = '<%=ucSelectedDeptInfoToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oText;
            }
        }

        var oValue = $("#<%=ResultItemContent.ClientID%> li").map(function () { return $(this).attr('data-value'); }).get().join(',');
        $('#<%=idUserIDList.ClientID%>').val(oValue);
        var oParentID = '<%=ucSelectedUserIDListToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oValue;
            }
        }

        var oText = $("#<%=ResultItemContent.ClientID%> li div").map(function () { return $(this).text(); }).get().join(',');
        $('#<%=idUserInfoList.ClientID%>').val(oText);
        var oParentID = '<%=ucSelectedUserInfoListToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oText;
            }
        }
    }
</script>

<asp:Panel ID="Panel1" runat="server">
    <asp:TextBox ID="idCompID" runat="server" Width="100" CausesValidation="false" />
    <asp:TextBox ID="idDeptID" runat="server" Width="100" CausesValidation="false" />
    <asp:TextBox ID="idUserIDList" runat="server" Width="200" CausesValidation="false" />
    <br />
    <asp:TextBox ID="idCompInfo" runat="server" Width="100" CausesValidation="false" />
    <asp:TextBox ID="idDeptInfo" runat="server" Width="100" CausesValidation="false" />
    <asp:TextBox ID="idUserInfoList" runat="server" Width="200" CausesValidation="false" />
    <table>
        <tr>
            <td valign="top" align="left">
                <fieldset class="Util_Fieldset">
                    <legend class="Util_Legend">
                        <%= Resources.CommCascadeSelect_From%></legend>
                    <div id="Area_CommUserList" runat="server" style="padding-left: 0px; margin-left: 0px;">
                        <asp:NoValidationDropDownList runat="server" ID="ddlCommUser" Height="20" Width="240"
                            CausesValidation="false" />
                        <br />
                        <asp:Button ID="btnAddCommUser" runat="server" CssClass="Util_clsBtnGray" Visible="true"
                            CausesValidation="false" UseSubmitBehavior="False" />
                    </div>
                    <div style="padding-top: 12px; padding-left: 0px; margin-left: 0px;">
                        <uc1:ucCascadingDropDown ID="ucCascadingDropDown1" runat="server" ucIsVerticalLayout="true"
                            ucCascadingWidth="250" ucDropDownListWidth="240" CausesValidation="false" />
                    </div>
                    <%--加到「選擇結果」--%>
                    <asp:Button ID="btnAddResult" runat="server" CssClass="Util_clsBtnGray" Visible="true"
                        CausesValidation="false" UseSubmitBehavior="False" />
                </fieldset>
            </td>
            <td style="width: 10px;"></td>
            <td id="Area_ResultList" runat="server" valign="top" align="left">
                <fieldset class="Util_Fieldset">
                    <%--選 擇 結 果--%>
                    <legend class="Util_Legend">
                        <%= Resources.CommCascadeSelect_To%></legend>
                    <div id="divResultBoxList" runat="server">
                        <ul id="ResultItemContent" runat="server" class="Util_MultiSelect_ReorderContent">
                            <asp:Literal ID="defResultItem" runat="server" Text="" />
                        </ul>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Panel>
