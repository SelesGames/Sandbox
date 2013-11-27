function textOverlay() {
    $('a.fill-div').each(function (idx) {
        var left = $(this).position().left;
        var top = $(this).position().top;
        var extraDelta = $(this).height() / 2 - 18;
        var width = $(this).width();
        $(this).find('.text-overlay').css('width', width).css('top', top + extraDelta).css('left', left);
    });
    $('.text-overlay').show();
}

$(function () {

    textOverlay();
    $(window).resize(function () {
        $('.text-overlay').hide();
        textOverlay();
    });

});