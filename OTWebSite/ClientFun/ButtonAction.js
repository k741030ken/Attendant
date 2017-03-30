//document.write("<INPUT language='javascript' id='__btnAction' runat='server' style='DISPLAY: none' onclick='SecurityActionButtonClick();' type='button' value='' name='__btnAction'>");
//document.write("<asp:TextBox id=""__btnAction"" runat=""server"" style=""display:none"" onclientclick=""SecurityActionButtonClick();"" />");
//document.write("<INPUT id='__ActionParam' style='DISPLAY: none' runat='server' type='text' name='__ActionParam'>");

var Color_SelectedRow = 'Moccasin';

function SecurityActionButtonClick()
{
	var objParams;
	var strParams;
	
	objParams = document.getElementById("__ActionParam");
	if (typeof objParams == undefined) return false;
	
	strParams = objParams.value;
	try
	{
	    return funAction(strParams);
	}
	catch (ex)
	{return true;}
}

/*
功能說明：設定控制項的Enable和Disable
建立人員：A02976
建立日期：2007.03.14
參數說明：buttonName=>Button名稱
          State=>true/false
*/
function setButtonState(buttonName, state)
{
    try
    {
        if (window.parent.frames["frmMain"].document.readyState == 'complete' || window.parent.frames["frmMain"].document.readyState == 'interactive')
            window.parent.frames[0].document.getElementById('ucButtonPermission_' + buttonName).disabled = !state
        else
            window.setTimeout("setButtonState('" + buttonName + "', " + state.toString() + ")", 100);
    }
    catch(ex)
    {
        window.setTimeout("setButtonState('" + buttonName + "', " + state.toString() + ")", 100);
    }
}

/*
功能說明：設定控制項的display
建立人員：Chung
建立日期：2012.02.08
參數說明：buttonName=>Button名稱
State=>none/block
*/
function setButtonDisplay(buttonName, state) {
    try {
        if (window.parent.frames["frmMain"].document.readyState == 'complete' || window.parent.frames["frmMain"].document.readyState == 'interactive')
            window.parent.frames[0].document.getElementById('ucButtonPermission_' + buttonName).style.display = state
        else
            window.setTimeout("setButtonDisplay('" + buttonName + "', " + state.toString() + ")", 100);
    }
    catch (ex) {
        window.setTimeout("setButtonDisplay('" + buttonName + "', " + state.toString() + ")", 100);
    }
}

/*
功能說明：設定控制項的Enable和Disable
建立人員：A02976
建立日期：2007.03.14
參數說明：buttonName=>Button名稱
          State=>true/false
*/
function disableAllButton()
{
    var btnName='';
    try
    {
        var btn = window.parent.frames[0].document.getElementsByTagName("input");
        for (var i=0; i<btn.length; i++)
        {
            if (btn[i].type.toString() == "submit")
            {
                btn[i].disabled = true;
                if (btnName == '') btnName = btn[i].id.toString();
            }
        }
        var ctl = window.parent.frames[0].document.getElementByID(btnName);
        if (!ctl.disabled)
            window.setTimeout("disableAllButton();", 100);
    }
    catch(ex)
    {
        window.setTimeout("disableAllButton();", 100);
    }
}

/*
功能說明：設定控制項的顯示
建立人員：A01975
建立日期：2008.07.31
參數說明：state=>block/none
*/
function setTrFunDisplay(state)
{
    try
    {
        if (window.parent.frames["frmMain"].document.readyState == 'complete' || window.parent.frames["frmMain"].document.readyState == 'interactive')
            window.parent.frames[0].document.getElementById('trFun').style.display = state
        else
            window.setTimeout("setTrFunDisplay('" + state.toString() + "')", 100);
    }
    catch(ex)
    {
        window.setTimeout("setTrFunDisplay('" + state.toString() + "')", 100);
    }
}

/*
功能說明：判斷網頁上的GridView是否有選取
建立人員：A02976
建立日期：2007.03.16
參    數：GridName=>若傳入空值，會以gvMain取代
*/
function hasSelectedRow(GridName)
{
    if (GridName == '') GridName = 'gvMain';
    var obj = document.getElementById('__SelectedRow' + GridName);
    
    if (obj != undefined)
    {
        if (obj.value == '')
            return false
        else
            return true;
    }
    
}

/*
功能說明：判斷網頁上的GridView是否有選取 For CheckBox
建立人員：A02976
建立日期：2007.03.16
參    數：GridName=>若傳入空值，會以gvMain取代
*/
function hasSelectedRows(GridName)
{
    if (GridName == '') GridName = 'gvMain';
    var obj = document.getElementById('__SelectedRows' + GridName);
    
    if (obj != undefined)
    {
        if (Replace(obj.value, ',', '') == '')
            return false
        else
            return true;
    }
    
}

/*
功能說明：GridView上的光棒效果
建立人員：A02976
建立日期：2007.03.16
*/
function do_onmouseout(me, GridName, RowIndex)
{
    var obj = document.getElementById('__SelectedRow' + GridName);
 
    if (obj != undefined)
    {
        if (obj.value == RowIndex)
        {
            me.style.backgroundColor = Color_SelectedRow;
        }
        else
        {
            if (parseInt(RowIndex) % 2 == 0)
                me.style.backgroundColor = 'white'
            else
                me.style.backgroundColor = '#e2e9fe';
        }
    }
    else
    {
        if (parseInt(RowIndex) % 2 == 0)
            me.style.backgroundColor = 'white'
        else
            me.style.backgroundColor = '#e2e9fe';
    }
}

/*
功能說明：GridView上的光棒效果(checkbox使用)
建立人員：A02976
建立日期：2007.03.16
*/
function do_onmouseout2(me, GridName, PageIndex, RowIndex)
{
    var obj = document.getElementById('__SelectedRows' + GridName);
 
    if (obj != undefined)
    {
        if (obj.value.indexOf(PageIndex + '.' + RowIndex) >= 0)
        {
            me.style.backgroundColor = Color_SelectedRow;
        }
        else
        {
            if (parseInt(RowIndex) % 2 == 0)
                me.style.backgroundColor = 'white'
            else
                me.style.backgroundColor = '#e2e9fe';
        }
    }
    else {
        if (parseInt(RowIndex) % 2 == 0)
            me.style.backgroundColor = 'white'
        else
            me.style.backgroundColor = '#e2e9fe';
    }
}
/*
功能說明：GridView上的Click轉成Select
建立人員：A02976
建立日期：2007.03.16
*/
function __RowSelected(me, GridName, RowIndex, rdoName)
{
    var obj = document.getElementById('__SelectedRow' + GridName);
    var oldRowIndex;
    var oldRowID;
    var oldRow;
    
    if (obj != undefined)
    {
        oldRowIndex = obj.value;
        obj.value = RowIndex;
        
        oldRowID = 'tr_' + GridName + '_' + oldRowIndex;
        oldRow = document.getElementById(oldRowID);
        
        if (oldRow != undefined)
        {
             if (parseInt(oldRowIndex) % 2 == 0)
                oldRow.style.backgroundColor = 'white'
            else
                oldRow.style.backgroundColor = '#e2e9fe';
        }
    }
    me.backgroundColor = Color_SelectedRow;
    //set radio button
    var objRdoIDText = document.getElementById('__GridViewRadioID' + GridName);

    if (objRdoIDText != undefined)
    {
        var aryRdoID = new Array;
        aryRdoID = objRdoIDText.value.split(',');
        
        for(var i=0; i<aryRdoID.length; i++)
        {
            if (aryRdoID[i] != '')
            {
                var objrdo = document.getElementById(aryRdoID[i]);
                if (aryRdoID[i] == rdoName)
                {
                    if (objrdo.disabled)
                    {
                        objrdo.checked = false;
                        obj.value = '';
                    }
                    else
                        objrdo.checked = true;
                }
                else
                    objrdo.checked = false;
            }
        }
    }
}

/*
功能說明：GridView上的Click轉成Select
建立人員：A02976
建立日期：2007.03.16
*/
function __CheckboxSelected(me, GridName, PageIndex, RowIndex, chkboxName)
{
    var obj = document.getElementById('__SelectedRows' + GridName);
       //alert(chkboxName);
    //檢查Checkbox
    var objChkBox = document.getElementById(chkboxName);
    if (objChkBox != undefined)
    {
        if (!objChkBox.disabled)
        {
            objChkBox.checked = !objChkBox.checked
           //alert(chkboxName + '~1');
            if (obj != undefined)
            {
                if (objChkBox.checked)
                {
                    if (obj.value != '') obj.value = obj.value + ',';
                    obj.value = obj.value + PageIndex + '.' + RowIndex;
                }
                else
                {
                    obj.value = obj.value.replace(PageIndex + '.' + RowIndex + ',', '');
                    obj.value = Replace(obj.value, ',' + PageIndex + '.' + RowIndex + ',');
                    obj.value = obj.value.replace(',' + PageIndex + '.' + RowIndex, '');
                    obj.value = obj.value.replace(PageIndex + '.' + RowIndex, '');
                }
            }
        }
    }
}

/*
功能說明：啟用GridView的Click
建立人員：A02976
建立日期：2007.07.04
*/
function __doGridRowClick(GridName, RowIndex)
{
    var obj = document.getElementById('__SelectedRow' + GridName);
    var objbtn = document.getElementById('__btnGridRowClick');
    var objGridName = document.getElementById('__ActionParam');

    if (obj != undefined && objbtn != undefined && objGridName != undefined)
    {
        var oldRowIndex;
        var oldRowID;
        var oldRow;
        
        oldRowIndex = obj.value;
        obj.value = RowIndex;
        
        oldRowID = 'tr_' + GridName + '_' + oldRowIndex;
        oldRow = document.getElementById(oldRowID);
        
        if (oldRow != undefined)
        {
             if (parseInt(oldRowIndex) % 2 == 0)
                oldRow.style.backgroundColor = 'white'
            else
                oldRow.style.backgroundColor = '#e2e9fe';
        }

        var objSelectedRow = document.getElementById('tr_' + GridName + '_' + RowIndex);
        if (objSelectedRow != undefined)
            objSelectedRow.style.backgroundColor = Color_SelectedRow;
        objGridName.value = GridName;
        objbtn.click();
    }
}