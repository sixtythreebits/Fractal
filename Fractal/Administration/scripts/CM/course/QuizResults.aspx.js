$(document).ready(function () {
    $(".datepicker input").datepicker({
        dateFormat: "M dd, y",
        onSelect: function (dateText, inst) {
            QuizResultsGrid.Refresh();
        }
    });

    $(".datepicker input").change(function (e) {
        if ($(this).val().length == 0) {
            QuizResultsGrid.Refresh();
        }
    });

    $("#SearchTextBox").keyup(function (e) {
        if (e.which == 13) {
            QuizResultsGrid.Refresh();
        }
    });

    $(".static").on("click", ".course-quiz-results .div2 a", function (e) {
        e.preventDefault();
        FancyBox.Init({
            href: $(this).attr("href"),
            height: 768,
            width: 1024
        }).ShowPopup();        
    });
});