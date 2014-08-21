var Abort;

$(function () {
    Abort = $("#HFAbort").val();

    $('.sign.in > a').click(function () {
        $('.sign.in > div').removeClass('hide');
        $(document).click(function (e) {
            if ($(e.target).parents('.sign.in').length == 0) {
                $('.sign.in > div').addClass('hide');
                $(document).unbind("click");
            }
        });
        return false;
    });
    $('.login > a').click(function () {
        $(this).parent('div').addClass('hide');
        $('.sign.in .forgot').removeClass('hide');
        $('.sign.in > div').addClass('small');
        return false;
    });
    $('.forgot > a').click(function () {
        $('.sign.in .forgot').addClass('hide');
        $('.sign.in .login').removeClass('hide');
        $('.sign.in > div').removeClass('small');
        return false;
    });

    $('.grid .name a').mouseover(function () {
        var SpanWidth = $(this).parent().parent().width();
        var LinkWidth = $(this).width();

        if (LinkWidth > SpanWidth) {
            speed = 4000 + 13 * (LinkWidth - SpanWidth);
            if (LinkWidth < 600) { speed = 3000 }
            //console.log(speed);
            $(this).stop().animate({ 'left': -(LinkWidth - SpanWidth) }, speed)
        }
    });
    $('.grid .name a').mouseout(function () {
        $(this).stop().animate({ 'left': 0 }, 1000)
    });

    $("#LoginUsernameTextBox,#LoginPasswordTextBox").keydown(function (e) {
        if (e.which == 13) {
            SignIn();
            return false;
        }
    });

    $("#SignInButton").click(function () {
        SignIn();
    });

    $("#ForgetPasswordEmail").keydown(function (e) {
        if (e.which == 13) {
            ResetPasswordRequest();
            return false;
        }
    });
    
    $("#ForgetPasswordButton").click(function () {
        ResetPasswordRequest();
    });
});

function ResetPasswordRequest() {
    $.ajax({
        url: /api/,
        type: "POST",
        data: { iud: "1", email: $("#ForgetPasswordEmail").val() },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            var json = JSON.parse(res);
            if (json != null) {
                if (json.res == "error") {
                    alert(json.message)
                }
                else {
                    $(".sign.in .forgot span").Show();
                    $(".sign.in .forgot input").Hide();
                }
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
}

function SignIn() {
    $.ajax({
        url: /api/,
        type: "POST",
        data: { iud: "0", username: $("#LoginUsernameTextBox").val(), password: $("#LoginPasswordTextBox").val() },
        dataType: "text",
        beforeSend: function () {
            GMLoader.open();
        },
        success: function (res) {
            var json = JSON.parse(res);
            if (json != null) {
                if (json.res == "error") {
                    alert(json.message)
                }
                else {
                    window.location = document.URL;
                }
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
}