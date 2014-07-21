<%@ Page Title="Quizes" Language="C#" MasterPageFile="../../MasterPage.master" AutoEventWireup="true" Inherits="management_CM_quizes" Codebehind="quizes.aspx.cs" %>
<%@ MasterType TypeName="administration_MasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">    
    <script src="<%=Master.Root %>scripts/CM/quizes.aspx.js" type="text/javascript"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="quiz-list" class="static">
    <div class="main-title">
	    <span class="left"></span>
        <a href="#" class="icon text add">დამატება</a>    
        <input  class="search" type="text"/>
        <span class="right"></span>
    </div>    
    <asp:Repeater ID="QuizRepeater" runat="server">
    <HeaderTemplate>    
    <div class="had type2">
		<span class="title">დასახელება</span>
		<span class="question">შეკითხვები</span>
		<span class="date">შექმნის თარიღი &nbsp;&nbsp;&nbsp;&nbsp;</span>
	</div>
    <p class="type2">არ არის ჩანაწერი!!!</p>
	<ul class="type2">
    </HeaderTemplate>
    <ItemTemplate>
    <li id="<%# Eval("IDEncoded") %>">
		<a href="quiz.aspx?id=<%#Eval("ID") %>" class="title"><%#Eval("Caption") %></a>
		<span><%#Eval("QuestionsCount")%></span>
		<p><%#Eval("CRTime")%></p>
		<a href="#" class="icon rename"></a>
		<a href="#" class="icon delete"></a>
	</li>
    </ItemTemplate>
    <FooterTemplate>        
    </ul>
    </FooterTemplate>
    
    </asp:Repeater>						
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsPlaceHolder" Runat="Server">
<script type="text/javascript">
    var quizList = new Object();
    var QuizItemHtml = '<li id="[ID]"><a href="[URL]" class="title">[CAPTION]</a><span>0</span><p>[DATE]</p><a href="#" class="icon rename"></a><a href="#" class="icon delete"></a></li>';

    $(document).ready(function () {

        InitNoQuizzes();

        $("#view .main-title > input.search").keyup(function (e) {
            //alert($(this).val());
            var sr = $(this).val();
            if (e.which != 13) {
                $("#quiz-list ul li").each(function (e) {
                    if ($(this).children(".title").text().toUpperCase().match(sr.toUpperCase())) {
                        $(this).show(200);
                    }
                    else {
                        $(this).hide(200);
                    }
                });
            }
        });

        $(".add").click(function (e) {
            $.ajax({
                url: document.URL,
                type: "POST",
                data: { action: "create" },
                dataType: "text",
                beforeSend: function () {
                    GMLoader.open();
                },
                success: function (res) {

                    if (res.length > 0) {
                        var json = JSON.parse(res);
                        var html = QuizItemHtml.replace("[ID]", json.delete_id)
                                               .replace("[URL]", "quiz.aspx?id=" + json.id)
                                               .replace("[CAPTION]", json.caption)
                                               .replace("[DATE]", json.date);
                        $("ul.type2").prepend(html);
                        $("#" + json.delete_id + " .icon.rename").trigger("click");
                        InitNoQuizzes();
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

            return false;
        });

        $("ul.type2").on("click", ".delete", function (e) {
            e.preventDefault();
            var _this = $(this);
            $(DialogDivID).dialog("option",
            {
                title: "ტესტის წაშლა",
                width: 300,
                buttons:
                {
                    "კი": function () {
                        var row = _this.parent();

                        $.ajax({
                            url: document.URL,
                            type: "POST",
                            data: { action: "delete", id: row.attr("id") },
                            dataType: "text",
                            beforeSend: function () {
                                _this.addClass("loader");
                            },
                            success: function (res) {
                                if (res == "success") {
                                    _this.parent().hide(200, function () {
                                        $(this).remove();
                                        InitNoQuizzes();
                                    });
                                }
                                else {
                                    alert(Abort);
                                }
                            },
                            error: function (res) {
                                alert(Abort);
                            },
                            complete: function () {
                                _this.removeClass("loader");
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
        });

        $("ul.type2").on("click", ".rename", function (e) {
            var _this = $(this);
            var title = _this.parent().children("a.title");

            title.hide();
            title.after('<input type="text"/>');
            _this.addClass("disabled");

            var editor = _this.parent().children("input");
            editor.show();
            editor.val(title.text());
            editor.select();

            editor.focusout(function (e) {

                $.ajax({
                    url: document.URL,
                    type: "POST",
                    data: { action: "rename", id: editor.parent().attr("id"), name: editor.val() },
                    dataType: "text",
                    beforeSend: function () {
                        _this.addClass("loader");
                    },
                    success: function (res) {
                        if (res == "success") {
                            title.text(editor.val());
                        }
                        else {
                            alert(Abort);
                        }
                    },
                    error: function (res) {
                        alert(Abort);
                    },
                    complete: function () {
                        title.show();
                        editor.remove();
                        _this.removeClass("loader");
                        _this.removeClass("disabled");
                    }
                });
            });

            editor.keypress(function (event) {
                if (event.which == 13) {
                    event.preventDefault();
                    editor.blur();

                }
            });

            return false;
        });

    });

    function InitNoQuizzes() {
        if ($("ul.type2").children().length == 0) {
            $("p.type2").Show();
        }
        else {
            $("p.type2").Hide();
        }
    }

</script>
</asp:Content>