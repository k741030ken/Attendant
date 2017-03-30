/*********************
*  FormControl version 1.0
*  code by Netix, 2007.04
*********************/
    if(parseInt(navigator.appVersion.charAt(0)) >= 4) {
        isNS = (navigator.appName == "Netscape") ? true : false;  		
        isIE = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;  
    }
    webForm = document.forms[0];
    function IniChkRadioBtn(){
        try{
            for( j = 0 ; j < RadioCtrlList.length ; j++ ){
                arrTemp = RadioCtrlList[j].split(",");
                objRadio = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( chkRadioBtn(objRadio) != arrTemp[2] ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
                            objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
                    objInput.disabled = true;
                }
                for( k = 0 ; k < objRadio.length ; k++ ) objRadio[k].attachEvent("onclick",SyncRadio);
            }
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkCheckBox(){
        try{
            for( j = 0 ; j < CheckCtrlList.length ; j++ ){
                arrTemp = CheckCtrlList[j].split(",");
                objCheck = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( !objCheck.checked ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
                            objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
                    objInput.disabled = true;
                }
                objCheck.attachEvent("onclick",SyncCheck);
            }
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkEnable(){
        try{
            for( j = 0 ; j < EnableCtrlList.length ; j++ ){
                arrTemp = EnableCtrlList[j].split(",");
                objChk = eval("webForm."+arrTemp[0]);
                checkFlag = objChk.checked;
                objTarget = document.getElementById(arrTemp[1]);
                type = arrTemp[2];
	            switch(type){
	                case "2":
	                    for( k = 1 ; k < objTarget.childNodes.length ; k++ ){
	                        objChild = objTarget.childNodes[k];
	                        objChild.style.filter = (checkFlag)?"alpha(opacity=100)":"alpha(opacity=40)";
	                        for( i = 0 ; i < objChild.children.length ; i++ ){
                                if( !checkFlag ){
                                    switch( objChild.children[i].tagName ){
                                        case "INPUT":
                                            strType = objChild.children[i].type.toUpperCase();
                                            if( strType == "TEXT" || strType == "FILE" || strType == "PASSWORD" ){
		                                        objChild.children[i].value = "";
		                                    }else if( strType == "CHECKBOX" || strType == "RADIO" ){
		                                        objChild.children[i].checked = false;
		                                    }
                                            break;
                                        case "SELECT":
                                            objChild.children[i].selectedIndex = 0;
                                            break;
                                    }
                                }
                                objChild.children[i].disabled = (checkFlag)?false:true;
                            }
	                    }
		                break;
	                case "1":
                        objTarget.style.filter = (checkFlag)?"alpha(opacity=100)":"alpha(opacity=40)";
	                    for( i = 0 ; i < objTarget.children.length ; i++ ){
                            if( !checkFlag ){
                                switch( objTarget.children[i].tagName ){
                                    case "INPUT":
                                        strType = objTarget.children[i].type.toUpperCase();
	                                    if( strType == "TEXT" || strType == "FILE" || strType == "PASSWORD" ){
                                            objTarget.children[i].value = "";
                                        }else if( strType == "CHECKBOX" || strType == "RADIO" ){
                                            objTarget.children[i].checked = false;
                                        }
                                        break;
                                    case "SELECT":
                                        objTarget.children[i].selectedIndex = 0;
                                        break;
                                }
                            }
                            objTarget.children[i].disabled = (checkFlag)?false:true;
	                    }
		                break;
	            }
                objChk.attachEvent("onclick",EnableCtrl);
            }
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkDDLInput(){
        try{
            for( j = 0 ; j < DDLCtrlList.length ; j++ ){
                arrTemp = DDLCtrlList[j].split(",");
                objDDL = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( objDDL.selectedIndex == -1 || objDDL.options[objDDL.selectedIndex].value != arrTemp[2] ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
                            objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
                    objInput.disabled = true;
                }
                objDDL.attachEvent("onchange",NotifyDDL);
            }
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkTextArea(){
        //msgMask = "限<font color=red>{0}</font>個中文字之內，共輸入<font color=red>{1}</font>個字（ <input type=checkbox name='offcount'>關閉字數計算 ）";
        msgMask = "限<font color=red>{0}</font>個中文字之內，共輸入<font color=red>{1}</font>個字";
        try{
            for( j = 0 ; j < TextAreaCtrlList.length ; j++ ){
                objDIV = document.createElement("DIV");
                arrTemp = TextAreaCtrlList[j].split(",");
                objTxtArea = eval("webForm."+arrTemp[0]);
                if( objTxtArea ){
                    if( parseInt(arrTemp[1]) != -1 ){
                        counterHTML = msgMask;
                        counterHTML = counterHTML.replace( "{0}", arrTemp[1] );
                        if( isIE ){
                            counterHTML = counterHTML.replace( "{1}", "<span id='counter" + j + "'>0</span>" );
                        }
                        if( isNS ){
                            counterHTML = counterHTML.replace( "{1}", "<input readonly type=text size=3 maxlength=3 name=counter" + j + " value=0>" );
                        }
                        objDIV.innerHTML = counterHTML;
                        objTxtArea.parentElement.appendChild( objDIV );
                        objTxtArea.attachEvent( "onkeyup", Keychange );
                    }
                }
            }
            Keychange();
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkDistrict(){  
        try{
            for( j = 0 ; j < DistrictCtrlList.length ; j++ ){
                arrTemp = DistrictCtrlList[j].split(",");
                objCity = eval("webForm."+arrTemp[0]);
                objDistrict = eval("webForm."+arrTemp[1]);
                objDC = eval("webForm."+arrTemp[2]);
                objCity.attachEvent( "onchange", SyncCityDistrict );
                objDistrict.attachEvent( "onchange", SyncCityDistrict );
                objDC.attachEvent( "onchange", SyncCityDistrict );
                getCode( objCity, objDistrict, objDC );
            }
        }catch(err){
            alert(err.message);
        }
    }
    function IniChkInputMask(){
        try{
            for( j = 0 ; j < InputMaskCtrlList.length ; j++ ){
                arrTemp = InputMaskCtrlList[j].split(",");
                objInput = eval("webForm."+arrTemp[0]);
                strMask = arrTemp[1];
                objInput.attachEvent( "onfocus", ApplyMask );
                objInput.attachEvent( "onblur", ApplyMask );
                SyncMask();
            }
        }catch(err){
            alert(err.message);
        }
    }
    function SyncRadio(){
        objSrc = event.srcElement;
        for( j = 0 ; j < RadioCtrlList.length ; j++ ){
            if( RadioCtrlList[j].indexOf( objSrc.name + "," ) == 0 ){
                arrTemp = RadioCtrlList[j].split(",");
                objRadio = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( chkRadioBtn(objRadio) == arrTemp[2] ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "";
                            break;
                        case "SELECT":
                            break;
                    }
	                objInput.disabled = false;
	                objInput.focus();
                }else{
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
	                        objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
	                objInput.disabled = true;
                }
            }
        }
    }
    function SyncCheck(){
        objSrc = event.srcElement;
        for( j = 0 ; j < CheckCtrlList.length ; j++ ){
            if( CheckCtrlList[j].indexOf( objSrc.name + "," ) == 0 ){
                arrTemp = CheckCtrlList[j].split(",");
                objCheck = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( objCheck.checked ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "";
                            break;
                        case "SELECT":
                            break;
                    }
	                objInput.disabled = false;
	                objInput.focus();
                }else{
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
	                        objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
	                objInput.disabled = true;
                }
            }
        }
    }
    function NotifyDDL(){
        objSrc = event.srcElement;
        for( j = 0 ; j < DDLCtrlList.length ; j++ ){
            if( DDLCtrlList[j].indexOf( objSrc.name + "," ) == 0 ){
                arrTemp = DDLCtrlList[j].split(",");
                objDDL = eval("webForm."+arrTemp[0]);
                objInput = eval("webForm."+arrTemp[1]);
                if( objDDL.options[objDDL.selectedIndex].value == arrTemp[2] ){
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "";
                            break;
                        case "SELECT":
                            break;
                    }
	                objInput.disabled = false;
	                objInput.focus();
                }else{
                    switch( objInput.tagName ){
                        case "INPUT":
                            objInput.style.backgroundColor = "#F1F1F1";
	                        objInput.value = "";
                            break;
                        case "SELECT":
                            objInput.selectedIndex = -1;
                            break;
                    }
	                objInput.disabled = true;
                }
            }
        }
    }
    function chkRadioBtn(objRadio){
        res = -1;
        for( i = 0 ; i < objRadio.length ; i++ ) if( objRadio[i].checked ) res = objRadio[i].value;
        return res;
    }
    function Keychange() {
         for( j = 0 ; j < TextAreaCtrlList.length ; j++ ){
            arrTemp = TextAreaCtrlList[j].split(",");
            objTxtArea = eval("webForm."+arrTemp[0]);
            //if(webForm.offcount.checked ) return true;
            if( event.srcElement ){
                if( event.srcElement == objTxtArea ) objSrc = event.srcElement;
            }else{
                objSrc = objTxtArea;
            }
            if( objSrc != null ){
                var datalen = objSrc.value.length;
                objCounter = eval("document.all.counter"+j);
                if(isIE) objCounter.innerText=datalen;
                if(isNS) objCounter.value=datalen;
                objSrc = null;
            }
        }
    }
    function SyncCityDistrict(){
        objSrc = event.srcElement;
        for( j = 0 ; j < DistrictCtrlList.length ; j++ ){
            arrTemp = DistrictCtrlList[j].split(",");
            if( arrTemp[0] == objSrc.id || arrTemp[1] == objSrc.id ){
                objCity = eval("webForm."+arrTemp[0]);
                objDistrict = eval("webForm."+arrTemp[1]);
                objDC = eval("webForm."+arrTemp[2]);
                getCode( objCity, objDistrict, objDC );
            }
        }
    }
    function getCode( city, district, code ){
		var Index = city.selectedIndex;
		if( (code.value == "") || (event.type == "change") ){
			if( Index != 0 ){
				var newNode = eval("Option" + Index);
				district.options.length = newNode.length;
				for( j = 0 ; j < newNode.length ; j++ ){
					temp = newNode[j].split(',');
					district.options[j].text = temp[0];
					district.options[j].value = temp[1];
				}
			}else{
				district.options.length = 1;
				district.options[0].text = "鄉鎮市區";
				district.options[0].value = "";
			}
			code.value = district.value;
		}else{
			Index = 1;
			while( Index <= 25 ){
				newNode = eval("Option" + Index);
				for( i = 0 ; i < newNode.length ; i++ ){
					temp = newNode[i].split(',');
					if( temp[1] == code.value ){
						city.selectedIndex = Index;
						district.options.length = newNode.length;
						for( j = 0 ; j < newNode.length ; j++ ){
							temp = newNode[j].split(',');
							district.options[j].text = temp[0];
							district.options[j].value = temp[1];
							if( temp[1] == code.value ) district.options[j].selected = true;
						}
						break;
					}
				}
				Index++;
			}
		}
	}
	function SyncMask(){
         for( j = 0 ; j < InputMaskCtrlList.length ; j++ ){
            arrTemp = InputMaskCtrlList[j].split(",");
            objInput = eval("webForm."+arrTemp[0]);
            strMask = arrTemp[1];
            if( event.srcElement ){
                if( event.srcElement == objInput ) objSrc = event.srcElement;
            }else{
                objSrc = objInput;
            }
            if( objSrc != null ){
                var datalen = objSrc.value.length;
                if( datalen == 0 ){
                    objSrc.value = strMask;
                    objSrc.style.color = "#999999";
                }
                objSrc = null;
            }
        }
	}
	function ApplyMask(){
        for( j = 0 ; j < InputMaskCtrlList.length ; j++ ){
            arrTemp = InputMaskCtrlList[j].split(",");
            objInput = eval("webForm."+arrTemp[0]);
            strMask = arrTemp[1];
            if( objInput == event.srcElement ){
                if( objInput.value == strMask && event.type == "focus" ){
                    objInput.value = "";
                    objInput.style.color = "#000000";
                }else if( objInput.value == "" && event.type == "blur" ){
                    objInput.value = strMask;
                    objInput.style.color = "#999999";
                }
            }
        }
	}
	function EnableCtrl(){
	    var objSrc = event.srcElement;
        for( j = 0 ; j < EnableCtrlList.length ; j++ ){
            if( EnableCtrlList[j].indexOf( objSrc.name + "," ) == 0 ){
                arrTemp = EnableCtrlList[j].split(",");
                objChk = eval("webForm."+arrTemp[0]);
                checkFlag = objChk.checked;
                objTarget = document.getElementById(arrTemp[1]);
                type = arrTemp[2];
	            switch(type){
	                case "2":
	                    for( k = 1 ; k < objTarget.childNodes.length ; k++ ){
	                        objChild = objTarget.childNodes[k];
	                        objChild.style.filter = (checkFlag)?"alpha(opacity=100)":"alpha(opacity=40)";
	                        for( i = 0 ; i < objChild.children.length ; i++ ){
                                if( !checkFlag ){
                                    switch( objChild.children[i].tagName ){
                                        case "INPUT":
                                            strType = objChild.children[i].type.toUpperCase();
                                            if( strType == "TEXT" || strType == "FILE" || strType == "PASSWORD" ){
		                                        objChild.children[i].value = "";
		                                    }else if( strType == "CHECKBOX" || strType == "RADIO" ){
		                                        objChild.children[i].checked = false;
		                                    }
                                            break;
                                        case "SELECT":
                                            objChild.children[i].selectedIndex = 0;
                                            break;
                                    }
                                }
                                objChild.children[i].disabled = (checkFlag)?false:true;
                            }
	                    }
		                break;
	                case "1":
                        objTarget.style.filter = (checkFlag)?"alpha(opacity=100)":"alpha(opacity=40)";
	                    for( i = 0 ; i < objTarget.children.length ; i++ ){
                            if( !checkFlag ){
                                switch( objTarget.children[i].tagName ){
                                    case "INPUT":
                                        strType = objTarget.children[i].type.toUpperCase();
	                                    if( strType == "TEXT" || strType == "FILE" || strType == "PASSWORD" ){
                                            objTarget.children[i].value = "";
                                        }else if( strType == "CHECKBOX" || strType == "RADIO" ){
                                            objTarget.children[i].checked = false;
                                        }
                                        break;
                                    case "SELECT":
                                        objTarget.children[i].selectedIndex = 0;
                                        break;
                                }
                            }
                            objTarget.children[i].disabled = (checkFlag)?false:true;
	                    }
		                break;
	            }
            }
        }
	}
