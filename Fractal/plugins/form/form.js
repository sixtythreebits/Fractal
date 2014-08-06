$(function () {
    $("div.select").each(function () {
        var Text = $(this).children("select").children("option:selected").text();
        Text = Text.length > 40 ? Text.substring(0, 37) + " ..." : Text;
        $(this).children("p").text(Text);
    });

    $("div.select select").change(function () {
        var Text = $(this).children("option:selected").text();
        Text = Text.length > 40 ? Text.substring(0, 37) + " ..." : Text;
        $(this).parent().children("p").text(Text);
    });
});

$.fn.numeric = function () {
    this.keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
            // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });
}