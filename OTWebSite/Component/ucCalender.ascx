<%@ Control Language="VB" AutoEventWireup="True" CodeFile="ucCalender.ascx.vb" Inherits="Component_ucCalender" %>
<link type="text/css" rel="stylesheet" href="../StyleSheet.Css"/>
<asp:TextBox 
    ID="txtDate" 
    runat="server"
    CssClass="NormalTextStyle1"  
    ValidationGroup="CheckData" 
    Width="80px"
    Style="ime-mode:disabled" />
<asp:ImageButton 
    ID="imgDate" 
    runat="Server" 
    AlternateText="點我顯示日曆" 
    CausesValidation="False" 
    ImageUrl="../images/calendar.gif"/>
<ajaxToolkit:MaskedEditExtender 
    ID="meeDate" 
    runat="server" 
    AcceptNegative="Left" 
    DisplayMoney="Right" 
    ErrorTooltipEnabled="True" 
    Mask="9999/99/99"
    MaskType="Date"
    UserDateFormat="YearMonthDay"
    MessageValidatorTip="true" 
    OnFocusCssClass="MaskedEditFocus"
    OnInvalidCssClass="MaskedEditError" 
    TargetControlID="txtDate" />
<ajaxToolkit:MaskedEditValidator 
    ID="mevSDate" 
    runat="server" 
    ControlExtender="meeDate" 
    ControlToValidate="txtDate" 
    Display="Dynamic" 
    EmptyValueBlurredText="請輸入日期" 
    EmptyValueMessage="請輸入日期" 
    InvalidValueBlurredMessage="日期格式錯誤"   
    InvalidValueMessage="日期格式錯誤" 
    TooltipMessage="請輸入日期" 
    ValidationGroup="CheckData" />
<ajaxToolkit:CalendarExtender 
    ID="ceDate" 
    runat="server" 
    CssClass="Calendar" 
    Format="yyyy/MM/dd"
    PopupButtonID="imgDate"
    TargetControlID="txtDate" />
