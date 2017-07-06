<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ucPageGetEmpID.aspx.cs" Inherits="Util_ucPageGetEmpID" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function CloseWindow() {
            //            var btnSave = window.document.getElementById("hidModalPopupClientID").value + "_btnSave";
            var btnClose = window.document.getElementById("hidModalPopupClientID").value + "_ucEmpModalPopup_btnClose";
            //            window.parent.document.getElementById(btnSave).click();
            window.parent.document.getElementById(btnClose).click();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 20%;
        }
        .body
        {
            font-family: "微軟正黑體" , "微软雅黑" , "Microsoft JhengHei" , Arial, sans-serif;
            color: #000000;
        }
        .GridViewStyle
        {
            font-size: 15px;
            background-color: white;
            border-color: #3366CC;
            border-style: none;
            border-width: 1px;
        }
        .td_detail
        {
            border-width: 1px;
            border-style: solid;
            border-color: #89b3f5;
            font-size: 14px;
            font-family: Calibri, 新細明體;
            height: 16px;
        }
        .td_header
        {
            text-align: center;
            background-color: #89b3f5;
            color: dimgray;
            font-size: 14px;
            border-width: 1px;
            border-style: solid;
            border-color: #5384e6;
            font-weight: normal;
        }
        .tbl_Condition
        {
            border-right: lightgrey 1px solid;
            border-top: lightgrey 1px solid;
            font-size: 14px;
            border-left: lightgrey 1px solid;
            color: black;
            border-bottom: lightgrey 1px solid;
            background-color: #d0e0ff;
        }
        .tr_evenline
        {
            background-color: White;
        }
        .tr_oddline
        {
            background-color: #e2e9fe;
        }
        .GridView_PagerStyle
        {
            background-color: white;
            font-weight: bold;
            text-align: left;
            color: #003399;
        }
        .buttonface
        {
            background-color:White;
            border-right: gray 1px solid;
            border-top: gray 1px solid;
            font-size: 14px;
            border-left: gray 1px solid;
            cursor: hand;
            color: black;
            padding-top: 3px;
            border-bottom: gray 1px solid;
            background-repeat: repeat-x;
            font-family: Calibri, 新細明體, 'Courier New' , 細明體;
            letter-spacing: 1px;
            border-collapse: collapse;
            text-align: center;
        }
        .GridView_EmptyRowStyle, .GridView_EmptyRowStyle > td
        {
            border-width: 1px;
            border-style: solid;
            border-color: #89b3f5;
            text-align: center;
            background-color: #ffffff;
            font-size: 14px;
        }
        .Util_Fieldset
        {
            padding: 10px;
            border: 1px solid #556C98;
             background-color: #ffffff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:HiddenField ID="hidModalPopupClientID" runat="server" />
        <%--<fieldset class="Util_Fieldset">--%>
            <asp:Button ID="btnOK" runat="server" Text="確定" OnClick="btnOK_Click" CssClass="buttonface"
                Height="25px" />
            <center>
                <table width="100%" class="tbl_Condition">
                    <tr>
                        <td align="right" class="style1">
                            請選擇公司：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpCompID" runat="server" AutoPostBack="true" Width="195px"
                                OnSelectedIndexChanged="ddlEmpCompID_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:Label ID="lblMsg" runat="server" Text="(請選擇公司代號)" ForeColor="Red" 
                            Font-Size="20px" Visible="False">
                        </asp:Label>--%>
                            <%--                 <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server">
                            </asp:MaskedEditExtender>--%>
                        </td>
                    </tr>
                    <%--<tr>
                    <td align="right">
                        部:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmpDeptID" runat="server" AutoPostBack="true" Width="195px"
                            OnSelectedIndexChanged="ddlEmpDeptID_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                    <tr>
                        <td align="right" class="style1">
                            請選擇單位：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpOrganID" runat="server" AutoPostBack="true" Width="195px"
                                OnSelectedIndexChanged="ddlEmpOrganID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style1">
                            請選擇人員：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmpID" runat="server" AutoPostBack="true" Width="195px"
                                OnSelectedIndexChanged="ddlEmpID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style1">
                            <%--<asp:TextBox runat="server" ID="txtQueryString" Width="100%" ></asp:TextBox>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txtQueryString" runat="server" Width="100%" AutoPostBack="true"
                                Style="text-transform: uppercase" MaxLength="6"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="btnQuery" CssClass="buttonface" Text="員編或姓名速查" Height="25px"
                                OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="padding-top: 30px;">
                            <asp:GridView Font-Size="12px" Font-Names="Microsoft Sans Serif" CssClass="GridViewStyle"
                                HeaderStyle-CssClass="td_header" RowStyle-CssClass="td_detail" AllowPaging="true"
                                PageSize="12" runat="server" ID="gvMain" Width="100%" AutoGenerateColumns="false"
                                DataKeyNames="CompID,CompName,EmpID,EmpNameN,DeptID,DeptName,OrganID,OrganName,WorkTypeID,WorkType,PositionID,Position,FlowOrganID,FlowOrganName,TitleID,TitleName, _Key"
                                OnRowCommand="gvMain_RowCommand" OnPageIndexChanging="gvMain_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="td_detail" Height="15px" />
                                        <HeaderStyle CssClass="td_header" Width="5%" />
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="選取" CommandName="select" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="部門" DataField="DeptName" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="科組課" DataField="OrganName" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="員工姓名" DataField="EmpNameN" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="員工編號" DataField="EmpID" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="職位" DataField="Position" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="工作性質" DataField="WorkType" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderStyle-Width="10%" HeaderText="職稱" DataField="TitleName" ItemStyle-HorizontalAlign="Center" />
                                    <%--<asp:BoundField HeaderStyle-Width="10%" HeaderText="工作地點" DataField="WorkSiteName" ItemStyle-HorizontalAlign="Center" />--%>
                                </Columns>
                                <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="tr_evenline" />
                                <AlternatingRowStyle CssClass="tr_oddline" />
                                <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="#b5efff" />
                                <PagerStyle CssClass="GridView_PagerStyle" />
                                <PagerSettings Position="Top" />
                            </asp:GridView>
                            <asp:TextBox runat="server" ID="TextBox1" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <%-- <hr class="Util_clsHR" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
            </center>
        <%--</fieldset>--%>
    </div>
    </form>
</body>
</html>
