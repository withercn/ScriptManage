var suspendcode = '<DIV id="lovexin1" style="Z-INDEX: 10; position:fixed;right: 0px; bottom: 0px; width: 252px; height: 172px;cursor:pointer;display:none;"><a onclick="hideLovexin1()"   style="width:30px;height:20px;display:block;position:absolute;cursor:pointer; right:3px;top:2px;text-align:center; color:#777777; font-family:Arial, Helvetica, sans-serif;font-size:20px;" title="关闭">&nbsp;</a><a href="javascript:void(0)" onclick="openZoosUrl();" id="qqShake"><img src="http://www.fhylmr.com/templets/js/addition/qqdd.gif" width="252" height="172" border="0" /></a></DIV>'
var suspendcode2 = '<DIV id="lovexin1" style="Z-INDEX: 10; position:fixed;right: 0px; bottom: 0px; width: 252px; height: 172px;cursor:pointer;display:none;"><a onclick="hideLovexin1()"   style="width:30px;height:20px;display:block;position:absolute;cursor:pointer; right:3px;top:2px;text-align:center; color:#777777; font-family:Arial, Helvetica, sans-serif;font-size:20px;" title="关闭">&nbsp;</a><a href="javascript:void(0)" onclick="openZoosUrl();" id="qqShake"><img src="http://www.fhylmr.com/templets/js/addition/qqdd.gif" width="252" height="172" border="0" /></a></DIV>'
document.write(suspendcode2);

$(document).ready(function () {
    $('#lovexin1').css('display', 'block');
    FollowDiv = {
        follow: function () {
            $('#lovexin1').css('position', 'absolute');
            $(window).scroll(function () {
                var f_top = $(window).scrollTop() + $(window).height() - $("#lovexin1").outerHeight();
                $('#lovexin1').css('top', f_top);
            });
        }
    }
    /*FF和IE7可以通过position:fixed来定位，只有ie6需要动态设置高度.*/
    if ($.browser.msie && $.browser.version == 6) {
        FollowDiv.follow();
    }
    shake();
    repeat = setInterval(shake, 10000); //这里repeat是全局，在hideLovexin1函数中清空
});
function hideLovexin1() {
    $('#lovexin1').css('display', 'none');
        window.setTimeout("ShowLoveXin()", 5000);
        //clearInterval(repeat);
    }
	function ShowLoveXin(){
	$('#lovexin1').css('display', 'block');
	}
/* 窗口抖动 */
function shake() {
    var a = ['bottom', 'right'], b = 0;
    var u = setInterval(function () {
        $('#lovexin1').css(a[b % 2], (b++) % 4 < 2 ? 0 : 4);
        if (b > 17) {
            clearInterval(u);
            b = 0;
        }
    }, 30)
}