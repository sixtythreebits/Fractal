$(document).ready(function () {
    
    $(".image-tip").click(function (e) {
        e.preventDefault();

        var _this = $(this);
        GMLoader.open();

        $.ajax({
            url: Root.replace("administration/", "") + "api/?iud=10",
            type: "POST",
            data: { action: "preview", id: _this.attr("id") },
            success: function (res) {
                if (res.length > 0) {
                    eval(res);
                } else {
                    alert(Abort);
                }
            },
            dataType: "text",
            error: function (res) {
                alert(Abort);
            },
            complete: function () {
                GMLoader.close();
            }
        });
    });

    $('.submit.quiz').click(function (e) {
        e.preventDefault();
        
        $(DialogDivID).dialog("option",
        {
            title: "Submit Quiz",
            width: 300,
            buttons:
            {
                "Submit": function () {
                    GMLoader.open();
                    $(this).dialog("close");
                    var xml = "<data>";

                    var qid;
                    var aid;
                    var ul = $('.questions > li > ul');
                    ul.each(function () {
                        var li = $(this).children("li");                        
                        qid = $(this).attr("id").split("_");
                        li.each(function () {
                            if ($(this).children().prop("checked")) {
                                aid = $(this).attr('id').split("_");
                                xml += '<answer question_id="' + qid[1] + '" answer_id="' + aid[1] + '" />';
                            }
                        });
                    });

                    xml += "</data>";

                    $.ajax({
                        url: Root + "api/?iud=9",
                        type: "POST",
                        data: { action: "submit", data: xml, params: $("#HFData").val() },
                        dataType: "text",
                        success: function (res) {
                            var x = StringtoXML(res);
                            if (x.find("result").text() == "success") {
                                GMLoader.close();

                                $(DialogDivID).find("p").text(x.find("message").text());

                                var IsOpen = false;

                                $(DialogDivID).dialog("option",
                                {
                                    title: "Result",
                                    width: 280,
                                    buttons:
                                    {
                                        "OK": function () {
                                            $(this).dialog("close");
                                            window.location = document.URL;
                                        }
                                    },
                                    open: function (event, ui) {

                                        $(".ui-dialog-titlebar-close.ui-corner-all").remove();
                                    }
                                });

                                $(DialogDivID).dialog("open");
                            }
                            else {
                                GMLoader.close();
                                alert(Abort);
                            }
                        },
                        error: function () {
                            GMLoader.close();
                            alert(Abort);
                        }
                    });

                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });

        $(DialogDivID).dialog("open");

    });

    $('.controll-panel a[rel="hint"]').click(function (e) {
        $('.hint[id=hint_' + $(this).parent().parent().parent().attr("id") + ']').toggle();
        if ($(this).text() == "SHOW hint") {
            $(this).text("HIDE hint");
        }
        else {
            $(this).text("SHOW hint");
        }
        return false;
    });

    $('.controll-panel a[rel="analysis"]').click(function (e) {
        $('.analysis[id=analysis_' + $(this).parent().parent().parent().attr("id") + ']').toggle();
        if ($(this).text() == "SHOW Analysis") {
            $(this).text("HIDE Analysis");
        }
        else {
            $(this).text("SHOW Analysis");
        }
        return false;
    });

    $('.controll-panel a[rel="correct answer"]').click(function (e) {        
        if ($(this).text() == "SHOW Correct Answer") {
            $(this).text("HIDE Correct Answer");
            $('ul[id=ul_' + $(this).parent().parent().parent().attr("id") + ']').children('.cor').addClass("corect");
        }
        else {
            $(this).text("SHOW Correct Answer");
            $('ul[id=ul_' + $(this).parent().parent().parent().attr("id") + ']').children('.cor').removeClass("corect");
        }
        return false;
    });

    $('.controll-panel a[rel="video answer"]').click(function (e) {
        var item = $(this).parent().parent().parent().parent().children(".video-answer");

        if ($(this).text() == "SHOW video explanation") {

            $('.controll-panel a[rel="video answer"]').text("SHOW video explanation");
            $(".video-answer").Hide();
            $(".player-placeholder").html('');


            item.Show();
            item.children(".player-placeholder").append('<div id="video-player"></div>');
            var time = item.children("input[type=hidden]").val();
            
            $("#HFVCode").val($(this).attr("href"));
            PlayerObject.Root = window['WebRoot'] == undefined ? Root : window['WebRoot'];
            PlayerObject.ShowUsername = true;
            PlayerObject.Height = 480;
            PlayerObject.Width = 640;
            PlayerObject.RequestVideo(false, $(this).attr("href"));
            PlayerObject.RequestComplete = function () {
                jwplayer(PlayerObject.VideoPlayerDivID).seek(time);
            }
            $(this).text("hide video explanation");
        }
        else {
            item.Hide();
            item.children(".player-placeholder").html('');
            $(this).text("SHOW video explanation");
        }
        return false;
    });

    $('li').each(function () {
        if ($(this).index() > 0 && (($(this).hasClass('medium') && $(this).index() % 4 == 0) || ($(this).hasClass('small') && $(this).index() % 8 == 0))) {
            $(this).attr('style', 'clear:both');
        }
    });
});