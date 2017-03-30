//IE 不支援動態調整 onbeforeunload 內容，只好利用自訂變數輔助判讀 2015.06.29
var Util_IsChkDirty = true;
//儲存or還原下拉功能表目前位置 2014.01.16
var Util_CurrSelectIndex;
function Util_SaveCurrSelectIndex(objID) {
    oID = document.getElementById(objID);
    if (oID != null) Util_CurrSelectIndex = oID.selectedIndex;
}

function Util_ResetCurrSelectIndex(objID) {
    oID = document.getElementById(objID);
    if (oID != null) oID.selectedIndex = Util_CurrSelectIndex;
}

//利用alertify.js強化 alert() 2014.03.12
//若有指定 URL，則會進行轉址(可為 self 或是 top)
function Util_Alert(strHtmlMsg, strURL, IsRootYN) {
    strHtmlMsg = strHtmlMsg + '<br /><br />';
    if (strURL.length == 0) {
        alertify.alert(strHtmlMsg);
    }
    else {
        alertify.alert(strHtmlMsg, function (e) { if (IsRootYN == 'Y') { top.location.href = strURL; } else { location.href = strURL; } });
    }
}

//若頁面有使用 Validator 物件，可用此函數顯示自訂頁面檢核失敗提醒訊息 2014.12.23
//*可利用 Util.setJS_AlertPageNotValid　進行設定
//*增加傳回檢核值 2016.09.29
function Util_AlertPageNotValid(NotValidMsg) {
    if (typeof (Page_ClientValidate) == 'function') {
        Page_ClientValidate();
    }
    if (!Page_IsValid) {
        if (NotValidMsg == '') {
            alert(JS_Alert_PageNotValid);
        }
        else {
            alert(NotValidMsg);
        }
        return false;
    }
    return true;
}

//利用alertify.js強化 confirm() 2014.03.10
//因為行為模式與正常 JS 的 confirm不同，故需分段處理以便 ASP.NET能正常運作
function Util_Confirm(objID, strHtmlMsg) {
    oID = document.getElementById(objID);
    if (oID != null) {
        alertify.confirm(strHtmlMsg, function (e) {
            if (e) {
                oID.onclick = '';
                oID.click();
            }
            else
                return false;
        });
    }
    return false;
}

//顯示時鐘 2015.03.13 新增
//可自訂時鐘前、後方字串內容
// ex: Util_SetClock('jsClock','Curr Time : ',' IP:xxx.xxx.xxx.xxx');
//* 可利用 Util.setJS_DispDateTime　呼叫
function Util_SetClock(objID, Prefix, Suffix) {
    if (objID == null) return;
    if (Prefix == null) Prefix = '';
    if (Suffix == null) Suffix = '';

    oID = document.getElementById(objID);
    if (oID == null) return;

    var today = new Date();
    var h = today.getHours();
    var m = today.getMinutes();
    var s = today.getSeconds();
    m = (m < 10) ? '0' + m : m;
    s = (s < 10) ? '0' + s : s;

    oID.innerHTML = Prefix + h + ':' + m + ':' + s + Suffix;
    var t = setTimeout(function () { Util_SetClock(objID, Prefix, Suffix) }, 500);
}

//自訂物件 CSS 的 Class 名稱  2015.03.18
//ex: Util_SetCssClass('tabHold,tabBank', 'tabActClass')
//* 可利用 Util.setJS_SetCssClass　呼叫
function Util_SetCssClass(strIDList, strClassName) {
    var oList = strIDList.split(',');
    for (idx = 0; idx < oList.length; idx++) {
        oID = document.getElementById(oList[idx]);
        if (oID != null) {
            oID.className = strClassName;
        }
    }
}

//擴充 JavaScript，類似 c# string.format()  2014.03.07
//ex: var strMsg = String.format("Hello , {0} {1}","早安","小明")
String.format = function (src) {
    if (arguments.length == 0) return null;
    var args = Array.prototype.slice.call(arguments, 1);
    return src.replace(/\{(\d+)\}/g, function (m, i) {
        return args[i];
    });
};

//擴充 JavaScript ，類似 jquery 的 document.ready()  2013.08.06
// http://www.cnblogs.com/rubylouvre/archive/2009/12/30/1635645.html
//ex:  dom.Ready(function(){ alert('dom Ready！')});
new function () {
    dom = [];
    dom.isReady = false;
    dom.isFunction = function (obj) {
        return Object.prototype.toString.call(obj) === "[object Function]";
    }
    dom.Ready = function (fn) {
        dom.initReady();
        if (dom.isFunction(fn)) {
            if (dom.isReady) {
                fn();
            } else {
                dom.push(fn);
            }
        }
    }
    dom.fireReady = function () {
        if (dom.isReady) return;
        dom.isReady = true;
        for (var i = 0, n = dom.length; i < n; i++) {
            var fn = dom[i];
            fn();
        }
        dom.length = 0;
    }
    dom.initReady = function () {
        if (document.addEventListener) {
            document.addEventListener("DOMContentLoaded", function () {
                document.removeEventListener("DOMContentLoaded", arguments.callee, false);
                dom.fireReady();
            }, false);
        } else {
            if (document.getElementById) {
                document.write("<script id=\"ie-domReady\" defer='defer'src=\"//:\"><\/script>");
                document.getElementById("ie-domReady").onreadystatechange = function () {
                    if (this.readyState === "complete") {
                        dom.fireReady();
                        this.onreadystatechange = null;
                        this.parentNode.removeChild(this)
                    }
                };
            }
        }
    }
}

//亂數產生器，可用在發動 Ajax 時， Url 後綴加上亂數值，避開 IE 特有的 Cache 問題
//2017.02 改使用 seedrandom.min.js 產生
//** min / max 為選擇性參數 **
function Util_GetRandom(min, max) {
    rnd = Math.seedrandom();
    if (typeof (min) == 'undefined' || typeof (max) == 'undefined')
        return rnd;
    else
        return Math.floor(rnd * (max - min + 1) + min);
}

//強化並修正原來JavaScript IsNumeric() 的Bug  2013.08.07
function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

//切換指定物件 ID 的顯示與否
function Util_ToggleDisplay(objID) {
    oID = document.getElementById(objID);
    if (oID == null) {
        return;
    }
    else {
        if (oID.style.display == 'none')
            oID.style.display = '';
        else
            oID.style.display = 'none';
    }
}

//自動設定 iFrame 的高度，將此函數放到 iframe 的 onload 事件即可
function Util_SetFrameHeight(frmID) {
    var oID = document.getElementById(frmID);
    if (oID != null && !window.opera) {
        if (oID.contentDocument && oID.contentDocument.body.offsetHeight) {
            oID.height = oID.contentDocument.body.offsetHeight + 20;
        } else if (oID.Document && oID.Document.body.scrollHeight) {
            oID.height = oID.Document.body.scrollHeight + 20;
        }
    }
}

//列印特定區塊(解決IE列印Bug)
//動態加入內建 CSS 2014.06.20
function Util_PrintBlock(objID) {
    oID = document.getElementById(objID);
    if (oID != null) {
        var value = oID.innerHTML;
        var printPage = window.open("", "printPage", "top=100,left=100,width=320,height=160,fullscreen=0,scrollbars=0,resizable=1,menubar=no,location=no");
        printPage.document.open();
        printPage.document.write("<HTML><head></head><BODY onload='window.print();window.close()'>");
        printPage.document.write('<link rel="stylesheet" href="' + JS_Util_CSSUrl + '">');
        //printPage.document.write("<PRE>");
        printPage.document.write(value);
        //printPage.document.write("</PRE>");
        printPage.document.close("</BODY></HTML>");
    }
}

//列印特定iFrame(解決IE列印Bug)
function Util_PrintFrame(objFrameID) {
    oID = document.getElementById(objFrameID);
    if (oID != null && oID.contentWindow) {
        oID.contentWindow.focus();
        oID.contentWindow.print();
    } else {
        alert('[PrintFrame] for IE Only');
    }
}

//置中彈出視窗扣除指定像素後的 Top 座標  2015.06.24
function Util_getPopTop(h) {
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;
    height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;
    var top = ((height / 2) - (h / 2)) + dualScreenTop
    return top;
}

//置中彈出視窗扣除指定像素後的 Left 座標  2015.06.24
function Util_getPopLeft(w) {
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    return left;
}

//取出瀏覽器的視窗寬度
function Util_getWidth() {
    if (self.innerWidth) {
        return self.innerWidth;
    }
    else if (document.documentElement && document.documentElement.clientHeight) {
        return document.documentElement.clientWidth;
    }
    else if (document.body) {
        return document.body.clientWidth;
    }
    return 0;
}

//取出瀏覽器的視窗高度
function Util_getHeight() {
    if (self.innerWidth) {
        return self.innerHeight;
    }
    else if (document.documentElement && document.documentElement.clientHeight) {
        return document.documentElement.clientHeight;
    }
    else if (document.body) {
        return document.body.clientHeight;
    }
    return 0;
}

//根據指定核取方塊決定指定物件的顯示與否(被選取才顯示)
function Util_ChkBoxToggleDisplay(chkID, objID) {
    cID = document.getElementById(chkID);
    oID = document.getElementById(objID);
    if (cID == null || oID == null) {
        return;
    }
    else {
        if (cID.checked)
            oID.style.display = '';
        else
            oID.style.display = 'none';
    }
}

//根據指定核取方塊決定指定物件的可見與否(被選取才可見)
function Util_ChkBoxToggleVisibility(chkID, objID) {
    cID = document.getElementById(chkID);
    oID = document.getElementById(objID);
    if (cID == null || oID == null) {
        return;
    }
    else {
        if (cID.checked)
            oID.style.visibility = '';
        else
            oID.style.visibility = 'hidden';
    }
}

//根據主核取方塊決定子核取方塊是否選取
function Util_ChkBoxToggleChecked(chkID1, chkID2) {
    srcID = document.getElementById(chkID1);
    tarID = document.getElementById(chkID2);
    if (srcID == null || tarID == null) {
        return;
    }
    else {
        if (tarID.checked != srcID.checked) {
            tarID.click();
        }
    }
}

//檢查空白(可使用在物件的 OnClientClick 屬性)
function Util_ChkEmpty(objID, EmptyErrMsg) {
    oID = document.getElementById(objID);
    if (oID == null)
        return false;
    else {
        if (oID.value.length <= 0) {
            alert(EmptyErrMsg);
            return false;
        }
    }
    return true;
}

//檢查最大輸入字數
//* 可利用 Util.setJS_ChkMaxLength　呼叫
function Util_ChkMaxLength(objID, MaxLen) {
    oID = document.getElementById(objID);
    if (oID == null) return;

    if (oID.value.length > MaxLen) {
        //alert('最多輸入 [' + MaxLen + '] 字');
        alert(String.format(JS_Alert_MaxLength, MaxLen));
        oID.value = oID.value.substring(0, MaxLen);
    }
}

//顯示並檢查已輸入/可輸入最大字數
//* 可利用 Util.setJS_DisplayChkMaxLength　呼叫
function Util_DisplayChkMaxLength(dispObjID, chkObjID, chkMaxLen) {
    oDispID = document.getElementById(dispObjID);
    oChkID = document.getElementById(chkObjID);
    if (oDispID == null) return;
    if (oChkID == null) return;
    Util_ChkMaxLength(chkObjID, chkMaxLen)
    oDispID.innerHTML = String.format(JS_Msg_DispInputMaxWords, oChkID.value.length, chkMaxLen);
}

//強制設值
function Util_SetValue(toID, toValue, IsParentYN) {
    if (toID == '') return;
    if (toValue == null) return;
    if (IsParentYN == 'Y')
        var toObj = window.parent.document.getElementById(toID);
    else
        var toObj = document.getElementById(toID);

    if (toObj != null) {
        toObj.value = toValue;
    }
}

//強迫只能輸入數字(可指定小數位數) 2014.03.04
//例：<input type='text' id='txt01' onkeyup='return Util_ChkNumber('txt01',2)'>
function Util_ChkNumber(pID, pLen) {
    oID = document.getElementById(pID);
    if (oID == null) return;
    var patt = '';
    if (pLen <= 0) {
        patt = '\\d+';
    }
    else {
        patt = '\\d+[.]?[0-9]{0,' + pLen + '}';
    }
    newValue = new RegExp(patt).exec(oID.value);
    if (newValue != null) {
        oID.value = newValue;
    }
    else {
        oID.value = '';
    }
    return false;
}


//複製 fmID 物件的值到 toID 物件 ，若IsParent=true ，則 toID 為上層物件
function Util_CopyTextBox(fmID, toID, IsParentYN) {
    if (fmID == null || fmID == '') return;
    if (toID == null || toID == '') return;
    var fmObj = document.getElementById(fmID)
    if (IsParentYN == 'Y')
        var toObj = window.parent.document.getElementById(toID);
    else
        var toObj = document.getElementById(toID);

    if (fmObj != null && toObj != null) {
        toObj.value = fmObj.value;
    }
}

//2014.08.29 新增
//將「清單字串」項目數量塞到指定ID
function Util_GetTextBoxStringListQtyToTextBox(fmID, toID, sepChar) {
    if (sepChar == null) sepChar = ',';
    oFmID = document.getElementById(fmID);
    oToID = document.getElementById(toID);
    if (oFmID != null && oToID != null) {
        if (oFmID.value == "")
            oToID.value = 0;
        else
            oToID.value = oFmID.value.split(sepChar).length;
    }
}

//2015.02.26 新增
//指定物件 前景/背景色 閃爍效果
//* 可利用 Util.setJS_Blink　呼叫
function Util_SetBlink(ID, IsBackgroundYN, color1, color2, timer) {
    obj = document.getElementById(ID);
    if (obj != null) {
        if (IsBackgroundYN == 'Y')
            obj.style.background = color1;
        else
            obj.style.color = color1;

        setTimeout("Util_SetBlink('" + ID + "','" + IsBackgroundYN + "','" + color2 + "','" + color1 + "'," + timer + ")", timer);
    }
}

//============ ListBox 相關  Begin =======================
// 改變 CheckBoxlist 選擇項目的 CSS 並將其複製到 ListBoxID 
// 其中 ListBoxID 是在前端顯示已選項目內容(Optional)
// 若有定義 ListBoxID ，可加上 Button 來控制其項目的移動
// 譬如： <input type="button" value="UP" onclick="Util_ListBoxItemMove('idListBox','-');" />
//        <input type="button" value="DN" onclick="Util_ListBoxItemMove('idListBox','+');" />
function Util_ChkBoxListSelectedItem(ChkBoxListID, ChkBoxListItemQty, ListBoxID) {
    var objCtrl = document.getElementById(ChkBoxListID);
    var objList = document.getElementById(ListBoxID);
    if (objCtrl == null) {
        return;
    }
    if (objList != null) {
        objList.options.length = 0;
    }

    for (i = 0; i < ChkBoxListItemQty; i++) {
        var objItem = document.getElementById(ChkBoxListID + '_' + i);
        var isCheck = objItem.checked;
        if (isCheck == true) {
            objItem.parentNode.parentNode.className = 'Util_ChkBoxListSelectedItem';
            if (objList != null) {
                objList.options[objList.options.length] = new Option(objItem.parentNode.childNodes[1].innerHTML, objItem.value);
            }
        }
        else {
            objItem.parentNode.parentNode.className = '';
        }
    }
}

//2014.08.29 新增
//將 ChkBoxList[選取]項目的 value 及 text，[取代] TargetValueID / TargetTextID 現值，若 IsParentYN = Y ，則為上層物件
function Util_GetChkBoxListSelectedItemToTextBox(ChkBoxListID, ChkBoxListItemQty, TargetValueID, TargetTextID, IsParentYN) {
    var oChkList = document.getElementById(ChkBoxListID);
    var oValue = null;
    var oText = null;
    if (IsParentYN == 'Y') {
        if (TargetValueID.length > 0) {
            oValue = window.parent.document.getElementById(TargetValueID);
        }
        if (TargetTextID.length > 0) {
            oText = window.parent.document.getElementById(TargetTextID);
        }
    }
    else {
        var oValue = document.getElementById(TargetValueID);
        var oText = document.getElementById(TargetTextID);
    }

    var strValueList = "";
    var strTextList = "";
    if (oChkList != null) {
        for (i = 0; i < ChkBoxListItemQty; i++) {
            var objItem = document.getElementById(ChkBoxListID + '_' + i);
            var isCheck = objItem.checked;
            if (isCheck == true) {
                if (strValueList.length > 0) strValueList += ",";
                if (strTextList.length > 0) strTextList += ",";
                strValueList += objItem.value;
                strTextList += objItem.parentNode.childNodes[1].innerHTML;
            }
        }
        if (oValue != null) oValue.value = strValueList;
        if (oText != null) oText.value = strTextList;
    }
}

//2014.09.01 新增
//根據已選清單值，反推 ChkBoxList 需[選取]的項目(若有SourceTextID參數，代表也要反推清單文字欄位)
function Util_SetChkBoxListSelectedItemFromTextBox(SourceValueID, ChkBoxListID, ChkBoxListItemQty, TargetTextID) {
    var oSource = document.getElementById(SourceValueID);
    var oChkList = document.getElementById(ChkBoxListID);
    var oText = document.getElementById(TargetTextID);

    if (oSource != null && oChkList != null) {
        if (oSource.value == "") {
            for (i = 0; i < ChkBoxListItemQty; i++) {
                var objItem = document.getElementById(ChkBoxListID + '_' + i);
                objItem.checked = false;
                objItem.selected = false;
                objItem.parentNode.className = '';
            }

            if (oText != null) {
                oText.value = "";
            }
        }
        else {
            var oSourceList = oSource.value.split(',');
            var strTextList = "";
            for (i = 0; i < ChkBoxListItemQty; i++) {
                var objItem = document.getElementById(ChkBoxListID + '_' + i);
                if (oSourceList.indexOf(objItem.value) >= 0) {
                    objItem.checked = true;
                    objItem.selected = true;
                    objItem.parentNode.parentNode.className = 'Util_ChkBoxListSelectedItem';

                    if (strTextList.length > 0) strTextList += ",";
                    strTextList += objItem.parentNode.childNodes[1].innerHTML;
                }
                else {
                    objItem.checked = false;
                    objItem.selected = false;
                    objItem.parentNode.parentNode.className = '';
                }
            }

            if (oText != null) {
                oText.value = strTextList;
            }
        }
    }
}

// 根據搜尋字串過濾出符合的 CheckBoxlist 項目 
function Util_ChkBoxListItemSearch(ChkBoxListID, ChkBoxListItemQty, ChkTextBoxID) {
    var objCtrl = document.getElementById(ChkBoxListID);
    var ChkText = document.getElementById(ChkTextBoxID).value;
    for (i = 0; i < ChkBoxListItemQty; i++) {

        var objItem = document.getElementById(ChkBoxListID + '_' + i);
        var objRow = objItem.parentNode.parentNode; //取出項目所在的 [TR] 2014.09 改寫

        if (ChkText.length > 0) {
            objRow.style.display = 'none';
            if (objItem.parentNode.childNodes[1] != null) {
                var chkItemText = objItem.parentNode.childNodes[1].innerHTML;
                //2015.01.22 改為不分大小寫
                if (chkItemText.search(new RegExp(ChkText, 'i')) >= 0) {
                    objRow.style.display = '';
                }
            }
        }
        else {
            objRow.style.display = '';
        }
    }
}


// 全選 CheckBoxlist 項目，改變其CSS 並複製到 ListBoxID 
function Util_ChkBoxListSelectAllItem(ChkBoxListID, ChkBoxListItemQty, ListBoxID) {
    var objCtrl = document.getElementById(ChkBoxListID);
    var objList = document.getElementById(ListBoxID);
    if (objCtrl == null) {
        return;
    }
    if (objList != null) {
        //for (i = objList.length - 1; i >= 0; i--) objList.remove(i);
        objList.options.length = 0;
    }

    for (i = 0; i < ChkBoxListItemQty; i++) {
        var objItem = document.getElementById(ChkBoxListID + '_' + i);
        //加入判斷，只有顯示中的項目才可加入 2013.12.09
        if (objItem.parentNode.style.display == '') {
            objItem.checked = true;
            objItem.selected = true;
            objItem.parentNode.parentNode.className = 'Util_ChkBoxListSelectedItem';
            if (objList != null) {
                objList.options[objList.options.length] = new Option(objItem.parentNode.childNodes[1].innerHTML, objItem.value);
            }
        }
    }
}


// 全部取消 CheckBoxlist 項目選擇，並還原CSS及ListBoxID
function Util_ChkBoxListClearAllItem(ChkBoxListID, ChkBoxListItemQty, ListBoxID) {
    var objCtrl = document.getElementById(ChkBoxListID);
    var objList = document.getElementById(ListBoxID);
    if (objCtrl == null) {
        return;
    }
    if (objList != null) {
        objList.options.length = 0;
    }

    for (i = 0; i < ChkBoxListItemQty; i++) {
        var objItem = document.getElementById(ChkBoxListID + '_' + i);
        objItem.checked = false;
        objItem.selected = false;
        objItem.parentNode.parentNode.className = '';
    }
}


// 清空 ListBoxID 所有已選項目，並還原 ChkBoxListID 及其 CSS
function Util_ChkBoxListClearAllSelectedItem(ChkBoxListID, ChkBoxListItemQty, ListBoxID) {
    var objCtrl = document.getElementById(ChkBoxListID);
    var objList = document.getElementById(ListBoxID);
    if (objCtrl == null) {
        return;
    }
    if (objList != null) {
        var arRemove = new Array();
        var arIndex = 0;
        for (i = objList.length - 1; i >= 0; i--) {
            if (objList.options[i].selected) {
                arRemove[arIndex] = objList.options[i].value;
                arIndex++;
                objList.remove(i);
            }
        }

        for (i = 0; i < ChkBoxListItemQty; i++) {
            var objItem = document.getElementById(ChkBoxListID + '_' + i);
            var arIndex = arRemove.indexOf(objItem.value);
            if (arIndex != -1) {
                objItem.checked = false;
                objItem.selected = false;
                objItem.parentNode.className = '';
            }
        }

    }
    return;
}

//控制ListBox內項目的上下移動  2014.03.14 修正可一次移動多個項目
// 譬如： <input type="button" value="UP" onclick="Util_ListBoxItemMove('idListBox','-');" />
//        <input type="button" value="DN" onclick="Util_ListBoxItemMove('idListBox','+');" />
function Util_ListBoxItemMove(listID, direction) {
    var element = document.getElementById(listID);
    if (element.type == 'select-multiple' || element.type == 'select-one') {
        //MoveUp
        if (direction == '-') {
            for (var i = 0; i < element.options.length; i++) {
                if (element.options[i].selected) {
                    if (i > 0 && element.options[i - 1].selected != true) {
                        Util_SwapListBoxItem(listID, i, i - 1);
                    }
                }
            }
        } //MoveUp

        //MoveDown
        if (direction == '+') {
            for (var i = element.options.length - 1; i >= 0; i--) {
                if (element.options[i].selected) {
                    if (i < (element.options.length - 1) && !element.options[i + 1].selected) {
                        Util_SwapListBoxItem(listID, i, i + 1);
                    }
                }
            }
        } //MoveDown

    } //element.type
}

//Swap ListItem
function Util_SwapListBoxItem(listID, index1, index2) {
    var element = document.getElementById(listID);
    // Make sure the indexes are valid
    if (index1 != index2 &&
        index1 >= 0 && index1 < element.options.length &&
        index2 >= 0 && index2 < element.options.length) {

        // Save the selection state of all of the options because Opera
        // seems to forget them when we click the button
        var optionStates = new Array();
        for (i = 0; i < element.options.length; i++) {
            optionStates[i] = element.options[i].selected;
        }

        // Save the first option into a temporary variable
        var option = element.options[index1];

        // Copy the second option into the first option's place
        element.options[index1] =
            new Option(element.options[index2].text,
                       element.options[index2].value,
                       element.options[index2].defaultSelected,
                       element.options[index2].selected);

        // Copy the first option into the second option's place
        element.options[index2] =
            new Option(option.text,
                       option.value,
                       option.defaultSelected,
                       option.selected);

        // Reset the selection states for Opera's benefit
        for (i = 0; i < element.options.length; i++) {
            element.options[i].selected = optionStates[i];
        }

        // Then select the ones we swapped, if they were selected before the swap
        element.options[index1].selected = optionStates[index2];
        element.options[index2].selected = optionStates[index1];
    }
} // swapOptions()


//清空 ListBoxID 所有已選項目
function Util_ClearAllSelectedItem(ListBoxID) {
    var objList = document.getElementById(ListBoxID);
    if (objList != null) {
        for (i = objList.length - 1; i >= 0; i--) {
            if (objList.options[i].selected) objList.remove(i);
        }
    }
}

//將項目複製到指定物件，並可設定指定物件的最大項目數量
function Util_CopyItem(SourceID, TargetID, IsAllowMultiSelectYN) {
    var i, j, si, ti;
    var objSource = document.getElementById(SourceID);
    var objTarget = document.getElementById(TargetID);
    if (objSource != null && objTarget != null) {
        for (si = 0; si < objSource.length; si++) {
            if (objSource.options[si].selected && objSource.options[si].value != "") {
                //若為空白或重複則不加入
                var isAdd = true;
                for (ti = 0; ti < objTarget.length; ti++) {
                    if (objTarget.options[ti].value == objSource.options[si].value)
                        isAdd = false;
                }
                if (isAdd) {
                    if (IsAllowMultiSelectYN == 'N' && objTarget.length >= 1) {
                        //alert("此處限制只能單選");
                        alert(JS_Alert_OnlyChooseOne);
                    }
                    else {
                        ti = objTarget.length++;
                        objTarget.options[ti].value = objSource.options[si].value;
                        objTarget.options[ti].text = objSource.options[si].text;
                    }
                } else {
                    //alert("資料重複選擇");
                    alert(JS_Alert_RepeatChoose);
                }

            }
        }
    }
}

//將 ListBox[所有]項目的 value 及 text，[取代] TargetValueID / TargetTextID 現值，若 IsParentYN = Y ，則為上層物件
function Util_GetListBoxAllItemToTextBox(SourceListBoxID, TargetValueID, TargetTextID, IsParentYN) {
    var oList = document.getElementById(SourceListBoxID);
    var oValue = null;
    var oText = null;
    if (IsParentYN == 'Y') {
        if (TargetValueID.length > 0) {
            oValue = window.parent.document.getElementById(TargetValueID);
        }
        if (TargetTextID.length > 0) {
            oText = window.parent.document.getElementById(TargetTextID);
        }
    }
    else {
        var oValue = document.getElementById(TargetValueID);
        var oText = document.getElementById(TargetTextID);
    }

    var strValueList = "";
    var strTextList = "";
    if (oList != null) {
        var items = oList.options;
        for (i = 0; i < items.length; i++) {
            if (strValueList.length > 0) strValueList += ",";
            if (strTextList.length > 0) strTextList += ",";
            strValueList += items[i].value;
            strTextList += items[i].text;
        }
        if (oValue != null) oValue.value = strValueList;
        if (oText != null) oText.value = strTextList;
    }
}

//將 ListBox[選取]項目的 value 及 text，[取代] TargetValueID / TargetTextID 現值，若 IsParentYN = Y ，則為上層物件
function Util_GetListBoxSelectedItemToTextBox(SourceListBoxID, TargetValueID, TargetTextID, IsParentYN) {
    var oList = document.getElementById(SourceListBoxID);
    var oValue = null;
    var oText = null;
    if (IsParentYN == 'Y') {
        if (TargetValueID.length > 0) {
            oValue = window.parent.document.getElementById(TargetValueID);
        }
        if (TargetTextID.length > 0) {
            oText = window.parent.document.getElementById(TargetTextID);
        }
    }
    else {
        var oValue = document.getElementById(TargetValueID);
        var oText = document.getElementById(TargetTextID);
    }

    var strValueList = "";
    var strTextList = "";
    if (oList != null) {
        var items = oList.options;
        for (i = 0; i < items.length; i++) {
            if (items[i].selected) {
                if (strValueList.length > 0) strValueList += ",";
                if (strTextList.length > 0) strTextList += ",";
                strValueList += items[i].value;
                strTextList += items[i].text;
            }
        }
        if (oValue != null) oValue.value = strValueList;
        if (oText != null) oText.value = strTextList;
    }
}
//============ ListBox 相關  End =======================

//2013.07.23 新增
// 將TreeView 相關子節點一併選取
// 例： TreeView1.Attributes.Add("onclick", "Util_TreeNodeChecked();");
// 參考來源： http://blog.tenyi.com/2008/07/aspnettreeview-checked.html
function Util_TreeNodeChecked() {
    var ele = window.event.srcElement;
    if (ele.type == 'checkbox') {
        var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
        var div = document.getElementById(childrenDivID);
        if (div == null) return;
        var checkBoxs = div.getElementsByTagName('INPUT');
        for (var i = 0; i < checkBoxs.length; i++) {
            if (checkBoxs[i].type == 'checkbox')
                checkBoxs[i].checked = ele.checked;
        }
    }
}

// 快速搜尋下拉選單項目(配合 Util.setJS_SearchDropDownListItem() )
// 參考來源: http://itgroup.blueshop.com.tw/cwvdavid/pg?n=convew&i=260361
//========= Begin =========
var ddl_already_init = false;
var ddl_NameSearch_value = "";
var ddl_ct_name = [];
var ddl_ct_val = [];

function Util_SearchDropDownListItem(SearchTextID, DropDownListID) {
    var myvalue = document.getElementById(SearchTextID).value;
    if (ddl_NameSearch_value == myvalue)
        return false;
    else
        ddl_NameSearch_value = myvalue;

    var RegExp_NameSearch = new RegExp(myvalue, "i");
    var Name_DropDownList = document.getElementById(DropDownListID);
    if (Name_DropDownList == null) {
        return false;
    }

    Util_Dropdown_arrayInit(Name_DropDownList); //初始化陣列...
    Util_RemoveAllOptions(Name_DropDownList); //清除選單...

    //因Server端會固定在[0]產生一個空項目，該項目需固定保留
    Util_AddOption(Name_DropDownList, ddl_ct_name[0], ddl_ct_val[0]);
    //其餘項目則需內容符合才能建成選單項目
    for (i = 1; i < ddl_ct_name.length; i++) {
        if (RegExp_NameSearch.test(ddl_ct_name[i])) //只列出符合條件的...
        {
            Util_AddOption(Name_DropDownList, ddl_ct_name[i], ddl_ct_val[i]);
        }
    }

    Name_DropDownList.options[0].selected = true;
}


function Util_Dropdown_arrayInit(DDL_obj) {
    if (!ddl_already_init) {
        for (var i = 0; i < DDL_obj.options.length; i++) {
            ddl_ct_name[i] = DDL_obj.options[i].text;
            ddl_ct_val[i] = DDL_obj.options[i].value;
        }

        ddl_already_init = true;
    }
}

//清除所有清單選項
function Util_RemoveAllOptions(objList) {
    if (objList != null) {
        objList.options.length = 0;
    }
}

//增加清單選項
function Util_AddOption(objList, text, value) {
    if (objList != null) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        objList.options.add(optn);
    }
}


//2017.02.09 新增
//將 DropDownList [選取]項目的 [value / text]，[取代/添加] 到 TargetTextID ，若 IsParentYN = Y ，則為上層物件
//** IsDropDownTextYN 為選擇性參數(預設 Y) **
function Util_DropDownListItemToTextBox(DropDownListID, TargetTextID, IsParentYN, IsReplaceYN, IsDropDownTextYN) {
    var oSource = document.getElementById(DropDownListID);
    var oValue = oSource.options[oSource.selectedIndex].value;
    var oText = oSource.options[oSource.selectedIndex].text;
    if (oValue.length > 0 && oText.length > 0) {
        if (IsParentYN == 'Y')
            var oTarget = window.parent.document.getElementById(TargetTextID);
        else
            var oTarget = document.getElementById(TargetTextID);

        if (oTarget != null) {
            if (typeof (IsDropDownTextYN) == 'undefined' || IsDropDownTextYN == 'Y')
            {
                //DropDownList [text]
                if (IsReplaceYN == 'Y')
                    oTarget.value = oText;
                else
                    oTarget.value += oText;
            }
            else
            {
                //DropDownList [value]
                if (IsReplaceYN == 'Y')
                    oTarget.value = oValue;
                else
                    oTarget.value += oValue;
            }
        }
    }
}

//=========  End  =========

//文字輸入框加上 浮水印 (配合 Util.setJS_TextBoxWaterMark() )
//參考來源： http://www.codeproject.com/Articles/15193/Applying-a-watermark-to-a-Textbox-in-a-Web-Applica
//========= Begin =========
function Util_WaterMark_Focus(objname, waterMarkText) {
    obj = document.getElementById(objname);
    if (obj.value == waterMarkText) {
        obj.value = "";
        obj.className = "";
    }
}

function Util_WaterMark_Blur(objname, waterMarkText) {
    obj = document.getElementById(objname);
    if (obj.value == "") {
        obj.value = waterMarkText;
        obj.className = "Util_WaterMarkedTextBox";
    }
    else {
        obj.className = "";
    }
}
//=========  End  =========

//產生 XmlHttp 物件 (跨瀏覽器)
//參考來源 http://www.mikesdotnetting.com/Article/40/ASP.NET-and-Ajax-using-XmlHttpRequest

//擴充 IE 取得 XMLHttpRequest 的相容範圍，供 Util_GetXmlHttpObject() 使用
if (window.ActiveXObject && !window.XMLHttpRequest) {
    window.XMLHttpRequest = function () {
        var msxmls = ['Msxml6', 'Msxml5', 'Msxml4', 'Msxml3', 'Msxml2', 'Microsoft'], ex;
        for (var i = 0; i < msxmls.length; i++)
            try { return new ActiveXObject(msxmls[i] + '.XMLHTTP') } catch (ex) { }
        throw new Error("No XML component installed!");
    }
}

function Util_GetXmlHttpObject(handler) {
    var objXmlHttp = null
    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        try {
            objXmlHttp = new XMLHttpRequest();
            objXmlHttp.onreadystatechange = handler
            return objXmlHttp
        }
        catch (e) {
            alert("Error. Scripting for ActiveX might be disabled")
            return
        }
    }
    if (navigator.userAgent.indexOf("Mozilla") >= 0) {
        objXmlHttp = new XMLHttpRequest()
        objXmlHttp.onload = handler
        objXmlHttp.onerror = handler
        return objXmlHttp
    }
}

//========= 全形半形轉換 Begin =========
// 2017.01.20
//轉換對照表
var Util_HalfList = "!\"#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
var Util_FullList = "%uFF01%u201D%uFF03%uFF04%uFF05%uFF06%u2019%uFF08%uFF09%uFF0A%uFF0B%uFF0C%uFF0D%uFF0E%uFF0F%uFF10%uFF11%uFF12%uFF13%uFF14%uFF15%uFF16%uFF17%uFF18%uFF19%uFF1A%uFF1B%uFF1C%uFF1D%uFF1E%uFF1F%uFF20%uFF21%uFF22%uFF23%uFF24%uFF25%uFF26%uFF27%uFF28%uFF29%uFF2A%uFF2B%uFF2C%uFF2D%uFF2E%uFF2F%uFF30%uFF31%uFF32%uFF33%uFF34%uFF35%uFF36%uFF37%uFF38%uFF39%uFF3A%uFF3B%uFF3C%uFF3D%uFF3E%uFF3F%u2018%uFF41%uFF42%uFF43%uFF44%uFF45%uFF46%uFF47%uFF48%uFF49%uFF4A%uFF4B%uFF4C%uFF4D%uFF4E%uFF4F%uFF50%uFF51%uFF52%uFF53%uFF54%uFF55%uFF56%uFF57%uFF58%uFF59%uFF5A%uFF5B%uFF5C%uFF5D%uFF5E";

//半形事件檢查
function Util_ChkToHalf(event, objID) {
    if (window.event.keyCode > 36 && window.event.keyCode < 41) {
        return;
    }
    else {
        Util_ToHalf(objID);
        return;
    }
}

//全形事件檢查
function Util_ChkToFull(event, objID) {
    if (window.event.keyCode > 36 && window.event.keyCode < 41) {
        return;
    }
    else {
        Util_ToFull(objID);
        return;
    }
}

//轉成半形
function Util_ToHalf(objID) {
    var objTxt = document.getElementById(objID);
    var text = objTxt.value;
    if (text == null) {
        return;
    }

    var result = "";
    for (var i = 0; i < text.length; i++) {
        var val = escape(text.charAt(i));
        var j = Util_FullList.indexOf(val);
        result += (((j > -1) && (val.length == 6)) ? Util_HalfList.charAt(j / 6) : text.charAt(i));
    }
    objTxt.value = result;
}

//轉成全形
function Util_ToFull(objID) {
    var objTxt = document.getElementById(objID);
    var text = objTxt.value;
    var result = "";
    for (var i = 0 ; i < text.length ; i++) {
        var val = text.charAt(i);
        var j = Util_HalfList.indexOf(val) * 6;
        result += (j > -1 ? unescape(Util_FullList.substring(j, j + 6)) : val);
    }
    objTxt.value = result;
}
//========= 全形半形轉換 End =========


//產生 XmlHttp 物件 (跨瀏覽器)，並根據成功(onComplete) 失敗(onError)呼叫指定函數 
// onComplete 一定要定義 ，onError 為選擇性參數
//參考來源 http://www.mikesdotnetting.com/Article/40/ASP.NET-and-Ajax-using-XmlHttpRequest
//　　　　 http://stackoverflow.com/questions/10369839/cant-return-xmlhttp-responsetext
// ex: Util_GetXmlHttpObject( function(result){alert('Get Data=' + result)} , function(){alert('Fail')} )
function Util_GetXmlHttpObject(onComplete, onError) {
    var objXmlHttp = null;
    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        try {
            objXmlHttp = new XMLHttpRequest();
            objXmlHttp.onreadystatechange = function () {
                if (objXmlHttp.readyState == 4 || objXmlHttp.readyState == "complete") {
                    //2013.08.06 加入逾時控制
                    setTimeout("function () {objXmlHttp.abort();alert('request timed out,abort request.');}", 3000);

                    if (objXmlHttp.responseText.length > 0) {
                        onComplete(objXmlHttp.responseText);
                    } else {
                        if (onError != null) onError();
                    }
                }
            };
            return objXmlHttp
        }
        catch (e) {
            alert("Error. Scripting for ActiveX might be disabled")
            return
        }
    }

    if (navigator.userAgent.indexOf("Mozilla") >= 0) {
        objXmlHttp = new XMLHttpRequest();
        objXmlHttp.onload = function () { if (objXmlHttp.responseText.length > 0) onComplete(objXmlHttp.responseText); };
        objXmlHttp.onerror = function () { if (onError != null) onError(); };

        return objXmlHttp
    }
}

