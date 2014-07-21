var layout = new Object();

layout.changeLayout = function () {
    $("#content").width($(window).width() - 350);
};

layout.setWidth = function (width) {
    $("#content").width(width);
};

$(document).ready(function () {
    layout.changeLayout();
});
