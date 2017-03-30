<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA090.aspx.vb" Inherits="WF_WFA090" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param) {
        }

        function EntertoSubmit() {
            if (window.event.keyCode == 13) {
                try {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch (ex)
                { }
            }
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="80%">
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label2" runat="server" Text="類別"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlFlowType" AutoPostBack="true" runat="server" CssClass="DropDownListStyle" Width="250px">
                                    <asp:ListItem Text="授信结案-S999" Value="S999" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="徵信結案-CC99" Value="CC99"></asp:ListItem>
                                    <asp:ListItem Text="徵信結案-CC98" Value="CC98"></asp:ListItem>
                                    <asp:ListItem Text="覆审结案-RV99" Value="RV99"></asp:ListItem>
                                    <asp:ListItem Text="覆审销案-RV98" Value="RV98"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="lblInDept" runat="server" Text="部門"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlOrgan" AutoPostBack="true" runat="server" CssClass="DropDownListStyle" Width="250px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="lblInAO" runat="server" text="AO"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlAO" runat="server" CssClass="DropDownListStyle" Width="250px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label9" runat="server" Text="案件編號"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlAppID" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="=" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtAppID" runat="server" CssClass="InputTextStyle_Thin" Width="100px" style="text-transform:uppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label1" runat="server" Text="客戶統編"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlCustomerID" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="="></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like" Selected="True"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtCustomerID" runat="server" CssClass="InputTextStyle_Thin" Width="100px" MaxLength="10" style="text-transform:uppercase"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="30%" class="td_QueryHeader">
                                <asp:Label ID="Label5" runat="server" Text="公司名稱"></asp:Label>
                            </td>
                            <td align="left" width="70%" class="td_Query">
                                <asp:DropDownList ID="ddlCName" runat="server" CssClass="DropDownListStyle">
                                    <asp:ListItem Text="等於" Value="="></asp:ListItem>
                                    <asp:ListItem Text="相似" Value="like" Selected="True"></asp:ListItem>
                                </asp:DropDownList><asp:TextBox ID="txtCName" runat="server" CssClass="InputTextStyle_Thin" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="1" cellspacing="1" border="0" height="100%" width="80%">
                        <tr>
                            <td style="color:Red; font-size:12px">PS.笔数最多仅显示200笔，若数据量过大，请缩小条件。</td>
                        </tr>
                    </table> 
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="80%">
                        <tr>
                            <td>
                                <asp:GridView ID="gvMain" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="False"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="AppID,CustomerID,CName,OfficerNm" Width="100%" HeaderStyle-ForeColor="Dimgray" RowStyle-Height="18px" Visible="false">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="18px" />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbReturn" runat="Server" Text="選取" CommandName="Edit" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申贷編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="13%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppID" runat="server" Text='<%# Eval("AppID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerID" HeaderText="客戶統編" ReadOnly="True">
                                            <HeaderStyle Width="11%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CName" HeaderText="客戶名稱" ReadOnly="True">
                                            <HeaderStyle Width="22%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AppTypeNm" HeaderText="案件類別" ReadOnly="True">
                                            <HeaderStyle Width="13%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApproveDate" HeaderText="核准日期" ReadOnly="True">
                                            <HeaderStyle Width="9%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OrganName" HeaderText="受理单位" ReadOnly="True">
                                            <HeaderStyle Width="12%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OfficerNm" HeaderText="負責AO" ReadOnly="True">
                                            <HeaderStyle Width="14%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                    <HeaderStyle ForeColor="DimGray" />
                                </asp:GridView>
                                <asp:GridView ID="gvMain_CC99" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="False"  AutoGenerateColumns="False" CellPadding="2" DataKeyNames="AppID,CustomerID,CName,OfficerNm,CCID" Width="100%" HeaderStyle-ForeColor="Dimgray" RowStyle-Height="18px" Visible="False">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="18px" />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbReturn" runat="Server" Text="選取" CommandName="Edit" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="征信报告編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCCID" runat="server" Text='<%# Eval("CCID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerID" HeaderText="客戶統編" ReadOnly="True">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CName" HeaderText="客戶名稱" ReadOnly="True">
                                            <HeaderStyle Width="30%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OfficerNm" HeaderText="負責AO" ReadOnly="True">
                                            <HeaderStyle Width="15%" CssClass="td_header" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="最後異動日期">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="19%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastChgDate" runat="server" Text='<%# Eval("LastChgDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                    <HeaderStyle ForeColor="DimGray" />
                                </asp:GridView>
                                <asp:GridView ID="gvMain_CR" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="False" 
                                		AutoGenerateColumns="False" CellPadding="2" 
                                		DataKeyNames="CRID, AppID, CustomerID, CName, OfficerNm"
                                		Width="100%" HeaderStyle-ForeColor="Dimgray" RowStyle-Height="18px" 
                                		Visible="False">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="18px" />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbReturn" runat="Server" Text="選取" CommandName="Edit" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="覆审报告編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCRID" runat="server" Text='<%# Eval("CRID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申贷編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppID" runat="server" Text='<%# Eval("AppID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerID" HeaderText="客戶統編" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="12%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CName" HeaderText="客戶名稱" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="25%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OfficerNm" HeaderText="負責AO" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="15%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="最後異動日期">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="18%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastChgDate" runat="server" Text='<%# Eval("LastChgDate", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                    <HeaderStyle ForeColor="DimGray" />
                                </asp:GridView>
                                <asp:GridView ID="gvMain_RA" CssClass="GridViewStyle" runat="server" AllowPaging="False" AllowSorting="False" 
                                		AutoGenerateColumns="False" CellPadding="2" 
                                		DataKeyNames="RAID, AppID, CustomerID, CName, OfficerNm"
                                		Width="100%" HeaderStyle-ForeColor="Dimgray" RowStyle-Height="18px" 
                                		Visible="False">
                                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle CssClass="td_detail" Height="18px" />
                                            <HeaderStyle CssClass="td_header" Width="6%" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbReturn" runat="Server" Text="選取" CommandName="Edit" onmouseover="this.style.color='red';" onmouseout="this.style.color='blue';"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="利费率編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRAID" runat="server" Text='<%# Eval("RAID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申贷編號">
                                            <ItemStyle CssClass="td_detail" />
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppID" runat="server" Text='<%# Eval("AppID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CustomerID" HeaderText="客戶統編" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="10%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CName" HeaderText="客戶名稱" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="25%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OfficerNm" HeaderText="負責AO" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="14%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RATypeNm" HeaderText="申请類別" ReadOnly="True">
                                            <HeaderStyle CssClass="td_header" Width="30%" />
                                            <ItemStyle CssClass="td_detail" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！"></asp:Label>
                                    </EmptyDataTemplate>
                                    <RowStyle CssClass="tr_evenline" />
                                    <AlternatingRowStyle CssClass="tr_oddline" />
                                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                                    <PagerStyle CssClass="GridView_PagerStyle" />
                                    <PagerSettings Position="Top" />
                                    <HeaderStyle ForeColor="DimGray" />
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


