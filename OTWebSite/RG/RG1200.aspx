<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RG1200.aspx.vb" Inherits="RG_RG1200" %>

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
            var strMsg;
            strMsg = "RG1200資料上傳-檔案檢核失敗，請下載錯誤紀錄log檔案!";
            alert(Param);
            fromField = document.getElementById("btnUpd1");
            fromField.click();
            //            switch (Param) {
            //                case "":
            //                    alert(strMsg);
            //                    fromField = document.getElementById("btnUpd1");
            //                    fromField.click();
            //            }
        }
       -->
    </script>
</head>

<body style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:0" >
    <form id="frmContent" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true" EnableScriptLocalization="true" ID="ScriptManager1" />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition"  width="95%">
                        <tr>
                            <td class="td_EditHeader" width="25%">
                                <asp:Label ID="lblUploadType" Font-Size="15px" ForeColor="Blue" runat="server" Text="上傳類型"></asp:Label>
                                <uc:Release ID="ucRelease" runat="server" WindowHeight="350" WindowWidth="350" style="display:none" />
                                <asp:Label ID="lblReleaseResult" runat="server" ForeColor="Blue" Text="" style="display:none"></asp:Label>
                            </td>
                            <td class="td_Edit" align="left">
                                <asp:RadioButtonList ID="UploadSelect" Font-Size="15px" runat="server">
                                    <asp:ListItem Value="1">1.員工基本資料</asp:ListItem>
                                    <asp:ListItem Value="2">2.員工年薪主檔資料</asp:ListItem>
                                    <asp:ListItem Value="3">3.員工教育資料</asp:ListItem>
                                    <asp:ListItem Value="4">4.員工前職經歷資料</asp:ListItem>
                                    <asp:ListItem Value="5">5.員工勞退自提比率資料</asp:ListItem>
                                    <asp:ListItem Value="6">6.員工簽核資料</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Button ID = "btnUpd1" runat="server" Text="" Width = "0px" style="display:none;"></asp:Button>
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
                                            <dt><asp:Label ID="dt1" Font-Size="15px" runat="server" Text="(1) 員工基本資料：RG1200_Personal"></asp:Label></dt>
                                            <dt><asp:Label ID="dt2" Font-Size="15px" runat="server" Text="(2) 員工年薪主檔資料：RG1200_Salary"></asp:Label></dt>
                                            <dt><asp:Label ID="dt3" Font-Size="15px" runat="server" Text="(3) 員工教育資料：RG1200_Education"></asp:Label></dt>
                                            <dt><asp:Label ID="dt4" Font-Size="15px" runat="server" Text="(4) 員工前職經歷資料：RG1200_Experience"></asp:Label></dt>
                                            <dt><asp:Label ID="dt5" Font-Size="15px" runat="server" Text="(5) 員工勞退自提比率資料：RG1200_EmpRatio"></asp:Label></dt>
                                            <dt><asp:Label ID="dt6" Font-Size="15px" runat="server" Text="(6) 員工簽核資料：RG1200_EmpFlow"></asp:Label></dt>
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
                                <asp:HiddenField ID="hidFileUploadFileName" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hidLogFileName" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="IsDataExist" runat="server"></asp:HiddenField>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
