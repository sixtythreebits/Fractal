var customForm = new Object();
$(function () {

    $(document).on("click", "div.toggler", function (e) {
        customForm.toggleIt(this, 180);
    });

    $('.select select').each(function (index, item) {

        if ($(item).children("optgroup").length > 0) {
            $(item).parents(".select").setSelectOptGroupInteraction();
        }
        else {
            $(item).parents(".select").setSelectInteraction();
        }
    })
         
    $(".radio-buttons > li > a.on").each(function (e) {

        if ($(this).parent().hasClass("seasons")) {

            $(this).css("backgroundImage", "url('/plugins/FormAdmin/img/button-" + $(this).parent().attr("class").split(" ")[1] + "-ol.png')");
        }
        else {
            $(this).css("backgroundImage", "url('/plugins/FormAdmin/img/button-ol.png')");
        }

    });
    $(".radio-buttons > li > a").click(function (e) {
        customForm.chooseIt(this);
        return false;
    });

    $(".file-uploader input[type=file]").change(function () {
        $(this).parent().parent().prev().text($(this).val());
    });
    $(".file-uploader .icon.text.image").click(function () {
        var url = $(this).attr("href");
        if (url != undefined && url != null && url.length > 0) {
            FancyBox.Init({
                href: $(this).attr("href")
            }).ShowImgPopup();
        }
        return false;
    });

    $(".form.img-list .img").click(function () {
        var url = $(this).attr("href");
        if (url != undefined && url != null && url.length > 0) {
            FancyBox.Init({
                href: $(this).attr("href")
            }).ShowImgPopup();
        }
        return false;
    });
    $(".form.img-list .icon.delete").click(function () {

        var _this = $(this);

        $(DialogDivID).dialog("option",
        {
            title: "Delete Image",
            width: 255,
            buttons:
            {
                "Yes": function () {
                    $(this).dialog("close");
                    $.ajax({
                        url: document.URL,
                        type: "POST",
                        data: { action: "delete-image", id: _this.attr("data-hash") },
                        dataType: "text",
                        beforeSend: function () {
                            GMLoader.open();
                        },
                        success: function (res) {
                            if (res == "success") {
                                _this.parent().slideUp(200, function () {
                                    $(this).remove();
                                });
                            } else {
                                alert(Abort);
                            }
                        },
                        error: function (res) {
                            alert(Abort);
                        },
                        complete: function () {                            
                            GMLoader.close();
                        }
                    });
                },
                "No": function () {
                    $(this).dialog("close");
                }
            }
        });
        $(DialogDivID).dialog("open");
        return false;
    });
    $('.form.img-list li').on('click', 'input[type=radio]', function () {
        $(this).parent().parent('li').prependTo('.img-list ul');
    });
});

jQuery.fn.extend({

    setSelectTitle: function (val) {
        this.children("select").prepend("<option value=null>" + val + "</option>");
        this.children("select").val("null");
        this.children("p").html(val);
    },
    setSelectInteraction: function (MinLength) {
        
        MinLength = MinLength > 0 ? MinLength : 35;

        var text = this.children("select").children("option[value = '" + this.children("select").val() + "']").html();
        text = text == null ? "" : text;
        text = text.length > 35 ? text.substring(0, MinLength) + " ..." : text;
        this.children("p").html(text);

        this.children("select").change(function (e) {
            var text = $(this).children("option[value = '" + $(this).val() + "']").html();
            text = text.length > 35 ? text.substring(0, MinLength) + " ..." : text;
            $(this).parent().children("p").html(text);            
        });
    },
    setSelectOptGroupInteraction: function () {

        var select = this.children("select");
        var val = select.val();
        var text;
        if (val.length == 0) {
            text = select.find("option[disabled!=disabled]").first().html().replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "").replace(/^\s*/, "").replace(/\s*$/, "");
        }
        else {
            text = select.find("option[value='" + val + "']").html().replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "").replace(/^\s*/, "").replace(/\s*$/, "");
        }
        text = text == null ? "" : text;
        text = text.length > 35 ? text.substring(0, 35) + " ..." : text;
        this.children("p").text(text);

        this.children("select").change(function (e) {
            var text = $(this).find("option[value = " + $(this).val() + "]").html().replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "").replace(/^\s*/, "").replace(/\s*$/, "");            
            text = text.length > 35 ? text.substring(0, 35) + " ..." : text;
            $(this).parent().children("p").html(text);
        });
    },
	resetSelect: function () {
	    var first = $(this).children().first();
	    $(this).parent().children("p").html(first.html());
	    $(this).val(first.val());
		this.change();
	},
	setSelectedValue: function (value) {
	    var self = $(this);
	    var option = self.children("option[value='" + value + "']").first();
	    self.parent().children("p").html(option.html());
	    self.val(option.val());
	    this.change();
	}
});

customForm.chooseIt = function (t) {

    if ($(t).parent().hasClass("seasons")) {
        $(t).parent().parent().children("li").each(function (e) {
            $(this).children("a").css("backgroundImage", "url('/plugins/FormAdmin/img/button-" + $(this).attr("class").split(" ")[1] + ".png')");
        });

    }
    else {
        $(t).parent().parent().children("li").children("a").css("backgroundImage", "url('/plugins/FormAdmin/img/button.png')");
    }

    $(t).css("background", $(t).css("backgroundImage").replace(".png", "-ol.png"));

    $(t).change();
    $(t).addClass("on");
}
customForm.toggleIt = function (t, _speed) {

    if ($(t).hasClass("on")) {
        $(t).val(false)
        $(t).removeClass("on");
        if ($(t).hasClass("small")) {
            $(t).children(".pad").animate({ "left": 23 }, _speed);
        }
        else {
            $(t).children(".pad").animate({ "left": 40 }, _speed);
        }
        $(t).change();
    }
    else {
        $(t).val(true)
        $(t).addClass("on");
        $(t).children(".pad").animate({ "left": 0 }, _speed);
        $(t).change();

    }
}