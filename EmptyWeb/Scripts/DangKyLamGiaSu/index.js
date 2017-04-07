var _rawData = {
    provinces: ['An Giang', 'Bà Rịa-Vũng Tàu', 'Bạc Liêu', 'Bắc Kạn', 'Bắc Giang', 'Bắc Ninh', 'Bến Tre', 'Bình Dương', 'Bình Định', 'Bình Phước', 'Bình Thuận', 'Cà Mau', 'Cao Bằng', 'Cần Thơ', 'Đà Nẵng', 'Đắk Lắk', 'Đắk Nông', 'Điện Biên', 'Đồng Nai', 'Đồng Tháp', 'Gia Lai', 'Hà Giang', 'Hà Nam', 'Hà Nội', 'Hà Tây', 'Hà Tĩnh', 'Hải Dương', 'Hải Phòng', 'Hòa Bình', 'Hồ Chí Minh', 'Hậu Giang', 'Hưng Yên', 'Khánh Hòa', 'Kiên Giang', 'Kon Tum', 'Lai Châu', 'Lào Cai', 'Lạng Sơn', 'Lâm Đồng', 'Long An', 'Nam Định', 'Nghệ An', 'Ninh Bình', 'Ninh Thuận', 'Phú Thọ', 'Phú Yên', 'Quảng Bình', 'Quảng Nam', 'Quảng Ngãi', 'Quảng Ninh', 'Quảng Trị', 'Sóc Trăng', 'Sơn La', 'Tây Ninh', 'Thái Bình', 'Thái Nguyên', 'Thanh Hóa', 'Thừa Thiên - Huế', 'Tiền Giang', 'Trà Vinh', 'Tuyên Quang', 'Vĩnh Long', 'Vĩnh Phúc', 'Yên Bái'],
    academic_levels: ['Sinh viên', 'Giáo viên', 'Giảng viên', 'Trợ giảng', 'Khác'],
    genders: ['Nam', 'Nữ']
}

$('.form-group').find('label').addClass('control-label col-md-2');
$('.form-group').find('input').addClass('form-control');
$('.form-group').find('select').addClass('form-control');

bindSelect2('#selectGioiTinh', _rawData.genders, '- Giới tính -', true);
bindSelect2('#selectQueQuan', _rawData.provinces, '- Tỉnh/Thành phố -', false);
bindSelect2('#selectTrinhDo', _rawData.academic_levels, '- Trình độ -', true);

$('#datetimepicker').datetimepicker({
    format: "DD/MM/YYYY"
});

function bindSelect2(selector, rawData, placeholder, hideSearch) {
    var selectData = [];
    for (var item in rawData) {
        selectData.push({ id: selectData.length, text: rawData[selectData.length] });
    }
    $(selector).select2({
        theme: "bootstrap",
        placeholder: placeholder,
        data: selectData,
        minimumResultsForSearch: hideSearch ? -1 : 0
    });
}

$('#selectTrinhDo').on('change', function () {
    if ($("#selectTrinhDo option:selected").text() == 'Khác') {
        $("input[name='TrinhDoKhac']").attr('type', 'text');
    } else {
        $("input[name='TrinhDoKhac']").attr('type', 'hidden');
    }
});