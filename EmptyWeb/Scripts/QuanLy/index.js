$(function () {
    var placeHolder = $('.place-holder');

    //$('#collapseReport').

    //reports BEGIN
    $('#collapseReport').on('shown.bs.collapse', function () {
        $(this).AjaxData("/QuanLy/GetReportData", null, UpdateReportData, null);
    });

    function UpdateReportData(data) {
        $('.btn-load-dangkylamgiasu').next('.badge').html(data.wantToBeTutorCount);
        $('.btn-load-yeucautimgiasu').next('.badge').html(data.lookForTutorCount);
        $('.btn-load-feedback').next('.badge').html(data.feedbackCount);
    }

    $('a.btn-load-dangkylamgiasu').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/GetAllDangKyLamGiaSu', null);
    });

    $('a.btn-load-yeucautimgiasu').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/GetAllYeuCauTimGiaSu', null);
    });

    $('a.btn-load-log').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/GetAllLog', null);
    });
    //reports END


    //Site modules BEGIN
    $('a.btn-load-muc').on('click', function () {
        placeHolder.AjaxHtml('/QuanLy/Muc', null);
    });
    //Site modules END
});