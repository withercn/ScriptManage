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
    $(".tablesorter").tablesorter({ widthFixed: true, widgets: ['zebra'] })
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
//$("thead tr .header").click(function () {
//    if ($(this).hasClass("headerSortUp"))
//        $("img", this).attr("src", "images/asc.gif");
//    else if ($(this).hasClass("headerSortDown"))
//        $("img", this).attr("src", "images/desc.gif");
//    else
//        $("img", this).attr("src", "images/bg.gif");
//});