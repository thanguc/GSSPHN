$(function () {
    toastr.options = {
        "positionClass": "toast-top-center"
    };
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

    $('.panel-collapse').on('show.bs.collapse', function () {
        $(this).siblings('.panel-heading').addClass('active');
    });

    $('.panel-collapse').on('hide.bs.collapse', function () {
        $(this).siblings('.panel-heading').removeClass('active');
    });

    $('.btn-action-confirm').on('click', function () {
        if (!$(".form-check input").is(":checked")) {
            toastr.warning("Xin vui lòng đồng ý cam kết với các điều khoản đăng ký!");
            return;
        }
        var form = $(".form-dang-ky").formize();
        var data = new FormData();
        data.append("HoTen", form.get("HoTen"));
        data.append("DiaChi", form.get("DiaChi"));
        data.append("SDT", form.get("SDT"));
        data.append("Email", form.get("Email"));
        data.append("YeuCau", form.get("YeuCau"));

        data.append("GioiTinh", form.get("GioiTinh"));
        data.append("TrinhDoId", form.get("TrinhDoId"));
        data.append("TrinhDoKhac", form.get("TrinhDoKhac"));
        data.append("DonVi", form.get("DonVi"));
        data.append("ChuyenNganh", form.get("ChuyenNganh"));
        $(".form-dang-ky").parent().AjaxFormData({
            url: form.action,
            data: data,
            onSuccess: function () {
                $(".alert-DangKyTimGiaSuThanhCong").closest(".modal").on("hidden.bs.modal", function () {
                    location.href = "/";
                }).modal("show");
            },
            onError: function (err) {
                if (err && err.length && err.length > 0) {
                    toastr.error(err.join("<br>"));
                } else {
                    $(".alert-DangKyTimGiaSuThatBai").closest(".modal").modal("show");
                }
            }
        });
    });
});
