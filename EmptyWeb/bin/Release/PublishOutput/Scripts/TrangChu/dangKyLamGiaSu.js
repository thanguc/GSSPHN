$(function () {
    toastr.options = {
        "positionClass": "toast-top-center"
    };
    var $slQueQuan = $('#selectQueQuan');
    var $slGioiTinh = $('#selectGioiTinh');
    var $slTrinhDo = $('#selectTrinhDo');

    $slQueQuan.AjaxPost({
        url: '/CommonList/GetListQueQuan',
        onSuccess: function (data) {
            $slQueQuan.bindSelect2(data, '- Tỉnh/Thành phố -', false, 1);
        }
    });

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
        } else {
            $("#trinhDoKhac").addClass('hidden');
            $("#trinhDoKhac input").val('');
        }
    });

    $('.panel-collapse').on('show.bs.collapse', function () {
        $(this).siblings('.panel-heading').addClass('active');
    });

    $('.panel-collapse').on('hide.bs.collapse', function () {
        $(this).siblings('.panel-heading').removeClass('active');
    });

    /* upload image*/
    var $uploadCrop;

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.upload-anh-the').addClass('ready');
                $uploadCrop.croppie('bind', {
                    url: e.target.result
                }).then(function () {
                    //console.log('jQuery bind complete');
                });
            }
            reader.readAsDataURL(input.files[0]);
        }
        else {
            //swal("Sorry - you're browser doesn't support the FileReader API");
        }
    }

    $uploadCrop = $('.upload-anh-the').croppie({
        enableExif: true,
        viewport: {
            width: 150,
            height: 225,
            type: 'square'
        },
        boundary: {
            width: 150,
            height: 225
        }
    });

    $('input[name=fileAnhThe]').on('change', function () {
        readFile(this);
    });

    $('.btn-action-confirm').on('click', function () {
        if (!$(".form-check input").is(":checked")) {
            toastr.warning("Xin vui lòng đồng ý cam kết với các điều khoản đăng ký!");
            return;
        }
        if ($('.upload-anh-the').is(".ready")){
            $uploadCrop.croppie('result', {
                type: 'blob',
                size: 'viewport',
                format: 'jpeg'
            }).then(function (r) {
                submitForm(r);
            });
        } else {
            submitForm();
        }
    });

    function submitForm(anhThe) {
        var form = $(".form-dang-ky").formize();
        var data = new FormData();
        data.append("HoTen", form.get("HoTen"));
        data.append("NgaySinh", form.get("NgaySinh"));
        data.append("GioiTinh", form.get("GioiTinh"));
        data.append("QueQuanID", form.get("QueQuanID"));
        data.append("SDT", form.get("SDT"));
        data.append("DiaChi", form.get("DiaChi"));
        data.append("Email", form.get("Email"));
        data.append("TrinhDoID", form.get("TrinhDoID"));
        data.append("TrinhDoKhac", form.get("TrinhDoKhac"));
        data.append("DonVi", form.get("DonVi"));
        data.append("ChuyenNganh", form.get("ChuyenNganh"));
        data.append("GioiThieu", form.get("GioiThieu"));
        if (anhThe) {
            data.append("fileAnhThe", anhThe, "anh-the.jpeg");
        }
        $(".form-dang-ky").parent().AjaxFormData({
            url: form.action,
            data: data,
            onSuccess: function () {
                $(".alert-DangKyLamGiaSuThanhCong").closest(".modal").on("hidden.bs.modal", function () {
                    location.href = "/";
                }).modal("show");
            },
            onError: function (err) {
                if (err && err.length && err.length > 0) {
                    toastr.error(err.join("<br>"));
                } else {
                    $(".alert-DangKyLamGiaSuThatBai").closest(".modal").modal("show");
                }
            }
        });
    }
})
