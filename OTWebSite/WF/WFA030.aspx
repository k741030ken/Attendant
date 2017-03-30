<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFA030.aspx.vb" Inherits="WF_WFA030" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>流程明細</title>
    <link type="text/css" rel="stylesheet" href="../StyleSheet.Css" />
    <script type="text/javascript" language="javascript">
    <!--
        function funAction(Param)
        {
            switch(Param)
            {
                case 'btnActionP':
                    window.self.focus();
                    window.self.print();
                    return false;
                    break;
                case 'btnActionX':
                    window.top.close();
                    break;
            }
        }
        
        function checkOpener()
        {
　　        if (window.opener) { 
　　             document.getElementById("btnClose").style.display = "";
　　             document.getElementById("ucTitle1").style.display = "inline-block";
　　        }else{
　　　　          document.getElementById("btnClose").style.display = "none";
　　　　          document.getElementById("ucTitle1").style.display = "none";
            }
         }   
    -->
    </script>
  
</head>
<body  style="margin-top:10px; margin-left:30px">
    <form id="frmContent" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 97%;">
      <tr>
         <td>
             <asp:DataList ID="DataListMaster" runat="server" Width="100%">  
                     <ItemTemplate>
                           <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
                                 <tr>
                                       <td colspan= "3" >
                                           <asp:Label ID="labApplyCaseDesc" Height="16px" style="vertical-align:baseline" runat="server" Text="标头" CssClass="td_EditHeader"></asp:Label>
                                           <asp:Label ID="LabFlowCaseDesc" runat="server" CssClass="td_detail"></asp:Label>
                                       </td>
                                  </tr>  
                                  <tr>   
                                       <td align="left" width="15%" style="height: 18px" class="td_header" >
                                            <asp:Label ID="labApplyNoTitle" runat="server" Text="申請編號" Width ="100%"></asp:Label> 
                                       </td>
                                       <td align="left" width="35%"  style="height: 18px" class="td_header" >
                                          <asp:Label ID="labCustNmTitle" runat="server" Text="客戶名稱" Width ="100%"></asp:Label>
                                       </td>
                                       <td align="left" style="height: 18px; width: 50%;" class="td_header">
                                          <asp:Label ID="labNoteTitle" runat="server" Text="摘要" Width ="100%"></asp:Label> 
                                       </td>
                                  </tr>   
                                 <tr>
                                       <td align="Center"  style="width: 10%; height: 21px" class="td_detail">
                                           <asp:Label ID="labApplyNo" runat="server" Text="" Width ="99%" BorderColor ="White"></asp:Label>
                                       </td>
                                       <td align="left"  style="height: 21px" class="td_detail">
                                            <asp:Label ID="labCustNm" runat="server" Text="" Width ="99%" BorderColor ="White"></asp:Label> 
                                       </td>
                                       <td align="left"   style="height: 21px" class="td_detail">
                                          <asp:Label ID="labNote" runat="server" Text="" Width ="99%" BorderColor ="White"></asp:Label>
                                       </td>
                                  </tr> 
                                 <tr>
                                     <td colspan="3" style=" height: 4px"></td>
                                 </tr>
                                  <tr id="trCC" style="display:none" runat="server">
                                     <td colspan="3">
                                        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
                                            <tr>
                                                <td width="5%"></td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" CssClass="f9" Font-Bold="True" ForeColor="Blue" Text="徵信組長意見："></asp:Label>                                       
                                                    <asp:DataList ID="dlCC" runat="server" Width= "100%">
                                                        <ItemTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="1" width="100%" >
                                                                <tr>
                                                                    <td align="left" width="15%" style="height: 18px" class="td_header">
                                                                        <asp:Label ID="Label1" runat="server" Text="申請編號" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td align="left" width="35%" style="height: 18px" class="td_header">
                                                                        <asp:Label ID="Label3" runat="server" Text="客戶名稱" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="height: 18px; width: 50%;" class="td_header">
                                                                        <asp:Label ID="Label5" runat="server" Text="摘要" Width="100%"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                       <td align="Center" style="width: 10%; height: 21px" class="td_detail">
                                                                           <asp:Label ID="lblApplyNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "KeyID") %>'></asp:Label>
                                                                       </td>
                                                                       <td align="left"  style="height: 21px" class="td_detail">
                                                                            <asp:Label ID="lblCustNm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName") %>'></asp:Label> 
                                                                       </td>
                                                                       <td align="left"   style="height: 21px" class="td_detail">
                                                                          <asp:Label ID="lblNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlowNote") %>'></asp:Label>
                                                                       </td>
                                                                  </tr> 
                                                            </table>
                                                            <table border="0" cellpadding="0" cellspacing="1" width="100%" >
                                                                 <tr>
                                                                     <td align="center"  class="td_EditHeader" style='width: 5%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                         <asp:Label ID="Label2" runat="server" Text="關卡" Width="100%" BorderColor ="White"></asp:Label>
                                                                     </td>
                                                                     <td align="center" width="20%" class="td_detail" style='height: 21px;width: 15%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>' >
                                                                        <%#DataBinder.Eval(Container.DataItem, "FlowStepDesc")%>
                                                                     </td>
                                                                     <td align="center"  class="td_EditHeader" style='width: 5%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                         <asp:Label ID="Label4" runat="server" Text="動作" Width="100%" BorderColor ="White"></asp:Label>
                                                                     </td>
                                                                     <td align="center"  width="13%" class="td_detail" style='height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                         <%#IIf(DataBinder.Eval(Container.DataItem, "FlowStepAction").ToString() = "", "　", DataBinder.Eval(Container.DataItem, "FlowStepAction"))%>
                                                                         <%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "(同意)", " ")%>
                                                                     </td>
                                                                      <td align="center"  width="8%" class="td_EditHeader" style='height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                         <asp:Label ID="Label7" runat="server" Text="指派/執行" Width="100%" BorderColor ="White"></asp:Label>
                                                                     </td>
                                                                     <td align="center" class="td_detail" style='width: 24%;height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                         <%#DataBinder.Eval(Container.DataItem, "ToUserName")%> 
                                                                     </td>
                                                                     <td align="center" class="td_detail" style='width: 15%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag").ToString() = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                        <%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "UpdDate")) = True, "　", DataBinder.Eval(Container.DataItem, "UpdDate", "{0:yyyy/MM/dd HH:mm:ss}"))%>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td align="center"  style=" height: 21px"  class="td_EditHeader">
                                                                         <asp:Label ID="Label9" runat="server" Text="意見" Width="100%" BorderColor ="White"></asp:Label>
                                                                     </td>
                                                                     <td align="left" colspan="6" valign="top" class="td_detail">
                                                                         <%#IIf(DataBinder.Eval(Container.DataItem, "LogRemark").ToString() = "", IIf(DataBinder.Eval(Container.DataItem, "ShowMode").ToString() = "1", DataBinder.Eval(Container.DataItem, "FlowStepOpinion").ToString.Replace(chr(10),"<BR>"), "* * * * * * * * * * * "), IIf(DataBinder.Eval(Container.DataItem, "ShowMode").ToString() = "1", DataBinder.Eval(Container.DataItem, "FlowStepOpinion").ToString.Replace(chr(10),"<BR>"), "* * * * * * * * * * * ") & "<BR>" & DataBinder.Eval(Container.DataItem, "LogRemark")) & "　"%>
                                                                     </td>
                                                                 </tr>
                                                                 <tr>
                                                                     <td colspan="7" style=" height: 4px"></td>
                                                                 </tr>
                                                             </table>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </td>
                                            </tr>
                                        </table>
                                     </td>
                                  </tr>
                                  <tr>
                                     <td colspan="3">
                                                 <asp:DataList ID="DataDetail" runat="server" Width= "100%">
                                                 <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%">
                                                         <tr>
                                                             <td colspan="7"  style=" height: 4px"></td>
                                                         </tr>
                                                         <tr >
                                                             <td align="center"  class="td_EditHeader" style='width: 5%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                 <asp:Label ID="Label2" runat="server" Text="關卡" Width="100%" BorderColor ="White"></asp:Label>
                                                             </td>
                                                             <td align="center" width="20%" class="td_detail" style='height: 21px;width: 15%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>' >
                                                                <%#DataBinder.Eval(Container.DataItem, "FLowStepDesc")%>
                                                             </td>
                                                             <td align="center"  class="td_EditHeader" style='width: 5%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                 <asp:Label ID="Label4" runat="server" Text="動作" Width="100%" BorderColor ="White"></asp:Label>
                                                             </td>
                                                             <td align="center"  width="13%" class="td_detail" style='height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                 <%#IIf(DataBinder.Eval(Container.DataItem, "FlowStepAction") = "", "　", DataBinder.Eval(Container.DataItem, "FlowStepAction"))%>
                                                                 <%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "(同意)", " ")%>
                                                             </td>    
                                                              <td align="center"  width="8%" class="td_EditHeader" style='height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                 <asp:Label ID="Label7" runat="server" Text="指派/執行" Width="100%" BorderColor ="White"></asp:Label>
                                                             </td>
                                                             <td align="center" class="td_detail" style='width: 24%;height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                 <%#DataBinder.Eval(Container.DataItem, "ToUserName")%> 
                                                             </td>
                                                             <td align="center" class="td_detail" style='width: 15%; height: 21px;<%#IIf(DataBinder.Eval(Container.DataItem, "AgreeFlag") = "Y", "font-weight:bold;color:Blue;", " ")%>'>
                                                                <%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "UpdDate")) = True, "　", DataBinder.Eval(Container.DataItem, "UpdDate", "{0:yyyy/MM/dd HH:mm:ss}"))%>
                                                             </td>
                                                         </tr>
                                                         <tr>
                                                             <td align="center"  style=" height: 21px"  class="td_EditHeader">
                                                                 <asp:Label ID="Label9" runat="server" Text="意見" Width="100%" BorderColor ="White"></asp:Label>
                                                             </td>
                                                             <td align="left" colspan="6" valign="top" class="td_detail">
                                                                 <%#IIf(DataBinder.Eval(Container.DataItem, "LogRemark") = "", IIf(DataBinder.Eval(Container.DataItem, "ShowMode") = "1", DataBinder.Eval(Container.DataItem, "FlowStepOpinion").ToString.Replace(chr(10),"<BR>"), "* * * * * * * * * * * "), IIf(DataBinder.Eval(Container.DataItem, "ShowMode") = "1", DataBinder.Eval(Container.DataItem, "FlowStepOpinion").ToString.Replace(chr(10),"<BR>"), "* * * * * * * * * * * ") & "<BR>" & DataBinder.Eval(Container.DataItem, "LogRemark")) & "　"%>
                                                             </td>
                                                         </tr>
                                                     </table>
                                                 </ItemTemplate>
                                             </asp:DataList><br />
                                     </td>
                                  </tr>  
                            </table> 
                         </ItemTemplate>
             </asp:DataList>
         
          </td>
      </tr>
      </table>  
        
 
    </form>  
</body>
</html>
