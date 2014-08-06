/*
field - StreamingTutors standart formated input caption to find corresponding error fields for display
ErrorMessage - Message text  for error
ErrorVarStr - Indicator error variable for assigning "true" value to it.
*/
$(document).ready(function () {
    $(["/plugins/icons/img/success.png", "/plugins/icons/img/loader.gif"]).preload();
});

function SetInputFieldError(selector, ErrorMessage, ErrorVarStr) {
    ClearIndicatorFields(selector);    
    $(".form .left div" + selector + " input,.form .left div" + selector + " .select").addClass("error");
    $(".form .right span" + selector).Hide();
    $(".form .right span.error" + selector).Show();
    $(".form .right span.error" + selector).text(ErrorMessage);

    if (!(ErrorVarStr == null || ErrorVarStr == undefined || ErrorVarStr == "")) {
        eval(ErrorVarStr + " = true;");
    }
}

/*
field - StreamingTutors standart formated input caption to find corresponding success fields for display.
ErrorVarStr - Indicator error variable for assigning "false" value to it.
*/
function SetInputFieldSuccess(selector, ErrorVarStr) {
    ClearIndicatorFields(selector);
    $(".form .left div" + selector).append('<img src="/plugins/icons/img/success.png" alt="done" />');

    if (!(ErrorVarStr == null || ErrorVarStr == undefined || ErrorVarStr == "")) {
        eval(ErrorVarStr + " = false;");
    }
}

/*
field - StreamingTutors standart formated input caption to find corresponding success fields for display.
ErrorVarStr - Indicator error variable for assigning "false" value to it.
*/
function SetInputFieldNonVisualSuccess(selector, ErrorVarStr) {
    ClearIndicatorFields(selector);

    if (!(ErrorVarStr == null || ErrorVarStr == undefined || ErrorVarStr == "")) {
        eval(ErrorVarStr + " = false;");
    }
}

/*
field - StreamingTutors standart formated input caption to find corresponding loading fields for display.
*/
function SetInputFieldLoading(selector) {
    ClearIndicatorFields(selector);
    $(".form .left div" + selector).append('<img src="/plugins/icons/img/loader.gif" alt="loading" />');
}


/*
field - StreamingTutors standart formated input caption to clear any error, success, or whatever indications.
*/
function ClearIndicatorFields(selector) {    
    $(".form .left div" + selector + " input,.form .left div" + selector + " .select").removeClass("error");
    $(".form .left div" + selector).children("img").remove();
    $(".form .right span" + selector).removeClass("hidden");
    $(".form .right span.error" + selector).addClass("hidden");
}

/*
field - html element id where window must be scrolled during an error
*/
function ScrollTo(selector) {
    $(window).scrollTo(selector, { duration: 300 }, { easing: 'elasout' });
}