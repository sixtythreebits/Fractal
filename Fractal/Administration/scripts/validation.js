/*
field - StreamingTutors standart formated input caption to find corresponding error fields for display
ErrorMessage - Message text  for error
ErrorVarStr - Indicator error variable for assigning "true" value to it.
*/
function SetInputFieldError(field, ErrorMessage, ErrorVarStr) {
    ClearIndicatorFields(field);

    $(".form." + field).children("label").after("<p class=\"error-text\">" + ErrorMessage + "</p>");
    $(".form." + field).children().last().after("<div class=\"icon fail\"></div>");
    if (!(ErrorVarStr == null || ErrorVarStr == undefined || ErrorVarStr == "")) {
        eval(ErrorVarStr + " = true;");
    }
}

/*
field - StreamingTutors standart formated input caption to find corresponding success fields for display.
ErrorVarStr - Indicator error variable for assigning "false" value to it.
*/
function SetInputFieldSuccess(field, ErrorVarStr) {
    ClearIndicatorFields(field);    
    $(".form." + field).children().last().after("<div class=\"icon success\"></div>");
    eval(ErrorVarStr + " = false;");
}

/*
field - StreamingTutors standart formated input caption to find corresponding success fields for display.
ErrorVarStr - Indicator error variable for assigning "false" value to it.
*/
function SetInputFieldNonVisualSuccess(field, ErrorVarStr) {
    ClearIndicatorFields(field);    
    eval(ErrorVarStr + " = false;");
}

/*
field - StreamingTutors standart formated input caption to find corresponding loading fields for display.
*/
function SetInputFieldLoading(field) {
    ClearIndicatorFields(field);
    $(".form." + field).children("span").after("<div class=\"icon loader\"></div>");
}


/*
field - StreamingTutors standart formated input caption to clear any error, success, or whatever indications.
*/
function ClearIndicatorFields(field) {
    $(".form." + field).children("p").remove();
    $(".form." + field).children("div:last-child").remove();
}

/*
field - html element id where window must be scrolled during an error
*/
function ScrollTo(field) {
    $(window).scrollTo("#" + field, { duration: 300 }, { easing: 'elasout' });
}

/*
validates only numbers
*/
function validatenumbers(key) {
    var keycode = (key.which) ? key.which : key.keyCode;
    if (((keycode > 47) && (keycode < 58)) || (keycode == 8) || (keycode == 9) || (keycode == 39) || (keycode == 37) || (keycode == 46)) {
        return true;
    }
    else {        
        return false;
    }
}

