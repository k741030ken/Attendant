<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ST2100.aspx.vb" Inherits="ST_ST2100" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <style type="text/css">
        .NoPic
        {
            color: Silver;
            font-size: 50px;
            font-family: Arial;
            font-weight: bolder;
        }
        .PhotoPanel
        {
        	width: 350px;
        	height: 350px;
        	background-color: #F8F8F8;
        	text-align: center;
        	line-height: 350px;
        	vertical-align: middle;
        	position: relative;
        }
        .imgPhoto
        {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            margin: auto;
            max-height: 350px;
            max-width: 350px;
        }
        #imgNewPhoto
        {
        	filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale);    	
        }
        #imgNewPhotoHidden
        {
        	filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=image);
        	visibility: hidden;
        }        
    </style>
    <script type="text/javascript">
    <!--
        function onLoadImage(file) {
            var lblNewPhoto_NoPic = document.getElementById("lblNewPhoto_NoPic");
            var imgNewPhoto = document.getElementById("imgNewPhoto");
            var imgNewPhotoHidden = document.getElementById("imgNewPhotoHidden");

            if (!file.value.toLowerCase().match(/.jpg|.jpeg/)) {
                alert("請上傳jpg檔！");
                lblNewPhoto_NoPic.style.display = "block";
                imgNewPhoto.style.display = "none";
                return false;
            }

            if (imgNewPhoto.filters) {
                file.select();
                var imgSrc = document.selection.createRange().text;
                imgNewPhoto.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                imgNewPhotoHidden.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = imgSrc;
                imgNewPhoto.style.display = "block";

                setTimeout(function () {
                    autoSizePreview(imgNewPhoto, imgNewPhotoHidden.offsetWidth, imgNewPhotoHidden.offsetHeight)
                }, 100);
            }
            lblNewPhoto_NoPic.style.display = "none";
        }

        function autoSizePreview(obj, oriWidth, oriHeight) {
            var zoomParam = clasImgZoomParam(350, 350, oriWidth, oriHeight);
            obj.style.width = zoomParam.width + "px";
            obj.style.height = zoomParam.height + "px";
            obj.style.marginTop = zoomParam.top + "px";
            obj.style.marginLeft = zoomParam.left + "px";
        }

        function clasImgZoomParam(maxWidth, maxHeight, width, height) {
            var param = { width: width, height: height, top: 0, left: 0 };
            if (width > maxWidth || height > maxHeight) {
                rateWidth = width / maxWidth;
                rateHeight = height / maxHeight;
                if (rateWidth > rateHeight) {
                    param.width = maxWidth;
                    param.height = height / rateWidth;
                } else {
                    param.width = width / rateHeight;
                    param.height = maxHeight;
                }
            }
            param.left = (maxWidth - param.width) / 2;
            param.top = (maxHeight - param.height) / 2;
            return param;
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
                <td align="center" style="height: 30px;">
                    <table cellpadding="1" cellspacing="1" border="0" class="tbl_Condition" height="100%" width="100%">
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblCompID" Font-Size="15px" runat="server" Text="公司代碼："></asp:Label>
                            </td>
                            <td align="left" width="70%">
                                <asp:DropDownList ID="ddlCompID" runat="server" AutoPostBack="true" Font-Names="細明體"></asp:DropDownList>
                                <asp:Label ID="lblCompRoleID" Font-Size="15px" runat="server" CssClass="InputTextStyle_Thin" Width="200px"></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                        <tr>
                            <td width="5%"></td>
                            <td align="left" width="15%">
                                <asp:Label ID="lblEmpID" Font-Size="15px" runat="server" Text="員工編號："></asp:Label>
                            </td>
                            <td align="left" width="70%">
                                <asp:TextBox ID="txtEmpID" runat="server" CssClass="InputTextStyle_Thin" AutoPostBack="true" Style="text-transform: uppercase"></asp:TextBox>
                                <uc:ButtonQuerySelectUserID ID="ucSelectEmpID" runat="server" ButtonText="..." ButtonHint="選擇人員..." WindowHeight="550" WindowWidth="500" />
                                <asp:Label ID="txtEmpName" Font-Size="15px" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="10%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <table width="100%" height="100%" cellspacing="15" class="tbl_Content">
                        <tr>
                            <td align="left" colspan="2" width="100%">
                                <asp:FileUpload ID="fuEmpPhotoUrl" CssClass="InputTextStyle_Thin" Width="70%" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="50%">
                                <asp:Label ID="lblOldPhoto" Font-Size="15px" runat="server" Text="原相片"></asp:Label>
                            </td>
                            <td align="left" width="50%">
                                <asp:Label ID="lblNewPhoto" Font-Size="15px" runat="server" Text="預覽更新相片"></asp:Label>
                        </tr>
                        <tr>
                            <td align="left" width="50%">
                                <asp:Panel ID="pnlOldPhoto" runat="server" CssClass="PhotoPanel InputTextStyle_Thin">
                                    <asp:Label ID="lblOldPhoto_NoPic" CssClass="NoPic" runat="server" Text="NoPicture"></asp:Label>
                                    <asp:Image ID="imgOldPhoto" CssClass="imgPhoto" Visible="false" runat="server" />
                                </asp:Panel>
                            </td>
                            <td align="left" width="50%">
                                <asp:Panel ID="pnlNewPhoto" runat="server" CssClass="PhotoPanel InputTextStyle_Thin">
                                    <asp:Label ID="lblNewPhoto_NoPic" CssClass="NoPic" runat="server" Text="NoPicture"></asp:Label>
                                    <asp:Panel ID="imgNewPhoto" runat="server"></asp:Panel>
                                    <asp:Image ID="imgNewPhotoHidden" runat="server" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
