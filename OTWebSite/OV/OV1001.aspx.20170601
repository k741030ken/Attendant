<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OV1001.aspx.vb" Inherits="OV_OV1001" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>天然災害設定(新增)</title>
    <style type="text/css">
        .imgPhoto
        {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            margin: auto;
            max-height: 150px;
            max-width: 150px;
        }
        .NoPic
        {
            color: Silver;
            font-size: 30px;
            font-family: Arial;
            font-weight: bolder;
        }
 

        .txtRemarkstyle
        {
            Width :99.5%;
            Height:200px;
            }

        
             .style1
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            background-color: #f3f8ff;
            min-width: 110px;
        }
        .style2
        {
            font-size: 14px;
            height: 20px;
            border: 1px solid #f3f8ff;
            min-width: 110px;
        }
        input, option, span, div, select,label
        {
            font-family: 微軟正黑體,Calibri, 新細明體,sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="True">
    </asp:ToolkitScriptManager>
    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%"
        width="100%">
        <tr>
        <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="Label1" runat="server" Text="*設定方式：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td class="style2" colspan="3">
                <asp:RadioButtonList ID="rdoListType" runat="server" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdoListType_CheckedChanged" AutoPostBack="true" Style="display: inline">
                    <asp:ListItem Text="依縣市" Value="0"></asp:ListItem>
                    <asp:ListItem Text="依人員" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
           <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="lblDate" runat="server" Text="*留守日期：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td class="style2" colspan="3">
                <uc:ucCalender ID="ucBeginDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                    Width="80px" />
                <asp:Label ID="Label3" runat="server" Text=" ~ "></asp:Label>
                <uc:ucCalender ID="ucEndDate" runat="server" CssClass="InputTextStyle_Thin" Enabled="True"
                    Width="80px" />
            </td>
        </tr>
        <tr>
           <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="lblTimeBegin" runat="server" Text="*留守開始時間：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="StartTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="StartTimeM" runat="server">
                </asp:DropDownList>
            </td>
            <td width="10%" class="style1">
                <asp:Label ID="lblTimeEnd" runat="server" Text="*留守結束時間：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td class="style2">
                <asp:DropDownList ID="EndTimeH" runat="server">
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text=" : "></asp:Label>
                <asp:DropDownList ID="EndTimeM" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
           <td width="10%"></td>
            <td width="10%" class="style1">
                <asp:Label ID="Label2" runat="server" Text="*留守類型：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
            </td>
            <td class="style2" colspan="3">
                <asp:DropDownList ID="ddlType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr id="trCity" runat="server" visible="false">
           <td width="10%"></td>
            <td width="10%" class="style1" style="vertical-align: text-top; margin-top: 10px;">
                <asp:Label ID="lblCityCode" runat="server" Text="*縣市：" Style="margin-top: 10px;" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
                <br />
            </td>
            <td class="style2" style="vertical-align: text-top;">
                <asp:DropDownList ID="ddlCityCode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCityCode_SelectedIndexChanged"
                    Style="margin-top: 10px;">
                </asp:DropDownList>
            </td>
            <td class="style1">
            </td>
            <%--     <td width="45%" class="style2">
                <asp:CheckBox ID="Check_All" runat="server" />
            <br/>
                <asp:CheckBoxList ID="CityArea" runat="server">
                </asp:CheckBoxList>--%>
            <td width="45%" class="style2">
                <asp:GridView ID="gvMain1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    CssClass="GridViewStyle" CellPadding="2" DataKeyNames="WorkSiteID,Remark" Width="40%"
                    PageSize="15" HeaderStyle-ForeColor="dimgray">
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle CssClass="td_detail" Height="15px" />
                            <HeaderStyle CssClass="td_header" Width="1%" />
                            <HeaderTemplate>
                                <uc:ucGridViewChoiceAll ID="ucGridViewChoiceAll" CheckBoxName="chk_gvMain" HeaderText="全選"
                                    runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_gvMain" runat="server" Checked="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="工作地點" ReadOnly="True" SortExpression="FlowSN">
                            <HeaderStyle Width="4%" CssClass="td_header" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle CssClass="tr_oddline" />
                </asp:GridView>
                <asp:Label ID="gvMain1Hidden" runat="server" Text="無工作地點在此縣市" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr id="trPerson" runat="server" visible="false">
           <td width="10%"></td>
            <td colspan="4">
                <table style="width: 100%" class="tbl_Content">
                    <tr>
                        <td width="10%" class="style1">
                            <asp:Label ID="lblRemark" runat="server" Text="說明："></asp:Label>
                        </td>
                        <td class="style2" colspan="3">
                            <asp:TextBox ID="txtRemark" MaxLength="200" TextMode="MultiLine"  
                                runat="server" CssClass="txtRemarkstyle"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="style1">
                            <asp:Label ID="lblCompID" runat="server" Text="*公司：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
                        </td>
                        <td colspan="3" class="style2">
                            <asp:DropDownList ID="ddlCompID" runat="server"  Enabled="false" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOrgType" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDeptID" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOrganID" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Button ID="btnEmpID" runat="server" Text="查詢" />
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" class="style1">
                            <asp:Label ID="lblEmpID" runat="server" Text="員編：" Font-Names="微軟正黑體" ForeColor="blue"></asp:Label>
                        </td>
                        <td colspan="3" class="style2">
                            <asp:TextBox ID="txtEmpID" runat="server" AutoPostBack="true" MaxLength="6" Style="width:100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="tbOTEmpID_FilteredTextBoxExtender" 
                                runat="server" TargetControlID="txtEmpID" FilterType="UppercaseLetters,Numbers">
                            </asp:FilteredTextBoxExtender>
                            <asp:Label ID="txtEmpName" runat="server" Style="width:100px"  ></asp:Label>
                            <uc:ButtonQuerySelectUserID ID="ucQueryEmp" runat="server" WindowHeight="800" WindowWidth="600" ButtonText="..." />
                        </td>
                    </tr>
                </table>
                <table id="table1" class="tbl_Content" border="0" style="width: 100%; height: 100%;
                    font-size: 12px; font-family: 細明體;">
                    <tr valign="middle">
                        <td style="width: 45%" align="center">
                            <asp:Label ID="lblLeftCaption" runat="server" ForeColor="blue" Text="未選人員"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                        <td style="width: 45%" align="center">
                            <asp:Label ID="lblRightCaption" runat="server" ForeColor="blue" Text="已選人員"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 45%" class="style2">
                            <asp:ListBox ID="lstLeft" Width="100%" runat="server" SelectionMode="Multiple" Rows="25"
                               ></asp:ListBox>
                        </td>
                        <td style="width: 5%" align="center">
                            <%--<asp:Button ID="btn_Alladd" runat="server" Text="全部選取" Width = "90%"/>--%>
                            <p>
                                <asp:ImageButton ID="btnMoveRight" runat="server" ImageUrl="../images/Next.gif" CausesValidation="False">
                                </asp:ImageButton>
                                <p>
                                    <asp:ImageButton ID="btnMoveLeft" runat="server" ImageUrl="../images/Prev.gif" CausesValidation="False">
                                    </asp:ImageButton>
                                    <p>
                                        <%--<asp:Button ID="Button1" runat="server" Text="全部刪除" Width = "90%"/>--%>
                        </td>
                        <td style="width: 45%" class="style2">
                            <asp:ListBox ID="lstRight" Width="100%" runat="server" SelectionMode="Multiple" Rows="25"
                                ></asp:ListBox>
                        </td>
                        <td style="width: 5%" align="center">
                            <%--<asp:ImageButton ID="btnMoveUp" runat="server" ImageUrl="../images/btnUp.gif" CausesValidation="False">
                            </asp:ImageButton>
                            <p>
                                <asp:ImageButton ID="btnMoveDown" runat="server" ImageUrl="../images/btnDown.gif"
                                    CausesValidation="False"></asp:ImageButton></p>--%>
                        </td>
                    </tr>
                    <tr valign="middle">
                        <td colspan="4">
                            <asp:TextBox ID="txtLeftResult" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="txtRightResult" runat="server" Style="display: none"></asp:TextBox>
                            <!--回傳值(以,隔開)-->
                            <asp:TextBox ID="txtValueResult" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="txtTextResult" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtReturnValue" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
