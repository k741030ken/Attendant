    (function ($) {
        $.fn.currencyFormat = function (p, s) {
            this.each(function (i) {
                $(this).keypress(function (e) {
                    //alow backspace(8), enter(13), and other non character keypress events
                    if (e.which == "0" || e.which == "8" || e.which == "13" || e.ctrlKey || e.altKey) {
                        return;
                    }
                    var pressedChar = String.fromCharCode(e.which);
                    var updatedInput = this.value.substring(0, getSelectionStart(this))
                        + pressedChar
				        + this.value.substring(getSelectionEnd(this), this.value.length);

                    var regExp = new RegExp((s == 0) ? '^[-]{0,1}\\d{1,' + p + '}$' : '^[-]{0,1}\\d{0,' + p + '}(\\.\\d{0,' + s + '})?$');
                    if (!regExp.test(updatedInput)) {
                        e.preventDefault(); //stop the keypress event
                        return;
                    }
                    return;
                });
            });
            return this; //for chaining
        }
    })(jQuery);

    //from: http://javascript.nwbox.com/cursor_position/
    function getSelectionStart(o) {
        if (o.createTextRange) {
            var r = document.selection.createRange().duplicate()
            r.moveEnd('character', o.value.length)
            if (r.text == '') return o.value.length
            return o.value.lastIndexOf(r.text)
        } else return o.selectionStart
    }

    //from: http://javascript.nwbox.com/cursor_position/
    function getSelectionEnd(o) {
        if (o.createTextRange) {
            var r = document.selection.createRange().duplicate()
            r.moveStart('character', -o.value.length)
            return r.text.length
        } else return o.selectionEnd
    }