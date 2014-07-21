var course = new Object();
course.IsLoading = false;
course.IsCloning = false;

$(document).click(function () {
    if (course.IsLoading == true && course.IsCloning == true) {
        course.IsCloning = false;
        course.IsLoading = false;
    }
});

$(document).ready(function () {

    $("#view .main-title > input.search").keyup(function (e) {
        var sr = $(this).val();
        if (e.which != 13) {

            $("#course-list .section").each(function (e) {
                var hideCount = 0;
                $(this).children("ul").children("li").each(function (e) {
                    if ($(this).children("a").text().toUpperCase().match(sr.toUpperCase())) {
                        $(this).fadeIn(100);
                    }
                    else {
                        $(this).fadeOut(100);
                        hideCount++;
                    }
                });
                if (hideCount == $(this).children("ul").children("li").length) {
                    $(this).hide(200);
                }
                else {
                    $(this).show(200);
                }
            });
        }

    });

    $(".icon.add").click(function (e) {
        course.createCourse();
        return false;
    });

    $("#course-list").on("click", ".rename", function (e) {
        course.rename($(this));
        return false;
    });

    $("#course-list").on("click", ".copy", function (e) {
        course.IsLoading = true;
        setTimeout(function () {
            course.IsCloning = true;
        }, 1000);
        return false;
    });

    $("#course-list").on("click", ".delete", function (e) {
        course.archiveCourse($(this));
        return false;
    });
});


course.createCourse = function () {
    $.ajax({
        url: document.URL,
        type: "POST",
        data: { action: "create" },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            if (res.indexOf("success") > -1) {
                var id = res.split("_")[1];
                if (!course.newCourse) {
                    $("#course-list").prepend("<div class='section'><h2>New Courses</h2><ul></ul></div>");
                    course.newCourse = $("#course-list > .section:first-child");
                }
                course.newCourse.children("ul").append('<li id="' + id + '"><div class="hover-menu"><ul><li><a title="rename course" class="icon rename"></a></li><li><a title="archive course" class="icon delete"></a></li></ul></div><div class="cover"><a href="course/default.aspx?id=' + id + '"></a><span></span></div><a href="' + Root + 'CM/course/default.aspx?id=' + id + '">New Course</a></li>');
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
};

course.rename = function (_this) {
    if (!_this.parent().parent().children("input").val()) {
        course.IsLoading = true;

        var title = _this.parent().parents("li").children("a");
        var id = title.parent().attr("id");

        title.after("<input type='text' value='" + title.text() + "'/>")
        title.hide();
        var editor = _this.parent().parents("li").children("input");
        editor.select();

        editor.focusout(function (e) {
            var val = editor.val();

            $.ajax({
                url: document.URL,
                type: "POST",
                data: { action: "rename", id: id, caption: val },
                dataType: "text",
                beforeSend: function () {
                    _this.addClass("loader");
                    _this.addClass("disabled");
                },
                success: function (res) {
                    if (res == "success") {
                        title.text(val);
                    } else {
                        alert(Abort);
                    }
                },
                error: function (res) {
                    alert(Abort);
                },
                complete: function () {
                    course.IsLoading = false;
                    title.show();
                    editor.remove();
                    _this.removeClass("loader");
                    _this.removeClass("disabled");
                }
            });
        });

        editor.keypress(function (event) {
            if (event.which == 13) {
                editor.blur();
                return false;
            }
        });
    }
};

course.archiveCourse = function (_this) {

    var li = _this.parent().parents("li");
    var ul = li.parent();
    var id = li.attr("id");

    $(DialogDivID).dialog("option",
    {
        title: "წიგნის წაშლა",
        width: 255,
        buttons:
        {
            "კი": function () {
                $(this).dialog("close");
                $.ajax({
                    url: document.URL,
                    type: "POST",
                    data: { action: "archive", id: id },
                    dataType: "text",
                    beforeSend: function () {
                        GMLoader.open();
                    },
                    success: function (res) {
                        if (res == "success") {
                            li.animate({ "opacity": 0 }, 200, function () {
                                $(this).hide(300, function () {
                                    $(this).remove();
                                    if (ul.children().length == 0) {
                                        ul.parent().remove();
                                        course.newCourse = null;
                                    }
                                });
                            });

                        } else {
                            alert(Abort);
                        }
                    },
                    error: function (res) {
                        alert(Abort);
                    },
                    complete: function () {
                        course.IsLoading = false;
                        GMLoader.close();
                    }
                });
            },

            "არა": function () {
                $(this).dialog("close");
                course.IsLoading = false;
            }
        }
    });
    course.IsLoading = true;
    $(DialogDivID).dialog("open");
};

$(window).resize(function () {
    layout.changeLayout();
});