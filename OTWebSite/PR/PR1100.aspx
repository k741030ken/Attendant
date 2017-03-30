<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PR1100.aspx.vb" Inherits="PR_PR1100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <style type="text/css">
        #UploadSelect { margin:20px 10px }
        li { margin:20px 0 }
        dt { margin-top:10px } 
    </style>
    <script type="text/javascript">
    <!--
        function IsTODownload(Param) {
            alert(Param);
            fromField = document.getElementById("btnUpd");
            fromField.click();
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0">
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="95%">
                        <tr>
                            <td class="td_EditHeader" width="25%">
                                <asp:Label ID="lblCompID" Font-Size="15px" ForeColor="Blue" runat="server" Text="公司代碼"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                            </td>
                            <td class="td_Edit" align="left">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_EditHeader" width="25%">
                                <asp:Label ID="lblUploadType" Font-Size="15px" ForeColor="Blue" runat="server" Text="上傳類型"></asp:Label>
                                
                            </td>
                            <td class="td_Edit" align="left">
                                <asp:RadioButtonList ID="UploadSelect" Font-Size="15px" runat="server">
                                    <asp:ListItem Value="1">1.整批津貼資料</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Button ID = "btnUpd" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_EditHeader" width="25%">
                                <asp:Label ID="lblUploadRemark" Font-Size="15px" ForeColor="Blue" runat="server" Text="說明"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left">
                                <ol>
                                    <li>
                                        <asp:Label ID="li1" Font-Size="15px" runat="server" Text="檔案格式XLS"></asp:Label>
                                    </li>
                                    <li>
                                        <asp:Label ID="li2" Font-Size="15px" runat="server" Text="請保留標題欄位列"></asp:Label>
                                        <dl>
                                            <dt><asp:Label ID="li3" Font-Size="15px" runat="server" Text="(系統判斷上傳檔案第一列資料為欄位標題)"></asp:Label></dt>
                                        </dl>
                                    </li>
                                    <li>
                                        <asp:Label ID="li4" Font-Size="15px" runat="server" Text="將工作表(WorkSheet)命名為對應的上傳類型代碼"></asp:Label>
                                        <dl>
                                            <dt><asp:Label ID="dt1" Font-Size="15px" runat="server" Text="(1) 整批津貼資料：PR1100_Salary_Allowance"></asp:Label></dt>
                                        </dl>
                                    </li>
                                    <li style="list-style-type:none">
                                        <asp:Button ID="btnDownload" Font-Size="15px" runat="server" Text="下傳『欄位格式說明檔』" />
                                    </li>
                                </ol>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_EditHeader" width="25%">
                                <asp:Label ID="lblUploadPath" Font-Size="15px" ForeColor="Blue" runat="server" Text="*檔案路徑"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left">
                                <asp:FileUpload ID="FileUpload" runat="server" Width="100%" CssClass="InputTextStyle_Thin" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
