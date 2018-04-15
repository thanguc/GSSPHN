$(function () {
    if ($(".intro-header").length == 1) {
        //$('.page-overlay-dangky').removeClass("hide");
        $('nav').addClass('landing-header');
    }

    $('#datetimepicker').datetimepicker({
        format: "DD/MM/YYYY"
    });

    $(window).bind('scroll', function () {
        if ($(window).scrollTop() > 100) {
            $('.back-to-top').show();
            if ($(".intro-header").length == 1) {
                $('nav').removeClass('landing-header');
            }
        } else {
            $('.back-to-top').hide();
            if ($(".intro-header").length == 1) {
                $('nav').addClass('landing-header');
            }
        }
    });

    $('.back-to-top').on('click', function () {
        verticalOffset = typeof verticalOffset !== 'undefined' ? verticalOffset : 0;
        element = $('body');
        offset = element.offset();
        offsetTop = offset.top;
        $('html, body').animate({ scrollTop: offsetTop }, 500, 'linear');
    });

    $('#accordion td a').on('click', function () {
        $('#accordion td a').parent('td').removeClass('active');
        $(this).parent('td').addClass('active');
    });

    $(document).on('show.bs.modal', '.modal', function () {
        $('body').addClass('hide-scrollbar');
    });

    $(document).on('hide.bs.modal', '.modal', function () {
        $('body').removeClass('hide-scrollbar');
    });

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-left",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    $.getJSON('http://ipinfo.io', function (data) {
        if (data) {
            $(".ip").html("IP: " + data.ip);
        } else {
            $(".ip").html("IP: unknown");
        }
    });
});