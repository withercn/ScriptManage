var delay = 800;
$(".aside h3").append("<a class=\"toggleLink\">Hide</a>");
$(".toggleLink").click(function () {
    if ($(this).html() == "Hide") {
        $(this).text("Show");
        $(this).parent().next('.toggle').slideUp('slow');
    }
    else {
        $(this).text("Hide");
        $(this).parent().next('.toggle').slideDown('slow');
    }
});
$(function () {
    var winHeight = $(window).height() - 80;
    $(".main").css("min-height", winHeight);
    $(".aside").css("height", winHeight);
    $(".content").css("min-height", winHeight);
    if ($(".tablesorter").length > 0)
        $(".tablesorter").tablesorter({ widthFixed: true, widgets: ['zebra'] });
    $(".selects").val($(".selects").attr("selectedValue"));
});

$(".tablesorter tbody tr").click(function () {
    if ($("input:checkbox", this).attr("checked") != undefined)
        $("input:checkbox", this).removeAttr("checked");
    else
        $("input:checkbox", this).attr("checked", "checked");
});
$("span.all").click(function () { $(".tablesorter input:checkbox").attr("checked", "checked"); });
$("span.none").click(function () {
    $(".tablesorter input:checkbox").each(function () {
        if ($(this).attr("checked") != undefined)
            $(this).removeAttr("checked");
        else
            $(this).attr("checked", "checked");
    });
});
$(".selects").change(function () {
    $("#code").removeClass();
    $("#code").addClass("code" + $(this).val());
    $("#code").focus();
    $("#code").select();
});
function divInit(obj) {
    $(".codeContent>div", obj).slideUp(delay);
    $("header h3.hide", obj).hide();
    $("header h3:eq(0)", obj).show();
}
$(".list header h3").click(function () {
    if (this.className != 'hide') {
        $(this).hide();
        $(this).next().show();
        var hide = $(".list header h3.hide");
        $("input[name='name']", hide).focus();
        $("input[name='name']", hide).select();
    }
});
$(".list header h3.hide input:text").blur(function () {
    $(this).parent().hide();
    $(this).parent().prev().show();
    $(".list header h3 label").html(this.value);
});
$(".list header .history").click(function () {
    var block = $(this).parent().parent().parent();
    divInit(block);
    if ($(".codeList", block).css("display") == "none") {
        var scriptid = $(this).attr("scriptid");
        var action = "/Script/History/" + scriptid;
        $.getJSON(action, function (data) {
            $(".codeList", block).empty();
            var dt = $("<dt></dt>");
            $(data).each(function (index) {
                var code = data[index];
                var dl = $('<dl codeid="' + code.id + '" class="pager">修改时间：' + code.dates + '</dl>');
                var link = $("<a>代码</a>");
                link.click(function () {
                    var dd = $(this).parent().parent().next();
                    if (dd.css("display") == "none")
                        dd.slideDown(delay);
                    else
                        dd.slideUp(delay);
                });
                var undo = $('<a href="/Script/Undo/' + code.id + '">还原</a>');
                var remove = $('<a href="/Script/Remove/' + code.id + '">删除</a>');
                var div = $("<div></div>");
                div.append(link).append(undo).append(remove);
                dl.append(div);
                dt.append(dl).append($('<dd><pre class="brush:js;toolbar: false;">' + code.code + '</pre></dd>'));
            });
            $(".codeList", block).append(dt).slideDown(delay);
            SyntaxHighlighter.highlight();
        });
    }
    else
        $(".codeList", block).slideUp(delay);
});
$(".list header .save").click(function () {
    var block = $(this).parent().parent().parent();
    divInit(block);
    var id = $(this).attr("val");
    var action = "/Script/Update/" + id;
    var code = $(".editor textarea", block).val();
    var token = $("input[name='__RequestVerificationToken']", block).val();
    var sid = $("input[name='sid']", block).val();
    var name = $("input[name='name']", block).val();
    $("input[name='update']", block).val(0);
    $.post(action, { name: name, code: code, __RequestVerificationToken: token, sid: sid }, function (result) { window.parent.location.reload(); location.href = result; });
});
function delay(time, url) {
    setTimeout("location.href = " + url, time);
}
/*********************************************************************************************************************/
/*让TextArea支持Tab键*/
/*********************************************************************************************************************/
HTMLTextAreaElement.prototype.getCaretPosition = function () { //return the caret position of the textarea
    return this.selectionStart;
};
HTMLTextAreaElement.prototype.setCaretPosition = function (position) { //change the caret position of the textarea
    this.selectionStart = position;
    this.selectionEnd = position;
    this.focus();
};
HTMLTextAreaElement.prototype.hasSelection = function () { //if the textarea has selection then return true
    if (this.selectionStart == this.selectionEnd) {
        return false;
    } else {
        return true;
    }
};
HTMLTextAreaElement.prototype.getSelectedText = function () { //return the selection text
    return this.value.substring(this.selectionStart, this.selectionEnd);
};
HTMLTextAreaElement.prototype.setSelection = function (start, end) { //change the selection area of the textarea
    this.selectionStart = start;
    this.selectionEnd = end;
    this.focus();
};
$("textarea").keydown(function (event) {
    //support tab on textarea
    if (event.keyCode == 9) { //tab was pressed
        var newCaretPosition;
        newCaretPosition = this.getCaretPosition() + "    ".length;
        this.value = this.value.substring(0, this.getCaretPosition()) + "    " + this.value.substring(this.getCaretPosition(), this.value.length);
        this.setCaretPosition(newCaretPosition);
        return false;
    }
    if (event.keyCode == 8) { //backspace
        if (this.value.substring(this.getCaretPosition() - 4, this.getCaretPosition()) == "    ") { //it's a tab space
            var newCaretPosition;
            newCaretPosition = this.getCaretPosition() - 3;
            this.value = this.value.substring(0, this.getCaretPosition() - 3) + this.value.substring(this.getCaretPosition(), this.value.length);
            this.setCaretPosition(newCaretPosition);
        }
    }
    if (event.keyCode == 37) { //left arrow
        var newCaretPosition;
        if (this.value.substring(this.getCaretPosition() - 4, this.getCaretPosition()) == "    ") { //it's a tab space
            newCaretPosition = this.getCaretPosition() - 3;
            this.setCaretPosition(newCaretPosition);
        }
    }
    if (event.keyCode == 39) { //right arrow
        var newCaretPosition;
        if (this.value.substring(this.getCaretPosition() + 4, this.getCaretPosition()) == "    ") { //it's a tab space
            newCaretPosition = this.getCaretPosition() + 3;
            this.setCaretPosition(newCaretPosition);
        }
    }
});