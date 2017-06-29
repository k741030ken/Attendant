<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UtilTest.aspx.cs" Inherits="Util_Test"
    MaintainScrollPositionOnPostback="true" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Devarchive.Net" Namespace="Devarchive.Net" TagPrefix="cc1" %>
<%@ Register Src="ucCommMultiSelect.ascx" TagName="ucCommMultiSelect" TagPrefix="uc1" %>
<%@ Register Src="ucModalPopup.ascx" TagName="ucModalPopup" TagPrefix="uc3" %>
<%@ Register Src="ucCommSingleSelect.ascx" TagName="ucCommSingleSelect" TagPrefix="uc4" %>
<%@ Register Src="ucPageInfo.ascx" TagName="ucPageInfo" TagPrefix="uc2" %>
<%@ Register Src="ucCascadingDropDown.ascx" TagName="ucCascadingDropDown" TagPrefix="uc6" %>
<%@ Register Src="ucCommCascadeSelect.ascx" TagName="ucCommCascadeSelect" TagPrefix="uc5" %>
<%@ Register Src="ucGridView.ascx" TagName="ucGridView" TagPrefix="uc8" %>
<%@ Register Src="ucCommCascadeSelectButton.ascx" TagName="ucCommCascadeSelectButton" TagPrefix="uc9" %>
<%@ Register Src="ucCommMultiSelectButton.ascx" TagName="ucCommMultiSelectButton" TagPrefix="uc10" %>
<%@ Register Src="ucAttachDownloadButton.ascx" TagName="ucAttachDownloadButton" TagPrefix="uc12" %>
<%@ Register Src="ucAttachAdminButton.ascx" TagName="ucAttachAdminButton" TagPrefix="uc11" %>
<%@ Register Src="ucUploadButton.ascx" TagName="ucUploadButton" TagPrefix="uc13" %>
<%@ Register Src="~/Util/ucCommUserAdminButton.ascx" TagPrefix="uc1" TagName="ucCommUserAdminButton" %>

<!DOCTYPE html >
<html>
<head id="Head1" runat="server">
    <title>UtilTest</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true">
        </asp:ToolkitScriptManager>
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">System Tools</legend>
            <asp:Button ID="btnAppClear" CssClass="Util_clsBtn" Width="150" runat="server"   Text="清除 Application 變數" OnClick="btnAppClear_Click" />
            <asp:Button ID="btnSessClear" CssClass="Util_clsBtn" Width="150" runat="server"  Text="清除 Session 變數" OnClick="btnSessClear_Click" />
            <asp:Button ID="btnReStartApp" CssClass="Util_clsBtn" Width="150" runat="server" Text="重新啟動系統" OnClick="btnReStartApp_Click" />
            <br />
            <br />
            Cache Name:<asp:TextBox ID="txtCacheName" runat="server" Text="" Width="150px" />
            <asp:Button ID="btnCleanCache" CssClass="Util_clsBtn" Width="100" runat="server" Text="清除指定快取"
                OnClick="btnCleanCache_Click" />
            <asp:Button ID="btnCleanAllCache" CssClass="Util_clsBtn" Width="100" runat="server" Text="清除全部快取"
                OnClick="btnCleanAllCache_Click" />
            <br />
            <asp:Label ID="labCacheList" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">getAppKey (取得自訂編號)</legend>
            <br />
            KeyDB<asp:TextBox ID="txtAppKeyDB" runat="server" Text="NetSample" Width="80px" />
            KeyID<asp:TextBox ID="txtAppKeyID" runat="server" Text="Test1" Width="80px" />
            KeyQty<asp:TextBox ID="txtAppKeyQty" runat="server" Text="" Width="20px">1</asp:TextBox>
            <asp:Button ID="btnAppKey" CssClass="Util_clsBtn" runat="server" Text="執行" OnClick="btnAppKey_Click" />
            <br />
            <asp:Label ID="labAppKey" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">getLastKeySeqNo (取出指定資料表鍵值最後編號)</legend>DB<asp:TextBox
                ID="txtDetailDB" runat="server" Text="" Width="80px">NetSample</asp:TextBox>
            Table<asp:TextBox ID="txtDetailTable" runat="server" Text="" Width="80px">PODetail</asp:TextBox>
            AllKeyFieldList<asp:TextBox ID="txtDetailKeyFieldList" runat="server" Text="" Width="120px">POID,POSeq</asp:TextBox>
            PartKeyValueList<asp:TextBox ID="txtMasterKeyValueList" runat="server" Text="" Width="120px">201404.002</asp:TextBox>
            <asp:Button ID="btnAppDetailLastSeqNo" CssClass="Util_clsBtn" runat="server" Text="執行" OnClick="btnAppDetailLastSeqNo_Click" />
            <br />
            <asp:Label ID="labAppDetailLastSeqNo" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">ucModalPopup (彈出視窗)</legend>FrameURL=
        <asp:TextBox ID="txtPopupURL" runat="server" Text="" Width="550px"></asp:TextBox>
            <br />
            <asp:Button ID="btnShowPopup" CssClass="Util_clsBtn" runat="server" Width="150" Text="彈出FrameURL" OnClick="btnShowPopup_Click" />
            <asp:Button ID="btnShowPopupRefresh" CssClass="Util_clsBtn" runat="server" Width="250" Text="彈出FrameURL並重新整理子視窗" OnClick="btnShowPopupRefresh_Click" />
            <br />
            <hr class="Util_clsHR" />
            <br />
            Content =<asp:TextBox ID="txtPopupContent" runat="server" Text="" Width="550px"></asp:TextBox>
            <br />
            <asp:Button ID="btnShowPopupContent" CssClass="Util_clsBtn" runat="server" Width="150" Text="彈出Content" OnClick="btnShowPopupContent_Click" />
            <br />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">MsgBox / Confirm / Notify (加強JS效果)</legend>Msg
        <asp:TextBox ID="txtMsg" Text="測試內容" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnMsgBox" CssClass="Util_clsBtn" runat="server" Text="MsgBox" OnClick="btnMsgBox_Click" />
            <asp:Button ID="btnConfirm" CssClass="Util_clsBtn" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
            <asp:Button ID="btnNotify1" CssClass="Util_clsBtn" runat="server" Text="Notify 1" OnClick="btnNotify1_Click" />
            <asp:Button ID="btnNotify2" CssClass="Util_clsBtn" runat="server" Text="Notify 2" OnClick="btnNotify2_Click" />
            <asp:Button ID="btnNotify3" CssClass="Util_clsBtn" runat="server" Text="Notify 3" OnClick="btnNotify3_Click" />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">Upload</legend>
            <uc13:ucUploadButton ID="ucUploadButton1" runat="server" />
            &nbsp;&nbsp;<uc13:ucUploadButton ID="ucUploadButton2" runat="server" ucIsPopNewWindow="true" />
            <asp:GridView ID="gvUpload" runat="server" Visible="False" CellPadding="4" ForeColor="#333333"
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <hr />
            範例：上傳 Excel 後即時轉成 DataTable，並套用到 GridView現
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">Attach (附件管理)</legend>
            <br />
            AttachDB
        <asp:TextBox ID="txtAttachDB" Text="NetSample" runat="server"></asp:TextBox><br />
            AttachID
        <asp:TextBox ID="txtAttachID" Text="a" runat="server"></asp:TextBox><br />
            <uc12:ucAttachDownloadButton ID="ucAttachDownloadButton1" runat="server" />
            <uc12:ucAttachDownloadButton ID="ucAttachDownloadButton2" runat="server" />
            <br />
            AttachFileMaxQty
        <asp:TextBox ID="txtAttachFileMaxQty" Text="" runat="server"></asp:TextBox>(預設：1)<br />
            AttachFileMaxKB
        <asp:TextBox ID="txtAttachFileMaxKB" Text="" runat="server"></asp:TextBox>(預設：1024)<br />
            AttachFileTotKB
        <asp:TextBox ID="txtAttachFileTotKB" Text="" runat="server"></asp:TextBox><br />
            AnonymousYN
        <asp:DropDownList ID="ddlAnonymousYN" runat="server">
            <asp:ListItem>Y</asp:ListItem>
            <asp:ListItem Selected="True">N</asp:ListItem>
        </asp:DropDownList>
            (預設：N)<br />
            AttachFileExtList
        <asp:TextBox ID="txtAttachFileExtList" Text="" runat="server"></asp:TextBox>(例:pdf,doc)<br />
            <uc11:ucAttachAdminButton ID="ucAttachAdminButton1" runat="server" />
            <uc11:ucAttachAdminButton ID="ucAttachAdminButton2" runat="server" />
            <br />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">ucCommSingleSelect</legend>
            <uc4:ucCommSingleSelect ID="ucCommSingleSelect1" runat="server" />
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">ucCommMultiSelect</legend>
            <uc10:ucCommMultiSelectButton ID="ucCommMultiSelectButton1" runat="server" ucBtnCaption="彈出[複選清單](ucButton)"
                ucBtnWidth="-1" />
            <br />
            傳回值
        <asp:TextBox ID="txtPopupID1" runat="server" Text="" Width="300px"></asp:TextBox>
            <asp:TextBox ID="txtPopupInfo1" runat="server" Text="" Width="500px"></asp:TextBox>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">ucCommCascadeSelect</legend>
            <uc9:ucCommCascadeSelectButton ID="ucCommCascadeSelectButton1" runat="server" ucBtnCaption="彈出[關聯選單](ucButton)"
                ucBtnWidth="-1" />
            <br />
            是否選人
        <asp:DropDownList ID="ddlIsSeleUser1" runat="server">
            <asp:ListItem Selected="True">Y</asp:ListItem>
            <asp:ListItem>N</asp:ListItem>
        </asp:DropDownList>
            人員多選
        <asp:DropDownList ID="ddlIsMultiSele1" runat="server">
            <asp:ListItem Selected="True">Y</asp:ListItem>
            <asp:ListItem>N</asp:ListItem>
        </asp:DropDownList>
            <br />
            預設值:<br />
            <asp:TextBox ID="txtDefComp1" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDefDept1" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDefUser1" runat="server" Text="" Width="150px"></asp:TextBox>
            <br />
            傳回值:
        <br />
            <asp:TextBox ID="txtComp1" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtCompInfo1" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtDept1" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDeptInfo1" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtUser1" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtUserInfo1" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <hr class="Util_clsHR" />
            <asp:Button ID="btnPopupCascade" CssClass="Util_clsBtn" runat="server" Width="170" Text="彈出[關聯選單](Panel)" OnClick="btnPopupCascade_Click" />
            <br />
            是否選人
        <asp:DropDownList ID="ddlIsSeleUser2" runat="server">
            <asp:ListItem Selected="True">Y</asp:ListItem>
            <asp:ListItem>N</asp:ListItem>
        </asp:DropDownList>
            人員多選
        <asp:DropDownList ID="ddlIsMultiSele2" runat="server">
            <asp:ListItem Selected="True">Y</asp:ListItem>
            <asp:ListItem>N</asp:ListItem>
        </asp:DropDownList>
            <br />
            預設值:<br />
            <asp:TextBox ID="txtDefComp2" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDefDept2" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDefUser2" runat="server" Text="" Width="150px"></asp:TextBox>
            <br />
            傳回值:<br />
            <asp:TextBox ID="txtComp2" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtCompInfo2" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtDept2" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtDeptInfo2" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtUser2" runat="server" Text="" Width="150px"></asp:TextBox>
            <asp:TextBox ID="txtUserInfo2" runat="server" Text="" Width="450px"></asp:TextBox>
            <br />
            <hr class="Util_clsHR" />
            <uc1:ucCommUserAdminButton runat="server" ID="ucCommUserAdminButton1" ucBtnWidth="150" ucBtnCaption="常用人員(對話框)" />
            <uc1:ucCommUserAdminButton runat="server" ID="ucCommUserAdminButton2" ucBtnWidth="150" ucBtnCaption="常用人員(新視窗)" ucIsPopNewWindow="true" />
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>Ajax關聯式下拉選單(最多五層，後端使用 WCF 技術)</legend>
            <uc6:ucCascadingDropDown ID="ucCascadingDropDown1" runat="server" />
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>getDomainNameFromURL()</legend>URL<asp:TextBox ID="txtDomaimURL"
                runat="server" Text="" Width="300px"></asp:TextBox>
            <asp:Button ID="btnDomain" CssClass="Util_clsBtn" runat="server" Width="120" Text="取出主機名" OnClick="btnDomain_Click" /><br />
            <asp:Label ID="labDomain" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsHostAlive()</legend>Host Name<asp:TextBox ID="txtHost01"
                runat="server" Text="" Width="300px"></asp:TextBox>(也可為IP) 逾時<asp:TextBox ID="txtHost02"
                    runat="server" Text="" Width="100px"></asp:TextBox>(預設為1000毫秒)
        <asp:Button ID="btnHost" CssClass="Util_clsBtn" runat="server" Width="150" Text="偵測主機狀態(Ping)" OnClick="btnHost_Click" /><br />
            <asp:Label ID="labHost" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>getStringJoin()</legend>陣列內容<asp:TextBox ID="txtStringJoin1"
                runat="server" Text="" Width="300px"></asp:TextBox>
            元素連接字串<asp:TextBox ID="txtStringJoin2" runat="server" Text="," Width="20px"></asp:TextBox>
            <asp:Button CssClass="Util_clsBtn" ID="btnStringJoin" runat="server" Width="250" Text="將陣列轉成字串(使用StringBuilder)" OnClick="btnStringJoin_Click" /><br />
            <asp:Label ID="labStringJoin" runat="server" Text=""></asp:Label>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>編解碼相關</legend>Cust64Seed：<asp:TextBox ID="txtCust64Seed"
                runat="server" Text="" Width="550px"></asp:TextBox><br />
            欲處理的字串：<asp:TextBox ID="txtCustContent" runat="server" Text="" Width="450px"></asp:TextBox><br />
            <asp:Button ID="btnCust64Encode" CssClass="Util_clsBtn" Width="120" runat="server" Text="Cust64Encode" OnClick="btnCust64Encode_Click" />
            <asp:Button ID="btnCust64Decode" CssClass="Util_clsBtn" Width="120" runat="server" Text="Cust64Decode" OnClick="btnCust64Decode_Click" />
            <asp:Button ID="btnMD5Hash" CssClass="Util_clsBtn" Width="120" runat="server" Text="MD5Hash" OnClick="btnMD5Hash_Click" /><br />
            <asp:Literal ID="labCustMsg" runat="server"></asp:Literal>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>getHtmlMessage()</legend>類別<asp:DropDownList ID="ddlHtmlMsg"
                runat="server">
            </asp:DropDownList>
            訊息內容<asp:TextBox ID="txtHtmlMsg" runat="server" Text="" Width="300px"></asp:TextBox>
            <asp:Button ID="btnHtmlMsg" CssClass="Util_clsBtn" runat="server" Text="顯示" OnClick="btnHtmlMsg_Click" /><br />
            <asp:Literal ID="labHtmlMsg" runat="server"></asp:Literal>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>ExportCalendar()</legend>
            <table border="0">
                <tr>
                    <td>開始日期<asp:Calendar ID="dtStart" runat="server"></asp:Calendar>
                    </td>
                    <td>結束日期<asp:Calendar ID="dtEnd" runat="server"></asp:Calendar>
                    </td>
                    <td>主旨<asp:TextBox ID="txtCalSubject" runat="server" Text="" Width="300px"></asp:TextBox>
                        <br />
                        <br />
                        地點<asp:TextBox ID="txtCalLocation" runat="server" Text="" Width="300px"></asp:TextBox>
                        <br />
                        <br />
                        內容<asp:TextBox ID="txtCalBody" runat="server" Text="" Width="450px" TextMode="MultiLine"
                            Height="80px"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btnCalendar" CssClass="Util_clsBtn" Width="120" runat="server" Text="產生行事曆(2007)" OnClick="btnCalendar_Click" />
                        <asp:Button ID="btnCalender2003" CssClass="Util_clsBtn" Width="120" runat="server" Text="產生行事曆(2003)" OnClick="btnCalender2003_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsSendMail()</legend>寄件者*<asp:TextBox ID="txtMailFrom"
                runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01 < aa@xx.sinopac.com
        >)<br />
            收件者*<asp:TextBox ID="txtMailTo" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            主 旨*<asp:TextBox ID="txtMailSubject" runat="server" Text="" Width="500px"></asp:TextBox><br />
            本 文<asp:TextBox ID="txtMailBody" runat="server" Text="" TextMode="MultiLine" Height="100px"
                Width="600px"></asp:TextBox><br />
            附 件<asp:TextBox ID="txtMailAttach" runat="server" Text="" Width="300px"></asp:TextBox>(格式：/temp/aa.zip,/temp/bb.doc)<br />
            副 本<asp:TextBox ID="txtMailCC" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            密 件<asp:TextBox ID="txtMailBCC" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            <asp:Button ID="btnMail" CssClass="Util_clsBtn" runat="server" Text="Send" OnClick="btnMail_Click" /><br />
            <asp:Literal ID="labMailMsg" runat="server"></asp:Literal>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>IsSendMail()</legend>寄件者*<asp:TextBox ID="txtMailFrom2"
                runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01 < aa@xx.sinopac.com
        >)<br />
            收件者*<asp:TextBox ID="txtMailTo2" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            主 旨*<asp:TextBox ID="txtMailSubject2" runat="server" Text="" Width="500px"></asp:TextBox><br />
            本 文<asp:TextBox ID="txtMailBody2" runat="server" Text="" TextMode="MultiLine" Height="100px"
                Width="600px"></asp:TextBox><br />
            附 件<asp:TextBox ID="txtMailAttach2" runat="server" Text="" Width="600px"></asp:TextBox>(JSON格式，欄位依序是：AttachDB,AttachID,SeqNo)<br />
            副 本<asp:TextBox ID="txtMailCC2" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            密 件<asp:TextBox ID="txtMailBCC2" runat="server" Text="" Width="400px"></asp:TextBox>(格式：User01
        < aa@xx.sinopac.com > ,User02 < bb@xx.sinopac.com >)<br />
            <asp:Button ID="btnMail2" CssClass="Util_clsBtn" runat="server" Text="Send" OnClick="btnMail2_Click" /><br />
            <asp:Literal ID="labMailMsg2" runat="server"></asp:Literal>
        </fieldset>
        <br />
        <br />
        <fieldset class='Util_Fieldset'>
            <legend class='Util_Legend'>setJS_TinyMCE()</legend>預設
        <asp:TextBox ID="txtMCE" runat="server" TextMode="MultiLine"></asp:TextBox>
            <p>
                <asp:Button ID="btnMCE" CssClass="Util_clsBtn" runat="server" Text="觀看結果" OnClick="btnMCE_Click" />
                <asp:Button ID="btnMCEClear" CssClass="Util_clsBtn" runat="server" Text="清除結果" />
            </p>
            <asp:TextBox ID="txtMCEResult" runat="server" TextMode="MultiLine" Rows="5" Width="600"></asp:TextBox>
            <hr />
            自訂
        <asp:TextBox ID="txtCustMCE" runat="server" TextMode="MultiLine"></asp:TextBox>
            <p>
                <asp:Button ID="btnCustMCE" CssClass="Util_clsBtn" runat="server" Text="觀看結果" OnClick="btnCustMCE_Click" />
                <asp:Button ID="btnCustMCEClear" CssClass="Util_clsBtn" runat="server" Text="清除結果" />
            </p>
            <asp:TextBox ID="txtCustMCEResult" runat="server" TextMode="MultiLine" Rows="5" Width="500"></asp:TextBox>
        </fieldset>
        <br />
        <fieldset class="Util_Fieldset">
            <legend class="Util_Legend">ucPageInfo</legend>
            <uc2:ucPageInfo ID="ucPageInfo1" runat="server" />
        </fieldset>
        <uc3:ucModalPopup ID="ucModalPopup1" runat="server" />
        <%--隱藏區塊，彈出視窗時才顯示--%>
        <div style="display: none;">
            <asp:Panel ID="pnlCascading1" runat="server">
                <uc5:ucCommCascadeSelect ID="ucCommCascadeSelect1" runat="server" />
            </asp:Panel>
        </div>
    </form>
</body>
</html>
