if(document.getElementById("LRdiv0"))
{
var obj = document.getElementById("LRdiv0");
obj.parentNode.removeChild(obj);
obj = document.getElementById("LRdiv1");
obj.parentNode.removeChild(obj);
}
var ctop = (document.documentElement.clientHeight - 240) / 2;
var cleft = (document.documentElement.clientWidth - 464) / 2;
document.write('<div style="display: block;" id="LRdiv0">');
document.write('<iframe style="position:absolute;z-index:2147483647;top:expression(this.nextSibling.offsetTop);left:expression(this.nextSibling.offsetLeft);width:1px;display:none;" id="LRfloater0_if" src="http://sz16.zoosnet.net/LR/im.aspx" frameBorder="0"></iframe>');
document.write('<div style=" width:464px; height:240px;z-index:2147483647;position:fixed!important;top:' + ctop + 'px;left:' + cleft + 'px;_position:absolute;_margin-left:0px;_margin-top:0px;_top:expression(eval(document.compatMode && document.compatMode==\'CSS1Compat\')?(documentElement.scrollTop + (document.documentElement.clientHeight/2)-(this.offsetHeight/2)):(document.body.scrollTop + document.body.clientHeight - this.clientHeight-15));_left:expression(eval(document.compatMode && document.compatMode==\'CSS1Compat\')?(documentElement.scrollLeft + (document.documentElement.clientWidth/2)-(this.offsetWidth/2)):(document.body.scrollLeft + document.body.clientWidth - this.clientWidth-27));" id="LRfloater0">');
document.write('<table style="border: #8dc4eb 2px solid; padding: 0px; background-color: #e1effc;margin: 2px; width: 420px; border-collapse:collapse;" id="LR_Tb2" align="center"><tbody><tr><td style="padding:0px;margin:0px;height: 20px;" valign="bottom" width="400"><font style="color:#000000;margin-left:12px;font-size:12px;font-weight: old" id="bytTitle"></font></td><td style="padding:0px;margin:0px;" width="20" align="right"><a onclick="onlinerIcon0.hidden();" href="javascript:void(0)"><img border="0" src="http://www.fhylmr.com/templets/js/addition/LAL31671888/close.gif"></a></td></tr><tr><td colspan="2"><table style="border:#a7c5e3 1px solid;background-color:#ffffff; margin: 0px 7px 7px;width:400px;border-collapse:collapse;" id="LR_Tb3" align="center"><tbody><tr><td><table style="border:0px;padding:0px;margin:0px;width:400px;height:104px;" id="LR_Tb4" cellspacing="0" cellpadding="0" align="center"><tbody><tr><td style="width: 110px" rowspan="2" align="center"><img src="http://www.fhylmr.com/templets/js/addition/LAL31671888/p.gif" /></td><td style="padding:19px 10px 2px 10px;color:#000000;font-size:12px;" valign="top" align="left"><p><a onclick="openZoosUrl();" href="javascript:void(0)"><img border="0" src="http://www.fhylmr.com/templets/js/addition/LAL31671888/offline_cn.gif"></a><br><br><a onclick="openZoosUrl();" href="javascript:void(0)"><img border="0" src="http://www.fhylmr.com/templets/js/addition/LAL31671888/online_cn.gif"></a><br></p></td></tr><tr><td height="30" align="right"><table style="padding:0px;margin:0px;width:180px;" border="0"><tbody><tr><td><a onclick="openZoosUrl();" href="javascript:void(0)"><img border="0" src="http://www.fhylmr.com/templets/js/addition/LAL31671888/a_cn.gif"></a></td><td width="20"></td><td><a onclick="onlinerIcon0.hidden();" href="javascript:void(0)"><img border="0" src="http://www.fhylmr.com/templets/js/addition/LAL31671888/r_cn.gif"></a></td><td width="20"></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table>');
document.write('</div><div style="display: none;" id="LRdiv1"></div>');

var browser = navigator.userAgent;
if (browser.toLowerCase().indexOf("ie") != -1) { window.attachEvent("onload", SetTitle); }
else { window.addEventListener("load", SetTitle, false); }
function SetTitle(u) {
    if (LR_ip1 != null) {
        document.getElementById("bytTitle").innerHTML = unescape('%u60a8%u597d%uff0c%u6765%u81ea' + LR_ip1 + '%u7684%u670b%u53cb');
        clearTimeout(u);
    }
    else
        u = setTimeout(SetTitle, 1000);
}
/*if (LR_websiteid.toLowerCase() == 'pet88470573')
{
    setTimeout("openZoosUrls('chatwin');", 10000);
}
else */
if (LR_websiteid.toLowerCase() != 'byt62547211' && LR_websiteid.toLowerCase() != 'pet88470573')// && location.host.indexOf("fhbyby.com")==-1 && location.host != "byby.fhylmr.com")
    setTimeout("openZoosUrls('chatwin');", 5000);

function openZoosUrls(url, data) {
//alert(LR_ClientEnd);
    if (typeof (openZoosUrl_UserDefine) == 'function') {
        if (openZoosUrl_UserDefine()) return;
    };
    if (typeof (LR_istate) != 'undefined') {
        LR_istate = 3;
    }
    if (typeof (LR_opentimeout) != 'undefined' && typeof (LR_next_invite_seconds) != 'undefined') LR_next_invite_seconds = 999999;
    if (!url || url == 'chatwin') {
        url = ((LR_userurl0 && typeof (LR_userurl) != 'undefined') ? LR_userurl : (LR_sysurl + 'LR/Chatpre.aspx')) + '?id=' + LR_websiteid + '&cid=' + LR_cid + '&lng=' + LR_lng + '&sid=' + LR_sid + '&p=' + escape(location.href) + lr_refer5238();
    }
    else if (url == 'sendnote') {
        url = LR_sysurl + 'LR/Chatwin2.aspx?siteid=' + LR_websiteid + '&cid=' + LR_cid + '&sid=' + LR_sid + '&lng=' + LR_lng + '&p=' + escape(location.href) + lr_refer5238();
    }
    if (typeof (LR_UserSSL) != 'undefined' && LR_UserSSL && url.charAt(4) == ':') url = url.substring(0, 4) + 's' + url.substring(4, url.length);
    if (!data) {
        if (typeof (LR_explain) != 'undefined' && LR_explain != '') {
            url += '&e=' + escape(escape(LR_explain));
        }
        else if (typeof (LiveAutoInvite1) != 'undefined') {
            url += '&e=' + escape(escape(LiveAutoInvite1));
        }

    }
    var oWindow;
    if (typeof (LR_ucd) != 'undefined') {
        url += '&ucd=' + escape(LR_ucd);
    }
    if (data) url += data;
    url += '&d=' + new Date().getTime();
    if (LR_isMobile) {
        oWindow = window.open(url);
    }
    else {
        try {
            oWindow = window.open(url, 'LR_WIN_' + LR_websiteid, 'toolbar=no,width=630,height=435,resizable=yes,location=no,scrollbars=no,left=' + ((screen.width - 630) / 4) + ',top=' + ((screen.height - 435) / 4));
            if (oWindow == null) {
                LR_ClientEnd = 0;
                //window.location = url;
                return;
            }
            oWindow.focus();
        }
        catch (e) {
            if (oWindow == null) {
                LR_ClientEnd = 0;
                //window.location = url;
            }

        }

    }
}