var $slGioiTinh = $('#selectGioiTinh');
var $slTrinhDo = $('#selectTrinhDo');

$slTrinhDo.AjaxPost({
    url: '/CommonList/GetListTrinhDo',
    onSuccess: function (data) {
        $slTrinhDo.bindSelect2(data, '- Trình độ -', true, 1);
    }
});

$slGioiTinh.bindSelect2(['Nam', 'Nữ'], '- Giới tính -', true, null);

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