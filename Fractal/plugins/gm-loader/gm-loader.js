var GMLoader = new Object();

GMLoader.state = false;
GMLoader.html = '<div class="gm_loader"><div class="gm_loader_bg"></div><div class="gm_loader_content"><div><span class="gm_loader_logo"></span><span class="gm_loader_bar"></span></div></div></div>';

GMLoader.open = function () {

    if (!GMLoader.state) {
        $("body").append(GMLoader.html);
        GMLoader.state = true;
    }
}

GMLoader.close = function () {
    if (GMLoader.state) {
        $(".gm_loader").remove();
        GMLoader.state = false;

    }
}

$.fn.extend({
    ShowLoader: function () {
        this.append(GMLoader.html);
    },
    HideLoader: function () {
        this.find(".gm_loader").fadeOut(1500, function () { $(this).remove(); });
    }
});
