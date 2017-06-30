<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGetEmpID.ascx.cs" Inherits="Util_ucGetEmpID" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Util/ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc" %>
    <script type="text/javascript">
        function CloseWindow() {
//            var btnClose = window.document.getElementById(btnSave).value;
            window.parent.document.getElementById(btnSave).click();
        }
    </script>
    <fom ID="form1"  runat="server">
<span style="height: 25px;">
    <asp:FilteredTextBoxExtender ID="ftxtEmpID" runat="server" TargetControlID="txtEmpID"
        FilterType="Numbers,UppercaseLetters,LowercaseLetters">
    </asp:FilteredTextBoxExtender>
    <%--<asp:TextBox ID="txtEmpID" runat="server" Width="100px" AutoPostBack="true" Style="text-transform: uppercase"
        OnTextChanged="txtEmpID_TextChanged" MaxLength="6"></asp:TextBox>--%>
    <asp:ImageButton ID="imgEmpID" runat="server" ImageUrl="~/Util/WebClient/Icon_Group.png"
        OnClick="imgEmpID_Click" />
    <%--<asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label>--%>
<%--<asp:Button ID="btnSave" runat="server" Text="..." 
    onclick="btnSave_Click"  />--%>
    <uc:ucModalPopup ID="ucEmpModalPopup" runat="server"  ucCausesValidation="false" />
</span>
</fom>