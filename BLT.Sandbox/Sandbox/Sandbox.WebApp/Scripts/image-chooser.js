// requires <input type="file" name="image" id="image" /> in multipart/form-data form
$(function () {
    var $file = $(':file').css({
        visibility: 'hidden',
        display: 'none'
    })
    .after('<input type="text" readonly="readonly" value="" class="col-xs-12 col-sm-6 col-md-3 imageChooserLabel" />'
                + '<input type="button" value="Browse" class="btn btn-sm imageChooserButton"/>')
    .change(function () {
        $('.imageChooserLabel').val($(this).val());
    });
    $('.imageChooserButton').click(function () {
        $file.click();
    });
});
