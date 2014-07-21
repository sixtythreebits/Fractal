var quizes = new Object();

$(document).ready(function () {

    if ($("#HFQC").val() == "0") {
        $(".icon.info").removeClass("hidden");
    }
    else {
        $(".icon.add").removeClass("hidden");
    }

    $("#view .main-title > input.search").keyup(function (e) {
        var sr = $(this).val();
        if (e.which != 13) {

            $("#quizes ul li .content h1").each(function (e) {

                if ($(this).text().toUpperCase().match(sr.toUpperCase())) {
                    $(this).parent().parent().show(200);
                }
                else {
                    $(this).parent().parent().hide(200);
                }
            });
        }
    });

    $(".icon.add").click(function (e) {
        e.preventDefault();
        FancyBox.Init({
            href: $("#HFNewURL").val(),
            height: 650,
            width: 680
        }).ShowPopup();
    });
    
    $("#quizes").on("click", ".icon.delete", function (e) {
        e.preventDefault();
        var _this = $(this);
        $(DialogDivID).dialog("option",
        {
            title: "კავშირის გაწყვეტა",
            width: 200,
            buttons:
            {
                "კი": function () {
                    quizes.remove(_this);
                    $(this).dialog("close");
                },

                "არა": function () {
                    $(this).dialog("close");
                }
            }
        });
        $(DialogDivID).dialog("open");
    });
});


quizes.remove = function (_this) {
    $.ajax({
        url: document.URL,
        type: "POST",
        data: { action: "delete", id: _this.parent().parent().parent().parent().attr("id") },
        dataType: "text",
        beforeSend: function () {            
            _this.addClass("loader");
        },
        success: function (res) {
            if (res.indexOf("success") > -1) {
                _this.parent().parent().parent().parent().hide(200, function () {
                    $(this).remove();
                });
                $(".icon.info").addClass("hidden");
                $(".icon.add").removeClass("hidden");
            }
            else {
                _this.children().remove();
                alert(Abort);
            }
        },
        error: function (res) {
            _this.children().remove();
            alert(Abort);
        },
        complete: function () {
            _this.removeClass("loader");
        }
    });
}