<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SC0443.aspx.vb" Inherits="SC_SC0443" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript">
    <!--
        function funAction(Param)
        {
            return true;
        }

        function EntertoSubmit()
        {
            if (window.event.keyCode == 13)
            {
                try
                {
                    window.parent.frames[0].document.getElementById("ucButtonPermission_btnQuery").click();
                }
                catch(ex)
                {}
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
                    <asp:Label ID="lb1" ForeColor="Red" Font-Size="8" runat="server">資料以匯入文件為準，如資料庫中已有相同年份與類別的報表將會進行覆蓋!!</asp:Label>                    
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 30px;">
                    <table width="90%" class="tbl_Edit" cellpadding="1" cellspacing="1">
                        <tr style="height:20px">
                            <td class="td_EditHeader" width="30%" align="center"><asp:Label ID="lblATID" CssClass="MustInputCaption" runat="server" Text="上傳檔案"></asp:Label>
                            </td>
                            <td class="td_Edit" style="width:70%" align="left"><asp:FileUpload ID="myFileUpload" runat="server" CssClass="InputTextStyle_Thin" Width="100%" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </form>
    
</body>
</html>
