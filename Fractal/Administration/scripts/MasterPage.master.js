var Root;
var WebRoot;
var Abort;

$(document).ready(function () {
    Root = $("#HFRoot").val();
    WebRoot = $("#HFWebRoot").val();
    Abort = $("#HFAbort").val();
    

    $('#analog-clock').clock({ offset: '+4', type: 'analog' });

    $('.side-menu > ul > li > a').click(function (e) {
        e.preventDefault();
        ToggleMenuItem($(this));
    });
    $(".top-menu .on .top-menu").removeClass("hidden");
    $(".top-menu .selected").removeAttr("href");

    $(".side-menu").on('click', '.page-toggle.close', function () {
        $('html').animate({ marginLeft: -280 });
        $('#content').animate({ marginLeft: 40 });
        $(this).removeClass('close').addClass('open');
    });
    $(".side-menu").on('click', '.page-toggle.open', function () {
        $('html').animate({ marginLeft: 0 });
        $('#content').animate({ marginLeft: 320 });
        $(this).removeClass('open').addClass('close');
    });

    $(".help-container span").click(function () {
        $(this).parent().toggleClass('show');
        return false;
    })
    $(".help-container a").click(function () {
        window.open($(this).attr("href"), 'Invoice', 'width=800, height=700, toolbar=no, scrollbars=yes, status=no');
        return false;
    })
});


function ToggleMenuItem(item) {
    if (item.parent("li").hasClass("on")) {
        item.parent("li").attr("class", "off");
        item.parent("li").children("ul").slideUp(250);
    }
    else if (item.parent("li").hasClass("off")) {
        item.parent("li").attr("class", "on");
        item.parent("li").children("ul").slideDown(250);
    }
}

function ShowDataPopup(HeaderText, ContentUrl, Height, Width) {
    DataPopup.SetContentHtml('');
    DataPopup.SetHeaderText(HeaderText);
    DataPopup.SetContentUrl(ContentUrl);
    DataPopup.Show();
    DataPopup.SetHeight(Height);
    DataPopup.SetWidth(Width);
}


function StringtoXML(text) {
    xmlDoc = $.parseXML(text);
    return $(xmlDoc);
}

function TrimString(string) {
    return string.replace(/(^\s+)|(\s+$)/g, "");
}

function OnCallbackError(s, e) {
    if (e.message.indexOf("Response.Redirect") > -1) {
        e.handled = false;
        window.location = document.URL;
    }
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

function Decode(encoded) {
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
    var output = "";
    var chr1, chr2, chr3;
    var enc1, enc2, enc3, enc4;
    var i = 0;

    do {
        enc1 = keyStr.indexOf(encoded.charAt(i++));
        enc2 = keyStr.indexOf(encoded.charAt(i++));
        enc3 = keyStr.indexOf(encoded.charAt(i++));
        enc4 = keyStr.indexOf(encoded.charAt(i++));

        chr1 = (enc1 << 2) | (enc2 >> 4);
        chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
        chr3 = ((enc3 & 3) << 6) | enc4;

        output = output + String.fromCharCode(chr1);

        if (enc3 != 64) {
            output = output + String.fromCharCode(chr2);
        }
        if (enc4 != 64) {
            output = output + String.fromCharCode(chr3);
        }
    } while (i < encoded.length);

    return output;
}

// Special ajax post with content type for asp shorthand because standard post is not working http://stackoverflow.com/a/8820074/47672
$.postASP = function (url, data, callback, type, errorCallback) {
    if (jQuery.isFunction(data)) {
        type = type || callback;
        callback = data;
        data = undefined;
    }
    return jQuery.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: callback,
        dataType: type,
        //contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            //GMLoader.open();
        },
        complete: function () {
            GMLoader.close();
        },
        error: errorCallback
    });
};