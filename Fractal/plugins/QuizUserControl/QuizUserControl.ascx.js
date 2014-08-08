$(document).ready(function () {
    
    $('#SubmitQuiz').click(function (e) {
        $(DialogDivID).dialog("option",
        {
            title: "გამოცდის დასრულება",
            width: 450,
            buttons:
            {
                "კი": function () {
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
                        url: "/api/",
                        type: "POST",
                        data: { iud:9, action: "submit", data: xml, params: $("#HFData").val() },
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
                "არა": function () {
                    $(this).dialog("close");
                }
            }
        });

        $(DialogDivID).dialog("open");

        return false;
    });

    $('.controll-panel .question_hint').click(function (e) {
        $('.hint[id=hint_' + $(this).parent().parent().parent().attr("id") + ']').toggle();
        if ($(this).text() == "მაჩვენე მინიშნება") {
            $(this).text("დამალე მინიშნება");
        }
        else {
            $(this).text("მაჩვენე მინიშნება");
        }
        return false;
    });
    
    $('li').each(function () {
        if ($(this).index() > 0 && (($(this).hasClass('medium') && $(this).index() % 4 == 0) || ($(this).hasClass('small') && $(this).index() % 8 == 0))) {
            $(this).attr('style', 'clear:both');
        }
    });
});