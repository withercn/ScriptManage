$(".aside h3").append("<a class=\"toggleLink\">隐藏</a>");
$(".toggleLink").click(function () {
    if ($(this).html() == "隐藏") {
        $(this).text("显示");
        $(this).parent().next('.toggle').slideUp('slow');
    }
    else {
        $(this).text("隐藏");
        $(this).parent().next('.toggle').slideDown('slow');
    }
});
$(function () {
    var winHeight = $(window).height();
    $(".main").css("height", winHeight);
    $(".aside").css("height", winHeight);
    $(".content").css("height", winHeight);
});
