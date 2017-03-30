<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommUserAdmin.aspx.cs" Inherits="Util_CommUserAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common" %>
<%@ Register TagPrefix="asp" Assembly="SinoPac.WebExpress.Common" Namespace="SinoPac.WebExpress.Common.Properties" %>
<%@ Register Src="ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CommUserAdmin</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:Label ID="labErrMsg" runat="server" Visible="false"></asp:Label>
        <div id="divEdit" runat="server">
            <fieldset class="Util_Fieldset">
                <legend class="Util_Legend">
                    <asp:Label ID="labEdit" runat="server" Text="Edit"></asp:Label>
                </legend>
                <div>
                    <asp:TextBox ID="idCompID" runat="server" Width="100" />
                    <asp:TextBox ID="idDeptID" runat="server" Width="100" />
                    <asp:TextBox ID="idUserIDList" runat="server" Width="200" />
                    <br />
                    <asp:TextBox ID="idCompInfo" runat="server" Width="100" />
                    <asp:TextBox ID="idDeptInfo" runat="server" Width="100" />
                    <asp:TextBox ID="idUserInfoList" runat="server" Width="200" />
                    <table>
                        <tr>
                            <td valign="top" width="260" align="left">
                                <fieldset class="Util_Fieldset">
                                    <legend class="Util_Legend">
                                        <%= Resources.CommCascadeSelect_From%></legend>
                                    <div id="DivSelectArea" runat="server" style="height: 135px;">
                                        <div style="padding-top: 12px; padding-left: 0px; margin-left: 0px;">
                                            <uc1:ucCascadingDropDown ID="ucCascadingDropDown1" runat="server" ucCascadingWidth="250" ucDropDownListWidth="240" />
                                        </div>
                                    </div>
                                    <%--加到「選擇結果」--%>
                                    <asp:Button ID="btnAddResult" runat="server" CssClass="Util_clsBtnGray" Width="240px" Visible="true" CausesValidation="false" />
                                </fieldset>
                            </td>
                            <td style="width: 10px;"></td>
                            <td valign="top" width="260" align="left">
                                <fieldset class="Util_Fieldset">
                                    <%--選 擇 結 果--%>
                                    <legend class="Util_Legend">
                                        <%= Resources.CommCascadeSelect_To%></legend>
                                    <div id="divResultBoxList" runat="server" style="font-size: 10pt; white-space: nowrap; border: 1px solid silver; overflow: auto; width: 300px; height: 160px;">
                                        <ul id="ResultItemContent" runat="server" class="Util_MultiSelect_ReorderContent">
                                            <asp:Literal ID="defResultItem" runat="server" Text="" />
                                        </ul>
                                    </div>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center; padding-top: 10px;">
                                <asp:Button ID="btnSave" runat="server" Visible="true" CssClass="Util_clsBtnGray" Style="width: 350px;" CausesValidation="false"
                                    OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>


                </div>
            </fieldset>
        </div>
    </form>
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

            var oValue = $("#<%=ucCascadingDropDown1.FindControl("ddl01").ClientID%> :selected").val();
        $('#<%=idCompID.ClientID%>').val(oValue);

        var oText = "";
        if (oValue.length > 0) oText = $("#<%=ucCascadingDropDown1.FindControl("ddl01").ClientID%> :selected").text();
        $('#<%=idCompInfo.ClientID%>').val(oText);

        var oValue = $("#<%=ucCascadingDropDown1.FindControl("ddl02").ClientID%> :selected").val();
        $('#<%=idDeptID.ClientID%>').val(oValue);

        var oText = "";
        if (oValue.length > 0) oText = $("#<%=ucCascadingDropDown1.FindControl("ddl02").ClientID%> :selected").text();
        $('#<%=idDeptInfo.ClientID%>').val(oText);

        var oValue = $("#<%=ResultItemContent.ClientID%> li").map(function () { return $(this).attr('data-value'); }).get().join(',');
        $('#<%=idUserIDList.ClientID%>').val(oValue);

        var oText = $("#<%=ResultItemContent.ClientID%> li div").map(function () { return $(this).text(); }).get().join(',');
        $('#<%=idUserInfoList.ClientID%>').val(oText);
    }
    </script>
</body>
</html>
