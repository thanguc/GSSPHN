$.fn.AjaxPost = function (options) {
    var $this = $(this);
    $.ajax({
        url: options.url,
        type: 'post',
        data: options.data,
        beforeSend: function () {
            $this.addClass("position-relative").ploading({ action: 'show' });
        },
        success: function (r) {
            if (r.Error) {
                if (options.onError) {
                    options.onError(r.Error);
                } else {
                    toastr.error(r.Error);
                }
            } else if (options.onSuccess) {
                options.onSuccess(r);
            }
        },
        error: function (r) {
            if (options.onError) {
                options.onError(r);
            } else {
                toastr.error('Có lỗi đã xảy ra! Xin vui lòng thử lại sau hoặc liên hệ với admin.');
            }
        },
        complete: function () {
            $this.removeClass("position-relative").ploading({ action: 'hide' });
        }
    });
};

$.fn.AjaxFormData = function (options) {
    var $this = $(this);
    $.ajax({
        url: options.url,
        type: 'post',
        data: options.data,
        processData: false,
        contentType: false,
        beforeSend: function () {
            $this.addClass("position-relative").ploading({ action: 'show' });
        },
        success: function (r) {
            if (r.Error) {
                if (options.onError) {
                    options.onError(r.Error);
                } else {
                    toastr.error(r.Error);
                }
            } else if (options.onSuccess) {
                options.onSuccess(r);
            }
        },
        error: function (r) {
            if (options.onError) {
                options.onError(r);
            } else {
                toastr.error('Có lỗi đã xảy ra! Xin vui lòng thử lại sau hoặc liên hệ với admin.');
            }
        },
        complete: function () {
            $this.removeClass("position-relative").ploading({ action: 'hide' });
        }
    });
};

$.fn.AjaxHtml = function (url, params) {
    var $this = $(this);
    $.ajax({
        url: url,
        type: 'post',
        data: params,
        dataType: 'html',
        beforeSend: function () {
            $this.addClass("position-relative").ploading({ action: 'show' });
        },
        success: function (html) {
            $this.removeClass("position-relative").ploading({ action: 'hide' });
            $this.empty().html(html);
        },
        complete: function () {
            $this.removeClass("position-relative").ploading({ action: 'hide' });
        }
    });
};

$.fn.AjaxDialog = function (url, params) {
    var $this = $(this);
    $.ajax({
        url: url,
        type: 'post',
        data: params,
        dataType: 'html',
        success: function (html) {
            var $modal = $(html);
            $this.parent().append($modal);
            $modal.modal('show');
            $('.modal-backdrop.in:not(.fade)').remove();
            $modal.on('hidden.bs.modal', function () {
                $modal.remove();
            });
        }
    });
};

$.fn.formize = function () {
    return new Formize($(this));
};

function Formize($this) {
    this.self = $this;

    this.action = $this.attr('action');

    this.get = function (field) {
        return $this.find('[name="' + field + '"]').val();
    };

    this.set = function (field, value) {
        $this.find('[name="' + field + '"]').val(value);
    };

    this.field = function (field) {
        return $this.find('[name="' + field + '"]');
    };

    //this.isValid = function () {
    //    console.log('ivl');
    //    $this.on("submit", function (e) {
    //        console.log('sm');
    //        e.preventDefault();
    //    });
    //    $this.submit();
    //    return $this.checkValidity();
    //};

    this.destroy = function () {
        this.self = null;
        this.action = null;
        this.get = undefined;
        this.set = undefined;
        this.field = undefined;
        this.destroy = undefined;
    };
}

$.fn.bindSelect2 = function (rawData, placeholder, hideSearch, startId) {
    var selectData = [];
    var $this = $(this);
    for (var item in rawData) {
        selectData.push({ id: startId ? startId : selectData.length, text: rawData[selectData.length] });
        startId++;
    }
    $this.select2({
        theme: "bootstrap",
        placeholder: placeholder,
        data: selectData,
        minimumResultsForSearch: hideSearch ? -1 : 0
    });
    if ($this.attr('value')) {
        //console.log($this.select2());
        $this.select2('val', $this.attr('value'));
    }
};

var SITE = new function () {
    this.normalize = function (str) {
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/ /g, "-");
        str = str.replace(/[^a-zA-Z0-9-]/g, '');
        return str;
    };

    this.closeOpeningModal = function (onHiddenMethod) {
        $openingModal = $('.modal.in');
        if (onHiddenMethod !== null && onHiddenMethod !== undefined) {
            $openingModal.on('hidden.bs.modal', function () {
                onHiddenMethod();
                $openingModal.off('hidden.bs.modal');
            });
        }
        $openingModal.modal('hide');
    };
};