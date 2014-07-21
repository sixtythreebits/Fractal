var PostUrl;
var PreviewUrl;
var AssetDownloadUrl;
var AssetPreviewUrl;
var QuizID;
var SelectedQuestionID = null;
var VideoAnswerID = null;
var SelectedAnswerID = null;
var ConfirmDeleteQuestionAsset;
var ConfirmDeleteQuestionVideo;
var ConfirmDeleteQuestion;
var ConfirmDeleteAnswer;
var VideoAssetsPopupUrl;
var TypingTimeout;
var iud;
                                                                                                                                                                 //Here in <a></a> goes question text
var questionHTML = '<div class="question-box {0}"><div><em class="arrow"></em><span class="drag"></span><span class="index">1</span><span class="expand"></span><span class="question-text"><span><a></a></span></span><em class="asset">Asset: <b></b></em><a class="addAnswer">Add Answer</a></div><ul class="answer-box ui-sortable ui-droppable"></ul></div>';
var answerHTML = '<li id="{0}"><span class="alphabet">A</span><span><input type="checkbox" /></span><label class="answer-text"><span><a></a></span></label></li>';	

$(document).ready(function () {

    iud = $("#HFiud").val();
    QuizID = $("#HFQuizID").val();
    PostUrl = "/plugins/QuizMaker/QuizMakerApi.ashx";
    PreviewUrl = "/administration/popuppages/QuizPreview.aspx?id="+QuizID;
    AssetDownloadUrl = "/download/";
    AssetPreviewUrl = "/preview/";    
    VideoAssetsPopupUrl = $("#HFVideoAssetsPopupUrl").val();

    ConfirmDeleteQuestionAsset = $("#HFConfirmDeleteQuestionAsset").val();
    ConfirmDeleteQuestionVideo = $("#HFConfirmDeleteQuestionVideo").val();
    ConfirmDeleteQuestion = $("#HFConfirmDeleteQuestion").val();
    ConfirmDeleteAnswer = $("#HFConfirmDeleteAnswer ").val();
    
    $(".icon.upload").click(function (e) {
        FancyBox.Init({
            href: $("#HFUploadQuestionsUrl").val(),
            height: 450,
            width: 420
        }).ShowPopup();
        return false;
    });

    $("#ShowPreviewButton").click(function (e) {
        e.preventDefault();
        FancyBox.Init({
            href: PreviewUrl + "&title=" + $("#QuizCaptionTextBox").val(),
            height: 768,
            width: 1024
        }).ShowPopup();        
    });

    $(".qz-question,.qz-bank,.qz-answer").aToolTip({});
    $(".qz-bank,.qz-question").click(function () {
        return false;
    });

    $("#VideoTimeTextBox").mask("99:99:99");

	$('.attach .delete').click(function (e) {
		e.preventDefault();
		$(this).parent().parent('li').slideUp(400, function() { $(this).remove(); });
	});

	$(".question-td").on("click", ".collapse", function (e) {
	    e.preventDefault();
	    $(this).removeClass('collapse').addClass('expand');
	    $(this).parent('div').next('ul').slideUp();
	});
	$(".question-td").on("click", ".expand", function (e) {
	    e.preventDefault();
	    $(this).removeClass('expand').addClass('collapse');
	    $(this).parent().next('ul').slideDown();
	});

	InitQuizProperties();
			
    // dropzone  --------------------------------------------
	Dropzone.autoDiscover = false;
	$('div#dropz').dropzone({ url: PostUrl },
		Dropzone.options.dropz = {
		    thumbnailWidth: 420,
		    thumbnailHeight: 250,		    
		    addRemoveLinks: true,
		    uploadMultiple: true,
		    acceptedFiles: "image/*",
		    dictRemoveFile: "Clear Image",
		    dictDefaultMessage: "გადმოათრიეთ ან <strong>ატვირთეთ</strong> სურათი კომპიუტერიდან.",
		}
	);

	$('div.dropzone').each(function () {
	    Dropzone.forElement(this).on('sending', function (file, xhr, formData) {
	        GMLoader.open();
	        formData.append("iud", 6);
	        formData.append("id", QuizID);
	        formData.append("qid", SelectedQuestionID);
	    });

	    Dropzone.forElement(this).on('success', function (file, res) {	        
	        if (res.length > 0) {
	            var json = JSON.parse(res);
	            $(".question-img").Show();
	            $(".question-img img").attr("src", AssetDownloadUrl + json.asset_id);
	            $(".question-box." + SelectedQuestionID + " em.asset b").text(json.caption);
	        }
	        else {
	            alert(Abort);
	        }
	    });

	    Dropzone.forElement(this).on('complete', function () {
	        $("#dropz").removeClass("dz-started");
	        GMLoader.close();
	    });	    	    
	});
	//text animation on mouseover
    //$('.question-box .question-text a,.answer-box label a').mouseover(function(){
	$(".question-td").on("mouseover", ".question-box .question-text a,.answer-box label a", function () {
	    var SpanWidth = $(this).parent().parent().width();
	    var LinkWidth = $(this).width();
	    if (LinkWidth > SpanWidth) {
	        speed = 4000 + 13 * (LinkWidth - SpanWidth);
	        if (LinkWidth < 600) { speed = 4000 }
	        $(this).stop().animate({ 'left': -(LinkWidth - SpanWidth) }, speed)
	    }
	});
    //$('.question-box .question-text a,.answer-box label a').mouseout(function(){
	$(".question-td").on("mouseout", ".question-box .question-text a,.answer-box label a", function () {
		$(this).stop().animate({'left': 0},1000)
	});
	
	// on click show rigt content --------------------------------------------
	$('.show_quizz_properties').click(function (e) {
	    e.preventDefault();
	    ClearAllAreas();
		targ = $(this).attr('href');
		targ=targ.replace('#','');
		$('.properties-td > div').hide();
		$('.properties-td > div.' + targ).fadeIn();
		InitQuizProperties();
	});
	
    // on question/answer text click show rigt content
	$(".question-td").on("click", ".question-box > div", function (e) {
	    var _this = $(this);
	    var qid = _this.parents(".question-box").attr("class").split(' ')[1];
	    InitQuestionProperties(_this, qid);
	});

	//$("#TagsTextBox").tagsManager({
	//    tagsContainer: ".tags-container",
	//    output: "#HFTags"
	//});

	//$("#TagsTextBox").keyup(function (e) {
	//    $(this).val($(this).val().toUpperCase());
	//});

	$("#ScoreTextBox").numericInput({
	    allowNegative: false,
	    allowFloat: false
	});

	$("#ScoreTextBox").keyup(function (e) {
	    if (!(parseInt($(this).val()) > 0)) {
	        $(this).val("1");
	    }
	});
	
	$("#ClearQuestionImage").click(function (e) {
	    e.preventDefault();
	    $(DialogDivID).html(ConfirmDeleteQuestionAsset);
	    $(DialogDivID).dialog({
	        title: "სურათის წაშლა",
	        width: 200,
	        buttons:
            {
                "Yes": function () {
                    $.ajax({
                        url: PostUrl,
                        type: "POST",
                        data: { id: QuizID, iud: "7", qid: SelectedQuestionID },
                        dataType: "text",
                        beforeSend: function () {
                            GMLoader.open();
                        },
                        success: function (res) {
                            if (res == "success") {
                                $(".question-img").Hide();
                                $(".question-img img").removeAttr("src");
                                $(".question-box." + SelectedQuestionID + " em.asset b").text('');
                            }
                            else {
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
                    $(this).dialog("close");
                },
                "No": function () {
                    $(this).dialog("close");
                }
            }
	    });
	    $(DialogDivID).dialog("open");
	   
	});

	$(".icon.library").click(function (e) {
	    e.preventDefault();
	    FancyBox.Init({
	        href: VideoAssetsPopupUrl,
	        height: 650,
	        width: 835
	    }).ShowPopup();
	});

	$("#SetVideoTimeButton").click(function (e) {
	    e.preventDefault();       
	    $("#VideoTimeTextBox").val(SecondsToHMS(Math.floor(jwplayer(PlayerObject.VideoPlayerDivID).getPosition()), 0));
	});

	$("#ClearVideo").click(function () {	    
	    $(DialogDivID).html(ConfirmDeleteQuestionVideo);
	    $(DialogDivID).dialog({
	        title: "Remove Video",
	        width: 200,
	        buttons:
            {
                "Yes": function () {
                    VideoAnswerID = null;
                    $(".question-edit .video").Hide();
                    $("#SaveQuestionButton").trigger("click");
                    $(this).dialog("close");
                },
                "No": function () {                    
                    $(this).dialog("close");
                }
            }
	    });
	    $(DialogDivID).dialog("open");

	    return false;
	});

	$("#SaveQuestionButton").click(function () {	    
	    SaveQuestion();
	    return false;
	});
        
	$(".question-td").on("click", ".answer-box li", function (e) {
	    if (SelectedAnswerID != $(this).attr("id")) {
	        ClearAllAreas();
	        $('.properties-td > div').hide();
	        $('.properties-td > div.answer-edit').fadeIn();
	        $('.answer-box').children('li').removeClass('selected');
	        $('.question-box').children('div').removeClass('selected');
	        $(this).addClass('selected');	        
	        InitAnswerData($(this).attr("id"));
	    }
	});
	
	$("#DeleteQuestion").click(function (e) {
	    e.preventDefault();	    
	    $(DialogDivID).dialog({
	        title: "შეკითხვის წაშლა",
	        width: 400,
	        buttons:
            {
                //"Disconnect From Quiz": function () {
                //    DeleteQuestion(0);
                //    $(this).dialog("close");
                //},
                "კი": function () {
                    DeleteQuestion(1);
                    $(this).dialog("close");
                },
                "არა": function () {
                    $(this).dialog("close");
                }
            }
	    });
	    $(DialogDivID + " > p").text(ConfirmDeleteQuestion);
	    $(DialogDivID).dialog("open");
	});

	$("#BankSearchTextBox").keydown(function (e) {
	    var key = e.keyCode;
	    var val = $(this).val()

	    if (key == 13 && val.length > 2) {
	        e.preventDefault();
	    }
	    else {
	        StartTypingTimer();
	    }
	});

	ApplyAnswerSortable();
    	
	// Sortable,Draggable,Droppable --------------------------------------------			
	$(function() {
		//sortable
		$('.question-td').sortable({
			revert: true,
			placeholder: "drop-place",
			handle: ".drag",
			update: function (ev, ui) {
			    var _this = $(this);
			    var itemClass = ui.item.attr("class")
			    
			    if (ui.item.hasClass("qz-question")){
			        CreateNewQuestion($(ui.item).index(), $(ui.item));			        
			    }
			    else if (ui.item.hasClass("bank-question")) {
			        AppendQuestionFromBank(ui.item.index(), ui.item);
			    }
			    else {
			        UpdateQuestionSorting();
			    }
				
			    FixQuestionNumbering();
			}
		});
				
		//draggable
		$('.qz-question').draggable({
			connectToSortable: '.question-td',
			helper: 'clone',
			revert: 'invalid',
		});
						
		$('.qz-bank').draggable({
			helper: 'clone',
		  	revert: 'invalid',
		});
		
		$('.qz-answer').draggable({
		  connectToSortable: '.answer-box',
		  helper: 'clone',
		  revert: 'invalid'
		});
		$('.question-td,.answer-box').disableSelection();
		
		
		//droppable
		$('.question-td').droppable({
			accept: ".qz-bank,.qz-question",
			drop: function( event, ui ) {
				var draggableClass = ui.draggable.attr("class")
				if (draggableClass == 'qz-bank ui-draggable') {
				    ClearAllAreas();				    
				    $('.properties-td > div').hide();                    
					$('.properties-td > div.question-from-bank').fadeIn();
				}
				if (draggableClass == 'qz-question ui-draggable') {				    
					$('.properties-td > div').hide();
					$('.properties-td > div.question-edit').fadeIn();
				}
			}
		});

		$('.question-td').on('click', '.addAnswer', function () {
		    var qid = $(this).parent().parent().attr("class").split(' ')[1];
		    var NewLi = $('<li></li>');

		    $(this).parent().next('ul').prepend(NewLi)
		    CreateNewAnswer(0, qid, NewLi, $(".question-box." + qid + " ul.answer-box"));

		    var expand = $(this).parent().find(".expand");
		    if (expand.length > 0) {
		        expand.removeClass("expand").addClass("collapse");
		        $(this).parent().next().slideDown();
		    }
		});

	});			

	$(".toggler").change(function () {
	    var parent = $(this).parent().attr("class");
	    var val = $(this).attr("class").split(" ").length > 1 ? "1" : "0";

	    switch (parent) {
	        case "is_published":
	            {
	                $("#HFIsPublished").val(val);
	                break;
	            }
	        case "aq-practice":
	            {
	                $("#HFPractice").val(val);
	                break;
	            }
	        case "aq-hints":
	            {
	                $("#HFHints").val(val);
	                break;
	            }
	        case "aq-analysis":
	            {
	                $("#HFAnalysis").val(val);
	                break;
	            }
	        case "aq-other":
	            {
	                $("#HFOther").val(val);
	                break;
	            }
	        case "aq-skip":
	            {
	                $("#HFSkip").val(val);
	                break;
	            }
	        default:
	            {
	                $("#HFCorrect").val(val);
	                break;
	            }
	    }	    
	});

	$("#SaveAnswerButton").click(function (e) {
	    e.preventDefault();
	    SaveAnswer(0);
	});

	$(".question-td").on("click", ".answer-box input", function (e) {
	    if (SelectedAnswerID != $(this).parent().parent().attr("id")) {
	        if ($(this).is(":checked")) {
	            $(this).prop("checked", false);
	        }
	        else {
	            $(this).prop("checked", true);
	        }
	        e.preventDefault();
	    }
	    else {
	        SaveAnswer(1);
	    }
	});

	$("#DeleteAnswerButton").click(function (e) {
	    e.preventDefault();
	    DeleteAnswer();
	});

	$("#SaveButton").click(function (e) {
	    e.preventDefault();
	    switch (iud)
	    {
	        case "1":
	            {
	                SaveCourseQuiz();
	                break;
	            }
	        case "2":
	            {
	                SaveCourseSectionStudyQuiz();
	                break;
	            }
	        case "3":
	            {
	                SaveAssetQuiz();
	                break;
	            }
	        default:
	            {
	                SaveQuizBasicInfo();
	                break;
	            }
	    }	    
	});
});

function ClearQuestionArea() {
    $(".question-box." + SelectedQuestionID + " > div").removeClass("selected");    
    $(".question-img").Hide();
    $(".question-img img").removeAttr("src");
    $("#QuestionTextBox").val("");
    $("#ScoreTextBox").val("1");
    //$("#TagsTextBox").tagsManager('empty');
    $("#HintTextBox").val("");
    $("#AnalysisTextBox").val("");
    $(".question-edit .vid-answer div a").Hide();
    $(".question-edit .video").Hide();
    $("#VideoTimeTextBox").val("");
    SelectedQuestionID = null;
    VideoAnswerID = null;
}

function ClearBankArea() {
    $(".bank-content").html('');
}

function ClearAnswerArea() {
    $("#AnswerTextBox").val('');
    $("#HFCorrect").val("0");
    $("div.toggler").removeClass("on");
    $("div.toggler > .pad").css({ "left": "40px" });
    $("#ExplanationTextBox").val('');
    SelectedAnswerID = null;
}

function ClearAllAreas() {
    ClearQuestionArea();
    ClearBankArea();
    ClearAnswerArea();
}

/*-------------------------------------------- Question Stuff --------------------------------------------*/
function ApplyBankQuestionsDragable() {
    $('.bank-content > div').draggable({
        connectToSortable: '.question-td',
        helper: 'clone',
        revert: 'invalid',
        handle: ".drag"
    });
}

function InitQuestionProperties(_this, qid) {
    if (qid != SelectedQuestionID) {        
        $.ajax({
            url: PostUrl,
            type: "POST",
            data: { id: QuizID, iud: "1", qid: qid },
            dataType: "text",
            beforeSend: function () {
                $(".tags-container").html('');
                GMLoader.open();
            },
            success: function (res) {
                if (res.length > 0) {
                    var q = JSON.parse(res);
                    ClearQuestionArea();                    
                    SelectedQuestionID = qid;

                    $("#QuestionTextBox").val(q.question);
                    $("#HintTextBox").val(q.hint);
                    $("#AnalysisTextBox").val(q.analysis);
                    $("#ScoreTextBox").val(q.score);

                    if (q.asset_id == null) {
                        $(".question-img").Hide();
                    }
                    else {
                        $(".question-img").Show();
                        $(".question-img img").attr("src", AssetDownloadUrl + q.asset_id);
                    }

                    //if (q.tags != null) {
                    //    $(q.tags.split(',')).each(function (i, e) {
                    //        var index = i + 1;
                    //        $("#TagsTextBox").tagsManager('pushTag', e);
                    //    });
                    //}

                    if (q.video_answer_id != null) {
                        ShowQuestionVideo(q.video_answer_id, q.video_answer_caption, q.player_data, q.time);
                    }

                    $('.properties-td > div').hide();
                    $('.properties-td > div.question-edit').fadeIn();
                    $('.question-box').children('div').removeClass('selected');
                    $('.answer-box').children('li').removeClass('selected');
                    _this.parents('.question-box').children('div').addClass('selected');
                }
                else {
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
    }
}

function CreateNewQuestion(Number, DroppedItem) {
    var x =
    "<data>" +
        "<quiz_id>" + QuizID + "</quiz_id>" +
        "<question></question>" +
        "<score>1</score>" +
        "<number>" + (Number + 1) + "</number>" +
    "</data>";
    

    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "0", qid: SelectedQuestionID, data: x },
        dataType: "text",
        beforeSend: function () {
            $(".tags-container").html('');
            GMLoader.open();
        },
        success: function (res) {
            if (res.length > 0) {
                var q = JSON.parse(res);
                DroppedItem.replaceWith(questionHTML.replace("{0}", q.id));
                FixQuestionNumbering();
                ApplyAnswerSortable();                                                
                $(".question-box." + q.id + " > div").trigger("click");
            }
            else {
                DroppedItem.replaceWith('');
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
}

function SaveQuestion() {    
    var x =
        "<data>" +
            "<id>{0}</id>" +
            "<question><![CDATA[" + $("#QuestionTextBox").val() + "]]></question>" +
    	    "<hint><![CDATA[" + $("#HintTextBox").val() + "]]></hint>" +
            "<analysis><![CDATA[" + $("#AnalysisTextBox").val() + "]]></analysis>" +
            "<score>" + $("#ScoreTextBox").val() + "</score>";
    //if (VideoAnswerID != null) {
    //    x += (
    //            "<video_answer_id>" + VideoAnswerID + "</video_answer_id>" +
    //            "<time><![CDATA[" + $("#VideoTimeTextBox").val() + "]]></time>"
    //         );
    //}
    //var Tags = $("#HFTags").val();
    //if (Tags.length > 0) {
    //    x += "<tags>";
    //    $(Tags.split(",")).each(function (i, e) {
    //        x += ("<tag><name><![CDATA[" + e + "]]></name></tag>");
    //    });
    //    x += "</tags>";
    //}
    x += "</data>";    
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "2", qid: SelectedQuestionID, data: x },
        dataType: "text",
        beforeSend: function () {            
            GMLoader.open();
        },
        success: function (res) {
            if (res == "success") {
                $(".question-box." + SelectedQuestionID + " .question-text a").text($("#QuestionTextBox").val());
            }
        },
        error: function (res) {
            alert(Abort);
        },
        complete: function () {
            GMLoader.close();
        }
    });
}

function UpdateQuestionSorting() {
    var x = "<data><questions>";
    $(".question-box").each(function (i, e) {
        x += ("<question><id>" + $(e).attr("class").replace("question-box ", "") + "</id><number>" + (i + 1) + "</number></question>");
    });
    x += "</questions></data>";

    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "3", data: x },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res == "success") {
            }
        },
        error: function (res) {
            alert(Abort);
        },
        complete: function () {
            GMLoader.close();
        }
    });
}

function DeleteQuestion(iud) {
    iud = iud == 0 ? 4 : 5;
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: iud, qid: SelectedQuestionID },
        dataType: "text",
        beforeSend: function () {            
            GMLoader.open();
        },
        success: function (res) {
            if (res.length > 0) {
                var json = JSON.parse(res);
                if (json.result == "success") {                    
                    $("div." + SelectedQuestionID).slideUp(200, function () {
                        $(this).remove();
                        FixQuestionNumbering();
                        ClearQuestionArea();
                    });
                    $('.properties-td > div').hide();
                    $('.properties-td > div.quizz-properties').fadeIn();
                }
                else {
                    alert(json.error_message);
                }
            }
            else {
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
}

function FixQuestionNumbering() {
    $(".question-td").children('div').each(function (i, e) {
        $(this).children().find('.index').text(i + 1);
    });
}

function OnTeacherAssetAddButtonClicked(item, ChildWindow) {    
    
    var AssetID = item.parent().attr("id");
    var AssetCaption = item.prev().prev().children("h1").children("a").text();

    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "8", asset_id: AssetID },
        dataType: "text",
        beforeSend: function () {
            item.addClass("loader");
        },
        success: function (res) {
            if (res.length > 0) {
                ChildWindow.ClosePopup(true);
                ShowQuestionVideo(AssetID, AssetCaption, res, 0);
            }
            else {
                alert(Abort);                
            }
        },
        error: function (res) {
            alert(Abort);
        },
        complete: function () {
            item.removeClass("loader");
        }
    });
}

function ShowQuestionVideo(AssetID, AssetCaption, vcode, time) {
    VideoAnswerID = AssetID;
    $(".question-edit .video").Show();
    $(".vid-answer .icon.video").text(AssetCaption);
    $("#VideoTimeTextBox").val(SecondsToHMS(time,0));

    PlayerObject.Root = WebRoot;
    $("#HFVCode").val(vcode);
    
    PlayerObject.RequestVideo(false, vcode);    
}

function StartTypingTimer() {
    if (TypingTimeout != undefined)
        clearTimeout(TypingTimeout);

    TypingTimeout = setTimeout(function () {
        var val = $("#BankSearchTextBox").val();
        if (val.length > 2) {
            $('.search span').removeClass('hidden');
            DoBankSearch();
        }
    }, 500);
}

function DoBankSearch() {
    ClearBankArea();
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "9", user_id: $("#HFOwnerID").val(), keywords: $("#BankSearchTextBox").val() },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            
            if (res.length > 0) {
                var j = JSON.parse(res);
                var html="";
                $(j).each(function (i, e) {
                    html += ('<div class="ui-draggable bank-question ' + e.id + '"><div><span class="drag"></span>');
                    if (e.asset_id != null) {
                        html += ('<img src="' + AssetPreviewUrl + e.preview_data + '" width="113" height="85" />');
                    }
                    html+=('<p>'+e.question+'</p></div>');
                    if (e.tags != null) {
                        html += '<span>';
                        $(e.tags).each(function (j, t) {
                            html += ('<a href="#">' + t + '</a> ')
                        });                        
                    }
                    html += '</span></div>';
                });                
                $(".bank-content").append(html);
                ApplyBankQuestionsDragable();
            }
        },
        error: function (res) {
            alert(Abort);
        },
        complete: function () {
            GMLoader.close();
        }
    });
}

function AppendQuestionFromBank(Number, DroppedItem) {

    var BankQuestionSelector = "." + DroppedItem.attr("class").split(' ').join('.');
    var qid = DroppedItem.attr("class").split(' ')[2];
    var question = DroppedItem.find("p").text();
        
    var x =
       "<data>" +
           "<quiz_id>" + QuizID + "</quiz_id>" +
           "<question_id>" + qid + "</question_id>" +
           "<number>" + (Number + 1) + "</number>" +
       "</data>";


    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "10", data: x },
        dataType: "text",
        beforeSend: function () {
            $(".tags-container").html('');
            GMLoader.open();
        },
        success: function (res) {
            if (res.length > 0) {
                var json = JSON.parse(res);
                if (json.result == "success") {
                    DroppedItem.replaceWith(questionHTML.replace("{0}", qid));
                    $(".question-box." + qid + " .question-text a").text(question);                    
                    $(".question-box." + qid + " em.asset b").text(json.asset_caption);
                    FixQuestionNumbering();
                    $(BankQuestionSelector).slideUp();
                    ApplyAnswerSortable();
                }
            }
            else {
                alert(Abort);
                DroppedItem.replaceWith('');
            }
        },
        error: function (res) {
            alert(Abort);
        },
        complete: function () {
            GMLoader.close();
        }
    });
}
/*-------------------------------------------/ Question Stuff --------------------------------------------*/

/*-------------------------------------------- Answer Stuff --------------------------------------------*/
function ApplyAnswerSortable() {
    $('.answer-box').sortable({
        revert: true,
        placeholder: "drop-place",
        update: function (ev, ui) {
            var itemClass = ui.item.attr("class")
            if (itemClass == 'qz-answer ui-draggable') {

                var qid = ui.item.parent().parent().attr("class").split(' ')[1]
                CreateNewAnswer(ui.item.index(), qid, ui.item, $(this));
            }
            else {
                UpdateAnswerSorting($(ui.item).parent());
            }
        }
    });
}

function FixAnswerNumbering(AnswersUl) {    
    AnswersUl.children('li').each(function (i, e) {        
        $(this).find('.alphabet').text(String.fromCharCode(65 + i));
    });
}

function CreateNewAnswer(Number, qid, DroppedItem, AnswerContainer) {

    var x =
   "<data>" +
       "<quiz_id>" + QuizID + "</quiz_id>" +
       "<question_id>" + qid + "</question_id>" +
       "<number>" + (Number + 1) + "</number>" +
   "</data>";

    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "11", data: x },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res.length > 0) {
                var a = JSON.parse(res);
                DroppedItem.replaceWith(answerHTML.replace("{0}", a.id));
                FixAnswerNumbering(AnswerContainer);
                $("#" + a.id).trigger("click");
            }
            else {
                DroppedItem.replaceWith('');
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
}

function InitAnswerData(id) {
    
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "12", aid: id },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res.length > 0) {
                var a = JSON.parse(res);
                SelectedAnswerID = id;
                $("#AnswerTextBox").val(a.answer);
                $("#ExplanationTextBox").val(a.explanation);
                $("#HFCorrect").val(a.is_correct);
                if (a.is_correct == "1") {
                    customForm.toggleIt($("div.toggler"), 180);
                }
            }
            else {
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
}

function SaveAnswer(iud) {
    var x;    
    if (iud == 0) {
        var IsCorrect = $("#HFCorrect").val();
        x =
        "<data>" +
            "<id>" + SelectedAnswerID + "</id>" +
            "<answer><![CDATA[" + $("#AnswerTextBox").val() + "]]></answer>" +
            "<explanation><![CDATA[" + $("#ExplanationTextBox").val() + "]]></explanation>" +
            "<is_correct>" + IsCorrect + "</is_correct>" +
        "</data>";
    }
    else {
        x =
        "<data>" +
            "<id>" + SelectedAnswerID + "</id>" +
            "<is_correct>" + ($("#" + SelectedAnswerID + " input").attr("checked") == "checked" ? "0" : "1") + "</is_correct>" +
        "</data>";
    }

        
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "13", data: x },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res == "success") {
                if (iud == 0) {
                    $("#" + SelectedAnswerID + " label span a").text($("#AnswerTextBox").val());
                    if (IsCorrect == "1") {
                        $("#" + SelectedAnswerID + " input").attr("checked", "checked");
                    }
                    else {
                        $("#" + SelectedAnswerID + " input").removeAttr("checked");
                    }
                }
                else {
                    customForm.toggleIt($("div.toggler"), 180);
                }
            }
            else {
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
}

function UpdateAnswerSorting(AnswerContainer) {
    var x = "<data>";
    AnswerContainer.children().each(function (i, e) {        
        x += (
        "<answer>" +
            "<id>" + $(e).attr("id") + "</id>" +
            "<number>" + (i + 1) + "</number>" +
        "</answer>"
        );
    });
    x += "</data>";
    FixAnswerNumbering(AnswerContainer);
    $.ajax({
        url: PostUrl,
        type: "POST",
        data: { id: QuizID, iud: "14", data: x },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res != "success") {               
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
}

function DeleteAnswer() {
    $(DialogDivID).html(ConfirmDeleteAnswer);
    $(DialogDivID).dialog({
        title: "შეკითხვის წაშლა",
        width: 200,
        buttons:
        {            
            "კი": function () {
                var AnswerContainer = $("#" + SelectedAnswerID).parent();
                $.ajax({
                    url: PostUrl,
                    type: "POST",
                    data: { id: QuizID, iud: "15", aid: SelectedAnswerID },
                    dataType: "text",
                    beforeSend: function () {
                        GMLoader.open();
                    },
                    success: function (res) {
                        if (res == "success") {
                            $("#" + SelectedAnswerID).slideUp(200, function () {
                                $(this).remove();
                                FixAnswerNumbering(AnswerContainer);
                                ClearAnswerArea();
                            });
                            $('.properties-td > div').hide();
                            $('.properties-td > div.quizz-properties').fadeIn();
                        }
                        else {
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
                $(this).dialog("close");
            },
            "არა": function () {
                $(this).dialog("close");
            }
        }
    });
    $(DialogDivID).dialog("open");
}
/*-------------------------------------------/ Answer Stuff --------------------------------------------*/
/*-------------------------------------------- Quiz Stuff --------------------------------------------*/
var RequiredQuizCaption;
var RequiredStartDate;
var RequiredEndDate;
var StartGreaterThanEnd;
var RequiredGradeDate;

var QuizError = null;
var StartDateError = null;
var EndDateError = null;
var GradeDateError = null;

function InitQuizProperties() {
    switch (iud) {
        case "1":
            {
                InitCourseQuizProperties();
                break;
            }
        case "2":
            {
                InitCourseStudyQuestionProperties();
                break;
            }
        case "3":
            {
                InitAssetQuizProperties();
                break;
            }

    }
}

function InitCourseQuizProperties() {
    RequiredQuizCaption = $("#HFRequiredQuizCaption").val();
    RequiredStartDate = $("#HFRequiredStartDate").val();
    RequiredEndDate = $("#HFRequiredEndDate").val();
    StartGreaterThanEnd = $("#HFStartGreaterThanEnd").val();
    RequiredGradeDate = $("#HFRequiredGradeDate").val();

    if ($("#HFIsPublished").val() == "1") {
        customForm.toggleIt($(".is_published .toggler"), 180);
    }

    $("#CourseQuizStartDate,#CourseQuizEndDate,#CourseQuizGradeDate").datetimepicker(
    {
        dateFormat: "M d, yy",
        timeFormat: "hh:mm tt",
        changeMonth: true,
        changeYear: true,
        beforeShow: function () {
            setTimeout(function () {
                $('.ui-datepicker').css('z-index', 99999999999999);
            }, 0);
        }
    });

    $("#CourseQuizStartDate,#CourseQuizEndDate,#CourseQuizGradeDate").attr("readonly", "readonly");

    $("#QuizCaptionTextBox").focusout(function () {
        ValidateQuiz();
    });

    $("#CourseQuizStartDate").focusout(function () {
        setTimeout("ValidateCourseQuizStartDate();", 200);
    });

    $("#CourseQuizStartDate").change(function () {
        setTimeout("ValidateCourseQuizStartDate();", 200);
    });

    $("#CourseQuizEndDate").focusout(function () {
        setTimeout("ValidateCourseQuizEndDate();", 200);
    });

    $("#CourseQuizEndDate").change(function () {
        setTimeout("ValidateCourseQuizEndDate();", 200);
    });

    $("#CourseQuizGradeDate").focusout(function () {
        setTimeout("ValidateCourseQuizGradeDate();", 200);
    });

    $("#CourseQuizGradeDate").change(function () {
        setTimeout("ValidateCourseQuizGradeDate();", 200);
    });

}

function InitCourseStudyQuestionProperties() {
    RequiredQuizCaption = $("#HFRequiredQuizCaption").val();

    if ($("#HFIsPublished").val() == "1") {
        customForm.toggleIt($(".is_published .toggler"), 180);
    }
}

function InitAssetQuizProperties() {
    $("#TimeTextBox").mask("99:99:99");

    if ($("#HFPractice").val() == "1") {
        customForm.toggleIt($(".aq-practice .toggler"), 180);
    }

    if ($("#HFHints").val() == "1") {
        customForm.toggleIt($(".aq-hints .toggler"), 180);
    }

    if ($("#HFAnalysis").val() == "1") {
        customForm.toggleIt($(".aq-analysis .toggler"), 180);
    }

    if ($("#HFOther").val() == "1") {
        customForm.toggleIt($(".aq-other .toggler"), 180);
    }

    if ($("#HFSkip").val() == "1") {
        customForm.toggleIt($(".aq-skip .toggler"), 180);
    }    
}


function ValidateQuiz() {
    var val = $("#QuizCaptionTextBox").val()
    if (val.length > 0) {
        SetInputFieldSuccess("caption", "QuizError");
    }
    else {
        SetInputFieldError("caption", RequiredQuizCaption, "QuizError");
    }
}

function ValidateCourseQuizStartDate() {
    var d1 = $("#CourseQuizStartDate").val();
    var d2 = $("#CourseQuizEndDate").val();

    if (d1.length == 0) {
        SetInputFieldError("start-date", RequiredStartDate, "StartDateError");
    }
    else {
        if (d2.length > 0) {
            SetInputFieldSuccess("start-date", "StartDateError");
            ValidateCourseQuizEndDate();
        }
        else {
            SetInputFieldSuccess("start-date", "EndDateError");
        }
        SetInputFieldSuccess("start-date", "StartDateError");
    }
}

function ValidateCourseQuizEndDate() {
    var d1 = $("#CourseQuizStartDate").val();
    var d2 = $("#CourseQuizEndDate").val();

    if (d2.length == 0) {
        SetInputFieldError("end-date", RequiredEndDate, "EndDateError");
    }
    else {
        d1 = new Date(d1);
        d2 = new Date(d2);
        if (d1 > d2) {
            SetInputFieldError("end-date", StartGreaterThanEnd, "EndDateError");
        }
        else {
            SetInputFieldSuccess("end-date", "EndDateError");
        }
    }
}

function ValidateCourseQuizGradeDate() {
    var val = $("#CourseQuizGradeDate").val();
    if (val.length == 0) {
        SetInputFieldError("grade-date", RequiredGradeDate, "GradeDateError");
    }
    else {
        SetInputFieldSuccess("grade-date", "GradeDateError");
    }
}


function IsQuizBasicInfoFormValid()
{
    if (QuizError == null) {
        ValidateQuiz();
    }

    return QuizError == false;
}

function IsCourseQuizFormValid() {

    if (QuizError == null) {
        ValidateQuiz();
    }

    if (StartDateError == null) {
        ValidateCourseQuizStartDate();
    }

    if (EndDateError == null) {
        ValidateCourseQuizEndDate();
    }

    if (GradeDateError == null) {
        ValidateCourseQuizGradeDate();
    }

    return QuizError == false && StartDateError == false && EndDateError == false && GradeDateError == false

}


function SaveQuizBasicInfo() {
    if (IsQuizBasicInfoFormValid()) {
        $.ajax({
            url: PostUrl,
            type: "POST",
            data: { id: QuizID, iud: "16", caption: $("#QuizCaptionTextBox").val() },
            dataType: "text",
            beforeSend: function () {
                GMLoader.open();
            },
            success: function (res) {
                if (res == "success") {
                    $(".quiz-title span").text($("#QuizCaptionTextBox").val());
                }
                else {
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
    }
}

function SaveCourseQuiz() {
    if (IsCourseQuizFormValid()) {
        var x =
        "<data>" +
            "<quiz_id>" + QuizID + "</quiz_id>" +
            "<caption><![CDATA[" + $("#QuizCaptionTextBox").val() + "]]></caption>" +
            "<course_id>" + gup("id") + "</course_id>" +
            "<start_date>" + $("#CourseQuizStartDate").val() + "</start_date>" +
            "<end_date>" + $("#CourseQuizEndDate").val() + "</end_date>" +
            "<grade_date>" + $("#CourseQuizGradeDate").val() + "</grade_date>" +
            "<is_published>" + $("#HFIsPublished").val() + "</is_published>" +
        "</data>";
        $.ajax({
            url: PostUrl,
            type: "POST",
            data: { id: QuizID, iud: "17", data: x },
            dataType: "text",
            beforeSend: function () {
                GMLoader.open();
            },
            success: function (res) {
                if (res == "success") {

                }
                else {
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
    }
}

function SaveCourseSectionStudyQuiz() {
    if (IsQuizBasicInfoFormValid()) {
        var x =
       "<data>" +
           "<quiz_id>" + QuizID + "</quiz_id>" +
           "<course_id>" + gup("id") + "</course_id>" +
           "<caption><![CDATA[" + $("#QuizCaptionTextBox").val() + "]]></caption>" +
           "<is_published>" + $("#HFIsPublished").val() + "</is_published>" +
       "</data>";
        $.ajax({
            url: PostUrl,
            type: "POST",
            data: { id: QuizID, iud: "17", data: x },
            dataType: "text",
            beforeSend: function () {
                GMLoader.open();
            },
            success: function (res) {
                if (res == "success") {

                }
                else {
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
    }
}

function SaveAssetQuiz() {
    if (IsQuizBasicInfoFormValid()) {
        var x =
       "<data>" +
           "<quiz_id>" + QuizID + "</quiz_id>" +
           "<asset_id>" + gup("id") + "</asset_id>" +
           "<caption><![CDATA[" + $("#QuizCaptionTextBox").val() + "]]></caption>" +
           "<is_practice>" + $("#HFPractice").val() + "</is_practice>" +
           "<allow_hints>" + $("#HFHints").val() + "</allow_hints>" +
           "<allow_analysis>" + $("#HFAnalysis").val() + "</allow_analysis>" +
           "<allow_other>" + $("#HFOther").val() + "</allow_other>" +
           "<allow_skip>" + $("#HFSkip").val() + "</allow_skip>" +
           "<time><![CDATA[" + HMSToSeconds($("#TimeTextBox").val()) + "]]></time>" +
       "</data>";
        $.ajax({
            url: PostUrl,
            type: "POST",
            data: { id: QuizID, iud: "18", data: x },
            dataType: "text",
            beforeSend: function () {
                GMLoader.open();
            },
            success: function (res) {
                if (res == "success") {

                }
                else {
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
    }
}
/*-------------------------------------------/ Quiz Stuff --------------------------------------------*/