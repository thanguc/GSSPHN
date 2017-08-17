$(function () {
    var placeHolder = $('.place-holder');

    //reports BEGIN
    $('#collapseReport').on('shown.bs.collapse', function () {
        $(this).AjaxData("/QuanLy/GetReportData", null, UpdateReportData, null);
    });

    function UpdateReportData(data) {
        $('.btn-load-dangkylamgiasu').next('.label').html(data.wantToBeTutorCount);
        $('.btn-load-dangkytimgiasu').next('.label').html(data.lookForTutorCount);
        $('.btn-load-feedback').next('.label').html(data.feedbackCount);
    }

    $('a.btn-load-dangkylamgiasu').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/GetAllDangKyLamGiaSu', null);
    });

    $('a.btn-load-log').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/GetAllLog', null);
    });
    //reports END

});