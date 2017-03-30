<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA020.aspx.vb" Inherits="WF_WFA020" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>未命名頁面</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript">
    <!--
        
        function WFredirectPage(Path)
        {
            window.parent.parent.frames[1].location='WFA010.aspx';
            window.parent.parent.frames[2].imgSplit_WorkFlow.src = "../images/mopen.png";
            window.parent.parent.frames[2].imgSplit_WorkFlow.alt = "Close WorkFlow menu";
            window.parent.parent.frmSet.cols = "0,280,20,*";
            window.parent.parent.frames[2].imgSplit_WorkFlow.style.display = "";
            showSubmitting();        
            if (Path != '') {
               window.parent.parent.frames[3].location = '../WF/WFA021.aspx?Path=' + Path;
           }
            //checkStatus();
            window.setTimeout("checkStatus()", 500);
        }

        /*
            InfoPath : 訊息來源
            MssageType : MESSAGE / ERROR
            Message : 訊息
            ButtonTitle : 按鈕上的字眼, Default為 "返回"
            NextWebPage : 按鍵按鈕後所導向的頁面
        */
        function GetMessagePage(InforPath, MessageType, Message, ButtonTitle, NextWebPage) {
            RedirectPage('../WF/WFA021.aspx?InforPath=' + InforPath + 
                                                       '&MessageType=' + MessageType + 
                                                       '&Message=' + Message +
                                                       '&ButtonTitle=' + ButtonTitle + 
                                                       '&NextWebPage=' + NextWebPage);
        }
        
        function openWFMenu()
        {
            window.parent.parent.frames[2].imgSplit_WorkFlow.src = "../images/mopen.png";
            window.parent.parent.frames[2].imgSplit_WorkFlow.alt = "Close WorkFlow menu";
            window.parent.parent.frmSet.cols = "0,280,20,*";
            window.parent.parent.frames[2].imgSplit_WorkFlow.style.display = "";
        }
        
        function openWindow(url){
           window.open(url,"FlowDetail", 'height=600px,width=850px,top=20px,left=50px,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no, status=no')
        }
        
        var objBtn = null;
        function StepCheck(obj, RequireOpinion, ActionName, CheckList, checkCode) {
            if (CheckList != "") {
                alert(CheckList);
                return false;
            }
            var errflag = "1"
            var objTR = obj.parentElement.parentElement;
            var objTable = document.getElementById("dlNextStep");

            var objLabFinalOpin = objTR.cells[objTR.cells.length - 1].children[2];
            if (objLabFinalOpin.value != '') {
                obj.form.TxtAreaOption.value = objLabFinalOpin.value;
            }

            var AssignUser = '';
            for (i = 0; i < objTable.rows.length; i++) {
                var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                if (objInput.tagName.toUpperCase() == "INPUT") {
                    objInput.disabled = true;
                }
            }

            if (RequireOpinion == "Y") {
                if (obj.form.TxtAreaOption.value == "") {
                    alert("動作[ " + ActionName + " ]意見必需輸入!");
                    errflag = "2"

                } else {
                    var objInput = objTR.cells[objTR.cells.length - 2].children[0]
                    switch (objInput.tagName.toUpperCase()) {
                        case "INPUT":           //for textbox
                            AssignUser = objInput.value;
                            if (objInput.value == "") {
                                alert("未有下關處理人員!");
                                errflag = "2"
                            }
                            break;
                        case "SELECT":          //for dropdownlist
                            AssignUser = objInput.options[objInput.selectedIndex].value;
                            if (objInput.options[objInput.selectedIndex].value == "" || objInput.options[objInput.selectedIndex].value == "---請選擇---") {
                                alert("未選擇處理人員!");
                                errflag = "2"
                            }
                            break;
                    }
                }
            } else {
                if (obj.form.TxtAreaOption.value == '') {
                    obj.form.TxtAreaOption.value = "無意見";
                }
                var objInput = objTR.cells[objTR.cells.length - 2].children[0]
                switch (objInput.tagName.toUpperCase()) {
                    case "INPUT":           //for textbox
                        AssignUser = objInput.value;
                        if (objInput.value == "") {
                            alert("未有下關處理人員!");
                            errflag = "2"
                        }
                        break;
                    case "SELECT":          //for dropdownlist
                        AssignUser = objInput.options[objInput.selectedIndex].value;
                        if (objInput.options[objInput.selectedIndex].value == "" || objInput.options[objInput.selectedIndex].value == "---請選擇---") {
                            alert("未選擇處理人員!");
                            errflag = "2"
                        }
                        break;
                }
            }

            if (errflag == "2") {
                for (i = 0; i < objTable.rows.length; i++) {
                    var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                    if (objInput.tagName.toUpperCase() == "INPUT") {
                        objInput.disabled = false;
                    }
                }
                return false;
            } else {
                objBtn = obj;
                var ComfirmMsgStr = '';
                ComfirmMsgStr += '\r執行動作：' + objBtn.value;
                ComfirmMsgStr += '\r關卡：' + objTR.cells[1].children[1].innerText;
                ComfirmMsgStr += '\r指派：' + AssignUser;
                if (obj.form.TxtAreaOption.value.toString().length > 300)
                    ComfirmMsgStr += '\r意見內容：' + obj.form.TxtAreaOption.value.toString().substring(0, 300) + '......';
                else
                    ComfirmMsgStr += '\r意見內容：' + obj.form.TxtAreaOption.value;

                if (confirm(ComfirmMsgStr)) {
                    var objLabUrl = objTR.cells[objTR.cells.length - 1].children[0];
                    if (objLabUrl.value != '') {
                        alert("執行動作：[ " + objBtn.value + " ]有必需填寫事項!");

                        if (objLabUrl.value != '') {
                            var strRtn = ShowDialogWithHeader2(objLabUrl.value, '1000px', '700px');
                            if (strRtn == 'N') obj.form.UrlCheckFlag.value = 'N';
                        }
                        else
                            obj.form.UrlCheckFlag.value = 'N';

                        if (obj.form.UrlCheckFlag.value == 'Y') {
                            alert("送出之相關訊息,執行動作：[ " + objBtn.value + " ]未完成!");
                            
                            for (i = 0; i < objTable.rows.length; i++) {
                                var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                                if (objInput.tagName.toUpperCase() == "INPUT") {
                                    objInput.disabled = false;
                                }
                            }
                            return false;
                        } else {
                            for (i = 0; i < objTable.rows.length; i++) {
                                WFStepCloseMenu();
                                var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                                if (objInput.tagName.toUpperCase() == "INPUT") {
                                    objInput.disabled = false;
                                    objInput.style.display = "none";
                                }
                            }
                            return true;
                        }
                    } else {
                        for (i = 0; i < objTable.rows.length; i++) {
                            WFStepCloseMenu();
                            var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                            if (objInput.tagName.toUpperCase() == "INPUT") {
                                objInput.disabled = false;
                                objInput.style.display = "none";
                            }
                        }
                        return true;
                    }
                } else {
                    for (i = 0; i < objTable.rows.length; i++) {
                        var objInput = objTable.rows[i].cells[0].children[0].rows[0].cells[0].children[0];
                        if (objInput.tagName.toUpperCase() == "INPUT") {
                            objInput.disabled = false;
                        }
                    }
                    return false;
                }
            }
        }
        
       
        function WFStepCloseMenu()
        {
            window.parent.parent.frames[2].imgSplit_WorkFlow.src = "";
            window.parent.parent.frames[2].imgSplit_WorkFlow.alt = "";
            window.parent.parent.frmSet.cols = "280,0,20,*";
            window.parent.parent.frames[2].imgSplit_WorkFlow.style.display = "none";
        }
       
        function checkStatus()
        {
            var msg;
			
			try
			{
				var obj = window.parent.frames[1].document.getElementById('__ActionParam');
				msg = obj.value;
				hidePopupWindow();
			}
			catch(ex)
			{
				window.setTimeout("checkStatus()", 100);
			}
        }
        
        function setLabOpin()
        {
          	if (document.getElementById("DivOpinLab").style.display =="none")
	          {	
	             document.getElementById("DivOpinLab").style.display="";	
	             document.getElementById("LabsetOpin").innerText=" － ";	      
	          }
	        else	
	          {	
	             document.getElementById("DivOpinLab").style.display="none";
	             document.getElementById("LabsetOpin").innerText=" ＋ ";	
	          }
         } 

        function funShowFlowDetail()
        {
            frmContent.btnShowFlowDetail.click();
        }
    -->
    </script>
</head>
<body style="margin-top:10px; margin-left:10">
    <form id="frmContent" runat="server" >
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
      <tr>
         <td  style="height: 7px">
         </td>
      </tr>
      <tr >
         <td >
            <fieldset class="Fieldset" style="border:0">
                <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" PageSize="15" CssClass="GridViewStyle" meta:resourcekey="gvMainResource2" >
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <Columns>
                        <asp:BoundField DataField="KeyID" HeaderText="申請編號" ReadOnly="True" SortExpression="KeyID" meta:resourcekey="BoundFieldResource3" >
                            <HeaderStyle Width="13%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CustomerName" HeaderText="客戶名稱" ReadOnly="True" SortExpression="CustomerName" meta:resourcekey="BoundFieldResource5" >
                            <HeaderStyle Width="27%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Note" HeaderText="摘要" ReadOnly="True" SortExpression="Note" meta:resourcekey="BoundFieldResource6" >
                            <HeaderStyle Width="22%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" Font-Bold="true" ForeColor="Red" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrUserName" HeaderText="申請者" ReadOnly="True" SortExpression="CrUserName" meta:resourcekey="BoundFieldResource6" >
                            <HeaderStyle Width="12%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CrBrName" HeaderText="申請單位" ReadOnly="True" SortExpression="CrUserName" meta:resourcekey="BoundFieldResource6" >
                            <HeaderStyle Width="14%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Crdate" HeaderText="申請日期" ReadOnly="True" SortExpression="Crdate" meta:resourcekey="BoundFieldResource6" >
                            <HeaderStyle Width="10%" CssClass="td_header" Font-Size="12px" />
                            <ItemStyle CssClass="td_detail" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="GridView_EmptyRowStyle" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lblNoData" runat="server" Text="無資料顯示！" meta:resourcekey="lblNoDataResource1"></asp:Label>
                    </EmptyDataTemplate>
                    <RowStyle CssClass="tr_evenline" />
                    <AlternatingRowStyle CssClass="tr_oddline" />
                    <SelectedRowStyle CssClass="GridView_SelectedRowStyle" BackColor="Moccasin" />
                    <PagerStyle CssClass="GridView_PagerStyle" />
                    <PagerSettings Position="Top" />
                </asp:GridView>
            </fieldset>
        </td>
      </tr>
      <tr>
          <td>
             <fieldset class="Fieldset"><legend id="legendOpt" class="Legend"><font color='dimgray'>上一關意見</font>(<asp:Label ID="LabShowFlowLog" runat="server" Text="Label"></asp:Label>)</legend>
                 <table border="0" cellpadding="0" cellspacing="1"  style="width: 100%;" id="TABLE1">
                   <tr>
                      <td align="left" colspan='7' style="height: 7px">
                      </td>
                   </tr>   
                   <tr>
                      <td align="center" valign="middle"  style="width: 10%; height: 21px" Class="td_EditHeader">
                          <asp:Label ID="Label2" runat="server" Text="關卡" Width ="99%" BorderColor ="White"></asp:Label>
                      </td>
                      <td align="left" width="20%"  style="height: 21px" Class="td_detail">
                           <asp:Label ID="LabPreStepDesc" runat="server" Text=""  Width ="99%" BorderColor ="White"></asp:Label> 
                      </td>
                      <td align="center"  style="width: 10%; height: 21px" Class="td_EditHeader">
                          <asp:Label ID="Label1" runat="server" Text="動作"  Width ="99%" BorderColor ="White"></asp:Label>
                      </td>
                      <td align="left" width="15%" style="height: 21px" Class="td_detail">
                           <asp:Label ID="LabPreStepAction" runat="server" Text=""  Width ="99%" BorderColor ="White"></asp:Label> 
                      </td>
                      <td align="center" width="10%"  style="height: 21px" Class="td_EditHeader">
                         <asp:Label ID="Label3" runat="server" Text="執行人"  Width ="99%" BorderColor ="White"></asp:Label>
                      </td>
                      <td align="left" style="height: 21px; width: 20%;" Class="td_detail">
                         <asp:Label ID="LabPreStepTime" runat="server" Text=""  Width ="99%" BorderColor ="White"></asp:Label> 
                      </td>
                      <td align="left"  style="height: 21px; width: 15%;" Class="td_detail">
                         <asp:Label ID="LabPreStepActUser" runat="server" Text=""  Width ="99%" BorderColor ="White"></asp:Label> 
                      </td>
                   </tr>   
                   <tr>
                      <td align="center" style="width: 8%; height: 21px" Class="td_EditHeader">
                          <asp:Label ID="Label9" runat="server"  Text="意見" Width ="99%" BorderColor ="White"></asp:Label>
                      </td>
                      <td align="left"  style="width: 10%; height: 21px" colspan ="6" Class="td_detail">
                           <asp:Label ID="LabPreStepOpin" runat="server"  CssClass="td_detail" Width ="99%" BorderColor ="White"></asp:Label>
                      </td>
                   </tr>    
                 </table>      
             </fieldset>
          </td>
      </tr>
      <tr>
         <td>
             <div id="DivOpin" style="width:100%" runat="server">
             <fieldset class = "Fieldset" ><legend id="legend1" class="Legend"><A href=# onclick="javascript:setLabOpin()"><asp:Label ID="LabsetOpin" runat="server" Text=" － "></asp:Label></A><font color='dimgray'> 操作提示 </font></legend>
                       <div id="DivOpinLab" style="width:100%;position:relative">
                            <asp:Label ID="LabOpin" runat="server" CssClass="td_detail" BorderColor ="White"></asp:Label>
                       </div>
             </fieldset>  
             </div>
         </td>
      </tr>
      <tr>
         <td>
             <div id="divAbnormal" style="width:100%; display:none" runat="server">
             <fieldset class = "Fieldset" ><legend id="legend3" class="Legend"><font color='red'>「徵信異常案件，業務單位承作原因」      *請依授信案件分層授權準則提高一層級送審*</font></legend>
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAbnormal" runat="server" TextMode="multiLine" Height="60px" CssClass="InputTextStyle_Thin" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
             </fieldset>  
             </div>
         </td>
      </tr>
      <tr>
         <td>
             <fieldset class = "Fieldset"><legend id="legend2" class="Legend"><font color='dimgray'><asp:Label ID="lab_FLowStepDesc" runat="server" ></asp:Label><asp:Label ID="LabFlowCaseDesc" runat="server" ></asp:Label></font></legend>
                  <table style="width:100%">
                      <tr>
                          <td align="center" valign="middle"  style="width: 20%; height: 20px" Class="td_EditHeader">
                              <asp:Label ID="Label4" runat="server" Text="意見內容" Width ="99%" BorderColor ="White"></asp:Label>
                          </td>
                          <td align="left" width="20%"  style="height: 20px; border:0" Class="td_detail">
                               <asp:CheckBox ID="CkboxTrace" runat="server" Font-Size="12px" Text="加入追蹤清單" />
                          </td>
                          <td align="left" width="20%"  style="height: 20px; border:0" Class="td_detail">
                               <asp:CheckBox ID="CkFlowDispatchFlag" runat="server" Text="急件" Font-Size="12px" />
                               <asp:HiddenField ID="UrlCheckFlag" Value="Y" runat="server" />
                          </td>
                          <td style="width:50%" align="left">
                                <asp:Label ID="lblPreUse" runat="server" Text="◎本授信案屬先行動用案件" ForeColor="red" Font-Underline="true" style="display:none" Font-Size="12px"></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td align="center" valign="middle"  style="width: 10%; height: 21px" Class="td_EditHeader">
                              <asp:Label ID="Label5" runat="server" Text="關卡片語" Width ="99%" BorderColor ="White"></asp:Label>
                          </td>
                        <td colspan="3" Class="td_detail">
                            <asp:DropDownList ID="FlowPhraseDrpList" CssClass="DropDownListStyle" runat="server"  DataTextField="LFlowPhrase" DataValueField="FlowPhrase" AutoPostBack="true" Width="350px"></asp:DropDownList>
                            <asp:LinkButton ID="lbnPhraseEdit" ForeColor="blue" runat="server" Text="◎編輯自訂片語◎" Font-Size="12px"></asp:LinkButton>
                            <asp:Button ID="btnOpinTemp" runat="server"  Text="意見暫存" Width="65px" CommandName="UcSubmit" OnClick="btnOpinTemp_Click" CssClass="buttonface" />
                            <asp:Button ID="btnRefresh" runat="server" OnClick="btnRefresh_Click" Text="Button" />
                        </td>
                    </tr>
                    <tr>
                         <td align="center" colspan="4">
                              <textarea id="TxtAreaOption" style="width:100%;font-size: 12px; height:60px" runat="server" rows="3" class="InputTextStyle_Thin" ></textarea>
                         </td>
                    </tr> 
                </table>
                  <table style="width:100%">
                      <tr>
                         <td>
                         
                             <asp:DataList ID="dlNextStep" runat="server" Width="100%" DataKeyField="">
                                 <ItemTemplate>
                   
                                     <table style="width:100%" >
                                          <tr>
                                              <td style="width:35%">
                                                  &nbsp; &nbsp;<asp:Button ID="btnAction" CssClass="buttonface" runat="server" Text="" Width="180px" CommandName="UcSubmit" />
                                              </td>
                                              <td style="width:38%">
                                                  &nbsp; &nbsp;<asp:Label ID="lblMustOpin" runat="server" Text="" CssClass="f11">=></asp:Label> <asp:Label ID="lblDesc" runat="server" Text="" CssClass="td_header"></asp:Label> 
                                              </td>
                                              <td style="width: 27%">
                                                  &nbsp; <asp:DropDownList ID="ddlUserList" runat="server" CssClass="DropDownListStyle" style="width:150px"></asp:DropDownList>
                                                  <asp:TextBox runat="server" style="text-align:center" id="txtUserList" CssClass="InputTextStyle_Thin" Width="150px"></asp:TextBox>  
                                               </td>
                                               <td style="width: 0%">
                                                   <asp:HiddenField ID="lblBeforeUrl" runat="server" />
                                                   <asp:HiddenField ID="lblFlowStepMail" runat="server" />
                                                   <asp:HiddenField ID="lblFinalOpin" runat="server" />
                                               </td>
                                          </tr>
                                        </table>
                                 </ItemTemplate>            
                             </asp:DataList>
                          </td>
                      </tr>
                   </table>
             </fieldset>  
         </td>
      </tr>
      </table>      
      <asp:Button ID="btnShowFlowDetail" runat="server" style="display:none" />
    </form>  
</body>
</html>
