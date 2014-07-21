var Root;
var Abort;

$(document).ready(function () {

    Root = $("#HFRoot").val();
    Abort = $("#HFAbort").val();

    $(".pop-close").click(function (e) {
        e.preventDefault();
        ClosePopup(true);
    });

    var FancyBox = parent.window["FancyBox"];
    var height = FancyBox == undefined ? 1000 : FancyBox.height - 114;
    height = height > 20 ? height : 1000;

    $("#pop-content").css(
    {
        'overflow': 'auto',
        'height': height
    });


});

function ClosePopup(DoNotReload) {
    if (!DoNotReload) {
        window.parent.window.location.reload();
    }
    window.parent.$.fancybox.close();
}

function validatenumbers(key) {
    var keycode = (key.which) ? key.which : key.keyCode
    if (((keycode > 47) && (keycode < 58)) || (keycode == 8) || (keycode == 9)) {
        return true;
    }
    else {
        return false;
    }
}

function StringtoXML(text) {
    xmlDoc = $.parseXML(text);
    return $(xmlDoc);
}

(function ($) {
    var cache = [];
    $.preLoadImages = function () {
        var args_len = arguments.length;
        for (var i = args_len; i--;) {
            var cacheImage = document.createElement('img');
            cacheImage.src = arguments[i];
            cache.push(cacheImage);
        }
    }
})(jQuery)

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return null;
    else
        return results[1];
}