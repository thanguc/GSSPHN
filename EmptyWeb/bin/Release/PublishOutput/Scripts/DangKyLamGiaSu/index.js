var _rawData = {
    provinces: null,
    academic_levels: ['Sinh viên', 'Giáo viên', 'Giảng viên', 'Trợ giảng', 'Khác'],
    genders: ['Nam', 'Nữ']
}

var $slQueQuan = $('#selectQueQuan');
var $slGioiTinh = $('#selectGioiTinh');
var $slTrinhDo = $('#selectTrinhDo');

$slQueQuan.AjaxData('/DangKyLamGiaSu/GetListQueQuan', null,
    function (data) {
        _rawData.provinces = data;
        $slQueQuan.bindSelect2(_rawData.provinces, '- Tỉnh/Thành phố -', false, 1);
    }, null
);

$slGioiTinh.bindSelect2(_rawData.genders, '- Giới tính -', true, null);

$slTrinhDo.bindSelect2(_rawData.academic_levels, '- Trình độ -', true, null);

$slTrinhDo.on('change', function () {
    if ($("#selectTrinhDo option:selected").text() === 'Khác') {
        $("#trinhDoKhac").removeClass('hidden');
        $("#trinhDoKhac input").prop('required', 'true');
    } else {
        $("#trinhDoKhac").addClass('hidden');
        $("#trinhDoKhac input").removeAttr('required');
        $("#trinhDoKhac input").val('');
    }
});