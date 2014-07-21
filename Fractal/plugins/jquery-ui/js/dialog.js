var DialogDivID = "#dialog";
$(document).ready(function () {    
    $(DialogDivID).dialog({
        autoOpen: false,
        resizable: false,
        show: "fade",
        hide: "fade",
        modal: true
    });    
});
