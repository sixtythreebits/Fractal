var SuccessErrorObject = new Object();

$(function () {
    if ($(".success-massage").length > 0) {
        SuccessErrorObject.HideSuccessWithTimeout(5);
    }
});

SuccessErrorObject.ShowSuccess = function () {
    $(".success-massage").show();
}

SuccessErrorObject.HideSuccess = function () {
    $(".success-massage").slideUp(200);
}

SuccessErrorObject.HideSuccessWithTimeout = function (seconds) {
    setTimeout(function () {
        $(".success-massage").slideUp(400);
    }, seconds * 1000);
}

SuccessErrorObject.ShowError = function () {
    $(".error-massage").show();
}

SuccessErrorObject.HideError = function () {
    $(".error-massage").slideUp();
}

SuccessErrorObject.HideErrorWithTimeout = function (seconds) {
    setTimeout(function () {
        $(".error-massage").slideUp(400);
    }, seconds * 1000);
}