
var formValid = false;
var CustomValidatorValid = true;
if (typeof jQuery != 'undefined') {
    var minVer = '1.8.2';
    if (!minVersion(minVer)) {
        CustomValidatorValid = false;
        alert('CustomValidator requires jQuery v' + minVer + ' or later!  You are using v' + $.fn.jquery);
    }
} else {
    CustomValidatorValid = false;
    alert('JQuery is not included!');
}

function minVersion(version) {
    var $vrs = window.jQuery.fn.jquery.split('.'),
      min = version.split('.');
    for (var i = 0, len = $vrs.length; i < len; i++) {
        if (min[i] && $vrs[i] < min[i]) {
            return false;
        }
    }
    return true;
}

if (CustomValidatorValid) {
    $(document).ready(function () {
        $("form").validate({ onsubmit: false });

        $.validator.addMethod("prcidno", function (value, element) {
            var valid = /^\d{18}$/.test(value);
            if (valid) {
                var key = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
                var validkey = ["1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"];
                var chkValue = 0;
                for (i = 0; i < 17; i++) {
                    chkValue += parseInt(value.substr(i, 1)) * key[i];
                }
                valid = (value.substr(17, 1) == validkey[chkValue % 11]);
            } else if (/^\d{15}$/.test(value)) {
                valid = true;
            }
            return valid || this.optional(element);
        }, "公民身分證號不正確");

        //台灣身份證字號驗證
        $.validator.addMethod("CheckTWPID", function (value, element) {
            var strID = value.toString();
            if (strID.length != 10) return false;
            strID = strID.toUpperCase();
            var c = strID.charCodeAt(0) - 65;
            if (c < 0 || c > 25) return false;
            if (!IsNumericString(strID.substr(1))) return false;
            if (strID.charAt(1) != '1' && strID.charAt(1) != '2') return false;
            var codes = new Array(10, 11, 12, 13, 14, 15, 16, 17, 34, 18, 19, 20, 21, 22, 35, 23, 24, 25, 26, 27, 28, 29, 32, 30, 31, 33);

            var se = new Array(10);
            se[0] = parseInt(codes[c].toString().charAt(0));
            se[1] = parseInt(codes[c].toString().charAt(1));
            var we = se[0] + (se[1] * 9);
            var checkcode = 0;
            for (i = 1; i <= 9; i++) {
                se[i + 1] = parseInt(strID.substr(i, 1));
                we = we + (se[i + 1] * (9 - i));
            }
            we = we + se[10]
            checkcode = (we % 10);
            if (checkcode != 0) return false;
            return true;
        }, "身分證號不正確");

        $.validator.addMethod("loancardno", function (value, element) {
            var valid = /^[\d|A-Z]{3}\d{13}$/.test(value);
            if (valid) {
                var key = [1, 3, 5, 7, 11, 2, 13, 1, 1, 17, 19, 97, 23, 29];
                var chkValue = 0;
                for (i = 0; i < 14; i++) {
                    chkValue += parseInt("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".indexOf(value.substr(i, 1))) * key[i];
                }
                var strValue = "0" + (chkValue % 97 + 1);
                valid = (value.substr(14, 2) == strValue.substr(strValue.length - 2));
            }
            return valid || this.optional(element);
        }, "贷款卡编码不正确");
        $.validator.addMethod("orgidno", function (value, element) {
            var valid = /^[0-9|A-Z]{8}[0-9|X]$/.test(value);
            if (valid) {
                var key = [3, 7, 9, 10, 5, 8, 4, 2];
                var chkValue = 0;
                for (i = 0; i < 8; i++) {
                    chkValue += parseInt("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".indexOf(value.substr(i, 1))) * key[i];
                }
                var strValue = 11 - chkValue % 11;
                valid = (value.substr(8, 1) == "0123456789X0".substr(strValue, 1));
            }
            return valid || this.optional(element);
        }, "組織機構代碼不正確");

        $.validator.addMethod("phonenum", function (value, element) {
            var phonenum = /^[Pp0-9\#\(\)\+\-]+$/;
            return this.optional(element) || (phonenum.test(value));
        }, "聯絡電話不正確");


        $.validator.addMethod("english", function (value, element) {
            //[\x00-\xff] 單字節  [^\u4E00-\u9fa5] 非中文
            var english = /^[\x00-\xff]+$/;
            return this.optional(element) || (english.test(value));
        }, "不得輸入中文字");

        $.validator.addMethod("month", function (value, element) {
            var month = /^([1-9]|0[1-9]|1[0-2])$/;
            return this.optional(element) || (month.test(value));
        }, "請輸入合法年月");
    });
    $(window).load(function () {
        $('textarea[rel]').each(function () {
            switch ($(this).attr('rel').split(',')[0] || '') {
                case 'wordcount':
                    ApplyWordCount($(this));
                    break;
            }
        });

        $('input[rel]').each(function () {
            switch ($(this).attr('rel').split(',')[0] || '') {
                case 'decimal':
                    ApplyDecimalCheck($(this));
                    break;
                case 'wordcount':
                    ApplyWordCount($(this));
                    break;
                case 'bindctrl':
                    BindControl($(this));
                    break;
            }
        });
    });
}

function ApplyDecimalCheck(element) {
    var p = parseInt(element.attr('rel').split(',')[1]);
    var s = parseInt(element.attr('rel').split(',')[2]) || 0;
    if (element.hasClass('force')) {
        element.currencyFormat(p, s);
    } else {
        element.rules('add', { required: element.hasClass('required'), decimal: [p, s] });
    }
}

function ApplyWordCount(element) {
    var settings = element.attr('rel').split(',').length;
    element.NobleCount(element.attr('rel').split(',')[2] || '', {
        max_chars: parseInt(element.attr('rel').split(',')[1]) || 0,
        block_negative: true,
        cloak: (settings == 4),
        on_update: function (t_obj, char_area, c_settings, char_rem) {
            //console.log('c_settings.max_chars = ' + c_settings.max_chars);
            //console.log('char_rem = ' + char_rem);

        var word_type = ['限<font color=red>{0}</font>個中文字之內，共輸入<font color=red>{1}</font>個字'];

            if (c_settings.cloak) {
                char_area.html(word_type[parseInt(t_obj.attr('rel').split(',')[3] || 0)].replace('{0}', c_settings.max_chars).replace('{1}', c_settings.max_chars - char_rem));
            }
        }
    });
}

function BindControl(element) {
    var rel = element.attr('rel');
    var arr = rel.split(',');
    var cnt = arr.length;
    //rel="bindctrl,#控制項1"
    //rel="bindctrl,#控制項1,#控制項2,....,比對的值"
    //rel="bindctrl,#控制項1,#控制項2,....,#控制項n"

    var match = (cnt < 3) ? true : false;
    rel = rel.replace('bindctrl,', '');
    if (arr[cnt - 1].indexOf('#') != 0) {
        match = element.val() == arr[cnt - 1];
        rel = rel.replace(',' + arr[cnt - 1], '');
    } else match = true;
    //最後rel會只剩下 #控制項1,#控制項2,....,#控制項n

    //點選時
    element.bind('change', function () {
        var checked = $(this).is(':checked');     

        for (var i = 0; i < rel.split(',').length; i++) {
            if (checked && match) {
                $(rel.split(',')[i]).removeAttr('disabled').select();
            } else {
                $(rel.split(',')[i]).attr('disabled', 'disabled');
            }
        }
    });

    //剛進入頁面時
    if (!element.is(':checked')) {        
        if (match) {
            for (var i = 0; i < rel.split(',').length; i++) {
                $(rel.split(',')[i]).attr('disabled', 'disabled');
            }
        }
    }
}

function checkFunAction() {
    if (formValid) return true;
    var validObj = new Object();
    var validCnt = 0;
    $('.remote').each(function () {
        validObj[$(this).attr('id')] = $(this).val();
        validCnt++;
    });
    
    if (validCnt > 0) {
        $.ajax({
            type: "POST",
            url: location.href.split("?")[0] + '/CheckData',
            cache: false,
            data: JSON.stringify({ "args": validObj }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var arrMsg = $.parseJSON(data.d);

                formValid = true;
                var validate = $("form").validate();
                $.each(arrMsg, function (index, msg) {
                    if (msg.success) return;
                    formValid = false;

                    validate.errorList.push({
                        message: msg.message,
                        element: $('#' + msg.target)[0]
                    });
                });
                if (!formValid) {
                    validate.defaultShowErrors();
                } else if ($('form').valid()) {
                    $('#__btnAction').click();
                }
            },
            error: AjaxErrorHandling
        });
    }
    if ($('form').valid() && validCnt == 0) {
        return true;
    }
    return false;
}

function AjaxErrorHandling(request, status, error) {
    try {
        var msg = $.parseJSON(request.responseText);
        alert(request.statusText + '(' + request.status + ')\r\n' + msg.Message);
    } catch (e) {
        var regex = /<title>(.*?)<\/title>/ig;
        var match = regex.exec(request.responseText);
        alert(request.statusText + '(' + request.status + ')\r\n' + match[1].replace('<br>', '\r\n'));
    }
}
