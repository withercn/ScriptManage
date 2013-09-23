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
$("span.all").click(function () { $(".module input:checkbox").attr("checked", "checked"); });
$("span.none").click(function () {
    $(".module input:checkbox").each(function () {
        if ($(this).attr("checked") != undefined)
            $(this).removeAttr("checked");
        else
            $(this).attr("checked", "checked");
    });
});
$(".selects").change(function () {
    $("#code").attr("class", "code" + $(this).val());
    $("#code").focus();
    $("#code").select();
});
function divInit(obj) {
    $(".codeContent>div", obj).slideUp(delay);
    $("header h3.hide", obj).hide();
    $("header h3:eq(0)", obj).show();
}
$(".scripts header h3").click(function () {
    if (this.className != 'hide') {
        $(this).hide();
        $(this).next().show();
        var hide = $(".scripts header h3.hide");
        $("input[name='name']", hide).focus();
        $("input[name='name']", hide).select();
    }
});
$(".scripts header h3.hide input:text").blur(function () {
    $(this).parent().hide();
    $(this).parent().prev().show();
    $(".scripts header h3 label").html(this.value);
});
$("header .history").click(function () {
    var role = eval($(this).attr("role").toLowerCase());
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
                var dl = $('<dl dlid="' + code.id + '" class="pager">修改时间：' + code.dates + (code.type == 2 ? ' <span class="remote">(远程脚本)</span>' : '') + '</dl>');
                var link = $("<a>代码</a>");
                link.click(function () {
                    var dd = $(this).parent().parent().next();
                    if (dd.css("display") == "none")
                        dd.slideDown(delay);
                    else
                        dd.slideUp(delay);
                });
                var div = $("<div></div>");
                div.append(link);
                if (role) {
                    var undo = $('<a href="/Script/Undo/' + code.id + '">还原</a>');
                    div.append(undo);
                    var removes = $('<a>删除</a>');
                    removes.click(function () {
                        var action = "/Script/Remove/" + code.id;
                        $.post(action, null, function (results) {
                            $("dd[ddid='" + code.id + "']").remove();
                            $("dl[dlid='" + code.id + "']").fadeOut(delay, function () { $(this).remove(); });
                        });
                    });
                    div.append(removes);
                }
                var run = $("<a>运行</a>");
                run.click(function () {
                    var wintar = window.open("about:blank");
                    wintar.document.open('text/html', 'replace');
                    wintar.opener = null;
                    wintar.document.writeln('<script type="text/javascript" src="/scripts/jquery-1.8.2.min.js"></script>');
                    if (code.type == 2)
                        wintar.document.write('<script type="text/javascript" src="' + code.code + '"></script>');
                    else
                        wintar.document.write('<script type="text/javascript">' + code.code + '</script>');
                    wintar.document.close();
                });
                div.append(run);
                dl.append(div);
                dt.append(dl).append($('<dd ddid=' + code.id + '><pre class="brush:js;toolbar: false;">' + code.code + '</pre></dd>'));
            });
            $(".codeList", block).append(dt).slideDown(delay);
            SyntaxHighlighter.highlight();
        });
    }
    else
        $(".codeList", block).slideUp(delay);
});
$(".scripts .module header").click(function () {
    if ($("input:checkbox", this).attr("checked") == undefined)
        $("input:checkbox", this).attr("checked", "checked");
    else
        $("input:checkbox", this).removeAttr("checked");

});
$("a.submit").click(function () {
    var index = $("a.submit").index(this);
    $("form:eq(" + index + ")").submit();
});
$(".copyCode").click(function () {
    copyToClipboard($("#download").val());
});
function isPlaceholder() { return 'placeholder' in document.createElement('input'); }
if (!isPlaceholder()) {
    $("input").not("input[type='password']").each(//把input绑定事件 排除password框  
        function () {
            var color = $(this).css("color");
            var pColor = "#ccc";
            if ($(this).val() == "" && $(this).attr("placeholder") != "") {
                $(this).val($(this).attr("placeholder"));
                $(this).css("color", pColor);
                $(this).focus(function () {
                    if ($(this).val() == $(this).attr("placeholder")) {
                        $(this).val("");
                        $(this).css("color", color);
                    }
                });
                $(this).blur(function () {
                    if ($(this).val() == "") {
                        $(this).val($(this).attr("placeholder"));
                        $(this).css("color", pColor);
                    }
                });
            }
        });
}
function copyToClipboard(txt) {
    if (window.clipboardData) {
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", txt);
        alert("已经成功复制到剪帖板上！");
    } else if (navigator.userAgent.indexOf("Opera") != -1) {
        window.location = txt;
    } else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        } catch (e) {
            alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将'signed.applets.codebase_principal_support'设置为'true'");
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip) return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
        alert("已经成功复制到剪帖板上！");
    }
}