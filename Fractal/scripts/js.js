$(function(){
    $('.sign > a').click(function () {
        $('.sign > div').removeClass('hide');
        $(document).click(function (e) {
            if ($(e.target).parents('.sign').length == 0) {
                $('.sign > div').addClass('hide');
                $(document).unbind("click");
            }
        });
        return false;
    });
    $('.login > a').click(function () {
        console.log('a');
        $(this).parent('div').addClass('hide');
        $('.sign .forgot').removeClass('hide');
        $('.sign > div').addClass('small');
        return false;
    });
    $('.forgot > a').click(function () {
        $('.sign .forgot').addClass('hide');
        $('.sign .login').removeClass('hide');
        $('.sign > div').removeClass('small');
        return false;
    });

    $('.slider > ul').bxSlider({
        auto: true,
        controls: false
    });

});