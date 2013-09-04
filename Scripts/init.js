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
$(".list header .show").click(function () {
    var index = $(".list header .show").index(this);
    $(".list .editor:eq(" + index + ")").hide(500);
    if ($(".syntaxhighlighter:eq(" + index + ")").css("display") == "none") {
        $(".syntaxhighlighter:eq(" + index + ")").slideDown(1000);
        $(this).html("关闭");
    }
    else {
        $(".syntaxhighlighter:eq(" + index + ")").slideUp(1000);
        $(this).html("查看");
    }
});
$(".list header .editCode").click(function () {
    var index = $(".list header .editCode").index(this);
    $(".syntaxhighlighter:eq(" + index + ")").slideUp(500);
    $(".list header .show:eq(" + index + ")").html("查看");
    if ($(".list .editor:eq(" + index + ")").css("display") == "none")
        $(".list .editor:eq(" + index + ")").slideDown(1000);
    else
        $(".list .editor:eq(" + index + ")").slideUp(1000);
});
$(".list header .save").click(function () {
    var index = $(".list header .save").index(this);
    var id = $(this).attr("val");
    var action = "/Script/Update/" + id;
    var code = $(".list .editor textarea").val();
    var token = $("form:eq(" + index + ") input:hidden:eq(1)").val();
    var sid = $("form:eq(" + index + ") input:hidden:eq(0)").val();
    $.post(action, { id: id, code: code, __RequestVerificationToken: token, sid: sid }, function (result) { location.href = result;});
});
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