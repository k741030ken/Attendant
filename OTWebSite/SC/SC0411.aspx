<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0411.aspx.vb" Inherits="SC_SC0411" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
    -->
    </script>
</head>
<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table width="95%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblFlowID" CssClass="MustInputCaption" runat="server" Text="*流程名稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:DropDownList ID="ddlFlowID" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblFlowVer" CssClass="MustInputCaption" runat="server" Text="*流程版本"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtFlowVer" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblFlowStepID" CssClass="MustInputCaption" runat="server" Text="*關卡編號"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtFlowStepID" CssClass="InputTextStyle_Thin" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblDescription" runat="server" Text="關卡名稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtDescription" Width="300px" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblMenuTitle" runat="server" Text="選單名稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtMenuTitle" Width="300px" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblShowModeMenuTitle" runat="server" Text="唯讀模式選單名稱"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtShowModeMenuTitle" Width="300px" CssClass="InputTextStyle_Thin" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblDefaultPage" runat="server" Text="預設顯示網頁"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtDefaultPage" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" Width="300px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblShowModePage" runat="server" Text="唯讀模式預設網頁"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtShowModePage" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100" Width="300px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblProcDay" runat="server" Text="關卡天數"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtProcDay" CssClass="InputTextStyle_Thin" runat="server" MaxLength="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblIntimation" runat="server" Text="關卡提示"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtIntimation" Width="300px" CssClass="InputTextStyle_Thin" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="lblAgreeRate" runat="server" Text="同意比率"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left"><asp:TextBox ID="txtAgreeRate" CssClass="InputTextStyle_Thin" runat="server" MaxLength="5"></asp:TextBox>&nbsp;&nbsp;&nbsp;<Font style="color:dimgray; font-size:12px; font-family:MS Sans Serif">PS.空白或0表示不考慮此條件</Font>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="15%" align="center"><asp:Label ID="Label1" runat="server" Text="關卡按鈕"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:85%" align="left">
                                <asp:LinkButton ID="lbAddButton" runat="server" Font-Size="12px" Text="新增" ForeColor="blue"></asp:LinkButton><br />
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                    CellPadding="2" Width="100%" PageSize="10" DataKeyNames="SeqNo" HorizontalAlign="center">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUpdate" runat="server" Text="修改" Font-Size="12px" ForeColor="blue" CommandName="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbDelete" runat="server" Text="刪除" Font-Size="12px" ForeColor="blue" CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lbSave" runat="server" Text="儲存" Font-Size="12px" ForeColor="blue" CommandName="Update"></asp:LinkButton>
                                                <asp:LinkButton ID="lbCancel" runat="server" Text="取消" Font-Size="12px" ForeColor="blue" CommandName="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="功能按鍵">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblButtonName" runat="server" Text='<%# Eval("ButtonName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtButtonName" Width="99%" runat="server" Text='<%# Eval("ButtonName") %>' CssClass="InputTextStyle_Thin" MaxLength="20"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="需填<br>意見">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgRequireOpinion" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# iif(Eval("RequireOpinion")="Y","True","False") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkRequireOpinion" runat="server" Checked='<%# iif(Eval("RequireOpinion")="Y","True","False") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="指向關卡">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNextStepID" runat="server" Text='<%# Eval("NextStepID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNextStepID" Width="99%" runat="server" Text='<%# Eval("NextStepID") %>' CssClass="InputTextStyle_Thin" MaxLength="10"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="郵件<br>通知">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgSendMail" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# iif(Eval("SendMail")="Y","True","False") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkSendMail" runat="server" Checked='<%# iif(Eval("SendMail")="Y","True","False") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="預設人員">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefaultUserGroup" runat="server" Text='<%# Eval("DefaultUserGroupNm") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDefaultUserGroup" runat="server" CssClass="DropDownListStyle"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="預設人<br>員參數">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefaultUserGroupEx" runat="server" Text='<%# Eval("DefaultUserGroupEx") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDefaultUserGroupEx" runat="server" CssClass="InputTextStyle_Thin" MaxLength="100" Width="99%" Text='<%# Eval("DefaultUserGroupEx") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="結束<br>流程">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgCloseFlag" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# iif(Eval("CloseFlag")="Y","True","False") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkCloseFlag" runat="server" Checked='<%# iif(Eval("CloseFlag")="Y","True","False") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="同意<br>註記">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center"  />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:Image ID="imgAgreeFlag" runat="server" ImageUrl="~/images/chkbox.gif" Visible='<%# iif(Eval("AgreeFlag")="Y","True","False") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkAgreeFlag" runat="server" Checked='<%# iif(Eval("AgreeFlag")="Y","True","False") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemStyle CssClass="td_detail" Height="15px" HorizontalAlign="center" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbUp" runat="server" Text="Up" Font-Size="12px" ForeColor="blue" CommandName="Up" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                                <asp:LinkButton ID="lbDown" runat="server" Text="Dn" Font-Size="12px" ForeColor="blue" CommandName="Down" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

        </table>
        </form>
</body>
</html>
