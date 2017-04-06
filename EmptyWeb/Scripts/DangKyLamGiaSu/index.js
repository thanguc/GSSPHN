$('.form-group').find('label').addClass('control-label col-md-2');
$('.form-group').find('input').addClass('form-control');
$('.form-group').find('select').addClass('form-control');

$("#selectGioiTinh").select2({
    theme: "bootstrap",
    placeholder: "- Chọn Giới Tính -",
    minimumResultsForSearch: -1
});

$('#datetimepicker').datetimepicker({
    format: "DD/MM/YYYY"
});

var provinces_rawdata = ['An Giang', 'Bà Rịa-Vũng Tàu', 'Bạc Liêu', 'Bắc Kạn', 'Bắc Giang', 'Bắc Ninh', 'Bến Tre', 'Bình Dương', 'Bình Định', 'Bình Phước', 'Bình Thuận', 'Cà Mau', 'Cao Bằng', 'Cần Thơ', 'Đà Nẵng', 'Đắk Lắk', 'Đắk Nông', 'Điện Biên', 'Đồng Nai', 'Đồng Tháp', 'Gia Lai', 'Hà Giang', 'Hà Nam', 'Hà Nội', 'Hà Tây', 'Hà Tĩnh', 'Hải Dương', 'Hải Phòng', 'Hòa Bình', 'Hồ Chí Minh', 'Hậu Giang', 'Hưng Yên', 'Khánh Hòa', 'Kiên Giang', 'Kon Tum', 'Lai Châu', 'Lào Cai', 'Lạng Sơn', 'Lâm Đồng', 'Long An', 'Nam Định', 'Nghệ An', 'Ninh Bình', 'Ninh Thuận', 'Phú Thọ', 'Phú Yên', 'Quảng Bình', 'Quảng Nam', 'Quảng Ngãi', 'Quảng Ninh', 'Quảng Trị', 'Sóc Trăng', 'Sơn La', 'Tây Ninh', 'Thái Bình', 'Thái Nguyên', 'Thanh Hóa', 'Thừa Thiên - Huế', 'Tiền Giang', 'Trà Vinh', 'Tuyên Quang', 'Vĩnh Long', 'Vĩnh Phúc', 'Yên Bái'];
var provinces_selectdata = [];
for (var item in provinces_rawdata) {
    provinces_selectdata.push({ id: provinces_selectdata.length, text: provinces_rawdata[provinces_selectdata.length] });
}

$("#selectQueQuan").select2({
    theme: 'bootstrap',
    placeholder: '- Tỉnh/Thành phố -',
    data: provinces_selectdata
})