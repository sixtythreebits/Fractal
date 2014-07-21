var customForm = new Object();

$(document).ready(function(e){
			
    $(".custom-form-text").each(function (e) {

        $(this).after("<div class='custom-form-input-style'><span class='tl'></span><span class='tr'></span><span class='t'></span><span class='l'></span><span class='r'></span><span class='bl'></span><span class='br'></span><span class='b'></span></div>");
        $(this).css({ "border": "none", "position": "relative", "z-index": "1", "background": "transparent" });
        var k = $(this).next();


        k.css(customForm.cssPropertyToString($(this)));
        k.offset().left = $(this).offset().left;
        k.offset().top = $(this).offset().top;
        k.css({ "position": "absolute", "z-index": "0" });
        k.parent().css("position", "relative");

        k.children("span.tl").css({
            "position": "absolute",
            "left": 0,
            "top": 0,
            "width": 10,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-t-l.png')"
        });
        k.children("span.tr").css({
            "position": "absolute",
            "right": 0,
            "top": 0,
            "width": 10,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-t-r.png')"
        });
        k.children("span.bl").css({
            "position": "absolute",
            "left": 0,
            "bottom": 0,
            "width": 10,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-b-l.png')"
        });
        k.children("span.br").css({
            "position": "absolute",
            "right": 0,
            "bottom": 0,
            "width": 10,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-b-r.png')"
        });
        k.children("span.t").css({
            "position": "absolute",
            "left": 10,
            "top": 0,
            "width": k.width() - 20,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-t.png')"
        });
        k.children("span.b").css({
            "position": "absolute",
            "left": 10,
            "bottom": 0,
            "width": k.width() - 20,
            "height": 10,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-b.png')"
        });
        k.children("span.l").css({
            "position": "absolute",
            "left": 0,
            "top": 10,
            "width": 10,
            "height": k.height() - 20,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-l.png')"
        });
        k.children("span.r").css({
            "position": "absolute",
            "right": 0,
            "top": 10,
            "width": 10,
            "height": k.height() - 20,
            "background": "url('" + WebRoot + "plugins/FormAdmin/img/textarea-r.png')"
        });
        $(this).css({
            "left": $(this).css("left") == "auto" ? 10 : Number($(this).css("left").split("px")[0]) + 10,
            "top": $(this).css("top") == "auto" ? 10 : Number($(this).css("top").split("px")[0]) + 10,
            //"width":

            "height": (k.height() - 20),
            "margin-bottom": $(this).css("margin-bottom") == "auto" ? 20 : Number($(this).css("margin-bottom").split("px")[0]) + 20,
        });
        $(this).style("width", (k.width() - 20) + "px", "important");
    });
	$(".custom-form-input").each(function(e){
		
		$(this).after("<div class='custom-form-input-style'><span></span><span></span></div>");
		$(this).css({
			"border":"0px none",
			"background":"transparent",	
		});
		if(!Number($(this).css("z-index")))
		{
					$(this).css("z-index",0);
		}
		var k = $(this).next();
		
		k.css({
			"position":"absolute",
			"background":"red",
			"left":$(this).position().left + 10,
			"top":$(this).position().top,
			"margin-left":$(this).css("margin-left"),
			"margin-top":$(this).css("margin-top"),
			"width":$(this).width() -20,
			"height":$(this).height(),
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-text-center.png')",
			"z-index":$(this).css("z-index")-1
		});
		k.children("span").css({
			"position":"absolute",
			"background":"red",
			"width":10,
			"height":k.height(),
			"top":0
		});
		k.children("span").first().css({
			"left":-10,
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-text-left.png')"
		});
		k.children("span").last().css({
			"right":-10,
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-text-right.png')"
		});
		$(this).css({
		    "padding-left": Number($(this).css("padding-left").split("px")[0]) + 15,
		    "padding-right": Number($(this).css("padding-left").split("px")[0]) + 15
		});
	});
	
	$(".custom-form-select").each(function(e){	    
		$(this).after("<div class='custom-form-input-style'><span></span><p></p><span></span></div>");
		$(this).css({
			"border":"0px none",
			"background":"transparent",	
			"position":"relative",
			"opacity":"0",
			"filter":"alpha(opacity=0)",
			"height":41
		});
		if (!Number($(this).css("z-index"))) {
		    $(this).css("z-index", 0);
		}
		var k = $(this).next();

		
		var val = $(this).val();
		var text;
		if (val != null) {
		    if (val.length > 0) {
		        text = $(this).find("option[value='" + val + "']").html();
		    }
		    else {
		        text = $(this).find("option[disabled!=disabled]").first().html();
		    }
		    text = text == null ? "" : text.replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "").replace(/^\s*/, "").replace(/\s*$/, "");
		    text = text != null && text.length > 35 ? text.substring(0, 35) + " ..." : text;
		    k.children("p").html(text);
		}
		$(this).change(function (e) {
		    var text = $(this).find("option[value = " + $(this).val() + "]").html().replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "").replace(/^\s*/, "").replace(/\s*$/, "");
		    text = text == null ? "" : text;
		    text = text.length > 35 ? text.substring(0, 35) + " ..." : text;
		    k.children("p").html(text);            
        });
		
		   
		k.css({
			"position":"absolute",
			"background":"red",
			"left":$(this).position().left + 10,
			"top":$(this).position().top,
			"margin-left":$(this).css("margin-left"),
			"margin-top":$(this).css("margin-top"),
			"width":$(this).width() -45,
			"height":$(this).height(),
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-text-center.png')",
			"z-index":$(this).css("z-index")-1

		});
		k.children("span").css({
			"position":"absolute",
			"background":"red",
			"width":10,
			"height":k.height(),
			"top":0
		});
		k.children("span").first().css({
			"left":-10,
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-text-left.png')"
		});
		k.children("span").last().css({
			"right":-35,
			"width":35,
			"background":"url('" +WebRoot + "plugins/FormAdmin/img/input-select-right.png')"
		});
		k.children("p").css({
			"line-height":"41px",
			"font-size":"12px",
			"position":"absolute",
			"left":5
			
		});
		
		
	});
    
	$(document).on("click", "div.toggler", function (e) {	    
	    customForm.toggleIt(this, 180);
	});

	$(".radio-buttons > li > a.on").each(function(e){
		
		if($(this).parent().hasClass("seasons"))
		{

		    $(this).css("backgroundImage", "url('" + WebRoot + "plugins/FormAdmin/img/button-" + $(this).parent().attr("class").split(" ")[1] + "-ol.png')");
		}
		else
		{
		    $(this).css("backgroundImage", "url('" + WebRoot + "plugins/FormAdmin/img/button-ol.png')");
		}
		
	});
	$(".radio-buttons > li > a").click(function(e){
		e.preventDefault();        
		customForm.chooseIt(this);
	})
	
	
});

customForm.chooseIt = function(t){
	
	if($(t).parent().hasClass("seasons"))
	{
		$(t).parent().parent().children("li").each(function(e){
		    $(this).children("a").css("backgroundImage", "url('" + WebRoot + "plugins/FormAdmin/img/button-" + $(this).attr("class").split(" ")[1] + ".png')");
		});
		
	}
	else
	{
		$(t).parent().parent().children("li").children("a").css("backgroundImage","url('" +WebRoot + "plugins/FormAdmin/img/button.png')");
	}
	
	$(t).css("background",$(t).css("backgroundImage").replace(".png","-ol.png"));

	$(t).change();
	$(t).addClass("on");
}

customForm.toggleIt = function(t,_speed){
	
	if($(t).hasClass("on"))
	{
		$(t).val(false)
		$(t).removeClass("on");
		if($(t).hasClass("small"))
		{
			$(t).children(".pad").animate({"left":23},_speed);
		}
		else
		{
			$(t).children(".pad").animate({"left":40},_speed);
		}
		$(t).change();
	}
	else
	{
		$(t).val(true)
		$(t).addClass("on");
		$(t).children(".pad").animate({"left":0},_speed);
		$(t).change();
		
	}
}
customForm.cssPropertyToString = function(_this){
	var k = {
		"left":_this.css("left") == "auto" ? 0 :_this.css("left"),
		"right":_this.css("right") == "auto" ? 0 :_this.css("right"),
		"top":_this.css("top") == "auto" ? 0 :_this.css("top"),
		"bottom":_this.css("bottom") == "auto" ? 0 :_this.css("bottom"),
		"margin-left":_this.css("margin-left"),
		"margin-right":_this.css("margin-right"),
		"margin-top":_this.css("margin-top"),
		"margin-bottom":_this.css("margin-bottom"),
		"padding-left":_this.css("padding-left"),
		"padding-right":_this.css("padding-right"),
		"padding-top":_this.css("padding-top"),
		"padding-bottom":_this.css("padding-bottom"),
		"float":_this.css("float"),
		"position":_this.css("position"),
		"display":_this.css("display"),
		"width":_this.css("width"),
		"height":_this.css("height"),
	}
	
		
	return k;
}



//style function
// For those who need them (< IE 9), add support for CSS functions
var isStyleFuncSupported = CSSStyleDeclaration.prototype.getPropertyValue != null;
if (!isStyleFuncSupported) {
    CSSStyleDeclaration.prototype.getPropertyValue = function(a) {
        return this.getAttribute(a);
    };
    CSSStyleDeclaration.prototype.setProperty = function(styleName, value, priority) {
        this.setAttribute(styleName,value);
        var priority = typeof priority != 'undefined' ? priority : '';
        if (priority != '') {
            // Add priority manually
            var rule = new RegExp(RegExp.escape(styleName) + '\\s*:\\s*' + RegExp.escape(value) + '(\\s*;)?', 'gmi');
            this.cssText = this.cssText.replace(rule, styleName + ': ' + value + ' !' + priority + ';');
        } 
    }
    CSSStyleDeclaration.prototype.removeProperty = function(a) {
        return this.removeAttribute(a);
    }
    CSSStyleDeclaration.prototype.getPropertyPriority = function(styleName) {
        var rule = new RegExp(RegExp.escape(styleName) + '\\s*:\\s*[^\\s]*\\s*!important(\\s*;)?', 'gmi');
        return rule.test(this.cssText) ? 'important' : '';
    }
}

// Escape regex chars with \
RegExp.escape = function(text) {
    return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
}

// The style function
jQuery.fn.style = function(styleName, value, priority) {
    // DOM node
    var node = this.get(0);
    // Ensure we have a DOM node 
    if (typeof node == 'undefined') {
        return;
    }
    // CSSStyleDeclaration
    var style = this.get(0).style;
    // Getter/Setter
    if (typeof styleName != 'undefined') {
        if (typeof value != 'undefined') {
            // Set style property
            var priority = typeof priority != 'undefined' ? priority : '';
            style.setProperty(styleName, value, priority);
        } else {
            // Get style property
            return style.getPropertyValue(styleName);
        }
    } else {
        // Get CSSStyleDeclaration
        return style;
    }
}


var isStyleFuncSupported = CSSStyleDeclaration.prototype.getPropertyValue != null;
if (!isStyleFuncSupported) {
    CSSStyleDeclaration.prototype.getPropertyValue = function (a) {
        return this.getAttribute(a);
    };
    CSSStyleDeclaration.prototype.setProperty = function (styleName, value, priority) {
        this.setAttribute(styleName, value);
        var priority = typeof priority != 'undefined' ? priority : '';
        if (priority != '') {
            // Add priority manually
            var rule = new RegExp(RegExp.escape(styleName) + '\\s*:\\s*' + RegExp.escape(value) + '(\\s*;)?', 'gmi');
            this.cssText = this.cssText.replace(rule, styleName + ': ' + value + ' !' + priority + ';');
        }
    }
    CSSStyleDeclaration.prototype.removeProperty = function (a) {
        return this.removeAttribute(a);
    }
    CSSStyleDeclaration.prototype.getPropertyPriority = function (styleName) {
        var rule = new RegExp(RegExp.escape(styleName) + '\\s*:\\s*[^\\s]*\\s*!important(\\s*;)?', 'gmi');
        return rule.test(this.cssText) ? 'important' : '';
    }
}

// Escape regex chars with \
RegExp.escape = function (text) {
    return text.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
}

// The style function
jQuery.fn.style = function (styleName, value, priority) {
    // DOM node
    var node = this.get(0);
    // Ensure we have a DOM node 
    if (typeof node == 'undefined') {
        return;
    }
    // CSSStyleDeclaration
    var style = this.get(0).style;
    // Getter/Setter
    if (typeof styleName != 'undefined') {
        if (typeof value != 'undefined') {
            // Set style property
            var priority = typeof priority != 'undefined' ? priority : '';
            style.setProperty(styleName, value, priority);
        } else {
            // Get style property
            return style.getPropertyValue(styleName);
        }
    } else {
        // Get CSSStyleDeclaration
        return style;
    }
}
