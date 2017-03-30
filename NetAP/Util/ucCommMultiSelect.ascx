<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCommMultiSelect.ascx.cs"
    Inherits="Util_ucCommMultiSelect" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>

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
        var chkValue = $(this).closest('li').attr('data-value');
        //JS [value] 設值時加入雙引號防止當為特殊符號時會造成誤判 2016.08.17
        $('#<%= idSourceBoxList.ClientID %> :checkbox[value="' + chkValue + '"]').attr('checked', false).closest('tr').attr('class', '');
        $(this).closest('li').remove();
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

        var oValue = $("#<%=ResultItemContent.ClientID%> li").map(function () { return $(this).attr('data-value'); }).get().join(',');
        $('#<%=idSelectedIDList.ClientID%>').val(oValue);
        var oParentID = '<%=ucSelectedIDListToParentObjClientID%>';
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
        $('#<%=idSelectedInfoList.ClientID%>').val(oText);
        var oParentID = '<%=ucSelectedInfoListToParentObjClientID%>';
        if (oParentID.length > 0) {
            if (oParentAccess)
                var oParent = window.parent.document.getElementById(oParentID);
            else
                var oParent = document.getElementById(oParentID);

            if (oParent != null) {
                oParent.value = oText;
                // 2016.07.21 新增 TextArea 判斷
                if (oParent.tagName == 'TEXTAREA') {
                    oParent.value = oParent.value.replace(/,/g, '\n');
                }
            }
        }
    }
</script>
<div style="white-space: nowrap; display: inline-block; vertical-align: top; *display: inline;">
    <span id="divHeadArea" runat="server" style="display: inline-block; vertical-align: top; *display: inline; *zoom: 1;">
        <asp:CheckBox ID="chkVisibility" runat="server" Checked="true" Visible="false" /><asp:Label
            ID="labCaption" runat="server" Text="" Visible="false" Width="80" Style="text-align: right;"></asp:Label>
    </span><span id="divDataArea" runat="server" style="position: relative; vertical-align: top; border: 0px solid gray;">
        <asp:TextBox ID="idSelectedIDList" runat="server" CausesValidation="false" Width="200px" />
        <asp:TextBox ID="idSelectedInfoList" runat="server" CausesValidation="false" Width="400px" />
        <table id="MultiSelectArea" runat="server" border="0" style="position: absolute; width: auto;">
            <tr>
                <td style="text-align: left; white-space: nowrap; padding-left: 3px;">
                    <%--候選清單--%>
                    <fieldset class="Util_Fieldset">
                        <legend class="Util_Legend">
                            <%=Resources.CommMultiSelect_From%>
                        </legend>
                        <asp:TextBox ID="txtSearch" runat="server" Visible="false" Height="20"></asp:TextBox>
                        <div id="divSourceBoxList" runat="server">
                            <asp:NoValidationCheckBoxList ID="idSourceBoxList" CssClass="Util_MultiSelect_Checkboxlist" Width="100%"
                                runat="server" ViewStateMode="Disabled" BorderWidth="0" CellPadding="0" CellSpacing="0">
                            </asp:NoValidationCheckBoxList>
                        </div>
                    </fieldset>
                </td>
                <td style="width: 10px;"></td>
                <td style="text-align: left; white-space: nowrap; padding-left: 3px;">
                    <%--選擇結果--%>
                    <fieldset class="Util_Fieldset">
                        <legend class="Util_Legend">
                            <%=Resources.CommMultiSelect_To %>
                        </legend>
                        <div id="divResultBoxList" runat="server">
                            <ul id="ResultItemContent" runat="server" class="Util_MultiSelect_ReorderContent">
                                <asp:Literal ID="defResultItem" runat="server" Text="" />
                            </ul>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="idSelectedIDList"
                            Enabled="false" Display="Dynamic" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; text-align: center;">
                    <%--全部選擇--%>
                    <asp:Button ID="btnSelectAll" runat="server" CssClass="Util_clsBtnGray" UseSubmitBehavior="False" Width="180" />
                </td>
                <td></td>
                <td style="padding-top: 5px; text-align: center;">
                    <%--全部取消--%>
                    <asp:Button ID="btnCancelAll" runat="server" CssClass="Util_clsBtnGray" UseSubmitBehavior="False" Width="180" />
                </td>
            </tr>
        </table>
    </span>
</div>
