// 此為UserControl專用的script
/******************************************************************************
** ucDate.ascx
******************************************************************************/
function funGetDate(obj, strDate)
{
    var ctl = document.getElementById(obj);
    var url = "../Component/Frameset.aspx?URL=" + encodeURIComponent("../Component/SelectDate.aspx?Date=" + ctl.value);
    var strRtn;
    
    strRtn = ShowDialog(url, '235', '238');
    
    if (strRtn != undefined && strRtn != '')
    {
        if (ctl != undefined)
            ctl.value = strRtn;
    }
    
    return false;
}

//檢核(検核)日期格式 YYYYMMDD or YYYY/MM/DD
function IsValidYYYYsMMsDD(strDateText)
{
	var aDaysInMonth=new Array(31,28,31,30,31,30,31,31,30,31,30,31);
	var strDates,iMonth,iDay,iYear;
	if(strDateText.indexOf("/")>=0)
	{
		strDates=strDateText.split("/");
		if(strDates.length!=3) return false;
	}
	else
	{
		if(strDateText.length!=8) return false;
		strDates = new Array(strDateText.substr(0,4),strDateText.substr(4,2),strDateText.substr(6,2));
	}
	iYear  = parseInt(strDates[0],10);
	if(isNaN(iYear)) return false;
	if(iYear<1753 || iYear>9999) return false; //check for SQL Server Datetime's Year range
	iMonth = parseInt(strDates[1],10);
	if(isNaN(iMonth)) return false;
	iDay = parseInt(strDates[2],10);
	if(isNaN(iDay)) return false;
	var iDaysInMonth=(iMonth!=2) ? aDaysInMonth[iMonth-1] : ((iYear%4==0 && iYear%100!=0 || iYear % 400==0)?29:28);
	if(!(iMonth<13 && iMonth>0 && iDay>0 && iDay<=iDaysInMonth)) return false;
	return true;
}
//CustomValidation function
function ValidateYYYYsMMsDD(source,argument)
{
	argument.IsValid = IsValidYYYYsMMsDD(argument.Value);
}

//(U)2006/02/17 By Gemy 
function ValidateOverToday(source, argument)
{	
	var sdate = argument.Value;
	var today=new Date();
	var stoday=(today.getUTCFullYear() + '/' + (today.getUTCMonth()+1) + '/' + today.getUTCDate());	
	
	if (Date.parse(sdate) > (Date.parse(stoday)) )
	{
	    argument.IsValid = alert("日期--日期不可大於今日");	
	}
	
}

//Format Date-Text to YYYY/MM/DD
function FormatDateText(strDateText)
{
	if(!IsValidYYYYsMMsDD(strDateText))
	{
		return strDateText;				
	}
	var strDates,iMonth,iDay,iYear;
	if(strDateText.indexOf("/")>=0)
	{
		strDates=strDateText.split("/");
		if(strDates.length!=3) return false;
	}
	else
	{
		if(strDateText.length!=8) return false;
		strDates = new Array(strDateText.substr(0,4),strDateText.substr(4,2),strDateText.substr(6,2));
	}
	var sYear  = String(parseInt(strDates[0],10));
	while(sYear.length<4) sYear = "0" + sYear;
	var sMonth = String(parseInt(strDates[1],10));
	while(sMonth.length<2) sMonth = "0" + sMonth;
	var sDay = String(parseInt(strDates[2],10));
	while(sDay.length<2) sDay = "0" + sDay;
	
	return sYear + "/" + sMonth + "/" + sDay;
}


//========================================================
//ucMultiSelect.ascx使用
//========================================================
function SaveResult2(lst,txtText,txtValue)
{	
	var strNameResult="";
	var strValueResult="";
	var opts=lst.options;
	for(var i=0;i<opts.length;i++)
	{
		var li=opts[i];
		var strItemName=li.text;
		var strItemValue=li.value;
		if(strNameResult.length>0)
		{
			strNameResult = strNameResult + ",";
		}
		strNameResult = strNameResult + strItemName;
		
		if(strValueResult.length>0)
		{
			strValueResult = strValueResult + ",";
		}
		strValueResult = strValueResult + strItemValue;
	}

	txtText.value=strNameResult;
	txtValue.value=strValueResult;
}

function SaveResult(lst,txtResult)
{
	var strResult="";	
	var opts=lst.options;
	for(var i=0;i<opts.length;i++)
	{
		var li=opts[i];
		var strItem=li.text + "|" + li.value;
		if(strResult.length>0)
		{
			strResult = strResult + "\x09";
		}
		strResult = strResult + strItem;
	}
	txtResult.value=strResult;
}

function MoveUp(lstLeft,txtLeftResult,lstRight,txtRightResult,txtTextResult,txtValueResult)
{
	if(lstRight.selectedIndex<=0) return;
	var opts=lstRight.options;
	for(var i=1;i<opts.length;i++)
	{
		var it=opts[i];
		if(it.selected)
		{
			opts.remove(i);
			opts.add(it,i-1);
		}
	}
	SaveResult(lstLeft,txtLeftResult);
	SaveResult(lstRight,txtRightResult);
	SaveResult2(lstRight,txtTextResult,txtValueResult);
	
}

function MoveDown(lstLeft,txtLeftResult,lstRight,txtRightResult,txtTextResult,txtValueResult)
{
	var opts=lstRight.options;
	if(lstRight.selectedIndex<0 || opts[opts.length-1].selected) return;
	for(var i=opts.length-2;i>=0;i--)
	{
		var it=opts[i];
		if(it.selected)
		{
			opts.remove(i,1);
			opts.add(it,i+1);
		}
	}
	SaveResult(lstLeft,txtLeftResult);
	SaveResult(lstRight,txtRightResult);
	SaveResult2(lstRight,txtTextResult,txtValueResult);
}

function MoveRight(lstLeft,txtLeftResult,lstRight,txtRightResult,txtTextResult,txtValueResult)
{
	if(lstLeft.selectedIndex<0) return;
	var opts=lstLeft.options;
	var addIndex = lstRight.options.length;
	for(var i=opts.length-1;i>=0;i--)
	{
		var it=opts[i];
		if(it.selected)
		{
			opts.remove(i);
			lstRight.options.add(it,addIndex);
			it.selected=false;
		}
	}
	SaveResult(lstLeft,txtLeftResult);
	SaveResult(lstRight,txtRightResult);
	SaveResult2(lstRight,txtTextResult,txtValueResult);
}

function MoveLeft(lstLeft,txtLeftResult,lstRight,txtRightResult,txtTextResult,txtValueResult)
{
	if(lstRight.selectedIndex<0) return;
	var opts=lstRight.options;
	var addIndex = lstLeft.options.length;
	for(var i=opts.length-1;i>=0;i--)
	{
		var it=opts[i];
		if(it.selected)
		{
			opts.remove(i);
			lstLeft.options.add(it,addIndex);
			it.selected=false;
		}
	}
	SaveResult(lstLeft,txtLeftResult);
	SaveResult(lstRight,txtRightResult);
	SaveResult2(lstRight,txtTextResult,txtValueResult);
}

//========================================================
//ucButtonMutiSelect.ascx使用,傳回PageMutiSelect.aspx的Return值(Value||Name)
//========================================================
function PageCallReturnValue(txtParam,txtValueResult,txtTextResult,txtValueDataControl,txtTextDataControl)
{          
    var strReturn='';        
    strReturn=window.top.showModalDialog(txtParam.value,"","status:no;center:yes;resizable:no;scrollbars:no;dialogWidth:600px;dialogHeight:430px")

    if ((strReturn!=undefined) && (strReturn.length>0))
    {
        txtValueResult.value=strReturn.split('|$|')[0];
        txtTextResult.value=strReturn.split('|$|')[1];

        if (txtValueDataControl!=undefined) 
        {
             txtValueDataControl.value=strReturn.split('|$|')[0];
        }
        if (txtTextDataControl!=undefined) 
        {
            txtTextDataControl.value=strReturn.split('|$|')[1];
        }
              
    }
   
}

//========================================================
//ucButtonQuerySelect.ascx使用,傳回PageQuerySelect.aspx的Return值(Value)
//========================================================
function PageQuerySelectReturnValue(txtParam,txtValueResult, ctlID, WindowWidth, WindowHeight, UserControlID)
{       
    var strReturn='';    

    //strReturn=window.top.showModalDialog(txtParam.value,"","status:no;center:yes;resizable:no;scrollbars:no;dialogWidth:700px;dialogHeight:540px")
    strReturn = ShowDialogWithHeader2(txtParam.value, WindowWidth, WindowHeight);

    if ((strReturn!=undefined) && (strReturn.length>0))
    {
        txtValueResult.value = strReturn;
        if (ctlID != '')
        {
            var obj = document.getElementById(ctlID);
            if (obj != undefined)
                obj.value = strReturn;
        }
        var objreturnValue = document.getElementById('__returnValue');
	    if (objreturnValue != undefined)
	    {
	        strReturn = UserControlID + ':' + strReturn
	        objreturnValue.value = strReturn;
	        var bolCallback = true;
	        //嘗試呼叫onModalReturn
	        try
	        {
	            bolCallback = onShowModalReturn(strReturn);
	        }
	        catch(ex)
	        {bolCallback = true;}

            if (bolCallback)
            {
                var objbtn = document.getElementById('__btnDoModalReturn');
                if (objbtn != undefined)
                {
                    objbtn.click();
                }
            }
	    }
        
    }

}

//========================================================
//ucButtonWorkType.ascx使用,傳回PageWorkType.aspx的Return值(Value)
//========================================================
function PageWorkTypeReturnValue(txtParam, txtValueResult, ctlID, WindowWidth, WindowHeight, UserControlID) {
    var strReturn = '';

    //strReturn=window.top.showModalDialog(txtParam.value,"","status:no;center:yes;resizable:no;scrollbars:no;dialogWidth:700px;dialogHeight:540px")
    strReturn = ShowDialogWithHeader2(txtParam.value, WindowWidth, WindowHeight);

    if ((strReturn != undefined) && (strReturn.length > 0)) {
        txtValueResult.value = strReturn;
        if (ctlID != '') {
            var obj = document.getElementById(ctlID);
            if (obj != undefined)
                obj.value = strReturn;
        }
        var objreturnValue = document.getElementById('__returnValue');
        if (objreturnValue != undefined) {
            strReturn = UserControlID + ':' + strReturn
            objreturnValue.value = strReturn;
            var bolCallback = true;
            //嘗試呼叫onModalReturn
            try {
                bolCallback = onShowModalReturn(strReturn);
            }
            catch (ex)
	        { bolCallback = true; }

            if (bolCallback) {
                var objbtn = document.getElementById('__btnDoModalReturn');
                if (objbtn != undefined) {
                    objbtn.click();
                }
            }
        }

    }

}

//========================================================
//ucButtonPositio.ascx使用,傳回PagePositio.aspx的Return值(Value)
//========================================================
function PagePositionReturnValue(txtParam, txtValueResult, ctlID, WindowWidth, WindowHeight, UserControlID) {
    var strReturn = '';

    //strReturn=window.top.showModalDialog(txtParam.value,"","status:no;center:yes;resizable:no;scrollbars:no;dialogWidth:700px;dialogHeight:540px")
    strReturn = ShowDialogWithHeader2(txtParam.value, WindowWidth, WindowHeight);

    if ((strReturn != undefined) && (strReturn.length > 0)) {
        txtValueResult.value = strReturn;
        if (ctlID != '') {
            var obj = document.getElementById(ctlID);
            if (obj != undefined)
                obj.value = strReturn;
        }
        var objreturnValue = document.getElementById('__returnValue');
        if (objreturnValue != undefined) {
            strReturn = UserControlID + ':' + strReturn
            objreturnValue.value = strReturn;
            var bolCallback = true;
            //嘗試呼叫onModalReturn
            try {
                bolCallback = onShowModalReturn(strReturn);
            }
            catch (ex)
	        { bolCallback = true; }

            if (bolCallback) {
                var objbtn = document.getElementById('__btnDoModalReturn');
                if (objbtn != undefined) {
                    objbtn.click();
                }
            }
        }

    }

}

//========================================================
//ucID.js
//========================================================
function IsNumericString(str)
{
	for(var i=0;i<str.length;i++)
	{
		var c=str.charCodeAt(i);
		if(c<48 || c>57) return false;
	}
	return true;
}
function IsEnglishLetterString(str)
{
	for(var i=0;i<str.length;i++)
	{
		var c=str.charCodeAt(i);
		if(!(c>=65 && c<=90) || (c>=97 && c<=122)) return false;
	}
	return true;
}
function IsCitizenID(strID)
{

	if(strID.length!=10) return false;
	strID=strID.toUpperCase();
	var c = strID.charCodeAt(0)-65;
	if(c<0 || c>25) return false;
	if(!IsNumericString(strID.substr(1))) return false;
	if(strID.charAt(1)!='1' && strID.charAt(1)!='2') return false;
	var codes=new Array(10,11,12,13,14,15,16,17,34,18,19,20,21,22,35,23,24,25,26,27,28,29,32,30,31,33);
	var se=new Array(10);
	se[0]=parseInt(codes[c].toString().charAt(0));
	se[1]=parseInt(codes[c].toString().charAt(1));
	var we=se[0]+(se[1]*9);
	var checkcode=0;
	for(i=1;i<=9;i++) 
	{
		se[i+1]=parseInt(strID.substr(i,1));
		we=we+(se[i+1]*(9-i));
	}
	we=we+se[10]
	checkcode=(we%10);
	if(checkcode!=0) return false;        
	return true;
}

function IsCompanyID(strID)
{
	if(strID.length!=8) return false;
	if(!IsNumericString(strID)) return false;
	var seeds = new Array(1,2,1,2,1,2,4,1);
	var chk_sum = 0;
	for(var i=0;i<8;i++)
	{
		var val = strID.charAt(i) * seeds[i] ;
		chk_sum = chk_sum + parseInt(val/10,10) + (val % 10) ;                  	   	
	}   
	if ((chk_sum % 10) == 0) 
		return true;
	else if (strID.charAt(6) == 7 && (chk_sum + 1) % 10 == 0) 
		return true;
	else
		return false;
	
}

function IsMappingID(strID)
{
	if(strID.length==10 && strID.substr(0,2)=="CS" && IsNumericString(strID.substr(2))) return true;
	if(strID.length>3 && (strID.substr(0,3)=="THK" || strID.substr(0,3)=="OBU") || strID.substr(0,3)=="TLA") return true;
	if(strID.length>2 && strID.substr(0,2)=="LA") return true;
	return false;
}

function IsForeignID(strID)
{
	if(strID.length!=10) return false;
	/* update by Chung 2007.09.07 修改外國人ID檢核
	if(!IsNumericString(strID.substr(0,8))) return false;
	if(!IsEnglishLetterString(strID.substr(8,2))) return false;
	*/
	if(IsNumericString(strID.substr(0,8)) && IsEnglishLetterString(strID.substr(8,2))) return true;
	if(IsNumericString(strID.substr(2,8)) && IsEnglishLetterString(strID.substr(0,2))) return true;
	return false;
}
//檢核(検核)身分證字號,統一編號,自編代號,外國人代號

function IsValidID(strID)
{
	if(IsMappingID(strID)) return true;
	if(IsCitizenID(strID)) return true;
	if(IsCompanyID(strID)) return true;
	if(IsForeignID(strID)) return true;
	return false;
}

/**************************************************************
功能說明：PageQueryMultiSelect使用，控制畫面Checkbox選取
建立人員：A02976
建立日期：2007.04.25
***************************************************************/
function __pqmsMultiSelected(GridName, chkboxName, txtKeyName)
{
    var objchkbox = document.getElementById(chkboxName);
    var objtxtKey = document.getElementById(txtKeyName);
    var obj = document.getElementById('__SelectedRows' + GridName);

    if (objchkbox != undefined)
    {
        if (obj.value.indexOf(objtxtKey.value) >= 0)
            objchkbox.checked = false
        else
            objchkbox.checked = true;
       
        if (obj != undefined)
        {
            if (objchkbox.checked)
            {
                if (obj.value != '') obj.value = obj.value + ',';
                obj.value = obj.value + objtxtKey.value;
            }
            else
            {
                obj.value = obj.value.replace(',' + objtxtKey.value, '');
                obj.value = obj.value.replace(objtxtKey.value, '');
            }
        }
    }
}

/*
功能說明：GridView上的光棒效果(checkbox使用)
建立人員：A02976
建立日期：2007.03.16
*/
function __pqmsOnmouseout(me, GridName, Key, RowIndex) {
    var obj = document.getElementById('__SelectedRows' + GridName);
 
    if (obj != undefined)
    {
        if (obj.value.indexOf(Key) >= 0)
        {
            me.style.backgroundColor = 'Moccasin';
        }
        else
        {
            if (parseInt(RowIndex) % 2 == 0)
                me.style.backgroundColor = 'white'
            else
                me.style.backgroundColor = '#e2e9fe';
        }
    }
}

/******************************************************************************
** ucAddress.ascx
******************************************************************************/
function funGetAddress(obj, CountryNo, ZoneNo, CityNo, ZoneSubNo, RoadDetail)
{
    var ctl = document.getElementById(obj);
    if (ctl == undefined) return false;
    var URL = '?FunID=MAddress&Path=../Component/AddressHelper.aspx';
    var strRtn = ShowDialogWithHeader2(URL, "450px", "250px");
    
    if (strRtn != undefined && strRtn != '')
    {
        var aryData = new Array;
        aryData = strRtn.split('|$|');
        ctl.value = aryData[4];
        ctl = document.getElementById(CountryNo);
        ctl.value = aryData[0];
        ctl = document.getElementById(ZoneNo);
        ctl.value = aryData[1].toString().split('|')[0];
        ctl = document.getElementById(CityNo);
        ctl.value = aryData[1].toString().split('|')[1];
        ctl = document.getElementById(ZoneSubNo);
        ctl.value = aryData[2].toString();
        ctl = document.getElementById(RoadDetail);
        ctl.value = aryData[3].toString();
    }
    
    return false;
}


