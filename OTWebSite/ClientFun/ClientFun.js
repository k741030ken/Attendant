// JScript 檔
window.history.forward(); 
//必須存為 UTF-8 否則無法成功執行 (Must be UTF-8 encoded) 简体ぎ︰
var wndModelessChilds = new Array(0);
var wndModalChild;
var isRootWindow = false;
var wndPopup = window.createPopup();
var intervalID=null;
var timeoutID=null;
var discardedThreadIDs = new Array(0);
var intCurrentInterval=0;
var CS_QUERYSTRING_NAME_LAUNCH_PAGE = "_LP";

var dotNet__doPostBack=undefined;

function showSubmitting(handler) {
    try { 
        if (navigator.userAgent.search("MSIE") > -1) {
            if (handler == undefined)
                popupMessage('資料傳送中，請稍候...');
            else {
                popupMessageE(handler, '資料傳送中，請稍候...');
            }
        }
    }
    catch (ex) {
    }
    return true;
}

function hidePopupWindow(handler) {
    if (navigator.userAgent.search("MSIE") > -1) {
        if (handler == undefined) {
            if (wndPopup != undefined)
                wndPopup.hide();
        }
        else {
            handler.hide();
        }
    }
}

function popupMessage(msg,w,h)
{
    if (wndPopup.isOpen) return;
	//window.focus();
	if(w==undefined) w=screen.width/3;
	if(h==undefined) h=screen.height/6;
    var oBody = wndPopup.document.body;
	oBody.style.overflow="auto";
	oBody.innerHTML="<table width='100%' height='100%' cellpadding='0' cellspacing='0' style='OVERFLOW: auto; BORDER-TOP-STYLE: ridge; BORDER-RIGHT-STYLE: ridge; BORDER-LEFT-STYLE: ridge; BACKGROUND-COLOR: lightyellow; BORDER-BOTTOM-STYLE: ridge'><tr height=100%><td align=center valign=center >" + msg + "</td></tr></table>"
    //wndPopup.show((screen.width-w)/2, (screen.height-h)/2, w, h);
    var x=(window.top.document.body.offsetWidth - w)/2 + window.top.screenLeft + window.top.document.body.offsetLeft;
    var y=(window.top.document.body.offsetHeight - h)/2 + window.top.screenTop + window.top.document.body.offsetTop;
    wndPopup.show(x,y,w,h);
}

function popupMessageE(handler,msg,w,h)
{
    if (wndPopup.isOpen) return;
	//window.focus();
	if(w==undefined) w=screen.width/3;
	if(h==undefined) h=screen.height/6;
    var oBody = handler.document.body;
	oBody.style.overflow="auto";
	oBody.innerHTML="<table width='100%' height='100%' cellpadding='0' cellspacing='0' style='OVERFLOW: auto; BORDER-TOP-STYLE: ridge; BORDER-RIGHT-STYLE: ridge; BORDER-LEFT-STYLE: ridge; BACKGROUND-COLOR: lightyellow; BORDER-BOTTOM-STYLE: ridge'><tr height=100%><td align=center valign=center >" + msg + "</td></tr></table>"
    //wndPopup.show((screen.width-w)/2, (screen.height-h)/2, w, h);
    var x=(window.top.document.body.offsetWidth - w)/2 + window.top.screenLeft + window.top.document.body.offsetLeft;
    var y=(window.top.document.body.offsetHeight - h)/2 + window.top.screenTop + window.top.document.body.offsetTop;
    handler.show(x,y,w,h);
}


function ne__doPostBack(eventTarget, eventArgument) 
{
	if(dotNet__doPostBack!=undefined)
	{
		dotNet__doPostBack(eventTarget, eventArgument);
		showSubmitting();
	}
}

function ne_onsubmit()
{
	if(typeof(ValidatorOnSubmit)!="undefined")
	{
		if(ValidatorOnSubmit())
		{	
			return true;
		}
		return false;
	}
	return true;
}
//控制TextBox輸入長度相關功能{
function getLengthInByte(str)
{
	var len=0;
	for(i=0;i<str.length;i++)
	{
		len++;
		if(str.charCodeAt(i)>255) len++;
	}
	return len;
}

function trimLengthInByte(obj,maxlength)
{
	if(obj == undefined || obj.type == undefined || (obj.type!="text" && obj.type!="textarea")) return 0;
	if(isNaN(maxlength) || maxlength<=0) return 0;
	var str = obj.value;
	var lenB = getLengthInByte(str);
	if(lenB>maxlength)
	{
		var lenC=str.length; //length in char
		while(lenB>maxlength)
		{
			lenC--;
			var cc=str.charCodeAt(lenC);
			if(cc>255) lenB--;
			else if(cc==10 && lenC>1 && str.charCodeAt(lenC-1)==13)
			{
				lenC--;
				lenB--;
			}
			lenB--;
		}
		obj.value = str.substr(0,lenC);
	}
	return lenB;
}

function limitInputLengthInByte(obj,maxlength)
{
	
	if(obj == undefined || obj.type == undefined || (obj.type!="text" && obj.type!="textarea")) return;
	if(isNaN(maxlength) || maxlength<=0) return;
	if(window.event.type=="keydown")
	{
		if(window.event.ctrlKey || window.event.altKey) return;
		var kc=window.event.keyCode;
		if(kc==32 || kc==106 || kc==107 || kc==226 || (kc>=48 && kc<=57)  || (kc>=65 && kc<=90) || (kc>=96 && kc<=105)  || (kc>=109 && kc<=111)  || (kc>=186 && kc<=222))
		{
			if(getLengthInByte(obj.value)>=maxlength)
			{
				window.event.returnValue=false;
				window.event.cancelBubble=true;
			}
		}
	}
	else if(window.event.type=="keyup")
	{
		if(window.event.keyCode==229) return;
		trimLengthInByte(obj,maxlength);
	}
	else if(window.event.type=="select" || window.event.type=="change")
	{
		trimLengthInByte(obj,maxlength);
	}
}
//控制TextBox輸入長度相關功能}

function dbg(msg)
{
	alert(msg);
}


function resizeWindow(intWidth,intHeight,bolCenter)
{
	if(window.top.dialogHeight==undefined)
	{
		window.top.resizeTo(intWidth,intHeight);
		if(bolCenter)
		{
			window.top.moveTo((screen.availWidth-intWidth)/2,(screen.availHeight-intHeight)/2);
		}
	}	
	else
	{
		window.top.dialogWidth = intWidth + "px";
		window.top.dialogHeight = intHeight + "px";
		if(bolCenter)
		{
			window.top.dialogLeft = String((screen.availWidth-intWidth)/2) + "px";
			window.top.dialogTop = String((screen.availHeight-intHeight)/2) + "px";
		}
	}
}

function autoResizeWindow()
{
	//window.status = document.body.scrollWidth + " / " + document.body.scrollHeight + " ; " + document.body.offsetWidth + " / " + document.body.offsetHeight + " ; " + document.body.clientWidth + " / " + document.body.clientHeight + " ; " 
	var offsetX = document.body.scrollWidth - document.body.offsetWidth;
	if(offsetX<0) offsetX=0;
	var offsetY = document.body.scrollHeight - document.body.offsetHeight;
	if(offsetY<0) offsetY=0;
	if(window.top.dialogHeight==undefined)
	{
		window.top.resizeBy(offsetX,offsetY);
		window.top.moveBy(-offsetX/2,-offsetY/2);
	}
	else
	{
		if(offsetY>0)
		{
			var dH = parseInt(window.top.dialogHeight.substr(0,window.top.dialogHeight.length-2));
			window.top.dialogHeight = String(dH + offsetY) + "px";
			var dT = parseInt(window.top.dialogTop.substr(0,window.top.dialogTop.length-2));
			window.top.dialogTop = String(dT - (offsetY/2)) + "px" ;
		}
		if(offsetX>0)
		{
			var dW = parseInt(window.top.dialogWidth.substr(0,window.top.dialogWidth.length-2));
			window.top.dialogWidth = dW + offsetX + "px";
			var dL = parseInt(window.top.dialogLeft.substr(0,window.top.dialogLeft.length-2));
			window.top.dialogLeft = String(dL - offsetX/2) + "px";
		}
	}
}

function setFirstTextControlFocus()
{
	if(window.document.forms.length>0)
	{
		for(i=0;i<window.document.forms[0].elements.length;i++)
		{
			if(window.document.forms[0].elements[i].type != undefined && window.document.forms[0].elements[i].type == "text" && window.document.forms[0].elements[i].disabled != undefined && !window.document.forms[0].elements[i].disabled) 
			{
				if(window.document.forms[0].elements[i].isDisabled == undefined || !window.document.forms[0].elements[i].isDisabled)			
				{
					try
					{
						window.document.forms[0].elements[i].focus();
						return true;
					}
					catch(e)
					{
						return false;
					}
				}
			}
		}
	}
	return false;
}

function setDefaultFocus()
{
	if(strDefaultFocusControlClientID.length>0 && window.document.forms.length>0 && window.document.forms[0].elements[strDefaultFocusControlClientID]!=undefined && window.document.forms[0].elements[strDefaultFocusControlClientID].focus!=undefined && window.document.forms[0].elements[strDefaultFocusControlClientID].disabled != undefined && !window.document.forms[0].elements[strDefaultFocusControlClientID].disabled)
	{
		if(window.document.forms[0].elements[strDefaultFocusControlClientID].isDisabled == undefined || !window.document.forms[0].elements[strDefaultFocusControlClientID].isDisabled)			
		{
			try
			{
				window.document.forms[0].elements[strDefaultFocusControlClientID].focus();
			}
			catch(e)
			{
				//alert(e);
			}
			return;
		}
	}
	if (strDefaultFocusControlClientID.length > 0) alert("無法設定元件-" + strDefaultFocusControlClientID + "的輸入焦點!");
	else setFirstTextControlFocus();
}

function setCookie(sName, sValue, expireDate)
{
	document.cookie = sName + "=" + escape(sValue) + ";path=/" + ((expireDate == undefined || expireDate == null) ? "" : ("; expires=" + expireDate.toGMTString()));
}

function getCookie(sName)
{	var a = document.cookie.split("; ");
	for(var i=0;i<a.length;i++)
	{	var a1=a[i].split("=");
		if(sName==a1[0])
		{	return unescape(a1[1]);
		}
	}
	return null;
}

function removeCookie(sName)
{
	var sValue=getCookie(sName);
	if(sValue)
	{
		setCookie(sName,sValue,new Date());
	}
}

function isLogout()
{
	return (getCookie(strFormsCookieName) == undefined);
}

function forceLogout()
{
	removeCookie(strFormsCookieName);
}

function focusOpenner()
{
	if(window.opener!=undefined) window.opener.focus();
}

function focusMainMenu()
{
	var wnd = getRootFrameContainer();
	if(wnd!=undefined)
	{
		if(wnd==window.top) window.top.location.reload();
		else if(wnd.isRootWindow) wnd.focus();
	}
}

function removeClosedChilds()
{
	for(i=wndModelessChilds.length-1;i>=0;i--)
	{
		try
		{
			if(wndModelessChilds[i][0].closed==true) wndModelessChilds.splice(i,1);
		}
		catch(e)
		{
			alert("Exception in removeClosedChilds()");
		}
	}
}

function isAnyChildNotClosed()
{
	for(i=0;i<wndModelessChilds.length;i++)
	{
		try
		{
			if(wndModelessChilds[i][0].closed==false)
			{
				return true;
			}
		}
		catch(e)
		{
			alert("Exceiption in isAnyChildNotClosed()");
		}
	}
	return false;
}

function openModalFrameChild(url,feature,form)
{
	if(urlFrameControl!=undefined)
	{
		var modalArguments = new Object();
		//mark by Chung
		//modalArguments.LaunchPage = url;
		//modalArguments.ParentWindow = window;
		if(feature==undefined) feature=strModalWindowFeature;
		form.document.parentWindow.status = "對話視窗開啟中，請稍候...'";
		var retval = window.showModalDialog(urlFrameControl+'?Path='+url, modalArguments, feature);
		if(wndModalChild != undefined)
		{
			var wnd = wndModalChild;
			wndModalChild = undefined;
			form.submit();
			form.document.parentWindow.showSubmitting();
		}
		return retval;
	}
	else
	{
	    alert("系統錯誤! urlFrameControl 值未設定!");
	}
}

function openExistModelessChildIndex(windowName,url,overrideExistWindow)
{
	if(windowName!=undefined && windowName.length>0 && windowName!="_blank")
	{
		for(i=0;i<wndModelessChilds.length;i++)
		{
			if(wndModelessChilds[i][2]==windowName)
			{
				if(wndModelessChilds[i][0] != undefined && !wndModelessChilds[i][0].closed)
				{
					if(overrideExistWindow!=undefined && overrideExistWindow==true)
					{
						//dbg("openExistModelessChildIndex : " + url);
						wndModelessChilds[i][0].open(url,"_self");
					}
					wndModelessChilds[i][0].focus();
					return true;
				}
			}
		}
	}
	return false;
}

function openModelessFrameChild(url,feature,windowName,overrideExistWindow)
{
	if(urlFrameControl!=undefined)
	{
		removeClosedChilds();
		if(feature==undefined || feature.length==0) feature=strModelessWindowFeature;
		if(windowName == undefined || windowName.length<=0) windowName = "_blank";
		windowName=windowName.replace(" ","_");
		launchUrl=urlFrameControl + "?" + CS_QUERYSTRING_NAME_LAUNCH_PAGE + "=" + encodeURIComponent(url);
		if(openExistModelessChildIndex(windowName,launchUrl,overrideExistWindow)) return;
		//dbg("openModelessFrameChild : " + launchUrl);
		var child = window.open(launchUrl,windowName,feature);
		var wndInfo = new Array();
		wndInfo.push(child);
		wndInfo.push(launchUrl);
		wndInfo.push(windowName);
		wndModelessChilds.push(wndInfo);
	}
	else
	{
	    alert("系統錯誤! urlFrameControl 值未設定!");
	}
}

function openModelessPage(url,feature,windowName,overrideExistWindow)
{
	removeClosedChilds();
	if(feature==undefined) feature=strModelessWindowFeature;
	if(windowName == undefined || windowName.length<=0) windowName = "_blank";
	if(openExistModelessChildIndex(windowName,url,overrideExistWindow)) return;
	//dbg("openModelessPage : " + url);
	var child = window.open(url,windowName,feature);
	var wndInfo = new Array();
	wndInfo.push(child);
	wndInfo.push(url);
	wndInfo.push(windowName);
	wndModelessChilds.push(wndInfo);
}

function openStateViewer()
{
	window.top.open(urlControlPath + "/StateViewer.aspx","StateViewer",strModelessWindowFeature);
}

function closeModalChild()
{

	if(wndModalChild != undefined && wndModalChild.closed != undefined && !wndModalChild.closed)
	{
		var wnd = wndModalChild;
		wndModalChild = undefined;
		wnd.forceClose();
	}
	
	for(i=0;i<wndModelessChilds.length;i++)
	{
		try
		{
			if(wndModelessChilds[i][0].closed==false)
			{
				if(wndModelessChilds[i][0].closeModalChild != undefined)
				{
					wndModelessChilds[i][0].closeModalChild();
				}
			}
		}
		catch(e)
		{
			alert("Exception in closeModalChild()");
		}
	}
}

function closeModelessChilds()
{
	for(i=wndModelessChilds.length-1;i>=0;i--)
	{
		try
		{
			if(wndModelessChilds[i][0].closed==false)
			{
				if(wndModelessChilds[i][0].forceClose != undefined)
				{
					wndModelessChilds[i][0].forceClose();
				}
				else
				{
					wndModelessChilds[i][0].close();
				}
			}
		}
		catch(e)
		{
			alert("Exception in closeModelessChilds()");			
		}
	}
	wndModelessChilds.length=0;
}

function closeAllChilds()
{
	closeModalChild();
	closeModelessChilds();
}

function confirmUnclosedChild()
{
	removeClosedChilds();
	if(isAnyChildNotClosed())
	{
	    event.returnValue = "此動作將會關閉其他子視窗!";
	}
}

function closeWindowHandler()
{
	closeAllChilds();
	var wndRoot = getRootFrameContainer();
	if(!isLogout() && wndRoot != undefined)
	{
		var ids = new Array(0);
		for(i=0;i<window.frames.length;i++)
		{
			if(window.frames[i].getAllThreadID != undefined)
			{
				window.frames[i].getAllThreadID(ids);
			}
		}
		while(ids.length>0) wndRoot.discardedThreadIDs.push(ids.pop());
		wndRoot.discardThreadID(window==wndRoot);
	}
}

function forceClose()
{
	window.onunload = null;
	closeWindowHandler();
	window.top.close();
}

function getThreadID()
{
	if(window.document.forms[0]._PAGESI != undefined)
	{
		return window.document.forms[0]._PAGESI.value;
	}
	return undefined;
}

function setTitle()
{
	//dbg("setTitle : " + window.document.title);
	try
	{
		window.top.document.title=window.document.title;
	}
	catch(e)
	{
		//window.status="Fail to set document's title!" + e;
	}
}

function contextMenuHandler()
{
	event.returnValue = false;
}

function getAllThreadID(ids)
{
	for(i=0;i<window.frames.length;i++)
	{
		if(window.frames[i].getAllThreadID != undefined)
		{
			window.frames[i].getAllThreadID(ids);
		}
	}
	var threadID = getThreadID();
	if(threadID != undefined)
	{
		ids.push(threadID);
	}
}

function removeDiscardedThreadID()
{
	timeoutID = null;
	var threadID;
	try
	{
		if(discardedThreadIDs.length>0)
		{
			threadID = discardedThreadIDs.join(",");
			discardedThreadIDs.length=0;
			getTopFrameContainer().frames["ServiceFrame"].location = window.urlRemoveThread + "?_PAGESI=" + threadID;
		}
		if(discardedThreadIDs.length>0 && timeoutID == null) timeoutID = window.setTimeout(removeDiscardedThreadID,1000);
	}
	catch(ex)
	{
		alert(ex);
	}
}

function discardThreadID(sync)
{
	if(discardedThreadIDs.length<=0) return;
	if(sync)
	{
		if(timeoutID != null) 
		{
			window.clearTimeout(timeoutID);
			timeoutID=null;
		}
		removeDiscardedThreadID();
	}
	else
	{
		if(timeoutID == null) timeoutID = window.setTimeout(removeDiscardedThreadID,2000);
	}
}

function countdownTimeout()
{
	var secondsTimeout=parseInt(getCookie("secondsTimeout"),10);
	if(isNaN(secondsTimeout)) 
	{
	    alert("找不到連線逾時 Cookie 值, 已取消連線逾時控制功能!");
		window.clearInterval(intervalID);
		intervalID = null;
		intCurrentInterval=0;
		return;
	}
	//var secondsRemain = secondsTimeout - Math.round((new Date().valueOf()/1000));
	var secondsRemain = secondsTimeout - intCurrentInterval/1000;
	setCookie("secondsTimeout",secondsRemain.toString());
	//window.status="window.parent=" + window.parent + ";secondsTimeout=" + secondsTimeout + ";secondsRemain=" + secondsRemain + ";intCurrentInterval=" + intCurrentInterval;
	var msg;
	//debug msg="您的連線作業將於" + secondsRemain + "秒鐘後失效!";
	//debug window.status = msg;
	if(secondsRemain <= 0 || isLogout())
	{
		window.clearInterval(intervalID);
		intervalID = null;
		intCurrentInterval=0;
		closeWindowHandler();
		if(!isLogout())
		{
		    msg = "您的連線作業已因逾時而失效!";
			window.status = msg;
			popupMessage(msg);
			alert(msg);
			forceLogout();
			window.location.reload(true);
		}
	}
	else
	{
		if(secondsRemain <= 300)
		{
			if(secondsRemain >= 60)
			{
				var minutesRemain = Math.round(secondsRemain/60);
				msg = "由於系統設有閒置期限, 您的連線作業將於 " + minutesRemain + " 分鐘後失效! 請在這段閒置期限內送出您的表單資料!";
				window.status = msg;
				popupMessage(msg);
			}
			else
			{
			    msg = "您的連線作業即將於 " + secondsRemain + " 秒鐘後失效...";
				window.status = msg;
				popupMessage(msg);
				window.clearInterval(intervalID);
				intCurrentInterval = secondsRemain*1000;
				intervalID=window.setInterval("countdownTimeout()",intCurrentInterval);
			}
		}
		else
		{
		    if (window.status.indexOf("連線作業") >= 0)
			{
				window.status = "";
			}
			if(intCurrentInterval != intSessionTimeoutDetectInterval && intervalID != null)
			{
				window.clearInterval(intervalID);
				intCurrentInterval = intSessionTimeoutDetectInterval;
				intervalID=window.setInterval("countdownTimeout()",intCurrentInterval);
			}
		}
	}
}

function enableCountdownTimeout()
{
	if(!isLogout())
	{
		//dbg("enableCountdownTimeout!");
		window.status="";
		if(intervalID!=null)
		{	
			try
			{
				window.clearInterval(intervalID);
			}
			catch(e)
			{
			}
			intervalID=null;
		}
		intCurrentInterval = intSessionTimeoutDetectInterval;
		intervalID=window.setInterval("countdownTimeout()",intCurrentInterval);
	}
}

function enableConfirmUnclosedChild()
{
	window.onbeforeunload = confirmUnclosedChild;
}

function getTopFrameContainer()
{
	var wndFrameContainer=undefined;
	var wnd = window;
	while(wnd != wnd.top)
	{
		if(wnd.isFrameContainer) wndFrameContainer = wnd;
		wnd=wnd.parent;
	}
	if(wnd.isFrameContainer) wndFrameContainer = wnd;
	return wndFrameContainer;
}

function getRootFrameContainer()
{
	var wnd = getTopFrameContainer();
	while(wnd != undefined && wnd.top.opener != undefined && !wnd.top.opener.closed && wnd.top.opener.getTopFrameContainer!=undefined)
	{
		wnd = wnd.top.opener.getTopFrameContainer();
	}
	if(wnd != undefined && wnd.isFrameContainer) return wnd;
	return undefined;
}

function initPage()
{
	window.status="";
	//dbg("window.opener=" + window.opener);
	var wndTopFrameContainer = getTopFrameContainer()
	if(window.isFrameContainer)
	{
		if(window == wndTopFrameContainer)
		{
			window.onunload = closeWindowHandler;
			//dbg("isFrameContainer!\nwindow.parent==undefined : " + (window.parent==undefined) + "\nwindow.name : " + (window.name) + "\nwindow.parent==window.top : " + (window.parent==window.top) + "\nwindow.parent==window : " + (window.parent==window)  + "\nwindow.location : " + window.location);
		}
	}
	else
	{
	
		//dbg("Not isFrameContainer!\nwindow.name : " + (window.name) + "\nwindow.opener==undefined : " + (window.opener==undefined) + "\nwindow.top.opener==undefined : " + (window.top.opener==undefined) + "\nwindow.parent==undefined : " + (window.parent==undefined) + "\nwindow.parent==window.top : " + (window.parent==window.top)  + "\nwindow.location : " + window.location);
		if(wndTopFrameContainer==undefined)
		{
			dbg("wndTopFrameContainer==undefined");
			var url=window.location.href;
			var threadID = getThreadID();
			if(threadID!=undefined  && threadID.length>0)
			{	
				if(url.indexOf("?")>=0)
				{
					url=url+"&_PAGESI="+threadID;
				}
				else
				{
					url=url+"?_PAGESI="+threadID;
				}
			}
			window.location = urlFrameControl + "?" + CS_QUERYSTRING_NAME_LAUNCH_PAGE + "=" + encodeURIComponent(url);
			return;
		}
		var wndRoot = getRootFrameContainer();
		if(wndRoot == undefined)
		{
			alert("Cannot getRootFrameContainer()");//impossible here!
			return;
		}
		if(wndTopFrameContainer==wndRoot)
		{
			window.status = "";
			if(isLogout())
			{
				wndTopFrameContainer.closeWindowHandler();
			}
			else
			{
				wndTopFrameContainer.enableCountdownTimeout();
				wndTopFrameContainer.enableConfirmUnclosedChild();
			}
		}
		setTitle();
		setDefaultFocus();
//		document.oncontextmenu = autoResizeWindow;
	}
	if(typeof(__doPostBack) != "undefined")
	{
		dotNet__doPostBack=__doPostBack;
		__doPostBack = ne__doPostBack;
	}
}

/*
功能：window.showModalDialog，只是Default帶ECA090的Header
參數：URL=呼叫的網頁
	  Width=網頁的寬度，空值Default[800]
	  Height=網頁的高度，空值Default[600]
	  WindowTitle=視窗上的標題
	  ButtonTitle=確定按鈕上的字眼，空值Default[確定]
	  CancelTitle=取消按鈕上的字眼，空值Default[取消]
範例：strRtn = ShowDialogWithHeader(URL, '600', '400', '測試視窗', '', '');
*/
function ShowDialogWithHeader(URL, Width, Height)
{
	var strRtn = '';
	var strurl = '';
	if (Width == '') Width = '800';
	if (Height == '') Height = '600';

	if (URL.toUpperCase().indexOf('PATH=') >= 0)
	    strurl = '../SC/SC0050.aspx' + Replace(URL, "'", "''")
	else
	    strurl = '../SC/SC0050.aspx?Path=' + Replace(Replace(URL, '&', '%26'), "'", "''");

	try
	{
		strRtn = window.showModalDialog(strurl, '', 'dialogHeight:' + Height + 'px; dialogWidth: ' + Width + 'px; edge: Raised;center: Yes; help: No; resizable: No; status: No;');
	}
	catch (ex)
	{
		strRtn = undefined;
	}
	
	//Mark by Chung 2007.07.04
	//if (strRtn == undefined) return;
	var objreturnValue = document.getElementById('__returnValue');
	if (objreturnValue != undefined)
	{
	    objreturnValue.value = strRtn;
	    var bolCallback = true;
	    //嘗試呼叫onModalReturn
	    try
	    {
	        bolCallback = onShowModalReturn(strRtn);
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

//給無須底層控制的回傳值的程式叫用
function ShowDialogWithHeader2(URL, Width, Height)
{
	var strRtn = '';
	var strurl = '';
	if (Width == '') Width = '800';
	if (Height == '') Height = '600';
	
	if (URL.toUpperCase().indexOf('PATH=') >= 0)
	    strurl = '../SC/SC0050.aspx' + Replace(URL, "'", "''")
	else
	    strurl = '../SC/SC0050.aspx?Path=' + Replace(Replace(URL, '&', '%26'), "'", "''");

	try
	{
		strRtn = window.showModalDialog(strurl, '', 'dialogHeight:' + Height + 'px; dialogWidth: ' + Width + 'px; edge: Raised;center: Yes; help: No; resizable: No; status: No;');
	}
	catch (ex)
	{
		strRtn = undefined;
	}
	
	return strRtn;
}

/*
功能：Redirect Page，僅提供功能轉換
參數：URL=呼叫的網頁
	  Width=網頁的寬度，空值Default[800]
	  Height=網頁的高度，空值Default[600]
	  WindowTitle=視窗上的標題
	  ButtonTitle=確定按鈕上的字眼，空值Default[確定]
	  CancelTitle=取消按鈕上的字眼，空值Default[取消]
範例：strRtn = ShowDialogWithHeader(URL, '600', '400', '測試視窗', '', '');
異動說明：
    2008.01.16 Update by Chung
        增加FunID的傳入。以("||")隔開，Format為FunID=XXXX。但使用split後，Length一定要為2
        範例：/AP/CC1400.aspx||FunID=CC2330
*/
function RedirectPage(URL)
{
	var strRtn = '';
	var strurl = '';
	var strFunID = '';
	var aryURL = new Array;

    if (URL.toUpperCase().indexOf('FUNID') >= 0)
    {
	    aryURL = URL.split('||');
	    if (aryURL.length == 2)
	    {
            for (var i=0; i<aryURL.length; i++)
            {
                if (aryURL[i].toString().substring(0, 5).toUpperCase() == 'FUNID')
                    strFunID = aryURL[i]
                else
                    URL = aryURL[i];
            }
	    }
	}

    if (strFunID == '')
	    strurl = '../SC/SC0050.aspx?Path=' + Replace(Replace(URL, '&', '%26'), "'", "''")
	else
	    strurl = '../SC/SC0050.aspx?' + strFunID + '&Path=' + Replace(Replace(URL, '&', '%26'), "'", "''");
	showSubmitting();
	window.parent.location = strurl;
	//window.top.frames[0].frames[1].frames[3].location = strurl;
}

/*
功能：Redirect Flow Page，提供功能轉換並顯示Flow Menu
參數：URL=呼叫的網頁
Width=網頁的寬度，空值Default[800]
Height=網頁的高度，空值Default[600]
WindowTitle=視窗上的標題
ButtonTitle=確定按鈕上的字眼，空值Default[確定]
CancelTitle=取消按鈕上的字眼，空值Default[取消]
範例：strRtn = ShowDialogWithHeader(URL, '600', '400', '測試視窗', '', '');
異動說明：
2008.01.16 Update by Chung
增加FunID的傳入。以("||")隔開，Format為FunID=XXXX。但使用split後，Length一定要為2
範例：/AP/CC1400.aspx||FunID=CC2330
*/
function RedirectFlowPage(URL) {
    var strUrl = '../SC/SC0050.aspx?Path=' + Replace(Replace(URL, '&', '%26'), "'", "''");

    showSubmitting();
    window.parent.parent.frames[1].location = '../WF/WFA010.aspx';
    window.parent.parent.frames[2].imgSplit_WorkFlow.src = "../images/mopen.png";
    window.parent.parent.frames[2].imgSplit_WorkFlow.alt = "Close WorkFlow menu";
    window.parent.parent.frames[2].document.bgColor = '#f1f1f1';
    window.parent.parent.frmSet.cols = "0,360,20,*";
    window.parent.parent.frames[2].imgSplit_WorkFlow.style.display = "";
    window.parent.location = strUrl;
}


/*
功能：置換字串
*/
function Replace(Src, FindStr, ReplaceStr)
{
	var Position;
	var RtnStr = '';
	var SrcLen = Src.length;
	var FindStrLen = FindStr.length;
	
	if (FindStrLen == 0)
		return Src;
	
	Position = Src.indexOf(FindStr, 0);
	while (Position >= 0)
	{
		RtnStr = RtnStr + Src.substring(0, Position + FindStrLen).replace(FindStr, ReplaceStr);
		if (Position == SrcLen)
			break
		else
		{
			Src = Src.substring(Position + FindStrLen, SrcLen)
			SrcLen = Src.length;
		}
		Position = Src.indexOf(FindStr, 0);
	}
	RtnStr = RtnStr + Src;
	
	return RtnStr;
}

/*
功能：window.showModalDialog
參數：URL=呼叫的網頁
	  Width=網頁的寬度，空值Default[800]
	  Height=網頁的高度，空值Default[600]
範例：strRtn = ShowDialog(URL, '600', '400')
*/
function ShowDialog(URL, Width, Height)
{
	var strRtn = '';
	if (Width == '') Width = '800';
	if (Height == '') Height = '600';
	
	try
	{
		strRtn = window.showModalDialog(URL, '', 'dialogHeight:' + Height + 'px; dialogWidth: ' + Width + 'px; edge: Raised;center: Yes; help: No; resizable: No; status: No;');
	}
	catch (ex)
	{
		strRtn = '';
	}
	
	return strRtn;
}

/*
功能：window.showModalDialog
參數：URL=呼叫的網頁
	  Width=網頁的寬度，空值Default[800]
	  Height=網頁的高度，空值Default[600]
範例：strRtn = ShowDialog(URL, '600', '400')
*/
function ShowDialogWithEvent(URL, Width, Height)
{
	var strRtn = '';
	if (Width == '') Width = '800';
	if (Height == '') Height = '600';
	
	try
	{
		strRtn = window.showModalDialog(URL, '', 'dialogHeight:' + Height + 'px; dialogWidth: ' + Width + 'px; edge: Raised;center: Yes; help: No; resizable: No; status: No;');
	}
	catch (ex)
	{
		strRtn = '';
	}
	
	var objreturnValue = document.getElementById('__returnValue');
	if (objreturnValue != undefined)
	{
	    objreturnValue.value = strRtn;
	    var bolCallback = true;
	    //嘗試呼叫onModalReturn
	    try
	    {
	        bolCallback = onShowModalReturn(strRtn);
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


function setTitle(sTitle)
{
    if (sTitle == '')
        window.top.document.title = 'HR管理系統'
    else
        window.top.document.title = 'HR管理系統-' + sTitle;
}

function OpenWindow(URL, WithHeader)
{
    if (WithHeader == undefined)
        window.open(URL, "", "channelmode=no,directories=no,height=550px,menubar=no,resizable=yes,toolbar=no,width=795px,scrollbars=yes");
    else
    {
        if (WithHeader == true)
        {
        	var strRtn = '';
	        var strurl = '';
	        var strFunID = '';
	        var aryURL = new Array;
        	
            if (URL.toUpperCase().indexOf('FUNID') >= 0)
            {
	            aryURL = URL.split('||');
	            if (aryURL.length == 2)
	            {
                    for (var i=0; i<aryURL.length; i++)
                    {
                        if (aryURL[i].toString().substring(0, 5).toUpperCase() == 'FUNID')
                            strFunID = aryURL[i]
                        else
                            URL = aryURL[i];
                    }
	            }
	        }

            if (strFunID == '')
	            strurl = '../SC/SC0050.aspx?Path=' + Replace(Replace(URL, '&', '%26'), "'", "''")
	        else
	            strurl = '../SC/SC0050.aspx?' + strFunID + '&Path=' + Replace(Replace(URL, '&', '%26'), "'", "''");
            window.open(strurl, "", "channelmode=no,directories=no,height=550px,menubar=no,resizable=yes,toolbar=no,width=795px,scrollbars=yes");
        }
        else
            window.open(URL, "", "channelmode=no,directories=no,height=550px,menubar=no,resizable=yes,toolbar=no,width=795px,scrollbars=yes");
    }
}
/*
	將傳入的字串前後空白去掉
*/
function gfunTrim(Src)
{
	return Src.replace(/^[\s]+/g, "").replace(/[\s]+$/g,"")
}

function enterToTab()
{
    if (window.event.keyCode == 13)
    {
        event.keyCode = 9;
        event.cancelBubble = true;
        return false;
    }
}

function wm(objTopDiv, objBottomDiv, msg)
{	
	var intLoop ;
	var intBase = 250;
	var strTmp  = "";
	var decH = objBottomDiv.offsetTop;
	var decT = parseInt(decH / intBase)  ; 

	if (decT < 1) 
		decT = 1;
		
	msg = Replace(msg, "$$$", "<BR>");

	
	for (intLoop=0; intLoop<=decT ; intLoop++)
	{
		if (decH > 100 + intBase * intLoop ) {
			strTmp = strTmp + "<P style=\"filter:alpha(opacity=20);Font-size:16pt;font-style:italic;font-weight:bold;color=Grey;z-index:-1;position:absolute;top:" 
				+ (50 + intBase * intLoop) + "px;left:50px\"><FONT face='新細明體'>" + msg + "</FONT></P>";
		}
	}
	objTopDiv.innerHTML = strTmp ;
}

function WaterMark(objTopDiv, objBottomDiv, msg, printFlag)
{	
	var intLoop ;
	var intBase = 250;
	var strTmp  = "";
	var decH = objBottomDiv.offsetTop;
	var decT = parseInt(decH / intBase); 
	var zindex = 1 ;

	if (decT < 1) 
		decT = 1;

    if (printFlag == "1")
        zindex = -1;

	msg = Replace(msg, "$$$", "<BR>");
	
	for (intLoop=0; intLoop<=decT ; intLoop++)
	{
		if (decH > 100 + intBase * intLoop ) {
			strTmp = strTmp + "<P style=\"filter:alpha(opacity=20);Font-size:16pt;font-style:italic;font-weight:bold;color=Grey;"
			    + "z-index:" + zindex  + ";position:absolute;top:" 
				+ (50 + intBase * intLoop) + "px;left:50px\"><FONT face='新細明體'>" + msg + "</FONT></P>";
		}
	}
	objTopDiv.innerHTML = strTmp ;
}

/*
功能：將傳入的字串格式化為三位一撇
*/
function currencyFormat(obj)
{
    var pnum = tellPoint(obj);
    //alert(pnum);
    var Src = Replace(obj.value, ",", "");
        
    var newStr="";  
    var intLen;
    var intPoint = Src.indexOf(',');
    var intPos = 0;
   
    if (intPoint >= 0)
        intLen = intPoint
    else
        intLen = Src.length;

    if (intLen <= 3)
        return Src;

    var b = intLen % 3;
    var iCount = Math.floor(intLen / 3);
    
    if (b == 0)
        b = 3;
    
    newStr = Src.substr(0, b) + ',';
    intPos = b;
    
    
    for (var i=0; i < iCount-1; i++)
    {
        intPos = b + i * 3 + 3;
        if (intPos == intLen)
            newStr = newStr + Src.substr(b + i * 3, 3)
        else
            newStr = newStr + Src.substr(b + i * 3, 3) + ',';
    }

    if (intPos != intLen)
    {
        if (b != 3)
            newStr = newStr + Src.substr(intPos, intLen - intPos);
    }
    
        
    obj.value = newStr;

}
/*
function movePoint()     
{     
	var pn = parseInt(pnum.value);     
	     
	if(isNaN(pn))     
		return;     
	     
	var rng = box.createTextRange(); 
		     
	rng.moveStart("character",pn);     
	     
	rng.collapse(true);	     
	     
	rng.select();     
}    

function tellPoint(obj)     
{     
	var rng = event.srcElement.createTextRange();	
     
	rng.moveToPoint(event.x,event.y);	     
	rng.moveStart("character",-event.srcElement.value.length);
	     
	return rng.text.length;
}     
*/
/*
功能：顯示訊息在Button列
建立人員：A02976
建立日期：2008.09.18
*/
function showInformation(strMsg)
{
    try
    {
        var objMsg = window.parent.frames[0].document.getElementById("txtMsg")
        objMsg.value = strMsg;
        objMsg.style.display = 'block';
        setTimeout('clearTitleMessage()', 15000);
    }
    catch(ex)
    {}
}

/*
功能：清除Button列的訊息
建立人員：A02976
建立日期：2008.09.18
*/
function clearTitleMessage()
{
    try
    {
        var objMsg = window.parent.frames[0].document.getElementById("txtMsg")
        objMsg.value = '';
        objMsg.style.display = 'none';        
    }
    catch(ex)
    {}
}

/*
功能：使用Jquery初始化GridViewStyle的Table
建立人員：Netix
建立日期：2013.04.18
*/
function ApplyGridViewStyle(arrGV) {
    if (typeof (arrGV) == 'string') arrGV = [arrGV];
    $.each(arrGV, function (index, msg) {
        var table = $('#' + msg);
        table.attr({ 'cellspacing': 0, 'cellpadding': 2, 'border': 1, 'class': 'GridViewStyle' }).css({ 'width': '100%', 'border-collapse': 'collapse' });
        $('#' + msg + ' thead th').attr('class', 'td_header');
        $('#' + msg + ' tbody > tr > td, #' + msg + ' tfoot > tr > td').attr('class', 'td_detail');
        $('#' + msg + ' tbody tr:even').attr('class', 'tr_evenline');
        $('#' + msg + ' tbody tr:odd').attr('class', 'tr_oddline');
        $('#' + msg + ' tbody tr').css('cursor', 'hand').hover(
                    function () { $(this).addClass('tr_hilite'); }, function () { $(this).removeClass('tr_hilite'); }).live('click', function () {
                        $(this).toggleClass('tr_selected');
                    });
    });
}